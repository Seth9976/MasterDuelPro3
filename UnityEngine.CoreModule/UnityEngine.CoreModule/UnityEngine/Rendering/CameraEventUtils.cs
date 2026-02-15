using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000336 RID: 822
	internal static class CameraEventUtils
	{
		// Token: 0x06001638 RID: 5688 RVA: 0x0002EA3C File Offset: 0x0002CC3C
		public static bool IsValid(CameraEvent value)
		{
			return value >= CameraEvent.BeforeDepthTexture && value <= CameraEvent.AfterHaloAndLensFlares;
		}
	}
}
