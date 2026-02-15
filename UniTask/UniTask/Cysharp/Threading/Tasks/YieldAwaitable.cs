using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x020000FE RID: 254
	public readonly struct YieldAwaitable
	{
		// Token: 0x06000661 RID: 1633 RVA: 0x0001F972 File Offset: 0x0001DB72
		public YieldAwaitable(PlayerLoopTiming timing)
		{
			this.timing = timing;
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0001F97B File Offset: 0x0001DB7B
		public YieldAwaitable.Awaiter GetAwaiter()
		{
			return new YieldAwaitable.Awaiter(this.timing);
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0001F988 File Offset: 0x0001DB88
		public UniTask ToUniTask()
		{
			return UniTask.Yield(this.timing, CancellationToken.None, false);
		}

		// Token: 0x040003F6 RID: 1014
		private readonly PlayerLoopTiming timing;

		// Token: 0x020000FF RID: 255
		public readonly struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x06000664 RID: 1636 RVA: 0x0001F99B File Offset: 0x0001DB9B
			public Awaiter(PlayerLoopTiming timing)
			{
				this.timing = timing;
			}

			// Token: 0x17000041 RID: 65
			// (get) Token: 0x06000665 RID: 1637 RVA: 0x000030E1 File Offset: 0x000012E1
			public bool IsCompleted
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000666 RID: 1638 RVA: 0x000030EE File Offset: 0x000012EE
			public void GetResult()
			{
			}

			// Token: 0x06000667 RID: 1639 RVA: 0x0001F9A4 File Offset: 0x0001DBA4
			public void OnCompleted(Action continuation)
			{
				PlayerLoopHelper.AddContinuation(this.timing, continuation);
			}

			// Token: 0x06000668 RID: 1640 RVA: 0x0001F9A4 File Offset: 0x0001DBA4
			public void UnsafeOnCompleted(Action continuation)
			{
				PlayerLoopHelper.AddContinuation(this.timing, continuation);
			}

			// Token: 0x040003F7 RID: 1015
			private readonly PlayerLoopTiming timing;
		}
	}
}
