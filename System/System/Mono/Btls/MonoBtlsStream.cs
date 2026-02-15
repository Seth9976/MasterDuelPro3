using System;
using System.IO;
using System.Net.Security;
using Mono.Net.Security;
using Mono.Security.Interface;

namespace Mono.Btls
{
	// Token: 0x020000B7 RID: 183
	internal class MonoBtlsStream : MobileAuthenticatedStream
	{
		// Token: 0x0600034D RID: 845 RVA: 0x00003F18 File Offset: 0x00002118
		public MonoBtlsStream(Stream innerStream, bool leaveInnerStreamOpen, SslStream owner, MonoTlsSettings settings, MobileTlsProvider provider)
			: base(innerStream, leaveInnerStreamOpen, owner, settings, provider)
		{
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000BEF9 File Offset: 0x0000A0F9
		protected override MobileTlsContext CreateContext(MonoSslAuthenticationOptions options)
		{
			return new MonoBtlsContext(this, options);
		}
	}
}
