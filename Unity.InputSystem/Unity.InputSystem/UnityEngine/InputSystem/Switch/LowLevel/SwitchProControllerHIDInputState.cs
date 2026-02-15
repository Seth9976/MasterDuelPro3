using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Switch.LowLevel
{
	// Token: 0x0200012F RID: 303
	[StructLayout(LayoutKind.Explicit, Size = 7)]
	internal struct SwitchProControllerHIDInputState : IInputStateTypeInfo
	{
		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x000471B0 File Offset: 0x000453B0
		public FourCC format
		{
			get
			{
				return SwitchProControllerHIDInputState.Format;
			}
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x000471B7 File Offset: 0x000453B7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public SwitchProControllerHIDInputState WithButton(SwitchProControllerHIDInputState.Button button, bool value = true)
		{
			this.Set(button, value);
			return this;
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x000471C8 File Offset: 0x000453C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Set(SwitchProControllerHIDInputState.Button button, bool state)
		{
			if (button >= SwitchProControllerHIDInputState.Button.Capture)
			{
				if (button < (SwitchProControllerHIDInputState.Button)18)
				{
					byte bit = (byte)(1 << button - SwitchProControllerHIDInputState.Button.Capture);
					if (state)
					{
						this.buttons2 |= bit;
						return;
					}
					this.buttons2 &= ~bit;
				}
				return;
			}
			ushort bit2 = (ushort)(1 << (int)button);
			if (state)
			{
				this.buttons1 |= bit2;
				return;
			}
			this.buttons1 &= ~bit2;
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x0004723B File Offset: 0x0004543B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Press(SwitchProControllerHIDInputState.Button button)
		{
			this.Set(button, true);
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x00047245 File Offset: 0x00045445
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Release(SwitchProControllerHIDInputState.Button button)
		{
			this.Set(button, false);
		}

		// Token: 0x04000700 RID: 1792
		public static FourCC Format = new FourCC('S', 'P', 'V', 'S');

		// Token: 0x04000701 RID: 1793
		[InputControl(name = "leftStick", layout = "Stick", format = "VC2B")]
		[InputControl(name = "leftStick/x", offset = 0U, format = "BYTE", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5")]
		[InputControl(name = "leftStick/left", offset = 0U, format = "BYTE", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0.15,clampMax=0.5,invert")]
		[InputControl(name = "leftStick/right", offset = 0U, format = "BYTE", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0.5,clampMax=0.85")]
		[InputControl(name = "leftStick/y", offset = 1U, format = "BYTE", parameters = "invert,normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5")]
		[InputControl(name = "leftStick/up", offset = 1U, format = "BYTE", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0.15,clampMax=0.5,invert")]
		[InputControl(name = "leftStick/down", offset = 1U, format = "BYTE", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0.5,clampMax=0.85,invert=false")]
		[FieldOffset(0)]
		public byte leftStickX;

		// Token: 0x04000702 RID: 1794
		[FieldOffset(1)]
		public byte leftStickY;

		// Token: 0x04000703 RID: 1795
		[InputControl(name = "rightStick", layout = "Stick", format = "VC2B")]
		[InputControl(name = "rightStick/x", offset = 0U, format = "BYTE", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5")]
		[InputControl(name = "rightStick/left", offset = 0U, format = "BYTE", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0,clampMax=0.5,invert")]
		[InputControl(name = "rightStick/right", offset = 0U, format = "BYTE", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0.5,clampMax=1")]
		[InputControl(name = "rightStick/y", offset = 1U, format = "BYTE", parameters = "invert,normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5")]
		[InputControl(name = "rightStick/up", offset = 1U, format = "BYTE", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0.15,clampMax=0.5,invert")]
		[InputControl(name = "rightStick/down", offset = 1U, format = "BYTE", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0.5,clampMax=0.85,invert=false")]
		[FieldOffset(2)]
		public byte rightStickX;

		// Token: 0x04000704 RID: 1796
		[FieldOffset(3)]
		public byte rightStickY;

		// Token: 0x04000705 RID: 1797
		[InputControl(name = "dpad", format = "BIT", bit = 0U, sizeInBits = 4U)]
		[InputControl(name = "dpad/up", bit = 0U)]
		[InputControl(name = "dpad/right", bit = 1U)]
		[InputControl(name = "dpad/down", bit = 2U)]
		[InputControl(name = "dpad/left", bit = 3U)]
		[InputControl(name = "buttonWest", displayName = "Y", shortDisplayName = "Y", bit = 4U, usage = "SecondaryAction")]
		[InputControl(name = "buttonNorth", displayName = "X", shortDisplayName = "X", bit = 5U)]
		[InputControl(name = "buttonSouth", displayName = "B", shortDisplayName = "B", bit = 6U, usages = new string[] { "Back", "Cancel" })]
		[InputControl(name = "buttonEast", displayName = "A", shortDisplayName = "A", bit = 7U, usages = new string[] { "PrimaryAction", "Submit" })]
		[InputControl(name = "leftShoulder", displayName = "L", shortDisplayName = "L", bit = 8U)]
		[InputControl(name = "rightShoulder", displayName = "R", shortDisplayName = "R", bit = 9U)]
		[InputControl(name = "leftStickPress", displayName = "Left Stick", bit = 10U)]
		[InputControl(name = "rightStickPress", displayName = "Right Stick", bit = 11U)]
		[InputControl(name = "leftTrigger", displayName = "ZL", shortDisplayName = "ZL", format = "BIT", bit = 12U)]
		[InputControl(name = "rightTrigger", displayName = "ZR", shortDisplayName = "ZR", format = "BIT", bit = 13U)]
		[InputControl(name = "start", displayName = "Plus", bit = 14U, usage = "Menu")]
		[InputControl(name = "select", displayName = "Minus", bit = 15U)]
		[FieldOffset(4)]
		public ushort buttons1;

		// Token: 0x04000706 RID: 1798
		[InputControl(name = "capture", layout = "Button", displayName = "Capture", bit = 0U)]
		[InputControl(name = "home", layout = "Button", displayName = "Home", bit = 1U)]
		[FieldOffset(6)]
		public byte buttons2;

		// Token: 0x02000130 RID: 304
		public enum Button
		{
			// Token: 0x04000708 RID: 1800
			Up,
			// Token: 0x04000709 RID: 1801
			Right,
			// Token: 0x0400070A RID: 1802
			Down,
			// Token: 0x0400070B RID: 1803
			Left,
			// Token: 0x0400070C RID: 1804
			West,
			// Token: 0x0400070D RID: 1805
			North,
			// Token: 0x0400070E RID: 1806
			South,
			// Token: 0x0400070F RID: 1807
			East,
			// Token: 0x04000710 RID: 1808
			L,
			// Token: 0x04000711 RID: 1809
			R,
			// Token: 0x04000712 RID: 1810
			StickL,
			// Token: 0x04000713 RID: 1811
			StickR,
			// Token: 0x04000714 RID: 1812
			ZL,
			// Token: 0x04000715 RID: 1813
			ZR,
			// Token: 0x04000716 RID: 1814
			Plus,
			// Token: 0x04000717 RID: 1815
			Minus,
			// Token: 0x04000718 RID: 1816
			Capture,
			// Token: 0x04000719 RID: 1817
			Home,
			// Token: 0x0400071A RID: 1818
			X = 5,
			// Token: 0x0400071B RID: 1819
			B,
			// Token: 0x0400071C RID: 1820
			Y = 4,
			// Token: 0x0400071D RID: 1821
			A = 7
		}
	}
}
