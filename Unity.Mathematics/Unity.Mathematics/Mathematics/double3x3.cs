using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000024 RID: 36
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct double3x3 : IEquatable<double3x3>, IFormattable
	{
		// Token: 0x06000D7A RID: 3450 RVA: 0x0002DB47 File Offset: 0x0002BD47
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x3(double3 c0, double3 c1, double3 c2)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x0002DB5E File Offset: 0x0002BD5E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x3(double m00, double m01, double m02, double m10, double m11, double m12, double m20, double m21, double m22)
		{
			this.c0 = new double3(m00, m10, m20);
			this.c1 = new double3(m01, m11, m21);
			this.c2 = new double3(m02, m12, m22);
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x0002DB90 File Offset: 0x0002BD90
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x3(double v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x0002DBB8 File Offset: 0x0002BDB8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x3(bool v)
		{
			this.c0 = math.select(new double3(0.0), new double3(1.0), v);
			this.c1 = math.select(new double3(0.0), new double3(1.0), v);
			this.c2 = math.select(new double3(0.0), new double3(1.0), v);
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x0002DC40 File Offset: 0x0002BE40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x3(bool3x3 v)
		{
			this.c0 = math.select(new double3(0.0), new double3(1.0), v.c0);
			this.c1 = math.select(new double3(0.0), new double3(1.0), v.c1);
			this.c2 = math.select(new double3(0.0), new double3(1.0), v.c2);
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x0002DCD4 File Offset: 0x0002BED4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x3(int v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x0002DCFA File Offset: 0x0002BEFA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x3(int3x3 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x0002DD2F File Offset: 0x0002BF2F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x3(uint v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x0002DD55 File Offset: 0x0002BF55
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x3(uint3x3 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x0002DD8A File Offset: 0x0002BF8A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x3(float v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x0002DDB0 File Offset: 0x0002BFB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3x3(float3x3 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x0000A3FC File Offset: 0x000085FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3x3(double v)
		{
			return new double3x3(v);
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x0000A404 File Offset: 0x00008604
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double3x3(bool v)
		{
			return new double3x3(v);
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x0000A40C File Offset: 0x0000860C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double3x3(bool3x3 v)
		{
			return new double3x3(v);
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x0000A414 File Offset: 0x00008614
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3x3(int v)
		{
			return new double3x3(v);
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x0000A41C File Offset: 0x0000861C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3x3(int3x3 v)
		{
			return new double3x3(v);
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x0000A424 File Offset: 0x00008624
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3x3(uint v)
		{
			return new double3x3(v);
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x0000A42C File Offset: 0x0000862C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3x3(uint3x3 v)
		{
			return new double3x3(v);
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x0000A434 File Offset: 0x00008634
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3x3(float v)
		{
			return new double3x3(v);
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x0000A43C File Offset: 0x0000863C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3x3(float3x3 v)
		{
			return new double3x3(v);
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x0002DDE5 File Offset: 0x0002BFE5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 operator *(double3x3 lhs, double3x3 rhs)
		{
			return new double3x3(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2);
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x0002DE1F File Offset: 0x0002C01F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 operator *(double3x3 lhs, double rhs)
		{
			return new double3x3(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs);
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x0002DE4A File Offset: 0x0002C04A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 operator *(double lhs, double3x3 rhs)
		{
			return new double3x3(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2);
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x0002DE75 File Offset: 0x0002C075
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 operator +(double3x3 lhs, double3x3 rhs)
		{
			return new double3x3(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2);
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x0002DEAF File Offset: 0x0002C0AF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 operator +(double3x3 lhs, double rhs)
		{
			return new double3x3(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs);
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x0002DEDA File Offset: 0x0002C0DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 operator +(double lhs, double3x3 rhs)
		{
			return new double3x3(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2);
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x0002DF05 File Offset: 0x0002C105
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 operator -(double3x3 lhs, double3x3 rhs)
		{
			return new double3x3(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2);
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x0002DF3F File Offset: 0x0002C13F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 operator -(double3x3 lhs, double rhs)
		{
			return new double3x3(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs);
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x0002DF6A File Offset: 0x0002C16A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 operator -(double lhs, double3x3 rhs)
		{
			return new double3x3(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2);
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x0002DF95 File Offset: 0x0002C195
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 operator /(double3x3 lhs, double3x3 rhs)
		{
			return new double3x3(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2);
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x0002DFCF File Offset: 0x0002C1CF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 operator /(double3x3 lhs, double rhs)
		{
			return new double3x3(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs);
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x0002DFFA File Offset: 0x0002C1FA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 operator /(double lhs, double3x3 rhs)
		{
			return new double3x3(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2);
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x0002E025 File Offset: 0x0002C225
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 operator %(double3x3 lhs, double3x3 rhs)
		{
			return new double3x3(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2);
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x0002E05F File Offset: 0x0002C25F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 operator %(double3x3 lhs, double rhs)
		{
			return new double3x3(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs);
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x0002E08A File Offset: 0x0002C28A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 operator %(double lhs, double3x3 rhs)
		{
			return new double3x3(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2);
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x0002E0B8 File Offset: 0x0002C2B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 operator ++(double3x3 val)
		{
			double3 @double = double3.op_Increment(val.c0);
			val.c0 = @double;
			double3 double2 = @double;
			@double = double3.op_Increment(val.c1);
			val.c1 = @double;
			double3 double3 = @double;
			@double = double3.op_Increment(val.c2);
			val.c2 = @double;
			return new double3x3(double2, double3, @double);
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x0002E118 File Offset: 0x0002C318
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 operator --(double3x3 val)
		{
			double3 @double = double3.op_Decrement(val.c0);
			val.c0 = @double;
			double3 double2 = @double;
			@double = double3.op_Decrement(val.c1);
			val.c1 = @double;
			double3 double3 = @double;
			@double = double3.op_Decrement(val.c2);
			val.c2 = @double;
			return new double3x3(double2, double3, @double);
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x0002E178 File Offset: 0x0002C378
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator <(double3x3 lhs, double3x3 rhs)
		{
			return new bool3x3(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2);
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x0002E1B2 File Offset: 0x0002C3B2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator <(double3x3 lhs, double rhs)
		{
			return new bool3x3(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs);
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x0002E1DD File Offset: 0x0002C3DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator <(double lhs, double3x3 rhs)
		{
			return new bool3x3(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2);
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x0002E208 File Offset: 0x0002C408
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator <=(double3x3 lhs, double3x3 rhs)
		{
			return new bool3x3(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2);
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x0002E242 File Offset: 0x0002C442
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator <=(double3x3 lhs, double rhs)
		{
			return new bool3x3(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs);
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x0002E26D File Offset: 0x0002C46D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator <=(double lhs, double3x3 rhs)
		{
			return new bool3x3(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2);
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x0002E298 File Offset: 0x0002C498
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator >(double3x3 lhs, double3x3 rhs)
		{
			return new bool3x3(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2);
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x0002E2D2 File Offset: 0x0002C4D2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator >(double3x3 lhs, double rhs)
		{
			return new bool3x3(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs);
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x0002E2FD File Offset: 0x0002C4FD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator >(double lhs, double3x3 rhs)
		{
			return new bool3x3(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2);
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x0002E328 File Offset: 0x0002C528
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator >=(double3x3 lhs, double3x3 rhs)
		{
			return new bool3x3(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2);
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x0002E362 File Offset: 0x0002C562
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator >=(double3x3 lhs, double rhs)
		{
			return new bool3x3(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs);
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x0002E38D File Offset: 0x0002C58D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator >=(double lhs, double3x3 rhs)
		{
			return new bool3x3(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2);
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x0002E3B8 File Offset: 0x0002C5B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 operator -(double3x3 val)
		{
			return new double3x3(-val.c0, -val.c1, -val.c2);
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x0002E3E0 File Offset: 0x0002C5E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 operator +(double3x3 val)
		{
			return new double3x3(+val.c0, +val.c1, +val.c2);
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x0002E408 File Offset: 0x0002C608
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator ==(double3x3 lhs, double3x3 rhs)
		{
			return new bool3x3(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2);
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x0002E442 File Offset: 0x0002C642
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator ==(double3x3 lhs, double rhs)
		{
			return new bool3x3(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs);
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x0002E46D File Offset: 0x0002C66D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator ==(double lhs, double3x3 rhs)
		{
			return new bool3x3(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2);
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x0002E498 File Offset: 0x0002C698
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator !=(double3x3 lhs, double3x3 rhs)
		{
			return new bool3x3(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2);
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x0002E4D2 File Offset: 0x0002C6D2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator !=(double3x3 lhs, double rhs)
		{
			return new bool3x3(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs);
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x0002E4FD File Offset: 0x0002C6FD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator !=(double lhs, double3x3 rhs)
		{
			return new bool3x3(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2);
		}

		// Token: 0x17000285 RID: 645
		public unsafe ref double3 this[int index]
		{
			get
			{
				fixed (double3x3* ptr = &this)
				{
					return ref *(double3*)(ptr + (IntPtr)index * (IntPtr)sizeof(double3) / (IntPtr)sizeof(double3x3));
				}
			}
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x0002E543 File Offset: 0x0002C743
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(double3x3 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2);
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x0002E580 File Offset: 0x0002C780
		public override bool Equals(object o)
		{
			if (o is double3x3)
			{
				double3x3 converted = (double3x3)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x0002E5A5 File Offset: 0x0002C7A5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x0002E5B4 File Offset: 0x0002C7B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("double3x3({0}, {1}, {2},  {3}, {4}, {5},  {6}, {7}, {8})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c2.x,
				this.c0.y,
				this.c1.y,
				this.c2.y,
				this.c0.z,
				this.c1.z,
				this.c2.z
			});
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x0002E680 File Offset: 0x0002C880
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("double3x3({0}, {1}, {2},  {3}, {4}, {5},  {6}, {7}, {8})", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c2.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c2.y.ToString(format, formatProvider),
				this.c0.z.ToString(format, formatProvider),
				this.c1.z.ToString(format, formatProvider),
				this.c2.z.ToString(format, formatProvider)
			});
		}

		// Token: 0x04000086 RID: 134
		public double3 c0;

		// Token: 0x04000087 RID: 135
		public double3 c1;

		// Token: 0x04000088 RID: 136
		public double3 c2;

		// Token: 0x04000089 RID: 137
		public static readonly double3x3 identity = new double3x3(1.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 1.0);

		// Token: 0x0400008A RID: 138
		public static readonly double3x3 zero;
	}
}
