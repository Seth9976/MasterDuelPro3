using System;
using System.IO;
using ICSharpCode.SharpZipLib.Checksum;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace ICSharpCode.SharpZipLib.GZip
{
	// Token: 0x02000092 RID: 146
	public class GZipInputStream : InflaterInputStream
	{
		// Token: 0x060004D9 RID: 1241 RVA: 0x00017EB8 File Offset: 0x000160B8
		public GZipInputStream(Stream baseInputStream)
			: this(baseInputStream, 4096)
		{
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00017EC6 File Offset: 0x000160C6
		public GZipInputStream(Stream baseInputStream, int size)
			: base(baseInputStream, new Inflater(true), size)
		{
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00017ED8 File Offset: 0x000160D8
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num;
			do
			{
				if (!this.readGZIPHeader)
				{
					try
					{
						if (!this.ReadHeader())
						{
							return 0;
						}
					}
					catch (Exception ex) when (this.completedLastBlock && (ex is GZipException || ex is EndOfStreamException))
					{
						return 0;
					}
				}
				num = base.Read(buffer, offset, count);
				if (num > 0)
				{
					this.crc.Update(new ArraySegment<byte>(buffer, offset, num));
				}
				if (this.inf.IsFinished)
				{
					this.ReadFooter();
				}
			}
			while (num <= 0 && count != 0);
			return num;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00017F84 File Offset: 0x00016184
		public string GetFilename()
		{
			return this.fileName;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00017F8C File Offset: 0x0001618C
		private bool ReadHeader()
		{
			this.crc = new Crc32();
			if (this.inputBuffer.Available <= 0)
			{
				this.inputBuffer.Fill();
				if (this.inputBuffer.Available <= 0)
				{
					return false;
				}
			}
			Crc32 crc = new Crc32();
			byte b = this.inputBuffer.ReadLeByte();
			crc.Update((int)b);
			if (b != 31)
			{
				throw new GZipException("Error GZIP header, first magic byte doesn't match");
			}
			b = this.inputBuffer.ReadLeByte();
			if (b != 139)
			{
				throw new GZipException("Error GZIP header,  second magic byte doesn't match");
			}
			crc.Update((int)b);
			byte b2 = this.inputBuffer.ReadLeByte();
			if (b2 != 8)
			{
				throw new GZipException("Error GZIP header, data not in deflate format");
			}
			crc.Update((int)b2);
			byte b3 = this.inputBuffer.ReadLeByte();
			crc.Update((int)b3);
			if ((b3 & 224) != 0)
			{
				throw new GZipException("Reserved flag bits in GZIP header != 0");
			}
			GZipFlags gzipFlags = (GZipFlags)b3;
			for (int i = 0; i < 6; i++)
			{
				crc.Update((int)this.inputBuffer.ReadLeByte());
			}
			if (gzipFlags.HasFlag(GZipFlags.FEXTRA))
			{
				byte b4 = this.inputBuffer.ReadLeByte();
				byte b5 = this.inputBuffer.ReadLeByte();
				crc.Update((int)b4);
				crc.Update((int)b5);
				int num = ((int)b5 << 8) | (int)b4;
				for (int j = 0; j < num; j++)
				{
					crc.Update((int)this.inputBuffer.ReadLeByte());
				}
			}
			if (gzipFlags.HasFlag(GZipFlags.FNAME))
			{
				byte[] array = new byte[1024];
				int num2 = 0;
				int num3;
				while ((num3 = (int)this.inputBuffer.ReadLeByte()) > 0)
				{
					if (num2 < 1024)
					{
						array[num2++] = (byte)num3;
					}
					crc.Update(num3);
				}
				crc.Update(num3);
				this.fileName = GZipConstants.Encoding.GetString(array, 0, num2);
			}
			else
			{
				this.fileName = null;
			}
			if (gzipFlags.HasFlag(GZipFlags.FCOMMENT))
			{
				int num4;
				while ((num4 = (int)this.inputBuffer.ReadLeByte()) > 0)
				{
					crc.Update(num4);
				}
				crc.Update(num4);
			}
			if (gzipFlags.HasFlag(GZipFlags.FHCRC))
			{
				byte b6 = this.inputBuffer.ReadLeByte();
				if (b6 < 0)
				{
					throw new EndOfStreamException("EOS reading GZIP header");
				}
				int num5 = (int)this.inputBuffer.ReadLeByte();
				if (num5 < 0)
				{
					throw new EndOfStreamException("EOS reading GZIP header");
				}
				if ((((int)b6 << 8) | num5) != ((int)crc.Value & 65535))
				{
					throw new GZipException("Header CRC value mismatch");
				}
			}
			this.readGZIPHeader = true;
			return true;
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00018218 File Offset: 0x00016418
		private void ReadFooter()
		{
			byte[] array = new byte[8];
			long num = this.inf.TotalOut & (long)((ulong)(-1));
			this.inputBuffer.Available += this.inf.RemainingInput;
			this.inf.Reset();
			int num2;
			for (int i = 8; i > 0; i -= num2)
			{
				num2 = this.inputBuffer.ReadClearTextBuffer(array, 8 - i, i);
				if (num2 <= 0)
				{
					throw new EndOfStreamException("EOS reading GZIP footer");
				}
			}
			int num3 = (int)(array[0] & byte.MaxValue) | ((int)(array[1] & byte.MaxValue) << 8) | ((int)(array[2] & byte.MaxValue) << 16) | ((int)array[3] << 24);
			if (num3 != (int)this.crc.Value)
			{
				throw new GZipException(string.Format("GZIP crc sum mismatch, theirs \"{0:x8}\" and ours \"{1:x8}\"", num3, (int)this.crc.Value));
			}
			uint num4 = (uint)((int)(array[4] & byte.MaxValue) | ((int)(array[5] & byte.MaxValue) << 8) | ((int)(array[6] & byte.MaxValue) << 16) | ((int)array[7] << 24));
			if (num != (long)((ulong)num4))
			{
				throw new GZipException("Number of bytes mismatch in footer");
			}
			this.readGZIPHeader = false;
			this.completedLastBlock = true;
		}

		// Token: 0x040003C2 RID: 962
		protected Crc32 crc;

		// Token: 0x040003C3 RID: 963
		private bool readGZIPHeader;

		// Token: 0x040003C4 RID: 964
		private bool completedLastBlock;

		// Token: 0x040003C5 RID: 965
		private string fileName;
	}
}
