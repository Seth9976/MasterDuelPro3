using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000107 RID: 263
	public struct SwitchToTaskPoolAwaitable
	{
		// Token: 0x06000680 RID: 1664 RVA: 0x0001FB58 File Offset: 0x0001DD58
		public SwitchToTaskPoolAwaitable.Awaiter GetAwaiter()
		{
			return default(SwitchToTaskPoolAwaitable.Awaiter);
		}

		// Token: 0x02000108 RID: 264
		public struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x17000045 RID: 69
			// (get) Token: 0x06000681 RID: 1665 RVA: 0x000030E1 File Offset: 0x000012E1
			public bool IsCompleted
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000682 RID: 1666 RVA: 0x000030EE File Offset: 0x000012EE
			public void GetResult()
			{
			}

			// Token: 0x06000683 RID: 1667 RVA: 0x0001FB6E File Offset: 0x0001DD6E
			public void OnCompleted(Action continuation)
			{
				Task.Factory.StartNew(SwitchToTaskPoolAwaitable.Awaiter.switchToCallback, continuation, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
			}

			// Token: 0x06000684 RID: 1668 RVA: 0x0001FB6E File Offset: 0x0001DD6E
			public void UnsafeOnCompleted(Action continuation)
			{
				Task.Factory.StartNew(SwitchToTaskPoolAwaitable.Awaiter.switchToCallback, continuation, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
			}

			// Token: 0x06000685 RID: 1669 RVA: 0x0001FB36 File Offset: 0x0001DD36
			private static void Callback(object state)
			{
				((Action)state)();
			}

			// Token: 0x04000407 RID: 1031
			private static readonly Action<object> switchToCallback = new Action<object>(SwitchToTaskPoolAwaitable.Awaiter.Callback);
		}
	}
}
