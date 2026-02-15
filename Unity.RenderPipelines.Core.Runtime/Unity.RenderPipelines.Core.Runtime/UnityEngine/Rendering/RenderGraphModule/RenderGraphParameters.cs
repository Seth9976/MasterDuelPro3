using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x02000244 RID: 580
	[MovedFrom(true, "UnityEngine.Experimental.Rendering.RenderGraphModule", "UnityEngine.Rendering.RenderGraphModule", null)]
	public struct RenderGraphParameters
	{
		// Token: 0x04000A25 RID: 2597
		public string executionName;

		// Token: 0x04000A26 RID: 2598
		public int currentFrameIndex;

		// Token: 0x04000A27 RID: 2599
		public bool rendererListCulling;

		// Token: 0x04000A28 RID: 2600
		public ScriptableRenderContext scriptableRenderContext;

		// Token: 0x04000A29 RID: 2601
		public CommandBuffer commandBuffer;

		// Token: 0x04000A2A RID: 2602
		internal bool invalidContextForTesting;
	}
}
