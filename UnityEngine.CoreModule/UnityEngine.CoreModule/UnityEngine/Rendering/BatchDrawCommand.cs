using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200037A RID: 890
	public struct BatchDrawCommand
	{
		// Token: 0x04000A80 RID: 2688
		public BatchDrawCommandFlags flags;

		// Token: 0x04000A81 RID: 2689
		public BatchID batchID;

		// Token: 0x04000A82 RID: 2690
		public BatchMaterialID materialID;

		// Token: 0x04000A83 RID: 2691
		public ushort splitVisibilityMask;

		// Token: 0x04000A84 RID: 2692
		public ushort lightmapIndex;

		// Token: 0x04000A85 RID: 2693
		public int sortingPosition;

		// Token: 0x04000A86 RID: 2694
		public uint visibleOffset;

		// Token: 0x04000A87 RID: 2695
		public uint visibleCount;

		// Token: 0x04000A88 RID: 2696
		public BatchMeshID meshID;

		// Token: 0x04000A89 RID: 2697
		public ushort submeshIndex;

		// Token: 0x04000A8A RID: 2698
		private ushort unusedPadding2;
	}
}
