using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200000F RID: 15
	public static class ComponentUtility
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002759 File Offset: 0x00000959
		public static bool IsUniversalCamera(Camera camera)
		{
			return camera.GetComponent<UniversalAdditionalCameraData>() != null;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002767 File Offset: 0x00000967
		public static bool IsUniversalLight(Light light)
		{
			return light.GetComponent<UniversalAdditionalLightData>() != null;
		}
	}
}
