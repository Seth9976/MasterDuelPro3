using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200000A RID: 10
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, false, -1, "./Library/PackageCache/com.unity.render-pipelines.universal/ShaderLibrary/Debug/DebugViewEnums.cs")]
	public enum DebugMipInfoMode
	{
		// Token: 0x04000036 RID: 54
		None,
		// Token: 0x04000037 RID: 55
		MipStreamingPerformance,
		// Token: 0x04000038 RID: 56
		MipStreamingStatus,
		// Token: 0x04000039 RID: 57
		MipStreamingActivity,
		// Token: 0x0400003A RID: 58
		MipStreamingPriority,
		// Token: 0x0400003B RID: 59
		MipCount,
		// Token: 0x0400003C RID: 60
		MipRatio
	}
}
