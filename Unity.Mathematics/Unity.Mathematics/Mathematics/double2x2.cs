using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200001E RID: 30
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct double2x2 : IEquatable<double2x2>, IFormattable
	{
		// Token: 0x06000BB5 RID: 2997 RVA: 0x0002971A File Offset: 0x0002791A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x2(double2 c0, double2 c1)
		{
			this.c0 = c0;
			this.c1 = c1;
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0002972A File Offset: 0x0002792A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x2(double m00, double m01, double m10, double m11)
		{
			this.c0 = new double2(m00, m10);
			this.c1 = new double2(m01, m11);
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x00029747 File Offset: 0x00027947
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x2(double v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x00029764 File Offset: 0x00027964
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x2(bool v)
		{
			this.c0 = math.select(new double2(0.0), new double2(1.0), v);
			this.c1 = math.select(new double2(0.0), new double2(1.0), v);
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x000297C4 File Offset: 0x000279C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x2(bool2x2 v)
		{
			this.c0 = math.select(new double2(0.0), new double2(1.0), v.c0);
			this.c1 = math.select(new double2(0.0), new double2(1.0), v.c1);
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0002982B File Offset: 0x00027A2B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x2(int v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x00029845 File Offset: 0x00027A45
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x2(int2x2 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x00029869 File Offset: 0x00027A69
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x2(uint v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x00029883 File Offset: 0x00027A83
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x2(uint2x2 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x000298A7 File Offset: 0x00027AA7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x2(float v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x000298C1 File Offset: 0x00027AC1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x2(float2x2 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x00009AF5 File Offset: 0x00007CF5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2x2(double v)
		{
			return new double2x2(v);
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x00009AFD File Offset: 0x00007CFD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double2x2(bool v)
		{
			return new double2x2(v);
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x00009B05 File Offset: 0x00007D05
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double2x2(bool2x2 v)
		{
			return new double2x2(v);
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x00009B0D File Offset: 0x00007D0D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2x2(int v)
		{
			return new double2x2(v);
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x00009B15 File Offset: 0x00007D15
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2x2(int2x2 v)
		{
			return new double2x2(v);
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x00009B1D File Offset: 0x00007D1D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2x2(uint v)
		{
			return new double2x2(v);
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x00009B25 File Offset: 0x00007D25
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2x2(uint2x2 v)
		{
			return new double2x2(v);
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x00009B2D File Offset: 0x00007D2D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2x2(float v)
		{
			return new double2x2(v);
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x00009B35 File Offset: 0x00007D35
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2x2(float2x2 v)
		{
			return new double2x2(v);
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x000298E5 File Offset: 0x00027AE5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 operator *(double2x2 lhs, double2x2 rhs)
		{
			return new double2x2(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1);
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x0002990E File Offset: 0x00027B0E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 operator *(double2x2 lhs, double rhs)
		{
			return new double2x2(lhs.c0 * rhs, lhs.c1 * rhs);
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0002992D File Offset: 0x00027B2D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 operator *(double lhs, double2x2 rhs)
		{
			return new double2x2(lhs * rhs.c0, lhs * rhs.c1);
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x0002994C File Offset: 0x00027B4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 operator +(double2x2 lhs, double2x2 rhs)
		{
			return new double2x2(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1);
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x00029975 File Offset: 0x00027B75
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 operator +(double2x2 lhs, double rhs)
		{
			return new double2x2(lhs.c0 + rhs, lhs.c1 + rhs);
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x00029994 File Offset: 0x00027B94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 operator +(double lhs, double2x2 rhs)
		{
			return new double2x2(lhs + rhs.c0, lhs + rhs.c1);
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x000299B3 File Offset: 0x00027BB3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 operator -(double2x2 lhs, double2x2 rhs)
		{
			return new double2x2(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1);
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x000299DC File Offset: 0x00027BDC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 operator -(double2x2 lhs, double rhs)
		{
			return new double2x2(lhs.c0 - rhs, lhs.c1 - rhs);
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x000299FB File Offset: 0x00027BFB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 operator -(double lhs, double2x2 rhs)
		{
			return new double2x2(lhs - rhs.c0, lhs - rhs.c1);
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x00029A1A File Offset: 0x00027C1A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 operator /(double2x2 lhs, double2x2 rhs)
		{
			return new double2x2(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1);
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x00029A43 File Offset: 0x00027C43
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 operator /(double2x2 lhs, double rhs)
		{
			return new double2x2(lhs.c0 / rhs, lhs.c1 / rhs);
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00029A62 File Offset: 0x00027C62
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 operator /(double lhs, double2x2 rhs)
		{
			return new double2x2(lhs / rhs.c0, lhs / rhs.c1);
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x00029A81 File Offset: 0x00027C81
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 operator %(double2x2 lhs, double2x2 rhs)
		{
			return new double2x2(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1);
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x00029AAA File Offset: 0x00027CAA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 operator %(double2x2 lhs, double rhs)
		{
			return new double2x2(lhs.c0 % rhs, lhs.c1 % rhs);
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x00029AC9 File Offset: 0x00027CC9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 operator %(double lhs, double2x2 rhs)
		{
			return new double2x2(lhs % rhs.c0, lhs % rhs.c1);
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x00029AE8 File Offset: 0x00027CE8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 operator ++(double2x2 val)
		{
			double2 @double = double2.op_Increment(val.c0);
			val.c0 = @double;
			double2 double2 = @double;
			@double = double2.op_Increment(val.c1);
			val.c1 = @double;
			return new double2x2(double2, @double);
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x00029B30 File Offset: 0x00027D30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 operator --(double2x2 val)
		{
			double2 @double = double2.op_Decrement(val.c0);
			val.c0 = @double;
			double2 double2 = @double;
			@double = double2.op_Decrement(val.c1);
			val.c1 = @double;
			return new double2x2(double2, @double);
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x00029B76 File Offset: 0x00027D76
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <(double2x2 lhs, double2x2 rhs)
		{
			return new bool2x2(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1);
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x00029B9F File Offset: 0x00027D9F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <(double2x2 lhs, double rhs)
		{
			return new bool2x2(lhs.c0 < rhs, lhs.c1 < rhs);
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x00029BBE File Offset: 0x00027DBE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <(double lhs, double2x2 rhs)
		{
			return new bool2x2(lhs < rhs.c0, lhs < rhs.c1);
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x00029BDD File Offset: 0x00027DDD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <=(double2x2 lhs, double2x2 rhs)
		{
			return new bool2x2(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1);
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x00029C06 File Offset: 0x00027E06
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <=(double2x2 lhs, double rhs)
		{
			return new bool2x2(lhs.c0 <= rhs, lhs.c1 <= rhs);
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x00029C25 File Offset: 0x00027E25
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <=(double lhs, double2x2 rhs)
		{
			return new bool2x2(lhs <= rhs.c0, lhs <= rhs.c1);
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x00029C44 File Offset: 0x00027E44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >(double2x2 lhs, double2x2 rhs)
		{
			return new bool2x2(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1);
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x00029C6D File Offset: 0x00027E6D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >(double2x2 lhs, double rhs)
		{
			return new bool2x2(lhs.c0 > rhs, lhs.c1 > rhs);
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x00029C8C File Offset: 0x00027E8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >(double lhs, double2x2 rhs)
		{
			return new bool2x2(lhs > rhs.c0, lhs > rhs.c1);
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x00029CAB File Offset: 0x00027EAB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >=(double2x2 lhs, double2x2 rhs)
		{
			return new bool2x2(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1);
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x00029CD4 File Offset: 0x00027ED4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >=(double2x2 lhs, double rhs)
		{
			return new bool2x2(lhs.c0 >= rhs, lhs.c1 >= rhs);
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x00029CF3 File Offset: 0x00027EF3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >=(double lhs, double2x2 rhs)
		{
			return new bool2x2(lhs >= rhs.c0, lhs >= rhs.c1);
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x00029D12 File Offset: 0x00027F12
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 operator -(double2x2 val)
		{
			return new double2x2(-val.c0, -val.c1);
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00029D2F File Offset: 0x00027F2F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 operator +(double2x2 val)
		{
			return new double2x2(+val.c0, +val.c1);
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x00029D4C File Offset: 0x00027F4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator ==(double2x2 lhs, double2x2 rhs)
		{
			return new bool2x2(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1);
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x00029D75 File Offset: 0x00027F75
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator ==(double2x2 lhs, double rhs)
		{
			return new bool2x2(lhs.c0 == rhs, lhs.c1 == rhs);
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x00029D94 File Offset: 0x00027F94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator ==(double lhs, double2x2 rhs)
		{
			return new bool2x2(lhs == rhs.c0, lhs == rhs.c1);
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x00029DB3 File Offset: 0x00027FB3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator !=(double2x2 lhs, double2x2 rhs)
		{
			return new bool2x2(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1);
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x00029DDC File Offset: 0x00027FDC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator !=(double2x2 lhs, double rhs)
		{
			return new bool2x2(lhs.c0 != rhs, lhs.c1 != rhs);
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x00029DFB File Offset: 0x00027FFB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator !=(double lhs, double2x2 rhs)
		{
			return new bool2x2(lhs != rhs.c0, lhs != rhs.c1);
		}

		// Token: 0x1700020B RID: 523
		public unsafe ref double2 this[int index]
		{
			get
			{
				fixed (double2x2* ptr = &this)
				{
					return ref *(double2*)(ptr + (IntPtr)index * (IntPtr)sizeof(double2) / (IntPtr)sizeof(double2x2));
				}
			}
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x00029E37 File Offset: 0x00028037
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(double2x2 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1);
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x00029E60 File Offset: 0x00028060
		public override bool Equals(object o)
		{
			if (o is double2x2)
			{
				double2x2 converted = (double2x2)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x00029E85 File Offset: 0x00028085
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x00029E94 File Offset: 0x00028094
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("double2x2({0}, {1},  {2}, {3})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c0.y,
				this.c1.y
			});
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x00029F00 File Offset: 0x00028100
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("double2x2({0}, {1},  {2}, {3})", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider)
			});
		}

		// Token: 0x0400006F RID: 111
		public double2 c0;

		// Token: 0x04000070 RID: 112
		public double2 c1;

		// Token: 0x04000071 RID: 113
		public static readonly double2x2 identity = new double2x2(1.0, 0.0, 0.0, 1.0);

		// Token: 0x04000072 RID: 114
		public static readonly double2x2 zero;
	}
}
