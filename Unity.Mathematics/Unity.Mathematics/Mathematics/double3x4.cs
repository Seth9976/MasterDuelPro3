using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000025 RID: 37
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct double3x4 : IEquatable<double3x4>, IFormattable
	{
		// Token: 0x06000DBA RID: 3514 RVA: 0x0002E7C4 File Offset: 0x0002C9C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x4(double3 c0, double3 c1, double3 c2, double3 c3)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
			this.c3 = c3;
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x0002E7E4 File Offset: 0x0002C9E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x4(double m00, double m01, double m02, double m03, double m10, double m11, double m12, double m13, double m20, double m21, double m22, double m23)
		{
			this.c0 = new double3(m00, m10, m20);
			this.c1 = new double3(m01, m11, m21);
			this.c2 = new double3(m02, m12, m22);
			this.c3 = new double3(m03, m13, m23);
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x0002E832 File Offset: 0x0002CA32
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x4(double v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0002E864 File Offset: 0x0002CA64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x4(bool v)
		{
			this.c0 = math.select(new double3(0.0), new double3(1.0), v);
			this.c1 = math.select(new double3(0.0), new double3(1.0), v);
			this.c2 = math.select(new double3(0.0), new double3(1.0), v);
			this.c3 = math.select(new double3(0.0), new double3(1.0), v);
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x0002E914 File Offset: 0x0002CB14
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x4(bool3x4 v)
		{
			this.c0 = math.select(new double3(0.0), new double3(1.0), v.c0);
			this.c1 = math.select(new double3(0.0), new double3(1.0), v.c1);
			this.c2 = math.select(new double3(0.0), new double3(1.0), v.c2);
			this.c3 = math.select(new double3(0.0), new double3(1.0), v.c3);
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x0002E9D5 File Offset: 0x0002CBD5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x4(int v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x0002EA08 File Offset: 0x0002CC08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x4(int3x4 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
			this.c3 = v.c3;
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x0002EA59 File Offset: 0x0002CC59
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x4(uint v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x0002EA8C File Offset: 0x0002CC8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x4(uint3x4 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
			this.c3 = v.c3;
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x0002EADD File Offset: 0x0002CCDD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x4(float v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x0002EB10 File Offset: 0x0002CD10
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x4(float3x4 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
			this.c3 = v.c3;
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x0000A79E File Offset: 0x0000899E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3x4(double v)
		{
			return new double3x4(v);
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x0000A7A6 File Offset: 0x000089A6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double3x4(bool v)
		{
			return new double3x4(v);
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x0000A7AE File Offset: 0x000089AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double3x4(bool3x4 v)
		{
			return new double3x4(v);
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x0000A7B6 File Offset: 0x000089B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3x4(int v)
		{
			return new double3x4(v);
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x0000A7BE File Offset: 0x000089BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3x4(int3x4 v)
		{
			return new double3x4(v);
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x0000A7C6 File Offset: 0x000089C6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3x4(uint v)
		{
			return new double3x4(v);
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x0000A7CE File Offset: 0x000089CE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3x4(uint3x4 v)
		{
			return new double3x4(v);
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x0000A7D6 File Offset: 0x000089D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3x4(float v)
		{
			return new double3x4(v);
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x0000A7DE File Offset: 0x000089DE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3x4(float3x4 v)
		{
			return new double3x4(v);
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x0002EB64 File Offset: 0x0002CD64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 operator *(double3x4 lhs, double3x4 rhs)
		{
			return new double3x4(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2, lhs.c3 * rhs.c3);
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x0002EBBA File Offset: 0x0002CDBA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 operator *(double3x4 lhs, double rhs)
		{
			return new double3x4(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs, lhs.c3 * rhs);
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x0002EBF1 File Offset: 0x0002CDF1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 operator *(double lhs, double3x4 rhs)
		{
			return new double3x4(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2, lhs * rhs.c3);
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x0002EC28 File Offset: 0x0002CE28
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 operator +(double3x4 lhs, double3x4 rhs)
		{
			return new double3x4(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2, lhs.c3 + rhs.c3);
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x0002EC7E File Offset: 0x0002CE7E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 operator +(double3x4 lhs, double rhs)
		{
			return new double3x4(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs, lhs.c3 + rhs);
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x0002ECB5 File Offset: 0x0002CEB5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 operator +(double lhs, double3x4 rhs)
		{
			return new double3x4(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2, lhs + rhs.c3);
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x0002ECEC File Offset: 0x0002CEEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 operator -(double3x4 lhs, double3x4 rhs)
		{
			return new double3x4(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2, lhs.c3 - rhs.c3);
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x0002ED42 File Offset: 0x0002CF42
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 operator -(double3x4 lhs, double rhs)
		{
			return new double3x4(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs, lhs.c3 - rhs);
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x0002ED79 File Offset: 0x0002CF79
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 operator -(double lhs, double3x4 rhs)
		{
			return new double3x4(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2, lhs - rhs.c3);
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x0002EDB0 File Offset: 0x0002CFB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 operator /(double3x4 lhs, double3x4 rhs)
		{
			return new double3x4(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2, lhs.c3 / rhs.c3);
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x0002EE06 File Offset: 0x0002D006
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 operator /(double3x4 lhs, double rhs)
		{
			return new double3x4(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs, lhs.c3 / rhs);
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x0002EE3D File Offset: 0x0002D03D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 operator /(double lhs, double3x4 rhs)
		{
			return new double3x4(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2, lhs / rhs.c3);
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x0002EE74 File Offset: 0x0002D074
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 operator %(double3x4 lhs, double3x4 rhs)
		{
			return new double3x4(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2, lhs.c3 % rhs.c3);
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x0002EECA File Offset: 0x0002D0CA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 operator %(double3x4 lhs, double rhs)
		{
			return new double3x4(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs, lhs.c3 % rhs);
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x0002EF01 File Offset: 0x0002D101
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 operator %(double lhs, double3x4 rhs)
		{
			return new double3x4(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2, lhs % rhs.c3);
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x0002EF38 File Offset: 0x0002D138
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 operator ++(double3x4 val)
		{
			double3 @double = double3.op_Increment(val.c0);
			val.c0 = @double;
			double3 double2 = @double;
			@double = double3.op_Increment(val.c1);
			val.c1 = @double;
			double3 double3 = @double;
			@double = double3.op_Increment(val.c2);
			val.c2 = @double;
			double3 double4 = @double;
			@double = double3.op_Increment(val.c3);
			val.c3 = @double;
			return new double3x4(double2, double3, double4, @double);
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x0002EFB4 File Offset: 0x0002D1B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 operator --(double3x4 val)
		{
			double3 @double = double3.op_Decrement(val.c0);
			val.c0 = @double;
			double3 double2 = @double;
			@double = double3.op_Decrement(val.c1);
			val.c1 = @double;
			double3 double3 = @double;
			@double = double3.op_Decrement(val.c2);
			val.c2 = @double;
			double3 double4 = @double;
			@double = double3.op_Decrement(val.c3);
			val.c3 = @double;
			return new double3x4(double2, double3, double4, @double);
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x0002F030 File Offset: 0x0002D230
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <(double3x4 lhs, double3x4 rhs)
		{
			return new bool3x4(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2, lhs.c3 < rhs.c3);
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x0002F086 File Offset: 0x0002D286
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <(double3x4 lhs, double rhs)
		{
			return new bool3x4(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs, lhs.c3 < rhs);
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x0002F0BD File Offset: 0x0002D2BD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <(double lhs, double3x4 rhs)
		{
			return new bool3x4(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2, lhs < rhs.c3);
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x0002F0F4 File Offset: 0x0002D2F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <=(double3x4 lhs, double3x4 rhs)
		{
			return new bool3x4(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2, lhs.c3 <= rhs.c3);
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x0002F14A File Offset: 0x0002D34A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <=(double3x4 lhs, double rhs)
		{
			return new bool3x4(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs, lhs.c3 <= rhs);
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x0002F181 File Offset: 0x0002D381
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <=(double lhs, double3x4 rhs)
		{
			return new bool3x4(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2, lhs <= rhs.c3);
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x0002F1B8 File Offset: 0x0002D3B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >(double3x4 lhs, double3x4 rhs)
		{
			return new bool3x4(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2, lhs.c3 > rhs.c3);
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x0002F20E File Offset: 0x0002D40E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >(double3x4 lhs, double rhs)
		{
			return new bool3x4(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs, lhs.c3 > rhs);
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x0002F245 File Offset: 0x0002D445
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >(double lhs, double3x4 rhs)
		{
			return new bool3x4(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2, lhs > rhs.c3);
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x0002F27C File Offset: 0x0002D47C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >=(double3x4 lhs, double3x4 rhs)
		{
			return new bool3x4(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2, lhs.c3 >= rhs.c3);
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x0002F2D2 File Offset: 0x0002D4D2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >=(double3x4 lhs, double rhs)
		{
			return new bool3x4(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs, lhs.c3 >= rhs);
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x0002F309 File Offset: 0x0002D509
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >=(double lhs, double3x4 rhs)
		{
			return new bool3x4(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2, lhs >= rhs.c3);
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x0002F340 File Offset: 0x0002D540
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 operator -(double3x4 val)
		{
			return new double3x4(-val.c0, -val.c1, -val.c2, -val.c3);
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x0002F373 File Offset: 0x0002D573
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 operator +(double3x4 val)
		{
			return new double3x4(+val.c0, +val.c1, +val.c2, +val.c3);
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x0002F3A8 File Offset: 0x0002D5A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator ==(double3x4 lhs, double3x4 rhs)
		{
			return new bool3x4(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2, lhs.c3 == rhs.c3);
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x0002F3FE File Offset: 0x0002D5FE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator ==(double3x4 lhs, double rhs)
		{
			return new bool3x4(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs, lhs.c3 == rhs);
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x0002F435 File Offset: 0x0002D635
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator ==(double lhs, double3x4 rhs)
		{
			return new bool3x4(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2, lhs == rhs.c3);
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x0002F46C File Offset: 0x0002D66C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator !=(double3x4 lhs, double3x4 rhs)
		{
			return new bool3x4(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2, lhs.c3 != rhs.c3);
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x0002F4C2 File Offset: 0x0002D6C2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator !=(double3x4 lhs, double rhs)
		{
			return new bool3x4(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs, lhs.c3 != rhs);
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x0002F4F9 File Offset: 0x0002D6F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator !=(double lhs, double3x4 rhs)
		{
			return new bool3x4(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2, lhs != rhs.c3);
		}

		// Token: 0x17000286 RID: 646
		public unsafe ref double3 this[int index]
		{
			get
			{
				fixed (double3x4* ptr = &this)
				{
					return ref *(double3*)(ptr + (IntPtr)index * (IntPtr)sizeof(double3) / (IntPtr)sizeof(double3x4));
				}
			}
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x0002F54C File Offset: 0x0002D74C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(double3x4 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2) && this.c3.Equals(rhs.c3);
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x0002F5A8 File Offset: 0x0002D7A8
		public override bool Equals(object o)
		{
			if (o is double3x4)
			{
				double3x4 converted = (double3x4)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x0002F5CD File Offset: 0x0002D7CD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x0002F5DC File Offset: 0x0002D7DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("double3x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7},  {8}, {9}, {10}, {11})", new object[]
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

		// Token: 0x06000DF8 RID: 3576 RVA: 0x0002F6E4 File Offset: 0x0002D8E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("double3x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7},  {8}, {9}, {10}, {11})", new object[]
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

		// Token: 0x0400008B RID: 139
		public double3 c0;

		// Token: 0x0400008C RID: 140
		public double3 c1;

		// Token: 0x0400008D RID: 141
		public double3 c2;

		// Token: 0x0400008E RID: 142
		public double3 c3;

		// Token: 0x0400008F RID: 143
		public static readonly double3x4 zero;
	}
}
