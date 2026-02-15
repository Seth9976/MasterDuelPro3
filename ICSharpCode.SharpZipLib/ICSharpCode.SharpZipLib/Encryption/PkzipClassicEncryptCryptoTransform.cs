using System;
using System.Security.Cryptography;

namespace ICSharpCode.SharpZipLib.Encryption
{
	// Token: 0x0200009C RID: 156
	internal class PkzipClassicEncryptCryptoTransform : PkzipClassicCryptoBase, ICryptoTransform, IDisposable
	{
		// Token: 0x06000509 RID: 1289 RVA: 0x0001919F File Offset: 0x0001739F
		internal PkzipClassicEncryptCryptoTransform(byte[] keyBlock)
		{
			base.SetKeys(keyBlock);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x000191B0 File Offset: 0x000173B0
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			byte[] array = new byte[inputCount];
			this.TransformBlock(inputBuffer, inputOffset, inputCount, array, 0);
			return array;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x000191D4 File Offset: 0x000173D4
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			for (int i = inputOffset; i < inputOffset + inputCount; i++)
			{
				byte b = inputBuffer[i];
				outputBuffer[outputOffset++] = inputBuffer[i] ^ base.TransformByte();
				base.UpdateKeys(b);
			}
			return inputCount;
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x0000869D File Offset: 0x0000689D
		public bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x0000869D File Offset: 0x0000689D
		public int InputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x0000869D File Offset: 0x0000689D
		public int OutputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x0000869D File Offset: 0x0000689D
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00019210 File Offset: 0x00017410
		public void Dispose()
		{
			base.Reset();
		}
	}
}
