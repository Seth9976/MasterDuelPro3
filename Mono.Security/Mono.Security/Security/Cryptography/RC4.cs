using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000050 RID: 80
	public abstract class RC4 : SymmetricAlgorithm
	{
		// Token: 0x060001C7 RID: 455 RVA: 0x0000B0ED File Offset: 0x000092ED
		public RC4()
		{
			this.KeySizeValue = 128;
			this.BlockSizeValue = 64;
			this.FeedbackSizeValue = this.BlockSizeValue;
			this.LegalBlockSizesValue = RC4.s_legalBlockSizes;
			this.LegalKeySizesValue = RC4.s_legalKeySizes;
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000B12A File Offset: 0x0000932A
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x00002945 File Offset: 0x00000B45
		public override byte[] IV
		{
			get
			{
				return new byte[0];
			}
			set
			{
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000B132 File Offset: 0x00009332
		public static RC4 Create()
		{
			return RC4.Create("RC4");
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000B140 File Offset: 0x00009340
		public new static RC4 Create(string algName)
		{
			object obj = CryptoConfig.CreateFromName(algName);
			if (obj == null)
			{
				obj = new ARC4Managed();
			}
			return (RC4)obj;
		}

		// Token: 0x04000211 RID: 529
		private static KeySizes[] s_legalBlockSizes = new KeySizes[]
		{
			new KeySizes(64, 64, 0)
		};

		// Token: 0x04000212 RID: 530
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(40, 2048, 8)
		};
	}
}
