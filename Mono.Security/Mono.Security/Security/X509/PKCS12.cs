using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using Mono.Security.Cryptography;

namespace Mono.Security.X509
{
	// Token: 0x02000014 RID: 20
	public class PKCS12 : ICloneable
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00003DCC File Offset: 0x00001FCC
		public PKCS12()
		{
			this._iterations = 2000;
			this._keyBags = new ArrayList();
			this._secretBags = new ArrayList();
			this._certs = new X509CertificateCollection();
			this._keyBagsChanged = false;
			this._secretBagsChanged = false;
			this._certsChanged = false;
			this._safeBags = new ArrayList();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003E2B File Offset: 0x0000202B
		public PKCS12(byte[] data)
			: this()
		{
			this.Password = null;
			this.Decode(data);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003E41 File Offset: 0x00002041
		public PKCS12(byte[] data, string password)
			: this()
		{
			this.Password = password;
			this.Decode(data);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003E58 File Offset: 0x00002058
		private void Decode(byte[] data)
		{
			ASN1 asn = new ASN1(data);
			if (asn.Tag != 48)
			{
				throw new ArgumentException("invalid data");
			}
			if (asn[0].Tag != 2)
			{
				throw new ArgumentException("invalid PFX version");
			}
			PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo(asn[1]);
			if (contentInfo.ContentType != "1.2.840.113549.1.7.1")
			{
				throw new ArgumentException("invalid authenticated safe");
			}
			if (asn.Count > 2)
			{
				ASN1 asn2 = asn[2];
				if (asn2.Tag != 48)
				{
					throw new ArgumentException("invalid MAC");
				}
				ASN1 asn3 = asn2[0];
				if (asn3.Tag != 48)
				{
					throw new ArgumentException("invalid MAC");
				}
				if (ASN1Convert.ToOid(asn3[0][0]) != "1.3.14.3.2.26")
				{
					throw new ArgumentException("unsupported HMAC");
				}
				byte[] value = asn3[1].Value;
				ASN1 asn4 = asn2[1];
				if (asn4.Tag != 4)
				{
					throw new ArgumentException("missing MAC salt");
				}
				this._iterations = 1;
				if (asn2.Count > 2)
				{
					ASN1 asn5 = asn2[2];
					if (asn5.Tag != 2)
					{
						throw new ArgumentException("invalid MAC iteration");
					}
					this._iterations = ASN1Convert.ToInt32(asn5);
				}
				byte[] value2 = contentInfo.Content[0].Value;
				byte[] array = this.MAC(this._password, asn4.Value, this._iterations, value2);
				if (!this.Compare(value, array))
				{
					byte[] array2 = new byte[2];
					array = this.MAC(array2, asn4.Value, this._iterations, value2);
					if (!this.Compare(value, array))
					{
						throw new CryptographicException("Invalid MAC - file may have been tampered with!");
					}
					this._password = array2;
				}
			}
			ASN1 asn6 = new ASN1(contentInfo.Content[0].Value);
			for (int i = 0; i < asn6.Count; i++)
			{
				PKCS7.ContentInfo contentInfo2 = new PKCS7.ContentInfo(asn6[i]);
				string contentType = contentInfo2.ContentType;
				if (!(contentType == "1.2.840.113549.1.7.1"))
				{
					if (!(contentType == "1.2.840.113549.1.7.6"))
					{
						if (!(contentType == "1.2.840.113549.1.7.3"))
						{
							throw new ArgumentException("unknown authenticatedSafe");
						}
						throw new NotImplementedException("public key encrypted");
					}
					else
					{
						PKCS7.EncryptedData encryptedData = new PKCS7.EncryptedData(contentInfo2.Content[0]);
						ASN1 asn7 = new ASN1(this.Decrypt(encryptedData));
						for (int j = 0; j < asn7.Count; j++)
						{
							ASN1 asn8 = asn7[j];
							this.ReadSafeBag(asn8);
						}
					}
				}
				else
				{
					ASN1 asn9 = new ASN1(contentInfo2.Content[0].Value);
					for (int k = 0; k < asn9.Count; k++)
					{
						ASN1 asn10 = asn9[k];
						this.ReadSafeBag(asn10);
					}
				}
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000413C File Offset: 0x0000233C
		~PKCS12()
		{
			if (this._password != null)
			{
				Array.Clear(this._password, 0, this._password.Length);
			}
			this._password = null;
		}

		// Token: 0x1700001A RID: 26
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00004188 File Offset: 0x00002388
		public string Password
		{
			set
			{
				if (this._password != null)
				{
					Array.Clear(this._password, 0, this._password.Length);
				}
				this._password = null;
				if (value != null)
				{
					if (value.Length > 0)
					{
						int num = value.Length;
						int num2 = 0;
						if (num < PKCS12.MaximumPasswordLength)
						{
							if (value[num - 1] != '\0')
							{
								num2 = 1;
							}
						}
						else
						{
							num = PKCS12.MaximumPasswordLength;
						}
						this._password = new byte[num + num2 << 1];
						Encoding.BigEndianUnicode.GetBytes(value, 0, num, this._password, 0);
						return;
					}
					this._password = new byte[2];
				}
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000070 RID: 112 RVA: 0x0000421C File Offset: 0x0000241C
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00004224 File Offset: 0x00002424
		public int IterationCount
		{
			get
			{
				return this._iterations;
			}
			set
			{
				this._iterations = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00004230 File Offset: 0x00002430
		public ArrayList Keys
		{
			get
			{
				if (this._keyBagsChanged)
				{
					this._keyBags.Clear();
					foreach (object obj in this._safeBags)
					{
						SafeBag safeBag = (SafeBag)obj;
						if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.1"))
						{
							byte[] privateKey = new PKCS8.PrivateKeyInfo(safeBag.ASN1[1].Value).PrivateKey;
							byte b = privateKey[0];
							if (b != 2)
							{
								if (b == 48)
								{
									this._keyBags.Add(PKCS8.PrivateKeyInfo.DecodeRSA(privateKey));
								}
							}
							else
							{
								DSAParameters dsaparameters = default(DSAParameters);
								this._keyBags.Add(PKCS8.PrivateKeyInfo.DecodeDSA(privateKey, dsaparameters));
							}
							Array.Clear(privateKey, 0, privateKey.Length);
						}
						else if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.2"))
						{
							PKCS8.EncryptedPrivateKeyInfo encryptedPrivateKeyInfo = new PKCS8.EncryptedPrivateKeyInfo(safeBag.ASN1[1].Value);
							byte[] array = this.Decrypt(encryptedPrivateKeyInfo.Algorithm, encryptedPrivateKeyInfo.Salt, encryptedPrivateKeyInfo.IterationCount, encryptedPrivateKeyInfo.EncryptedData);
							byte[] privateKey2 = new PKCS8.PrivateKeyInfo(array).PrivateKey;
							byte b = privateKey2[0];
							if (b != 2)
							{
								if (b == 48)
								{
									this._keyBags.Add(PKCS8.PrivateKeyInfo.DecodeRSA(privateKey2));
								}
							}
							else
							{
								DSAParameters dsaparameters2 = default(DSAParameters);
								this._keyBags.Add(PKCS8.PrivateKeyInfo.DecodeDSA(privateKey2, dsaparameters2));
							}
							Array.Clear(privateKey2, 0, privateKey2.Length);
							Array.Clear(array, 0, array.Length);
						}
					}
					this._keyBagsChanged = false;
				}
				return ArrayList.ReadOnly(this._keyBags);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000043FC File Offset: 0x000025FC
		public X509CertificateCollection Certificates
		{
			get
			{
				if (this._certsChanged)
				{
					this._certs.Clear();
					foreach (object obj in this._safeBags)
					{
						SafeBag safeBag = (SafeBag)obj;
						if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.3"))
						{
							PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo(safeBag.ASN1[1].Value);
							this._certs.Add(new X509Certificate(contentInfo.Content[0].Value));
						}
					}
					this._certsChanged = false;
				}
				return this._certs;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000044BC File Offset: 0x000026BC
		internal RandomNumberGenerator RNG
		{
			get
			{
				if (this._rng == null)
				{
					this._rng = RandomNumberGenerator.Create();
				}
				return this._rng;
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000044D8 File Offset: 0x000026D8
		private bool Compare(byte[] expected, byte[] actual)
		{
			bool flag = false;
			if (expected.Length == actual.Length)
			{
				for (int i = 0; i < expected.Length; i++)
				{
					if (expected[i] != actual[i])
					{
						return false;
					}
				}
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000450C File Offset: 0x0000270C
		private SymmetricAlgorithm GetSymmetricAlgorithm(string algorithmOid, byte[] salt, int iterationCount)
		{
			string text = null;
			int num = 8;
			int num2 = 8;
			PKCS12.DeriveBytes deriveBytes = new PKCS12.DeriveBytes();
			deriveBytes.Password = this._password;
			deriveBytes.Salt = salt;
			deriveBytes.IterationCount = iterationCount;
			uint num3 = global::<PrivateImplementationDetails>.ComputeStringHash(algorithmOid);
			if (num3 <= 2949822700U)
			{
				if (num3 <= 2882712224U)
				{
					if (num3 != 1314512600U)
					{
						if (num3 != 1331290219U)
						{
							if (num3 == 2882712224U)
							{
								if (algorithmOid == "1.2.840.113549.1.12.1.6")
								{
									deriveBytes.HashName = "SHA1";
									text = "RC2";
									num = 5;
									goto IL_02FE;
								}
							}
						}
						else if (algorithmOid == "1.2.840.113549.1.5.11")
						{
							deriveBytes.HashName = "SHA1";
							text = "RC2";
							num = 4;
							goto IL_02FE;
						}
					}
					else if (algorithmOid == "1.2.840.113549.1.5.10")
					{
						deriveBytes.HashName = "SHA1";
						text = "DES";
						goto IL_02FE;
					}
				}
				else if (num3 != 2916267462U)
				{
					if (num3 != 2933045081U)
					{
						if (num3 == 2949822700U)
						{
							if (algorithmOid == "1.2.840.113549.1.12.1.2")
							{
								deriveBytes.HashName = "SHA1";
								text = "RC4";
								num = 5;
								num2 = 0;
								goto IL_02FE;
							}
						}
					}
					else if (algorithmOid == "1.2.840.113549.1.12.1.5")
					{
						deriveBytes.HashName = "SHA1";
						text = "RC2";
						num = 16;
						goto IL_02FE;
					}
				}
				else if (algorithmOid == "1.2.840.113549.1.12.1.4")
				{
					deriveBytes.HashName = "SHA1";
					text = "TripleDES";
					num = 16;
					goto IL_02FE;
				}
			}
			else if (num3 <= 3543878904U)
			{
				if (num3 != 2966600319U)
				{
					if (num3 != 3000155557U)
					{
						if (num3 == 3543878904U)
						{
							if (algorithmOid == "1.2.840.113549.1.5.1")
							{
								deriveBytes.HashName = "MD2";
								text = "DES";
								goto IL_02FE;
							}
						}
					}
					else if (algorithmOid == "1.2.840.113549.1.12.1.1")
					{
						deriveBytes.HashName = "SHA1";
						text = "RC4";
						num = 16;
						num2 = 0;
						goto IL_02FE;
					}
				}
				else if (algorithmOid == "1.2.840.113549.1.12.1.3")
				{
					deriveBytes.HashName = "SHA1";
					text = "TripleDES";
					num = 24;
					goto IL_02FE;
				}
			}
			else if (num3 != 3577434142U)
			{
				if (num3 != 3627766999U)
				{
					if (num3 == 3661322237U)
					{
						if (algorithmOid == "1.2.840.113549.1.5.6")
						{
							deriveBytes.HashName = "MD5";
							text = "RC2";
							num = 4;
							goto IL_02FE;
						}
					}
				}
				else if (algorithmOid == "1.2.840.113549.1.5.4")
				{
					deriveBytes.HashName = "MD2";
					text = "RC2";
					num = 4;
					goto IL_02FE;
				}
			}
			else if (algorithmOid == "1.2.840.113549.1.5.3")
			{
				deriveBytes.HashName = "MD5";
				text = "DES";
				goto IL_02FE;
			}
			throw new NotSupportedException("unknown oid " + text);
			IL_02FE:
			SymmetricAlgorithm symmetricAlgorithm = SymmetricAlgorithm.Create(text);
			symmetricAlgorithm.Key = deriveBytes.DeriveKey(num);
			if (num2 > 0)
			{
				symmetricAlgorithm.IV = deriveBytes.DeriveIV(num2);
				symmetricAlgorithm.Mode = CipherMode.CBC;
			}
			return symmetricAlgorithm;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000484C File Offset: 0x00002A4C
		public byte[] Decrypt(string algorithmOid, byte[] salt, int iterationCount, byte[] encryptedData)
		{
			SymmetricAlgorithm symmetricAlgorithm = null;
			byte[] array = null;
			try
			{
				symmetricAlgorithm = this.GetSymmetricAlgorithm(algorithmOid, salt, iterationCount);
				array = symmetricAlgorithm.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);
			}
			finally
			{
				if (symmetricAlgorithm != null)
				{
					symmetricAlgorithm.Clear();
				}
			}
			return array;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004898 File Offset: 0x00002A98
		public byte[] Decrypt(PKCS7.EncryptedData ed)
		{
			return this.Decrypt(ed.EncryptionAlgorithm.ContentType, ed.EncryptionAlgorithm.Content[0].Value, ASN1Convert.ToInt32(ed.EncryptionAlgorithm.Content[1]), ed.EncryptedContent);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000048E8 File Offset: 0x00002AE8
		public byte[] Encrypt(string algorithmOid, byte[] salt, int iterationCount, byte[] data)
		{
			byte[] array = null;
			using (SymmetricAlgorithm symmetricAlgorithm = this.GetSymmetricAlgorithm(algorithmOid, salt, iterationCount))
			{
				array = symmetricAlgorithm.CreateEncryptor().TransformFinalBlock(data, 0, data.Length);
			}
			return array;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004934 File Offset: 0x00002B34
		private DSAParameters GetExistingParameters(out bool found)
		{
			foreach (X509Certificate x509Certificate in this.Certificates)
			{
				if (x509Certificate.KeyAlgorithmParameters != null)
				{
					DSA dsa = x509Certificate.DSA;
					if (dsa != null)
					{
						found = true;
						return dsa.ExportParameters(false);
					}
				}
			}
			found = false;
			return default(DSAParameters);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000049B4 File Offset: 0x00002BB4
		private void AddPrivateKey(PKCS8.PrivateKeyInfo pki)
		{
			byte[] privateKey = pki.PrivateKey;
			try
			{
				string algorithm = pki.Algorithm;
				if (!(algorithm == "1.2.840.113549.1.1.1"))
				{
					if (!(algorithm == "1.2.840.10040.4.1"))
					{
						if (!(algorithm == "1.2.840.10045.2.1"))
						{
						}
						throw new CryptographicException("Unknown private key format");
					}
					bool flag;
					DSAParameters existingParameters = this.GetExistingParameters(out flag);
					if (flag)
					{
						this._keyBags.Add(PKCS8.PrivateKeyInfo.DecodeDSA(privateKey, existingParameters));
					}
				}
				else
				{
					this._keyBags.Add(PKCS8.PrivateKeyInfo.DecodeRSA(privateKey));
				}
			}
			finally
			{
				Array.Clear(privateKey, 0, privateKey.Length);
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004A58 File Offset: 0x00002C58
		private void ReadSafeBag(ASN1 safeBag)
		{
			if (safeBag.Tag != 48)
			{
				throw new ArgumentException("invalid safeBag");
			}
			ASN1 asn = safeBag[0];
			if (asn.Tag != 6)
			{
				throw new ArgumentException("invalid safeBag id");
			}
			ASN1 asn2 = safeBag[1];
			string text = ASN1Convert.ToOid(asn);
			if (!(text == "1.2.840.113549.1.12.10.1.1"))
			{
				if (!(text == "1.2.840.113549.1.12.10.1.2"))
				{
					if (!(text == "1.2.840.113549.1.12.10.1.3"))
					{
						if (!(text == "1.2.840.113549.1.12.10.1.4"))
						{
							if (!(text == "1.2.840.113549.1.12.10.1.5"))
							{
								if (!(text == "1.2.840.113549.1.12.10.1.6"))
								{
									throw new ArgumentException("unknown safeBag oid");
								}
							}
							else
							{
								byte[] value = asn2.Value;
								this._secretBags.Add(value);
							}
						}
					}
					else
					{
						PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo(asn2.Value);
						if (contentInfo.ContentType != "1.2.840.113549.1.9.22.1")
						{
							throw new NotSupportedException("unsupport certificate type");
						}
						X509Certificate x509Certificate = new X509Certificate(contentInfo.Content[0].Value);
						this._certs.Add(x509Certificate);
					}
				}
				else
				{
					PKCS8.EncryptedPrivateKeyInfo encryptedPrivateKeyInfo = new PKCS8.EncryptedPrivateKeyInfo(asn2.Value);
					byte[] array = this.Decrypt(encryptedPrivateKeyInfo.Algorithm, encryptedPrivateKeyInfo.Salt, encryptedPrivateKeyInfo.IterationCount, encryptedPrivateKeyInfo.EncryptedData);
					this.AddPrivateKey(new PKCS8.PrivateKeyInfo(array));
					Array.Clear(array, 0, array.Length);
				}
			}
			else
			{
				this.AddPrivateKey(new PKCS8.PrivateKeyInfo(asn2.Value));
			}
			if (safeBag.Count > 2)
			{
				ASN1 asn3 = safeBag[2];
				if (asn3.Tag != 49)
				{
					throw new ArgumentException("invalid safeBag attributes id");
				}
				for (int i = 0; i < asn3.Count; i++)
				{
					ASN1 asn4 = asn3[i];
					if (asn4.Tag != 48)
					{
						throw new ArgumentException("invalid PKCS12 attributes id");
					}
					ASN1 asn5 = asn4[0];
					if (asn5.Tag != 6)
					{
						throw new ArgumentException("invalid attribute id");
					}
					string text2 = ASN1Convert.ToOid(asn5);
					ASN1 asn6 = asn4[1];
					for (int j = 0; j < asn6.Count; j++)
					{
						ASN1 asn7 = asn6[j];
						if (!(text2 == "1.2.840.113549.1.9.20"))
						{
							if (text2 == "1.2.840.113549.1.9.21")
							{
								if (asn7.Tag != 4)
								{
									throw new ArgumentException("invalid attribute value id");
								}
							}
						}
						else if (asn7.Tag != 30)
						{
							throw new ArgumentException("invalid attribute value id");
						}
					}
				}
			}
			this._safeBags.Add(new SafeBag(text, safeBag));
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004CDC File Offset: 0x00002EDC
		private ASN1 Pkcs8ShroudedKeyBagSafeBag(AsymmetricAlgorithm aa, IDictionary attributes)
		{
			PKCS8.PrivateKeyInfo privateKeyInfo = new PKCS8.PrivateKeyInfo();
			if (aa is RSA)
			{
				privateKeyInfo.Algorithm = "1.2.840.113549.1.1.1";
				privateKeyInfo.PrivateKey = PKCS8.PrivateKeyInfo.Encode((RSA)aa);
			}
			else
			{
				if (!(aa is DSA))
				{
					throw new CryptographicException("Unknown asymmetric algorithm {0}", aa.ToString());
				}
				privateKeyInfo.Algorithm = null;
				privateKeyInfo.PrivateKey = PKCS8.PrivateKeyInfo.Encode((DSA)aa);
			}
			PKCS8.EncryptedPrivateKeyInfo encryptedPrivateKeyInfo = new PKCS8.EncryptedPrivateKeyInfo();
			encryptedPrivateKeyInfo.Algorithm = "1.2.840.113549.1.12.1.3";
			encryptedPrivateKeyInfo.IterationCount = this._iterations;
			encryptedPrivateKeyInfo.EncryptedData = this.Encrypt("1.2.840.113549.1.12.1.3", encryptedPrivateKeyInfo.Salt, this._iterations, privateKeyInfo.GetBytes());
			ASN1 asn = new ASN1(48);
			asn.Add(ASN1Convert.FromOid("1.2.840.113549.1.12.10.1.2"));
			ASN1 asn2 = new ASN1(160);
			asn2.Add(new ASN1(encryptedPrivateKeyInfo.GetBytes()));
			asn.Add(asn2);
			if (attributes != null)
			{
				ASN1 asn3 = new ASN1(49);
				IDictionaryEnumerator enumerator = attributes.GetEnumerator();
				while (enumerator.MoveNext())
				{
					string text = (string)enumerator.Key;
					if (!(text == "1.2.840.113549.1.9.20"))
					{
						if (text == "1.2.840.113549.1.9.21")
						{
							ArrayList arrayList = (ArrayList)enumerator.Value;
							if (arrayList.Count > 0)
							{
								ASN1 asn4 = new ASN1(48);
								asn4.Add(ASN1Convert.FromOid("1.2.840.113549.1.9.21"));
								ASN1 asn5 = new ASN1(49);
								foreach (object obj in arrayList)
								{
									byte[] array = (byte[])obj;
									asn5.Add(new ASN1(4)
									{
										Value = array
									});
								}
								asn4.Add(asn5);
								asn3.Add(asn4);
							}
						}
					}
					else
					{
						ArrayList arrayList2 = (ArrayList)enumerator.Value;
						if (arrayList2.Count > 0)
						{
							ASN1 asn6 = new ASN1(48);
							asn6.Add(ASN1Convert.FromOid("1.2.840.113549.1.9.20"));
							ASN1 asn7 = new ASN1(49);
							foreach (object obj2 in arrayList2)
							{
								byte[] array2 = (byte[])obj2;
								asn7.Add(new ASN1(30)
								{
									Value = array2
								});
							}
							asn6.Add(asn7);
							asn3.Add(asn6);
						}
					}
				}
				if (asn3.Count > 0)
				{
					asn.Add(asn3);
				}
			}
			return asn;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004FA4 File Offset: 0x000031A4
		private ASN1 CertificateSafeBag(X509Certificate x509, IDictionary attributes)
		{
			ASN1 asn = new ASN1(4, x509.RawData);
			PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo();
			contentInfo.ContentType = "1.2.840.113549.1.9.22.1";
			contentInfo.Content.Add(asn);
			ASN1 asn2 = new ASN1(160);
			asn2.Add(contentInfo.ASN1);
			ASN1 asn3 = new ASN1(48);
			asn3.Add(ASN1Convert.FromOid("1.2.840.113549.1.12.10.1.3"));
			asn3.Add(asn2);
			if (attributes != null)
			{
				ASN1 asn4 = new ASN1(49);
				IDictionaryEnumerator enumerator = attributes.GetEnumerator();
				while (enumerator.MoveNext())
				{
					string text = (string)enumerator.Key;
					if (!(text == "1.2.840.113549.1.9.20"))
					{
						if (text == "1.2.840.113549.1.9.21")
						{
							ArrayList arrayList = (ArrayList)enumerator.Value;
							if (arrayList.Count > 0)
							{
								ASN1 asn5 = new ASN1(48);
								asn5.Add(ASN1Convert.FromOid("1.2.840.113549.1.9.21"));
								ASN1 asn6 = new ASN1(49);
								foreach (object obj in arrayList)
								{
									byte[] array = (byte[])obj;
									asn6.Add(new ASN1(4)
									{
										Value = array
									});
								}
								asn5.Add(asn6);
								asn4.Add(asn5);
							}
						}
					}
					else
					{
						ArrayList arrayList2 = (ArrayList)enumerator.Value;
						if (arrayList2.Count > 0)
						{
							ASN1 asn7 = new ASN1(48);
							asn7.Add(ASN1Convert.FromOid("1.2.840.113549.1.9.20"));
							ASN1 asn8 = new ASN1(49);
							foreach (object obj2 in arrayList2)
							{
								byte[] array2 = (byte[])obj2;
								asn8.Add(new ASN1(30)
								{
									Value = array2
								});
							}
							asn7.Add(asn8);
							asn4.Add(asn7);
						}
					}
				}
				if (asn4.Count > 0)
				{
					asn3.Add(asn4);
				}
			}
			return asn3;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000051F4 File Offset: 0x000033F4
		private byte[] MAC(byte[] password, byte[] salt, int iterations, byte[] data)
		{
			PKCS12.DeriveBytes deriveBytes = new PKCS12.DeriveBytes();
			deriveBytes.HashName = "SHA1";
			deriveBytes.Password = password;
			deriveBytes.Salt = salt;
			deriveBytes.IterationCount = iterations;
			HMACSHA1 hmacsha = (HMACSHA1)HMAC.Create();
			hmacsha.Key = deriveBytes.DeriveMAC(20);
			return hmacsha.ComputeHash(data, 0, data.Length);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000524C File Offset: 0x0000344C
		public byte[] GetBytes()
		{
			ASN1 asn = new ASN1(48);
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this._safeBags)
			{
				SafeBag safeBag = (SafeBag)obj;
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.3"))
				{
					PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo(safeBag.ASN1[1].Value);
					arrayList.Add(new X509Certificate(contentInfo.Content[0].Value));
				}
			}
			ArrayList arrayList2 = new ArrayList();
			ArrayList arrayList3 = new ArrayList();
			foreach (X509Certificate x509Certificate in this.Certificates)
			{
				bool flag = false;
				foreach (object obj2 in arrayList)
				{
					X509Certificate x509Certificate2 = (X509Certificate)obj2;
					if (this.Compare(x509Certificate.RawData, x509Certificate2.RawData))
					{
						flag = true;
					}
				}
				if (!flag)
				{
					arrayList2.Add(x509Certificate);
				}
			}
			foreach (object obj3 in arrayList)
			{
				X509Certificate x509Certificate3 = (X509Certificate)obj3;
				bool flag2 = false;
				foreach (X509Certificate x509Certificate4 in this.Certificates)
				{
					if (this.Compare(x509Certificate3.RawData, x509Certificate4.RawData))
					{
						flag2 = true;
					}
				}
				if (!flag2)
				{
					arrayList3.Add(x509Certificate3);
				}
			}
			foreach (object obj4 in arrayList3)
			{
				X509Certificate x509Certificate5 = (X509Certificate)obj4;
				this.RemoveCertificate(x509Certificate5);
			}
			foreach (object obj5 in arrayList2)
			{
				X509Certificate x509Certificate6 = (X509Certificate)obj5;
				this.AddCertificate(x509Certificate6);
			}
			if (this._safeBags.Count > 0)
			{
				ASN1 asn2 = new ASN1(48);
				foreach (object obj6 in this._safeBags)
				{
					SafeBag safeBag2 = (SafeBag)obj6;
					if (safeBag2.BagOID.Equals("1.2.840.113549.1.12.10.1.3"))
					{
						asn2.Add(safeBag2.ASN1);
					}
				}
				if (asn2.Count > 0)
				{
					PKCS7.ContentInfo contentInfo2 = this.EncryptedContentInfo(asn2, "1.2.840.113549.1.12.1.3");
					asn.Add(contentInfo2.ASN1);
				}
			}
			if (this._safeBags.Count > 0)
			{
				ASN1 asn3 = new ASN1(48);
				foreach (object obj7 in this._safeBags)
				{
					SafeBag safeBag3 = (SafeBag)obj7;
					if (safeBag3.BagOID.Equals("1.2.840.113549.1.12.10.1.1") || safeBag3.BagOID.Equals("1.2.840.113549.1.12.10.1.2"))
					{
						asn3.Add(safeBag3.ASN1);
					}
				}
				if (asn3.Count > 0)
				{
					ASN1 asn4 = new ASN1(160);
					asn4.Add(new ASN1(4, asn3.GetBytes()));
					asn.Add(new PKCS7.ContentInfo("1.2.840.113549.1.7.1")
					{
						Content = asn4
					}.ASN1);
				}
			}
			if (this._safeBags.Count > 0)
			{
				ASN1 asn5 = new ASN1(48);
				foreach (object obj8 in this._safeBags)
				{
					SafeBag safeBag4 = (SafeBag)obj8;
					if (safeBag4.BagOID.Equals("1.2.840.113549.1.12.10.1.5"))
					{
						asn5.Add(safeBag4.ASN1);
					}
				}
				if (asn5.Count > 0)
				{
					PKCS7.ContentInfo contentInfo3 = this.EncryptedContentInfo(asn5, "1.2.840.113549.1.12.1.3");
					asn.Add(contentInfo3.ASN1);
				}
			}
			ASN1 asn6 = new ASN1(4, asn.GetBytes());
			ASN1 asn7 = new ASN1(160);
			asn7.Add(asn6);
			PKCS7.ContentInfo contentInfo4 = new PKCS7.ContentInfo("1.2.840.113549.1.7.1");
			contentInfo4.Content = asn7;
			ASN1 asn8 = new ASN1(48);
			if (this._password != null)
			{
				byte[] array = new byte[20];
				this.RNG.GetBytes(array);
				byte[] array2 = this.MAC(this._password, array, this._iterations, contentInfo4.Content[0].Value);
				ASN1 asn9 = new ASN1(48);
				asn9.Add(ASN1Convert.FromOid("1.3.14.3.2.26"));
				asn9.Add(new ASN1(5));
				ASN1 asn10 = new ASN1(48);
				asn10.Add(asn9);
				asn10.Add(new ASN1(4, array2));
				asn8.Add(asn10);
				asn8.Add(new ASN1(4, array));
				asn8.Add(ASN1Convert.FromInt32(this._iterations));
			}
			ASN1 asn11 = new ASN1(2, new byte[] { 3 });
			ASN1 asn12 = new ASN1(48);
			asn12.Add(asn11);
			asn12.Add(contentInfo4.ASN1);
			if (asn8.Count > 0)
			{
				asn12.Add(asn8);
			}
			return asn12.GetBytes();
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00005898 File Offset: 0x00003A98
		private PKCS7.ContentInfo EncryptedContentInfo(ASN1 safeBags, string algorithmOid)
		{
			byte[] array = new byte[8];
			this.RNG.GetBytes(array);
			ASN1 asn = new ASN1(48);
			asn.Add(new ASN1(4, array));
			asn.Add(ASN1Convert.FromInt32(this._iterations));
			ASN1 asn2 = new ASN1(48);
			asn2.Add(ASN1Convert.FromOid(algorithmOid));
			asn2.Add(asn);
			byte[] array2 = this.Encrypt(algorithmOid, array, this._iterations, safeBags.GetBytes());
			ASN1 asn3 = new ASN1(128, array2);
			ASN1 asn4 = new ASN1(48);
			asn4.Add(ASN1Convert.FromOid("1.2.840.113549.1.7.1"));
			asn4.Add(asn2);
			asn4.Add(asn3);
			ASN1 asn5 = new ASN1(2, new byte[1]);
			ASN1 asn6 = new ASN1(48);
			asn6.Add(asn5);
			asn6.Add(asn4);
			ASN1 asn7 = new ASN1(160);
			asn7.Add(asn6);
			return new PKCS7.ContentInfo("1.2.840.113549.1.7.6")
			{
				Content = asn7
			};
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000059A0 File Offset: 0x00003BA0
		public void AddCertificate(X509Certificate cert)
		{
			this.AddCertificate(cert, null);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000059AC File Offset: 0x00003BAC
		public void AddCertificate(X509Certificate cert, IDictionary attributes)
		{
			bool flag = false;
			int num = 0;
			while (!flag && num < this._safeBags.Count)
			{
				SafeBag safeBag = (SafeBag)this._safeBags[num];
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.3"))
				{
					X509Certificate x509Certificate = new X509Certificate(new PKCS7.ContentInfo(safeBag.ASN1[1].Value).Content[0].Value);
					if (this.Compare(cert.RawData, x509Certificate.RawData))
					{
						flag = true;
					}
				}
				num++;
			}
			if (!flag)
			{
				this._safeBags.Add(new SafeBag("1.2.840.113549.1.12.10.1.3", this.CertificateSafeBag(cert, attributes)));
				this._certsChanged = true;
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00005A62 File Offset: 0x00003C62
		public void RemoveCertificate(X509Certificate cert)
		{
			this.RemoveCertificate(cert, null);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00005A6C File Offset: 0x00003C6C
		public void RemoveCertificate(X509Certificate cert, IDictionary attrs)
		{
			int num = -1;
			int num2 = 0;
			while (num == -1 && num2 < this._safeBags.Count)
			{
				SafeBag safeBag = (SafeBag)this._safeBags[num2];
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.3"))
				{
					ASN1 asn = safeBag.ASN1;
					X509Certificate x509Certificate = new X509Certificate(new PKCS7.ContentInfo(asn[1].Value).Content[0].Value);
					if (this.Compare(cert.RawData, x509Certificate.RawData))
					{
						if (attrs != null)
						{
							if (asn.Count == 3)
							{
								ASN1 asn2 = asn[2];
								int num3 = 0;
								for (int i = 0; i < asn2.Count; i++)
								{
									ASN1 asn3 = asn2[i];
									string text = ASN1Convert.ToOid(asn3[0]);
									ArrayList arrayList = (ArrayList)attrs[text];
									if (arrayList != null)
									{
										ASN1 asn4 = asn3[1];
										if (arrayList.Count == asn4.Count)
										{
											int num4 = 0;
											for (int j = 0; j < asn4.Count; j++)
											{
												ASN1 asn5 = asn4[j];
												byte[] array = (byte[])arrayList[j];
												if (this.Compare(array, asn5.Value))
												{
													num4++;
												}
											}
											if (num4 == asn4.Count)
											{
												num3++;
											}
										}
									}
								}
								if (num3 == asn2.Count)
								{
									num = num2;
								}
							}
						}
						else
						{
							num = num2;
						}
					}
				}
				num2++;
			}
			if (num != -1)
			{
				this._safeBags.RemoveAt(num);
				this._certsChanged = true;
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00005C0E File Offset: 0x00003E0E
		private bool CompareAsymmetricAlgorithm(AsymmetricAlgorithm a1, AsymmetricAlgorithm a2)
		{
			return a1.KeySize == a2.KeySize && a1.ToXmlString(false) == a2.ToXmlString(false);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00005C34 File Offset: 0x00003E34
		public void AddPkcs8ShroudedKeyBag(AsymmetricAlgorithm aa, IDictionary attributes)
		{
			bool flag = false;
			int num = 0;
			while (!flag && num < this._safeBags.Count)
			{
				SafeBag safeBag = (SafeBag)this._safeBags[num];
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.2"))
				{
					PKCS8.EncryptedPrivateKeyInfo encryptedPrivateKeyInfo = new PKCS8.EncryptedPrivateKeyInfo(safeBag.ASN1[1].Value);
					byte[] array = this.Decrypt(encryptedPrivateKeyInfo.Algorithm, encryptedPrivateKeyInfo.Salt, encryptedPrivateKeyInfo.IterationCount, encryptedPrivateKeyInfo.EncryptedData);
					byte[] privateKey = new PKCS8.PrivateKeyInfo(array).PrivateKey;
					byte b = privateKey[0];
					AsymmetricAlgorithm asymmetricAlgorithm;
					if (b != 2)
					{
						if (b != 48)
						{
							Array.Clear(array, 0, array.Length);
							Array.Clear(privateKey, 0, privateKey.Length);
							throw new CryptographicException("Unknown private key format");
						}
						asymmetricAlgorithm = PKCS8.PrivateKeyInfo.DecodeRSA(privateKey);
					}
					else
					{
						asymmetricAlgorithm = PKCS8.PrivateKeyInfo.DecodeDSA(privateKey, default(DSAParameters));
					}
					Array.Clear(array, 0, array.Length);
					Array.Clear(privateKey, 0, privateKey.Length);
					if (this.CompareAsymmetricAlgorithm(aa, asymmetricAlgorithm))
					{
						flag = true;
					}
				}
				num++;
			}
			if (!flag)
			{
				this._safeBags.Add(new SafeBag("1.2.840.113549.1.12.10.1.2", this.Pkcs8ShroudedKeyBagSafeBag(aa, attributes)));
				this._keyBagsChanged = true;
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00005D74 File Offset: 0x00003F74
		public object Clone()
		{
			PKCS12 pkcs;
			if (this._password != null)
			{
				pkcs = new PKCS12(this.GetBytes(), Encoding.BigEndianUnicode.GetString(this._password));
			}
			else
			{
				pkcs = new PKCS12(this.GetBytes());
			}
			pkcs.IterationCount = this.IterationCount;
			return pkcs;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00005DC2 File Offset: 0x00003FC2
		public static int MaximumPasswordLength
		{
			get
			{
				return PKCS12.password_max_length;
			}
		}

		// Token: 0x0400002C RID: 44
		private byte[] _password;

		// Token: 0x0400002D RID: 45
		private ArrayList _keyBags;

		// Token: 0x0400002E RID: 46
		private ArrayList _secretBags;

		// Token: 0x0400002F RID: 47
		private X509CertificateCollection _certs;

		// Token: 0x04000030 RID: 48
		private bool _keyBagsChanged;

		// Token: 0x04000031 RID: 49
		private bool _secretBagsChanged;

		// Token: 0x04000032 RID: 50
		private bool _certsChanged;

		// Token: 0x04000033 RID: 51
		private int _iterations;

		// Token: 0x04000034 RID: 52
		private ArrayList _safeBags;

		// Token: 0x04000035 RID: 53
		private RandomNumberGenerator _rng;

		// Token: 0x04000036 RID: 54
		private static int password_max_length = int.MaxValue;

		// Token: 0x02000015 RID: 21
		public class DeriveBytes
		{
			// Token: 0x17000020 RID: 32
			// (set) Token: 0x0600008C RID: 140 RVA: 0x00005DD5 File Offset: 0x00003FD5
			public string HashName
			{
				set
				{
					this._hashName = value;
				}
			}

			// Token: 0x17000021 RID: 33
			// (set) Token: 0x0600008D RID: 141 RVA: 0x00005DDE File Offset: 0x00003FDE
			public int IterationCount
			{
				set
				{
					this._iterations = value;
				}
			}

			// Token: 0x17000022 RID: 34
			// (set) Token: 0x0600008E RID: 142 RVA: 0x00005DE7 File Offset: 0x00003FE7
			public byte[] Password
			{
				set
				{
					if (value == null)
					{
						this._password = new byte[0];
						return;
					}
					this._password = (byte[])value.Clone();
				}
			}

			// Token: 0x17000023 RID: 35
			// (set) Token: 0x0600008F RID: 143 RVA: 0x00005E0A File Offset: 0x0000400A
			public byte[] Salt
			{
				set
				{
					if (value != null)
					{
						this._salt = (byte[])value.Clone();
						return;
					}
					this._salt = null;
				}
			}

			// Token: 0x06000090 RID: 144 RVA: 0x00005E28 File Offset: 0x00004028
			private void Adjust(byte[] a, int aOff, byte[] b)
			{
				int num = (int)((b[b.Length - 1] & byte.MaxValue) + (a[aOff + b.Length - 1] & byte.MaxValue) + 1);
				a[aOff + b.Length - 1] = (byte)num;
				num >>= 8;
				for (int i = b.Length - 2; i >= 0; i--)
				{
					num += (int)((b[i] & byte.MaxValue) + (a[aOff + i] & byte.MaxValue));
					a[aOff + i] = (byte)num;
					num >>= 8;
				}
			}

			// Token: 0x06000091 RID: 145 RVA: 0x00005E98 File Offset: 0x00004098
			private byte[] Derive(byte[] diversifier, int n)
			{
				HashAlgorithm hashAlgorithm = PKCS1.CreateFromName(this._hashName);
				int num = hashAlgorithm.HashSize >> 3;
				int num2 = 64;
				byte[] array = new byte[n];
				byte[] array2;
				if (this._salt != null && this._salt.Length != 0)
				{
					array2 = new byte[num2 * ((this._salt.Length + num2 - 1) / num2)];
					for (int num3 = 0; num3 != array2.Length; num3++)
					{
						array2[num3] = this._salt[num3 % this._salt.Length];
					}
				}
				else
				{
					array2 = new byte[0];
				}
				byte[] array3;
				if (this._password != null && this._password.Length != 0)
				{
					array3 = new byte[num2 * ((this._password.Length + num2 - 1) / num2)];
					for (int num4 = 0; num4 != array3.Length; num4++)
					{
						array3[num4] = this._password[num4 % this._password.Length];
					}
				}
				else
				{
					array3 = new byte[0];
				}
				byte[] array4 = new byte[array2.Length + array3.Length];
				Buffer.BlockCopy(array2, 0, array4, 0, array2.Length);
				Buffer.BlockCopy(array3, 0, array4, array2.Length, array3.Length);
				byte[] array5 = new byte[num2];
				int num5 = (n + num - 1) / num;
				for (int i = 1; i <= num5; i++)
				{
					hashAlgorithm.TransformBlock(diversifier, 0, diversifier.Length, diversifier, 0);
					hashAlgorithm.TransformFinalBlock(array4, 0, array4.Length);
					byte[] array6 = hashAlgorithm.Hash;
					hashAlgorithm.Initialize();
					for (int num6 = 1; num6 != this._iterations; num6++)
					{
						array6 = hashAlgorithm.ComputeHash(array6, 0, array6.Length);
					}
					for (int num7 = 0; num7 != array5.Length; num7++)
					{
						array5[num7] = array6[num7 % array6.Length];
					}
					for (int num8 = 0; num8 != array4.Length / num2; num8++)
					{
						this.Adjust(array4, num8 * num2, array5);
					}
					if (i == num5)
					{
						Buffer.BlockCopy(array6, 0, array, (i - 1) * num, array.Length - (i - 1) * num);
					}
					else
					{
						Buffer.BlockCopy(array6, 0, array, (i - 1) * num, array6.Length);
					}
				}
				return array;
			}

			// Token: 0x06000092 RID: 146 RVA: 0x0000609D File Offset: 0x0000429D
			public byte[] DeriveKey(int size)
			{
				return this.Derive(PKCS12.DeriveBytes.keyDiversifier, size);
			}

			// Token: 0x06000093 RID: 147 RVA: 0x000060AB File Offset: 0x000042AB
			public byte[] DeriveIV(int size)
			{
				return this.Derive(PKCS12.DeriveBytes.ivDiversifier, size);
			}

			// Token: 0x06000094 RID: 148 RVA: 0x000060B9 File Offset: 0x000042B9
			public byte[] DeriveMAC(int size)
			{
				return this.Derive(PKCS12.DeriveBytes.macDiversifier, size);
			}

			// Token: 0x04000037 RID: 55
			private static byte[] keyDiversifier = new byte[]
			{
				1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
				1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
				1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
				1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
				1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
				1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
				1, 1, 1, 1
			};

			// Token: 0x04000038 RID: 56
			private static byte[] ivDiversifier = new byte[]
			{
				2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
				2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
				2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
				2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
				2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
				2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
				2, 2, 2, 2
			};

			// Token: 0x04000039 RID: 57
			private static byte[] macDiversifier = new byte[]
			{
				3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
				3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
				3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
				3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
				3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
				3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
				3, 3, 3, 3
			};

			// Token: 0x0400003A RID: 58
			private string _hashName;

			// Token: 0x0400003B RID: 59
			private int _iterations;

			// Token: 0x0400003C RID: 60
			private byte[] _password;

			// Token: 0x0400003D RID: 61
			private byte[] _salt;
		}
	}
}
