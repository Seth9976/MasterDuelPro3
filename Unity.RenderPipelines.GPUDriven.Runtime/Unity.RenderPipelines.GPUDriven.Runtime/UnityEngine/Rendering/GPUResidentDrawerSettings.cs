using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000031 RID: 49
	public struct GPUResidentDrawerSettings
	{
		// Token: 0x040000A2 RID: 162
		public GPUResidentDrawerMode mode;

		// Token: 0x040000A3 RID: 163
		public bool supportDitheringCrossFade;

		// Token: 0x040000A4 RID: 164
		public bool enableOcclusionCulling;

		// Token: 0x040000A5 RID: 165
		public bool allowInEditMode;

		// Token: 0x040000A6 RID: 166
		public float smallMeshScreenPercentage;

		// Token: 0x040000A7 RID: 167
		public Shader errorShader;

		// Token: 0x040000A8 RID: 168
		public Shader loadingShader;
	}
}
