using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000380 RID: 896
	public struct BatchCullingOutputDrawCommands
	{
		// Token: 0x04000AC3 RID: 2755
		public unsafe BatchDrawCommand* drawCommands;

		// Token: 0x04000AC4 RID: 2756
		public unsafe BatchDrawCommandIndirect* indirectDrawCommands;

		// Token: 0x04000AC5 RID: 2757
		public unsafe BatchDrawCommandProcedural* proceduralDrawCommands;

		// Token: 0x04000AC6 RID: 2758
		public unsafe BatchDrawCommandProceduralIndirect* proceduralIndirectDrawCommands;

		// Token: 0x04000AC7 RID: 2759
		public unsafe int* visibleInstances;

		// Token: 0x04000AC8 RID: 2760
		public unsafe BatchDrawRange* drawRanges;

		// Token: 0x04000AC9 RID: 2761
		public unsafe float* instanceSortingPositions;

		// Token: 0x04000ACA RID: 2762
		public unsafe int* drawCommandPickingInstanceIDs;

		// Token: 0x04000ACB RID: 2763
		public int drawCommandCount;

		// Token: 0x04000ACC RID: 2764
		public int indirectDrawCommandCount;

		// Token: 0x04000ACD RID: 2765
		public int proceduralDrawCommandCount;

		// Token: 0x04000ACE RID: 2766
		public int proceduralIndirectDrawCommandCount;

		// Token: 0x04000ACF RID: 2767
		public int visibleInstanceCount;

		// Token: 0x04000AD0 RID: 2768
		public int drawRangeCount;

		// Token: 0x04000AD1 RID: 2769
		public int instanceSortingPositionFloatCount;
	}
}
