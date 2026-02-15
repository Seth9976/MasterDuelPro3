using System;
using System.IO;
using System.Text;
using ICSharpCode.SharpZipLib.Checksum;
using ICSharpCode.SharpZipLib.Encryption;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000043 RID: 67
	public class ZipInputStream : InflaterInputStream
	{
		// Token: 0x0600020D RID: 525 RVA: 0x000098C2 File Offset: 0x00007AC2
		public ZipInputStream(Stream baseInputStream)
			: base(baseInputStream, new Inflater(true))
		{
			this.internalReader = new ZipInputStream.ReadDataHandler(this.ReadingNotAvailable);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000098F9 File Offset: 0x00007AF9
		public ZipInputStream(Stream baseInputStream, int bufferSize)
			: base(baseInputStream, new Inflater(true), bufferSize)
		{
			this.internalReader = new ZipInputStream.ReadDataHandler(this.ReadingNotAvailable);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00009934 File Offset: 0x00007B34
		public ZipInputStream(Stream baseInputStream, StringCodec stringCodec)
			: base(baseInputStream, new Inflater(true))
		{
			this.internalReader = new ZipInputStream.ReadDataHandler(this.ReadingNotAvailable);
			if (stringCodec != null)
			{
				this._stringCodec = stringCodec;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00009980 File Offset: 0x00007B80
		// (set) Token: 0x06000211 RID: 529 RVA: 0x00009988 File Offset: 0x00007B88
		public string Password
		{
			get
			{
				return this.password;
			}
			set
			{
				this.password = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00009994 File Offset: 0x00007B94
		public bool CanDecompressEntry
		{
			get
			{
				return this.entry != null && ZipInputStream.IsEntryCompressionMethodSupported(this.entry) && this.entry.CanDecompress && (!this.entry.HasFlag(GeneralBitFlags.Descriptor) || this.entry.CompressionMethod != CompressionMethod.Stored || this.entry.IsCrypted);
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000099F0 File Offset: 0x00007BF0
		private static bool IsEntryCompressionMethodSupported(ZipEntry entry)
		{
			CompressionMethod compressionMethodForHeader = entry.CompressionMethodForHeader;
			return compressionMethodForHeader == CompressionMethod.Deflated || compressionMethodForHeader == CompressionMethod.Stored;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00009A10 File Offset: 0x00007C10
		public ZipEntry GetNextEntry()
		{
			if (this.crc == null)
			{
				throw new InvalidOperationException("Closed.");
			}
			if (this.entry != null)
			{
				this.CloseEntry();
			}
			if (!this.SkipUntilNextEntry())
			{
				base.Dispose();
				return null;
			}
			short num = (short)this.inputBuffer.ReadLeShort();
			this.flags = this.inputBuffer.ReadLeShort();
			this.method = (CompressionMethod)this.inputBuffer.ReadLeShort();
			uint num2 = (uint)this.inputBuffer.ReadLeInt();
			int num3 = this.inputBuffer.ReadLeInt();
			this.csize = (long)this.inputBuffer.ReadLeInt();
			this.size = (long)this.inputBuffer.ReadLeInt();
			int num4 = this.inputBuffer.ReadLeShort();
			int num5 = this.inputBuffer.ReadLeShort();
			bool flag = (this.flags & 1) == 1;
			byte[] array = new byte[num4];
			this.inputBuffer.ReadRawBuffer(array);
			Encoding encoding = this._stringCodec.ZipInputEncoding(this.flags);
			string @string = encoding.GetString(array);
			bool flag2 = encoding.IsZipUnicode();
			this.entry = new ZipEntry(@string, (int)num, 51, this.method, flag2)
			{
				Flags = this.flags
			};
			if ((this.flags & 8) == 0)
			{
				this.entry.Crc = (long)num3 & (long)((ulong)(-1));
				this.entry.Size = this.size & (long)((ulong)(-1));
				this.entry.CompressedSize = this.csize & (long)((ulong)(-1));
				this.entry.CryptoCheckValue = (byte)((num3 >> 24) & 255);
			}
			else
			{
				if (num3 != 0)
				{
					this.entry.Crc = (long)num3 & (long)((ulong)(-1));
				}
				if (this.size != 0L)
				{
					this.entry.Size = this.size & (long)((ulong)(-1));
				}
				if (this.csize != 0L)
				{
					this.entry.CompressedSize = this.csize & (long)((ulong)(-1));
				}
				this.entry.CryptoCheckValue = (byte)((num2 >> 8) & 255U);
			}
			this.entry.DosTime = (long)((ulong)num2);
			if (num5 > 0)
			{
				byte[] array2 = new byte[num5];
				this.inputBuffer.ReadRawBuffer(array2);
				this.entry.ExtraData = array2;
			}
			this.entry.ProcessExtraData(true);
			if (this.entry.CompressedSize >= 0L)
			{
				this.csize = this.entry.CompressedSize;
			}
			if (this.entry.Size >= 0L)
			{
				this.size = this.entry.Size;
			}
			if (this.method == CompressionMethod.Stored && ((!flag && this.csize != this.size) || (flag && this.csize - 12L != this.size)))
			{
				throw new ZipException("Stored, but compressed != uncompressed");
			}
			if (ZipInputStream.IsEntryCompressionMethodSupported(this.entry))
			{
				this.internalReader = new ZipInputStream.ReadDataHandler(this.InitialRead);
			}
			else
			{
				this.internalReader = new ZipInputStream.ReadDataHandler(this.ReadingNotSupported);
			}
			return this.entry;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00009CE8 File Offset: 0x00007EE8
		private bool SkipUntilNextEntry()
		{
			int num = 0;
			while (this.inputBuffer.ReadLeByte() == 0)
			{
				num++;
			}
			this.inputBuffer.Available++;
			int num2 = 0;
			uint num3 = (uint)this.inputBuffer.ReadLeInt();
			for (;;)
			{
				if (num3 <= 84233040U)
				{
					if (num3 == 33639248U)
					{
						break;
					}
					if (num3 == 67324752U)
					{
						return true;
					}
					if (num3 == 84233040U)
					{
						break;
					}
				}
				else if (num3 == 101010256U || num3 == 101075792U || num3 == 117853008U)
				{
					break;
				}
				num3 = (uint)(((int)this.inputBuffer.ReadLeByte() << 24) | (int)(num3 >> 8));
				num2++;
			}
			return false;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00009D88 File Offset: 0x00007F88
		private void ReadDataDescriptor()
		{
			if (this.inputBuffer.ReadLeInt() != 134695760)
			{
				throw new ZipException("Data descriptor signature not found");
			}
			this.entry.Crc = (long)this.inputBuffer.ReadLeInt() & (long)((ulong)(-1));
			if (this.entry.LocalHeaderRequiresZip64)
			{
				this.csize = this.inputBuffer.ReadLeLong();
				this.size = this.inputBuffer.ReadLeLong();
			}
			else
			{
				this.csize = (long)this.inputBuffer.ReadLeInt();
				this.size = (long)this.inputBuffer.ReadLeInt();
			}
			this.entry.CompressedSize = this.csize;
			this.entry.Size = this.size;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00009E44 File Offset: 0x00008044
		private void CompleteCloseEntry(bool testCrc)
		{
			base.StopDecrypting();
			if ((this.flags & 8) != 0)
			{
				this.ReadDataDescriptor();
			}
			this.size = 0L;
			if (testCrc && (this.crc.Value & (long)((ulong)(-1))) != this.entry.Crc && this.entry.Crc != -1L)
			{
				throw new ZipException("CRC mismatch");
			}
			this.crc.Reset();
			if (this.method == CompressionMethod.Deflated)
			{
				this.inf.Reset();
			}
			this.entry = null;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00009ED0 File Offset: 0x000080D0
		public void CloseEntry()
		{
			if (this.crc == null)
			{
				throw new InvalidOperationException("Closed");
			}
			if (this.entry == null)
			{
				return;
			}
			if (this.method == CompressionMethod.Deflated)
			{
				if ((this.flags & 8) != 0)
				{
					byte[] array = new byte[4096];
					while (this.Read(array, 0, array.Length) > 0)
					{
					}
					return;
				}
				this.csize -= this.inf.TotalIn;
				this.inputBuffer.Available += this.inf.RemainingInput;
			}
			if ((long)this.inputBuffer.Available > this.csize && this.csize >= 0L)
			{
				this.inputBuffer.Available = (int)((long)this.inputBuffer.Available - this.csize);
			}
			else
			{
				this.csize -= (long)this.inputBuffer.Available;
				this.inputBuffer.Available = 0;
				while (this.csize != 0L)
				{
					long num = base.Skip(this.csize);
					if (num <= 0L)
					{
						throw new ZipException("Zip archive ends early.");
					}
					this.csize -= num;
				}
			}
			this.CompleteCloseEntry(false);
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00009FFB File Offset: 0x000081FB
		public override int Available
		{
			get
			{
				if (this.entry == null)
				{
					return 0;
				}
				return 1;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000A008 File Offset: 0x00008208
		public override long Length
		{
			get
			{
				if (this.entry == null)
				{
					throw new InvalidOperationException("No current entry");
				}
				if (this.entry.Size >= 0L)
				{
					return this.entry.Size;
				}
				throw new ZipException("Length not available for the current entry");
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000A044 File Offset: 0x00008244
		public override int ReadByte()
		{
			byte[] array = new byte[1];
			if (this.Read(array, 0, 1) <= 0)
			{
				return -1;
			}
			return (int)(array[0] & byte.MaxValue);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000A06F File Offset: 0x0000826F
		private int ReadingNotAvailable(byte[] destination, int offset, int count)
		{
			throw new InvalidOperationException("Unable to read from this stream");
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000A07B File Offset: 0x0000827B
		private int ReadingNotSupported(byte[] destination, int offset, int count)
		{
			throw new ZipException("The compression method for this entry is not supported");
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000A087 File Offset: 0x00008287
		private int StoredDescriptorEntry(byte[] destination, int offset, int count)
		{
			throw new StreamUnsupportedException("The combination of Stored compression method and Descriptor flag is not possible to read using ZipInputStream");
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000A094 File Offset: 0x00008294
		private int InitialRead(byte[] destination, int offset, int count)
		{
			bool flag = (this.entry.Flags & 8) != 0;
			if (this.entry.IsCrypted)
			{
				if (this.password == null)
				{
					throw new ZipException("No password set.");
				}
				PkzipClassicManaged pkzipClassicManaged = new PkzipClassicManaged();
				byte[] array = PkzipClassic.GenerateKeys(this._stringCodec.ZipCryptoEncoding.GetBytes(this.password));
				this.inputBuffer.CryptoTransform = pkzipClassicManaged.CreateDecryptor(array, null);
				byte[] array2 = new byte[12];
				this.inputBuffer.ReadClearTextBuffer(array2, 0, 12);
				if (array2[11] != this.entry.CryptoCheckValue)
				{
					throw new ZipException("Invalid password");
				}
				if (this.csize >= 12L)
				{
					this.csize -= 12L;
				}
				else if (!flag)
				{
					throw new ZipException(string.Format("Entry compressed size {0} too small for encryption", this.csize));
				}
			}
			else
			{
				this.inputBuffer.CryptoTransform = null;
			}
			if (this.csize <= 0L && !flag)
			{
				this.internalReader = new ZipInputStream.ReadDataHandler(this.ReadingNotAvailable);
				return 0;
			}
			if (this.method == CompressionMethod.Deflated && this.inputBuffer.Available > 0)
			{
				this.inputBuffer.SetInflaterInput(this.inf);
			}
			if (!this.entry.IsCrypted && this.method == CompressionMethod.Stored && flag)
			{
				this.internalReader = new ZipInputStream.ReadDataHandler(this.StoredDescriptorEntry);
				return this.StoredDescriptorEntry(destination, offset, count);
			}
			if (!this.CanDecompressEntry)
			{
				this.internalReader = new ZipInputStream.ReadDataHandler(this.ReadingNotSupported);
				return this.ReadingNotSupported(destination, offset, count);
			}
			this.internalReader = new ZipInputStream.ReadDataHandler(this.BodyRead);
			return this.BodyRead(destination, offset, count);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000A24C File Offset: 0x0000844C
		public override int Read(byte[] buffer, int offset, int count)
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
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("Invalid offset/count combination");
			}
			return this.internalReader(buffer, offset, count);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000A2B0 File Offset: 0x000084B0
		private int BodyRead(byte[] buffer, int offset, int count)
		{
			if (this.crc == null)
			{
				throw new InvalidOperationException("Closed");
			}
			if (this.entry == null || count <= 0)
			{
				return 0;
			}
			if (offset + count > buffer.Length)
			{
				throw new ArgumentException("Offset + count exceeds buffer size");
			}
			bool flag = false;
			CompressionMethod compressionMethod = this.method;
			if (compressionMethod != CompressionMethod.Stored)
			{
				if (compressionMethod == CompressionMethod.Deflated)
				{
					count = base.Read(buffer, offset, count);
					if (count <= 0)
					{
						if (!this.inf.IsFinished)
						{
							throw new ZipException("Inflater not finished!");
						}
						this.inputBuffer.Available = this.inf.RemainingInput;
						if ((this.flags & 8) == 0 && ((this.inf.TotalIn != this.csize && this.csize != (long)((ulong)(-1)) && this.csize != -1L) || this.inf.TotalOut != this.size))
						{
							throw new ZipException(string.Concat(new string[]
							{
								"Size mismatch: ",
								this.csize.ToString(),
								";",
								this.size.ToString(),
								" <-> ",
								this.inf.TotalIn.ToString(),
								";",
								this.inf.TotalOut.ToString()
							}));
						}
						this.inf.Reset();
						flag = true;
					}
				}
			}
			else
			{
				if ((long)count > this.csize && this.csize >= 0L)
				{
					count = (int)this.csize;
				}
				if (count > 0)
				{
					count = this.inputBuffer.ReadClearTextBuffer(buffer, offset, count);
					if (count > 0)
					{
						this.csize -= (long)count;
						this.size -= (long)count;
					}
				}
				if (this.csize == 0L)
				{
					flag = true;
				}
				else if (count < 0)
				{
					throw new ZipException("EOF in stored block");
				}
			}
			if (count > 0)
			{
				this.crc.Update(new ArraySegment<byte>(buffer, offset, count));
			}
			if (flag)
			{
				this.CompleteCloseEntry(true);
			}
			return count;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000A4A6 File Offset: 0x000086A6
		protected override void Dispose(bool disposing)
		{
			this.internalReader = new ZipInputStream.ReadDataHandler(this.ReadingNotAvailable);
			this.crc = null;
			this.entry = null;
			base.Dispose(disposing);
		}

		// Token: 0x04000146 RID: 326
		private ZipInputStream.ReadDataHandler internalReader;

		// Token: 0x04000147 RID: 327
		private Crc32 crc = new Crc32();

		// Token: 0x04000148 RID: 328
		private ZipEntry entry;

		// Token: 0x04000149 RID: 329
		private long size;

		// Token: 0x0400014A RID: 330
		private CompressionMethod method;

		// Token: 0x0400014B RID: 331
		private int flags;

		// Token: 0x0400014C RID: 332
		private string password;

		// Token: 0x0400014D RID: 333
		private readonly StringCodec _stringCodec = ZipStrings.GetStringCodec();

		// Token: 0x02000044 RID: 68
		// (Invoke) Token: 0x06000224 RID: 548
		private delegate int ReadDataHandler(byte[] b, int offset, int length);
	}
}
