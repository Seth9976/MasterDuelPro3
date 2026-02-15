using System;
using System.Security.Cryptography;

namespace ICSharpCode.SharpZipLib.Encryption
{
	// Token: 0x0200009D RID: 157
	internal class PkzipClassicDecryptCryptoTransform : PkzipClassicCryptoBase, ICryptoTransform, IDisposable
	{
		// Token: 0x06000511 RID: 1297 RVA: 0x0001919F File Offset: 0x0001739F
		internal PkzipClassicDecryptCryptoTransform(byte[] keyBlock)
		{
			base.SetKeys(keyBlock);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00019218 File Offset: 0x00017418
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			byte[] array = new byte[inputCount];
			this.TransformBlock(inputBuffer, inputOffset, inputCount, array, 0);
			return array;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0001923C File Offset: 0x0001743C
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			for (int i = inputOffset; i < inputOffset + inputCount; i++)
			{
				byte b = inputBuffer[i] ^ base.TransformByte();
				outputBuffer[outputOffset++] = b;
				base.UpdateKeys(b);
			}
			return inputCount;
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x0000869D File Offset: 0x0000689D
		public bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x0000869D File Offset: 0x0000689D
		public int InputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x0000869D File Offset: 0x0000689D
		public int OutputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x0000869D File Offset: 0x0000689D
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00019210 File Offset: 0x00017410
		public void Dispose()
		{
			base.Reset();
		}
	}
}
