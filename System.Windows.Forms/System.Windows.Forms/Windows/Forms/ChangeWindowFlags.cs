using System;

namespace System.Windows.Forms
{
	// Token: 0x0200026A RID: 618
	[Flags]
	internal enum ChangeWindowFlags
	{
		// Token: 0x0400106A RID: 4202
		CWX = 1,
		// Token: 0x0400106B RID: 4203
		CWY = 2,
		// Token: 0x0400106C RID: 4204
		CWWidth = 4,
		// Token: 0x0400106D RID: 4205
		CWHeight = 8,
		// Token: 0x0400106E RID: 4206
		CWBorderWidth = 16,
		// Token: 0x0400106F RID: 4207
		CWSibling = 32,
		// Token: 0x04001070 RID: 4208
		CWStackMode = 64
	}
}
