using System;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x02000254 RID: 596
	internal sealed class SingleAssignmentDisposable : IDisposable
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x0002E4D4 File Offset: 0x0002C6D4
		public bool IsDisposed
		{
			get
			{
				object obj = this.gate;
				bool flag2;
				lock (obj)
				{
					flag2 = this.disposed;
				}
				return flag2;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000D4F RID: 3407 RVA: 0x0002E518 File Offset: 0x0002C718
		// (set) Token: 0x06000D50 RID: 3408 RVA: 0x0002E520 File Offset: 0x0002C720
		public IDisposable Disposable
		{
			get
			{
				return this.current;
			}
			set
			{
				IDisposable old = null;
				object obj = this.gate;
				bool alreadyDisposed;
				lock (obj)
				{
					alreadyDisposed = this.disposed;
					old = this.current;
					if (!alreadyDisposed)
					{
						if (value == null)
						{
							return;
						}
						this.current = value;
					}
				}
				if (alreadyDisposed && value != null)
				{
					value.Dispose();
					return;
				}
				if (old != null)
				{
					throw new InvalidOperationException("Disposable is already set");
				}
			}
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0002E594 File Offset: 0x0002C794
		public void Dispose()
		{
			IDisposable old = null;
			object obj = this.gate;
			lock (obj)
			{
				if (!this.disposed)
				{
					this.disposed = true;
					old = this.current;
					this.current = null;
				}
			}
			if (old != null)
			{
				old.Dispose();
			}
		}

		// Token: 0x040006B6 RID: 1718
		private readonly object gate = new object();

		// Token: 0x040006B7 RID: 1719
		private IDisposable current;

		// Token: 0x040006B8 RID: 1720
		private bool disposed;
	}
}
