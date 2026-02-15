using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x02000225 RID: 549
	[MovedFrom(true, "UnityEngine.Experimental.Rendering.RenderGraphModule", "UnityEngine.Rendering.RenderGraphModule", null)]
	public interface IUnsafeRenderGraphBuilder : IBaseRenderGraphBuilder, IDisposable
	{
		// Token: 0x06000ECC RID: 3788
		void SetRenderFunc<PassData>(BaseRenderFunc<PassData, UnsafeGraphContext> renderFunc) where PassData : class, new();
	}
}
