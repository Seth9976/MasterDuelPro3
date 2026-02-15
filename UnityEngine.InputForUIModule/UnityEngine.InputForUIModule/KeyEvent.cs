using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.IntegerTime;
using UnityEngine.Bindings;

namespace UnityEngine.InputForUI
{
	// Token: 0x02000010 RID: 16
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal struct KeyEvent : IEventProperties
	{
		// Token: 0x1700001C RID: 28
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002747 File Offset: 0x00000947
		public DiscreteTime timestamp
		{
			[CompilerGenerated]
			set
			{
				this.<timestamp>k__BackingField = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002750 File Offset: 0x00000950
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002758 File Offset: 0x00000958
		public EventSource eventSource { readonly get; set; }

		// Token: 0x1700001E RID: 30
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002761 File Offset: 0x00000961
		public uint playerId
		{
			[CompilerGenerated]
			set
			{
				this.<playerId>k__BackingField = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600003B RID: 59 RVA: 0x0000276A File Offset: 0x0000096A
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002772 File Offset: 0x00000972
		public EventModifiers eventModifiers { readonly get; set; }

		// Token: 0x0600003D RID: 61 RVA: 0x0000277C File Offset: 0x0000097C
		public override string ToString()
		{
			KeyEvent.Type type = this.type;
			KeyEvent.Type type2 = type;
			string text;
			if (type2 - KeyEvent.Type.KeyPressed > 2)
			{
				if (type2 != KeyEvent.Type.State)
				{
					throw new ArgumentOutOfRangeException();
				}
				text = string.Format("{0} Pressed:{1}", this.type, this.buttonsState);
			}
			else
			{
				text = string.Format("{0} {1}", this.type, this.keyCode);
			}
			return text;
		}

		// Token: 0x04000051 RID: 81
		public KeyEvent.Type type;

		// Token: 0x04000052 RID: 82
		public KeyCode keyCode;

		// Token: 0x04000053 RID: 83
		public KeyEvent.ButtonsState buttonsState;

		// Token: 0x02000011 RID: 17
		public enum Type
		{
			// Token: 0x04000059 RID: 89
			KeyPressed = 1,
			// Token: 0x0400005A RID: 90
			KeyRepeated,
			// Token: 0x0400005B RID: 91
			KeyReleased,
			// Token: 0x0400005C RID: 92
			State
		}

		// Token: 0x02000012 RID: 18
		public struct ButtonsState
		{
			// Token: 0x0600003E RID: 62 RVA: 0x000027F0 File Offset: 0x000009F0
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal static bool ShouldBeProcessed(KeyCode keyCode)
			{
				return keyCode <= KeyCode.Menu;
			}

			// Token: 0x0600003F RID: 63 RVA: 0x00002810 File Offset: 0x00000A10
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private unsafe bool GetUnchecked(uint index)
			{
				return (*((ref this.buttons.FixedElementField) + (UIntPtr)(index >> 3)) & (byte)(1 << (int)(index & 7U))) > 0;
			}

			// Token: 0x06000040 RID: 64 RVA: 0x00002840 File Offset: 0x00000A40
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private void SetUnchecked(uint index)
			{
				ref byte ptr = (ref this.buttons.FixedElementField) + (UIntPtr)(index >> 3);
				ptr |= (byte)(1 << (int)(index & 7U));
			}

			// Token: 0x06000041 RID: 65 RVA: 0x00002861 File Offset: 0x00000A61
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private void ClearUnchecked(uint index)
			{
				ref byte ptr = (ref this.buttons.FixedElementField) + (UIntPtr)(index >> 3);
				ptr &= (byte)(~(byte)(1 << (int)(index & 7U)));
			}

			// Token: 0x06000042 RID: 66 RVA: 0x00002884 File Offset: 0x00000A84
			public bool IsPressed(KeyCode keyCode)
			{
				return KeyEvent.ButtonsState.ShouldBeProcessed(keyCode) && this.GetUnchecked((uint)keyCode);
			}

			// Token: 0x06000043 RID: 67 RVA: 0x000028A8 File Offset: 0x00000AA8
			public IEnumerable<KeyCode> GetAllPressed()
			{
				uint num;
				for (uint index = 0U; index <= 319U; index = num)
				{
					bool @unchecked = this.GetUnchecked(index);
					if (@unchecked)
					{
						yield return (KeyCode)index;
					}
					num = index + 1U;
				}
				yield break;
			}

			// Token: 0x06000044 RID: 68 RVA: 0x000028C0 File Offset: 0x00000AC0
			public void SetPressed(KeyCode keyCode, bool pressed)
			{
				bool flag = !KeyEvent.ButtonsState.ShouldBeProcessed(keyCode);
				if (!flag)
				{
					if (pressed)
					{
						this.SetUnchecked((uint)keyCode);
					}
					else
					{
						this.ClearUnchecked((uint)keyCode);
					}
				}
			}

			// Token: 0x06000045 RID: 69 RVA: 0x000028F4 File Offset: 0x00000AF4
			public unsafe void Reset()
			{
				int i = 0;
				while ((long)i < 40L)
				{
					*((ref this.buttons.FixedElementField) + i) = 0;
					i++;
				}
			}

			// Token: 0x06000046 RID: 70 RVA: 0x00002924 File Offset: 0x00000B24
			public override string ToString()
			{
				return string.Join<KeyCode>(",", this.GetAllPressed());
			}

			// Token: 0x0400005D RID: 93
			[FixedBuffer(typeof(byte), 40)]
			private KeyEvent.ButtonsState.<buttons>e__FixedBuffer buttons;

			// Token: 0x02000014 RID: 20
			[UnsafeValueType]
			[CompilerGenerated]
			[StructLayout(LayoutKind.Sequential, Size = 40)]
			public struct <buttons>e__FixedBuffer
			{
				// Token: 0x04000064 RID: 100
				public byte FixedElementField;
			}
		}
	}
}
