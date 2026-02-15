using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200037D RID: 893
	public struct BatchDrawCommandProceduralIndirect
	{
		// Token: 0x04000AA6 RID: 2726
		public BatchDrawCommandFlags flags;

		// Token: 0x04000AA7 RID: 2727
		public BatchID batchID;

		// Token: 0x04000AA8 RID: 2728
		public BatchMaterialID materialID;

		// Token: 0x04000AA9 RID: 2729
		public ushort splitVisibilityMask;

		// Token: 0x04000AAA RID: 2730
		public ushort lightmapIndex;

		// Token: 0x04000AAB RID: 2731
		public int sortingPosition;

		// Token: 0x04000AAC RID: 2732
		public uint visibleOffset;

		// Token: 0x04000AAD RID: 2733
		public MeshTopology topology;

		// Token: 0x04000AAE RID: 2734
		public GraphicsBufferHandle indexBufferHandle;

		// Token: 0x04000AAF RID: 2735
		public GraphicsBufferHandle visibleInstancesBufferHandle;

		// Token: 0x04000AB0 RID: 2736
		public uint visibleInstancesBufferWindowOffset;

		// Token: 0x04000AB1 RID: 2737
		public uint visibleInstancesBufferWindowSizeBytes;

		// Token: 0x04000AB2 RID: 2738
		public GraphicsBufferHandle indirectArgsBufferHandle;

		// Token: 0x04000AB3 RID: 2739
		public uint indirectArgsBufferOffset;
	}
}
