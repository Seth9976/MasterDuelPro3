using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000029 RID: 41
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct double4x3 : IEquatable<double4x3>, IFormattable
	{
		// Token: 0x0600100F RID: 4111 RVA: 0x00033DCD File Offset: 0x00031FCD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x3(double4 c0, double4 c1, double4 c2)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x00033DE4 File Offset: 0x00031FE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x3(double m00, double m01, double m02, double m10, double m11, double m12, double m20, double m21, double m22, double m30, double m31, double m32)
		{
			this.c0 = new double4(m00, m10, m20, m30);
			this.c1 = new double4(m01, m11, m21, m31);
			this.c2 = new double4(m02, m12, m22, m32);
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x00033E1C File Offset: 0x0003201C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x3(double v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x00033E44 File Offset: 0x00032044
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x3(bool v)
		{
			this.c0 = math.select(new double4(0.0), new double4(1.0), v);
			this.c1 = math.select(new double4(0.0), new double4(1.0), v);
			this.c2 = math.select(new double4(0.0), new double4(1.0), v);
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x00033ECC File Offset: 0x000320CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x3(bool4x3 v)
		{
			this.c0 = math.select(new double4(0.0), new double4(1.0), v.c0);
			this.c1 = math.select(new double4(0.0), new double4(1.0), v.c1);
			this.c2 = math.select(new double4(0.0), new double4(1.0), v.c2);
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x00033F60 File Offset: 0x00032160
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x3(int v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x00033F86 File Offset: 0x00032186
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x3(int4x3 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x00033FBB File Offset: 0x000321BB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x3(uint v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x00033FE1 File Offset: 0x000321E1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x3(uint4x3 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x00034016 File Offset: 0x00032216
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x3(float v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x0003403C File Offset: 0x0003223C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4x3(float4x3 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x0000AE7E File Offset: 0x0000907E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x3(double v)
		{
			return new double4x3(v);
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x0000AE86 File Offset: 0x00009086
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double4x3(bool v)
		{
			return new double4x3(v);
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x0000AE8E File Offset: 0x0000908E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double4x3(bool4x3 v)
		{
			return new double4x3(v);
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x0000AE96 File Offset: 0x00009096
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x3(int v)
		{
			return new double4x3(v);
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x0000AE9E File Offset: 0x0000909E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x3(int4x3 v)
		{
			return new double4x3(v);
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x0000AEA6 File Offset: 0x000090A6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x3(uint v)
		{
			return new double4x3(v);
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x0000AEAE File Offset: 0x000090AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x3(uint4x3 v)
		{
			return new double4x3(v);
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x0000AEB6 File Offset: 0x000090B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x3(float v)
		{
			return new double4x3(v);
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x0000AEBE File Offset: 0x000090BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4x3(float4x3 v)
		{
			return new double4x3(v);
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x00034071 File Offset: 0x00032271
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 operator *(double4x3 lhs, double4x3 rhs)
		{
			return new double4x3(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2);
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x000340AB File Offset: 0x000322AB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 operator *(double4x3 lhs, double rhs)
		{
			return new double4x3(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs);
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x000340D6 File Offset: 0x000322D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 operator *(double lhs, double4x3 rhs)
		{
			return new double4x3(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2);
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x00034101 File Offset: 0x00032301
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 operator +(double4x3 lhs, double4x3 rhs)
		{
			return new double4x3(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2);
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x0003413B File Offset: 0x0003233B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 operator +(double4x3 lhs, double rhs)
		{
			return new double4x3(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs);
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x00034166 File Offset: 0x00032366
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 operator +(double lhs, double4x3 rhs)
		{
			return new double4x3(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2);
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x00034191 File Offset: 0x00032391
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 operator -(double4x3 lhs, double4x3 rhs)
		{
			return new double4x3(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2);
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x000341CB File Offset: 0x000323CB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 operator -(double4x3 lhs, double rhs)
		{
			return new double4x3(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs);
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x000341F6 File Offset: 0x000323F6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 operator -(double lhs, double4x3 rhs)
		{
			return new double4x3(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2);
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x00034221 File Offset: 0x00032421
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 operator /(double4x3 lhs, double4x3 rhs)
		{
			return new double4x3(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2);
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x0003425B File Offset: 0x0003245B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 operator /(double4x3 lhs, double rhs)
		{
			return new double4x3(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs);
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x00034286 File Offset: 0x00032486
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 operator /(double lhs, double4x3 rhs)
		{
			return new double4x3(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2);
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x000342B1 File Offset: 0x000324B1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 operator %(double4x3 lhs, double4x3 rhs)
		{
			return new double4x3(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2);
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x000342EB File Offset: 0x000324EB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 operator %(double4x3 lhs, double rhs)
		{
			return new double4x3(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs);
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x00034316 File Offset: 0x00032516
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 operator %(double lhs, double4x3 rhs)
		{
			return new double4x3(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2);
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x00034344 File Offset: 0x00032544
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 operator ++(double4x3 val)
		{
			double4 @double = double4.op_Increment(val.c0);
			val.c0 = @double;
			double4 double2 = @double;
			@double = double4.op_Increment(val.c1);
			val.c1 = @double;
			double4 double3 = @double;
			@double = double4.op_Increment(val.c2);
			val.c2 = @double;
			return new double4x3(double2, double3, @double);
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x000343A4 File Offset: 0x000325A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 operator --(double4x3 val)
		{
			double4 @double = double4.op_Decrement(val.c0);
			val.c0 = @double;
			double4 double2 = @double;
			@double = double4.op_Decrement(val.c1);
			val.c1 = @double;
			double4 double3 = @double;
			@double = double4.op_Decrement(val.c2);
			val.c2 = @double;
			return new double4x3(double2, double3, @double);
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x00034404 File Offset: 0x00032604
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <(double4x3 lhs, double4x3 rhs)
		{
			return new bool4x3(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2);
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x0003443E File Offset: 0x0003263E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <(double4x3 lhs, double rhs)
		{
			return new bool4x3(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs);
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x00034469 File Offset: 0x00032669
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <(double lhs, double4x3 rhs)
		{
			return new bool4x3(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2);
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x00034494 File Offset: 0x00032694
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <=(double4x3 lhs, double4x3 rhs)
		{
			return new bool4x3(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2);
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x000344CE File Offset: 0x000326CE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <=(double4x3 lhs, double rhs)
		{
			return new bool4x3(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs);
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x000344F9 File Offset: 0x000326F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <=(double lhs, double4x3 rhs)
		{
			return new bool4x3(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2);
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x00034524 File Offset: 0x00032724
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >(double4x3 lhs, double4x3 rhs)
		{
			return new bool4x3(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2);
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x0003455E File Offset: 0x0003275E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >(double4x3 lhs, double rhs)
		{
			return new bool4x3(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs);
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x00034589 File Offset: 0x00032789
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >(double lhs, double4x3 rhs)
		{
			return new bool4x3(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2);
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x000345B4 File Offset: 0x000327B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >=(double4x3 lhs, double4x3 rhs)
		{
			return new bool4x3(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2);
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x000345EE File Offset: 0x000327EE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >=(double4x3 lhs, double rhs)
		{
			return new bool4x3(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs);
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x00034619 File Offset: 0x00032819
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >=(double lhs, double4x3 rhs)
		{
			return new bool4x3(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2);
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x00034644 File Offset: 0x00032844
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 operator -(double4x3 val)
		{
			return new double4x3(-val.c0, -val.c1, -val.c2);
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x0003466C File Offset: 0x0003286C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 operator +(double4x3 val)
		{
			return new double4x3(+val.c0, +val.c1, +val.c2);
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x00034694 File Offset: 0x00032894
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator ==(double4x3 lhs, double4x3 rhs)
		{
			return new bool4x3(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2);
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x000346CE File Offset: 0x000328CE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator ==(double4x3 lhs, double rhs)
		{
			return new bool4x3(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs);
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x000346F9 File Offset: 0x000328F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator ==(double lhs, double4x3 rhs)
		{
			return new bool4x3(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2);
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x00034724 File Offset: 0x00032924
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator !=(double4x3 lhs, double4x3 rhs)
		{
			return new bool4x3(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2);
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x0003475E File Offset: 0x0003295E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator !=(double4x3 lhs, double rhs)
		{
			return new bool4x3(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs);
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x00034789 File Offset: 0x00032989
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator !=(double lhs, double4x3 rhs)
		{
			return new bool4x3(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2);
		}

		// Token: 0x170003D9 RID: 985
		public unsafe ref double4 this[int index]
		{
			get
			{
				fixed (double4x3* ptr = &this)
				{
					return ref *(double4*)(ptr + (IntPtr)index * (IntPtr)sizeof(double4) / (IntPtr)sizeof(double4x3));
				}
			}
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x000347CF File Offset: 0x000329CF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(double4x3 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2);
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x0003480C File Offset: 0x00032A0C
		public override bool Equals(object o)
		{
			if (o is double4x3)
			{
				double4x3 converted = (double4x3)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x00034831 File Offset: 0x00032A31
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x00034840 File Offset: 0x00032A40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("double4x3({0}, {1}, {2},  {3}, {4}, {5},  {6}, {7}, {8},  {9}, {10}, {11})", new object[]
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

		// Token: 0x0600104D RID: 4173 RVA: 0x00034948 File Offset: 0x00032B48
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("double4x3({0}, {1}, {2},  {3}, {4}, {5},  {6}, {7}, {8},  {9}, {10}, {11})", new object[]
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

		// Token: 0x0400009C RID: 156
		public double4 c0;

		// Token: 0x0400009D RID: 157
		public double4 c1;

		// Token: 0x0400009E RID: 158
		public double4 c2;

		// Token: 0x0400009F RID: 159
		public static readonly double4x3 zero;
	}
}
