using System;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000071 RID: 113
	public sealed class TimeoutController : IDisposable
	{
		// Token: 0x06000181 RID: 385 RVA: 0x00003469 File Offset: 0x00001669
		private static void CancelCancellationTokenSourceState(object state)
		{
			((CancellationTokenSource)state).Cancel();
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000057DB File Offset: 0x000039DB
		public TimeoutController(DelayType delayType = DelayType.DeltaTime, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update)
		{
			this.timeoutSource = new CancellationTokenSource();
			this.originalLinkCancellationTokenSource = null;
			this.linkedSource = null;
			this.delayType = delayType;
			this.delayTiming = delayTiming;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000580C File Offset: 0x00003A0C
		public TimeoutController(CancellationTokenSource linkCancellationTokenSource, DelayType delayType = DelayType.DeltaTime, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update)
		{
			this.timeoutSource = new CancellationTokenSource();
			this.originalLinkCancellationTokenSource = linkCancellationTokenSource;
			this.linkedSource = CancellationTokenSource.CreateLinkedTokenSource(this.timeoutSource.Token, linkCancellationTokenSource.Token);
			this.delayType = delayType;
			this.delayTiming = delayTiming;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000585B File Offset: 0x00003A5B
		public CancellationToken Timeout(int millisecondsTimeout)
		{
			return this.Timeout(TimeSpan.FromMilliseconds((double)millisecondsTimeout));
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000586C File Offset: 0x00003A6C
		public CancellationToken Timeout(TimeSpan timeout)
		{
			if (this.originalLinkCancellationTokenSource != null && this.originalLinkCancellationTokenSource.IsCancellationRequested)
			{
				return this.originalLinkCancellationTokenSource.Token;
			}
			if (this.timeoutSource.IsCancellationRequested)
			{
				this.timeoutSource.Dispose();
				this.timeoutSource = new CancellationTokenSource();
				if (this.linkedSource != null)
				{
					this.linkedSource.Cancel();
					this.linkedSource.Dispose();
					this.linkedSource = CancellationTokenSource.CreateLinkedTokenSource(this.timeoutSource.Token, this.originalLinkCancellationTokenSource.Token);
				}
				PlayerLoopTimer playerLoopTimer = this.timer;
				if (playerLoopTimer != null)
				{
					playerLoopTimer.Dispose();
				}
				this.timer = null;
			}
			CancellationToken token = ((this.linkedSource != null) ? this.linkedSource : this.timeoutSource).Token;
			if (this.timer == null)
			{
				this.timer = PlayerLoopTimer.StartNew(timeout, false, this.delayType, this.delayTiming, token, TimeoutController.CancelCancellationTokenSourceStateDelegate, this.timeoutSource);
			}
			else
			{
				this.timer.Restart(timeout);
			}
			return token;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000596C File Offset: 0x00003B6C
		public bool IsTimeout()
		{
			return this.timeoutSource.IsCancellationRequested;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00005979 File Offset: 0x00003B79
		public void Reset()
		{
			PlayerLoopTimer playerLoopTimer = this.timer;
			if (playerLoopTimer == null)
			{
				return;
			}
			playerLoopTimer.Stop();
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000598C File Offset: 0x00003B8C
		public void Dispose()
		{
			if (this.isDisposed)
			{
				return;
			}
			try
			{
				PlayerLoopTimer playerLoopTimer = this.timer;
				if (playerLoopTimer != null)
				{
					playerLoopTimer.Dispose();
				}
				this.timeoutSource.Cancel();
				this.timeoutSource.Dispose();
				if (this.linkedSource != null)
				{
					this.linkedSource.Cancel();
					this.linkedSource.Dispose();
				}
			}
			finally
			{
				this.isDisposed = true;
			}
		}

		// Token: 0x040000F1 RID: 241
		private static readonly Action<object> CancelCancellationTokenSourceStateDelegate = new Action<object>(TimeoutController.CancelCancellationTokenSourceState);

		// Token: 0x040000F2 RID: 242
		private CancellationTokenSource timeoutSource;

		// Token: 0x040000F3 RID: 243
		private CancellationTokenSource linkedSource;

		// Token: 0x040000F4 RID: 244
		private PlayerLoopTimer timer;

		// Token: 0x040000F5 RID: 245
		private bool isDisposed;

		// Token: 0x040000F6 RID: 246
		private readonly DelayType delayType;

		// Token: 0x040000F7 RID: 247
		private readonly PlayerLoopTiming delayTiming;

		// Token: 0x040000F8 RID: 248
		private readonly CancellationTokenSource originalLinkCancellationTokenSource;
	}
}
