using System;
using System.Threading;
using Cysharp.Threading.Tasks.Internal;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000068 RID: 104
	internal sealed class RealtimePlayerLoopTimer : PlayerLoopTimer
	{
		// Token: 0x06000163 RID: 355 RVA: 0x000051EC File Offset: 0x000033EC
		public RealtimePlayerLoopTimer(TimeSpan interval, bool periodic, PlayerLoopTiming playerLoopTiming, CancellationToken cancellationToken, Action<object> timerCallback, object state)
			: base(periodic, playerLoopTiming, cancellationToken, timerCallback, state)
		{
			this.ResetCore(new TimeSpan?(interval));
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00005338 File Offset: 0x00003538
		protected override bool MoveNextCore()
		{
			return this.stopwatch.ElapsedTicks < this.intervalTicks;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00005350 File Offset: 0x00003550
		protected override void ResetCore(TimeSpan? interval)
		{
			this.stopwatch = ValueStopwatch.StartNew();
			if (interval != null)
			{
				this.intervalTicks = interval.Value.Ticks;
			}
		}

		// Token: 0x040000DE RID: 222
		private ValueStopwatch stopwatch;

		// Token: 0x040000DF RID: 223
		private long intervalTicks;
	}
}
