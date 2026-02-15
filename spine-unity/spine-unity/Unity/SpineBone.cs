using System;

namespace Spine.Unity
{
	// Token: 0x0200006A RID: 106
	public class SpineBone : SpineAttributeBase
	{
		// Token: 0x06000334 RID: 820 RVA: 0x00012D82 File Offset: 0x00010F82
		public SpineBone(string startsWith = "", string dataField = "", bool includeNone = true, bool fallbackToTextField = false)
		{
			this.startsWith = startsWith;
			this.dataField = dataField;
			this.includeNone = includeNone;
			this.fallbackToTextField = fallbackToTextField;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00012DA7 File Offset: 0x00010FA7
		public static Bone GetBone(string boneName, SkeletonRenderer renderer)
		{
			if (renderer.skeleton != null)
			{
				return renderer.skeleton.FindBone(boneName);
			}
			return null;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00012DBF File Offset: 0x00010FBF
		public static BoneData GetBoneData(string boneName, SkeletonDataAsset skeletonDataAsset)
		{
			return skeletonDataAsset.GetSkeletonData(true).FindBone(boneName);
		}
	}
}
