using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x020000C0 RID: 192
	internal sealed class NativeRingQueueDebugView<[global::System.Runtime.CompilerServices.IsUnmanaged] T> where T : struct, ValueType
	{
		// Token: 0x060008FC RID: 2300 RVA: 0x0001B126 File Offset: 0x00019326
		public NativeRingQueueDebugView(NativeRingQueue<T> data)
		{
			this.Data = data.m_RingQueue;
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x0001B13C File Offset: 0x0001933C
		public unsafe T[] Items
		{
			get
			{
				T[] result = new T[this.Data->Length];
				int read = this.Data->m_Read;
				int capacity = this.Data->m_Capacity;
				for (int i = 0; i < result.Length; i++)
				{
					result[i] = this.Data->Ptr[(IntPtr)((read + i) % capacity) * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
				}
				return result;
			}
		}

		// Token: 0x040003EB RID: 1003
		private unsafe UnsafeRingQueue<T>* Data;
	}
}
