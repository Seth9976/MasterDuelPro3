using System;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Security.Interface
{
	// Token: 0x02000041 RID: 65
	// (Invoke) Token: 0x06000142 RID: 322
	public delegate X509Certificate MonoLocalCertificateSelectionCallback(string targetHost, X509CertificateCollection localCertificates, X509Certificate remoteCertificate, string[] acceptableIssuers);
}
