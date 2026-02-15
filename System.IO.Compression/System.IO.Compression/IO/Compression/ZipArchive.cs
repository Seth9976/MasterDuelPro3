using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace System.IO.Compression
{
	/// <summary>Represents a package of compressed files in the zip archive format.</summary>
	// Token: 0x0200001B RID: 27
	public class ZipArchive : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Compression.ZipArchive" /> class from the specified stream and with the specified mode.</summary>
		/// <param name="stream">The input or output stream.</param>
		/// <param name="mode">One of the enumeration values that indicates whether the zip archive is used to read, create, or update entries.</param>
		/// <exception cref="T:System.ArgumentException">The stream is already closed, or the capabilities of the stream do not match the mode.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="mode" /> is an invalid value.</exception>
		/// <exception cref="T:System.IO.InvalidDataException">The contents of the stream could not be interpreted as a zip archive.-or-<paramref name="mode" /> is <see cref="F:System.IO.Compression.ZipArchiveMode.Update" /> and an entry is missing from the archive or is corrupt and cannot be read.-or-<paramref name="mode" /> is <see cref="F:System.IO.Compression.ZipArchiveMode.Update" /> and an entry is too large to fit into memory.</exception>
		// Token: 0x060000B8 RID: 184 RVA: 0x00005067 File Offset: 0x00003267
		public ZipArchive(Stream stream, ZipArchiveMode mode)
			: this(stream, mode, false, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Compression.ZipArchive" /> class on the specified stream for the specified mode, uses the specified encoding for entry names, and optionally leaves the stream open.</summary>
		/// <param name="stream">The input or output stream.</param>
		/// <param name="mode">One of the enumeration values that indicates whether the zip archive is used to read, create, or update entries.</param>
		/// <param name="leaveOpen">true to leave the stream open after the <see cref="T:System.IO.Compression.ZipArchive" /> object is disposed; otherwise, false.</param>
		/// <param name="entryNameEncoding">The encoding to use when reading or writing entry names in this archive. Specify a value for this parameter only when an encoding is required for interoperability with zip archive tools and libraries that do not support UTF-8 encoding for entry names.</param>
		/// <exception cref="T:System.ArgumentException">The stream is already closed, or the capabilities of the stream do not match the mode.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="mode" /> is an invalid value.</exception>
		/// <exception cref="T:System.IO.InvalidDataException">The contents of the stream could not be interpreted as a zip archive.-or-<paramref name="mode" /> is <see cref="F:System.IO.Compression.ZipArchiveMode.Update" /> and an entry is missing from the archive or is corrupt and cannot be read.-or-<paramref name="mode" /> is <see cref="F:System.IO.Compression.ZipArchiveMode.Update" /> and an entry is too large to fit into memory.</exception>
		// Token: 0x060000B9 RID: 185 RVA: 0x00005073 File Offset: 0x00003273
		public ZipArchive(Stream stream, ZipArchiveMode mode, bool leaveOpen, Encoding entryNameEncoding)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.EntryNameEncoding = entryNameEncoding;
			this.Init(stream, mode, leaveOpen);
		}

		/// <summary>Gets the collection of entries that are currently in the zip archive.</summary>
		/// <returns>The collection of entries that are currently in the zip archive.</returns>
		/// <exception cref="T:System.NotSupportedException">The zip archive does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The zip archive has been disposed.</exception>
		/// <exception cref="T:System.IO.InvalidDataException">The zip archive is corrupt, and its entries cannot be retrieved.</exception>
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000BA RID: 186 RVA: 0x0000509A File Offset: 0x0000329A
		public ReadOnlyCollection<ZipArchiveEntry> Entries
		{
			get
			{
				if (this._mode == ZipArchiveMode.Create)
				{
					throw new NotSupportedException("Cannot access entries in Create mode.");
				}
				this.ThrowIfDisposed();
				this.EnsureCentralDirectoryRead();
				return this._entriesCollection;
			}
		}

		/// <summary>Gets a value that describes the type of action the zip archive can perform on entries.</summary>
		/// <returns>One of the enumeration values that describes the type of action (read, create, or update) the zip archive can perform on entries.</returns>
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000050C2 File Offset: 0x000032C2
		public ZipArchiveMode Mode
		{
			get
			{
				return this._mode;
			}
		}

		/// <summary>Called by the <see cref="M:System.IO.Compression.ZipArchive.Dispose" /> and <see cref="M:System.Object.Finalize" /> methods to release the unmanaged resources used by the current instance of the <see cref="T:System.IO.Compression.ZipArchive" /> class, and optionally finishes writing the archive and releases the managed resources.</summary>
		/// <param name="disposing">true to finish writing the archive and release unmanaged and managed resources; false to release only unmanaged resources.</param>
		// Token: 0x060000BC RID: 188 RVA: 0x000050CC File Offset: 0x000032CC
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this._isDisposed)
			{
				try
				{
					ZipArchiveMode mode = this._mode;
					if (mode != ZipArchiveMode.Read)
					{
						int num = mode - ZipArchiveMode.Create;
						this.WriteFile();
					}
				}
				finally
				{
					this.CloseStreams();
					this._isDisposed = true;
				}
			}
		}

		/// <summary>Releases the resources used by the current instance of the <see cref="T:System.IO.Compression.ZipArchive" /> class.</summary>
		// Token: 0x060000BD RID: 189 RVA: 0x0000511C File Offset: 0x0000331C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Retrieves a wrapper for the specified entry in the zip archive.</summary>
		/// <returns>A wrapper for the specified entry in the archive; null if the entry does not exist in the archive.</returns>
		/// <param name="entryName">A path, relative to the root of the archive, that identifies the entry to retrieve.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="entryName" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="entryName" /> is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The zip archive does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The zip archive has been disposed.</exception>
		/// <exception cref="T:System.IO.InvalidDataException">The zip archive is corrupt, and its entries cannot be retrieved.</exception>
		// Token: 0x060000BE RID: 190 RVA: 0x0000512C File Offset: 0x0000332C
		public ZipArchiveEntry GetEntry(string entryName)
		{
			if (entryName == null)
			{
				throw new ArgumentNullException("entryName");
			}
			if (this._mode == ZipArchiveMode.Create)
			{
				throw new NotSupportedException("Cannot access entries in Create mode.");
			}
			this.EnsureCentralDirectoryRead();
			ZipArchiveEntry zipArchiveEntry;
			this._entriesDictionary.TryGetValue(entryName, out zipArchiveEntry);
			return zipArchiveEntry;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00005171 File Offset: 0x00003371
		internal BinaryReader ArchiveReader
		{
			get
			{
				return this._archiveReader;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00005179 File Offset: 0x00003379
		internal Stream ArchiveStream
		{
			get
			{
				return this._archiveStream;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00005181 File Offset: 0x00003381
		internal uint NumberOfThisDisk
		{
			get
			{
				return this._numberOfThisDisk;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00005189 File Offset: 0x00003389
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00005191 File Offset: 0x00003391
		internal Encoding EntryNameEncoding
		{
			get
			{
				return this._entryNameEncoding;
			}
			private set
			{
				if (value != null && (value.Equals(Encoding.BigEndianUnicode) || value.Equals(Encoding.Unicode)))
				{
					throw new ArgumentException("The specified entry name encoding is not supported.", "EntryNameEncoding");
				}
				this._entryNameEncoding = value;
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000051C8 File Offset: 0x000033C8
		private void AddEntry(ZipArchiveEntry entry)
		{
			this._entries.Add(entry);
			string fullName = entry.FullName;
			if (!this._entriesDictionary.ContainsKey(fullName))
			{
				this._entriesDictionary.Add(fullName, entry);
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00005203 File Offset: 0x00003403
		internal void ReleaseArchiveStream(ZipArchiveEntry entry)
		{
			this._archiveStreamOwner = null;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000520C File Offset: 0x0000340C
		internal void RemoveEntry(ZipArchiveEntry entry)
		{
			this._entries.Remove(entry);
			this._entriesDictionary.Remove(entry.FullName);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000522D File Offset: 0x0000342D
		internal void ThrowIfDisposed()
		{
			if (this._isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00005248 File Offset: 0x00003448
		private void CloseStreams()
		{
			if (this._leaveOpen)
			{
				if (this._backingStream != null)
				{
					this._archiveStream.Dispose();
				}
				return;
			}
			this._archiveStream.Dispose();
			Stream backingStream = this._backingStream;
			if (backingStream != null)
			{
				backingStream.Dispose();
			}
			BinaryReader archiveReader = this._archiveReader;
			if (archiveReader == null)
			{
				return;
			}
			archiveReader.Dispose();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000529D File Offset: 0x0000349D
		private void EnsureCentralDirectoryRead()
		{
			if (!this._readEntries)
			{
				this.ReadCentralDirectory();
				this._readEntries = true;
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000052B4 File Offset: 0x000034B4
		private void Init(Stream stream, ZipArchiveMode mode, bool leaveOpen)
		{
			Stream stream2 = null;
			try
			{
				this._backingStream = null;
				switch (mode)
				{
				case ZipArchiveMode.Read:
					if (!stream.CanRead)
					{
						throw new ArgumentException("Cannot use read mode on a non-readable stream.");
					}
					if (!stream.CanSeek)
					{
						this._backingStream = stream;
						stream = (stream2 = new MemoryStream());
						this._backingStream.CopyTo(stream);
						stream.Seek(0L, SeekOrigin.Begin);
					}
					break;
				case ZipArchiveMode.Create:
					if (!stream.CanWrite)
					{
						throw new ArgumentException("Cannot use create mode on a non-writable stream.");
					}
					break;
				case ZipArchiveMode.Update:
					if (!stream.CanRead || !stream.CanWrite || !stream.CanSeek)
					{
						throw new ArgumentException("Update mode requires a stream with read, write, and seek capabilities.");
					}
					break;
				default:
					throw new ArgumentOutOfRangeException("mode");
				}
				this._mode = mode;
				if (mode == ZipArchiveMode.Create && !stream.CanSeek)
				{
					this._archiveStream = new PositionPreservingWriteOnlyStreamWrapper(stream);
				}
				else
				{
					this._archiveStream = stream;
				}
				this._archiveStreamOwner = null;
				if (mode == ZipArchiveMode.Create)
				{
					this._archiveReader = null;
				}
				else
				{
					this._archiveReader = new BinaryReader(this._archiveStream);
				}
				this._entries = new List<ZipArchiveEntry>();
				this._entriesCollection = new ReadOnlyCollection<ZipArchiveEntry>(this._entries);
				this._entriesDictionary = new Dictionary<string, ZipArchiveEntry>();
				this._readEntries = false;
				this._leaveOpen = leaveOpen;
				this._centralDirectoryStart = 0L;
				this._isDisposed = false;
				this._numberOfThisDisk = 0U;
				this._archiveComment = null;
				switch (mode)
				{
				case ZipArchiveMode.Read:
					this.ReadEndOfCentralDirectory();
					goto IL_01BC;
				case ZipArchiveMode.Create:
					this._readEntries = true;
					goto IL_01BC;
				}
				if (this._archiveStream.Length == 0L)
				{
					this._readEntries = true;
				}
				else
				{
					this.ReadEndOfCentralDirectory();
					this.EnsureCentralDirectoryRead();
					foreach (ZipArchiveEntry zipArchiveEntry in this._entries)
					{
						zipArchiveEntry.ThrowIfNotOpenable(false, true);
					}
				}
				IL_01BC:;
			}
			catch
			{
				if (stream2 != null)
				{
					stream2.Dispose();
				}
				throw;
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000054C0 File Offset: 0x000036C0
		private void ReadCentralDirectory()
		{
			try
			{
				this._archiveStream.Seek(this._centralDirectoryStart, SeekOrigin.Begin);
				long num = 0L;
				bool flag = this.Mode == ZipArchiveMode.Update;
				ZipCentralDirectoryFileHeader zipCentralDirectoryFileHeader;
				while (ZipCentralDirectoryFileHeader.TryReadBlock(this._archiveReader, flag, out zipCentralDirectoryFileHeader))
				{
					this.AddEntry(new ZipArchiveEntry(this, zipCentralDirectoryFileHeader));
					num += 1L;
				}
				if (num != this._expectedNumberOfEntries)
				{
					throw new InvalidDataException("Number of entries expected in End Of Central Directory does not correspond to number of entries in Central Directory.");
				}
			}
			catch (EndOfStreamException ex)
			{
				throw new InvalidDataException(SR.Format("Central Directory is invalid.", ex));
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000554C File Offset: 0x0000374C
		private void ReadEndOfCentralDirectory()
		{
			try
			{
				this._archiveStream.Seek(-18L, SeekOrigin.End);
				if (!ZipHelper.SeekBackwardsToSignature(this._archiveStream, 101010256U))
				{
					throw new InvalidDataException("End of Central Directory record could not be found.");
				}
				long position = this._archiveStream.Position;
				ZipEndOfCentralDirectoryBlock zipEndOfCentralDirectoryBlock;
				ZipEndOfCentralDirectoryBlock.TryReadBlock(this._archiveReader, out zipEndOfCentralDirectoryBlock);
				if (zipEndOfCentralDirectoryBlock.NumberOfThisDisk != zipEndOfCentralDirectoryBlock.NumberOfTheDiskWithTheStartOfTheCentralDirectory)
				{
					throw new InvalidDataException("Split or spanned archives are not supported.");
				}
				this._numberOfThisDisk = (uint)zipEndOfCentralDirectoryBlock.NumberOfThisDisk;
				this._centralDirectoryStart = (long)((ulong)zipEndOfCentralDirectoryBlock.OffsetOfStartOfCentralDirectoryWithRespectToTheStartingDiskNumber);
				if (zipEndOfCentralDirectoryBlock.NumberOfEntriesInTheCentralDirectory != zipEndOfCentralDirectoryBlock.NumberOfEntriesInTheCentralDirectoryOnThisDisk)
				{
					throw new InvalidDataException("Split or spanned archives are not supported.");
				}
				this._expectedNumberOfEntries = (long)((ulong)zipEndOfCentralDirectoryBlock.NumberOfEntriesInTheCentralDirectory);
				if (this._mode == ZipArchiveMode.Update)
				{
					this._archiveComment = zipEndOfCentralDirectoryBlock.ArchiveComment;
				}
				if (zipEndOfCentralDirectoryBlock.NumberOfThisDisk == 65535 || zipEndOfCentralDirectoryBlock.OffsetOfStartOfCentralDirectoryWithRespectToTheStartingDiskNumber == 4294967295U || zipEndOfCentralDirectoryBlock.NumberOfEntriesInTheCentralDirectory == 65535)
				{
					this._archiveStream.Seek(position - 16L, SeekOrigin.Begin);
					if (ZipHelper.SeekBackwardsToSignature(this._archiveStream, 117853008U))
					{
						Zip64EndOfCentralDirectoryLocator zip64EndOfCentralDirectoryLocator;
						Zip64EndOfCentralDirectoryLocator.TryReadBlock(this._archiveReader, out zip64EndOfCentralDirectoryLocator);
						if (zip64EndOfCentralDirectoryLocator.OffsetOfZip64EOCD > 9223372036854775807UL)
						{
							throw new InvalidDataException("Offset to Zip64 End Of Central Directory record cannot be held in an Int64.");
						}
						long offsetOfZip64EOCD = (long)zip64EndOfCentralDirectoryLocator.OffsetOfZip64EOCD;
						this._archiveStream.Seek(offsetOfZip64EOCD, SeekOrigin.Begin);
						Zip64EndOfCentralDirectoryRecord zip64EndOfCentralDirectoryRecord;
						if (!Zip64EndOfCentralDirectoryRecord.TryReadBlock(this._archiveReader, out zip64EndOfCentralDirectoryRecord))
						{
							throw new InvalidDataException("Zip 64 End of Central Directory Record not where indicated.");
						}
						this._numberOfThisDisk = zip64EndOfCentralDirectoryRecord.NumberOfThisDisk;
						if (zip64EndOfCentralDirectoryRecord.NumberOfEntriesTotal > 9223372036854775807UL)
						{
							throw new InvalidDataException("Number of Entries cannot be held in an Int64.");
						}
						if (zip64EndOfCentralDirectoryRecord.OffsetOfCentralDirectory > 9223372036854775807UL)
						{
							throw new InvalidDataException("Offset to Central Directory cannot be held in an Int64.");
						}
						if (zip64EndOfCentralDirectoryRecord.NumberOfEntriesTotal != zip64EndOfCentralDirectoryRecord.NumberOfEntriesOnThisDisk)
						{
							throw new InvalidDataException("Split or spanned archives are not supported.");
						}
						this._expectedNumberOfEntries = (long)zip64EndOfCentralDirectoryRecord.NumberOfEntriesTotal;
						this._centralDirectoryStart = (long)zip64EndOfCentralDirectoryRecord.OffsetOfCentralDirectory;
					}
				}
				if (this._centralDirectoryStart > this._archiveStream.Length)
				{
					throw new InvalidDataException("Offset to Central Directory cannot be held in an Int64.");
				}
			}
			catch (EndOfStreamException ex)
			{
				throw new InvalidDataException("Central Directory corrupt.", ex);
			}
			catch (IOException ex2)
			{
				throw new InvalidDataException("Central Directory corrupt.", ex2);
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000057A4 File Offset: 0x000039A4
		private void WriteFile()
		{
			if (this._mode == ZipArchiveMode.Update)
			{
				List<ZipArchiveEntry> list = new List<ZipArchiveEntry>();
				foreach (ZipArchiveEntry zipArchiveEntry in this._entries)
				{
					if (!zipArchiveEntry.LoadLocalHeaderExtraFieldAndCompressedBytesIfNeeded())
					{
						list.Add(zipArchiveEntry);
					}
				}
				foreach (ZipArchiveEntry zipArchiveEntry2 in list)
				{
					zipArchiveEntry2.Delete();
				}
				this._archiveStream.Seek(0L, SeekOrigin.Begin);
				this._archiveStream.SetLength(0L);
			}
			foreach (ZipArchiveEntry zipArchiveEntry3 in this._entries)
			{
				zipArchiveEntry3.WriteAndFinishLocalEntry();
			}
			long position = this._archiveStream.Position;
			foreach (ZipArchiveEntry zipArchiveEntry4 in this._entries)
			{
				zipArchiveEntry4.WriteCentralDirectoryFileHeader();
			}
			long num = this._archiveStream.Position - position;
			this.WriteArchiveEpilogue(position, num);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000590C File Offset: 0x00003B0C
		private void WriteArchiveEpilogue(long startOfCentralDirectory, long sizeOfCentralDirectory)
		{
			if (startOfCentralDirectory >= (long)((ulong)(-1)) || sizeOfCentralDirectory >= (long)((ulong)(-1)) || this._entries.Count >= 65535)
			{
				long position = this._archiveStream.Position;
				Zip64EndOfCentralDirectoryRecord.WriteBlock(this._archiveStream, (long)this._entries.Count, startOfCentralDirectory, sizeOfCentralDirectory);
				Zip64EndOfCentralDirectoryLocator.WriteBlock(this._archiveStream, position);
			}
			ZipEndOfCentralDirectoryBlock.WriteBlock(this._archiveStream, (long)this._entries.Count, startOfCentralDirectory, sizeOfCentralDirectory, this._archiveComment);
		}

		// Token: 0x04000099 RID: 153
		private Stream _archiveStream;

		// Token: 0x0400009A RID: 154
		private ZipArchiveEntry _archiveStreamOwner;

		// Token: 0x0400009B RID: 155
		private BinaryReader _archiveReader;

		// Token: 0x0400009C RID: 156
		private ZipArchiveMode _mode;

		// Token: 0x0400009D RID: 157
		private List<ZipArchiveEntry> _entries;

		// Token: 0x0400009E RID: 158
		private ReadOnlyCollection<ZipArchiveEntry> _entriesCollection;

		// Token: 0x0400009F RID: 159
		private Dictionary<string, ZipArchiveEntry> _entriesDictionary;

		// Token: 0x040000A0 RID: 160
		private bool _readEntries;

		// Token: 0x040000A1 RID: 161
		private bool _leaveOpen;

		// Token: 0x040000A2 RID: 162
		private long _centralDirectoryStart;

		// Token: 0x040000A3 RID: 163
		private bool _isDisposed;

		// Token: 0x040000A4 RID: 164
		private uint _numberOfThisDisk;

		// Token: 0x040000A5 RID: 165
		private long _expectedNumberOfEntries;

		// Token: 0x040000A6 RID: 166
		private Stream _backingStream;

		// Token: 0x040000A7 RID: 167
		private byte[] _archiveComment;

		// Token: 0x040000A8 RID: 168
		private Encoding _entryNameEncoding;
	}
}
