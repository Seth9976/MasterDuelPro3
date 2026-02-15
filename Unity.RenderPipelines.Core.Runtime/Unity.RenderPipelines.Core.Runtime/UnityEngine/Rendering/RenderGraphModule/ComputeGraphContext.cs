using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x02000242 RID: 578
	[MovedFrom(true, "UnityEngine.Experimental.Rendering.RenderGraphModule", "UnityEngine.Rendering.RenderGraphModule", null)]
	public class ComputeGraphContext : IDerivedRendergraphContext
	{
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000F7F RID: 3967 RVA: 0x00038EE1 File Offset: 0x000370E1
		public RenderGraphDefaultResources defaultResources
		{
			get
			{
				return this.wrappedContext.defaultResources;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000F80 RID: 3968 RVA: 0x00038EEE File Offset: 0x000370EE
		public RenderGraphObjectPool renderGraphPool
		{
			get
			{
				return this.wrappedContext.renderGraphPool;
			}
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x00038EFB File Offset: 0x000370FB
		public void FromInternalContext(InternalRenderGraphContext context)
		{
			this.wrappedContext = context;
			ComputeGraphContext.computecmd.m_WrappedCommandBuffer = this.wrappedContext.cmd;
			ComputeGraphContext.computecmd.m_ExecutingPass = context.executingPass;
			this.cmd = ComputeGraphContext.computecmd;
		}

		// Token: 0x04000A1F RID: 2591
		private InternalRenderGraphContext wrappedContext;

		// Token: 0x04000A20 RID: 2592
		public ComputeCommandBuffer cmd;

		// Token: 0x04000A21 RID: 2593
		internal static ComputeCommandBuffer computecmd = new ComputeCommandBuffer(null, null, false);
	}
}
