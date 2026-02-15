using System;

namespace Unity.Jobs.LowLevel.Unsafe
{
	// Token: 0x02000018 RID: 24
	public enum ScheduleMode
	{
		// Token: 0x04000015 RID: 21
		Run,
		// Token: 0x04000016 RID: 22
		[Obsolete("Batched is obsolete, use Parallel or Single depending on job type. (UnityUpgradable) -> Parallel", false)]
		Batched,
		// Token: 0x04000017 RID: 23
		Parallel = 1,
		// Token: 0x04000018 RID: 24
		Single
	}
}
