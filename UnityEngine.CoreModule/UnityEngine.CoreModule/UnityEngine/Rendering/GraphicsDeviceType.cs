using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200033D RID: 829
	[UsedByNativeCode]
	public enum GraphicsDeviceType
	{
		// Token: 0x0400094C RID: 2380
		[Obsolete("OpenGL2 is no longer supported in Unity 5.5+")]
		OpenGL2,
		// Token: 0x0400094D RID: 2381
		[Obsolete("Direct3D 9 is no longer supported in Unity 2017.2+")]
		Direct3D9,
		// Token: 0x0400094E RID: 2382
		Direct3D11,
		// Token: 0x0400094F RID: 2383
		[Obsolete("PS3 is no longer supported in Unity 5.5+")]
		PlayStation3,
		// Token: 0x04000950 RID: 2384
		Null,
		// Token: 0x04000951 RID: 2385
		[Obsolete("Xbox360 is no longer supported in Unity 5.5+")]
		Xbox360 = 6,
		// Token: 0x04000952 RID: 2386
		[Obsolete("OpenGL ES 2.0 is no longer supported in Unity 2023.1")]
		OpenGLES2 = 8,
		// Token: 0x04000953 RID: 2387
		OpenGLES3 = 11,
		// Token: 0x04000954 RID: 2388
		[Obsolete("PVita is no longer supported as of Unity 2018")]
		PlayStationVita,
		// Token: 0x04000955 RID: 2389
		PlayStation4,
		// Token: 0x04000956 RID: 2390
		XboxOne,
		// Token: 0x04000957 RID: 2391
		[Obsolete("PlayStationMobile is no longer supported in Unity 5.3+")]
		PlayStationMobile,
		// Token: 0x04000958 RID: 2392
		Metal,
		// Token: 0x04000959 RID: 2393
		OpenGLCore,
		// Token: 0x0400095A RID: 2394
		Direct3D12,
		// Token: 0x0400095B RID: 2395
		[Obsolete("Nintendo 3DS support is unavailable since 2018.1")]
		N3DS,
		// Token: 0x0400095C RID: 2396
		Vulkan = 21,
		// Token: 0x0400095D RID: 2397
		Switch,
		// Token: 0x0400095E RID: 2398
		XboxOneD3D12,
		// Token: 0x0400095F RID: 2399
		GameCoreXboxOne,
		// Token: 0x04000960 RID: 2400
		[Obsolete("GameCoreScarlett is deprecated, please use GameCoreXboxSeries (UnityUpgradable) -> GameCoreXboxSeries", false)]
		GameCoreScarlett = -1,
		// Token: 0x04000961 RID: 2401
		GameCoreXboxSeries = 25,
		// Token: 0x04000962 RID: 2402
		PlayStation5,
		// Token: 0x04000963 RID: 2403
		PlayStation5NGGC,
		// Token: 0x04000964 RID: 2404
		WebGPU,
		// Token: 0x04000965 RID: 2405
		ReservedCFE
	}
}
