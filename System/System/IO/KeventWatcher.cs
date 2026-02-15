using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x02000365 RID: 869
	internal class KeventWatcher : IFileWatcher
	{
		// Token: 0x0600156D RID: 5485 RVA: 0x000026E5 File Offset: 0x000008E5
		private KeventWatcher()
		{
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x0005C3BC File Offset: 0x0005A5BC
		public static bool GetInstance(out IFileWatcher watcher)
		{
			if (KeventWatcher.failed)
			{
				watcher = null;
				return false;
			}
			if (KeventWatcher.instance != null)
			{
				watcher = KeventWatcher.instance;
				return true;
			}
			KeventWatcher.watches = Hashtable.Synchronized(new Hashtable());
			int num = KeventWatcher.kqueue();
			if (num == -1)
			{
				KeventWatcher.failed = true;
				watcher = null;
				return false;
			}
			KeventWatcher.close(num);
			KeventWatcher.instance = new KeventWatcher();
			watcher = KeventWatcher.instance;
			return true;
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x0005C424 File Offset: 0x0005A624
		public void StartDispatching(object handle)
		{
			FileSystemWatcher fileSystemWatcher = handle as FileSystemWatcher;
			KqueueMonitor kqueueMonitor;
			if (KeventWatcher.watches.ContainsKey(fileSystemWatcher))
			{
				kqueueMonitor = (KqueueMonitor)KeventWatcher.watches[fileSystemWatcher];
			}
			else
			{
				kqueueMonitor = new KqueueMonitor(fileSystemWatcher);
				KeventWatcher.watches.Add(fileSystemWatcher, kqueueMonitor);
			}
			kqueueMonitor.Start();
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x0005C474 File Offset: 0x0005A674
		public void StopDispatching(object handle)
		{
			FileSystemWatcher fileSystemWatcher = handle as FileSystemWatcher;
			KqueueMonitor kqueueMonitor = (KqueueMonitor)KeventWatcher.watches[fileSystemWatcher];
			if (kqueueMonitor == null)
			{
				return;
			}
			kqueueMonitor.Stop();
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x00002FA0 File Offset: 0x000011A0
		public void Dispose(object handle)
		{
		}

		// Token: 0x06001572 RID: 5490
		[DllImport("libc")]
		private static extern int close(int fd);

		// Token: 0x06001573 RID: 5491
		[DllImport("libc")]
		private static extern int kqueue();

		// Token: 0x04000CF3 RID: 3315
		private static bool failed;

		// Token: 0x04000CF4 RID: 3316
		private static KeventWatcher instance;

		// Token: 0x04000CF5 RID: 3317
		private static Hashtable watches;
	}
}
