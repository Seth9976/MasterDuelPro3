using System;

namespace System.Security.Cryptography
{
	// Token: 0x020003B4 RID: 948
	internal abstract class RSAPKCS1SignatureDescription : SignatureDescription
	{
		// Token: 0x0600206C RID: 8300 RVA: 0x00084316 File Offset: 0x00082516
		protected RSAPKCS1SignatureDescription(string hashAlgorithm, string digestAlgorithm)
		{
			base.KeyAlgorithm = "System.Security.Cryptography.RSA";
			base.DigestAlgorithm = digestAlgorithm;
			base.FormatterAlgorithm = "System.Security.Cryptography.RSAPKCS1SignatureFormatter";
			base.DeformatterAlgorithm = "System.Security.Cryptography.RSAPKCS1SignatureDeformatter";
			this._hashAlgorithm = hashAlgorithm;
		}

		// Token: 0x04000F36 RID: 3894
		private string _hashAlgorithm;
	}
}
