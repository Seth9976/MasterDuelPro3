using System;
using System.Runtime.InteropServices;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	/// <summary>Creates an <see cref="T:System.Security.Cryptography.RSA" /> PKCS #1 version 1.5 signature.</summary>
	// Token: 0x020003C5 RID: 965
	[ComVisible(true)]
	public class RSAPKCS1SignatureFormatter : AsymmetricSignatureFormatter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSAPKCS1SignatureFormatter" /> class.</summary>
		// Token: 0x060020E7 RID: 8423 RVA: 0x00088E04 File Offset: 0x00087004
		public RSAPKCS1SignatureFormatter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSAPKCS1SignatureFormatter" /> class with the specified key.</summary>
		/// <param name="key">The instance of the <see cref="T:System.Security.Cryptography.RSA" /> algorithm that holds the private key. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x060020E8 RID: 8424 RVA: 0x00088E0C File Offset: 0x0008700C
		public RSAPKCS1SignatureFormatter(AsymmetricAlgorithm key)
		{
			this.SetKey(key);
		}

		/// <summary>Creates the <see cref="T:System.Security.Cryptography.RSA" /> PKCS #1 signature for the specified data.</summary>
		/// <returns>The digital signature for <paramref name="rgbHash" />.</returns>
		/// <param name="rgbHash">The data to be signed. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">The key is null.-or- The hash algorithm is null. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rgbHash" /> parameter is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060020E9 RID: 8425 RVA: 0x00088E1C File Offset: 0x0008701C
		public override byte[] CreateSignature(byte[] rgbHash)
		{
			if (this.rsa == null)
			{
				throw new CryptographicUnexpectedOperationException(Locale.GetText("No key pair available."));
			}
			if (this.hash == null)
			{
				throw new CryptographicUnexpectedOperationException(Locale.GetText("Missing hash algorithm."));
			}
			if (rgbHash == null)
			{
				throw new ArgumentNullException("rgbHash");
			}
			return PKCS1.Sign_v15(this.rsa, this.hash, rgbHash);
		}

		/// <summary>Sets the hash algorithm to use for creating the signature.</summary>
		/// <param name="strName">The name of the hash algorithm to use for creating the signature. </param>
		// Token: 0x060020EA RID: 8426 RVA: 0x00088E79 File Offset: 0x00087079
		public override void SetHashAlgorithm(string strName)
		{
			if (strName == null)
			{
				throw new ArgumentNullException("strName");
			}
			this.hash = strName;
		}

		/// <summary>Sets the private key to use for creating the signature.</summary>
		/// <param name="key">The instance of the <see cref="T:System.Security.Cryptography.RSA" /> algorithm that holds the private key. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x060020EB RID: 8427 RVA: 0x00088E90 File Offset: 0x00087090
		public override void SetKey(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.rsa = (RSA)key;
		}

		// Token: 0x04000F6A RID: 3946
		private RSA rsa;

		// Token: 0x04000F6B RID: 3947
		private string hash;
	}
}
