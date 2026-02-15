using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Profiling;
using UnityEngine.InputSystem.Composites;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Processors;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x020000C0 RID: 192
	internal class InputManager
	{
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x0003495F File Offset: 0x00032B5F
		public ReadOnlyArray<InputDevice> devices
		{
			get
			{
				return new ReadOnlyArray<InputDevice>(this.m_Devices, 0, this.m_DevicesCount);
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000A2D RID: 2605 RVA: 0x00034973 File Offset: 0x00032B73
		public TypeTable processors
		{
			get
			{
				return this.m_Processors;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x0003497B File Offset: 0x00032B7B
		public TypeTable interactions
		{
			get
			{
				return this.m_Interactions;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x00034983 File Offset: 0x00032B83
		public TypeTable composites
		{
			get
			{
				return this.m_Composites;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x0003498C File Offset: 0x00032B8C
		public InputMetrics metrics
		{
			get
			{
				InputMetrics result = this.m_Metrics;
				result.currentNumDevices = this.m_DevicesCount;
				result.currentStateSizeInBytes = (int)this.m_StateBuffers.totalSize;
				result.currentControlCount = this.m_DevicesCount;
				for (int i = 0; i < this.m_DevicesCount; i++)
				{
					result.currentControlCount += this.m_Devices[i].allControls.Count;
				}
				result.currentLayoutCount = this.m_Layouts.layoutTypes.Count;
				result.currentLayoutCount += this.m_Layouts.layoutStrings.Count;
				result.currentLayoutCount += this.m_Layouts.layoutBuilders.Count;
				result.currentLayoutCount += this.m_Layouts.layoutOverrides.Count;
				return result;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x00034A72 File Offset: 0x00032C72
		// (set) Token: 0x06000A32 RID: 2610 RVA: 0x00034A7A File Offset: 0x00032C7A
		public InputSettings settings
		{
			get
			{
				return this.m_Settings;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this.m_Settings == value)
				{
					return;
				}
				this.m_Settings = value;
				this.ApplySettings();
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x00034AAC File Offset: 0x00032CAC
		// (set) Token: 0x06000A34 RID: 2612 RVA: 0x00034AB4 File Offset: 0x00032CB4
		public InputActionAsset actions
		{
			get
			{
				return this.m_Actions;
			}
			set
			{
				this.m_Actions = value;
				this.ApplyActions();
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000A35 RID: 2613 RVA: 0x00034AC3 File Offset: 0x00032CC3
		// (set) Token: 0x06000A36 RID: 2614 RVA: 0x00034ACB File Offset: 0x00032CCB
		public InputUpdateType updateMask
		{
			get
			{
				return this.m_UpdateMask;
			}
			set
			{
				if (this.m_UpdateMask == value)
				{
					return;
				}
				this.m_UpdateMask = value;
				if (this.m_DevicesCount > 0)
				{
					this.ReallocateStateBuffers();
				}
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x00034AED File Offset: 0x00032CED
		public InputUpdateType defaultUpdateType
		{
			get
			{
				if (this.m_CurrentUpdate != InputUpdateType.None)
				{
					return this.m_CurrentUpdate;
				}
				return this.m_UpdateMask.GetUpdateTypeForPlayer();
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x00034B09 File Offset: 0x00032D09
		// (set) Token: 0x06000A39 RID: 2617 RVA: 0x00034B11 File Offset: 0x00032D11
		public InputSettings.ScrollDeltaBehavior scrollDeltaBehavior
		{
			get
			{
				return this.m_ScrollDeltaBehavior;
			}
			set
			{
				if (this.m_ScrollDeltaBehavior == value)
				{
					return;
				}
				this.m_ScrollDeltaBehavior = value;
				InputRuntime.s_Instance.normalizeScrollWheelDelta = this.m_ScrollDeltaBehavior == InputSettings.ScrollDeltaBehavior.UniformAcrossAllPlatforms;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x00034B37 File Offset: 0x00032D37
		// (set) Token: 0x06000A3B RID: 2619 RVA: 0x00034B3F File Offset: 0x00032D3F
		public float pollingFrequency
		{
			get
			{
				return this.m_PollingFrequency;
			}
			set
			{
				if (value <= 0f)
				{
					throw new ArgumentException("Polling frequency must be greater than zero", "value");
				}
				this.m_PollingFrequency = value;
				if (this.m_Runtime != null)
				{
					this.m_Runtime.pollingFrequency = value;
				}
			}
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000A3C RID: 2620 RVA: 0x00034B74 File Offset: 0x00032D74
		// (remove) Token: 0x06000A3D RID: 2621 RVA: 0x00034B82 File Offset: 0x00032D82
		public event Action<InputDevice, InputDeviceChange> onDeviceChange
		{
			add
			{
				this.m_DeviceChangeListeners.AddCallback(value);
			}
			remove
			{
				this.m_DeviceChangeListeners.RemoveCallback(value);
			}
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000A3E RID: 2622 RVA: 0x00034B90 File Offset: 0x00032D90
		// (remove) Token: 0x06000A3F RID: 2623 RVA: 0x00034B9E File Offset: 0x00032D9E
		public event Action<InputDevice, InputEventPtr> onDeviceStateChange
		{
			add
			{
				this.m_DeviceStateChangeListeners.AddCallback(value);
			}
			remove
			{
				this.m_DeviceStateChangeListeners.RemoveCallback(value);
			}
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000A40 RID: 2624 RVA: 0x00034BAC File Offset: 0x00032DAC
		// (remove) Token: 0x06000A41 RID: 2625 RVA: 0x00034BBA File Offset: 0x00032DBA
		public event InputDeviceCommandDelegate onDeviceCommand
		{
			add
			{
				this.m_DeviceCommandCallbacks.AddCallback(value);
			}
			remove
			{
				this.m_DeviceCommandCallbacks.RemoveCallback(value);
			}
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000A42 RID: 2626 RVA: 0x00034BC8 File Offset: 0x00032DC8
		// (remove) Token: 0x06000A43 RID: 2627 RVA: 0x00034BDC File Offset: 0x00032DDC
		public event InputDeviceFindControlLayoutDelegate onFindControlLayoutForDevice
		{
			add
			{
				this.m_DeviceFindLayoutCallbacks.AddCallback(value);
				this.AddAvailableDevicesThatAreNowRecognized();
			}
			remove
			{
				this.m_DeviceFindLayoutCallbacks.RemoveCallback(value);
			}
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000A44 RID: 2628 RVA: 0x00034BEA File Offset: 0x00032DEA
		// (remove) Token: 0x06000A45 RID: 2629 RVA: 0x00034BF8 File Offset: 0x00032DF8
		public event Action<string, InputControlLayoutChange> onLayoutChange
		{
			add
			{
				this.m_LayoutChangeListeners.AddCallback(value);
			}
			remove
			{
				this.m_LayoutChangeListeners.RemoveCallback(value);
			}
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000A46 RID: 2630 RVA: 0x00034C06 File Offset: 0x00032E06
		// (remove) Token: 0x06000A47 RID: 2631 RVA: 0x00034C14 File Offset: 0x00032E14
		public event Action<InputEventPtr, InputDevice> onEvent
		{
			add
			{
				this.m_EventListeners.AddCallback(value);
			}
			remove
			{
				this.m_EventListeners.RemoveCallback(value);
			}
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000A48 RID: 2632 RVA: 0x00034C22 File Offset: 0x00032E22
		// (remove) Token: 0x06000A49 RID: 2633 RVA: 0x00034C36 File Offset: 0x00032E36
		public event Action onBeforeUpdate
		{
			add
			{
				this.InstallBeforeUpdateHookIfNecessary();
				this.m_BeforeUpdateListeners.AddCallback(value);
			}
			remove
			{
				this.m_BeforeUpdateListeners.RemoveCallback(value);
			}
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000A4A RID: 2634 RVA: 0x00034C44 File Offset: 0x00032E44
		// (remove) Token: 0x06000A4B RID: 2635 RVA: 0x00034C52 File Offset: 0x00032E52
		public event Action onAfterUpdate
		{
			add
			{
				this.m_AfterUpdateListeners.AddCallback(value);
			}
			remove
			{
				this.m_AfterUpdateListeners.RemoveCallback(value);
			}
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000A4C RID: 2636 RVA: 0x00034C60 File Offset: 0x00032E60
		// (remove) Token: 0x06000A4D RID: 2637 RVA: 0x00034C6E File Offset: 0x00032E6E
		public event Action onSettingsChange
		{
			add
			{
				this.m_SettingsChangedListeners.AddCallback(value);
			}
			remove
			{
				this.m_SettingsChangedListeners.RemoveCallback(value);
			}
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000A4E RID: 2638 RVA: 0x00034C7C File Offset: 0x00032E7C
		// (remove) Token: 0x06000A4F RID: 2639 RVA: 0x00034C8A File Offset: 0x00032E8A
		public event Action onActionsChange
		{
			add
			{
				this.m_ActionsChangedListeners.AddCallback(value);
			}
			remove
			{
				this.m_ActionsChangedListeners.RemoveCallback(value);
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x00034C98 File Offset: 0x00032E98
		public bool isProcessingEvents
		{
			get
			{
				return this.m_InputEventStream.isOpen;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x00034CA5 File Offset: 0x00032EA5
		private bool gameIsPlaying
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x00034CA8 File Offset: 0x00032EA8
		private bool gameHasFocus
		{
			get
			{
				return this.m_HasFocus || this.gameShouldGetInputRegardlessOfFocus;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x00034CBA File Offset: 0x00032EBA
		private bool gameShouldGetInputRegardlessOfFocus
		{
			get
			{
				return this.m_Settings.backgroundBehavior == InputSettings.BackgroundBehavior.IgnoreFocus;
			}
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x00034CCC File Offset: 0x00032ECC
		public void RegisterControlLayout(string name, Type type)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			bool isDeviceLayout = typeof(InputDevice).IsAssignableFrom(type);
			bool isControlLayout = typeof(InputControl).IsAssignableFrom(type);
			if (!isDeviceLayout && !isControlLayout)
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"Types used as layouts have to be InputControls or InputDevices; '",
					type.Name,
					"' is a '",
					type.BaseType.Name,
					"'"
				}), "type");
			}
			InternedString internedName = new InternedString(name);
			bool isReplacement = this.m_Layouts.HasLayout(internedName);
			this.m_Layouts.layoutTypes[internedName] = type;
			string baseLayout = null;
			Type baseType = type.BaseType;
			while (baseLayout == null && baseType != typeof(InputControl))
			{
				foreach (KeyValuePair<InternedString, Type> entry in this.m_Layouts.layoutTypes)
				{
					if (entry.Value == baseType)
					{
						baseLayout = entry.Key;
						break;
					}
				}
				baseType = baseType.BaseType;
			}
			this.PerformLayoutPostRegistration(internedName, new InlinedArray<InternedString>(new InternedString(baseLayout)), isReplacement, isDeviceLayout, false);
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x00034E40 File Offset: 0x00033040
		public void RegisterControlLayout(string json, string name = null, bool isOverride = false)
		{
			if (string.IsNullOrEmpty(json))
			{
				throw new ArgumentNullException("json");
			}
			InternedString nameFromJson;
			InlinedArray<InternedString> baseLayouts;
			InputDeviceMatcher deviceMatcher;
			InputControlLayout.ParseHeaderFieldsFromJson(json, out nameFromJson, out baseLayouts, out deviceMatcher);
			InternedString internedLayoutName = new InternedString(name);
			if (internedLayoutName.IsEmpty())
			{
				internedLayoutName = nameFromJson;
				if (internedLayoutName.IsEmpty())
				{
					throw new ArgumentException("Layout name has not been given and is not set in JSON layout", "name");
				}
			}
			if (isOverride && baseLayouts.length == 0)
			{
				throw new ArgumentException(string.Format("Layout override '{0}' must have 'extend' property mentioning layout to which to apply the overrides", internedLayoutName), "json");
			}
			bool isReplacement = this.m_Layouts.HasLayout(internedLayoutName);
			if (isReplacement && isOverride && !this.m_Layouts.layoutOverrideNames.Contains(internedLayoutName))
			{
				throw new ArgumentException(string.Format("Failed to register layout override '{0}'", internedLayoutName) + string.Format("since a layout named '{0}' already exist. Layout overrides must ", internedLayoutName) + "have unique names with respect to existing layouts.");
			}
			this.m_Layouts.layoutStrings[internedLayoutName] = json;
			if (isOverride)
			{
				this.m_Layouts.layoutOverrideNames.Add(internedLayoutName);
				for (int i = 0; i < baseLayouts.length; i++)
				{
					InternedString baseLayoutName = baseLayouts[i];
					InternedString[] overrideList;
					this.m_Layouts.layoutOverrides.TryGetValue(baseLayoutName, out overrideList);
					if (!isReplacement)
					{
						ArrayHelpers.Append<InternedString>(ref overrideList, internedLayoutName);
					}
					this.m_Layouts.layoutOverrides[baseLayoutName] = overrideList;
				}
			}
			this.PerformLayoutPostRegistration(internedLayoutName, baseLayouts, isReplacement, false, isOverride);
			if (!deviceMatcher.empty)
			{
				this.RegisterControlLayoutMatcher(internedLayoutName, deviceMatcher);
			}
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x00034FB4 File Offset: 0x000331B4
		public void RegisterControlLayoutBuilder(Func<InputControlLayout> method, string name, string baseLayout = null)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			InternedString internedLayoutName = new InternedString(name);
			InternedString internedBaseLayoutName = new InternedString(baseLayout);
			bool isReplacement = this.m_Layouts.HasLayout(internedLayoutName);
			this.m_Layouts.layoutBuilders[internedLayoutName] = method;
			this.PerformLayoutPostRegistration(internedLayoutName, new InlinedArray<InternedString>(internedBaseLayoutName), isReplacement, false, false);
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x00035024 File Offset: 0x00033224
		private void PerformLayoutPostRegistration(InternedString layoutName, InlinedArray<InternedString> baseLayouts, bool isReplacement, bool isKnownToBeDeviceLayout = false, bool isOverride = false)
		{
			this.m_LayoutRegistrationVersion++;
			InputControlLayout.s_CacheInstance.Clear();
			if (!isOverride && baseLayouts.length > 0)
			{
				if (baseLayouts.length > 1)
				{
					throw new NotSupportedException(string.Format("Layout '{0}' has multiple base layouts; this is only supported on layout overrides", layoutName));
				}
				InternedString baseLayoutName = baseLayouts[0];
				if (!baseLayoutName.IsEmpty())
				{
					this.m_Layouts.baseLayoutTable[layoutName] = baseLayoutName;
				}
			}
			this.m_Layouts.precompiledLayouts.Remove(layoutName);
			if (this.m_Layouts.precompiledLayouts.Count > 0)
			{
				foreach (InternedString layout in this.m_Layouts.precompiledLayouts.Keys.ToArray<InternedString>())
				{
					string metadata = this.m_Layouts.precompiledLayouts[layout].metadata;
					if (isOverride)
					{
						for (int i = 0; i < baseLayouts.length; i++)
						{
							if (layout == baseLayouts[i] || StringHelpers.CharacterSeparatedListsHaveAtLeastOneCommonElement(metadata, baseLayouts[i], ';'))
							{
								this.m_Layouts.precompiledLayouts.Remove(layout);
							}
						}
					}
					else if (StringHelpers.CharacterSeparatedListsHaveAtLeastOneCommonElement(metadata, layoutName, ';'))
					{
						this.m_Layouts.precompiledLayouts.Remove(layout);
					}
				}
			}
			if (isOverride)
			{
				for (int j = 0; j < baseLayouts.length; j++)
				{
					this.RecreateDevicesUsingLayout(baseLayouts[j], isKnownToBeDeviceLayout);
				}
			}
			else
			{
				this.RecreateDevicesUsingLayout(layoutName, isKnownToBeDeviceLayout);
			}
			InputControlLayoutChange change = (isReplacement ? InputControlLayoutChange.Replaced : InputControlLayoutChange.Added);
			DelegateHelpers.InvokeCallbacksSafe<string, InputControlLayoutChange>(ref this.m_LayoutChangeListeners, layoutName.ToString(), change, InputManager.k_InputOnLayoutChangeMarker, "InputSystem.onLayoutChange", null);
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x000351EC File Offset: 0x000333EC
		public void RegisterPrecompiledLayout<TDevice>(string metadata) where TDevice : InputDevice, new()
		{
			if (metadata == null)
			{
				throw new ArgumentNullException("metadata");
			}
			Type deviceType = typeof(TDevice).BaseType;
			InternedString layoutName = this.FindOrRegisterDeviceLayoutForType(deviceType);
			Dictionary<InternedString, InputControlLayout.Collection.PrecompiledLayout> precompiledLayouts = this.m_Layouts.precompiledLayouts;
			InternedString internedString = layoutName;
			InputControlLayout.Collection.PrecompiledLayout precompiledLayout = default(InputControlLayout.Collection.PrecompiledLayout);
			precompiledLayout.factoryMethod = () => new TDevice();
			precompiledLayout.metadata = metadata;
			precompiledLayouts[internedString] = precompiledLayout;
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x00035268 File Offset: 0x00033468
		private void RecreateDevicesUsingLayout(InternedString layout, bool isKnownToBeDeviceLayout = false)
		{
			if (this.m_DevicesCount == 0)
			{
				return;
			}
			List<InputDevice> devicesUsingLayout = null;
			for (int i = 0; i < this.m_DevicesCount; i++)
			{
				InputDevice device = this.m_Devices[i];
				bool usesLayout;
				if (isKnownToBeDeviceLayout)
				{
					usesLayout = this.IsControlUsingLayout(device, layout);
				}
				else
				{
					usesLayout = this.IsControlOrChildUsingLayoutRecursive(device, layout);
				}
				if (usesLayout)
				{
					if (devicesUsingLayout == null)
					{
						devicesUsingLayout = new List<InputDevice>();
					}
					devicesUsingLayout.Add(device);
				}
			}
			if (devicesUsingLayout == null)
			{
				return;
			}
			using (InputDeviceBuilder.Ref())
			{
				for (int j = 0; j < devicesUsingLayout.Count; j++)
				{
					InputDevice device2 = devicesUsingLayout[j];
					this.RecreateDevice(device2, device2.m_Layout);
				}
			}
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00035320 File Offset: 0x00033520
		private bool IsControlOrChildUsingLayoutRecursive(InputControl control, InternedString layout)
		{
			if (this.IsControlUsingLayout(control, layout))
			{
				return true;
			}
			ReadOnlyArray<InputControl> children = control.children;
			for (int i = 0; i < children.Count; i++)
			{
				if (this.IsControlOrChildUsingLayoutRecursive(children[i], layout))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00035368 File Offset: 0x00033568
		private bool IsControlUsingLayout(InputControl control, InternedString layout)
		{
			if (control.layout == layout)
			{
				return true;
			}
			InternedString baseLayout = control.m_Layout;
			while (this.m_Layouts.baseLayoutTable.TryGetValue(baseLayout, out baseLayout))
			{
				if (baseLayout == layout)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x000353B0 File Offset: 0x000335B0
		public void RegisterControlLayoutMatcher(string layoutName, InputDeviceMatcher matcher)
		{
			if (string.IsNullOrEmpty(layoutName))
			{
				throw new ArgumentNullException("layoutName");
			}
			if (matcher.empty)
			{
				throw new ArgumentException("Matcher cannot be empty", "matcher");
			}
			InternedString internedLayoutName = new InternedString(layoutName);
			this.m_Layouts.AddMatcher(internedLayoutName, matcher);
			this.RecreateDevicesUsingLayoutWithInferiorMatch(matcher);
			this.AddAvailableDevicesMatchingDescription(matcher, internedLayoutName);
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x00035410 File Offset: 0x00033610
		public void RegisterControlLayoutMatcher(Type type, InputDeviceMatcher matcher)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (matcher.empty)
			{
				throw new ArgumentException("Matcher cannot be empty", "matcher");
			}
			InternedString layoutName = this.m_Layouts.TryFindLayoutForType(type);
			if (layoutName.IsEmpty())
			{
				throw new ArgumentException("Type '" + type.Name + "' has not been registered as a control layout", "type");
			}
			this.RegisterControlLayoutMatcher(layoutName, matcher);
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00035490 File Offset: 0x00033690
		private void RecreateDevicesUsingLayoutWithInferiorMatch(InputDeviceMatcher deviceMatcher)
		{
			if (this.m_DevicesCount == 0)
			{
				return;
			}
			using (InputDeviceBuilder.Ref())
			{
				int deviceCount = this.m_DevicesCount;
				for (int i = 0; i < deviceCount; i++)
				{
					InputDevice device = this.m_Devices[i];
					InputDeviceDescription deviceDescription = device.description;
					if (!deviceDescription.empty && deviceMatcher.MatchPercentage(deviceDescription) > 0f)
					{
						InternedString layoutName = this.TryFindMatchingControlLayout(ref deviceDescription, device.deviceId);
						if (layoutName != device.m_Layout)
						{
							device.m_Description = deviceDescription;
							this.RecreateDevice(device, layoutName);
							i--;
							deviceCount--;
						}
					}
				}
			}
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00035544 File Offset: 0x00033744
		private void RecreateDevice(InputDevice oldDevice, InternedString newLayout)
		{
			this.RemoveDevice(oldDevice, true);
			InputDevice newDevice = InputDevice.Build<InputDevice>(newLayout, oldDevice.m_Variants, oldDevice.m_Description, false);
			newDevice.m_DeviceId = oldDevice.m_DeviceId;
			newDevice.m_Description = oldDevice.m_Description;
			if (oldDevice.native)
			{
				newDevice.m_DeviceFlags |= InputDevice.DeviceFlags.Native;
			}
			if (oldDevice.remote)
			{
				newDevice.m_DeviceFlags |= InputDevice.DeviceFlags.Remote;
			}
			if (!oldDevice.enabled)
			{
				newDevice.m_DeviceFlags |= InputDevice.DeviceFlags.DisabledStateHasBeenQueriedFromRuntime;
				newDevice.m_DeviceFlags |= InputDevice.DeviceFlags.DisabledInFrontend;
			}
			this.AddDevice(newDevice);
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x000355EC File Offset: 0x000337EC
		private void AddAvailableDevicesMatchingDescription(InputDeviceMatcher matcher, InternedString layout)
		{
			for (int i = 0; i < this.m_AvailableDeviceCount; i++)
			{
				if (!this.m_AvailableDevices[i].isRemoved)
				{
					int deviceId = this.m_AvailableDevices[i].deviceId;
					if (this.TryGetDeviceById(deviceId) == null && matcher.MatchPercentage(this.m_AvailableDevices[i].description) > 0f)
					{
						try
						{
							this.AddDevice(layout, deviceId, null, this.m_AvailableDevices[i].description, this.m_AvailableDevices[i].isNative ? InputDevice.DeviceFlags.Native : ((InputDevice.DeviceFlags)0), default(InternedString));
						}
						catch (Exception exception)
						{
							Debug.LogError(string.Format("Layout '{0}' matches existing device '{1}' but failed to instantiate: {2}", layout, this.m_AvailableDevices[i].description, exception));
							Debug.LogException(exception);
							goto IL_00E8;
						}
						EnableDeviceCommand command = EnableDeviceCommand.Create();
						this.m_Runtime.DeviceCommand(deviceId, ref command);
					}
				}
				IL_00E8:;
			}
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x00035704 File Offset: 0x00033904
		public void RemoveControlLayout(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			InternedString internedName = new InternedString(name);
			int i = 0;
			while (i < this.m_DevicesCount)
			{
				InputDevice device = this.m_Devices[i];
				if (this.IsControlOrChildUsingLayoutRecursive(device, internedName))
				{
					this.RemoveDevice(device, true);
				}
				else
				{
					i++;
				}
			}
			this.m_Layouts.layoutTypes.Remove(internedName);
			this.m_Layouts.layoutStrings.Remove(internedName);
			this.m_Layouts.layoutBuilders.Remove(internedName);
			this.m_Layouts.baseLayoutTable.Remove(internedName);
			this.m_LayoutRegistrationVersion++;
			DelegateHelpers.InvokeCallbacksSafe<string, InputControlLayoutChange>(ref this.m_LayoutChangeListeners, name, InputControlLayoutChange.Removed, InputManager.k_InputOnLayoutChangeMarker, "InputSystem.onLayoutChange", null);
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x000357C8 File Offset: 0x000339C8
		public InputControlLayout TryLoadControlLayout(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!typeof(InputControl).IsAssignableFrom(type))
			{
				throw new ArgumentException("Type '" + type.Name + "' is not an InputControl", "type");
			}
			InternedString layoutName = this.m_Layouts.TryFindLayoutForType(type);
			if (layoutName.IsEmpty())
			{
				throw new ArgumentException("Type '" + type.Name + "' has not been registered as a control layout", "type");
			}
			return this.m_Layouts.TryLoadLayout(layoutName, null);
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0003585E File Offset: 0x00033A5E
		public InputControlLayout TryLoadControlLayout(InternedString name)
		{
			return this.m_Layouts.TryLoadLayout(name, null);
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x00035870 File Offset: 0x00033A70
		public InternedString TryFindMatchingControlLayout(ref InputDeviceDescription deviceDescription, int deviceId = 0)
		{
			InternedString layoutName = new InternedString(string.Empty);
			try
			{
				layoutName = this.m_Layouts.TryFindMatchingLayout(deviceDescription);
				if (layoutName.IsEmpty() && !string.IsNullOrEmpty(deviceDescription.deviceClass))
				{
					InternedString deviceClassLowerCase = new InternedString(deviceDescription.deviceClass);
					Type type = this.m_Layouts.GetControlTypeForLayout(deviceClassLowerCase);
					if (type != null && typeof(InputDevice).IsAssignableFrom(type))
					{
						layoutName = new InternedString(deviceDescription.deviceClass);
					}
				}
				if (this.m_DeviceFindLayoutCallbacks.length > 0)
				{
					if (this.m_DeviceFindExecuteCommandDelegate == null)
					{
						this.m_DeviceFindExecuteCommandDelegate = delegate(ref InputDeviceCommand commandRef)
						{
							if (this.m_DeviceFindExecuteCommandDeviceId == 0)
							{
								return -1L;
							}
							return this.m_Runtime.DeviceCommand(this.m_DeviceFindExecuteCommandDeviceId, ref commandRef);
						};
					}
					this.m_DeviceFindExecuteCommandDeviceId = deviceId;
					bool haveOverriddenLayoutName = false;
					this.m_DeviceFindLayoutCallbacks.LockForChanges();
					for (int i = 0; i < this.m_DeviceFindLayoutCallbacks.length; i++)
					{
						try
						{
							string newLayout = this.m_DeviceFindLayoutCallbacks[i](ref deviceDescription, layoutName, this.m_DeviceFindExecuteCommandDelegate);
							if (!string.IsNullOrEmpty(newLayout) && !haveOverriddenLayoutName)
							{
								layoutName = new InternedString(newLayout);
								haveOverriddenLayoutName = true;
							}
						}
						catch (Exception ex)
						{
							Debug.LogError(ex.GetType().Name + " while executing 'InputSystem.onFindLayoutForDevice' callbacks");
							Debug.LogException(ex);
						}
					}
					this.m_DeviceFindLayoutCallbacks.UnlockForChanges();
				}
			}
			finally
			{
			}
			return layoutName;
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x000359EC File Offset: 0x00033BEC
		private InternedString FindOrRegisterDeviceLayoutForType(Type type)
		{
			InternedString layoutName = this.m_Layouts.TryFindLayoutForType(type);
			if (layoutName.IsEmpty() && layoutName.IsEmpty())
			{
				layoutName = new InternedString(type.Name);
				this.RegisterControlLayout(type.Name, type);
			}
			return layoutName;
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x00035A34 File Offset: 0x00033C34
		private bool IsDeviceLayoutMarkedAsSupportedInSettings(InternedString layoutName)
		{
			ReadOnlyArray<string> supportedDevices = this.m_Settings.supportedDevices;
			if (supportedDevices.Count == 0)
			{
				return true;
			}
			for (int i = 0; i < supportedDevices.Count; i++)
			{
				InternedString supportedLayout = new InternedString(supportedDevices[i]);
				if (layoutName == supportedLayout || this.m_Layouts.IsBasedOn(supportedLayout, layoutName))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x00035A94 File Offset: 0x00033C94
		public IEnumerable<string> ListControlLayouts(string basedOn = null)
		{
			if (!string.IsNullOrEmpty(basedOn))
			{
				InternedString internedBasedOn = new InternedString(basedOn);
				foreach (KeyValuePair<InternedString, Type> entry in this.m_Layouts.layoutTypes)
				{
					if (this.m_Layouts.IsBasedOn(internedBasedOn, entry.Key))
					{
						yield return entry.Key;
					}
				}
				Dictionary<InternedString, Type>.Enumerator enumerator = default(Dictionary<InternedString, Type>.Enumerator);
				foreach (KeyValuePair<InternedString, string> entry2 in this.m_Layouts.layoutStrings)
				{
					if (this.m_Layouts.IsBasedOn(internedBasedOn, entry2.Key))
					{
						yield return entry2.Key;
					}
				}
				Dictionary<InternedString, string>.Enumerator enumerator2 = default(Dictionary<InternedString, string>.Enumerator);
				foreach (KeyValuePair<InternedString, Func<InputControlLayout>> entry3 in this.m_Layouts.layoutBuilders)
				{
					if (this.m_Layouts.IsBasedOn(internedBasedOn, entry3.Key))
					{
						yield return entry3.Key;
					}
				}
				Dictionary<InternedString, Func<InputControlLayout>>.Enumerator enumerator3 = default(Dictionary<InternedString, Func<InputControlLayout>>.Enumerator);
				internedBasedOn = default(InternedString);
			}
			else
			{
				foreach (KeyValuePair<InternedString, Type> entry4 in this.m_Layouts.layoutTypes)
				{
					yield return entry4.Key;
				}
				Dictionary<InternedString, Type>.Enumerator enumerator = default(Dictionary<InternedString, Type>.Enumerator);
				foreach (KeyValuePair<InternedString, string> entry5 in this.m_Layouts.layoutStrings)
				{
					yield return entry5.Key;
				}
				Dictionary<InternedString, string>.Enumerator enumerator2 = default(Dictionary<InternedString, string>.Enumerator);
				foreach (KeyValuePair<InternedString, Func<InputControlLayout>> entry6 in this.m_Layouts.layoutBuilders)
				{
					yield return entry6.Key;
				}
				Dictionary<InternedString, Func<InputControlLayout>>.Enumerator enumerator3 = default(Dictionary<InternedString, Func<InputControlLayout>>.Enumerator);
			}
			yield break;
			yield break;
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x00035AAC File Offset: 0x00033CAC
		public int GetControls<TControl>(string path, ref InputControlList<TControl> controls) where TControl : InputControl
		{
			if (string.IsNullOrEmpty(path))
			{
				return 0;
			}
			if (this.m_DevicesCount == 0)
			{
				return 0;
			}
			int deviceCount = this.m_DevicesCount;
			int numMatches = 0;
			for (int i = 0; i < deviceCount; i++)
			{
				InputDevice device = this.m_Devices[i];
				numMatches += InputControlPath.TryFindControls<TControl>(device, path, 0, ref controls);
			}
			return numMatches;
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00035AF8 File Offset: 0x00033CF8
		public void SetDeviceUsage(InputDevice device, InternedString usage)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (device.usages.Count == 1 && device.usages[0] == usage)
			{
				return;
			}
			if (device.usages.Count == 0 && usage.IsEmpty())
			{
				return;
			}
			device.ClearDeviceUsages();
			if (!usage.IsEmpty())
			{
				device.AddDeviceUsage(usage);
			}
			this.NotifyUsageChanged(device);
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x00035B74 File Offset: 0x00033D74
		public void AddDeviceUsage(InputDevice device, InternedString usage)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (usage.IsEmpty())
			{
				throw new ArgumentException("Usage string cannot be empty", "usage");
			}
			if (device.usages.Contains(usage))
			{
				return;
			}
			device.AddDeviceUsage(usage);
			this.NotifyUsageChanged(device);
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x00035BC8 File Offset: 0x00033DC8
		public void RemoveDeviceUsage(InputDevice device, InternedString usage)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (usage.IsEmpty())
			{
				throw new ArgumentException("Usage string cannot be empty", "usage");
			}
			if (!device.usages.Contains(usage))
			{
				return;
			}
			device.RemoveDeviceUsage(usage);
			this.NotifyUsageChanged(device);
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x00035C19 File Offset: 0x00033E19
		private void NotifyUsageChanged(InputDevice device)
		{
			InputActionState.OnDeviceChange(device, InputDeviceChange.UsageChanged);
			DelegateHelpers.InvokeCallbacksSafe<InputDevice, InputDeviceChange>(ref this.m_DeviceChangeListeners, device, InputDeviceChange.UsageChanged, InputManager.k_InputOnDeviceChangeMarker, "InputSystem.onDeviceChange", null);
			device.MakeCurrent();
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00035C40 File Offset: 0x00033E40
		public InputDevice AddDevice(Type type, string name = null)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			InternedString layoutName = this.FindOrRegisterDeviceLayoutForType(type);
			return this.AddDevice(layoutName, name, default(InternedString));
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00035C80 File Offset: 0x00033E80
		public InputDevice AddDevice(string layout, string name = null, InternedString variants = default(InternedString))
		{
			if (string.IsNullOrEmpty(layout))
			{
				throw new ArgumentNullException("layout");
			}
			InputDevice device = InputDevice.Build<InputDevice>(layout, variants, default(InputDeviceDescription), false);
			if (!string.IsNullOrEmpty(name))
			{
				device.m_Name = new InternedString(name);
			}
			this.AddDevice(device);
			return device;
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x00035CD4 File Offset: 0x00033ED4
		private InputDevice AddDevice(InternedString layout, int deviceId, string deviceName = null, InputDeviceDescription deviceDescription = default(InputDeviceDescription), InputDevice.DeviceFlags deviceFlags = (InputDevice.DeviceFlags)0, InternedString variants = default(InternedString))
		{
			string text = new InternedString(layout);
			InputDeviceDescription inputDeviceDescription = deviceDescription;
			InputDevice device = InputDevice.Build<InputDevice>(text, variants, inputDeviceDescription, false);
			device.m_DeviceId = deviceId;
			device.m_Description = deviceDescription;
			device.m_DeviceFlags |= deviceFlags;
			if (!string.IsNullOrEmpty(deviceName))
			{
				device.m_Name = new InternedString(deviceName);
			}
			if (!string.IsNullOrEmpty(deviceDescription.product))
			{
				device.m_DisplayName = deviceDescription.product;
			}
			this.AddDevice(device);
			return device;
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x00035D58 File Offset: 0x00033F58
		public void AddDevice(InputDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (string.IsNullOrEmpty(device.layout))
			{
				throw new InvalidOperationException("Device has no associated layout");
			}
			if (ArrayHelpers.Contains<InputDevice>(this.m_Devices, device))
			{
				return;
			}
			this.MakeDeviceNameUnique(device);
			this.AssignUniqueDeviceId(device);
			device.m_DeviceIndex = ArrayHelpers.AppendWithCapacity<InputDevice>(ref this.m_Devices, ref this.m_DevicesCount, device, 10);
			this.m_DevicesById[device.deviceId] = device;
			device.m_StateBlock.byteOffset = uint.MaxValue;
			this.ReallocateStateBuffers();
			this.InitializeDeviceState(device);
			this.m_Metrics.maxNumDevices = Mathf.Max(this.m_DevicesCount, this.m_Metrics.maxNumDevices);
			this.m_Metrics.maxStateSizeInBytes = Mathf.Max((int)this.m_StateBuffers.totalSize, this.m_Metrics.maxStateSizeInBytes);
			for (int i = 0; i < this.m_AvailableDeviceCount; i++)
			{
				if (this.m_AvailableDevices[i].deviceId == device.deviceId)
				{
					this.m_AvailableDevices[i].isRemoved = false;
				}
			}
			if (true && !this.gameHasFocus && this.m_Settings.backgroundBehavior != InputSettings.BackgroundBehavior.IgnoreFocus && this.m_Runtime.runInBackground && device.QueryEnabledStateFromRuntime() && !this.ShouldRunDeviceInBackground(device))
			{
				this.EnableOrDisableDevice(device, false, InputManager.DeviceDisableScope.TemporaryWhilePlayerIsInBackground);
			}
			InputActionState.OnDeviceChange(device, InputDeviceChange.Added);
			IInputUpdateCallbackReceiver beforeUpdateCallbackReceiver = device as IInputUpdateCallbackReceiver;
			if (beforeUpdateCallbackReceiver != null)
			{
				this.onBeforeUpdate += beforeUpdateCallbackReceiver.OnUpdate;
			}
			if (device is IInputStateCallbackReceiver)
			{
				this.InstallBeforeUpdateHookIfNecessary();
				device.m_DeviceFlags |= InputDevice.DeviceFlags.HasStateCallbacks;
				this.m_HaveDevicesWithStateCallbackReceivers = true;
			}
			if (device is IEventMerger)
			{
				device.hasEventMerger = true;
			}
			if (device is IEventPreProcessor)
			{
				device.hasEventPreProcessor = true;
			}
			if (device.updateBeforeRender)
			{
				this.updateMask |= InputUpdateType.BeforeRender;
			}
			device.NotifyAdded();
			device.MakeCurrent();
			DelegateHelpers.InvokeCallbacksSafe<InputDevice, InputDeviceChange>(ref this.m_DeviceChangeListeners, device, InputDeviceChange.Added, InputManager.k_InputOnDeviceChangeMarker, "InputSystem.onDeviceChange", null);
			if (device.enabled)
			{
				device.RequestSync();
			}
			device.SetOptimizedControlDataTypeRecursively();
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00035F64 File Offset: 0x00034164
		public InputDevice AddDevice(InputDeviceDescription description)
		{
			return this.AddDevice(description, true, null, 0, (InputDevice.DeviceFlags)0);
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00035F74 File Offset: 0x00034174
		public InputDevice AddDevice(InputDeviceDescription description, bool throwIfNoLayoutFound, string deviceName = null, int deviceId = 0, InputDevice.DeviceFlags deviceFlags = (InputDevice.DeviceFlags)0)
		{
			InternedString layout = this.TryFindMatchingControlLayout(ref description, deviceId);
			if (!layout.IsEmpty())
			{
				InputDevice inputDevice = this.AddDevice(layout, deviceId, deviceName, description, deviceFlags, default(InternedString));
				inputDevice.m_Description = description;
				return inputDevice;
			}
			if (throwIfNoLayoutFound)
			{
				throw new ArgumentException(string.Format("Cannot find layout matching device description '{0}'", description), "description");
			}
			if (deviceId != 0)
			{
				DisableDeviceCommand command = DisableDeviceCommand.Create();
				this.m_Runtime.DeviceCommand(deviceId, ref command);
			}
			return null;
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00035FEC File Offset: 0x000341EC
		public InputDevice AddDevice(InputDeviceDescription description, InternedString layout, string deviceName = null, int deviceId = 0, InputDevice.DeviceFlags deviceFlags = (InputDevice.DeviceFlags)0)
		{
			InputDevice inputDevice2;
			try
			{
				InputDevice inputDevice = this.AddDevice(layout, deviceId, deviceName, description, deviceFlags, default(InternedString));
				inputDevice.m_Description = description;
				inputDevice2 = inputDevice;
			}
			finally
			{
			}
			return inputDevice2;
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0003602C File Offset: 0x0003422C
		public void RemoveDevice(InputDevice device, bool keepOnListOfAvailableDevices = false)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (device.m_DeviceIndex == -1)
			{
				return;
			}
			this.RemoveStateChangeMonitors(device);
			int deviceIndex = device.m_DeviceIndex;
			int deviceId = device.deviceId;
			if (deviceIndex < this.m_StateChangeMonitors.LengthSafe<InputManager.StateChangeMonitorsForDevice>())
			{
				int count = this.m_StateChangeMonitors.Length;
				this.m_StateChangeMonitors.EraseAtWithCapacity(ref count, deviceIndex);
			}
			this.m_Devices.EraseAtWithCapacity(ref this.m_DevicesCount, deviceIndex);
			this.m_DevicesById.Remove(deviceId);
			if (this.m_Devices != null)
			{
				this.ReallocateStateBuffers();
			}
			else
			{
				this.m_StateBuffers.FreeAll();
			}
			for (int i = deviceIndex; i < this.m_DevicesCount; i++)
			{
				this.m_Devices[i].m_DeviceIndex--;
			}
			device.m_DeviceIndex = -1;
			int j = 0;
			while (j < this.m_AvailableDeviceCount)
			{
				if (this.m_AvailableDevices[j].deviceId == deviceId)
				{
					if (keepOnListOfAvailableDevices)
					{
						this.m_AvailableDevices[j].isRemoved = true;
						break;
					}
					this.m_AvailableDevices.EraseAtWithCapacity(ref this.m_AvailableDeviceCount, j);
					break;
				}
				else
				{
					j++;
				}
			}
			device.BakeOffsetIntoStateBlockRecursive((uint)(-(uint)((ulong)device.m_StateBlock.byteOffset)));
			InputActionState.OnDeviceChange(device, InputDeviceChange.Removed);
			IInputUpdateCallbackReceiver beforeUpdateCallbackReceiver = device as IInputUpdateCallbackReceiver;
			if (beforeUpdateCallbackReceiver != null)
			{
				this.onBeforeUpdate -= beforeUpdateCallbackReceiver.OnUpdate;
			}
			if (device.updateBeforeRender)
			{
				bool haveDeviceRequiringBeforeRender = false;
				for (int k = 0; k < this.m_DevicesCount; k++)
				{
					if (this.m_Devices[k].updateBeforeRender)
					{
						haveDeviceRequiringBeforeRender = true;
						break;
					}
				}
				if (!haveDeviceRequiringBeforeRender)
				{
					this.updateMask &= ~InputUpdateType.BeforeRender;
				}
			}
			device.NotifyRemoved();
			DelegateHelpers.InvokeCallbacksSafe<InputDevice, InputDeviceChange>(ref this.m_DeviceChangeListeners, device, InputDeviceChange.Removed, InputManager.k_InputOnDeviceChangeMarker, "InputSystem.onDeviceChange", null);
			InputDevice device2 = InputSystem.GetDevice(device.GetType());
			if (device2 == null)
			{
				return;
			}
			device2.MakeCurrent();
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x00036201 File Offset: 0x00034401
		public void FlushDisconnectedDevices()
		{
			this.m_DisconnectedDevices.Clear(this.m_DisconnectedDevicesCount);
			this.m_DisconnectedDevicesCount = 0;
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0003621C File Offset: 0x0003441C
		public unsafe void ResetDevice(InputDevice device, bool alsoResetDontResetControls = false, bool? issueResetCommand = null)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (!device.added)
			{
				throw new InvalidOperationException(string.Format("Device '{0}' has not been added to the system", device));
			}
			bool isHardReset = alsoResetDontResetControls || !device.hasDontResetControls;
			InputDeviceChange change = (isHardReset ? InputDeviceChange.HardReset : InputDeviceChange.SoftReset);
			InputActionState.OnDeviceChange(device, change);
			DelegateHelpers.InvokeCallbacksSafe<InputDevice, InputDeviceChange>(ref this.m_DeviceChangeListeners, device, change, InputManager.k_InputOnDeviceChangeMarker, "InputSystem.onDeviceChange", null);
			if (!alsoResetDontResetControls)
			{
				ICustomDeviceReset customReset = device as ICustomDeviceReset;
				if (customReset != null)
				{
					customReset.Reset();
					goto IL_01BA;
				}
			}
			void* defaultStatePtr = device.defaultStatePtr;
			uint deviceStateBlockSize = device.stateBlock.alignedSizeInBytes;
			using (NativeArray<byte> tempBuffer = new NativeArray<byte>((int)(24U + deviceStateBlockSize), Allocator.Temp, NativeArrayOptions.ClearMemory))
			{
				StateEvent* stateEventPtr = (StateEvent*)tempBuffer.GetUnsafePtr<byte>();
				void* statePtr = stateEventPtr->state;
				double currentTime = this.m_Runtime.currentTime;
				ref InputStateBlock stateBlock = ref device.m_StateBlock;
				stateEventPtr->baseEvent.type = 1398030676;
				stateEventPtr->baseEvent.sizeInBytes = 24U + deviceStateBlockSize;
				stateEventPtr->baseEvent.time = currentTime;
				stateEventPtr->baseEvent.deviceId = device.deviceId;
				stateEventPtr->baseEvent.eventId = -1;
				stateEventPtr->stateFormat = device.m_StateBlock.format;
				if (isHardReset)
				{
					UnsafeUtility.MemCpy(statePtr, (void*)((byte*)defaultStatePtr + stateBlock.byteOffset), (long)((ulong)deviceStateBlockSize));
				}
				else
				{
					void* currentStatePtr = device.currentStatePtr;
					void* resetMaskPtr = this.m_StateBuffers.resetMaskBuffer;
					UnsafeUtility.MemCpy(statePtr, (void*)((byte*)currentStatePtr + stateBlock.byteOffset), (long)((ulong)deviceStateBlockSize));
					MemoryHelpers.MemCpyMasked(statePtr, (void*)((byte*)defaultStatePtr + stateBlock.byteOffset), (int)deviceStateBlockSize, (void*)((byte*)resetMaskPtr + stateBlock.byteOffset));
				}
				this.UpdateState(device, this.defaultUpdateType, statePtr, 0U, deviceStateBlockSize, currentTime, new InputEventPtr((InputEvent*)stateEventPtr));
			}
			IL_01BA:
			bool doIssueResetCommand = isHardReset;
			if (issueResetCommand != null)
			{
				doIssueResetCommand = issueResetCommand.Value;
			}
			if (doIssueResetCommand)
			{
				device.RequestReset();
			}
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0003641C File Offset: 0x0003461C
		public InputDevice TryGetDevice(string nameOrLayout)
		{
			if (string.IsNullOrEmpty(nameOrLayout))
			{
				throw new ArgumentException("Name is null or empty.", "nameOrLayout");
			}
			if (this.m_DevicesCount == 0)
			{
				return null;
			}
			string nameOrLayoutLowerCase = nameOrLayout.ToLower();
			for (int i = 0; i < this.m_DevicesCount; i++)
			{
				InputDevice device = this.m_Devices[i];
				if (device.m_Name.ToLower() == nameOrLayoutLowerCase || device.m_Layout.ToLower() == nameOrLayoutLowerCase)
				{
					return device;
				}
			}
			return null;
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x00036495 File Offset: 0x00034695
		public InputDevice GetDevice(string nameOrLayout)
		{
			InputDevice inputDevice = this.TryGetDevice(nameOrLayout);
			if (inputDevice == null)
			{
				throw new ArgumentException("Cannot find device with name or layout '" + nameOrLayout + "'", "nameOrLayout");
			}
			return inputDevice;
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x000364BC File Offset: 0x000346BC
		public InputDevice TryGetDevice(Type layoutType)
		{
			InternedString layoutName = this.m_Layouts.TryFindLayoutForType(layoutType);
			if (layoutName.IsEmpty())
			{
				return null;
			}
			return this.TryGetDevice(layoutName);
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x000364F0 File Offset: 0x000346F0
		public InputDevice TryGetDeviceById(int id)
		{
			InputDevice result;
			if (this.m_DevicesById.TryGetValue(id, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x00036510 File Offset: 0x00034710
		public int GetUnsupportedDevices(List<InputDeviceDescription> descriptions)
		{
			if (descriptions == null)
			{
				throw new ArgumentNullException("descriptions");
			}
			int numFound = 0;
			for (int i = 0; i < this.m_AvailableDeviceCount; i++)
			{
				if (this.TryGetDeviceById(this.m_AvailableDevices[i].deviceId) == null)
				{
					descriptions.Add(this.m_AvailableDevices[i].description);
					numFound++;
				}
			}
			return numFound;
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x00036574 File Offset: 0x00034774
		public void EnableOrDisableDevice(InputDevice device, bool enable, InputManager.DeviceDisableScope scope = InputManager.DeviceDisableScope.Everywhere)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (enable)
			{
				switch (scope)
				{
				case InputManager.DeviceDisableScope.Everywhere:
					device.disabledWhileInBackground = false;
					if (!device.disabledInFrontend && !device.disabledInRuntime)
					{
						return;
					}
					if (device.disabledInRuntime)
					{
						device.ExecuteEnableCommand();
						device.disabledInRuntime = false;
					}
					if (device.disabledInFrontend)
					{
						if (!device.RequestSync())
						{
							this.ResetDevice(device, false, null);
						}
						device.disabledInFrontend = false;
					}
					break;
				case InputManager.DeviceDisableScope.InFrontendOnly:
					device.disabledWhileInBackground = false;
					if (!device.disabledInFrontend && device.disabledInRuntime)
					{
						return;
					}
					if (!device.disabledInRuntime)
					{
						device.ExecuteDisableCommand();
						device.disabledInRuntime = true;
					}
					if (device.disabledInFrontend)
					{
						if (!device.RequestSync())
						{
							this.ResetDevice(device, false, null);
						}
						device.disabledInFrontend = false;
					}
					break;
				case InputManager.DeviceDisableScope.TemporaryWhilePlayerIsInBackground:
					if (device.disabledWhileInBackground)
					{
						if (device.disabledInRuntime)
						{
							device.ExecuteEnableCommand();
							device.disabledInRuntime = false;
						}
						if (!device.RequestSync())
						{
							this.ResetDevice(device, false, null);
						}
						device.disabledWhileInBackground = false;
					}
					break;
				}
			}
			else
			{
				switch (scope)
				{
				case InputManager.DeviceDisableScope.Everywhere:
					device.disabledWhileInBackground = false;
					if (device.disabledInFrontend && device.disabledInRuntime)
					{
						return;
					}
					if (!device.disabledInRuntime)
					{
						device.ExecuteDisableCommand();
						device.disabledInRuntime = true;
					}
					if (!device.disabledInFrontend)
					{
						this.ResetDevice(device, false, new bool?(false));
						device.disabledInFrontend = true;
					}
					break;
				case InputManager.DeviceDisableScope.InFrontendOnly:
					device.disabledWhileInBackground = false;
					if (!device.disabledInRuntime && device.disabledInFrontend)
					{
						return;
					}
					if (device.disabledInRuntime)
					{
						device.ExecuteEnableCommand();
						device.disabledInRuntime = false;
					}
					if (!device.disabledInFrontend)
					{
						this.ResetDevice(device, false, new bool?(false));
						device.disabledInFrontend = true;
					}
					break;
				case InputManager.DeviceDisableScope.TemporaryWhilePlayerIsInBackground:
					if (device.disabledInFrontend || device.disabledWhileInBackground)
					{
						return;
					}
					device.disabledWhileInBackground = true;
					this.ResetDevice(device, false, new bool?(false));
					device.ExecuteDisableCommand();
					device.disabledInRuntime = true;
					break;
				}
			}
			InputDeviceChange deviceChange = (enable ? InputDeviceChange.Enabled : InputDeviceChange.Disabled);
			DelegateHelpers.InvokeCallbacksSafe<InputDevice, InputDeviceChange>(ref this.m_DeviceChangeListeners, device, deviceChange, InputManager.k_InputOnDeviceChangeMarker, "InputSystem.onDeviceChange", null);
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x000367B9 File Offset: 0x000349B9
		private unsafe void QueueEvent(InputEvent* eventPtr)
		{
			if (this.m_InputEventStream.isOpen)
			{
				this.m_InputEventStream.Write(eventPtr);
				return;
			}
			this.m_Runtime.QueueEvent(eventPtr);
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x000367E1 File Offset: 0x000349E1
		public void QueueEvent(InputEventPtr ptr)
		{
			this.QueueEvent(ptr.data);
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x000367F0 File Offset: 0x000349F0
		public unsafe void QueueEvent<TEvent>(ref TEvent inputEvent) where TEvent : struct, IInputEventTypeInfo
		{
			this.QueueEvent((InputEvent*)UnsafeUtility.AddressOf<TEvent>(ref inputEvent));
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x000367FE File Offset: 0x000349FE
		public void Update()
		{
			this.Update(this.defaultUpdateType);
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0003680C File Offset: 0x00034A0C
		public void Update(InputUpdateType updateType)
		{
			this.m_Runtime.Update(updateType);
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0003681A File Offset: 0x00034A1A
		internal void Initialize(IInputRuntime runtime, InputSettings settings)
		{
			this.m_Settings = settings;
			this.InitializeActions();
			this.InitializeData();
			this.InstallRuntime(runtime);
			this.InstallGlobals();
			this.ApplySettings();
			this.ApplyActions();
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x00036848 File Offset: 0x00034A48
		internal void Destroy()
		{
			for (int i = 0; i < this.m_DevicesCount; i++)
			{
				this.m_Devices[i].NotifyRemoved();
			}
			this.m_StateBuffers.FreeAll();
			this.UninstallGlobals();
			if (this.m_Settings != null && this.m_Settings.hideFlags == HideFlags.HideAndDontSave)
			{
				Object.DestroyImmediate(this.m_Settings);
			}
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x000368AC File Offset: 0x00034AAC
		private void InitializeActions()
		{
			this.m_Actions = null;
			foreach (InputActionAsset candidate in Resources.FindObjectsOfTypeAll<InputActionAsset>())
			{
				if (candidate.m_IsProjectWide)
				{
					this.m_Actions = candidate;
					return;
				}
			}
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x000368E8 File Offset: 0x00034AE8
		internal void InitializeData()
		{
			this.m_Layouts.Allocate();
			this.m_Processors.Initialize();
			this.m_Interactions.Initialize();
			this.m_Composites.Initialize();
			this.m_DevicesById = new Dictionary<int, InputDevice>();
			this.m_UpdateMask = InputUpdateType.Dynamic | InputUpdateType.Fixed;
			this.m_HasFocus = Application.isFocused;
			this.m_ScrollDeltaBehavior = InputSettings.ScrollDeltaBehavior.UniformAcrossAllPlatforms;
			this.m_PollingFrequency = 60f;
			this.RegisterControlLayout("Axis", typeof(AxisControl));
			this.RegisterControlLayout("Button", typeof(ButtonControl));
			this.RegisterControlLayout("DiscreteButton", typeof(DiscreteButtonControl));
			this.RegisterControlLayout("Key", typeof(KeyControl));
			this.RegisterControlLayout("Analog", typeof(AxisControl));
			this.RegisterControlLayout("Integer", typeof(IntegerControl));
			this.RegisterControlLayout("Digital", typeof(IntegerControl));
			this.RegisterControlLayout("Double", typeof(DoubleControl));
			this.RegisterControlLayout("Vector2", typeof(Vector2Control));
			this.RegisterControlLayout("Vector3", typeof(Vector3Control));
			this.RegisterControlLayout("Delta", typeof(DeltaControl));
			this.RegisterControlLayout("Quaternion", typeof(QuaternionControl));
			this.RegisterControlLayout("Stick", typeof(StickControl));
			this.RegisterControlLayout("Dpad", typeof(DpadControl));
			this.RegisterControlLayout("DpadAxis", typeof(DpadControl.DpadAxisControl));
			this.RegisterControlLayout("AnyKey", typeof(AnyKeyControl));
			this.RegisterControlLayout("Touch", typeof(TouchControl));
			this.RegisterControlLayout("TouchPhase", typeof(TouchPhaseControl));
			this.RegisterControlLayout("TouchPress", typeof(TouchPressControl));
			this.RegisterControlLayout("Gamepad", typeof(Gamepad));
			this.RegisterControlLayout("Joystick", typeof(Joystick));
			this.RegisterControlLayout("Keyboard", typeof(Keyboard));
			this.RegisterControlLayout("Pointer", typeof(Pointer));
			this.RegisterControlLayout("Mouse", typeof(Mouse));
			this.RegisterControlLayout("Pen", typeof(Pen));
			this.RegisterControlLayout("Touchscreen", typeof(Touchscreen));
			this.RegisterControlLayout("Sensor", typeof(Sensor));
			this.RegisterControlLayout("Accelerometer", typeof(Accelerometer));
			this.RegisterControlLayout("Gyroscope", typeof(Gyroscope));
			this.RegisterControlLayout("GravitySensor", typeof(GravitySensor));
			this.RegisterControlLayout("AttitudeSensor", typeof(AttitudeSensor));
			this.RegisterControlLayout("LinearAccelerationSensor", typeof(LinearAccelerationSensor));
			this.RegisterControlLayout("MagneticFieldSensor", typeof(MagneticFieldSensor));
			this.RegisterControlLayout("LightSensor", typeof(LightSensor));
			this.RegisterControlLayout("PressureSensor", typeof(PressureSensor));
			this.RegisterControlLayout("HumiditySensor", typeof(HumiditySensor));
			this.RegisterControlLayout("AmbientTemperatureSensor", typeof(AmbientTemperatureSensor));
			this.RegisterControlLayout("StepCounter", typeof(StepCounter));
			this.RegisterControlLayout("TrackedDevice", typeof(TrackedDevice));
			this.RegisterPrecompiledLayout<FastKeyboard>(";AnyKey;Button;Axis;Key;DiscreteButton;Keyboard");
			this.RegisterPrecompiledLayout<FastTouchscreen>("AutoWindowSpace;Touch;Vector2;Delta;Analog;TouchPress;Button;Axis;Integer;TouchPhase;Double;Touchscreen;Pointer");
			this.RegisterPrecompiledLayout<FastMouse>("AutoWindowSpace;Vector2;Delta;Button;Axis;Digital;Integer;Mouse;Pointer");
			this.processors.AddTypeRegistration("Invert", typeof(InvertProcessor));
			this.processors.AddTypeRegistration("InvertVector2", typeof(InvertVector2Processor));
			this.processors.AddTypeRegistration("InvertVector3", typeof(InvertVector3Processor));
			this.processors.AddTypeRegistration("Clamp", typeof(ClampProcessor));
			this.processors.AddTypeRegistration("Normalize", typeof(NormalizeProcessor));
			this.processors.AddTypeRegistration("NormalizeVector2", typeof(NormalizeVector2Processor));
			this.processors.AddTypeRegistration("NormalizeVector3", typeof(NormalizeVector3Processor));
			this.processors.AddTypeRegistration("Scale", typeof(ScaleProcessor));
			this.processors.AddTypeRegistration("ScaleVector2", typeof(ScaleVector2Processor));
			this.processors.AddTypeRegistration("ScaleVector3", typeof(ScaleVector3Processor));
			this.processors.AddTypeRegistration("StickDeadzone", typeof(StickDeadzoneProcessor));
			this.processors.AddTypeRegistration("AxisDeadzone", typeof(AxisDeadzoneProcessor));
			this.processors.AddTypeRegistration("CompensateDirection", typeof(CompensateDirectionProcessor));
			this.processors.AddTypeRegistration("CompensateRotation", typeof(CompensateRotationProcessor));
			this.interactions.AddTypeRegistration("Hold", typeof(HoldInteraction));
			this.interactions.AddTypeRegistration("Tap", typeof(TapInteraction));
			this.interactions.AddTypeRegistration("SlowTap", typeof(SlowTapInteraction));
			this.interactions.AddTypeRegistration("MultiTap", typeof(MultiTapInteraction));
			this.interactions.AddTypeRegistration("Press", typeof(PressInteraction));
			this.composites.AddTypeRegistration("1DAxis", typeof(AxisComposite));
			this.composites.AddTypeRegistration("2DVector", typeof(Vector2Composite));
			this.composites.AddTypeRegistration("3DVector", typeof(Vector3Composite));
			this.composites.AddTypeRegistration("Axis", typeof(AxisComposite));
			this.composites.AddTypeRegistration("Dpad", typeof(Vector2Composite));
			this.composites.AddTypeRegistration("ButtonWithOneModifier", typeof(ButtonWithOneModifier));
			this.composites.AddTypeRegistration("ButtonWithTwoModifiers", typeof(ButtonWithTwoModifiers));
			this.composites.AddTypeRegistration("OneModifier", typeof(OneModifierComposite));
			this.composites.AddTypeRegistration("TwoModifiers", typeof(TwoModifiersComposite));
			this.RegisterCustomTypes();
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x00036FD8 File Offset: 0x000351D8
		private void RegisterCustomTypes(Type[] types)
		{
			foreach (Type type in types)
			{
				if (type.IsClass && !type.IsAbstract && !type.IsGenericType)
				{
					if (typeof(InputProcessor).IsAssignableFrom(type))
					{
						InputSystem.RegisterProcessor(type, null);
					}
					else if (typeof(IInputInteraction).IsAssignableFrom(type))
					{
						InputSystem.RegisterInteraction(type, null);
					}
					else if (typeof(InputBindingComposite).IsAssignableFrom(type))
					{
						InputSystem.RegisterBindingComposite(type, null);
					}
				}
			}
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x00037060 File Offset: 0x00035260
		private void RegisterCustomTypes()
		{
			Assembly inputSystemAssembly = typeof(InputProcessor).Assembly;
			string inputSystemName = inputSystemAssembly.GetName().Name;
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				try
				{
					if (!(assembly == inputSystemAssembly))
					{
						AssemblyName[] referencedAssemblies = assembly.GetReferencedAssemblies();
						for (int j = 0; j < referencedAssemblies.Length; j++)
						{
							if (referencedAssemblies[j].Name == inputSystemName)
							{
								this.RegisterCustomTypes(assembly.GetTypes());
								break;
							}
						}
					}
				}
				catch (ReflectionTypeLoadException)
				{
				}
			}
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x00037108 File Offset: 0x00035308
		internal void InstallRuntime(IInputRuntime runtime)
		{
			if (this.m_Runtime != null)
			{
				this.m_Runtime.onUpdate = null;
				this.m_Runtime.onBeforeUpdate = null;
				this.m_Runtime.onDeviceDiscovered = null;
				this.m_Runtime.onPlayerFocusChanged = null;
				this.m_Runtime.onShouldRunUpdate = null;
			}
			this.m_Runtime = runtime;
			this.m_Runtime.onUpdate = new InputUpdateDelegate(this.OnUpdate);
			this.m_Runtime.onDeviceDiscovered = new Action<int, string>(this.OnNativeDeviceDiscovered);
			this.m_Runtime.onPlayerFocusChanged = new Action<bool>(this.OnFocusChanged);
			this.m_Runtime.onShouldRunUpdate = new Func<InputUpdateType, bool>(this.ShouldRunUpdate);
			this.m_Runtime.pollingFrequency = this.pollingFrequency;
			this.m_HasFocus = this.m_Runtime.isPlayerFocused;
			if (this.m_BeforeUpdateListeners.length > 0 || this.m_HaveDevicesWithStateCallbackReceivers)
			{
				this.m_Runtime.onBeforeUpdate = new Action<InputUpdateType>(this.OnBeforeUpdate);
				this.m_NativeBeforeUpdateHooked = true;
			}
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x00037214 File Offset: 0x00035414
		internal void InstallGlobals()
		{
			InputControlLayout.s_Layouts = this.m_Layouts;
			InputProcessor.s_Processors = this.m_Processors;
			InputInteraction.s_Interactions = this.m_Interactions;
			InputBindingComposite.s_Composites = this.m_Composites;
			InputRuntime.s_Instance = this.m_Runtime;
			InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup = this.m_Runtime.currentTimeOffsetToRealtimeSinceStartup;
			InputUpdate.Restore(default(InputUpdate.SerializedState));
			InputStateBuffers.SwitchTo(this.m_StateBuffers, InputUpdateType.Dynamic);
			InputStateBuffers.s_DefaultStateBuffer = this.m_StateBuffers.defaultStateBuffer;
			InputStateBuffers.s_NoiseMaskBuffer = this.m_StateBuffers.noiseMaskBuffer;
			InputStateBuffers.s_ResetMaskBuffer = this.m_StateBuffers.resetMaskBuffer;
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x000372B4 File Offset: 0x000354B4
		internal void UninstallGlobals()
		{
			if (InputControlLayout.s_Layouts.baseLayoutTable == this.m_Layouts.baseLayoutTable)
			{
				InputControlLayout.s_Layouts = default(InputControlLayout.Collection);
			}
			if (InputProcessor.s_Processors.table == this.m_Processors.table)
			{
				InputProcessor.s_Processors = default(TypeTable);
			}
			if (InputInteraction.s_Interactions.table == this.m_Interactions.table)
			{
				InputInteraction.s_Interactions = default(TypeTable);
			}
			if (InputBindingComposite.s_Composites.table == this.m_Composites.table)
			{
				InputBindingComposite.s_Composites = default(TypeTable);
			}
			InputControlLayout.s_CacheInstance = default(InputControlLayout.Cache);
			InputControlLayout.s_CacheInstanceRef = 0;
			if (this.m_Runtime != null)
			{
				this.m_Runtime.onUpdate = null;
				this.m_Runtime.onDeviceDiscovered = null;
				this.m_Runtime.onBeforeUpdate = null;
				this.m_Runtime.onPlayerFocusChanged = null;
				this.m_Runtime.onShouldRunUpdate = null;
				if (InputRuntime.s_Instance == this.m_Runtime)
				{
					InputRuntime.s_Instance = null;
				}
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x000373B1 File Offset: 0x000355B1
		// (set) Token: 0x06000A8C RID: 2700 RVA: 0x000373B9 File Offset: 0x000355B9
		internal bool optimizedControlsFeatureEnabled
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_OptimizedControlsFeatureEnabled;
			}
			set
			{
				this.m_OptimizedControlsFeatureEnabled = value;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x000373C2 File Offset: 0x000355C2
		// (set) Token: 0x06000A8E RID: 2702 RVA: 0x000373CA File Offset: 0x000355CA
		internal bool readValueCachingFeatureEnabled
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_ReadValueCachingFeatureEnabled;
			}
			set
			{
				this.m_ReadValueCachingFeatureEnabled = value;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x000373D3 File Offset: 0x000355D3
		// (set) Token: 0x06000A90 RID: 2704 RVA: 0x000373DB File Offset: 0x000355DB
		internal bool paranoidReadValueCachingChecksEnabled
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_ParanoidReadValueCachingChecksEnabled;
			}
			set
			{
				this.m_ParanoidReadValueCachingChecksEnabled = value;
			}
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x000373E4 File Offset: 0x000355E4
		private void MakeDeviceNameUnique(InputDevice device)
		{
			if (this.m_DevicesCount == 0)
			{
				return;
			}
			string deviceName = StringHelpers.MakeUniqueName<InputDevice>(device.name, this.m_Devices, delegate(InputDevice x)
			{
				if (x == null)
				{
					return string.Empty;
				}
				return x.name;
			});
			if (deviceName != device.name)
			{
				InputManager.ResetControlPathsRecursive(device);
				device.m_Name = new InternedString(deviceName);
			}
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0003744C File Offset: 0x0003564C
		private static void ResetControlPathsRecursive(InputControl control)
		{
			control.m_Path = null;
			ReadOnlyArray<InputControl> children = control.children;
			int childCount = children.Count;
			for (int i = 0; i < childCount; i++)
			{
				InputManager.ResetControlPathsRecursive(children[i]);
			}
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x00037488 File Offset: 0x00035688
		private void AssignUniqueDeviceId(InputDevice device)
		{
			if (device.deviceId != 0)
			{
				InputDevice existingDeviceWithId = this.TryGetDeviceById(device.deviceId);
				if (existingDeviceWithId != null)
				{
					throw new InvalidOperationException(string.Format("Duplicate device ID {0} detected for devices '{1}' and '{2}'", device.deviceId, device.name, existingDeviceWithId.name));
				}
			}
			else
			{
				device.m_DeviceId = this.m_Runtime.AllocateDeviceId();
			}
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x000374E8 File Offset: 0x000356E8
		private void ReallocateStateBuffers()
		{
			InputStateBuffers oldBuffers = this.m_StateBuffers;
			InputStateBuffers newBuffers = default(InputStateBuffers);
			newBuffers.AllocateAll(this.m_Devices, this.m_DevicesCount);
			newBuffers.MigrateAll(this.m_Devices, this.m_DevicesCount, oldBuffers);
			oldBuffers.FreeAll();
			this.m_StateBuffers = newBuffers;
			InputStateBuffers.s_DefaultStateBuffer = newBuffers.defaultStateBuffer;
			InputStateBuffers.s_NoiseMaskBuffer = newBuffers.noiseMaskBuffer;
			InputStateBuffers.s_ResetMaskBuffer = newBuffers.resetMaskBuffer;
			InputStateBuffers.SwitchTo(this.m_StateBuffers, (InputUpdate.s_LatestUpdateType != InputUpdateType.None) ? InputUpdate.s_LatestUpdateType : this.defaultUpdateType);
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0003757C File Offset: 0x0003577C
		private unsafe void InitializeDefaultState(InputDevice device)
		{
			if (!device.hasControlsWithDefaultState)
			{
				return;
			}
			ReadOnlyArray<InputControl> controls = device.allControls;
			int controlCount = controls.Count;
			void* defaultStateBuffer = this.m_StateBuffers.defaultStateBuffer;
			for (int i = 0; i < controlCount; i++)
			{
				InputControl control = controls[i];
				if (control.hasDefaultState)
				{
					control.m_StateBlock.Write(defaultStateBuffer, control.m_DefaultState);
				}
			}
			InputStateBlock stateBlock = device.m_StateBlock;
			int deviceIndex = device.m_DeviceIndex;
			if (this.m_StateBuffers.m_PlayerStateBuffers.valid)
			{
				stateBlock.CopyToFrom(this.m_StateBuffers.m_PlayerStateBuffers.GetFrontBuffer(deviceIndex), defaultStateBuffer);
				stateBlock.CopyToFrom(this.m_StateBuffers.m_PlayerStateBuffers.GetBackBuffer(deviceIndex), defaultStateBuffer);
			}
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0003763C File Offset: 0x0003583C
		private unsafe void InitializeDeviceState(InputDevice device)
		{
			ReadOnlyArray<InputControl> controls = device.allControls;
			int controlCount = controls.Count;
			void* resetMaskBuffer = this.m_StateBuffers.resetMaskBuffer;
			bool haveControlsWithDefaultState = device.hasControlsWithDefaultState;
			void* noiseMaskBuffer = this.m_StateBuffers.noiseMaskBuffer;
			MemoryHelpers.SetBitsInBuffer(noiseMaskBuffer, (int)device.stateBlock.byteOffset, 0, (int)device.stateBlock.sizeInBits, false);
			MemoryHelpers.SetBitsInBuffer(resetMaskBuffer, (int)device.stateBlock.byteOffset, 0, (int)device.stateBlock.sizeInBits, true);
			void* defaultStateBuffer = this.m_StateBuffers.defaultStateBuffer;
			for (int i = 0; i < controlCount; i++)
			{
				InputControl control = controls[i];
				if (!control.usesStateFromOtherControl)
				{
					if (!control.noisy || control.dontReset)
					{
						ref InputStateBlock stateBlock = ref control.m_StateBlock;
						if (!control.noisy)
						{
							MemoryHelpers.SetBitsInBuffer(noiseMaskBuffer, (int)stateBlock.byteOffset, (int)stateBlock.bitOffset, (int)stateBlock.sizeInBits, true);
						}
						if (control.dontReset)
						{
							MemoryHelpers.SetBitsInBuffer(resetMaskBuffer, (int)stateBlock.byteOffset, (int)stateBlock.bitOffset, (int)stateBlock.sizeInBits, false);
						}
					}
					if (haveControlsWithDefaultState && control.hasDefaultState)
					{
						control.m_StateBlock.Write(defaultStateBuffer, control.m_DefaultState);
					}
				}
			}
			if (haveControlsWithDefaultState)
			{
				ref InputStateBlock deviceStateBlock = ref device.m_StateBlock;
				int deviceIndex = device.m_DeviceIndex;
				if (this.m_StateBuffers.m_PlayerStateBuffers.valid)
				{
					deviceStateBlock.CopyToFrom(this.m_StateBuffers.m_PlayerStateBuffers.GetFrontBuffer(deviceIndex), defaultStateBuffer);
					deviceStateBlock.CopyToFrom(this.m_StateBuffers.m_PlayerStateBuffers.GetBackBuffer(deviceIndex), defaultStateBuffer);
				}
			}
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x000377E4 File Offset: 0x000359E4
		private void OnNativeDeviceDiscovered(int deviceId, string deviceDescriptor)
		{
			this.RestoreDevicesAfterDomainReloadIfNecessary();
			InputDevice device = this.TryMatchDisconnectedDevice(deviceDescriptor);
			InputDeviceDescription description = ((device != null) ? device.description : InputDeviceDescription.FromJson(deviceDescriptor));
			bool markAsRemoved = false;
			try
			{
				if (this.m_Settings.supportedDevices.Count > 0)
				{
					InternedString layout = ((device != null) ? device.m_Layout : this.TryFindMatchingControlLayout(ref description, deviceId));
					if (!this.IsDeviceLayoutMarkedAsSupportedInSettings(layout))
					{
						markAsRemoved = true;
						return;
					}
				}
				if (device != null)
				{
					device.m_DeviceId = deviceId;
					device.m_DeviceFlags |= InputDevice.DeviceFlags.Native;
					device.m_DeviceFlags &= ~InputDevice.DeviceFlags.DisabledInFrontend;
					device.m_DeviceFlags &= ~InputDevice.DeviceFlags.DisabledWhileInBackground;
					device.m_DeviceFlags &= ~InputDevice.DeviceFlags.DisabledStateHasBeenQueriedFromRuntime;
					this.AddDevice(device);
					DelegateHelpers.InvokeCallbacksSafe<InputDevice, InputDeviceChange>(ref this.m_DeviceChangeListeners, device, InputDeviceChange.Reconnected, InputManager.k_InputOnDeviceChangeMarker, "InputSystem.onDeviceChange", null);
				}
				else
				{
					this.AddDevice(description, false, null, deviceId, InputDevice.DeviceFlags.Native);
				}
			}
			catch (Exception exception)
			{
				Debug.LogError(string.Format("Could not create a device for '{0}' (exception: {1})", description, exception));
			}
			finally
			{
				ArrayHelpers.AppendWithCapacity<InputManager.AvailableDevice>(ref this.m_AvailableDevices, ref this.m_AvailableDeviceCount, new InputManager.AvailableDevice
				{
					description = description,
					deviceId = deviceId,
					isNative = true,
					isRemoved = markAsRemoved
				}, 10);
			}
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x00037940 File Offset: 0x00035B40
		private JsonParser.JsonString MakeEscapedJsonString(string theString)
		{
			if (string.IsNullOrEmpty(theString))
			{
				return new JsonParser.JsonString
				{
					text = string.Empty,
					hasEscapes = false
				};
			}
			StringBuilder builder = new StringBuilder();
			int length = theString.Length;
			bool hasEscapes = false;
			for (int i = 0; i < length; i++)
			{
				char ch = theString[i];
				if (ch == '\\' || ch == '"')
				{
					builder.Append('\\');
					hasEscapes = true;
				}
				builder.Append(ch);
			}
			return new JsonParser.JsonString
			{
				text = builder.ToString(),
				hasEscapes = hasEscapes
			};
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x000379E4 File Offset: 0x00035BE4
		private InputDevice TryMatchDisconnectedDevice(string deviceDescriptor)
		{
			for (int i = 0; i < this.m_DisconnectedDevicesCount; i++)
			{
				InputDevice device = this.m_DisconnectedDevices[i];
				InputDeviceDescription description = device.description;
				if (InputDeviceDescription.ComparePropertyToDeviceDescriptor("interface", description.interfaceName, deviceDescriptor) && InputDeviceDescription.ComparePropertyToDeviceDescriptor("product", description.product, deviceDescriptor) && InputDeviceDescription.ComparePropertyToDeviceDescriptor("manufacturer", description.manufacturer, deviceDescriptor) && InputDeviceDescription.ComparePropertyToDeviceDescriptor("type", description.deviceClass, deviceDescriptor) && InputDeviceDescription.ComparePropertyToDeviceDescriptor("capabilities", this.MakeEscapedJsonString(description.capabilities), deviceDescriptor) && InputDeviceDescription.ComparePropertyToDeviceDescriptor("serial", description.serial, deviceDescriptor))
				{
					this.m_DisconnectedDevices.EraseAtWithCapacity(ref this.m_DisconnectedDevicesCount, i);
					return device;
				}
			}
			return null;
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x00037AC7 File Offset: 0x00035CC7
		private void InstallBeforeUpdateHookIfNecessary()
		{
			if (this.m_NativeBeforeUpdateHooked || this.m_Runtime == null)
			{
				return;
			}
			this.m_Runtime.onBeforeUpdate = new Action<InputUpdateType>(this.OnBeforeUpdate);
			this.m_NativeBeforeUpdateHooked = true;
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x000049FE File Offset: 0x00002BFE
		private void RestoreDevicesAfterDomainReloadIfNecessary()
		{
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x000049FE File Offset: 0x00002BFE
		private void WarnAboutDevicesFailingToRecreateAfterDomainReload()
		{
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x00037AF8 File Offset: 0x00035CF8
		private void OnBeforeUpdate(InputUpdateType updateType)
		{
			this.RestoreDevicesAfterDomainReloadIfNecessary();
			if ((updateType & this.m_UpdateMask) == InputUpdateType.None)
			{
				return;
			}
			InputStateBuffers.SwitchTo(this.m_StateBuffers, updateType);
			InputUpdate.OnBeforeUpdate(updateType);
			if (this.m_HaveDevicesWithStateCallbackReceivers && updateType != InputUpdateType.BeforeRender)
			{
				for (int i = 0; i < this.m_DevicesCount; i++)
				{
					InputDevice device = this.m_Devices[i];
					if (device.hasStateCallbacks)
					{
						((IInputStateCallbackReceiver)device).OnNextUpdate();
					}
				}
			}
			DelegateHelpers.InvokeCallbacksSafe(ref this.m_BeforeUpdateListeners, InputManager.k_InputOnBeforeUpdateMarker, "InputSystem.onBeforeUpdate", null);
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x00037B78 File Offset: 0x00035D78
		internal void ApplySettings()
		{
			InputUpdateType newUpdateMask = InputUpdateType.Editor;
			if ((this.m_UpdateMask & InputUpdateType.BeforeRender) != InputUpdateType.None)
			{
				newUpdateMask |= InputUpdateType.BeforeRender;
			}
			if (this.m_Settings.updateMode == (InputSettings.UpdateMode)0)
			{
				this.m_Settings.updateMode = InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;
			}
			switch (this.m_Settings.updateMode)
			{
			case InputSettings.UpdateMode.ProcessEventsInDynamicUpdate:
				newUpdateMask |= InputUpdateType.Dynamic;
				break;
			case InputSettings.UpdateMode.ProcessEventsInFixedUpdate:
				newUpdateMask |= InputUpdateType.Fixed;
				break;
			case InputSettings.UpdateMode.ProcessEventsManually:
				newUpdateMask |= InputUpdateType.Manual;
				break;
			default:
				throw new NotSupportedException("Invalid input update mode: " + this.m_Settings.updateMode.ToString());
			}
			this.updateMask = newUpdateMask;
			this.scrollDeltaBehavior = this.m_Settings.scrollDeltaBehavior;
			this.AddAvailableDevicesThatAreNowRecognized();
			if (this.settings.supportedDevices.Count > 0)
			{
				for (int i = 0; i < this.m_DevicesCount; i++)
				{
					InputDevice device = this.m_Devices[i];
					InternedString layout = device.m_Layout;
					bool isInAvailableDevices = false;
					for (int j = 0; j < this.m_AvailableDeviceCount; j++)
					{
						if (this.m_AvailableDevices[j].deviceId == device.deviceId)
						{
							isInAvailableDevices = true;
							break;
						}
					}
					if (isInAvailableDevices && !this.IsDeviceLayoutMarkedAsSupportedInSettings(layout))
					{
						this.RemoveDevice(device, true);
						i--;
					}
				}
			}
			if (this.m_Settings.m_FeatureFlags != null)
			{
				this.m_ReadValueCachingFeatureEnabled = this.m_Settings.IsFeatureEnabled("USE_READ_VALUE_CACHING");
				this.m_OptimizedControlsFeatureEnabled = this.m_Settings.IsFeatureEnabled("USE_OPTIMIZED_CONTROLS");
				this.m_ParanoidReadValueCachingChecksEnabled = this.m_Settings.IsFeatureEnabled("PARANOID_READ_VALUE_CACHING_CHECKS");
			}
			Touchscreen.s_TapTime = this.settings.defaultTapTime;
			Touchscreen.s_TapDelayTime = this.settings.multiTapDelayTime;
			Touchscreen.s_TapRadiusSquared = this.settings.tapRadius * this.settings.tapRadius;
			ButtonControl.s_GlobalDefaultButtonPressPoint = Mathf.Clamp(this.settings.defaultButtonPressPoint, 0.0001f, float.MaxValue);
			ButtonControl.s_GlobalDefaultButtonReleaseThreshold = this.settings.buttonReleaseThreshold;
			foreach (InputDevice inputDevice in this.devices)
			{
				inputDevice.SetOptimizedControlDataTypeRecursively();
			}
			foreach (InputDevice inputDevice2 in this.devices)
			{
				inputDevice2.MarkAsStaleRecursively();
			}
			DelegateHelpers.InvokeCallbacksSafe(ref this.m_SettingsChangedListeners, InputManager.k_InputOnSettingsChangeMarker, "InputSystem.onSettingsChange", null);
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x00037E1C File Offset: 0x0003601C
		internal void ApplyActions()
		{
			DelegateHelpers.InvokeCallbacksSafe(ref this.m_ActionsChangedListeners, InputManager.k_InputOnActionsChangeMarker, "InputSystem.onActionsChange", null);
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x00037E34 File Offset: 0x00036034
		internal unsafe long ExecuteGlobalCommand<TCommand>(ref TCommand command) where TCommand : struct, IInputDeviceCommandInfo
		{
			InputDeviceCommand* ptr = (InputDeviceCommand*)UnsafeUtility.AddressOf<TCommand>(ref command);
			return InputRuntime.s_Instance.DeviceCommand(0, ptr);
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x00037E54 File Offset: 0x00036054
		internal void AddAvailableDevicesThatAreNowRecognized()
		{
			for (int i = 0; i < this.m_AvailableDeviceCount; i++)
			{
				int id = this.m_AvailableDevices[i].deviceId;
				if (this.TryGetDeviceById(id) == null)
				{
					InternedString layout = this.TryFindMatchingControlLayout(ref this.m_AvailableDevices[i].description, id);
					if (this.IsDeviceLayoutMarkedAsSupportedInSettings(layout))
					{
						if (layout.IsEmpty())
						{
							if (id != 0)
							{
								DisableDeviceCommand command = DisableDeviceCommand.Create();
								this.m_Runtime.DeviceCommand(id, ref command);
							}
						}
						else
						{
							try
							{
								this.AddDevice(this.m_AvailableDevices[i].description, layout, null, id, this.m_AvailableDevices[i].isNative ? InputDevice.DeviceFlags.Native : ((InputDevice.DeviceFlags)0));
							}
							catch (Exception)
							{
							}
						}
					}
				}
			}
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00037F24 File Offset: 0x00036124
		private bool ShouldRunDeviceInBackground(InputDevice device)
		{
			return this.m_Settings.backgroundBehavior != InputSettings.BackgroundBehavior.ResetAndDisableAllDevices && device.canRunInBackground;
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x00037F3C File Offset: 0x0003613C
		internal void OnFocusChanged(bool focus)
		{
			bool runInBackground = this.m_Runtime.runInBackground;
			if (this.m_Settings.backgroundBehavior == InputSettings.BackgroundBehavior.IgnoreFocus && runInBackground)
			{
				this.m_HasFocus = focus;
				return;
			}
			if (!focus)
			{
				if (runInBackground)
				{
					for (int i = 0; i < this.m_DevicesCount; i++)
					{
						InputDevice device = this.m_Devices[i];
						if (device.enabled && !this.ShouldRunDeviceInBackground(device))
						{
							this.EnableOrDisableDevice(device, false, InputManager.DeviceDisableScope.TemporaryWhilePlayerIsInBackground);
							int index = this.m_Devices.IndexOfReference(device, this.m_DevicesCount);
							if (index == -1)
							{
								i--;
							}
							else
							{
								i = index;
							}
						}
					}
				}
			}
			else
			{
				for (int j = 0; j < this.m_DevicesCount; j++)
				{
					InputDevice device2 = this.m_Devices[j];
					if (device2.disabledWhileInBackground)
					{
						this.EnableOrDisableDevice(device2, true, InputManager.DeviceDisableScope.TemporaryWhilePlayerIsInBackground);
					}
					else if (device2.enabled && !runInBackground && !device2.RequestSync())
					{
						this.ResetDevice(device2, false, null);
					}
				}
			}
			this.m_HasFocus = focus;
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x00038034 File Offset: 0x00036234
		internal bool ShouldRunUpdate(InputUpdateType updateType)
		{
			if (updateType == InputUpdateType.None)
			{
				return true;
			}
			InputUpdateType mask = this.m_UpdateMask;
			return (updateType & mask) > InputUpdateType.None;
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x00038054 File Offset: 0x00036254
		private unsafe void OnUpdate(InputUpdateType updateType, ref InputEventBuffer eventBuffer)
		{
			if (this.m_InputEventStream.isOpen)
			{
				throw new InvalidOperationException("Already have an event buffer set! Was OnUpdate() called recursively?");
			}
			this.RestoreDevicesAfterDomainReloadIfNecessary();
			if ((updateType & this.m_UpdateMask) == InputUpdateType.None)
			{
				return;
			}
			this.WarnAboutDevicesFailingToRecreateAfterDomainReload();
			int num = this.m_Metrics.totalUpdateCount + 1;
			this.m_Metrics.totalUpdateCount = num;
			InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup = this.m_Runtime.currentTimeOffsetToRealtimeSinceStartup;
			InputStateBuffers.SwitchTo(this.m_StateBuffers, updateType);
			this.m_CurrentUpdate = updateType;
			InputUpdate.OnUpdate(updateType);
			bool shouldProcessActionTimeouts = updateType.IsPlayerUpdate() && this.gameIsPlaying;
			double currentTime = ((updateType == InputUpdateType.Fixed) ? this.m_Runtime.currentTimeForFixedUpdate : this.m_Runtime.currentTime);
			bool timesliceEvents = (updateType == InputUpdateType.Fixed || updateType == InputUpdateType.BeforeRender) && InputSystem.settings.updateMode == InputSettings.UpdateMode.ProcessEventsInFixedUpdate;
			bool canFlushBuffer = !this.gameHasFocus && !this.m_Runtime.runInBackground;
			if (eventBuffer.eventCount == 0 || canFlushBuffer)
			{
				if (shouldProcessActionTimeouts)
				{
					this.ProcessStateChangeMonitorTimeouts();
				}
				this.InvokeAfterUpdateCallback(updateType);
				if (canFlushBuffer)
				{
					eventBuffer.Reset();
				}
				this.m_CurrentUpdate = InputUpdateType.None;
				return;
			}
			long processingStartTime = Stopwatch.GetTimestamp();
			double totalEventLag = 0.0;
			try
			{
				this.m_InputEventStream = new InputEventStream(ref eventBuffer, this.m_Settings.maxQueuedEventsPerUpdate);
				uint totalEventBytesProcessed = 0U;
				InputEvent* skipEventMergingFor = null;
				while (this.m_InputEventStream.remainingEventCount > 0)
				{
					InputDevice device = null;
					InputEvent* currentEventReadPtr = this.m_InputEventStream.currentEventPtr;
					if (updateType == InputUpdateType.BeforeRender)
					{
						while (this.m_InputEventStream.remainingEventCount > 0)
						{
							device = this.TryGetDeviceById(currentEventReadPtr->deviceId);
							if (device != null && device.updateBeforeRender && (currentEventReadPtr->type == 1398030676 || currentEventReadPtr->type == 1145852993))
							{
								break;
							}
							currentEventReadPtr = this.m_InputEventStream.Advance(true);
						}
					}
					if (this.m_InputEventStream.remainingEventCount == 0)
					{
						break;
					}
					double currentEventTimeInternal = currentEventReadPtr->internalTime;
					FourCC currentEventType = currentEventReadPtr->type;
					if (timesliceEvents && currentEventTimeInternal >= currentTime)
					{
						this.m_InputEventStream.Advance(true);
					}
					else
					{
						if (device == null)
						{
							device = this.TryGetDeviceById(currentEventReadPtr->deviceId);
						}
						if (device == null)
						{
							this.m_InputEventStream.Advance(false);
						}
						else if (!device.enabled && currentEventType != 1146242381 && currentEventType != 1145259591 && (device.m_DeviceFlags & (InputDevice.DeviceFlags.DisabledInRuntime | InputDevice.DeviceFlags.DisabledWhileInBackground)) != (InputDevice.DeviceFlags)0)
						{
							this.m_InputEventStream.Advance(false);
						}
						else
						{
							if (!this.settings.disableRedundantEventsMerging && device.hasEventMerger && currentEventReadPtr != skipEventMergingFor)
							{
								InputEvent* nextEvent = this.m_InputEventStream.Peek();
								if (nextEvent != null && currentEventReadPtr->deviceId == nextEvent->deviceId && (!timesliceEvents || nextEvent->internalTime < currentTime))
								{
									if (((IEventMerger)device).MergeForward(currentEventReadPtr, nextEvent))
									{
										this.m_InputEventStream.Advance(false);
										continue;
									}
									skipEventMergingFor = nextEvent;
								}
							}
							if (device.hasEventPreProcessor && !((IEventPreProcessor)device).PreProcessEvent(currentEventReadPtr))
							{
								this.m_InputEventStream.Advance(false);
							}
							else
							{
								if (this.m_EventListeners.length > 0)
								{
									DelegateHelpers.InvokeCallbacksSafe<InputEventPtr, InputDevice>(ref this.m_EventListeners, new InputEventPtr(currentEventReadPtr), device, InputManager.k_InputOnEventMarker, "InputSystem.onEvent", null);
									if (currentEventReadPtr->handled)
									{
										this.m_InputEventStream.Advance(false);
										continue;
									}
								}
								if (currentEventTimeInternal <= currentTime)
								{
									totalEventLag += currentTime - currentEventTimeInternal;
								}
								num = this.m_Metrics.totalEventCount + 1;
								this.m_Metrics.totalEventCount = num;
								this.m_Metrics.totalEventBytes = this.m_Metrics.totalEventBytes + (int)currentEventReadPtr->sizeInBytes;
								num = currentEventType;
								if (num <= 1146242381)
								{
									if (num != 1145259591)
									{
										if (num == 1145852993)
										{
											goto IL_0418;
										}
										if (num == 1146242381)
										{
											this.RemoveDevice(device, false);
											if (device.native && !device.description.empty)
											{
												ArrayHelpers.AppendWithCapacity<InputDevice>(ref this.m_DisconnectedDevices, ref this.m_DisconnectedDevicesCount, device, 10);
												DelegateHelpers.InvokeCallbacksSafe<InputDevice, InputDeviceChange>(ref this.m_DeviceChangeListeners, device, InputDeviceChange.Disconnected, InputManager.k_InputOnDeviceChangeMarker, "InputSystem.onDeviceChange", null);
											}
										}
									}
									else
									{
										device.NotifyConfigurationChanged();
										InputActionState.OnDeviceChange(device, InputDeviceChange.ConfigurationChanged);
										DelegateHelpers.InvokeCallbacksSafe<InputDevice, InputDeviceChange>(ref this.m_DeviceChangeListeners, device, InputDeviceChange.ConfigurationChanged, InputManager.k_InputOnDeviceChangeMarker, "InputSystem.onDeviceChange", null);
									}
								}
								else if (num <= 1229800787)
								{
									if (num != 1146245972)
									{
										if (num == 1229800787)
										{
											IMECompositionEvent* imeEventPtr = (IMECompositionEvent*)currentEventReadPtr;
											ITextInputReceiver textInputReceiver2 = device as ITextInputReceiver;
											if (textInputReceiver2 != null)
											{
												textInputReceiver2.OnIMECompositionChanged(imeEventPtr->compositionString);
											}
										}
									}
									else
									{
										this.ResetDevice(device, ((DeviceResetEvent*)currentEventReadPtr)->hardReset, null);
									}
								}
								else
								{
									if (num == 1398030676)
									{
										goto IL_0418;
									}
									if (num == 1413830740)
									{
										TextEvent* textEventPtr = (TextEvent*)currentEventReadPtr;
										ITextInputReceiver textInputReceiver = device as ITextInputReceiver;
										if (textInputReceiver != null)
										{
											int utf32Char = textEventPtr->character;
											if (utf32Char >= 65536)
											{
												utf32Char -= 65536;
												int highSurrogate = 55296 + ((utf32Char >> 10) & 1023);
												int lowSurrogate = 56320 + (utf32Char & 1023);
												textInputReceiver.OnTextInput((char)highSurrogate);
												textInputReceiver.OnTextInput((char)lowSurrogate);
											}
											else
											{
												textInputReceiver.OnTextInput((char)utf32Char);
											}
										}
									}
								}
								IL_0643:
								this.m_InputEventStream.Advance(false);
								if (!this.AreMaximumEventBytesPerUpdateExceeded(totalEventBytesProcessed))
								{
									continue;
								}
								break;
								IL_0418:
								InputEventPtr eventPtr = new InputEventPtr(currentEventReadPtr);
								bool deviceIsStateCallbackReceiver = device.hasStateCallbacks;
								if (currentEventTimeInternal < device.m_LastUpdateTimeInternal && (!deviceIsStateCallbackReceiver || !(device.stateBlock.format != eventPtr.stateFormat)))
								{
									goto IL_0643;
								}
								bool haveChangedStateOtherThanNoise;
								if (deviceIsStateCallbackReceiver)
								{
									this.m_ShouldMakeCurrentlyUpdatingDeviceCurrent = true;
									((IInputStateCallbackReceiver)device).OnStateEvent(eventPtr);
									haveChangedStateOtherThanNoise = this.m_ShouldMakeCurrentlyUpdatingDeviceCurrent;
								}
								else
								{
									if (device.stateBlock.format != eventPtr.stateFormat)
									{
										goto IL_0643;
									}
									haveChangedStateOtherThanNoise = this.UpdateState(device, eventPtr, updateType);
								}
								totalEventBytesProcessed += eventPtr.sizeInBytes;
								device.m_CurrentProcessedEventBytesOnUpdate += eventPtr.sizeInBytes;
								if (device.m_LastUpdateTimeInternal <= eventPtr.internalTime)
								{
									device.m_LastUpdateTimeInternal = eventPtr.internalTime;
								}
								if (haveChangedStateOtherThanNoise)
								{
									device.MakeCurrent();
									goto IL_0643;
								}
								goto IL_0643;
							}
						}
					}
				}
				this.m_Metrics.totalEventProcessingTime = this.m_Metrics.totalEventProcessingTime + (double)(Stopwatch.GetTimestamp() - processingStartTime) / (double)Stopwatch.Frequency;
				this.m_Metrics.totalEventLagTime = this.m_Metrics.totalEventLagTime + totalEventLag;
				this.ResetCurrentProcessedEventBytesForDevices();
				this.m_InputEventStream.Close(ref eventBuffer);
			}
			catch (Exception)
			{
				this.m_InputEventStream.CleanUpAfterException();
				throw;
			}
			if (shouldProcessActionTimeouts)
			{
				this.ProcessStateChangeMonitorTimeouts();
			}
			this.InvokeAfterUpdateCallback(updateType);
			this.m_CurrentUpdate = InputUpdateType.None;
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x00038758 File Offset: 0x00036958
		private bool AreMaximumEventBytesPerUpdateExceeded(uint totalEventBytesProcessed)
		{
			if (this.m_Settings.maxEventBytesPerUpdate > 0 && (ulong)totalEventBytesProcessed >= (ulong)((long)this.m_Settings.maxEventBytesPerUpdate))
			{
				string eventsProcessedByDeviceLog = string.Empty;
				if (Debug.isDebugBuild)
				{
					eventsProcessedByDeviceLog = "Total events processed by devices in last update call:\n" + this.MakeStringWithEventsProcessedByDevice();
				}
				Debug.LogError("Exceeded budget for maximum input event throughput per InputSystem.Update(). Discarding remaining events. Increase InputSystem.settings.maxEventBytesPerUpdate or set it to 0 to remove the limit.\n" + eventsProcessedByDeviceLog);
				return true;
			}
			return false;
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x000387B4 File Offset: 0x000369B4
		private string MakeStringWithEventsProcessedByDevice()
		{
			StringBuilder eventsProcessedByDeviceLog = new StringBuilder();
			for (int i = 0; i < this.m_DevicesCount; i++)
			{
				InputDevice deviceToLog = this.devices[i];
				if (deviceToLog != null && deviceToLog.m_CurrentProcessedEventBytesOnUpdate > 0U)
				{
					eventsProcessedByDeviceLog.Append(string.Format(" - {0} bytes processed by {1}\n", deviceToLog.m_CurrentProcessedEventBytesOnUpdate, deviceToLog));
				}
			}
			return eventsProcessedByDeviceLog.ToString();
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x00038818 File Offset: 0x00036A18
		private void ResetCurrentProcessedEventBytesForDevices()
		{
			if (Debug.isDebugBuild)
			{
				for (int i = 0; i < this.m_DevicesCount; i++)
				{
					InputDevice device = this.m_Devices[i];
					if (device != null && device.m_CurrentProcessedEventBytesOnUpdate > 0U)
					{
						device.m_CurrentProcessedEventBytesOnUpdate = 0U;
					}
				}
			}
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0003885C File Offset: 0x00036A5C
		[Conditional("UNITY_EDITOR")]
		private void CheckAllDevicesOptimizedControlsHaveValidState()
		{
			if (!InputSystem.s_Manager.m_OptimizedControlsFeatureEnabled)
			{
				return;
			}
			foreach (InputDevice inputDevice in this.devices)
			{
			}
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x000388B8 File Offset: 0x00036AB8
		private void InvokeAfterUpdateCallback(InputUpdateType updateType)
		{
			if (updateType == InputUpdateType.Editor && this.gameIsPlaying)
			{
				return;
			}
			DelegateHelpers.InvokeCallbacksSafe(ref this.m_AfterUpdateListeners, InputManager.k_InputOnAfterUpdateMarker, "InputSystem.onAfterUpdate", null);
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x000388DD File Offset: 0x00036ADD
		internal void DontMakeCurrentlyUpdatingDeviceCurrent()
		{
			this.m_ShouldMakeCurrentlyUpdatingDeviceCurrent = false;
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x000388E8 File Offset: 0x00036AE8
		internal unsafe bool UpdateState(InputDevice device, InputEvent* eventPtr, InputUpdateType updateType)
		{
			InputStateBlock stateBlockOfDevice = device.m_StateBlock;
			uint stateBlockSizeOfDevice = stateBlockOfDevice.sizeInBits / 8U;
			uint offsetInDeviceStateToCopyTo = 0U;
			byte* ptrToReceivedState;
			uint sizeOfStateToCopy;
			if (eventPtr->type == 1398030676)
			{
				StateEvent stateEvent = *(StateEvent*)eventPtr;
				uint stateSizeInBytes = ((StateEvent*)eventPtr)->stateSizeInBytes;
				ptrToReceivedState = (byte*)((StateEvent*)eventPtr)->state;
				sizeOfStateToCopy = stateSizeInBytes;
				if (sizeOfStateToCopy > stateBlockSizeOfDevice)
				{
					sizeOfStateToCopy = stateBlockSizeOfDevice;
				}
			}
			else
			{
				DeltaStateEvent deltaStateEvent = *(DeltaStateEvent*)eventPtr;
				uint deltaStateSizeInBytes = ((DeltaStateEvent*)eventPtr)->deltaStateSizeInBytes;
				ptrToReceivedState = (byte*)((DeltaStateEvent*)eventPtr)->deltaState;
				offsetInDeviceStateToCopyTo = ((DeltaStateEvent*)eventPtr)->stateOffset;
				sizeOfStateToCopy = deltaStateSizeInBytes;
				if (offsetInDeviceStateToCopyTo + sizeOfStateToCopy > stateBlockSizeOfDevice)
				{
					if (offsetInDeviceStateToCopyTo >= stateBlockSizeOfDevice)
					{
						return false;
					}
					sizeOfStateToCopy = stateBlockSizeOfDevice - offsetInDeviceStateToCopyTo;
				}
			}
			return this.UpdateState(device, updateType, (void*)ptrToReceivedState, offsetInDeviceStateToCopyTo, sizeOfStateToCopy, eventPtr->internalTime, eventPtr);
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00038990 File Offset: 0x00036B90
		internal unsafe bool UpdateState(InputDevice device, InputUpdateType updateType, void* statePtr, uint stateOffsetInDevice, uint stateSize, double internalTime, InputEventPtr eventPtr = default(InputEventPtr))
		{
			int deviceIndex = device.m_DeviceIndex;
			ref InputStateBlock stateBlockOfDevice = ref device.m_StateBlock;
			byte* deviceBuffer = (byte*)InputStateBuffers.GetFrontBufferForDevice(deviceIndex);
			this.SortStateChangeMonitorsIfNecessary(deviceIndex);
			bool haveSignalledMonitors = this.ProcessStateChangeMonitors(deviceIndex, statePtr, (void*)(deviceBuffer + stateBlockOfDevice.byteOffset), stateSize, stateOffsetInDevice);
			uint deviceStateOffset = device.m_StateBlock.byteOffset + stateOffsetInDevice;
			void* ptr = (void*)(deviceBuffer + deviceStateOffset);
			byte* noiseMask = (device.noisy ? ((byte*)InputStateBuffers.s_NoiseMaskBuffer + deviceStateOffset) : null);
			bool makeDeviceCurrent = !MemoryHelpers.MemCmpBitRegion(ptr, statePtr, 0U, stateSize * 8U, (void*)noiseMask);
			bool flipped = this.FlipBuffersForDeviceIfNecessary(device, updateType);
			this.WriteStateChange(this.m_StateBuffers.m_PlayerStateBuffers, deviceIndex, ref stateBlockOfDevice, stateOffsetInDevice, statePtr, stateSize, flipped);
			if (makeDeviceCurrent)
			{
				if (InputSystem.s_Manager.m_ReadValueCachingFeatureEnabled || device.m_UseCachePathForButtonPresses)
				{
					using (HashSet<int>.Enumerator enumerator = device.m_UpdatedButtons.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							int button = enumerator.Current;
							((ButtonControl)device.allControls[button]).UpdateWasPressed();
						}
						goto IL_0145;
					}
				}
				int buttonCount = 0;
				foreach (ButtonControl buttonControl in device.m_ButtonControlsCheckingPressState)
				{
					buttonControl.UpdateWasPressed();
					buttonCount++;
				}
				if (buttonCount > 45)
				{
					device.m_UseCachePathForButtonPresses = true;
				}
			}
			IL_0145:
			DelegateHelpers.InvokeCallbacksSafe<InputDevice, InputEventPtr>(ref this.m_DeviceStateChangeListeners, device, eventPtr, InputManager.k_InputOnDeviceSettingsChangeMarker, "InputSystem.onDeviceStateChange", null);
			if (haveSignalledMonitors)
			{
				this.FireStateChangeNotifications(deviceIndex, internalTime, eventPtr);
			}
			return makeDeviceCurrent;
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x00038B2C File Offset: 0x00036D2C
		private unsafe void WriteStateChange(InputStateBuffers.DoubleBuffers buffers, int deviceIndex, ref InputStateBlock deviceStateBlock, uint stateOffsetInDevice, void* statePtr, uint stateSizeInBytes, bool flippedBuffers)
		{
			void* frontBuffer = buffers.GetFrontBuffer(deviceIndex);
			uint deviceStateSize = deviceStateBlock.sizeInBits / 8U;
			if (flippedBuffers && deviceStateSize != stateSizeInBytes)
			{
				void* backBuffer = buffers.GetBackBuffer(deviceIndex);
				UnsafeUtility.MemCpy((void*)((byte*)frontBuffer + deviceStateBlock.byteOffset), (void*)((byte*)backBuffer + deviceStateBlock.byteOffset), (long)((ulong)deviceStateSize));
			}
			if (InputSystem.s_Manager.m_ReadValueCachingFeatureEnabled || this.m_Devices[deviceIndex].m_UseCachePathForButtonPresses)
			{
				byte* buffer = (byte*)frontBuffer;
				if (flippedBuffers && deviceStateSize == stateSizeInBytes)
				{
					buffer = (byte*)buffers.GetBackBuffer(deviceIndex);
				}
				this.m_Devices[deviceIndex].WriteChangedControlStates(buffer + deviceStateBlock.byteOffset, statePtr, stateSizeInBytes, stateOffsetInDevice);
			}
			UnsafeUtility.MemCpy((void*)((byte*)((byte*)frontBuffer + deviceStateBlock.byteOffset) + stateOffsetInDevice), statePtr, (long)((ulong)stateSizeInBytes));
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00038BD8 File Offset: 0x00036DD8
		private bool FlipBuffersForDeviceIfNecessary(InputDevice device, InputUpdateType updateType)
		{
			if (updateType == InputUpdateType.BeforeRender)
			{
				return false;
			}
			if (device.m_CurrentUpdateStepCount != InputUpdate.s_UpdateStepCount)
			{
				this.m_StateBuffers.m_PlayerStateBuffers.SwapBuffers(device.m_DeviceIndex);
				device.m_CurrentUpdateStepCount = InputUpdate.s_UpdateStepCount;
				return true;
			}
			return false;
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x00038C14 File Offset: 0x00036E14
		public void AddStateChangeMonitor(InputControl control, IInputStateChangeMonitor monitor, long monitorIndex, uint groupIndex)
		{
			if (this.m_DevicesCount <= 0)
			{
				return;
			}
			int deviceIndex = control.device.m_DeviceIndex;
			if (this.m_StateChangeMonitors == null)
			{
				this.m_StateChangeMonitors = new InputManager.StateChangeMonitorsForDevice[this.m_DevicesCount];
			}
			else if (this.m_StateChangeMonitors.Length <= deviceIndex)
			{
				Array.Resize<InputManager.StateChangeMonitorsForDevice>(ref this.m_StateChangeMonitors, this.m_DevicesCount);
			}
			if (!this.isProcessingEvents && this.m_StateChangeMonitors[deviceIndex].needToCompactArrays)
			{
				this.m_StateChangeMonitors[deviceIndex].CompactArrays();
			}
			this.m_StateChangeMonitors[deviceIndex].Add(control, monitor, monitorIndex, groupIndex);
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00038CB0 File Offset: 0x00036EB0
		private void RemoveStateChangeMonitors(InputDevice device)
		{
			if (this.m_StateChangeMonitors == null)
			{
				return;
			}
			int deviceIndex = device.m_DeviceIndex;
			if (deviceIndex >= this.m_StateChangeMonitors.Length)
			{
				return;
			}
			this.m_StateChangeMonitors[deviceIndex].Clear();
			for (int i = 0; i < this.m_StateChangeMonitorTimeouts.length; i++)
			{
				InputControl control = this.m_StateChangeMonitorTimeouts[i].control;
				if (((control != null) ? control.device : null) == device)
				{
					this.m_StateChangeMonitorTimeouts[i] = default(InputManager.StateChangeMonitorTimeout);
				}
			}
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00038D38 File Offset: 0x00036F38
		public void RemoveStateChangeMonitor(InputControl control, IInputStateChangeMonitor monitor, long monitorIndex)
		{
			if (this.m_StateChangeMonitors == null)
			{
				return;
			}
			int deviceIndex = control.device.m_DeviceIndex;
			if (deviceIndex == -1)
			{
				return;
			}
			if (deviceIndex >= this.m_StateChangeMonitors.Length)
			{
				return;
			}
			this.m_StateChangeMonitors[deviceIndex].Remove(monitor, monitorIndex, this.isProcessingEvents);
			for (int i = 0; i < this.m_StateChangeMonitorTimeouts.length; i++)
			{
				if (this.m_StateChangeMonitorTimeouts[i].monitor == monitor && this.m_StateChangeMonitorTimeouts[i].monitorIndex == monitorIndex)
				{
					this.m_StateChangeMonitorTimeouts[i] = default(InputManager.StateChangeMonitorTimeout);
				}
			}
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00038DD8 File Offset: 0x00036FD8
		public void AddStateChangeMonitorTimeout(InputControl control, IInputStateChangeMonitor monitor, double time, long monitorIndex, int timerIndex)
		{
			this.m_StateChangeMonitorTimeouts.Append(new InputManager.StateChangeMonitorTimeout
			{
				control = control,
				time = time,
				monitor = monitor,
				monitorIndex = monitorIndex,
				timerIndex = timerIndex
			});
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00038E24 File Offset: 0x00037024
		public void RemoveStateChangeMonitorTimeout(IInputStateChangeMonitor monitor, long monitorIndex, int timerIndex)
		{
			int timeoutCount = this.m_StateChangeMonitorTimeouts.length;
			for (int i = 0; i < timeoutCount; i++)
			{
				if (this.m_StateChangeMonitorTimeouts[i].monitor == monitor && this.m_StateChangeMonitorTimeouts[i].monitorIndex == monitorIndex && this.m_StateChangeMonitorTimeouts[i].timerIndex == timerIndex)
				{
					this.m_StateChangeMonitorTimeouts[i] = default(InputManager.StateChangeMonitorTimeout);
					return;
				}
			}
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00038E9B File Offset: 0x0003709B
		private void SortStateChangeMonitorsIfNecessary(int deviceIndex)
		{
			if (this.m_StateChangeMonitors != null && deviceIndex < this.m_StateChangeMonitors.Length && this.m_StateChangeMonitors[deviceIndex].needToUpdateOrderingOfMonitors)
			{
				this.m_StateChangeMonitors[deviceIndex].SortMonitorsByIndex();
			}
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00038ED4 File Offset: 0x000370D4
		public void SignalStateChangeMonitor(InputControl control, IInputStateChangeMonitor monitor)
		{
			int deviceIndex = control.device.m_DeviceIndex;
			ref InputManager.StateChangeMonitorsForDevice monitorsForDevice = ref this.m_StateChangeMonitors[deviceIndex];
			for (int i = 0; i < monitorsForDevice.signalled.length; i++)
			{
				this.SortStateChangeMonitorsIfNecessary(i);
				ref InputManager.StateChangeMonitorListener listener = ref monitorsForDevice.listeners[i];
				if (listener.control == control && listener.monitor == monitor)
				{
					monitorsForDevice.signalled.SetBit(i);
				}
			}
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00038F44 File Offset: 0x00037144
		public void FireStateChangeNotifications()
		{
			double time = this.m_Runtime.currentTime;
			int count = Math.Min(this.m_StateChangeMonitors.LengthSafe<InputManager.StateChangeMonitorsForDevice>(), this.m_DevicesCount);
			for (int i = 0; i < count; i++)
			{
				this.FireStateChangeNotifications(i, time, null);
			}
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00038F8C File Offset: 0x0003718C
		private unsafe bool ProcessStateChangeMonitors(int deviceIndex, void* newStateFromEvent, void* oldStateOfDevice, uint newStateSizeInBytes, uint newStateOffsetInBytes)
		{
			if (this.m_StateChangeMonitors == null)
			{
				return false;
			}
			if (deviceIndex >= this.m_StateChangeMonitors.Length)
			{
				return false;
			}
			MemoryHelpers.BitRegion[] memoryRegions = this.m_StateChangeMonitors[deviceIndex].memoryRegions;
			if (memoryRegions == null)
			{
				return false;
			}
			int numMonitors = this.m_StateChangeMonitors[deviceIndex].count;
			bool signalled = false;
			DynamicBitfield signals = this.m_StateChangeMonitors[deviceIndex].signalled;
			bool haveChangedSignalsBitfield = false;
			MemoryHelpers.BitRegion newEventMemoryRegion = new MemoryHelpers.BitRegion(newStateOffsetInBytes, 0U, newStateSizeInBytes * 8U);
			for (int i = 0; i < numMonitors; i++)
			{
				MemoryHelpers.BitRegion memoryRegion = memoryRegions[i];
				if (memoryRegion.sizeInBits == 0U)
				{
					int listenerCount = numMonitors;
					int memoryRegionCount = numMonitors;
					this.m_StateChangeMonitors[deviceIndex].listeners.EraseAtWithCapacity(ref listenerCount, i);
					memoryRegions.EraseAtWithCapacity(ref memoryRegionCount, i);
					signals.SetLength(numMonitors - 1);
					haveChangedSignalsBitfield = true;
					numMonitors--;
					i--;
				}
				else
				{
					MemoryHelpers.BitRegion overlap = newEventMemoryRegion.Overlap(memoryRegion);
					if (!overlap.isEmpty && !MemoryHelpers.Compare(oldStateOfDevice, (void*)((byte*)newStateFromEvent - newStateOffsetInBytes), overlap))
					{
						signals.SetBit(i);
						haveChangedSignalsBitfield = true;
						signalled = true;
					}
				}
			}
			if (haveChangedSignalsBitfield)
			{
				this.m_StateChangeMonitors[deviceIndex].signalled = signals;
			}
			this.m_StateChangeMonitors[deviceIndex].needToCompactArrays = false;
			return signalled;
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x000390C4 File Offset: 0x000372C4
		internal unsafe void FireStateChangeNotifications(int deviceIndex, double internalTime, InputEvent* eventPtr)
		{
			if (this.m_StateChangeMonitors == null)
			{
				return;
			}
			if (this.m_StateChangeMonitors.Length <= deviceIndex)
			{
				return;
			}
			ref DynamicBitfield signals = ref this.m_StateChangeMonitors[deviceIndex].signalled;
			if (signals.AnyBitIsSet() && this.m_StateChangeMonitors[deviceIndex].listeners == null)
			{
				return;
			}
			ref InputManager.StateChangeMonitorListener[] listeners = ref this.m_StateChangeMonitors[deviceIndex].listeners;
			double time = internalTime - InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup;
			InputEvent tempEvent = new InputEvent(new FourCC('F', 'A', 'K', 'E'), 20, -1, internalTime);
			if (eventPtr == null)
			{
				eventPtr = (InputEvent*)UnsafeUtility.AddressOf<InputEvent>(ref tempEvent);
			}
			eventPtr->handled = false;
			for (int i = 0; i < signals.length; i++)
			{
				if (signals.TestBit(i))
				{
					InputManager.StateChangeMonitorListener listener = listeners[i];
					try
					{
						listener.monitor.NotifyControlStateChanged(listener.control, time, eventPtr, listener.monitorIndex);
					}
					catch (Exception exception)
					{
						Debug.LogError(string.Format("Exception '{0}' thrown from state change monitor '{1}' on '{2}'", exception.GetType().Name, listener.monitor.GetType().Name, listener.control));
						Debug.LogException(exception);
					}
					if (eventPtr->handled)
					{
						uint groupIndex = listeners[i].groupIndex;
						for (int j = i + 1; j < signals.length; j++)
						{
							if (listeners[j].groupIndex == groupIndex && listeners[j].monitor == listener.monitor)
							{
								signals.ClearBit(j);
							}
						}
						eventPtr->handled = false;
					}
					signals.ClearBit(i);
				}
			}
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00039270 File Offset: 0x00037470
		private void ProcessStateChangeMonitorTimeouts()
		{
			if (this.m_StateChangeMonitorTimeouts.length == 0)
			{
				return;
			}
			double currentTime = this.m_Runtime.currentTime - InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup;
			int remainingTimeoutCount = 0;
			for (int i = 0; i < this.m_StateChangeMonitorTimeouts.length; i++)
			{
				if (this.m_StateChangeMonitorTimeouts[i].control != null)
				{
					if (this.m_StateChangeMonitorTimeouts[i].time <= currentTime)
					{
						InputManager.StateChangeMonitorTimeout timeout = this.m_StateChangeMonitorTimeouts[i];
						timeout.monitor.NotifyTimerExpired(timeout.control, currentTime, timeout.monitorIndex, timeout.timerIndex);
					}
					else
					{
						if (i != remainingTimeoutCount)
						{
							this.m_StateChangeMonitorTimeouts[remainingTimeoutCount] = this.m_StateChangeMonitorTimeouts[i];
						}
						remainingTimeoutCount++;
					}
				}
			}
			this.m_StateChangeMonitorTimeouts.SetLength(remainingTimeoutCount);
		}

		// Token: 0x04000451 RID: 1105
		private static readonly ProfilerMarker k_InputUpdateProfilerMarker = new ProfilerMarker("InputUpdate");

		// Token: 0x04000452 RID: 1106
		private static readonly ProfilerMarker k_InputTryFindMatchingControllerMarker = new ProfilerMarker("InputSystem.TryFindMatchingControlLayout");

		// Token: 0x04000453 RID: 1107
		private static readonly ProfilerMarker k_InputAddDeviceMarker = new ProfilerMarker("InputSystem.AddDevice");

		// Token: 0x04000454 RID: 1108
		private static readonly ProfilerMarker k_InputRestoreDevicesAfterReloadMarker = new ProfilerMarker("InputManager.RestoreDevicesAfterDomainReload");

		// Token: 0x04000455 RID: 1109
		private static readonly ProfilerMarker k_InputRegisterCustomTypesMarker = new ProfilerMarker("InputManager.RegisterCustomTypes");

		// Token: 0x04000456 RID: 1110
		private static readonly ProfilerMarker k_InputOnBeforeUpdateMarker = new ProfilerMarker("InputSystem.onBeforeUpdate");

		// Token: 0x04000457 RID: 1111
		private static readonly ProfilerMarker k_InputOnAfterUpdateMarker = new ProfilerMarker("InputSystem.onAfterUpdate");

		// Token: 0x04000458 RID: 1112
		private static readonly ProfilerMarker k_InputOnSettingsChangeMarker = new ProfilerMarker("InputSystem.onSettingsChange");

		// Token: 0x04000459 RID: 1113
		private static readonly ProfilerMarker k_InputOnDeviceSettingsChangeMarker = new ProfilerMarker("InputSystem.onDeviceSettingsChange");

		// Token: 0x0400045A RID: 1114
		private static readonly ProfilerMarker k_InputOnEventMarker = new ProfilerMarker("InputSystem.onEvent");

		// Token: 0x0400045B RID: 1115
		private static readonly ProfilerMarker k_InputOnLayoutChangeMarker = new ProfilerMarker("InputSystem.onLayoutChange");

		// Token: 0x0400045C RID: 1116
		private static readonly ProfilerMarker k_InputOnDeviceChangeMarker = new ProfilerMarker("InpustSystem.onDeviceChange");

		// Token: 0x0400045D RID: 1117
		private static readonly ProfilerMarker k_InputOnActionsChangeMarker = new ProfilerMarker("InpustSystem.onActionsChange");

		// Token: 0x0400045E RID: 1118
		internal int m_LayoutRegistrationVersion;

		// Token: 0x0400045F RID: 1119
		private float m_PollingFrequency;

		// Token: 0x04000460 RID: 1120
		internal InputControlLayout.Collection m_Layouts;

		// Token: 0x04000461 RID: 1121
		private TypeTable m_Processors;

		// Token: 0x04000462 RID: 1122
		private TypeTable m_Interactions;

		// Token: 0x04000463 RID: 1123
		private TypeTable m_Composites;

		// Token: 0x04000464 RID: 1124
		private int m_DevicesCount;

		// Token: 0x04000465 RID: 1125
		private InputDevice[] m_Devices;

		// Token: 0x04000466 RID: 1126
		private Dictionary<int, InputDevice> m_DevicesById;

		// Token: 0x04000467 RID: 1127
		internal int m_AvailableDeviceCount;

		// Token: 0x04000468 RID: 1128
		internal InputManager.AvailableDevice[] m_AvailableDevices;

		// Token: 0x04000469 RID: 1129
		internal int m_DisconnectedDevicesCount;

		// Token: 0x0400046A RID: 1130
		internal InputDevice[] m_DisconnectedDevices;

		// Token: 0x0400046B RID: 1131
		internal InputUpdateType m_UpdateMask;

		// Token: 0x0400046C RID: 1132
		private InputUpdateType m_CurrentUpdate;

		// Token: 0x0400046D RID: 1133
		internal InputStateBuffers m_StateBuffers;

		// Token: 0x0400046E RID: 1134
		private InputSettings.ScrollDeltaBehavior m_ScrollDeltaBehavior;

		// Token: 0x0400046F RID: 1135
		private CallbackArray<Action<InputDevice, InputDeviceChange>> m_DeviceChangeListeners;

		// Token: 0x04000470 RID: 1136
		private CallbackArray<Action<InputDevice, InputEventPtr>> m_DeviceStateChangeListeners;

		// Token: 0x04000471 RID: 1137
		private CallbackArray<InputDeviceFindControlLayoutDelegate> m_DeviceFindLayoutCallbacks;

		// Token: 0x04000472 RID: 1138
		internal CallbackArray<InputDeviceCommandDelegate> m_DeviceCommandCallbacks;

		// Token: 0x04000473 RID: 1139
		private CallbackArray<Action<string, InputControlLayoutChange>> m_LayoutChangeListeners;

		// Token: 0x04000474 RID: 1140
		private CallbackArray<Action<InputEventPtr, InputDevice>> m_EventListeners;

		// Token: 0x04000475 RID: 1141
		private CallbackArray<Action> m_BeforeUpdateListeners;

		// Token: 0x04000476 RID: 1142
		private CallbackArray<Action> m_AfterUpdateListeners;

		// Token: 0x04000477 RID: 1143
		private CallbackArray<Action> m_SettingsChangedListeners;

		// Token: 0x04000478 RID: 1144
		private CallbackArray<Action> m_ActionsChangedListeners;

		// Token: 0x04000479 RID: 1145
		private bool m_NativeBeforeUpdateHooked;

		// Token: 0x0400047A RID: 1146
		private bool m_HaveDevicesWithStateCallbackReceivers;

		// Token: 0x0400047B RID: 1147
		private bool m_HasFocus;

		// Token: 0x0400047C RID: 1148
		private InputEventStream m_InputEventStream;

		// Token: 0x0400047D RID: 1149
		private InputDeviceExecuteCommandDelegate m_DeviceFindExecuteCommandDelegate;

		// Token: 0x0400047E RID: 1150
		private int m_DeviceFindExecuteCommandDeviceId;

		// Token: 0x0400047F RID: 1151
		internal IInputRuntime m_Runtime;

		// Token: 0x04000480 RID: 1152
		internal InputMetrics m_Metrics;

		// Token: 0x04000481 RID: 1153
		internal InputSettings m_Settings;

		// Token: 0x04000482 RID: 1154
		private bool m_OptimizedControlsFeatureEnabled;

		// Token: 0x04000483 RID: 1155
		private bool m_ReadValueCachingFeatureEnabled;

		// Token: 0x04000484 RID: 1156
		private bool m_ParanoidReadValueCachingChecksEnabled;

		// Token: 0x04000485 RID: 1157
		private InputActionAsset m_Actions;

		// Token: 0x04000486 RID: 1158
		private bool m_ShouldMakeCurrentlyUpdatingDeviceCurrent;

		// Token: 0x04000487 RID: 1159
		internal InputManager.StateChangeMonitorsForDevice[] m_StateChangeMonitors;

		// Token: 0x04000488 RID: 1160
		private InlinedArray<InputManager.StateChangeMonitorTimeout> m_StateChangeMonitorTimeouts;

		// Token: 0x020000C1 RID: 193
		internal enum DeviceDisableScope
		{
			// Token: 0x0400048A RID: 1162
			Everywhere,
			// Token: 0x0400048B RID: 1163
			InFrontendOnly,
			// Token: 0x0400048C RID: 1164
			TemporaryWhilePlayerIsInBackground
		}

		// Token: 0x020000C2 RID: 194
		[Serializable]
		internal struct AvailableDevice
		{
			// Token: 0x0400048D RID: 1165
			public InputDeviceDescription description;

			// Token: 0x0400048E RID: 1166
			public int deviceId;

			// Token: 0x0400048F RID: 1167
			public bool isNative;

			// Token: 0x04000490 RID: 1168
			public bool isRemoved;
		}

		// Token: 0x020000C3 RID: 195
		private struct StateChangeMonitorTimeout
		{
			// Token: 0x04000491 RID: 1169
			public InputControl control;

			// Token: 0x04000492 RID: 1170
			public double time;

			// Token: 0x04000493 RID: 1171
			public IInputStateChangeMonitor monitor;

			// Token: 0x04000494 RID: 1172
			public long monitorIndex;

			// Token: 0x04000495 RID: 1173
			public int timerIndex;
		}

		// Token: 0x020000C4 RID: 196
		internal struct StateChangeMonitorListener
		{
			// Token: 0x04000496 RID: 1174
			public InputControl control;

			// Token: 0x04000497 RID: 1175
			public IInputStateChangeMonitor monitor;

			// Token: 0x04000498 RID: 1176
			public long monitorIndex;

			// Token: 0x04000499 RID: 1177
			public uint groupIndex;
		}

		// Token: 0x020000C5 RID: 197
		internal struct StateChangeMonitorsForDevice
		{
			// Token: 0x170002B2 RID: 690
			// (get) Token: 0x06000ABE RID: 2750 RVA: 0x00039427 File Offset: 0x00037627
			public int count
			{
				get
				{
					return this.signalled.length;
				}
			}

			// Token: 0x06000ABF RID: 2751 RVA: 0x00039434 File Offset: 0x00037634
			public void Add(InputControl control, IInputStateChangeMonitor monitor, long monitorIndex, uint groupIndex)
			{
				int listenerCount = this.signalled.length;
				ArrayHelpers.AppendWithCapacity<InputManager.StateChangeMonitorListener>(ref this.listeners, ref listenerCount, new InputManager.StateChangeMonitorListener
				{
					monitor = monitor,
					monitorIndex = monitorIndex,
					groupIndex = groupIndex,
					control = control
				}, 10);
				ref InputStateBlock controlStateBlock = ref control.m_StateBlock;
				int memoryRegionCount = this.signalled.length;
				ArrayHelpers.AppendWithCapacity<MemoryHelpers.BitRegion>(ref this.memoryRegions, ref memoryRegionCount, new MemoryHelpers.BitRegion(controlStateBlock.byteOffset - control.device.stateBlock.byteOffset, controlStateBlock.bitOffset, controlStateBlock.sizeInBits), 10);
				this.signalled.SetLength(this.signalled.length + 1);
				this.needToUpdateOrderingOfMonitors = true;
			}

			// Token: 0x06000AC0 RID: 2752 RVA: 0x000394F8 File Offset: 0x000376F8
			public void Remove(IInputStateChangeMonitor monitor, long monitorIndex, bool deferRemoval)
			{
				if (this.listeners == null)
				{
					return;
				}
				int i = 0;
				while (i < this.signalled.length)
				{
					if (this.listeners[i].monitor == monitor && this.listeners[i].monitorIndex == monitorIndex)
					{
						if (deferRemoval)
						{
							this.listeners[i] = default(InputManager.StateChangeMonitorListener);
							this.memoryRegions[i] = default(MemoryHelpers.BitRegion);
							this.signalled.ClearBit(i);
							this.needToCompactArrays = true;
							return;
						}
						this.RemoveAt(i);
						return;
					}
					else
					{
						i++;
					}
				}
			}

			// Token: 0x06000AC1 RID: 2753 RVA: 0x0003958F File Offset: 0x0003778F
			public void Clear()
			{
				this.listeners.Clear(this.count);
				this.signalled.SetLength(0);
				this.needToCompactArrays = false;
			}

			// Token: 0x06000AC2 RID: 2754 RVA: 0x000395B8 File Offset: 0x000377B8
			public void CompactArrays()
			{
				for (int i = this.count - 1; i >= 0; i--)
				{
					if (this.memoryRegions[i].sizeInBits == 0U)
					{
						this.RemoveAt(i);
					}
				}
				this.needToCompactArrays = false;
			}

			// Token: 0x06000AC3 RID: 2755 RVA: 0x000395FC File Offset: 0x000377FC
			private void RemoveAt(int i)
			{
				int numListeners = this.count;
				int numMemoryRegions = this.count;
				this.listeners.EraseAtWithCapacity(ref numListeners, i);
				this.memoryRegions.EraseAtWithCapacity(ref numMemoryRegions, i);
				this.signalled.SetLength(this.count - 1);
			}

			// Token: 0x06000AC4 RID: 2756 RVA: 0x00039648 File Offset: 0x00037848
			public void SortMonitorsByIndex()
			{
				for (int i = 1; i < this.signalled.length; i++)
				{
					for (int j = i; j > 0; j--)
					{
						int complexityFromMonitorIndex = InputActionState.GetComplexityFromMonitorIndex(this.listeners[j - 1].monitorIndex);
						int secondComplexity = InputActionState.GetComplexityFromMonitorIndex(this.listeners[j].monitorIndex);
						if (complexityFromMonitorIndex >= secondComplexity)
						{
							break;
						}
						this.listeners.SwapElements(j, j - 1);
						this.memoryRegions.SwapElements(j, j - 1);
					}
				}
				this.needToUpdateOrderingOfMonitors = false;
			}

			// Token: 0x0400049A RID: 1178
			public MemoryHelpers.BitRegion[] memoryRegions;

			// Token: 0x0400049B RID: 1179
			public InputManager.StateChangeMonitorListener[] listeners;

			// Token: 0x0400049C RID: 1180
			public DynamicBitfield signalled;

			// Token: 0x0400049D RID: 1181
			public bool needToUpdateOrderingOfMonitors;

			// Token: 0x0400049E RID: 1182
			public bool needToCompactArrays;
		}
	}
}
