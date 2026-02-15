using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x02000253 RID: 595
	[DebuggerDisplay("RenderPass: {name} (Index:{index} Async:{enableAsyncCompute})")]
	internal sealed class RenderGraphPass<PassData> : BaseRenderGraphPass<PassData, RenderGraphContext> where PassData : class, new()
	{
		// Token: 0x0600104B RID: 4171 RVA: 0x0003B55B File Offset: 0x0003975B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override void Execute(InternalRenderGraphContext renderGraphContext)
		{
			RenderGraphPass<PassData>.c.FromInternalContext(renderGraphContext);
			this.renderFunc(this.data, RenderGraphPass<PassData>.c);
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x0003B57E File Offset: 0x0003977E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override void Release(RenderGraphObjectPool pool)
		{
			base.Release(pool);
			pool.Release<RenderGraphPass<PassData>>(this);
		}

		// Token: 0x04000A69 RID: 2665
		internal static RenderGraphContext c;
	}
}
