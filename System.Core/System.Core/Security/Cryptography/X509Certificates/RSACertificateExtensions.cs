using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x02000017 RID: 23
	public static class RSACertificateExtensions
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00004330 File Offset: 0x00002530
		public static RSA GetRSAPublicKey(this X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			return certificate.PublicKey.Key as RSA;
		}
	}
}
