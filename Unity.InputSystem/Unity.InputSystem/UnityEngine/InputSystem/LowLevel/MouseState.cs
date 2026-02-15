using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x02000199 RID: 409
	[StructLayout(LayoutKind.Explicit, Size = 30)]
	public struct MouseState : IInputStateTypeInfo
	{
		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x0004DE58 File Offset: 0x0004C058
		public static FourCC Format
		{
			get
			{
				return new FourCC('M', 'O', 'U', 'S');
			}
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x0004DE68 File Offset: 0x0004C068
		public MouseState WithButton(MouseButton button, bool state = true)
		{
			uint bit = 1U << (int)button;
			if (state)
			{
				this.buttons |= (ushort)bit;
			}
			else
			{
				this.buttons &= (ushort)(~(ushort)bit);
			}
			return this;
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x0004DEA8 File Offset: 0x0004C0A8
		public FourCC format
		{
			get
			{
				return MouseState.Format;
			}
		}

		// Token: 0x04000995 RID: 2453
		[InputControl(usage = "Point", dontReset = true)]
		[FieldOffset(0)]
		public Vector2 position;

		// Token: 0x04000996 RID: 2454
		[InputControl(usage = "Secondary2DMotion", layout = "Delta")]
		[FieldOffset(8)]
		public Vector2 delta;

		// Token: 0x04000997 RID: 2455
		[InputControl(displayName = "Scroll", layout = "Delta")]
		[InputControl(name = "scroll/x", aliases = new string[] { "horizontal" }, usage = "ScrollHorizontal", displayName = "Left/Right")]
		[InputControl(name = "scroll/y", aliases = new string[] { "vertical" }, usage = "ScrollVertical", displayName = "Up/Down", shortDisplayName = "Wheel")]
		[FieldOffset(16)]
		public Vector2 scroll;

		// Token: 0x04000998 RID: 2456
		[InputControl(name = "press", useStateFrom = "leftButton", synthetic = true, usages = new string[] { })]
		[InputControl(name = "leftButton", layout = "Button", bit = 0U, usage = "PrimaryAction", displayName = "Left Button", shortDisplayName = "LMB")]
		[InputControl(name = "rightButton", layout = "Button", bit = 1U, usage = "SecondaryAction", displayName = "Right Button", shortDisplayName = "RMB")]
		[InputControl(name = "middleButton", layout = "Button", bit = 2U, displayName = "Middle Button", shortDisplayName = "MMB")]
		[InputControl(name = "forwardButton", layout = "Button", bit = 3U, usage = "Forward", displayName = "Forward")]
		[InputControl(name = "backButton", layout = "Button", bit = 4U, usage = "Back", displayName = "Back")]
		[InputControl(name = "pressure", layout = "Axis", usage = "Pressure", offset = 4294967294U, format = "FLT", sizeInBits = 32U)]
		[InputControl(name = "radius", layout = "Vector2", usage = "Radius", offset = 4294967294U, format = "VEC2", sizeInBits = 64U)]
		[InputControl(name = "pointerId", layout = "Digital", format = "BIT", sizeInBits = 1U, offset = 4294967294U)]
		[FieldOffset(24)]
		public ushort buttons;

		// Token: 0x04000999 RID: 2457
		[InputControl(name = "displayIndex", layout = "Integer", displayName = "Display Index")]
		[FieldOffset(26)]
		public ushort displayIndex;

		// Token: 0x0400099A RID: 2458
		[InputControl(name = "clickCount", layout = "Integer", displayName = "Click Count", synthetic = true)]
		[FieldOffset(28)]
		public ushort clickCount;
	}
}
