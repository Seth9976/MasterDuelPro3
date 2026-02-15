using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Rendering.UI;

namespace UnityEngine.Rendering
{
	// Token: 0x02000091 RID: 145
	public sealed class DebugManager
	{
		// Token: 0x060005A0 RID: 1440 RVA: 0x0000C460 File Offset: 0x0000A660
		private void RegisterActions()
		{
			this.m_DebugActions = new DebugActionDesc[9];
			this.m_DebugActionStates = new DebugActionState[9];
			this.AddAction(DebugAction.EnableDebugMenu, new DebugActionDesc
			{
				buttonAction = this.debugActionMap.FindAction("Enable Debug", false),
				repeatMode = DebugActionRepeatMode.Never
			});
			this.AddAction(DebugAction.ResetAll, new DebugActionDesc
			{
				buttonAction = this.debugActionMap.FindAction("Debug Reset", false),
				repeatMode = DebugActionRepeatMode.Never
			});
			this.AddAction(DebugAction.NextDebugPanel, new DebugActionDesc
			{
				buttonAction = this.debugActionMap.FindAction("Debug Next", false),
				repeatMode = DebugActionRepeatMode.Never
			});
			this.AddAction(DebugAction.PreviousDebugPanel, new DebugActionDesc
			{
				buttonAction = this.debugActionMap.FindAction("Debug Previous", false),
				repeatMode = DebugActionRepeatMode.Never
			});
			DebugActionDesc validate = new DebugActionDesc();
			validate.buttonAction = this.debugActionMap.FindAction("Debug Validate", false);
			validate.repeatMode = DebugActionRepeatMode.Never;
			this.AddAction(DebugAction.Action, validate);
			this.AddAction(DebugAction.MakePersistent, new DebugActionDesc
			{
				buttonAction = this.debugActionMap.FindAction("Debug Persistent", false),
				repeatMode = DebugActionRepeatMode.Never
			});
			DebugActionDesc multiplier = new DebugActionDesc();
			multiplier.buttonAction = this.debugActionMap.FindAction("Debug Multiplier", false);
			multiplier.repeatMode = DebugActionRepeatMode.Delay;
			validate.repeatDelay = 0f;
			this.AddAction(DebugAction.Multiplier, multiplier);
			this.AddAction(DebugAction.MoveVertical, new DebugActionDesc
			{
				buttonAction = this.debugActionMap.FindAction("Debug Vertical", false),
				repeatMode = DebugActionRepeatMode.Delay,
				repeatDelay = 0.16f
			});
			this.AddAction(DebugAction.MoveHorizontal, new DebugActionDesc
			{
				buttonAction = this.debugActionMap.FindAction("Debug Horizontal", false),
				repeatMode = DebugActionRepeatMode.Delay,
				repeatDelay = 0.16f
			});
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0000C64C File Offset: 0x0000A84C
		internal void EnableInputActions()
		{
			foreach (InputAction inputAction in this.debugActionMap)
			{
				inputAction.Enable();
			}
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0000C698 File Offset: 0x0000A898
		private void AddAction(DebugAction action, DebugActionDesc desc)
		{
			this.m_DebugActions[(int)action] = desc;
			this.m_DebugActionStates[(int)action] = new DebugActionState();
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0000C6C0 File Offset: 0x0000A8C0
		private void SampleAction(int actionIndex)
		{
			DebugActionDesc desc = this.m_DebugActions[actionIndex];
			DebugActionState state = this.m_DebugActionStates[actionIndex];
			if (!state.runningAction && desc.buttonAction != null)
			{
				float value = desc.buttonAction.ReadValue<float>();
				if (!Mathf.Approximately(value, 0f))
				{
					state.TriggerWithButton(desc.buttonAction, value);
				}
			}
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0000C718 File Offset: 0x0000A918
		private void UpdateAction(int actionIndex)
		{
			DebugActionDesc desc = this.m_DebugActions[actionIndex];
			DebugActionState state = this.m_DebugActionStates[actionIndex];
			if (state.runningAction)
			{
				state.Update(desc);
			}
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0000C748 File Offset: 0x0000A948
		internal void UpdateActions()
		{
			for (int actionIndex = 0; actionIndex < this.m_DebugActions.Length; actionIndex++)
			{
				this.UpdateAction(actionIndex);
				this.SampleAction(actionIndex);
			}
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0000C776 File Offset: 0x0000A976
		internal float GetAction(DebugAction action)
		{
			return this.m_DebugActionStates[(int)action].actionState;
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0000C788 File Offset: 0x0000A988
		internal bool GetActionToggleDebugMenuWithTouch()
		{
			if (!EnhancedTouchSupport.enabled)
			{
				return false;
			}
			ReadOnlyArray<Touch> touches = Touch.activeTouches;
			int count = touches.Count;
			TouchPhase? expectedTouchPhase = null;
			if (count == 3)
			{
				foreach (Touch touch in touches)
				{
					if ((expectedTouchPhase == null || touch.phase == expectedTouchPhase.Value) && touch.tapCount == 2)
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0000C820 File Offset: 0x0000AA20
		internal bool GetActionReleaseScrollTarget()
		{
			bool flag = Mouse.current != null && Mouse.current.scroll.ReadValue() != Vector2.zero;
			bool touchSupported = Touchscreen.current != null;
			return flag || touchSupported;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0000C85C File Offset: 0x0000AA5C
		private void RegisterInputs()
		{
			this.debugActionMap.AddAction("Enable Debug", InputActionType.Button, null, null, null, null, null).AddCompositeBinding("ButtonWithOneModifier", null, null).With("Modifier", "<Gamepad>/rightStickPress", null, null)
				.With("Button", "<Gamepad>/leftStickPress", null, null)
				.With("Modifier", "<Keyboard>/leftCtrl", null, null)
				.With("Button", "<Keyboard>/backspace", null, null);
			this.debugActionMap.AddAction("Debug Reset", InputActionType.Button, null, null, null, null, null).AddCompositeBinding("ButtonWithOneModifier", null, null).With("Modifier", "<Gamepad>/rightStickPress", null, null)
				.With("Button", "<Gamepad>/b", null, null)
				.With("Modifier", "<Keyboard>/leftAlt", null, null)
				.With("Button", "<Keyboard>/backspace", null, null);
			InputAction inputAction = this.debugActionMap.AddAction("Debug Next", InputActionType.Button, null, null, null, null, null);
			inputAction.AddBinding("<Keyboard>/pageDown", null, null, null);
			inputAction.AddBinding("<Gamepad>/rightShoulder", null, null, null);
			InputAction inputAction2 = this.debugActionMap.AddAction("Debug Previous", InputActionType.Button, null, null, null, null, null);
			inputAction2.AddBinding("<Keyboard>/pageUp", null, null, null);
			inputAction2.AddBinding("<Gamepad>/leftShoulder", null, null, null);
			InputAction inputAction3 = this.debugActionMap.AddAction("Debug Validate", InputActionType.Button, null, null, null, null, null);
			inputAction3.AddBinding("<Keyboard>/enter", null, null, null);
			inputAction3.AddBinding("<Gamepad>/a", null, null, null);
			InputAction inputAction4 = this.debugActionMap.AddAction("Debug Persistent", InputActionType.Button, null, null, null, null, null);
			inputAction4.AddBinding("<Keyboard>/rightShift", null, null, null);
			inputAction4.AddBinding("<Gamepad>/x", null, null, null);
			InputAction inputAction5 = this.debugActionMap.AddAction("Debug Multiplier", InputActionType.Value, null, null, null, null, null);
			inputAction5.AddBinding("<Keyboard>/leftShift", null, null, null);
			inputAction5.AddBinding("<Gamepad>/y", null, null, null);
			this.debugActionMap.AddAction("Debug Vertical", InputActionType.Value, null, null, null, null, null).AddCompositeBinding("1DAxis", null, null).With("Positive", "<Gamepad>/dpad/up", null, null)
				.With("Negative", "<Gamepad>/dpad/down", null, null)
				.With("Positive", "<Keyboard>/upArrow", null, null)
				.With("Negative", "<Keyboard>/downArrow", null, null);
			this.debugActionMap.AddAction("Debug Horizontal", InputActionType.Value, null, null, null, null, null).AddCompositeBinding("1DAxis", null, null).With("Positive", "<Gamepad>/dpad/right", null, null)
				.With("Negative", "<Gamepad>/dpad/left", null, null)
				.With("Positive", "<Keyboard>/rightArrow", null, null)
				.With("Negative", "<Keyboard>/leftArrow", null, null);
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060005AA RID: 1450 RVA: 0x0000CB34 File Offset: 0x0000AD34
		// (remove) Token: 0x060005AB RID: 1451 RVA: 0x0000CB68 File Offset: 0x0000AD68
		public static event Action<DebugManager.UIMode, bool> windowStateChanged;

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x0000CB9B File Offset: 0x0000AD9B
		// (set) Token: 0x060005AD RID: 1453 RVA: 0x0000CBA8 File Offset: 0x0000ADA8
		public bool displayEditorUI
		{
			get
			{
				return this.editorUIState.open;
			}
			set
			{
				this.editorUIState.open = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x0000CBB6 File Offset: 0x0000ADB6
		// (set) Token: 0x060005AF RID: 1455 RVA: 0x0000CBBE File Offset: 0x0000ADBE
		public bool enableRuntimeUI
		{
			get
			{
				return this.m_EnableRuntimeUI;
			}
			set
			{
				if (value != this.m_EnableRuntimeUI)
				{
					this.m_EnableRuntimeUI = value;
					DebugUpdater.SetEnabled(value);
				}
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0000CBD6 File Offset: 0x0000ADD6
		// (set) Token: 0x060005B1 RID: 1457 RVA: 0x0000CBF4 File Offset: 0x0000ADF4
		public bool displayRuntimeUI
		{
			get
			{
				return this.m_Root != null && this.m_Root.activeInHierarchy;
			}
			set
			{
				if (value)
				{
					this.m_Root = Object.Instantiate<Transform>(Resources.Load<Transform>("DebugUICanvas")).gameObject;
					this.m_Root.name = "[Debug Canvas]";
					this.m_Root.transform.localPosition = Vector3.zero;
					this.m_RootUICanvas = this.m_Root.GetComponent<DebugUIHandlerCanvas>();
					this.m_Root.SetActive(true);
				}
				else
				{
					CoreUtils.Destroy(this.m_Root);
					this.m_Root = null;
					this.m_RootUICanvas = null;
				}
				this.onDisplayRuntimeUIChanged(value);
				DebugUpdater.HandleInternalEventSystemComponents(value);
				this.runtimeUIState.open = this.m_Root != null && this.m_Root.activeInHierarchy;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0000CCB4 File Offset: 0x0000AEB4
		// (set) Token: 0x060005B3 RID: 1459 RVA: 0x0000CCD1 File Offset: 0x0000AED1
		public bool displayPersistentRuntimeUI
		{
			get
			{
				return this.m_RootUIPersistentCanvas != null && this.m_PersistentRoot.activeInHierarchy;
			}
			set
			{
				if (value)
				{
					this.EnsurePersistentCanvas();
					return;
				}
				CoreUtils.Destroy(this.m_PersistentRoot);
				this.m_PersistentRoot = null;
				this.m_RootUIPersistentCanvas = null;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0000CCF6 File Offset: 0x0000AEF6
		public static DebugManager instance
		{
			get
			{
				return DebugManager.s_Instance.Value;
			}
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0000CD02 File Offset: 0x0000AF02
		private void UpdateReadOnlyCollection()
		{
			this.m_Panels.Sort();
			this.m_ReadOnlyPanels = this.m_Panels.AsReadOnly();
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x0000CD20 File Offset: 0x0000AF20
		public ReadOnlyCollection<DebugUI.Panel> panels
		{
			get
			{
				if (this.m_ReadOnlyPanels == null)
				{
					this.UpdateReadOnlyCollection();
				}
				return this.m_ReadOnlyPanels;
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060005B7 RID: 1463 RVA: 0x0000CD38 File Offset: 0x0000AF38
		// (remove) Token: 0x060005B8 RID: 1464 RVA: 0x0000CD70 File Offset: 0x0000AF70
		public event Action<bool> onDisplayRuntimeUIChanged = delegate
		{
		};

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060005B9 RID: 1465 RVA: 0x0000CDA8 File Offset: 0x0000AFA8
		// (remove) Token: 0x060005BA RID: 1466 RVA: 0x0000CDE0 File Offset: 0x0000AFE0
		public event Action onSetDirty = delegate
		{
		};

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060005BB RID: 1467 RVA: 0x0000CE18 File Offset: 0x0000B018
		// (remove) Token: 0x060005BC RID: 1468 RVA: 0x0000CE50 File Offset: 0x0000B050
		private event Action resetData;

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x0000CE85 File Offset: 0x0000B085
		public bool isAnyDebugUIActive
		{
			get
			{
				return this.displayRuntimeUI || this.displayPersistentRuntimeUI;
			}
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0000CE98 File Offset: 0x0000B098
		private DebugManager()
		{
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0000CF3B File Offset: 0x0000B13B
		public void RefreshEditor()
		{
			this.refreshEditorRequested = true;
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0000CF44 File Offset: 0x0000B144
		public void Reset()
		{
			Action action = this.resetData;
			if (action != null)
			{
				action();
			}
			this.ReDrawOnScreenDebug();
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0000CF5D File Offset: 0x0000B15D
		public void ReDrawOnScreenDebug()
		{
			if (this.displayRuntimeUI)
			{
				DebugUIHandlerCanvas rootUICanvas = this.m_RootUICanvas;
				if (rootUICanvas == null)
				{
					return;
				}
				rootUICanvas.RequestHierarchyReset();
			}
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0000CF77 File Offset: 0x0000B177
		public void RegisterData(IDebugData data)
		{
			this.resetData += data.GetReset();
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0000CF85 File Offset: 0x0000B185
		public void UnregisterData(IDebugData data)
		{
			this.resetData -= data.GetReset();
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0000CF94 File Offset: 0x0000B194
		public int GetState()
		{
			int hash = 17;
			foreach (DebugUI.Panel panel in this.m_Panels)
			{
				hash = hash * 23 + panel.GetHashCode();
			}
			return hash;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0000CFF0 File Offset: 0x0000B1F0
		internal void RegisterRootCanvas(DebugUIHandlerCanvas root)
		{
			this.m_Root = root.gameObject;
			this.m_RootUICanvas = root;
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0000D005 File Offset: 0x0000B205
		internal void ChangeSelection(DebugUIHandlerWidget widget, bool fromNext)
		{
			this.m_RootUICanvas.ChangeSelection(widget, fromNext);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0000D014 File Offset: 0x0000B214
		internal void SetScrollTarget(DebugUIHandlerWidget widget)
		{
			if (this.m_RootUICanvas != null)
			{
				this.m_RootUICanvas.SetScrollTarget(widget);
			}
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0000D030 File Offset: 0x0000B230
		private void EnsurePersistentCanvas()
		{
			if (this.m_RootUIPersistentCanvas == null)
			{
				DebugUIHandlerPersistentCanvas uiManager = Object.FindFirstObjectByType<DebugUIHandlerPersistentCanvas>();
				if (uiManager == null)
				{
					this.m_PersistentRoot = Object.Instantiate<Transform>(Resources.Load<Transform>("DebugUIPersistentCanvas")).gameObject;
					this.m_PersistentRoot.name = "[Debug Canvas - Persistent]";
					this.m_PersistentRoot.transform.localPosition = Vector3.zero;
				}
				else
				{
					this.m_PersistentRoot = uiManager.gameObject;
				}
				this.m_RootUIPersistentCanvas = this.m_PersistentRoot.GetComponent<DebugUIHandlerPersistentCanvas>();
			}
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0000D0B8 File Offset: 0x0000B2B8
		internal void TogglePersistent(DebugUI.Widget widget, int? forceTupleIndex = null)
		{
			if (widget == null)
			{
				return;
			}
			this.EnsurePersistentCanvas();
			DebugUI.Value value = widget as DebugUI.Value;
			if (value != null)
			{
				this.m_RootUIPersistentCanvas.Toggle(value, null);
				return;
			}
			DebugUI.ValueTuple valueTuple = widget as DebugUI.ValueTuple;
			if (valueTuple == null)
			{
				DebugUI.Container container = widget as DebugUI.Container;
				if (container != null)
				{
					int pinnedIndex = container.children.Max(delegate(DebugUI.Widget w)
					{
						DebugUI.ValueTuple valueTuple2 = w as DebugUI.ValueTuple;
						if (valueTuple2 == null)
						{
							return -1;
						}
						return valueTuple2.pinnedElementIndex;
					});
					using (IEnumerator<DebugUI.Widget> enumerator = container.children.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							DebugUI.Widget child = enumerator.Current;
							if (child is DebugUI.Value || child is DebugUI.ValueTuple)
							{
								this.TogglePersistent(child, new int?(pinnedIndex));
							}
						}
						return;
					}
				}
				Debug.Log("Only readonly items can be made persistent.");
				return;
			}
			this.m_RootUIPersistentCanvas.Toggle(valueTuple, forceTupleIndex);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0000D1A4 File Offset: 0x0000B3A4
		private void OnPanelDirty(DebugUI.Panel panel)
		{
			this.onSetDirty();
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0000D1B4 File Offset: 0x0000B3B4
		public int PanelIndex([DisallowNull] string displayName)
		{
			if (displayName == null)
			{
				displayName = string.Empty;
			}
			for (int i = 0; i < this.m_Panels.Count; i++)
			{
				if (displayName.Equals(this.m_Panels[i].displayName, StringComparison.InvariantCultureIgnoreCase))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0000D1FE File Offset: 0x0000B3FE
		public string PanelDiplayName([DisallowNull] int panelIndex)
		{
			if (panelIndex < 0 || panelIndex > this.m_Panels.Count - 1)
			{
				return string.Empty;
			}
			return this.m_Panels[panelIndex].displayName;
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0000D22B File Offset: 0x0000B42B
		public void RequestEditorWindowPanelIndex(int index)
		{
			this.m_RequestedPanelIndex = new int?(index);
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0000D239 File Offset: 0x0000B439
		internal int? GetRequestedEditorWindowPanelIndex()
		{
			int? requestedPanelIndex = this.m_RequestedPanelIndex;
			this.m_RequestedPanelIndex = null;
			return requestedPanelIndex;
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0000D250 File Offset: 0x0000B450
		public DebugUI.Panel GetPanel(string displayName, bool createIfNull = false, int groupIndex = 0, bool overrideIfExist = false)
		{
			int panelIndex = this.PanelIndex(displayName);
			DebugUI.Panel p = ((panelIndex >= 0) ? this.m_Panels[panelIndex] : null);
			if (p != null)
			{
				if (!overrideIfExist)
				{
					return p;
				}
				p.onSetDirty -= this.OnPanelDirty;
				this.RemovePanel(p);
				p = null;
			}
			if (createIfNull)
			{
				p = new DebugUI.Panel
				{
					displayName = displayName,
					groupIndex = groupIndex
				};
				p.onSetDirty += this.OnPanelDirty;
				this.m_Panels.Add(p);
				this.UpdateReadOnlyCollection();
			}
			return p;
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0000D2DC File Offset: 0x0000B4DC
		public int FindPanelIndex(string displayName)
		{
			return this.m_Panels.FindIndex((DebugUI.Panel p) => p.displayName == displayName);
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0000D310 File Offset: 0x0000B510
		public void RemovePanel(string displayName)
		{
			DebugUI.Panel panel = null;
			foreach (DebugUI.Panel p in this.m_Panels)
			{
				if (p.displayName == displayName)
				{
					p.onSetDirty -= this.OnPanelDirty;
					panel = p;
					break;
				}
			}
			this.RemovePanel(panel);
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0000D38C File Offset: 0x0000B58C
		public void RemovePanel(DebugUI.Panel panel)
		{
			if (panel == null)
			{
				return;
			}
			this.m_Panels.Remove(panel);
			this.UpdateReadOnlyCollection();
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0000D3A8 File Offset: 0x0000B5A8
		public DebugUI.Widget[] GetItems(DebugUI.Flags flags)
		{
			List<DebugUI.Widget> temp;
			DebugUI.Widget[] array;
			using (ListPool<DebugUI.Widget>.Get(out temp))
			{
				foreach (DebugUI.Panel panel in this.m_Panels)
				{
					DebugUI.Widget[] widgets = this.GetItemsFromContainer(flags, panel);
					temp.AddRange(widgets);
				}
				array = temp.ToArray();
			}
			return array;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0000D434 File Offset: 0x0000B634
		internal DebugUI.Widget[] GetItemsFromContainer(DebugUI.Flags flags, DebugUI.IContainer container)
		{
			List<DebugUI.Widget> temp;
			DebugUI.Widget[] array;
			using (ListPool<DebugUI.Widget>.Get(out temp))
			{
				foreach (DebugUI.Widget child in container.children)
				{
					if (child.flags.HasFlag(flags))
					{
						temp.Add(child);
					}
					else
					{
						DebugUI.IContainer containerChild = child as DebugUI.IContainer;
						if (containerChild != null)
						{
							temp.AddRange(this.GetItemsFromContainer(flags, containerChild));
						}
					}
				}
				array = temp.ToArray();
			}
			return array;
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0000D4E4 File Offset: 0x0000B6E4
		public DebugUI.Widget GetItem(string queryPath)
		{
			foreach (DebugUI.Panel panel in this.m_Panels)
			{
				DebugUI.Widget w = this.GetItem(queryPath, panel);
				if (w != null)
				{
					return w;
				}
			}
			return null;
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0000D544 File Offset: 0x0000B744
		private DebugUI.Widget GetItem(string queryPath, DebugUI.IContainer container)
		{
			foreach (DebugUI.Widget child in container.children)
			{
				if (child.queryPath == queryPath)
				{
					return child;
				}
				DebugUI.IContainer containerChild = child as DebugUI.IContainer;
				if (containerChild != null)
				{
					DebugUI.Widget w = this.GetItem(queryPath, containerChild);
					if (w != null)
					{
						return w;
					}
				}
			}
			return null;
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0000CBA8 File Offset: 0x0000ADA8
		[Obsolete("Use DebugManager.instance.displayEditorUI property instead. #from(23.1)")]
		public void ToggleEditorUI(bool open)
		{
			this.editorUIState.open = open;
		}

		// Token: 0x040001CA RID: 458
		private const string kEnableDebugBtn1 = "Enable Debug Button 1";

		// Token: 0x040001CB RID: 459
		private const string kEnableDebugBtn2 = "Enable Debug Button 2";

		// Token: 0x040001CC RID: 460
		private const string kDebugPreviousBtn = "Debug Previous";

		// Token: 0x040001CD RID: 461
		private const string kDebugNextBtn = "Debug Next";

		// Token: 0x040001CE RID: 462
		private const string kValidateBtn = "Debug Validate";

		// Token: 0x040001CF RID: 463
		private const string kPersistentBtn = "Debug Persistent";

		// Token: 0x040001D0 RID: 464
		private const string kDPadVertical = "Debug Vertical";

		// Token: 0x040001D1 RID: 465
		private const string kDPadHorizontal = "Debug Horizontal";

		// Token: 0x040001D2 RID: 466
		private const string kMultiplierBtn = "Debug Multiplier";

		// Token: 0x040001D3 RID: 467
		private const string kResetBtn = "Debug Reset";

		// Token: 0x040001D4 RID: 468
		private const string kEnableDebug = "Enable Debug";

		// Token: 0x040001D5 RID: 469
		private DebugActionDesc[] m_DebugActions;

		// Token: 0x040001D6 RID: 470
		private DebugActionState[] m_DebugActionStates;

		// Token: 0x040001D7 RID: 471
		private InputActionMap debugActionMap = new InputActionMap("Debug Menu");

		// Token: 0x040001D9 RID: 473
		private DebugManager.UIState editorUIState = new DebugManager.UIState
		{
			mode = DebugManager.UIMode.EditorMode
		};

		// Token: 0x040001DA RID: 474
		private bool m_EnableRuntimeUI = true;

		// Token: 0x040001DB RID: 475
		private DebugManager.UIState runtimeUIState = new DebugManager.UIState
		{
			mode = DebugManager.UIMode.RuntimeMode
		};

		// Token: 0x040001DC RID: 476
		private static readonly Lazy<DebugManager> s_Instance = new Lazy<DebugManager>(() => new DebugManager());

		// Token: 0x040001DD RID: 477
		private ReadOnlyCollection<DebugUI.Panel> m_ReadOnlyPanels;

		// Token: 0x040001DE RID: 478
		private readonly List<DebugUI.Panel> m_Panels = new List<DebugUI.Panel>();

		// Token: 0x040001E2 RID: 482
		public bool refreshEditorRequested;

		// Token: 0x040001E3 RID: 483
		private int? m_RequestedPanelIndex;

		// Token: 0x040001E4 RID: 484
		private GameObject m_Root;

		// Token: 0x040001E5 RID: 485
		private DebugUIHandlerCanvas m_RootUICanvas;

		// Token: 0x040001E6 RID: 486
		private GameObject m_PersistentRoot;

		// Token: 0x040001E7 RID: 487
		private DebugUIHandlerPersistentCanvas m_RootUIPersistentCanvas;

		// Token: 0x02000092 RID: 146
		public enum UIMode
		{
			// Token: 0x040001E9 RID: 489
			EditorMode,
			// Token: 0x040001EA RID: 490
			RuntimeMode
		}

		// Token: 0x02000093 RID: 147
		private class UIState
		{
			// Token: 0x1700005B RID: 91
			// (get) Token: 0x060005D9 RID: 1497 RVA: 0x0000D5D8 File Offset: 0x0000B7D8
			// (set) Token: 0x060005DA RID: 1498 RVA: 0x0000D5E0 File Offset: 0x0000B7E0
			public bool open
			{
				get
				{
					return this.m_Open;
				}
				set
				{
					if (this.m_Open == value)
					{
						return;
					}
					this.m_Open = value;
					Action<DebugManager.UIMode, bool> windowStateChanged = DebugManager.windowStateChanged;
					if (windowStateChanged == null)
					{
						return;
					}
					windowStateChanged(this.mode, this.m_Open);
				}
			}

			// Token: 0x040001EB RID: 491
			public DebugManager.UIMode mode;

			// Token: 0x040001EC RID: 492
			[SerializeField]
			private bool m_Open;
		}
	}
}
