using System;

namespace System.Numerics
{
	// Token: 0x02000005 RID: 5
	internal static class BigIntegerCalculator
	{
		// Token: 0x06000049 RID: 73 RVA: 0x000038E8 File Offset: 0x00001AE8
		public static uint[] Add(uint[] left, uint right)
		{
			uint[] array = new uint[left.Length + 1];
			long num = (long)((ulong)left[0] + (ulong)right);
			array[0] = (uint)num;
			long num2 = num >> 32;
			for (int i = 1; i < left.Length; i++)
			{
				num = (long)((ulong)left[i] + (ulong)num2);
				array[i] = (uint)num;
				num2 = num >> 32;
			}
			array[left.Length] = (uint)num2;
			return array;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000393C File Offset: 0x00001B3C
		public unsafe static uint[] Add(uint[] left, uint[] right)
		{
			uint[] array = new uint[left.Length + 1];
			fixed (uint[] array2 = left)
			{
				uint* ptr;
				if (left == null || array2.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array2[0];
				}
				fixed (uint[] array3 = right)
				{
					uint* ptr2;
					if (right == null || array3.Length == 0)
					{
						ptr2 = null;
					}
					else
					{
						ptr2 = &array3[0];
					}
					fixed (uint* ptr3 = &array[0])
					{
						uint* ptr4 = ptr3;
						BigIntegerCalculator.Add(ptr, left.Length, ptr2, right.Length, ptr4, array.Length);
						array2 = null;
						array3 = null;
					}
					return array;
				}
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000039B4 File Offset: 0x00001BB4
		private unsafe static void Add(uint* left, int leftLength, uint* right, int rightLength, uint* bits, int bitsLength)
		{
			int i = 0;
			long num = 0L;
			while (i < rightLength)
			{
				long num2 = (long)((ulong)left[i] + (ulong)num + (ulong)right[i]);
				bits[i] = (uint)num2;
				num = num2 >> 32;
				i++;
			}
			while (i < leftLength)
			{
				long num3 = (long)((ulong)left[i] + (ulong)num);
				bits[i] = (uint)num3;
				num = num3 >> 32;
				i++;
			}
			bits[i] = (uint)num;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003A24 File Offset: 0x00001C24
		private unsafe static void AddSelf(uint* left, int leftLength, uint* right, int rightLength)
		{
			int i = 0;
			long num = 0L;
			while (i < rightLength)
			{
				long num2 = (long)((ulong)left[i] + (ulong)num + (ulong)right[i]);
				left[i] = (uint)num2;
				num = num2 >> 32;
				i++;
			}
			while (num != 0L && i < leftLength)
			{
				long num3 = (long)((ulong)left[i] + (ulong)num);
				left[i] = (uint)num3;
				num = num3 >> 32;
				i++;
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003A88 File Offset: 0x00001C88
		public static uint[] Subtract(uint[] left, uint right)
		{
			uint[] array = new uint[left.Length];
			long num = (long)((ulong)left[0] - (ulong)right);
			array[0] = (uint)num;
			long num2 = num >> 32;
			for (int i = 1; i < left.Length; i++)
			{
				num = (long)((ulong)left[i] + (ulong)num2);
				array[i] = (uint)num;
				num2 = num >> 32;
			}
			return array;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003AD0 File Offset: 0x00001CD0
		public unsafe static uint[] Subtract(uint[] left, uint[] right)
		{
			uint[] array = new uint[left.Length];
			fixed (uint[] array2 = left)
			{
				uint* ptr;
				if (left == null || array2.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array2[0];
				}
				uint[] array4;
				fixed (uint[] array3 = right)
				{
					uint* ptr2;
					if (right == null || array3.Length == 0)
					{
						ptr2 = null;
					}
					else
					{
						ptr2 = &array3[0];
					}
					uint* ptr3;
					if ((array4 = array) == null || array4.Length == 0)
					{
						ptr3 = null;
					}
					else
					{
						ptr3 = &array4[0];
					}
					BigIntegerCalculator.Subtract(ptr, left.Length, ptr2, right.Length, ptr3, array.Length);
					array2 = null;
				}
				array4 = null;
				return array;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003B54 File Offset: 0x00001D54
		private unsafe static void Subtract(uint* left, int leftLength, uint* right, int rightLength, uint* bits, int bitsLength)
		{
			int i = 0;
			long num = 0L;
			while (i < rightLength)
			{
				long num2 = (long)((ulong)left[i] + (ulong)num - (ulong)right[i]);
				bits[i] = (uint)num2;
				num = num2 >> 32;
				i++;
			}
			while (i < leftLength)
			{
				long num3 = (long)((ulong)left[i] + (ulong)num);
				bits[i] = (uint)num3;
				num = num3 >> 32;
				i++;
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003BB8 File Offset: 0x00001DB8
		public static int Compare(uint[] left, uint[] right)
		{
			if (left.Length < right.Length)
			{
				return -1;
			}
			if (left.Length > right.Length)
			{
				return 1;
			}
			for (int i = left.Length - 1; i >= 0; i--)
			{
				if (left[i] < right[i])
				{
					return -1;
				}
				if (left[i] > right[i])
				{
					return 1;
				}
			}
			return 0;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003C00 File Offset: 0x00001E00
		public static uint[] Divide(uint[] left, uint right)
		{
			uint[] array = new uint[left.Length];
			ulong num = 0UL;
			for (int i = left.Length - 1; i >= 0; i--)
			{
				ulong num2 = (num << 32) | (ulong)left[i];
				ulong num3 = num2 / (ulong)right;
				array[i] = (uint)num3;
				num = num2 - num3 * (ulong)right;
			}
			return array;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003C44 File Offset: 0x00001E44
		public static uint Remainder(uint[] left, uint right)
		{
			ulong num = 0UL;
			for (int i = left.Length - 1; i >= 0; i--)
			{
				num = ((num << 32) | (ulong)left[i]) % (ulong)right;
			}
			return (uint)num;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003C74 File Offset: 0x00001E74
		public unsafe static uint[] Divide(uint[] left, uint[] right)
		{
			uint[] array = BigIntegerCalculator.CreateCopy(left);
			uint[] array2 = new uint[left.Length - right.Length + 1];
			fixed (uint* ptr = &array[0])
			{
				uint* ptr2 = ptr;
				fixed (uint* ptr3 = &right[0])
				{
					uint* ptr4 = ptr3;
					fixed (uint* ptr5 = &array2[0])
					{
						uint* ptr6 = ptr5;
						BigIntegerCalculator.Divide(ptr2, array.Length, ptr4, right.Length, ptr6, array2.Length);
						ptr = null;
						ptr3 = null;
					}
					return array2;
				}
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003CDC File Offset: 0x00001EDC
		public unsafe static uint[] Remainder(uint[] left, uint[] right)
		{
			uint[] array = BigIntegerCalculator.CreateCopy(left);
			fixed (uint* ptr = &array[0])
			{
				uint* ptr2 = ptr;
				fixed (uint* ptr3 = &right[0])
				{
					uint* ptr4 = ptr3;
					BigIntegerCalculator.Divide(ptr2, array.Length, ptr4, right.Length, null, 0);
					ptr = null;
				}
				return array;
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003D1C File Offset: 0x00001F1C
		private unsafe static void Divide(uint* left, int leftLength, uint* right, int rightLength, uint* bits, int bitsLength)
		{
			uint num = right[rightLength - 1];
			uint num2 = ((rightLength > 1) ? right[rightLength - 2] : 0U);
			int num3 = BigIntegerCalculator.LeadingZeros(num);
			int num4 = 32 - num3;
			if (num3 > 0)
			{
				uint num5 = ((rightLength > 2) ? right[rightLength - 3] : 0U);
				num = (num << num3) | (num2 >> num4);
				num2 = (num2 << num3) | (num5 >> num4);
			}
			for (int i = leftLength; i >= rightLength; i--)
			{
				int num6 = i - rightLength;
				uint num7 = ((i < leftLength) ? left[i] : 0U);
				ulong num8 = ((ulong)num7 << 32) | (ulong)left[i - 1];
				uint num9 = ((i > 1) ? left[i - 2] : 0U);
				if (num3 > 0)
				{
					uint num10 = ((i > 2) ? left[i - 3] : 0U);
					num8 = (num8 << num3) | (ulong)(num9 >> num4);
					num9 = (num9 << num3) | (num10 >> num4);
				}
				ulong num11 = num8 / (ulong)num;
				if (num11 > (ulong)(-1))
				{
					num11 = (ulong)(-1);
				}
				while (BigIntegerCalculator.DivideGuessTooBig(num11, num8, num9, num, num2))
				{
					num11 -= 1UL;
				}
				if (num11 > 0UL && BigIntegerCalculator.SubtractDivisor(left + num6, leftLength - num6, right, rightLength, num11) != num7)
				{
					BigIntegerCalculator.AddDivisor(left + num6, leftLength - num6, right, rightLength);
					num11 -= 1UL;
				}
				if (bitsLength != 0)
				{
					bits[num6] = (uint)num11;
				}
				if (i < leftLength)
				{
					left[i] = 0U;
				}
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003E94 File Offset: 0x00002094
		private unsafe static uint AddDivisor(uint* left, int leftLength, uint* right, int rightLength)
		{
			ulong num = 0UL;
			for (int i = 0; i < rightLength; i++)
			{
				ulong num2 = (ulong)left[i] + num + (ulong)right[i];
				left[i] = (uint)num2;
				num = num2 >> 32;
			}
			return (uint)num;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003ED4 File Offset: 0x000020D4
		private unsafe static uint SubtractDivisor(uint* left, int leftLength, uint* right, int rightLength, ulong q)
		{
			ulong num = 0UL;
			for (int i = 0; i < rightLength; i++)
			{
				num += (ulong)right[i] * q;
				uint num2 = (uint)num;
				num >>= 32;
				if (left[i] < num2)
				{
					num += 1UL;
				}
				left[i] = left[i] - num2;
			}
			return (uint)num;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003F28 File Offset: 0x00002128
		private static bool DivideGuessTooBig(ulong q, ulong valHi, uint valLo, uint divHi, uint divLo)
		{
			ulong num = (ulong)divHi * q;
			ulong num2 = (ulong)divLo * q;
			num += num2 >> 32;
			num2 &= (ulong)(-1);
			return num >= valHi && (num > valHi || (num2 >= (ulong)valLo && num2 > (ulong)valLo));
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003F68 File Offset: 0x00002168
		private static uint[] CreateCopy(uint[] value)
		{
			uint[] array = new uint[value.Length];
			Array.Copy(value, 0, array, 0, array.Length);
			return array;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003F8C File Offset: 0x0000218C
		private static int LeadingZeros(uint value)
		{
			if (value == 0U)
			{
				return 32;
			}
			int num = 0;
			if ((value & 4294901760U) == 0U)
			{
				num += 16;
				value <<= 16;
			}
			if ((value & 4278190080U) == 0U)
			{
				num += 8;
				value <<= 8;
			}
			if ((value & 4026531840U) == 0U)
			{
				num += 4;
				value <<= 4;
			}
			if ((value & 3221225472U) == 0U)
			{
				num += 2;
				value <<= 2;
			}
			if ((value & 2147483648U) == 0U)
			{
				num++;
			}
			return num;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003FFC File Offset: 0x000021FC
		public unsafe static uint[] Square(uint[] value)
		{
			uint[] array = new uint[value.Length + value.Length];
			uint[] array3;
			fixed (uint[] array2 = value)
			{
				uint* ptr;
				if (value == null || array2.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array2[0];
				}
				uint* ptr2;
				if ((array3 = array) == null || array3.Length == 0)
				{
					ptr2 = null;
				}
				else
				{
					ptr2 = &array3[0];
				}
				BigIntegerCalculator.Square(ptr, value.Length, ptr2, array.Length);
			}
			array3 = null;
			return array;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000405C File Offset: 0x0000225C
		private unsafe static void Square(uint* value, int valueLength, uint* bits, int bitsLength)
		{
			if (valueLength < BigIntegerCalculator.SquareThreshold)
			{
				for (int i = 0; i < valueLength; i++)
				{
					ulong num = 0UL;
					for (int j = 0; j < i; j++)
					{
						ulong num2 = (ulong)bits[i + j] + num;
						ulong num3 = (ulong)value[j] * (ulong)value[i];
						bits[i + j] = (uint)(num2 + (num3 << 1));
						num = num3 + (num2 >> 1) >> 31;
					}
					ulong num4 = (ulong)value[i] * (ulong)value[i] + num;
					bits[i + i] = (uint)num4;
					bits[i + i + 1] = (uint)(num4 >> 32);
				}
				return;
			}
			int num5 = valueLength >> 1;
			int num6 = num5 << 1;
			int num7 = num5;
			uint* ptr = value + num5;
			int num8 = valueLength - num5;
			int num9 = num6;
			uint* ptr2 = bits + num6;
			int num10 = bitsLength - num6;
			BigIntegerCalculator.Square(value, num7, bits, num9);
			BigIntegerCalculator.Square(ptr, num8, ptr2, num10);
			int num11 = num8 + 1;
			int num12 = num11 + num11;
			if (num12 < BigIntegerCalculator.AllocationThreshold)
			{
				uint* ptr4;
				checked
				{
					uint* ptr3 = stackalloc uint[unchecked((UIntPtr)num11) * 4];
					ptr4 = stackalloc uint[unchecked((UIntPtr)num12) * 4];
					BigIntegerCalculator.Add(ptr, num8, value, num7, ptr3, num11);
					BigIntegerCalculator.Square(ptr3, num11, ptr4, num12);
					BigIntegerCalculator.SubtractCore(ptr2, num10, bits, num9, ptr4, num12);
				}
				BigIntegerCalculator.AddSelf(bits + num5, bitsLength - num5, ptr4, num12);
				return;
			}
			uint[] array;
			uint* ptr5;
			if ((array = new uint[num11]) == null || array.Length == 0)
			{
				ptr5 = null;
			}
			else
			{
				ptr5 = &array[0];
			}
			uint[] array2;
			uint* ptr6;
			if ((array2 = new uint[num12]) == null || array2.Length == 0)
			{
				ptr6 = null;
			}
			else
			{
				ptr6 = &array2[0];
			}
			BigIntegerCalculator.Add(ptr, num8, value, num7, ptr5, num11);
			BigIntegerCalculator.Square(ptr5, num11, ptr6, num12);
			BigIntegerCalculator.SubtractCore(ptr2, num10, bits, num9, ptr6, num12);
			BigIntegerCalculator.AddSelf(bits + num5, bitsLength - num5, ptr6, num12);
			array = null;
			array2 = null;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000424C File Offset: 0x0000244C
		public static uint[] Multiply(uint[] left, uint right)
		{
			int i = 0;
			ulong num = 0UL;
			uint[] array = new uint[left.Length + 1];
			while (i < left.Length)
			{
				ulong num2 = (ulong)left[i] * (ulong)right + num;
				array[i] = (uint)num2;
				num = num2 >> 32;
				i++;
			}
			array[i] = (uint)num;
			return array;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00004290 File Offset: 0x00002490
		public unsafe static uint[] Multiply(uint[] left, uint[] right)
		{
			uint[] array = new uint[left.Length + right.Length];
			fixed (uint[] array2 = left)
			{
				uint* ptr;
				if (left == null || array2.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array2[0];
				}
				uint[] array4;
				fixed (uint[] array3 = right)
				{
					uint* ptr2;
					if (right == null || array3.Length == 0)
					{
						ptr2 = null;
					}
					else
					{
						ptr2 = &array3[0];
					}
					uint* ptr3;
					if ((array4 = array) == null || array4.Length == 0)
					{
						ptr3 = null;
					}
					else
					{
						ptr3 = &array4[0];
					}
					BigIntegerCalculator.Multiply(ptr, left.Length, ptr2, right.Length, ptr3, array.Length);
					array2 = null;
				}
				array4 = null;
				return array;
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00004318 File Offset: 0x00002518
		private unsafe static void Multiply(uint* left, int leftLength, uint* right, int rightLength, uint* bits, int bitsLength)
		{
			if (rightLength < BigIntegerCalculator.MultiplyThreshold)
			{
				for (int i = 0; i < rightLength; i++)
				{
					ulong num = 0UL;
					for (int j = 0; j < leftLength; j++)
					{
						ulong num2 = (ulong)bits[i + j] + num + (ulong)left[j] * (ulong)right[i];
						bits[i + j] = (uint)num2;
						num = num2 >> 32;
					}
					bits[i + leftLength] = (uint)num;
				}
				return;
			}
			int num3 = rightLength >> 1;
			int num4 = num3 << 1;
			int num5 = num3;
			uint* ptr = left + num3;
			int num6 = leftLength - num3;
			int num7 = num3;
			uint* ptr2 = right + num3;
			int num8 = rightLength - num3;
			int num9 = num4;
			uint* ptr3 = bits + num4;
			int num10 = bitsLength - num4;
			BigIntegerCalculator.Multiply(left, num5, right, num7, bits, num9);
			BigIntegerCalculator.Multiply(ptr, num6, ptr2, num8, ptr3, num10);
			int num11 = num6 + 1;
			int num12 = num8 + 1;
			int num13 = num11 + num12;
			if (num13 < BigIntegerCalculator.AllocationThreshold)
			{
				uint* ptr6;
				checked
				{
					uint* ptr4 = stackalloc uint[unchecked((UIntPtr)num11) * 4];
					uint* ptr5 = stackalloc uint[unchecked((UIntPtr)num12) * 4];
					ptr6 = stackalloc uint[unchecked((UIntPtr)num13) * 4];
					BigIntegerCalculator.Add(ptr, num6, left, num5, ptr4, num11);
					BigIntegerCalculator.Add(ptr2, num8, right, num7, ptr5, num12);
					BigIntegerCalculator.Multiply(ptr4, num11, ptr5, num12, ptr6, num13);
					BigIntegerCalculator.SubtractCore(ptr3, num10, bits, num9, ptr6, num13);
				}
				BigIntegerCalculator.AddSelf(bits + num3, bitsLength - num3, ptr6, num13);
				return;
			}
			uint[] array;
			uint* ptr7;
			if ((array = new uint[num11]) == null || array.Length == 0)
			{
				ptr7 = null;
			}
			else
			{
				ptr7 = &array[0];
			}
			uint[] array2;
			uint* ptr8;
			if ((array2 = new uint[num12]) == null || array2.Length == 0)
			{
				ptr8 = null;
			}
			else
			{
				ptr8 = &array2[0];
			}
			uint[] array3;
			uint* ptr9;
			if ((array3 = new uint[num13]) == null || array3.Length == 0)
			{
				ptr9 = null;
			}
			else
			{
				ptr9 = &array3[0];
			}
			BigIntegerCalculator.Add(ptr, num6, left, num5, ptr7, num11);
			BigIntegerCalculator.Add(ptr2, num8, right, num7, ptr8, num12);
			BigIntegerCalculator.Multiply(ptr7, num11, ptr8, num12, ptr9, num13);
			BigIntegerCalculator.SubtractCore(ptr3, num10, bits, num9, ptr9, num13);
			BigIntegerCalculator.AddSelf(bits + num3, bitsLength - num3, ptr9, num13);
			array = null;
			array2 = null;
			array3 = null;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004558 File Offset: 0x00002758
		private unsafe static void SubtractCore(uint* left, int leftLength, uint* right, int rightLength, uint* core, int coreLength)
		{
			int i = 0;
			long num = 0L;
			while (i < rightLength)
			{
				long num2 = (long)((ulong)core[i] + (ulong)num - (ulong)left[i] - (ulong)right[i]);
				core[i] = (uint)num2;
				num = num2 >> 32;
				i++;
			}
			while (i < leftLength)
			{
				long num3 = (long)((ulong)core[i] + (ulong)num - (ulong)left[i]);
				core[i] = (uint)num3;
				num = num3 >> 32;
				i++;
			}
			while (num != 0L && i < coreLength)
			{
				long num4 = (long)((ulong)core[i] + (ulong)num);
				core[i] = (uint)num4;
				num = num4 >> 32;
				i++;
			}
		}

		// Token: 0x0400000C RID: 12
		private static int ReducerThreshold = 32;

		// Token: 0x0400000D RID: 13
		private static int SquareThreshold = 32;

		// Token: 0x0400000E RID: 14
		private static int AllocationThreshold = 256;

		// Token: 0x0400000F RID: 15
		private static int MultiplyThreshold = 32;
	}
}
