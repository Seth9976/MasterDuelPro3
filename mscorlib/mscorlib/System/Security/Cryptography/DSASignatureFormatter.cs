using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Creates a Digital Signature Algorithm (<see cref="T:System.Security.Cryptography.DSA" />) signature.</summary>
	// Token: 0x0200038F RID: 911
	[ComVisible(true)]
	public class DSASignatureFormatter : AsymmetricSignatureFormatter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.DSASignatureFormatter" /> class.</summary>
		// Token: 0x06001F73 RID: 8051 RVA: 0x0007CBD6 File Offset: 0x0007ADD6
		public DSASignatureFormatter()
		{
			this._oid = CryptoConfig.MapNameToOID("SHA1");
		}

		/// <summary>Specifies the key to be used for the Digital Signature Algorithm (<see cref="T:System.Security.Cryptography.DSA" />) signature formatter.</summary>
		/// <param name="key">The instance of <see cref="T:System.Security.Cryptography.DSA" /> that holds the key. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06001F74 RID: 8052 RVA: 0x0007CBEE File Offset: 0x0007ADEE
		public override void SetKey(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._dsaKey = (DSA)key;
		}

		/// <summary>Specifies the hash algorithm for the Digital Signature Algorithm (<see cref="T:System.Security.Cryptography.DSA" />) signature formatter.</summary>
		/// <param name="strName">The name of the hash algorithm to use for the signature formatter. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">The <paramref name="strName" /> parameter does not map to the <see cref="T:System.Security.Cryptography.SHA1" /> hash algorithm. </exception>
		// Token: 0x06001F75 RID: 8053 RVA: 0x0007CC0A File Offset: 0x0007AE0A
		public override void SetHashAlgorithm(string strName)
		{
			if (CryptoConfig.MapNameToOID(strName) != this._oid)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("This operation is not supported for this class."));
			}
		}

		/// <summary>Creates the Digital Signature Algorithm (<see cref="T:System.Security.Cryptography.DSA" />) PKCS #1 signature for the specified data.</summary>
		/// <returns>The digital signature for the specified data.</returns>
		/// <param name="rgbHash">The data to be signed. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rgbHash" /> is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">The OID is null.-or-The DSA key is null.</exception>
		// Token: 0x06001F76 RID: 8054 RVA: 0x0007CC30 File Offset: 0x0007AE30
		public override byte[] CreateSignature(byte[] rgbHash)
		{
			if (rgbHash == null)
			{
				throw new ArgumentNullException("rgbHash");
			}
			if (this._oid == null)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Required object identifier (OID) cannot be found."));
			}
			if (this._dsaKey == null)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("No asymmetric key object has been associated with this formatter object."));
			}
			return this._dsaKey.CreateSignature(rgbHash);
		}

		// Token: 0x04000EC6 RID: 3782
		private DSA _dsaKey;

		// Token: 0x04000EC7 RID: 3783
		private string _oid;
	}
}
