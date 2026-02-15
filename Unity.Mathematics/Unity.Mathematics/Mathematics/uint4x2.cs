using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000060 RID: 96
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct uint4x2 : IEquatable<uint4x2>, IFormattable
	{
		// Token: 0x060023CE RID: 9166 RVA: 0x000647D1 File Offset: 0x000629D1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x2(uint4 c0, uint4 c1)
		{
			this.c0 = c0;
			this.c1 = c1;
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x000647E1 File Offset: 0x000629E1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x2(uint m00, uint m01, uint m10, uint m11, uint m20, uint m21, uint m30, uint m31)
		{
			this.c0 = new uint4(m00, m10, m20, m30);
			this.c1 = new uint4(m01, m11, m21, m31);
		}

		// Token: 0x060023D0 RID: 9168 RVA: 0x00064806 File Offset: 0x00062A06
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x2(uint v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x060023D1 RID: 9169 RVA: 0x00064820 File Offset: 0x00062A20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x2(bool v)
		{
			this.c0 = math.select(new uint4(0U), new uint4(1U), v);
			this.c1 = math.select(new uint4(0U), new uint4(1U), v);
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x00064852 File Offset: 0x00062A52
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x2(bool4x2 v)
		{
			this.c0 = math.select(new uint4(0U), new uint4(1U), v.c0);
			this.c1 = math.select(new uint4(0U), new uint4(1U), v.c1);
		}

		// Token: 0x060023D3 RID: 9171 RVA: 0x0006488E File Offset: 0x00062A8E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x2(int v)
		{
			this.c0 = (uint4)v;
			this.c1 = (uint4)v;
		}

		// Token: 0x060023D4 RID: 9172 RVA: 0x000648A8 File Offset: 0x00062AA8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x2(int4x2 v)
		{
			this.c0 = (uint4)v.c0;
			this.c1 = (uint4)v.c1;
		}

		// Token: 0x060023D5 RID: 9173 RVA: 0x000648CC File Offset: 0x00062ACC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x2(float v)
		{
			this.c0 = (uint4)v;
			this.c1 = (uint4)v;
		}

		// Token: 0x060023D6 RID: 9174 RVA: 0x000648E6 File Offset: 0x00062AE6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x2(float4x2 v)
		{
			this.c0 = (uint4)v.c0;
			this.c1 = (uint4)v.c1;
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x0006490A File Offset: 0x00062B0A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x2(double v)
		{
			this.c0 = (uint4)v;
			this.c1 = (uint4)v;
		}

		// Token: 0x060023D8 RID: 9176 RVA: 0x00064924 File Offset: 0x00062B24
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x2(double4x2 v)
		{
			this.c0 = (uint4)v.c0;
			this.c1 = (uint4)v.c1;
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x00020D91 File Offset: 0x0001EF91
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator uint4x2(uint v)
		{
			return new uint4x2(v);
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x00020D99 File Offset: 0x0001EF99
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x2(bool v)
		{
			return new uint4x2(v);
		}

		// Token: 0x060023DB RID: 9179 RVA: 0x00020DA1 File Offset: 0x0001EFA1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x2(bool4x2 v)
		{
			return new uint4x2(v);
		}

		// Token: 0x060023DC RID: 9180 RVA: 0x00020DA9 File Offset: 0x0001EFA9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x2(int v)
		{
			return new uint4x2(v);
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x00020DB1 File Offset: 0x0001EFB1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x2(int4x2 v)
		{
			return new uint4x2(v);
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x00020DB9 File Offset: 0x0001EFB9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x2(float v)
		{
			return new uint4x2(v);
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x00020DC1 File Offset: 0x0001EFC1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x2(float4x2 v)
		{
			return new uint4x2(v);
		}

		// Token: 0x060023E0 RID: 9184 RVA: 0x00020DC9 File Offset: 0x0001EFC9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x2(double v)
		{
			return new uint4x2(v);
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x00020DD1 File Offset: 0x0001EFD1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x2(double4x2 v)
		{
			return new uint4x2(v);
		}

		// Token: 0x060023E2 RID: 9186 RVA: 0x00064948 File Offset: 0x00062B48
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator *(uint4x2 lhs, uint4x2 rhs)
		{
			return new uint4x2(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1);
		}

		// Token: 0x060023E3 RID: 9187 RVA: 0x00064971 File Offset: 0x00062B71
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator *(uint4x2 lhs, uint rhs)
		{
			return new uint4x2(lhs.c0 * rhs, lhs.c1 * rhs);
		}

		// Token: 0x060023E4 RID: 9188 RVA: 0x00064990 File Offset: 0x00062B90
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator *(uint lhs, uint4x2 rhs)
		{
			return new uint4x2(lhs * rhs.c0, lhs * rhs.c1);
		}

		// Token: 0x060023E5 RID: 9189 RVA: 0x000649AF File Offset: 0x00062BAF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator +(uint4x2 lhs, uint4x2 rhs)
		{
			return new uint4x2(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1);
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x000649D8 File Offset: 0x00062BD8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator +(uint4x2 lhs, uint rhs)
		{
			return new uint4x2(lhs.c0 + rhs, lhs.c1 + rhs);
		}

		// Token: 0x060023E7 RID: 9191 RVA: 0x000649F7 File Offset: 0x00062BF7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator +(uint lhs, uint4x2 rhs)
		{
			return new uint4x2(lhs + rhs.c0, lhs + rhs.c1);
		}

		// Token: 0x060023E8 RID: 9192 RVA: 0x00064A16 File Offset: 0x00062C16
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator -(uint4x2 lhs, uint4x2 rhs)
		{
			return new uint4x2(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1);
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x00064A3F File Offset: 0x00062C3F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator -(uint4x2 lhs, uint rhs)
		{
			return new uint4x2(lhs.c0 - rhs, lhs.c1 - rhs);
		}

		// Token: 0x060023EA RID: 9194 RVA: 0x00064A5E File Offset: 0x00062C5E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator -(uint lhs, uint4x2 rhs)
		{
			return new uint4x2(lhs - rhs.c0, lhs - rhs.c1);
		}

		// Token: 0x060023EB RID: 9195 RVA: 0x00064A7D File Offset: 0x00062C7D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator /(uint4x2 lhs, uint4x2 rhs)
		{
			return new uint4x2(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1);
		}

		// Token: 0x060023EC RID: 9196 RVA: 0x00064AA6 File Offset: 0x00062CA6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator /(uint4x2 lhs, uint rhs)
		{
			return new uint4x2(lhs.c0 / rhs, lhs.c1 / rhs);
		}

		// Token: 0x060023ED RID: 9197 RVA: 0x00064AC5 File Offset: 0x00062CC5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator /(uint lhs, uint4x2 rhs)
		{
			return new uint4x2(lhs / rhs.c0, lhs / rhs.c1);
		}

		// Token: 0x060023EE RID: 9198 RVA: 0x00064AE4 File Offset: 0x00062CE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator %(uint4x2 lhs, uint4x2 rhs)
		{
			return new uint4x2(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1);
		}

		// Token: 0x060023EF RID: 9199 RVA: 0x00064B0D File Offset: 0x00062D0D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator %(uint4x2 lhs, uint rhs)
		{
			return new uint4x2(lhs.c0 % rhs, lhs.c1 % rhs);
		}

		// Token: 0x060023F0 RID: 9200 RVA: 0x00064B2C File Offset: 0x00062D2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator %(uint lhs, uint4x2 rhs)
		{
			return new uint4x2(lhs % rhs.c0, lhs % rhs.c1);
		}

		// Token: 0x060023F1 RID: 9201 RVA: 0x00064B4C File Offset: 0x00062D4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator ++(uint4x2 val)
		{
			uint4 @uint = uint4.op_Increment(val.c0);
			val.c0 = @uint;
			uint4 uint2 = @uint;
			@uint = uint4.op_Increment(val.c1);
			val.c1 = @uint;
			return new uint4x2(uint2, @uint);
		}

		// Token: 0x060023F2 RID: 9202 RVA: 0x00064B94 File Offset: 0x00062D94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator --(uint4x2 val)
		{
			uint4 @uint = uint4.op_Decrement(val.c0);
			val.c0 = @uint;
			uint4 uint2 = @uint;
			@uint = uint4.op_Decrement(val.c1);
			val.c1 = @uint;
			return new uint4x2(uint2, @uint);
		}

		// Token: 0x060023F3 RID: 9203 RVA: 0x00064BDA File Offset: 0x00062DDA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <(uint4x2 lhs, uint4x2 rhs)
		{
			return new bool4x2(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1);
		}

		// Token: 0x060023F4 RID: 9204 RVA: 0x00064C03 File Offset: 0x00062E03
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <(uint4x2 lhs, uint rhs)
		{
			return new bool4x2(lhs.c0 < rhs, lhs.c1 < rhs);
		}

		// Token: 0x060023F5 RID: 9205 RVA: 0x00064C22 File Offset: 0x00062E22
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <(uint lhs, uint4x2 rhs)
		{
			return new bool4x2(lhs < rhs.c0, lhs < rhs.c1);
		}

		// Token: 0x060023F6 RID: 9206 RVA: 0x00064C41 File Offset: 0x00062E41
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <=(uint4x2 lhs, uint4x2 rhs)
		{
			return new bool4x2(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1);
		}

		// Token: 0x060023F7 RID: 9207 RVA: 0x00064C6A File Offset: 0x00062E6A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <=(uint4x2 lhs, uint rhs)
		{
			return new bool4x2(lhs.c0 <= rhs, lhs.c1 <= rhs);
		}

		// Token: 0x060023F8 RID: 9208 RVA: 0x00064C89 File Offset: 0x00062E89
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <=(uint lhs, uint4x2 rhs)
		{
			return new bool4x2(lhs <= rhs.c0, lhs <= rhs.c1);
		}

		// Token: 0x060023F9 RID: 9209 RVA: 0x00064CA8 File Offset: 0x00062EA8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >(uint4x2 lhs, uint4x2 rhs)
		{
			return new bool4x2(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1);
		}

		// Token: 0x060023FA RID: 9210 RVA: 0x00064CD1 File Offset: 0x00062ED1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >(uint4x2 lhs, uint rhs)
		{
			return new bool4x2(lhs.c0 > rhs, lhs.c1 > rhs);
		}

		// Token: 0x060023FB RID: 9211 RVA: 0x00064CF0 File Offset: 0x00062EF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >(uint lhs, uint4x2 rhs)
		{
			return new bool4x2(lhs > rhs.c0, lhs > rhs.c1);
		}

		// Token: 0x060023FC RID: 9212 RVA: 0x00064D0F File Offset: 0x00062F0F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >=(uint4x2 lhs, uint4x2 rhs)
		{
			return new bool4x2(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1);
		}

		// Token: 0x060023FD RID: 9213 RVA: 0x00064D38 File Offset: 0x00062F38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >=(uint4x2 lhs, uint rhs)
		{
			return new bool4x2(lhs.c0 >= rhs, lhs.c1 >= rhs);
		}

		// Token: 0x060023FE RID: 9214 RVA: 0x00064D57 File Offset: 0x00062F57
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >=(uint lhs, uint4x2 rhs)
		{
			return new bool4x2(lhs >= rhs.c0, lhs >= rhs.c1);
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x00064D76 File Offset: 0x00062F76
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator -(uint4x2 val)
		{
			return new uint4x2(-val.c0, -val.c1);
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x00064D93 File Offset: 0x00062F93
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator +(uint4x2 val)
		{
			return new uint4x2(+val.c0, +val.c1);
		}

		// Token: 0x06002401 RID: 9217 RVA: 0x00064DB0 File Offset: 0x00062FB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator <<(uint4x2 x, int n)
		{
			return new uint4x2(x.c0 << n, x.c1 << n);
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x00064DCF File Offset: 0x00062FCF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator >>(uint4x2 x, int n)
		{
			return new uint4x2(x.c0 >> n, x.c1 >> n);
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x00064DEE File Offset: 0x00062FEE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator ==(uint4x2 lhs, uint4x2 rhs)
		{
			return new bool4x2(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1);
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x00064E17 File Offset: 0x00063017
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator ==(uint4x2 lhs, uint rhs)
		{
			return new bool4x2(lhs.c0 == rhs, lhs.c1 == rhs);
		}

		// Token: 0x06002405 RID: 9221 RVA: 0x00064E36 File Offset: 0x00063036
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator ==(uint lhs, uint4x2 rhs)
		{
			return new bool4x2(lhs == rhs.c0, lhs == rhs.c1);
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x00064E55 File Offset: 0x00063055
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator !=(uint4x2 lhs, uint4x2 rhs)
		{
			return new bool4x2(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1);
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x00064E7E File Offset: 0x0006307E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator !=(uint4x2 lhs, uint rhs)
		{
			return new bool4x2(lhs.c0 != rhs, lhs.c1 != rhs);
		}

		// Token: 0x06002408 RID: 9224 RVA: 0x00064E9D File Offset: 0x0006309D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator !=(uint lhs, uint4x2 rhs)
		{
			return new bool4x2(lhs != rhs.c0, lhs != rhs.c1);
		}

		// Token: 0x06002409 RID: 9225 RVA: 0x00064EBC File Offset: 0x000630BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator ~(uint4x2 val)
		{
			return new uint4x2(~val.c0, ~val.c1);
		}

		// Token: 0x0600240A RID: 9226 RVA: 0x00064ED9 File Offset: 0x000630D9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator &(uint4x2 lhs, uint4x2 rhs)
		{
			return new uint4x2(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1);
		}

		// Token: 0x0600240B RID: 9227 RVA: 0x00064F02 File Offset: 0x00063102
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator &(uint4x2 lhs, uint rhs)
		{
			return new uint4x2(lhs.c0 & rhs, lhs.c1 & rhs);
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x00064F21 File Offset: 0x00063121
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator &(uint lhs, uint4x2 rhs)
		{
			return new uint4x2(lhs & rhs.c0, lhs & rhs.c1);
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x00064F40 File Offset: 0x00063140
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator |(uint4x2 lhs, uint4x2 rhs)
		{
			return new uint4x2(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1);
		}

		// Token: 0x0600240E RID: 9230 RVA: 0x00064F69 File Offset: 0x00063169
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator |(uint4x2 lhs, uint rhs)
		{
			return new uint4x2(lhs.c0 | rhs, lhs.c1 | rhs);
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x00064F88 File Offset: 0x00063188
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator |(uint lhs, uint4x2 rhs)
		{
			return new uint4x2(lhs | rhs.c0, lhs | rhs.c1);
		}

		// Token: 0x06002410 RID: 9232 RVA: 0x00064FA7 File Offset: 0x000631A7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator ^(uint4x2 lhs, uint4x2 rhs)
		{
			return new uint4x2(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1);
		}

		// Token: 0x06002411 RID: 9233 RVA: 0x00064FD0 File Offset: 0x000631D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator ^(uint4x2 lhs, uint rhs)
		{
			return new uint4x2(lhs.c0 ^ rhs, lhs.c1 ^ rhs);
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x00064FEF File Offset: 0x000631EF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 operator ^(uint lhs, uint4x2 rhs)
		{
			return new uint4x2(lhs ^ rhs.c0, lhs ^ rhs.c1);
		}

		// Token: 0x17000B87 RID: 2951
		public unsafe ref uint4 this[int index]
		{
			get
			{
				fixed (uint4x2* ptr = &this)
				{
					return ref *(uint4*)(ptr + (IntPtr)index * (IntPtr)sizeof(uint4) / (IntPtr)sizeof(uint4x2));
				}
			}
		}

		// Token: 0x06002414 RID: 9236 RVA: 0x0006502B File Offset: 0x0006322B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(uint4x2 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1);
		}

		// Token: 0x06002415 RID: 9237 RVA: 0x00065054 File Offset: 0x00063254
		public override bool Equals(object o)
		{
			if (o is uint4x2)
			{
				uint4x2 converted = (uint4x2)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06002416 RID: 9238 RVA: 0x00065079 File Offset: 0x00063279
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06002417 RID: 9239 RVA: 0x00065088 File Offset: 0x00063288
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("uint4x2({0}, {1},  {2}, {3},  {4}, {5},  {6}, {7})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c0.y,
				this.c1.y,
				this.c0.z,
				this.c1.z,
				this.c0.w,
				this.c1.w
			});
		}

		// Token: 0x06002418 RID: 9240 RVA: 0x00065140 File Offset: 0x00063340
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("uint4x2({0}, {1},  {2}, {3},  {4}, {5},  {6}, {7})", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c0.z.ToString(format, formatProvider),
				this.c1.z.ToString(format, formatProvider),
				this.c0.w.ToString(format, formatProvider),
				this.c1.w.ToString(format, formatProvider)
			});
		}

		// Token: 0x0400016E RID: 366
		public uint4 c0;

		// Token: 0x0400016F RID: 367
		public uint4 c1;

		// Token: 0x04000170 RID: 368
		public static readonly uint4x2 zero;
	}
}
