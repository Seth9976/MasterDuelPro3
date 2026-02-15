using System;
using System.Collections.Generic;
using System.Text;

namespace System.IO.Compression
{
	/// <summary>Represents a compressed file within a zip archive.</summary>
	// Token: 0x0200001C RID: 28
	public class ZipArchiveEntry
	{
		// Token: 0x060000CF RID: 207 RVA: 0x00005988 File Offset: 0x00003B88
		internal ZipArchiveEntry(ZipArchive archive, ZipCentralDirectoryFileHeader cd)
		{
			this._archive = archive;
			this._originallyInArchive = true;
			this._diskNumberStart = cd.DiskNumberStart;
			this._versionMadeByPlatform = (ZipVersionMadeByPlatform)cd.VersionMadeByCompatibility;
			this._versionMadeBySpecification = (ZipVersionNeededValues)cd.VersionMadeBySpecification;
			this._versionToExtract = (ZipVersionNeededValues)cd.VersionNeededToExtract;
			this._generalPurposeBitFlag = (ZipArchiveEntry.BitFlagValues)cd.GeneralPurposeBitFlag;
			this.CompressionMethod = (ZipArchiveEntry.CompressionMethodValues)cd.CompressionMethod;
			this._lastModified = new DateTimeOffset(ZipHelper.DosTimeToDateTime(cd.LastModified));
			this._compressedSize = cd.CompressedSize;
			this._uncompressedSize = cd.UncompressedSize;
			this._externalFileAttr = cd.ExternalFileAttributes;
			this._offsetOfLocalHeader = cd.RelativeOffsetOfLocalHeader;
			this._storedOffsetOfCompressedData = null;
			this._crc32 = cd.Crc32;
			this._compressedBytes = null;
			this._storedUncompressedData = null;
			this._currentlyOpenForWrite = false;
			this._everOpenedForWrite = false;
			this._outstandingWriteStream = null;
			this.FullName = this.DecodeEntryName(cd.Filename);
			this._lhUnknownExtraFields = null;
			this._cdUnknownExtraFields = cd.ExtraFields;
			this._fileComment = cd.FileComment;
			this._compressionLevel = null;
		}

		/// <summary>Gets the relative path of the entry in the zip archive.</summary>
		/// <returns>The relative path of the entry in the zip archive.</returns>
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00005AAF File Offset: 0x00003CAF
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00005AB8 File Offset: 0x00003CB8
		public string FullName
		{
			get
			{
				return this._storedEntryName;
			}
			private set
			{
				if (value == null)
				{
					throw new ArgumentNullException("FullName");
				}
				bool flag;
				this._storedEntryNameBytes = this.EncodeEntryName(value, out flag);
				this._storedEntryName = value;
				if (flag)
				{
					this._generalPurposeBitFlag |= ZipArchiveEntry.BitFlagValues.UnicodeFileName;
				}
				else
				{
					this._generalPurposeBitFlag &= ~ZipArchiveEntry.BitFlagValues.UnicodeFileName;
				}
				if (ZipArchiveEntry.ParseFileName(value, this._versionMadeByPlatform) == "")
				{
					this.VersionToExtractAtLeast(ZipVersionNeededValues.ExplicitDirectory);
				}
			}
		}

		/// <summary>Gets the file name of the entry in the zip archive.</summary>
		/// <returns>The file name of the entry in the zip archive.</returns>
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00005B32 File Offset: 0x00003D32
		public string Name
		{
			get
			{
				return ZipArchiveEntry.ParseFileName(this.FullName, this._versionMadeByPlatform);
			}
		}

		/// <summary>Deletes the entry from the zip archive.</summary>
		/// <exception cref="T:System.IO.IOException">The entry is already open for reading or writing.</exception>
		/// <exception cref="T:System.NotSupportedException">The zip archive for this entry was opened in a mode other than <see cref="F:System.IO.Compression.ZipArchiveMode.Update" />. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The zip archive for this entry has been disposed.</exception>
		// Token: 0x060000D3 RID: 211 RVA: 0x00005B48 File Offset: 0x00003D48
		public void Delete()
		{
			if (this._archive == null)
			{
				return;
			}
			if (this._currentlyOpenForWrite)
			{
				throw new IOException("Cannot delete an entry currently open for writing.");
			}
			if (this._archive.Mode != ZipArchiveMode.Update)
			{
				throw new NotSupportedException("Delete can only be used when the archive is in Update mode.");
			}
			this._archive.ThrowIfDisposed();
			this._archive.RemoveEntry(this);
			this._archive = null;
			this.UnloadStreams();
		}

