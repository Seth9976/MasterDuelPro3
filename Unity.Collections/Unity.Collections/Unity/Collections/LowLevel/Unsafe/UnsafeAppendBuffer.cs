using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Jobs;
using Unity.Mathematics;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000104 RID: 260
	[GenerateTestsForBurstCompatibility]
	public struct UnsafeAppendBuffer : INativeDisposable, IDisposable
	{
		// Token: 0x06000AE4 RID: 2788 RVA: 0x0002163E File Offset: 0x0001F83E
		public UnsafeAppendBuffer(int initialCapacity, int alignment, AllocatorManager.AllocatorHandle allocator)
		{
			this.Alignment = alignment;
			this.Allocator = allocator;
			this.Ptr = null;
			this.Length = 0;
			this.Capacity = 0;
			this.SetCapacity(math.max(initialCapacity, 1));
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x00021671 File Offset: 0x0001F871
		public unsafe UnsafeAppendBuffer(void* ptr, int length)
		{
			this.Alignment = 0;
			this.Allocator = AllocatorManager.None;
			this.Ptr = (byte*)ptr;
			this.Length = 0;
			this.Capacity = length;
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x0002169A File Offset: 0x0001F89A
		public readonly bool IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.Length == 0;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x000216A5 File Offset: 0x0001F8A5
		public readonly bool IsCreated
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.Ptr != null;
			}
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x000216B4 File Offset: 0x0001F8B4
		public void Dispose()
		{
			if (!this.IsCreated)
			{
				return;
			}
			if (CollectionHelper.ShouldDeallocate(this.Allocator))
			{
				Memory.Unmanaged.Free<byte>(this.Ptr, this.Allocator);
				this.Allocator = AllocatorManager.Invalid;
			}
			this.Ptr = null;
			this.Length = 0;
			this.Capacity = 0;
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0002170C File Offset: 0x0001F90C
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

		// Token: 0x06000AEA RID: 2794 RVA: 0x00021776 File Offset: 0x0001F976
		public void Reset()
		{
			this.Length = 0;
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x00021780 File Offset: 0x0001F980
		public unsafe void SetCapacity(int capacity)
		{
			if (capacity <= this.Capacity)
			{
				return;
			}
			capacity = math.max(64, math.ceilpow2(capacity));
			byte* newPtr = (byte*)Memory.Unmanaged.Allocate((long)capacity, this.Alignment, this.Allocator);
			if (this.Ptr != null)
			{
				UnsafeUtility.MemCpy((void*)newPtr, (void*)this.Ptr, (long)this.Length);
				Memory.Unmanaged.Free<byte>(this.Ptr, this.Allocator);
			}
			this.Ptr = newPtr;
			this.Capacity = capacity;
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x000217F6 File Offset: 0x0001F9F6
		public void ResizeUninitialized(int length)
		{
			this.SetCapacity(length);
			this.Length = length;
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x00021808 File Offset: 0x0001FA08
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe void Add<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(T value) where T : struct, ValueType
		{
			int structSize = UnsafeUtility.SizeOf<T>();
			this.SetCapacity(this.Length + structSize);
			void* addr = (void*)(this.Ptr + this.Length);
			if (CollectionHelper.IsAligned(addr, UnsafeUtility.AlignOf<T>()))
			{
				UnsafeUtility.CopyStructureToPtr<T>(ref value, addr);
			}
			else
			{
				UnsafeUtility.MemCpy(addr, (void*)(&value), (long)structSize);
			}
			this.Length += structSize;
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00021868 File Offset: 0x0001FA68
		public unsafe void Add(void* ptr, int structSize)
		{
			this.SetCapacity(this.Length + structSize);
			UnsafeUtility.MemCpy((void*)(this.Ptr + this.Length), ptr, (long)structSize);
			this.Length += structSize;
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0002189B File Offset: 0x0001FA9B
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe void AddArray<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(void* ptr, int length) where T : struct, ValueType
		{
			this.Add<int>(length);
			if (length != 0)
			{
				this.Add(ptr, length * UnsafeUtility.SizeOf<T>());
			}
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x000218B5 File Offset: 0x0001FAB5
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public void Add<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(NativeArray<T> value) where T : struct, ValueType
		{
			this.Add<int>(value.Length);
			this.Add(value.GetUnsafeReadOnlyPtr<T>(), UnsafeUtility.SizeOf<T>() * value.Length);
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x000218E0 File Offset: 0x0001FAE0
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe T Pop<[global::System.Runtime.CompilerServices.IsUnmanaged] T>() where T : struct, ValueType
		{
			int structSize = UnsafeUtility.SizeOf<T>();
			long num = this.Ptr;
			long size = (long)this.Length;
			long addr = num + size - (long)structSize;
			T data;
			if (CollectionHelper.IsAligned((ulong)addr, UnsafeUtility.AlignOf<T>()))
			{
				data = UnsafeUtility.ReadArrayElement<T>(addr, 0);
			}
			else
			{
				UnsafeUtility.MemCpy((void*)(&data), addr, (long)structSize);
			}
			this.Length -= structSize;
			return data;
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0002193C File Offset: 0x0001FB3C
		public unsafe void Pop(void* ptr, int structSize)
		{
			long num = this.Ptr;
			long size = (long)this.Length;
			long addr = num + size - (long)structSize;
			UnsafeUtility.MemCpy(ptr, addr, (long)structSize);
			this.Length -= structSize;
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00021976 File Offset: 0x0001FB76
		public UnsafeAppendBuffer.Reader AsReader()
		{
			return new UnsafeAppendBuffer.Reader(ref this);
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00021980 File Offset: 0x0001FB80
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckAlignment(int alignment)
		{
			int num = ((alignment == 0) ? 1 : 0);
			bool powTwoAlignment = ((alignment - 1) & alignment) == 0;
			if (num != 0 || !powTwoAlignment)
			{
				throw new ArgumentException(string.Format("Specified alignment must be non-zero positive power of two. Requested: {0}", alignment));
			}
		}

		// Token: 0x0400049D RID: 1181
		[NativeDisableUnsafePtrRestriction]
		public unsafe byte* Ptr;

		// Token: 0x0400049E RID: 1182
		public int Length;

		// Token: 0x0400049F RID: 1183
		public int Capacity;

		// Token: 0x040004A0 RID: 1184
		public AllocatorManager.AllocatorHandle Allocator;

		// Token: 0x040004A1 RID: 1185
		public readonly int Alignment;

		// Token: 0x02000105 RID: 261
		[GenerateTestsForBurstCompatibility]
		public struct Reader
		{
			// Token: 0x06000AF5 RID: 2805 RVA: 0x000219B7 File Offset: 0x0001FBB7
			public Reader(ref UnsafeAppendBuffer buffer)
			{
				this.Ptr = buffer.Ptr;
				this.Size = buffer.Length;
				this.Offset = 0;
			}

			// Token: 0x06000AF6 RID: 2806 RVA: 0x000219D8 File Offset: 0x0001FBD8
			public unsafe Reader(void* ptr, int length)
			{
				this.Ptr = (byte*)ptr;
				this.Size = length;
				this.Offset = 0;
			}

			// Token: 0x1700013D RID: 317
			// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x000219EF File Offset: 0x0001FBEF
			public bool EndOfBuffer
			{
				get
				{
					return this.Offset == this.Size;
				}
			}

			// Token: 0x06000AF8 RID: 2808 RVA: 0x00021A00 File Offset: 0x0001FC00
			[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
			public unsafe void ReadNext<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(out T value) where T : struct, ValueType
			{
				int structSize = UnsafeUtility.SizeOf<T>();
				void* addr = (void*)(this.Ptr + this.Offset);
				if (CollectionHelper.IsAligned(addr, UnsafeUtility.AlignOf<T>()))
				{
					UnsafeUtility.CopyPtrToStructure<T>(addr, out value);
				}
				else
				{
					fixed (T* ptr = &value)
					{
						void* pValue = (void*)ptr;
						UnsafeUtility.MemCpy(pValue, addr, (long)structSize);
					}
				}
				this.Offset += structSize;
			}

			// Token: 0x06000AF9 RID: 2809 RVA: 0x00021A58 File Offset: 0x0001FC58
			[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
			public unsafe T ReadNext<[global::System.Runtime.CompilerServices.IsUnmanaged] T>() where T : struct, ValueType
			{
				int structSize = UnsafeUtility.SizeOf<T>();
				void* addr = (void*)(this.Ptr + this.Offset);
				T value;
				if (CollectionHelper.IsAligned(addr, UnsafeUtility.AlignOf<T>()))
				{
					value = UnsafeUtility.ReadArrayElement<T>(addr, 0);
				}
				else
				{
					UnsafeUtility.MemCpy((void*)(&value), addr, (long)structSize);
				}
				this.Offset += structSize;
				return value;
			}

			// Token: 0x06000AFA RID: 2810 RVA: 0x00021AAB File Offset: 0x0001FCAB
			public unsafe void* ReadNext(int structSize)
			{
				void* ptr = (void*)((IntPtr)((void*)this.Ptr) + this.Offset);
				this.Offset += structSize;
				return ptr;
			}

			// Token: 0x06000AFB RID: 2811 RVA: 0x00021AD8 File Offset: 0x0001FCD8
			[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
			public unsafe void ReadNext<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(out NativeArray<T> value, AllocatorManager.AllocatorHandle allocator) where T : struct, ValueType
			{
				int length = this.ReadNext<int>();
				value = CollectionHelper.CreateNativeArray<T>(length, allocator, NativeArrayOptions.UninitializedMemory);
				int size = length * UnsafeUtility.SizeOf<T>();
				if (size > 0)
				{
					void* ptr = this.ReadNext(size);
					UnsafeUtility.MemCpy(value.GetUnsafePtr<T>(), ptr, (long)size);
				}
			}

			// Token: 0x06000AFC RID: 2812 RVA: 0x00021B21 File Offset: 0x0001FD21
			[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
			public unsafe void* ReadNextArray<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(out int length) where T : struct, ValueType
			{
				length = this.ReadNext<int>();
				if (length != 0)
				{
					return this.ReadNext(length * UnsafeUtility.SizeOf<T>());
				}
				return null;
			}

			// Token: 0x06000AFD RID: 2813 RVA: 0x00021B40 File Offset: 0x0001FD40
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			[Conditional("UNITY_DOTS_DEBUG")]
			private void CheckBounds(int structSize)
			{
				if (this.Offset + structSize > this.Size)
				{
					throw new ArgumentException(string.Format("Requested value outside bounds of UnsafeAppendOnlyBuffer. Remaining bytes: {0} Requested: {1}", this.Size - this.Offset, structSize));
				}
			}

			// Token: 0x040004A2 RID: 1186
			public unsafe readonly byte* Ptr;

			// Token: 0x040004A3 RID: 1187
			public readonly int Size;

			// Token: 0x040004A4 RID: 1188
			public int Offset;
		}
	}
}
