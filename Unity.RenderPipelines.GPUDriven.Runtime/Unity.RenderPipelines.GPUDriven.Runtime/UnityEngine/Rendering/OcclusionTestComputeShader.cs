using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000B5 RID: 181
	internal struct OcclusionTestComputeShader
	{
		// Token: 0x060002BC RID: 700 RVA: 0x00011E74 File Offset: 0x00010074
		public void Init(ComputeShader cs)
		{
			this.cs = cs;
			this.occlusionDebugKeyword = new LocalKeyword(cs, "OCCLUSION_DEBUG");
		}

		// Token: 0x040003A0 RID: 928
		public ComputeShader cs;

		// Token: 0x040003A1 RID: 929
		public LocalKeyword occlusionDebugKeyword;
	}
}
