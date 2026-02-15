using System;
using System.Runtime.CompilerServices;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnityEngine.UIElements
{
	// Token: 0x02000092 RID: 146
	[AddComponentMenu("UI Toolkit/Panel Event Handler (UI Toolkit)")]
	public class PanelEventHandler : UIBehaviour, IPointerMoveHandler, IEventSystemHandler, IPointerUpHandler, IPointerDownHandler, ISubmitHandler, ICancelHandler, IMoveHandler, IScrollHandler, ISelectHandler, IDeselectHandler, IPointerExitHandler, IPointerEnterHandler, IRuntimePanelComponent, IPointerClickHandler
	{
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x00017B93 File Offset: 0x00015D93
		// (set) Token: 0x06000589 RID: 1417 RVA: 0x00017B9C File Offset: 0x00015D9C
		public IPanel panel
		{
			get
			{
				return this.m_Panel;
			}
			set
			{
				BaseRuntimePanel newPanel = (BaseRuntimePanel)value;
				if (this.m_Panel != newPanel)
				{
					this.UnregisterCallbacks();
					this.m_Panel = newPanel;
					this.RegisterCallbacks();
				}
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x00017BCC File Offset: 0x00015DCC
		private GameObject selectableGameObject
		{
			get
			{
				BaseRuntimePanel panel = this.m_Panel;
				if (panel == null)
				{
					return null;
				}
				return panel.selectableGameObject;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x00017BDF File Offset: 0x00015DDF
		private EventSystem eventSystem
		{
			get
			{
				return UIElementsRuntimeUtility.activeEventSystem as EventSystem;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x00017BEB File Offset: 0x00015DEB
		private bool isCurrentFocusedPanel
		{
			get
			{
				return this.m_Panel != null && this.eventSystem != null && this.eventSystem.currentSelectedGameObject == this.selectableGameObject;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x00017C1B File Offset: 0x00015E1B
		private Focusable currentFocusedElement
		{
			get
			{
				BaseRuntimePanel panel = this.m_Panel;
				if (panel == null)
				{
					return null;
				}
				return panel.focusController.GetLeafFocusedElement();
			}
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00017C33 File Offset: 0x00015E33
		protected override void OnEnable()
		{
			base.OnEnable();
			this.RegisterCallbacks();
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00017C41 File Offset: 0x00015E41
		protected override void OnDisable()
		{
			base.OnDisable();
			this.UnregisterCallbacks();
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00017C50 File Offset: 0x00015E50
		private void RegisterCallbacks()
		{
			if (this.m_Panel != null)
			{
				this.m_Panel.destroyed += this.OnPanelDestroyed;
				this.m_Panel.visualTree.RegisterCallback<FocusEvent>(new EventCallback<FocusEvent>(this.OnElementFocus), TrickleDown.TrickleDown);
				this.m_Panel.visualTree.RegisterCallback<BlurEvent>(new EventCallback<BlurEvent>(this.OnElementBlur), TrickleDown.TrickleDown);
			}
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00017CB8 File Offset: 0x00015EB8
		private void UnregisterCallbacks()
		{
			if (this.m_Panel != null)
			{
				this.m_Panel.destroyed -= this.OnPanelDestroyed;
				this.m_Panel.visualTree.UnregisterCallback<FocusEvent>(new EventCallback<FocusEvent>(this.OnElementFocus), TrickleDown.TrickleDown);
				this.m_Panel.visualTree.UnregisterCallback<BlurEvent>(new EventCallback<BlurEvent>(this.OnElementBlur), TrickleDown.TrickleDown);
			}
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00017D1E File Offset: 0x00015F1E
		private void OnPanelDestroyed()
		{
			this.panel = null;
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00017D27 File Offset: 0x00015F27
		private void OnElementFocus(FocusEvent e)
		{
			if (!this.m_Selecting && this.eventSystem != null)
			{
				this.eventSystem.SetSelectedGameObject(this.selectableGameObject);
			}
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00002209 File Offset: 0x00000409
		private void OnElementBlur(BlurEvent e)
		{
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00017D50 File Offset: 0x00015F50
		public void OnSelect(BaseEventData eventData)
		{
			this.m_Selecting = true;
			try
			{
				BaseRuntimePanel panel = this.m_Panel;
				if (panel != null)
				{
					panel.Focus();
				}
			}
			finally
			{
				this.m_Selecting = false;
			}
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00017D90 File Offset: 0x00015F90
		public void OnDeselect(BaseEventData eventData)
		{
			BaseRuntimePanel panel = this.m_Panel;
			if (panel == null)
			{
				return;
			}
			panel.Blur();
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00017DA4 File Offset: 0x00015FA4
		public void OnPointerMove(PointerEventData eventData)
		{
			if (this.m_Panel == null || !this.ReadPointerData(this.m_PointerEvent, eventData, PanelEventHandler.PointerEventType.Default))
			{
				return;
			}
			using (PointerMoveEvent e = PointerEventBase<PointerMoveEvent>.GetPooled(this.m_PointerEvent))
			{
				this.SendEvent(e, eventData);
			}
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00017DFC File Offset: 0x00015FFC
		public void OnPointerUp(PointerEventData eventData)
		{
			if (this.m_Panel == null || !this.ReadPointerData(this.m_PointerEvent, eventData, PanelEventHandler.PointerEventType.Up))
			{
				return;
			}
			using (PointerUpEvent e = PointerEventBase<PointerUpEvent>.GetPooled(this.m_PointerEvent))
			{
				this.SendEvent(e, eventData);
				if (e.pressedButtons == 0)
				{
					PointerDeviceState.SetPlayerPanelWithSoftPointerCapture(e.pointerId, null);
				}
			}
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00017E68 File Offset: 0x00016068
		public void OnPointerDown(PointerEventData eventData)
		{
			if (this.m_Panel == null || !this.ReadPointerData(this.m_PointerEvent, eventData, PanelEventHandler.PointerEventType.Down))
			{
				return;
			}
			if (this.eventSystem != null)
			{
				this.eventSystem.SetSelectedGameObject(this.selectableGameObject);
			}
			using (PointerDownEvent e = PointerEventBase<PointerDownEvent>.GetPooled(this.m_PointerEvent))
			{
				this.SendEvent(e, eventData);
				PointerDeviceState.SetPlayerPanelWithSoftPointerCapture(e.pointerId, this.m_Panel);
			}
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00017EF0 File Offset: 0x000160F0
		public void OnPointerExit(PointerEventData eventData)
		{
			if (this.m_Panel == null || !this.ReadPointerData(this.m_PointerEvent, eventData, PanelEventHandler.PointerEventType.Default))
			{
				return;
			}
			if (eventData.pointerCurrentRaycast.gameObject == base.gameObject && eventData.pointerPressRaycast.gameObject != base.gameObject && this.m_PointerEvent.pointerId != PointerId.mousePointerId)
			{
				using (PointerCancelEvent e = PointerEventBase<PointerCancelEvent>.GetPooled(this.m_PointerEvent))
				{
					this.SendEvent(e, eventData);
					if (e.pressedButtons == 0)
					{
						PointerDeviceState.SetPlayerPanelWithSoftPointerCapture(e.pointerId, null);
					}
				}
			}
			this.m_Panel.PointerLeavesPanel(this.m_PointerEvent.pointerId, this.m_PointerEvent.position);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00017FC8 File Offset: 0x000161C8
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (this.m_Panel == null || !this.ReadPointerData(this.m_PointerEvent, eventData, PanelEventHandler.PointerEventType.Default))
			{
				return;
			}
			this.m_Panel.PointerEntersPanel(this.m_PointerEvent.pointerId, this.m_PointerEvent.position);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00018014 File Offset: 0x00016214
		public void OnPointerClick(PointerEventData eventData)
		{
			this.m_LastClickTime = Time.unscaledTime;
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00018024 File Offset: 0x00016224
		public void OnSubmit(BaseEventData eventData)
		{
			if (this.m_Panel == null)
			{
				return;
			}
			Focusable target = this.currentFocusedElement ?? this.m_Panel.visualTree;
			this.ProcessImguiEvents(target);
			using (NavigationSubmitEvent e = NavigationEventBase<NavigationSubmitEvent>.GetPooled(PanelEventHandler.s_Modifiers))
			{
				e.target = target;
				this.SendEvent(e, eventData);
			}
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00018090 File Offset: 0x00016290
		public void OnCancel(BaseEventData eventData)
		{
			if (this.m_Panel == null)
			{
				return;
			}
			Focusable target = this.currentFocusedElement ?? this.m_Panel.visualTree;
			this.ProcessImguiEvents(target);
			using (NavigationCancelEvent e = NavigationEventBase<NavigationCancelEvent>.GetPooled(PanelEventHandler.s_Modifiers))
			{
				e.target = target;
				this.SendEvent(e, eventData);
			}
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x000180FC File Offset: 0x000162FC
		public void OnMove(AxisEventData eventData)
		{
			if (this.m_Panel == null)
			{
				return;
			}
			Focusable target = this.currentFocusedElement ?? this.m_Panel.visualTree;
			this.ProcessImguiEvents(target);
			using (NavigationMoveEvent e = NavigationMoveEvent.GetPooled(eventData.moveVector, PanelEventHandler.s_Modifiers))
			{
				e.target = target;
				this.SendEvent(e, eventData);
			}
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0001816C File Offset: 0x0001636C
		public void OnScroll(PointerEventData eventData)
		{
			if (this.m_Panel == null || !this.ReadPointerData(this.m_PointerEvent, eventData, PanelEventHandler.PointerEventType.Default))
			{
				return;
			}
			Vector2 uguiScrollDelta = eventData.scrollDelta;
			Vector2 uitkScrollDelta = this.eventSystem.currentInputModule.ConvertPointerEventScrollDeltaToTicks(uguiScrollDelta) * 3f;
			uitkScrollDelta.y = -uitkScrollDelta.y;
			using (WheelEvent e = WheelEvent.GetPooled(uitkScrollDelta, this.m_PointerEvent))
			{
				this.SendEvent(e, eventData);
			}
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x000181FC File Offset: 0x000163FC
		private void SendEvent(EventBase e, BaseEventData sourceEventData)
		{
			this.m_Panel.SendEvent(e, DispatchMode.Default);
			if (e.isPropagationStopped)
			{
				sourceEventData.Use();
			}
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00018219 File Offset: 0x00016419
		private void SendEvent(EventBase e, Event sourceEvent)
		{
			this.m_Panel.SendEvent(e, DispatchMode.Default);
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00018228 File Offset: 0x00016428
		internal void Update()
		{
			if (this.isCurrentFocusedPanel)
			{
				this.ProcessImguiEvents(this.currentFocusedElement ?? this.m_Panel.visualTree);
			}
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0001824D File Offset: 0x0001644D
		private void LateUpdate()
		{
			this.ProcessImguiEvents(null);
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00018258 File Offset: 0x00016458
		private void ProcessImguiEvents(Focusable target)
		{
			bool first = true;
			while (Event.PopEvent(this.m_Event))
			{
				if (this.m_Event.type != EventType.Ignore && this.m_Event.type != EventType.Repaint && this.m_Event.type != EventType.Layout)
				{
					PanelEventHandler.s_Modifiers = (first ? this.m_Event.modifiers : (PanelEventHandler.s_Modifiers | this.m_Event.modifiers));
					first = false;
					if (target != null)
					{
						this.ProcessKeyboardEvent(this.m_Event, target);
						if (this.eventSystem.sendNavigationEvents)
						{
							this.ProcessTabEvent(this.m_Event, target);
						}
					}
				}
			}
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x000182F6 File Offset: 0x000164F6
		private void ProcessKeyboardEvent(Event e, Focusable target)
		{
			if (e.type == EventType.KeyUp)
			{
				this.SendKeyUpEvent(e, target);
				return;
			}
			if (e.type == EventType.KeyDown)
			{
				this.SendKeyDownEvent(e, target);
			}
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0001831B File Offset: 0x0001651B
		private void ProcessTabEvent(Event e, Focusable target)
		{
			if (e.ShouldSendNavigationMoveEventRuntime())
			{
				this.SendTabEvent(e, e.shift ? NavigationMoveEvent.Direction.Previous : NavigationMoveEvent.Direction.Next, target);
			}
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0001833C File Offset: 0x0001653C
		private void SendTabEvent(Event e, NavigationMoveEvent.Direction direction, Focusable target)
		{
			using (NavigationMoveEvent ev = NavigationMoveEvent.GetPooled(direction, PanelEventHandler.s_Modifiers))
			{
				ev.target = target;
				this.SendEvent(ev, e);
			}
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00018380 File Offset: 0x00016580
		private void SendKeyUpEvent(Event e, Focusable target)
		{
			using (KeyUpEvent ev = (KeyUpEvent)UIElementsRuntimeUtility.CreateEvent(e))
			{
				ev.target = target;
				this.SendEvent(ev, e);
			}
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x000183C4 File Offset: 0x000165C4
		private void SendKeyDownEvent(Event e, Focusable target)
		{
			using (KeyDownEvent ev = (KeyDownEvent)UIElementsRuntimeUtility.CreateEvent(e))
			{
				ev.target = target;
				this.SendEvent(ev, e);
			}
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00018408 File Offset: 0x00016608
		private bool ReadPointerData(PanelEventHandler.PointerEvent pe, PointerEventData eventData, PanelEventHandler.PointerEventType eventType = PanelEventHandler.PointerEventType.Default)
		{
			if (this.eventSystem == null || this.eventSystem.currentInputModule == null)
			{
				return false;
			}
			pe.Read(this, eventData, eventType);
			Vector2 panelPosition;
			Vector2 panelDelta;
			this.m_Panel.ScreenToPanel(pe.position, pe.deltaPosition, out panelPosition, out panelDelta, true);
			pe.SetPosition(panelPosition, panelDelta);
			return true;
		}

		// Token: 0x0400028B RID: 651
		private BaseRuntimePanel m_Panel;

		// Token: 0x0400028C RID: 652
		private readonly PanelEventHandler.PointerEvent m_PointerEvent = new PanelEventHandler.PointerEvent();

		// Token: 0x0400028D RID: 653
		private float m_LastClickTime;

		// Token: 0x0400028E RID: 654
		private bool m_Selecting;

		// Token: 0x0400028F RID: 655
		private Event m_Event = new Event();

		// Token: 0x04000290 RID: 656
		private static EventModifiers s_Modifiers;

		// Token: 0x02000093 RID: 147
		private enum PointerEventType
		{
			// Token: 0x04000292 RID: 658
			Default,
			// Token: 0x04000293 RID: 659
			Down,
			// Token: 0x04000294 RID: 660
			Up
		}

		// Token: 0x02000094 RID: 148
		private class PointerEvent : IPointerEvent
		{
			// Token: 0x1700016E RID: 366
			// (get) Token: 0x060005AD RID: 1453 RVA: 0x00018499 File Offset: 0x00016699
			// (set) Token: 0x060005AE RID: 1454 RVA: 0x000184A1 File Offset: 0x000166A1
			public int pointerId { get; private set; }

			// Token: 0x1700016F RID: 367
			// (get) Token: 0x060005AF RID: 1455 RVA: 0x000184AA File Offset: 0x000166AA
			// (set) Token: 0x060005B0 RID: 1456 RVA: 0x000184B2 File Offset: 0x000166B2
			public string pointerType { get; private set; }

			// Token: 0x17000170 RID: 368
			// (get) Token: 0x060005B1 RID: 1457 RVA: 0x000184BB File Offset: 0x000166BB
			// (set) Token: 0x060005B2 RID: 1458 RVA: 0x000184C3 File Offset: 0x000166C3
			public bool isPrimary { get; private set; }

			// Token: 0x17000171 RID: 369
			// (get) Token: 0x060005B3 RID: 1459 RVA: 0x000184CC File Offset: 0x000166CC
			// (set) Token: 0x060005B4 RID: 1460 RVA: 0x000184D4 File Offset: 0x000166D4
			public int button { get; private set; }

			// Token: 0x17000172 RID: 370
			// (get) Token: 0x060005B5 RID: 1461 RVA: 0x000184DD File Offset: 0x000166DD
			// (set) Token: 0x060005B6 RID: 1462 RVA: 0x000184E5 File Offset: 0x000166E5
			public int pressedButtons { get; private set; }

			// Token: 0x17000173 RID: 371
			// (get) Token: 0x060005B7 RID: 1463 RVA: 0x000184EE File Offset: 0x000166EE
			// (set) Token: 0x060005B8 RID: 1464 RVA: 0x000184F6 File Offset: 0x000166F6
			public Vector3 position { get; private set; }

			// Token: 0x17000174 RID: 372
			// (get) Token: 0x060005B9 RID: 1465 RVA: 0x000184FF File Offset: 0x000166FF
			// (set) Token: 0x060005BA RID: 1466 RVA: 0x00018507 File Offset: 0x00016707
			public Vector3 localPosition { get; private set; }

			// Token: 0x17000175 RID: 373
			// (get) Token: 0x060005BB RID: 1467 RVA: 0x00018510 File Offset: 0x00016710
			// (set) Token: 0x060005BC RID: 1468 RVA: 0x00018518 File Offset: 0x00016718
			public Vector3 deltaPosition { get; private set; }

			// Token: 0x17000176 RID: 374
			// (get) Token: 0x060005BD RID: 1469 RVA: 0x00018521 File Offset: 0x00016721
			// (set) Token: 0x060005BE RID: 1470 RVA: 0x00018529 File Offset: 0x00016729
			public float deltaTime { get; private set; }

			// Token: 0x17000177 RID: 375
			// (get) Token: 0x060005BF RID: 1471 RVA: 0x00018532 File Offset: 0x00016732
			// (set) Token: 0x060005C0 RID: 1472 RVA: 0x0001853A File Offset: 0x0001673A
			public int clickCount { get; private set; }

			// Token: 0x17000178 RID: 376
			// (get) Token: 0x060005C1 RID: 1473 RVA: 0x00018543 File Offset: 0x00016743
			// (set) Token: 0x060005C2 RID: 1474 RVA: 0x0001854B File Offset: 0x0001674B
			public float pressure { get; private set; }

			// Token: 0x17000179 RID: 377
			// (get) Token: 0x060005C3 RID: 1475 RVA: 0x00018554 File Offset: 0x00016754
			// (set) Token: 0x060005C4 RID: 1476 RVA: 0x0001855C File Offset: 0x0001675C
			public float tangentialPressure { get; private set; }

			// Token: 0x1700017A RID: 378
			// (get) Token: 0x060005C5 RID: 1477 RVA: 0x00018565 File Offset: 0x00016765
			// (set) Token: 0x060005C6 RID: 1478 RVA: 0x0001856D File Offset: 0x0001676D
			public float altitudeAngle { get; private set; }

			// Token: 0x1700017B RID: 379
			// (get) Token: 0x060005C7 RID: 1479 RVA: 0x00018576 File Offset: 0x00016776
			// (set) Token: 0x060005C8 RID: 1480 RVA: 0x0001857E File Offset: 0x0001677E
			public float azimuthAngle { get; private set; }

			// Token: 0x1700017C RID: 380
			// (get) Token: 0x060005C9 RID: 1481 RVA: 0x00018587 File Offset: 0x00016787
			// (set) Token: 0x060005CA RID: 1482 RVA: 0x0001858F File Offset: 0x0001678F
			public float twist { get; private set; }

			// Token: 0x1700017D RID: 381
			// (get) Token: 0x060005CB RID: 1483 RVA: 0x00018598 File Offset: 0x00016798
			// (set) Token: 0x060005CC RID: 1484 RVA: 0x000185A0 File Offset: 0x000167A0
			public Vector2 tilt { get; private set; }

			// Token: 0x1700017E RID: 382
			// (get) Token: 0x060005CD RID: 1485 RVA: 0x000185A9 File Offset: 0x000167A9
			// (set) Token: 0x060005CE RID: 1486 RVA: 0x000185B1 File Offset: 0x000167B1
			public PenStatus penStatus { get; private set; }

			// Token: 0x1700017F RID: 383
			// (get) Token: 0x060005CF RID: 1487 RVA: 0x000185BA File Offset: 0x000167BA
			// (set) Token: 0x060005D0 RID: 1488 RVA: 0x000185C2 File Offset: 0x000167C2
			public Vector2 radius { get; private set; }

			// Token: 0x17000180 RID: 384
			// (get) Token: 0x060005D1 RID: 1489 RVA: 0x000185CB File Offset: 0x000167CB
			// (set) Token: 0x060005D2 RID: 1490 RVA: 0x000185D3 File Offset: 0x000167D3
			public Vector2 radiusVariance { get; private set; }

			// Token: 0x17000181 RID: 385
			// (get) Token: 0x060005D3 RID: 1491 RVA: 0x000185DC File Offset: 0x000167DC
			// (set) Token: 0x060005D4 RID: 1492 RVA: 0x000185E4 File Offset: 0x000167E4
			public EventModifiers modifiers { get; private set; }

			// Token: 0x17000182 RID: 386
			// (get) Token: 0x060005D5 RID: 1493 RVA: 0x000185ED File Offset: 0x000167ED
			public bool shiftKey
			{
				get
				{
					return (this.modifiers & EventModifiers.Shift) > EventModifiers.None;
				}
			}

			// Token: 0x17000183 RID: 387
			// (get) Token: 0x060005D6 RID: 1494 RVA: 0x000185FA File Offset: 0x000167FA
			public bool ctrlKey
			{
				get
				{
					return (this.modifiers & EventModifiers.Control) > EventModifiers.None;
				}
			}

			// Token: 0x17000184 RID: 388
			// (get) Token: 0x060005D7 RID: 1495 RVA: 0x00018607 File Offset: 0x00016807
			public bool commandKey
			{
				get
				{
					return (this.modifiers & EventModifiers.Command) > EventModifiers.None;
				}
			}

			// Token: 0x17000185 RID: 389
			// (get) Token: 0x060005D8 RID: 1496 RVA: 0x00018614 File Offset: 0x00016814
			public bool altKey
			{
				get
				{
					return (this.modifiers & EventModifiers.Alt) > EventModifiers.None;
				}
			}

			// Token: 0x17000186 RID: 390
			// (get) Token: 0x060005D9 RID: 1497 RVA: 0x00018621 File Offset: 0x00016821
			public bool actionKey
			{
				get
				{
					if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.OSXPlayer)
					{
						return this.ctrlKey;
					}
					return this.commandKey;
				}
			}

			// Token: 0x060005DA RID: 1498 RVA: 0x00018640 File Offset: 0x00016840
			public void Read(PanelEventHandler self, PointerEventData eventData, PanelEventHandler.PointerEventType eventType)
			{
				this.pointerId = self.eventSystem.currentInputModule.ConvertUIToolkitPointerId(eventData);
				this.pointerType = (PanelEventHandler.PointerEvent.<Read>g__InRange|90_0(this.pointerId, PointerId.touchPointerIdBase, PointerId.touchPointerCount) ? PointerType.touch : (PanelEventHandler.PointerEvent.<Read>g__InRange|90_0(this.pointerId, PointerId.penPointerIdBase, PointerId.penPointerCount) ? PointerType.pen : PointerType.mouse));
				this.isPrimary = this.pointerId == PointerId.mousePointerId || this.pointerId == PointerId.touchPointerIdBase || this.pointerId == PointerId.penPointerIdBase;
				int h = Screen.height;
				Vector3 eventPosition = MultipleDisplayUtilities.GetRelativeMousePositionForRaycast(eventData);
				int eventDisplayIndex = (int)eventPosition.z;
				if (eventDisplayIndex > 0 && eventDisplayIndex < Display.displays.Length)
				{
					h = Display.displays[eventDisplayIndex].systemHeight;
				}
				Vector2 delta = eventData.delta;
				eventPosition.y = (float)h - eventPosition.y;
				delta.y = -delta.y;
				this.localPosition = (this.position = eventPosition);
				this.deltaPosition = delta;
				this.deltaTime = 0f;
				this.pressure = eventData.pressure;
				this.tangentialPressure = eventData.tangentialPressure;
				this.altitudeAngle = eventData.altitudeAngle;
				this.azimuthAngle = eventData.azimuthAngle;
				this.twist = eventData.twist;
				this.tilt = eventData.tilt;
				this.penStatus = eventData.penStatus;
				this.radius = eventData.radius;
				this.radiusVariance = eventData.radiusVariance;
				this.modifiers = PanelEventHandler.s_Modifiers;
				if (eventType == PanelEventHandler.PointerEventType.Default)
				{
					this.button = -1;
					this.clickCount = 0;
				}
				else
				{
					this.button = Mathf.Max(0, (int)eventData.button);
					this.clickCount = eventData.clickCount;
					if (eventType == PanelEventHandler.PointerEventType.Down)
					{
						if (Time.unscaledTime > self.m_LastClickTime + (float)ClickDetector.s_DoubleClickTime * 0.001f)
						{
							this.clickCount = 0;
						}
						int clickCount = this.clickCount;
						this.clickCount = clickCount + 1;
						PointerDeviceState.PressButton(this.pointerId, this.button);
					}
					else if (eventType == PanelEventHandler.PointerEventType.Up)
					{
						PointerDeviceState.ReleaseButton(this.pointerId, this.button);
					}
					this.clickCount = Mathf.Max(1, this.clickCount);
				}
				this.pressedButtons = PointerDeviceState.GetPressedButtons(this.pointerId);
			}

			// Token: 0x060005DB RID: 1499 RVA: 0x00018888 File Offset: 0x00016A88
			public void SetPosition(Vector3 positionOverride, Vector3 deltaOverride)
			{
				this.position = positionOverride;
				this.localPosition = positionOverride;
				this.deltaPosition = deltaOverride;
			}

			// Token: 0x060005DD RID: 1501 RVA: 0x000188AC File Offset: 0x00016AAC
			[CompilerGenerated]
			internal static bool <Read>g__InRange|90_0(int i, int start, int count)
			{
				return i >= start && i < start + count;
			}
		}
	}
}
