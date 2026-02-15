using System;

namespace Mono.Net.Security
{
	// Token: 0x02000066 RID: 102
	internal class AsyncHandshakeRequest : AsyncProtocolRequest
	{
		// Token: 0x06000134 RID: 308 RVA: 0x0000556E File Offset: 0x0000376E
		public AsyncHandshakeRequest(MobileAuthenticatedStream parent, bool sync)
			: base(parent, sync)
		{
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00005578 File Offset: 0x00003778
		protected override AsyncOperationStatus Run(AsyncOperationStatus status)
		{
			return base.Parent.ProcessHandshake(status, false);
		}
	}
}
