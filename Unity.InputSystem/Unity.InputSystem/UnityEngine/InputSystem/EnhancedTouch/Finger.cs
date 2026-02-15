using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.EnhancedTouch
{
	// Token: 0x0200014E RID: 334
	public class Finger
	{
		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000E94 RID: 3732 RVA: 0x0004A94B File Offset: 0x00048B4B
		public Touchscreen screen { get; }

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000E95 RID: 3733 RVA: 0x0004A953 File Offset: 0x00048B53
		public int index { get; }

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000E96 RID: 3734 RVA: 0x0004A95C File Offset: 0x00048B5C
		public bool isActive
		{
			get
			{
				return this.currentTouch.valid;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000E97 RID: 3735 RVA: 0x0004A978 File Offset: 0x00048B78
		public Vector2 screenPosition
		{
			get
			{
				Touch touch = this.lastTouch;
				if (!touch.valid)
				{
					return default(Vector2);
				}
				return touch.screenPosition;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x0004A9A8 File Offset: 0x00048BA8
		public Touch lastTouch
		{
			get
			{
				int count = this.m_StateHistory.Count;
				if (count == 0)
				{
					return default(Touch);
				}
				return new Touch(this, this.m_StateHistory[count - 1]);
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000E99 RID: 3737 RVA: 0x0004A9E4 File Offset: 0x00048BE4
		public Touch currentTouch
		{
			get
			{
				Touch touch = this.lastTouch;
				if (!touch.valid)
				{
					return default(Touch);
				}
				if (touch.isInProgress)
				{
					return touch;
				}
				if (touch.updateStepCount == InputUpdate.s_UpdateStepCount)
				{
					return touch;
				}
				return default(Touch);
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000E9A RID: 3738 RVA: 0x0004AA2F File Offset: 0x00048C2F
		public TouchHistory touchHistory
		{
			get
			{
				return new TouchHistory(this, this.m_StateHistory, -1, -1);
			}
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x0004AA40 File Offset: 0x00048C40
		internal unsafe Finger(Touchscreen screen, int index, InputUpdateType updateMask)
		{
			this.screen = screen;
			this.index = index;
			this.m_StateHistory = new InputStateHistory<TouchState>(screen.touches[index])
			{
				historyDepth = Touch.maxHistoryLengthPerFinger,
				extraMemoryPerRecord = UnsafeUtility.SizeOf<Touch.ExtraDataPerTouchState>(),
				onRecordAdded = new Action<InputStateHistory.Record>(this.OnTouchRecorded),
				onShouldRecordStateChange = new Func<InputControl, double, InputEventPtr, bool>(Finger.ShouldRecordTouch),
				updateMask = updateMask
			};
			this.m_StateHistory.StartRecording();
			if (screen.touches[index].isInProgress)
			{
				this.m_StateHistory.RecordStateChange(screen.touches[index], *screen.touches[index].value, -1.0);
			}
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x0004AB1C File Offset: 0x00048D1C
		private unsafe static bool ShouldRecordTouch(InputControl control, double time, InputEventPtr eventPtr)
		{
			if (!eventPtr.valid)
			{
				return false;
			}
			FourCC eventType = eventPtr.type;
			if (eventType != 1398030676 && eventType != 1145852993)
			{
				return false;
			}
			TouchState* currentTouchState = (TouchState*)((byte*)control.currentStatePtr + control.stateBlock.byteOffset);
			return !currentTouchState->isTapRelease;
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x0004AB84 File Offset: 0x00048D84
		private unsafe void OnTouchRecorded(InputStateHistory.Record record)
		{
			int recordIndex = record.recordIndex;
			InputStateHistory.RecordHeader* recordUnchecked = this.m_StateHistory.GetRecordUnchecked(recordIndex);
			TouchState* touchState = (TouchState*)recordUnchecked->statePtrWithoutControlIndex;
			touchState->updateStepCount = InputUpdate.s_UpdateStepCount;
			Touch.s_GlobalState.playerState.haveBuiltActiveTouches = false;
			Touch.ExtraDataPerTouchState* extraData = (Touch.ExtraDataPerTouchState*)(recordUnchecked + this.m_StateHistory.bytesPerRecord / sizeof(InputStateHistory.RecordHeader) - UnsafeUtility.SizeOf<Touch.ExtraDataPerTouchState>() / sizeof(InputStateHistory.RecordHeader));
			ref Touch.ExtraDataPerTouchState ptr = ref *extraData;
			uint num = Touch.s_GlobalState.playerState.lastId + 1U;
			Touch.s_GlobalState.playerState.lastId = num;
			ptr.uniqueId = num;
			extraData->accumulatedDelta = touchState->delta;
			if (touchState->phase != TouchPhase.Began)
			{
				if (recordIndex != this.m_StateHistory.m_HeadIndex)
				{
					int previousRecordIndex = ((recordIndex == 0) ? (this.m_StateHistory.historyDepth - 1) : (recordIndex - 1));
					TouchState* previousTouchState = (TouchState*)this.m_StateHistory.GetRecordUnchecked(previousRecordIndex)->statePtrWithoutControlIndex;
					touchState->delta -= previousTouchState->delta;
					touchState->beganInSameFrame = previousTouchState->beganInSameFrame && previousTouchState->updateStepCount == touchState->updateStepCount;
				}
			}
			else
			{
				touchState->beganInSameFrame = true;
			}
			switch (touchState->phase)
			{
			case TouchPhase.Began:
				DelegateHelpers.InvokeCallbacksSafe<Finger>(ref Touch.s_GlobalState.onFingerDown, this, "Touch.onFingerDown", null);
				return;
			case TouchPhase.Moved:
				DelegateHelpers.InvokeCallbacksSafe<Finger>(ref Touch.s_GlobalState.onFingerMove, this, "Touch.onFingerMove", null);
				return;
			case TouchPhase.Ended:
			case TouchPhase.Canceled:
				DelegateHelpers.InvokeCallbacksSafe<Finger>(ref Touch.s_GlobalState.onFingerUp, this, "Touch.onFingerUp", null);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x0004ACF4 File Offset: 0x00048EF4
		private unsafe Touch FindTouch(uint uniqueId)
		{
			foreach (InputStateHistory<TouchState>.Record record in this.m_StateHistory)
			{
				if (((Touch.ExtraDataPerTouchState*)record.GetUnsafeExtraMemoryPtrUnchecked())->uniqueId == uniqueId)
				{
					return new Touch(this, record);
				}
			}
			return default(Touch);
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x0004AD60 File Offset: 0x00048F60
		internal unsafe TouchHistory GetTouchHistory(Touch touch)
		{
			InputStateHistory<TouchState>.Record touchRecord = touch.m_TouchRecord;
			if (touchRecord.owner != this.m_StateHistory)
			{
				touch = this.FindTouch(touch.uniqueId);
				if (!touch.valid)
				{
					return default(TouchHistory);
				}
			}
			int touchId = touch.touchId;
			int startIndex = touch.m_TouchRecord.index;
			int count = 0;
			if (touch.phase != TouchPhase.Began)
			{
				InputStateHistory<TouchState>.Record previousRecord = touch.m_TouchRecord.previous;
				while (previousRecord.valid)
				{
					TouchState* touchState = (TouchState*)previousRecord.GetUnsafeMemoryPtr();
					if (touchState->touchId != touchId)
					{
						break;
					}
					count++;
					if (touchState->phase == TouchPhase.Began)
					{
						break;
					}
					previousRecord = previousRecord.previous;
				}
			}
			if (count == 0)
			{
				return default(TouchHistory);
			}
			startIndex--;
			return new TouchHistory(this, this.m_StateHistory, startIndex, count);
		}

		// Token: 0x0400085B RID: 2139
		internal readonly InputStateHistory<TouchState> m_StateHistory;
	}
}
