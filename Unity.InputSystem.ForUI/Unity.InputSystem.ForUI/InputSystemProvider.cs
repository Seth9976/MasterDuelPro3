using System;
using System.Collections.Generic;
using Unity.IntegerTime;
using UnityEngine.InputForUI;
using UnityEngine.InputSystem.Controls;

namespace UnityEngine.InputSystem.Plugins.InputForUI
{
	// Token: 0x02000004 RID: 4
	internal class InputSystemProvider : IEventProviderImpl
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BB File Offset: 0x000002BB
		static InputSystemProvider()
		{
			EventProvider.SetInputSystemProvider(new InputSystemProvider());
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020C7 File Offset: 0x000002C7
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void Bootstrap()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020C9 File Offset: 0x000002C9
		private EventModifiers m_EventModifiers
		{
			get
			{
				return this.m_InputEventPartialProvider._eventModifiers;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020D6 File Offset: 0x000002D6
		private DiscreteTime m_CurrentTime
		{
			get
			{
				return (DiscreteTime)Time.timeAsRational;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020E4 File Offset: 0x000002E4
		public void Initialize()
		{
			if (this.m_InputEventPartialProvider == null)
			{
				this.m_InputEventPartialProvider = new InputEventPartialProvider();
			}
			this.m_InputEventPartialProvider.Initialize();
			this.m_Events.Clear();
			this.m_MouseState.Reset();
			this.m_PenState.Reset();
			this.m_SeenPenEvents = false;
			this.m_TouchState.Reset();
			this.m_SeenTouchEvents = false;
			this.m_Cfg = InputSystemProvider.Configuration.GetDefaultConfiguration();
			this.RegisterActions();
			InputSystem.onActionsChange += this.OnActionsChange;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000216B File Offset: 0x0000036B
		public void Shutdown()
		{
			this.UnregisterActions();
			this.m_InputEventPartialProvider.Shutdown();
			this.m_InputEventPartialProvider = null;
			InputSystem.onActionsChange -= this.OnActionsChange;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002196 File Offset: 0x00000396
		public void OnActionsChange()
		{
			this.UnregisterActions();
			this.m_Cfg = InputSystemProvider.Configuration.GetDefaultConfiguration();
			this.RegisterActions();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021B0 File Offset: 0x000003B0
		public void Update()
		{
			this.m_InputEventPartialProvider.Update();
			this.m_Events.Sort((Event a, Event b) => InputSystemProvider.SortEvents(a, b));
			DiscreteTime currentTime = (DiscreteTime)Time.timeAsRational;
			this.DirectionNavigation(currentTime);
			foreach (Event ev in this.m_Events)
			{
				if (this.m_SeenTouchEvents && ev.type == Event.Type.PointerEvent && ev.eventSource == EventSource.Pen)
				{
					this.m_PenState.Reset();
				}
				else if ((this.m_SeenTouchEvents || this.m_SeenPenEvents) && ev.type == Event.Type.PointerEvent && (ev.eventSource == EventSource.Mouse || ev.eventSource == EventSource.Unspecified))
				{
					this.m_MouseState.Reset();
				}
				else
				{
					EventProvider.Dispatch(in ev);
				}
			}
			if (this.m_ResetSeenEventsOnUpdate)
			{
				this.ResetSeenEvents();
				this.m_ResetSeenEventsOnUpdate = false;
			}
			this.m_Events.Clear();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022D0 File Offset: 0x000004D0
		private void ResetSeenEvents()
		{
			this.m_SeenTouchEvents = false;
			this.m_SeenPenEvents = false;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022E0 File Offset: 0x000004E0
		public bool ActionAssetIsNotNull()
		{
			return this.m_InputActionAsset != null;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022F0 File Offset: 0x000004F0
		private void DirectionNavigation(DiscreteTime currentTime)
		{
			ValueTuple<Vector2, bool> valueTuple = this.ReadCurrentNavigationMoveVector();
			Vector2 move = valueTuple.Item1;
			bool axesButtonWerePressed = valueTuple.Item2;
			NavigationEvent.Direction direction = NavigationEvent.DetermineMoveDirection(move, 0.6f);
			if (direction == NavigationEvent.Direction.None)
			{
				direction = this.ReadNextPreviousDirection();
				axesButtonWerePressed = this.m_NextPreviousAction.WasPressedThisFrame();
			}
			if (direction == NavigationEvent.Direction.None)
			{
				this.m_RepeatHelper.Reset();
				return;
			}
			if (this.m_RepeatHelper.ShouldSendMoveEvent(currentTime, direction, axesButtonWerePressed))
			{
				Event @event = Event.From(new NavigationEvent
				{
					type = NavigationEvent.Type.Move,
					direction = direction,
					timestamp = currentTime,
					eventSource = this.GetEventSource(this.GetActiveDeviceFromDirection(direction)),
					playerId = 0U,
					eventModifiers = this.m_EventModifiers
				});
				EventProvider.Dispatch(in @event);
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000023AC File Offset: 0x000005AC
		private InputDevice GetActiveDeviceFromDirection(NavigationEvent.Direction direction)
		{
			switch (direction)
			{
			case NavigationEvent.Direction.Left:
			case NavigationEvent.Direction.Up:
			case NavigationEvent.Direction.Right:
			case NavigationEvent.Direction.Down:
				if (this.m_MoveAction != null)
				{
					return this.m_MoveAction.action.activeControl.device;
				}
				break;
			case NavigationEvent.Direction.Next:
			case NavigationEvent.Direction.Previous:
				if (this.m_NextPreviousAction != null)
				{
					return this.m_NextPreviousAction.activeControl.device;
				}
				break;
			}
			return Keyboard.current;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002420 File Offset: 0x00000620
		private ValueTuple<Vector2, bool> ReadCurrentNavigationMoveVector()
		{
			if (this.m_MoveAction == null)
			{
				return new ValueTuple<Vector2, bool>(default(Vector2), false);
			}
			Vector2 vector = this.m_MoveAction.action.ReadValue<Vector2>();
			bool axisWasPressed = this.m_MoveAction.action.WasPressedThisFrame();
			return new ValueTuple<Vector2, bool>(vector, axisWasPressed);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002474 File Offset: 0x00000674
		private NavigationEvent.Direction ReadNextPreviousDirection()
		{
			if (!this.m_NextPreviousAction.IsPressed() || !(this.m_NextPreviousAction.activeControl.device is Keyboard))
			{
				return NavigationEvent.Direction.None;
			}
			if (!(this.m_NextPreviousAction.activeControl.device as Keyboard).shiftKey.isPressed)
			{
				return NavigationEvent.Direction.Next;
			}
			return NavigationEvent.Direction.Previous;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000024CB File Offset: 0x000006CB
		private static int SortEvents(Event a, Event b)
		{
			return Event.CompareType(a, b);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000024D4 File Offset: 0x000006D4
		public void OnFocusChanged(bool focus)
		{
			this.m_InputEventPartialProvider.OnFocusChanged(focus);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000024E4 File Offset: 0x000006E4
		public bool RequestCurrentState(Event.Type type)
		{
			if (this.m_InputEventPartialProvider.RequestCurrentState(type))
			{
				return true;
			}
			if (type != Event.Type.PointerEvent)
			{
				if (type != Event.Type.IMECompositionEvent)
				{
				}
				return false;
			}
			if (this.m_TouchState.LastPositionValid)
			{
				Event @event = Event.From(this.ToPointerStateEvent(this.m_CurrentTime, in this.m_TouchState, EventSource.Touch));
				EventProvider.Dispatch(in @event);
			}
			if (this.m_PenState.LastPositionValid)
			{
				Event @event = Event.From(this.ToPointerStateEvent(this.m_CurrentTime, in this.m_PenState, EventSource.Pen));
				EventProvider.Dispatch(in @event);
			}
			if (this.m_MouseState.LastPositionValid)
			{
				Event @event = Event.From(this.ToPointerStateEvent(this.m_CurrentTime, in this.m_MouseState, EventSource.Mouse));
				EventProvider.Dispatch(in @event);
			}
			return this.m_TouchState.LastPositionValid || this.m_PenState.LastPositionValid || this.m_MouseState.LastPositionValid;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000025C1 File Offset: 0x000007C1
		public uint playerCount
		{
			get
			{
				return 1U;
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000025C4 File Offset: 0x000007C4
		private static Vector2 ScreenBottomLeftToPanelPosition(Vector2 position, int targetDisplay)
		{
			int screenHeight = Screen.height;
			if (targetDisplay > 0 && targetDisplay < Display.displays.Length)
			{
				screenHeight = Display.displays[targetDisplay].systemHeight;
			}
			position.y = (float)screenHeight - position.y;
			return position;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002604 File Offset: 0x00000804
		private PointerEvent ToPointerStateEvent(DiscreteTime currentTime, in PointerState state, EventSource eventSource)
		{
			PointerEvent pointerEvent = default(PointerEvent);
			pointerEvent.type = PointerEvent.Type.State;
			pointerEvent.pointerIndex = 0;
			pointerEvent.position = state.LastPosition;
			pointerEvent.deltaPosition = Vector2.zero;
			pointerEvent.scroll = Vector2.zero;
			pointerEvent.displayIndex = state.LastDisplayIndex;
			pointerEvent.button = PointerEvent.Button.None;
			PointerState pointerState = state;
			pointerEvent.buttonsState = pointerState.ButtonsState;
			pointerEvent.clickCount = 0;
			pointerEvent.timestamp = currentTime;
			pointerEvent.eventSource = eventSource;
			pointerEvent.playerId = 0U;
			pointerEvent.eventModifiers = this.m_EventModifiers;
			return pointerEvent;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000026A8 File Offset: 0x000008A8
		private EventSource GetEventSource(InputAction.CallbackContext ctx)
		{
			InputDevice device = ctx.control.device;
			return this.GetEventSource(device);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000026C9 File Offset: 0x000008C9
		private EventSource GetEventSource(InputDevice device)
		{
			if (device is Touchscreen)
			{
				return EventSource.Touch;
			}
			if (device is Pen)
			{
				return EventSource.Pen;
			}
			if (device is Mouse)
			{
				return EventSource.Mouse;
			}
			if (device is Keyboard)
			{
				return EventSource.Keyboard;
			}
			if (device is Gamepad)
			{
				return EventSource.Gamepad;
			}
			return EventSource.Unspecified;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000026FE File Offset: 0x000008FE
		private ref PointerState GetPointerStateForSource(EventSource eventSource)
		{
			if (eventSource == EventSource.Pen)
			{
				return ref this.m_PenState;
			}
			if (eventSource == EventSource.Touch)
			{
				return ref this.m_TouchState;
			}
			return ref this.m_MouseState;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000271C File Offset: 0x0000091C
		private void DispatchFromCallback(in Event ev)
		{
			this.m_Events.Add(ev);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002730 File Offset: 0x00000930
		private static int FindTouchFingerIndex(Touchscreen touchscreen, InputAction.CallbackContext ctx)
		{
			if (touchscreen == null)
			{
				return 0;
			}
			Vector2Control asVector2Control = ((ctx.control is Vector2Control) ? ((Vector2Control)ctx.control) : null);
			TouchPressControl asTouchPressControl = ((ctx.control is TouchPressControl) ? ((TouchPressControl)ctx.control) : null);
			TouchControl asTouchControl = ((ctx.control is TouchControl) ? ((TouchControl)ctx.control) : null);
			for (int i = 0; i < touchscreen.touches.Count; i++)
			{
				if (asVector2Control != null && asVector2Control == touchscreen.touches[i].position)
				{
					return i;
				}
				if (asTouchPressControl != null && asTouchPressControl == touchscreen.touches[i].press)
				{
					return i;
				}
				if (asTouchControl != null && asTouchControl == touchscreen.touches[i])
				{
					return i;
				}
			}
			return 0;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000280C File Offset: 0x00000A0C
		private void OnPointerPerformed(InputAction.CallbackContext ctx)
		{
			EventSource eventSource = this.GetEventSource(ctx);
			ref PointerState pointerState = ref this.GetPointerStateForSource(eventSource);
			Pointer asPointerDevice = ((ctx.control.device is Pointer) ? ((Pointer)ctx.control.device) : null);
			Pen asPenDevice = ((ctx.control.device is Pen) ? ((Pen)ctx.control.device) : null);
			Touchscreen asTouchscreenDevice = ((ctx.control.device is Touchscreen) ? ((Touchscreen)ctx.control.device) : null);
			TouchControl asTouchControl = ((ctx.control is TouchControl) ? ((TouchControl)ctx.control) : null);
			int pointerIndex = InputSystemProvider.FindTouchFingerIndex(asTouchscreenDevice, ctx);
			this.m_ResetSeenEventsOnUpdate = false;
			if (asTouchControl != null || asTouchscreenDevice != null)
			{
				this.m_SeenTouchEvents = true;
			}
			else if (asPenDevice != null)
			{
				this.m_SeenPenEvents = true;
			}
			Vector2 vector = ctx.ReadValue<Vector2>();
			int targetDisplay = ((asPointerDevice != null) ? asPointerDevice.displayIndex.ReadValue() : ((asTouchscreenDevice != null) ? asTouchscreenDevice.displayIndex.ReadValue() : 0));
			Vector2 position = InputSystemProvider.ScreenBottomLeftToPanelPosition(vector, targetDisplay);
			Vector2 delta = (pointerState.LastPositionValid ? (position - pointerState.LastPosition) : Vector2.zero);
			Vector2 tilt = ((asPenDevice != null) ? asPenDevice.tilt.ReadValue() : Vector2.zero);
			float twist = ((asPenDevice != null) ? asPenDevice.twist.ReadValue() : 0f);
			float pressure = ((asPenDevice != null) ? asPenDevice.pressure.ReadValue() : ((asTouchControl != null) ? asTouchControl.pressure.ReadValue() : 0f));
			bool isInverted = asPenDevice != null && asPenDevice.eraser.isPressed;
			if (delta.sqrMagnitude >= 0.01f)
			{
				Event @event = Event.From(new PointerEvent
				{
					type = PointerEvent.Type.PointerMoved,
					pointerIndex = pointerIndex,
					position = position,
					deltaPosition = delta,
					scroll = Vector2.zero,
					displayIndex = targetDisplay,
					tilt = tilt,
					twist = twist,
					pressure = pressure,
					isInverted = isInverted,
					button = PointerEvent.Button.None,
					buttonsState = pointerState.ButtonsState,
					clickCount = 0,
					timestamp = this.m_CurrentTime,
					eventSource = eventSource,
					playerId = 0U,
					eventModifiers = this.m_EventModifiers
				});
				this.DispatchFromCallback(in @event);
				pointerState.OnMove(this.m_CurrentTime, position, targetDisplay);
				return;
			}
			if (!pointerState.LastPositionValid)
			{
				pointerState.OnMove(this.m_CurrentTime, position, targetDisplay);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002AA0 File Offset: 0x00000CA0
		private void OnSubmitPerformed(InputAction.CallbackContext ctx)
		{
			Event @event = Event.From(new NavigationEvent
			{
				type = NavigationEvent.Type.Submit,
				direction = NavigationEvent.Direction.None,
				timestamp = this.m_CurrentTime,
				eventSource = this.GetEventSource(ctx),
				playerId = 0U,
				eventModifiers = this.m_EventModifiers
			});
			this.DispatchFromCallback(in @event);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002B04 File Offset: 0x00000D04
		private void OnCancelPerformed(InputAction.CallbackContext ctx)
		{
			Event @event = Event.From(new NavigationEvent
			{
				type = NavigationEvent.Type.Cancel,
				direction = NavigationEvent.Direction.None,
				timestamp = this.m_CurrentTime,
				eventSource = this.GetEventSource(ctx),
				playerId = 0U,
				eventModifiers = this.m_EventModifiers
			});
			this.DispatchFromCallback(in @event);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002B68 File Offset: 0x00000D68
		private void OnClickPerformed(InputAction.CallbackContext ctx, EventSource eventSource, PointerEvent.Button button)
		{
			ref PointerState state = ref this.GetPointerStateForSource(eventSource);
			Touchscreen asTouchscreenDevice = ((ctx.control.device is Touchscreen) ? ((Touchscreen)ctx.control.device) : null);
			bool flag = ((ctx.control is TouchControl) ? ((TouchControl)ctx.control) : null) != null;
			int pointerIndex = InputSystemProvider.FindTouchFingerIndex(asTouchscreenDevice, ctx);
			this.m_ResetSeenEventsOnUpdate = true;
			if (flag || asTouchscreenDevice != null)
			{
				this.m_SeenTouchEvents = true;
			}
			bool wasPressed = state.ButtonsState.Get(button);
			bool isPressed = ctx.ReadValueAsButton();
			state.OnButtonChange(this.m_CurrentTime, button, wasPressed, isPressed);
			Event @event = Event.From(new PointerEvent
			{
				type = (isPressed ? PointerEvent.Type.ButtonPressed : PointerEvent.Type.ButtonReleased),
				pointerIndex = pointerIndex,
				position = state.LastPosition,
				deltaPosition = Vector2.zero,
				scroll = Vector2.zero,
				displayIndex = state.LastDisplayIndex,
				tilt = Vector2.zero,
				twist = 0f,
				pressure = 0f,
				isInverted = false,
				button = button,
				buttonsState = state.ButtonsState,
				clickCount = state.ClickCount,
				timestamp = this.m_CurrentTime,
				eventSource = eventSource,
				playerId = 0U,
				eventModifiers = this.m_EventModifiers
			});
			this.DispatchFromCallback(in @event);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002CE1 File Offset: 0x00000EE1
		private void OnLeftClickPerformed(InputAction.CallbackContext ctx)
		{
			this.OnClickPerformed(ctx, this.GetEventSource(ctx), PointerEvent.Button.Primary);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002CF2 File Offset: 0x00000EF2
		private void OnMiddleClickPerformed(InputAction.CallbackContext ctx)
		{
			this.OnClickPerformed(ctx, this.GetEventSource(ctx), PointerEvent.Button.PenBarrelButton);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002D03 File Offset: 0x00000F03
		private void OnRightClickPerformed(InputAction.CallbackContext ctx)
		{
			this.OnClickPerformed(ctx, this.GetEventSource(ctx), PointerEvent.Button.PenEraserInTouch);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002D14 File Offset: 0x00000F14
		private void OnScrollWheelPerformed(InputAction.CallbackContext ctx)
		{
			Vector2 scrollTicks = ctx.ReadValue<Vector2>() / InputSystem.scrollWheelDeltaPerTick;
			if (scrollTicks.sqrMagnitude < 0.01f)
			{
				return;
			}
			EventSource eventSource = this.GetEventSource(ctx);
			ref PointerState state = ref this.GetPointerStateForSource(eventSource);
			Vector2 position = Vector2.zero;
			int targetDisplay = 0;
			if (state.LastPositionValid)
			{
				position = state.LastPosition;
				targetDisplay = state.LastDisplayIndex;
			}
			else if (eventSource == EventSource.Mouse && Mouse.current != null)
			{
				position = Mouse.current.position.ReadValue();
				targetDisplay = Mouse.current.displayIndex.ReadValue();
			}
			Vector2 scrollDelta = new Vector2
			{
				x = scrollTicks.x * 3f,
				y = -scrollTicks.y * 3f
			};
			Event @event = Event.From(new PointerEvent
			{
				type = PointerEvent.Type.Scroll,
				pointerIndex = 0,
				position = position,
				deltaPosition = Vector2.zero,
				scroll = scrollDelta,
				displayIndex = targetDisplay,
				tilt = Vector2.zero,
				twist = 0f,
				pressure = 0f,
				isInverted = false,
				button = PointerEvent.Button.None,
				buttonsState = state.ButtonsState,
				clickCount = 0,
				timestamp = this.m_CurrentTime,
				eventSource = EventSource.Mouse,
				playerId = 0U,
				eventModifiers = this.m_EventModifiers
			});
			this.DispatchFromCallback(in @event);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002E95 File Offset: 0x00001095
		private void RegisterNextPreviousAction()
		{
			this.m_NextPreviousAction = new InputAction("nextPreviousAction", InputActionType.Button, null, null, null, null);
			this.m_NextPreviousAction.AddBinding("<Keyboard>/tab", null, null, null);
			this.m_NextPreviousAction.Enable();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002ECB File Offset: 0x000010CB
		private void UnregisterFixedActions()
		{
			if (this.m_NextPreviousAction != null)
			{
				this.m_NextPreviousAction.Disable();
				this.m_NextPreviousAction = null;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002EE8 File Offset: 0x000010E8
		private void RegisterActions()
		{
			this.m_InputActionAsset = this.m_Cfg.ActionAsset;
			Action<InputActionAsset> action = InputSystemProvider.s_OnRegisterActions;
			if (action != null)
			{
				action(this.m_InputActionAsset);
			}
			this.m_PointAction = InputActionReference.Create(this.m_InputActionAsset.FindAction(this.m_Cfg.PointAction, false));
			this.m_MoveAction = InputActionReference.Create(this.m_InputActionAsset.FindAction(this.m_Cfg.MoveAction, false));
			this.m_SubmitAction = InputActionReference.Create(this.m_InputActionAsset.FindAction(this.m_Cfg.SubmitAction, false));
			this.m_CancelAction = InputActionReference.Create(this.m_InputActionAsset.FindAction(this.m_Cfg.CancelAction, false));
			this.m_LeftClickAction = InputActionReference.Create(this.m_InputActionAsset.FindAction(this.m_Cfg.LeftClickAction, false));
			this.m_MiddleClickAction = InputActionReference.Create(this.m_InputActionAsset.FindAction(this.m_Cfg.MiddleClickAction, false));
			this.m_RightClickAction = InputActionReference.Create(this.m_InputActionAsset.FindAction(this.m_Cfg.RightClickAction, false));
			this.m_ScrollWheelAction = InputActionReference.Create(this.m_InputActionAsset.FindAction(this.m_Cfg.ScrollWheelAction, false));
			if (this.m_PointAction != null && this.m_PointAction.action != null)
			{
				this.m_PointAction.action.performed += this.OnPointerPerformed;
			}
			if (this.m_SubmitAction != null && this.m_SubmitAction.action != null)
			{
				this.m_SubmitAction.action.performed += this.OnSubmitPerformed;
			}
			if (this.m_CancelAction != null && this.m_CancelAction.action != null)
			{
				this.m_CancelAction.action.performed += this.OnCancelPerformed;
			}
			if (this.m_LeftClickAction != null && this.m_LeftClickAction.action != null)
			{
				this.m_LeftClickAction.action.performed += this.OnLeftClickPerformed;
			}
			if (this.m_MiddleClickAction != null && this.m_MiddleClickAction.action != null)
			{
				this.m_MiddleClickAction.action.performed += this.OnMiddleClickPerformed;
			}
			if (this.m_RightClickAction != null && this.m_RightClickAction.action != null)
			{
				this.m_RightClickAction.action.performed += this.OnRightClickPerformed;
			}
			if (this.m_ScrollWheelAction != null && this.m_ScrollWheelAction.action != null)
			{
				this.m_ScrollWheelAction.action.performed += this.OnScrollWheelPerformed;
			}
			if (InputSystem.actions == null)
			{
				this.m_InputActionAsset.FindActionMap("UI", true).Enable();
			}
			else
			{
				this.m_InputActionAsset.Enable();
			}
			this.RegisterNextPreviousAction();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000031E4 File Offset: 0x000013E4
		private void UnregisterActions()
		{
			if (this.m_PointAction != null && this.m_PointAction.action != null)
			{
				this.m_PointAction.action.performed -= this.OnPointerPerformed;
			}
			if (this.m_SubmitAction != null && this.m_SubmitAction.action != null)
			{
				this.m_SubmitAction.action.performed -= this.OnSubmitPerformed;
			}
			if (this.m_CancelAction != null && this.m_CancelAction.action != null)
			{
				this.m_CancelAction.action.performed -= this.OnCancelPerformed;
			}
			if (this.m_LeftClickAction != null && this.m_LeftClickAction.action != null)
			{
				this.m_LeftClickAction.action.performed -= this.OnLeftClickPerformed;
			}
			if (this.m_MiddleClickAction != null && this.m_MiddleClickAction.action != null)
			{
				this.m_MiddleClickAction.action.performed -= this.OnMiddleClickPerformed;
			}
			if (this.m_RightClickAction != null && this.m_RightClickAction.action != null)
			{
				this.m_RightClickAction.action.performed -= this.OnRightClickPerformed;
			}
			if (this.m_ScrollWheelAction != null && this.m_ScrollWheelAction.action != null)
			{
				this.m_ScrollWheelAction.action.performed -= this.OnScrollWheelPerformed;
			}
			this.m_PointAction = null;
			this.m_MoveAction = null;
			this.m_SubmitAction = null;
			this.m_CancelAction = null;
			this.m_LeftClickAction = null;
			this.m_MiddleClickAction = null;
			this.m_RightClickAction = null;
			this.m_ScrollWheelAction = null;
			if (this.m_InputActionAsset != null)
			{
				this.m_InputActionAsset.Disable();
			}
			this.UnregisterFixedActions();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000033C9 File Offset: 0x000015C9
		internal static void SetOnRegisterActions(Action<InputActionAsset> callback)
		{
			InputSystemProvider.s_OnRegisterActions = callback;
		}

		// Token: 0x04000006 RID: 6
		private InputSystemProvider.Configuration m_Cfg;

		// Token: 0x04000007 RID: 7
		private InputEventPartialProvider m_InputEventPartialProvider;

		// Token: 0x04000008 RID: 8
		private InputActionAsset m_InputActionAsset;

		// Token: 0x04000009 RID: 9
		private InputActionReference m_PointAction;

		// Token: 0x0400000A RID: 10
		private InputActionReference m_MoveAction;

		// Token: 0x0400000B RID: 11
		private InputActionReference m_SubmitAction;

		// Token: 0x0400000C RID: 12
		private InputActionReference m_CancelAction;

		// Token: 0x0400000D RID: 13
		private InputActionReference m_LeftClickAction;

		// Token: 0x0400000E RID: 14
		private InputActionReference m_MiddleClickAction;

		// Token: 0x0400000F RID: 15
		private InputActionReference m_RightClickAction;

		// Token: 0x04000010 RID: 16
		private InputActionReference m_ScrollWheelAction;

		// Token: 0x04000011 RID: 17
		private InputAction m_NextPreviousAction;

		// Token: 0x04000012 RID: 18
		private List<Event> m_Events = new List<Event>();

		// Token: 0x04000013 RID: 19
		private PointerState m_MouseState;

		// Token: 0x04000014 RID: 20
		private PointerState m_PenState;

		// Token: 0x04000015 RID: 21
		private bool m_SeenPenEvents;

		// Token: 0x04000016 RID: 22
		private PointerState m_TouchState;

		// Token: 0x04000017 RID: 23
		private bool m_SeenTouchEvents;

		// Token: 0x04000018 RID: 24
		private const float k_SmallestReportedMovementSqrDist = 0.01f;

		// Token: 0x04000019 RID: 25
		private NavigationEventRepeatHelper m_RepeatHelper = new NavigationEventRepeatHelper();

		// Token: 0x0400001A RID: 26
		private bool m_ResetSeenEventsOnUpdate;

		// Token: 0x0400001B RID: 27
		private const float kScrollUGUIScaleFactor = 3f;

		// Token: 0x0400001C RID: 28
		private static Action<InputActionAsset> s_OnRegisterActions;

		// Token: 0x0400001D RID: 29
		private const uint k_DefaultPlayerId = 0U;

		// Token: 0x02000005 RID: 5
		public struct Configuration
		{
			// Token: 0x0600002A RID: 42 RVA: 0x000033F0 File Offset: 0x000015F0
			public static InputSystemProvider.Configuration GetDefaultConfiguration()
			{
				InputActionAsset projectWideInputActions = InputSystem.actions;
				bool useProjectWideInputActions = projectWideInputActions != null && projectWideInputActions.FindActionMap("UI", false) != null;
				return new InputSystemProvider.Configuration
				{
					ActionAsset = (useProjectWideInputActions ? InputSystem.actions : new DefaultInputActions().asset),
					PointAction = "UI/Point",
					MoveAction = "UI/Navigate",
					SubmitAction = "UI/Submit",
					CancelAction = "UI/Cancel",
					LeftClickAction = "UI/Click",
					MiddleClickAction = "UI/MiddleClick",
					RightClickAction = "UI/RightClick",
					ScrollWheelAction = "UI/ScrollWheel"
				};
			}

			// Token: 0x0400001E RID: 30
			public InputActionAsset ActionAsset;

			// Token: 0x0400001F RID: 31
			public string PointAction;

			// Token: 0x04000020 RID: 32
			public string MoveAction;

			// Token: 0x04000021 RID: 33
			public string SubmitAction;

			// Token: 0x04000022 RID: 34
			public string CancelAction;

			// Token: 0x04000023 RID: 35
			public string LeftClickAction;

			// Token: 0x04000024 RID: 36
			public string MiddleClickAction;

			// Token: 0x04000025 RID: 37
			public string RightClickAction;

			// Token: 0x04000026 RID: 38
			public string ScrollWheelAction;
		}
	}
}
