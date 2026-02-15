using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IntegerTime;

namespace UnityEngine.InputForUI
{
	// Token: 0x02000024 RID: 36
	internal class InputManagerProvider : IEventProviderImpl
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003AE8 File Offset: 0x00001CE8
		private EventModifiers _eventModifiers
		{
			get
			{
				return this._inputEventPartialProvider._eventModifiers;
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003B50 File Offset: 0x00001D50
		public void Initialize()
		{
			if (this._inputEventPartialProvider == null)
			{
				this._inputEventPartialProvider = new InputEventPartialProvider();
			}
			this._inputEventPartialProvider.Initialize();
			this._inputEventPartialProvider._sendNavigationEventOnTabKey = true;
			this._mouseState.Reset();
			this._isPenPresent = false;
			this._seenAtLeastOnePenPosition = false;
			this._lastSeenPenPositionForDetection = default(Vector2);
			this._penState.Reset();
			this._lastPenData = default(PenData);
			this._touchFingerIdToFingerIndex.Clear();
			this._touchNextFingerIndex = 0;
			this._touchState.Reset();
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000331D File Offset: 0x0000151D
		public void Shutdown()
		{
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public void Update()
		{
			this._inputEventPartialProvider.Update();
			DiscreteTime currentTime = (DiscreteTime)this._time.timeAsRational;
			this.DetectPen();
			bool touchWasReported = false;
			bool touchSupported = this._input.touchSupported;
			if (touchSupported)
			{
				touchWasReported = this.CheckTouchEvents(currentTime);
			}
			bool penWasReported = false;
			bool flag = !touchWasReported && this._isPenPresent;
			if (flag)
			{
				DiscreteTime discreteTime = currentTime;
				PenData lastPenContactEvent = this._input.GetLastPenContactEvent();
				penWasReported = this.CheckPenEvent(discreteTime, in lastPenContactEvent);
			}
			else
			{
				this._penState.Reset();
			}
			bool flag2 = !penWasReported && !touchWasReported && this._input.mousePresent;
			if (flag2)
			{
				this.CheckMouseEvents(currentTime, false);
			}
			else
			{
				this.CheckMouseEvents(currentTime, true);
				this._mouseState.LastPositionValid = false;
			}
			bool mousePresent = this._input.mousePresent;
			if (mousePresent)
			{
				this.CheckMouseScroll(currentTime);
			}
			this.CheckIfIMEChanged(currentTime);
			this.DirectionNavigation(currentTime);
			this.SubmitCancelNavigation(currentTime);
			this.NextPreviousNavigation(currentTime);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003CE4 File Offset: 0x00001EE4
		private bool CheckTouchEvents(DiscreteTime currentTime)
		{
			bool allAreReleased = true;
			bool touchWasReported = false;
			for (int i = 0; i < this._input.touchCount; i++)
			{
				Touch touch = this._input.GetTouch(i);
				bool flag = touch.type == TouchType.Indirect || touch.phase == TouchPhase.Stationary;
				if (!flag)
				{
					int fingerIndex;
					bool flag2 = !this._touchFingerIdToFingerIndex.TryGetValue(touch.fingerId, out fingerIndex);
					if (flag2)
					{
						int touchNextFingerIndex = this._touchNextFingerIndex;
						this._touchNextFingerIndex = touchNextFingerIndex + 1;
						fingerIndex = touchNextFingerIndex;
						this._touchFingerIdToFingerIndex.Add(touch.fingerId, fingerIndex);
					}
					int targetDisplay;
					Vector2 position = InputManagerProvider.MultiDisplayBottomLeftToPanelPosition(touch.position, out targetDisplay);
					PointerEvent.Type type = PointerEvent.Type.PointerMoved;
					PointerEvent.Button button = PointerEvent.Button.None;
					switch (touch.phase)
					{
					case TouchPhase.Began:
						type = PointerEvent.Type.ButtonPressed;
						button = PointerEvent.Button.Primary;
						allAreReleased = false;
						this._touchState.OnButtonDown(currentTime, button);
						break;
					case TouchPhase.Moved:
						allAreReleased = false;
						break;
					case TouchPhase.Ended:
						type = PointerEvent.Type.ButtonReleased;
						button = PointerEvent.Button.Primary;
						this._touchState.OnButtonUp(currentTime, button);
						break;
					case TouchPhase.Canceled:
						type = PointerEvent.Type.TouchCanceled;
						button = PointerEvent.Button.Primary;
						this._touchState.OnButtonUp(currentTime, button);
						break;
					}
					Event @event = Event.From(new PointerEvent
					{
						type = type,
						pointerIndex = fingerIndex,
						position = position,
						deltaPosition = touch.deltaPosition,
						scroll = Vector2.zero,
						displayIndex = targetDisplay,
						tilt = InputManagerProvider.AzimuthAndAlitutudeToTilt(touch.altitudeAngle, touch.azimuthAngle),
						twist = 0f,
						pressure = ((Mathf.Abs(touch.maximumPossiblePressure) > Mathf.Epsilon) ? (touch.pressure / touch.maximumPossiblePressure) : 1f),
						isInverted = false,
						button = button,
						buttonsState = this._touchState.ButtonsState,
						clickCount = this._touchState.ClickCount,
						timestamp = currentTime,
						eventSource = EventSource.Touch,
						playerId = 0U,
						eventModifiers = this._eventModifiers
					});
					EventProvider.Dispatch(in @event);
					touchWasReported = true;
				}
			}
			bool flag3 = allAreReleased;
			if (flag3)
			{
				this._touchNextFingerIndex = 0;
				this._touchFingerIdToFingerIndex.Clear();
			}
			return touchWasReported;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003F54 File Offset: 0x00002154
		private void DetectPen()
		{
			bool isPenPresent = this._isPenPresent;
			if (!isPenPresent)
			{
				Vector2 position = this._input.GetLastPenContactEvent().position;
				bool seenAtLeastOnePenPosition = this._seenAtLeastOnePenPosition;
				if (seenAtLeastOnePenPosition)
				{
					float sqrDist = (position - this._lastSeenPenPositionForDetection).sqrMagnitude;
					this._isPenPresent = sqrDist >= 0.01f;
				}
				else
				{
					this._lastSeenPenPositionForDetection = position;
					this._seenAtLeastOnePenPosition = true;
				}
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003FC4 File Offset: 0x000021C4
		private static PointerEvent.Button PenStatusToButton(PenStatus status)
		{
			bool flag = (status & PenStatus.Eraser) > PenStatus.None;
			PointerEvent.Button button;
			if (flag)
			{
				button = PointerEvent.Button.PenEraserInTouch;
			}
			else
			{
				bool flag2 = (status & PenStatus.Barrel) > PenStatus.None;
				if (flag2)
				{
					button = PointerEvent.Button.PenBarrelButton;
				}
				else
				{
					button = PointerEvent.Button.Primary;
				}
			}
			return button;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003FF4 File Offset: 0x000021F4
		private bool CheckPenEvent(DiscreteTime currentTime, in PenData currentPenData)
		{
			Vector2 position = currentPenData.position;
			int targetDisplay = 0;
			Vector2 delta = (this._penState.LastPositionValid ? (position - this._penState.LastPosition) : Vector2.zero);
			PointerEvent.Button button = PointerEvent.Button.None;
			bool flag = currentPenData.contactType != this._lastPenData.contactType;
			PointerEvent.Type type;
			if (flag)
			{
				PenEventType contactType = currentPenData.contactType;
				PenEventType penEventType = contactType;
				if (penEventType != PenEventType.PenDown)
				{
					if (penEventType != PenEventType.PenUp)
					{
						type = PointerEvent.Type.PointerMoved;
					}
					else
					{
						type = PointerEvent.Type.ButtonReleased;
						button = InputManagerProvider.PenStatusToButton(this._lastPenData.penStatus);
						this._penState.OnButtonUp(currentTime, button);
					}
				}
				else
				{
					type = PointerEvent.Type.ButtonPressed;
					button = InputManagerProvider.PenStatusToButton(currentPenData.penStatus);
					this._penState.OnButtonDown(currentTime, button);
				}
			}
			else
			{
				type = PointerEvent.Type.PointerMoved;
			}
			this._lastPenData = currentPenData;
			bool penWasReported = false;
			bool flag2 = type != PointerEvent.Type.PointerMoved || !this._penState.LastPositionValid || delta.sqrMagnitude >= 0.01f;
			if (flag2)
			{
				Event @event = Event.From(new PointerEvent
				{
					type = type,
					pointerIndex = 0,
					position = position,
					deltaPosition = delta,
					scroll = Vector2.zero,
					displayIndex = targetDisplay,
					tilt = currentPenData.tilt,
					twist = currentPenData.twist,
					pressure = currentPenData.pressure,
					isInverted = ((currentPenData.penStatus & PenStatus.Inverted) > PenStatus.None),
					button = button,
					buttonsState = this._penState.ButtonsState,
					clickCount = this._penState.ClickCount,
					timestamp = currentTime,
					eventSource = EventSource.Pen,
					playerId = 0U,
					eventModifiers = this._eventModifiers
				});
				EventProvider.Dispatch(in @event);
				penWasReported = true;
			}
			this._penState.OnMove(currentTime, position, targetDisplay);
			return penWasReported;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000041F0 File Offset: 0x000023F0
		private void CheckMouseEvents(DiscreteTime currentTime, bool muted = false)
		{
			int targetDisplay;
			Vector2 position = InputManagerProvider.MultiDisplayBottomLeftToPanelPosition(this._input.mousePosition, out targetDisplay);
			bool lastPositionValid = this._mouseState.LastPositionValid;
			if (lastPositionValid)
			{
				Vector2 delta = position - this._mouseState.LastPosition;
				bool flag = delta.sqrMagnitude >= 0.01f;
				if (flag)
				{
					bool flag2 = !muted;
					if (flag2)
					{
						PointerEvent pointerEvent = new PointerEvent
						{
							type = PointerEvent.Type.PointerMoved,
							pointerIndex = 0,
							position = position,
							deltaPosition = delta,
							scroll = Vector2.zero,
							displayIndex = targetDisplay,
							tilt = Vector2.zero,
							twist = 0f,
							pressure = 0f,
							isInverted = false,
							button = PointerEvent.Button.None,
							buttonsState = this._mouseState.ButtonsState,
							clickCount = 0,
							timestamp = currentTime,
							eventSource = EventSource.Mouse,
							playerId = 0U,
							eventModifiers = this._eventModifiers
						};
						Event @event = Event.From(pointerEvent);
						EventProvider.Dispatch(in @event);
					}
					this._mouseState.OnMove(currentTime, position, targetDisplay);
				}
			}
			else
			{
				this._mouseState.OnMove(currentTime, position, targetDisplay);
			}
			for (int buttonIndex = 0; buttonIndex < 5; buttonIndex++)
			{
				PointerEvent.Button button = PointerEvent.ButtonFromButtonIndex(buttonIndex);
				bool previousState = this._mouseState.ButtonsState.Get(button);
				bool isDown = this._input.GetMouseButtonDown(buttonIndex);
				bool isUp = this._input.GetMouseButtonUp(buttonIndex);
				bool currentState = this._input.GetMouseButton(buttonIndex);
				InputManagerProvider.ButtonEventsIterator it = InputManagerProvider.ButtonEventsIterator.FromState(previousState, isDown, isUp, currentState);
				bool previousStateInIterator = previousState;
				while (it.MoveNext())
				{
					bool flag3 = it.Current;
					this._mouseState.OnButtonChange(currentTime, button, previousStateInIterator, flag3);
					previousStateInIterator = it.Current;
					bool flag4 = !muted;
					if (flag4)
					{
						PointerEvent pointerEvent = new PointerEvent
						{
							type = (it.Current ? PointerEvent.Type.ButtonPressed : PointerEvent.Type.ButtonReleased),
							pointerIndex = 0,
							position = this._mouseState.LastPosition,
							deltaPosition = Vector2.zero,
							scroll = Vector2.zero,
							displayIndex = this._mouseState.LastDisplayIndex,
							tilt = Vector2.zero,
							twist = 0f,
							pressure = 0f,
							isInverted = false,
							button = button,
							buttonsState = this._mouseState.ButtonsState,
							clickCount = this._mouseState.ClickCount,
							timestamp = currentTime,
							eventSource = EventSource.Mouse,
							playerId = 0U,
							eventModifiers = this._eventModifiers
						};
						Event @event = Event.From(pointerEvent);
						EventProvider.Dispatch(in @event);
					}
				}
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004500 File Offset: 0x00002700
		private void CheckMouseScroll(DiscreteTime currentTime)
		{
			Vector2 scrollDelta = this._input.mouseScrollDelta;
			bool flag = scrollDelta.sqrMagnitude < 0.01f;
			if (!flag)
			{
				int targetDisplay = 0;
				bool lastPositionValid = this._mouseState.LastPositionValid;
				Vector2 position;
				if (lastPositionValid)
				{
					position = this._mouseState.LastPosition;
					targetDisplay = this._mouseState.LastDisplayIndex;
				}
				else
				{
					position = InputManagerProvider.MultiDisplayBottomLeftToPanelPosition(this._input.mousePosition, out targetDisplay);
				}
				scrollDelta.x *= 3f;
				scrollDelta.y *= -3f;
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
					buttonsState = this._mouseState.ButtonsState,
					clickCount = 0,
					timestamp = currentTime,
					eventSource = EventSource.Mouse,
					playerId = 0U,
					eventModifiers = this._eventModifiers
				});
				EventProvider.Dispatch(in @event);
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000465C File Offset: 0x0000285C
		private void NextPreviousNavigation(DiscreteTime currentTime)
		{
			int navigateNext = (this.InputManagerGetButtonDownOrDefault(this._configuration.NavigateNextButton) ? 1 : 0) + (this.InputManagerGetButtonDownOrDefault(this._configuration.NavigatePreviousButton) ? (-1) : 0);
			bool flag = navigateNext != 0;
			if (flag)
			{
				bool isShiftPressed = this._eventModifiers.isShiftPressed;
				if (isShiftPressed)
				{
					navigateNext = -navigateNext;
				}
				Event @event = Event.From(new NavigationEvent
				{
					type = NavigationEvent.Type.Move,
					direction = ((navigateNext >= 0) ? NavigationEvent.Direction.Next : NavigationEvent.Direction.Previous),
					timestamp = currentTime,
					eventSource = this.GetEventSourceFromPressedKey(),
					playerId = 0U,
					eventModifiers = this._eventModifiers
				});
				EventProvider.Dispatch(in @event);
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004718 File Offset: 0x00002918
		private void SubmitCancelNavigation(DiscreteTime currentTime)
		{
			bool flag = this.InputManagerGetButtonDownOrDefault(this._configuration.SubmitButton);
			if (flag)
			{
				Event @event = Event.From(new NavigationEvent
				{
					type = NavigationEvent.Type.Submit,
					direction = NavigationEvent.Direction.None,
					timestamp = currentTime,
					eventSource = this.GetEventSourceFromPressedKey(),
					playerId = 0U,
					eventModifiers = this._eventModifiers
				});
				EventProvider.Dispatch(in @event);
			}
			bool flag2 = this.InputManagerGetButtonDownOrDefault(this._configuration.CancelButton);
			if (flag2)
			{
				Event @event = Event.From(new NavigationEvent
				{
					type = NavigationEvent.Type.Cancel,
					direction = NavigationEvent.Direction.None,
					timestamp = currentTime,
					eventSource = this.GetEventSourceFromPressedKey(),
					playerId = 0U,
					eventModifiers = this._eventModifiers
				});
				EventProvider.Dispatch(in @event);
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004800 File Offset: 0x00002A00
		private void DirectionNavigation(DiscreteTime currentTime)
		{
			ValueTuple<Vector2, bool> valueTuple = this.ReadCurrentNavigationMoveVector();
			Vector2 move = valueTuple.Item1;
			bool axesButtonWerePressed = valueTuple.Item2;
			NavigationEvent.Direction direction = NavigationEvent.DetermineMoveDirection(move, 0.6f);
			bool flag = direction == NavigationEvent.Direction.None;
			if (flag)
			{
				this._navigationEventRepeatHelper.Reset();
			}
			else
			{
				bool flag2 = this._navigationEventRepeatHelper.ShouldSendMoveEvent(currentTime, direction, axesButtonWerePressed);
				if (flag2)
				{
					EventSource eventSource = this.GetEventSourceFromPressedKey();
					bool flag3 = eventSource == EventSource.Unspecified && !axesButtonWerePressed;
					if (flag3)
					{
						eventSource = EventSource.Gamepad;
					}
					Event @event = Event.From(new NavigationEvent
					{
						type = NavigationEvent.Type.Move,
						direction = direction,
						timestamp = currentTime,
						eventSource = eventSource,
						playerId = 0U,
						eventModifiers = this._eventModifiers
					});
					EventProvider.Dispatch(in @event);
				}
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000048CC File Offset: 0x00002ACC
		private void CheckIfIMEChanged(DiscreteTime currentTime)
		{
			string currentCompositionString = this._input.compositionString;
			bool flag = string.IsNullOrEmpty(this._compositionString) != string.IsNullOrEmpty(currentCompositionString) && this._compositionString != currentCompositionString;
			if (flag)
			{
				this._compositionString = currentCompositionString;
				Event @event = Event.From(this.ToIMECompositionEvent(currentTime, this._compositionString));
				EventProvider.Dispatch(in @event);
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004930 File Offset: 0x00002B30
		public void OnFocusChanged(bool focus)
		{
			this._inputEventPartialProvider.OnFocusChanged(focus);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004940 File Offset: 0x00002B40
		private EventSource GetEventSourceFromPressedKey()
		{
			bool flag = this.InputManagerKeyboardWasPressed();
			EventSource eventSource;
			if (flag)
			{
				eventSource = EventSource.Keyboard;
			}
			else
			{
				bool flag2 = this.InputManagerJoystickWasPressed();
				if (flag2)
				{
					eventSource = EventSource.Gamepad;
				}
				else
				{
					eventSource = EventSource.Unspecified;
				}
			}
			return eventSource;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004970 File Offset: 0x00002B70
		private bool InputManagerJoystickWasPressed()
		{
			for (KeyCode key = KeyCode.Joystick1Button0; key <= KeyCode.Joystick8Button19; key++)
			{
				bool key2 = this._input.GetKey(key);
				if (key2)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000049B8 File Offset: 0x00002BB8
		private bool InputManagerKeyboardWasPressed()
		{
			for (KeyCode key = KeyCode.None; key <= KeyCode.Menu; key++)
			{
				bool key2 = this._input.GetKey(key);
				if (key2)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000049FC File Offset: 0x00002BFC
		private float InputManagerGetAxisRawOrDefault(string axisName)
		{
			float num;
			try
			{
				num = ((!string.IsNullOrEmpty(axisName)) ? this._input.GetAxisRaw(axisName) : 0f);
			}
			catch
			{
				num = 0f;
			}
			return num;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004A44 File Offset: 0x00002C44
		private bool InputManagerGetButtonDownOrDefault(string axisName)
		{
			bool flag;
			try
			{
				flag = !string.IsNullOrEmpty(axisName) && this._input.GetButtonDown(axisName);
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004A8C File Offset: 0x00002C8C
		private ValueTuple<Vector2, bool> ReadCurrentNavigationMoveVector()
		{
			Vector2 move = new Vector2(this.InputManagerGetAxisRawOrDefault(this._configuration.HorizontalAxis), this.InputManagerGetAxisRawOrDefault(this._configuration.VerticalAxis));
			bool btnPressed = false;
			bool flag = this.InputManagerGetButtonDownOrDefault(this._configuration.HorizontalAxis);
			if (flag)
			{
				bool flag2 = move.x < 0f;
				if (flag2)
				{
					move.x = -1f;
				}
				else
				{
					bool flag3 = move.x > 0f;
					if (flag3)
					{
						move.x = 1f;
					}
				}
				btnPressed = true;
			}
			bool flag4 = this.InputManagerGetButtonDownOrDefault(this._configuration.VerticalAxis);
			if (flag4)
			{
				bool flag5 = move.y < 0f;
				if (flag5)
				{
					move.y = -1f;
				}
				else
				{
					bool flag6 = move.y > 0f;
					if (flag6)
					{
						move.y = 1f;
					}
				}
				btnPressed = true;
			}
			return new ValueTuple<Vector2, bool>(move, btnPressed);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004B84 File Offset: 0x00002D84
		private IMECompositionEvent ToIMECompositionEvent(DiscreteTime currentTime, string compositionString)
		{
			return new IMECompositionEvent
			{
				compositionString = compositionString,
				timestamp = currentTime,
				eventSource = EventSource.Unspecified,
				playerId = 0U,
				eventModifiers = this._eventModifiers
			};
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004BD0 File Offset: 0x00002DD0
		internal static float TiltToAzimuth(Vector2 tilt)
		{
			float azimuth = 0f;
			bool flag = tilt.x != 0f;
			if (flag)
			{
				azimuth = 1.5707964f - Mathf.Atan2(-Mathf.Cos(tilt.x) * Mathf.Sin(tilt.y), Mathf.Cos(tilt.y) * Mathf.Sin(tilt.x));
				bool flag2 = azimuth < 0f;
				if (flag2)
				{
					azimuth += 6.2831855f;
				}
				bool flag3 = azimuth >= 1.5707964f;
				if (flag3)
				{
					azimuth -= 1.5707964f;
				}
				else
				{
					azimuth += 4.712389f;
				}
			}
			return azimuth;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004C74 File Offset: 0x00002E74
		internal static Vector2 AzimuthAndAlitutudeToTilt(float altitude, float azimuth)
		{
			return new Vector2(0f, 0f)
			{
				x = Mathf.Atan(Mathf.Cos(azimuth) * Mathf.Cos(altitude) / Mathf.Sin(azimuth)),
				y = Mathf.Atan(Mathf.Cos(azimuth) * Mathf.Sin(altitude) / Mathf.Sin(azimuth))
			};
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004CD8 File Offset: 0x00002ED8
		internal static float TiltToAltitude(Vector2 tilt)
		{
			return 1.5707964f - Mathf.Acos(Mathf.Cos(tilt.x) * Mathf.Cos(tilt.y));
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004D0C File Offset: 0x00002F0C
		private static Vector2 MultiDisplayBottomLeftToPanelPosition(Vector2 position, out int targetDisplay)
		{
			int? targetDisplayMaybe;
			Vector2 screenPosition = InputManagerProvider.MultiDisplayToLocalScreenPosition(position, out targetDisplayMaybe);
			targetDisplay = targetDisplayMaybe.GetValueOrDefault();
			return InputManagerProvider.ScreenBottomLeftToPanelPosition(screenPosition, targetDisplay);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004D38 File Offset: 0x00002F38
		private static Vector2 MultiDisplayToLocalScreenPosition(Vector2 position, out int? targetDisplay)
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

		// Token: 0x060000AC RID: 172 RVA: 0x00004D8C File Offset: 0x00002F8C
		private static Vector2 ScreenBottomLeftToPanelPosition(Vector2 position, int targetDisplay)
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

		// Token: 0x040000B6 RID: 182
		private InputEventPartialProvider _inputEventPartialProvider;

		// Token: 0x040000B7 RID: 183
		private string _compositionString = string.Empty;

		// Token: 0x040000B8 RID: 184
		private InputManagerProvider.Configuration _configuration = InputManagerProvider.Configuration.GetDefaultConfiguration();

		// Token: 0x040000B9 RID: 185
		private InputManagerProvider.IInput _input = new InputManagerProvider.Input();

		// Token: 0x040000BA RID: 186
		private InputManagerProvider.ITime _time = new InputManagerProvider.Time();

		// Token: 0x040000BB RID: 187
		private NavigationEventRepeatHelper _navigationEventRepeatHelper = new NavigationEventRepeatHelper();

		// Token: 0x040000BC RID: 188
		private PointerState _mouseState;

		// Token: 0x040000BD RID: 189
		private bool _isPenPresent;

		// Token: 0x040000BE RID: 190
		private bool _seenAtLeastOnePenPosition;

		// Token: 0x040000BF RID: 191
		private Vector2 _lastSeenPenPositionForDetection;

		// Token: 0x040000C0 RID: 192
		private PointerState _penState;

		// Token: 0x040000C1 RID: 193
		private PenData _lastPenData;

		// Token: 0x040000C2 RID: 194
		private Dictionary<int, int> _touchFingerIdToFingerIndex = new Dictionary<int, int>();

		// Token: 0x040000C3 RID: 195
		private int _touchNextFingerIndex;

		// Token: 0x040000C4 RID: 196
		private PointerState _touchState;

		// Token: 0x02000025 RID: 37
		private struct ButtonEventsIterator : IEnumerator
		{
			// Token: 0x17000032 RID: 50
			// (get) Token: 0x060000AD RID: 173 RVA: 0x00004DD7 File Offset: 0x00002FD7
			public bool Current
			{
				get
				{
					return this._bit % 2 == 0;
				}
			}

			// Token: 0x060000AE RID: 174 RVA: 0x00004DE4 File Offset: 0x00002FE4
			public bool MoveNext()
			{
				for (;;)
				{
					this._bit++;
					bool flag = (this._mask & (1U << this._bit)) > 0U;
					if (flag)
					{
						break;
					}
					if (this._bit >= 4)
					{
						goto Block_1;
					}
				}
				return true;
				Block_1:
				return false;
			}

			// Token: 0x060000AF RID: 175 RVA: 0x00004E31 File Offset: 0x00003031
			public void Reset()
			{
				this._bit = -1;
			}

			// Token: 0x17000033 RID: 51
			// (get) Token: 0x060000B0 RID: 176 RVA: 0x00004E3B File Offset: 0x0000303B
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060000B1 RID: 177 RVA: 0x00004E48 File Offset: 0x00003048
			public static InputManagerProvider.ButtonEventsIterator FromState(bool previous, bool down, bool up, bool current)
			{
				uint mask = ((!previous && current) ? 1U : ((previous && !current) ? 2U : 0U));
				return new InputManagerProvider.ButtonEventsIterator
				{
					_mask = mask,
					_bit = -1
				};
			}

			// Token: 0x040000C5 RID: 197
			private uint _mask;

			// Token: 0x040000C6 RID: 198
			private int _bit;
		}

		// Token: 0x02000026 RID: 38
		public struct Configuration
		{
			// Token: 0x060000B2 RID: 178 RVA: 0x00004E88 File Offset: 0x00003088
			public static InputManagerProvider.Configuration GetDefaultConfiguration()
			{
				return new InputManagerProvider.Configuration
				{
					HorizontalAxis = "Horizontal",
					VerticalAxis = "Vertical",
					SubmitButton = "Submit",
					CancelButton = "Cancel",
					NavigateNextButton = "Next",
					NavigatePreviousButton = "Previous",
					InputActionsPerSecond = 10f,
					RepeatDelay = 0.5f
				};
			}

			// Token: 0x040000C7 RID: 199
			public string HorizontalAxis;

			// Token: 0x040000C8 RID: 200
			public string VerticalAxis;

			// Token: 0x040000C9 RID: 201
			public string SubmitButton;

			// Token: 0x040000CA RID: 202
			public string CancelButton;

			// Token: 0x040000CB RID: 203
			public string NavigateNextButton;

			// Token: 0x040000CC RID: 204
			public string NavigatePreviousButton;

			// Token: 0x040000CD RID: 205
			public float InputActionsPerSecond;

			// Token: 0x040000CE RID: 206
			public float RepeatDelay;
		}

		// Token: 0x02000027 RID: 39
		internal interface IInput
		{
			// Token: 0x17000034 RID: 52
			// (get) Token: 0x060000B3 RID: 179
			string compositionString { get; }

			// Token: 0x060000B4 RID: 180
			bool GetKey(KeyCode keyCode);

			// Token: 0x060000B5 RID: 181
			bool GetButtonDown(string button);

			// Token: 0x060000B6 RID: 182
			float GetAxisRaw(string axis);

			// Token: 0x060000B7 RID: 183
			PenData GetLastPenContactEvent();

			// Token: 0x17000035 RID: 53
			// (get) Token: 0x060000B8 RID: 184
			bool touchSupported { get; }

			// Token: 0x17000036 RID: 54
			// (get) Token: 0x060000B9 RID: 185
			int touchCount { get; }

			// Token: 0x060000BA RID: 186
			Touch GetTouch(int index);

			// Token: 0x17000037 RID: 55
			// (get) Token: 0x060000BB RID: 187
			bool mousePresent { get; }

			// Token: 0x060000BC RID: 188
			bool GetMouseButton(int button);

			// Token: 0x060000BD RID: 189
			bool GetMouseButtonDown(int button);

			// Token: 0x060000BE RID: 190
			bool GetMouseButtonUp(int button);

			// Token: 0x17000038 RID: 56
			// (get) Token: 0x060000BF RID: 191
			Vector3 mousePosition { get; }

			// Token: 0x17000039 RID: 57
			// (get) Token: 0x060000C0 RID: 192
			Vector2 mouseScrollDelta { get; }
		}

		// Token: 0x02000028 RID: 40
		private class Input : InputManagerProvider.IInput
		{
			// Token: 0x1700003A RID: 58
			// (get) Token: 0x060000C1 RID: 193 RVA: 0x00004F03 File Offset: 0x00003103
			public string compositionString
			{
				get
				{
					return UnityEngine.Input.compositionString;
				}
			}

			// Token: 0x060000C2 RID: 194 RVA: 0x00004F0A File Offset: 0x0000310A
			public bool GetKey(KeyCode key)
			{
				return UnityEngine.Input.GetKey(key);
			}

			// Token: 0x060000C3 RID: 195 RVA: 0x00004F12 File Offset: 0x00003112
			public bool GetButtonDown(string button)
			{
				return UnityEngine.Input.GetButtonDown(button);
			}

			// Token: 0x060000C4 RID: 196 RVA: 0x00004F1A File Offset: 0x0000311A
			public float GetAxisRaw(string axis)
			{
				return UnityEngine.Input.GetAxisRaw(axis);
			}

			// Token: 0x060000C5 RID: 197 RVA: 0x00004F22 File Offset: 0x00003122
			public PenData GetLastPenContactEvent()
			{
				return UnityEngine.Input.GetLastPenContactEvent();
			}

			// Token: 0x1700003B RID: 59
			// (get) Token: 0x060000C6 RID: 198 RVA: 0x00004F29 File Offset: 0x00003129
			public bool touchSupported
			{
				get
				{
					return UnityEngine.Input.touchSupported;
				}
			}

			// Token: 0x1700003C RID: 60
			// (get) Token: 0x060000C7 RID: 199 RVA: 0x00004F30 File Offset: 0x00003130
			public int touchCount
			{
				get
				{
					return UnityEngine.Input.touchCount;
				}
			}

			// Token: 0x060000C8 RID: 200 RVA: 0x00004F37 File Offset: 0x00003137
			public Touch GetTouch(int index)
			{
				return UnityEngine.Input.GetTouch(index);
			}

			// Token: 0x1700003D RID: 61
			// (get) Token: 0x060000C9 RID: 201 RVA: 0x00004F3F File Offset: 0x0000313F
			public bool mousePresent
			{
				get
				{
					return UnityEngine.Input.mousePresent;
				}
			}

			// Token: 0x060000CA RID: 202 RVA: 0x00004F46 File Offset: 0x00003146
			public bool GetMouseButton(int button)
			{
				return UnityEngine.Input.GetMouseButton(button);
			}

			// Token: 0x060000CB RID: 203 RVA: 0x00004F4E File Offset: 0x0000314E
			public bool GetMouseButtonDown(int button)
			{
				return UnityEngine.Input.GetMouseButtonDown(button);
			}

			// Token: 0x060000CC RID: 204 RVA: 0x00004F56 File Offset: 0x00003156
			public bool GetMouseButtonUp(int button)
			{
				return UnityEngine.Input.GetMouseButtonUp(button);
			}

			// Token: 0x1700003E RID: 62
			// (get) Token: 0x060000CD RID: 205 RVA: 0x00004F5E File Offset: 0x0000315E
			public Vector3 mousePosition
			{
				get
				{
					return UnityEngine.Input.mousePosition;
				}
			}

			// Token: 0x1700003F RID: 63
			// (get) Token: 0x060000CE RID: 206 RVA: 0x00004F65 File Offset: 0x00003165
			public Vector2 mouseScrollDelta
			{
				get
				{
					return UnityEngine.Input.mouseScrollDelta;
				}
			}
		}

		// Token: 0x02000029 RID: 41
		internal interface ITime
		{
			// Token: 0x17000040 RID: 64
			// (get) Token: 0x060000D0 RID: 208
			RationalTime timeAsRational { get; }
		}

		// Token: 0x0200002A RID: 42
		private class Time : InputManagerProvider.ITime
		{
			// Token: 0x17000041 RID: 65
			// (get) Token: 0x060000D1 RID: 209 RVA: 0x00004F6C File Offset: 0x0000316C
			public RationalTime timeAsRational
			{
				get
				{
					return UnityEngine.Time.timeAsRational;
				}
			}
		}
	}
}
