using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000032 RID: 50
	public static class InputActionRebindingExtensions
	{
		// Token: 0x060001FB RID: 507 RVA: 0x00006A30 File Offset: 0x00004C30
		public static PrimitiveValue? GetParameterValue(this InputAction action, string name, InputBinding bindingMask = default(InputBinding))
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			return action.GetParameterValue(new InputActionRebindingExtensions.ParameterOverride(name, bindingMask, default(PrimitiveValue)));
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00006A74 File Offset: 0x00004C74
		private static PrimitiveValue? GetParameterValue(this InputAction action, InputActionRebindingExtensions.ParameterOverride parameterOverride)
		{
			parameterOverride.bindingMask.action = action.name;
			InputActionMap actionMap = action.GetOrCreateActionMap();
			actionMap.ResolveBindingsIfNecessary();
			using (InputActionRebindingExtensions.ParameterEnumerator enumerator = new InputActionRebindingExtensions.ParameterEnumerable(actionMap.m_State, parameterOverride, actionMap.m_MapIndexInState).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					InputActionRebindingExtensions.Parameter parameter = enumerator.Current;
					return new PrimitiveValue?(PrimitiveValue.FromObject(parameter.field.GetValue(parameter.instance)));
				}
			}
			return null;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00006B1C File Offset: 0x00004D1C
		public static PrimitiveValue? GetParameterValue(this InputAction action, string name, int bindingIndex)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			if (bindingIndex < 0)
			{
				throw new ArgumentOutOfRangeException("bindingIndex");
			}
			int indexOnMap = action.BindingIndexOnActionToBindingIndexOnMap(bindingIndex);
			InputBinding bindingMask = new InputBinding
			{
				id = action.GetOrCreateActionMap().bindings[indexOnMap].id
			};
			return action.GetParameterValue(name, bindingMask);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00006B98 File Offset: 0x00004D98
		public unsafe static TValue? GetParameterValue<TObject, TValue>(this InputAction action, Expression<Func<TObject, TValue>> expr, InputBinding bindingMask = default(InputBinding)) where TValue : struct
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (expr == null)
			{
				throw new ArgumentNullException("expr");
			}
			InputActionRebindingExtensions.ParameterOverride parameterOverride = InputActionRebindingExtensions.ExtractParameterOverride<TObject, TValue>(expr, bindingMask, default(PrimitiveValue));
			PrimitiveValue? value = action.GetParameterValue(parameterOverride);
			if (value == null)
			{
				return null;
			}
			if (Type.GetTypeCode(typeof(TValue)) == value.Value.type)
			{
				PrimitiveValue v = value.Value;
				TValue result = default(TValue);
				UnsafeUtility.MemCpy(UnsafeUtility.AddressOf<TValue>(ref result), (void*)v.valuePtr, (long)UnsafeUtility.SizeOf<TValue>());
				return new TValue?(result);
			}
			return new TValue?((TValue)((object)Convert.ChangeType(value.Value.ToObject(), typeof(TValue))));
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00006C68 File Offset: 0x00004E68
		public static void ApplyParameterOverride<TObject, TValue>(this InputAction action, Expression<Func<TObject, TValue>> expr, TValue value, InputBinding bindingMask = default(InputBinding)) where TValue : struct
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (expr == null)
			{
				throw new ArgumentNullException("expr");
			}
			InputActionMap actionMap = action.GetOrCreateActionMap();
			actionMap.ResolveBindingsIfNecessary();
			bindingMask.action = action.name;
			InputActionRebindingExtensions.ParameterOverride parameterOverride = InputActionRebindingExtensions.ExtractParameterOverride<TObject, TValue>(expr, bindingMask, PrimitiveValue.From<TValue>(value));
			InputActionRebindingExtensions.ApplyParameterOverride(actionMap.m_State, actionMap.m_MapIndexInState, ref actionMap.m_ParameterOverrides, ref actionMap.m_ParameterOverridesCount, parameterOverride);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00006CD8 File Offset: 0x00004ED8
		public static void ApplyParameterOverride<TObject, TValue>(this InputActionMap actionMap, Expression<Func<TObject, TValue>> expr, TValue value, InputBinding bindingMask = default(InputBinding)) where TValue : struct
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			if (expr == null)
			{
				throw new ArgumentNullException("expr");
			}
			actionMap.ResolveBindingsIfNecessary();
			InputActionRebindingExtensions.ParameterOverride parameterOverride = InputActionRebindingExtensions.ExtractParameterOverride<TObject, TValue>(expr, bindingMask, PrimitiveValue.From<TValue>(value));
			InputActionRebindingExtensions.ApplyParameterOverride(actionMap.m_State, actionMap.m_MapIndexInState, ref actionMap.m_ParameterOverrides, ref actionMap.m_ParameterOverridesCount, parameterOverride);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00006D34 File Offset: 0x00004F34
		public static void ApplyParameterOverride<TObject, TValue>(this InputActionAsset asset, Expression<Func<TObject, TValue>> expr, TValue value, InputBinding bindingMask = default(InputBinding)) where TValue : struct
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (expr == null)
			{
				throw new ArgumentNullException("expr");
			}
			asset.ResolveBindingsIfNecessary();
			InputActionRebindingExtensions.ParameterOverride parameterOverride = InputActionRebindingExtensions.ExtractParameterOverride<TObject, TValue>(expr, bindingMask, PrimitiveValue.From<TValue>(value));
			InputActionRebindingExtensions.ApplyParameterOverride(asset.m_SharedStateForAllMaps, -1, ref asset.m_ParameterOverrides, ref asset.m_ParameterOverridesCount, parameterOverride);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00006D90 File Offset: 0x00004F90
		private static InputActionRebindingExtensions.ParameterOverride ExtractParameterOverride<TObject, TValue>(Expression<Func<TObject, TValue>> expr, InputBinding bindingMask = default(InputBinding), PrimitiveValue value = default(PrimitiveValue))
		{
			if (expr == null)
			{
				throw new ArgumentException("Expression must be a LambdaExpression but was a " + expr.GetType().Name + " instead", "expr");
			}
			MemberExpression body = expr.Body as MemberExpression;
			if (body == null)
			{
				UnaryExpression unary = expr.Body as UnaryExpression;
				if (unary != null && unary.NodeType == ExpressionType.Convert)
				{
					MemberExpression b = unary.Operand as MemberExpression;
					if (b != null)
					{
						body = b;
						goto IL_008D;
					}
				}
				throw new ArgumentException("Body in LambdaExpression must be a MemberExpression (x.name) but was a " + expr.GetType().Name + " instead", "expr");
			}
			IL_008D:
			string objectRegistrationName;
			if (typeof(InputProcessor).IsAssignableFrom(typeof(TObject)))
			{
				objectRegistrationName = InputProcessor.s_Processors.FindNameForType(typeof(TObject));
			}
			else if (typeof(IInputInteraction).IsAssignableFrom(typeof(TObject)))
			{
				objectRegistrationName = InputInteraction.s_Interactions.FindNameForType(typeof(TObject));
			}
			else
			{
				if (!typeof(InputBindingComposite).IsAssignableFrom(typeof(TObject)))
				{
					throw new ArgumentException("Given type must be an InputProcessor, IInputInteraction, or InputBindingComposite (was " + typeof(TObject).Name + ")", "TObject");
				}
				objectRegistrationName = InputBindingComposite.s_Composites.FindNameForType(typeof(TObject));
			}
			return new InputActionRebindingExtensions.ParameterOverride(objectRegistrationName, body.Member.Name, bindingMask, value);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00006F10 File Offset: 0x00005110
		public static void ApplyParameterOverride(this InputActionMap actionMap, string name, PrimitiveValue value, InputBinding bindingMask = default(InputBinding))
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			actionMap.ResolveBindingsIfNecessary();
			InputActionRebindingExtensions.ApplyParameterOverride(actionMap.m_State, actionMap.m_MapIndexInState, ref actionMap.m_ParameterOverrides, ref actionMap.m_ParameterOverridesCount, new InputActionRebindingExtensions.ParameterOverride(name, bindingMask, value));
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00006F6C File Offset: 0x0000516C
		public static void ApplyParameterOverride(this InputActionAsset asset, string name, PrimitiveValue value, InputBinding bindingMask = default(InputBinding))
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			asset.ResolveBindingsIfNecessary();
			InputActionRebindingExtensions.ApplyParameterOverride(asset.m_SharedStateForAllMaps, -1, ref asset.m_ParameterOverrides, ref asset.m_ParameterOverridesCount, new InputActionRebindingExtensions.ParameterOverride(name, bindingMask, value));
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00006FC8 File Offset: 0x000051C8
		public static void ApplyParameterOverride(this InputAction action, string name, PrimitiveValue value, InputBinding bindingMask = default(InputBinding))
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			InputActionMap actionMap = action.GetOrCreateActionMap();
			actionMap.ResolveBindingsIfNecessary();
			bindingMask.action = action.name;
			InputActionRebindingExtensions.ApplyParameterOverride(actionMap.m_State, actionMap.m_MapIndexInState, ref actionMap.m_ParameterOverrides, ref actionMap.m_ParameterOverridesCount, new InputActionRebindingExtensions.ParameterOverride(name, bindingMask, value));
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00007034 File Offset: 0x00005234
		public static void ApplyParameterOverride(this InputAction action, string name, PrimitiveValue value, int bindingIndex)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			if (bindingIndex < 0)
			{
				throw new ArgumentOutOfRangeException("bindingIndex");
			}
			int indexOnMap = action.BindingIndexOnActionToBindingIndexOnMap(bindingIndex);
			InputBinding bindingMask = new InputBinding
			{
				id = action.GetOrCreateActionMap().bindings[indexOnMap].id
			};
			action.ApplyParameterOverride(name, value, bindingMask);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x000070B0 File Offset: 0x000052B0
		private static void ApplyParameterOverride(InputActionState state, int mapIndex, ref InputActionRebindingExtensions.ParameterOverride[] parameterOverrides, ref int parameterOverridesCount, InputActionRebindingExtensions.ParameterOverride parameterOverride)
		{
			bool haveExistingOverride = false;
			if (parameterOverrides != null)
			{
				for (int i = 0; i < parameterOverridesCount; i++)
				{
					ref InputActionRebindingExtensions.ParameterOverride p = ref parameterOverrides[i];
					if (string.Equals(p.objectRegistrationName, parameterOverride.objectRegistrationName, StringComparison.OrdinalIgnoreCase) && string.Equals(p.parameter, parameterOverride.parameter, StringComparison.OrdinalIgnoreCase) && p.bindingMask == parameterOverride.bindingMask)
					{
						haveExistingOverride = true;
						p = parameterOverride;
						break;
					}
				}
			}
			if (!haveExistingOverride)
			{
				ArrayHelpers.AppendWithCapacity<InputActionRebindingExtensions.ParameterOverride>(ref parameterOverrides, ref parameterOverridesCount, parameterOverride, 10);
			}
			foreach (InputActionRebindingExtensions.Parameter parameter in new InputActionRebindingExtensions.ParameterEnumerable(state, parameterOverride, mapIndex))
			{
				InputActionMap actionMap = state.GetActionMap(parameter.bindingIndex);
				ref InputBinding binding = ref state.GetBinding(parameter.bindingIndex);
				InputActionRebindingExtensions.ParameterOverride? overrideToApply = InputActionRebindingExtensions.ParameterOverride.Find(actionMap, ref binding, parameterOverride.parameter, parameterOverride.objectRegistrationName);
				if (overrideToApply != null)
				{
					TypeCode fieldTypeCode = Type.GetTypeCode(parameter.field.FieldType);
					parameter.field.SetValue(parameter.instance, overrideToApply.Value.value.ConvertTo(fieldTypeCode).ToObject());
				}
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00007204 File Offset: 0x00005404
		public static int GetBindingIndex(this InputAction action, InputBinding bindingMask)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			ReadOnlyArray<InputBinding> bindings = action.bindings;
			for (int i = 0; i < bindings.Count; i++)
			{
				if (bindingMask.Matches(bindings[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000724C File Offset: 0x0000544C
		public static int GetBindingIndex(this InputActionMap actionMap, InputBinding bindingMask)
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			ReadOnlyArray<InputBinding> bindings = actionMap.bindings;
			for (int i = 0; i < bindings.Count; i++)
			{
				if (bindingMask.Matches(bindings[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00007294 File Offset: 0x00005494
		public static int GetBindingIndex(this InputAction action, string group = null, string path = null)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			return action.GetBindingIndex(new InputBinding(path, null, group, null, null, null));
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000072C4 File Offset: 0x000054C4
		public static InputBinding? GetBindingForControl(this InputAction action, InputControl control)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			int bindingIndex = action.GetBindingIndexForControl(control);
			if (bindingIndex == -1)
			{
				return null;
			}
			return new InputBinding?(action.bindings[bindingIndex]);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00007318 File Offset: 0x00005518
		public unsafe static int GetBindingIndexForControl(this InputAction action, InputControl control)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
			orCreateActionMap.ResolveBindingsIfNecessary();
			InputActionState state = orCreateActionMap.m_State;
			InputControl[] controls = state.controls;
			int controlCount = state.totalControlCount;
			InputActionState.BindingState* bindingStates = state.bindingStates;
			int* controlIndexToBindingIndex = state.controlIndexToBindingIndex;
			int actionIndex = action.m_ActionIndexInState;
			for (int i = 0; i < controlCount; i++)
			{
				if (controls[i] == control)
				{
					int bindingIndexInState = controlIndexToBindingIndex[i];
					if (bindingStates[bindingIndexInState].actionIndex == actionIndex)
					{
						int bindingIndexInMap = state.GetBindingIndexInMap(bindingIndexInState);
						return action.BindingIndexOnMapToBindingIndexOnAction(bindingIndexInMap);
					}
				}
			}
			return -1;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x000073C4 File Offset: 0x000055C4
		public static string GetBindingDisplayString(this InputAction action, InputBinding.DisplayStringOptions options = (InputBinding.DisplayStringOptions)0, string group = null)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			InputBinding bindingMask;
			if (!string.IsNullOrEmpty(group))
			{
				bindingMask = InputBinding.MaskByGroup(group);
			}
			else
			{
				InputBinding? mask = action.FindEffectiveBindingMask();
				if (mask != null)
				{
					bindingMask = mask.Value;
				}
				else
				{
					bindingMask = default(InputBinding);
				}
			}
			return action.GetBindingDisplayString(bindingMask, options);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000741C File Offset: 0x0000561C
		public static string GetBindingDisplayString(this InputAction action, InputBinding bindingMask, InputBinding.DisplayStringOptions options = (InputBinding.DisplayStringOptions)0)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			string result = string.Empty;
			ReadOnlyArray<InputBinding> bindings = action.bindings;
			for (int i = 0; i < bindings.Count; i++)
			{
				if (!bindings[i].isPartOfComposite && bindingMask.Matches(bindings[i]))
				{
					string text = action.GetBindingDisplayString(i, options);
					if (result != "")
					{
						result = result + " | " + text;
					}
					else
					{
						result = text;
					}
				}
			}
			return result;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000074A4 File Offset: 0x000056A4
		public static string GetBindingDisplayString(this InputAction action, int bindingIndex, InputBinding.DisplayStringOptions options = (InputBinding.DisplayStringOptions)0)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			string text;
			string text2;
			return action.GetBindingDisplayString(bindingIndex, out text, out text2, options);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x000074CC File Offset: 0x000056CC
		public unsafe static string GetBindingDisplayString(this InputAction action, int bindingIndex, out string deviceLayoutName, out string controlPath, InputBinding.DisplayStringOptions options = (InputBinding.DisplayStringOptions)0)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			deviceLayoutName = null;
			controlPath = null;
			ReadOnlyArray<InputBinding> bindings = action.bindings;
			int bindingCount = bindings.Count;
			if (bindingIndex < 0 || bindingIndex >= bindingCount)
			{
				throw new ArgumentOutOfRangeException(string.Format("Binding index {0} is out of range on action '{1}' with {2} bindings", bindingIndex, action, bindings.Count), "bindingIndex");
			}
			if (!bindings[bindingIndex].isComposite)
			{
				InputControl control = null;
				InputActionMap actionMap = action.GetOrCreateActionMap();
				actionMap.ResolveBindingsIfNecessary();
				InputActionState actionState = actionMap.m_State;
				int bindingIndexInMap = action.BindingIndexOnActionToBindingIndexOnMap(bindingIndex);
				int bindingIndexInState = actionState.GetBindingIndexInState(actionMap.m_MapIndexInState, bindingIndexInMap);
				InputActionState.BindingState* bindingStatePtr = actionState.bindingStates + bindingIndexInState;
				if (bindingStatePtr->controlCount > 0)
				{
					control = actionState.controls[bindingStatePtr->controlStartIndex];
				}
				InputBinding binding = bindings[bindingIndex];
				if (string.IsNullOrEmpty(binding.effectiveInteractions))
				{
					binding.overrideInteractions = action.interactions;
				}
				else if (!string.IsNullOrEmpty(action.interactions))
				{
					binding.overrideInteractions = binding.effectiveInteractions + ";action.interactions";
				}
				return binding.ToDisplayString(out deviceLayoutName, out controlPath, options, control);
			}
			string compositeName = NameAndParameters.Parse(bindings[bindingIndex].effectivePath).name;
			int firstPartIndex = bindingIndex + 1;
			int lastPartIndex = firstPartIndex;
			while (lastPartIndex < bindingCount && bindings[lastPartIndex].isPartOfComposite)
			{
				lastPartIndex++;
			}
			int partCount = lastPartIndex - firstPartIndex;
			string[] partStrings = new string[partCount];
			for (int i = 0; i < partCount; i++)
			{
				string partString = action.GetBindingDisplayString(firstPartIndex + i, options);
				if (string.IsNullOrEmpty(partString))
				{
					partString = " ";
				}
				partStrings[i] = partString;
			}
			string displayFormatString = InputBindingComposite.GetDisplayFormatString(compositeName);
			if (string.IsNullOrEmpty(displayFormatString))
			{
				return StringHelpers.Join<string>("/", partStrings);
			}
			return StringHelpers.ExpandTemplateString(displayFormatString, delegate(string fragment)
			{
				string result = string.Empty;
				for (int j = 0; j < partCount; j++)
				{
					if (string.Equals(bindings[firstPartIndex + j].name, fragment, StringComparison.InvariantCultureIgnoreCase))
					{
						if (!string.IsNullOrEmpty(result))
						{
							result = result + "|" + partStrings[j];
						}
						else
						{
							result = partStrings[j];
						}
					}
				}
				if (string.IsNullOrEmpty(result))
				{
					result = " ";
				}
				return result;
			});
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000770C File Offset: 0x0000590C
		public static void ApplyBindingOverride(this InputAction action, string newPath, string group = null, string path = null)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			action.ApplyBindingOverride(new InputBinding
			{
				overridePath = newPath,
				groups = group,
				path = path
			});
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00007750 File Offset: 0x00005950
		public static void ApplyBindingOverride(this InputAction action, InputBinding bindingOverride)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			bool enabled = action.enabled;
			if (enabled)
			{
				action.Disable();
			}
			bindingOverride.action = action.name;
			action.GetOrCreateActionMap().ApplyBindingOverride(bindingOverride);
			if (enabled)
			{
				action.Enable();
				action.RequestInitialStateCheckOnEnabledAction();
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000077A4 File Offset: 0x000059A4
		public static void ApplyBindingOverride(this InputAction action, int bindingIndex, InputBinding bindingOverride)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			int indexOnMap = action.BindingIndexOnActionToBindingIndexOnMap(bindingIndex);
			bindingOverride.action = action.name;
			action.GetOrCreateActionMap().ApplyBindingOverride(indexOnMap, bindingOverride);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000077E4 File Offset: 0x000059E4
		public static void ApplyBindingOverride(this InputAction action, int bindingIndex, string path)
		{
			if (path == null)
			{
				throw new ArgumentException("Binding path cannot be null", "path");
			}
			action.ApplyBindingOverride(bindingIndex, new InputBinding
			{
				overridePath = path
			});
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000781C File Offset: 0x00005A1C
		public static int ApplyBindingOverride(this InputActionMap actionMap, InputBinding bindingOverride)
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			InputBinding[] bindings = actionMap.m_Bindings;
			if (bindings == null)
			{
				return 0;
			}
			int bindingCount = bindings.Length;
			int matchCount = 0;
			for (int i = 0; i < bindingCount; i++)
			{
				if (bindingOverride.Matches(ref bindings[i], (InputBinding.MatchOptions)0))
				{
					bindings[i].overridePath = bindingOverride.overridePath;
					bindings[i].overrideInteractions = bindingOverride.overrideInteractions;
					bindings[i].overrideProcessors = bindingOverride.overrideProcessors;
					matchCount++;
				}
			}
			if (matchCount > 0)
			{
				actionMap.OnBindingModified();
			}
			return matchCount;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x000078B0 File Offset: 0x00005AB0
		public static void ApplyBindingOverride(this InputActionMap actionMap, int bindingIndex, InputBinding bindingOverride)
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			InputBinding[] bindings = actionMap.m_Bindings;
			int bindingsCount = ((bindings != null) ? bindings.Length : 0);
			if (bindingIndex < 0 || bindingIndex >= bindingsCount)
			{
				throw new ArgumentOutOfRangeException("bindingIndex", string.Format("Cannot apply override to binding at index {0} in map '{1}' with only {2} bindings", bindingIndex, actionMap, bindingsCount));
			}
			actionMap.m_Bindings[bindingIndex].overridePath = bindingOverride.overridePath;
			actionMap.m_Bindings[bindingIndex].overrideInteractions = bindingOverride.overrideInteractions;
			actionMap.m_Bindings[bindingIndex].overrideProcessors = bindingOverride.overrideProcessors;
			actionMap.OnBindingModified();
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00007954 File Offset: 0x00005B54
		public static void RemoveBindingOverride(this InputAction action, int bindingIndex)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			action.ApplyBindingOverride(bindingIndex, default(InputBinding));
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000797F File Offset: 0x00005B7F
		public static void RemoveBindingOverride(this InputAction action, InputBinding bindingMask)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			bindingMask.overridePath = null;
			bindingMask.overrideInteractions = null;
			bindingMask.overrideProcessors = null;
			action.ApplyBindingOverride(bindingMask);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x000079AE File Offset: 0x00005BAE
		private static void RemoveBindingOverride(this InputActionMap actionMap, InputBinding bindingMask)
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			bindingMask.overridePath = null;
			bindingMask.overrideInteractions = null;
			bindingMask.overrideProcessors = null;
			actionMap.ApplyBindingOverride(bindingMask);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000079E0 File Offset: 0x00005BE0
		public static void RemoveAllBindingOverrides(this IInputActionCollection2 actions)
		{
			if (actions == null)
			{
				throw new ArgumentNullException("actions");
			}
			using (InputActionRebindingExtensions.DeferBindingResolution())
			{
				foreach (InputAction action in actions)
				{
					InputActionMap actionMap = action.GetOrCreateActionMap();
					InputBinding[] bindings = actionMap.m_Bindings;
					int numBindings = bindings.LengthSafe<InputBinding>();
					for (int i = 0; i < numBindings; i++)
					{
						ref InputBinding binding = ref bindings[i];
						if (binding.TriggersAction(action))
						{
							binding.RemoveOverrides();
						}
					}
					actionMap.OnBindingModified();
				}
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00007A98 File Offset: 0x00005C98
		public static void RemoveAllBindingOverrides(this InputAction action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			string actionName = action.name;
			InputActionMap actionMap = action.GetOrCreateActionMap();
			InputBinding[] bindings = actionMap.m_Bindings;
			if (bindings == null)
			{
				return;
			}
			int bindingCount = bindings.Length;
			for (int i = 0; i < bindingCount; i++)
			{
				if (string.Compare(bindings[i].action, actionName, StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					bindings[i].overridePath = null;
					bindings[i].overrideInteractions = null;
					bindings[i].overrideProcessors = null;
				}
			}
			actionMap.OnBindingModified();
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00007B28 File Offset: 0x00005D28
		public static void ApplyBindingOverrides(this InputActionMap actionMap, IEnumerable<InputBinding> overrides)
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			if (overrides == null)
			{
				throw new ArgumentNullException("overrides");
			}
			foreach (InputBinding binding in overrides)
			{
				actionMap.ApplyBindingOverride(binding);
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00007B90 File Offset: 0x00005D90
		public static void RemoveBindingOverrides(this InputActionMap actionMap, IEnumerable<InputBinding> overrides)
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			if (overrides == null)
			{
				throw new ArgumentNullException("overrides");
			}
			foreach (InputBinding binding in overrides)
			{
				actionMap.RemoveBindingOverride(binding);
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00007BF4 File Offset: 0x00005DF4
		public static int ApplyBindingOverridesOnMatchingControls(this InputAction action, InputControl control)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			ReadOnlyArray<InputBinding> bindings = action.bindings;
			int bindingsCount = bindings.Count;
			int numMatchingControls = 0;
			for (int i = 0; i < bindingsCount; i++)
			{
				InputControl matchingControl = InputControlPath.TryFindControl(control, bindings[i].path, 0);
				if (matchingControl != null)
				{
					action.ApplyBindingOverride(i, matchingControl.path);
					numMatchingControls++;
				}
			}
			return numMatchingControls;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00007C6C File Offset: 0x00005E6C
		public static int ApplyBindingOverridesOnMatchingControls(this InputActionMap actionMap, InputControl control)
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			ReadOnlyArray<InputAction> actions = actionMap.actions;
			int actionCount = actions.Count;
			int numMatchingControls = 0;
			for (int i = 0; i < actionCount; i++)
			{
				numMatchingControls = actions[i].ApplyBindingOverridesOnMatchingControls(control);
			}
			return numMatchingControls;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00007CC4 File Offset: 0x00005EC4
		public static string SaveBindingOverridesAsJson(this IInputActionCollection2 actions)
		{
			if (actions == null)
			{
				throw new ArgumentNullException("actions");
			}
			List<InputActionMap.BindingOverrideJson> overrides = new List<InputActionMap.BindingOverrideJson>();
			foreach (InputBinding binding in actions.bindings)
			{
				actions.AddBindingOverrideJsonTo(binding, overrides, null);
			}
			if (overrides.Count == 0)
			{
				return string.Empty;
			}
			return JsonUtility.ToJson(new InputActionMap.BindingOverrideListJson
			{
				bindings = overrides
			});
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00007D50 File Offset: 0x00005F50
		public static string SaveBindingOverridesAsJson(this InputAction action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			bool isSingletonAction = action.isSingletonAction;
			InputActionMap actionMap = action.GetOrCreateActionMap();
			List<InputActionMap.BindingOverrideJson> list = new List<InputActionMap.BindingOverrideJson>();
			foreach (InputBinding binding in action.bindings)
			{
				if (isSingletonAction || binding.TriggersAction(action))
				{
					actionMap.AddBindingOverrideJsonTo(binding, list, isSingletonAction ? action : null);
				}
			}
			if (list.Count == 0)
			{
				return string.Empty;
			}
			return JsonUtility.ToJson(new InputActionMap.BindingOverrideListJson
			{
				bindings = list
			});
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00007E0C File Offset: 0x0000600C
		private static void AddBindingOverrideJsonTo(this IInputActionCollection2 actions, InputBinding binding, List<InputActionMap.BindingOverrideJson> list, InputAction action = null)
		{
			if (!binding.hasOverrides)
			{
				return;
			}
			if (action == null)
			{
				action = actions.FindAction(binding.action, false);
			}
			string actionName = ((action != null && !action.isSingletonAction) ? (action.actionMap.name + "/" + action.name) : "");
			InputActionMap.BindingOverrideJson @override = InputActionMap.BindingOverrideJson.FromBinding(binding, actionName);
			list.Add(@override);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00007E74 File Offset: 0x00006074
		public static void LoadBindingOverridesFromJson(this IInputActionCollection2 actions, string json, bool removeExisting = true)
		{
			if (actions == null)
			{
				throw new ArgumentNullException("actions");
			}
			using (InputActionRebindingExtensions.DeferBindingResolution())
			{
				if (removeExisting)
				{
					actions.RemoveAllBindingOverrides();
				}
				actions.LoadBindingOverridesFromJsonInternal(json);
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00007EC4 File Offset: 0x000060C4
		public static void LoadBindingOverridesFromJson(this InputAction action, string json, bool removeExisting = true)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			using (InputActionRebindingExtensions.DeferBindingResolution())
			{
				if (removeExisting)
				{
					action.RemoveAllBindingOverrides();
				}
				action.GetOrCreateActionMap().LoadBindingOverridesFromJsonInternal(json);
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00007F18 File Offset: 0x00006118
		private static void LoadBindingOverridesFromJsonInternal(this IInputActionCollection2 actions, string json)
		{
			if (string.IsNullOrEmpty(json))
			{
				return;
			}
			foreach (InputActionMap.BindingOverrideJson entry in JsonUtility.FromJson<InputActionMap.BindingOverrideListJson>(json).bindings)
			{
				if (!string.IsNullOrEmpty(entry.id))
				{
					InputAction action;
					int bindingIndex = actions.FindBinding(new InputBinding
					{
						m_Id = entry.id
					}, out action);
					if (bindingIndex != -1)
					{
						action.ApplyBindingOverride(bindingIndex, InputActionMap.BindingOverrideJson.ToBinding(entry));
						continue;
					}
				}
				Debug.LogWarning("Could not override binding as no existing binding was found with the id: " + entry.id);
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00007FC8 File Offset: 0x000061C8
		public static InputActionRebindingExtensions.RebindingOperation PerformInteractiveRebinding(this InputAction action, int bindingIndex = -1)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			InputActionRebindingExtensions.RebindingOperation rebind = new InputActionRebindingExtensions.RebindingOperation().WithAction(action).OnMatchWaitForAnother(0.05f).WithControlsExcluding("<Pointer>/delta")
				.WithControlsExcluding("<Pointer>/position")
				.WithControlsExcluding("<Touchscreen>/touch*/position")
				.WithControlsExcluding("<Touchscreen>/touch*/delta")
				.WithControlsExcluding("<Mouse>/clickCount")
				.WithMatchingEventsBeingSuppressed(true);
			if (rebind.expectedControlType != "Button")
			{
				rebind.WithCancelingThrough("<Keyboard>/escape");
			}
			if (bindingIndex >= 0)
			{
				ReadOnlyArray<InputBinding> bindings = action.bindings;
				if (bindingIndex >= bindings.Count)
				{
					throw new ArgumentOutOfRangeException(string.Format("Binding index {0} is out of range for action '{1}' with {2} bindings", bindingIndex, action, bindings.Count), "bindings");
				}
				if (bindings[bindingIndex].isComposite)
				{
					throw new InvalidOperationException(string.Format("Cannot perform rebinding on composite binding '{0}' of '{1}'", bindings[bindingIndex], action));
				}
				rebind.WithTargetBinding(bindingIndex);
			}
			return rebind;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x000080C5 File Offset: 0x000062C5
		internal static InputActionRebindingExtensions.DeferBindingResolutionWrapper DeferBindingResolution()
		{
			if (InputActionRebindingExtensions.s_DeferBindingResolutionWrapper == null)
			{
				InputActionRebindingExtensions.s_DeferBindingResolutionWrapper = new InputActionRebindingExtensions.DeferBindingResolutionWrapper();
			}
			InputActionRebindingExtensions.s_DeferBindingResolutionWrapper.Acquire();
			return InputActionRebindingExtensions.s_DeferBindingResolutionWrapper;
		}

		// Token: 0x0400010E RID: 270
		private static InputActionRebindingExtensions.DeferBindingResolutionWrapper s_DeferBindingResolutionWrapper;

		// Token: 0x02000033 RID: 51
		internal struct Parameter
		{
			// Token: 0x0400010F RID: 271
			public object instance;

			// Token: 0x04000110 RID: 272
			public FieldInfo field;

			// Token: 0x04000111 RID: 273
			public int bindingIndex;
		}

		// Token: 0x02000034 RID: 52
		private struct ParameterEnumerable : IEnumerable<InputActionRebindingExtensions.Parameter>, IEnumerable
		{
			// Token: 0x06000228 RID: 552 RVA: 0x000080E7 File Offset: 0x000062E7
			public ParameterEnumerable(InputActionState state, InputActionRebindingExtensions.ParameterOverride parameter, int mapIndex = -1)
			{
				this.m_State = state;
				this.m_Parameter = parameter;
				this.m_MapIndex = mapIndex;
			}

			// Token: 0x06000229 RID: 553 RVA: 0x000080FE File Offset: 0x000062FE
			public InputActionRebindingExtensions.ParameterEnumerator GetEnumerator()
			{
				return new InputActionRebindingExtensions.ParameterEnumerator(this.m_State, this.m_Parameter, this.m_MapIndex);
			}

			// Token: 0x0600022A RID: 554 RVA: 0x00008117 File Offset: 0x00006317
			IEnumerator<InputActionRebindingExtensions.Parameter> IEnumerable<InputActionRebindingExtensions.Parameter>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600022B RID: 555 RVA: 0x00008117 File Offset: 0x00006317
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04000112 RID: 274
			private InputActionState m_State;

			// Token: 0x04000113 RID: 275
			private InputActionRebindingExtensions.ParameterOverride m_Parameter;

			// Token: 0x04000114 RID: 276
			private int m_MapIndex;
		}

		// Token: 0x02000035 RID: 53
		private struct ParameterEnumerator : IEnumerator<InputActionRebindingExtensions.Parameter>, IEnumerator, IDisposable
		{
			// Token: 0x0600022C RID: 556 RVA: 0x00008124 File Offset: 0x00006324
			public ParameterEnumerator(InputActionState state, InputActionRebindingExtensions.ParameterOverride parameter, int mapIndex = -1)
			{
				this = default(InputActionRebindingExtensions.ParameterEnumerator);
				this.m_State = state;
				this.m_ParameterName = parameter.parameter;
				this.m_MapIndex = mapIndex;
				this.m_ObjectType = parameter.objectType;
				this.m_MayBeComposite = this.m_ObjectType == null || typeof(InputBindingComposite).IsAssignableFrom(this.m_ObjectType);
				this.m_MayBeProcessor = this.m_ObjectType == null || typeof(InputProcessor).IsAssignableFrom(this.m_ObjectType);
				this.m_MayBeInteraction = this.m_ObjectType == null || typeof(IInputInteraction).IsAssignableFrom(this.m_ObjectType);
				this.m_BindingMask = parameter.bindingMask;
				this.Reset();
			}

			// Token: 0x0600022D RID: 557 RVA: 0x000081F8 File Offset: 0x000063F8
			private bool MoveToNextBinding()
			{
				ref InputActionState.BindingState bindingState;
				for (;;)
				{
					this.m_BindingCurrentIndex++;
					if (this.m_BindingCurrentIndex >= this.m_BindingEndIndex)
					{
						break;
					}
					ref InputBinding binding = ref this.m_State.GetBinding(this.m_BindingCurrentIndex);
					bindingState = this.m_State.GetBindingState(this.m_BindingCurrentIndex);
					if ((bindingState.processorCount != 0 || bindingState.interactionCount != 0 || binding.isComposite) && (!this.m_MayBeComposite || this.m_MayBeProcessor || this.m_MayBeInteraction || binding.isComposite) && (!this.m_MayBeProcessor || this.m_MayBeComposite || this.m_MayBeInteraction || bindingState.processorCount != 0) && (!this.m_MayBeInteraction || this.m_MayBeComposite || this.m_MayBeProcessor || bindingState.interactionCount != 0) && this.m_BindingMask.Matches(ref binding, (InputBinding.MatchOptions)0))
					{
						goto Block_12;
					}
				}
				return false;
				Block_12:
				if (this.m_MayBeComposite)
				{
					ref InputBinding binding;
					this.m_CurrentBindingIsComposite = binding.isComposite;
				}
				this.m_ProcessorCurrentIndex = bindingState.processorStartIndex - 1;
				this.m_ProcessorEndIndex = bindingState.processorStartIndex + bindingState.processorCount;
				this.m_InteractionCurrentIndex = bindingState.interactionStartIndex - 1;
				this.m_InteractionEndIndex = bindingState.interactionStartIndex + bindingState.interactionCount;
				return true;
			}

			// Token: 0x0600022E RID: 558 RVA: 0x00008330 File Offset: 0x00006530
			private bool MoveToNextInteraction()
			{
				while (this.m_InteractionCurrentIndex < this.m_InteractionEndIndex)
				{
					this.m_InteractionCurrentIndex++;
					if (this.m_InteractionCurrentIndex == this.m_InteractionEndIndex)
					{
						break;
					}
					IInputInteraction interaction = this.m_State.interactions[this.m_InteractionCurrentIndex];
					if (this.FindParameter(interaction))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x0600022F RID: 559 RVA: 0x00008388 File Offset: 0x00006588
			private bool MoveToNextProcessor()
			{
				while (this.m_ProcessorCurrentIndex < this.m_ProcessorEndIndex)
				{
					this.m_ProcessorCurrentIndex++;
					if (this.m_ProcessorCurrentIndex == this.m_ProcessorEndIndex)
					{
						break;
					}
					InputProcessor processor = this.m_State.processors[this.m_ProcessorCurrentIndex];
					if (this.FindParameter(processor))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000230 RID: 560 RVA: 0x000083E0 File Offset: 0x000065E0
			private bool FindParameter(object instance)
			{
				if (this.m_ObjectType != null && !this.m_ObjectType.IsInstanceOfType(instance))
				{
					return false;
				}
				FieldInfo field = instance.GetType().GetField(this.m_ParameterName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
				if (field == null)
				{
					return false;
				}
				this.m_CurrentParameter = field;
				this.m_CurrentObject = instance;
				return true;
			}

			// Token: 0x06000231 RID: 561 RVA: 0x0000843C File Offset: 0x0000663C
			public bool MoveNext()
			{
				while (!this.m_MayBeInteraction || !this.MoveToNextInteraction())
				{
					if (this.m_MayBeProcessor && this.MoveToNextProcessor())
					{
						return true;
					}
					if (!this.MoveToNextBinding())
					{
						return false;
					}
					if (this.m_MayBeComposite && this.m_CurrentBindingIsComposite)
					{
						int compositeIndex = this.m_State.GetBindingState(this.m_BindingCurrentIndex).compositeOrCompositeBindingIndex;
						InputBindingComposite composite = this.m_State.composites[compositeIndex];
						if (this.FindParameter(composite))
						{
							return true;
						}
					}
				}
				return true;
			}

			// Token: 0x06000232 RID: 562 RVA: 0x000084B8 File Offset: 0x000066B8
			public unsafe void Reset()
			{
				this.m_CurrentObject = null;
				this.m_CurrentParameter = null;
				this.m_InteractionCurrentIndex = 0;
				this.m_InteractionEndIndex = 0;
				this.m_ProcessorCurrentIndex = 0;
				this.m_ProcessorEndIndex = 0;
				this.m_CurrentBindingIsComposite = false;
				if (this.m_MapIndex < 0)
				{
					this.m_BindingCurrentIndex = -1;
					this.m_BindingEndIndex = this.m_State.totalBindingCount;
					return;
				}
				this.m_BindingCurrentIndex = this.m_State.mapIndices[this.m_MapIndex].bindingStartIndex - 1;
				this.m_BindingEndIndex = this.m_State.mapIndices[this.m_MapIndex].bindingStartIndex + this.m_State.mapIndices[this.m_MapIndex].bindingCount;
			}

			// Token: 0x170000C2 RID: 194
			// (get) Token: 0x06000233 RID: 563 RVA: 0x00008584 File Offset: 0x00006784
			public InputActionRebindingExtensions.Parameter Current
			{
				get
				{
					return new InputActionRebindingExtensions.Parameter
					{
						instance = this.m_CurrentObject,
						field = this.m_CurrentParameter,
						bindingIndex = this.m_BindingCurrentIndex
					};
				}
			}

			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x06000234 RID: 564 RVA: 0x000085C1 File Offset: 0x000067C1
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000235 RID: 565 RVA: 0x000049FE File Offset: 0x00002BFE
			public void Dispose()
			{
			}

			// Token: 0x04000115 RID: 277
			private InputActionState m_State;

			// Token: 0x04000116 RID: 278
			private int m_MapIndex;

			// Token: 0x04000117 RID: 279
			private int m_BindingCurrentIndex;

			// Token: 0x04000118 RID: 280
			private int m_BindingEndIndex;

			// Token: 0x04000119 RID: 281
			private int m_InteractionCurrentIndex;

			// Token: 0x0400011A RID: 282
			private int m_InteractionEndIndex;

			// Token: 0x0400011B RID: 283
			private int m_ProcessorCurrentIndex;

			// Token: 0x0400011C RID: 284
			private int m_ProcessorEndIndex;

			// Token: 0x0400011D RID: 285
			private InputBinding m_BindingMask;

			// Token: 0x0400011E RID: 286
			private Type m_ObjectType;

			// Token: 0x0400011F RID: 287
			private string m_ParameterName;

			// Token: 0x04000120 RID: 288
			private bool m_MayBeInteraction;

			// Token: 0x04000121 RID: 289
			private bool m_MayBeProcessor;

			// Token: 0x04000122 RID: 290
			private bool m_MayBeComposite;

			// Token: 0x04000123 RID: 291
			private bool m_CurrentBindingIsComposite;

			// Token: 0x04000124 RID: 292
			private object m_CurrentObject;

			// Token: 0x04000125 RID: 293
			private FieldInfo m_CurrentParameter;
		}

		// Token: 0x02000036 RID: 54
		internal struct ParameterOverride
		{
			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x06000236 RID: 566 RVA: 0x000085CE File Offset: 0x000067CE
			public Type objectType
			{
				get
				{
					Type type;
					if ((type = InputProcessor.s_Processors.LookupTypeRegistration(this.objectRegistrationName)) == null)
					{
						type = InputInteraction.s_Interactions.LookupTypeRegistration(this.objectRegistrationName) ?? InputBindingComposite.s_Composites.LookupTypeRegistration(this.objectRegistrationName);
					}
					return type;
				}
			}

			// Token: 0x06000237 RID: 567 RVA: 0x00008608 File Offset: 0x00006808
			public ParameterOverride(string parameterName, InputBinding bindingMask, PrimitiveValue value = default(PrimitiveValue))
			{
				int colonIndex = parameterName.IndexOf(':');
				if (colonIndex < 0)
				{
					this.objectRegistrationName = null;
					this.parameter = parameterName;
				}
				else
				{
					this.objectRegistrationName = parameterName.Substring(0, colonIndex);
					this.parameter = parameterName.Substring(colonIndex + 1);
				}
				this.bindingMask = bindingMask;
				this.value = value;
			}

			// Token: 0x06000238 RID: 568 RVA: 0x0000865D File Offset: 0x0000685D
			public ParameterOverride(string objectRegistrationName, string parameterName, InputBinding bindingMask, PrimitiveValue value = default(PrimitiveValue))
			{
				this.objectRegistrationName = objectRegistrationName;
				this.parameter = parameterName;
				this.bindingMask = bindingMask;
				this.value = value;
			}

			// Token: 0x06000239 RID: 569 RVA: 0x0000867C File Offset: 0x0000687C
			public static InputActionRebindingExtensions.ParameterOverride? Find(InputActionMap actionMap, ref InputBinding binding, string parameterName, string objectRegistrationName)
			{
				InputActionRebindingExtensions.ParameterOverride? parameterOverride = InputActionRebindingExtensions.ParameterOverride.Find(actionMap.m_ParameterOverrides, actionMap.m_ParameterOverridesCount, ref binding, parameterName, objectRegistrationName);
				InputActionAsset asset = actionMap.asset;
				InputActionRebindingExtensions.ParameterOverride? overrideOnAsset = ((asset != null) ? InputActionRebindingExtensions.ParameterOverride.Find(asset.m_ParameterOverrides, asset.m_ParameterOverridesCount, ref binding, parameterName, objectRegistrationName) : null);
				return InputActionRebindingExtensions.ParameterOverride.PickMoreSpecificOne(parameterOverride, overrideOnAsset);
			}

			// Token: 0x0600023A RID: 570 RVA: 0x000086D4 File Offset: 0x000068D4
			private static InputActionRebindingExtensions.ParameterOverride? Find(InputActionRebindingExtensions.ParameterOverride[] overrides, int overrideCount, ref InputBinding binding, string parameterName, string objectRegistrationName)
			{
				InputActionRebindingExtensions.ParameterOverride? result = null;
				for (int i = 0; i < overrideCount; i++)
				{
					ref InputActionRebindingExtensions.ParameterOverride current = ref overrides[i];
					if (string.Equals(parameterName, current.parameter, StringComparison.OrdinalIgnoreCase) && current.bindingMask.Matches(binding) && (current.objectRegistrationName == null || string.Equals(current.objectRegistrationName, objectRegistrationName, StringComparison.OrdinalIgnoreCase)))
					{
						if (result == null)
						{
							result = new InputActionRebindingExtensions.ParameterOverride?(current);
						}
						else
						{
							result = InputActionRebindingExtensions.ParameterOverride.PickMoreSpecificOne(result, new InputActionRebindingExtensions.ParameterOverride?(current));
						}
					}
				}
				return result;
			}

			// Token: 0x0600023B RID: 571 RVA: 0x00008764 File Offset: 0x00006964
			private static InputActionRebindingExtensions.ParameterOverride? PickMoreSpecificOne(InputActionRebindingExtensions.ParameterOverride? first, InputActionRebindingExtensions.ParameterOverride? second)
			{
				if (first == null)
				{
					return second;
				}
				if (second == null)
				{
					return first;
				}
				if (first.Value.objectRegistrationName != null && second.Value.objectRegistrationName == null)
				{
					return first;
				}
				if (second.Value.objectRegistrationName != null && first.Value.objectRegistrationName == null)
				{
					return second;
				}
				InputActionRebindingExtensions.ParameterOverride parameterOverride = first.Value;
				if (parameterOverride.bindingMask.effectivePath != null)
				{
					parameterOverride = second.Value;
					if (parameterOverride.bindingMask.effectivePath == null)
					{
						return first;
					}
				}
				parameterOverride = second.Value;
				if (parameterOverride.bindingMask.effectivePath != null)
				{
					parameterOverride = first.Value;
					if (parameterOverride.bindingMask.effectivePath == null)
					{
						return second;
					}
				}
				parameterOverride = first.Value;
				if (parameterOverride.bindingMask.action != null)
				{
					parameterOverride = second.Value;
					if (parameterOverride.bindingMask.action == null)
					{
						return first;
					}
				}
				parameterOverride = second.Value;
				if (parameterOverride.bindingMask.action != null)
				{
					parameterOverride = first.Value;
					if (parameterOverride.bindingMask.action == null)
					{
						return second;
					}
				}
				return first;
			}

			// Token: 0x04000126 RID: 294
			public string objectRegistrationName;

			// Token: 0x04000127 RID: 295
			public string parameter;

			// Token: 0x04000128 RID: 296
			public InputBinding bindingMask;

			// Token: 0x04000129 RID: 297
			public PrimitiveValue value;
		}

		// Token: 0x02000037 RID: 55
		public sealed class RebindingOperation : IDisposable
		{
			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x0600023C RID: 572 RVA: 0x0000887C File Offset: 0x00006A7C
			public InputAction action
			{
				get
				{
					return this.m_ActionToRebind;
				}
			}

			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x0600023D RID: 573 RVA: 0x00008884 File Offset: 0x00006A84
			public InputBinding? bindingMask
			{
				get
				{
					return this.m_BindingMask;
				}
			}

			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x0600023E RID: 574 RVA: 0x0000888C File Offset: 0x00006A8C
			public InputControlList<InputControl> candidates
			{
				get
				{
					return this.m_Candidates;
				}
			}

			// Token: 0x170000C8 RID: 200
			// (get) Token: 0x0600023F RID: 575 RVA: 0x00008894 File Offset: 0x00006A94
			public ReadOnlyArray<float> scores
			{
				get
				{
					return new ReadOnlyArray<float>(this.m_Scores, 0, this.m_Candidates.Count);
				}
			}

			// Token: 0x170000C9 RID: 201
			// (get) Token: 0x06000240 RID: 576 RVA: 0x000088AD File Offset: 0x00006AAD
			public ReadOnlyArray<float> magnitudes
			{
				get
				{
					return new ReadOnlyArray<float>(this.m_Magnitudes, 0, this.m_Candidates.Count);
				}
			}

			// Token: 0x170000CA RID: 202
			// (get) Token: 0x06000241 RID: 577 RVA: 0x000088C6 File Offset: 0x00006AC6
			public InputControl selectedControl
			{
				get
				{
					if (this.m_Candidates.Count == 0)
					{
						return null;
					}
					return this.m_Candidates[0];
				}
			}

			// Token: 0x170000CB RID: 203
			// (get) Token: 0x06000242 RID: 578 RVA: 0x000088E3 File Offset: 0x00006AE3
			public bool started
			{
				get
				{
					return (this.m_Flags & InputActionRebindingExtensions.RebindingOperation.Flags.Started) > (InputActionRebindingExtensions.RebindingOperation.Flags)0;
				}
			}

			// Token: 0x170000CC RID: 204
			// (get) Token: 0x06000243 RID: 579 RVA: 0x000088F0 File Offset: 0x00006AF0
			public bool completed
			{
				get
				{
					return (this.m_Flags & InputActionRebindingExtensions.RebindingOperation.Flags.Completed) > (InputActionRebindingExtensions.RebindingOperation.Flags)0;
				}
			}

			// Token: 0x170000CD RID: 205
			// (get) Token: 0x06000244 RID: 580 RVA: 0x000088FD File Offset: 0x00006AFD
			public bool canceled
			{
				get
				{
					return (this.m_Flags & InputActionRebindingExtensions.RebindingOperation.Flags.Canceled) > (InputActionRebindingExtensions.RebindingOperation.Flags)0;
				}
			}

			// Token: 0x170000CE RID: 206
			// (get) Token: 0x06000245 RID: 581 RVA: 0x0000890A File Offset: 0x00006B0A
			public double startTime
			{
				get
				{
					return this.m_StartTime;
				}
			}

			// Token: 0x170000CF RID: 207
			// (get) Token: 0x06000246 RID: 582 RVA: 0x00008912 File Offset: 0x00006B12
			public float timeout
			{
				get
				{
					return this.m_Timeout;
				}
			}

			// Token: 0x170000D0 RID: 208
			// (get) Token: 0x06000247 RID: 583 RVA: 0x0000891A File Offset: 0x00006B1A
			public string expectedControlType
			{
				get
				{
					return this.m_ExpectedLayout;
				}
			}

			// Token: 0x06000248 RID: 584 RVA: 0x00008928 File Offset: 0x00006B28
			public InputActionRebindingExtensions.RebindingOperation WithAction(InputAction action)
			{
				this.ThrowIfRebindInProgress();
				if (action == null)
				{
					throw new ArgumentNullException("action");
				}
				if (action.enabled)
				{
					throw new InvalidOperationException(string.Format("Cannot rebind action '{0}' while it is enabled", action));
				}
				this.m_ActionToRebind = action;
				if (!string.IsNullOrEmpty(action.expectedControlType))
				{
					this.WithExpectedControlType(action.expectedControlType);
				}
				else if (action.type == InputActionType.Button)
				{
					this.WithExpectedControlType("Button");
				}
				return this;
			}

			// Token: 0x06000249 RID: 585 RVA: 0x0000899B File Offset: 0x00006B9B
			public InputActionRebindingExtensions.RebindingOperation WithMatchingEventsBeingSuppressed(bool value = true)
			{
				this.ThrowIfRebindInProgress();
				if (value)
				{
					this.m_Flags |= InputActionRebindingExtensions.RebindingOperation.Flags.SuppressMatchingEvents;
				}
				else
				{
					this.m_Flags &= ~InputActionRebindingExtensions.RebindingOperation.Flags.SuppressMatchingEvents;
				}
				return this;
			}

			// Token: 0x0600024A RID: 586 RVA: 0x000089CD File Offset: 0x00006BCD
			public InputActionRebindingExtensions.RebindingOperation WithCancelingThrough(string binding)
			{
				this.ThrowIfRebindInProgress();
				this.m_CancelBinding = binding;
				return this;
			}

			// Token: 0x0600024B RID: 587 RVA: 0x000089DD File Offset: 0x00006BDD
			public InputActionRebindingExtensions.RebindingOperation WithCancelingThrough(InputControl control)
			{
				this.ThrowIfRebindInProgress();
				if (control == null)
				{
					throw new ArgumentNullException("control");
				}
				return this.WithCancelingThrough(control.path);
			}

			// Token: 0x0600024C RID: 588 RVA: 0x000089FF File Offset: 0x00006BFF
			public InputActionRebindingExtensions.RebindingOperation WithExpectedControlType(string layoutName)
			{
				this.ThrowIfRebindInProgress();
				this.m_ExpectedLayout = new InternedString(layoutName);
				return this;
			}

			// Token: 0x0600024D RID: 589 RVA: 0x00008A14 File Offset: 0x00006C14
			public InputActionRebindingExtensions.RebindingOperation WithExpectedControlType(Type type)
			{
				this.ThrowIfRebindInProgress();
				if (type != null && !typeof(InputControl).IsAssignableFrom(type))
				{
					throw new ArgumentException("Type '" + type.Name + "' is not an InputControl", "type");
				}
				this.m_ControlType = type;
				return this;
			}

			// Token: 0x0600024E RID: 590 RVA: 0x00008A6A File Offset: 0x00006C6A
			public InputActionRebindingExtensions.RebindingOperation WithExpectedControlType<TControl>() where TControl : InputControl
			{
				this.ThrowIfRebindInProgress();
				return this.WithExpectedControlType(typeof(TControl));
			}

			// Token: 0x0600024F RID: 591 RVA: 0x00008A84 File Offset: 0x00006C84
			public InputActionRebindingExtensions.RebindingOperation WithTargetBinding(int bindingIndex)
			{
				if (bindingIndex < 0)
				{
					throw new ArgumentOutOfRangeException("bindingIndex");
				}
				this.m_TargetBindingIndex = bindingIndex;
				if (this.m_ActionToRebind != null && bindingIndex < this.m_ActionToRebind.bindings.Count)
				{
					InputBinding binding = this.m_ActionToRebind.bindings[bindingIndex];
					if (binding.isPartOfComposite)
					{
						string nameOfComposite = this.m_ActionToRebind.ChangeBinding(bindingIndex).PreviousCompositeBinding(null).binding.GetNameOfComposite();
						string partName = binding.name;
						string expectedLayout = InputBindingComposite.GetExpectedControlLayoutName(nameOfComposite, partName);
						if (!string.IsNullOrEmpty(expectedLayout))
						{
							this.WithExpectedControlType(expectedLayout);
						}
					}
					InputActionMap actionMap = this.action.actionMap;
					InputActionAsset asset = ((actionMap != null) ? actionMap.asset : null);
					if (asset != null && !string.IsNullOrEmpty(binding.groups))
					{
						string[] array = binding.groups.Split(';', StringSplitOptions.None);
						for (int i = 0; i < array.Length; i++)
						{
							string group = array[i];
							int controlSchemeIndex = asset.controlSchemes.IndexOf((InputControlScheme x) => group.Equals(x.bindingGroup, StringComparison.InvariantCultureIgnoreCase));
							if (controlSchemeIndex != -1)
							{
								foreach (InputControlScheme.DeviceRequirement requirement in asset.controlSchemes[controlSchemeIndex].deviceRequirements)
								{
									this.WithControlsHavingToMatchPath(requirement.controlPath);
								}
							}
						}
					}
				}
				return this;
			}

			// Token: 0x06000250 RID: 592 RVA: 0x00008C30 File Offset: 0x00006E30
			public InputActionRebindingExtensions.RebindingOperation WithBindingMask(InputBinding? bindingMask)
			{
				this.m_BindingMask = bindingMask;
				return this;
			}

			// Token: 0x06000251 RID: 593 RVA: 0x00008C3C File Offset: 0x00006E3C
			public InputActionRebindingExtensions.RebindingOperation WithBindingGroup(string group)
			{
				return this.WithBindingMask(new InputBinding?(new InputBinding
				{
					groups = group
				}));
			}

			// Token: 0x06000252 RID: 594 RVA: 0x00008C65 File Offset: 0x00006E65
			public InputActionRebindingExtensions.RebindingOperation WithoutGeneralizingPathOfSelectedControl()
			{
				this.m_Flags |= InputActionRebindingExtensions.RebindingOperation.Flags.DontGeneralizePathOfSelectedControl;
				return this;
			}

			// Token: 0x06000253 RID: 595 RVA: 0x00008C7A File Offset: 0x00006E7A
			public InputActionRebindingExtensions.RebindingOperation WithRebindAddingNewBinding(string group = null)
			{
				this.m_Flags |= InputActionRebindingExtensions.RebindingOperation.Flags.AddNewBinding;
				this.m_BindingGroupForNewBinding = group;
				return this;
			}

			// Token: 0x06000254 RID: 596 RVA: 0x00008C96 File Offset: 0x00006E96
			public InputActionRebindingExtensions.RebindingOperation WithMagnitudeHavingToBeGreaterThan(float magnitude)
			{
				this.ThrowIfRebindInProgress();
				if (magnitude < 0f)
				{
					throw new ArgumentException(string.Format("Magnitude has to be positive but was {0}", magnitude), "magnitude");
				}
				this.m_MagnitudeThreshold = magnitude;
				return this;
			}

			// Token: 0x06000255 RID: 597 RVA: 0x00008CC9 File Offset: 0x00006EC9
			public InputActionRebindingExtensions.RebindingOperation WithoutIgnoringNoisyControls()
			{
				this.ThrowIfRebindInProgress();
				this.m_Flags |= InputActionRebindingExtensions.RebindingOperation.Flags.DontIgnoreNoisyControls;
				return this;
			}

			// Token: 0x06000256 RID: 598 RVA: 0x00008CE4 File Offset: 0x00006EE4
			public InputActionRebindingExtensions.RebindingOperation WithControlsHavingToMatchPath(string path)
			{
				this.ThrowIfRebindInProgress();
				if (string.IsNullOrEmpty(path))
				{
					throw new ArgumentNullException("path");
				}
				for (int i = 0; i < this.m_IncludePathCount; i++)
				{
					if (string.Compare(this.m_IncludePaths[i], path, StringComparison.InvariantCultureIgnoreCase) == 0)
					{
						return this;
					}
				}
				ArrayHelpers.AppendWithCapacity<string>(ref this.m_IncludePaths, ref this.m_IncludePathCount, path, 10);
				return this;
			}

			// Token: 0x06000257 RID: 599 RVA: 0x00008D44 File Offset: 0x00006F44
			public InputActionRebindingExtensions.RebindingOperation WithControlsExcluding(string path)
			{
				this.ThrowIfRebindInProgress();
				if (string.IsNullOrEmpty(path))
				{
					throw new ArgumentNullException("path");
				}
				for (int i = 0; i < this.m_ExcludePathCount; i++)
				{
					if (string.Compare(this.m_ExcludePaths[i], path, StringComparison.InvariantCultureIgnoreCase) == 0)
					{
						return this;
					}
				}
				ArrayHelpers.AppendWithCapacity<string>(ref this.m_ExcludePaths, ref this.m_ExcludePathCount, path, 10);
				return this;
			}

			// Token: 0x06000258 RID: 600 RVA: 0x00008DA4 File Offset: 0x00006FA4
			public InputActionRebindingExtensions.RebindingOperation WithTimeout(float timeInSeconds)
			{
				this.m_Timeout = timeInSeconds;
				return this;
			}

			// Token: 0x06000259 RID: 601 RVA: 0x00008DAE File Offset: 0x00006FAE
			public InputActionRebindingExtensions.RebindingOperation OnComplete(Action<InputActionRebindingExtensions.RebindingOperation> callback)
			{
				this.m_OnComplete = callback;
				return this;
			}

			// Token: 0x0600025A RID: 602 RVA: 0x00008DB8 File Offset: 0x00006FB8
			public InputActionRebindingExtensions.RebindingOperation OnCancel(Action<InputActionRebindingExtensions.RebindingOperation> callback)
			{
				this.m_OnCancel = callback;
				return this;
			}

			// Token: 0x0600025B RID: 603 RVA: 0x00008DC2 File Offset: 0x00006FC2
			public InputActionRebindingExtensions.RebindingOperation OnPotentialMatch(Action<InputActionRebindingExtensions.RebindingOperation> callback)
			{
				this.m_OnPotentialMatch = callback;
				return this;
			}

			// Token: 0x0600025C RID: 604 RVA: 0x00008DCC File Offset: 0x00006FCC
			public InputActionRebindingExtensions.RebindingOperation OnGeneratePath(Func<InputControl, string> callback)
			{
				this.m_OnGeneratePath = callback;
				return this;
			}

			// Token: 0x0600025D RID: 605 RVA: 0x00008DD6 File Offset: 0x00006FD6
			public InputActionRebindingExtensions.RebindingOperation OnComputeScore(Func<InputControl, InputEventPtr, float> callback)
			{
				this.m_OnComputeScore = callback;
				return this;
			}

			// Token: 0x0600025E RID: 606 RVA: 0x00008DE0 File Offset: 0x00006FE0
			public InputActionRebindingExtensions.RebindingOperation OnApplyBinding(Action<InputActionRebindingExtensions.RebindingOperation, string> callback)
			{
				this.m_OnApplyBinding = callback;
				return this;
			}

			// Token: 0x0600025F RID: 607 RVA: 0x00008DEA File Offset: 0x00006FEA
			public InputActionRebindingExtensions.RebindingOperation OnMatchWaitForAnother(float seconds)
			{
				this.m_WaitSecondsAfterMatch = seconds;
				return this;
			}

			// Token: 0x06000260 RID: 608 RVA: 0x00008DF4 File Offset: 0x00006FF4
			public InputActionRebindingExtensions.RebindingOperation Start()
			{
				if (this.started)
				{
					return this;
				}
				if (this.m_ActionToRebind != null && this.m_ActionToRebind.bindings.Count == 0 && (this.m_Flags & InputActionRebindingExtensions.RebindingOperation.Flags.AddNewBinding) == (InputActionRebindingExtensions.RebindingOperation.Flags)0)
				{
					throw new InvalidOperationException(string.Format("Action '{0}' must have at least one existing binding or must be used with WithRebindingAddNewBinding()", this.action));
				}
				if (this.m_ActionToRebind == null && this.m_OnApplyBinding == null)
				{
					throw new InvalidOperationException("Must either have an action (call WithAction()) to apply binding to or have a custom callback to apply the binding (call OnApplyBinding())");
				}
				this.m_StartTime = InputState.currentTime;
				if (this.m_WaitSecondsAfterMatch > 0f || this.m_Timeout > 0f)
				{
					this.HookOnAfterUpdate();
					this.m_LastMatchTime = -1.0;
				}
				this.HookOnEvent();
				this.m_Flags |= InputActionRebindingExtensions.RebindingOperation.Flags.Started;
				this.m_Flags &= ~InputActionRebindingExtensions.RebindingOperation.Flags.Canceled;
				this.m_Flags &= ~InputActionRebindingExtensions.RebindingOperation.Flags.Completed;
				return this;
			}

			// Token: 0x06000261 RID: 609 RVA: 0x00008ED4 File Offset: 0x000070D4
			public void Cancel()
			{
				if (!this.started)
				{
					return;
				}
				this.OnCancel();
			}

			// Token: 0x06000262 RID: 610 RVA: 0x00008EE5 File Offset: 0x000070E5
			public void Complete()
			{
				if (!this.started)
				{
					return;
				}
				this.OnComplete();
			}

			// Token: 0x06000263 RID: 611 RVA: 0x00008EF8 File Offset: 0x000070F8
			public void AddCandidate(InputControl control, float score, float magnitude = -1f)
			{
				if (control == null)
				{
					throw new ArgumentNullException("control");
				}
				int index = this.m_Candidates.IndexOf(control);
				if (index != -1)
				{
					this.m_Scores[index] = score;
				}
				else
				{
					int scoreCount = this.m_Candidates.Count;
					int magnitudeCount = this.m_Candidates.Count;
					this.m_Candidates.Add(control);
					ArrayHelpers.AppendWithCapacity<float>(ref this.m_Scores, ref scoreCount, score, 10);
					ArrayHelpers.AppendWithCapacity<float>(ref this.m_Magnitudes, ref magnitudeCount, magnitude, 10);
				}
				this.SortCandidatesByScore();
			}

			// Token: 0x06000264 RID: 612 RVA: 0x00008F7C File Offset: 0x0000717C
			public void RemoveCandidate(InputControl control)
			{
				if (control == null)
				{
					throw new ArgumentNullException("control");
				}
				int index = this.m_Candidates.IndexOf(control);
				if (index == -1)
				{
					return;
				}
				int candidateCount = this.m_Candidates.Count;
				this.m_Candidates.RemoveAt(index);
				this.m_Scores.EraseAtWithCapacity(ref candidateCount, index);
			}

			// Token: 0x06000265 RID: 613 RVA: 0x00008FCF File Offset: 0x000071CF
			public void Dispose()
			{
				this.UnhookOnEvent();
				this.UnhookOnAfterUpdate();
				this.m_Candidates.Dispose();
				this.m_LayoutCache.Clear();
			}

			// Token: 0x06000266 RID: 614 RVA: 0x00008FF4 File Offset: 0x000071F4
			~RebindingOperation()
			{
				this.Dispose();
			}

			// Token: 0x06000267 RID: 615 RVA: 0x00009020 File Offset: 0x00007220
			public InputActionRebindingExtensions.RebindingOperation Reset()
			{
				this.Cancel();
				this.m_ActionToRebind = null;
				this.m_BindingMask = null;
				this.m_ControlType = null;
				this.m_ExpectedLayout = default(InternedString);
				this.m_IncludePathCount = 0;
				this.m_ExcludePathCount = 0;
				this.m_TargetBindingIndex = -1;
				this.m_BindingGroupForNewBinding = null;
				this.m_CancelBinding = null;
				this.m_MagnitudeThreshold = 0.2f;
				this.m_Timeout = 0f;
				this.m_WaitSecondsAfterMatch = 0f;
				this.m_Flags = (InputActionRebindingExtensions.RebindingOperation.Flags)0;
				Dictionary<InputControl, float> startingActuations = this.m_StartingActuations;
				if (startingActuations != null)
				{
					startingActuations.Clear();
				}
				return this;
			}

			// Token: 0x06000268 RID: 616 RVA: 0x000090B8 File Offset: 0x000072B8
			private void HookOnEvent()
			{
				if ((this.m_Flags & InputActionRebindingExtensions.RebindingOperation.Flags.OnEventHooked) != (InputActionRebindingExtensions.RebindingOperation.Flags)0)
				{
					return;
				}
				if (this.m_OnEventDelegate == null)
				{
					this.m_OnEventDelegate = new Action<InputEventPtr, InputDevice>(this.OnEvent);
				}
				InputSystem.onEvent += this.m_OnEventDelegate;
				this.m_Flags |= InputActionRebindingExtensions.RebindingOperation.Flags.OnEventHooked;
			}

			// Token: 0x06000269 RID: 617 RVA: 0x0000910D File Offset: 0x0000730D
			private void UnhookOnEvent()
			{
				if ((this.m_Flags & InputActionRebindingExtensions.RebindingOperation.Flags.OnEventHooked) == (InputActionRebindingExtensions.RebindingOperation.Flags)0)
				{
					return;
				}
				InputSystem.onEvent -= this.m_OnEventDelegate;
				this.m_Flags &= ~InputActionRebindingExtensions.RebindingOperation.Flags.OnEventHooked;
			}

			// Token: 0x0600026A RID: 618 RVA: 0x00009140 File Offset: 0x00007340
			private unsafe void OnEvent(InputEventPtr eventPtr, InputDevice device)
			{
				FourCC eventType = eventPtr.type;
				if (eventType != 1398030676 && eventType != 1145852993)
				{
					return;
				}
				bool haveChangedCandidates = false;
				bool suppressEvent = false;
				InputControlExtensions.Enumerate controlEnumerationFlags = InputControlExtensions.Enumerate.IncludeSyntheticControls | InputControlExtensions.Enumerate.IncludeNonLeafControls;
				if ((this.m_Flags & InputActionRebindingExtensions.RebindingOperation.Flags.DontIgnoreNoisyControls) != (InputActionRebindingExtensions.RebindingOperation.Flags)0)
				{
					controlEnumerationFlags |= InputControlExtensions.Enumerate.IncludeNoisyControls;
				}
				foreach (InputControl control in eventPtr.EnumerateControls(controlEnumerationFlags, device, 0f))
				{
					void* statePtr = control.GetStatePtrFromStateEventUnchecked(eventPtr, eventType);
					if (!string.IsNullOrEmpty(this.m_CancelBinding) && InputControlPath.Matches(this.m_CancelBinding, control) && control.HasValueChangeInState(statePtr))
					{
						this.OnCancel();
						break;
					}
					if ((this.m_ExcludePathCount <= 0 || !InputActionRebindingExtensions.RebindingOperation.HavePathMatch(control, this.m_ExcludePaths, this.m_ExcludePathCount)) && (this.m_IncludePathCount <= 0 || InputActionRebindingExtensions.RebindingOperation.HavePathMatch(control, this.m_IncludePaths, this.m_IncludePathCount)) && (!(this.m_ControlType != null) || this.m_ControlType.IsInstanceOfType(control)) && (this.m_ExpectedLayout.IsEmpty() || !(this.m_ExpectedLayout != control.m_Layout) || InputControlLayout.s_Layouts.IsBasedOn(this.m_ExpectedLayout, control.m_Layout)))
					{
						if (control.CheckStateIsAtDefault(statePtr, null))
						{
							if (!this.m_StartingActuations.ContainsKey(control))
							{
								this.m_StartingActuations.Add(control, 0f);
							}
							this.m_StartingActuations[control] = 0f;
						}
						else
						{
							suppressEvent = true;
							float magnitude = control.EvaluateMagnitude(statePtr);
							if (magnitude >= 0f)
							{
								float startingMagnitude;
								if (!this.m_StartingActuations.TryGetValue(control, out startingMagnitude))
								{
									startingMagnitude = control.magnitude;
									this.m_StartingActuations.Add(control, startingMagnitude);
								}
								if (Mathf.Abs(startingMagnitude - magnitude) < this.m_MagnitudeThreshold)
								{
									continue;
								}
							}
							float score;
							if (this.m_OnComputeScore != null)
							{
								score = this.m_OnComputeScore(control, eventPtr);
							}
							else
							{
								score = magnitude;
								if (!control.synthetic)
								{
									score += 1f;
								}
							}
							int candidateIndex = this.m_Candidates.IndexOf(control);
							if (candidateIndex != -1)
							{
								if (this.m_Scores[candidateIndex] < score)
								{
									haveChangedCandidates = true;
									this.m_Scores[candidateIndex] = score;
									if (this.m_WaitSecondsAfterMatch > 0f)
									{
										this.m_LastMatchTime = InputState.currentTime;
									}
								}
							}
							else
							{
								int scoreCount = this.m_Candidates.Count;
								int magnitudeCount = this.m_Candidates.Count;
								this.m_Candidates.Add(control);
								ArrayHelpers.AppendWithCapacity<float>(ref this.m_Scores, ref scoreCount, score, 10);
								ArrayHelpers.AppendWithCapacity<float>(ref this.m_Magnitudes, ref magnitudeCount, magnitude, 10);
								haveChangedCandidates = true;
								if (this.m_WaitSecondsAfterMatch > 0f)
								{
									this.m_LastMatchTime = InputState.currentTime;
								}
							}
						}
					}
				}
				if (suppressEvent && (this.m_Flags & InputActionRebindingExtensions.RebindingOperation.Flags.SuppressMatchingEvents) != (InputActionRebindingExtensions.RebindingOperation.Flags)0)
				{
					eventPtr.handled = true;
				}
				if (haveChangedCandidates && !this.canceled)
				{
					if (this.m_OnPotentialMatch != null)
					{
						this.SortCandidatesByScore();
						this.m_OnPotentialMatch(this);
						return;
					}
					if (this.m_WaitSecondsAfterMatch <= 0f)
					{
						this.OnComplete();
						return;
					}
					this.SortCandidatesByScore();
				}
			}

			// Token: 0x0600026B RID: 619 RVA: 0x000094A0 File Offset: 0x000076A0
			private void SortCandidatesByScore()
			{
				int candidateCount = this.m_Candidates.Count;
				if (candidateCount <= 1)
				{
					return;
				}
				for (int i = 1; i < candidateCount; i++)
				{
					int j = i;
					while (j > 0 && this.m_Scores[j - 1] < this.m_Scores[j])
					{
						int k = j - 1;
						this.m_Scores.SwapElements(j, k);
						this.m_Candidates.SwapElements(j, k);
						this.m_Magnitudes.SwapElements(j, k);
						j--;
					}
				}
			}

			// Token: 0x0600026C RID: 620 RVA: 0x00009518 File Offset: 0x00007718
			private static bool HavePathMatch(InputControl control, string[] paths, int pathCount)
			{
				for (int i = 0; i < pathCount; i++)
				{
					if (InputControlPath.MatchesPrefix(paths[i], control))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x0600026D RID: 621 RVA: 0x00009540 File Offset: 0x00007740
			private void HookOnAfterUpdate()
			{
				if ((this.m_Flags & InputActionRebindingExtensions.RebindingOperation.Flags.OnAfterUpdateHooked) != (InputActionRebindingExtensions.RebindingOperation.Flags)0)
				{
					return;
				}
				if (this.m_OnAfterUpdateDelegate == null)
				{
					this.m_OnAfterUpdateDelegate = new Action(this.OnAfterUpdate);
				}
				InputSystem.onAfterUpdate += this.m_OnAfterUpdateDelegate;
				this.m_Flags |= InputActionRebindingExtensions.RebindingOperation.Flags.OnAfterUpdateHooked;
			}

			// Token: 0x0600026E RID: 622 RVA: 0x0000958D File Offset: 0x0000778D
			private void UnhookOnAfterUpdate()
			{
				if ((this.m_Flags & InputActionRebindingExtensions.RebindingOperation.Flags.OnAfterUpdateHooked) == (InputActionRebindingExtensions.RebindingOperation.Flags)0)
				{
					return;
				}
				InputSystem.onAfterUpdate -= this.m_OnAfterUpdateDelegate;
				this.m_Flags &= ~InputActionRebindingExtensions.RebindingOperation.Flags.OnAfterUpdateHooked;
			}

			// Token: 0x0600026F RID: 623 RVA: 0x000095B8 File Offset: 0x000077B8
			private void OnAfterUpdate()
			{
				if (this.m_LastMatchTime < 0.0 && this.m_Timeout > 0f && InputState.currentTime - this.m_StartTime > (double)this.m_Timeout)
				{
					this.Cancel();
					return;
				}
				if (this.m_WaitSecondsAfterMatch <= 0f)
				{
					return;
				}
				if (this.m_LastMatchTime < 0.0)
				{
					return;
				}
				if (InputState.currentTime >= this.m_LastMatchTime + (double)this.m_WaitSecondsAfterMatch)
				{
					this.Complete();
				}
			}

			// Token: 0x06000270 RID: 624 RVA: 0x0000963C File Offset: 0x0000783C
			private void OnComplete()
			{
				this.SortCandidatesByScore();
				if (this.m_Candidates.Count > 0)
				{
					InputControl selectedControl = this.m_Candidates[0];
					string path = selectedControl.path;
					if (this.m_OnGeneratePath != null)
					{
						string newPath = this.m_OnGeneratePath(selectedControl);
						if (!string.IsNullOrEmpty(newPath))
						{
							path = newPath;
						}
						else if ((this.m_Flags & InputActionRebindingExtensions.RebindingOperation.Flags.DontGeneralizePathOfSelectedControl) == (InputActionRebindingExtensions.RebindingOperation.Flags)0)
						{
							path = this.GeneratePathForControl(selectedControl);
						}
					}
					else if ((this.m_Flags & InputActionRebindingExtensions.RebindingOperation.Flags.DontGeneralizePathOfSelectedControl) == (InputActionRebindingExtensions.RebindingOperation.Flags)0)
					{
						path = this.GeneratePathForControl(selectedControl);
					}
					if (this.m_OnApplyBinding != null)
					{
						this.m_OnApplyBinding(this, path);
					}
					else if ((this.m_Flags & InputActionRebindingExtensions.RebindingOperation.Flags.AddNewBinding) != (InputActionRebindingExtensions.RebindingOperation.Flags)0)
					{
						this.m_ActionToRebind.AddBinding(path, null, null, this.m_BindingGroupForNewBinding);
					}
					else if (this.m_TargetBindingIndex >= 0)
					{
						if (this.m_TargetBindingIndex >= this.m_ActionToRebind.bindings.Count)
						{
							throw new InvalidOperationException(string.Format("Target binding index {0} out of range for action '{1}' with {2} bindings", this.m_TargetBindingIndex, this.m_ActionToRebind, this.m_ActionToRebind.bindings.Count));
						}
						this.m_ActionToRebind.ApplyBindingOverride(this.m_TargetBindingIndex, path);
					}
					else if (this.m_BindingMask != null)
					{
						InputBinding bindingOverride = this.m_BindingMask.Value;
						bindingOverride.overridePath = path;
						this.m_ActionToRebind.ApplyBindingOverride(bindingOverride);
					}
					else
					{
						this.m_ActionToRebind.ApplyBindingOverride(path, null, null);
					}
				}
				this.m_Flags |= InputActionRebindingExtensions.RebindingOperation.Flags.Completed;
				Action<InputActionRebindingExtensions.RebindingOperation> onComplete = this.m_OnComplete;
				if (onComplete != null)
				{
					onComplete(this);
				}
				this.ResetAfterMatchCompleted();
			}

			// Token: 0x06000271 RID: 625 RVA: 0x000097DB File Offset: 0x000079DB
			private void OnCancel()
			{
				this.m_Flags |= InputActionRebindingExtensions.RebindingOperation.Flags.Canceled;
				Action<InputActionRebindingExtensions.RebindingOperation> onCancel = this.m_OnCancel;
				if (onCancel != null)
				{
					onCancel(this);
				}
				this.ResetAfterMatchCompleted();
			}

			// Token: 0x06000272 RID: 626 RVA: 0x00009804 File Offset: 0x00007A04
			private void ResetAfterMatchCompleted()
			{
				this.m_Flags &= ~InputActionRebindingExtensions.RebindingOperation.Flags.Started;
				this.m_Candidates.Clear();
				this.m_Candidates.Capacity = 0;
				this.m_StartTime = -1.0;
				this.m_StartingActuations.Clear();
				this.UnhookOnEvent();
				this.UnhookOnAfterUpdate();
			}

			// Token: 0x06000273 RID: 627 RVA: 0x0000985D File Offset: 0x00007A5D
			private void ThrowIfRebindInProgress()
			{
				if (this.started)
				{
					throw new InvalidOperationException("Cannot reconfigure rebinding while operation is in progress");
				}
			}

			// Token: 0x06000274 RID: 628 RVA: 0x00009874 File Offset: 0x00007A74
			private string GeneratePathForControl(InputControl control)
			{
				InputDevice device = control.device;
				InternedString deviceLayoutName = InputControlLayout.s_Layouts.FindLayoutThatIntroducesControl(control, this.m_LayoutCache);
				if (this.m_PathBuilder == null)
				{
					this.m_PathBuilder = new StringBuilder();
				}
				else
				{
					this.m_PathBuilder.Length = 0;
				}
				control.BuildPath(deviceLayoutName, this.m_PathBuilder);
				return this.m_PathBuilder.ToString();
			}

			// Token: 0x0400012A RID: 298
			public const float kDefaultMagnitudeThreshold = 0.2f;

			// Token: 0x0400012B RID: 299
			private InputAction m_ActionToRebind;

			// Token: 0x0400012C RID: 300
			private InputBinding? m_BindingMask;

			// Token: 0x0400012D RID: 301
			private Type m_ControlType;

			// Token: 0x0400012E RID: 302
			private InternedString m_ExpectedLayout;

			// Token: 0x0400012F RID: 303
			private int m_IncludePathCount;

			// Token: 0x04000130 RID: 304
			private string[] m_IncludePaths;

			// Token: 0x04000131 RID: 305
			private int m_ExcludePathCount;

			// Token: 0x04000132 RID: 306
			private string[] m_ExcludePaths;

			// Token: 0x04000133 RID: 307
			private int m_TargetBindingIndex = -1;

			// Token: 0x04000134 RID: 308
			private string m_BindingGroupForNewBinding;

			// Token: 0x04000135 RID: 309
			private string m_CancelBinding;

			// Token: 0x04000136 RID: 310
			private float m_MagnitudeThreshold = 0.2f;

			// Token: 0x04000137 RID: 311
			private float[] m_Scores;

			// Token: 0x04000138 RID: 312
			private float[] m_Magnitudes;

			// Token: 0x04000139 RID: 313
			private double m_LastMatchTime;

			// Token: 0x0400013A RID: 314
			private double m_StartTime;

			// Token: 0x0400013B RID: 315
			private float m_Timeout;

			// Token: 0x0400013C RID: 316
			private float m_WaitSecondsAfterMatch;

			// Token: 0x0400013D RID: 317
			private InputControlList<InputControl> m_Candidates;

			// Token: 0x0400013E RID: 318
			private Action<InputActionRebindingExtensions.RebindingOperation> m_OnComplete;

			// Token: 0x0400013F RID: 319
			private Action<InputActionRebindingExtensions.RebindingOperation> m_OnCancel;

			// Token: 0x04000140 RID: 320
			private Action<InputActionRebindingExtensions.RebindingOperation> m_OnPotentialMatch;

			// Token: 0x04000141 RID: 321
			private Func<InputControl, string> m_OnGeneratePath;

			// Token: 0x04000142 RID: 322
			private Func<InputControl, InputEventPtr, float> m_OnComputeScore;

			// Token: 0x04000143 RID: 323
			private Action<InputActionRebindingExtensions.RebindingOperation, string> m_OnApplyBinding;

			// Token: 0x04000144 RID: 324
			private Action<InputEventPtr, InputDevice> m_OnEventDelegate;

			// Token: 0x04000145 RID: 325
			private Action m_OnAfterUpdateDelegate;

			// Token: 0x04000146 RID: 326
			private InputControlLayout.Cache m_LayoutCache;

			// Token: 0x04000147 RID: 327
			private StringBuilder m_PathBuilder;

			// Token: 0x04000148 RID: 328
			private InputActionRebindingExtensions.RebindingOperation.Flags m_Flags;

			// Token: 0x04000149 RID: 329
			private Dictionary<InputControl, float> m_StartingActuations = new Dictionary<InputControl, float>();

			// Token: 0x02000038 RID: 56
			[Flags]
			private enum Flags
			{
				// Token: 0x0400014B RID: 331
				Started = 1,
				// Token: 0x0400014C RID: 332
				Completed = 2,
				// Token: 0x0400014D RID: 333
				Canceled = 4,
				// Token: 0x0400014E RID: 334
				OnEventHooked = 8,
				// Token: 0x0400014F RID: 335
				OnAfterUpdateHooked = 16,
				// Token: 0x04000150 RID: 336
				DontIgnoreNoisyControls = 64,
				// Token: 0x04000151 RID: 337
				DontGeneralizePathOfSelectedControl = 128,
				// Token: 0x04000152 RID: 338
				AddNewBinding = 256,
				// Token: 0x04000153 RID: 339
				SuppressMatchingEvents = 512
			}
		}

		// Token: 0x0200003A RID: 58
		internal class DeferBindingResolutionWrapper : IDisposable
		{
			// Token: 0x06000278 RID: 632 RVA: 0x00009913 File Offset: 0x00007B13
			public void Acquire()
			{
				InputActionMap.s_DeferBindingResolution++;
			}

			// Token: 0x06000279 RID: 633 RVA: 0x00009921 File Offset: 0x00007B21
			public void Dispose()
			{
				if (InputActionMap.s_DeferBindingResolution > 0)
				{
					InputActionMap.s_DeferBindingResolution--;
				}
				if (InputActionMap.s_DeferBindingResolution == 0)
				{
					InputActionState.DeferredResolutionOfBindings();
				}
			}
		}
	}
}
