using System;
using UnityEngine.Bindings;

namespace UnityEngine.Accessibility
{
	// Token: 0x0200000A RID: 10
	[NativeHeader("Modules/Accessibility/Native/AccessibilityNotificationContext.h")]
	internal enum AccessibilityNotification
	{
		// Token: 0x04000030 RID: 48
		None,
		// Token: 0x04000031 RID: 49
		Announcement,
		// Token: 0x04000032 RID: 50
		AnnouncementFinished,
		// Token: 0x04000033 RID: 51
		ScreenReaderStatusChanged,
		// Token: 0x04000034 RID: 52
		ScreenChanged,
		// Token: 0x04000035 RID: 53
		LayoutChanged,
		// Token: 0x04000036 RID: 54
		PageScrolled,
		// Token: 0x04000037 RID: 55
		ElementFocused,
		// Token: 0x04000038 RID: 56
		ElementUnfocused,
		// Token: 0x04000039 RID: 57
		FontScaleChanged,
		// Token: 0x0400003A RID: 58
		BoldTextStatusChanged,
		// Token: 0x0400003B RID: 59
		ClosedCaptioningStatusChanged
	}
}
