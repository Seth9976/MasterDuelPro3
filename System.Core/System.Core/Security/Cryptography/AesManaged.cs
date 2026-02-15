using System;

namespace System.Security.Cryptography
{
	/// <summary>Provides a managed implementation of the Advanced Encryption Standard (AES) symmetric algorithm. </summary>
	// Token: 0x02000014 RID: 20
	public sealed class AesManaged : Aes
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.AesManaged" /> class. </summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The Windows security policy setting for FIPS is enabled.</exception>
		/// <exception cref="T:System.InvalidOperationException">This implementation is not part of the Windows Platform FIPS-validated cryptographic algorithms.</exception>
		// Token: 0x0600002C RID: 44 RVA: 0x00002288 File Offset: 0x00000488
		public AesManaged()
		{
			if (CryptoConfig.AllowOnlyFipsAlgorithms)
			{
				throw new InvalidOperationException(global::SR.GetString("This implementation is not part of the Windows Platform FIPS validated cryptographic algorithms."));
			}
			this.m_rijndael = new RijndaelManaged();
			this.m_rijndael.BlockSize = this.BlockSize;
			this.m_rijndael.KeySize = this.KeySize;
		}

		/// <summary>Gets or sets the number of bits to use as feedback. </summary>
		/// <returns>The feedback size, in bits.</returns>
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000022DF File Offset: 0x000004DF
		public override int FeedbackSize
		{
			get
			{
				return this.m_rijndael.FeedbackSize;
			}
		}

		/// <summary>Gets or sets the initialization vector (IV) to use for the symmetric algorithm. </summary>
		/// <returns>The initialization vector to use for the symmetric algorithm</returns>
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000022EC File Offset: 0x000004EC
		// (set) Token: 0x0600002F RID: 47 RVA: 0x000022F9 File Offset: 0x000004F9
		public override byte[] IV
		{
			get
			{
				return this.m_rijndael.IV;
			}
			set
			{
				this.m_rijndael.IV = value;
			}
		}

		/// <summary>Gets or sets the secret key used for the symmetric algorithm.</summary>
		/// <returns>The key for the symmetric algorithm.</returns>
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002307 File Offset: 0x00000507
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002314 File Offset: 0x00000514
		public override byte[] Key
		{
			get
			{
				return this.m_rijndael.Key;
			}
			set
			{
				this.m_rijndael.Key = value;
			}
		}

		/// <summary>Gets or sets the size, in bits, of the secret key used for the symmetric algorithm. </summary>
		/// <returns>The size, in bits, of the key used by the symmetric algorithm.</returns>
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002322 File Offset: 0x00000522
		// (set) Token: 0x06000033 RID: 51 RVA: 0x0000232F File Offset: 0x0000052F
		public override int KeySize
		{
			get
			{
				return this.m_rijndael.KeySize;
			}
			set
			{
				this.m_rijndael.KeySize = value;
			}
		}

		/// <summary>Gets or sets the mode for operation of the symmetric algorithm.</summary>
		/// <returns>One of the enumeration values that specifies the block cipher mode to use for encryption. The default is <see cref="F:System.Security.Cryptography.CipherMode.CBC" />.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///   <see cref="P:System.Security.Cryptography.AesManaged.Mode" /> is set to <see cref="F:System.Security.Cryptography.CipherMode.CFB" /> or <see cref="F:System.Security.Cryptography.CipherMode.OFB" />.</exception>
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000034 RID: 52 RVA: 0x0000233D File Offset: 0x0000053D
		// (set) Token: 0x06000035 RID: 53 RVA: 0x0000234A File Offset: 0x0000054A
		public override CipherMode Mode
		{
			get
			{
				return this.m_rijndael.Mode;
			}
			set
			{
				if (value == CipherMode.CFB || value == CipherMode.OFB)
				{
					throw new CryptographicException(global::SR.GetString("Specified cipher mode is not valid for this algorithm."));
				}
				this.m_rijndael.Mode = value;
			}
		}

