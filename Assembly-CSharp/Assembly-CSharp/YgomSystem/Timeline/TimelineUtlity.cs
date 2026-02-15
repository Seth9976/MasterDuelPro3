using System;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace YgomSystem.Timeline
{
	// Token: 0x020006C9 RID: 1737
	public static class TimelineUtlity
	{
		// Token: 0x06003629 RID: 13865 RVA: 0x000F34DC File Offset: 0x000F16DC
		public static TrackAsset GetTrackAsset(this PlayableDirector pd, string tracklabel)
		{
			foreach (PlayableBinding pb in pd.playableAsset.outputs)
			{
				TrackAsset track = pb.sourceObject as TrackAsset;
				if (!(track == null) && track.name == tracklabel)
				{
					return track;
				}
			}
			return null;
		}

		// Token: 0x0600362A RID: 13866 RVA: 0x0000216A File Offset: 0x0000036A
		public static TimelineClip GetTimelineClip(this TrackAsset ta, string cliplabel)
		{
			return null;
		}

		// Token: 0x0600362B RID: 13867 RVA: 0x0000216A File Offset: 0x0000036A
		public static TimelineClip GetTimelineClip(this PlayableDirector pd, string tracklabel, string cliplabel)
		{
			return null;
		}

		// Token: 0x0600362C RID: 13868 RVA: 0x0000216A File Offset: 0x0000036A
		public static TimelineClip GetTimelineClip(this PlayableDirector pd, string cliplabel)
		{
			return null;
		}

		// Token: 0x0600362D RID: 13869 RVA: 0x0000216A File Offset: 0x0000036A
		public static SignalEmitter GetSignalEmitter(this PlayableDirector pd, string label)
		{
			return null;
		}

		// Token: 0x0600362E RID: 13870 RVA: 0x000F3554 File Offset: 0x000F1754
		public static T GetTrack<T>(this PlayableDirector pd) where T : TrackAsset
		{
			return default(T);
		}

		// Token: 0x0600362F RID: 13871 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<T> GetTracks<T>(this PlayableDirector pd) where T : TrackAsset
		{
			return null;
		}

		// Token: 0x06003630 RID: 13872 RVA: 0x0000216A File Offset: 0x0000036A
		public static TrackAsset GetTrack(this PlayableDirector pd, string label)
		{
			return null;
		}

		// Token: 0x06003631 RID: 13873 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<TrackAsset> GetTracks(this PlayableDirector pd, string label)
		{
			return null;
		}

		// Token: 0x06003632 RID: 13874 RVA: 0x000F356C File Offset: 0x000F176C
		public static T GetTrack<T>(this PlayableDirector pd, string label) where T : TrackAsset
		{
			return default(T);
		}

		// Token: 0x06003633 RID: 13875 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<T> GetTracks<T>(this PlayableDirector pd, string label) where T : TrackAsset
		{
			return null;
		}

		// Token: 0x06003634 RID: 13876 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetSpeed(this PlayableDirector pd, double speed)
		{
		}
	}
}
