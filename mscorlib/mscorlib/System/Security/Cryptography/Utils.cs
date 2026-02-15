using System;

namespace System.Security.Cryptography
{
	// Token: 0x020003BD RID: 957
	internal static class Utils
	{
		// Token: 0x17000399 RID: 921
		// (get) Token: 0x0600209B RID: 8347 RVA: 0x00084A03 File Offset: 0x00082C03
		internal static RNGCryptoServiceProvider StaticRandomNumberGenerator
		{
			get
			{
				if (Utils._rng == null)
				{
					Utils._rng = new RNGCryptoServiceProvider();
				}
				return Utils._rng;
			}
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x00084A24 File Offset: 0x00082C24
		internal static byte[] GenerateRandom(int keySize)
		{
			byte[] array = new byte[keySize];
			Utils.StaticRandomNumberGenerator.GetBytes(array);
			return array;
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x0000C091 File Offset: 0x0000A291
		internal static bool HasAlgorithm(int dwCalg, int dwKeySize)
		{
			return true;
		}

		// Token: 0x0600209E RID: 8350 RVA: 0x00084A44 File Offset: 0x00082C44
		internal static string DiscardWhiteSpaces(string inputBuffer)
		{
			return Utils.DiscardWhiteSpaces(inputBuffer, 0, inputBuffer.Length);
		}

		// Token: 0x0600209F RID: 8351 RVA: 0x00084A54 File Offset: 0x00082C54
		internal static string DiscardWhiteSpaces(string inputBuffer, int inputOffset, int inputCount)
		{
			int num = 0;
			for (int i = 0; i < inputCount; i++)
			{
				if (char.IsWhiteSpace(inputBuffer[inputOffset + i]))
				{
					num++;
				}
			}
			char[] array = new char[inputCount - num];
			num = 0;
			for (int i = 0; i < inputCount; i++)
			{
				if (!char.IsWhiteSpace(inputBuffer[inputOffset + i]))
				{
					array[num++] = inputBuffer[inputOffset + i];
				}
			}
			return new string(array);
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x00084AC0 File Offset: 0x00082CC0
		internal static int ConvertByteArrayToInt(byte[] input)
		{
			int num = 0;
			for (int i = 0; i < input.Length; i++)
			{
				num *= 256;
				num += (int)input[i];
			}
			return num;
		}

		// Token: 0x060020A1 RID: 8353 RVA: 0x00084AEC File Offset: 0x00082CEC
		internal static byte[] ConvertIntToByteArray(int dwInput)
		{
			byte[] array = new byte[8];
			int num = 0;
			if (dwInput == 0)
			{
				return new byte[1];
			}
			int i = dwInput;
			while (i > 0)
			{
				int num2 = i % 256;
				array[num] = (byte)num2;
				i = (i - num2) / 256;
				num++;
			}
			byte[] array2 = new byte[num];
			for (int j = 0; j < num; j++)
			{
				array2[j] = array[num - j - 1];
			}
			return array2;
		}

		// Token: 0x060020A2 RID: 8354 RVA: 0x00084B58 File Offset: 0x00082D58
		internal static byte[] FixupKeyParity(byte[] key)
		{
			byte[] array = new byte[key.Length];
			for (int i = 0; i < key.Length; i++)
			{
				array[i] = key[i] & 254;
				byte b = (byte)((int)(array[i] & 15) ^ (array[i] >> 4));
				byte b2 = (byte)((int)(b & 3) ^ (b >> 2));
				if ((byte)((int)(b2 & 1) ^ (b2 >> 1)) == 0)
				{
					byte[] array2 = array;
					int num = i;
					array2[num] |= 1;
				}
			}
			return array;
		}

		// Token: 0x060020A3 RID: 8355 RVA: 0x00084BB8 File Offset: 0x00082DB8
		internal unsafe static void DWORDFromLittleEndian(uint* x, int digits, byte* block)
		{
			int i = 0;
			int num = 0;
			while (i < digits)
			{
				x[i] = (uint)((int)block[num] | ((int)block[num + 1] << 8) | ((int)block[num + 2] << 16) | ((int)block[num + 3] << 24));
				i++;
				num += 4;
			}
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x00084C00 File Offset: 0x00082E00
		internal static void DWORDToLittleEndian(byte[] block, uint[] x, int digits)
		{
			int i = 0;
			int num = 0;
			while (i < digits)
			{
				block[num] = (byte)(x[i] & 255U);
				block[num + 1] = (byte)((x[i] >> 8) & 255U);
				block[num + 2] = (byte)((x[i] >> 16) & 255U);
				block[num + 3] = (byte)((x[i] >> 24) & 255U);
				i++;
				num += 4;
			}
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x00084C64 File Offset: 0x00082E64
		internal unsafe static void DWORDFromBigEndian(uint* x, int digits, byte* block)
		{
			int i = 0;
			int num = 0;
			while (i < digits)
			{
				x[i] = (uint)(((int)block[num] << 24) | ((int)block[num + 1] << 16) | ((int)block[num + 2] << 8) | (int)block[num + 3]);
				i++;
				num += 4;
			}
		}

		// Token: 0x060020A6 RID: 8358 RVA: 0x00084CAC File Offset: 0x00082EAC
		internal static void DWORDToBigEndian(byte[] block, uint[] x, int digits)
		{
			int i = 0;
			int num = 0;
			while (i < digits)
			{
				block[num] = (byte)((x[i] >> 24) & 255U);
				block[num + 1] = (byte)((x[i] >> 16) & 255U);
				block[num + 2] = (byte)((x[i] >> 8) & 255U);
				block[num + 3] = (byte)(x[i] & 255U);
				i++;
				num += 4;
			}
		}

		// Token: 0x060020A7 RID: 8359 RVA: 0x00084D10 File Offset: 0x00082F10
		internal unsafe static void QuadWordFromBigEndian(ulong* x, int digits, byte* block)
		{
			int i = 0;
			int num = 0;
			while (i < digits)
			{
				x[i] = ((ulong)block[num] << 56) | ((ulong)block[num + 1] << 48) | ((ulong)block[num + 2] << 40) | ((ulong)block[num + 3] << 32) | ((ulong)block[num + 4] << 24) | ((ulong)block[num + 5] << 16) | ((ulong)block[num + 6] << 8) | (ulong)block[num + 7];
				i++;
				num += 8;
			}
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x00084D88 File Offset: 0x00082F88
		internal static void QuadWordToBigEndian(byte[] block, ulong[] x, int digits)
		{
			int i = 0;
			int num = 0;
			while (i < digits)
			{
				block[num] = (byte)((x[i] >> 56) & 255UL);
				block[num + 1] = (byte)((x[i] >> 48) & 255UL);
				block[num + 2] = (byte)((x[i] >> 40) & 255UL);
				block[num + 3] = (byte)((x[i] >> 32) & 255UL);
				block[num + 4] = (byte)((x[i] >> 24) & 255UL);
				block[num + 5] = (byte)((x[i] >> 16) & 255UL);
				block[num + 6] = (byte)((x[i] >> 8) & 255UL);
				block[num + 7] = (byte)(x[i] & 255UL);
				i++;
				num += 8;
			}
		}

		// Token: 0x060020A9 RID: 8361 RVA: 0x00084E3F File Offset: 0x0008303F
		internal static bool _ProduceLegacyHmacValues()
		{
			return Environment.GetEnvironmentVariable("legacyHMACMode") == "1";
		}

		// Token: 0x04000F42 RID: 3906
		private static volatile RNGCryptoServiceProvider _rng;
	}
}
