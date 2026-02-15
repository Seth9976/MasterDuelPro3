using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000146 RID: 326
	public enum RenderPassEvent
	{
		// Token: 0x04000796 RID: 1942
		BeforeRendering,
		// Token: 0x04000797 RID: 1943
		BeforeRenderingShadows = 50,
		// Token: 0x04000798 RID: 1944
		AfterRenderingShadows = 100,
		// Token: 0x04000799 RID: 1945
		BeforeRenderingPrePasses = 150,
		// Token: 0x0400079A RID: 1946
		AfterRenderingPrePasses = 200,
		// Token: 0x0400079B RID: 1947
		BeforeRenderingGbuffer = 210,
		// Token: 0x0400079C RID: 1948
		AfterRenderingGbuffer = 220,
		// Token: 0x0400079D RID: 1949
		BeforeRenderingDeferredLights = 230,
		// Token: 0x0400079E RID: 1950
		AfterRenderingDeferredLights = 240,
		// Token: 0x0400079F RID: 1951
		BeforeRenderingOpaques = 250,
		// Token: 0x040007A0 RID: 1952
		AfterRenderingOpaques = 300,
		// Token: 0x040007A1 RID: 1953
		BeforeRenderingSkybox = 350,
		// Token: 0x040007A2 RID: 1954
		AfterRenderingSkybox = 400,
		// Token: 0x040007A3 RID: 1955
		BeforeRenderingTransparents = 450,
		// Token: 0x040007A4 RID: 1956
		AfterRenderingTransparents = 500,
		// Token: 0x040007A5 RID: 1957
		BeforeRenderingPostProcessing = 550,
		// Token: 0x040007A6 RID: 1958
		AfterRenderingPostProcessing = 600,
		// Token: 0x040007A7 RID: 1959
		AfterRendering = 1000
	}
}
