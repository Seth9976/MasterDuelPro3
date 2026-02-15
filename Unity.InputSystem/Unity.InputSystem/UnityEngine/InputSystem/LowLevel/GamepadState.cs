using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x0200018D RID: 397
	[StructLayout(LayoutKind.Explicit, Size = 28)]
	public struct GamepadState : IInputStateTypeInfo
	{
		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x0004DCB1 File Offset: 0x0004BEB1
		public static FourCC Format
		{
			get
			{
				return new FourCC('G', 'P', 'A', 'D');
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000FB3 RID: 4019 RVA: 0x0004DCC0 File Offset: 0x0004BEC0
		public FourCC format
		{
			get
			{
				return GamepadState.Format;
			}
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x0004DCC8 File Offset: 0x0004BEC8
		public GamepadState(params GamepadButton[] buttons)
		{
			this = default(GamepadState);
			if (buttons == null)
			{
				throw new ArgumentNullException("buttons");
			}
			foreach (GamepadButton button in buttons)
			{
				uint bit = 1U << (int)button;
				this.buttons |= bit;
			}
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x0004DD14 File Offset: 0x0004BF14
		public GamepadState WithButton(GamepadButton button, bool value = true)
		{
			uint bit = 1U << (int)button;
			if (value)
			{
				this.buttons |= bit;
			}
			else
			{
				this.buttons &= ~bit;
			}
			return this;
		}

		// Token: 0x04000963 RID: 2403
		internal const string ButtonSouthShortDisplayName = "A";

		// Token: 0x04000964 RID: 2404
		internal const string ButtonNorthShortDisplayName = "Y";

		// Token: 0x04000965 RID: 2405
		internal const string ButtonWestShortDisplayName = "X";

		// Token: 0x04000966 RID: 2406
		internal const string ButtonEastShortDisplayName = "B";

		// Token: 0x04000967 RID: 2407
		[InputControl(name = "dpad", layout = "Dpad", usage = "Hatswitch", displayName = "D-Pad", format = "BIT", sizeInBits = 4U, bit = 0U)]
		[InputControl(name = "buttonSouth", layout = "Button", bit = 6U, usages = new string[] { "PrimaryAction", "Submit" }, aliases = new string[] { "a", "cross" }, displayName = "Button South", shortDisplayName = "A")]
		[InputControl(name = "buttonWest", layout = "Button", bit = 7U, usage = "SecondaryAction", aliases = new string[] { "x", "square" }, displayName = "Button West", shortDisplayName = "X")]
		[InputControl(name = "buttonNorth", layout = "Button", bit = 4U, aliases = new string[] { "y", "triangle" }, displayName = "Button North", shortDisplayName = "Y")]
		[InputControl(name = "buttonEast", layout = "Button", bit = 5U, usages = new string[] { "Back", "Cancel" }, aliases = new string[] { "b", "circle" }, displayName = "Button East", shortDisplayName = "B")]
		[InputControl(name = "leftStickPress", layout = "Button", bit = 8U, displayName = "Left Stick Press")]
		[InputControl(name = "rightStickPress", layout = "Button", bit = 9U, displayName = "Right Stick Press")]
		[InputControl(name = "leftShoulder", layout = "Button", bit = 10U, displayName = "Left Shoulder", shortDisplayName = "LB")]
		[InputControl(name = "rightShoulder", layout = "Button", bit = 11U, displayName = "Right Shoulder", shortDisplayName = "RB")]
		[InputControl(name = "start", layout = "Button", bit = 12U, usage = "Menu", displayName = "Start")]
		[InputControl(name = "select", layout = "Button", bit = 13U, displayName = "Select")]
		[FieldOffset(0)]
		public uint buttons;

		// Token: 0x04000968 RID: 2408
		[InputControl(layout = "Stick", usage = "Primary2DMotion", processors = "stickDeadzone", displayName = "Left Stick", shortDisplayName = "LS")]
		[FieldOffset(4)]
		public Vector2 leftStick;

		// Token: 0x04000969 RID: 2409
		[InputControl(layout = "Stick", usage = "Secondary2DMotion", processors = "stickDeadzone", displayName = "Right Stick", shortDisplayName = "RS")]
		[FieldOffset(12)]
		public Vector2 rightStick;

		// Token: 0x0400096A RID: 2410
		[InputControl(layout = "Button", format = "FLT", usage = "SecondaryTrigger", displayName = "Left Trigger", shortDisplayName = "LT")]
		[FieldOffset(20)]
		public float leftTrigger;

		// Token: 0x0400096B RID: 2411
		[InputControl(layout = "Button", format = "FLT", usage = "SecondaryTrigger", displayName = "Right Trigger", shortDisplayName = "RT")]
		[FieldOffset(24)]
		public float rightTrigger;
	}
}
