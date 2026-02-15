using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Assertions;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x02000587 RID: 1415
	internal struct LayoutDataStore : IDisposable
	{
		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x060026D2 RID: 9938 RVA: 0x0009A67A File Offset: 0x0009887A
		public bool IsValid
		{
			get
			{
				return null != this.m_Data;
			}
		}

		// Token: 0x060026D3 RID: 9939 RVA: 0x0009A68C File Offset: 0x0009888C
		public unsafe LayoutDataStore(ComponentType[] components, int initialCapacity, Allocator allocator)
		{
			Assert.IsTrue(components.Length != 0, "LayoutDataStore requires at least one component size.");
			Assert.IsTrue(components[0].Size >= 4, string.Format("{0} requires a minimum element size of {1} to alias", "LayoutDataStore", 4));
			this.m_Allocator = allocator;
			this.m_Data = (LayoutDataStore.Data*)UnsafeUtility.Malloc((long)UnsafeUtility.SizeOf<LayoutDataStore.Data>(), UnsafeUtility.AlignOf<LayoutDataStore.Data>(), this.m_Allocator);
			UnsafeUtility.MemClear((void*)this.m_Data, (long)UnsafeUtility.SizeOf<LayoutDataStore.Data>());
			this.m_Data->ComponentCount = components.Length;
			this.m_Data->Components = (LayoutDataStore.ComponentDataStore*)UnsafeUtility.Malloc((long)(UnsafeUtility.SizeOf<LayoutDataStore.ComponentDataStore>() * components.Length), UnsafeUtility.AlignOf<LayoutDataStore.ComponentDataStore>(), allocator);
			for (int i = 0; i < components.Length; i++)
			{
				this.m_Data->Components[i] = new LayoutDataStore.ComponentDataStore(components[i].Size, allocator);
			}
			this.ResizeCapacity(initialCapacity);
			this.m_Data->NextFreeIndex = 0;
		}

		// Token: 0x060026D4 RID: 9940 RVA: 0x0009A790 File Offset: 0x00098990
		public unsafe void Dispose()
		{
			for (int i = 0; i < this.m_Data->ComponentCount; i++)
			{
				this.m_Data->Components[i].Dispose();
			}
			UnsafeUtility.Free((void*)this.m_Data->Versions, this.m_Allocator);
			UnsafeUtility.Free((void*)this.m_Data->Components, this.m_Allocator);
			UnsafeUtility.Free((void*)this.m_Data, this.m_Allocator);
			this.m_Data = null;
		}

		// Token: 0x060026D5 RID: 9941 RVA: 0x0009A81C File Offset: 0x00098A1C
		public unsafe bool Exists(in LayoutHandle handle)
		{
			bool flag = (ulong)handle.Index >= (ulong)((long)this.m_Data->Capacity);
			return !flag && this.m_Data->Versions[handle.Index] == handle.Version;
		}

		// Token: 0x060026D6 RID: 9942 RVA: 0x0009A86C File Offset: 0x00098A6C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe readonly void* GetComponentDataPtr(int index, int componentIndex)
		{
			return (void*)this.m_Data->Components[componentIndex].GetComponentDataPtr(index);
		}

		// Token: 0x060026D7 RID: 9943 RVA: 0x0009A89C File Offset: 0x00098A9C
		private unsafe LayoutHandle Allocate(byte** data, int count)
		{
			int index = this.m_Data->NextFreeIndex;
			int nextIndex = LayoutDataStore.GetNextFreeIndex(this.m_Data->Components, index);
			bool flag = nextIndex == -1;
			if (flag)
			{
				this.IncreaseCapacity();
				nextIndex = LayoutDataStore.GetNextFreeIndex(this.m_Data->Components, index);
			}
			int version = this.m_Data->Versions[index];
			this.m_Data->NextFreeIndex = nextIndex;
			Debug.Assert(this.m_Data->ComponentCount == count, "All components must be initialized");
			Debug.Assert(data != null);
			for (int i = 0; i < count; i++)
			{
				Debug.Assert(*(IntPtr*)(data + (IntPtr)i * (IntPtr)sizeof(byte*) / (IntPtr)sizeof(byte*)) != (IntPtr)((UIntPtr)0));
				byte* ptr = this.m_Data->Components[i].GetComponentDataPtr(index);
				UnsafeUtility.MemCpy((void*)ptr, *(IntPtr*)(data + (IntPtr)i * (IntPtr)sizeof(byte*) / (IntPtr)sizeof(byte*)), (long)this.m_Data->Components[i].Size);
			}
			return new LayoutHandle(index, version);
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x0009A9C0 File Offset: 0x00098BC0
		public unsafe void Free(in LayoutHandle handle)
		{
			bool flag = !this.Exists(in handle);
			if (flag)
			{
				throw new InvalidOperationException(string.Format("Failed to Free handle with Index={0} Version={1}", handle.Index, handle.Version));
			}
			this.m_Data->Versions[handle.Index]++;
			LayoutDataStore.SetNextFreeIndex(this.m_Data->Components, handle.Index, this.m_Data->NextFreeIndex);
			this.m_Data->NextFreeIndex = handle.Index;
		}

		// Token: 0x060026D9 RID: 9945 RVA: 0x0009AA4F File Offset: 0x00098C4F
		private unsafe static void SetNextFreeIndex(LayoutDataStore.ComponentDataStore* ptr, int index, int value)
		{
			*(int*)ptr->GetComponentDataPtr(index) = value;
		}

		// Token: 0x060026DA RID: 9946 RVA: 0x0009AA5C File Offset: 0x00098C5C
		private unsafe static int GetNextFreeIndex(LayoutDataStore.ComponentDataStore* ptr, int index)
		{
			return *(int*)ptr->GetComponentDataPtr(index);
		}

		// Token: 0x060026DB RID: 9947 RVA: 0x0009AA76 File Offset: 0x00098C76
		private unsafe void IncreaseCapacity()
		{
			this.ResizeCapacity((int)((float)this.m_Data->Capacity * 1.5f));
		}

		// Token: 0x060026DC RID: 9948 RVA: 0x0009AA94 File Offset: 0x00098C94
		private unsafe void ResizeCapacity(int capacity)
		{
			Assert.IsTrue(capacity > 0);
			this.m_Data->Versions = (int*)LayoutDataStore.ResizeArray((void*)this.m_Data->Versions, (long)this.m_Data->Capacity, (long)capacity, 4L, 4, this.m_Allocator);
			for (int i = 0; i < this.m_Data->ComponentCount; i++)
			{
				this.m_Data->Components[i].ResizeCapacity(capacity);
			}
			int start = ((this.m_Data->Capacity > 0) ? (this.m_Data->Capacity - 1) : 0);
			for (int j = start; j < capacity; j++)
			{
				this.m_Data->Versions[j] = 1;
				LayoutDataStore.SetNextFreeIndex(this.m_Data->Components, j, j + 1);
			}
			LayoutDataStore.SetNextFreeIndex(this.m_Data->Components, capacity - 1, -1);
			this.m_Data->Capacity = capacity;
		}

		// Token: 0x060026DD RID: 9949 RVA: 0x0009AB90 File Offset: 0x00098D90
		private unsafe static void* ResizeArray(void* fromPtr, long fromCount, long toCount, long size, int align, Allocator allocator)
		{
			Assert.IsTrue(toCount > 0L);
			void* toPtr = UnsafeUtility.Malloc(size * toCount, align, allocator);
			Assert.IsTrue(toPtr != null);
			bool flag = fromCount <= 0L;
			void* ptr;
			if (flag)
			{
				ptr = toPtr;
			}
			else
			{
				long countToCopy = ((toCount < fromCount) ? toCount : fromCount);
				long bytesToCopy = countToCopy * size;
				UnsafeUtility.MemCpy(toPtr, fromPtr, bytesToCopy);
				UnsafeUtility.Free(fromPtr, allocator);
				ptr = toPtr;
			}
			return ptr;
		}

		// Token: 0x060026DE RID: 9950 RVA: 0x0009ABFC File Offset: 0x00098DFC
		public unsafe LayoutHandle Allocate<[global::System.Runtime.CompilerServices.IsUnmanaged] T0>(in T0 component0) where T0 : struct, ValueType
		{
			checked
			{
				fixed (T0* ptr = &component0)
				{
					T0* ptr0 = ptr;
					byte** data = stackalloc byte*[unchecked((UIntPtr)1) * (UIntPtr)sizeof(byte*)];
					*(IntPtr*)data = ptr0;
					return this.Allocate(data, 1);
				}
			}
		}

		// Token: 0x060026DF RID: 9951 RVA: 0x0009AC2C File Offset: 0x00098E2C
		public unsafe LayoutHandle Allocate<[global::System.Runtime.CompilerServices.IsUnmanaged] T0, [global::System.Runtime.CompilerServices.IsUnmanaged] T1, [global::System.Runtime.CompilerServices.IsUnmanaged] T2, [global::System.Runtime.CompilerServices.IsUnmanaged] T3>(in T0 component0, in T1 component1, in T2 component2, in T3 component3) where T0 : struct, ValueType where T1 : struct, ValueType where T2 : struct, ValueType where T3 : struct, ValueType
		{
			fixed (T0* ptr4 = &component0)
			{
				T0* ptr0 = ptr4;
				fixed (T1* ptr5 = &component1)
				{
					T1* ptr = ptr5;
					fixed (T2* ptr6 = &component2)
					{
						T2* ptr2 = ptr6;
						fixed (T3* ptr7 = &component3)
						{
							T3* ptr3 = ptr7;
							byte** data;
							checked
							{
								data = stackalloc byte*[unchecked((UIntPtr)4) * (UIntPtr)sizeof(byte*)];
								*(IntPtr*)data = ptr0;
							}
							*(IntPtr*)(data + sizeof(byte*) / sizeof(byte*)) = ptr;
							*(IntPtr*)(data + (IntPtr)2 * (IntPtr)sizeof(byte*) / (IntPtr)sizeof(byte*)) = ptr2;
							*(IntPtr*)(data + (IntPtr)3 * (IntPtr)sizeof(byte*) / (IntPtr)sizeof(byte*)) = ptr3;
							return this.Allocate(data, 4);
						}
					}
				}
			}
		}

		// Token: 0x040013A8 RID: 5032
		private const int k_ChunkSize = 32768;

		// Token: 0x040013A9 RID: 5033
		private readonly Allocator m_Allocator;

		// Token: 0x040013AA RID: 5034
		[NativeDisableUnsafePtrRestriction]
		private unsafe LayoutDataStore.Data* m_Data;

		// Token: 0x02000588 RID: 1416
		private struct Chunk
		{
			// Token: 0x040013AB RID: 5035
			[NativeDisableUnsafePtrRestriction]
			public unsafe byte* Buffer;
		}

		// Token: 0x02000589 RID: 1417
		private struct ComponentDataStore : IDisposable
		{
			// Token: 0x060026E0 RID: 9952 RVA: 0x0009AC9F File Offset: 0x00098E9F
			public ComponentDataStore(int size, Allocator allocator)
			{
				this.Allocator = allocator;
				this.Size = size;
				this.ComponentCountPerChunk = 32768 / size;
				this.ChunkCount = 0;
				this.m_Chunks = null;
			}

			// Token: 0x060026E1 RID: 9953 RVA: 0x0009ACCC File Offset: 0x00098ECC
			public unsafe void Dispose()
			{
				bool flag = null == this.m_Chunks;
				if (!flag)
				{
					for (int i = 0; i < this.ChunkCount; i++)
					{
						UnsafeUtility.Free((void*)this.m_Chunks[i].Buffer, this.Allocator);
					}
					UnsafeUtility.Free((void*)this.m_Chunks, this.Allocator);
					this.ChunkCount = 0;
					this.m_Chunks = null;
				}
			}

			// Token: 0x060026E2 RID: 9954 RVA: 0x0009AD44 File Offset: 0x00098F44
			public unsafe byte* GetComponentDataPtr(int index)
			{
				int chunkIndex = index / this.ComponentCountPerChunk;
				int indexInChunk = index % this.ComponentCountPerChunk;
				return this.m_Chunks[chunkIndex].Buffer + indexInChunk * this.Size;
			}

			// Token: 0x060026E3 RID: 9955 RVA: 0x0009AD88 File Offset: 0x00098F88
			public unsafe void ResizeCapacity(int capacity)
			{
				int newChunkCount = capacity / this.ComponentCountPerChunk + 1;
				bool flag = newChunkCount > this.ChunkCount;
				if (flag)
				{
					this.m_Chunks = (LayoutDataStore.Chunk*)LayoutDataStore.ResizeArray((void*)this.m_Chunks, (long)this.ChunkCount, (long)newChunkCount, (long)UnsafeUtility.SizeOf<LayoutDataStore.Chunk>(), UnsafeUtility.AlignOf<LayoutDataStore.Chunk>(), this.Allocator);
					for (int i = this.ChunkCount; i < newChunkCount; i++)
					{
						this.m_Chunks[i] = new LayoutDataStore.Chunk
						{
							Buffer = (byte*)UnsafeUtility.Malloc(32768L, 4, this.Allocator)
						};
					}
				}
				else
				{
					bool flag2 = newChunkCount < this.ChunkCount;
					if (flag2)
					{
						for (int j = this.ChunkCount - 1; j >= newChunkCount; j--)
						{
							UnsafeUtility.Free((void*)this.m_Chunks[j].Buffer, this.Allocator);
						}
						this.m_Chunks = (LayoutDataStore.Chunk*)LayoutDataStore.ResizeArray((void*)this.m_Chunks, (long)this.ChunkCount, (long)newChunkCount, (long)UnsafeUtility.SizeOf<LayoutDataStore.Chunk>(), UnsafeUtility.AlignOf<LayoutDataStore.Chunk>(), this.Allocator);
					}
				}
				this.ChunkCount = newChunkCount;
			}

			// Token: 0x040013AC RID: 5036
			public Allocator Allocator;

			// Token: 0x040013AD RID: 5037
			public int Size;

			// Token: 0x040013AE RID: 5038
			public int ComponentCountPerChunk;

			// Token: 0x040013AF RID: 5039
			public int ChunkCount;

			// Token: 0x040013B0 RID: 5040
			[NativeDisableUnsafePtrRestriction]
			private unsafe LayoutDataStore.Chunk* m_Chunks;
		}

		// Token: 0x0200058A RID: 1418
		private struct Data
		{
			// Token: 0x040013B1 RID: 5041
			public int Capacity;

			// Token: 0x040013B2 RID: 5042
			public int NextFreeIndex;

			// Token: 0x040013B3 RID: 5043
			public int ComponentCount;

			// Token: 0x040013B4 RID: 5044
			[NativeDisableUnsafePtrRestriction]
			public unsafe int* Versions;

			// Token: 0x040013B5 RID: 5045
			[NativeDisableUnsafePtrRestriction]
			public unsafe LayoutDataStore.ComponentDataStore* Components;
		}
	}
}
