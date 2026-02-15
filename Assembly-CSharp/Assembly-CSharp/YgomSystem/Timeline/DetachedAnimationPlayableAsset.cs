using System;
using UnityEngine;
using UnityEngine.Playables;

namespace YgomSystem.Timeline
{
	// Token: 0x0200069E RID: 1694
	public class DetachedAnimationPlayableAsset : PlayableAsset
	{
		// Token: 0x0600354E RID: 13646 RVA: 0x000F2C98 File Offset: 0x000F0E98
		public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
		{
			return default(Playable);
		}

		// Token: 0x0400307C RID: 12412
		public DetachedAnimationPlayableBehaviour template;
	}
}
