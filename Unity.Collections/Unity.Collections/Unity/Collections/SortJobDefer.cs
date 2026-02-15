using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x020000C8 RID: 200
	[GenerateTestsForBurstCompatibility(RequiredUnityDefine = "UNITY_2020_2_OR_NEWER", GenericTypeArguments = new Type[]
	{
		typeof(int),
		typeof(NativeSortExtension.DefaultComparer<int>)
	})]
	public struct SortJobDefer<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U> where T : struct, ValueType where U : IComparer<T>
	{
		// Token: 0x06000938 RID: 2360 RVA: 0x0001BFA0 File Offset: 0x0001A1A0
		public JobHandle Schedule(JobHandle inputDeps = default(JobHandle))
		{
			SortJobDefer<T, U>.SegmentSort segmentSortJob = new SortJobDefer<T, U>.SegmentSort
			{
				DataRO = this.Data,
				Data = this.Data.m_ListData,
				Comp = this.Comp,
				SegmentWidth = 1024
			};
			JobHandle segmentSortJobHandle = (ref segmentSortJob).ScheduleByRef(this.Data, 1024, inputDeps);
			return new SortJobDefer<T, U>.SegmentSortMerge
			{
				Data = this.Data,
				Comp = this.Comp,
				SegmentWidth = 1024
			}.Schedule(segmentSortJobHandle);
		}

		// Token: 0x040003FA RID: 1018
		public NativeList<T> Data;

		// Token: 0x040003FB RID: 1019
		public U Comp;

		// Token: 0x020000C9 RID: 201
		[BurstCompile]
		public struct SegmentSort : IJobParallelForDefer
		{
			// Token: 0x06000939 RID: 2361 RVA: 0x0001C038 File Offset: 0x0001A238
			public unsafe void Execute(int index)
			{
				int startIndex = index * this.SegmentWidth;
				int segmentLength = ((this.Data->Length - startIndex < this.SegmentWidth) ? (this.Data->Length - startIndex) : this.SegmentWidth);
				NativeSortExtension.Sort<T, U>(this.Data->Ptr + (IntPtr)startIndex * (IntPtr)sizeof(T) / (IntPtr)sizeof(T), segmentLength, this.Comp);
			}

			// Token: 0x040003FC RID: 1020
			[ReadOnly]
			internal NativeList<T> DataRO;

			// Token: 0x040003FD RID: 1021
			[NativeDisableUnsafePtrRestriction]
			internal unsafe UnsafeList<T>* Data;

			// Token: 0x040003FE RID: 1022
			internal U Comp;

			// Token: 0x040003FF RID: 1023
			internal int SegmentWidth;
		}

		// Token: 0x020000CA RID: 202
		[BurstCompile]
		public struct SegmentSortMerge : IJob
		{
			// Token: 0x0600093A RID: 2362 RVA: 0x0001C09C File Offset: 0x0001A29C
			public unsafe void Execute()
			{
				int length = this.Data.Length;
				T* ptr = this.Data.GetUnsafePtr<T>();
				int segmentCount = (length + (this.SegmentWidth - 1)) / this.SegmentWidth;
				int* segmentIndex;
				checked
				{
					segmentIndex = stackalloc int[unchecked((UIntPtr)segmentCount) * 4];
				}
				T* resultCopy = (T*)Memory.Unmanaged.Allocate((long)(UnsafeUtility.SizeOf<T>() * length), 16, Allocator.Temp);
				for (int sortIndex = 0; sortIndex < length; sortIndex++)
				{
					int bestSegmentIndex = -1;
					T bestValue = default(T);
					for (int i = 0; i < segmentCount; i++)
					{
						int startIndex = i * this.SegmentWidth;
						int offset = segmentIndex[i];
						int segmentLength = ((length - startIndex < this.SegmentWidth) ? (length - startIndex) : this.SegmentWidth);
						if (offset != segmentLength)
						{
							T nextValue = ptr[(IntPtr)(startIndex + offset) * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
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
				UnsafeUtility.MemCpy((void*)ptr, (void*)resultCopy, (long)(UnsafeUtility.SizeOf<T>() * length));
			}

			// Token: 0x04000400 RID: 1024
			[NativeDisableUnsafePtrRestriction]
			internal NativeList<T> Data;

			// Token: 0x04000401 RID: 1025
			internal U Comp;

			// Token: 0x04000402 RID: 1026
			internal int SegmentWidth;
		}
	}
}
