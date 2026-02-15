using System;

namespace Mono
{
	// Token: 0x02000047 RID: 71
	internal struct SafeGPtrArrayHandle : IDisposable
	{
		// Token: 0x06000087 RID: 135 RVA: 0x00002B9D File Offset: 0x00000D9D
		internal SafeGPtrArrayHandle(IntPtr ptr)
		{
			this.handle = new RuntimeGPtrArrayHandle(ptr);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002BAB File Offset: 0x00000DAB
		public void Dispose()
		{
			RuntimeGPtrArrayHandle.DestroyAndFree(ref this.handle);
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00002BB8 File Offset: 0x00000DB8
		internal int Length
		{
			get
			{
				return this.handle.Length;
			}
		}

		// Token: 0x1700000E RID: 14
		internal IntPtr this[int i]
		{
			get
			{
				return this.handle[i];
			}
		}

		// Token: 0x0400013B RID: 315
		private RuntimeGPtrArrayHandle handle;
	}
}
