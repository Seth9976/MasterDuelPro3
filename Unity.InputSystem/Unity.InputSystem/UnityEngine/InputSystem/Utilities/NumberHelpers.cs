using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200024D RID: 589
	internal static class NumberHelpers
	{
		// Token: 0x06001584 RID: 5508 RVA: 0x00062410 File Offset: 0x00060610
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int AlignToMultipleOf(this int number, int alignment)
		{
			int remainder = number % alignment;
			if (remainder == 0)
			{
				return number;
			}
			return number + alignment - remainder;
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x0006242C File Offset: 0x0006062C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long AlignToMultipleOf(this long number, long alignment)
		{
			long remainder = number % alignment;
			if (remainder == 0L)
			{
				return number;
			}
			return number + alignment - remainder;
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x00062448 File Offset: 0x00060648
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint AlignToMultipleOf(this uint number, uint alignment)
		{
			uint remainder = number % alignment;
			if (remainder == 0U)
			{
				return number;
			}
			return number + alignment - remainder;
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x00062463 File Offset: 0x00060663
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Approximately(double a, double b)
		{
			return Math.Abs(b - a) < Math.Max(1E-06 * Math.Max(Math.Abs(a), Math.Abs(b)), 4E-323);
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x00062498 File Offset: 0x00060698
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float IntToNormalizedFloat(int value, int minValue, int maxValue)
		{
			if (value <= minValue)
			{
				return 0f;
			}
			if (value >= maxValue)
			{
				return 1f;
			}
			return (float)(((double)value - (double)minValue) / ((double)maxValue - (double)minValue));
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x000624BA File Offset: 0x000606BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int NormalizedFloatToInt(float value, int intMinValue, int intMaxValue)
		{
			if (value <= 0f)
			{
				return intMinValue;
			}
			if (value >= 1f)
			{
				return intMaxValue;
			}
			return (int)((double)value * ((double)intMaxValue - (double)intMinValue) + (double)intMinValue);
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x000624DC File Offset: 0x000606DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float UIntToNormalizedFloat(uint value, uint minValue, uint maxValue)
		{
			if (value <= minValue)
			{
				return 0f;
			}
			if (value >= maxValue)
			{
				return 1f;
			}
			return (float)((value - minValue) / (maxValue - minValue));
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x00062502 File Offset: 0x00060702
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint NormalizedFloatToUInt(float value, uint uintMinValue, uint uintMaxValue)
		{
			if (value <= 0f)
			{
				return uintMinValue;
			}
			if (value >= 1f)
			{
				return uintMaxValue;
			}
			return (uint)((double)value * (uintMaxValue - uintMinValue) + uintMinValue);
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x00062528 File Offset: 0x00060728
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint RemapUIntBitsToNormalizeFloatToUIntBits(uint value, uint inBitSize, uint outBitSize)
		{
			uint inMaxValue = (uint)((1L << (int)inBitSize) - 1L);
			uint outMaxValue = (uint)((1L << (int)outBitSize) - 1L);
			return NumberHelpers.NormalizedFloatToUInt(NumberHelpers.UIntToNormalizedFloat(value, 0U, inMaxValue), 0U, outMaxValue);
		}
	}
}
