using System;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x0200025F RID: 607
	internal class BufferPool : RenderGraphResourcePool<GraphicsBuffer>
	{
		// Token: 0x06001073 RID: 4211 RVA: 0x0003B88C File Offset: 0x00039A8C
		protected override void ReleaseInternalResource(GraphicsBuffer res)
		{
			res.Release();
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x0003B894 File Offset: 0x00039A94
		protected override string GetResourceName(in GraphicsBuffer res)
		{
			return "GraphicsBufferNameNotAvailable";
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x0003B89B File Offset: 0x00039A9B
		protected override long GetResourceSize(in GraphicsBuffer res)
		{
			return (long)(res.count * res.stride);
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x0003B8AD File Offset: 0x00039AAD
		protected override string GetResourceTypeName()
		{
			return "GraphicsBuffer";
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x0003B8B4 File Offset: 0x00039AB4
		protected override int GetSortIndex(GraphicsBuffer res)
		{
			return res.GetHashCode();
		}
	}
}
