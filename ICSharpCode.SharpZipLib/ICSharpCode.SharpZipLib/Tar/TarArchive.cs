using System;
using System.IO;
using System.Text;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Tar
{
	// Token: 0x0200006C RID: 108
	public class TarArchive : IDisposable
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600037F RID: 895 RVA: 0x00011790 File Offset: 0x0000F990
		// (remove) Token: 0x06000380 RID: 896 RVA: 0x000117C8 File Offset: 0x0000F9C8
		public event ProgressMessageHandler ProgressMessageEvent;

		// Token: 0x06000381 RID: 897 RVA: 0x00011800 File Offset: 0x0000FA00
		protected virtual void OnProgressMessageEvent(TarEntry entry, string message)
		{
			ProgressMessageHandler progressMessageEvent = this.ProgressMessageEvent;
			if (progressMessageEvent != null)
			{
				progressMessageEvent(this, entry, message);
			}
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00011820 File Offset: 0x0000FA20
		protected TarArchive()
		{
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0001183E File Offset: 0x0000FA3E
		protected TarArchive(TarInputStream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.tarIn = stream;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00011871 File Offset: 0x0000FA71
		protected TarArchive(TarOutputStream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.tarOut = stream;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x000118A4 File Offset: 0x0000FAA4
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public static TarArchive CreateInputTarArchive(Stream inputStream)
		{
			return TarArchive.CreateInputTarArchive(inputStream, null);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x000118B0 File Offset: 0x0000FAB0
		public static TarArchive CreateInputTarArchive(Stream inputStream, Encoding nameEncoding)
		{
			if (inputStream == null)
			{
				throw new ArgumentNullException("inputStream");
			}
			TarInputStream tarInputStream = inputStream as TarInputStream;
			TarArchive tarArchive;
			if (tarInputStream != null)
			{
				tarArchive = new TarArchive(tarInputStream);
			}
			else
			{
				tarArchive = TarArchive.CreateInputTarArchive(inputStream, 20, nameEncoding);
			}
			return tarArchive;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x000118E9 File Offset: 0x0000FAE9
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public static TarArchive CreateInputTarArchive(Stream inputStream, int blockFactor)
		{
			return TarArchive.CreateInputTarArchive(inputStream, blockFactor, null);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x000118F3 File Offset: 0x0000FAF3
		public static TarArchive CreateInputTarArchive(Stream inputStream, int blockFactor, Encoding nameEncoding)
		{
			if (inputStream == null)
			{
				throw new ArgumentNullException("inputStream");
			}
			if (inputStream is TarInputStream)
			{
				throw new ArgumentException("TarInputStream not valid");
			}
			return new TarArchive(new TarInputStream(inputStream, blockFactor, nameEncoding));
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00011924 File Offset: 0x0000FB24
		public static TarArchive CreateOutputTarArchive(Stream outputStream, Encoding nameEncoding)
		{
			if (outputStream == null)
			{
				throw new ArgumentNullException("outputStream");
			}
			TarOutputStream tarOutputStream = outputStream as TarOutputStream;
			TarArchive tarArchive;
			if (tarOutputStream != null)
			{
				tarArchive = new TarArchive(tarOutputStream);
			}
			else
			{
				tarArchive = TarArchive.CreateOutputTarArchive(outputStream, 20, nameEncoding);
			}
			return tarArchive;
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0001195D File Offset: 0x0000FB5D
		public static TarArchive CreateOutputTarArchive(Stream outputStream)
		{
			return TarArchive.CreateOutputTarArchive(outputStream, null);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00011966 File Offset: 0x0000FB66
		public static TarArchive CreateOutputTarArchive(Stream outputStream, int blockFactor)
		{
			return TarArchive.CreateOutputTarArchive(outputStream, blockFactor, null);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00011970 File Offset: 0x0000FB70
		public static TarArchive CreateOutputTarArchive(Stream outputStream, int blockFactor, Encoding nameEncoding)
		{
			if (outputStream == null)
			{
				throw new ArgumentNullException("outputStream");
			}
			if (outputStream is TarOutputStream)
			{
				throw new ArgumentException("TarOutputStream is not valid");
			}
			return new TarArchive(new TarOutputStream(outputStream, blockFactor, nameEncoding));
		}

		// Token: 0x0600038D RID: 909 RVA: 0x000119A0 File Offset: 0x0000FBA0
		public void SetKeepOldFiles(bool keepExistingFiles)
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("TarArchive");
			}
			this.keepOldFiles = keepExistingFiles;
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600038E RID: 910 RVA: 0x000119BC File Offset: 0x0000FBBC
		// (set) Token: 0x0600038F RID: 911 RVA: 0x000119D7 File Offset: 0x0000FBD7
		public bool AsciiTranslate
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("TarArchive");
				}
				return this.asciiTranslate;
			}
			set
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("TarArchive");
				}
				this.asciiTranslate = value;
			}
		}

		// Token: 0x06000390 RID: 912 RVA: 0x000119D7 File Offset: 0x0000FBD7
		[Obsolete("Use the AsciiTranslate property")]
		public void SetAsciiTranslation(bool translateAsciiFiles)
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("TarArchive");
			}
			this.asciiTranslate = translateAsciiFiles;
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000391 RID: 913 RVA: 0x000119F3 File Offset: 0x0000FBF3
		// (set) Token: 0x06000392 RID: 914 RVA: 0x00011A0E File Offset: 0x0000FC0E
		public string PathPrefix
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("TarArchive");
				}
				return this.pathPrefix;
			}
			set
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("TarArchive");
				}
				this.pathPrefix = value;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000393 RID: 915 RVA: 0x00011A2A File Offset: 0x0000FC2A
		// (set) Token: 0x06000394 RID: 916 RVA: 0x00011A45 File Offset: 0x0000FC45
		public string RootPath
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("TarArchive");
				}
				return this.rootPath;
			}
			set
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("TarArchive");
				}
				this.rootPath = value.ToTarArchivePath().TrimEnd(new char[] { '/' });
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00011A76 File Offset: 0x0000FC76
		public void SetUserInfo(int userId, string userName, int groupId, string groupName)
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("TarArchive");
			}
			this.userId = userId;
			this.userName = userName;
			this.groupId = groupId;
			this.groupName = groupName;
			this.applyUserInfoOverrides = true;
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000396 RID: 918 RVA: 0x00011AAF File Offset: 0x0000FCAF
		// (set) Token: 0x06000397 RID: 919 RVA: 0x00011ACA File Offset: 0x0000FCCA
		public bool ApplyUserInfoOverrides
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("TarArchive");
				}
				return this.applyUserInfoOverrides;
			}
			set
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("TarArchive");
				}
				this.applyUserInfoOverrides = value;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000398 RID: 920 RVA: 0x00011AE6 File Offset: 0x0000FCE6
		public int UserId
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("TarArchive");
				}
				return this.userId;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000399 RID: 921 RVA: 0x00011B01 File Offset: 0x0000FD01
		public string UserName
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("TarArchive");
				}
				return this.userName;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600039A RID: 922 RVA: 0x00011B1C File Offset: 0x0000FD1C
		public int GroupId
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("TarArchive");
				}
				return this.groupId;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600039B RID: 923 RVA: 0x00011B37 File Offset: 0x0000FD37
		public string GroupName
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("TarArchive");
				}
				return this.groupName;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600039C RID: 924 RVA: 0x00011B54 File Offset: 0x0000FD54
		public int RecordSize
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("TarArchive");
				}
				if (this.tarIn != null)
				{
					return this.tarIn.RecordSize;
				}
				if (this.tarOut != null)
				{
					return this.tarOut.RecordSize;
				}
				return 10240;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (set) Token: 0x0600039D RID: 925 RVA: 0x00011BA1 File Offset: 0x0000FDA1
		public bool IsStreamOwner
		{
			set
			{
				if (this.tarIn != null)
				{
					this.tarIn.IsStreamOwner = value;
					return;
				}
				this.tarOut.IsStreamOwner = value;
			}
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00011BC4 File Offset: 0x0000FDC4
		[Obsolete("Use Close instead")]
		public void CloseArchive()
		{
			this.Close();
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00011BCC File Offset: 0x0000FDCC
		public void ListContents()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("TarArchive");
			}
			for (;;)
			{
				TarEntry nextEntry = this.tarIn.GetNextEntry();
				if (nextEntry == null)
				{
					break;
				}
				this.OnProgressMessageEvent(nextEntry, null);
			}
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00011C05 File Offset: 0x0000FE05
		public void ExtractContents(string destinationDirectory)
		{
			this.ExtractContents(destinationDirectory, false);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00011C10 File Offset: 0x0000FE10
		public void ExtractContents(string destinationDirectory, bool allowParentTraversal)
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("TarArchive");
			}
			string text = Path.GetFullPath(destinationDirectory).TrimEnd(new char[] { '/', '\\' });
			for (;;)
			{
				TarEntry nextEntry = this.tarIn.GetNextEntry();
				if (nextEntry == null)
				{
					break;
				}
				if (nextEntry.TarHeader.TypeFlag != 49 && nextEntry.TarHeader.TypeFlag != 50)
				{
					this.ExtractEntry(text, nextEntry, allowParentTraversal);
				}
			}
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00011C84 File Offset: 0x0000FE84
		private void ExtractEntry(string destDir, TarEntry entry, bool allowParentTraversal)
		{
			this.OnProgressMessageEvent(entry, null);
			string text = entry.Name;
			if (Path.IsPathRooted(text))
			{
				text = text.Substring(Path.GetPathRoot(text).Length);
			}
			text = text.Replace('/', Path.DirectorySeparatorChar);
			string text2 = Path.Combine(destDir, text);
			string text3 = Path.GetDirectoryName(Path.GetFullPath(text2)) ?? "";
			bool flag = entry.IsDirectory && entry.Name == "";
			if (!allowParentTraversal && !flag && !text3.StartsWith(destDir, StringComparison.InvariantCultureIgnoreCase))
			{
				throw new InvalidNameException("Parent traversal in paths is not allowed");
			}
			if (entry.IsDirectory)
			{
				TarArchive.EnsureDirectoryExists(text2);
				return;
			}
			TarArchive.EnsureDirectoryExists(Path.GetDirectoryName(text2));
			bool flag2 = true;
			FileInfo fileInfo = new FileInfo(text2);
			if (fileInfo.Exists)
			{
				if (this.keepOldFiles)
				{
					this.OnProgressMessageEvent(entry, "Destination file already exists");
					flag2 = false;
				}
				else if ((fileInfo.Attributes & FileAttributes.ReadOnly) != (FileAttributes)0)
				{
					this.OnProgressMessageEvent(entry, "Destination file already exists, and is read-only");
					flag2 = false;
				}
			}
			if (flag2)
			{
				using (FileStream fileStream = File.Create(text2))
				{
					if (this.asciiTranslate)
					{
						this.ExtractAndTranslateEntry(text2, fileStream);
					}
					else
					{
						this.tarIn.CopyEntryContents(fileStream);
					}
				}
			}
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00011DC8 File Offset: 0x0000FFC8
		private void ExtractAndTranslateEntry(string destFile, Stream outputStream)
		{
			if (!TarArchive.IsBinary(destFile))
			{
				using (StreamWriter streamWriter = new StreamWriter(outputStream, new UTF8Encoding(false), 1024, true))
				{
					byte[] array = new byte[32768];
					for (;;)
					{
						int num = this.tarIn.Read(array, 0, array.Length);
						if (num <= 0)
						{
							break;
						}
						int num2 = 0;
						for (int i = 0; i < num; i++)
						{
							if (array[i] == 10)
							{
								string @string = Encoding.ASCII.GetString(array, num2, i - num2);
								streamWriter.WriteLine(@string);
								num2 = i + 1;
							}
						}
					}
					return;
				}
			}
			this.tarIn.CopyEntryContents(outputStream);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00011E78 File Offset: 0x00010078
		public void WriteEntry(TarEntry sourceEntry, bool recurse)
		{
			if (sourceEntry == null)
			{
				throw new ArgumentNullException("sourceEntry");
			}
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("TarArchive");
			}
			try
			{
				if (recurse)
				{
					TarHeader.SetValueDefaults(sourceEntry.UserId, sourceEntry.UserName, sourceEntry.GroupId, sourceEntry.GroupName);
				}
				this.WriteEntryCore(sourceEntry, recurse);
			}
			finally
			{
				if (recurse)
				{
					TarHeader.RestoreSetValues();
				}
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00011EEC File Offset: 0x000100EC
		private void WriteEntryCore(TarEntry sourceEntry, bool recurse)
		{
			string text = null;
			string text2 = sourceEntry.File;
			TarEntry tarEntry = (TarEntry)sourceEntry.Clone();
			if (this.applyUserInfoOverrides)
			{
				tarEntry.GroupId = this.groupId;
				tarEntry.GroupName = this.groupName;
				tarEntry.UserId = this.userId;
				tarEntry.UserName = this.userName;
			}
			this.OnProgressMessageEvent(tarEntry, null);
			if (this.asciiTranslate && !tarEntry.IsDirectory && !TarArchive.IsBinary(text2))
			{
				text = PathUtils.GetTempFileName(null);
				using (StreamReader streamReader = File.OpenText(text2))
				{
					using (Stream stream = File.Create(text))
					{
						for (;;)
						{
							string text3 = streamReader.ReadLine();
							if (text3 == null)
							{
								break;
							}
							byte[] bytes = Encoding.ASCII.GetBytes(text3);
							stream.Write(bytes, 0, bytes.Length);
							stream.WriteByte(10);
						}
						stream.Flush();
					}
				}
				tarEntry.Size = new FileInfo(text).Length;
				text2 = text;
			}
			string text4 = null;
			if (!string.IsNullOrEmpty(this.rootPath) && tarEntry.Name.StartsWith(this.rootPath, StringComparison.OrdinalIgnoreCase))
			{
				text4 = tarEntry.Name.Substring(this.rootPath.Length + 1);
			}
			if (this.pathPrefix != null)
			{
				text4 = ((text4 == null) ? (this.pathPrefix + "/" + tarEntry.Name) : (this.pathPrefix + "/" + text4));
			}
			if (text4 != null)
			{
				tarEntry.Name = text4;
			}
			this.tarOut.PutNextEntry(tarEntry);
			if (tarEntry.IsDirectory)
			{
				if (recurse)
				{
					TarEntry[] directoryEntries = tarEntry.GetDirectoryEntries();
					for (int i = 0; i < directoryEntries.Length; i++)
					{
						this.WriteEntryCore(directoryEntries[i], recurse);
					}
					return;
				}
			}
			else
			{
				using (Stream stream2 = File.OpenRead(text2))
				{
					byte[] array = new byte[32768];
					for (;;)
					{
						int num = stream2.Read(array, 0, array.Length);
						if (num <= 0)
						{
							break;
						}
						this.tarOut.Write(array, 0, num);
					}
				}
				if (!string.IsNullOrEmpty(text))
				{
					File.Delete(text);
				}
				this.tarOut.CloseEntry();
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00012134 File Offset: 0x00010334
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00012144 File Offset: 0x00010344
		protected virtual void Dispose(bool disposing)
		{
			if (!this.isDisposed)
			{
				this.isDisposed = true;
				if (disposing)
				{
					if (this.tarOut != null)
					{
						this.tarOut.Flush();
						this.tarOut.Dispose();
					}
					if (this.tarIn != null)
					{
						this.tarIn.Dispose();
					}
				}
			}
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00012194 File Offset: 0x00010394
		public virtual void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x000121A0 File Offset: 0x000103A0
		~TarArchive()
		{
			this.Dispose(false);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x000121D0 File Offset: 0x000103D0
		private static void EnsureDirectoryExists(string directoryName)
		{
			if (!Directory.Exists(directoryName))
			{
				try
				{
					Directory.CreateDirectory(directoryName);
				}
				catch (Exception ex)
				{
					throw new TarException("Exception creating directory '" + directoryName + "', " + ex.Message, ex);
				}
			}
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0001221C File Offset: 0x0001041C
		private static bool IsBinary(string filename)
		{
			using (FileStream fileStream = File.OpenRead(filename))
			{
				int num = Math.Min(4096, (int)fileStream.Length);
				byte[] array = new byte[num];
				int num2 = fileStream.Read(array, 0, num);
				for (int i = 0; i < num2; i++)
				{
					byte b = array[i];
					if (b < 8 || (b > 13 && b < 32) || b == 255)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04000283 RID: 643
		private bool keepOldFiles;

		// Token: 0x04000284 RID: 644
		private bool asciiTranslate;

		// Token: 0x04000285 RID: 645
		private int userId;

		// Token: 0x04000286 RID: 646
		private string userName = string.Empty;

		// Token: 0x04000287 RID: 647
		private int groupId;

		// Token: 0x04000288 RID: 648
		private string groupName = string.Empty;

		// Token: 0x04000289 RID: 649
		private string rootPath;

		// Token: 0x0400028A RID: 650
		private string pathPrefix;

		// Token: 0x0400028B RID: 651
		private bool applyUserInfoOverrides;

		// Token: 0x0400028C RID: 652
		private TarInputStream tarIn;

		// Token: 0x0400028D RID: 653
		private TarOutputStream tarOut;

		// Token: 0x0400028E RID: 654
		private bool isDisposed;
	}
}
