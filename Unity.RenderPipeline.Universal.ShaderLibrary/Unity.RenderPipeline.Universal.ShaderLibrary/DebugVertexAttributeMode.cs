using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000005 RID: 5
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, false, -1, "./Library/PackageCache/com.unity.render-pipelines.universal/ShaderLibrary/Debug/DebugViewEnums.cs")]
	public enum DebugVertexAttributeMode
	{
		// Token: 0x04000015 RID: 21
		None,
		// Token: 0x04000016 RID: 22
		Texcoord0,
		// Token: 0x04000017 RID: 23
		Texcoord1,
		// Token: 0x04000018 RID: 24
		Texcoord2,
		// Token: 0x04000019 RID: 25
		Texcoord3,
		// Token: 0x0400001A RID: 26
		Color,
		// Token: 0x0400001B RID: 27
		Tangent,
		// Token: 0x0400001C RID: 28
		Normal
	}
}
