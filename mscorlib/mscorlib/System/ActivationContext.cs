using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Unity;

namespace System
{
	/// <summary>Identifies the activation context for the current application. This class cannot be inherited. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001BE RID: 446
	[ComVisible(false)]
	[Serializable]
	public sealed class ActivationContext : IDisposable, ISerializable
	{
		// Token: 0x06001166 RID: 4454 RVA: 0x00047E54 File Offset: 0x00046054
		~ActivationContext()
		{
			this.Dispose(false);
		}

		/// <summary>Gets the application identity for the current application.</summary>
		/// <returns>An <see cref="T:System.ApplicationIdentity" /> object that identifies the current application.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06001167 RID: 4455 RVA: 0x00047E84 File Offset: 0x00046084
		public ApplicationIdentity Identity
		{
			get
			{
				return this._appid;
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.ActivationContext" />. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001168 RID: 4456 RVA: 0x00047E8C File Offset: 0x0004608C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x00047E9B File Offset: 0x0004609B
		private void Dispose(bool disposing)
		{
			if (this._disposed)
			{
				this._disposed = true;
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="info">The object to populate with data.</param>
		/// <param name="context">The structure for this serialization.</param>
		// Token: 0x0600116A RID: 4458 RVA: 0x00047EAE File Offset: 0x000460AE
		[MonoTODO("Missing serialization support")]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x000176B9 File Offset: 0x000158B9
		internal ActivationContext()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000713 RID: 1811
		private ApplicationIdentity _appid;

		// Token: 0x04000714 RID: 1812
		private bool _disposed;
	}
}
