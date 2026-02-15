using System;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x0200026E RID: 622
	internal struct TextureAccess
	{
		// Token: 0x060010FF RID: 4351 RVA: 0x0003D8B8 File Offset: 0x0003BAB8
		public TextureAccess(TextureHandle handle, AccessFlags flags, int mipLevel, int depthSlice)
		{
			this.textureHandle = handle;
			this.flags = flags;
			this.mipLevel = mipLevel;
			this.depthSlice = depthSlice;
		}

		// Token: 0x04000AB0 RID: 2736
		public TextureHandle textureHandle;

		// Token: 0x04000AB1 RID: 2737
		public int mipLevel;

		// Token: 0x04000AB2 RID: 2738
		public int depthSlice;

		// Token: 0x04000AB3 RID: 2739
		public AccessFlags flags;
	}
}
