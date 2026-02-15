using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x02000246 RID: 582
	[MovedFrom(true, "UnityEngine.Experimental.Rendering.RenderGraphModule", "UnityEngine.Rendering.RenderGraphModule", null)]
	public struct RenderGraphProfilingScope : IDisposable
	{
		// Token: 0x06000F8D RID: 3981 RVA: 0x00005704 File Offset: 0x00003904
		public RenderGraphProfilingScope(RenderGraph renderGraph, ProfilingSampler sampler)
		{
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x00038FA5 File Offset: 0x000371A5
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x00005704 File Offset: 0x00003904
		private void Dispose(bool disposing)
		{
		}
	}
}
