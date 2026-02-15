using System;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.Jobs
{
	// Token: 0x020001F5 RID: 501
	public static class IJobParallelForTransformExtensions
	{
		// Token: 0x0600139D RID: 5021 RVA: 0x00029540 File Offset: 0x00027740
		private unsafe static IntPtr GetReflectionData<T>() where T : struct, IJobParallelForTransform
		{
			IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.Initialize();
			return *IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.jobReflectionData.Data;
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x00029568 File Offset: 0x00027768
		public static JobHandle Schedule<T>(this T jobData, TransformAccessArray transforms, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParallelForTransform
		{
			JobsUtility.JobScheduleParameters scheduleParams = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobParallelForTransformExtensions.GetReflectionData<T>(), dependsOn, ScheduleMode.Batched);
			return JobsUtility.ScheduleParallelForTransform(ref scheduleParams, transforms.GetTransformAccessArrayForSchedule());
		}

		// Token: 0x020001F6 RID: 502
		internal struct TransformParallelForLoopStruct<T> where T : struct, IJobParallelForTransform
		{
			// Token: 0x0600139F RID: 5023 RVA: 0x000295A0 File Offset: 0x000277A0
			[BurstDiscard]
			internal unsafe static void Initialize()
			{
				bool flag = *IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.jobReflectionData.Data == IntPtr.Zero;
				if (flag)
				{
					*IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.jobReflectionData.Data = JobsUtility.CreateJobReflectionData(typeof(T), new IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.ExecuteJobFunction(IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.Execute), null, null);
				}
			}

			// Token: 0x060013A0 RID: 5024 RVA: 0x000295F0 File Offset: 0x000277F0
			public unsafe static void Execute(ref T jobData, IntPtr jobData2, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex)
			{
				IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.TransformJobData transformJobData;
				UnsafeUtility.CopyPtrToStructure<IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.TransformJobData>((void*)jobData2, out transformJobData);
				int* sortedToUserIndex = (int*)(void*)TransformAccessArray.GetSortedToUserIndex(transformJobData.TransformAccessArray);
				TransformAccess* sortedTransformAccess = (TransformAccess*)(void*)TransformAccessArray.GetSortedTransformAccess(transformJobData.TransformAccessArray);
				bool flag = transformJobData.IsReadOnly == 1;
				if (flag)
				{
					for (;;)
					{
						int begin;
						int end;
						bool flag2 = !JobsUtility.GetWorkStealingRange(ref ranges, jobIndex, out begin, out end);
						if (flag2)
						{
							break;
						}
						int endThatCompilerCanSeeWillNeverChange = end;
						for (int i = begin; i < endThatCompilerCanSeeWillNeverChange; i++)
						{
							int sortedIndex = i;
							int userIndex = sortedToUserIndex[sortedIndex];
							TransformAccess transformAccess = sortedTransformAccess[sortedIndex];
							jobData.Execute(userIndex, transformAccess);
						}
					}
				}
				else
				{
					int begin2;
					int end2;
					JobsUtility.GetJobRange(ref ranges, jobIndex, out begin2, out end2);
					for (int j = begin2; j < end2; j++)
					{
						int sortedIndex2 = j;
						int userIndex2 = sortedToUserIndex[sortedIndex2];
						TransformAccess transformAccess2 = sortedTransformAccess[sortedIndex2];
						jobData.Execute(userIndex2, transformAccess2);
					}
				}
			}

			// Token: 0x04000721 RID: 1825
			internal static readonly BurstLike.SharedStatic<IntPtr> jobReflectionData = BurstLike.SharedStatic<IntPtr>.GetOrCreate<IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>>(0U);

			// Token: 0x020001F7 RID: 503
			private struct TransformJobData
			{
				// Token: 0x04000722 RID: 1826
				public IntPtr TransformAccessArray;

				// Token: 0x04000723 RID: 1827
				public int IsReadOnly;
			}

			// Token: 0x020001F8 RID: 504
			// (Invoke) Token: 0x060013A3 RID: 5027
			public delegate void ExecuteJobFunction(ref T jobData, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);
		}
	}
}
