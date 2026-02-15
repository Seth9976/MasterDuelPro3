using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the base class from which all implementations of the <see cref="T:System.Security.Cryptography.Rijndael" /> symmetric encryption algorithm must inherit.</summary>
	// Token: 0x020003A2 RID: 930
	[ComVisible(true)]
	public abstract class Rijndael : SymmetricAlgorithm
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Security.Cryptography.Rijndael" />.</summary>
		// Token: 0x06001FDA RID: 8154 RVA: 0x0007DAA3 File Offset: 0x0007BCA3
		protected Rijndael()
		{
			this.KeySizeValue = 256;
			this.BlockSizeValue = 128;
			this.FeedbackSizeValue = this.BlockSizeValue;
			this.LegalBlockSizesValue = Rijndael.s_legalBlockSizes;
			this.LegalKeySizesValue = Rijndael.s_legalKeySizes;
		}

		// Token: 0x04000EEF RID: 3823
		private static KeySizes[] s_legalBlockSizes = new KeySizes[]
		{
			new KeySizes(128, 256, 64)
		};

		// Token: 0x04000EF0 RID: 3824
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(128, 256, 64)
		};
	}
}
