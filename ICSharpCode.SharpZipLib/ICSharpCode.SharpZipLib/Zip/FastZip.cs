using System;
using System.Collections;
using System.IO;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip.Compression;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000008 RID: 8
	public class FastZip
	{
		// Token: 0x06000021 RID: 33 RVA: 0x000022E2 File Offset: 0x000004E2
		public FastZip()
		{
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002318 File Offset: 0x00000518
		public FastZip(ZipEntryFactory.TimeSetting timeSetting)
		{
			this.entryFactory_ = new ZipEntryFactory(timeSetting);
			this.restoreDateTimeOnExtract_ = true;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000236C File Offset: 0x0000056C
		public FastZip(DateTime time)
		{
			this.entryFactory_ = new ZipEntryFactory(time);
			this.restoreDateTimeOnExtract_ = true;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000023BD File Offset: 0x000005BD
		public FastZip(FastZipEvents events)
		{
			this.events_ = events;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000023F7 File Offset: 0x000005F7
		// (set) Token: 0x06000026 RID: 38 RVA: 0x000023FF File Offset: 0x000005FF
		public bool CreateEmptyDirectories
		{
			get
			{
				return this.createEmptyDirectories_;
			}
			set
			{
				this.createEmptyDirectories_ = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002408 File Offset: 0x00000608
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002410 File Offset: 0x00000610
		public string Password
		{
			get
			{
				return this.password_;
			}
			set
			{
				this.password_ = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002419 File Offset: 0x00000619
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002421 File Offset: 0x00000621
		public ZipEncryptionMethod EntryEncryptionMethod { get; set; } = ZipEncryptionMethod.ZipCrypto;

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000242A File Offset: 0x0000062A
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002437 File Offset: 0x00000637
		public INameTransform NameTransform
		{
			get
			{
				return this.entryFactory_.NameTransform;
			}
			set
			{
				this.entryFactory_.NameTransform = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002445 File Offset: 0x00000645
		// (set) Token: 0x0600002E RID: 46 RVA: 0x0000244D File Offset: 0x0000064D
		public IEntryFactory EntryFactory
		{
			get
			{
				return this.entryFactory_;
			}
			set
			{
				if (value == null)
				{
					this.entryFactory_ = new ZipEntryFactory();
					return;
				}
				this.entryFactory_ = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002465 File Offset: 0x00000665
		// (set) Token: 0x06000030 RID: 48 RVA: 0x0000246D File Offset: 0x0000066D
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

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002476 File Offset: 0x00000676
		// (set) Token: 0x06000032 RID: 50 RVA: 0x0000247E File Offset: 0x0000067E
		public bool RestoreDateTimeOnExtract
		{
			get
			{
				return this.restoreDateTimeOnExtract_;
			}
			set
			{
				this.restoreDateTimeOnExtract_ = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002487 File Offset: 0x00000687
		// (set) Token: 0x06000034 RID: 52 RVA: 0x0000248F File Offset: 0x0000068F
		public bool RestoreAttributesOnExtract
		{
			get
			{
				return this.restoreAttributesOnExtract_;
			}
			set
			{
				this.restoreAttributesOnExtract_ = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002498 File Offset: 0x00000698
		// (set) Token: 0x06000036 RID: 54 RVA: 0x000024A0 File Offset: 0x000006A0
		public Deflater.CompressionLevel CompressionLevel
		{
			get
			{
				return this.compressionLevel_;
			}
			set
			{
				this.compressionLevel_ = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000024A9 File Offset: 0x000006A9
		// (set) Token: 0x06000038 RID: 56 RVA: 0x000024B9 File Offset: 0x000006B9
		public bool UseUnicode
		{
			get
			{
				return !this._stringCodec.ForceZipLegacyEncoding;
			}
			set
			{
				this._stringCodec.ForceZipLegacyEncoding = !value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000024CA File Offset: 0x000006CA
		// (set) Token: 0x0600003A RID: 58 RVA: 0x000024D7 File Offset: 0x000006D7
		public int LegacyCodePage
		{
			get
			{
				return this._stringCodec.CodePage;
			}
			set
			{
				this._stringCodec = StringCodec.FromCodePage(value);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000024E5 File Offset: 0x000006E5
		// (set) Token: 0x0600003C RID: 60 RVA: 0x000024ED File Offset: 0x000006ED
		public StringCodec StringCodec
		{
			get
			{
				return this._stringCodec;
			}
			set
			{
				this._stringCodec = value;
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000024F6 File Offset: 0x000006F6
		public void CreateZip(string zipFileName, string sourceDirectory, bool recurse, string fileFilter, string directoryFilter)
		{
			this.CreateZip(File.Create(zipFileName), sourceDirectory, recurse, fileFilter, directoryFilter);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000250A File Offset: 0x0000070A
		public void CreateZip(string zipFileName, string sourceDirectory, bool recurse, string fileFilter)
		{
			this.CreateZip(File.Create(zipFileName), sourceDirectory, recurse, fileFilter, null);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000251D File Offset: 0x0000071D
		public void CreateZip(Stream outputStream, string sourceDirectory, bool recurse, string fileFilter, string directoryFilter)
		{
			this.CreateZip(outputStream, sourceDirectory, recurse, fileFilter, directoryFilter, false);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002530 File Offset: 0x00000730
		public void CreateZip(Stream outputStream, string sourceDirectory, bool recurse, string fileFilter, string directoryFilter, bool leaveOpen)
		{
			FileSystemScanner fileSystemScanner = new FileSystemScanner(fileFilter, directoryFilter);
			this.CreateZip(outputStream, sourceDirectory, recurse, fileSystemScanner, leaveOpen);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002553 File Offset: 0x00000753
		public void CreateZip(string zipFileName, string sourceDirectory, bool recurse, IScanFilter fileFilter, IScanFilter directoryFilter)
		{
			this.CreateZip(File.Create(zipFileName), sourceDirectory, recurse, fileFilter, directoryFilter, false);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002568 File Offset: 0x00000768
		public void CreateZip(Stream outputStream, string sourceDirectory, bool recurse, IScanFilter fileFilter, IScanFilter directoryFilter, bool leaveOpen = false)
		{
			FileSystemScanner fileSystemScanner = new FileSystemScanner(fileFilter, directoryFilter);
			this.CreateZip(outputStream, sourceDirectory, recurse, fileSystemScanner, leaveOpen);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000258C File Offset: 0x0000078C
		private void CreateZip(Stream outputStream, string sourceDirectory, bool recurse, FileSystemScanner scanner, bool leaveOpen)
		{
			this.NameTransform = new ZipNameTransform(sourceDirectory);
			this.sourceDirectory_ = sourceDirectory;
			using (this.outputStream_ = new ZipOutputStream(outputStream, this._stringCodec))
			{
				this.outputStream_.SetLevel((int)this.CompressionLevel);
				this.outputStream_.IsStreamOwner = !leaveOpen;
				this.outputStream_.NameTransform = null;
				if (!string.IsNullOrEmpty(this.password_) && this.EntryEncryptionMethod != ZipEncryptionMethod.None)
				{
					this.outputStream_.Password = this.password_;
				}
				this.outputStream_.UseZip64 = this.UseZip64;
				scanner.ProcessFile = (ProcessFileHandler)Delegate.Combine(scanner.ProcessFile, new ProcessFileHandler(this.ProcessFile));
				if (this.CreateEmptyDirectories)
				{
					scanner.ProcessDirectory += this.ProcessDirectory;
				}
				if (this.events_ != null)
				{
					if (this.events_.FileFailure != null)
					{
						scanner.FileFailure = (FileFailureHandler)Delegate.Combine(scanner.FileFailure, this.events_.FileFailure);
					}
					if (this.events_.DirectoryFailure != null)
					{
						scanner.DirectoryFailure = (DirectoryFailureHandler)Delegate.Combine(scanner.DirectoryFailure, this.events_.DirectoryFailure);
					}
				}
				scanner.Scan(sourceDirectory, recurse);
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000026FC File Offset: 0x000008FC
		public void ExtractZip(string zipFileName, string targetDirectory, string fileFilter)
		{
			this.ExtractZip(zipFileName, targetDirectory, FastZip.Overwrite.Always, null, fileFilter, null, this.restoreDateTimeOnExtract_, false);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000271C File Offset: 0x0000091C
		public void ExtractZip(string zipFileName, string targetDirectory, FastZip.Overwrite overwrite, FastZip.ConfirmOverwriteDelegate confirmDelegate, string fileFilter, string directoryFilter, bool restoreDateTime, bool allowParentTraversal = false)
		{
			Stream stream = File.Open(zipFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
			this.ExtractZip(stream, targetDirectory, overwrite, confirmDelegate, fileFilter, directoryFilter, restoreDateTime, true, allowParentTraversal);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002748 File Offset: 0x00000948
		public void ExtractZip(Stream inputStream, string targetDirectory, FastZip.Overwrite overwrite, FastZip.ConfirmOverwriteDelegate confirmDelegate, string fileFilter, string directoryFilter, bool restoreDateTime, bool isStreamOwner, bool allowParentTraversal = false)
		{
			if (overwrite == FastZip.Overwrite.Prompt && confirmDelegate == null)
			{
				throw new ArgumentNullException("confirmDelegate");
			}
			this.continueRunning_ = true;
			this.overwrite_ = overwrite;
			this.confirmDelegate_ = confirmDelegate;
			this.extractNameTransform_ = new WindowsNameTransform(targetDirectory, allowParentTraversal);
			this.fileFilter_ = new NameFilter(fileFilter);
			this.directoryFilter_ = new NameFilter(directoryFilter);
			this.restoreDateTimeOnExtract_ = restoreDateTime;
			using (this.zipFile_ = new ZipFile(inputStream, !isStreamOwner, this._stringCodec))
			{
				if (this.password_ != null)
				{
					this.zipFile_.Password = this.password_;
				}
				IEnumerator enumerator = this.zipFile_.GetEnumerator();
				while (this.continueRunning_ && enumerator.MoveNext())
				{
					ZipEntry zipEntry = (ZipEntry)enumerator.Current;
					if (zipEntry.IsFile)
					{
						if (this.directoryFilter_.IsMatch(Path.GetDirectoryName(zipEntry.Name)) && this.fileFilter_.IsMatch(zipEntry.Name))
						{
							this.ExtractEntry(zipEntry);
						}
					}
					else if (zipEntry.IsDirectory && this.directoryFilter_.IsMatch(zipEntry.Name) && this.CreateEmptyDirectories)
					{
						this.ExtractEntry(zipEntry);
					}
				}
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002890 File Offset: 0x00000A90
		private void ProcessDirectory(object sender, DirectoryEventArgs e)
		{
			if (!e.HasMatchingFiles && this.CreateEmptyDirectories)
			{
				if (this.events_ != null)
				{
					this.events_.OnProcessDirectory(e.Name, e.HasMatchingFiles);
				}
				if (e.ContinueRunning && e.Name != this.sourceDirectory_)
				{
					ZipEntry zipEntry = this.entryFactory_.MakeDirectoryEntry(e.Name);
					this.outputStream_.PutNextEntry(zipEntry);
				}
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002908 File Offset: 0x00000B08
		private void ProcessFile(object sender, ScanEventArgs e)
		{
			if (this.events_ != null && this.events_.ProcessFile != null)
			{
				this.events_.ProcessFile(sender, e);
			}
			if (e.ContinueRunning)
			{
				try
				{
					using (FileStream fileStream = File.Open(e.Name, FileMode.Open, FileAccess.Read, FileShare.Read))
					{
						ZipEntry zipEntry = this.entryFactory_.MakeFileEntry(e.Name);
						if (this._stringCodec.ForceZipLegacyEncoding)
						{
							zipEntry.IsUnicodeText = false;
						}
						this.ConfigureEntryEncryption(zipEntry);
						this.outputStream_.PutNextEntry(zipEntry);
						this.AddFileContents(e.Name, fileStream);
					}
				}
				catch (Exception ex)
				{
					if (this.events_ == null)
					{
						this.continueRunning_ = false;
						throw;
					}
					this.continueRunning_ = this.events_.OnFileFailure(e.Name, ex);
				}
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000029F4 File Offset: 0x00000BF4
		private void ConfigureEntryEncryption(ZipEntry entry)
		{
			if (!string.IsNullOrEmpty(this.Password) && entry.AESEncryptionStrength == 0)
			{
				ZipEncryptionMethod entryEncryptionMethod = this.EntryEncryptionMethod;
				if (entryEncryptionMethod == ZipEncryptionMethod.AES128)
				{
					entry.AESKeySize = 128;
					return;
				}
				if (entryEncryptionMethod != ZipEncryptionMethod.AES256)
				{
					return;
				}
				entry.AESKeySize = 256;
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002A40 File Offset: 0x00000C40
		private void AddFileContents(string name, Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (this.buffer_ == null)
			{
				this.buffer_ = new byte[4096];
			}
			if (this.events_ != null && this.events_.Progress != null)
			{
				StreamUtils.Copy(stream, this.outputStream_, this.buffer_, this.events_.Progress, this.events_.ProgressInterval, this, name);
			}
			else
			{
				StreamUtils.Copy(stream, this.outputStream_, this.buffer_);
			}
			if (this.events_ != null)
			{
				this.continueRunning_ = this.events_.OnCompletedFile(name);
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002AE0 File Offset: 0x00000CE0
		private void ExtractFileEntry(ZipEntry entry, string targetName)
		{
			bool flag = true;
			if (this.overwrite_ != FastZip.Overwrite.Always && File.Exists(targetName))
			{
				flag = this.overwrite_ == FastZip.Overwrite.Prompt && this.confirmDelegate_ != null && this.confirmDelegate_(targetName);
			}
			if (flag)
			{
				if (this.events_ != null)
				{
					this.continueRunning_ = this.events_.OnProcessFile(entry.Name);
				}
				if (this.continueRunning_)
				{
					try
					{
						using (FileStream fileStream = File.Create(targetName))
						{
							if (this.buffer_ == null)
							{
								this.buffer_ = new byte[4096];
							}
							using (Stream inputStream = this.zipFile_.GetInputStream(entry))
							{
								if (this.events_ != null && this.events_.Progress != null)
								{
									StreamUtils.Copy(inputStream, fileStream, this.buffer_, this.events_.Progress, this.events_.ProgressInterval, this, entry.Name, entry.Size);
								}
								else
								{
									StreamUtils.Copy(inputStream, fileStream, this.buffer_);
								}
							}
							if (this.events_ != null)
							{
								this.continueRunning_ = this.events_.OnCompletedFile(entry.Name);
							}
						}
						if (this.restoreDateTimeOnExtract_)
						{
							switch (this.entryFactory_.Setting)
							{
							case ZipEntryFactory.TimeSetting.LastWriteTime:
								File.SetLastWriteTime(targetName, entry.DateTime);
								break;
							case ZipEntryFactory.TimeSetting.LastWriteTimeUtc:
								File.SetLastWriteTimeUtc(targetName, entry.DateTime);
								break;
							case ZipEntryFactory.TimeSetting.CreateTime:
								File.SetCreationTime(targetName, entry.DateTime);
								break;
							case ZipEntryFactory.TimeSetting.CreateTimeUtc:
								File.SetCreationTimeUtc(targetName, entry.DateTime);
								break;
							case ZipEntryFactory.TimeSetting.LastAccessTime:
								File.SetLastAccessTime(targetName, entry.DateTime);
								break;
							case ZipEntryFactory.TimeSetting.LastAccessTimeUtc:
								File.SetLastAccessTimeUtc(targetName, entry.DateTime);
								break;
							case ZipEntryFactory.TimeSetting.Fixed:
								File.SetLastWriteTime(targetName, this.entryFactory_.FixedDateTime);
								break;
							default:
								throw new ZipException("Unhandled time setting in ExtractFileEntry");
							}
						}
						if (this.RestoreAttributesOnExtract && entry.IsDOSEntry && entry.ExternalFileAttributes != -1)
						{
							FileAttributes fileAttributes = (FileAttributes)entry.ExternalFileAttributes;
							fileAttributes &= FileAttributes.ReadOnly | FileAttributes.Hidden | FileAttributes.Archive | FileAttributes.Normal;
							File.SetAttributes(targetName, fileAttributes);
						}
					}
					catch (Exception ex)
					{
						if (this.events_ == null)
						{
							this.continueRunning_ = false;
							throw;
						}
						this.continueRunning_ = this.events_.OnFileFailure(targetName, ex);
					}
				}
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002D64 File Offset: 0x00000F64
		private void ExtractEntry(ZipEntry entry)
		{
			bool flag = entry.IsCompressionMethodSupported();
			string text = entry.Name;
			if (flag)
			{
				if (entry.IsFile)
				{
					text = this.extractNameTransform_.TransformFile(text);
				}
				else if (entry.IsDirectory)
				{
					text = this.extractNameTransform_.TransformDirectory(text);
				}
				flag = !string.IsNullOrEmpty(text);
			}
			string text2 = string.Empty;
			if (flag)
			{
				if (entry.IsDirectory)
				{
					text2 = text;
				}
				else
				{
					text2 = Path.GetDirectoryName(Path.GetFullPath(text));
				}
			}
			if (flag && !Directory.Exists(text2) && (!entry.IsDirectory || this.CreateEmptyDirectories))
			{
				try
				{
					FastZipEvents fastZipEvents = this.events_;
					this.continueRunning_ = fastZipEvents == null || fastZipEvents.OnProcessDirectory(text2, true);
					if (this.continueRunning_)
					{
						Directory.CreateDirectory(text2);
						if (entry.IsDirectory && this.restoreDateTimeOnExtract_)
						{
							switch (this.entryFactory_.Setting)
							{
							case ZipEntryFactory.TimeSetting.LastWriteTime:
								Directory.SetLastWriteTime(text2, entry.DateTime);
								break;
							case ZipEntryFactory.TimeSetting.LastWriteTimeUtc:
								Directory.SetLastWriteTimeUtc(text2, entry.DateTime);
								break;
							case ZipEntryFactory.TimeSetting.CreateTime:
								Directory.SetCreationTime(text2, entry.DateTime);
								break;
							case ZipEntryFactory.TimeSetting.CreateTimeUtc:
								Directory.SetCreationTimeUtc(text2, entry.DateTime);
								break;
							case ZipEntryFactory.TimeSetting.LastAccessTime:
								Directory.SetLastAccessTime(text2, entry.DateTime);
								break;
							case ZipEntryFactory.TimeSetting.LastAccessTimeUtc:
								Directory.SetLastAccessTimeUtc(text2, entry.DateTime);
								break;
							case ZipEntryFactory.TimeSetting.Fixed:
								Directory.SetLastWriteTime(text2, this.entryFactory_.FixedDateTime);
								break;
							default:
								throw new ZipException("Unhandled time setting in ExtractEntry");
							}
						}
					}
					else
					{
						flag = false;
					}
				}
				catch (Exception ex)
				{
					flag = false;
					if (this.events_ == null)
					{
						this.continueRunning_ = false;
						throw;
					}
					if (entry.IsDirectory)
					{
						this.continueRunning_ = this.events_.OnDirectoryFailure(text, ex);
					}
					else
					{
						this.continueRunning_ = this.events_.OnFileFailure(text, ex);
					}
				}
			}
			if (flag && entry.IsFile)
			{
				this.ExtractFileEntry(entry, text);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002F54 File Offset: 0x00001154
		private static int MakeExternalAttributes(FileInfo info)
		{
			return (int)info.Attributes;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002F5C File Offset: 0x0000115C
		private static bool NameIsValid(string name)
		{
			return !string.IsNullOrEmpty(name) && name.IndexOfAny(Path.GetInvalidPathChars()) < 0;
		}

		// Token: 0x0400000C RID: 12
		private bool continueRunning_;

		// Token: 0x0400000D RID: 13
		private byte[] buffer_;

		// Token: 0x0400000E RID: 14
		private ZipOutputStream outputStream_;

		// Token: 0x0400000F RID: 15
		private ZipFile zipFile_;

		// Token: 0x04000010 RID: 16
		private string sourceDirectory_;

		// Token: 0x04000011 RID: 17
		private NameFilter fileFilter_;

		// Token: 0x04000012 RID: 18
		private NameFilter directoryFilter_;

		// Token: 0x04000013 RID: 19
		private FastZip.Overwrite overwrite_;

		// Token: 0x04000014 RID: 20
		private FastZip.ConfirmOverwriteDelegate confirmDelegate_;

		// Token: 0x04000015 RID: 21
		private bool restoreDateTimeOnExtract_;

		// Token: 0x04000016 RID: 22
		private bool restoreAttributesOnExtract_;

		// Token: 0x04000017 RID: 23
		private bool createEmptyDirectories_;

		// Token: 0x04000018 RID: 24
		private FastZipEvents events_;

		// Token: 0x04000019 RID: 25
		private IEntryFactory entryFactory_ = new ZipEntryFactory();

		// Token: 0x0400001A RID: 26
		private INameTransform extractNameTransform_;

		// Token: 0x0400001B RID: 27
		private UseZip64 useZip64_ = UseZip64.Dynamic;

		// Token: 0x0400001C RID: 28
		private Deflater.CompressionLevel compressionLevel_ = Deflater.CompressionLevel.DEFAULT_COMPRESSION;

		// Token: 0x0400001D RID: 29
		private StringCodec _stringCodec = ZipStrings.GetStringCodec();

		// Token: 0x0400001E RID: 30
		private string password_;

		// Token: 0x02000009 RID: 9
		public enum Overwrite
		{
			// Token: 0x04000020 RID: 32
			Prompt,
			// Token: 0x04000021 RID: 33
			Never,
			// Token: 0x04000022 RID: 34
			Always
		}

		// Token: 0x0200000A RID: 10
		// (Invoke) Token: 0x06000050 RID: 80
		public delegate bool ConfirmOverwriteDelegate(string fileName);
	}
}
