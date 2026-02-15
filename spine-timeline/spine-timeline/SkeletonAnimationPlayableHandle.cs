using System;
using UnityEngine;

namespace Spine.Unity.Playables
{
	// Token: 0x02000006 RID: 6
	[AddComponentMenu("Spine/Playables/SkeletonAnimation Playable Handle (Playables)")]
	public class SkeletonAnimationPlayableHandle : SpinePlayableHandleBase
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020F4 File Offset: 0x000002F4
		public override Skeleton Skeleton
		{
			get
			{
				return this.skeletonAnimation.Skeleton;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002101 File Offset: 0x00000301
		public override SkeletonData SkeletonData
		{
			get
			{
				return this.skeletonAnimation.Skeleton.Data;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002113 File Offset: 0x00000313
		private void Awake()
		{
			this.InitializeReference();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000211B File Offset: 0x0000031B
		private void InitializeReference()
		{
			if (this.skeletonAnimation == null)
			{
				this.skeletonAnimation = base.GetComponent<SkeletonAnimation>();
			}
		}

		// Token: 0x04000009 RID: 9
		public SkeletonAnimation skeletonAnimation;
	}
}
