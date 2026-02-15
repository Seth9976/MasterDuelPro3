using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001ED RID: 493
	[EventCategory(EventCategory.Pointer)]
	public abstract class MouseEventBase<T> : EventBase<T>, IMouseEvent, IMouseEventInternal, IPointerOrMouseEvent where T : MouseEventBase<T>, new()
	{
		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000DC0 RID: 3520 RVA: 0x0003F424 File Offset: 0x0003D624
		// (set) Token: 0x06000DC1 RID: 3521 RVA: 0x0003F42C File Offset: 0x0003D62C
		public EventModifiers modifiers { get; protected set; }

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x0003F435 File Offset: 0x0003D635
		// (set) Token: 0x06000DC3 RID: 3523 RVA: 0x0003F43D File Offset: 0x0003D63D
		public Vector2 mousePosition { get; protected set; }

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x0003F446 File Offset: 0x0003D646
		// (set) Token: 0x06000DC5 RID: 3525 RVA: 0x0003F44E File Offset: 0x0003D64E
		public Vector2 localMousePosition { get; internal set; }

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x0003F457 File Offset: 0x0003D657
		// (set) Token: 0x06000DC7 RID: 3527 RVA: 0x0003F45F File Offset: 0x0003D65F
		public Vector2 mouseDelta { get; protected set; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x0003F468 File Offset: 0x0003D668
		// (set) Token: 0x06000DC9 RID: 3529 RVA: 0x0003F470 File Offset: 0x0003D670
		public int clickCount { get; protected set; }

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x0003F479 File Offset: 0x0003D679
		// (set) Token: 0x06000DCB RID: 3531 RVA: 0x0003F481 File Offset: 0x0003D681
		public int button { get; protected set; }

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x0003F48A File Offset: 0x0003D68A
		// (set) Token: 0x06000DCD RID: 3533 RVA: 0x0003F492 File Offset: 0x0003D692
		public int pressedButtons { get; protected set; }

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000DCE RID: 3534 RVA: 0x0003F49B File Offset: 0x0003D69B
		// (set) Token: 0x06000DCF RID: 3535 RVA: 0x0003F4A3 File Offset: 0x0003D6A3
		bool IMouseEventInternal.triggeredByOS { get; set; }

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x0003F4AC File Offset: 0x0003D6AC
		// (set) Token: 0x06000DD1 RID: 3537 RVA: 0x0003F4B4 File Offset: 0x0003D6B4
		IPointerEvent IMouseEventInternal.sourcePointerEvent { get; set; }

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x0003F4BD File Offset: 0x0003D6BD
		int IPointerOrMouseEvent.pointerId
		{
			get
			{
				return PointerId.mousePointerId;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000DD3 RID: 3539 RVA: 0x0003F4C4 File Offset: 0x0003D6C4
		Vector3 IPointerOrMouseEvent.position
		{
			get
			{
				return this.mousePosition;
			}
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x0003F4D1 File Offset: 0x0003D6D1
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x0003F4E4 File Offset: 0x0003D6E4
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.BubblesOrTricklesDown;
			this.modifiers = EventModifiers.None;
			this.mousePosition = Vector2.zero;
			this.localMousePosition = Vector2.zero;
			this.mouseDelta = Vector2.zero;
			this.clickCount = 0;
			this.button = 0;
			this.pressedButtons = 0;
			((IMouseEventInternal)this).triggeredByOS = false;
			((IMouseEventInternal)this).sourcePointerEvent = null;
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x0003F550 File Offset: 0x0003D750
		// (set) Token: 0x06000DD7 RID: 3543 RVA: 0x0003F568 File Offset: 0x0003D768
		public override IEventHandler currentTarget
		{
			get
			{
				return base.currentTarget;
			}
			internal set
			{
				base.currentTarget = value;
				VisualElement element = this.currentTarget as VisualElement;
				bool flag = element != null;
				if (flag)
				{
					this.localMousePosition = element.WorldToLocal(this.mousePosition);
				}
				else
				{
					this.localMousePosition = this.mousePosition;
				}
			}
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x0003F5B8 File Offset: 0x0003D7B8
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			bool flag = ((IMouseEventInternal)this).sourcePointerEvent == null && ((IMouseEventInternal)this).triggeredByOS;
			if (flag)
			{
				PointerDeviceState.SavePointerPosition(PointerId.mousePointerId, this.mousePosition, panel, panel.contextType);
			}
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x0003F600 File Offset: 0x0003D800
		protected internal override void PostDispatch(IPanel panel)
		{
			EventBase pointerEvent = ((IMouseEventInternal)this).sourcePointerEvent as EventBase;
			bool flag = pointerEvent != null;
			if (flag)
			{
				Debug.Assert(!pointerEvent.processed, "!pointerEvent.processed");
				bool isPropagationStopped = base.isPropagationStopped;
				if (isPropagationStopped)
				{
					pointerEvent.StopPropagation();
				}
				bool isImmediatePropagationStopped = base.isImmediatePropagationStopped;
				if (isImmediatePropagationStopped)
				{
					pointerEvent.StopImmediatePropagation();
				}
				pointerEvent.processedByFocusController |= base.processedByFocusController;
			}
			base.PostDispatch(panel);
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x0003F67A File Offset: 0x0003D87A
		internal override void Dispatch(BaseVisualElementPanel panel)
		{
			EventDispatchUtilities.DispatchToCapturingElementOrElementUnderPointer(this, panel, PointerId.mousePointerId, this.mousePosition);
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x0003F690 File Offset: 0x0003D890
		public static T GetPooled(Event systemEvent)
		{
			T e = EventBase<T>.GetPooled();
			e.imguiEvent = systemEvent;
			bool flag = systemEvent != null;
			if (flag)
			{
				e.modifiers = systemEvent.modifiers;
				e.mousePosition = systemEvent.mousePosition;
				e.localMousePosition = systemEvent.mousePosition;
				e.mouseDelta = systemEvent.delta;
				e.button = systemEvent.button;
				e.pressedButtons = PointerDeviceState.GetPressedButtons(PointerId.mousePointerId);
				e.clickCount = systemEvent.clickCount;
				e.triggeredByOS = true;
			}
			return e;
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x0003F754 File Offset: 0x0003D954
		internal static T GetPooled(IMouseEvent triggerEvent, Vector2 mousePosition)
		{
			bool flag = triggerEvent != null;
			T t;
			if (flag)
			{
				t = MouseEventBase<T>.GetPooled(triggerEvent);
			}
			else
			{
				T e = EventBase<T>.GetPooled();
				e.mousePosition = mousePosition;
				e.localMousePosition = mousePosition;
				t = e;
			}
			return t;
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x0003F79C File Offset: 0x0003D99C
		public static T GetPooled(IMouseEvent triggerEvent)
		{
			T e = EventBase<T>.GetPooled(triggerEvent as EventBase);
			bool flag = triggerEvent != null;
			if (flag)
			{
				e.modifiers = triggerEvent.modifiers;
				e.mousePosition = triggerEvent.mousePosition;
				e.localMousePosition = triggerEvent.mousePosition;
				e.mouseDelta = triggerEvent.mouseDelta;
				e.button = triggerEvent.button;
				e.pressedButtons = triggerEvent.pressedButtons;
				e.clickCount = triggerEvent.clickCount;
				IMouseEventInternal mouseEventInternal = triggerEvent as IMouseEventInternal;
				bool flag2 = mouseEventInternal != null;
				if (flag2)
				{
					e.triggeredByOS = mouseEventInternal.triggeredByOS;
				}
			}
			return e;
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x0003F86C File Offset: 0x0003DA6C
		protected static T GetPooled(IPointerEvent pointerEvent)
		{
			T e = EventBase<T>.GetPooled();
			EventBase eventBase = e;
			EventBase eventBase2 = pointerEvent as EventBase;
			eventBase.elementTarget = ((eventBase2 != null) ? eventBase2.elementTarget : null);
			EventBase eventBase3 = e;
			EventBase eventBase4 = pointerEvent as EventBase;
			eventBase3.imguiEvent = ((eventBase4 != null) ? eventBase4.imguiEvent : null);
			e.modifiers = pointerEvent.modifiers;
			e.mousePosition = pointerEvent.position;
			e.localMousePosition = pointerEvent.position;
			e.mouseDelta = pointerEvent.deltaPosition;
			e.button = ((pointerEvent.button == -1) ? 0 : pointerEvent.button);
			e.pressedButtons = pointerEvent.pressedButtons;
			e.clickCount = pointerEvent.clickCount;
			IPointerEventInternal pointerEventInternal = pointerEvent as IPointerEventInternal;
			bool flag = pointerEventInternal != null;
			if (flag)
			{
				e.triggeredByOS = pointerEventInternal.triggeredByOS;
				e.sourcePointerEvent = pointerEvent;
			}
			return e;
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x0003F98A File Offset: 0x0003DB8A
		protected MouseEventBase()
		{
			this.LocalInit();
		}
	}
}
