using System;
using System.Runtime.InteropServices;

namespace System.Buffers
{
	// Token: 0x02000784 RID: 1924
	public struct MemoryHandle : IDisposable
	{
		// Token: 0x06003CEE RID: 15598 RVA: 0x000EAC40 File Offset: 0x000E8E40
		[CLSCompliant(false)]
		public unsafe MemoryHandle(void* pointer, GCHandle handle = default(GCHandle), IPinnable pinnable = null)
		{
			this._pointer = pointer;
			this._handle = handle;
			this._pinnable = pinnable;
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06003CEF RID: 15599 RVA: 0x000EAC57 File Offset: 0x000E8E57
		[CLSCompliant(false)]
		public unsafe void* Pointer
		{
			get
			{
				return this._pointer;
			}
		}

		// Token: 0x06003CF0 RID: 15600 RVA: 0x000EAC5F File Offset: 0x000E8E5F
		public void Dispose()
		{
			if (this._handle.IsAllocated)
			{
				this._handle.Free();
			}
			if (this._pinnable != null)
			{
				this._pinnable.Unpin();
				this._pinnable = null;
			}
			this._pointer = null;
		}

		// Token: 0x04001F62 RID: 8034
		private unsafe void* _pointer;

		// Token: 0x04001F63 RID: 8035
		private GCHandle _handle;

		// Token: 0x04001F64 RID: 8036
		private IPinnable _pinnable;
	}
}
