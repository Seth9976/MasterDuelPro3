using System;

namespace UnityEngine.Timeline
{
	// Token: 0x0200005D RID: 93
	[Serializable]
	public class PlayableTrack : TrackAsset
	{
		// Token: 0x06000309 RID: 777 RVA: 0x0000A42D File Offset: 0x0000862D
		protected override void OnCreateClip(TimelineClip clip)
		{
			if (clip.asset != null)
			{
				clip.displayName = clip.asset.GetType().Name;
			}
		}
	}
}
