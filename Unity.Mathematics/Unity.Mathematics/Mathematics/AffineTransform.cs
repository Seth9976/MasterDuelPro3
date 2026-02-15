using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000008 RID: 8
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct AffineTransform : IEquatable<AffineTransform>, IFormattable
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00007BAD File Offset: 0x00005DAD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public AffineTransform(float3 translation, quaternion rotation)
		{
			this.rs = math.float3x3(rotation);
			this.t = translation;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00007BC2 File Offset: 0x00005DC2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public AffineTransform(float3 translation, quaternion rotation, float3 scale)
		{
			this.rs = math.mulScale(math.float3x3(rotation), scale);
			this.t = translation;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00007BDD File Offset: 0x00005DDD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public AffineTransform(float3 translation, float3x3 rotationScale)
		{
			this.rs = rotationScale;
			this.t = translation;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00007BED File Offset: 0x00005DED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public AffineTransform(float3x3 rotationScale)
		{
			this.rs = rotationScale;
			this.t = float3.zero;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00007C01 File Offset: 0x00005E01
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public AffineTransform(RigidTransform rigid)
		{
			this.rs = math.float3x3(rigid.rot);
			this.t = rigid.pos;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00007C20 File Offset: 0x00005E20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public AffineTransform(float3x4 m)
		{
			this.rs = math.float3x3(m.c0, m.c1, m.c2);
			this.t = m.c3;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00007C4C File Offset: 0x00005E4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public AffineTransform(float4x4 m)
		{
			this.rs = math.float3x3(m.c0.xyz, m.c1.xyz, m.c2.xyz);
			this.t = m.c3.xyz;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00007C9A File Offset: 0x00005E9A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3x4(AffineTransform m)
		{
			return math.float3x4(m.rs.c0, m.rs.c1, m.rs.c2, m.t);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00007CC8 File Offset: 0x00005EC8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4x4(AffineTransform m)
		{
			return math.float4x4(math.float4(m.rs.c0, 0f), math.float4(m.rs.c1, 0f), math.float4(m.rs.c2, 0f), math.float4(m.t, 1f));
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00007D29 File Offset: 0x00005F29
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(AffineTransform rhs)
		{
			return this.rs.Equals(rhs.rs) && this.t.Equals(rhs.t);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00007D54 File Offset: 0x00005F54
		public override bool Equals(object o)
		{
			if (o is AffineTransform)
			{
				AffineTransform converted = (AffineTransform)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00007D79 File Offset: 0x00005F79
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00007D88 File Offset: 0x00005F88
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("AffineTransform(({0}f, {1}f, {2}f,  {3}f, {4}f, {5}f,  {6}f, {7}f, {8}f), ({9}f, {10}f, {11}f))", new object[]
			{
				this.rs.c0.x,
				this.rs.c1.x,
				this.rs.c2.x,
				this.rs.c0.y,
				this.rs.c1.y,
				this.rs.c2.y,
				this.rs.c0.z,
				this.rs.c1.z,
				this.rs.c2.z,
				this.t.x,
				this.t.y,
				this.t.z
			});
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00007EBC File Offset: 0x000060BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("AffineTransform(({0}f, {1}f, {2}f,  {3}f, {4}f, {5}f,  {6}f, {7}f, {8}f), ({9}f, {10}f, {11}f))", new object[]
			{
				this.rs.c0.x.ToString(format, formatProvider),
				this.rs.c1.x.ToString(format, formatProvider),
				this.rs.c2.x.ToString(format, formatProvider),
				this.rs.c0.y.ToString(format, formatProvider),
				this.rs.c1.y.ToString(format, formatProvider),
				this.rs.c2.y.ToString(format, formatProvider),
				this.rs.c0.z.ToString(format, formatProvider),
				this.rs.c1.z.ToString(format, formatProvider),
				this.rs.c2.z.ToString(format, formatProvider),
				this.t.x.ToString(format, formatProvider),
				this.t.y.ToString(format, formatProvider),
				this.t.z.ToString(format, formatProvider)
			});
		}

		// Token: 0x04000006 RID: 6
		public float3x3 rs;

		// Token: 0x04000007 RID: 7
		public float3 t;

		// Token: 0x04000008 RID: 8
		public static readonly AffineTransform identity = new AffineTransform(float3.zero, float3x3.identity);

		// Token: 0x04000009 RID: 9
		public static readonly AffineTransform zero;
	}
}
