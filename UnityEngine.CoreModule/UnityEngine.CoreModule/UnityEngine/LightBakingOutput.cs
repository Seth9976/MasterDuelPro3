using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000F8 RID: 248
	[NativeHeader("Runtime/Camera/SharedLightData.h")]
	public struct LightBakingOutput
	{
		// Token: 0x040002C9 RID: 713
		public int probeOcclusionLightIndex;

		// Token: 0x040002CA RID: 714
		public int occlusionMaskChannel;

		// Token: 0x040002CB RID: 715
		[NativeName("lightmapBakeMode.lightmapBakeType")]
		public LightmapBakeType lightmapBakeType;

		// Token: 0x040002CC RID: 716
		[NativeName("lightmapBakeMode.mixedLightingMode")]
		public MixedLightingMode mixedLightingMode;

		// Token: 0x040002CD RID: 717
		public bool isBaked;
	}
}
