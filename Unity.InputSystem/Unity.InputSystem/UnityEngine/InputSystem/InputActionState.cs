using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Profiling;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000044 RID: 68
	internal class InputActionState : IInputStateChangeMonitor, ICloneable, IDisposable
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000B7B6 File Offset: 0x000099B6
		public int totalCompositeCount
		{
			get
			{
				return this.memory.compositeCount;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000B7C3 File Offset: 0x000099C3
		public int totalMapCount
		{
			get
			{
				return this.memory.mapCount;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000B7D0 File Offset: 0x000099D0
		public int totalActionCount
		{
			get
			{
				return this.memory.actionCount;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000B7DD File Offset: 0x000099DD
		public int totalBindingCount
		{
			get
			{
				return this.memory.bindingCount;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000B7EA File Offset: 0x000099EA
		public int totalInteractionCount
		{
			get
			{
				return this.memory.interactionCount;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000B7F7 File Offset: 0x000099F7
		public int totalControlCount
		{
			get
			{
				return this.memory.controlCount;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000B804 File Offset: 0x00009A04
		public unsafe InputActionState.ActionMapIndices* mapIndices
		{
			get
			{
				return this.memory.mapIndices;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000B811 File Offset: 0x00009A11
		public unsafe InputActionState.TriggerState* actionStates
		{
			get
			{
				return this.memory.actionStates;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000B81E File Offset: 0x00009A1E
		public unsafe InputActionState.BindingState* bindingStates
		{
			get
			{
				return this.memory.bindingStates;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000B82B File Offset: 0x00009A2B
		public unsafe InputActionState.InteractionState* interactionStates
		{
			get
			{
				return this.memory.interactionStates;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000B838 File Offset: 0x00009A38
		public unsafe int* controlIndexToBindingIndex
		{
			get
			{
				return this.memory.controlIndexToBindingIndex;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000B845 File Offset: 0x00009A45
		public unsafe ushort* controlGroupingAndComplexity
		{
			get
			{
				return this.memory.controlGroupingAndComplexity;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000B852 File Offset: 0x00009A52
		public unsafe float* controlMagnitudes
		{
			get
			{
				return this.memory.controlMagnitudes;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000B85F File Offset: 0x00009A5F
		public unsafe uint* enabledControls
		{
			get
			{
				return (uint*)this.memory.enabledControls;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000B86C File Offset: 0x00009A6C
		public bool isProcessingControlStateChange
		{
			get
			{
				return this.m_InProcessControlStateChange;
			}
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000B874 File Offset: 0x00009A74
		public void Initialize(InputBindingResolver resolver)
		{
			this.ClaimDataFrom(resolver);
			this.AddToGlobalList();
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000B884 File Offset: 0x00009A84
		private unsafe void ComputeControlGroupingIfNecessary()
		{
			if (this.memory.controlGroupingInitialized)
			{
				return;
			}
			bool disableControlGrouping = !InputSystem.settings.shortcutKeysConsumeInput;
			uint currentGroup = 1U;
			for (int i = 0; i < this.totalControlCount; i++)
			{
				InputControl control = this.controls[i];
				int bindingIndex = this.controlIndexToBindingIndex[i];
				ref InputActionState.BindingState binding = ref this.bindingStates[bindingIndex];
				int complexity = 1;
				if (binding.isPartOfComposite && !disableControlGrouping)
				{
					int compositeBindingIndex = binding.compositeOrCompositeBindingIndex;
					for (int j = compositeBindingIndex + 1; j < this.totalBindingCount; j++)
					{
						ref InputActionState.BindingState partBinding = ref this.bindingStates[j];
						if (!partBinding.isPartOfComposite || partBinding.compositeOrCompositeBindingIndex != compositeBindingIndex)
						{
							break;
						}
						complexity++;
					}
				}
				this.controlGroupingAndComplexity[i * 2 + 1] = (ushort)complexity;
				if (this.controlGroupingAndComplexity[i * 2] == 0)
				{
					if (!disableControlGrouping)
					{
						for (int k = 0; k < this.totalControlCount; k++)
						{
							InputControl otherControl = this.controls[k];
							if (control == otherControl)
							{
								this.controlGroupingAndComplexity[k * 2] = (ushort)currentGroup;
							}
						}
					}
					this.controlGroupingAndComplexity[i * 2] = (ushort)currentGroup;
					currentGroup += 1U;
				}
			}
			this.memory.controlGroupingInitialized = true;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000B9CC File Offset: 0x00009BCC
		public void ClaimDataFrom(InputBindingResolver resolver)
		{
			this.totalProcessorCount = resolver.totalProcessorCount;
			this.maps = resolver.maps;
			this.interactions = resolver.interactions;
			this.processors = resolver.processors;
			this.composites = resolver.composites;
			this.controls = resolver.controls;
			this.memory = resolver.memory;
			resolver.memory = default(InputActionState.UnmanagedMemory);
			this.ComputeControlGroupingIfNecessary();
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000BA40 File Offset: 0x00009C40
		~InputActionState()
		{
			this.Destroy(true);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000BA70 File Offset: 0x00009C70
		public void Dispose()
		{
			this.Destroy(false);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000BA7C File Offset: 0x00009C7C
		private unsafe void Destroy(bool isFinalizing = false)
		{
			if (!isFinalizing)
			{
				for (int i = 0; i < this.totalMapCount; i++)
				{
					InputActionMap map = this.maps[i];
					if (map.enabled)
					{
						this.DisableControls(i, this.mapIndices[i].controlStartIndex, this.mapIndices[i].controlCount);
					}
					if (map.m_Asset != null)
					{
						map.m_Asset.m_SharedStateForAllMaps = null;
					}
					map.m_State = null;
					map.m_MapIndexInState = -1;
					map.m_EnabledActionsCount = 0;
					InputAction[] actions = map.m_Actions;
					if (actions != null)
					{
						for (int j = 0; j < actions.Length; j++)
						{
							actions[j].m_ActionIndexInState = -1;
						}
					}
				}
				this.RemoveMapFromGlobalList();
			}
			this.memory.Dispose();
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000BB4C File Offset: 0x00009D4C
		public InputActionState Clone()
		{
			return new InputActionState
			{
				maps = ArrayHelpers.Copy<InputActionMap>(this.maps),
				controls = ArrayHelpers.Copy<InputControl>(this.controls),
				interactions = ArrayHelpers.Copy<IInputInteraction>(this.interactions),
				processors = ArrayHelpers.Copy<InputProcessor>(this.processors),
				composites = ArrayHelpers.Copy<InputBindingComposite>(this.composites),
				totalProcessorCount = this.totalProcessorCount,
				memory = this.memory.Clone()
			};
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000BBD0 File Offset: 0x00009DD0
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000BBD8 File Offset: 0x00009DD8
		private bool IsUsingDevice(InputDevice device)
		{
			bool haveMapsWithoutDeviceRestrictions = false;
			for (int i = 0; i < this.totalMapCount; i++)
			{
				ReadOnlyArray<InputDevice>? devicesForMap = this.maps[i].devices;
				if (devicesForMap == null)
				{
					haveMapsWithoutDeviceRestrictions = true;
				}
				else if (devicesForMap.Value.Contains(device))
				{
					return true;
				}
			}
			if (!haveMapsWithoutDeviceRestrictions)
			{
				return false;
			}
			for (int j = 0; j < this.totalControlCount; j++)
			{
				if (this.controls[j].device == device)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000BC54 File Offset: 0x00009E54
		private bool CanUseDevice(InputDevice device)
		{
			bool haveMapWithoutDeviceRestrictions = false;
			for (int i = 0; i < this.totalMapCount; i++)
			{
				ReadOnlyArray<InputDevice>? devicesForMap = this.maps[i].devices;
				if (devicesForMap == null)
				{
					haveMapWithoutDeviceRestrictions = true;
				}
				else if (devicesForMap.Value.Contains(device))
				{
					return true;
				}
			}
			if (!haveMapWithoutDeviceRestrictions)
			{
				return false;
			}
			for (int j = 0; j < this.totalMapCount; j++)
			{
				InputBinding[] bindings = this.maps[j].m_Bindings;
				if (bindings != null)
				{
					int bindingCount = bindings.Length;
					for (int k = 0; k < bindingCount; k++)
					{
						if (InputControlPath.TryFindControl(device, bindings[k].effectivePath, 0) != null)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000BD00 File Offset: 0x00009F00
		public bool HasEnabledActions()
		{
			for (int i = 0; i < this.totalMapCount; i++)
			{
				if (this.maps[i].enabled)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000BD30 File Offset: 0x00009F30
		private unsafe void FinishBindingCompositeSetups()
		{
			for (int i = 0; i < this.totalBindingCount; i++)
			{
				ref InputActionState.BindingState binding = ref this.bindingStates[i];
				if (binding.isComposite && binding.compositeOrCompositeBindingIndex != -1)
				{
					InputBindingComposite inputBindingComposite = this.composites[binding.compositeOrCompositeBindingIndex];
					InputBindingCompositeContext context = new InputBindingCompositeContext
					{
						m_State = this,
						m_BindingIndex = i
					};
					inputBindingComposite.CallFinishSetup(ref context);
				}
			}
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000BDA0 File Offset: 0x00009FA0
		internal unsafe void PrepareForBindingReResolution(bool needFullResolve, ref InputControlList<InputControl> activeControls, ref bool hasEnabledActions)
		{
			bool needToCloneActiveControls = false;
			for (int i = 0; i < this.totalMapCount; i++)
			{
				InputActionMap map = this.maps[i];
				if (map.enabled)
				{
					hasEnabledActions = true;
					if (needFullResolve)
					{
						this.DisableAllActions(map);
					}
					else
					{
						foreach (InputAction action in map.actions)
						{
							if (action.phase.IsInProgress())
							{
								if (action.ActiveControlIsValid(action.activeControl))
								{
									if (!needToCloneActiveControls)
									{
										activeControls = new InputControlList<InputControl>(Allocator.Temp, 0);
										activeControls.Resize(this.totalControlCount);
										needToCloneActiveControls = true;
									}
									ref InputActionState.TriggerState actionState = ref this.actionStates[action.m_ActionIndexInState];
									int activeControlIndex = actionState.controlIndex;
									activeControls[activeControlIndex] = this.controls[activeControlIndex];
									InputActionState.BindingState bindingState = this.bindingStates[actionState.bindingIndex];
									for (int j = 0; j < bindingState.interactionCount; j++)
									{
										int interactionIndex = bindingState.interactionStartIndex + j;
										if (this.interactionStates[interactionIndex].phase.IsInProgress())
										{
											activeControlIndex = this.interactionStates[interactionIndex].triggerControlIndex;
											if (action.ActiveControlIsValid(this.controls[activeControlIndex]))
											{
												activeControls[activeControlIndex] = this.controls[activeControlIndex];
											}
											else
											{
												this.ResetInteractionState(interactionIndex);
											}
										}
									}
								}
								else
								{
									this.ResetActionState(action.m_ActionIndexInState, InputActionPhase.Waiting, false);
								}
							}
						}
						this.DisableControls(map);
					}
				}
				map.ClearCachedActionData(!needFullResolve);
			}
			this.NotifyListenersOfActionChange(InputActionChange.BoundControlsAboutToChange);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000BF80 File Offset: 0x0000A180
		public void FinishBindingResolution(bool hasEnabledActions, InputActionState.UnmanagedMemory oldMemory, InputControlList<InputControl> activeControls, bool isFullResolve)
		{
			this.FinishBindingCompositeSetups();
			if (hasEnabledActions)
			{
				this.RestoreActionStatesAfterReResolvingBindings(oldMemory, activeControls, isFullResolve);
				return;
			}
			this.NotifyListenersOfActionChange(InputActionChange.BoundControlsChanged);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000BFA0 File Offset: 0x0000A1A0
		private unsafe void RestoreActionStatesAfterReResolvingBindings(InputActionState.UnmanagedMemory oldState, InputControlList<InputControl> activeControls, bool isFullResolve)
		{
			for (int actionIndex = 0; actionIndex < this.totalActionCount; actionIndex++)
			{
				ref InputActionState.TriggerState oldActionState = ref oldState.actionStates[actionIndex];
				ref InputActionState.TriggerState newActionState = ref this.actionStates[actionIndex];
				newActionState.lastCanceledInUpdate = oldActionState.lastCanceledInUpdate;
				newActionState.lastPerformedInUpdate = oldActionState.lastPerformedInUpdate;
				newActionState.lastCompletedInUpdate = oldActionState.lastCompletedInUpdate;
				newActionState.pressedInUpdate = oldActionState.pressedInUpdate;
				newActionState.releasedInUpdate = oldActionState.releasedInUpdate;
				newActionState.startTime = oldActionState.startTime;
				newActionState.bindingIndex = oldActionState.bindingIndex;
				newActionState.frame = oldActionState.frame;
				if (oldActionState.phase != InputActionPhase.Disabled)
				{
					newActionState.phase = InputActionPhase.Waiting;
					if (isFullResolve)
					{
						this.maps[newActionState.mapIndex].m_EnabledActionsCount++;
					}
				}
			}
			for (int bindingIndex = 0; bindingIndex < this.totalBindingCount; bindingIndex++)
			{
				ref InputActionState.BindingState newBindingState = ref this.memory.bindingStates[bindingIndex];
				if (!newBindingState.isPartOfComposite)
				{
					if (newBindingState.isComposite)
					{
						int compositeIndex = newBindingState.compositeOrCompositeBindingIndex;
						this.memory.compositeMagnitudes[compositeIndex] = oldState.compositeMagnitudes[compositeIndex];
					}
					int actionIndex2 = newBindingState.actionIndex;
					if (actionIndex2 != -1)
					{
						ref InputActionState.TriggerState newActionState2 = ref this.actionStates[actionIndex2];
						if (!newActionState2.isDisabled)
						{
							newBindingState.initialStateCheckPending = newBindingState.wantsInitialStateCheck;
							this.EnableControls(newBindingState.mapIndex, newBindingState.controlStartIndex, newBindingState.controlCount);
							if (!isFullResolve)
							{
								ref InputActionState.BindingState oldBindingState = ref this.memory.bindingStates[bindingIndex];
								newBindingState.triggerEventIdForComposite = oldBindingState.triggerEventIdForComposite;
								ref InputActionState.TriggerState oldActionState2 = ref oldState.actionStates[actionIndex2];
								if (bindingIndex == oldActionState2.bindingIndex && oldActionState2.phase.IsInProgress() && activeControls.Count > 0 && activeControls[oldActionState2.controlIndex] != null)
								{
									InputControl control = activeControls[oldActionState2.controlIndex];
									int newControlIndex = this.FindControlIndexOnBinding(bindingIndex, control);
									if (newControlIndex != -1)
									{
										newActionState2.phase = oldActionState2.phase;
										newActionState2.controlIndex = newControlIndex;
										newActionState2.magnitude = oldActionState2.magnitude;
										newActionState2.interactionIndex = oldActionState2.interactionIndex;
										this.memory.controlMagnitudes[newControlIndex] = oldActionState2.magnitude;
									}
									for (int i = 0; i < newBindingState.interactionCount; i++)
									{
										ref InputActionState.InteractionState oldInteractionState = ref oldState.interactionStates[oldBindingState.interactionStartIndex + i];
										if (oldInteractionState.phase.IsInProgress())
										{
											control = activeControls[oldInteractionState.triggerControlIndex];
											if (control != null)
											{
												newControlIndex = this.FindControlIndexOnBinding(bindingIndex, control);
												ref InputActionState.InteractionState newInteractionState = ref this.interactionStates[newBindingState.interactionStartIndex + i];
												newInteractionState.phase = oldInteractionState.phase;
												newInteractionState.performedTime = oldInteractionState.performedTime;
												newInteractionState.startTime = oldInteractionState.startTime;
												newInteractionState.triggerControlIndex = newControlIndex;
												if (oldInteractionState.isTimerRunning)
												{
													InputActionState.TriggerState trigger = new InputActionState.TriggerState
													{
														mapIndex = newBindingState.mapIndex,
														controlIndex = newControlIndex,
														bindingIndex = bindingIndex,
														time = oldInteractionState.timerStartTime,
														interactionIndex = newBindingState.interactionStartIndex + i
													};
													this.StartTimeout(oldInteractionState.timerDuration, ref trigger);
													newInteractionState.totalTimeoutCompletionDone = oldInteractionState.totalTimeoutCompletionDone;
													newInteractionState.totalTimeoutCompletionTimeRemaining = oldInteractionState.totalTimeoutCompletionTimeRemaining;
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			this.HookOnBeforeUpdate();
			this.NotifyListenersOfActionChange(InputActionChange.BoundControlsChanged);
			if (isFullResolve && InputActionState.s_GlobalState.onActionChange.length > 0)
			{
				for (int j = 0; j < this.totalMapCount; j++)
				{
					InputActionMap map = this.maps[j];
					if (map.m_SingletonAction == null && map.m_EnabledActionsCount == map.m_Actions.LengthSafe<InputAction>())
					{
						InputActionState.NotifyListenersOfActionChange(InputActionChange.ActionMapEnabled, map);
					}
					else
					{
						foreach (InputAction action in map.actions)
						{
							if (action.enabled)
							{
								InputActionState.NotifyListenersOfActionChange(InputActionChange.ActionEnabled, action);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000C424 File Offset: 0x0000A624
		private unsafe bool IsActiveControl(int bindingIndex, int controlIndex)
		{
			ref InputActionState.BindingState bindingState = ref this.bindingStates[bindingIndex];
			int actionIndex = bindingState.actionIndex;
			if (actionIndex == -1)
			{
				return false;
			}
			if (this.actionStates[actionIndex].controlIndex == controlIndex)
			{
				return true;
			}
			for (int i = 0; i < bindingState.interactionCount; i++)
			{
				if (this.interactionStates[this.bindingStates->interactionStartIndex + i].triggerControlIndex == controlIndex)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000C4A4 File Offset: 0x0000A6A4
		private unsafe int FindControlIndexOnBinding(int bindingIndex, InputControl control)
		{
			int controlStartIndex = this.bindingStates[bindingIndex].controlStartIndex;
			int controlCount = this.bindingStates[bindingIndex].controlCount;
			for (int i = 0; i < controlCount; i++)
			{
				if (control == this.controls[controlStartIndex + i])
				{
					return controlStartIndex + i;
				}
			}
			return -1;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000C4FC File Offset: 0x0000A6FC
		private unsafe void ResetActionStatesDrivenBy(InputDevice device)
		{
			using (InputActionRebindingExtensions.DeferBindingResolution())
			{
				for (int actionIndex = 0; actionIndex < this.totalActionCount; actionIndex++)
				{
					InputActionState.TriggerState* actionState = this.actionStates + actionIndex;
					if (actionState->phase != InputActionPhase.Waiting && actionState->phase != InputActionPhase.Disabled)
					{
						if (actionState->isPassThrough)
						{
							if (!this.IsActionBoundToControlFromDevice(device, actionIndex))
							{
								goto IL_0065;
							}
						}
						else
						{
							int controlIndex = actionState->controlIndex;
							if (controlIndex == -1 || this.controls[controlIndex].device != device)
							{
								goto IL_0065;
							}
						}
						this.ResetActionState(actionIndex, InputActionPhase.Waiting, false);
					}
					IL_0065:;
				}
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000C598 File Offset: 0x0000A798
		private unsafe bool IsActionBoundToControlFromDevice(InputDevice device, int actionIndex)
		{
			bool usesControlFromDevice = false;
			ushort bindingCount;
			ushort bindingStartIndex = this.GetActionBindingStartIndexAndCount(actionIndex, out bindingCount);
			for (int i = 0; i < (int)bindingCount; i++)
			{
				ushort bindingIndex = this.memory.actionBindingIndices[(int)bindingStartIndex + i];
				int controlCount = this.bindingStates[bindingIndex].controlCount;
				int controlStartIndex = this.bindingStates[bindingIndex].controlStartIndex;
				for (int j = 0; j < controlCount; j++)
				{
					if (this.controls[controlStartIndex + j].device == device)
					{
						usesControlFromDevice = true;
						break;
					}
				}
			}
			return usesControlFromDevice;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000C62C File Offset: 0x0000A82C
		public unsafe void ResetActionState(int actionIndex, InputActionPhase toPhase = InputActionPhase.Waiting, bool hardReset = false)
		{
			InputActionState.TriggerState* actionState = this.actionStates + actionIndex;
			if (actionState->phase != InputActionPhase.Waiting && actionState->phase != InputActionPhase.Disabled)
			{
				actionState->time = InputState.currentTime;
				if (actionState->interactionIndex != -1)
				{
					int bindingIndex = actionState->bindingIndex;
					if (bindingIndex != -1)
					{
						int mapIndex = actionState->mapIndex;
						int interactionCount = this.bindingStates[bindingIndex].interactionCount;
						int interactionStartIndex = this.bindingStates[bindingIndex].interactionStartIndex;
						for (int i = 0; i < interactionCount; i++)
						{
							int interactionIndex = interactionStartIndex + i;
							this.ResetInteractionStateAndCancelIfNecessary(mapIndex, bindingIndex, interactionIndex, toPhase);
						}
					}
				}
				else if (actionState->phase != InputActionPhase.Canceled)
				{
					this.ChangePhaseOfAction(InputActionPhase.Canceled, ref this.actionStates[actionIndex], toPhase);
				}
			}
			actionState->phase = toPhase;
			actionState->controlIndex = -1;
			actionState->bindingIndex = (int)this.memory.actionBindingIndices[this.memory.actionBindingIndicesAndCounts[actionIndex]];
			actionState->interactionIndex = -1;
			actionState->startTime = 0.0;
			actionState->time = 0.0;
			actionState->hasMultipleConcurrentActuations = false;
			actionState->inProcessing = false;
			actionState->isPressed = false;
			if (hardReset)
			{
				actionState->lastCanceledInUpdate = 0U;
				actionState->lastPerformedInUpdate = 0U;
				actionState->lastCompletedInUpdate = 0U;
				actionState->pressedInUpdate = 0U;
				actionState->releasedInUpdate = 0U;
				actionState->frame = 0;
			}
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000C797 File Offset: 0x0000A997
		public unsafe ref InputActionState.TriggerState FetchActionState(InputAction action)
		{
			return ref this.actionStates[action.m_ActionIndexInState];
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000C7AE File Offset: 0x0000A9AE
		public unsafe InputActionState.ActionMapIndices FetchMapIndices(InputActionMap map)
		{
			return this.mapIndices[map.m_MapIndexInState];
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000C7CC File Offset: 0x0000A9CC
		public unsafe void EnableAllActions(InputActionMap map)
		{
			this.EnableControls(map);
			int mapIndex = map.m_MapIndexInState;
			int actionCount = this.mapIndices[mapIndex].actionCount;
			int actionStartIndex = this.mapIndices[mapIndex].actionStartIndex;
			for (int i = 0; i < actionCount; i++)
			{
				int actionIndex = actionStartIndex + i;
				InputActionState.TriggerState* actionState = this.actionStates + actionIndex;
				if (actionState->isDisabled)
				{
					actionState->phase = InputActionPhase.Waiting;
				}
				actionState->inProcessing = false;
			}
			map.m_EnabledActionsCount = actionCount;
			this.HookOnBeforeUpdate();
			if (map.m_SingletonAction != null)
			{
				InputActionState.NotifyListenersOfActionChange(InputActionChange.ActionEnabled, map.m_SingletonAction);
				return;
			}
			InputActionState.NotifyListenersOfActionChange(InputActionChange.ActionMapEnabled, map);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000C87C File Offset: 0x0000AA7C
		private unsafe void EnableControls(InputActionMap map)
		{
			int mapIndex = map.m_MapIndexInState;
			int controlCount = this.mapIndices[mapIndex].controlCount;
			int controlStartIndex = this.mapIndices[mapIndex].controlStartIndex;
			if (controlCount > 0)
			{
				this.EnableControls(mapIndex, controlStartIndex, controlCount);
			}
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000C8CC File Offset: 0x0000AACC
		public unsafe void EnableSingleAction(InputAction action)
		{
			this.EnableControls(action);
			int actionIndex = action.m_ActionIndexInState;
			this.actionStates[actionIndex].phase = InputActionPhase.Waiting;
			action.m_ActionMap.m_EnabledActionsCount++;
			this.HookOnBeforeUpdate();
			InputActionState.NotifyListenersOfActionChange(InputActionChange.ActionEnabled, action);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000C920 File Offset: 0x0000AB20
		private unsafe void EnableControls(InputAction action)
		{
			int actionIndex = action.m_ActionIndexInState;
			int mapIndex = action.m_ActionMap.m_MapIndexInState;
			int bindingStartIndex = this.mapIndices[mapIndex].bindingStartIndex;
			int bindingCount = this.mapIndices[mapIndex].bindingCount;
			InputActionState.BindingState* bindingStatesPtr = this.memory.bindingStates;
			for (int i = 0; i < bindingCount; i++)
			{
				int bindingIndex = bindingStartIndex + i;
				InputActionState.BindingState* bindingState = bindingStatesPtr + bindingIndex;
				if (bindingState->actionIndex == actionIndex && !bindingState->isPartOfComposite)
				{
					int controlCount = bindingState->controlCount;
					if (controlCount != 0)
					{
						this.EnableControls(mapIndex, bindingState->controlStartIndex, controlCount);
					}
				}
			}
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000C9D0 File Offset: 0x0000ABD0
		public unsafe void DisableAllActions(InputActionMap map)
		{
			this.DisableControls(map);
			int mapIndex = map.m_MapIndexInState;
			int actionStartIndex = this.mapIndices[mapIndex].actionStartIndex;
			int actionCount = this.mapIndices[mapIndex].actionCount;
			bool allActionsEnabled = map.m_EnabledActionsCount == actionCount;
			for (int i = 0; i < actionCount; i++)
			{
				int actionIndex = actionStartIndex + i;
				if (this.actionStates[actionIndex].phase != InputActionPhase.Disabled)
				{
					this.ResetActionState(actionIndex, InputActionPhase.Disabled, false);
					if (!allActionsEnabled)
					{
						InputActionState.NotifyListenersOfActionChange(InputActionChange.ActionDisabled, map.m_Actions[i]);
					}
				}
			}
			map.m_EnabledActionsCount = 0;
			if (map.m_SingletonAction != null)
			{
				InputActionState.NotifyListenersOfActionChange(InputActionChange.ActionDisabled, map.m_SingletonAction);
				return;
			}
			if (allActionsEnabled)
			{
				InputActionState.NotifyListenersOfActionChange(InputActionChange.ActionMapDisabled, map);
			}
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000CA94 File Offset: 0x0000AC94
		public unsafe void DisableControls(InputActionMap map)
		{
			int mapIndex = map.m_MapIndexInState;
			int controlCount = this.mapIndices[mapIndex].controlCount;
			int controlStartIndex = this.mapIndices[mapIndex].controlStartIndex;
			if (controlCount > 0)
			{
				this.DisableControls(mapIndex, controlStartIndex, controlCount);
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000CAE1 File Offset: 0x0000ACE1
		public void DisableSingleAction(InputAction action)
		{
			this.DisableControls(action);
			this.ResetActionState(action.m_ActionIndexInState, InputActionPhase.Disabled, false);
			action.m_ActionMap.m_EnabledActionsCount--;
			InputActionState.NotifyListenersOfActionChange(InputActionChange.ActionDisabled, action);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000CB14 File Offset: 0x0000AD14
		private unsafe void DisableControls(InputAction action)
		{
			int actionIndex = action.m_ActionIndexInState;
			int mapIndex = action.m_ActionMap.m_MapIndexInState;
			int bindingStartIndex = this.mapIndices[mapIndex].bindingStartIndex;
			int bindingCount = this.mapIndices[mapIndex].bindingCount;
			InputActionState.BindingState* bindingStatesPtr = this.memory.bindingStates;
			for (int i = 0; i < bindingCount; i++)
			{
				int bindingIndex = bindingStartIndex + i;
				InputActionState.BindingState* bindingState = bindingStatesPtr + bindingIndex;
				if (bindingState->actionIndex == actionIndex && !bindingState->isPartOfComposite)
				{
					int controlCount = bindingState->controlCount;
					if (controlCount != 0)
					{
						this.DisableControls(mapIndex, bindingState->controlStartIndex, controlCount);
					}
				}
			}
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000CBC4 File Offset: 0x0000ADC4
		private unsafe void EnableControls(int mapIndex, int controlStartIndex, int numControls)
		{
			InputManager manager = InputSystem.s_Manager;
			for (int i = 0; i < numControls; i++)
			{
				int controlIndex = controlStartIndex + i;
				if (!this.IsControlEnabled(controlIndex))
				{
					int bindingIndex = this.controlIndexToBindingIndex[controlIndex];
					long mapControlAndBindingIndex = this.ToCombinedMapAndControlAndBindingIndex(mapIndex, controlIndex, bindingIndex);
					InputActionState.BindingState* bindingStatePtr = this.bindingStates + bindingIndex;
					if (bindingStatePtr->wantsInitialStateCheck)
					{
						this.SetInitialStateCheckPending(bindingStatePtr, true);
					}
					manager.AddStateChangeMonitor(this.controls[controlIndex], this, mapControlAndBindingIndex, (uint)this.controlGroupingAndComplexity[controlIndex * 2]);
					this.SetControlEnabled(controlIndex, true);
				}
			}
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000CC54 File Offset: 0x0000AE54
		private unsafe void DisableControls(int mapIndex, int controlStartIndex, int numControls)
		{
			InputManager manager = InputSystem.s_Manager;
			for (int i = 0; i < numControls; i++)
			{
				int controlIndex = controlStartIndex + i;
				if (this.IsControlEnabled(controlIndex))
				{
					int bindingIndex = this.controlIndexToBindingIndex[controlIndex];
					long mapControlAndBindingIndex = this.ToCombinedMapAndControlAndBindingIndex(mapIndex, controlIndex, bindingIndex);
					InputActionState.BindingState* bindingStatePtr = this.bindingStates + bindingIndex;
					if (bindingStatePtr->wantsInitialStateCheck)
					{
						this.SetInitialStateCheckPending(bindingStatePtr, false);
					}
					manager.RemoveStateChangeMonitor(this.controls[controlIndex], this, mapControlAndBindingIndex);
					bindingStatePtr->pressTime = 0.0;
					this.SetControlEnabled(controlIndex, false);
				}
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000CCE8 File Offset: 0x0000AEE8
		public unsafe void SetInitialStateCheckPending(int actionIndex, bool value = true)
		{
			int mapIndex = this.actionStates[actionIndex].mapIndex;
			int bindingStartIndex = this.mapIndices[mapIndex].bindingStartIndex;
			int bindingCount = this.mapIndices[mapIndex].bindingCount;
			for (int i = 0; i < bindingCount; i++)
			{
				ref InputActionState.BindingState bindingState = ref this.bindingStates[bindingStartIndex + i];
				if (bindingState.actionIndex == actionIndex && !bindingState.isPartOfComposite)
				{
					bindingState.initialStateCheckPending = value;
				}
			}
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000CD74 File Offset: 0x0000AF74
		private unsafe void SetInitialStateCheckPending(InputActionState.BindingState* bindingStatePtr, bool value)
		{
			if (bindingStatePtr->isPartOfComposite)
			{
				int compositeIndex = bindingStatePtr->compositeOrCompositeBindingIndex;
				this.bindingStates[compositeIndex].initialStateCheckPending = value;
				return;
			}
			bindingStatePtr->initialStateCheckPending = value;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000CDB0 File Offset: 0x0000AFB0
		private unsafe bool IsControlEnabled(int controlIndex)
		{
			int intIndex = controlIndex / 32;
			uint mask = 1U << controlIndex % 32;
			return (this.enabledControls[intIndex] & mask) > 0U;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000CDE0 File Offset: 0x0000AFE0
		private unsafe void SetControlEnabled(int controlIndex, bool state)
		{
			int intIndex = controlIndex / 32;
			uint mask = 1U << controlIndex % 32;
			if (state)
			{
				this.enabledControls[intIndex] |= mask;
				return;
			}
			this.enabledControls[intIndex] &= ~mask;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000CE21 File Offset: 0x0000B021
		private void HookOnBeforeUpdate()
		{
			if (this.m_OnBeforeUpdateHooked)
			{
				return;
			}
			if (this.m_OnBeforeUpdateDelegate == null)
			{
				this.m_OnBeforeUpdateDelegate = new Action(this.OnBeforeInitialUpdate);
			}
			InputSystem.s_Manager.onBeforeUpdate += this.m_OnBeforeUpdateDelegate;
			this.m_OnBeforeUpdateHooked = true;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000CE5D File Offset: 0x0000B05D
		private void UnhookOnBeforeUpdate()
		{
			if (!this.m_OnBeforeUpdateHooked)
			{
				return;
			}
			InputSystem.s_Manager.onBeforeUpdate -= this.m_OnBeforeUpdateDelegate;
			this.m_OnBeforeUpdateHooked = false;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000CE80 File Offset: 0x0000B080
		private unsafe void OnBeforeInitialUpdate()
		{
			if (InputState.currentUpdateType == InputUpdateType.BeforeRender)
			{
				return;
			}
			this.UnhookOnBeforeUpdate();
			double time = InputState.currentTime;
			InputManager manager = InputSystem.s_Manager;
			for (int bindingIndex = 0; bindingIndex < this.totalBindingCount; bindingIndex++)
			{
				ref InputActionState.BindingState bindingState = ref this.bindingStates[bindingIndex];
				if (bindingState.initialStateCheckPending)
				{
					bindingState.initialStateCheckPending = false;
					int controlStartIndex = bindingState.controlStartIndex;
					int controlCount = bindingState.controlCount;
					bool isComposite = bindingState.isComposite;
					bool didFindControlToSignal = false;
					for (int i = 0; i < controlCount; i++)
					{
						int controlIndex = controlStartIndex + i;
						InputControl control = this.controls[controlIndex];
						if (!this.IsActiveControl(bindingIndex, controlIndex) && !control.CheckStateIsAtDefault())
						{
							if (control.IsValueConsideredPressed(control.magnitude) && (bindingState.pressTime == 0.0 || bindingState.pressTime > time))
							{
								bindingState.pressTime = time;
							}
							if (!isComposite || !didFindControlToSignal)
							{
								manager.SignalStateChangeMonitor(control, this);
								didFindControlToSignal = true;
							}
						}
					}
				}
			}
			manager.FireStateChangeNotifications();
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000CF80 File Offset: 0x0000B180
		void IInputStateChangeMonitor.NotifyControlStateChanged(InputControl control, double time, InputEventPtr eventPtr, long mapControlAndBindingIndex)
		{
			int mapIndex;
			int controlIndex;
			int bindingIndex;
			this.SplitUpMapAndControlAndBindingIndex(mapControlAndBindingIndex, out mapIndex, out controlIndex, out bindingIndex);
			this.ProcessControlStateChange(mapIndex, controlIndex, bindingIndex, time, eventPtr);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000CFA8 File Offset: 0x0000B1A8
		void IInputStateChangeMonitor.NotifyTimerExpired(InputControl control, double time, long mapControlAndBindingIndex, int interactionIndex)
		{
			int mapIndex;
			int controlIndex;
			int bindingIndex;
			this.SplitUpMapAndControlAndBindingIndex(mapControlAndBindingIndex, out mapIndex, out controlIndex, out bindingIndex);
			this.ProcessTimeout(time, mapIndex, controlIndex, bindingIndex, interactionIndex);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000CFD0 File Offset: 0x0000B1D0
		private unsafe long ToCombinedMapAndControlAndBindingIndex(int mapIndex, int controlIndex, int bindingIndex)
		{
			ushort complexity = this.controlGroupingAndComplexity[controlIndex * 2 + 1];
			return (long)controlIndex | ((long)bindingIndex << 24) | ((long)mapIndex << 40) | (long)((long)((ulong)complexity) << 48);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000D002 File Offset: 0x0000B202
		private void SplitUpMapAndControlAndBindingIndex(long mapControlAndBindingIndex, out int mapIndex, out int controlIndex, out int bindingIndex)
		{
			controlIndex = (int)(mapControlAndBindingIndex & 16777215L);
			bindingIndex = (int)((mapControlAndBindingIndex >> 24) & 65535L);
			mapIndex = (int)((mapControlAndBindingIndex >> 40) & 255L);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000D02C File Offset: 0x0000B22C
		internal static int GetComplexityFromMonitorIndex(long mapControlAndBindingIndex)
		{
			return (int)((mapControlAndBindingIndex >> 48) & 255L);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000D03C File Offset: 0x0000B23C
		private unsafe void ProcessControlStateChange(int mapIndex, int controlIndex, int bindingIndex, double time, InputEventPtr eventPtr)
		{
			using (InputActionRebindingExtensions.DeferBindingResolution())
			{
				this.m_InProcessControlStateChange = true;
				this.m_CurrentlyProcessingThisEvent = eventPtr;
				try
				{
					InputActionState.BindingState* bindingStatePtr = this.bindingStates + bindingIndex;
					int actionIndex = bindingStatePtr->actionIndex;
					InputActionState.TriggerState trigger = new InputActionState.TriggerState
					{
						mapIndex = mapIndex,
						controlIndex = controlIndex,
						bindingIndex = bindingIndex,
						interactionIndex = -1,
						time = time,
						startTime = time,
						isPassThrough = (actionIndex != -1 && this.actionStates[actionIndex].isPassThrough),
						isButton = (actionIndex != -1 && this.actionStates[actionIndex].isButton)
					};
					if (this.m_OnBeforeUpdateHooked)
					{
						bindingStatePtr->initialStateCheckPending = false;
					}
					InputControl control = this.controls[controlIndex];
					trigger.magnitude = (control.CheckStateIsAtDefault() ? 0f : control.magnitude);
					this.controlMagnitudes[controlIndex] = trigger.magnitude;
					if (control.IsValueConsideredPressed(trigger.magnitude) && (bindingStatePtr->pressTime == 0.0 || bindingStatePtr->pressTime > trigger.time))
					{
						bindingStatePtr->pressTime = trigger.time;
					}
					bool haveInteractionsOnComposite = false;
					bool compositeAlreadyTriggered = false;
					if (bindingStatePtr->isPartOfComposite)
					{
						int compositeBindingIndex = bindingStatePtr->compositeOrCompositeBindingIndex;
						InputActionState.BindingState* compositeBindingPtr = this.bindingStates + compositeBindingIndex;
						if (!InputActionState.ShouldIgnoreInputOnCompositeBinding(compositeBindingPtr, eventPtr))
						{
							int compositeIndex = this.bindingStates[compositeBindingIndex].compositeOrCompositeBindingIndex;
							InputBindingCompositeContext compositeContext = new InputBindingCompositeContext
							{
								m_State = this,
								m_BindingIndex = compositeBindingIndex
							};
							trigger.magnitude = this.composites[compositeIndex].EvaluateMagnitude(ref compositeContext);
							this.memory.compositeMagnitudes[compositeIndex] = trigger.magnitude;
							int interactionCountOnComposite = compositeBindingPtr->interactionCount;
							if (interactionCountOnComposite > 0)
							{
								haveInteractionsOnComposite = true;
								this.ProcessInteractions(ref trigger, compositeBindingPtr->interactionStartIndex, interactionCountOnComposite);
							}
						}
						else
						{
							compositeAlreadyTriggered = true;
						}
					}
					bool isConflictingInput = false;
					if (!compositeAlreadyTriggered)
					{
						isConflictingInput = this.IsConflictingInput(ref trigger, actionIndex);
						bindingStatePtr = this.bindingStates + trigger.bindingIndex;
					}
					if (!isConflictingInput)
					{
						this.ProcessButtonState(ref trigger, actionIndex, bindingStatePtr);
					}
					int interactionCount = bindingStatePtr->interactionCount;
					if (interactionCount > 0 && !bindingStatePtr->isPartOfComposite)
					{
						this.ProcessInteractions(ref trigger, bindingStatePtr->interactionStartIndex, interactionCount);
					}
					else if (!haveInteractionsOnComposite && !isConflictingInput && !compositeAlreadyTriggered)
					{
						this.ProcessDefaultInteraction(ref trigger, actionIndex);
					}
				}
				finally
				{
					this.m_InProcessControlStateChange = false;
					this.m_CurrentlyProcessingThisEvent = default(InputEventPtr);
				}
			}
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000D318 File Offset: 0x0000B518
		private unsafe void ProcessButtonState(ref InputActionState.TriggerState trigger, int actionIndex, InputActionState.BindingState* bindingStatePtr)
		{
			InputControl control = this.controls[trigger.controlIndex];
			float pressPoint = (control.isButton ? ((ButtonControl)control).pressPointOrDefault : ButtonControl.s_GlobalDefaultButtonPressPoint);
			if (this.controlMagnitudes[trigger.controlIndex] <= pressPoint * ButtonControl.s_GlobalDefaultButtonReleaseThreshold)
			{
				bindingStatePtr->pressTime = 0.0;
			}
			float actuation = trigger.magnitude;
			InputActionState.TriggerState* actionState = this.actionStates + actionIndex;
			if (!actionState->isPressed && actuation >= pressPoint)
			{
				actionState->pressedInUpdate = InputUpdate.s_UpdateStepCount;
				actionState->isPressed = true;
				actionState->frame = Time.frameCount;
				return;
			}
			if (actionState->isPressed)
			{
				float releasePoint = pressPoint * ButtonControl.s_GlobalDefaultButtonReleaseThreshold;
				if (actuation <= releasePoint)
				{
					actionState->releasedInUpdate = InputUpdate.s_UpdateStepCount;
					actionState->isPressed = false;
					actionState->frame = Time.frameCount;
				}
			}
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000D3F0 File Offset: 0x0000B5F0
		private unsafe static bool ShouldIgnoreInputOnCompositeBinding(InputActionState.BindingState* binding, InputEvent* eventPtr)
		{
			if (eventPtr == null)
			{
				return false;
			}
			int eventId = eventPtr->eventId;
			if (eventId != 0 && binding->triggerEventIdForComposite == eventId)
			{
				return true;
			}
			binding->triggerEventIdForComposite = eventId;
			return false;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000D424 File Offset: 0x0000B624
		private unsafe bool IsConflictingInput(ref InputActionState.TriggerState trigger, int actionIndex)
		{
			InputActionState.TriggerState* actionState = this.actionStates + actionIndex;
			if (!actionState->mayNeedConflictResolution)
			{
				return false;
			}
			int triggerControlIndex = trigger.controlIndex;
			if (this.bindingStates[trigger.bindingIndex].isPartOfComposite)
			{
				int compositeBindingIndex = this.bindingStates[trigger.bindingIndex].compositeOrCompositeBindingIndex;
				triggerControlIndex = this.bindingStates[compositeBindingIndex].controlStartIndex;
			}
			int actionStateControlIndex = actionState->controlIndex;
			if (this.bindingStates[actionState->bindingIndex].isPartOfComposite)
			{
				int compositeBindingIndex2 = this.bindingStates[actionState->bindingIndex].compositeOrCompositeBindingIndex;
				actionStateControlIndex = this.bindingStates[compositeBindingIndex2].controlStartIndex;
			}
			if (actionStateControlIndex == -1)
			{
				actionState->magnitude = trigger.magnitude;
				return false;
			}
			bool isControlCurrentlyDrivingTheAction = triggerControlIndex == actionStateControlIndex || this.controls[triggerControlIndex] == this.controls[actionStateControlIndex];
			if (trigger.magnitude > actionState->magnitude)
			{
				if (trigger.magnitude > 0f && !isControlCurrentlyDrivingTheAction && actionState->magnitude > 0f)
				{
					actionState->hasMultipleConcurrentActuations = true;
				}
				actionState->magnitude = trigger.magnitude;
				return false;
			}
			if (trigger.magnitude < actionState->magnitude)
			{
				if (!isControlCurrentlyDrivingTheAction)
				{
					if (trigger.magnitude > 0f)
					{
						actionState->hasMultipleConcurrentActuations = true;
					}
					return true;
				}
				if (!actionState->hasMultipleConcurrentActuations)
				{
					actionState->magnitude = trigger.magnitude;
					return false;
				}
				ushort bindingCount;
				ushort bindingStartIndex = this.GetActionBindingStartIndexAndCount(actionIndex, out bindingCount);
				float highestActuationLevel = trigger.magnitude;
				int controlWithHighestActuation = -1;
				int bindingWithHighestActuation = -1;
				int numActuations = 0;
				for (int i = 0; i < (int)bindingCount; i++)
				{
					ushort bindingIndex = this.memory.actionBindingIndices[(int)bindingStartIndex + i];
					InputActionState.BindingState* binding = this.memory.bindingStates + bindingIndex;
					if (binding->isComposite)
					{
						int firstControlIndex = binding->controlStartIndex;
						int compositeIndex = binding->compositeOrCompositeBindingIndex;
						float magnitude = this.memory.compositeMagnitudes[compositeIndex];
						if (magnitude > 0f)
						{
							numActuations++;
						}
						if (magnitude > highestActuationLevel)
						{
							controlWithHighestActuation = firstControlIndex;
							bindingWithHighestActuation = this.controlIndexToBindingIndex[firstControlIndex];
							highestActuationLevel = magnitude;
						}
					}
					else if (!binding->isPartOfComposite)
					{
						for (int j = 0; j < binding->controlCount; j++)
						{
							int controlIndex = binding->controlStartIndex + j;
							float magnitude2 = this.memory.controlMagnitudes[controlIndex];
							if (magnitude2 > 0f)
							{
								numActuations++;
							}
							if (magnitude2 > highestActuationLevel)
							{
								controlWithHighestActuation = controlIndex;
								bindingWithHighestActuation = (int)bindingIndex;
								highestActuationLevel = magnitude2;
							}
						}
					}
				}
				if (numActuations <= 1)
				{
					actionState->hasMultipleConcurrentActuations = false;
				}
				if (controlWithHighestActuation != -1)
				{
					trigger.controlIndex = controlWithHighestActuation;
					trigger.bindingIndex = bindingWithHighestActuation;
					trigger.magnitude = highestActuationLevel;
					if (actionState->bindingIndex != bindingWithHighestActuation)
					{
						if (actionState->interactionIndex != -1)
						{
							this.ResetInteractionState(actionState->interactionIndex);
						}
						InputActionState.BindingState* ptr = this.bindingStates + bindingWithHighestActuation;
						int interactionCount = ptr->interactionCount;
						int interactionStartIndex = ptr->interactionStartIndex;
						for (int k = 0; k < interactionCount; k++)
						{
							if (this.interactionStates[interactionStartIndex + k].phase.IsInProgress())
							{
								actionState->interactionIndex = interactionStartIndex + k;
								trigger.interactionIndex = interactionStartIndex + k;
								break;
							}
						}
					}
					actionState->controlIndex = controlWithHighestActuation;
					actionState->bindingIndex = bindingWithHighestActuation;
					actionState->magnitude = highestActuationLevel;
					return false;
				}
			}
			if (!isControlCurrentlyDrivingTheAction && Mathf.Approximately(trigger.magnitude, actionState->magnitude))
			{
				if (trigger.magnitude > 0f)
				{
					actionState->hasMultipleConcurrentActuations = true;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000D7C1 File Offset: 0x0000B9C1
		private unsafe ushort GetActionBindingStartIndexAndCount(int actionIndex, out ushort bindingCount)
		{
			bindingCount = this.memory.actionBindingIndicesAndCounts[actionIndex * 2 + 1];
			return this.memory.actionBindingIndicesAndCounts[actionIndex * 2];
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000D7F0 File Offset: 0x0000B9F0
		private unsafe void ProcessDefaultInteraction(ref InputActionState.TriggerState trigger, int actionIndex)
		{
			InputActionState.TriggerState* actionState = this.actionStates + actionIndex;
			switch (actionState->phase)
			{
			case InputActionPhase.Waiting:
				if (trigger.isPassThrough)
				{
					this.ChangePhaseOfAction(InputActionPhase.Performed, ref trigger, InputActionPhase.Waiting);
					return;
				}
				if (trigger.isButton)
				{
					float magnitude = trigger.magnitude;
					if (magnitude > 0f)
					{
						this.ChangePhaseOfAction(InputActionPhase.Started, ref trigger, InputActionPhase.Waiting);
					}
					ButtonControl button = this.controls[trigger.controlIndex] as ButtonControl;
					float threshold = ((button != null) ? button.pressPointOrDefault : ButtonControl.s_GlobalDefaultButtonPressPoint);
					if (magnitude >= threshold)
					{
						this.ChangePhaseOfAction(InputActionPhase.Performed, ref trigger, InputActionPhase.Performed);
						return;
					}
				}
				else if (InputActionState.IsActuated(ref trigger, 0f))
				{
					this.ChangePhaseOfAction(InputActionPhase.Started, ref trigger, InputActionPhase.Waiting);
					this.ChangePhaseOfAction(InputActionPhase.Performed, ref trigger, InputActionPhase.Started);
					return;
				}
				break;
			case InputActionPhase.Started:
				if (actionState->isButton)
				{
					float actuation = trigger.magnitude;
					ButtonControl button2 = this.controls[trigger.controlIndex] as ButtonControl;
					float threshold2 = ((button2 != null) ? button2.pressPointOrDefault : ButtonControl.s_GlobalDefaultButtonPressPoint);
					if (actuation >= threshold2)
					{
						this.ChangePhaseOfAction(InputActionPhase.Performed, ref trigger, InputActionPhase.Performed);
						return;
					}
					if (Mathf.Approximately(actuation, 0f))
					{
						this.ChangePhaseOfAction(InputActionPhase.Canceled, ref trigger, InputActionPhase.Waiting);
						return;
					}
				}
				else
				{
					if (!InputActionState.IsActuated(ref trigger, 0f))
					{
						this.ChangePhaseOfAction(InputActionPhase.Canceled, ref trigger, InputActionPhase.Waiting);
						return;
					}
					this.ChangePhaseOfAction(InputActionPhase.Performed, ref trigger, InputActionPhase.Started);
					return;
				}
				break;
			case InputActionPhase.Performed:
				if (actionState->isButton)
				{
					float actuation2 = trigger.magnitude;
					ButtonControl button3 = this.controls[trigger.controlIndex] as ButtonControl;
					float pressPoint = ((button3 != null) ? button3.pressPointOrDefault : ButtonControl.s_GlobalDefaultButtonPressPoint);
					if (Mathf.Approximately(0f, actuation2))
					{
						this.ChangePhaseOfAction(InputActionPhase.Canceled, ref trigger, InputActionPhase.Waiting);
						return;
					}
					float threshold3 = pressPoint * ButtonControl.s_GlobalDefaultButtonReleaseThreshold;
					if (actuation2 <= threshold3)
					{
						this.ChangePhaseOfAction(InputActionPhase.Started, ref trigger, InputActionPhase.Waiting);
						return;
					}
				}
				else if (actionState->isPassThrough)
				{
					this.ChangePhaseOfAction(InputActionPhase.Performed, ref trigger, InputActionPhase.Performed);
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000D9C0 File Offset: 0x0000BBC0
		private unsafe void ProcessInteractions(ref InputActionState.TriggerState trigger, int interactionStartIndex, int interactionCount)
		{
			InputInteractionContext context = new InputInteractionContext
			{
				m_State = this,
				m_TriggerState = trigger
			};
			for (int i = 0; i < interactionCount; i++)
			{
				int index = interactionStartIndex + i;
				InputActionState.InteractionState state = this.interactionStates[index];
				IInputInteraction inputInteraction = this.interactions[index];
				context.m_TriggerState.phase = state.phase;
				context.m_TriggerState.startTime = state.startTime;
				context.m_TriggerState.interactionIndex = index;
				inputInteraction.Process(ref context);
			}
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000DA58 File Offset: 0x0000BC58
		private unsafe void ProcessTimeout(double time, int mapIndex, int controlIndex, int bindingIndex, int interactionIndex)
		{
			ref InputActionState.InteractionState currentState = ref this.interactionStates[interactionIndex];
			InputInteractionContext context = new InputInteractionContext
			{
				m_State = this,
				m_TriggerState = new InputActionState.TriggerState
				{
					phase = currentState.phase,
					time = time,
					mapIndex = mapIndex,
					controlIndex = controlIndex,
					bindingIndex = bindingIndex,
					interactionIndex = interactionIndex,
					startTime = currentState.startTime
				},
				timerHasExpired = true
			};
			currentState.isTimerRunning = false;
			currentState.totalTimeoutCompletionTimeRemaining = Mathf.Max(currentState.totalTimeoutCompletionTimeRemaining - currentState.timerDuration, 0f);
			currentState.timerDuration = 0f;
			this.interactions[interactionIndex].Process(ref context);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000DB24 File Offset: 0x0000BD24
		internal unsafe void SetTotalTimeoutCompletionTime(float seconds, ref InputActionState.TriggerState trigger)
		{
			InputActionState.InteractionState* ptr = this.interactionStates + trigger.interactionIndex;
			ptr->totalTimeoutCompletionDone = 0f;
			ptr->totalTimeoutCompletionTimeRemaining = seconds;
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000DB4C File Offset: 0x0000BD4C
		internal unsafe void StartTimeout(float seconds, ref InputActionState.TriggerState trigger)
		{
			InputManager manager = InputSystem.s_Manager;
			double currentTime = trigger.time;
			InputControl control = this.controls[trigger.controlIndex];
			int interactionIndex = trigger.interactionIndex;
			long monitorIndex = this.ToCombinedMapAndControlAndBindingIndex(trigger.mapIndex, trigger.controlIndex, trigger.bindingIndex);
			InputActionState.InteractionState* ptr = this.interactionStates + interactionIndex;
			if (ptr->isTimerRunning)
			{
				this.StopTimeout(interactionIndex);
			}
			manager.AddStateChangeMonitorTimeout(control, this, currentTime + (double)seconds, monitorIndex, interactionIndex);
			ptr->isTimerRunning = true;
			ptr->timerStartTime = currentTime;
			ptr->timerDuration = seconds;
			ptr->timerMonitorIndex = monitorIndex;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000DBE0 File Offset: 0x0000BDE0
		private unsafe void StopTimeout(int interactionIndex)
		{
			ref InputActionState.InteractionState interactionState = ref this.interactionStates[interactionIndex];
			InputSystem.s_Manager.RemoveStateChangeMonitorTimeout(this, interactionState.timerMonitorIndex, interactionIndex);
			interactionState.isTimerRunning = false;
			interactionState.totalTimeoutCompletionDone += interactionState.timerDuration;
			interactionState.totalTimeoutCompletionTimeRemaining = Mathf.Max(interactionState.totalTimeoutCompletionTimeRemaining - interactionState.timerDuration, 0f);
			interactionState.timerDuration = 0f;
			interactionState.timerStartTime = 0.0;
			interactionState.timerMonitorIndex = 0L;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000DC6C File Offset: 0x0000BE6C
		internal unsafe void ChangePhaseOfInteraction(InputActionPhase newPhase, ref InputActionState.TriggerState trigger, InputActionPhase phaseAfterPerformed = InputActionPhase.Waiting, InputActionPhase phaseAfterCanceled = InputActionPhase.Waiting, bool processNextInteractionOnCancel = true)
		{
			int interactionIndex = trigger.interactionIndex;
			int bindingIndex = trigger.bindingIndex;
			InputActionPhase phaseAfterPerformedOrCanceled = InputActionPhase.Waiting;
			if (newPhase == InputActionPhase.Performed)
			{
				phaseAfterPerformedOrCanceled = phaseAfterPerformed;
			}
			else if (newPhase == InputActionPhase.Canceled)
			{
				phaseAfterPerformedOrCanceled = phaseAfterCanceled;
			}
			ref InputActionState.InteractionState interactionState = ref this.interactionStates[interactionIndex];
			if (interactionState.isTimerRunning)
			{
				this.StopTimeout(trigger.interactionIndex);
			}
			interactionState.phase = newPhase;
			interactionState.triggerControlIndex = trigger.controlIndex;
			interactionState.startTime = trigger.startTime;
			if (newPhase == InputActionPhase.Performed)
			{
				interactionState.performedTime = trigger.time;
			}
			int actionIndex = this.bindingStates[bindingIndex].actionIndex;
			if (actionIndex != -1)
			{
				if (this.actionStates[actionIndex].phase == InputActionPhase.Waiting)
				{
					if (!this.ChangePhaseOfAction(newPhase, ref trigger, phaseAfterPerformedOrCanceled))
					{
						return;
					}
				}
				else if (newPhase == InputActionPhase.Canceled && this.actionStates[actionIndex].interactionIndex == trigger.interactionIndex)
				{
					if (!this.ChangePhaseOfAction(newPhase, ref trigger, phaseAfterPerformedOrCanceled))
					{
						return;
					}
					if (!processNextInteractionOnCancel)
					{
						return;
					}
					int interactionStartIndex = this.bindingStates[bindingIndex].interactionStartIndex;
					int numInteractions = this.bindingStates[bindingIndex].interactionCount;
					int i = 0;
					while (i < numInteractions)
					{
						int index = interactionStartIndex + i;
						if (index != trigger.interactionIndex && (this.interactionStates[index].phase == InputActionPhase.Started || this.interactionStates[index].phase == InputActionPhase.Performed))
						{
							double startTime = this.interactionStates[index].startTime;
							InputActionState.TriggerState triggerForInteraction = new InputActionState.TriggerState
							{
								phase = InputActionPhase.Started,
								controlIndex = this.interactionStates[index].triggerControlIndex,
								bindingIndex = trigger.bindingIndex,
								interactionIndex = index,
								mapIndex = trigger.mapIndex,
								time = startTime,
								startTime = startTime
							};
							if (!this.ChangePhaseOfAction(InputActionPhase.Started, ref triggerForInteraction, phaseAfterPerformedOrCanceled))
							{
								return;
							}
							if (this.interactionStates[index].phase != InputActionPhase.Performed)
							{
								break;
							}
							triggerForInteraction = new InputActionState.TriggerState
							{
								phase = InputActionPhase.Performed,
								controlIndex = this.interactionStates[index].triggerControlIndex,
								bindingIndex = trigger.bindingIndex,
								interactionIndex = index,
								mapIndex = trigger.mapIndex,
								time = this.interactionStates[index].performedTime,
								startTime = startTime
							};
							if (!this.ChangePhaseOfAction(InputActionPhase.Performed, ref triggerForInteraction, phaseAfterPerformedOrCanceled))
							{
								return;
							}
							while (i < numInteractions)
							{
								index = interactionStartIndex + i;
								this.ResetInteractionState(index);
								i++;
							}
							break;
						}
						else
						{
							i++;
						}
					}
				}
				else if (this.actionStates[actionIndex].interactionIndex == trigger.interactionIndex)
				{
					if (!this.ChangePhaseOfAction(newPhase, ref trigger, phaseAfterPerformedOrCanceled))
					{
						return;
					}
					if (newPhase == InputActionPhase.Performed)
					{
						int interactionStartIndex2 = this.bindingStates[bindingIndex].interactionStartIndex;
						int numInteractions2 = this.bindingStates[bindingIndex].interactionCount;
						for (int j = 0; j < numInteractions2; j++)
						{
							int index2 = interactionStartIndex2 + j;
							if (index2 != trigger.interactionIndex)
							{
								this.ResetInteractionState(index2);
							}
						}
					}
				}
			}
			if (newPhase != InputActionPhase.Performed || actionIndex == -1 || this.actionStates[actionIndex].isPerformed || this.actionStates[actionIndex].interactionIndex == trigger.interactionIndex)
			{
				if (newPhase == InputActionPhase.Performed && phaseAfterPerformed != InputActionPhase.Waiting)
				{
					interactionState.phase = phaseAfterPerformed;
					return;
				}
				if (newPhase == InputActionPhase.Performed || newPhase == InputActionPhase.Canceled)
				{
					this.ResetInteractionState(trigger.interactionIndex);
				}
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000E038 File Offset: 0x0000C238
		private unsafe bool ChangePhaseOfAction(InputActionPhase newPhase, ref InputActionState.TriggerState trigger, InputActionPhase phaseAfterPerformedOrCanceled = InputActionPhase.Waiting)
		{
			int actionIndex = this.bindingStates[trigger.bindingIndex].actionIndex;
			if (actionIndex == -1)
			{
				return true;
			}
			InputActionState.TriggerState* actionState = this.actionStates + actionIndex;
			if (actionState->isDisabled)
			{
				return true;
			}
			actionState->inProcessing = true;
			try
			{
				if (actionState->isPassThrough && trigger.interactionIndex == -1)
				{
					this.ChangePhaseOfActionInternal(actionIndex, actionState, newPhase, ref trigger, newPhase == InputActionPhase.Canceled && phaseAfterPerformedOrCanceled == InputActionPhase.Disabled);
					if (!actionState->inProcessing)
					{
						return false;
					}
				}
				else if (newPhase == InputActionPhase.Performed && actionState->phase == InputActionPhase.Waiting)
				{
					this.ChangePhaseOfActionInternal(actionIndex, actionState, InputActionPhase.Started, ref trigger, false);
					if (!actionState->inProcessing)
					{
						return false;
					}
					this.ChangePhaseOfActionInternal(actionIndex, actionState, newPhase, ref trigger, false);
					if (!actionState->inProcessing)
					{
						return false;
					}
					if (phaseAfterPerformedOrCanceled == InputActionPhase.Waiting)
					{
						this.ChangePhaseOfActionInternal(actionIndex, actionState, InputActionPhase.Canceled, ref trigger, false);
					}
					if (!actionState->inProcessing)
					{
						return false;
					}
					actionState->phase = phaseAfterPerformedOrCanceled;
				}
				else if (actionState->phase != newPhase || newPhase == InputActionPhase.Performed)
				{
					this.ChangePhaseOfActionInternal(actionIndex, actionState, newPhase, ref trigger, newPhase == InputActionPhase.Canceled && phaseAfterPerformedOrCanceled == InputActionPhase.Disabled);
					if (!actionState->inProcessing)
					{
						return false;
					}
					if (newPhase == InputActionPhase.Performed || newPhase == InputActionPhase.Canceled)
					{
						actionState->phase = phaseAfterPerformedOrCanceled;
					}
				}
			}
			finally
			{
				actionState->inProcessing = false;
			}
			if (actionState->phase == InputActionPhase.Waiting)
			{
				actionState->controlIndex = -1;
				actionState->flags &= ~InputActionState.TriggerState.Flags.HaveMagnitude;
			}
			return true;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000E1A0 File Offset: 0x0000C3A0
		private unsafe void ChangePhaseOfActionInternal(int actionIndex, InputActionState.TriggerState* actionState, InputActionPhase newPhase, ref InputActionState.TriggerState trigger, bool isDisablingAction = false)
		{
			InputActionState.TriggerState newState = trigger;
			newState.flags = actionState->flags;
			if (newPhase != InputActionPhase.Canceled)
			{
				newState.magnitude = trigger.magnitude;
			}
			else
			{
				newState.magnitude = 0f;
			}
			newState.phase = newPhase;
			newState.frame = Time.frameCount;
			if (newPhase == InputActionPhase.Performed)
			{
				newState.lastPerformedInUpdate = InputUpdate.s_UpdateStepCount;
				newState.lastCanceledInUpdate = actionState->lastCanceledInUpdate;
				if (this.controlGroupingAndComplexity[trigger.controlIndex * 2 + 1] > 1 && this.m_CurrentlyProcessingThisEvent.valid)
				{
					this.m_CurrentlyProcessingThisEvent.handled = true;
				}
			}
			else if (newPhase == InputActionPhase.Canceled)
			{
				newState.lastCanceledInUpdate = InputUpdate.s_UpdateStepCount;
				newState.lastPerformedInUpdate = actionState->lastPerformedInUpdate;
			}
			else
			{
				newState.lastPerformedInUpdate = actionState->lastPerformedInUpdate;
				newState.lastCanceledInUpdate = actionState->lastCanceledInUpdate;
			}
			if (actionState->phase == InputActionPhase.Performed && newPhase != InputActionPhase.Performed && !isDisablingAction)
			{
				newState.lastCompletedInUpdate = InputUpdate.s_UpdateStepCount;
			}
			else
			{
				newState.lastCompletedInUpdate = actionState->lastCompletedInUpdate;
			}
			newState.pressedInUpdate = actionState->pressedInUpdate;
			newState.releasedInUpdate = actionState->releasedInUpdate;
			if (newPhase == InputActionPhase.Started)
			{
				newState.startTime = newState.time;
			}
			*actionState = newState;
			InputActionMap map = this.maps[trigger.mapIndex];
			InputAction action = map.m_Actions[actionIndex - this.mapIndices[trigger.mapIndex].actionStartIndex];
			trigger.phase = newPhase;
			switch (newPhase)
			{
			case InputActionPhase.Started:
				this.CallActionListeners(actionIndex, map, newPhase, ref action.m_OnStarted, "started");
				return;
			case InputActionPhase.Performed:
				this.CallActionListeners(actionIndex, map, newPhase, ref action.m_OnPerformed, "performed");
				return;
			case InputActionPhase.Canceled:
				this.CallActionListeners(actionIndex, map, newPhase, ref action.m_OnCanceled, "canceled");
				return;
			default:
				return;
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000E370 File Offset: 0x0000C570
		private void CallActionListeners(int actionIndex, InputActionMap actionMap, InputActionPhase phase, ref CallbackArray<Action<InputAction.CallbackContext>> listeners, string callbackName)
		{
			CallbackArray<Action<InputAction.CallbackContext>> callbacksOnMap = actionMap.m_ActionCallbacks;
			if (listeners.length == 0 && callbacksOnMap.length == 0 && InputActionState.s_GlobalState.onActionChange.length == 0)
			{
				return;
			}
			InputAction.CallbackContext context = new InputAction.CallbackContext
			{
				m_State = this,
				m_ActionIndex = actionIndex
			};
			InputAction action = context.action;
			if (InputActionState.s_GlobalState.onActionChange.length > 0)
			{
				InputActionChange change;
				switch (phase)
				{
				case InputActionPhase.Started:
					change = InputActionChange.ActionStarted;
					break;
				case InputActionPhase.Performed:
					change = InputActionChange.ActionPerformed;
					break;
				case InputActionPhase.Canceled:
					change = InputActionChange.ActionCanceled;
					break;
				default:
					return;
				}
				DelegateHelpers.InvokeCallbacksSafe<object, InputActionChange>(ref InputActionState.s_GlobalState.onActionChange, action, change, InputActionState.k_InputOnActionChangeMarker, "InputSystem.onActionChange", null);
			}
			DelegateHelpers.InvokeCallbacksSafe<InputAction.CallbackContext>(ref listeners, context, callbackName, action);
			DelegateHelpers.InvokeCallbacksSafe<InputAction.CallbackContext>(ref callbacksOnMap, context, callbackName, actionMap);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000E434 File Offset: 0x0000C634
		private object GetActionOrNoneString(ref InputActionState.TriggerState trigger)
		{
			InputAction action = this.GetActionOrNull(ref trigger);
			if (action == null)
			{
				return "<none>";
			}
			return action;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000E454 File Offset: 0x0000C654
		internal unsafe InputAction GetActionOrNull(int bindingIndex)
		{
			int actionIndex = this.bindingStates[bindingIndex].actionIndex;
			if (actionIndex == -1)
			{
				return null;
			}
			int mapIndex = this.bindingStates[bindingIndex].mapIndex;
			int actionStartIndex = this.mapIndices[mapIndex].actionStartIndex;
			return this.maps[mapIndex].m_Actions[actionIndex - actionStartIndex];
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000E4BC File Offset: 0x0000C6BC
		internal unsafe InputAction GetActionOrNull(ref InputActionState.TriggerState trigger)
		{
			int actionIndex = this.bindingStates[trigger.bindingIndex].actionIndex;
			if (actionIndex == -1)
			{
				return null;
			}
			int actionStartIndex = this.mapIndices[trigger.mapIndex].actionStartIndex;
			return this.maps[trigger.mapIndex].m_Actions[actionIndex - actionStartIndex];
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000E51B File Offset: 0x0000C71B
		internal InputControl GetControl(ref InputActionState.TriggerState trigger)
		{
			return this.controls[trigger.controlIndex];
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000E52A File Offset: 0x0000C72A
		private IInputInteraction GetInteractionOrNull(ref InputActionState.TriggerState trigger)
		{
			if (trigger.interactionIndex == -1)
			{
				return null;
			}
			return this.interactions[trigger.interactionIndex];
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000E544 File Offset: 0x0000C744
		internal unsafe int GetBindingIndexInMap(int bindingIndex)
		{
			int mapIndex = this.bindingStates[bindingIndex].mapIndex;
			int bindingStartIndex = this.mapIndices[mapIndex].bindingStartIndex;
			return bindingIndex - bindingStartIndex;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000E580 File Offset: 0x0000C780
		internal unsafe int GetBindingIndexInState(int mapIndex, int bindingIndexInMap)
		{
			return this.mapIndices[mapIndex].bindingStartIndex + bindingIndexInMap;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000E599 File Offset: 0x0000C799
		internal unsafe ref InputActionState.BindingState GetBindingState(int bindingIndex)
		{
			return ref this.bindingStates[bindingIndex];
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000E5AC File Offset: 0x0000C7AC
		internal unsafe ref InputBinding GetBinding(int bindingIndex)
		{
			int mapIndex = this.bindingStates[bindingIndex].mapIndex;
			int bindingStartIndex = this.mapIndices[mapIndex].bindingStartIndex;
			return ref this.maps[mapIndex].m_Bindings[bindingIndex - bindingStartIndex];
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000E5FC File Offset: 0x0000C7FC
		internal unsafe InputActionMap GetActionMap(int bindingIndex)
		{
			int mapIndex = this.bindingStates[bindingIndex].mapIndex;
			return this.maps[mapIndex];
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000E628 File Offset: 0x0000C828
		private unsafe void ResetInteractionStateAndCancelIfNecessary(int mapIndex, int bindingIndex, int interactionIndex, InputActionPhase phaseAfterCanceled)
		{
			int actionIndex = this.bindingStates[bindingIndex].actionIndex;
			if (this.actionStates[actionIndex].interactionIndex == interactionIndex)
			{
				InputActionPhase phase = this.interactionStates[interactionIndex].phase;
				if (phase - InputActionPhase.Started <= 1)
				{
					this.ChangePhaseOfInteraction(InputActionPhase.Canceled, ref this.actionStates[actionIndex], InputActionPhase.Waiting, phaseAfterCanceled, false);
				}
				this.actionStates[actionIndex].interactionIndex = -1;
			}
			this.ResetInteractionState(interactionIndex);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000E6B8 File Offset: 0x0000C8B8
		private unsafe void ResetInteractionState(int interactionIndex)
		{
			this.interactions[interactionIndex].Reset();
			if (this.interactionStates[interactionIndex].isTimerRunning)
			{
				this.StopTimeout(interactionIndex);
			}
			this.interactionStates[interactionIndex] = new InputActionState.InteractionState
			{
				phase = InputActionPhase.Waiting,
				triggerControlIndex = -1
			};
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000E720 File Offset: 0x0000C920
		internal unsafe int GetValueSizeInBytes(int bindingIndex, int controlIndex)
		{
			if (this.bindingStates[bindingIndex].isPartOfComposite)
			{
				int compositeBindingIndex = this.bindingStates[bindingIndex].compositeOrCompositeBindingIndex;
				int compositeIndex = this.bindingStates[compositeBindingIndex].compositeOrCompositeBindingIndex;
				return this.composites[compositeIndex].valueSizeInBytes;
			}
			return this.controls[controlIndex].valueSizeInBytes;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000E78C File Offset: 0x0000C98C
		internal unsafe Type GetValueType(int bindingIndex, int controlIndex)
		{
			if (this.bindingStates[bindingIndex].isPartOfComposite)
			{
				int compositeBindingIndex = this.bindingStates[bindingIndex].compositeOrCompositeBindingIndex;
				int compositeIndex = this.bindingStates[compositeBindingIndex].compositeOrCompositeBindingIndex;
				return this.composites[compositeIndex].valueType;
			}
			return this.controls[controlIndex].valueType;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000E7F8 File Offset: 0x0000C9F8
		internal static bool IsActuated(ref InputActionState.TriggerState trigger, float threshold = 0f)
		{
			float magnitude = trigger.magnitude;
			if (magnitude < 0f)
			{
				return true;
			}
			if (Mathf.Approximately(threshold, 0f))
			{
				return magnitude > 0f;
			}
			return magnitude >= threshold;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000E834 File Offset: 0x0000CA34
		internal unsafe void ReadValue(int bindingIndex, int controlIndex, void* buffer, int bufferSize, bool ignoreComposites = false)
		{
			InputControl control = null;
			if (!ignoreComposites && this.bindingStates[bindingIndex].isPartOfComposite)
			{
				int compositeBindingIndex = this.bindingStates[bindingIndex].compositeOrCompositeBindingIndex;
				int compositeIndex = this.bindingStates[compositeBindingIndex].compositeOrCompositeBindingIndex;
				InputBindingComposite inputBindingComposite = this.composites[compositeIndex];
				InputBindingCompositeContext context = new InputBindingCompositeContext
				{
					m_State = this,
					m_BindingIndex = compositeBindingIndex
				};
				inputBindingComposite.ReadValue(ref context, buffer, bufferSize);
				bindingIndex = compositeBindingIndex;
			}
			else
			{
				control = this.controls[controlIndex];
				control.ReadValueIntoBuffer(buffer, bufferSize);
			}
			int processorCount = this.bindingStates[bindingIndex].processorCount;
			if (processorCount > 0)
			{
				int processorStartIndex = this.bindingStates[bindingIndex].processorStartIndex;
				for (int i = 0; i < processorCount; i++)
				{
					this.processors[processorStartIndex + i].Process(buffer, bufferSize, control);
				}
			}
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000E928 File Offset: 0x0000CB28
		internal unsafe TValue ReadValue<TValue>(int bindingIndex, int controlIndex, bool ignoreComposites = false) where TValue : struct
		{
			TValue value = default(TValue);
			InputControl<TValue> controlOfType = null;
			if (!ignoreComposites && this.bindingStates[bindingIndex].isPartOfComposite)
			{
				int compositeBindingIndex = this.bindingStates[bindingIndex].compositeOrCompositeBindingIndex;
				int compositeIndex = this.bindingStates[compositeBindingIndex].compositeOrCompositeBindingIndex;
				InputBindingComposite compositeObject = this.composites[compositeIndex];
				InputBindingCompositeContext context = new InputBindingCompositeContext
				{
					m_State = this,
					m_BindingIndex = compositeBindingIndex
				};
				InputBindingComposite<TValue> compositeOfType = compositeObject as InputBindingComposite<TValue>;
				if (compositeOfType == null)
				{
					Type valueType = compositeObject.valueType;
					if (!valueType.IsAssignableFrom(typeof(TValue)))
					{
						throw new InvalidOperationException(string.Format("Cannot read value of type '{0}' from composite '{1}' bound to action '{2}' (composite is a '{3}' with value type '{4}')", new object[]
						{
							typeof(TValue).Name,
							compositeObject,
							this.GetActionOrNull(bindingIndex),
							compositeIndex.GetType().Name,
							valueType.GetNiceTypeName()
						}));
					}
					compositeObject.ReadValue(ref context, UnsafeUtility.AddressOf<TValue>(ref value), UnsafeUtility.SizeOf<TValue>());
				}
				else
				{
					value = compositeOfType.ReadValue(ref context);
				}
				bindingIndex = compositeBindingIndex;
			}
			else if (controlIndex != -1)
			{
				InputControl control = this.controls[controlIndex];
				controlOfType = control as InputControl<TValue>;
				if (controlOfType == null)
				{
					throw new InvalidOperationException(string.Format("Cannot read value of type '{0}' from control '{1}' bound to action '{2}' (control is a '{3}' with value type '{4}')", new object[]
					{
						typeof(TValue).GetNiceTypeName(),
						control.path,
						this.GetActionOrNull(bindingIndex),
						control.GetType().Name,
						control.valueType.GetNiceTypeName()
					}));
				}
				value = *controlOfType.value;
			}
			return this.ApplyProcessors<TValue>(bindingIndex, value, controlOfType);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000EAE0 File Offset: 0x0000CCE0
		internal unsafe TValue ApplyProcessors<TValue>(int bindingIndex, TValue value, InputControl<TValue> controlOfType = null) where TValue : struct
		{
			int processorCount = this.bindingStates[bindingIndex].processorCount;
			if (processorCount > 0)
			{
				int processorStartIndex = this.bindingStates[bindingIndex].processorStartIndex;
				for (int i = 0; i < processorCount; i++)
				{
					InputProcessor<TValue> processor = this.processors[processorStartIndex + i] as InputProcessor<TValue>;
					if (processor != null)
					{
						value = processor.Process(value, controlOfType);
					}
				}
			}
			return value;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000EB48 File Offset: 0x0000CD48
		public unsafe float EvaluateCompositePartMagnitude(int bindingIndex, int partNumber)
		{
			int num = bindingIndex + 1;
			float currentMagnitude = float.MinValue;
			int index = num;
			while (index < this.totalBindingCount && this.bindingStates[index].isPartOfComposite)
			{
				if (this.bindingStates[index].partIndex == partNumber)
				{
					int controlCount = this.bindingStates[index].controlCount;
					int controlStartIndex = this.bindingStates[index].controlStartIndex;
					for (int i = 0; i < controlCount; i++)
					{
						currentMagnitude = Mathf.Max(this.controls[controlStartIndex + i].magnitude, currentMagnitude);
					}
				}
				index++;
			}
			return currentMagnitude;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000EBF4 File Offset: 0x0000CDF4
		internal unsafe double GetCompositePartPressTime(int bindingIndex, int partNumber)
		{
			int num = bindingIndex + 1;
			double pressTime = double.MaxValue;
			int index = num;
			while (index < this.totalBindingCount && this.bindingStates[index].isPartOfComposite)
			{
				ref InputActionState.BindingState bindingState = ref this.bindingStates[index];
				if (bindingState.partIndex == partNumber && bindingState.pressTime != 0.0 && bindingState.pressTime < pressTime)
				{
					pressTime = bindingState.pressTime;
				}
				index++;
			}
			if (pressTime == 1.7976931348623157E+308)
			{
				return -1.0;
			}
			return pressTime;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000EC88 File Offset: 0x0000CE88
		internal unsafe TValue ReadCompositePartValue<TValue, TComparer>(int bindingIndex, int partNumber, bool* buttonValuePtr, out int controlIndex, TComparer comparer = default(TComparer)) where TValue : struct where TComparer : IComparer<TValue>
		{
			TValue result = default(TValue);
			int num = bindingIndex + 1;
			bool isFirstValue = true;
			controlIndex = -1;
			int index = num;
			while (index < this.totalBindingCount && this.bindingStates[index].isPartOfComposite)
			{
				if (this.bindingStates[index].partIndex == partNumber)
				{
					int controlCount = this.bindingStates[index].controlCount;
					int controlStartIndex = this.bindingStates[index].controlStartIndex;
					for (int i = 0; i < controlCount; i++)
					{
						int thisControlIndex = controlStartIndex + i;
						TValue value = this.ReadValue<TValue>(index, thisControlIndex, true);
						if (isFirstValue)
						{
							result = value;
							controlIndex = thisControlIndex;
							isFirstValue = false;
						}
						else if (comparer.Compare(value, result) > 0)
						{
							result = value;
							controlIndex = thisControlIndex;
						}
						if (buttonValuePtr != null && controlIndex == thisControlIndex)
						{
							InputControl control = this.controls[thisControlIndex];
							ButtonControl button = control as ButtonControl;
							if (button != null)
							{
								*buttonValuePtr = button.isPressed;
							}
							else if (control is InputControl<float>)
							{
								void* valuePtr = UnsafeUtility.AddressOf<TValue>(ref value);
								*buttonValuePtr = *(float*)valuePtr >= ButtonControl.s_GlobalDefaultButtonPressPoint;
							}
						}
					}
				}
				index++;
			}
			return result;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000EDC0 File Offset: 0x0000CFC0
		internal unsafe bool ReadCompositePartValue(int bindingIndex, int partNumber, void* buffer, int bufferSize)
		{
			int num = bindingIndex + 1;
			float currentMagnitude = float.MinValue;
			int index = num;
			while (index < this.totalBindingCount && this.bindingStates[index].isPartOfComposite)
			{
				if (this.bindingStates[index].partIndex == partNumber)
				{
					int controlCount = this.bindingStates[index].controlCount;
					int controlStartIndex = this.bindingStates[index].controlStartIndex;
					for (int i = 0; i < controlCount; i++)
					{
						int thisControlIndex = controlStartIndex + i;
						float magnitude = this.controls[thisControlIndex].magnitude;
						if (magnitude >= currentMagnitude)
						{
							this.ReadValue(index, thisControlIndex, buffer, bufferSize, true);
							currentMagnitude = magnitude;
						}
					}
				}
				index++;
			}
			return currentMagnitude > float.MinValue;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000EE8C File Offset: 0x0000D08C
		internal unsafe object ReadCompositePartValueAsObject(int bindingIndex, int partNumber)
		{
			int num = bindingIndex + 1;
			float currentMagnitude = float.MinValue;
			object currentValue = null;
			int index = num;
			while (index < this.totalBindingCount && this.bindingStates[index].isPartOfComposite)
			{
				if (this.bindingStates[index].partIndex == partNumber)
				{
					int controlCount = this.bindingStates[index].controlCount;
					int controlStartIndex = this.bindingStates[index].controlStartIndex;
					for (int i = 0; i < controlCount; i++)
					{
						int thisControlIndex = controlStartIndex + i;
						float magnitude = this.controls[thisControlIndex].magnitude;
						if (magnitude >= currentMagnitude)
						{
							currentValue = this.ReadValueAsObject(index, thisControlIndex, true);
							currentMagnitude = magnitude;
						}
					}
				}
				index++;
			}
			return currentValue;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000EF54 File Offset: 0x0000D154
		internal unsafe object ReadValueAsObject(int bindingIndex, int controlIndex, bool ignoreComposites = false)
		{
			InputControl control = null;
			object value = null;
			if (!ignoreComposites && this.bindingStates[bindingIndex].isPartOfComposite)
			{
				int compositeBindingIndex = this.bindingStates[bindingIndex].compositeOrCompositeBindingIndex;
				int compositeIndex = this.bindingStates[compositeBindingIndex].compositeOrCompositeBindingIndex;
				InputBindingComposite inputBindingComposite = this.composites[compositeIndex];
				InputBindingCompositeContext context = new InputBindingCompositeContext
				{
					m_State = this,
					m_BindingIndex = compositeBindingIndex
				};
				value = inputBindingComposite.ReadValueAsObject(ref context);
				bindingIndex = compositeBindingIndex;
			}
			else if (controlIndex != -1)
			{
				control = this.controls[controlIndex];
				value = control.ReadValueAsObject();
			}
			if (value != null)
			{
				int processorCount = this.bindingStates[bindingIndex].processorCount;
				if (processorCount > 0)
				{
					int processorStartIndex = this.bindingStates[bindingIndex].processorStartIndex;
					for (int i = 0; i < processorCount; i++)
					{
						value = this.processors[processorStartIndex + i].ProcessAsObject(value, control);
					}
				}
			}
			return value;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000F04C File Offset: 0x0000D24C
		internal unsafe bool ReadValueAsButton(int bindingIndex, int controlIndex)
		{
			ButtonControl buttonControl = null;
			if (!this.bindingStates[bindingIndex].isPartOfComposite)
			{
				buttonControl = this.controls[controlIndex] as ButtonControl;
			}
			float floatValue = this.ReadValue<float>(bindingIndex, controlIndex, false);
			if (buttonControl != null)
			{
				return floatValue >= buttonControl.pressPointOrDefault;
			}
			return floatValue >= ButtonControl.s_GlobalDefaultButtonPressPoint;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000F0A8 File Offset: 0x0000D2A8
		internal static ISavedState SaveAndResetState()
		{
			ISavedState savedState = new SavedStructState<InputActionState.GlobalState>(ref InputActionState.s_GlobalState, delegate(ref InputActionState.GlobalState state)
			{
				InputActionState.s_GlobalState = state;
			}, delegate
			{
				InputActionState.ResetGlobals();
			});
			InputActionState.s_GlobalState = default(InputActionState.GlobalState);
			return savedState;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000F108 File Offset: 0x0000D308
		private void AddToGlobalList()
		{
			InputActionState.CompactGlobalList();
			GCHandle handle = GCHandle.Alloc(this, GCHandleType.Weak);
			InputActionState.s_GlobalState.globalList.AppendWithCapacity(handle, 10);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000F138 File Offset: 0x0000D338
		private void RemoveMapFromGlobalList()
		{
			int count = InputActionState.s_GlobalState.globalList.length;
			for (int i = 0; i < count; i++)
			{
				if (InputActionState.s_GlobalState.globalList[i].Target == this)
				{
					InputActionState.s_GlobalState.globalList[i].Free();
					InputActionState.s_GlobalState.globalList.RemoveAtByMovingTailWithCapacity(i);
					return;
				}
			}
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000F1A8 File Offset: 0x0000D3A8
		private static void CompactGlobalList()
		{
			int length = InputActionState.s_GlobalState.globalList.length;
			int head = 0;
			for (int i = 0; i < length; i++)
			{
				GCHandle handle = InputActionState.s_GlobalState.globalList[i];
				if (handle.IsAllocated && handle.Target != null)
				{
					if (head != i)
					{
						InputActionState.s_GlobalState.globalList[head] = handle;
					}
					head++;
				}
				else
				{
					if (handle.IsAllocated)
					{
						InputActionState.s_GlobalState.globalList[i].Free();
					}
					InputActionState.s_GlobalState.globalList[i] = default(GCHandle);
				}
			}
			InputActionState.s_GlobalState.globalList.length = head;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000F260 File Offset: 0x0000D460
		internal void NotifyListenersOfActionChange(InputActionChange change)
		{
			for (int i = 0; i < this.totalMapCount; i++)
			{
				InputActionMap map = this.maps[i];
				if (map.m_SingletonAction != null)
				{
					InputActionState.NotifyListenersOfActionChange(change, map.m_SingletonAction);
				}
				else
				{
					if (!(map.m_Asset == null))
					{
						InputActionState.NotifyListenersOfActionChange(change, map.m_Asset);
						return;
					}
					InputActionState.NotifyListenersOfActionChange(change, map);
				}
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000F2C1 File Offset: 0x0000D4C1
		internal static void NotifyListenersOfActionChange(InputActionChange change, object actionOrMapOrAsset)
		{
			DelegateHelpers.InvokeCallbacksSafe<object, InputActionChange>(ref InputActionState.s_GlobalState.onActionChange, actionOrMapOrAsset, change, InputActionState.k_InputOnActionChangeMarker, "InputSystem.onActionChange", null);
			if (change == InputActionChange.BoundControlsChanged)
			{
				DelegateHelpers.InvokeCallbacksSafe<object>(ref InputActionState.s_GlobalState.onActionControlsChanged, actionOrMapOrAsset, "onActionControlsChange", null);
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000F2FC File Offset: 0x0000D4FC
		private static void ResetGlobals()
		{
			InputActionState.DestroyAllActionMapStates();
			for (int i = 0; i < InputActionState.s_GlobalState.globalList.length; i++)
			{
				if (InputActionState.s_GlobalState.globalList[i].IsAllocated)
				{
					InputActionState.s_GlobalState.globalList[i].Free();
				}
			}
			InputActionState.s_GlobalState.globalList.length = 0;
			InputActionState.s_GlobalState.onActionChange.Clear();
			InputActionState.s_GlobalState.onActionControlsChanged.Clear();
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000F388 File Offset: 0x0000D588
		internal unsafe static int FindAllEnabledActions(List<InputAction> result)
		{
			int numFound = 0;
			int stateCount = InputActionState.s_GlobalState.globalList.length;
			for (int i = 0; i < stateCount; i++)
			{
				GCHandle handle = InputActionState.s_GlobalState.globalList[i];
				if (handle.IsAllocated)
				{
					InputActionState state = (InputActionState)handle.Target;
					if (state != null)
					{
						int mapCount = state.totalMapCount;
						InputActionMap[] maps = state.maps;
						for (int j = 0; j < mapCount; j++)
						{
							InputActionMap map = maps[j];
							if (map.enabled)
							{
								InputAction[] actions = map.m_Actions;
								int actionCount = actions.Length;
								if (map.m_EnabledActionsCount == actionCount)
								{
									result.AddRange(actions);
									numFound += actionCount;
								}
								else
								{
									int actionStartIndex = state.mapIndices[map.m_MapIndexInState].actionStartIndex;
									for (int k = 0; k < actionCount; k++)
									{
										if (state.actionStates[actionStartIndex + k].phase != InputActionPhase.Disabled)
										{
											result.Add(actions[k]);
											numFound++;
										}
									}
								}
							}
						}
					}
				}
			}
			return numFound;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000F4AC File Offset: 0x0000D6AC
		internal static void OnDeviceChange(InputDevice device, InputDeviceChange change)
		{
			for (int i = 0; i < InputActionState.s_GlobalState.globalList.length; i++)
			{
				GCHandle handle = InputActionState.s_GlobalState.globalList[i];
				if (!handle.IsAllocated || handle.Target == null)
				{
					if (handle.IsAllocated)
					{
						InputActionState.s_GlobalState.globalList[i].Free();
					}
					InputActionState.s_GlobalState.globalList.RemoveAtWithCapacity(i);
					i--;
				}
				else
				{
					InputActionState state = (InputActionState)handle.Target;
					bool needsFullResolve = true;
					switch (change)
					{
					case InputDeviceChange.Added:
						if (!state.CanUseDevice(device))
						{
							goto IL_0155;
						}
						needsFullResolve = false;
						break;
					case InputDeviceChange.Removed:
					{
						if (!state.IsUsingDevice(device))
						{
							goto IL_0155;
						}
						for (int j = 0; j < state.totalMapCount; j++)
						{
							InputActionMap inputActionMap = state.maps[j];
							inputActionMap.m_Devices.Remove(device);
							InputActionAsset asset = inputActionMap.asset;
							if (asset != null)
							{
								asset.m_Devices.Remove(device);
							}
						}
						needsFullResolve = false;
						break;
					}
					case InputDeviceChange.UsageChanged:
					case InputDeviceChange.ConfigurationChanged:
						if (!state.IsUsingDevice(device) && !state.CanUseDevice(device))
						{
							goto IL_0155;
						}
						break;
					case InputDeviceChange.SoftReset:
					case InputDeviceChange.HardReset:
						if (state.IsUsingDevice(device))
						{
							state.ResetActionStatesDrivenBy(device);
							goto IL_0155;
						}
						goto IL_0155;
					}
					int k = 0;
					while (k < state.totalMapCount && !state.maps[k].LazyResolveBindings(needsFullResolve))
					{
						k++;
					}
				}
				IL_0155:;
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000F628 File Offset: 0x0000D828
		internal static void DeferredResolutionOfBindings()
		{
			InputActionMap.s_DeferBindingResolution++;
			try
			{
				if (InputActionMap.s_NeedToResolveBindings)
				{
					for (int i = 0; i < InputActionState.s_GlobalState.globalList.length; i++)
					{
						GCHandle handle = InputActionState.s_GlobalState.globalList[i];
						InputActionState state = (handle.IsAllocated ? ((InputActionState)handle.Target) : null);
						if (state == null)
						{
							if (handle.IsAllocated)
							{
								InputActionState.s_GlobalState.globalList[i].Free();
							}
							InputActionState.s_GlobalState.globalList.RemoveAtWithCapacity(i);
							i--;
						}
						else
						{
							for (int j = 0; j < state.totalMapCount; j++)
							{
								state.maps[j].ResolveBindingsIfNecessary();
							}
						}
					}
					InputActionMap.s_NeedToResolveBindings = false;
				}
			}
			finally
			{
				InputActionMap.s_DeferBindingResolution--;
			}
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000F718 File Offset: 0x0000D918
		internal static void DisableAllActions()
		{
			for (int i = 0; i < InputActionState.s_GlobalState.globalList.length; i++)
			{
				GCHandle handle = InputActionState.s_GlobalState.globalList[i];
				if (handle.IsAllocated && handle.Target != null)
				{
					InputActionState inputActionState = (InputActionState)handle.Target;
					int mapCount = inputActionState.totalMapCount;
					InputActionMap[] maps = inputActionState.maps;
					for (int j = 0; j < mapCount; j++)
					{
						maps[j].Disable();
					}
				}
			}
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000F794 File Offset: 0x0000D994
		internal static void DestroyAllActionMapStates()
		{
			while (InputActionState.s_GlobalState.globalList.length > 0)
			{
				int index = InputActionState.s_GlobalState.globalList.length - 1;
				GCHandle handle = InputActionState.s_GlobalState.globalList[index];
				if (!handle.IsAllocated || handle.Target == null)
				{
					if (handle.IsAllocated)
					{
						InputActionState.s_GlobalState.globalList[index].Free();
					}
					InputActionState.s_GlobalState.globalList.RemoveAtWithCapacity(index);
				}
				else
				{
					((InputActionState)handle.Target).Destroy(false);
				}
			}
		}

		// Token: 0x0400016F RID: 367
		public const int kInvalidIndex = -1;

		// Token: 0x04000170 RID: 368
		public InputActionMap[] maps;

		// Token: 0x04000171 RID: 369
		public InputControl[] controls;

		// Token: 0x04000172 RID: 370
		public IInputInteraction[] interactions;

		// Token: 0x04000173 RID: 371
		public InputProcessor[] processors;

		// Token: 0x04000174 RID: 372
		public InputBindingComposite[] composites;

		// Token: 0x04000175 RID: 373
		public int totalProcessorCount;

		// Token: 0x04000176 RID: 374
		public InputActionState.UnmanagedMemory memory;

		// Token: 0x04000177 RID: 375
		private bool m_OnBeforeUpdateHooked;

		// Token: 0x04000178 RID: 376
		private bool m_OnAfterUpdateHooked;

		// Token: 0x04000179 RID: 377
		private bool m_InProcessControlStateChange;

		// Token: 0x0400017A RID: 378
		private InputEventPtr m_CurrentlyProcessingThisEvent;

		// Token: 0x0400017B RID: 379
		private Action m_OnBeforeUpdateDelegate;

		// Token: 0x0400017C RID: 380
		private Action m_OnAfterUpdateDelegate;

		// Token: 0x0400017D RID: 381
		private static readonly ProfilerMarker k_InputInitialActionStateCheckMarker = new ProfilerMarker("InitialActionStateCheck");

		// Token: 0x0400017E RID: 382
		private static readonly ProfilerMarker k_InputActionResolveConflictMarker = new ProfilerMarker("InputActionResolveConflict");

		// Token: 0x0400017F RID: 383
		private static readonly ProfilerMarker k_InputActionCallbackMarker = new ProfilerMarker("InputActionCallback");

		// Token: 0x04000180 RID: 384
		private static readonly ProfilerMarker k_InputOnActionChangeMarker = new ProfilerMarker("InpustSystem.onActionChange");

		// Token: 0x04000181 RID: 385
		private static readonly ProfilerMarker k_InputOnDeviceChangeMarker = new ProfilerMarker("InpustSystem.onDeviceChange");

		// Token: 0x04000182 RID: 386
		internal static InputActionState.GlobalState s_GlobalState;

		// Token: 0x02000045 RID: 69
		[StructLayout(LayoutKind.Explicit, Size = 48)]
		internal struct InteractionState
		{
			// Token: 0x170000EA RID: 234
			// (get) Token: 0x06000359 RID: 857 RVA: 0x0000F88C File Offset: 0x0000DA8C
			// (set) Token: 0x0600035A RID: 858 RVA: 0x0000F8A3 File Offset: 0x0000DAA3
			public int triggerControlIndex
			{
				get
				{
					if (this.m_TriggerControlIndex == 65535)
					{
						return -1;
					}
					return (int)this.m_TriggerControlIndex;
				}
				set
				{
					if (value == -1)
					{
						this.m_TriggerControlIndex = ushort.MaxValue;
						return;
					}
					if (value < 0 || value >= 65535)
					{
						throw new NotSupportedException("More than ushort.MaxValue-1 controls in a single InputActionState");
					}
					this.m_TriggerControlIndex = (ushort)value;
				}
			}

			// Token: 0x170000EB RID: 235
			// (get) Token: 0x0600035B RID: 859 RVA: 0x0000F8D4 File Offset: 0x0000DAD4
			// (set) Token: 0x0600035C RID: 860 RVA: 0x0000F8DC File Offset: 0x0000DADC
			public double startTime
			{
				get
				{
					return this.m_StartTime;
				}
				set
				{
					this.m_StartTime = value;
				}
			}

			// Token: 0x170000EC RID: 236
			// (get) Token: 0x0600035D RID: 861 RVA: 0x0000F8E5 File Offset: 0x0000DAE5
			// (set) Token: 0x0600035E RID: 862 RVA: 0x0000F8ED File Offset: 0x0000DAED
			public double performedTime
			{
				get
				{
					return this.m_PerformedTime;
				}
				set
				{
					this.m_PerformedTime = value;
				}
			}

			// Token: 0x170000ED RID: 237
			// (get) Token: 0x0600035F RID: 863 RVA: 0x0000F8F6 File Offset: 0x0000DAF6
			// (set) Token: 0x06000360 RID: 864 RVA: 0x0000F8FE File Offset: 0x0000DAFE
			public double timerStartTime
			{
				get
				{
					return this.m_TimerStartTime;
				}
				set
				{
					this.m_TimerStartTime = value;
				}
			}

			// Token: 0x170000EE RID: 238
			// (get) Token: 0x06000361 RID: 865 RVA: 0x0000F907 File Offset: 0x0000DB07
			// (set) Token: 0x06000362 RID: 866 RVA: 0x0000F90F File Offset: 0x0000DB0F
			public float timerDuration
			{
				get
				{
					return this.m_TimerDuration;
				}
				set
				{
					this.m_TimerDuration = value;
				}
			}

			// Token: 0x170000EF RID: 239
			// (get) Token: 0x06000363 RID: 867 RVA: 0x0000F918 File Offset: 0x0000DB18
			// (set) Token: 0x06000364 RID: 868 RVA: 0x0000F920 File Offset: 0x0000DB20
			public float totalTimeoutCompletionDone
			{
				get
				{
					return this.m_TotalTimeoutCompletionTimeDone;
				}
				set
				{
					this.m_TotalTimeoutCompletionTimeDone = value;
				}
			}

			// Token: 0x170000F0 RID: 240
			// (get) Token: 0x06000365 RID: 869 RVA: 0x0000F929 File Offset: 0x0000DB29
			// (set) Token: 0x06000366 RID: 870 RVA: 0x0000F931 File Offset: 0x0000DB31
			public float totalTimeoutCompletionTimeRemaining
			{
				get
				{
					return this.m_TotalTimeoutCompletionTimeRemaining;
				}
				set
				{
					this.m_TotalTimeoutCompletionTimeRemaining = value;
				}
			}

			// Token: 0x170000F1 RID: 241
			// (get) Token: 0x06000367 RID: 871 RVA: 0x0000F93A File Offset: 0x0000DB3A
			// (set) Token: 0x06000368 RID: 872 RVA: 0x0000F942 File Offset: 0x0000DB42
			public long timerMonitorIndex
			{
				get
				{
					return this.m_TimerMonitorIndex;
				}
				set
				{
					this.m_TimerMonitorIndex = value;
				}
			}

			// Token: 0x170000F2 RID: 242
			// (get) Token: 0x06000369 RID: 873 RVA: 0x0000F94B File Offset: 0x0000DB4B
			// (set) Token: 0x0600036A RID: 874 RVA: 0x0000F958 File Offset: 0x0000DB58
			public bool isTimerRunning
			{
				get
				{
					return (this.m_Flags & 1) == 1;
				}
				set
				{
					if (value)
					{
						this.m_Flags |= 1;
						return;
					}
					InputActionState.InteractionState.Flags mask = ~InputActionState.InteractionState.Flags.TimerRunning;
					this.m_Flags &= (byte)mask;
				}
			}

			// Token: 0x170000F3 RID: 243
			// (get) Token: 0x0600036B RID: 875 RVA: 0x0000F98B File Offset: 0x0000DB8B
			// (set) Token: 0x0600036C RID: 876 RVA: 0x0000F993 File Offset: 0x0000DB93
			public InputActionPhase phase
			{
				get
				{
					return (InputActionPhase)this.m_Phase;
				}
				set
				{
					this.m_Phase = (byte)value;
				}
			}

			// Token: 0x04000183 RID: 387
			[FieldOffset(0)]
			private ushort m_TriggerControlIndex;

			// Token: 0x04000184 RID: 388
			[FieldOffset(2)]
			private byte m_Phase;

			// Token: 0x04000185 RID: 389
			[FieldOffset(3)]
			private byte m_Flags;

			// Token: 0x04000186 RID: 390
			[FieldOffset(4)]
			private float m_TimerDuration;

			// Token: 0x04000187 RID: 391
			[FieldOffset(8)]
			private double m_StartTime;

			// Token: 0x04000188 RID: 392
			[FieldOffset(16)]
			private double m_TimerStartTime;

			// Token: 0x04000189 RID: 393
			[FieldOffset(24)]
			private double m_PerformedTime;

			// Token: 0x0400018A RID: 394
			[FieldOffset(32)]
			private float m_TotalTimeoutCompletionTimeDone;

			// Token: 0x0400018B RID: 395
			[FieldOffset(36)]
			private float m_TotalTimeoutCompletionTimeRemaining;

			// Token: 0x0400018C RID: 396
			[FieldOffset(40)]
			private long m_TimerMonitorIndex;

			// Token: 0x02000046 RID: 70
			[Flags]
			private enum Flags
			{
				// Token: 0x0400018E RID: 398
				TimerRunning = 1
			}
		}

		// Token: 0x02000047 RID: 71
		[StructLayout(LayoutKind.Explicit, Size = 32)]
		internal struct BindingState
		{
			// Token: 0x170000F4 RID: 244
			// (get) Token: 0x0600036D RID: 877 RVA: 0x0000F99D File Offset: 0x0000DB9D
			// (set) Token: 0x0600036E RID: 878 RVA: 0x0000F9A8 File Offset: 0x0000DBA8
			public int controlStartIndex
			{
				get
				{
					return (int)this.m_ControlStartIndex;
				}
				set
				{
					if (value >= 65535)
					{
						throw new NotSupportedException("Total control count in state cannot exceed byte.MaxValue=" + ushort.MaxValue.ToString());
					}
					this.m_ControlStartIndex = (ushort)value;
				}
			}

			// Token: 0x170000F5 RID: 245
			// (get) Token: 0x0600036F RID: 879 RVA: 0x0000F9E2 File Offset: 0x0000DBE2
			// (set) Token: 0x06000370 RID: 880 RVA: 0x0000F9EC File Offset: 0x0000DBEC
			public int controlCount
			{
				get
				{
					return (int)this.m_ControlCount;
				}
				set
				{
					if (value >= 255)
					{
						throw new NotSupportedException("Control count per binding cannot exceed byte.MaxValue=" + byte.MaxValue.ToString());
					}
					this.m_ControlCount = (byte)value;
				}
			}

			// Token: 0x170000F6 RID: 246
			// (get) Token: 0x06000371 RID: 881 RVA: 0x0000FA26 File Offset: 0x0000DC26
			// (set) Token: 0x06000372 RID: 882 RVA: 0x0000FA40 File Offset: 0x0000DC40
			public int interactionStartIndex
			{
				get
				{
					if (this.m_InteractionStartIndex == 65535)
					{
						return -1;
					}
					return (int)this.m_InteractionStartIndex;
				}
				set
				{
					if (value == -1)
					{
						this.m_InteractionStartIndex = ushort.MaxValue;
						return;
					}
					if (value >= 65535)
					{
						throw new NotSupportedException("Interaction count cannot exceed ushort.MaxValue=" + ushort.MaxValue.ToString());
					}
					this.m_InteractionStartIndex = (ushort)value;
				}
			}

			// Token: 0x170000F7 RID: 247
			// (get) Token: 0x06000373 RID: 883 RVA: 0x0000FA8A File Offset: 0x0000DC8A
			// (set) Token: 0x06000374 RID: 884 RVA: 0x0000FA94 File Offset: 0x0000DC94
			public int interactionCount
			{
				get
				{
					return (int)this.m_InteractionCount;
				}
				set
				{
					if (value >= 255)
					{
						throw new NotSupportedException("Interaction count per binding cannot exceed byte.MaxValue=" + byte.MaxValue.ToString());
					}
					this.m_InteractionCount = (byte)value;
				}
			}

			// Token: 0x170000F8 RID: 248
			// (get) Token: 0x06000375 RID: 885 RVA: 0x0000FACE File Offset: 0x0000DCCE
			// (set) Token: 0x06000376 RID: 886 RVA: 0x0000FAE8 File Offset: 0x0000DCE8
			public int processorStartIndex
			{
				get
				{
					if (this.m_ProcessorStartIndex == 65535)
					{
						return -1;
					}
					return (int)this.m_ProcessorStartIndex;
				}
				set
				{
					if (value == -1)
					{
						this.m_ProcessorStartIndex = ushort.MaxValue;
						return;
					}
					if (value >= 65535)
					{
						throw new NotSupportedException("Processor count cannot exceed ushort.MaxValue=" + ushort.MaxValue.ToString());
					}
					this.m_ProcessorStartIndex = (ushort)value;
				}
			}

			// Token: 0x170000F9 RID: 249
			// (get) Token: 0x06000377 RID: 887 RVA: 0x0000FB32 File Offset: 0x0000DD32
			// (set) Token: 0x06000378 RID: 888 RVA: 0x0000FB3C File Offset: 0x0000DD3C
			public int processorCount
			{
				get
				{
					return (int)this.m_ProcessorCount;
				}
				set
				{
					if (value >= 255)
					{
						throw new NotSupportedException("Processor count per binding cannot exceed byte.MaxValue=" + byte.MaxValue.ToString());
					}
					this.m_ProcessorCount = (byte)value;
				}
			}

			// Token: 0x170000FA RID: 250
			// (get) Token: 0x06000379 RID: 889 RVA: 0x0000FB76 File Offset: 0x0000DD76
			// (set) Token: 0x0600037A RID: 890 RVA: 0x0000FB90 File Offset: 0x0000DD90
			public int actionIndex
			{
				get
				{
					if (this.m_ActionIndex == 65535)
					{
						return -1;
					}
					return (int)this.m_ActionIndex;
				}
				set
				{
					if (value == -1)
					{
						this.m_ActionIndex = ushort.MaxValue;
						return;
					}
					if (value >= 65535)
					{
						throw new NotSupportedException("Action count cannot exceed ushort.MaxValue=" + ushort.MaxValue.ToString());
					}
					this.m_ActionIndex = (ushort)value;
				}
			}

			// Token: 0x170000FB RID: 251
			// (get) Token: 0x0600037B RID: 891 RVA: 0x0000FBDA File Offset: 0x0000DDDA
			// (set) Token: 0x0600037C RID: 892 RVA: 0x0000FBE4 File Offset: 0x0000DDE4
			public int mapIndex
			{
				get
				{
					return (int)this.m_MapIndex;
				}
				set
				{
					if (value >= 255)
					{
						throw new NotSupportedException("Map count cannot exceed byte.MaxValue=" + byte.MaxValue.ToString());
					}
					this.m_MapIndex = (byte)value;
				}
			}

			// Token: 0x170000FC RID: 252
			// (get) Token: 0x0600037D RID: 893 RVA: 0x0000FC1E File Offset: 0x0000DE1E
			// (set) Token: 0x0600037E RID: 894 RVA: 0x0000FC38 File Offset: 0x0000DE38
			public int compositeOrCompositeBindingIndex
			{
				get
				{
					if (this.m_CompositeOrCompositeBindingIndex == 65535)
					{
						return -1;
					}
					return (int)this.m_CompositeOrCompositeBindingIndex;
				}
				set
				{
					if (value == -1)
					{
						this.m_CompositeOrCompositeBindingIndex = ushort.MaxValue;
						return;
					}
					if (value >= 65535)
					{
						throw new NotSupportedException("Composite count cannot exceed ushort.MaxValue=" + ushort.MaxValue.ToString());
					}
					this.m_CompositeOrCompositeBindingIndex = (ushort)value;
				}
			}

			// Token: 0x170000FD RID: 253
			// (get) Token: 0x0600037F RID: 895 RVA: 0x0000FC82 File Offset: 0x0000DE82
			// (set) Token: 0x06000380 RID: 896 RVA: 0x0000FC8A File Offset: 0x0000DE8A
			public int triggerEventIdForComposite
			{
				get
				{
					return this.m_TriggerEventIdForComposite;
				}
				set
				{
					this.m_TriggerEventIdForComposite = value;
				}
			}

			// Token: 0x170000FE RID: 254
			// (get) Token: 0x06000381 RID: 897 RVA: 0x0000FC93 File Offset: 0x0000DE93
			// (set) Token: 0x06000382 RID: 898 RVA: 0x0000FC9B File Offset: 0x0000DE9B
			public double pressTime
			{
				get
				{
					return this.m_PressTime;
				}
				set
				{
					this.m_PressTime = value;
				}
			}

			// Token: 0x170000FF RID: 255
			// (get) Token: 0x06000383 RID: 899 RVA: 0x0000FCA4 File Offset: 0x0000DEA4
			// (set) Token: 0x06000384 RID: 900 RVA: 0x0000FCAC File Offset: 0x0000DEAC
			public InputActionState.BindingState.Flags flags
			{
				get
				{
					return (InputActionState.BindingState.Flags)this.m_Flags;
				}
				set
				{
					this.m_Flags = (byte)value;
				}
			}

			// Token: 0x17000100 RID: 256
			// (get) Token: 0x06000385 RID: 901 RVA: 0x0000FCB6 File Offset: 0x0000DEB6
			// (set) Token: 0x06000386 RID: 902 RVA: 0x0000FCC3 File Offset: 0x0000DEC3
			public bool chainsWithNext
			{
				get
				{
					return (this.flags & InputActionState.BindingState.Flags.ChainsWithNext) == InputActionState.BindingState.Flags.ChainsWithNext;
				}
				set
				{
					if (value)
					{
						this.flags |= InputActionState.BindingState.Flags.ChainsWithNext;
						return;
					}
					this.flags &= ~InputActionState.BindingState.Flags.ChainsWithNext;
				}
			}

			// Token: 0x17000101 RID: 257
			// (get) Token: 0x06000387 RID: 903 RVA: 0x0000FCE6 File Offset: 0x0000DEE6
			// (set) Token: 0x06000388 RID: 904 RVA: 0x0000FCF3 File Offset: 0x0000DEF3
			public bool isEndOfChain
			{
				get
				{
					return (this.flags & InputActionState.BindingState.Flags.EndOfChain) == InputActionState.BindingState.Flags.EndOfChain;
				}
				set
				{
					if (value)
					{
						this.flags |= InputActionState.BindingState.Flags.EndOfChain;
						return;
					}
					this.flags &= ~InputActionState.BindingState.Flags.EndOfChain;
				}
			}

			// Token: 0x17000102 RID: 258
			// (get) Token: 0x06000389 RID: 905 RVA: 0x0000FD16 File Offset: 0x0000DF16
			public bool isPartOfChain
			{
				get
				{
					return this.chainsWithNext || this.isEndOfChain;
				}
			}

			// Token: 0x17000103 RID: 259
			// (get) Token: 0x0600038A RID: 906 RVA: 0x0000FD28 File Offset: 0x0000DF28
			// (set) Token: 0x0600038B RID: 907 RVA: 0x0000FD35 File Offset: 0x0000DF35
			public bool isComposite
			{
				get
				{
					return (this.flags & InputActionState.BindingState.Flags.Composite) == InputActionState.BindingState.Flags.Composite;
				}
				set
				{
					if (value)
					{
						this.flags |= InputActionState.BindingState.Flags.Composite;
						return;
					}
					this.flags &= ~InputActionState.BindingState.Flags.Composite;
				}
			}

			// Token: 0x17000104 RID: 260
			// (get) Token: 0x0600038C RID: 908 RVA: 0x0000FD58 File Offset: 0x0000DF58
			// (set) Token: 0x0600038D RID: 909 RVA: 0x0000FD65 File Offset: 0x0000DF65
			public bool isPartOfComposite
			{
				get
				{
					return (this.flags & InputActionState.BindingState.Flags.PartOfComposite) == InputActionState.BindingState.Flags.PartOfComposite;
				}
				set
				{
					if (value)
					{
						this.flags |= InputActionState.BindingState.Flags.PartOfComposite;
						return;
					}
					this.flags &= ~InputActionState.BindingState.Flags.PartOfComposite;
				}
			}

			// Token: 0x17000105 RID: 261
			// (get) Token: 0x0600038E RID: 910 RVA: 0x0000FD88 File Offset: 0x0000DF88
			// (set) Token: 0x0600038F RID: 911 RVA: 0x0000FD96 File Offset: 0x0000DF96
			public bool initialStateCheckPending
			{
				get
				{
					return (this.flags & InputActionState.BindingState.Flags.InitialStateCheckPending) > (InputActionState.BindingState.Flags)0;
				}
				set
				{
					if (value)
					{
						this.flags |= InputActionState.BindingState.Flags.InitialStateCheckPending;
						return;
					}
					this.flags &= ~InputActionState.BindingState.Flags.InitialStateCheckPending;
				}
			}

			// Token: 0x17000106 RID: 262
			// (get) Token: 0x06000390 RID: 912 RVA: 0x0000FDBA File Offset: 0x0000DFBA
			// (set) Token: 0x06000391 RID: 913 RVA: 0x0000FDC8 File Offset: 0x0000DFC8
			public bool wantsInitialStateCheck
			{
				get
				{
					return (this.flags & InputActionState.BindingState.Flags.WantsInitialStateCheck) > (InputActionState.BindingState.Flags)0;
				}
				set
				{
					if (value)
					{
						this.flags |= InputActionState.BindingState.Flags.WantsInitialStateCheck;
						return;
					}
					this.flags &= ~InputActionState.BindingState.Flags.WantsInitialStateCheck;
				}
			}

			// Token: 0x17000107 RID: 263
			// (get) Token: 0x06000392 RID: 914 RVA: 0x0000FDEC File Offset: 0x0000DFEC
			// (set) Token: 0x06000393 RID: 915 RVA: 0x0000FDF4 File Offset: 0x0000DFF4
			public int partIndex
			{
				get
				{
					return (int)this.m_PartIndex;
				}
				set
				{
					if (this.partIndex < 0)
					{
						throw new ArgumentOutOfRangeException("value", "Part index must not be negative");
					}
					if (this.partIndex > 255)
					{
						throw new InvalidOperationException("Part count must not exceed byte.MaxValue=" + byte.MaxValue.ToString());
					}
					this.m_PartIndex = (byte)value;
				}
			}

			// Token: 0x0400018F RID: 399
			[FieldOffset(0)]
			private byte m_ControlCount;

			// Token: 0x04000190 RID: 400
			[FieldOffset(1)]
			private byte m_InteractionCount;

			// Token: 0x04000191 RID: 401
			[FieldOffset(2)]
			private byte m_ProcessorCount;

			// Token: 0x04000192 RID: 402
			[FieldOffset(3)]
			private byte m_MapIndex;

			// Token: 0x04000193 RID: 403
			[FieldOffset(4)]
			private byte m_Flags;

			// Token: 0x04000194 RID: 404
			[FieldOffset(5)]
			private byte m_PartIndex;

			// Token: 0x04000195 RID: 405
			[FieldOffset(6)]
			private ushort m_ActionIndex;

			// Token: 0x04000196 RID: 406
			[FieldOffset(8)]
			private ushort m_CompositeOrCompositeBindingIndex;

			// Token: 0x04000197 RID: 407
			[FieldOffset(10)]
			private ushort m_ProcessorStartIndex;

			// Token: 0x04000198 RID: 408
			[FieldOffset(12)]
			private ushort m_InteractionStartIndex;

			// Token: 0x04000199 RID: 409
			[FieldOffset(14)]
			private ushort m_ControlStartIndex;

			// Token: 0x0400019A RID: 410
			[FieldOffset(16)]
			private double m_PressTime;

			// Token: 0x0400019B RID: 411
			[FieldOffset(24)]
			private int m_TriggerEventIdForComposite;

			// Token: 0x0400019C RID: 412
			[FieldOffset(28)]
			private int __padding;

			// Token: 0x02000048 RID: 72
			[Flags]
			public enum Flags
			{
				// Token: 0x0400019E RID: 414
				ChainsWithNext = 1,
				// Token: 0x0400019F RID: 415
				EndOfChain = 2,
				// Token: 0x040001A0 RID: 416
				Composite = 4,
				// Token: 0x040001A1 RID: 417
				PartOfComposite = 8,
				// Token: 0x040001A2 RID: 418
				InitialStateCheckPending = 16,
				// Token: 0x040001A3 RID: 419
				WantsInitialStateCheck = 32
			}
		}

		// Token: 0x02000049 RID: 73
		[StructLayout(LayoutKind.Explicit, Size = 56)]
		public struct TriggerState
		{
			// Token: 0x17000108 RID: 264
			// (get) Token: 0x06000394 RID: 916 RVA: 0x0000FE4C File Offset: 0x0000E04C
			// (set) Token: 0x06000395 RID: 917 RVA: 0x0000FE54 File Offset: 0x0000E054
			public InputActionPhase phase
			{
				get
				{
					return (InputActionPhase)this.m_Phase;
				}
				set
				{
					this.m_Phase = (byte)value;
				}
			}

			// Token: 0x17000109 RID: 265
			// (get) Token: 0x06000396 RID: 918 RVA: 0x0000FE5E File Offset: 0x0000E05E
			public bool isDisabled
			{
				get
				{
					return this.phase == InputActionPhase.Disabled;
				}
			}

			// Token: 0x1700010A RID: 266
			// (get) Token: 0x06000397 RID: 919 RVA: 0x0000FE69 File Offset: 0x0000E069
			public bool isWaiting
			{
				get
				{
					return this.phase == InputActionPhase.Waiting;
				}
			}

			// Token: 0x1700010B RID: 267
			// (get) Token: 0x06000398 RID: 920 RVA: 0x0000FE74 File Offset: 0x0000E074
			public bool isStarted
			{
				get
				{
					return this.phase == InputActionPhase.Started;
				}
			}

			// Token: 0x1700010C RID: 268
			// (get) Token: 0x06000399 RID: 921 RVA: 0x0000FE7F File Offset: 0x0000E07F
			public bool isPerformed
			{
				get
				{
					return this.phase == InputActionPhase.Performed;
				}
			}

			// Token: 0x1700010D RID: 269
			// (get) Token: 0x0600039A RID: 922 RVA: 0x0000FE8A File Offset: 0x0000E08A
			public bool isCanceled
			{
				get
				{
					return this.phase == InputActionPhase.Canceled;
				}
			}

			// Token: 0x1700010E RID: 270
			// (get) Token: 0x0600039B RID: 923 RVA: 0x0000FE95 File Offset: 0x0000E095
			// (set) Token: 0x0600039C RID: 924 RVA: 0x0000FE9D File Offset: 0x0000E09D
			public double time
			{
				get
				{
					return this.m_Time;
				}
				set
				{
					this.m_Time = value;
				}
			}

			// Token: 0x1700010F RID: 271
			// (get) Token: 0x0600039D RID: 925 RVA: 0x0000FEA6 File Offset: 0x0000E0A6
			// (set) Token: 0x0600039E RID: 926 RVA: 0x0000FEAE File Offset: 0x0000E0AE
			public double startTime
			{
				get
				{
					return this.m_StartTime;
				}
				set
				{
					this.m_StartTime = value;
				}
			}

			// Token: 0x17000110 RID: 272
			// (get) Token: 0x0600039F RID: 927 RVA: 0x0000FEB7 File Offset: 0x0000E0B7
			// (set) Token: 0x060003A0 RID: 928 RVA: 0x0000FEBF File Offset: 0x0000E0BF
			public float magnitude
			{
				get
				{
					return this.m_Magnitude;
				}
				set
				{
					this.flags |= InputActionState.TriggerState.Flags.HaveMagnitude;
					this.m_Magnitude = value;
				}
			}

			// Token: 0x17000111 RID: 273
			// (get) Token: 0x060003A1 RID: 929 RVA: 0x0000FED6 File Offset: 0x0000E0D6
			public bool haveMagnitude
			{
				get
				{
					return (this.flags & InputActionState.TriggerState.Flags.HaveMagnitude) > (InputActionState.TriggerState.Flags)0;
				}
			}

			// Token: 0x17000112 RID: 274
			// (get) Token: 0x060003A2 RID: 930 RVA: 0x0000FEE3 File Offset: 0x0000E0E3
			// (set) Token: 0x060003A3 RID: 931 RVA: 0x0000FEEB File Offset: 0x0000E0EB
			public int mapIndex
			{
				get
				{
					return (int)this.m_MapIndex;
				}
				set
				{
					if (value < 0 || value > 255)
					{
						throw new NotSupportedException("More than byte.MaxValue InputActionMaps in a single InputActionState");
					}
					this.m_MapIndex = (byte)value;
				}
			}

			// Token: 0x17000113 RID: 275
			// (get) Token: 0x060003A4 RID: 932 RVA: 0x0000FF0C File Offset: 0x0000E10C
			// (set) Token: 0x060003A5 RID: 933 RVA: 0x0000FF23 File Offset: 0x0000E123
			public int controlIndex
			{
				get
				{
					if (this.m_ControlIndex == 65535)
					{
						return -1;
					}
					return (int)this.m_ControlIndex;
				}
				set
				{
					if (value == -1)
					{
						this.m_ControlIndex = ushort.MaxValue;
						return;
					}
					if (value < 0 || value >= 65535)
					{
						throw new NotSupportedException("More than ushort.MaxValue-1 controls in a single InputActionState");
					}
					this.m_ControlIndex = (ushort)value;
				}
			}

			// Token: 0x17000114 RID: 276
			// (get) Token: 0x060003A6 RID: 934 RVA: 0x0000FF54 File Offset: 0x0000E154
			// (set) Token: 0x060003A7 RID: 935 RVA: 0x0000FF5C File Offset: 0x0000E15C
			public int bindingIndex
			{
				get
				{
					return (int)this.m_BindingIndex;
				}
				set
				{
					if (value < 0 || value > 65535)
					{
						throw new NotSupportedException("More than ushort.MaxValue bindings in a single InputActionState");
					}
					this.m_BindingIndex = (ushort)value;
				}
			}

			// Token: 0x17000115 RID: 277
			// (get) Token: 0x060003A8 RID: 936 RVA: 0x0000FF7D File Offset: 0x0000E17D
			// (set) Token: 0x060003A9 RID: 937 RVA: 0x0000FF94 File Offset: 0x0000E194
			public int interactionIndex
			{
				get
				{
					if (this.m_InteractionIndex == 65535)
					{
						return -1;
					}
					return (int)this.m_InteractionIndex;
				}
				set
				{
					if (value == -1)
					{
						this.m_InteractionIndex = ushort.MaxValue;
						return;
					}
					if (value < 0 || value >= 65535)
					{
						throw new NotSupportedException("More than ushort.MaxValue-1 interactions in a single InputActionState");
					}
					this.m_InteractionIndex = (ushort)value;
				}
			}

			// Token: 0x17000116 RID: 278
			// (get) Token: 0x060003AA RID: 938 RVA: 0x0000FFC5 File Offset: 0x0000E1C5
			// (set) Token: 0x060003AB RID: 939 RVA: 0x0000FFCD File Offset: 0x0000E1CD
			public uint lastPerformedInUpdate
			{
				get
				{
					return this.m_LastPerformedInUpdate;
				}
				set
				{
					this.m_LastPerformedInUpdate = value;
				}
			}

			// Token: 0x17000117 RID: 279
			// (get) Token: 0x060003AC RID: 940 RVA: 0x0000FFD6 File Offset: 0x0000E1D6
			// (set) Token: 0x060003AD RID: 941 RVA: 0x0000FFDE File Offset: 0x0000E1DE
			internal int frame
			{
				get
				{
					return this.m_Frame;
				}
				set
				{
					this.m_Frame = value;
				}
			}

			// Token: 0x17000118 RID: 280
			// (get) Token: 0x060003AE RID: 942 RVA: 0x0000FFE7 File Offset: 0x0000E1E7
			// (set) Token: 0x060003AF RID: 943 RVA: 0x0000FFEF File Offset: 0x0000E1EF
			public uint lastCompletedInUpdate
			{
				get
				{
					return this.m_LastCompletedInUpdate;
				}
				set
				{
					this.m_LastCompletedInUpdate = value;
				}
			}

			// Token: 0x17000119 RID: 281
			// (get) Token: 0x060003B0 RID: 944 RVA: 0x0000FFF8 File Offset: 0x0000E1F8
			// (set) Token: 0x060003B1 RID: 945 RVA: 0x00010000 File Offset: 0x0000E200
			public uint lastCanceledInUpdate
			{
				get
				{
					return this.m_LastCanceledInUpdate;
				}
				set
				{
					this.m_LastCanceledInUpdate = value;
				}
			}

			// Token: 0x1700011A RID: 282
			// (get) Token: 0x060003B2 RID: 946 RVA: 0x00010009 File Offset: 0x0000E209
			// (set) Token: 0x060003B3 RID: 947 RVA: 0x00010011 File Offset: 0x0000E211
			public uint pressedInUpdate
			{
				get
				{
					return this.m_PressedInUpdate;
				}
				set
				{
					this.m_PressedInUpdate = value;
				}
			}

			// Token: 0x1700011B RID: 283
			// (get) Token: 0x060003B4 RID: 948 RVA: 0x0001001A File Offset: 0x0000E21A
			// (set) Token: 0x060003B5 RID: 949 RVA: 0x00010022 File Offset: 0x0000E222
			public uint releasedInUpdate
			{
				get
				{
					return this.m_ReleasedInUpdate;
				}
				set
				{
					this.m_ReleasedInUpdate = value;
				}
			}

			// Token: 0x1700011C RID: 284
			// (get) Token: 0x060003B6 RID: 950 RVA: 0x0001002B File Offset: 0x0000E22B
			// (set) Token: 0x060003B7 RID: 951 RVA: 0x00010038 File Offset: 0x0000E238
			public bool isPassThrough
			{
				get
				{
					return (this.flags & InputActionState.TriggerState.Flags.PassThrough) > (InputActionState.TriggerState.Flags)0;
				}
				set
				{
					if (value)
					{
						this.flags |= InputActionState.TriggerState.Flags.PassThrough;
						return;
					}
					this.flags &= ~InputActionState.TriggerState.Flags.PassThrough;
				}
			}

			// Token: 0x1700011D RID: 285
			// (get) Token: 0x060003B8 RID: 952 RVA: 0x0001005B File Offset: 0x0000E25B
			// (set) Token: 0x060003B9 RID: 953 RVA: 0x00010069 File Offset: 0x0000E269
			public bool isButton
			{
				get
				{
					return (this.flags & InputActionState.TriggerState.Flags.Button) > (InputActionState.TriggerState.Flags)0;
				}
				set
				{
					if (value)
					{
						this.flags |= InputActionState.TriggerState.Flags.Button;
						return;
					}
					this.flags &= ~InputActionState.TriggerState.Flags.Button;
				}
			}

			// Token: 0x1700011E RID: 286
			// (get) Token: 0x060003BA RID: 954 RVA: 0x0001008D File Offset: 0x0000E28D
			// (set) Token: 0x060003BB RID: 955 RVA: 0x0001009B File Offset: 0x0000E29B
			public bool isPressed
			{
				get
				{
					return (this.flags & InputActionState.TriggerState.Flags.Pressed) > (InputActionState.TriggerState.Flags)0;
				}
				set
				{
					if (value)
					{
						this.flags |= InputActionState.TriggerState.Flags.Pressed;
						return;
					}
					this.flags &= ~InputActionState.TriggerState.Flags.Pressed;
				}
			}

			// Token: 0x1700011F RID: 287
			// (get) Token: 0x060003BC RID: 956 RVA: 0x000100BF File Offset: 0x0000E2BF
			// (set) Token: 0x060003BD RID: 957 RVA: 0x000100CC File Offset: 0x0000E2CC
			public bool mayNeedConflictResolution
			{
				get
				{
					return (this.flags & InputActionState.TriggerState.Flags.MayNeedConflictResolution) > (InputActionState.TriggerState.Flags)0;
				}
				set
				{
					if (value)
					{
						this.flags |= InputActionState.TriggerState.Flags.MayNeedConflictResolution;
						return;
					}
					this.flags &= ~InputActionState.TriggerState.Flags.MayNeedConflictResolution;
				}
			}

			// Token: 0x17000120 RID: 288
			// (get) Token: 0x060003BE RID: 958 RVA: 0x000100EF File Offset: 0x0000E2EF
			// (set) Token: 0x060003BF RID: 959 RVA: 0x000100FC File Offset: 0x0000E2FC
			public bool hasMultipleConcurrentActuations
			{
				get
				{
					return (this.flags & InputActionState.TriggerState.Flags.HasMultipleConcurrentActuations) > (InputActionState.TriggerState.Flags)0;
				}
				set
				{
					if (value)
					{
						this.flags |= InputActionState.TriggerState.Flags.HasMultipleConcurrentActuations;
						return;
					}
					this.flags &= ~InputActionState.TriggerState.Flags.HasMultipleConcurrentActuations;
				}
			}

			// Token: 0x17000121 RID: 289
			// (get) Token: 0x060003C0 RID: 960 RVA: 0x0001011F File Offset: 0x0000E31F
			// (set) Token: 0x060003C1 RID: 961 RVA: 0x0001012D File Offset: 0x0000E32D
			public bool inProcessing
			{
				get
				{
					return (this.flags & InputActionState.TriggerState.Flags.InProcessing) > (InputActionState.TriggerState.Flags)0;
				}
				set
				{
					if (value)
					{
						this.flags |= InputActionState.TriggerState.Flags.InProcessing;
						return;
					}
					this.flags &= ~InputActionState.TriggerState.Flags.InProcessing;
				}
			}

			// Token: 0x17000122 RID: 290
			// (get) Token: 0x060003C2 RID: 962 RVA: 0x00010151 File Offset: 0x0000E351
			// (set) Token: 0x060003C3 RID: 963 RVA: 0x00010159 File Offset: 0x0000E359
			public InputActionState.TriggerState.Flags flags
			{
				get
				{
					return (InputActionState.TriggerState.Flags)this.m_Flags;
				}
				set
				{
					this.m_Flags = (byte)value;
				}
			}

			// Token: 0x040001A4 RID: 420
			public const int kMaxNumMaps = 255;

			// Token: 0x040001A5 RID: 421
			public const int kMaxNumControls = 65535;

			// Token: 0x040001A6 RID: 422
			public const int kMaxNumBindings = 65535;

			// Token: 0x040001A7 RID: 423
			[FieldOffset(0)]
			private byte m_Phase;

			// Token: 0x040001A8 RID: 424
			[FieldOffset(1)]
			private byte m_Flags;

			// Token: 0x040001A9 RID: 425
			[FieldOffset(2)]
			private byte m_MapIndex;

			// Token: 0x040001AA RID: 426
			[FieldOffset(4)]
			private ushort m_ControlIndex;

			// Token: 0x040001AB RID: 427
			[FieldOffset(8)]
			private double m_Time;

			// Token: 0x040001AC RID: 428
			[FieldOffset(16)]
			private double m_StartTime;

			// Token: 0x040001AD RID: 429
			[FieldOffset(24)]
			private ushort m_BindingIndex;

			// Token: 0x040001AE RID: 430
			[FieldOffset(26)]
			private ushort m_InteractionIndex;

			// Token: 0x040001AF RID: 431
			[FieldOffset(28)]
			private float m_Magnitude;

			// Token: 0x040001B0 RID: 432
			[FieldOffset(32)]
			private uint m_LastPerformedInUpdate;

			// Token: 0x040001B1 RID: 433
			[FieldOffset(36)]
			private uint m_LastCanceledInUpdate;

			// Token: 0x040001B2 RID: 434
			[FieldOffset(40)]
			private uint m_PressedInUpdate;

			// Token: 0x040001B3 RID: 435
			[FieldOffset(44)]
			private uint m_ReleasedInUpdate;

			// Token: 0x040001B4 RID: 436
			[FieldOffset(48)]
			private uint m_LastCompletedInUpdate;

			// Token: 0x040001B5 RID: 437
			[FieldOffset(52)]
			private int m_Frame;

			// Token: 0x0200004A RID: 74
			[Flags]
			public enum Flags
			{
				// Token: 0x040001B7 RID: 439
				HaveMagnitude = 1,
				// Token: 0x040001B8 RID: 440
				PassThrough = 2,
				// Token: 0x040001B9 RID: 441
				MayNeedConflictResolution = 4,
				// Token: 0x040001BA RID: 442
				HasMultipleConcurrentActuations = 8,
				// Token: 0x040001BB RID: 443
				InProcessing = 16,
				// Token: 0x040001BC RID: 444
				Button = 32,
				// Token: 0x040001BD RID: 445
				Pressed = 64
			}
		}

		// Token: 0x0200004B RID: 75
		public struct ActionMapIndices
		{
			// Token: 0x040001BE RID: 446
			public int actionStartIndex;

			// Token: 0x040001BF RID: 447
			public int actionCount;

			// Token: 0x040001C0 RID: 448
			public int controlStartIndex;

			// Token: 0x040001C1 RID: 449
			public int controlCount;

			// Token: 0x040001C2 RID: 450
			public int bindingStartIndex;

			// Token: 0x040001C3 RID: 451
			public int bindingCount;

			// Token: 0x040001C4 RID: 452
			public int interactionStartIndex;

			// Token: 0x040001C5 RID: 453
			public int interactionCount;

			// Token: 0x040001C6 RID: 454
			public int processorStartIndex;

			// Token: 0x040001C7 RID: 455
			public int processorCount;

			// Token: 0x040001C8 RID: 456
			public int compositeStartIndex;

			// Token: 0x040001C9 RID: 457
			public int compositeCount;
		}

		// Token: 0x0200004C RID: 76
		public struct UnmanagedMemory : IDisposable
		{
			// Token: 0x17000123 RID: 291
			// (get) Token: 0x060003C4 RID: 964 RVA: 0x00010163 File Offset: 0x0000E363
			public bool isAllocated
			{
				get
				{
					return this.basePtr != null;
				}
			}

			// Token: 0x17000124 RID: 292
			// (get) Token: 0x060003C5 RID: 965 RVA: 0x00010174 File Offset: 0x0000E374
			public unsafe int sizeInBytes
			{
				get
				{
					return this.mapCount * sizeof(InputActionState.ActionMapIndices) + this.actionCount * sizeof(InputActionState.TriggerState) + this.bindingCount * sizeof(InputActionState.BindingState) + this.interactionCount * sizeof(InputActionState.InteractionState) + this.controlCount * 4 + this.compositeCount * 4 + this.controlCount * 4 + this.controlCount * 2 * 2 + this.actionCount * 2 * 2 + this.bindingCount * 2 + (this.controlCount + 31) / 32 * 4;
				}
			}

			// Token: 0x060003C6 RID: 966 RVA: 0x00010204 File Offset: 0x0000E404
			public unsafe void Allocate(int mapCount, int actionCount, int bindingCount, int controlCount, int interactionCount, int compositeCount)
			{
				this.mapCount = mapCount;
				this.actionCount = actionCount;
				this.interactionCount = interactionCount;
				this.bindingCount = bindingCount;
				this.controlCount = controlCount;
				this.compositeCount = compositeCount;
				int numBytes = this.sizeInBytes;
				byte* ptr = (byte*)UnsafeUtility.Malloc((long)numBytes, 8, Allocator.Persistent);
				UnsafeUtility.MemClear((void*)ptr, (long)numBytes);
				this.basePtr = (void*)ptr;
				this.actionStates = (InputActionState.TriggerState*)ptr;
				ptr += actionCount * sizeof(InputActionState.TriggerState);
				this.interactionStates = (InputActionState.InteractionState*)ptr;
				ptr += interactionCount * sizeof(InputActionState.InteractionState);
				this.bindingStates = (InputActionState.BindingState*)ptr;
				ptr += bindingCount * sizeof(InputActionState.BindingState);
				this.mapIndices = (InputActionState.ActionMapIndices*)ptr;
				ptr += mapCount * sizeof(InputActionState.ActionMapIndices);
				this.controlMagnitudes = (float*)ptr;
				ptr += controlCount * 4;
				this.compositeMagnitudes = (float*)ptr;
				ptr += compositeCount * 4;
				this.controlIndexToBindingIndex = (int*)ptr;
				ptr += controlCount * 4;
				this.controlGroupingAndComplexity = (ushort*)ptr;
				ptr += controlCount * 2 * 2;
				this.actionBindingIndicesAndCounts = (ushort*)ptr;
				ptr += actionCount * 2 * 2;
				this.actionBindingIndices = (ushort*)ptr;
				ptr += bindingCount * 2;
				this.enabledControls = (int*)ptr;
				ptr += (controlCount + 31) / 32 * 4;
			}

			// Token: 0x060003C7 RID: 967 RVA: 0x00010314 File Offset: 0x0000E514
			public void Dispose()
			{
				if (this.basePtr == null)
				{
					return;
				}
				UnsafeUtility.Free(this.basePtr, Allocator.Persistent);
				this.basePtr = null;
				this.actionStates = null;
				this.interactionStates = null;
				this.bindingStates = null;
				this.mapIndices = null;
				this.controlMagnitudes = null;
				this.compositeMagnitudes = null;
				this.controlIndexToBindingIndex = null;
				this.controlGroupingAndComplexity = null;
				this.actionBindingIndices = null;
				this.actionBindingIndicesAndCounts = null;
				this.mapCount = 0;
				this.actionCount = 0;
				this.bindingCount = 0;
				this.controlCount = 0;
				this.interactionCount = 0;
				this.compositeCount = 0;
			}

			// Token: 0x060003C8 RID: 968 RVA: 0x000103BC File Offset: 0x0000E5BC
			public unsafe void CopyDataFrom(InputActionState.UnmanagedMemory memory)
			{
				UnsafeUtility.MemCpy((void*)this.mapIndices, (void*)memory.mapIndices, (long)(memory.mapCount * sizeof(InputActionState.ActionMapIndices)));
				UnsafeUtility.MemCpy((void*)this.actionStates, (void*)memory.actionStates, (long)(memory.actionCount * sizeof(InputActionState.TriggerState)));
				UnsafeUtility.MemCpy((void*)this.bindingStates, (void*)memory.bindingStates, (long)(memory.bindingCount * sizeof(InputActionState.BindingState)));
				UnsafeUtility.MemCpy((void*)this.interactionStates, (void*)memory.interactionStates, (long)(memory.interactionCount * sizeof(InputActionState.InteractionState)));
				UnsafeUtility.MemCpy((void*)this.controlMagnitudes, (void*)memory.controlMagnitudes, (long)(memory.controlCount * 4));
				UnsafeUtility.MemCpy((void*)this.compositeMagnitudes, (void*)memory.compositeMagnitudes, (long)(memory.compositeCount * 4));
				UnsafeUtility.MemCpy((void*)this.controlIndexToBindingIndex, (void*)memory.controlIndexToBindingIndex, (long)(memory.controlCount * 4));
				UnsafeUtility.MemCpy((void*)this.controlGroupingAndComplexity, (void*)memory.controlGroupingAndComplexity, (long)(memory.controlCount * 2 * 2));
				UnsafeUtility.MemCpy((void*)this.actionBindingIndicesAndCounts, (void*)memory.actionBindingIndicesAndCounts, (long)(memory.actionCount * 2 * 2));
				UnsafeUtility.MemCpy((void*)this.actionBindingIndices, (void*)memory.actionBindingIndices, (long)(memory.bindingCount * 2));
				UnsafeUtility.MemCpy((void*)this.enabledControls, (void*)memory.enabledControls, (long)((memory.controlCount + 31) / 32 * 4));
			}

			// Token: 0x060003C9 RID: 969 RVA: 0x00010508 File Offset: 0x0000E708
			public InputActionState.UnmanagedMemory Clone()
			{
				if (!this.isAllocated)
				{
					return default(InputActionState.UnmanagedMemory);
				}
				InputActionState.UnmanagedMemory clone = default(InputActionState.UnmanagedMemory);
				int num = this.mapCount;
				int num2 = this.actionCount;
				int num3 = this.controlCount;
				clone.Allocate(num, num2, this.bindingCount, num3, this.interactionCount, this.compositeCount);
				clone.CopyDataFrom(this);
				return clone;
			}

			// Token: 0x040001CA RID: 458
			public unsafe void* basePtr;

			// Token: 0x040001CB RID: 459
			public int mapCount;

			// Token: 0x040001CC RID: 460
			public int actionCount;

			// Token: 0x040001CD RID: 461
			public int interactionCount;

			// Token: 0x040001CE RID: 462
			public int bindingCount;

			// Token: 0x040001CF RID: 463
			public int controlCount;

			// Token: 0x040001D0 RID: 464
			public int compositeCount;

			// Token: 0x040001D1 RID: 465
			public unsafe InputActionState.TriggerState* actionStates;

			// Token: 0x040001D2 RID: 466
			public unsafe InputActionState.BindingState* bindingStates;

			// Token: 0x040001D3 RID: 467
			public unsafe InputActionState.InteractionState* interactionStates;

			// Token: 0x040001D4 RID: 468
			public unsafe float* controlMagnitudes;

			// Token: 0x040001D5 RID: 469
			public unsafe float* compositeMagnitudes;

			// Token: 0x040001D6 RID: 470
			public unsafe int* enabledControls;

			// Token: 0x040001D7 RID: 471
			public unsafe ushort* actionBindingIndicesAndCounts;

			// Token: 0x040001D8 RID: 472
			public unsafe ushort* actionBindingIndices;

			// Token: 0x040001D9 RID: 473
			public unsafe int* controlIndexToBindingIndex;

			// Token: 0x040001DA RID: 474
			public unsafe ushort* controlGroupingAndComplexity;

			// Token: 0x040001DB RID: 475
			public bool controlGroupingInitialized;

			// Token: 0x040001DC RID: 476
			public unsafe InputActionState.ActionMapIndices* mapIndices;
		}

		// Token: 0x0200004D RID: 77
		internal struct GlobalState
		{
			// Token: 0x040001DD RID: 477
			internal InlinedArray<GCHandle> globalList;

			// Token: 0x040001DE RID: 478
			internal CallbackArray<Action<object, InputActionChange>> onActionChange;

			// Token: 0x040001DF RID: 479
			internal CallbackArray<Action<object>> onActionControlsChanged;
		}
	}
}
