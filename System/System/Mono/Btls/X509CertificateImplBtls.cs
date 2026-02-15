using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Win32.SafeHandles;
using Mono.Security.Authenticode;
using Mono.Security.Cryptography;

namespace Mono.Btls
{
	// Token: 0x020000D3 RID: 211
	internal class X509CertificateImplBtls : X509Certificate2ImplUnix
	{
		// Token: 0x060003E7 RID: 999 RVA: 0x0000D2EF File Offset: 0x0000B4EF
		internal X509CertificateImplBtls(MonoBtlsX509 x509)
		{
			this.x509 = x509.Copy();
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000D304 File Offset: 0x0000B504
		private X509CertificateImplBtls(X509CertificateImplBtls other)
		{
			this.x509 = ((other.x509 != null) ? other.x509.Copy() : null);
			this.nativePrivateKey = ((other.nativePrivateKey != null) ? other.nativePrivateKey.Copy() : null);
			if (other.intermediateCerts != null)
			{
				this.intermediateCerts = other.intermediateCerts.Clone();
			}
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000D368 File Offset: 0x0000B568
		internal X509CertificateImplBtls(byte[] data, MonoBtlsX509Format format)
		{
			this.x509 = MonoBtlsX509.LoadFromData(data, format);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000D380 File Offset: 0x0000B580
		internal X509CertificateImplBtls(byte[] data, SafePasswordHandle password, X509KeyStorageFlags keyStorageFlags)
		{
			if (password == null || password.IsInvalid)
			{
				try
				{
					this.Import(data);
					return;
				}
				catch (Exception ex)
				{
					try
					{
						this.ImportPkcs12(data, null);
					}
					catch
					{
						try
						{
							this.ImportAuthenticode(data);
						}
						catch
						{
							throw new CryptographicException(global::Locale.GetText("Unable to decode certificate."), ex);
						}
					}
					return;
				}
			}
			try
			{
				this.ImportPkcs12(data, password);
			}
			catch (Exception ex2)
			{
				try
				{
					this.Import(data);
				}
				catch
				{
					try
					{
						this.ImportAuthenticode(data);
					}
					catch
					{
						throw new CryptographicException(global::Locale.GetText("Unable to decode certificate."), ex2);
					}
				}
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000D454 File Offset: 0x0000B654
		public override bool IsValid
		{
			get
			{
				return this.x509 != null && this.x509.IsValid;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x0000D46B File Offset: 0x0000B66B
		internal MonoBtlsX509 X509
		{
			get
			{
				base.ThrowIfContextInvalid();
				return this.x509;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000D479 File Offset: 0x0000B679
		internal MonoBtlsKey NativePrivateKey
		{
			get
			{
				base.ThrowIfContextInvalid();
				return this.nativePrivateKey;
			}
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000D487 File Offset: 0x0000B687
		public override X509CertificateImpl Clone()
		{
			base.ThrowIfContextInvalid();
			return new X509CertificateImplBtls(this);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000D495 File Offset: 0x0000B695
		protected override byte[] GetRawCertData()
		{
			base.ThrowIfContextInvalid();
			return this.X509.GetRawData(MonoBtlsX509Format.DER);
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0000D4A9 File Offset: 0x0000B6A9
		internal override X509CertificateImplCollection IntermediateCertificates
		{
			get
			{
				return this.intermediateCerts;
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000D4B1 File Offset: 0x0000B6B1
		protected override void Dispose(bool disposing)
		{
			if (this.x509 != null)
			{
				this.x509.Dispose();
				this.x509 = null;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0000D4CD File Offset: 0x0000B6CD
		public override bool HasPrivateKey
		{
			get
			{
				return this.nativePrivateKey != null;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0000D4D8 File Offset: 0x0000B6D8
		// (set) Token: 0x060003F4 RID: 1012 RVA: 0x0000D4F8 File Offset: 0x0000B6F8
		public override AsymmetricAlgorithm PrivateKey
		{
			get
			{
				if (this.nativePrivateKey == null)
				{
					return null;
				}
				return PKCS8.PrivateKeyInfo.DecodeRSA(this.nativePrivateKey.GetBytes(true));
			}
			set
			{
				if (this.nativePrivateKey != null)
				{
					this.nativePrivateKey.Dispose();
				}
				try
				{
					if (value != null)
					{
						this.nativePrivateKey = MonoBtlsKey.CreateFromRSAPrivateKey((RSA)value);
					}
				}
				catch
				{
					this.nativePrivateKey = null;
				}
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000D4D8 File Offset: 0x0000B6D8
		public override RSA GetRSAPrivateKey()
		{
			if (this.nativePrivateKey == null)
			{
				return null;
			}
			return PKCS8.PrivateKeyInfo.DecodeRSA(this.nativePrivateKey.GetBytes(true));
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000D54C File Offset: 0x0000B74C
		public override DSA GetDSAPrivateKey()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000D553 File Offset: 0x0000B753
		private void Import(byte[] data)
		{
			if (data != null)
			{
				if (data.Length != 0 && data[0] != 48)
				{
					this.x509 = MonoBtlsX509.LoadFromData(data, MonoBtlsX509Format.PEM);
					return;
				}
				this.x509 = MonoBtlsX509.LoadFromData(data, MonoBtlsX509Format.DER);
			}
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000D580 File Offset: 0x0000B780
		private void ImportPkcs12(byte[] data, SafePasswordHandle password)
		{
			using (MonoBtlsPkcs12 monoBtlsPkcs = new MonoBtlsPkcs12())
			{
				if (password == null || password.IsInvalid)
				{
					try
					{
						monoBtlsPkcs.Import(data, null);
						goto IL_0046;
					}
					catch
					{
						using (SafePasswordHandle safePasswordHandle = new SafePasswordHandle(string.Empty))
						{
							monoBtlsPkcs.Import(data, safePasswordHandle);
						}
						goto IL_0046;
					}
				}
				monoBtlsPkcs.Import(data, password);
				IL_0046:
				this.x509 = monoBtlsPkcs.GetCertificate(0);
				if (monoBtlsPkcs.HasPrivateKey)
				{
					this.nativePrivateKey = monoBtlsPkcs.GetPrivateKey();
				}
				if (monoBtlsPkcs.Count > 1)
				{
					this.intermediateCerts = new X509CertificateImplCollection();
					for (int i = 0; i < monoBtlsPkcs.Count; i++)
					{
						using (MonoBtlsX509 certificate = monoBtlsPkcs.GetCertificate(i))
						{
							if (MonoBtlsX509.Compare(certificate, this.x509) != 0)
							{
								X509CertificateImplBtls x509CertificateImplBtls = new X509CertificateImplBtls(certificate);
								this.intermediateCerts.Add(x509CertificateImplBtls, true);
							}
						}
					}
				}
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000D694 File Offset: 0x0000B894
		private void ImportAuthenticode(byte[] data)
		{
			if (data != null)
			{
				AuthenticodeDeformatter authenticodeDeformatter = new AuthenticodeDeformatter(data);
				this.Import(authenticodeDeformatter.SigningCertificate.RawData);
			}
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000D6BC File Offset: 0x0000B8BC
		public override bool Verify(X509Certificate2 thisCertificate)
		{
			bool flag;
			using (MonoBtlsX509Chain monoBtlsX509Chain = new MonoBtlsX509Chain())
			{
				monoBtlsX509Chain.AddCertificate(this.x509.Copy());
				if (this.intermediateCerts != null)
				{
					for (int i = 0; i < this.intermediateCerts.Count; i++)
					{
						X509CertificateImplBtls x509CertificateImplBtls = (X509CertificateImplBtls)this.intermediateCerts[i];
						monoBtlsX509Chain.AddCertificate(x509CertificateImplBtls.x509.Copy());
					}
				}
				flag = MonoBtlsProvider.ValidateCertificate(monoBtlsX509Chain, null);
			}
			return flag;
		}

		// Token: 0x0400032E RID: 814
		private MonoBtlsX509 x509;

		// Token: 0x0400032F RID: 815
		private MonoBtlsKey nativePrivateKey;

		// Token: 0x04000330 RID: 816
		private X509CertificateImplCollection intermediateCerts;
	}
}
