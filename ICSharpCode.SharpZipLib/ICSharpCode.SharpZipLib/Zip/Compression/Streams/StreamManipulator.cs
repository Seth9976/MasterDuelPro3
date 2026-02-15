using System;

namespace ICSharpCode.SharpZipLib.Zip.Compression.Streams
{
	// Token: 0x02000069 RID: 105
	public class StreamManipulator
	{
		// Token: 0x0600036A RID: 874 RVA: 0x00011404 File Offset: 0x0000F604
		public int PeekBits(int bitCount)
		{
			if (this.bitsInBuffer_ < bitCount)
			{
				if (this.windowStart_ == this.windowEnd_)
				{
					return -1;
				}
				uint num = this.buffer_;
				byte[] array = this.window_;
				int num2 = this.windowStart_;
				this.windowStart_ = num2 + 1;
				uint num3 = array[num2] & 255U;
				byte[] array2 = this.window_;
				num2 = this.windowStart_;
				this.windowStart_ = num2 + 1;
				this.buffer_ = num | ((num3 | ((array2[num2] & 255U) << 8)) << this.bitsInBuffer_);
				this.bitsInBuffer_ += 16;
			}
			return (int)((ulong)this.buffer_ & (ulong)((long)((1 << bitCount) - 1)));
		}

		// Token: 0x0600036B RID: 875 RVA: 0x000114A4 File Offset: 0x0000F6A4
		public bool TryGetBits(int bitCount, ref int output, int outputOffset = 0)
		{
			int num = this.PeekBits(bitCount);
			if (num < 0)
			{
				return false;
			}
			output = num + outputOffset;
			this.DropBits(bitCount);
			return true;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x000114CC File Offset: 0x0000F6CC
		public bool TryGetBits(int bitCount, ref byte[] array, int index)
		{
			int num = this.PeekBits(bitCount);
			if (num < 0)
			{
				return false;
			}
			array[index] = (byte)num;
			this.DropBits(bitCount);
			return true;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x000114F5 File Offset: 0x0000F6F5
		public void DropBits(int bitCount)
		{
			this.buffer_ >>= bitCount;
			this.bitsInBuffer_ -= bitCount;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00011516 File Offset: 0x0000F716
		public int GetBits(int bitCount)
		{
			int num = this.PeekBits(bitCount);
			if (num >= 0)
			{
				this.DropBits(bitCount);
			}
			return num;
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0001152A File Offset: 0x0000F72A
		public int AvailableBits
		{
			get
			{
				return this.bitsInBuffer_;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000370 RID: 880 RVA: 0x00011532 File Offset: 0x0000F732
		public int AvailableBytes
		{
			get
			{
				return this.windowEnd_ - this.windowStart_ + (this.bitsInBuffer_ >> 3);
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0001154A File Offset: 0x0000F74A
		public void SkipToByteBoundary()
		{
			this.buffer_ >>= this.bitsInBuffer_ & 7;
			this.bitsInBuffer_ &= -8;
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000372 RID: 882 RVA: 0x00011573 File Offset: 0x0000F773
		public bool IsNeedingInput
		{
			get
			{
				return this.windowStart_ == this.windowEnd_;
			}
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00011584 File Offset: 0x0000F784
		public int CopyBytes(byte[] output, int offset, int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			if ((this.bitsInBuffer_ & 7) != 0)
			{
				throw new InvalidOperationException("Bit buffer is not byte aligned!");
			}
			int num = 0;
			while (this.bitsInBuffer_ > 0 && length > 0)
			{
				output[offset++] = (byte)this.buffer_;
				this.buffer_ >>= 8;
				this.bitsInBuffer_ -= 8;
				length--;
				num++;
			}
			if (length == 0)
			{
				return num;
			}
			int num2 = this.windowEnd_ - this.windowStart_;
			if (length > num2)
			{
				length = num2;
			}
			Array.Copy(this.window_, this.windowStart_, output, offset, length);
			this.windowStart_ += length;
			if (((this.windowStart_ - this.windowEnd_) & 1) != 0)
			{
				byte[] array = this.window_;
				int num3 = this.windowStart_;
				this.windowStart_ = num3 + 1;
				this.buffer_ = array[num3] & 255U;
				this.bitsInBuffer_ = 8;
			}
			return num + length;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00011678 File Offset: 0x0000F878
		public void Reset()
		{
			this.buffer_ = 0U;
			this.windowStart_ = (this.windowEnd_ = (this.bitsInBuffer_ = 0));
		}

		// Token: 0x06000375 RID: 885 RVA: 0x000116A8 File Offset: 0x0000F8A8
		public void SetInput(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Cannot be negative");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Cannot be negative");
			}
			if (this.windowStart_ < this.windowEnd_)
			{
				throw new InvalidOperationException("Old input was not completely processed");
			}
			int num = offset + count;
			if (offset > num || num > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if ((count & 1) != 0)
			{
				this.buffer_ |= (uint)((uint)(buffer[offset++] & byte.MaxValue) << this.bitsInBuffer_);
				this.bitsInBuffer_ += 8;
			}
			this.window_ = buffer;
			this.windowStart_ = offset;
			this.windowEnd_ = num;
		}

		// Token: 0x0400027D RID: 637
		private byte[] window_;

		// Token: 0x0400027E RID: 638
		private int windowStart_;

		// Token: 0x0400027F RID: 639
		private int windowEnd_;

		// Token: 0x04000280 RID: 640
		private uint buffer_;

		// Token: 0x04000281 RID: 641
		private int bitsInBuffer_;
	}
}
