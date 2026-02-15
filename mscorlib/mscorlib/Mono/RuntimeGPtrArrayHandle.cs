using System;
using System.Runtime.CompilerServices;

namespace Mono
{
	// Token: 0x02000038 RID: 56
	internal struct RuntimeGPtrArrayHandle
	{
		// Token: 0x0600007B RID: 123 RVA: 0x00002A01 File Offset: 0x00000C01
		internal unsafe RuntimeGPtrArrayHandle(IntPtr ptr)
		{
			this.value = (RuntimeStructs.GPtrArray*)(void*)ptr;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002A0F File Offset: 0x00000C0F
		internal unsafe int Length
		{
			get
			{
				return this.value->len;
			}
		}

		// Token: 0x1700000C RID: 12
		internal IntPtr this[int i]
		{
			get
			{
				return this.Lookup(i);
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002A25 File Offset: 0x00000C25
		internal unsafe IntPtr Lookup(int i)
		{
			if (i >= 0 && i < this.Length)
			{
				return this.value->data[i];
			}
			throw new IndexOutOfRangeException();
		}

		// Token: 0x0600007F RID: 127
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void GPtrArrayFree(RuntimeStructs.GPtrArray* value);

		// Token: 0x06000080 RID: 128 RVA: 0x00002A50 File Offset: 0x00000C50
		internal static void DestroyAndFree(ref RuntimeGPtrArrayHandle h)
		{
			RuntimeGPtrArrayHandle.GPtrArrayFree(h.value);
			h.value = null;
		}

		// Token: 0x04000111 RID: 273
		private unsafe RuntimeStructs.GPtrArray* value;
	}
}
