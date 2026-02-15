using System;

namespace Newtonsoft.Json
{
	// Token: 0x0200008E RID: 142
	public enum WriteState
	{
		// Token: 0x04000359 RID: 857
		Error,
		// Token: 0x0400035A RID: 858
		Closed,
		// Token: 0x0400035B RID: 859
		Object,
		// Token: 0x0400035C RID: 860
		Array,
		// Token: 0x0400035D RID: 861
		Constructor,
		// Token: 0x0400035E RID: 862
		Property,
		// Token: 0x0400035F RID: 863
		Start
	}
}
