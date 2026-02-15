using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200010B RID: 267
	public struct ReturnToSynchronizationContext
	{
		// Token: 0x06000690 RID: 1680 RVA: 0x0001FC15 File Offset: 0x0001DE15
		public ReturnToSynchronizationContext(SynchronizationContext syncContext, bool dontPostWhenSameContext, CancellationToken cancellationToken)
		{
			this.syncContext = syncContext;
			this.dontPostWhenSameContext = dontPostWhenSameContext;
			this.cancellationToken = cancellationToken;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0001FC2C File Offset: 0x0001DE2C
		public ReturnToSynchronizationContext.Awaiter DisposeAsync()
		{
			return new ReturnToSynchronizationContext.Awaiter(this.syncContext, this.dontPostWhenSameContext, this.cancellationToken);
		}

		// Token: 0x0400040D RID: 1037
		private readonly SynchronizationContext syncContext;

		// Token: 0x0400040E RID: 1038
		private readonly bool dontPostWhenSameContext;

		// Token: 0x0400040F RID: 1039
		private readonly CancellationToken cancellationToken;

		// Token: 0x0200010C RID: 268
		public struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x06000692 RID: 1682 RVA: 0x0001FC45 File Offset: 0x0001DE45
			public Awaiter(SynchronizationContext synchronizationContext, bool dontPostWhenSameContext, CancellationToken cancellationToken)
			{
				this.synchronizationContext = synchronizationContext;
				this.dontPostWhenSameContext = dontPostWhenSameContext;
				this.cancellationToken = cancellationToken;
			}

			// Token: 0x06000693 RID: 1683 RVA: 0x0001FC5C File Offset: 0x0001DE5C
			public ReturnToSynchronizationContext.Awaiter GetAwaiter()
			{
				return this;
			}

			// Token: 0x17000047 RID: 71
			// (get) Token: 0x06000694 RID: 1684 RVA: 0x0001FC64 File Offset: 0x0001DE64
			public bool IsCompleted
			{
				get
				{
					return this.dontPostWhenSameContext && SynchronizationContext.Current == this.synchronizationContext;
				}
			}

			// Token: 0x06000695 RID: 1685 RVA: 0x0001FC80 File Offset: 0x0001DE80
			public void GetResult()
			{
				this.cancellationToken.ThrowIfCancellationRequested();
			}

			// Token: 0x06000696 RID: 1686 RVA: 0x0001FC9B File Offset: 0x0001DE9B
			public void OnCompleted(Action continuation)
			{
				this.synchronizationContext.Post(ReturnToSynchronizationContext.Awaiter.switchToCallback, continuation);
			}

			// Token: 0x06000697 RID: 1687 RVA: 0x0001FC9B File Offset: 0x0001DE9B
			public void UnsafeOnCompleted(Action continuation)
			{
				this.synchronizationContext.Post(ReturnToSynchronizationContext.Awaiter.switchToCallback, continuation);
			}

			// Token: 0x06000698 RID: 1688 RVA: 0x0001FB36 File Offset: 0x0001DD36
			private static void Callback(object state)
			{
				((Action)state)();
			}

			// Token: 0x04000410 RID: 1040
			private static readonly SendOrPostCallback switchToCallback = new SendOrPostCallback(ReturnToSynchronizationContext.Awaiter.Callback);

			// Token: 0x04000411 RID: 1041
			private readonly SynchronizationContext synchronizationContext;

			// Token: 0x04000412 RID: 1042
			private readonly bool dontPostWhenSameContext;

			// Token: 0x04000413 RID: 1043
			private readonly CancellationToken cancellationToken;
		}
	}
}
