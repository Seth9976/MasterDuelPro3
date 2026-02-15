using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace YgomSystem.Timeline
{
	// Token: 0x020006BF RID: 1727
	[ExcludeFromPreset]
	[Serializable]
	public class SkipTrack : TrackAsset
	{
		// Token: 0x060035D8 RID: 13784 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreateClip(TimelineClip clip)
		{
		}

		// Token: 0x060035D9 RID: 13785 RVA: 0x000F3458 File Offset: 0x000F1658
		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			return default(Playable);
		}
	}
}
