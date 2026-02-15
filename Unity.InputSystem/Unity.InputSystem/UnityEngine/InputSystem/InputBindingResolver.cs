using System;
using System.Collections.Generic;
using System.Reflection;
using Unity.Collections;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200005C RID: 92
	internal struct InputBindingResolver : IDisposable
	{
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x00011367 File Offset: 0x0000F567
		public int totalMapCount
		{
			get
			{
				return this.memory.mapCount;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x00011374 File Offset: 0x0000F574
		public int totalActionCount
		{
			get
			{
				return this.memory.actionCount;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x00011381 File Offset: 0x0000F581
		public int totalBindingCount
		{
			get
			{
				return this.memory.bindingCount;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x0001138E File Offset: 0x0000F58E
		public int totalControlCount
		{
			get
			{
				return this.memory.controlCount;
			}
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0001139B File Offset: 0x0000F59B
		public void Dispose()
		{
			this.memory.Dispose();
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x000113A8 File Offset: 0x0000F5A8
		public void StartWithPreviousResolve(InputActionState state, bool isFullResolve)
		{
			this.m_IsControlOnlyResolve = !isFullResolve;
			this.maps = state.maps;
			this.interactions = state.interactions;
			this.processors = state.processors;
			this.composites = state.composites;
			this.controls = state.controls;
			if (isFullResolve)
			{
				if (this.maps != null)
				{
					Array.Clear(this.maps, 0, state.totalMapCount);
				}
				if (this.interactions != null)
				{
					Array.Clear(this.interactions, 0, state.totalInteractionCount);
				}
				if (this.processors != null)
				{
					Array.Clear(this.processors, 0, state.totalProcessorCount);
				}
				if (this.composites != null)
				{
					Array.Clear(this.composites, 0, state.totalCompositeCount);
				}
			}
			if (this.controls != null)
			{
				Array.Clear(this.controls, 0, state.totalControlCount);
			}
			state.maps = null;
			state.interactions = null;
			state.processors = null;
			state.composites = null;
			state.controls = null;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x000114A4 File Offset: 0x0000F6A4
		public unsafe void AddActionMap(InputActionMap actionMap)
		{
			InputSystem.EnsureInitialized();
			InputAction[] actionsInThisMap = actionMap.m_Actions;
			InputBinding[] bindingsInThisMap = actionMap.m_Bindings;
			int bindingCountInThisMap = ((bindingsInThisMap != null) ? bindingsInThisMap.Length : 0);
			int actionCountInThisMap = ((actionsInThisMap != null) ? actionsInThisMap.Length : 0);
			int mapIndex = this.totalMapCount;
			int actionStartIndex = this.totalActionCount;
			int bindingStartIndex = this.totalBindingCount;
			int controlStartIndex = this.totalControlCount;
			int interactionStartIndex = this.totalInteractionCount;
			int processorStartIndex = this.totalProcessorCount;
			int compositeStartIndex = this.totalCompositeCount;
			InputActionState.UnmanagedMemory newMemory = default(InputActionState.UnmanagedMemory);
			int num = this.totalMapCount + 1;
			int num2 = this.totalActionCount + actionCountInThisMap;
			int num3 = this.totalBindingCount + bindingCountInThisMap;
			int num4 = this.totalInteractionCount;
			int num5 = this.totalCompositeCount;
			newMemory.Allocate(num, num2, num3, this.totalControlCount, num4, num5);
			if (this.memory.isAllocated)
			{
				newMemory.CopyDataFrom(this.memory);
			}
			int currentCompositeBindingIndex = -1;
			int currentCompositeIndex = -1;
			int currentCompositePartCount = 0;
			int currentCompositeActionIndexInMap = -1;
			InputAction currentCompositeAction = null;
			InputBinding? bindingMaskOnThisMap = actionMap.m_BindingMask;
			ReadOnlyArray<InputDevice>? devicesForThisMap = actionMap.devices;
			bool isSingletonAction = actionMap.m_SingletonAction != null;
			InputControlList<InputControl> resolvedControls = new InputControlList<InputControl>(Allocator.Temp, 0);
			try
			{
				for (int i = 0; i < bindingCountInThisMap; i++)
				{
					InputActionState.BindingState* bindingStatesPtr = newMemory.bindingStates;
					ref InputBinding unresolvedBinding = ref bindingsInThisMap[i];
					int bindingIndex = bindingStartIndex + i;
					bool isComposite = unresolvedBinding.isComposite;
					bool isPartOfComposite = !isComposite && unresolvedBinding.isPartOfComposite;
					InputActionState.BindingState* bindingState = bindingStatesPtr + bindingIndex;
					try
					{
						int firstControlIndex = 0;
						int firstInteractionIndex = -1;
						int firstProcessorIndex = -1;
						int actionIndexForBinding = -1;
						int partIndex = -1;
						int numControls = 0;
						int numInteractions = 0;
						int numProcessors = 0;
						if (isPartOfComposite && currentCompositeBindingIndex == -1)
						{
							throw new InvalidOperationException(string.Format("Binding '{0}' is marked as being part of a composite but the preceding binding is not a composite", unresolvedBinding));
						}
						int actionIndexInMap = -1;
						string actionName = unresolvedBinding.action;
						InputAction action = null;
						if (!isPartOfComposite)
						{
							if (isSingletonAction)
							{
								actionIndexInMap = 0;
							}
							else if (!string.IsNullOrEmpty(actionName))
							{
								actionIndexInMap = actionMap.FindActionIndex(actionName);
							}
							if (actionIndexInMap != -1)
							{
								action = actionsInThisMap[actionIndexInMap];
							}
						}
						else
						{
							actionIndexInMap = currentCompositeActionIndexInMap;
							action = currentCompositeAction;
						}
						if (isComposite)
						{
							currentCompositeBindingIndex = bindingIndex;
							currentCompositeAction = action;
							currentCompositeActionIndexInMap = actionIndexInMap;
						}
						string path = unresolvedBinding.effectivePath;
						bool bindingIsDisabled = string.IsNullOrEmpty(path) || action == null || (!isComposite && this.bindingMask != null && !this.bindingMask.Value.Matches(ref unresolvedBinding, InputBinding.MatchOptions.EmptyGroupMatchesAny)) || (!isComposite && bindingMaskOnThisMap != null && !bindingMaskOnThisMap.Value.Matches(ref unresolvedBinding, InputBinding.MatchOptions.EmptyGroupMatchesAny)) || (!isComposite && action != null && action.m_BindingMask != null && !action.m_BindingMask.Value.Matches(ref unresolvedBinding, InputBinding.MatchOptions.EmptyGroupMatchesAny));
						if (!bindingIsDisabled && !isComposite)
						{
							firstControlIndex = this.memory.controlCount + resolvedControls.Count;
							if (devicesForThisMap != null)
							{
								ReadOnlyArray<InputDevice> list = devicesForThisMap.Value;
								for (int j = 0; j < list.Count; j++)
								{
									InputDevice device = list[j];
									if (device.added)
									{
										numControls += InputControlPath.TryFindControls<InputControl>(device, path, 0, ref resolvedControls);
									}
								}
							}
							else
							{
								numControls = InputSystem.FindControls<InputControl>(path, ref resolvedControls);
							}
						}
						if (!bindingIsDisabled)
						{
							string processorString = unresolvedBinding.effectiveProcessors;
							if (!string.IsNullOrEmpty(processorString))
							{
								firstProcessorIndex = this.InstantiateWithParameters<InputProcessor>(InputProcessor.s_Processors, processorString, ref this.processors, ref this.totalProcessorCount, actionMap, ref unresolvedBinding);
								if (firstProcessorIndex != -1)
								{
									numProcessors = this.totalProcessorCount - firstProcessorIndex;
								}
							}
							if (!string.IsNullOrEmpty(action.m_Processors))
							{
								int index = this.InstantiateWithParameters<InputProcessor>(InputProcessor.s_Processors, action.m_Processors, ref this.processors, ref this.totalProcessorCount, actionMap, ref unresolvedBinding);
								if (index != -1)
								{
									if (firstProcessorIndex == -1)
									{
										firstProcessorIndex = index;
									}
									numProcessors += this.totalProcessorCount - index;
								}
							}
							if (isPartOfComposite)
							{
								if (currentCompositeBindingIndex != -1)
								{
									firstInteractionIndex = bindingStatesPtr[currentCompositeBindingIndex].interactionStartIndex;
									numInteractions = bindingStatesPtr[currentCompositeBindingIndex].interactionCount;
								}
							}
							else
							{
								string interactionString = unresolvedBinding.effectiveInteractions;
								if (!string.IsNullOrEmpty(interactionString))
								{
									firstInteractionIndex = this.InstantiateWithParameters<IInputInteraction>(InputInteraction.s_Interactions, interactionString, ref this.interactions, ref this.totalInteractionCount, actionMap, ref unresolvedBinding);
									if (firstInteractionIndex != -1)
									{
										numInteractions = this.totalInteractionCount - firstInteractionIndex;
									}
								}
								if (!string.IsNullOrEmpty(action.m_Interactions))
								{
									int index2 = this.InstantiateWithParameters<IInputInteraction>(InputInteraction.s_Interactions, action.m_Interactions, ref this.interactions, ref this.totalInteractionCount, actionMap, ref unresolvedBinding);
									if (index2 != -1)
									{
										if (firstInteractionIndex == -1)
										{
											firstInteractionIndex = index2;
										}
										numInteractions += this.totalInteractionCount - index2;
									}
								}
							}
							if (isComposite)
							{
								InputBindingComposite composite = InputBindingResolver.InstantiateBindingComposite(ref unresolvedBinding, actionMap);
								currentCompositeIndex = ArrayHelpers.AppendWithCapacity<InputBindingComposite>(ref this.composites, ref this.totalCompositeCount, composite, 10);
								firstControlIndex = this.memory.controlCount + resolvedControls.Count;
							}
							else if (!isPartOfComposite && currentCompositeBindingIndex != -1)
							{
								currentCompositePartCount = 0;
								currentCompositeBindingIndex = -1;
								currentCompositeIndex = -1;
								currentCompositeAction = null;
								currentCompositeActionIndexInMap = -1;
							}
						}
						if (isPartOfComposite && currentCompositeBindingIndex != -1 && numControls > 0)
						{
							if (string.IsNullOrEmpty(unresolvedBinding.name))
							{
								throw new InvalidOperationException(string.Format("Binding '{0}' that is part of composite '{1}' is missing a name", unresolvedBinding, this.composites[currentCompositeIndex]));
							}
							partIndex = InputBindingResolver.AssignCompositePartIndex(this.composites[currentCompositeIndex], unresolvedBinding.name, ref currentCompositePartCount);
							bindingStatesPtr[currentCompositeBindingIndex].controlCount += numControls;
							actionIndexForBinding = bindingStatesPtr[currentCompositeBindingIndex].actionIndex;
						}
						else if (actionIndexInMap != -1)
						{
							actionIndexForBinding = actionStartIndex + actionIndexInMap;
						}
						*bindingState = new InputActionState.BindingState
						{
							controlStartIndex = firstControlIndex,
							controlCount = numControls,
							interactionStartIndex = firstInteractionIndex,
							interactionCount = numInteractions,
							processorStartIndex = firstProcessorIndex,
							processorCount = numProcessors,
							isComposite = isComposite,
							isPartOfComposite = unresolvedBinding.isPartOfComposite,
							partIndex = partIndex,
							actionIndex = actionIndexForBinding,
							compositeOrCompositeBindingIndex = (isComposite ? currentCompositeIndex : currentCompositeBindingIndex),
							mapIndex = this.totalMapCount,
							wantsInitialStateCheck = (action != null && action.wantsInitialStateCheck)
						};
					}
					catch (Exception exception)
					{
						Debug.LogError(string.Format("{0} while resolving binding '{1}' in action map '{2}'", exception.GetType().Name, unresolvedBinding, actionMap));
						Debug.LogException(exception);
						if (exception.IsExceptionIndicatingBugInCode())
						{
							throw;
						}
					}
				}
				int controlCountInThisMap = resolvedControls.Count;
				int newTotalControlCount = this.memory.controlCount + controlCountInThisMap;
				if (newMemory.interactionCount != this.totalInteractionCount || newMemory.compositeCount != this.totalCompositeCount || newMemory.controlCount != newTotalControlCount)
				{
					InputActionState.UnmanagedMemory finalMemory = default(InputActionState.UnmanagedMemory);
					finalMemory.Allocate(newMemory.mapCount, newMemory.actionCount, newMemory.bindingCount, newTotalControlCount, this.totalInteractionCount, this.totalCompositeCount);
					finalMemory.CopyDataFrom(newMemory);
					newMemory.Dispose();
					newMemory = finalMemory;
				}
				int controlCountInArray = this.memory.controlCount;
				ArrayHelpers.AppendListWithCapacity<InputControl, InputControlList<InputControl>>(ref this.controls, ref controlCountInArray, resolvedControls, 10);
				for (int k = 0; k < bindingCountInThisMap; k++)
				{
					InputActionState.BindingState* ptr = newMemory.bindingStates + (bindingStartIndex + k);
					int numControls2 = ptr->controlCount;
					int startIndex = ptr->controlStartIndex;
					for (int l = 0; l < numControls2; l++)
					{
						newMemory.controlIndexToBindingIndex[startIndex + l] = bindingStartIndex + k;
					}
				}
				for (int m = this.memory.interactionCount; m < newMemory.interactionCount; m++)
				{
					InputActionState.InteractionState* ptr2 = newMemory.interactionStates + m;
					ptr2->phase = InputActionPhase.Waiting;
					ptr2->triggerControlIndex = -1;
				}
				int runningIndexInBindingIndices = this.memory.bindingCount;
				for (int n = 0; n < actionCountInThisMap; n++)
				{
					InputAction action2 = actionsInThisMap[n];
					int actionIndex = actionStartIndex + n;
					action2.m_ActionIndexInState = actionIndex;
					newMemory.actionBindingIndicesAndCounts[actionIndex * 2] = (ushort)runningIndexInBindingIndices;
					int firstBindingIndexForAction = -1;
					int bindingCountForAction = 0;
					int numPossibleConcurrentActuations = 0;
					for (int n2 = 0; n2 < bindingCountInThisMap; n2++)
					{
						int bindingIndex2 = bindingStartIndex + n2;
						InputActionState.BindingState* bindingState2 = newMemory.bindingStates + bindingIndex2;
						if (bindingState2->actionIndex == actionIndex && !bindingState2->isPartOfComposite)
						{
							newMemory.actionBindingIndices[runningIndexInBindingIndices] = (ushort)bindingIndex2;
							runningIndexInBindingIndices++;
							bindingCountForAction++;
							if (firstBindingIndexForAction == -1)
							{
								firstBindingIndexForAction = bindingIndex2;
							}
							if (bindingState2->isComposite)
							{
								if (bindingState2->controlCount > 0)
								{
									numPossibleConcurrentActuations++;
								}
							}
							else
							{
								numPossibleConcurrentActuations += bindingState2->controlCount;
							}
						}
					}
					if (firstBindingIndexForAction == -1)
					{
						firstBindingIndexForAction = 0;
					}
					newMemory.actionBindingIndicesAndCounts[actionIndex * 2 + 1] = (ushort)bindingCountForAction;
					bool isPassThroughAction = action2.type == InputActionType.PassThrough;
					bool isButtonAction = action2.type == InputActionType.Button;
					bool mayNeedConflictResolution = !isPassThroughAction && numPossibleConcurrentActuations > 1;
					newMemory.actionStates[actionIndex] = new InputActionState.TriggerState
					{
						phase = InputActionPhase.Disabled,
						mapIndex = mapIndex,
						controlIndex = -1,
						interactionIndex = -1,
						isPassThrough = isPassThroughAction,
						isButton = isButtonAction,
						mayNeedConflictResolution = mayNeedConflictResolution,
						bindingIndex = firstBindingIndexForAction
					};
				}
				newMemory.mapIndices[mapIndex] = new InputActionState.ActionMapIndices
				{
					actionStartIndex = actionStartIndex,
					actionCount = actionCountInThisMap,
					controlStartIndex = controlStartIndex,
					controlCount = controlCountInThisMap,
					bindingStartIndex = bindingStartIndex,
					bindingCount = bindingCountInThisMap,
					interactionStartIndex = interactionStartIndex,
					interactionCount = this.totalInteractionCount - interactionStartIndex,
					processorStartIndex = processorStartIndex,
					processorCount = this.totalProcessorCount - processorStartIndex,
					compositeStartIndex = compositeStartIndex,
					compositeCount = this.totalCompositeCount - compositeStartIndex
				};
				actionMap.m_MapIndexInState = mapIndex;
				int finalActionMapCount = this.memory.mapCount;
				ArrayHelpers.AppendWithCapacity<InputActionMap>(ref this.maps, ref finalActionMapCount, actionMap, 4);
				this.memory.Dispose();
				this.memory = newMemory;
			}
			catch (Exception)
			{
				newMemory.Dispose();
				throw;
			}
			finally
			{
				resolvedControls.Dispose();
			}
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00011EDC File Offset: 0x000100DC
		private int InstantiateWithParameters<TType>(TypeTable registrations, string namesAndParameters, ref TType[] array, ref int count, InputActionMap actionMap, ref InputBinding binding)
		{
			if (!NameAndParameters.ParseMultiple(namesAndParameters, ref this.m_Parameters))
			{
				return -1;
			}
			int firstIndex = count;
			for (int i = 0; i < this.m_Parameters.Count; i++)
			{
				string objectRegistrationName = this.m_Parameters[i].name;
				Type type = registrations.LookupTypeRegistration(objectRegistrationName);
				if (type == null)
				{
					Debug.LogError(string.Concat(new string[]
					{
						"No ",
						typeof(TType).Name,
						" with name '",
						objectRegistrationName,
						"' (mentioned in '",
						namesAndParameters,
						"') has been registered"
					}));
				}
				else if (!this.m_IsControlOnlyResolve)
				{
					object obj = Activator.CreateInstance(type);
					if (obj is TType)
					{
						TType instance = (TType)((object)obj);
						InputBindingResolver.ApplyParameters(this.m_Parameters[i].parameters, instance, actionMap, ref binding, objectRegistrationName, namesAndParameters);
						ArrayHelpers.AppendWithCapacity<TType>(ref array, ref count, instance, 10);
					}
					else
					{
						Debug.LogError(string.Concat(new string[]
						{
							"Type '",
							type.Name,
							"' registered as '",
							objectRegistrationName,
							"' (mentioned in '",
							namesAndParameters,
							"') is not an ",
							typeof(TType).Name
						}));
					}
				}
				else
				{
					count++;
				}
			}
			return firstIndex;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0001204C File Offset: 0x0001024C
		private static InputBindingComposite InstantiateBindingComposite(ref InputBinding binding, InputActionMap actionMap)
		{
			NameAndParameters nameAndParametersParsed = NameAndParameters.Parse(binding.effectivePath);
			Type type = InputBindingComposite.s_Composites.LookupTypeRegistration(nameAndParametersParsed.name);
			if (type == null)
			{
				throw new InvalidOperationException("No binding composite with name '" + nameAndParametersParsed.name + "' has been registered");
			}
			InputBindingComposite instance = Activator.CreateInstance(type) as InputBindingComposite;
			if (instance == null)
			{
				throw new InvalidOperationException(string.Concat(new string[] { "Registered type '", type.Name, "' used for '", nameAndParametersParsed.name, "' is not an InputBindingComposite" }));
			}
			InputBindingResolver.ApplyParameters(nameAndParametersParsed.parameters, instance, actionMap, ref binding, nameAndParametersParsed.name, binding.effectivePath);
			return instance;
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00012104 File Offset: 0x00010304
		private static void ApplyParameters(ReadOnlyArray<NamedValue> parameters, object instance, InputActionMap actionMap, ref InputBinding binding, string objectRegistrationName, string namesAndParameters)
		{
			foreach (NamedValue parameter in parameters)
			{
				FieldInfo field = instance.GetType().GetField(parameter.name, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (field == null)
				{
					Debug.LogError(string.Concat(new string[]
					{
						"Type '",
						instance.GetType().Name,
						"' registered as '",
						objectRegistrationName,
						"' (mentioned in '",
						namesAndParameters,
						"') has no public field called '",
						parameter.name,
						"'"
					}));
				}
				else
				{
					TypeCode fieldTypeCode = Type.GetTypeCode(field.FieldType);
					InputActionRebindingExtensions.ParameterOverride? parameterOverride = InputActionRebindingExtensions.ParameterOverride.Find(actionMap, ref binding, parameter.name, objectRegistrationName);
					field.SetValue(instance, ((parameterOverride != null) ? parameterOverride.Value.value : parameter.value).ConvertTo(fieldTypeCode).ToObject());
				}
			}
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00012224 File Offset: 0x00010424
		private static int AssignCompositePartIndex(object composite, string name, ref int currentCompositePartCount)
		{
			Type type = composite.GetType();
			FieldInfo field = type.GetField(name, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (field == null)
			{
				throw new InvalidOperationException(string.Format("Cannot find public field '{0}' used as parameter of binding composite '{1}' of type '{2}'", name, composite, type));
			}
			if (field.FieldType != typeof(int))
			{
				throw new InvalidOperationException(string.Format("Field '{0}' used as a parameter of binding composite '{1}' must be of type 'int' but is of type '{2}' instead", name, composite, type.Name));
			}
			int partIndex = (int)field.GetValue(composite);
			if (partIndex == 0)
			{
				int num = currentCompositePartCount + 1;
				currentCompositePartCount = num;
				partIndex = num;
				field.SetValue(composite, partIndex);
			}
			return partIndex;
		}

		// Token: 0x04000217 RID: 535
		public int totalProcessorCount;

		// Token: 0x04000218 RID: 536
		public int totalCompositeCount;

		// Token: 0x04000219 RID: 537
		public int totalInteractionCount;

		// Token: 0x0400021A RID: 538
		public InputActionMap[] maps;

		// Token: 0x0400021B RID: 539
		public InputControl[] controls;

		// Token: 0x0400021C RID: 540
		public InputActionState.UnmanagedMemory memory;

		// Token: 0x0400021D RID: 541
		public IInputInteraction[] interactions;

		// Token: 0x0400021E RID: 542
		public InputProcessor[] processors;

		// Token: 0x0400021F RID: 543
		public InputBindingComposite[] composites;

		// Token: 0x04000220 RID: 544
		public InputBinding? bindingMask;

		// Token: 0x04000221 RID: 545
		private bool m_IsControlOnlyResolve;

		// Token: 0x04000222 RID: 546
		private List<NameAndParameters> m_Parameters;
	}
}
