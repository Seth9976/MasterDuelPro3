using System;

namespace UnityEngine.ResourceManagement.Util
{
	// Token: 0x02000032 RID: 50
	internal class PlatformUtilities
	{
		// Token: 0x06000131 RID: 305 RVA: 0x00006432 File Offset: 0x00004632
		internal static bool PlatformUsesMultiThreading(RuntimePlatform platform)
		{
			return platform != RuntimePlatform.WebGLPlayer;
		}
	}
}
