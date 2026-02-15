using System;
using Unity.Collections;
using Unity.Jobs;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200052D RID: 1325
	internal class JobMerger : IDisposable
	{
		// Token: 0x060024AC RID: 9388 RVA: 0x0008BCCD File Offset: 0x00089ECD
		public JobMerger(int capacity)
		{
			Debug.Assert(capacity > 1);
			this.m_Jobs = new NativeArray<JobHandle>(capacity, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x0008BCF0 File Offset: 0x00089EF0
		public void Add(JobHandle job)
		{
			bool flag = this.m_JobCount < this.m_Jobs.Length;
			if (flag)
			{
				int jobCount = this.m_JobCount;
				this.m_JobCount = jobCount + 1;
				this.m_Jobs[jobCount] = job;
			}
			else
			{
				this.m_Jobs[0] = JobHandle.CombineDependencies(this.m_Jobs);
				this.m_Jobs[1] = job;
				this.m_JobCount = 2;
			}
		}

		// Token: 0x060024AE RID: 9390 RVA: 0x0008BD64 File Offset: 0x00089F64
		public JobHandle MergeAndReset()
		{
			JobHandle mergedJob = default(JobHandle);
			bool flag = this.m_JobCount > 1;
			if (flag)
			{
				mergedJob = JobHandle.CombineDependencies(this.m_Jobs.Slice(0, this.m_JobCount));
			}
			else
			{
				bool flag2 = this.m_JobCount == 1;
				if (flag2)
				{
					mergedJob = this.m_Jobs[0];
				}
			}
			this.m_JobCount = 0;
			return mergedJob;
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x060024AF RID: 9391 RVA: 0x0008BDC7 File Offset: 0x00089FC7
		// (set) Token: 0x060024B0 RID: 9392 RVA: 0x0008BDCF File Offset: 0x00089FCF
		private protected bool disposed { protected get; private set; }

		// Token: 0x060024B1 RID: 9393 RVA: 0x0008BDD8 File Offset: 0x00089FD8
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x0008BDEC File Offset: 0x00089FEC
		protected void Dispose(bool disposing)
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

		// Token: 0x040011AF RID: 4527
		private NativeArray<JobHandle> m_Jobs;

		// Token: 0x040011B0 RID: 4528
		private int m_JobCount;
	}
}
