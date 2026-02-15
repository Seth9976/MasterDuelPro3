using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200022E RID: 558
	public sealed class InputActionTrace : IEnumerable<InputActionTrace.ActionEventPtr>, IEnumerable, IDisposable
	{
		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x0600145C RID: 5212 RVA: 0x0005CF8D File Offset: 0x0005B18D
		public InputEventBuffer buffer
		{
			get
			{
				return this.m_EventBuffer;
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x0600145D RID: 5213 RVA: 0x0005CF95 File Offset: 0x0005B195
		public int count
		{
			get
			{
				return this.m_EventBuffer.eventCount;
			}
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x000020E2 File Offset: 0x000002E2
		public InputActionTrace()
		{
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x0005CFA2 File Offset: 0x0005B1A2
		public InputActionTrace(InputAction action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			this.SubscribeTo(action);
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x0005CFBF File Offset: 0x0005B1BF
		public InputActionTrace(InputActionMap actionMap)
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			this.SubscribeTo(actionMap);
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x0005CFDC File Offset: 0x0005B1DC
		public void SubscribeToAll()
		{
			if (this.m_SubscribedToAll)
			{
				return;
			}
			this.HookOnActionChange();
			this.m_SubscribedToAll = true;
			while (this.m_SubscribedActions.length > 0)
			{
				this.UnsubscribeFrom(this.m_SubscribedActions[this.m_SubscribedActions.length - 1]);
			}
			while (this.m_SubscribedActionMaps.length > 0)
			{
				this.UnsubscribeFrom(this.m_SubscribedActionMaps[this.m_SubscribedActionMaps.length - 1]);
			}
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x0005D05C File Offset: 0x0005B25C
		public void UnsubscribeFromAll()
		{
			if (this.count == 0)
			{
				this.UnhookOnActionChange();
			}
			this.m_SubscribedToAll = false;
			while (this.m_SubscribedActions.length > 0)
			{
				this.UnsubscribeFrom(this.m_SubscribedActions[this.m_SubscribedActions.length - 1]);
			}
			while (this.m_SubscribedActionMaps.length > 0)
			{
				this.UnsubscribeFrom(this.m_SubscribedActionMaps[this.m_SubscribedActionMaps.length - 1]);
			}
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x0005D0DC File Offset: 0x0005B2DC
		public void SubscribeTo(InputAction action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (this.m_CallbackDelegate == null)
			{
				this.m_CallbackDelegate = new Action<InputAction.CallbackContext>(this.RecordAction);
			}
			action.performed += this.m_CallbackDelegate;
			action.started += this.m_CallbackDelegate;
			action.canceled += this.m_CallbackDelegate;
			this.m_SubscribedActions.AppendWithCapacity(action, 10);
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x0005D144 File Offset: 0x0005B344
		public void SubscribeTo(InputActionMap actionMap)
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			if (this.m_CallbackDelegate == null)
			{
				this.m_CallbackDelegate = new Action<InputAction.CallbackContext>(this.RecordAction);
			}
			actionMap.actionTriggered += this.m_CallbackDelegate;
			this.m_SubscribedActionMaps.AppendWithCapacity(actionMap, 10);
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x0005D194 File Offset: 0x0005B394
		public void UnsubscribeFrom(InputAction action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (this.m_CallbackDelegate == null)
			{
				return;
			}
			action.performed -= this.m_CallbackDelegate;
			action.started -= this.m_CallbackDelegate;
			action.canceled -= this.m_CallbackDelegate;
			int index = this.m_SubscribedActions.IndexOfReference(action);
			if (index != -1)
			{
				this.m_SubscribedActions.RemoveAtWithCapacity(index);
			}
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x0005D1FC File Offset: 0x0005B3FC
		public void UnsubscribeFrom(InputActionMap actionMap)
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			if (this.m_CallbackDelegate == null)
			{
				return;
			}
			actionMap.actionTriggered -= this.m_CallbackDelegate;
			int index = this.m_SubscribedActionMaps.IndexOfReference(actionMap);
			if (index != -1)
			{
				this.m_SubscribedActionMaps.RemoveAtWithCapacity(index);
			}
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x0005D24C File Offset: 0x0005B44C
		public unsafe void RecordAction(InputAction.CallbackContext context)
		{
			int stateIndex = this.m_ActionMapStates.IndexOfReference(context.m_State);
			if (stateIndex == -1)
			{
				stateIndex = this.m_ActionMapStates.AppendWithCapacity(context.m_State, 10);
			}
			this.HookOnActionChange();
			int valueSizeInBytes = context.valueSizeInBytes;
			ActionEvent* eventPtr = (ActionEvent*)this.m_EventBuffer.AllocateEvent(ActionEvent.GetEventSizeWithValueSize(valueSizeInBytes), 2048, Allocator.Persistent);
			ref InputActionState.TriggerState triggerState = ref context.m_State.actionStates[context.m_ActionIndex];
			eventPtr->baseEvent.type = ActionEvent.Type;
			eventPtr->baseEvent.time = triggerState.time;
			eventPtr->stateIndex = stateIndex;
			eventPtr->controlIndex = triggerState.controlIndex;
			eventPtr->bindingIndex = triggerState.bindingIndex;
			eventPtr->interactionIndex = triggerState.interactionIndex;
			eventPtr->startTime = triggerState.startTime;
			eventPtr->phase = triggerState.phase;
			byte* valueBuffer = eventPtr->valueData;
			context.ReadValue((void*)valueBuffer, valueSizeInBytes);
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x0005D33A File Offset: 0x0005B53A
		public void Clear()
		{
			this.m_EventBuffer.Reset();
			this.m_ActionMapStates.ClearWithCapacity();
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x0005D354 File Offset: 0x0005B554
		~InputActionTrace()
		{
			this.DisposeInternal();
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x0005D380 File Offset: 0x0005B580
		public override string ToString()
		{
			if (this.count == 0)
			{
				return "[]";
			}
			StringBuilder str = new StringBuilder();
			str.Append('[');
			bool isFirst = true;
			foreach (InputActionTrace.ActionEventPtr eventPtr in this)
			{
				if (!isFirst)
				{
					str.Append(",\n");
				}
				str.Append(eventPtr.ToString());
				isFirst = false;
			}
			str.Append(']');
			return str.ToString();
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x0005D414 File Offset: 0x0005B614
		public void Dispose()
		{
			this.UnsubscribeFromAll();
			this.DisposeInternal();
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x0005D424 File Offset: 0x0005B624
		private void DisposeInternal()
		{
			for (int i = 0; i < this.m_ActionMapStateClones.length; i++)
			{
				this.m_ActionMapStateClones[i].Dispose();
			}
			this.m_EventBuffer.Dispose();
			this.m_ActionMapStates.Clear();
			this.m_ActionMapStateClones.Clear();
			if (this.m_ActionChangeDelegate != null)
			{
				InputSystem.onActionChange -= this.m_ActionChangeDelegate;
				this.m_ActionChangeDelegate = null;
			}
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x0005D493 File Offset: 0x0005B693
		public IEnumerator<InputActionTrace.ActionEventPtr> GetEnumerator()
		{
			return new InputActionTrace.Enumerator(this);
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x0005D4A0 File Offset: 0x0005B6A0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x0005D4A8 File Offset: 0x0005B6A8
		private void HookOnActionChange()
		{
			if (this.m_OnActionChangeHooked)
			{
				return;
			}
			if (this.m_ActionChangeDelegate == null)
			{
				this.m_ActionChangeDelegate = new Action<object, InputActionChange>(this.OnActionChange);
			}
			InputSystem.onActionChange += this.m_ActionChangeDelegate;
			this.m_OnActionChangeHooked = true;
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x0005D4DF File Offset: 0x0005B6DF
		private void UnhookOnActionChange()
		{
			if (!this.m_OnActionChangeHooked)
			{
				return;
			}
			InputSystem.onActionChange -= this.m_ActionChangeDelegate;
			this.m_OnActionChangeHooked = false;
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x0005D4FC File Offset: 0x0005B6FC
		private void OnActionChange(object actionOrMapOrAsset, InputActionChange change)
		{
			if (this.m_SubscribedToAll && change - InputActionChange.ActionStarted <= 2)
			{
				InputAction inputAction = (InputAction)actionOrMapOrAsset;
				int actionIndex = inputAction.m_ActionIndexInState;
				InputActionState stateForAction = inputAction.m_ActionMap.m_State;
				InputAction.CallbackContext context = new InputAction.CallbackContext
				{
					m_State = stateForAction,
					m_ActionIndex = actionIndex
				};
				this.RecordAction(context);
				return;
			}
			if (change != InputActionChange.BoundControlsAboutToChange)
			{
				return;
			}
			InputAction action = actionOrMapOrAsset as InputAction;
			if (action != null)
			{
				this.CloneActionStateBeforeBindingsChange(action.m_ActionMap);
				return;
			}
			InputActionMap actionMap = actionOrMapOrAsset as InputActionMap;
			if (actionMap != null)
			{
				this.CloneActionStateBeforeBindingsChange(actionMap);
				return;
			}
			InputActionAsset actionAsset = actionOrMapOrAsset as InputActionAsset;
			if (actionAsset != null)
			{
				foreach (InputActionMap actionMapInAsset in actionAsset.actionMaps)
				{
					this.CloneActionStateBeforeBindingsChange(actionMapInAsset);
				}
			}
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x0005D5E0 File Offset: 0x0005B7E0
		private void CloneActionStateBeforeBindingsChange(InputActionMap actionMap)
		{
			InputActionState state = actionMap.m_State;
			if (state == null)
			{
				return;
			}
			int stateIndex = this.m_ActionMapStates.IndexOfReference(state);
			if (stateIndex == -1)
			{
				return;
			}
			InputActionState clone = state.Clone();
			this.m_ActionMapStateClones.Append(clone);
			this.m_ActionMapStates[stateIndex] = clone;
		}

		// Token: 0x04000C2D RID: 3117
		private bool m_SubscribedToAll;

		// Token: 0x04000C2E RID: 3118
		private bool m_OnActionChangeHooked;

		// Token: 0x04000C2F RID: 3119
		private InlinedArray<InputAction> m_SubscribedActions;

		// Token: 0x04000C30 RID: 3120
		private InlinedArray<InputActionMap> m_SubscribedActionMaps;

		// Token: 0x04000C31 RID: 3121
		private InputEventBuffer m_EventBuffer;

		// Token: 0x04000C32 RID: 3122
		private InlinedArray<InputActionState> m_ActionMapStates;

		// Token: 0x04000C33 RID: 3123
		private InlinedArray<InputActionState> m_ActionMapStateClones;

		// Token: 0x04000C34 RID: 3124
		private Action<InputAction.CallbackContext> m_CallbackDelegate;

		// Token: 0x04000C35 RID: 3125
		private Action<object, InputActionChange> m_ActionChangeDelegate;

		// Token: 0x0200022F RID: 559
		public struct ActionEventPtr
		{
			// Token: 0x170005D4 RID: 1492
			// (get) Token: 0x06001473 RID: 5235 RVA: 0x0005D62B File Offset: 0x0005B82B
			public unsafe InputAction action
			{
				get
				{
					return this.m_State.GetActionOrNull(this.m_Ptr->bindingIndex);
				}
			}

			// Token: 0x170005D5 RID: 1493
			// (get) Token: 0x06001474 RID: 5236 RVA: 0x0005D643 File Offset: 0x0005B843
			public unsafe InputActionPhase phase
			{
				get
				{
					return this.m_Ptr->phase;
				}
			}

			// Token: 0x170005D6 RID: 1494
			// (get) Token: 0x06001475 RID: 5237 RVA: 0x0005D650 File Offset: 0x0005B850
			public unsafe InputControl control
			{
				get
				{
					return this.m_State.controls[this.m_Ptr->controlIndex];
				}
			}

			// Token: 0x170005D7 RID: 1495
			// (get) Token: 0x06001476 RID: 5238 RVA: 0x0005D66C File Offset: 0x0005B86C
			public unsafe IInputInteraction interaction
			{
				get
				{
					int index = this.m_Ptr->interactionIndex;
					if (index == -1)
					{
						return null;
					}
					return this.m_State.interactions[index];
				}
			}

			// Token: 0x170005D8 RID: 1496
			// (get) Token: 0x06001477 RID: 5239 RVA: 0x0005D698 File Offset: 0x0005B898
			public unsafe double time
			{
				get
				{
					return this.m_Ptr->baseEvent.time;
				}
			}

			// Token: 0x170005D9 RID: 1497
			// (get) Token: 0x06001478 RID: 5240 RVA: 0x0005D6AA File Offset: 0x0005B8AA
			public unsafe double startTime
			{
				get
				{
					return this.m_Ptr->startTime;
				}
			}

			// Token: 0x170005DA RID: 1498
			// (get) Token: 0x06001479 RID: 5241 RVA: 0x0005D6B7 File Offset: 0x0005B8B7
			public double duration
			{
				get
				{
					return this.time - this.startTime;
				}
			}

			// Token: 0x170005DB RID: 1499
			// (get) Token: 0x0600147A RID: 5242 RVA: 0x0005D6C6 File Offset: 0x0005B8C6
			public unsafe int valueSizeInBytes
			{
				get
				{
					return this.m_Ptr->valueSizeInBytes;
				}
			}

			// Token: 0x0600147B RID: 5243 RVA: 0x0005D6D4 File Offset: 0x0005B8D4
			public unsafe object ReadValueAsObject()
			{
				if (this.m_Ptr == null)
				{
					throw new InvalidOperationException("ActionEventPtr is invalid");
				}
				byte* valuePtr = this.m_Ptr->valueData;
				int bindingIndex = this.m_Ptr->bindingIndex;
				if (!this.m_State.bindingStates[bindingIndex].isPartOfComposite)
				{
					int valueSizeInBytes = this.m_Ptr->valueSizeInBytes;
					return this.control.ReadValueFromBufferAsObject((void*)valuePtr, valueSizeInBytes);
				}
				int compositeBindingIndex = this.m_State.bindingStates[bindingIndex].compositeOrCompositeBindingIndex;
				int compositeIndex = this.m_State.bindingStates[compositeBindingIndex].compositeOrCompositeBindingIndex;
				InputBindingComposite composite = this.m_State.composites[compositeIndex];
				Type valueType = composite.valueType;
				if (valueType == null)
				{
					throw new InvalidOperationException(string.Format("Cannot read value from Composite '{0}' which does not have a valueType set", composite));
				}
				return Marshal.PtrToStructure(new IntPtr((void*)valuePtr), valueType);
			}

			// Token: 0x0600147C RID: 5244 RVA: 0x0005D7C0 File Offset: 0x0005B9C0
			public unsafe void ReadValue(void* buffer, int bufferSize)
			{
				int valueSizeInBytes = this.m_Ptr->valueSizeInBytes;
				if (bufferSize < valueSizeInBytes)
				{
					throw new ArgumentException(string.Format("Expected buffer of at least {0} bytes but got buffer of just {1} bytes instead", valueSizeInBytes, bufferSize), "bufferSize");
				}
				UnsafeUtility.MemCpy(buffer, (void*)this.m_Ptr->valueData, (long)valueSizeInBytes);
			}

			// Token: 0x0600147D RID: 5245 RVA: 0x0005D814 File Offset: 0x0005BA14
			public unsafe TValue ReadValue<TValue>() where TValue : struct
			{
				int valueSizeInBytes = this.m_Ptr->valueSizeInBytes;
				if (UnsafeUtility.SizeOf<TValue>() != valueSizeInBytes)
				{
					throw new InvalidOperationException(string.Format("Cannot read a value of type '{0}' with size {1} from event on action '{2}' with value size {3}", new object[]
					{
						typeof(TValue).Name,
						UnsafeUtility.SizeOf<TValue>(),
						this.action,
						valueSizeInBytes
					}));
				}
				TValue result = new TValue();
				UnsafeUtility.MemCpy(UnsafeUtility.AddressOf<TValue>(ref result), (void*)this.m_Ptr->valueData, (long)valueSizeInBytes);
				return result;
			}

			// Token: 0x0600147E RID: 5246 RVA: 0x0005D89C File Offset: 0x0005BA9C
			public override string ToString()
			{
				if (this.m_Ptr == null)
				{
					return "<null>";
				}
				string actionName = ((this.action.actionMap != null) ? (this.action.actionMap.name + "/" + this.action.name) : this.action.name);
				return string.Format("{{ action={0} phase={1} time={2} control={3} value={4} interaction={5} duration={6} }}", new object[]
				{
					actionName,
					this.phase,
					this.time,
					this.control,
					this.ReadValueAsObject(),
					this.interaction,
					this.duration
				});
			}

			// Token: 0x04000C36 RID: 3126
			internal InputActionState m_State;

			// Token: 0x04000C37 RID: 3127
			internal unsafe ActionEvent* m_Ptr;
		}

		// Token: 0x02000230 RID: 560
		private struct Enumerator : IEnumerator<InputActionTrace.ActionEventPtr>, IEnumerator, IDisposable
		{
			// Token: 0x0600147F RID: 5247 RVA: 0x0005D954 File Offset: 0x0005BB54
			public unsafe Enumerator(InputActionTrace trace)
			{
				this.m_Trace = trace;
				this.m_Buffer = (ActionEvent*)trace.m_EventBuffer.bufferPtr.data;
				this.m_EventCount = trace.m_EventBuffer.eventCount;
				this.m_CurrentEvent = null;
				this.m_CurrentIndex = 0;
			}

			// Token: 0x06001480 RID: 5248 RVA: 0x0005D9A4 File Offset: 0x0005BBA4
			public unsafe bool MoveNext()
			{
				if (this.m_CurrentIndex == this.m_EventCount)
				{
					return false;
				}
				if (this.m_CurrentEvent == null)
				{
					this.m_CurrentEvent = this.m_Buffer;
					return this.m_CurrentEvent != null;
				}
				this.m_CurrentIndex++;
				if (this.m_CurrentIndex == this.m_EventCount)
				{
					return false;
				}
				this.m_CurrentEvent = (ActionEvent*)InputEvent.GetNextInMemory((InputEvent*)this.m_CurrentEvent);
				return true;
			}

			// Token: 0x06001481 RID: 5249 RVA: 0x0005DA15 File Offset: 0x0005BC15
			public void Reset()
			{
				this.m_CurrentEvent = null;
				this.m_CurrentIndex = 0;
			}

			// Token: 0x06001482 RID: 5250 RVA: 0x000049FE File Offset: 0x00002BFE
			public void Dispose()
			{
			}

			// Token: 0x170005DC RID: 1500
			// (get) Token: 0x06001483 RID: 5251 RVA: 0x0005DA28 File Offset: 0x0005BC28
			public unsafe InputActionTrace.ActionEventPtr Current
			{
				get
				{
					InputActionState state = this.m_Trace.m_ActionMapStates[this.m_CurrentEvent->stateIndex];
					return new InputActionTrace.ActionEventPtr
					{
						m_State = state,
						m_Ptr = this.m_CurrentEvent
					};
				}
			}

			// Token: 0x170005DD RID: 1501
			// (get) Token: 0x06001484 RID: 5252 RVA: 0x0005DA6F File Offset: 0x0005BC6F
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000C38 RID: 3128
			private readonly InputActionTrace m_Trace;

			// Token: 0x04000C39 RID: 3129
			private unsafe readonly ActionEvent* m_Buffer;

			// Token: 0x04000C3A RID: 3130
			private readonly int m_EventCount;

			// Token: 0x04000C3B RID: 3131
			private unsafe ActionEvent* m_CurrentEvent;

			// Token: 0x04000C3C RID: 3132
			private int m_CurrentIndex;
		}
	}
}
