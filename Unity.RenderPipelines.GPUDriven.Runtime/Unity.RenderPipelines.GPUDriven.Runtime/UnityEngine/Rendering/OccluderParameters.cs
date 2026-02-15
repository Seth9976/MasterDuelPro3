using System;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering
{
	// Token: 0x0200002F RID: 47
	public struct OccluderParameters
	{
		// Token: 0x060000F0 RID: 240 RVA: 0x00005996 File Offset: 0x00003B96
		public OccluderParameters(int viewInstanceID)
		{
			this.viewInstanceID = viewInstanceID;
			this.subviewCount = 1;
			this.depthTexture = TextureHandle.nullHandle;
			this.depthSize = Vector2Int.zero;
			this.depthIsArray = false;
		}

		// Token: 0x0400009A RID: 154
		public int viewInstanceID;

		// Token: 0x0400009B RID: 155
		public int subviewCount;

		// Token: 0x0400009C RID: 156
		public TextureHandle depthTexture;

		// Token: 0x0400009D RID: 157
		public Vector2Int depthSize;

		// Token: 0x0400009E RID: 158
		public bool depthIsArray;
	}
}
