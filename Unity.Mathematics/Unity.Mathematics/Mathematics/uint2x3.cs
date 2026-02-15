using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000057 RID: 87
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct uint2x3 : IEquatable<uint2x3>, IFormattable
	{
		// Token: 0x06001FA7 RID: 8103 RVA: 0x0005A795 File Offset: 0x00058995
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x3(uint2 c0, uint2 c1, uint2 c2)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
		}

		// Token: 0x06001FA8 RID: 8104 RVA: 0x0005A7AC File Offset: 0x000589AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x3(uint m00, uint m01, uint m02, uint m10, uint m11, uint m12)
		{
			this.c0 = new uint2(m00, m10);
			this.c1 = new uint2(m01, m11);
			this.c2 = new uint2(m02, m12);
		}

		// Token: 0x06001FA9 RID: 8105 RVA: 0x0005A7D8 File Offset: 0x000589D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x3(uint v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x06001FAA RID: 8106 RVA: 0x0005A800 File Offset: 0x00058A00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x3(bool v)
		{
			this.c0 = math.select(new uint2(0U), new uint2(1U), v);
			this.c1 = math.select(new uint2(0U), new uint2(1U), v);
			this.c2 = math.select(new uint2(0U), new uint2(1U), v);
		}

		// Token: 0x06001FAB RID: 8107 RVA: 0x0005A858 File Offset: 0x00058A58
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x3(bool2x3 v)
		{
			this.c0 = math.select(new uint2(0U), new uint2(1U), v.c0);
			this.c1 = math.select(new uint2(0U), new uint2(1U), v.c1);
			this.c2 = math.select(new uint2(0U), new uint2(1U), v.c2);
		}

		// Token: 0x06001FAC RID: 8108 RVA: 0x0005A8BC File Offset: 0x00058ABC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x3(int v)
		{
			this.c0 = (uint2)v;
			this.c1 = (uint2)v;
			this.c2 = (uint2)v;
		}

		// Token: 0x06001FAD RID: 8109 RVA: 0x0005A8E2 File Offset: 0x00058AE2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x3(int2x3 v)
		{
			this.c0 = (uint2)v.c0;
			this.c1 = (uint2)v.c1;
			this.c2 = (uint2)v.c2;
		}

		// Token: 0x06001FAE RID: 8110 RVA: 0x0005A917 File Offset: 0x00058B17
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x3(float v)
		{
			this.c0 = (uint2)v;
			this.c1 = (uint2)v;
			this.c2 = (uint2)v;
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x0005A93D File Offset: 0x00058B3D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x3(float2x3 v)
		{
			this.c0 = (uint2)v.c0;
			this.c1 = (uint2)v.c1;
			this.c2 = (uint2)v.c2;
		}

		// Token: 0x06001FB0 RID: 8112 RVA: 0x0005A972 File Offset: 0x00058B72
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x3(double v)
		{
			this.c0 = (uint2)v;
			this.c1 = (uint2)v;
			this.c2 = (uint2)v;
		}

		// Token: 0x06001FB1 RID: 8113 RVA: 0x0005A998 File Offset: 0x00058B98
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2x3(double2x3 v)
		{
			this.c0 = (uint2)v.c0;
			this.c1 = (uint2)v.c1;
			this.c2 = (uint2)v.c2;
		}

		// Token: 0x06001FB2 RID: 8114 RVA: 0x000200D9 File Offset: 0x0001E2D9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator uint2x3(uint v)
		{
			return new uint2x3(v);
		}

		// Token: 0x06001FB3 RID: 8115 RVA: 0x000200E1 File Offset: 0x0001E2E1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x3(bool v)
		{
			return new uint2x3(v);
		}

		// Token: 0x06001FB4 RID: 8116 RVA: 0x000200E9 File Offset: 0x0001E2E9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x3(bool2x3 v)
		{
			return new uint2x3(v);
		}

		// Token: 0x06001FB5 RID: 8117 RVA: 0x000200F1 File Offset: 0x0001E2F1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x3(int v)
		{
			return new uint2x3(v);
		}

		// Token: 0x06001FB6 RID: 8118 RVA: 0x000200F9 File Offset: 0x0001E2F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x3(int2x3 v)
		{
			return new uint2x3(v);
		}

		// Token: 0x06001FB7 RID: 8119 RVA: 0x00020101 File Offset: 0x0001E301
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x3(float v)
		{
			return new uint2x3(v);
		}

		// Token: 0x06001FB8 RID: 8120 RVA: 0x00020109 File Offset: 0x0001E309
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x3(float2x3 v)
		{
			return new uint2x3(v);
		}

		// Token: 0x06001FB9 RID: 8121 RVA: 0x00020111 File Offset: 0x0001E311
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x3(double v)
		{
			return new uint2x3(v);
		}

		// Token: 0x06001FBA RID: 8122 RVA: 0x00020119 File Offset: 0x0001E319
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2x3(double2x3 v)
		{
			return new uint2x3(v);
		}

		// Token: 0x06001FBB RID: 8123 RVA: 0x0005A9CD File Offset: 0x00058BCD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator *(uint2x3 lhs, uint2x3 rhs)
		{
			return new uint2x3(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2);
		}

		// Token: 0x06001FBC RID: 8124 RVA: 0x0005AA07 File Offset: 0x00058C07
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator *(uint2x3 lhs, uint rhs)
		{
			return new uint2x3(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs);
		}

		// Token: 0x06001FBD RID: 8125 RVA: 0x0005AA32 File Offset: 0x00058C32
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator *(uint lhs, uint2x3 rhs)
		{
			return new uint2x3(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2);
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x0005AA5D File Offset: 0x00058C5D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator +(uint2x3 lhs, uint2x3 rhs)
		{
			return new uint2x3(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2);
		}

		// Token: 0x06001FBF RID: 8127 RVA: 0x0005AA97 File Offset: 0x00058C97
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator +(uint2x3 lhs, uint rhs)
		{
			return new uint2x3(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs);
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x0005AAC2 File Offset: 0x00058CC2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator +(uint lhs, uint2x3 rhs)
		{
			return new uint2x3(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2);
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x0005AAED File Offset: 0x00058CED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator -(uint2x3 lhs, uint2x3 rhs)
		{
			return new uint2x3(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2);
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x0005AB27 File Offset: 0x00058D27
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator -(uint2x3 lhs, uint rhs)
		{
			return new uint2x3(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs);
		}

		// Token: 0x06001FC3 RID: 8131 RVA: 0x0005AB52 File Offset: 0x00058D52
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator -(uint lhs, uint2x3 rhs)
		{
			return new uint2x3(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2);
		}

		// Token: 0x06001FC4 RID: 8132 RVA: 0x0005AB7D File Offset: 0x00058D7D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator /(uint2x3 lhs, uint2x3 rhs)
		{
			return new uint2x3(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2);
		}

		// Token: 0x06001FC5 RID: 8133 RVA: 0x0005ABB7 File Offset: 0x00058DB7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator /(uint2x3 lhs, uint rhs)
		{
			return new uint2x3(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs);
		}

		// Token: 0x06001FC6 RID: 8134 RVA: 0x0005ABE2 File Offset: 0x00058DE2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator /(uint lhs, uint2x3 rhs)
		{
			return new uint2x3(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2);
		}

		// Token: 0x06001FC7 RID: 8135 RVA: 0x0005AC0D File Offset: 0x00058E0D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator %(uint2x3 lhs, uint2x3 rhs)
		{
			return new uint2x3(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2);
		}

		// Token: 0x06001FC8 RID: 8136 RVA: 0x0005AC47 File Offset: 0x00058E47
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator %(uint2x3 lhs, uint rhs)
		{
			return new uint2x3(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs);
		}

		// Token: 0x06001FC9 RID: 8137 RVA: 0x0005AC72 File Offset: 0x00058E72
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator %(uint lhs, uint2x3 rhs)
		{
			return new uint2x3(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2);
		}

		// Token: 0x06001FCA RID: 8138 RVA: 0x0005ACA0 File Offset: 0x00058EA0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator ++(uint2x3 val)
		{
			uint2 @uint = uint2.op_Increment(val.c0);
			val.c0 = @uint;
			uint2 uint2 = @uint;
			@uint = uint2.op_Increment(val.c1);
			val.c1 = @uint;
			uint2 uint3 = @uint;
			@uint = uint2.op_Increment(val.c2);
			val.c2 = @uint;
			return new uint2x3(uint2, uint3, @uint);
		}

		// Token: 0x06001FCB RID: 8139 RVA: 0x0005AD00 File Offset: 0x00058F00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator --(uint2x3 val)
		{
			uint2 @uint = uint2.op_Decrement(val.c0);
			val.c0 = @uint;
			uint2 uint2 = @uint;
			@uint = uint2.op_Decrement(val.c1);
			val.c1 = @uint;
			uint2 uint3 = @uint;
			@uint = uint2.op_Decrement(val.c2);
			val.c2 = @uint;
			return new uint2x3(uint2, uint3, @uint);
		}

		// Token: 0x06001FCC RID: 8140 RVA: 0x0005AD60 File Offset: 0x00058F60
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <(uint2x3 lhs, uint2x3 rhs)
		{
			return new bool2x3(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2);
		}

		// Token: 0x06001FCD RID: 8141 RVA: 0x0005AD9A File Offset: 0x00058F9A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <(uint2x3 lhs, uint rhs)
		{
			return new bool2x3(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs);
		}

		// Token: 0x06001FCE RID: 8142 RVA: 0x0005ADC5 File Offset: 0x00058FC5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <(uint lhs, uint2x3 rhs)
		{
			return new bool2x3(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2);
		}

		// Token: 0x06001FCF RID: 8143 RVA: 0x0005ADF0 File Offset: 0x00058FF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <=(uint2x3 lhs, uint2x3 rhs)
		{
			return new bool2x3(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2);
		}

		// Token: 0x06001FD0 RID: 8144 RVA: 0x0005AE2A File Offset: 0x0005902A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <=(uint2x3 lhs, uint rhs)
		{
			return new bool2x3(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs);
		}

		// Token: 0x06001FD1 RID: 8145 RVA: 0x0005AE55 File Offset: 0x00059055
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <=(uint lhs, uint2x3 rhs)
		{
			return new bool2x3(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2);
		}

		// Token: 0x06001FD2 RID: 8146 RVA: 0x0005AE80 File Offset: 0x00059080
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >(uint2x3 lhs, uint2x3 rhs)
		{
			return new bool2x3(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2);
		}

		// Token: 0x06001FD3 RID: 8147 RVA: 0x0005AEBA File Offset: 0x000590BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >(uint2x3 lhs, uint rhs)
		{
			return new bool2x3(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs);
		}

		// Token: 0x06001FD4 RID: 8148 RVA: 0x0005AEE5 File Offset: 0x000590E5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >(uint lhs, uint2x3 rhs)
		{
			return new bool2x3(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2);
		}

		// Token: 0x06001FD5 RID: 8149 RVA: 0x0005AF10 File Offset: 0x00059110
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >=(uint2x3 lhs, uint2x3 rhs)
		{
			return new bool2x3(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2);
		}

		// Token: 0x06001FD6 RID: 8150 RVA: 0x0005AF4A File Offset: 0x0005914A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >=(uint2x3 lhs, uint rhs)
		{
			return new bool2x3(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs);
		}

		// Token: 0x06001FD7 RID: 8151 RVA: 0x0005AF75 File Offset: 0x00059175
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >=(uint lhs, uint2x3 rhs)
		{
			return new bool2x3(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2);
		}

		// Token: 0x06001FD8 RID: 8152 RVA: 0x0005AFA0 File Offset: 0x000591A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator -(uint2x3 val)
		{
			return new uint2x3(-val.c0, -val.c1, -val.c2);
		}

		// Token: 0x06001FD9 RID: 8153 RVA: 0x0005AFC8 File Offset: 0x000591C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator +(uint2x3 val)
		{
			return new uint2x3(+val.c0, +val.c1, +val.c2);
		}

		// Token: 0x06001FDA RID: 8154 RVA: 0x0005AFF0 File Offset: 0x000591F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator <<(uint2x3 x, int n)
		{
			return new uint2x3(x.c0 << n, x.c1 << n, x.c2 << n);
		}

		// Token: 0x06001FDB RID: 8155 RVA: 0x0005B01B File Offset: 0x0005921B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator >>(uint2x3 x, int n)
		{
			return new uint2x3(x.c0 >> n, x.c1 >> n, x.c2 >> n);
		}

		// Token: 0x06001FDC RID: 8156 RVA: 0x0005B046 File Offset: 0x00059246
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator ==(uint2x3 lhs, uint2x3 rhs)
		{
			return new bool2x3(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2);
		}

		// Token: 0x06001FDD RID: 8157 RVA: 0x0005B080 File Offset: 0x00059280
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator ==(uint2x3 lhs, uint rhs)
		{
			return new bool2x3(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs);
		}

		// Token: 0x06001FDE RID: 8158 RVA: 0x0005B0AB File Offset: 0x000592AB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator ==(uint lhs, uint2x3 rhs)
		{
			return new bool2x3(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2);
		}

		// Token: 0x06001FDF RID: 8159 RVA: 0x0005B0D6 File Offset: 0x000592D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator !=(uint2x3 lhs, uint2x3 rhs)
		{
			return new bool2x3(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2);
		}

		// Token: 0x06001FE0 RID: 8160 RVA: 0x0005B110 File Offset: 0x00059310
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator !=(uint2x3 lhs, uint rhs)
		{
			return new bool2x3(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs);
		}

		// Token: 0x06001FE1 RID: 8161 RVA: 0x0005B13B File Offset: 0x0005933B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator !=(uint lhs, uint2x3 rhs)
		{
			return new bool2x3(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2);
		}

		// Token: 0x06001FE2 RID: 8162 RVA: 0x0005B166 File Offset: 0x00059366
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator ~(uint2x3 val)
		{
			return new uint2x3(~val.c0, ~val.c1, ~val.c2);
		}

		// Token: 0x06001FE3 RID: 8163 RVA: 0x0005B18E File Offset: 0x0005938E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator &(uint2x3 lhs, uint2x3 rhs)
		{
			return new uint2x3(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1, lhs.c2 & rhs.c2);
		}

		// Token: 0x06001FE4 RID: 8164 RVA: 0x0005B1C8 File Offset: 0x000593C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator &(uint2x3 lhs, uint rhs)
		{
			return new uint2x3(lhs.c0 & rhs, lhs.c1 & rhs, lhs.c2 & rhs);
		}

		// Token: 0x06001FE5 RID: 8165 RVA: 0x0005B1F3 File Offset: 0x000593F3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator &(uint lhs, uint2x3 rhs)
		{
			return new uint2x3(lhs & rhs.c0, lhs & rhs.c1, lhs & rhs.c2);
		}

		// Token: 0x06001FE6 RID: 8166 RVA: 0x0005B21E File Offset: 0x0005941E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator |(uint2x3 lhs, uint2x3 rhs)
		{
			return new uint2x3(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1, lhs.c2 | rhs.c2);
		}

		// Token: 0x06001FE7 RID: 8167 RVA: 0x0005B258 File Offset: 0x00059458
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator |(uint2x3 lhs, uint rhs)
		{
			return new uint2x3(lhs.c0 | rhs, lhs.c1 | rhs, lhs.c2 | rhs);
		}

		// Token: 0x06001FE8 RID: 8168 RVA: 0x0005B283 File Offset: 0x00059483
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator |(uint lhs, uint2x3 rhs)
		{
			return new uint2x3(lhs | rhs.c0, lhs | rhs.c1, lhs | rhs.c2);
		}

		// Token: 0x06001FE9 RID: 8169 RVA: 0x0005B2AE File Offset: 0x000594AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator ^(uint2x3 lhs, uint2x3 rhs)
		{
			return new uint2x3(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1, lhs.c2 ^ rhs.c2);
		}

		// Token: 0x06001FEA RID: 8170 RVA: 0x0005B2E8 File Offset: 0x000594E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator ^(uint2x3 lhs, uint rhs)
		{
			return new uint2x3(lhs.c0 ^ rhs, lhs.c1 ^ rhs, lhs.c2 ^ rhs);
		}

		// Token: 0x06001FEB RID: 8171 RVA: 0x0005B313 File Offset: 0x00059513
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 operator ^(uint lhs, uint2x3 rhs)
		{
			return new uint2x3(lhs ^ rhs.c0, lhs ^ rhs.c1, lhs ^ rhs.c2);
		}

		// Token: 0x170009BB RID: 2491
		public unsafe ref uint2 this[int index]
		{
			get
			{
				fixed (uint2x3* ptr = &this)
				{
					return ref *(uint2*)(ptr + (IntPtr)index * (IntPtr)sizeof(uint2) / (IntPtr)sizeof(uint2x3));
				}
			}
		}

		// Token: 0x06001FED RID: 8173 RVA: 0x0005B35B File Offset: 0x0005955B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(uint2x3 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2);
		}

		// Token: 0x06001FEE RID: 8174 RVA: 0x0005B398 File Offset: 0x00059598
		public override bool Equals(object o)
		{
			if (o is uint2x3)
			{
				uint2x3 converted = (uint2x3)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06001FEF RID: 8175 RVA: 0x0005B3BD File Offset: 0x000595BD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06001FF0 RID: 8176 RVA: 0x0005B3CC File Offset: 0x000595CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("uint2x3({0}, {1}, {2},  {3}, {4}, {5})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c2.x,
				this.c0.y,
				this.c1.y,
				this.c2.y
			});
		}

		// Token: 0x06001FF1 RID: 8177 RVA: 0x0005B45C File Offset: 0x0005965C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("uint2x3({0}, {1}, {2},  {3}, {4}, {5})", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c2.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c2.y.ToString(format, formatProvider)
			});
		}

		// Token: 0x04000148 RID: 328
		public uint2 c0;

		// Token: 0x04000149 RID: 329
		public uint2 c1;

		// Token: 0x0400014A RID: 330
		public uint2 c2;

		// Token: 0x0400014B RID: 331
		public static readonly uint2x3 zero;
	}
}
