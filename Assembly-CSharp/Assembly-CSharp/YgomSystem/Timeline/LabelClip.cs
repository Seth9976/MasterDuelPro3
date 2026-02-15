using System;
using UnityEngine;
using UnityEngine.Playables;

namespace YgomSystem.Timeline
{
	// Token: 0x020006A9 RID: 1705
	[Serializable]
	public class LabelClip : PlayableAsset
	{
		// Token: 0x0600356A RID: 13674 RVA: 0x000F2F58 File Offset: 0x000F1158
		public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
		{
			ScriptPlayable<LabelBehaviour> playable = ScriptPlayable<LabelBehaviour>.Create(graph, 0);
			this.template = playable.GetBehaviour();
			return playable;
		}

		// Token: 0x040030C8 RID: 12488
		public LabelBehaviour template;
	}
}
