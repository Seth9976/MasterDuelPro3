using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200002D RID: 45
	public struct OcclusionCullingSettings
	{
		// Token: 0x060000EE RID: 238 RVA: 0x0000592A File Offset: 0x00003B2A
		public OcclusionCullingSettings(int viewInstanceID, OcclusionTest occlusionTest)
		{
			this.viewInstanceID = viewInstanceID;
			this.occlusionTest = occlusionTest;
			this.instanceMultiplier = 1;
		}

		// Token: 0x04000090 RID: 144
		public int viewInstanceID;

		// Token: 0x04000091 RID: 145
		public OcclusionTest occlusionTest;

		// Token: 0x04000092 RID: 146
		public int instanceMultiplier;
	}
}
