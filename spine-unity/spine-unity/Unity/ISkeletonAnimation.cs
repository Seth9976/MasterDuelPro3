using System;

namespace Spine.Unity
{
	// Token: 0x02000057 RID: 87
	public interface ISkeletonAnimation : ISpineComponent
	{
		// Token: 0x14000036 RID: 54
		// (add) Token: 0x060002DB RID: 731
		// (remove) Token: 0x060002DC RID: 732
		event ISkeletonAnimationDelegate OnAnimationRebuild;

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x060002DD RID: 733
		// (remove) Token: 0x060002DE RID: 734
		event UpdateBonesDelegate UpdateLocal;

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x060002DF RID: 735
		// (remove) Token: 0x060002E0 RID: 736
		event UpdateBonesDelegate UpdateWorld;

		// Token: 0x14000039 RID: 57
		// (add) Token: 0x060002E1 RID: 737
		// (remove) Token: 0x060002E2 RID: 738
		event UpdateBonesDelegate UpdateComplete;

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060002E3 RID: 739
		Skeleton Skeleton { get; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060002E4 RID: 740
		// (set) Token: 0x060002E5 RID: 741
		UpdateTiming UpdateTiming { get; set; }
	}
}
