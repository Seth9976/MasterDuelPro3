using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x0200004C RID: 76
	public sealed class PKCS1
	{
		// Token: 0x060001A3 RID: 419 RVA: 0x0000A494 File Offset: 0x00008694
		private static bool Compare(byte[] array1, byte[] array2)
		{
			bool flag = array1.Length == array2.Length;
			if (flag)
			{
				for (int i = 0; i < array1.Length; i++)
				{
					if (array1[i] != array2[i])
					{
						return false;
					}
				}
			}
			return flag;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000A4C8 File Offset: 0x000086C8
		public static byte[] I2OSP(byte[] x, int size)
		{
			byte[] array = new byte[size];
			Buffer.BlockCopy(x, 0, array, array.Length - x.Length, x.Length);
			return array;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000A4F0 File Offset: 0x000086F0
		public static byte[] OS2IP(byte[] x)
		{
			int num = 0;
			while (x[num++] == 0 && num < x.Length)
			{
			}
			num--;
			if (num > 0)
			{
				byte[] array = new byte[x.Length - num];
				Buffer.BlockCopy(x, num, array, 0, array.Length);
				return array;
			}
			return x;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000A530 File Offset: 0x00008730
		public static byte[] RSAVP1(RSA rsa, byte[] s)
		{
			return rsa.EncryptValue(s);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000A53C File Offset: 0x0000873C
		public static bool Verify_v15(RSA rsa, HashAlgorithm hash, byte[] hashValue, byte[] signature, bool tryNonStandardEncoding)
		{
			int num = rsa.KeySize >> 3;
			byte[] array = PKCS1.OS2IP(signature);
			byte[] array2 = PKCS1.I2OSP(PKCS1.RSAVP1(rsa, array), num);
			bool flag = PKCS1.Compare(PKCS1.Encode_v15(hash, hashValue, num), array2);
			if (flag || !tryNonStandardEncoding)
			{
				return flag;
			}
			if (array2[0] != 0 || array2[1] != 1)
			{
				return false;
			}
			int i;
			for (i = 2; i < array2.Length - hashValue.Length - 1; i++)
			{
				if (array2[i] != 255)
				{
					return false;
				}
			}
			if (array2[i++] != 0)
			{
				return false;
			}
			byte[] array3 = new byte[hashValue.Length];
			Buffer.BlockCopy(array2, i, array3, 0, array3.Length);
			return PKCS1.Compare(array3, hashValue);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000A5E0 File Offset: 0x000087E0
		public static byte[] Encode_v15(HashAlgorithm hash, byte[] hashValue, int emLength)
		{
			if (hashValue.Length != hash.HashSize >> 3)
			{
				throw new CryptographicException("bad hash length for " + hash.ToString());
			}
			string text = CryptoConfig.MapNameToOID(hash.ToString());
			byte[] array;
			if (text != null)
			{
				ASN1 asn = new ASN1(48);
				asn.Add(new ASN1(CryptoConfig.EncodeOID(text)));
				asn.Add(new ASN1(5));
				ASN1 asn2 = new ASN1(4, hashValue);
				ASN1 asn3 = new ASN1(48);
				asn3.Add(asn);
				asn3.Add(asn2);
				array = asn3.GetBytes();
			}
			else
			{
				array = hashValue;
			}
			Buffer.BlockCopy(hashValue, 0, array, array.Length - hashValue.Length, hashValue.Length);
			int num = Math.Max(8, emLength - array.Length - 3);
			byte[] array2 = new byte[num + array.Length + 3];
			array2[1] = 1;
			for (int i = 2; i < num + 2; i++)
			{
				array2[i] = byte.MaxValue;
			}
			Buffer.BlockCopy(array, 0, array2, num + 3, array.Length);
			return array2;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000A6D4 File Offset: 0x000088D4
		internal static string HashNameFromOid(string oid, bool throwOnError = true)
		{
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(oid);
			if (num <= 719034781U)
			{
				if (num <= 601591448U)
				{
					if (num != 510574318U)
					{
						if (num != 601591448U)
						{
							goto IL_0173;
						}
						if (!(oid == "1.2.840.113549.1.1.5"))
						{
							goto IL_0173;
						}
					}
					else if (!(oid == "1.2.840.10040.4.3"))
					{
						goto IL_0173;
					}
				}
				else if (num != 618369067U)
				{
					if (num != 702257162U)
					{
						if (num != 719034781U)
						{
							goto IL_0173;
						}
						if (!(oid == "1.2.840.113549.1.1.2"))
						{
							goto IL_0173;
						}
						return "MD2";
					}
					else
					{
						if (!(oid == "1.2.840.113549.1.1.3"))
						{
							goto IL_0173;
						}
						return "MD4";
					}
				}
				else
				{
					if (!(oid == "1.2.840.113549.1.1.4"))
					{
						goto IL_0173;
					}
					return "MD5";
				}
			}
			else if (num <= 2477476687U)
			{
				if (num != 875536856U)
				{
					if (num != 2477476687U)
					{
						goto IL_0173;
					}
					if (!(oid == "1.2.840.113549.1.1.11"))
					{
						goto IL_0173;
					}
					return "SHA256";
				}
				else if (!(oid == "1.3.14.3.2.29"))
				{
					goto IL_0173;
				}
			}
			else if (num != 2494254306U)
			{
				if (num != 2511031925U)
				{
					if (num != 3493391575U)
					{
						goto IL_0173;
					}
					if (!(oid == "1.3.36.3.3.1.2"))
					{
						goto IL_0173;
					}
					return "RIPEMD160";
				}
				else
				{
					if (!(oid == "1.2.840.113549.1.1.13"))
					{
						goto IL_0173;
					}
					return "SHA512";
				}
			}
			else
			{
				if (!(oid == "1.2.840.113549.1.1.12"))
				{
					goto IL_0173;
				}
				return "SHA384";
			}
			return "SHA1";
			IL_0173:
			if (throwOnError)
			{
				throw new CryptographicException("Unsupported hash algorithm: " + oid);
			}
			return null;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000A869 File Offset: 0x00008A69
		internal static HashAlgorithm CreateFromOid(string oid)
		{
			return PKCS1.CreateFromName(PKCS1.HashNameFromOid(oid, true));
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000A877 File Offset: 0x00008A77
		internal static HashAlgorithm CreateFromName(string name)
		{
			return HashAlgorithm.Create(name);
		}

		// Token: 0x04000205 RID: 517
		private static byte[] emptySHA1 = new byte[]
		{
			218, 57, 163, 238, 94, 107, 75, 13, 50, 85,
			191, 239, 149, 96, 24, 144, 175, 216, 7, 9
		};

		// Token: 0x04000206 RID: 518
		private static byte[] emptySHA256 = new byte[]
		{
			227, 176, 196, 66, 152, 252, 28, 20, 154, 251,
			244, 200, 153, 111, 185, 36, 39, 174, 65, 228,
			100, 155, 147, 76, 164, 149, 153, 27, 120, 82,
			184, 85
		};

		// Token: 0x04000207 RID: 519
		private static byte[] emptySHA384 = new byte[]
		{
			56, 176, 96, 167, 81, 172, 150, 56, 76, 217,
			50, 126, 177, 177, 227, 106, 33, 253, 183, 17,
			20, 190, 7, 67, 76, 12, 199, 191, 99, 246,
			225, 218, 39, 78, 222, 191, 231, 111, 101, 251,
			213, 26, 210, 241, 72, 152, 185, 91
		};

		// Token: 0x04000208 RID: 520
		private static byte[] emptySHA512 = new byte[]
		{
			207, 131, 225, 53, 126, 239, 184, 189, 241, 84,
			40, 80, 214, 109, 128, 7, 214, 32, 228, 5,
			11, 87, 21, 220, 131, 244, 169, 33, 211, 108,
			233, 206, 71, 208, 209, 60, 93, 133, 242, 176,
			byte.MaxValue, 131, 24, 210, 135, 126, 236, 47, 99, 185,
			49, 189, 71, 65, 122, 129, 165, 56, 50, 122,
			249, 39, 218, 62
		};
	}
}
