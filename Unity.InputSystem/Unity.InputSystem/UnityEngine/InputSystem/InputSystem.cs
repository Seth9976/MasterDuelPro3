using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Profiling;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.Haptics;
using UnityEngine.InputSystem.HID;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Switch;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem.XInput;
using UnityEngine.InputSystem.XR;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000068 RID: 104
	public static class InputSystem
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600048B RID: 1163 RVA: 0x00013314 File Offset: 0x00011514
		// (remove) Token: 0x0600048C RID: 1164 RVA: 0x00013358 File Offset: 0x00011558
		public static event Action<string, InputControlLayoutChange> onLayoutChange
		{
			add
			{
				InputManager inputManager = InputSystem.s_Manager;
				lock (inputManager)
				{
					InputSystem.s_Manager.onLayoutChange += value;
				}
			}
			remove
			{
				InputManager inputManager = InputSystem.s_Manager;
				lock (inputManager)
				{
					InputSystem.s_Manager.onLayoutChange -= value;
				}
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0001339C File Offset: 0x0001159C
		public static void RegisterLayout(Type type, string name = null, InputDeviceMatcher? matches = null)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (string.IsNullOrEmpty(name))
			{
				name = type.Name;
			}
			InputSystem.s_Manager.RegisterControlLayout(name, type);
			if (matches != null)
			{
				InputSystem.s_Manager.RegisterControlLayoutMatcher(name, matches.Value);
			}
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x000133F4 File Offset: 0x000115F4
		public static void RegisterLayout<T>(string name = null, InputDeviceMatcher? matches = null) where T : InputControl
		{
			InputSystem.RegisterLayout(typeof(T), name, matches);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00013407 File Offset: 0x00011607
		public static void RegisterLayout(string json, string name = null, InputDeviceMatcher? matches = null)
		{
			InputSystem.s_Manager.RegisterControlLayout(json, name, false);
			if (matches != null)
			{
				InputSystem.s_Manager.RegisterControlLayoutMatcher(name, matches.Value);
			}
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00013431 File Offset: 0x00011631
		public static void RegisterLayoutOverride(string json, string name = null)
		{
			InputSystem.s_Manager.RegisterControlLayout(json, name, true);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00013440 File Offset: 0x00011640
		public static void RegisterLayoutMatcher(string layoutName, InputDeviceMatcher matcher)
		{
			InputSystem.s_Manager.RegisterControlLayoutMatcher(layoutName, matcher);
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0001344E File Offset: 0x0001164E
		public static void RegisterLayoutMatcher<TDevice>(InputDeviceMatcher matcher) where TDevice : InputDevice
		{
			InputSystem.s_Manager.RegisterControlLayoutMatcher(typeof(TDevice), matcher);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00013468 File Offset: 0x00011668
		public static void RegisterLayoutBuilder(Func<InputControlLayout> buildMethod, string name, string baseLayout = null, InputDeviceMatcher? matches = null)
		{
			if (buildMethod == null)
			{
				throw new ArgumentNullException("buildMethod");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			InputSystem.s_Manager.RegisterControlLayoutBuilder(buildMethod, name, baseLayout);
			if (matches != null)
			{
				InputSystem.s_Manager.RegisterControlLayoutMatcher(name, matches.Value);
			}
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x000134BE File Offset: 0x000116BE
		public static void RegisterPrecompiledLayout<TDevice>(string metadata) where TDevice : InputDevice, new()
		{
			InputSystem.s_Manager.RegisterPrecompiledLayout<TDevice>(metadata);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x000134CB File Offset: 0x000116CB
		public static void RemoveLayout(string name)
		{
			InputSystem.s_Manager.RemoveControlLayout(name);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x000134D8 File Offset: 0x000116D8
		public static string TryFindMatchingLayout(InputDeviceDescription deviceDescription)
		{
			return InputSystem.s_Manager.TryFindMatchingControlLayout(ref deviceDescription, 0);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x000134EC File Offset: 0x000116EC
		public static IEnumerable<string> ListLayouts()
		{
			return InputSystem.s_Manager.ListControlLayouts(null);
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x000134F9 File Offset: 0x000116F9
		public static IEnumerable<string> ListLayoutsBasedOn(string baseLayout)
		{
			if (string.IsNullOrEmpty(baseLayout))
			{
				throw new ArgumentNullException("baseLayout");
			}
			return InputSystem.s_Manager.ListControlLayouts(baseLayout);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00013519 File Offset: 0x00011719
		public static InputControlLayout LoadLayout(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			return InputSystem.s_Manager.TryLoadControlLayout(new InternedString(name));
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0001353E File Offset: 0x0001173E
		public static InputControlLayout LoadLayout<TControl>() where TControl : InputControl
		{
			return InputSystem.s_Manager.TryLoadControlLayout(typeof(TControl));
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00013554 File Offset: 0x00011754
		public static string GetNameOfBaseLayout(string layoutName)
		{
			if (string.IsNullOrEmpty(layoutName))
			{
				throw new ArgumentNullException("layoutName");
			}
			InternedString internedLayoutName = new InternedString(layoutName);
			InternedString result;
			if (InputControlLayout.s_Layouts.baseLayoutTable.TryGetValue(internedLayoutName, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00013598 File Offset: 0x00011798
		public static bool IsFirstLayoutBasedOnSecond(string firstLayoutName, string secondLayoutName)
		{
			if (string.IsNullOrEmpty(firstLayoutName))
			{
				throw new ArgumentNullException("firstLayoutName");
			}
			if (string.IsNullOrEmpty(secondLayoutName))
			{
				throw new ArgumentNullException("secondLayoutName");
			}
			InternedString internedFirstName = new InternedString(firstLayoutName);
			InternedString internedSecondName = new InternedString(secondLayoutName);
			return internedFirstName == internedSecondName || InputControlLayout.s_Layouts.IsBasedOn(internedSecondName, internedFirstName);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x000135F4 File Offset: 0x000117F4
		public static void RegisterProcessor(Type type, string name = null)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (string.IsNullOrEmpty(name))
			{
				name = type.Name;
				if (name.EndsWith("Processor"))
				{
					name = name.Substring(0, name.Length - "Processor".Length);
				}
			}
			Dictionary<InternedString, InputControlLayout.Collection.PrecompiledLayout> precompiledLayouts = InputSystem.s_Manager.m_Layouts.precompiledLayouts;
			foreach (InternedString key in new List<InternedString>(precompiledLayouts.Keys))
			{
				if (StringHelpers.CharacterSeparatedListsHaveAtLeastOneCommonElement(precompiledLayouts[key].metadata, name, ';'))
				{
					InputSystem.s_Manager.m_Layouts.precompiledLayouts.Remove(key);
				}
			}
			InputSystem.s_Manager.processors.AddTypeRegistration(name, type);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x000136E0 File Offset: 0x000118E0
		public static void RegisterProcessor<T>(string name = null)
		{
			InputSystem.RegisterProcessor(typeof(T), name);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x000136F4 File Offset: 0x000118F4
		public static Type TryGetProcessor(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			return InputSystem.s_Manager.processors.LookupTypeRegistration(name);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00013728 File Offset: 0x00011928
		public static IEnumerable<string> ListProcessors()
		{
			return InputSystem.s_Manager.processors.names;
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x00013747 File Offset: 0x00011947
		public static ReadOnlyArray<InputDevice> devices
		{
			get
			{
				return InputSystem.s_Manager.devices;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x00013753 File Offset: 0x00011953
		public static ReadOnlyArray<InputDevice> disconnectedDevices
		{
			get
			{
				return new ReadOnlyArray<InputDevice>(InputSystem.s_Manager.m_DisconnectedDevices, 0, InputSystem.s_Manager.m_DisconnectedDevicesCount);
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060004A3 RID: 1187 RVA: 0x00013770 File Offset: 0x00011970
		// (remove) Token: 0x060004A4 RID: 1188 RVA: 0x000137C4 File Offset: 0x000119C4
		public static event Action<InputDevice, InputDeviceChange> onDeviceChange
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				InputManager inputManager = InputSystem.s_Manager;
				lock (inputManager)
				{
					InputSystem.s_Manager.onDeviceChange += value;
				}
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				InputManager inputManager = InputSystem.s_Manager;
				lock (inputManager)
				{
					InputSystem.s_Manager.onDeviceChange -= value;
				}
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060004A5 RID: 1189 RVA: 0x00013818 File Offset: 0x00011A18
		// (remove) Token: 0x060004A6 RID: 1190 RVA: 0x0001386C File Offset: 0x00011A6C
		public static event InputDeviceCommandDelegate onDeviceCommand
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				InputManager inputManager = InputSystem.s_Manager;
				lock (inputManager)
				{
					InputSystem.s_Manager.onDeviceCommand += value;
				}
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				InputManager inputManager = InputSystem.s_Manager;
				lock (inputManager)
				{
					InputSystem.s_Manager.onDeviceCommand -= value;
				}
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060004A7 RID: 1191 RVA: 0x000138C0 File Offset: 0x00011AC0
		// (remove) Token: 0x060004A8 RID: 1192 RVA: 0x00013904 File Offset: 0x00011B04
		public static event InputDeviceFindControlLayoutDelegate onFindLayoutForDevice
		{
			add
			{
				InputManager inputManager = InputSystem.s_Manager;
				lock (inputManager)
				{
					InputSystem.s_Manager.onFindControlLayoutForDevice += value;
				}
			}
			remove
			{
				InputManager inputManager = InputSystem.s_Manager;
				lock (inputManager)
				{
					InputSystem.s_Manager.onFindControlLayoutForDevice -= value;
				}
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x00013948 File Offset: 0x00011B48
		// (set) Token: 0x060004AA RID: 1194 RVA: 0x00013954 File Offset: 0x00011B54
		public static float pollingFrequency
		{
			get
			{
				return InputSystem.s_Manager.pollingFrequency;
			}
			set
			{
				InputSystem.s_Manager.pollingFrequency = value;
			}
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00013961 File Offset: 0x00011B61
		public static InputDevice AddDevice(string layout, string name = null, string variants = null)
		{
			if (string.IsNullOrEmpty(layout))
			{
				throw new ArgumentNullException("layout");
			}
			return InputSystem.s_Manager.AddDevice(layout, name, new InternedString(variants));
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00013988 File Offset: 0x00011B88
		public static TDevice AddDevice<TDevice>(string name = null) where TDevice : InputDevice
		{
			InputDevice device = InputSystem.s_Manager.AddDevice(typeof(TDevice), name);
			TDevice tdevice = device as TDevice;
			if (tdevice == null)
			{
				if (device != null)
				{
					InputSystem.RemoveDevice(device);
				}
				throw new InvalidOperationException("Layout registered for type '" + typeof(TDevice).Name + "' did not produce a device of that type; layout probably has been overridden");
			}
			return tdevice;
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x000139EB File Offset: 0x00011BEB
		public static InputDevice AddDevice(InputDeviceDescription description)
		{
			if (description.empty)
			{
				throw new ArgumentException("Description must not be empty", "description");
			}
			return InputSystem.s_Manager.AddDevice(description);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00013A11 File Offset: 0x00011C11
		public static void AddDevice(InputDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			InputSystem.s_Manager.AddDevice(device);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00013A2C File Offset: 0x00011C2C
		public static void RemoveDevice(InputDevice device)
		{
			InputSystem.s_Manager.RemoveDevice(device, false);
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00013A3A File Offset: 0x00011C3A
		public static void FlushDisconnectedDevices()
		{
			InputSystem.s_Manager.FlushDisconnectedDevices();
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00013A46 File Offset: 0x00011C46
		public static InputDevice GetDevice(string nameOrLayout)
		{
			return InputSystem.s_Manager.TryGetDevice(nameOrLayout);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00013A53 File Offset: 0x00011C53
		public static TDevice GetDevice<TDevice>() where TDevice : InputDevice
		{
			return (TDevice)((object)InputSystem.GetDevice(typeof(TDevice)));
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00013A6C File Offset: 0x00011C6C
		public static InputDevice GetDevice(Type type)
		{
			InputDevice result = null;
			double lastUpdateTime = -1.0;
			foreach (InputDevice device in InputSystem.devices)
			{
				if (type.IsInstanceOfType(device) && (result == null || device.m_LastUpdateTimeInternal > lastUpdateTime))
				{
					result = device;
					lastUpdateTime = result.m_LastUpdateTimeInternal;
				}
			}
			return result;
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00013AEC File Offset: 0x00011CEC
		public static TDevice GetDevice<TDevice>(InternedString usage) where TDevice : InputDevice
		{
			TDevice result = default(TDevice);
			double lastUpdateTime = -1.0;
			foreach (InputDevice inputDevice in InputSystem.devices)
			{
				TDevice deviceOfType = inputDevice as TDevice;
				if (deviceOfType != null && deviceOfType.usages.Contains(usage) && (result == null || deviceOfType.m_LastUpdateTimeInternal > lastUpdateTime))
				{
					result = deviceOfType;
					lastUpdateTime = result.m_LastUpdateTimeInternal;
				}
			}
			return result;
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00013B9C File Offset: 0x00011D9C
		public static TDevice GetDevice<TDevice>(string usage) where TDevice : InputDevice
		{
			return InputSystem.GetDevice<TDevice>(new InternedString(usage));
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00013BA9 File Offset: 0x00011DA9
		public static InputDevice GetDeviceById(int deviceId)
		{
			return InputSystem.s_Manager.TryGetDeviceById(deviceId);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00013BB6 File Offset: 0x00011DB6
		public static List<InputDeviceDescription> GetUnsupportedDevices()
		{
			List<InputDeviceDescription> list = new List<InputDeviceDescription>();
			InputSystem.GetUnsupportedDevices(list);
			return list;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00013BC4 File Offset: 0x00011DC4
		public static int GetUnsupportedDevices(List<InputDeviceDescription> descriptions)
		{
			return InputSystem.s_Manager.GetUnsupportedDevices(descriptions);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00013BD1 File Offset: 0x00011DD1
		public static void EnableDevice(InputDevice device)
		{
			InputSystem.s_Manager.EnableOrDisableDevice(device, true, InputManager.DeviceDisableScope.Everywhere);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00013BE0 File Offset: 0x00011DE0
		public static void DisableDevice(InputDevice device, bool keepSendingEvents = false)
		{
			InputSystem.s_Manager.EnableOrDisableDevice(device, false, keepSendingEvents ? InputManager.DeviceDisableScope.InFrontendOnly : InputManager.DeviceDisableScope.Everywhere);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00013BF5 File Offset: 0x00011DF5
		public static bool TrySyncDevice(InputDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (!device.added)
			{
				throw new InvalidOperationException(string.Format("Device '{0}' has not been added", device));
			}
			return device.RequestSync();
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00013C24 File Offset: 0x00011E24
		public static void ResetDevice(InputDevice device, bool alsoResetDontResetControls = false)
		{
			InputSystem.s_Manager.ResetDevice(device, alsoResetDontResetControls, null);
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00013C46 File Offset: 0x00011E46
		[Obsolete("Use 'ResetDevice' instead.", false)]
		public static bool TryResetDevice(InputDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			return device.RequestReset();
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00013C5C File Offset: 0x00011E5C
		public static void PauseHaptics()
		{
			ReadOnlyArray<InputDevice> devicesList = InputSystem.devices;
			int devicesCount = devicesList.Count;
			for (int i = 0; i < devicesCount; i++)
			{
				IHaptics haptics = devicesList[i] as IHaptics;
				if (haptics != null)
				{
					haptics.PauseHaptics();
				}
			}
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00013C9C File Offset: 0x00011E9C
		public static void ResumeHaptics()
		{
			ReadOnlyArray<InputDevice> devicesList = InputSystem.devices;
			int devicesCount = devicesList.Count;
			for (int i = 0; i < devicesCount; i++)
			{
				IHaptics haptics = devicesList[i] as IHaptics;
				if (haptics != null)
				{
					haptics.ResumeHaptics();
				}
			}
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00013CDC File Offset: 0x00011EDC
		public static void ResetHaptics()
		{
			ReadOnlyArray<InputDevice> devicesList = InputSystem.devices;
			int devicesCount = devicesList.Count;
			for (int i = 0; i < devicesCount; i++)
			{
				IHaptics haptics = devicesList[i] as IHaptics;
				if (haptics != null)
				{
					haptics.ResetHaptics();
				}
			}
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00013D1A File Offset: 0x00011F1A
		public static void SetDeviceUsage(InputDevice device, string usage)
		{
			InputSystem.SetDeviceUsage(device, new InternedString(usage));
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00013D28 File Offset: 0x00011F28
		public static void SetDeviceUsage(InputDevice device, InternedString usage)
		{
			InputSystem.s_Manager.SetDeviceUsage(device, usage);
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00013D36 File Offset: 0x00011F36
		public static void AddDeviceUsage(InputDevice device, string usage)
		{
			InputSystem.s_Manager.AddDeviceUsage(device, new InternedString(usage));
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00013D49 File Offset: 0x00011F49
		public static void AddDeviceUsage(InputDevice device, InternedString usage)
		{
			InputSystem.s_Manager.AddDeviceUsage(device, usage);
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00013D57 File Offset: 0x00011F57
		public static void RemoveDeviceUsage(InputDevice device, string usage)
		{
			InputSystem.s_Manager.RemoveDeviceUsage(device, new InternedString(usage));
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00013D6A File Offset: 0x00011F6A
		public static void RemoveDeviceUsage(InputDevice device, InternedString usage)
		{
			InputSystem.s_Manager.RemoveDeviceUsage(device, usage);
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00013D78 File Offset: 0x00011F78
		public static InputControl FindControl(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			ReadOnlyArray<InputDevice> devices = InputSystem.s_Manager.devices;
			int numDevices = devices.Count;
			for (int i = 0; i < numDevices; i++)
			{
				InputControl control = InputControlPath.TryFindControl(devices[i], path, 0);
				if (control != null)
				{
					return control;
				}
			}
			return null;
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00013DCD File Offset: 0x00011FCD
		public static InputControlList<InputControl> FindControls(string path)
		{
			return InputSystem.FindControls<InputControl>(path);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00013DD8 File Offset: 0x00011FD8
		public static InputControlList<TControl> FindControls<TControl>(string path) where TControl : InputControl
		{
			InputControlList<TControl> list = default(InputControlList<TControl>);
			InputSystem.FindControls<TControl>(path, ref list);
			return list;
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00013DF7 File Offset: 0x00011FF7
		public static int FindControls<TControl>(string path, ref InputControlList<TControl> controls) where TControl : InputControl
		{
			return InputSystem.s_Manager.GetControls<TControl>(path, ref controls);
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x00013E05 File Offset: 0x00012005
		internal static bool isProcessingEvents
		{
			get
			{
				return InputSystem.s_Manager.isProcessingEvents;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x00013E14 File Offset: 0x00012014
		// (set) Token: 0x060004CD RID: 1229 RVA: 0x000049FE File Offset: 0x00002BFE
		public static InputEventListener onEvent
		{
			get
			{
				return default(InputEventListener);
			}
			set
			{
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x00013E2C File Offset: 0x0001202C
		public static IObservable<InputControl> onAnyButtonPress
		{
			get
			{
				return from e in InputSystem.onEvent
					select e.GetFirstButtonPressOrNull(-1f, true) into c
					where c != null
					select c;
			}
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00013E8B File Offset: 0x0001208B
		public static void QueueEvent(InputEventPtr eventPtr)
		{
			if (!eventPtr.valid)
			{
				throw new ArgumentException("Received a null event pointer", "eventPtr");
			}
			InputSystem.s_Manager.QueueEvent(eventPtr);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00013EB1 File Offset: 0x000120B1
		public static void QueueEvent<TEvent>(ref TEvent inputEvent) where TEvent : struct, IInputEventTypeInfo
		{
			InputSystem.s_Manager.QueueEvent<TEvent>(ref inputEvent);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00013EC0 File Offset: 0x000120C0
		public unsafe static void QueueStateEvent<TState>(InputDevice device, TState state, double time = -1.0) where TState : struct, IInputStateTypeInfo
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (device.m_DeviceIndex == -1)
			{
				throw new InvalidOperationException(string.Format("Cannot queue state event for device '{0}' because device has not been added to system", device));
			}
			uint stateSize = (uint)UnsafeUtility.SizeOf<TState>();
			if (stateSize > 512U)
			{
				throw new ArgumentException(string.Format("Size of '{0}' exceeds maximum supported state size of {1}", typeof(TState).Name, 512), "state");
			}
			long eventSize = (long)UnsafeUtility.SizeOf<StateEvent>() + (long)((ulong)stateSize) - 1L;
			if (time < 0.0)
			{
				time = InputRuntime.s_Instance.currentTime;
			}
			else
			{
				time += InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup;
			}
			InputSystem.StateEventBuffer eventBuffer;
			eventBuffer.stateEvent = new StateEvent
			{
				baseEvent = new InputEvent(1398030676, (int)eventSize, device.deviceId, time),
				stateFormat = state.format
			};
			UnsafeUtility.MemCpy((void*)(&eventBuffer.stateEvent.stateData.FixedElementField), UnsafeUtility.AddressOf<TState>(ref state), (long)((ulong)stateSize));
			InputSystem.s_Manager.QueueEvent<StateEvent>(ref eventBuffer.stateEvent);
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00013FD8 File Offset: 0x000121D8
		public unsafe static void QueueDeltaStateEvent<TDelta>(InputControl control, TDelta delta, double time = -1.0) where TDelta : struct
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (control.stateBlock.bitOffset != 0U)
			{
				throw new InvalidOperationException(string.Format("Cannot send delta state events against bitfield controls: {0}", control));
			}
			InputDevice device = control.device;
			if (device.m_DeviceIndex == -1)
			{
				throw new InvalidOperationException(string.Format("Cannot queue state event for control '{0}' on device '{1}' because device has not been added to system", control, device));
			}
			if (time < 0.0)
			{
				time = InputRuntime.s_Instance.currentTime;
			}
			else
			{
				time += InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup;
			}
			uint deltaSize = (uint)UnsafeUtility.SizeOf<TDelta>();
			if (deltaSize > 512U)
			{
				throw new ArgumentException(string.Format("Size of state delta '{0}' exceeds maximum supported state size of {1}", typeof(TDelta).Name, 512), "delta");
			}
			if (deltaSize != control.stateBlock.alignedSizeInBytes)
			{
				throw new ArgumentException(string.Format("Size {0} of delta state of type {1} provided for control '{2}' does not match size {3} of control", new object[]
				{
					deltaSize,
					typeof(TDelta).Name,
					control,
					control.stateBlock.alignedSizeInBytes
				}), "delta");
			}
			long eventSize = (long)UnsafeUtility.SizeOf<DeltaStateEvent>() + (long)((ulong)deltaSize) - 1L;
			InputSystem.DeltaStateEventBuffer eventBuffer;
			eventBuffer.stateEvent = new DeltaStateEvent
			{
				baseEvent = new InputEvent(1145852993, (int)eventSize, device.deviceId, time),
				stateFormat = device.stateBlock.format,
				stateOffset = control.m_StateBlock.byteOffset - device.m_StateBlock.byteOffset
			};
			UnsafeUtility.MemCpy((void*)(&eventBuffer.stateEvent.stateData.FixedElementField), UnsafeUtility.AddressOf<TDelta>(ref delta), (long)((ulong)deltaSize));
			InputSystem.s_Manager.QueueEvent<DeltaStateEvent>(ref eventBuffer.stateEvent);
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0001419C File Offset: 0x0001239C
		public static void QueueConfigChangeEvent(InputDevice device, double time = -1.0)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (device.deviceId == 0)
			{
				throw new InvalidOperationException("Device has not been added");
			}
			if (time < 0.0)
			{
				time = InputRuntime.s_Instance.currentTime;
			}
			else
			{
				time += InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup;
			}
			DeviceConfigurationEvent inputEvent = DeviceConfigurationEvent.Create(device.deviceId, time);
			InputSystem.s_Manager.QueueEvent<DeviceConfigurationEvent>(ref inputEvent);
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00014208 File Offset: 0x00012408
		public static void QueueTextEvent(InputDevice device, char character, double time = -1.0)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (device.deviceId == 0)
			{
				throw new InvalidOperationException("Device has not been added");
			}
			if (time < 0.0)
			{
				time = InputRuntime.s_Instance.currentTime;
			}
			else
			{
				time += InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup;
			}
			TextEvent inputEvent = TextEvent.Create(device.deviceId, character, time);
			InputSystem.s_Manager.QueueEvent<TextEvent>(ref inputEvent);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00014273 File Offset: 0x00012473
		public static void Update()
		{
			InputSystem.s_Manager.Update();
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00014280 File Offset: 0x00012480
		internal static void Update(InputUpdateType updateType)
		{
			if (updateType != InputUpdateType.None && (InputSystem.s_Manager.updateMask & updateType) == InputUpdateType.None)
			{
				throw new InvalidOperationException(string.Format("'{0}' updates are not enabled; InputSystem.settings.updateMode is set to '{1}'", updateType, InputSystem.settings.updateMode));
			}
			InputSystem.s_Manager.Update(updateType);
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060004D7 RID: 1239 RVA: 0x000142D0 File Offset: 0x000124D0
		// (remove) Token: 0x060004D8 RID: 1240 RVA: 0x00014314 File Offset: 0x00012514
		public static event Action onBeforeUpdate
		{
			add
			{
				InputManager inputManager = InputSystem.s_Manager;
				lock (inputManager)
				{
					InputSystem.s_Manager.onBeforeUpdate += value;
				}
			}
			remove
			{
				InputManager inputManager = InputSystem.s_Manager;
				lock (inputManager)
				{
					InputSystem.s_Manager.onBeforeUpdate -= value;
				}
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060004D9 RID: 1241 RVA: 0x00014358 File Offset: 0x00012558
		// (remove) Token: 0x060004DA RID: 1242 RVA: 0x0001439C File Offset: 0x0001259C
		public static event Action onAfterUpdate
		{
			add
			{
				InputManager inputManager = InputSystem.s_Manager;
				lock (inputManager)
				{
					InputSystem.s_Manager.onAfterUpdate += value;
				}
			}
			remove
			{
				InputManager inputManager = InputSystem.s_Manager;
				lock (inputManager)
				{
					InputSystem.s_Manager.onAfterUpdate -= value;
				}
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x000143E0 File Offset: 0x000125E0
		// (set) Token: 0x060004DC RID: 1244 RVA: 0x000143EC File Offset: 0x000125EC
		public static InputSettings settings
		{
			get
			{
				return InputSystem.s_Manager.settings;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (InputSystem.s_Manager.m_Settings == value)
				{
					return;
				}
				InputSystem.s_Manager.settings = value;
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060004DD RID: 1245 RVA: 0x00014420 File Offset: 0x00012620
		// (remove) Token: 0x060004DE RID: 1246 RVA: 0x0001442D File Offset: 0x0001262D
		public static event Action onSettingsChange
		{
			add
			{
				InputSystem.s_Manager.onSettingsChange += value;
			}
			remove
			{
				InputSystem.s_Manager.onSettingsChange -= value;
			}
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0001443A File Offset: 0x0001263A
		private static void EnableActions()
		{
			if (InputSystem.actions == null)
			{
				return;
			}
			InputSystem.actions.Enable();
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00014454 File Offset: 0x00012654
		private static void DisableActions(bool triggerSetupChanged = false)
		{
			InputActionAsset projectWideActions = InputSystem.actions;
			if (projectWideActions == null)
			{
				return;
			}
			projectWideActions.Disable();
			if (triggerSetupChanged)
			{
				projectWideActions.OnSetupChanged();
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x00014480 File Offset: 0x00012680
		// (set) Token: 0x060004E2 RID: 1250 RVA: 0x00014492 File Offset: 0x00012692
		public static InputActionAsset actions
		{
			get
			{
				InputManager inputManager = InputSystem.s_Manager;
				if (inputManager == null)
				{
					return null;
				}
				return inputManager.actions;
			}
			set
			{
				if (Application.isPlaying)
				{
					throw new Exception("Attempted to set property InputSystem.actions during Play-mode which is not supported. Assigning this property is only allowed in Edit-mode.");
				}
				if (InputSystem.s_Manager.actions == value)
				{
					return;
				}
				value != null;
				InputSystem.s_Manager.actions = value;
			}
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060004E3 RID: 1251 RVA: 0x000144C7 File Offset: 0x000126C7
		// (remove) Token: 0x060004E4 RID: 1252 RVA: 0x000144D4 File Offset: 0x000126D4
		public static event Action onActionsChange
		{
			add
			{
				InputSystem.s_Manager.onActionsChange += value;
			}
			remove
			{
				InputSystem.s_Manager.onActionsChange -= value;
			}
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060004E5 RID: 1253 RVA: 0x000144E1 File Offset: 0x000126E1
		// (remove) Token: 0x060004E6 RID: 1254 RVA: 0x00014501 File Offset: 0x00012701
		public static event Action<object, InputActionChange> onActionChange
		{
			add
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				InputActionState.s_GlobalState.onActionChange.AddCallback(value);
			}
			remove
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				InputActionState.s_GlobalState.onActionChange.RemoveCallback(value);
			}
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00014524 File Offset: 0x00012724
		public static void RegisterInteraction(Type type, string name = null)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (string.IsNullOrEmpty(name))
			{
				name = type.Name;
				if (name.EndsWith("Interaction"))
				{
					name = name.Substring(0, name.Length - "Interaction".Length);
				}
			}
			InputSystem.s_Manager.interactions.AddTypeRegistration(name, type);
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00014590 File Offset: 0x00012790
		public static void RegisterInteraction<T>(string name = null)
		{
			InputSystem.RegisterInteraction(typeof(T), name);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x000145A4 File Offset: 0x000127A4
		public static Type TryGetInteraction(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			return InputSystem.s_Manager.interactions.LookupTypeRegistration(name);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x000145D8 File Offset: 0x000127D8
		public static IEnumerable<string> ListInteractions()
		{
			return InputSystem.s_Manager.interactions.names;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x000145F8 File Offset: 0x000127F8
		public static void RegisterBindingComposite(Type type, string name)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (string.IsNullOrEmpty(name))
			{
				name = type.Name;
				if (name.EndsWith("Composite"))
				{
					name = name.Substring(0, name.Length - "Composite".Length);
				}
			}
			InputSystem.s_Manager.composites.AddTypeRegistration(name, type);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00014664 File Offset: 0x00012864
		public static void RegisterBindingComposite<T>(string name = null)
		{
			InputSystem.RegisterBindingComposite(typeof(T), name);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00014678 File Offset: 0x00012878
		public static Type TryGetBindingComposite(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			return InputSystem.s_Manager.composites.LookupTypeRegistration(name);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x000146AB File Offset: 0x000128AB
		public static void DisableAllEnabledActions()
		{
			InputActionState.DisableAllActions();
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x000146B2 File Offset: 0x000128B2
		public static List<InputAction> ListEnabledActions()
		{
			List<InputAction> list = new List<InputAction>();
			InputSystem.ListEnabledActions(list);
			return list;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x000146C0 File Offset: 0x000128C0
		public static int ListEnabledActions(List<InputAction> actions)
		{
			if (actions == null)
			{
				throw new ArgumentNullException("actions");
			}
			return InputActionState.FindAllEnabledActions(actions);
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x000146D6 File Offset: 0x000128D6
		public static InputRemoting remoting
		{
			get
			{
				return InputSystem.s_Remote;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x000146DD File Offset: 0x000128DD
		public static Version version
		{
			get
			{
				return new Version("1.11.2");
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x000146E9 File Offset: 0x000128E9
		// (set) Token: 0x060004F4 RID: 1268 RVA: 0x000146FA File Offset: 0x000128FA
		public static bool runInBackground
		{
			get
			{
				return InputSystem.s_Manager.m_Runtime.runInBackground;
			}
			set
			{
				InputSystem.s_Manager.m_Runtime.runInBackground = value;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x0001470C File Offset: 0x0001290C
		internal static float scrollWheelDeltaPerTick
		{
			get
			{
				return InputRuntime.s_Instance.scrollWheelDeltaPerTick;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x00014718 File Offset: 0x00012918
		public static InputMetrics metrics
		{
			get
			{
				return InputSystem.s_Manager.metrics;
			}
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00014724 File Offset: 0x00012924
		static InputSystem()
		{
			InputSystem.InitializeInPlayer(null, null);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0001473C File Offset: 0x0001293C
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void RunInitializeInPlayer()
		{
			if (InputSystem.s_Manager == null)
			{
				InputSystem.InitializeInPlayer(null, null);
			}
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x000049FE File Offset: 0x00002BFE
		internal static void EnsureInitialized()
		{
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0001474C File Offset: 0x0001294C
		private static void InitializeInPlayer(IInputRuntime runtime = null, InputSettings settings = null)
		{
			if (settings == null)
			{
				settings = Resources.FindObjectsOfTypeAll<InputSettings>().FirstOrDefault<InputSettings>() ?? ScriptableObject.CreateInstance<InputSettings>();
			}
			InputSystem.s_Manager = new InputManager();
			InputSystem.s_Manager.Initialize(runtime ?? NativeInputRuntime.instance, settings);
			InputSystem.PerformDefaultPluginInitialization();
			InputSystem.EnableActions();
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x000147A0 File Offset: 0x000129A0
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void RunInitialUpdate()
		{
			InputSystem.Update(InputUpdateType.None);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x000147A8 File Offset: 0x000129A8
		private static void PerformDefaultPluginInitialization()
		{
			UISupport.Initialize();
			XInputSupport.Initialize();
			DualShockSupport.Initialize();
			HIDSupport.Initialize();
			SwitchSupportHID.Initialize();
			XRSupport.Initialize();
		}

		// Token: 0x04000246 RID: 582
		internal const string kAssemblyVersion = "1.11.2";

		// Token: 0x04000247 RID: 583
		internal const string kDocUrl = "https://docs.unity3d.com/Packages/com.unity.inputsystem@1.11";

		// Token: 0x04000248 RID: 584
		private static readonly ProfilerMarker k_InputResetMarker = new ProfilerMarker("InputSystem.Reset");

		// Token: 0x04000249 RID: 585
		internal static InputManager s_Manager;

		// Token: 0x0400024A RID: 586
		internal static InputRemoting s_Remote;

		// Token: 0x02000069 RID: 105
		private struct StateEventBuffer
		{
			// Token: 0x0400024B RID: 587
			public StateEvent stateEvent;

			// Token: 0x0400024C RID: 588
			public const int kMaxSize = 512;

			// Token: 0x0400024D RID: 589
			[FixedBuffer(typeof(byte), 511)]
			public InputSystem.StateEventBuffer.<data>e__FixedBuffer data;

			// Token: 0x0200006A RID: 106
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 511)]
			public struct <data>e__FixedBuffer
			{
				// Token: 0x0400024E RID: 590
				public byte FixedElementField;
			}
		}

		// Token: 0x0200006B RID: 107
		private struct DeltaStateEventBuffer
		{
			// Token: 0x0400024F RID: 591
			public DeltaStateEvent stateEvent;

			// Token: 0x04000250 RID: 592
			public const int kMaxSize = 512;

			// Token: 0x04000251 RID: 593
			[FixedBuffer(typeof(byte), 511)]
			public InputSystem.DeltaStateEventBuffer.<data>e__FixedBuffer data;

			// Token: 0x0200006C RID: 108
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 511)]
			public struct <data>e__FixedBuffer
			{
				// Token: 0x04000252 RID: 594
				public byte FixedElementField;
			}
		}
	}
}
