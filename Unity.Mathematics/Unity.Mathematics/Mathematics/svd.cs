using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000053 RID: 83
	[Il2CppEagerStaticClassConstruction]
	public static class svd
	{
		// Token: 0x06001EE4 RID: 7908 RVA: 0x00058D4C File Offset: 0x00056F4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void condSwap(bool c, ref float x, ref float y)
		{
			float tmp = x;
			x = math.select(x, y, c);
			y = math.select(y, tmp, c);
		}

		// Token: 0x06001EE5 RID: 7909 RVA: 0x00058D74 File Offset: 0x00056F74
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void condNegSwap(bool c, ref float3 x, ref float3 y)
		{
			float3 tmp = -x;
			x = math.select(x, y, c);
			y = math.select(y, tmp, c);
		}

		// Token: 0x06001EE6 RID: 7910 RVA: 0x00058DB8 File Offset: 0x00056FB8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static quaternion condNegSwapQuat(bool c, quaternion q, float4 mask)
		{
			return math.mul(q, math.select(quaternion.identity.value, mask * 0.70710677f, c));
		}

		// Token: 0x06001EE7 RID: 7911 RVA: 0x00058DE0 File Offset: 0x00056FE0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void sortSingularValues(ref float3x3 b, ref quaternion v)
		{
			float l0 = math.lengthsq(b.c0);
			float l = math.lengthsq(b.c1);
			float l2 = math.lengthsq(b.c2);
			bool c = l0 < l;
			svd.condNegSwap(c, ref b.c0, ref b.c1);
			v = svd.condNegSwapQuat(c, v, math.float4(0f, 0f, 1f, 1f));
			svd.condSwap(c, ref l0, ref l);
			c = l0 < l2;
			svd.condNegSwap(c, ref b.c0, ref b.c2);
			v = svd.condNegSwapQuat(c, v, math.float4(0f, -1f, 0f, 1f));
			svd.condSwap(c, ref l0, ref l2);
			c = l < l2;
			svd.condNegSwap(c, ref b.c1, ref b.c2);
			v = svd.condNegSwapQuat(c, v, math.float4(1f, 0f, 0f, 1f));
		}

		// Token: 0x06001EE8 RID: 7912 RVA: 0x00058EEC File Offset: 0x000570EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static quaternion approxGivensQuat(float3 pq, float4 mask)
		{
			float ch = 2f * (pq.x - pq.y);
			float sh = pq.z;
			return math.normalize(math.select(math.float4(0.38268343f, 0.38268343f, 0.38268343f, 0.9238795f), math.float4(sh, sh, sh, ch), 5.8284273f * sh * sh < ch * ch) * mask);
		}

		// Token: 0x06001EE9 RID: 7913 RVA: 0x00058F5C File Offset: 0x0005715C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static quaternion qrGivensQuat(float2 pq, float4 mask)
		{
			float i = math.sqrt(pq.x * pq.x + pq.y * pq.y);
			float sh = math.select(0f, pq.y, i > 1E-15f);
			float ch = math.abs(pq.x) + math.max(i, 1E-15f);
			svd.condSwap(pq.x < 0f, ref sh, ref ch);
			return math.normalize(math.float4(sh, sh, sh, ch) * mask);
		}

		// Token: 0x06001EEA RID: 7914 RVA: 0x00058FEC File Offset: 0x000571EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static quaternion givensQRFactorization(float3x3 b, out float3x3 r)
		{
			quaternion quaternion = svd.qrGivensQuat(math.float2(b.c0.x, b.c0.y), math.float4(0f, 0f, 1f, 1f));
			float3x3 qmt = math.float3x3(math.conjugate(quaternion));
			r = math.mul(qmt, b);
			quaternion q = svd.qrGivensQuat(math.float2(r.c0.x, r.c0.z), math.float4(0f, -1f, 0f, 1f));
			quaternion quaternion2 = math.mul(quaternion, q);
			qmt = math.float3x3(math.conjugate(q));
			r = math.mul(qmt, r);
			q = svd.qrGivensQuat(math.float2(r.c1.y, r.c1.z), math.float4(1f, 0f, 0f, 1f));
			quaternion quaternion3 = math.mul(quaternion2, q);
			qmt = math.float3x3(math.conjugate(q));
			r = math.mul(qmt, r);
			return quaternion3;
		}

		// Token: 0x06001EEB RID: 7915 RVA: 0x00059108 File Offset: 0x00057308
		private static quaternion jacobiIteration(ref float3x3 s, int iterations = 5)
		{
			quaternion v = quaternion.identity;
			for (int i = 0; i < iterations; i++)
			{
				quaternion q = svd.approxGivensQuat(math.float3(s.c0.x, s.c1.y, s.c0.y), math.float4(0f, 0f, 1f, 1f));
				v = math.mul(v, q);
				float3x3 qm = math.float3x3(q);
				s = math.mul(math.mul(math.transpose(qm), s), qm);
				q = svd.approxGivensQuat(math.float3(s.c1.y, s.c2.z, s.c1.z), math.float4(1f, 0f, 0f, 1f));
				v = math.mul(v, q);
				qm = math.float3x3(q);
				s = math.mul(math.mul(math.transpose(qm), s), qm);
				q = svd.approxGivensQuat(math.float3(s.c2.z, s.c0.x, s.c2.x), math.float4(0f, 1f, 0f, 1f));
				v = math.mul(v, q);
				qm = math.float3x3(q);
				s = math.mul(math.mul(math.transpose(qm), s), qm);
			}
			return v;
		}

		// Token: 0x06001EEC RID: 7916 RVA: 0x00059284 File Offset: 0x00057484
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static float3 singularValuesDecomposition(float3x3 a, out quaternion u, out quaternion v)
		{
			u = quaternion.identity;
			v = quaternion.identity;
			float3x3 s = math.mul(math.transpose(a), a);
			v = svd.jacobiIteration(ref s, 5);
			float3x3 b = math.float3x3(v);
			b = math.mul(a, b);
			svd.sortSingularValues(ref b, ref v);
			float3x3 e;
			u = svd.givensQRFactorization(b, out e);
			return math.float3(e.c0.x, e.c1.y, e.c2.z);
		}

		// Token: 0x06001EED RID: 7917 RVA: 0x00059312 File Offset: 0x00057512
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static float3 rcpsafe(float3 x, float epsilon = 1E-09f)
		{
			return math.select(math.rcp(x), float3.zero, math.abs(x) < epsilon);
		}

		// Token: 0x06001EEE RID: 7918 RVA: 0x00059330 File Offset: 0x00057530
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 svdInverse(float3x3 a)
		{
			quaternion u;
			quaternion v;
			float3 e = svd.singularValuesDecomposition(a, out u, out v);
			float3x3 um = math.float3x3(u);
			return math.mul(math.float3x3(v), math.scaleMul(svd.rcpsafe(e, 1E-06f), math.transpose(um)));
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x00059370 File Offset: 0x00057570
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion svdRotation(float3x3 a)
		{
			quaternion u;
			quaternion v;
			svd.singularValuesDecomposition(a, out u, out v);
			return math.mul(u, math.conjugate(v));
		}

		// Token: 0x0400013B RID: 315
		public const float k_EpsilonDeterminant = 1E-06f;

		// Token: 0x0400013C RID: 316
		public const float k_EpsilonRCP = 1E-09f;

		// Token: 0x0400013D RID: 317
		public const float k_EpsilonNormalSqrt = 1E-15f;

		// Token: 0x0400013E RID: 318
		public const float k_EpsilonNormal = 1E-30f;
	}
}
