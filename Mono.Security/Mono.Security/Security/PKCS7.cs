using System;
using System.Collections;
using Mono.Security.X509;

namespace Mono.Security
{
	// Token: 0x0200000E RID: 14
	public sealed class PKCS7
	{
		// Token: 0x0200000F RID: 15
		public class ContentInfo
		{
			// Token: 0x06000049 RID: 73 RVA: 0x00003640 File Offset: 0x00001840
			public ContentInfo()
			{
				this.content = new ASN1(160);
			}

			// Token: 0x0600004A RID: 74 RVA: 0x00003658 File Offset: 0x00001858
			public ContentInfo(string oid)
				: this()
			{
				this.contentType = oid;
			}

			// Token: 0x0600004B RID: 75 RVA: 0x00003667 File Offset: 0x00001867
			public ContentInfo(byte[] data)
				: this(new ASN1(data))
			{
			}

			// Token: 0x0600004C RID: 76 RVA: 0x00003678 File Offset: 0x00001878
			public ContentInfo(ASN1 asn1)
			{
				if (asn1.Tag != 48 || (asn1.Count < 1 && asn1.Count > 2))
				{
					throw new ArgumentException("Invalid ASN1");
				}
				if (asn1[0].Tag != 6)
				{
					throw new ArgumentException("Invalid contentType");
				}
				this.contentType = ASN1Convert.ToOid(asn1[0]);
				if (asn1.Count > 1)
				{
					if (asn1[1].Tag != 160)
					{
						throw new ArgumentException("Invalid content");
					}
					this.content = asn1[1];
				}
			}

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x0600004D RID: 77 RVA: 0x00003712 File Offset: 0x00001912
			public ASN1 ASN1
			{
				get
				{
					return this.GetASN1();
				}
			}

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x0600004E RID: 78 RVA: 0x0000371A File Offset: 0x0000191A
			// (set) Token: 0x0600004F RID: 79 RVA: 0x00003722 File Offset: 0x00001922
			public ASN1 Content
			{
				get
				{
					return this.content;
				}
				set
				{
					this.content = value;
				}
			}

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x06000050 RID: 80 RVA: 0x0000372B File Offset: 0x0000192B
			// (set) Token: 0x06000051 RID: 81 RVA: 0x00003733 File Offset: 0x00001933
			public string ContentType
			{
				get
				{
					return this.contentType;
				}
				set
				{
					this.contentType = value;
				}
			}

			// Token: 0x06000052 RID: 82 RVA: 0x0000373C File Offset: 0x0000193C
			internal ASN1 GetASN1()
			{
				ASN1 asn = new ASN1(48);
				asn.Add(ASN1Convert.FromOid(this.contentType));
				if (this.content != null && this.content.Count > 0)
				{
					asn.Add(this.content);
				}
				return asn;
			}

			// Token: 0x04000015 RID: 21
			private string contentType;

			// Token: 0x04000016 RID: 22
			private ASN1 content;
		}

		// Token: 0x02000010 RID: 16
		public class EncryptedData
		{
			// Token: 0x06000053 RID: 83 RVA: 0x00003787 File Offset: 0x00001987
			public EncryptedData()
			{
				this._version = 0;
			}

			// Token: 0x06000054 RID: 84 RVA: 0x00003798 File Offset: 0x00001998
			public EncryptedData(ASN1 asn1)
				: this()
			{
				if (asn1.Tag != 48 || asn1.Count < 2)
				{
					throw new ArgumentException("Invalid EncryptedData");
				}
				if (asn1[0].Tag != 2)
				{
					throw new ArgumentException("Invalid version");
				}
				this._version = asn1[0].Value[0];
				ASN1 asn2 = asn1[1];
				if (asn2.Tag != 48)
				{
					throw new ArgumentException("missing EncryptedContentInfo");
				}
				ASN1 asn3 = asn2[0];
				if (asn3.Tag != 6)
				{
					throw new ArgumentException("missing EncryptedContentInfo.ContentType");
				}
				this._content = new PKCS7.ContentInfo(ASN1Convert.ToOid(asn3));
				ASN1 asn4 = asn2[1];
				if (asn4.Tag != 48)
				{
					throw new ArgumentException("missing EncryptedContentInfo.ContentEncryptionAlgorithmIdentifier");
				}
				this._encryptionAlgorithm = new PKCS7.ContentInfo(ASN1Convert.ToOid(asn4[0]));
				this._encryptionAlgorithm.Content = asn4[1];
				ASN1 asn5 = asn2[2];
				if (asn5.Tag != 128)
				{
					throw new ArgumentException("missing EncryptedContentInfo.EncryptedContent");
				}
				this._encrypted = asn5.Value;
			}

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x06000055 RID: 85 RVA: 0x000038B1 File Offset: 0x00001AB1
			public PKCS7.ContentInfo EncryptionAlgorithm
			{
				get
				{
					return this._encryptionAlgorithm;
				}
			}

