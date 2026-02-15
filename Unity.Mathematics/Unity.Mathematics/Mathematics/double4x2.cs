using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000028 RID: 40
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct double4x2 : IEquatable<double4x2>, IFormattable
	{
		// Token: 0x06000FD0 RID: 4048 RVA: 0x000334D1 File Offset: 0x000316D1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x2(double4 c0, double4 c1)
		{
			this.c0 = c0;
			this.c1 = c1;
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x000334E1 File Offset: 0x000316E1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x2(double m00, double m01, double m10, double m11, double m20, double m21, double m30, double m31)
		{
			this.c0 = new double4(m00, m10, m20, m30);
			this.c1 = new double4(m01, m11, m21, m31);
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x00033506 File Offset: 0x00031706
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x2(double v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x00033520 File Offset: 0x00031720
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x2(bool v)
		{
			this.c0 = math.select(new double4(0.0), new double4(1.0), v);
			this.c1 = math.select(new double4(0.0), new double4(1.0), v);
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x00033580 File Offset: 0x00031780
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x2(bool4x2 v)
		{
			this.c0 = math.select(new double4(0.0), new double4(1.0), v.c0);
			this.c1 = math.select(new double4(0.0), new double4(1.0), v.c1);
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x000335E7 File Offset: 0x000317E7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x2(int v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x00033601 File Offset: 0x00031801
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x2(int4x2 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x00033625 File Offset: 0x00031825
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x2(uint v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x0003363F File Offset: 0x0003183F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x2(uint4x2 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x00033663 File Offset: 0x00031863
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x2(float v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x0003367D File Offset: 0x0003187D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x2(float4x2 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x0000ACB9 File Offset: 0x00008EB9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x2(double v)
		{
			return new double4x2(v);
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x0000ACC1 File Offset: 0x00008EC1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double4x2(bool v)
		{
			return new double4x2(v);
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x0000ACC9 File Offset: 0x00008EC9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double4x2(bool4x2 v)
		{
			return new double4x2(v);
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x0000ACD1 File Offset: 0x00008ED1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x2(int v)
		{
			return new double4x2(v);
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x0000ACD9 File Offset: 0x00008ED9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x2(int4x2 v)
		{
			return new double4x2(v);
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x0000ACE1 File Offset: 0x00008EE1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x2(uint v)
		{
			return new double4x2(v);
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x0000ACE9 File Offset: 0x00008EE9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x2(uint4x2 v)
		{
			return new double4x2(v);
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x0000ACF1 File Offset: 0x00008EF1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x2(float v)
		{
			return new double4x2(v);
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x0000ACF9 File Offset: 0x00008EF9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x2(float4x2 v)
		{
			return new double4x2(v);
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x000336A1 File Offset: 0x000318A1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 operator *(double4x2 lhs, double4x2 rhs)
		{
			return new double4x2(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1);
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x000336CA File Offset: 0x000318CA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 operator *(double4x2 lhs, double rhs)
		{
			return new double4x2(lhs.c0 * rhs, lhs.c1 * rhs);
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x000336E9 File Offset: 0x000318E9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 operator *(double lhs, double4x2 rhs)
		{
			return new double4x2(lhs * rhs.c0, lhs * rhs.c1);
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x00033708 File Offset: 0x00031908
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 operator +(double4x2 lhs, double4x2 rhs)
		{
			return new double4x2(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1);
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x00033731 File Offset: 0x00031931
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 operator +(double4x2 lhs, double rhs)
		{
			return new double4x2(lhs.c0 + rhs, lhs.c1 + rhs);
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x00033750 File Offset: 0x00031950
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 operator +(double lhs, double4x2 rhs)
		{
			return new double4x2(lhs + rhs.c0, lhs + rhs.c1);
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x0003376F File Offset: 0x0003196F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 operator -(double4x2 lhs, double4x2 rhs)
		{
			return new double4x2(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1);
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x00033798 File Offset: 0x00031998
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 operator -(double4x2 lhs, double rhs)
		{
			return new double4x2(lhs.c0 - rhs, lhs.c1 - rhs);
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x000337B7 File Offset: 0x000319B7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 operator -(double lhs, double4x2 rhs)
		{
			return new double4x2(lhs - rhs.c0, lhs - rhs.c1);
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x000337D6 File Offset: 0x000319D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 operator /(double4x2 lhs, double4x2 rhs)
		{
			return new double4x2(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1);
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x000337FF File Offset: 0x000319FF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 operator /(double4x2 lhs, double rhs)
		{
			return new double4x2(lhs.c0 / rhs, lhs.c1 / rhs);
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x0003381E File Offset: 0x00031A1E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 operator /(double lhs, double4x2 rhs)
		{
			return new double4x2(lhs / rhs.c0, lhs / rhs.c1);
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x0003383D File Offset: 0x00031A3D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 operator %(double4x2 lhs, double4x2 rhs)
		{
			return new double4x2(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1);
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x00033866 File Offset: 0x00031A66
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 operator %(double4x2 lhs, double rhs)
		{
			return new double4x2(lhs.c0 % rhs, lhs.c1 % rhs);
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x00033885 File Offset: 0x00031A85
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 operator %(double lhs, double4x2 rhs)
		{
			return new double4x2(lhs % rhs.c0, lhs % rhs.c1);
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x000338A4 File Offset: 0x00031AA4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 operator ++(double4x2 val)
		{
			double4 @double = double4.op_Increment(val.c0);
			val.c0 = @double;
			double4 double2 = @double;
			@double = double4.op_Increment(val.c1);
			val.c1 = @double;
			return new double4x2(double2, @double);
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x000338EC File Offset: 0x00031AEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 operator --(double4x2 val)
		{
			double4 @double = double4.op_Decrement(val.c0);
			val.c0 = @double;
			double4 double2 = @double;
			@double = double4.op_Decrement(val.c1);
			val.c1 = @double;
			return new double4x2(double2, @double);
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x00033932 File Offset: 0x00031B32
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <(double4x2 lhs, double4x2 rhs)
		{
			return new bool4x2(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1);
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x0003395B File Offset: 0x00031B5B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <(double4x2 lhs, double rhs)
		{
			return new bool4x2(lhs.c0 < rhs, lhs.c1 < rhs);
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x0003397A File Offset: 0x00031B7A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <(double lhs, double4x2 rhs)
		{
			return new bool4x2(lhs < rhs.c0, lhs < rhs.c1);
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x00033999 File Offset: 0x00031B99
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <=(double4x2 lhs, double4x2 rhs)
		{
			return new bool4x2(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1);
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x000339C2 File Offset: 0x00031BC2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <=(double4x2 lhs, double rhs)
		{
			return new bool4x2(lhs.c0 <= rhs, lhs.c1 <= rhs);
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x000339E1 File Offset: 0x00031BE1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <=(double lhs, double4x2 rhs)
		{
			return new bool4x2(lhs <= rhs.c0, lhs <= rhs.c1);
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x00033A00 File Offset: 0x00031C00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >(double4x2 lhs, double4x2 rhs)
		{
			return new bool4x2(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1);
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x00033A29 File Offset: 0x00031C29
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >(double4x2 lhs, double rhs)
		{
			return new bool4x2(lhs.c0 > rhs, lhs.c1 > rhs);
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x00033A48 File Offset: 0x00031C48
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >(double lhs, double4x2 rhs)
		{
			return new bool4x2(lhs > rhs.c0, lhs > rhs.c1);
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x00033A67 File Offset: 0x00031C67
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >=(double4x2 lhs, double4x2 rhs)
		{
			return new bool4x2(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1);
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x00033A90 File Offset: 0x00031C90
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >=(double4x2 lhs, double rhs)
		{
			return new bool4x2(lhs.c0 >= rhs, lhs.c1 >= rhs);
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x00033AAF File Offset: 0x00031CAF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >=(double lhs, double4x2 rhs)
		{
			return new bool4x2(lhs >= rhs.c0, lhs >= rhs.c1);
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x00033ACE File Offset: 0x00031CCE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 operator -(double4x2 val)
		{
			return new double4x2(-val.c0, -val.c1);
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x00033AEB File Offset: 0x00031CEB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 operator +(double4x2 val)
		{
			return new double4x2(+val.c0, +val.c1);
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x00033B08 File Offset: 0x00031D08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator ==(double4x2 lhs, double4x2 rhs)
		{
			return new bool4x2(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1);
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x00033B31 File Offset: 0x00031D31
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator ==(double4x2 lhs, double rhs)
		{
			return new bool4x2(lhs.c0 == rhs, lhs.c1 == rhs);
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x00033B50 File Offset: 0x00031D50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator ==(double lhs, double4x2 rhs)
		{
			return new bool4x2(lhs == rhs.c0, lhs == rhs.c1);
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x00033B6F File Offset: 0x00031D6F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator !=(double4x2 lhs, double4x2 rhs)
		{
			return new bool4x2(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1);
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x00033B98 File Offset: 0x00031D98
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator !=(double4x2 lhs, double rhs)
		{
			return new bool4x2(lhs.c0 != rhs, lhs.c1 != rhs);
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x00033BB7 File Offset: 0x00031DB7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator !=(double lhs, double4x2 rhs)
		{
			return new bool4x2(lhs != rhs.c0, lhs != rhs.c1);
		}

		// Token: 0x170003D8 RID: 984
		public unsafe ref double4 this[int index]
		{
			get
			{
				fixed (double4x2* ptr = &this)
				{
					return ref *(double4*)(ptr + (IntPtr)index * (IntPtr)sizeof(double4) / (IntPtr)sizeof(double4x2));
				}
			}
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x00033BF3 File Offset: 0x00031DF3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(double4x2 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1);
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x00033C1C File Offset: 0x00031E1C
		public override bool Equals(object o)
		{
			if (o is double4x2)
			{
				double4x2 converted = (double4x2)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x00033C41 File Offset: 0x00031E41
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x00033C50 File Offset: 0x00031E50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("double4x2({0}, {1},  {2}, {3},  {4}, {5},  {6}, {7})", new object[]
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

		// Token: 0x0600100E RID: 4110 RVA: 0x00033D08 File Offset: 0x00031F08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("double4x2({0}, {1},  {2}, {3},  {4}, {5},  {6}, {7})", new object[]
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

		// Token: 0x04000099 RID: 153
		public double4 c0;

		// Token: 0x0400009A RID: 154
		public double4 c1;

		// Token: 0x0400009B RID: 155
		public static readonly double4x2 zero;
	}
}
