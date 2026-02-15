using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001C5 RID: 453
	internal sealed class UnitySynchronizationContext : SynchronizationContext
	{
		// Token: 0x060011AF RID: 4527 RVA: 0x00025DC9 File Offset: 0x00023FC9
		private UnitySynchronizationContext(int mainThreadID)
		{
			this.m_AsyncWorkQueue = new List<UnitySynchronizationContext.WorkRequest>(20);
			this.m_MainThreadID = mainThreadID;
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x00025DFB File Offset: 0x00023FFB
		private UnitySynchronizationContext(List<UnitySynchronizationContext.WorkRequest> queue, int mainThreadID)
		{
			this.m_AsyncWorkQueue = queue;
			this.m_MainThreadID = mainThreadID;
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x00025E28 File Offset: 0x00024028
		public override void Send(SendOrPostCallback callback, object state)
		{
			bool flag = this.m_MainThreadID == Thread.CurrentThread.ManagedThreadId;
			if (flag)
			{
				callback(state);
			}
			else
			{
				using (ManualResetEvent waitHandle = new ManualResetEvent(false))
				{
					List<UnitySynchronizationContext.WorkRequest> asyncWorkQueue = this.m_AsyncWorkQueue;
					lock (asyncWorkQueue)
					{
						this.m_AsyncWorkQueue.Add(new UnitySynchronizationContext.WorkRequest(callback, state, waitHandle));
					}
					waitHandle.WaitOne();
				}
			}
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x00025EC8 File Offset: 0x000240C8
		public override void OperationStarted()
		{
			Interlocked.Increment(ref this.m_TrackedCount);
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x00025ED7 File Offset: 0x000240D7
		public override void OperationCompleted()
		{
			Interlocked.Decrement(ref this.m_TrackedCount);
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x00025EE8 File Offset: 0x000240E8
		public override void Post(SendOrPostCallback callback, object state)
		{
			List<UnitySynchronizationContext.WorkRequest> asyncWorkQueue = this.m_AsyncWorkQueue;
			lock (asyncWorkQueue)
			{
				this.m_AsyncWorkQueue.Add(new UnitySynchronizationContext.WorkRequest(callback, state, null));
			}
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x00025F3C File Offset: 0x0002413C
		public override SynchronizationContext CreateCopy()
		{
			return new UnitySynchronizationContext(this.m_AsyncWorkQueue, this.m_MainThreadID);
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x00025F60 File Offset: 0x00024160
		public void Exec()
		{
			List<UnitySynchronizationContext.WorkRequest> asyncWorkQueue = this.m_AsyncWorkQueue;
			lock (asyncWorkQueue)
			{
				this.m_CurrentFrameWork.AddRange(this.m_AsyncWorkQueue);
				this.m_AsyncWorkQueue.Clear();
			}
			while (this.m_CurrentFrameWork.Count > 0)
			{
				UnitySynchronizationContext.WorkRequest work = this.m_CurrentFrameWork[0];
				this.m_CurrentFrameWork.RemoveAt(0);
				work.Invoke();
			}
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x00025FF8 File Offset: 0x000241F8
		private bool HasPendingTasks()
		{
			return this.m_AsyncWorkQueue.Count != 0 || this.m_TrackedCount != 0;
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x00026024 File Offset: 0x00024224
		[RequiredByNativeCode]
		private static void InitializeSynchronizationContext()
		{
			UnitySynchronizationContext synchronizationContext = new UnitySynchronizationContext(Thread.CurrentThread.ManagedThreadId);
			SynchronizationContext.SetSynchronizationContext(synchronizationContext);
			Awaitable.SetSynchronizationContext(synchronizationContext);
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x00026050 File Offset: 0x00024250
		[RequiredByNativeCode]
		private static void ExecuteTasks()
		{
			UnitySynchronizationContext context = SynchronizationContext.Current as UnitySynchronizationContext;
			bool flag = context != null;
			if (flag)
			{
				context.Exec();
			}
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x00026078 File Offset: 0x00024278
		[RequiredByNativeCode]
		private static bool ExecutePendingTasks(long millisecondsTimeout)
		{
			UnitySynchronizationContext context = SynchronizationContext.Current as UnitySynchronizationContext;
			bool flag = context == null;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				while (context.HasPendingTasks())
				{
					bool flag3 = stopwatch.ElapsedMilliseconds > millisecondsTimeout;
					if (flag3)
					{
						break;
					}
					context.Exec();
					Thread.Sleep(1);
				}
				flag2 = !context.HasPendingTasks();
			}
			return flag2;
		}

		// Token: 0x040006A2 RID: 1698
		private readonly List<UnitySynchronizationContext.WorkRequest> m_AsyncWorkQueue;

		// Token: 0x040006A3 RID: 1699
		private readonly List<UnitySynchronizationContext.WorkRequest> m_CurrentFrameWork = new List<UnitySynchronizationContext.WorkRequest>(20);

		// Token: 0x040006A4 RID: 1700
		private readonly int m_MainThreadID;

		// Token: 0x040006A5 RID: 1701
		private int m_TrackedCount = 0;

		// Token: 0x020001C6 RID: 454
		private struct WorkRequest
		{
			// Token: 0x060011BB RID: 4539 RVA: 0x000260E8 File Offset: 0x000242E8
			public WorkRequest(SendOrPostCallback callback, object state, ManualResetEvent waitHandle = null)
			{
				this.m_DelagateCallback = callback;
				this.m_DelagateState = state;
				this.m_WaitHandle = waitHandle;
			}

			// Token: 0x060011BC RID: 4540 RVA: 0x00026100 File Offset: 0x00024300
			public void Invoke()
			{
				try
				{
					this.m_DelagateCallback(this.m_DelagateState);
				}
				finally
				{
					ManualResetEvent waitHandle = this.m_WaitHandle;
					if (waitHandle != null)
					{
						waitHandle.Set();
					}
				}
			}

			// Token: 0x040006A6 RID: 1702
			private readonly SendOrPostCallback m_DelagateCallback;

			// Token: 0x040006A7 RID: 1703
			private readonly object m_DelagateState;

			// Token: 0x040006A8 RID: 1704
			private readonly ManualResetEvent m_WaitHandle;
		}
	}
}
