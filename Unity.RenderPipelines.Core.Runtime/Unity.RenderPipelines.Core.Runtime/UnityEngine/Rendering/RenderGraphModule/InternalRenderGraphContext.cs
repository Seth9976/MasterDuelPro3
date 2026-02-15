using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x0200023E RID: 574
	[MovedFrom(true, "UnityEngine.Experimental.Rendering.RenderGraphModule", "UnityEngine.Rendering.RenderGraphModule", null)]
	public class InternalRenderGraphContext
	{
		// Token: 0x04000A15 RID: 2581
		internal ScriptableRenderContext renderContext;

		// Token: 0x04000A16 RID: 2582
		internal CommandBuffer cmd;

		// Token: 0x04000A17 RID: 2583
		internal RenderGraphObjectPool renderGraphPool;

		// Token: 0x04000A18 RID: 2584
		internal RenderGraphDefaultResources defaultResources;

		// Token: 0x04000A19 RID: 2585
		internal RenderGraphPass executingPass;

		// Token: 0x04000A1A RID: 2586
		internal bool contextlessTesting;
	}
}
