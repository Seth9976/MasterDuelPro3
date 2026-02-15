using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace YgomSystem.Timeline
{
	// Token: 0x020006CD RID: 1741
	[ExcludeFromPreset]
	[Serializable]
	public class WaitClickTrack : TrackAsset
	{
		// Token: 0x06003641 RID: 13889 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreateClip(TimelineClip clip)
		{
		}

		// Token: 0x06003642 RID: 13890 RVA: 0x000F359C File Offset: 0x000F179C
		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			return default(Playable);
		}
	}
}
