using System;
using Mono.Security.X509;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020001D1 RID: 465
	internal static class X509Helper2
	{
		// Token: 0x06000B59 RID: 2905 RVA: 0x00039218 File Offset: 0x00037418
		[MonoTODO("Investigate replacement; see comments in source.")]
		internal static X509Certificate GetMonoCertificate(X509Certificate2 certificate)
		{
			X509Certificate2ImplMono x509Certificate2ImplMono = certificate.Impl as X509Certificate2ImplMono;
			if (x509Certificate2ImplMono != null)
			{
				return x509Certificate2ImplMono.MonoCertificate;
			}
			return new X509Certificate(certificate.RawData);
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x00039246 File Offset: 0x00037446
		internal static X509ChainImpl CreateChainImpl(bool useMachineContext)
		{
			return new X509ChainImplMono(useMachineContext);
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0003924E File Offset: 0x0003744E
		public static bool IsValid(X509ChainImpl impl)
		{
			return impl != null && impl.IsValid;
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0003925B File Offset: 0x0003745B
		internal static void ThrowIfContextInvalid(X509ChainImpl impl)
		{
			if (!X509Helper2.IsValid(impl))
			{
				throw X509Helper2.GetInvalidChainContextException();
			}
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0003926B File Offset: 0x0003746B
		internal static Exception GetInvalidChainContextException()
		{
			return new CryptographicException(global::Locale.GetText("Chain instance is empty."));
		}
	}
}
