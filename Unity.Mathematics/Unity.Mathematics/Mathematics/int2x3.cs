using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000044 RID: 68
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct int2x3 : IEquatable<int2x3>, IFormattable
	{
		// Token: 0x0600195A RID: 6490 RVA: 0x0004A545 File Offset: 0x00048745
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x3(int2 c0, int2 c1, int2 c2)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x0004A55C File Offset: 0x0004875C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x3(int m00, int m01, int m02, int m10, int m11, int m12)
		{
			this.c0 = new int2(m00, m10);
			this.c1 = new int2(m01, m11);
			this.c2 = new int2(m02, m12);
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x0004A588 File Offset: 0x00048788
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x3(int v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x0004A5B0 File Offset: 0x000487B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x3(bool v)
		{
			this.c0 = math.select(new int2(0), new int2(1), v);
			this.c1 = math.select(new int2(0), new int2(1), v);
			this.c2 = math.select(new int2(0), new int2(1), v);
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x0004A608 File Offset: 0x00048808
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x3(bool2x3 v)
		{
			this.c0 = math.select(new int2(0), new int2(1), v.c0);
			this.c1 = math.select(new int2(0), new int2(1), v.c1);
			this.c2 = math.select(new int2(0), new int2(1), v.c2);
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x0004A66C File Offset: 0x0004886C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x3(uint v)
		{
			this.c0 = (int2)v;
			this.c1 = (int2)v;
			this.c2 = (int2)v;
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x0004A692 File Offset: 0x00048892
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x3(uint2x3 v)
		{
			this.c0 = (int2)v.c0;
			this.c1 = (int2)v.c1;
			this.c2 = (int2)v.c2;
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x0004A6C7 File Offset: 0x000488C7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x3(float v)
		{
			this.c0 = (int2)v;
			this.c1 = (int2)v;
			this.c2 = (int2)v;
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x0004A6ED File Offset: 0x000488ED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x3(float2x3 v)
		{
			this.c0 = (int2)v.c0;
			this.c1 = (int2)v.c1;
			this.c2 = (int2)v.c2;
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x0004A722 File Offset: 0x00048922
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x3(double v)
		{
			this.c0 = (int2)v;
			this.c1 = (int2)v;
			this.c2 = (int2)v;
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x0004A748 File Offset: 0x00048948
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x3(double2x3 v)
		{
			this.c0 = (int2)v.c0;
			this.c1 = (int2)v.c1;
			this.c2 = (int2)v.c2;
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x0000DFC7 File Offset: 0x0000C1C7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator int2x3(int v)
		{
			return new int2x3(v);
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x0000DFCF File Offset: 0x0000C1CF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x3(bool v)
		{
			return new int2x3(v);
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x0000DFD7 File Offset: 0x0000C1D7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x3(bool2x3 v)
		{
			return new int2x3(v);
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x0000DFDF File Offset: 0x0000C1DF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x3(uint v)
		{
			return new int2x3(v);
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x0000DFE7 File Offset: 0x0000C1E7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x3(uint2x3 v)
		{
			return new int2x3(v);
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x0000DFEF File Offset: 0x0000C1EF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x3(float v)
		{
			return new int2x3(v);
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x0000DFF7 File Offset: 0x0000C1F7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x3(float2x3 v)
		{
			return new int2x3(v);
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x0000DFFF File Offset: 0x0000C1FF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x3(double v)
		{
			return new int2x3(v);
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x0000E007 File Offset: 0x0000C207
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x3(double2x3 v)
		{
			return new int2x3(v);
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x0004A77D File Offset: 0x0004897D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator *(int2x3 lhs, int2x3 rhs)
		{
			return new int2x3(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2);
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x0004A7B7 File Offset: 0x000489B7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator *(int2x3 lhs, int rhs)
		{
			return new int2x3(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs);
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x0004A7E2 File Offset: 0x000489E2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator *(int lhs, int2x3 rhs)
		{
			return new int2x3(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2);
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x0004A80D File Offset: 0x00048A0D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator +(int2x3 lhs, int2x3 rhs)
		{
			return new int2x3(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2);
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x0004A847 File Offset: 0x00048A47
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator +(int2x3 lhs, int rhs)
		{
			return new int2x3(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs);
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x0004A872 File Offset: 0x00048A72
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator +(int lhs, int2x3 rhs)
		{
			return new int2x3(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2);
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x0004A89D File Offset: 0x00048A9D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator -(int2x3 lhs, int2x3 rhs)
		{
			return new int2x3(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2);
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x0004A8D7 File Offset: 0x00048AD7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator -(int2x3 lhs, int rhs)
		{
			return new int2x3(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs);
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x0004A902 File Offset: 0x00048B02
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator -(int lhs, int2x3 rhs)
		{
			return new int2x3(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2);
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x0004A92D File Offset: 0x00048B2D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator /(int2x3 lhs, int2x3 rhs)
		{
			return new int2x3(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2);
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x0004A967 File Offset: 0x00048B67
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator /(int2x3 lhs, int rhs)
		{
			return new int2x3(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs);
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x0004A992 File Offset: 0x00048B92
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator /(int lhs, int2x3 rhs)
		{
			return new int2x3(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2);
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x0004A9BD File Offset: 0x00048BBD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator %(int2x3 lhs, int2x3 rhs)
		{
			return new int2x3(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2);
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x0004A9F7 File Offset: 0x00048BF7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator %(int2x3 lhs, int rhs)
		{
			return new int2x3(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs);
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x0004AA22 File Offset: 0x00048C22
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator %(int lhs, int2x3 rhs)
		{
			return new int2x3(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2);
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x0004AA50 File Offset: 0x00048C50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator ++(int2x3 val)
		{
			int2 @int = int2.op_Increment(val.c0);
			val.c0 = @int;
			int2 int2 = @int;
			@int = int2.op_Increment(val.c1);
			val.c1 = @int;
			int2 int3 = @int;
			@int = int2.op_Increment(val.c2);
			val.c2 = @int;
			return new int2x3(int2, int3, @int);
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x0004AAB0 File Offset: 0x00048CB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator --(int2x3 val)
		{
			int2 @int = int2.op_Decrement(val.c0);
			val.c0 = @int;
			int2 int2 = @int;
			@int = int2.op_Decrement(val.c1);
			val.c1 = @int;
			int2 int3 = @int;
			@int = int2.op_Decrement(val.c2);
			val.c2 = @int;
			return new int2x3(int2, int3, @int);
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x0004AB10 File Offset: 0x00048D10
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <(int2x3 lhs, int2x3 rhs)
		{
			return new bool2x3(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2);
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x0004AB4A File Offset: 0x00048D4A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <(int2x3 lhs, int rhs)
		{
			return new bool2x3(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs);
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x0004AB75 File Offset: 0x00048D75
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <(int lhs, int2x3 rhs)
		{
			return new bool2x3(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2);
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x0004ABA0 File Offset: 0x00048DA0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <=(int2x3 lhs, int2x3 rhs)
		{
			return new bool2x3(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2);
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x0004ABDA File Offset: 0x00048DDA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <=(int2x3 lhs, int rhs)
		{
			return new bool2x3(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs);
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x0004AC05 File Offset: 0x00048E05
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <=(int lhs, int2x3 rhs)
		{
			return new bool2x3(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2);
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x0004AC30 File Offset: 0x00048E30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >(int2x3 lhs, int2x3 rhs)
		{
			return new bool2x3(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2);
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x0004AC6A File Offset: 0x00048E6A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >(int2x3 lhs, int rhs)
		{
			return new bool2x3(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs);
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x0004AC95 File Offset: 0x00048E95
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >(int lhs, int2x3 rhs)
		{
			return new bool2x3(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2);
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x0004ACC0 File Offset: 0x00048EC0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >=(int2x3 lhs, int2x3 rhs)
		{
			return new bool2x3(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2);
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x0004ACFA File Offset: 0x00048EFA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >=(int2x3 lhs, int rhs)
		{
			return new bool2x3(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs);
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x0004AD25 File Offset: 0x00048F25
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >=(int lhs, int2x3 rhs)
		{
			return new bool2x3(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2);
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x0004AD50 File Offset: 0x00048F50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator -(int2x3 val)
		{
			return new int2x3(-val.c0, -val.c1, -val.c2);
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x0004AD78 File Offset: 0x00048F78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator +(int2x3 val)
		{
			return new int2x3(+val.c0, +val.c1, +val.c2);
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x0004ADA0 File Offset: 0x00048FA0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator <<(int2x3 x, int n)
		{
			return new int2x3(x.c0 << n, x.c1 << n, x.c2 << n);
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x0004ADCB File Offset: 0x00048FCB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator >>(int2x3 x, int n)
		{
			return new int2x3(x.c0 >> n, x.c1 >> n, x.c2 >> n);
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x0004ADF6 File Offset: 0x00048FF6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator ==(int2x3 lhs, int2x3 rhs)
		{
			return new bool2x3(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2);
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x0004AE30 File Offset: 0x00049030
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator ==(int2x3 lhs, int rhs)
		{
			return new bool2x3(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs);
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x0004AE5B File Offset: 0x0004905B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator ==(int lhs, int2x3 rhs)
		{
			return new bool2x3(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2);
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x0004AE86 File Offset: 0x00049086
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator !=(int2x3 lhs, int2x3 rhs)
		{
			return new bool2x3(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2);
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x0004AEC0 File Offset: 0x000490C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator !=(int2x3 lhs, int rhs)
		{
			return new bool2x3(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs);
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x0004AEEB File Offset: 0x000490EB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator !=(int lhs, int2x3 rhs)
		{
			return new bool2x3(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2);
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x0004AF16 File Offset: 0x00049116
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator ~(int2x3 val)
		{
			return new int2x3(~val.c0, ~val.c1, ~val.c2);
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x0004AF3E File Offset: 0x0004913E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator &(int2x3 lhs, int2x3 rhs)
		{
			return new int2x3(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1, lhs.c2 & rhs.c2);
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x0004AF78 File Offset: 0x00049178
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator &(int2x3 lhs, int rhs)
		{
			return new int2x3(lhs.c0 & rhs, lhs.c1 & rhs, lhs.c2 & rhs);
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x0004AFA3 File Offset: 0x000491A3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator &(int lhs, int2x3 rhs)
		{
			return new int2x3(lhs & rhs.c0, lhs & rhs.c1, lhs & rhs.c2);
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x0004AFCE File Offset: 0x000491CE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator |(int2x3 lhs, int2x3 rhs)
		{
			return new int2x3(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1, lhs.c2 | rhs.c2);
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x0004B008 File Offset: 0x00049208
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator |(int2x3 lhs, int rhs)
		{
			return new int2x3(lhs.c0 | rhs, lhs.c1 | rhs, lhs.c2 | rhs);
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x0004B033 File Offset: 0x00049233
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator |(int lhs, int2x3 rhs)
		{
			return new int2x3(lhs | rhs.c0, lhs | rhs.c1, lhs | rhs.c2);
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x0004B05E File Offset: 0x0004925E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator ^(int2x3 lhs, int2x3 rhs)
		{
			return new int2x3(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1, lhs.c2 ^ rhs.c2);
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x0004B098 File Offset: 0x00049298
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator ^(int2x3 lhs, int rhs)
		{
			return new int2x3(lhs.c0 ^ rhs, lhs.c1 ^ rhs, lhs.c2 ^ rhs);
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x0004B0C3 File Offset: 0x000492C3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 operator ^(int lhs, int2x3 rhs)
		{
			return new int2x3(lhs ^ rhs.c0, lhs ^ rhs.c1, lhs ^ rhs.c2);
		}

		// Token: 0x170007CE RID: 1998
		public unsafe ref int2 this[int index]
		{
			get
			{
				fixed (int2x3* ptr = &this)
				{
					return ref *(int2*)(ptr + (IntPtr)index * (IntPtr)sizeof(int2) / (IntPtr)sizeof(int2x3));
				}
			}
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x0004B10B File Offset: 0x0004930B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(int2x3 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2);
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x0004B148 File Offset: 0x00049348
		public override bool Equals(object o)
		{
			if (o is int2x3)
			{
				int2x3 converted = (int2x3)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x0004B16D File Offset: 0x0004936D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x0004B17C File Offset: 0x0004937C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("int2x3({0}, {1}, {2},  {3}, {4}, {5})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c2.x,
				this.c0.y,
				this.c1.y,
				this.c2.y
			});
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x0004B20C File Offset: 0x0004940C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("int2x3({0}, {1}, {2},  {3}, {4}, {5})", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c2.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c2.y.ToString(format, formatProvider)
			});
		}

		// Token: 0x04000102 RID: 258
		public int2 c0;

		// Token: 0x04000103 RID: 259
		public int2 c1;

		// Token: 0x04000104 RID: 260
		public int2 c2;

		// Token: 0x04000105 RID: 261
		public static readonly int2x3 zero;
	}
}
