using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000DE RID: 222
	[NativeHeader("Runtime/GfxDevice/FrameTiming.h")]
	public struct FrameTiming
	{
		// Token: 0x04000295 RID: 661
		[NativeName("totalFrameTime")]
		public double cpuFrameTime;

		// Token: 0x04000296 RID: 662
		[NativeName("mainThreadActiveTime")]
		public double cpuMainThreadFrameTime;

		// Token: 0x04000297 RID: 663
		[NativeName("mainThreadPresentWaitTime")]
		public double cpuMainThreadPresentWaitTime;

		// Token: 0x04000298 RID: 664
		[NativeName("renderThreadActiveTime")]
		public double cpuRenderThreadFrameTime;

		// Token: 0x04000299 RID: 665
		[NativeName("gpuFrameTime")]
		public double gpuFrameTime;

		// Token: 0x0400029A RID: 666
		[NativeName("frameStartTimestamp")]
		public ulong frameStartTimestamp;

		// Token: 0x0400029B RID: 667
		[NativeName("firstSubmitTimestamp")]
		public ulong firstSubmitTimestamp;

		// Token: 0x0400029C RID: 668
		[NativeName("presentFrameTimestamp")]
		public ulong cpuTimePresentCalled;

		// Token: 0x0400029D RID: 669
		[NativeName("frameCompleteTimestamp")]
		public ulong cpuTimeFrameComplete;

		// Token: 0x0400029E RID: 670
		[NativeName("heightScale")]
		public float heightScale;

		// Token: 0x0400029F RID: 671
		[NativeName("widthScale")]
		public float widthScale;

		// Token: 0x040002A0 RID: 672
		[NativeName("syncInterval")]
		public uint syncInterval;
	}
}
