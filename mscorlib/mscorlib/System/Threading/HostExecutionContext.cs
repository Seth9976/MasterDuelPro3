using System;

namespace System.Threading
{
	/// <summary>Encapsulates and propagates the host execution context across threads. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200027B RID: 635
	[MonoTODO("Useless until the runtime supports it")]
	public class HostExecutionContext : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.HostExecutionContext" /> class. </summary>
		// Token: 0x0600178D RID: 6029 RVA: 0x0005B991 File Offset: 0x00059B91
		public HostExecutionContext()
		{
			this._state = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.HostExecutionContext" /> class using the specified state. </summary>
		/// <param name="state">An object representing the host execution context state.</param>
		// Token: 0x0600178E RID: 6030 RVA: 0x0005B9A0 File Offset: 0x00059BA0
		public HostExecutionContext(object state)
		{
			this._state = state;
		}

		/// <summary>Creates a copy of the current host execution context.</summary>
		/// <returns>A <see cref="T:System.Threading.HostExecutionContext" /> object representing the host context for the current thread.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600178F RID: 6031 RVA: 0x0005B9AF File Offset: 0x00059BAF
		public virtual HostExecutionContext CreateCopy()
		{
			return new HostExecutionContext(this._state);
		}

		/// <summary>Gets or sets the state of the host execution context.</summary>
		/// <returns>An object representing the host execution context state.</returns>
		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06001790 RID: 6032 RVA: 0x0005B9BC File Offset: 0x00059BBC
		// (set) Token: 0x06001791 RID: 6033 RVA: 0x0005B9C4 File Offset: 0x00059BC4
		protected internal object State
		{
			get
			{
				return this._state;
			}
			set
			{
				this._state = value;
			}
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Threading.HostExecutionContext" /> class.</summary>
		// Token: 0x06001792 RID: 6034 RVA: 0x0005B9CD File Offset: 0x00059BCD
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>When overridden in a derived class, releases the unmanaged resources used by the <see cref="T:System.Threading.WaitHandle" />, and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06001793 RID: 6035 RVA: 0x00002C89 File Offset: 0x00000E89
		public virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x04000B36 RID: 2870
		private object _state;
	}
}
