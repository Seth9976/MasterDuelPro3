using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Checksum;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Encryption;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000047 RID: 71
	public class ZipOutputStream : DeflaterOutputStream
	{
		// Token: 0x06000234 RID: 564 RVA: 0x0000A814 File Offset: 0x00008A14
		public ZipOutputStream(Stream baseOutputStream)
			: base(baseOutputStream, new Deflater(-1, true))
		{
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000A870 File Offset: 0x00008A70
		public ZipOutputStream(Stream baseOutputStream, int bufferSize)
			: base(baseOutputStream, new Deflater(-1, true), bufferSize)
		{
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000A8CD File Offset: 0x00008ACD
		public ZipOutputStream(Stream baseOutputStream, StringCodec stringCodec)
			: this(baseOutputStream)
		{
			this._stringCodec = stringCodec;
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000A8DD File Offset: 0x00008ADD
		public bool IsFinished
		{
			get
			{
				return this.entries == null;
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000A8E8 File Offset: 0x00008AE8
		public void SetComment(string comment)
		{
			byte[] bytes = this._stringCodec.ZipArchiveCommentEncoding.GetBytes(comment);
			if (bytes.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("comment");
			}
			this.zipComment = bytes;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000A923 File Offset: 0x00008B23
		public void SetLevel(int level)
		{
			this.deflater_.SetLevel(level);
			this.defaultCompressionLevel = level;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000A938 File Offset: 0x00008B38
		public int GetLevel()
		{
			return this.deflater_.GetLevel();
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000A945 File Offset: 0x00008B45
		// (set) Token: 0x0600023C RID: 572 RVA: 0x0000A94D File Offset: 0x00008B4D
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

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000A956 File Offset: 0x00008B56
		// (set) Token: 0x0600023E RID: 574 RVA: 0x0000A95E File Offset: 0x00008B5E
		public INameTransform NameTransform { get; set; } = new PathTransformer();

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000A967 File Offset: 0x00008B67
		// (set) Token: 0x06000240 RID: 576 RVA: 0x0000A96F File Offset: 0x00008B6F
		public string Password
		{
			get
			{
				return this.password;
			}
			set
			{
				if (value != null && value.Length == 0)
				{
					this.password = null;
					return;
				}
				this.password = value;
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000A98B File Offset: 0x00008B8B
		private void WriteLeShort(int value)
		{
			this.baseOutputStream_.WriteByte((byte)(value & 255));
			this.baseOutputStream_.WriteByte((byte)((value >> 8) & 255));
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000A9B5 File Offset: 0x00008BB5
		private void WriteLeInt(int value)
		{
			this.WriteLeShort(value);
			this.WriteLeShort(value >> 16);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000A9C8 File Offset: 0x00008BC8
		private void WriteLeLong(long value)
		{
			this.WriteLeInt((int)value);
			this.WriteLeInt((int)(value >> 32));
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000A9DD File Offset: 0x00008BDD
		private void TransformEntryName(ZipEntry entry)
		{
			if (this.NameTransform == null)
			{
				return;
			}
			entry.Name = (entry.IsDirectory ? this.NameTransform.TransformDirectory(entry.Name) : this.NameTransform.TransformFile(entry.Name));
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000AA1A File Offset: 0x00008C1A
		public void PutNextEntry(ZipEntry entry)
		{
			if (this.curEntry != null)
			{
				this.CloseEntry();
			}
			this.PutNextEntry(this.baseOutputStream_, entry, 0L, false);
			if (entry.IsCrypted)
			{
				this.WriteOutput(this.GetEntryEncryptionHeader(entry));
			}
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000AA50 File Offset: 0x00008C50
		public void PutNextPassthroughEntry(ZipEntry entry)
		{
			if (this.curEntry != null)
			{
				this.CloseEntry();
			}
			if (entry.Crc < 0L)
			{
				throw new ZipException("Crc must be set for passthrough entry");
			}
			if (entry.Size < 0L)
			{
				throw new ZipException("Size must be set for passthrough entry");
			}
			if (entry.CompressedSize < 0L)
			{
				throw new ZipException("CompressedSize must be set for passthrough entry");
			}
			if (entry.CompressionMethod != CompressionMethod.Deflated)
			{
				throw new NotImplementedException("Only Deflated entries are supported for passthrough");
			}
			if (!string.IsNullOrEmpty(this.Password))
			{
				throw new NotImplementedException("Encrypted passthrough entries are not supported");
			}
			this.PutNextEntry(this.baseOutputStream_, entry, 0L, true);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000AAE6 File Offset: 0x00008CE6
		private void WriteOutput(byte[] bytes)
		{
			this.baseOutputStream_.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000AAF8 File Offset: 0x00008CF8
		private Task WriteOutputAsync(byte[] bytes)
		{
			return this.baseOutputStream_.WriteAsync(bytes, 0, bytes.Length);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000AB0A File Offset: 0x00008D0A
		private byte[] GetEntryEncryptionHeader(ZipEntry entry)
		{
			if (entry.AESKeySize <= 0)
			{
				return this.CreateZipCryptoHeader((entry.Crc < 0L) ? (entry.DosTime << 16) : entry.Crc);
			}
			return this.InitializeAESPassword(entry, this.Password);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000AB44 File Offset: 0x00008D44
		internal void PutNextEntry(Stream stream, ZipEntry entry, long streamOffset = 0L, bool passthroughEntry = false)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			if (this.entries == null)
			{
				throw new InvalidOperationException("ZipOutputStream was finished");
			}
			if (this.entries.Count == 2147483647)
			{
				throw new ZipException("Too many entries for Zip file");
			}
			CompressionMethod compressionMethod = entry.CompressionMethod;
			if (compressionMethod != CompressionMethod.Deflated && compressionMethod != CompressionMethod.Stored)
			{
				throw new NotImplementedException("Compression method not supported");
			}
			if (entry.AESKeySize > 0 && string.IsNullOrEmpty(this.Password))
			{
				throw new InvalidOperationException("The Password property must be set before AES encrypted entries can be added");
			}
			this.entryIsPassthrough = passthroughEntry;
			int num = this.defaultCompressionLevel;
			entry.Flags &= 2048;
			this.patchEntryHeader = false;
			bool flag;
			if (entry.Size == 0L && !this.entryIsPassthrough)
			{
				entry.CompressedSize = entry.Size;
				entry.Crc = 0L;
				compressionMethod = CompressionMethod.Stored;
				flag = true;
			}
			else
			{
				flag = entry.Size >= 0L && entry.HasCrc && entry.CompressedSize >= 0L;
				if (compressionMethod == CompressionMethod.Stored)
				{
					if (!flag)
					{
						if (!base.CanPatchEntries)
						{
							compressionMethod = CompressionMethod.Deflated;
							num = 0;
						}
					}
					else
					{
						entry.CompressedSize = entry.Size;
						flag = entry.HasCrc;
					}
				}
			}
			if (!flag)
			{
				if (!base.CanPatchEntries)
				{
					entry.Flags |= 8;
				}
				else
				{
					this.patchEntryHeader = true;
				}
			}
			if (this.Password != null)
			{
				entry.IsCrypted = true;
				if (entry.Crc < 0L)
				{
					entry.Flags |= 8;
				}
			}
			entry.Offset = this.offset;
			entry.CompressionMethod = compressionMethod;
			this.curMethod = compressionMethod;
			if (this.useZip64_ == UseZip64.On || (entry.Size < 0L && this.useZip64_ == UseZip64.Dynamic))
			{
				entry.ForceZip64();
			}
			this.TransformEntryName(entry);
			EntryPatchData entryPatchData;
			this.offset += (long)ZipFormat.WriteLocalHeader(stream, entry, out entryPatchData, flag, this.patchEntryHeader, streamOffset, this._stringCodec);
			this.patchData = entryPatchData;
			if (entry.AESKeySize > 0)
			{
				this.offset += (long)entry.AESOverheadSize;
			}
			this.curEntry = entry;
			this.size = 0L;
			if (this.entryIsPassthrough)
			{
				return;
			}
			this.crc.Reset();
			if (compressionMethod == CompressionMethod.Deflated)
			{
				this.deflater_.Reset();
				this.deflater_.SetLevel(num);
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000AD78 File Offset: 0x00008F78
		public async Task PutNextEntryAsync(ZipEntry entry, CancellationToken ct = default(CancellationToken))
		{
			if (this.curEntry != null)
			{
				await this.CloseEntryAsync(ct).ConfigureAwait(false);
			}
			long position = (base.CanPatchEntries ? this.baseOutputStream_.Position : (-1L));
			await this.baseOutputStream_.WriteProcToStreamAsync(delegate(Stream s)
			{
				this.PutNextEntry(s, entry, position, false);
			}, ct).ConfigureAwait(false);
			if (entry.IsCrypted)
			{
				await this.WriteOutputAsync(this.GetEntryEncryptionHeader(entry)).ConfigureAwait(false);
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000ADCC File Offset: 0x00008FCC
		public void CloseEntry()
		{
			this.FinishCompressionSyncOrAsync(null).GetAwaiter().GetResult();
			this.WriteEntryFooter(this.baseOutputStream_);
			if (this.patchEntryHeader)
			{
				this.patchEntryHeader = false;
				ZipFormat.PatchLocalHeaderSync(this.baseOutputStream_, this.curEntry, this.patchData);
			}
			this.entries.Add(this.curEntry);
			this.curEntry = null;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000AE40 File Offset: 0x00009040
		private async Task FinishCompressionSyncOrAsync(CancellationToken? ct)
		{
			if (!this.entryIsPassthrough)
			{
				if (this.curMethod == CompressionMethod.Deflated)
				{
					if (this.size >= 0L)
					{
						if (ct != null)
						{
							await base.FinishAsync(ct.Value).ConfigureAwait(false);
						}
						else
						{
							base.Finish();
						}
					}
					else
					{
						this.deflater_.Reset();
					}
				}
				if (this.curMethod == CompressionMethod.Stored)
				{
					base.GetAuthCodeIfAES();
				}
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000AE8C File Offset: 0x0000908C
		public async Task CloseEntryAsync(CancellationToken ct)
		{
			await this.FinishCompressionSyncOrAsync(new CancellationToken?(ct)).ConfigureAwait(false);
			await this.baseOutputStream_.WriteProcToStreamAsync(new Action<Stream>(this.WriteEntryFooter), ct).ConfigureAwait(false);
			if (this.patchEntryHeader)
			{
				this.patchEntryHeader = false;
				await ZipFormat.PatchLocalHeaderAsync(this.baseOutputStream_, this.curEntry, this.patchData, ct).ConfigureAwait(false);
			}
			this.entries.Add(this.curEntry);
			this.curEntry = null;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000AED8 File Offset: 0x000090D8
		internal void WriteEntryFooter(Stream stream)
		{
			if (this.curEntry == null)
			{
				throw new InvalidOperationException("No open entry");
			}
			if (!this.entryIsPassthrough)
			{
				long totalOut = this.size;
				if (this.curMethod == CompressionMethod.Deflated && this.size >= 0L)
				{
					totalOut = this.deflater_.TotalOut;
				}
				if (this.curEntry.AESKeySize > 0)
				{
					stream.Write(this.AESAuthCode, 0, 10);
					this.curEntry.Crc = 0L;
				}
				else if (this.curEntry.Crc < 0L)
				{
					this.curEntry.Crc = this.crc.Value;
				}
				else if (this.curEntry.Crc != this.crc.Value)
				{
					throw new ZipException(string.Format("crc was {0}, but {1} was expected", this.crc.Value, this.curEntry.Crc));
				}
				if (this.curEntry.Size < 0L)
				{
					this.curEntry.Size = this.size;
				}
				else if (this.curEntry.Size != this.size)
				{
					throw new ZipException(string.Format("size was {0}, but {1} was expected", this.size, this.curEntry.Size));
				}
				if (this.curEntry.CompressedSize < 0L)
				{
					this.curEntry.CompressedSize = totalOut;
				}
				else if (this.curEntry.CompressedSize != totalOut)
				{
					throw new ZipException(string.Format("compressed size was {0}, but {1} expected", totalOut, this.curEntry.CompressedSize));
				}
				this.offset += totalOut;
				if (this.curEntry.IsCrypted)
				{
					this.curEntry.CompressedSize += (long)this.curEntry.EncryptionOverheadSize;
				}
				if ((this.curEntry.Flags & 8) != 0)
				{
					stream.WriteLEInt(134695760);
					stream.WriteLEInt((int)this.curEntry.Crc);
					if (this.curEntry.LocalHeaderRequiresZip64)
					{
						stream.WriteLELong(this.curEntry.CompressedSize);
						stream.WriteLELong(this.curEntry.Size);
						this.offset += 24L;
						return;
					}
					stream.WriteLEInt((int)this.curEntry.CompressedSize);
					stream.WriteLEInt((int)this.curEntry.Size);
					this.offset += 16L;
				}
				return;
			}
			if (this.curEntry.CompressedSize != this.size)
			{
				throw new ZipException(string.Format("compressed size was {0}, but {1} expected", this.size, this.curEntry.CompressedSize));
			}
			this.offset += this.size;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000B1A0 File Offset: 0x000093A0
		protected byte[] InitializeAESPassword(ZipEntry entry, string rawPassword)
		{
			byte[] array = new byte[entry.AESSaltLen];
			if (ZipOutputStream._aesRnd == null)
			{
				ZipOutputStream._aesRnd = RandomNumberGenerator.Create();
			}
			ZipOutputStream._aesRnd.GetBytes(array);
			int num = entry.AESKeySize / 8;
			this.cryptoTransform_ = new ZipAESTransform(rawPassword, array, num, true);
			byte[] array2 = new byte[array.Length + 2];
			Array.Copy(array, array2, array.Length);
			Array.Copy(((ZipAESTransform)this.cryptoTransform_).PwdVerifier, 0, array2, array2.Length - 2, 2);
			return array2;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000B220 File Offset: 0x00009420
		private byte[] CreateZipCryptoHeader(long crcValue)
		{
			this.offset += 12L;
			this.InitializeZipCryptoPassword(this.Password);
			byte[] array = new byte[12];
			using (RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create())
			{
				randomNumberGenerator.GetBytes(array);
			}
			array[11] = (byte)(crcValue >> 24);
			base.EncryptBlock(array, 0, array.Length);
			return array;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000B290 File Offset: 0x00009490
		private void InitializeZipCryptoPassword(string password)
		{
			PkzipClassicManaged pkzipClassicManaged = new PkzipClassicManaged();
			byte[] array = PkzipClassic.GenerateKeys(base.ZipCryptoEncoding.GetBytes(password));
			this.cryptoTransform_ = pkzipClassicManaged.CreateEncryptor(array, null);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000B2C4 File Offset: 0x000094C4
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.WriteSyncOrAsync(buffer, offset, count, null).GetAwaiter().GetResult();
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000B2F0 File Offset: 0x000094F0
		public override async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken ct)
		{
			await this.WriteSyncOrAsync(buffer, offset, count, new CancellationToken?(ct)).ConfigureAwait(false);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000B354 File Offset: 0x00009554
		private async Task WriteSyncOrAsync(byte[] buffer, int offset, int count, CancellationToken? ct)
		{
			if (this.curEntry == null)
			{
				throw new InvalidOperationException("No open entry.");
			}
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
			if (this.curEntry.AESKeySize == 0 && !this.entryIsPassthrough)
			{
				this.crc.Update(new ArraySegment<byte>(buffer, offset, count));
			}
			this.size += (long)count;
			if (this.curMethod == CompressionMethod.Stored || this.entryIsPassthrough)
			{
				if (this.Password != null)
				{
					this.CopyAndEncrypt(buffer, offset, count);
				}
				else if (ct != null)
				{
					await this.baseOutputStream_.WriteAsync(buffer, offset, count, ct.Value).ConfigureAwait(false);
				}
				else
				{
					this.baseOutputStream_.Write(buffer, offset, count);
				}
			}
			else if (ct != null)
			{
				await base.WriteAsync(buffer, offset, count, ct.Value).ConfigureAwait(false);
			}
			else
			{
				base.Write(buffer, offset, count);
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000B3B8 File Offset: 0x000095B8
		private void CopyAndEncrypt(byte[] buffer, int offset, int count)
		{
			byte[] array = new byte[4096];
			while (count > 0)
			{
				int num = ((count < 4096) ? count : 4096);
				Array.Copy(buffer, offset, array, 0, num);
				base.EncryptBlock(array, 0, num);
				this.baseOutputStream_.Write(array, 0, num);
				count -= num;
				offset += num;
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000B414 File Offset: 0x00009614
		public override void Finish()
		{
			if (this.entries == null)
			{
				return;
			}
			if (this.curEntry != null)
			{
				this.CloseEntry();
			}
			long num = (long)this.entries.Count;
			long num2 = 0L;
			foreach (ZipEntry zipEntry in this.entries)
			{
				num2 += (long)ZipFormat.WriteEndEntry(this.baseOutputStream_, zipEntry, this._stringCodec);
			}
			ZipFormat.WriteEndOfCentralDirectory(this.baseOutputStream_, num, num2, this.offset, this.zipComment);
			this.entries = null;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000B4C0 File Offset: 0x000096C0
		public override async Task FinishAsync(CancellationToken ct)
		{
			using (MemoryStream ms = new MemoryStream())
			{
				if (this.entries == null)
				{
					return;
				}
				if (this.curEntry != null)
				{
					await this.CloseEntryAsync(ct).ConfigureAwait(false);
				}
				long numEntries = (long)this.entries.Count;
				long sizeEntries = 0L;
				using (List<ZipEntry>.Enumerator enumerator = this.entries.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ZipEntry entry = enumerator.Current;
						await this.baseOutputStream_.WriteProcToStreamAsync(ms, delegate(Stream s)
						{
							sizeEntries += (long)ZipFormat.WriteEndEntry(s, entry, this._stringCodec);
						}, ct).ConfigureAwait(false);
					}
				}
				List<ZipEntry>.Enumerator enumerator = default(List<ZipEntry>.Enumerator);
				await this.baseOutputStream_.WriteProcToStreamAsync(ms, delegate(Stream s)
				{
					ZipFormat.WriteEndOfCentralDirectory(s, numEntries, sizeEntries, this.offset, this.zipComment);
				}, ct).ConfigureAwait(false);
				this.entries = null;
			}
			MemoryStream ms = null;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000B50B File Offset: 0x0000970B
		public override void Flush()
		{
			if (this.curMethod == CompressionMethod.Stored)
			{
				this.baseOutputStream_.Flush();
				return;
			}
			base.Flush();
		}

		// Token: 0x04000152 RID: 338
		private List<ZipEntry> entries = new List<ZipEntry>();

		// Token: 0x04000153 RID: 339
		private Crc32 crc = new Crc32();

		// Token: 0x04000154 RID: 340
		private ZipEntry curEntry;

		// Token: 0x04000155 RID: 341
		private bool entryIsPassthrough;

		// Token: 0x04000156 RID: 342
		private int defaultCompressionLevel = -1;

		// Token: 0x04000157 RID: 343
		private CompressionMethod curMethod = CompressionMethod.Deflated;

		// Token: 0x04000158 RID: 344
		private long size;

		// Token: 0x04000159 RID: 345
		private long offset;

		// Token: 0x0400015A RID: 346
		private byte[] zipComment = Empty.Array<byte>();

		// Token: 0x0400015B RID: 347
		private bool patchEntryHeader;

		// Token: 0x0400015C RID: 348
		private EntryPatchData patchData;

		// Token: 0x0400015D RID: 349
		private UseZip64 useZip64_ = UseZip64.Dynamic;

		// Token: 0x0400015E RID: 350
		private string password;

		// Token: 0x0400015F RID: 351
		private static RandomNumberGenerator _aesRnd = RandomNumberGenerator.Create();
	}
}
