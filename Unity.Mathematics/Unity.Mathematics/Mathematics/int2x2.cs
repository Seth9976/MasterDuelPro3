using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000043 RID: 67
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct int2x2 : IEquatable<int2x2>, IFormattable
	{
		// Token: 0x0600190E RID: 6414 RVA: 0x00049BAA File Offset: 0x00047DAA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x2(int2 c0, int2 c1)
		{
			this.c0 = c0;
			this.c1 = c1;
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x00049BBA File Offset: 0x00047DBA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x2(int m00, int m01, int m10, int m11)
		{
			this.c0 = new int2(m00, m10);
			this.c1 = new int2(m01, m11);
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x00049BD7 File Offset: 0x00047DD7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x2(int v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x00049BF1 File Offset: 0x00047DF1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x2(bool v)
		{
			this.c0 = math.select(new int2(0), new int2(1), v);
			this.c1 = math.select(new int2(0), new int2(1), v);
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x00049C23 File Offset: 0x00047E23
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x2(bool2x2 v)
		{
			this.c0 = math.select(new int2(0), new int2(1), v.c0);
			this.c1 = math.select(new int2(0), new int2(1), v.c1);
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x00049C5F File Offset: 0x00047E5F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x2(uint v)
		{
			this.c0 = (int2)v;
			this.c1 = (int2)v;
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x00049C79 File Offset: 0x00047E79
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x2(uint2x2 v)
		{
			this.c0 = (int2)v.c0;
			this.c1 = (int2)v.c1;
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x00049C9D File Offset: 0x00047E9D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x2(float v)
		{
			this.c0 = (int2)v;
			this.c1 = (int2)v;
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x00049CB7 File Offset: 0x00047EB7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x2(float2x2 v)
		{
			this.c0 = (int2)v.c0;
			this.c1 = (int2)v.c1;
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x00049CDB File Offset: 0x00047EDB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x2(double v)
		{
			this.c0 = (int2)v;
			this.c1 = (int2)v;
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x00049CF5 File Offset: 0x00047EF5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x2(double2x2 v)
		{
			this.c0 = (int2)v.c0;
			this.c1 = (int2)v.c1;
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x0000DE39 File Offset: 0x0000C039
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator int2x2(int v)
		{
			return new int2x2(v);
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x0000DE41 File Offset: 0x0000C041
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x2(bool v)
		{
			return new int2x2(v);
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x0000DE49 File Offset: 0x0000C049
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x2(bool2x2 v)
		{
			return new int2x2(v);
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x0000DE51 File Offset: 0x0000C051
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x2(uint v)
		{
			return new int2x2(v);
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x0000DE59 File Offset: 0x0000C059
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x2(uint2x2 v)
		{
			return new int2x2(v);
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x0000DE61 File Offset: 0x0000C061
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x2(float v)
		{
			return new int2x2(v);
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x0000DE69 File Offset: 0x0000C069
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x2(float2x2 v)
		{
			return new int2x2(v);
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x0000DE71 File Offset: 0x0000C071
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x2(double v)
		{
			return new int2x2(v);
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x0000DE79 File Offset: 0x0000C079
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x2(double2x2 v)
		{
			return new int2x2(v);
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x00049D19 File Offset: 0x00047F19
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator *(int2x2 lhs, int2x2 rhs)
		{
			return new int2x2(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1);
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x00049D42 File Offset: 0x00047F42
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator *(int2x2 lhs, int rhs)
		{
			return new int2x2(lhs.c0 * rhs, lhs.c1 * rhs);
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x00049D61 File Offset: 0x00047F61
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator *(int lhs, int2x2 rhs)
		{
			return new int2x2(lhs * rhs.c0, lhs * rhs.c1);
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x00049D80 File Offset: 0x00047F80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator +(int2x2 lhs, int2x2 rhs)
		{
			return new int2x2(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1);
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x00049DA9 File Offset: 0x00047FA9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator +(int2x2 lhs, int rhs)
		{
			return new int2x2(lhs.c0 + rhs, lhs.c1 + rhs);
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x00049DC8 File Offset: 0x00047FC8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator +(int lhs, int2x2 rhs)
		{
			return new int2x2(lhs + rhs.c0, lhs + rhs.c1);
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x00049DE7 File Offset: 0x00047FE7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator -(int2x2 lhs, int2x2 rhs)
		{
			return new int2x2(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1);
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x00049E10 File Offset: 0x00048010
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator -(int2x2 lhs, int rhs)
		{
			return new int2x2(lhs.c0 - rhs, lhs.c1 - rhs);
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x00049E2F File Offset: 0x0004802F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator -(int lhs, int2x2 rhs)
		{
			return new int2x2(lhs - rhs.c0, lhs - rhs.c1);
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x00049E4E File Offset: 0x0004804E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator /(int2x2 lhs, int2x2 rhs)
		{
			return new int2x2(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1);
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x00049E77 File Offset: 0x00048077
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator /(int2x2 lhs, int rhs)
		{
			return new int2x2(lhs.c0 / rhs, lhs.c1 / rhs);
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x00049E96 File Offset: 0x00048096
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator /(int lhs, int2x2 rhs)
		{
			return new int2x2(lhs / rhs.c0, lhs / rhs.c1);
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x00049EB5 File Offset: 0x000480B5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator %(int2x2 lhs, int2x2 rhs)
		{
			return new int2x2(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1);
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x00049EDE File Offset: 0x000480DE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator %(int2x2 lhs, int rhs)
		{
			return new int2x2(lhs.c0 % rhs, lhs.c1 % rhs);
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x00049EFD File Offset: 0x000480FD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator %(int lhs, int2x2 rhs)
		{
			return new int2x2(lhs % rhs.c0, lhs % rhs.c1);
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x00049F1C File Offset: 0x0004811C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator ++(int2x2 val)
		{
			int2 @int = int2.op_Increment(val.c0);
			val.c0 = @int;
			int2 int2 = @int;
			@int = int2.op_Increment(val.c1);
			val.c1 = @int;
			return new int2x2(int2, @int);
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x00049F64 File Offset: 0x00048164
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator --(int2x2 val)
		{
			int2 @int = int2.op_Decrement(val.c0);
			val.c0 = @int;
			int2 int2 = @int;
			@int = int2.op_Decrement(val.c1);
			val.c1 = @int;
			return new int2x2(int2, @int);
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x00049FAA File Offset: 0x000481AA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <(int2x2 lhs, int2x2 rhs)
		{
			return new bool2x2(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1);
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x00049FD3 File Offset: 0x000481D3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <(int2x2 lhs, int rhs)
		{
			return new bool2x2(lhs.c0 < rhs, lhs.c1 < rhs);
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x00049FF2 File Offset: 0x000481F2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <(int lhs, int2x2 rhs)
		{
			return new bool2x2(lhs < rhs.c0, lhs < rhs.c1);
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x0004A011 File Offset: 0x00048211
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <=(int2x2 lhs, int2x2 rhs)
		{
			return new bool2x2(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1);
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x0004A03A File Offset: 0x0004823A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <=(int2x2 lhs, int rhs)
		{
			return new bool2x2(lhs.c0 <= rhs, lhs.c1 <= rhs);
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x0004A059 File Offset: 0x00048259
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <=(int lhs, int2x2 rhs)
		{
			return new bool2x2(lhs <= rhs.c0, lhs <= rhs.c1);
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x0004A078 File Offset: 0x00048278
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >(int2x2 lhs, int2x2 rhs)
		{
			return new bool2x2(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1);
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x0004A0A1 File Offset: 0x000482A1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >(int2x2 lhs, int rhs)
		{
			return new bool2x2(lhs.c0 > rhs, lhs.c1 > rhs);
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x0004A0C0 File Offset: 0x000482C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >(int lhs, int2x2 rhs)
		{
			return new bool2x2(lhs > rhs.c0, lhs > rhs.c1);
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x0004A0DF File Offset: 0x000482DF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >=(int2x2 lhs, int2x2 rhs)
		{
			return new bool2x2(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1);
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x0004A108 File Offset: 0x00048308
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >=(int2x2 lhs, int rhs)
		{
			return new bool2x2(lhs.c0 >= rhs, lhs.c1 >= rhs);
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x0004A127 File Offset: 0x00048327
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >=(int lhs, int2x2 rhs)
		{
			return new bool2x2(lhs >= rhs.c0, lhs >= rhs.c1);
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x0004A146 File Offset: 0x00048346
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator -(int2x2 val)
		{
			return new int2x2(-val.c0, -val.c1);
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x0004A163 File Offset: 0x00048363
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator +(int2x2 val)
		{
			return new int2x2(+val.c0, +val.c1);
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x0004A180 File Offset: 0x00048380
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator <<(int2x2 x, int n)
		{
			return new int2x2(x.c0 << n, x.c1 << n);
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x0004A19F File Offset: 0x0004839F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator >>(int2x2 x, int n)
		{
			return new int2x2(x.c0 >> n, x.c1 >> n);
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x0004A1BE File Offset: 0x000483BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator ==(int2x2 lhs, int2x2 rhs)
		{
			return new bool2x2(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1);
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x0004A1E7 File Offset: 0x000483E7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator ==(int2x2 lhs, int rhs)
		{
			return new bool2x2(lhs.c0 == rhs, lhs.c1 == rhs);
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x0004A206 File Offset: 0x00048406
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator ==(int lhs, int2x2 rhs)
		{
			return new bool2x2(lhs == rhs.c0, lhs == rhs.c1);
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x0004A225 File Offset: 0x00048425
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator !=(int2x2 lhs, int2x2 rhs)
		{
			return new bool2x2(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1);
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x0004A24E File Offset: 0x0004844E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator !=(int2x2 lhs, int rhs)
		{
			return new bool2x2(lhs.c0 != rhs, lhs.c1 != rhs);
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x0004A26D File Offset: 0x0004846D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator !=(int lhs, int2x2 rhs)
		{
			return new bool2x2(lhs != rhs.c0, lhs != rhs.c1);
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x0004A28C File Offset: 0x0004848C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator ~(int2x2 val)
		{
			return new int2x2(~val.c0, ~val.c1);
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x0004A2A9 File Offset: 0x000484A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator &(int2x2 lhs, int2x2 rhs)
		{
			return new int2x2(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1);
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x0004A2D2 File Offset: 0x000484D2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator &(int2x2 lhs, int rhs)
		{
			return new int2x2(lhs.c0 & rhs, lhs.c1 & rhs);
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x0004A2F1 File Offset: 0x000484F1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator &(int lhs, int2x2 rhs)
		{
			return new int2x2(lhs & rhs.c0, lhs & rhs.c1);
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x0004A310 File Offset: 0x00048510
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator |(int2x2 lhs, int2x2 rhs)
		{
			return new int2x2(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1);
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x0004A339 File Offset: 0x00048539
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator |(int2x2 lhs, int rhs)
		{
			return new int2x2(lhs.c0 | rhs, lhs.c1 | rhs);
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x0004A358 File Offset: 0x00048558
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator |(int lhs, int2x2 rhs)
		{
			return new int2x2(lhs | rhs.c0, lhs | rhs.c1);
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x0004A377 File Offset: 0x00048577
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator ^(int2x2 lhs, int2x2 rhs)
		{
			return new int2x2(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1);
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x0004A3A0 File Offset: 0x000485A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator ^(int2x2 lhs, int rhs)
		{
			return new int2x2(lhs.c0 ^ rhs, lhs.c1 ^ rhs);
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x0004A3BF File Offset: 0x000485BF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 operator ^(int lhs, int2x2 rhs)
		{
			return new int2x2(lhs ^ rhs.c0, lhs ^ rhs.c1);
		}

		// Token: 0x170007CD RID: 1997
		public unsafe ref int2 this[int index]
		{
			get
			{
				fixed (int2x2* ptr = &this)
				{
					return ref *(int2*)(ptr + (IntPtr)index * (IntPtr)sizeof(int2) / (IntPtr)sizeof(int2x2));
				}
			}
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x0004A3FB File Offset: 0x000485FB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(int2x2 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1);
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x0004A424 File Offset: 0x00048624
		public override bool Equals(object o)
		{
			if (o is int2x2)
			{
				int2x2 converted = (int2x2)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x0004A449 File Offset: 0x00048649
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x0004A458 File Offset: 0x00048658
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("int2x2({0}, {1},  {2}, {3})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c0.y,
				this.c1.y
			});
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x0004A4C4 File Offset: 0x000486C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("int2x2({0}, {1},  {2}, {3})", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider)
			});
		}

		// Token: 0x040000FE RID: 254
		public int2 c0;

		// Token: 0x040000FF RID: 255
		public int2 c1;

		// Token: 0x04000100 RID: 256
		public static readonly int2x2 identity = new int2x2(1, 0, 0, 1);

		// Token: 0x04000101 RID: 257
		public static readonly int2x2 zero;
	}
}
