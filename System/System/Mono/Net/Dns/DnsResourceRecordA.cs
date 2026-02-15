using System;

namespace Mono.Net.Dns
{
	// Token: 0x0200008B RID: 139
	internal class DnsResourceRecordA : DnsResourceRecordIPAddress
	{
		// Token: 0x06000228 RID: 552 RVA: 0x0000897A File Offset: 0x00006B7A
		internal DnsResourceRecordA(DnsResourceRecord rr)
			: base(rr, 4)
		{
		}
	}
}
