using System;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000007 RID: 7
	public class FastZipEvents
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000017 RID: 23 RVA: 0x0000214C File Offset: 0x0000034C
		// (remove) Token: 0x06000018 RID: 24 RVA: 0x00002184 File Offset: 0x00000384
		public event EventHandler<DirectoryEventArgs> ProcessDirectory;

		// Token: 0x06000019 RID: 25 RVA: 0x000021BC File Offset: 0x000003BC
		public bool OnDirectoryFailure(string directory, Exception e)
		{
			bool flag = false;
			DirectoryFailureHandler directoryFailure = this.DirectoryFailure;
			if (directoryFailure != null)
			{
				ScanFailureEventArgs scanFailureEventArgs = new ScanFailureEventArgs(directory, e);
				directoryFailure(this, scanFailureEventArgs);
				flag = scanFailureEventArgs.ContinueRunning;
			}
			return flag;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000021F0 File Offset: 0x000003F0
		public bool OnFileFailure(string file, Exception e)
		{
			FileFailureHandler fileFailure = this.FileFailure;
			bool flag = fileFailure != null;
			if (flag)
			{
				ScanFailureEventArgs scanFailureEventArgs = new ScanFailureEventArgs(file, e);
				fileFailure(this, scanFailureEventArgs);
				flag = scanFailureEventArgs.ContinueRunning;
			}
			return flag;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002224 File Offset: 0x00000424
		public bool OnProcessFile(string file)
		{
			bool flag = true;
			ProcessFileHandler processFile = this.ProcessFile;
			if (processFile != null)
			{
				ScanEventArgs scanEventArgs = new ScanEventArgs(file);
				processFile(this, scanEventArgs);
				flag = scanEventArgs.ContinueRunning;
			}
			return flag;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002254 File Offset: 0x00000454
		public bool OnCompletedFile(string file)
		{
			bool flag = true;
			CompletedFileHandler completedFile = this.CompletedFile;
			if (completedFile != null)
			{
				ScanEventArgs scanEventArgs = new ScanEventArgs(file);
				completedFile(this, scanEventArgs);
				flag = scanEventArgs.ContinueRunning;
			}
			return flag;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002284 File Offset: 0x00000484
		public bool OnProcessDirectory(string directory, bool hasMatchingFiles)
		{
			bool flag = true;
			EventHandler<DirectoryEventArgs> processDirectory = this.ProcessDirectory;
			if (processDirectory != null)
			{
				DirectoryEventArgs directoryEventArgs = new DirectoryEventArgs(directory, hasMatchingFiles);
				processDirectory(this, directoryEventArgs);
				flag = directoryEventArgs.ContinueRunning;
			}
			return flag;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000022B5 File Offset: 0x000004B5
		// (set) Token: 0x0600001F RID: 31 RVA: 0x000022BD File Offset: 0x000004BD
		public TimeSpan ProgressInterval
		{
			get
			{
				return this.progressInterval_;
			}
			set
			{
				this.progressInterval_ = value;
			}
		}

		// Token: 0x04000005 RID: 5
		public ProcessFileHandler ProcessFile;

		// Token: 0x04000006 RID: 6
		public ProgressHandler Progress;

		// Token: 0x04000007 RID: 7
		public CompletedFileHandler CompletedFile;

		// Token: 0x04000008 RID: 8
		public DirectoryFailureHandler DirectoryFailure;

		// Token: 0x04000009 RID: 9
		public FileFailureHandler FileFailure;

		// Token: 0x0400000A RID: 10
		private TimeSpan progressInterval_ = TimeSpan.FromSeconds(3.0);
	}
}
