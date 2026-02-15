using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the base class from which all implementations of the <see cref="T:System.Security.Cryptography.RC2" /> algorithm must derive.</summary>
	// Token: 0x020003A0 RID: 928
	[ComVisible(true)]
	public abstract class RC2 : SymmetricAlgorithm
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Security.Cryptography.RC2" />.</summary>
		// Token: 0x06001FCE RID: 8142 RVA: 0x0007D919 File Offset: 0x0007BB19
		protected RC2()
		{
			this.KeySizeValue = 128;
			this.BlockSizeValue = 64;
			this.FeedbackSizeValue = this.BlockSizeValue;
			this.LegalBlockSizesValue = RC2.s_legalBlockSizes;
			this.LegalKeySizesValue = RC2.s_legalKeySizes;
		}

		/// <summary>Gets or sets the effective size of the secret key used by the <see cref="T:System.Security.Cryptography.RC2" /> algorithm in bits.</summary>
		/// <returns>The effective key size used by the <see cref="T:System.Security.Cryptography.RC2" /> algorithm.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The effective key size is invalid. </exception>
		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06001FCF RID: 8143 RVA: 0x0007D956 File Offset: 0x0007BB56
		public virtual int EffectiveKeySize
		{
			get
			{
				if (this.EffectiveKeySizeValue == 0)
				{
					return this.KeySizeValue;
				}
				return this.EffectiveKeySizeValue;
			}
		}

		/// <summary>Gets or sets the size of the secret key used by the <see cref="T:System.Security.Cryptography.RC2" /> algorithm in bits.</summary>
		/// <returns>The size of the secret key used by the <see cref="T:System.Security.Cryptography.RC2" /> algorithm.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The value for the RC2 key size is less than the effective key size value.</exception>
		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06001FD0 RID: 8144 RVA: 0x0007D96D File Offset: 0x0007BB6D
		// (set) Token: 0x06001FD1 RID: 8145 RVA: 0x0007D975 File Offset: 0x0007BB75
		public override int KeySize
		{
			get
			{
				return this.KeySizeValue;
			}
			set
			{
				if (value < this.EffectiveKeySizeValue)
				{
					throw new CryptographicException(Environment.GetResourceString("EffectiveKeySize value must be at least as large as the KeySize value."));
				}
				base.KeySize = value;
			}
		}

		/// <summary>Represents the effective size of the secret key used by the <see cref="T:System.Security.Cryptography.RC2" /> algorithm in bits.</summary>
		// Token: 0x04000EEA RID: 3818
		protected int EffectiveKeySizeValue;

		// Token: 0x04000EEB RID: 3819
		private static KeySizes[] s_legalBlockSizes = new KeySizes[]
		{
			new KeySizes(64, 64, 0)
		};

		// Token: 0x04000EEC RID: 3820
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(40, 1024, 8)
		};
	}
}
