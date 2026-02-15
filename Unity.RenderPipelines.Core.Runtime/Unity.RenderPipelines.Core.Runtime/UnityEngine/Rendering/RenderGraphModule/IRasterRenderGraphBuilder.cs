using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x02000226 RID: 550
	[MovedFrom(true, "UnityEngine.Experimental.Rendering.RenderGraphModule", "UnityEngine.Rendering.RenderGraphModule", null)]
	public interface IRasterRenderGraphBuilder : IBaseRenderGraphBuilder, IDisposable
	{
		// Token: 0x06000ECD RID: 3789 RVA: 0x000356F1 File Offset: 0x000338F1
		void SetRenderAttachment(TextureHandle tex, int index, AccessFlags flags = AccessFlags.Write)
		{
			this.SetRenderAttachment(tex, index, flags, 0, -1);
		}

		// Token: 0x06000ECE RID: 3790
		void SetRenderAttachment(TextureHandle tex, int index, AccessFlags flags, int mipLevel, int depthSlice);

		// Token: 0x06000ECF RID: 3791 RVA: 0x000356FE File Offset: 0x000338FE
		void SetInputAttachment(TextureHandle tex, int index, AccessFlags flags = AccessFlags.Read)
		{
			this.SetInputAttachment(tex, index, flags, 0, -1);
		}

		// Token: 0x06000ED0 RID: 3792
		void SetInputAttachment(TextureHandle tex, int index, AccessFlags flags, int mipLevel, int depthSlice);

		// Token: 0x06000ED1 RID: 3793 RVA: 0x0003570B File Offset: 0x0003390B
		void SetRenderAttachmentDepth(TextureHandle tex, AccessFlags flags = AccessFlags.Write)
		{
			this.SetRenderAttachmentDepth(tex, flags, 0, -1);
		}

		// Token: 0x06000ED2 RID: 3794
		void SetRenderAttachmentDepth(TextureHandle tex, AccessFlags flags, int mipLevel, int depthSlice);

		// Token: 0x06000ED3 RID: 3795
		TextureHandle SetRandomAccessAttachment(TextureHandle tex, int index, AccessFlags flags = AccessFlags.ReadWrite);

		// Token: 0x06000ED4 RID: 3796
		BufferHandle UseBufferRandomAccess(BufferHandle tex, int index, AccessFlags flags = AccessFlags.Read);

		// Token: 0x06000ED5 RID: 3797
		BufferHandle UseBufferRandomAccess(BufferHandle tex, int index, bool preserveCounterValue, AccessFlags flags = AccessFlags.Read);

		// Token: 0x06000ED6 RID: 3798
		void SetRenderFunc<PassData>(BaseRenderFunc<PassData, RasterGraphContext> renderFunc) where PassData : class, new();
	}
}
