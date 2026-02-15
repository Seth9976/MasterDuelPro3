using System;

namespace SevenZip.Compression.RangeCoder
{
	// Token: 0x02000192 RID: 402
	internal struct BitTreeDecoder
	{
		// Token: 0x060005B9 RID: 1465 RVA: 0x0001AF04 File Offset: 0x00019104
		public BitTreeDecoder(int numBitLevels)
		{
			this.NumBitLevels = numBitLevels;
			this.Models = new BitDecoder[1 << numBitLevels];
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0001AF20 File Offset: 0x00019120
		public void Init()
		{
			uint i = 1U;
			while ((ulong)i < (ulong)(1L << (this.NumBitLevels & 31)))
			{
				this.Models[(int)i].Init();
				i += 1U;
			}
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0001AF58 File Offset: 0x00019158
		public uint Decode(Decoder rangeDecoder)
		{
			uint i = 1U;
			for (int bitIndex = this.NumBitLevels; bitIndex > 0; bitIndex--)
			{
				i = (i << 1) + this.Models[(int)i].Decode(rangeDecoder);
			}
			return i - (1U << this.NumBitLevels);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0001AF9C File Offset: 0x0001919C
		public uint ReverseDecode(Decoder rangeDecoder)
		{
			uint i = 1U;
			uint symbol = 0U;
			for (int bitIndex = 0; bitIndex < this.NumBitLevels; bitIndex++)
			{
				uint bit = this.Models[(int)i].Decode(rangeDecoder);
				i <<= 1;
				i += bit;
				symbol |= bit << bitIndex;
			}
			return symbol;
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0001AFE4 File Offset: 0x000191E4
		public static uint ReverseDecode(BitDecoder[] Models, uint startIndex, Decoder rangeDecoder, int NumBitLevels)
		{
			uint i = 1U;
			uint symbol = 0U;
			for (int bitIndex = 0; bitIndex < NumBitLevels; bitIndex++)
			{
				uint bit = Models[(int)(startIndex + i)].Decode(rangeDecoder);
				i <<= 1;
				i += bit;
				symbol |= bit << bitIndex;
			}
			return symbol;
		}

		// Token: 0x04000A4F RID: 2639
		private BitDecoder[] Models;

		// Token: 0x04000A50 RID: 2640
		private int NumBitLevels;
	}
}
