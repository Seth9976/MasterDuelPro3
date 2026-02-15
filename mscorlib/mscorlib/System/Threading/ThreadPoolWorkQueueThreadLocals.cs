using System;

namespace System.Threading
{
	// Token: 0x0200026F RID: 623
	internal sealed class ThreadPoolWorkQueueThreadLocals
	{
		// Token: 0x0600170A RID: 5898 RVA: 0x0005A50F File Offset: 0x0005870F
		public ThreadPoolWorkQueueThreadLocals(ThreadPoolWorkQueue tpq)
		{
			this.workQueue = tpq;
			this.workStealingQueue = new ThreadPoolWorkQueue.WorkStealingQueue();
			ThreadPoolWorkQueue.allThreadQueues.Add(this.workStealingQueue);
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x0005A550 File Offset: 0x00058750
		private void CleanUp()
		{
			if (this.workStealingQueue != null)
			{
				if (this.workQueue != null)
				{
					bool flag = false;
					while (!flag)
					{
						try
						{
						}
						finally
						{
							IThreadPoolWorkItem threadPoolWorkItem = null;
							if (this.workStealingQueue.LocalPop(out threadPoolWorkItem))
							{
								this.workQueue.Enqueue(threadPoolWorkItem, true);
							}
							else
							{
								flag = true;
							}
						}
					}
				}
				ThreadPoolWorkQueue.allThreadQueues.Remove(this.workStealingQueue);
			}
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x0005A5BC File Offset: 0x000587BC
		~ThreadPoolWorkQueueThreadLocals()
		{
			if (!Environment.HasShutdownStarted && !AppDomain.CurrentDomain.IsFinalizingForUnload())
			{
				this.CleanUp();
			}
		}

		// Token: 0x04000B09 RID: 2825
		[ThreadStatic]
		public static ThreadPoolWorkQueueThreadLocals threadLocals;

		// Token: 0x04000B0A RID: 2826
		public readonly ThreadPoolWorkQueue workQueue;

		// Token: 0x04000B0B RID: 2827
		public readonly ThreadPoolWorkQueue.WorkStealingQueue workStealingQueue;

		// Token: 0x04000B0C RID: 2828
		public readonly Random random = new Random(Thread.CurrentThread.ManagedThreadId);
	}
}
