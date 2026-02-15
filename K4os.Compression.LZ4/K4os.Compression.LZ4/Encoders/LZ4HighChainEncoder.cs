using System;
using K4os.Compression.LZ4.Engine;
using K4os.Compression.LZ4.Internal;

namespace K4os.Compression.LZ4.Encoders
{
	// Token: 0x0200002A RID: 42
	public class LZ4HighChainEncoder : LZ4EncoderBase
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x00006025 File Offset: 0x00004225
		public unsafe LZ4HighChainEncoder(LZ4Level level, int blockSize, int extraBlocks = 0)
			: base(true, blockSize, extraBlocks)
		{
			if (level < LZ4Level.L03_HC)
			{
				level = LZ4Level.L03_HC;
			}
			if (level > LZ4Level.L12_MAX)
			{
				level = LZ4Level.L12_MAX;
			}
			this._context = (LZ4_64_HC.LZ4HC_CCtx_t*)Mem.AllocZero(sizeof(LZ4_64_HC.LZ4HC_CCtx_t));
			LZ4_64_HC.LZ4_resetStreamHC(this._context, (int)level);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000605D File Offset: 0x0000425D
		protected unsafe override void ReleaseUnmanaged()
		{
			base.ReleaseUnmanaged();
			Mem.Free((void*)this._context);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00006070 File Offset: 0x00004270
		protected unsafe override int EncodeBlock(byte* source, int sourceLength, byte* target, int targetLength)
		{
			return LZ4_64_HC.LZ4_compress_HC_continue(this._context, source, target, sourceLength, targetLength);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00006082 File Offset: 0x00004282
		protected unsafe override int CopyDict(byte* target, int length)
		{
			return LZ4_64_HC.LZ4_saveDictHC(this._context, target, length);
		}

		// Token: 0x0400009E RID: 158
		private unsafe readonly LZ4_64_HC.LZ4HC_CCtx_t* _context;
	}
}
