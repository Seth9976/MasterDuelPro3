using System;
using System.Collections.Generic;

namespace UnityEngine.Timeline
{
	// Token: 0x02000078 RID: 120
	internal static class TimelineCreateUtilities
	{
		// Token: 0x06000367 RID: 871 RVA: 0x0000B8E4 File Offset: 0x00009AE4
		public static string GenerateUniqueActorName(List<ScriptableObject> tracks, string name)
		{
			if (!tracks.Exists((ScriptableObject x) => x != null && x.name == name))
			{
				return name;
			}
			int numberInParentheses = 0;
			string baseName = name;
			if (!string.IsNullOrEmpty(name) && name[name.Length - 1] == ')')
			{
				int index = name.LastIndexOf('(');
				if (index > 0 && int.TryParse(name.Substring(index + 1, name.Length - index - 2), out numberInParentheses))
				{
					numberInParentheses++;
					baseName = name.Substring(0, index);
				}
			}
			baseName = baseName.TrimEnd();
			for (int i = numberInParentheses; i < numberInParentheses + 5000; i++)
			{
				if (i > 0)
				{
					string result = string.Format("{0} ({1})", baseName, i);
					if (!tracks.Exists((ScriptableObject x) => x != null && x.name == result))
					{
						return result;
					}
				}
			}
			return name;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000B9FA File Offset: 0x00009BFA
		public static void SaveAssetIntoObject(Object childAsset, Object masterAsset)
		{
			if (childAsset == null || masterAsset == null)
			{
				return;
			}
			if ((masterAsset.hideFlags & HideFlags.DontSave) != HideFlags.None)
			{
				childAsset.hideFlags |= HideFlags.DontSave;
				return;
			}
			childAsset.hideFlags |= HideFlags.HideInHierarchy;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000BA38 File Offset: 0x00009C38
		public static void RemoveAssetFromObject(Object childAsset, Object masterAsset)
		{
			if (!(childAsset == null))
			{
				masterAsset == null;
			}
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000BA4C File Offset: 0x00009C4C
		public static AnimationClip CreateAnimationClipForTrack(string name, TrackAsset track, bool isLegacy)
		{
			TimelineAsset timelineAsset = ((track != null) ? track.timelineAsset : null);
			HideFlags trackFlags = ((track != null) ? track.hideFlags : HideFlags.None);
			AnimationClip animationClip = new AnimationClip();
			animationClip.legacy = isLegacy;
			animationClip.name = name;
			animationClip.frameRate = ((timelineAsset == null) ? ((float)TimelineAsset.EditorSettings.kDefaultFrameRate) : ((float)timelineAsset.editorSettings.frameRate));
			TimelineCreateUtilities.SaveAssetIntoObject(animationClip, timelineAsset);
			animationClip.hideFlags = trackFlags & ~HideFlags.HideInHierarchy;
			return animationClip;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000BAC8 File Offset: 0x00009CC8
		public static bool ValidateParentTrack(TrackAsset parent, Type childType)
		{
			if (childType == null || !typeof(TrackAsset).IsAssignableFrom(childType))
			{
				return false;
			}
			if (parent == null)
			{
				return true;
			}
			if (parent is ILayerable && !parent.isSubTrack && parent.GetType() == childType)
			{
				return true;
			}
			SupportsChildTracksAttribute attr = Attribute.GetCustomAttribute(parent.GetType(), typeof(SupportsChildTracksAttribute)) as SupportsChildTracksAttribute;
			if (attr == null)
			{
				return false;
			}
			if (attr.childType == null)
			{
				return true;
			}
			if (childType == attr.childType)
			{
				int nestCount = 0;
				TrackAsset p = parent;
				while (p != null && p.isSubTrack)
				{
					nestCount++;
					p = p.parent as TrackAsset;
				}
				return nestCount < attr.levels;
			}
			return false;
		}
	}
}
