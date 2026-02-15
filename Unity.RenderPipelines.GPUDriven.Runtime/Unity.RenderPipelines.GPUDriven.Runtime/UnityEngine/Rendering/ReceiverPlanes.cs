using System;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000016 RID: 22
	internal struct ReceiverPlanes
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00003813 File Offset: 0x00001A13
		private static bool IsSignBitSet(float x)
		{
			return math.asuint(x) >> 31 > 0U;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003824 File Offset: 0x00001A24
		internal NativeArray<Plane> LightFacingFrustumPlaneSubArray()
		{
			return this.planes.AsArray().GetSubArray(0, this.lightFacingPlaneCount);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000384C File Offset: 0x00001A4C
		internal NativeArray<Plane> SilhouettePlaneSubArray()
		{
			return this.planes.AsArray().GetSubArray(this.lightFacingPlaneCount, this.planes.Length - this.lightFacingPlaneCount);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003884 File Offset: 0x00001A84
		internal static ReceiverPlanes CreateEmptyForTesting(Allocator allocator)
		{
			return new ReceiverPlanes
			{
				planes = new NativeList<Plane>(allocator),
				lightFacingPlaneCount = 0
			};
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000038B4 File Offset: 0x00001AB4
		internal void Dispose(JobHandle job)
		{
			this.planes.Dispose(job);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000038C4 File Offset: 0x00001AC4
		internal static ReceiverPlanes Create(in BatchCullingContext cc, Allocator allocator)
		{
			ReceiverPlanes result = new ReceiverPlanes
			{
				planes = new NativeList<Plane>(allocator),
				lightFacingPlaneCount = 0
			};
			if (cc.viewType == BatchCullingViewType.Light && cc.receiverPlaneCount != 0)
			{
				bool isLightOrthographic = false;
				if (cc.cullingSplits.Length > 0)
				{
					Matrix4x4 i = cc.cullingSplits[0].cullingMatrix;
					isLightOrthographic = i[15] == 1f && i[11] == 0f && i[7] == 0f && i[3] == 0f;
				}
				if (isLightOrthographic)
				{
					Vector3 lightDir = -cc.localToWorldMatrix.GetColumn(2);
					int planeSignBits = 0;
					for (int j = 0; j < cc.receiverPlaneCount; j++)
					{
						Plane plane = cc.cullingPlanes[cc.receiverPlaneOffset + j];
						if (ReceiverPlanes.IsSignBitSet(Vector3.Dot(plane.normal, lightDir)))
						{
							planeSignBits |= 1 << j;
						}
						else
						{
							result.planes.Add(in plane);
						}
					}
					result.lightFacingPlaneCount = result.planes.Length;
					if (cc.receiverPlaneCount == 6)
					{
						for (int k = 0; k < cc.receiverPlaneCount; k++)
						{
							for (int l = k + 1; l < cc.receiverPlaneCount; l++)
							{
								if (k / 2 != l / 2 && (((planeSignBits >> k) ^ (planeSignBits >> l)) & 1) != 0)
								{
									int indexA;
									int indexB;
									if (((planeSignBits >> k) & 1) != 0)
									{
										int num = l;
										int num2 = k;
										indexA = num;
										indexB = num2;
									}
									else
									{
										int num3 = k;
										int num2 = l;
										indexA = num3;
										indexB = num2;
									}
									Plane planeA = cc.cullingPlanes[cc.receiverPlaneOffset + indexA];
									Plane planeB = cc.cullingPlanes[cc.receiverPlaneOffset + indexB];
									float4 @float = new float4(planeA.normal, planeA.distance);
									float4 planeEqB = new float4(planeB.normal, planeB.distance);
									float4 silhouettePlaneEq = Line.PlaneContainingLineWithNormalPerpendicularToVector(Line.LineOfPlaneIntersectingPlane(@float, planeEqB), lightDir);
									silhouettePlaneEq /= math.length(silhouettePlaneEq.xyz);
									if (!math.any(math.isnan(silhouettePlaneEq)))
									{
										Plane plane3 = new Plane(silhouettePlaneEq.xyz, silhouettePlaneEq.w);
										result.planes.Add(in plane3);
									}
								}
							}
						}
					}
				}
				else
				{
					Vector3 lightPos = cc.localToWorldMatrix.GetPosition();
					int planeSignBits2 = 0;
					for (int m = 0; m < cc.receiverPlaneCount; m++)
					{
						Plane plane2 = cc.cullingPlanes[cc.receiverPlaneOffset + m];
						if (ReceiverPlanes.IsSignBitSet(plane2.GetDistanceToPoint(lightPos)))
						{
							planeSignBits2 |= 1 << m;
						}
						else
						{
							result.planes.Add(in plane2);
						}
					}
					result.lightFacingPlaneCount = result.planes.Length;
					if (cc.receiverPlaneCount == 6)
					{
						for (int n = 0; n < cc.receiverPlaneCount; n++)
						{
							for (int j2 = n + 1; j2 < cc.receiverPlaneCount; j2++)
							{
								if (n / 2 != j2 / 2 && (((planeSignBits2 >> n) ^ (planeSignBits2 >> j2)) & 1) != 0)
								{
									int indexA2;
									int indexB2;
									if (((planeSignBits2 >> n) & 1) != 0)
									{
										int num4 = j2;
										int num2 = n;
										indexA2 = num4;
										indexB2 = num2;
									}
									else
									{
										int num5 = n;
										int num2 = j2;
										indexA2 = num5;
										indexB2 = num2;
									}
									Plane planeA2 = cc.cullingPlanes[cc.receiverPlaneOffset + indexA2];
									Plane planeB2 = cc.cullingPlanes[cc.receiverPlaneOffset + indexB2];
									float4 float2 = new float4(planeA2.normal, planeA2.distance);
									float4 planeEqB2 = new float4(planeB2.normal, planeB2.distance);
									float4 silhouettePlaneEq2 = Line.PlaneContainingLineAndPoint(Line.LineOfPlaneIntersectingPlane(float2, planeEqB2), lightPos);
									silhouettePlaneEq2 /= math.length(silhouettePlaneEq2.xyz);
									if (!math.any(math.isnan(silhouettePlaneEq2)))
									{
										Plane plane3 = new Plane(silhouettePlaneEq2.xyz, silhouettePlaneEq2.w);
										result.planes.Add(in plane3);
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x04000036 RID: 54
		public NativeList<Plane> planes;

		// Token: 0x04000037 RID: 55
		public int lightFacingPlaneCount;
	}
}
