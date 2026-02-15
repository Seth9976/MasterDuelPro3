using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000167 RID: 359
	[Serializable]
	internal class DecalSettings
	{
		// Token: 0x0400082E RID: 2094
		public DecalTechniqueOption technique;

		// Token: 0x0400082F RID: 2095
		public float maxDrawDistance = 1000f;

		// Token: 0x04000830 RID: 2096
		public bool decalLayers;

		// Token: 0x04000831 RID: 2097
		public DBufferSettings dBufferSettings;

		// Token: 0x04000832 RID: 2098
		public DecalScreenSpaceSettings screenSpaceSettings;
	}
}
