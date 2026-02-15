using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x02000254 RID: 596
	[DebuggerDisplay("RenderPass: {name} (Index:{index} Async:{enableAsyncCompute})")]
	internal sealed class ComputeRenderGraphPass<PassData> : BaseRenderGraphPass<PassData, ComputeGraphContext> where PassData : class, new()
	{
		// Token: 0x0600104F RID: 4175 RVA: 0x0003B596 File Offset: 0x00039796
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override void Execute(InternalRenderGraphContext renderGraphContext)
		{
			ComputeRenderGraphPass<PassData>.c.FromInternalContext(renderGraphContext);
			this.renderFunc(this.data, ComputeRenderGraphPass<PassData>.c);
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x0003B5B9 File Offset: 0x000397B9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override void Release(RenderGraphObjectPool pool)
		{
			base.Release(pool);
			pool.Release<ComputeRenderGraphPass<PassData>>(this);
		}

		// Token: 0x04000A6A RID: 2666
		internal static ComputeGraphContext c = new ComputeGraphContext();
	}
}
