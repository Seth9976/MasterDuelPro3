using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.Rendering
{
	// Token: 0x02000042 RID: 66
	internal struct InstanceOcclusionEventDebugArray : IDisposable
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600010F RID: 271 RVA: 0x0000716B File Offset: 0x0000536B
		public GraphicsBuffer CounterBuffer
		{
			get
			{
				return this.m_CounterBuffer;
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00007173 File Offset: 0x00005373
		public void Init()
		{
			this.m_CounterBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured, 128, 4);
			this.m_PendingInfo = new UnsafeList<InstanceOcclusionEventDebugArray.Info>(4, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			this.m_Requests = new NativeQueue<InstanceOcclusionEventDebugArray.Request>(Allocator.Persistent);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000071AC File Offset: 0x000053AC
		public void Dispose()
		{
			if (this.m_HasLatest)
			{
				this.m_LatestInfo.Dispose();
				this.m_LatestCounters.Dispose();
				this.m_HasLatest = false;
			}
			InstanceOcclusionEventDebugArray.Request req;
			while (this.m_Requests.TryDequeue(out req))
			{
				req.readback.WaitForCompletion();
				req.info.Dispose();
			}
			this.m_Requests.Dispose();
			this.m_PendingInfo.Dispose();
			this.m_CounterBuffer.Dispose();
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00007228 File Offset: 0x00005428
		public int TryAdd(int viewInstanceID, InstanceOcclusionEventType eventType, int occluderVersion, int subviewMask, OcclusionTest occlusionTest)
		{
			int passIndex = this.m_PendingInfo.Length;
			if (passIndex + 1 > 64)
			{
				return -1;
			}
			InstanceOcclusionEventDebugArray.Info info = default(InstanceOcclusionEventDebugArray.Info);
			info.viewInstanceID = viewInstanceID;
			info.eventType = eventType;
			info.occluderVersion = occluderVersion;
			info.subviewMask = subviewMask;
			info.occlusionTest = occlusionTest;
			this.m_PendingInfo.Add(in info);
			return passIndex;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000728C File Offset: 0x0000548C
		public void MoveToDebugStatsAndClear(DebugRendererBatcherStats debugStats)
		{
			if (this.m_PendingInfo.Length > 0)
			{
				InstanceOcclusionEventDebugArray.Request request = new InstanceOcclusionEventDebugArray.Request
				{
					info = this.m_PendingInfo,
					readback = AsyncGPUReadback.Request(this.m_CounterBuffer, this.m_PendingInfo.Length * 2 * 4, 0, null)
				};
				this.m_Requests.Enqueue(request);
				this.m_PendingInfo = new UnsafeList<InstanceOcclusionEventDebugArray.Info>(4, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			}
			while (!this.m_Requests.IsEmpty())
			{
				InstanceOcclusionEventDebugArray.Request request = this.m_Requests.Peek();
				if (!request.readback.done)
				{
					break;
				}
				InstanceOcclusionEventDebugArray.Request req = this.m_Requests.Dequeue();
				if (!req.readback.hasError)
				{
					NativeArray<int> src = req.readback.GetData<int>(0);
					if (src.Length == req.info.Length * 2)
					{
						if (this.m_HasLatest)
						{
							this.m_LatestInfo.Dispose();
							this.m_LatestCounters.Dispose();
							this.m_HasLatest = false;
						}
						this.m_LatestInfo = req.info;
						this.m_LatestCounters = new NativeArray<int>(src, Allocator.Persistent);
						this.m_HasLatest = true;
					}
				}
			}
			debugStats.instanceOcclusionEventStats.Clear();
			if (this.m_HasLatest)
			{
				for (int index = 0; index < this.m_LatestInfo.Length; index++)
				{
					InstanceOcclusionEventDebugArray.Info info = this.m_LatestInfo[index];
					int occluderVersion = -1;
					if (info.HasVersion())
					{
						occluderVersion = 0;
						for (int prevIndex = 0; prevIndex < index; prevIndex++)
						{
							InstanceOcclusionEventDebugArray.Info prevInfo = this.m_LatestInfo[prevIndex];
							if (prevInfo.HasVersion() && prevInfo.viewInstanceID == info.viewInstanceID)
							{
								occluderVersion = info.occluderVersion - prevInfo.occluderVersion;
								break;
							}
						}
					}
					int counterBase = index * 2;
					int occludedCounter = this.m_LatestCounters[counterBase];
					int notOccludedCounter = this.m_LatestCounters[counterBase + 1];
					InstanceOcclusionEventStats instanceOcclusionEventStats = default(InstanceOcclusionEventStats);
					instanceOcclusionEventStats.viewInstanceID = info.viewInstanceID;
					instanceOcclusionEventStats.eventType = info.eventType;
					instanceOcclusionEventStats.occluderVersion = occluderVersion;
					instanceOcclusionEventStats.subviewMask = info.subviewMask;
					instanceOcclusionEventStats.occlusionTest = info.occlusionTest;
					instanceOcclusionEventStats.visibleInstances = notOccludedCounter;
					instanceOcclusionEventStats.culledInstances = occludedCounter;
					debugStats.instanceOcclusionEventStats.Add(in instanceOcclusionEventStats);
				}
			}
			NativeArray<int> zeros = new NativeArray<int>(128, Allocator.Temp, NativeArrayOptions.ClearMemory);
			this.m_CounterBuffer.SetData<int>(zeros);
			zeros.Dispose();
		}

		// Token: 0x0400011E RID: 286
		private const int InitialPassCount = 4;

		// Token: 0x0400011F RID: 287
		private const int MaxPassCount = 64;

		// Token: 0x04000120 RID: 288
		private GraphicsBuffer m_CounterBuffer;

		// Token: 0x04000121 RID: 289
		private UnsafeList<InstanceOcclusionEventDebugArray.Info> m_PendingInfo;

		// Token: 0x04000122 RID: 290
		private NativeQueue<InstanceOcclusionEventDebugArray.Request> m_Requests;

		// Token: 0x04000123 RID: 291
		private UnsafeList<InstanceOcclusionEventDebugArray.Info> m_LatestInfo;

		// Token: 0x04000124 RID: 292
		private NativeArray<int> m_LatestCounters;

		// Token: 0x04000125 RID: 293
		private bool m_HasLatest;

		// Token: 0x02000043 RID: 67
		internal struct Info
		{
			// Token: 0x06000114 RID: 276 RVA: 0x00007503 File Offset: 0x00005703
			public bool HasVersion()
			{
				return this.eventType == InstanceOcclusionEventType.OccluderUpdate || this.occlusionTest > OcclusionTest.None;
			}

			// Token: 0x04000126 RID: 294
			public int viewInstanceID;

			// Token: 0x04000127 RID: 295
			public InstanceOcclusionEventType eventType;

			// Token: 0x04000128 RID: 296
			public int occluderVersion;

			// Token: 0x04000129 RID: 297
			public int subviewMask;

			// Token: 0x0400012A RID: 298
			public OcclusionTest occlusionTest;
		}

		// Token: 0x02000044 RID: 68
		internal struct Request
		{
			// Token: 0x0400012B RID: 299
			public UnsafeList<InstanceOcclusionEventDebugArray.Info> info;

			// Token: 0x0400012C RID: 300
			public AsyncGPUReadbackRequest readback;
		}
	}
}
