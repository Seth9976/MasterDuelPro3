using System;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x0200026D RID: 621
	internal struct RendererListLegacyResource
	{
		// Token: 0x060010FE RID: 4350 RVA: 0x0003D8A2 File Offset: 0x0003BAA2
		internal RendererListLegacyResource(in bool active = false)
		{
			this.rendererList = default(RendererList);
			this.isActive = active;
		}

		// Token: 0x04000AAE RID: 2734
		public RendererList rendererList;

		// Token: 0x04000AAF RID: 2735
		public bool isActive;
	}
}
