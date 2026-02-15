using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000105 RID: 261
	public struct SwitchToThreadPoolAwaitable
	{
		// Token: 0x06000679 RID: 1657 RVA: 0x0001FB04 File Offset: 0x0001DD04
		public SwitchToThreadPoolAwaitable.Awaiter GetAwaiter()
		{
			return default(SwitchToThreadPoolAwaitable.Awaiter);
		}

		// Token: 0x02000106 RID: 262
		public struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x17000044 RID: 68
			// (get) Token: 0x0600067A RID: 1658 RVA: 0x000030E1 File Offset: 0x000012E1
			public bool IsCompleted
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600067B RID: 1659 RVA: 0x000030EE File Offset: 0x000012EE
			public void GetResult()
			{
			}

			// Token: 0x0600067C RID: 1660 RVA: 0x0001FB1A File Offset: 0x0001DD1A
			public void OnCompleted(Action continuation)
			{
				ThreadPool.QueueUserWorkItem(SwitchToThreadPoolAwaitable.Awaiter.switchToCallback, continuation);
			}

			// Token: 0x0600067D RID: 1661 RVA: 0x0001FB28 File Offset: 0x0001DD28
			public void UnsafeOnCompleted(Action continuation)
			{
				ThreadPool.UnsafeQueueUserWorkItem(SwitchToThreadPoolAwaitable.Awaiter.switchToCallback, continuation);
			}

			// Token: 0x0600067E RID: 1662 RVA: 0x0001FB36 File Offset: 0x0001DD36
			private static void Callback(object state)
			{
				((Action)state)();
			}

			// Token: 0x04000406 RID: 1030
			private static readonly WaitCallback switchToCallback = new WaitCallback(SwitchToThreadPoolAwaitable.Awaiter.Callback);
		}
	}
}
