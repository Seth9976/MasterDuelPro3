using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000339 RID: 825
	public enum BuiltinRenderTextureType
	{
		// Token: 0x04000923 RID: 2339
		PropertyName = -4,
		// Token: 0x04000924 RID: 2340
		BufferPtr,
		// Token: 0x04000925 RID: 2341
		RenderTexture,
		// Token: 0x04000926 RID: 2342
		BindableTexture,
		// Token: 0x04000927 RID: 2343
		None,
		// Token: 0x04000928 RID: 2344
		CurrentActive,
		// Token: 0x04000929 RID: 2345
		CameraTarget,
		// Token: 0x0400092A RID: 2346
		Depth,
		// Token: 0x0400092B RID: 2347
		DepthNormals,
		// Token: 0x0400092C RID: 2348
		ResolvedDepth,
		// Token: 0x0400092D RID: 2349
		[Obsolete("Deferred Lighting has been removed, so PrepassNormalsSpec built-in render texture type is never used now.", false)]
		PrepassNormalsSpec = 7,
		// Token: 0x0400092E RID: 2350
		[Obsolete("Deferred Lighting has been removed, so PrepassLight built-in render texture type is never used now.", false)]
		PrepassLight,
		// Token: 0x0400092F RID: 2351
		[Obsolete("Deferred Lighting has been removed, so PrepassLightSpec built-in render texture type is never used now.", false)]
		PrepassLightSpec,
		// Token: 0x04000930 RID: 2352
		GBuffer0,
		// Token: 0x04000931 RID: 2353
		GBuffer1,
		// Token: 0x04000932 RID: 2354
		GBuffer2,
		// Token: 0x04000933 RID: 2355
		GBuffer3,
		// Token: 0x04000934 RID: 2356
		Reflections,
		// Token: 0x04000935 RID: 2357
		MotionVectors,
		// Token: 0x04000936 RID: 2358
		GBuffer4,
		// Token: 0x04000937 RID: 2359
		GBuffer5,
		// Token: 0x04000938 RID: 2360
		GBuffer6,
		// Token: 0x04000939 RID: 2361
		GBuffer7
	}
}
