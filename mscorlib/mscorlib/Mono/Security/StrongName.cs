using System;
using System.IO;
using System.Security.Cryptography;
using Mono.Security.Cryptography;

namespace Mono.Security
{
	// Token: 0x02000065 RID: 101
	internal sealed class StrongName
	{
		// Token: 0x0600014F RID: 335 RVA: 0x00007F6C File Offset: 0x0000616C
		public StrongName(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (data.Length == 16)
			{
				int i = 0;
				int num = 0;
				while (i < data.Length)
				{
					num += (int)data[i++];
				}
				if (num == 4)
				{
					this.publicKey = (byte[])data.Clone();
					return;
				}
			}
			else
			{
				this.RSA = CryptoConvert.FromCapiKeyBlob(data);
				if (this.rsa == null)
				{
					throw new ArgumentException("data isn't a correctly encoded RSA public key");
				}
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00007FDF File Offset: 0x000061DF
		public StrongName(RSA rsa)
		{
			if (rsa == null)
			{
				throw new ArgumentNullException("rsa");
			}
			this.RSA = rsa;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00007FFC File Offset: 0x000061FC
		private void InvalidateCache()
		{
			this.publicKey = null;
			this.keyToken = null;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000152 RID: 338 RVA: 0x0000800C File Offset: 0x0000620C
		public bool CanSign
		{
			get
			{
				if (this.rsa == null)
				{
					return false;
				}
				if (this.RSA is RSACryptoServiceProvider)
				{
					return !(this.rsa as RSACryptoServiceProvider).PublicOnly;
				}
				if (this.RSA is RSAManaged)
				{
					return !(this.rsa as RSAManaged).PublicOnly;
				}
				bool flag;
				try
				{
					RSAParameters rsaparameters = this.rsa.ExportParameters(true);
					flag = rsaparameters.D != null && rsaparameters.P != null && rsaparameters.Q != null;
				}
				catch (CryptographicException)
				{
					flag = false;
				}
				return flag;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000153 RID: 339 RVA: 0x000080A8 File Offset: 0x000062A8
		// (set) Token: 0x06000154 RID: 340 RVA: 0x000080C3 File Offset: 0x000062C3
		public RSA RSA
		{
			get
			{
				if (this.rsa == null)
				{
					this.rsa = RSA.Create();
				}
				return this.rsa;
			}
			set
			{
				this.rsa = value;
				this.InvalidateCache();
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000155 RID: 341 RVA: 0x000080D4 File Offset: 0x000062D4
		public byte[] PublicKey
		{
			get
			{
				if (this.publicKey == null)
				{
					byte[] array = CryptoConvert.ToCapiKeyBlob(this.rsa, false);
					this.publicKey = new byte[32 + (this.rsa.KeySize >> 3)];
					this.publicKey[0] = array[4];
					this.publicKey[1] = array[5];
					this.publicKey[2] = array[6];
					this.publicKey[3] = array[7];
					this.publicKey[4] = 4;
					this.publicKey[5] = 128;
					this.publicKey[6] = 0;
					this.publicKey[7] = 0;
					byte[] bytes = BitConverterLE.GetBytes(this.publicKey.Length - 12);
					this.publicKey[8] = bytes[0];
					this.publicKey[9] = bytes[1];
					this.publicKey[10] = bytes[2];
					this.publicKey[11] = bytes[3];
					this.publicKey[12] = 6;
					Buffer.BlockCopy(array, 1, this.publicKey, 13, this.publicKey.Length - 13);
					this.publicKey[23] = 49;
				}
				return (byte[])this.publicKey.Clone();
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000156 RID: 342 RVA: 0x000081E8 File Offset: 0x000063E8
		public byte[] PublicKeyToken
		{
			get
			{
				if (this.keyToken == null)
				{
					byte[] array = this.PublicKey;
					if (array == null)
					{
						return null;
					}
					byte[] array2 = StrongName.GetHashAlgorithm(this.TokenAlgorithm).ComputeHash(array);
					this.keyToken = new byte[8];
					Buffer.BlockCopy(array2, array2.Length - 8, this.keyToken, 0, 8);
					Array.Reverse<byte>(this.keyToken, 0, 8);
				}
				return (byte[])this.keyToken.Clone();
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00008257 File Offset: 0x00006457
		private static HashAlgorithm GetHashAlgorithm(string algorithm)
		{
			return HashAlgorithm.Create(algorithm);
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000158 RID: 344 RVA: 0x0000825F File Offset: 0x0000645F
		public string TokenAlgorithm
		{
			get
			{
				if (this.tokenAlgorithm == null)
				{
					this.tokenAlgorithm = "SHA1";
				}
				return this.tokenAlgorithm;
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000827C File Offset: 0x0000647C
		private uint RVAtoPosition(uint r, int sections, byte[] headers)
		{
			for (int i = 0; i < sections; i++)
			{
				uint num = BitConverterLE.ToUInt32(headers, i * 40 + 20);
				uint num2 = BitConverterLE.ToUInt32(headers, i * 40 + 12);
				int num3 = (int)BitConverterLE.ToUInt32(headers, i * 40 + 8);
				if (num2 <= r && (ulong)r < (ulong)num2 + (ulong)((long)num3))
				{
					return num + r - num2;
				}
			}
			return 0U;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000082D2 File Offset: 0x000064D2
		private static StrongName.StrongNameSignature Error(string a)
		{
			return null;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000082D8 File Offset: 0x000064D8
		private static byte[] ReadMore(Stream stream, byte[] a, int newSize)
		{
			int num = a.Length;
			Array.Resize<byte>(ref a, newSize);
			if (newSize <= num)
			{
				return a;
			}
			int num2 = newSize - num;
			if (stream.Read(a, num, num2) != num2)
			{
				return null;
			}
			return a;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000830C File Offset: 0x0000650C
		internal StrongName.StrongNameSignature StrongHash(Stream stream, StrongName.StrongNameOptions options)
		{
			byte[] array = new byte[64];
			int num = stream.Read(array, 0, 64);
			if (num == 64 && array[0] == 77 && array[1] == 90)
			{
				int num2 = BitConverterLE.ToInt32(array, 60);
				if (num2 < 64)
				{
					return StrongName.Error("peHeader_lt_64");
				}
				array = StrongName.ReadMore(stream, array, num2);
				if (array == null)
				{
					return StrongName.Error("read_mz2_failed");
				}
			}
			else
			{
				if (num < 4 || array[0] != 80 || array[1] != 69 || array[2] != 0 || array[3] != 0)
				{
					return StrongName.Error("read_mz_or_mzsig_failed");
				}
				stream.Position = 0L;
				array = new byte[0];
			}
			int num3 = 2;
			int num4 = 24 + num3;
			byte[] array2 = new byte[num4];
			if (stream.Read(array2, 0, num4) != num4 || array2[0] != 80 || array2[1] != 69 || array2[2] != 0 || array2[3] != 0)
			{
				return StrongName.Error("read_minimumHeadersSize_or_pesig_failed");
			}
			num3 = (int)BitConverterLE.ToUInt16(array2, 20);
			if (num3 < 2)
			{
				return StrongName.Error(string.Format("sizeOfOptionalHeader_lt_2 ${0}", num3));
			}
			int num5 = 24 + num3;
			if (num5 < 24)
			{
				return StrongName.Error("headers_overflow");
			}
			array2 = StrongName.ReadMore(stream, array2, num5);
			if (array2 == null)
			{
				return StrongName.Error("read_pe2_failed");
			}
			uint num6 = (uint)BitConverterLE.ToUInt16(array2, 24);
			int num7 = 0;
			bool flag = false;
			if (num6 != 267U)
			{
				if (num6 == 523U)
				{
					num7 = 16;
				}
				else
				{
					if (num6 != 263U)
					{
						return StrongName.Error("bad_magic_value");
					}
					flag = true;
				}
			}
			uint num8 = 0U;
			if (!flag)
			{
				if (num3 >= 116 + num7 + 4)
				{
					num8 = BitConverterLE.ToUInt32(array2, 116 + num7);
				}
				int num9 = 64;
				while (num9 < num3 && num9 < 68)
				{
					array2[24 + num9] = 0;
					num9++;
				}
				int num10 = 128 + num7;
				while (num10 < num3 && num10 < 136 + num7)
				{
					array2[24 + num10] = 0;
					num10++;
				}
			}
			int num11 = (int)BitConverterLE.ToUInt16(array2, 6);
			byte[] array3 = new byte[num11 * 40];
			if (stream.Read(array3, 0, array3.Length) != array3.Length)
			{
				return StrongName.Error("read_section_headers_failed");
			}
			uint num12 = 0U;
			uint num13 = 0U;
			uint num14 = 0U;
			uint num15 = 0U;
			if (15U < num8 && num3 >= 216 + num7)
			{
				uint num16 = BitConverterLE.ToUInt32(array2, 232 + num7);
				uint num17 = this.RVAtoPosition(num16, num11, array3);
				int num18 = BitConverterLE.ToInt32(array2, 236 + num7);
				byte[] array4 = new byte[num18];
				stream.Position = (long)((ulong)num17);
				if (stream.Read(array4, 0, num18) != num18)
				{
					return StrongName.Error("read_cli_header_failed");
				}
				uint num19 = BitConverterLE.ToUInt32(array4, 32);
				num12 = this.RVAtoPosition(num19, num11, array3);
				num13 = BitConverterLE.ToUInt32(array4, 36);
				uint num20 = BitConverterLE.ToUInt32(array4, 8);
				num14 = this.RVAtoPosition(num20, num11, array3);
				num15 = BitConverterLE.ToUInt32(array4, 12);
			}
			StrongName.StrongNameSignature strongNameSignature = new StrongName.StrongNameSignature();
			strongNameSignature.SignaturePosition = num12;
			strongNameSignature.SignatureLength = num13;
			strongNameSignature.MetadataPosition = num14;
			strongNameSignature.MetadataLength = num15;
			using (HashAlgorithm hashAlgorithm = HashAlgorithm.Create(this.TokenAlgorithm))
			{
				if (options == StrongName.StrongNameOptions.Metadata)
				{
					hashAlgorithm.Initialize();
					byte[] array5 = new byte[num15];
					stream.Position = (long)((ulong)num14);
					if (stream.Read(array5, 0, (int)num15) != (int)num15)
					{
						return StrongName.Error("read_cli_metadata_failed");
					}
					strongNameSignature.Hash = hashAlgorithm.ComputeHash(array5);
					return strongNameSignature;
				}
				else
				{
					using (CryptoStream cryptoStream = new CryptoStream(Stream.Null, hashAlgorithm, CryptoStreamMode.Write))
					{
						cryptoStream.Write(array, 0, array.Length);
						cryptoStream.Write(array2, 0, array2.Length);
						cryptoStream.Write(array3, 0, array3.Length);
						for (int i = 0; i < num11; i++)
						{
							uint num21 = BitConverterLE.ToUInt32(array3, i * 40 + 20);
							int num22 = BitConverterLE.ToInt32(array3, i * 40 + 16);
							byte[] array6 = new byte[num22];
							stream.Position = (long)((ulong)num21);
							if (stream.Read(array6, 0, num22) != num22)
							{
								return StrongName.Error("read_section_failed");
							}
							if (num21 <= num12 && num12 < num21 + (uint)num22)
							{
								int num23 = (int)(num12 - num21);
								if (num23 > 0)
								{
									cryptoStream.Write(array6, 0, num23);
								}
								strongNameSignature.Signature = new byte[num13];
								Buffer.BlockCopy(array6, num23, strongNameSignature.Signature, 0, (int)num13);
								Array.Reverse<byte>(strongNameSignature.Signature);
								int num24 = (int)((long)num23 + (long)((ulong)num13));
								int num25 = num22 - num24;
								if (num25 > 0)
								{
									cryptoStream.Write(array6, num24, num25);
								}
							}
							else
							{
								cryptoStream.Write(array6, 0, num22);
							}
						}
					}
					strongNameSignature.Hash = hashAlgorithm.Hash;
				}
			}
			return strongNameSignature;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00008804 File Offset: 0x00006A04
		public bool Sign(string fileName)
		{
			StrongName.StrongNameSignature strongNameSignature;
			using (FileStream fileStream = File.OpenRead(fileName))
			{
				strongNameSignature = this.StrongHash(fileStream, StrongName.StrongNameOptions.Signature);
			}
			if (strongNameSignature.Hash == null)
			{
				return false;
			}
			byte[] array = null;
			try
			{
				RSAPKCS1SignatureFormatter rsapkcs1SignatureFormatter = new RSAPKCS1SignatureFormatter(this.rsa);
				rsapkcs1SignatureFormatter.SetHashAlgorithm(this.TokenAlgorithm);
				array = rsapkcs1SignatureFormatter.CreateSignature(strongNameSignature.Hash);
				Array.Reverse<byte>(array);
			}
			catch (CryptographicException)
			{
				return false;
			}
			using (FileStream fileStream2 = File.OpenWrite(fileName))
			{
				fileStream2.Position = (long)((ulong)strongNameSignature.SignaturePosition);
				fileStream2.Write(array, 0, array.Length);
			}
			return true;
		}

		// Token: 0x040001C2 RID: 450
		private RSA rsa;

		// Token: 0x040001C3 RID: 451
		private byte[] publicKey;

		// Token: 0x040001C4 RID: 452
		private byte[] keyToken;

		// Token: 0x040001C5 RID: 453
		private string tokenAlgorithm;

		// Token: 0x040001C6 RID: 454
		private static object lockObject = new object();

		// Token: 0x02000066 RID: 102
		internal class StrongNameSignature
		{
			// Token: 0x1700001F RID: 31
			// (get) Token: 0x0600015F RID: 351 RVA: 0x000088D0 File Offset: 0x00006AD0
			// (set) Token: 0x06000160 RID: 352 RVA: 0x000088D8 File Offset: 0x00006AD8
			public byte[] Hash
			{
				get
				{
					return this.hash;
				}
				set
				{
					this.hash = value;
				}
			}

			// Token: 0x17000020 RID: 32
			// (get) Token: 0x06000161 RID: 353 RVA: 0x000088E1 File Offset: 0x00006AE1
			// (set) Token: 0x06000162 RID: 354 RVA: 0x000088E9 File Offset: 0x00006AE9
			public byte[] Signature
			{
				get
				{
					return this.signature;
				}
				set
				{
					this.signature = value;
				}
			}

			// Token: 0x17000021 RID: 33
			// (set) Token: 0x06000163 RID: 355 RVA: 0x000088F2 File Offset: 0x00006AF2
			public uint MetadataPosition
			{
				set
				{
					this.metadataPosition = value;
				}
			}

			// Token: 0x17000022 RID: 34
			// (set) Token: 0x06000164 RID: 356 RVA: 0x000088FB File Offset: 0x00006AFB
			public uint MetadataLength
			{
				set
				{
					this.metadataLength = value;
				}
			}

			// Token: 0x17000023 RID: 35
			// (get) Token: 0x06000165 RID: 357 RVA: 0x00008904 File Offset: 0x00006B04
			// (set) Token: 0x06000166 RID: 358 RVA: 0x0000890C File Offset: 0x00006B0C
			public uint SignaturePosition
			{
				get
				{
					return this.signaturePosition;
				}
				set
				{
					this.signaturePosition = value;
				}
			}

			// Token: 0x17000024 RID: 36
			// (set) Token: 0x06000167 RID: 359 RVA: 0x00008915 File Offset: 0x00006B15
			public uint SignatureLength
			{
				set
				{
					this.signatureLength = value;
				}
			}

			// Token: 0x040001C7 RID: 455
			private byte[] hash;

			// Token: 0x040001C8 RID: 456
			private byte[] signature;

			// Token: 0x040001C9 RID: 457
			private uint signaturePosition;

			// Token: 0x040001CA RID: 458
			private uint signatureLength;

			// Token: 0x040001CB RID: 459
			private uint metadataPosition;

			// Token: 0x040001CC RID: 460
			private uint metadataLength;
		}

		// Token: 0x02000067 RID: 103
		internal enum StrongNameOptions
		{
			// Token: 0x040001CE RID: 462
			Metadata,
			// Token: 0x040001CF RID: 463
			Signature
		}
	}
}
