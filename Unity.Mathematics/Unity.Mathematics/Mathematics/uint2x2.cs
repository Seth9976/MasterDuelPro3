using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000056 RID: 86
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct uint2x2 : IEquatable<uint2x2>, IFormattable
	{
		// Token: 0x06001F5B RID: 8027 RVA: 0x00059DFA File Offset: 0x00057FFA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x2(uint2 c0, uint2 c1)
		{
			this.c0 = c0;
			this.c1 = c1;
		}

		// Token: 0x06001F5C RID: 8028 RVA: 0x00059E0A File Offset: 0x0005800A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x2(uint m00, uint m01, uint m10, uint m11)
		{
			this.c0 = new uint2(m00, m10);
			this.c1 = new uint2(m01, m11);
		}

		// Token: 0x06001F5D RID: 8029 RVA: 0x00059E27 File Offset: 0x00058027
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x2(uint v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x00059E41 File Offset: 0x00058041
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x2(bool v)
		{
			this.c0 = math.select(new uint2(0U), new uint2(1U), v);
			this.c1 = math.select(new uint2(0U), new uint2(1U), v);
		}

		// Token: 0x06001F5F RID: 8031 RVA: 0x00059E73 File Offset: 0x00058073
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x2(bool2x2 v)
		{
			this.c0 = math.select(new uint2(0U), new uint2(1U), v.c0);
			this.c1 = math.select(new uint2(0U), new uint2(1U), v.c1);
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x00059EAF File Offset: 0x000580AF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x2(int v)
		{
			this.c0 = (uint2)v;
			this.c1 = (uint2)v;
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x00059EC9 File Offset: 0x000580C9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x2(int2x2 v)
		{
			this.c0 = (uint2)v.c0;
			this.c1 = (uint2)v.c1;
		}

		// Token: 0x06001F62 RID: 8034 RVA: 0x00059EED File Offset: 0x000580ED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x2(float v)
		{
			this.c0 = (uint2)v;
			this.c1 = (uint2)v;
		}

		// Token: 0x06001F63 RID: 8035 RVA: 0x00059F07 File Offset: 0x00058107
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x2(float2x2 v)
		{
			this.c0 = (uint2)v.c0;
			this.c1 = (uint2)v.c1;
		}

		// Token: 0x06001F64 RID: 8036 RVA: 0x00059F2B File Offset: 0x0005812B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x2(double v)
		{
			this.c0 = (uint2)v;
			this.c1 = (uint2)v;
		}

		// Token: 0x06001F65 RID: 8037 RVA: 0x00059F45 File Offset: 0x00058145
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x2(double2x2 v)
		{
			this.c0 = (uint2)v.c0;
			this.c1 = (uint2)v.c1;
		}

		// Token: 0x06001F66 RID: 8038 RVA: 0x0001FFA1 File Offset: 0x0001E1A1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator uint2x2(uint v)
		{
			return new uint2x2(v);
		}

		// Token: 0x06001F67 RID: 8039 RVA: 0x0001FFA9 File Offset: 0x0001E1A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x2(bool v)
		{
			return new uint2x2(v);
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x0001FFB1 File Offset: 0x0001E1B1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x2(bool2x2 v)
		{
			return new uint2x2(v);
		}

		// Token: 0x06001F69 RID: 8041 RVA: 0x0001FFB9 File Offset: 0x0001E1B9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x2(int v)
		{
			return new uint2x2(v);
		}

		// Token: 0x06001F6A RID: 8042 RVA: 0x0001FFC1 File Offset: 0x0001E1C1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x2(int2x2 v)
		{
			return new uint2x2(v);
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x0001FFC9 File Offset: 0x0001E1C9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x2(float v)
		{
			return new uint2x2(v);
		}

		// Token: 0x06001F6C RID: 8044 RVA: 0x0001FFD1 File Offset: 0x0001E1D1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x2(float2x2 v)
		{
			return new uint2x2(v);
		}

		// Token: 0x06001F6D RID: 8045 RVA: 0x0001FFD9 File Offset: 0x0001E1D9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x2(double v)
		{
			return new uint2x2(v);
		}

		// Token: 0x06001F6E RID: 8046 RVA: 0x0001FFE1 File Offset: 0x0001E1E1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x2(double2x2 v)
		{
			return new uint2x2(v);
		}

		// Token: 0x06001F6F RID: 8047 RVA: 0x00059F69 File Offset: 0x00058169
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator *(uint2x2 lhs, uint2x2 rhs)
		{
			return new uint2x2(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1);
		}

		// Token: 0x06001F70 RID: 8048 RVA: 0x00059F92 File Offset: 0x00058192
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator *(uint2x2 lhs, uint rhs)
		{
			return new uint2x2(lhs.c0 * rhs, lhs.c1 * rhs);
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x00059FB1 File Offset: 0x000581B1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator *(uint lhs, uint2x2 rhs)
		{
			return new uint2x2(lhs * rhs.c0, lhs * rhs.c1);
		}

		// Token: 0x06001F72 RID: 8050 RVA: 0x00059FD0 File Offset: 0x000581D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator +(uint2x2 lhs, uint2x2 rhs)
		{
			return new uint2x2(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1);
		}

		// Token: 0x06001F73 RID: 8051 RVA: 0x00059FF9 File Offset: 0x000581F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator +(uint2x2 lhs, uint rhs)
		{
			return new uint2x2(lhs.c0 + rhs, lhs.c1 + rhs);
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x0005A018 File Offset: 0x00058218
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator +(uint lhs, uint2x2 rhs)
		{
			return new uint2x2(lhs + rhs.c0, lhs + rhs.c1);
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x0005A037 File Offset: 0x00058237
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator -(uint2x2 lhs, uint2x2 rhs)
		{
			return new uint2x2(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1);
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x0005A060 File Offset: 0x00058260
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator -(uint2x2 lhs, uint rhs)
		{
			return new uint2x2(lhs.c0 - rhs, lhs.c1 - rhs);
		}

		// Token: 0x06001F77 RID: 8055 RVA: 0x0005A07F File Offset: 0x0005827F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator -(uint lhs, uint2x2 rhs)
		{
			return new uint2x2(lhs - rhs.c0, lhs - rhs.c1);
		}

		// Token: 0x06001F78 RID: 8056 RVA: 0x0005A09E File Offset: 0x0005829E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator /(uint2x2 lhs, uint2x2 rhs)
		{
			return new uint2x2(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1);
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x0005A0C7 File Offset: 0x000582C7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator /(uint2x2 lhs, uint rhs)
		{
			return new uint2x2(lhs.c0 / rhs, lhs.c1 / rhs);
		}

		// Token: 0x06001F7A RID: 8058 RVA: 0x0005A0E6 File Offset: 0x000582E6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator /(uint lhs, uint2x2 rhs)
		{
			return new uint2x2(lhs / rhs.c0, lhs / rhs.c1);
		}

		// Token: 0x06001F7B RID: 8059 RVA: 0x0005A105 File Offset: 0x00058305
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator %(uint2x2 lhs, uint2x2 rhs)
		{
			return new uint2x2(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1);
		}

		// Token: 0x06001F7C RID: 8060 RVA: 0x0005A12E File Offset: 0x0005832E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator %(uint2x2 lhs, uint rhs)
		{
			return new uint2x2(lhs.c0 % rhs, lhs.c1 % rhs);
		}

		// Token: 0x06001F7D RID: 8061 RVA: 0x0005A14D File Offset: 0x0005834D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator %(uint lhs, uint2x2 rhs)
		{
			return new uint2x2(lhs % rhs.c0, lhs % rhs.c1);
		}

		// Token: 0x06001F7E RID: 8062 RVA: 0x0005A16C File Offset: 0x0005836C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator ++(uint2x2 val)
		{
			uint2 @uint = uint2.op_Increment(val.c0);
			val.c0 = @uint;
			uint2 uint2 = @uint;
			@uint = uint2.op_Increment(val.c1);
			val.c1 = @uint;
			return new uint2x2(uint2, @uint);
		}

		// Token: 0x06001F7F RID: 8063 RVA: 0x0005A1B4 File Offset: 0x000583B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator --(uint2x2 val)
		{
			uint2 @uint = uint2.op_Decrement(val.c0);
			val.c0 = @uint;
			uint2 uint2 = @uint;
			@uint = uint2.op_Decrement(val.c1);
			val.c1 = @uint;
			return new uint2x2(uint2, @uint);
		}

		// Token: 0x06001F80 RID: 8064 RVA: 0x0005A1FA File Offset: 0x000583FA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <(uint2x2 lhs, uint2x2 rhs)
		{
			return new bool2x2(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1);
		}

		// Token: 0x06001F81 RID: 8065 RVA: 0x0005A223 File Offset: 0x00058423
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <(uint2x2 lhs, uint rhs)
		{
			return new bool2x2(lhs.c0 < rhs, lhs.c1 < rhs);
		}

		// Token: 0x06001F82 RID: 8066 RVA: 0x0005A242 File Offset: 0x00058442
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <(uint lhs, uint2x2 rhs)
		{
			return new bool2x2(lhs < rhs.c0, lhs < rhs.c1);
		}

		// Token: 0x06001F83 RID: 8067 RVA: 0x0005A261 File Offset: 0x00058461
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <=(uint2x2 lhs, uint2x2 rhs)
		{
			return new bool2x2(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1);
		}

		// Token: 0x06001F84 RID: 8068 RVA: 0x0005A28A File Offset: 0x0005848A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <=(uint2x2 lhs, uint rhs)
		{
			return new bool2x2(lhs.c0 <= rhs, lhs.c1 <= rhs);
		}

		// Token: 0x06001F85 RID: 8069 RVA: 0x0005A2A9 File Offset: 0x000584A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <=(uint lhs, uint2x2 rhs)
		{
			return new bool2x2(lhs <= rhs.c0, lhs <= rhs.c1);
		}

		// Token: 0x06001F86 RID: 8070 RVA: 0x0005A2C8 File Offset: 0x000584C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >(uint2x2 lhs, uint2x2 rhs)
		{
			return new bool2x2(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1);
		}

		// Token: 0x06001F87 RID: 8071 RVA: 0x0005A2F1 File Offset: 0x000584F1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >(uint2x2 lhs, uint rhs)
		{
			return new bool2x2(lhs.c0 > rhs, lhs.c1 > rhs);
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x0005A310 File Offset: 0x00058510
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >(uint lhs, uint2x2 rhs)
		{
			return new bool2x2(lhs > rhs.c0, lhs > rhs.c1);
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x0005A32F File Offset: 0x0005852F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >=(uint2x2 lhs, uint2x2 rhs)
		{
			return new bool2x2(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1);
		}

		// Token: 0x06001F8A RID: 8074 RVA: 0x0005A358 File Offset: 0x00058558
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >=(uint2x2 lhs, uint rhs)
		{
			return new bool2x2(lhs.c0 >= rhs, lhs.c1 >= rhs);
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x0005A377 File Offset: 0x00058577
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >=(uint lhs, uint2x2 rhs)
		{
			return new bool2x2(lhs >= rhs.c0, lhs >= rhs.c1);
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x0005A396 File Offset: 0x00058596
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator -(uint2x2 val)
		{
			return new uint2x2(-val.c0, -val.c1);
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x0005A3B3 File Offset: 0x000585B3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator +(uint2x2 val)
		{
			return new uint2x2(+val.c0, +val.c1);
		}

		// Token: 0x06001F8E RID: 8078 RVA: 0x0005A3D0 File Offset: 0x000585D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator <<(uint2x2 x, int n)
		{
			return new uint2x2(x.c0 << n, x.c1 << n);
		}

		// Token: 0x06001F8F RID: 8079 RVA: 0x0005A3EF File Offset: 0x000585EF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator >>(uint2x2 x, int n)
		{
			return new uint2x2(x.c0 >> n, x.c1 >> n);
		}

		// Token: 0x06001F90 RID: 8080 RVA: 0x0005A40E File Offset: 0x0005860E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator ==(uint2x2 lhs, uint2x2 rhs)
		{
			return new bool2x2(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1);
		}

		// Token: 0x06001F91 RID: 8081 RVA: 0x0005A437 File Offset: 0x00058637
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator ==(uint2x2 lhs, uint rhs)
		{
			return new bool2x2(lhs.c0 == rhs, lhs.c1 == rhs);
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x0005A456 File Offset: 0x00058656
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator ==(uint lhs, uint2x2 rhs)
		{
			return new bool2x2(lhs == rhs.c0, lhs == rhs.c1);
		}

		// Token: 0x06001F93 RID: 8083 RVA: 0x0005A475 File Offset: 0x00058675
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator !=(uint2x2 lhs, uint2x2 rhs)
		{
			return new bool2x2(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1);
		}

		// Token: 0x06001F94 RID: 8084 RVA: 0x0005A49E File Offset: 0x0005869E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator !=(uint2x2 lhs, uint rhs)
		{
			return new bool2x2(lhs.c0 != rhs, lhs.c1 != rhs);
		}

		// Token: 0x06001F95 RID: 8085 RVA: 0x0005A4BD File Offset: 0x000586BD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator !=(uint lhs, uint2x2 rhs)
		{
			return new bool2x2(lhs != rhs.c0, lhs != rhs.c1);
		}

		// Token: 0x06001F96 RID: 8086 RVA: 0x0005A4DC File Offset: 0x000586DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator ~(uint2x2 val)
		{
			return new uint2x2(~val.c0, ~val.c1);
		}

		// Token: 0x06001F97 RID: 8087 RVA: 0x0005A4F9 File Offset: 0x000586F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator &(uint2x2 lhs, uint2x2 rhs)
		{
			return new uint2x2(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1);
		}

		// Token: 0x06001F98 RID: 8088 RVA: 0x0005A522 File Offset: 0x00058722
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator &(uint2x2 lhs, uint rhs)
		{
			return new uint2x2(lhs.c0 & rhs, lhs.c1 & rhs);
		}

		// Token: 0x06001F99 RID: 8089 RVA: 0x0005A541 File Offset: 0x00058741
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator &(uint lhs, uint2x2 rhs)
		{
			return new uint2x2(lhs & rhs.c0, lhs & rhs.c1);
		}

		// Token: 0x06001F9A RID: 8090 RVA: 0x0005A560 File Offset: 0x00058760
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator |(uint2x2 lhs, uint2x2 rhs)
		{
			return new uint2x2(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1);
		}

		// Token: 0x06001F9B RID: 8091 RVA: 0x0005A589 File Offset: 0x00058789
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator |(uint2x2 lhs, uint rhs)
		{
			return new uint2x2(lhs.c0 | rhs, lhs.c1 | rhs);
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x0005A5A8 File Offset: 0x000587A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator |(uint lhs, uint2x2 rhs)
		{
			return new uint2x2(lhs | rhs.c0, lhs | rhs.c1);
		}

		// Token: 0x06001F9D RID: 8093 RVA: 0x0005A5C7 File Offset: 0x000587C7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator ^(uint2x2 lhs, uint2x2 rhs)
		{
			return new uint2x2(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1);
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x0005A5F0 File Offset: 0x000587F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator ^(uint2x2 lhs, uint rhs)
		{
			return new uint2x2(lhs.c0 ^ rhs, lhs.c1 ^ rhs);
		}

		// Token: 0x06001F9F RID: 8095 RVA: 0x0005A60F File Offset: 0x0005880F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 operator ^(uint lhs, uint2x2 rhs)
		{
			return new uint2x2(lhs ^ rhs.c0, lhs ^ rhs.c1);
		}

		// Token: 0x170009BA RID: 2490
		public unsafe ref uint2 this[int index]
		{
			get
			{
				fixed (uint2x2* ptr = &this)
				{
					return ref *(uint2*)(ptr + (IntPtr)index * (IntPtr)sizeof(uint2) / (IntPtr)sizeof(uint2x2));
				}
			}
		}

		// Token: 0x06001FA1 RID: 8097 RVA: 0x0005A64B File Offset: 0x0005884B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(uint2x2 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1);
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x0005A674 File Offset: 0x00058874
		public override bool Equals(object o)
		{
			if (o is uint2x2)
			{
				uint2x2 converted = (uint2x2)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x0005A699 File Offset: 0x00058899
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x0005A6A8 File Offset: 0x000588A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("uint2x2({0}, {1},  {2}, {3})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c0.y,
				this.c1.y
			});
		}

		// Token: 0x06001FA5 RID: 8101 RVA: 0x0005A714 File Offset: 0x00058914
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("uint2x2({0}, {1},  {2}, {3})", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider)
			});
		}

		// Token: 0x04000144 RID: 324
		public uint2 c0;

		// Token: 0x04000145 RID: 325
		public uint2 c1;

		// Token: 0x04000146 RID: 326
		public static readonly uint2x2 identity = new uint2x2(1U, 0U, 0U, 1U);

		// Token: 0x04000147 RID: 327
		public static readonly uint2x2 zero;
	}
}
