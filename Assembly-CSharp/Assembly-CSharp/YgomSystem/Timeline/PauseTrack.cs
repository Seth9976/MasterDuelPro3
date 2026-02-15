using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace YgomSystem.Timeline
{
	// Token: 0x020006B5 RID: 1717
	[ExcludeFromPreset]
	[Serializable]
	public class PauseTrack : TrackAsset
	{
		// Token: 0x060035B4 RID: 13748 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreateClip(TimelineClip clip)
		{
		}

		// Token: 0x060035B5 RID: 13749 RVA: 0x000F33F8 File Offset: 0x000F15F8
		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			return default(Playable);
		}
	}
}
