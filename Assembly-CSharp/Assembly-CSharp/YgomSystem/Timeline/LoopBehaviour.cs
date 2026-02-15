using System;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace YgomSystem.Timeline
{
	// Token: 0x020006AF RID: 1711
	[Serializable]
	public class LoopBehaviour : PlayableBehaviour
	{
		// Token: 0x06003595 RID: 13717 RVA: 0x000F3074 File Offset: 0x000F1274
		public override void PrepareFrame(Playable playable, FrameData info)
		{
			if (this.m_director == null)
			{
				this.m_director = playable.GetGraph<Playable>().GetResolver() as PlayableDirector;
			}
			if (this.loopClip != null && this.m_director != null && this.m_director.time > this.loopClip.extrapolatedStart + this.loopClip.duration)
			{
				this.m_director.time = this.loopClip.extrapolatedStart;
			}
		}

		// Token: 0x040030DC RID: 12508
		private PlayableDirector m_director;

		// Token: 0x040030DD RID: 12509
		public TimelineClip loopClip;
	}
}
