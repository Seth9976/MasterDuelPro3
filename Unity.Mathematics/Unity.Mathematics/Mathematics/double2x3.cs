using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200001F RID: 31
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct double2x3 : IEquatable<double2x3>, IFormattable
	{
		// Token: 0x06000BF5 RID: 3061 RVA: 0x00029FA1 File Offset: 0x000281A1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x3(double2 c0, double2 c1, double2 c2)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x00029FB8 File Offset: 0x000281B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x3(double m00, double m01, double m02, double m10, double m11, double m12)
		{
			this.c0 = new double2(m00, m10);
			this.c1 = new double2(m01, m11);
			this.c2 = new double2(m02, m12);
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x00029FE4 File Offset: 0x000281E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x3(double v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x0002A00C File Offset: 0x0002820C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x3(bool v)
		{
			this.c0 = math.select(new double2(0.0), new double2(1.0), v);
			this.c1 = math.select(new double2(0.0), new double2(1.0), v);
			this.c2 = math.select(new double2(0.0), new double2(1.0), v);
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x0002A094 File Offset: 0x00028294
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x3(bool2x3 v)
		{
			this.c0 = math.select(new double2(0.0), new double2(1.0), v.c0);
			this.c1 = math.select(new double2(0.0), new double2(1.0), v.c1);
			this.c2 = math.select(new double2(0.0), new double2(1.0), v.c2);
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x0002A128 File Offset: 0x00028328
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x3(int v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x0002A14E File Offset: 0x0002834E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x3(int2x3 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x0002A183 File Offset: 0x00028383
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x3(uint v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x0002A1A9 File Offset: 0x000283A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x3(uint2x3 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x0002A1DE File Offset: 0x000283DE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x3(float v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x0002A204 File Offset: 0x00028404
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2x3(float2x3 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x00009CE7 File Offset: 0x00007EE7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2x3(double v)
		{
			return new double2x3(v);
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x00009CEF File Offset: 0x00007EEF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double2x3(bool v)
		{
			return new double2x3(v);
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x00009CF7 File Offset: 0x00007EF7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double2x3(bool2x3 v)
		{
			return new double2x3(v);
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x00009CFF File Offset: 0x00007EFF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2x3(int v)
		{
			return new double2x3(v);
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x00009D07 File Offset: 0x00007F07
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2x3(int2x3 v)
		{
			return new double2x3(v);
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x00009D0F File Offset: 0x00007F0F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2x3(uint v)
		{
			return new double2x3(v);
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x00009D17 File Offset: 0x00007F17
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2x3(uint2x3 v)
		{
			return new double2x3(v);
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x00009D1F File Offset: 0x00007F1F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2x3(float v)
		{
			return new double2x3(v);
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x00009D27 File Offset: 0x00007F27
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2x3(float2x3 v)
		{
			return new double2x3(v);
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x0002A239 File Offset: 0x00028439
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 operator *(double2x3 lhs, double2x3 rhs)
		{
			return new double2x3(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2);
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x0002A273 File Offset: 0x00028473
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 operator *(double2x3 lhs, double rhs)
		{
			return new double2x3(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs);
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x0002A29E File Offset: 0x0002849E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 operator *(double lhs, double2x3 rhs)
		{
			return new double2x3(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2);
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x0002A2C9 File Offset: 0x000284C9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 operator +(double2x3 lhs, double2x3 rhs)
		{
			return new double2x3(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2);
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x0002A303 File Offset: 0x00028503
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 operator +(double2x3 lhs, double rhs)
		{
			return new double2x3(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs);
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0002A32E File Offset: 0x0002852E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 operator +(double lhs, double2x3 rhs)
		{
			return new double2x3(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2);
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0002A359 File Offset: 0x00028559
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 operator -(double2x3 lhs, double2x3 rhs)
		{
			return new double2x3(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2);
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0002A393 File Offset: 0x00028593
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 operator -(double2x3 lhs, double rhs)
		{
			return new double2x3(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs);
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x0002A3BE File Offset: 0x000285BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 operator -(double lhs, double2x3 rhs)
		{
			return new double2x3(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2);
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0002A3E9 File Offset: 0x000285E9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 operator /(double2x3 lhs, double2x3 rhs)
		{
			return new double2x3(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2);
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x0002A423 File Offset: 0x00028623
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 operator /(double2x3 lhs, double rhs)
		{
			return new double2x3(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs);
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x0002A44E File Offset: 0x0002864E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 operator /(double lhs, double2x3 rhs)
		{
			return new double2x3(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2);
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x0002A479 File Offset: 0x00028679
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 operator %(double2x3 lhs, double2x3 rhs)
		{
			return new double2x3(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2);
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x0002A4B3 File Offset: 0x000286B3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 operator %(double2x3 lhs, double rhs)
		{
			return new double2x3(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs);
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x0002A4DE File Offset: 0x000286DE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 operator %(double lhs, double2x3 rhs)
		{
			return new double2x3(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2);
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0002A50C File Offset: 0x0002870C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 operator ++(double2x3 val)
		{
			double2 @double = double2.op_Increment(val.c0);
			val.c0 = @double;
			double2 double2 = @double;
			@double = double2.op_Increment(val.c1);
			val.c1 = @double;
			double2 double3 = @double;
			@double = double2.op_Increment(val.c2);
			val.c2 = @double;
			return new double2x3(double2, double3, @double);
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0002A56C File Offset: 0x0002876C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 operator --(double2x3 val)
		{
			double2 @double = double2.op_Decrement(val.c0);
			val.c0 = @double;
			double2 double2 = @double;
			@double = double2.op_Decrement(val.c1);
			val.c1 = @double;
			double2 double3 = @double;
			@double = double2.op_Decrement(val.c2);
			val.c2 = @double;
			return new double2x3(double2, double3, @double);
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x0002A5CC File Offset: 0x000287CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <(double2x3 lhs, double2x3 rhs)
		{
			return new bool2x3(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2);
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x0002A606 File Offset: 0x00028806
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <(double2x3 lhs, double rhs)
		{
			return new bool2x3(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs);
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x0002A631 File Offset: 0x00028831
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <(double lhs, double2x3 rhs)
		{
			return new bool2x3(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2);
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x0002A65C File Offset: 0x0002885C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <=(double2x3 lhs, double2x3 rhs)
		{
			return new bool2x3(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2);
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x0002A696 File Offset: 0x00028896
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <=(double2x3 lhs, double rhs)
		{
			return new bool2x3(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs);
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x0002A6C1 File Offset: 0x000288C1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <=(double lhs, double2x3 rhs)
		{
			return new bool2x3(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2);
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x0002A6EC File Offset: 0x000288EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >(double2x3 lhs, double2x3 rhs)
		{
			return new bool2x3(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2);
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x0002A726 File Offset: 0x00028926
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >(double2x3 lhs, double rhs)
		{
			return new bool2x3(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs);
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x0002A751 File Offset: 0x00028951
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >(double lhs, double2x3 rhs)
		{
			return new bool2x3(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2);
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0002A77C File Offset: 0x0002897C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >=(double2x3 lhs, double2x3 rhs)
		{
			return new bool2x3(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2);
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x0002A7B6 File Offset: 0x000289B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >=(double2x3 lhs, double rhs)
		{
			return new bool2x3(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs);
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x0002A7E1 File Offset: 0x000289E1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >=(double lhs, double2x3 rhs)
		{
			return new bool2x3(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2);
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x0002A80C File Offset: 0x00028A0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 operator -(double2x3 val)
		{
			return new double2x3(-val.c0, -val.c1, -val.c2);
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x0002A834 File Offset: 0x00028A34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 operator +(double2x3 val)
		{
			return new double2x3(+val.c0, +val.c1, +val.c2);
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x0002A85C File Offset: 0x00028A5C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator ==(double2x3 lhs, double2x3 rhs)
		{
			return new bool2x3(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2);
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x0002A896 File Offset: 0x00028A96
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator ==(double2x3 lhs, double rhs)
		{
			return new bool2x3(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs);
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x0002A8C1 File Offset: 0x00028AC1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator ==(double lhs, double2x3 rhs)
		{
			return new bool2x3(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2);
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0002A8EC File Offset: 0x00028AEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator !=(double2x3 lhs, double2x3 rhs)
		{
			return new bool2x3(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2);
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x0002A926 File Offset: 0x00028B26
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator !=(double2x3 lhs, double rhs)
		{
			return new bool2x3(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs);
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x0002A951 File Offset: 0x00028B51
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator !=(double lhs, double2x3 rhs)
		{
			return new bool2x3(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2);
		}

		// Token: 0x1700020C RID: 524
		public unsafe ref double2 this[int index]
		{
			get
			{
				fixed (double2x3* ptr = &this)
				{
					return ref *(double2*)(ptr + (IntPtr)index * (IntPtr)sizeof(double2) / (IntPtr)sizeof(double2x3));
				}
			}
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x0002A997 File Offset: 0x00028B97
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(double2x3 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2);
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x0002A9D4 File Offset: 0x00028BD4
		public override bool Equals(object o)
		{
			if (o is double2x3)
			{
				double2x3 converted = (double2x3)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x0002A9F9 File Offset: 0x00028BF9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x0002AA08 File Offset: 0x00028C08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("double2x3({0}, {1}, {2},  {3}, {4}, {5})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c2.x,
				this.c0.y,
				this.c1.y,
				this.c2.y
			});
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x0002AA98 File Offset: 0x00028C98
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("double2x3({0}, {1}, {2},  {3}, {4}, {5})", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c2.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c2.y.ToString(format, formatProvider)
			});
		}

		// Token: 0x04000073 RID: 115
		public double2 c0;

		// Token: 0x04000074 RID: 116
		public double2 c1;

		// Token: 0x04000075 RID: 117
		public double2 c2;

		// Token: 0x04000076 RID: 118
		public static readonly double2x3 zero;
	}
}
