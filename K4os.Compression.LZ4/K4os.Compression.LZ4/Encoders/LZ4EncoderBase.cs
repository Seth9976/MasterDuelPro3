using System;
using K4os.Compression.LZ4.Internal;

namespace K4os.Compression.LZ4.Encoders
{
	// Token: 0x02000027 RID: 39
	public abstract class LZ4EncoderBase : UnmanagedResources, ILZ4Encoder, IDisposable
	{
		// Token: 0x060000AB RID: 171 RVA: 0x00005B24 File Offset: 0x00003D24
		protected unsafe LZ4EncoderBase(bool chaining, int blockSize, int extraBlocks)
		{
			blockSize = Mem.RoundUp(Math.Max(blockSize, 1024), 1024);
			extraBlocks = Math.Max(extraBlocks, 0);
			int num = (chaining ? 65536 : 0);
			this._blockSize = blockSize;
			this._inputLength = num + (1 + extraBlocks) * blockSize + 8;
			this._inputIndex = (this._inputPointer = 0);
			this._inputBuffer = (byte*)Mem.Alloc(this._inputLength + 8);
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00005B9C File Offset: 0x00003D9C
		public int BlockSize
		{
			get
			{
				return this._blockSize;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00005BA4 File Offset: 0x00003DA4
		public int BytesReady
		{
			get
			{
				return this._inputPointer - this._inputIndex;
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00005BB4 File Offset: 0x00003DB4
		public unsafe int Topup(byte* source, int length)
		{
			base.ThrowIfDisposed();
			if (length == 0)
			{
				return 0;
			}
			int num = this._inputIndex + this._blockSize - this._inputPointer;
			if (num <= 0)
			{
				return 0;
			}
			int num2 = Math.Min(num, length);
			Mem.Move(this._inputBuffer + this._inputPointer, source, num2);
			this._inputPointer += num2;
			return num2;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00005C14 File Offset: 0x00003E14
		public unsafe int Encode(byte* target, int length, bool allowCopy)
		{
			base.ThrowIfDisposed();
			int num = this._inputPointer - this._inputIndex;
			if (num <= 0)
			{
				return 0;
			}
			int num2 = this.EncodeBlock(this._inputBuffer + this._inputIndex, num, target, length);
			if (num2 <= 0)
			{
				throw new InvalidOperationException("Failed to encode chunk. Target buffer too small.");
			}
			if (allowCopy && num2 >= num)
			{
				Mem.Move(target, this._inputBuffer + this._inputIndex, num);
				num2 = -num;
			}
			this.Commit();
			return num2;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00005C88 File Offset: 0x00003E88
		private void Commit()
		{
			this._inputIndex = this._inputPointer;
			if (this._inputIndex + this._blockSize <= this._inputLength)
			{
				return;
			}
			this._inputIndex = (this._inputPointer = this.CopyDict(this._inputBuffer, this._inputPointer));
		}

		// Token: 0x060000B1 RID: 177
		protected unsafe abstract int EncodeBlock(byte* source, int sourceLength, byte* target, int targetLength);

		// Token: 0x060000B2 RID: 178
		protected unsafe abstract int CopyDict(byte* target, int dictionaryLength);

		// Token: 0x060000B3 RID: 179 RVA: 0x00005CD8 File Offset: 0x00003ED8
		protected unsafe override void ReleaseUnmanaged()
		{
			base.ReleaseUnmanaged();
			Mem.Free((void*)this._inputBuffer);
		}

		// Token: 0x04000098 RID: 152
		private unsafe readonly byte* _inputBuffer;

		// Token: 0x04000099 RID: 153
		private readonly int _inputLength;

		// Token: 0x0400009A RID: 154
		private readonly int _blockSize;

		// Token: 0x0400009B RID: 155
		private int _inputIndex;

		// Token: 0x0400009C RID: 156
		private int _inputPointer;
	}
}
