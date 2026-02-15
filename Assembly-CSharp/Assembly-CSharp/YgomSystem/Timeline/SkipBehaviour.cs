using System;
using UnityEngine.Playables;

namespace YgomSystem.Timeline
{
	// Token: 0x020006B9 RID: 1721
	[Serializable]
	public class SkipBehaviour : PlayableBehaviour
	{
		// Token: 0x060035C2 RID: 13762 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnPlayableCreate(Playable playable)
		{
		}

		// Token: 0x060035C3 RID: 13763 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnGraphStart(Playable playable)
		{
		}

		// Token: 0x060035C4 RID: 13764 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnGraphStop(Playable playable)
		{
		}

		// Token: 0x060035C5 RID: 13765 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnBehaviourPlay(Playable playable, FrameData info)
		{
		}

		// Token: 0x060035C6 RID: 13766 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnBehaviourPause(Playable playable, FrameData info)
		{
		}

		// Token: 0x060035C7 RID: 13767 RVA: 0x0000216D File Offset: 0x0000036D
		public override void PrepareFrame(Playable playable, FrameData info)
		{
		}

		// Token: 0x060035C8 RID: 13768 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnPlayableDestroy(Playable playable)
		{
		}

		// Token: 0x040030F3 RID: 12531
		private PlayableDirector m_Director;

		// Token: 0x040030F4 RID: 12532
		[NonSerialized]
		public double endTime;
	}
}
