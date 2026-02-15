using System;

namespace SevenZip.Compression.RangeCoder
{
	// Token: 0x02000190 RID: 400
	internal struct BitDecoder
	{
		// Token: 0x060005AE RID: 1454 RVA: 0x0001ABEB File Offset: 0x00018DEB
		public void UpdateModel(int numMoveBits, uint symbol)
		{
			if (symbol == 0U)
			{
				this.Prob += 2048U - this.Prob >> numMoveBits;
				return;
			}
			this.Prob -= this.Prob >> numMoveBits;
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0001AC27 File Offset: 0x00018E27
		public void Init()
		{
			this.Prob = 1024U;
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0001AC34 File Offset: 0x00018E34
		public uint Decode(Decoder rangeDecoder)
		{
			uint newBound = (rangeDecoder.Range >> 11) * this.Prob;
			if (rangeDecoder.Code < newBound)
			{
				rangeDecoder.Range = newBound;
				this.Prob += 2048U - this.Prob >> 5;
				if (rangeDecoder.Range < 16777216U)
				{
					rangeDecoder.Code = (rangeDecoder.Code << 8) | (uint)((byte)rangeDecoder.Stream.ReadByte());
					rangeDecoder.Range <<= 8;
				}
				return 0U;
			}
			rangeDecoder.Range -= newBound;
			rangeDecoder.Code -= newBound;
			this.Prob -= this.Prob >> 5;
			if (rangeDecoder.Range < 16777216U)
			{
				rangeDecoder.Code = (rangeDecoder.Code << 8) | (uint)((byte)rangeDecoder.Stream.ReadByte());
				rangeDecoder.Range <<= 8;
			}
			return 1U;
		}

		// Token: 0x04000A49 RID: 2633
		public const int kNumBitModelTotalBits = 11;

		// Token: 0x04000A4A RID: 2634
		public const uint kBitModelTotal = 2048U;

		// Token: 0x04000A4B RID: 2635
		private const int kNumMoveBits = 5;

		// Token: 0x04000A4C RID: 2636
		private uint Prob;
	}
}
