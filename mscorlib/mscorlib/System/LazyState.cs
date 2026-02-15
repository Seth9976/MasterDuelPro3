using System;

namespace System
{
	// Token: 0x02000115 RID: 277
	internal enum LazyState
	{
		// Token: 0x04000431 RID: 1073
		NoneViaConstructor,
		// Token: 0x04000432 RID: 1074
		NoneViaFactory,
		// Token: 0x04000433 RID: 1075
		NoneException,
		// Token: 0x04000434 RID: 1076
		PublicationOnlyViaConstructor,
		// Token: 0x04000435 RID: 1077
		PublicationOnlyViaFactory,
		// Token: 0x04000436 RID: 1078
		PublicationOnlyWait,
		// Token: 0x04000437 RID: 1079
		PublicationOnlyException,
		// Token: 0x04000438 RID: 1080
		ExecutionAndPublicationViaConstructor,
		// Token: 0x04000439 RID: 1081
		ExecutionAndPublicationViaFactory,
		// Token: 0x0400043A RID: 1082
		ExecutionAndPublicationException
	}
}
