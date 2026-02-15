using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x02000228 RID: 552
	[MovedFrom(true, "UnityEngine.Experimental.Rendering.RenderGraphModule", "UnityEngine.Rendering.RenderGraphModule", null)]
	public interface IRenderGraphRecorder
	{
		// Token: 0x06000ED8 RID: 3800
		void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData);
	}
}
