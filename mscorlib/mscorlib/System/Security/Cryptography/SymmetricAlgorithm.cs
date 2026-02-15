using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the abstract base class from which all implementations of symmetric algorithms must inherit.</summary>
	// Token: 0x020003BA RID: 954
	[ComVisible(true)]
	public abstract class SymmetricAlgorithm : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SymmetricAlgorithm" /> class.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The implementation of the class derived from the symmetric algorithm is not valid.</exception>
		// Token: 0x06002072 RID: 8306 RVA: 0x000843C9 File Offset: 0x000825C9
		protected SymmetricAlgorithm()
		{
			this.ModeValue = CipherMode.CBC;
			this.PaddingValue = PaddingMode.PKCS7;
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Security.Cryptography.SymmetricAlgorithm" /> class.</summary>
		// Token: 0x06002073 RID: 8307 RVA: 0x000843DF File Offset: 0x000825DF
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Security.Cryptography.SymmetricAlgorithm" /> class.</summary>
		// Token: 0x06002074 RID: 8308 RVA: 0x0007BD5C File Offset: 0x00079F5C
		public void Clear()
		{
			((IDisposable)this).Dispose();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.SymmetricAlgorithm" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06002075 RID: 8309 RVA: 0x000843F0 File Offset: 0x000825F0
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.KeyValue != null)
				{
					Array.Clear(this.KeyValue, 0, this.KeyValue.Length);
					this.KeyValue = null;
				}
				if (this.IVValue != null)
				{
					Array.Clear(this.IVValue, 0, this.IVValue.Length);
					this.IVValue = null;
				}
			}
		}

		/// <summary>Gets or sets the block size, in bits, of the cryptographic operation.</summary>
		/// <returns>The block size, in bits.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The block size is invalid. </exception>
		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06002076 RID: 8310 RVA: 0x00084446 File Offset: 0x00082646
		// (set) Token: 0x06002077 RID: 8311 RVA: 0x00084450 File Offset: 0x00082650
		public virtual int BlockSize
		{
			get
			{
				return this.BlockSizeValue;
			}
			set
			{
				for (int i = 0; i < this.LegalBlockSizesValue.Length; i++)
				{
					if (this.LegalBlockSizesValue[i].SkipSize == 0)
					{
						if (this.LegalBlockSizesValue[i].MinSize == value)
						{
							this.BlockSizeValue = value;
							this.IVValue = null;
							return;
						}
					}
					else
					{
						for (int j = this.LegalBlockSizesValue[i].MinSize; j <= this.LegalBlockSizesValue[i].MaxSize; j += this.LegalBlockSizesValue[i].SkipSize)
						{
							if (j == value)
							{
								if (this.BlockSizeValue != value)
								{
									this.BlockSizeValue = value;
									this.IVValue = null;
								}
								return;
							}
						}
					}
				}
				throw new CryptographicException(Environment.GetResourceString("Specified block size is not valid for this algorithm."));
			}
		}

		/// <summary>Gets or sets the feedback size, in bits, of the cryptographic operation.</summary>
		/// <returns>The feedback size in bits.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The feedback size is larger than the block size. </exception>
		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06002078 RID: 8312 RVA: 0x000844FC File Offset: 0x000826FC
		public virtual int FeedbackSize
		{
			get
			{
				return this.FeedbackSizeValue;
			}
		}

		/// <summary>Gets or sets the initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />) for the symmetric algorithm.</summary>
		/// <returns>The initialization vector.</returns>
		/// <exception cref="T:System.ArgumentNullException">An attempt was made to set the initialization vector to null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An attempt was made to set the initialization vector to an invalid size. </exception>
		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06002079 RID: 8313 RVA: 0x00084504 File Offset: 0x00082704
		// (set) Token: 0x0600207A RID: 8314 RVA: 0x00084524 File Offset: 0x00082724
		public virtual byte[] IV
		{
			get
			{
				if (this.IVValue == null)
				{
					this.GenerateIV();
				}
				return (byte[])this.IVValue.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length != this.BlockSizeValue / 8)
				{
					throw new CryptographicException(Environment.GetResourceString("Specified initialization vector (IV) does not match the block size for this algorithm."));
				}
				this.IVValue = (byte[])value.Clone();
			}
		}

		/// <summary>Gets or sets the secret key for the symmetric algorithm.</summary>
		/// <returns>The secret key to use for the symmetric algorithm.</returns>
		/// <exception cref="T:System.ArgumentNullException">An attempt was made to set the key to null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key size is invalid.</exception>
		// Token: 0x17000392 RID: 914
		// (get) Token: 0x0600207B RID: 8315 RVA: 0x00084562 File Offset: 0x00082762
		// (set) Token: 0x0600207C RID: 8316 RVA: 0x00084584 File Offset: 0x00082784
		public virtual byte[] Key
		{
			get
			{
				if (this.KeyValue == null)
				{
					this.GenerateKey();
				}
				return (byte[])this.KeyValue.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!this.ValidKeySize(value.Length * 8))
				{
					throw new CryptographicException(Environment.GetResourceString("Specified key is not a valid size for this algorithm."));
				}
				this.KeyValue = (byte[])value.Clone();
				this.KeySizeValue = value.Length * 8;
			}
		}

		/// <summary>Gets the block sizes, in bits, that are supported by the symmetric algorithm.</summary>
		/// <returns>An array that contains the block sizes supported by the algorithm.</returns>
		// Token: 0x17000393 RID: 915
		// (get) Token: 0x0600207D RID: 8317 RVA: 0x000845D8 File Offset: 0x000827D8
		public virtual KeySizes[] LegalBlockSizes
		{
			get
			{
				return (KeySizes[])this.LegalBlockSizesValue.Clone();
			}
		}

		/// <summary>Gets the key sizes, in bits, that are supported by the symmetric algorithm.</summary>
		/// <returns>An array that contains the key sizes supported by the algorithm.</returns>
		// Token: 0x17000394 RID: 916
		// (get) Token: 0x0600207E RID: 8318 RVA: 0x000845EA File Offset: 0x000827EA
		public virtual KeySizes[] LegalKeySizes
		{
			get
			{
				return (KeySizes[])this.LegalKeySizesValue.Clone();
			}
		}

		/// <summary>Gets or sets the size, in bits, of the secret key used by the symmetric algorithm.</summary>
		/// <returns>The size, in bits, of the secret key used by the symmetric algorithm.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key size is not valid. </exception>
		// Token: 0x17000395 RID: 917
		// (get) Token: 0x0600207F RID: 8319 RVA: 0x0007D96D File Offset: 0x0007BB6D
		// (set) Token: 0x06002080 RID: 8320 RVA: 0x000845FC File Offset: 0x000827FC
		public virtual int KeySize
		{
			get
			{
				return this.KeySizeValue;
			}
			set
			{
				if (!this.ValidKeySize(value))
				{
					throw new CryptographicException(Environment.GetResourceString("Specified key is not a valid size for this algorithm."));
				}
				this.KeySizeValue = value;
				this.KeyValue = null;
			}
		}

		/// <summary>Gets or sets the mode for operation of the symmetric algorithm.</summary>
		/// <returns>The mode for operation of the symmetric algorithm. The default is <see cref="F:System.Security.Cryptography.CipherMode.CBC" />.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cipher mode is not one of the <see cref="T:System.Security.Cryptography.CipherMode" /> values. </exception>
		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06002081 RID: 8321 RVA: 0x00084625 File Offset: 0x00082825
		// (set) Token: 0x06002082 RID: 8322 RVA: 0x0008462D File Offset: 0x0008282D
		public virtual CipherMode Mode
		{
			get
			{
				return this.ModeValue;
			}
			set
			{
				if (value < CipherMode.CBC || CipherMode.CFB < value)
				{
					throw new CryptographicException(Environment.GetResourceString("Specified cipher mode is not valid for this algorithm."));
				}
				this.ModeValue = value;
			}
		}

		/// <summary>Gets or sets the padding mode used in the symmetric algorithm.</summary>
		/// <returns>The padding mode used in the symmetric algorithm. The default is <see cref="F:System.Security.Cryptography.PaddingMode.PKCS7" />.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The padding mode is not one of the <see cref="T:System.Security.Cryptography.PaddingMode" /> values. </exception>
		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06002083 RID: 8323 RVA: 0x0008464E File Offset: 0x0008284E
		// (set) Token: 0x06002084 RID: 8324 RVA: 0x00084656 File Offset: 0x00082856
		public virtual PaddingMode Padding
		{
			get
			{
				return this.PaddingValue;
			}
			set
			{
				if (value < PaddingMode.None || PaddingMode.ISO10126 < value)
				{
					throw new CryptographicException(Environment.GetResourceString("Specified padding mode is not valid for this algorithm."));
				}
				this.PaddingValue = value;
			}
		}

		/// <summary>Determines whether the specified key size is valid for the current algorithm.</summary>
		/// <returns>true if the specified key size is valid for the current algorithm; otherwise, false.</returns>
		/// <param name="bitLength">The length, in bits, to check for a valid key size. </param>
		// Token: 0x06002085 RID: 8325 RVA: 0x00084678 File Offset: 0x00082878
		public bool ValidKeySize(int bitLength)
		{
			KeySizes[] legalKeySizes = this.LegalKeySizes;
			if (legalKeySizes == null)
			{
				return false;
			}
			for (int i = 0; i < legalKeySizes.Length; i++)
			{
				if (legalKeySizes[i].SkipSize == 0)
				{
					if (legalKeySizes[i].MinSize == bitLength)
					{
						return true;
					}
				}
				else
				{
					for (int j = legalKeySizes[i].MinSize; j <= legalKeySizes[i].MaxSize; j += legalKeySizes[i].SkipSize)
					{
						if (j == bitLength)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		/// <summary>Creates the specified cryptographic object used to perform the symmetric algorithm.</summary>
		/// <returns>A cryptographic object used to perform the symmetric algorithm.</returns>
		/// <param name="algName">The name of the specific implementation of the <see cref="T:System.Security.Cryptography.SymmetricAlgorithm" /> class to use. </param>
		// Token: 0x06002086 RID: 8326 RVA: 0x000846DE File Offset: 0x000828DE
		public static SymmetricAlgorithm Create(string algName)
		{
			return (SymmetricAlgorithm)CryptoConfig.CreateFromName(algName);
		}

		/// <summary>Creates a symmetric encryptor object with the current <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" /> property and initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).</summary>
		/// <returns>A symmetric encryptor object.</returns>
		// Token: 0x06002087 RID: 8327 RVA: 0x000846EB File Offset: 0x000828EB
		public virtual ICryptoTransform CreateEncryptor()
		{
			return this.CreateEncryptor(this.Key, this.IV);
		}

		/// <summary>When overridden in a derived class, creates a symmetric encryptor object with the specified <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" /> property and initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).</summary>
		/// <returns>A symmetric encryptor object.</returns>
		/// <param name="rgbKey">The secret key to use for the symmetric algorithm. </param>
		/// <param name="rgbIV">The initialization vector to use for the symmetric algorithm. </param>
		// Token: 0x06002088 RID: 8328
		public abstract ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV);

		/// <summary>Creates a symmetric decryptor object with the current <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" /> property and initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).</summary>
		/// <returns>A symmetric decryptor object.</returns>
		// Token: 0x06002089 RID: 8329 RVA: 0x000846FF File Offset: 0x000828FF
		public virtual ICryptoTransform CreateDecryptor()
		{
			return this.CreateDecryptor(this.Key, this.IV);
		}

		/// <summary>When overridden in a derived class, creates a symmetric decryptor object with the specified <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" /> property and initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).</summary>
		/// <returns>A symmetric decryptor object.</returns>
		/// <param name="rgbKey">The secret key to use for the symmetric algorithm. </param>
		/// <param name="rgbIV">The initialization vector to use for the symmetric algorithm. </param>
		// Token: 0x0600208A RID: 8330
		public abstract ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV);

		/// <summary>When overridden in a derived class, generates a random key (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" />) to use for the algorithm. </summary>
		// Token: 0x0600208B RID: 8331
		public abstract void GenerateKey();

		/// <summary>When overridden in a derived class, generates a random initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />) to use for the algorithm.</summary>
		// Token: 0x0600208C RID: 8332
		public abstract void GenerateIV();

		/// <summary>Represents the block size, in bits, of the cryptographic operation.</summary>
		// Token: 0x04000F37 RID: 3895
		protected int BlockSizeValue;

		/// <summary>Represents the feedback size, in bits, of the cryptographic operation.</summary>
		// Token: 0x04000F38 RID: 3896
		protected int FeedbackSizeValue;

		/// <summary>Represents the initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />) for the symmetric algorithm.</summary>
		// Token: 0x04000F39 RID: 3897
		protected byte[] IVValue;

		/// <summary>Represents the secret key for the symmetric algorithm.</summary>
		// Token: 0x04000F3A RID: 3898
		protected byte[] KeyValue;

		/// <summary>Specifies the block sizes, in bits, that are supported by the symmetric algorithm.</summary>
		// Token: 0x04000F3B RID: 3899
		protected KeySizes[] LegalBlockSizesValue;

		/// <summary>Specifies the key sizes, in bits, that are supported by the symmetric algorithm.</summary>
		// Token: 0x04000F3C RID: 3900
		protected KeySizes[] LegalKeySizesValue;

		/// <summary>Represents the size, in bits, of the secret key used by the symmetric algorithm.</summary>
		// Token: 0x04000F3D RID: 3901
		protected int KeySizeValue;

		/// <summary>Represents the cipher mode used in the symmetric algorithm.</summary>
		// Token: 0x04000F3E RID: 3902
		protected CipherMode ModeValue;

		/// <summary>Represents the padding mode used in the symmetric algorithm.</summary>
		// Token: 0x04000F3F RID: 3903
		protected PaddingMode PaddingValue;
	}
}
