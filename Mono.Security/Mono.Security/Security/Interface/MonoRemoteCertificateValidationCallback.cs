using System;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Security.Interface
{
	// Token: 0x02000040 RID: 64
	// (Invoke) Token: 0x06000140 RID: 320
	public delegate bool MonoRemoteCertificateValidationCallback(string targetHost, X509Certificate certificate, X509Chain chain, MonoSslPolicyErrors sslPolicyErrors);
}
