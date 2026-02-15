using System;
using Unity.Collections;

namespace UnityEngine.Rendering
{
	// Token: 0x020003A9 RID: 937
	public struct ShadowCastersCullingInfos
	{
		// Token: 0x04000BE1 RID: 3041
		public NativeArray<ShadowSplitData> splitBuffer;

		// Token: 0x04000BE2 RID: 3042
		public NativeArray<LightShadowCasterCullingInfo> perLightInfos;
	}
}
