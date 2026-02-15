using System;
using System.Security.Cryptography;
using ICSharpCode.SharpZipLib.Zip;

namespace ICSharpCode.SharpZipLib.Encryption
{
	// Token: 0x020000A0 RID: 160
	internal class ZipAESTransform : ICryptoTransform, IDisposable
	{
		// Token: 0x0600052C RID: 1324 RVA: 0x00019664 File Offset: 0x00017864
		public ZipAESTransform(string key, byte[] saltBytes, int blockSize, bool writeMode)
		{
			if (blockSize != 16 && blockSize != 32)
			{
				throw new Exception("Invalid blocksize " + blockSize.ToString() + ". Must be 16 or 32.");
			}
			if (saltBytes.Length != blockSize / 2)
			{
				throw new Exception("Invalid salt len. Must be " + (blockSize / 2).ToString() + " for blocksize " + blockSize.ToString());
			}
			this._blockSize = blockSize;
			this._encryptBuffer = new byte[this._blockSize];
			this._encrPos = 16;
			Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(key, saltBytes, 1000);
			Aes aes = Aes.Create();
			aes.Mode = CipherMode.ECB;
			this._counterNonce = new byte[this._blockSize];
			byte[] bytes = rfc2898DeriveBytes.GetBytes(this._blockSize);
			byte[] bytes2 = rfc2898DeriveBytes.GetBytes(this._blockSize);
			this._encryptor = aes.CreateEncryptor(bytes, new byte[16]);
			this._pwdVerifier = rfc2898DeriveBytes.GetBytes(2);
			this._hmacsha1 = IncrementalHash.CreateHMAC(HashAlgorithmName.SHA1, bytes2);
			this._writeMode = writeMode;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0001976C File Offset: 0x0001796C
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			if (!this._writeMode)
			{
				this._hmacsha1.AppendData(inputBuffer, inputOffset, inputCount);
			}
			for (int i = 0; i < inputCount; i++)
			{
				if (this._encrPos == 16)
				{
					int num = 0;
					for (;;)
					{
						byte[] counterNonce = this._counterNonce;
						int num2 = num;
						byte b = counterNonce[num2] + 1;
						counterNonce[num2] = b;
						if (b != 0)
						{
							break;
						}
						num++;
					}
					this._encryptor.TransformBlock(this._counterNonce, 0, this._blockSize, this._encryptBuffer, 0);
					this._encrPos = 0;
				}
				int num3 = i + outputOffset;
				byte b2 = inputBuffer[i + inputOffset];
				byte[] encryptBuffer = this._encryptBuffer;
				int encrPos = this._encrPos;
				this._encrPos = encrPos + 1;
				outputBuffer[num3] = b2 ^ encryptBuffer[encrPos];
			}
			if (this._writeMode)
			{
				this._hmacsha1.AppendData(outputBuffer, outputOffset, inputCount);
			}
			return inputCount;
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x0001982A File Offset: 0x00017A2A
		public byte[] PwdVerifier
		{
			get
			{
				return this._pwdVerifier;
			}
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00019834 File Offset: 0x00017A34
		public byte[] GetAuthCode()
		{
			byte[] array;
			if ((array = this._authCode) == null)
			{
				array = (this._authCode = this._hmacsha1.GetHashAndReset());
			}
			return array;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00019860 File Offset: 0x00017A60
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			byte[] array = Array.Empty<byte>();
			if (inputCount != 0)
			{
				if (inputCount > 10)
				{
					int num = inputCount - 10;
					array = new byte[num];
					this.TransformBlock(inputBuffer, inputOffset, num, array, 0);
				}
				else if (inputCount < 10)
				{
					throw new ZipException("Auth code missing from input stream");
				}
				this._authCode = this._hmacsha1.GetHashAndReset();
			}
			return array;
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x000198B7 File Offset: 0x00017AB7
		public int InputBlockSize
		{
			get
			{
				return this._blockSize;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x000198B7 File Offset: 0x00017AB7
		public int OutputBlockSize
		{
			get
			{
				return this._blockSize;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x0000869D File Offset: 0x0000689D
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x0000869D File Offset: 0x0000689D
		public bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x000198BF File Offset: 0x00017ABF
		public void Dispose()
		{
			this._encryptor.Dispose();
		}

		// Token: 0x040003FC RID: 1020
		private const int PWD_VER_LENGTH = 2;

		// Token: 0x040003FD RID: 1021
		private const int KEY_ROUNDS = 1000;

		// Token: 0x040003FE RID: 1022
		private const int ENCRYPT_BLOCK = 16;

		// Token: 0x040003FF RID: 1023
		private int _blockSize;

		// Token: 0x04000400 RID: 1024
		private readonly ICryptoTransform _encryptor;

		// Token: 0x04000401 RID: 1025
		private readonly byte[] _counterNonce;

		// Token: 0x04000402 RID: 1026
		private byte[] _encryptBuffer;

		// Token: 0x04000403 RID: 1027
		private int _encrPos;

		// Token: 0x04000404 RID: 1028
		private byte[] _pwdVerifier;

		// Token: 0x04000405 RID: 1029
		private IncrementalHash _hmacsha1;

		// Token: 0x04000406 RID: 1030
		private byte[] _authCode;

		// Token: 0x04000407 RID: 1031
		private bool _writeMode;
	}
}
