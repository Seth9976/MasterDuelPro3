using System;

namespace UnityEngine.Timeline
{
	// Token: 0x0200006E RID: 110
	internal static class Extrapolation
	{
		// Token: 0x06000328 RID: 808 RVA: 0x0000AA94 File Offset: 0x00008C94
		internal static void CalculateExtrapolationTimes(this TrackAsset asset)
		{
			TimelineClip[] clips = asset.clips;
			if (clips == null || clips.Length == 0)
			{
				return;
			}
			if (!clips[0].SupportsExtrapolation())
			{
				return;
			}
			TimelineClip[] orderedClips = Extrapolation.SortClipsByStartTime(clips);
			if (orderedClips.Length != 0)
			{
				for (int i = 0; i < orderedClips.Length; i++)
				{
					double minTime = double.PositiveInfinity;
					for (int j = 0; j < orderedClips.Length; j++)
					{
						if (i != j)
						{
							double deltaTime = orderedClips[j].start - orderedClips[i].end;
							if (deltaTime >= -TimeUtility.kTimeEpsilon && deltaTime < minTime)
							{
								minTime = Math.Min(minTime, deltaTime);
							}
							if (orderedClips[j].start <= orderedClips[i].end && orderedClips[j].end > orderedClips[i].end)
							{
								minTime = 0.0;
							}
						}
					}
					minTime = ((minTime <= Extrapolation.kMinExtrapolationTime) ? 0.0 : minTime);
					orderedClips[i].SetPostExtrapolationTime(minTime);
				}
				orderedClips[0].SetPreExtrapolationTime(Math.Max(0.0, orderedClips[0].start));
				for (int k = 1; k < orderedClips.Length; k++)
				{
					double preTime = 0.0;
					int prevClip = -1;
					for (int l = 0; l < k; l++)
					{
						if (orderedClips[l].end > orderedClips[k].start)
						{
							prevClip = -1;
							preTime = 0.0;
							break;
						}
						double gap = orderedClips[k].start - orderedClips[l].end;
						if (prevClip == -1 || gap < preTime)
						{
							preTime = gap;
							prevClip = l;
						}
					}
					if (prevClip >= 0 && orderedClips[prevClip].postExtrapolationMode != TimelineClip.ClipExtrapolation.None)
					{
						preTime = 0.0;
					}
					preTime = ((preTime <= Extrapolation.kMinExtrapolationTime) ? 0.0 : preTime);
					orderedClips[k].SetPreExtrapolationTime(preTime);
				}
			}
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000AC58 File Offset: 0x00008E58
		private static TimelineClip[] SortClipsByStartTime(TimelineClip[] clips)
		{
			TimelineClip[] orderedClips = new TimelineClip[clips.Length];
			Array.Copy(clips, orderedClips, clips.Length);
			Array.Sort<TimelineClip>(orderedClips, (TimelineClip clip1, TimelineClip clip2) => clip1.start.CompareTo(clip2.start));
			return orderedClips;
		}

		// Token: 0x04000174 RID: 372
		internal static readonly double kMinExtrapolationTime = TimeUtility.kTimeEpsilon * 1000.0;
	}
}
