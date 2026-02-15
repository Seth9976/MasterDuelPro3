using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000023 RID: 35
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct double3x2 : IEquatable<double3x2>, IFormattable
	{
		// Token: 0x06000D3B RID: 3387 RVA: 0x0002D29E File Offset: 0x0002B49E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x2(double3 c0, double3 c1)
		{
			this.c0 = c0;
			this.c1 = c1;
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x0002D2AE File Offset: 0x0002B4AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x2(double m00, double m01, double m10, double m11, double m20, double m21)
		{
			this.c0 = new double3(m00, m10, m20);
			this.c1 = new double3(m01, m11, m21);
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x0002D2CF File Offset: 0x0002B4CF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x2(double v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x0002D2EC File Offset: 0x0002B4EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x2(bool v)
		{
			this.c0 = math.select(new double3(0.0), new double3(1.0), v);
			this.c1 = math.select(new double3(0.0), new double3(1.0), v);
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x0002D34C File Offset: 0x0002B54C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x2(bool3x2 v)
		{
			this.c0 = math.select(new double3(0.0), new double3(1.0), v.c0);
			this.c1 = math.select(new double3(0.0), new double3(1.0), v.c1);
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x0002D3B3 File Offset: 0x0002B5B3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x2(int v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x0002D3CD File Offset: 0x0002B5CD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x2(int3x2 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x0002D3F1 File Offset: 0x0002B5F1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x2(uint v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x0002D40B File Offset: 0x0002B60B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x2(uint3x2 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x0002D42F File Offset: 0x0002B62F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x2(float v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x0002D449 File Offset: 0x0002B649
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x2(float3x2 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x0000A267 File Offset: 0x00008467
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3x2(double v)
		{
			return new double3x2(v);
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0000A26F File Offset: 0x0000846F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double3x2(bool v)
		{
			return new double3x2(v);
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x0000A277 File Offset: 0x00008477
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double3x2(bool3x2 v)
		{
			return new double3x2(v);
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x0000A27F File Offset: 0x0000847F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3x2(int v)
		{
			return new double3x2(v);
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x0000A287 File Offset: 0x00008487
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3x2(int3x2 v)
		{
			return new double3x2(v);
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x0000A28F File Offset: 0x0000848F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3x2(uint v)
		{
			return new double3x2(v);
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x0000A297 File Offset: 0x00008497
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3x2(uint3x2 v)
		{
			return new double3x2(v);
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x0000A29F File Offset: 0x0000849F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3x2(float v)
		{
			return new double3x2(v);
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x0000A2A7 File Offset: 0x000084A7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3x2(float3x2 v)
		{
			return new double3x2(v);
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x0002D46D File Offset: 0x0002B66D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 operator *(double3x2 lhs, double3x2 rhs)
		{
			return new double3x2(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1);
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x0002D496 File Offset: 0x0002B696
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 operator *(double3x2 lhs, double rhs)
		{
			return new double3x2(lhs.c0 * rhs, lhs.c1 * rhs);
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0002D4B5 File Offset: 0x0002B6B5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 operator *(double lhs, double3x2 rhs)
		{
			return new double3x2(lhs * rhs.c0, lhs * rhs.c1);
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x0002D4D4 File Offset: 0x0002B6D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 operator +(double3x2 lhs, double3x2 rhs)
		{
			return new double3x2(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1);
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x0002D4FD File Offset: 0x0002B6FD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 operator +(double3x2 lhs, double rhs)
		{
			return new double3x2(lhs.c0 + rhs, lhs.c1 + rhs);
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x0002D51C File Offset: 0x0002B71C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 operator +(double lhs, double3x2 rhs)
		{
			return new double3x2(lhs + rhs.c0, lhs + rhs.c1);
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x0002D53B File Offset: 0x0002B73B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 operator -(double3x2 lhs, double3x2 rhs)
		{
			return new double3x2(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1);
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0002D564 File Offset: 0x0002B764
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 operator -(double3x2 lhs, double rhs)
		{
			return new double3x2(lhs.c0 - rhs, lhs.c1 - rhs);
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x0002D583 File Offset: 0x0002B783
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 operator -(double lhs, double3x2 rhs)
		{
			return new double3x2(lhs - rhs.c0, lhs - rhs.c1);
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x0002D5A2 File Offset: 0x0002B7A2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 operator /(double3x2 lhs, double3x2 rhs)
		{
			return new double3x2(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1);
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x0002D5CB File Offset: 0x0002B7CB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 operator /(double3x2 lhs, double rhs)
		{
			return new double3x2(lhs.c0 / rhs, lhs.c1 / rhs);
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x0002D5EA File Offset: 0x0002B7EA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 operator /(double lhs, double3x2 rhs)
		{
			return new double3x2(lhs / rhs.c0, lhs / rhs.c1);
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x0002D609 File Offset: 0x0002B809
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 operator %(double3x2 lhs, double3x2 rhs)
		{
			return new double3x2(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1);
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x0002D632 File Offset: 0x0002B832
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 operator %(double3x2 lhs, double rhs)
		{
			return new double3x2(lhs.c0 % rhs, lhs.c1 % rhs);
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x0002D651 File Offset: 0x0002B851
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 operator %(double lhs, double3x2 rhs)
		{
			return new double3x2(lhs % rhs.c0, lhs % rhs.c1);
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x0002D670 File Offset: 0x0002B870
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 operator ++(double3x2 val)
		{
			double3 @double = double3.op_Increment(val.c0);
			val.c0 = @double;
			double3 double2 = @double;
			@double = double3.op_Increment(val.c1);
			val.c1 = @double;
			return new double3x2(double2, @double);
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x0002D6B8 File Offset: 0x0002B8B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 operator --(double3x2 val)
		{
			double3 @double = double3.op_Decrement(val.c0);
			val.c0 = @double;
			double3 double2 = @double;
			@double = double3.op_Decrement(val.c1);
			val.c1 = @double;
			return new double3x2(double2, @double);
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x0002D6FE File Offset: 0x0002B8FE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <(double3x2 lhs, double3x2 rhs)
		{
			return new bool3x2(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1);
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x0002D727 File Offset: 0x0002B927
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <(double3x2 lhs, double rhs)
		{
			return new bool3x2(lhs.c0 < rhs, lhs.c1 < rhs);
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x0002D746 File Offset: 0x0002B946
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <(double lhs, double3x2 rhs)
		{
			return new bool3x2(lhs < rhs.c0, lhs < rhs.c1);
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x0002D765 File Offset: 0x0002B965
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <=(double3x2 lhs, double3x2 rhs)
		{
			return new bool3x2(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1);
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x0002D78E File Offset: 0x0002B98E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <=(double3x2 lhs, double rhs)
		{
			return new bool3x2(lhs.c0 <= rhs, lhs.c1 <= rhs);
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x0002D7AD File Offset: 0x0002B9AD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <=(double lhs, double3x2 rhs)
		{
			return new bool3x2(lhs <= rhs.c0, lhs <= rhs.c1);
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x0002D7CC File Offset: 0x0002B9CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >(double3x2 lhs, double3x2 rhs)
		{
			return new bool3x2(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1);
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x0002D7F5 File Offset: 0x0002B9F5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >(double3x2 lhs, double rhs)
		{
			return new bool3x2(lhs.c0 > rhs, lhs.c1 > rhs);
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x0002D814 File Offset: 0x0002BA14
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >(double lhs, double3x2 rhs)
		{
			return new bool3x2(lhs > rhs.c0, lhs > rhs.c1);
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x0002D833 File Offset: 0x0002BA33
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >=(double3x2 lhs, double3x2 rhs)
		{
			return new bool3x2(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1);
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x0002D85C File Offset: 0x0002BA5C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >=(double3x2 lhs, double rhs)
		{
			return new bool3x2(lhs.c0 >= rhs, lhs.c1 >= rhs);
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x0002D87B File Offset: 0x0002BA7B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >=(double lhs, double3x2 rhs)
		{
			return new bool3x2(lhs >= rhs.c0, lhs >= rhs.c1);
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x0002D89A File Offset: 0x0002BA9A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 operator -(double3x2 val)
		{
			return new double3x2(-val.c0, -val.c1);
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x0002D8B7 File Offset: 0x0002BAB7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 operator +(double3x2 val)
		{
			return new double3x2(+val.c0, +val.c1);
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x0002D8D4 File Offset: 0x0002BAD4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator ==(double3x2 lhs, double3x2 rhs)
		{
			return new bool3x2(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1);
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x0002D8FD File Offset: 0x0002BAFD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator ==(double3x2 lhs, double rhs)
		{
			return new bool3x2(lhs.c0 == rhs, lhs.c1 == rhs);
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x0002D91C File Offset: 0x0002BB1C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator ==(double lhs, double3x2 rhs)
		{
			return new bool3x2(lhs == rhs.c0, lhs == rhs.c1);
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x0002D93B File Offset: 0x0002BB3B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator !=(double3x2 lhs, double3x2 rhs)
		{
			return new bool3x2(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1);
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x0002D964 File Offset: 0x0002BB64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator !=(double3x2 lhs, double rhs)
		{
			return new bool3x2(lhs.c0 != rhs, lhs.c1 != rhs);
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x0002D983 File Offset: 0x0002BB83
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator !=(double lhs, double3x2 rhs)
		{
			return new bool3x2(lhs != rhs.c0, lhs != rhs.c1);
		}

		// Token: 0x17000284 RID: 644
		public unsafe ref double3 this[int index]
		{
			get
			{
				fixed (double3x2* ptr = &this)
				{
					return ref *(double3*)(ptr + (IntPtr)index * (IntPtr)sizeof(double3) / (IntPtr)sizeof(double3x2));
				}
			}
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x0002D9BF File Offset: 0x0002BBBF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(double3x2 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1);
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x0002D9E8 File Offset: 0x0002BBE8
		public override bool Equals(object o)
		{
			if (o is double3x2)
			{
				double3x2 converted = (double3x2)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x0002DA0D File Offset: 0x0002BC0D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x0002DA1C File Offset: 0x0002BC1C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("double3x2({0}, {1},  {2}, {3},  {4}, {5})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c0.y,
				this.c1.y,
				this.c0.z,
				this.c1.z
			});
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x0002DAAC File Offset: 0x0002BCAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("double3x2({0}, {1},  {2}, {3},  {4}, {5})", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c0.z.ToString(format, formatProvider),
				this.c1.z.ToString(format, formatProvider)
			});
		}

		// Token: 0x04000083 RID: 131
		public double3 c0;

		// Token: 0x04000084 RID: 132
		public double3 c1;

		// Token: 0x04000085 RID: 133
		public static readonly double3x2 zero;
	}
}
