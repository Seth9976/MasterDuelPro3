using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020001B4 RID: 436
	public static class LightExtensions
	{
		// Token: 0x0600094A RID: 2378 RVA: 0x0002E4D0 File Offset: 0x0002C6D0
		public static UniversalAdditionalLightData GetUniversalAdditionalLightData(this Light light)
		{
			GameObject gameObject = light.gameObject;
			UniversalAdditionalLightData lightData;
			if (!gameObject.TryGetComponent<UniversalAdditionalLightData>(out lightData))
			{
				lightData = gameObject.AddComponent<UniversalAdditionalLightData>();
			}
			return lightData;
		}
	}
}
