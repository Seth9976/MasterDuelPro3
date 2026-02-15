using System;
using System.Collections.Generic;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000461 RID: 1121
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal static class UIEventRegistration
	{
		// Token: 0x06002111 RID: 8465 RVA: 0x00079768 File Offset: 0x00077968
		static UIEventRegistration()
		{
			GUIUtility.takeCapture = (Action)Delegate.Combine(GUIUtility.takeCapture, new Action(delegate
			{
				UIEventRegistration.TakeCapture();
			}));
			GUIUtility.releaseCapture = (Action)Delegate.Combine(GUIUtility.releaseCapture, new Action(delegate
			{
				UIEventRegistration.ReleaseCapture();
			}));
			GUIUtility.processEvent = (Func<int, IntPtr, bool>)Delegate.Combine(GUIUtility.processEvent, new Func<int, IntPtr, bool>((int i, IntPtr ptr) => UIEventRegistration.ProcessEvent(i, ptr)));
			GUIUtility.cleanupRoots = (Action)Delegate.Combine(GUIUtility.cleanupRoots, new Action(delegate
			{
				UIEventRegistration.CleanupRoots();
			}));
			GUIUtility.endContainerGUIFromException = (Func<Exception, bool>)Delegate.Combine(GUIUtility.endContainerGUIFromException, new Func<Exception, bool>((Exception exception) => UIEventRegistration.EndContainerGUIFromException(exception)));
			GUIUtility.guiChanged = (Action)Delegate.Combine(GUIUtility.guiChanged, new Action(delegate
			{
				UIEventRegistration.MakeCurrentIMGUIContainerDirty();
			}));
		}

		// Token: 0x06002112 RID: 8466 RVA: 0x00079858 File Offset: 0x00077A58
		internal static void RegisterUIElementSystem(IUIElementsUtility utility)
		{
			UIEventRegistration.s_Utilities.Insert(0, utility);
		}

		// Token: 0x06002113 RID: 8467 RVA: 0x00079868 File Offset: 0x00077A68
		private static void TakeCapture()
		{
			foreach (IUIElementsUtility uiElementsUtility in UIEventRegistration.s_Utilities)
			{
				bool flag = uiElementsUtility.TakeCapture();
				if (flag)
				{
					break;
				}
			}
		}

		// Token: 0x06002114 RID: 8468 RVA: 0x000798C4 File Offset: 0x00077AC4
		private static void ReleaseCapture()
		{
			foreach (IUIElementsUtility uiElementsUtility in UIEventRegistration.s_Utilities)
			{
				bool flag = uiElementsUtility.ReleaseCapture();
				if (flag)
				{
					break;
				}
			}
		}

		// Token: 0x06002115 RID: 8469 RVA: 0x00079920 File Offset: 0x00077B20
		private static bool EndContainerGUIFromException(Exception exception)
		{
			foreach (IUIElementsUtility uiElementsUtility in UIEventRegistration.s_Utilities)
			{
				bool flag = uiElementsUtility.EndContainerGUIFromException(exception);
				if (flag)
				{
					return true;
				}
			}
			return GUIUtility.ShouldRethrowException(exception);
		}

		// Token: 0x06002116 RID: 8470 RVA: 0x0007998C File Offset: 0x00077B8C
		private static bool ProcessEvent(int instanceID, IntPtr nativeEventPtr)
		{
			bool eventHandled = false;
			foreach (IUIElementsUtility uiElementsUtility in UIEventRegistration.s_Utilities)
			{
				bool flag = uiElementsUtility.ProcessEvent(instanceID, nativeEventPtr, ref eventHandled);
				if (flag)
				{
					return eventHandled;
				}
			}
			return false;
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x000799FC File Offset: 0x00077BFC
		private static void CleanupRoots()
		{
			foreach (IUIElementsUtility uiElementsUtility in UIEventRegistration.s_Utilities)
			{
				bool flag = uiElementsUtility.CleanupRoots();
				if (flag)
				{
					break;
				}
			}
		}

		// Token: 0x06002118 RID: 8472 RVA: 0x00079A58 File Offset: 0x00077C58
		internal static void MakeCurrentIMGUIContainerDirty()
		{
			foreach (IUIElementsUtility uiElementsUtility in UIEventRegistration.s_Utilities)
			{
				bool flag = uiElementsUtility.MakeCurrentIMGUIContainerDirty();
				if (flag)
				{
					break;
				}
			}
		}

		// Token: 0x04000E9F RID: 3743
		private static List<IUIElementsUtility> s_Utilities = new List<IUIElementsUtility>();
	}
}
