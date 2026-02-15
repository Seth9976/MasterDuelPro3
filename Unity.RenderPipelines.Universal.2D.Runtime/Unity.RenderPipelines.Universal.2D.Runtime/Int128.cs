using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200000A RID: 10
	internal struct Int128
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002438 File Offset: 0x00000638
		public Int128(long _lo)
		{
			this.lo = (ulong)_lo;
			if (_lo < 0L)
			{
				this.hi = -1L;
				return;
			}
			this.hi = 0L;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002457 File Offset: 0x00000657
		public Int128(long _hi, ulong _lo)
		{
			this.lo = _lo;
			this.hi = _hi;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002467 File Offset: 0x00000667
		public Int128(Int128 val)
		{
			this.hi = val.hi;
			this.lo = val.lo;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002481 File Offset: 0x00000681
		public bool IsNegative()
		{
			return this.hi < 0L;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002490 File Offset: 0x00000690
		public static bool operator ==(Int128 val1, Int128 val2)
		{
			return val1 == val2 || (val1 != null && val2 != null && val1.hi == val2.hi && val1.lo == val2.lo);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000024DD File Offset: 0x000006DD
		public static bool operator !=(Int128 val1, Int128 val2)
		{
			return !(val1 == val2);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000024EC File Offset: 0x000006EC
		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is Int128))
			{
				return false;
			}
			Int128 i128 = (Int128)obj;
			return i128.hi == this.hi && i128.lo == this.lo;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000252B File Offset: 0x0000072B
		public override int GetHashCode()
		{
			return this.hi.GetHashCode() ^ this.lo.GetHashCode();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002544 File Offset: 0x00000744
		public static bool operator >(Int128 val1, Int128 val2)
		{
			if (val1.hi != val2.hi)
			{
				return val1.hi > val2.hi;
			}
			return val1.lo > val2.lo;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002571 File Offset: 0x00000771
		public static bool operator <(Int128 val1, Int128 val2)
		{
			if (val1.hi != val2.hi)
			{
				return val1.hi < val2.hi;
			}
			return val1.lo < val2.lo;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000259E File Offset: 0x0000079E
		public static Int128 operator +(Int128 lhs, Int128 rhs)
		{
			lhs.hi += rhs.hi;
			lhs.lo += rhs.lo;
			if (lhs.lo < rhs.lo)
			{
				lhs.hi += 1L;
			}
			return lhs;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000025DE File Offset: 0x000007DE
		public static Int128 operator -(Int128 lhs, Int128 rhs)
		{
			return lhs + -rhs;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000025EC File Offset: 0x000007EC
		public static Int128 operator -(Int128 val)
		{
			if (val.lo == 0UL)
			{
				return new Int128(-val.hi, 0UL);
			}
			return new Int128(~val.hi, ~val.lo + 1UL);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000261C File Offset: 0x0000081C
		public static explicit operator double(Int128 val)
		{
			if (val.hi >= 0L)
			{
				return val.lo + (double)val.hi * 1.8446744073709552E+19;
			}
			if (val.lo == 0UL)
			{
				return (double)val.hi * 1.8446744073709552E+19;
			}
			return -(~val.lo + (double)(~(double)val.hi) * 1.8446744073709552E+19);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002688 File Offset: 0x00000888
		public static Int128 Int128Mul(long lhs, long rhs)
		{
			bool flag = lhs < 0L != rhs < 0L;
			if (lhs < 0L)
			{
				lhs = -lhs;
			}
			if (rhs < 0L)
			{
				rhs = -rhs;
			}
			ulong num = (ulong)lhs >> 32;
			ulong int1Lo = (ulong)(lhs & (long)((ulong)(-1)));
			ulong int2Hi = (ulong)rhs >> 32;
			ulong int2Lo = (ulong)(rhs & (long)((ulong)(-1)));
			ulong a = num * int2Hi;
			ulong b = int1Lo * int2Lo;
			ulong c = num * int2Lo + int1Lo * int2Hi;
			long hi = (long)(a + (c >> 32));
			ulong lo = (c << 32) + b;
			if (lo < b)
			{
				hi += 1L;
			}
			Int128 result = new Int128(hi, lo);
			if (!flag)
			{
				return result;
			}
			return -result;
		}

		// Token: 0x04000011 RID: 17
		private long hi;

		// Token: 0x04000012 RID: 18
		private ulong lo;
	}
}
