using System;
using K4os.Compression.LZ4.Engine;
using K4os.Compression.LZ4.Internal;

namespace K4os.Compression.LZ4.Encoders
{
	// Token: 0x02000024 RID: 36
	public class LZ4ChainDecoder : UnmanagedResources, ILZ4Decoder, IDisposable
	{
		// Token: 0x06000099 RID: 153 RVA: 0x00005808 File Offset: 0x00003A08
		public unsafe LZ4ChainDecoder(int blockSize, int extraBlocks)
		{
			blockSize = Mem.RoundUp(Math.Max(blockSize, 1024), 1024);
			extraBlocks = Math.Max(extraBlocks, 0);
			this._blockSize = blockSize;
			this._outputLength = 65536 + (1 + extraBlocks) * this._blockSize + 8;
			this._outputIndex = 0;
			this._outputBuffer = (byte*)Mem.Alloc(this._outputLength + 8);
			this._context = (LZ4_xx.LZ4_streamDecode_t*)Mem.AllocZero(sizeof(LZ4_xx.LZ4_streamDecode_t));
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00005885 File Offset: 0x00003A85
		public int BlockSize
		{
			get
			{
				return this._blockSize;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600009B RID: 155 RVA: 0x0000588D File Offset: 0x00003A8D
		public int BytesReady
		{
			get
			{
				return this._outputIndex;
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00005898 File Offset: 0x00003A98
		public unsafe int Decode(byte* source, int length, int blockSize)
		{
			if (blockSize <= 0)
			{
				blockSize = this._blockSize;
			}
			this.Prepare(blockSize);
			int num = this.DecodeBlock(source, length, this._outputBuffer + this._outputIndex, blockSize);
			if (num < 0)
			{
				throw new InvalidOperationException();
			}
			this._outputIndex += num;
			return num;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000058E8 File Offset: 0x00003AE8
		public unsafe int Inject(byte* source, int length)
		{
			if (length <= 0)
			{
				return 0;
			}
			if (length > Math.Max(this._blockSize, 65536))
			{
				throw new InvalidOperationException();
			}
			if (this._outputIndex + length < this._outputLength)
			{
				Mem.Move(this._outputBuffer + this._outputIndex, source, length);
				this._outputIndex = this.ApplyDict(this._outputIndex + length);
			}
			else if (length >= 65536)
			{
				Mem.Move(this._outputBuffer, source, length);
				this._outputIndex = this.ApplyDict(length);
			}
			else
			{
				int num = Math.Min(65536 - length, this._outputIndex);
				Mem.Move(this._outputBuffer, this._outputBuffer + this._outputIndex - num, num);
				Mem.Move(this._outputBuffer + num, source, length);
				this._outputIndex = this.ApplyDict(num + length);
			}
			return length;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000059BF File Offset: 0x00003BBF
		public unsafe void Drain(byte* target, int offset, int length)
		{
			offset = this._outputIndex + offset;
			if (offset < 0 || length < 0 || offset + length > this._outputIndex)
			{
				throw new InvalidOperationException();
			}
			Mem.Move(target, this._outputBuffer + offset, length);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000059F3 File Offset: 0x00003BF3
		private void Prepare(int blockSize)
		{
			if (this._outputIndex + blockSize <= this._outputLength)
			{
				return;
			}
			this._outputIndex = this.CopyDict(this._outputIndex);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00005A18 File Offset: 0x00003C18
		private int CopyDict(int index)
		{
			int num = Math.Max(index - 65536, 0);
			int num2 = index - num;
			Mem.Move(this._outputBuffer, this._outputBuffer + num, num2);
			LZ4_xx.LZ4_setStreamDecode(this._context, this._outputBuffer, num2);
			return num2;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00005A60 File Offset: 0x00003C60
		private int ApplyDict(int index)
		{
			int num = Math.Max(index - 65536, 0);
			int num2 = index - num;
			LZ4_xx.LZ4_setStreamDecode(this._context, this._outputBuffer + num, num2);
			return index;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00005A94 File Offset: 0x00003C94
		private unsafe int DecodeBlock(byte* source, int sourceLength, byte* target, int targetLength)
		{
			return LZ4_xx.LZ4_decompress_safe_continue(this._context, source, target, sourceLength, targetLength);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00005AA6 File Offset: 0x00003CA6
		protected unsafe override void ReleaseUnmanaged()
		{
			base.ReleaseUnmanaged();
			Mem.Free((void*)this._context);
			Mem.Free((void*)this._outputBuffer);
		}

		// Token: 0x04000093 RID: 147
		private unsafe readonly LZ4_xx.LZ4_streamDecode_t* _context;

		// Token: 0x04000094 RID: 148
		private readonly int _blockSize;

		// Token: 0x04000095 RID: 149
		private unsafe readonly byte* _outputBuffer;

		// Token: 0x04000096 RID: 150
		private readonly int _outputLength;

		// Token: 0x04000097 RID: 151
		private int _outputIndex;
	}
}
