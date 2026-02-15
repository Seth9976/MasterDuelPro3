using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Interface;

namespace Mono.Net.Security.Private
{
	// Token: 0x0200007E RID: 126
	internal static class CallbackHelpers
	{
		// Token: 0x060001F2 RID: 498 RVA: 0x00007FF4 File Offset: 0x000061F4
		internal static MonoRemoteCertificateValidationCallback PublicToMono(RemoteCertificateValidationCallback callback)
		{
			if (callback == null)
			{
				return null;
			}
			return (string h, X509Certificate c, X509Chain ch, MonoSslPolicyErrors e) => callback(h, c, ch, (SslPolicyErrors)e);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00008024 File Offset: 0x00006224
		internal static LocalCertSelectionCallback MonoToInternal(MonoLocalCertificateSelectionCallback callback)
		{
			if (callback == null)
			{
				return null;
			}
			return (string t, X509CertificateCollection lc, X509Certificate rc, string[] ai) => callback(t, lc, rc, ai);
		}
	}
}
