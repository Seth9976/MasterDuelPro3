using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Jobs;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200011E RID: 286
	[DebuggerDisplay("Length = {Length}, Capacity = {Capacity}, IsCreated = {IsCreated}, IsEmpty = {IsEmpty}")]
	[DebuggerTypeProxy(typeof(UnsafePtrListDebugView<>))]
	[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
	public struct UnsafePtrList<[global::System.Runtime.CompilerServices.IsUnmanaged] T> : INativeDisposable, IDisposable, IEnumerable<IntPtr>, IEnumerable where T : struct, ValueType
	{
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x000245A4 File Offset: 0x000227A4
		// (set) Token: 0x06000BFD RID: 3069 RVA: 0x000245C4 File Offset: 0x000227C4
		public int Length
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return this.ListDataRO<T>().Length;
			}
			set
			{
				(ref this).ListData<T>().Length = value;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x000245D4 File Offset: 0x000227D4
		// (set) Token: 0x06000BFF RID: 3071 RVA: 0x000245F4 File Offset: 0x000227F4
		public int Capacity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			readonly get
			{
				return this.ListDataRO<T>().Capacity;
			}
			set
			{
				(ref this).ListData<T>().Capacity = value;
			}
		}

		// Token: 0x1700016A RID: 362
		public unsafe T* this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return *(IntPtr*)(this.Ptr + (IntPtr)CollectionHelper.AssumePositive(index) * (IntPtr)sizeof(T*) / (IntPtr)sizeof(T*));
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				*(IntPtr*)(this.Ptr + (IntPtr)CollectionHelper.AssumePositive(index) * (IntPtr)sizeof(T*) / (IntPtr)sizeof(T*)) = value;
			}
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x00024633 File Offset: 0x00022833
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe ref T* ElementAt(int index)
		{
			return ref this.Ptr[(IntPtr)CollectionHelper.AssumePositive(index) * (IntPtr)sizeof(T*) / (IntPtr)sizeof(T*)];
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x0002464A File Offset: 0x0002284A
		public unsafe UnsafePtrList(T** ptr, int length)
		{
			this = default(UnsafePtrList<T>);
			this.Ptr = ptr;
			this.m_length = length;
			this.m_capacity = length;
			this.Allocator = AllocatorManager.None;
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x00024673 File Offset: 0x00022873
		public unsafe UnsafePtrList(int initialCapacity, AllocatorManager.AllocatorHandle allocator, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory)
		{
			this.Ptr = null;
			this.m_length = 0;
			this.m_capacity = 0;
			this.padding = 0;
			this.Allocator = AllocatorManager.None;
			*(ref this).ListData<T>() = new UnsafeList<IntPtr>(initialCapacity, allocator, options);
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x000246B0 File Offset: 0x000228B0
		public unsafe static UnsafePtrList<T>* Create(T** ptr, int length)
		{
			UnsafePtrList<T>* ptr2 = AllocatorManager.Allocate<UnsafePtrList<T>>(AllocatorManager.Persistent, 1);
			*ptr2 = new UnsafePtrList<T>(ptr, length);
			return ptr2;
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x000246CA File Offset: 0x000228CA
		public unsafe static UnsafePtrList<T>* Create(int initialCapacity, AllocatorManager.AllocatorHandle allocator, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory)
		{
			UnsafePtrList<T>* ptr = AllocatorManager.Allocate<UnsafePtrList<T>>(allocator, 1);
			*ptr = new UnsafePtrList<T>(initialCapacity, allocator, options);
			return ptr;
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x000246E4 File Offset: 0x000228E4
		public unsafe static void Destroy(UnsafePtrList<T>* listData)
		{
			AllocatorManager.AllocatorHandle allocatorHandle = (((ref *listData).ListData<T>().Allocator.Value == AllocatorManager.Invalid.Value) ? AllocatorManager.Persistent : (ref *listData).ListData<T>().Allocator);
			listData->Dispose();
			AllocatorManager.Free<UnsafePtrList<T>>(allocatorHandle, listData, 1);
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x0002472F File Offset: 0x0002292F
		public readonly bool IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return !this.IsCreated || this.Length == 0;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x00024744 File Offset: 0x00022944
		public readonly bool IsCreated
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.Ptr != null;
			}
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x00024753 File Offset: 0x00022953
		public void Dispose()
		{
			(ref this).ListData<T>().Dispose();
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x00024760 File Offset: 0x00022960
		public JobHandle Dispose(JobHandle inputDeps)
		{
			return (ref this).ListData<T>().Dispose(inputDeps);
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x0002476E File Offset: 0x0002296E
		public void Clear()
		{
			(ref this).ListData<T>().Clear();
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x0002477B File Offset: 0x0002297B
		public void Resize(int length, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory)
		{
			(ref this).ListData<T>().Resize(length, options);
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0002478A File Offset: 0x0002298A
		public void SetCapacity(int capacity)
		{
			(ref this).ListData<T>().SetCapacity(capacity);
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x00024798 File Offset: 0x00022998
		public void TrimExcess()
		{
			(ref this).ListData<T>().TrimExcess();
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x000247A8 File Offset: 0x000229A8
		public unsafe int IndexOf(void* ptr)
		{
			for (int i = 0; i < this.Length; i++)
			{
				if (*(IntPtr*)(this.Ptr + (IntPtr)i * (IntPtr)sizeof(T*) / (IntPtr)sizeof(T*)) == ptr)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x000247DD File Offset: 0x000229DD
		public unsafe bool Contains(void* ptr)
		{
			return this.IndexOf(ptr) != -1;
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x000247EC File Offset: 0x000229EC
		public unsafe void AddNoResize(void* value)
		{
			(ref this).ListData<T>().AddNoResize((IntPtr)value);
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x000247FF File Offset: 0x000229FF
		public unsafe void AddRangeNoResize(void** ptr, int count)
		{
			(ref this).ListData<T>().AddRangeNoResize((void*)ptr, count);
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x0002480E File Offset: 0x00022A0E
		public unsafe void AddRangeNoResize(UnsafePtrList<T> list)
		{
			(ref this).ListData<T>().AddRangeNoResize((void*)list.Ptr, list.Length);
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x00024828 File Offset: 0x00022A28
		public void Add(in IntPtr value)
		{
			(ref this).ListData<T>().Add(in value);
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x00024838 File Offset: 0x00022A38
		public unsafe void Add(void* value)
		{
			ref UnsafeList<IntPtr> ptr = ref (ref this).ListData<T>();
			IntPtr intPtr = (IntPtr)value;
			ptr.Add(in intPtr);
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00024859 File Offset: 0x00022A59
		public unsafe void AddRange(void* ptr, int length)
		{
			(ref this).ListData<T>().AddRange(ptr, length);
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x00024868 File Offset: 0x00022A68
		public unsafe void AddRange(UnsafePtrList<T> list)
		{
			(ref this).ListData<T>().AddRange(*(ref list).ListData<T>());
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x00024881 File Offset: 0x00022A81
		public void InsertRangeWithBeginEnd(int begin, int end)
		{
			(ref this).ListData<T>().InsertRangeWithBeginEnd(begin, end);
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x00024890 File Offset: 0x00022A90
		public void RemoveAtSwapBack(int index)
		{
			(ref this).ListData<T>().RemoveAtSwapBack(index);
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x0002489E File Offset: 0x00022A9E
		public void RemoveRangeSwapBack(int index, int count)
		{
			(ref this).ListData<T>().RemoveRangeSwapBack(index, count);
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x000248AD File Offset: 0x00022AAD
		public void RemoveAt(int index)
		{
			(ref this).ListData<T>().RemoveAt(index);
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x000248BB File Offset: 0x00022ABB
		public void RemoveRange(int index, int count)
		{
			(ref this).ListData<T>().RemoveRange(index, count);
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x000078E7 File Offset: 0x00005AE7
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x000078E7 File Offset: 0x00005AE7
		IEnumerator<IntPtr> IEnumerable<IntPtr>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x000248CA File Offset: 0x00022ACA
		public UnsafePtrList<T>.ReadOnly AsReadOnly()
		{
			return new UnsafePtrList<T>.ReadOnly(this.Ptr, this.Length);
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x000248DD File Offset: 0x00022ADD
		public UnsafePtrList<T>.ParallelReader AsParallelReader()
		{
			return new UnsafePtrList<T>.ParallelReader(this.Ptr, this.Length);
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x000248F0 File Offset: 0x00022AF0
		public unsafe UnsafePtrList<T>.ParallelWriter AsParallelWriter()
		{
			return new UnsafePtrList<T>.ParallelWriter(this.Ptr, (UnsafeList<IntPtr>*)UnsafeUtility.AddressOf<UnsafePtrList<T>>(ref this));
		}

		// Token: 0x040004DC RID: 1244
		[NativeDisableUnsafePtrRestriction]
		public unsafe readonly T** Ptr;

		// Token: 0x040004DD RID: 1245
		public readonly int m_length;

		// Token: 0x040004DE RID: 1246
		public readonly int m_capacity;

		// Token: 0x040004DF RID: 1247
		public readonly AllocatorManager.AllocatorHandle Allocator;

		// Token: 0x040004E0 RID: 1248
		private readonly int padding;

		// Token: 0x0200011F RID: 287
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public struct ReadOnly
		{
			// Token: 0x1700016D RID: 365
			// (get) Token: 0x06000C23 RID: 3107 RVA: 0x00024903 File Offset: 0x00022B03
			public readonly bool IsCreated
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return this.Ptr != null;
				}
			}

			// Token: 0x1700016E RID: 366
			// (get) Token: 0x06000C24 RID: 3108 RVA: 0x00024912 File Offset: 0x00022B12
			public readonly bool IsEmpty
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return !this.IsCreated || this.Length == 0;
				}
			}

			// Token: 0x06000C25 RID: 3109 RVA: 0x00024927 File Offset: 0x00022B27
			internal unsafe ReadOnly(T** ptr, int length)
			{
				this.Ptr = ptr;
				this.Length = length;
			}

			// Token: 0x06000C26 RID: 3110 RVA: 0x00024938 File Offset: 0x00022B38
			public unsafe int IndexOf(void* ptr)
			{
				for (int i = 0; i < this.Length; i++)
				{
					if (*(IntPtr*)(this.Ptr + (IntPtr)i * (IntPtr)sizeof(T*) / (IntPtr)sizeof(T*)) == ptr)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x06000C27 RID: 3111 RVA: 0x0002496D File Offset: 0x00022B6D
			public unsafe bool Contains(void* ptr)
			{
				return this.IndexOf(ptr) != -1;
			}

			// Token: 0x040004E1 RID: 1249
			[NativeDisableUnsafePtrRestriction]
			public unsafe readonly T** Ptr;

			// Token: 0x040004E2 RID: 1250
			public readonly int Length;
		}

		// Token: 0x02000120 RID: 288
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public struct ParallelReader
		{
			// Token: 0x06000C28 RID: 3112 RVA: 0x0002497C File Offset: 0x00022B7C
			internal unsafe ParallelReader(T** ptr, int length)
			{
				this.Ptr = ptr;
				this.Length = length;
			}

			// Token: 0x06000C29 RID: 3113 RVA: 0x0002498C File Offset: 0x00022B8C
			public unsafe int IndexOf(void* ptr)
			{
				for (int i = 0; i < this.Length; i++)
				{
					if (*(IntPtr*)(this.Ptr + (IntPtr)i * (IntPtr)sizeof(T*) / (IntPtr)sizeof(T*)) == ptr)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x06000C2A RID: 3114 RVA: 0x000249C1 File Offset: 0x00022BC1
			public unsafe bool Contains(void* ptr)
			{
				return this.IndexOf(ptr) != -1;
			}

			// Token: 0x040004E3 RID: 1251
			[NativeDisableUnsafePtrRestriction]
			public unsafe readonly T** Ptr;

			// Token: 0x040004E4 RID: 1252
			public readonly int Length;
		}

		// Token: 0x02000121 RID: 289
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public struct ParallelWriter
		{
			// Token: 0x06000C2B RID: 3115 RVA: 0x000249D0 File Offset: 0x00022BD0
			internal unsafe ParallelWriter(T** ptr, UnsafeList<IntPtr>* listData)
			{
				this.Ptr = ptr;
				this.ListData = listData;
			}

			// Token: 0x06000C2C RID: 3116 RVA: 0x000249E0 File Offset: 0x00022BE0
			public unsafe void AddNoResize(T* value)
			{
				this.ListData->AddNoResize((IntPtr)((void*)value));
			}

			// Token: 0x06000C2D RID: 3117 RVA: 0x000249F3 File Offset: 0x00022BF3
			public unsafe void AddRangeNoResize(T** ptr, int count)
			{
				this.ListData->AddRangeNoResize((void*)ptr, count);
			}

			// Token: 0x06000C2E RID: 3118 RVA: 0x00024A02 File Offset: 0x00022C02
			public unsafe void AddRangeNoResize(UnsafePtrList<T> list)
			{
				this.ListData->AddRangeNoResize((void*)list.Ptr, list.Length);
			}

			// Token: 0x040004E5 RID: 1253
			[NativeDisableUnsafePtrRestriction]
			public unsafe readonly T** Ptr;

			// Token: 0x040004E6 RID: 1254
			[NativeDisableUnsafePtrRestriction]
			public unsafe UnsafeList<IntPtr>* ListData;
		}
	}
}
