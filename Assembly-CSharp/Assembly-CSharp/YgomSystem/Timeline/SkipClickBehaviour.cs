using System;
using UnityEngine.Playables;
using YgomSystem.UI;

namespace YgomSystem.Timeline
{
	// Token: 0x020006BA RID: 1722
	[Serializable]
	public class SkipClickBehaviour : PlayableBehaviour
	{
		// Token: 0x060035CA RID: 13770 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnPlayableCreate(Playable playable)
		{
		}

		// Token: 0x060035CB RID: 13771 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnBehaviourPlay(Playable playable, FrameData info)
		{
		}

		// Token: 0x060035CC RID: 13772 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnBehaviourPause(Playable playable, FrameData info)
		{
		}

		// Token: 0x060035CD RID: 13773 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnPlayableDestroy(Playable playable)
		{
		}

		// Token: 0x060035CE RID: 13774 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnClicked()
		{
		}

		// Token: 0x040030F5 RID: 12533
		private PlayableDirector m_Director;

		// Token: 0x040030F6 RID: 12534
		[NonSerialized]
		public double endTime;

		// Token: 0x040030F7 RID: 12535
		[NonSerialized]
		public SelectionButton waitButton;
	}
}
