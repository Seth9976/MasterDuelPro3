using System;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000017 RID: 23
	internal struct FrustumPlaneCuller
	{
		// Token: 0x06000083 RID: 131 RVA: 0x00003D4B File Offset: 0x00001F4B
		internal void Dispose(JobHandle job)
		{
			this.planePackets.Dispose(job);
			this.splitInfos.Dispose(job);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003D68 File Offset: 0x00001F68
		internal static FrustumPlaneCuller Create(in BatchCullingContext cc, NativeArray<Plane> receiverPlanes, in ReceiverSphereCuller receiverSphereCuller, Allocator allocator)
		{
			int splitCount = cc.cullingSplits.Length;
			int totalPacketCount = 0;
			for (int splitIndex = 0; splitIndex < splitCount; splitIndex++)
			{
				int planeCount = receiverPlanes.Length + cc.cullingSplits[splitIndex].cullingPlaneCount;
				totalPacketCount += (planeCount + 3) / 4;
			}
			FrustumPlaneCuller result = new FrustumPlaneCuller
			{
				planePackets = new NativeList<FrustumPlaneCuller.PlanePacket4>(totalPacketCount, allocator),
				splitInfos = new NativeList<FrustumPlaneCuller.SplitInfo>(splitCount, allocator)
			};
			result.planePackets.ResizeUninitialized(totalPacketCount);
			result.splitInfos.ResizeUninitialized(splitCount);
			NativeList<Plane> tmpPlanes = new NativeList<Plane>(Allocator.Temp);
			int packetBase = 0;
			for (int splitIndex2 = 0; splitIndex2 < splitCount; splitIndex2++)
			{
				CullingSplit split = cc.cullingSplits[splitIndex2];
				tmpPlanes.Clear();
				for (int i = 0; i < split.cullingPlaneCount; i++)
				{
					Plane plane = cc.cullingPlanes[split.cullingPlaneOffset + i];
					tmpPlanes.Add(in plane);
				}
				ReceiverSphereCuller receiverSphereCuller2 = receiverSphereCuller;
				if (receiverSphereCuller2.UseReceiverPlanes())
				{
					tmpPlanes.AddRange(receiverPlanes);
				}
				int packetCount = (tmpPlanes.Length + 3) / 4;
				result.splitInfos[splitIndex2] = new FrustumPlaneCuller.SplitInfo
				{
					packetCount = packetCount
				};
				for (int j = 0; j < packetCount; j++)
				{
					result.planePackets[packetBase + j] = new FrustumPlaneCuller.PlanePacket4(tmpPlanes.AsArray(), 4 * j, tmpPlanes.Length - 1);
				}
				packetBase += packetCount;
			}
			tmpPlanes.Dispose();
			return result;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003F18 File Offset: 0x00002118
		internal static uint ComputeSplitVisibilityMask(NativeArray<FrustumPlaneCuller.PlanePacket4> planePackets, NativeArray<FrustumPlaneCuller.SplitInfo> splitInfos, in AABB bounds)
		{
			float3 @float = bounds.center;
			float4 cx = @float.xxxx;
			@float = bounds.center;
			float4 cy = @float.yyyy;
			@float = bounds.center;
			float4 cz = @float.zzzz;
			@float = bounds.extents;
			float4 ex = @float.xxxx;
			@float = bounds.extents;
			float4 ey = @float.yyyy;
			@float = bounds.extents;
			float4 ez = @float.zzzz;
			uint splitVisibilityMask = 0U;
			int packetBase = 0;
			int splitCount = splitInfos.Length;
			for (int splitIndex = 0; splitIndex < splitCount; splitIndex++)
			{
				FrustumPlaneCuller.SplitInfo splitInfo = splitInfos[splitIndex];
				bool4 isCulled = new bool4(false);
				for (int i = 0; i < splitInfo.packetCount; i++)
				{
					FrustumPlaneCuller.PlanePacket4 p = planePackets[packetBase + i];
					float4 distances = p.nx * cx + p.ny * cy + p.nz * cz + p.d;
					float4 radii = p.nxAbs * ex + p.nyAbs * ey + p.nzAbs * ez;
					isCulled |= distances + radii < float4.zero;
				}
				if (!math.any(isCulled))
				{
					splitVisibilityMask |= 1U << splitIndex;
				}
				packetBase += splitInfo.packetCount;
			}
			return splitVisibilityMask;
		}

		// Token: 0x04000038 RID: 56
		public NativeList<FrustumPlaneCuller.PlanePacket4> planePackets;

		// Token: 0x04000039 RID: 57
		public NativeList<FrustumPlaneCuller.SplitInfo> splitInfos;

		// Token: 0x02000018 RID: 24
		internal struct PlanePacket4
		{
			// Token: 0x06000086 RID: 134 RVA: 0x000040A0 File Offset: 0x000022A0
			public PlanePacket4(NativeArray<Plane> planes, int offset, int limit)
			{
				Plane p0 = planes[Mathf.Min(offset, limit)];
				Plane p = planes[Mathf.Min(offset + 1, limit)];
				Plane p2 = planes[Mathf.Min(offset + 2, limit)];
				Plane p3 = planes[Mathf.Min(offset + 3, limit)];
				this.nx = new float4(p0.normal.x, p.normal.x, p2.normal.x, p3.normal.x);
				this.ny = new float4(p0.normal.y, p.normal.y, p2.normal.y, p3.normal.y);
				this.nz = new float4(p0.normal.z, p.normal.z, p2.normal.z, p3.normal.z);
				this.d = new float4(p0.distance, p.distance, p2.distance, p3.distance);
				this.nxAbs = math.abs(this.nx);
				this.nyAbs = math.abs(this.ny);
				this.nzAbs = math.abs(this.nz);
			}

			// Token: 0x0400003A RID: 58
			public float4 nx;

			// Token: 0x0400003B RID: 59
			public float4 ny;

			// Token: 0x0400003C RID: 60
			public float4 nz;

			// Token: 0x0400003D RID: 61
			public float4 d;

			// Token: 0x0400003E RID: 62
			public float4 nxAbs;

			// Token: 0x0400003F RID: 63
			public float4 nyAbs;

			// Token: 0x04000040 RID: 64
			public float4 nzAbs;
		}

		// Token: 0x02000019 RID: 25
		internal struct SplitInfo
		{
			// Token: 0x04000041 RID: 65
			public int packetCount;
		}
	}
}
