using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000335 RID: 821
	public enum CameraEvent
	{
		// Token: 0x040008F2 RID: 2290
		BeforeDepthTexture,
		// Token: 0x040008F3 RID: 2291
		AfterDepthTexture,
		// Token: 0x040008F4 RID: 2292
		BeforeDepthNormalsTexture,
		// Token: 0x040008F5 RID: 2293
		AfterDepthNormalsTexture,
		// Token: 0x040008F6 RID: 2294
		BeforeGBuffer,
		// Token: 0x040008F7 RID: 2295
		AfterGBuffer,
		// Token: 0x040008F8 RID: 2296
		BeforeLighting,
		// Token: 0x040008F9 RID: 2297
		AfterLighting,
		// Token: 0x040008FA RID: 2298
		BeforeFinalPass,
		// Token: 0x040008FB RID: 2299
		AfterFinalPass,
		// Token: 0x040008FC RID: 2300
		BeforeForwardOpaque,
		// Token: 0x040008FD RID: 2301
		AfterForwardOpaque,
		// Token: 0x040008FE RID: 2302
		BeforeImageEffectsOpaque,
		// Token: 0x040008FF RID: 2303
		AfterImageEffectsOpaque,
		// Token: 0x04000900 RID: 2304
		BeforeSkybox,
		// Token: 0x04000901 RID: 2305
		AfterSkybox,
		// Token: 0x04000902 RID: 2306
		BeforeForwardAlpha,
		// Token: 0x04000903 RID: 2307
		AfterForwardAlpha,
		// Token: 0x04000904 RID: 2308
		BeforeImageEffects,
		// Token: 0x04000905 RID: 2309
		AfterImageEffects,
		// Token: 0x04000906 RID: 2310
		AfterEverything,
		// Token: 0x04000907 RID: 2311
		BeforeReflections,
		// Token: 0x04000908 RID: 2312
		AfterReflections,
		// Token: 0x04000909 RID: 2313
		BeforeHaloAndLensFlares,
		// Token: 0x0400090A RID: 2314
		AfterHaloAndLensFlares
	}
}
