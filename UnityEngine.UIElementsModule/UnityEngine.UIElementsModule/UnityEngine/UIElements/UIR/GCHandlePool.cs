using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x020004FC RID: 1276
	internal class GCHandlePool : IDisposable
	{
		// Token: 0x060023B1 RID: 9137 RVA: 0x00082DD3 File Offset: 0x00080FD3
		public GCHandlePool(int capacity = 256, int allocBatchSize = 64)
		{
			this.m_Handles = new List<GCHandle>(capacity);
			this.m_UsedHandlesCount = 0;
			this.k_AllocBatchSize = allocBatchSize;
		}

		// Token: 0x060023B2 RID: 9138 RVA: 0x00082DF8 File Offset: 0x00080FF8
		public GCHandle Get(object target)
		{
			bool flag = target == null;
			GCHandle gchandle;
			if (flag)
			{
				gchandle = default(GCHandle);
			}
			else
			{
				bool flag2 = this.m_UsedHandlesCount < this.m_Handles.Count;
				if (flag2)
				{
					List<GCHandle> handles = this.m_Handles;
					int usedHandlesCount = this.m_UsedHandlesCount;
					this.m_UsedHandlesCount = usedHandlesCount + 1;
					GCHandle h = handles[usedHandlesCount];
					h.Target = target;
					gchandle = h;
				}
				else
				{
					GCHandle h2 = GCHandle.Alloc(target);
					this.m_Handles.Add(h2);
					this.m_UsedHandlesCount++;
					int i = 0;
					int count = this.k_AllocBatchSize - 1;
					while (i < count)
					{
						this.m_Handles.Add(GCHandle.Alloc(null));
						i++;
					}
					gchandle = h2;
				}
			}
			return gchandle;
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x00082EC4 File Offset: 0x000810C4
		public IntPtr GetIntPtr(object target)
		{
			bool flag = target == null;
			IntPtr intPtr;
			if (flag)
			{
				intPtr = IntPtr.Zero;
			}
			else
			{
				intPtr = GCHandle.ToIntPtr(this.Get(target));
			}
			return intPtr;
		}

		// Token: 0x060023B4 RID: 9140 RVA: 0x00082EF4 File Offset: 0x000810F4
		public void ReturnAll()
		{
			for (int i = 0; i < this.m_UsedHandlesCount; i++)
			{
				GCHandle h = this.m_Handles[i];
				h.Target = null;
				this.m_Handles[i] = h;
			}
			this.m_UsedHandlesCount = 0;
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x060023B5 RID: 9141 RVA: 0x00082F44 File Offset: 0x00081144
		// (set) Token: 0x060023B6 RID: 9142 RVA: 0x00082F4C File Offset: 0x0008114C
		internal bool disposed { get; private set; }

		// Token: 0x060023B7 RID: 9143 RVA: 0x00082F55 File Offset: 0x00081155
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060023B8 RID: 9144 RVA: 0x00082F68 File Offset: 0x00081168
		private void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					foreach (GCHandle h in this.m_Handles)
					{
						bool isAllocated = h.IsAllocated;
						if (isAllocated)
						{
							h.Free();
						}
					}
					this.m_Handles = null;
				}
				this.disposed = true;
			}
		}

		// Token: 0x0400103F RID: 4159
		private List<GCHandle> m_Handles;

		// Token: 0x04001040 RID: 4160
		private int m_UsedHandlesCount;

		// Token: 0x04001041 RID: 4161
		private readonly int k_AllocBatchSize;
	}
}
