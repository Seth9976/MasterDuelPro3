using System;
using System.Threading;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace UnityEngine.Rendering
{
	// Token: 0x020000D1 RID: 209
	internal struct ParallelBitArray
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000323 RID: 803 RVA: 0x00013C4A File Offset: 0x00011E4A
		public int Length
		{
			get
			{
				return this.m_Length;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000324 RID: 804 RVA: 0x00013C52 File Offset: 0x00011E52
		public bool IsCreated
		{
			get
			{
				return this.m_Bits.IsCreated;
			}
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00013C5F File Offset: 0x00011E5F
		public ParallelBitArray(int length, Allocator allocator, NativeArrayOptions options = NativeArrayOptions.ClearMemory)
		{
			this.m_Allocator = allocator;
			this.m_Bits = new NativeArray<long>((length + 63) / 64, allocator, options);
			this.m_Length = length;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00013C83 File Offset: 0x00011E83
		public void Dispose()
		{
			this.m_Bits.Dispose();
			this.m_Length = 0;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00013C97 File Offset: 0x00011E97
		public void Dispose(JobHandle inputDeps)
		{
			this.m_Bits.Dispose(inputDeps);
			this.m_Length = 0;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00013CB0 File Offset: 0x00011EB0
		public void Resize(int newLength)
		{
			int oldLength = this.m_Length;
			if (newLength == oldLength)
			{
				return;
			}
			int oldBitsLength = this.m_Bits.Length;
			int newBitsLength = (newLength + 63) / 64;
			if (newBitsLength != oldBitsLength)
			{
				NativeArray<long> newBits = new NativeArray<long>(newBitsLength, this.m_Allocator, NativeArrayOptions.UninitializedMemory);
				if (this.m_Bits.IsCreated)
				{
					NativeArray<long>.Copy(this.m_Bits, newBits, this.m_Bits.Length);
					this.m_Bits.Dispose();
				}
				this.m_Bits = newBits;
			}
			int validLength = Math.Min(oldLength, newLength);
			for (int chunkIndex = Math.Min(oldBitsLength, newBitsLength); chunkIndex < this.m_Bits.Length; chunkIndex++)
			{
				int validBitCount = Math.Max(validLength - 64 * chunkIndex, 0);
				if (validBitCount < 64)
				{
					ulong validMask = (1UL << validBitCount) - 1UL;
					ref NativeArray<long> ptr = ref this.m_Bits;
					int num = chunkIndex;
					ptr[num] &= (long)validMask;
				}
			}
			this.m_Length = newLength;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00013D9C File Offset: 0x00011F9C
		public unsafe void Set(int index, bool value)
		{
			int entry_index = index >> 6;
			long* entries = (long*)this.m_Bits.GetUnsafePtr<long>();
			ulong bit = 1UL << index;
			long and_mask = (long)(~(long)bit);
			long or_mask = (long)(value ? bit : 0UL);
			long old_entry;
			long new_entry;
			do
			{
				old_entry = Interlocked.Read(ref entries[entry_index]);
				new_entry = (old_entry & and_mask) | or_mask;
			}
			while (Interlocked.CompareExchange(ref entries[entry_index], new_entry, old_entry) != old_entry);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00013DFC File Offset: 0x00011FFC
		public unsafe bool Get(int index)
		{
			int entry_index = index >> 6;
			long* entries = (long*)this.m_Bits.GetUnsafeReadOnlyPtr<long>();
			long check_mask = 1L << index;
			return (entries[entry_index] & check_mask) != 0L;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00013E31 File Offset: 0x00012031
		public ulong GetChunk(int chunk_index)
		{
			return (ulong)this.m_Bits[chunk_index];
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00013E3F File Offset: 0x0001203F
		public void SetChunk(int chunk_index, ulong chunk_bits)
		{
			this.m_Bits[chunk_index] = (long)chunk_bits;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00013E50 File Offset: 0x00012050
		public unsafe ulong InterlockedReadChunk(int chunk_index)
		{
			long* entries = (long*)this.m_Bits.GetUnsafeReadOnlyPtr<long>();
			return (ulong)Interlocked.Read(ref entries[chunk_index]);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00013E74 File Offset: 0x00012074
		public unsafe void InterlockedOrChunk(int chunk_index, ulong chunk_bits)
		{
			long* entries = (long*)this.m_Bits.GetUnsafePtr<long>();
			long old_entry;
			long new_entry;
			do
			{
				old_entry = Interlocked.Read(ref entries[chunk_index]);
				new_entry = old_entry | (long)chunk_bits;
			}
			while (Interlocked.CompareExchange(ref entries[chunk_index], new_entry, old_entry) != old_entry);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00013EAD File Offset: 0x000120AD
		public int ChunkCount()
		{
			return this.m_Bits.Length;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00013EBC File Offset: 0x000120BC
		public ParallelBitArray GetSubArray(int length)
		{
			return new ParallelBitArray
			{
				m_Bits = this.m_Bits.GetSubArray(0, (length + 63) / 64),
				m_Length = length
			};
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00013EF4 File Offset: 0x000120F4
		public NativeArray<long> GetBitsArray()
		{
			return this.m_Bits;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00013EFC File Offset: 0x000120FC
		public void FillZeroes(int length)
		{
			length = Math.Min(length, this.m_Length);
			int chunkIndex = length / 64;
			int remainder = length & 63;
			long num = 0L;
			(ref this.m_Bits).FillArray(in num, 0, chunkIndex);
			if (remainder > 0)
			{
				long lastChunkMask = (1L << remainder) - 1L;
				ref NativeArray<long> ptr = ref this.m_Bits;
				int num2 = chunkIndex;
				ptr[num2] &= ~lastChunkMask;
			}
		}

		// Token: 0x0400042B RID: 1067
		private Allocator m_Allocator;

		// Token: 0x0400042C RID: 1068
		private NativeArray<long> m_Bits;

		// Token: 0x0400042D RID: 1069
		private int m_Length;
	}
}
