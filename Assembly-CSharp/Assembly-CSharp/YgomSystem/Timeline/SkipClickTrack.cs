using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace YgomSystem.Timeline
{
	// Token: 0x020006BD RID: 1725
	[ExcludeFromPreset]
	[Serializable]
	public class SkipClickTrack : TrackAsset
	{
		// Token: 0x060035D3 RID: 13779 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreateClip(TimelineClip clip)
		{
		}

		// Token: 0x060035D4 RID: 13780 RVA: 0x000F3428 File Offset: 0x000F1628
		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			return default(Playable);
		}
	}
}
