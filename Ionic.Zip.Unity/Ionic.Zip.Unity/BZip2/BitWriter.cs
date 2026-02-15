using System;
using System.IO;

namespace Ionic.BZip2
{
	// Token: 0x02000042 RID: 66
	internal class BitWriter
	{
		// Token: 0x06000334 RID: 820 RVA: 0x000111D8 File Offset: 0x0000F3D8
		public BitWriter(Stream s)
		{
			this.output = s;
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000335 RID: 821 RVA: 0x000111E7 File Offset: 0x0000F3E7
		public byte RemainingBits
		{
			get
			{
				return (byte)((this.accumulator >> 32 - this.nAccumulatedBits) & 255U);
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000336 RID: 822 RVA: 0x00011203 File Offset: 0x0000F403
		public int NumRemainingBits
		{
			get
			{
				return this.nAccumulatedBits;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0001120B File Offset: 0x0000F40B
		public int TotalBytesWrittenOut
		{
			get
			{
				return this.totalBytesWrittenOut;
			}
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00011213 File Offset: 0x0000F413
		public void Reset()
		{
			this.accumulator = 0U;
			this.nAccumulatedBits = 0;
			this.totalBytesWrittenOut = 0;
			this.output.Seek(0L, 0);
			this.output.SetLength(0L);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00011248 File Offset: 0x0000F448
		public void WriteBits(int nbits, uint value)
		{
			int i = this.nAccumulatedBits;
			uint num = this.accumulator;
			while (i >= 8)
			{
				this.output.WriteByte((byte)((num >> 24) & 255U));
				this.totalBytesWrittenOut++;
				num <<= 8;
				i -= 8;
			}
			this.accumulator = num | (value << 32 - i - nbits);
			this.nAccumulatedBits = i + nbits;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x000112B1 File Offset: 0x0000F4B1
		public void WriteByte(byte b)
		{
			this.WriteBits(8, (uint)b);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x000112BC File Offset: 0x0000F4BC
		public void WriteInt(uint u)
		{
			this.WriteBits(8, (u >> 24) & 255U);
			this.WriteBits(8, (u >> 16) & 255U);
			this.WriteBits(8, (u >> 8) & 255U);
			this.WriteBits(8, u & 255U);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00011309 File Offset: 0x0000F509
		public void Flush()
		{
			this.WriteBits(0, 0U);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00011314 File Offset: 0x0000F514
		public void FinishAndPad()
		{
			this.Flush();
			if (this.NumRemainingBits > 0)
			{
				byte b = (byte)((this.accumulator >> 24) & 255U);
				this.output.WriteByte(b);
				this.totalBytesWrittenOut++;
			}
		}

		// Token: 0x040001AC RID: 428
		private uint accumulator;

		// Token: 0x040001AD RID: 429
		private int nAccumulatedBits;

		// Token: 0x040001AE RID: 430
		private Stream output;

		// Token: 0x040001AF RID: 431
		private int totalBytesWrittenOut;
	}
}
