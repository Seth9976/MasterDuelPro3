using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x02000255 RID: 597
	[DebuggerDisplay("RenderPass: {name} (Index:{index} Async:{enableAsyncCompute})")]
	internal sealed class RasterRenderGraphPass<PassData> : BaseRenderGraphPass<PassData, RasterGraphContext> where PassData : class, new()
	{
		// Token: 0x06001053 RID: 4179 RVA: 0x0003B5DD File Offset: 0x000397DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override void Execute(InternalRenderGraphContext renderGraphContext)
		{
			RasterRenderGraphPass<PassData>.c.FromInternalContext(renderGraphContext);
			this.renderFunc(this.data, RasterRenderGraphPass<PassData>.c);
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x0003B600 File Offset: 0x00039800
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override void Release(RenderGraphObjectPool pool)
		{
			base.Release(pool);
			pool.Release<RasterRenderGraphPass<PassData>>(this);
		}

		// Token: 0x04000A6B RID: 2667
		internal static RasterGraphContext c;
	}
}
