using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x020000CB RID: 203
	[NativeContainer]
	[GenerateTestsForBurstCompatibility]
	public struct NativeStream : INativeDisposable, IDisposable
	{
		// Token: 0x0600093B RID: 2363 RVA: 0x0001C1C2 File Offset: 0x0001A3C2
		public NativeStream(int bufferCount, AllocatorManager.AllocatorHandle allocator)
		{
			NativeStream.AllocateBlock(out this, allocator);
			this.m_Stream.AllocateForEach(bufferCount);
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0001C1D8 File Offset: 0x0001A3D8
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe static JobHandle ScheduleConstruct<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(out NativeStream stream, NativeList<T> bufferCount, JobHandle dependency, AllocatorManager.AllocatorHandle allocator) where T : struct, ValueType
		{
			NativeStream.AllocateBlock(out stream, allocator);
			return new NativeStream.ConstructJobList
			{
				List = (UntypedUnsafeList*)bufferCount.GetUnsafeList(),
				Container = stream
			}.Schedule(dependency);
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0001C218 File Offset: 0x0001A418
		public static JobHandle ScheduleConstruct(out NativeStream stream, NativeArray<int> bufferCount, JobHandle dependency, AllocatorManager.AllocatorHandle allocator)
		{
			NativeStream.AllocateBlock(out stream, allocator);
			return new NativeStream.ConstructJob
			{
				Length = bufferCount,
				Container = stream
			}.Schedule(dependency);
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x0001C250 File Offset: 0x0001A450
		public readonly bool IsEmpty()
		{
			return this.m_Stream.IsEmpty();
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x0001C25D File Offset: 0x0001A45D
		public readonly bool IsCreated
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Stream.IsCreated;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x0001C26A File Offset: 0x0001A46A
		public readonly int ForEachCount
		{
			get
			{
				return this.m_Stream.ForEachCount;
			}
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0001C277 File Offset: 0x0001A477
		public NativeStream.Reader AsReader()
		{
			return new NativeStream.Reader(ref this);
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0001C27F File Offset: 0x0001A47F
		public NativeStream.Writer AsWriter()
		{
			return new NativeStream.Writer(ref this);
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0001C287 File Offset: 0x0001A487
		public int Count()
		{
			return this.m_Stream.Count();
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0001C294 File Offset: 0x0001A494
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public NativeArray<T> ToNativeArray<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(AllocatorManager.AllocatorHandle allocator) where T : struct, ValueType
		{
			return this.m_Stream.ToNativeArray<T>(allocator);
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0001C2A2 File Offset: 0x0001A4A2
		public void Dispose()
		{
			if (!this.IsCreated)
			{
				return;
			}
			this.m_Stream.Dispose();
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0001C2B8 File Offset: 0x0001A4B8
		public JobHandle Dispose(JobHandle inputDeps)
		{
			if (!this.IsCreated)
			{
				return inputDeps;
			}
			JobHandle jobHandle = new NativeStreamDisposeJob
			{
				Data = new NativeStreamDispose
				{
					m_StreamData = this.m_Stream
				}
			}.Schedule(inputDeps);
			this.m_Stream = default(UnsafeStream);
			return jobHandle;
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0001C307 File Offset: 0x0001A507
		private static void AllocateBlock(out NativeStream stream, AllocatorManager.AllocatorHandle allocator)
		{
			UnsafeStream.AllocateBlock(out stream.m_Stream, allocator);
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0001C315 File Offset: 0x0001A515
		private void AllocateForEach(int forEachCount)
		{
			this.m_Stream.AllocateForEach(forEachCount);
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x0001C323 File Offset: 0x0001A523
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckForEachCountGreaterThanZero(int forEachCount)
		{
			if (forEachCount <= 0)
			{
				throw new ArgumentException("foreachCount must be > 0", "foreachCount");
			}
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00002C47 File Offset: 0x00000E47
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private readonly void CheckRead()
		{
		}

		// Token: 0x04000403 RID: 1027
		private UnsafeStream m_Stream;

		// Token: 0x020000CC RID: 204
		[BurstCompile]
		private struct ConstructJobList : IJob
		{
			// Token: 0x0600094B RID: 2379 RVA: 0x0001C339 File Offset: 0x0001A539
			public unsafe void Execute()
			{
				this.Container.AllocateForEach(this.List->m_length);
			}

			// Token: 0x04000404 RID: 1028
			public NativeStream Container;

			// Token: 0x04000405 RID: 1029
			[ReadOnly]
			[NativeDisableUnsafePtrRestriction]
			public unsafe UntypedUnsafeList* List;
		}

		// Token: 0x020000CD RID: 205
		[BurstCompile]
		private struct ConstructJob : IJob
		{
			// Token: 0x0600094C RID: 2380 RVA: 0x0001C351 File Offset: 0x0001A551
			public void Execute()
			{
				this.Container.AllocateForEach(this.Length[0]);
			}

			// Token: 0x04000406 RID: 1030
			public NativeStream Container;

			// Token: 0x04000407 RID: 1031
			[ReadOnly]
			public NativeArray<int> Length;
		}

		// Token: 0x020000CE RID: 206
		[NativeContainer]
		[NativeContainerSupportsMinMaxWriteRestriction]
		[GenerateTestsForBurstCompatibility]
		public struct Writer
		{
			// Token: 0x0600094D RID: 2381 RVA: 0x0001C36A File Offset: 0x0001A56A
			internal Writer(ref NativeStream stream)
			{
				this.m_Writer = stream.m_Stream.AsWriter();
			}

			// Token: 0x1700010E RID: 270
			// (get) Token: 0x0600094E RID: 2382 RVA: 0x0001C37D File Offset: 0x0001A57D
			public int ForEachCount
			{
				get
				{
					return this.m_Writer.ForEachCount;
				}
			}

			// Token: 0x0600094F RID: 2383 RVA: 0x00002C47 File Offset: 0x00000E47
			public void PatchMinMaxRange(int foreEachIndex)
			{
			}

			// Token: 0x06000950 RID: 2384 RVA: 0x0001C38A File Offset: 0x0001A58A
			public void BeginForEachIndex(int foreachIndex)
			{
				this.m_Writer.BeginForEachIndex(foreachIndex);
			}

			// Token: 0x06000951 RID: 2385 RVA: 0x0001C398 File Offset: 0x0001A598
			public void EndForEachIndex()
			{
				this.m_Writer.EndForEachIndex();
			}

			// Token: 0x06000952 RID: 2386 RVA: 0x0001C3A5 File Offset: 0x0001A5A5
			[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
			public unsafe void Write<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(T value) where T : struct, ValueType
			{
				*this.Allocate<T>() = value;
			}

			// Token: 0x06000953 RID: 2387 RVA: 0x0001C3B4 File Offset: 0x0001A5B4
			[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
			public unsafe ref T Allocate<[global::System.Runtime.CompilerServices.IsUnmanaged] T>() where T : struct, ValueType
			{
				int size = UnsafeUtility.SizeOf<T>();
				return UnsafeUtility.AsRef<T>((void*)this.Allocate(size));
			}

			// Token: 0x06000954 RID: 2388 RVA: 0x0001C3D3 File Offset: 0x0001A5D3
			public unsafe byte* Allocate(int size)
			{
				return this.m_Writer.Allocate(size);
			}

			// Token: 0x06000955 RID: 2389 RVA: 0x00002C47 File Offset: 0x00000E47
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[Conditional("UNITY_DOTS_DEBUG")]
			private void CheckBeginForEachIndex(int foreachIndex)
			{
			}

			// Token: 0x06000956 RID: 2390 RVA: 0x00002C47 File Offset: 0x00000E47
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[Conditional("UNITY_DOTS_DEBUG")]
			private void CheckEndForEachIndex()
			{
			}

			// Token: 0x06000957 RID: 2391 RVA: 0x00002C47 File Offset: 0x00000E47
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[Conditional("UNITY_DOTS_DEBUG")]
			private void CheckAllocateSize(int size)
			{
			}

			// Token: 0x04000408 RID: 1032
			private UnsafeStream.Writer m_Writer;
		}

		// Token: 0x020000CF RID: 207
		[NativeContainer]
		[NativeContainerIsReadOnly]
		[GenerateTestsForBurstCompatibility]
		public struct Reader
		{
			// Token: 0x06000958 RID: 2392 RVA: 0x0001C3E1 File Offset: 0x0001A5E1
			internal Reader(ref NativeStream stream)
			{
				this.m_Reader = stream.m_Stream.AsReader();
			}

			// Token: 0x06000959 RID: 2393 RVA: 0x0001C3F4 File Offset: 0x0001A5F4
			public int BeginForEachIndex(int foreachIndex)
			{
				return this.m_Reader.BeginForEachIndex(foreachIndex);
			}

			// Token: 0x0600095A RID: 2394 RVA: 0x0001C402 File Offset: 0x0001A602
			public void EndForEachIndex()
			{
				this.m_Reader.EndForEachIndex();
			}

			// Token: 0x1700010F RID: 271
			// (get) Token: 0x0600095B RID: 2395 RVA: 0x0001C40F File Offset: 0x0001A60F
			public int ForEachCount
			{
				get
				{
					return this.m_Reader.ForEachCount;
				}
			}

			// Token: 0x17000110 RID: 272
			// (get) Token: 0x0600095C RID: 2396 RVA: 0x0001C41C File Offset: 0x0001A61C
			public int RemainingItemCount
			{
				get
				{
					return this.m_Reader.RemainingItemCount;
				}
			}

			// Token: 0x0600095D RID: 2397 RVA: 0x0001C42C File Offset: 0x0001A62C
			public unsafe byte* ReadUnsafePtr(int size)
			{
				this.m_Reader.m_RemainingItemCount = this.m_Reader.m_RemainingItemCount - 1;
				byte* ptr = this.m_Reader.m_CurrentPtr;
				this.m_Reader.m_CurrentPtr = this.m_Reader.m_CurrentPtr + size;
				if (this.m_Reader.m_CurrentPtr != this.m_Reader.m_CurrentBlockEnd)
				{
					this.m_Reader.m_CurrentBlock = this.m_Reader.m_CurrentBlock->Next;
					this.m_Reader.m_CurrentPtr = &this.m_Reader.m_CurrentBlock->Data.FixedElementField;
					this.m_Reader.m_CurrentBlockEnd = (byte*)(this.m_Reader.m_CurrentBlock + 4096 / sizeof(UnsafeStreamBlock));
					ptr = this.m_Reader.m_CurrentPtr;
					this.m_Reader.m_CurrentPtr = this.m_Reader.m_CurrentPtr + size;
				}
				return ptr;
			}

			// Token: 0x0600095E RID: 2398 RVA: 0x0001C4F4 File Offset: 0x0001A6F4
			[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
			public unsafe ref T Read<[global::System.Runtime.CompilerServices.IsUnmanaged] T>() where T : struct, ValueType
			{
				int size = UnsafeUtility.SizeOf<T>();
				return UnsafeUtility.AsRef<T>((void*)this.ReadUnsafePtr(size));
			}

			// Token: 0x0600095F RID: 2399 RVA: 0x0001C513 File Offset: 0x0001A713
			[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
			public ref T Peek<[global::System.Runtime.CompilerServices.IsUnmanaged] T>() where T : struct, ValueType
			{
				UnsafeUtility.SizeOf<T>();
				return this.m_Reader.Peek<T>();
			}

			// Token: 0x06000960 RID: 2400 RVA: 0x0001C526 File Offset: 0x0001A726
			public int Count()
			{
				return this.m_Reader.Count();
			}

			// Token: 0x06000961 RID: 2401 RVA: 0x00002C47 File Offset: 0x00000E47
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[Conditional("UNITY_DOTS_DEBUG")]
			private void CheckNotReadingOutOfBounds(int size)
			{
			}

			// Token: 0x06000962 RID: 2402 RVA: 0x00002C47 File Offset: 0x00000E47
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			private void CheckRead()
			{
			}

			// Token: 0x06000963 RID: 2403 RVA: 0x00002C47 File Offset: 0x00000E47
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[Conditional("UNITY_DOTS_DEBUG")]
			private void CheckReadSize(int size)
			{
			}

			// Token: 0x06000964 RID: 2404 RVA: 0x00002C47 File Offset: 0x00000E47
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[Conditional("UNITY_DOTS_DEBUG")]
			private void CheckBeginForEachIndex(int forEachIndex)
			{
			}

			// Token: 0x06000965 RID: 2405 RVA: 0x0001C533 File Offset: 0x0001A733
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[Conditional("UNITY_DOTS_DEBUG")]
			private void CheckEndForEachIndex()
			{
				if (this.m_Reader.m_RemainingItemCount != 0)
				{
					throw new ArgumentException("Not all elements (Count) have been read. If this is intentional, simply skip calling EndForEachIndex();");
				}
				if (this.m_Reader.m_CurrentBlockEnd != this.m_Reader.m_CurrentPtr)
				{
					throw new ArgumentException("Not all data (Data Size) has been read. If this is intentional, simply skip calling EndForEachIndex();");
				}
			}

			// Token: 0x04000409 RID: 1033
			private UnsafeStream.Reader m_Reader;
		}
	}
}
