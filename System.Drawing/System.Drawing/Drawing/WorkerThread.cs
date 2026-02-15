using System;
using System.Threading;

namespace System.Drawing
{
	// Token: 0x02000052 RID: 82
	internal class WorkerThread
	{
		// Token: 0x06000303 RID: 771 RVA: 0x0000B389 File Offset: 0x00009589
		public WorkerThread(EventHandler frmChgHandler, AnimateEventArgs aniEvtArgs, int[] delay)
		{
			this.frameChangeHandler = frmChgHandler;
			this.animateEventArgs = aniEvtArgs;
			this.delay = delay;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000B3A8 File Offset: 0x000095A8
		public void LoopHandler()
		{
			try
			{
				int num = 0;
				for (;;)
				{
					Thread.Sleep(this.delay[num++]);
					this.frameChangeHandler(null, this.animateEventArgs);
					if (num == this.delay.Length)
					{
						num = 0;
					}
				}
			}
			catch (ThreadAbortException)
			{
				Thread.ResetAbort();
			}
		}

		// Token: 0x04000183 RID: 387
		private EventHandler frameChangeHandler;

		// Token: 0x04000184 RID: 388
		private AnimateEventArgs animateEventArgs;

		// Token: 0x04000185 RID: 389
		private int[] delay;
	}
}
