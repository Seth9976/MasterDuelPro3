using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Willow
{
	// Token: 0x02001545 RID: 5445
	[Serializable]
	public class ActiveObjectTrack : TrackAsset
	{
		// Token: 0x06009DCE RID: 40398 RVA: 0x0019B425 File Offset: 0x00199625
		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			return Playable.Create(graph, inputCount);
		}

		// Token: 0x06009DCF RID: 40399 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreateClip(TimelineClip clip)
		{
		}

		// Token: 0x06009DD0 RID: 40400 RVA: 0x0000216D File Offset: 0x0000036D
		private void ChangeDisplayName(TimelineClip clip)
		{
		}

		// Token: 0x0400DD9E RID: 56734
		public const string kTrackName = "Active Object Track";
	}
}
