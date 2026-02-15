using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.Rendering
{
	// Token: 0x02000050 RID: 80
	public struct ListBuffer<[IsUnmanaged] T> where T : struct, ValueType
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x00008A89 File Offset: 0x00006C89
		internal unsafe T* BufferPtr
		{
			get
			{
				return this.m_BufferPtr;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x00008A91 File Offset: 0x00006C91
		public unsafe int Count
		{
			get
			{
				return *this.m_CountPtr;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x00008A9A File Offset: 0x00006C9A
		public int Capacity
		{
			get
			{
				return this.m_Capacity;
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00008AA2 File Offset: 0x00006CA2
		public unsafe ListBuffer(T* bufferPtr, int* countPtr, int capacity)
		{
			this.m_BufferPtr = bufferPtr;
			this.m_Capacity = capacity;
			this.m_CountPtr = countPtr;
		}

		// Token: 0x1700003A RID: 58
		public unsafe ref T this[in int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new IndexOutOfRangeException(string.Format("Expected a value between 0 and {0}, but received {1}.", this.Count, index));
				}
				return ref this.m_BufferPtr[(IntPtr)index * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00008B0B File Offset: 0x00006D0B
		public unsafe ref T GetUnchecked(in int index)
		{
			return ref this.m_BufferPtr[(IntPtr)index * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00008B1E File Offset: 0x00006D1E
		public unsafe bool TryAdd(in T value)
		{
			if (this.Count >= this.m_Capacity)
			{
				return false;
			}
			this.m_BufferPtr[(IntPtr)this.Count * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)] = value;
			(*this.m_CountPtr)++;
			return true;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00008B5C File Offset: 0x00006D5C
		public unsafe void CopyTo(T* dstBuffer, int startDstIndex, int copyCount)
		{
			UnsafeUtility.MemCpy((void*)(dstBuffer + (IntPtr)startDstIndex * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)), (void*)this.m_BufferPtr, (long)(UnsafeUtility.SizeOf<T>() * copyCount));
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00008B7C File Offset: 0x00006D7C
		public unsafe bool TryCopyTo(ListBuffer<T> other)
		{
			if (other.Count + this.Count >= other.m_Capacity)
			{
				return false;
			}
			UnsafeUtility.MemCpy((void*)(other.m_BufferPtr + (IntPtr)other.Count * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)), (void*)this.m_BufferPtr, (long)(UnsafeUtility.SizeOf<T>() * this.Count));
			*other.m_CountPtr += this.Count;
			return true;
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00008BE0 File Offset: 0x00006DE0
		public unsafe bool TryCopyFrom(T* srcPtr, int count)
		{
			if (count + this.Count > this.m_Capacity)
			{
				return false;
			}
			UnsafeUtility.MemCpy((void*)(this.m_BufferPtr + (IntPtr)this.Count * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)), (void*)srcPtr, (long)(UnsafeUtility.SizeOf<T>() * count));
			*this.m_CountPtr += count;
			return true;
		}

		// Token: 0x04000123 RID: 291
		private unsafe T* m_BufferPtr;

		// Token: 0x04000124 RID: 292
		private int m_Capacity;

		// Token: 0x04000125 RID: 293
		private unsafe int* m_CountPtr;
	}
}
