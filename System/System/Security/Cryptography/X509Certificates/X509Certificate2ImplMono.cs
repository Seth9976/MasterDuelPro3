using System;
using Microsoft.Win32.SafeHandles;
using Mono.Security.Authenticode;
using Mono.Security.Cryptography;
using Mono.Security.X509;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020001C0 RID: 448
	internal class X509Certificate2ImplMono : X509Certificate2ImplUnix
	{
		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x0003680A File Offset: 0x00034A0A
		public override bool IsValid
		{
			get
			{
				return this._cert != null;
			}
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x00036815 File Offset: 0x00034A15
		public X509Certificate2ImplMono(X509Certificate cert)
		{
			this._cert = cert;
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x00036824 File Offset: 0x00034A24
		private X509Certificate2ImplMono(X509Certificate2ImplMono other)
		{
			this._cert = other._cert;
			if (other.intermediateCerts != null)
			{
				this.intermediateCerts = other.intermediateCerts.Clone();
			}
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x00036854 File Offset: 0x00034A54
		public X509Certificate2ImplMono(byte[] rawData, SafePasswordHandle password, X509KeyStorageFlags keyStorageFlags)
		{
			switch (X509Certificate2.GetCertContentType(rawData))
			{
			case X509ContentType.Cert:
			case X509ContentType.Pkcs7:
				this._cert = new X509Certificate(rawData);
				return;
			case X509ContentType.Pfx:
				this._cert = this.ImportPkcs12(rawData, password);
				return;
			case X509ContentType.Authenticode:
			{
				AuthenticodeDeformatter authenticodeDeformatter = new AuthenticodeDeformatter(rawData);
				this._cert = authenticodeDeformatter.SigningCertificate;
				if (this._cert != null)
				{
					return;
				}
				break;
			}
			}
			throw new CryptographicException(global::Locale.GetText("Unable to decode certificate."));
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x000368D7 File Offset: 0x00034AD7
		public override X509CertificateImpl Clone()
		{
			base.ThrowIfContextInvalid();
			return new X509Certificate2ImplMono(this);
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x000368E5 File Offset: 0x00034AE5
		private X509Certificate Cert
		{
			get
			{
				base.ThrowIfContextInvalid();
				return this._cert;
			}
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x000368F3 File Offset: 0x00034AF3
		protected override byte[] GetRawCertData()
		{
			base.ThrowIfContextInvalid();
			return this.Cert.RawData;
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x00036906 File Offset: 0x00034B06
		public override bool HasPrivateKey
		{
			get
			{
				return this.PrivateKey != null;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x00036914 File Offset: 0x00034B14
		// (set) Token: 0x06000AA3 RID: 2723 RVA: 0x00036A10 File Offset: 0x00034C10
		public override AsymmetricAlgorithm PrivateKey
		{
			get
			{
				if (this._cert == null)
				{
					throw new CryptographicException(X509Certificate2ImplMono.empty_error);
				}
				try
				{
					RSACryptoServiceProvider rsacryptoServiceProvider = this._cert.RSA as RSACryptoServiceProvider;
					if (rsacryptoServiceProvider != null)
					{
						if (rsacryptoServiceProvider.PublicOnly)
						{
							return null;
						}
						RSACryptoServiceProvider rsacryptoServiceProvider2 = new RSACryptoServiceProvider();
						rsacryptoServiceProvider2.ImportParameters(this._cert.RSA.ExportParameters(true));
						return rsacryptoServiceProvider2;
					}
					else
					{
						Mono.Security.Cryptography.RSAManaged rsamanaged = this._cert.RSA as Mono.Security.Cryptography.RSAManaged;
						if (rsamanaged != null)
						{
							if (rsamanaged.PublicOnly)
							{
								return null;
							}
							Mono.Security.Cryptography.RSAManaged rsamanaged2 = new Mono.Security.Cryptography.RSAManaged();
							rsamanaged2.ImportParameters(this._cert.RSA.ExportParameters(true));
							return rsamanaged2;
						}
						else
						{
							DSACryptoServiceProvider dsacryptoServiceProvider = this._cert.DSA as DSACryptoServiceProvider;
							if (dsacryptoServiceProvider != null)
							{
								if (dsacryptoServiceProvider.PublicOnly)
								{
									return null;
								}
								DSACryptoServiceProvider dsacryptoServiceProvider2 = new DSACryptoServiceProvider();
								dsacryptoServiceProvider2.ImportParameters(this._cert.DSA.ExportParameters(true));
								return dsacryptoServiceProvider2;
							}
						}
					}
				}
				catch
				{
				}
				return null;
			}
			set
			{
				if (this._cert == null)
				{
					throw new CryptographicException(X509Certificate2ImplMono.empty_error);
				}
				if (value == null)
				{
					this._cert.RSA = null;
					this._cert.DSA = null;
					return;
				}
				if (value is RSA)
				{
					this._cert.RSA = (RSA)value;
					return;
				}
				if (value is DSA)
				{
					this._cert.DSA = (DSA)value;
					return;
				}
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x00036A85 File Offset: 0x00034C85
		public override RSA GetRSAPrivateKey()
		{
			return this.PrivateKey as RSA;
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x00036A92 File Offset: 0x00034C92
		public override DSA GetDSAPrivateKey()
		{
			return this.PrivateKey as DSA;
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x00036AA0 File Offset: 0x00034CA0
		private X509Certificate ImportPkcs12(byte[] rawData, SafePasswordHandle password)
		{
			if (password == null || password.IsInvalid)
			{
				return this.ImportPkcs12(rawData, null);
			}
			string text = password.Mono_DangerousGetString();
			return this.ImportPkcs12(rawData, text);
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x00036AD0 File Offset: 0x00034CD0
		private X509Certificate ImportPkcs12(byte[] rawData, string password)
		{
			PKCS12 pkcs = null;
			if (string.IsNullOrEmpty(password))
			{
				try
				{
					pkcs = new PKCS12(rawData, null);
					goto IL_002B;
				}
				catch
				{
					pkcs = new PKCS12(rawData, string.Empty);
					goto IL_002B;
				}
			}
			pkcs = new PKCS12(rawData, password);
			IL_002B:
			if (pkcs.Certificates.Count == 0)
			{
				return null;
			}
			if (pkcs.Keys.Count == 0)
			{
				return pkcs.Certificates[0];
			}
			X509Certificate x509Certificate = null;
			AsymmetricAlgorithm asymmetricAlgorithm = pkcs.Keys[0] as AsymmetricAlgorithm;
			string text = asymmetricAlgorithm.ToXmlString(false);
			foreach (X509Certificate x509Certificate2 in pkcs.Certificates)
			{
				if ((x509Certificate2.RSA != null && text == x509Certificate2.RSA.ToXmlString(false)) || (x509Certificate2.DSA != null && text == x509Certificate2.DSA.ToXmlString(false)))
				{
					x509Certificate = x509Certificate2;
					break;
				}
			}
			if (x509Certificate == null)
			{
				x509Certificate = pkcs.Certificates[0];
			}
			else
			{
				x509Certificate.RSA = asymmetricAlgorithm as RSA;
				x509Certificate.DSA = asymmetricAlgorithm as DSA;
			}
			if (pkcs.Certificates.Count > 1)
			{
				this.intermediateCerts = new X509CertificateImplCollection();
				foreach (X509Certificate x509Certificate3 in pkcs.Certificates)
				{
					if (x509Certificate3 != x509Certificate)
					{
						X509Certificate2ImplMono x509Certificate2ImplMono = new X509Certificate2ImplMono(x509Certificate3);
						this.intermediateCerts.Add(x509Certificate2ImplMono, true);
					}
				}
			}
			return x509Certificate;
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x00036C88 File Offset: 0x00034E88
		[MonoTODO("by default this depends on the incomplete X509Chain")]
		public override bool Verify(X509Certificate2 thisCertificate)
		{
			if (this._cert == null)
			{
				throw new CryptographicException(X509Certificate2ImplMono.empty_error);
			}
			return X509Chain.Create().Build(thisCertificate);
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x00036CAD File Offset: 0x00034EAD
		internal override X509CertificateImplCollection IntermediateCertificates
		{
			get
			{
				return this.intermediateCerts;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x00036CB5 File Offset: 0x00034EB5
		internal X509Certificate MonoCertificate
		{
			get
			{
				return this._cert;
			}
		}

		// Token: 0x04000828 RID: 2088
		private X509CertificateImplCollection intermediateCerts;

		// Token: 0x04000829 RID: 2089
		private X509Certificate _cert;

		// Token: 0x0400082A RID: 2090
		private static string empty_error = global::Locale.GetText("Certificate instance is empty.");

		// Token: 0x0400082B RID: 2091
		private static byte[] signedData = new byte[] { 42, 134, 72, 134, 247, 13, 1, 7, 2 };
	}
}
