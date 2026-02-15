using System;
using System.Diagnostics;
using Unity.Jobs;
using Unity.Mathematics;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000108 RID: 264
	[DebuggerDisplay("Length = {Length}, IsCreated = {IsCreated}")]
	[DebuggerTypeProxy(typeof(UnsafeBitArrayDebugView))]
	[GenerateTestsForBurstCompatibility]
	public struct UnsafeBitArray : INativeDisposable, IDisposable
	{
		// Token: 0x06000B0A RID: 2826 RVA: 0x00021CF0 File Offset: 0x0001FEF0
		public unsafe UnsafeBitArray(void* ptr, int sizeInBytes, AllocatorManager.AllocatorHandle allocator = default(AllocatorManager.AllocatorHandle))
		{
			this.Ptr = (ulong*)ptr;
			this.Length = sizeInBytes * 8;
			this.Capacity = sizeInBytes * 8;
			this.Allocator = allocator;
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00021D12 File Offset: 0x0001FF12
		public UnsafeBitArray(int numBits, AllocatorManager.AllocatorHandle allocator, NativeArrayOptions options = NativeArrayOptions.ClearMemory)
		{
			this.Allocator = allocator;
			this.Ptr = null;
			this.Length = 0;
			this.Capacity = 0;
			this.Resize(numBits, options);
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x00021D3C File Offset: 0x0001FF3C
		internal unsafe static UnsafeBitArray* Alloc(AllocatorManager.AllocatorHandle allocator)
		{
			return (UnsafeBitArray*)Memory.Unmanaged.Allocate((long)sizeof(UnsafeBitArray), UnsafeUtility.AlignOf<UnsafeBitArray>(), allocator);
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x00021D5D File Offset: 0x0001FF5D
		internal unsafe static void Free(UnsafeBitArray* data, AllocatorManager.AllocatorHandle allocator)
		{
			if (data == null)
			{
				throw new InvalidOperationException("UnsafeBitArray has yet to be created or has been destroyed!");
			}
			data->Dispose();
			Memory.Unmanaged.Free<UnsafeBitArray>(data, allocator);
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x00021D7C File Offset: 0x0001FF7C
		public readonly bool IsCreated
		{
			get
			{
				return this.Ptr != null;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x00021D8B File Offset: 0x0001FF8B
		public readonly bool IsEmpty
		{
			get
			{
				return !this.IsCreated || this.Length == 0;
			}
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x00021DA0 File Offset: 0x0001FFA0
		private unsafe void Realloc(int capacityInBits)
		{
			int newCapacity = Bitwise.AlignUp(capacityInBits, 64);
			int sizeInBytes = newCapacity / 8;
			ulong* newPointer = null;
			if (sizeInBytes > 0)
			{
				newPointer = (ulong*)Memory.Unmanaged.Allocate((long)sizeInBytes, 16, this.Allocator);
				if (this.Capacity > 0)
				{
					int bytesToCopy = math.min(newCapacity, this.Capacity) / 8;
					UnsafeUtility.MemCpy((void*)newPointer, (void*)this.Ptr, (long)bytesToCopy);
				}
			}
			Memory.Unmanaged.Free<ulong>(this.Ptr, this.Allocator);
			this.Ptr = newPointer;
			this.Capacity = newCapacity;
			this.Length = math.min(this.Length, newCapacity);
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00021E28 File Offset: 0x00020028
		public void Resize(int numBits, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory)
		{
			int minCapacity = math.max(numBits, 1);
			if (minCapacity > this.Capacity)
			{
				this.SetCapacity(minCapacity);
			}
			int oldLength = this.Length;
			this.Length = numBits;
			if (options == NativeArrayOptions.ClearMemory && oldLength < this.Length)
			{
				this.SetBits(oldLength, false, this.Length - oldLength);
			}
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x00021E78 File Offset: 0x00020078
		public void SetCapacity(int capacityInBits)
		{
			if (this.Capacity == capacityInBits)
			{
				return;
			}
			this.Realloc(capacityInBits);
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x00021E8B File Offset: 0x0002008B
		public void TrimExcess()
		{
			this.SetCapacity(this.Length);
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x00021E9C File Offset: 0x0002009C
		public void Dispose()
		{
			if (!this.IsCreated)
			{
				return;
			}
			if (CollectionHelper.ShouldDeallocate(this.Allocator))
			{
				Memory.Unmanaged.Free<ulong>(this.Ptr, this.Allocator);
				this.Allocator = AllocatorManager.Invalid;
			}
			this.Ptr = null;
			this.Length = 0;
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x00021EEC File Offset: 0x000200EC
		public unsafe JobHandle Dispose(JobHandle inputDeps)
		{
			if (!this.IsCreated)
			{
				return inputDeps;
			}
			if (CollectionHelper.ShouldDeallocate(this.Allocator))
			{
				JobHandle jobHandle = new UnsafeDisposeJob
				{
					Ptr = (void*)this.Ptr,
					Allocator = this.Allocator
				}.Schedule(inputDeps);
				this.Ptr = null;
				this.Allocator = AllocatorManager.Invalid;
				return jobHandle;
			}
			this.Ptr = null;
			return inputDeps;
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x00021F58 File Offset: 0x00020158
		public unsafe void Clear()
		{
			int sizeInBytes = Bitwise.AlignUp(this.Length, 64) / 8;
			UnsafeUtility.MemClear((void*)this.Ptr, (long)sizeInBytes);
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x00021F84 File Offset: 0x00020184
		public unsafe static void Set(ulong* ptr, int pos, bool value)
		{
			int idx = pos >> 6;
			int shift = pos & 63;
			ulong mask = 1UL << shift;
			ulong bits = (ptr[idx] & ~mask) | (ulong)((long)(-(long)Bitwise.FromBool(value)) & (long)mask);
			ptr[idx] = bits;
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00021FC0 File Offset: 0x000201C0
		public void Set(int pos, bool value)
		{
			UnsafeBitArray.Set(this.Ptr, pos, value);
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x00021FD0 File Offset: 0x000201D0
		public unsafe void SetBits(int pos, bool value, int numBits)
		{
			int num = math.min(pos + numBits, this.Length);
			int idxB = pos >> 6;
			int shiftB = pos & 63;
			int idxE = num - 1 >> 6;
			int shiftE = num & 63;
			ulong maskB = ulong.MaxValue << shiftB;
			ulong maskE = ulong.MaxValue >> 64 - shiftE;
			ulong orBits = (ulong)((long)(-(long)Bitwise.FromBool(value)));
			ulong orBitsB = maskB & orBits;
			ulong orBitsE = maskE & orBits;
			ulong cmaskB = ~maskB;
			ulong cmaskE = ~maskE;
			if (idxB == idxE)
			{
				ulong cmaskBE = ~(maskB & maskE);
				ulong orBitsBE = orBitsB & orBitsE;
				this.Ptr[idxB] = (this.Ptr[idxB] & cmaskBE) | orBitsBE;
				return;
			}
			this.Ptr[idxB] = (this.Ptr[idxB] & cmaskB) | orBitsB;
			for (int idx = idxB + 1; idx < idxE; idx++)
			{
				this.Ptr[idx] = orBits;
			}
			this.Ptr[idxE] = (this.Ptr[idxE] & cmaskE) | orBitsE;
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x000220C4 File Offset: 0x000202C4
		public unsafe void SetBits(int pos, ulong value, int numBits = 1)
		{
			int idxB = pos >> 6;
			int shiftB = pos & 63;
			if (shiftB + numBits <= 64)
			{
				ulong mask = ulong.MaxValue >> 64 - numBits;
				this.Ptr[idxB] = Bitwise.ReplaceBits(this.Ptr[idxB], shiftB, mask, value);
				return;
			}
			int num = math.min(pos + numBits, this.Length);
			int idxE = num - 1 >> 6;
			int shiftE = num & 63;
			ulong maskB = ulong.MaxValue >> shiftB;
			this.Ptr[idxB] = Bitwise.ReplaceBits(this.Ptr[idxB], shiftB, maskB, value);
			ulong valueE = value >> 64 - shiftB;
			ulong maskE = ulong.MaxValue >> 64 - shiftE;
			this.Ptr[idxE] = Bitwise.ReplaceBits(this.Ptr[idxE], 0, maskE, valueE);
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0002218A File Offset: 0x0002038A
		public ulong GetBits(int pos, int numBits = 1)
		{
			return Bitwise.GetBits(this.Ptr, this.Length, pos, numBits);
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0002219F File Offset: 0x0002039F
		public bool IsSet(int pos)
		{
			return Bitwise.IsSet(this.Ptr, pos);
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x000221AD File Offset: 0x000203AD
		internal void CopyUlong(int dstPos, ref UnsafeBitArray srcBitArray, int srcPos, int numBits)
		{
			this.SetBits(dstPos, srcBitArray.GetBits(srcPos, numBits), numBits);
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x000221C1 File Offset: 0x000203C1
		public void Copy(int dstPos, int srcPos, int numBits)
		{
			if (dstPos == srcPos)
			{
				return;
			}
			this.Copy(dstPos, ref this, srcPos, numBits);
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x000221D4 File Offset: 0x000203D4
		public unsafe void Copy(int dstPos, ref UnsafeBitArray srcBitArray, int srcPos, int numBits)
		{
			if (numBits == 0)
			{
				return;
			}
			if (numBits <= 64)
			{
				this.CopyUlong(dstPos, ref srcBitArray, srcPos, numBits);
				return;
			}
			if (numBits <= 128)
			{
				this.CopyUlong(dstPos, ref srcBitArray, srcPos, 64);
				numBits -= 64;
				if (numBits > 0)
				{
					this.CopyUlong(dstPos + 64, ref srcBitArray, srcPos + 64, numBits);
					return;
				}
			}
			else if ((dstPos & 7) == (srcPos & 7))
			{
				int dstPosInBytes = CollectionHelper.Align(dstPos, 8) >> 3;
				int srcPosInBytes = CollectionHelper.Align(srcPos, 8) >> 3;
				int numPreBits = dstPosInBytes * 8 - dstPos;
				if (numPreBits > 0)
				{
					this.CopyUlong(dstPos, ref srcBitArray, srcPos, numPreBits);
				}
				int num = numBits - numPreBits;
				int numBytes = num / 8;
				if (numBytes > 0)
				{
					UnsafeUtility.MemMove((void*)(this.Ptr + dstPosInBytes / 8), (void*)(srcBitArray.Ptr + srcPosInBytes / 8), (long)numBytes);
				}
				int numPostBits = num & 7;
				if (numPostBits > 0)
				{
					this.CopyUlong((dstPosInBytes + numBytes) * 8, ref srcBitArray, (srcPosInBytes + numBytes) * 8, numPostBits);
					return;
				}
			}
			else
			{
				int numPreBits2 = CollectionHelper.Align(dstPos, 64) - dstPos;
				if (numPreBits2 > 0)
				{
					this.CopyUlong(dstPos, ref srcBitArray, srcPos, numPreBits2);
					numBits -= numPreBits2;
					dstPos += numPreBits2;
					srcPos += numPreBits2;
				}
				while (numBits >= 64)
				{
					this.Ptr[dstPos >> 6] = srcBitArray.GetBits(srcPos, 64);
					numBits -= 64;
					dstPos += 64;
					srcPos += 64;
				}
				if (numBits > 0)
				{
					this.CopyUlong(dstPos, ref srcBitArray, srcPos, numBits);
				}
			}
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x00022314 File Offset: 0x00020514
		public int Find(int pos, int numBits)
		{
			int count = this.Length - pos;
			return Bitwise.Find(this.Ptr, pos, count, numBits);
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x00022338 File Offset: 0x00020538
		public int Find(int pos, int count, int numBits)
		{
			return Bitwise.Find(this.Ptr, pos, count, numBits);
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00022348 File Offset: 0x00020548
		public bool TestNone(int pos, int numBits = 1)
		{
			return Bitwise.TestNone(this.Ptr, this.Length, pos, numBits);
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0002235D File Offset: 0x0002055D
		public bool TestAny(int pos, int numBits = 1)
		{
			return Bitwise.TestAny(this.Ptr, this.Length, pos, numBits);
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00022372 File Offset: 0x00020572
		public bool TestAll(int pos, int numBits = 1)
		{
			return Bitwise.TestAll(this.Ptr, this.Length, pos, numBits);
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00022387 File Offset: 0x00020587
		public int CountBits(int pos, int numBits = 1)
		{
			return Bitwise.CountBits(this.Ptr, this.Length, pos, numBits);
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0002239C File Offset: 0x0002059C
		public UnsafeBitArray.ReadOnly AsReadOnly()
		{
			return new UnsafeBitArray.ReadOnly(this.Ptr, this.Length);
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x000223AF File Offset: 0x000205AF
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckSizeMultipleOf8(int sizeInBytes)
		{
			if ((sizeInBytes & 7) != 0)
			{
				throw new ArgumentException(string.Format("BitArray invalid arguments: sizeInBytes {0} (must be multiple of 8-bytes, sizeInBytes: {1}).", sizeInBytes, sizeInBytes));
			}
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x000223D2 File Offset: 0x000205D2
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private void CheckArgs(int pos, int numBits)
		{
			if (pos < 0 || pos >= this.Length || numBits < 1)
			{
				throw new ArgumentException(string.Format("BitArray invalid arguments: pos {0} (must be 0-{1}), numBits {2} (must be greater than 0).", pos, this.Length - 1, numBits));
			}
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00022410 File Offset: 0x00020610
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private void CheckArgsPosCount(int begin, int count, int numBits)
		{
			if (begin < 0 || begin >= this.Length)
			{
				throw new ArgumentException(string.Format("BitArray invalid argument: begin {0} (must be 0-{1}).", begin, this.Length - 1));
			}
			if (count < 0 || count > this.Length)
			{
				throw new ArgumentException(string.Format("BitArray invalid argument: count {0} (must be 0-{1}).", count, this.Length));
			}
			if (numBits < 1 || count < numBits)
			{
				throw new ArgumentException(string.Format("BitArray invalid argument: numBits {0} (must be greater than 0).", numBits));
			}
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0002249C File Offset: 0x0002069C
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private void CheckArgsUlong(int pos, int numBits)
		{
			if (numBits < 1 || numBits > 64)
			{
				throw new ArgumentException(string.Format("BitArray invalid arguments: numBits {0} (must be 1-64).", numBits));
			}
			if (pos + numBits > this.Length)
			{
				throw new ArgumentException(string.Format("BitArray invalid arguments: Out of bounds pos {0}, numBits {1}, Length {2}.", pos, numBits, this.Length));
			}
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x000224FC File Offset: 0x000206FC
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckArgsCopy(ref UnsafeBitArray dstBitArray, int dstPos, ref UnsafeBitArray srcBitArray, int srcPos, int numBits)
		{
			if (srcPos + numBits > srcBitArray.Length)
			{
				throw new ArgumentException(string.Format("BitArray invalid arguments: Out of bounds - source position {0}, numBits {1}, source bit array Length {2}.", srcPos, numBits, srcBitArray.Length));
			}
			if (dstPos + numBits > dstBitArray.Length)
			{
				throw new ArgumentException(string.Format("BitArray invalid arguments: Out of bounds - destination position {0}, numBits {1}, destination bit array Length {2}.", dstPos, numBits, dstBitArray.Length));
			}
		}

		// Token: 0x040004A7 RID: 1191
		[NativeDisableUnsafePtrRestriction]
		public unsafe ulong* Ptr;

		// Token: 0x040004A8 RID: 1192
		public int Length;

		// Token: 0x040004A9 RID: 1193
		public int Capacity;

		// Token: 0x040004AA RID: 1194
		public AllocatorManager.AllocatorHandle Allocator;

		// Token: 0x02000109 RID: 265
		public struct ReadOnly
		{
			// Token: 0x17000140 RID: 320
			// (get) Token: 0x06000B2C RID: 2860 RVA: 0x00022571 File Offset: 0x00020771
			public readonly bool IsCreated
			{
				get
				{
					return this.Ptr != null;
				}
			}

			// Token: 0x17000141 RID: 321
			// (get) Token: 0x06000B2D RID: 2861 RVA: 0x00022580 File Offset: 0x00020780
			public readonly bool IsEmpty
			{
				get
				{
					return !this.IsCreated || this.Length == 0;
				}
			}

			// Token: 0x06000B2E RID: 2862 RVA: 0x00022595 File Offset: 0x00020795
			internal unsafe ReadOnly(ulong* ptr, int length)
			{
				this.Ptr = ptr;
				this.Length = length;
			}

			// Token: 0x06000B2F RID: 2863 RVA: 0x000225A5 File Offset: 0x000207A5
			public readonly ulong GetBits(int pos, int numBits = 1)
			{
				return Bitwise.GetBits(this.Ptr, this.Length, pos, numBits);
			}

			// Token: 0x06000B30 RID: 2864 RVA: 0x000225BA File Offset: 0x000207BA
			public readonly bool IsSet(int pos)
			{
				return Bitwise.IsSet(this.Ptr, pos);
			}

			// Token: 0x06000B31 RID: 2865 RVA: 0x000225C8 File Offset: 0x000207C8
			public readonly int Find(int pos, int numBits)
			{
				int count = this.Length - pos;
				return Bitwise.Find(this.Ptr, pos, count, numBits);
			}

			// Token: 0x06000B32 RID: 2866 RVA: 0x000225EC File Offset: 0x000207EC
			public readonly int Find(int pos, int count, int numBits)
			{
				return Bitwise.Find(this.Ptr, pos, count, numBits);
			}

			// Token: 0x06000B33 RID: 2867 RVA: 0x000225FC File Offset: 0x000207FC
			public readonly bool TestNone(int pos, int numBits = 1)
			{
				return Bitwise.TestNone(this.Ptr, pos, numBits, 1);
			}

			// Token: 0x06000B34 RID: 2868 RVA: 0x0002260C File Offset: 0x0002080C
			public readonly bool TestAny(int pos, int numBits = 1)
			{
				return Bitwise.TestAny(this.Ptr, this.Length, pos, numBits);
			}

			// Token: 0x06000B35 RID: 2869 RVA: 0x00022621 File Offset: 0x00020821
			public readonly bool TestAll(int pos, int numBits = 1)
			{
				return Bitwise.TestAll(this.Ptr, this.Length, pos, numBits);
			}

			// Token: 0x06000B36 RID: 2870 RVA: 0x00022636 File Offset: 0x00020836
			public readonly int CountBits(int pos, int numBits = 1)
			{
				return Bitwise.CountBits(this.Ptr, this.Length, pos, numBits);
			}

			// Token: 0x06000B37 RID: 2871 RVA: 0x0002264B File Offset: 0x0002084B
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[Conditional("UNITY_DOTS_DEBUG")]
			private readonly void CheckArgs(int pos, int numBits)
			{
				if (pos < 0 || pos >= this.Length || numBits < 1)
				{
					throw new ArgumentException(string.Format("BitArray invalid arguments: pos {0} (must be 0-{1}), numBits {2} (must be greater than 0).", pos, this.Length - 1, numBits));
				}
			}

			// Token: 0x06000B38 RID: 2872 RVA: 0x00022688 File Offset: 0x00020888
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[Conditional("UNITY_DOTS_DEBUG")]
			private readonly void CheckArgsPosCount(int begin, int count, int numBits)
			{
				if (begin < 0 || begin >= this.Length)
				{
					throw new ArgumentException(string.Format("BitArray invalid argument: begin {0} (must be 0-{1}).", begin, this.Length - 1));
				}
				if (count < 0 || count > this.Length)
				{
					throw new ArgumentException(string.Format("BitArray invalid argument: count {0} (must be 0-{1}).", count, this.Length));
				}
				if (numBits < 1 || count < numBits)
				{
					throw new ArgumentException(string.Format("BitArray invalid argument: numBits {0} (must be greater than 0).", numBits));
				}
			}

			// Token: 0x06000B39 RID: 2873 RVA: 0x00022714 File Offset: 0x00020914
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[Conditional("UNITY_DOTS_DEBUG")]
			private readonly void CheckArgsUlong(int pos, int numBits)
			{
				if (numBits < 1 || numBits > 64)
				{
					throw new ArgumentException(string.Format("BitArray invalid arguments: numBits {0} (must be 1-64).", numBits));
				}
				if (pos + numBits > this.Length)
				{
					throw new ArgumentException(string.Format("BitArray invalid arguments: Out of bounds pos {0}, numBits {1}, Length {2}.", pos, numBits, this.Length));
				}
			}

			// Token: 0x040004AB RID: 1195
			[NativeDisableUnsafePtrRestriction]
			public unsafe readonly ulong* Ptr;

			// Token: 0x040004AC RID: 1196
			public readonly int Length;
		}
	}
}
