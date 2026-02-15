using System;
using UnityEngine.Playables;

namespace YgomSystem.Timeline
{
	// Token: 0x020006C3 RID: 1731
	public class SoundStopPlayableBehaviour : PlayableBehaviour
	{
		// Token: 0x060035E4 RID: 13796 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnGraphStart(Playable playable)
		{
		}

		// Token: 0x060035E5 RID: 13797 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnBehaviourPlay(Playable playable, FrameData info)
		{
		}

		// Token: 0x04003107 RID: 12551
		public SoundStopPlayableAsset playableAsset;

		// Token: 0x04003108 RID: 12552
		public string stopLabel;

		// Token: 0x04003109 RID: 12553
		public bool skipDuplicate;

		// Token: 0x0400310A RID: 12554
		public float fade;

		// Token: 0x0400310B RID: 12555
		private bool stopped;
	}
}
