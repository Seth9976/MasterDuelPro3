using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections
{
	// Token: 0x020000D7 RID: 215
	internal struct UnmanagedArray<[global::System.Runtime.CompilerServices.IsUnmanaged] T> : IDisposable where T : struct, ValueType
	{
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x0001D20B File Offset: 0x0001B40B
		public int Length
		{
			get
			{
				return this.m_length;
			}
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0001D213 File Offset: 0x0001B413
		public unsafe UnmanagedArray(int length, AllocatorManager.AllocatorHandle allocator)
		{
			this.m_pointer = (IntPtr)((void*)Memory.Unmanaged.Array.Allocate<T>((long)length, allocator));
			this.m_length = length;
			this.m_allocator = allocator;
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0001D236 File Offset: 0x0001B436
		public unsafe void Dispose()
		{
			Memory.Unmanaged.Free<T>((T*)(void*)this.m_pointer, Allocator.Persistent);
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0001D24E File Offset: 0x0001B44E
		public unsafe T* GetUnsafePointer()
		{
			return (T*)(void*)this.m_pointer;
		}

		// Token: 0x1700011F RID: 287
		public unsafe ref T this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref *(T*)((byte*)(void*)this.m_pointer + (IntPtr)index * (IntPtr)sizeof(T));
			}
		}

		// Token: 0x04000413 RID: 1043
		private IntPtr m_pointer;

		// Token: 0x04000414 RID: 1044
		private int m_length;

		// Token: 0x04000415 RID: 1045
		private AllocatorManager.AllocatorHandle m_allocator;
	}
}
