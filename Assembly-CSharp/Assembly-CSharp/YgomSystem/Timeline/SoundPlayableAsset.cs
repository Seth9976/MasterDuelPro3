using System;
using UnityEngine;
using UnityEngine.Playables;

namespace YgomSystem.Timeline
{
	// Token: 0x020006C0 RID: 1728
	public class SoundPlayableAsset : PlayableAsset
	{
		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x060035DB RID: 13787 RVA: 0x000F165E File Offset: 0x000EF85E
		public override double duration
		{
			get
			{
				return 0.0;
			}
		}

		// Token: 0x060035DC RID: 13788 RVA: 0x000F3470 File Offset: 0x000F1670
		public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
		{
			ScriptPlayable<SoundPlayableBehaviour> playable = ScriptPlayable<SoundPlayableBehaviour>.Create(graph, 0);
			SoundPlayableBehaviour behaviour = playable.GetBehaviour();
			behaviour.playableAsset = this;
			behaviour.startLabel = this.startLabel;
			behaviour.skipDuplicate = this.skipDuplicate;
			return playable;
		}

		// Token: 0x040030FD RID: 12541
		public string startLabel;

		// Token: 0x040030FE RID: 12542
		public bool mute;

		// Token: 0x040030FF RID: 12543
		public bool skipDuplicate;

		// Token: 0x04003100 RID: 12544
		public LabeledPlayableController LabeledPlayableController;
	}
}
