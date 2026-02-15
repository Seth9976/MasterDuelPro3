using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Mathematics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000D2 RID: 210
	internal static class ParallelSortExtensions
	{
		// Token: 0x06000333 RID: 819 RVA: 0x00013F64 File Offset: 0x00012164
		internal static JobHandle ParallelSort(this NativeArray<int> array)
		{
			if (array.Length <= 1)
			{
				return default(JobHandle);
			}
			JobHandle jobHandle = default(JobHandle);
			if (array.Length >= 2048)
			{
				int workersCount = Mathf.Max(JobsUtility.JobWorkerCount + 1, 1);
				int batchSize = Mathf.Max(256, Mathf.CeilToInt((float)array.Length / (float)workersCount));
				int jobsCount = Mathf.CeilToInt((float)array.Length / (float)batchSize);
				NativeArray<int> supportArray = new NativeArray<int>(array.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
				NativeArray<int> counter = new NativeArray<int>(1, Allocator.TempJob, NativeArrayOptions.ClearMemory);
				NativeArray<int> buckets = new NativeArray<int>(jobsCount * 256, Allocator.TempJob, NativeArrayOptions.ClearMemory);
				NativeArray<int> indices = new NativeArray<int>(jobsCount * 256, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
				NativeArray<int> indicesSum = new NativeArray<int>(16, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
				NativeArray<int> arraySource = array;
				NativeArray<int> arrayDest = supportArray;
				for (int radix = 0; radix < 4; radix++)
				{
					ParallelSortExtensions.RadixSortBucketCountJob bucketCountJobData = new ParallelSortExtensions.RadixSortBucketCountJob
					{
						radix = radix,
						jobsCount = jobsCount,
						batchSize = batchSize,
						buckets = buckets,
						array = arraySource
					};
					ParallelSortExtensions.RadixSortBatchPrefixSumJob batchPrefixSumJobData = new ParallelSortExtensions.RadixSortBatchPrefixSumJob
					{
						radix = radix,
						jobsCount = jobsCount,
						array = arraySource,
						counter = counter,
						buckets = buckets,
						indices = indices,
						indicesSum = indicesSum
					};
					ParallelSortExtensions.RadixSortPrefixSumJob prefixSumJobData = new ParallelSortExtensions.RadixSortPrefixSumJob
					{
						jobsCount = jobsCount,
						indices = indices,
						indicesSum = indicesSum
					};
					ParallelSortExtensions.RadixSortBucketSortJob radixSortBucketSortJob = new ParallelSortExtensions.RadixSortBucketSortJob
					{
						radix = radix,
						batchSize = batchSize,
						indices = indices,
						array = arraySource,
						arraySorted = arrayDest
					};
					jobHandle = bucketCountJobData.ScheduleParallel(jobsCount, 1, jobHandle);
					jobHandle = batchPrefixSumJobData.ScheduleParallel(16, 1, jobHandle);
					jobHandle = prefixSumJobData.ScheduleParallel(16, 1, jobHandle);
					jobHandle = radixSortBucketSortJob.ScheduleParallel(jobsCount, 1, jobHandle);
					JobHandle.ScheduleBatchedJobs();
					ParallelSortExtensions.<ParallelSort>g__Swap|2_0(ref arraySource, ref arrayDest);
				}
				supportArray.Dispose();
				counter.Dispose();
				buckets.Dispose();
				indices.Dispose();
				indicesSum.Dispose();
			}
			else
			{
				jobHandle = array.SortJob<int>().Schedule(default(JobHandle));
			}
			return jobHandle;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00014198 File Offset: 0x00012398
		[CompilerGenerated]
		internal static void <ParallelSort>g__Swap|2_0(ref NativeArray<int> a, ref NativeArray<int> b)
		{
			NativeArray<int> temp = a;
			a = b;
			b = temp;
		}

		// Token: 0x0400042E RID: 1070
		private const int kMinRadixSortArraySize = 2048;

		// Token: 0x0400042F RID: 1071
		private const int kMinRadixSortBatchSize = 256;

		// Token: 0x020000D3 RID: 211
		[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
		internal struct RadixSortBucketCountJob : IJobFor
		{
			// Token: 0x06000335 RID: 821 RVA: 0x000141C0 File Offset: 0x000123C0
			public void Execute(int index)
			{
				int num = index * this.batchSize;
				int end = math.min(num + this.batchSize, this.array.Length);
				int jobBuckets = index * 256;
				for (int i = num; i < end; i++)
				{
					int bucket = (this.array[i] >> this.radix * 8) & 255;
					ref NativeArray<int> ptr = ref this.buckets;
					int num2 = jobBuckets + bucket;
					ptr[num2]++;
				}
			}

			// Token: 0x04000430 RID: 1072
			[ReadOnly]
			public int radix;

			// Token: 0x04000431 RID: 1073
			[ReadOnly]
			public int jobsCount;

			// Token: 0x04000432 RID: 1074
			[ReadOnly]
			public int batchSize;

			// Token: 0x04000433 RID: 1075
			[ReadOnly]
			[NativeDisableContainerSafetyRestriction]
			[NoAlias]
			public NativeArray<int> array;

			// Token: 0x04000434 RID: 1076
			[NativeDisableContainerSafetyRestriction]
			[NoAlias]
			public NativeArray<int> buckets;
		}

		// Token: 0x020000D4 RID: 212
		[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
		internal struct RadixSortBatchPrefixSumJob : IJobFor
		{
			// Token: 0x06000336 RID: 822 RVA: 0x00014241 File Offset: 0x00012441
			private static int AtomicIncrement(NativeArray<int> counter)
			{
				return Interlocked.Increment(UnsafeUtility.AsRef<int>(counter.GetUnsafePtr<int>()));
			}

			// Token: 0x06000337 RID: 823 RVA: 0x00014254 File Offset: 0x00012454
			private int JobIndexPrefixSum(int sum, int i)
			{
				for (int j = 0; j < this.jobsCount; j++)
				{
					int k = i + j * 256;
					this.indices[k] = sum;
					sum += this.buckets[k];
					this.buckets[k] = 0;
				}
				return sum;
			}

			// Token: 0x06000338 RID: 824 RVA: 0x000142A8 File Offset: 0x000124A8
			public void Execute(int index)
			{
				int num = index * 16;
				int end = num + 16;
				int jobSum = 0;
				for (int i = num; i < end; i++)
				{
					jobSum = this.JobIndexPrefixSum(jobSum, i);
				}
				this.indicesSum[index] = jobSum;
				if (ParallelSortExtensions.RadixSortBatchPrefixSumJob.AtomicIncrement(this.counter) == 16)
				{
					int sum = 0;
					if (this.radix < 3)
					{
						for (int j = 0; j < 16; j++)
						{
							int indexSum = this.indicesSum[j];
							this.indicesSum[j] = sum;
							sum += indexSum;
						}
					}
					else
					{
						for (int k = 8; k < 16; k++)
						{
							int indexSum2 = this.indicesSum[k];
							this.indicesSum[k] = sum;
							sum += indexSum2;
						}
						for (int l = 0; l < 8; l++)
						{
							int indexSum3 = this.indicesSum[l];
							this.indicesSum[l] = sum;
							sum += indexSum3;
						}
					}
					this.counter[0] = 0;
				}
			}

			// Token: 0x04000435 RID: 1077
			[ReadOnly]
			public int radix;

			// Token: 0x04000436 RID: 1078
			[ReadOnly]
			public int jobsCount;

			// Token: 0x04000437 RID: 1079
			[ReadOnly]
			[NativeDisableContainerSafetyRestriction]
			[NoAlias]
			public NativeArray<int> array;

			// Token: 0x04000438 RID: 1080
			[NativeDisableContainerSafetyRestriction]
			[NoAlias]
			public NativeArray<int> counter;

			// Token: 0x04000439 RID: 1081
			[NativeDisableContainerSafetyRestriction]
			[NoAlias]
			public NativeArray<int> indicesSum;

			// Token: 0x0400043A RID: 1082
			[NativeDisableContainerSafetyRestriction]
			[NoAlias]
			public NativeArray<int> buckets;

			// Token: 0x0400043B RID: 1083
			[NativeDisableContainerSafetyRestriction]
			[NoAlias]
			public NativeArray<int> indices;
		}

		// Token: 0x020000D5 RID: 213
		[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
		internal struct RadixSortPrefixSumJob : IJobFor
		{
			// Token: 0x06000339 RID: 825 RVA: 0x000143A8 File Offset: 0x000125A8
			public void Execute(int index)
			{
				int start = index * 16;
				int end = start + 16;
				int jobSum = this.indicesSum[index];
				for (int i = 0; i < this.jobsCount; i++)
				{
					for (int j = start; j < end; j++)
					{
						int num = i * 256 + j;
						ref NativeArray<int> ptr = ref this.indices;
						int num2 = num;
						ptr[num2] += jobSum;
					}
				}
			}

			// Token: 0x0400043C RID: 1084
			[ReadOnly]
			public int jobsCount;

			// Token: 0x0400043D RID: 1085
			[NativeDisableContainerSafetyRestriction]
			[NoAlias]
			public NativeArray<int> indicesSum;

			// Token: 0x0400043E RID: 1086
			[NativeDisableContainerSafetyRestriction]
			[NoAlias]
			public NativeArray<int> indices;
		}

		// Token: 0x020000D6 RID: 214
		[BurstCompile(DisableSafetyChecks = true, OptimizeFor = OptimizeFor.Performance)]
		internal struct RadixSortBucketSortJob : IJobFor
		{
			// Token: 0x0600033A RID: 826 RVA: 0x00014418 File Offset: 0x00012618
			public void Execute(int index)
			{
				int num = index * this.batchSize;
				int end = math.min(num + this.batchSize, this.array.Length);
				int jobIndices = index * 256;
				for (int i = num; i < end; i++)
				{
					int value = this.array[i];
					int bucket = (value >> this.radix * 8) & 255;
					int num2 = jobIndices + bucket;
					int num3 = this.indices[num2];
					this.indices[num2] = num3 + 1;
					int sortedIndex = num3;
					this.arraySorted[sortedIndex] = value;
				}
			}

			// Token: 0x0400043F RID: 1087
			[ReadOnly]
			public int radix;

			// Token: 0x04000440 RID: 1088
			[ReadOnly]
			public int batchSize;

			// Token: 0x04000441 RID: 1089
			[ReadOnly]
			[NativeDisableContainerSafetyRestriction]
			[NoAlias]
			public NativeArray<int> array;

			// Token: 0x04000442 RID: 1090
			[NativeDisableContainerSafetyRestriction]
			[NoAlias]
			public NativeArray<int> indices;

			// Token: 0x04000443 RID: 1091
			[NativeDisableContainerSafetyRestriction]
			[NoAlias]
			public NativeArray<int> arraySorted;
		}
	}
}
