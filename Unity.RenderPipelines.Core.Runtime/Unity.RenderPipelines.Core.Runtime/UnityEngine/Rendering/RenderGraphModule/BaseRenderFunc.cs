using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x02000245 RID: 581
	// (Invoke) Token: 0x06000F8A RID: 3978
	[MovedFrom(true, "UnityEngine.Experimental.Rendering.RenderGraphModule", "UnityEngine.Rendering.RenderGraphModule", null)]
	public delegate void BaseRenderFunc<PassData, ContextType>(PassData data, ContextType renderGraphContext) where PassData : class, new();
}
