using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200017A RID: 378
	public enum ShaderVariantLogLevel
	{
		// Token: 0x0400075C RID: 1884
		[Tooltip("No shader variants are logged")]
		Disabled,
		// Token: 0x0400075D RID: 1885
		[Tooltip("Only shaders that are compatible with SRPs (e.g., URP, HDRP) are logged")]
		OnlySRPShaders,
		// Token: 0x0400075E RID: 1886
		[Tooltip("All shader variants are logged")]
		AllShaders
	}
}
