using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000053 RID: 83
	internal enum RenderPassEvent2D
	{
		// Token: 0x040001FC RID: 508
		None = -1,
		// Token: 0x040001FD RID: 509
		BeforeRendering,
		// Token: 0x040001FE RID: 510
		BeforeRenderingLayer = 100,
		// Token: 0x040001FF RID: 511
		BeforeRenderingShadows = 200,
		// Token: 0x04000200 RID: 512
		BeforeRenderingNormals = 300,
		// Token: 0x04000201 RID: 513
		BeforeRenderingLights = 400,
		// Token: 0x04000202 RID: 514
		BeforeRenderingSprites = 500,
		// Token: 0x04000203 RID: 515
		AfterRenderingLayer = 600,
		// Token: 0x04000204 RID: 516
		BeforeRenderingPostProcessing = 700,
		// Token: 0x04000205 RID: 517
		AfterRenderingPostProcessing = 800,
		// Token: 0x04000206 RID: 518
		AfterRendering = 900
	}
}
