using System;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering
{
	// Token: 0x0200009D RID: 157
	internal struct IndirectBufferContextHandles
	{
		// Token: 0x06000289 RID: 649 RVA: 0x00010DEC File Offset: 0x0000EFEC
		public void UseForOcclusionTest(IBaseRenderGraphBuilder builder)
		{
			this.instanceBuffer = builder.UseBuffer(in this.instanceBuffer, AccessFlags.ReadWrite);
			this.instanceInfoBuffer = builder.UseBuffer(in this.instanceInfoBuffer, AccessFlags.Read);
			this.argsBuffer = builder.UseBuffer(in this.argsBuffer, AccessFlags.ReadWrite);
			this.drawInfoBuffer = builder.UseBuffer(in this.drawInfoBuffer, AccessFlags.Read);
		}

		// Token: 0x04000332 RID: 818
		public BufferHandle instanceBuffer;

		// Token: 0x04000333 RID: 819
		public BufferHandle instanceInfoBuffer;

		// Token: 0x04000334 RID: 820
		public BufferHandle argsBuffer;

		// Token: 0x04000335 RID: 821
		public BufferHandle drawInfoBuffer;
	}
}
