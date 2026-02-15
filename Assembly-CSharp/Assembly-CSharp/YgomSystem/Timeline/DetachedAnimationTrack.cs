using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace YgomSystem.Timeline
{
	// Token: 0x020006A0 RID: 1696
	[ExcludeFromPreset]
	[Serializable]
	public class DetachedAnimationTrack : TrackAsset
	{
		// Token: 0x06003555 RID: 13653 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreateClip(TimelineClip clip)
		{
		}

		// Token: 0x06003556 RID: 13654 RVA: 0x000F2CB0 File Offset: 0x000F0EB0
		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			return default(Playable);
		}
	}
}
