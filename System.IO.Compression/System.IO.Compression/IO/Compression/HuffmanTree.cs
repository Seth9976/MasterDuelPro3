using System;

namespace System.IO.Compression
{
	// Token: 0x02000011 RID: 17
	internal sealed class HuffmanTree
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000064 RID: 100 RVA: 0x0000381D File Offset: 0x00001A1D
		public static HuffmanTree StaticLiteralLengthTree { get; } = new HuffmanTree(HuffmanTree.GetStaticLiteralTreeLength());

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003824 File Offset: 0x00001A24
		public static HuffmanTree StaticDistanceTree { get; } = new HuffmanTree(HuffmanTree.GetStaticDistanceTreeLength());

		// Token: 0x06000066 RID: 102 RVA: 0x0000382C File Offset: 0x00001A2C
		public HuffmanTree(byte[] codeLengths)
		{
			this._codeLengthArray = codeLengths;
			if (this._codeLengthArray.Length == 288)
			{
				this._tableBits = 9;
			}
			else
			{
				this._tableBits = 7;
			}
			this._tableMask = (1 << this._tableBits) - 1;
			this._table = new short[1 << this._tableBits];
			this._left = new short[2 * this._codeLengthArray.Length];
			this._right = new short[2 * this._codeLengthArray.Length];
			this.CreateTable();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000038C0 File Offset: 0x00001AC0
		private static byte[] GetStaticLiteralTreeLength()
		{
			byte[] array = new byte[288];
			for (int i = 0; i <= 143; i++)
			{
				array[i] = 8;
			}
			for (int j = 144; j <= 255; j++)
			{
				array[j] = 9;
			}
			for (int k = 256; k <= 279; k++)
			{
				array[k] = 7;
			}
			for (int l = 280; l <= 287; l++)
			{
				array[l] = 8;
			}
			return array;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000393C File Offset: 0x00001B3C
		private static byte[] GetStaticDistanceTreeLength()
		{
			byte[] array = new byte[32];
			for (int i = 0; i < 32; i++)
			{
				array[i] = 5;
			}
			return array;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003964 File Offset: 0x00001B64
		private uint[] CalculateHuffmanCode()
		{
			uint[] array = new uint[17];
			foreach (int num in this._codeLengthArray)
			{
				array[num] += 1U;
			}
			array[0] = 0U;
			uint[] array2 = new uint[17];
			uint num2 = 0U;
			for (int j = 1; j <= 16; j++)
			{
				num2 = num2 + array[j - 1] << 1;
				array2[j] = num2;
			}
			uint[] array3 = new uint[288];
			for (int k = 0; k < this._codeLengthArray.Length; k++)
			{
				int num3 = (int)this._codeLengthArray[k];
				if (num3 > 0)
				{
					array3[k] = FastEncoderStatics.BitReverse(array2[num3], num3);
					array2[num3] += 1U;
				}
			}
			return array3;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003A28 File Offset: 0x00001C28
		private void CreateTable()
		{
			uint[] array = this.CalculateHuffmanCode();
			short num = (short)this._codeLengthArray.Length;
			for (int i = 0; i < this._codeLengthArray.Length; i++)
			{
				int num2 = (int)this._codeLengthArray[i];
				if (num2 > 0)
				{
					int num3 = (int)array[i];
					if (num2 > this._tableBits)
					{
						int num4 = num2 - this._tableBits;
						int num5 = 1 << this._tableBits;
						int num6 = num3 & ((1 << this._tableBits) - 1);
						short[] array2 = this._table;
						do
						{
							short num7 = array2[num6];
							if (num7 == 0)
							{
								array2[num6] = -num;
								num7 = -num;
								num += 1;
							}
							if (num7 > 0)
							{
								goto Block_6;
							}
							if ((num3 & num5) == 0)
							{
								array2 = this._left;
							}
							else
							{
								array2 = this._right;
							}
							num6 = (int)(-(int)num7);
							num5 <<= 1;
							num4--;
						}
						while (num4 != 0);
						array2[num6] = (short)i;
						goto IL_0119;
						Block_6:
						throw new InvalidDataException("Failed to construct a huffman tree using the length array. The stream might be corrupted.");
					}
					int num8 = 1 << num2;
					if (num3 >= num8)
					{
						throw new InvalidDataException("Failed to construct a huffman tree using the length array. The stream might be corrupted.");
					}
					int num9 = 1 << this._tableBits - num2;
					for (int j = 0; j < num9; j++)
					{
						this._table[num3] = (short)i;
						num3 += num8;
					}
				}
				IL_0119:;
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003B60 File Offset: 0x00001D60
		public int GetNextSymbol(InputBuffer input)
		{
			uint num = input.TryLoad16Bits();
			if (input.AvailableBits == 0)
			{
				return -1;
			}
			int num2;
			checked
			{
				num2 = (int)this._table[(int)((IntPtr)(unchecked((ulong)num & (ulong)((long)this._tableMask))))];
			}
			if (num2 < 0)
			{
				uint num3 = 1U << this._tableBits;
				do
				{
					num2 = -num2;
					if ((num & num3) == 0U)
					{
						num2 = (int)this._left[num2];
					}
					else
					{
						num2 = (int)this._right[num2];
					}
					num3 <<= 1;
				}
				while (num2 < 0);
			}
			int num4 = (int)this._codeLengthArray[num2];
			if (num4 <= 0)
			{
				throw new InvalidDataException("Failed to construct a huffman tree using the length array. The stream might be corrupted.");
			}
			if (num4 > input.AvailableBits)
			{
				return -1;
			}
			input.SkipBits(num4);
			return num2;
		}

		// Token: 0x04000043 RID: 67
		private readonly int _tableBits;

		// Token: 0x04000044 RID: 68
		private readonly short[] _table;

		// Token: 0x04000045 RID: 69
		private readonly short[] _left;

		// Token: 0x04000046 RID: 70
		private readonly short[] _right;

		// Token: 0x04000047 RID: 71
		private readonly byte[] _codeLengthArray;

		// Token: 0x04000048 RID: 72
		private readonly int _tableMask;
	}
}
