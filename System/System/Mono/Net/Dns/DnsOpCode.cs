using System;

namespace Mono.Net.Dns
{
	// Token: 0x02000083 RID: 131
	internal enum DnsOpCode : byte
	{
		// Token: 0x04000184 RID: 388
		Query,
		// Token: 0x04000185 RID: 389
		[Obsolete]
		IQuery,
		// Token: 0x04000186 RID: 390
		Status,
		// Token: 0x04000187 RID: 391
		Notify = 4,
		// Token: 0x04000188 RID: 392
		Update
	}
}
