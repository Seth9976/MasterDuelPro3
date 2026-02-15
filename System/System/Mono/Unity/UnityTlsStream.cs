using System;
using System.IO;
using System.Net.Security;
using Mono.Net.Security;
using Mono.Security.Interface;

namespace Mono.Unity
{
	// Token: 0x02000051 RID: 81
	internal class UnityTlsStream : MobileAuthenticatedStream
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x00003F18 File Offset: 0x00002118
		public UnityTlsStream(Stream innerStream, bool leaveInnerStreamOpen, SslStream owner, MonoTlsSettings settings, MobileTlsProvider provider)
			: base(innerStream, leaveInnerStreamOpen, owner, settings, provider)
		{
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003F27 File Offset: 0x00002127
		protected override MobileTlsContext CreateContext(MonoSslAuthenticationOptions options)
		{
			return new UnityTlsContext(this, options);
		}
	}
}
