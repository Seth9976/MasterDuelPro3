using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000019 RID: 25
	public struct CancellationTokenAwaitable
	{
		// Token: 0x06000098 RID: 152 RVA: 0x00003416 File Offset: 0x00001616
		public CancellationTokenAwaitable(CancellationToken cancellationToken)
		{
			this.cancellationToken = cancellationToken;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000341F File Offset: 0x0000161F
		public CancellationTokenAwaitable.Awaiter GetAwaiter()
		{
			return new CancellationTokenAwaitable.Awaiter(this.cancellationToken);
		}

		// Token: 0x04000054 RID: 84
		private CancellationToken cancellationToken;

		// Token: 0x0200001A RID: 26
		public struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x0600009A RID: 154 RVA: 0x0000342C File Offset: 0x0000162C
			public Awaiter(CancellationToken cancellationToken)
			{
				this.cancellationToken = cancellationToken;
			}

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x0600009B RID: 155 RVA: 0x00003435 File Offset: 0x00001635
			public bool IsCompleted
			{
				get
				{
					return !this.cancellationToken.CanBeCanceled || this.cancellationToken.IsCancellationRequested;
				}
			}

			// Token: 0x0600009C RID: 156 RVA: 0x000030EE File Offset: 0x000012EE
			public void GetResult()
			{
			}

			// Token: 0x0600009D RID: 157 RVA: 0x00003451 File Offset: 0x00001651
			public void OnCompleted(Action continuation)
			{
				this.UnsafeOnCompleted(continuation);
			}

			// Token: 0x0600009E RID: 158 RVA: 0x0000345A File Offset: 0x0000165A
			public void UnsafeOnCompleted(Action continuation)
			{
				this.cancellationToken.RegisterWithoutCaptureExecutionContext(continuation);
			}

			// Token: 0x04000055 RID: 85
			private CancellationToken cancellationToken;
		}
	}
}
