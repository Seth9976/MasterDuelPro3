using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000094 RID: 148
	internal struct IndirectBufferAllocInfo
	{
		// Token: 0x06000276 RID: 630 RVA: 0x000101D0 File Offset: 0x0000E3D0
		public bool IsEmpty()
		{
			return this.drawCount == 0;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x000101DB File Offset: 0x0000E3DB
		public bool IsWithinLimits(in IndirectBufferLimits limits)
		{
			return this.drawAllocIndex + this.drawCount <= limits.maxDrawCount && this.instanceAllocIndex + this.instanceCount <= limits.maxInstanceCount;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0001020C File Offset: 0x0000E40C
		public int GetExtraDrawInfoSlotIndex()
		{
			return this.drawAllocIndex + this.drawCount;
		}

		// Token: 0x04000303 RID: 771
		public int drawAllocIndex;

		// Token: 0x04000304 RID: 772
		public int drawCount;

		// Token: 0x04000305 RID: 773
		public int instanceAllocIndex;

		// Token: 0x04000306 RID: 774
		public int instanceCount;
	}
}
