using System;
using System.Net;
using Mono.Security.X509.Extensions;

namespace Mono.Security.X509
{
	// Token: 0x0200001C RID: 28
	public class X509Chain
	{
		// Token: 0x060000E1 RID: 225 RVA: 0x00008001 File Offset: 0x00006201
		public X509Chain()
		{
			this.certs = new X509CertificateCollection();
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00008014 File Offset: 0x00006214
		public X509CertificateCollection TrustAnchors
		{
			get
			{
				if (this.roots == null)
				{
					this.roots = new X509CertificateCollection();
					this.roots.AddRange(X509StoreManager.TrustedRootCertificates);
					return this.roots;
				}
				return this.roots;
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00008046 File Offset: 0x00006246
		public void LoadCertificates(X509CertificateCollection collection)
		{
			this.certs.AddRange(collection);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00008054 File Offset: 0x00006254
		public bool Build(X509Certificate leaf)
		{
			this._status = X509ChainStatusFlags.NoError;
			if (this._chain == null)
			{
				this._chain = new X509CertificateCollection();
				X509Certificate x509Certificate = leaf;
				X509Certificate x509Certificate2 = x509Certificate;
				while (x509Certificate != null && !x509Certificate.IsSelfSigned)
				{
					x509Certificate2 = x509Certificate;
					this._chain.Add(x509Certificate);
					x509Certificate = this.FindCertificateParent(x509Certificate);
				}
				this._root = this.FindCertificateRoot(x509Certificate2);
			}
			else
			{
				int count = this._chain.Count;
				if (count > 0)
				{
					if (this.IsParent(leaf, this._chain[0]))
					{
						int num = 1;
						while (num < count && this.IsParent(this._chain[num - 1], this._chain[num]))
						{
							num++;
						}
						if (num == count)
						{
							this._root = this.FindCertificateRoot(this._chain[count - 1]);
						}
					}
				}
				else
				{
					this._root = this.FindCertificateRoot(leaf);
				}
			}
			if (this._chain != null && this._status == X509ChainStatusFlags.NoError)
			{
				foreach (X509Certificate x509Certificate3 in this._chain)
				{
					if (!this.IsValid(x509Certificate3))
					{
						return false;
					}
				}
				if (!this.IsValid(leaf))
				{
					if (this._status == X509ChainStatusFlags.NotTimeNested)
					{
						this._status = X509ChainStatusFlags.NotTimeValid;
					}
					return false;
				}
				if (this._root != null && !this.IsValid(this._root))
				{
					return false;
				}
			}
			IL_0161:
			return this._status == X509ChainStatusFlags.NoError;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000081E0 File Offset: 0x000063E0
		public void Reset()
		{
			this._status = X509ChainStatusFlags.NoError;
			this.roots = null;
			this.certs.Clear();
			if (this._chain != null)
			{
				this._chain.Clear();
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000820E File Offset: 0x0000640E
		private bool IsValid(X509Certificate cert)
		{
			if (!cert.IsCurrent)
			{
				this._status = X509ChainStatusFlags.NotTimeNested;
				return false;
			}
			bool checkCertificateRevocationList = ServicePointManager.CheckCertificateRevocationList;
			return true;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00008228 File Offset: 0x00006428
		private X509Certificate FindCertificateParent(X509Certificate child)
		{
			foreach (X509Certificate x509Certificate in this.certs)
			{
				if (this.IsParent(child, x509Certificate))
				{
					return x509Certificate;
				}
			}
			return null;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00008288 File Offset: 0x00006488
		private X509Certificate FindCertificateRoot(X509Certificate potentialRoot)
		{
			if (potentialRoot == null)
			{
				this._status = X509ChainStatusFlags.PartialChain;
				return null;
			}
			if (this.IsTrusted(potentialRoot))
			{
				return potentialRoot;
			}
			foreach (X509Certificate x509Certificate in this.TrustAnchors)
			{
				if (this.IsParent(potentialRoot, x509Certificate))
				{
					return x509Certificate;
				}
			}
			if (potentialRoot.IsSelfSigned)
			{
				this._status = X509ChainStatusFlags.UntrustedRoot;
				return potentialRoot;
			}
			this._status = X509ChainStatusFlags.PartialChain;
			return null;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00008320 File Offset: 0x00006520
		private bool IsTrusted(X509Certificate potentialTrusted)
		{
			return this.TrustAnchors.Contains(potentialTrusted);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00008330 File Offset: 0x00006530
		private bool IsParent(X509Certificate child, X509Certificate parent)
		{
			if (child.IssuerName != parent.SubjectName)
			{
				return false;
			}
			if (parent.Version > 2 && !this.IsTrusted(parent))
			{
				X509Extension x509Extension = parent.Extensions["2.5.29.19"];
				if (x509Extension != null)
				{
					if (!new BasicConstraintsExtension(x509Extension).CertificateAuthority)
					{
						this._status = X509ChainStatusFlags.InvalidBasicConstraints;
					}
				}
				else
				{
					this._status = X509ChainStatusFlags.InvalidBasicConstraints;
				}
			}
			if (!child.VerifySignature(parent.RSA))
			{
				this._status = X509ChainStatusFlags.NotSignatureValid;
				return false;
			}
			return true;
		}

		// Token: 0x04000073 RID: 115
		private X509CertificateCollection roots;

		// Token: 0x04000074 RID: 116
		private X509CertificateCollection certs;

		// Token: 0x04000075 RID: 117
		private X509Certificate _root;

		// Token: 0x04000076 RID: 118
		private X509CertificateCollection _chain;

		// Token: 0x04000077 RID: 119
		private X509ChainStatusFlags _status;
	}
}
