using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Btls
{
	// Token: 0x020000D4 RID: 212
	internal class X509ChainImplBtls : X509ChainImpl
	{
		// Token: 0x060003FB RID: 1019 RVA: 0x0000D748 File Offset: 0x0000B948
		internal X509ChainImplBtls(MonoBtlsX509Chain chain)
		{
			this.chain = chain.Copy();
			this.policy = new X509ChainPolicy();
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000D768 File Offset: 0x0000B968
		internal X509ChainImplBtls(MonoBtlsX509StoreCtx storeCtx)
		{
			this.storeCtx = storeCtx.Copy();
			this.chain = storeCtx.GetChain();
			this.policy = new X509ChainPolicy();
			this.untrustedChain = storeCtx.GetUntrusted();
			if (this.untrustedChain != null)
			{
				this.untrusted = new X509Certificate2Collection();
				this.policy.ExtraStore = this.untrusted;
				for (int i = 0; i < this.untrustedChain.Count; i++)
				{
					using (X509CertificateImplBtls x509CertificateImplBtls = new X509CertificateImplBtls(this.untrustedChain.GetCertificate(i)))
					{
						this.untrusted.Add(new X509Certificate2(x509CertificateImplBtls));
					}
				}
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x0000D824 File Offset: 0x0000BA24
		public override bool IsValid
		{
			get
			{
				return this.chain != null && this.chain.IsValid;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x0000D83B File Offset: 0x0000BA3B
		internal MonoBtlsX509StoreCtx StoreCtx
		{
			get
			{
				base.ThrowIfContextInvalid();
				return this.storeCtx;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0000D84C File Offset: 0x0000BA4C
		public override X509ChainElementCollection ChainElements
		{
			get
			{
				base.ThrowIfContextInvalid();
				if (this.elements != null)
				{
					return this.elements;
				}
				this.elements = new X509ChainElementCollection();
				this.certificates = new X509Certificate2[this.chain.Count];
				for (int i = 0; i < this.certificates.Length; i++)
				{
					using (X509CertificateImplBtls x509CertificateImplBtls = new X509CertificateImplBtls(this.chain.GetCertificate(i)))
					{
						this.certificates[i] = new X509Certificate2(x509CertificateImplBtls);
					}
					this.elements.Add(this.certificates[i]);
				}
				return this.elements;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x0000D8F8 File Offset: 0x0000BAF8
		public override X509ChainPolicy ChainPolicy
		{
			get
			{
				return this.policy;
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000D900 File Offset: 0x0000BB00
		public override void AddStatus(X509ChainStatusFlags errorCode)
		{
			if (this.chainStatusList == null)
			{
				this.chainStatusList = new List<X509ChainStatus>();
			}
			this.chainStatusList.Add(new X509ChainStatus(errorCode));
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x000028AE File Offset: 0x00000AAE
		public override bool Build(X509Certificate2 certificate)
		{
			return false;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000D928 File Offset: 0x0000BB28
		public override void Reset()
		{
			if (this.certificates != null)
			{
				X509Certificate2[] array = this.certificates;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].Dispose();
				}
				this.certificates = null;
			}
			if (this.elements != null)
			{
				this.elements.Clear();
				this.elements = null;
			}
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000D97C File Offset: 0x0000BB7C
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.chain != null)
				{
					this.chain.Dispose();
					this.chain = null;
				}
				if (this.storeCtx != null)
				{
					this.storeCtx.Dispose();
					this.storeCtx = null;
				}
				if (this.untrustedChain != null)
				{
					this.untrustedChain.Dispose();
					this.untrustedChain = null;
				}
				if (this.untrusted != null)
				{
					foreach (X509Certificate2 x509Certificate in this.untrusted)
					{
						x509Certificate.Dispose();
					}
					this.untrusted = null;
				}
				if (this.certificates != null)
				{
					X509Certificate2[] array = this.certificates;
					for (int i = 0; i < array.Length; i++)
					{
						array[i].Dispose();
					}
					this.certificates = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000331 RID: 817
		private MonoBtlsX509StoreCtx storeCtx;

		// Token: 0x04000332 RID: 818
		private MonoBtlsX509Chain chain;

		// Token: 0x04000333 RID: 819
		private MonoBtlsX509Chain untrustedChain;

		// Token: 0x04000334 RID: 820
		private X509ChainElementCollection elements;

		// Token: 0x04000335 RID: 821
		private X509Certificate2Collection untrusted;

		// Token: 0x04000336 RID: 822
		private X509Certificate2[] certificates;

		// Token: 0x04000337 RID: 823
		private X509ChainPolicy policy;

		// Token: 0x04000338 RID: 824
		private List<X509ChainStatus> chainStatusList;
	}
}
