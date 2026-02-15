using System;
using Unity.Collections;
using Unity.Jobs;

namespace UnityEngine.Rendering
{
	// Token: 0x02000040 RID: 64
	internal struct InstanceCullerSplitDebugArray : IDisposable
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00006FBC File Offset: 0x000051BC
		public NativeArray<int> Counters
		{
			get
			{
				return this.m_Counters;
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00006FC4 File Offset: 0x000051C4
		public void Init()
		{
			this.m_Info = new NativeList<InstanceCullerSplitDebugArray.Info>(Allocator.Persistent);
			this.m_Counters = new NativeArray<int>(128, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			this.m_CounterSync = new NativeQueue<JobHandle>(Allocator.Persistent);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00006FFA File Offset: 0x000051FA
		public void Dispose()
		{
			this.m_Info.Dispose();
			this.m_Counters.Dispose();
			this.m_CounterSync.Dispose();
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00007020 File Offset: 0x00005220
		public int TryAddSplits(BatchCullingViewType viewType, int viewInstanceID, int splitCount)
		{
			int baseIndex = this.m_Info.Length;
			if (baseIndex + splitCount > 64)
			{
				return -1;
			}
			for (int splitIndex = 0; splitIndex < splitCount; splitIndex++)
			{
				InstanceCullerSplitDebugArray.Info info = default(InstanceCullerSplitDebugArray.Info);
				info.viewType = viewType;
				info.viewInstanceID = viewInstanceID;
				info.splitIndex = splitIndex;
				this.m_Info.Add(in info);
			}
			return baseIndex;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000707C File Offset: 0x0000527C
		public void AddSync(int baseIndex, JobHandle jobHandle)
		{
			if (baseIndex != -1)
			{
				this.m_CounterSync.Enqueue(jobHandle);
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00007090 File Offset: 0x00005290
		public void MoveToDebugStatsAndClear(DebugRendererBatcherStats debugStats)
		{
			JobHandle jobHandle;
			while (this.m_CounterSync.TryDequeue(out jobHandle))
			{
				jobHandle.Complete();
			}
			debugStats.instanceCullerStats.Clear();
			for (int index = 0; index < this.m_Info.Length; index++)
			{
				InstanceCullerSplitDebugArray.Info info = this.m_Info[index];
				int counterBase = index * 2;
				InstanceCullerViewStats instanceCullerViewStats = default(InstanceCullerViewStats);
				instanceCullerViewStats.viewType = info.viewType;
				instanceCullerViewStats.viewInstanceID = info.viewInstanceID;
				instanceCullerViewStats.splitIndex = info.splitIndex;
				instanceCullerViewStats.visibleInstances = this.m_Counters[counterBase];
				instanceCullerViewStats.drawCommands = this.m_Counters[counterBase + 1];
				debugStats.instanceCullerStats.Add(in instanceCullerViewStats);
			}
			this.m_Info.Clear();
			int num = 0;
			(ref this.m_Counters).FillArray(in num, 0, -1);
		}

		// Token: 0x04000117 RID: 279
		private const int MaxSplitCount = 64;

		// Token: 0x04000118 RID: 280
		private NativeList<InstanceCullerSplitDebugArray.Info> m_Info;

		// Token: 0x04000119 RID: 281
		private NativeArray<int> m_Counters;

		// Token: 0x0400011A RID: 282
		private NativeQueue<JobHandle> m_CounterSync;

		// Token: 0x02000041 RID: 65
		internal struct Info
		{
			// Token: 0x0400011B RID: 283
			public BatchCullingViewType viewType;

			// Token: 0x0400011C RID: 284
			public int viewInstanceID;

			// Token: 0x0400011D RID: 285
			public int splitIndex;
		}
	}
}
