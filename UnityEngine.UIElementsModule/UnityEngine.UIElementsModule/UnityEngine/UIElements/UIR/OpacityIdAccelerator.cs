using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000540 RID: 1344
	internal class OpacityIdAccelerator : IDisposable
	{
		// Token: 0x06002517 RID: 9495 RVA: 0x00090C30 File Offset: 0x0008EE30
		public void CreateJob(NativeSlice<Vertex> oldVerts, NativeSlice<Vertex> newVerts, Color32 opacityData, int vertexCount)
		{
			JobHandle jobHandle = new OpacityIdAccelerator.OpacityIdUpdateJob
			{
				oldVerts = oldVerts,
				newVerts = newVerts,
				opacityData = opacityData
			}.Schedule(vertexCount, 128, default(JobHandle));
			bool flag = this.m_NextJobIndex == this.m_Jobs.Length;
			if (flag)
			{
				this.m_Jobs[0] = JobHandle.CombineDependencies(this.m_Jobs);
				this.m_NextJobIndex = 1;
				JobHandle.ScheduleBatchedJobs();
			}
			int nextJobIndex = this.m_NextJobIndex;
			this.m_NextJobIndex = nextJobIndex + 1;
			this.m_Jobs[nextJobIndex] = jobHandle;
		}

		// Token: 0x06002518 RID: 9496 RVA: 0x00090CD4 File Offset: 0x0008EED4
		public void CompleteJobs()
		{
			bool flag = this.m_NextJobIndex > 0;
			if (flag)
			{
				bool flag2 = this.m_NextJobIndex > 1;
				if (flag2)
				{
					JobHandle.CombineDependencies(this.m_Jobs.Slice(0, this.m_NextJobIndex)).Complete();
				}
				else
				{
					this.m_Jobs[0].Complete();
				}
			}
			this.m_NextJobIndex = 0;
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06002519 RID: 9497 RVA: 0x00090D3C File Offset: 0x0008EF3C
		// (set) Token: 0x0600251A RID: 9498 RVA: 0x00090D44 File Offset: 0x0008EF44
		private protected bool disposed { protected get; private set; }

		// Token: 0x0600251B RID: 9499 RVA: 0x00090D4D File Offset: 0x0008EF4D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x00090D60 File Offset: 0x0008EF60
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.m_Jobs.Dispose();
				}
				this.disposed = true;
			}
		}

		// Token: 0x04001226 RID: 4646
		private NativeArray<JobHandle> m_Jobs = new NativeArray<JobHandle>(256, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);

		// Token: 0x04001227 RID: 4647
		private int m_NextJobIndex;

		// Token: 0x02000541 RID: 1345
		private struct OpacityIdUpdateJob : IJobParallelFor
		{
			// Token: 0x0600251E RID: 9502 RVA: 0x00090DB0 File Offset: 0x0008EFB0
			public void Execute(int i)
			{
				Vertex vert = this.oldVerts[i];
				vert.opacityColorPages.r = this.opacityData.r;
				vert.opacityColorPages.g = this.opacityData.g;
				vert.ids.b = this.opacityData.b;
				this.newVerts[i] = vert;
			}

			// Token: 0x04001229 RID: 4649
			[NativeDisableContainerSafetyRestriction]
			public NativeSlice<Vertex> oldVerts;

			// Token: 0x0400122A RID: 4650
			[NativeDisableContainerSafetyRestriction]
			public NativeSlice<Vertex> newVerts;

			// Token: 0x0400122B RID: 4651
			public Color32 opacityData;
		}
	}
}
