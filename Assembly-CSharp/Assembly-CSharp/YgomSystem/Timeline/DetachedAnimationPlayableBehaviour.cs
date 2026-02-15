using System;
using UnityEngine;
using UnityEngine.Playables;

namespace YgomSystem.Timeline
{
	// Token: 0x0200069F RID: 1695
	[Serializable]
	public class DetachedAnimationPlayableBehaviour : PlayableBehaviour
	{
		// Token: 0x06003550 RID: 13648 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnPlayableCreate(Playable playable)
		{
		}

		// Token: 0x06003551 RID: 13649 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnBehaviourPlay(Playable playable, FrameData info)
		{
		}

		// Token: 0x06003552 RID: 13650 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnBehaviourPause(Playable playable, FrameData info)
		{
		}

		// Token: 0x06003553 RID: 13651 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnPlayableDestroy(Playable playable)
		{
		}

		// Token: 0x0400307D RID: 12413
		public AnimationClip animClip;

		// Token: 0x0400307E RID: 12414
		[NonSerialized]
		public Animator target;

		// Token: 0x0400307F RID: 12415
		private PlayableGraph m_PlayableGraph;
	}
}
