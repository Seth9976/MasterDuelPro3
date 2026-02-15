using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x02000252 RID: 594
	[DebuggerDisplay("RenderPass: {name} (Index:{index} Async:{enableAsyncCompute})")]
	internal abstract class BaseRenderGraphPass<PassData, TRenderGraphContext> : RenderGraphPass where PassData : class, new()
	{
		// Token: 0x06001046 RID: 4166 RVA: 0x0003B4E3 File Offset: 0x000396E3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Initialize(int passIndex, PassData passData, string passName, RenderGraphPassType passType, ProfilingSampler sampler)
		{
			base.Clear();
			base.index = passIndex;
			this.data = passData;
			base.name = passName;
			base.type = passType;
			base.customSampler = sampler;
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x0003B510 File Offset: 0x00039710
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override void Release(RenderGraphObjectPool pool)
		{
			pool.Release<PassData>(this.data);
			this.data = default(PassData);
			this.renderFunc = null;
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x0003B531 File Offset: 0x00039731
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool HasRenderFunc()
		{
			return this.renderFunc != null;
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x0003B53C File Offset: 0x0003973C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetRenderFuncHash()
		{
			if (this.renderFunc == null)
			{
				return 0;
			}
			return HashFNV1A32.GetFuncHashCode(this.renderFunc);
		}

		// Token: 0x04000A67 RID: 2663
		internal PassData data;

		// Token: 0x04000A68 RID: 2664
		internal BaseRenderFunc<PassData, TRenderGraphContext> renderFunc;
	}
}
