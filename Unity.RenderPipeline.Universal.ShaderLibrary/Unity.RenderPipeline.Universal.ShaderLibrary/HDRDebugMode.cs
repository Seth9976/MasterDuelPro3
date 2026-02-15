using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000011 RID: 17
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, false, -1, "./Library/PackageCache/com.unity.render-pipelines.universal/ShaderLibrary/Debug/DebugViewEnums.cs")]
	public enum HDRDebugMode
	{
		// Token: 0x0400005D RID: 93
		None,
		// Token: 0x0400005E RID: 94
		GamutView,
		// Token: 0x0400005F RID: 95
		GamutClip,
		// Token: 0x04000060 RID: 96
		ValuesAbovePaperWhite
	}
}
