using System;

namespace UnityEngine.Timeline
{
	// Token: 0x0200004C RID: 76
	public static class TrackAssetExtensions
	{
		// Token: 0x060002B5 RID: 693 RVA: 0x00009410 File Offset: 0x00007610
		public static GroupTrack GetGroup(this TrackAsset asset)
		{
			if (asset == null)
			{
				return null;
			}
			return asset.parent as GroupTrack;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00009428 File Offset: 0x00007628
		public static void SetGroup(this TrackAsset asset, GroupTrack group)
		{
			if (asset == null || asset == group || asset.parent == group)
			{
				return;
			}
			if (group != null && asset.timelineAsset != group.timelineAsset)
			{
				throw new InvalidOperationException("Cannot assign to a group in a different timeline");
			}
			TimelineAsset timeline = asset.timelineAsset;
			TrackAsset parentTrack = asset.parent as TrackAsset;
			TimelineAsset parentTimeline = asset.parent as TimelineAsset;
			if (parentTrack != null || parentTimeline != null)
			{
				if (parentTimeline != null)
				{
					parentTimeline.RemoveTrack(asset);
				}
				else
				{
					parentTrack.RemoveSubTrack(asset);
				}
			}
			if (group == null)
			{
				asset.parent = asset.timelineAsset;
				timeline.AddTrackInternal(asset);
				return;
			}
			group.AddChild(asset);
		}
	}
}
