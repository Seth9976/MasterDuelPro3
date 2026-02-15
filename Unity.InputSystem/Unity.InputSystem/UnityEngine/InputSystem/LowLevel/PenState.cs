using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x0200019B RID: 411
	[StructLayout(LayoutKind.Explicit, Size = 36)]
	public struct PenState : IInputStateTypeInfo
	{
		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000FCA RID: 4042 RVA: 0x0004DEAF File Offset: 0x0004C0AF
		public static FourCC Format
		{
			get
			{
				return new FourCC('P', 'E', 'N', ' ');
			}
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x0004DEC0 File Offset: 0x0004C0C0
		public PenState WithButton(PenButton button, bool state = true)
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

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000FCC RID: 4044 RVA: 0x0004DF00 File Offset: 0x0004C100
		public FourCC format
		{
			get
			{
				return PenState.Format;
			}
		}

		// Token: 0x040009A1 RID: 2465
		[InputControl(usage = "Point", dontReset = true)]
		[FieldOffset(0)]
		public Vector2 position;

		// Token: 0x040009A2 RID: 2466
		[InputControl(usage = "Secondary2DMotion", layout = "Delta")]
		[FieldOffset(8)]
		public Vector2 delta;

		// Token: 0x040009A3 RID: 2467
		[InputControl(layout = "Vector2", displayName = "Tilt", usage = "Tilt")]
		[FieldOffset(16)]
		public Vector2 tilt;

		// Token: 0x040009A4 RID: 2468
		[InputControl(layout = "Analog", usage = "Pressure", defaultState = 0f)]
		[FieldOffset(24)]
		public float pressure;

		// Token: 0x040009A5 RID: 2469
		[InputControl(layout = "Axis", displayName = "Twist", usage = "Twist")]
		[FieldOffset(28)]
		public float twist;

		// Token: 0x040009A6 RID: 2470
		[InputControl(name = "tip", displayName = "Tip", layout = "Button", bit = 0U, usage = "PrimaryAction")]
		[InputControl(name = "press", useStateFrom = "tip", synthetic = true, usages = new string[] { })]
		[InputControl(name = "eraser", displayName = "Eraser", layout = "Button", bit = 1U)]
		[InputControl(name = "inRange", displayName = "In Range?", layout = "Button", bit = 4U, synthetic = true)]
		[InputControl(name = "barrel1", displayName = "Barrel Button #1", layout = "Button", bit = 2U, alias = "barrelFirst", usage = "SecondaryAction")]
		[InputControl(name = "barrel2", displayName = "Barrel Button #2", layout = "Button", bit = 3U, alias = "barrelSecond")]
		[InputControl(name = "barrel3", displayName = "Barrel Button #3", layout = "Button", bit = 5U, alias = "barrelThird")]
		[InputControl(name = "barrel4", displayName = "Barrel Button #4", layout = "Button", bit = 6U, alias = "barrelFourth")]
		[InputControl(name = "radius", layout = "Vector2", format = "VEC2", sizeInBits = 64U, usage = "Radius", offset = 4294967294U)]
		[InputControl(name = "pointerId", layout = "Digital", format = "UINT", sizeInBits = 32U, offset = 4294967294U)]
		[FieldOffset(32)]
		public ushort buttons;

		// Token: 0x040009A7 RID: 2471
		[FieldOffset(34)]
		private ushort displayIndex;
	}
}
