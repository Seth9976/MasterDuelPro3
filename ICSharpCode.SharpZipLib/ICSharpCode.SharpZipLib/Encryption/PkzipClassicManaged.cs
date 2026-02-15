using System;
using System.Security.Cryptography;

namespace ICSharpCode.SharpZipLib.Encryption
{
	// Token: 0x0200009E RID: 158
	public sealed class PkzipClassicManaged : PkzipClassic
	{
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x00019276 File Offset: 0x00017476
		// (set) Token: 0x0600051A RID: 1306 RVA: 0x00019279 File Offset: 0x00017479
		public override int BlockSize
		{
			get
			{
				return 8;
			}
			set
			{
				if (value != 8)
				{
					throw new CryptographicException("Block size is invalid");
				}
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x0001928A File Offset: 0x0001748A
		public override KeySizes[] LegalKeySizes
		{
			get
			{
				return new KeySizes[]
				{
					new KeySizes(96, 96, 0)
				};
			}
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00008444 File Offset: 0x00006644
		public override void GenerateIV()
		{
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x0001929F File Offset: 0x0001749F
		public override KeySizes[] LegalBlockSizes
		{
			get
			{
				return new KeySizes[]
				{
					new KeySizes(8, 8, 0)
				};
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x000192B2 File Offset: 0x000174B2
		// (set) Token: 0x0600051F RID: 1311 RVA: 0x000192D2 File Offset: 0x000174D2
		public override byte[] Key
		{
			get
			{
				if (this.key_ == null)
				{
					this.GenerateKey();
				}
				return (byte[])this.key_.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length != 12)
				{
					throw new CryptographicException("Key size is illegal");
				}
				this.key_ = (byte[])value.Clone();
			}
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00019308 File Offset: 0x00017508
		public override void GenerateKey()
		{
			this.key_ = new byte[12];
			using (RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create())
			{
				randomNumberGenerator.GetBytes(this.key_);
			}
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00019350 File Offset: 0x00017550
		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
		{
			this.key_ = rgbKey;
			return new PkzipClassicEncryptCryptoTransform(this.Key);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00019364 File Offset: 0x00017564
		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
		{
			this.key_ = rgbKey;
			return new PkzipClassicDecryptCryptoTransform(this.Key);
		}

		// Token: 0x040003F0 RID: 1008
		private byte[] key_;
	}
}
