using System;
using Unity.Collections;

namespace UnityEngine.Rendering
{
	// Token: 0x02000025 RID: 37
	internal class DebugRendererBatcherStats : IDisposable
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x00005788 File Offset: 0x00003988
		public DebugRendererBatcherStats()
		{
			this.instanceCullerStats = new NativeList<InstanceCullerViewStats>(Allocator.Persistent);
			this.instanceOcclusionEventStats = new NativeList<InstanceOcclusionEventStats>(Allocator.Persistent);
			this.occluderStats = new NativeList<DebugOccluderStats>(Allocator.Persistent);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000057C4 File Offset: 0x000039C4
		public void Dispose()
		{
			if (this.instanceCullerStats.IsCreated)
			{
				this.instanceCullerStats.Dispose();
			}
			if (this.instanceOcclusionEventStats.IsCreated)
			{
				this.instanceOcclusionEventStats.Dispose();
			}
			if (this.occluderStats.IsCreated)
			{
				this.occluderStats.Dispose();
			}
		}

		// Token: 0x04000071 RID: 113
		public bool enabled;

		// Token: 0x04000072 RID: 114
		public NativeList<InstanceCullerViewStats> instanceCullerStats;

		// Token: 0x04000073 RID: 115
		public NativeList<InstanceOcclusionEventStats> instanceOcclusionEventStats;

		// Token: 0x04000074 RID: 116
		public NativeList<DebugOccluderStats> occluderStats;

		// Token: 0x04000075 RID: 117
		public bool occlusionOverlayEnabled;

		// Token: 0x04000076 RID: 118
		public bool occlusionOverlayCountVisible;

		// Token: 0x04000077 RID: 119
		public bool overrideOcclusionTestToAlwaysPass;
	}
}
