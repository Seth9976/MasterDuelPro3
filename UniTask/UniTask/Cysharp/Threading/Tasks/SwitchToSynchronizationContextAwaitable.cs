using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000109 RID: 265
	public struct SwitchToSynchronizationContextAwaitable
	{
		// Token: 0x06000687 RID: 1671 RVA: 0x0001FB9F File Offset: 0x0001DD9F
		public SwitchToSynchronizationContextAwaitable(SynchronizationContext synchronizationContext, CancellationToken cancellationToken)
		{
			this.synchronizationContext = synchronizationContext;
			this.cancellationToken = cancellationToken;
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0001FBAF File Offset: 0x0001DDAF
		public SwitchToSynchronizationContextAwaitable.Awaiter GetAwaiter()
		{
			return new SwitchToSynchronizationContextAwaitable.Awaiter(this.synchronizationContext, this.cancellationToken);
		}

		// Token: 0x04000408 RID: 1032
		private readonly SynchronizationContext synchronizationContext;

		// Token: 0x04000409 RID: 1033
		private readonly CancellationToken cancellationToken;

		// Token: 0x0200010A RID: 266
		public struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x06000689 RID: 1673 RVA: 0x0001FBC2 File Offset: 0x0001DDC2
			public Awaiter(SynchronizationContext synchronizationContext, CancellationToken cancellationToken)
			{
				this.synchronizationContext = synchronizationContext;
				this.cancellationToken = cancellationToken;
			}

			// Token: 0x17000046 RID: 70
			// (get) Token: 0x0600068A RID: 1674 RVA: 0x000030E1 File Offset: 0x000012E1
			public bool IsCompleted
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600068B RID: 1675 RVA: 0x0001FBD4 File Offset: 0x0001DDD4
			public void GetResult()
			{
				this.cancellationToken.ThrowIfCancellationRequested();
			}

			// Token: 0x0600068C RID: 1676 RVA: 0x0001FBEF File Offset: 0x0001DDEF
			public void OnCompleted(Action continuation)
			{
				this.synchronizationContext.Post(SwitchToSynchronizationContextAwaitable.Awaiter.switchToCallback, continuation);
			}

			// Token: 0x0600068D RID: 1677 RVA: 0x0001FBEF File Offset: 0x0001DDEF
			public void UnsafeOnCompleted(Action continuation)
			{
				this.synchronizationContext.Post(SwitchToSynchronizationContextAwaitable.Awaiter.switchToCallback, continuation);
			}

			// Token: 0x0600068E RID: 1678 RVA: 0x0001FB36 File Offset: 0x0001DD36
			private static void Callback(object state)
			{
				((Action)state)();
			}

			// Token: 0x0400040A RID: 1034
			private static readonly SendOrPostCallback switchToCallback = new SendOrPostCallback(SwitchToSynchronizationContextAwaitable.Awaiter.Callback);

			// Token: 0x0400040B RID: 1035
			private readonly SynchronizationContext synchronizationContext;

			// Token: 0x0400040C RID: 1036
			private readonly CancellationToken cancellationToken;
		}
	}
}
