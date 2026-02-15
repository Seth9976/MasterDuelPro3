using System;

namespace System.Threading
{
	// Token: 0x02000270 RID: 624
	internal static class _ThreadPoolWaitCallback
	{
		// Token: 0x0600170D RID: 5901 RVA: 0x0005A5FC File Offset: 0x000587FC
		internal static bool PerformWaitCallback()
		{
			return ThreadPoolWorkQueue.Dispatch();
		}
	}
}