			// Token: 0x1700000C RID: 12
			// (get) Token: 0x06000056 RID: 86 RVA: 0x000038B9 File Offset: 0x00001AB9
			public byte[] EncryptedContent
			{
				get
				{
					if (this._encrypted == null)
					{
						return null;
					}
					return (byte[])this._encrypted.Clone();
				}
			}

			// Token: 0x04000017 RID: 23
			private byte _version;

			// Token: 0x04000018 RID: 24
			private PKCS7.ContentInfo _content;

			// Token: 0x04000019 RID: 25
			private PKCS7.ContentInfo _encryptionAlgorithm;

			// Token: 0x0400001A RID: 26
			private byte[] _encrypted;
		}

		// Token: 0x02000011 RID: 17
		public class SignedData
		{
			// Token: 0x06000057 RID: 87 RVA: 0x000038D8 File Offset: 0x00001AD8
			public SignedData(ASN1 asn1)
			{
				if (asn1[0].Tag != 48 || asn1[0].Count < 4)
				{
					throw new ArgumentException("Invalid SignedData");
				}
				if (asn1[0][0].Tag != 2)
				{
					throw new ArgumentException("Invalid version");
				}
				this.version = asn1[0][0].Value[0];
				this.contentInfo = new PKCS7.ContentInfo(asn1[0][2]);
				int num = 3;
				this.certs = new X509CertificateCollection();
				if (asn1[0][num].Tag == 160)
				{
					for (int i = 0; i < asn1[0][num].Count; i++)
					{
						this.certs.Add(new X509Certificate(asn1[0][num][i].GetBytes()));
					}
					num++;
				}
				this.crls = new ArrayList();
				if (asn1[0][num].Tag == 161)
				{
					for (int j = 0; j < asn1[0][num].Count; j++)
					{
						this.crls.Add(asn1[0][num][j].GetBytes());
					}
					num++;
				}
				if (asn1[0][num].Count > 0)
				{
					this.signerInfo = new PKCS7.SignerInfo(asn1[0][num]);
				}
				else
				{
					this.signerInfo = new PKCS7.SignerInfo();
				}
				if (this.signerInfo.HashName != null)
				{
					this.HashName = this.OidToName(this.signerInfo.HashName);
				}
				this.mda = this.signerInfo.AuthenticatedAttributes.Count > 0;
			}

			// Token: 0x1700000D RID: 13
			// (get) Token: 0x06000058 RID: 88 RVA: 0x00003AB7 File Offset: 0x00001CB7
			public X509CertificateCollection Certificates
			{
				get
				{
					return this.certs;
				}
			}

			// Token: 0x1700000E RID: 14
			// (get) Token: 0x06000059 RID: 89 RVA: 0x00003ABF File Offset: 0x00001CBF
			public PKCS7.ContentInfo ContentInfo
			{
				get
				{
					return this.contentInfo;
				}
			}

			// Token: 0x1700000F RID: 15
			// (set) Token: 0x0600005A RID: 90 RVA: 0x00003AC7 File Offset: 0x00001CC7
			public string HashName
			{
				set
				{
					this.hashAlgorithm = value;
					this.signerInfo.HashName = value;
				}
			}

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x0600005B RID: 91 RVA: 0x00003ADC File Offset: 0x00001CDC
			public PKCS7.SignerInfo SignerInfo
			{
				get
				{
					return this.signerInfo;
				}
			}

			// Token: 0x0600005C RID: 92 RVA: 0x00003AE4 File Offset: 0x00001CE4
			internal string OidToName(string oid)
			{
				if (oid == "1.3.14.3.2.26")
				{
					return "SHA1";
				}
				if (oid == "1.2.840.113549.2.2")
				{
					return "MD2";
				}
				if (oid == "1.2.840.113549.2.5")
				{
					return "MD5";
				}
				if (oid == "2.16.840.1.101.3.4.1")
				{
					return "SHA256";
				}
				if (oid == "2.16.840.1.101.3.4.2")
				{
					return "SHA384";
				}
				if (!(oid == "2.16.840.1.101.3.4.3"))
				{
					return oid;
				}
				return "SHA512";
			}

			// Token: 0x0400001B RID: 27
			private byte version;

			// Token: 0x0400001C RID: 28
			private string hashAlgorithm;

			// Token: 0x0400001D RID: 29
			private PKCS7.ContentInfo contentInfo;

