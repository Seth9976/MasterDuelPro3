using System;
using System.Collections;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x0200004D RID: 77
	public sealed class PKCS8
	{
		// Token: 0x0200004E RID: 78
		public class PrivateKeyInfo
		{
			// Token: 0x060001AD RID: 429 RVA: 0x0000A8E9 File Offset: 0x00008AE9
			public PrivateKeyInfo()
			{
				this._version = 0;
				this._list = new ArrayList();
			}

			// Token: 0x060001AE RID: 430 RVA: 0x0000A903 File Offset: 0x00008B03
			public PrivateKeyInfo(byte[] data)
				: this()
			{
				this.Decode(data);
			}

			// Token: 0x1700007B RID: 123
			// (get) Token: 0x060001AF RID: 431 RVA: 0x0000A912 File Offset: 0x00008B12
			// (set) Token: 0x060001B0 RID: 432 RVA: 0x0000A91A File Offset: 0x00008B1A
			public string Algorithm
			{
				get
				{
					return this._algorithm;
				}
				set
				{
					this._algorithm = value;
				}
			}

			// Token: 0x1700007C RID: 124
			// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000A923 File Offset: 0x00008B23
			// (set) Token: 0x060001B2 RID: 434 RVA: 0x0000A93F File Offset: 0x00008B3F
			public byte[] PrivateKey
			{
				get
				{
					if (this._key == null)
					{
						return null;
					}
					return (byte[])this._key.Clone();
				}
				set
				{
					if (value == null)
					{
						throw new ArgumentNullException("PrivateKey");
					}
					this._key = (byte[])value.Clone();
				}
			}

			// Token: 0x060001B3 RID: 435 RVA: 0x0000A960 File Offset: 0x00008B60
			private void Decode(byte[] data)
			{
				ASN1 asn = new ASN1(data);
				if (asn.Tag != 48)
				{
					throw new CryptographicException("invalid PrivateKeyInfo");
				}
				ASN1 asn2 = asn[0];
				if (asn2.Tag != 2)
				{
					throw new CryptographicException("invalid version");
				}
				this._version = (int)asn2.Value[0];
				ASN1 asn3 = asn[1];
				if (asn3.Tag != 48)
				{
					throw new CryptographicException("invalid algorithm");
				}
				ASN1 asn4 = asn3[0];
				if (asn4.Tag != 6)
				{
					throw new CryptographicException("missing algorithm OID");
				}
				this._algorithm = ASN1Convert.ToOid(asn4);
				ASN1 asn5 = asn[2];
				this._key = asn5.Value;
				if (asn.Count > 3)
				{
					ASN1 asn6 = asn[3];
					for (int i = 0; i < asn6.Count; i++)
					{
						this._list.Add(asn6[i]);
					}
				}
			}

			// Token: 0x060001B4 RID: 436 RVA: 0x0000AA48 File Offset: 0x00008C48
			public byte[] GetBytes()
			{
				ASN1 asn = new ASN1(48);
				asn.Add(ASN1Convert.FromOid(this._algorithm));
				asn.Add(new ASN1(5));
				ASN1 asn2 = new ASN1(48);
				asn2.Add(new ASN1(2, new byte[] { (byte)this._version }));
				asn2.Add(asn);
				asn2.Add(new ASN1(4, this._key));
				if (this._list.Count > 0)
				{
					ASN1 asn3 = new ASN1(160);
					foreach (object obj in this._list)
					{
						ASN1 asn4 = (ASN1)obj;
						asn3.Add(asn4);
					}
					asn2.Add(asn3);
				}
				return asn2.GetBytes();
			}

			// Token: 0x060001B5 RID: 437 RVA: 0x0000AB38 File Offset: 0x00008D38
			private static byte[] RemoveLeadingZero(byte[] bigInt)
			{
				int num = 0;
				int num2 = bigInt.Length;
				if (bigInt[0] == 0)
				{
					num = 1;
					num2--;
				}
				byte[] array = new byte[num2];
				Buffer.BlockCopy(bigInt, num, array, 0, num2);
				return array;
			}

			// Token: 0x060001B6 RID: 438 RVA: 0x0000AB68 File Offset: 0x00008D68
			private static byte[] Normalize(byte[] bigInt, int length)
			{
				if (bigInt.Length == length)
				{
					return bigInt;
				}
				if (bigInt.Length > length)
				{
					return PKCS8.PrivateKeyInfo.RemoveLeadingZero(bigInt);
				}
				byte[] array = new byte[length];
				Buffer.BlockCopy(bigInt, 0, array, length - bigInt.Length, bigInt.Length);
				return array;
			}

			// Token: 0x060001B7 RID: 439 RVA: 0x0000ABA4 File Offset: 0x00008DA4
			public static RSA DecodeRSA(byte[] keypair)
			{
				ASN1 asn = new ASN1(keypair);
				if (asn.Tag != 48)
				{
					throw new CryptographicException("invalid private key format");
				}
				if (asn[0].Tag != 2)
				{
					throw new CryptographicException("missing version");
				}
				if (asn.Count < 9)
				{
					throw new CryptographicException("not enough key parameters");
				}
				RSAParameters rsaparameters = new RSAParameters
				{
					Modulus = PKCS8.PrivateKeyInfo.RemoveLeadingZero(asn[1].Value)
				};
				int num = rsaparameters.Modulus.Length;
				int num2 = num >> 1;
				rsaparameters.D = PKCS8.PrivateKeyInfo.Normalize(asn[3].Value, num);
				rsaparameters.DP = PKCS8.PrivateKeyInfo.Normalize(asn[6].Value, num2);
				rsaparameters.DQ = PKCS8.PrivateKeyInfo.Normalize(asn[7].Value, num2);
				rsaparameters.Exponent = PKCS8.PrivateKeyInfo.RemoveLeadingZero(asn[2].Value);
				rsaparameters.InverseQ = PKCS8.PrivateKeyInfo.Normalize(asn[8].Value, num2);
				rsaparameters.P = PKCS8.PrivateKeyInfo.Normalize(asn[4].Value, num2);
				rsaparameters.Q = PKCS8.PrivateKeyInfo.Normalize(asn[5].Value, num2);
				RSA rsa = null;
				try
				{
					rsa = RSA.Create();
					rsa.ImportParameters(rsaparameters);
				}
				catch (CryptographicException)
				{
					rsa = new RSACryptoServiceProvider(new CspParameters
					{
						Flags = CspProviderFlags.UseMachineKeyStore
					});
					rsa.ImportParameters(rsaparameters);
				}
				return rsa;
			}

			// Token: 0x060001B8 RID: 440 RVA: 0x0000AD1C File Offset: 0x00008F1C
			public static byte[] Encode(RSA rsa)
			{
				RSAParameters rsaparameters = rsa.ExportParameters(true);
				ASN1 asn = new ASN1(48);
				asn.Add(new ASN1(2, new byte[1]));
				asn.Add(ASN1Convert.FromUnsignedBigInteger(rsaparameters.Modulus));
				asn.Add(ASN1Convert.FromUnsignedBigInteger(rsaparameters.Exponent));
				asn.Add(ASN1Convert.FromUnsignedBigInteger(rsaparameters.D));
				asn.Add(ASN1Convert.FromUnsignedBigInteger(rsaparameters.P));
				asn.Add(ASN1Convert.FromUnsignedBigInteger(rsaparameters.Q));
				asn.Add(ASN1Convert.FromUnsignedBigInteger(rsaparameters.DP));
				asn.Add(ASN1Convert.FromUnsignedBigInteger(rsaparameters.DQ));
				asn.Add(ASN1Convert.FromUnsignedBigInteger(rsaparameters.InverseQ));
				return asn.GetBytes();
			}

			// Token: 0x060001B9 RID: 441 RVA: 0x0000ADE4 File Offset: 0x00008FE4
			public static DSA DecodeDSA(byte[] privateKey, DSAParameters dsaParameters)
			{
				ASN1 asn = new ASN1(privateKey);
				if (asn.Tag != 2)
				{
					throw new CryptographicException("invalid private key format");
				}
				dsaParameters.X = PKCS8.PrivateKeyInfo.Normalize(asn.Value, 20);
				DSA dsa = DSA.Create();
				dsa.ImportParameters(dsaParameters);
				return dsa;
			}

			// Token: 0x060001BA RID: 442 RVA: 0x0000AE2C File Offset: 0x0000902C
			public static byte[] Encode(DSA dsa)
			{
				return ASN1Convert.FromUnsignedBigInteger(dsa.ExportParameters(true).X).GetBytes();
			}

			// Token: 0x060001BB RID: 443 RVA: 0x0000AE44 File Offset: 0x00009044
			public static byte[] Encode(AsymmetricAlgorithm aa)
			{
				if (aa is RSA)
				{
					return PKCS8.PrivateKeyInfo.Encode((RSA)aa);
				}
				if (aa is DSA)
				{
					return PKCS8.PrivateKeyInfo.Encode((DSA)aa);
				}
				throw new CryptographicException("Unknown asymmetric algorithm {0}", aa.ToString());
			}

			// Token: 0x04000209 RID: 521
			private int _version;

			// Token: 0x0400020A RID: 522
			private string _algorithm;

			// Token: 0x0400020B RID: 523
			private byte[] _key;

			// Token: 0x0400020C RID: 524
			private ArrayList _list;
		}

		// Token: 0x0200004F RID: 79
		public class EncryptedPrivateKeyInfo
		{
			// Token: 0x060001BC RID: 444 RVA: 0x0000293D File Offset: 0x00000B3D
			public EncryptedPrivateKeyInfo()
			{
			}

			// Token: 0x060001BD RID: 445 RVA: 0x0000AE7E File Offset: 0x0000907E
			public EncryptedPrivateKeyInfo(byte[] data)
				: this()
			{
				this.Decode(data);
			}

			// Token: 0x1700007D RID: 125
			// (get) Token: 0x060001BE RID: 446 RVA: 0x0000AE8D File Offset: 0x0000908D
			// (set) Token: 0x060001BF RID: 447 RVA: 0x0000AE95 File Offset: 0x00009095
			public string Algorithm
			{
				get
				{
					return this._algorithm;
				}
				set
				{
					this._algorithm = value;
				}
			}

			// Token: 0x1700007E RID: 126
			// (get) Token: 0x060001C0 RID: 448 RVA: 0x0000AE9E File Offset: 0x0000909E
			// (set) Token: 0x060001C1 RID: 449 RVA: 0x0000AEBA File Offset: 0x000090BA
			public byte[] EncryptedData
			{
				get
				{
					if (this._data != null)
					{
						return (byte[])this._data.Clone();
					}
					return null;
				}
				set
				{
					this._data = ((value == null) ? null : ((byte[])value.Clone()));
				}
			}

			// Token: 0x1700007F RID: 127
			// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000AED3 File Offset: 0x000090D3
			public byte[] Salt
			{
				get
				{
					if (this._salt == null)
					{
						RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
						this._salt = new byte[8];
						randomNumberGenerator.GetBytes(this._salt);
					}
					return (byte[])this._salt.Clone();
				}
			}

			// Token: 0x17000080 RID: 128
			// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000AF09 File Offset: 0x00009109
			// (set) Token: 0x060001C4 RID: 452 RVA: 0x0000AF11 File Offset: 0x00009111
			public int IterationCount
			{
				get
				{
					return this._iterations;
				}
				set
				{
					if (value < 0)
					{
						throw new ArgumentOutOfRangeException("IterationCount", "Negative");
					}
					this._iterations = value;
				}
			}

			// Token: 0x060001C5 RID: 453 RVA: 0x0000AF30 File Offset: 0x00009130
			private void Decode(byte[] data)
			{
				ASN1 asn = new ASN1(data);
				if (asn.Tag != 48)
				{
					throw new CryptographicException("invalid EncryptedPrivateKeyInfo");
				}
				ASN1 asn2 = asn[0];
				if (asn2.Tag != 48)
				{
					throw new CryptographicException("invalid encryptionAlgorithm");
				}
				ASN1 asn3 = asn2[0];
				if (asn3.Tag != 6)
				{
					throw new CryptographicException("invalid algorithm");
				}
				this._algorithm = ASN1Convert.ToOid(asn3);
				if (asn2.Count > 1)
				{
					ASN1 asn4 = asn2[1];
					if (asn4.Tag != 48)
					{
						throw new CryptographicException("invalid parameters");
					}
					ASN1 asn5 = asn4[0];
					if (asn5.Tag != 4)
					{
						throw new CryptographicException("invalid salt");
					}
					this._salt = asn5.Value;
					ASN1 asn6 = asn4[1];
					if (asn6.Tag != 2)
					{
						throw new CryptographicException("invalid iterationCount");
					}
					this._iterations = ASN1Convert.ToInt32(asn6);
				}
				ASN1 asn7 = asn[1];
				if (asn7.Tag != 4)
				{
					throw new CryptographicException("invalid EncryptedData");
				}
				this._data = asn7.Value;
			}

			// Token: 0x060001C6 RID: 454 RVA: 0x0000B03C File Offset: 0x0000923C
			public byte[] GetBytes()
			{
				if (this._algorithm == null)
				{
					throw new CryptographicException("No algorithm OID specified");
				}
				ASN1 asn = new ASN1(48);
				asn.Add(ASN1Convert.FromOid(this._algorithm));
				if (this._iterations > 0 || this._salt != null)
				{
					ASN1 asn2 = new ASN1(4, this._salt);
					ASN1 asn3 = ASN1Convert.FromInt32(this._iterations);
					ASN1 asn4 = new ASN1(48);
					asn4.Add(asn2);
					asn4.Add(asn3);
					asn.Add(asn4);
				}
				ASN1 asn5 = new ASN1(4, this._data);
				ASN1 asn6 = new ASN1(48);
				asn6.Add(asn);
				asn6.Add(asn5);
				return asn6.GetBytes();
			}

			// Token: 0x0400020D RID: 525
			private string _algorithm;

			// Token: 0x0400020E RID: 526
			private byte[] _salt;

			// Token: 0x0400020F RID: 527
			private int _iterations;

			// Token: 0x04000210 RID: 528
			private byte[] _data;
		}
	}
}
