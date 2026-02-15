using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.InputForUI
{
	// Token: 0x0200000B RID: 11
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal struct EventModifiers
	{
		// Token: 0x06000022 RID: 34 RVA: 0x000024CC File Offset: 0x000006CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsPressed(EventModifiers.Modifiers mod)
		{
			return (this._state & (uint)mod) > 0U;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000024D9 File Offset: 0x000006D9
		public bool isShiftPressed
		{
			get
			{
				return this.IsPressed(EventModifiers.Modifiers.Shift);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000024E2 File Offset: 0x000006E2
		public bool isCtrlPressed
		{
			get
			{
				return this.IsPressed(EventModifiers.Modifiers.Ctrl);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000024EC File Offset: 0x000006EC
		public bool isAltPressed
		{
			get
			{
				return this.IsPressed(EventModifiers.Modifiers.Alt);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000024F6 File Offset: 0x000006F6
		public bool isMetaPressed
		{
			get
			{
				return this.IsPressed(EventModifiers.Modifiers.Meta);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002503 File Offset: 0x00000703
		public bool isCapsLockEnabled
		{
			get
			{
				return this.IsPressed(EventModifiers.Modifiers.CapsLock);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002510 File Offset: 0x00000710
		public bool isFunctionKeyPressed
		{
			get
			{
				return this.IsPressed(EventModifiers.Modifiers.FunctionKey);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000251D File Offset: 0x0000071D
		public bool isNumericPressed
		{
			get
			{
				return this.IsPressed(EventModifiers.Modifiers.Numeric);
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000252C File Offset: 0x0000072C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetPressed(EventModifiers.Modifiers modifier, bool pressed)
		{
			if (pressed)
			{
				this._state |= (uint)modifier;
			}
			else
			{
				this._state &= (uint)(~(uint)modifier);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000255E File Offset: 0x0000075E
		public void Reset()
		{
			this._state = 0U;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002568 File Offset: 0x00000768
		private static void Append(ref string str, string value)
		{
			str = (string.IsNullOrEmpty(str) ? value : (str + "," + value));
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002588 File Offset: 0x00000788
		public override string ToString()
		{
			string str = string.Empty;
			bool flag = this.IsPressed(EventModifiers.Modifiers.LeftShift);
			if (flag)
			{
				EventModifiers.Append(ref str, "LeftShift");
			}
			bool flag2 = this.IsPressed(EventModifiers.Modifiers.RightShift);
			if (flag2)
			{
				EventModifiers.Append(ref str, "RightShift");
			}
			bool flag3 = this.IsPressed(EventModifiers.Modifiers.LeftCtrl);
			if (flag3)
			{
				EventModifiers.Append(ref str, "LeftCtrl");
			}
			bool flag4 = this.IsPressed(EventModifiers.Modifiers.RightCtrl);
			if (flag4)
			{
				EventModifiers.Append(ref str, "RightCtrl");
			}
			bool flag5 = this.IsPressed(EventModifiers.Modifiers.LeftAlt);
			if (flag5)
			{
				EventModifiers.Append(ref str, "LeftAlt");
			}
			bool flag6 = this.IsPressed(EventModifiers.Modifiers.RightAlt);
			if (flag6)
			{
				EventModifiers.Append(ref str, "RightAlt");
			}
			bool flag7 = this.IsPressed(EventModifiers.Modifiers.LeftMeta);
			if (flag7)
			{
				EventModifiers.Append(ref str, "LeftMeta");
			}
			bool flag8 = this.IsPressed(EventModifiers.Modifiers.RightMeta);
			if (flag8)
			{
				EventModifiers.Append(ref str, "RightMeta");
			}
			bool flag9 = this.IsPressed(EventModifiers.Modifiers.CapsLock);
			if (flag9)
			{
				EventModifiers.Append(ref str, "CapsLock");
			}
			bool flag10 = this.IsPressed(EventModifiers.Modifiers.Numlock);
			if (flag10)
			{
				EventModifiers.Append(ref str, "Numlock");
			}
			bool flag11 = this.IsPressed(EventModifiers.Modifiers.FunctionKey);
			if (flag11)
			{
				EventModifiers.Append(ref str, "FunctionKey");
			}
			bool flag12 = this.IsPressed(EventModifiers.Modifiers.Numeric);
			if (flag12)
			{
				EventModifiers.Append(ref str, "Numeric");
			}
			return str;
		}

		// Token: 0x04000033 RID: 51
		private uint _state;

		// Token: 0x0200000C RID: 12
		[Flags]
		public enum Modifiers : uint
		{
			// Token: 0x04000035 RID: 53
			LeftShift = 1U,
			// Token: 0x04000036 RID: 54
			RightShift = 2U,
			// Token: 0x04000037 RID: 55
			Shift = 3U,
			// Token: 0x04000038 RID: 56
			LeftCtrl = 4U,
			// Token: 0x04000039 RID: 57
			RightCtrl = 8U,
			// Token: 0x0400003A RID: 58
			Ctrl = 12U,
			// Token: 0x0400003B RID: 59
			LeftAlt = 16U,
			// Token: 0x0400003C RID: 60
			RightAlt = 32U,
			// Token: 0x0400003D RID: 61
			Alt = 48U,
			// Token: 0x0400003E RID: 62
			LeftMeta = 64U,
			// Token: 0x0400003F RID: 63
			RightMeta = 128U,
			// Token: 0x04000040 RID: 64
			Meta = 192U,
			// Token: 0x04000041 RID: 65
			CapsLock = 256U,
			// Token: 0x04000042 RID: 66
			Numlock = 512U,
			// Token: 0x04000043 RID: 67
			FunctionKey = 1024U,
			// Token: 0x04000044 RID: 68
			Numeric = 2048U
		}
	}
}
