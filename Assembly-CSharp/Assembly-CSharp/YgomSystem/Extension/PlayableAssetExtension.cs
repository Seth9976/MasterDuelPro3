using System;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace YgomSystem.Extension
{
	// Token: 0x02000774 RID: 1908
	public static class PlayableAssetExtension
	{
		// Token: 0x06003B5B RID: 15195 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, TimelineClip> CollectTrackClipsMap(this PlayableAsset playableAsset, string trackName)
		{
			return null;
		}

		// Token: 0x06003B5C RID: 15196 RVA: 0x0000216A File Offset: 0x0000036A
		public static TrackAsset FindTrack(this PlayableAsset playableAsset, string trackName)
		{
			return null;
		}

		// Token: 0x06003B5D RID: 15197 RVA: 0x000F3954 File Offset: 0x000F1B54
		public static TimelineClip FindClicp(this TrackAsset trackAsset, string clipName)
		{
			foreach (TimelineClip clip in trackAsset.GetClips())
			{
				if (clip.displayName == clipName)
				{
					return clip;
				}
			}
			return null;
		}

		// Token: 0x06003B5E RID: 15198 RVA: 0x0000216A File Offset: 0x0000036A
		public static PlayableDirector GetSourcePlayableDirector(this ControlPlayableAsset controlClip, PlayableDirector director)
		{
			return null;
		}
	}
}
