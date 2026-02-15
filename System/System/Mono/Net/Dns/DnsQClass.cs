using System;

namespace Mono.Net.Dns
{
	// Token: 0x02000085 RID: 133
	internal enum DnsQClass : ushort
	{
		// Token: 0x0400018D RID: 397
		Internet = 1,
		// Token: 0x0400018E RID: 398
		IN = 1,
		// Token: 0x0400018F RID: 399
		CSNET,
		// Token: 0x04000190 RID: 400
		CS = 2,
		// Token: 0x04000191 RID: 401
		CHAOS,
		// Token: 0x04000192 RID: 402
		CH = 3,
		// Token: 0x04000193 RID: 403
		Hesiod,
		// Token: 0x04000194 RID: 404
		HS = 4,
		// Token: 0x04000195 RID: 405
		None = 254,
		// Token: 0x04000196 RID: 406
		Any
	}
}
