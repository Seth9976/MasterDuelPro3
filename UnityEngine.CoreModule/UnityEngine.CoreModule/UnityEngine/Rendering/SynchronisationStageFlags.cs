using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000355 RID: 853
	public enum SynchronisationStageFlags
	{
		// Token: 0x04000A07 RID: 2567
		VertexProcessing = 1,
		// Token: 0x04000A08 RID: 2568
		PixelProcessing,
		// Token: 0x04000A09 RID: 2569
		ComputeProcessing = 4,
		// Token: 0x04000A0A RID: 2570
		AllGPUOperations = 7
	}
}
