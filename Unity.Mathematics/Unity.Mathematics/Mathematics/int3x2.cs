using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000048 RID: 72
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct int3x2 : IEquatable<int3x2>, IFormattable
	{
		// Token: 0x06001AC0 RID: 6848 RVA: 0x0004DD36 File Offset: 0x0004BF36
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x2(int3 c0, int3 c1)
		{
			this.c0 = c0;
			this.c1 = c1;
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x0004DD46 File Offset: 0x0004BF46
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x2(int m00, int m01, int m10, int m11, int m20, int m21)
		{
			this.c0 = new int3(m00, m10, m20);
			this.c1 = new int3(m01, m11, m21);
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x0004DD67 File Offset: 0x0004BF67
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x2(int v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x0004DD81 File Offset: 0x0004BF81
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x2(bool v)
		{
			this.c0 = math.select(new int3(0), new int3(1), v);
			this.c1 = math.select(new int3(0), new int3(1), v);
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x0004DDB3 File Offset: 0x0004BFB3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x2(bool3x2 v)
		{
			this.c0 = math.select(new int3(0), new int3(1), v.c0);
			this.c1 = math.select(new int3(0), new int3(1), v.c1);
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x0004DDEF File Offset: 0x0004BFEF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x2(uint v)
		{
			this.c0 = (int3)v;
			this.c1 = (int3)v;
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x0004DE09 File Offset: 0x0004C009
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x2(uint3x2 v)
		{
			this.c0 = (int3)v.c0;
			this.c1 = (int3)v.c1;
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x0004DE2D File Offset: 0x0004C02D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x2(float v)
		{
			this.c0 = (int3)v;
			this.c1 = (int3)v;
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x0004DE47 File Offset: 0x0004C047
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x2(float3x2 v)
		{
			this.c0 = (int3)v.c0;
			this.c1 = (int3)v.c1;
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x0004DE6B File Offset: 0x0004C06B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x2(double v)
		{
			this.c0 = (int3)v;
			this.c1 = (int3)v;
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x0004DE85 File Offset: 0x0004C085
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x2(double3x2 v)
		{
			this.c0 = (int3)v.c0;
			this.c1 = (int3)v.c1;
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x0000E537 File Offset: 0x0000C737
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator int3x2(int v)
		{
			return new int3x2(v);
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x0000E53F File Offset: 0x0000C73F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x2(bool v)
		{
			return new int3x2(v);
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x0000E547 File Offset: 0x0000C747
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x2(bool3x2 v)
		{
			return new int3x2(v);
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x0000E54F File Offset: 0x0000C74F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x2(uint v)
		{
			return new int3x2(v);
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x0000E557 File Offset: 0x0000C757
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x2(uint3x2 v)
		{
			return new int3x2(v);
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x0000E55F File Offset: 0x0000C75F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x2(float v)
		{
			return new int3x2(v);
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x0000E567 File Offset: 0x0000C767
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x2(float3x2 v)
		{
			return new int3x2(v);
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x0000E56F File Offset: 0x0000C76F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x2(double v)
		{
			return new int3x2(v);
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x0000E577 File Offset: 0x0000C777
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x2(double3x2 v)
		{
			return new int3x2(v);
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x0004DEA9 File Offset: 0x0004C0A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator *(int3x2 lhs, int3x2 rhs)
		{
			return new int3x2(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1);
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x0004DED2 File Offset: 0x0004C0D2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator *(int3x2 lhs, int rhs)
		{
			return new int3x2(lhs.c0 * rhs, lhs.c1 * rhs);
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x0004DEF1 File Offset: 0x0004C0F1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator *(int lhs, int3x2 rhs)
		{
			return new int3x2(lhs * rhs.c0, lhs * rhs.c1);
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x0004DF10 File Offset: 0x0004C110
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator +(int3x2 lhs, int3x2 rhs)
		{
			return new int3x2(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1);
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x0004DF39 File Offset: 0x0004C139
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator +(int3x2 lhs, int rhs)
		{
			return new int3x2(lhs.c0 + rhs, lhs.c1 + rhs);
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x0004DF58 File Offset: 0x0004C158
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator +(int lhs, int3x2 rhs)
		{
			return new int3x2(lhs + rhs.c0, lhs + rhs.c1);
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x0004DF77 File Offset: 0x0004C177
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator -(int3x2 lhs, int3x2 rhs)
		{
			return new int3x2(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1);
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x0004DFA0 File Offset: 0x0004C1A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator -(int3x2 lhs, int rhs)
		{
			return new int3x2(lhs.c0 - rhs, lhs.c1 - rhs);
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x0004DFBF File Offset: 0x0004C1BF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator -(int lhs, int3x2 rhs)
		{
			return new int3x2(lhs - rhs.c0, lhs - rhs.c1);
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x0004DFDE File Offset: 0x0004C1DE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator /(int3x2 lhs, int3x2 rhs)
		{
			return new int3x2(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1);
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x0004E007 File Offset: 0x0004C207
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator /(int3x2 lhs, int rhs)
		{
			return new int3x2(lhs.c0 / rhs, lhs.c1 / rhs);
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x0004E026 File Offset: 0x0004C226
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator /(int lhs, int3x2 rhs)
		{
			return new int3x2(lhs / rhs.c0, lhs / rhs.c1);
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x0004E045 File Offset: 0x0004C245
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator %(int3x2 lhs, int3x2 rhs)
		{
			return new int3x2(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1);
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x0004E06E File Offset: 0x0004C26E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator %(int3x2 lhs, int rhs)
		{
			return new int3x2(lhs.c0 % rhs, lhs.c1 % rhs);
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x0004E08D File Offset: 0x0004C28D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator %(int lhs, int3x2 rhs)
		{
			return new int3x2(lhs % rhs.c0, lhs % rhs.c1);
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x0004E0AC File Offset: 0x0004C2AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator ++(int3x2 val)
		{
			int3 @int = int3.op_Increment(val.c0);
			val.c0 = @int;
			int3 int2 = @int;
			@int = int3.op_Increment(val.c1);
			val.c1 = @int;
			return new int3x2(int2, @int);
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x0004E0F4 File Offset: 0x0004C2F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator --(int3x2 val)
		{
			int3 @int = int3.op_Decrement(val.c0);
			val.c0 = @int;
			int3 int2 = @int;
			@int = int3.op_Decrement(val.c1);
			val.c1 = @int;
			return new int3x2(int2, @int);
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x0004E13A File Offset: 0x0004C33A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <(int3x2 lhs, int3x2 rhs)
		{
			return new bool3x2(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1);
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x0004E163 File Offset: 0x0004C363
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <(int3x2 lhs, int rhs)
		{
			return new bool3x2(lhs.c0 < rhs, lhs.c1 < rhs);
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x0004E182 File Offset: 0x0004C382
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <(int lhs, int3x2 rhs)
		{
			return new bool3x2(lhs < rhs.c0, lhs < rhs.c1);
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x0004E1A1 File Offset: 0x0004C3A1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <=(int3x2 lhs, int3x2 rhs)
		{
			return new bool3x2(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1);
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x0004E1CA File Offset: 0x0004C3CA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <=(int3x2 lhs, int rhs)
		{
			return new bool3x2(lhs.c0 <= rhs, lhs.c1 <= rhs);
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x0004E1E9 File Offset: 0x0004C3E9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <=(int lhs, int3x2 rhs)
		{
			return new bool3x2(lhs <= rhs.c0, lhs <= rhs.c1);
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x0004E208 File Offset: 0x0004C408
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >(int3x2 lhs, int3x2 rhs)
		{
			return new bool3x2(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1);
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x0004E231 File Offset: 0x0004C431
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >(int3x2 lhs, int rhs)
		{
			return new bool3x2(lhs.c0 > rhs, lhs.c1 > rhs);
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x0004E250 File Offset: 0x0004C450
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >(int lhs, int3x2 rhs)
		{
			return new bool3x2(lhs > rhs.c0, lhs > rhs.c1);
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x0004E26F File Offset: 0x0004C46F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >=(int3x2 lhs, int3x2 rhs)
		{
			return new bool3x2(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1);
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x0004E298 File Offset: 0x0004C498
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >=(int3x2 lhs, int rhs)
		{
			return new bool3x2(lhs.c0 >= rhs, lhs.c1 >= rhs);
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x0004E2B7 File Offset: 0x0004C4B7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >=(int lhs, int3x2 rhs)
		{
			return new bool3x2(lhs >= rhs.c0, lhs >= rhs.c1);
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x0004E2D6 File Offset: 0x0004C4D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator -(int3x2 val)
		{
			return new int3x2(-val.c0, -val.c1);
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x0004E2F3 File Offset: 0x0004C4F3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator +(int3x2 val)
		{
			return new int3x2(+val.c0, +val.c1);
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x0004E310 File Offset: 0x0004C510
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator <<(int3x2 x, int n)
		{
			return new int3x2(x.c0 << n, x.c1 << n);
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x0004E32F File Offset: 0x0004C52F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator >>(int3x2 x, int n)
		{
			return new int3x2(x.c0 >> n, x.c1 >> n);
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x0004E34E File Offset: 0x0004C54E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator ==(int3x2 lhs, int3x2 rhs)
		{
			return new bool3x2(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1);
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x0004E377 File Offset: 0x0004C577
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator ==(int3x2 lhs, int rhs)
		{
			return new bool3x2(lhs.c0 == rhs, lhs.c1 == rhs);
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x0004E396 File Offset: 0x0004C596
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator ==(int lhs, int3x2 rhs)
		{
			return new bool3x2(lhs == rhs.c0, lhs == rhs.c1);
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x0004E3B5 File Offset: 0x0004C5B5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator !=(int3x2 lhs, int3x2 rhs)
		{
			return new bool3x2(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1);
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x0004E3DE File Offset: 0x0004C5DE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator !=(int3x2 lhs, int rhs)
		{
			return new bool3x2(lhs.c0 != rhs, lhs.c1 != rhs);
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x0004E3FD File Offset: 0x0004C5FD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator !=(int lhs, int3x2 rhs)
		{
			return new bool3x2(lhs != rhs.c0, lhs != rhs.c1);
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x0004E41C File Offset: 0x0004C61C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator ~(int3x2 val)
		{
			return new int3x2(~val.c0, ~val.c1);
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x0004E439 File Offset: 0x0004C639
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator &(int3x2 lhs, int3x2 rhs)
		{
			return new int3x2(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1);
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x0004E462 File Offset: 0x0004C662
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator &(int3x2 lhs, int rhs)
		{
			return new int3x2(lhs.c0 & rhs, lhs.c1 & rhs);
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x0004E481 File Offset: 0x0004C681
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator &(int lhs, int3x2 rhs)
		{
			return new int3x2(lhs & rhs.c0, lhs & rhs.c1);
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x0004E4A0 File Offset: 0x0004C6A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator |(int3x2 lhs, int3x2 rhs)
		{
			return new int3x2(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1);
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x0004E4C9 File Offset: 0x0004C6C9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator |(int3x2 lhs, int rhs)
		{
			return new int3x2(lhs.c0 | rhs, lhs.c1 | rhs);
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x0004E4E8 File Offset: 0x0004C6E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator |(int lhs, int3x2 rhs)
		{
			return new int3x2(lhs | rhs.c0, lhs | rhs.c1);
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x0004E507 File Offset: 0x0004C707
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator ^(int3x2 lhs, int3x2 rhs)
		{
			return new int3x2(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1);
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x0004E530 File Offset: 0x0004C730
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator ^(int3x2 lhs, int rhs)
		{
			return new int3x2(lhs.c0 ^ rhs, lhs.c1 ^ rhs);
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x0004E54F File Offset: 0x0004C74F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 operator ^(int lhs, int3x2 rhs)
		{
			return new int3x2(lhs ^ rhs.c0, lhs ^ rhs.c1);
		}

		// Token: 0x17000846 RID: 2118
		public unsafe ref int3 this[int index]
		{
			get
			{
				fixed (int3x2* ptr = &this)
				{
					return ref *(int3*)(ptr + (IntPtr)index * (IntPtr)sizeof(int3) / (IntPtr)sizeof(int3x2));
				}
			}
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x0004E58B File Offset: 0x0004C78B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(int3x2 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1);
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x0004E5B4 File Offset: 0x0004C7B4
		public override bool Equals(object o)
		{
			if (o is int3x2)
			{
				int3x2 converted = (int3x2)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x0004E5D9 File Offset: 0x0004C7D9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x0004E5E8 File Offset: 0x0004C7E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("int3x2({0}, {1},  {2}, {3},  {4}, {5})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c0.y,
				this.c1.y,
				this.c0.z,
				this.c1.z
			});
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x0004E678 File Offset: 0x0004C878
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("int3x2({0}, {1},  {2}, {3},  {4}, {5})", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c0.z.ToString(format, formatProvider),
				this.c1.z.ToString(format, formatProvider)
			});
		}

		// Token: 0x04000112 RID: 274
		public int3 c0;

		// Token: 0x04000113 RID: 275
		public int3 c1;

		// Token: 0x04000114 RID: 276
		public static readonly int3x2 zero;
	}
}
