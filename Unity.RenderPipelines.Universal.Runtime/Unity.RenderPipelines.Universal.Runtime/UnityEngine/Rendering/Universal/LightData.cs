using System;
using Unity.Collections;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020001C9 RID: 457
	public struct LightData
	{
		// Token: 0x060009DF RID: 2527 RVA: 0x0003259A File Offset: 0x0003079A
		internal LightData(ContextContainer frameData)
		{
			this.frameData = frameData;
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x000325A3 File Offset: 0x000307A3
		internal UniversalLightData universalLightData
		{
			get
			{
				return this.frameData.Get<UniversalLightData>();
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x000325B0 File Offset: 0x000307B0
		public ref int mainLightIndex
		{
			get
			{
				return ref this.frameData.Get<UniversalLightData>().mainLightIndex;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x000325C2 File Offset: 0x000307C2
		public ref int additionalLightsCount
		{
			get
			{
				return ref this.frameData.Get<UniversalLightData>().additionalLightsCount;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x000325D4 File Offset: 0x000307D4
		public ref int maxPerObjectAdditionalLightsCount
		{
			get
			{
				return ref this.frameData.Get<UniversalLightData>().maxPerObjectAdditionalLightsCount;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x000325E6 File Offset: 0x000307E6
		public ref NativeArray<VisibleLight> visibleLights
		{
			get
			{
				return ref this.frameData.Get<UniversalLightData>().visibleLights;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x000325F8 File Offset: 0x000307F8
		public ref bool shadeAdditionalLightsPerVertex
		{
			get
			{
				return ref this.frameData.Get<UniversalLightData>().shadeAdditionalLightsPerVertex;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x0003260A File Offset: 0x0003080A
		public ref bool supportsMixedLighting
		{
			get
			{
				return ref this.frameData.Get<UniversalLightData>().supportsMixedLighting;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x0003261C File Offset: 0x0003081C
		public ref bool reflectionProbeBoxProjection
		{
			get
			{
				return ref this.frameData.Get<UniversalLightData>().reflectionProbeBoxProjection;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x0003262E File Offset: 0x0003082E
		public ref bool reflectionProbeBlending
		{
			get
			{
				return ref this.frameData.Get<UniversalLightData>().reflectionProbeBlending;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x00032640 File Offset: 0x00030840
		public ref bool supportsLightLayers
		{
			get
			{
				return ref this.frameData.Get<UniversalLightData>().supportsLightLayers;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x00032652 File Offset: 0x00030852
		public ref bool supportsAdditionalLights
		{
			get
			{
				return ref this.frameData.Get<UniversalLightData>().supportsAdditionalLights;
			}
		}

		// Token: 0x04000A0A RID: 2570
		private ContextContainer frameData;
	}
}
