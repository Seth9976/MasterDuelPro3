using System;
using K4os.Compression.LZ4.Internal;

namespace K4os.Compression.LZ4.Encoders
{
	// Token: 0x02000022 RID: 34
	public class LZ4BlockDecoder : UnmanagedResources, ILZ4Decoder, IDisposable
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00005684 File Offset: 0x00003884
		public int BlockSize
		{
			get
			{
				return this._blockSize;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000090 RID: 144 RVA: 0x0000568C File Offset: 0x0000388C
		public int BytesReady
		{
			get
			{
				return this._outputIndex;
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00005694 File Offset: 0x00003894
		public unsafe LZ4BlockDecoder(int blockSize)
		{
			blockSize = Mem.RoundUp(Math.Max(blockSize, 1024), 1024);
			this._blockSize = blockSize;
			this._outputLength = this._blockSize + 8;
			this._outputIndex = 0;
			this._outputBuffer = (byte*)Mem.Alloc(this._outputLength + 8);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000056F0 File Offset: 0x000038F0
		public unsafe int Decode(byte* source, int length, int blockSize = 0)
		{
			base.ThrowIfDisposed();
			if (blockSize <= 0)
			{
				blockSize = this._blockSize;
			}
			if (blockSize > this._blockSize)
			{
				throw new InvalidOperationException();
			}
			int num = LZ4Codec.Decode(source, length, this._outputBuffer, this._outputLength);
			if (num < 0)
			{
				throw new InvalidOperationException();
			}
			this._outputIndex = num;
			return this._outputIndex;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000574C File Offset: 0x0000394C
		public unsafe int Inject(byte* source, int length)
		{
			base.ThrowIfDisposed();
			if (length <= 0)
			{
				return this._outputIndex = 0;
			}
			if (length > this._outputLength)
			{
				throw new InvalidOperationException();
			}
			Mem.Move(this._outputBuffer, source, length);
			this._outputIndex = length;
			return length;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00005792 File Offset: 0x00003992
		public unsafe void Drain(byte* target, int offset, int length)
		{
			base.ThrowIfDisposed();
			offset = this._outputIndex + offset;
			if (offset < 0 || length < 0 || offset + length > this._outputIndex)
			{
				throw new InvalidOperationException();
			}
			Mem.Move(target, this._outputBuffer + offset, length);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000057CC File Offset: 0x000039CC
		protected unsafe override void ReleaseUnmanaged()
		{
			base.ReleaseUnmanaged();
			Mem.Free((void*)this._outputBuffer);
		}

		// Token: 0x0400008E RID: 142
		private readonly int _blockSize;

		// Token: 0x0400008F RID: 143
		private readonly int _outputLength;

		// Token: 0x04000090 RID: 144
		private int _outputIndex;

		// Token: 0x04000091 RID: 145
		private unsafe readonly byte* _outputBuffer;
	}
}
