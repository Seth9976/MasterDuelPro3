using System;

namespace Ookii.Dialogs.Interop
{
	// Token: 0x0200005A RID: 90
	[Flags]
	internal enum ProgressDialogFlags : uint
	{
		// Token: 0x0400023A RID: 570
		Normal = 0U,
		// Token: 0x0400023B RID: 571
		Modal = 1U,
		// Token: 0x0400023C RID: 572
		AutoTime = 2U,
		// Token: 0x0400023D RID: 573
		NoTime = 4U,
		// Token: 0x0400023E RID: 574
		NoMinimize = 8U,
		// Token: 0x0400023F RID: 575
		NoProgressBar = 16U,
		// Token: 0x04000240 RID: 576
		MarqueeProgress = 32U,
		// Token: 0x04000241 RID: 577
		NoCancel = 64U
	}
}
