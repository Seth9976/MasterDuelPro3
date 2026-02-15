using System;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x02000006 RID: 6
	[CreateAssetMenu(menuName = "Spine/Animation Reference Asset", order = 100)]
	public class AnimationReferenceAsset : ScriptableObject, IHasSkeletonDataAsset, ISpineComponent
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002798 File Offset: 0x00000998
		public SkeletonDataAsset SkeletonDataAsset
		{
			get
			{
				return this.skeletonDataAsset;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000027A0 File Offset: 0x000009A0
		public Animation Animation
		{
			get
			{
				if (this.animation == null)
				{
					this.Initialize();
				}
				return this.animation;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000027B6 File Offset: 0x000009B6
		public void Clear()
		{
			this.animation = null;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000027C0 File Offset: 0x000009C0
		public void Initialize()
		{
			if (this.skeletonDataAsset == null)
			{
				return;
			}
			SkeletonData skeletonData = this.skeletonDataAsset.GetSkeletonData(true);
			this.animation = ((skeletonData != null) ? skeletonData.FindAnimation(this.animationName) : null);
			if (this.animation == null)
			{
				Debug.LogWarningFormat("Animation '{0}' not found in SkeletonData : {1}.", new object[]
				{
					this.animationName,
					this.skeletonDataAsset.name
				});
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002830 File Offset: 0x00000A30
		public static implicit operator Animation(AnimationReferenceAsset asset)
		{
			return asset.Animation;
		}

		// Token: 0x0400000C RID: 12
		private const bool QuietSkeletonData = true;

		// Token: 0x0400000D RID: 13
		[SerializeField]
		protected SkeletonDataAsset skeletonDataAsset;

		// Token: 0x0400000E RID: 14
		[SerializeField]
		[SpineAnimation("", "", true, false, false)]
		protected string animationName;

		// Token: 0x0400000F RID: 15
		private Animation animation;
	}
}
