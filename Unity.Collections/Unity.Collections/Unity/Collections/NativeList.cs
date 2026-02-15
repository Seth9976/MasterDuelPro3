using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x0200009A RID: 154
	[NativeContainer]
	[DebuggerDisplay("Length = {m_ListData == null ? default : m_ListData->Length}, Capacity = {m_ListData == null ? default : m_ListData->Capacity}")]
	[DebuggerTypeProxy(typeof(NativeListDebugView<>))]
	[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
	public struct NativeList<[global::System.Runtime.CompilerServices.IsUnmanaged] T> : INativeDisposable, IDisposable, INativeList<T>, IIndexable<T>, IEnumerable<T>, IEnumerable where T : struct, ValueType
	{
		// Token: 0x06000786 RID: 1926 RVA: 0x00017E04 File Offset: 0x00016004
		public NativeList(AllocatorManager.AllocatorHandle allocator)
		{
			this = new NativeList<T>(1, allocator);
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x00017E10 File Offset: 0x00016010
		public NativeList(int initialCapacity, AllocatorManager.AllocatorHandle allocator)
		{
			this = default(NativeList<T>);
			AllocatorManager.AllocatorHandle temp = allocator;
			this.Initialize<AllocatorManager.AllocatorHandle>(initialCapacity, ref temp);
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00017E2F File Offset: 0x0001602F
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(AllocatorManager.AllocatorHandle) })]
		internal void Initialize<[global::System.Runtime.CompilerServices.IsUnmanaged] U>(int initialCapacity, ref U allocator) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			this.m_ListData = UnsafeList<T>.Create<U>(initialCapacity, ref allocator, NativeArrayOptions.UninitializedMemory);
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00017E40 File Offset: 0x00016040
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(AllocatorManager.AllocatorHandle) })]
		internal static NativeList<T> New<[global::System.Runtime.CompilerServices.IsUnmanaged] U>(int initialCapacity, ref U allocator) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			NativeList<T> nativelist = default(NativeList<T>);
			nativelist.Initialize<U>(initialCapacity, ref allocator);
			return nativelist;
		}

		// Token: 0x170000D0 RID: 208
		public unsafe T this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return (*this.m_ListData)[index];
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				(*this.m_ListData)[index] = value;
			}
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00017E7C File Offset: 0x0001607C
		public unsafe ref T ElementAt(int index)
		{
			return this.m_ListData->ElementAt(index);
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600078D RID: 1933 RVA: 0x00017E8A File Offset: 0x0001608A
		// (set) Token: 0x0600078E RID: 1934 RVA: 0x00017E9C File Offset: 0x0001609C
		public unsafe int Length
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return CollectionHelper.AssumePositive(this.m_ListData->Length);
			}
			set
			{
				this.m_ListData->Resize(value, NativeArrayOptions.ClearMemory);
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600078F RID: 1935 RVA: 0x00017EAB File Offset: 0x000160AB
		// (set) Token: 0x06000790 RID: 1936 RVA: 0x00017EB8 File Offset: 0x000160B8
		public unsafe int Capacity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return this.m_ListData->Capacity;
			}
			set
			{
				this.m_ListData->Capacity = value;
			}
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00017EC6 File Offset: 0x000160C6
		public unsafe UnsafeList<T>* GetUnsafeList()
		{
			return this.m_ListData;
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x00017ECE File Offset: 0x000160CE
		public unsafe void AddNoResize(T value)
		{
			this.m_ListData->AddNoResize(value);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x00017EDC File Offset: 0x000160DC
		public unsafe void AddRangeNoResize(void* ptr, int count)
		{
			this.m_ListData->AddRangeNoResize(ptr, count);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00017EEB File Offset: 0x000160EB
		public unsafe void AddRangeNoResize(NativeList<T> list)
		{
			this.m_ListData->AddRangeNoResize(*list.m_ListData);
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00017F03 File Offset: 0x00016103
		public unsafe void Add(in T value)
		{
			this.m_ListData->Add(in value);
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00017F11 File Offset: 0x00016111
		public void AddRange(NativeArray<T> array)
		{
			this.AddRange(array.GetUnsafeReadOnlyPtr<T>(), array.Length);
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00017F26 File Offset: 0x00016126
		public unsafe void AddRange(void* ptr, int count)
		{
			this.m_ListData->AddRange(ptr, CollectionHelper.AssumePositive(count));
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00017F3A File Offset: 0x0001613A
		public unsafe void AddReplicate(in T value, int count)
		{
			this.m_ListData->AddReplicate(in value, CollectionHelper.AssumePositive(count));
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00017F4E File Offset: 0x0001614E
		public unsafe void InsertRangeWithBeginEnd(int begin, int end)
		{
			this.m_ListData->InsertRangeWithBeginEnd(begin, end);
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00017F5D File Offset: 0x0001615D
		public void InsertRange(int index, int count)
		{
			this.InsertRangeWithBeginEnd(index, index + count);
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00017F69 File Offset: 0x00016169
		public unsafe void RemoveAtSwapBack(int index)
		{
			this.m_ListData->RemoveAtSwapBack(index);
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00017F77 File Offset: 0x00016177
		public unsafe void RemoveRangeSwapBack(int index, int count)
		{
			this.m_ListData->RemoveRangeSwapBack(index, count);
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00017F86 File Offset: 0x00016186
		public unsafe void RemoveAt(int index)
		{
			this.m_ListData->RemoveAt(index);
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00017F94 File Offset: 0x00016194
		public unsafe void RemoveRange(int index, int count)
		{
			this.m_ListData->RemoveRange(index, count);
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x00017FA3 File Offset: 0x000161A3
		public unsafe readonly bool IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_ListData == null || this.m_ListData->Length == 0;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x00017FBF File Offset: 0x000161BF
		public readonly bool IsCreated
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_ListData != null;
			}
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00017FCE File Offset: 0x000161CE
		public void Dispose()
		{
			if (!this.IsCreated)
			{
				return;
			}
			UnsafeList<T>.Destroy(this.m_ListData);
			this.m_ListData = null;
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00017FEC File Offset: 0x000161EC
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(AllocatorManager.AllocatorHandle) })]
		internal void Dispose<[global::System.Runtime.CompilerServices.IsUnmanaged] U>(ref U allocator) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			if (!this.IsCreated)
			{
				return;
			}
			UnsafeList<T>.Destroy<U>(this.m_ListData, ref allocator);
			this.m_ListData = null;
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0001800C File Offset: 0x0001620C
		public unsafe JobHandle Dispose(JobHandle inputDeps)
		{
			if (!this.IsCreated)
			{
				return inputDeps;
			}
			JobHandle jobHandle = new NativeListDisposeJob
			{
				Data = new NativeListDispose
				{
					m_ListData = (UntypedUnsafeList*)this.m_ListData
				}
			}.Schedule(inputDeps);
			this.m_ListData = null;
			return jobHandle;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00018057 File Offset: 0x00016257
		public unsafe void Clear()
		{
			this.m_ListData->Clear();
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00018064 File Offset: 0x00016264
		[Obsolete("Implicit cast from `NativeList<T>` to `NativeArray<T>` has been deprecated; Use '.AsArray()' method to do explicit cast instead.", false)]
		public static implicit operator NativeArray<T>(NativeList<T> nativeList)
		{
			return nativeList.AsArray();
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0001806D File Offset: 0x0001626D
		public unsafe NativeArray<T> AsArray()
		{
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)this.m_ListData->Ptr, this.m_ListData->Length, Allocator.None);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0001808C File Offset: 0x0001628C
		public unsafe NativeArray<T> AsDeferredJobArray()
		{
			byte* buffer = (byte*)this.m_ListData;
			buffer++;
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)buffer, 0, Allocator.Invalid);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x000180AC File Offset: 0x000162AC
		public unsafe NativeArray<T> ToArray(AllocatorManager.AllocatorHandle allocator)
		{
			NativeArray<T> nativeArray = CollectionHelper.CreateNativeArray<T>(this.Length, allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeUtility.MemCpy(nativeArray.m_Buffer, (void*)this.m_ListData->Ptr, (long)(this.Length * UnsafeUtility.SizeOf<T>()));
			return nativeArray;
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x000180DE File Offset: 0x000162DE
		public unsafe void CopyFrom(in NativeArray<T> other)
		{
			this.m_ListData->CopyFrom(in other);
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x000180EC File Offset: 0x000162EC
		public unsafe void CopyFrom(in UnsafeList<T> other)
		{
			this.m_ListData->CopyFrom(in other);
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x000180FA File Offset: 0x000162FA
		public unsafe void CopyFrom(in NativeList<T> other)
		{
			this.CopyFrom(in *other.m_ListData);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00018108 File Offset: 0x00016308
		public NativeArray<T>.Enumerator GetEnumerator()
		{
			NativeArray<T> array = this.AsArray();
			return new NativeArray<T>.Enumerator(ref array);
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x000078E7 File Offset: 0x00005AE7
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x000078E7 File Offset: 0x00005AE7
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00018123 File Offset: 0x00016323
		public unsafe void Resize(int length, NativeArrayOptions options)
		{
			this.m_ListData->Resize(length, options);
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00018132 File Offset: 0x00016332
		public void ResizeUninitialized(int length)
		{
			this.Resize(length, NativeArrayOptions.UninitializedMemory);
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0001813C File Offset: 0x0001633C
		public unsafe void SetCapacity(int capacity)
		{
			this.m_ListData->SetCapacity(capacity);
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0001814A File Offset: 0x0001634A
		public unsafe void TrimExcess()
		{
			this.m_ListData->TrimExcess();
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x00018157 File Offset: 0x00016357
		public unsafe NativeArray<T>.ReadOnly AsReadOnly()
		{
			return new NativeArray<T>.ReadOnly((void*)this.m_ListData->Ptr, this.m_ListData->Length);
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00018157 File Offset: 0x00016357
		public unsafe NativeArray<T>.ReadOnly AsParallelReader()
		{
			return new NativeArray<T>.ReadOnly((void*)this.m_ListData->Ptr, this.m_ListData->Length);
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x00018174 File Offset: 0x00016374
		public NativeList<T>.ParallelWriter AsParallelWriter()
		{
			return new NativeList<T>.ParallelWriter(this.m_ListData);
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x00018181 File Offset: 0x00016381
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckInitialCapacity(int initialCapacity)
		{
			if (initialCapacity < 0)
			{
				throw new ArgumentOutOfRangeException("initialCapacity", "Capacity must be >= 0");
			}
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00018197 File Offset: 0x00016397
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckTotalSize(int initialCapacity, long totalSize)
		{
			if (totalSize > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("initialCapacity", string.Format("Capacity * sizeof(T) cannot exceed {0} bytes", int.MaxValue));
			}
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x000181C1 File Offset: 0x000163C1
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckSufficientCapacity(int capacity, int length)
		{
			if (capacity < length)
			{
				throw new InvalidOperationException(string.Format("Length {0} exceeds Capacity {1}", length, capacity));
			}
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x000181E3 File Offset: 0x000163E3
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckIndexInRange(int value, int length)
		{
			if (value < 0)
			{
				throw new IndexOutOfRangeException(string.Format("Value {0} must be positive.", value));
			}
			if (value >= length)
			{
				throw new IndexOutOfRangeException(string.Format("Value {0} is out of range in NativeList of '{1}' Length.", value, length));
			}
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0001821F File Offset: 0x0001641F
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckArgPositive(int value)
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException(string.Format("Value {0} must be positive.", value));
			}
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0001823C File Offset: 0x0001643C
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private unsafe void CheckHandleMatches(AllocatorManager.AllocatorHandle handle)
		{
			if (this.m_ListData == null)
			{
				throw new ArgumentOutOfRangeException(string.Format("Allocator handle {0} can't match because container is not initialized.", handle));
			}
			if (this.m_ListData->Allocator.Index != handle.Index)
			{
				throw new ArgumentOutOfRangeException(string.Format("Allocator handle {0} can't match because container handle index doesn't match.", handle));
			}
			if (this.m_ListData->Allocator.Version != handle.Version)
			{
				throw new ArgumentOutOfRangeException(string.Format("Allocator handle {0} matches container handle index, but has different version.", handle));
			}
		}

		// Token: 0x040003C0 RID: 960
		[NativeDisableUnsafePtrRestriction]
		internal unsafe UnsafeList<T>* m_ListData;

		// Token: 0x0200009B RID: 155
		[NativeContainer]
		[NativeContainerIsAtomicWriteOnly]
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public struct ParallelWriter
		{
			// Token: 0x170000D5 RID: 213
			// (get) Token: 0x060007BC RID: 1980 RVA: 0x000182C5 File Offset: 0x000164C5
			public unsafe readonly void* Ptr
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return (void*)this.ListData->Ptr;
				}
			}

			// Token: 0x060007BD RID: 1981 RVA: 0x000182D2 File Offset: 0x000164D2
			internal unsafe ParallelWriter(UnsafeList<T>* listData)
			{
				this.ListData = listData;
			}

			// Token: 0x060007BE RID: 1982 RVA: 0x000182DC File Offset: 0x000164DC
			public unsafe void AddNoResize(T value)
			{
				int idx = Interlocked.Increment(ref this.ListData->m_length) - 1;
				UnsafeUtility.WriteArrayElement<T>((void*)this.ListData->Ptr, idx, value);
			}

			// Token: 0x060007BF RID: 1983 RVA: 0x00018310 File Offset: 0x00016510
			public unsafe void AddRangeNoResize(void* ptr, int count)
			{
				int idx = Interlocked.Add(ref this.ListData->m_length, count) - count;
				int sizeOf = sizeof(T);
				void* dst = (void*)(this.ListData->Ptr + idx * sizeOf / sizeof(T));
				UnsafeUtility.MemCpy(dst, ptr, (long)(count * sizeOf));
			}

			// Token: 0x060007C0 RID: 1984 RVA: 0x00018353 File Offset: 0x00016553
			public unsafe void AddRangeNoResize(UnsafeList<T> list)
			{
				this.AddRangeNoResize((void*)list.Ptr, list.Length);
			}

			// Token: 0x060007C1 RID: 1985 RVA: 0x00018368 File Offset: 0x00016568
			public unsafe void AddRangeNoResize(NativeList<T> list)
			{
				this.AddRangeNoResize(*list.m_ListData);
			}

			// Token: 0x040003C1 RID: 961
			[NativeDisableUnsafePtrRestriction]
			public unsafe UnsafeList<T>* ListData;
		}
	}
}
