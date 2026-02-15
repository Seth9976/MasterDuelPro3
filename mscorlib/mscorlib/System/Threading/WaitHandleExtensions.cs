using System;
using Microsoft.Win32.SafeHandles;

namespace System.Threading
{
	// Token: 0x02000279 RID: 633
	public static class WaitHandleExtensions
	{
		// Token: 0x06001780 RID: 6016 RVA: 0x0005B8C3 File Offset: 0x00059AC3
		public static SafeWaitHandle GetSafeWaitHandle(this WaitHandle waitHandle)
		{
			if (waitHandle == null)
			{
				throw new ArgumentNullException("waitHandle");
			}
			return waitHandle.SafeWaitHandle;
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x0005B8D9 File Offset: 0x00059AD9
		public static void SetSafeWaitHandle(this WaitHandle waitHandle, SafeWaitHandle value)
		{
			if (waitHandle == null)
			{
				throw new ArgumentNullException("waitHandle");
			}
			waitHandle.SafeWaitHandle = value;
		}
	}
}
