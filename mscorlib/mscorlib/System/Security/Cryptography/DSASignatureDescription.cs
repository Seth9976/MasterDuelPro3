using System;

namespace System.Security.Cryptography
{
	// Token: 0x020003B9 RID: 953
	internal class DSASignatureDescription : SignatureDescription
	{
		// Token: 0x06002071 RID: 8305 RVA: 0x00084395 File Offset: 0x00082595
		public DSASignatureDescription()
		{
			base.KeyAlgorithm = "System.Security.Cryptography.DSACryptoServiceProvider";
			base.DigestAlgorithm = "System.Security.Cryptography.SHA1CryptoServiceProvider";
			base.FormatterAlgorithm = "System.Security.Cryptography.DSASignatureFormatter";
			base.DeformatterAlgorithm = "System.Security.Cryptography.DSASignatureDeformatter";
		}
	}
}
