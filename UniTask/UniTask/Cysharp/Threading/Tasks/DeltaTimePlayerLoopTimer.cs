using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000066 RID: 102
	internal sealed class DeltaTimePlayerLoopTimer : PlayerLoopTimer
	{
		// Token: 0x0600015D RID: 349 RVA: 0x000051EC File Offset: 0x000033EC
		public DeltaTimePlayerLoopTimer(TimeSpan interval, bool periodic, PlayerLoopTiming playerLoopTiming, CancellationToken cancellationToken, Action<object> timerCallback, object state)
			: base(periodic, playerLoopTiming, cancellationToken, timerCallback, state)
		{
			this.ResetCore(new TimeSpan?(interval));
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00005208 File Offset: 0x00003408
		protected override bool MoveNextCore()
		{
			if (this.elapsed == 0f && this.initialFrame == Time.frameCount)
			{
				return true;
			}
			this.elapsed += Time.deltaTime;
			return this.elapsed < this.interval;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00005254 File Offset: 0x00003454
		protected override void ResetCore(TimeSpan? interval)
		{
			this.elapsed = 0f;
			this.initialFrame = (PlayerLoopHelper.IsMainThread ? Time.frameCount : (-1));
			if (interval != null)
			{
				this.interval = (float)interval.Value.TotalSeconds;
			}
		}

		// Token: 0x040000D8 RID: 216
		private int initialFrame;

		// Token: 0x040000D9 RID: 217
		private float elapsed;

		// Token: 0x040000DA RID: 218
		private float interval;
	}
}
