using System;
using System.Collections.Generic;
using Unity.Collections;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020001CB RID: 459
	public struct ShadowData
	{
		// Token: 0x06000A34 RID: 2612 RVA: 0x00032B8A File Offset: 0x00030D8A
		internal ShadowData(ContextContainer frameData)
		{
			this.frameData = frameData;
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000A35 RID: 2613 RVA: 0x00032B93 File Offset: 0x00030D93
		internal UniversalShadowData universalShadowData
		{
			get
			{
				return this.frameData.Get<UniversalShadowData>();
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000A36 RID: 2614 RVA: 0x00032BA0 File Offset: 0x00030DA0
		public ref bool supportsMainLightShadows
		{
			get
			{
				return ref this.frameData.Get<UniversalShadowData>().supportsMainLightShadows;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x00032BB2 File Offset: 0x00030DB2
		internal ref bool mainLightShadowsEnabled
		{
			get
			{
				return ref this.frameData.Get<UniversalShadowData>().mainLightShadowsEnabled;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x00032BC4 File Offset: 0x00030DC4
		public ref int mainLightShadowmapWidth
		{
			get
			{
				return ref this.frameData.Get<UniversalShadowData>().mainLightShadowmapWidth;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x00032BD6 File Offset: 0x00030DD6
		public ref int mainLightShadowmapHeight
		{
			get
			{
				return ref this.frameData.Get<UniversalShadowData>().mainLightShadowmapHeight;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x00032BE8 File Offset: 0x00030DE8
		public ref int mainLightShadowCascadesCount
		{
			get
			{
				return ref this.frameData.Get<UniversalShadowData>().mainLightShadowCascadesCount;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x00032BFA File Offset: 0x00030DFA
		public ref Vector3 mainLightShadowCascadesSplit
		{
			get
			{
				return ref this.frameData.Get<UniversalShadowData>().mainLightShadowCascadesSplit;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x00032C0C File Offset: 0x00030E0C
		public ref float mainLightShadowCascadeBorder
		{
			get
			{
				return ref this.frameData.Get<UniversalShadowData>().mainLightShadowCascadeBorder;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x00032C1E File Offset: 0x00030E1E
		public ref bool supportsAdditionalLightShadows
		{
			get
			{
				return ref this.frameData.Get<UniversalShadowData>().supportsAdditionalLightShadows;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x00032C30 File Offset: 0x00030E30
		internal ref bool additionalLightShadowsEnabled
		{
			get
			{
				return ref this.frameData.Get<UniversalShadowData>().additionalLightShadowsEnabled;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x00032C42 File Offset: 0x00030E42
		public ref int additionalLightsShadowmapWidth
		{
			get
			{
				return ref this.frameData.Get<UniversalShadowData>().additionalLightsShadowmapWidth;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x00032C54 File Offset: 0x00030E54
		public ref int additionalLightsShadowmapHeight
		{
			get
			{
				return ref this.frameData.Get<UniversalShadowData>().additionalLightsShadowmapHeight;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000A41 RID: 2625 RVA: 0x00032C66 File Offset: 0x00030E66
		public ref bool supportsSoftShadows
		{
			get
			{
				return ref this.frameData.Get<UniversalShadowData>().supportsSoftShadows;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x00032C78 File Offset: 0x00030E78
		public ref int shadowmapDepthBufferBits
		{
			get
			{
				return ref this.frameData.Get<UniversalShadowData>().shadowmapDepthBufferBits;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x00032C8A File Offset: 0x00030E8A
		public ref List<Vector4> bias
		{
			get
			{
				return ref this.frameData.Get<UniversalShadowData>().bias;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x00032C9C File Offset: 0x00030E9C
		public ref List<int> resolution
		{
			get
			{
				return ref this.frameData.Get<UniversalShadowData>().resolution;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x00032CAE File Offset: 0x00030EAE
		internal ref bool isKeywordAdditionalLightShadowsEnabled
		{
			get
			{
				return ref this.frameData.Get<UniversalShadowData>().isKeywordAdditionalLightShadowsEnabled;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x00032CC0 File Offset: 0x00030EC0
		internal ref bool isKeywordSoftShadowsEnabled
		{
			get
			{
				return ref this.frameData.Get<UniversalShadowData>().isKeywordSoftShadowsEnabled;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x00032CD2 File Offset: 0x00030ED2
		internal ref int mainLightShadowResolution
		{
			get
			{
				return ref this.frameData.Get<UniversalShadowData>().mainLightShadowResolution;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x00032CE4 File Offset: 0x00030EE4
		internal ref int mainLightRenderTargetWidth
		{
			get
			{
				return ref this.frameData.Get<UniversalShadowData>().mainLightRenderTargetWidth;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x00032CF6 File Offset: 0x00030EF6
		internal ref int mainLightRenderTargetHeight
		{
			get
			{
				return ref this.frameData.Get<UniversalShadowData>().mainLightRenderTargetHeight;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x00032D08 File Offset: 0x00030F08
		internal ref NativeArray<URPLightShadowCullingInfos> visibleLightsShadowCullingInfos
		{
			get
			{
				return ref this.frameData.Get<UniversalShadowData>().visibleLightsShadowCullingInfos;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000A4B RID: 2635 RVA: 0x00032D1A File Offset: 0x00030F1A
		internal ref AdditionalLightsShadowAtlasLayout shadowAtlasLayout
		{
			get
			{
				return ref this.frameData.Get<UniversalShadowData>().shadowAtlasLayout;
			}
		}

		// Token: 0x04000A0C RID: 2572
		private ContextContainer frameData;
	}
}
