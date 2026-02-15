using System;

namespace SevenZip.Compression.RangeCoder
{
	// Token: 0x0200018F RID: 399
	internal struct BitEncoder
	{
		// Token: 0x060005A7 RID: 1447 RVA: 0x0001AA68 File Offset: 0x00018C68
		public void Init()
		{
			this.Prob = 1024U;
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0001AA75 File Offset: 0x00018C75
		public void UpdateModel(uint symbol)
		{
			if (symbol == 0U)
			{
				this.Prob += 2048U - this.Prob >> 5;
				return;
			}
			this.Prob -= this.Prob >> 5;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0001AAAC File Offset: 0x00018CAC
		public void Encode(Encoder encoder, uint symbol)
		{
			uint newBound = (encoder.Range >> 11) * this.Prob;
			if (symbol == 0U)
			{
				encoder.Range = newBound;
				this.Prob += 2048U - this.Prob >> 5;
			}
			else
			{
				encoder.Low += (ulong)newBound;
				encoder.Range -= newBound;
				this.Prob -= this.Prob >> 5;
			}
			if (encoder.Range < 16777216U)
			{
				encoder.Range <<= 8;
				encoder.ShiftLow();
			}
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0001AB44 File Offset: 0x00018D44
		static BitEncoder()
		{
			for (int i = 8; i >= 0; i--)
			{
				uint num = 1U << 9 - i - 1;
				uint end = 1U << 9 - i;
				for (uint j = num; j < end; j += 1U)
				{
					BitEncoder.ProbPrices[(int)j] = (uint)((i << 6) + (int)(end - j << 6 >> 9 - i - 1));
				}
			}
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0001ABA6 File Offset: 0x00018DA6
		public uint GetPrice(uint symbol)
		{
			checked
			{
				return BitEncoder.ProbPrices[(int)((IntPtr)((unchecked((ulong)(this.Prob - symbol) ^ (ulong)((long)(-(long)symbol))) & 2047UL) >> 2))];
			}
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0001ABC5 File Offset: 0x00018DC5
		public uint GetPrice0()
		{
			return BitEncoder.ProbPrices[(int)(this.Prob >> 2)];
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0001ABD5 File Offset: 0x00018DD5
		public uint GetPrice1()
		{
			return BitEncoder.ProbPrices[(int)(2048U - this.Prob >> 2)];
		}

		// Token: 0x04000A42 RID: 2626
		public const int kNumBitModelTotalBits = 11;

		// Token: 0x04000A43 RID: 2627
		public const uint kBitModelTotal = 2048U;

		// Token: 0x04000A44 RID: 2628
		private const int kNumMoveBits = 5;

		// Token: 0x04000A45 RID: 2629
		private const int kNumMoveReducingBits = 2;

		// Token: 0x04000A46 RID: 2630
		public const int kNumBitPriceShiftBits = 6;

		// Token: 0x04000A47 RID: 2631
		private uint Prob;

		// Token: 0x04000A48 RID: 2632
		private static uint[] ProbPrices = new uint[512];
	}
}
