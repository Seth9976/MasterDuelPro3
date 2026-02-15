using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000103 RID: 259
	public struct ReturnToMainThread
	{
		// Token: 0x06000671 RID: 1649 RVA: 0x0001FA89 File Offset: 0x0001DC89
		public ReturnToMainThread(PlayerLoopTiming playerLoopTiming, CancellationToken cancellationToken)
		{
			this.playerLoopTiming = playerLoopTiming;
			this.cancellationToken = cancellationToken;
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0001FA99 File Offset: 0x0001DC99
		public ReturnToMainThread.Awaiter DisposeAsync()
		{
			return new ReturnToMainThread.Awaiter(this.playerLoopTiming, this.cancellationToken);
		}

		// Token: 0x04000402 RID: 1026
		private readonly PlayerLoopTiming playerLoopTiming;

		// Token: 0x04000403 RID: 1027
		private readonly CancellationToken cancellationToken;

		// Token: 0x02000104 RID: 260
		public readonly struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x06000673 RID: 1651 RVA: 0x0001FAAC File Offset: 0x0001DCAC
			public Awaiter(PlayerLoopTiming timing, CancellationToken cancellationToken)
			{
				this.timing = timing;
				this.cancellationToken = cancellationToken;
			}

			// Token: 0x06000674 RID: 1652 RVA: 0x0001FABC File Offset: 0x0001DCBC
			public ReturnToMainThread.Awaiter GetAwaiter()
			{
				return this;
			}

			// Token: 0x17000043 RID: 67
			// (get) Token: 0x06000675 RID: 1653 RVA: 0x0001FAC4 File Offset: 0x0001DCC4
			public bool IsCompleted
			{
				get
				{
					return PlayerLoopHelper.MainThreadId == Thread.CurrentThread.ManagedThreadId;
				}
			}

			// Token: 0x06000676 RID: 1654 RVA: 0x0001FAD8 File Offset: 0x0001DCD8
			public void GetResult()
			{
				this.cancellationToken.ThrowIfCancellationRequested();
			}

			// Token: 0x06000677 RID: 1655 RVA: 0x0001FAF3 File Offset: 0x0001DCF3
			public void OnCompleted(Action continuation)
			{
				PlayerLoopHelper.AddContinuation(this.timing, continuation);
			}

			// Token: 0x06000678 RID: 1656 RVA: 0x0001FAF3 File Offset: 0x0001DCF3
			public void UnsafeOnCompleted(Action continuation)
			{
				PlayerLoopHelper.AddContinuation(this.timing, continuation);
			}

			// Token: 0x04000404 RID: 1028
			private readonly PlayerLoopTiming timing;

			// Token: 0x04000405 RID: 1029
			private readonly CancellationToken cancellationToken;
		}
	}
}
