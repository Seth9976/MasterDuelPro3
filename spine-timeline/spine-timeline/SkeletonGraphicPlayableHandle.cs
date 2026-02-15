using System;
using UnityEngine;

namespace Spine.Unity.Playables
{
	// Token: 0x02000007 RID: 7
	[AddComponentMenu("Spine/Playables/SkeletonGraphic Playable Handle (Playables)")]
	public class SkeletonGraphicPlayableHandle : SpinePlayableHandleBase
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000213F File Offset: 0x0000033F
		public override Skeleton Skeleton
		{
			get
			{
				return this.skeletonGraphic.Skeleton;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000214C File Offset: 0x0000034C
		public override SkeletonData SkeletonData
		{
			get
			{
				return this.skeletonGraphic.Skeleton.Data;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000215E File Offset: 0x0000035E
		private void Awake()
		{
			this.InitializeReference();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002166 File Offset: 0x00000366
		private void InitializeReference()
		{
			if (this.skeletonGraphic == null)
			{
				this.skeletonGraphic = base.GetComponent<SkeletonGraphic>();
			}
		}

		// Token: 0x0400000A RID: 10
		public SkeletonGraphic skeletonGraphic;
	}
}
