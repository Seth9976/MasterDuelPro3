using System;
using UnityEngine;
using UnityEngine.Playables;

namespace YgomSystem.Timeline
{
	// Token: 0x020006C2 RID: 1730
	public class SoundStopPlayableAsset : PlayableAsset
	{
		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x060035E1 RID: 13793 RVA: 0x000F165E File Offset: 0x000EF85E
		public override double duration
		{
			get
			{
				return 0.0;
			}
		}

		// Token: 0x060035E2 RID: 13794 RVA: 0x000F34C4 File Offset: 0x000F16C4
		public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
		{
			return default(Playable);
		}

		// Token: 0x04003104 RID: 12548
		public string stopLabel;

		// Token: 0x04003105 RID: 12549
		public float fade;

		// Token: 0x04003106 RID: 12550
		public bool mute;
	}
}
