using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200037C RID: 892
	public struct BatchDrawCommandProcedural
	{
		// Token: 0x04000A99 RID: 2713
		public BatchDrawCommandFlags flags;

		// Token: 0x04000A9A RID: 2714
		public BatchID batchID;

		// Token: 0x04000A9B RID: 2715
		public BatchMaterialID materialID;

		// Token: 0x04000A9C RID: 2716
		public ushort splitVisibilityMask;

		// Token: 0x04000A9D RID: 2717
		public ushort lightmapIndex;

		// Token: 0x04000A9E RID: 2718
		public int sortingPosition;

		// Token: 0x04000A9F RID: 2719
		public uint visibleOffset;

		// Token: 0x04000AA0 RID: 2720
		public uint visibleCount;

		// Token: 0x04000AA1 RID: 2721
		public MeshTopology topology;

		// Token: 0x04000AA2 RID: 2722
		public GraphicsBufferHandle indexBufferHandle;

		// Token: 0x04000AA3 RID: 2723
		public uint baseVertex;

		// Token: 0x04000AA4 RID: 2724
		public uint indexOffsetBytes;

		// Token: 0x04000AA5 RID: 2725
		public uint elementCount;
	}
}
