using System;
using System.Collections.Generic;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020001C4 RID: 452
	internal class X509CertificateImplCollection : IDisposable
	{
		// Token: 0x06000AD0 RID: 2768 RVA: 0x00037102 File Offset: 0x00035302
		public X509CertificateImplCollection()
		{
			this.list = new List<X509CertificateImpl>();
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x00037118 File Offset: 0x00035318
		private X509CertificateImplCollection(X509CertificateImplCollection other)
		{
			this.list = new List<X509CertificateImpl>();
			foreach (X509CertificateImpl x509CertificateImpl in other.list)
			{
				this.list.Add(x509CertificateImpl.Clone());
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x00037188 File Offset: 0x00035388
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x170001F9 RID: 505
		public X509CertificateImpl this[int index]
		{
			get
			{
				return this.list[index];
			}
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x000371A3 File Offset: 0x000353A3
		public void Add(X509CertificateImpl impl, bool takeOwnership)
		{
			if (!takeOwnership)
			{
				impl = impl.Clone();
			}
			this.list.Add(impl);
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x000371BC File Offset: 0x000353BC
		public X509CertificateImplCollection Clone()
		{
			return new X509CertificateImplCollection(this);
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x000371C4 File Offset: 0x000353C4
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x000371D4 File Offset: 0x000353D4
		protected virtual void Dispose(bool disposing)
		{
			foreach (X509CertificateImpl x509CertificateImpl in this.list)
			{
				try
				{
					x509CertificateImpl.Dispose();
				}
				catch
				{
				}
			}
			this.list.Clear();
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x00037244 File Offset: 0x00035444
		~X509CertificateImplCollection()
		{
			this.Dispose(false);
		}

		// Token: 0x0400082F RID: 2095
		private List<X509CertificateImpl> list;
	}
}
