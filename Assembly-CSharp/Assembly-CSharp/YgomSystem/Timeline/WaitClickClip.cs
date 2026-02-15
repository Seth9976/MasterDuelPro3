using System;
using UnityEngine;
using UnityEngine.Playables;
using YgomSystem.UI;

namespace YgomSystem.Timeline
{
	// Token: 0x020006CB RID: 1739
	[Serializable]
	public class WaitClickClip : PlayableAsset
	{
		// Token: 0x0600363E RID: 13886 RVA: 0x000F3584 File Offset: 0x000F1784
		public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
		{
			return default(Playable);
		}

		// Token: 0x0400312B RID: 12587
		public WaitClickBehaviour template;

		// Token: 0x0400312C RID: 12588
		[NonSerialized]
		public double startTime;

		// Token: 0x0400312D RID: 12589
		[NonSerialized]
		public double endTime;

		// Token: 0x0400312E RID: 12590
		[NonSerialized]
		public SelectionButton waitButton;
	}
}
