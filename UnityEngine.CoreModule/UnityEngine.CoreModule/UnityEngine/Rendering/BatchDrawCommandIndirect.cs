using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200037B RID: 891
	public struct BatchDrawCommandIndirect
	{
		// Token: 0x04000A8B RID: 2699
		public BatchDrawCommandFlags flags;

		// Token: 0x04000A8C RID: 2700
		public BatchID batchID;

		// Token: 0x04000A8D RID: 2701
		public BatchMaterialID materialID;

		// Token: 0x04000A8E RID: 2702
		public ushort splitVisibilityMask;

		// Token: 0x04000A8F RID: 2703
		public ushort lightmapIndex;

		// Token: 0x04000A90 RID: 2704
		public int sortingPosition;

		// Token: 0x04000A91 RID: 2705
		public uint visibleOffset;

		// Token: 0x04000A92 RID: 2706
		public BatchMeshID meshID;

		// Token: 0x04000A93 RID: 2707
		public MeshTopology topology;

		// Token: 0x04000A94 RID: 2708
		public GraphicsBufferHandle visibleInstancesBufferHandle;

		// Token: 0x04000A95 RID: 2709
		public uint visibleInstancesBufferWindowOffset;

		// Token: 0x04000A96 RID: 2710
		public uint visibleInstancesBufferWindowSizeBytes;

		// Token: 0x04000A97 RID: 2711
		public GraphicsBufferHandle indirectArgsBufferHandle;

		// Token: 0x04000A98 RID: 2712
		public uint indirectArgsBufferOffset;
	}
}
