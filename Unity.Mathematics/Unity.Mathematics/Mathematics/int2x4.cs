using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000045 RID: 69
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct int2x4 : IEquatable<int2x4>, IFormattable
	{
		// Token: 0x060019A5 RID: 6565 RVA: 0x0004B2A7 File Offset: 0x000494A7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x4(int2 c0, int2 c1, int2 c2, int2 c3)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
			this.c3 = c3;
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x0004B2C6 File Offset: 0x000494C6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x4(int m00, int m01, int m02, int m03, int m10, int m11, int m12, int m13)
		{
			this.c0 = new int2(m00, m10);
			this.c1 = new int2(m01, m11);
			this.c2 = new int2(m02, m12);
			this.c3 = new int2(m03, m13);
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x0004B301 File Offset: 0x00049501
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x4(int v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x0004B334 File Offset: 0x00049534
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x4(bool v)
		{
			this.c0 = math.select(new int2(0), new int2(1), v);
			this.c1 = math.select(new int2(0), new int2(1), v);
			this.c2 = math.select(new int2(0), new int2(1), v);
			this.c3 = math.select(new int2(0), new int2(1), v);
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x0004B3A4 File Offset: 0x000495A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x4(bool2x4 v)
		{
			this.c0 = math.select(new int2(0), new int2(1), v.c0);
			this.c1 = math.select(new int2(0), new int2(1), v.c1);
			this.c2 = math.select(new int2(0), new int2(1), v.c2);
			this.c3 = math.select(new int2(0), new int2(1), v.c3);
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x0004B425 File Offset: 0x00049625
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x4(uint v)
		{
			this.c0 = (int2)v;
			this.c1 = (int2)v;
			this.c2 = (int2)v;
			this.c3 = (int2)v;
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x0004B458 File Offset: 0x00049658
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x4(uint2x4 v)
		{
			this.c0 = (int2)v.c0;
			this.c1 = (int2)v.c1;
			this.c2 = (int2)v.c2;
			this.c3 = (int2)v.c3;
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x0004B4A9 File Offset: 0x000496A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x4(float v)
		{
			this.c0 = (int2)v;
			this.c1 = (int2)v;
			this.c2 = (int2)v;
			this.c3 = (int2)v;
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x0004B4DC File Offset: 0x000496DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x4(float2x4 v)
		{
			this.c0 = (int2)v.c0;
			this.c1 = (int2)v.c1;
			this.c2 = (int2)v.c2;
			this.c3 = (int2)v.c3;
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x0004B52D File Offset: 0x0004972D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x4(double v)
		{
			this.c0 = (int2)v;
			this.c1 = (int2)v;
			this.c2 = (int2)v;
			this.c3 = (int2)v;
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x0004B560 File Offset: 0x00049760
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2x4(double2x4 v)
		{
			this.c0 = (int2)v.c0;
			this.c1 = (int2)v.c1;
			this.c2 = (int2)v.c2;
			this.c3 = (int2)v.c3;
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x0000E180 File Offset: 0x0000C380
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator int2x4(int v)
		{
			return new int2x4(v);
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x0000E188 File Offset: 0x0000C388
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x4(bool v)
		{
			return new int2x4(v);
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x0000E190 File Offset: 0x0000C390
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x4(bool2x4 v)
		{
			return new int2x4(v);
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x0000E198 File Offset: 0x0000C398
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x4(uint v)
		{
			return new int2x4(v);
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x0000E1A0 File Offset: 0x0000C3A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x4(uint2x4 v)
		{
			return new int2x4(v);
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x0000E1A8 File Offset: 0x0000C3A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x4(float v)
		{
			return new int2x4(v);
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x0000E1B0 File Offset: 0x0000C3B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x4(float2x4 v)
		{
			return new int2x4(v);
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x0000E1B8 File Offset: 0x0000C3B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x4(double v)
		{
			return new int2x4(v);
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x0000E1C0 File Offset: 0x0000C3C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2x4(double2x4 v)
		{
			return new int2x4(v);
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x0004B5B4 File Offset: 0x000497B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator *(int2x4 lhs, int2x4 rhs)
		{
			return new int2x4(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2, lhs.c3 * rhs.c3);
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x0004B60A File Offset: 0x0004980A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator *(int2x4 lhs, int rhs)
		{
			return new int2x4(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs, lhs.c3 * rhs);
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x0004B641 File Offset: 0x00049841
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator *(int lhs, int2x4 rhs)
		{
			return new int2x4(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2, lhs * rhs.c3);
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x0004B678 File Offset: 0x00049878
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator +(int2x4 lhs, int2x4 rhs)
		{
			return new int2x4(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2, lhs.c3 + rhs.c3);
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x0004B6CE File Offset: 0x000498CE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator +(int2x4 lhs, int rhs)
		{
			return new int2x4(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs, lhs.c3 + rhs);
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x0004B705 File Offset: 0x00049905
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator +(int lhs, int2x4 rhs)
		{
			return new int2x4(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2, lhs + rhs.c3);
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x0004B73C File Offset: 0x0004993C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator -(int2x4 lhs, int2x4 rhs)
		{
			return new int2x4(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2, lhs.c3 - rhs.c3);
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x0004B792 File Offset: 0x00049992
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator -(int2x4 lhs, int rhs)
		{
			return new int2x4(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs, lhs.c3 - rhs);
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x0004B7C9 File Offset: 0x000499C9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator -(int lhs, int2x4 rhs)
		{
			return new int2x4(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2, lhs - rhs.c3);
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x0004B800 File Offset: 0x00049A00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator /(int2x4 lhs, int2x4 rhs)
		{
			return new int2x4(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2, lhs.c3 / rhs.c3);
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x0004B856 File Offset: 0x00049A56
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator /(int2x4 lhs, int rhs)
		{
			return new int2x4(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs, lhs.c3 / rhs);
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x0004B88D File Offset: 0x00049A8D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator /(int lhs, int2x4 rhs)
		{
			return new int2x4(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2, lhs / rhs.c3);
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x0004B8C4 File Offset: 0x00049AC4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator %(int2x4 lhs, int2x4 rhs)
		{
			return new int2x4(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2, lhs.c3 % rhs.c3);
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x0004B91A File Offset: 0x00049B1A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator %(int2x4 lhs, int rhs)
		{
			return new int2x4(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs, lhs.c3 % rhs);
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x0004B951 File Offset: 0x00049B51
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator %(int lhs, int2x4 rhs)
		{
			return new int2x4(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2, lhs % rhs.c3);
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x0004B988 File Offset: 0x00049B88
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator ++(int2x4 val)
		{
			int2 @int = int2.op_Increment(val.c0);
			val.c0 = @int;
			int2 int2 = @int;
			@int = int2.op_Increment(val.c1);
			val.c1 = @int;
			int2 int3 = @int;
			@int = int2.op_Increment(val.c2);
			val.c2 = @int;
			int2 int4 = @int;
			@int = int2.op_Increment(val.c3);
			val.c3 = @int;
			return new int2x4(int2, int3, int4, @int);
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x0004BA04 File Offset: 0x00049C04
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator --(int2x4 val)
		{
			int2 @int = int2.op_Decrement(val.c0);
			val.c0 = @int;
			int2 int2 = @int;
			@int = int2.op_Decrement(val.c1);
			val.c1 = @int;
			int2 int3 = @int;
			@int = int2.op_Decrement(val.c2);
			val.c2 = @int;
			int2 int4 = @int;
			@int = int2.op_Decrement(val.c3);
			val.c3 = @int;
			return new int2x4(int2, int3, int4, @int);
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x0004BA80 File Offset: 0x00049C80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator <(int2x4 lhs, int2x4 rhs)
		{
			return new bool2x4(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2, lhs.c3 < rhs.c3);
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x0004BAD6 File Offset: 0x00049CD6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator <(int2x4 lhs, int rhs)
		{
			return new bool2x4(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs, lhs.c3 < rhs);
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x0004BB0D File Offset: 0x00049D0D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator <(int lhs, int2x4 rhs)
		{
			return new bool2x4(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2, lhs < rhs.c3);
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x0004BB44 File Offset: 0x00049D44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator <=(int2x4 lhs, int2x4 rhs)
		{
			return new bool2x4(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2, lhs.c3 <= rhs.c3);
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x0004BB9A File Offset: 0x00049D9A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator <=(int2x4 lhs, int rhs)
		{
			return new bool2x4(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs, lhs.c3 <= rhs);
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x0004BBD1 File Offset: 0x00049DD1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator <=(int lhs, int2x4 rhs)
		{
			return new bool2x4(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2, lhs <= rhs.c3);
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x0004BC08 File Offset: 0x00049E08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator >(int2x4 lhs, int2x4 rhs)
		{
			return new bool2x4(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2, lhs.c3 > rhs.c3);
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x0004BC5E File Offset: 0x00049E5E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator >(int2x4 lhs, int rhs)
		{
			return new bool2x4(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs, lhs.c3 > rhs);
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x0004BC95 File Offset: 0x00049E95
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator >(int lhs, int2x4 rhs)
		{
			return new bool2x4(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2, lhs > rhs.c3);
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x0004BCCC File Offset: 0x00049ECC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator >=(int2x4 lhs, int2x4 rhs)
		{
			return new bool2x4(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2, lhs.c3 >= rhs.c3);
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x0004BD22 File Offset: 0x00049F22
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator >=(int2x4 lhs, int rhs)
		{
			return new bool2x4(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs, lhs.c3 >= rhs);
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x0004BD59 File Offset: 0x00049F59
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator >=(int lhs, int2x4 rhs)
		{
			return new bool2x4(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2, lhs >= rhs.c3);
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x0004BD90 File Offset: 0x00049F90
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator -(int2x4 val)
		{
			return new int2x4(-val.c0, -val.c1, -val.c2, -val.c3);
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x0004BDC3 File Offset: 0x00049FC3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator +(int2x4 val)
		{
			return new int2x4(+val.c0, +val.c1, +val.c2, +val.c3);
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x0004BDF6 File Offset: 0x00049FF6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator <<(int2x4 x, int n)
		{
			return new int2x4(x.c0 << n, x.c1 << n, x.c2 << n, x.c3 << n);
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x0004BE2D File Offset: 0x0004A02D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator >>(int2x4 x, int n)
		{
			return new int2x4(x.c0 >> n, x.c1 >> n, x.c2 >> n, x.c3 >> n);
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x0004BE64 File Offset: 0x0004A064
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator ==(int2x4 lhs, int2x4 rhs)
		{
			return new bool2x4(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2, lhs.c3 == rhs.c3);
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x0004BEBA File Offset: 0x0004A0BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator ==(int2x4 lhs, int rhs)
		{
			return new bool2x4(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs, lhs.c3 == rhs);
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x0004BEF1 File Offset: 0x0004A0F1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator ==(int lhs, int2x4 rhs)
		{
			return new bool2x4(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2, lhs == rhs.c3);
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x0004BF28 File Offset: 0x0004A128
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator !=(int2x4 lhs, int2x4 rhs)
		{
			return new bool2x4(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2, lhs.c3 != rhs.c3);
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x0004BF7E File Offset: 0x0004A17E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator !=(int2x4 lhs, int rhs)
		{
			return new bool2x4(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs, lhs.c3 != rhs);
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x0004BFB5 File Offset: 0x0004A1B5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator !=(int lhs, int2x4 rhs)
		{
			return new bool2x4(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2, lhs != rhs.c3);
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x0004BFEC File Offset: 0x0004A1EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator ~(int2x4 val)
		{
			return new int2x4(~val.c0, ~val.c1, ~val.c2, ~val.c3);
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x0004C020 File Offset: 0x0004A220
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator &(int2x4 lhs, int2x4 rhs)
		{
			return new int2x4(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1, lhs.c2 & rhs.c2, lhs.c3 & rhs.c3);
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x0004C076 File Offset: 0x0004A276
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator &(int2x4 lhs, int rhs)
		{
			return new int2x4(lhs.c0 & rhs, lhs.c1 & rhs, lhs.c2 & rhs, lhs.c3 & rhs);
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x0004C0AD File Offset: 0x0004A2AD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator &(int lhs, int2x4 rhs)
		{
			return new int2x4(lhs & rhs.c0, lhs & rhs.c1, lhs & rhs.c2, lhs & rhs.c3);
		}

		// Token: 0x060019E4 RID: 6628 RVA: 0x0004C0E4 File Offset: 0x0004A2E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator |(int2x4 lhs, int2x4 rhs)
		{
			return new int2x4(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1, lhs.c2 | rhs.c2, lhs.c3 | rhs.c3);
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x0004C13A File Offset: 0x0004A33A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator |(int2x4 lhs, int rhs)
		{
			return new int2x4(lhs.c0 | rhs, lhs.c1 | rhs, lhs.c2 | rhs, lhs.c3 | rhs);
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x0004C171 File Offset: 0x0004A371
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator |(int lhs, int2x4 rhs)
		{
			return new int2x4(lhs | rhs.c0, lhs | rhs.c1, lhs | rhs.c2, lhs | rhs.c3);
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x0004C1A8 File Offset: 0x0004A3A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator ^(int2x4 lhs, int2x4 rhs)
		{
			return new int2x4(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1, lhs.c2 ^ rhs.c2, lhs.c3 ^ rhs.c3);
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x0004C1FE File Offset: 0x0004A3FE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator ^(int2x4 lhs, int rhs)
		{
			return new int2x4(lhs.c0 ^ rhs, lhs.c1 ^ rhs, lhs.c2 ^ rhs, lhs.c3 ^ rhs);
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x0004C235 File Offset: 0x0004A435
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 operator ^(int lhs, int2x4 rhs)
		{
			return new int2x4(lhs ^ rhs.c0, lhs ^ rhs.c1, lhs ^ rhs.c2, lhs ^ rhs.c3);
		}

		// Token: 0x170007CF RID: 1999
		public unsafe ref int2 this[int index]
		{
			get
			{
				fixed (int2x4* ptr = &this)
				{
					return ref *(int2*)(ptr + (IntPtr)index * (IntPtr)sizeof(int2) / (IntPtr)sizeof(int2x4));
				}
			}
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x0004C288 File Offset: 0x0004A488
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(int2x4 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2) && this.c3.Equals(rhs.c3);
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x0004C2E4 File Offset: 0x0004A4E4
		public override bool Equals(object o)
		{
			if (o is int2x4)
			{
				int2x4 converted = (int2x4)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x0004C309 File Offset: 0x0004A509
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x0004C318 File Offset: 0x0004A518
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("int2x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c2.x,
				this.c3.x,
				this.c0.y,
				this.c1.y,
				this.c2.y,
				this.c3.y
			});
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x0004C3D0 File Offset: 0x0004A5D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("int2x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7})", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c2.x.ToString(format, formatProvider),
				this.c3.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c2.y.ToString(format, formatProvider),
				this.c3.y.ToString(format, formatProvider)
			});
		}

		// Token: 0x04000106 RID: 262
		public int2 c0;

		// Token: 0x04000107 RID: 263
		public int2 c1;

		// Token: 0x04000108 RID: 264
		public int2 c2;

		// Token: 0x04000109 RID: 265
		public int2 c3;

		// Token: 0x0400010A RID: 266
		public static readonly int2x4 zero;
	}
}
