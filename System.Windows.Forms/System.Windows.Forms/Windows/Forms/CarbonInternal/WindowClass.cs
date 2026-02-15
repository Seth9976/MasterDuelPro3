using System;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x0200039E RID: 926
	internal enum WindowClass : uint
	{
		// Token: 0x04001CF5 RID: 7413
		kAlertWindowClass = 1U,
		// Token: 0x04001CF6 RID: 7414
		kMovableAlertWindowClass,
		// Token: 0x04001CF7 RID: 7415
		kModalWindowClass,
		// Token: 0x04001CF8 RID: 7416
		kMovableModalWindowClass,
		// Token: 0x04001CF9 RID: 7417
		kFloatingWindowClass,
		// Token: 0x04001CFA RID: 7418
		kDocumentWindowClass,
		// Token: 0x04001CFB RID: 7419
		kUtilityWindowClass = 8U,
		// Token: 0x04001CFC RID: 7420
		kHelpWindowClass = 10U,
		// Token: 0x04001CFD RID: 7421
		kSheetWindowClass,
		// Token: 0x04001CFE RID: 7422
		kToolbarWindowClass,
		// Token: 0x04001CFF RID: 7423
		kPlainWindowClass,
		// Token: 0x04001D00 RID: 7424
		kOverlayWindowClass,
		// Token: 0x04001D01 RID: 7425
		kSheetAlertWindowClass,
		// Token: 0x04001D02 RID: 7426
		kAltPlainWindowClass,
		// Token: 0x04001D03 RID: 7427
		kDrawerWindowClass = 20U,
		// Token: 0x04001D04 RID: 7428
		kAllWindowClasses = 4294967295U
	}
}
