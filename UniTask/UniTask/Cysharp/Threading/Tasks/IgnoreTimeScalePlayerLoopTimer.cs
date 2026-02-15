using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000067 RID: 103
	internal sealed class IgnoreTimeScalePlayerLoopTimer : PlayerLoopTimer
	{
		// Token: 0x06000160 RID: 352 RVA: 0x000051EC File Offset: 0x000033EC
		public IgnoreTimeScalePlayerLoopTimer(TimeSpan interval, bool periodic, PlayerLoopTiming playerLoopTiming, CancellationToken cancellationToken, Action<object> timerCallback, object state)
			: base(periodic, playerLoopTiming, cancellationToken, timerCallback, state)
		{
			this.ResetCore(new TimeSpan?(interval));
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000052A0 File Offset: 0x000034A0
		protected override bool MoveNextCore()
		{
			if (this.elapsed == 0f && this.initialFrame == Time.frameCount)
			{
				return true;
			}
			this.elapsed += Time.unscaledDeltaTime;
			return this.elapsed < this.interval;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000052EC File Offset: 0x000034EC
		protected override void ResetCore(TimeSpan? interval)
		{
			this.elapsed = 0f;
			this.initialFrame = (PlayerLoopHelper.IsMainThread ? Time.frameCount : (-1));
			if (interval != null)
			{
				this.interval = (float)interval.Value.TotalSeconds;
			}
		}

		// Token: 0x040000DB RID: 219
		private int initialFrame;

		// Token: 0x040000DC RID: 220
		private float elapsed;

		// Token: 0x040000DD RID: 221
		private float interval;
	}
}
