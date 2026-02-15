using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x02000241 RID: 577
	[MovedFrom(true, "UnityEngine.Experimental.Rendering.RenderGraphModule", "UnityEngine.Rendering.RenderGraphModule", null)]
	public struct RasterGraphContext : IDerivedRendergraphContext
	{
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000F7B RID: 3963 RVA: 0x00038E7F File Offset: 0x0003707F
		public RenderGraphDefaultResources defaultResources
		{
			get
			{
				return this.wrappedContext.defaultResources;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000F7C RID: 3964 RVA: 0x00038E8C File Offset: 0x0003708C
		public RenderGraphObjectPool renderGraphPool
		{
			get
			{
				return this.wrappedContext.renderGraphPool;
			}
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x00038E99 File Offset: 0x00037099
		public void FromInternalContext(InternalRenderGraphContext context)
		{
			this.wrappedContext = context;
			RasterGraphContext.rastercmd.m_WrappedCommandBuffer = this.wrappedContext.cmd;
			RasterGraphContext.rastercmd.m_ExecutingPass = context.executingPass;
			this.cmd = RasterGraphContext.rastercmd;
		}

		// Token: 0x04000A1C RID: 2588
		private InternalRenderGraphContext wrappedContext;

		// Token: 0x04000A1D RID: 2589
		public RasterCommandBuffer cmd;

		// Token: 0x04000A1E RID: 2590
		internal static RasterCommandBuffer rastercmd = new RasterCommandBuffer(null, null, false);
	}
}
