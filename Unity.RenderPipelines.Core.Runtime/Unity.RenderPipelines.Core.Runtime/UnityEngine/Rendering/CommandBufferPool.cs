using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000027 RID: 39
	public static class CommandBufferPool
	{
		// Token: 0x060003C7 RID: 967 RVA: 0x00006683 File Offset: 0x00004883
		public static CommandBuffer Get()
		{
			CommandBuffer commandBuffer = CommandBufferPool.s_BufferPool.Get();
			commandBuffer.name = "";
			return commandBuffer;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000669A File Offset: 0x0000489A
		public static CommandBuffer Get(string name)
		{
			CommandBuffer commandBuffer = CommandBufferPool.s_BufferPool.Get();
			commandBuffer.name = name;
			return commandBuffer;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x000066AD File Offset: 0x000048AD
		public static void Release(CommandBuffer buffer)
		{
			CommandBufferPool.s_BufferPool.Release(buffer);
		}

		// Token: 0x0400009E RID: 158
		private static ObjectPool<CommandBuffer> s_BufferPool = new ObjectPool<CommandBuffer>(null, delegate(CommandBuffer x)
		{
			x.Clear();
		}, true);
	}
}