		/// <summary>Opens the entry from the zip archive.</summary>
		/// <returns>The stream that represents the contents of the entry.</returns>
		/// <exception cref="T:System.IO.IOException">The entry is already currently open for writing.-or-The entry has been deleted from the archive.-or-The archive for this entry was opened with the <see cref="F:System.IO.Compression.ZipArchiveMode.Create" /> mode, and this entry has already been written to. </exception>
		/// <exception cref="T:System.IO.InvalidDataException">The entry is either missing from the archive or is corrupt and cannot be read. -or-The entry has been compressed by using a compression method that is not supported.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The zip archive for this entry has been disposed.</exception>
		// Token: 0x060000D4 RID: 212 RVA: 0x00005BB0 File Offset: 0x00003DB0
		public Stream Open()
		{
			this.ThrowIfInvalidArchive();
			switch (this._archive.Mode)
			{
			case ZipArchiveMode.Read:
				return this.OpenInReadMode(true);
			case ZipArchiveMode.Create:
				return this.OpenInWriteMode();
			}
			return this.OpenInUpdateMode();
		}

		/// <summary>Retrieves the relative path of the entry in the zip archive.</summary>
		/// <returns>The relative path of the entry, which is the value stored in the <see cref="P:System.IO.Compression.ZipArchiveEntry.FullName" /> property.</returns>
		// Token: 0x060000D5 RID: 213 RVA: 0x00005BF8 File Offset: 0x00003DF8
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00005C00 File Offset: 0x00003E00
		private long OffsetOfCompressedData
		{
			get
			{
				if (this._storedOffsetOfCompressedData == null)
				{
					this._archive.ArchiveStream.Seek(this._offsetOfLocalHeader, SeekOrigin.Begin);
					if (!ZipLocalFileHeader.TrySkipBlock(this._archive.ArchiveReader))
					{
						throw new InvalidDataException("A local file header is corrupt.");
					}
					this._storedOffsetOfCompressedData = new long?(this._archive.ArchiveStream.Position);
				}
				return this._storedOffsetOfCompressedData.Value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00005C78 File Offset: 0x00003E78
		private MemoryStream UncompressedData
		{
			get
			{
				if (this._storedUncompressedData == null)
				{
					this._storedUncompressedData = new MemoryStream((int)this._uncompressedSize);
					if (this._originallyInArchive)
					{
						using (Stream stream = this.OpenInReadMode(false))
						{
							try
							{
								stream.CopyTo(this._storedUncompressedData);
							}
							catch (InvalidDataException)
							{
								this._storedUncompressedData.Dispose();
								this._storedUncompressedData = null;
								this._currentlyOpenForWrite = false;
								this._everOpenedForWrite = false;
								throw;
							}
						}
					}
					this.CompressionMethod = ZipArchiveEntry.CompressionMethodValues.Deflate;
				}
				return this._storedUncompressedData;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00005D14 File Offset: 0x00003F14
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00005D1C File Offset: 0x00003F1C
		private ZipArchiveEntry.CompressionMethodValues CompressionMethod
		{
			get
			{
				return this._storedCompressionMethod;
			}
			set
			{
				if (value == ZipArchiveEntry.CompressionMethodValues.Deflate)
				{
					this.VersionToExtractAtLeast(ZipVersionNeededValues.ExplicitDirectory);
				}
				else if (value == ZipArchiveEntry.CompressionMethodValues.Deflate64)
				{
					this.VersionToExtractAtLeast(ZipVersionNeededValues.Deflate64);
				}
				this._storedCompressionMethod = value;
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00005D40 File Offset: 0x00003F40
		private string DecodeEntryName(byte[] entryNameBytes)
		{
			Encoding encoding;
			if ((this._generalPurposeBitFlag & ZipArchiveEntry.BitFlagValues.UnicodeFileName) == (ZipArchiveEntry.BitFlagValues)0)
			{
				encoding = ((this._archive == null) ? Encoding.UTF8 : (this._archive.EntryNameEncoding ?? Encoding.UTF8));
			}
			else
			{
				encoding = Encoding.UTF8;
			}
			return encoding.GetString(entryNameBytes);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00005D90 File Offset: 0x00003F90
		private byte[] EncodeEntryName(string entryName, out bool isUTF8)
		{
			Encoding encoding;
			if (this._archive != null && this._archive.EntryNameEncoding != null)
			{
				encoding = this._archive.EntryNameEncoding;
			}
			else
			{
				encoding = (ZipHelper.RequiresUnicode(entryName) ? Encoding.UTF8 : Encoding.ASCII);
			}
			isUTF8 = encoding.Equals(Encoding.UTF8);
			return encoding.GetBytes(entryName);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005DE9 File Offset: 0x00003FE9
		internal void WriteAndFinishLocalEntry()
		{
			this.CloseStreams();
			this.WriteLocalFileHeaderAndDataIfNeeded();
			this.UnloadStreams();
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005E00 File Offset: 0x00004000
		internal void WriteCentralDirectoryFileHeader()
		{
			BinaryWriter binaryWriter = new BinaryWriter(this._archive.ArchiveStream);
			Zip64ExtraField zip64ExtraField = default(Zip64ExtraField);
			bool flag = false;
			uint num;
			uint num2;
			if (this.SizesTooLarge())
			{
				flag = true;
				num = uint.MaxValue;
				num2 = uint.MaxValue;
				zip64ExtraField.CompressedSize = new long?(this._compressedSize);
				zip64ExtraField.UncompressedSize = new long?(this._uncompressedSize);
			}
			else
			{
				num = (uint)this._compressedSize;
				num2 = (uint)this._uncompressedSize;
			}
			uint num3;
			if (this._offsetOfLocalHeader > (long)((ulong)(-1)))
			{
				flag = true;
				num3 = uint.MaxValue;
				zip64ExtraField.LocalHeaderOffset = new long?(this._offsetOfLocalHeader);
			}
			else
			{
				num3 = (uint)this._offsetOfLocalHeader;
			}
			if (flag)
			{
				this.VersionToExtractAtLeast(ZipVersionNeededValues.Zip64);
			}
			int num4 = (int)(flag ? zip64ExtraField.TotalSize : 0) + ((this._cdUnknownExtraFields != null) ? ZipGenericExtraField.TotalSize(this._cdUnknownExtraFields) : 0);
			ushort num5;
			if (num4 > 65535)
			{
				num5 = (flag ? zip64ExtraField.TotalSize : 0);
				this._cdUnknownExtraFields = null;
			}
			else
			{
				num5 = (ushort)num4;
			}
			binaryWriter.Write(33639248U);
			binaryWriter.Write((byte)this._versionMadeBySpecification);
			binaryWriter.Write((byte)ZipArchiveEntry.CurrentZipPlatform);
			binaryWriter.Write((ushort)this._versionToExtract);
			binaryWriter.Write((ushort)this._generalPurposeBitFlag);
			binaryWriter.Write((ushort)this.CompressionMethod);
			binaryWriter.Write(ZipHelper.DateTimeToDosTime(this._lastModified.DateTime));
			binaryWriter.Write(this._crc32);
			binaryWriter.Write(num);
			binaryWriter.Write(num2);
			binaryWriter.Write((ushort)this._storedEntryNameBytes.Length);
			binaryWriter.Write(num5);
			binaryWriter.Write((this._fileComment != null) ? ((ushort)this._fileComment.Length) : 0);
			binaryWriter.Write(0);
			binaryWriter.Write(0);
			binaryWriter.Write(this._externalFileAttr);
			binaryWriter.Write(num3);
			binaryWriter.Write(this._storedEntryNameBytes);
			if (flag)
			{
				zip64ExtraField.WriteBlock(this._archive.ArchiveStream);
			}
			if (this._cdUnknownExtraFields != null)
			{
				ZipGenericExtraField.WriteAllBlocks(this._cdUnknownExtraFields, this._archive.ArchiveStream);
			}
			if (this._fileComment != null)
			{
				binaryWriter.Write(this._fileComment);
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00006014 File Offset: 0x00004214
		internal bool LoadLocalHeaderExtraFieldAndCompressedBytesIfNeeded()
		{
			if (this._originallyInArchive)
			{
				this._archive.ArchiveStream.Seek(this._offsetOfLocalHeader, SeekOrigin.Begin);
				this._lhUnknownExtraFields = ZipLocalFileHeader.GetExtraFields(this._archive.ArchiveReader);
			}
			if (!this._everOpenedForWrite && this._originallyInArchive)
			{
				this._compressedBytes = new byte[this._compressedSize / 2147483591L + 1L][];
				for (int i = 0; i < this._compressedBytes.Length - 1; i++)
				{
					this._compressedBytes[i] = new byte[2147483591];
				}
				this._compressedBytes[this._compressedBytes.Length - 1] = new byte[this._compressedSize % 2147483591L];
				this._archive.ArchiveStream.Seek(this.OffsetOfCompressedData, SeekOrigin.Begin);
				for (int j = 0; j < this._compressedBytes.Length - 1; j++)
				{
					ZipHelper.ReadBytes(this._archive.ArchiveStream, this._compressedBytes[j], 2147483591);
				}
				ZipHelper.ReadBytes(this._archive.ArchiveStream, this._compressedBytes[this._compressedBytes.Length - 1], (int)(this._compressedSize % 2147483591L));
			}
			return true;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00006150 File Offset: 0x00004350
		internal void ThrowIfNotOpenable(bool needToUncompress, bool needToLoadIntoMemory)
		{
			string text;
			if (!this.IsOpenable(needToUncompress, needToLoadIntoMemory, out text))
			{
				throw new InvalidDataException(text);
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00006170 File Offset: 0x00004370
		private CheckSumAndSizeWriteStream GetDataCompressor(Stream backingStream, bool leaveBackingStreamOpen, EventHandler onClose)
		{
			Stream stream = ((this._compressionLevel != null) ? new DeflateStream(backingStream, this._compressionLevel.Value, leaveBackingStreamOpen) : new DeflateStream(backingStream, CompressionMode.Compress, leaveBackingStreamOpen));
			bool flag = true;
			bool flag2 = leaveBackingStreamOpen && !flag;
			return new CheckSumAndSizeWriteStream(stream, backingStream, flag2, this, onClose, delegate(long initialPosition, long currentPosition, uint checkSum, Stream backing, ZipArchiveEntry thisRef, EventHandler closeHandler)
			{
				thisRef._crc32 = checkSum;
				thisRef._uncompressedSize = currentPosition;
				thisRef._compressedSize = backing.Position - initialPosition;
				if (closeHandler != null)
				{
					closeHandler(thisRef, EventArgs.Empty);
				}
			});
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000061DC File Offset: 0x000043DC
		private Stream GetDataDecompressor(Stream compressedStreamToRead)
		{
			ZipArchiveEntry.CompressionMethodValues compressionMethod = this.CompressionMethod;
			if (compressionMethod != ZipArchiveEntry.CompressionMethodValues.Stored)
			{
				if (compressionMethod == ZipArchiveEntry.CompressionMethodValues.Deflate)
				{
					return new DeflateStream(compressedStreamToRead, CompressionMode.Decompress);
				}
				if (compressionMethod == ZipArchiveEntry.CompressionMethodValues.Deflate64)
				{
					return new DeflateManagedStream(compressedStreamToRead, ZipArchiveEntry.CompressionMethodValues.Deflate64);
				}
			}
			return compressedStreamToRead;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00006218 File Offset: 0x00004418
		private Stream OpenInReadMode(bool checkOpenable)
		{
			if (checkOpenable)
			{
				this.ThrowIfNotOpenable(true, false);
			}
			Stream stream = new SubReadStream(this._archive.ArchiveStream, this.OffsetOfCompressedData, this._compressedSize);
			return this.GetDataDecompressor(stream);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00006254 File Offset: 0x00004454
		private Stream OpenInWriteMode()
		{
			if (this._everOpenedForWrite)
			{
				throw new IOException("Entries in create mode may only be written to once, and only one entry may be held open at a time.");
			}
			this._everOpenedForWrite = true;
			CheckSumAndSizeWriteStream dataCompressor = this.GetDataCompressor(this._archive.ArchiveStream, true, delegate(object o, EventArgs e)
			{
				ZipArchiveEntry zipArchiveEntry = (ZipArchiveEntry)o;
				zipArchiveEntry._archive.ReleaseArchiveStream(zipArchiveEntry);
				zipArchiveEntry._outstandingWriteStream = null;
			});
			this._outstandingWriteStream = new ZipArchiveEntry.DirectToArchiveWriterStream(dataCompressor, this);
			return new WrappedStream(this._outstandingWriteStream, true);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000062C8 File Offset: 0x000044C8
		private Stream OpenInUpdateMode()
		{
			if (this._currentlyOpenForWrite)
			{
				throw new IOException("Entries cannot be opened multiple times in Update mode.");
			}
			this.ThrowIfNotOpenable(true, true);
			this._everOpenedForWrite = true;
			this._currentlyOpenForWrite = true;
			this.UncompressedData.Seek(0L, SeekOrigin.Begin);
			return new WrappedStream(this.UncompressedData, this, delegate(ZipArchiveEntry thisRef)
			{
				thisRef._currentlyOpenForWrite = false;
			});
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00006338 File Offset: 0x00004538
		private bool IsOpenable(bool needToUncompress, bool needToLoadIntoMemory, out string message)
		{
			message = null;
			if (this._originallyInArchive)
			{
				if (needToUncompress && this.CompressionMethod != ZipArchiveEntry.CompressionMethodValues.Stored && this.CompressionMethod != ZipArchiveEntry.CompressionMethodValues.Deflate && this.CompressionMethod != ZipArchiveEntry.CompressionMethodValues.Deflate64)
				{
					ZipArchiveEntry.CompressionMethodValues compressionMethod = this.CompressionMethod;
					if (compressionMethod == ZipArchiveEntry.CompressionMethodValues.BZip2 || compressionMethod == ZipArchiveEntry.CompressionMethodValues.LZMA)
					{
						message = SR.Format("The archive entry was compressed using {0} and is not supported.", this.CompressionMethod.ToString());
					}
					else
					{
						message = "The archive entry was compressed using an unsupported compression method.";
					}
					return false;
				}
				if ((long)this._diskNumberStart != (long)((ulong)this._archive.NumberOfThisDisk))
				{
					message = "Split or spanned archives are not supported.";
					return false;
				}
				if (this._offsetOfLocalHeader > this._archive.ArchiveStream.Length)
				{
					message = "A local file header is corrupt.";
					return false;
				}
				this._archive.ArchiveStream.Seek(this._offsetOfLocalHeader, SeekOrigin.Begin);
				if (!ZipLocalFileHeader.TrySkipBlock(this._archive.ArchiveReader))
				{
					message = "A local file header is corrupt.";
					return false;
				}
				if (this.OffsetOfCompressedData + this._compressedSize > this._archive.ArchiveStream.Length)
				{
					message = "A local file header is corrupt.";
					return false;
				}
				if (needToLoadIntoMemory && this._compressedSize > 2147483647L && !ZipArchiveEntry.s_allowLargeZipArchiveEntriesInUpdateMode)
				{
					message = "Entries larger than 4GB are not supported in Update mode.";
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00006469 File Offset: 0x00004669
		private bool SizesTooLarge()
		{
			return this._compressedSize > (long)((ulong)(-1)) || this._uncompressedSize > (long)((ulong)(-1));
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00006484 File Offset: 0x00004684
		private bool WriteLocalFileHeader(bool isEmptyFile)
		{
			BinaryWriter binaryWriter = new BinaryWriter(this._archive.ArchiveStream);
			Zip64ExtraField zip64ExtraField = default(Zip64ExtraField);
			bool flag = false;
			uint num;
			uint num2;
			if (isEmptyFile)
			{
				this.CompressionMethod = ZipArchiveEntry.CompressionMethodValues.Stored;
				num = 0U;
				num2 = 0U;
			}
			else if (this._archive.Mode == ZipArchiveMode.Create && !this._archive.ArchiveStream.CanSeek && !isEmptyFile)
			{
				this._generalPurposeBitFlag |= ZipArchiveEntry.BitFlagValues.DataDescriptor;
				flag = false;
				num = 0U;
				num2 = 0U;
			}
			else if (this.SizesTooLarge())
			{
				flag = true;
				num = uint.MaxValue;
				num2 = uint.MaxValue;
				zip64ExtraField.CompressedSize = new long?(this._compressedSize);
				zip64ExtraField.UncompressedSize = new long?(this._uncompressedSize);
				this.VersionToExtractAtLeast(ZipVersionNeededValues.Zip64);
			}
			else
			{
				flag = false;
				num = (uint)this._compressedSize;
				num2 = (uint)this._uncompressedSize;
			}
			this._offsetOfLocalHeader = binaryWriter.BaseStream.Position;
			int num3 = (int)(flag ? zip64ExtraField.TotalSize : 0) + ((this._lhUnknownExtraFields != null) ? ZipGenericExtraField.TotalSize(this._lhUnknownExtraFields) : 0);
			ushort num4;
			if (num3 > 65535)
			{
				num4 = (flag ? zip64ExtraField.TotalSize : 0);
				this._lhUnknownExtraFields = null;
			}
			else
			{
				num4 = (ushort)num3;
			}
			binaryWriter.Write(67324752U);
			binaryWriter.Write((ushort)this._versionToExtract);
			binaryWriter.Write((ushort)this._generalPurposeBitFlag);
			binaryWriter.Write((ushort)this.CompressionMethod);
			binaryWriter.Write(ZipHelper.DateTimeToDosTime(this._lastModified.DateTime));
			binaryWriter.Write(this._crc32);
			binaryWriter.Write(num);
			binaryWriter.Write(num2);
			binaryWriter.Write((ushort)this._storedEntryNameBytes.Length);
			binaryWriter.Write(num4);
			binaryWriter.Write(this._storedEntryNameBytes);
			if (flag)
			{
				zip64ExtraField.WriteBlock(this._archive.ArchiveStream);
			}
			if (this._lhUnknownExtraFields != null)
			{
				ZipGenericExtraField.WriteAllBlocks(this._lhUnknownExtraFields, this._archive.ArchiveStream);
			}
			return flag;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00006660 File Offset: 0x00004860
		private void WriteLocalFileHeaderAndDataIfNeeded()
		{
			if (this._storedUncompressedData != null || this._compressedBytes != null)
			{
				if (this._storedUncompressedData != null)
				{
					this._uncompressedSize = this._storedUncompressedData.Length;
					using (Stream stream = new ZipArchiveEntry.DirectToArchiveWriterStream(this.GetDataCompressor(this._archive.ArchiveStream, true, null), this))
					{
						this._storedUncompressedData.Seek(0L, SeekOrigin.Begin);
						this._storedUncompressedData.CopyTo(stream);
						this._storedUncompressedData.Dispose();
						this._storedUncompressedData = null;
						return;
					}
				}
				if (this._uncompressedSize == 0L)
				{
					this.CompressionMethod = ZipArchiveEntry.CompressionMethodValues.Stored;
				}
				this.WriteLocalFileHeader(false);
				foreach (byte[] array in this._compressedBytes)
				{
					this._archive.ArchiveStream.Write(array, 0, array.Length);
				}
				return;
			}
			if (this._archive.Mode == ZipArchiveMode.Update || !this._everOpenedForWrite)
			{
				this._everOpenedForWrite = true;
				this.WriteLocalFileHeader(true);
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00006768 File Offset: 0x00004968
		private void WriteCrcAndSizesInLocalHeader(bool zip64HeaderUsed)
		{
			long position = this._archive.ArchiveStream.Position;
			BinaryWriter binaryWriter = new BinaryWriter(this._archive.ArchiveStream);
			bool flag = this.SizesTooLarge();
			bool flag2 = flag && !zip64HeaderUsed;
			uint num = (flag ? uint.MaxValue : ((uint)this._compressedSize));
			uint num2 = (flag ? uint.MaxValue : ((uint)this._uncompressedSize));
			if (flag2)
			{
				this._generalPurposeBitFlag |= ZipArchiveEntry.BitFlagValues.DataDescriptor;
				this._archive.ArchiveStream.Seek(this._offsetOfLocalHeader + 6L, SeekOrigin.Begin);
				binaryWriter.Write((ushort)this._generalPurposeBitFlag);
			}
			this._archive.ArchiveStream.Seek(this._offsetOfLocalHeader + 14L, SeekOrigin.Begin);
			if (!flag2)
			{
				binaryWriter.Write(this._crc32);
				binaryWriter.Write(num);
				binaryWriter.Write(num2);
			}
			else
			{
				binaryWriter.Write(0U);
				binaryWriter.Write(0U);
				binaryWriter.Write(0U);
			}
			if (zip64HeaderUsed)
			{
				this._archive.ArchiveStream.Seek(this._offsetOfLocalHeader + 30L + (long)this._storedEntryNameBytes.Length + 4L, SeekOrigin.Begin);
				binaryWriter.Write(this._uncompressedSize);
				binaryWriter.Write(this._compressedSize);
				this._archive.ArchiveStream.Seek(position, SeekOrigin.Begin);
			}
			this._archive.ArchiveStream.Seek(position, SeekOrigin.Begin);
			if (flag2)
			{
				binaryWriter.Write(this._crc32);
				binaryWriter.Write(this._compressedSize);
				binaryWriter.Write(this._uncompressedSize);
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000068E0 File Offset: 0x00004AE0
		private void WriteDataDescriptor()
		{
			BinaryWriter binaryWriter = new BinaryWriter(this._archive.ArchiveStream);
			binaryWriter.Write(134695760U);
			binaryWriter.Write(this._crc32);
			if (this.SizesTooLarge())
			{
				binaryWriter.Write(this._compressedSize);
				binaryWriter.Write(this._uncompressedSize);
				return;
			}
			binaryWriter.Write((uint)this._compressedSize);
			binaryWriter.Write((uint)this._uncompressedSize);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00006950 File Offset: 0x00004B50
		private void UnloadStreams()
		{
			if (this._storedUncompressedData != null)
			{
				this._storedUncompressedData.Dispose();
			}
			this._compressedBytes = null;
			this._outstandingWriteStream = null;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00006973 File Offset: 0x00004B73
		private void CloseStreams()
		{
			if (this._outstandingWriteStream != null)
			{
				this._outstandingWriteStream.Dispose();
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00006988 File Offset: 0x00004B88
		private void VersionToExtractAtLeast(ZipVersionNeededValues value)
		{
			if (this._versionToExtract < value)
			{
				this._versionToExtract = value;
			}
			if (this._versionMadeBySpecification < value)
			{
				this._versionMadeBySpecification = value;
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000069AA File Offset: 0x00004BAA
		private void ThrowIfInvalidArchive()
		{
			if (this._archive == null)
			{
				throw new InvalidOperationException("Cannot modify deleted entry.");
			}
			this._archive.ThrowIfDisposed();
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000069CC File Offset: 0x00004BCC
		private static string GetFileName_Windows(string path)
		{
			int num = path.Length;
			while (--num >= 0)
			{
				char c = path[num];
				if (c == '\\' || c == '/' || c == ':')
				{
					return path.Substring(num + 1);
				}
			}
			return path;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00006A0C File Offset: 0x00004C0C
		private static string GetFileName_Unix(string path)
		{
			int num = path.Length;
			while (--num >= 0)
			{
				if (path[num] == '/')
				{
					return path.Substring(num + 1);
				}
			}
			return path;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00006A40 File Offset: 0x00004C40
		internal static string ParseFileName(string path, ZipVersionMadeByPlatform madeByPlatform)
		{
			if (madeByPlatform == ZipVersionMadeByPlatform.Windows)
			{
				return ZipArchiveEntry.GetFileName_Windows(path);
			}
			if (madeByPlatform != ZipVersionMadeByPlatform.Unix)
			{
				return ZipArchiveEntry.ParseFileName(path, ZipArchiveEntry.CurrentZipPlatform);
			}
			return ZipArchiveEntry.GetFileName_Unix(path);
		}

		// Token: 0x040000A9 RID: 169
		private ZipArchive _archive;

		// Token: 0x040000AA RID: 170
		private readonly bool _originallyInArchive;

		// Token: 0x040000AB RID: 171
		private readonly int _diskNumberStart;

		// Token: 0x040000AC RID: 172
		private readonly ZipVersionMadeByPlatform _versionMadeByPlatform;

		// Token: 0x040000AD RID: 173
		private ZipVersionNeededValues _versionMadeBySpecification;

		// Token: 0x040000AE RID: 174
		private ZipVersionNeededValues _versionToExtract;

		// Token: 0x040000AF RID: 175
		private ZipArchiveEntry.BitFlagValues _generalPurposeBitFlag;

		// Token: 0x040000B0 RID: 176
		private ZipArchiveEntry.CompressionMethodValues _storedCompressionMethod;

		// Token: 0x040000B1 RID: 177
		private DateTimeOffset _lastModified;

		// Token: 0x040000B2 RID: 178
		private long _compressedSize;

		// Token: 0x040000B3 RID: 179
		private long _uncompressedSize;

		// Token: 0x040000B4 RID: 180
		private long _offsetOfLocalHeader;

		// Token: 0x040000B5 RID: 181
		private long? _storedOffsetOfCompressedData;

		// Token: 0x040000B6 RID: 182
		private uint _crc32;

		// Token: 0x040000B7 RID: 183
		private byte[][] _compressedBytes;

		// Token: 0x040000B8 RID: 184
		private MemoryStream _storedUncompressedData;

		// Token: 0x040000B9 RID: 185
		private bool _currentlyOpenForWrite;

		// Token: 0x040000BA RID: 186
		private bool _everOpenedForWrite;

		// Token: 0x040000BB RID: 187
		private Stream _outstandingWriteStream;

		// Token: 0x040000BC RID: 188
		private uint _externalFileAttr;

		// Token: 0x040000BD RID: 189
		private string _storedEntryName;

		// Token: 0x040000BE RID: 190
		private byte[] _storedEntryNameBytes;

		// Token: 0x040000BF RID: 191
		private List<ZipGenericExtraField> _cdUnknownExtraFields;

		// Token: 0x040000C0 RID: 192
		private List<ZipGenericExtraField> _lhUnknownExtraFields;

		// Token: 0x040000C1 RID: 193
		private byte[] _fileComment;

		// Token: 0x040000C2 RID: 194
		private CompressionLevel? _compressionLevel;

		// Token: 0x040000C3 RID: 195
		private static readonly bool s_allowLargeZipArchiveEntriesInUpdateMode = IntPtr.Size > 4;

		// Token: 0x040000C4 RID: 196
		internal static readonly ZipVersionMadeByPlatform CurrentZipPlatform = ((Path.PathSeparator == '/') ? ZipVersionMadeByPlatform.Unix : ZipVersionMadeByPlatform.Windows);

		// Token: 0x0200001D RID: 29
		private sealed class DirectToArchiveWriterStream : Stream
		{
			// Token: 0x060000F3 RID: 243 RVA: 0x00006A85 File Offset: 0x00004C85
			public DirectToArchiveWriterStream(CheckSumAndSizeWriteStream crcSizeStream, ZipArchiveEntry entry)
			{
				this._position = 0L;
				this._crcSizeStream = crcSizeStream;
				this._everWritten = false;
				this._isDisposed = false;
				this._entry = entry;
				this._usedZip64inLH = false;
				this._canWrite = true;
			}

			// Token: 0x1700002F RID: 47
			// (get) Token: 0x060000F4 RID: 244 RVA: 0x00006ABF File Offset: 0x00004CBF
			public override long Length
			{
				get
				{
					this.ThrowIfDisposed();
					throw new NotSupportedException("This stream from ZipArchiveEntry does not support seeking.");
				}
			}

			// Token: 0x17000030 RID: 48
			// (get) Token: 0x060000F5 RID: 245 RVA: 0x00006AD1 File Offset: 0x00004CD1
			// (set) Token: 0x060000F6 RID: 246 RVA: 0x00006ABF File Offset: 0x00004CBF
			public override long Position
			{
				get
				{
					this.ThrowIfDisposed();
					return this._position;
				}
				set
				{
					this.ThrowIfDisposed();
					throw new NotSupportedException("This stream from ZipArchiveEntry does not support seeking.");
				}
			}

			// Token: 0x17000031 RID: 49
			// (get) Token: 0x060000F7 RID: 247 RVA: 0x00002273 File Offset: 0x00000473
			public override bool CanRead
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000032 RID: 50
			// (get) Token: 0x060000F8 RID: 248 RVA: 0x00002273 File Offset: 0x00000473
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000033 RID: 51
			// (get) Token: 0x060000F9 RID: 249 RVA: 0x00006ADF File Offset: 0x00004CDF
			public override bool CanWrite
			{
				get
				{
					return this._canWrite;
				}
			}

			// Token: 0x060000FA RID: 250 RVA: 0x00006AE7 File Offset: 0x00004CE7
			private void ThrowIfDisposed()
			{
				if (this._isDisposed)
				{
					throw new ObjectDisposedException(base.GetType().ToString(), "A stream from ZipArchiveEntry has been disposed.");
				}
			}

			// Token: 0x060000FB RID: 251 RVA: 0x00006B07 File Offset: 0x00004D07
			public override int Read(byte[] buffer, int offset, int count)
			{
				this.ThrowIfDisposed();
				throw new NotSupportedException("This stream from ZipArchiveEntry does not support reading.");
			}

			// Token: 0x060000FC RID: 252 RVA: 0x00006ABF File Offset: 0x00004CBF
			public override long Seek(long offset, SeekOrigin origin)
			{
				this.ThrowIfDisposed();
				throw new NotSupportedException("This stream from ZipArchiveEntry does not support seeking.");
			}

			// Token: 0x060000FD RID: 253 RVA: 0x00006B19 File Offset: 0x00004D19
			public override void SetLength(long value)
			{
				this.ThrowIfDisposed();
				throw new NotSupportedException("SetLength requires a stream that supports seeking and writing.");
			}

			// Token: 0x060000FE RID: 254 RVA: 0x00006B2C File Offset: 0x00004D2C
			public override void Write(byte[] buffer, int offset, int count)
			{
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer");
				}
				if (offset < 0)
				{
					throw new ArgumentOutOfRangeException("offset", "The argument must be non-negative.");
				}
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException("count", "The argument must be non-negative.");
				}
				if (buffer.Length - offset < count)
				{
					throw new ArgumentException("The offset and length parameters are not valid for the array that was given.");
				}
				this.ThrowIfDisposed();
				if (count == 0)
				{
					return;
				}
				if (!this._everWritten)
				{
					this._everWritten = true;
					this._usedZip64inLH = this._entry.WriteLocalFileHeader(false);
				}
				this._crcSizeStream.Write(buffer, offset, count);
				this._position += (long)count;
			}

			// Token: 0x060000FF RID: 255 RVA: 0x00006BCA File Offset: 0x00004DCA
			public override void Flush()
			{
				this.ThrowIfDisposed();
				this._crcSizeStream.Flush();
			}

			// Token: 0x06000100 RID: 256 RVA: 0x00006BE0 File Offset: 0x00004DE0
			protected override void Dispose(bool disposing)
			{
				if (disposing && !this._isDisposed)
				{
					this._crcSizeStream.Dispose();
					if (!this._everWritten)
					{
						this._entry.WriteLocalFileHeader(true);
					}
					else if (this._entry._archive.ArchiveStream.CanSeek)
					{
						this._entry.WriteCrcAndSizesInLocalHeader(this._usedZip64inLH);
					}
					else
					{
						this._entry.WriteDataDescriptor();
					}
					this._canWrite = false;
					this._isDisposed = true;
				}
				base.Dispose(disposing);
			}

			// Token: 0x040000C5 RID: 197
			private long _position;

			// Token: 0x040000C6 RID: 198
			private CheckSumAndSizeWriteStream _crcSizeStream;

			// Token: 0x040000C7 RID: 199
			private bool _everWritten;

			// Token: 0x040000C8 RID: 200
			private bool _isDisposed;

			// Token: 0x040000C9 RID: 201
			private ZipArchiveEntry _entry;

			// Token: 0x040000CA RID: 202
			private bool _usedZip64inLH;

			// Token: 0x040000CB RID: 203
			private bool _canWrite;
		}

		// Token: 0x0200001E RID: 30
		[Flags]
		private enum BitFlagValues : ushort
		{
			// Token: 0x040000CD RID: 205
			DataDescriptor = 8,
			// Token: 0x040000CE RID: 206
			UnicodeFileName = 2048
		}

		// Token: 0x0200001F RID: 31
		internal enum CompressionMethodValues : ushort
		{
			// Token: 0x040000D0 RID: 208
			Stored,
			// Token: 0x040000D1 RID: 209
			Deflate = 8,
			// Token: 0x040000D2 RID: 210
			Deflate64,
			// Token: 0x040000D3 RID: 211
			BZip2 = 12,
			// Token: 0x040000D4 RID: 212
			LZMA = 14
		}
	}
}
