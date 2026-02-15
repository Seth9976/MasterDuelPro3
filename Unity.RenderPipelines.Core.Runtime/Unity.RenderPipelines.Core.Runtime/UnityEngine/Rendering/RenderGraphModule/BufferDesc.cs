using System;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x0200025D RID: 605
	public struct BufferDesc
	{
		// Token: 0x06001068 RID: 4200 RVA: 0x0003B6FC File Offset: 0x000398FC
		public BufferDesc(int count, int stride)
		{
			this = default(BufferDesc);
			this.count = count;
			this.stride = stride;
			this.target = GraphicsBuffer.Target.Structured;
			this.usageFlags = GraphicsBuffer.UsageFlags.None;
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x0003B722 File Offset: 0x00039922
		public BufferDesc(int count, int stride, GraphicsBuffer.Target target)
		{
			this = default(BufferDesc);
			this.count = count;
			this.stride = stride;
			this.target = target;
			this.usageFlags = GraphicsBuffer.UsageFlags.None;
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x0003B748 File Offset: 0x00039948
		public override int GetHashCode()
		{
			HashFNV1A32 hashCode = HashFNV1A32.Create();
			hashCode.Append(in this.count);
			hashCode.Append(in this.stride);
			int num = (int)this.target;
			hashCode.Append(in num);
			num = (int)this.usageFlags;
			hashCode.Append(in num);
			return hashCode.value;
		}

		// Token: 0x04000A7B RID: 2683
		public int count;

		// Token: 0x04000A7C RID: 2684
		public int stride;

		// Token: 0x04000A7D RID: 2685
		public string name;

		// Token: 0x04000A7E RID: 2686
		public GraphicsBuffer.Target target;

		// Token: 0x04000A7F RID: 2687
		public GraphicsBuffer.UsageFlags usageFlags;
	}
}
