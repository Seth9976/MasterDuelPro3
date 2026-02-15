using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the base class from which all implementations of asymmetric signature formatters derive.</summary>
	// Token: 0x02000381 RID: 897
	[ComVisible(true)]
	public abstract class AsymmetricSignatureFormatter
	{
		/// <summary>When overridden in a derived class, sets the asymmetric algorithm to use to create the signature.</summary>
		/// <param name="key">The instance of the implementation of <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> to use to create the signature. </param>
		// Token: 0x06001F3A RID: 7994
		public abstract void SetKey(AsymmetricAlgorithm key);

		/// <summary>When overridden in a derived class, sets the hash algorithm to use for creating the signature.</summary>
		/// <param name="strName">The name of the hash algorithm to use for creating the signature. </param>
		// Token: 0x06001F3B RID: 7995
		public abstract void SetHashAlgorithm(string strName);

		/// <summary>When overridden in a derived class, creates the signature for the specified data.</summary>
		/// <returns>The digital signature for the <paramref name="rgbHash" /> parameter.</returns>
		/// <param name="rgbHash">The data to be signed. </param>
		// Token: 0x06001F3C RID: 7996
		public abstract byte[] CreateSignature(byte[] rgbHash);
	}
}
