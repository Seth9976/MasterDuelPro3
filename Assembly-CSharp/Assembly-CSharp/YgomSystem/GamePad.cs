using System;
using UnityEngine;

namespace YgomSystem
{
	// Token: 0x020004A0 RID: 1184
	public abstract class GamePad
	{
		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x0600263F RID: 9791 RVA: 0x0000216A File Offset: 0x0000036A
		public static GamePadUpdater GamePadUpdater
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06002640 RID: 9792 RVA: 0x0000216A File Offset: 0x0000036A
		public static GamePad[] GamePadArray
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06002641 RID: 9793 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int gamePadMaxNum
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06002642 RID: 9794 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int UsingPadId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06002643 RID: 9795 RVA: 0x000029CC File Offset: 0x00000BCC
		public int PadID
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06002644 RID: 9796 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsFuncButton(int Type)
		{
			return false;
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x0000216A File Offset: 0x0000036A
		public static GamePad GetGamePad(int iPadID = 1)
		{
			return null;
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x0000216A File Offset: 0x0000036A
		public static GamePad FindGamePad(int padId)
		{
			return null;
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InitializeGamePadSystem(GameObject residentObject)
		{
		}

		// Token: 0x06002648 RID: 9800
		protected abstract int resolveFuncButton(int Type);

		// Token: 0x06002649 RID: 9801 RVA: 0x00002739 File Offset: 0x00000939
		public GamePad(int iPadID)
		{
		}

		// Token: 0x0600264A RID: 9802 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void UpdateFrame()
		{
		}

		// Token: 0x0600264B RID: 9803
		public abstract bool GetKey(int Type);

		// Token: 0x0600264C RID: 9804
		public abstract bool GetKeyDown(int Type);

		// Token: 0x0600264D RID: 9805
		public abstract float GetAnalog(int Type);

		// Token: 0x0600264E RID: 9806 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void Vibrate(GamePad.VIBRATION Id)
		{
		}

		// Token: 0x0600264F RID: 9807 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void StopVibration()
		{
		}

		// Token: 0x06002650 RID: 9808 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetPhysicalKey(int Type)
		{
			return 0;
		}

		// Token: 0x06002651 RID: 9809 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetFunctionalKey(int Type)
		{
			return 0;
		}

		// Token: 0x0400274D RID: 10061
		public const int GAMEPAD_01 = 1;

		// Token: 0x0400274E RID: 10062
		public const int GAMEPAD_02 = 2;

		// Token: 0x0400274F RID: 10063
		public const int GAMEPAD_03 = 3;

		// Token: 0x04002750 RID: 10064
		public const int GAMEPAD_04 = 4;

		// Token: 0x04002751 RID: 10065
		public static readonly int PAD_MAX_NUM;

		// Token: 0x04002752 RID: 10066
		public const int BUTTON_INVALID = -1;

		// Token: 0x04002753 RID: 10067
		public const int BUTTON_RLEFT = 0;

		// Token: 0x04002754 RID: 10068
		public const int BUTTON_RDOWN = 1;

		// Token: 0x04002755 RID: 10069
		public const int BUTTON_RRIGHT = 2;

		// Token: 0x04002756 RID: 10070
		public const int BUTTON_RUP = 3;

		// Token: 0x04002757 RID: 10071
		public const int BUTTON_L1 = 4;

		// Token: 0x04002758 RID: 10072
		public const int BUTTON_R1 = 5;

		// Token: 0x04002759 RID: 10073
		public const int BUTTON_L2 = 6;

		// Token: 0x0400275A RID: 10074
		public const int BUTTON_R2 = 7;

		// Token: 0x0400275B RID: 10075
		public const int BUTTON_OPTION1 = 9;

		// Token: 0x0400275C RID: 10076
		public const int BUTTON_L3 = 10;

		// Token: 0x0400275D RID: 10077
		public const int BUTTON_R3 = 11;

		// Token: 0x0400275E RID: 10078
		public const int BUTTON_OPTION2 = 13;

		// Token: 0x0400275F RID: 10079
		public const int BUTTON_UP = 100;

		// Token: 0x04002760 RID: 10080
		public const int BUTTON_RIGHT = 101;

		// Token: 0x04002761 RID: 10081
		public const int BUTTON_DOWN = 102;

		// Token: 0x04002762 RID: 10082
		public const int BUTTON_LEFT = 103;

		// Token: 0x04002763 RID: 10083
		public const int ANALOG_L_X = 200;

		// Token: 0x04002764 RID: 10084
		public const int ANALOG_L_Y = 201;

		// Token: 0x04002765 RID: 10085
		public const int ANALOG_R_X = 202;

		// Token: 0x04002766 RID: 10086
		public const int ANALOG_R_Y = 203;

		// Token: 0x04002767 RID: 10087
		public const int ANALOG_L2 = 204;

		// Token: 0x04002768 RID: 10088
		public const int ANALOG_R2 = 205;

		// Token: 0x04002769 RID: 10089
		public const int BUTTON_FUNC_base = 1000;

		// Token: 0x0400276A RID: 10090
		public const int BUTTON_FUNC_DECISION = 1001;

		// Token: 0x0400276B RID: 10091
		public const int BUTTON_FUNC_CANCEL = 1002;

		// Token: 0x0400276C RID: 10092
		public const int BUTTON_FUNC_end = 1003;

		// Token: 0x0400276D RID: 10093
		private static GamePad[] g_GamePadArray;

		// Token: 0x0400276E RID: 10094
		private static GamePadUpdater g_GamePadUpdater;

		// Token: 0x0400276F RID: 10095
		protected int m_iPadID;

		// Token: 0x020004A1 RID: 1185
		public enum VIBRATION
		{
			// Token: 0x04002771 RID: 10097
			NONE,
			// Token: 0x04002772 RID: 10098
			SYS_RESPONSE,
			// Token: 0x04002773 RID: 10099
			DUEL_MONSTER_CUTIN,
			// Token: 0x04002774 RID: 10100
			DUEL_MONSTER_LAND_MID,
			// Token: 0x04002775 RID: 10101
			DUEL_MONSTER_LAND_HIGH,
			// Token: 0x04002776 RID: 10102
			DUEL_CARD_BREAK,
			// Token: 0x04002777 RID: 10103
			DUEL_ATTACK_LOW,
			// Token: 0x04002778 RID: 10104
			DUEL_ATTACK_HIGH,
			// Token: 0x04002779 RID: 10105
			DUEL_EFFECT_DAMAGE,
			// Token: 0x0400277A RID: 10106
			DUEL_BG_BREAK,
			// Token: 0x0400277B RID: 10107
			DUEL_FINISHBLOW
		}
	}
}
