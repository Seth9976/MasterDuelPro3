using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000386 RID: 902
	public struct BatchRendererGroupCreateInfo
	{
		// Token: 0x04000AFF RID: 2815
		public BatchRendererGroup.OnPerformCulling cullingCallback;

		// Token: 0x04000B00 RID: 2816
		public BatchRendererGroup.OnFinishedCulling finishedCullingCallback;

		// Token: 0x04000B01 RID: 2817
		public IntPtr userContext;
	}
}
