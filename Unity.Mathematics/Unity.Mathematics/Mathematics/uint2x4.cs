using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000058 RID: 88
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct uint2x4 : IEquatable<uint2x4>, IFormattable
	{
		// Token: 0x06001FF2 RID: 8178 RVA: 0x0005B4F7 File Offset: 0x000596F7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x4(uint2 c0, uint2 c1, uint2 c2, uint2 c3)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
			this.c3 = c3;
		}

		// Token: 0x06001FF3 RID: 8179 RVA: 0x0005B516 File Offset: 0x00059716
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x4(uint m00, uint m01, uint m02, uint m03, uint m10, uint m11, uint m12, uint m13)
		{
			this.c0 = new uint2(m00, m10);
			this.c1 = new uint2(m01, m11);
			this.c2 = new uint2(m02, m12);
			this.c3 = new uint2(m03, m13);
		}

		// Token: 0x06001FF4 RID: 8180 RVA: 0x0005B551 File Offset: 0x00059751
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x4(uint v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x06001FF5 RID: 8181 RVA: 0x0005B584 File Offset: 0x00059784
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x4(bool v)
		{
			this.c0 = math.select(new uint2(0U), new uint2(1U), v);
			this.c1 = math.select(new uint2(0U), new uint2(1U), v);
			this.c2 = math.select(new uint2(0U), new uint2(1U), v);
			this.c3 = math.select(new uint2(0U), new uint2(1U), v);
		}

		// Token: 0x06001FF6 RID: 8182 RVA: 0x0005B5F4 File Offset: 0x000597F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x4(bool2x4 v)
		{
			this.c0 = math.select(new uint2(0U), new uint2(1U), v.c0);
			this.c1 = math.select(new uint2(0U), new uint2(1U), v.c1);
			this.c2 = math.select(new uint2(0U), new uint2(1U), v.c2);
			this.c3 = math.select(new uint2(0U), new uint2(1U), v.c3);
		}

		// Token: 0x06001FF7 RID: 8183 RVA: 0x0005B675 File Offset: 0x00059875
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x4(int v)
		{
			this.c0 = (uint2)v;
			this.c1 = (uint2)v;
			this.c2 = (uint2)v;
			this.c3 = (uint2)v;
		}

		// Token: 0x06001FF8 RID: 8184 RVA: 0x0005B6A8 File Offset: 0x000598A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x4(int2x4 v)
		{
			this.c0 = (uint2)v.c0;
			this.c1 = (uint2)v.c1;
			this.c2 = (uint2)v.c2;
			this.c3 = (uint2)v.c3;
		}

		// Token: 0x06001FF9 RID: 8185 RVA: 0x0005B6F9 File Offset: 0x000598F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x4(float v)
		{
			this.c0 = (uint2)v;
			this.c1 = (uint2)v;
			this.c2 = (uint2)v;
			this.c3 = (uint2)v;
		}

		// Token: 0x06001FFA RID: 8186 RVA: 0x0005B72C File Offset: 0x0005992C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x4(float2x4 v)
		{
			this.c0 = (uint2)v.c0;
			this.c1 = (uint2)v.c1;
			this.c2 = (uint2)v.c2;
			this.c3 = (uint2)v.c3;
		}

		// Token: 0x06001FFB RID: 8187 RVA: 0x0005B77D File Offset: 0x0005997D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x4(double v)
		{
			this.c0 = (uint2)v;
			this.c1 = (uint2)v;
			this.c2 = (uint2)v;
			this.c3 = (uint2)v;
		}

		// Token: 0x06001FFC RID: 8188 RVA: 0x0005B7B0 File Offset: 0x000599B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x4(double2x4 v)
		{
			this.c0 = (uint2)v.c0;
			this.c1 = (uint2)v.c1;
			this.c2 = (uint2)v.c2;
			this.c3 = (uint2)v.c3;
		}

		// Token: 0x06001FFD RID: 8189 RVA: 0x00020275 File Offset: 0x0001E475
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator uint2x4(uint v)
		{
			return new uint2x4(v);
		}

		// Token: 0x06001FFE RID: 8190 RVA: 0x0002027D File Offset: 0x0001E47D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x4(bool v)
		{
			return new uint2x4(v);
		}

		// Token: 0x06001FFF RID: 8191 RVA: 0x00020285 File Offset: 0x0001E485
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x4(bool2x4 v)
		{
			return new uint2x4(v);
		}

		// Token: 0x06002000 RID: 8192 RVA: 0x0002028D File Offset: 0x0001E48D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x4(int v)
		{
			return new uint2x4(v);
		}

		// Token: 0x06002001 RID: 8193 RVA: 0x00020295 File Offset: 0x0001E495
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x4(int2x4 v)
		{
			return new uint2x4(v);
		}

		// Token: 0x06002002 RID: 8194 RVA: 0x0002029D File Offset: 0x0001E49D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x4(float v)
		{
			return new uint2x4(v);
		}

		// Token: 0x06002003 RID: 8195 RVA: 0x000202A5 File Offset: 0x0001E4A5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x4(float2x4 v)
		{
			return new uint2x4(v);
		}

		// Token: 0x06002004 RID: 8196 RVA: 0x000202AD File Offset: 0x0001E4AD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x4(double v)
		{
			return new uint2x4(v);
		}

		// Token: 0x06002005 RID: 8197 RVA: 0x000202B5 File Offset: 0x0001E4B5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x4(double2x4 v)
		{
			return new uint2x4(v);
		}

		// Token: 0x06002006 RID: 8198 RVA: 0x0005B804 File Offset: 0x00059A04
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator *(uint2x4 lhs, uint2x4 rhs)
		{
			return new uint2x4(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2, lhs.c3 * rhs.c3);
		}

		// Token: 0x06002007 RID: 8199 RVA: 0x0005B85A File Offset: 0x00059A5A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator *(uint2x4 lhs, uint rhs)
		{
			return new uint2x4(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs, lhs.c3 * rhs);
		}

		// Token: 0x06002008 RID: 8200 RVA: 0x0005B891 File Offset: 0x00059A91
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator *(uint lhs, uint2x4 rhs)
		{
			return new uint2x4(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2, lhs * rhs.c3);
		}

		// Token: 0x06002009 RID: 8201 RVA: 0x0005B8C8 File Offset: 0x00059AC8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator +(uint2x4 lhs, uint2x4 rhs)
		{
			return new uint2x4(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2, lhs.c3 + rhs.c3);
		}

		// Token: 0x0600200A RID: 8202 RVA: 0x0005B91E File Offset: 0x00059B1E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator +(uint2x4 lhs, uint rhs)
		{
			return new uint2x4(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs, lhs.c3 + rhs);
		}

		// Token: 0x0600200B RID: 8203 RVA: 0x0005B955 File Offset: 0x00059B55
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator +(uint lhs, uint2x4 rhs)
		{
			return new uint2x4(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2, lhs + rhs.c3);
		}

		// Token: 0x0600200C RID: 8204 RVA: 0x0005B98C File Offset: 0x00059B8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator -(uint2x4 lhs, uint2x4 rhs)
		{
			return new uint2x4(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2, lhs.c3 - rhs.c3);
		}

		// Token: 0x0600200D RID: 8205 RVA: 0x0005B9E2 File Offset: 0x00059BE2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator -(uint2x4 lhs, uint rhs)
		{
			return new uint2x4(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs, lhs.c3 - rhs);
		}

		// Token: 0x0600200E RID: 8206 RVA: 0x0005BA19 File Offset: 0x00059C19
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator -(uint lhs, uint2x4 rhs)
		{
			return new uint2x4(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2, lhs - rhs.c3);
		}

		// Token: 0x0600200F RID: 8207 RVA: 0x0005BA50 File Offset: 0x00059C50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator /(uint2x4 lhs, uint2x4 rhs)
		{
			return new uint2x4(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2, lhs.c3 / rhs.c3);
		}

		// Token: 0x06002010 RID: 8208 RVA: 0x0005BAA6 File Offset: 0x00059CA6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator /(uint2x4 lhs, uint rhs)
		{
			return new uint2x4(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs, lhs.c3 / rhs);
		}

		// Token: 0x06002011 RID: 8209 RVA: 0x0005BADD File Offset: 0x00059CDD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator /(uint lhs, uint2x4 rhs)
		{
			return new uint2x4(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2, lhs / rhs.c3);
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x0005BB14 File Offset: 0x00059D14
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator %(uint2x4 lhs, uint2x4 rhs)
		{
			return new uint2x4(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2, lhs.c3 % rhs.c3);
		}

		// Token: 0x06002013 RID: 8211 RVA: 0x0005BB6A File Offset: 0x00059D6A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator %(uint2x4 lhs, uint rhs)
		{
			return new uint2x4(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs, lhs.c3 % rhs);
		}

		// Token: 0x06002014 RID: 8212 RVA: 0x0005BBA1 File Offset: 0x00059DA1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator %(uint lhs, uint2x4 rhs)
		{
			return new uint2x4(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2, lhs % rhs.c3);
		}

		// Token: 0x06002015 RID: 8213 RVA: 0x0005BBD8 File Offset: 0x00059DD8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator ++(uint2x4 val)
		{
			uint2 @uint = uint2.op_Increment(val.c0);
			val.c0 = @uint;
			uint2 uint2 = @uint;
			@uint = uint2.op_Increment(val.c1);
			val.c1 = @uint;
			uint2 uint3 = @uint;
			@uint = uint2.op_Increment(val.c2);
			val.c2 = @uint;
			uint2 uint4 = @uint;
			@uint = uint2.op_Increment(val.c3);
			val.c3 = @uint;
			return new uint2x4(uint2, uint3, uint4, @uint);
		}

		// Token: 0x06002016 RID: 8214 RVA: 0x0005BC54 File Offset: 0x00059E54
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator --(uint2x4 val)
		{
			uint2 @uint = uint2.op_Decrement(val.c0);
			val.c0 = @uint;
			uint2 uint2 = @uint;
			@uint = uint2.op_Decrement(val.c1);
			val.c1 = @uint;
			uint2 uint3 = @uint;
			@uint = uint2.op_Decrement(val.c2);
			val.c2 = @uint;
			uint2 uint4 = @uint;
			@uint = uint2.op_Decrement(val.c3);
			val.c3 = @uint;
			return new uint2x4(uint2, uint3, uint4, @uint);
		}

		// Token: 0x06002017 RID: 8215 RVA: 0x0005BCD0 File Offset: 0x00059ED0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator <(uint2x4 lhs, uint2x4 rhs)
		{
			return new bool2x4(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2, lhs.c3 < rhs.c3);
		}

		// Token: 0x06002018 RID: 8216 RVA: 0x0005BD26 File Offset: 0x00059F26
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator <(uint2x4 lhs, uint rhs)
		{
			return new bool2x4(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs, lhs.c3 < rhs);
		}

		// Token: 0x06002019 RID: 8217 RVA: 0x0005BD5D File Offset: 0x00059F5D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator <(uint lhs, uint2x4 rhs)
		{
			return new bool2x4(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2, lhs < rhs.c3);
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x0005BD94 File Offset: 0x00059F94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator <=(uint2x4 lhs, uint2x4 rhs)
		{
			return new bool2x4(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2, lhs.c3 <= rhs.c3);
		}

		// Token: 0x0600201B RID: 8219 RVA: 0x0005BDEA File Offset: 0x00059FEA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator <=(uint2x4 lhs, uint rhs)
		{
			return new bool2x4(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs, lhs.c3 <= rhs);
		}

		// Token: 0x0600201C RID: 8220 RVA: 0x0005BE21 File Offset: 0x0005A021
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator <=(uint lhs, uint2x4 rhs)
		{
			return new bool2x4(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2, lhs <= rhs.c3);
		}

		// Token: 0x0600201D RID: 8221 RVA: 0x0005BE58 File Offset: 0x0005A058
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator >(uint2x4 lhs, uint2x4 rhs)
		{
			return new bool2x4(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2, lhs.c3 > rhs.c3);
		}

		// Token: 0x0600201E RID: 8222 RVA: 0x0005BEAE File Offset: 0x0005A0AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator >(uint2x4 lhs, uint rhs)
		{
			return new bool2x4(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs, lhs.c3 > rhs);
		}

		// Token: 0x0600201F RID: 8223 RVA: 0x0005BEE5 File Offset: 0x0005A0E5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator >(uint lhs, uint2x4 rhs)
		{
			return new bool2x4(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2, lhs > rhs.c3);
		}

		// Token: 0x06002020 RID: 8224 RVA: 0x0005BF1C File Offset: 0x0005A11C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator >=(uint2x4 lhs, uint2x4 rhs)
		{
			return new bool2x4(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2, lhs.c3 >= rhs.c3);
		}

		// Token: 0x06002021 RID: 8225 RVA: 0x0005BF72 File Offset: 0x0005A172
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator >=(uint2x4 lhs, uint rhs)
		{
			return new bool2x4(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs, lhs.c3 >= rhs);
		}

		// Token: 0x06002022 RID: 8226 RVA: 0x0005BFA9 File Offset: 0x0005A1A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator >=(uint lhs, uint2x4 rhs)
		{
			return new bool2x4(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2, lhs >= rhs.c3);
		}

		// Token: 0x06002023 RID: 8227 RVA: 0x0005BFE0 File Offset: 0x0005A1E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator -(uint2x4 val)
		{
			return new uint2x4(-val.c0, -val.c1, -val.c2, -val.c3);
		}

		// Token: 0x06002024 RID: 8228 RVA: 0x0005C013 File Offset: 0x0005A213
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator +(uint2x4 val)
		{
			return new uint2x4(+val.c0, +val.c1, +val.c2, +val.c3);
		}

		// Token: 0x06002025 RID: 8229 RVA: 0x0005C046 File Offset: 0x0005A246
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator <<(uint2x4 x, int n)
		{
			return new uint2x4(x.c0 << n, x.c1 << n, x.c2 << n, x.c3 << n);
		}

		// Token: 0x06002026 RID: 8230 RVA: 0x0005C07D File Offset: 0x0005A27D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator >>(uint2x4 x, int n)
		{
			return new uint2x4(x.c0 >> n, x.c1 >> n, x.c2 >> n, x.c3 >> n);
		}

		// Token: 0x06002027 RID: 8231 RVA: 0x0005C0B4 File Offset: 0x0005A2B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator ==(uint2x4 lhs, uint2x4 rhs)
		{
			return new bool2x4(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2, lhs.c3 == rhs.c3);
		}

		// Token: 0x06002028 RID: 8232 RVA: 0x0005C10A File Offset: 0x0005A30A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator ==(uint2x4 lhs, uint rhs)
		{
			return new bool2x4(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs, lhs.c3 == rhs);
		}

		// Token: 0x06002029 RID: 8233 RVA: 0x0005C141 File Offset: 0x0005A341
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator ==(uint lhs, uint2x4 rhs)
		{
			return new bool2x4(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2, lhs == rhs.c3);
		}

		// Token: 0x0600202A RID: 8234 RVA: 0x0005C178 File Offset: 0x0005A378
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator !=(uint2x4 lhs, uint2x4 rhs)
		{
			return new bool2x4(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2, lhs.c3 != rhs.c3);
		}

		// Token: 0x0600202B RID: 8235 RVA: 0x0005C1CE File Offset: 0x0005A3CE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator !=(uint2x4 lhs, uint rhs)
		{
			return new bool2x4(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs, lhs.c3 != rhs);
		}

		// Token: 0x0600202C RID: 8236 RVA: 0x0005C205 File Offset: 0x0005A405
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator !=(uint lhs, uint2x4 rhs)
		{
			return new bool2x4(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2, lhs != rhs.c3);
		}

		// Token: 0x0600202D RID: 8237 RVA: 0x0005C23C File Offset: 0x0005A43C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator ~(uint2x4 val)
		{
			return new uint2x4(~val.c0, ~val.c1, ~val.c2, ~val.c3);
		}

		// Token: 0x0600202E RID: 8238 RVA: 0x0005C270 File Offset: 0x0005A470
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator &(uint2x4 lhs, uint2x4 rhs)
		{
			return new uint2x4(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1, lhs.c2 & rhs.c2, lhs.c3 & rhs.c3);
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x0005C2C6 File Offset: 0x0005A4C6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator &(uint2x4 lhs, uint rhs)
		{
			return new uint2x4(lhs.c0 & rhs, lhs.c1 & rhs, lhs.c2 & rhs, lhs.c3 & rhs);
		}

		// Token: 0x06002030 RID: 8240 RVA: 0x0005C2FD File Offset: 0x0005A4FD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator &(uint lhs, uint2x4 rhs)
		{
			return new uint2x4(lhs & rhs.c0, lhs & rhs.c1, lhs & rhs.c2, lhs & rhs.c3);
		}

		// Token: 0x06002031 RID: 8241 RVA: 0x0005C334 File Offset: 0x0005A534
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator |(uint2x4 lhs, uint2x4 rhs)
		{
			return new uint2x4(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1, lhs.c2 | rhs.c2, lhs.c3 | rhs.c3);
		}

		// Token: 0x06002032 RID: 8242 RVA: 0x0005C38A File Offset: 0x0005A58A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator |(uint2x4 lhs, uint rhs)
		{
			return new uint2x4(lhs.c0 | rhs, lhs.c1 | rhs, lhs.c2 | rhs, lhs.c3 | rhs);
		}

		// Token: 0x06002033 RID: 8243 RVA: 0x0005C3C1 File Offset: 0x0005A5C1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator |(uint lhs, uint2x4 rhs)
		{
			return new uint2x4(lhs | rhs.c0, lhs | rhs.c1, lhs | rhs.c2, lhs | rhs.c3);
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x0005C3F8 File Offset: 0x0005A5F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator ^(uint2x4 lhs, uint2x4 rhs)
		{
			return new uint2x4(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1, lhs.c2 ^ rhs.c2, lhs.c3 ^ rhs.c3);
		}

		// Token: 0x06002035 RID: 8245 RVA: 0x0005C44E File Offset: 0x0005A64E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator ^(uint2x4 lhs, uint rhs)
		{
			return new uint2x4(lhs.c0 ^ rhs, lhs.c1 ^ rhs, lhs.c2 ^ rhs, lhs.c3 ^ rhs);
		}

		// Token: 0x06002036 RID: 8246 RVA: 0x0005C485 File Offset: 0x0005A685
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 operator ^(uint lhs, uint2x4 rhs)
		{
			return new uint2x4(lhs ^ rhs.c0, lhs ^ rhs.c1, lhs ^ rhs.c2, lhs ^ rhs.c3);
		}

		// Token: 0x170009BC RID: 2492
		public unsafe ref uint2 this[int index]
		{
			get
			{
				fixed (uint2x4* ptr = &this)
				{
					return ref *(uint2*)(ptr + (IntPtr)index * (IntPtr)sizeof(uint2) / (IntPtr)sizeof(uint2x4));
				}
			}
		}

		// Token: 0x06002038 RID: 8248 RVA: 0x0005C4D8 File Offset: 0x0005A6D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(uint2x4 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2) && this.c3.Equals(rhs.c3);
		}

		// Token: 0x06002039 RID: 8249 RVA: 0x0005C534 File Offset: 0x0005A734
		public override bool Equals(object o)
		{
			if (o is uint2x4)
			{
				uint2x4 converted = (uint2x4)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x0600203A RID: 8250 RVA: 0x0005C559 File Offset: 0x0005A759
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x0600203B RID: 8251 RVA: 0x0005C568 File Offset: 0x0005A768
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("uint2x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c2.x,
				this.c3.x,
				this.c0.y,
				this.c1.y,
				this.c2.y,
				this.c3.y
			});
		}

		// Token: 0x0600203C RID: 8252 RVA: 0x0005C620 File Offset: 0x0005A820
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("uint2x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7})", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c2.x.ToString(format, formatProvider),
				this.c3.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c2.y.ToString(format, formatProvider),
				this.c3.y.ToString(format, formatProvider)
			});
		}

		// Token: 0x0400014C RID: 332
		public uint2 c0;

		// Token: 0x0400014D RID: 333
		public uint2 c1;

		// Token: 0x0400014E RID: 334
		public uint2 c2;

		// Token: 0x0400014F RID: 335
		public uint2 c3;

		// Token: 0x04000150 RID: 336
		public static readonly uint2x4 zero;
	}
}
