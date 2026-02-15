using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.DualShock.LowLevel
{
	// Token: 0x02000167 RID: 359
	[StructLayout(LayoutKind.Explicit, Size = 9)]
	internal struct DualShock4HIDInputReport : IInputStateTypeInfo
	{
		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000F52 RID: 3922 RVA: 0x0004D33E File Offset: 0x0004B53E
		public FourCC format
		{
			get
			{
				return DualShock4HIDInputReport.Format;
			}
		}

		// Token: 0x040008F0 RID: 2288
		public static FourCC Format = new FourCC('D', '4', 'V', 'S');

		// Token: 0x040008F1 RID: 2289
		[InputControl(name = "leftStick", layout = "Stick", format = "VC2B")]
		[InputControl(name = "leftStick/x", offset = 0U, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5")]
		[InputControl(name = "leftStick/left", offset = 0U, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=1,clampMin=0,clampMax=0.5,invert")]
		[InputControl(name = "leftStick/right", offset = 0U, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=1,clampMin=0.5,clampMax=1")]
		[InputControl(name = "leftStick/y", offset = 1U, format = "BYTE", parameters = "invert,normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5")]
		[InputControl(name = "leftStick/up", offset = 1U, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=1,clampMin=0,clampMax=0.5,invert")]
		[InputControl(name = "leftStick/down", offset = 1U, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=1,clampMin=0.5,clampMax=1,invert=false")]
		[FieldOffset(0)]
		public byte leftStickX;

		// Token: 0x040008F2 RID: 2290
		[FieldOffset(1)]
		public byte leftStickY;

		// Token: 0x040008F3 RID: 2291
		[InputControl(name = "rightStick", layout = "Stick", format = "VC2B")]
		[InputControl(name = "rightStick/x", offset = 0U, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5")]
		[InputControl(name = "rightStick/left", offset = 0U, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=1,clampMin=0,clampMax=0.5,invert")]
		[InputControl(name = "rightStick/right", offset = 0U, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=1,clampMin=0.5,clampMax=1")]
		[InputControl(name = "rightStick/y", offset = 1U, format = "BYTE", parameters = "invert,normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5")]
		[InputControl(name = "rightStick/up", offset = 1U, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=1,clampMin=0,clampMax=0.5,invert")]
		[InputControl(name = "rightStick/down", offset = 1U, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=1,clampMin=0.5,clampMax=1,invert=false")]
		[FieldOffset(2)]
		public byte rightStickX;

		// Token: 0x040008F4 RID: 2292
		[FieldOffset(3)]
		public byte rightStickY;

		// Token: 0x040008F5 RID: 2293
		[InputControl(name = "dpad", format = "BIT", layout = "Dpad", sizeInBits = 4U, defaultState = 8)]
		[InputControl(name = "dpad/up", format = "BIT", layout = "DiscreteButton", parameters = "minValue=7,maxValue=1,nullValue=8,wrapAtValue=7", bit = 0U, sizeInBits = 4U)]
		[InputControl(name = "dpad/right", format = "BIT", layout = "DiscreteButton", parameters = "minValue=1,maxValue=3", bit = 0U, sizeInBits = 4U)]
		[InputControl(name = "dpad/down", format = "BIT", layout = "DiscreteButton", parameters = "minValue=3,maxValue=5", bit = 0U, sizeInBits = 4U)]
		[InputControl(name = "dpad/left", format = "BIT", layout = "DiscreteButton", parameters = "minValue=5, maxValue=7", bit = 0U, sizeInBits = 4U)]
		[InputControl(name = "buttonWest", displayName = "Square", bit = 4U)]
		[InputControl(name = "buttonSouth", displayName = "Cross", bit = 5U)]
		[InputControl(name = "buttonEast", displayName = "Circle", bit = 6U)]
		[InputControl(name = "buttonNorth", displayName = "Triangle", bit = 7U)]
		[FieldOffset(4)]
		public byte buttons1;

		// Token: 0x040008F6 RID: 2294
		[InputControl(name = "leftShoulder", bit = 0U)]
		[InputControl(name = "rightShoulder", bit = 1U)]
		[InputControl(name = "leftTriggerButton", layout = "Button", bit = 2U, synthetic = true)]
		[InputControl(name = "rightTriggerButton", layout = "Button", bit = 3U, synthetic = true)]
		[InputControl(name = "select", displayName = "Share", bit = 4U)]
		[InputControl(name = "start", displayName = "Options", bit = 5U)]
		[InputControl(name = "leftStickPress", bit = 6U)]
		[InputControl(name = "rightStickPress", bit = 7U)]
		[FieldOffset(5)]
		public byte buttons2;

		// Token: 0x040008F7 RID: 2295
		[InputControl(name = "systemButton", layout = "Button", displayName = "System", bit = 0U)]
		[InputControl(name = "touchpadButton", layout = "Button", displayName = "Touchpad Press", bit = 1U)]
		[FieldOffset(6)]
		public byte buttons3;

		// Token: 0x040008F8 RID: 2296
		[InputControl(name = "leftTrigger", format = "BYTE")]
		[FieldOffset(7)]
		public byte leftTrigger;

		// Token: 0x040008F9 RID: 2297
		[InputControl(name = "rightTrigger", format = "BYTE")]
		[FieldOffset(8)]
		public byte rightTrigger;
	}
}
