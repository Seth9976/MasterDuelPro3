using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	// Token: 0x0200045D RID: 1117
	internal static class UIElementsRuntimeUtility
	{
		// Token: 0x1400002C RID: 44
		// (add) Token: 0x060020E4 RID: 8420 RVA: 0x00078D9C File Offset: 0x00076F9C
		// (remove) Token: 0x060020E5 RID: 8421 RVA: 0x00078DD0 File Offset: 0x00076FD0
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<BaseRuntimePanel> onCreatePanel;

		// Token: 0x060020E6 RID: 8422 RVA: 0x00078E04 File Offset: 0x00077004
		static UIElementsRuntimeUtility()
		{
			Canvas.externBeginRenderOverlays = new Action<int>(UIElementsRuntimeUtility.BeginRenderOverlays);
			Canvas.externRenderOverlaysBefore = delegate(int displayIndex, int sortOrder)
			{
				UIElementsRuntimeUtility.RenderOverlaysBeforePriority(displayIndex, (float)sortOrder);
			};
			Canvas.externEndRenderOverlays = new Action<int>(UIElementsRuntimeUtility.EndRenderOverlays);
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x00078E94 File Offset: 0x00077094
		public static EventBase CreateEvent(Event systemEvent)
		{
			return UIElementsUtility.CreateEvent(systemEvent, systemEvent.rawType);
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x00078EB4 File Offset: 0x000770B4
		public static BaseRuntimePanel FindOrCreateRuntimePanel(ScriptableObject ownerObject, UIElementsRuntimeUtility.CreateRuntimePanelDelegate createDelegate)
		{
			Panel cachedPanel;
			bool flag = UIElementsUtility.TryGetPanel(ownerObject.GetInstanceID(), out cachedPanel);
			if (flag)
			{
				BaseRuntimePanel runtimePanel = cachedPanel as BaseRuntimePanel;
				bool flag2 = runtimePanel != null;
				if (flag2)
				{
					return runtimePanel;
				}
				UIElementsRuntimeUtility.RemoveCachedPanelInternal(ownerObject.GetInstanceID());
			}
			BaseRuntimePanel panel = createDelegate(ownerObject);
			panel.IMGUIEventInterests = new EventInterests
			{
				wantsMouseMove = true,
				wantsMouseEnterLeaveWindow = true
			};
			UIElementsRuntimeUtility.RegisterCachedPanelInternal(ownerObject.GetInstanceID(), panel);
			Action<BaseRuntimePanel> action = UIElementsRuntimeUtility.onCreatePanel;
			if (action != null)
			{
				action(panel);
			}
			return panel;
		}

		// Token: 0x060020E9 RID: 8425 RVA: 0x00078F48 File Offset: 0x00077148
		public static void DisposeRuntimePanel(ScriptableObject ownerObject)
		{
			Panel panel;
			bool flag = UIElementsUtility.TryGetPanel(ownerObject.GetInstanceID(), out panel);
			if (flag)
			{
				panel.Dispose();
				UIElementsRuntimeUtility.RemoveCachedPanelInternal(ownerObject.GetInstanceID());
			}
		}

		// Token: 0x060020EA RID: 8426 RVA: 0x00078F7C File Offset: 0x0007717C
		private static void RegisterCachedPanelInternal(int instanceID, IPanel panel)
		{
			UIElementsUtility.RegisterCachedPanel(instanceID, panel as Panel);
			UIElementsRuntimeUtility.s_PanelOrderingDirty = true;
			bool flag = !UIElementsRuntimeUtility.s_RegisteredPlayerloopCallback;
			if (flag)
			{
				UIElementsRuntimeUtility.s_RegisteredPlayerloopCallback = true;
				UIElementsRuntimeUtility.RegisterPlayerloopCallback();
				Canvas.SetExternalCanvasEnabled(true);
			}
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x00078FC0 File Offset: 0x000771C0
		private static void RemoveCachedPanelInternal(int instanceID)
		{
			UIElementsUtility.RemoveCachedPanel(instanceID);
			UIElementsRuntimeUtility.s_PanelOrderingDirty = true;
			UIElementsRuntimeUtility.s_SortedRuntimePanels.Clear();
			UIElementsUtility.GetAllPanels(UIElementsRuntimeUtility.s_SortedRuntimePanels, ContextType.Player);
			bool flag = UIElementsRuntimeUtility.s_SortedRuntimePanels.Count == 0;
			if (flag)
			{
				UIElementsRuntimeUtility.s_RegisteredPlayerloopCallback = false;
				UIElementsRuntimeUtility.UnregisterPlayerloopCallback();
				Canvas.SetExternalCanvasEnabled(false);
			}
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x00079018 File Offset: 0x00077218
		public static void RenderOffscreenPanels()
		{
			Camera oldCam = Camera.current;
			RenderTexture oldRT = RenderTexture.active;
			foreach (Panel panel2 in UIElementsRuntimeUtility.GetSortedPlayerPanels())
			{
				BaseRuntimePanel panel = (BaseRuntimePanel)panel2;
				bool flag = !panel.drawsInCameras && panel.targetTexture != null;
				if (flag)
				{
					UIElementsRuntimeUtility.RenderPanel(panel, false);
				}
			}
			Camera.SetupCurrent(oldCam);
			RenderTexture.active = oldRT;
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x000790B0 File Offset: 0x000772B0
		public static void RepaintPanel(BaseRuntimePanel panel)
		{
			Camera oldCam = Camera.current;
			RenderTexture oldRT = RenderTexture.active;
			using (UIElementsRuntimeUtility.s_RepaintProfilerMarker.Auto())
			{
				panel.Repaint(Event.current);
			}
			Camera.SetupCurrent(oldCam);
			RenderTexture.active = oldRT;
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x00079114 File Offset: 0x00077314
		public static void RenderPanel(BaseRuntimePanel panel, bool restoreState = true)
		{
			Debug.Assert(!panel.drawsInCameras);
			Camera oldCam = Camera.current;
			RenderTexture oldRT = RenderTexture.active;
			panel.Render();
			bool flag = !panel.drawsInCameras && restoreState;
			if (flag)
			{
				Camera.SetupCurrent(oldCam);
				RenderTexture.active = oldRT;
			}
		}

		// Token: 0x060020EF RID: 8431 RVA: 0x00079163 File Offset: 0x00077363
		internal static void BeginRenderOverlays(int displayIndex)
		{
			UIElementsRuntimeUtility.currentOverlayIndex = 0;
		}

		// Token: 0x060020F0 RID: 8432 RVA: 0x0007916C File Offset: 0x0007736C
		internal static void RenderOverlaysBeforePriority(int displayIndex, float maxPriority)
		{
			bool flag = UIElementsRuntimeUtility.currentOverlayIndex < 0;
			if (!flag)
			{
				List<Panel> runTimePanels = UIElementsRuntimeUtility.GetSortedPlayerPanels();
				while (UIElementsRuntimeUtility.currentOverlayIndex < runTimePanels.Count)
				{
					BaseRuntimePanel p = runTimePanels[UIElementsRuntimeUtility.currentOverlayIndex] as BaseRuntimePanel;
					bool flag2 = p != null;
					if (flag2)
					{
						bool flag3 = p.sortingPriority >= maxPriority;
						if (flag3)
						{
							break;
						}
						bool flag4 = p.targetDisplay == displayIndex && !p.drawsInCameras && p.targetTexture == null;
						if (flag4)
						{
							UIElementsRuntimeUtility.RenderPanel(p, true);
						}
					}
					UIElementsRuntimeUtility.currentOverlayIndex++;
				}
			}
		}

		// Token: 0x060020F1 RID: 8433 RVA: 0x00079211 File Offset: 0x00077411
		internal static void EndRenderOverlays(int displayIndex)
		{
			UIElementsRuntimeUtility.RenderOverlaysBeforePriority(displayIndex, float.MaxValue);
			UIElementsRuntimeUtility.currentOverlayIndex = -1;
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x00079228 File Offset: 0x00077428
		public static void RepaintPanels(bool onlyOffscreen)
		{
			foreach (Panel panel2 in UIElementsRuntimeUtility.GetSortedPlayerPanels())
			{
				BaseRuntimePanel panel = (BaseRuntimePanel)panel2;
				bool flag = !onlyOffscreen || panel.targetTexture != null;
				if (flag)
				{
					UIElementsRuntimeUtility.RepaintPanel(panel);
				}
			}
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x060020F3 RID: 8435 RVA: 0x0007929C File Offset: 0x0007749C
		// (set) Token: 0x060020F4 RID: 8436 RVA: 0x000792A3 File Offset: 0x000774A3
		internal static Object activeEventSystem { get; private set; }

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x060020F5 RID: 8437 RVA: 0x000792AB File Offset: 0x000774AB
		internal static bool useDefaultEventSystem
		{
			get
			{
				return UIElementsRuntimeUtility.activeEventSystem == null;
			}
		}

		// Token: 0x060020F6 RID: 8438 RVA: 0x000792B8 File Offset: 0x000774B8
		public static void RegisterEventSystem(Object eventSystem)
		{
			bool flag = UIElementsRuntimeUtility.activeEventSystem != null && UIElementsRuntimeUtility.activeEventSystem != eventSystem && eventSystem.GetType().Name == "EventSystem";
			if (flag)
			{
				Debug.LogWarning("There can be only one active Event System.");
			}
			UIElementsRuntimeUtility.activeEventSystem = eventSystem;
		}

		// Token: 0x060020F7 RID: 8439 RVA: 0x00079310 File Offset: 0x00077510
		public static void UnregisterEventSystem(Object eventSystem)
		{
			bool flag = UIElementsRuntimeUtility.activeEventSystem == eventSystem;
			if (flag)
			{
				UIElementsRuntimeUtility.activeEventSystem = null;
			}
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x060020F8 RID: 8440 RVA: 0x00079334 File Offset: 0x00077534
		internal static DefaultEventSystem defaultEventSystem
		{
			get
			{
				DefaultEventSystem defaultEventSystem;
				if ((defaultEventSystem = UIElementsRuntimeUtility.s_DefaultEventSystem) == null)
				{
					defaultEventSystem = (UIElementsRuntimeUtility.s_DefaultEventSystem = new DefaultEventSystem());
				}
				return defaultEventSystem;
			}
		}

		// Token: 0x060020F9 RID: 8441 RVA: 0x0007934C File Offset: 0x0007754C
		public static void UpdatePanels()
		{
			UIElementsRuntimeUtility.RemoveUnusedPanels();
			foreach (Panel panel2 in UIElementsRuntimeUtility.GetSortedPlayerPanels())
			{
				BaseRuntimePanel panel = (BaseRuntimePanel)panel2;
				panel.Update();
			}
			bool useDefaultEventSystem = UIElementsRuntimeUtility.useDefaultEventSystem;
			if (useDefaultEventSystem)
			{
				UIElementsRuntimeUtility.defaultEventSystem.isInputReady = true;
				UIElementsRuntimeUtility.defaultEventSystem.Update(DefaultEventSystem.UpdateMode.IgnoreIfAppNotFocused);
			}
			else
			{
				bool flag = UIElementsRuntimeUtility.s_DefaultEventSystem != null;
				if (flag)
				{
					UIElementsRuntimeUtility.s_DefaultEventSystem.isInputReady = false;
				}
			}
		}

		// Token: 0x060020FA RID: 8442 RVA: 0x000793F0 File Offset: 0x000775F0
		internal static void MarkPotentiallyEmpty(PanelSettings settings)
		{
			bool flag = !UIElementsRuntimeUtility.s_PotentiallyEmptyPanelSettings.Contains(settings);
			if (flag)
			{
				UIElementsRuntimeUtility.s_PotentiallyEmptyPanelSettings.Add(settings);
			}
		}

		// Token: 0x060020FB RID: 8443 RVA: 0x0007941C File Offset: 0x0007761C
		internal static void RemoveUnusedPanels()
		{
			foreach (PanelSettings psetting in UIElementsRuntimeUtility.s_PotentiallyEmptyPanelSettings)
			{
				UIDocumentList m_AttachedUIDocumentsList = psetting.m_AttachedUIDocumentsList;
				bool flag = m_AttachedUIDocumentsList == null || m_AttachedUIDocumentsList.m_AttachedUIDocuments.Count == 0;
				if (flag)
				{
					psetting.DisposePanel();
				}
			}
			UIElementsRuntimeUtility.s_PotentiallyEmptyPanelSettings.Clear();
		}

		// Token: 0x060020FC RID: 8444 RVA: 0x000794A0 File Offset: 0x000776A0
		public static void RegisterPlayerloopCallback()
		{
			UIElementsRuntimeUtilityNative.RegisterPlayerloopCallback();
			UIElementsRuntimeUtilityNative.UpdatePanelsCallback = new Action(UIElementsRuntimeUtility.UpdatePanels);
			UIElementsRuntimeUtilityNative.RepaintPanelsCallback = new Action<bool>(UIElementsRuntimeUtility.RepaintPanels);
			UIElementsRuntimeUtilityNative.RenderOffscreenPanelsCallback = new Action(UIElementsRuntimeUtility.RenderOffscreenPanels);
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x000794DC File Offset: 0x000776DC
		public static void UnregisterPlayerloopCallback()
		{
			UIElementsRuntimeUtilityNative.UnregisterPlayerloopCallback();
			UIElementsRuntimeUtilityNative.UpdatePanelsCallback = null;
			UIElementsRuntimeUtilityNative.RepaintPanelsCallback = null;
			UIElementsRuntimeUtilityNative.RenderOffscreenPanelsCallback = null;
			bool flag = UIElementsRuntimeUtility.s_DefaultEventSystem != null;
			if (flag)
			{
				UIElementsRuntimeUtility.s_DefaultEventSystem.isInputReady = false;
			}
		}

		// Token: 0x060020FE RID: 8446 RVA: 0x0007951A File Offset: 0x0007771A
		internal static void SetPanelOrderingDirty()
		{
			UIElementsRuntimeUtility.s_PanelOrderingDirty = true;
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x00079524 File Offset: 0x00077724
		internal static List<Panel> GetSortedPlayerPanels()
		{
			bool flag = UIElementsRuntimeUtility.s_PanelOrderingDirty;
			if (flag)
			{
				UIElementsRuntimeUtility.SortPanels();
			}
			return UIElementsRuntimeUtility.s_SortedRuntimePanels;
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x0007954C File Offset: 0x0007774C
		private static void SortPanels()
		{
			UIElementsRuntimeUtility.s_SortedRuntimePanels.Clear();
			UIElementsUtility.GetAllPanels(UIElementsRuntimeUtility.s_SortedRuntimePanels, ContextType.Player);
			UIElementsRuntimeUtility.s_SortedRuntimePanels.Sort(delegate(Panel a, Panel b)
			{
				BaseRuntimePanel runtimePanelA = a as BaseRuntimePanel;
				BaseRuntimePanel runtimePanelB = b as BaseRuntimePanel;
				bool flag2 = runtimePanelA == null || runtimePanelB == null;
				int num;
				if (flag2)
				{
					num = 0;
				}
				else
				{
					float diff = runtimePanelA.sortingPriority - runtimePanelB.sortingPriority;
					bool flag3 = Mathf.Approximately(0f, diff);
					if (flag3)
					{
						num = runtimePanelA.m_RuntimePanelCreationIndex.CompareTo(runtimePanelB.m_RuntimePanelCreationIndex);
					}
					else
					{
						num = ((diff < 0f) ? (-1) : 1);
					}
				}
				return num;
			});
			for (int i = 0; i < UIElementsRuntimeUtility.s_SortedRuntimePanels.Count; i++)
			{
				BaseRuntimePanel runtimePanel = UIElementsRuntimeUtility.s_SortedRuntimePanels[i] as BaseRuntimePanel;
				bool flag = runtimePanel != null;
				if (flag)
				{
					runtimePanel.resolvedSortingIndex = i;
				}
			}
			UIElementsRuntimeUtility.s_ResolvedSortingIndexMax = UIElementsRuntimeUtility.s_SortedRuntimePanels.Count - 1;
			UIElementsRuntimeUtility.s_PanelOrderingDirty = false;
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x000795F0 File Offset: 0x000777F0
		internal static Vector2 MultiDisplayBottomLeftToPanelPosition(Vector2 position, out int? targetDisplay)
		{
			Vector2 screenPosition = UIElementsRuntimeUtility.MultiDisplayToLocalScreenPosition(position, out targetDisplay);
			return UIElementsRuntimeUtility.ScreenBottomLeftToPanelPosition(screenPosition, targetDisplay.GetValueOrDefault());
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x00079618 File Offset: 0x00077818
		internal static Vector2 MultiDisplayToLocalScreenPosition(Vector2 position, out int? targetDisplay)
		{
			Vector3 relativePosition = Display.RelativeMouseAt(position);
			bool flag = relativePosition != Vector3.zero;
			Vector2 vector;
			if (flag)
			{
				targetDisplay = new int?((int)relativePosition.z);
				vector = relativePosition;
			}
			else
			{
				targetDisplay = null;
				vector = position;
			}
			return vector;
		}

		// Token: 0x06002103 RID: 8451 RVA: 0x0007966C File Offset: 0x0007786C
		internal static Vector2 ScreenBottomLeftToPanelPosition(Vector2 position, int targetDisplay)
		{
			int screenHeight = Screen.height;
			bool flag = targetDisplay > 0 && targetDisplay < Display.displays.Length;
			if (flag)
			{
				screenHeight = Display.displays[targetDisplay].systemHeight;
			}
			position.y = (float)screenHeight - position.y;
			return position;
		}

		// Token: 0x06002104 RID: 8452 RVA: 0x000796B8 File Offset: 0x000778B8
		internal static Vector2 ScreenBottomLeftToPanelDelta(Vector2 delta)
		{
			delta.y = -delta.y;
			return delta;
		}

		// Token: 0x04000E93 RID: 3731
		private static bool s_RegisteredPlayerloopCallback = false;

		// Token: 0x04000E94 RID: 3732
		private static List<Panel> s_SortedRuntimePanels = new List<Panel>();

		// Token: 0x04000E95 RID: 3733
		private static bool s_PanelOrderingDirty = true;

		// Token: 0x04000E96 RID: 3734
		internal static int s_ResolvedSortingIndexMax = 0;

		// Token: 0x04000E97 RID: 3735
		internal static readonly string s_RepaintProfilerMarkerName = "UIElementsRuntimeUtility.DoDispatch(Repaint Event)";

		// Token: 0x04000E98 RID: 3736
		private static readonly ProfilerMarker s_RepaintProfilerMarker = new ProfilerMarker(UIElementsRuntimeUtility.s_RepaintProfilerMarkerName);

		// Token: 0x04000E99 RID: 3737
		private static int currentOverlayIndex = -1;

		// Token: 0x04000E9B RID: 3739
		private static DefaultEventSystem s_DefaultEventSystem;

		// Token: 0x04000E9C RID: 3740
		private static List<PanelSettings> s_PotentiallyEmptyPanelSettings = new List<PanelSettings>();

		// Token: 0x0200045E RID: 1118
		// (Invoke) Token: 0x06002106 RID: 8454
		public delegate BaseRuntimePanel CreateRuntimePanelDelegate(ScriptableObject ownerObject);
	}
}
