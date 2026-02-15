using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x02000243 RID: 579
	[MovedFrom(true, "UnityEngine.Experimental.Rendering.RenderGraphModule", "UnityEngine.Rendering.RenderGraphModule", null)]
	public class UnsafeGraphContext : IDerivedRendergraphContext
	{
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000F84 RID: 3972 RVA: 0x00038F43 File Offset: 0x00037143
		public RenderGraphDefaultResources defaultResources
		{
			get
			{
				return this.wrappedContext.defaultResources;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000F85 RID: 3973 RVA: 0x00038F50 File Offset: 0x00037150
		public RenderGraphObjectPool renderGraphPool
		{
			get
			{
				return this.wrappedContext.renderGraphPool;
			}
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x00038F5D File Offset: 0x0003715D
		public void FromInternalContext(InternalRenderGraphContext context)
		{
			this.wrappedContext = context;
			UnsafeGraphContext.unsCmd.m_WrappedCommandBuffer = this.wrappedContext.cmd;
			UnsafeGraphContext.unsCmd.m_ExecutingPass = context.executingPass;
			this.cmd = UnsafeGraphContext.unsCmd;
		}

		// Token: 0x04000A22 RID: 2594
		private InternalRenderGraphContext wrappedContext;

		// Token: 0x04000A23 RID: 2595
		public UnsafeCommandBuffer cmd;

		// Token: 0x04000A24 RID: 2596
		internal static UnsafeCommandBuffer unsCmd = new UnsafeCommandBuffer(null, null, false);
	}
}
