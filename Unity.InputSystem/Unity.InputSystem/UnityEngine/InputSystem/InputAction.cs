using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Serialization;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200001C RID: 28
	[Serializable]
	public sealed class InputAction : ICloneable, IDisposable
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000132 RID: 306 RVA: 0x000033B9 File Offset: 0x000015B9
		public string name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000133 RID: 307 RVA: 0x000033C1 File Offset: 0x000015C1
		public InputActionType type
		{
			get
			{
				return this.m_Type;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000134 RID: 308 RVA: 0x000033C9 File Offset: 0x000015C9
		public Guid id
		{
			get
			{
				this.MakeSureIdIsInPlace();
				return new Guid(this.m_Id);
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000135 RID: 309 RVA: 0x000033E0 File Offset: 0x000015E0
		internal Guid idDontGenerate
		{
			get
			{
				if (string.IsNullOrEmpty(this.m_Id))
				{
					return default(Guid);
				}
				return new Guid(this.m_Id);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000136 RID: 310 RVA: 0x0000340F File Offset: 0x0000160F
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00003417 File Offset: 0x00001617
		public string expectedControlType
		{
			get
			{
				return this.m_ExpectedControlType;
			}
			set
			{
				this.m_ExpectedControlType = value;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00003420 File Offset: 0x00001620
		public string processors
		{
			get
			{
				return this.m_Processors;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00003428 File Offset: 0x00001628
		public string interactions
		{
			get
			{
				return this.m_Interactions;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00003430 File Offset: 0x00001630
		public InputActionMap actionMap
		{
			get
			{
				if (!this.isSingletonAction)
				{
					return this.m_ActionMap;
				}
				return null;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00003442 File Offset: 0x00001642
		// (set) Token: 0x0600013C RID: 316 RVA: 0x0000344C File Offset: 0x0000164C
		public InputBinding? bindingMask
		{
			get
			{
				return this.m_BindingMask;
			}
			set
			{
				if (value == this.m_BindingMask)
				{
					return;
				}
				if (value != null)
				{
					InputBinding v = value.Value;
					v.action = this.name;
					value = new InputBinding?(v);
				}
				this.m_BindingMask = value;
				InputActionMap map = this.GetOrCreateActionMap();
				if (map.m_State != null)
				{
					map.LazyResolveBindings(true);
				}
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600013D RID: 317 RVA: 0x000034DB File Offset: 0x000016DB
		public ReadOnlyArray<InputBinding> bindings
		{
			get
			{
				return this.GetOrCreateActionMap().GetBindingsForSingleAction(this);
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600013E RID: 318 RVA: 0x000034E9 File Offset: 0x000016E9
		public ReadOnlyArray<InputControl> controls
		{
			get
			{
				InputActionMap orCreateActionMap = this.GetOrCreateActionMap();
				orCreateActionMap.ResolveBindingsIfNecessary();
				return orCreateActionMap.GetControlsForSingleAction(this);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00003500 File Offset: 0x00001700
		public InputActionPhase phase
		{
			get
			{
				return this.currentState.phase;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000140 RID: 320 RVA: 0x0000351B File Offset: 0x0000171B
		public bool inProgress
		{
			get
			{
				return this.phase.IsInProgress();
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00003528 File Offset: 0x00001728
		public bool enabled
		{
			get
			{
				return this.phase > InputActionPhase.Disabled;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000142 RID: 322 RVA: 0x00003533 File Offset: 0x00001733
		// (remove) Token: 0x06000143 RID: 323 RVA: 0x00003541 File Offset: 0x00001741
		public event Action<InputAction.CallbackContext> started
		{
			add
			{
				this.m_OnStarted.AddCallback(value);
			}
			remove
			{
				this.m_OnStarted.RemoveCallback(value);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000144 RID: 324 RVA: 0x0000354F File Offset: 0x0000174F
		// (remove) Token: 0x06000145 RID: 325 RVA: 0x0000355D File Offset: 0x0000175D
		public event Action<InputAction.CallbackContext> canceled
		{
			add
			{
				this.m_OnCanceled.AddCallback(value);
			}
			remove
			{
				this.m_OnCanceled.RemoveCallback(value);
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000146 RID: 326 RVA: 0x0000356B File Offset: 0x0000176B
		// (remove) Token: 0x06000147 RID: 327 RVA: 0x00003579 File Offset: 0x00001779
		public event Action<InputAction.CallbackContext> performed
		{
			add
			{
				this.m_OnPerformed.AddCallback(value);
			}
			remove
			{
				this.m_OnPerformed.RemoveCallback(value);
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00003587 File Offset: 0x00001787
		public bool triggered
		{
			get
			{
				return this.WasPerformedThisFrame();
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00003590 File Offset: 0x00001790
		public unsafe InputControl activeControl
		{
			get
			{
				InputActionState state = this.GetOrCreateActionMap().m_State;
				if (state != null)
				{
					int controlIndex = state.actionStates[this.m_ActionIndexInState].controlIndex;
					if (controlIndex != -1)
					{
						return state.controls[controlIndex];
					}
				}
				return null;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600014A RID: 330 RVA: 0x000035D8 File Offset: 0x000017D8
		public unsafe Type activeValueType
		{
			get
			{
				InputActionState state = this.GetOrCreateActionMap().m_State;
				if (state != null)
				{
					InputActionState.TriggerState* actionStatePtr = state.actionStates + this.m_ActionIndexInState;
					int controlIndex = actionStatePtr->controlIndex;
					if (controlIndex != -1)
					{
						return state.GetValueType(actionStatePtr->bindingIndex, controlIndex);
					}
				}
				return null;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00003625 File Offset: 0x00001825
		// (set) Token: 0x0600014C RID: 332 RVA: 0x0000363C File Offset: 0x0000183C
		public bool wantsInitialStateCheck
		{
			get
			{
				return this.type == InputActionType.Value || (this.m_Flags & InputAction.ActionFlags.WantsInitialStateCheck) > (InputAction.ActionFlags)0;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= InputAction.ActionFlags.WantsInitialStateCheck;
					return;
				}
				this.m_Flags &= ~InputAction.ActionFlags.WantsInitialStateCheck;
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00003660 File Offset: 0x00001860
		public InputAction()
		{
			this.m_Id = Guid.NewGuid().ToString();
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00003694 File Offset: 0x00001894
		public InputAction(string name = null, InputActionType type = InputActionType.Value, string binding = null, string interactions = null, string processors = null, string expectedControlType = null)
		{
			this.m_Name = name;
			this.m_Type = type;
			if (!string.IsNullOrEmpty(binding))
			{
				this.m_SingletonActionBindings = new InputBinding[]
				{
					new InputBinding
					{
						path = binding,
						interactions = interactions,
						processors = processors,
						action = this.m_Name,
						id = Guid.NewGuid()
					}
				};
				this.m_BindingsStartIndex = 0;
				this.m_BindingsCount = 1;
			}
			else
			{
				this.m_Interactions = interactions;
				this.m_Processors = processors;
			}
			this.m_ExpectedControlType = expectedControlType;
			this.m_Id = Guid.NewGuid().ToString();
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00003754 File Offset: 0x00001954
		public void Dispose()
		{
			InputActionMap actionMap = this.m_ActionMap;
			if (actionMap == null)
			{
				return;
			}
			InputActionState state = actionMap.m_State;
			if (state == null)
			{
				return;
			}
			state.Dispose();
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00003770 File Offset: 0x00001970
		public override string ToString()
		{
			string str;
			if (this.m_Name == null)
			{
				str = "<Unnamed>";
			}
			else if (this.m_ActionMap != null && !this.isSingletonAction && !string.IsNullOrEmpty(this.m_ActionMap.name))
			{
				str = this.m_ActionMap.name + "/" + this.m_Name;
			}
			else
			{
				str = this.m_Name;
			}
			ReadOnlyArray<InputControl> controls = this.controls;
			if (controls.Count > 0)
			{
				str += "[";
				bool isFirst = true;
				foreach (InputControl control in controls)
				{
					if (!isFirst)
					{
						str += ",";
					}
					str += control.path;
					isFirst = false;
				}
				str += "]";
			}
			return str;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000385C File Offset: 0x00001A5C
		public void Enable()
		{
			if (this.enabled)
			{
				return;
			}
			InputActionMap orCreateActionMap = this.GetOrCreateActionMap();
			orCreateActionMap.ResolveBindingsIfNecessary();
			orCreateActionMap.m_State.EnableSingleAction(this);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000387F File Offset: 0x00001A7F
		public void Disable()
		{
			if (!this.enabled)
			{
				return;
			}
			this.m_ActionMap.m_State.DisableSingleAction(this);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000389C File Offset: 0x00001A9C
		public InputAction Clone()
		{
			return new InputAction(this.m_Name, this.m_Type, null, null, null, null)
			{
				m_SingletonActionBindings = this.bindings.ToArray(),
				m_BindingsCount = this.m_BindingsCount,
				m_ExpectedControlType = this.m_ExpectedControlType,
				m_Interactions = this.m_Interactions,
				m_Processors = this.m_Processors,
				m_Flags = this.m_Flags
			};
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000390E File Offset: 0x00001B0E
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00003918 File Offset: 0x00001B18
		public unsafe TValue ReadValue<TValue>() where TValue : struct
		{
			InputActionState state = this.GetOrCreateActionMap().m_State;
			if (state == null)
			{
				return default(TValue);
			}
			InputActionState.TriggerState* actionStatePtr = state.actionStates + this.m_ActionIndexInState;
			if (!actionStatePtr->phase.IsInProgress())
			{
				return state.ApplyProcessors<TValue>(actionStatePtr->bindingIndex, default(TValue), null);
			}
			return state.ReadValue<TValue>(actionStatePtr->bindingIndex, actionStatePtr->controlIndex, false);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000398C File Offset: 0x00001B8C
		public unsafe object ReadValueAsObject()
		{
			InputActionState state = this.GetOrCreateActionMap().m_State;
			if (state == null)
			{
				return null;
			}
			InputActionState.TriggerState* actionStatePtr = state.actionStates + this.m_ActionIndexInState;
			if (actionStatePtr->phase.IsInProgress())
			{
				int controlIndex = actionStatePtr->controlIndex;
				if (controlIndex != -1)
				{
					return state.ReadValueAsObject(actionStatePtr->bindingIndex, controlIndex, false);
				}
			}
			return null;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000039EC File Offset: 0x00001BEC
		public unsafe float GetControlMagnitude()
		{
			InputActionState state = this.GetOrCreateActionMap().m_State;
			if (state != null)
			{
				InputActionState.TriggerState* actionStatePtr = state.actionStates + this.m_ActionIndexInState;
				if (actionStatePtr->haveMagnitude)
				{
					return actionStatePtr->magnitude;
				}
			}
			return 0f;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00003A33 File Offset: 0x00001C33
		public void Reset()
		{
			InputActionState state = this.GetOrCreateActionMap().m_State;
			if (state == null)
			{
				return;
			}
			state.ResetActionState(this.m_ActionIndexInState, this.enabled ? InputActionPhase.Waiting : InputActionPhase.Disabled, true);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00003A60 File Offset: 0x00001C60
		public unsafe bool IsPressed()
		{
			InputActionState state = this.GetOrCreateActionMap().m_State;
			return state != null && state.actionStates[this.m_ActionIndexInState].isPressed;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00003A9C File Offset: 0x00001C9C
		public unsafe bool IsInProgress()
		{
			InputActionState state = this.GetOrCreateActionMap().m_State;
			return state != null && state.actionStates[this.m_ActionIndexInState].phase.IsInProgress();
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00003ADC File Offset: 0x00001CDC
		public unsafe bool WasPressedThisFrame()
		{
			InputActionState state = this.GetOrCreateActionMap().m_State;
			if (state != null)
			{
				InputActionState.TriggerState* actionStatePtr = state.actionStates + this.m_ActionIndexInState;
				uint currentUpdateStep = InputUpdate.s_UpdateStepCount;
				return actionStatePtr->pressedInUpdate == currentUpdateStep && currentUpdateStep != 0U && actionStatePtr->frame == Time.frameCount;
			}
			return false;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00003B34 File Offset: 0x00001D34
		public unsafe bool WasReleasedThisFrame()
		{
			InputActionState state = this.GetOrCreateActionMap().m_State;
			if (state != null)
			{
				InputActionState.TriggerState* actionStatePtr = state.actionStates + this.m_ActionIndexInState;
				uint currentUpdateStep = InputUpdate.s_UpdateStepCount;
				return actionStatePtr->releasedInUpdate == currentUpdateStep && currentUpdateStep != 0U && actionStatePtr->frame == Time.frameCount;
			}
			return false;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00003B8C File Offset: 0x00001D8C
		public unsafe bool WasPerformedThisFrame()
		{
			InputActionState state = this.GetOrCreateActionMap().m_State;
			if (state != null)
			{
				InputActionState.TriggerState* actionStatePtr = state.actionStates + this.m_ActionIndexInState;
				uint currentUpdateStep = InputUpdate.s_UpdateStepCount;
				return actionStatePtr->lastPerformedInUpdate == currentUpdateStep && currentUpdateStep != 0U && actionStatePtr->frame == Time.frameCount;
			}
			return false;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00003BE4 File Offset: 0x00001DE4
		public unsafe bool WasCompletedThisFrame()
		{
			InputActionState state = this.GetOrCreateActionMap().m_State;
			if (state != null)
			{
				InputActionState.TriggerState* actionStatePtr = state.actionStates + this.m_ActionIndexInState;
				uint currentUpdateStep = InputUpdate.s_UpdateStepCount;
				return actionStatePtr->lastCompletedInUpdate == currentUpdateStep && currentUpdateStep != 0U && actionStatePtr->frame == Time.frameCount;
			}
			return false;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00003C3C File Offset: 0x00001E3C
		public unsafe float GetTimeoutCompletionPercentage()
		{
			InputActionState state = this.GetOrCreateActionMap().m_State;
			if (state == null)
			{
				return 0f;
			}
			ref InputActionState.TriggerState actionState = ref state.actionStates[this.m_ActionIndexInState];
			int interactionIndex = actionState.interactionIndex;
			if (interactionIndex == -1)
			{
				return (float)((actionState.phase == InputActionPhase.Performed) ? 1 : 0);
			}
			ref InputActionState.InteractionState interactionState = ref state.interactionStates[interactionIndex];
			InputActionPhase phase = interactionState.phase;
			if (phase != InputActionPhase.Started)
			{
				if (phase != InputActionPhase.Performed)
				{
					return 0f;
				}
				return 1f;
			}
			else
			{
				float timerCompletion = 0f;
				if (interactionState.isTimerRunning)
				{
					float duration = interactionState.timerDuration;
					double remainingTime = interactionState.timerStartTime + (double)duration - InputState.currentTime;
					if (remainingTime <= 0.0)
					{
						timerCompletion = 1f;
					}
					else
					{
						timerCompletion = (float)(((double)duration - remainingTime) / (double)duration);
					}
				}
				if (interactionState.totalTimeoutCompletionTimeRemaining > 0f)
				{
					return (interactionState.totalTimeoutCompletionDone + timerCompletion * interactionState.timerDuration) / (interactionState.totalTimeoutCompletionDone + interactionState.totalTimeoutCompletionTimeRemaining);
				}
				return timerCompletion;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00003D3F File Offset: 0x00001F3F
		internal bool isSingletonAction
		{
			get
			{
				return this.m_ActionMap == null || this.m_ActionMap.m_SingletonAction == this;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00003D5C File Offset: 0x00001F5C
		private unsafe InputActionState.TriggerState currentState
		{
			get
			{
				if (this.m_ActionIndexInState == -1)
				{
					return default(InputActionState.TriggerState);
				}
				return *this.m_ActionMap.m_State.FetchActionState(this);
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00003D92 File Offset: 0x00001F92
		internal string MakeSureIdIsInPlace()
		{
			if (string.IsNullOrEmpty(this.m_Id))
			{
				this.GenerateId();
			}
			return this.m_Id;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00003DB0 File Offset: 0x00001FB0
		internal void GenerateId()
		{
			this.m_Id = Guid.NewGuid().ToString();
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00003DD6 File Offset: 0x00001FD6
		internal InputActionMap GetOrCreateActionMap()
		{
			if (this.m_ActionMap == null)
			{
				this.CreateInternalActionMapForSingletonAction();
			}
			return this.m_ActionMap;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00003DEC File Offset: 0x00001FEC
		private void CreateInternalActionMapForSingletonAction()
		{
			this.m_ActionMap = new InputActionMap
			{
				m_Actions = new InputAction[] { this },
				m_SingletonAction = this,
				m_Bindings = this.m_SingletonActionBindings
			};
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00003E29 File Offset: 0x00002029
		internal void RequestInitialStateCheckOnEnabledAction()
		{
			this.GetOrCreateActionMap().m_State.SetInitialStateCheckPending(this.m_ActionIndexInState, true);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00003E44 File Offset: 0x00002044
		internal bool ActiveControlIsValid(InputControl control)
		{
			if (control == null)
			{
				return false;
			}
			InputDevice device = control.device;
			if (!device.added)
			{
				return false;
			}
			ReadOnlyArray<InputDevice>? deviceList = this.GetOrCreateActionMap().devices;
			return deviceList == null || deviceList.Value.ContainsReference(device);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00003E90 File Offset: 0x00002090
		internal InputBinding? FindEffectiveBindingMask()
		{
			if (this.m_BindingMask != null)
			{
				return this.m_BindingMask;
			}
			InputActionMap actionMap = this.m_ActionMap;
			if (actionMap != null && actionMap.m_BindingMask != null)
			{
				return this.m_ActionMap.m_BindingMask;
			}
			InputActionMap actionMap2 = this.m_ActionMap;
			if (actionMap2 == null)
			{
				return null;
			}
			InputActionAsset asset = actionMap2.m_Asset;
			if (asset == null)
			{
				return null;
			}
			return asset.m_BindingMask;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00003F04 File Offset: 0x00002104
		internal int BindingIndexOnActionToBindingIndexOnMap(int indexOfBindingOnAction)
		{
			InputBinding[] bindingsInMap = this.GetOrCreateActionMap().m_Bindings;
			int bindingCountInMap = bindingsInMap.LengthSafe<InputBinding>();
			string name = this.name;
			int currentBindingIndexOnAction = -1;
			for (int i = 0; i < bindingCountInMap; i++)
			{
				if (bindingsInMap[i].TriggersAction(this))
				{
					currentBindingIndexOnAction++;
					if (currentBindingIndexOnAction == indexOfBindingOnAction)
					{
						return i;
					}
				}
			}
			throw new ArgumentOutOfRangeException("indexOfBindingOnAction", string.Format("Binding index {0} is out of range for action '{1}' with {2} bindings", indexOfBindingOnAction, this, currentBindingIndexOnAction + 1));
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00003F78 File Offset: 0x00002178
		internal int BindingIndexOnMapToBindingIndexOnAction(int indexOfBindingOnMap)
		{
			InputBinding[] bindingsInMap = this.GetOrCreateActionMap().m_Bindings;
			string actionName = this.name;
			int bindingIndexOnAction = 0;
			for (int i = indexOfBindingOnMap - 1; i >= 0; i--)
			{
				ref InputBinding binding = ref bindingsInMap[i];
				if (string.Compare(binding.action, actionName, StringComparison.InvariantCultureIgnoreCase) == 0 || binding.action == this.m_Id)
				{
					bindingIndexOnAction++;
				}
			}
			return bindingIndexOnAction;
		}

		// Token: 0x04000085 RID: 133
		[Tooltip("Human readable name of the action. Must be unique within its action map (case is ignored). Can be changed without breaking references to the action.")]
		[SerializeField]
		internal string m_Name;

		// Token: 0x04000086 RID: 134
		[Tooltip("Determines how the action triggers.\n\nA Value action will start and perform when a control moves from its default value and then perform on every value change. It will cancel when controls go back to default value. Also, when enabled, a Value action will respond right away to a control's current value.\n\nA Button action will start when a button is pressed and perform when the press threshold (see 'Default Button Press Point' in settings) is reached. It will cancel when the button is going below the release threshold (see 'Button Release Threshold' in settings). Also, if a button is already pressed when the action is enabled, the button has to be released first.\n\nA Pass-Through action will not explicitly start and will never cancel. Instead, for every value change on any bound control, the action will perform.")]
		[SerializeField]
		internal InputActionType m_Type;

		// Token: 0x04000087 RID: 135
		[FormerlySerializedAs("m_ExpectedControlLayout")]
		[Tooltip("The type of control expected by the action (e.g. \"Button\" or \"Stick\"). This will limit the controls shown when setting up bindings in the UI and will also limit which controls can be bound interactively to the action.")]
		[SerializeField]
		internal string m_ExpectedControlType;

		// Token: 0x04000088 RID: 136
		[Tooltip("Unique ID of the action (GUID). Used to reference the action from bindings such that actions can be renamed without breaking references.")]
		[SerializeField]
		internal string m_Id;

		// Token: 0x04000089 RID: 137
		[SerializeField]
		internal string m_Processors;

		// Token: 0x0400008A RID: 138
		[SerializeField]
		internal string m_Interactions;

		// Token: 0x0400008B RID: 139
		[SerializeField]
		internal InputBinding[] m_SingletonActionBindings;

		// Token: 0x0400008C RID: 140
		[SerializeField]
		internal InputAction.ActionFlags m_Flags;

		// Token: 0x0400008D RID: 141
		[NonSerialized]
		internal InputBinding? m_BindingMask;

		// Token: 0x0400008E RID: 142
		[NonSerialized]
		internal int m_BindingsStartIndex;

		// Token: 0x0400008F RID: 143
		[NonSerialized]
		internal int m_BindingsCount;

		// Token: 0x04000090 RID: 144
		[NonSerialized]
		internal int m_ControlStartIndex;

		// Token: 0x04000091 RID: 145
		[NonSerialized]
		internal int m_ControlCount;

		// Token: 0x04000092 RID: 146
		[NonSerialized]
		internal int m_ActionIndexInState = -1;

		// Token: 0x04000093 RID: 147
		[NonSerialized]
		internal InputActionMap m_ActionMap;

		// Token: 0x04000094 RID: 148
		[NonSerialized]
		internal CallbackArray<Action<InputAction.CallbackContext>> m_OnStarted;

		// Token: 0x04000095 RID: 149
		[NonSerialized]
		internal CallbackArray<Action<InputAction.CallbackContext>> m_OnCanceled;

		// Token: 0x04000096 RID: 150
		[NonSerialized]
		internal CallbackArray<Action<InputAction.CallbackContext>> m_OnPerformed;

		// Token: 0x0200001D RID: 29
		[Flags]
		internal enum ActionFlags
		{
			// Token: 0x04000098 RID: 152
			WantsInitialStateCheck = 1
		}

		// Token: 0x0200001E RID: 30
		public struct CallbackContext
		{
			// Token: 0x17000097 RID: 151
			// (get) Token: 0x0600016B RID: 363 RVA: 0x00003FDA File Offset: 0x000021DA
			private int actionIndex
			{
				get
				{
					return this.m_ActionIndex;
				}
			}

			// Token: 0x17000098 RID: 152
			// (get) Token: 0x0600016C RID: 364 RVA: 0x00003FE2 File Offset: 0x000021E2
			private unsafe int bindingIndex
			{
				get
				{
					return this.m_State.actionStates[this.actionIndex].bindingIndex;
				}
			}

			// Token: 0x17000099 RID: 153
			// (get) Token: 0x0600016D RID: 365 RVA: 0x00004003 File Offset: 0x00002203
			private unsafe int controlIndex
			{
				get
				{
					return this.m_State.actionStates[this.actionIndex].controlIndex;
				}
			}

			// Token: 0x1700009A RID: 154
			// (get) Token: 0x0600016E RID: 366 RVA: 0x00004024 File Offset: 0x00002224
			private unsafe int interactionIndex
			{
				get
				{
					return this.m_State.actionStates[this.actionIndex].interactionIndex;
				}
			}

			// Token: 0x1700009B RID: 155
			// (get) Token: 0x0600016F RID: 367 RVA: 0x00004045 File Offset: 0x00002245
			public unsafe InputActionPhase phase
			{
				get
				{
					if (this.m_State == null)
					{
						return InputActionPhase.Disabled;
					}
					return this.m_State.actionStates[this.actionIndex].phase;
				}
			}

			// Token: 0x1700009C RID: 156
			// (get) Token: 0x06000170 RID: 368 RVA: 0x00004070 File Offset: 0x00002270
			public bool started
			{
				get
				{
					return this.phase == InputActionPhase.Started;
				}
			}

			// Token: 0x1700009D RID: 157
			// (get) Token: 0x06000171 RID: 369 RVA: 0x0000407B File Offset: 0x0000227B
			public bool performed
			{
				get
				{
					return this.phase == InputActionPhase.Performed;
				}
			}

			// Token: 0x1700009E RID: 158
			// (get) Token: 0x06000172 RID: 370 RVA: 0x00004086 File Offset: 0x00002286
			public bool canceled
			{
				get
				{
					return this.phase == InputActionPhase.Canceled;
				}
			}

			// Token: 0x1700009F RID: 159
			// (get) Token: 0x06000173 RID: 371 RVA: 0x00004091 File Offset: 0x00002291
			public InputAction action
			{
				get
				{
					InputActionState state = this.m_State;
					if (state == null)
					{
						return null;
					}
					return state.GetActionOrNull(this.bindingIndex);
				}
			}

			// Token: 0x170000A0 RID: 160
			// (get) Token: 0x06000174 RID: 372 RVA: 0x000040AA File Offset: 0x000022AA
			public InputControl control
			{
				get
				{
					InputActionState state = this.m_State;
					if (state == null)
					{
						return null;
					}
					return state.controls[this.controlIndex];
				}
			}

			// Token: 0x170000A1 RID: 161
			// (get) Token: 0x06000175 RID: 373 RVA: 0x000040C4 File Offset: 0x000022C4
			public IInputInteraction interaction
			{
				get
				{
					if (this.m_State == null)
					{
						return null;
					}
					int index = this.interactionIndex;
					if (index == -1)
					{
						return null;
					}
					return this.m_State.interactions[index];
				}
			}

			// Token: 0x170000A2 RID: 162
			// (get) Token: 0x06000176 RID: 374 RVA: 0x000040F5 File Offset: 0x000022F5
			public unsafe double time
			{
				get
				{
					if (this.m_State == null)
					{
						return 0.0;
					}
					return this.m_State.actionStates[this.actionIndex].time;
				}
			}

			// Token: 0x170000A3 RID: 163
			// (get) Token: 0x06000177 RID: 375 RVA: 0x00004128 File Offset: 0x00002328
			public unsafe double startTime
			{
				get
				{
					if (this.m_State == null)
					{
						return 0.0;
					}
					return this.m_State.actionStates[this.actionIndex].startTime;
				}
			}

			// Token: 0x170000A4 RID: 164
			// (get) Token: 0x06000178 RID: 376 RVA: 0x0000415B File Offset: 0x0000235B
			public double duration
			{
				get
				{
					return this.time - this.startTime;
				}
			}

			// Token: 0x170000A5 RID: 165
			// (get) Token: 0x06000179 RID: 377 RVA: 0x0000416A File Offset: 0x0000236A
			public Type valueType
			{
				get
				{
					InputActionState state = this.m_State;
					if (state == null)
					{
						return null;
					}
					return state.GetValueType(this.bindingIndex, this.controlIndex);
				}
			}

			// Token: 0x170000A6 RID: 166
			// (get) Token: 0x0600017A RID: 378 RVA: 0x00004189 File Offset: 0x00002389
			public int valueSizeInBytes
			{
				get
				{
					if (this.m_State == null)
					{
						return 0;
					}
					return this.m_State.GetValueSizeInBytes(this.bindingIndex, this.controlIndex);
				}
			}

			// Token: 0x0600017B RID: 379 RVA: 0x000041AC File Offset: 0x000023AC
			public unsafe void ReadValue(void* buffer, int bufferSize)
			{
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer");
				}
				if (this.m_State != null && this.phase.IsInProgress())
				{
					this.m_State.ReadValue(this.bindingIndex, this.controlIndex, buffer, bufferSize, false);
					return;
				}
				int valueSize = this.valueSizeInBytes;
				if (bufferSize < valueSize)
				{
					throw new ArgumentException(string.Format("Expected buffer of at least {0} bytes but got buffer of only {1} bytes", valueSize, bufferSize), "bufferSize");
				}
				UnsafeUtility.MemClear(buffer, (long)this.valueSizeInBytes);
			}

			// Token: 0x0600017C RID: 380 RVA: 0x00004234 File Offset: 0x00002434
			public TValue ReadValue<TValue>() where TValue : struct
			{
				TValue value = default(TValue);
				if (this.m_State != null)
				{
					value = (this.phase.IsInProgress() ? this.m_State.ReadValue<TValue>(this.bindingIndex, this.controlIndex, false) : this.m_State.ApplyProcessors<TValue>(this.bindingIndex, value, null));
				}
				return value;
			}

			// Token: 0x0600017D RID: 381 RVA: 0x00004290 File Offset: 0x00002490
			public bool ReadValueAsButton()
			{
				bool value = false;
				if (this.m_State != null && this.phase.IsInProgress())
				{
					value = this.m_State.ReadValueAsButton(this.bindingIndex, this.controlIndex);
				}
				return value;
			}

			// Token: 0x0600017E RID: 382 RVA: 0x000042CD File Offset: 0x000024CD
			public object ReadValueAsObject()
			{
				if (this.m_State != null && this.phase.IsInProgress())
				{
					return this.m_State.ReadValueAsObject(this.bindingIndex, this.controlIndex, false);
				}
				return null;
			}

			// Token: 0x0600017F RID: 383 RVA: 0x00004300 File Offset: 0x00002500
			public override string ToString()
			{
				return string.Format("{{ action={0} phase={1} time={2} control={3} value={4} interaction={5} }}", new object[]
				{
					this.action,
					this.phase,
					this.time,
					this.control,
					this.ReadValueAsObject(),
					this.interaction
				});
			}

			// Token: 0x04000099 RID: 153
			internal InputActionState m_State;

			// Token: 0x0400009A RID: 154
			internal int m_ActionIndex;
		}
	}
}
