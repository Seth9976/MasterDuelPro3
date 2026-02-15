using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200004E RID: 78
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct int4x3 : IEquatable<int4x3>, IFormattable
	{
		// Token: 0x06001DCC RID: 7628 RVA: 0x00054FA9 File Offset: 0x000531A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x3(int4 c0, int4 c1, int4 c2)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x00054FC0 File Offset: 0x000531C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x3(int m00, int m01, int m02, int m10, int m11, int m12, int m20, int m21, int m22, int m30, int m31, int m32)
		{
			this.c0 = new int4(m00, m10, m20, m30);
			this.c1 = new int4(m01, m11, m21, m31);
			this.c2 = new int4(m02, m12, m22, m32);
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x00054FF8 File Offset: 0x000531F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x3(int v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x00055020 File Offset: 0x00053220
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x3(bool v)
		{
			this.c0 = math.select(new int4(0), new int4(1), v);
			this.c1 = math.select(new int4(0), new int4(1), v);
			this.c2 = math.select(new int4(0), new int4(1), v);
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x00055078 File Offset: 0x00053278
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x3(bool4x3 v)
		{
			this.c0 = math.select(new int4(0), new int4(1), v.c0);
			this.c1 = math.select(new int4(0), new int4(1), v.c1);
			this.c2 = math.select(new int4(0), new int4(1), v.c2);
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x000550DC File Offset: 0x000532DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x3(uint v)
		{
			this.c0 = (int4)v;
			this.c1 = (int4)v;
			this.c2 = (int4)v;
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x00055102 File Offset: 0x00053302
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x3(uint4x3 v)
		{
			this.c0 = (int4)v.c0;
			this.c1 = (int4)v.c1;
			this.c2 = (int4)v.c2;
		}

		// Token: 0x06001DD3 RID: 7635 RVA: 0x00055137 File Offset: 0x00053337
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x3(float v)
		{
			this.c0 = (int4)v;
			this.c1 = (int4)v;
			this.c2 = (int4)v;
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x0005515D File Offset: 0x0005335D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x3(float4x3 v)
		{
			this.c0 = (int4)v.c0;
			this.c1 = (int4)v.c1;
			this.c2 = (int4)v.c2;
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x00055192 File Offset: 0x00053392
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x3(double v)
		{
			this.c0 = (int4)v;
			this.c1 = (int4)v;
			this.c2 = (int4)v;
		}

		// Token: 0x06001DD6 RID: 7638 RVA: 0x000551B8 File Offset: 0x000533B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x3(double4x3 v)
		{
			this.c0 = (int4)v.c0;
			this.c1 = (int4)v.c1;
			this.c2 = (int4)v.c2;
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x0000EF8A File Offset: 0x0000D18A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator int4x3(int v)
		{
			return new int4x3(v);
		}

		// Token: 0x06001DD8 RID: 7640 RVA: 0x0000EF92 File Offset: 0x0000D192
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x3(bool v)
		{
			return new int4x3(v);
		}

		// Token: 0x06001DD9 RID: 7641 RVA: 0x0000EF9A File Offset: 0x0000D19A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x3(bool4x3 v)
		{
			return new int4x3(v);
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x0000EFA2 File Offset: 0x0000D1A2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x3(uint v)
		{
			return new int4x3(v);
		}

		// Token: 0x06001DDB RID: 7643 RVA: 0x0000EFAA File Offset: 0x0000D1AA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x3(uint4x3 v)
		{
			return new int4x3(v);
		}

		// Token: 0x06001DDC RID: 7644 RVA: 0x0000EFB2 File Offset: 0x0000D1B2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x3(float v)
		{
			return new int4x3(v);
		}

		// Token: 0x06001DDD RID: 7645 RVA: 0x0000EFBA File Offset: 0x0000D1BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x3(float4x3 v)
		{
			return new int4x3(v);
		}

		// Token: 0x06001DDE RID: 7646 RVA: 0x0000EFC2 File Offset: 0x0000D1C2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x3(double v)
		{
			return new int4x3(v);
		}

		// Token: 0x06001DDF RID: 7647 RVA: 0x0000EFCA File Offset: 0x0000D1CA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x3(double4x3 v)
		{
			return new int4x3(v);
		}

		// Token: 0x06001DE0 RID: 7648 RVA: 0x000551ED File Offset: 0x000533ED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator *(int4x3 lhs, int4x3 rhs)
		{
			return new int4x3(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2);
		}

		// Token: 0x06001DE1 RID: 7649 RVA: 0x00055227 File Offset: 0x00053427
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator *(int4x3 lhs, int rhs)
		{
			return new int4x3(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs);
		}

		// Token: 0x06001DE2 RID: 7650 RVA: 0x00055252 File Offset: 0x00053452
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator *(int lhs, int4x3 rhs)
		{
			return new int4x3(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2);
		}

		// Token: 0x06001DE3 RID: 7651 RVA: 0x0005527D File Offset: 0x0005347D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator +(int4x3 lhs, int4x3 rhs)
		{
			return new int4x3(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2);
		}

		// Token: 0x06001DE4 RID: 7652 RVA: 0x000552B7 File Offset: 0x000534B7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator +(int4x3 lhs, int rhs)
		{
			return new int4x3(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs);
		}

		// Token: 0x06001DE5 RID: 7653 RVA: 0x000552E2 File Offset: 0x000534E2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator +(int lhs, int4x3 rhs)
		{
			return new int4x3(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2);
		}

		// Token: 0x06001DE6 RID: 7654 RVA: 0x0005530D File Offset: 0x0005350D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator -(int4x3 lhs, int4x3 rhs)
		{
			return new int4x3(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2);
		}

		// Token: 0x06001DE7 RID: 7655 RVA: 0x00055347 File Offset: 0x00053547
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator -(int4x3 lhs, int rhs)
		{
			return new int4x3(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs);
		}

		// Token: 0x06001DE8 RID: 7656 RVA: 0x00055372 File Offset: 0x00053572
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator -(int lhs, int4x3 rhs)
		{
			return new int4x3(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2);
		}

		// Token: 0x06001DE9 RID: 7657 RVA: 0x0005539D File Offset: 0x0005359D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator /(int4x3 lhs, int4x3 rhs)
		{
			return new int4x3(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2);
		}

		// Token: 0x06001DEA RID: 7658 RVA: 0x000553D7 File Offset: 0x000535D7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator /(int4x3 lhs, int rhs)
		{
			return new int4x3(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs);
		}

		// Token: 0x06001DEB RID: 7659 RVA: 0x00055402 File Offset: 0x00053602
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator /(int lhs, int4x3 rhs)
		{
			return new int4x3(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2);
		}

		// Token: 0x06001DEC RID: 7660 RVA: 0x0005542D File Offset: 0x0005362D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator %(int4x3 lhs, int4x3 rhs)
		{
			return new int4x3(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2);
		}

		// Token: 0x06001DED RID: 7661 RVA: 0x00055467 File Offset: 0x00053667
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator %(int4x3 lhs, int rhs)
		{
			return new int4x3(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs);
		}

		// Token: 0x06001DEE RID: 7662 RVA: 0x00055492 File Offset: 0x00053692
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator %(int lhs, int4x3 rhs)
		{
			return new int4x3(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2);
		}

		// Token: 0x06001DEF RID: 7663 RVA: 0x000554C0 File Offset: 0x000536C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator ++(int4x3 val)
		{
			int4 @int = int4.op_Increment(val.c0);
			val.c0 = @int;
			int4 int2 = @int;
			@int = int4.op_Increment(val.c1);
			val.c1 = @int;
			int4 int3 = @int;
			@int = int4.op_Increment(val.c2);
			val.c2 = @int;
			return new int4x3(int2, int3, @int);
		}

		// Token: 0x06001DF0 RID: 7664 RVA: 0x00055520 File Offset: 0x00053720
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator --(int4x3 val)
		{
			int4 @int = int4.op_Decrement(val.c0);
			val.c0 = @int;
			int4 int2 = @int;
			@int = int4.op_Decrement(val.c1);
			val.c1 = @int;
			int4 int3 = @int;
			@int = int4.op_Decrement(val.c2);
			val.c2 = @int;
			return new int4x3(int2, int3, @int);
		}

		// Token: 0x06001DF1 RID: 7665 RVA: 0x00055580 File Offset: 0x00053780
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <(int4x3 lhs, int4x3 rhs)
		{
			return new bool4x3(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2);
		}

		// Token: 0x06001DF2 RID: 7666 RVA: 0x000555BA File Offset: 0x000537BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <(int4x3 lhs, int rhs)
		{
			return new bool4x3(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs);
		}

		// Token: 0x06001DF3 RID: 7667 RVA: 0x000555E5 File Offset: 0x000537E5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <(int lhs, int4x3 rhs)
		{
			return new bool4x3(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2);
		}

		// Token: 0x06001DF4 RID: 7668 RVA: 0x00055610 File Offset: 0x00053810
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <=(int4x3 lhs, int4x3 rhs)
		{
			return new bool4x3(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2);
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x0005564A File Offset: 0x0005384A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <=(int4x3 lhs, int rhs)
		{
			return new bool4x3(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs);
		}

		// Token: 0x06001DF6 RID: 7670 RVA: 0x00055675 File Offset: 0x00053875
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <=(int lhs, int4x3 rhs)
		{
			return new bool4x3(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2);
		}

		// Token: 0x06001DF7 RID: 7671 RVA: 0x000556A0 File Offset: 0x000538A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >(int4x3 lhs, int4x3 rhs)
		{
			return new bool4x3(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2);
		}

		// Token: 0x06001DF8 RID: 7672 RVA: 0x000556DA File Offset: 0x000538DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >(int4x3 lhs, int rhs)
		{
			return new bool4x3(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs);
		}

		// Token: 0x06001DF9 RID: 7673 RVA: 0x00055705 File Offset: 0x00053905
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >(int lhs, int4x3 rhs)
		{
			return new bool4x3(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2);
		}

		// Token: 0x06001DFA RID: 7674 RVA: 0x00055730 File Offset: 0x00053930
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >=(int4x3 lhs, int4x3 rhs)
		{
			return new bool4x3(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2);
		}

		// Token: 0x06001DFB RID: 7675 RVA: 0x0005576A File Offset: 0x0005396A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >=(int4x3 lhs, int rhs)
		{
			return new bool4x3(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs);
		}

		// Token: 0x06001DFC RID: 7676 RVA: 0x00055795 File Offset: 0x00053995
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >=(int lhs, int4x3 rhs)
		{
			return new bool4x3(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2);
		}

		// Token: 0x06001DFD RID: 7677 RVA: 0x000557C0 File Offset: 0x000539C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator -(int4x3 val)
		{
			return new int4x3(-val.c0, -val.c1, -val.c2);
		}

		// Token: 0x06001DFE RID: 7678 RVA: 0x000557E8 File Offset: 0x000539E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator +(int4x3 val)
		{
			return new int4x3(+val.c0, +val.c1, +val.c2);
		}

		// Token: 0x06001DFF RID: 7679 RVA: 0x00055810 File Offset: 0x00053A10
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator <<(int4x3 x, int n)
		{
			return new int4x3(x.c0 << n, x.c1 << n, x.c2 << n);
		}

		// Token: 0x06001E00 RID: 7680 RVA: 0x0005583B File Offset: 0x00053A3B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator >>(int4x3 x, int n)
		{
			return new int4x3(x.c0 >> n, x.c1 >> n, x.c2 >> n);
		}

		// Token: 0x06001E01 RID: 7681 RVA: 0x00055866 File Offset: 0x00053A66
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator ==(int4x3 lhs, int4x3 rhs)
		{
			return new bool4x3(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2);
		}

		// Token: 0x06001E02 RID: 7682 RVA: 0x000558A0 File Offset: 0x00053AA0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator ==(int4x3 lhs, int rhs)
		{
			return new bool4x3(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs);
		}

		// Token: 0x06001E03 RID: 7683 RVA: 0x000558CB File Offset: 0x00053ACB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator ==(int lhs, int4x3 rhs)
		{
			return new bool4x3(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2);
		}

		// Token: 0x06001E04 RID: 7684 RVA: 0x000558F6 File Offset: 0x00053AF6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator !=(int4x3 lhs, int4x3 rhs)
		{
			return new bool4x3(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2);
		}

		// Token: 0x06001E05 RID: 7685 RVA: 0x00055930 File Offset: 0x00053B30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator !=(int4x3 lhs, int rhs)
		{
			return new bool4x3(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs);
		}

		// Token: 0x06001E06 RID: 7686 RVA: 0x0005595B File Offset: 0x00053B5B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator !=(int lhs, int4x3 rhs)
		{
			return new bool4x3(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2);
		}

		// Token: 0x06001E07 RID: 7687 RVA: 0x00055986 File Offset: 0x00053B86
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator ~(int4x3 val)
		{
			return new int4x3(~val.c0, ~val.c1, ~val.c2);
		}

		// Token: 0x06001E08 RID: 7688 RVA: 0x000559AE File Offset: 0x00053BAE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator &(int4x3 lhs, int4x3 rhs)
		{
			return new int4x3(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1, lhs.c2 & rhs.c2);
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x000559E8 File Offset: 0x00053BE8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator &(int4x3 lhs, int rhs)
		{
			return new int4x3(lhs.c0 & rhs, lhs.c1 & rhs, lhs.c2 & rhs);
		}

		// Token: 0x06001E0A RID: 7690 RVA: 0x00055A13 File Offset: 0x00053C13
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator &(int lhs, int4x3 rhs)
		{
			return new int4x3(lhs & rhs.c0, lhs & rhs.c1, lhs & rhs.c2);
		}

		// Token: 0x06001E0B RID: 7691 RVA: 0x00055A3E File Offset: 0x00053C3E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator |(int4x3 lhs, int4x3 rhs)
		{
			return new int4x3(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1, lhs.c2 | rhs.c2);
		}

		// Token: 0x06001E0C RID: 7692 RVA: 0x00055A78 File Offset: 0x00053C78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator |(int4x3 lhs, int rhs)
		{
			return new int4x3(lhs.c0 | rhs, lhs.c1 | rhs, lhs.c2 | rhs);
		}

		// Token: 0x06001E0D RID: 7693 RVA: 0x00055AA3 File Offset: 0x00053CA3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator |(int lhs, int4x3 rhs)
		{
			return new int4x3(lhs | rhs.c0, lhs | rhs.c1, lhs | rhs.c2);
		}

		// Token: 0x06001E0E RID: 7694 RVA: 0x00055ACE File Offset: 0x00053CCE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator ^(int4x3 lhs, int4x3 rhs)
		{
			return new int4x3(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1, lhs.c2 ^ rhs.c2);
		}

		// Token: 0x06001E0F RID: 7695 RVA: 0x00055B08 File Offset: 0x00053D08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator ^(int4x3 lhs, int rhs)
		{
			return new int4x3(lhs.c0 ^ rhs, lhs.c1 ^ rhs, lhs.c2 ^ rhs);
		}

		// Token: 0x06001E10 RID: 7696 RVA: 0x00055B33 File Offset: 0x00053D33
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 operator ^(int lhs, int4x3 rhs)
		{
			return new int4x3(lhs ^ rhs.c0, lhs ^ rhs.c1, lhs ^ rhs.c2);
		}

		// Token: 0x1700099B RID: 2459
		public unsafe ref int4 this[int index]
		{
			get
			{
				fixed (int4x3* ptr = &this)
				{
					return ref *(int4*)(ptr + (IntPtr)index * (IntPtr)sizeof(int4) / (IntPtr)sizeof(int4x3));
				}
			}
		}

		// Token: 0x06001E12 RID: 7698 RVA: 0x00055B7B File Offset: 0x00053D7B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(int4x3 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2);
		}

		// Token: 0x06001E13 RID: 7699 RVA: 0x00055BB8 File Offset: 0x00053DB8
		public override bool Equals(object o)
		{
			if (o is int4x3)
			{
				int4x3 converted = (int4x3)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06001E14 RID: 7700 RVA: 0x00055BDD File Offset: 0x00053DDD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06001E15 RID: 7701 RVA: 0x00055BEC File Offset: 0x00053DEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("int4x3({0}, {1}, {2},  {3}, {4}, {5},  {6}, {7}, {8},  {9}, {10}, {11})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c2.x,
				this.c0.y,
				this.c1.y,
				this.c2.y,
				this.c0.z,
				this.c1.z,
				this.c2.z,
				this.c0.w,
				this.c1.w,
				this.c2.w
			});
		}

		// Token: 0x06001E16 RID: 7702 RVA: 0x00055CF4 File Offset: 0x00053EF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("int4x3({0}, {1}, {2},  {3}, {4}, {5},  {6}, {7}, {8},  {9}, {10}, {11})", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c2.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c2.y.ToString(format, formatProvider),
				this.c0.z.ToString(format, formatProvider),
				this.c1.z.ToString(format, formatProvider),
				this.c2.z.ToString(format, formatProvider),
				this.c0.w.ToString(format, formatProvider),
				this.c1.w.ToString(format, formatProvider),
				this.c2.w.ToString(format, formatProvider)
			});
		}

		// Token: 0x0400012B RID: 299
		public int4 c0;

		// Token: 0x0400012C RID: 300
		public int4 c1;

		// Token: 0x0400012D RID: 301
		public int4 c2;

		// Token: 0x0400012E RID: 302
		public static readonly int4x3 zero;
	}
}
