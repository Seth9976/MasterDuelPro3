using System;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000145 RID: 325
	[GenerateTestsForBurstCompatibility]
	public struct UnsafeStream : INativeDisposable, IDisposable
	{
		// Token: 0x06000D89 RID: 3465 RVA: 0x00029CDE File Offset: 0x00027EDE
		public UnsafeStream(int bufferCount, AllocatorManager.AllocatorHandle allocator)
		{
			UnsafeStream.AllocateBlock(out this, allocator);
			this.AllocateForEach(bufferCount);
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00029CF0 File Offset: 0x00027EF0
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe static JobHandle ScheduleConstruct<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(out UnsafeStream stream, NativeList<T> bufferCount, JobHandle dependency, AllocatorManager.AllocatorHandle allocator) where T : struct, ValueType
		{
			UnsafeStream.AllocateBlock(out stream, allocator);
			return new UnsafeStream.ConstructJobList
			{
				List = (UntypedUnsafeList*)bufferCount.GetUnsafeList(),
				Container = stream
			}.Schedule(dependency);
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x00029D30 File Offset: 0x00027F30
		public static JobHandle ScheduleConstruct(out UnsafeStream stream, NativeArray<int> bufferCount, JobHandle dependency, AllocatorManager.AllocatorHandle allocator)
		{
			UnsafeStream.AllocateBlock(out stream, allocator);
			return new UnsafeStream.ConstructJob
			{
				Length = bufferCount,
				Container = stream
			}.Schedule(dependency);
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x00029D68 File Offset: 0x00027F68
		internal unsafe static void AllocateBlock(out UnsafeStream stream, AllocatorManager.AllocatorHandle allocator)
		{
			int blockCount = JobsUtility.ThreadIndexCount;
			int allocationSize = sizeof(UnsafeStreamBlockData) + sizeof(UnsafeStreamBlock*) * blockCount;
			AllocatorManager.Block blk = (ref allocator).AllocateBlock(allocationSize, 16, 1);
			UnsafeUtility.MemClear((void*)blk.Range.Pointer, blk.AllocatedBytes);
			stream.m_BlockData = blk;
			UnsafeStreamBlockData* blockData = (UnsafeStreamBlockData*)(void*)blk.Range.Pointer;
			blockData->Allocator = allocator;
			blockData->BlockCount = blockCount;
			blockData->Blocks = (UnsafeStreamBlock**)(void*)(blk.Range.Pointer + sizeof(UnsafeStreamBlockData));
			blockData->Ranges = default(AllocatorManager.Block);
			blockData->RangeCount = 0;
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x00029E10 File Offset: 0x00028010
		internal unsafe void AllocateForEach(int forEachCount)
		{
			UnsafeStreamBlockData* blockData = (UnsafeStreamBlockData*)(void*)this.m_BlockData.Range.Pointer;
			blockData->Ranges = (ref this.m_BlockData.Range.Allocator).AllocateBlock(sizeof(UnsafeStreamRange), 16, forEachCount);
			blockData->RangeCount = forEachCount;
			UnsafeUtility.MemClear((void*)blockData->Ranges.Range.Pointer, blockData->Ranges.AllocatedBytes);
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x00029E84 File Offset: 0x00028084
		public unsafe readonly bool IsEmpty()
		{
			if (!this.IsCreated)
			{
				return true;
			}
			UnsafeStreamBlockData* blockData = (UnsafeStreamBlockData*)(void*)this.m_BlockData.Range.Pointer;
			UnsafeStreamRange* ranges = (UnsafeStreamRange*)(void*)blockData->Ranges.Range.Pointer;
			for (int i = 0; i != blockData->RangeCount; i++)
			{
				if (ranges[i].ElementCount > 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000D8F RID: 3471 RVA: 0x00029EEE File Offset: 0x000280EE
		public readonly bool IsCreated
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_BlockData.Range.Pointer != IntPtr.Zero;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x00029F0A File Offset: 0x0002810A
		public unsafe readonly int ForEachCount
		{
			get
			{
				return ((UnsafeStreamBlockData*)(void*)this.m_BlockData.Range.Pointer)->RangeCount;
			}
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x00029F26 File Offset: 0x00028126
		public UnsafeStream.Reader AsReader()
		{
			return new UnsafeStream.Reader(ref this);
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x00029F2E File Offset: 0x0002812E
		public UnsafeStream.Writer AsWriter()
		{
			return new UnsafeStream.Writer(ref this);
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x00029F38 File Offset: 0x00028138
		public unsafe int Count()
		{
			int itemCount = 0;
			UnsafeStreamBlockData* blockData = (UnsafeStreamBlockData*)(void*)this.m_BlockData.Range.Pointer;
			UnsafeStreamRange* ranges = (UnsafeStreamRange*)(void*)blockData->Ranges.Range.Pointer;
			for (int i = 0; i != blockData->RangeCount; i++)
			{
				itemCount += ranges[i].ElementCount;
			}
			return itemCount;
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x00029F98 File Offset: 0x00028198
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe NativeArray<T> ToNativeArray<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(AllocatorManager.AllocatorHandle allocator) where T : struct, ValueType
		{
			NativeArray<T> array = CollectionHelper.CreateNativeArray<T>(this.Count(), allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeStream.Reader reader = this.AsReader();
			int offset = 0;
			for (int i = 0; i != reader.ForEachCount; i++)
			{
				reader.BeginForEachIndex(i);
				int rangeItemCount = reader.RemainingItemCount;
				for (int j = 0; j < rangeItemCount; j++)
				{
					array[offset] = *reader.Read<T>();
					offset++;
				}
				reader.EndForEachIndex();
			}
			return array;
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x0002A014 File Offset: 0x00028214
		private unsafe void Deallocate()
		{
			if (!this.IsCreated)
			{
				return;
			}
			UnsafeStreamBlockData* blockData = (UnsafeStreamBlockData*)(void*)this.m_BlockData.Range.Pointer;
			for (int i = 0; i != blockData->BlockCount; i++)
			{
				UnsafeStreamBlock* next;
				for (UnsafeStreamBlock* block = *(IntPtr*)(blockData->Blocks + (IntPtr)i * (IntPtr)sizeof(UnsafeStreamBlock*) / (IntPtr)sizeof(UnsafeStreamBlock*)); block != null; block = next)
				{
					next = block->Next;
					blockData->Free(block);
				}
			}
			blockData->Ranges.Dispose();
			this.m_BlockData.Dispose();
			this.m_BlockData = default(AllocatorManager.Block);
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x0002A09A File Offset: 0x0002829A
		public void Dispose()
		{
			if (!this.IsCreated)
			{
				return;
			}
			this.Deallocate();
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x0002A0AC File Offset: 0x000282AC
		public JobHandle Dispose(JobHandle inputDeps)
		{
			if (!this.IsCreated)
			{
				return inputDeps;
			}
			JobHandle jobHandle = new UnsafeStream.DisposeJob
			{
				Container = this
			}.Schedule(inputDeps);
			this.m_BlockData = default(AllocatorManager.Block);
			return jobHandle;
		}

		// Token: 0x04000536 RID: 1334
		[NativeDisableUnsafePtrRestriction]
		internal AllocatorManager.Block m_BlockData;

		// Token: 0x02000146 RID: 326
		[BurstCompile]
		private struct DisposeJob : IJob
		{
			// Token: 0x06000D98 RID: 3480 RVA: 0x0002A0EB File Offset: 0x000282EB
			public void Execute()
			{
				this.Container.Deallocate();
			}

			// Token: 0x04000537 RID: 1335
			public UnsafeStream Container;
		}

		// Token: 0x02000147 RID: 327
		[BurstCompile]
		private struct ConstructJobList : IJob
		{
			// Token: 0x06000D99 RID: 3481 RVA: 0x0002A0F8 File Offset: 0x000282F8
			public unsafe void Execute()
			{
				this.Container.AllocateForEach(this.List->m_length);
			}

			// Token: 0x04000538 RID: 1336
			public UnsafeStream Container;

			// Token: 0x04000539 RID: 1337
			[ReadOnly]
			[NativeDisableUnsafePtrRestriction]
			public unsafe UntypedUnsafeList* List;
		}

		// Token: 0x02000148 RID: 328
		[BurstCompile]
		private struct ConstructJob : IJob
		{
			// Token: 0x06000D9A RID: 3482 RVA: 0x0002A110 File Offset: 0x00028310
			public void Execute()
			{
				this.Container.AllocateForEach(this.Length[0]);
			}

			// Token: 0x0400053A RID: 1338
			public UnsafeStream Container;

			// Token: 0x0400053B RID: 1339
			[ReadOnly]
			public NativeArray<int> Length;
		}

		// Token: 0x02000149 RID: 329
		[GenerateTestsForBurstCompatibility]
		public struct Writer
		{
			// Token: 0x06000D9B RID: 3483 RVA: 0x0002A12C File Offset: 0x0002832C
			internal Writer(ref UnsafeStream stream)
			{
				this.m_BlockData = stream.m_BlockData;
				this.m_ForeachIndex = int.MinValue;
				this.m_ElementCount = -1;
				this.m_CurrentBlock = null;
				this.m_CurrentBlockEnd = null;
				this.m_CurrentPtr = null;
				this.m_FirstBlock = null;
				this.m_NumberOfBlocks = 0;
				this.m_FirstOffset = 0;
				this.m_ThreadIndex = 0;
			}

			// Token: 0x1700019E RID: 414
			// (get) Token: 0x06000D9C RID: 3484 RVA: 0x0002A18C File Offset: 0x0002838C
			public unsafe int ForEachCount
			{
				get
				{
					return ((UnsafeStreamBlockData*)(void*)this.m_BlockData.Range.Pointer)->RangeCount;
				}
			}

			// Token: 0x06000D9D RID: 3485 RVA: 0x0002A1A8 File Offset: 0x000283A8
			public unsafe void BeginForEachIndex(int foreachIndex)
			{
				this.m_ForeachIndex = foreachIndex;
				this.m_ElementCount = 0;
				this.m_NumberOfBlocks = 0;
				this.m_FirstBlock = this.m_CurrentBlock;
				this.m_FirstOffset = (int)((long)((byte*)this.m_CurrentPtr - (byte*)this.m_CurrentBlock));
			}

			// Token: 0x06000D9E RID: 3486 RVA: 0x0002A1E4 File Offset: 0x000283E4
			public unsafe void EndForEachIndex()
			{
				UnsafeStreamBlockData* blockData = (UnsafeStreamBlockData*)(void*)this.m_BlockData.Range.Pointer;
				UnsafeStreamRange* ranges = (UnsafeStreamRange*)(void*)blockData->Ranges.Range.Pointer;
				ranges[this.m_ForeachIndex].ElementCount = this.m_ElementCount;
				ranges[this.m_ForeachIndex].OffsetInFirstBlock = this.m_FirstOffset;
				ranges[this.m_ForeachIndex].Block = this.m_FirstBlock;
				ranges[this.m_ForeachIndex].LastOffset = (int)((long)((byte*)this.m_CurrentPtr - (byte*)this.m_CurrentBlock));
				ranges[this.m_ForeachIndex].NumberOfBlocks = this.m_NumberOfBlocks;
			}

			// Token: 0x06000D9F RID: 3487 RVA: 0x0002A2AF File Offset: 0x000284AF
			[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
			public unsafe void Write<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(T value) where T : struct, ValueType
			{
				*this.Allocate<T>() = value;
			}

			// Token: 0x06000DA0 RID: 3488 RVA: 0x0002A2C0 File Offset: 0x000284C0
			[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
			public unsafe ref T Allocate<[global::System.Runtime.CompilerServices.IsUnmanaged] T>() where T : struct, ValueType
			{
				int size = UnsafeUtility.SizeOf<T>();
				return UnsafeUtility.AsRef<T>((void*)this.Allocate(size));
			}

			// Token: 0x06000DA1 RID: 3489 RVA: 0x0002A2E0 File Offset: 0x000284E0
			public unsafe byte* Allocate(int size)
			{
				byte* ptr = this.m_CurrentPtr;
				this.m_CurrentPtr += size;
				if (this.m_CurrentPtr != this.m_CurrentBlockEnd)
				{
					UnsafeStreamBlock* oldBlock = this.m_CurrentBlock;
					UnsafeStreamBlockData* blockData = (UnsafeStreamBlockData*)(void*)this.m_BlockData.Range.Pointer;
					this.m_CurrentBlock = blockData->Allocate(oldBlock, this.m_ThreadIndex);
					this.m_CurrentPtr = &this.m_CurrentBlock->Data.FixedElementField;
					if (this.m_FirstBlock == null)
					{
						this.m_FirstOffset = (int)((long)((byte*)this.m_CurrentPtr - (byte*)this.m_CurrentBlock));
						this.m_FirstBlock = this.m_CurrentBlock;
					}
					else
					{
						this.m_NumberOfBlocks++;
					}
					this.m_CurrentBlockEnd = (byte*)(this.m_CurrentBlock + 4096 / sizeof(UnsafeStreamBlock));
					ptr = this.m_CurrentPtr;
					this.m_CurrentPtr += size;
				}
				this.m_ElementCount++;
				return ptr;
			}

			// Token: 0x0400053C RID: 1340
			[NativeDisableUnsafePtrRestriction]
			internal AllocatorManager.Block m_BlockData;

			// Token: 0x0400053D RID: 1341
			[NativeDisableUnsafePtrRestriction]
			private unsafe UnsafeStreamBlock* m_CurrentBlock;

			// Token: 0x0400053E RID: 1342
			[NativeDisableUnsafePtrRestriction]
			private unsafe byte* m_CurrentPtr;

			// Token: 0x0400053F RID: 1343
			[NativeDisableUnsafePtrRestriction]
			private unsafe byte* m_CurrentBlockEnd;

			// Token: 0x04000540 RID: 1344
			internal int m_ForeachIndex;

			// Token: 0x04000541 RID: 1345
			private int m_ElementCount;

			// Token: 0x04000542 RID: 1346
			[NativeDisableUnsafePtrRestriction]
			private unsafe UnsafeStreamBlock* m_FirstBlock;

			// Token: 0x04000543 RID: 1347
			private int m_FirstOffset;

			// Token: 0x04000544 RID: 1348
			private int m_NumberOfBlocks;

			// Token: 0x04000545 RID: 1349
			[NativeSetThreadIndex]
			private int m_ThreadIndex;
		}

		// Token: 0x0200014A RID: 330
		[GenerateTestsForBurstCompatibility]
		public struct Reader
		{
			// Token: 0x06000DA2 RID: 3490 RVA: 0x0002A3CD File Offset: 0x000285CD
			internal Reader(ref UnsafeStream stream)
			{
				this.m_BlockData = stream.m_BlockData;
				this.m_CurrentBlock = null;
				this.m_CurrentPtr = null;
				this.m_CurrentBlockEnd = null;
				this.m_RemainingItemCount = 0;
				this.m_LastBlockSize = 0;
			}

			// Token: 0x06000DA3 RID: 3491 RVA: 0x0002A404 File Offset: 0x00028604
			public unsafe int BeginForEachIndex(int foreachIndex)
			{
				UnsafeStreamBlockData* blockData = (UnsafeStreamBlockData*)(void*)this.m_BlockData.Range.Pointer;
				UnsafeStreamRange* ranges = (UnsafeStreamRange*)(void*)blockData->Ranges.Range.Pointer;
				this.m_RemainingItemCount = ranges[foreachIndex].ElementCount;
				this.m_LastBlockSize = ranges[foreachIndex].LastOffset;
				this.m_CurrentBlock = ranges[foreachIndex].Block;
				this.m_CurrentPtr = (byte*)(this.m_CurrentBlock + ranges[foreachIndex].OffsetInFirstBlock / sizeof(UnsafeStreamBlock));
				this.m_CurrentBlockEnd = (byte*)(this.m_CurrentBlock + 4096 / sizeof(UnsafeStreamBlock));
				return this.m_RemainingItemCount;
			}

			// Token: 0x06000DA4 RID: 3492 RVA: 0x00002C47 File Offset: 0x00000E47
			public void EndForEachIndex()
			{
			}

			// Token: 0x1700019F RID: 415
			// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x0002A4B4 File Offset: 0x000286B4
			public unsafe int ForEachCount
			{
				get
				{
					return ((UnsafeStreamBlockData*)(void*)this.m_BlockData.Range.Pointer)->RangeCount;
				}
			}

			// Token: 0x170001A0 RID: 416
			// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x0002A4D0 File Offset: 0x000286D0
			public int RemainingItemCount
			{
				get
				{
					return this.m_RemainingItemCount;
				}
			}

			// Token: 0x06000DA7 RID: 3495 RVA: 0x0002A4D8 File Offset: 0x000286D8
			public unsafe byte* ReadUnsafePtr(int size)
			{
				this.m_RemainingItemCount--;
				byte* ptr = this.m_CurrentPtr;
				this.m_CurrentPtr += size;
				if (this.m_CurrentPtr != this.m_CurrentBlockEnd)
				{
					this.m_CurrentBlock = this.m_CurrentBlock->Next;
					this.m_CurrentPtr = &this.m_CurrentBlock->Data.FixedElementField;
					this.m_CurrentBlockEnd = (byte*)(this.m_CurrentBlock + 4096 / sizeof(UnsafeStreamBlock));
					ptr = this.m_CurrentPtr;
					this.m_CurrentPtr += size;
				}
				return ptr;
			}

			// Token: 0x06000DA8 RID: 3496 RVA: 0x0002A568 File Offset: 0x00028768
			[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
			public unsafe ref T Read<[global::System.Runtime.CompilerServices.IsUnmanaged] T>() where T : struct, ValueType
			{
				int size = UnsafeUtility.SizeOf<T>();
				return UnsafeUtility.AsRef<T>((void*)this.ReadUnsafePtr(size));
			}

			// Token: 0x06000DA9 RID: 3497 RVA: 0x0002A588 File Offset: 0x00028788
			[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
			public unsafe ref T Peek<[global::System.Runtime.CompilerServices.IsUnmanaged] T>() where T : struct, ValueType
			{
				int size = UnsafeUtility.SizeOf<T>();
				byte* ptr = this.m_CurrentPtr;
				if (ptr + size != this.m_CurrentBlockEnd)
				{
					ptr = &this.m_CurrentBlock->Next->Data.FixedElementField;
				}
				return UnsafeUtility.AsRef<T>((void*)ptr);
			}

			// Token: 0x06000DAA RID: 3498 RVA: 0x0002A5CC File Offset: 0x000287CC
			public unsafe int Count()
			{
				UnsafeStreamBlockData* blockData = (UnsafeStreamBlockData*)(void*)this.m_BlockData.Range.Pointer;
				UnsafeStreamRange* ranges = (UnsafeStreamRange*)(void*)blockData->Ranges.Range.Pointer;
				int itemCount = 0;
				for (int i = 0; i != blockData->RangeCount; i++)
				{
					itemCount += ranges[i].ElementCount;
				}
				return itemCount;
			}

			// Token: 0x04000546 RID: 1350
			[NativeDisableUnsafePtrRestriction]
			internal AllocatorManager.Block m_BlockData;

			// Token: 0x04000547 RID: 1351
			[NativeDisableUnsafePtrRestriction]
			internal unsafe UnsafeStreamBlock* m_CurrentBlock;

			// Token: 0x04000548 RID: 1352
			[NativeDisableUnsafePtrRestriction]
			internal unsafe byte* m_CurrentPtr;

			// Token: 0x04000549 RID: 1353
			[NativeDisableUnsafePtrRestriction]
			internal unsafe byte* m_CurrentBlockEnd;

			// Token: 0x0400054A RID: 1354
			internal int m_RemainingItemCount;

			// Token: 0x0400054B RID: 1355
			internal int m_LastBlockSize;
		}
	}
}
