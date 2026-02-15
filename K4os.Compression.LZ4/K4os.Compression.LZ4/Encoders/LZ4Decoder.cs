using System;

namespace K4os.Compression.LZ4.Encoders
{
	// Token: 0x02000025 RID: 37
	public static class LZ4Decoder
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x00005AC4 File Offset: 0x00003CC4
		public static ILZ4Decoder Create(bool chaining, int blockSize, int extraBlocks = 0)
		{
			if (chaining)
			{
				return LZ4Decoder.CreateChainDecoder(blockSize, extraBlocks);
			}
			return LZ4Decoder.CreateBlockDecoder(blockSize);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00005AD7 File Offset: 0x00003CD7
		private static ILZ4Decoder CreateChainDecoder(int blockSize, int extraBlocks)
		{
			return new LZ4ChainDecoder(blockSize, extraBlocks);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00005AE0 File Offset: 0x00003CE0
		private static ILZ4Decoder CreateBlockDecoder(int blockSize)
		{
			return new LZ4BlockDecoder(blockSize);
		}
	}
}
