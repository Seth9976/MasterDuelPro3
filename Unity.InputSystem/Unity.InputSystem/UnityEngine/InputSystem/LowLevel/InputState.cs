using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020001D5 RID: 469
	public static class InputState
	{
		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x0600115D RID: 4445 RVA: 0x000519BC File Offset: 0x0004FBBC
		public static InputUpdateType currentUpdateType
		{
			get
			{
				return InputUpdate.s_LatestUpdateType;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x0600115E RID: 4446 RVA: 0x000519C3 File Offset: 0x0004FBC3
		public static uint updateCount
		{
			get
			{
				return InputUpdate.s_UpdateStepCount;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x0600115F RID: 4447 RVA: 0x000519CA File Offset: 0x0004FBCA
		public static double currentTime
		{
			get
			{
				return InputRuntime.s_Instance.currentTime - InputRuntime.s_CurrentTimeOffsetToRealtimeSinceStartup;
			}
		}

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06001160 RID: 4448 RVA: 0x000519DC File Offset: 0x0004FBDC
		// (remove) Token: 0x06001161 RID: 4449 RVA: 0x000519E9 File Offset: 0x0004FBE9
		public static event Action<InputDevice, InputEventPtr> onChange
		{
			add
			{
				InputSystem.s_Manager.onDeviceStateChange += value;
			}
			remove
			{
				InputSystem.s_Manager.onDeviceStateChange -= value;
			}
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x000519F8 File Offset: 0x0004FBF8
		public unsafe static void Change(InputDevice device, InputEventPtr eventPtr, InputUpdateType updateType = InputUpdateType.None)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			if (!eventPtr.valid)
			{
				throw new ArgumentNullException("eventPtr");
			}
			FourCC eventType = eventPtr.type;
			FourCC stateFormat;
			if (eventType == 1398030676)
			{
				stateFormat = StateEvent.FromUnchecked(eventPtr)->stateFormat;
			}
			else
			{
				if (!(eventType == 1145852993))
				{
					return;
				}
				stateFormat = DeltaStateEvent.FromUnchecked(eventPtr)->stateFormat;
			}
			if (stateFormat != device.stateBlock.format)
			{
				throw new ArgumentException(string.Format("State format {0} from event does not match state format {1} of device {2}", stateFormat, device.stateBlock.format, device), "eventPtr");
			}
			InputSystem.s_Manager.UpdateState(device, eventPtr, (updateType != InputUpdateType.None) ? updateType : InputSystem.s_Manager.defaultUpdateType);
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x00051AD7 File Offset: 0x0004FCD7
		public static void Change<TState>(InputControl control, TState state, InputUpdateType updateType = InputUpdateType.None, InputEventPtr eventPtr = default(InputEventPtr)) where TState : struct
		{
			InputState.Change<TState>(control, ref state, updateType, eventPtr);
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x00051AE4 File Offset: 0x0004FCE4
		public unsafe static void Change<TState>(InputControl control, ref TState state, InputUpdateType updateType = InputUpdateType.None, InputEventPtr eventPtr = default(InputEventPtr)) where TState : struct
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (control.stateBlock.bitOffset != 0U || control.stateBlock.sizeInBits % 8U != 0U)
			{
				throw new ArgumentException(string.Format("Cannot change state of bitfield control '{0}' using this method", control), "control");
			}
			InputDevice device = control.device;
			long stateSize = Math.Min((long)UnsafeUtility.SizeOf<TState>(), (long)((ulong)control.m_StateBlock.alignedSizeInBytes));
			void* statePtr = UnsafeUtility.AddressOf<TState>(ref state);
			uint stateOffset = control.stateBlock.byteOffset - device.stateBlock.byteOffset;
			InputSystem.s_Manager.UpdateState(device, (updateType != InputUpdateType.None) ? updateType : InputSystem.s_Manager.defaultUpdateType, statePtr, stateOffset, (uint)stateSize, eventPtr.valid ? eventPtr.internalTime : InputRuntime.s_Instance.currentTime, eventPtr);
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x00051BBC File Offset: 0x0004FDBC
		public static bool IsIntegerFormat(this FourCC format)
		{
			return format == InputStateBlock.FormatBit || format == InputStateBlock.FormatInt || format == InputStateBlock.FormatByte || format == InputStateBlock.FormatShort || format == InputStateBlock.FormatSBit || format == InputStateBlock.FormatUInt || format == InputStateBlock.FormatUShort || format == InputStateBlock.FormatLong || format == InputStateBlock.FormatULong;
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x00051C40 File Offset: 0x0004FE40
		public static void AddChangeMonitor(InputControl control, IInputStateChangeMonitor monitor, long monitorIndex = -1L, uint groupIndex = 0U)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (monitor == null)
			{
				throw new ArgumentNullException("monitor");
			}
			if (!control.device.added)
			{
				throw new ArgumentException(string.Format("Device for control '{0}' has not been added to system", control));
			}
			InputSystem.s_Manager.AddStateChangeMonitor(control, monitor, monitorIndex, groupIndex);
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x00051C98 File Offset: 0x0004FE98
		public static IInputStateChangeMonitor AddChangeMonitor(InputControl control, Action<InputControl, double, InputEventPtr, long> valueChangeCallback, int monitorIndex = -1, Action<InputControl, double, long, int> timerExpiredCallback = null)
		{
			if (valueChangeCallback == null)
			{
				throw new ArgumentNullException("valueChangeCallback");
			}
			InputState.StateChangeMonitorDelegate monitor = new InputState.StateChangeMonitorDelegate
			{
				valueChangeCallback = valueChangeCallback,
				timerExpiredCallback = timerExpiredCallback
			};
			InputState.AddChangeMonitor(control, monitor, (long)monitorIndex, 0U);
			return monitor;
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x00051CD2 File Offset: 0x0004FED2
		public static void RemoveChangeMonitor(InputControl control, IInputStateChangeMonitor monitor, long monitorIndex = -1L)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (monitor == null)
			{
				throw new ArgumentNullException("monitor");
			}
			InputSystem.s_Manager.RemoveStateChangeMonitor(control, monitor, monitorIndex);
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x00051CFD File Offset: 0x0004FEFD
		public static void AddChangeMonitorTimeout(InputControl control, IInputStateChangeMonitor monitor, double time, long monitorIndex = -1L, int timerIndex = -1)
		{
			if (monitor == null)
			{
				throw new ArgumentNullException("monitor");
			}
			InputSystem.s_Manager.AddStateChangeMonitorTimeout(control, monitor, time, monitorIndex, timerIndex);
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x00051D1D File Offset: 0x0004FF1D
		public static void RemoveChangeMonitorTimeout(IInputStateChangeMonitor monitor, long monitorIndex = -1L, int timerIndex = -1)
		{
			if (monitor == null)
			{
				throw new ArgumentNullException("monitor");
			}
			InputSystem.s_Manager.RemoveStateChangeMonitorTimeout(monitor, monitorIndex, timerIndex);
		}

		// Token: 0x020001D6 RID: 470
		private class StateChangeMonitorDelegate : IInputStateChangeMonitor
		{
			// Token: 0x0600116B RID: 4459 RVA: 0x00051D3A File Offset: 0x0004FF3A
			public void NotifyControlStateChanged(InputControl control, double time, InputEventPtr eventPtr, long monitorIndex)
			{
				this.valueChangeCallback(control, time, eventPtr, monitorIndex);
			}

			// Token: 0x0600116C RID: 4460 RVA: 0x00051D4C File Offset: 0x0004FF4C
			public void NotifyTimerExpired(InputControl control, double time, long monitorIndex, int timerIndex)
			{
				Action<InputControl, double, long, int> action = this.timerExpiredCallback;
				if (action == null)
				{
					return;
				}
				action(control, time, monitorIndex, timerIndex);
			}

			// Token: 0x04000A6C RID: 2668
			public Action<InputControl, double, InputEventPtr, long> valueChangeCallback;

			// Token: 0x04000A6D RID: 2669
			public Action<InputControl, double, long, int> timerExpiredCallback;
		}
	}
}
