using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000015 RID: 21
	// (Invoke) Token: 0x060000A3 RID: 163
	public delegate bool CertificateValidationCallback(X509Certificate certificate, SslPolicyErrors certificateErrors);
}
