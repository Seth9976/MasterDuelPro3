using System;

namespace K4os.Compression.LZ4.Encoders
{
	// Token: 0x02000026 RID: 38
	public static class LZ4Encoder
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x00005AE8 File Offset: 0x00003CE8
		public static ILZ4Encoder Create(bool chaining, LZ4Level level, int blockSize, int extraBlocks = 0)
		{
			if (!chaining)
			{
				return LZ4Encoder.CreateBlockEncoder(level, blockSize);
			}
			if (level != LZ4Level.L00_FAST)
			{
				return LZ4Encoder.CreateHighEncoder(level, blockSize, extraBlocks);
			}
			return LZ4Encoder.CreateFastEncoder(blockSize, extraBlocks);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00005B08 File Offset: 0x00003D08
		private static ILZ4Encoder CreateBlockEncoder(LZ4Level level, int blockSize)
		{
			return new LZ4BlockEncoder(level, blockSize);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00005B11 File Offset: 0x00003D11
		private static ILZ4Encoder CreateFastEncoder(int blockSize, int extraBlocks)
		{
			return new LZ4FastChainEncoder(blockSize, extraBlocks);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00005B1A File Offset: 0x00003D1A
		private static ILZ4Encoder CreateHighEncoder(LZ4Level level, int blockSize, int extraBlocks)
		{
			return new LZ4HighChainEncoder(level, blockSize, extraBlocks);
		}
	}
}
