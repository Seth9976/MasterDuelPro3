using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace Unity.Jobs
{
	// Token: 0x02000015 RID: 21
	[NativeType(Header = "Runtime/Jobs/ScriptBindings/JobsBindings.h")]
	public struct JobHandle : IEquatable<JobHandle>
	{
		// Token: 0x06000026 RID: 38 RVA: 0x0000243C File Offset: 0x0000063C
		public void Complete()
		{
			bool flag = this.jobGroup == 0UL;
			if (!flag)
			{
				JobHandle.ScheduleBatchedJobsAndComplete(ref this);
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002464 File Offset: 0x00000664
		public bool IsCompleted
		{
			get
			{
				return JobHandle.ScheduleBatchedJobsAndIsCompleted(ref this);
			}
		}

		// Token: 0x06000028 RID: 40
		[NativeMethod("ScheduleBatchedScriptingJobs", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ScheduleBatchedJobs();

		// Token: 0x06000029 RID: 41
		[NativeMethod("ScheduleBatchedScriptingJobsAndComplete", IsFreeFunction = true, IsThreadSafe = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ScheduleBatchedJobsAndComplete(ref JobHandle job);

		// Token: 0x0600002A RID: 42
		[NativeMethod("ScheduleBatchedScriptingJobsAndIsCompleted", IsFreeFunction = true, IsThreadSafe = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ScheduleBatchedJobsAndIsCompleted(ref JobHandle job);

		// Token: 0x0600002B RID: 43 RVA: 0x0000247C File Offset: 0x0000067C
		public static JobHandle CombineDependencies(JobHandle job0, JobHandle job1)
		{
			return JobHandle.CombineDependenciesInternal2(ref job0, ref job1);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002498 File Offset: 0x00000698
		public static JobHandle CombineDependencies(NativeArray<JobHandle> jobs)
		{
			return JobHandle.CombineDependenciesInternalPtr(jobs.GetUnsafeReadOnlyPtr<JobHandle>(), jobs.Length);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000024BC File Offset: 0x000006BC
		public static JobHandle CombineDependencies(NativeSlice<JobHandle> jobs)
		{
			return JobHandle.CombineDependenciesInternalPtr(jobs.GetUnsafeReadOnlyPtr<JobHandle>(), jobs.Length);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000024E0 File Offset: 0x000006E0
		[NativeMethod(IsFreeFunction = true, IsThreadSafe = true, ThrowsException = true)]
		private static JobHandle CombineDependenciesInternal2(ref JobHandle job0, ref JobHandle job1)
		{
			JobHandle jobHandle;
			JobHandle.CombineDependenciesInternal2_Injected(ref job0, ref job1, out jobHandle);
			return jobHandle;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000024F8 File Offset: 0x000006F8
		[NativeMethod(IsFreeFunction = true, IsThreadSafe = true, ThrowsException = true)]
		internal unsafe static JobHandle CombineDependenciesInternalPtr(void* jobs, int count)
		{
			JobHandle jobHandle;
			JobHandle.CombineDependenciesInternalPtr_Injected(jobs, count, out jobHandle);
			return jobHandle;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002510 File Offset: 0x00000710
		public bool Equals(JobHandle other)
		{
			return this.jobGroup == other.jobGroup;
		}

		// Token: 0x06000031 RID: 49
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CombineDependenciesInternal2_Injected(ref JobHandle job0, ref JobHandle job1, out JobHandle ret);

		// Token: 0x06000032 RID: 50
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void CombineDependenciesInternalPtr_Injected(void* jobs, int count, out JobHandle ret);

		// Token: 0x0400000D RID: 13
		internal ulong jobGroup;

		// Token: 0x0400000E RID: 14
		internal int version;
	}
}
