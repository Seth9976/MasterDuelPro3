using System;
using System.Collections;
using System.Reflection;
using System.Security.Cryptography;
using Mono.Security.Cryptography;

namespace Mono.Security.X509
{
	// Token: 0x02000017 RID: 23
	[DefaultMember("Item")]
	public class X509Crl
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x00006D7C File Offset: 0x00004F7C
		public X509Crl(byte[] crl)
		{
			if (crl == null)
			{
				throw new ArgumentNullException("crl");
			}
			this.encoded = (byte[])crl.Clone();
			this.Parse(this.encoded);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00006DB0 File Offset: 0x00004FB0
		private void Parse(byte[] crl)
		{
			string text = "Input data cannot be coded as a valid CRL.";
			try
			{
				ASN1 asn = new ASN1(this.encoded);
				if (asn.Tag != 48 || asn.Count != 3)
				{
					throw new CryptographicException(text);
				}
				ASN1 asn2 = asn[0];
				if (asn2.Tag != 48 || asn2.Count < 3)
				{
					throw new CryptographicException(text);
				}
				int num = 0;
				if (asn2[num].Tag == 2)
				{
					this.version = asn2[num++].Value[0] + 1;
				}
				else
				{
					this.version = 1;
				}
				this.signatureOID = ASN1Convert.ToOid(asn2[num++][0]);
				this.issuer = X501.ToString(asn2[num++]);
				this.thisUpdate = ASN1Convert.ToDateTime(asn2[num++]);
				ASN1 asn3 = asn2[num++];
				if (asn3.Tag == 23 || asn3.Tag == 24)
				{
					this.nextUpdate = ASN1Convert.ToDateTime(asn3);
					asn3 = asn2[num++];
				}
				this.entries = new ArrayList();
				if (asn3 != null && asn3.Tag == 48)
				{
					ASN1 asn4 = asn3;
					for (int i = 0; i < asn4.Count; i++)
					{
						this.entries.Add(new X509Crl.X509CrlEntry(asn4[i]));
					}
				}
				else
				{
					num--;
				}
				ASN1 asn5 = asn2[num];
				if (asn5 != null && asn5.Tag == 160 && asn5.Count == 1)
				{
					this.extensions = new X509ExtensionCollection(asn5[0]);
				}
				else
				{
					this.extensions = new X509ExtensionCollection(null);
				}
				string text2 = ASN1Convert.ToOid(asn[1][0]);
				if (this.signatureOID != text2)
				{
					throw new CryptographicException(text + " [Non-matching signature algorithms in CRL]");
				}
				byte[] value = asn[2].Value;
				this.signature = new byte[value.Length - 1];
				Buffer.BlockCopy(value, 1, this.signature, 0, this.signature.Length);
			}
			catch
			{
				throw new CryptographicException(text);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00006FF0 File Offset: 0x000051F0
		public X509ExtensionCollection Extensions
		{
			get
			{
				return this.extensions;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00006FF8 File Offset: 0x000051F8
		public byte[] Hash
		{
			get
			{
				if (this.hash_value == null)
				{
					byte[] bytes = new ASN1(this.encoded)[0].GetBytes();
					using (HashAlgorithm hashAlgorithm = PKCS1.CreateFromOid(this.signatureOID))
					{
						this.hash_value = hashAlgorithm.ComputeHash(bytes);
					}
				}
				return this.hash_value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00007060 File Offset: 0x00005260
		public string IssuerName
		{
			get
			{
				return this.issuer;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00007068 File Offset: 0x00005268
		public DateTime NextUpdate
		{
			get
			{
				return this.nextUpdate;
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00007070 File Offset: 0x00005270
		private bool Compare(byte[] array1, byte[] array2)
		{
			if (array1 == null && array2 == null)
			{
				return true;
			}
			if (array1 == null || array2 == null)
			{
				return false;
			}
			if (array1.Length != array2.Length)
			{
				return false;
			}
			for (int i = 0; i < array1.Length; i++)
			{
				if (array1[i] != array2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000070B0 File Offset: 0x000052B0
		public X509Crl.X509CrlEntry GetCrlEntry(X509Certificate x509)
		{
			if (x509 == null)
			{
				throw new ArgumentNullException("x509");
			}
			return this.GetCrlEntry(x509.SerialNumber);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000070CC File Offset: 0x000052CC
		public X509Crl.X509CrlEntry GetCrlEntry(byte[] serialNumber)
		{
			if (serialNumber == null)
			{
				throw new ArgumentNullException("serialNumber");
			}
			for (int i = 0; i < this.entries.Count; i++)
			{
				X509Crl.X509CrlEntry x509CrlEntry = (X509Crl.X509CrlEntry)this.entries[i];
				if (this.Compare(serialNumber, x509CrlEntry.SerialNumber))
				{
					return x509CrlEntry;
				}
			}
			return null;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00007124 File Offset: 0x00005324
		internal bool VerifySignature(DSA dsa)
		{
			if (this.signatureOID != "1.2.840.10040.4.3")
			{
				throw new CryptographicException("Unsupported hash algorithm: " + this.signatureOID);
			}
			DSASignatureDeformatter dsasignatureDeformatter = new DSASignatureDeformatter(dsa);
			dsasignatureDeformatter.SetHashAlgorithm("SHA1");
			ASN1 asn = new ASN1(this.signature);
			if (asn == null || asn.Count != 2)
			{
				return false;
			}
			byte[] value = asn[0].Value;
			byte[] value2 = asn[1].Value;
			byte[] array = new byte[40];
			int num = Math.Max(0, value.Length - 20);
			int num2 = Math.Max(0, 20 - value.Length);
			Buffer.BlockCopy(value, num, array, num2, value.Length - num);
			int num3 = Math.Max(0, value2.Length - 20);
			int num4 = Math.Max(20, 40 - value2.Length);
			Buffer.BlockCopy(value2, num3, array, num4, value2.Length - num3);
			return dsasignatureDeformatter.VerifySignature(this.Hash, array);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00007213 File Offset: 0x00005413
		internal bool VerifySignature(RSA rsa)
		{
			RSAPKCS1SignatureDeformatter rsapkcs1SignatureDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
			rsapkcs1SignatureDeformatter.SetHashAlgorithm(PKCS1.HashNameFromOid(this.signatureOID, true));
			return rsapkcs1SignatureDeformatter.VerifySignature(this.Hash, this.signature);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00007240 File Offset: 0x00005440
		public bool VerifySignature(AsymmetricAlgorithm aa)
		{
			if (aa == null)
			{
				throw new ArgumentNullException("aa");
			}
			if (aa is RSA)
			{
				return this.VerifySignature(aa as RSA);
			}
			if (aa is DSA)
			{
				return this.VerifySignature(aa as DSA);
			}
			throw new NotSupportedException("Unknown Asymmetric Algorithm " + aa.ToString());
		}

		// Token: 0x0400004E RID: 78
		private string issuer;

		// Token: 0x0400004F RID: 79
		private byte version;

		// Token: 0x04000050 RID: 80
		private DateTime thisUpdate;

		// Token: 0x04000051 RID: 81
		private DateTime nextUpdate;

		// Token: 0x04000052 RID: 82
		private ArrayList entries;

		// Token: 0x04000053 RID: 83
		private string signatureOID;

		// Token: 0x04000054 RID: 84
		private byte[] signature;

		// Token: 0x04000055 RID: 85
		private X509ExtensionCollection extensions;

		// Token: 0x04000056 RID: 86
		private byte[] encoded;

		// Token: 0x04000057 RID: 87
		private byte[] hash_value;

		// Token: 0x02000018 RID: 24
		public class X509CrlEntry
		{
			// Token: 0x060000AF RID: 175 RVA: 0x0000729C File Offset: 0x0000549C
			internal X509CrlEntry(ASN1 entry)
			{
				this.sn = entry[0].Value;
				Array.Reverse<byte>(this.sn);
				this.revocationDate = ASN1Convert.ToDateTime(entry[1]);
				this.extensions = new X509ExtensionCollection(entry[2]);
			}

			// Token: 0x17000028 RID: 40
			// (get) Token: 0x060000B0 RID: 176 RVA: 0x000072F0 File Offset: 0x000054F0
			public byte[] SerialNumber
			{
				get
				{
					return (byte[])this.sn.Clone();
				}
			}

			// Token: 0x17000029 RID: 41
			// (get) Token: 0x060000B1 RID: 177 RVA: 0x00007302 File Offset: 0x00005502
			public DateTime RevocationDate
			{
				get
				{
					return this.revocationDate;
				}
			}

			// Token: 0x1700002A RID: 42
			// (get) Token: 0x060000B2 RID: 178 RVA: 0x0000730A File Offset: 0x0000550A
			public X509ExtensionCollection Extensions
			{
				get
				{
					return this.extensions;
				}
			}

			// Token: 0x04000058 RID: 88
			private byte[] sn;

			// Token: 0x04000059 RID: 89
			private DateTime revocationDate;

			// Token: 0x0400005A RID: 90
			private X509ExtensionCollection extensions;
		}
	}
}
