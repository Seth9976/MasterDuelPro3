using System;
using System.Security;
using System.Security.Cryptography;
using Mono.Security.Cryptography;
using Mono.Security.X509;

namespace Mono.Security.Authenticode
{
	// Token: 0x02000054 RID: 84
	public class AuthenticodeDeformatter : AuthenticodeBase
	{
		// Token: 0x060001E5 RID: 485 RVA: 0x0000C376 File Offset: 0x0000A576
		public AuthenticodeDeformatter()
		{
			this.reason = -1;
			this.signerChain = new X509Chain();
			this.timestampChain = new X509Chain();
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000C39B File Offset: 0x0000A59B
		public AuthenticodeDeformatter(byte[] rawData)
			: this()
		{
			this.RawData = rawData;
		}

		// Token: 0x17000085 RID: 133
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x0000C3AC File Offset: 0x0000A5AC
		public byte[] RawData
		{
			set
			{
				this.Reset();
				this.rawdata = value;
				try
				{
					this.CheckSignature();
				}
				catch (SecurityException)
				{
					throw;
				}
				catch
				{
					this.reason = 1;
				}
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000C3F8 File Offset: 0x0000A5F8
		public X509Certificate SigningCertificate
		{
			get
			{
				return this.signingCertificate;
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000C400 File Offset: 0x0000A600
		private bool CheckSignature()
		{
			if (this.filename != null)
			{
				base.Open(this.filename);
			}
			else
			{
				base.Open(this.rawdata);
			}
			this.entry = base.GetSecurityEntry();
			if (this.entry == null)
			{
				this.reason = 1;
				base.Close();
				return false;
			}
			PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo(this.entry);
			if (contentInfo.ContentType != "1.2.840.113549.1.7.2")
			{
				base.Close();
				return false;
			}
			PKCS7.SignedData signedData = new PKCS7.SignedData(contentInfo.Content);
			if (signedData.ContentInfo.ContentType != "1.3.6.1.4.1.311.2.1.4")
			{
				base.Close();
				return false;
			}
			this.coll = signedData.Certificates;
			ASN1 content = signedData.ContentInfo.Content;
			this.signedHash = content[0][1][1];
			int length = this.signedHash.Length;
			HashAlgorithm hashAlgorithm;
			if (length <= 20)
			{
				if (length == 16)
				{
					hashAlgorithm = MD5.Create();
					this.hash = base.GetHash(hashAlgorithm);
					goto IL_0176;
				}
				if (length == 20)
				{
					hashAlgorithm = SHA1.Create();
					this.hash = base.GetHash(hashAlgorithm);
					goto IL_0176;
				}
			}
			else
			{
				if (length == 32)
				{
					hashAlgorithm = SHA256.Create();
					this.hash = base.GetHash(hashAlgorithm);
					goto IL_0176;
				}
				if (length == 48)
				{
					hashAlgorithm = SHA384.Create();
					this.hash = base.GetHash(hashAlgorithm);
					goto IL_0176;
				}
				if (length == 64)
				{
					hashAlgorithm = SHA512.Create();
					this.hash = base.GetHash(hashAlgorithm);
					goto IL_0176;
				}
			}
			this.reason = 5;
			base.Close();
			return false;
			IL_0176:
			base.Close();
			if (!this.signedHash.CompareValue(this.hash))
			{
				this.reason = 2;
			}
			byte[] value = content[0].Value;
			hashAlgorithm.Initialize();
			byte[] array = hashAlgorithm.ComputeHash(value);
			return this.VerifySignature(signedData, array, hashAlgorithm) && this.reason == 0;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000C5D8 File Offset: 0x0000A7D8
		private bool CompareIssuerSerial(string issuer, byte[] serial, X509Certificate x509)
		{
			if (issuer != x509.IssuerName)
			{
				return false;
			}
			if (serial.Length != x509.SerialNumber.Length)
			{
				return false;
			}
			int num = serial.Length;
			for (int i = 0; i < serial.Length; i++)
			{
				if (serial[i] != x509.SerialNumber[--num])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000C62C File Offset: 0x0000A82C
		private bool VerifySignature(PKCS7.SignedData sd, byte[] calculatedMessageDigest, HashAlgorithm ha)
		{
			string text = null;
			ASN1 asn = null;
			for (int i = 0; i < sd.SignerInfo.AuthenticatedAttributes.Count; i++)
			{
				ASN1 asn2 = (ASN1)sd.SignerInfo.AuthenticatedAttributes[i];
				string text2 = ASN1Convert.ToOid(asn2[0]);
				if (!(text2 == "1.2.840.113549.1.9.3"))
				{
					if (!(text2 == "1.2.840.113549.1.9.4"))
					{
						if (!(text2 == "1.3.6.1.4.1.311.2.1.11") && !(text2 == "1.3.6.1.4.1.311.2.1.12"))
						{
						}
					}
					else
					{
						asn = asn2[1][0];
					}
				}
				else
				{
					text = ASN1Convert.ToOid(asn2[1][0]);
				}
			}
			if (text != "1.3.6.1.4.1.311.2.1.4")
			{
				return false;
			}
			if (asn == null)
			{
				return false;
			}
			if (!asn.CompareValue(calculatedMessageDigest))
			{
				return false;
			}
			string text3 = CryptoConfig.MapNameToOID(ha.ToString());
			ASN1 asn3 = new ASN1(49);
			foreach (object obj in sd.SignerInfo.AuthenticatedAttributes)
			{
				ASN1 asn4 = (ASN1)obj;
				asn3.Add(asn4);
			}
			ha.Initialize();
			byte[] array = ha.ComputeHash(asn3.GetBytes());
			byte[] signature = sd.SignerInfo.Signature;
			string issuerName = sd.SignerInfo.IssuerName;
			byte[] serialNumber = sd.SignerInfo.SerialNumber;
			foreach (X509Certificate x509Certificate in this.coll)
			{
				if (this.CompareIssuerSerial(issuerName, serialNumber, x509Certificate) && x509Certificate.PublicKey.Length > signature.Length >> 3)
				{
					this.signingCertificate = x509Certificate;
					if (((RSACryptoServiceProvider)x509Certificate.RSA).VerifyHash(array, text3, signature))
					{
						this.signerChain.LoadCertificates(this.coll);
						this.trustedRoot = this.signerChain.Build(x509Certificate);
						break;
					}
				}
			}
			if (sd.SignerInfo.UnauthenticatedAttributes.Count == 0)
			{
				this.trustedTimestampRoot = true;
			}
			else
			{
				for (int j = 0; j < sd.SignerInfo.UnauthenticatedAttributes.Count; j++)
				{
					ASN1 asn5 = (ASN1)sd.SignerInfo.UnauthenticatedAttributes[j];
					if (ASN1Convert.ToOid(asn5[0]) == "1.2.840.113549.1.9.6")
					{
						PKCS7.SignerInfo signerInfo = new PKCS7.SignerInfo(asn5[1]);
						this.trustedTimestampRoot = this.VerifyCounterSignature(signerInfo, signature);
					}
				}
			}
			return this.trustedRoot && this.trustedTimestampRoot;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000C8F8 File Offset: 0x0000AAF8
		private bool VerifyCounterSignature(PKCS7.SignerInfo cs, byte[] signature)
		{
			if (cs.Version > 1)
			{
				return false;
			}
			string text = null;
			ASN1 asn = null;
			for (int i = 0; i < cs.AuthenticatedAttributes.Count; i++)
			{
				ASN1 asn2 = (ASN1)cs.AuthenticatedAttributes[i];
				string text2 = ASN1Convert.ToOid(asn2[0]);
				if (!(text2 == "1.2.840.113549.1.9.3"))
				{
					if (!(text2 == "1.2.840.113549.1.9.4"))
					{
						if (text2 == "1.2.840.113549.1.9.5")
						{
							this.timestamp = ASN1Convert.ToDateTime(asn2[1][0]);
						}
					}
					else
					{
						asn = asn2[1][0];
					}
				}
				else
				{
					text = ASN1Convert.ToOid(asn2[1][0]);
				}
			}
			if (text != "1.2.840.113549.1.7.1")
			{
				return false;
			}
			if (asn == null)
			{
				return false;
			}
			string text3 = null;
			int length = asn.Length;
			if (length <= 20)
			{
				if (length != 16)
				{
					if (length == 20)
					{
						text3 = "SHA1";
					}
				}
				else
				{
					text3 = "MD5";
				}
			}
			else if (length != 32)
			{
				if (length != 48)
				{
					if (length == 64)
					{
						text3 = "SHA512";
					}
				}
				else
				{
					text3 = "SHA384";
				}
			}
			else
			{
				text3 = "SHA256";
			}
			HashAlgorithm hashAlgorithm = HashAlgorithm.Create(text3);
			if (!asn.CompareValue(hashAlgorithm.ComputeHash(signature)))
			{
				return false;
			}
			byte[] signature2 = cs.Signature;
			ASN1 asn3 = new ASN1(49);
			foreach (object obj in cs.AuthenticatedAttributes)
			{
				ASN1 asn4 = (ASN1)obj;
				asn3.Add(asn4);
			}
			byte[] array = hashAlgorithm.ComputeHash(asn3.GetBytes());
			string issuerName = cs.IssuerName;
			byte[] serialNumber = cs.SerialNumber;
			foreach (X509Certificate x509Certificate in this.coll)
			{
				if (this.CompareIssuerSerial(issuerName, serialNumber, x509Certificate) && x509Certificate.PublicKey.Length > signature2.Length)
				{
					RSACryptoServiceProvider rsacryptoServiceProvider = (RSACryptoServiceProvider)x509Certificate.RSA;
					RSAManaged rsamanaged = new RSAManaged();
					rsamanaged.ImportParameters(rsacryptoServiceProvider.ExportParameters(false));
					if (PKCS1.Verify_v15(rsamanaged, hashAlgorithm, array, signature2, true))
					{
						this.timestampChain.LoadCertificates(this.coll);
						return this.timestampChain.Build(x509Certificate);
					}
				}
			}
			return false;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000CB84 File Offset: 0x0000AD84
		private void Reset()
		{
			this.filename = null;
			this.rawdata = null;
			this.entry = null;
			this.hash = null;
			this.signedHash = null;
			this.signingCertificate = null;
			this.reason = -1;
			this.trustedRoot = false;
			this.trustedTimestampRoot = false;
			this.signerChain.Reset();
			this.timestampChain.Reset();
			this.timestamp = DateTime.MinValue;
		}

		// Token: 0x04000229 RID: 553
		private string filename;

		// Token: 0x0400022A RID: 554
		private byte[] rawdata;

		// Token: 0x0400022B RID: 555
		private byte[] hash;

		// Token: 0x0400022C RID: 556
		private X509CertificateCollection coll;

		// Token: 0x0400022D RID: 557
		private ASN1 signedHash;

		// Token: 0x0400022E RID: 558
		private DateTime timestamp;

		// Token: 0x0400022F RID: 559
		private X509Certificate signingCertificate;

		// Token: 0x04000230 RID: 560
		private int reason;

		// Token: 0x04000231 RID: 561
		private bool trustedRoot;

		// Token: 0x04000232 RID: 562
		private bool trustedTimestampRoot;

		// Token: 0x04000233 RID: 563
		private byte[] entry;

		// Token: 0x04000234 RID: 564
		private X509Chain signerChain;

		// Token: 0x04000235 RID: 565
		private X509Chain timestampChain;
	}
}
