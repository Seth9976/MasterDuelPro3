using System;
using UnityEngine;
using UnityEngine.Playables;

namespace YgomSystem.Timeline
{
	// Token: 0x020006B4 RID: 1716
	[Serializable]
	public class PauseClip : PlayableAsset
	{
		// Token: 0x060035B2 RID: 13746 RVA: 0x000F33E0 File Offset: 0x000F15E0
		public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
		{
			return default(Playable);
		}

		// Token: 0x040030EE RID: 12526
		public PauseBehaviour template;

		// Token: 0x040030EF RID: 12527
		[NonSerialized]
		public double startTime;

		// Token: 0x040030F0 RID: 12528
		[NonSerialized]
		public double endTime;
	}
}
