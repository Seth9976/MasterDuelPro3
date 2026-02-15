using System;

namespace Mono.Net.Dns
{
	// Token: 0x0200008C RID: 140
	internal class DnsResourceRecordAAAA : DnsResourceRecordIPAddress
	{
		// Token: 0x06000229 RID: 553 RVA: 0x00008984 File Offset: 0x00006B84
		internal DnsResourceRecordAAAA(DnsResourceRecord rr)
			: base(rr, 16)
		{
		}
	}
}
