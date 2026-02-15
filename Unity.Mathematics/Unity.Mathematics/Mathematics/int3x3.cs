using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000049 RID: 73
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct int3x3 : IEquatable<int3x3>, IFormattable
	{
		// Token: 0x06001B0B RID: 6923 RVA: 0x0004E713 File Offset: 0x0004C913
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x3(int3 c0, int3 c1, int3 c2)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
		}

		// Token: 0x06001B0C RID: 6924 RVA: 0x0004E72A File Offset: 0x0004C92A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x3(int m00, int m01, int m02, int m10, int m11, int m12, int m20, int m21, int m22)
		{
			this.c0 = new int3(m00, m10, m20);
			this.c1 = new int3(m01, m11, m21);
			this.c2 = new int3(m02, m12, m22);
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x0004E75C File Offset: 0x0004C95C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x3(int v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x0004E784 File Offset: 0x0004C984
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x3(bool v)
		{
			this.c0 = math.select(new int3(0), new int3(1), v);
			this.c1 = math.select(new int3(0), new int3(1), v);
			this.c2 = math.select(new int3(0), new int3(1), v);
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x0004E7DC File Offset: 0x0004C9DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x3(bool3x3 v)
		{
			this.c0 = math.select(new int3(0), new int3(1), v.c0);
			this.c1 = math.select(new int3(0), new int3(1), v.c1);
			this.c2 = math.select(new int3(0), new int3(1), v.c2);
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x0004E840 File Offset: 0x0004CA40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x3(uint v)
		{
			this.c0 = (int3)v;
			this.c1 = (int3)v;
			this.c2 = (int3)v;
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x0004E866 File Offset: 0x0004CA66
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x3(uint3x3 v)
		{
			this.c0 = (int3)v.c0;
			this.c1 = (int3)v.c1;
			this.c2 = (int3)v.c2;
		}

		// Token: 0x06001B12 RID: 6930 RVA: 0x0004E89B File Offset: 0x0004CA9B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x3(float v)
		{
			this.c0 = (int3)v;
			this.c1 = (int3)v;
			this.c2 = (int3)v;
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x0004E8C1 File Offset: 0x0004CAC1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x3(float3x3 v)
		{
			this.c0 = (int3)v.c0;
			this.c1 = (int3)v.c1;
			this.c2 = (int3)v.c2;
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x0004E8F6 File Offset: 0x0004CAF6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x3(double v)
		{
			this.c0 = (int3)v;
			this.c1 = (int3)v;
			this.c2 = (int3)v;
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x0004E91C File Offset: 0x0004CB1C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x3(double3x3 v)
		{
			this.c0 = (int3)v.c0;
			this.c1 = (int3)v.c1;
			this.c2 = (int3)v.c2;
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x0000E6CC File Offset: 0x0000C8CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator int3x3(int v)
		{
			return new int3x3(v);
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x0000E6D4 File Offset: 0x0000C8D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x3(bool v)
		{
			return new int3x3(v);
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x0000E6DC File Offset: 0x0000C8DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x3(bool3x3 v)
		{
			return new int3x3(v);
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x0000E6E4 File Offset: 0x0000C8E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x3(uint v)
		{
			return new int3x3(v);
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x0000E6EC File Offset: 0x0000C8EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x3(uint3x3 v)
		{
			return new int3x3(v);
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x0000E6F4 File Offset: 0x0000C8F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x3(float v)
		{
			return new int3x3(v);
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x0000E6FC File Offset: 0x0000C8FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x3(float3x3 v)
		{
			return new int3x3(v);
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x0000E704 File Offset: 0x0000C904
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x3(double v)
		{
			return new int3x3(v);
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x0000E70C File Offset: 0x0000C90C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x3(double3x3 v)
		{
			return new int3x3(v);
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x0004E951 File Offset: 0x0004CB51
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator *(int3x3 lhs, int3x3 rhs)
		{
			return new int3x3(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2);
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x0004E98B File Offset: 0x0004CB8B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator *(int3x3 lhs, int rhs)
		{
			return new int3x3(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs);
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x0004E9B6 File Offset: 0x0004CBB6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator *(int lhs, int3x3 rhs)
		{
			return new int3x3(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2);
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x0004E9E1 File Offset: 0x0004CBE1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator +(int3x3 lhs, int3x3 rhs)
		{
			return new int3x3(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2);
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x0004EA1B File Offset: 0x0004CC1B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator +(int3x3 lhs, int rhs)
		{
			return new int3x3(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs);
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x0004EA46 File Offset: 0x0004CC46
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator +(int lhs, int3x3 rhs)
		{
			return new int3x3(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2);
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x0004EA71 File Offset: 0x0004CC71
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator -(int3x3 lhs, int3x3 rhs)
		{
			return new int3x3(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2);
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x0004EAAB File Offset: 0x0004CCAB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator -(int3x3 lhs, int rhs)
		{
			return new int3x3(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs);
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x0004EAD6 File Offset: 0x0004CCD6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator -(int lhs, int3x3 rhs)
		{
			return new int3x3(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2);
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x0004EB01 File Offset: 0x0004CD01
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator /(int3x3 lhs, int3x3 rhs)
		{
			return new int3x3(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2);
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x0004EB3B File Offset: 0x0004CD3B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator /(int3x3 lhs, int rhs)
		{
			return new int3x3(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs);
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x0004EB66 File Offset: 0x0004CD66
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator /(int lhs, int3x3 rhs)
		{
			return new int3x3(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2);
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x0004EB91 File Offset: 0x0004CD91
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator %(int3x3 lhs, int3x3 rhs)
		{
			return new int3x3(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2);
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x0004EBCB File Offset: 0x0004CDCB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator %(int3x3 lhs, int rhs)
		{
			return new int3x3(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs);
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x0004EBF6 File Offset: 0x0004CDF6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator %(int lhs, int3x3 rhs)
		{
			return new int3x3(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2);
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x0004EC24 File Offset: 0x0004CE24
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator ++(int3x3 val)
		{
			int3 @int = int3.op_Increment(val.c0);
			val.c0 = @int;
			int3 int2 = @int;
			@int = int3.op_Increment(val.c1);
			val.c1 = @int;
			int3 int3 = @int;
			@int = int3.op_Increment(val.c2);
			val.c2 = @int;
			return new int3x3(int2, int3, @int);
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x0004EC84 File Offset: 0x0004CE84
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator --(int3x3 val)
		{
			int3 @int = int3.op_Decrement(val.c0);
			val.c0 = @int;
			int3 int2 = @int;
			@int = int3.op_Decrement(val.c1);
			val.c1 = @int;
			int3 int3 = @int;
			@int = int3.op_Decrement(val.c2);
			val.c2 = @int;
			return new int3x3(int2, int3, @int);
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x0004ECE4 File Offset: 0x0004CEE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator <(int3x3 lhs, int3x3 rhs)
		{
			return new bool3x3(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2);
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x0004ED1E File Offset: 0x0004CF1E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator <(int3x3 lhs, int rhs)
		{
			return new bool3x3(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs);
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x0004ED49 File Offset: 0x0004CF49
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator <(int lhs, int3x3 rhs)
		{
			return new bool3x3(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2);
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x0004ED74 File Offset: 0x0004CF74
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator <=(int3x3 lhs, int3x3 rhs)
		{
			return new bool3x3(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2);
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x0004EDAE File Offset: 0x0004CFAE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator <=(int3x3 lhs, int rhs)
		{
			return new bool3x3(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs);
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x0004EDD9 File Offset: 0x0004CFD9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator <=(int lhs, int3x3 rhs)
		{
			return new bool3x3(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2);
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x0004EE04 File Offset: 0x0004D004
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator >(int3x3 lhs, int3x3 rhs)
		{
			return new bool3x3(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2);
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x0004EE3E File Offset: 0x0004D03E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator >(int3x3 lhs, int rhs)
		{
			return new bool3x3(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs);
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x0004EE69 File Offset: 0x0004D069
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator >(int lhs, int3x3 rhs)
		{
			return new bool3x3(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2);
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x0004EE94 File Offset: 0x0004D094
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator >=(int3x3 lhs, int3x3 rhs)
		{
			return new bool3x3(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2);
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x0004EECE File Offset: 0x0004D0CE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator >=(int3x3 lhs, int rhs)
		{
			return new bool3x3(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs);
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x0004EEF9 File Offset: 0x0004D0F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator >=(int lhs, int3x3 rhs)
		{
			return new bool3x3(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2);
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x0004EF24 File Offset: 0x0004D124
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator -(int3x3 val)
		{
			return new int3x3(-val.c0, -val.c1, -val.c2);
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x0004EF4C File Offset: 0x0004D14C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator +(int3x3 val)
		{
			return new int3x3(+val.c0, +val.c1, +val.c2);
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x0004EF74 File Offset: 0x0004D174
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator <<(int3x3 x, int n)
		{
			return new int3x3(x.c0 << n, x.c1 << n, x.c2 << n);
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x0004EF9F File Offset: 0x0004D19F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator >>(int3x3 x, int n)
		{
			return new int3x3(x.c0 >> n, x.c1 >> n, x.c2 >> n);
		}

		// Token: 0x06001B40 RID: 6976 RVA: 0x0004EFCA File Offset: 0x0004D1CA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator ==(int3x3 lhs, int3x3 rhs)
		{
			return new bool3x3(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2);
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x0004F004 File Offset: 0x0004D204
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator ==(int3x3 lhs, int rhs)
		{
			return new bool3x3(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs);
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x0004F02F File Offset: 0x0004D22F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator ==(int lhs, int3x3 rhs)
		{
			return new bool3x3(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2);
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x0004F05A File Offset: 0x0004D25A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator !=(int3x3 lhs, int3x3 rhs)
		{
			return new bool3x3(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2);
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x0004F094 File Offset: 0x0004D294
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator !=(int3x3 lhs, int rhs)
		{
			return new bool3x3(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs);
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x0004F0BF File Offset: 0x0004D2BF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator !=(int lhs, int3x3 rhs)
		{
			return new bool3x3(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2);
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x0004F0EA File Offset: 0x0004D2EA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator ~(int3x3 val)
		{
			return new int3x3(~val.c0, ~val.c1, ~val.c2);
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x0004F112 File Offset: 0x0004D312
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator &(int3x3 lhs, int3x3 rhs)
		{
			return new int3x3(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1, lhs.c2 & rhs.c2);
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x0004F14C File Offset: 0x0004D34C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator &(int3x3 lhs, int rhs)
		{
			return new int3x3(lhs.c0 & rhs, lhs.c1 & rhs, lhs.c2 & rhs);
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x0004F177 File Offset: 0x0004D377
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator &(int lhs, int3x3 rhs)
		{
			return new int3x3(lhs & rhs.c0, lhs & rhs.c1, lhs & rhs.c2);
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x0004F1A2 File Offset: 0x0004D3A2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator |(int3x3 lhs, int3x3 rhs)
		{
			return new int3x3(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1, lhs.c2 | rhs.c2);
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x0004F1DC File Offset: 0x0004D3DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator |(int3x3 lhs, int rhs)
		{
			return new int3x3(lhs.c0 | rhs, lhs.c1 | rhs, lhs.c2 | rhs);
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x0004F207 File Offset: 0x0004D407
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator |(int lhs, int3x3 rhs)
		{
			return new int3x3(lhs | rhs.c0, lhs | rhs.c1, lhs | rhs.c2);
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x0004F232 File Offset: 0x0004D432
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator ^(int3x3 lhs, int3x3 rhs)
		{
			return new int3x3(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1, lhs.c2 ^ rhs.c2);
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x0004F26C File Offset: 0x0004D46C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator ^(int3x3 lhs, int rhs)
		{
			return new int3x3(lhs.c0 ^ rhs, lhs.c1 ^ rhs, lhs.c2 ^ rhs);
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x0004F297 File Offset: 0x0004D497
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 operator ^(int lhs, int3x3 rhs)
		{
			return new int3x3(lhs ^ rhs.c0, lhs ^ rhs.c1, lhs ^ rhs.c2);
		}

		// Token: 0x17000847 RID: 2119
		public unsafe ref int3 this[int index]
		{
			get
			{
				fixed (int3x3* ptr = &this)
				{
					return ref *(int3*)(ptr + (IntPtr)index * (IntPtr)sizeof(int3) / (IntPtr)sizeof(int3x3));
				}
			}
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x0004F2DF File Offset: 0x0004D4DF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(int3x3 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2);
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x0004F31C File Offset: 0x0004D51C
		public override bool Equals(object o)
		{
			if (o is int3x3)
			{
				int3x3 converted = (int3x3)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x0004F341 File Offset: 0x0004D541
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x0004F350 File Offset: 0x0004D550
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("int3x3({0}, {1}, {2},  {3}, {4}, {5},  {6}, {7}, {8})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c2.x,
				this.c0.y,
				this.c1.y,
				this.c2.y,
				this.c0.z,
				this.c1.z,
				this.c2.z
			});
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x0004F41C File Offset: 0x0004D61C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("int3x3({0}, {1}, {2},  {3}, {4}, {5},  {6}, {7}, {8})", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c2.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c2.y.ToString(format, formatProvider),
				this.c0.z.ToString(format, formatProvider),
				this.c1.z.ToString(format, formatProvider),
				this.c2.z.ToString(format, formatProvider)
			});
		}

		// Token: 0x04000115 RID: 277
		public int3 c0;

		// Token: 0x04000116 RID: 278
		public int3 c1;

		// Token: 0x04000117 RID: 279
		public int3 c2;

		// Token: 0x04000118 RID: 280
		public static readonly int3x3 identity = new int3x3(1, 0, 0, 0, 1, 0, 0, 0, 1);

		// Token: 0x04000119 RID: 281
		public static readonly int3x3 zero;
	}
}
