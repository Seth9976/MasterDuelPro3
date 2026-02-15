using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.DualShock.LowLevel;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.DualShock
{
	// Token: 0x02000158 RID: 344
	[InputControlLayout(stateType = typeof(DualSenseHIDInputReport), displayName = "DualSense HID")]
	public class DualSenseGamepadHID : DualShockGamepad, IEventMerger, IEventPreProcessor, IInputStateCallbackReceiver
	{
		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000F11 RID: 3857 RVA: 0x0004C21E File Offset: 0x0004A41E
		// (set) Token: 0x06000F12 RID: 3858 RVA: 0x0004C226 File Offset: 0x0004A426
		public ButtonControl leftTriggerButton { get; protected set; }

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000F13 RID: 3859 RVA: 0x0004C22F File Offset: 0x0004A42F
		// (set) Token: 0x06000F14 RID: 3860 RVA: 0x0004C237 File Offset: 0x0004A437
		public ButtonControl rightTriggerButton { get; protected set; }

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000F15 RID: 3861 RVA: 0x0004C240 File Offset: 0x0004A440
		// (set) Token: 0x06000F16 RID: 3862 RVA: 0x0004C248 File Offset: 0x0004A448
		public ButtonControl playStationButton { get; protected set; }

		// Token: 0x06000F17 RID: 3863 RVA: 0x0004C251 File Offset: 0x0004A451
		protected override void FinishSetup()
		{
			this.leftTriggerButton = base.GetChildControl<ButtonControl>("leftTriggerButton");
			this.rightTriggerButton = base.GetChildControl<ButtonControl>("rightTriggerButton");
			this.playStationButton = base.GetChildControl<ButtonControl>("systemButton");
			base.FinishSetup();
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x0004C28C File Offset: 0x0004A48C
		public override void PauseHaptics()
		{
			if (this.m_LowFrequencyMotorSpeed == null && this.m_HighFrequenceyMotorSpeed == null)
			{
				return;
			}
			this.SetMotorSpeedsAndLightBarColor(new float?(0f), new float?(0f), this.m_LightBarColor);
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x0004C2CC File Offset: 0x0004A4CC
		public override void ResetHaptics()
		{
			if (this.m_LowFrequencyMotorSpeed == null && this.m_HighFrequenceyMotorSpeed == null)
			{
				return;
			}
			this.m_HighFrequenceyMotorSpeed = null;
			this.m_LowFrequencyMotorSpeed = null;
			this.SetMotorSpeedsAndLightBarColor(this.m_LowFrequencyMotorSpeed, this.m_HighFrequenceyMotorSpeed, this.m_LightBarColor);
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x0004C325 File Offset: 0x0004A525
		public override void ResumeHaptics()
		{
			if (this.m_LowFrequencyMotorSpeed == null && this.m_HighFrequenceyMotorSpeed == null)
			{
				return;
			}
			this.SetMotorSpeedsAndLightBarColor(this.m_LowFrequencyMotorSpeed, this.m_HighFrequenceyMotorSpeed, this.m_LightBarColor);
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x0004C35B File Offset: 0x0004A55B
		public override void SetLightBarColor(Color color)
		{
			this.m_LightBarColor = new Color?(color);
			this.SetMotorSpeedsAndLightBarColor(this.m_LowFrequencyMotorSpeed, this.m_HighFrequenceyMotorSpeed, this.m_LightBarColor);
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x0004C382 File Offset: 0x0004A582
		public override void SetMotorSpeeds(float lowFrequency, float highFrequency)
		{
			this.m_LowFrequencyMotorSpeed = new float?(lowFrequency);
			this.m_HighFrequenceyMotorSpeed = new float?(highFrequency);
			this.SetMotorSpeedsAndLightBarColor(this.m_LowFrequencyMotorSpeed, this.m_HighFrequenceyMotorSpeed, this.m_LightBarColor);
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x0004C3B8 File Offset: 0x0004A5B8
		public bool SetMotorSpeedsAndLightBarColor(float? lowFrequency, float? highFrequency, Color? color)
		{
			float lf = ((lowFrequency != null) ? lowFrequency.Value : 0f);
			float hf = ((highFrequency != null) ? highFrequency.Value : 0f);
			Color c = ((color != null) ? color.Value : Color.black);
			DualSenseHIDUSBOutputReport command = DualSenseHIDUSBOutputReport.Create(new DualSenseHIDOutputReportPayload
			{
				enableFlags1 = 3,
				enableFlags2 = 4,
				lowFrequencyMotorSpeed = (byte)NumberHelpers.NormalizedFloatToUInt(lf, 0U, 255U),
				highFrequencyMotorSpeed = (byte)NumberHelpers.NormalizedFloatToUInt(hf, 0U, 255U),
				redColor = (byte)NumberHelpers.NormalizedFloatToUInt(c.r, 0U, 255U),
				greenColor = (byte)NumberHelpers.NormalizedFloatToUInt(c.g, 0U, 255U),
				blueColor = (byte)NumberHelpers.NormalizedFloatToUInt(c.b, 0U, 255U)
			}, base.hidDescriptor.outputReportSize);
			return base.ExecuteCommand<DualSenseHIDUSBOutputReport>(ref command) >= 0L;
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x0004C4BA File Offset: 0x0004A6BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static bool MergeForward(DualSenseGamepadHID.DualSenseHIDUSBInputReport* currentState, DualSenseGamepadHID.DualSenseHIDUSBInputReport* nextState)
		{
			return currentState->buttons0 == nextState->buttons0 && currentState->buttons1 == nextState->buttons1 && currentState->buttons2 == nextState->buttons2;
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x0004C4E8 File Offset: 0x0004A6E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static bool MergeForward(DualSenseGamepadHID.DualSenseHIDBluetoothInputReport* currentState, DualSenseGamepadHID.DualSenseHIDBluetoothInputReport* nextState)
		{
			return currentState->buttons0 == nextState->buttons0 && currentState->buttons1 == nextState->buttons1 && currentState->buttons2 == nextState->buttons2;
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x0004C516 File Offset: 0x0004A716
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static bool MergeForward(DualSenseGamepadHID.DualSenseHIDMinimalInputReport* currentState, DualSenseGamepadHID.DualSenseHIDMinimalInputReport* nextState)
		{
			return currentState->buttons0 == nextState->buttons0 && currentState->buttons1 == nextState->buttons1 && currentState->buttons2 == nextState->buttons2;
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x0004C544 File Offset: 0x0004A744
		unsafe bool IEventMerger.MergeForward(InputEventPtr currentEventPtr, InputEventPtr nextEventPtr)
		{
			if (currentEventPtr.type != 1398030676 || nextEventPtr.type != 1398030676)
			{
				return false;
			}
			StateEvent* currentEvent = StateEvent.FromUnchecked(currentEventPtr);
			StateEvent* nextEvent = StateEvent.FromUnchecked(nextEventPtr);
			if (currentEvent->stateFormat != DualSenseGamepadHID.DualSenseHIDGenericInputReport.Format || nextEvent->stateFormat != DualSenseGamepadHID.DualSenseHIDGenericInputReport.Format)
			{
				return false;
			}
			if (currentEvent->stateSizeInBytes != nextEvent->stateSizeInBytes)
			{
				return false;
			}
			DualSenseGamepadHID.DualSenseHIDGenericInputReport* currentGenericReport = (DualSenseGamepadHID.DualSenseHIDGenericInputReport*)currentEvent->state;
			DualSenseGamepadHID.DualSenseHIDGenericInputReport* nextGenericReport = (DualSenseGamepadHID.DualSenseHIDGenericInputReport*)nextEvent->state;
			if (currentGenericReport->reportId != nextGenericReport->reportId)
			{
				return false;
			}
			if (currentGenericReport->reportId == 1)
			{
				if ((ulong)currentEvent->stateSizeInBytes == (ulong)((long)DualSenseGamepadHID.DualSenseHIDMinimalInputReport.ExpectedSize1) || (ulong)currentEvent->stateSizeInBytes == (ulong)((long)DualSenseGamepadHID.DualSenseHIDMinimalInputReport.ExpectedSize2))
				{
					DualSenseGamepadHID.DualSenseHIDMinimalInputReport* currentState = (DualSenseGamepadHID.DualSenseHIDMinimalInputReport*)currentEvent->state;
					DualSenseGamepadHID.DualSenseHIDMinimalInputReport* nextState = (DualSenseGamepadHID.DualSenseHIDMinimalInputReport*)nextEvent->state;
					return DualSenseGamepadHID.MergeForward(currentState, nextState);
				}
				DualSenseGamepadHID.DualSenseHIDUSBInputReport* currentState2 = (DualSenseGamepadHID.DualSenseHIDUSBInputReport*)currentEvent->state;
				DualSenseGamepadHID.DualSenseHIDUSBInputReport* nextState2 = (DualSenseGamepadHID.DualSenseHIDUSBInputReport*)nextEvent->state;
				return DualSenseGamepadHID.MergeForward(currentState2, nextState2);
			}
			else
			{
				if (currentGenericReport->reportId == 49)
				{
					DualSenseGamepadHID.DualSenseHIDBluetoothInputReport* currentState3 = (DualSenseGamepadHID.DualSenseHIDBluetoothInputReport*)currentEvent->state;
					DualSenseGamepadHID.DualSenseHIDBluetoothInputReport* nextState3 = (DualSenseGamepadHID.DualSenseHIDBluetoothInputReport*)nextEvent->state;
					return DualSenseGamepadHID.MergeForward(currentState3, nextState3);
				}
				return false;
			}
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x0004C668 File Offset: 0x0004A868
		unsafe bool IEventPreProcessor.PreProcessEvent(InputEventPtr eventPtr)
		{
			if (eventPtr.type != 1398030676)
			{
				return eventPtr.type != 1145852993;
			}
			StateEvent* stateEvent = StateEvent.FromUnchecked(eventPtr);
			if (stateEvent->stateFormat == DualSenseHIDInputReport.Format)
			{
				return true;
			}
			uint size = stateEvent->stateSizeInBytes;
			if (stateEvent->stateFormat != DualSenseGamepadHID.DualSenseHIDGenericInputReport.Format || (ulong)size < (ulong)((long)sizeof(DualSenseHIDInputReport)))
			{
				return false;
			}
			DualSenseGamepadHID.DualSenseHIDGenericInputReport* genericReport = (DualSenseGamepadHID.DualSenseHIDGenericInputReport*)stateEvent->state;
			if (genericReport->reportId == 1)
			{
				if ((ulong)stateEvent->stateSizeInBytes == (ulong)((long)DualSenseGamepadHID.DualSenseHIDMinimalInputReport.ExpectedSize1) || (ulong)stateEvent->stateSizeInBytes == (ulong)((long)DualSenseGamepadHID.DualSenseHIDMinimalInputReport.ExpectedSize2))
				{
					DualSenseHIDInputReport data = ((DualSenseGamepadHID.DualSenseHIDMinimalInputReport*)stateEvent->state)->ToHIDInputReport();
					*(DualSenseHIDInputReport*)stateEvent->state = data;
				}
				else
				{
					DualSenseHIDInputReport data2 = ((DualSenseGamepadHID.DualSenseHIDUSBInputReport*)stateEvent->state)->ToHIDInputReport();
					*(DualSenseHIDInputReport*)stateEvent->state = data2;
				}
				stateEvent->stateFormat = DualSenseHIDInputReport.Format;
				return true;
			}
			if (genericReport->reportId == 49)
			{
				DualSenseHIDInputReport data3 = ((DualSenseGamepadHID.DualSenseHIDBluetoothInputReport*)stateEvent->state)->ToHIDInputReport();
				*(DualSenseHIDInputReport*)stateEvent->state = data3;
				stateEvent->stateFormat = DualSenseHIDInputReport.Format;
				return true;
			}
			return false;
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x000049FE File Offset: 0x00002BFE
		public void OnNextUpdate()
		{
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x0004C788 File Offset: 0x0004A988
		public unsafe void OnStateEvent(InputEventPtr eventPtr)
		{
			if (eventPtr.type == 1398030676 && eventPtr.stateFormat == DualSenseHIDInputReport.Format)
			{
				DualSenseHIDInputReport* currentState = (DualSenseHIDInputReport*)((byte*)base.currentStatePtr + this.m_StateBlock.byteOffset);
				DualSenseHIDInputReport* newState = (DualSenseHIDInputReport*)StateEvent.FromUnchecked(eventPtr)->state;
				if (newState->leftStickX >= 120 && newState->leftStickX <= 135 && newState->leftStickY >= 120 && newState->leftStickY <= 135 && newState->rightStickX >= 120 && newState->rightStickX <= 135 && newState->rightStickY >= 120 && newState->rightStickY <= 135 && newState->leftTrigger == currentState->leftTrigger && newState->rightTrigger == currentState->rightTrigger && newState->buttons0 == currentState->buttons0 && newState->buttons1 == currentState->buttons1 && newState->buttons2 == currentState->buttons2)
				{
					InputSystem.s_Manager.DontMakeCurrentlyUpdatingDeviceCurrent();
				}
			}
			InputState.Change(this, eventPtr, InputUpdateType.None);
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x0001751C File Offset: 0x0001571C
		public bool GetStateOffsetForEvent(InputControl control, InputEventPtr eventPtr, ref uint offset)
		{
			return false;
		}

		// Token: 0x04000895 RID: 2197
		private float? m_LowFrequencyMotorSpeed;

		// Token: 0x04000896 RID: 2198
		private float? m_HighFrequenceyMotorSpeed;

		// Token: 0x04000897 RID: 2199
		protected Color? m_LightBarColor;

		// Token: 0x04000898 RID: 2200
		private byte outputSequenceId;

		// Token: 0x04000899 RID: 2201
		internal const byte JitterMaskLow = 120;

		// Token: 0x0400089A RID: 2202
		internal const byte JitterMaskHigh = 135;

		// Token: 0x02000159 RID: 345
		[StructLayout(LayoutKind.Explicit)]
		internal struct DualSenseHIDGenericInputReport
		{
			// Token: 0x17000411 RID: 1041
			// (get) Token: 0x06000F27 RID: 3879 RVA: 0x00046CC2 File Offset: 0x00044EC2
			public static FourCC Format
			{
				get
				{
					return new FourCC('H', 'I', 'D', ' ');
				}
			}

			// Token: 0x0400089B RID: 2203
			[FieldOffset(0)]
			public byte reportId;
		}

		// Token: 0x0200015A RID: 346
		[StructLayout(LayoutKind.Explicit)]
		internal struct DualSenseHIDUSBInputReport
		{
			// Token: 0x06000F28 RID: 3880 RVA: 0x0004C8B4 File Offset: 0x0004AAB4
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public DualSenseHIDInputReport ToHIDInputReport()
			{
				return new DualSenseHIDInputReport
				{
					leftStickX = this.leftStickX,
					leftStickY = this.leftStickY,
					rightStickX = this.rightStickX,
					rightStickY = this.rightStickY,
					leftTrigger = this.leftTrigger,
					rightTrigger = this.rightTrigger,
					buttons0 = this.buttons0,
					buttons1 = this.buttons1,
					buttons2 = (this.buttons2 & 7)
				};
			}

			// Token: 0x0400089C RID: 2204
			public const int ExpectedReportId = 1;

			// Token: 0x0400089D RID: 2205
			[FieldOffset(0)]
			public byte reportId;

			// Token: 0x0400089E RID: 2206
			[FieldOffset(1)]
			public byte leftStickX;

			// Token: 0x0400089F RID: 2207
			[FieldOffset(2)]
			public byte leftStickY;

			// Token: 0x040008A0 RID: 2208
			[FieldOffset(3)]
			public byte rightStickX;

			// Token: 0x040008A1 RID: 2209
			[FieldOffset(4)]
			public byte rightStickY;

			// Token: 0x040008A2 RID: 2210
			[FieldOffset(5)]
			public byte leftTrigger;

			// Token: 0x040008A3 RID: 2211
			[FieldOffset(6)]
			public byte rightTrigger;

			// Token: 0x040008A4 RID: 2212
			[FieldOffset(8)]
			public byte buttons0;

			// Token: 0x040008A5 RID: 2213
			[FieldOffset(9)]
			public byte buttons1;

			// Token: 0x040008A6 RID: 2214
			[FieldOffset(10)]
			public byte buttons2;
		}

		// Token: 0x0200015B RID: 347
		[StructLayout(LayoutKind.Explicit)]
		internal struct DualSenseHIDBluetoothInputReport
		{
			// Token: 0x06000F29 RID: 3881 RVA: 0x0004C944 File Offset: 0x0004AB44
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public DualSenseHIDInputReport ToHIDInputReport()
			{
				return new DualSenseHIDInputReport
				{
					leftStickX = this.leftStickX,
					leftStickY = this.leftStickY,
					rightStickX = this.rightStickX,
					rightStickY = this.rightStickY,
					leftTrigger = this.leftTrigger,
					rightTrigger = this.rightTrigger,
					buttons0 = this.buttons0,
					buttons1 = this.buttons1,
					buttons2 = (this.buttons2 & 7)
				};
			}

			// Token: 0x040008A7 RID: 2215
			public const int ExpectedReportId = 49;

			// Token: 0x040008A8 RID: 2216
			[FieldOffset(0)]
			public byte reportId;

			// Token: 0x040008A9 RID: 2217
			[FieldOffset(2)]
			public byte leftStickX;

			// Token: 0x040008AA RID: 2218
			[FieldOffset(3)]
			public byte leftStickY;

			// Token: 0x040008AB RID: 2219
			[FieldOffset(4)]
			public byte rightStickX;

			// Token: 0x040008AC RID: 2220
			[FieldOffset(5)]
			public byte rightStickY;

			// Token: 0x040008AD RID: 2221
			[FieldOffset(6)]
			public byte leftTrigger;

			// Token: 0x040008AE RID: 2222
			[FieldOffset(7)]
			public byte rightTrigger;

			// Token: 0x040008AF RID: 2223
			[FieldOffset(9)]
			public byte buttons0;

			// Token: 0x040008B0 RID: 2224
			[FieldOffset(10)]
			public byte buttons1;

			// Token: 0x040008B1 RID: 2225
			[FieldOffset(11)]
			public byte buttons2;
		}

		// Token: 0x0200015C RID: 348
		[StructLayout(LayoutKind.Explicit)]
		internal struct DualSenseHIDMinimalInputReport
		{
			// Token: 0x06000F2A RID: 3882 RVA: 0x0004C9D4 File Offset: 0x0004ABD4
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public DualSenseHIDInputReport ToHIDInputReport()
			{
				return new DualSenseHIDInputReport
				{
					leftStickX = this.leftStickX,
					leftStickY = this.leftStickY,
					rightStickX = this.rightStickX,
					rightStickY = this.rightStickY,
					leftTrigger = this.leftTrigger,
					rightTrigger = this.rightTrigger,
					buttons0 = this.buttons0,
					buttons1 = this.buttons1,
					buttons2 = (this.buttons2 & 3)
				};
			}

			// Token: 0x040008B2 RID: 2226
			public static int ExpectedSize1 = 10;

			// Token: 0x040008B3 RID: 2227
			public static int ExpectedSize2 = 78;

			// Token: 0x040008B4 RID: 2228
			[FieldOffset(0)]
			public byte reportId;

			// Token: 0x040008B5 RID: 2229
			[FieldOffset(1)]
			public byte leftStickX;

			// Token: 0x040008B6 RID: 2230
			[FieldOffset(2)]
			public byte leftStickY;

			// Token: 0x040008B7 RID: 2231
			[FieldOffset(3)]
			public byte rightStickX;

			// Token: 0x040008B8 RID: 2232
			[FieldOffset(4)]
			public byte rightStickY;

			// Token: 0x040008B9 RID: 2233
			[FieldOffset(5)]
			public byte buttons0;

			// Token: 0x040008BA RID: 2234
			[FieldOffset(6)]
			public byte buttons1;

			// Token: 0x040008BB RID: 2235
			[FieldOffset(7)]
			public byte buttons2;

			// Token: 0x040008BC RID: 2236
			[FieldOffset(8)]
			public byte leftTrigger;

			// Token: 0x040008BD RID: 2237
			[FieldOffset(9)]
			public byte rightTrigger;
		}
	}
}