			// Token: 0x0400001E RID: 30
			private X509CertificateCollection certs;

			// Token: 0x0400001F RID: 31
			private ArrayList crls;

			// Token: 0x04000020 RID: 32
			private PKCS7.SignerInfo signerInfo;

			// Token: 0x04000021 RID: 33
			private bool mda;
		}

		// Token: 0x02000012 RID: 18
		public class SignerInfo
		{
			// Token: 0x0600005D RID: 93 RVA: 0x00003B66 File Offset: 0x00001D66
			public SignerInfo()
			{
				this.version = 1;
				this.authenticatedAttributes = new ArrayList();
				this.unauthenticatedAttributes = new ArrayList();
			}

			// Token: 0x0600005E RID: 94 RVA: 0x00003B8C File Offset: 0x00001D8C
			public SignerInfo(ASN1 asn1)
				: this()
			{
				if (asn1[0].Tag != 48 || asn1[0].Count < 5)
				{
					throw new ArgumentException("Invalid SignedData");
				}
				if (asn1[0][0].Tag != 2)
				{
					throw new ArgumentException("Invalid version");
				}
				this.version = asn1[0][0].Value[0];
				ASN1 asn2 = asn1[0][1];
				if (asn2.Tag == 128 && this.version == 3)
				{
					this.ski = asn2.Value;
				}
				else
				{
					this.issuer = X501.ToString(asn2[0]);
					this.serial = asn2[1].Value;
				}
				ASN1 asn3 = asn1[0][2];
				this.hashAlgorithm = ASN1Convert.ToOid(asn3[0]);
				int num = 3;
				ASN1 asn4 = asn1[0][num];
				if (asn4.Tag == 160)
				{
					num++;
					for (int i = 0; i < asn4.Count; i++)
					{
						this.authenticatedAttributes.Add(asn4[i]);
					}
				}
				num++;
				ASN1 asn5 = asn1[0][num++];
				if (asn5.Tag == 4)
				{
					this.signature = asn5.Value;
				}
				ASN1 asn6 = asn1[0][num];
				if (asn6 != null && asn6.Tag == 161)
				{
					for (int j = 0; j < asn6.Count; j++)
					{
						this.unauthenticatedAttributes.Add(asn6[j]);
					}
				}
			}

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x0600005F RID: 95 RVA: 0x00003D3D File Offset: 0x00001F3D
			public string IssuerName
			{
				get
				{
					return this.issuer;
				}
			}

			// Token: 0x17000012 RID: 18
			// (get) Token: 0x06000060 RID: 96 RVA: 0x00003D45 File Offset: 0x00001F45
			public byte[] SerialNumber
			{
				get
				{
					if (this.serial == null)
					{
						return null;
					}
					return (byte[])this.serial.Clone();
				}
			}

			// Token: 0x17000013 RID: 19
			// (get) Token: 0x06000061 RID: 97 RVA: 0x00003D61 File Offset: 0x00001F61
			public ArrayList AuthenticatedAttributes
			{
				get
				{
					return this.authenticatedAttributes;
				}
			}

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x06000062 RID: 98 RVA: 0x00003D69 File Offset: 0x00001F69
			// (set) Token: 0x06000063 RID: 99 RVA: 0x00003D71 File Offset: 0x00001F71
			public string HashName
			{
				get
				{
					return this.hashAlgorithm;
				}
				set
				{
					this.hashAlgorithm = value;
				}
			}

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x06000064 RID: 100 RVA: 0x00003D7A File Offset: 0x00001F7A
			public byte[] Signature
			{
				get
				{
					if (this.signature == null)
					{
						return null;
					}
					return (byte[])this.signature.Clone();
				}
			}

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x06000065 RID: 101 RVA: 0x00003D96 File Offset: 0x00001F96
			public ArrayList UnauthenticatedAttributes
			{
				get
				{
					return this.unauthenticatedAttributes;
				}
			}

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x06000066 RID: 102 RVA: 0x00003D9E File Offset: 0x00001F9E
			public byte Version
			{
				get
				{
					return this.version;
				}
			}

			// Token: 0x04000022 RID: 34
			private byte version;

			// Token: 0x04000023 RID: 35
			private string hashAlgorithm;

			// Token: 0x04000024 RID: 36
			private ArrayList authenticatedAttributes;

			// Token: 0x04000025 RID: 37
			private ArrayList unauthenticatedAttributes;

			// Token: 0x04000026 RID: 38
			private byte[] signature;

			// Token: 0x04000027 RID: 39
			private string issuer;

			// Token: 0x04000028 RID: 40
			private byte[] serial;

			// Token: 0x04000029 RID: 41
			private byte[] ski;
		}
	}
}
