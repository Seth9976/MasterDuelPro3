using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Defines a wrapper object to access the cryptographic service provider (CSP) implementation of the <see cref="T:System.Security.Cryptography.RC2" /> algorithm. This class cannot be inherited.</summary>
	// Token: 0x020003A1 RID: 929
	[ComVisible(true)]
	public sealed class RC2CryptoServiceProvider : RC2
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RC2CryptoServiceProvider" /> class.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired. </exception>
		/// <exception cref="T:System.InvalidOperationException">A non-compliant FIPS algorithm was found.</exception>
		// Token: 0x06001FD3 RID: 8147 RVA: 0x0007D9CC File Offset: 0x0007BBCC
		public RC2CryptoServiceProvider()
		{
			if (CryptoConfig.AllowOnlyFipsAlgorithms)
			{
				throw new InvalidOperationException(Environment.GetResourceString("This implementation is not part of the Windows Platform FIPS validated cryptographic algorithms."));
			}
			if (!Utils.HasAlgorithm(26114, 0))
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptographic service provider (CSP) could not be found for this algorithm."));
			}
			this.LegalKeySizesValue = RC2CryptoServiceProvider.s_legalKeySizes;
			this.FeedbackSizeValue = 8;
		}

		/// <summary>Gets or sets the effective size, in bits, of the secret key used by the <see cref="T:System.Security.Cryptography.RC2" /> algorithm.</summary>
		/// <returns>The effective key size, in bits, used by the <see cref="T:System.Security.Cryptography.RC2" /> algorithm.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">The <see cref="P:System.Security.Cryptography.RC2CryptoServiceProvider.EffectiveKeySize" /> property was set to a value other than the <see cref="F:System.Security.Cryptography.SymmetricAlgorithm.KeySizeValue" /> property. </exception>
		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06001FD4 RID: 8148 RVA: 0x0007D96D File Offset: 0x0007BB6D
		public override int EffectiveKeySize
		{
			get
			{
				return this.KeySizeValue;
			}
		}

		/// <summary>Creates a symmetric <see cref="T:System.Security.Cryptography.RC2" /> encryptor object with the specified key (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" />) and initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).</summary>
		/// <returns>A symmetric <see cref="T:System.Security.Cryptography.RC2" /> encryptor object.</returns>
		/// <param name="rgbKey">The secret key to use for the symmetric algorithm. </param>
		/// <param name="rgbIV">The initialization vector to use for the symmetric algorithm. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An <see cref="F:System.Security.Cryptography.CipherMode.OFB" /> cipher mode was used.-or-A <see cref="F:System.Security.Cryptography.CipherMode.CFB" /> cipher mode with a feedback size other than 8 bits was used.-or-An invalid key size was used.-or-The algorithm key size was not available.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001FD5 RID: 8149 RVA: 0x0007DA25 File Offset: 0x0007BC25
		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
		{
			if (this.m_use40bitSalt)
			{
				throw new NotImplementedException("UseSalt=true is not implemented on Mono yet");
			}
			return new RC2Transform(this, true, rgbKey, rgbIV);
		}

		/// <summary>Creates a symmetric <see cref="T:System.Security.Cryptography.RC2" /> decryptor object with the specified key (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" />)and initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).</summary>
		/// <returns>A symmetric <see cref="T:System.Security.Cryptography.RC2" /> decryptor object.</returns>
		/// <param name="rgbKey">The secret key to use for the symmetric algorithm. </param>
		/// <param name="rgbIV">The initialization vector to use for the symmetric algorithm. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An <see cref="F:System.Security.Cryptography.CipherMode.OFB" /> cipher mode was used.-or-A <see cref="F:System.Security.Cryptography.CipherMode.CFB" /> cipher mode with a feedback size other than 8 bits was used.-or-An invalid key size was used.-or-The algorithm key size was not available.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001FD6 RID: 8150 RVA: 0x0007DA43 File Offset: 0x0007BC43
		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
		{
			if (this.m_use40bitSalt)
			{
				throw new NotImplementedException("UseSalt=true is not implemented on Mono yet");
			}
			return new RC2Transform(this, false, rgbKey, rgbIV);
		}

		/// <summary>Generates a random key (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" />) to be used for the algorithm.</summary>
		// Token: 0x06001FD7 RID: 8151 RVA: 0x0007DA61 File Offset: 0x0007BC61
		public override void GenerateKey()
		{
			this.KeyValue = new byte[this.KeySizeValue / 8];
			Utils.StaticRandomNumberGenerator.GetBytes(this.KeyValue);
		}

		/// <summary>Generates a random initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />) to use for the algorithm.</summary>
		// Token: 0x06001FD8 RID: 8152 RVA: 0x0007C72D File Offset: 0x0007A92D
		public override void GenerateIV()
		{
			this.IVValue = new byte[8];
			Utils.StaticRandomNumberGenerator.GetBytes(this.IVValue);
		}

		// Token: 0x04000EED RID: 3821
		private bool m_use40bitSalt;

		// Token: 0x04000EEE RID: 3822
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(40, 128, 8)
		};
	}
}
