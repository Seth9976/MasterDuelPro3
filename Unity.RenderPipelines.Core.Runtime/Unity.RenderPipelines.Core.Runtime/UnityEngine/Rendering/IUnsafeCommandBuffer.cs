using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000024 RID: 36
	public interface IUnsafeCommandBuffer : IBaseCommandBuffer, IRasterCommandBuffer, IComputeCommandBuffer
	{
		// Token: 0x06000244 RID: 580
		void Clear();

		// Token: 0x06000245 RID: 581
		void SetRenderTarget(RenderTargetIdentifier rt);

		// Token: 0x06000246 RID: 582
		void SetRenderTarget(RenderTargetIdentifier rt, RenderBufferLoadAction loadAction, RenderBufferStoreAction storeAction);

		// Token: 0x06000247 RID: 583
		void SetRenderTarget(RenderTargetIdentifier rt, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction);

		// Token: 0x06000248 RID: 584
		void SetRenderTarget(RenderTargetIdentifier rt, int mipLevel);

		// Token: 0x06000249 RID: 585
		void SetRenderTarget(RenderTargetIdentifier rt, int mipLevel, CubemapFace cubemapFace);

		// Token: 0x0600024A RID: 586
		void SetRenderTarget(RenderTargetIdentifier rt, int mipLevel, CubemapFace cubemapFace, int depthSlice);

		// Token: 0x0600024B RID: 587
		void SetRenderTarget(RenderTargetIdentifier color, RenderTargetIdentifier depth);

		// Token: 0x0600024C RID: 588
		void SetRenderTarget(RenderTargetIdentifier color, RenderTargetIdentifier depth, int mipLevel);

		// Token: 0x0600024D RID: 589
		void SetRenderTarget(RenderTargetIdentifier color, RenderTargetIdentifier depth, int mipLevel, CubemapFace cubemapFace);

		// Token: 0x0600024E RID: 590
		void SetRenderTarget(RenderTargetIdentifier color, RenderTargetIdentifier depth, int mipLevel, CubemapFace cubemapFace, int depthSlice);

		// Token: 0x0600024F RID: 591
		void SetRenderTarget(RenderTargetIdentifier color, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderTargetIdentifier depth, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction);

		// Token: 0x06000250 RID: 592
		void SetRenderTarget(RenderTargetIdentifier[] colors, RenderTargetIdentifier depth);

		// Token: 0x06000251 RID: 593
		void SetRenderTarget(RenderTargetIdentifier[] colors, RenderTargetIdentifier depth, int mipLevel, CubemapFace cubemapFace, int depthSlice);

		// Token: 0x06000252 RID: 594
		void SetRenderTarget(RenderTargetBinding binding, int mipLevel, CubemapFace cubemapFace, int depthSlice);

		// Token: 0x06000253 RID: 595
		void SetRenderTarget(RenderTargetBinding binding);
	}
}
