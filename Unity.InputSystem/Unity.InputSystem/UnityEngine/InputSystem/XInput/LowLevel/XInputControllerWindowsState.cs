using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.XInput.LowLevel
{
	// Token: 0x02000105 RID: 261
	[StructLayout(LayoutKind.Explicit, Size = 4)]
	internal struct XInputControllerWindowsState : IInputStateTypeInfo
	{
		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000C9B RID: 3227 RVA: 0x0003FB3B File Offset: 0x0003DD3B
		public FourCC format
		{
			get
			{
				return new FourCC('X', 'I', 'N', 'P');
			}
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x0003FB4A File Offset: 0x0003DD4A
		public XInputControllerWindowsState WithButton(XInputControllerWindowsState.Button button)
		{
			this.buttons |= (ushort)(1 << (int)button);
			return this;
		}

		// Token: 0x040005DC RID: 1500
		[InputControl(name = "dpad", layout = "Dpad", sizeInBits = 4U, bit = 0U)]
		[InputControl(name = "dpad/up", bit = 0U)]
		[InputControl(name = "dpad/down", bit = 1U)]
		[InputControl(name = "dpad/left", bit = 2U)]
		[InputControl(name = "dpad/right", bit = 3U)]
		[InputControl(name = "start", bit = 4U, displayName = "Start")]
		[InputControl(name = "select", bit = 5U, displayName = "Select")]
		[InputControl(name = "leftStickPress", bit = 6U)]
		[InputControl(name = "rightStickPress", bit = 7U)]
		[InputControl(name = "leftShoulder", bit = 8U)]
		[InputControl(name = "rightShoulder", bit = 9U)]
		[InputControl(name = "buttonSouth", bit = 12U, displayName = "A")]
		[InputControl(name = "buttonEast", bit = 13U, displayName = "B")]
		[InputControl(name = "buttonWest", bit = 14U, displayName = "X")]
		[InputControl(name = "buttonNorth", bit = 15U, displayName = "Y")]
		[FieldOffset(0)]
		public ushort buttons;

		// Token: 0x040005DD RID: 1501
		[InputControl(name = "leftTrigger", format = "BYTE")]
		[FieldOffset(2)]
		public byte leftTrigger;

		// Token: 0x040005DE RID: 1502
		[InputControl(name = "rightTrigger", format = "BYTE")]
		[FieldOffset(3)]
		public byte rightTrigger;

		// Token: 0x040005DF RID: 1503
		[InputControl(name = "leftStick", layout = "Stick", format = "VC2S")]
		[InputControl(name = "leftStick/x", offset = 0U, format = "SHRT", parameters = "clamp=false,invert=false,normalize=false")]
		[InputControl(name = "leftStick/left", offset = 0U, format = "SHRT")]
		[InputControl(name = "leftStick/right", offset = 0U, format = "SHRT")]
		[InputControl(name = "leftStick/y", offset = 2U, format = "SHRT", parameters = "clamp=false,invert=false,normalize=false")]
		[InputControl(name = "leftStick/up", offset = 2U, format = "SHRT")]
		[InputControl(name = "leftStick/down", offset = 2U, format = "SHRT")]
		[FieldOffset(4)]
		public short leftStickX;

		// Token: 0x040005E0 RID: 1504
		[FieldOffset(6)]
		public short leftStickY;

		// Token: 0x040005E1 RID: 1505
		[InputControl(name = "rightStick", layout = "Stick", format = "VC2S")]
		[InputControl(name = "rightStick/x", offset = 0U, format = "SHRT", parameters = "clamp=false,invert=false,normalize=false")]
		[InputControl(name = "rightStick/left", offset = 0U, format = "SHRT")]
		[InputControl(name = "rightStick/right", offset = 0U, format = "SHRT")]
		[InputControl(name = "rightStick/y", offset = 2U, format = "SHRT", parameters = "clamp=false,invert=false,normalize=false")]
		[InputControl(name = "rightStick/up", offset = 2U, format = "SHRT")]
		[InputControl(name = "rightStick/down", offset = 2U, format = "SHRT")]
		[FieldOffset(8)]
		public short rightStickX;

		// Token: 0x040005E2 RID: 1506
		[FieldOffset(10)]
		public short rightStickY;

		// Token: 0x02000106 RID: 262
		public enum Button
		{
			// Token: 0x040005E4 RID: 1508
			DPadUp,
			// Token: 0x040005E5 RID: 1509
			DPadDown,
			// Token: 0x040005E6 RID: 1510
			DPadLeft,
			// Token: 0x040005E7 RID: 1511
			DPadRight,
			// Token: 0x040005E8 RID: 1512
			Start,
			// Token: 0x040005E9 RID: 1513
			Select,
			// Token: 0x040005EA RID: 1514
			LeftThumbstickPress,
			// Token: 0x040005EB RID: 1515
			RightThumbstickPress,
			// Token: 0x040005EC RID: 1516
			LeftShoulder,
			// Token: 0x040005ED RID: 1517
			RightShoulder,
			// Token: 0x040005EE RID: 1518
			A = 12,
			// Token: 0x040005EF RID: 1519
			B,
			// Token: 0x040005F0 RID: 1520
			X,
			// Token: 0x040005F1 RID: 1521
			Y
		}
	}
}
