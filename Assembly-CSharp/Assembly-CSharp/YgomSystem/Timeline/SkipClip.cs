using System;
using UnityEngine;
using UnityEngine.Playables;

namespace YgomSystem.Timeline
{
	// Token: 0x020006BE RID: 1726
	[Serializable]
	public class SkipClip : PlayableAsset
	{
		// Token: 0x060035D6 RID: 13782 RVA: 0x000F3440 File Offset: 0x000F1640
		public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
		{
			return default(Playable);
		}

		// Token: 0x040030FB RID: 12539
		public SkipBehaviour template;

		// Token: 0x040030FC RID: 12540
		[NonSerialized]
		public double endTime;
	}
}
