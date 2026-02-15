using System;

namespace System.IO
{
	// Token: 0x02000367 RID: 871
	internal class NullFileWatcher : IFileWatcher
	{
		// Token: 0x06001574 RID: 5492 RVA: 0x00002FA0 File Offset: 0x000011A0
		public void StartDispatching(object handle)
		{
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x00002FA0 File Offset: 0x000011A0
		public void StopDispatching(object handle)
		{
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x00002FA0 File Offset: 0x000011A0
		public void Dispose(object handle)
		{
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x0005C4A4 File Offset: 0x0005A6A4
		public static bool GetInstance(out IFileWatcher watcher)
		{
			if (NullFileWatcher.instance != null)
			{
				watcher = NullFileWatcher.instance;
				return true;
			}
			IFileWatcher fileWatcher;
			watcher = (fileWatcher = new NullFileWatcher());
			NullFileWatcher.instance = fileWatcher;
			return true;
		}

		// Token: 0x04000CFF RID: 3327
		private static IFileWatcher instance;
	}
}
