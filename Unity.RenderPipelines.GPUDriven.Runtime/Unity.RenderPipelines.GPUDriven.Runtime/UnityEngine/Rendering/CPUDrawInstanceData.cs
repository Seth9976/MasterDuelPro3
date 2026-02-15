using System;
using Unity.Collections;
using Unity.Jobs;

namespace UnityEngine.Rendering
{
	// Token: 0x02000055 RID: 85
	internal class CPUDrawInstanceData
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000141 RID: 321 RVA: 0x000092C3 File Offset: 0x000074C3
		public NativeList<DrawInstance> drawInstances
		{
			get
			{
				return this.m_DrawInstances;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000142 RID: 322 RVA: 0x000092CB File Offset: 0x000074CB
		public NativeParallelHashMap<DrawKey, int> batchHash
		{
			get
			{
				return this.m_BatchHash;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000143 RID: 323 RVA: 0x000092D3 File Offset: 0x000074D3
		public NativeList<DrawBatch> drawBatches
		{
			get
			{
				return this.m_DrawBatches;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000144 RID: 324 RVA: 0x000092DB File Offset: 0x000074DB
		public NativeParallelHashMap<RangeKey, int> rangeHash
		{
			get
			{
				return this.m_RangeHash;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000145 RID: 325 RVA: 0x000092E3 File Offset: 0x000074E3
		public NativeList<DrawRange> drawRanges
		{
			get
			{
				return this.m_DrawRanges;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000146 RID: 326 RVA: 0x000092EB File Offset: 0x000074EB
		public NativeArray<int> drawBatchIndices
		{
			get
			{
				return this.m_DrawBatchIndices.AsArray();
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000147 RID: 327 RVA: 0x000092F8 File Offset: 0x000074F8
		public NativeArray<int> drawInstanceIndices
		{
			get
			{
				return this.m_DrawInstanceIndices.AsArray();
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00009305 File Offset: 0x00007505
		public bool valid
		{
			get
			{
				return this.m_DrawInstances.IsCreated;
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00009314 File Offset: 0x00007514
		public void Initialize()
		{
			this.m_RangeHash = new NativeParallelHashMap<RangeKey, int>(1024, Allocator.Persistent);
			this.m_DrawRanges = new NativeList<DrawRange>(Allocator.Persistent);
			this.m_BatchHash = new NativeParallelHashMap<DrawKey, int>(1024, Allocator.Persistent);
			this.m_DrawBatches = new NativeList<DrawBatch>(Allocator.Persistent);
			this.m_DrawInstances = new NativeList<DrawInstance>(1024, Allocator.Persistent);
			this.m_DrawInstanceIndices = new NativeList<int>(1024, Allocator.Persistent);
			this.m_DrawBatchIndices = new NativeList<int>(1024, Allocator.Persistent);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000093B4 File Offset: 0x000075B4
		public void Dispose()
		{
			if (this.m_DrawBatchIndices.IsCreated)
			{
				this.m_DrawBatchIndices.Dispose();
			}
			if (this.m_DrawInstanceIndices.IsCreated)
			{
				this.m_DrawInstanceIndices.Dispose();
			}
			if (this.m_DrawInstances.IsCreated)
			{
				this.m_DrawInstances.Dispose();
			}
			if (this.m_DrawBatches.IsCreated)
			{
				this.m_DrawBatches.Dispose();
			}
			if (this.m_BatchHash.IsCreated)
			{
				this.m_BatchHash.Dispose();
			}
			if (this.m_DrawRanges.IsCreated)
			{
				this.m_DrawRanges.Dispose();
			}
			if (this.m_RangeHash.IsCreated)
			{
				this.m_RangeHash.Dispose();
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000946C File Offset: 0x0000766C
		public void RebuildDrawListsIfNeeded()
		{
			if (!this.m_NeedsRebuild)
			{
				return;
			}
			this.m_NeedsRebuild = false;
			this.m_DrawInstanceIndices.ResizeUninitialized(this.m_DrawInstances.Length);
			this.m_DrawBatchIndices.ResizeUninitialized(this.m_DrawBatches.Length);
			NativeArray<int> internalDrawIndex = new NativeArray<int>(this.drawBatches.Length * 16, Allocator.TempJob, NativeArrayOptions.ClearMemory);
			JobHandle prefixSumJobHandle = new PrefixSumDrawInstancesJob
			{
				rangeHash = this.m_RangeHash,
				drawRanges = this.m_DrawRanges,
				drawBatches = this.m_DrawBatches,
				drawBatchIndices = this.m_DrawBatchIndices.AsArray()
			}.Schedule(default(JobHandle));
			new BuildDrawListsJob
			{
				drawInstances = this.m_DrawInstances,
				batchHash = this.m_BatchHash,
				drawBatches = this.m_DrawBatches,
				internalDrawIndex = internalDrawIndex,
				drawInstanceIndices = this.m_DrawInstanceIndices.AsArray()
			}.Schedule(this.m_DrawInstances.Length, 128, prefixSumJobHandle).Complete();
			internalDrawIndex.Dispose();
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00009594 File Offset: 0x00007794
		public void DestroyDrawInstanceIndices(NativeArray<int> drawInstanceIndicesToDestroy)
		{
			drawInstanceIndicesToDestroy.ParallelSort().Complete();
			new RemoveDrawInstanceIndicesJob
			{
				drawInstanceIndices = drawInstanceIndicesToDestroy,
				drawInstances = this.m_DrawInstances,
				drawBatches = this.m_DrawBatches,
				drawRanges = this.m_DrawRanges,
				batchHash = this.m_BatchHash,
				rangeHash = this.m_RangeHash
			}.Run<RemoveDrawInstanceIndicesJob>();
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00009608 File Offset: 0x00007808
		public void DestroyDrawInstances(NativeArray<InstanceHandle> destroyedInstances)
		{
			if (this.m_DrawInstances.IsEmpty || destroyedInstances.Length == 0)
			{
				return;
			}
			this.NeedsRebuild();
			NativeArray<InstanceHandle> destroyedInstancesSorted = new NativeArray<InstanceHandle>(destroyedInstances, Allocator.TempJob);
			destroyedInstancesSorted.Reinterpret<int>().ParallelSort().Complete();
			NativeList<int> drawInstanceIndicesToDestroy = new NativeList<int>(this.m_DrawInstances.Length, Allocator.TempJob);
			new FindDrawInstancesJob
			{
				instancesSorted = destroyedInstancesSorted,
				drawInstances = this.m_DrawInstances,
				outDrawInstanceIndicesWriter = drawInstanceIndicesToDestroy.AsParallelWriter()
			}.ScheduleBatch(this.m_DrawInstances.Length, 128, default(JobHandle)).Complete();
			this.DestroyDrawInstanceIndices(drawInstanceIndicesToDestroy.AsArray());
			destroyedInstancesSorted.Dispose();
			drawInstanceIndicesToDestroy.Dispose();
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000096D4 File Offset: 0x000078D4
		public void DestroyMaterialDrawInstances(NativeArray<uint> destroyedBatchMaterials)
		{
			if (this.m_DrawInstances.IsEmpty || destroyedBatchMaterials.Length == 0)
			{
				return;
			}
			this.NeedsRebuild();
			NativeArray<uint> destroyedBatchMaterialsSorted = new NativeArray<uint>(destroyedBatchMaterials, Allocator.TempJob);
			destroyedBatchMaterialsSorted.Reinterpret<int>().ParallelSort().Complete();
			NativeList<int> drawInstanceIndicesToDestroy = new NativeList<int>(this.m_DrawInstances.Length, Allocator.TempJob);
			new FindMaterialDrawInstancesJob
			{
				materialsSorted = destroyedBatchMaterialsSorted,
				drawInstances = this.m_DrawInstances,
				outDrawInstanceIndicesWriter = drawInstanceIndicesToDestroy.AsParallelWriter()
			}.ScheduleBatch(this.m_DrawInstances.Length, 128, default(JobHandle)).Complete();
			this.DestroyDrawInstanceIndices(drawInstanceIndicesToDestroy.AsArray());
			destroyedBatchMaterialsSorted.Dispose();
			drawInstanceIndicesToDestroy.Dispose();
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000097A0 File Offset: 0x000079A0
		public void NeedsRebuild()
		{
			this.m_NeedsRebuild = true;
		}

		// Token: 0x0400017F RID: 383
		private NativeParallelHashMap<RangeKey, int> m_RangeHash;

		// Token: 0x04000180 RID: 384
		private NativeList<DrawRange> m_DrawRanges;

		// Token: 0x04000181 RID: 385
		private NativeParallelHashMap<DrawKey, int> m_BatchHash;

		// Token: 0x04000182 RID: 386
		private NativeList<DrawBatch> m_DrawBatches;

		// Token: 0x04000183 RID: 387
		private NativeList<DrawInstance> m_DrawInstances;

		// Token: 0x04000184 RID: 388
		private NativeList<int> m_DrawInstanceIndices;

		// Token: 0x04000185 RID: 389
		private NativeList<int> m_DrawBatchIndices;

		// Token: 0x04000186 RID: 390
		private bool m_NeedsRebuild;
	}
}
