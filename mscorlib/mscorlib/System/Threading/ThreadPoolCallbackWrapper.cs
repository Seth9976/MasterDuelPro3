using System;
using Internal.Runtime.Augments;

namespace System.Threading
{
	// Token: 0x02000241 RID: 577
	internal struct ThreadPoolCallbackWrapper
	{
		// Token: 0x06001536 RID: 5430 RVA: 0x00055504 File Offset: 0x00053704
		public static ThreadPoolCallbackWrapper Enter()
		{
			return new ThreadPoolCallbackWrapper
			{
				_currentThread = RuntimeThread.InitializeThreadPoolThread()
			};
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x00055526 File Offset: 0x00053726
		public void Exit(bool resetThread = true)
		{
			if (resetThread)
			{
				this._currentThread.ResetThreadPoolThread();
			}
		}

		// Token: 0x04000A69 RID: 2665
		private RuntimeThread _currentThread;
	}
}
