using System;
using ICSharpCode.SharpZipLib.Checksum;

namespace ICSharpCode.SharpZipLib.Encryption
{
	// Token: 0x0200009B RID: 155
	internal class PkzipClassicCryptoBase
	{
		// Token: 0x06000504 RID: 1284 RVA: 0x00019056 File Offset: 0x00017256
		protected byte TransformByte()
		{
			uint num = (this.keys[2] & 65535U) | 2U;
			return (byte)(num * (num ^ 1U) >> 8);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00019070 File Offset: 0x00017270
		protected void SetKeys(byte[] keyData)
		{
			if (keyData == null)
			{
				throw new ArgumentNullException("keyData");
			}
			if (keyData.Length != 12)
			{
				throw new InvalidOperationException("Key length is not valid");
			}
			this.keys = new uint[3];
			this.keys[0] = (uint)(((int)keyData[3] << 24) | ((int)keyData[2] << 16) | ((int)keyData[1] << 8) | (int)keyData[0]);
			this.keys[1] = (uint)(((int)keyData[7] << 24) | ((int)keyData[6] << 16) | ((int)keyData[5] << 8) | (int)keyData[4]);
			this.keys[2] = (uint)(((int)keyData[11] << 24) | ((int)keyData[10] << 16) | ((int)keyData[9] << 8) | (int)keyData[8]);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0001910C File Offset: 0x0001730C
		protected void UpdateKeys(byte ch)
		{
			this.keys[0] = Crc32.ComputeCrc32(this.keys[0], ch);
			this.keys[1] = this.keys[1] + (uint)((byte)this.keys[0]);
			this.keys[1] = this.keys[1] * 134775813U + 1U;
			this.keys[2] = Crc32.ComputeCrc32(this.keys[2], (byte)(this.keys[1] >> 24));
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00019182 File Offset: 0x00017382
		protected void Reset()
		{
			this.keys[0] = 0U;
			this.keys[1] = 0U;
			this.keys[2] = 0U;
		}

		// Token: 0x040003EF RID: 1007
		private uint[] keys;
	}
}
