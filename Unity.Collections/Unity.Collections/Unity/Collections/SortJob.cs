using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Mathematics;

namespace Unity.Collections
{
	// Token: 0x020000C5 RID: 197
	[GenerateTestsForBurstCompatibility(RequiredUnityDefine = "UNITY_2020_2_OR_NEWER", GenericTypeArguments = new Type[]
	{
		typeof(int),
		typeof(NativeSortExtension.DefaultComparer<int>)
	})]
	public struct SortJob<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U> where T : struct, ValueType where U : IComparer<T>
	{
		// Token: 0x06000935 RID: 2357 RVA: 0x0001BD50 File Offset: 0x00019F50
		public JobHandle Schedule(JobHandle inputDeps = default(JobHandle))
		{
			if (this.Length == 0)
			{
				return inputDeps;
			}
			int segmentCount = (this.Length + 1023) / 1024;
			int maxThreadCount = JobsUtility.ThreadIndexCount;
			int workerCount = math.max(1, maxThreadCount);
			int workerSegmentCount = segmentCount / workerCount;
			JobHandle segmentSortJobHandle = new SortJob<T, U>.SegmentSort
			{
				Data = this.Data,
				Comp = this.Comp,
				Length = this.Length,
				SegmentWidth = 1024
			}.Schedule(segmentCount, workerSegmentCount, inputDeps);
			return new SortJob<T, U>.SegmentSortMerge
			{
				Data = this.Data,
				Comp = this.Comp,
				Length = this.Length,
				SegmentWidth = 1024
			}.Schedule(segmentSortJobHandle);
		}

		// Token: 0x040003EF RID: 1007
		public unsafe T* Data;

		// Token: 0x040003F0 RID: 1008
		public U Comp;

		// Token: 0x040003F1 RID: 1009
		public int Length;

		// Token: 0x020000C6 RID: 198
		[BurstCompile]
		public struct SegmentSort : IJobParallelFor
		{
			// Token: 0x06000936 RID: 2358 RVA: 0x0001BE18 File Offset: 0x0001A018
			public void Execute(int index)
			{
				int startIndex = index * this.SegmentWidth;
				int segmentLength = ((this.Length - startIndex < this.SegmentWidth) ? (this.Length - startIndex) : this.SegmentWidth);
				NativeSortExtension.Sort<T, U>(this.Data + (IntPtr)startIndex * (IntPtr)sizeof(T) / (IntPtr)sizeof(T), segmentLength, this.Comp);
			}

			// Token: 0x040003F2 RID: 1010
			[NativeDisableUnsafePtrRestriction]
			internal unsafe T* Data;

			// Token: 0x040003F3 RID: 1011
			internal U Comp;

			// Token: 0x040003F4 RID: 1012
			internal int Length;

			// Token: 0x040003F5 RID: 1013
			internal int SegmentWidth;
		}

		// Token: 0x020000C7 RID: 199
		[BurstCompile]
		public struct SegmentSortMerge : IJob
		{
			// Token: 0x06000937 RID: 2359 RVA: 0x0001BE6C File Offset: 0x0001A06C
			public unsafe void Execute()
			{
				int segmentCount = (this.Length + (this.SegmentWidth - 1)) / this.SegmentWidth;
				int* segmentIndex;
				checked
				{
					segmentIndex = stackalloc int[unchecked((UIntPtr)segmentCount) * 4];
				}
				T* resultCopy = (T*)Memory.Unmanaged.Allocate((long)(UnsafeUtility.SizeOf<T>() * this.Length), 16, Allocator.Temp);
				for (int sortIndex = 0; sortIndex < this.Length; sortIndex++)
				{
					int bestSegmentIndex = -1;
					T bestValue = default(T);
					for (int i = 0; i < segmentCount; i++)
					{
						int startIndex = i * this.SegmentWidth;
						int offset = segmentIndex[i];
						int segmentLength = ((this.Length - startIndex < this.SegmentWidth) ? (this.Length - startIndex) : this.SegmentWidth);
						if (offset != segmentLength)
						{
							T nextValue = this.Data[(IntPtr)(startIndex + offset) * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
							if (bestSegmentIndex == -1 || this.Comp.Compare(nextValue, bestValue) <= 0)
							{
								bestValue = nextValue;
								bestSegmentIndex = i;
							}
						}
					}
					segmentIndex[bestSegmentIndex]++;
					resultCopy[(IntPtr)sortIndex * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)] = bestValue;
				}
				UnsafeUtility.MemCpy((void*)this.Data, (void*)resultCopy, (long)(UnsafeUtility.SizeOf<T>() * this.Length));
			}

			// Token: 0x040003F6 RID: 1014
			[NativeDisableUnsafePtrRestriction]
			internal unsafe T* Data;

			// Token: 0x040003F7 RID: 1015
			internal U Comp;

			// Token: 0x040003F8 RID: 1016
			internal int Length;

			// Token: 0x040003F9 RID: 1017
			internal int SegmentWidth;
		}
	}
}
