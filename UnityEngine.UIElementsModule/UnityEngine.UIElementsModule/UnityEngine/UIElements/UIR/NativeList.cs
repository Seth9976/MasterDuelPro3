using System;
using Unity.Collections;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200053D RID: 1341
	internal class NativeList<T> : IDisposable where T : struct
	{
		// Token: 0x06002500 RID: 9472 RVA: 0x00090540 File Offset: 0x0008E740
		public NativeList(int initialCapacity)
		{
			Debug.Assert(initialCapacity > 0);
			this.m_NativeArray = new NativeArray<T>(initialCapacity, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x00090564 File Offset: 0x0008E764
		private void Expand(int newLength)
		{
			NativeArray<T> newArray = new NativeArray<T>(newLength, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			newArray.Slice(0, this.m_Count).CopyFrom(this.m_NativeArray);
			this.m_NativeArray.Dispose();
			this.m_NativeArray = newArray;
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x000905B0 File Offset: 0x0008E7B0
		public void Add(NativeSlice<T> src)
		{
			int required = this.m_Count + src.Length;
			bool flag = this.m_NativeArray.Length < required;
			if (flag)
			{
				this.Expand(required << 1);
			}
			this.m_NativeArray.Slice(this.m_Count, src.Length).CopyFrom(src);
			this.m_Count += src.Length;
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x0009061F File Offset: 0x0008E81F
		public void Clear()
		{
			this.m_Count = 0;
		}

		// Token: 0x06002504 RID: 9476 RVA: 0x0009062C File Offset: 0x0008E82C
		public NativeSlice<T> GetSlice(int start, int length)
		{
			return this.m_NativeArray.Slice(start, length);
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06002505 RID: 9477 RVA: 0x0009064B File Offset: 0x0008E84B
		public int Count
		{
			get
			{
				return this.m_Count;
			}
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06002506 RID: 9478 RVA: 0x00090653 File Offset: 0x0008E853
		// (set) Token: 0x06002507 RID: 9479 RVA: 0x0009065B File Offset: 0x0008E85B
		private protected bool disposed { protected get; private set; }

		// Token: 0x06002508 RID: 9480 RVA: 0x00090664 File Offset: 0x0008E864
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002509 RID: 9481 RVA: 0x00090678 File Offset: 0x0008E878
		protected void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.m_NativeArray.Dispose();
				}
				this.disposed = true;
			}
		}

		// Token: 0x04001216 RID: 4630
		private NativeArray<T> m_NativeArray;

		// Token: 0x04001217 RID: 4631
		private int m_Count;
	}
}
