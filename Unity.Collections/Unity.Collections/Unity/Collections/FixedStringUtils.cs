using System;
using System.Runtime.InteropServices;

namespace Unity.Collections
{
	// Token: 0x0200007C RID: 124
	[GenerateTestsForBurstCompatibility]
	internal static class FixedStringUtils
	{
		// Token: 0x060006C5 RID: 1733 RVA: 0x000169D4 File Offset: 0x00014BD4
		internal static ParseError Base10ToBase2(ref float output, ulong mantissa10, int exponent10)
		{
			if (mantissa10 == 0UL)
			{
				output = 0f;
				return ParseError.None;
			}
			if (exponent10 == 0)
			{
				output = mantissa10;
				return ParseError.None;
			}
			int exponent11 = exponent10;
			ulong mantissa11 = mantissa10;
			while (exponent10 > 0)
			{
				while ((mantissa11 & 16140901064495857664UL) != 0UL)
				{
					mantissa11 >>= 1;
					exponent11++;
				}
				mantissa11 *= 5UL;
				exponent10--;
			}
			while (exponent10 < 0)
			{
				while ((mantissa11 & 9223372036854775808UL) == 0UL)
				{
					mantissa11 <<= 1;
					exponent11--;
				}
				mantissa11 /= 5UL;
				exponent10++;
			}
			FixedStringUtils.UintFloatUnion ufu = new FixedStringUtils.UintFloatUnion
			{
				floatValue = mantissa11
			};
			int e = (int)(((ufu.uintValue >> 23) & 255U) - 127U);
			e += exponent11;
			if (e > 128)
			{
				return ParseError.Overflow;
			}
			if (e < -127)
			{
				return ParseError.Underflow;
			}
			ufu.uintValue = (ufu.uintValue & 2155872255U) | (uint)((uint)(e + 127) << 23);
			output = ufu.floatValue;
			return ParseError.None;
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00016AA4 File Offset: 0x00014CA4
		internal static void Base2ToBase10(ref ulong mantissa10, ref int exponent10, float input)
		{
			FixedStringUtils.UintFloatUnion ufu = new FixedStringUtils.UintFloatUnion
			{
				floatValue = input
			};
			if (ufu.uintValue == 0U)
			{
				mantissa10 = 0UL;
				exponent10 = 0;
				return;
			}
			uint mantissa11 = (ufu.uintValue & 8388607U) | 8388608U;
			int exponent11 = (int)((ufu.uintValue >> 23) - 127U - 23U);
			mantissa10 = (ulong)mantissa11;
			exponent10 = exponent11;
			if (exponent11 > 0)
			{
				while (exponent11 > 0)
				{
					while (mantissa10 <= 1844674407370955161UL)
					{
						mantissa10 *= 10UL;
						exponent10--;
					}
					mantissa10 /= 5UL;
					exponent11--;
				}
			}
			if (exponent11 < 0)
			{
				while (exponent11 < 0)
				{
					while (mantissa10 > 3689348814741910323UL)
					{
						mantissa10 /= 10UL;
						exponent10++;
					}
					mantissa10 *= 5UL;
					exponent11++;
				}
			}
			while (mantissa10 > 9999999UL || mantissa10 % 10UL == 0UL)
			{
				mantissa10 = (mantissa10 + ((mantissa10 < 100000000UL) ? 5UL : 0UL)) / 10UL;
				exponent10++;
			}
		}

		// Token: 0x0200007D RID: 125
		[StructLayout(LayoutKind.Explicit)]
		internal struct UintFloatUnion
		{
			// Token: 0x040003A0 RID: 928
			[FieldOffset(0)]
			public uint uintValue;

			// Token: 0x040003A1 RID: 929
			[FieldOffset(0)]
			public float floatValue;
		}
	}
}
