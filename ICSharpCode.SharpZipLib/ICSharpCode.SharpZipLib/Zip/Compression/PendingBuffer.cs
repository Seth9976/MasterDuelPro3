using System;

namespace ICSharpCode.SharpZipLib.Zip.Compression
{
	// Token: 0x02000060 RID: 96
	public class PendingBuffer
	{
		// Token: 0x06000304 RID: 772 RVA: 0x0000FC1D File Offset: 0x0000DE1D
		public PendingBuffer()
			: this(4096)
		{
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000FC2A File Offset: 0x0000DE2A
		public PendingBuffer(int bufferSize)
		{
			this.buffer = new byte[bufferSize];
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000FC40 File Offset: 0x0000DE40
		public void Reset()
		{
			this.start = (this.end = (this.bitCount = 0));
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000FC68 File Offset: 0x0000DE68
		public void WriteByte(int value)
		{
			byte[] array = this.buffer;
			int num = this.end;
			this.end = num + 1;
			array[num] = (byte)value;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000FC90 File Offset: 0x0000DE90
		public void WriteShort(int value)
		{
			byte[] array = this.buffer;
			int num = this.end;
			this.end = num + 1;
			array[num] = (byte)value;
			byte[] array2 = this.buffer;
			num = this.end;
			this.end = num + 1;
			array2[num] = (byte)(value >> 8);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000FCD4 File Offset: 0x0000DED4
		public void WriteInt(int value)
		{
			byte[] array = this.buffer;
			int num = this.end;
			this.end = num + 1;
			array[num] = (byte)value;
			byte[] array2 = this.buffer;
			num = this.end;
			this.end = num + 1;
			array2[num] = (byte)(value >> 8);
			byte[] array3 = this.buffer;
			num = this.end;
			this.end = num + 1;
			array3[num] = (byte)(value >> 16);
			byte[] array4 = this.buffer;
			num = this.end;
			this.end = num + 1;
			array4[num] = (byte)(value >> 24);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000FD51 File Offset: 0x0000DF51
		public void WriteBlock(byte[] block, int offset, int length)
		{
			Array.Copy(block, offset, this.buffer, this.end, length);
			this.end += length;
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000FD75 File Offset: 0x0000DF75
		public int BitCount
		{
			get
			{
				return this.bitCount;
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000FD80 File Offset: 0x0000DF80
		public void AlignToByte()
		{
			if (this.bitCount > 0)
			{
				byte[] array = this.buffer;
				int num = this.end;
				this.end = num + 1;
				array[num] = (byte)this.bits;
				if (this.bitCount > 8)
				{
					byte[] array2 = this.buffer;
					num = this.end;
					this.end = num + 1;
					array2[num] = (byte)(this.bits >> 8);
				}
			}
			this.bits = 0U;
			this.bitCount = 0;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000FDF0 File Offset: 0x0000DFF0
		public void WriteBits(int b, int count)
		{
			this.bits |= (uint)((uint)b << this.bitCount);
			this.bitCount += count;
			if (this.bitCount >= 16)
			{
				byte[] array = this.buffer;
				int num = this.end;
				this.end = num + 1;
				array[num] = (byte)this.bits;
				byte[] array2 = this.buffer;
				num = this.end;
				this.end = num + 1;
				array2[num] = (byte)(this.bits >> 8);
				this.bits >>= 16;
				this.bitCount -= 16;
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000FE8C File Offset: 0x0000E08C
		public void WriteShortMSB(int s)
		{
			byte[] array = this.buffer;
			int num = this.end;
			this.end = num + 1;
			array[num] = (byte)(s >> 8);
			byte[] array2 = this.buffer;
			num = this.end;
			this.end = num + 1;
			array2[num] = (byte)s;
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600030F RID: 783 RVA: 0x0000FECF File Offset: 0x0000E0CF
		public bool IsFlushed
		{
			get
			{
				return this.end == 0;
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000FEDC File Offset: 0x0000E0DC
		public int Flush(byte[] output, int offset, int length)
		{
			if (this.bitCount >= 8)
			{
				byte[] array = this.buffer;
				int num = this.end;
				this.end = num + 1;
				array[num] = (byte)this.bits;
				this.bits >>= 8;
				this.bitCount -= 8;
			}
			if (length > this.end - this.start)
			{
				length = this.end - this.start;
				Array.Copy(this.buffer, this.start, output, offset, length);
				this.start = 0;
				this.end = 0;
			}
			else
			{
				Array.Copy(this.buffer, this.start, output, offset, length);
				this.start += length;
			}
			return length;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000FF94 File Offset: 0x0000E194
		public byte[] ToByteArray()
		{
			this.AlignToByte();
			byte[] array = new byte[this.end - this.start];
			Array.Copy(this.buffer, this.start, array, 0, array.Length);
			this.start = 0;
			this.end = 0;
			return array;
		}

		// Token: 0x04000245 RID: 581
		private readonly byte[] buffer;

		// Token: 0x04000246 RID: 582
		private int start;

		// Token: 0x04000247 RID: 583
		private int end;

		// Token: 0x04000248 RID: 584
		private uint bits;

		// Token: 0x04000249 RID: 585
		private int bitCount;
	}
}
