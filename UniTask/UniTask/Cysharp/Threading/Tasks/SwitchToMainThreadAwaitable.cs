using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000101 RID: 257
	public struct SwitchToMainThreadAwaitable
	{
		// Token: 0x0600066A RID: 1642 RVA: 0x0001FA07 File Offset: 0x0001DC07
		public SwitchToMainThreadAwaitable(PlayerLoopTiming playerLoopTiming, CancellationToken cancellationToken)
		{
			this.playerLoopTiming = playerLoopTiming;
			this.cancellationToken = cancellationToken;
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0001FA17 File Offset: 0x0001DC17
		public SwitchToMainThreadAwaitable.Awaiter GetAwaiter()
		{
			return new SwitchToMainThreadAwaitable.Awaiter(this.playerLoopTiming, this.cancellationToken);
		}

		// Token: 0x040003FE RID: 1022
		private readonly PlayerLoopTiming playerLoopTiming;

		// Token: 0x040003FF RID: 1023
		private readonly CancellationToken cancellationToken;

		// Token: 0x02000102 RID: 258
		public struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x0600066C RID: 1644 RVA: 0x0001FA2A File Offset: 0x0001DC2A
			public Awaiter(PlayerLoopTiming playerLoopTiming, CancellationToken cancellationToken)
			{
				this.playerLoopTiming = playerLoopTiming;
				this.cancellationToken = cancellationToken;
			}

			// Token: 0x17000042 RID: 66
			// (get) Token: 0x0600066D RID: 1645 RVA: 0x0001FA3C File Offset: 0x0001DC3C
			public bool IsCompleted
			{
				get
				{
					int currentThreadId = Thread.CurrentThread.ManagedThreadId;
					return PlayerLoopHelper.MainThreadId == currentThreadId;
				}
			}

			// Token: 0x0600066E RID: 1646 RVA: 0x0001FA60 File Offset: 0x0001DC60
			public void GetResult()
			{
				this.cancellationToken.ThrowIfCancellationRequested();
			}

			// Token: 0x0600066F RID: 1647 RVA: 0x0001FA7B File Offset: 0x0001DC7B
			public void OnCompleted(Action continuation)
			{
				PlayerLoopHelper.AddContinuation(this.playerLoopTiming, continuation);
			}

			// Token: 0x06000670 RID: 1648 RVA: 0x0001FA7B File Offset: 0x0001DC7B
			public void UnsafeOnCompleted(Action continuation)
			{
				PlayerLoopHelper.AddContinuation(this.playerLoopTiming, continuation);
			}

			// Token: 0x04000400 RID: 1024
			private readonly PlayerLoopTiming playerLoopTiming;

			// Token: 0x04000401 RID: 1025
			private readonly CancellationToken cancellationToken;
		}
	}
}
