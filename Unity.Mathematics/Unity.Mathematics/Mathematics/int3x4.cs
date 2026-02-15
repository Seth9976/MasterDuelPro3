using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200004A RID: 74
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct int3x4 : IEquatable<int3x4>, IFormattable
	{
		// Token: 0x06001B57 RID: 6999 RVA: 0x0004F518 File Offset: 0x0004D718
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x4(int3 c0, int3 c1, int3 c2, int3 c3)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
			this.c3 = c3;
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x0004F538 File Offset: 0x0004D738
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x4(int m00, int m01, int m02, int m03, int m10, int m11, int m12, int m13, int m20, int m21, int m22, int m23)
		{
			this.c0 = new int3(m00, m10, m20);
			this.c1 = new int3(m01, m11, m21);
			this.c2 = new int3(m02, m12, m22);
			this.c3 = new int3(m03, m13, m23);
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x0004F586 File Offset: 0x0004D786
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x4(int v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x0004F5B8 File Offset: 0x0004D7B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x4(bool v)
		{
			this.c0 = math.select(new int3(0), new int3(1), v);
			this.c1 = math.select(new int3(0), new int3(1), v);
			this.c2 = math.select(new int3(0), new int3(1), v);
			this.c3 = math.select(new int3(0), new int3(1), v);
		}

		// Token: 0x06001B5B RID: 7003 RVA: 0x0004F628 File Offset: 0x0004D828
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x4(bool3x4 v)
		{
			this.c0 = math.select(new int3(0), new int3(1), v.c0);
			this.c1 = math.select(new int3(0), new int3(1), v.c1);
			this.c2 = math.select(new int3(0), new int3(1), v.c2);
			this.c3 = math.select(new int3(0), new int3(1), v.c3);
		}

		// Token: 0x06001B5C RID: 7004 RVA: 0x0004F6A9 File Offset: 0x0004D8A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x4(uint v)
		{
			this.c0 = (int3)v;
			this.c1 = (int3)v;
			this.c2 = (int3)v;
			this.c3 = (int3)v;
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x0004F6DC File Offset: 0x0004D8DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x4(uint3x4 v)
		{
			this.c0 = (int3)v.c0;
			this.c1 = (int3)v.c1;
			this.c2 = (int3)v.c2;
			this.c3 = (int3)v.c3;
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x0004F72D File Offset: 0x0004D92D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x4(float v)
		{
			this.c0 = (int3)v;
			this.c1 = (int3)v;
			this.c2 = (int3)v;
			this.c3 = (int3)v;
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x0004F760 File Offset: 0x0004D960
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x4(float3x4 v)
		{
			this.c0 = (int3)v.c0;
			this.c1 = (int3)v.c1;
			this.c2 = (int3)v.c2;
			this.c3 = (int3)v.c3;
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x0004F7B1 File Offset: 0x0004D9B1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x4(double v)
		{
			this.c0 = (int3)v;
			this.c1 = (int3)v;
			this.c2 = (int3)v;
			this.c3 = (int3)v;
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x0004F7E4 File Offset: 0x0004D9E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3x4(double3x4 v)
		{
			this.c0 = (int3)v.c0;
			this.c1 = (int3)v.c1;
			this.c2 = (int3)v.c2;
			this.c3 = (int3)v.c3;
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x0000E96E File Offset: 0x0000CB6E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator int3x4(int v)
		{
			return new int3x4(v);
		}

		// Token: 0x06001B63 RID: 7011 RVA: 0x0000E976 File Offset: 0x0000CB76
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x4(bool v)
		{
			return new int3x4(v);
		}

		// Token: 0x06001B64 RID: 7012 RVA: 0x0000E97E File Offset: 0x0000CB7E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x4(bool3x4 v)
		{
			return new int3x4(v);
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x0000E986 File Offset: 0x0000CB86
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x4(uint v)
		{
			return new int3x4(v);
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x0000E98E File Offset: 0x0000CB8E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x4(uint3x4 v)
		{
			return new int3x4(v);
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x0000E996 File Offset: 0x0000CB96
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x4(float v)
		{
			return new int3x4(v);
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x0000E99E File Offset: 0x0000CB9E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x4(float3x4 v)
		{
			return new int3x4(v);
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x0000E9A6 File Offset: 0x0000CBA6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x4(double v)
		{
			return new int3x4(v);
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x0000E9AE File Offset: 0x0000CBAE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int3x4(double3x4 v)
		{
			return new int3x4(v);
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x0004F838 File Offset: 0x0004DA38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator *(int3x4 lhs, int3x4 rhs)
		{
			return new int3x4(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2, lhs.c3 * rhs.c3);
		}

		// Token: 0x06001B6C RID: 7020 RVA: 0x0004F88E File Offset: 0x0004DA8E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator *(int3x4 lhs, int rhs)
		{
			return new int3x4(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs, lhs.c3 * rhs);
		}

		// Token: 0x06001B6D RID: 7021 RVA: 0x0004F8C5 File Offset: 0x0004DAC5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator *(int lhs, int3x4 rhs)
		{
			return new int3x4(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2, lhs * rhs.c3);
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x0004F8FC File Offset: 0x0004DAFC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator +(int3x4 lhs, int3x4 rhs)
		{
			return new int3x4(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2, lhs.c3 + rhs.c3);
		}

		// Token: 0x06001B6F RID: 7023 RVA: 0x0004F952 File Offset: 0x0004DB52
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator +(int3x4 lhs, int rhs)
		{
			return new int3x4(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs, lhs.c3 + rhs);
		}

		// Token: 0x06001B70 RID: 7024 RVA: 0x0004F989 File Offset: 0x0004DB89
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator +(int lhs, int3x4 rhs)
		{
			return new int3x4(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2, lhs + rhs.c3);
		}

		// Token: 0x06001B71 RID: 7025 RVA: 0x0004F9C0 File Offset: 0x0004DBC0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator -(int3x4 lhs, int3x4 rhs)
		{
			return new int3x4(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2, lhs.c3 - rhs.c3);
		}

		// Token: 0x06001B72 RID: 7026 RVA: 0x0004FA16 File Offset: 0x0004DC16
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator -(int3x4 lhs, int rhs)
		{
			return new int3x4(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs, lhs.c3 - rhs);
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x0004FA4D File Offset: 0x0004DC4D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator -(int lhs, int3x4 rhs)
		{
			return new int3x4(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2, lhs - rhs.c3);
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x0004FA84 File Offset: 0x0004DC84
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator /(int3x4 lhs, int3x4 rhs)
		{
			return new int3x4(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2, lhs.c3 / rhs.c3);
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x0004FADA File Offset: 0x0004DCDA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator /(int3x4 lhs, int rhs)
		{
			return new int3x4(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs, lhs.c3 / rhs);
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x0004FB11 File Offset: 0x0004DD11
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator /(int lhs, int3x4 rhs)
		{
			return new int3x4(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2, lhs / rhs.c3);
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x0004FB48 File Offset: 0x0004DD48
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator %(int3x4 lhs, int3x4 rhs)
		{
			return new int3x4(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2, lhs.c3 % rhs.c3);
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x0004FB9E File Offset: 0x0004DD9E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator %(int3x4 lhs, int rhs)
		{
			return new int3x4(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs, lhs.c3 % rhs);
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x0004FBD5 File Offset: 0x0004DDD5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator %(int lhs, int3x4 rhs)
		{
			return new int3x4(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2, lhs % rhs.c3);
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x0004FC0C File Offset: 0x0004DE0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator ++(int3x4 val)
		{
			int3 @int = int3.op_Increment(val.c0);
			val.c0 = @int;
			int3 int2 = @int;
			@int = int3.op_Increment(val.c1);
			val.c1 = @int;
			int3 int3 = @int;
			@int = int3.op_Increment(val.c2);
			val.c2 = @int;
			int3 int4 = @int;
			@int = int3.op_Increment(val.c3);
			val.c3 = @int;
			return new int3x4(int2, int3, int4, @int);
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x0004FC88 File Offset: 0x0004DE88
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator --(int3x4 val)
		{
			int3 @int = int3.op_Decrement(val.c0);
			val.c0 = @int;
			int3 int2 = @int;
			@int = int3.op_Decrement(val.c1);
			val.c1 = @int;
			int3 int3 = @int;
			@int = int3.op_Decrement(val.c2);
			val.c2 = @int;
			int3 int4 = @int;
			@int = int3.op_Decrement(val.c3);
			val.c3 = @int;
			return new int3x4(int2, int3, int4, @int);
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x0004FD04 File Offset: 0x0004DF04
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <(int3x4 lhs, int3x4 rhs)
		{
			return new bool3x4(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2, lhs.c3 < rhs.c3);
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x0004FD5A File Offset: 0x0004DF5A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <(int3x4 lhs, int rhs)
		{
			return new bool3x4(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs, lhs.c3 < rhs);
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x0004FD91 File Offset: 0x0004DF91
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <(int lhs, int3x4 rhs)
		{
			return new bool3x4(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2, lhs < rhs.c3);
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x0004FDC8 File Offset: 0x0004DFC8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <=(int3x4 lhs, int3x4 rhs)
		{
			return new bool3x4(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2, lhs.c3 <= rhs.c3);
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x0004FE1E File Offset: 0x0004E01E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <=(int3x4 lhs, int rhs)
		{
			return new bool3x4(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs, lhs.c3 <= rhs);
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x0004FE55 File Offset: 0x0004E055
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <=(int lhs, int3x4 rhs)
		{
			return new bool3x4(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2, lhs <= rhs.c3);
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x0004FE8C File Offset: 0x0004E08C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >(int3x4 lhs, int3x4 rhs)
		{
			return new bool3x4(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2, lhs.c3 > rhs.c3);
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x0004FEE2 File Offset: 0x0004E0E2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >(int3x4 lhs, int rhs)
		{
			return new bool3x4(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs, lhs.c3 > rhs);
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x0004FF19 File Offset: 0x0004E119
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >(int lhs, int3x4 rhs)
		{
			return new bool3x4(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2, lhs > rhs.c3);
		}

		// Token: 0x06001B85 RID: 7045 RVA: 0x0004FF50 File Offset: 0x0004E150
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >=(int3x4 lhs, int3x4 rhs)
		{
			return new bool3x4(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2, lhs.c3 >= rhs.c3);
		}

		// Token: 0x06001B86 RID: 7046 RVA: 0x0004FFA6 File Offset: 0x0004E1A6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >=(int3x4 lhs, int rhs)
		{
			return new bool3x4(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs, lhs.c3 >= rhs);
		}

		// Token: 0x06001B87 RID: 7047 RVA: 0x0004FFDD File Offset: 0x0004E1DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >=(int lhs, int3x4 rhs)
		{
			return new bool3x4(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2, lhs >= rhs.c3);
		}

		// Token: 0x06001B88 RID: 7048 RVA: 0x00050014 File Offset: 0x0004E214
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator -(int3x4 val)
		{
			return new int3x4(-val.c0, -val.c1, -val.c2, -val.c3);
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x00050047 File Offset: 0x0004E247
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator +(int3x4 val)
		{
			return new int3x4(+val.c0, +val.c1, +val.c2, +val.c3);
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x0005007A File Offset: 0x0004E27A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator <<(int3x4 x, int n)
		{
			return new int3x4(x.c0 << n, x.c1 << n, x.c2 << n, x.c3 << n);
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x000500B1 File Offset: 0x0004E2B1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator >>(int3x4 x, int n)
		{
			return new int3x4(x.c0 >> n, x.c1 >> n, x.c2 >> n, x.c3 >> n);
		}

		// Token: 0x06001B8C RID: 7052 RVA: 0x000500E8 File Offset: 0x0004E2E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator ==(int3x4 lhs, int3x4 rhs)
		{
			return new bool3x4(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2, lhs.c3 == rhs.c3);
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x0005013E File Offset: 0x0004E33E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator ==(int3x4 lhs, int rhs)
		{
			return new bool3x4(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs, lhs.c3 == rhs);
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x00050175 File Offset: 0x0004E375
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator ==(int lhs, int3x4 rhs)
		{
			return new bool3x4(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2, lhs == rhs.c3);
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x000501AC File Offset: 0x0004E3AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator !=(int3x4 lhs, int3x4 rhs)
		{
			return new bool3x4(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2, lhs.c3 != rhs.c3);
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x00050202 File Offset: 0x0004E402
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator !=(int3x4 lhs, int rhs)
		{
			return new bool3x4(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs, lhs.c3 != rhs);
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x00050239 File Offset: 0x0004E439
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator !=(int lhs, int3x4 rhs)
		{
			return new bool3x4(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2, lhs != rhs.c3);
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x00050270 File Offset: 0x0004E470
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator ~(int3x4 val)
		{
			return new int3x4(~val.c0, ~val.c1, ~val.c2, ~val.c3);
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x000502A4 File Offset: 0x0004E4A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator &(int3x4 lhs, int3x4 rhs)
		{
			return new int3x4(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1, lhs.c2 & rhs.c2, lhs.c3 & rhs.c3);
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x000502FA File Offset: 0x0004E4FA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator &(int3x4 lhs, int rhs)
		{
			return new int3x4(lhs.c0 & rhs, lhs.c1 & rhs, lhs.c2 & rhs, lhs.c3 & rhs);
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x00050331 File Offset: 0x0004E531
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator &(int lhs, int3x4 rhs)
		{
			return new int3x4(lhs & rhs.c0, lhs & rhs.c1, lhs & rhs.c2, lhs & rhs.c3);
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x00050368 File Offset: 0x0004E568
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator |(int3x4 lhs, int3x4 rhs)
		{
			return new int3x4(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1, lhs.c2 | rhs.c2, lhs.c3 | rhs.c3);
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x000503BE File Offset: 0x0004E5BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator |(int3x4 lhs, int rhs)
		{
			return new int3x4(lhs.c0 | rhs, lhs.c1 | rhs, lhs.c2 | rhs, lhs.c3 | rhs);
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x000503F5 File Offset: 0x0004E5F5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator |(int lhs, int3x4 rhs)
		{
			return new int3x4(lhs | rhs.c0, lhs | rhs.c1, lhs | rhs.c2, lhs | rhs.c3);
		}

		// Token: 0x06001B99 RID: 7065 RVA: 0x0005042C File Offset: 0x0004E62C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator ^(int3x4 lhs, int3x4 rhs)
		{
			return new int3x4(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1, lhs.c2 ^ rhs.c2, lhs.c3 ^ rhs.c3);
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x00050482 File Offset: 0x0004E682
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator ^(int3x4 lhs, int rhs)
		{
			return new int3x4(lhs.c0 ^ rhs, lhs.c1 ^ rhs, lhs.c2 ^ rhs, lhs.c3 ^ rhs);
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x000504B9 File Offset: 0x0004E6B9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 operator ^(int lhs, int3x4 rhs)
		{
			return new int3x4(lhs ^ rhs.c0, lhs ^ rhs.c1, lhs ^ rhs.c2, lhs ^ rhs.c3);
		}

		// Token: 0x17000848 RID: 2120
		public unsafe ref int3 this[int index]
		{
			get
			{
				fixed (int3x4* ptr = &this)
				{
					return ref *(int3*)(ptr + (IntPtr)index * (IntPtr)sizeof(int3) / (IntPtr)sizeof(int3x4));
				}
			}
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x0005050C File Offset: 0x0004E70C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(int3x4 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2) && this.c3.Equals(rhs.c3);
		}

		// Token: 0x06001B9E RID: 7070 RVA: 0x00050568 File Offset: 0x0004E768
		public override bool Equals(object o)
		{
			if (o is int3x4)
			{
				int3x4 converted = (int3x4)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x0005058D File Offset: 0x0004E78D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x0005059C File Offset: 0x0004E79C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("int3x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7},  {8}, {9}, {10}, {11})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c2.x,
				this.c3.x,
				this.c0.y,
				this.c1.y,
				this.c2.y,
				this.c3.y,
				this.c0.z,
				this.c1.z,
				this.c2.z,
				this.c3.z
			});
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x000506A4 File Offset: 0x0004E8A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("int3x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7},  {8}, {9}, {10}, {11})", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c2.x.ToString(format, formatProvider),
				this.c3.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c2.y.ToString(format, formatProvider),
				this.c3.y.ToString(format, formatProvider),
				this.c0.z.ToString(format, formatProvider),
				this.c1.z.ToString(format, formatProvider),
				this.c2.z.ToString(format, formatProvider),
				this.c3.z.ToString(format, formatProvider)
			});
		}

		// Token: 0x0400011A RID: 282
		public int3 c0;

		// Token: 0x0400011B RID: 283
		public int3 c1;

		// Token: 0x0400011C RID: 284
		public int3 c2;

		// Token: 0x0400011D RID: 285
		public int3 c3;

		// Token: 0x0400011E RID: 286
		public static readonly int3x4 zero;
	}
}
