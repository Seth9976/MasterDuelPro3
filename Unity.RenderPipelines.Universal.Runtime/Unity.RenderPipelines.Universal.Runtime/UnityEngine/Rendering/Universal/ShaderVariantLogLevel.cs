using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000A0 RID: 160
	[Obsolete("This is obsolete, UnityEngine.Rendering.ShaderVariantLogLevel instead.", true)]
	public enum ShaderVariantLogLevel
	{
		// Token: 0x04000317 RID: 791
		Disabled,
		// Token: 0x04000318 RID: 792
		[InspectorName("Only URP Shaders")]
		OnlyUniversalRPShaders,
		// Token: 0x04000319 RID: 793
		[InspectorName("All Shaders")]
		AllShaders
	}
}
