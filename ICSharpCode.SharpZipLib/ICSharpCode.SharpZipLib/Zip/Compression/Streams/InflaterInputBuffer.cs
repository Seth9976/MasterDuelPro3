using System;
using System.IO;
using System.Security.Cryptography;

namespace ICSharpCode.SharpZipLib.Zip.Compression.Streams
{
	// Token: 0x02000066 RID: 102
	public class InflaterInputBuffer
	{
		// Token: 0x06000338 RID: 824 RVA: 0x00010A7E File Offset: 0x0000EC7E
		public InflaterInputBuffer(Stream stream)
			: this(stream, 4096)
		{
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00010A8C File Offset: 0x0000EC8C
		public InflaterInputBuffer(Stream stream, int bufferSize)
		{
			this.inputStream = stream;
			if (bufferSize < 1024)
			{
				bufferSize = 1024;
			}
			this.rawData = new byte[bufferSize];
			this.clearText = this.rawData;
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00010AC2 File Offset: 0x0000ECC2
		public int RawLength
		{
			get
			{
				return this.rawLength;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600033B RID: 827 RVA: 0x00010ACA File Offset: 0x0000ECCA
		public byte[] RawData
		{
			get
			{
				return this.rawData;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00010AD2 File Offset: 0x0000ECD2
		public int ClearTextLength
		{
			get
			{
				return this.clearTextLength;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00010ADA File Offset: 0x0000ECDA
		public byte[] ClearText
		{
			get
			{
				return this.clearText;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600033E RID: 830 RVA: 0x00010AE2 File Offset: 0x0000ECE2
		// (set) Token: 0x0600033F RID: 831 RVA: 0x00010AEA File Offset: 0x0000ECEA
		public int Available
		{
			get
			{
				return this.available;
			}
			set
			{
				this.available = value;
			}
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00010AF3 File Offset: 0x0000ECF3
		public void SetInflaterInput(Inflater inflater)
		{
			if (this.available > 0)
			{
				inflater.SetInput(this.clearText, this.clearTextLength - this.available, this.available);
				this.available = 0;
			}
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00010B24 File Offset: 0x0000ED24
		public void Fill()
		{
			this.rawLength = 0;
			int num = this.rawData.Length;
			while (num > 0 && this.inputStream.CanRead)
			{
				int num2 = this.inputStream.Read(this.rawData, this.rawLength, num);
				if (num2 <= 0)
				{
					break;
				}
				this.rawLength += num2;
				num -= num2;
			}
			if (this.cryptoTransform != null)
			{
				this.clearTextLength = this.cryptoTransform.TransformBlock(this.rawData, 0, this.rawLength, this.clearText, 0);
			}
			else
			{
				this.clearTextLength = this.rawLength;
			}
			this.available = this.clearTextLength;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00010BCA File Offset: 0x0000EDCA
		public int ReadRawBuffer(byte[] buffer)
		{
			return this.ReadRawBuffer(buffer, 0, buffer.Length);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00010BD8 File Offset: 0x0000EDD8
		public int ReadRawBuffer(byte[] outBuffer, int offset, int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			int num = offset;
			int i = length;
			while (i > 0)
			{
				if (this.available <= 0)
				{
					this.Fill();
					if (this.available <= 0)
					{
						return 0;
					}
				}
				int num2 = Math.Min(i, this.available);
				Array.Copy(this.rawData, this.rawLength - this.available, outBuffer, num, num2);
				num += num2;
				i -= num2;
				this.available -= num2;
			}
			return length;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00010C58 File Offset: 0x0000EE58
		public int ReadClearTextBuffer(byte[] outBuffer, int offset, int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			int num = offset;
			int i = length;
			while (i > 0)
			{
				if (this.available <= 0)
				{
					this.Fill();
					if (this.available <= 0)
					{
						return 0;
					}
				}
				int num2 = Math.Min(i, this.available);
				Array.Copy(this.clearText, this.clearTextLength - this.available, outBuffer, num, num2);
				num += num2;
				i -= num2;
				this.available -= num2;
			}
			return length;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00010CD8 File Offset: 0x0000EED8
		public byte ReadLeByte()
		{
			if (this.available <= 0)
			{
				this.Fill();
				if (this.available <= 0)
				{
					throw new ZipException("EOF in header");
				}
			}
			byte b = this.rawData[this.rawLength - this.available];
			this.available--;
			return b;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00010D2A File Offset: 0x0000EF2A
		public int ReadLeShort()
		{
			return (int)this.ReadLeByte() | ((int)this.ReadLeByte() << 8);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00010D3B File Offset: 0x0000EF3B
		public int ReadLeInt()
		{
			return this.ReadLeShort() | (this.ReadLeShort() << 16);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00010D4D File Offset: 0x0000EF4D
		public long ReadLeLong()
		{
			return (long)((ulong)this.ReadLeInt() | (ulong)((ulong)((long)this.ReadLeInt()) << 32));
		}

		// Token: 0x170000BC RID: 188
		// (set) Token: 0x06000349 RID: 841 RVA: 0x00010D64 File Offset: 0x0000EF64
		public ICryptoTransform CryptoTransform
		{
			set
			{
				this.cryptoTransform = value;
				if (this.cryptoTransform != null)
				{
					if (this.rawData == this.clearText)
					{
						if (this.internalClearText == null)
						{
							this.internalClearText = new byte[this.rawData.Length];
						}
						this.clearText = this.internalClearText;
					}
					this.clearTextLength = this.rawLength;
					if (this.available > 0)
					{
						this.cryptoTransform.TransformBlock(this.rawData, this.rawLength - this.available, this.available, this.clearText, this.rawLength - this.available);
						return;
					}
				}
				else
				{
					this.clearText = this.rawData;
					this.clearTextLength = this.rawLength;
				}
			}
		}

		// Token: 0x0400026A RID: 618
		private int rawLength;

		// Token: 0x0400026B RID: 619
		private byte[] rawData;

		// Token: 0x0400026C RID: 620
		private int clearTextLength;

		// Token: 0x0400026D RID: 621
		private byte[] clearText;

		// Token: 0x0400026E RID: 622
		private byte[] internalClearText;

		// Token: 0x0400026F RID: 623
		private int available;

		// Token: 0x04000270 RID: 624
		private ICryptoTransform cryptoTransform;

		// Token: 0x04000271 RID: 625
		private Stream inputStream;
	}
}
