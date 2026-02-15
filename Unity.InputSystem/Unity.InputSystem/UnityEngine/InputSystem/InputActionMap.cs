using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000026 RID: 38
	[Serializable]
	public sealed class InputActionMap : ICloneable, ISerializationCallbackReceiver, IInputActionCollection2, IInputActionCollection, IEnumerable<InputAction>, IEnumerable, IDisposable
	{
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00004DE7 File Offset: 0x00002FE7
		public string name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00004DEF File Offset: 0x00002FEF
		public InputActionAsset asset
		{
			get
			{
				return this.m_Asset;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00004DF7 File Offset: 0x00002FF7
		public Guid id
		{
			get
			{
				if (string.IsNullOrEmpty(this.m_Id))
				{
					this.GenerateId();
				}
				return new Guid(this.m_Id);
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00004E18 File Offset: 0x00003018
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

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00004E47 File Offset: 0x00003047
		public bool enabled
		{
			get
			{
				return this.m_EnabledActionsCount > 0;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00004E52 File Offset: 0x00003052
		public ReadOnlyArray<InputAction> actions
		{
			get
			{
				return new ReadOnlyArray<InputAction>(this.m_Actions);
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00004E5F File Offset: 0x0000305F
		public ReadOnlyArray<InputBinding> bindings
		{
			get
			{
				return new ReadOnlyArray<InputBinding>(this.m_Bindings);
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00004E6C File Offset: 0x0000306C
		IEnumerable<InputBinding> IInputActionCollection2.bindings
		{
			get
			{
				return this.bindings;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00004E7C File Offset: 0x0000307C
		public ReadOnlyArray<InputControlScheme> controlSchemes
		{
			get
			{
				if (this.m_Asset == null)
				{
					return default(ReadOnlyArray<InputControlScheme>);
				}
				return this.m_Asset.controlSchemes;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00004EAC File Offset: 0x000030AC
		// (set) Token: 0x060001BA RID: 442 RVA: 0x00004EB4 File Offset: 0x000030B4
		public InputBinding? bindingMask
		{
			get
			{
				return this.m_BindingMask;
			}
			set
			{
				if (this.m_BindingMask == value)
				{
					return;
				}
				this.m_BindingMask = value;
				this.LazyResolveBindings(true);
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00004F10 File Offset: 0x00003110
		// (set) Token: 0x060001BC RID: 444 RVA: 0x00004F4D File Offset: 0x0000314D
		public ReadOnlyArray<InputDevice>? devices
		{
			get
			{
				ReadOnlyArray<InputDevice>? readOnlyArray = this.m_Devices.Get();
				if (readOnlyArray != null)
				{
					return readOnlyArray;
				}
				InputActionAsset asset = this.m_Asset;
				if (asset == null)
				{
					return null;
				}
				return asset.devices;
			}
			set
			{
				if (this.m_Devices.Set(value))
				{
					this.LazyResolveBindings(false);
				}
			}
		}

		// Token: 0x170000BD RID: 189
		public InputAction this[string actionNameOrId]
		{
			get
			{
				if (actionNameOrId == null)
				{
					throw new ArgumentNullException("actionNameOrId");
				}
				InputAction inputAction = this.FindAction(actionNameOrId, false);
				if (inputAction == null)
				{
					throw new KeyNotFoundException("Cannot find action '" + actionNameOrId + "'");
				}
				return inputAction;
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060001BE RID: 446 RVA: 0x00004F96 File Offset: 0x00003196
		// (remove) Token: 0x060001BF RID: 447 RVA: 0x00004FA4 File Offset: 0x000031A4
		public event Action<InputAction.CallbackContext> actionTriggered
		{
			add
			{
				this.m_ActionCallbacks.AddCallback(value);
			}
			remove
			{
				this.m_ActionCallbacks.RemoveCallback(value);
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00004FB2 File Offset: 0x000031B2
		public InputActionMap()
		{
			InputActionMap.s_NeedToResolveBindings = true;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00004FC7 File Offset: 0x000031C7
		public InputActionMap(string name)
			: this()
		{
			this.m_Name = name;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00004FD6 File Offset: 0x000031D6
		public void Dispose()
		{
			InputActionState state = this.m_State;
			if (state == null)
			{
				return;
			}
			state.Dispose();
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00004FE8 File Offset: 0x000031E8
		internal int FindActionIndex(string nameOrId)
		{
			if (string.IsNullOrEmpty(nameOrId))
			{
				return -1;
			}
			if (this.m_Actions == null)
			{
				return -1;
			}
			this.SetUpActionLookupTable();
			int actionCount = this.m_Actions.Length;
			if (nameOrId.StartsWith("{") && nameOrId.EndsWith("}"))
			{
				int length = nameOrId.Length - 2;
				for (int i = 0; i < actionCount; i++)
				{
					if (string.Compare(this.m_Actions[i].m_Id, 0, nameOrId, 1, length) == 0)
					{
						return i;
					}
				}
			}
			int actionIndex;
			if (this.m_ActionIndexByNameOrId.TryGetValue(nameOrId, out actionIndex))
			{
				return actionIndex;
			}
			for (int j = 0; j < actionCount; j++)
			{
				if (this.m_Actions[j].m_Id == nameOrId || string.Compare(this.m_Actions[j].m_Name, nameOrId, StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					return j;
				}
			}
			return -1;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000050B8 File Offset: 0x000032B8
		private void SetUpActionLookupTable()
		{
			if (this.m_ActionIndexByNameOrId != null || this.m_Actions == null)
			{
				return;
			}
			this.m_ActionIndexByNameOrId = new Dictionary<string, int>();
			int actionCount = this.m_Actions.Length;
			for (int i = 0; i < actionCount; i++)
			{
				InputAction action = this.m_Actions[i];
				action.MakeSureIdIsInPlace();
				this.m_ActionIndexByNameOrId[action.name] = i;
				this.m_ActionIndexByNameOrId[action.m_Id] = i;
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000512A File Offset: 0x0000332A
		internal void ClearActionLookupTable()
		{
			Dictionary<string, int> actionIndexByNameOrId = this.m_ActionIndexByNameOrId;
			if (actionIndexByNameOrId == null)
			{
				return;
			}
			actionIndexByNameOrId.Clear();
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000513C File Offset: 0x0000333C
		private int FindActionIndex(Guid id)
		{
			if (this.m_Actions == null)
			{
				return -1;
			}
			int actionCount = this.m_Actions.Length;
			for (int i = 0; i < actionCount; i++)
			{
				if (this.m_Actions[i].idDontGenerate == id)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00005180 File Offset: 0x00003380
		public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
		{
			if (actionNameOrId == null)
			{
				throw new ArgumentNullException("actionNameOrId");
			}
			int index = this.FindActionIndex(actionNameOrId);
			if (index != -1)
			{
				return this.m_Actions[index];
			}
			if (throwIfNotFound)
			{
				throw new ArgumentException(string.Format("No action '{0}' in '{1}'", actionNameOrId, this), "actionNameOrId");
			}
			return null;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000051CC File Offset: 0x000033CC
		public InputAction FindAction(Guid id)
		{
			int index = this.FindActionIndex(id);
			if (index == -1)
			{
				return null;
			}
			return this.m_Actions[index];
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x000051F0 File Offset: 0x000033F0
		public bool IsUsableWithDevice(InputDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (this.m_Bindings == null)
			{
				return false;
			}
			foreach (InputBinding binding in this.m_Bindings)
			{
				string path = binding.effectivePath;
				if (!string.IsNullOrEmpty(path) && InputControlPath.Matches(path, device))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000524E File Offset: 0x0000344E
		public void Enable()
		{
			if (this.m_Actions == null || this.m_EnabledActionsCount == this.m_Actions.Length)
			{
				return;
			}
			this.ResolveBindingsIfNecessary();
			this.m_State.EnableAllActions(this);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000527C File Offset: 0x0000347C
		public void Disable()
		{
			if (!this.enabled)
			{
				return;
			}
			this.m_State.DisableAllActions(this);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00005294 File Offset: 0x00003494
		public InputActionMap Clone()
		{
			InputActionMap clone = new InputActionMap
			{
				m_Name = this.m_Name
			};
			if (this.m_Actions != null)
			{
				int actionCount = this.m_Actions.Length;
				InputAction[] actions = new InputAction[actionCount];
				for (int i = 0; i < actionCount; i++)
				{
					InputAction original = this.m_Actions[i];
					actions[i] = new InputAction
					{
						m_Name = original.m_Name,
						m_ActionMap = clone,
						m_Type = original.m_Type,
						m_Interactions = original.m_Interactions,
						m_Processors = original.m_Processors,
						m_ExpectedControlType = original.m_ExpectedControlType,
						m_Flags = original.m_Flags
					};
				}
				clone.m_Actions = actions;
			}
			if (this.m_Bindings != null)
			{
				int bindingCount = this.m_Bindings.Length;
				InputBinding[] bindings = new InputBinding[bindingCount];
				Array.Copy(this.m_Bindings, 0, bindings, 0, bindingCount);
				for (int j = 0; j < bindingCount; j++)
				{
					bindings[j].m_Id = null;
				}
				clone.m_Bindings = bindings;
			}
			return clone;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000539D File Offset: 0x0000359D
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000053A5 File Offset: 0x000035A5
		public bool Contains(InputAction action)
		{
			return action != null && action.actionMap == this;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x000053B5 File Offset: 0x000035B5
		public override string ToString()
		{
			if (this.m_Asset != null)
			{
				return string.Format("{0}:{1}", this.m_Asset, this.m_Name);
			}
			if (!string.IsNullOrEmpty(this.m_Name))
			{
				return this.m_Name;
			}
			return "<Unnamed Action Map>";
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x000053F8 File Offset: 0x000035F8
		public IEnumerator<InputAction> GetEnumerator()
		{
			return this.actions.GetEnumerator();
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00005418 File Offset: 0x00003618
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00005420 File Offset: 0x00003620
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x0000542D File Offset: 0x0000362D
		private bool needToResolveBindings
		{
			get
			{
				return (this.m_Flags & InputActionMap.Flags.NeedToResolveBindings) > (InputActionMap.Flags)0;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= InputActionMap.Flags.NeedToResolveBindings;
					return;
				}
				this.m_Flags &= ~InputActionMap.Flags.NeedToResolveBindings;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00005450 File Offset: 0x00003650
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x0000545D File Offset: 0x0000365D
		private bool bindingResolutionNeedsFullReResolve
		{
			get
			{
				return (this.m_Flags & InputActionMap.Flags.BindingResolutionNeedsFullReResolve) > (InputActionMap.Flags)0;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= InputActionMap.Flags.BindingResolutionNeedsFullReResolve;
					return;
				}
				this.m_Flags &= ~InputActionMap.Flags.BindingResolutionNeedsFullReResolve;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00005480 File Offset: 0x00003680
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x0000548D File Offset: 0x0000368D
		private bool controlsForEachActionInitialized
		{
			get
			{
				return (this.m_Flags & InputActionMap.Flags.ControlsForEachActionInitialized) > (InputActionMap.Flags)0;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= InputActionMap.Flags.ControlsForEachActionInitialized;
					return;
				}
				this.m_Flags &= ~InputActionMap.Flags.ControlsForEachActionInitialized;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x000054B0 File Offset: 0x000036B0
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x000054BD File Offset: 0x000036BD
		private bool bindingsForEachActionInitialized
		{
			get
			{
				return (this.m_Flags & InputActionMap.Flags.BindingsForEachActionInitialized) > (InputActionMap.Flags)0;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= InputActionMap.Flags.BindingsForEachActionInitialized;
					return;
				}
				this.m_Flags &= ~InputActionMap.Flags.BindingsForEachActionInitialized;
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x000054E0 File Offset: 0x000036E0
		internal ReadOnlyArray<InputBinding> GetBindingsForSingleAction(InputAction action)
		{
			if (!this.bindingsForEachActionInitialized)
			{
				this.SetUpPerActionControlAndBindingArrays();
			}
			return new ReadOnlyArray<InputBinding>(this.m_BindingsForEachAction, action.m_BindingsStartIndex, action.m_BindingsCount);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00005507 File Offset: 0x00003707
		internal ReadOnlyArray<InputControl> GetControlsForSingleAction(InputAction action)
		{
			if (!this.controlsForEachActionInitialized)
			{
				this.SetUpPerActionControlAndBindingArrays();
			}
			return new ReadOnlyArray<InputControl>(this.m_ControlsForEachAction, action.m_ControlStartIndex, action.m_ControlCount);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00005530 File Offset: 0x00003730
		private unsafe void SetUpPerActionControlAndBindingArrays()
		{
			if (this.m_Bindings == null)
			{
				this.m_ControlsForEachAction = null;
				this.m_BindingsForEachAction = null;
				this.controlsForEachActionInitialized = true;
				this.bindingsForEachActionInitialized = true;
				return;
			}
			if (this.m_SingletonAction != null)
			{
				this.m_BindingsForEachAction = this.m_Bindings;
				InputActionState state = this.m_State;
				this.m_ControlsForEachAction = ((state != null) ? state.controls : null);
				this.m_SingletonAction.m_BindingsStartIndex = 0;
				this.m_SingletonAction.m_BindingsCount = this.m_Bindings.Length;
				this.m_SingletonAction.m_ControlStartIndex = 0;
				InputAction singletonAction = this.m_SingletonAction;
				InputActionState state2 = this.m_State;
				singletonAction.m_ControlCount = ((state2 != null) ? state2.totalControlCount : 0);
				if (this.m_ControlsForEachAction.HaveDuplicateReferences(0, this.m_SingletonAction.m_ControlCount))
				{
					int numControls = 0;
					InputControl[] controls = new InputControl[this.m_SingletonAction.m_ControlCount];
					for (int i = 0; i < this.m_SingletonAction.m_ControlCount; i++)
					{
						if (!controls.ContainsReference(this.m_ControlsForEachAction[i]))
						{
							controls[numControls] = this.m_ControlsForEachAction[i];
							numControls++;
						}
					}
					this.m_ControlsForEachAction = controls;
					this.m_SingletonAction.m_ControlCount = numControls;
				}
			}
			else
			{
				InputActionState state3 = this.m_State;
				InputActionState.ActionMapIndices mapIndices = ((state3 != null) ? state3.FetchMapIndices(this) : default(InputActionState.ActionMapIndices));
				for (int j = 0; j < this.m_Actions.Length; j++)
				{
					InputAction inputAction = this.m_Actions[j];
					inputAction.m_BindingsCount = 0;
					inputAction.m_BindingsStartIndex = -1;
					inputAction.m_ControlCount = 0;
					inputAction.m_ControlStartIndex = -1;
				}
				int bindingCount = this.m_Bindings.Length;
				for (int k = 0; k < bindingCount; k++)
				{
					InputAction action = this.FindAction(this.m_Bindings[k].action, false);
					if (action != null)
					{
						action.m_BindingsCount++;
					}
				}
				int newBindingsArrayIndex = 0;
				if (this.m_State != null && (this.m_ControlsForEachAction == null || this.m_ControlsForEachAction.Length != mapIndices.controlCount))
				{
					if (mapIndices.controlCount == 0)
					{
						this.m_ControlsForEachAction = null;
					}
					else
					{
						this.m_ControlsForEachAction = new InputControl[mapIndices.controlCount];
					}
				}
				InputBinding[] newBindingsArray = null;
				int currentControlIndex = 0;
				int currentBindingIndex = 0;
				while (currentBindingIndex < this.m_Bindings.Length)
				{
					InputAction currentAction = this.FindAction(this.m_Bindings[currentBindingIndex].action, false);
					if (currentAction == null || currentAction.m_BindingsStartIndex != -1)
					{
						currentBindingIndex++;
					}
					else
					{
						currentAction.m_BindingsStartIndex = ((newBindingsArray != null) ? newBindingsArrayIndex : currentBindingIndex);
						currentAction.m_ControlStartIndex = currentControlIndex;
						int bindingCountForCurrentAction = currentAction.m_BindingsCount;
						int sourceBindingToCopy = currentBindingIndex;
						for (int l = 0; l < bindingCountForCurrentAction; l++)
						{
							if (this.FindAction(this.m_Bindings[sourceBindingToCopy].action, false) != currentAction)
							{
								if (newBindingsArray == null)
								{
									newBindingsArray = new InputBinding[this.m_Bindings.Length];
									newBindingsArrayIndex = sourceBindingToCopy;
									Array.Copy(this.m_Bindings, 0, newBindingsArray, 0, sourceBindingToCopy);
								}
								do
								{
									sourceBindingToCopy++;
								}
								while (this.FindAction(this.m_Bindings[sourceBindingToCopy].action, false) != currentAction);
							}
							else if (currentBindingIndex == sourceBindingToCopy)
							{
								currentBindingIndex++;
							}
							if (newBindingsArray != null)
							{
								newBindingsArray[newBindingsArrayIndex++] = this.m_Bindings[sourceBindingToCopy];
							}
							if (this.m_State != null && !this.m_Bindings[sourceBindingToCopy].isComposite)
							{
								ref InputActionState.BindingState bindingState = ref this.m_State.bindingStates[mapIndices.bindingStartIndex + sourceBindingToCopy];
								int controlCountForBinding = bindingState.controlCount;
								if (controlCountForBinding > 0)
								{
									int controlStartIndexForBinding = bindingState.controlStartIndex;
									for (int m = 0; m < controlCountForBinding; m++)
									{
										InputControl control = this.m_State.controls[controlStartIndexForBinding + m];
										if (!this.m_ControlsForEachAction.ContainsReference(currentAction.m_ControlStartIndex, currentAction.m_ControlCount, control))
										{
											this.m_ControlsForEachAction[currentControlIndex] = control;
											currentControlIndex++;
											currentAction.m_ControlCount++;
										}
									}
								}
							}
							sourceBindingToCopy++;
						}
					}
				}
				if (newBindingsArray == null)
				{
					this.m_BindingsForEachAction = this.m_Bindings;
				}
				else
				{
					this.m_BindingsForEachAction = newBindingsArray;
				}
			}
			this.controlsForEachActionInitialized = true;
			this.bindingsForEachActionInitialized = true;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00005948 File Offset: 0x00003B48
		internal void OnWantToChangeSetup()
		{
			if (this.asset != null)
			{
				using (ReadOnlyArray<InputActionMap>.Enumerator enumerator = this.asset.actionMaps.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.enabled)
						{
							throw new InvalidOperationException(string.Format("Cannot add, remove, or change elements of InputActionAsset {0} while one or more of its actions are enabled", this.asset));
						}
					}
					return;
				}
			}
			if (this.enabled)
			{
				throw new InvalidOperationException(string.Format("Cannot add, remove, or change elements of InputActionMap {0} while one or more of its actions are enabled", this));
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000059E0 File Offset: 0x00003BE0
		internal void OnSetupChanged()
		{
			if (this.m_Asset != null)
			{
				this.m_Asset.MarkAsDirty();
				using (ReadOnlyArray<InputActionMap>.Enumerator enumerator = this.m_Asset.actionMaps.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						InputActionMap inputActionMap = enumerator.Current;
						inputActionMap.m_State = null;
					}
					goto IL_005C;
				}
			}
			this.m_State = null;
			IL_005C:
			this.ClearCachedActionData(false);
			this.LazyResolveBindings(true);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00005A68 File Offset: 0x00003C68
		internal void OnBindingModified()
		{
			this.ClearCachedActionData(false);
			this.LazyResolveBindings(true);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00005A79 File Offset: 0x00003C79
		internal void ClearCachedActionData(bool onlyControls = false)
		{
			if (!onlyControls)
			{
				this.bindingsForEachActionInitialized = false;
				this.m_BindingsForEachAction = null;
				this.m_ActionIndexByNameOrId = null;
			}
			this.controlsForEachActionInitialized = false;
			this.m_ControlsForEachAction = null;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00005AA4 File Offset: 0x00003CA4
		internal void GenerateId()
		{
			this.m_Id = Guid.NewGuid().ToString();
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00005ACC File Offset: 0x00003CCC
		internal bool LazyResolveBindings(bool fullResolve)
		{
			this.m_ControlsForEachAction = null;
			this.controlsForEachActionInitialized = false;
			InputActionMap.s_NeedToResolveBindings = true;
			if (this.m_State == null)
			{
				return false;
			}
			this.needToResolveBindings = true;
			this.bindingResolutionNeedsFullReResolve = this.bindingResolutionNeedsFullReResolve || fullResolve;
			if (InputActionMap.s_DeferBindingResolution > 0)
			{
				return false;
			}
			this.ResolveBindings();
			return true;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00005B1D File Offset: 0x00003D1D
		internal bool ResolveBindingsIfNecessary()
		{
			if (this.m_State != null && !this.needToResolveBindings)
			{
				return false;
			}
			if (this.m_State != null && this.m_State.isProcessingControlStateChange)
			{
				return false;
			}
			this.ResolveBindings();
			return true;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00005B50 File Offset: 0x00003D50
		internal void ResolveBindings()
		{
			using (InputActionRebindingExtensions.DeferBindingResolution())
			{
				InputActionState.UnmanagedMemory oldMemory = default(InputActionState.UnmanagedMemory);
				try
				{
					InputBindingResolver resolver = default(InputBindingResolver);
					bool needFullResolve = this.m_State == null;
					OneOrMore<InputActionMap, ReadOnlyArray<InputActionMap>> actionMaps;
					if (this.m_Asset != null)
					{
						actionMaps = this.m_Asset.actionMaps;
						resolver.bindingMask = this.m_Asset.m_BindingMask;
						using (IEnumerator<InputActionMap> enumerator = actionMaps.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								InputActionMap map = enumerator.Current;
								needFullResolve |= map.bindingResolutionNeedsFullReResolve;
								map.needToResolveBindings = false;
								map.bindingResolutionNeedsFullReResolve = false;
								map.controlsForEachActionInitialized = false;
							}
							goto IL_00C8;
						}
					}
					actionMaps = this;
					needFullResolve |= this.bindingResolutionNeedsFullReResolve;
					this.needToResolveBindings = false;
					this.bindingResolutionNeedsFullReResolve = false;
					this.controlsForEachActionInitialized = false;
					IL_00C8:
					bool hasEnabledActions = false;
					InputControlList<InputControl> activeControls = default(InputControlList<InputControl>);
					if (this.m_State != null)
					{
						oldMemory = this.m_State.memory.Clone();
						this.m_State.PrepareForBindingReResolution(needFullResolve, ref activeControls, ref hasEnabledActions);
						resolver.StartWithPreviousResolve(this.m_State, needFullResolve);
						this.m_State.memory.Dispose();
					}
					foreach (InputActionMap map2 in actionMaps)
					{
						resolver.AddActionMap(map2);
					}
					if (this.m_State == null)
					{
						this.m_State = new InputActionState();
						this.m_State.Initialize(resolver);
					}
					else
					{
						this.m_State.ClaimDataFrom(resolver);
					}
					if (this.m_Asset != null)
					{
						foreach (InputActionMap inputActionMap in actionMaps)
						{
							inputActionMap.m_State = this.m_State;
						}
						this.m_Asset.m_SharedStateForAllMaps = this.m_State;
					}
					this.m_State.FinishBindingResolution(hasEnabledActions, oldMemory, activeControls, needFullResolve);
				}
				finally
				{
					oldMemory.Dispose();
				}
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00005DD0 File Offset: 0x00003FD0
		public int FindBinding(InputBinding mask, out InputAction action)
		{
			int index = this.FindBindingRelativeToMap(mask);
			if (index == -1)
			{
				action = null;
				return -1;
			}
			action = this.m_SingletonAction ?? this.FindAction(this.bindings[index].action, false);
			return action.BindingIndexOnMapToBindingIndexOnAction(index);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00005E20 File Offset: 0x00004020
		internal int FindBindingRelativeToMap(InputBinding mask)
		{
			InputBinding[] bindings = this.m_Bindings;
			int bindingsCount = bindings.LengthSafe<InputBinding>();
			for (int i = 0; i < bindingsCount; i++)
			{
				ref InputBinding binding = ref bindings[i];
				if (mask.Matches(ref binding, (InputBinding.MatchOptions)0))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00005E60 File Offset: 0x00004060
		public static InputActionMap[] FromJson(string json)
		{
			if (json == null)
			{
				throw new ArgumentNullException("json");
			}
			return JsonUtility.FromJson<InputActionMap.ReadFileJson>(json).ToMaps();
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00005E89 File Offset: 0x00004089
		public static string ToJson(IEnumerable<InputActionMap> maps)
		{
			if (maps == null)
			{
				throw new ArgumentNullException("maps");
			}
			return JsonUtility.ToJson(InputActionMap.WriteFileJson.FromMaps(maps), true);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00005EAA File Offset: 0x000040AA
		public string ToJson()
		{
			return JsonUtility.ToJson(InputActionMap.WriteFileJson.FromMap(this), true);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000049FE File Offset: 0x00002BFE
		public void OnBeforeSerialize()
		{
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00005EC0 File Offset: 0x000040C0
		public void OnAfterDeserialize()
		{
			InputActionMap.s_NeedToResolveBindings = true;
			this.m_State = null;
			this.m_MapIndexInState = -1;
			this.m_EnabledActionsCount = 0;
			if (this.m_Actions != null)
			{
				int actionCount = this.m_Actions.Length;
				for (int i = 0; i < actionCount; i++)
				{
					this.m_Actions[i].m_ActionMap = this;
				}
			}
			this.ClearCachedActionData(false);
			this.ClearActionLookupTable();
		}

		// Token: 0x040000C7 RID: 199
		[SerializeField]
		internal string m_Name;

		// Token: 0x040000C8 RID: 200
		[SerializeField]
		internal string m_Id;

		// Token: 0x040000C9 RID: 201
		[SerializeField]
		internal InputActionAsset m_Asset;

		// Token: 0x040000CA RID: 202
		[SerializeField]
		internal InputAction[] m_Actions;

		// Token: 0x040000CB RID: 203
		[SerializeField]
		internal InputBinding[] m_Bindings;

		// Token: 0x040000CC RID: 204
		[NonSerialized]
		private InputBinding[] m_BindingsForEachAction;

		// Token: 0x040000CD RID: 205
		[NonSerialized]
		private InputControl[] m_ControlsForEachAction;

		// Token: 0x040000CE RID: 206
		[NonSerialized]
		internal int m_EnabledActionsCount;

		// Token: 0x040000CF RID: 207
		[NonSerialized]
		internal InputAction m_SingletonAction;

		// Token: 0x040000D0 RID: 208
		[NonSerialized]
		internal int m_MapIndexInState = -1;

		// Token: 0x040000D1 RID: 209
		[NonSerialized]
		internal InputActionState m_State;

		// Token: 0x040000D2 RID: 210
		[NonSerialized]
		internal InputBinding? m_BindingMask;

		// Token: 0x040000D3 RID: 211
		[NonSerialized]
		private InputActionMap.Flags m_Flags;

		// Token: 0x040000D4 RID: 212
		[NonSerialized]
		internal int m_ParameterOverridesCount;

		// Token: 0x040000D5 RID: 213
		[NonSerialized]
		internal InputActionRebindingExtensions.ParameterOverride[] m_ParameterOverrides;

		// Token: 0x040000D6 RID: 214
		[NonSerialized]
		internal InputActionMap.DeviceArray m_Devices;

		// Token: 0x040000D7 RID: 215
		[NonSerialized]
		internal CallbackArray<Action<InputAction.CallbackContext>> m_ActionCallbacks;

		// Token: 0x040000D8 RID: 216
		[NonSerialized]
		internal Dictionary<string, int> m_ActionIndexByNameOrId;

		// Token: 0x040000D9 RID: 217
		internal static int s_DeferBindingResolution;

		// Token: 0x040000DA RID: 218
		internal static bool s_NeedToResolveBindings;

		// Token: 0x02000027 RID: 39
		[Flags]
		private enum Flags
		{
			// Token: 0x040000DC RID: 220
			NeedToResolveBindings = 1,
			// Token: 0x040000DD RID: 221
			BindingResolutionNeedsFullReResolve = 2,
			// Token: 0x040000DE RID: 222
			ControlsForEachActionInitialized = 4,
			// Token: 0x040000DF RID: 223
			BindingsForEachActionInitialized = 8
		}

		// Token: 0x02000028 RID: 40
		internal struct DeviceArray
		{
			// Token: 0x060001EC RID: 492 RVA: 0x00005F20 File Offset: 0x00004120
			public int IndexOf(InputDevice device)
			{
				return this.m_DeviceArray.IndexOfReference(device, this.m_DeviceCount);
			}

			// Token: 0x060001ED RID: 493 RVA: 0x00005F34 File Offset: 0x00004134
			public bool Remove(InputDevice device)
			{
				int index = this.IndexOf(device);
				if (index < 0)
				{
					return false;
				}
				this.m_DeviceArray.EraseAtWithCapacity(ref this.m_DeviceCount, index);
				return true;
			}

			// Token: 0x060001EE RID: 494 RVA: 0x00005F64 File Offset: 0x00004164
			public ReadOnlyArray<InputDevice>? Get()
			{
				if (!this.m_HaveValue)
				{
					return null;
				}
				return new ReadOnlyArray<InputDevice>?(new ReadOnlyArray<InputDevice>(this.m_DeviceArray, 0, this.m_DeviceCount));
			}

			// Token: 0x060001EF RID: 495 RVA: 0x00005F9C File Offset: 0x0000419C
			public bool Set(ReadOnlyArray<InputDevice>? devices)
			{
				if (devices == null)
				{
					if (!this.m_HaveValue)
					{
						return false;
					}
					if (this.m_DeviceCount > 0)
					{
						Array.Clear(this.m_DeviceArray, 0, this.m_DeviceCount);
					}
					this.m_DeviceCount = 0;
					this.m_HaveValue = false;
				}
				else
				{
					ReadOnlyArray<InputDevice> array = devices.Value;
					if (this.m_HaveValue && array.Count == this.m_DeviceCount && array.HaveEqualReferences(this.m_DeviceArray, this.m_DeviceCount))
					{
						return false;
					}
					if (this.m_DeviceCount > 0)
					{
						this.m_DeviceArray.Clear(ref this.m_DeviceCount);
					}
					this.m_HaveValue = true;
					this.m_DeviceCount = 0;
					ArrayHelpers.AppendListWithCapacity<InputDevice, ReadOnlyArray<InputDevice>>(ref this.m_DeviceArray, ref this.m_DeviceCount, array, 10);
				}
				return true;
			}

			// Token: 0x040000E0 RID: 224
			private bool m_HaveValue;

			// Token: 0x040000E1 RID: 225
			private int m_DeviceCount;

			// Token: 0x040000E2 RID: 226
			private InputDevice[] m_DeviceArray;
		}

		// Token: 0x02000029 RID: 41
		[Serializable]
		internal struct BindingOverrideListJson
		{
			// Token: 0x040000E3 RID: 227
			public List<InputActionMap.BindingOverrideJson> bindings;
		}

		// Token: 0x0200002A RID: 42
		[Serializable]
		internal struct BindingOverrideJson
		{
			// Token: 0x060001F0 RID: 496 RVA: 0x0000605C File Offset: 0x0000425C
			public static InputActionMap.BindingOverrideJson FromBinding(InputBinding binding, string actionName)
			{
				return new InputActionMap.BindingOverrideJson
				{
					action = actionName,
					id = binding.id.ToString(),
					path = (binding.overridePath ?? "null"),
					interactions = (binding.overrideInteractions ?? "null"),
					processors = (binding.overrideProcessors ?? "null")
				};
			}

			// Token: 0x060001F1 RID: 497 RVA: 0x000060DB File Offset: 0x000042DB
			public static InputActionMap.BindingOverrideJson FromBinding(InputBinding binding)
			{
				return InputActionMap.BindingOverrideJson.FromBinding(binding, binding.action);
			}

			// Token: 0x060001F2 RID: 498 RVA: 0x000060EC File Offset: 0x000042EC
			public static InputBinding ToBinding(InputActionMap.BindingOverrideJson bindingOverride)
			{
				return new InputBinding
				{
					overridePath = ((bindingOverride.path != "null") ? bindingOverride.path : null),
					overrideInteractions = ((bindingOverride.interactions != "null") ? bindingOverride.interactions : null),
					overrideProcessors = ((bindingOverride.processors != "null") ? bindingOverride.processors : null)
				};
			}

			// Token: 0x040000E4 RID: 228
			public string action;

			// Token: 0x040000E5 RID: 229
			public string id;

			// Token: 0x040000E6 RID: 230
			public string path;

			// Token: 0x040000E7 RID: 231
			public string interactions;

			// Token: 0x040000E8 RID: 232
			public string processors;
		}

		// Token: 0x0200002B RID: 43
		[Serializable]
		internal struct BindingJson
		{
			// Token: 0x060001F3 RID: 499 RVA: 0x00006168 File Offset: 0x00004368
			public InputBinding ToBinding()
			{
				return new InputBinding
				{
					name = (string.IsNullOrEmpty(this.name) ? null : this.name),
					m_Id = (string.IsNullOrEmpty(this.id) ? null : this.id),
					path = this.path,
					action = (string.IsNullOrEmpty(this.action) ? null : this.action),
					interactions = (string.IsNullOrEmpty(this.interactions) ? null : this.interactions),
					processors = (string.IsNullOrEmpty(this.processors) ? null : this.processors),
					groups = (string.IsNullOrEmpty(this.groups) ? null : this.groups),
					isComposite = this.isComposite,
					isPartOfComposite = this.isPartOfComposite
				};
			}

			// Token: 0x060001F4 RID: 500 RVA: 0x00006254 File Offset: 0x00004454
			public static InputActionMap.BindingJson FromBinding(ref InputBinding binding)
			{
				return new InputActionMap.BindingJson
				{
					name = binding.name,
					id = binding.m_Id,
					path = binding.path,
					action = binding.action,
					interactions = binding.interactions,
					processors = binding.processors,
					groups = binding.groups,
					isComposite = binding.isComposite,
					isPartOfComposite = binding.isPartOfComposite
				};
			}

			// Token: 0x040000E9 RID: 233
			public string name;

			// Token: 0x040000EA RID: 234
			public string id;

			// Token: 0x040000EB RID: 235
			public string path;

			// Token: 0x040000EC RID: 236
			public string interactions;

			// Token: 0x040000ED RID: 237
			public string processors;

			// Token: 0x040000EE RID: 238
			public string groups;

			// Token: 0x040000EF RID: 239
			public string action;

			// Token: 0x040000F0 RID: 240
			public bool isComposite;

			// Token: 0x040000F1 RID: 241
			public bool isPartOfComposite;
		}

		// Token: 0x0200002C RID: 44
		[Serializable]
		internal struct ReadActionJson
		{
			// Token: 0x060001F5 RID: 501 RVA: 0x000062E0 File Offset: 0x000044E0
			public InputAction ToAction(string actionName = null)
			{
				if (!string.IsNullOrEmpty(this.expectedControlLayout))
				{
					this.expectedControlType = this.expectedControlLayout;
				}
				InputActionType actionType = InputActionType.Value;
				if (!string.IsNullOrEmpty(this.type))
				{
					actionType = (InputActionType)Enum.Parse(typeof(InputActionType), this.type, true);
				}
				else if (this.passThrough)
				{
					actionType = InputActionType.PassThrough;
				}
				else if (this.initialStateCheck)
				{
					actionType = InputActionType.Value;
				}
				else if (!string.IsNullOrEmpty(this.expectedControlType) && (this.expectedControlType == "Button" || this.expectedControlType == "Key"))
				{
					actionType = InputActionType.Button;
				}
				return new InputAction(actionName ?? this.name, actionType, null, null, null, null)
				{
					m_Id = (string.IsNullOrEmpty(this.id) ? null : this.id),
					m_ExpectedControlType = ((!string.IsNullOrEmpty(this.expectedControlType)) ? this.expectedControlType : null),
					m_Processors = this.processors,
					m_Interactions = this.interactions,
					wantsInitialStateCheck = this.initialStateCheck
				};
			}

			// Token: 0x040000F2 RID: 242
			public string name;

			// Token: 0x040000F3 RID: 243
			public string type;

			// Token: 0x040000F4 RID: 244
			public string id;

			// Token: 0x040000F5 RID: 245
			public string expectedControlType;

			// Token: 0x040000F6 RID: 246
			public string expectedControlLayout;

			// Token: 0x040000F7 RID: 247
			public string processors;

			// Token: 0x040000F8 RID: 248
			public string interactions;

			// Token: 0x040000F9 RID: 249
			public bool passThrough;

			// Token: 0x040000FA RID: 250
			public bool initialStateCheck;

			// Token: 0x040000FB RID: 251
			public InputActionMap.BindingJson[] bindings;
		}

		// Token: 0x0200002D RID: 45
		[Serializable]
		internal struct WriteActionJson
		{
			// Token: 0x060001F6 RID: 502 RVA: 0x000063F0 File Offset: 0x000045F0
			public static InputActionMap.WriteActionJson FromAction(InputAction action)
			{
				return new InputActionMap.WriteActionJson
				{
					name = action.m_Name,
					type = action.m_Type.ToString(),
					id = action.m_Id,
					expectedControlType = action.m_ExpectedControlType,
					processors = action.processors,
					interactions = action.interactions,
					initialStateCheck = action.wantsInitialStateCheck
				};
			}

			// Token: 0x040000FC RID: 252
			public string name;

			// Token: 0x040000FD RID: 253
			public string type;

			// Token: 0x040000FE RID: 254
			public string id;

			// Token: 0x040000FF RID: 255
			public string expectedControlType;

			// Token: 0x04000100 RID: 256
			public string processors;

			// Token: 0x04000101 RID: 257
			public string interactions;

			// Token: 0x04000102 RID: 258
			public bool initialStateCheck;
		}

		// Token: 0x0200002E RID: 46
		[Serializable]
		internal struct ReadMapJson
		{
			// Token: 0x04000103 RID: 259
			public string name;

			// Token: 0x04000104 RID: 260
			public string id;

			// Token: 0x04000105 RID: 261
			public InputActionMap.ReadActionJson[] actions;

			// Token: 0x04000106 RID: 262
			public InputActionMap.BindingJson[] bindings;
		}

		// Token: 0x0200002F RID: 47
		[Serializable]
		internal struct WriteMapJson
		{
			// Token: 0x060001F7 RID: 503 RVA: 0x0000646C File Offset: 0x0000466C
			public static InputActionMap.WriteMapJson FromMap(InputActionMap map)
			{
				InputActionMap.WriteActionJson[] jsonActions = null;
				InputActionMap.BindingJson[] jsonBindings = null;
				InputAction[] actions = map.m_Actions;
				if (actions != null)
				{
					int actionCount = actions.Length;
					jsonActions = new InputActionMap.WriteActionJson[actionCount];
					for (int i = 0; i < actionCount; i++)
					{
						jsonActions[i] = InputActionMap.WriteActionJson.FromAction(actions[i]);
					}
				}
				InputBinding[] bindings = map.m_Bindings;
				if (bindings != null)
				{
					int bindingCount = bindings.Length;
					jsonBindings = new InputActionMap.BindingJson[bindingCount];
					for (int j = 0; j < bindingCount; j++)
					{
						jsonBindings[j] = InputActionMap.BindingJson.FromBinding(ref bindings[j]);
					}
				}
				return new InputActionMap.WriteMapJson
				{
					name = map.name,
					id = map.id.ToString(),
					actions = jsonActions,
					bindings = jsonBindings
				};
			}

			// Token: 0x04000107 RID: 263
			public string name;

			// Token: 0x04000108 RID: 264
			public string id;

			// Token: 0x04000109 RID: 265
			public InputActionMap.WriteActionJson[] actions;

			// Token: 0x0400010A RID: 266
			public InputActionMap.BindingJson[] bindings;
		}

		// Token: 0x02000030 RID: 48
		[Serializable]
		internal struct WriteFileJson
		{
			// Token: 0x060001F8 RID: 504 RVA: 0x00006538 File Offset: 0x00004738
			public static InputActionMap.WriteFileJson FromMap(InputActionMap map)
			{
				return new InputActionMap.WriteFileJson
				{
					maps = new InputActionMap.WriteMapJson[] { InputActionMap.WriteMapJson.FromMap(map) }
				};
			}

			// Token: 0x060001F9 RID: 505 RVA: 0x00006568 File Offset: 0x00004768
			public static InputActionMap.WriteFileJson FromMaps(IEnumerable<InputActionMap> maps)
			{
				int mapCount = maps.Count<InputActionMap>();
				if (mapCount == 0)
				{
					return default(InputActionMap.WriteFileJson);
				}
				InputActionMap.WriteMapJson[] mapsJson = new InputActionMap.WriteMapJson[mapCount];
				int index = 0;
				foreach (InputActionMap map in maps)
				{
					mapsJson[index++] = InputActionMap.WriteMapJson.FromMap(map);
				}
				return new InputActionMap.WriteFileJson
				{
					maps = mapsJson
				};
			}

			// Token: 0x0400010B RID: 267
			public InputActionMap.WriteMapJson[] maps;
		}

		// Token: 0x02000031 RID: 49
		[Serializable]
		internal struct ReadFileJson
		{
			// Token: 0x060001FA RID: 506 RVA: 0x000065F0 File Offset: 0x000047F0
			public InputActionMap[] ToMaps()
			{
				List<InputActionMap> mapList = new List<InputActionMap>();
				List<List<InputAction>> actionLists = new List<List<InputAction>>();
				List<List<InputBinding>> bindingLists = new List<List<InputBinding>>();
				InputActionMap.ReadActionJson[] array = this.actions;
				int actionCount = ((array != null) ? array.Length : 0);
				for (int i = 0; i < actionCount; i++)
				{
					InputActionMap.ReadActionJson jsonAction = this.actions[i];
					if (string.IsNullOrEmpty(jsonAction.name))
					{
						throw new InvalidOperationException(string.Format("Action number {0} has no name", i + 1));
					}
					string mapName = null;
					string actionName = jsonAction.name;
					int indexOfFirstSlash = actionName.IndexOf('/');
					if (indexOfFirstSlash != -1)
					{
						mapName = actionName.Substring(0, indexOfFirstSlash);
						actionName = actionName.Substring(indexOfFirstSlash + 1);
						if (string.IsNullOrEmpty(actionName))
						{
							throw new InvalidOperationException("Invalid action name '" + jsonAction.name + "' (missing action name after '/')");
						}
					}
					InputActionMap map = null;
					int mapIndex;
					for (mapIndex = 0; mapIndex < mapList.Count; mapIndex++)
					{
						if (string.Compare(mapList[mapIndex].name, mapName, StringComparison.InvariantCultureIgnoreCase) == 0)
						{
							map = mapList[mapIndex];
							break;
						}
					}
					if (map == null)
					{
						map = new InputActionMap(mapName);
						mapIndex = mapList.Count;
						mapList.Add(map);
						actionLists.Add(new List<InputAction>());
						bindingLists.Add(new List<InputBinding>());
					}
					InputAction action = jsonAction.ToAction(actionName);
					actionLists[mapIndex].Add(action);
					if (jsonAction.bindings != null)
					{
						List<InputBinding> bindingsForMap = bindingLists[mapIndex];
						for (int j = 0; j < jsonAction.bindings.Length; j++)
						{
							InputActionMap.BindingJson jsonBinding = jsonAction.bindings[j];
							InputBinding binding = jsonBinding.ToBinding();
							binding.action = action.m_Name;
							bindingsForMap.Add(binding);
						}
					}
				}
				InputActionMap.ReadMapJson[] array2 = this.maps;
				int mapCount = ((array2 != null) ? array2.Length : 0);
				for (int k = 0; k < mapCount; k++)
				{
					InputActionMap.ReadMapJson jsonMap = this.maps[k];
					string mapName2 = jsonMap.name;
					if (string.IsNullOrEmpty(mapName2))
					{
						throw new InvalidOperationException(string.Format("Map number {0} has no name", k + 1));
					}
					InputActionMap map2 = null;
					int mapIndex2;
					for (mapIndex2 = 0; mapIndex2 < mapList.Count; mapIndex2++)
					{
						if (string.Compare(mapList[mapIndex2].name, mapName2, StringComparison.InvariantCultureIgnoreCase) == 0)
						{
							map2 = mapList[mapIndex2];
							break;
						}
					}
					if (map2 == null)
					{
						map2 = new InputActionMap(mapName2)
						{
							m_Id = (string.IsNullOrEmpty(jsonMap.id) ? null : jsonMap.id)
						};
						mapIndex2 = mapList.Count;
						mapList.Add(map2);
						actionLists.Add(new List<InputAction>());
						bindingLists.Add(new List<InputBinding>());
					}
					InputActionMap.ReadActionJson[] array3 = jsonMap.actions;
					int actionCountInMap = ((array3 != null) ? array3.Length : 0);
					for (int l = 0; l < actionCountInMap; l++)
					{
						InputActionMap.ReadActionJson jsonAction2 = jsonMap.actions[l];
						if (string.IsNullOrEmpty(jsonAction2.name))
						{
							throw new InvalidOperationException(string.Format("Action number {0} in map '{1}' has no name", k + 1, mapName2));
						}
						InputAction action2 = jsonAction2.ToAction(null);
						actionLists[mapIndex2].Add(action2);
						if (jsonAction2.bindings != null)
						{
							List<InputBinding> bindingList = bindingLists[mapIndex2];
							for (int m = 0; m < jsonAction2.bindings.Length; m++)
							{
								InputActionMap.BindingJson jsonBinding2 = jsonAction2.bindings[m];
								InputBinding binding2 = jsonBinding2.ToBinding();
								binding2.action = action2.m_Name;
								bindingList.Add(binding2);
							}
						}
					}
					InputActionMap.BindingJson[] bindings = jsonMap.bindings;
					int bindingCountInMap = ((bindings != null) ? bindings.Length : 0);
					List<InputBinding> bindingsForMap2 = bindingLists[mapIndex2];
					for (int n = 0; n < bindingCountInMap; n++)
					{
						InputActionMap.BindingJson jsonBinding3 = jsonMap.bindings[n];
						InputBinding binding3 = jsonBinding3.ToBinding();
						bindingsForMap2.Add(binding3);
					}
				}
				for (int i2 = 0; i2 < mapList.Count; i2++)
				{
					InputActionMap map3 = mapList[i2];
					InputAction[] actionArray = actionLists[i2].ToArray();
					InputBinding[] bindingArray = bindingLists[i2].ToArray();
					map3.m_Actions = actionArray;
					map3.m_Bindings = bindingArray;
					for (int n2 = 0; n2 < actionArray.Length; n2++)
					{
						actionArray[n2].m_ActionMap = map3;
					}
				}
				return mapList.ToArray();
			}

			// Token: 0x0400010C RID: 268
			public InputActionMap.ReadActionJson[] actions;

			// Token: 0x0400010D RID: 269
			public InputActionMap.ReadMapJson[] maps;
		}
	}
}
