using System;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using Mono.Security.Cryptography;

namespace Mono.Security.X509
{
	// Token: 0x02000019 RID: 25
	public class X509Certificate : ISerializable
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x00007314 File Offset: 0x00005514
		private void Parse(byte[] data)
		{
			try
			{
				this.decoder = new ASN1(data);
				if (this.decoder.Tag != 48)
				{
					throw new CryptographicException(X509Certificate.encoding_error);
				}
				if (this.decoder[0].Tag != 48)
				{
					throw new CryptographicException(X509Certificate.encoding_error);
				}
				ASN1 asn = this.decoder[0];
				int num = 0;
				ASN1 asn2 = this.decoder[0][num];
				this.version = 1;
				if (asn2.Tag == 160 && asn2.Count > 0)
				{
					this.version += (int)asn2[0].Value[0];
					num++;
				}
				ASN1 asn3 = this.decoder[0][num++];
				if (asn3.Tag != 2)
				{
					throw new CryptographicException(X509Certificate.encoding_error);
				}
				this.serialnumber = asn3.Value;
				Array.Reverse<byte>(this.serialnumber, 0, this.serialnumber.Length);
				num++;
				this.issuer = asn.Element(num++, 48);
				this.m_issuername = X501.ToString(this.issuer);
				ASN1 asn4 = asn.Element(num++, 48);
				ASN1 asn5 = asn4[0];
				this.m_from = ASN1Convert.ToDateTime(asn5);
				ASN1 asn6 = asn4[1];
				this.m_until = ASN1Convert.ToDateTime(asn6);
				this.subject = asn.Element(num++, 48);
				this.m_subject = X501.ToString(this.subject);
				ASN1 asn7 = asn.Element(num++, 48);
				ASN1 asn8 = asn7.Element(0, 48);
				ASN1 asn9 = asn8.Element(0, 6);
				this.m_keyalgo = ASN1Convert.ToOid(asn9);
				ASN1 asn10 = asn8[1];
				this.m_keyalgoparams = ((asn8.Count > 1) ? asn10.GetBytes() : null);
				ASN1 asn11 = asn7.Element(1, 3);
				int num2 = asn11.Length - 1;
				this.m_publickey = new byte[num2];
				Buffer.BlockCopy(asn11.Value, 1, this.m_publickey, 0, num2);
				byte[] value = this.decoder[2].Value;
				this.signature = new byte[value.Length - 1];
				Buffer.BlockCopy(value, 1, this.signature, 0, this.signature.Length);
				asn8 = this.decoder[1];
				asn9 = asn8.Element(0, 6);
				this.m_signaturealgo = ASN1Convert.ToOid(asn9);
				asn10 = asn8[1];
				if (asn10 != null)
				{
					this.m_signaturealgoparams = asn10.GetBytes();
				}
				else
				{
					this.m_signaturealgoparams = null;
				}
				ASN1 asn12 = asn.Element(num, 129);
				if (asn12 != null)
				{
					num++;
					this.issuerUniqueID = asn12.Value;
				}
				ASN1 asn13 = asn.Element(num, 130);
				if (asn13 != null)
				{
					num++;
					this.subjectUniqueID = asn13.Value;
				}
				ASN1 asn14 = asn.Element(num, 163);
				if (asn14 != null && asn14.Count == 1)
				{
					this.extensions = new X509ExtensionCollection(asn14[0]);
				}
				else
				{
					this.extensions = new X509ExtensionCollection(null);
				}
				this.m_encodedcert = (byte[])data.Clone();
			}
			catch (Exception ex)
			{
				throw new CryptographicException(X509Certificate.encoding_error, ex);
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00007660 File Offset: 0x00005860
		public X509Certificate(byte[] data)
		{
			if (data != null)
			{
				if (data.Length != 0 && data[0] != 48)
				{
					try
					{
						data = X509Certificate.PEM("CERTIFICATE", data);
					}
					catch (Exception ex)
					{
						throw new CryptographicException(X509Certificate.encoding_error, ex);
					}
				}
				this.Parse(data);
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000076B4 File Offset: 0x000058B4
		private byte[] GetUnsignedBigInteger(byte[] integer)
		{
			if (integer[0] == 0)
			{
				int num = integer.Length - 1;
				byte[] array = new byte[num];
				Buffer.BlockCopy(integer, 1, array, 0, num);
				return array;
			}
			return integer;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x000076E0 File Offset: 0x000058E0
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x0000781A File Offset: 0x00005A1A
		public DSA DSA
		{
			get
			{
				if (this.m_keyalgoparams == null)
				{
					throw new CryptographicException("Missing key algorithm parameters.");
				}
				if (this._dsa == null && this.m_keyalgo == "1.2.840.10040.4.1")
				{
					DSAParameters dsaparameters = default(DSAParameters);
					ASN1 asn = new ASN1(this.m_publickey);
					if (asn == null || asn.Tag != 2)
					{
						return null;
					}
					dsaparameters.Y = this.GetUnsignedBigInteger(asn.Value);
					ASN1 asn2 = new ASN1(this.m_keyalgoparams);
					if (asn2 == null || asn2.Tag != 48 || asn2.Count < 3)
					{
						return null;
					}
					if (asn2[0].Tag != 2 || asn2[1].Tag != 2 || asn2[2].Tag != 2)
					{
						return null;
					}
					dsaparameters.P = this.GetUnsignedBigInteger(asn2[0].Value);
					dsaparameters.Q = this.GetUnsignedBigInteger(asn2[1].Value);
					dsaparameters.G = this.GetUnsignedBigInteger(asn2[2].Value);
					this._dsa = new DSACryptoServiceProvider(dsaparameters.Y.Length << 3);
					this._dsa.ImportParameters(dsaparameters);
				}
				return this._dsa;
			}
			set
			{
				this._dsa = value;
				if (value != null)
				{
					this._rsa = null;
				}
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x0000782D File Offset: 0x00005A2D
		public X509ExtensionCollection Extensions
		{
			get
			{
				return this.extensions;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00007838 File Offset: 0x00005A38
		public byte[] Hash
		{
			get
			{
				if (this.certhash == null)
				{
					if (this.decoder == null || this.decoder.Count < 1)
					{
						return null;
					}
					string text = PKCS1.HashNameFromOid(this.m_signaturealgo, false);
					if (text == null)
					{
						return null;
					}
					byte[] bytes = this.decoder[0].GetBytes();
					using (HashAlgorithm hashAlgorithm = PKCS1.CreateFromName(text))
					{
						this.certhash = hashAlgorithm.ComputeHash(bytes, 0, bytes.Length);
					}
				}
				return (byte[])this.certhash.Clone();
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000BA RID: 186 RVA: 0x000078D0 File Offset: 0x00005AD0
		public virtual string IssuerName
		{
			get
			{
				return this.m_issuername;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000078D8 File Offset: 0x00005AD8
		public virtual string KeyAlgorithm
		{
			get
			{
				return this.m_keyalgo;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000BC RID: 188 RVA: 0x000078E0 File Offset: 0x00005AE0
		// (set) Token: 0x060000BD RID: 189 RVA: 0x000078FC File Offset: 0x00005AFC
		public virtual byte[] KeyAlgorithmParameters
		{
			get
			{
				if (this.m_keyalgoparams == null)
				{
					return null;
				}
				return (byte[])this.m_keyalgoparams.Clone();
			}
			set
			{
				this.m_keyalgoparams = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00007905 File Offset: 0x00005B05
		public virtual byte[] PublicKey
		{
			get
			{
				if (this.m_publickey == null)
				{
					return null;
				}
				return (byte[])this.m_publickey.Clone();
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00007924 File Offset: 0x00005B24
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x000079D9 File Offset: 0x00005BD9
		public virtual RSA RSA
		{
			get
			{
				if (this._rsa == null && this.m_keyalgo == "1.2.840.113549.1.1.1")
				{
					RSAParameters rsaparameters = default(RSAParameters);
					ASN1 asn = new ASN1(this.m_publickey);
					ASN1 asn2 = asn[0];
					if (asn2 == null || asn2.Tag != 2)
					{
						return null;
					}
					ASN1 asn3 = asn[1];
					if (asn3.Tag != 2)
					{
						return null;
					}
					rsaparameters.Modulus = this.GetUnsignedBigInteger(asn2.Value);
					rsaparameters.Exponent = asn3.Value;
					int num = rsaparameters.Modulus.Length << 3;
					this._rsa = new RSACryptoServiceProvider(num);
					this._rsa.ImportParameters(rsaparameters);
				}
				return this._rsa;
			}
			set
			{
				if (value != null)
				{
					this._dsa = null;
				}
				this._rsa = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x000079EC File Offset: 0x00005BEC
		public virtual byte[] RawData
		{
			get
			{
				if (this.m_encodedcert == null)
				{
					return null;
				}
				return (byte[])this.m_encodedcert.Clone();
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00007A08 File Offset: 0x00005C08
		public virtual byte[] SerialNumber
		{
			get
			{
				if (this.serialnumber == null)
				{
					return null;
				}
				return (byte[])this.serialnumber.Clone();
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00007A24 File Offset: 0x00005C24
		public virtual byte[] Signature
		{
			get
			{
				if (this.signature == null)
				{
					return null;
				}
				string signaturealgo = this.m_signaturealgo;
				uint num = global::<PrivateImplementationDetails>.ComputeStringHash(signaturealgo);
				if (num <= 719034781U)
				{
					if (num <= 601591448U)
					{
						if (num != 510574318U)
						{
							if (num != 601591448U)
							{
								goto IL_022D;
							}
							if (!(signaturealgo == "1.2.840.113549.1.1.5"))
							{
								goto IL_022D;
							}
						}
						else
						{
							if (!(signaturealgo == "1.2.840.10040.4.3"))
							{
								goto IL_022D;
							}
							ASN1 asn = new ASN1(this.signature);
							if (asn == null || asn.Count != 2)
							{
								return null;
							}
							byte[] value = asn[0].Value;
							byte[] value2 = asn[1].Value;
							byte[] array = new byte[40];
							int num2 = Math.Max(0, value.Length - 20);
							int num3 = Math.Max(0, 20 - value.Length);
							Buffer.BlockCopy(value, num2, array, num3, value.Length - num2);
							int num4 = Math.Max(0, value2.Length - 20);
							int num5 = Math.Max(20, 40 - value2.Length);
							Buffer.BlockCopy(value2, num4, array, num5, value2.Length - num4);
							return array;
						}
					}
					else if (num != 618369067U)
					{
						if (num != 702257162U)
						{
							if (num != 719034781U)
							{
								goto IL_022D;
							}
							if (!(signaturealgo == "1.2.840.113549.1.1.2"))
							{
								goto IL_022D;
							}
						}
						else if (!(signaturealgo == "1.2.840.113549.1.1.3"))
						{
							goto IL_022D;
						}
					}
					else if (!(signaturealgo == "1.2.840.113549.1.1.4"))
					{
						goto IL_022D;
					}
				}
				else if (num <= 2477476687U)
				{
					if (num != 875536856U)
					{
						if (num != 2477476687U)
						{
							goto IL_022D;
						}
						if (!(signaturealgo == "1.2.840.113549.1.1.11"))
						{
							goto IL_022D;
						}
					}
					else if (!(signaturealgo == "1.3.14.3.2.29"))
					{
						goto IL_022D;
					}
				}
				else if (num != 2494254306U)
				{
					if (num != 2511031925U)
					{
						if (num != 3493391575U)
						{
							goto IL_022D;
						}
						if (!(signaturealgo == "1.3.36.3.3.1.2"))
						{
							goto IL_022D;
						}
					}
					else if (!(signaturealgo == "1.2.840.113549.1.1.13"))
					{
						goto IL_022D;
					}
				}
				else if (!(signaturealgo == "1.2.840.113549.1.1.12"))
				{
					goto IL_022D;
				}
				return (byte[])this.signature.Clone();
				IL_022D:
				throw new CryptographicException("Unsupported hash algorithm: " + this.m_signaturealgo);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00007C73 File Offset: 0x00005E73
		public virtual string SubjectName
		{
			get
			{
				return this.m_subject;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00007C7B File Offset: 0x00005E7B
		public virtual DateTime ValidFrom
		{
			get
			{
				return this.m_from;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00007C83 File Offset: 0x00005E83
		public virtual DateTime ValidUntil
		{
			get
			{
				return this.m_until;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00007C8B File Offset: 0x00005E8B
		public int Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00007C93 File Offset: 0x00005E93
		public bool IsCurrent
		{
			get
			{
				return this.WasCurrent(DateTime.UtcNow);
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00007CA0 File Offset: 0x00005EA0
		public bool WasCurrent(DateTime instant)
		{
			return instant > this.ValidFrom && instant <= this.ValidUntil;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00007CBE File Offset: 0x00005EBE
		internal bool VerifySignature(DSA dsa)
		{
			DSASignatureDeformatter dsasignatureDeformatter = new DSASignatureDeformatter(dsa);
			dsasignatureDeformatter.SetHashAlgorithm("SHA1");
			return dsasignatureDeformatter.VerifySignature(this.Hash, this.Signature);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00007CE2 File Offset: 0x00005EE2
		internal bool VerifySignature(RSA rsa)
		{
			if (this.m_signaturealgo == "1.2.840.10040.4.3")
			{
				return false;
			}
			RSAPKCS1SignatureDeformatter rsapkcs1SignatureDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
			rsapkcs1SignatureDeformatter.SetHashAlgorithm(PKCS1.HashNameFromOid(this.m_signaturealgo, true));
			return rsapkcs1SignatureDeformatter.VerifySignature(this.Hash, this.Signature);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00007D24 File Offset: 0x00005F24
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

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00007D80 File Offset: 0x00005F80
		public bool IsSelfSigned
		{
			get
			{
				if (this.m_issuername != this.m_subject)
				{
					return false;
				}
				bool flag;
				try
				{
					if (this.RSA != null)
					{
						flag = this.VerifySignature(this.RSA);
					}
					else if (this.DSA != null)
					{
						flag = this.VerifySignature(this.DSA);
					}
					else
					{
						flag = false;
					}
				}
				catch (CryptographicException)
				{
					flag = false;
				}
				return flag;
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00007DEC File Offset: 0x00005FEC
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("raw", this.m_encodedcert);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00007E00 File Offset: 0x00006000
		private static byte[] PEM(string type, byte[] data)
		{
			string @string = Encoding.ASCII.GetString(data);
			string text = string.Format("-----BEGIN {0}-----", type);
			string text2 = string.Format("-----END {0}-----", type);
			int num = @string.IndexOf(text) + text.Length;
			int num2 = @string.IndexOf(text2, num);
			return Convert.FromBase64String(@string.Substring(num, num2 - num));
		}

		// Token: 0x0400005B RID: 91
		private ASN1 decoder;

		// Token: 0x0400005C RID: 92
		private byte[] m_encodedcert;

		// Token: 0x0400005D RID: 93
		private DateTime m_from;

		// Token: 0x0400005E RID: 94
		private DateTime m_until;

		// Token: 0x0400005F RID: 95
		private ASN1 issuer;

		// Token: 0x04000060 RID: 96
		private string m_issuername;

		// Token: 0x04000061 RID: 97
		private string m_keyalgo;

		// Token: 0x04000062 RID: 98
		private byte[] m_keyalgoparams;

		// Token: 0x04000063 RID: 99
		private ASN1 subject;

		// Token: 0x04000064 RID: 100
		private string m_subject;

		// Token: 0x04000065 RID: 101
		private byte[] m_publickey;

		// Token: 0x04000066 RID: 102
		private byte[] signature;

		// Token: 0x04000067 RID: 103
		private string m_signaturealgo;

		// Token: 0x04000068 RID: 104
		private byte[] m_signaturealgoparams;

		// Token: 0x04000069 RID: 105
		private byte[] certhash;

		// Token: 0x0400006A RID: 106
		private RSA _rsa;

		// Token: 0x0400006B RID: 107
		private DSA _dsa;

		// Token: 0x0400006C RID: 108
		private int version;

		// Token: 0x0400006D RID: 109
		private byte[] serialnumber;

		// Token: 0x0400006E RID: 110
		private byte[] issuerUniqueID;

		// Token: 0x0400006F RID: 111
		private byte[] subjectUniqueID;

		// Token: 0x04000070 RID: 112
		private X509ExtensionCollection extensions;

		// Token: 0x04000071 RID: 113
		private static string encoding_error = Locale.GetText("Input data cannot be coded as a valid certificate.");
	}
}
