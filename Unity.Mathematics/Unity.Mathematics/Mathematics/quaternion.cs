using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Unity.Mathematics
{
	// Token: 0x02000050 RID: 80
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct quaternion : IEquatable<quaternion>, IFormattable
	{
		// Token: 0x06001E63 RID: 7779 RVA: 0x00057193 File Offset: 0x00055393
		public static implicit operator Quaternion(quaternion q)
		{
			return new Quaternion(q.value.x, q.value.y, q.value.z, q.value.w);
		}

		// Token: 0x06001E64 RID: 7780 RVA: 0x000571C6 File Offset: 0x000553C6
		public static implicit operator quaternion(Quaternion q)
		{
			return new quaternion(q.x, q.y, q.z, q.w);
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x000571E5 File Offset: 0x000553E5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public quaternion(float x, float y, float z, float w)
		{
			this.value.x = x;
			this.value.y = y;
			this.value.z = z;
			this.value.w = w;
		}

		// Token: 0x06001E66 RID: 7782 RVA: 0x00057218 File Offset: 0x00055418
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public quaternion(float4 value)
		{
			this.value = value;
		}

		// Token: 0x06001E67 RID: 7783 RVA: 0x0001F59D File Offset: 0x0001D79D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator quaternion(float4 v)
		{
			return new quaternion(v);
		}

		// Token: 0x06001E68 RID: 7784 RVA: 0x00057224 File Offset: 0x00055424
		public quaternion(float3x3 m)
		{
			float3 u = m.c0;
			float3 v = m.c1;
			float3 w = m.c2;
			uint u_sign = math.asuint(u.x) & 2147483648U;
			float t = v.y + math.asfloat(math.asuint(w.z) ^ u_sign);
			uint4 u_mask = math.uint4((int)u_sign >> 31);
			uint4 t_mask = math.uint4(math.asint(t) >> 31);
			float tr = 1f + math.abs(u.x);
			uint4 sign_flips = math.uint4(0U, 2147483648U, 2147483648U, 2147483648U) ^ (u_mask & math.uint4(0U, 2147483648U, 0U, 2147483648U)) ^ (t_mask & math.uint4(2147483648U, 2147483648U, 2147483648U, 0U));
			this.value = math.float4(tr, u.y, w.x, v.z) + math.asfloat(math.asuint(math.float4(t, v.x, u.z, w.y)) ^ sign_flips);
			this.value = math.asfloat((math.asuint(this.value) & ~u_mask) | (math.asuint(this.value.zwxy) & u_mask));
			this.value = math.asfloat((math.asuint(this.value.wzyx) & ~t_mask) | (math.asuint(this.value) & t_mask));
			this.value = math.normalize(this.value);
		}

		// Token: 0x06001E69 RID: 7785 RVA: 0x000573D4 File Offset: 0x000555D4
		public quaternion(float4x4 m)
		{
			float4 u = m.c0;
			float4 v = m.c1;
			float4 w = m.c2;
			uint u_sign = math.asuint(u.x) & 2147483648U;
			float t = v.y + math.asfloat(math.asuint(w.z) ^ u_sign);
			uint4 u_mask = math.uint4((int)u_sign >> 31);
			uint4 t_mask = math.uint4(math.asint(t) >> 31);
			float tr = 1f + math.abs(u.x);
			uint4 sign_flips = math.uint4(0U, 2147483648U, 2147483648U, 2147483648U) ^ (u_mask & math.uint4(0U, 2147483648U, 0U, 2147483648U)) ^ (t_mask & math.uint4(2147483648U, 2147483648U, 2147483648U, 0U));
			this.value = math.float4(tr, u.y, w.x, v.z) + math.asfloat(math.asuint(math.float4(t, v.x, u.z, w.y)) ^ sign_flips);
			this.value = math.asfloat((math.asuint(this.value) & ~u_mask) | (math.asuint(this.value.zwxy) & u_mask));
			this.value = math.asfloat((math.asuint(this.value.wzyx) & ~t_mask) | (math.asuint(this.value) & t_mask));
			this.value = math.normalize(this.value);
		}

		// Token: 0x06001E6A RID: 7786 RVA: 0x00057584 File Offset: 0x00055784
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion AxisAngle(float3 axis, float angle)
		{
			float sina;
			float cosa;
			math.sincos(0.5f * angle, out sina, out cosa);
			return math.quaternion(math.float4(axis * sina, cosa));
		}

		// Token: 0x06001E6B RID: 7787 RVA: 0x000575B4 File Offset: 0x000557B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion EulerXYZ(float3 xyz)
		{
			float3 s;
			float3 c;
			math.sincos(0.5f * xyz, out s, out c);
			return math.quaternion(math.float4(s.xyz, c.x) * c.yxxy * c.zzyz + s.yxxy * s.zzyz * math.float4(c.xyz, s.x) * math.float4(-1f, 1f, -1f, 1f));
		}

		// Token: 0x06001E6C RID: 7788 RVA: 0x00057654 File Offset: 0x00055854
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion EulerXZY(float3 xyz)
		{
			float3 s;
			float3 c;
			math.sincos(0.5f * xyz, out s, out c);
			return math.quaternion(math.float4(s.xyz, c.x) * c.yxxy * c.zzyz + s.yxxy * s.zzyz * math.float4(c.xyz, s.x) * math.float4(1f, 1f, -1f, -1f));
		}

		// Token: 0x06001E6D RID: 7789 RVA: 0x000576F4 File Offset: 0x000558F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion EulerYXZ(float3 xyz)
		{
			float3 s;
			float3 c;
			math.sincos(0.5f * xyz, out s, out c);
			return math.quaternion(math.float4(s.xyz, c.x) * c.yxxy * c.zzyz + s.yxxy * s.zzyz * math.float4(c.xyz, s.x) * math.float4(-1f, 1f, 1f, -1f));
		}

		// Token: 0x06001E6E RID: 7790 RVA: 0x00057794 File Offset: 0x00055994
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion EulerYZX(float3 xyz)
		{
			float3 s;
			float3 c;
			math.sincos(0.5f * xyz, out s, out c);
			return math.quaternion(math.float4(s.xyz, c.x) * c.yxxy * c.zzyz + s.yxxy * s.zzyz * math.float4(c.xyz, s.x) * math.float4(-1f, -1f, 1f, 1f));
		}

		// Token: 0x06001E6F RID: 7791 RVA: 0x00057834 File Offset: 0x00055A34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion EulerZXY(float3 xyz)
		{
			float3 s;
			float3 c;
			math.sincos(0.5f * xyz, out s, out c);
			return math.quaternion(math.float4(s.xyz, c.x) * c.yxxy * c.zzyz + s.yxxy * s.zzyz * math.float4(c.xyz, s.x) * math.float4(1f, -1f, -1f, 1f));
		}

		// Token: 0x06001E70 RID: 7792 RVA: 0x000578D4 File Offset: 0x00055AD4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion EulerZYX(float3 xyz)
		{
			float3 s;
			float3 c;
			math.sincos(0.5f * xyz, out s, out c);
			return math.quaternion(math.float4(s.xyz, c.x) * c.yxxy * c.zzyz + s.yxxy * s.zzyz * math.float4(c.xyz, s.x) * math.float4(1f, -1f, 1f, -1f));
		}

		// Token: 0x06001E71 RID: 7793 RVA: 0x00057971 File Offset: 0x00055B71
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion EulerXYZ(float x, float y, float z)
		{
			return quaternion.EulerXYZ(math.float3(x, y, z));
		}

		// Token: 0x06001E72 RID: 7794 RVA: 0x00057980 File Offset: 0x00055B80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion EulerXZY(float x, float y, float z)
		{
			return quaternion.EulerXZY(math.float3(x, y, z));
		}

		// Token: 0x06001E73 RID: 7795 RVA: 0x0005798F File Offset: 0x00055B8F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion EulerYXZ(float x, float y, float z)
		{
			return quaternion.EulerYXZ(math.float3(x, y, z));
		}

		// Token: 0x06001E74 RID: 7796 RVA: 0x0005799E File Offset: 0x00055B9E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion EulerYZX(float x, float y, float z)
		{
			return quaternion.EulerYZX(math.float3(x, y, z));
		}

		// Token: 0x06001E75 RID: 7797 RVA: 0x000579AD File Offset: 0x00055BAD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion EulerZXY(float x, float y, float z)
		{
			return quaternion.EulerZXY(math.float3(x, y, z));
		}

		// Token: 0x06001E76 RID: 7798 RVA: 0x000579BC File Offset: 0x00055BBC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion EulerZYX(float x, float y, float z)
		{
			return quaternion.EulerZYX(math.float3(x, y, z));
		}

		// Token: 0x06001E77 RID: 7799 RVA: 0x000579CC File Offset: 0x00055BCC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion Euler(float3 xyz, math.RotationOrder order = math.RotationOrder.ZXY)
		{
			switch (order)
			{
			case math.RotationOrder.XYZ:
				return quaternion.EulerXYZ(xyz);
			case math.RotationOrder.XZY:
				return quaternion.EulerXZY(xyz);
			case math.RotationOrder.YXZ:
				return quaternion.EulerYXZ(xyz);
			case math.RotationOrder.YZX:
				return quaternion.EulerYZX(xyz);
			case math.RotationOrder.ZXY:
				return quaternion.EulerZXY(xyz);
			case math.RotationOrder.ZYX:
				return quaternion.EulerZYX(xyz);
			default:
				return quaternion.identity;
			}
		}

		// Token: 0x06001E78 RID: 7800 RVA: 0x00057A28 File Offset: 0x00055C28
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion Euler(float x, float y, float z, math.RotationOrder order = math.RotationOrder.ZXY)
		{
			return quaternion.Euler(math.float3(x, y, z), order);
		}

		// Token: 0x06001E79 RID: 7801 RVA: 0x00057A38 File Offset: 0x00055C38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion RotateX(float angle)
		{
			float sina;
			float cosa;
			math.sincos(0.5f * angle, out sina, out cosa);
			return math.quaternion(sina, 0f, 0f, cosa);
		}

		// Token: 0x06001E7A RID: 7802 RVA: 0x00057A68 File Offset: 0x00055C68
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion RotateY(float angle)
		{
			float sina;
			float cosa;
			math.sincos(0.5f * angle, out sina, out cosa);
			return math.quaternion(0f, sina, 0f, cosa);
		}

		// Token: 0x06001E7B RID: 7803 RVA: 0x00057A98 File Offset: 0x00055C98
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion RotateZ(float angle)
		{
			float sina;
			float cosa;
			math.sincos(0.5f * angle, out sina, out cosa);
			return math.quaternion(0f, 0f, sina, cosa);
		}

		// Token: 0x06001E7C RID: 7804 RVA: 0x00057AC8 File Offset: 0x00055CC8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion LookRotation(float3 forward, float3 up)
		{
			float3 t = math.normalize(math.cross(up, forward));
			return math.quaternion(math.float3x3(t, math.cross(forward, t), forward));
		}

		// Token: 0x06001E7D RID: 7805 RVA: 0x00057AF8 File Offset: 0x00055CF8
		public static quaternion LookRotationSafe(float3 forward, float3 up)
		{
			float forwardLengthSq = math.dot(forward, forward);
			float upLengthSq = math.dot(up, up);
			forward *= math.rsqrt(forwardLengthSq);
			up *= math.rsqrt(upLengthSq);
			float3 t = math.cross(up, forward);
			float tLengthSq = math.dot(t, t);
			t *= math.rsqrt(tLengthSq);
			float num = math.min(math.min(forwardLengthSq, upLengthSq), tLengthSq);
			float mx = math.max(math.max(forwardLengthSq, upLengthSq), tLengthSq);
			bool accept = num > 1E-35f && mx < 1E+35f && math.isfinite(forwardLengthSq) && math.isfinite(upLengthSq) && math.isfinite(tLengthSq);
			return math.quaternion(math.select(math.float4(0f, 0f, 0f, 1f), math.quaternion(math.float3x3(t, math.cross(forward, t), forward)).value, accept));
		}

		// Token: 0x06001E7E RID: 7806 RVA: 0x00057BD4 File Offset: 0x00055DD4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(quaternion x)
		{
			return this.value.x == x.value.x && this.value.y == x.value.y && this.value.z == x.value.z && this.value.w == x.value.w;
		}

		// Token: 0x06001E7F RID: 7807 RVA: 0x00057C44 File Offset: 0x00055E44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object x)
		{
			if (x is quaternion)
			{
				quaternion converted = (quaternion)x;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06001E80 RID: 7808 RVA: 0x00057C69 File Offset: 0x00055E69
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06001E81 RID: 7809 RVA: 0x00057C78 File Offset: 0x00055E78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("quaternion({0}f, {1}f, {2}f, {3}f)", new object[]
			{
				this.value.x,
				this.value.y,
				this.value.z,
				this.value.w
			});
		}

		// Token: 0x06001E82 RID: 7810 RVA: 0x00057CE4 File Offset: 0x00055EE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("quaternion({0}f, {1}f, {2}f, {3}f)", new object[]
			{
				this.value.x.ToString(format, formatProvider),
				this.value.y.ToString(format, formatProvider),
				this.value.z.ToString(format, formatProvider),
				this.value.w.ToString(format, formatProvider)
			});
		}

		// Token: 0x04000135 RID: 309
		public float4 value;

		// Token: 0x04000136 RID: 310
		public static readonly quaternion identity = new quaternion(0f, 0f, 0f, 1f);
	}
}
