using System;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000463 RID: 1123
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal class UIElementsUtility : IUIElementsUtility
	{
		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06002121 RID: 8481 RVA: 0x00079B04 File Offset: 0x00077D04
		public static bool isOSXContextualMenuPlatform
		{
			get
			{
				RuntimePlatform platform = Application.platform;
				return platform == RuntimePlatform.OSXEditor || platform == RuntimePlatform.OSXPlayer || UIElementsUtility.s_EnableOSXContextualMenuEventsOnNonOSXPlatforms;
			}
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x00079B2B File Offset: 0x00077D2B
		private UIElementsUtility()
		{
			UIEventRegistration.RegisterUIElementSystem(this);
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x00079B3C File Offset: 0x00077D3C
		bool IUIElementsUtility.MakeCurrentIMGUIContainerDirty()
		{
			bool flag = UIElementsUtility.s_ContainerStack.Count > 0;
			bool flag2;
			if (flag)
			{
				UIElementsUtility.s_ContainerStack.Peek().MarkDirtyLayout();
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x00079B78 File Offset: 0x00077D78
		bool IUIElementsUtility.TakeCapture()
		{
			bool flag = UIElementsUtility.s_ContainerStack.Count > 0;
			bool flag2;
			if (flag)
			{
				IMGUIContainer topmostContainer = UIElementsUtility.s_ContainerStack.Peek();
				topmostContainer.CaptureMouse();
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x00079BB4 File Offset: 0x00077DB4
		bool IUIElementsUtility.ReleaseCapture()
		{
			return false;
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x00079BC8 File Offset: 0x00077DC8
		bool IUIElementsUtility.ProcessEvent(int instanceID, IntPtr nativeEventPtr, ref bool eventHandled)
		{
			Panel panel;
			bool flag = nativeEventPtr != IntPtr.Zero && UIElementsUtility.s_UIElementsCache.TryGetValue(instanceID, out panel);
			bool flag3;
			if (flag)
			{
				bool flag2 = panel.contextType == ContextType.Editor;
				if (flag2)
				{
					UIElementsUtility.s_EventInstance.CopyFromPtr(nativeEventPtr);
					eventHandled = UIElementsUtility.DoDispatch(panel);
				}
				flag3 = true;
			}
			else
			{
				flag3 = false;
			}
			return flag3;
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x00079C24 File Offset: 0x00077E24
		bool IUIElementsUtility.CleanupRoots()
		{
			UIElementsUtility.s_EventInstance = null;
			UIElementsUtility.s_UIElementsCache = null;
			UIElementsUtility.s_ContainerStack = null;
			return false;
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x00079C4C File Offset: 0x00077E4C
		bool IUIElementsUtility.EndContainerGUIFromException(Exception exception)
		{
			bool flag = UIElementsUtility.s_ContainerStack.Count > 0;
			if (flag)
			{
				GUIUtility.EndContainer();
				UIElementsUtility.s_ContainerStack.Pop();
			}
			return false;
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x00079C83 File Offset: 0x00077E83
		public static void RegisterCachedPanel(int instanceID, Panel panel)
		{
			UIElementsUtility.s_UIElementsCache.Add(instanceID, panel);
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x00079C93 File Offset: 0x00077E93
		public static void RemoveCachedPanel(int instanceID)
		{
			UIElementsUtility.s_UIElementsCache.Remove(instanceID);
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x00079CA4 File Offset: 0x00077EA4
		public static bool TryGetPanel(int instanceID, out Panel panel)
		{
			return UIElementsUtility.s_UIElementsCache.TryGetValue(instanceID, out panel);
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x00079CC4 File Offset: 0x00077EC4
		internal static void BeginContainerGUI(GUILayoutUtility.LayoutCache cache, Event evt, IMGUIContainer container)
		{
			bool useOwnerObjectGUIState = container.useOwnerObjectGUIState;
			if (useOwnerObjectGUIState)
			{
				GUIUtility.BeginContainerFromOwner(container.elementPanel.ownerObject);
			}
			else
			{
				GUIUtility.BeginContainer(container.guiState);
			}
			UIElementsUtility.s_ContainerStack.Push(container);
			GUIUtility.s_SkinMode = (int)container.contextType;
			GUIUtility.s_OriginalID = container.elementPanel.ownerObject.GetInstanceID();
			bool flag = Event.current == null;
			if (flag)
			{
				Event.current = evt;
			}
			else
			{
				Event.current.CopyFrom(evt);
			}
			GUI.enabled = container.enabledInHierarchy;
			GUILayoutUtility.BeginContainer(cache);
			GUIUtility.ResetGlobalState();
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x00079D6C File Offset: 0x00077F6C
		internal static void EndContainerGUI(Event evt, Rect layoutSize)
		{
			bool flag = Event.current.type == EventType.Layout && UIElementsUtility.s_ContainerStack.Count > 0;
			if (flag)
			{
				GUILayoutUtility.LayoutFromContainer(layoutSize.width, layoutSize.height);
			}
			GUILayoutUtility.SelectIDList(GUIUtility.s_OriginalID, false);
			GUIContent.ClearStaticCache();
			bool flag2 = UIElementsUtility.s_ContainerStack.Count > 0;
			if (flag2)
			{
			}
			evt.CopyFrom(Event.current);
			bool flag3 = UIElementsUtility.s_ContainerStack.Count > 0;
			if (flag3)
			{
				GUIUtility.EndContainer();
				UIElementsUtility.s_ContainerStack.Pop();
			}
		}

		// Token: 0x0600212E RID: 8494 RVA: 0x00079E08 File Offset: 0x00078008
		internal static EventBase CreateEvent(Event systemEvent)
		{
			return UIElementsUtility.CreateEvent(systemEvent, systemEvent.rawType);
		}

		// Token: 0x0600212F RID: 8495 RVA: 0x00079E28 File Offset: 0x00078028
		internal static EventBase CreateEvent(Event systemEvent, EventType eventType)
		{
			switch (eventType)
			{
			case EventType.MouseDown:
				goto IL_0097;
			case EventType.MouseUp:
				goto IL_00C2;
			case EventType.MouseMove:
				break;
			case EventType.MouseDrag:
				return PointerEventBase<PointerMoveEvent>.GetPooled(systemEvent);
			case EventType.KeyDown:
				return KeyboardEventBase<KeyDownEvent>.GetPooled(systemEvent);
			case EventType.KeyUp:
				return KeyboardEventBase<KeyUpEvent>.GetPooled(systemEvent);
			case EventType.ScrollWheel:
				return WheelEvent.GetPooled(systemEvent);
			case EventType.Repaint:
			case EventType.Layout:
			case EventType.DragUpdated:
			case EventType.DragPerform:
			case EventType.Ignore:
			case EventType.Used:
			case EventType.DragExited:
			case (EventType)17:
			case (EventType)18:
			case (EventType)19:
				goto IL_0134;
			case EventType.ValidateCommand:
				return CommandEventBase<ValidateCommandEvent>.GetPooled(systemEvent);
			case EventType.ExecuteCommand:
				return CommandEventBase<ExecuteCommandEvent>.GetPooled(systemEvent);
			case EventType.ContextClick:
				return MouseEventBase<ContextClickEvent>.GetPooled(systemEvent);
			case EventType.MouseEnterWindow:
				return MouseEventBase<MouseEnterWindowEvent>.GetPooled(systemEvent);
			case EventType.MouseLeaveWindow:
				return MouseLeaveWindowEvent.GetPooled(systemEvent);
			default:
				switch (eventType)
				{
				case EventType.TouchDown:
					goto IL_0097;
				case EventType.TouchUp:
					goto IL_00C2;
				case EventType.TouchMove:
					break;
				default:
					goto IL_0134;
				}
				break;
			}
			return PointerEventBase<PointerMoveEvent>.GetPooled(systemEvent);
			IL_0097:
			bool flag = PointerDeviceState.HasAdditionalPressedButtons(PointerId.mousePointerId, systemEvent.button);
			if (flag)
			{
				return PointerEventBase<PointerMoveEvent>.GetPooled(systemEvent);
			}
			return PointerEventBase<PointerDownEvent>.GetPooled(systemEvent);
			IL_00C2:
			bool flag2 = PointerDeviceState.HasAdditionalPressedButtons(PointerId.mousePointerId, systemEvent.button);
			if (flag2)
			{
				return PointerEventBase<PointerMoveEvent>.GetPooled(systemEvent);
			}
			return PointerEventBase<PointerUpEvent>.GetPooled(systemEvent);
			IL_0134:
			return IMGUIEvent.GetPooled(systemEvent);
		}

		// Token: 0x06002130 RID: 8496 RVA: 0x00079F74 File Offset: 0x00078174
		private static bool DoDispatch(BaseVisualElementPanel panel)
		{
			Debug.Assert(panel.contextType == ContextType.Editor, "panel.contextType == ContextType.Editor");
			bool usesEvent = false;
			bool flag = UIElementsUtility.s_EventInstance.type == EventType.Repaint;
			if (flag)
			{
				Camera oldCam = Camera.current;
				RenderTexture oldRT = RenderTexture.active;
				Camera.SetupCurrent(null);
				RenderTexture.active = null;
				using (UIElementsUtility.s_RepaintProfilerMarker.Auto())
				{
					panel.Repaint(UIElementsUtility.s_EventInstance);
					panel.Render();
				}
				usesEvent = panel.IMGUIContainersCount > 0;
				Camera.SetupCurrent(oldCam);
				RenderTexture.active = oldRT;
			}
			else
			{
				panel.ValidateLayout();
				using (EventBase evt = UIElementsUtility.CreateEvent(UIElementsUtility.s_EventInstance))
				{
					bool immediate = UIElementsUtility.s_EventInstance.type == EventType.Used || UIElementsUtility.s_EventInstance.type == EventType.Layout || UIElementsUtility.s_EventInstance.type == EventType.ExecuteCommand || UIElementsUtility.s_EventInstance.type == EventType.ValidateCommand;
					using (UIElementsUtility.s_EventProfilerMarker.Auto())
					{
						panel.SendEvent(evt, immediate ? DispatchMode.Immediate : DispatchMode.Default);
					}
					bool isPropagationStopped = evt.isPropagationStopped;
					if (isPropagationStopped)
					{
						panel.visualTree.IncrementVersion(VersionChangeType.Repaint);
						usesEvent = true;
					}
				}
			}
			return usesEvent;
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x0007A100 File Offset: 0x00078300
		internal static void GetAllPanels(List<Panel> panels, ContextType contextType)
		{
			Dictionary<int, Panel>.Enumerator iterator = UIElementsUtility.GetPanelsIterator();
			while (iterator.MoveNext())
			{
				KeyValuePair<int, Panel> keyValuePair = iterator.Current;
				bool flag = keyValuePair.Value.contextType == contextType;
				if (flag)
				{
					keyValuePair = iterator.Current;
					panels.Add(keyValuePair.Value);
				}
			}
		}

		// Token: 0x06002132 RID: 8498 RVA: 0x0007A158 File Offset: 0x00078358
		internal static Dictionary<int, Panel>.Enumerator GetPanelsIterator()
		{
			return UIElementsUtility.s_UIElementsCache.GetEnumerator();
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x0007A174 File Offset: 0x00078374
		internal static float PixelsPerUnitScaleForElement(VisualElement ve, Sprite sprite)
		{
			bool flag = ve == null || ve.elementPanel == null || sprite == null;
			float num;
			if (flag)
			{
				num = 1f;
			}
			else
			{
				float referencePixelsPerUnit = ve.elementPanel.referenceSpritePixelsPerUnit;
				float pixelsPerUnit = sprite.pixelsPerUnit;
				pixelsPerUnit = Mathf.Max(0.01f, pixelsPerUnit);
				num = referencePixelsPerUnit / pixelsPerUnit;
			}
			return num;
		}

		// Token: 0x06002134 RID: 8500 RVA: 0x0007A1CC File Offset: 0x000783CC
		internal static string ParseMenuName(string menuName)
		{
			bool flag = string.IsNullOrEmpty(menuName);
			string text;
			if (flag)
			{
				text = string.Empty;
			}
			else
			{
				string displayValue = menuName.TrimEnd();
				int separatorPos = displayValue.LastIndexOf(' ');
				bool flag2 = separatorPos > -1;
				if (flag2)
				{
					int modifierPos = Array.IndexOf<char>(UIElementsUtility.s_Modifiers, displayValue[separatorPos + 1]);
					bool flag3 = displayValue.Length > separatorPos + 1 && modifierPos > -1;
					if (flag3)
					{
						displayValue = displayValue.Substring(0, separatorPos).TrimEnd();
					}
				}
				text = displayValue;
			}
			return text;
		}

		// Token: 0x04000EA1 RID: 3745
		private static Stack<IMGUIContainer> s_ContainerStack = new Stack<IMGUIContainer>();

		// Token: 0x04000EA2 RID: 3746
		private static Dictionary<int, Panel> s_UIElementsCache = new Dictionary<int, Panel>();

		// Token: 0x04000EA3 RID: 3747
		private static Event s_EventInstance = new Event();

		// Token: 0x04000EA4 RID: 3748
		internal static Color editorPlayModeTintColor = Color.white;

		// Token: 0x04000EA5 RID: 3749
		internal static float singleLineHeight = 18f;

		// Token: 0x04000EA6 RID: 3750
		internal static bool s_EnableOSXContextualMenuEventsOnNonOSXPlatforms;

		// Token: 0x04000EA7 RID: 3751
		private static UIElementsUtility s_Instance = new UIElementsUtility();

		// Token: 0x04000EA8 RID: 3752
		internal static List<Panel> s_PanelsIterationList = new List<Panel>();

		// Token: 0x04000EA9 RID: 3753
		internal static readonly string s_RepaintProfilerMarkerName = "UIElementsUtility.DoDispatch(Repaint Event)";

		// Token: 0x04000EAA RID: 3754
		internal static readonly string s_EventProfilerMarkerName = "UIElementsUtility.DoDispatch(Non Repaint Event)";

		// Token: 0x04000EAB RID: 3755
		private static readonly ProfilerMarker s_RepaintProfilerMarker = new ProfilerMarker(UIElementsUtility.s_RepaintProfilerMarkerName);

		// Token: 0x04000EAC RID: 3756
		private static readonly ProfilerMarker s_EventProfilerMarker = new ProfilerMarker(UIElementsUtility.s_EventProfilerMarkerName);

		// Token: 0x04000EAD RID: 3757
		internal static char[] s_Modifiers = new char[] { '&', '%', '^', '#', '_' };
	}
}
