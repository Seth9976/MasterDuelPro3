using System;
using Unity.Jobs;

namespace UnityEngine.Rendering
{
	// Token: 0x02000095 RID: 149
	internal struct IndirectBufferContext
	{
		// Token: 0x06000279 RID: 633 RVA: 0x0001021B File Offset: 0x0000E41B
		public IndirectBufferContext(JobHandle cullingJobHandle)
		{
			this.cullingJobHandle = cullingJobHandle;
			this.bufferState = IndirectBufferContext.BufferState.Pending;
			this.occluderVersion = 0;
			this.subviewMask = 0;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00010239 File Offset: 0x0000E439
		public bool Matches(IndirectBufferContext.BufferState bufferState, int occluderVersion, int subviewMask)
		{
			return this.bufferState == bufferState && this.occluderVersion == occluderVersion && this.subviewMask == subviewMask;
		}

		// Token: 0x04000307 RID: 775
		public JobHandle cullingJobHandle;

		// Token: 0x04000308 RID: 776
		public IndirectBufferContext.BufferState bufferState;

		// Token: 0x04000309 RID: 777
		public int occluderVersion;

		// Token: 0x0400030A RID: 778
		public int subviewMask;

		// Token: 0x02000096 RID: 150
		public enum BufferState
		{
			// Token: 0x0400030C RID: 780
			Pending,
			// Token: 0x0400030D RID: 781
			Zeroed,
			// Token: 0x0400030E RID: 782
			NoOcclusionTest,
			// Token: 0x0400030F RID: 783
			AllInstancesOcclusionTested,
			// Token: 0x04000310 RID: 784
			OccludedInstancesReTested
		}
	}
}
