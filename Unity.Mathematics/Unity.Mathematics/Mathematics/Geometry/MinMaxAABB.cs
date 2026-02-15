using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics.Geometry
{
	// Token: 0x02000063 RID: 99
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct MinMaxAABB : IEquatable<MinMaxAABB>
	{
		// Token: 0x060024B0 RID: 9392 RVA: 0x000673EF File Offset: 0x000655EF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public MinMaxAABB(float3 min, float3 max)
		{
			this.Min = min;
			this.Max = max;
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x000673FF File Offset: 0x000655FF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static MinMaxAABB CreateFromCenterAndExtents(float3 center, float3 extents)
		{
			return MinMaxAABB.CreateFromCenterAndHalfExtents(center, extents * 0.5f);
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x00067412 File Offset: 0x00065612
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static MinMaxAABB CreateFromCenterAndHalfExtents(float3 center, float3 halfExtents)
		{
			return new MinMaxAABB(center - halfExtents, center + halfExtents);
		}

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x060024B3 RID: 9395 RVA: 0x00067427 File Offset: 0x00065627
		public float3 Extents
		{
			get
			{
				return this.Max - this.Min;
			}
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x060024B4 RID: 9396 RVA: 0x0006743A File Offset: 0x0006563A
		public float3 HalfExtents
		{
			get
			{
				return (this.Max - this.Min) * 0.5f;
			}
		}

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x060024B5 RID: 9397 RVA: 0x00067457 File Offset: 0x00065657
		public float3 Center
		{
			get
			{
				return (this.Max + this.Min) * 0.5f;
			}
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x060024B6 RID: 9398 RVA: 0x00067474 File Offset: 0x00065674
		public bool IsValid
		{
			get
			{
				return math.all(this.Min <= this.Max);
			}
		}

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x060024B7 RID: 9399 RVA: 0x0006748C File Offset: 0x0006568C
		public float SurfaceArea
		{
			get
			{
				float3 diff = this.Max - this.Min;
				return 2f * math.dot(diff, diff.yzx);
			}
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x000674BE File Offset: 0x000656BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Contains(float3 point)
		{
			return math.all((point >= this.Min) & (point <= this.Max));
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x000674E2 File Offset: 0x000656E2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Contains(MinMaxAABB aabb)
		{
			return math.all((this.Min <= aabb.Min) & (this.Max >= aabb.Max));
		}

		// Token: 0x060024BA RID: 9402 RVA: 0x00067510 File Offset: 0x00065710
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Overlaps(MinMaxAABB aabb)
		{
			return math.all((this.Max >= aabb.Min) & (this.Min <= aabb.Max));
		}

		// Token: 0x060024BB RID: 9403 RVA: 0x0006753E File Offset: 0x0006573E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Expand(float signedDistance)
		{
			this.Min -= signedDistance;
			this.Max += signedDistance;
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x00067564 File Offset: 0x00065764
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Encapsulate(MinMaxAABB aabb)
		{
			this.Min = math.min(this.Min, aabb.Min);
			this.Max = math.max(this.Max, aabb.Max);
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x00067594 File Offset: 0x00065794
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Encapsulate(float3 point)
		{
			this.Min = math.min(this.Min, point);
			this.Max = math.max(this.Max, point);
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x000675BA File Offset: 0x000657BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(MinMaxAABB other)
		{
			return this.Min.Equals(other.Min) && this.Max.Equals(other.Max);
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x000675E2 File Offset: 0x000657E2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("MinMaxAABB({0}, {1})", this.Min, this.Max);
		}

		// Token: 0x0400017B RID: 379
		public float3 Min;

		// Token: 0x0400017C RID: 380
		public float3 Max;
	}
}
