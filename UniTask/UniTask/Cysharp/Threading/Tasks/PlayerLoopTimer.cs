using System;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000065 RID: 101
	public abstract class PlayerLoopTimer : IDisposable, IPlayerLoopItem
	{
		// Token: 0x06000153 RID: 339 RVA: 0x00005035 File Offset: 0x00003235
		protected PlayerLoopTimer(bool periodic, PlayerLoopTiming playerLoopTiming, CancellationToken cancellationToken, Action<object> timerCallback, object state)
		{
			this.periodic = periodic;
			this.playerLoopTiming = playerLoopTiming;
			this.cancellationToken = cancellationToken;
			this.timerCallback = timerCallback;
			this.state = state;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00005064 File Offset: 0x00003264
		public static PlayerLoopTimer Create(TimeSpan interval, bool periodic, DelayType delayType, PlayerLoopTiming playerLoopTiming, CancellationToken cancellationToken, Action<object> timerCallback, object state)
		{
			switch (delayType)
			{
			case DelayType.UnscaledDeltaTime:
				return new IgnoreTimeScalePlayerLoopTimer(interval, periodic, playerLoopTiming, cancellationToken, timerCallback, state);
			case DelayType.Realtime:
				return new RealtimePlayerLoopTimer(interval, periodic, playerLoopTiming, cancellationToken, timerCallback, state);
			}
			return new DeltaTimePlayerLoopTimer(interval, periodic, playerLoopTiming, cancellationToken, timerCallback, state);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000050B1 File Offset: 0x000032B1
		public static PlayerLoopTimer StartNew(TimeSpan interval, bool periodic, DelayType delayType, PlayerLoopTiming playerLoopTiming, CancellationToken cancellationToken, Action<object> timerCallback, object state)
		{
			PlayerLoopTimer playerLoopTimer = PlayerLoopTimer.Create(interval, periodic, delayType, playerLoopTiming, cancellationToken, timerCallback, state);
			playerLoopTimer.Restart();
			return playerLoopTimer;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000050C8 File Offset: 0x000032C8
		public void Restart()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException(null);
			}
			this.ResetCore(null);
			if (!this.isRunning)
			{
				this.isRunning = true;
				PlayerLoopHelper.AddAction(this.playerLoopTiming, this);
			}
			this.tryStop = false;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00005115 File Offset: 0x00003315
		public void Restart(TimeSpan interval)
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException(null);
			}
			this.ResetCore(new TimeSpan?(interval));
			if (!this.isRunning)
			{
				this.isRunning = true;
				PlayerLoopHelper.AddAction(this.playerLoopTiming, this);
			}
			this.tryStop = false;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005154 File Offset: 0x00003354
		public void Stop()
		{
			this.tryStop = true;
		}

		// Token: 0x06000159 RID: 345
		protected abstract void ResetCore(TimeSpan? newInterval);

		// Token: 0x0600015A RID: 346 RVA: 0x0000515D File Offset: 0x0000335D
		public void Dispose()
		{
			this.isDisposed = true;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00005168 File Offset: 0x00003368
		bool IPlayerLoopItem.MoveNext()
		{
			if (this.isDisposed)
			{
				this.isRunning = false;
				return false;
			}
			if (this.tryStop)
			{
				this.isRunning = false;
				return false;
			}
			if (this.cancellationToken.IsCancellationRequested)
			{
				this.isRunning = false;
				return false;
			}
			if (this.MoveNextCore())
			{
				return true;
			}
			this.timerCallback(this.state);
			if (this.periodic)
			{
				this.ResetCore(null);
				return true;
			}
			this.isRunning = false;
			return false;
		}

		// Token: 0x0600015C RID: 348
		protected abstract bool MoveNextCore();

		// Token: 0x040000D0 RID: 208
		private readonly CancellationToken cancellationToken;

		// Token: 0x040000D1 RID: 209
		private readonly Action<object> timerCallback;

		// Token: 0x040000D2 RID: 210
		private readonly object state;

		// Token: 0x040000D3 RID: 211
		private readonly PlayerLoopTiming playerLoopTiming;

		// Token: 0x040000D4 RID: 212
		private readonly bool periodic;

		// Token: 0x040000D5 RID: 213
		private bool isRunning;

		// Token: 0x040000D6 RID: 214
		private bool tryStop;

		// Token: 0x040000D7 RID: 215
		private bool isDisposed;
	}
}
