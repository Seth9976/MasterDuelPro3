using System;

namespace Mono.Net.Dns
{
	// Token: 0x02000094 RID: 148
	internal enum ResolverError
	{
		// Token: 0x04000250 RID: 592
		NoError,
		// Token: 0x04000251 RID: 593
		FormatError,
		// Token: 0x04000252 RID: 594
		ServerFailure,
		// Token: 0x04000253 RID: 595
		NameError,
		// Token: 0x04000254 RID: 596
		NotImplemented,
		// Token: 0x04000255 RID: 597
		Refused,
		// Token: 0x04000256 RID: 598
		ResponseHeaderError,
		// Token: 0x04000257 RID: 599
		ResponseFormatError,
		// Token: 0x04000258 RID: 600
		Timeout
	}
}
