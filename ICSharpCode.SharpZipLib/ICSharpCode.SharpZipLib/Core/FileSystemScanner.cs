using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x020000B4 RID: 180
	public class FileSystemScanner
	{
		// Token: 0x0600058D RID: 1421 RVA: 0x0001A3EC File Offset: 0x000185EC
		public FileSystemScanner(string filter)
		{
			this.fileFilter_ = new PathFilter(filter);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0001A400 File Offset: 0x00018600
		public FileSystemScanner(string fileFilter, string directoryFilter)
		{
			this.fileFilter_ = new PathFilter(fileFilter);
			this.directoryFilter_ = new PathFilter(directoryFilter);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0001A420 File Offset: 0x00018620
		public FileSystemScanner(IScanFilter fileFilter)
		{
			this.fileFilter_ = fileFilter;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0001A42F File Offset: 0x0001862F
		public FileSystemScanner(IScanFilter fileFilter, IScanFilter directoryFilter)
		{
			this.fileFilter_ = fileFilter;
			this.directoryFilter_ = directoryFilter;
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000591 RID: 1425 RVA: 0x0001A448 File Offset: 0x00018648
		// (remove) Token: 0x06000592 RID: 1426 RVA: 0x0001A480 File Offset: 0x00018680
		public event EventHandler<DirectoryEventArgs> ProcessDirectory;

		// Token: 0x06000593 RID: 1427 RVA: 0x0001A4B8 File Offset: 0x000186B8
		private bool OnDirectoryFailure(string directory, Exception e)
		{
			DirectoryFailureHandler directoryFailure = this.DirectoryFailure;
			bool flag = directoryFailure != null;
			if (flag)
			{
				ScanFailureEventArgs scanFailureEventArgs = new ScanFailureEventArgs(directory, e);
				directoryFailure(this, scanFailureEventArgs);
				this.alive_ = scanFailureEventArgs.ContinueRunning;
			}
			return flag;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0001A4F0 File Offset: 0x000186F0
		private bool OnFileFailure(string file, Exception e)
		{
			bool flag = this.FileFailure != null;
			if (flag)
			{
				ScanFailureEventArgs scanFailureEventArgs = new ScanFailureEventArgs(file, e);
				this.FileFailure(this, scanFailureEventArgs);
				this.alive_ = scanFailureEventArgs.ContinueRunning;
			}
			return flag;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0001A52C File Offset: 0x0001872C
		private void OnProcessFile(string file)
		{
			ProcessFileHandler processFile = this.ProcessFile;
			if (processFile != null)
			{
				ScanEventArgs scanEventArgs = new ScanEventArgs(file);
				processFile(this, scanEventArgs);
				this.alive_ = scanEventArgs.ContinueRunning;
			}
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0001A560 File Offset: 0x00018760
		private void OnCompleteFile(string file)
		{
			CompletedFileHandler completedFile = this.CompletedFile;
			if (completedFile != null)
			{
				ScanEventArgs scanEventArgs = new ScanEventArgs(file);
				completedFile(this, scanEventArgs);
				this.alive_ = scanEventArgs.ContinueRunning;
			}
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0001A594 File Offset: 0x00018794
		private void OnProcessDirectory(string directory, bool hasMatchingFiles)
		{
			EventHandler<DirectoryEventArgs> processDirectory = this.ProcessDirectory;
			if (processDirectory != null)
			{
				DirectoryEventArgs directoryEventArgs = new DirectoryEventArgs(directory, hasMatchingFiles);
				processDirectory(this, directoryEventArgs);
				this.alive_ = directoryEventArgs.ContinueRunning;
			}
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0001A5C7 File Offset: 0x000187C7
		public void Scan(string directory, bool recurse)
		{
			this.alive_ = true;
			this.ScanDir(directory, recurse);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0001A5D8 File Offset: 0x000187D8
		private void ScanDir(string directory, bool recurse)
		{
			try
			{
				string[] files = Directory.GetFiles(directory);
				bool flag = false;
				for (int i = 0; i < files.Length; i++)
				{
					if (!this.fileFilter_.IsMatch(files[i]))
					{
						files[i] = null;
					}
					else
					{
						flag = true;
					}
				}
				this.OnProcessDirectory(directory, flag);
				if (this.alive_ && flag)
				{
					foreach (string text in files)
					{
						try
						{
							if (text != null)
							{
								this.OnProcessFile(text);
								if (!this.alive_)
								{
									break;
								}
							}
						}
						catch (Exception ex)
						{
							if (!this.OnFileFailure(text, ex))
							{
								throw;
							}
						}
					}
				}
			}
			catch (Exception ex2)
			{
				if (!this.OnDirectoryFailure(directory, ex2))
				{
					throw;
				}
			}
			if (this.alive_ && recurse)
			{
				try
				{
					foreach (string text2 in Directory.GetDirectories(directory))
					{
						if (this.directoryFilter_ == null || this.directoryFilter_.IsMatch(text2))
						{
							this.ScanDir(text2, true);
							if (!this.alive_)
							{
								break;
							}
						}
					}
				}
				catch (Exception ex3)
				{
					if (!this.OnDirectoryFailure(directory, ex3))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x0400043A RID: 1082
		public ProcessFileHandler ProcessFile;

		// Token: 0x0400043B RID: 1083
		public CompletedFileHandler CompletedFile;

		// Token: 0x0400043C RID: 1084
		public DirectoryFailureHandler DirectoryFailure;

		// Token: 0x0400043D RID: 1085
		public FileFailureHandler FileFailure;

		// Token: 0x0400043E RID: 1086
		private IScanFilter fileFilter_;

		// Token: 0x0400043F RID: 1087
		private IScanFilter directoryFilter_;

		// Token: 0x04000440 RID: 1088
		private bool alive_;
	}
}
