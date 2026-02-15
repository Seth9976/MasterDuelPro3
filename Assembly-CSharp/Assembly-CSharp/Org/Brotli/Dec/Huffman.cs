using System;

namespace Org.Brotli.Dec
{
	// Token: 0x0200007E RID: 126
	internal sealed class Huffman
	{
		// Token: 0x0600026D RID: 621 RVA: 0x00008F74 File Offset: 0x00007174
		private static int GetNextKey(int key, int len)
		{
			int step = 1 << len - 1;
			while ((key & step) != 0)
			{
				step >>= 1;
			}
			return (key & (step - 1)) + step;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00008F9C File Offset: 0x0000719C
		private static void ReplicateValue(int[] table, int offset, int step, int end, int item)
		{
			do
			{
				end -= step;
				table[offset + end] = item;
			}
			while (end > 0);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00008FB0 File Offset: 0x000071B0
		private static int NextTableBitSize(int[] count, int len, int rootBits)
		{
			int left = 1 << len - rootBits;
			while (len < 15)
			{
				left -= count[len];
				if (left <= 0)
				{
					break;
				}
				len++;
				left <<= 1;
			}
			return len - rootBits;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00008FE4 File Offset: 0x000071E4
		internal static void BuildHuffmanTable(int[] rootTable, int tableOffset, int rootBits, int[] codeLengths, int codeLengthsSize)
		{
			int[] sorted = new int[codeLengthsSize];
			int[] count = new int[16];
			int[] offset = new int[16];
			int symbol;
			for (symbol = 0; symbol < codeLengthsSize; symbol++)
			{
				count[codeLengths[symbol]]++;
			}
			offset[1] = 0;
			for (int len = 1; len < 15; len++)
			{
				offset[len + 1] = offset[len] + count[len];
			}
			for (symbol = 0; symbol < codeLengthsSize; symbol++)
			{
				if (codeLengths[symbol] != 0)
				{
					int[] array = sorted;
					int[] array2 = offset;
					int num = codeLengths[symbol];
					int num2 = array2[num];
					array2[num] = num2 + 1;
					array[num2] = symbol;
				}
			}
			int tableSize = 1 << rootBits;
			int totalSize = tableSize;
			int key;
			if (offset[15] == 1)
			{
				for (key = 0; key < totalSize; key++)
				{
					rootTable[tableOffset + key] = sorted[0];
				}
				return;
			}
			key = 0;
			symbol = 0;
			int len2 = 1;
			int step = 2;
			while (len2 <= rootBits)
			{
				while (count[len2] > 0)
				{
					Huffman.ReplicateValue(rootTable, tableOffset + key, step, tableSize, (len2 << 16) | sorted[symbol++]);
					key = Huffman.GetNextKey(key, len2);
					count[len2]--;
				}
				len2++;
				step <<= 1;
			}
			int mask = totalSize - 1;
			int low = -1;
			int currentOffset = tableOffset;
			int len3 = rootBits + 1;
			int step2 = 2;
			while (len3 <= 15)
			{
				while (count[len3] > 0)
				{
					if ((key & mask) != low)
					{
						currentOffset += tableSize;
						int tableBits = Huffman.NextTableBitSize(count, len3, rootBits);
						tableSize = 1 << tableBits;
						totalSize += tableSize;
						low = key & mask;
						rootTable[tableOffset + low] = (tableBits + rootBits << 16) | (currentOffset - tableOffset - low);
					}
					Huffman.ReplicateValue(rootTable, currentOffset + (key >> rootBits), step2, tableSize, (len3 - rootBits << 16) | sorted[symbol++]);
					key = Huffman.GetNextKey(key, len3);
					count[len3]--;
				}
				len3++;
				step2 <<= 1;
			}
		}

		// Token: 0x040002EA RID: 746
		internal const int HuffmanMaxTableSize = 1080;

		// Token: 0x040002EB RID: 747
		private const int MaxLength = 15;
	}
}
