using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000038 RID: 56
	internal class InfiniteRuntimeClip : RuntimeElement
	{
		// Token: 0x0600023A RID: 570 RVA: 0x000080C2 File Offset: 0x000062C2
		public InfiniteRuntimeClip(Playable playable)
		{
			this.m_Playable = playable;
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600023B RID: 571 RVA: 0x000080D1 File Offset: 0x000062D1
		public override long intervalStart
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600023C RID: 572 RVA: 0x000080D5 File Offset: 0x000062D5
		public override long intervalEnd
		{
			get
			{
				return InfiniteRuntimeClip.kIntervalEnd;
			}
		}

		// Token: 0x1700009E RID: 158
		// (set) Token: 0x0600023D RID: 573 RVA: 0x000080DC File Offset: 0x000062DC
		public override bool enable
		{
			set
			{
				if (value)
				{
					this.m_Playable.Play<Playable>();
					return;
				}
				this.m_Playable.Pause<Playable>();
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000080F8 File Offset: 0x000062F8
		public override void EvaluateAt(double localTime, FrameData frameData)
		{
			this.m_Playable.SetTime(localTime);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00008106 File Offset: 0x00006306
		public override void DisableAt(double localTime, double rootDuration, FrameData frameData)
		{
			this.m_Playable.SetTime(localTime);
			this.enable = false;
		}

		// Token: 0x0400010D RID: 269
		private Playable m_Playable;

		// Token: 0x0400010E RID: 270
		private static readonly long kIntervalEnd = DiscreteTime.GetNearestTick(TimelineClip.kMaxTimeValue);
	}
}
