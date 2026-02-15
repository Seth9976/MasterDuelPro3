using System;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Jobs
{
	// Token: 0x02000012 RID: 18
	public static class IJobParallelForExtensions
	{
		// Token: 0x0600001D RID: 29 RVA: 0x000022F0 File Offset: 0x000004F0
		public static void EarlyJobInit<T>() where T : struct, IJobParallelFor
		{
			IJobParallelForExtensions.ParallelForJobStruct<T>.Initialize();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000022FC File Offset: 0x000004FC
		private unsafe static IntPtr GetReflectionData<T>() where T : struct, IJobParallelFor
		{
			IJobParallelForExtensions.ParallelForJobStruct<T>.Initialize();
			return *IJobParallelForExtensions.ParallelForJobStruct<T>.jobReflectionData.Data;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002324 File Offset: 0x00000524
		public static JobHandle Schedule<T>(this T jobData, int arrayLength, int innerloopBatchCount, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParallelFor
		{
			JobsUtility.JobScheduleParameters scheduleParams = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobParallelForExtensions.GetReflectionData<T>(), dependsOn, ScheduleMode.Batched);
			return JobsUtility.ScheduleParallelFor(ref scheduleParams, arrayLength, innerloopBatchCount);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002354 File Offset: 0x00000554
		public static void Run<T>(this T jobData, int arrayLength) where T : struct, IJobParallelFor
		{
			JobsUtility.JobScheduleParameters scheduleParams = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobParallelForExtensions.GetReflectionData<T>(), default(JobHandle), ScheduleMode.Run);
			JobsUtility.ScheduleParallelFor(ref scheduleParams, arrayLength, arrayLength);
		}

		// Token: 0x02000013 RID: 19
		internal struct ParallelForJobStruct<T> where T : struct, IJobParallelFor
		{
			// Token: 0x06000021 RID: 33 RVA: 0x0000238C File Offset: 0x0000058C
			[BurstDiscard]
			internal unsafe static void Initialize()
			{
				bool flag = *IJobParallelForExtensions.ParallelForJobStruct<T>.jobReflectionData.Data == IntPtr.Zero;
				if (flag)
				{
					*IJobParallelForExtensions.ParallelForJobStruct<T>.jobReflectionData.Data = JobsUtility.CreateJobReflectionData(typeof(T), new IJobParallelForExtensions.ParallelForJobStruct<T>.ExecuteJobFunction(IJobParallelForExtensions.ParallelForJobStruct<T>.Execute), null, null);
				}
			}

			// Token: 0x06000022 RID: 34 RVA: 0x000023DC File Offset: 0x000005DC
			public static void Execute(ref T jobData, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex)
			{
				for (;;)
				{
					int begin;
					int end;
					bool flag = !JobsUtility.GetWorkStealingRange(ref ranges, jobIndex, out begin, out end);
					if (flag)
					{
						break;
					}
					int endThatCompilerCanSeeWillNeverChange = end;
					for (int i = begin; i < endThatCompilerCanSeeWillNeverChange; i++)
					{
						jobData.Execute(i);
					}
				}
			}

			// Token: 0x0400000C RID: 12
			internal static readonly BurstLike.SharedStatic<IntPtr> jobReflectionData = BurstLike.SharedStatic<IntPtr>.GetOrCreate<IJobParallelForExtensions.ParallelForJobStruct<T>>(0U);

			// Token: 0x02000014 RID: 20
			// (Invoke) Token: 0x06000025 RID: 37
			public delegate void ExecuteJobFunction(ref T data, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);
		}
	}
}
