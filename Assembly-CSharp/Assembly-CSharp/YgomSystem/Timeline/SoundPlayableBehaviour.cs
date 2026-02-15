using System;
using MDPro3;
using UnityEngine.Playables;

namespace YgomSystem.Timeline
{
	// Token: 0x020006C1 RID: 1729
	public class SoundPlayableBehaviour : PlayableBehaviour
	{
		// Token: 0x060035DE RID: 13790 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnGraphStart(Playable playable)
		{
		}

		// Token: 0x060035DF RID: 13791 RVA: 0x000F34B0 File Offset: 0x000F16B0
		public override void OnBehaviourPlay(Playable playable, FrameData info)
		{
			AudioManager.PlaySE(this.startLabel, 1f);
		}

		// Token: 0x04003101 RID: 12545
		public SoundPlayableAsset playableAsset;

		// Token: 0x04003102 RID: 12546
		public string startLabel;

		// Token: 0x04003103 RID: 12547
		public bool skipDuplicate;
	}
}
