using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200004D RID: 77
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct int4x2 : IEquatable<int4x2>, IFormattable
	{
		// Token: 0x06001D81 RID: 7553 RVA: 0x00054575 File Offset: 0x00052775
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x2(int4 c0, int4 c1)
		{
			this.c0 = c0;
			this.c1 = c1;
		}

		// Token: 0x06001D82 RID: 7554 RVA: 0x00054585 File Offset: 0x00052785
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x2(int m00, int m01, int m10, int m11, int m20, int m21, int m30, int m31)
		{
			this.c0 = new int4(m00, m10, m20, m30);
			this.c1 = new int4(m01, m11, m21, m31);
		}

		// Token: 0x06001D83 RID: 7555 RVA: 0x000545AA File Offset: 0x000527AA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x2(int v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06001D84 RID: 7556 RVA: 0x000545C4 File Offset: 0x000527C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x2(bool v)
		{
			this.c0 = math.select(new int4(0), new int4(1), v);
			this.c1 = math.select(new int4(0), new int4(1), v);
		}

		// Token: 0x06001D85 RID: 7557 RVA: 0x000545F6 File Offset: 0x000527F6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x2(bool4x2 v)
		{
			this.c0 = math.select(new int4(0), new int4(1), v.c0);
			this.c1 = math.select(new int4(0), new int4(1), v.c1);
		}

		// Token: 0x06001D86 RID: 7558 RVA: 0x00054632 File Offset: 0x00052832
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x2(uint v)
		{
			this.c0 = (int4)v;
			this.c1 = (int4)v;
		}

		// Token: 0x06001D87 RID: 7559 RVA: 0x0005464C File Offset: 0x0005284C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x2(uint4x2 v)
		{
			this.c0 = (int4)v.c0;
			this.c1 = (int4)v.c1;
		}

		// Token: 0x06001D88 RID: 7560 RVA: 0x00054670 File Offset: 0x00052870
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x2(float v)
		{
			this.c0 = (int4)v;
			this.c1 = (int4)v;
		}

		// Token: 0x06001D89 RID: 7561 RVA: 0x0005468A File Offset: 0x0005288A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x2(float4x2 v)
		{
			this.c0 = (int4)v.c0;
			this.c1 = (int4)v.c1;
		}

		// Token: 0x06001D8A RID: 7562 RVA: 0x000546AE File Offset: 0x000528AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x2(double v)
		{
			this.c0 = (int4)v;
			this.c1 = (int4)v;
		}

		// Token: 0x06001D8B RID: 7563 RVA: 0x000546C8 File Offset: 0x000528C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x2(double4x2 v)
		{
			this.c0 = (int4)v.c0;
			this.c1 = (int4)v.c1;
		}

		// Token: 0x06001D8C RID: 7564 RVA: 0x0000EDC5 File Offset: 0x0000CFC5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator int4x2(int v)
		{
			return new int4x2(v);
		}

		// Token: 0x06001D8D RID: 7565 RVA: 0x0000EDCD File Offset: 0x0000CFCD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x2(bool v)
		{
			return new int4x2(v);
		}

		// Token: 0x06001D8E RID: 7566 RVA: 0x0000EDD5 File Offset: 0x0000CFD5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x2(bool4x2 v)
		{
			return new int4x2(v);
		}

		// Token: 0x06001D8F RID: 7567 RVA: 0x0000EDDD File Offset: 0x0000CFDD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x2(uint v)
		{
			return new int4x2(v);
		}

		// Token: 0x06001D90 RID: 7568 RVA: 0x0000EDE5 File Offset: 0x0000CFE5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x2(uint4x2 v)
		{
			return new int4x2(v);
		}

		// Token: 0x06001D91 RID: 7569 RVA: 0x0000EDED File Offset: 0x0000CFED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x2(float v)
		{
			return new int4x2(v);
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x0000EDF5 File Offset: 0x0000CFF5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x2(float4x2 v)
		{
			return new int4x2(v);
		}

		// Token: 0x06001D93 RID: 7571 RVA: 0x0000EDFD File Offset: 0x0000CFFD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x2(double v)
		{
			return new int4x2(v);
		}

		// Token: 0x06001D94 RID: 7572 RVA: 0x0000EE05 File Offset: 0x0000D005
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x2(double4x2 v)
		{
			return new int4x2(v);
		}

		// Token: 0x06001D95 RID: 7573 RVA: 0x000546EC File Offset: 0x000528EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator *(int4x2 lhs, int4x2 rhs)
		{
			return new int4x2(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1);
		}

		// Token: 0x06001D96 RID: 7574 RVA: 0x00054715 File Offset: 0x00052915
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator *(int4x2 lhs, int rhs)
		{
			return new int4x2(lhs.c0 * rhs, lhs.c1 * rhs);
		}

		// Token: 0x06001D97 RID: 7575 RVA: 0x00054734 File Offset: 0x00052934
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator *(int lhs, int4x2 rhs)
		{
			return new int4x2(lhs * rhs.c0, lhs * rhs.c1);
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x00054753 File Offset: 0x00052953
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator +(int4x2 lhs, int4x2 rhs)
		{
			return new int4x2(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1);
		}

		// Token: 0x06001D99 RID: 7577 RVA: 0x0005477C File Offset: 0x0005297C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator +(int4x2 lhs, int rhs)
		{
			return new int4x2(lhs.c0 + rhs, lhs.c1 + rhs);
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x0005479B File Offset: 0x0005299B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator +(int lhs, int4x2 rhs)
		{
			return new int4x2(lhs + rhs.c0, lhs + rhs.c1);
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x000547BA File Offset: 0x000529BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator -(int4x2 lhs, int4x2 rhs)
		{
			return new int4x2(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1);
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x000547E3 File Offset: 0x000529E3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator -(int4x2 lhs, int rhs)
		{
			return new int4x2(lhs.c0 - rhs, lhs.c1 - rhs);
		}

		// Token: 0x06001D9D RID: 7581 RVA: 0x00054802 File Offset: 0x00052A02
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator -(int lhs, int4x2 rhs)
		{
			return new int4x2(lhs - rhs.c0, lhs - rhs.c1);
		}

		// Token: 0x06001D9E RID: 7582 RVA: 0x00054821 File Offset: 0x00052A21
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator /(int4x2 lhs, int4x2 rhs)
		{
			return new int4x2(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1);
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x0005484A File Offset: 0x00052A4A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator /(int4x2 lhs, int rhs)
		{
			return new int4x2(lhs.c0 / rhs, lhs.c1 / rhs);
		}

		// Token: 0x06001DA0 RID: 7584 RVA: 0x00054869 File Offset: 0x00052A69
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator /(int lhs, int4x2 rhs)
		{
			return new int4x2(lhs / rhs.c0, lhs / rhs.c1);
		}

		// Token: 0x06001DA1 RID: 7585 RVA: 0x00054888 File Offset: 0x00052A88
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator %(int4x2 lhs, int4x2 rhs)
		{
			return new int4x2(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1);
		}

		// Token: 0x06001DA2 RID: 7586 RVA: 0x000548B1 File Offset: 0x00052AB1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator %(int4x2 lhs, int rhs)
		{
			return new int4x2(lhs.c0 % rhs, lhs.c1 % rhs);
		}

		// Token: 0x06001DA3 RID: 7587 RVA: 0x000548D0 File Offset: 0x00052AD0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator %(int lhs, int4x2 rhs)
		{
			return new int4x2(lhs % rhs.c0, lhs % rhs.c1);
		}

		// Token: 0x06001DA4 RID: 7588 RVA: 0x000548F0 File Offset: 0x00052AF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator ++(int4x2 val)
		{
			int4 @int = int4.op_Increment(val.c0);
			val.c0 = @int;
			int4 int2 = @int;
			@int = int4.op_Increment(val.c1);
			val.c1 = @int;
			return new int4x2(int2, @int);
		}

		// Token: 0x06001DA5 RID: 7589 RVA: 0x00054938 File Offset: 0x00052B38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator --(int4x2 val)
		{
			int4 @int = int4.op_Decrement(val.c0);
			val.c0 = @int;
			int4 int2 = @int;
			@int = int4.op_Decrement(val.c1);
			val.c1 = @int;
			return new int4x2(int2, @int);
		}

		// Token: 0x06001DA6 RID: 7590 RVA: 0x0005497E File Offset: 0x00052B7E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <(int4x2 lhs, int4x2 rhs)
		{
			return new bool4x2(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1);
		}

		// Token: 0x06001DA7 RID: 7591 RVA: 0x000549A7 File Offset: 0x00052BA7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <(int4x2 lhs, int rhs)
		{
			return new bool4x2(lhs.c0 < rhs, lhs.c1 < rhs);
		}

		// Token: 0x06001DA8 RID: 7592 RVA: 0x000549C6 File Offset: 0x00052BC6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <(int lhs, int4x2 rhs)
		{
			return new bool4x2(lhs < rhs.c0, lhs < rhs.c1);
		}

		// Token: 0x06001DA9 RID: 7593 RVA: 0x000549E5 File Offset: 0x00052BE5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <=(int4x2 lhs, int4x2 rhs)
		{
			return new bool4x2(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1);
		}

		// Token: 0x06001DAA RID: 7594 RVA: 0x00054A0E File Offset: 0x00052C0E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <=(int4x2 lhs, int rhs)
		{
			return new bool4x2(lhs.c0 <= rhs, lhs.c1 <= rhs);
		}

		// Token: 0x06001DAB RID: 7595 RVA: 0x00054A2D File Offset: 0x00052C2D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <=(int lhs, int4x2 rhs)
		{
			return new bool4x2(lhs <= rhs.c0, lhs <= rhs.c1);
		}

		// Token: 0x06001DAC RID: 7596 RVA: 0x00054A4C File Offset: 0x00052C4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >(int4x2 lhs, int4x2 rhs)
		{
			return new bool4x2(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1);
		}

		// Token: 0x06001DAD RID: 7597 RVA: 0x00054A75 File Offset: 0x00052C75
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >(int4x2 lhs, int rhs)
		{
			return new bool4x2(lhs.c0 > rhs, lhs.c1 > rhs);
		}

		// Token: 0x06001DAE RID: 7598 RVA: 0x00054A94 File Offset: 0x00052C94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >(int lhs, int4x2 rhs)
		{
			return new bool4x2(lhs > rhs.c0, lhs > rhs.c1);
		}

		// Token: 0x06001DAF RID: 7599 RVA: 0x00054AB3 File Offset: 0x00052CB3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >=(int4x2 lhs, int4x2 rhs)
		{
			return new bool4x2(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1);
		}

		// Token: 0x06001DB0 RID: 7600 RVA: 0x00054ADC File Offset: 0x00052CDC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >=(int4x2 lhs, int rhs)
		{
			return new bool4x2(lhs.c0 >= rhs, lhs.c1 >= rhs);
		}

		// Token: 0x06001DB1 RID: 7601 RVA: 0x00054AFB File Offset: 0x00052CFB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >=(int lhs, int4x2 rhs)
		{
			return new bool4x2(lhs >= rhs.c0, lhs >= rhs.c1);
		}

		// Token: 0x06001DB2 RID: 7602 RVA: 0x00054B1A File Offset: 0x00052D1A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator -(int4x2 val)
		{
			return new int4x2(-val.c0, -val.c1);
		}

		// Token: 0x06001DB3 RID: 7603 RVA: 0x00054B37 File Offset: 0x00052D37
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator +(int4x2 val)
		{
			return new int4x2(+val.c0, +val.c1);
		}

		// Token: 0x06001DB4 RID: 7604 RVA: 0x00054B54 File Offset: 0x00052D54
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator <<(int4x2 x, int n)
		{
			return new int4x2(x.c0 << n, x.c1 << n);
		}

		// Token: 0x06001DB5 RID: 7605 RVA: 0x00054B73 File Offset: 0x00052D73
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator >>(int4x2 x, int n)
		{
			return new int4x2(x.c0 >> n, x.c1 >> n);
		}

		// Token: 0x06001DB6 RID: 7606 RVA: 0x00054B92 File Offset: 0x00052D92
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator ==(int4x2 lhs, int4x2 rhs)
		{
			return new bool4x2(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1);
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x00054BBB File Offset: 0x00052DBB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator ==(int4x2 lhs, int rhs)
		{
			return new bool4x2(lhs.c0 == rhs, lhs.c1 == rhs);
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x00054BDA File Offset: 0x00052DDA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator ==(int lhs, int4x2 rhs)
		{
			return new bool4x2(lhs == rhs.c0, lhs == rhs.c1);
		}

		// Token: 0x06001DB9 RID: 7609 RVA: 0x00054BF9 File Offset: 0x00052DF9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator !=(int4x2 lhs, int4x2 rhs)
		{
			return new bool4x2(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1);
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x00054C22 File Offset: 0x00052E22
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator !=(int4x2 lhs, int rhs)
		{
			return new bool4x2(lhs.c0 != rhs, lhs.c1 != rhs);
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x00054C41 File Offset: 0x00052E41
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator !=(int lhs, int4x2 rhs)
		{
			return new bool4x2(lhs != rhs.c0, lhs != rhs.c1);
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x00054C60 File Offset: 0x00052E60
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator ~(int4x2 val)
		{
			return new int4x2(~val.c0, ~val.c1);
		}

		// Token: 0x06001DBD RID: 7613 RVA: 0x00054C7D File Offset: 0x00052E7D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator &(int4x2 lhs, int4x2 rhs)
		{
			return new int4x2(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1);
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x00054CA6 File Offset: 0x00052EA6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator &(int4x2 lhs, int rhs)
		{
			return new int4x2(lhs.c0 & rhs, lhs.c1 & rhs);
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x00054CC5 File Offset: 0x00052EC5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator &(int lhs, int4x2 rhs)
		{
			return new int4x2(lhs & rhs.c0, lhs & rhs.c1);
		}

		// Token: 0x06001DC0 RID: 7616 RVA: 0x00054CE4 File Offset: 0x00052EE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator |(int4x2 lhs, int4x2 rhs)
		{
			return new int4x2(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1);
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x00054D0D File Offset: 0x00052F0D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator |(int4x2 lhs, int rhs)
		{
			return new int4x2(lhs.c0 | rhs, lhs.c1 | rhs);
		}

		// Token: 0x06001DC2 RID: 7618 RVA: 0x00054D2C File Offset: 0x00052F2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator |(int lhs, int4x2 rhs)
		{
			return new int4x2(lhs | rhs.c0, lhs | rhs.c1);
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x00054D4B File Offset: 0x00052F4B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator ^(int4x2 lhs, int4x2 rhs)
		{
			return new int4x2(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1);
		}

		// Token: 0x06001DC4 RID: 7620 RVA: 0x00054D74 File Offset: 0x00052F74
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator ^(int4x2 lhs, int rhs)
		{
			return new int4x2(lhs.c0 ^ rhs, lhs.c1 ^ rhs);
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x00054D93 File Offset: 0x00052F93
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 operator ^(int lhs, int4x2 rhs)
		{
			return new int4x2(lhs ^ rhs.c0, lhs ^ rhs.c1);
		}

		// Token: 0x1700099A RID: 2458
		public unsafe ref int4 this[int index]
		{
			get
			{
				fixed (int4x2* ptr = &this)
				{
					return ref *(int4*)(ptr + (IntPtr)index * (IntPtr)sizeof(int4) / (IntPtr)sizeof(int4x2));
				}
			}
		}

		// Token: 0x06001DC7 RID: 7623 RVA: 0x00054DCF File Offset: 0x00052FCF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(int4x2 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1);
		}

		// Token: 0x06001DC8 RID: 7624 RVA: 0x00054DF8 File Offset: 0x00052FF8
		public override bool Equals(object o)
		{
			if (o is int4x2)
			{
				int4x2 converted = (int4x2)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x00054E1D File Offset: 0x0005301D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x00054E2C File Offset: 0x0005302C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("int4x2({0}, {1},  {2}, {3},  {4}, {5},  {6}, {7})", new object[]
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

		// Token: 0x06001DCB RID: 7627 RVA: 0x00054EE4 File Offset: 0x000530E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("int4x2({0}, {1},  {2}, {3},  {4}, {5},  {6}, {7})", new object[]
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

		// Token: 0x04000128 RID: 296
		public int4 c0;

		// Token: 0x04000129 RID: 297
		public int4 c1;

		// Token: 0x0400012A RID: 298
		public static readonly int4x2 zero;
	}
}
