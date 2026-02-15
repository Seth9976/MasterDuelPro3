using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Verifies a Digital Signature Algorithm (<see cref="T:System.Security.Cryptography.DSA" />) PKCS#1 v1.5 signature.</summary>
	// Token: 0x0200038E RID: 910
	[ComVisible(true)]
	public class DSASignatureDeformatter : AsymmetricSignatureDeformatter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.DSASignatureDeformatter" /> class.</summary>
		// Token: 0x06001F6E RID: 8046 RVA: 0x0007CB0D File Offset: 0x0007AD0D
		public DSASignatureDeformatter()
		{
			this._oid = CryptoConfig.MapNameToOID("SHA1");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.DSASignatureDeformatter" /> class with the specified key.</summary>
		/// <param name="key">The instance of Digital Signature Algorithm (<see cref="T:System.Security.Cryptography.DSA" />) that holds the key. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06001F6F RID: 8047 RVA: 0x0007CB25 File Offset: 0x0007AD25
		public DSASignatureDeformatter(AsymmetricAlgorithm key)
			: this()
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._dsaKey = (DSA)key;
		}

		/// <summary>Specifies the key to be used for the Digital Signature Algorithm (<see cref="T:System.Security.Cryptography.DSA" />) signature deformatter.</summary>
		/// <param name="key">The instance of <see cref="T:System.Security.Cryptography.DSA" /> that holds the key. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06001F70 RID: 8048 RVA: 0x0007CB47 File Offset: 0x0007AD47
		public override void SetKey(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._dsaKey = (DSA)key;
		}

		/// <summary>Specifies the hash algorithm for the Digital Signature Algorithm (<see cref="T:System.Security.Cryptography.DSA" />) signature deformatter.</summary>
		/// <param name="strName">The name of the hash algorithm to use for the signature deformatter. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">The <paramref name="strName" /> parameter does not map to the <see cref="T:System.Security.Cryptography.SHA1" /> hash algorithm. </exception>
		// Token: 0x06001F71 RID: 8049 RVA: 0x0007CB63 File Offset: 0x0007AD63
		public override void SetHashAlgorithm(string strName)
		{
			if (CryptoConfig.MapNameToOID(strName) != this._oid)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("This operation is not supported for this class."));
			}
		}

		/// <summary>Verifies the Digital Signature Algorithm (<see cref="T:System.Security.Cryptography.DSA" />) signature on the data.</summary>
		/// <returns>true if the signature is valid for the data; otherwise, false.</returns>
		/// <param name="rgbHash">The data signed with <paramref name="rgbSignature" />. </param>
		/// <param name="rgbSignature">The signature to be verified for <paramref name="rgbHash" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rgbHash" /> is null.-or-<paramref name="rgbSignature" /> is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">The DSA key is missing.</exception>
		// Token: 0x06001F72 RID: 8050 RVA: 0x0007CB88 File Offset: 0x0007AD88
		public override bool VerifySignature(byte[] rgbHash, byte[] rgbSignature)
		{
			if (rgbHash == null)
			{
				throw new ArgumentNullException("rgbHash");
			}
			if (rgbSignature == null)
			{
				throw new ArgumentNullException("rgbSignature");
			}
			if (this._dsaKey == null)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("No asymmetric key object has been associated with this formatter object."));
			}
			return this._dsaKey.VerifySignature(rgbHash, rgbSignature);
		}

		// Token: 0x04000EC4 RID: 3780
		private DSA _dsaKey;

		// Token: 0x04000EC5 RID: 3781
		private string _oid;
	}
}
