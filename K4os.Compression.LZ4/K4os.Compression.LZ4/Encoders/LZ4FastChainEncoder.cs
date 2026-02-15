using System;
using K4os.Compression.LZ4.Engine;
using K4os.Compression.LZ4.Internal;

namespace K4os.Compression.LZ4.Encoders
{
	// Token: 0x02000029 RID: 41
	public class LZ4FastChainEncoder : LZ4EncoderBase
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x00005FD4 File Offset: 0x000041D4
		public unsafe LZ4FastChainEncoder(int blockSize, int extraBlocks = 0)
			: base(true, blockSize, extraBlocks)
		{
			this._context = (LZ4_xx.LZ4_stream_t*)Mem.AllocZero(sizeof(LZ4_xx.LZ4_stream_t));
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00005FF0 File Offset: 0x000041F0
		protected unsafe override void ReleaseUnmanaged()
		{
			base.ReleaseUnmanaged();
			Mem.Free((void*)this._context);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00006003 File Offset: 0x00004203
		protected unsafe override int EncodeBlock(byte* source, int sourceLength, byte* target, int targetLength)
		{
			return LZ4_64.LZ4_compress_fast_continue(this._context, source, target, sourceLength, targetLength, 1);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00006016 File Offset: 0x00004216
		protected unsafe override int CopyDict(byte* target, int length)
		{
			return LZ4_xx.LZ4_saveDict(this._context, target, length);
		}

		// Token: 0x0400009D RID: 157
		private unsafe readonly LZ4_xx.LZ4_stream_t* _context;
	}
}
