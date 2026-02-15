using System;
using Unity.Collections;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.OnScreen
{
	// Token: 0x02000132 RID: 306
	public abstract class OnScreenControl : MonoBehaviour
	{
		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x00047296 File Offset: 0x00045496
		// (set) Token: 0x06000E20 RID: 3616 RVA: 0x0004729E File Offset: 0x0004549E
		public string controlPath
		{
			get
			{
				return this.controlPathInternal;
			}
			set
			{
				this.controlPathInternal = value;
				if (base.isActiveAndEnabled)
				{
					this.SetupInputControl();
				}
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000E21 RID: 3617 RVA: 0x000472B5 File Offset: 0x000454B5
		public InputControl control
		{
			get
			{
				return this.m_Control;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000E22 RID: 3618
		// (set) Token: 0x06000E23 RID: 3619
		protected abstract string controlPathInternal { get; set; }

		// Token: 0x06000E24 RID: 3620 RVA: 0x000472C0 File Offset: 0x000454C0
		private void SetupInputControl()
		{
			string path = this.controlPathInternal;
			if (string.IsNullOrEmpty(path))
			{
				return;
			}
			string layoutName = InputControlPath.TryGetDeviceLayout(path);
			if (layoutName == null)
			{
				Debug.LogError(string.Concat(new string[]
				{
					"Cannot determine device layout to use based on control path '",
					path,
					"' used in ",
					base.GetType().Name,
					" component"
				}), this);
				return;
			}
			InternedString internedLayoutName = new InternedString(layoutName);
			int deviceInfoIndex = -1;
			for (int i = 0; i < OnScreenControl.s_OnScreenDevices.length; i++)
			{
				if (OnScreenControl.s_OnScreenDevices[i].device.m_Layout == internedLayoutName)
				{
					deviceInfoIndex = i;
					break;
				}
			}
			InputDevice device;
			if (deviceInfoIndex == -1)
			{
				try
				{
					device = InputSystem.AddDevice(layoutName, null, null);
				}
				catch (Exception exception)
				{
					Debug.LogError(string.Concat(new string[]
					{
						"Could not create device with layout '",
						layoutName,
						"' used in '",
						base.GetType().Name,
						"' component"
					}));
					Debug.LogException(exception);
					return;
				}
				InputSystem.AddDeviceUsage(device, "OnScreen");
				InputEventPtr eventPtr;
				NativeArray<byte> buffer = StateEvent.From(device, out eventPtr, Allocator.Persistent);
				deviceInfoIndex = OnScreenControl.s_OnScreenDevices.Append(new OnScreenControl.OnScreenDeviceInfo
				{
					eventPtr = eventPtr,
					buffer = buffer,
					device = device
				});
			}
			else
			{
				device = OnScreenControl.s_OnScreenDevices[deviceInfoIndex].device;
			}
			this.m_Control = InputControlPath.TryFindControl(device, path, 0);
			if (this.m_Control == null)
			{
				Debug.LogError(string.Concat(new string[]
				{
					"Cannot find control with path '",
					path,
					"' on device of type '",
					layoutName,
					"' referenced by component '",
					base.GetType().Name,
					"'"
				}), this);
				if (OnScreenControl.s_OnScreenDevices[deviceInfoIndex].firstControl == null)
				{
					OnScreenControl.s_OnScreenDevices[deviceInfoIndex].Destroy();
					OnScreenControl.s_OnScreenDevices.RemoveAt(deviceInfoIndex);
				}
				return;
			}
			this.m_InputEventPtr = OnScreenControl.s_OnScreenDevices[deviceInfoIndex].eventPtr;
			OnScreenControl.s_OnScreenDevices[deviceInfoIndex] = OnScreenControl.s_OnScreenDevices[deviceInfoIndex].AddControl(this);
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x000474FC File Offset: 0x000456FC
		protected void SendValueToControl<TValue>(TValue value) where TValue : struct
		{
			if (this.m_Control == null)
			{
				return;
			}
			InputControl<TValue> control = this.m_Control as InputControl<TValue>;
			if (control == null)
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"The control path ",
					this.controlPath,
					" yields a control of type ",
					this.m_Control.GetType().Name,
					" which is not an InputControl with value type ",
					typeof(TValue).Name
				}), "value");
			}
			this.m_InputEventPtr.internalTime = InputRuntime.s_Instance.currentTime;
			control.WriteValueIntoEvent(value, this.m_InputEventPtr);
			InputSystem.QueueEvent(this.m_InputEventPtr);
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x000475AA File Offset: 0x000457AA
		protected void SentDefaultValueToControl()
		{
			if (this.m_Control == null)
			{
				return;
			}
			this.m_InputEventPtr.internalTime = InputRuntime.s_Instance.currentTime;
			this.m_Control.ResetToDefaultStateInEvent(this.m_InputEventPtr);
			InputSystem.QueueEvent(this.m_InputEventPtr);
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x000475E7 File Offset: 0x000457E7
		protected virtual void OnEnable()
		{
			this.SetupInputControl();
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x000475F0 File Offset: 0x000457F0
		protected virtual void OnDisable()
		{
			if (this.m_Control == null)
			{
				return;
			}
			InputDevice device = this.m_Control.device;
			for (int i = 0; i < OnScreenControl.s_OnScreenDevices.length; i++)
			{
				if (OnScreenControl.s_OnScreenDevices[i].device == device)
				{
					OnScreenControl.OnScreenDeviceInfo deviceInfo = OnScreenControl.s_OnScreenDevices[i].RemoveControl(this);
					if (deviceInfo.firstControl == null)
					{
						OnScreenControl.s_OnScreenDevices[i].Destroy();
						OnScreenControl.s_OnScreenDevices.RemoveAt(i);
					}
					else
					{
						OnScreenControl.s_OnScreenDevices[i] = deviceInfo;
						if (!this.m_Control.CheckStateIsAtDefault())
						{
							this.SentDefaultValueToControl();
						}
					}
					this.m_Control = null;
					this.m_InputEventPtr = default(InputEventPtr);
					return;
				}
			}
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x000476B6 File Offset: 0x000458B6
		internal string GetWarningMessage()
		{
			return string.Format("{0} needs to be attached as a child to a UI Canvas and have a RectTransform component to function properly.", base.GetType());
		}

		// Token: 0x0400071F RID: 1823
		private InputControl m_Control;

		// Token: 0x04000720 RID: 1824
		private OnScreenControl m_NextControlOnDevice;

		// Token: 0x04000721 RID: 1825
		private InputEventPtr m_InputEventPtr;

		// Token: 0x04000722 RID: 1826
		private static InlinedArray<OnScreenControl.OnScreenDeviceInfo> s_OnScreenDevices;

		// Token: 0x02000133 RID: 307
		private struct OnScreenDeviceInfo
		{
			// Token: 0x06000E2B RID: 3627 RVA: 0x000476D0 File Offset: 0x000458D0
			public OnScreenControl.OnScreenDeviceInfo AddControl(OnScreenControl control)
			{
				control.m_NextControlOnDevice = this.firstControl;
				this.firstControl = control;
				return this;
			}

			// Token: 0x06000E2C RID: 3628 RVA: 0x000476EC File Offset: 0x000458EC
			public OnScreenControl.OnScreenDeviceInfo RemoveControl(OnScreenControl control)
			{
				if (this.firstControl == control)
				{
					this.firstControl = control.m_NextControlOnDevice;
				}
				else
				{
					OnScreenControl current = this.firstControl.m_NextControlOnDevice;
					OnScreenControl previous = this.firstControl;
					while (current != null)
					{
						if (!(current != control))
						{
							previous.m_NextControlOnDevice = current.m_NextControlOnDevice;
							break;
						}
						previous = current;
						current = current.m_NextControlOnDevice;
					}
				}
				control.m_NextControlOnDevice = null;
				return this;
			}

			// Token: 0x06000E2D RID: 3629 RVA: 0x00047760 File Offset: 0x00045960
			public void Destroy()
			{
				if (this.buffer.IsCreated)
				{
					this.buffer.Dispose();
				}
				if (this.device != null)
				{
					InputSystem.RemoveDevice(this.device);
				}
				this.device = null;
				this.buffer = default(NativeArray<byte>);
			}

			// Token: 0x04000723 RID: 1827
			public InputEventPtr eventPtr;

			// Token: 0x04000724 RID: 1828
			public NativeArray<byte> buffer;

			// Token: 0x04000725 RID: 1829
			public InputDevice device;

			// Token: 0x04000726 RID: 1830
			public OnScreenControl firstControl;
		}
	}
}
