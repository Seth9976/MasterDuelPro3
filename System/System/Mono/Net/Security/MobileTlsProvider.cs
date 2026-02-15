using System;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Interface;

namespace Mono.Net.Security
{
	// Token: 0x02000075 RID: 117
	internal abstract class MobileTlsProvider : MonoTlsProvider
	{
		// Token: 0x060001A8 RID: 424
		internal abstract MobileAuthenticatedStream CreateSslStream(SslStream sslStream, Stream innerStream, bool leaveInnerStreamOpen, MonoTlsSettings settings);

		// Token: 0x060001A9 RID: 425
		internal abstract bool ValidateCertificate(ChainValidationHelper validator, string targetHost, bool serverMode, X509CertificateCollection certificates, bool wantsChain, ref X509Chain chain, ref SslPolicyErrors errors, ref int status11);
	}
}
