using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x020005A3 RID: 1443
	public class KeyInputInfo
	{
		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06002D94 RID: 11668 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002D95 RID: 11669 RVA: 0x0000216D File Offset: 0x0000036D
		public int onKeyCount
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06002D96 RID: 11670 RVA: 0x000029C5 File Offset: 0x00000BC5
		public static float padInputRepeatStartTime
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06002D97 RID: 11671 RVA: 0x000029C5 File Offset: 0x00000BC5
		public static float padInputRepeatIntervalTime
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x06002D98 RID: 11672 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize()
		{
		}

		// Token: 0x06002D99 RID: 11673 RVA: 0x0000216D File Offset: 0x0000036D
		public void ResetInputInfo()
		{
		}

		// Token: 0x06002D9A RID: 11674 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool UpdatePadKeyInputInfo(GamePad pad)
		{
			return false;
		}

		// Token: 0x06002D9B RID: 11675 RVA: 0x000029CC File Offset: 0x00000BCC
		private int UpdatePadKeyInputInfo(GamePad pad, SelectorManager.KeyType keyType, ref bool isReleased, ref bool isOnPush, ref bool isPushed, ref bool isOnRelease)
		{
			return 0;
		}

		// Token: 0x06002D9C RID: 11676 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool UpdateAnalogInput(GamePad pad)
		{
			return false;
		}

		// Token: 0x06002D9D RID: 11677 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool UpdatePadDirectionalInputInfo(GamePad pad)
		{
			return false;
		}

		// Token: 0x06002D9E RID: 11678 RVA: 0x000029CC File Offset: 0x00000BCC
		private int GetGamePadKeyConfig(SelectorManager.KeyType keyType)
		{
			return 0;
		}

		// Token: 0x06002D9F RID: 11679 RVA: 0x000F1FD4 File Offset: 0x000F01D4
		public static ValueTuple<int, int> GetGamePadKeyConfig(SelectorManager.AnalogType analogType)
		{
			return default(ValueTuple<int, int>);
		}

		// Token: 0x06002DA0 RID: 11680 RVA: 0x0000216A File Offset: 0x0000036A
		private KeyCode[] GetKeyboardKeyConfig(SelectorManager.KeyType keyType)
		{
			return null;
		}

		// Token: 0x06002DA1 RID: 11681 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool GetKey(GamePad pad, int gamepad_key_id, KeyCode[] key_code)
		{
			return false;
		}

		// Token: 0x06002DA2 RID: 11682 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool UpdateBackkeyInputInfo(bool on_back_key)
		{
			return false;
		}

		// Token: 0x06002DA3 RID: 11683 RVA: 0x000029CC File Offset: 0x00000BCC
		private SelectorManager.KeyStatus UpdateKeyStatus(SelectorManager.KeyType key_type, bool on_key)
		{
			return SelectorManager.KeyStatus.Released;
		}

		// Token: 0x06002DA4 RID: 11684 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool UpdateAnalogInput(SelectorManager.AnalogType type, Vector2 input)
		{
			return false;
		}

		// Token: 0x06002DA5 RID: 11685 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool UpdateMouseInputInfo(bool on_left, bool on_right, bool on_center, float wheel, Vector2 screenPoint, bool onScreen)
		{
			return false;
		}

		// Token: 0x06002DA6 RID: 11686 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool UpdateMouseStatus(SelectorManager.MouseType mouse_type, bool on_key)
		{
			return false;
		}

		// Token: 0x06002DA7 RID: 11687 RVA: 0x000029CC File Offset: 0x00000BCC
		public SelectorManager.KeyStatus GetKeyStatus(SelectorManager.KeyType key_type)
		{
			return SelectorManager.KeyStatus.Released;
		}

		// Token: 0x06002DA8 RID: 11688 RVA: 0x000F1FEC File Offset: 0x000F01EC
		public Vector2 GetAnalogInput(SelectorManager.AnalogType analog_type)
		{
			return default(Vector2);
		}

		// Token: 0x06002DA9 RID: 11689 RVA: 0x000029CC File Offset: 0x00000BCC
		public SelectorManager.KeyStatus GetMouseStatus(SelectorManager.MouseType mouse_type)
		{
			return SelectorManager.KeyStatus.Released;
		}

		// Token: 0x06002DAA RID: 11690 RVA: 0x000F2004 File Offset: 0x000F0204
		public ValueTuple<bool, Vector2, Vector2> GetScreenPoint()
		{
			return default(ValueTuple<bool, Vector2, Vector2>);
		}

		// Token: 0x06002DAB RID: 11691 RVA: 0x000029CC File Offset: 0x00000BCC
		public SelectorManager.KeyType GetKeyType(SelectorManager.KeyStatus status)
		{
			return SelectorManager.KeyType.None;
		}

		// Token: 0x06002DAC RID: 11692 RVA: 0x000029CC File Offset: 0x00000BCC
		public SelectorManager.MouseType GetMouseType(SelectorManager.KeyStatus status)
		{
			return SelectorManager.MouseType.None;
		}

		// Token: 0x06002DAD RID: 11693 RVA: 0x000029CC File Offset: 0x00000BCC
		public SelectorManager.AnalogType GetAnalogType()
		{
			return SelectorManager.AnalogType.None;
		}

		// Token: 0x04002B75 RID: 11125
		private Dictionary<SelectorManager.KeyType, SelectorManager.KeyStatus> keyStatus;

		// Token: 0x04002B76 RID: 11126
		private Dictionary<SelectorManager.AnalogType, Vector2> analogInput;

		// Token: 0x04002B77 RID: 11127
		private Dictionary<SelectorManager.MouseType, SelectorManager.KeyStatus> mouseStatus;

		// Token: 0x04002B78 RID: 11128
		private Vector2 screenPoint;

		// Token: 0x04002B79 RID: 11129
		private Vector2 screenPointDelta;

		// Token: 0x04002B7A RID: 11130
		private bool onScreen;

		// Token: 0x04002B7B RID: 11131
		private float padInputContinueTime;

		// Token: 0x04002B7C RID: 11132
		private SelectorManager.KeyType currentDirection;

		// Token: 0x04002B7D RID: 11133
		private SelectorManager.KeyType[] keyPrioriority;

		// Token: 0x04002B7E RID: 11134
		private SelectorManager.MouseType[] mousePriority;

		// Token: 0x04002B7F RID: 11135
		private SelectorManager.AnalogType[] analogPriority;
	}
}
