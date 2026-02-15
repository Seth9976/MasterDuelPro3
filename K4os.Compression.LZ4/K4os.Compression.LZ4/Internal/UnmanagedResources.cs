using System;
using System.Threading;

namespace K4os.Compression.LZ4.Internal
{
	// Token: 0x0200000A RID: 10
	public abstract class UnmanagedResources : IDisposable
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000028C4 File Offset: 0x00000AC4
		public bool IsDisposed
		{
			get
			{
				return Interlocked.CompareExchange(ref this._disposed, 0, 0) != 0;
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000028D6 File Offset: 0x00000AD6
		protected void ThrowIfDisposed()
		{
			if (this.IsDisposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName + " is already disposed");
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000028C2 File Offset: 0x00000AC2
		protected virtual void ReleaseUnmanaged()
		{
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000028C2 File Offset: 0x00000AC2
		protected virtual void ReleaseManaged()
		{
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000028FB File Offset: 0x00000AFB
		protected virtual void Dispose(bool disposing)
		{
			if (Interlocked.CompareExchange(ref this._disposed, 1, 0) != 0)
			{
				return;
			}
			this.ReleaseUnmanaged();
			if (disposing)
			{
				this.ReleaseManaged();
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000291C File Offset: 0x00000B1C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000292C File Offset: 0x00000B2C
		~UnmanagedResources()
		{
			this.Dispose(false);
		}

		// Token: 0x04000024 RID: 36
		private int _disposed;
	}
}
