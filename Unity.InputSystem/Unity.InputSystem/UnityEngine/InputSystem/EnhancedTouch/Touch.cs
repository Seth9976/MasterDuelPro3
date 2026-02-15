using System;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.EnhancedTouch
{
	// Token: 0x0200014F RID: 335
	public struct Touch : IEquatable<Touch>
	{
		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000EA0 RID: 3744 RVA: 0x0004AE2A File Offset: 0x0004902A
		public bool valid
		{
			get
			{
				return this.m_TouchRecord.valid;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000EA1 RID: 3745 RVA: 0x0004AE37 File Offset: 0x00049037
		public Finger finger
		{
			get
			{
				return this.m_Finger;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000EA2 RID: 3746 RVA: 0x0004AE3F File Offset: 0x0004903F
		public TouchPhase phase
		{
			get
			{
				return this.state.phase;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000EA3 RID: 3747 RVA: 0x0004AE4C File Offset: 0x0004904C
		public bool began
		{
			get
			{
				return this.phase == TouchPhase.Began;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000EA4 RID: 3748 RVA: 0x0004AE57 File Offset: 0x00049057
		public bool inProgress
		{
			get
			{
				return this.phase == TouchPhase.Moved || this.phase == TouchPhase.Stationary || this.phase == TouchPhase.Began;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000EA5 RID: 3749 RVA: 0x0004AE76 File Offset: 0x00049076
		public bool ended
		{
			get
			{
				return this.phase == TouchPhase.Ended || this.phase == TouchPhase.Canceled;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000EA6 RID: 3750 RVA: 0x0004AE8C File Offset: 0x0004908C
		public int touchId
		{
			get
			{
				return this.state.touchId;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000EA7 RID: 3751 RVA: 0x0004AE99 File Offset: 0x00049099
		public float pressure
		{
			get
			{
				return this.state.pressure;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000EA8 RID: 3752 RVA: 0x0004AEA6 File Offset: 0x000490A6
		public Vector2 radius
		{
			get
			{
				return this.state.radius;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000EA9 RID: 3753 RVA: 0x0004AEB3 File Offset: 0x000490B3
		public double startTime
		{
			get
			{
				return this.state.startTime;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000EAA RID: 3754 RVA: 0x0004AEC0 File Offset: 0x000490C0
		public double time
		{
			get
			{
				return this.m_TouchRecord.time;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000EAB RID: 3755 RVA: 0x0004AECD File Offset: 0x000490CD
		public Touchscreen screen
		{
			get
			{
				return this.finger.screen;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000EAC RID: 3756 RVA: 0x0004AEDA File Offset: 0x000490DA
		public Vector2 screenPosition
		{
			get
			{
				return this.state.position;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000EAD RID: 3757 RVA: 0x0004AEE7 File Offset: 0x000490E7
		public Vector2 startScreenPosition
		{
			get
			{
				return this.state.startPosition;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000EAE RID: 3758 RVA: 0x0004AEF4 File Offset: 0x000490F4
		public Vector2 delta
		{
			get
			{
				return this.state.delta;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000EAF RID: 3759 RVA: 0x0004AF01 File Offset: 0x00049101
		public int tapCount
		{
			get
			{
				return (int)this.state.tapCount;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x0004AF0E File Offset: 0x0004910E
		public bool isTap
		{
			get
			{
				return this.state.isTap;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000EB1 RID: 3761 RVA: 0x0004AF1B File Offset: 0x0004911B
		public int displayIndex
		{
			get
			{
				return (int)this.state.displayIndex;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000EB2 RID: 3762 RVA: 0x0004AF28 File Offset: 0x00049128
		public bool isInProgress
		{
			get
			{
				TouchPhase phase = this.phase;
				return phase - TouchPhase.Began <= 1 || phase == TouchPhase.Stationary;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000EB3 RID: 3763 RVA: 0x0004AF49 File Offset: 0x00049149
		internal uint updateStepCount
		{
			get
			{
				return this.state.updateStepCount;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000EB4 RID: 3764 RVA: 0x0004AF56 File Offset: 0x00049156
		internal uint uniqueId
		{
			get
			{
				return this.extraData.uniqueId;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000EB5 RID: 3765 RVA: 0x0004AF63 File Offset: 0x00049163
		private unsafe ref TouchState state
		{
			get
			{
				return ref *(TouchState*)this.m_TouchRecord.GetUnsafeMemoryPtr();
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x0004AF70 File Offset: 0x00049170
		private unsafe ref Touch.ExtraDataPerTouchState extraData
		{
			get
			{
				return ref *(Touch.ExtraDataPerTouchState*)this.m_TouchRecord.GetUnsafeExtraMemoryPtr();
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x0004AF7D File Offset: 0x0004917D
		public TouchHistory history
		{
			get
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("Touch is invalid");
				}
				return this.finger.GetTouchHistory(this);
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000EB8 RID: 3768 RVA: 0x0004AFA3 File Offset: 0x000491A3
		public static ReadOnlyArray<Touch> activeTouches
		{
			get
			{
				Touch.s_GlobalState.playerState.UpdateActiveTouches();
				return new ReadOnlyArray<Touch>(Touch.s_GlobalState.playerState.activeTouches, 0, Touch.s_GlobalState.playerState.activeTouchCount);
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x0004AFD8 File Offset: 0x000491D8
		public static ReadOnlyArray<Finger> fingers
		{
			get
			{
				return new ReadOnlyArray<Finger>(Touch.s_GlobalState.playerState.fingers, 0, Touch.s_GlobalState.playerState.totalFingerCount);
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x0004AFFE File Offset: 0x000491FE
		public static ReadOnlyArray<Finger> activeFingers
		{
			get
			{
				Touch.s_GlobalState.playerState.UpdateActiveFingers();
				return new ReadOnlyArray<Finger>(Touch.s_GlobalState.playerState.activeFingers, 0, Touch.s_GlobalState.playerState.activeFingerCount);
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000EBB RID: 3771 RVA: 0x0004B033 File Offset: 0x00049233
		public static IEnumerable<Touchscreen> screens
		{
			get
			{
				return Touch.s_GlobalState.touchscreens;
			}
		}

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06000EBC RID: 3772 RVA: 0x0004B044 File Offset: 0x00049244
		// (remove) Token: 0x06000EBD RID: 3773 RVA: 0x0004B064 File Offset: 0x00049264
		public static event Action<Finger> onFingerDown
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				Touch.s_GlobalState.onFingerDown.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				Touch.s_GlobalState.onFingerDown.RemoveCallback(value);
			}
		}

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06000EBE RID: 3774 RVA: 0x0004B084 File Offset: 0x00049284
		// (remove) Token: 0x06000EBF RID: 3775 RVA: 0x0004B0A4 File Offset: 0x000492A4
		public static event Action<Finger> onFingerUp
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				Touch.s_GlobalState.onFingerUp.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				Touch.s_GlobalState.onFingerUp.RemoveCallback(value);
			}
		}

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06000EC0 RID: 3776 RVA: 0x0004B0C4 File Offset: 0x000492C4
		// (remove) Token: 0x06000EC1 RID: 3777 RVA: 0x0004B0E4 File Offset: 0x000492E4
		public static event Action<Finger> onFingerMove
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				Touch.s_GlobalState.onFingerMove.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				Touch.s_GlobalState.onFingerMove.RemoveCallback(value);
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000EC2 RID: 3778 RVA: 0x0004B104 File Offset: 0x00049304
		public static int maxHistoryLengthPerFinger
		{
			get
			{
				return Touch.s_GlobalState.historyLengthPerFinger;
			}
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x0004B110 File Offset: 0x00049310
		internal Touch(Finger finger, InputStateHistory<TouchState>.Record touchRecord)
		{
			this.m_Finger = finger;
			this.m_TouchRecord = touchRecord;
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x0004B120 File Offset: 0x00049320
		public override string ToString()
		{
			if (!this.valid)
			{
				return "<None>";
			}
			return string.Format("{{id={0} finger={1} phase={2} position={3} delta={4} time={5}}}", new object[]
			{
				this.touchId,
				this.finger.index,
				this.phase,
				this.screenPosition,
				this.delta,
				this.time
			});
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x0004B1A4 File Offset: 0x000493A4
		public bool Equals(Touch other)
		{
			return object.Equals(this.m_Finger, other.m_Finger) && this.m_TouchRecord.Equals(other.m_TouchRecord);
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x0004B1CC File Offset: 0x000493CC
		public override bool Equals(object obj)
		{
			if (obj is Touch)
			{
				Touch other = (Touch)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x0004B1F1 File Offset: 0x000493F1
		public override int GetHashCode()
		{
			return (((this.m_Finger != null) ? this.m_Finger.GetHashCode() : 0) * 397) ^ this.m_TouchRecord.GetHashCode();
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x0004B221 File Offset: 0x00049421
		internal static void AddTouchscreen(Touchscreen screen)
		{
			Touch.s_GlobalState.touchscreens.AppendWithCapacity(screen, 5);
			Touch.s_GlobalState.playerState.AddFingers(screen);
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x0004B248 File Offset: 0x00049448
		internal static void RemoveTouchscreen(Touchscreen screen)
		{
			int index = Touch.s_GlobalState.touchscreens.IndexOfReference(screen);
			Touch.s_GlobalState.touchscreens.RemoveAtWithCapacity(index);
			Touch.s_GlobalState.playerState.RemoveFingers(screen);
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x0004B286 File Offset: 0x00049486
		internal static void BeginUpdate()
		{
			if (Touch.s_GlobalState.playerState.haveActiveTouchesNeedingRefreshNextUpdate)
			{
				Touch.s_GlobalState.playerState.haveBuiltActiveTouches = false;
			}
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x0004B2AC File Offset: 0x000494AC
		private static Touch.GlobalState CreateGlobalState()
		{
			return new Touch.GlobalState
			{
				historyLengthPerFinger = 64
			};
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x0004B2CC File Offset: 0x000494CC
		internal static ISavedState SaveAndResetState()
		{
			ISavedState savedState = new SavedStructState<Touch.GlobalState>(ref Touch.s_GlobalState, delegate(ref Touch.GlobalState state)
			{
				Touch.s_GlobalState = state;
			}, delegate
			{
			});
			Touch.s_GlobalState = Touch.CreateGlobalState();
			return savedState;
		}

		// Token: 0x0400085C RID: 2140
		private readonly Finger m_Finger;

		// Token: 0x0400085D RID: 2141
		internal InputStateHistory<TouchState>.Record m_TouchRecord;

		// Token: 0x0400085E RID: 2142
		internal static Touch.GlobalState s_GlobalState = Touch.CreateGlobalState();

		// Token: 0x02000150 RID: 336
		internal struct GlobalState
		{
			// Token: 0x0400085F RID: 2143
			internal InlinedArray<Touchscreen> touchscreens;

			// Token: 0x04000860 RID: 2144
			internal int historyLengthPerFinger;

			// Token: 0x04000861 RID: 2145
			internal CallbackArray<Action<Finger>> onFingerDown;

			// Token: 0x04000862 RID: 2146
			internal CallbackArray<Action<Finger>> onFingerMove;

			// Token: 0x04000863 RID: 2147
			internal CallbackArray<Action<Finger>> onFingerUp;

			// Token: 0x04000864 RID: 2148
			internal Touch.FingerAndTouchState playerState;
		}

		// Token: 0x02000151 RID: 337
		internal struct FingerAndTouchState
		{
			// Token: 0x06000ECE RID: 3790 RVA: 0x0004B338 File Offset: 0x00049538
			public void AddFingers(Touchscreen screen)
			{
				int touchCount = screen.touches.Count;
				ArrayHelpers.EnsureCapacity<Finger>(ref this.fingers, this.totalFingerCount, touchCount, 10);
				for (int i = 0; i < touchCount; i++)
				{
					Finger finger = new Finger(screen, i, this.updateMask);
					ArrayHelpers.AppendWithCapacity<Finger>(ref this.fingers, ref this.totalFingerCount, finger, 10);
				}
			}

			// Token: 0x06000ECF RID: 3791 RVA: 0x0004B398 File Offset: 0x00049598
			public void RemoveFingers(Touchscreen screen)
			{
				int touchCount = screen.touches.Count;
				for (int i = 0; i < this.fingers.Length; i++)
				{
					if (this.fingers[i].screen == screen)
					{
						for (int j = 0; j < touchCount; j++)
						{
							this.fingers[i + j].m_StateHistory.Dispose();
						}
						ArrayHelpers.EraseSliceWithCapacity<Finger>(ref this.fingers, ref this.totalFingerCount, i, touchCount);
						break;
					}
				}
				this.haveBuiltActiveTouches = false;
			}

			// Token: 0x06000ED0 RID: 3792 RVA: 0x0004B414 File Offset: 0x00049614
			public void Destroy()
			{
				for (int i = 0; i < this.totalFingerCount; i++)
				{
					this.fingers[i].m_StateHistory.Dispose();
				}
				InputStateHistory<TouchState> inputStateHistory = this.activeTouchState;
				if (inputStateHistory != null)
				{
					inputStateHistory.Dispose();
				}
				this.activeTouchState = null;
			}

			// Token: 0x06000ED1 RID: 3793 RVA: 0x0004B45C File Offset: 0x0004965C
			public void UpdateActiveFingers()
			{
				this.activeFingerCount = 0;
				for (int i = 0; i < this.totalFingerCount; i++)
				{
					Finger finger = this.fingers[i];
					if (finger.currentTouch.valid)
					{
						ArrayHelpers.AppendWithCapacity<Finger>(ref this.activeFingers, ref this.activeFingerCount, finger, 10);
					}
				}
			}

			// Token: 0x06000ED2 RID: 3794 RVA: 0x0004B4B0 File Offset: 0x000496B0
			public unsafe void UpdateActiveTouches()
			{
				if (this.haveBuiltActiveTouches)
				{
					return;
				}
				if (this.activeTouchState == null)
				{
					this.activeTouchState = new InputStateHistory<TouchState>(null)
					{
						extraMemoryPerRecord = UnsafeUtility.SizeOf<Touch.ExtraDataPerTouchState>()
					};
				}
				else
				{
					this.activeTouchState.Clear();
					this.activeTouchState.m_ControlCount = 0;
					this.activeTouchState.m_Controls.Clear<InputControl>();
				}
				this.activeTouchCount = 0;
				this.haveActiveTouchesNeedingRefreshNextUpdate = false;
				uint currentUpdateStepCount = InputUpdate.s_UpdateStepCount;
				for (int i = 0; i < this.totalFingerCount; i++)
				{
					ref Finger finger = ref this.fingers[i];
					InputStateHistory<TouchState> history = finger.m_StateHistory;
					int touchRecordCount = history.Count;
					if (touchRecordCount != 0)
					{
						int insertAt = this.activeTouchCount;
						int currentTouchId = 0;
						TouchState* currentTouchState = default(TouchState*);
						int touchRecordIndex = history.UserIndexToRecordIndex(touchRecordCount - 1);
						InputStateHistory.RecordHeader* touchRecordHeader = history.GetRecordUnchecked(touchRecordIndex);
						int touchRecordSize = history.bytesPerRecord;
						int extraMemoryOffset = touchRecordSize - history.extraMemoryPerRecord;
						for (int j = 0; j < touchRecordCount; j++)
						{
							if (j != 0)
							{
								touchRecordIndex--;
								if (touchRecordIndex < 0)
								{
									touchRecordIndex = history.historyDepth - 1;
									touchRecordHeader = history.GetRecordUnchecked(touchRecordIndex);
								}
								else
								{
									touchRecordHeader -= touchRecordSize / sizeof(InputStateHistory.RecordHeader);
								}
							}
							TouchState* touchState = (TouchState*)touchRecordHeader->statePtrWithoutControlIndex;
							bool wasUpdatedThisFrame = touchState->updateStepCount == currentUpdateStepCount;
							if (touchState->touchId == currentTouchId && !touchState->phase.IsEndedOrCanceled())
							{
								if (wasUpdatedThisFrame && touchState->phase == TouchPhase.Began)
								{
									currentTouchState->phase = TouchPhase.Began;
									currentTouchState->position = touchState->position;
									currentTouchState->delta = default(Vector2);
									this.haveActiveTouchesNeedingRefreshNextUpdate = true;
								}
							}
							else
							{
								if (touchState->phase.IsEndedOrCanceled() && (!touchState->beganInSameFrame || touchState->updateStepCount != currentUpdateStepCount - 1U) && !wasUpdatedThisFrame)
								{
									break;
								}
								Touch.ExtraDataPerTouchState* touchExtraState = (Touch.ExtraDataPerTouchState*)(touchRecordHeader + extraMemoryOffset / sizeof(InputStateHistory.RecordHeader));
								int newRecordIndex;
								InputStateHistory.RecordHeader* newRecordHeader = this.activeTouchState.AllocateRecord(out newRecordIndex);
								TouchState* newRecordState = (TouchState*)newRecordHeader->statePtrWithControlIndex;
								Touch.ExtraDataPerTouchState* newRecordExtraState = (Touch.ExtraDataPerTouchState*)(newRecordHeader + this.activeTouchState.bytesPerRecord / sizeof(InputStateHistory.RecordHeader) - UnsafeUtility.SizeOf<Touch.ExtraDataPerTouchState>() / sizeof(InputStateHistory.RecordHeader));
								newRecordHeader->time = touchRecordHeader->time;
								newRecordHeader->controlIndex = ArrayHelpers.AppendWithCapacity<InputControl>(ref this.activeTouchState.m_Controls, ref this.activeTouchState.m_ControlCount, finger.m_StateHistory.controls[0], 10);
								UnsafeUtility.MemCpy((void*)newRecordState, (void*)touchState, (long)UnsafeUtility.SizeOf<TouchState>());
								UnsafeUtility.MemCpy((void*)newRecordExtraState, (void*)touchExtraState, (long)UnsafeUtility.SizeOf<Touch.ExtraDataPerTouchState>());
								TouchPhase phase = touchState->phase;
								if ((phase == TouchPhase.Moved || phase == TouchPhase.Began) && !wasUpdatedThisFrame && (phase != TouchPhase.Moved || !touchState->beganInSameFrame || touchState->updateStepCount != currentUpdateStepCount - 1U))
								{
									newRecordState->phase = TouchPhase.Stationary;
									newRecordState->delta = default(Vector2);
								}
								else if (!wasUpdatedThisFrame && !touchState->beganInSameFrame)
								{
									newRecordState->delta = default(Vector2);
								}
								else
								{
									newRecordState->delta = newRecordExtraState->accumulatedDelta;
								}
								InputStateHistory<TouchState>.Record newRecord = new InputStateHistory<TouchState>.Record(this.activeTouchState, newRecordIndex, newRecordHeader);
								Touch newTouch = new Touch(finger, newRecord);
								ArrayHelpers.InsertAtWithCapacity<Touch>(ref this.activeTouches, ref this.activeTouchCount, insertAt, newTouch, 10);
								currentTouchId = touchState->touchId;
								currentTouchState = newRecordState;
								if (newTouch.phase != TouchPhase.Stationary)
								{
									this.haveActiveTouchesNeedingRefreshNextUpdate = true;
								}
							}
						}
					}
				}
				this.haveBuiltActiveTouches = true;
			}

			// Token: 0x04000865 RID: 2149
			public InputUpdateType updateMask;

			// Token: 0x04000866 RID: 2150
			public Finger[] fingers;

			// Token: 0x04000867 RID: 2151
			public Finger[] activeFingers;

			// Token: 0x04000868 RID: 2152
			public Touch[] activeTouches;

			// Token: 0x04000869 RID: 2153
			public int activeFingerCount;

			// Token: 0x0400086A RID: 2154
			public int activeTouchCount;

			// Token: 0x0400086B RID: 2155
			public int totalFingerCount;

			// Token: 0x0400086C RID: 2156
			public uint lastId;

			// Token: 0x0400086D RID: 2157
			public bool haveBuiltActiveTouches;

			// Token: 0x0400086E RID: 2158
			public bool haveActiveTouchesNeedingRefreshNextUpdate;

			// Token: 0x0400086F RID: 2159
			public InputStateHistory<TouchState> activeTouchState;
		}

		// Token: 0x02000152 RID: 338
		internal struct ExtraDataPerTouchState
		{
			// Token: 0x04000870 RID: 2160
			public Vector2 accumulatedDelta;

			// Token: 0x04000871 RID: 2161
			public uint uniqueId;
		}
	}
}
