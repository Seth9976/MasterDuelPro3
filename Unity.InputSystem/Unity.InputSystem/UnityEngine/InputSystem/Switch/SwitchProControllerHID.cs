using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Switch.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Switch
{
	// Token: 0x02000124 RID: 292
	[InputControlLayout(stateType = typeof(SwitchProControllerHIDInputState), displayName = "Switch Pro Controller")]
	public class SwitchProControllerHID : Gamepad, IInputStateCallbackReceiver, IEventPreProcessor
	{
		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000DFC RID: 3580 RVA: 0x000462E9 File Offset: 0x000444E9
		// (set) Token: 0x06000DFD RID: 3581 RVA: 0x000462F1 File Offset: 0x000444F1
		[InputControl(name = "capture", displayName = "Capture")]
		public ButtonControl captureButton { get; protected set; }

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000DFE RID: 3582 RVA: 0x000462FA File Offset: 0x000444FA
		// (set) Token: 0x06000DFF RID: 3583 RVA: 0x00046302 File Offset: 0x00044502
		[InputControl(name = "home", displayName = "Home")]
		public ButtonControl homeButton { get; protected set; }

		// Token: 0x06000E00 RID: 3584 RVA: 0x0004630B File Offset: 0x0004450B
		protected override void OnAdded()
		{
			base.OnAdded();
			this.captureButton = base.GetChildControl<ButtonControl>("capture");
			this.homeButton = base.GetChildControl<ButtonControl>("home");
			this.HandshakeRestart();
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x0004633B File Offset: 0x0004453B
		private void HandshakeRestart()
		{
			this.m_HandshakeStepIndex = -1;
			this.m_HandshakeTimer = InputRuntime.s_Instance.currentTime;
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x00046354 File Offset: 0x00044554
		private void HandshakeTick()
		{
			double currentTime = InputRuntime.s_Instance.currentTime;
			if (currentTime >= this.m_LastUpdateTimeInternal + 2.0 && currentTime >= this.m_HandshakeTimer + 2.0)
			{
				this.m_HandshakeStepIndex = 0;
			}
			else
			{
				if (this.m_HandshakeStepIndex + 1 >= SwitchProControllerHID.s_HandshakeSequence.Length)
				{
					return;
				}
				if (currentTime <= this.m_HandshakeTimer + 0.1)
				{
					return;
				}
				this.m_HandshakeStepIndex++;
			}
			this.m_HandshakeTimer = currentTime;
			SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType command = SwitchProControllerHID.s_HandshakeSequence[this.m_HandshakeStepIndex];
			SwitchProControllerHID.SwitchMagicOutputHIDBluetooth commandBt = SwitchProControllerHID.SwitchMagicOutputHIDBluetooth.Create(command);
			if (base.ExecuteCommand<SwitchProControllerHID.SwitchMagicOutputHIDBluetooth>(ref commandBt) > 0L)
			{
				return;
			}
			SwitchProControllerHID.SwitchMagicOutputHIDUSB commandUsb = SwitchProControllerHID.SwitchMagicOutputHIDUSB.Create(command);
			base.ExecuteCommand<SwitchProControllerHID.SwitchMagicOutputHIDUSB>(ref commandUsb);
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x00046409 File Offset: 0x00044609
		public void OnNextUpdate()
		{
			this.HandshakeTick();
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x00046414 File Offset: 0x00044614
		public unsafe void OnStateEvent(InputEventPtr eventPtr)
		{
			if (eventPtr.type == 1398030676 && eventPtr.stateFormat == SwitchProControllerHIDInputState.Format)
			{
				SwitchProControllerHIDInputState* currentState = (SwitchProControllerHIDInputState*)((byte*)base.currentStatePtr + this.m_StateBlock.byteOffset);
				SwitchProControllerHIDInputState* newState = (SwitchProControllerHIDInputState*)StateEvent.FromUnchecked(eventPtr)->state;
				if (newState->leftStickX >= 120 && newState->leftStickX <= 135 && newState->leftStickY >= 120 && newState->leftStickY <= 135 && newState->rightStickX >= 120 && newState->rightStickX <= 135 && newState->rightStickY >= 120 && newState->rightStickY <= 135 && newState->buttons1 == currentState->buttons1 && newState->buttons2 == currentState->buttons2)
				{
					InputSystem.s_Manager.DontMakeCurrentlyUpdatingDeviceCurrent();
				}
			}
			InputState.Change(this, eventPtr, InputUpdateType.None);
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x0001751C File Offset: 0x0001571C
		public bool GetStateOffsetForEvent(InputControl control, InputEventPtr eventPtr, ref uint offset)
		{
			return false;
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x00046504 File Offset: 0x00044704
		public unsafe bool PreProcessEvent(InputEventPtr eventPtr)
		{
			if (eventPtr.type == 1145852993)
			{
				return DeltaStateEvent.FromUnchecked(eventPtr)->stateFormat == SwitchProControllerHIDInputState.Format;
			}
			if (eventPtr.type != 1398030676)
			{
				return true;
			}
			StateEvent* stateEvent = StateEvent.FromUnchecked(eventPtr);
			uint size = stateEvent->stateSizeInBytes;
			if (stateEvent->stateFormat == SwitchProControllerHIDInputState.Format)
			{
				return true;
			}
			if (stateEvent->stateFormat != SwitchProControllerHID.SwitchHIDGenericInputReport.Format || (ulong)size < (ulong)((long)sizeof(SwitchProControllerHID.SwitchHIDGenericInputReport)))
			{
				return false;
			}
			SwitchProControllerHID.SwitchHIDGenericInputReport* genericReport = (SwitchProControllerHID.SwitchHIDGenericInputReport*)stateEvent->state;
			if (genericReport->reportId == 63 && size >= 12U)
			{
				SwitchProControllerHIDInputState data = ((SwitchProControllerHID.SwitchSimpleInputReport*)stateEvent->state)->ToHIDInputReport();
				*(SwitchProControllerHIDInputState*)stateEvent->state = data;
				stateEvent->stateFormat = SwitchProControllerHIDInputState.Format;
				return true;
			}
			if (genericReport->reportId == 48 && size >= 25U)
			{
				SwitchProControllerHIDInputState data2 = ((SwitchProControllerHID.SwitchFullInputReport*)stateEvent->state)->ToHIDInputReport();
				*(SwitchProControllerHIDInputState*)stateEvent->state = data2;
				stateEvent->stateFormat = SwitchProControllerHIDInputState.Format;
				return true;
			}
			if (size == 8U || size == 9U)
			{
				int bugOffset = ((size == 9U) ? 1 : 0);
				SwitchProControllerHIDInputState data3 = ((SwitchProControllerHID.SwitchInputOnlyReport*)((byte*)stateEvent->state + bugOffset))->ToHIDInputReport();
				*(SwitchProControllerHIDInputState*)stateEvent->state = data3;
				stateEvent->stateFormat = SwitchProControllerHIDInputState.Format;
				return true;
			}
			return false;
		}

		// Token: 0x040006CB RID: 1739
		private static readonly SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType[] s_HandshakeSequence = new SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType[]
		{
			SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType.Status,
			SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType.Handshake,
			SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType.Highspeed,
			SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType.Handshake,
			SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType.ForceUSB
		};

		// Token: 0x040006CC RID: 1740
		private int m_HandshakeStepIndex;

		// Token: 0x040006CD RID: 1741
		private double m_HandshakeTimer;

		// Token: 0x040006CE RID: 1742
		internal const byte JitterMaskLow = 120;

		// Token: 0x040006CF RID: 1743
		internal const byte JitterMaskHigh = 135;

		// Token: 0x02000125 RID: 293
		[StructLayout(LayoutKind.Explicit, Size = 7)]
		private struct SwitchInputOnlyReport
		{
			// Token: 0x06000E09 RID: 3593 RVA: 0x00046664 File Offset: 0x00044864
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public SwitchProControllerHIDInputState ToHIDInputReport()
			{
				SwitchProControllerHIDInputState state = new SwitchProControllerHIDInputState
				{
					leftStickX = this.leftX,
					leftStickY = this.leftY,
					rightStickX = this.rightX,
					rightStickY = this.rightY
				};
				state.Set(SwitchProControllerHIDInputState.Button.West, (this.buttons0 & 1) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.South, (this.buttons0 & 2) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.East, (this.buttons0 & 4) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.North, (this.buttons0 & 8) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.L, (this.buttons0 & 16) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.R, (this.buttons0 & 32) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.ZL, (this.buttons0 & 64) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.ZR, (this.buttons0 & 128) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.Minus, (this.buttons1 & 1) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.Plus, (this.buttons1 & 2) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.StickL, (this.buttons1 & 4) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.StickR, (this.buttons1 & 8) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.Home, (this.buttons1 & 16) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.Capture, (this.buttons1 & 32) > 0);
				bool left = false;
				bool up = false;
				bool right = false;
				bool down = false;
				switch (this.hat)
				{
				case 0:
					up = true;
					break;
				case 1:
					up = true;
					right = true;
					break;
				case 2:
					right = true;
					break;
				case 3:
					down = true;
					right = true;
					break;
				case 4:
					down = true;
					break;
				case 5:
					down = true;
					left = true;
					break;
				case 6:
					left = true;
					break;
				case 7:
					up = true;
					left = true;
					break;
				}
				state.Set(SwitchProControllerHIDInputState.Button.Left, left);
				state.Set(SwitchProControllerHIDInputState.Button.Up, up);
				state.Set(SwitchProControllerHIDInputState.Button.Right, right);
				state.Set(SwitchProControllerHIDInputState.Button.Down, down);
				return state;
			}

			// Token: 0x040006D0 RID: 1744
			public const int kSize = 7;

			// Token: 0x040006D1 RID: 1745
			[FieldOffset(0)]
			public byte buttons0;

			// Token: 0x040006D2 RID: 1746
			[FieldOffset(1)]
			public byte buttons1;

			// Token: 0x040006D3 RID: 1747
			[FieldOffset(2)]
			public byte hat;

			// Token: 0x040006D4 RID: 1748
			[FieldOffset(3)]
			public byte leftX;

			// Token: 0x040006D5 RID: 1749
			[FieldOffset(4)]
			public byte leftY;

			// Token: 0x040006D6 RID: 1750
			[FieldOffset(5)]
			public byte rightX;

			// Token: 0x040006D7 RID: 1751
			[FieldOffset(6)]
			public byte rightY;
		}

		// Token: 0x02000126 RID: 294
		[StructLayout(LayoutKind.Explicit, Size = 12)]
		private struct SwitchSimpleInputReport
		{
			// Token: 0x06000E0A RID: 3594 RVA: 0x00046858 File Offset: 0x00044A58
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public SwitchProControllerHIDInputState ToHIDInputReport()
			{
				byte leftXByte = (byte)NumberHelpers.RemapUIntBitsToNormalizeFloatToUIntBits((uint)this.leftX, 16U, 8U);
				byte leftYByte = (byte)NumberHelpers.RemapUIntBitsToNormalizeFloatToUIntBits((uint)this.leftY, 16U, 8U);
				byte rightXByte = (byte)NumberHelpers.RemapUIntBitsToNormalizeFloatToUIntBits((uint)this.rightX, 16U, 8U);
				byte rightYByte = (byte)NumberHelpers.RemapUIntBitsToNormalizeFloatToUIntBits((uint)this.rightY, 16U, 8U);
				SwitchProControllerHIDInputState state = new SwitchProControllerHIDInputState
				{
					leftStickX = leftXByte,
					leftStickY = leftYByte,
					rightStickX = rightXByte,
					rightStickY = rightYByte
				};
				state.Set(SwitchProControllerHIDInputState.Button.South, (this.buttons0 & 1) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.East, (this.buttons0 & 2) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.West, (this.buttons0 & 4) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.North, (this.buttons0 & 8) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.L, (this.buttons0 & 16) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.R, (this.buttons0 & 32) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.ZL, (this.buttons0 & 64) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.ZR, (this.buttons0 & 128) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.Minus, (this.buttons1 & 1) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.Plus, (this.buttons1 & 2) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.StickL, (this.buttons1 & 4) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.StickR, (this.buttons1 & 8) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.Home, (this.buttons1 & 16) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.Capture, (this.buttons1 & 32) > 0);
				bool left = false;
				bool up = false;
				bool right = false;
				bool down = false;
				switch (this.hat)
				{
				case 0:
					up = true;
					break;
				case 1:
					up = true;
					right = true;
					break;
				case 2:
					right = true;
					break;
				case 3:
					down = true;
					right = true;
					break;
				case 4:
					down = true;
					break;
				case 5:
					down = true;
					left = true;
					break;
				case 6:
					left = true;
					break;
				case 7:
					up = true;
					left = true;
					break;
				}
				state.Set(SwitchProControllerHIDInputState.Button.Left, left);
				state.Set(SwitchProControllerHIDInputState.Button.Up, up);
				state.Set(SwitchProControllerHIDInputState.Button.Right, right);
				state.Set(SwitchProControllerHIDInputState.Button.Down, down);
				return state;
			}

			// Token: 0x040006D8 RID: 1752
			public const int kSize = 12;

			// Token: 0x040006D9 RID: 1753
			public const byte ExpectedReportId = 63;

			// Token: 0x040006DA RID: 1754
			[FieldOffset(0)]
			public byte reportId;

			// Token: 0x040006DB RID: 1755
			[FieldOffset(1)]
			public byte buttons0;

			// Token: 0x040006DC RID: 1756
			[FieldOffset(2)]
			public byte buttons1;

			// Token: 0x040006DD RID: 1757
			[FieldOffset(3)]
			public byte hat;

			// Token: 0x040006DE RID: 1758
			[FieldOffset(4)]
			public ushort leftX;

			// Token: 0x040006DF RID: 1759
			[FieldOffset(6)]
			public ushort leftY;

			// Token: 0x040006E0 RID: 1760
			[FieldOffset(8)]
			public ushort rightX;

			// Token: 0x040006E1 RID: 1761
			[FieldOffset(10)]
			public ushort rightY;
		}

		// Token: 0x02000127 RID: 295
		[StructLayout(LayoutKind.Explicit, Size = 25)]
		private struct SwitchFullInputReport
		{
			// Token: 0x06000E0B RID: 3595 RVA: 0x00046A88 File Offset: 0x00044C88
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public SwitchProControllerHIDInputState ToHIDInputReport()
			{
				uint leftXRaw = (uint)((int)this.left0 | ((int)(this.left1 & 15) << 8));
				uint leftYRaw = (uint)(((this.left1 & 240) >> 4) | ((int)this.left2 << 4));
				uint num = (uint)((int)this.right0 | ((int)(this.right1 & 15) << 8));
				uint rightYRaw = (uint)(((this.right1 & 240) >> 4) | ((int)this.right2 << 4));
				byte leftXByte = (byte)NumberHelpers.RemapUIntBitsToNormalizeFloatToUIntBits(leftXRaw, 12U, 8U);
				byte leftYByte = byte.MaxValue - (byte)NumberHelpers.RemapUIntBitsToNormalizeFloatToUIntBits(leftYRaw, 12U, 8U);
				byte rightXByte = (byte)NumberHelpers.RemapUIntBitsToNormalizeFloatToUIntBits(num, 12U, 8U);
				byte rightYByte = byte.MaxValue - (byte)NumberHelpers.RemapUIntBitsToNormalizeFloatToUIntBits(rightYRaw, 12U, 8U);
				SwitchProControllerHIDInputState state = new SwitchProControllerHIDInputState
				{
					leftStickX = leftXByte,
					leftStickY = leftYByte,
					rightStickX = rightXByte,
					rightStickY = rightYByte
				};
				state.Set(SwitchProControllerHIDInputState.Button.West, (this.buttons0 & 1) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.North, (this.buttons0 & 2) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.South, (this.buttons0 & 4) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.East, (this.buttons0 & 8) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.R, (this.buttons0 & 64) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.ZR, (this.buttons0 & 128) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.Minus, (this.buttons1 & 1) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.Plus, (this.buttons1 & 2) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.StickR, (this.buttons1 & 4) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.StickL, (this.buttons1 & 8) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.Home, (this.buttons1 & 16) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.Capture, (this.buttons1 & 32) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.Down, (this.buttons2 & 1) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.Up, (this.buttons2 & 2) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.Right, (this.buttons2 & 4) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.Left, (this.buttons2 & 8) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.L, (this.buttons2 & 64) > 0);
				state.Set(SwitchProControllerHIDInputState.Button.ZL, (this.buttons2 & 128) > 0);
				return state;
			}

			// Token: 0x040006E2 RID: 1762
			public const int kSize = 25;

			// Token: 0x040006E3 RID: 1763
			public const byte ExpectedReportId = 48;

			// Token: 0x040006E4 RID: 1764
			[FieldOffset(0)]
			public byte reportId;

			// Token: 0x040006E5 RID: 1765
			[FieldOffset(3)]
			public byte buttons0;

			// Token: 0x040006E6 RID: 1766
			[FieldOffset(4)]
			public byte buttons1;

			// Token: 0x040006E7 RID: 1767
			[FieldOffset(5)]
			public byte buttons2;

			// Token: 0x040006E8 RID: 1768
			[FieldOffset(6)]
			public byte left0;

			// Token: 0x040006E9 RID: 1769
			[FieldOffset(7)]
			public byte left1;

			// Token: 0x040006EA RID: 1770
			[FieldOffset(8)]
			public byte left2;

			// Token: 0x040006EB RID: 1771
			[FieldOffset(9)]
			public byte right0;

			// Token: 0x040006EC RID: 1772
			[FieldOffset(10)]
			public byte right1;

			// Token: 0x040006ED RID: 1773
			[FieldOffset(11)]
			public byte right2;
		}

		// Token: 0x02000128 RID: 296
		[StructLayout(LayoutKind.Explicit)]
		private struct SwitchHIDGenericInputReport
		{
			// Token: 0x170003BA RID: 954
			// (get) Token: 0x06000E0C RID: 3596 RVA: 0x00046CC2 File Offset: 0x00044EC2
			public static FourCC Format
			{
				get
				{
					return new FourCC('H', 'I', 'D', ' ');
				}
			}

			// Token: 0x040006EE RID: 1774
			[FieldOffset(0)]
			public byte reportId;
		}

		// Token: 0x02000129 RID: 297
		[StructLayout(LayoutKind.Explicit, Size = 49)]
		internal struct SwitchMagicOutputReport
		{
			// Token: 0x040006EF RID: 1775
			public const int kSize = 49;

			// Token: 0x040006F0 RID: 1776
			public const byte ExpectedReplyInputReportId = 129;

			// Token: 0x040006F1 RID: 1777
			[FieldOffset(0)]
			public byte reportType;

			// Token: 0x040006F2 RID: 1778
			[FieldOffset(1)]
			public byte commandId;

			// Token: 0x0200012A RID: 298
			internal enum ReportType
			{
				// Token: 0x040006F4 RID: 1780
				Magic = 128
			}

			// Token: 0x0200012B RID: 299
			public enum CommandIdType
			{
				// Token: 0x040006F6 RID: 1782
				Status = 1,
				// Token: 0x040006F7 RID: 1783
				Handshake,
				// Token: 0x040006F8 RID: 1784
				Highspeed,
				// Token: 0x040006F9 RID: 1785
				ForceUSB
			}
		}

		// Token: 0x0200012C RID: 300
		[StructLayout(LayoutKind.Explicit, Size = 57)]
		internal struct SwitchMagicOutputHIDBluetooth : IInputDeviceCommandInfo
		{
			// Token: 0x170003BB RID: 955
			// (get) Token: 0x06000E0D RID: 3597 RVA: 0x00046CD1 File Offset: 0x00044ED1
			public static FourCC Type
			{
				get
				{
					return new FourCC('H', 'I', 'D', 'O');
				}
			}

			// Token: 0x170003BC RID: 956
			// (get) Token: 0x06000E0E RID: 3598 RVA: 0x00046CE0 File Offset: 0x00044EE0
			public FourCC typeStatic
			{
				get
				{
					return SwitchProControllerHID.SwitchMagicOutputHIDBluetooth.Type;
				}
			}

			// Token: 0x06000E0F RID: 3599 RVA: 0x00046CE8 File Offset: 0x00044EE8
			public static SwitchProControllerHID.SwitchMagicOutputHIDBluetooth Create(SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType type)
			{
				return new SwitchProControllerHID.SwitchMagicOutputHIDBluetooth
				{
					baseCommand = new InputDeviceCommand(SwitchProControllerHID.SwitchMagicOutputHIDBluetooth.Type, 57),
					report = new SwitchProControllerHID.SwitchMagicOutputReport
					{
						reportType = 128,
						commandId = (byte)type
					}
				};
			}

			// Token: 0x040006FA RID: 1786
			public const int kSize = 57;

			// Token: 0x040006FB RID: 1787
			[FieldOffset(0)]
			public InputDeviceCommand baseCommand;

			// Token: 0x040006FC RID: 1788
			[FieldOffset(8)]
			public SwitchProControllerHID.SwitchMagicOutputReport report;
		}

		// Token: 0x0200012D RID: 301
		[StructLayout(LayoutKind.Explicit, Size = 72)]
		internal struct SwitchMagicOutputHIDUSB : IInputDeviceCommandInfo
		{
			// Token: 0x170003BD RID: 957
			// (get) Token: 0x06000E10 RID: 3600 RVA: 0x00046CD1 File Offset: 0x00044ED1
			public static FourCC Type
			{
				get
				{
					return new FourCC('H', 'I', 'D', 'O');
				}
			}

			// Token: 0x170003BE RID: 958
			// (get) Token: 0x06000E11 RID: 3601 RVA: 0x00046D36 File Offset: 0x00044F36
			public FourCC typeStatic
			{
				get
				{
					return SwitchProControllerHID.SwitchMagicOutputHIDUSB.Type;
				}
			}

			// Token: 0x06000E12 RID: 3602 RVA: 0x00046D40 File Offset: 0x00044F40
			public static SwitchProControllerHID.SwitchMagicOutputHIDUSB Create(SwitchProControllerHID.SwitchMagicOutputReport.CommandIdType type)
			{
				return new SwitchProControllerHID.SwitchMagicOutputHIDUSB
				{
					baseCommand = new InputDeviceCommand(SwitchProControllerHID.SwitchMagicOutputHIDUSB.Type, 72),
					report = new SwitchProControllerHID.SwitchMagicOutputReport
					{
						reportType = 128,
						commandId = (byte)type
					}
				};
			}

			// Token: 0x040006FD RID: 1789
			public const int kSize = 72;

			// Token: 0x040006FE RID: 1790
			[FieldOffset(0)]
			public InputDeviceCommand baseCommand;

			// Token: 0x040006FF RID: 1791
			[FieldOffset(8)]
			public SwitchProControllerHID.SwitchMagicOutputReport report;
		}
	}
}
