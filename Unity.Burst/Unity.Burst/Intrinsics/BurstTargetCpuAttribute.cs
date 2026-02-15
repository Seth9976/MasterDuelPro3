using System;

namespace Unity.Burst.Intrinsics
{
	// Token: 0x02000032 RID: 50
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[BurstRuntime.PreserveAttribute]
	internal sealed class BurstTargetCpuAttribute : Attribute
	{
		// Token: 0x06000A54 RID: 2644 RVA: 0x000065A5 File Offset: 0x000047A5
		public BurstTargetCpuAttribute(BurstTargetCpu TargetCpu)
		{
			this.TargetCpu = TargetCpu;
		}

		// Token: 0x04000175 RID: 373
		public readonly BurstTargetCpu TargetCpu;
	}
}
