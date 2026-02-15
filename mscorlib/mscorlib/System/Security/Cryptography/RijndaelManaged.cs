using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Accesses the managed version of the <see cref="T:System.Security.Cryptography.Rijndael" /> algorithm. This class cannot be inherited.</summary>
	// Token: 0x020003A3 RID: 931
	[ComVisible(true)]
	public sealed class RijndaelManaged : Rijndael
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RijndaelManaged" /> class.</summary>
		/// <exception cref="T:System.InvalidOperationException">This class is not compliant with the FIPS algorithm.</exception>
		// Token: 0x06001FDC RID: 8156 RVA: 0x0007DB23 File Offset: 0x0007BD23
		public RijndaelManaged()
		{
			if (CryptoConfig.AllowOnlyFipsAlgorithms)
			{
				throw new InvalidOperationException(Environment.GetResourceString("This implementation is not part of the Windows Platform FIPS validated cryptographic algorithms."));
			}
		}

		/// <summary>Creates a symmetric <see cref="T:System.Security.Cryptography.Rijndael" /> encryptor object with the specified <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" /> and initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).</summary>
		/// <returns>A symmetric <see cref="T:System.Security.Cryptography.Rijndael" /> encryptor object.</returns>
		/// <param name="rgbKey">The secret key to be used for the symmetric algorithm. The key size must be 128, 192, or 256 bits.</param>
		/// <param name="rgbIV">The IV to be used for the symmetric algorithm. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rgbKey" /> parameter is null.-or-The <paramref name="rgbIV" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The value of the <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Mode" /> property is not <see cref="F:System.Security.Cryptography.CipherMode.ECB" />, <see cref="F:System.Security.Cryptography.CipherMode.CBC" />, or <see cref="F:System.Security.Cryptography.CipherMode.CFB" />.</exception>
		// Token: 0x06001FDD RID: 8157 RVA: 0x0007DB42 File Offset: 0x0007BD42
		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
		{
			return this.NewEncryptor(rgbKey, this.ModeValue, rgbIV, this.FeedbackSizeValue, RijndaelManagedTransformMode.Encrypt);
		}

		/// <summary>Creates a symmetric <see cref="T:System.Security.Cryptography.Rijndael" /> decryptor object with the specified <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" /> and initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).</summary>
		/// <returns>A symmetric <see cref="T:System.Security.Cryptography.Rijndael" /> decryptor object.</returns>
		/// <param name="rgbKey">The secret key to be used for the symmetric algorithm. The key size must be 128, 192, or 256 bits.</param>
		/// <param name="rgbIV">The IV to be used for the symmetric algorithm. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rgbKey" /> parameter is null.-or-The <paramref name="rgbIV" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The value of the <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Mode" /> property is not <see cref="F:System.Security.Cryptography.CipherMode.ECB" />, <see cref="F:System.Security.Cryptography.CipherMode.CBC" />, or <see cref="F:System.Security.Cryptography.CipherMode.CFB" />.</exception>
		// Token: 0x06001FDE RID: 8158 RVA: 0x0007DB59 File Offset: 0x0007BD59
		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
		{
			return this.NewEncryptor(rgbKey, this.ModeValue, rgbIV, this.FeedbackSizeValue, RijndaelManagedTransformMode.Decrypt);
		}

		/// <summary>Generates a random <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" /> to be used for the algorithm.</summary>
		// Token: 0x06001FDF RID: 8159 RVA: 0x0007DB70 File Offset: 0x0007BD70
		public override void GenerateKey()
		{
			this.KeyValue = Utils.GenerateRandom(this.KeySizeValue / 8);
		}

		/// <summary>Generates a random initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />) to be used for the algorithm.</summary>
		// Token: 0x06001FE0 RID: 8160 RVA: 0x0007DB85 File Offset: 0x0007BD85
		public override void GenerateIV()
		{
			this.IVValue = Utils.GenerateRandom(this.BlockSizeValue / 8);
		}

		// Token: 0x06001FE1 RID: 8161 RVA: 0x0007DB9A File Offset: 0x0007BD9A
		private ICryptoTransform NewEncryptor(byte[] rgbKey, CipherMode mode, byte[] rgbIV, int feedbackSize, RijndaelManagedTransformMode encryptMode)
		{
			if (rgbKey == null)
			{
				rgbKey = Utils.GenerateRandom(this.KeySizeValue / 8);
			}
			if (rgbIV == null)
			{
				rgbIV = Utils.GenerateRandom(this.BlockSizeValue / 8);
			}
			return new RijndaelManagedTransform(rgbKey, mode, rgbIV, this.BlockSizeValue, feedbackSize, this.PaddingValue, encryptMode);
		}
	}
}
