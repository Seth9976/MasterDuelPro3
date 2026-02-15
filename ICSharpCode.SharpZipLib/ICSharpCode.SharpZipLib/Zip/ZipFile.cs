using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.Checksum;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Encryption;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000029 RID: 41
	public class ZipFile : IEnumerable, IDisposable
	{
		// Token: 0x0600011E RID: 286 RVA: 0x00004E10 File Offset: 0x00003010
		private void OnKeysRequired(string fileName)
		{
			if (this.KeysRequired != null)
			{
				KeysRequiredEventArgs keysRequiredEventArgs = new KeysRequiredEventArgs(fileName, this.key);
				this.KeysRequired(this, keysRequiredEventArgs);
				this.key = keysRequiredEventArgs.Key;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00004E4B File Offset: 0x0000304B
		// (set) Token: 0x06000120 RID: 288 RVA: 0x00004E53 File Offset: 0x00003053
		private byte[] Key
		{
			get
			{
				return this.key;
			}
			set
			{
				this.key = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (set) Token: 0x06000121 RID: 289 RVA: 0x00004E5C File Offset: 0x0000305C
		public string Password
		{
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.key = null;
				}
				else
				{
					this.key = PkzipClassic.GenerateKeys(this.ZipCryptoEncoding.GetBytes(value));
				}
				this.rawPassword_ = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00004E8D File Offset: 0x0000308D
		private bool HaveKeys
		{
			get
			{
				return this.key != null;
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00004E98 File Offset: 0x00003098
		public ZipFile(string name, StringCodec stringCodec = null)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.name_ = name;
			this.baseStream_ = File.Open(name, FileMode.Open, FileAccess.Read, FileShare.Read);
			this.isStreamOwner = true;
			if (stringCodec != null)
			{
				this._stringCodec = stringCodec;
			}
			try
			{
				this.ReadEntries();
			}
			catch
			{
				this.DisposeInternal(true);
				throw;
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00004F38 File Offset: 0x00003138
		public ZipFile(FileStream file)
			: this(file, false)
		{
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00004F44 File Offset: 0x00003144
		public ZipFile(FileStream file, bool leaveOpen)
		{
			if (file == null)
			{
				throw new ArgumentNullException("file");
			}
			if (!file.CanSeek)
			{
				throw new ArgumentException("Stream is not seekable", "file");
			}
			this.baseStream_ = file;
			this.name_ = file.Name;
			this.isStreamOwner = !leaveOpen;
			try
			{
				this.ReadEntries();
			}
			catch
			{
				this.DisposeInternal(true);
				throw;
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00004FF0 File Offset: 0x000031F0
		public ZipFile(Stream stream)
			: this(stream, false, null)
		{
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00004FFC File Offset: 0x000031FC
		public ZipFile(Stream stream, bool leaveOpen, StringCodec stringCodec = null)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanSeek)
			{
				throw new ArgumentException("Stream is not seekable", "stream");
			}
			this.baseStream_ = stream;
			this.isStreamOwner = !leaveOpen;
			if (stringCodec != null)
			{
				this._stringCodec = stringCodec;
			}
			if (this.baseStream_.Length > 0L)
			{
				try
				{
					this.ReadEntries();
					return;
				}
				catch
				{
					this.DisposeInternal(true);
					throw;
				}
			}
			this.entries_ = Empty.Array<ZipEntry>();
			this.isNewArchive_ = true;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000050C8 File Offset: 0x000032C8
		internal ZipFile()
		{
			this.entries_ = Empty.Array<ZipEntry>();
			this.isNewArchive_ = true;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005120 File Offset: 0x00003320
		~ZipFile()
		{
			this.Dispose(false);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00005150 File Offset: 0x00003350
		public void Close()
		{
			this.DisposeInternal(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00005160 File Offset: 0x00003360
		public static ZipFile Create(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			FileStream fileStream = File.Create(fileName);
			return new ZipFile
			{
				name_ = fileName,
				baseStream_ = fileStream,
				isStreamOwner = true
			};
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000519C File Offset: 0x0000339C
		public static ZipFile Create(Stream outStream)
		{
			if (outStream == null)
			{
				throw new ArgumentNullException("outStream");
			}
			if (!outStream.CanWrite)
			{
				throw new ArgumentException("Stream is not writeable", "outStream");
			}
			if (!outStream.CanSeek)
			{
				throw new ArgumentException("Stream is not seekable", "outStream");
			}
			return new ZipFile
			{
				baseStream_ = outStream
			};
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600012D RID: 301 RVA: 0x000051F3 File Offset: 0x000033F3
		// (set) Token: 0x0600012E RID: 302 RVA: 0x000051FB File Offset: 0x000033FB
		public bool IsStreamOwner
		{
			get
			{
				return this.isStreamOwner;
			}
			set
			{
				this.isStreamOwner = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00005204 File Offset: 0x00003404
		public bool IsEmbeddedArchive
		{
			get
			{
				return this.offsetOfFirstEntry > 0L;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00005210 File Offset: 0x00003410
		public bool IsNewArchive
		{
			get
			{
				return this.isNewArchive_;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00005218 File Offset: 0x00003418
		public string ZipFileComment
		{
			get
			{
				return this.comment_;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00005220 File Offset: 0x00003420
		public string Name
		{
			get
			{
				return this.name_;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00005228 File Offset: 0x00003428
		[Obsolete("Use the Count property instead")]
		public int Size
		{
			get
			{
				return this.entries_.Length;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00005232 File Offset: 0x00003432
		public long Count
		{
			get
			{
				return (long)this.entries_.Length;
			}
		}

		// Token: 0x1700005D RID: 93
		[IndexerName("EntryByIndex")]
		public ZipEntry this[int index]
		{
			get
			{
				return (ZipEntry)this.entries_[index].Clone();
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00005251 File Offset: 0x00003451
		// (set) Token: 0x06000137 RID: 311 RVA: 0x0000525E File Offset: 0x0000345E
		public Encoding ZipCryptoEncoding
		{
			get
			{
				return this._stringCodec.ZipCryptoEncoding;
			}
			set
			{
				this._stringCodec = this._stringCodec.WithZipCryptoEncoding(value);
			}
		}

		// Token: 0x1700005F RID: 95
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00005272 File Offset: 0x00003472
		public StringCodec StringCodec
		{
			set
			{
				this._stringCodec = value;
				if (!this.isNewArchive_)
				{
					this.ReadEntries();
				}
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005289 File Offset: 0x00003489
		public IEnumerator GetEnumerator()
		{
			if (this.isDisposed_)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			return new ZipFile.ZipEntryEnumerator(this.entries_);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000052AC File Offset: 0x000034AC
		public int FindEntry(string name, bool ignoreCase)
		{
			if (this.isDisposed_)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			for (int i = 0; i < this.entries_.Length; i++)
			{
				if (string.Compare(name, this.entries_[i].Name, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00005300 File Offset: 0x00003500
		public ZipEntry GetEntry(string name)
		{
			if (this.isDisposed_)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			int num = this.FindEntry(name, true);
			if (num < 0)
			{
				return null;
			}
			return (ZipEntry)this.entries_[num].Clone();
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00005344 File Offset: 0x00003544
		public Stream GetInputStream(ZipEntry entry)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			if (this.isDisposed_)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			long num = entry.ZipFileIndex;
			if (num < 0L || num >= (long)this.entries_.Length || this.entries_[(int)(checked((IntPtr)num))].Name != entry.Name)
			{
				num = (long)this.FindEntry(entry.Name, true);
				if (num < 0L)
				{
					throw new ZipException("Entry cannot be found");
				}
			}
			return this.GetInputStream(num);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000053CC File Offset: 0x000035CC
		public Stream GetInputStream(long entryIndex)
		{
			if (this.isDisposed_)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			checked
			{
				long num = this.LocateEntry(this.entries_[(int)((IntPtr)entryIndex)]);
				CompressionMethod compressionMethod = this.entries_[(int)((IntPtr)entryIndex)].CompressionMethod;
				Stream stream = new ZipFile.PartialInputStream(this, num, this.entries_[(int)((IntPtr)entryIndex)].CompressedSize);
				if (this.entries_[(int)((IntPtr)entryIndex)].IsCrypted)
				{
					stream = this.CreateAndInitDecryptionStream(stream, this.entries_[(int)((IntPtr)entryIndex)]);
					if (stream == null)
					{
						throw new ZipException("Unable to decrypt this entry");
					}
				}
				if (compressionMethod != CompressionMethod.Stored)
				{
					if (compressionMethod != CompressionMethod.Deflated)
					{
						if (compressionMethod != CompressionMethod.BZip2)
						{
							throw new ZipException("Unsupported compression method " + compressionMethod.ToString());
						}
						stream = new BZip2InputStream(stream);
					}
					else
					{
						stream = new InflaterInputStream(stream, new Inflater(true));
					}
				}
				return stream;
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00005494 File Offset: 0x00003694
		public bool TestArchive(bool testData)
		{
			return this.TestArchive(testData, TestStrategy.FindFirstError, null);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000054A0 File Offset: 0x000036A0
		public bool TestArchive(bool testData, TestStrategy strategy, ZipTestResultHandler resultHandler)
		{
			if (this.isDisposed_)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			TestStatus testStatus = new TestStatus(this);
			if (resultHandler != null)
			{
				resultHandler(testStatus, null);
			}
			ZipFile.HeaderTest headerTest = (testData ? (ZipFile.HeaderTest.Extract | ZipFile.HeaderTest.Header) : ZipFile.HeaderTest.Header);
			bool flag = true;
			try
			{
				int num = 0;
				while (flag && (long)num < this.Count)
				{
					if (resultHandler != null)
					{
						testStatus.SetEntry(this[num]);
						testStatus.SetOperation(TestOperation.EntryHeader);
						resultHandler(testStatus, null);
					}
					try
					{
						this.TestLocalHeader(this[num], headerTest);
					}
					catch (ZipException ex)
					{
						testStatus.AddError();
						if (resultHandler != null)
						{
							resultHandler(testStatus, "Exception during test - '" + ex.Message + "'");
						}
						flag &= strategy > TestStrategy.FindFirstError;
					}
					if (flag && testData && this[num].IsFile)
					{
						bool flag2 = this[num].AESKeySize == 0;
						if (resultHandler != null)
						{
							testStatus.SetOperation(TestOperation.EntryData);
							resultHandler(testStatus, null);
						}
						Crc32 crc = new Crc32();
						using (Stream inputStream = this.GetInputStream(this[num]))
						{
							byte[] array = new byte[4096];
							long num2 = 0L;
							int num3;
							while ((num3 = inputStream.Read(array, 0, array.Length)) > 0)
							{
								if (flag2)
								{
									crc.Update(new ArraySegment<byte>(array, 0, num3));
								}
								if (resultHandler != null)
								{
									num2 += (long)num3;
									testStatus.SetBytesTested(num2);
									resultHandler(testStatus, null);
								}
							}
						}
						if (flag2 && this[num].Crc != crc.Value)
						{
							testStatus.AddError();
							if (resultHandler != null)
							{
								resultHandler(testStatus, "CRC mismatch");
							}
							flag &= strategy > TestStrategy.FindFirstError;
						}
						if ((this[num].Flags & 8) != 0)
						{
							DescriptorData descriptorData = new DescriptorData();
							ZipFormat.ReadDataDescriptor(this.baseStream_, this[num].LocalHeaderRequiresZip64, descriptorData);
							if (flag2 && this[num].Crc != descriptorData.Crc)
							{
								testStatus.AddError();
								if (resultHandler != null)
								{
									resultHandler(testStatus, "Descriptor CRC mismatch");
								}
							}
							if (this[num].CompressedSize != descriptorData.CompressedSize)
							{
								testStatus.AddError();
								if (resultHandler != null)
								{
									resultHandler(testStatus, "Descriptor compressed size mismatch");
								}
							}
							if (this[num].Size != descriptorData.Size)
							{
								testStatus.AddError();
								if (resultHandler != null)
								{
									resultHandler(testStatus, "Descriptor size mismatch");
								}
							}
						}
					}
					if (resultHandler != null)
					{
						testStatus.SetOperation(TestOperation.EntryComplete);
						resultHandler(testStatus, null);
					}
					num++;
				}
				if (resultHandler != null)
				{
					testStatus.SetOperation(TestOperation.MiscellaneousTests);
					resultHandler(testStatus, null);
				}
			}
			catch (Exception ex2)
			{
				testStatus.AddError();
				if (resultHandler != null)
				{
					resultHandler(testStatus, "Exception during test - '" + ex2.Message + "'");
				}
			}
			if (resultHandler != null)
			{
				testStatus.SetOperation(TestOperation.Complete);
				testStatus.SetEntry(null);
				resultHandler(testStatus, null);
			}
			return testStatus.ErrorCount == 0;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000057B8 File Offset: 0x000039B8
		private long TestLocalHeader(ZipEntry entry, ZipFile.HeaderTest tests)
		{
			Stream stream = this.baseStream_;
			long num12;
			lock (stream)
			{
				bool flag2 = (tests & ZipFile.HeaderTest.Header) > ZipFile.HeaderTest.None;
				bool flag3 = (tests & ZipFile.HeaderTest.Extract) > ZipFile.HeaderTest.None;
				long num = this.offsetOfFirstEntry + entry.Offset;
				this.baseStream_.Seek(num, SeekOrigin.Begin);
				int num2 = (int)this.ReadLEUint();
				if (num2 != 67324752)
				{
					throw new ZipException(string.Format("Wrong local header signature at 0x{0:x}, expected 0x{1:x8}, actual 0x{2:x8}", num, 67324752, num2));
				}
				short num3 = (short)(this.ReadLEUshort() & 255);
				GeneralBitFlags generalBitFlags = (GeneralBitFlags)this.ReadLEUshort();
				CompressionMethod compressionMethod = (CompressionMethod)this.ReadLEUshort();
				short num4 = (short)this.ReadLEUshort();
				short num5 = (short)this.ReadLEUshort();
				uint num6 = this.ReadLEUint();
				long num7 = (long)((ulong)this.ReadLEUint());
				long num8 = (long)((ulong)this.ReadLEUint());
				int num9 = (int)this.ReadLEUshort();
				int num10 = (int)this.ReadLEUshort();
				byte[] array = new byte[num9];
				StreamUtils.ReadFully(this.baseStream_, array);
				byte[] array2 = new byte[num10];
				StreamUtils.ReadFully(this.baseStream_, array2);
				ZipExtraData zipExtraData = new ZipExtraData(array2);
				if (zipExtraData.Find(1))
				{
					num8 = zipExtraData.ReadLong();
					num7 = zipExtraData.ReadLong();
					if (generalBitFlags.HasAny(GeneralBitFlags.Descriptor))
					{
						if (num8 != 0L && num8 != entry.Size)
						{
							throw new ZipException("Size invalid for descriptor");
						}
						if (num7 != 0L && num7 != entry.CompressedSize)
						{
							throw new ZipException("Compressed size invalid for descriptor");
						}
					}
				}
				else if (num3 >= 45 && ((uint)num8 == 4294967295U || (uint)num7 == 4294967295U))
				{
					throw new ZipException("Required Zip64 extended information missing");
				}
				if (flag3 && entry.IsFile)
				{
					if (!entry.IsCompressionMethodSupported())
					{
						throw new ZipException("Compression method not supported");
					}
					if (num3 > 51 || (num3 > 20 && num3 < 45))
					{
						throw new ZipException(string.Format("Version required to extract this entry not supported ({0})", num3));
					}
					if (generalBitFlags.HasAny(GeneralBitFlags.Patched | GeneralBitFlags.StrongEncryption | GeneralBitFlags.EnhancedCompress | GeneralBitFlags.HeaderMasked))
					{
						throw new ZipException(string.Format("The library does not support the zip features required to extract this entry ({0:F})", generalBitFlags & (GeneralBitFlags.Patched | GeneralBitFlags.StrongEncryption | GeneralBitFlags.EnhancedCompress | GeneralBitFlags.HeaderMasked)));
					}
				}
				if (flag2)
				{
					if (num3 <= 63 && num3 != 10 && num3 != 11 && num3 != 20 && num3 != 21 && num3 != 25 && num3 != 27 && num3 != 45 && num3 != 46 && num3 != 50 && num3 != 51 && num3 != 52 && num3 != 61 && num3 != 62 && num3 != 63)
					{
						throw new ZipException(string.Format("Version required to extract this entry is invalid ({0})", num3));
					}
					Encoding encoding = this._stringCodec.ZipInputEncoding(generalBitFlags);
					if (generalBitFlags.HasAny(GeneralBitFlags.ReservedPKware4 | GeneralBitFlags.ReservedPkware14 | GeneralBitFlags.ReservedPkware15))
					{
						throw new ZipException("Reserved bit flags cannot be set.");
					}
					if (generalBitFlags.HasAny(GeneralBitFlags.Encrypted) && num3 < 20)
					{
						throw new ZipException(string.Format("Version required to extract this entry is too low for encryption ({0})", num3));
					}
					if (generalBitFlags.HasAny(GeneralBitFlags.StrongEncryption))
					{
						if (!generalBitFlags.HasAny(GeneralBitFlags.Encrypted))
						{
							throw new ZipException("Strong encryption flag set but encryption flag is not set");
						}
						if (num3 < 50)
						{
							throw new ZipException(string.Format("Version required to extract this entry is too low for encryption ({0})", num3));
						}
					}
					if (generalBitFlags.HasAny(GeneralBitFlags.Patched) && num3 < 27)
					{
						throw new ZipException(string.Format("Patched data requires higher version than ({0})", num3));
					}
					if (generalBitFlags != (GeneralBitFlags)entry.Flags)
					{
						throw new ZipException(string.Format("Central header/local header flags mismatch ({0:F} vs {1:F})", (GeneralBitFlags)entry.Flags, generalBitFlags));
					}
					if (entry.CompressionMethodForHeader != compressionMethod)
					{
						throw new ZipException(string.Format("Central header/local header compression method mismatch ({0:G} vs {1:G})", entry.CompressionMethodForHeader, compressionMethod));
					}
					if (entry.Version != (int)num3)
					{
						throw new ZipException("Extract version mismatch");
					}
					if (generalBitFlags.HasAny(GeneralBitFlags.StrongEncryption) && num3 < 62)
					{
						throw new ZipException("Strong encryption flag set but version not high enough");
					}
					if (generalBitFlags.HasAny(GeneralBitFlags.HeaderMasked) && (num4 != 0 || num5 != 0))
					{
						throw new ZipException("Header masked set but date/time values non-zero");
					}
					if (!generalBitFlags.HasAny(GeneralBitFlags.Descriptor) && num6 != (uint)entry.Crc)
					{
						throw new ZipException("Central header/local header crc mismatch");
					}
					if (num8 == 0L && num7 == 0L && num6 != 0U)
					{
						throw new ZipException("Invalid CRC for empty entry");
					}
					if (entry.Name.Length > num9)
					{
						throw new ZipException("File name length mismatch");
					}
					string @string = encoding.GetString(array);
					if (@string != entry.Name)
					{
						throw new ZipException("Central header and local header file name mismatch");
					}
					if (entry.IsDirectory)
					{
						if (num8 > 0L)
						{
							throw new ZipException("Directory cannot have size");
						}
						if (entry.IsCrypted)
						{
							if (num7 > (long)(entry.EncryptionOverheadSize + 2))
							{
								throw new ZipException("Directory compressed size invalid");
							}
						}
						else if (num7 > 2L)
						{
							throw new ZipException("Directory compressed size invalid");
						}
					}
					if (!ZipNameTransform.IsValidName(@string, true))
					{
						throw new ZipException("Name is invalid");
					}
				}
				if (!generalBitFlags.HasAny(GeneralBitFlags.Descriptor) || ((num8 > 0L || num7 > 0L) && entry.Size > 0L))
				{
					if (num8 != 0L && num8 != entry.Size)
					{
						throw new ZipException(string.Format("Size mismatch between central header ({0}) and local header ({1})", entry.Size, num8));
					}
					if (num7 != 0L && num7 != entry.CompressedSize && num7 != (long)((ulong)(-1)) && num7 != -1L)
					{
						throw new ZipException(string.Format("Compressed size mismatch between central header({0}) and local header({1})", entry.CompressedSize, num7));
					}
				}
				int num11 = num9 + num10;
				num12 = this.offsetOfFirstEntry + entry.Offset + 30L + (long)num11;
			}
			return num12;
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00005D20 File Offset: 0x00003F20
		// (set) Token: 0x06000142 RID: 322 RVA: 0x00005D2D File Offset: 0x00003F2D
		public INameTransform NameTransform
		{
			get
			{
				return this.updateEntryFactory_.NameTransform;
			}
			set
			{
				this.updateEntryFactory_.NameTransform = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00005D3B File Offset: 0x00003F3B
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00005D43 File Offset: 0x00003F43
		public IEntryFactory EntryFactory
		{
			get
			{
				return this.updateEntryFactory_;
			}
			set
			{
				if (value == null)
				{
					this.updateEntryFactory_ = new ZipEntryFactory();
					return;
				}
				this.updateEntryFactory_ = value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00005D5B File Offset: 0x00003F5B
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00005D63 File Offset: 0x00003F63
		public int BufferSize
		{
			get
			{
				return this.bufferSize_;
			}
			set
			{
				if (value < 1024)
				{
					throw new ArgumentOutOfRangeException("value", "cannot be below 1024");
				}
				if (this.bufferSize_ != value)
				{
					this.bufferSize_ = value;
					this.copyBuffer_ = null;
				}
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00005D94 File Offset: 0x00003F94
		public bool IsUpdating
		{
			get
			{
				return this.updates_ != null;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00005D9F File Offset: 0x00003F9F
		// (set) Token: 0x06000149 RID: 329 RVA: 0x00005DA7 File Offset: 0x00003FA7
		public UseZip64 UseZip64
		{
			get
			{
				return this.useZip64_;
			}
			set
			{
				this.useZip64_ = value;
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00005DB0 File Offset: 0x00003FB0
		public void BeginUpdate(IArchiveStorage archiveStorage, IDynamicDataSource dataSource)
		{
			if (this.isDisposed_)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			if (this.IsEmbeddedArchive)
			{
				throw new ZipException("Cannot update embedded/SFX archives");
			}
			if (archiveStorage == null)
			{
				throw new ArgumentNullException("archiveStorage");
			}
			this.archiveStorage_ = archiveStorage;
			if (dataSource == null)
			{
				throw new ArgumentNullException("dataSource");
			}
			this.updateDataSource_ = dataSource;
			this.updateIndex_ = new Dictionary<string, int>();
			this.updates_ = new List<ZipFile.ZipUpdate>(this.entries_.Length);
			foreach (ZipEntry zipEntry in this.entries_)
			{
				int count = this.updates_.Count;
				this.updates_.Add(new ZipFile.ZipUpdate(zipEntry));
				this.updateIndex_.Add(zipEntry.Name, count);
			}
			this.updates_.Sort(new ZipFile.UpdateComparer());
			int num = 0;
			foreach (ZipFile.ZipUpdate zipUpdate in this.updates_)
			{
				if (num == this.updates_.Count - 1)
				{
					break;
				}
				zipUpdate.OffsetBasedSize = this.updates_[num + 1].Entry.Offset - zipUpdate.Entry.Offset;
				num++;
			}
			this.updateCount_ = (long)this.updates_.Count;
			this.contentsEdited_ = false;
			this.commentEdited_ = false;
			this.newComment_ = null;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00005F34 File Offset: 0x00004134
		public void BeginUpdate(IArchiveStorage archiveStorage)
		{
			this.BeginUpdate(archiveStorage, new DynamicDiskDataSource());
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00005F42 File Offset: 0x00004142
		public void BeginUpdate()
		{
			if (this.Name == null)
			{
				this.BeginUpdate(new MemoryArchiveStorage(), new DynamicDiskDataSource());
				return;
			}
			this.BeginUpdate(new DiskArchiveStorage(this), new DynamicDiskDataSource());
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00005F70 File Offset: 0x00004170
		public void CommitUpdate()
		{
			if (this.isDisposed_)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			this.CheckUpdating();
			try
			{
				this.updateIndex_.Clear();
				this.updateIndex_ = null;
				if (this.contentsEdited_)
				{
					this.RunUpdates();
				}
				else if (this.commentEdited_ && !this.isNewArchive_)
				{
					this.UpdateCommentOnly();
				}
				else if (this.entries_.Length == 0)
				{
					byte[] array = ((this.newComment_ != null) ? this.newComment_.RawComment : this._stringCodec.ZipArchiveCommentEncoding.GetBytes(this.comment_));
					ZipFormat.WriteEndOfCentralDirectory(this.baseStream_, 0L, 0L, 0L, array);
				}
			}
			finally
			{
				this.PostUpdateCleanup();
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00006034 File Offset: 0x00004234
		public void AbortUpdate()
		{
			this.PostUpdateCleanup();
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000603C File Offset: 0x0000423C
		public void SetComment(string comment)
		{
			if (this.isDisposed_)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			this.CheckUpdating();
			this.newComment_ = new ZipFile.ZipString(comment, this._stringCodec.ZipArchiveCommentEncoding);
			if (this.newComment_.RawLength > 65535)
			{
				this.newComment_ = null;
				throw new ZipException("Comment length exceeds maximum - 65535");
			}
			this.commentEdited_ = true;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x000060A4 File Offset: 0x000042A4
		private void AddUpdate(ZipFile.ZipUpdate update)
		{
			this.contentsEdited_ = true;
			int num = this.FindExistingUpdate(update.Entry.Name, true);
			if (num >= 0)
			{
				if (this.updates_[num] == null)
				{
					this.updateCount_ += 1L;
				}
				this.updates_[num] = update;
				return;
			}
			num = this.updates_.Count;
			this.updates_.Add(update);
			this.updateCount_ += 1L;
			this.updateIndex_.Add(update.Entry.Name, num);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00006138 File Offset: 0x00004338
		public void Add(string fileName, CompressionMethod compressionMethod, bool useUnicodeText)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			if (this.isDisposed_)
			{
				throw new ObjectDisposedException("ZipFile");
			}
			ZipFile.CheckSupportedCompressionMethod(compressionMethod);
			this.CheckUpdating();
			this.contentsEdited_ = true;
			ZipEntry zipEntry = this.EntryFactory.MakeFileEntry(fileName);
			zipEntry.IsUnicodeText = useUnicodeText;
			zipEntry.CompressionMethod = compressionMethod;
			this.AddUpdate(new ZipFile.ZipUpdate(fileName, zipEntry));
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000061A4 File Offset: 0x000043A4
		public void Add(string fileName, CompressionMethod compressionMethod)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			ZipFile.CheckSupportedCompressionMethod(compressionMethod);
			this.CheckUpdating();
			this.contentsEdited_ = true;
			ZipEntry zipEntry = this.EntryFactory.MakeFileEntry(fileName);
			zipEntry.CompressionMethod = compressionMethod;
			this.AddUpdate(new ZipFile.ZipUpdate(fileName, zipEntry));
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000061F3 File Offset: 0x000043F3
		public void Add(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			this.CheckUpdating();
			this.AddUpdate(new ZipFile.ZipUpdate(fileName, this.EntryFactory.MakeFileEntry(fileName)));
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00006221 File Offset: 0x00004421
		public void Add(string fileName, string entryName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			if (entryName == null)
			{
				throw new ArgumentNullException("entryName");
			}
			this.CheckUpdating();
			this.AddUpdate(new ZipFile.ZipUpdate(fileName, this.EntryFactory.MakeFileEntry(fileName, entryName, true)));
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000625F File Offset: 0x0000445F
		public void Add(IStaticDataSource dataSource, string entryName)
		{
			if (dataSource == null)
			{
				throw new ArgumentNullException("dataSource");
			}
			if (entryName == null)
			{
				throw new ArgumentNullException("entryName");
			}
			this.CheckUpdating();
			this.AddUpdate(new ZipFile.ZipUpdate(dataSource, this.EntryFactory.MakeFileEntry(entryName, false)));
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000629C File Offset: 0x0000449C
		public void Add(IStaticDataSource dataSource, string entryName, CompressionMethod compressionMethod)
		{
			if (dataSource == null)
			{
				throw new ArgumentNullException("dataSource");
			}
			if (entryName == null)
			{
				throw new ArgumentNullException("entryName");
			}
			ZipFile.CheckSupportedCompressionMethod(compressionMethod);
			this.CheckUpdating();
			ZipEntry zipEntry = this.EntryFactory.MakeFileEntry(entryName, false);
			zipEntry.CompressionMethod = compressionMethod;
			this.AddUpdate(new ZipFile.ZipUpdate(dataSource, zipEntry));
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000062F4 File Offset: 0x000044F4
		public void Add(IStaticDataSource dataSource, string entryName, CompressionMethod compressionMethod, bool useUnicodeText)
		{
			if (dataSource == null)
			{
				throw new ArgumentNullException("dataSource");
			}
			if (entryName == null)
			{
				throw new ArgumentNullException("entryName");
			}
			ZipFile.CheckSupportedCompressionMethod(compressionMethod);
			this.CheckUpdating();
			ZipEntry zipEntry = this.EntryFactory.MakeFileEntry(entryName, false);
			zipEntry.IsUnicodeText = useUnicodeText;
			zipEntry.CompressionMethod = compressionMethod;
			this.AddUpdate(new ZipFile.ZipUpdate(dataSource, zipEntry));
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00006353 File Offset: 0x00004553
		public void Add(ZipEntry entry)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			this.CheckUpdating();
			if (entry.Size != 0L || entry.CompressedSize != 0L)
			{
				throw new ZipException("Entry cannot have any data");
			}
			this.AddUpdate(new ZipFile.ZipUpdate(ZipFile.UpdateCommand.Add, entry));
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00006394 File Offset: 0x00004594
		public void Add(IStaticDataSource dataSource, ZipEntry entry)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			if (dataSource == null)
			{
				throw new ArgumentNullException("dataSource");
			}
			if (entry.AESKeySize > 0)
			{
				throw new NotSupportedException("Creation of AES encrypted entries is not supported");
			}
			ZipFile.CheckSupportedCompressionMethod(entry.CompressionMethod);
			this.CheckUpdating();
			this.AddUpdate(new ZipFile.ZipUpdate(dataSource, entry));
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000063F0 File Offset: 0x000045F0
		public void AddDirectory(string directoryName)
		{
			if (directoryName == null)
			{
				throw new ArgumentNullException("directoryName");
			}
			this.CheckUpdating();
			ZipEntry zipEntry = this.EntryFactory.MakeDirectoryEntry(directoryName);
			this.AddUpdate(new ZipFile.ZipUpdate(ZipFile.UpdateCommand.Add, zipEntry));
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000642B File Offset: 0x0000462B
		private static void CheckSupportedCompressionMethod(CompressionMethod compressionMethod)
		{
			if (compressionMethod != CompressionMethod.Deflated && compressionMethod != CompressionMethod.Stored && compressionMethod != CompressionMethod.BZip2)
			{
				throw new NotImplementedException("Compression method not supported");
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00006444 File Offset: 0x00004644
		public bool Delete(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			this.CheckUpdating();
			int num = this.FindExistingUpdate(fileName, false);
			if (num >= 0 && this.updates_[num] != null)
			{
				bool flag = true;
				this.contentsEdited_ = true;
				this.updates_[num] = null;
				this.updateCount_ -= 1L;
				return flag;
			}
			throw new ZipException("Cannot find entry to delete");
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000064B8 File Offset: 0x000046B8
		public void Delete(ZipEntry entry)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			this.CheckUpdating();
			int num = this.FindExistingUpdate(entry);
			if (num >= 0)
			{
				this.contentsEdited_ = true;
				this.updates_[num] = null;
				this.updateCount_ -= 1L;
				return;
			}
			throw new ZipException("Cannot find entry to delete");
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00006513 File Offset: 0x00004713
		private void WriteLEShort(int value)
		{
			this.baseStream_.WriteByte((byte)(value & 255));
			this.baseStream_.WriteByte((byte)((value >> 8) & 255));
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000653D File Offset: 0x0000473D
		private void WriteLEUshort(ushort value)
		{
			this.baseStream_.WriteByte((byte)(value & 255));
			this.baseStream_.WriteByte((byte)(value >> 8));
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00006561 File Offset: 0x00004761
		private void WriteLEInt(int value)
		{
			this.WriteLEShort(value & 65535);
			this.WriteLEShort(value >> 16);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000657A File Offset: 0x0000477A
		private void WriteLEUint(uint value)
		{
			this.WriteLEUshort((ushort)(value & 65535U));
			this.WriteLEUshort((ushort)(value >> 16));
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00006595 File Offset: 0x00004795
		private void WriteLeLong(long value)
		{
			this.WriteLEInt((int)(value & (long)((ulong)(-1))));
			this.WriteLEInt((int)(value >> 32));
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000065AD File Offset: 0x000047AD
		private void WriteLEUlong(ulong value)
		{
			this.WriteLEUint((uint)(value & (ulong)(-1)));
			this.WriteLEUint((uint)(value >> 32));
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000065C8 File Offset: 0x000047C8
		private void WriteLocalEntryHeader(ZipFile.ZipUpdate update)
		{
			ZipEntry outEntry = update.OutEntry;
			outEntry.Offset = this.baseStream_.Position;
			if (update.Command != ZipFile.UpdateCommand.Copy)
			{
				if (outEntry.CompressionMethod == CompressionMethod.Deflated)
				{
					if (outEntry.Size == 0L)
					{
						outEntry.CompressedSize = outEntry.Size;
						outEntry.Crc = 0L;
						outEntry.CompressionMethod = CompressionMethod.Stored;
					}
				}
				else if (outEntry.CompressionMethod == CompressionMethod.Stored)
				{
					outEntry.Flags &= -9;
				}
				if (this.HaveKeys)
				{
					outEntry.IsCrypted = true;
					if (outEntry.Crc < 0L)
					{
						outEntry.Flags |= 8;
					}
				}
				else
				{
					outEntry.IsCrypted = false;
				}
				switch (this.useZip64_)
				{
				case UseZip64.On:
					outEntry.ForceZip64();
					break;
				case UseZip64.Dynamic:
					if (outEntry.Size < 0L)
					{
						outEntry.ForceZip64();
					}
					break;
				}
			}
			this.WriteLEInt(67324752);
			this.WriteLEShort(outEntry.Version);
			this.WriteLEShort(outEntry.Flags);
			this.WriteLEShort((int)((byte)outEntry.CompressionMethodForHeader));
			this.WriteLEInt((int)outEntry.DosTime);
			if (!outEntry.HasCrc)
			{
				update.CrcPatchOffset = this.baseStream_.Position;
				this.WriteLEInt(0);
			}
			else
			{
				this.WriteLEInt((int)outEntry.Crc);
			}
			if (outEntry.LocalHeaderRequiresZip64)
			{
				this.WriteLEInt(-1);
				this.WriteLEInt(-1);
			}
			else
			{
				if (outEntry.CompressedSize < 0L || outEntry.Size < 0L)
				{
					update.SizePatchOffset = this.baseStream_.Position;
				}
				this.WriteLEInt((int)outEntry.CompressedSize);
				this.WriteLEInt((int)outEntry.Size);
			}
			byte[] bytes = this._stringCodec.ZipInputEncoding(outEntry.Flags).GetBytes(outEntry.Name);
			if (bytes.Length > 65535)
			{
				throw new ZipException("Entry name too long.");
			}
			ZipExtraData zipExtraData = new ZipExtraData(outEntry.ExtraData);
			if (outEntry.LocalHeaderRequiresZip64)
			{
				zipExtraData.StartNewEntry();
				zipExtraData.AddLeLong(outEntry.Size);
				zipExtraData.AddLeLong(outEntry.CompressedSize);
				zipExtraData.AddNewEntry(1);
			}
			else
			{
				zipExtraData.Delete(1);
			}
			outEntry.ExtraData = zipExtraData.GetEntryData();
			this.WriteLEShort(bytes.Length);
			this.WriteLEShort(outEntry.ExtraData.Length);
			if (bytes.Length != 0)
			{
				this.baseStream_.Write(bytes, 0, bytes.Length);
			}
			if (outEntry.LocalHeaderRequiresZip64)
			{
				if (!zipExtraData.Find(1))
				{
					throw new ZipException("Internal error cannot find extra data");
				}
				update.SizePatchOffset = this.baseStream_.Position + (long)zipExtraData.CurrentReadIndex;
			}
			if (outEntry.ExtraData.Length != 0)
			{
				this.baseStream_.Write(outEntry.ExtraData, 0, outEntry.ExtraData.Length);
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00006868 File Offset: 0x00004A68
		private int WriteCentralDirectoryHeader(ZipEntry entry)
		{
			if (entry.CompressedSize < 0L)
			{
				throw new ZipException("Attempt to write central directory entry with unknown csize");
			}
			if (entry.Size < 0L)
			{
				throw new ZipException("Attempt to write central directory entry with unknown size");
			}
			if (entry.Crc < 0L)
			{
				throw new ZipException("Attempt to write central directory entry with unknown crc");
			}
			this.WriteLEInt(33639248);
			this.WriteLEShort((entry.HostSystem << 8) | entry.VersionMadeBy);
			this.WriteLEShort(entry.Version);
			this.WriteLEShort(entry.Flags);
			this.WriteLEShort((int)((byte)entry.CompressionMethodForHeader));
			this.WriteLEInt((int)entry.DosTime);
			this.WriteLEInt((int)entry.Crc);
			bool flag = false;
			if (entry.IsZip64Forced() || entry.CompressedSize >= (long)((ulong)(-1)))
			{
				flag = true;
				this.WriteLEInt(-1);
			}
			else
			{
				this.WriteLEInt((int)(entry.CompressedSize & (long)((ulong)(-1))));
			}
			bool flag2 = false;
			if (entry.IsZip64Forced() || entry.Size >= (long)((ulong)(-1)))
			{
				flag2 = true;
				this.WriteLEInt(-1);
			}
			else
			{
				this.WriteLEInt((int)entry.Size);
			}
			byte[] bytes = this._stringCodec.ZipInputEncoding(entry.Flags).GetBytes(entry.Name);
			if (bytes.Length > 65535)
			{
				throw new ZipException("Entry name is too long.");
			}
			this.WriteLEShort(bytes.Length);
			ZipExtraData zipExtraData = new ZipExtraData(entry.ExtraData);
			if (entry.CentralHeaderRequiresZip64)
			{
				zipExtraData.StartNewEntry();
				if (flag2)
				{
					zipExtraData.AddLeLong(entry.Size);
				}
				if (flag)
				{
					zipExtraData.AddLeLong(entry.CompressedSize);
				}
				if (entry.Offset >= (long)((ulong)(-1)))
				{
					zipExtraData.AddLeLong(entry.Offset);
				}
				zipExtraData.AddNewEntry(1);
			}
			else
			{
				zipExtraData.Delete(1);
			}
			byte[] entryData = zipExtraData.GetEntryData();
			this.WriteLEShort(entryData.Length);
			this.WriteLEShort((entry.Comment != null) ? entry.Comment.Length : 0);
			this.WriteLEShort(0);
			this.WriteLEShort(0);
			if (entry.ExternalFileAttributes != -1)
			{
				this.WriteLEInt(entry.ExternalFileAttributes);
			}
			else if (entry.IsDirectory)
			{
				this.WriteLEUint(16U);
			}
			else
			{
				this.WriteLEUint(0U);
			}
			if (entry.Offset >= (long)((ulong)(-1)))
			{
				this.WriteLEUint(uint.MaxValue);
			}
			else
			{
				this.WriteLEUint((uint)((int)entry.Offset));
			}
			if (bytes.Length != 0)
			{
				this.baseStream_.Write(bytes, 0, bytes.Length);
			}
			if (entryData.Length != 0)
			{
				this.baseStream_.Write(entryData, 0, entryData.Length);
			}
			byte[] array = ((entry.Comment != null) ? Encoding.ASCII.GetBytes(entry.Comment) : Empty.Array<byte>());
			if (array.Length != 0)
			{
				this.baseStream_.Write(array, 0, array.Length);
			}
			return 46 + bytes.Length + entryData.Length + array.Length;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00006B07 File Offset: 0x00004D07
		private void PostUpdateCleanup()
		{
			this.updateDataSource_ = null;
			this.updates_ = null;
			this.updateIndex_ = null;
			if (this.archiveStorage_ != null)
			{
				this.archiveStorage_.Dispose();
				this.archiveStorage_ = null;
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00006B38 File Offset: 0x00004D38
		private string GetTransformedFileName(string name)
		{
			INameTransform nameTransform = this.NameTransform;
			if (nameTransform == null)
			{
				return name;
			}
			return nameTransform.TransformFile(name);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00006B58 File Offset: 0x00004D58
		private string GetTransformedDirectoryName(string name)
		{
			INameTransform nameTransform = this.NameTransform;
			if (nameTransform == null)
			{
				return name;
			}
			return nameTransform.TransformDirectory(name);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00006B78 File Offset: 0x00004D78
		private byte[] GetBuffer()
		{
			if (this.copyBuffer_ == null)
			{
				this.copyBuffer_ = new byte[this.bufferSize_];
			}
			return this.copyBuffer_;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00006B9C File Offset: 0x00004D9C
		private void CopyDescriptorBytes(ZipFile.ZipUpdate update, Stream dest, Stream source)
		{
			int i = ZipFile.GetDescriptorSize(update, false);
			if (i == 0)
			{
				return;
			}
			byte[] buffer = this.GetBuffer();
			source.Read(buffer, 0, 4);
			dest.Write(buffer, 0, 4);
			if (BitConverter.ToUInt32(buffer, 0) != 134695760U)
			{
				i -= buffer.Length;
			}
			while (i > 0)
			{
				int num = Math.Min(buffer.Length, i);
				int num2 = source.Read(buffer, 0, num);
				if (num2 <= 0)
				{
					throw new ZipException("Unxpected end of stream");
				}
				dest.Write(buffer, 0, num2);
				i -= num2;
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00006C1C File Offset: 0x00004E1C
		private void CopyBytes(ZipFile.ZipUpdate update, Stream destination, Stream source, long bytesToCopy, bool updateCrc)
		{
			if (destination == source)
			{
				throw new InvalidOperationException("Destination and source are the same");
			}
			Crc32 crc = new Crc32();
			byte[] buffer = this.GetBuffer();
			long num = bytesToCopy;
			long num2 = 0L;
			int num4;
			do
			{
				int num3 = buffer.Length;
				if (bytesToCopy < (long)num3)
				{
					num3 = (int)bytesToCopy;
				}
				num4 = source.Read(buffer, 0, num3);
				if (num4 > 0)
				{
					if (updateCrc)
					{
						crc.Update(new ArraySegment<byte>(buffer, 0, num4));
					}
					destination.Write(buffer, 0, num4);
					bytesToCopy -= (long)num4;
					num2 += (long)num4;
				}
			}
			while (num4 > 0 && bytesToCopy > 0L);
			if (num2 != num)
			{
				throw new ZipException(string.Format("Failed to copy bytes expected {0} read {1}", num, num2));
			}
			if (updateCrc)
			{
				update.OutEntry.Crc = crc.Value;
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00006CD8 File Offset: 0x00004ED8
		private static int GetDescriptorSize(ZipFile.ZipUpdate update, bool includingSignature)
		{
			if (!((GeneralBitFlags)update.Entry.Flags).HasAny(GeneralBitFlags.Descriptor))
			{
				return 0;
			}
			int num = (update.Entry.LocalHeaderRequiresZip64 ? 24 : 16);
			if (!includingSignature)
			{
				return num - 4;
			}
			return num;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00006D18 File Offset: 0x00004F18
		private void CopyDescriptorBytesDirect(ZipFile.ZipUpdate update, Stream stream, ref long destinationPosition, long sourcePosition)
		{
			byte[] buffer = this.GetBuffer();
			stream.Position = sourcePosition;
			stream.Read(buffer, 0, 4);
			bool flag = BitConverter.ToUInt32(buffer, 0) == 134695760U;
			int i = ZipFile.GetDescriptorSize(update, flag);
			while (i > 0)
			{
				stream.Position = sourcePosition;
				int num = stream.Read(buffer, 0, i);
				if (num <= 0)
				{
					throw new ZipException("Unexpected end of stream");
				}
				stream.Position = destinationPosition;
				stream.Write(buffer, 0, num);
				i -= num;
				destinationPosition += (long)num;
				sourcePosition += (long)num;
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00006DA4 File Offset: 0x00004FA4
		private void CopyEntryDataDirect(ZipFile.ZipUpdate update, Stream stream, bool updateCrc, ref long destinationPosition, ref long sourcePosition)
		{
			long num = update.Entry.CompressedSize;
			Crc32 crc = new Crc32();
			byte[] buffer = this.GetBuffer();
			long num2 = num;
			long num3 = 0L;
			int num5;
			do
			{
				int num4 = buffer.Length;
				if (num < (long)num4)
				{
					num4 = (int)num;
				}
				stream.Position = sourcePosition;
				num5 = stream.Read(buffer, 0, num4);
				if (num5 > 0)
				{
					if (updateCrc)
					{
						crc.Update(new ArraySegment<byte>(buffer, 0, num5));
					}
					stream.Position = destinationPosition;
					stream.Write(buffer, 0, num5);
					destinationPosition += (long)num5;
					sourcePosition += (long)num5;
					num -= (long)num5;
					num3 += (long)num5;
				}
			}
			while (num5 > 0 && num > 0L);
			if (num3 != num2)
			{
				throw new ZipException(string.Format("Failed to copy bytes expected {0} read {1}", num2, num3));
			}
			if (updateCrc)
			{
				update.OutEntry.Crc = crc.Value;
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00006E80 File Offset: 0x00005080
		private int FindExistingUpdate(ZipEntry entry)
		{
			int num = -1;
			if (this.updateIndex_.ContainsKey(entry.Name))
			{
				num = this.updateIndex_[entry.Name];
			}
			return num;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00006EB8 File Offset: 0x000050B8
		private int FindExistingUpdate(string fileName, bool isEntryName = false)
		{
			int num = -1;
			string text = ((!isEntryName) ? this.GetTransformedFileName(fileName) : fileName);
			if (this.updateIndex_.ContainsKey(text))
			{
				num = this.updateIndex_[text];
			}
			return num;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00006EF4 File Offset: 0x000050F4
		private Stream GetOutputStream(ZipEntry entry)
		{
			Stream stream = this.baseStream_;
			if (entry.IsCrypted)
			{
				stream = this.CreateAndInitEncryptionStream(stream, entry);
			}
			CompressionMethod compressionMethod = entry.CompressionMethod;
			if (compressionMethod != CompressionMethod.Stored)
			{
				if (compressionMethod != CompressionMethod.Deflated)
				{
					if (compressionMethod != CompressionMethod.BZip2)
					{
						throw new ZipException("Unknown compression method " + entry.CompressionMethod.ToString());
					}
					stream = new BZip2OutputStream(stream)
					{
						IsStreamOwner = entry.IsCrypted
					};
				}
				else
				{
					stream = new DeflaterOutputStream(stream, new Deflater(9, true))
					{
						IsStreamOwner = entry.IsCrypted
					};
				}
			}
			else if (!entry.IsCrypted)
			{
				stream = new ZipFile.UncompressedStream(stream);
			}
			return stream;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00006F98 File Offset: 0x00005198
		private void AddEntry(ZipFile workFile, ZipFile.ZipUpdate update)
		{
			Stream stream = null;
			if (update.Entry.IsFile)
			{
				stream = update.GetSource();
				if (stream == null)
				{
					stream = this.updateDataSource_.GetSource(update.Entry, update.Filename);
				}
			}
			bool flag = update.Entry.AESKeySize == 0;
			if (stream != null)
			{
				using (stream)
				{
					long length = stream.Length;
					if (update.OutEntry.Size < 0L)
					{
						update.OutEntry.Size = length;
					}
					else if (update.OutEntry.Size != length)
					{
						throw new ZipException("Entry size/stream size mismatch");
					}
					workFile.WriteLocalEntryHeader(update);
					long position = workFile.baseStream_.Position;
					using (Stream outputStream = workFile.GetOutputStream(update.OutEntry))
					{
						this.CopyBytes(update, outputStream, stream, length, flag);
					}
					long position2 = workFile.baseStream_.Position;
					update.OutEntry.CompressedSize = position2 - position;
					if ((update.OutEntry.Flags & 8) == 8)
					{
						ZipFormat.WriteDataDescriptor(workFile.baseStream_, update.OutEntry);
					}
					return;
				}
			}
			workFile.WriteLocalEntryHeader(update);
			update.OutEntry.CompressedSize = 0L;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000070E4 File Offset: 0x000052E4
		private void ModifyEntry(ZipFile workFile, ZipFile.ZipUpdate update)
		{
			workFile.WriteLocalEntryHeader(update);
			long position = workFile.baseStream_.Position;
			if (update.Entry.IsFile && update.Filename != null)
			{
				using (Stream outputStream = workFile.GetOutputStream(update.OutEntry))
				{
					using (Stream inputStream = this.GetInputStream(update.Entry))
					{
						this.CopyBytes(update, outputStream, inputStream, inputStream.Length, true);
					}
				}
			}
			long position2 = workFile.baseStream_.Position;
			update.Entry.CompressedSize = position2 - position;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00007190 File Offset: 0x00005390
		private void CopyEntryDirect(ZipFile workFile, ZipFile.ZipUpdate update, ref long destinationPosition)
		{
			bool flag = update.Entry.Offset == destinationPosition;
			if (!flag)
			{
				this.baseStream_.Position = destinationPosition;
				workFile.WriteLocalEntryHeader(update);
				destinationPosition = this.baseStream_.Position;
			}
			long num = 0L;
			long num2 = update.Entry.Offset + 26L;
			this.baseStream_.Seek(num2, SeekOrigin.Begin);
			uint num3 = (uint)this.ReadLEUshort();
			uint num4 = (uint)this.ReadLEUshort();
			num = this.baseStream_.Position + (long)((ulong)num3) + (long)((ulong)num4);
			if (!flag)
			{
				if (update.Entry.CompressedSize > 0L)
				{
					this.CopyEntryDataDirect(update, this.baseStream_, false, ref destinationPosition, ref num);
				}
				this.CopyDescriptorBytesDirect(update, this.baseStream_, ref destinationPosition, num);
				return;
			}
			if (update.OffsetBasedSize != -1L)
			{
				destinationPosition += update.OffsetBasedSize;
				return;
			}
			destinationPosition += num - num2 + 26L;
			destinationPosition += update.Entry.CompressedSize;
			this.baseStream_.Seek(destinationPosition, SeekOrigin.Begin);
			bool flag2 = this.ReadLEUint() == 134695760U;
			destinationPosition += (long)ZipFile.GetDescriptorSize(update, flag2);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000072A4 File Offset: 0x000054A4
		private void CopyEntry(ZipFile workFile, ZipFile.ZipUpdate update)
		{
			workFile.WriteLocalEntryHeader(update);
			if (update.Entry.CompressedSize > 0L)
			{
				long num = update.Entry.Offset + 26L;
				this.baseStream_.Seek(num, SeekOrigin.Begin);
				uint num2 = (uint)this.ReadLEUshort();
				uint num3 = (uint)this.ReadLEUshort();
				this.baseStream_.Seek((long)((ulong)(num2 + num3)), SeekOrigin.Current);
				this.CopyBytes(update, workFile.baseStream_, this.baseStream_, update.Entry.CompressedSize, false);
			}
			this.CopyDescriptorBytes(update, workFile.baseStream_, this.baseStream_);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00007336 File Offset: 0x00005536
		private void Reopen(Stream source)
		{
			this.isNewArchive_ = false;
			if (source == null)
			{
				throw new ZipException("Failed to reopen archive - no source");
			}
			this.baseStream_ = source;
			this.ReadEntries();
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000735B File Offset: 0x0000555B
		private void Reopen()
		{
			if (this.Name == null)
			{
				throw new InvalidOperationException("Name is not known cannot Reopen");
			}
			this.Reopen(File.Open(this.Name, FileMode.Open, FileAccess.Read, FileShare.Read));
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00007384 File Offset: 0x00005584
		private void UpdateCommentOnly()
		{
			long length = this.baseStream_.Length;
			Stream stream;
			if (this.archiveStorage_.UpdateMode == FileUpdateMode.Safe)
			{
				stream = this.archiveStorage_.MakeTemporaryCopy(this.baseStream_);
				this.baseStream_.Dispose();
				this.baseStream_ = null;
			}
			else if (this.archiveStorage_.UpdateMode == FileUpdateMode.Direct)
			{
				this.baseStream_ = this.archiveStorage_.OpenForDirectUpdate(this.baseStream_);
				stream = this.baseStream_;
			}
			else
			{
				this.baseStream_.Dispose();
				this.baseStream_ = null;
				stream = new FileStream(this.Name, FileMode.Open, FileAccess.ReadWrite);
			}
			try
			{
				if (ZipFormat.LocateBlockWithSignature(stream, 101010256, length, 22, 65535) < 0L)
				{
					throw new ZipException("Cannot find central directory");
				}
				stream.Position += 16L;
				byte[] rawComment = this.newComment_.RawComment;
				stream.WriteLEShort(rawComment.Length);
				stream.Write(rawComment, 0, rawComment.Length);
				stream.SetLength(stream.Position);
			}
			finally
			{
				if (stream != this.baseStream_)
				{
					stream.Dispose();
				}
			}
			if (this.archiveStorage_.UpdateMode == FileUpdateMode.Safe)
			{
				this.Reopen(this.archiveStorage_.ConvertTemporaryToFinal());
				return;
			}
			this.ReadEntries();
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000074C4 File Offset: 0x000056C4
		private void RunUpdates()
		{
			long num = 0L;
			long num2 = 0L;
			bool flag = false;
			long num3 = 0L;
			ZipFile zipFile;
			if (this.IsNewArchive)
			{
				zipFile = this;
				zipFile.baseStream_.Position = 0L;
				flag = true;
			}
			else if (this.archiveStorage_.UpdateMode == FileUpdateMode.Direct)
			{
				zipFile = this;
				zipFile.baseStream_.Position = 0L;
				flag = true;
				this.updates_.Sort(new ZipFile.UpdateComparer());
			}
			else
			{
				zipFile = ZipFile.Create(this.archiveStorage_.GetTemporaryOutput());
				zipFile.UseZip64 = this.UseZip64;
				if (this.key != null)
				{
					zipFile.key = (byte[])this.key.Clone();
				}
			}
			try
			{
				foreach (ZipFile.ZipUpdate zipUpdate in this.updates_)
				{
					if (zipUpdate != null)
					{
						switch (zipUpdate.Command)
						{
						case ZipFile.UpdateCommand.Copy:
							if (flag)
							{
								this.CopyEntryDirect(zipFile, zipUpdate, ref num3);
							}
							else
							{
								this.CopyEntry(zipFile, zipUpdate);
							}
							break;
						case ZipFile.UpdateCommand.Modify:
							this.ModifyEntry(zipFile, zipUpdate);
							break;
						case ZipFile.UpdateCommand.Add:
							if (!this.IsNewArchive && flag)
							{
								zipFile.baseStream_.Position = num3;
							}
							this.AddEntry(zipFile, zipUpdate);
							if (flag)
							{
								num3 = zipFile.baseStream_.Position;
							}
							break;
						}
					}
				}
				if (!this.IsNewArchive && flag)
				{
					zipFile.baseStream_.Position = num3;
				}
				long position = zipFile.baseStream_.Position;
				foreach (ZipFile.ZipUpdate zipUpdate2 in this.updates_)
				{
					if (zipUpdate2 != null)
					{
						num += (long)zipFile.WriteCentralDirectoryHeader(zipUpdate2.OutEntry);
					}
				}
				ZipFile.ZipString zipString = this.newComment_;
				byte[] array = ((zipString != null) ? zipString.RawComment : null) ?? this._stringCodec.ZipArchiveCommentEncoding.GetBytes(this.comment_);
				ZipFormat.WriteEndOfCentralDirectory(zipFile.baseStream_, this.updateCount_, num, position, array);
				num2 = zipFile.baseStream_.Position;
				foreach (ZipFile.ZipUpdate zipUpdate3 in this.updates_)
				{
					if (zipUpdate3 != null)
					{
						if (zipUpdate3.CrcPatchOffset > 0L && zipUpdate3.OutEntry.CompressedSize > 0L)
						{
							zipFile.baseStream_.Position = zipUpdate3.CrcPatchOffset;
							zipFile.WriteLEInt((int)zipUpdate3.OutEntry.Crc);
						}
						if (zipUpdate3.SizePatchOffset > 0L)
						{
							zipFile.baseStream_.Position = zipUpdate3.SizePatchOffset;
							if (zipUpdate3.OutEntry.LocalHeaderRequiresZip64)
							{
								zipFile.WriteLeLong(zipUpdate3.OutEntry.Size);
								zipFile.WriteLeLong(zipUpdate3.OutEntry.CompressedSize);
							}
							else
							{
								zipFile.WriteLEInt((int)zipUpdate3.OutEntry.CompressedSize);
								zipFile.WriteLEInt((int)zipUpdate3.OutEntry.Size);
							}
						}
					}
				}
			}
			catch
			{
				zipFile.Close();
				if (!flag && zipFile.Name != null)
				{
					File.Delete(zipFile.Name);
				}
				throw;
			}
			if (flag)
			{
				zipFile.baseStream_.SetLength(num2);
				zipFile.baseStream_.Flush();
				this.isNewArchive_ = false;
				this.ReadEntries();
				return;
			}
			this.baseStream_.Dispose();
			this.Reopen(this.archiveStorage_.ConvertTemporaryToFinal());
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000078AC File Offset: 0x00005AAC
		private void CheckUpdating()
		{
			if (this.updates_ == null)
			{
				throw new InvalidOperationException("BeginUpdate has not been called");
			}
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000078C1 File Offset: 0x00005AC1
		void IDisposable.Dispose()
		{
			this.Close();
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000078CC File Offset: 0x00005ACC
		private void DisposeInternal(bool disposing)
		{
			if (!this.isDisposed_)
			{
				this.isDisposed_ = true;
				this.entries_ = Empty.Array<ZipEntry>();
				if (this.IsStreamOwner && this.baseStream_ != null)
				{
					Stream stream = this.baseStream_;
					lock (stream)
					{
						this.baseStream_.Dispose();
					}
				}
				this.PostUpdateCleanup();
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00007944 File Offset: 0x00005B44
		protected virtual void Dispose(bool disposing)
		{
			this.DisposeInternal(disposing);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00007950 File Offset: 0x00005B50
		private ushort ReadLEUshort()
		{
			int num = this.baseStream_.ReadByte();
			if (num < 0)
			{
				throw new EndOfStreamException("End of stream");
			}
			int num2 = this.baseStream_.ReadByte();
			if (num2 < 0)
			{
				throw new EndOfStreamException("End of stream");
			}
			return (ushort)num | (ushort)(num2 << 8);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00007999 File Offset: 0x00005B99
		private uint ReadLEUint()
		{
			return (uint)((int)this.ReadLEUshort() | ((int)this.ReadLEUshort() << 16));
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000079AB File Offset: 0x00005BAB
		private ulong ReadLEUlong()
		{
			return (ulong)this.ReadLEUint() | ((ulong)this.ReadLEUint() << 32);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000079BF File Offset: 0x00005BBF
		private long LocateBlockWithSignature(int signature, long endLocation, int minimumBlockSize, int maximumVariableData)
		{
			return ZipFormat.LocateBlockWithSignature(this.baseStream_, signature, endLocation, minimumBlockSize, maximumVariableData);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000079D4 File Offset: 0x00005BD4
		private void ReadEntries()
		{
			if (!this.baseStream_.CanSeek)
			{
				throw new ZipException("ZipFile stream must be seekable");
			}
			long num = this.LocateBlockWithSignature(101010256, this.baseStream_.Length, 22, 65535);
			if (num < 0L)
			{
				throw new ZipException("Cannot find central directory");
			}
			int num2 = (int)this.ReadLEUshort();
			ushort num3 = this.ReadLEUshort();
			ulong num4 = (ulong)this.ReadLEUshort();
			ulong num5 = (ulong)this.ReadLEUshort();
			ulong num6 = (ulong)this.ReadLEUint();
			long num7 = (long)((ulong)this.ReadLEUint());
			uint num8 = (uint)this.ReadLEUshort();
			if (num8 > 0U)
			{
				byte[] array = new byte[num8];
				StreamUtils.ReadFully(this.baseStream_, array);
				this.comment_ = this._stringCodec.ZipArchiveCommentEncoding.GetString(array);
			}
			else
			{
				this.comment_ = string.Empty;
			}
			bool flag = false;
			bool flag2 = num2 == 65535 || num3 == ushort.MaxValue || num4 == 65535UL || num5 == 65535UL || num6 == (ulong)(-1) || num7 == (long)((ulong)(-1));
			if (this.LocateBlockWithSignature(117853008, num - 4L, 20, 0) < 0L)
			{
				if (flag2)
				{
					throw new ZipException("Cannot find Zip64 locator");
				}
			}
			else
			{
				flag = true;
				this.ReadLEUint();
				ulong num9 = this.ReadLEUlong();
				this.ReadLEUint();
				this.baseStream_.Position = (long)num9;
				if ((ulong)this.ReadLEUint() != 101075792UL)
				{
					throw new ZipException(string.Format("Invalid Zip64 Central directory signature at {0:X}", num9));
				}
				this.ReadLEUlong();
				this.ReadLEUshort();
				this.ReadLEUshort();
				this.ReadLEUint();
				this.ReadLEUint();
				num4 = this.ReadLEUlong();
				num5 = this.ReadLEUlong();
				num6 = this.ReadLEUlong();
				num7 = (long)this.ReadLEUlong();
			}
			this.entries_ = new ZipEntry[num4];
			if (!flag && num7 < num - (long)(4UL + num6))
			{
				this.offsetOfFirstEntry = num - (long)(4UL + num6 + (ulong)num7);
				if (this.offsetOfFirstEntry <= 0L)
				{
					throw new ZipException("Invalid embedded zip archive");
				}
			}
			this.baseStream_.Seek(this.offsetOfFirstEntry + num7, SeekOrigin.Begin);
			for (ulong num10 = 0UL; num10 < num4; num10 += 1UL)
			{
				if (this.ReadLEUint() != 33639248U)
				{
					throw new ZipException("Wrong Central Directory signature");
				}
				int num11 = (int)this.ReadLEUshort();
				int num12 = (int)this.ReadLEUshort();
				int num13 = (int)this.ReadLEUshort();
				int num14 = (int)this.ReadLEUshort();
				uint num15 = this.ReadLEUint();
				uint num16 = this.ReadLEUint();
				long num17 = (long)((ulong)this.ReadLEUint());
				long num18 = (long)((ulong)this.ReadLEUint());
				int num19 = (int)this.ReadLEUshort();
				int num20 = (int)this.ReadLEUshort();
				int num21 = (int)this.ReadLEUshort();
				this.ReadLEUshort();
				this.ReadLEUshort();
				uint num22 = this.ReadLEUint();
				long num23 = (long)((ulong)this.ReadLEUint());
				byte[] array2 = new byte[Math.Max(num19, num21)];
				Encoding encoding = this._stringCodec.ZipInputEncoding(num13);
				StreamUtils.ReadFully(this.baseStream_, array2, 0, num19);
				string @string = encoding.GetString(array2, 0, num19);
				bool flag3 = encoding.IsZipUnicode();
				ZipEntry zipEntry = new ZipEntry(@string, num12, num11, (CompressionMethod)num14, flag3)
				{
					Crc = (long)((ulong)num16 & (ulong)(-1)),
					Size = (num18 & (long)((ulong)(-1))),
					CompressedSize = (num17 & (long)((ulong)(-1))),
					Flags = num13,
					DosTime = (long)((ulong)num15),
					ZipFileIndex = (long)num10,
					Offset = num23,
					ExternalFileAttributes = (int)num22
				};
				if (!zipEntry.HasFlag(GeneralBitFlags.Descriptor))
				{
					zipEntry.CryptoCheckValue = (byte)(num16 >> 24);
				}
				else
				{
					zipEntry.CryptoCheckValue = (byte)((num15 >> 8) & 255U);
				}
				if (num20 > 0)
				{
					byte[] array3 = new byte[num20];
					StreamUtils.ReadFully(this.baseStream_, array3);
					zipEntry.ExtraData = array3;
				}
				zipEntry.ProcessExtraData(false);
				if (num21 > 0)
				{
					StreamUtils.ReadFully(this.baseStream_, array2, 0, num21);
					zipEntry.Comment = encoding.GetString(array2, 0, num21);
				}
				this.entries_[(int)(checked((IntPtr)num10))] = zipEntry;
			}
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00007DB1 File Offset: 0x00005FB1
		private long LocateEntry(ZipEntry entry)
		{
			return this.TestLocalHeader(entry, this.SkipLocalEntryTestsOnLocate ? ZipFile.HeaderTest.None : ZipFile.HeaderTest.Extract);
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00007DC6 File Offset: 0x00005FC6
		// (set) Token: 0x06000185 RID: 389 RVA: 0x00007DCE File Offset: 0x00005FCE
		public bool SkipLocalEntryTestsOnLocate { get; set; }

		// Token: 0x06000186 RID: 390 RVA: 0x00007DD8 File Offset: 0x00005FD8
		private Stream CreateAndInitDecryptionStream(Stream baseStream, ZipEntry entry)
		{
			CryptoStream cryptoStream;
			if (entry.CompressionMethodForHeader == CompressionMethod.WinZipAES)
			{
				if (entry.Version < 51)
				{
					throw new ZipException("Decryption method not supported");
				}
				this.OnKeysRequired(entry.Name);
				if (this.rawPassword_ == null)
				{
					throw new ZipException("No password available for AES encrypted stream");
				}
				int aessaltLen = entry.AESSaltLen;
				byte[] array = new byte[aessaltLen];
				int num = StreamUtils.ReadRequestedBytes(baseStream, array, 0, aessaltLen);
				if (num != aessaltLen)
				{
					throw new ZipException(string.Format("AES Salt expected {0} git {1}", aessaltLen, num));
				}
				byte[] array2 = new byte[2];
				StreamUtils.ReadFully(baseStream, array2);
				int num2 = entry.AESKeySize / 8;
				ZipAESTransform zipAESTransform = new ZipAESTransform(this.rawPassword_, array, num2, false);
				byte[] pwdVerifier = zipAESTransform.PwdVerifier;
				if (pwdVerifier[0] != array2[0] || pwdVerifier[1] != array2[1])
				{
					throw new ZipException("Invalid password for AES");
				}
				cryptoStream = new ZipAESStream(baseStream, zipAESTransform, CryptoStreamMode.Read);
			}
			else
			{
				if (entry.Version >= 50 && entry.HasFlag(GeneralBitFlags.StrongEncryption))
				{
					throw new ZipException("Decryption method not supported");
				}
				PkzipClassicManaged pkzipClassicManaged = new PkzipClassicManaged();
				this.OnKeysRequired(entry.Name);
				if (!this.HaveKeys)
				{
					throw new ZipException("No password available for encrypted stream");
				}
				cryptoStream = new CryptoStream(baseStream, pkzipClassicManaged.CreateDecryptor(this.key, null), CryptoStreamMode.Read);
				ZipFile.CheckClassicPassword(cryptoStream, entry);
			}
			return cryptoStream;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00007F28 File Offset: 0x00006128
		private Stream CreateAndInitEncryptionStream(Stream baseStream, ZipEntry entry)
		{
			if (entry.Version >= 50 && entry.HasFlag(GeneralBitFlags.StrongEncryption))
			{
				return null;
			}
			PkzipClassicManaged pkzipClassicManaged = new PkzipClassicManaged();
			this.OnKeysRequired(entry.Name);
			if (!this.HaveKeys)
			{
				throw new ZipException("No password available for encrypted stream");
			}
			CryptoStream cryptoStream = new CryptoStream(new ZipFile.UncompressedStream(baseStream), pkzipClassicManaged.CreateEncryptor(this.key, null), CryptoStreamMode.Write);
			if (entry.Crc < 0L || entry.HasFlag(GeneralBitFlags.Descriptor))
			{
				ZipFile.WriteEncryptionHeader(cryptoStream, entry.DosTime << 16);
			}
			else
			{
				ZipFile.WriteEncryptionHeader(cryptoStream, entry.Crc);
			}
			return cryptoStream;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00007FBC File Offset: 0x000061BC
		private static void CheckClassicPassword(CryptoStream classicCryptoStream, ZipEntry entry)
		{
			byte[] array = new byte[12];
			StreamUtils.ReadFully(classicCryptoStream, array);
			if (array[11] != entry.CryptoCheckValue)
			{
				throw new ZipException("Invalid password");
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00007FF0 File Offset: 0x000061F0
		private static void WriteEncryptionHeader(Stream stream, long crcValue)
		{
			byte[] array = new byte[12];
			using (RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create())
			{
				randomNumberGenerator.GetBytes(array);
			}
			array[11] = (byte)(crcValue >> 24);
			stream.Write(array, 0, array.Length);
		}

		// Token: 0x040000E3 RID: 227
		public ZipFile.KeysRequiredEventHandler KeysRequired;

		// Token: 0x040000E4 RID: 228
		private const int DefaultBufferSize = 4096;

		// Token: 0x040000E6 RID: 230
		private bool isDisposed_;

		// Token: 0x040000E7 RID: 231
		private string name_;

		// Token: 0x040000E8 RID: 232
		private string comment_ = string.Empty;

		// Token: 0x040000E9 RID: 233
		private string rawPassword_;

		// Token: 0x040000EA RID: 234
		private Stream baseStream_;

		// Token: 0x040000EB RID: 235
		private bool isStreamOwner;

		// Token: 0x040000EC RID: 236
		private long offsetOfFirstEntry;

		// Token: 0x040000ED RID: 237
		private ZipEntry[] entries_;

		// Token: 0x040000EE RID: 238
		private byte[] key;

		// Token: 0x040000EF RID: 239
		private bool isNewArchive_;

		// Token: 0x040000F0 RID: 240
		private StringCodec _stringCodec = ZipStrings.GetStringCodec();

		// Token: 0x040000F1 RID: 241
		private UseZip64 useZip64_ = UseZip64.Dynamic;

		// Token: 0x040000F2 RID: 242
		private List<ZipFile.ZipUpdate> updates_;

		// Token: 0x040000F3 RID: 243
		private long updateCount_;

		// Token: 0x040000F4 RID: 244
		private Dictionary<string, int> updateIndex_;

		// Token: 0x040000F5 RID: 245
		private IArchiveStorage archiveStorage_;

		// Token: 0x040000F6 RID: 246
		private IDynamicDataSource updateDataSource_;

		// Token: 0x040000F7 RID: 247
		private bool contentsEdited_;

		// Token: 0x040000F8 RID: 248
		private int bufferSize_ = 4096;

		// Token: 0x040000F9 RID: 249
		private byte[] copyBuffer_;

		// Token: 0x040000FA RID: 250
		private ZipFile.ZipString newComment_;

		// Token: 0x040000FB RID: 251
		private bool commentEdited_;

		// Token: 0x040000FC RID: 252
		private IEntryFactory updateEntryFactory_ = new ZipEntryFactory();

		// Token: 0x0200002A RID: 42
		// (Invoke) Token: 0x0600018B RID: 395
		public delegate void KeysRequiredEventHandler(object sender, KeysRequiredEventArgs e);

		// Token: 0x0200002B RID: 43
		[Flags]
		private enum HeaderTest
		{
			// Token: 0x040000FE RID: 254
			None = 0,
			// Token: 0x040000FF RID: 255
			Extract = 1,
			// Token: 0x04000100 RID: 256
			Header = 2
		}

		// Token: 0x0200002C RID: 44
		private enum UpdateCommand
		{
			// Token: 0x04000102 RID: 258
			Copy,
			// Token: 0x04000103 RID: 259
			Modify,
			// Token: 0x04000104 RID: 260
			Add
		}

		// Token: 0x0200002D RID: 45
		private class UpdateComparer : IComparer<ZipFile.ZipUpdate>
		{
			// Token: 0x0600018E RID: 398 RVA: 0x00008044 File Offset: 0x00006244
			public int Compare(ZipFile.ZipUpdate x, ZipFile.ZipUpdate y)
			{
				int num;
				if (x == null)
				{
					if (y == null)
					{
						num = 0;
					}
					else
					{
						num = -1;
					}
				}
				else if (y == null)
				{
					num = 1;
				}
				else
				{
					int num2 = ((x.Command == ZipFile.UpdateCommand.Copy || x.Command == ZipFile.UpdateCommand.Modify) ? 0 : 1);
					int num3 = ((y.Command == ZipFile.UpdateCommand.Copy || y.Command == ZipFile.UpdateCommand.Modify) ? 0 : 1);
					num = num2 - num3;
					if (num == 0)
					{
						long num4 = x.Entry.Offset - y.Entry.Offset;
						if (num4 < 0L)
						{
							num = -1;
						}
						else if (num4 == 0L)
						{
							num = 0;
						}
						else
						{
							num = 1;
						}
					}
				}
				return num;
			}
		}

		// Token: 0x0200002E RID: 46
		private class ZipUpdate
		{
			// Token: 0x06000190 RID: 400 RVA: 0x000080CA File Offset: 0x000062CA
			public ZipUpdate(string fileName, ZipEntry entry)
			{
				this.command_ = ZipFile.UpdateCommand.Add;
				this.entry_ = entry;
				this.filename_ = fileName;
			}

			// Token: 0x06000191 RID: 401 RVA: 0x00008100 File Offset: 0x00006300
			[Obsolete]
			public ZipUpdate(string fileName, string entryName, CompressionMethod compressionMethod)
			{
				this.command_ = ZipFile.UpdateCommand.Add;
				this.entry_ = new ZipEntry(entryName)
				{
					CompressionMethod = compressionMethod
				};
				this.filename_ = fileName;
			}

			// Token: 0x06000192 RID: 402 RVA: 0x0000814C File Offset: 0x0000634C
			[Obsolete]
			public ZipUpdate(string fileName, string entryName)
				: this(fileName, entryName, CompressionMethod.Deflated)
			{
			}

			// Token: 0x06000193 RID: 403 RVA: 0x00008158 File Offset: 0x00006358
			[Obsolete]
			public ZipUpdate(IStaticDataSource dataSource, string entryName, CompressionMethod compressionMethod)
			{
				this.command_ = ZipFile.UpdateCommand.Add;
				this.entry_ = new ZipEntry(entryName)
				{
					CompressionMethod = compressionMethod
				};
				this.dataSource_ = dataSource;
			}

			// Token: 0x06000194 RID: 404 RVA: 0x000081A4 File Offset: 0x000063A4
			public ZipUpdate(IStaticDataSource dataSource, ZipEntry entry)
			{
				this.command_ = ZipFile.UpdateCommand.Add;
				this.entry_ = entry;
				this.dataSource_ = dataSource;
			}

			// Token: 0x06000195 RID: 405 RVA: 0x000081D9 File Offset: 0x000063D9
			public ZipUpdate(ZipEntry original, ZipEntry updated)
			{
				throw new ZipException("Modify not currently supported");
			}

			// Token: 0x06000196 RID: 406 RVA: 0x00008203 File Offset: 0x00006403
			public ZipUpdate(ZipFile.UpdateCommand command, ZipEntry entry)
			{
				this.command_ = command;
				this.entry_ = (ZipEntry)entry.Clone();
			}

			// Token: 0x06000197 RID: 407 RVA: 0x0000823B File Offset: 0x0000643B
			public ZipUpdate(ZipEntry entry)
				: this(ZipFile.UpdateCommand.Copy, entry)
			{
			}

			// Token: 0x17000066 RID: 102
			// (get) Token: 0x06000198 RID: 408 RVA: 0x00008245 File Offset: 0x00006445
			public ZipEntry Entry
			{
				get
				{
					return this.entry_;
				}
			}

			// Token: 0x17000067 RID: 103
			// (get) Token: 0x06000199 RID: 409 RVA: 0x0000824D File Offset: 0x0000644D
			public ZipEntry OutEntry
			{
				get
				{
					if (this.outEntry_ == null)
					{
						this.outEntry_ = (ZipEntry)this.entry_.Clone();
					}
					return this.outEntry_;
				}
			}

			// Token: 0x17000068 RID: 104
			// (get) Token: 0x0600019A RID: 410 RVA: 0x00008273 File Offset: 0x00006473
			public ZipFile.UpdateCommand Command
			{
				get
				{
					return this.command_;
				}
			}

			// Token: 0x17000069 RID: 105
			// (get) Token: 0x0600019B RID: 411 RVA: 0x0000827B File Offset: 0x0000647B
			public string Filename
			{
				get
				{
					return this.filename_;
				}
			}

			// Token: 0x1700006A RID: 106
			// (get) Token: 0x0600019C RID: 412 RVA: 0x00008283 File Offset: 0x00006483
			// (set) Token: 0x0600019D RID: 413 RVA: 0x0000828B File Offset: 0x0000648B
			public long SizePatchOffset
			{
				get
				{
					return this.sizePatchOffset_;
				}
				set
				{
					this.sizePatchOffset_ = value;
				}
			}

			// Token: 0x1700006B RID: 107
			// (get) Token: 0x0600019E RID: 414 RVA: 0x00008294 File Offset: 0x00006494
			// (set) Token: 0x0600019F RID: 415 RVA: 0x0000829C File Offset: 0x0000649C
			public long CrcPatchOffset
			{
				get
				{
					return this.crcPatchOffset_;
				}
				set
				{
					this.crcPatchOffset_ = value;
				}
			}

			// Token: 0x1700006C RID: 108
			// (get) Token: 0x060001A0 RID: 416 RVA: 0x000082A5 File Offset: 0x000064A5
			// (set) Token: 0x060001A1 RID: 417 RVA: 0x000082AD File Offset: 0x000064AD
			public long OffsetBasedSize
			{
				get
				{
					return this._offsetBasedSize;
				}
				set
				{
					this._offsetBasedSize = value;
				}
			}

			// Token: 0x060001A2 RID: 418 RVA: 0x000082B8 File Offset: 0x000064B8
			public Stream GetSource()
			{
				Stream stream = null;
				if (this.dataSource_ != null)
				{
					stream = this.dataSource_.GetSource();
				}
				return stream;
			}

			// Token: 0x04000105 RID: 261
			private ZipEntry entry_;

			// Token: 0x04000106 RID: 262
			private ZipEntry outEntry_;

			// Token: 0x04000107 RID: 263
			private readonly ZipFile.UpdateCommand command_;

			// Token: 0x04000108 RID: 264
			private IStaticDataSource dataSource_;

			// Token: 0x04000109 RID: 265
			private readonly string filename_;

			// Token: 0x0400010A RID: 266
			private long sizePatchOffset_ = -1L;

			// Token: 0x0400010B RID: 267
			private long crcPatchOffset_ = -1L;

			// Token: 0x0400010C RID: 268
			private long _offsetBasedSize = -1L;
		}

		// Token: 0x0200002F RID: 47
		private class ZipString
		{
			// Token: 0x060001A3 RID: 419 RVA: 0x000082DC File Offset: 0x000064DC
			public ZipString(string comment, Encoding encoding)
			{
				this.comment_ = comment;
				this.isSourceString_ = true;
				this._encoding = encoding;
			}

			// Token: 0x060001A4 RID: 420 RVA: 0x000082F9 File Offset: 0x000064F9
			public ZipString(byte[] rawString, Encoding encoding)
			{
				this.rawComment_ = rawString;
				this._encoding = encoding;
			}

			// Token: 0x1700006D RID: 109
			// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000830F File Offset: 0x0000650F
			public bool IsSourceString
			{
				get
				{
					return this.isSourceString_;
				}
			}

			// Token: 0x1700006E RID: 110
			// (get) Token: 0x060001A6 RID: 422 RVA: 0x00008317 File Offset: 0x00006517
			public int RawLength
			{
				get
				{
					this.MakeBytesAvailable();
					return this.rawComment_.Length;
				}
			}

			// Token: 0x1700006F RID: 111
			// (get) Token: 0x060001A7 RID: 423 RVA: 0x00008327 File Offset: 0x00006527
			public byte[] RawComment
			{
				get
				{
					this.MakeBytesAvailable();
					return (byte[])this.rawComment_.Clone();
				}
			}

			// Token: 0x060001A8 RID: 424 RVA: 0x0000833F File Offset: 0x0000653F
			public void Reset()
			{
				if (this.isSourceString_)
				{
					this.rawComment_ = null;
					return;
				}
				this.comment_ = null;
			}

			// Token: 0x060001A9 RID: 425 RVA: 0x00008358 File Offset: 0x00006558
			private void MakeTextAvailable()
			{
				if (this.comment_ == null)
				{
					this.comment_ = this._encoding.GetString(this.rawComment_);
				}
			}

			// Token: 0x060001AA RID: 426 RVA: 0x00008379 File Offset: 0x00006579
			private void MakeBytesAvailable()
			{
				if (this.rawComment_ == null)
				{
					this.rawComment_ = this._encoding.GetBytes(this.comment_);
				}
			}

			// Token: 0x060001AB RID: 427 RVA: 0x0000839A File Offset: 0x0000659A
			public static implicit operator string(ZipFile.ZipString zipString)
			{
				zipString.MakeTextAvailable();
				return zipString.comment_;
			}

			// Token: 0x0400010D RID: 269
			private string comment_;

			// Token: 0x0400010E RID: 270
			private byte[] rawComment_;

			// Token: 0x0400010F RID: 271
			private readonly bool isSourceString_;

			// Token: 0x04000110 RID: 272
			private readonly Encoding _encoding;
		}

		// Token: 0x02000030 RID: 48
		private class ZipEntryEnumerator : IEnumerator
		{
			// Token: 0x060001AC RID: 428 RVA: 0x000083A8 File Offset: 0x000065A8
			public ZipEntryEnumerator(ZipEntry[] entries)
			{
				this.array = entries;
			}

			// Token: 0x17000070 RID: 112
			// (get) Token: 0x060001AD RID: 429 RVA: 0x000083BE File Offset: 0x000065BE
			public object Current
			{
				get
				{
					return this.array[this.index];
				}
			}

			// Token: 0x060001AE RID: 430 RVA: 0x000083CD File Offset: 0x000065CD
			public void Reset()
			{
				this.index = -1;
			}

			// Token: 0x060001AF RID: 431 RVA: 0x000083D8 File Offset: 0x000065D8
			public bool MoveNext()
			{
				int num = this.index + 1;
				this.index = num;
				return num < this.array.Length;
			}

			// Token: 0x04000111 RID: 273
			private ZipEntry[] array;

			// Token: 0x04000112 RID: 274
			private int index = -1;
		}

		// Token: 0x02000031 RID: 49
		private class UncompressedStream : Stream
		{
			// Token: 0x060001B0 RID: 432 RVA: 0x00008400 File Offset: 0x00006600
			public UncompressedStream(Stream baseStream)
			{
				this.baseStream_ = baseStream;
			}

			// Token: 0x17000071 RID: 113
			// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000840F File Offset: 0x0000660F
			public override bool CanRead
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060001B2 RID: 434 RVA: 0x00008412 File Offset: 0x00006612
			public override void Flush()
			{
				this.baseStream_.Flush();
			}

			// Token: 0x17000072 RID: 114
			// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000841F File Offset: 0x0000661F
			public override bool CanWrite
			{
				get
				{
					return this.baseStream_.CanWrite;
				}
			}

			// Token: 0x17000073 RID: 115
			// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000840F File Offset: 0x0000660F
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000074 RID: 116
			// (get) Token: 0x060001B5 RID: 437 RVA: 0x0000842C File Offset: 0x0000662C
			public override long Length
			{
				get
				{
					return 0L;
				}
			}

			// Token: 0x17000075 RID: 117
			// (get) Token: 0x060001B6 RID: 438 RVA: 0x00008430 File Offset: 0x00006630
			// (set) Token: 0x060001B7 RID: 439 RVA: 0x0000843D File Offset: 0x0000663D
			public override long Position
			{
				get
				{
					return this.baseStream_.Position;
				}
				set
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x060001B8 RID: 440 RVA: 0x0000840F File Offset: 0x0000660F
			public override int Read(byte[] buffer, int offset, int count)
			{
				return 0;
			}

			// Token: 0x060001B9 RID: 441 RVA: 0x0000842C File Offset: 0x0000662C
			public override long Seek(long offset, SeekOrigin origin)
			{
				return 0L;
			}

			// Token: 0x060001BA RID: 442 RVA: 0x00008444 File Offset: 0x00006644
			public override void SetLength(long value)
			{
			}

			// Token: 0x060001BB RID: 443 RVA: 0x00008446 File Offset: 0x00006646
			public override void Write(byte[] buffer, int offset, int count)
			{
				this.baseStream_.Write(buffer, offset, count);
			}

			// Token: 0x04000113 RID: 275
			private readonly Stream baseStream_;
		}

		// Token: 0x02000032 RID: 50
		private class PartialInputStream : Stream
		{
			// Token: 0x060001BC RID: 444 RVA: 0x00008456 File Offset: 0x00006656
			public PartialInputStream(ZipFile zipFile, long start, long length)
			{
				this.start_ = start;
				this.length_ = length;
				this.zipFile_ = zipFile;
				this.baseStream_ = this.zipFile_.baseStream_;
				this.readPos_ = start;
				this.end_ = start + length;
			}

			// Token: 0x060001BD RID: 445 RVA: 0x00008494 File Offset: 0x00006694
			public override int ReadByte()
			{
				if (this.readPos_ >= this.end_)
				{
					return -1;
				}
				Stream stream = this.baseStream_;
				int num2;
				lock (stream)
				{
					Stream stream2 = this.baseStream_;
					long num = this.readPos_;
					this.readPos_ = num + 1L;
					stream2.Seek(num, SeekOrigin.Begin);
					num2 = this.baseStream_.ReadByte();
				}
				return num2;
			}

			// Token: 0x060001BE RID: 446 RVA: 0x0000850C File Offset: 0x0000670C
			public override int Read(byte[] buffer, int offset, int count)
			{
				Stream stream = this.baseStream_;
				int num2;
				lock (stream)
				{
					if ((long)count > this.end_ - this.readPos_)
					{
						count = (int)(this.end_ - this.readPos_);
						if (count == 0)
						{
							return 0;
						}
					}
					if (this.baseStream_.Position != this.readPos_)
					{
						this.baseStream_.Seek(this.readPos_, SeekOrigin.Begin);
					}
					int num = this.baseStream_.Read(buffer, offset, count);
					if (num > 0)
					{
						this.readPos_ += (long)num;
					}
					num2 = num;
				}
				return num2;
			}

			// Token: 0x060001BF RID: 447 RVA: 0x000085BC File Offset: 0x000067BC
			public override void Write(byte[] buffer, int offset, int count)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060001C0 RID: 448 RVA: 0x000085BC File Offset: 0x000067BC
			public override void SetLength(long value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060001C1 RID: 449 RVA: 0x000085C4 File Offset: 0x000067C4
			public override long Seek(long offset, SeekOrigin origin)
			{
				long num = this.readPos_;
				switch (origin)
				{
				case SeekOrigin.Begin:
					num = this.start_ + offset;
					break;
				case SeekOrigin.Current:
					num = this.readPos_ + offset;
					break;
				case SeekOrigin.End:
					num = this.end_ + offset;
					break;
				}
				if (num < this.start_)
				{
					throw new ArgumentException("Negative position is invalid");
				}
				if (num > this.end_)
				{
					throw new IOException("Cannot seek past end");
				}
				this.readPos_ = num;
				return this.readPos_;
			}

			// Token: 0x060001C2 RID: 450 RVA: 0x00008444 File Offset: 0x00006644
			public override void Flush()
			{
			}

			// Token: 0x17000076 RID: 118
			// (get) Token: 0x060001C3 RID: 451 RVA: 0x00008640 File Offset: 0x00006840
			// (set) Token: 0x060001C4 RID: 452 RVA: 0x00008650 File Offset: 0x00006850
			public override long Position
			{
				get
				{
					return this.readPos_ - this.start_;
				}
				set
				{
					long num = this.start_ + value;
					if (num < this.start_)
					{
						throw new ArgumentException("Negative position is invalid");
					}
					if (num > this.end_)
					{
						throw new InvalidOperationException("Cannot seek past end");
					}
					this.readPos_ = num;
				}
			}

			// Token: 0x17000077 RID: 119
			// (get) Token: 0x060001C5 RID: 453 RVA: 0x00008695 File Offset: 0x00006895
			public override long Length
			{
				get
				{
					return this.length_;
				}
			}

			// Token: 0x17000078 RID: 120
			// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000840F File Offset: 0x0000660F
			public override bool CanWrite
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000079 RID: 121
			// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000869D File Offset: 0x0000689D
			public override bool CanSeek
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700007A RID: 122
			// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000869D File Offset: 0x0000689D
			public override bool CanRead
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700007B RID: 123
			// (get) Token: 0x060001C9 RID: 457 RVA: 0x000086A0 File Offset: 0x000068A0
			public override bool CanTimeout
			{
				get
				{
					return this.baseStream_.CanTimeout;
				}
			}

			// Token: 0x04000114 RID: 276
			private ZipFile zipFile_;

			// Token: 0x04000115 RID: 277
			private Stream baseStream_;

			// Token: 0x04000116 RID: 278
			private readonly long start_;

			// Token: 0x04000117 RID: 279
			private readonly long length_;

			// Token: 0x04000118 RID: 280
			private long readPos_;

			// Token: 0x04000119 RID: 281
			private readonly long end_;
		}
	}
}
