using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000529 RID: 1321
	internal class JobManager : IDisposable
	{
		// Token: 0x060024A1 RID: 9377 RVA: 0x0008BA01 File Offset: 0x00089C01
		public void Add(ref NudgeJobData job)
		{
			this.m_NudgeJobs.Add(ref job);
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x0008BA11 File Offset: 0x00089C11
		public void Add(ref ConvertMeshJobData job)
		{
			this.m_ConvertMeshJobs.Add(ref job);
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x0008BA21 File Offset: 0x00089C21
		public void Add(ref CopyMeshJobData job)
		{
			this.m_CopyMeshJobs.Add(ref job);
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x0008BA34 File Offset: 0x00089C34
		public void CompleteNudgeJobs()
		{
			foreach (NativeSlice<NudgeJobData> page in this.m_NudgeJobs.GetPages())
			{
				this.m_JobMerger.Add(JobProcessor.ScheduleNudgeJobs((IntPtr)page.GetUnsafePtr<NudgeJobData>(), page.Length));
			}
			this.m_JobMerger.MergeAndReset().Complete();
			this.m_NudgeJobs.Reset();
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x0008BACC File Offset: 0x00089CCC
		public void CompleteConvertMeshJobs()
		{
			foreach (NativeSlice<ConvertMeshJobData> page in this.m_ConvertMeshJobs.GetPages())
			{
				this.m_JobMerger.Add(JobProcessor.ScheduleConvertMeshJobs((IntPtr)page.GetUnsafePtr<ConvertMeshJobData>(), page.Length));
			}
			this.m_JobMerger.MergeAndReset().Complete();
			this.m_ConvertMeshJobs.Reset();
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x0008BB64 File Offset: 0x00089D64
		public void CompleteCopyMeshJobs()
		{
			foreach (NativeSlice<CopyMeshJobData> page in this.m_CopyMeshJobs.GetPages())
			{
				this.m_JobMerger.Add(JobProcessor.ScheduleCopyMeshJobs((IntPtr)page.GetUnsafePtr<CopyMeshJobData>(), page.Length));
			}
			this.m_JobMerger.MergeAndReset().Complete();
			this.m_CopyMeshJobs.Reset();
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x060024A7 RID: 9383 RVA: 0x0008BBFC File Offset: 0x00089DFC
		// (set) Token: 0x060024A8 RID: 9384 RVA: 0x0008BC04 File Offset: 0x00089E04
		private protected bool disposed { protected get; private set; }

		// Token: 0x060024A9 RID: 9385 RVA: 0x0008BC0D File Offset: 0x00089E0D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x0008BC20 File Offset: 0x00089E20
		protected void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.m_NudgeJobs.Dispose();
					this.m_ConvertMeshJobs.Dispose();
					this.m_CopyMeshJobs.Dispose();
					this.m_JobMerger.Dispose();
				}
				this.disposed = true;
			}
		}

		// Token: 0x04001187 RID: 4487
		private NativePagedList<NudgeJobData> m_NudgeJobs = new NativePagedList<NudgeJobData>(64, Allocator.Persistent, Allocator.Persistent);

		// Token: 0x04001188 RID: 4488
		private NativePagedList<ConvertMeshJobData> m_ConvertMeshJobs = new NativePagedList<ConvertMeshJobData>(64, Allocator.Persistent, Allocator.Persistent);

		// Token: 0x04001189 RID: 4489
		private NativePagedList<CopyMeshJobData> m_CopyMeshJobs = new NativePagedList<CopyMeshJobData>(64, Allocator.Persistent, Allocator.Persistent);

		// Token: 0x0400118A RID: 4490
		private JobMerger m_JobMerger = new JobMerger(128);
	}
}
