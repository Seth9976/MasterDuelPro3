using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x02000240 RID: 576
	[MovedFrom(true, "UnityEngine.Experimental.Rendering.RenderGraphModule", "UnityEngine.Rendering.RenderGraphModule", null)]
	public struct RenderGraphContext : IDerivedRendergraphContext
	{
		// Token: 0x06000F76 RID: 3958 RVA: 0x00038E42 File Offset: 0x00037042
		public void FromInternalContext(InternalRenderGraphContext context)
		{
			this.wrappedContext = context;
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000F77 RID: 3959 RVA: 0x00038E4B File Offset: 0x0003704B
		public ScriptableRenderContext renderContext
		{
			get
			{
				return this.wrappedContext.renderContext;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x00038E58 File Offset: 0x00037058
		public CommandBuffer cmd
		{
			get
			{
				return this.wrappedContext.cmd;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000F79 RID: 3961 RVA: 0x00038E65 File Offset: 0x00037065
		public RenderGraphObjectPool renderGraphPool
		{
			get
			{
				return this.wrappedContext.renderGraphPool;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000F7A RID: 3962 RVA: 0x00038E72 File Offset: 0x00037072
		public RenderGraphDefaultResources defaultResources
		{
			get
			{
				return this.wrappedContext.defaultResources;
			}
		}

		// Token: 0x04000A1B RID: 2587
		private InternalRenderGraphContext wrappedContext;
	}
}
