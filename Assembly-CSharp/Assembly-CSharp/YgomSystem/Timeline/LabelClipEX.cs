using System;
using UnityEngine;
using UnityEngine.Playables;

namespace YgomSystem.Timeline
{
	// Token: 0x020006AA RID: 1706
	[Serializable]
	public class LabelClipEX : LabelClip
	{
		// Token: 0x0600356C RID: 13676 RVA: 0x000F2F80 File Offset: 0x000F1180
		public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
		{
			ScriptPlayable<LabelBehaviour> playable = ScriptPlayable<LabelBehaviour>.Create(graph, 0);
			this.template = playable.GetBehaviour();
			return playable;
		}

		// Token: 0x040030C9 RID: 12489
		public bool overrideWrapMode;

		// Token: 0x040030CA RID: 12490
		public LabelDirectorWrapMode wrapmode;
	}
}
