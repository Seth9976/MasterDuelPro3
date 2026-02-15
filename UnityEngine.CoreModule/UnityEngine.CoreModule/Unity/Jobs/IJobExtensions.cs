using System;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Jobs
{
	// Token: 0x0200000A RID: 10
	public static class IJobExtensions
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000020D9 File Offset: 0x000002D9
		public static void EarlyJobInit<T>() where T : struct, IJob
		{
			IJobExtensions.JobStruct<T>.Initialize();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020E4 File Offset: 0x000002E4
		private unsafe static IntPtr GetReflectionData<T>() where T : struct, IJob
		{
			IJobExtensions.JobStruct<T>.Initialize();
			return *IJobExtensions.JobStruct<T>.jobReflectionData.Data;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000210C File Offset: 0x0000030C
		public static JobHandle Schedule<T>(this T jobData, JobHandle dependsOn = default(JobHandle)) where T : struct, IJob
		{
			JobsUtility.JobScheduleParameters scheduleParams = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobExtensions.GetReflectionData<T>(), dependsOn, ScheduleMode.Single);
			return JobsUtility.Schedule(ref scheduleParams);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000213C File Offset: 0x0000033C
		public static void Run<T>(this T jobData) where T : struct, IJob
		{
			JobsUtility.JobScheduleParameters scheduleParams = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobExtensions.GetReflectionData<T>(), default(JobHandle), ScheduleMode.Run);
			JobsUtility.Schedule(ref scheduleParams);
		}

		// Token: 0x0200000B RID: 11
		internal struct JobStruct<T> where T : struct, IJob
		{
			// Token: 0x0600000E RID: 14 RVA: 0x00002170 File Offset: 0x00000370
			[BurstDiscard]
			internal unsafe static void Initialize()
			{
				bool flag = *IJobExtensions.JobStruct<T>.jobReflectionData.Data == IntPtr.Zero;
				if (flag)
				{
					*IJobExtensions.JobStruct<T>.jobReflectionData.Data = JobsUtility.CreateJobReflectionData(typeof(T), new IJobExtensions.JobStruct<T>.ExecuteJobFunction(IJobExtensions.JobStruct<T>.Execute), null, null);
				}
			}

			// Token: 0x0600000F RID: 15 RVA: 0x000021BF File Offset: 0x000003BF
			public static void Execute(ref T data, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex)
			{
				data.Execute();
			}

			// Token: 0x0400000A RID: 10
			internal static readonly BurstLike.SharedStatic<IntPtr> jobReflectionData = BurstLike.SharedStatic<IntPtr>.GetOrCreate<IJobExtensions.JobStruct<T>>(0U);

			// Token: 0x0200000C RID: 12
			// (Invoke) Token: 0x06000012 RID: 18
			internal delegate void ExecuteJobFunction(ref T data, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);
		}
	}
}
