using System;

namespace SevenZip.Compression.RangeCoder
{
	// Token: 0x02000191 RID: 401
	internal struct BitTreeEncoder
	{
		// Token: 0x060005B1 RID: 1457 RVA: 0x0001AD1D File Offset: 0x00018F1D
		public BitTreeEncoder(int numBitLevels)
		{
			this.NumBitLevels = numBitLevels;
			this.Models = new BitEncoder[1 << numBitLevels];
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0001AD38 File Offset: 0x00018F38
		public void Init()
		{
			uint i = 1U;
			while ((ulong)i < (ulong)(1L << (this.NumBitLevels & 31)))
			{
				this.Models[(int)i].Init();
				i += 1U;
			}
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0001AD70 File Offset: 0x00018F70
		public void Encode(Encoder rangeEncoder, uint symbol)
		{
			uint i = 1U;
			int bitIndex = this.NumBitLevels;
			while (bitIndex > 0)
			{
				bitIndex--;
				uint bit = (symbol >> bitIndex) & 1U;
				this.Models[(int)i].Encode(rangeEncoder, bit);
				i = (i << 1) | bit;
			}
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0001ADB4 File Offset: 0x00018FB4
		public void ReverseEncode(Encoder rangeEncoder, uint symbol)
		{
			uint i = 1U;
			uint j = 0U;
			while ((ulong)j < (ulong)((long)this.NumBitLevels))
			{
				uint bit = symbol & 1U;
				this.Models[(int)i].Encode(rangeEncoder, bit);
				i = (i << 1) | bit;
				symbol >>= 1;
				j += 1U;
			}
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0001ADF8 File Offset: 0x00018FF8
		public uint GetPrice(uint symbol)
		{
			uint price = 0U;
			uint i = 1U;
			int bitIndex = this.NumBitLevels;
			while (bitIndex > 0)
			{
				bitIndex--;
				uint bit = (symbol >> bitIndex) & 1U;
				price += this.Models[(int)i].GetPrice(bit);
				i = (i << 1) + bit;
			}
			return price;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0001AE40 File Offset: 0x00019040
		public uint ReverseGetPrice(uint symbol)
		{
			uint price = 0U;
			uint i = 1U;
			for (int j = this.NumBitLevels; j > 0; j--)
			{
				uint bit = symbol & 1U;
				symbol >>= 1;
				price += this.Models[(int)i].GetPrice(bit);
				i = (i << 1) | bit;
			}
			return price;
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0001AE88 File Offset: 0x00019088
		public static uint ReverseGetPrice(BitEncoder[] Models, uint startIndex, int NumBitLevels, uint symbol)
		{
			uint price = 0U;
			uint i = 1U;
			for (int j = NumBitLevels; j > 0; j--)
			{
				uint bit = symbol & 1U;
				symbol >>= 1;
				price += Models[(int)(startIndex + i)].GetPrice(bit);
				i = (i << 1) | bit;
			}
			return price;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0001AEC8 File Offset: 0x000190C8
		public static void ReverseEncode(BitEncoder[] Models, uint startIndex, Encoder rangeEncoder, int NumBitLevels, uint symbol)
		{
			uint i = 1U;
			for (int j = 0; j < NumBitLevels; j++)
			{
				uint bit = symbol & 1U;
				Models[(int)(startIndex + i)].Encode(rangeEncoder, bit);
				i = (i << 1) | bit;
				symbol >>= 1;
			}
		}

		// Token: 0x04000A4D RID: 2637
		private BitEncoder[] Models;

		// Token: 0x04000A4E RID: 2638
		private int NumBitLevels;
	}
}
