using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Accessibility
{
	// Token: 0x0200000D RID: 13
	public static class AssistiveSupport
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000029E7 File Offset: 0x00000BE7
		// (set) Token: 0x06000054 RID: 84 RVA: 0x000029EE File Offset: 0x00000BEE
		public static bool isScreenReaderEnabled { get; private set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000055 RID: 85 RVA: 0x000029F6 File Offset: 0x00000BF6
		public static IAccessibilityNotificationDispatcher notificationDispatcher { get; } = new AssistiveSupport.NotificationDispatcher();

		// Token: 0x06000056 RID: 86 RVA: 0x000029FD File Offset: 0x00000BFD
		internal static void Initialize()
		{
			AssistiveSupport.isScreenReaderEnabled = AccessibilityManager.IsScreenReaderEnabled();
			AccessibilityManager.screenReaderStatusChanged += AssistiveSupport.ScreenReaderStatusChanged;
			AccessibilityManager.nodeFocusChanged += AssistiveSupport.NodeFocusChanged;
			AssistiveSupport.s_ServiceManager = new ServiceManager();
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002A3C File Offset: 0x00000C3C
		internal static T GetService<T>() where T : IService
		{
			bool flag = AssistiveSupport.s_ServiceManager == null;
			T t;
			if (flag)
			{
				t = default(T);
			}
			else
			{
				t = AssistiveSupport.s_ServiceManager.GetService<T>();
			}
			return t;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002A74 File Offset: 0x00000C74
		private static void ScreenReaderStatusChanged(bool screenReaderEnabled)
		{
			bool flag = AssistiveSupport.isScreenReaderEnabled == screenReaderEnabled;
			if (!flag)
			{
				AssistiveSupport.isScreenReaderEnabled = screenReaderEnabled;
				Action<bool> action = AssistiveSupport.screenReaderStatusChanged;
				if (action != null)
				{
					action(AssistiveSupport.isScreenReaderEnabled);
				}
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002AAE File Offset: 0x00000CAE
		private static void NodeFocusChanged(AccessibilityNode currentNode)
		{
			Action<AccessibilityNode> action = AssistiveSupport.nodeFocusChanged;
			if (action != null)
			{
				action(currentNode);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002AC3 File Offset: 0x00000CC3
		public static AccessibilityHierarchy activeHierarchy
		{
			get
			{
				AccessibilityHierarchyService service = AssistiveSupport.GetService<AccessibilityHierarchyService>();
				return (service != null) ? service.hierarchy : null;
			}
		}

		// Token: 0x04000045 RID: 69
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<AccessibilityNode> nodeFocusChanged;

		// Token: 0x04000046 RID: 70
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<bool> screenReaderStatusChanged;

		// Token: 0x04000049 RID: 73
		private static ServiceManager s_ServiceManager;

		// Token: 0x0200000E RID: 14
		internal class NotificationDispatcher : IAccessibilityNotificationDispatcher
		{
			// Token: 0x0600005C RID: 92 RVA: 0x00002AE2 File Offset: 0x00000CE2
			private static void Send(in AccessibilityNotificationContext context)
			{
				AccessibilityManager.SendAccessibilityNotification(in context);
			}

			// Token: 0x0600005D RID: 93 RVA: 0x00002AEC File Offset: 0x00000CEC
			public void SendScreenChanged(AccessibilityNode nodeToFocus = null)
			{
				AccessibilityNotificationContext notification = new AccessibilityNotificationContext
				{
					notification = AccessibilityNotification.ScreenChanged,
					nextNodeId = ((nodeToFocus == null) ? (-1) : nodeToFocus.id)
				};
				AssistiveSupport.NotificationDispatcher.Send(in notification);
			}
		}
	}
}
