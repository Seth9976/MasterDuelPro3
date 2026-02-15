using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x0200006E RID: 110
	internal sealed class PKCS1
	{
		// Token: 0x060001BC RID: 444 RVA: 0x0000AEA8 File Offset: 0x000090A8
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

		// Token: 0x060001BD RID: 445 RVA: 0x0000AEDC File Offset: 0x000090DC
		public static byte[] I2OSP(byte[] x, int size)
		{
			byte[] array = new byte[size];
			Buffer.BlockCopy(x, 0, array, array.Length - x.Length, x.Length);
			return array;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000AF04 File Offset: 0x00009104
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

		// Token: 0x060001BF RID: 447 RVA: 0x0000AF44 File Offset: 0x00009144
		public static byte[] RSASP1(RSA rsa, byte[] m)
		{
			return rsa.DecryptValue(m);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000AF4D File Offset: 0x0000914D
		public static byte[] RSAVP1(RSA rsa, byte[] s)
		{
			return rsa.EncryptValue(s);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000AF58 File Offset: 0x00009158
		public static byte[] Sign_v15(RSA rsa, HashAlgorithm hash, byte[] hashValue)
		{
			int num = rsa.KeySize >> 3;
			byte[] array = PKCS1.OS2IP(PKCS1.Encode_v15(hash, hashValue, num));
			return PKCS1.I2OSP(PKCS1.RSASP1(rsa, array), num);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000AF8C File Offset: 0x0000918C
		internal static byte[] Sign_v15(RSA rsa, string hashName, byte[] hashValue)
		{
			byte[] array;
			using (HashAlgorithm hashAlgorithm = PKCS1.CreateFromName(hashName))
			{
				array = PKCS1.Sign_v15(rsa, hashAlgorithm, hashValue);
			}
			return array;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000AFC8 File Offset: 0x000091C8
		public static bool Verify_v15(RSA rsa, HashAlgorithm hash, byte[] hashValue, byte[] signature)
		{
			return PKCS1.Verify_v15(rsa, hash, hashValue, signature, false);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000AFD4 File Offset: 0x000091D4
		internal static bool Verify_v15(RSA rsa, string hashName, byte[] hashValue, byte[] signature)
		{
			bool flag;
			using (HashAlgorithm hashAlgorithm = PKCS1.CreateFromName(hashName))
			{
				flag = PKCS1.Verify_v15(rsa, hashAlgorithm, hashValue, signature, false);
			}
			return flag;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000B010 File Offset: 0x00009210
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

		// Token: 0x060001C6 RID: 454 RVA: 0x0000B0B4 File Offset: 0x000092B4
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

		// Token: 0x060001C7 RID: 455 RVA: 0x00008257 File Offset: 0x00006457
		internal static HashAlgorithm CreateFromName(string name)
		{
			return HashAlgorithm.Create(name);
		}

		// Token: 0x040001FC RID: 508
		private static byte[] emptySHA1 = new byte[]
		{
			218, 57, 163, 238, 94, 107, 75, 13, 50, 85,
			191, 239, 149, 96, 24, 144, 175, 216, 7, 9
		};

		// Token: 0x040001FD RID: 509
		private static byte[] emptySHA256 = new byte[]
		{
			227, 176, 196, 66, 152, 252, 28, 20, 154, 251,
			244, 200, 153, 111, 185, 36, 39, 174, 65, 228,
			100, 155, 147, 76, 164, 149, 153, 27, 120, 82,
			184, 85
		};

		// Token: 0x040001FE RID: 510
		private static byte[] emptySHA384 = new byte[]
		{
			56, 176, 96, 167, 81, 172, 150, 56, 76, 217,
			50, 126, 177, 177, 227, 106, 33, 253, 183, 17,
			20, 190, 7, 67, 76, 12, 199, 191, 99, 246,
			225, 218, 39, 78, 222, 191, 231, 111, 101, 251,
			213, 26, 210, 241, 72, 152, 185, 91
		};

		// Token: 0x040001FF RID: 511
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
