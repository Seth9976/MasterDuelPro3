using System;
using UnityEngine.Playables;
using YgomSystem.UI;

namespace YgomSystem.Timeline
{
	// Token: 0x020006CA RID: 1738
	[Serializable]
	public class WaitClickBehaviour : PlayableBehaviour
	{
		// Token: 0x06003635 RID: 13877 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnPlayableCreate(Playable playable)
		{
		}

		// Token: 0x06003636 RID: 13878 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnGraphStart(Playable playable)
		{
		}

		// Token: 0x06003637 RID: 13879 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnGraphStop(Playable playable)
		{
		}

		// Token: 0x06003638 RID: 13880 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnBehaviourPlay(Playable playable, FrameData info)
		{
		}

		// Token: 0x06003639 RID: 13881 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnBehaviourPause(Playable playable, FrameData info)
		{
		}

		// Token: 0x0600363A RID: 13882 RVA: 0x0000216D File Offset: 0x0000036D
		public override void PrepareFrame(Playable playable, FrameData info)
		{
		}

		// Token: 0x0600363B RID: 13883 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnPlayableDestroy(Playable playable)
		{
		}

		// Token: 0x0600363C RID: 13884 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnClicked()
		{
		}

		// Token: 0x04003123 RID: 12579
		private PlayableDirector m_Director;

		// Token: 0x04003124 RID: 12580
		private bool m_WaitTrigger;

		// Token: 0x04003125 RID: 12581
		private bool m_Waited;

		// Token: 0x04003126 RID: 12582
		private bool m_IsLooping;

		// Token: 0x04003127 RID: 12583
		public bool isLoop;

		// Token: 0x04003128 RID: 12584
		[NonSerialized]
		public double startTime;

		// Token: 0x04003129 RID: 12585
		[NonSerialized]
		public double endTime;

		// Token: 0x0400312A RID: 12586
		[NonSerialized]
		public SelectionButton waitButton;
	}
}
