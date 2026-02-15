using System;
using System.Security.Cryptography.X509Certificates;

namespace System.Net.Security
{
	// Token: 0x020004F3 RID: 1267
	// (Invoke) Token: 0x06001EE9 RID: 7913
	internal delegate X509Certificate LocalCertSelectionCallback(string targetHost, X509CertificateCollection localCertificates, X509Certificate remoteCertificate, string[] acceptableIssuers);
}
