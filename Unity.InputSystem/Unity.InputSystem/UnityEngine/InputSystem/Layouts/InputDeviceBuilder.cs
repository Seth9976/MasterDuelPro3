using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Layouts
{
	// Token: 0x0200020A RID: 522
	internal struct InputDeviceBuilder : IDisposable
	{
		// Token: 0x06001351 RID: 4945 RVA: 0x00058470 File Offset: 0x00056670
		public void Setup(InternedString layout, InternedString variants, InputDeviceDescription deviceDescription = default(InputDeviceDescription))
		{
			this.m_LayoutCacheRef = InputControlLayout.CacheRef();
			this.InstantiateLayout(layout, variants, default(InternedString), null);
			this.FinalizeControlHierarchy();
			this.m_StateOffsetToControlMap.Sort();
			this.m_Device.m_Description = deviceDescription;
			this.m_Device.m_StateOffsetToControlMap = this.m_StateOffsetToControlMap.ToArray();
			this.m_Device.CallFinishSetupRecursive();
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x000584DC File Offset: 0x000566DC
		public InputDevice Finish()
		{
			InputDevice device = this.m_Device;
			int i = 0;
			using (ReadOnlyArray<InputControl>.Enumerator enumerator = device.allControls.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.isButton)
					{
						i++;
					}
				}
			}
			device.m_ButtonControlsCheckingPressState = new List<ButtonControl>(i);
			device.m_UpdatedButtons = new HashSet<int>(i);
			this.Reset();
			return device;
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x00058560 File Offset: 0x00056760
		public void Dispose()
		{
			this.m_LayoutCacheRef.Dispose();
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x0005856D File Offset: 0x0005676D
		private void Reset()
		{
			this.m_Device = null;
			Dictionary<string, InputControlLayout.ControlItem> childControlOverrides = this.m_ChildControlOverrides;
			if (childControlOverrides != null)
			{
				childControlOverrides.Clear();
			}
			List<uint> stateOffsetToControlMap = this.m_StateOffsetToControlMap;
			if (stateOffsetToControlMap == null)
			{
				return;
			}
			stateOffsetToControlMap.Clear();
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x00058598 File Offset: 0x00056798
		private InputControl InstantiateLayout(InternedString layout, InternedString variants, InternedString name, InputControl parent)
		{
			InputControlLayout layoutInstance = InputDeviceBuilder.FindOrLoadLayout(layout);
			return this.InstantiateLayout(layoutInstance, variants, name, parent);
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x000585BC File Offset: 0x000567BC
		private InputControl InstantiateLayout(InputControlLayout layout, InternedString variants, InternedString name, InputControl parent)
		{
			InputControl control = Activator.CreateInstance(layout.type) as InputControl;
			if (control == null)
			{
				throw new InvalidOperationException(string.Format("Type '{0}' referenced by layout '{1}' is not an InputControl", layout.type.Name, layout.name));
			}
			InputDevice controlAsDevice = control as InputDevice;
			if (controlAsDevice != null)
			{
				if (parent != null)
				{
					throw new InvalidOperationException(string.Format("Cannot instantiate device layout '{0}' as child of '{1}'; devices must be added at root", layout.name, parent.path));
				}
				this.m_Device = controlAsDevice;
				this.m_Device.m_StateBlock.byteOffset = 0U;
				this.m_Device.m_StateBlock.bitOffset = 0U;
				this.m_Device.m_StateBlock.format = layout.stateFormat;
				this.m_Device.m_AliasesForEachControl = null;
				this.m_Device.m_ChildrenForEachControl = null;
				this.m_Device.m_UpdatedButtons = null;
				this.m_Device.m_UsagesForEachControl = null;
				this.m_Device.m_UsageToControl = null;
				bool? flag = layout.m_UpdateBeforeRender;
				bool flag2 = true;
				if ((flag.GetValueOrDefault() == flag2) & (flag != null))
				{
					this.m_Device.m_DeviceFlags |= InputDevice.DeviceFlags.UpdateBeforeRender;
				}
				if (layout.canRunInBackground != null)
				{
					this.m_Device.m_DeviceFlags |= InputDevice.DeviceFlags.CanRunInBackgroundHasBeenQueried;
					flag = layout.canRunInBackground;
					flag2 = true;
					if ((flag.GetValueOrDefault() == flag2) & (flag != null))
					{
						this.m_Device.m_DeviceFlags |= InputDevice.DeviceFlags.CanRunInBackground;
					}
				}
			}
			else if (parent == null)
			{
				throw new InvalidOperationException(string.Format("Toplevel layout used with InputDeviceBuilder must be a device layout; '{0}' is a control layout", layout.name));
			}
			if (name.IsEmpty())
			{
				name = layout.name;
				int indexOfLastColon = name.ToString().LastIndexOf(':');
				if (indexOfLastColon != -1)
				{
					name = new InternedString(name.ToString().Substring(indexOfLastColon + 1));
				}
			}
			if (name.ToString().IndexOf('/') != -1)
			{
				name = new InternedString(name.ToString().CleanSlashes());
			}
			if (variants.IsEmpty())
			{
				variants = layout.variants;
				if (variants.IsEmpty())
				{
					variants = InputControlLayout.DefaultVariant;
				}
			}
			control.m_Name = name;
			control.m_DisplayNameFromLayout = layout.m_DisplayName;
			control.m_Layout = layout.name;
			control.m_Variants = variants;
			control.m_Parent = parent;
			control.m_Device = this.m_Device;
			if (control is InputDevice)
			{
				control.noisy = layout.isNoisy;
			}
			bool haveChildrenUsingStateFromOtherControl = false;
			try
			{
				this.AddChildControls(layout, variants, control, ref haveChildrenUsingStateFromOtherControl);
			}
			catch
			{
				throw;
			}
			InputDeviceBuilder.ComputeStateLayout(control);
			if (haveChildrenUsingStateFromOtherControl)
			{
				InputControlLayout.ControlItem[] controls = layout.m_Controls;
				for (int i = 0; i < controls.Length; i++)
				{
					ref InputControlLayout.ControlItem item = ref controls[i];
					if (!string.IsNullOrEmpty(item.useStateFrom))
					{
						InputDeviceBuilder.ApplyUseStateFrom(control, ref item, layout);
					}
				}
			}
			return control;
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x000588B4 File Offset: 0x00056AB4
		private void AddChildControls(InputControlLayout layout, InternedString variants, InputControl parent, ref bool haveChildrenUsingStateFromOtherControls)
		{
			InputControlLayout.ControlItem[] controlLayouts = layout.m_Controls;
			if (controlLayouts == null)
			{
				return;
			}
			int childCount = 0;
			bool haveControlLayoutWithPath = false;
			for (int i = 0; i < controlLayouts.Length; i++)
			{
				if (controlLayouts[i].variants.IsEmpty() || StringHelpers.CharacterSeparatedListsHaveAtLeastOneCommonElement(controlLayouts[i].variants, variants, ";"[0]))
				{
					if (controlLayouts[i].isModifyingExistingControl)
					{
						if (controlLayouts[i].isArray)
						{
							throw new NotSupportedException(string.Format("Control '{0}' in layout '{1}' is modifying the child of another control but is marked as an array", controlLayouts[i].name, layout.name));
						}
						haveControlLayoutWithPath = true;
						this.InsertChildControlOverride(parent, ref controlLayouts[i]);
					}
					else if (controlLayouts[i].isArray)
					{
						childCount += controlLayouts[i].arraySize;
					}
					else
					{
						childCount++;
					}
				}
			}
			if (childCount == 0)
			{
				parent.m_ChildCount = 0;
				parent.m_ChildStartIndex = 0;
				haveChildrenUsingStateFromOtherControls = false;
				return;
			}
			int firstChildIndex = ArrayHelpers.GrowBy<InputControl>(ref this.m_Device.m_ChildrenForEachControl, childCount);
			int childIndex = firstChildIndex;
			foreach (InputControlLayout.ControlItem controlLayout in controlLayouts)
			{
				if (!controlLayout.isModifyingExistingControl && (controlLayout.variants.IsEmpty() || StringHelpers.CharacterSeparatedListsHaveAtLeastOneCommonElement(controlLayout.variants, variants, ";"[0])))
				{
					if (controlLayout.isArray)
					{
						for (int k = 0; k < controlLayout.arraySize; k++)
						{
							string name = controlLayout.name + k.ToString();
							InputControl control = this.AddChildControl(layout, variants, parent, ref haveChildrenUsingStateFromOtherControls, controlLayout, childIndex, name);
							childIndex++;
							if (control.m_StateBlock.byteOffset != 4294967295U)
							{
								InputControl inputControl = control;
								inputControl.m_StateBlock.byteOffset = inputControl.m_StateBlock.byteOffset + (uint)(k * (int)control.m_StateBlock.alignedSizeInBytes);
							}
						}
					}
					else
					{
						this.AddChildControl(layout, variants, parent, ref haveChildrenUsingStateFromOtherControls, controlLayout, childIndex, null);
						childIndex++;
					}
				}
			}
			parent.m_ChildCount = childCount;
			parent.m_ChildStartIndex = firstChildIndex;
			if (haveControlLayoutWithPath)
			{
				for (int l = 0; l < controlLayouts.Length; l++)
				{
					InputControlLayout.ControlItem controlLayout2 = controlLayouts[l];
					if (controlLayout2.isModifyingExistingControl && (controlLayout2.variants.IsEmpty() || StringHelpers.CharacterSeparatedListsHaveAtLeastOneCommonElement(controlLayouts[l].variants, variants, ";"[0])))
					{
						this.AddChildControlIfMissing(layout, variants, parent, ref haveChildrenUsingStateFromOtherControls, ref controlLayout2);
					}
				}
			}
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x00058B60 File Offset: 0x00056D60
		private InputControl AddChildControl(InputControlLayout layout, InternedString variants, InputControl parent, ref bool haveChildrenUsingStateFromOtherControls, InputControlLayout.ControlItem controlItem, int childIndex, string nameOverride = null)
		{
			InternedString name = ((nameOverride != null) ? new InternedString(nameOverride) : controlItem.name);
			if (string.IsNullOrEmpty(controlItem.layout))
			{
				throw new InvalidOperationException(string.Format("Layout has not been set on control '{0}' in '{1}'", controlItem.name, layout.name));
			}
			if (this.m_ChildControlOverrides != null)
			{
				string pathLowerCase = this.ChildControlOverridePath(parent, name);
				InputControlLayout.ControlItem controlOverride;
				if (this.m_ChildControlOverrides.TryGetValue(pathLowerCase, out controlOverride))
				{
					controlItem = controlOverride.Merge(controlItem);
				}
			}
			InternedString layoutName = controlItem.layout;
			InputControl control;
			try
			{
				control = this.InstantiateLayout(layoutName, variants, name, parent);
			}
			catch (InputControlLayout.LayoutNotFoundException exception)
			{
				throw new InputControlLayout.LayoutNotFoundException(string.Format("Cannot find layout '{0}' used in control '{1}' of layout '{2}'", exception.layout, name, layout.name), exception);
			}
			this.m_Device.m_ChildrenForEachControl[childIndex] = control;
			control.noisy = controlItem.isNoisy;
			control.synthetic = controlItem.isSynthetic;
			control.usesStateFromOtherControl = !string.IsNullOrEmpty(controlItem.useStateFrom);
			control.dontReset = (control.noisy || controlItem.dontReset) && !control.usesStateFromOtherControl;
			if (control.noisy)
			{
				this.m_Device.noisy = true;
			}
			control.isButton = control is ButtonControl;
			if (control.dontReset)
			{
				this.m_Device.hasDontResetControls = true;
			}
			control.m_DisplayNameFromLayout = controlItem.displayName;
			control.m_ShortDisplayNameFromLayout = controlItem.shortDisplayName;
			control.m_DefaultState = controlItem.defaultState;
			if (!control.m_DefaultState.isEmpty)
			{
				this.m_Device.hasControlsWithDefaultState = true;
			}
			if (!controlItem.minValue.isEmpty)
			{
				control.m_MinValue = controlItem.minValue;
			}
			if (!controlItem.maxValue.isEmpty)
			{
				control.m_MaxValue = controlItem.maxValue;
			}
			if (!control.usesStateFromOtherControl)
			{
				control.m_StateBlock.byteOffset = controlItem.offset;
				control.m_StateBlock.bitOffset = controlItem.bit;
				if (controlItem.sizeInBits != 0U)
				{
					control.m_StateBlock.sizeInBits = controlItem.sizeInBits;
				}
				if (controlItem.format != 0)
				{
					InputDeviceBuilder.SetFormat(control, controlItem);
				}
			}
			else
			{
				control.m_StateBlock.sizeInBits = uint.MaxValue;
				haveChildrenUsingStateFromOtherControls = true;
			}
			ReadOnlyArray<InternedString> usages = controlItem.usages;
			if (usages.Count > 0)
			{
				int usageCount = usages.Count;
				int usageIndex = ArrayHelpers.AppendToImmutable<InternedString>(ref this.m_Device.m_UsagesForEachControl, usages.m_Array);
				control.m_UsageStartIndex = usageIndex;
				control.m_UsageCount = usageCount;
				ArrayHelpers.GrowBy<InputControl>(ref this.m_Device.m_UsageToControl, usageCount);
				for (int i = 0; i < usageCount; i++)
				{
					this.m_Device.m_UsageToControl[usageIndex + i] = control;
				}
			}
			if (controlItem.aliases.Count > 0)
			{
				int aliasCount = controlItem.aliases.Count;
				int aliasIndex = ArrayHelpers.AppendToImmutable<InternedString>(ref this.m_Device.m_AliasesForEachControl, controlItem.aliases.m_Array);
				control.m_AliasStartIndex = aliasIndex;
				control.m_AliasCount = aliasCount;
			}
			if (controlItem.parameters.Count > 0)
			{
				NamedValue.ApplyAllToObject<ReadOnlyArray<NamedValue>>(control, controlItem.parameters);
			}
			if (controlItem.processors.Count > 0)
			{
				InputDeviceBuilder.AddProcessors(control, ref controlItem, layout.name);
			}
			return control;
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x00058EDC File Offset: 0x000570DC
		private void InsertChildControlOverride(InputControl parent, ref InputControlLayout.ControlItem controlItem)
		{
			if (this.m_ChildControlOverrides == null)
			{
				this.m_ChildControlOverrides = new Dictionary<string, InputControlLayout.ControlItem>();
			}
			string pathLowerCase = this.ChildControlOverridePath(parent, controlItem.name);
			InputControlLayout.ControlItem existingOverrides;
			if (!this.m_ChildControlOverrides.TryGetValue(pathLowerCase, out existingOverrides))
			{
				this.m_ChildControlOverrides[pathLowerCase] = controlItem;
				return;
			}
			existingOverrides = existingOverrides.Merge(controlItem);
			this.m_ChildControlOverrides[pathLowerCase] = existingOverrides;
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x00058F48 File Offset: 0x00057148
		private string ChildControlOverridePath(InputControl parent, InternedString controlName)
		{
			string pathLowerCase = controlName.ToLower();
			for (InputControl current = parent; current != this.m_Device; current = current.m_Parent)
			{
				pathLowerCase = current.m_Name.ToLower() + "/" + pathLowerCase;
			}
			return pathLowerCase;
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x00058F8C File Offset: 0x0005718C
		private void AddChildControlIfMissing(InputControlLayout layout, InternedString variants, InputControl parent, ref bool haveChildrenUsingStateFromOtherControls, ref InputControlLayout.ControlItem controlItem)
		{
			InputControl child = InputControlPath.TryFindChild(parent, controlItem.name, 0);
			if (child != null)
			{
				return;
			}
			child = this.InsertChildControl(layout, variants, parent, ref haveChildrenUsingStateFromOtherControls, ref controlItem);
			if (child.parent != parent)
			{
				InputDeviceBuilder.ComputeStateLayout(child.parent);
			}
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x00058FD4 File Offset: 0x000571D4
		private InputControl InsertChildControl(InputControlLayout layout, InternedString variant, InputControl parent, ref bool haveChildrenUsingStateFromOtherControls, ref InputControlLayout.ControlItem controlItem)
		{
			string text = controlItem.name.ToString();
			int indexOfSlash = text.LastIndexOf('/');
			if (indexOfSlash == -1)
			{
				throw new InvalidOperationException("InsertChildControl has to be called with a slash-separated path");
			}
			string immediateParentPath = text.Substring(0, indexOfSlash);
			InputControl immediateParent = InputControlPath.TryFindChild(parent, immediateParentPath, 0);
			if (immediateParent == null)
			{
				throw new InvalidOperationException(string.Format("Cannot find parent '{0}' of control '{1}' in layout '{2}'", immediateParentPath, controlItem.name, layout.name));
			}
			string controlName = text.Substring(indexOfSlash + 1);
			if (controlName.Length == 0)
			{
				throw new InvalidOperationException(string.Format("Path cannot end in '/' (control '{0}' in layout '{1}')", controlItem.name, layout.name));
			}
			int childStartIndex = immediateParent.m_ChildStartIndex;
			if (childStartIndex == 0)
			{
				childStartIndex = this.m_Device.m_ChildrenForEachControl.LengthSafe<InputControl>();
				immediateParent.m_ChildStartIndex = childStartIndex;
			}
			int childIndex = childStartIndex + immediateParent.m_ChildCount;
			InputDeviceBuilder.ShiftChildIndicesInHierarchyOneUp(this.m_Device, childIndex, immediateParent);
			ArrayHelpers.InsertAt<InputControl>(ref this.m_Device.m_ChildrenForEachControl, childIndex, null);
			immediateParent.m_ChildCount++;
			return this.AddChildControl(layout, variant, immediateParent, ref haveChildrenUsingStateFromOtherControls, controlItem, childIndex, controlName);
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x000590FC File Offset: 0x000572FC
		private static void ApplyUseStateFrom(InputControl parent, ref InputControlLayout.ControlItem controlItem, InputControlLayout layout)
		{
			InputControl child = InputControlPath.TryFindChild(parent, controlItem.name, 0);
			InputControl referencedControl = InputControlPath.TryFindChild(parent, controlItem.useStateFrom, 0);
			if (referencedControl == null)
			{
				throw new InvalidOperationException(string.Format("Cannot find control '{0}' referenced in 'useStateFrom' of control '{1}' in layout '{2}'", controlItem.useStateFrom, controlItem.name, layout.name));
			}
			child.m_StateBlock = referencedControl.m_StateBlock;
			child.usesStateFromOtherControl = true;
			child.dontReset = referencedControl.dontReset;
			if (child.parent != referencedControl.parent)
			{
				for (InputControl parentInChain = referencedControl.parent; parentInChain != parent; parentInChain = parentInChain.parent)
				{
					InputControl inputControl = child;
					inputControl.m_StateBlock.byteOffset = inputControl.m_StateBlock.byteOffset + parentInChain.m_StateBlock.byteOffset;
				}
			}
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x000591B8 File Offset: 0x000573B8
		private static void ShiftChildIndicesInHierarchyOneUp(InputDevice device, int startIndex, InputControl exceptControl)
		{
			InputControl[] controls = device.m_ChildrenForEachControl;
			int count = controls.Length;
			for (int i = 0; i < count; i++)
			{
				InputControl control = controls[i];
				if (control != null && control != exceptControl && control.m_ChildStartIndex >= startIndex)
				{
					control.m_ChildStartIndex++;
				}
			}
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x00059200 File Offset: 0x00057400
		private void SetDisplayName(InputControl control, string longDisplayNameFromLayout, string shortDisplayNameFromLayout, bool shortName)
		{
			string displayNameFromLayout = (shortName ? shortDisplayNameFromLayout : longDisplayNameFromLayout);
			if (string.IsNullOrEmpty(displayNameFromLayout))
			{
				if (shortName)
				{
					if (control.parent == null || control.parent == control.device)
					{
						control.m_ShortDisplayNameFromLayout = null;
						return;
					}
					if (this.m_StringBuilder == null)
					{
						this.m_StringBuilder = new StringBuilder();
					}
					this.m_StringBuilder.Length = 0;
					InputDeviceBuilder.AddParentDisplayNameRecursive(control.parent, this.m_StringBuilder, true);
					if (this.m_StringBuilder.Length == 0)
					{
						control.m_ShortDisplayNameFromLayout = null;
						return;
					}
					if (!string.IsNullOrEmpty(longDisplayNameFromLayout))
					{
						this.m_StringBuilder.Append(longDisplayNameFromLayout);
					}
					else
					{
						this.m_StringBuilder.Append(control.name);
					}
					control.m_ShortDisplayNameFromLayout = this.m_StringBuilder.ToString();
					return;
				}
				else
				{
					displayNameFromLayout = control.name;
				}
			}
			if (control.parent != null && control.parent != control.device)
			{
				if (this.m_StringBuilder == null)
				{
					this.m_StringBuilder = new StringBuilder();
				}
				this.m_StringBuilder.Length = 0;
				InputDeviceBuilder.AddParentDisplayNameRecursive(control.parent, this.m_StringBuilder, shortName);
				this.m_StringBuilder.Append(displayNameFromLayout);
				displayNameFromLayout = this.m_StringBuilder.ToString();
			}
			if (shortName)
			{
				control.m_ShortDisplayNameFromLayout = displayNameFromLayout;
				return;
			}
			control.m_DisplayNameFromLayout = displayNameFromLayout;
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x00059348 File Offset: 0x00057548
		private static void AddParentDisplayNameRecursive(InputControl control, StringBuilder stringBuilder, bool shortName)
		{
			if (control.parent != null && control.parent != control.device)
			{
				InputDeviceBuilder.AddParentDisplayNameRecursive(control.parent, stringBuilder, shortName);
			}
			if (shortName)
			{
				string text = control.shortDisplayName;
				if (string.IsNullOrEmpty(text))
				{
					text = control.displayName;
				}
				stringBuilder.Append(text);
			}
			else
			{
				stringBuilder.Append(control.displayName);
			}
			stringBuilder.Append(' ');
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x000593B4 File Offset: 0x000575B4
		private static void AddProcessors(InputControl control, ref InputControlLayout.ControlItem controlItem, string layoutName)
		{
			int processorCount = controlItem.processors.Count;
			for (int i = 0; i < processorCount; i++)
			{
				string name = controlItem.processors[i].name;
				Type type = InputProcessor.s_Processors.LookupTypeRegistration(name);
				if (type == null)
				{
					throw new InvalidOperationException(string.Format("Cannot find processor '{0}' referenced by control '{1}' in layout '{2}'", name, controlItem.name, layoutName));
				}
				object processor = Activator.CreateInstance(type);
				ReadOnlyArray<NamedValue> parameters = controlItem.processors[i].parameters;
				if (parameters.Count > 0)
				{
					NamedValue.ApplyAllToObject<ReadOnlyArray<NamedValue>>(processor, parameters);
				}
				control.AddProcessor(processor);
			}
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x00059468 File Offset: 0x00057668
		private static void SetFormat(InputControl control, InputControlLayout.ControlItem controlItem)
		{
			control.m_StateBlock.format = controlItem.format;
			if (controlItem.sizeInBits == 0U)
			{
				int primitiveFormatSize = InputStateBlock.GetSizeOfPrimitiveFormatInBits(controlItem.format);
				if (primitiveFormatSize != -1)
				{
					control.m_StateBlock.sizeInBits = (uint)primitiveFormatSize;
				}
			}
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x000594AD File Offset: 0x000576AD
		private static InputControlLayout FindOrLoadLayout(string name)
		{
			return InputControlLayout.cache.FindOrLoadLayout(name, true);
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x000594BC File Offset: 0x000576BC
		private static void ComputeStateLayout(InputControl control)
		{
			ReadOnlyArray<InputControl> children = control.children;
			if (control.m_StateBlock.sizeInBits == 0U && control.m_StateBlock.format != 0)
			{
				int sizeInBits = InputStateBlock.GetSizeOfPrimitiveFormatInBits(control.m_StateBlock.format);
				if (sizeInBits != -1)
				{
					control.m_StateBlock.sizeInBits = (uint)sizeInBits;
				}
			}
			if (control.m_StateBlock.sizeInBits == 0U && children.Count == 0)
			{
				throw new InvalidOperationException(string.Concat(new string[] { "Control '", control.path, "' with layout '", control.layout, "' has no size set and has no children to compute size from" }));
			}
			if (children.Count == 0)
			{
				return;
			}
			uint firstUnfixedByteOffset = 0U;
			foreach (InputControl child in children)
			{
				if (child.m_StateBlock.sizeInBits != 4294967295U)
				{
					uint childSizeInBits = child.m_StateBlock.sizeInBits;
					if (childSizeInBits == 0U || childSizeInBits == 4294967295U)
					{
						throw new InvalidOperationException(string.Concat(new string[] { "Child '", child.name, "' of '", control.name, "' has no size set!" }));
					}
					if (child.m_StateBlock.byteOffset != 4294967295U && child.m_StateBlock.byteOffset != 4294967294U)
					{
						if (child.m_StateBlock.bitOffset == 4294967295U)
						{
							child.m_StateBlock.bitOffset = 0U;
						}
						uint endOffset = MemoryHelpers.ComputeFollowingByteOffset(child.m_StateBlock.byteOffset, child.m_StateBlock.bitOffset + childSizeInBits);
						if (endOffset > firstUnfixedByteOffset)
						{
							firstUnfixedByteOffset = endOffset;
						}
					}
				}
			}
			uint runningByteOffset = firstUnfixedByteOffset;
			InputControl firstBitAddressingChild = null;
			uint bitfieldSizeInBits = 0U;
			foreach (InputControl child2 in children)
			{
				if ((child2.m_StateBlock.byteOffset == 4294967295U || child2.m_StateBlock.byteOffset == 4294967294U) && child2.m_StateBlock.sizeInBits != 4294967295U)
				{
					bool flag = child2.m_StateBlock.sizeInBits % 8U > 0U;
					if (flag)
					{
						if (firstBitAddressingChild == null)
						{
							firstBitAddressingChild = child2;
						}
						if (child2.m_StateBlock.bitOffset == 4294967295U || child2.m_StateBlock.bitOffset == 4294967294U)
						{
							child2.m_StateBlock.bitOffset = bitfieldSizeInBits;
							bitfieldSizeInBits += child2.m_StateBlock.sizeInBits;
						}
						else
						{
							uint lastBit = child2.m_StateBlock.bitOffset + child2.m_StateBlock.sizeInBits;
							if (lastBit > bitfieldSizeInBits)
							{
								bitfieldSizeInBits = lastBit;
							}
						}
					}
					else
					{
						if (firstBitAddressingChild != null)
						{
							runningByteOffset = MemoryHelpers.ComputeFollowingByteOffset(runningByteOffset, bitfieldSizeInBits);
							firstBitAddressingChild = null;
						}
						if (child2.m_StateBlock.bitOffset == 4294967295U)
						{
							child2.m_StateBlock.bitOffset = 0U;
						}
						runningByteOffset = MemoryHelpers.AlignNatural(runningByteOffset, child2.m_StateBlock.alignedSizeInBytes);
					}
					child2.m_StateBlock.byteOffset = runningByteOffset;
					if (!flag)
					{
						runningByteOffset = MemoryHelpers.ComputeFollowingByteOffset(runningByteOffset, child2.m_StateBlock.sizeInBits);
					}
				}
			}
			if (firstBitAddressingChild != null)
			{
				runningByteOffset = MemoryHelpers.ComputeFollowingByteOffset(runningByteOffset, bitfieldSizeInBits);
			}
			uint totalSizeInBytes = runningByteOffset;
			control.m_StateBlock.sizeInBits = totalSizeInBytes * 8U;
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x00059814 File Offset: 0x00057A14
		private void FinalizeControlHierarchy()
		{
			if (this.m_StateOffsetToControlMap == null)
			{
				this.m_StateOffsetToControlMap = new List<uint>();
			}
			if ((long)this.m_Device.allControls.Count > 1024L)
			{
				throw new NotSupportedException(string.Format("Device '{0}' exceeds maximum supported control count of {1} (has {2} controls)", this.m_Device, 1024U, this.m_Device.allControls.Count));
			}
			InputDevice.ControlBitRangeNode rootNode = new InputDevice.ControlBitRangeNode((ushort)(this.m_Device.m_StateBlock.sizeInBits - 1U));
			this.m_Device.m_ControlTreeNodes = new InputDevice.ControlBitRangeNode[1];
			this.m_Device.m_ControlTreeNodes[0] = rootNode;
			int controlIndiciesNextFreeIndex = 0;
			this.FinalizeControlHierarchyRecursive(this.m_Device, -1, this.m_Device.m_ChildrenForEachControl, false, false, ref controlIndiciesNextFreeIndex);
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x000598E4 File Offset: 0x00057AE4
		private void FinalizeControlHierarchyRecursive(InputControl control, int controlIndex, InputControl[] allControls, bool noisy, bool dontReset, ref int controlIndiciesNextFreeIndex)
		{
			if (control.m_ChildCount == 0)
			{
				if (control.m_StateBlock.effectiveBitOffset >= 8192U)
				{
					throw new NotSupportedException(string.Format("Control '{0}' exceeds maximum supported state bit offset of {1} (bit offset {2})", control, 8191U, control.stateBlock.effectiveBitOffset));
				}
				if (control.m_StateBlock.sizeInBits >= 512U)
				{
					throw new NotSupportedException(string.Format("Control '{0}' exceeds maximum supported state bit size of {1} (bit offset {2})", control, 511U, control.stateBlock.sizeInBits));
				}
			}
			if (control != this.m_Device)
			{
				this.InsertControlBitRangeNode(ref this.m_Device.m_ControlTreeNodes[0], control, ref controlIndiciesNextFreeIndex, 0);
			}
			if (control.m_ChildCount == 0)
			{
				this.m_StateOffsetToControlMap.Add(InputDevice.EncodeStateOffsetToControlMapEntry((uint)controlIndex, control.m_StateBlock.effectiveBitOffset, control.m_StateBlock.sizeInBits));
			}
			string displayNameFromLayout = control.m_DisplayNameFromLayout;
			string shortDisplayNameFromLayout = control.m_ShortDisplayNameFromLayout;
			this.SetDisplayName(control, displayNameFromLayout, shortDisplayNameFromLayout, false);
			this.SetDisplayName(control, displayNameFromLayout, shortDisplayNameFromLayout, true);
			if (control != control.device)
			{
				if (noisy)
				{
					control.noisy = true;
				}
				else
				{
					noisy = control.noisy;
				}
				if (dontReset)
				{
					control.dontReset = true;
				}
				else
				{
					dontReset = control.dontReset;
				}
			}
			uint ourOffset = control.m_StateBlock.byteOffset;
			int childCount = control.m_ChildCount;
			int childStartIndex = control.m_ChildStartIndex;
			for (int i = 0; i < childCount; i++)
			{
				int childIndex = childStartIndex + i;
				InputControl child = allControls[childIndex];
				InputControl inputControl = child;
				inputControl.m_StateBlock.byteOffset = inputControl.m_StateBlock.byteOffset + ourOffset;
				this.FinalizeControlHierarchyRecursive(child, childIndex, allControls, noisy, dontReset, ref controlIndiciesNextFreeIndex);
			}
			control.isSetupFinished = true;
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x00059A8C File Offset: 0x00057C8C
		private void InsertControlBitRangeNode(ref InputDevice.ControlBitRangeNode parent, InputControl control, ref int controlIndiciesNextFreeIndex, ushort startOffset)
		{
			InputDevice.ControlBitRangeNode leftNode;
			InputDevice.ControlBitRangeNode rightNode;
			if (parent.leftChildIndex == -1)
			{
				ushort midPoint = this.GetBestMidPoint(parent, startOffset);
				leftNode = new InputDevice.ControlBitRangeNode(midPoint);
				rightNode = new InputDevice.ControlBitRangeNode(parent.endBitOffset);
				this.AddChildren(ref parent, leftNode, rightNode);
			}
			else
			{
				leftNode = this.m_Device.m_ControlTreeNodes[(int)parent.leftChildIndex];
				rightNode = this.m_Device.m_ControlTreeNodes[(int)(parent.leftChildIndex + 1)];
			}
			if (control.m_StateBlock.effectiveBitOffset < (uint)leftNode.endBitOffset && control.m_StateBlock.effectiveBitOffset + control.m_StateBlock.sizeInBits > (uint)leftNode.endBitOffset)
			{
				this.AddControlToNode(control, ref controlIndiciesNextFreeIndex, (int)parent.leftChildIndex);
				this.AddControlToNode(control, ref controlIndiciesNextFreeIndex, (int)(parent.leftChildIndex + 1));
				return;
			}
			if (control.m_StateBlock.effectiveBitOffset == (uint)startOffset && control.m_StateBlock.effectiveBitOffset + control.m_StateBlock.sizeInBits == (uint)leftNode.endBitOffset)
			{
				this.AddControlToNode(control, ref controlIndiciesNextFreeIndex, (int)parent.leftChildIndex);
				return;
			}
			if (control.m_StateBlock.effectiveBitOffset == (uint)leftNode.endBitOffset && control.m_StateBlock.effectiveBitOffset + control.m_StateBlock.sizeInBits == (uint)rightNode.endBitOffset)
			{
				this.AddControlToNode(control, ref controlIndiciesNextFreeIndex, (int)(parent.leftChildIndex + 1));
				return;
			}
			if (control.m_StateBlock.effectiveBitOffset < (uint)leftNode.endBitOffset)
			{
				this.InsertControlBitRangeNode(ref this.m_Device.m_ControlTreeNodes[(int)parent.leftChildIndex], control, ref controlIndiciesNextFreeIndex, startOffset);
				return;
			}
			this.InsertControlBitRangeNode(ref this.m_Device.m_ControlTreeNodes[(int)(parent.leftChildIndex + 1)], control, ref controlIndiciesNextFreeIndex, leftNode.endBitOffset);
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x00059C2C File Offset: 0x00057E2C
		private ushort GetBestMidPoint(InputDevice.ControlBitRangeNode parent, ushort startOffset)
		{
			ushort absoluteMidPoint = startOffset + ((parent.endBitOffset - startOffset - 1) / 2 + 1);
			ushort closestControlEndPointToMidPoint = ushort.MaxValue;
			ushort closestControlStartPointToMidPoint = ushort.MaxValue;
			InputControl[] array = this.m_Device.m_ChildrenForEachControl;
			for (int i = 0; i < array.Length; i++)
			{
				InputStateBlock stateBlock = array[i].m_StateBlock;
				if (stateBlock.effectiveBitOffset + stateBlock.sizeInBits - 1U >= (uint)startOffset && stateBlock.effectiveBitOffset < (uint)parent.endBitOffset && (ulong)stateBlock.sizeInBits <= (ulong)((long)(parent.endBitOffset - startOffset)) && stateBlock.effectiveBitOffset != (uint)startOffset && stateBlock.effectiveBitOffset + stateBlock.sizeInBits != (uint)parent.endBitOffset)
				{
					if (Math.Abs((long)((ulong)(stateBlock.effectiveBitOffset + stateBlock.sizeInBits) - (ulong)((long)absoluteMidPoint))) < (long)Math.Abs((int)(closestControlEndPointToMidPoint - absoluteMidPoint)) && stateBlock.effectiveBitOffset + stateBlock.sizeInBits < (uint)parent.endBitOffset)
					{
						closestControlEndPointToMidPoint = (ushort)(stateBlock.effectiveBitOffset + stateBlock.sizeInBits);
					}
					if (Math.Abs((long)((ulong)stateBlock.effectiveBitOffset - (ulong)((long)absoluteMidPoint))) < (long)Math.Abs((int)(closestControlStartPointToMidPoint - absoluteMidPoint)) && stateBlock.effectiveBitOffset >= (uint)startOffset)
					{
						closestControlStartPointToMidPoint = (ushort)stateBlock.effectiveBitOffset;
					}
				}
			}
			int absoluteMidPointCollisions = 0;
			int controlStartMidPointCollisions = 0;
			int controlEndMidPointCollisions = 0;
			foreach (InputControl control in this.m_Device.m_ChildrenForEachControl)
			{
				if (closestControlStartPointToMidPoint != 65535 && (uint)closestControlStartPointToMidPoint > control.m_StateBlock.effectiveBitOffset && (uint)closestControlStartPointToMidPoint < control.m_StateBlock.effectiveBitOffset + control.m_StateBlock.sizeInBits)
				{
					controlStartMidPointCollisions++;
				}
				if (closestControlEndPointToMidPoint != 65535 && (uint)closestControlEndPointToMidPoint > control.m_StateBlock.effectiveBitOffset && (uint)closestControlEndPointToMidPoint < control.m_StateBlock.effectiveBitOffset + control.m_StateBlock.sizeInBits)
				{
					controlEndMidPointCollisions++;
				}
				if ((uint)absoluteMidPoint > control.m_StateBlock.effectiveBitOffset && (uint)absoluteMidPoint < control.m_StateBlock.effectiveBitOffset + control.m_StateBlock.sizeInBits)
				{
					absoluteMidPointCollisions++;
				}
			}
			if (closestControlEndPointToMidPoint != 65535 && controlEndMidPointCollisions <= controlStartMidPointCollisions && controlEndMidPointCollisions <= absoluteMidPointCollisions)
			{
				return closestControlEndPointToMidPoint;
			}
			if (closestControlStartPointToMidPoint != 65535 && controlStartMidPointCollisions <= controlEndMidPointCollisions && controlStartMidPointCollisions <= absoluteMidPointCollisions)
			{
				return closestControlStartPointToMidPoint;
			}
			return absoluteMidPoint;
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x00059E68 File Offset: 0x00058068
		private void AddControlToNode(InputControl control, ref int controlIndiciesNextFreeIndex, int nodeIndex)
		{
			ref InputDevice.ControlBitRangeNode node = ref this.m_Device.m_ControlTreeNodes[nodeIndex];
			ushort leafControlStartIndex = node.controlStartIndex;
			if (node.controlCount == 0)
			{
				node.controlStartIndex = (ushort)controlIndiciesNextFreeIndex;
				leafControlStartIndex = node.controlStartIndex;
			}
			ArrayHelpers.InsertAt<ushort>(ref this.m_Device.m_ControlTreeIndices, (int)(node.controlStartIndex + (ushort)node.controlCount), this.GetControlIndex(control));
			ref InputDevice.ControlBitRangeNode ptr = ref node;
			ptr.controlCount += 1;
			controlIndiciesNextFreeIndex++;
			for (int i = 0; i < this.m_Device.m_ControlTreeNodes.Length; i++)
			{
				if (this.m_Device.m_ControlTreeNodes[i].controlCount != 0 && this.m_Device.m_ControlTreeNodes[i].controlStartIndex > leafControlStartIndex)
				{
					InputDevice.ControlBitRangeNode[] controlTreeNodes = this.m_Device.m_ControlTreeNodes;
					int num = i;
					controlTreeNodes[num].controlStartIndex = controlTreeNodes[num].controlStartIndex + 1;
				}
			}
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x00059F44 File Offset: 0x00058144
		private void AddChildren(ref InputDevice.ControlBitRangeNode parent, InputDevice.ControlBitRangeNode left, InputDevice.ControlBitRangeNode right)
		{
			if (parent.leftChildIndex != -1)
			{
				return;
			}
			int startIndex = this.m_Device.m_ControlTreeNodes.Length;
			parent.leftChildIndex = (short)startIndex;
			Array.Resize<InputDevice.ControlBitRangeNode>(ref this.m_Device.m_ControlTreeNodes, startIndex + 2);
			this.m_Device.m_ControlTreeNodes[startIndex] = left;
			this.m_Device.m_ControlTreeNodes[startIndex + 1] = right;
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x00059FAC File Offset: 0x000581AC
		private ushort GetControlIndex(InputControl control)
		{
			for (int i = 0; i < this.m_Device.m_ChildrenForEachControl.Length; i++)
			{
				if (control == this.m_Device.m_ChildrenForEachControl[i])
				{
					return (ushort)i;
				}
			}
			throw new InvalidOperationException(string.Format("InputDeviceBuilder error. Couldn't find control {0}.", control));
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x0600136C RID: 4972 RVA: 0x00059FF4 File Offset: 0x000581F4
		internal static ref InputDeviceBuilder instance
		{
			get
			{
				return ref InputDeviceBuilder.s_Instance;
			}
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x00059FFC File Offset: 0x000581FC
		internal static InputDeviceBuilder.RefInstance Ref()
		{
			InputDeviceBuilder.s_InstanceRef++;
			return default(InputDeviceBuilder.RefInstance);
		}

		// Token: 0x04000B92 RID: 2962
		private InputDevice m_Device;

		// Token: 0x04000B93 RID: 2963
		private InputControlLayout.CacheRefInstance m_LayoutCacheRef;

		// Token: 0x04000B94 RID: 2964
		private Dictionary<string, InputControlLayout.ControlItem> m_ChildControlOverrides;

		// Token: 0x04000B95 RID: 2965
		private List<uint> m_StateOffsetToControlMap;

		// Token: 0x04000B96 RID: 2966
		private StringBuilder m_StringBuilder;

		// Token: 0x04000B97 RID: 2967
		private const uint kSizeForControlUsingStateFromOtherControl = 4294967295U;

		// Token: 0x04000B98 RID: 2968
		private static InputDeviceBuilder s_Instance;

		// Token: 0x04000B99 RID: 2969
		private static int s_InstanceRef;

		// Token: 0x0200020B RID: 523
		internal struct RefInstance : IDisposable
		{
			// Token: 0x0600136E RID: 4974 RVA: 0x0005A01E File Offset: 0x0005821E
			public void Dispose()
			{
				InputDeviceBuilder.s_InstanceRef--;
				if (InputDeviceBuilder.s_InstanceRef <= 0)
				{
					InputDeviceBuilder.s_Instance.Dispose();
					InputDeviceBuilder.s_Instance = default(InputDeviceBuilder);
					InputDeviceBuilder.s_InstanceRef = 0;
					return;
				}
				InputDeviceBuilder.s_Instance.Reset();
			}
		}
	}
}
