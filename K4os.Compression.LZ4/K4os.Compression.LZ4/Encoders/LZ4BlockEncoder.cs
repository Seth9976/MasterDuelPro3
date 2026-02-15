using System;

namespace K4os.Compression.LZ4.Encoders
{
	// Token: 0x02000023 RID: 35
	public class LZ4BlockEncoder : LZ4EncoderBase
	{
		// Token: 0x06000096 RID: 150 RVA: 0x000057DF File Offset: 0x000039DF
		public LZ4BlockEncoder(LZ4Level level, int blockSize)
			: base(false, blockSize, 0)
		{
			this._level = level;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000057F1 File Offset: 0x000039F1
		protected unsafe override int EncodeBlock(byte* source, int sourceLength, byte* target, int targetLength)
		{
			return LZ4Codec.Encode(source, sourceLength, target, targetLength, this._level);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00005803 File Offset: 0x00003A03
		protected unsafe override int CopyDict(byte* target, int dictionaryLength)
		{
			return 0;
		}

		// Token: 0x04000092 RID: 146
		private readonly LZ4Level _level;
	}
}
