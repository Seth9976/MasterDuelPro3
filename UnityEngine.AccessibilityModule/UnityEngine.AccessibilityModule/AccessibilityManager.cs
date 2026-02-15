using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Pool;
using UnityEngine.Scripting;

namespace UnityEngine.Accessibility
{
	// Token: 0x02000003 RID: 3
	[VisibleToOtherModules(new string[] { "UnityEditor.AccessibilityModule" })]
	[NativeHeader("Modules/Accessibility/Native/AccessibilityManager.h")]
	internal static class AccessibilityManager
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000006 RID: 6 RVA: 0x000020D0 File Offset: 0x000002D0
		// (remove) Token: 0x06000007 RID: 7 RVA: 0x00002104 File Offset: 0x00000304
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<bool> screenReaderStatusChanged;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000008 RID: 8 RVA: 0x00002138 File Offset: 0x00000338
		// (remove) Token: 0x06000009 RID: 9 RVA: 0x0000216C File Offset: 0x0000036C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<AccessibilityNode> nodeFocusChanged;

		// Token: 0x0600000A RID: 10
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsScreenReaderEnabled();

		// Token: 0x0600000B RID: 11
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SendAccessibilityNotification(in AccessibilityNotificationContext context);

		// Token: 0x0600000C RID: 12 RVA: 0x0000219F File Offset: 0x0000039F
		[RequiredByNativeCode]
		[VisibleToOtherModules(new string[] { "UnityEditor.AccessibilityModule" })]
		internal static void Internal_Initialize()
		{
			AssistiveSupport.Initialize();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021A8 File Offset: 0x000003A8
		[RequiredByNativeCode]
		private static void Internal_Update()
		{
			bool flag = AccessibilityManager.s_AsyncNotificationContexts.Count == 0;
			if (!flag)
			{
				Queue<AccessibilityManager.NotificationContext> queue = AccessibilityManager.s_AsyncNotificationContexts;
				AccessibilityManager.NotificationContext[] contexts;
				lock (queue)
				{
					bool flag3 = AccessibilityManager.s_AsyncNotificationContexts.Count == 0;
					if (flag3)
					{
						return;
					}
					contexts = AccessibilityManager.s_AsyncNotificationContexts.ToArray();
					AccessibilityManager.s_AsyncNotificationContexts.Clear();
				}
				using (AccessibilityManager.GetExclusiveLock())
				{
					foreach (AccessibilityManager.NotificationContext context in contexts)
					{
						switch (context.notification)
						{
						case AccessibilityNotification.ScreenReaderStatusChanged:
						{
							Action<bool> action = AccessibilityManager.screenReaderStatusChanged;
							if (action != null)
							{
								action(context.isScreenReaderEnabled);
							}
							break;
						}
						case AccessibilityNotification.ElementFocused:
						{
							context.currentNode.InvokeFocusChanged(true);
							Action<AccessibilityNode> action2 = AccessibilityManager.nodeFocusChanged;
							if (action2 != null)
							{
								action2(context.currentNode);
							}
							break;
						}
						case AccessibilityNotification.ElementUnfocused:
							context.currentNode.InvokeFocusChanged(false);
							break;
						case AccessibilityNotification.FontScaleChanged:
							AccessibilitySettings.InvokeFontScaleChanged(context.fontScale);
							break;
						case AccessibilityNotification.BoldTextStatusChanged:
							AccessibilitySettings.InvokeBoldTextStatusChanged(context.isBoldTextEnabled);
							break;
						case AccessibilityNotification.ClosedCaptioningStatusChanged:
							AccessibilitySettings.InvokeClosedCaptionStatusChanged(context.isClosedCaptioningEnabled);
							break;
						}
					}
				}
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000233C File Offset: 0x0000053C
		[RequiredByNativeCode]
		private static int[] Internal_GetRootNodeIds()
		{
			AccessibilityHierarchyService service = AssistiveSupport.GetService<AccessibilityHierarchyService>();
			List<AccessibilityNode> rootNodes = ((service != null) ? service.GetRootNodes() : null);
			bool flag = rootNodes == null || rootNodes.Count == 0;
			int[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				List<int> rootNodeIds;
				using (CollectionPool<List<int>, int>.Get(out rootNodeIds))
				{
					for (int i = 0; i < rootNodes.Count; i++)
					{
						rootNodeIds.Add(rootNodes[i].id);
					}
					bool flag2 = rootNodeIds.Count == 0;
					if (flag2)
					{
						array = null;
					}
					else
					{
						array = rootNodeIds.ToArray();
					}
				}
			}
			return array;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000023EC File Offset: 0x000005EC
		[RequiredByNativeCode]
		internal static bool Internal_GetNode(int id, ref AccessibilityNodeData nodeData)
		{
			AccessibilityHierarchyService service = AssistiveSupport.GetService<AccessibilityHierarchyService>();
			bool flag = service == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				AccessibilityNode node;
				bool flag3 = service.TryGetNode(id, out node);
				if (flag3)
				{
					node.GetNodeData(ref nodeData);
					flag2 = true;
				}
				else
				{
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002430 File Offset: 0x00000630
		[RequiredByNativeCode]
		private static int Internal_GetNodeIdAt(float x, float y)
		{
			AccessibilityHierarchyService service = AssistiveSupport.GetService<AccessibilityHierarchyService>();
			bool flag = service == null;
			int num;
			if (flag)
			{
				num = -1;
			}
			else
			{
				List<AccessibilityNode> rootNodes = service.GetRootNodes();
				bool flag2 = rootNodes.Count == 0;
				if (flag2)
				{
					num = -1;
				}
				else
				{
					AccessibilityNode node;
					bool flag3 = service.TryGetNodeAt(x, y, out node);
					if (flag3)
					{
						num = node.id;
					}
					else
					{
						num = -1;
					}
				}
			}
			return num;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002490 File Offset: 0x00000690
		[RequiredByNativeCode]
		private static void Internal_OnAccessibilityNotificationReceived(ref AccessibilityNotificationContext context)
		{
			bool flag = context.notification == AccessibilityNotification.ElementFocused;
			if (!flag)
			{
				AccessibilityManager.QueueNotification(new AccessibilityManager.NotificationContext(ref context));
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000024BC File Offset: 0x000006BC
		internal static void QueueNotification(AccessibilityManager.NotificationContext notification)
		{
			Queue<AccessibilityManager.NotificationContext> queue = AccessibilityManager.s_AsyncNotificationContexts;
			lock (queue)
			{
				AccessibilityManager.s_AsyncNotificationContexts.Enqueue(notification);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002508 File Offset: 0x00000708
		internal static IDisposable GetExclusiveLock()
		{
			return new AccessibilityManager.ExclusiveLock();
		}

		// Token: 0x06000014 RID: 20
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Lock();

		// Token: 0x06000015 RID: 21
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Unlock();

		// Token: 0x04000003 RID: 3
		private static Queue<AccessibilityManager.NotificationContext> s_AsyncNotificationContexts = new Queue<AccessibilityManager.NotificationContext>();

		// Token: 0x02000004 RID: 4
		public struct NotificationContext
		{
			// Token: 0x17000002 RID: 2
			// (get) Token: 0x06000017 RID: 23 RVA: 0x0000252B File Offset: 0x0000072B
			// (set) Token: 0x06000018 RID: 24 RVA: 0x00002533 File Offset: 0x00000733
			public AccessibilityNotification notification { readonly get; set; }

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x06000019 RID: 25 RVA: 0x0000253C File Offset: 0x0000073C
			// (set) Token: 0x0600001A RID: 26 RVA: 0x00002544 File Offset: 0x00000744
			public bool isScreenReaderEnabled { readonly get; set; }

			// Token: 0x17000004 RID: 4
			// (set) Token: 0x0600001B RID: 27 RVA: 0x0000254D File Offset: 0x0000074D
			public string announcement
			{
				[CompilerGenerated]
				set
				{
					this.<announcement>k__BackingField = value;
				}
			}

			// Token: 0x17000005 RID: 5
			// (set) Token: 0x0600001C RID: 28 RVA: 0x00002556 File Offset: 0x00000756
			public bool wasAnnouncementSuccessful
			{
				[CompilerGenerated]
				set
				{
					this.<wasAnnouncementSuccessful>k__BackingField = value;
				}
			}

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x0600001D RID: 29 RVA: 0x0000255F File Offset: 0x0000075F
			// (set) Token: 0x0600001E RID: 30 RVA: 0x00002567 File Offset: 0x00000767
			public AccessibilityNode currentNode { readonly get; set; }

			// Token: 0x17000007 RID: 7
			// (set) Token: 0x0600001F RID: 31 RVA: 0x00002570 File Offset: 0x00000770
			public AccessibilityNode nextNode
			{
				[CompilerGenerated]
				set
				{
					this.<nextNode>k__BackingField = value;
				}
			}

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x06000020 RID: 32 RVA: 0x00002579 File Offset: 0x00000779
			// (set) Token: 0x06000021 RID: 33 RVA: 0x00002581 File Offset: 0x00000781
			public float fontScale { readonly get; set; }

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x06000022 RID: 34 RVA: 0x0000258A File Offset: 0x0000078A
			// (set) Token: 0x06000023 RID: 35 RVA: 0x00002592 File Offset: 0x00000792
			public bool isBoldTextEnabled { readonly get; set; }

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x06000024 RID: 36 RVA: 0x0000259B File Offset: 0x0000079B
			// (set) Token: 0x06000025 RID: 37 RVA: 0x000025A3 File Offset: 0x000007A3
			public bool isClosedCaptioningEnabled { readonly get; set; }

			// Token: 0x1700000B RID: 11
			// (set) Token: 0x06000026 RID: 38 RVA: 0x000025AC File Offset: 0x000007AC
			public AccessibilityNotificationContext nativeContext
			{
				[CompilerGenerated]
				set
				{
					this.<nativeContext>k__BackingField = value;
				}
			}

			// Token: 0x06000027 RID: 39 RVA: 0x000025B8 File Offset: 0x000007B8
			public NotificationContext(ref AccessibilityNotificationContext nativeNotification)
			{
				this.nativeContext = nativeNotification;
				this.notification = nativeNotification.notification;
				this.isScreenReaderEnabled = nativeNotification.isScreenReaderEnabled;
				this.announcement = nativeNotification.announcement;
				this.wasAnnouncementSuccessful = nativeNotification.wasAnnouncementSuccessful;
				AccessibilityNode node = null;
				AccessibilityHierarchy activeHierarchy = AssistiveSupport.activeHierarchy;
				if (activeHierarchy != null)
				{
					activeHierarchy.TryGetNode(nativeNotification.currentNodeId, out node);
				}
				this.currentNode = node;
				AccessibilityHierarchy activeHierarchy2 = AssistiveSupport.activeHierarchy;
				if (activeHierarchy2 != null)
				{
					activeHierarchy2.TryGetNode(nativeNotification.nextNodeId, out node);
				}
				this.nextNode = node;
				this.fontScale = 1f;
				this.isBoldTextEnabled = false;
				this.isClosedCaptioningEnabled = false;
			}
		}

		// Token: 0x02000005 RID: 5
		private sealed class ExclusiveLock : IDisposable
		{
			// Token: 0x06000028 RID: 40 RVA: 0x00002667 File Offset: 0x00000867
			public ExclusiveLock()
			{
				AccessibilityManager.Lock();
			}

			// Token: 0x06000029 RID: 41 RVA: 0x00002678 File Offset: 0x00000878
			~ExclusiveLock()
			{
				this.InternalDispose();
			}

			// Token: 0x0600002A RID: 42 RVA: 0x000026A8 File Offset: 0x000008A8
			private void InternalDispose()
			{
				bool flag = !this.m_Disposed;
				if (flag)
				{
					AccessibilityManager.Unlock();
					this.m_Disposed = true;
				}
			}

			// Token: 0x0600002B RID: 43 RVA: 0x000026D2 File Offset: 0x000008D2
			public void Dispose()
			{
				this.InternalDispose();
				GC.SuppressFinalize(this);
			}

			// Token: 0x04000010 RID: 16
			private bool m_Disposed;
		}
	}
}
