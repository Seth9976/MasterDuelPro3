using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.EnhancedTouch
{
	// Token: 0x02000156 RID: 342
	[AddComponentMenu("Input/Debug/Touch Simulation")]
	[ExecuteInEditMode]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.inputsystem@1.11/manual/Touch.html#touch-simulation")]
	public class TouchSimulation : MonoBehaviour, IInputStateChangeMonitor
	{
		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000EE3 RID: 3811 RVA: 0x0004B9A6 File Offset: 0x00049BA6
		// (set) Token: 0x06000EE4 RID: 3812 RVA: 0x0004B9AE File Offset: 0x00049BAE
		public Touchscreen simulatedTouchscreen { get; private set; }

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x0004B9B7 File Offset: 0x00049BB7
		public static TouchSimulation instance
		{
			get
			{
				return TouchSimulation.s_Instance;
			}
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x0004B9C0 File Offset: 0x00049BC0
		public static void Enable()
		{
			if (TouchSimulation.instance == null)
			{
				GameObject gameObject = new GameObject();
				gameObject.SetActive(false);
				gameObject.hideFlags = HideFlags.HideAndDontSave;
				TouchSimulation.s_Instance = gameObject.AddComponent<TouchSimulation>();
				TouchSimulation.instance.gameObject.SetActive(true);
			}
			TouchSimulation.instance.enabled = true;
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x0004BA13 File Offset: 0x00049C13
		public static void Disable()
		{
			if (TouchSimulation.instance != null)
			{
				TouchSimulation.instance.enabled = false;
			}
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x0004BA2D File Offset: 0x00049C2D
		public static void Destroy()
		{
			TouchSimulation.Disable();
			if (TouchSimulation.s_Instance != null)
			{
				Object.Destroy(TouchSimulation.s_Instance.gameObject);
				TouchSimulation.s_Instance = null;
			}
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0004BA58 File Offset: 0x00049C58
		protected void AddPointer(Pointer pointer)
		{
			if (pointer == null)
			{
				throw new ArgumentNullException("pointer");
			}
			if (this.m_Pointers.ContainsReference(this.m_NumPointers, pointer))
			{
				return;
			}
			ArrayHelpers.AppendWithCapacity<Pointer>(ref this.m_Pointers, ref this.m_NumPointers, pointer, 10);
			ArrayHelpers.Append<Vector2>(ref this.m_CurrentPositions, default(Vector2));
			ArrayHelpers.Append<int>(ref this.m_CurrentDisplayIndices, 0);
			InputSystem.DisableDevice(pointer, true);
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0004BAC8 File Offset: 0x00049CC8
		protected void RemovePointer(Pointer pointer)
		{
			if (pointer == null)
			{
				throw new ArgumentNullException("pointer");
			}
			int pointerIndex = this.m_Pointers.IndexOfReference(pointer, this.m_NumPointers);
			if (pointerIndex == -1)
			{
				return;
			}
			for (int i = 0; i < this.m_Touches.Length; i++)
			{
				ButtonControl button = this.m_Touches[i];
				if (button == null || button.device == pointer)
				{
					this.UpdateTouch(i, pointerIndex, TouchPhase.Canceled, default(InputEventPtr));
				}
			}
			this.m_Pointers.EraseAtWithCapacity(ref this.m_NumPointers, pointerIndex);
			ArrayHelpers.EraseAt<Vector2>(ref this.m_CurrentPositions, pointerIndex);
			ArrayHelpers.EraseAt<int>(ref this.m_CurrentDisplayIndices, pointerIndex);
			if (pointer.added)
			{
				InputSystem.EnableDevice(pointer);
			}
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x0004BB70 File Offset: 0x00049D70
		private unsafe void OnEvent(InputEventPtr eventPtr, InputDevice device)
		{
			if (device == this.simulatedTouchscreen)
			{
				return;
			}
			int pointerIndex = this.m_Pointers.IndexOfReference(device, this.m_NumPointers);
			if (pointerIndex < 0)
			{
				return;
			}
			FourCC eventType = eventPtr.type;
			if (eventType != 1398030676 && eventType != 1145852993)
			{
				return;
			}
			Pointer pointer = this.m_Pointers[pointerIndex];
			Vector2Control positionControl = pointer.position;
			void* positionStatePtr = positionControl.GetStatePtrFromStateEventUnchecked(eventPtr, eventType);
			if (positionStatePtr != null)
			{
				this.m_CurrentPositions[pointerIndex] = positionControl.ReadValueFromState(positionStatePtr);
			}
			IntegerControl displayIndexControl = pointer.displayIndex;
			void* displayIndexStatePtr = displayIndexControl.GetStatePtrFromStateEventUnchecked(eventPtr, eventType);
			if (displayIndexStatePtr != null)
			{
				this.m_CurrentDisplayIndices[pointerIndex] = displayIndexControl.ReadValueFromState(displayIndexStatePtr);
			}
			for (int i = 0; i < this.m_Touches.Length; i++)
			{
				ButtonControl button = this.m_Touches[i];
				if (button != null && button.device == device)
				{
					void* buttonStatePtr = button.GetStatePtrFromStateEventUnchecked(eventPtr, eventType);
					if (buttonStatePtr == null)
					{
						if (positionStatePtr != null)
						{
							this.UpdateTouch(i, pointerIndex, TouchPhase.Moved, eventPtr);
						}
					}
					else if (button.ReadValueFromState(buttonStatePtr) < ButtonControl.s_GlobalDefaultButtonPressPoint * ButtonControl.s_GlobalDefaultButtonReleaseThreshold)
					{
						this.UpdateTouch(i, pointerIndex, TouchPhase.Ended, eventPtr);
					}
				}
			}
			foreach (InputControl control in eventPtr.EnumerateControls(InputControlExtensions.Enumerate.IgnoreControlsInDefaultState, device, 0f))
			{
				if (control.isButton)
				{
					void* buttonStatePtr2 = control.GetStatePtrFromStateEventUnchecked(eventPtr, eventType);
					float value = 0f;
					control.ReadValueFromStateIntoBuffer(buttonStatePtr2, UnsafeUtility.AddressOf<float>(ref value), 4);
					if (value > ButtonControl.s_GlobalDefaultButtonPressPoint)
					{
						int touchIndex = this.m_Touches.IndexOfReference(control, -1);
						if (touchIndex < 0)
						{
							touchIndex = this.m_Touches.IndexOfReference(null, -1);
							if (touchIndex >= 0)
							{
								this.m_Touches[touchIndex] = (ButtonControl)control;
								this.UpdateTouch(touchIndex, pointerIndex, TouchPhase.Began, eventPtr);
							}
						}
						else
						{
							this.UpdateTouch(touchIndex, pointerIndex, TouchPhase.Moved, eventPtr);
						}
					}
				}
			}
			eventPtr.handled = true;
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x0004BD7C File Offset: 0x00049F7C
		private void OnDeviceChange(InputDevice device, InputDeviceChange change)
		{
			if (device == this.simulatedTouchscreen && change == InputDeviceChange.Removed)
			{
				TouchSimulation.Disable();
				return;
			}
			if (change != InputDeviceChange.Added)
			{
				if (change != InputDeviceChange.Removed)
				{
					return;
				}
				Pointer pointer = device as Pointer;
				if (pointer != null)
				{
					this.RemovePointer(pointer);
				}
			}
			else
			{
				Pointer pointer2 = device as Pointer;
				if (pointer2 != null)
				{
					if (device is Touchscreen)
					{
						return;
					}
					this.AddPointer(pointer2);
					return;
				}
			}
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x0004BDD0 File Offset: 0x00049FD0
		protected void OnEnable()
		{
			if (this.simulatedTouchscreen != null)
			{
				if (!this.simulatedTouchscreen.added)
				{
					InputSystem.AddDevice(this.simulatedTouchscreen);
				}
			}
			else
			{
				this.simulatedTouchscreen = InputSystem.GetDevice("Simulated Touchscreen") as Touchscreen;
				if (this.simulatedTouchscreen == null)
				{
					this.simulatedTouchscreen = InputSystem.AddDevice<Touchscreen>("Simulated Touchscreen");
				}
			}
			if (this.m_Touches == null)
			{
				this.m_Touches = new ButtonControl[this.simulatedTouchscreen.touches.Count];
			}
			if (this.m_TouchIds == null)
			{
				this.m_TouchIds = new int[this.simulatedTouchscreen.touches.Count];
			}
			foreach (InputDevice device in InputSystem.devices)
			{
				this.OnDeviceChange(device, InputDeviceChange.Added);
			}
			if (this.m_OnDeviceChange == null)
			{
				this.m_OnDeviceChange = new Action<InputDevice, InputDeviceChange>(this.OnDeviceChange);
			}
			if (this.m_OnEvent == null)
			{
				this.m_OnEvent = new Action<InputEventPtr, InputDevice>(this.OnEvent);
			}
			InputSystem.onDeviceChange += this.m_OnDeviceChange;
			InputSystem.onEvent += this.m_OnEvent;
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x0004BF18 File Offset: 0x0004A118
		protected void OnDisable()
		{
			if (this.simulatedTouchscreen != null && this.simulatedTouchscreen.added)
			{
				InputSystem.RemoveDevice(this.simulatedTouchscreen);
			}
			for (int i = 0; i < this.m_NumPointers; i++)
			{
				InputSystem.EnableDevice(this.m_Pointers[i]);
			}
			this.m_Pointers.Clear(this.m_NumPointers);
			this.m_Touches.Clear<ButtonControl>();
			this.m_NumPointers = 0;
			this.m_LastTouchId = 0;
			InputSystem.onDeviceChange -= this.m_OnDeviceChange;
			InputSystem.onEvent -= this.m_OnEvent;
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x0004BFB0 File Offset: 0x0004A1B0
		private void UpdateTouch(int touchIndex, int pointerIndex, TouchPhase phase, InputEventPtr eventPtr = default(InputEventPtr))
		{
			Vector2 position = this.m_CurrentPositions[pointerIndex];
			byte displayIndex = (byte)this.m_CurrentDisplayIndices[pointerIndex];
			TouchState touch = new TouchState
			{
				phase = phase,
				position = position,
				displayIndex = displayIndex
			};
			if (phase == TouchPhase.Began)
			{
				touch.startTime = (eventPtr.valid ? eventPtr.time : InputState.currentTime);
				touch.startPosition = position;
				int num = this.m_LastTouchId + 1;
				this.m_LastTouchId = num;
				touch.touchId = num;
				this.m_TouchIds[touchIndex] = this.m_LastTouchId;
			}
			else
			{
				touch.touchId = this.m_TouchIds[touchIndex];
			}
			InputSystem.QueueStateEvent<TouchState>(this.simulatedTouchscreen, touch, -1.0);
			if (phase.IsEndedOrCanceled())
			{
				this.m_Touches[touchIndex] = null;
			}
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x000049FE File Offset: 0x00002BFE
		void IInputStateChangeMonitor.NotifyControlStateChanged(InputControl control, double time, InputEventPtr eventPtr, long monitorIndex)
		{
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x000049FE File Offset: 0x00002BFE
		void IInputStateChangeMonitor.NotifyTimerExpired(InputControl control, double time, long monitorIndex, int timerIndex)
		{
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x000049FE File Offset: 0x00002BFE
		protected void InstallStateChangeMonitors(int startIndex = 0)
		{
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x000049FE File Offset: 0x00002BFE
		protected void OnSourceControlChangedValue(InputControl control, double time, InputEventPtr eventPtr, long sourceDeviceAndButtonIndex)
		{
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x000049FE File Offset: 0x00002BFE
		protected void UninstallStateChangeMonitors(int startIndex = 0)
		{
		}

		// Token: 0x0400087D RID: 2173
		[NonSerialized]
		private int m_NumPointers;

		// Token: 0x0400087E RID: 2174
		[NonSerialized]
		private Pointer[] m_Pointers;

		// Token: 0x0400087F RID: 2175
		[NonSerialized]
		private Vector2[] m_CurrentPositions;

		// Token: 0x04000880 RID: 2176
		[NonSerialized]
		private int[] m_CurrentDisplayIndices;

		// Token: 0x04000881 RID: 2177
		[NonSerialized]
		private ButtonControl[] m_Touches;

		// Token: 0x04000882 RID: 2178
		[NonSerialized]
		private int[] m_TouchIds;

		// Token: 0x04000883 RID: 2179
		[NonSerialized]
		private int m_LastTouchId;

		// Token: 0x04000884 RID: 2180
		[NonSerialized]
		private Action<InputDevice, InputDeviceChange> m_OnDeviceChange;

		// Token: 0x04000885 RID: 2181
		[NonSerialized]
		private Action<InputEventPtr, InputDevice> m_OnEvent;

		// Token: 0x04000886 RID: 2182
		internal static TouchSimulation s_Instance;
	}
}
