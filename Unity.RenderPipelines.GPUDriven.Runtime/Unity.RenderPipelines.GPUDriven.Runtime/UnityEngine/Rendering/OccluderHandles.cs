using System;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering
{
	// Token: 0x02000090 RID: 144
	internal struct OccluderHandles
	{
		// Token: 0x06000273 RID: 627 RVA: 0x0001016F File Offset: 0x0000E36F
		public bool IsValid()
		{
			return this.occluderDepthPyramid.IsValid();
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0001017C File Offset: 0x0000E37C
		public void UseForOcclusionTest(IBaseRenderGraphBuilder builder)
		{
			builder.UseTexture(in this.occluderDepthPyramid, AccessFlags.Read);
			if (this.occlusionDebugOverlay.IsValid())
			{
				builder.UseBuffer(in this.occlusionDebugOverlay, AccessFlags.ReadWrite);
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x000101A6 File Offset: 0x0000E3A6
		public void UseForOccluderUpdate(IBaseRenderGraphBuilder builder)
		{
			builder.UseTexture(in this.occluderDepthPyramid, AccessFlags.ReadWrite);
			if (this.occlusionDebugOverlay.IsValid())
			{
				builder.UseBuffer(in this.occlusionDebugOverlay, AccessFlags.ReadWrite);
			}
		}

		// Token: 0x040002F6 RID: 758
		public TextureHandle occluderDepthPyramid;

		// Token: 0x040002F7 RID: 759
		public BufferHandle occlusionDebugOverlay;
	}
}
