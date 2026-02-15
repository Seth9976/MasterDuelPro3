using System;
using UnityEngine;
using UnityEngine.Playables;
using YgomSystem.UI;

namespace YgomSystem.Timeline
{
	// Token: 0x020006BB RID: 1723
	[Serializable]
	public class SkipClickClip : PlayableAsset
	{
		// Token: 0x060035D0 RID: 13776 RVA: 0x000F3410 File Offset: 0x000F1610
		public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
		{
			return default(Playable);
		}

		// Token: 0x040030F8 RID: 12536
		public SkipClickBehaviour template;

		// Token: 0x040030F9 RID: 12537
		[NonSerialized]
		public double endTime;

		// Token: 0x040030FA RID: 12538
		[NonSerialized]
		public SelectionButton waitButton;
	}
}
