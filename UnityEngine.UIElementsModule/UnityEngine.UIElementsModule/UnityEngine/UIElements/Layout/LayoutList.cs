using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Assertions;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x02000597 RID: 1431
	internal struct LayoutList<[global::System.Runtime.CompilerServices.IsUnmanaged] T> : IDisposable where T : struct, ValueType
	{
		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x060026EB RID: 9963 RVA: 0x0009B0D8 File Offset: 0x000992D8
		public unsafe int Count
		{
			get
			{
				return this.m_Data->Count;
			}
		}

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x060026EC RID: 9964 RVA: 0x0009B0E5 File Offset: 0x000992E5
		public bool IsCreated
		{
			get
			{
				return null != this.m_Data;
			}
		}

		// Token: 0x17000A03 RID: 2563
		public unsafe ref T this[int index]
		{
			get
			{
				bool flag = (ulong)index > (ulong)((long)this.m_Data->Count);
				if (flag)
				{
					throw new ArgumentOutOfRangeException();
				}
				return ref this.m_Data->Values[(IntPtr)index * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
			}
		}

		// Token: 0x060026EE RID: 9966 RVA: 0x0009B135 File Offset: 0x00099335
		public LayoutList()
		{
			this.m_Data = null;
			this.m_Allocator = Allocator.Invalid;
		}

		// Token: 0x060026EF RID: 9967 RVA: 0x0009B148 File Offset: 0x00099348
		public unsafe LayoutList(int initialCapacity, Allocator allocator)
		{
			this.m_Allocator = allocator;
			this.m_Data = (LayoutList<T>.Data*)UnsafeUtility.Malloc((long)UnsafeUtility.SizeOf<LayoutList<T>.Data>(), 16, allocator);
			Assert.IsTrue(this.m_Data != null);
			UnsafeUtility.MemClear((void*)this.m_Data, (long)UnsafeUtility.SizeOf<LayoutList<T>.Data>());
			this.ResizeCapacity(initialCapacity);
		}

		// Token: 0x060026F0 RID: 9968 RVA: 0x0009B1A0 File Offset: 0x000993A0
		public unsafe void Dispose()
		{
			bool flag = null == this.m_Data;
			if (!flag)
			{
				bool flag2 = this.m_Data->Values != null;
				if (flag2)
				{
					UnsafeUtility.Free((void*)this.m_Data->Values, this.m_Allocator);
				}
				UnsafeUtility.Free((void*)this.m_Data, this.m_Allocator);
				this.m_Data = null;
			}
		}

		// Token: 0x060026F1 RID: 9969 RVA: 0x0009B208 File Offset: 0x00099408
		public unsafe void Insert(int index, T value)
		{
			bool flag = (ulong)index > (ulong)((long)this.m_Data->Count);
			if (flag)
			{
				throw new ArgumentOutOfRangeException();
			}
			bool flag2 = this.m_Data->Capacity == this.m_Data->Count;
			if (flag2)
			{
				this.IncreaseCapacity();
			}
			bool flag3 = index < this.m_Data->Count;
			if (flag3)
			{
				UnsafeUtility.MemMove((void*)(this.m_Data->Values + (IntPtr)index * (IntPtr)sizeof(T) / (IntPtr)sizeof(T) + sizeof(T) / sizeof(T)), (void*)(this.m_Data->Values + (IntPtr)index * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)), (long)(UnsafeUtility.SizeOf<T>() * (this.m_Data->Count - index)));
			}
			this.m_Data->Values[(IntPtr)index * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)] = value;
			this.m_Data->Count++;
		}

		// Token: 0x060026F2 RID: 9970 RVA: 0x0009B2E0 File Offset: 0x000994E0
		public unsafe void RemoveAt(int index)
		{
			bool flag = (ulong)index >= (ulong)((long)this.m_Data->Count);
			if (flag)
			{
				throw new ArgumentOutOfRangeException();
			}
			this.m_Data->Count--;
			UnsafeUtility.MemMove((void*)(this.m_Data->Values + (IntPtr)index * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)), (void*)(this.m_Data->Values + (IntPtr)index * (IntPtr)sizeof(T) / (IntPtr)sizeof(T) + sizeof(T) / sizeof(T)), (long)(UnsafeUtility.SizeOf<T>() * (this.m_Data->Count - index)));
			this.m_Data->Values[(IntPtr)this.m_Data->Count * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)] = default(T);
		}

		// Token: 0x060026F3 RID: 9971 RVA: 0x0009B38B File Offset: 0x0009958B
		private unsafe void IncreaseCapacity()
		{
			this.EnsureCapacity(this.m_Data->Capacity * 2);
		}

		// Token: 0x060026F4 RID: 9972 RVA: 0x0009B3A4 File Offset: 0x000995A4
		private unsafe void EnsureCapacity(int capacity)
		{
			bool flag = capacity <= this.m_Data->Capacity;
			if (!flag)
			{
				this.ResizeCapacity(capacity);
			}
		}

		// Token: 0x060026F5 RID: 9973 RVA: 0x0009B3D4 File Offset: 0x000995D4
		private unsafe void ResizeCapacity(int capacity)
		{
			Assert.IsTrue(capacity > 0);
			this.m_Data->Values = (T*)LayoutList<T>.ResizeArray((void*)this.m_Data->Values, (long)this.m_Data->Capacity, (long)capacity, (long)UnsafeUtility.SizeOf<T>(), 16, this.m_Allocator);
			this.m_Data->Capacity = capacity;
		}

		// Token: 0x060026F6 RID: 9974 RVA: 0x0009B430 File Offset: 0x00099630
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

		// Token: 0x0400140D RID: 5133
		private readonly Allocator m_Allocator;

		// Token: 0x0400140E RID: 5134
		private unsafe LayoutList<T>.Data* m_Data;

		// Token: 0x02000598 RID: 1432
		private struct Data
		{
			// Token: 0x0400140F RID: 5135
			public int Capacity;

			// Token: 0x04001410 RID: 5136
			public int Count;

			// Token: 0x04001411 RID: 5137
			public unsafe T* Values;
		}
	}
}
