using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x0200000C RID: 12
	internal class GCHandlePool
	{
		// Token: 0x06000061 RID: 97 RVA: 0x00002C54 File Offset: 0x00000E54
		public GCHandlePool()
		{
			this.m_handles = new GCHandle[128];
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002C70 File Offset: 0x00000E70
		public GCHandle Alloc(object o)
		{
			bool flag = this.m_current > 0;
			GCHandle gchandle;
			if (flag)
			{
				GCHandle[] handles = this.m_handles;
				int num = this.m_current - 1;
				this.m_current = num;
				GCHandle handle = handles[num];
				handle.Target = o;
				gchandle = handle;
			}
			else
			{
				gchandle = GCHandle.Alloc(o);
			}
			return gchandle;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002CC0 File Offset: 0x00000EC0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IntPtr AllocHandleIfNotNull(object o)
		{
			bool flag = o == null;
			IntPtr intPtr;
			if (flag)
			{
				intPtr = IntPtr.Zero;
			}
			else
			{
				intPtr = (IntPtr)this.Alloc(o);
			}
			return intPtr;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002CF0 File Offset: 0x00000EF0
		public void Free(GCHandle h)
		{
			bool flag = this.m_current == this.m_handles.Length;
			if (flag)
			{
				int newLength = this.m_handles.Length * 2;
				GCHandle[] newHandles = new GCHandle[newLength];
				Array.Copy(this.m_handles, newHandles, this.m_handles.Length);
				this.m_handles = newHandles;
			}
			h.Target = null;
			GCHandle[] handles = this.m_handles;
			int current = this.m_current;
			this.m_current = current + 1;
			handles[current] = h;
		}

		// Token: 0x04000021 RID: 33
		private GCHandle[] m_handles;

		// Token: 0x04000022 RID: 34
		private int m_current;
	}
}
