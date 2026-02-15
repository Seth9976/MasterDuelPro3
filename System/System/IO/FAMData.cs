using System;
using System.Collections;

namespace System.IO
{
	// Token: 0x02000352 RID: 850
	internal class FAMData
	{
		// Token: 0x04000C5C RID: 3164
		public FileSystemWatcher FSW;

		// Token: 0x04000C5D RID: 3165
		public string Directory;

		// Token: 0x04000C5E RID: 3166
		public string FileMask;

		// Token: 0x04000C5F RID: 3167
		public bool IncludeSubdirs;

		// Token: 0x04000C60 RID: 3168
		public bool Enabled;

		// Token: 0x04000C61 RID: 3169
		public FAMRequest Request;

		// Token: 0x04000C62 RID: 3170
		public Hashtable SubDirs;
	}
}
