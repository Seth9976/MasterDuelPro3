using System;
using System.Collections.Generic;

namespace System.IO
{
	// Token: 0x0200034A RID: 842
	internal class DefaultWatcherData
	{
		// Token: 0x04000C3C RID: 3132
		public FileSystemWatcher FSW;

		// Token: 0x04000C3D RID: 3133
		public string Directory;

		// Token: 0x04000C3E RID: 3134
		public string FileMask;

		// Token: 0x04000C3F RID: 3135
		public bool IncludeSubdirs;

		// Token: 0x04000C40 RID: 3136
		public bool Enabled;

		// Token: 0x04000C41 RID: 3137
		public bool NoWildcards;

		// Token: 0x04000C42 RID: 3138
		public DateTime DisabledTime;

		// Token: 0x04000C43 RID: 3139
		public object FilesLock = new object();

		// Token: 0x04000C44 RID: 3140
		public Dictionary<string, FileData> Files;
	}
}
