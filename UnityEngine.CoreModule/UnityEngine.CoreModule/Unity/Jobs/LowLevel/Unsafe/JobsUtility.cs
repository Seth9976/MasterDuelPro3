using System;
using System.Runtime.CompilerServices;
using Unity.Burst;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Jobs.LowLevel.Unsafe
{
	// Token: 0x02000019 RID: 25
	[NativeHeader("Runtime/Jobs/JobSystem.h")]
	[NativeType(Header = "Runtime/Jobs/ScriptBindings/JobsBindings.h")]
	public static class JobsUtility
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00002544 File Offset: 0x00000744
		public unsafe static void GetJobRange(ref JobRanges ranges, int jobIndex, out int beginIndex, out int endIndex)
		{
			int* startEndIndices = (int*)(void*)ranges.StartEndIndex;
			beginIndex = startEndIndices[jobIndex * 2];
			endIndex = startEndIndices[jobIndex * 2 + 1];
		}

		// Token: 0x06000035 RID: 53
		[NativeMethod(IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetWorkStealingRange(ref JobRanges ranges, int jobIndex, out int beginIndex, out int endIndex);

		// Token: 0x06000036 RID: 54 RVA: 0x00002578 File Offset: 0x00000778
		[FreeFunction("ScheduleManagedJob", ThrowsException = true, IsThreadSafe = true)]
		public static JobHandle Schedule(ref JobsUtility.JobScheduleParameters parameters)
		{
			JobHandle jobHandle;
			JobsUtility.Schedule_Injected(ref parameters, out jobHandle);
			return jobHandle;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002590 File Offset: 0x00000790
		[FreeFunction("ScheduleManagedJobParallelFor", ThrowsException = true, IsThreadSafe = true)]
		public static JobHandle ScheduleParallelFor(ref JobsUtility.JobScheduleParameters parameters, int arrayLength, int innerloopBatchCount)
		{
			JobHandle jobHandle;
			JobsUtility.ScheduleParallelFor_Injected(ref parameters, arrayLength, innerloopBatchCount, out jobHandle);
			return jobHandle;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000025A8 File Offset: 0x000007A8
		[FreeFunction("ScheduleManagedJobParallelForDeferArraySize", ThrowsException = true, IsThreadSafe = true)]
		public unsafe static JobHandle ScheduleParallelForDeferArraySize(ref JobsUtility.JobScheduleParameters parameters, int innerloopBatchCount, void* listData, void* listDataAtomicSafetyHandle)
		{
			JobHandle jobHandle;
			JobsUtility.ScheduleParallelForDeferArraySize_Injected(ref parameters, innerloopBatchCount, listData, listDataAtomicSafetyHandle, out jobHandle);
			return jobHandle;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000025C4 File Offset: 0x000007C4
		[FreeFunction("ScheduleManagedJobParallelForTransform", ThrowsException = true)]
		public static JobHandle ScheduleParallelForTransform(ref JobsUtility.JobScheduleParameters parameters, IntPtr transfromAccesssArray)
		{
			JobHandle jobHandle;
			JobsUtility.ScheduleParallelForTransform_Injected(ref parameters, transfromAccesssArray, out jobHandle);
			return jobHandle;
		}

		// Token: 0x0600003A RID: 58
		[FreeFunction(ThrowsException = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreateJobReflectionData(Type wrapperJobType, Type userJobType, object managedJobFunction0, object managedJobFunction1, object managedJobFunction2);

		// Token: 0x0600003B RID: 59 RVA: 0x000025DC File Offset: 0x000007DC
		public static IntPtr CreateJobReflectionData(Type type, object managedJobFunction0, object managedJobFunction1 = null, object managedJobFunction2 = null)
		{
			return JobsUtility.CreateJobReflectionData(type, type, managedJobFunction0, managedJobFunction1, managedJobFunction2);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000025F8 File Offset: 0x000007F8
		public static IntPtr CreateJobReflectionData(Type wrapperJobType, Type userJobType, object managedJobFunction0)
		{
			return JobsUtility.CreateJobReflectionData(wrapperJobType, userJobType, managedJobFunction0, null, null);
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600003D RID: 61
		public static extern bool IsExecutingJob
		{
			[NativeMethod(IsFreeFunction = true, IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000003 RID: 3
		// (set) Token: 0x0600003E RID: 62
		public static extern bool JobCompilerEnabled
		{
			[FreeFunction]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x0600003F RID: 63
		[FreeFunction("JobSystem::GetJobQueueWorkerThreadCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetJobQueueWorkerThreadCount();

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002614 File Offset: 0x00000814
		public static int JobWorkerCount
		{
			get
			{
				return JobsUtility.GetJobQueueWorkerThreadCount();
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000041 RID: 65
		public static extern int ThreadIndex
		{
			[FreeFunction("GetJobWorkerIndex", IsThreadSafe = true)]
			[BurstAuthorizedExternalMethod]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000042 RID: 66
		public static extern int ThreadIndexCount
		{
			[BurstAuthorizedExternalMethod]
			[FreeFunction("GetJobWorkerIndexCount", IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000262C File Offset: 0x0000082C
		[RequiredByNativeCode]
		private static void InvokePanicFunction()
		{
			JobsUtility.PanicFunction_ func = JobsUtility.PanicFunction;
			bool flag = func == null;
			if (!flag)
			{
				func();
			}
		}

		// Token: 0x06000044 RID: 68
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Schedule_Injected(ref JobsUtility.JobScheduleParameters parameters, out JobHandle ret);

		// Token: 0x06000045 RID: 69
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ScheduleParallelFor_Injected(ref JobsUtility.JobScheduleParameters parameters, int arrayLength, int innerloopBatchCount, out JobHandle ret);

		// Token: 0x06000046 RID: 70
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ScheduleParallelForDeferArraySize_Injected(ref JobsUtility.JobScheduleParameters parameters, int innerloopBatchCount, void* listData, void* listDataAtomicSafetyHandle, out JobHandle ret);

		// Token: 0x06000047 RID: 71
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ScheduleParallelForTransform_Injected(ref JobsUtility.JobScheduleParameters parameters, IntPtr transfromAccesssArray, out JobHandle ret);

		// Token: 0x04000019 RID: 25
		internal static JobsUtility.PanicFunction_ PanicFunction;

		// Token: 0x0200001A RID: 26
		public struct JobScheduleParameters
		{
			// Token: 0x06000048 RID: 72 RVA: 0x00002651 File Offset: 0x00000851
			public unsafe JobScheduleParameters(void* i_jobData, IntPtr i_reflectionData, JobHandle i_dependency, ScheduleMode i_scheduleMode)
			{
				this.Dependency = i_dependency;
				this.JobDataPtr = (IntPtr)i_jobData;
				this.ReflectionData = i_reflectionData;
				this.ScheduleMode = (int)i_scheduleMode;
			}

			// Token: 0x0400001A RID: 26
			public JobHandle Dependency;

			// Token: 0x0400001B RID: 27
			public int ScheduleMode;

			// Token: 0x0400001C RID: 28
			public IntPtr ReflectionData;

			// Token: 0x0400001D RID: 29
			public IntPtr JobDataPtr;
		}

		// Token: 0x0200001B RID: 27
		// (Invoke) Token: 0x0600004A RID: 74
		internal delegate void PanicFunction_();
	}
}
