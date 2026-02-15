using System;
using System.Runtime.CompilerServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the abstract base class from which all implementations of the Advanced Encryption Standard (AES) must inherit. </summary>
	// Token: 0x0200037E RID: 894
	[TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
	public abstract class Aes : SymmetricAlgorithm
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Aes" /> class. </summary>
		// Token: 0x06001F29 RID: 7977 RVA: 0x0007BFC4 File Offset: 0x0007A1C4
		protected Aes()
		{
			this.LegalBlockSizesValue = Aes.s_legalBlockSizes;
			this.LegalKeySizesValue = Aes.s_legalKeySizes;
			this.BlockSizeValue = 128;
			this.FeedbackSizeValue = 8;
			this.KeySizeValue = 256;
			this.ModeValue = CipherMode.CBC;
		}

		/// <summary>Creates a cryptographic object that is used to perform the symmetric algorithm.</summary>
		/// <returns>A cryptographic object that is used to perform the symmetric algorithm.</returns>
		// Token: 0x06001F2A RID: 7978 RVA: 0x0007C011 File Offset: 0x0007A211
		public static Aes Create()
		{
			return Aes.Create("AES");
		}

		/// <summary>Creates a cryptographic object that specifies the implementation of AES to use to perform the symmetric algorithm.</summary>
		/// <returns>A cryptographic object that is used to perform the symmetric algorithm.</returns>
		/// <param name="algorithmName">The name of the specific implementation of AES to use.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="algorithmName" /> parameter is null.</exception>
		// Token: 0x06001F2B RID: 7979 RVA: 0x0007C01D File Offset: 0x0007A21D
		public new static Aes Create(string algorithmName)
		{
			if (algorithmName == null)
			{
				throw new ArgumentNullException("algorithmName");
			}
			return CryptoConfig.CreateFromName(algorithmName) as Aes;
		}

		// Token: 0x04000E98 RID: 3736
		private static KeySizes[] s_legalBlockSizes = new KeySizes[]
		{
			new KeySizes(128, 128, 0)
		};

		// Token: 0x04000E99 RID: 3737
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(128, 256, 64)
		};
	}
}
