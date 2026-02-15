using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.Jobs;
using Unity.Mathematics;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000117 RID: 279
	[DebuggerDisplay("Length = {Length}, Capacity = {Capacity}, IsCreated = {IsCreated}, IsEmpty = {IsEmpty}")]
	[DebuggerTypeProxy(typeof(UnsafeListTDebugView<>))]
	[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
	public struct UnsafeList<[global::System.Runtime.CompilerServices.IsUnmanaged] T> : INativeDisposable, IDisposable, INativeList<T>, IIndexable<T>, IEnumerable<T>, IEnumerable where T : struct, ValueType
	{
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x000238A3 File Offset: 0x00021AA3
		// (set) Token: 0x06000BAF RID: 2991 RVA: 0x000238B0 File Offset: 0x00021AB0
		public int Length
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return CollectionHelper.AssumePositive(this.m_length);
			}
			set
			{
				if (value > this.Capacity)
				{
					this.Resize(value, NativeArrayOptions.UninitializedMemory);
					return;
				}
				this.m_length = value;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x000238CB File Offset: 0x00021ACB
		// (set) Token: 0x06000BB1 RID: 2993 RVA: 0x000238D8 File Offset: 0x00021AD8
		public int Capacity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return CollectionHelper.AssumePositive(this.m_capacity);
			}
			set
			{
				this.SetCapacity(value);
			}
		}

		// Token: 0x1700015F RID: 351
		public unsafe T this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.Ptr[(IntPtr)CollectionHelper.AssumePositive(index) * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.Ptr[(IntPtr)CollectionHelper.AssumePositive(index) * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)] = value;
			}
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0002391A File Offset: 0x00021B1A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe ref T ElementAt(int index)
		{
			return ref this.Ptr[(IntPtr)CollectionHelper.AssumePositive(index) * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x00023931 File Offset: 0x00021B31
		public unsafe UnsafeList(T* ptr, int length)
		{
			this = default(UnsafeList<T>);
			this.Ptr = ptr;
			this.m_length = length;
			this.m_capacity = length;
			this.Allocator = AllocatorManager.None;
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0002395C File Offset: 0x00021B5C
		public unsafe UnsafeList(int initialCapacity, AllocatorManager.AllocatorHandle allocator, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory)
		{
			this.Ptr = null;
			this.m_length = 0;
			this.m_capacity = 0;
			this.Allocator = allocator;
			this.padding = 0;
			this.SetCapacity(math.max(initialCapacity, 1));
			if (options == NativeArrayOptions.ClearMemory && this.Ptr != null)
			{
				int sizeOf = sizeof(T);
				UnsafeUtility.MemClear((void*)this.Ptr, (long)(this.Capacity * sizeOf));
			}
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x000239C4 File Offset: 0x00021BC4
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(AllocatorManager.AllocatorHandle) })]
		internal unsafe static UnsafeList<T>* Create<[global::System.Runtime.CompilerServices.IsUnmanaged] U>(int initialCapacity, ref U allocator, NativeArrayOptions options) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			UnsafeList<T>* ptr = (ref allocator).Allocate(default(UnsafeList<T>), 1);
			*ptr = new UnsafeList<T>(initialCapacity, allocator.Handle, options);
			return ptr;
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x000239FA File Offset: 0x00021BFA
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(AllocatorManager.AllocatorHandle) })]
		internal unsafe static void Destroy<[global::System.Runtime.CompilerServices.IsUnmanaged] U>(UnsafeList<T>* listData, ref U allocator) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			listData->Dispose<U>(ref allocator);
			(ref allocator).Free((void*)listData, sizeof(UnsafeList<T>), UnsafeUtility.AlignOf<UnsafeList<T>>(), 1);
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x00023A16 File Offset: 0x00021C16
		public unsafe static UnsafeList<T>* Create(int initialCapacity, AllocatorManager.AllocatorHandle allocator, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory)
		{
			UnsafeList<T>* ptr = AllocatorManager.Allocate<UnsafeList<T>>(allocator, 1);
			*ptr = new UnsafeList<T>(initialCapacity, allocator, options);
			return ptr;
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x00023A2D File Offset: 0x00021C2D
		public unsafe static void Destroy(UnsafeList<T>* listData)
		{
			AllocatorManager.AllocatorHandle allocator = listData->Allocator;
			listData->Dispose();
			AllocatorManager.Free<UnsafeList<T>>(allocator, listData, 1);
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x00023A42 File Offset: 0x00021C42
		public readonly bool IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return !this.IsCreated || this.m_length == 0;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x00023A57 File Offset: 0x00021C57
		public readonly bool IsCreated
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.Ptr != null;
			}
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x00023A66 File Offset: 0x00021C66
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(AllocatorManager.AllocatorHandle) })]
		internal void Dispose<[global::System.Runtime.CompilerServices.IsUnmanaged] U>(ref U allocator) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			(ref allocator).Free(this.Ptr, this.m_capacity);
			this.Ptr = null;
			this.m_length = 0;
			this.m_capacity = 0;
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x00023A90 File Offset: 0x00021C90
		public void Dispose()
		{
			if (!this.IsCreated)
			{
				return;
			}
			if (CollectionHelper.ShouldDeallocate(this.Allocator))
			{
				AllocatorManager.Free<T>(this.Allocator, this.Ptr, this.m_capacity);
				this.Allocator = AllocatorManager.Invalid;
			}
			this.Ptr = null;
			this.m_length = 0;
			this.m_capacity = 0;
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x00023AEC File Offset: 0x00021CEC
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

		// Token: 0x06000BC0 RID: 3008 RVA: 0x00023B56 File Offset: 0x00021D56
		public void Clear()
		{
			this.m_length = 0;
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x00023B60 File Offset: 0x00021D60
		public unsafe void Resize(int length, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory)
		{
			int oldLength = this.m_length;
			if (length > this.Capacity)
			{
				this.SetCapacity(length);
			}
			this.m_length = length;
			if (options == NativeArrayOptions.ClearMemory && oldLength < length)
			{
				int num = length - oldLength;
				byte* ptr = (byte*)this.Ptr;
				int sizeOf = sizeof(T);
				UnsafeUtility.MemClear((void*)(ptr + oldLength * sizeOf), (long)(num * sizeOf));
			}
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x00023BB4 File Offset: 0x00021DB4
		private unsafe void ResizeExact<[global::System.Runtime.CompilerServices.IsUnmanaged] U>(ref U allocator, int newCapacity) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			newCapacity = math.max(0, newCapacity);
			T* newPointer = null;
			int alignOf = UnsafeUtility.AlignOf<T>();
			int sizeOf = sizeof(T);
			if (newCapacity > 0)
			{
				newPointer = (T*)(ref allocator).Allocate(sizeOf, alignOf, newCapacity);
				if (this.Ptr != null && this.m_capacity > 0)
				{
					int bytesToCopy = math.min(newCapacity, this.Capacity) * sizeOf;
					UnsafeUtility.MemCpy((void*)newPointer, (void*)this.Ptr, (long)bytesToCopy);
				}
			}
			(ref allocator).Free(this.Ptr, this.Capacity);
			this.Ptr = newPointer;
			this.m_capacity = newCapacity;
			this.m_length = math.min(this.m_length, newCapacity);
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x00023C4A File Offset: 0x00021E4A
		private void ResizeExact(int capacity)
		{
			this.ResizeExact<AllocatorManager.AllocatorHandle>(ref this.Allocator, capacity);
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x00023C5C File Offset: 0x00021E5C
		private void SetCapacity<[global::System.Runtime.CompilerServices.IsUnmanaged] U>(ref U allocator, int capacity) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			int sizeOf = sizeof(T);
			int newCapacity = math.max(capacity, 64 / sizeOf);
			newCapacity = math.ceilpow2(newCapacity);
			if (newCapacity == this.Capacity)
			{
				return;
			}
			this.ResizeExact<U>(ref allocator, newCapacity);
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x00023C94 File Offset: 0x00021E94
		public void SetCapacity(int capacity)
		{
			this.SetCapacity<AllocatorManager.AllocatorHandle>(ref this.Allocator, capacity);
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x00023CA3 File Offset: 0x00021EA3
		public void TrimExcess()
		{
			if (this.Capacity != this.m_length)
			{
				this.ResizeExact(this.m_length);
			}
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x00023CBF File Offset: 0x00021EBF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe void AddNoResize(T value)
		{
			UnsafeUtility.WriteArrayElement<T>((void*)this.Ptr, this.m_length, value);
			this.m_length++;
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x00023CE4 File Offset: 0x00021EE4
		public unsafe void AddRangeNoResize(void* ptr, int count)
		{
			int sizeOf = sizeof(T);
			void* dst = (void*)(this.Ptr + this.m_length * sizeOf / sizeof(T));
			UnsafeUtility.MemCpy(dst, ptr, (long)(count * sizeOf));
			this.m_length += count;
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x00023D21 File Offset: 0x00021F21
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe void AddRangeNoResize(UnsafeList<T> list)
		{
			this.AddRangeNoResize((void*)list.Ptr, CollectionHelper.AssumePositive(list.Length));
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x00023D3C File Offset: 0x00021F3C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe void Add(in T value)
		{
			int idx = this.m_length;
			if (this.m_length < this.m_capacity)
			{
				this.Ptr[(IntPtr)idx * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)] = value;
				this.m_length++;
				return;
			}
			this.Resize(idx + 1, NativeArrayOptions.UninitializedMemory);
			this.Ptr[(IntPtr)idx * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)] = value;
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x00023DB0 File Offset: 0x00021FB0
		public unsafe void AddRange(void* ptr, int count)
		{
			int idx = this.m_length;
			if (this.m_length + count > this.Capacity)
			{
				this.Resize(this.m_length + count, NativeArrayOptions.UninitializedMemory);
			}
			else
			{
				this.m_length += count;
			}
			int sizeOf = sizeof(T);
			void* dst = (void*)(this.Ptr + idx * sizeOf / sizeof(T));
			UnsafeUtility.MemCpy(dst, ptr, (long)(count * sizeOf));
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x00023E10 File Offset: 0x00022010
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe void AddRange(UnsafeList<T> list)
		{
			this.AddRange((void*)list.Ptr, list.Length);
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x00023E28 File Offset: 0x00022028
		public unsafe void AddReplicate(in T value, int count)
		{
			int idx = this.m_length;
			if (this.m_length + count > this.Capacity)
			{
				this.Resize(this.m_length + count, NativeArrayOptions.UninitializedMemory);
			}
			else
			{
				this.m_length += count;
			}
			fixed (T* ptr2 = &value)
			{
				void* ptr = (void*)ptr2;
				UnsafeUtility.MemCpyReplicate((void*)(this.Ptr + (IntPtr)idx * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)), ptr, UnsafeUtility.SizeOf<T>(), count);
			}
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x00023E90 File Offset: 0x00022090
		public unsafe void InsertRangeWithBeginEnd(int begin, int end)
		{
			begin = CollectionHelper.AssumePositive(begin);
			end = CollectionHelper.AssumePositive(end);
			int items = end - begin;
			if (items < 1)
			{
				return;
			}
			int length = this.m_length;
			if (this.m_length + items > this.Capacity)
			{
				this.Resize(this.m_length + items, NativeArrayOptions.UninitializedMemory);
			}
			else
			{
				this.m_length += items;
			}
			int itemsToCopy = length - begin;
			if (itemsToCopy < 1)
			{
				return;
			}
			int sizeOf = sizeof(T);
			int bytesToCopy = itemsToCopy * sizeOf;
			byte* ptr = (byte*)this.Ptr;
			void* ptr2 = (void*)(ptr + end * sizeOf);
			byte* src = ptr + begin * sizeOf;
			UnsafeUtility.MemMove(ptr2, (void*)src, (long)bytesToCopy);
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x00023F1D File Offset: 0x0002211D
		public void InsertRange(int index, int count)
		{
			this.InsertRangeWithBeginEnd(index, index + count);
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x00023F2C File Offset: 0x0002212C
		public unsafe void RemoveAtSwapBack(int index)
		{
			index = CollectionHelper.AssumePositive(index);
			int copyFrom = this.m_length - 1;
			ref T ptr = ref this.Ptr[(IntPtr)index * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
			T* src = this.Ptr + (IntPtr)copyFrom * (IntPtr)sizeof(T) / (IntPtr)sizeof(T);
			ptr = *src;
			this.m_length--;
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x00023F84 File Offset: 0x00022184
		public unsafe void RemoveRangeSwapBack(int index, int count)
		{
			index = CollectionHelper.AssumePositive(index);
			count = CollectionHelper.AssumePositive(count);
			if (count > 0)
			{
				int copyFrom = math.max(this.m_length - count, index + count);
				int sizeOf = sizeof(T);
				void* dst = (void*)(this.Ptr + index * sizeOf / sizeof(T));
				void* src = (void*)(this.Ptr + copyFrom * sizeOf / sizeof(T));
				UnsafeUtility.MemCpy(dst, src, (long)((this.m_length - copyFrom) * sizeOf));
				this.m_length -= count;
			}
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x00023FF4 File Offset: 0x000221F4
		public unsafe void RemoveAt(int index)
		{
			index = CollectionHelper.AssumePositive(index);
			T* dst = this.Ptr + (IntPtr)index * (IntPtr)sizeof(T) / (IntPtr)sizeof(T);
			T* src = dst + sizeof(T) / sizeof(T);
			this.m_length--;
			for (int i = index; i < this.m_length; i++)
			{
				T* ptr = dst;
				dst = ptr + sizeof(T) / sizeof(T);
				T* ptr2 = src;
				src = ptr2 + sizeof(T) / sizeof(T);
				*ptr = *ptr2;
			}
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x00024060 File Offset: 0x00022260
		public unsafe void RemoveRange(int index, int count)
		{
			index = CollectionHelper.AssumePositive(index);
			count = CollectionHelper.AssumePositive(count);
			if (count > 0)
			{
				int copyFrom = math.min(index + count, this.m_length);
				int sizeOf = sizeof(T);
				void* dst = (void*)(this.Ptr + index * sizeOf / sizeof(T));
				void* src = (void*)(this.Ptr + copyFrom * sizeOf / sizeof(T));
				UnsafeUtility.MemCpy(dst, src, (long)((this.m_length - copyFrom) * sizeOf));
				this.m_length -= count;
			}
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x000240CD File Offset: 0x000222CD
		public UnsafeList<T>.ReadOnly AsReadOnly()
		{
			return new UnsafeList<T>.ReadOnly(this.Ptr, this.Length);
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x000240E0 File Offset: 0x000222E0
		public UnsafeList<T>.ParallelReader AsParallelReader()
		{
			return new UnsafeList<T>.ParallelReader(this.Ptr, this.Length);
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x000240F3 File Offset: 0x000222F3
		public unsafe UnsafeList<T>.ParallelWriter AsParallelWriter()
		{
			return new UnsafeList<T>.ParallelWriter((UnsafeList<T>*)UnsafeUtility.AddressOf<UnsafeList<T>>(ref this));
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x00024100 File Offset: 0x00022300
		public unsafe void CopyFrom(in NativeArray<T> other)
		{
			NativeArray<T> nativeArray = other;
			this.Resize(nativeArray.Length, NativeArrayOptions.UninitializedMemory);
			void* ptr = (void*)this.Ptr;
			void* unsafeReadOnlyPtr = other.GetUnsafeReadOnlyPtr<T>();
			int num = UnsafeUtility.SizeOf<T>();
			nativeArray = other;
			UnsafeUtility.MemCpy(ptr, unsafeReadOnlyPtr, (long)(num * nativeArray.Length));
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x0002414D File Offset: 0x0002234D
		public unsafe void CopyFrom(in UnsafeList<T> other)
		{
			this.Resize(other.Length, NativeArrayOptions.UninitializedMemory);
			UnsafeUtility.MemCpy((void*)this.Ptr, (void*)other.Ptr, (long)(UnsafeUtility.SizeOf<T>() * other.Length));
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x0002417C File Offset: 0x0002237C
		public UnsafeList<T>.Enumerator GetEnumerator()
		{
			return new UnsafeList<T>.Enumerator
			{
				m_Ptr = this.Ptr,
				m_Length = this.Length,
				m_Index = -1
			};
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x000078E7 File Offset: 0x00005AE7
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x000078E7 File Offset: 0x00005AE7
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x000241B4 File Offset: 0x000223B4
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		internal unsafe static void CheckNull(void* listData)
		{
			if (listData == null)
			{
				throw new InvalidOperationException("UnsafeList has yet to be created or has been destroyed!");
			}
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x000241C8 File Offset: 0x000223C8
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private void CheckIndexCount(int index, int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException(string.Format("Value for count {0} must be positive.", count));
			}
			if (index < 0)
			{
				throw new IndexOutOfRangeException(string.Format("Value for index {0} must be positive.", index));
			}
			if (index > this.Length)
			{
				throw new IndexOutOfRangeException(string.Format("Value for index {0} is out of bounds.", index));
			}
			if (index + count > this.Length)
			{
				throw new ArgumentOutOfRangeException(string.Format("Value for count {0} is out of bounds.", count));
			}
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x00024249 File Offset: 0x00022449
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private void CheckBeginEndNoLength(int begin, int end)
		{
			if (begin > end)
			{
				throw new ArgumentException(string.Format("Value for begin {0} index must less or equal to end {1}.", begin, end));
			}
			if (begin < 0)
			{
				throw new ArgumentOutOfRangeException(string.Format("Value for begin {0} must be positive.", begin));
			}
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x00024285 File Offset: 0x00022485
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private void CheckBeginEnd(int begin, int end)
		{
			if (begin > this.Length)
			{
				throw new ArgumentOutOfRangeException(string.Format("Value for begin {0} is out of bounds.", begin));
			}
			if (end > this.Length)
			{
				throw new ArgumentOutOfRangeException(string.Format("Value for end {0} is out of bounds.", end));
			}
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x00002C47 File Offset: 0x00000E47
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void CheckNoResizeHasEnoughCapacity(int length)
		{
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x000242C5 File Offset: 0x000224C5
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void CheckNoResizeHasEnoughCapacity(int length, int index)
		{
			if (this.Capacity < index + length)
			{
				throw new InvalidOperationException(string.Format("AddNoResize assumes that list capacity is sufficient (Capacity {0}, Length {1}), requested length {2}!", this.Capacity, this.Length, length));
			}
		}

		// Token: 0x040004CE RID: 1230
		[NativeDisableUnsafePtrRestriction]
		public unsafe T* Ptr;

		// Token: 0x040004CF RID: 1231
		public int m_length;

		// Token: 0x040004D0 RID: 1232
		public int m_capacity;

		// Token: 0x040004D1 RID: 1233
		public AllocatorManager.AllocatorHandle Allocator;

		// Token: 0x040004D2 RID: 1234
		private readonly int padding;

		// Token: 0x02000118 RID: 280
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public struct ReadOnly : IEnumerable<T>, IEnumerable
		{
			// Token: 0x17000162 RID: 354
			// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x000242FE File Offset: 0x000224FE
			public readonly bool IsCreated
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.Ptr != null;
				}
			}

			// Token: 0x17000163 RID: 355
			// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x0002430D File Offset: 0x0002250D
			public readonly bool IsEmpty
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return !this.IsCreated || this.Length == 0;
				}
			}

			// Token: 0x06000BE4 RID: 3044 RVA: 0x00024322 File Offset: 0x00022522
			internal unsafe ReadOnly(T* ptr, int length)
			{
				this.Ptr = ptr;
				this.Length = length;
			}

			// Token: 0x06000BE5 RID: 3045 RVA: 0x00024334 File Offset: 0x00022534
			public UnsafeList<T>.Enumerator GetEnumerator()
			{
				return new UnsafeList<T>.Enumerator
				{
					m_Ptr = this.Ptr,
					m_Length = this.Length,
					m_Index = -1
				};
			}

			// Token: 0x06000BE6 RID: 3046 RVA: 0x000078E7 File Offset: 0x00005AE7
			IEnumerator IEnumerable.GetEnumerator()
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000BE7 RID: 3047 RVA: 0x000078E7 File Offset: 0x00005AE7
			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				throw new NotImplementedException();
			}

			// Token: 0x040004D3 RID: 1235
			[NativeDisableUnsafePtrRestriction]
			public unsafe readonly T* Ptr;

			// Token: 0x040004D4 RID: 1236
			public readonly int Length;
		}

		// Token: 0x02000119 RID: 281
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public struct ParallelReader
		{
			// Token: 0x06000BE8 RID: 3048 RVA: 0x0002436C File Offset: 0x0002256C
			internal unsafe ParallelReader(T* ptr, int length)
			{
				this.Ptr = ptr;
				this.Length = length;
			}

			// Token: 0x040004D5 RID: 1237
			[NativeDisableUnsafePtrRestriction]
			public unsafe readonly T* Ptr;

			// Token: 0x040004D6 RID: 1238
			public readonly int Length;
		}

		// Token: 0x0200011A RID: 282
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public struct ParallelWriter
		{
			// Token: 0x17000164 RID: 356
			// (get) Token: 0x06000BE9 RID: 3049 RVA: 0x0002437C File Offset: 0x0002257C
			public unsafe readonly void* Ptr
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return (void*)this.ListData->Ptr;
				}
			}

			// Token: 0x06000BEA RID: 3050 RVA: 0x00024389 File Offset: 0x00022589
			internal unsafe ParallelWriter(UnsafeList<T>* listData)
			{
				this.ListData = listData;
			}

			// Token: 0x06000BEB RID: 3051 RVA: 0x00024394 File Offset: 0x00022594
			[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
			public unsafe void AddNoResize(T value)
			{
				int idx = Interlocked.Increment(ref this.ListData->m_length) - 1;
				UnsafeUtility.WriteArrayElement<T>((void*)this.ListData->Ptr, idx, value);
			}

			// Token: 0x06000BEC RID: 3052 RVA: 0x000243C8 File Offset: 0x000225C8
			[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
			public unsafe void AddRangeNoResize(void* ptr, int count)
			{
				int idx = Interlocked.Add(ref this.ListData->m_length, count) - count;
				void* dst = (void*)(this.ListData->Ptr + idx * sizeof(T) / sizeof(T));
				UnsafeUtility.MemCpy(dst, ptr, (long)(count * sizeof(T)));
			}

			// Token: 0x06000BED RID: 3053 RVA: 0x0002440E File Offset: 0x0002260E
			[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
			public unsafe void AddRangeNoResize(UnsafeList<T> list)
			{
				this.AddRangeNoResize((void*)list.Ptr, list.Length);
			}

			// Token: 0x040004D7 RID: 1239
			[NativeDisableUnsafePtrRestriction]
			public unsafe UnsafeList<T>* ListData;
		}

		// Token: 0x0200011B RID: 283
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x06000BEE RID: 3054 RVA: 0x00002C47 File Offset: 0x00000E47
			public void Dispose()
			{
			}

			// Token: 0x06000BEF RID: 3055 RVA: 0x00024424 File Offset: 0x00022624
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				int num = this.m_Index + 1;
				this.m_Index = num;
				return num < this.m_Length;
			}

			// Token: 0x06000BF0 RID: 3056 RVA: 0x0002444A File Offset: 0x0002264A
			public void Reset()
			{
				this.m_Index = -1;
			}

			// Token: 0x17000165 RID: 357
			// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x00024453 File Offset: 0x00022653
			public unsafe T Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.m_Ptr[(IntPtr)this.m_Index * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
				}
			}

			// Token: 0x17000166 RID: 358
			// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x0002446F File Offset: 0x0002266F
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x040004D8 RID: 1240
			internal unsafe T* m_Ptr;

			// Token: 0x040004D9 RID: 1241
			internal int m_Length;

			// Token: 0x040004DA RID: 1242
			internal int m_Index;
		}
	}
}
