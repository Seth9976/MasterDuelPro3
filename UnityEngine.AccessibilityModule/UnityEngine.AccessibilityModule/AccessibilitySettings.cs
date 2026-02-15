using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Accessibility
{
	// Token: 0x0200000C RID: 12
	[NativeHeader("Modules/Accessibility/Native/AccessibilitySettings.h")]
	public static class AccessibilitySettings
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00002918 File Offset: 0x00000B18
		[RequiredByNativeCode]
		private static void Internal_OnFontScaleChanged(float newFontScale)
		{
			AccessibilityManager.QueueNotification(new AccessibilityManager.NotificationContext
			{
				notification = AccessibilityNotification.FontScaleChanged,
				fontScale = newFontScale
			});
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002948 File Offset: 0x00000B48
		[RequiredByNativeCode]
		private static void Internal_OnBoldTextStatusChanged(bool enabled)
		{
			AccessibilityManager.QueueNotification(new AccessibilityManager.NotificationContext
			{
				notification = AccessibilityNotification.BoldTextStatusChanged,
				isBoldTextEnabled = enabled
			});
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002978 File Offset: 0x00000B78
		[RequiredByNativeCode]
		private static void Internal_OnClosedCaptioningStatusChanged(bool enabled)
		{
			AccessibilityManager.QueueNotification(new AccessibilityManager.NotificationContext
			{
				notification = AccessibilityNotification.ClosedCaptioningStatusChanged,
				isClosedCaptioningEnabled = enabled
			});
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000029A8 File Offset: 0x00000BA8
		internal static void InvokeFontScaleChanged(float newFontScale)
		{
			Action<float> action = AccessibilitySettings.fontScaleChanged;
			if (action != null)
			{
				action(newFontScale);
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000029BD File Offset: 0x00000BBD
		internal static void InvokeBoldTextStatusChanged(bool enabled)
		{
			Action<bool> action = AccessibilitySettings.boldTextStatusChanged;
			if (action != null)
			{
				action(enabled);
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000029D2 File Offset: 0x00000BD2
		internal static void InvokeClosedCaptionStatusChanged(bool enabled)
		{
			Action<bool> action = AccessibilitySettings.closedCaptioningStatusChanged;
			if (action != null)
			{
				action(enabled);
			}
		}

		// Token: 0x04000042 RID: 66
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action<float> fontScaleChanged;

		// Token: 0x04000043 RID: 67
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action<bool> boldTextStatusChanged;

		// Token: 0x04000044 RID: 68
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action<bool> closedCaptioningStatusChanged;
	}
}
