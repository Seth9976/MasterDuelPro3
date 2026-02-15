using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace YgomSystem.Timeline
{
	// Token: 0x020006B0 RID: 1712
	[Serializable]
	public class LoopClip : PlayableAsset
	{
		// Token: 0x06003597 RID: 13719 RVA: 0x000F30F8 File Offset: 0x000F12F8
		public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
		{
			ScriptPlayable<LoopBehaviour> playable = ScriptPlayable<LoopBehaviour>.Create(graph, 0);
			this.template = playable.GetBehaviour();
			return playable;
		}

		// Token: 0x06003598 RID: 13720 RVA: 0x000F3120 File Offset: 0x000F1320
		public void PassClip()
		{
			if (this.template != null)
			{
				this.template.loopClip = this.loopClip;
				return;
			}
			Debug.Log("LoopClip template is Null !");
		}

		// Token: 0x040030DE RID: 12510
		[HideInInspector]
		public LoopBehaviour template;

		// Token: 0x040030DF RID: 12511
		[HideInInspector]
		public TimelineClip loopClip;
	}
}
