using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x02000256 RID: 598
	[DebuggerDisplay("RenderPass: {name} (Index:{index} Async:{enableAsyncCompute})")]
	internal sealed class UnsafeRenderGraphPass<PassData> : BaseRenderGraphPass<PassData, UnsafeGraphContext> where PassData : class, new()
	{
		// Token: 0x06001057 RID: 4183 RVA: 0x0003B618 File Offset: 0x00039818
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override void Execute(InternalRenderGraphContext renderGraphContext)
		{
			UnsafeRenderGraphPass<PassData>.c.FromInternalContext(renderGraphContext);
			this.renderFunc(this.data, UnsafeRenderGraphPass<PassData>.c);
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x0003B63B File Offset: 0x0003983B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override void Release(RenderGraphObjectPool pool)
		{
			base.Release(pool);
			pool.Release<UnsafeRenderGraphPass<PassData>>(this);
		}

		// Token: 0x04000A6C RID: 2668
		internal static UnsafeGraphContext c = new UnsafeGraphContext();
	}
}
