using System;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x02000264 RID: 612
	[MovedFrom(true, "UnityEngine.Experimental.Rendering.RenderGraphModule", "UnityEngine.Rendering.RenderGraphModule", null)]
	public struct RenderTargetInfo
	{
		// Token: 0x04000A88 RID: 2696
		public int width;

		// Token: 0x04000A89 RID: 2697
		public int height;

		// Token: 0x04000A8A RID: 2698
		public int volumeDepth;

		// Token: 0x04000A8B RID: 2699
		public int msaaSamples;

		// Token: 0x04000A8C RID: 2700
		public GraphicsFormat format;

		// Token: 0x04000A8D RID: 2701
		public bool bindMS;
	}
}
