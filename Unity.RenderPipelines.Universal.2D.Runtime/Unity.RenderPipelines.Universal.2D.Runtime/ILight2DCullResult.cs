using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000033 RID: 51
	internal interface ILight2DCullResult
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600014C RID: 332
		List<Light2D> visibleLights { get; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600014D RID: 333
		HashSet<ShadowCasterGroup2D> visibleShadows { get; }

		// Token: 0x0600014E RID: 334
		LightStats GetLightStatsByLayer(int layerID, ref LayerBatch layer);

		// Token: 0x0600014F RID: 335
		bool IsSceneLit();
	}
}
