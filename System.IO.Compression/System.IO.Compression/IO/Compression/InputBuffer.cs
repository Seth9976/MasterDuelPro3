using System;

namespace System.IO.Compression
{
	// Token: 0x02000014 RID: 20
	internal sealed class InputBuffer
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000046EC File Offset: 0x000028EC
		public int AvailableBits
		{
			get
			{
				return this._bitsInBuffer;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000046F4 File Offset: 0x000028F4
		public int AvailableBytes
		{
			get
			{
				return this._end - this._start + this._bitsInBuffer / 8;
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000470C File Offset: 0x0000290C
		public bool EnsureBitsAvailable(int count)
		{
			if (this._bitsInBuffer < count)
			{
				if (this.NeedsInput())
				{
					return false;
				}
				uint bitBuffer = this._bitBuffer;
				byte[] buffer = this._buffer;
				int num = this._start;
				this._start = num + 1;
				this._bitBuffer = bitBuffer | (buffer[num] << (this._bitsInBuffer & 31));
				this._bitsInBuffer += 8;
				if (this._bitsInBuffer < count)
				{
					if (this.NeedsInput())
					{
						return false;
					}
					uint bitBuffer2 = this._bitBuffer;
					byte[] buffer2 = this._buffer;
					num = this._start;
					this._start = num + 1;
					this._bitBuffer = bitBuffer2 | (buffer2[num] << (this._bitsInBuffer & 31));
					this._bitsInBuffer += 8;
				}
			}
			return true;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000047C0 File Offset: 0x000029C0
		public uint TryLoad16Bits()
		{
			if (this._bitsInBuffer < 8)
			{
				if (this._start < this._end)
				{
					uint bitBuffer = this._bitBuffer;
					byte[] buffer = this._buffer;
					int num = this._start;
					this._start = num + 1;
					this._bitBuffer = bitBuffer | (buffer[num] << (this._bitsInBuffer & 31));
					this._bitsInBuffer += 8;
				}
				if (this._start < this._end)
				{
					uint bitBuffer2 = this._bitBuffer;
					byte[] buffer2 = this._buffer;
					int num = this._start;
					this._start = num + 1;
					this._bitBuffer = bitBuffer2 | (buffer2[num] << (this._bitsInBuffer & 31));
					this._bitsInBuffer += 8;
				}
			}
			else if (this._bitsInBuffer < 16 && this._start < this._end)
			{
				uint bitBuffer3 = this._bitBuffer;
				byte[] buffer3 = this._buffer;
				int num = this._start;
				this._start = num + 1;
				this._bitBuffer = bitBuffer3 | (buffer3[num] << (this._bitsInBuffer & 31));
				this._bitsInBuffer += 8;
			}
			return this._bitBuffer;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000048CF File Offset: 0x00002ACF
		private uint GetBitMask(int count)
		{
			return (1U << count) - 1U;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000048D9 File Offset: 0x00002AD9
		public int GetBits(int count)
		{
			if (!this.EnsureBitsAvailable(count))
			{
				return -1;
			}
			int num = (int)(this._bitBuffer & this.GetBitMask(count));
			this._bitBuffer >>= count;
			this._bitsInBuffer -= count;
			return num;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004914 File Offset: 0x00002B14
		public int CopyTo(byte[] output, int offset, int length)
		{
			int num = 0;
			while (this._bitsInBuffer > 0 && length > 0)
			{
				output[offset++] = (byte)this._bitBuffer;
				this._bitBuffer >>= 8;
				this._bitsInBuffer -= 8;
				length--;
				num++;
			}
			if (length == 0)
			{
				return num;
			}
			int num2 = this._end - this._start;
			if (length > num2)
			{
				length = num2;
			}
			Array.Copy(this._buffer, this._start, output, offset, length);
			this._start += length;
			return num + length;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000049A5 File Offset: 0x00002BA5
		public bool NeedsInput()
		{
			return this._start == this._end;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000049B5 File Offset: 0x00002BB5
		public void SetInput(byte[] buffer, int offset, int length)
		{
			this._buffer = buffer;
			this._start = offset;
			this._end = offset + length;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000049CE File Offset: 0x00002BCE
		public void SkipBits(int n)
		{
			this._bitBuffer >>= n;
			this._bitsInBuffer -= n;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000049EF File Offset: 0x00002BEF
		public void SkipToByteBoundary()
		{
			this._bitBuffer >>= this._bitsInBuffer % 8;
			this._bitsInBuffer -= this._bitsInBuffer % 8;
		}

		// Token: 0x04000080 RID: 128
		private byte[] _buffer;

		// Token: 0x04000081 RID: 129
		private int _start;

		// Token: 0x04000082 RID: 130
		private int _end;

		// Token: 0x04000083 RID: 131
		private uint _bitBuffer;

		// Token: 0x04000084 RID: 132
		private int _bitsInBuffer;
	}
}
