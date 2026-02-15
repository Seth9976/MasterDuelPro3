using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.IntegerTime;
using UnityEngine.InputForUI;

namespace UnityEngine.UIElements
{
	// Token: 0x0200017D RID: 381
	internal class DefaultEventSystem
	{
		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x00035EA1 File Offset: 0x000340A1
		private bool isAppFocused
		{
			get
			{
				return Application.isFocused;
			}
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00035EA8 File Offset: 0x000340A8
		private bool ShouldIgnoreEventsOnAppNotFocused()
		{
			OperatingSystemFamily operatingSystemFamily = SystemInfo.operatingSystemFamily;
			OperatingSystemFamily operatingSystemFamily2 = operatingSystemFamily;
			return operatingSystemFamily2 - OperatingSystemFamily.MacOSX <= 2;
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x00035ECF File Offset: 0x000340CF
		// (set) Token: 0x06000B27 RID: 2855 RVA: 0x00035ED8 File Offset: 0x000340D8
		public BaseRuntimePanel focusedPanel
		{
			get
			{
				return this.m_FocusedPanel;
			}
			set
			{
				bool flag = this.m_FocusedPanel != value;
				if (flag)
				{
					BaseRuntimePanel focusedPanel = this.m_FocusedPanel;
					if (focusedPanel != null)
					{
						focusedPanel.Blur();
					}
					this.m_FocusedPanel = value;
					BaseRuntimePanel focusedPanel2 = this.m_FocusedPanel;
					if (focusedPanel2 != null)
					{
						focusedPanel2.Focus();
					}
				}
			}
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00035F24 File Offset: 0x00034124
		public void Update(DefaultEventSystem.UpdateMode updateMode = DefaultEventSystem.UpdateMode.Always)
		{
			bool flag = !this.isAppFocused && this.ShouldIgnoreEventsOnAppNotFocused() && updateMode == DefaultEventSystem.UpdateMode.IgnoreIfAppNotFocused;
			if (!flag)
			{
				bool isInputForUIActive = this.m_IsInputForUIActive;
				if (isInputForUIActive)
				{
					this.inputForUIProcessor.ProcessInputForUIEvents();
				}
				else
				{
					this.legacyInputProcessor.ProcessLegacyInputEvents();
				}
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x00035F78 File Offset: 0x00034178
		internal DefaultEventSystem.LegacyInputProcessor legacyInputProcessor
		{
			get
			{
				DefaultEventSystem.LegacyInputProcessor legacyInputProcessor;
				if ((legacyInputProcessor = this.m_LegacyInputProcessor) == null)
				{
					legacyInputProcessor = (this.m_LegacyInputProcessor = new DefaultEventSystem.LegacyInputProcessor(this));
				}
				return legacyInputProcessor;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x00035FA0 File Offset: 0x000341A0
		private DefaultEventSystem.InputForUIProcessor inputForUIProcessor
		{
			get
			{
				DefaultEventSystem.InputForUIProcessor inputForUIProcessor;
				if ((inputForUIProcessor = this.m_InputForUIProcessor) == null)
				{
					inputForUIProcessor = (this.m_InputForUIProcessor = new DefaultEventSystem.InputForUIProcessor(this));
				}
				return inputForUIProcessor;
			}
		}

		// Token: 0x170001FA RID: 506
		// (set) Token: 0x06000B2B RID: 2859 RVA: 0x00035FC8 File Offset: 0x000341C8
		internal bool isInputReady
		{
			set
			{
				bool flag = this.m_IsInputReady == value;
				if (!flag)
				{
					this.m_IsInputReady = value;
					bool isInputReady = this.m_IsInputReady;
					if (isInputReady)
					{
						this.InitInputProcessor();
					}
					else
					{
						this.RemoveInputProcessor();
					}
				}
			}
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0003600C File Offset: 0x0003420C
		internal DefaultEventSystem.FocusBasedEventSequenceContext FocusBasedEventSequence()
		{
			return new DefaultEventSystem.FocusBasedEventSequenceContext(this);
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x00036024 File Offset: 0x00034224
		private void RemoveInputProcessor()
		{
			bool isInputForUIActive = this.m_IsInputForUIActive;
			if (isInputForUIActive)
			{
				EventProvider.Unsubscribe(new EventConsumer(this.inputForUIProcessor.OnEvent));
				EventProvider.SetEnabled(false);
				this.m_IsInputForUIActive = false;
			}
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x00036064 File Offset: 0x00034264
		private void InitInputProcessor()
		{
			bool useInputForUI = this.m_UseInputForUI;
			if (useInputForUI)
			{
				this.m_IsInputForUIActive = true;
				EventProvider.SetEnabled(true);
				EventProvider.Subscribe(new EventConsumer(this.inputForUIProcessor.OnEvent), 0, null, Array.Empty<Event.Type>());
			}
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x000360B2 File Offset: 0x000342B2
		internal void OnFocusEvent(RuntimePanel panel, FocusEvent evt)
		{
			this.focusedPanel = panel;
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x000360C0 File Offset: 0x000342C0
		internal void SendFocusBasedEvent<TArg>(Func<TArg, EventBase> evtFactory, TArg arg)
		{
			bool flag = this.m_PreviousFocusedPanel != null;
			if (flag)
			{
				using (EventBase evt = evtFactory(arg))
				{
					evt.elementTarget = ((VisualElement)this.m_PreviousFocusedElement) ?? this.m_PreviousFocusedPanel.visualTree;
					this.m_PreviousFocusedPanel.visualTree.SendEvent(evt);
					this.UpdateFocusedPanel(this.m_PreviousFocusedPanel);
					return;
				}
			}
			List<Panel> panels = UIElementsRuntimeUtility.GetSortedPlayerPanels();
			for (int i = panels.Count - 1; i >= 0; i--)
			{
				Panel panel = panels[i];
				BaseRuntimePanel runtimePanel = panel as BaseRuntimePanel;
				bool flag2 = runtimePanel != null;
				if (flag2)
				{
					using (EventBase evt2 = evtFactory(arg))
					{
						evt2.elementTarget = runtimePanel.visualTree;
						runtimePanel.visualTree.SendEvent(evt2);
						bool flag3 = runtimePanel.focusController.focusedElement != null;
						if (flag3)
						{
							this.focusedPanel = runtimePanel;
							break;
						}
						bool isPropagationStopped = evt2.isPropagationStopped;
						if (isPropagationStopped)
						{
							break;
						}
					}
				}
			}
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x00036204 File Offset: 0x00034404
		private void SendPositionBasedEvent<TArg>(Vector3 mousePosition, Vector3 delta, int pointerId, int? targetDisplay, Func<Vector3, Vector3, TArg, EventBase> evtFactory, TArg arg, bool deselectIfNoTarget = false)
		{
			bool flag = this.focusedPanel != null;
			if (flag)
			{
				this.UpdateFocusedPanel(this.focusedPanel);
			}
			IPanel capturingPanel = PointerDeviceState.GetPlayerPanelWithSoftPointerCapture(pointerId);
			IEventHandler capturing = RuntimePanel.s_EventDispatcher.pointerState.GetCapturingElement(pointerId);
			VisualElement capturingVE = capturing as VisualElement;
			bool flag2 = capturingVE != null;
			if (flag2)
			{
				capturingPanel = capturingVE.panel;
			}
			BaseRuntimePanel targetPanel = null;
			Vector2 targetPanelPosition = Vector2.zero;
			Vector2 targetPanelDelta = Vector2.zero;
			BaseRuntimePanel capturingRuntimePanel = capturingPanel as BaseRuntimePanel;
			bool flag3 = capturingRuntimePanel != null;
			if (flag3)
			{
				targetPanel = capturingRuntimePanel;
				targetPanel.ScreenToPanel(mousePosition, delta, out targetPanelPosition, out targetPanelDelta, false);
			}
			else
			{
				List<Panel> panels = UIElementsRuntimeUtility.GetSortedPlayerPanels();
				for (int i = panels.Count - 1; i >= 0; i--)
				{
					BaseRuntimePanel runtimePanel = panels[i] as BaseRuntimePanel;
					bool flag4;
					if (runtimePanel != null)
					{
						if (targetDisplay != null)
						{
							int targetDisplay2 = runtimePanel.targetDisplay;
							int? num = targetDisplay;
							flag4 = (targetDisplay2 == num.GetValueOrDefault()) & (num != null);
						}
						else
						{
							flag4 = true;
						}
					}
					else
					{
						flag4 = false;
					}
					bool flag5 = flag4;
					if (flag5)
					{
						bool flag6 = runtimePanel.ScreenToPanel(mousePosition, delta, out targetPanelPosition, out targetPanelDelta, false) && runtimePanel.Pick(targetPanelPosition) != null;
						if (flag6)
						{
							targetPanel = runtimePanel;
							break;
						}
					}
				}
			}
			BaseRuntimePanel lastActivePanel = PointerDeviceState.GetPanel(pointerId, ContextType.Player) as BaseRuntimePanel;
			bool flag7 = lastActivePanel != targetPanel;
			if (flag7)
			{
				if (lastActivePanel != null)
				{
					lastActivePanel.PointerLeavesPanel(pointerId, lastActivePanel.ScreenToPanel(mousePosition));
				}
				if (targetPanel != null)
				{
					targetPanel.PointerEntersPanel(pointerId, targetPanelPosition);
				}
			}
			bool flag8 = targetPanel != null;
			if (flag8)
			{
				using (EventBase evt = evtFactory(targetPanelPosition, targetPanelDelta, arg))
				{
					targetPanel.visualTree.SendEvent(evt);
					bool processedByFocusController = evt.processedByFocusController;
					if (processedByFocusController)
					{
						this.UpdateFocusedPanel(targetPanel);
					}
					bool flag9 = evt.eventTypeId == EventBase<PointerDownEvent>.TypeId();
					if (flag9)
					{
						PointerDeviceState.SetPlayerPanelWithSoftPointerCapture(pointerId, targetPanel);
					}
					else
					{
						bool flag10 = evt.eventTypeId == EventBase<PointerUpEvent>.TypeId() && ((PointerUpEvent)evt).pressedButtons == 0;
						if (flag10)
						{
							PointerDeviceState.SetPlayerPanelWithSoftPointerCapture(pointerId, null);
						}
					}
				}
			}
			else if (deselectIfNoTarget)
			{
				this.focusedPanel = null;
			}
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x00036470 File Offset: 0x00034670
		private void UpdateFocusedPanel(BaseRuntimePanel runtimePanel)
		{
			bool flag = runtimePanel.focusController.focusedElement != null;
			if (flag)
			{
				this.focusedPanel = runtimePanel;
			}
			else
			{
				bool flag2 = this.focusedPanel == runtimePanel;
				if (flag2)
				{
					this.focusedPanel = null;
				}
			}
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x000364B4 File Offset: 0x000346B4
		private static EventBase MakeTouchEvent(Touch touch, EventModifiers modifiers, int targetDisplay)
		{
			switch (touch.phase)
			{
			case TouchPhase.Began:
				return PointerEventBase<PointerDownEvent>.GetPooled(touch, modifiers, targetDisplay);
			case TouchPhase.Moved:
				return PointerEventBase<PointerMoveEvent>.GetPooled(touch, modifiers, targetDisplay);
			case TouchPhase.Ended:
				return PointerEventBase<PointerUpEvent>.GetPooled(touch, modifiers, targetDisplay);
			case TouchPhase.Canceled:
				return PointerEventBase<PointerCancelEvent>.GetPooled(touch, modifiers, targetDisplay);
			}
			return null;
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0003651C File Offset: 0x0003471C
		private static EventBase MakePenEvent(PenData pen, EventModifiers modifiers, int targetDisplay)
		{
			EventBase eventBase;
			switch (pen.contactType)
			{
			case PenEventType.NoContact:
				eventBase = PointerEventBase<PointerMoveEvent>.GetPooled(pen, modifiers, targetDisplay);
				break;
			case PenEventType.PenDown:
				eventBase = PointerEventBase<PointerDownEvent>.GetPooled(pen, modifiers, targetDisplay);
				break;
			case PenEventType.PenUp:
				eventBase = PointerEventBase<PointerUpEvent>.GetPooled(pen, modifiers, targetDisplay);
				break;
			default:
				eventBase = null;
				break;
			}
			return eventBase;
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00036570 File Offset: 0x00034770
		private void Log(object o)
		{
			Debug.Log(o);
			bool flag = this.logToGameScreen;
			if (flag)
			{
				this.LogToGameScreen(((o != null) ? o.ToString() : null) ?? "");
			}
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x000365AC File Offset: 0x000347AC
		private void LogWarning(object o)
		{
			Debug.LogWarning(o);
			bool flag = this.logToGameScreen;
			if (flag)
			{
				this.LogToGameScreen("Warning! " + ((o != null) ? o.ToString() : null));
			}
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x000365E8 File Offset: 0x000347E8
		private void LogToGameScreen(string s)
		{
			bool flag = this.m_LogLabel == null;
			if (flag)
			{
				this.m_LogLabel = new Label
				{
					style = 
					{
						position = Position.Absolute,
						bottom = 0f,
						color = Color.white
					}
				};
				Object.FindFirstObjectByType<UIDocument>().rootVisualElement.Add(this.m_LogLabel);
			}
			this.m_LogLines.Add(s + "\n");
			bool flag2 = this.m_LogLines.Count > 10;
			if (flag2)
			{
				this.m_LogLines.RemoveAt(0);
			}
			this.m_LogLabel.text = string.Concat(this.m_LogLines);
		}

		// Token: 0x04000729 RID: 1833
		internal static Func<bool> IsEditorRemoteConnected = () => false;

		// Token: 0x0400072A RID: 1834
		private BaseRuntimePanel m_FocusedPanel;

		// Token: 0x0400072B RID: 1835
		private BaseRuntimePanel m_PreviousFocusedPanel;

		// Token: 0x0400072C RID: 1836
		private Focusable m_PreviousFocusedElement;

		// Token: 0x0400072D RID: 1837
		private DefaultEventSystem.LegacyInputProcessor m_LegacyInputProcessor;

		// Token: 0x0400072E RID: 1838
		private DefaultEventSystem.InputForUIProcessor m_InputForUIProcessor;

		// Token: 0x0400072F RID: 1839
		private bool m_IsInputReady = false;

		// Token: 0x04000730 RID: 1840
		private bool m_UseInputForUI = true;

		// Token: 0x04000731 RID: 1841
		private bool m_IsInputForUIActive = false;

		// Token: 0x04000732 RID: 1842
		internal bool verbose = false;

		// Token: 0x04000733 RID: 1843
		internal bool logToGameScreen = false;

		// Token: 0x04000734 RID: 1844
		private Label m_LogLabel;

		// Token: 0x04000735 RID: 1845
		private List<string> m_LogLines = new List<string>();

		// Token: 0x0200017E RID: 382
		public enum UpdateMode
		{
			// Token: 0x04000737 RID: 1847
			Always,
			// Token: 0x04000738 RID: 1848
			IgnoreIfAppNotFocused
		}

		// Token: 0x0200017F RID: 383
		internal struct FocusBasedEventSequenceContext : IDisposable
		{
			// Token: 0x06000B3A RID: 2874 RVA: 0x00036700 File Offset: 0x00034900
			public FocusBasedEventSequenceContext(DefaultEventSystem es)
			{
				this.es = es;
				es.m_PreviousFocusedPanel = es.focusedPanel;
				BaseRuntimePanel focusedPanel = es.focusedPanel;
				es.m_PreviousFocusedElement = ((focusedPanel != null) ? focusedPanel.focusController.GetLeafFocusedElement() : null);
			}

			// Token: 0x06000B3B RID: 2875 RVA: 0x00036733 File Offset: 0x00034933
			public void Dispose()
			{
				this.es.m_PreviousFocusedPanel = null;
				this.es.m_PreviousFocusedElement = null;
			}

			// Token: 0x04000739 RID: 1849
			private DefaultEventSystem es;
		}

		// Token: 0x02000180 RID: 384
		private class InputForUIProcessor
		{
			// Token: 0x06000B3C RID: 2876 RVA: 0x0003674E File Offset: 0x0003494E
			public InputForUIProcessor(DefaultEventSystem eventSystem)
			{
				this.m_EventSystem = eventSystem;
			}

			// Token: 0x06000B3D RID: 2877 RVA: 0x00036780 File Offset: 0x00034980
			public bool OnEvent(in Event ev)
			{
				this.m_EventList.Enqueue(ev);
				return true;
			}

			// Token: 0x06000B3E RID: 2878 RVA: 0x000367A8 File Offset: 0x000349A8
			public void ProcessInputForUIEvents()
			{
				bool flag = this.m_EventList.Count == 0;
				if (!flag)
				{
					DefaultEventSystem.FocusBasedEventSequenceContext? focusContext = null;
					while (this.m_EventList.Count > 0)
					{
						Event newEvent = this.m_EventList.Dequeue();
						switch (newEvent.type)
						{
						case Event.Type.KeyEvent:
						{
							DefaultEventSystem.FocusBasedEventSequenceContext focusBasedEventSequenceContext = focusContext.GetValueOrDefault();
							if (focusContext == null)
							{
								focusBasedEventSequenceContext = this.m_EventSystem.FocusBasedEventSequence();
								focusContext = new DefaultEventSystem.FocusBasedEventSequenceContext?(focusBasedEventSequenceContext);
							}
							this.ProcessKeyEvent(newEvent.asKeyEvent);
							break;
						}
						case Event.Type.PointerEvent:
							this.ProcessPointerEvent(newEvent.asPointerEvent);
							break;
						case Event.Type.TextInputEvent:
						{
							DefaultEventSystem.FocusBasedEventSequenceContext focusBasedEventSequenceContext = focusContext.GetValueOrDefault();
							if (focusContext == null)
							{
								focusBasedEventSequenceContext = this.m_EventSystem.FocusBasedEventSequence();
								focusContext = new DefaultEventSystem.FocusBasedEventSequenceContext?(focusBasedEventSequenceContext);
							}
							this.ProcessTextInputEvent(newEvent.asTextInputEvent);
							break;
						}
						case Event.Type.IMECompositionEvent:
						{
							DefaultEventSystem.FocusBasedEventSequenceContext focusBasedEventSequenceContext = focusContext.GetValueOrDefault();
							if (focusContext == null)
							{
								focusBasedEventSequenceContext = this.m_EventSystem.FocusBasedEventSequence();
								focusContext = new DefaultEventSystem.FocusBasedEventSequenceContext?(focusBasedEventSequenceContext);
							}
							this.ProcessIMECompositionEvent(newEvent.asIMECompositionEvent);
							break;
						}
						case Event.Type.CommandEvent:
						{
							DefaultEventSystem.FocusBasedEventSequenceContext focusBasedEventSequenceContext = focusContext.GetValueOrDefault();
							if (focusContext == null)
							{
								focusBasedEventSequenceContext = this.m_EventSystem.FocusBasedEventSequence();
								focusContext = new DefaultEventSystem.FocusBasedEventSequenceContext?(focusBasedEventSequenceContext);
							}
							this.ProcessCommandEvent(newEvent.asCommandEvent);
							break;
						}
						case Event.Type.NavigationEvent:
						{
							DefaultEventSystem.FocusBasedEventSequenceContext focusBasedEventSequenceContext = focusContext.GetValueOrDefault();
							if (focusContext == null)
							{
								focusBasedEventSequenceContext = this.m_EventSystem.FocusBasedEventSequence();
								focusContext = new DefaultEventSystem.FocusBasedEventSequenceContext?(focusBasedEventSequenceContext);
							}
							this.ProcessNavigationEvent(newEvent.asNavigationEvent);
							break;
						}
						default:
						{
							bool verbose = this.m_EventSystem.verbose;
							if (verbose)
							{
								DefaultEventSystem eventSystem = this.m_EventSystem;
								string text = "Unsupported event (";
								string text2 = ((int)newEvent.type).ToString();
								string text3 = "): ";
								Event @event = newEvent;
								eventSystem.Log(text + text2 + text3 + @event.ToString());
							}
							break;
						}
						}
					}
					if (focusContext != null)
					{
						focusContext.GetValueOrDefault().Dispose();
					}
					this.m_LastPointerTimestamp = this.m_NextPointerTimestamp;
				}
			}

			// Token: 0x06000B3F RID: 2879 RVA: 0x000369E8 File Offset: 0x00034BE8
			private EventModifiers GetModifiers(EventModifiers eventModifiers)
			{
				EventModifiers mod = EventModifiers.None;
				bool isShiftPressed = eventModifiers.isShiftPressed;
				if (isShiftPressed)
				{
					mod |= EventModifiers.Shift;
				}
				bool isCtrlPressed = eventModifiers.isCtrlPressed;
				if (isCtrlPressed)
				{
					mod |= EventModifiers.Control;
				}
				bool isAltPressed = eventModifiers.isAltPressed;
				if (isAltPressed)
				{
					mod |= EventModifiers.Alt;
				}
				bool isMetaPressed = eventModifiers.isMetaPressed;
				if (isMetaPressed)
				{
					mod |= EventModifiers.Command;
				}
				bool isCapsLockEnabled = eventModifiers.isCapsLockEnabled;
				if (isCapsLockEnabled)
				{
					mod |= EventModifiers.CapsLock;
				}
				bool isNumericPressed = eventModifiers.isNumericPressed;
				if (isNumericPressed)
				{
					mod |= EventModifiers.Numeric;
				}
				bool isFunctionKeyPressed = eventModifiers.isFunctionKeyPressed;
				if (isFunctionKeyPressed)
				{
					mod |= EventModifiers.FunctionKey;
				}
				return mod;
			}

			// Token: 0x06000B40 RID: 2880 RVA: 0x00036A84 File Offset: 0x00034C84
			private void ProcessPointerEvent(PointerEvent pointerEvent)
			{
				Vector2 position = pointerEvent.position;
				int targetDisplay = pointerEvent.displayIndex;
				Vector2 deltaPosition = pointerEvent.deltaPosition;
				int pointerIdBase = ((pointerEvent.eventSource == EventSource.Touch) ? PointerId.touchPointerIdBase : ((pointerEvent.eventSource == EventSource.Pen) ? PointerId.penPointerIdBase : PointerId.mousePointerId));
				int pointerId = pointerIdBase + pointerEvent.pointerIndex;
				float deltaTime = ((this.m_LastPointerTimestamp != DiscreteTime.Zero) ? ((float)(pointerEvent.timestamp - this.m_LastPointerTimestamp)) : 0f);
				this.m_NextPointerTimestamp = pointerEvent.timestamp;
				bool flag = pointerEvent.type == PointerEvent.Type.PointerMoved;
				if (flag)
				{
					bool flag2 = !Mathf.Approximately(deltaPosition.x, 0f) || !Mathf.Approximately(deltaPosition.y, 0f);
					if (flag2)
					{
						this.m_EventSystem.SendPositionBasedEvent<ValueTuple<PointerEvent, int, float>>(position, deltaPosition, pointerId, new int?(targetDisplay), (Vector3 panelPosition, Vector3 panelDelta, [TupleElementNames(new string[] { "pointerEvent", "pointerId", "deltaTime" })] ValueTuple<PointerEvent, int, float> t) => PointerEventBase<PointerMoveEvent>.GetPooled(t.Item1, panelPosition, panelDelta, t.Item2, t.Item3), new ValueTuple<PointerEvent, int, float>(pointerEvent, pointerId, deltaTime), false);
					}
				}
				else
				{
					bool flag3 = pointerEvent.type == PointerEvent.Type.ButtonPressed;
					if (flag3)
					{
						this.m_EventSystem.SendPositionBasedEvent<ValueTuple<PointerEvent, int, float>>(position, deltaPosition, pointerId, new int?(targetDisplay), (Vector3 panelPosition, Vector3 panelDelta, [TupleElementNames(new string[] { "pointerEvent", "pointerId", "deltaTime" })] ValueTuple<PointerEvent, int, float> t) => PointerEventBase<PointerDownEvent>.GetPooled(t.Item1, panelPosition, panelDelta, t.Item2, t.Item3), new ValueTuple<PointerEvent, int, float>(pointerEvent, pointerId, deltaTime), false);
					}
					else
					{
						bool flag4 = pointerEvent.type == PointerEvent.Type.ButtonReleased;
						if (flag4)
						{
							this.m_EventSystem.SendPositionBasedEvent<ValueTuple<PointerEvent, int, float>>(position, deltaPosition, pointerId, new int?(targetDisplay), (Vector3 panelPosition, Vector3 panelDelta, [TupleElementNames(new string[] { "pointerEvent", "pointerId", "deltaTime" })] ValueTuple<PointerEvent, int, float> t) => PointerEventBase<PointerUpEvent>.GetPooled(t.Item1, panelPosition, panelDelta, t.Item2, t.Item3), new ValueTuple<PointerEvent, int, float>(pointerEvent, pointerId, deltaTime), true);
						}
						else
						{
							bool flag5 = pointerEvent.type == PointerEvent.Type.TouchCanceled;
							if (flag5)
							{
								this.m_EventSystem.SendPositionBasedEvent<ValueTuple<PointerEvent, int, float>>(position, deltaPosition, pointerId, new int?(targetDisplay), (Vector3 panelPosition, Vector3 panelDelta, [TupleElementNames(new string[] { "pointerEvent", "pointerId", "deltaTime" })] ValueTuple<PointerEvent, int, float> t) => PointerEventBase<PointerCancelEvent>.GetPooled(t.Item1, panelPosition, panelDelta, t.Item2, t.Item3), new ValueTuple<PointerEvent, int, float>(pointerEvent, pointerId, deltaTime), false);
							}
							else
							{
								bool flag6 = pointerEvent.type == PointerEvent.Type.Scroll;
								if (flag6)
								{
									Vector2 scrollDelta = pointerEvent.scroll;
									bool verbose = this.m_EventSystem.verbose;
									if (verbose)
									{
										DefaultEventSystem eventSystem = this.m_EventSystem;
										string text = "ScrollDelta: ";
										Vector2 vector = scrollDelta;
										eventSystem.Log(text + vector.ToString());
									}
									this.m_EventSystem.SendPositionBasedEvent<ValueTuple<EventModifiers, Vector2>>(pointerEvent.position, pointerEvent.deltaPosition, PointerId.mousePointerId, new int?(targetDisplay), (Vector3 panelPosition, Vector3 _, [TupleElementNames(new string[] { "modifiers", "scrollDelta" })] ValueTuple<EventModifiers, Vector2> t) => WheelEvent.GetPooled(t.Item2, panelPosition, t.Item1), new ValueTuple<EventModifiers, Vector2>(this.GetModifiers(pointerEvent.eventModifiers), scrollDelta), false);
								}
								else
								{
									bool verbose2 = this.m_EventSystem.verbose;
									if (verbose2)
									{
										DefaultEventSystem eventSystem2 = this.m_EventSystem;
										string text2 = "Unsupported event ";
										PointerEvent pointerEvent2 = pointerEvent;
										eventSystem2.Log(text2 + pointerEvent2.ToString());
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x06000B41 RID: 2881 RVA: 0x00036DBC File Offset: 0x00034FBC
			private void ProcessNavigationEvent(NavigationEvent navigationEvent)
			{
				bool verbose = this.m_EventSystem.verbose;
				if (verbose)
				{
					this.m_EventSystem.Log(navigationEvent);
				}
				EventModifiers mod = this.GetModifiers(navigationEvent.eventModifiers);
				NavigationDeviceType deviceType = ((navigationEvent.eventSource == EventSource.Keyboard) ? NavigationDeviceType.Keyboard : ((navigationEvent.eventSource == EventSource.Unspecified) ? NavigationDeviceType.Unknown : NavigationDeviceType.NonKeyboard));
				bool flag = navigationEvent.type == NavigationEvent.Type.Move;
				if (flag)
				{
					Vector2 move = Vector2.zero;
					bool flag2 = navigationEvent.direction == NavigationEvent.Direction.Left;
					if (flag2)
					{
						move.x = -1f;
					}
					else
					{
						bool flag3 = navigationEvent.direction == NavigationEvent.Direction.Right;
						if (flag3)
						{
							move.x = 1f;
						}
						else
						{
							bool flag4 = navigationEvent.direction == NavigationEvent.Direction.Up;
							if (flag4)
							{
								move.y = 1f;
							}
							else
							{
								bool flag5 = navigationEvent.direction == NavigationEvent.Direction.Down;
								if (flag5)
								{
									move.y = -1f;
								}
							}
						}
					}
					bool flag6 = move != Vector2.zero;
					if (flag6)
					{
						this.m_EventSystem.SendFocusBasedEvent<ValueTuple<Vector2, NavigationDeviceType, EventModifiers>>(([TupleElementNames(new string[] { "move", "deviceType", "mod" })] ValueTuple<Vector2, NavigationDeviceType, EventModifiers> t) => NavigationMoveEvent.GetPooled(t.Item1, t.Item2, t.Item3), new ValueTuple<Vector2, NavigationDeviceType, EventModifiers>(move, deviceType, mod));
					}
					else
					{
						NavigationMoveEvent.Direction direction = ((navigationEvent.direction == NavigationEvent.Direction.Previous) ? NavigationMoveEvent.Direction.Previous : NavigationMoveEvent.Direction.Next);
						this.m_EventSystem.SendFocusBasedEvent<ValueTuple<NavigationMoveEvent.Direction, NavigationDeviceType, EventModifiers>>(([TupleElementNames(new string[] { "direction", "deviceType", "mod" })] ValueTuple<NavigationMoveEvent.Direction, NavigationDeviceType, EventModifiers> t) => NavigationMoveEvent.GetPooled(t.Item1, t.Item2, t.Item3), new ValueTuple<NavigationMoveEvent.Direction, NavigationDeviceType, EventModifiers>(direction, deviceType, mod));
					}
				}
				else
				{
					bool flag7 = navigationEvent.type == NavigationEvent.Type.Submit;
					if (flag7)
					{
						this.m_EventSystem.SendFocusBasedEvent<ValueTuple<NavigationDeviceType, EventModifiers>>(([TupleElementNames(new string[] { "deviceType", "mod" })] ValueTuple<NavigationDeviceType, EventModifiers> t) => NavigationEventBase<NavigationSubmitEvent>.GetPooled(t.Item1, t.Item2), new ValueTuple<NavigationDeviceType, EventModifiers>(deviceType, mod));
					}
					else
					{
						bool flag8 = navigationEvent.type == NavigationEvent.Type.Cancel;
						if (flag8)
						{
							this.m_EventSystem.SendFocusBasedEvent<ValueTuple<NavigationDeviceType, EventModifiers>>(([TupleElementNames(new string[] { "deviceType", "mod" })] ValueTuple<NavigationDeviceType, EventModifiers> t) => NavigationEventBase<NavigationCancelEvent>.GetPooled(t.Item1, t.Item2), new ValueTuple<NavigationDeviceType, EventModifiers>(deviceType, mod));
						}
					}
				}
			}

			// Token: 0x06000B42 RID: 2882 RVA: 0x00036FBC File Offset: 0x000351BC
			private void ProcessKeyEvent(KeyEvent keyEvent)
			{
				bool verbose = this.m_EventSystem.verbose;
				if (verbose)
				{
					this.m_EventSystem.Log(keyEvent);
				}
				bool flag = keyEvent.type == KeyEvent.Type.KeyPressed || keyEvent.type == KeyEvent.Type.KeyRepeated;
				if (flag)
				{
					this.m_EventSystem.SendFocusBasedEvent<ValueTuple<EventModifiers, KeyCode>>(([TupleElementNames(new string[] { "modifiers", "keyCode" })] ValueTuple<EventModifiers, KeyCode> t) => KeyboardEventBase<KeyDownEvent>.GetPooled('\0', t.Item2, t.Item1), new ValueTuple<EventModifiers, KeyCode>(this.GetModifiers(keyEvent.eventModifiers), keyEvent.keyCode));
				}
				else
				{
					bool flag2 = keyEvent.type == KeyEvent.Type.KeyReleased;
					if (flag2)
					{
						this.m_EventSystem.SendFocusBasedEvent<ValueTuple<EventModifiers, KeyCode>>(([TupleElementNames(new string[] { "modifiers", "keyCode" })] ValueTuple<EventModifiers, KeyCode> t) => KeyboardEventBase<KeyUpEvent>.GetPooled('\0', t.Item2, t.Item1), new ValueTuple<EventModifiers, KeyCode>(this.GetModifiers(keyEvent.eventModifiers), keyEvent.keyCode));
					}
				}
			}

			// Token: 0x06000B43 RID: 2883 RVA: 0x000370A0 File Offset: 0x000352A0
			private void ProcessTextInputEvent(TextInputEvent textInputEvent)
			{
				bool verbose = this.m_EventSystem.verbose;
				if (verbose)
				{
					this.m_EventSystem.Log(textInputEvent);
				}
				this.m_EventSystem.SendFocusBasedEvent<ValueTuple<EventModifiers, char>>(([TupleElementNames(new string[] { "modifiers", "character" })] ValueTuple<EventModifiers, char> t) => KeyboardEventBase<KeyDownEvent>.GetPooled(t.Item2, KeyCode.None, t.Item1), new ValueTuple<EventModifiers, char>(this.GetModifiers(textInputEvent.eventModifiers), textInputEvent.character));
			}

			// Token: 0x06000B44 RID: 2884 RVA: 0x00037114 File Offset: 0x00035314
			private void ProcessCommandEvent(CommandEvent commandEvent)
			{
				bool verbose = this.m_EventSystem.verbose;
				if (verbose)
				{
					this.m_EventSystem.Log(commandEvent);
				}
			}

			// Token: 0x06000B45 RID: 2885 RVA: 0x00037144 File Offset: 0x00035344
			private void ProcessIMECompositionEvent(IMECompositionEvent compositionEvent)
			{
				bool verbose = this.m_EventSystem.verbose;
				if (verbose)
				{
					this.m_EventSystem.Log(compositionEvent);
				}
			}

			// Token: 0x0400073A RID: 1850
			private readonly DefaultEventSystem m_EventSystem;

			// Token: 0x0400073B RID: 1851
			private DiscreteTime m_LastPointerTimestamp = DiscreteTime.Zero;

			// Token: 0x0400073C RID: 1852
			private DiscreteTime m_NextPointerTimestamp = DiscreteTime.Zero;

			// Token: 0x0400073D RID: 1853
			private readonly Queue<Event> m_EventList = new Queue<Event>();
		}

		// Token: 0x02000182 RID: 386
		internal class LegacyInputProcessor
		{
			// Token: 0x170001FB RID: 507
			// (get) Token: 0x06000B54 RID: 2900 RVA: 0x000372C0 File Offset: 0x000354C0
			private EventModifiers m_CurrentPointerModifiers
			{
				get
				{
					return this.m_CurrentModifiers & (EventModifiers.Shift | EventModifiers.Control | EventModifiers.Alt | EventModifiers.Command);
				}
			}

			// Token: 0x170001FC RID: 508
			// (get) Token: 0x06000B55 RID: 2901 RVA: 0x000372CC File Offset: 0x000354CC
			public DefaultEventSystem.LegacyInputProcessor.IInput input
			{
				get
				{
					DefaultEventSystem.LegacyInputProcessor.IInput input;
					if ((input = this.m_Input) == null)
					{
						input = (this.m_Input = this.GetDefaultInput());
					}
					return input;
				}
			}

			// Token: 0x06000B56 RID: 2902 RVA: 0x000372F2 File Offset: 0x000354F2
			public LegacyInputProcessor(DefaultEventSystem eventSystem)
			{
				this.m_EventSystem = eventSystem;
			}

			// Token: 0x06000B57 RID: 2903 RVA: 0x00037334 File Offset: 0x00035534
			public DefaultEventSystem.LegacyInputProcessor.IInput GetDefaultInput()
			{
				DefaultEventSystem.LegacyInputProcessor.IInput input = new DefaultEventSystem.LegacyInputProcessor.Input();
				try
				{
					input.GetAxisRaw("Horizontal");
				}
				catch (InvalidOperationException)
				{
					input = new DefaultEventSystem.LegacyInputProcessor.NoInput();
					this.m_EventSystem.LogWarning("UI Toolkit is currently relying on the legacy Input Manager for its active input source, but the legacy Input Manager is not available using your current Project Settings. Some UI Toolkit functionality might be missing or not working properly as a result. To fix this problem, you can enable \"Input Manager (old)\" or \"Both\" in the Active Input Source setting of the Player section. UI Toolkit is using its internal default event system to process input. Alternatively, you may activate new Input System support with UI Toolkit by adding an EventSystem component to your active scene.");
				}
				return input;
			}

			// Token: 0x06000B58 RID: 2904 RVA: 0x0003738C File Offset: 0x0003558C
			public void ProcessLegacyInputEvents()
			{
				this.m_SendingPenEvent = this.ProcessPenEvents();
				bool flag = !this.m_SendingPenEvent;
				if (flag)
				{
					this.m_SendingTouchEvents = this.ProcessTouchEvents();
				}
				bool flag2 = !this.m_SendingPenEvent && !this.m_SendingTouchEvents;
				if (flag2)
				{
					this.ProcessMouseEvents();
				}
				else
				{
					this.m_MouseProcessedAtLeastOnce = false;
				}
				using (this.m_EventSystem.FocusBasedEventSequence())
				{
					this.SendIMGUIEvents();
					this.SendInputEvents();
				}
			}

			// Token: 0x06000B59 RID: 2905 RVA: 0x00037424 File Offset: 0x00035624
			private void SendIMGUIEvents()
			{
				bool first = true;
				while (Event.PopEvent(this.m_Event))
				{
					bool flag = this.m_Event.type == EventType.Ignore || this.m_Event.type == EventType.Repaint || this.m_Event.type == EventType.Layout;
					if (!flag)
					{
						this.m_CurrentModifiers = (first ? this.m_Event.modifiers : (this.m_CurrentModifiers | this.m_Event.modifiers));
						first = false;
						bool flag2 = this.m_Event.type == EventType.KeyUp || this.m_Event.type == EventType.KeyDown;
						if (flag2)
						{
							this.m_EventSystem.SendFocusBasedEvent<Event>((Event e) => UIElementsRuntimeUtility.CreateEvent(e), this.m_Event);
							this.ProcessTabEvent(this.m_Event, this.m_CurrentModifiers);
						}
						else
						{
							bool flag3 = this.m_Event.type == EventType.ScrollWheel;
							if (flag3)
							{
								int? targetDisplay;
								Vector2 position = UIElementsRuntimeUtility.MultiDisplayBottomLeftToPanelPosition(this.input.mousePosition, out targetDisplay);
								Vector2 delta = position - this.m_LastMousePosition;
								Vector2 scrollDelta = this.m_Event.delta;
								this.m_EventSystem.SendPositionBasedEvent<ValueTuple<EventModifiers, Vector2>>(position, delta, PointerId.mousePointerId, targetDisplay, (Vector3 panelPosition, Vector3 _, [TupleElementNames(new string[] { "modifiers", "scrollDelta" })] ValueTuple<EventModifiers, Vector2> t) => WheelEvent.GetPooled(t.Item2, panelPosition, t.Item1), new ValueTuple<EventModifiers, Vector2>(this.m_CurrentPointerModifiers, scrollDelta), false);
							}
							else
							{
								bool flag4 = (!this.m_SendingTouchEvents && !this.m_SendingPenEvent && this.m_Event.pointerType != PointerType.Mouse) || this.m_Event.type == EventType.MouseEnterWindow || this.m_Event.type == EventType.MouseLeaveWindow;
								if (flag4)
								{
									int pointerType = ((this.m_Event.pointerType == PointerType.Mouse) ? PointerId.mousePointerId : ((this.m_Event.pointerType == PointerType.Touch) ? PointerId.touchPointerIdBase : PointerId.penPointerIdBase));
									int? targetDisplay2;
									Vector3 screenPosition = UIElementsRuntimeUtility.MultiDisplayToLocalScreenPosition(this.m_Event.mousePosition, out targetDisplay2);
									Vector2 screenDelta = this.m_Event.delta;
									this.m_EventSystem.SendPositionBasedEvent<Event>(screenPosition, screenDelta, pointerType, targetDisplay2, delegate(Vector3 panelPosition, Vector3 panelDelta, Event evt)
									{
										evt.mousePosition = panelPosition;
										evt.delta = panelDelta;
										return UIElementsRuntimeUtility.CreateEvent(evt);
									}, this.m_Event, this.m_Event.type == EventType.MouseDown || this.m_Event.type == EventType.TouchDown);
								}
							}
						}
					}
				}
			}

			// Token: 0x06000B5A RID: 2906 RVA: 0x000376BC File Offset: 0x000358BC
			private void ProcessMouseEvents()
			{
				bool flag = !this.input.mousePresent;
				if (!flag)
				{
					int? targetDisplay;
					Vector2 position = UIElementsRuntimeUtility.MultiDisplayBottomLeftToPanelPosition(this.input.mousePosition, out targetDisplay);
					Vector2 delta = position - this.m_LastMousePosition;
					bool flag2 = !this.m_MouseProcessedAtLeastOnce;
					if (flag2)
					{
						delta = Vector2.zero;
						this.m_LastMousePosition = position;
						this.m_MouseProcessedAtLeastOnce = true;
					}
					else
					{
						bool flag3 = !Mathf.Approximately(delta.x, 0f) || !Mathf.Approximately(delta.y, 0f);
						if (flag3)
						{
							this.m_LastMousePosition = position;
							this.m_EventSystem.SendPositionBasedEvent<ValueTuple<EventModifiers, int?>>(position, delta, PointerId.mousePointerId, targetDisplay, (Vector3 panelPosition, Vector3 panelDelta, [TupleElementNames(new string[] { "modifiers", "targetDisplay" })] ValueTuple<EventModifiers, int?> t) => PointerEventBase<PointerMoveEvent>.GetPooled(EventType.MouseMove, panelPosition, panelDelta, -1, 0, t.Item1, t.Item2.GetValueOrDefault()), new ValueTuple<EventModifiers, int?>(this.m_CurrentPointerModifiers, targetDisplay), false);
						}
					}
					int mouseButtonCount = this.input.mouseButtonCount;
					for (int button = 0; button < mouseButtonCount; button++)
					{
						bool mouseButtonDown = this.input.GetMouseButtonDown(button);
						if (mouseButtonDown)
						{
							bool flag4 = this.m_LastMousePressButton != button || this.input.unscaledTime >= this.m_NextMousePressTime;
							if (flag4)
							{
								this.m_LastMousePressButton = button;
								this.m_LastMouseClickCount = 0;
							}
							int num = this.m_LastMouseClickCount + 1;
							this.m_LastMouseClickCount = num;
							int clickCount = num;
							this.m_NextMousePressTime = this.input.unscaledTime + this.input.doubleClickTime;
							this.m_EventSystem.SendPositionBasedEvent<ValueTuple<int, int, EventModifiers, int?>>(position, delta, PointerId.mousePointerId, targetDisplay, (Vector3 panelPosition, Vector3 panelDelta, [TupleElementNames(new string[] { "button", "clickCount", "modifiers", "targetDisplay" })] ValueTuple<int, int, EventModifiers, int?> t) => PointerEventHelper.GetPooled(EventType.MouseDown, panelPosition, panelDelta, t.Item1, t.Item2, t.Item3, t.Item4.GetValueOrDefault()), new ValueTuple<int, int, EventModifiers, int?>(button, clickCount, this.m_CurrentPointerModifiers, targetDisplay), true);
						}
						bool mouseButtonUp = this.input.GetMouseButtonUp(button);
						if (mouseButtonUp)
						{
							int clickCount2 = this.m_LastMouseClickCount;
							this.m_EventSystem.SendPositionBasedEvent<ValueTuple<int, int, EventModifiers, int?>>(position, delta, PointerId.mousePointerId, targetDisplay, (Vector3 panelPosition, Vector3 panelDelta, [TupleElementNames(new string[] { "button", "clickCount", "modifiers", "targetDisplay" })] ValueTuple<int, int, EventModifiers, int?> t) => PointerEventHelper.GetPooled(EventType.MouseUp, panelPosition, panelDelta, t.Item1, t.Item2, t.Item3, t.Item4.GetValueOrDefault()), new ValueTuple<int, int, EventModifiers, int?>(button, clickCount2, this.m_CurrentPointerModifiers, targetDisplay), false);
						}
					}
				}
			}

			// Token: 0x06000B5B RID: 2907 RVA: 0x00037920 File Offset: 0x00035B20
			private void SendInputEvents()
			{
				bool sendNavigationMove = this.ShouldSendMoveFromInput();
				bool flag = sendNavigationMove;
				if (flag)
				{
					this.m_EventSystem.SendFocusBasedEvent<DefaultEventSystem.LegacyInputProcessor>((DefaultEventSystem.LegacyInputProcessor self) => NavigationMoveEvent.GetPooled(self.GetRawMoveVector(), self.m_IsMoveFromKeyboard ? NavigationDeviceType.Keyboard : NavigationDeviceType.NonKeyboard, self.m_CurrentModifiers), this);
				}
				bool buttonDown = this.input.GetButtonDown("Submit");
				if (buttonDown)
				{
					this.m_EventSystem.SendFocusBasedEvent<DefaultEventSystem.LegacyInputProcessor>((DefaultEventSystem.LegacyInputProcessor self) => NavigationEventBase<NavigationSubmitEvent>.GetPooled(self.input.anyKey ? NavigationDeviceType.Keyboard : NavigationDeviceType.NonKeyboard, self.m_CurrentModifiers), this);
				}
				bool buttonDown2 = this.input.GetButtonDown("Cancel");
				if (buttonDown2)
				{
					this.m_EventSystem.SendFocusBasedEvent<DefaultEventSystem.LegacyInputProcessor>((DefaultEventSystem.LegacyInputProcessor self) => NavigationEventBase<NavigationCancelEvent>.GetPooled(self.input.anyKey ? NavigationDeviceType.Keyboard : NavigationDeviceType.NonKeyboard, self.m_CurrentModifiers), this);
				}
			}

			// Token: 0x06000B5C RID: 2908 RVA: 0x000379EC File Offset: 0x00035BEC
			private bool ProcessTouchEvents()
			{
				for (int i = 0; i < this.input.touchCount; i++)
				{
					Touch touch = this.input.GetTouch(i);
					bool flag = touch.type == TouchType.Indirect || touch.phase == TouchPhase.Stationary;
					if (!flag)
					{
						int? targetDisplay;
						touch.position = UIElementsRuntimeUtility.MultiDisplayBottomLeftToPanelPosition(touch.position, out targetDisplay);
						int? num;
						touch.rawPosition = UIElementsRuntimeUtility.MultiDisplayBottomLeftToPanelPosition(touch.rawPosition, out num);
						touch.deltaPosition = UIElementsRuntimeUtility.ScreenBottomLeftToPanelDelta(touch.deltaPosition);
						this.m_EventSystem.SendPositionBasedEvent<ValueTuple<Touch, int?>>(touch.position, touch.deltaPosition, PointerId.touchPointerIdBase + touch.fingerId, targetDisplay, delegate(Vector3 panelPosition, Vector3 panelDelta, [TupleElementNames(new string[] { "touch", "targetDisplay" })] ValueTuple<Touch, int?> t)
						{
							t.Item1.position = panelPosition;
							t.Item1.deltaPosition = panelDelta;
							return DefaultEventSystem.MakeTouchEvent(t.Item1, EventModifiers.None, t.Item2.GetValueOrDefault());
						}, new ValueTuple<Touch, int?>(touch, targetDisplay), false);
					}
				}
				return this.input.touchCount > 0;
			}

			// Token: 0x06000B5D RID: 2909 RVA: 0x00037AF8 File Offset: 0x00035CF8
			private bool ProcessPenEvents()
			{
				PenData p = this.input.GetLastPenContactEvent();
				bool flag = p.contactType == PenEventType.NoContact;
				bool flag2;
				if (flag)
				{
					flag2 = false;
				}
				else
				{
					this.m_EventSystem.SendPositionBasedEvent<PenData>(p.position, p.deltaPos, PointerId.penPointerIdBase, null, delegate(Vector3 panelPosition, Vector3 panelDelta, PenData _pen)
					{
						_pen.position = panelPosition;
						_pen.deltaPos = panelDelta;
						return DefaultEventSystem.MakePenEvent(_pen, EventModifiers.None, 0);
					}, p, false);
					this.input.ClearLastPenContactEvent();
					flag2 = true;
				}
				return flag2;
			}

			// Token: 0x06000B5E RID: 2910 RVA: 0x00037B88 File Offset: 0x00035D88
			private Vector2 GetRawMoveVector()
			{
				Vector2 move = Vector2.zero;
				move.x = this.input.GetAxisRaw("Horizontal");
				move.y = this.input.GetAxisRaw("Vertical");
				bool buttonDown = this.input.GetButtonDown("Horizontal");
				if (buttonDown)
				{
					bool flag = move.x < 0f;
					if (flag)
					{
						move.x = -1f;
					}
					bool flag2 = move.x > 0f;
					if (flag2)
					{
						move.x = 1f;
					}
				}
				bool buttonDown2 = this.input.GetButtonDown("Vertical");
				if (buttonDown2)
				{
					bool flag3 = move.y < 0f;
					if (flag3)
					{
						move.y = -1f;
					}
					bool flag4 = move.y > 0f;
					if (flag4)
					{
						move.y = 1f;
					}
				}
				return move;
			}

			// Token: 0x06000B5F RID: 2911 RVA: 0x00037C78 File Offset: 0x00035E78
			private bool ShouldSendMoveFromInput()
			{
				float time = this.input.unscaledTime;
				Vector2 movement = this.GetRawMoveVector();
				bool flag = Mathf.Approximately(movement.x, 0f) && Mathf.Approximately(movement.y, 0f);
				bool flag2;
				if (flag)
				{
					this.m_ConsecutiveMoveCount = 0;
					this.m_IsMoveFromKeyboard = false;
					flag2 = false;
				}
				else
				{
					bool allow = this.input.GetButtonDown("Horizontal") || this.input.GetButtonDown("Vertical");
					bool similarDir = Vector2.Dot(movement, this.m_LastMoveVector) > 0f;
					bool flag3 = !allow;
					if (flag3)
					{
						bool flag4 = similarDir && this.m_ConsecutiveMoveCount == 1;
						if (flag4)
						{
							allow = time > this.m_PrevActionTime + 0.5f;
						}
						else
						{
							allow = time > this.m_PrevActionTime + 0.1f;
						}
					}
					bool flag5 = !allow;
					if (flag5)
					{
						flag2 = false;
					}
					else
					{
						NavigationMoveEvent.Direction moveDirection = NavigationMoveEvent.DetermineMoveDirection(movement.x, movement.y, 0.6f);
						bool flag6 = moveDirection > NavigationMoveEvent.Direction.None;
						if (flag6)
						{
							bool flag7 = !similarDir;
							if (flag7)
							{
								this.m_ConsecutiveMoveCount = 0;
							}
							this.m_ConsecutiveMoveCount++;
							this.m_PrevActionTime = time;
							this.m_LastMoveVector = movement;
							this.m_IsMoveFromKeyboard |= this.input.anyKey;
						}
						else
						{
							this.m_ConsecutiveMoveCount = 0;
							this.m_IsMoveFromKeyboard = false;
						}
						flag2 = moveDirection > NavigationMoveEvent.Direction.None;
					}
				}
				return flag2;
			}

			// Token: 0x06000B60 RID: 2912 RVA: 0x00037DF4 File Offset: 0x00035FF4
			private void ProcessTabEvent(Event e, EventModifiers modifiers)
			{
				bool flag = e.ShouldSendNavigationMoveEventRuntime();
				if (flag)
				{
					NavigationMoveEvent.Direction direction = (e.shift ? NavigationMoveEvent.Direction.Previous : NavigationMoveEvent.Direction.Next);
					this.m_EventSystem.SendFocusBasedEvent<ValueTuple<NavigationMoveEvent.Direction, EventModifiers, DefaultEventSystem.LegacyInputProcessor.IInput>>(([TupleElementNames(new string[] { "direction", "modifiers", "input" })] ValueTuple<NavigationMoveEvent.Direction, EventModifiers, DefaultEventSystem.LegacyInputProcessor.IInput> t) => NavigationMoveEvent.GetPooled(t.Item1, t.Item3.anyKey ? NavigationDeviceType.Keyboard : NavigationDeviceType.NonKeyboard, t.Item2), new ValueTuple<NavigationMoveEvent.Direction, EventModifiers, DefaultEventSystem.LegacyInputProcessor.IInput>(direction, modifiers, this.input));
				}
			}

			// Token: 0x0400074B RID: 1867
			private bool m_SendingTouchEvents;

			// Token: 0x0400074C RID: 1868
			private bool m_SendingPenEvent;

			// Token: 0x0400074D RID: 1869
			private EventModifiers m_CurrentModifiers;

			// Token: 0x0400074E RID: 1870
			private int m_LastMousePressButton = -1;

			// Token: 0x0400074F RID: 1871
			private float m_NextMousePressTime = 0f;

			// Token: 0x04000750 RID: 1872
			private int m_LastMouseClickCount = 0;

			// Token: 0x04000751 RID: 1873
			private Vector2 m_LastMousePosition = Vector2.zero;

			// Token: 0x04000752 RID: 1874
			private bool m_MouseProcessedAtLeastOnce;

			// Token: 0x04000753 RID: 1875
			private DefaultEventSystem.LegacyInputProcessor.IInput m_Input;

			// Token: 0x04000754 RID: 1876
			private readonly Event m_Event = new Event();

			// Token: 0x04000755 RID: 1877
			private readonly DefaultEventSystem m_EventSystem;

			// Token: 0x04000756 RID: 1878
			private int m_ConsecutiveMoveCount;

			// Token: 0x04000757 RID: 1879
			private Vector2 m_LastMoveVector;

			// Token: 0x04000758 RID: 1880
			private float m_PrevActionTime;

			// Token: 0x04000759 RID: 1881
			private bool m_IsMoveFromKeyboard;

			// Token: 0x02000183 RID: 387
			internal interface IInput
			{
				// Token: 0x06000B61 RID: 2913
				bool GetButtonDown(string button);

				// Token: 0x06000B62 RID: 2914
				float GetAxisRaw(string axis);

				// Token: 0x06000B63 RID: 2915
				void ClearLastPenContactEvent();

				// Token: 0x06000B64 RID: 2916
				PenData GetLastPenContactEvent();

				// Token: 0x170001FD RID: 509
				// (get) Token: 0x06000B65 RID: 2917
				int touchCount { get; }

				// Token: 0x06000B66 RID: 2918
				Touch GetTouch(int index);

				// Token: 0x170001FE RID: 510
				// (get) Token: 0x06000B67 RID: 2919
				bool mousePresent { get; }

				// Token: 0x06000B68 RID: 2920
				bool GetMouseButtonDown(int button);

				// Token: 0x06000B69 RID: 2921
				bool GetMouseButtonUp(int button);

				// Token: 0x170001FF RID: 511
				// (get) Token: 0x06000B6A RID: 2922
				Vector3 mousePosition { get; }

				// Token: 0x17000200 RID: 512
				// (get) Token: 0x06000B6B RID: 2923
				int mouseButtonCount { get; }

				// Token: 0x17000201 RID: 513
				// (get) Token: 0x06000B6C RID: 2924
				bool anyKey { get; }

				// Token: 0x17000202 RID: 514
				// (get) Token: 0x06000B6D RID: 2925
				float unscaledTime { get; }

				// Token: 0x17000203 RID: 515
				// (get) Token: 0x06000B6E RID: 2926
				float doubleClickTime { get; }
			}

			// Token: 0x02000184 RID: 388
			private class Input : DefaultEventSystem.LegacyInputProcessor.IInput
			{
				// Token: 0x06000B6F RID: 2927 RVA: 0x00037E53 File Offset: 0x00036053
				public bool GetButtonDown(string button)
				{
					return UnityEngine.Input.GetButtonDown(button);
				}

				// Token: 0x06000B70 RID: 2928 RVA: 0x00037E5B File Offset: 0x0003605B
				public float GetAxisRaw(string axis)
				{
					return UnityEngine.Input.GetAxis(axis);
				}

				// Token: 0x06000B71 RID: 2929 RVA: 0x00037E63 File Offset: 0x00036063
				public void ClearLastPenContactEvent()
				{
					UnityEngine.Input.ClearLastPenContactEvent();
				}

				// Token: 0x06000B72 RID: 2930 RVA: 0x00037E6B File Offset: 0x0003606B
				public PenData GetLastPenContactEvent()
				{
					return UnityEngine.Input.GetLastPenContactEvent();
				}

				// Token: 0x17000204 RID: 516
				// (get) Token: 0x06000B73 RID: 2931 RVA: 0x00037E72 File Offset: 0x00036072
				public int touchCount
				{
					get
					{
						return UnityEngine.Input.touchCount;
					}
				}

				// Token: 0x06000B74 RID: 2932 RVA: 0x00037E79 File Offset: 0x00036079
				public Touch GetTouch(int index)
				{
					return UnityEngine.Input.GetTouch(index);
				}

				// Token: 0x17000205 RID: 517
				// (get) Token: 0x06000B75 RID: 2933 RVA: 0x00037E81 File Offset: 0x00036081
				public bool mousePresent
				{
					get
					{
						return UnityEngine.Input.mousePresent;
					}
				}

				// Token: 0x06000B76 RID: 2934 RVA: 0x00037E88 File Offset: 0x00036088
				public bool GetMouseButtonDown(int button)
				{
					return UnityEngine.Input.GetMouseButtonDown(button);
				}

				// Token: 0x06000B77 RID: 2935 RVA: 0x00037E90 File Offset: 0x00036090
				public bool GetMouseButtonUp(int button)
				{
					return UnityEngine.Input.GetMouseButtonUp(button);
				}

				// Token: 0x17000206 RID: 518
				// (get) Token: 0x06000B78 RID: 2936 RVA: 0x00037E98 File Offset: 0x00036098
				public Vector3 mousePosition
				{
					get
					{
						return UnityEngine.Input.mousePosition;
					}
				}

				// Token: 0x17000207 RID: 519
				// (get) Token: 0x06000B79 RID: 2937 RVA: 0x00037E9F File Offset: 0x0003609F
				public int mouseButtonCount
				{
					get
					{
						return 3;
					}
				}

				// Token: 0x17000208 RID: 520
				// (get) Token: 0x06000B7A RID: 2938 RVA: 0x00037EA2 File Offset: 0x000360A2
				public bool anyKey
				{
					get
					{
						return UnityEngine.Input.anyKey;
					}
				}

				// Token: 0x17000209 RID: 521
				// (get) Token: 0x06000B7B RID: 2939 RVA: 0x00037EA9 File Offset: 0x000360A9
				public float unscaledTime
				{
					get
					{
						return Time.unscaledTime;
					}
				}

				// Token: 0x1700020A RID: 522
				// (get) Token: 0x06000B7C RID: 2940 RVA: 0x00037EB0 File Offset: 0x000360B0
				public float doubleClickTime
				{
					get
					{
						return (float)Event.GetDoubleClickTime() * 0.001f;
					}
				}
			}

			// Token: 0x02000185 RID: 389
			private class NoInput : DefaultEventSystem.LegacyInputProcessor.IInput
			{
				// Token: 0x06000B7E RID: 2942 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
				public bool GetButtonDown(string button)
				{
					return false;
				}

				// Token: 0x06000B7F RID: 2943 RVA: 0x00037EBE File Offset: 0x000360BE
				public float GetAxisRaw(string axis)
				{
					return 0f;
				}

				// Token: 0x1700020B RID: 523
				// (get) Token: 0x06000B80 RID: 2944 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
				public int touchCount
				{
					get
					{
						return 0;
					}
				}

				// Token: 0x06000B81 RID: 2945 RVA: 0x00037EC8 File Offset: 0x000360C8
				public Touch GetTouch(int index)
				{
					return default(Touch);
				}

				// Token: 0x06000B82 RID: 2946 RVA: 0x000020EA File Offset: 0x000002EA
				public void ClearLastPenContactEvent()
				{
				}

				// Token: 0x06000B83 RID: 2947 RVA: 0x00037EE0 File Offset: 0x000360E0
				public PenData GetLastPenContactEvent()
				{
					return default(PenData);
				}

				// Token: 0x1700020C RID: 524
				// (get) Token: 0x06000B84 RID: 2948 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
				public bool mousePresent
				{
					get
					{
						return false;
					}
				}

				// Token: 0x06000B85 RID: 2949 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
				public bool GetMouseButtonDown(int button)
				{
					return false;
				}

				// Token: 0x06000B86 RID: 2950 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
				public bool GetMouseButtonUp(int button)
				{
					return false;
				}

				// Token: 0x1700020D RID: 525
				// (get) Token: 0x06000B87 RID: 2951 RVA: 0x00037EF8 File Offset: 0x000360F8
				public Vector3 mousePosition
				{
					get
					{
						return default(Vector3);
					}
				}

				// Token: 0x1700020E RID: 526
				// (get) Token: 0x06000B88 RID: 2952 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
				public int mouseButtonCount
				{
					get
					{
						return 0;
					}
				}

				// Token: 0x1700020F RID: 527
				// (get) Token: 0x06000B89 RID: 2953 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
				public bool anyKey
				{
					get
					{
						return false;
					}
				}

				// Token: 0x17000210 RID: 528
				// (get) Token: 0x06000B8A RID: 2954 RVA: 0x00037EBE File Offset: 0x000360BE
				public float unscaledTime
				{
					get
					{
						return 0f;
					}
				}

				// Token: 0x17000211 RID: 529
				// (get) Token: 0x06000B8B RID: 2955 RVA: 0x00037F0E File Offset: 0x0003610E
				public float doubleClickTime
				{
					get
					{
						return float.PositiveInfinity;
					}
				}
			}
		}
	}
}
