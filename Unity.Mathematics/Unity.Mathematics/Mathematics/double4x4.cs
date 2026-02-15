using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200002A RID: 42
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct double4x4 : IEquatable<double4x4>, IFormattable
	{
		// Token: 0x0600104E RID: 4174 RVA: 0x00034A65 File Offset: 0x00032C65
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x4(double4 c0, double4 c1, double4 c2, double4 c3)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
			this.c3 = c3;
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x00034A84 File Offset: 0x00032C84
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x4(double m00, double m01, double m02, double m03, double m10, double m11, double m12, double m13, double m20, double m21, double m22, double m23, double m30, double m31, double m32, double m33)
		{
			this.c0 = new double4(m00, m10, m20, m30);
			this.c1 = new double4(m01, m11, m21, m31);
			this.c2 = new double4(m02, m12, m22, m32);
			this.c3 = new double4(m03, m13, m23, m33);
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x00034ADA File Offset: 0x00032CDA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x4(double v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x00034B0C File Offset: 0x00032D0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x4(bool v)
		{
			this.c0 = math.select(new double4(0.0), new double4(1.0), v);
			this.c1 = math.select(new double4(0.0), new double4(1.0), v);
			this.c2 = math.select(new double4(0.0), new double4(1.0), v);
			this.c3 = math.select(new double4(0.0), new double4(1.0), v);
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x00034BBC File Offset: 0x00032DBC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x4(bool4x4 v)
		{
			this.c0 = math.select(new double4(0.0), new double4(1.0), v.c0);
			this.c1 = math.select(new double4(0.0), new double4(1.0), v.c1);
			this.c2 = math.select(new double4(0.0), new double4(1.0), v.c2);
			this.c3 = math.select(new double4(0.0), new double4(1.0), v.c3);
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x00034C7D File Offset: 0x00032E7D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x4(int v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x00034CB0 File Offset: 0x00032EB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x4(int4x4 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
			this.c3 = v.c3;
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x00034D01 File Offset: 0x00032F01
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x4(uint v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x00034D34 File Offset: 0x00032F34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x4(uint4x4 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
			this.c3 = v.c3;
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x00034D85 File Offset: 0x00032F85
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x4(float v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x00034DB8 File Offset: 0x00032FB8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x4(float4x4 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
			this.c3 = v.c3;
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x0000B0D6 File Offset: 0x000092D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x4(double v)
		{
			return new double4x4(v);
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x0000B0DE File Offset: 0x000092DE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double4x4(bool v)
		{
			return new double4x4(v);
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0000B0E6 File Offset: 0x000092E6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double4x4(bool4x4 v)
		{
			return new double4x4(v);
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x0000B0EE File Offset: 0x000092EE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x4(int v)
		{
			return new double4x4(v);
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x0000B0F6 File Offset: 0x000092F6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x4(int4x4 v)
		{
			return new double4x4(v);
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x0000B0FE File Offset: 0x000092FE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x4(uint v)
		{
			return new double4x4(v);
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x0000B106 File Offset: 0x00009306
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x4(uint4x4 v)
		{
			return new double4x4(v);
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x0000B10E File Offset: 0x0000930E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x4(float v)
		{
			return new double4x4(v);
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0000B116 File Offset: 0x00009316
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x4(float4x4 v)
		{
			return new double4x4(v);
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x00034E0C File Offset: 0x0003300C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 operator *(double4x4 lhs, double4x4 rhs)
		{
			return new double4x4(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2, lhs.c3 * rhs.c3);
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x00034E62 File Offset: 0x00033062
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 operator *(double4x4 lhs, double rhs)
		{
			return new double4x4(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs, lhs.c3 * rhs);
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x00034E99 File Offset: 0x00033099
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 operator *(double lhs, double4x4 rhs)
		{
			return new double4x4(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2, lhs * rhs.c3);
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x00034ED0 File Offset: 0x000330D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 operator +(double4x4 lhs, double4x4 rhs)
		{
			return new double4x4(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2, lhs.c3 + rhs.c3);
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x00034F26 File Offset: 0x00033126
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 operator +(double4x4 lhs, double rhs)
		{
			return new double4x4(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs, lhs.c3 + rhs);
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x00034F5D File Offset: 0x0003315D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 operator +(double lhs, double4x4 rhs)
		{
			return new double4x4(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2, lhs + rhs.c3);
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x00034F94 File Offset: 0x00033194
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 operator -(double4x4 lhs, double4x4 rhs)
		{
			return new double4x4(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2, lhs.c3 - rhs.c3);
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x00034FEA File Offset: 0x000331EA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 operator -(double4x4 lhs, double rhs)
		{
			return new double4x4(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs, lhs.c3 - rhs);
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x00035021 File Offset: 0x00033221
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 operator -(double lhs, double4x4 rhs)
		{
			return new double4x4(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2, lhs - rhs.c3);
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x00035058 File Offset: 0x00033258
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 operator /(double4x4 lhs, double4x4 rhs)
		{
			return new double4x4(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2, lhs.c3 / rhs.c3);
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x000350AE File Offset: 0x000332AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 operator /(double4x4 lhs, double rhs)
		{
			return new double4x4(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs, lhs.c3 / rhs);
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x000350E5 File Offset: 0x000332E5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 operator /(double lhs, double4x4 rhs)
		{
			return new double4x4(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2, lhs / rhs.c3);
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x0003511C File Offset: 0x0003331C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 operator %(double4x4 lhs, double4x4 rhs)
		{
			return new double4x4(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2, lhs.c3 % rhs.c3);
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x00035172 File Offset: 0x00033372
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 operator %(double4x4 lhs, double rhs)
		{
			return new double4x4(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs, lhs.c3 % rhs);
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x000351A9 File Offset: 0x000333A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 operator %(double lhs, double4x4 rhs)
		{
			return new double4x4(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2, lhs % rhs.c3);
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x000351E0 File Offset: 0x000333E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 operator ++(double4x4 val)
		{
			double4 @double = double4.op_Increment(val.c0);
			val.c0 = @double;
			double4 double2 = @double;
			@double = double4.op_Increment(val.c1);
			val.c1 = @double;
			double4 double3 = @double;
			@double = double4.op_Increment(val.c2);
			val.c2 = @double;
			double4 double4 = @double;
			@double = double4.op_Increment(val.c3);
			val.c3 = @double;
			return new double4x4(double2, double3, double4, @double);
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x0003525C File Offset: 0x0003345C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 operator --(double4x4 val)
		{
			double4 @double = double4.op_Decrement(val.c0);
			val.c0 = @double;
			double4 double2 = @double;
			@double = double4.op_Decrement(val.c1);
			val.c1 = @double;
			double4 double3 = @double;
			@double = double4.op_Decrement(val.c2);
			val.c2 = @double;
			double4 double4 = @double;
			@double = double4.op_Decrement(val.c3);
			val.c3 = @double;
			return new double4x4(double2, double3, double4, @double);
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x000352D8 File Offset: 0x000334D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <(double4x4 lhs, double4x4 rhs)
		{
			return new bool4x4(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2, lhs.c3 < rhs.c3);
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x0003532E File Offset: 0x0003352E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <(double4x4 lhs, double rhs)
		{
			return new bool4x4(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs, lhs.c3 < rhs);
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x00035365 File Offset: 0x00033565
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <(double lhs, double4x4 rhs)
		{
			return new bool4x4(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2, lhs < rhs.c3);
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x0003539C File Offset: 0x0003359C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <=(double4x4 lhs, double4x4 rhs)
		{
			return new bool4x4(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2, lhs.c3 <= rhs.c3);
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x000353F2 File Offset: 0x000335F2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <=(double4x4 lhs, double rhs)
		{
			return new bool4x4(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs, lhs.c3 <= rhs);
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x00035429 File Offset: 0x00033629
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <=(double lhs, double4x4 rhs)
		{
			return new bool4x4(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2, lhs <= rhs.c3);
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x00035460 File Offset: 0x00033660
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >(double4x4 lhs, double4x4 rhs)
		{
			return new bool4x4(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2, lhs.c3 > rhs.c3);
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x000354B6 File Offset: 0x000336B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >(double4x4 lhs, double rhs)
		{
			return new bool4x4(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs, lhs.c3 > rhs);
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x000354ED File Offset: 0x000336ED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >(double lhs, double4x4 rhs)
		{
			return new bool4x4(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2, lhs > rhs.c3);
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x00035524 File Offset: 0x00033724
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >=(double4x4 lhs, double4x4 rhs)
		{
			return new bool4x4(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2, lhs.c3 >= rhs.c3);
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x0003557A File Offset: 0x0003377A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >=(double4x4 lhs, double rhs)
		{
			return new bool4x4(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs, lhs.c3 >= rhs);
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x000355B1 File Offset: 0x000337B1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >=(double lhs, double4x4 rhs)
		{
			return new bool4x4(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2, lhs >= rhs.c3);
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x000355E8 File Offset: 0x000337E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 operator -(double4x4 val)
		{
			return new double4x4(-val.c0, -val.c1, -val.c2, -val.c3);
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x0003561B File Offset: 0x0003381B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 operator +(double4x4 val)
		{
			return new double4x4(+val.c0, +val.c1, +val.c2, +val.c3);
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x00035650 File Offset: 0x00033850
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator ==(double4x4 lhs, double4x4 rhs)
		{
			return new bool4x4(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2, lhs.c3 == rhs.c3);
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x000356A6 File Offset: 0x000338A6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator ==(double4x4 lhs, double rhs)
		{
			return new bool4x4(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs, lhs.c3 == rhs);
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x000356DD File Offset: 0x000338DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator ==(double lhs, double4x4 rhs)
		{
			return new bool4x4(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2, lhs == rhs.c3);
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x00035714 File Offset: 0x00033914
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator !=(double4x4 lhs, double4x4 rhs)
		{
			return new bool4x4(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2, lhs.c3 != rhs.c3);
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x0003576A File Offset: 0x0003396A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator !=(double4x4 lhs, double rhs)
		{
			return new bool4x4(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs, lhs.c3 != rhs);
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x000357A1 File Offset: 0x000339A1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator !=(double lhs, double4x4 rhs)
		{
			return new bool4x4(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2, lhs != rhs.c3);
		}

		// Token: 0x170003DA RID: 986
		public unsafe ref double4 this[int index]
		{
			get
			{
				fixed (double4x4* ptr = &this)
				{
					return ref *(double4*)(ptr + (IntPtr)index * (IntPtr)sizeof(double4) / (IntPtr)sizeof(double4x4));
				}
			}
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x000357F4 File Offset: 0x000339F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(double4x4 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2) && this.c3.Equals(rhs.c3);
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x00035850 File Offset: 0x00033A50
		public override bool Equals(object o)
		{
			if (o is double4x4)
			{
				double4x4 converted = (double4x4)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x00035875 File Offset: 0x00033A75
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x00035884 File Offset: 0x00033A84
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("double4x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7},  {8}, {9}, {10}, {11},  {12}, {13}, {14}, {15})", new object[]
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

		// Token: 0x0600108C RID: 4236 RVA: 0x000359DC File Offset: 0x00033BDC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("double4x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7},  {8}, {9}, {10}, {11},  {12}, {13}, {14}, {15})", new object[]
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

		// Token: 0x040000A0 RID: 160
		public double4 c0;

		// Token: 0x040000A1 RID: 161
		public double4 c1;

		// Token: 0x040000A2 RID: 162
		public double4 c2;

		// Token: 0x040000A3 RID: 163
		public double4 c3;

		// Token: 0x040000A4 RID: 164
		public static readonly double4x4 identity = new double4x4(1.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 1.0);

		// Token: 0x040000A5 RID: 165
		public static readonly double4x4 zero;
	}
}
