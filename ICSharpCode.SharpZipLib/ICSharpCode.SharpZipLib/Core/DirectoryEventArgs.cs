using System;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x020000AD RID: 173
	public class DirectoryEventArgs : ScanEventArgs
	{
		// Token: 0x06000572 RID: 1394 RVA: 0x0001A396 File Offset: 0x00018596
		public DirectoryEventArgs(string name, bool hasMatchingFiles)
			: base(name)
		{
			this.hasMatchingFiles_ = hasMatchingFiles;
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x0001A3A6 File Offset: 0x000185A6
		public bool HasMatchingFiles
		{
			get
			{
				return this.hasMatchingFiles_;
			}
		}

		// Token: 0x04000435 RID: 1077
		private readonly bool hasMatchingFiles_;
	}
}
