using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000DC RID: 220
	internal struct BuddyAllocator : IDisposable
	{
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x00014F20 File Offset: 0x00013120
		private ref BuddyAllocator.Header header
		{
			get
			{
				return UnsafeUtility.AsRef<BuddyAllocator.Header>(this.m_Data);
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x00014F2D File Offset: 0x0001312D
		private NativeArray<int> freeMaskCounts
		{
			get
			{
				return this.GetNativeArray<int>(this.m_ActiveFreeMaskCounts.Item1, this.m_ActiveFreeMaskCounts.Item2);
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x00014F4B File Offset: 0x0001314B
		private NativeArray<ulong> freeMasksStorage
		{
			get
			{
				return this.GetNativeArray<ulong>(this.m_FreeMasksStorage.Item1, this.m_FreeMasksStorage.Item2);
			}
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00014F6C File Offset: 0x0001316C
		private NativeArray<ulong> FreeMasks(int level)
		{
			return this.freeMasksStorage.GetSubArray(BuddyAllocator.LevelOffset64(level, this.header.branchingOrder), BuddyAllocator.LevelLength64(level, this.header.branchingOrder));
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x00014FA9 File Offset: 0x000131A9
		private NativeArray<int> freeMaskIndicesStorage
		{
			get
			{
				return this.GetNativeArray<int>(this.m_FreeMaskIndicesStorage.Item1, this.m_FreeMaskIndicesStorage.Item2);
			}
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00014FC8 File Offset: 0x000131C8
		private NativeArray<int> FreeMaskIndices(int level)
		{
			return this.freeMaskIndicesStorage.GetSubArray(BuddyAllocator.LevelOffset64(level, this.header.branchingOrder), BuddyAllocator.LevelLength64(level, this.header.branchingOrder));
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x00015005 File Offset: 0x00013205
		public int levelCount
		{
			get
			{
				return this.header.levelCount;
			}
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00015014 File Offset: 0x00013214
		public unsafe BuddyAllocator(int levelCount, int branchingOrder, Allocator allocator = Allocator.Persistent)
		{
			int dataSize = sizeof(BuddyAllocator.Header);
			this.m_ActiveFreeMaskCounts = BuddyAllocator.AllocateRange<int>(levelCount, ref dataSize);
			this.m_FreeMasksStorage = BuddyAllocator.AllocateRange<ulong>(BuddyAllocator.LevelOffset64(levelCount, branchingOrder), ref dataSize);
			this.m_FreeMaskIndicesStorage = BuddyAllocator.AllocateRange<int>(BuddyAllocator.LevelOffset64(levelCount, branchingOrder), ref dataSize);
			this.m_Data = UnsafeUtility.Malloc((long)dataSize, 64, allocator);
			UnsafeUtility.MemClear(this.m_Data, (long)dataSize);
			this.m_Allocator = allocator;
			*this.header = new BuddyAllocator.Header
			{
				branchingOrder = branchingOrder,
				levelCount = levelCount
			};
			this.FreeMasks(0)[0] = 15UL;
			this.freeMaskCounts[0] = 1;
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x000150CC File Offset: 0x000132CC
		public bool TryAllocate(int requestedLevel, out BuddyAllocation allocation)
		{
			allocation = default(BuddyAllocation);
			int level = requestedLevel;
			NativeArray<int> maskCounts = this.freeMaskCounts;
			while (level >= 0 && maskCounts[level] <= 0)
			{
				level--;
			}
			if (level < 0)
			{
				return false;
			}
			NativeArray<int> freeMaskIndices = this.FreeMaskIndices(level);
			int num = level;
			int num2 = maskCounts[num] - 1;
			maskCounts[num] = num2;
			int maskIndex = freeMaskIndices[num2];
			NativeArray<ulong> freeMasks = this.FreeMasks(level);
			ulong freeMask = freeMasks[maskIndex];
			int bitIndex = math.tzcnt(freeMask);
			freeMask ^= 1UL << bitIndex;
			freeMasks[maskIndex] = freeMask;
			if (freeMask != 0UL)
			{
				num2 = level;
				num = maskCounts[num2];
				maskCounts[num2] = num + 1;
				freeMaskIndices[num] = maskIndex;
			}
			int dataIndex = maskIndex * 64 + bitIndex;
			while (level < requestedLevel)
			{
				level++;
				dataIndex <<= this.header.branchingOrder;
				int maskIndex2 = dataIndex >> 6;
				int bitIndex2 = dataIndex & 63;
				NativeArray<ulong> freeMasks2 = this.FreeMasks(level);
				ulong freeMask2 = freeMasks2[maskIndex2];
				if (freeMask2 == 0UL)
				{
					NativeArray<int> freeMaskIndices2 = this.FreeMaskIndices(level);
					num = level;
					num2 = maskCounts[num];
					maskCounts[num] = num2 + 1;
					freeMaskIndices2[num2] = maskIndex2;
				}
				freeMask2 |= (1UL << BuddyAllocator.Pow2(this.header.branchingOrder)) - 2UL << bitIndex2;
				freeMasks2[maskIndex2] = freeMask2;
			}
			allocation.level = level;
			allocation.index = dataIndex;
			return true;
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00015248 File Offset: 0x00013448
		public void Free(BuddyAllocation allocation)
		{
			int level = allocation.level;
			int dataIndex = allocation.index;
			while (level >= 0)
			{
				int maskIndex = dataIndex >> 6;
				int bitIndex = dataIndex & 63;
				NativeArray<ulong> freeMasks = this.FreeMasks(level);
				ulong freeMask = freeMasks[maskIndex];
				bool wasZero = freeMask == 0UL;
				freeMask |= 1UL << bitIndex;
				NativeArray<int> indices = this.FreeMaskIndices(level);
				NativeArray<int> counts = this.freeMaskCounts;
				ulong superBlockMask = (1UL << BuddyAllocator.Pow2(this.header.branchingOrder)) - 1UL << (bitIndex >> this.header.branchingOrder) * BuddyAllocator.Pow2(this.header.branchingOrder);
				if (level == 0 || (~(freeMask != 0UL) & superBlockMask) != 0UL)
				{
					freeMasks[maskIndex] = freeMask;
					if (wasZero)
					{
						int num = level;
						int num2 = counts[num];
						counts[num] = num2 + 1;
						indices[num2] = maskIndex;
						return;
					}
					break;
				}
				else
				{
					freeMask &= ~superBlockMask;
					freeMasks[maskIndex] = freeMask;
					if (!wasZero && freeMask == 0UL)
					{
						for (int i = 0; i < indices.Length; i++)
						{
							if (indices[i] == maskIndex)
							{
								int num3 = i;
								int num2 = level;
								int num = counts[num2] - 1;
								counts[num2] = num;
								indices[num3] = indices[num];
								break;
							}
						}
					}
					level--;
					dataIndex >>= this.header.branchingOrder;
				}
			}
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x000153B0 File Offset: 0x000135B0
		public unsafe void Dispose()
		{
			UnsafeUtility.Free(this.m_Data, this.m_Allocator);
			this.m_Data = default(void*);
			this.m_Allocator = Allocator.Invalid;
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x000153D6 File Offset: 0x000135D6
		private NativeArray<T> GetNativeArray<T>(int offset, int length) where T : struct
		{
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(BuddyAllocator.PtrAdd(this.m_Data, offset), length, this.m_Allocator);
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x000153F0 File Offset: 0x000135F0
		private static int LevelOffset(int level, int branchingOrder)
		{
			return BuddyAllocator.Pow2(branchingOrder) * (BuddyAllocator.Pow2(branchingOrder * (level - 1) + branchingOrder) - 1) / (BuddyAllocator.Pow2(branchingOrder) - 1);
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00015410 File Offset: 0x00013610
		private static int LevelLength(int level, int branchingOrder)
		{
			return BuddyAllocator.Pow2N(branchingOrder, level + 1);
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0001541B File Offset: 0x0001361B
		private static int LevelOffset64(int level, int branchingOrder)
		{
			return math.min(level, 6 / branchingOrder) + BuddyAllocator.LevelOffset(math.max(0, level - 6 / branchingOrder), branchingOrder);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00015438 File Offset: 0x00013638
		private static int LevelLength64(int level, int branchingOrder)
		{
			return BuddyAllocator.Pow2N(branchingOrder, math.max(0, level - 6 / branchingOrder + 1));
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0001544D File Offset: 0x0001364D
		private static ValueTuple<int, int> AllocateRange<T>(int length, ref int dataSize) where T : struct
		{
			dataSize = BuddyAllocator.AlignForward(dataSize, UnsafeUtility.AlignOf<T>());
			ValueTuple<int, int> valueTuple = new ValueTuple<int, int>(dataSize, length);
			dataSize += length * UnsafeUtility.SizeOf<T>();
			return valueTuple;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00015474 File Offset: 0x00013674
		private static int AlignForward(int offset, int alignment)
		{
			int modulo = offset % alignment;
			if (modulo != 0)
			{
				offset += alignment - modulo;
			}
			return offset;
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00015490 File Offset: 0x00013690
		private unsafe static void* PtrAdd(void* ptr, int bytes)
		{
			return (void*)((IntPtr)ptr + bytes);
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x000154A3 File Offset: 0x000136A3
		private static int Pow2(int n)
		{
			return 1 << n;
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x000154AB File Offset: 0x000136AB
		private static int Pow2N(int x, int n)
		{
			return 1 << x * n;
		}

		// Token: 0x040004D8 RID: 1240
		private unsafe void* m_Data;

		// Token: 0x040004D9 RID: 1241
		private ValueTuple<int, int> m_ActiveFreeMaskCounts;

		// Token: 0x040004DA RID: 1242
		private ValueTuple<int, int> m_FreeMasksStorage;

		// Token: 0x040004DB RID: 1243
		private ValueTuple<int, int> m_FreeMaskIndicesStorage;

		// Token: 0x040004DC RID: 1244
		private Allocator m_Allocator;

		// Token: 0x020000DD RID: 221
		private struct Header
		{
			// Token: 0x040004DD RID: 1245
			public int branchingOrder;

			// Token: 0x040004DE RID: 1246
			public int levelCount;

			// Token: 0x040004DF RID: 1247
			public int allocationCount;

			// Token: 0x040004E0 RID: 1248
			public int freeAllocationIdsCount;
		}
	}
}
