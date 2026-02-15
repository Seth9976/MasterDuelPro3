using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000144 RID: 324
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, false, -1, "./Library/PackageCache/com.unity.render-pipelines.core/Runtime/Lighting/ProbeVolume/ShaderVariablesProbeVolumes.cs")]
	public enum APVLeakReductionMode
	{
		// Token: 0x04000605 RID: 1541
		None,
		// Token: 0x04000606 RID: 1542
		Performance,
		// Token: 0x04000607 RID: 1543
		Quality,
		// Token: 0x04000608 RID: 1544
		[Obsolete("Performance")]
		ValidityBased = 1,
		// Token: 0x04000609 RID: 1545
		[Obsolete("Quality")]
		ValidityAndNormalBased
	}
}
