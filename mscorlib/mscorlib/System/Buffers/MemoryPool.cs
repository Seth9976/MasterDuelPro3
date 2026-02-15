using System;

namespace System.Buffers
{
	// Token: 0x0200078D RID: 1933
	public abstract class MemoryPool<T> : IDisposable
	{
		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06003D0F RID: 15631 RVA: 0x000EB4D2 File Offset: 0x000E96D2
		public static MemoryPool<T> Shared
		{
			get
			{
				return MemoryPool<T>.s_shared;
			}
		}

		// Token: 0x06003D10 RID: 15632
		public abstract IMemoryOwner<T> Rent(int minBufferSize = -1);

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06003D11 RID: 15633
		public abstract int MaxBufferSize { get; }

		// Token: 0x06003D13 RID: 15635 RVA: 0x000EB4D9 File Offset: 0x000E96D9
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003D14 RID: 15636
		protected abstract void Dispose(bool disposing);

		// Token: 0x04001F74 RID: 8052
		private static readonly MemoryPool<T> s_shared = new ArrayMemoryPool<T>();
	}
}