		/// <summary>Gets or sets the padding mode used in the symmetric algorithm. </summary>
		/// <returns>One of the enumeration values that specifies the type of padding to apply. The default is <see cref="F:System.Security.Cryptography.PaddingMode.PKCS7" />.</returns>
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002370 File Offset: 0x00000570
		// (set) Token: 0x06000037 RID: 55 RVA: 0x0000237D File Offset: 0x0000057D
		public override PaddingMode Padding
		{
			get
			{
				return this.m_rijndael.Padding;
			}
			set
			{
				this.m_rijndael.Padding = value;
			}
		}

		/// <summary>Creates a symmetric decryptor object using the current key and initialization vector (IV).</summary>
		/// <returns>A symmetric decryptor object.</returns>
		// Token: 0x06000038 RID: 56 RVA: 0x0000238B File Offset: 0x0000058B
		public override ICryptoTransform CreateDecryptor()
		{
			return this.m_rijndael.CreateDecryptor();
		}

		/// <summary>Creates a symmetric decryptor object using the specified key and initialization vector (IV).</summary>
		/// <returns>A symmetric decryptor object.</returns>
		/// <param name="key">The secret key to use for the symmetric algorithm.</param>
		/// <param name="iv">The initialization vector to use for the symmetric algorithm.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> or <paramref name="iv" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is invalid.</exception>
		// Token: 0x06000039 RID: 57 RVA: 0x00002398 File Offset: 0x00000598
		public override ICryptoTransform CreateDecryptor(byte[] key, byte[] iv)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (!base.ValidKeySize(key.Length * 8))
			{
				throw new ArgumentException(global::SR.GetString("Specified key is not a valid size for this algorithm."), "key");
			}
			if (iv != null && iv.Length * 8 != this.BlockSizeValue)
			{
				throw new ArgumentException(global::SR.GetString("Specified initialization vector (IV) does not match the block size for this algorithm."), "iv");
			}
			return this.m_rijndael.CreateDecryptor(key, iv);
		}

		/// <summary>Creates a symmetric encryptor object using the current key and initialization vector (IV).</summary>
		/// <returns>A symmetric encryptor object.</returns>
		// Token: 0x0600003A RID: 58 RVA: 0x00002407 File Offset: 0x00000607
		public override ICryptoTransform CreateEncryptor()
		{
			return this.m_rijndael.CreateEncryptor();
		}

		/// <summary>Creates a symmetric encryptor object using the specified key and initialization vector (IV).</summary>
		/// <returns>A symmetric encryptor object.</returns>
		/// <param name="key">The secret key to use for the symmetric algorithm.</param>
		/// <param name="iv">The initialization vector to use for the symmetric algorithm.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> or <paramref name="iv" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is invalid.</exception>
		// Token: 0x0600003B RID: 59 RVA: 0x00002414 File Offset: 0x00000614
		public override ICryptoTransform CreateEncryptor(byte[] key, byte[] iv)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (!base.ValidKeySize(key.Length * 8))
			{
				throw new ArgumentException(global::SR.GetString("Specified key is not a valid size for this algorithm."), "key");
			}
			if (iv != null && iv.Length * 8 != this.BlockSizeValue)
			{
				throw new ArgumentException(global::SR.GetString("Specified initialization vector (IV) does not match the block size for this algorithm."), "iv");
			}
			return this.m_rijndael.CreateEncryptor(key, iv);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002484 File Offset: 0x00000684
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					((IDisposable)this.m_rijndael).Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		/// <summary>Generates a random initialization vector (IV) to use for the symmetric algorithm.</summary>
		// Token: 0x0600003D RID: 61 RVA: 0x000024BC File Offset: 0x000006BC
		public override void GenerateIV()
		{
			this.m_rijndael.GenerateIV();
		}

		/// <summary>Generates a random key to use for the symmetric algorithm. </summary>
		// Token: 0x0600003E RID: 62 RVA: 0x000024C9 File Offset: 0x000006C9
		public override void GenerateKey()
		{
			this.m_rijndael.GenerateKey();
		}

		// Token: 0x04000005 RID: 5
		private RijndaelManaged m_rijndael;
	}
}
