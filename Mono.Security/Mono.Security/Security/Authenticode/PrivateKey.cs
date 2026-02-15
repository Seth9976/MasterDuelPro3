using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Mono.Security.Cryptography;

namespace Mono.Security.Authenticode
{
	// Token: 0x02000055 RID: 85
	public class PrivateKey
	{
		// Token: 0x060001EE RID: 494 RVA: 0x0000CBF1 File Offset: 0x0000ADF1
		public PrivateKey(byte[] data, string password)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (!this.Decode(data, password))
			{
				throw new CryptographicException(Locale.GetText("Invalid data and/or password"));
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000CC21 File Offset: 0x0000AE21
		public RSA RSA
		{
			get
			{
				return this.rsa;
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000CC2C File Offset: 0x0000AE2C
		private byte[] DeriveKey(byte[] salt, string password)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(password);
			SHA1 sha = SHA1.Create();
			sha.TransformBlock(salt, 0, salt.Length, salt, 0);
			sha.TransformFinalBlock(bytes, 0, bytes.Length);
			byte[] array = new byte[16];
			Buffer.BlockCopy(sha.Hash, 0, array, 0, 16);
			sha.Clear();
			Array.Clear(bytes, 0, bytes.Length);
			return array;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000CC8C File Offset: 0x0000AE8C
		private bool Decode(byte[] pvk, string password)
		{
			if (BitConverterLE.ToUInt32(pvk, 0) != 2964713758U)
			{
				return false;
			}
			if (BitConverterLE.ToUInt32(pvk, 4) != 0U)
			{
				return false;
			}
			this.keyType = BitConverterLE.ToInt32(pvk, 8);
			this.encrypted = BitConverterLE.ToUInt32(pvk, 12) == 1U;
			int num = BitConverterLE.ToInt32(pvk, 16);
			int num2 = BitConverterLE.ToInt32(pvk, 20);
			byte[] array = new byte[num2];
			Buffer.BlockCopy(pvk, 24 + num, array, 0, num2);
			if (num > 0)
			{
				if (password == null)
				{
					return false;
				}
				byte[] array2 = new byte[num];
				Buffer.BlockCopy(pvk, 24, array2, 0, num);
				byte[] array3 = this.DeriveKey(array2, password);
				RC4.Create().CreateDecryptor(array3, null).TransformBlock(array, 8, array.Length - 8, array, 8);
				try
				{
					this.rsa = CryptoConvert.FromCapiPrivateKeyBlob(array);
					this.weak = false;
				}
				catch (CryptographicException)
				{
					this.weak = true;
					Buffer.BlockCopy(pvk, 24 + num, array, 0, num2);
					Array.Clear(array3, 5, 11);
					RC4.Create().CreateDecryptor(array3, null).TransformBlock(array, 8, array.Length - 8, array, 8);
					this.rsa = CryptoConvert.FromCapiPrivateKeyBlob(array);
				}
				Array.Clear(array3, 0, array3.Length);
			}
			else
			{
				this.weak = true;
				this.rsa = CryptoConvert.FromCapiPrivateKeyBlob(array);
				Array.Clear(array, 0, array.Length);
			}
			Array.Clear(pvk, 0, pvk.Length);
			return this.rsa != null;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000CDE8 File Offset: 0x0000AFE8
		public static PrivateKey CreateFromFile(string filename)
		{
			return PrivateKey.CreateFromFile(filename, null);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000CDF4 File Offset: 0x0000AFF4
		public static PrivateKey CreateFromFile(string filename, string password)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			byte[] array = null;
			using (FileStream fileStream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
			}
			return new PrivateKey(array, password);
		}

		// Token: 0x04000236 RID: 566
		private bool encrypted;

		// Token: 0x04000237 RID: 567
		private RSA rsa;

		// Token: 0x04000238 RID: 568
		private bool weak;

		// Token: 0x04000239 RID: 569
		private int keyType;
	}
}
