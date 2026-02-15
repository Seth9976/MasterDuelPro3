using System;

namespace SevenZip.Compression.RangeCoder
{
	// Token: 0x0200000E RID: 14
	internal struct BitDecoder
	{
		// Token: 0x0600002D RID: 45 RVA: 0x000028B4 File Offset: 0x00000AB4
		public void UpdateModel(int numMoveBits, uint symbol)
		{
			bool flag = symbol == 0U;
			if (flag)
			{
				this.Prob += 2048U - this.Prob >> numMoveBits;
			}
			else
			{
				this.Prob -= this.Prob >> numMoveBits;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002902 File Offset: 0x00000B02
		public void Init()
		{
			this.Prob = 1024U;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002910 File Offset: 0x00000B10
		public uint Decode(Decoder rangeDecoder)
		{
			uint num = (rangeDecoder.Range >> 11) * this.Prob;
			bool flag = rangeDecoder.Code < num;
			uint num2;
			if (flag)
			{
				rangeDecoder.Range = num;
				this.Prob += 2048U - this.Prob >> 5;
				bool flag2 = rangeDecoder.Range < 16777216U;
				if (flag2)
				{
					rangeDecoder.Code = (rangeDecoder.Code << 8) | (uint)((byte)rangeDecoder.Stream.ReadByte());
					rangeDecoder.Range <<= 8;
				}
				num2 = 0U;
			}
			else
			{
				rangeDecoder.Range -= num;
				rangeDecoder.Code -= num;
				this.Prob -= this.Prob >> 5;
				bool flag3 = rangeDecoder.Range < 16777216U;
				if (flag3)
				{
					rangeDecoder.Code = (rangeDecoder.Code << 8) | (uint)((byte)rangeDecoder.Stream.ReadByte());
					rangeDecoder.Range <<= 8;
				}
				num2 = 1U;
			}
			return num2;
		}

		// Token: 0x04000025 RID: 37
		public const int kNumBitModelTotalBits = 11;

		// Token: 0x04000026 RID: 38
		public const uint kBitModelTotal = 2048U;

		// Token: 0x04000027 RID: 39
		private const int kNumMoveBits = 5;

		// Token: 0x04000028 RID: 40
		private uint Prob;
	}
}
