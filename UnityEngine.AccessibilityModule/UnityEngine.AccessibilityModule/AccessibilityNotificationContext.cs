using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Accessibility
{
	// Token: 0x0200000B RID: 11
	[RequiredByNativeCode]
	[NativeType(CodegenOptions.Custom, "MonoAccessibilityNotificationContext")]
	[NativeHeader("Modules/Accessibility/Native/AccessibilityNotificationContext.h")]
	[NativeHeader("Modules/Accessibility/Bindings/AccessibilityNotificationContext.bindings.h")]
	internal struct AccessibilityNotificationContext
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000028D6 File Offset: 0x00000AD6
		// (set) Token: 0x06000046 RID: 70 RVA: 0x000028DE File Offset: 0x00000ADE
		public AccessibilityNotification notification { readonly get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000028E7 File Offset: 0x00000AE7
		public readonly bool isScreenReaderEnabled { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000028EF File Offset: 0x00000AEF
		public readonly string announcement { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000028F7 File Offset: 0x00000AF7
		public readonly bool wasAnnouncementSuccessful { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000028FF File Offset: 0x00000AFF
		public readonly int currentNodeId { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002907 File Offset: 0x00000B07
		// (set) Token: 0x0600004C RID: 76 RVA: 0x0000290F File Offset: 0x00000B0F
		public int nextNodeId { readonly get; set; }
	}
}
