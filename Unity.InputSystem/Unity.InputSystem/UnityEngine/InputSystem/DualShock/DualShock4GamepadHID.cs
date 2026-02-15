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
	// Token: 0x0200015D RID: 349
	[InputControlLayout(stateType = typeof(DualShock4HIDInputReport), hideInUI = true, isNoisy = true)]
	public class DualShock4GamepadHID : DualShockGamepad, IEventPreProcessor, IInputStateCallbackReceiver
	{
		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000F2C RID: 3884 RVA: 0x0004CA72 File Offset: 0x0004AC72
		// (set) Token: 0x06000F2D RID: 3885 RVA: 0x0004CA7A File Offset: 0x0004AC7A
		public ButtonControl leftTriggerButton { get; protected set; }

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000F2E RID: 3886 RVA: 0x0004CA83 File Offset: 0x0004AC83
		// (set) Token: 0x06000F2F RID: 3887 RVA: 0x0004CA8B File Offset: 0x0004AC8B
		public ButtonControl rightTriggerButton { get; protected set; }

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000F30 RID: 3888 RVA: 0x0004CA94 File Offset: 0x0004AC94
		// (set) Token: 0x06000F31 RID: 3889 RVA: 0x0004CA9C File Offset: 0x0004AC9C
		public ButtonControl playStationButton { get; protected set; }

		// Token: 0x06000F32 RID: 3890 RVA: 0x0004CAA5 File Offset: 0x0004ACA5
		protected override void FinishSetup()
		{
			this.leftTriggerButton = base.GetChildControl<ButtonControl>("leftTriggerButton");
			this.rightTriggerButton = base.GetChildControl<ButtonControl>("rightTriggerButton");
			this.playStationButton = base.GetChildControl<ButtonControl>("systemButton");
			base.FinishSetup();
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x0004CAE0 File Offset: 0x0004ACE0
		public override void PauseHaptics()
		{
			if (this.m_LowFrequencyMotorSpeed == null && this.m_HighFrequenceyMotorSpeed == null && this.m_LightBarColor == null)
			{
				return;
			}
			DualShockHIDOutputReport command = DualShockHIDOutputReport.Create(base.hidDescriptor.outputReportSize);
			command.SetMotorSpeeds(0f, 0f);
			if (this.m_LightBarColor != null)
			{
				command.SetColor(Color.black);
			}
			base.ExecuteCommand<DualShockHIDOutputReport>(ref command);
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x0004CB5C File Offset: 0x0004AD5C
		public override void ResetHaptics()
		{
			if (this.m_LowFrequencyMotorSpeed == null && this.m_HighFrequenceyMotorSpeed == null && this.m_LightBarColor == null)
			{
				return;
			}
			DualShockHIDOutputReport command = DualShockHIDOutputReport.Create(base.hidDescriptor.outputReportSize);
			command.SetMotorSpeeds(0f, 0f);
			if (this.m_LightBarColor != null)
			{
				command.SetColor(Color.black);
			}
			base.ExecuteCommand<DualShockHIDOutputReport>(ref command);
			this.m_HighFrequenceyMotorSpeed = null;
			this.m_LowFrequencyMotorSpeed = null;
			this.m_LightBarColor = null;
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x0004CBFC File Offset: 0x0004ADFC
		public override void ResumeHaptics()
		{
			if (this.m_LowFrequencyMotorSpeed == null && this.m_HighFrequenceyMotorSpeed == null && this.m_LightBarColor == null)
			{
				return;
			}
			DualShockHIDOutputReport command = DualShockHIDOutputReport.Create(base.hidDescriptor.outputReportSize);
			if (this.m_LowFrequencyMotorSpeed != null || this.m_HighFrequenceyMotorSpeed != null)
			{
				command.SetMotorSpeeds(this.m_LowFrequencyMotorSpeed.Value, this.m_HighFrequenceyMotorSpeed.Value);
			}
			if (this.m_LightBarColor != null)
			{
				command.SetColor(this.m_LightBarColor.Value);
			}
			base.ExecuteCommand<DualShockHIDOutputReport>(ref command);
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x0004CCA4 File Offset: 0x0004AEA4
		public override void SetLightBarColor(Color color)
		{
			DualShockHIDOutputReport command = DualShockHIDOutputReport.Create(base.hidDescriptor.outputReportSize);
			command.SetColor(color);
			base.ExecuteCommand<DualShockHIDOutputReport>(ref command);
			this.m_LightBarColor = new Color?(color);
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x0004CCE0 File Offset: 0x0004AEE0
		public override void SetMotorSpeeds(float lowFrequency, float highFrequency)
		{
			DualShockHIDOutputReport command = DualShockHIDOutputReport.Create(base.hidDescriptor.outputReportSize);
			command.SetMotorSpeeds(lowFrequency, highFrequency);
			base.ExecuteCommand<DualShockHIDOutputReport>(ref command);
			this.m_LowFrequencyMotorSpeed = new float?(lowFrequency);
			this.m_HighFrequenceyMotorSpeed = new float?(highFrequency);
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x0004CD28 File Offset: 0x0004AF28
		public bool SetMotorSpeedsAndLightBarColor(float lowFrequency, float highFrequency, Color color)
		{
			DualShockHIDOutputReport command = DualShockHIDOutputReport.Create(base.hidDescriptor.outputReportSize);
			command.SetMotorSpeeds(lowFrequency, highFrequency);
			command.SetColor(color);
			long num = base.ExecuteCommand<DualShockHIDOutputReport>(ref command);
			this.m_LowFrequencyMotorSpeed = new float?(lowFrequency);
			this.m_HighFrequenceyMotorSpeed = new float?(highFrequency);
			this.m_LightBarColor = new Color?(color);
			return num >= 0L;
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x0004CD8C File Offset: 0x0004AF8C
		unsafe bool IEventPreProcessor.PreProcessEvent(InputEventPtr eventPtr)
		{
			if (eventPtr.type != 1398030676)
			{
				return eventPtr.type != 1145852993;
			}
			StateEvent* stateEvent = StateEvent.FromUnchecked(eventPtr);
			if (stateEvent->stateFormat == DualShock4HIDInputReport.Format)
			{
				return true;
			}
			uint size = stateEvent->stateSizeInBytes;
			if (stateEvent->stateFormat != DualShock4GamepadHID.DualShock4HIDGenericInputReport.Format || (ulong)size < (ulong)((long)sizeof(DualShock4GamepadHID.DualShock4HIDGenericInputReport)))
			{
				return false;
			}
			byte* binaryData = (byte*)stateEvent->state;
			byte b = *binaryData;
			if (b != 1)
			{
				if (b - 17 > 8)
				{
					return false;
				}
				if ((binaryData[1] & 128) == 0)
				{
					return false;
				}
				if ((ulong)size < (ulong)((long)(sizeof(DualShock4GamepadHID.DualShock4HIDGenericInputReport) + 3)))
				{
					return false;
				}
				DualShock4HIDInputReport data = ((DualShock4GamepadHID.DualShock4HIDGenericInputReport*)(binaryData + 3))->ToHIDInputReport();
				*(DualShock4HIDInputReport*)stateEvent->state = data;
				stateEvent->stateFormat = DualShock4HIDInputReport.Format;
				return true;
			}
			else
			{
				if ((ulong)size < (ulong)((long)(sizeof(DualShock4GamepadHID.DualShock4HIDGenericInputReport) + 1)))
				{
					return false;
				}
				DualShock4HIDInputReport data2 = ((DualShock4GamepadHID.DualShock4HIDGenericInputReport*)(binaryData + 1))->ToHIDInputReport();
				*(DualShock4HIDInputReport*)stateEvent->state = data2;
				stateEvent->stateFormat = DualShock4HIDInputReport.Format;
				return true;
			}
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x000049FE File Offset: 0x00002BFE
		public void OnNextUpdate()
		{
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x0004CE98 File Offset: 0x0004B098
		public unsafe void OnStateEvent(InputEventPtr eventPtr)
		{
			if (eventPtr.type == 1398030676 && eventPtr.stateFormat == DualShock4HIDInputReport.Format)
			{
				DualShock4HIDInputReport* currentState = (DualShock4HIDInputReport*)((byte*)base.currentStatePtr + this.m_StateBlock.byteOffset);
				DualShock4HIDInputReport* newState = (DualShock4HIDInputReport*)StateEvent.FromUnchecked(eventPtr)->state;
				if (newState->leftStickX >= 120 && newState->leftStickX <= 135 && newState->leftStickY >= 120 && newState->leftStickY <= 135 && newState->rightStickX >= 120 && newState->rightStickX <= 135 && newState->rightStickY >= 120 && newState->rightStickY <= 135 && newState->leftTrigger == currentState->leftTrigger && newState->rightTrigger == currentState->rightTrigger && newState->buttons1 == currentState->buttons1 && newState->buttons2 == currentState->buttons2 && newState->buttons3 == currentState->buttons3)
				{
					InputSystem.s_Manager.DontMakeCurrentlyUpdatingDeviceCurrent();
				}
			}
			InputState.Change(this, eventPtr, InputUpdateType.None);
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x0001751C File Offset: 0x0001571C
		public bool GetStateOffsetForEvent(InputControl control, InputEventPtr eventPtr, ref uint offset)
		{
			return false;
		}

		// Token: 0x040008C1 RID: 2241
		private float? m_LowFrequencyMotorSpeed;

		// Token: 0x040008C2 RID: 2242
		private float? m_HighFrequenceyMotorSpeed;

		// Token: 0x040008C3 RID: 2243
		private Color? m_LightBarColor;

		// Token: 0x040008C4 RID: 2244
		internal const byte JitterMaskLow = 120;

		// Token: 0x040008C5 RID: 2245
		internal const byte JitterMaskHigh = 135;

		// Token: 0x0200015E RID: 350
		[StructLayout(LayoutKind.Explicit)]
		internal struct DualShock4HIDGenericInputReport
		{
			// Token: 0x17000415 RID: 1045
			// (get) Token: 0x06000F3E RID: 3902 RVA: 0x00046CC2 File Offset: 0x00044EC2
			public static FourCC Format
			{
				get
				{
					return new FourCC('H', 'I', 'D', ' ');
				}
			}

			// Token: 0x06000F3F RID: 3903 RVA: 0x0004CFBC File Offset: 0x0004B1BC
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public DualShock4HIDInputReport ToHIDInputReport()
			{
				return new DualShock4HIDInputReport
				{
					leftStickX = this.leftStickX,
					leftStickY = this.leftStickY,
					rightStickX = this.rightStickX,
					rightStickY = this.rightStickY,
					leftTrigger = this.leftTrigger,
					rightTrigger = this.rightTrigger,
					buttons1 = this.buttons0,
					buttons2 = this.buttons1,
					buttons3 = this.buttons2
				};
			}

			// Token: 0x040008C6 RID: 2246
			[FieldOffset(0)]
			public byte leftStickX;

			// Token: 0x040008C7 RID: 2247
			[FieldOffset(1)]
			public byte leftStickY;

			// Token: 0x040008C8 RID: 2248
			[FieldOffset(2)]
			public byte rightStickX;

			// Token: 0x040008C9 RID: 2249
			[FieldOffset(3)]
			public byte rightStickY;

			// Token: 0x040008CA RID: 2250
			[FieldOffset(4)]
			public byte buttons0;

			// Token: 0x040008CB RID: 2251
			[FieldOffset(5)]
			public byte buttons1;

			// Token: 0x040008CC RID: 2252
			[FieldOffset(6)]
			public byte buttons2;

			// Token: 0x040008CD RID: 2253
			[FieldOffset(7)]
			public byte leftTrigger;

			// Token: 0x040008CE RID: 2254
			[FieldOffset(8)]
			public byte rightTrigger;
		}
	}
}
