using System;

namespace System.Windows.Forms
{
	// Token: 0x02000271 RID: 625
	[Flags]
	internal enum MotifFunctions
	{
		// Token: 0x04001096 RID: 4246
		All = 1,
		// Token: 0x04001097 RID: 4247
		Resize = 2,
		// Token: 0x04001098 RID: 4248
		Move = 4,
		// Token: 0x04001099 RID: 4249
		Minimize = 8,
		// Token: 0x0400109A RID: 4250
		Maximize = 16,
		// Token: 0x0400109B RID: 4251
		Close = 32
	}
}
