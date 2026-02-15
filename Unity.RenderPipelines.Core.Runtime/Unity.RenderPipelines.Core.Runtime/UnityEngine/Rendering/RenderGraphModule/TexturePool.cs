using System;
using UnityEngine.Profiling;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x02000274 RID: 628
	internal class TexturePool : RenderGraphResourcePool<RTHandle>
	{
		// Token: 0x06001120 RID: 4384 RVA: 0x0003E222 File Offset: 0x0003C422
		protected override void ReleaseInternalResource(RTHandle res)
		{
			res.Release();
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x0003E22A File Offset: 0x0003C42A
		protected override string GetResourceName(in RTHandle res)
		{
			return res.rt.name;
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x0003E238 File Offset: 0x0003C438
		protected override long GetResourceSize(in RTHandle res)
		{
			return Profiler.GetRuntimeMemorySizeLong(res.rt);
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x0003E246 File Offset: 0x0003C446
		protected override string GetResourceTypeName()
		{
			return "Texture";
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x0003E24D File Offset: 0x0003C44D
		protected override int GetSortIndex(RTHandle res)
		{
			return res.GetInstanceID();
		}
	}
}
