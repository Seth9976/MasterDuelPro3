using System;
using System.Runtime.InteropServices;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	/// <summary>Performs asymmetric encryption and decryption using the implementation of the <see cref="T:System.Security.Cryptography.RSA" /> algorithm provided by the cryptographic service provider (CSP). This class cannot be inherited.</summary>
	// Token: 0x020003AA RID: 938
	[ComVisible(true)]
	public sealed class RSACryptoServiceProvider : RSA
	{
		/// <summary>Gets or sets a value indicating whether the key should be persisted in the computer's key store instead of the user profile store.</summary>
		/// <returns>true if the key should be persisted in the computer key store; otherwise, false.</returns>
		// Token: 0x17000388 RID: 904
		// (get) Token: 0x0600200D RID: 8205 RVA: 0x000820CB File Offset: 0x000802CB
		public static bool UseMachineKeyStore
		{
			get
			{
				return RSACryptoServiceProvider.s_UseMachineKeyStore == CspProviderFlags.UseMachineKeyStore;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> class using the default key.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired. </exception>
		// Token: 0x0600200E RID: 8206 RVA: 0x000820D7 File Offset: 0x000802D7
		public RSACryptoServiceProvider()
			: this(1024)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> class with the specified parameters.</summary>
		/// <param name="parameters">The parameters to be passed to the cryptographic service provider (CSP). </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The CSP cannot be acquired. </exception>
		// Token: 0x0600200F RID: 8207 RVA: 0x000820E4 File Offset: 0x000802E4
		public RSACryptoServiceProvider(CspParameters parameters)
			: this(1024, parameters)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> class with the specified key size.</summary>
		/// <param name="dwKeySize">The size of the key to use in bits. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired. </exception>
		// Token: 0x06002010 RID: 8208 RVA: 0x000820F2 File Offset: 0x000802F2
		public RSACryptoServiceProvider(int dwKeySize)
		{
			this.privateKeyExportable = true;
			base..ctor();
			this.Common(dwKeySize, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> class with the specified key size and parameters.</summary>
		/// <param name="dwKeySize">The size of the key to use in bits. </param>
		/// <param name="parameters">The parameters to be passed to the cryptographic service provider (CSP). </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The CSP cannot be acquired.-or- The key cannot be created. </exception>
		// Token: 0x06002011 RID: 8209 RVA: 0x0008210C File Offset: 0x0008030C
		public RSACryptoServiceProvider(int dwKeySize, CspParameters parameters)
		{
			this.privateKeyExportable = true;
			base..ctor();
			bool flag = parameters != null;
			this.Common(dwKeySize, flag);
			if (flag)
			{
				this.Common(parameters);
			}
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x00082140 File Offset: 0x00080340
		private void Common(int dwKeySize, bool parameters)
		{
			this.LegalKeySizesValue = new KeySizes[1];
			this.LegalKeySizesValue[0] = new KeySizes(384, 16384, 8);
			base.KeySize = dwKeySize;
			this.rsa = new RSAManaged(this.KeySize);
			this.rsa.KeyGenerated += this.OnKeyGenerated;
			this.persistKey = parameters;
			if (parameters)
			{
				return;
			}
			CspParameters cspParameters = new CspParameters(1);
			if (RSACryptoServiceProvider.UseMachineKeyStore)
			{
				cspParameters.Flags |= CspProviderFlags.UseMachineKeyStore;
			}
			this.store = new KeyPairPersistence(cspParameters);
		}

		// Token: 0x06002013 RID: 8211 RVA: 0x000821D4 File Offset: 0x000803D4
		private void Common(CspParameters p)
		{
			this.store = new KeyPairPersistence(p);
			bool flag = this.store.Load();
			bool flag2 = (p.Flags & CspProviderFlags.UseExistingKey) > CspProviderFlags.NoFlags;
			this.privateKeyExportable = (p.Flags & CspProviderFlags.UseNonExportableKey) == CspProviderFlags.NoFlags;
			if (flag2 && !flag)
			{
				throw new CryptographicException("Keyset does not exist");
			}
			if (this.store.KeyValue != null)
			{
				this.persisted = true;
				this.FromXmlString(this.store.KeyValue);
			}
		}

		// Token: 0x06002014 RID: 8212 RVA: 0x0008224C File Offset: 0x0008044C
		~RSACryptoServiceProvider()
		{
			this.Dispose(false);
		}

		/// <summary>Gets the size of the current key.</summary>
		/// <returns>The size of the key in bits.</returns>
		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06002015 RID: 8213 RVA: 0x0008227C File Offset: 0x0008047C
		public override int KeySize
		{
			get
			{
				if (this.rsa == null)
				{
					return this.KeySizeValue;
				}
				return this.rsa.KeySize;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> object contains only a public key.</summary>
		/// <returns>true if the <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> object contains only a public key; otherwise, false.</returns>
		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06002016 RID: 8214 RVA: 0x00082298 File Offset: 0x00080498
		[ComVisible(false)]
		public bool PublicOnly
		{
			get
			{
				return this.rsa.PublicOnly;
			}
		}

		/// <summary>This method is not supported in the current version.</summary>
		/// <returns>The decrypted data, which is the original plain text before encryption.</returns>
		/// <param name="rgb">The data to be decrypted. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported in the current version. </exception>
		// Token: 0x06002017 RID: 8215 RVA: 0x000822A5 File Offset: 0x000804A5
		public override byte[] DecryptValue(byte[] rgb)
		{
			if (!this.rsa.IsCrtPossible)
			{
				throw new CryptographicException("Incomplete private key - missing CRT.");
			}
			return this.rsa.DecryptValue(rgb);
		}

		/// <summary>This method is not supported in the current version.</summary>
		/// <returns>The encrypted data.</returns>
		/// <param name="rgb">The data to be encrypted. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported in the current version. </exception>
		// Token: 0x06002018 RID: 8216 RVA: 0x000822CB File Offset: 0x000804CB
		public override byte[] EncryptValue(byte[] rgb)
		{
			return this.rsa.EncryptValue(rgb);
		}

		/// <summary>Exports the <see cref="T:System.Security.Cryptography.RSAParameters" />.</summary>
		/// <returns>The parameters for <see cref="T:System.Security.Cryptography.RSA" />.</returns>
		/// <param name="includePrivateParameters">true to include private parameters; otherwise, false. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key cannot be exported. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002019 RID: 8217 RVA: 0x000822DC File Offset: 0x000804DC
		public override RSAParameters ExportParameters(bool includePrivateParameters)
		{
			if (includePrivateParameters && !this.privateKeyExportable)
			{
				throw new CryptographicException("cannot export private key");
			}
			RSAParameters rsaparameters = this.rsa.ExportParameters(includePrivateParameters);
			if (includePrivateParameters)
			{
				if (rsaparameters.D == null)
				{
					throw new ArgumentNullException("Missing D parameter for the private key.");
				}
				if (rsaparameters.P == null || rsaparameters.Q == null || rsaparameters.DP == null || rsaparameters.DQ == null || rsaparameters.InverseQ == null)
				{
					throw new CryptographicException("Missing some CRT parameters for the private key.");
				}
			}
			return rsaparameters;
		}

		/// <summary>Imports the specified <see cref="T:System.Security.Cryptography.RSAParameters" />.</summary>
		/// <param name="parameters">The parameters for <see cref="T:System.Security.Cryptography.RSA" />. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.-or- The <paramref name="parameters" /> parameter has missing fields. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600201A RID: 8218 RVA: 0x00082356 File Offset: 0x00080556
		public override void ImportParameters(RSAParameters parameters)
		{
			this.rsa.ImportParameters(parameters);
		}

		// Token: 0x0600201B RID: 8219 RVA: 0x00082364 File Offset: 0x00080564
		private string GetHashNameFromOID(string oid)
		{
			if (oid == "1.3.14.3.2.26")
			{
				return "SHA1";
			}
			if (oid == "1.2.840.113549.2.5")
			{
				return "MD5";
			}
			if (oid == "2.16.840.1.101.3.4.2.1")
			{
				return "SHA256";
			}
			if (oid == "2.16.840.1.101.3.4.2.2")
			{
				return "SHA384";
			}
			if (!(oid == "2.16.840.1.101.3.4.2.3"))
			{
				throw new CryptographicException(oid + " is an unsupported hash algorithm for RSA signing");
			}
			return "SHA512";
		}

		/// <summary>Verifies that a digital signature is valid by determining the hash value in the signature using the provided public key and comparing it to the provided hash value.</summary>
		/// <returns>true if the signature is valid; otherwise, false.</returns>
		/// <param name="rgbHash">The hash value of the signed data. </param>
		/// <param name="str">The hash algorithm identifier (OID) used to create the hash value of the data. </param>
		/// <param name="rgbSignature">The signature data to be verified. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rgbHash" /> parameter is null.-or- The <paramref name="rgbSignature" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.-or- The signature cannot be verified. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600201C RID: 8220 RVA: 0x000823E4 File Offset: 0x000805E4
		public bool VerifyHash(byte[] rgbHash, string str, byte[] rgbSignature)
		{
			if (rgbHash == null)
			{
				throw new ArgumentNullException("rgbHash");
			}
			if (rgbSignature == null)
			{
				throw new ArgumentNullException("rgbSignature");
			}
			HashAlgorithm hashAlgorithm = HashAlgorithm.Create((str == null) ? "SHA1" : this.GetHashNameFromOID(str));
			return PKCS1.Verify_v15(this, hashAlgorithm, rgbHash, rgbSignature);
		}

		// Token: 0x0600201D RID: 8221 RVA: 0x0008242D File Offset: 0x0008062D
		protected override void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				if (this.persisted && !this.persistKey)
				{
					this.store.Remove();
				}
				if (this.rsa != null)
				{
					this.rsa.Clear();
				}
				this.m_disposed = true;
			}
		}

		// Token: 0x0600201E RID: 8222 RVA: 0x0008246C File Offset: 0x0008066C
		private void OnKeyGenerated(object sender, EventArgs e)
		{
			if (this.persistKey && !this.persisted)
			{
				this.store.KeyValue = this.ToXmlString(!this.rsa.PublicOnly);
				this.store.Save();
				this.persisted = true;
			}
		}

		// Token: 0x04000F18 RID: 3864
		private static volatile CspProviderFlags s_UseMachineKeyStore;

		// Token: 0x04000F19 RID: 3865
		private KeyPairPersistence store;

		// Token: 0x04000F1A RID: 3866
		private bool persistKey;

		// Token: 0x04000F1B RID: 3867
		private bool persisted;

		// Token: 0x04000F1C RID: 3868
		private bool privateKeyExportable;

		// Token: 0x04000F1D RID: 3869
		private bool m_disposed;

		// Token: 0x04000F1E RID: 3870
		private RSAManaged rsa;
	}
}
