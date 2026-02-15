using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000CD RID: 205
	internal struct FrameTimeSample
	{
		// Token: 0x060006D3 RID: 1747 RVA: 0x000101F4 File Offset: 0x0000E3F4
		internal FrameTimeSample(float initValue)
		{
			this.FramesPerSecond = initValue;
			this.FullFrameTime = initValue;
			this.MainThreadCPUFrameTime = initValue;
			this.MainThreadCPUPresentWaitTime = initValue;
			this.RenderThreadCPUFrameTime = initValue;
			this.GPUFrameTime = initValue;
		}

		// Token: 0x04000281 RID: 641
		internal float FramesPerSecond;

		// Token: 0x04000282 RID: 642
		internal float FullFrameTime;

		// Token: 0x04000283 RID: 643
		internal float MainThreadCPUFrameTime;

		// Token: 0x04000284 RID: 644
		internal float MainThreadCPUPresentWaitTime;

		// Token: 0x04000285 RID: 645
		internal float RenderThreadCPUFrameTime;

		// Token: 0x04000286 RID: 646
		internal float GPUFrameTime;
	}
}
