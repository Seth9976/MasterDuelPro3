using System;
using System.Runtime.CompilerServices;

namespace Unity.Mathematics.Geometry
{
	// Token: 0x02000064 RID: 100
	public static class Math
	{
		// Token: 0x060024C0 RID: 9408 RVA: 0x00067604 File Offset: 0x00065804
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static MinMaxAABB Transform(RigidTransform transform, MinMaxAABB aabb)
		{
			float3 halfExtentsInA = aabb.HalfExtents;
			float3 @float = math.rotate(transform.rot, new float3(halfExtentsInA.x, 0f, 0f));
			float3 y = math.rotate(transform.rot, new float3(0f, halfExtentsInA.y, 0f));
			float3 z = math.rotate(transform.rot, new float3(0f, 0f, halfExtentsInA.z));
			float3 halfExtentsInB = math.abs(@float) + math.abs(y) + math.abs(z);
			float3 centerInB = math.transform(transform, aabb.Center);
			return new MinMaxAABB(centerInB - halfExtentsInB, centerInB + halfExtentsInB);
		}

		// Token: 0x060024C1 RID: 9409 RVA: 0x000676BC File Offset: 0x000658BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static MinMaxAABB Transform(float4x4 transform, MinMaxAABB aabb)
		{
			MinMaxAABB transformed = Math.Transform(new float3x3(transform), aabb);
			transformed.Min += transform.c3.xyz;
			transformed.Max += transform.c3.xyz;
			return transformed;
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x00067720 File Offset: 0x00065920
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static MinMaxAABB Transform(float3x3 transform, MinMaxAABB aabb)
		{
			float3 t = transform.c0.xyz * aabb.Min.xxx;
			float3 t2 = transform.c0.xyz * aabb.Max.xxx;
			bool3 minMask = t < t2;
			MinMaxAABB transformed = new MinMaxAABB(math.select(t2, t, minMask), math.select(t2, t, !minMask));
			t = transform.c1.xyz * aabb.Min.yyy;
			t2 = transform.c1.xyz * aabb.Max.yyy;
			minMask = t < t2;
			transformed.Min += math.select(t2, t, minMask);
			transformed.Max += math.select(t2, t, !minMask);
			t = transform.c2.xyz * aabb.Min.zzz;
			t2 = transform.c2.xyz * aabb.Max.zzz;
			minMask = t < t2;
			transformed.Min += math.select(t2, t, minMask);
			transformed.Max += math.select(t2, t, !minMask);
			return transformed;
		}
	}
}
