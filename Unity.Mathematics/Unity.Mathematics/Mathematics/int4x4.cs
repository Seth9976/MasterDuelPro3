using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200004F RID: 79
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct int4x4 : IEquatable<int4x4>, IFormattable
	{
		// Token: 0x06001E17 RID: 7703 RVA: 0x00055E11 File Offset: 0x00054011
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x4(int4 c0, int4 c1, int4 c2, int4 c3)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
			this.c3 = c3;
		}

		// Token: 0x06001E18 RID: 7704 RVA: 0x00055E30 File Offset: 0x00054030
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x4(int m00, int m01, int m02, int m03, int m10, int m11, int m12, int m13, int m20, int m21, int m22, int m23, int m30, int m31, int m32, int m33)
		{
			this.c0 = new int4(m00, m10, m20, m30);
			this.c1 = new int4(m01, m11, m21, m31);
			this.c2 = new int4(m02, m12, m22, m32);
			this.c3 = new int4(m03, m13, m23, m33);
		}

		// Token: 0x06001E19 RID: 7705 RVA: 0x00055E86 File Offset: 0x00054086
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x4(int v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x06001E1A RID: 7706 RVA: 0x00055EB8 File Offset: 0x000540B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x4(bool v)
		{
			this.c0 = math.select(new int4(0), new int4(1), v);
			this.c1 = math.select(new int4(0), new int4(1), v);
			this.c2 = math.select(new int4(0), new int4(1), v);
			this.c3 = math.select(new int4(0), new int4(1), v);
		}

		// Token: 0x06001E1B RID: 7707 RVA: 0x00055F28 File Offset: 0x00054128
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x4(bool4x4 v)
		{
			this.c0 = math.select(new int4(0), new int4(1), v.c0);
			this.c1 = math.select(new int4(0), new int4(1), v.c1);
			this.c2 = math.select(new int4(0), new int4(1), v.c2);
			this.c3 = math.select(new int4(0), new int4(1), v.c3);
		}

		// Token: 0x06001E1C RID: 7708 RVA: 0x00055FA9 File Offset: 0x000541A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x4(uint v)
		{
			this.c0 = (int4)v;
			this.c1 = (int4)v;
			this.c2 = (int4)v;
			this.c3 = (int4)v;
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x00055FDC File Offset: 0x000541DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x4(uint4x4 v)
		{
			this.c0 = (int4)v.c0;
			this.c1 = (int4)v.c1;
			this.c2 = (int4)v.c2;
			this.c3 = (int4)v.c3;
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x0005602D File Offset: 0x0005422D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x4(float v)
		{
			this.c0 = (int4)v;
			this.c1 = (int4)v;
			this.c2 = (int4)v;
			this.c3 = (int4)v;
		}

		// Token: 0x06001E1F RID: 7711 RVA: 0x00056060 File Offset: 0x00054260
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x4(float4x4 v)
		{
			this.c0 = (int4)v.c0;
			this.c1 = (int4)v.c1;
			this.c2 = (int4)v.c2;
			this.c3 = (int4)v.c3;
		}

		// Token: 0x06001E20 RID: 7712 RVA: 0x000560B1 File Offset: 0x000542B1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x4(double v)
		{
			this.c0 = (int4)v;
			this.c1 = (int4)v;
			this.c2 = (int4)v;
			this.c3 = (int4)v;
		}

		// Token: 0x06001E21 RID: 7713 RVA: 0x000560E4 File Offset: 0x000542E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4x4(double4x4 v)
		{
			this.c0 = (int4)v.c0;
			this.c1 = (int4)v.c1;
			this.c2 = (int4)v.c2;
			this.c3 = (int4)v.c3;
		}

		// Token: 0x06001E22 RID: 7714 RVA: 0x0000F1E2 File Offset: 0x0000D3E2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator int4x4(int v)
		{
			return new int4x4(v);
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x0000F1EA File Offset: 0x0000D3EA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x4(bool v)
		{
			return new int4x4(v);
		}

		// Token: 0x06001E24 RID: 7716 RVA: 0x0000F1F2 File Offset: 0x0000D3F2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x4(bool4x4 v)
		{
			return new int4x4(v);
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x0000F1FA File Offset: 0x0000D3FA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x4(uint v)
		{
			return new int4x4(v);
		}

		// Token: 0x06001E26 RID: 7718 RVA: 0x0000F202 File Offset: 0x0000D402
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x4(uint4x4 v)
		{
			return new int4x4(v);
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x0000F20A File Offset: 0x0000D40A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x4(float v)
		{
			return new int4x4(v);
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x0000F212 File Offset: 0x0000D412
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x4(float4x4 v)
		{
			return new int4x4(v);
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x0000F21A File Offset: 0x0000D41A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x4(double v)
		{
			return new int4x4(v);
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x0000F222 File Offset: 0x0000D422
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4x4(double4x4 v)
		{
			return new int4x4(v);
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x00056138 File Offset: 0x00054338
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator *(int4x4 lhs, int4x4 rhs)
		{
			return new int4x4(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2, lhs.c3 * rhs.c3);
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x0005618E File Offset: 0x0005438E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator *(int4x4 lhs, int rhs)
		{
			return new int4x4(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs, lhs.c3 * rhs);
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x000561C5 File Offset: 0x000543C5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator *(int lhs, int4x4 rhs)
		{
			return new int4x4(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2, lhs * rhs.c3);
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x000561FC File Offset: 0x000543FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator +(int4x4 lhs, int4x4 rhs)
		{
			return new int4x4(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2, lhs.c3 + rhs.c3);
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x00056252 File Offset: 0x00054452
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator +(int4x4 lhs, int rhs)
		{
			return new int4x4(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs, lhs.c3 + rhs);
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x00056289 File Offset: 0x00054489
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator +(int lhs, int4x4 rhs)
		{
			return new int4x4(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2, lhs + rhs.c3);
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x000562C0 File Offset: 0x000544C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator -(int4x4 lhs, int4x4 rhs)
		{
			return new int4x4(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2, lhs.c3 - rhs.c3);
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x00056316 File Offset: 0x00054516
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator -(int4x4 lhs, int rhs)
		{
			return new int4x4(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs, lhs.c3 - rhs);
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x0005634D File Offset: 0x0005454D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator -(int lhs, int4x4 rhs)
		{
			return new int4x4(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2, lhs - rhs.c3);
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x00056384 File Offset: 0x00054584
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator /(int4x4 lhs, int4x4 rhs)
		{
			return new int4x4(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2, lhs.c3 / rhs.c3);
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x000563DA File Offset: 0x000545DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator /(int4x4 lhs, int rhs)
		{
			return new int4x4(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs, lhs.c3 / rhs);
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x00056411 File Offset: 0x00054611
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator /(int lhs, int4x4 rhs)
		{
			return new int4x4(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2, lhs / rhs.c3);
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x00056448 File Offset: 0x00054648
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator %(int4x4 lhs, int4x4 rhs)
		{
			return new int4x4(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2, lhs.c3 % rhs.c3);
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x0005649E File Offset: 0x0005469E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator %(int4x4 lhs, int rhs)
		{
			return new int4x4(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs, lhs.c3 % rhs);
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x000564D5 File Offset: 0x000546D5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator %(int lhs, int4x4 rhs)
		{
			return new int4x4(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2, lhs % rhs.c3);
		}

		// Token: 0x06001E3A RID: 7738 RVA: 0x0005650C File Offset: 0x0005470C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator ++(int4x4 val)
		{
			int4 @int = int4.op_Increment(val.c0);
			val.c0 = @int;
			int4 int2 = @int;
			@int = int4.op_Increment(val.c1);
			val.c1 = @int;
			int4 int3 = @int;
			@int = int4.op_Increment(val.c2);
			val.c2 = @int;
			int4 int4 = @int;
			@int = int4.op_Increment(val.c3);
			val.c3 = @int;
			return new int4x4(int2, int3, int4, @int);
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x00056588 File Offset: 0x00054788
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator --(int4x4 val)
		{
			int4 @int = int4.op_Decrement(val.c0);
			val.c0 = @int;
			int4 int2 = @int;
			@int = int4.op_Decrement(val.c1);
			val.c1 = @int;
			int4 int3 = @int;
			@int = int4.op_Decrement(val.c2);
			val.c2 = @int;
			int4 int4 = @int;
			@int = int4.op_Decrement(val.c3);
			val.c3 = @int;
			return new int4x4(int2, int3, int4, @int);
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x00056604 File Offset: 0x00054804
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <(int4x4 lhs, int4x4 rhs)
		{
			return new bool4x4(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2, lhs.c3 < rhs.c3);
		}

		// Token: 0x06001E3D RID: 7741 RVA: 0x0005665A File Offset: 0x0005485A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <(int4x4 lhs, int rhs)
		{
			return new bool4x4(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs, lhs.c3 < rhs);
		}

		// Token: 0x06001E3E RID: 7742 RVA: 0x00056691 File Offset: 0x00054891
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <(int lhs, int4x4 rhs)
		{
			return new bool4x4(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2, lhs < rhs.c3);
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x000566C8 File Offset: 0x000548C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <=(int4x4 lhs, int4x4 rhs)
		{
			return new bool4x4(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2, lhs.c3 <= rhs.c3);
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x0005671E File Offset: 0x0005491E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <=(int4x4 lhs, int rhs)
		{
			return new bool4x4(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs, lhs.c3 <= rhs);
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x00056755 File Offset: 0x00054955
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <=(int lhs, int4x4 rhs)
		{
			return new bool4x4(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2, lhs <= rhs.c3);
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x0005678C File Offset: 0x0005498C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >(int4x4 lhs, int4x4 rhs)
		{
			return new bool4x4(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2, lhs.c3 > rhs.c3);
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x000567E2 File Offset: 0x000549E2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >(int4x4 lhs, int rhs)
		{
			return new bool4x4(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs, lhs.c3 > rhs);
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x00056819 File Offset: 0x00054A19
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >(int lhs, int4x4 rhs)
		{
			return new bool4x4(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2, lhs > rhs.c3);
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x00056850 File Offset: 0x00054A50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >=(int4x4 lhs, int4x4 rhs)
		{
			return new bool4x4(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2, lhs.c3 >= rhs.c3);
		}

		// Token: 0x06001E46 RID: 7750 RVA: 0x000568A6 File Offset: 0x00054AA6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >=(int4x4 lhs, int rhs)
		{
			return new bool4x4(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs, lhs.c3 >= rhs);
		}

		// Token: 0x06001E47 RID: 7751 RVA: 0x000568DD File Offset: 0x00054ADD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >=(int lhs, int4x4 rhs)
		{
			return new bool4x4(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2, lhs >= rhs.c3);
		}

		// Token: 0x06001E48 RID: 7752 RVA: 0x00056914 File Offset: 0x00054B14
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator -(int4x4 val)
		{
			return new int4x4(-val.c0, -val.c1, -val.c2, -val.c3);
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x00056947 File Offset: 0x00054B47
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator +(int4x4 val)
		{
			return new int4x4(+val.c0, +val.c1, +val.c2, +val.c3);
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x0005697A File Offset: 0x00054B7A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator <<(int4x4 x, int n)
		{
			return new int4x4(x.c0 << n, x.c1 << n, x.c2 << n, x.c3 << n);
		}

		// Token: 0x06001E4B RID: 7755 RVA: 0x000569B1 File Offset: 0x00054BB1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator >>(int4x4 x, int n)
		{
			return new int4x4(x.c0 >> n, x.c1 >> n, x.c2 >> n, x.c3 >> n);
		}

		// Token: 0x06001E4C RID: 7756 RVA: 0x000569E8 File Offset: 0x00054BE8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator ==(int4x4 lhs, int4x4 rhs)
		{
			return new bool4x4(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2, lhs.c3 == rhs.c3);
		}

		// Token: 0x06001E4D RID: 7757 RVA: 0x00056A3E File Offset: 0x00054C3E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator ==(int4x4 lhs, int rhs)
		{
			return new bool4x4(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs, lhs.c3 == rhs);
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x00056A75 File Offset: 0x00054C75
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator ==(int lhs, int4x4 rhs)
		{
			return new bool4x4(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2, lhs == rhs.c3);
		}

		// Token: 0x06001E4F RID: 7759 RVA: 0x00056AAC File Offset: 0x00054CAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator !=(int4x4 lhs, int4x4 rhs)
		{
			return new bool4x4(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2, lhs.c3 != rhs.c3);
		}

		// Token: 0x06001E50 RID: 7760 RVA: 0x00056B02 File Offset: 0x00054D02
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator !=(int4x4 lhs, int rhs)
		{
			return new bool4x4(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs, lhs.c3 != rhs);
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x00056B39 File Offset: 0x00054D39
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator !=(int lhs, int4x4 rhs)
		{
			return new bool4x4(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2, lhs != rhs.c3);
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x00056B70 File Offset: 0x00054D70
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator ~(int4x4 val)
		{
			return new int4x4(~val.c0, ~val.c1, ~val.c2, ~val.c3);
		}

		// Token: 0x06001E53 RID: 7763 RVA: 0x00056BA4 File Offset: 0x00054DA4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator &(int4x4 lhs, int4x4 rhs)
		{
			return new int4x4(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1, lhs.c2 & rhs.c2, lhs.c3 & rhs.c3);
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x00056BFA File Offset: 0x00054DFA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator &(int4x4 lhs, int rhs)
		{
			return new int4x4(lhs.c0 & rhs, lhs.c1 & rhs, lhs.c2 & rhs, lhs.c3 & rhs);
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x00056C31 File Offset: 0x00054E31
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator &(int lhs, int4x4 rhs)
		{
			return new int4x4(lhs & rhs.c0, lhs & rhs.c1, lhs & rhs.c2, lhs & rhs.c3);
		}

		// Token: 0x06001E56 RID: 7766 RVA: 0x00056C68 File Offset: 0x00054E68
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator |(int4x4 lhs, int4x4 rhs)
		{
			return new int4x4(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1, lhs.c2 | rhs.c2, lhs.c3 | rhs.c3);
		}

		// Token: 0x06001E57 RID: 7767 RVA: 0x00056CBE File Offset: 0x00054EBE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator |(int4x4 lhs, int rhs)
		{
			return new int4x4(lhs.c0 | rhs, lhs.c1 | rhs, lhs.c2 | rhs, lhs.c3 | rhs);
		}

		// Token: 0x06001E58 RID: 7768 RVA: 0x00056CF5 File Offset: 0x00054EF5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator |(int lhs, int4x4 rhs)
		{
			return new int4x4(lhs | rhs.c0, lhs | rhs.c1, lhs | rhs.c2, lhs | rhs.c3);
		}

		// Token: 0x06001E59 RID: 7769 RVA: 0x00056D2C File Offset: 0x00054F2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator ^(int4x4 lhs, int4x4 rhs)
		{
			return new int4x4(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1, lhs.c2 ^ rhs.c2, lhs.c3 ^ rhs.c3);
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x00056D82 File Offset: 0x00054F82
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator ^(int4x4 lhs, int rhs)
		{
			return new int4x4(lhs.c0 ^ rhs, lhs.c1 ^ rhs, lhs.c2 ^ rhs, lhs.c3 ^ rhs);
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x00056DB9 File Offset: 0x00054FB9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 operator ^(int lhs, int4x4 rhs)
		{
			return new int4x4(lhs ^ rhs.c0, lhs ^ rhs.c1, lhs ^ rhs.c2, lhs ^ rhs.c3);
		}

		// Token: 0x1700099C RID: 2460
		public unsafe ref int4 this[int index]
		{
			get
			{
				fixed (int4x4* ptr = &this)
				{
					return ref *(int4*)(ptr + (IntPtr)index * (IntPtr)sizeof(int4) / (IntPtr)sizeof(int4x4));
				}
			}
		}

		// Token: 0x06001E5D RID: 7773 RVA: 0x00056E0C File Offset: 0x0005500C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(int4x4 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2) && this.c3.Equals(rhs.c3);
		}

		// Token: 0x06001E5E RID: 7774 RVA: 0x00056E68 File Offset: 0x00055068
		public override bool Equals(object o)
		{
			if (o is int4x4)
			{
				int4x4 converted = (int4x4)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06001E5F RID: 7775 RVA: 0x00056E8D File Offset: 0x0005508D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06001E60 RID: 7776 RVA: 0x00056E9C File Offset: 0x0005509C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("int4x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7},  {8}, {9}, {10}, {11},  {12}, {13}, {14}, {15})", new object[]
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
				this.c3.z,
				this.c0.w,
				this.c1.w,
				this.c2.w,
				this.c3.w
			});
		}

		// Token: 0x06001E61 RID: 7777 RVA: 0x00056FF4 File Offset: 0x000551F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("int4x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7},  {8}, {9}, {10}, {11},  {12}, {13}, {14}, {15})", new object[]
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
				this.c3.z.ToString(format, formatProvider),
				this.c0.w.ToString(format, formatProvider),
				this.c1.w.ToString(format, formatProvider),
				this.c2.w.ToString(format, formatProvider),
				this.c3.w.ToString(format, formatProvider)
			});
		}

		// Token: 0x0400012F RID: 303
		public int4 c0;

		// Token: 0x04000130 RID: 304
		public int4 c1;

		// Token: 0x04000131 RID: 305
		public int4 c2;

		// Token: 0x04000132 RID: 306
		public int4 c3;

		// Token: 0x04000133 RID: 307
		public static readonly int4x4 identity = new int4x4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);

		// Token: 0x04000134 RID: 308
		public static readonly int4x4 zero;
	}
}
