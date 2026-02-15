using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020001C9 RID: 457
	internal abstract class X509ChainImpl : IDisposable
	{
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000AFE RID: 2814
		public abstract bool IsValid { get; }

		// Token: 0x06000AFF RID: 2815 RVA: 0x00037769 File Offset: 0x00035969
		protected void ThrowIfContextInvalid()
		{
			if (!this.IsValid)
			{
				throw X509Helper2.GetInvalidChainContextException();
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000B00 RID: 2816
		public abstract X509ChainElementCollection ChainElements { get; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000B01 RID: 2817
		public abstract X509ChainPolicy ChainPolicy { get; }

		// Token: 0x06000B02 RID: 2818
		public abstract bool Build(X509Certificate2 certificate);

		// Token: 0x06000B03 RID: 2819
		public abstract void AddStatus(X509ChainStatusFlags errorCode);

		// Token: 0x06000B04 RID: 2820
		public abstract void Reset();

		// Token: 0x06000B05 RID: 2821 RVA: 0x00037779 File Offset: 0x00035979
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00002FA0 File Offset: 0x000011A0
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00037788 File Offset: 0x00035988
		~X509ChainImpl()
		{
			this.Dispose(false);
		}
	}
}
