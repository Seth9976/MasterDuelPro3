using System;

namespace System.Data.Common
{
	/// <summary>The base class for a transaction. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000101 RID: 257
	public abstract class DbTransaction : MarshalByRefObject, IDisposable, IAsyncDisposable
	{
		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Data.Common.DbTransaction" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D7B RID: 3451 RVA: 0x000453F1 File Offset: 0x000435F1
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Data.Common.DbTransaction" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">If true, this method releases all resources held by any managed objects that this <see cref="T:System.Data.Common.DbTransaction" /> references.</param>
		// Token: 0x06000D7C RID: 3452 RVA: 0x00003FD2 File Offset: 0x000021D2
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>Rolls back a transaction from a pending state.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D7D RID: 3453
		public abstract void Rollback();
	}
}
