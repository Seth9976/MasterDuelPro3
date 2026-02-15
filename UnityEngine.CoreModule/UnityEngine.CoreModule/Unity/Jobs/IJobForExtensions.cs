using System;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Jobs
{
	// Token: 0x0200000E RID: 14
	public static class IJobForExtensions
	{
		// Token: 0x06000014 RID: 20 RVA: 0x000021DC File Offset: 0x000003DC
		public static void EarlyJobInit<T>() where T : struct, IJobFor
		{
			IJobForExtensions.ForJobStruct<T>.Initialize();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000021E8 File Offset: 0x000003E8
		private unsafe static IntPtr GetReflectionData<T>() where T : struct, IJobFor
		{
			IJobForExtensions.ForJobStruct<T>.Initialize();
			return *IJobForExtensions.ForJobStruct<T>.jobReflectionData.Data;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002210 File Offset: 0x00000410
		public static JobHandle ScheduleParallel<T>(this T jobData, int arrayLength, int innerloopBatchCount, JobHandle dependency) where T : struct, IJobFor
		{
			JobsUtility.JobScheduleParameters scheduleParams = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobForExtensions.GetReflectionData<T>(), dependency, ScheduleMode.Batched);
			return JobsUtility.ScheduleParallelFor(ref scheduleParams, arrayLength, innerloopBatchCount);
		}

		// Token: 0x0200000F RID: 15
		internal struct ForJobStruct<T> where T : struct, IJobFor
		{
			// Token: 0x06000017 RID: 23 RVA: 0x00002240 File Offset: 0x00000440
			[BurstDiscard]
			internal unsafe static void Initialize()
			{
				bool flag = *IJobForExtensions.ForJobStruct<T>.jobReflectionData.Data == IntPtr.Zero;
				if (flag)
				{
					*IJobForExtensions.ForJobStruct<T>.jobReflectionData.Data = JobsUtility.CreateJobReflectionData(typeof(T), new IJobForExtensions.ForJobStruct<T>.ExecuteJobFunction(IJobForExtensions.ForJobStruct<T>.Execute), null, null);
				}
			}

			// Token: 0x06000018 RID: 24 RVA: 0x00002290 File Offset: 0x00000490
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

			// Token: 0x0400000B RID: 11
			internal static readonly BurstLike.SharedStatic<IntPtr> jobReflectionData = BurstLike.SharedStatic<IntPtr>.GetOrCreate<IJobForExtensions.ForJobStruct<T>>(0U);

			// Token: 0x02000010 RID: 16
			// (Invoke) Token: 0x0600001B RID: 27
			public delegate void ExecuteJobFunction(ref T data, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);
		}
	}
}
