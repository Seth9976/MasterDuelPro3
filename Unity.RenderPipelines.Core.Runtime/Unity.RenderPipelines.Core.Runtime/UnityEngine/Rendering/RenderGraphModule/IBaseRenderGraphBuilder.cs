using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x02000223 RID: 547
	[MovedFrom(true, "UnityEngine.Experimental.Rendering.RenderGraphModule", "UnityEngine.Rendering.RenderGraphModule", null)]
	public interface IBaseRenderGraphBuilder : IDisposable
	{
		// Token: 0x06000EBD RID: 3773
		void UseTexture(in TextureHandle input, AccessFlags flags = AccessFlags.Read);

		// Token: 0x06000EBE RID: 3774
		void UseGlobalTexture(int propertyId, AccessFlags flags = AccessFlags.Read);

		// Token: 0x06000EBF RID: 3775
		void UseAllGlobalTextures(bool enable);

		// Token: 0x06000EC0 RID: 3776
		void SetGlobalTextureAfterPass(in TextureHandle input, int propertyId);

		// Token: 0x06000EC1 RID: 3777
		BufferHandle UseBuffer(in BufferHandle input, AccessFlags flags = AccessFlags.Read);

		// Token: 0x06000EC2 RID: 3778
		TextureHandle CreateTransientTexture(in TextureDesc desc);

		// Token: 0x06000EC3 RID: 3779
		TextureHandle CreateTransientTexture(in TextureHandle texture);

		// Token: 0x06000EC4 RID: 3780
		BufferHandle CreateTransientBuffer(in BufferDesc desc);

		// Token: 0x06000EC5 RID: 3781
		BufferHandle CreateTransientBuffer(in BufferHandle computebuffer);

		// Token: 0x06000EC6 RID: 3782
		void UseRendererList(in RendererListHandle input);

		// Token: 0x06000EC7 RID: 3783
		void EnableAsyncCompute(bool value);

		// Token: 0x06000EC8 RID: 3784
		void AllowPassCulling(bool value);

		// Token: 0x06000EC9 RID: 3785
		void AllowGlobalStateModification(bool value);

		// Token: 0x06000ECA RID: 3786
		void EnableFoveatedRasterization(bool value);
	}
}
