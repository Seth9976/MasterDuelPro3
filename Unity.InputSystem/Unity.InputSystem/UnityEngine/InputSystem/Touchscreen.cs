using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Profiling;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x020000BC RID: 188
	[InputControlLayout(stateType = typeof(TouchscreenState), isGenericTypeOfDevice = true)]
	public class Touchscreen : Pointer, IInputStateCallbackReceiver, IEventMerger, ICustomDeviceReset
	{
		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x00033D32 File Offset: 0x00031F32
		// (set) Token: 0x06000A08 RID: 2568 RVA: 0x00033D3A File Offset: 0x00031F3A
		public TouchControl primaryTouch { get; protected set; }

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x00033D43 File Offset: 0x00031F43
		// (set) Token: 0x06000A0A RID: 2570 RVA: 0x00033D4B File Offset: 0x00031F4B
		public ReadOnlyArray<TouchControl> touches { get; protected set; }

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x00033D54 File Offset: 0x00031F54
		// (set) Token: 0x06000A0C RID: 2572 RVA: 0x00033D61 File Offset: 0x00031F61
		protected TouchControl[] touchControlArray
		{
			get
			{
				return this.touches.m_Array;
			}
			set
			{
				this.touches = new ReadOnlyArray<TouchControl>(value);
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x00033D6F File Offset: 0x00031F6F
		// (set) Token: 0x06000A0E RID: 2574 RVA: 0x00033D76 File Offset: 0x00031F76
		public new static Touchscreen current { get; internal set; }

		// Token: 0x06000A0F RID: 2575 RVA: 0x00033D7E File Offset: 0x00031F7E
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			Touchscreen.current = this;
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x00033D8C File Offset: 0x00031F8C
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (Touchscreen.current == this)
			{
				Touchscreen.current = null;
			}
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x00033DA4 File Offset: 0x00031FA4
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.primaryTouch = base.GetChildControl<TouchControl>("primaryTouch");
			int touchControlCount = 0;
			using (ReadOnlyArray<InputControl>.Enumerator enumerator = base.children.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current is TouchControl)
					{
						touchControlCount++;
					}
				}
			}
			if (touchControlCount >= 1)
			{
				touchControlCount--;
			}
			TouchControl[] touchArray = new TouchControl[touchControlCount];
			int touchIndex = 0;
			foreach (InputControl child in base.children)
			{
				if (child != this.primaryTouch)
				{
					TouchControl control = child as TouchControl;
					if (control != null)
					{
						touchArray[touchIndex++] = control;
					}
				}
			}
			this.touches = new ReadOnlyArray<TouchControl>(touchArray);
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00033E94 File Offset: 0x00032094
		protected new unsafe void OnNextUpdate()
		{
			void* statePtr = base.currentStatePtr;
			TouchState* touchStatePtr = (TouchState*)((byte*)((byte*)statePtr + base.stateBlock.byteOffset) + 56);
			int i = 0;
			while (i < this.touches.Count)
			{
				if (touchStatePtr->delta != default(Vector2))
				{
					InputState.Change<Vector2>(this.touches[i].delta, Vector2.zero, InputUpdateType.None, default(InputEventPtr));
				}
				if (touchStatePtr->tapCount > 0 && InputState.currentTime >= touchStatePtr->startTime + (double)Touchscreen.s_TapTime + (double)Touchscreen.s_TapDelayTime)
				{
					InputState.Change<byte>(this.touches[i].tapCount, 0, InputUpdateType.None, default(InputEventPtr));
				}
				i++;
				touchStatePtr++;
			}
			TouchState* primaryTouchState = (TouchState*)((byte*)statePtr + base.stateBlock.byteOffset);
			if (primaryTouchState->delta != default(Vector2))
			{
				InputState.Change<Vector2>(this.primaryTouch.delta, Vector2.zero, InputUpdateType.None, default(InputEventPtr));
			}
			if (primaryTouchState->tapCount > 0 && InputState.currentTime >= primaryTouchState->startTime + (double)Touchscreen.s_TapTime + (double)Touchscreen.s_TapDelayTime)
			{
				InputState.Change<byte>(this.primaryTouch.tapCount, 0, InputUpdateType.None, default(InputEventPtr));
			}
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00034000 File Offset: 0x00032200
		protected new unsafe void OnStateEvent(InputEventPtr eventPtr)
		{
			if (eventPtr.type == 1145852993)
			{
				return;
			}
			StateEvent* stateEventPtr = StateEvent.FromUnchecked(eventPtr);
			if (stateEventPtr->stateFormat != TouchState.Format)
			{
				InputState.Change(this, eventPtr, InputUpdateType.None);
				return;
			}
			void* currentStatePtr = base.currentStatePtr;
			TouchState* currentTouchState = (TouchState*)((byte*)currentStatePtr + this.touches[0].stateBlock.byteOffset);
			TouchState* primaryTouchState = (TouchState*)((byte*)currentStatePtr + this.primaryTouch.stateBlock.byteOffset);
			int touchControlCount = this.touches.Count;
			TouchState newTouchState;
			if (stateEventPtr->stateSizeInBytes == 56U)
			{
				newTouchState = *(TouchState*)stateEventPtr->state;
			}
			else
			{
				newTouchState = default(TouchState);
				UnsafeUtility.MemCpy(UnsafeUtility.AddressOf<TouchState>(ref newTouchState), stateEventPtr->state, (long)((ulong)stateEventPtr->stateSizeInBytes));
			}
			newTouchState.tapCount = 0;
			newTouchState.isTapPress = false;
			newTouchState.isTapRelease = false;
			newTouchState.updateStepCount = InputUpdate.s_UpdateStepCount;
			if (newTouchState.phase != TouchPhase.Began)
			{
				int touchId = newTouchState.touchId;
				int i = 0;
				while (i < touchControlCount)
				{
					if (currentTouchState[i].touchId == touchId)
					{
						bool isPrimaryTouch = currentTouchState[i].isPrimaryTouch;
						newTouchState.isPrimaryTouch = isPrimaryTouch;
						if (newTouchState.delta == default(Vector2))
						{
							newTouchState.delta = newTouchState.position - currentTouchState[i].position;
						}
						newTouchState.delta += currentTouchState[i].delta;
						newTouchState.startTime = currentTouchState[i].startTime;
						newTouchState.startPosition = currentTouchState[i].startPosition;
						bool isTap = newTouchState.isNoneEndedOrCanceled && eventPtr.time - newTouchState.startTime <= (double)Touchscreen.s_TapTime && (newTouchState.position - newTouchState.startPosition).sqrMagnitude <= Touchscreen.s_TapRadiusSquared;
						if (isTap)
						{
							newTouchState.tapCount = currentTouchState[i].tapCount + 1;
						}
						else
						{
							newTouchState.tapCount = currentTouchState[i].tapCount;
						}
						if (isPrimaryTouch)
						{
							if (newTouchState.isNoneEndedOrCanceled)
							{
								newTouchState.isPrimaryTouch = false;
								bool haveOngoingTouch = false;
								for (int j = 0; j < touchControlCount; j++)
								{
									if (j != i && currentTouchState[j].isInProgress)
									{
										haveOngoingTouch = true;
										break;
									}
								}
								if (!haveOngoingTouch)
								{
									if (isTap)
									{
										Touchscreen.TriggerTap(this.primaryTouch, ref newTouchState, eventPtr);
									}
									else
									{
										InputState.Change<TouchState>(this.primaryTouch, ref newTouchState, InputUpdateType.None, eventPtr);
									}
								}
								else
								{
									TouchState newPrimaryTouchState = newTouchState;
									newPrimaryTouchState.phase = TouchPhase.Moved;
									newPrimaryTouchState.isOrphanedPrimaryTouch = true;
									InputState.Change<TouchState>(this.primaryTouch, ref newPrimaryTouchState, InputUpdateType.None, eventPtr);
								}
							}
							else
							{
								InputState.Change<TouchState>(this.primaryTouch, ref newTouchState, InputUpdateType.None, eventPtr);
							}
						}
						else if (newTouchState.isNoneEndedOrCanceled && primaryTouchState->isOrphanedPrimaryTouch)
						{
							bool haveOngoingTouch2 = false;
							for (int k = 0; k < touchControlCount; k++)
							{
								if (k != i && currentTouchState[k].isInProgress)
								{
									haveOngoingTouch2 = true;
									break;
								}
							}
							if (!haveOngoingTouch2)
							{
								primaryTouchState->isOrphanedPrimaryTouch = false;
								InputState.Change<byte>(this.primaryTouch.phase, 3, InputUpdateType.None, default(InputEventPtr));
							}
						}
						if (isTap)
						{
							Touchscreen.TriggerTap(this.touches[i], ref newTouchState, eventPtr);
							return;
						}
						InputState.Change<TouchState>(this.touches[i], ref newTouchState, InputUpdateType.None, eventPtr);
						return;
					}
					else
					{
						i++;
					}
				}
				return;
			}
			int l = 0;
			while (l < touchControlCount)
			{
				if (currentTouchState->isNoneEndedOrCanceled)
				{
					newTouchState.delta = Vector2.zero;
					newTouchState.startTime = eventPtr.time;
					newTouchState.startPosition = newTouchState.position;
					newTouchState.isPrimaryTouch = false;
					newTouchState.isOrphanedPrimaryTouch = false;
					newTouchState.isTap = false;
					newTouchState.tapCount = currentTouchState->tapCount;
					if (primaryTouchState->isNoneEndedOrCanceled)
					{
						newTouchState.isPrimaryTouch = true;
						InputState.Change<TouchState>(this.primaryTouch, ref newTouchState, InputUpdateType.None, eventPtr);
					}
					InputState.Change<TouchState>(this.touches[l], ref newTouchState, InputUpdateType.None, eventPtr);
					return;
				}
				l++;
				currentTouchState++;
			}
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00034472 File Offset: 0x00032672
		void IInputStateCallbackReceiver.OnNextUpdate()
		{
			this.OnNextUpdate();
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0003447A File Offset: 0x0003267A
		void IInputStateCallbackReceiver.OnStateEvent(InputEventPtr eventPtr)
		{
			this.OnStateEvent(eventPtr);
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00034484 File Offset: 0x00032684
		unsafe bool IInputStateCallbackReceiver.GetStateOffsetForEvent(InputControl control, InputEventPtr eventPtr, ref uint offset)
		{
			if (!eventPtr.IsA<StateEvent>())
			{
				return false;
			}
			StateEvent* stateEventPtr = StateEvent.FromUnchecked(eventPtr);
			if (stateEventPtr->stateFormat != TouchState.Format)
			{
				return false;
			}
			if (control == null)
			{
				TouchState* currentTouchState = (TouchState*)((byte*)base.currentStatePtr + this.touches[0].stateBlock.byteOffset);
				TouchState* eventTouchState = (TouchState*)stateEventPtr->state;
				int eventTouchId = eventTouchState->touchId;
				TouchPhase eventTouchPhase = eventTouchState->phase;
				int touchControlCount = this.touches.Count;
				for (int i = 0; i < touchControlCount; i++)
				{
					TouchState* touch = currentTouchState + i;
					if (touch->touchId == eventTouchId || (!touch->isInProgress && eventTouchPhase.IsActive()))
					{
						offset = this.primaryTouch.m_StateBlock.byteOffset + this.primaryTouch.m_StateBlock.alignedSizeInBytes - this.m_StateBlock.byteOffset + (uint)(i * UnsafeUtility.SizeOf<TouchState>());
						return true;
					}
				}
				return false;
			}
			TouchControl touchControl = control.FindInParentChain<TouchControl>();
			if (touchControl == null || touchControl.parent != this)
			{
				return false;
			}
			if (touchControl != this.primaryTouch)
			{
				return false;
			}
			offset = touchControl.stateBlock.byteOffset - this.m_StateBlock.byteOffset;
			return true;
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x000345C4 File Offset: 0x000327C4
		unsafe void ICustomDeviceReset.Reset()
		{
			void* statePtr = base.currentStatePtr;
			using (NativeArray<byte> buffer = new NativeArray<byte>(StateEvent.GetEventSizeWithPayload<TouchState>(), Allocator.Temp, NativeArrayOptions.ClearMemory))
			{
				StateEvent* eventPtr = (StateEvent*)buffer.GetUnsafePtr<byte>();
				eventPtr->baseEvent = new InputEvent(1398030676, buffer.Length, base.deviceId, -1.0);
				TouchState* primaryTouchState = (TouchState*)((byte*)statePtr + this.primaryTouch.stateBlock.byteOffset);
				if (primaryTouchState->phase.IsActive())
				{
					UnsafeUtility.MemCpy(eventPtr->state, (void*)primaryTouchState, (long)UnsafeUtility.SizeOf<TouchState>());
					((TouchState*)eventPtr->state)->phase = TouchPhase.Canceled;
					InputState.Change<TouchPhase>(this.primaryTouch.phase, TouchPhase.Canceled, InputUpdateType.None, new InputEventPtr((InputEvent*)eventPtr));
				}
				TouchState* touchStates = (TouchState*)((byte*)statePtr + this.touches[0].stateBlock.byteOffset);
				int touchCount = this.touches.Count;
				for (int i = 0; i < touchCount; i++)
				{
					if (touchStates[i].phase.IsActive())
					{
						UnsafeUtility.MemCpy(eventPtr->state, (void*)(touchStates + i), (long)UnsafeUtility.SizeOf<TouchState>());
						((TouchState*)eventPtr->state)->phase = TouchPhase.Canceled;
						InputState.Change<TouchPhase>(this.touches[i].phase, TouchPhase.Canceled, InputUpdateType.None, new InputEventPtr((InputEvent*)eventPtr));
					}
				}
			}
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0003474C File Offset: 0x0003294C
		internal unsafe static bool MergeForward(InputEventPtr currentEventPtr, InputEventPtr nextEventPtr)
		{
			if (currentEventPtr.type != 1398030676 || nextEventPtr.type != 1398030676)
			{
				return false;
			}
			StateEvent* currentEvent = StateEvent.FromUnchecked(currentEventPtr);
			StateEvent* nextEvent = StateEvent.FromUnchecked(nextEventPtr);
			if (currentEvent->stateFormat != TouchState.Format || nextEvent->stateFormat != TouchState.Format)
			{
				return false;
			}
			TouchState* currentState = (TouchState*)currentEvent->state;
			TouchState* nextState = (TouchState*)nextEvent->state;
			if (currentState->touchId != nextState->touchId || currentState->phaseId != nextState->phaseId || currentState->flags != nextState->flags)
			{
				return false;
			}
			nextState->delta += currentState->delta;
			return true;
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x00034816 File Offset: 0x00032A16
		bool IEventMerger.MergeForward(InputEventPtr currentEventPtr, InputEventPtr nextEventPtr)
		{
			return Touchscreen.MergeForward(currentEventPtr, nextEventPtr);
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0003481F File Offset: 0x00032A1F
		private static void TriggerTap(TouchControl control, ref TouchState state, InputEventPtr eventPtr)
		{
			state.isTapPress = true;
			state.isTapRelease = false;
			InputState.Change<TouchState>(control, ref state, InputUpdateType.None, eventPtr);
			state.isTapPress = false;
			state.isTapRelease = true;
			InputState.Change<TouchState>(control, ref state, InputUpdateType.None, eventPtr);
			state.isTapRelease = false;
		}

		// Token: 0x04000441 RID: 1089
		private static readonly ProfilerMarker k_TouchscreenUpdateMarker = new ProfilerMarker("Touchscreen.OnNextUpdate");

		// Token: 0x04000442 RID: 1090
		private static readonly ProfilerMarker k_TouchAllocateMarker = new ProfilerMarker("TouchAllocate");

		// Token: 0x04000444 RID: 1092
		internal static float s_TapTime;

		// Token: 0x04000445 RID: 1093
		internal static float s_TapDelayTime;

		// Token: 0x04000446 RID: 1094
		internal static float s_TapRadiusSquared;
	}
}
