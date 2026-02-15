using System;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x02000260 RID: 608
	internal abstract class IRenderGraphResourcePool
	{
		// Token: 0x06001079 RID: 4217
		public abstract void PurgeUnusedResources(int currentFrameIndex);

		// Token: 0x0600107A RID: 4218
		public abstract void Cleanup();

		// Token: 0x0600107B RID: 4219
		public abstract void CheckFrameAllocation(bool onException, int frameIndex);

		// Token: 0x0600107C RID: 4220
		public abstract void LogResources(RenderGraphLogger logger);
	}
}
