using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace Unity.Collections
{
	// Token: 0x02000033 RID: 51
	internal struct ArrayOfArrays<[global::System.Runtime.CompilerServices.IsUnmanaged] T> : IDisposable where T : struct, ValueType
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00003B05 File Offset: 0x00001D05
		private int BlockSizeInElements
		{
			get
			{
				return 1 << this.m_log2BlockSizeInElements;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00003B12 File Offset: 0x00001D12
		private int BlockSizeInBytes
		{
			get
			{
				return this.BlockSizeInElements * sizeof(T);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00003B21 File Offset: 0x00001D21
		private int BlockMask
		{
			get
			{
				return this.BlockSizeInElements - 1;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00003B2B File Offset: 0x00001D2B
		public int Length
		{
			get
			{
				return this.m_lengthInElements;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00003B33 File Offset: 0x00001D33
		public int Capacity
		{
			get
			{
				return this.m_capacityInElements;
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00003B3C File Offset: 0x00001D3C
		public unsafe ArrayOfArrays(int capacityInElements, AllocatorManager.AllocatorHandle backingAllocatorHandle, int log2BlockSizeInElements = 12)
		{
			this = default(ArrayOfArrays<T>);
			this.m_backingAllocatorHandle = backingAllocatorHandle;
			this.m_lengthInElements = 0;
			this.m_capacityInElements = capacityInElements;
			this.m_log2BlockSizeInElements = log2BlockSizeInElements;
			this.m_blocks = capacityInElements + this.BlockMask >> this.m_log2BlockSizeInElements;
			this.m_block = (IntPtr*)Memory.Unmanaged.Allocate((long)(sizeof(IntPtr) * this.m_blocks), 16, this.m_backingAllocatorHandle);
			UnsafeUtility.MemSet((void*)this.m_block, 0, (long)(sizeof(IntPtr) * this.m_blocks));
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003BC0 File Offset: 0x00001DC0
		public unsafe void LockfreeAdd(T t)
		{
			int elementIndex = Interlocked.Increment(ref this.m_lengthInElements) - 1;
			int blockIndex = this.BlockIndexOfElement(elementIndex);
			if (this.m_block[(IntPtr)blockIndex * (IntPtr)sizeof(IntPtr) / (IntPtr)sizeof(IntPtr)] == IntPtr.Zero)
			{
				void* pointer = Memory.Unmanaged.Allocate((long)this.BlockSizeInBytes, 16, this.m_backingAllocatorHandle);
				int lastBlock = math.min(this.m_blocks, blockIndex + 4);
				while (blockIndex < lastBlock && !(IntPtr.Zero == Interlocked.CompareExchange(ref this.m_block[(IntPtr)blockIndex * (IntPtr)sizeof(IntPtr) / (IntPtr)sizeof(IntPtr)], (IntPtr)pointer, IntPtr.Zero)))
				{
					blockIndex++;
				}
				if (blockIndex == lastBlock)
				{
					Memory.Unmanaged.Free(pointer, this.m_backingAllocatorHandle);
				}
			}
			*this[elementIndex] = t;
		}

		// Token: 0x17000024 RID: 36
		public unsafe ref T this[int elementIndex]
		{
			get
			{
				int blockIndex = this.BlockIndexOfElement(elementIndex);
				IntPtr intPtr = this.m_block[(IntPtr)blockIndex * (IntPtr)sizeof(IntPtr) / (IntPtr)sizeof(IntPtr)];
				int elementIndexInBlock = elementIndex & this.BlockMask;
				T* blockPointer = (T*)(void*)intPtr;
				return ref blockPointer[(IntPtr)elementIndexInBlock * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00003CB8 File Offset: 0x00001EB8
		public void Rewind()
		{
			this.m_lengthInElements = 0;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00003CC4 File Offset: 0x00001EC4
		public unsafe void Clear()
		{
			this.Rewind();
			for (int i = 0; i < this.m_blocks; i++)
			{
				if (this.m_block[(IntPtr)i * (IntPtr)sizeof(IntPtr) / (IntPtr)sizeof(IntPtr)] != IntPtr.Zero)
				{
					Memory.Unmanaged.Free((void*)this.m_block[(IntPtr)i * (IntPtr)sizeof(IntPtr) / (IntPtr)sizeof(IntPtr)], this.m_backingAllocatorHandle);
					this.m_block[(IntPtr)i * (IntPtr)sizeof(IntPtr) / (IntPtr)sizeof(IntPtr)] = IntPtr.Zero;
				}
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00003D3C File Offset: 0x00001F3C
		public void Dispose()
		{
			this.Clear();
			Memory.Unmanaged.Free<IntPtr>(this.m_block, this.m_backingAllocatorHandle);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00003D55 File Offset: 0x00001F55
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private void CheckElementIndex(int elementIndex)
		{
			if (elementIndex >= this.m_lengthInElements)
			{
				throw new ArgumentException(string.Format("Element index {0} must be less than length in elements {1}.", elementIndex, this.m_lengthInElements));
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00003D81 File Offset: 0x00001F81
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private void CheckBlockIndex(int blockIndex)
		{
			if (blockIndex >= this.m_blocks)
			{
				throw new ArgumentException(string.Format("Block index {0} must be less than number of blocks {1}.", blockIndex, this.m_blocks));
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00003DAD File Offset: 0x00001FAD
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private unsafe void CheckBlockIsNotNull(int blockIndex)
		{
			if (this.m_block[(IntPtr)blockIndex * (IntPtr)sizeof(IntPtr) / (IntPtr)sizeof(IntPtr)] == IntPtr.Zero)
			{
				throw new ArgumentException(string.Format("Block index {0} is a null pointer.", blockIndex));
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00003DE2 File Offset: 0x00001FE2
		public unsafe void RemoveAtSwapBack(int elementIndex)
		{
			*this[elementIndex] = *this[this.Length - 1];
			this.m_lengthInElements--;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00003E11 File Offset: 0x00002011
		private int BlockIndexOfElement(int elementIndex)
		{
			return elementIndex >> this.m_log2BlockSizeInElements;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00003E20 File Offset: 0x00002020
		public unsafe void TrimExcess()
		{
			for (int blockIndex = this.BlockIndexOfElement(this.m_lengthInElements + this.BlockMask); blockIndex < this.m_blocks; blockIndex++)
			{
				if (this.m_block[(IntPtr)blockIndex * (IntPtr)sizeof(IntPtr) / (IntPtr)sizeof(IntPtr)] != IntPtr.Zero)
				{
					Memory.Unmanaged.Free((void*)this.m_block[(IntPtr)blockIndex * (IntPtr)sizeof(IntPtr) / (IntPtr)sizeof(IntPtr)], this.m_backingAllocatorHandle);
					this.m_block[(IntPtr)blockIndex * (IntPtr)sizeof(IntPtr) / (IntPtr)sizeof(IntPtr)] = IntPtr.Zero;
				}
			}
		}

		// Token: 0x04000077 RID: 119
		private AllocatorManager.AllocatorHandle m_backingAllocatorHandle;

		// Token: 0x04000078 RID: 120
		private int m_lengthInElements;

		// Token: 0x04000079 RID: 121
		private int m_capacityInElements;

		// Token: 0x0400007A RID: 122
		private int m_log2BlockSizeInElements;

		// Token: 0x0400007B RID: 123
		private int m_blocks;

		// Token: 0x0400007C RID: 124
		private unsafe IntPtr* m_block;
	}
}
