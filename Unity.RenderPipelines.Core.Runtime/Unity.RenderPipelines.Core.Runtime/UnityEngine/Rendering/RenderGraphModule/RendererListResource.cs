using System;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x0200026C RID: 620
	internal struct RendererListResource
	{
		// Token: 0x060010FD RID: 4349 RVA: 0x0003D888 File Offset: 0x0003BA88
		internal RendererListResource(in RendererListParams desc)
		{
			this.desc = desc;
			this.rendererList = default(RendererList);
		}

		// Token: 0x04000AAC RID: 2732
		public RendererListParams desc;

		// Token: 0x04000AAD RID: 2733
		public RendererList rendererList;
	}
}
