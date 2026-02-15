using System;

namespace Mono.Net.Dns
{
	// Token: 0x02000089 RID: 137
	internal enum DnsRCode : ushort
	{
		// Token: 0x040001E2 RID: 482
		NoError,
		// Token: 0x040001E3 RID: 483
		FormErr,
		// Token: 0x040001E4 RID: 484
		ServFail,
		// Token: 0x040001E5 RID: 485
		NXDomain,
		// Token: 0x040001E6 RID: 486
		NotImp,
		// Token: 0x040001E7 RID: 487
		Refused,
		// Token: 0x040001E8 RID: 488
		YXDomain,
		// Token: 0x040001E9 RID: 489
		YXRRSet,
		// Token: 0x040001EA RID: 490
		NXRRSet,
		// Token: 0x040001EB RID: 491
		NotAuth,
		// Token: 0x040001EC RID: 492
		NotZone,
		// Token: 0x040001ED RID: 493
		BadVers = 16,
		// Token: 0x040001EE RID: 494
		BadSig = 16,
		// Token: 0x040001EF RID: 495
		BadKey,
		// Token: 0x040001F0 RID: 496
		BadTime,
		// Token: 0x040001F1 RID: 497
		BadMode,
		// Token: 0x040001F2 RID: 498
		BadName,
		// Token: 0x040001F3 RID: 499
		BadAlg,
		// Token: 0x040001F4 RID: 500
		BadTrunc
	}
}
