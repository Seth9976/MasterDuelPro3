using System;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	/// <summary>Performs symmetric encryption and decryption using the Cryptographic Application Programming Interfaces (CAPI) implementation of the Advanced Encryption Standard (AES) algorithm. </summary>
	// Token: 0x02000015 RID: 21
	public sealed class AesCryptoServiceProvider : Aes
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.AesCryptoServiceProvider" /> class. </summary>
		/// <exception cref="T:System.PlatformNotSupportedException">There is no supported key size for the current platform.</exception>
		// Token: 0x0600003F RID: 63 RVA: 0x000024D6 File Offset: 0x000006D6
		public AesCryptoServiceProvider()
		{
			this.FeedbackSizeValue = 8;
		}

		/// <summary>Generates a random initialization vector (IV) to use for the algorithm.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The initialization vector (IV) could not be generated. </exception>
		// Token: 0x06000040 RID: 64 RVA: 0x000024E5 File Offset: 0x000006E5
		public override void GenerateIV()
		{
			this.IVValue = KeyBuilder.IV(this.BlockSizeValue >> 3);
		}

		/// <summary>Generates a random key to use for the algorithm. </summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key could not be generated.</exception>
		// Token: 0x06000041 RID: 65 RVA: 0x000024FA File Offset: 0x000006FA
		public override void GenerateKey()
		{
			this.KeyValue = KeyBuilder.Key(this.KeySizeValue >> 3);
		}

		/// <summary>Creates a symmetric AES decryptor object using the specified key and initialization vector (IV).</summary>
		/// <returns>A symmetric AES decryptor object.</returns>
		/// <param name="key">The secret key to use for the symmetric algorithm.</param>
		/// <param name="iv">The initialization vector to use for the symmetric algorithm.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> or <paramref name="iv" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is invalid.</exception>
		// Token: 0x06000042 RID: 66 RVA: 0x0000250F File Offset: 0x0000070F
		public override ICryptoTransform CreateDecryptor(byte[] key, byte[] iv)
		{
			if (this.Mode == CipherMode.CFB && this.FeedbackSize > 64)
			{
				throw new CryptographicException("CFB with Feedbaack > 64 bits");
			}
			return new AesTransform(this, false, key, iv);
		}

		/// <summary>Creates a symmetric encryptor object using the specified key and initialization vector (IV).</summary>
		/// <returns>A symmetric AES encryptor object.</returns>
		/// <param name="key">The secret key to use for the symmetric algorithm.</param>
		/// <param name="iv">The initialization vector to use for the symmetric algorithm.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> or <paramref name="iv" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is invalid.</exception>
		// Token: 0x06000043 RID: 67 RVA: 0x00002538 File Offset: 0x00000738
		public override ICryptoTransform CreateEncryptor(byte[] key, byte[] iv)
		{
			if (this.Mode == CipherMode.CFB && this.FeedbackSize > 64)
			{
				throw new CryptographicException("CFB with Feedbaack > 64 bits");
			}
			return new AesTransform(this, true, key, iv);
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002561 File Offset: 0x00000761
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002569 File Offset: 0x00000769
		public override byte[] IV
		{
			get
			{
				return base.IV;
			}
			set
			{
				base.IV = value;
			}
		}

		/// <summary>Gets or sets the symmetric key that is used for encryption and decryption.</summary>
		/// <returns>The symmetric key that is used for encryption and decryption.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value for the key is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The size of the key is invalid.</exception>
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002572 File Offset: 0x00000772
		// (set) Token: 0x06000047 RID: 71 RVA: 0x0000257A File Offset: 0x0000077A
		public override byte[] Key
		{
			get
			{
				return base.Key;
			}
			set
			{
				base.Key = value;
			}
		}

		/// <summary>Gets or sets the size, in bits, of the secret key. </summary>
		/// <returns>The size, in bits, of the key.</returns>
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002583 File Offset: 0x00000783
		// (set) Token: 0x06000049 RID: 73 RVA: 0x0000258B File Offset: 0x0000078B
		public override int KeySize
		{
			get
			{
				return base.KeySize;
			}
			set
			{
				base.KeySize = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002594 File Offset: 0x00000794
		public override int FeedbackSize
		{
			get
			{
				return base.FeedbackSize;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600004B RID: 75 RVA: 0x0000259C File Offset: 0x0000079C
		// (set) Token: 0x0600004C RID: 76 RVA: 0x000025A4 File Offset: 0x000007A4
		public override CipherMode Mode
		{
			get
			{
				return base.Mode;
			}
			set
			{
				if (value == CipherMode.CTS)
				{
					throw new CryptographicException("CTS is not supported");
				}
				base.Mode = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000025BC File Offset: 0x000007BC
		// (set) Token: 0x0600004E RID: 78 RVA: 0x000025C4 File Offset: 0x000007C4
		public override PaddingMode Padding
		{
			get
			{
				return base.Padding;
			}
			set
			{
				base.Padding = value;
			}
		}

		/// <summary>Creates a symmetric AES decryptor object using the current key and initialization vector (IV).</summary>
		/// <returns>A symmetric AES decryptor object.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The current key is invalid or missing.</exception>
		// Token: 0x0600004F RID: 79 RVA: 0x000025CD File Offset: 0x000007CD
		public override ICryptoTransform CreateDecryptor()
		{
			return this.CreateDecryptor(this.Key, this.IV);
		}

		/// <summary>Creates a symmetric AES encryptor object using the current key and initialization vector (IV).</summary>
		/// <returns>A symmetric AES encryptor object.</returns>
		// Token: 0x06000050 RID: 80 RVA: 0x000025E1 File Offset: 0x000007E1
		public override ICryptoTransform CreateEncryptor()
		{
			return this.CreateEncryptor(this.Key, this.IV);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000025F5 File Offset: 0x000007F5
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}
