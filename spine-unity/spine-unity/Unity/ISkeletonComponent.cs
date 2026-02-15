using System;

namespace Spine.Unity
{
	// Token: 0x02000059 RID: 89
	public interface ISkeletonComponent : ISpineComponent
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060002E7 RID: 743
		SkeletonDataAsset SkeletonDataAsset { get; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060002E8 RID: 744
		Skeleton Skeleton { get; }
	}
}
