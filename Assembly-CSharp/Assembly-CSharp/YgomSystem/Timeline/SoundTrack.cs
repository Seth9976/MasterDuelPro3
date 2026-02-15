using System;
using UnityEngine;
using UnityEngine.Timeline;

namespace YgomSystem.Timeline
{
	// Token: 0x020006C4 RID: 1732
	[ExcludeFromPreset]
	[TrackColor(0f, 1f, 1f)]
	[TrackClipType(typeof(SoundPlayableAsset))]
	[Serializable]
	public class SoundTrack : TrackAsset
	{
		// Token: 0x060035E7 RID: 13799 RVA: 0x0000216D File Offset: 0x0000036D
		public void StopAllSound()
		{
		}

		// Token: 0x060035E8 RID: 13800 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetMute(bool mute)
		{
		}
	}
}
