using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using AOT;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Mathematics;

namespace Unity.Collections
{
	// Token: 0x020000D8 RID: 216
	[BurstCompile]
	public struct RewindableAllocator : AllocatorManager.IAllocator, IDisposable
	{
		// Token: 0x060009EA RID: 2538 RVA: 0x0001D274 File Offset: 0x0001B474
		public unsafe void Initialize(int initialSizeInBytes, bool enableBlockFree = false)
		{
			this.m_spinner = default(Spinner);
			this.m_block = new UnmanagedArray<RewindableAllocator.MemoryBlock>(64, Allocator.Persistent);
			long blockSize = (((long)initialSizeInBytes > 131072L) ? ((long)initialSizeInBytes) : 131072L);
			*this.m_block[0] = new RewindableAllocator.MemoryBlock(blockSize);
			this.m_last = (this.m_used = 0);
			this.m_enableBlockFree = (enableBlockFree ? 1 : 0);
			this.m_reachMaxBlockSize = (((long)initialSizeInBytes >= 67108864L) ? 1 : 0);
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x0001D2FD File Offset: 0x0001B4FD
		// (set) Token: 0x060009EC RID: 2540 RVA: 0x0001D308 File Offset: 0x0001B508
		public bool EnableBlockFree
		{
			get
			{
				return this.m_enableBlockFree > 0;
			}
			set
			{
				this.m_enableBlockFree = (value ? 1 : 0);
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x0001D317 File Offset: 0x0001B517
		public int BlocksAllocated
		{
			get
			{
				return this.m_last + 1;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x0001D321 File Offset: 0x0001B521
		public int InitialSizeInBytes
		{
			get
			{
				return (int)this.m_block[0].m_bytes;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x0001D335 File Offset: 0x0001B535
		internal long MaxMemoryBlockSize
		{
			get
			{
				return 67108864L;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x0001D340 File Offset: 0x0001B540
		internal long BytesAllocated
		{
			get
			{
				long totalBytes = 0L;
				for (int i = 0; i <= this.m_last; i++)
				{
					totalBytes += this.m_block[i].m_bytes;
				}
				return totalBytes;
			}
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0001D378 File Offset: 0x0001B578
		public void Rewind()
		{
			if (JobsUtility.IsExecutingJob)
			{
				throw new InvalidOperationException("You cannot Rewind a RewindableAllocator from a Job.");
			}
			this.m_handle.Rewind();
			while (this.m_last > this.m_used)
			{
				int num = this.m_last;
				this.m_last = num - 1;
				this.m_block[num].Dispose();
			}
			while (this.m_used > 0)
			{
				int num = this.m_used;
				this.m_used = num - 1;
				this.m_block[num].Rewind();
			}
			this.m_block[0].Rewind();
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0001D410 File Offset: 0x0001B610
		public void Dispose()
		{
			if (JobsUtility.IsExecutingJob)
			{
				throw new InvalidOperationException("You cannot Dispose a RewindableAllocator from a Job.");
			}
			this.m_used = 0;
			this.Rewind();
			this.m_block[0].Dispose();
			this.m_block.Dispose();
			this.m_last = (this.m_used = 0);
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x0001D468 File Offset: 0x0001B668
		[ExcludeFromBurstCompatTesting("Uses managed delegate")]
		public AllocatorManager.TryFunction Function
		{
			get
			{
				return new AllocatorManager.TryFunction(RewindableAllocator.Try);
			}
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0001D478 File Offset: 0x0001B678
		private unsafe int TryAllocate(ref AllocatorManager.Block block, int startIndex, int lastIndex, long alignedSize, long alignmentMask)
		{
			int best = startIndex;
			while (best <= lastIndex)
			{
				RewindableAllocator.Union readUnion = default(RewindableAllocator.Union);
				bool skip = false;
				readUnion.m_long = Interlocked.Read(ref this.m_block[best].m_union.m_long);
				long begin;
				RewindableAllocator.Union oldUnion;
				do
				{
					begin = (readUnion.m_current + alignmentMask) & ~alignmentMask;
					if (begin + block.Bytes > this.m_block[best].m_bytes)
					{
						goto Block_1;
					}
					oldUnion = readUnion;
					RewindableAllocator.Union newUnion = default(RewindableAllocator.Union);
					newUnion.m_current = ((begin + alignedSize > this.m_block[best].m_bytes) ? this.m_block[best].m_bytes : (begin + alignedSize));
					newUnion.m_allocCount = readUnion.m_allocCount + 1L;
					readUnion.m_long = Interlocked.CompareExchange(ref this.m_block[best].m_union.m_long, newUnion.m_long, oldUnion.m_long);
				}
				while (readUnion.m_long != oldUnion.m_long);
				IL_00F9:
				if (!skip)
				{
					block.Range.Pointer = (IntPtr)((void*)(this.m_block[best].m_pointer + begin));
					block.AllocatedItems = block.Range.Items;
					Interlocked.MemoryBarrier();
					int readUsed = this.m_used;
					int oldUsed;
					int newUsed;
					do
					{
						oldUsed = readUsed;
						newUsed = ((best > oldUsed) ? best : oldUsed);
						readUsed = Interlocked.CompareExchange(ref this.m_used, newUsed, oldUsed);
					}
					while (newUsed != oldUsed);
					return 0;
				}
				best++;
				continue;
				Block_1:
				skip = true;
				goto IL_00F9;
			}
			return -1;
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0001D5FC File Offset: 0x0001B7FC
		public unsafe int Try(ref AllocatorManager.Block block)
		{
			if (block.Range.Pointer == IntPtr.Zero)
			{
				int alignment = math.max(64, block.Alignment);
				int extra = ((alignment != 64) ? 1 : 0);
				int cachelineMask = 63;
				if (extra == 1)
				{
					alignment = (alignment + cachelineMask) & ~cachelineMask;
				}
				long mask = (long)alignment - 1L;
				long size = (block.Bytes + (long)(extra * alignment) + mask) & ~mask;
				int last = this.m_last;
				int error = this.TryAllocate(ref block, 0, this.m_last, size, mask);
				if (error == 0)
				{
					return error;
				}
				this.m_spinner.Acquire();
				error = this.TryAllocate(ref block, last, this.m_last, size, mask);
				if (error == 0)
				{
					this.m_spinner.Release();
					return error;
				}
				long bytes;
				if (this.m_reachMaxBlockSize == 0)
				{
					bytes = this.m_block[this.m_last].m_bytes << 1;
				}
				else
				{
					bytes = this.m_block[this.m_last].m_bytes + 67108864L;
				}
				bytes = math.max(bytes, size);
				this.m_reachMaxBlockSize = ((bytes >= 67108864L) ? 1 : 0);
				*this.m_block[this.m_last + 1] = new RewindableAllocator.MemoryBlock(bytes);
				Interlocked.Increment(ref this.m_last);
				error = this.TryAllocate(ref block, this.m_last, this.m_last, size, mask);
				this.m_spinner.Release();
				return error;
			}
			else
			{
				if (block.Range.Items == 0)
				{
					if (this.m_enableBlockFree != 0)
					{
						for (int blockIndex = 0; blockIndex <= this.m_last; blockIndex++)
						{
							if (this.m_block[blockIndex].Contains(block.Range.Pointer))
							{
								RewindableAllocator.Union readUnion = default(RewindableAllocator.Union);
								readUnion.m_long = Interlocked.Read(ref this.m_block[blockIndex].m_union.m_long);
								RewindableAllocator.Union oldUnion;
								do
								{
									oldUnion = readUnion;
									RewindableAllocator.Union newUnion = readUnion;
									long allocCount = newUnion.m_allocCount;
									newUnion.m_allocCount = allocCount - 1L;
									if (newUnion.m_allocCount == 0L)
									{
										newUnion.m_current = 0L;
									}
									readUnion.m_long = Interlocked.CompareExchange(ref this.m_block[blockIndex].m_union.m_long, newUnion.m_long, oldUnion.m_long);
								}
								while (readUnion.m_long != oldUnion.m_long);
							}
						}
					}
					return 0;
				}
				return -1;
			}
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0001D85C File Offset: 0x0001BA5C
		[BurstCompile]
		[MonoPInvokeCallback(typeof(AllocatorManager.TryFunction))]
		internal static int Try(IntPtr state, ref AllocatorManager.Block block)
		{
			return RewindableAllocator.Try_000009DE$BurstDirectCall.Invoke(state, ref block);
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x0001D865 File Offset: 0x0001BA65
		// (set) Token: 0x060009F8 RID: 2552 RVA: 0x0001D86D File Offset: 0x0001BA6D
		public AllocatorManager.AllocatorHandle Handle
		{
			get
			{
				return this.m_handle;
			}
			set
			{
				this.m_handle = value;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x0001D876 File Offset: 0x0001BA76
		public Allocator ToAllocator
		{
			get
			{
				return this.m_handle.ToAllocator;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x0001D883 File Offset: 0x0001BA83
		public bool IsCustomAllocator
		{
			get
			{
				return this.m_handle.IsCustomAllocator;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x000040E9 File Offset: 0x000022E9
		public bool IsAutoDispose
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0001D890 File Offset: 0x0001BA90
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public NativeArray<T> AllocateNativeArray<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(int length) where T : struct, ValueType
		{
			return new NativeArray<T>
			{
				m_Buffer = (ref this).AllocateStruct(default(T), length),
				m_Length = length,
				m_AllocatorLabel = Allocator.None
			};
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0001D8D0 File Offset: 0x0001BAD0
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe NativeList<T> AllocateNativeList<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(int capacity) where T : struct, ValueType
		{
			NativeList<T> container = default(NativeList<T>);
			container.m_ListData = (ref this).Allocate(default(UnsafeList<T>), 1);
			container.m_ListData->Ptr = (ref this).Allocate(default(T), capacity);
			container.m_ListData->m_length = 0;
			container.m_ListData->m_capacity = capacity;
			container.m_ListData->Allocator = Allocator.None;
			return container;
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0001D941 File Offset: 0x0001BB41
		[BurstCompile]
		[MonoPInvokeCallback(typeof(AllocatorManager.TryFunction))]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int Try$BurstManaged(IntPtr state, ref AllocatorManager.Block block)
		{
			return ((RewindableAllocator*)(void*)state)->Try(ref block);
		}

		// Token: 0x04000416 RID: 1046
		private const int kLog2MaxMemoryBlockSize = 26;

		// Token: 0x04000417 RID: 1047
		private const long kMaxMemoryBlockSize = 67108864L;

		// Token: 0x04000418 RID: 1048
		private const long kMinMemoryBlockSize = 131072L;

		// Token: 0x04000419 RID: 1049
		private const int kMaxNumBlocks = 64;

		// Token: 0x0400041A RID: 1050
		private const int kBlockBusyRewindMask = -2147483648;

		// Token: 0x0400041B RID: 1051
		private const int kBlockBusyAllocateMask = 2147483647;

		// Token: 0x0400041C RID: 1052
		private Spinner m_spinner;

		// Token: 0x0400041D RID: 1053
		private AllocatorManager.AllocatorHandle m_handle;

		// Token: 0x0400041E RID: 1054
		private UnmanagedArray<RewindableAllocator.MemoryBlock> m_block;

		// Token: 0x0400041F RID: 1055
		private int m_last;

		// Token: 0x04000420 RID: 1056
		private int m_used;

		// Token: 0x04000421 RID: 1057
		private byte m_enableBlockFree;

		// Token: 0x04000422 RID: 1058
		private byte m_reachMaxBlockSize;

		// Token: 0x020000D9 RID: 217
		internal struct Union
		{
			// Token: 0x1700012A RID: 298
			// (get) Token: 0x060009FF RID: 2559 RVA: 0x0001D94F File Offset: 0x0001BB4F
			// (set) Token: 0x06000A00 RID: 2560 RVA: 0x0001D961 File Offset: 0x0001BB61
			internal long m_current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_long & 1099511627775L;
				}
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				set
				{
					this.m_long &= -1099511627776L;
					this.m_long |= value & 1099511627775L;
				}
			}

			// Token: 0x1700012B RID: 299
			// (get) Token: 0x06000A01 RID: 2561 RVA: 0x0001D991 File Offset: 0x0001BB91
			// (set) Token: 0x06000A02 RID: 2562 RVA: 0x0001D9A3 File Offset: 0x0001BBA3
			internal long m_allocCount
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return (this.m_long >> 40) & 16777215L;
				}
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				set
				{
					this.m_long &= 1099511627775L;
					this.m_long |= (value & 16777215L) << 40;
				}
			}

			// Token: 0x04000423 RID: 1059
			internal long m_long;

			// Token: 0x04000424 RID: 1060
			private const int currentBits = 40;

			// Token: 0x04000425 RID: 1061
			private const int currentOffset = 0;

			// Token: 0x04000426 RID: 1062
			private const long currentMask = 1099511627775L;

			// Token: 0x04000427 RID: 1063
			private const int allocCountBits = 24;

			// Token: 0x04000428 RID: 1064
			private const int allocCountOffset = 40;

			// Token: 0x04000429 RID: 1065
			private const long allocCountMask = 16777215L;
		}

		// Token: 0x020000DA RID: 218
		[GenerateTestsForBurstCompatibility]
		internal struct MemoryBlock : IDisposable
		{
			// Token: 0x06000A03 RID: 2563 RVA: 0x0001D9D3 File Offset: 0x0001BBD3
			public unsafe MemoryBlock(long bytes)
			{
				this.m_pointer = (byte*)Memory.Unmanaged.Allocate(bytes, 16384, Allocator.Persistent);
				this.m_bytes = bytes;
				this.m_union = default(RewindableAllocator.Union);
			}

			// Token: 0x06000A04 RID: 2564 RVA: 0x0001D9FF File Offset: 0x0001BBFF
			public void Rewind()
			{
				this.m_union = default(RewindableAllocator.Union);
			}

			// Token: 0x06000A05 RID: 2565 RVA: 0x0001DA0D File Offset: 0x0001BC0D
			public void Dispose()
			{
				Memory.Unmanaged.Free<byte>(this.m_pointer, Allocator.Persistent);
				this.m_pointer = null;
				this.m_bytes = 0L;
				this.m_union = default(RewindableAllocator.Union);
			}

			// Token: 0x06000A06 RID: 2566 RVA: 0x0001DA3C File Offset: 0x0001BC3C
			public unsafe bool Contains(IntPtr ptr)
			{
				void* pointer = (void*)ptr;
				return pointer >= (void*)this.m_pointer && pointer < (void*)(this.m_pointer + this.m_union.m_current);
			}

			// Token: 0x0400042A RID: 1066
			public const int kMaximumAlignment = 16384;

			// Token: 0x0400042B RID: 1067
			public unsafe byte* m_pointer;

			// Token: 0x0400042C RID: 1068
			public long m_bytes;

			// Token: 0x0400042D RID: 1069
			public RewindableAllocator.Union m_union;
		}

		// Token: 0x020000DB RID: 219
		// (Invoke) Token: 0x06000A08 RID: 2568
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int Try_000009DE$PostfixBurstDelegate(IntPtr state, ref AllocatorManager.Block block);

		// Token: 0x020000DC RID: 220
		internal static class Try_000009DE$BurstDirectCall
		{
			// Token: 0x06000A0B RID: 2571 RVA: 0x0001DA74 File Offset: 0x0001BC74
			[BurstDiscard]
			private static void GetFunctionPointerDiscard(ref IntPtr A_0)
			{
				if (RewindableAllocator.Try_000009DE$BurstDirectCall.Pointer == 0)
				{
					RewindableAllocator.Try_000009DE$BurstDirectCall.Pointer = BurstCompiler.CompileFunctionPointer<RewindableAllocator.Try_000009DE$PostfixBurstDelegate>(new RewindableAllocator.Try_000009DE$PostfixBurstDelegate(RewindableAllocator.Try)).Value;
				}
				A_0 = RewindableAllocator.Try_000009DE$BurstDirectCall.Pointer;
			}

			// Token: 0x06000A0C RID: 2572 RVA: 0x0001DAB4 File Offset: 0x0001BCB4
			private static IntPtr GetFunctionPointer()
			{
				IntPtr intPtr = (IntPtr)0;
				RewindableAllocator.Try_000009DE$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
				return intPtr;
			}

			// Token: 0x06000A0D RID: 2573 RVA: 0x0001DACC File Offset: 0x0001BCCC
			public static int Invoke(IntPtr state, ref AllocatorManager.Block block)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = RewindableAllocator.Try_000009DE$BurstDirectCall.GetFunctionPointer();
					if (functionPointer != 0)
					{
						return calli(System.Int32(System.IntPtr,Unity.Collections.AllocatorManager/Block&), state, ref block, functionPointer);
					}
				}
				return RewindableAllocator.Try$BurstManaged(state, ref block);
			}

			// Token: 0x0400042E RID: 1070
			private static IntPtr Pointer;
		}
	}
}
