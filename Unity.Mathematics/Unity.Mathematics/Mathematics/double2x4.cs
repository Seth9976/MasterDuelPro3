using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000020 RID: 32
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct double2x4 : IEquatable<double2x4>, IFormattable
	{
		// Token: 0x06000C34 RID: 3124 RVA: 0x0002AB33 File Offset: 0x00028D33
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x4(double2 c0, double2 c1, double2 c2, double2 c3)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
			this.c3 = c3;
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x0002AB52 File Offset: 0x00028D52
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x4(double m00, double m01, double m02, double m03, double m10, double m11, double m12, double m13)
		{
			this.c0 = new double2(m00, m10);
			this.c1 = new double2(m01, m11);
			this.c2 = new double2(m02, m12);
			this.c3 = new double2(m03, m13);
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x0002AB8D File Offset: 0x00028D8D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x4(double v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0002ABC0 File Offset: 0x00028DC0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x4(bool v)
		{
			this.c0 = math.select(new double2(0.0), new double2(1.0), v);
			this.c1 = math.select(new double2(0.0), new double2(1.0), v);
			this.c2 = math.select(new double2(0.0), new double2(1.0), v);
			this.c3 = math.select(new double2(0.0), new double2(1.0), v);
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0002AC70 File Offset: 0x00028E70
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x4(bool2x4 v)
		{
			this.c0 = math.select(new double2(0.0), new double2(1.0), v.c0);
			this.c1 = math.select(new double2(0.0), new double2(1.0), v.c1);
			this.c2 = math.select(new double2(0.0), new double2(1.0), v.c2);
			this.c3 = math.select(new double2(0.0), new double2(1.0), v.c3);
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x0002AD31 File Offset: 0x00028F31
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x4(int v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x0002AD64 File Offset: 0x00028F64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x4(int2x4 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
			this.c3 = v.c3;
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x0002ADB5 File Offset: 0x00028FB5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x4(uint v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x0002ADE8 File Offset: 0x00028FE8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x4(uint2x4 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
			this.c3 = v.c3;
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x0002AE39 File Offset: 0x00029039
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x4(float v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x0002AE6C File Offset: 0x0002906C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x4(float2x4 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
			this.c3 = v.c3;
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x00009EA0 File Offset: 0x000080A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2x4(double v)
		{
			return new double2x4(v);
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x00009EA8 File Offset: 0x000080A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double2x4(bool v)
		{
			return new double2x4(v);
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x00009EB0 File Offset: 0x000080B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double2x4(bool2x4 v)
		{
			return new double2x4(v);
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x00009EB8 File Offset: 0x000080B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2x4(int v)
		{
			return new double2x4(v);
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x00009EC0 File Offset: 0x000080C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2x4(int2x4 v)
		{
			return new double2x4(v);
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x00009EC8 File Offset: 0x000080C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2x4(uint v)
		{
			return new double2x4(v);
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x00009ED0 File Offset: 0x000080D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2x4(uint2x4 v)
		{
			return new double2x4(v);
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x00009ED8 File Offset: 0x000080D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2x4(float v)
		{
			return new double2x4(v);
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x00009EE0 File Offset: 0x000080E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2x4(float2x4 v)
		{
			return new double2x4(v);
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x0002AEC0 File Offset: 0x000290C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 operator *(double2x4 lhs, double2x4 rhs)
		{
			return new double2x4(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2, lhs.c3 * rhs.c3);
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x0002AF16 File Offset: 0x00029116
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 operator *(double2x4 lhs, double rhs)
		{
			return new double2x4(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs, lhs.c3 * rhs);
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x0002AF4D File Offset: 0x0002914D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 operator *(double lhs, double2x4 rhs)
		{
			return new double2x4(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2, lhs * rhs.c3);
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x0002AF84 File Offset: 0x00029184
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 operator +(double2x4 lhs, double2x4 rhs)
		{
			return new double2x4(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2, lhs.c3 + rhs.c3);
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x0002AFDA File Offset: 0x000291DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 operator +(double2x4 lhs, double rhs)
		{
			return new double2x4(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs, lhs.c3 + rhs);
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0002B011 File Offset: 0x00029211
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 operator +(double lhs, double2x4 rhs)
		{
			return new double2x4(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2, lhs + rhs.c3);
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x0002B048 File Offset: 0x00029248
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 operator -(double2x4 lhs, double2x4 rhs)
		{
			return new double2x4(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2, lhs.c3 - rhs.c3);
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x0002B09E File Offset: 0x0002929E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 operator -(double2x4 lhs, double rhs)
		{
			return new double2x4(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs, lhs.c3 - rhs);
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x0002B0D5 File Offset: 0x000292D5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 operator -(double lhs, double2x4 rhs)
		{
			return new double2x4(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2, lhs - rhs.c3);
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x0002B10C File Offset: 0x0002930C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 operator /(double2x4 lhs, double2x4 rhs)
		{
			return new double2x4(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2, lhs.c3 / rhs.c3);
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x0002B162 File Offset: 0x00029362
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 operator /(double2x4 lhs, double rhs)
		{
			return new double2x4(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs, lhs.c3 / rhs);
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x0002B199 File Offset: 0x00029399
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 operator /(double lhs, double2x4 rhs)
		{
			return new double2x4(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2, lhs / rhs.c3);
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x0002B1D0 File Offset: 0x000293D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 operator %(double2x4 lhs, double2x4 rhs)
		{
			return new double2x4(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2, lhs.c3 % rhs.c3);
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x0002B226 File Offset: 0x00029426
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 operator %(double2x4 lhs, double rhs)
		{
			return new double2x4(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs, lhs.c3 % rhs);
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x0002B25D File Offset: 0x0002945D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 operator %(double lhs, double2x4 rhs)
		{
			return new double2x4(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2, lhs % rhs.c3);
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x0002B294 File Offset: 0x00029494
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 operator ++(double2x4 val)
		{
			double2 @double = double2.op_Increment(val.c0);
			val.c0 = @double;
			double2 double2 = @double;
			@double = double2.op_Increment(val.c1);
			val.c1 = @double;
			double2 double3 = @double;
			@double = double2.op_Increment(val.c2);
			val.c2 = @double;
			double2 double4 = @double;
			@double = double2.op_Increment(val.c3);
			val.c3 = @double;
			return new double2x4(double2, double3, double4, @double);
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x0002B310 File Offset: 0x00029510
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 operator --(double2x4 val)
		{
			double2 @double = double2.op_Decrement(val.c0);
			val.c0 = @double;
			double2 double2 = @double;
			@double = double2.op_Decrement(val.c1);
			val.c1 = @double;
			double2 double3 = @double;
			@double = double2.op_Decrement(val.c2);
			val.c2 = @double;
			double2 double4 = @double;
			@double = double2.op_Decrement(val.c3);
			val.c3 = @double;
			return new double2x4(double2, double3, double4, @double);
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x0002B38C File Offset: 0x0002958C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator <(double2x4 lhs, double2x4 rhs)
		{
			return new bool2x4(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2, lhs.c3 < rhs.c3);
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x0002B3E2 File Offset: 0x000295E2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator <(double2x4 lhs, double rhs)
		{
			return new bool2x4(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs, lhs.c3 < rhs);
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x0002B419 File Offset: 0x00029619
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator <(double lhs, double2x4 rhs)
		{
			return new bool2x4(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2, lhs < rhs.c3);
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x0002B450 File Offset: 0x00029650
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator <=(double2x4 lhs, double2x4 rhs)
		{
			return new bool2x4(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2, lhs.c3 <= rhs.c3);
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x0002B4A6 File Offset: 0x000296A6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator <=(double2x4 lhs, double rhs)
		{
			return new bool2x4(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs, lhs.c3 <= rhs);
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x0002B4DD File Offset: 0x000296DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator <=(double lhs, double2x4 rhs)
		{
			return new bool2x4(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2, lhs <= rhs.c3);
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x0002B514 File Offset: 0x00029714
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator >(double2x4 lhs, double2x4 rhs)
		{
			return new bool2x4(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2, lhs.c3 > rhs.c3);
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x0002B56A File Offset: 0x0002976A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator >(double2x4 lhs, double rhs)
		{
			return new bool2x4(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs, lhs.c3 > rhs);
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x0002B5A1 File Offset: 0x000297A1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator >(double lhs, double2x4 rhs)
		{
			return new bool2x4(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2, lhs > rhs.c3);
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x0002B5D8 File Offset: 0x000297D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator >=(double2x4 lhs, double2x4 rhs)
		{
			return new bool2x4(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2, lhs.c3 >= rhs.c3);
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x0002B62E File Offset: 0x0002982E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator >=(double2x4 lhs, double rhs)
		{
			return new bool2x4(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs, lhs.c3 >= rhs);
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x0002B665 File Offset: 0x00029865
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator >=(double lhs, double2x4 rhs)
		{
			return new bool2x4(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2, lhs >= rhs.c3);
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0002B69C File Offset: 0x0002989C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 operator -(double2x4 val)
		{
			return new double2x4(-val.c0, -val.c1, -val.c2, -val.c3);
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x0002B6CF File Offset: 0x000298CF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 operator +(double2x4 val)
		{
			return new double2x4(+val.c0, +val.c1, +val.c2, +val.c3);
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x0002B704 File Offset: 0x00029904
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator ==(double2x4 lhs, double2x4 rhs)
		{
			return new bool2x4(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2, lhs.c3 == rhs.c3);
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x0002B75A File Offset: 0x0002995A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator ==(double2x4 lhs, double rhs)
		{
			return new bool2x4(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs, lhs.c3 == rhs);
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x0002B791 File Offset: 0x00029991
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator ==(double lhs, double2x4 rhs)
		{
			return new bool2x4(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2, lhs == rhs.c3);
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x0002B7C8 File Offset: 0x000299C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator !=(double2x4 lhs, double2x4 rhs)
		{
			return new bool2x4(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2, lhs.c3 != rhs.c3);
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0002B81E File Offset: 0x00029A1E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator !=(double2x4 lhs, double rhs)
		{
			return new bool2x4(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs, lhs.c3 != rhs);
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x0002B855 File Offset: 0x00029A55
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator !=(double lhs, double2x4 rhs)
		{
			return new bool2x4(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2, lhs != rhs.c3);
		}

		// Token: 0x1700020D RID: 525
		public unsafe ref double2 this[int index]
		{
			get
			{
				fixed (double2x4* ptr = &this)
				{
					return ref *(double2*)(ptr + (IntPtr)index * (IntPtr)sizeof(double2) / (IntPtr)sizeof(double2x4));
				}
			}
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0002B8A8 File Offset: 0x00029AA8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(double2x4 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2) && this.c3.Equals(rhs.c3);
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x0002B904 File Offset: 0x00029B04
		public override bool Equals(object o)
		{
			if (o is double2x4)
			{
				double2x4 converted = (double2x4)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x0002B929 File Offset: 0x00029B29
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x0002B938 File Offset: 0x00029B38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("double2x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7})", new object[]
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

		// Token: 0x06000C72 RID: 3186 RVA: 0x0002B9F0 File Offset: 0x00029BF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("double2x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7})", new object[]
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

		// Token: 0x04000077 RID: 119
		public double2 c0;

		// Token: 0x04000078 RID: 120
		public double2 c1;

		// Token: 0x04000079 RID: 121
		public double2 c2;

		// Token: 0x0400007A RID: 122
		public double2 c3;

		// Token: 0x0400007B RID: 123
		public static readonly double2x4 zero;
	}
}
