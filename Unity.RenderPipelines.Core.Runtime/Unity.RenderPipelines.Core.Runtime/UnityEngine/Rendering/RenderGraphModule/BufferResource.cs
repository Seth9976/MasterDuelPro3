using System;
using System.Diagnostics;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x0200025E RID: 606
	[DebuggerDisplay("BufferResource ({desc.name})")]
	internal class BufferResource : RenderGraphResource<BufferDesc, GraphicsBuffer>
	{
		// Token: 0x0600106B RID: 4203 RVA: 0x0003B79C File Offset: 0x0003999C
		public override string GetName()
		{
			if (this.imported)
			{
				return "ImportedGraphicsBuffer";
			}
			return this.desc.name;
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x0003B7B7 File Offset: 0x000399B7
		public override int GetDescHashCode()
		{
			return this.desc.GetHashCode();
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x0003B7CA File Offset: 0x000399CA
		public override void CreateGraphicsResource()
		{
			this.GetName();
			this.graphicsResource = new GraphicsBuffer(this.desc.target, this.desc.usageFlags, this.desc.count, this.desc.stride);
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x0003B80A File Offset: 0x00039A0A
		public override void UpdateGraphicsResource()
		{
			if (this.graphicsResource != null)
			{
				this.graphicsResource.name = this.GetName();
			}
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x0003B825 File Offset: 0x00039A25
		public override void ReleaseGraphicsResource()
		{
			if (this.graphicsResource != null)
			{
				this.graphicsResource.Release();
			}
			base.ReleaseGraphicsResource();
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x0003B840 File Offset: 0x00039A40
		public override void LogCreation(RenderGraphLogger logger)
		{
			logger.LogLine("Created GraphicsBuffer: " + this.desc.name, Array.Empty<object>());
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x0003B862 File Offset: 0x00039A62
		public override void LogRelease(RenderGraphLogger logger)
		{
			logger.LogLine("Released GraphicsBuffer: " + this.desc.name, Array.Empty<object>());
		}
	}
}
