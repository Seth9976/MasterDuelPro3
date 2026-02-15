using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000193 RID: 403
	public struct ShadowSliceData
	{
		// Token: 0x0600087F RID: 2175 RVA: 0x0002880C File Offset: 0x00026A0C
		public void Clear()
		{
			this.viewMatrix = Matrix4x4.identity;
			this.projectionMatrix = Matrix4x4.identity;
			this.shadowTransform = Matrix4x4.identity;
			this.offsetX = (this.offsetY = 0);
			this.resolution = 1024;
		}

		// Token: 0x040008EC RID: 2284
		public Matrix4x4 viewMatrix;

		// Token: 0x040008ED RID: 2285
		public Matrix4x4 projectionMatrix;

		// Token: 0x040008EE RID: 2286
		public Matrix4x4 shadowTransform;

		// Token: 0x040008EF RID: 2287
		public int offsetX;

		// Token: 0x040008F0 RID: 2288
		public int offsetY;

		// Token: 0x040008F1 RID: 2289
		public int resolution;

		// Token: 0x040008F2 RID: 2290
		public ShadowSplitData splitData;
	}
}
