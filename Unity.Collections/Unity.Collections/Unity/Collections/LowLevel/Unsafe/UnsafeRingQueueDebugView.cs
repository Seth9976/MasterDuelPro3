using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200013F RID: 319
	internal sealed class UnsafeRingQueueDebugView<[global::System.Runtime.CompilerServices.IsUnmanaged] T> where T : struct, ValueType
	{
		// Token: 0x06000D81 RID: 3457 RVA: 0x00029B0B File Offset: 0x00027D0B
		public UnsafeRingQueueDebugView(UnsafeRingQueue<T> data)
		{
			this.Data = data;
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000D82 RID: 3458 RVA: 0x00029B1C File Offset: 0x00027D1C
		public unsafe T[] Items
		{
			get
			{
				T[] result = new T[this.Data.Length];
				int read = this.Data.m_Read;
				int capacity = this.Data.m_Capacity;
				for (int i = 0; i < result.Length; i++)
				{
					result[i] = this.Data.Ptr[(IntPtr)((read + i) % capacity) * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
				}
				return result;
			}
		}

		// Token: 0x04000524 RID: 1316
		private UnsafeRingQueue<T> Data;
	}
}
