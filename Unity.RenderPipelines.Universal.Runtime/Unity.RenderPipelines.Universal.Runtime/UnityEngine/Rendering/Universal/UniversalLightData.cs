using System;
using Unity.Collections;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000C4 RID: 196
	public class UniversalLightData : ContextItem
	{
		// Token: 0x060004E1 RID: 1249 RVA: 0x00012E14 File Offset: 0x00011014
		public override void Reset()
		{
			this.mainLightIndex = -1;
			this.additionalLightsCount = 0;
			this.maxPerObjectAdditionalLightsCount = 0;
			this.visibleLights = default(NativeArray<VisibleLight>);
			this.shadeAdditionalLightsPerVertex = false;
			this.supportsMixedLighting = false;
			this.reflectionProbeBoxProjection = false;
			this.reflectionProbeBlending = false;
			this.supportsLightLayers = false;
			this.supportsAdditionalLights = false;
		}

		// Token: 0x04000442 RID: 1090
		public int mainLightIndex;

		// Token: 0x04000443 RID: 1091
		public int additionalLightsCount;

		// Token: 0x04000444 RID: 1092
		public int maxPerObjectAdditionalLightsCount;

		// Token: 0x04000445 RID: 1093
		public NativeArray<VisibleLight> visibleLights;

		// Token: 0x04000446 RID: 1094
		public bool shadeAdditionalLightsPerVertex;

		// Token: 0x04000447 RID: 1095
		public bool supportsMixedLighting;

		// Token: 0x04000448 RID: 1096
		public bool reflectionProbeBoxProjection;

		// Token: 0x04000449 RID: 1097
		public bool reflectionProbeBlending;

		// Token: 0x0400044A RID: 1098
		public bool supportsLightLayers;

		// Token: 0x0400044B RID: 1099
		public bool supportsAdditionalLights;
	}
}
