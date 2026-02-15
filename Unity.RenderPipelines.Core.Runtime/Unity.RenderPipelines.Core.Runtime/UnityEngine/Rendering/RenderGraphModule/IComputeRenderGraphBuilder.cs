using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x02000224 RID: 548
	[MovedFrom(true, "UnityEngine.Experimental.Rendering.RenderGraphModule", "UnityEngine.Rendering.RenderGraphModule", null)]
	public interface IComputeRenderGraphBuilder : IBaseRenderGraphBuilder, IDisposable
	{
		// Token: 0x06000ECB RID: 3787
		void SetRenderFunc<PassData>(BaseRenderFunc<PassData, ComputeGraphContext> renderFunc) where PassData : class, new();
	}
}
