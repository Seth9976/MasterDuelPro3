using System;
using System.Runtime.InteropServices;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	/// <summary>Defines a wrapper object to access the cryptographic service provider (CSP) implementation of the <see cref="T:System.Security.Cryptography.DSA" /> algorithm. This class cannot be inherited. </summary>
	// Token: 0x020003C0 RID: 960
	[ComVisible(true)]
	public sealed class DSACryptoServiceProvider : DSA
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> class.</summary>
		// Token: 0x060020BA RID: 8378 RVA: 0x00086C92 File Offset: 0x00084E92
		public DSACryptoServiceProvider()
			: this(1024)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> class with the specified parameters for the cryptographic service provider (CSP).</summary>
		/// <param name="parameters">The parameters for the CSP. </param>
		// Token: 0x060020BB RID: 8379 RVA: 0x00086C9F File Offset: 0x00084E9F
		public DSACryptoServiceProvider(CspParameters parameters)
			: this(1024, parameters)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> class with the specified key size.</summary>
		/// <param name="dwKeySize">The size of the key for the asymmetric algorithm in bits. </param>
		// Token: 0x060020BC RID: 8380 RVA: 0x00086CAD File Offset: 0x00084EAD
		public DSACryptoServiceProvider(int dwKeySize)
		{
			this.privateKeyExportable = true;
			base..ctor();
			this.Common(dwKeySize, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> class with the specified key size and parameters for the cryptographic service provider (CSP).</summary>
		/// <param name="dwKeySize">The size of the key for the cryptographic algorithm in bits. </param>
		/// <param name="parameters">The parameters for the CSP. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The CSP cannot be acquired.-or- The key cannot be created. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="dwKeySize" /> is out of range.</exception>
		// Token: 0x060020BD RID: 8381 RVA: 0x00086CC4 File Offset: 0x00084EC4
		public DSACryptoServiceProvider(int dwKeySize, CspParameters parameters)
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

		// Token: 0x060020BE RID: 8382 RVA: 0x00086CF8 File Offset: 0x00084EF8
		private void Common(int dwKeySize, bool parameters)
		{
			this.LegalKeySizesValue = new KeySizes[1];
			this.LegalKeySizesValue[0] = new KeySizes(512, 1024, 64);
			this.KeySize = dwKeySize;
			this.dsa = new DSAManaged(dwKeySize);
			this.dsa.KeyGenerated += this.OnKeyGenerated;
			this.persistKey = parameters;
			if (parameters)
			{
				return;
			}
			CspParameters cspParameters = new CspParameters(13);
			if (DSACryptoServiceProvider.useMachineKeyStore)
			{
				cspParameters.Flags |= CspProviderFlags.UseMachineKeyStore;
			}
			this.store = new KeyPairPersistence(cspParameters);
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x00086D88 File Offset: 0x00084F88
		private void Common(CspParameters parameters)
		{
			this.store = new KeyPairPersistence(parameters);
			this.store.Load();
			if (this.store.KeyValue != null)
			{
				this.persisted = true;
				this.FromXmlString(this.store.KeyValue);
			}
			this.privateKeyExportable = (parameters.Flags & CspProviderFlags.UseNonExportableKey) == CspProviderFlags.NoFlags;
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x00086DE4 File Offset: 0x00084FE4
		~DSACryptoServiceProvider()
		{
			this.Dispose(false);
		}

		/// <summary>Gets the size of the key used by the asymmetric algorithm in bits.</summary>
		/// <returns>The size of the key used by the asymmetric algorithm.</returns>
		// Token: 0x1700039B RID: 923
		// (get) Token: 0x060020C1 RID: 8385 RVA: 0x00086E14 File Offset: 0x00085014
		public override int KeySize
		{
			get
			{
				return this.dsa.KeySize;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> object contains only a public key.</summary>
		/// <returns>true if the <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> object contains only a public key; otherwise, false.</returns>
		// Token: 0x1700039C RID: 924
		// (get) Token: 0x060020C2 RID: 8386 RVA: 0x00086E21 File Offset: 0x00085021
		[ComVisible(false)]
		public bool PublicOnly
		{
			get
			{
				return this.dsa.PublicOnly;
			}
		}

		/// <summary>Exports the <see cref="T:System.Security.Cryptography.DSAParameters" />.</summary>
		/// <returns>The parameters for <see cref="T:System.Security.Cryptography.DSA" />.</returns>
		/// <param name="includePrivateParameters">true to include private parameters; otherwise, false. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key cannot be exported. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060020C3 RID: 8387 RVA: 0x00086E2E File Offset: 0x0008502E
		public override DSAParameters ExportParameters(bool includePrivateParameters)
		{
			if (includePrivateParameters && !this.privateKeyExportable)
			{
				throw new CryptographicException(Locale.GetText("Cannot export private key"));
			}
			return this.dsa.ExportParameters(includePrivateParameters);
		}

		/// <summary>Imports the specified <see cref="T:System.Security.Cryptography.DSAParameters" />.</summary>
		/// <param name="parameters">The parameters for <see cref="T:System.Security.Cryptography.DSA" />. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.-or- The <paramref name="parameters" /> parameter has missing fields. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060020C4 RID: 8388 RVA: 0x00086E57 File Offset: 0x00085057
		public override void ImportParameters(DSAParameters parameters)
		{
			this.dsa.ImportParameters(parameters);
		}

		/// <summary>Creates the <see cref="T:System.Security.Cryptography.DSA" /> signature for the specified data.</summary>
		/// <returns>The digital signature for the specified data.</returns>
		/// <param name="rgbHash">The data to be signed. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060020C5 RID: 8389 RVA: 0x00086E65 File Offset: 0x00085065
		public override byte[] CreateSignature(byte[] rgbHash)
		{
			return this.dsa.CreateSignature(rgbHash);
		}

		/// <summary>Verifies the <see cref="T:System.Security.Cryptography.DSA" /> signature for the specified data.</summary>
		/// <returns>true if <paramref name="rgbSignature" /> matches the signature computed using the specified hash algorithm and key on <paramref name="rgbHash" />; otherwise, false.</returns>
		/// <param name="rgbHash">The data signed with <paramref name="rgbSignature" />. </param>
		/// <param name="rgbSignature">The signature to be verified for <paramref name="rgbData" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060020C6 RID: 8390 RVA: 0x00086E73 File Offset: 0x00085073
		public override bool VerifySignature(byte[] rgbHash, byte[] rgbSignature)
		{
			return this.dsa.VerifySignature(rgbHash, rgbSignature);
		}

		// Token: 0x060020C7 RID: 8391 RVA: 0x00086E82 File Offset: 0x00085082
		protected override void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				if (this.persisted && !this.persistKey)
				{
					this.store.Remove();
				}
				if (this.dsa != null)
				{
					this.dsa.Clear();
				}
				this.m_disposed = true;
			}
		}

		// Token: 0x060020C8 RID: 8392 RVA: 0x00086EC4 File Offset: 0x000850C4
		private void OnKeyGenerated(object sender, EventArgs e)
		{
			if (this.persistKey && !this.persisted)
			{
				this.store.KeyValue = this.ToXmlString(!this.dsa.PublicOnly);
				this.store.Save();
				this.persisted = true;
			}
		}

		// Token: 0x04000F52 RID: 3922
		private KeyPairPersistence store;

		// Token: 0x04000F53 RID: 3923
		private bool persistKey;

		// Token: 0x04000F54 RID: 3924
		private bool persisted;

		// Token: 0x04000F55 RID: 3925
		private bool privateKeyExportable;

		// Token: 0x04000F56 RID: 3926
		private bool m_disposed;

		// Token: 0x04000F57 RID: 3927
		private DSAManaged dsa;

		// Token: 0x04000F58 RID: 3928
		private static bool useMachineKeyStore;
	}
}
