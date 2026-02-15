using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Duel
{
	// Token: 0x02000D8D RID: 3469
	public class DuelLogController : MonoBehaviour
	{
		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x06006585 RID: 25989 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isOpen
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x06006586 RID: 25990 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006587 RID: 25991 RVA: 0x0000216D File Offset: 0x0000036D
		public Action<bool> onChangeOpenClose
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x06006588 RID: 25992 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006589 RID: 25993 RVA: 0x0000216D File Offset: 0x0000036D
		public bool IsReady
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x0600658A RID: 25994 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool m_IsIndent
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600658B RID: 25995 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(Transform parent, DuelClient host, SelectionButton duelLogButton, Action<bool> onChangeOpenClose, Action<DuelLogController> onFinish)
		{
		}

		// Token: 0x0600658C RID: 25996 RVA: 0x0000216D File Offset: 0x0000036D
		public void Open()
		{
		}

		// Token: 0x0600658D RID: 25997 RVA: 0x0000216D File Offset: 0x0000036D
		public void Close()
		{
		}

		// Token: 0x0600658E RID: 25998 RVA: 0x0000216D File Offset: 0x0000036D
		public void CloseTemprary()
		{
		}

		// Token: 0x0600658F RID: 25999 RVA: 0x0000216D File Offset: 0x0000036D
		public void Resume()
		{
		}

		// Token: 0x06006590 RID: 26000 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetAlpha(float alpha)
		{
		}

		// Token: 0x06006591 RID: 26001 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetAutoScroll(bool auto)
		{
		}

		// Token: 0x06006592 RID: 26002 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetShortkeyIconVisible(bool visible)
		{
		}

		// Token: 0x06006593 RID: 26003 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddLogData(Engine.ViewType viewType, int param1, int param2, int param3)
		{
		}

		// Token: 0x06006594 RID: 26004 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddLogText(int player, string text)
		{
		}

		// Token: 0x06006595 RID: 26005 RVA: 0x0000216D File Offset: 0x0000036D
		protected void OnUpdateListItemBase(GameObject gob, int baseitemindex)
		{
		}

		// Token: 0x06006596 RID: 26006 RVA: 0x0000216D File Offset: 0x0000036D
		protected void OnUpdateShowTurn(GameObject eom, int dataindex)
		{
		}

		// Token: 0x06006597 RID: 26007 RVA: 0x0000216D File Offset: 0x0000036D
		protected void OnUpdateShowAction(GameObject eom, int dataindex)
		{
		}

		// Token: 0x06006598 RID: 26008 RVA: 0x0000216D File Offset: 0x0000036D
		protected void OnUpdateShowChain(GameObject eom, int dataindex)
		{
		}

		// Token: 0x06006599 RID: 26009 RVA: 0x0000216D File Offset: 0x0000036D
		protected void OnUpdateShowTag(GameObject eom, int dataindex)
		{
		}

		// Token: 0x0600659A RID: 26010 RVA: 0x0000216D File Offset: 0x0000036D
		protected void OnUpdateShowCardName(GameObject eom, int dataindex)
		{
		}

		// Token: 0x0600659B RID: 26011 RVA: 0x0000216D File Offset: 0x0000036D
		protected void OnUpdateShowText(GameObject eom, int dataindex)
		{
		}

		// Token: 0x0600659C RID: 26012 RVA: 0x0000216D File Offset: 0x0000036D
		protected void OnUpdateShowPhase(GameObject eom, int dataindex)
		{
		}

		// Token: 0x0600659D RID: 26013 RVA: 0x0000216D File Offset: 0x0000036D
		protected void OnUpdateSeparateLine(GameObject eom, int dataindex)
		{
		}

		// Token: 0x0600659E RID: 26014 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AfterAddLog(string label, int datalistcount)
		{
		}

		// Token: 0x0600659F RID: 26015 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AddDuelStartLog(int param1, int param2, int param3)
		{
		}

		// Token: 0x060065A0 RID: 26016 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AddTurnChangeLog(int param1, int param2, int param3)
		{
		}

		// Token: 0x060065A1 RID: 26017 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AddBattleAttackLog(int param1, int param2, int param3)
		{
		}

		// Token: 0x060065A2 RID: 26018 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AddLifeDamageLog(int param1, int param2, int param3)
		{
		}

		// Token: 0x060065A3 RID: 26019 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AddPhaseChange(int param1, int param2, int param3)
		{
		}

		// Token: 0x060065A4 RID: 26020 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AddHandOpenLog(int param1, int param2, int param3)
		{
		}

		// Token: 0x060065A5 RID: 26021 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AddDeckShuffleLog(int param1, int param2, int param3)
		{
		}

		// Token: 0x060065A6 RID: 26022 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AddDeckFlipTopLog(int param1, int param2, int param3)
		{
		}

		// Token: 0x060065A7 RID: 26023 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AddCardLockonLog(int param1, int param2, int param3)
		{
		}

		// Token: 0x060065A8 RID: 26024 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AddCardMoveLog(int param1, int param2, int param3)
		{
		}

		// Token: 0x060065A9 RID: 26025 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AddCardSwapLog(int param1, int param2, int param3)
		{
		}

		// Token: 0x060065AA RID: 26026 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AddCardFlipTurnLog(int param1, int param2, int param3)
		{
		}

		// Token: 0x060065AB RID: 26027 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AddCardSetLog(int param1, int param2, int param3)
		{
		}

		// Token: 0x060065AC RID: 26028 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AddCardBreakLog(int param1, int param2, int param3)
		{
		}

		// Token: 0x060065AD RID: 26029 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AddCardExplosionLog(int param1, int param2, int param3)
		{
		}

		// Token: 0x060065AE RID: 26030 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AddCardHappenLog(int param1, int param2, int param3)
		{
		}

		// Token: 0x060065AF RID: 26031 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AddCardDisableLog(int param1, int param2, int param3)
		{
		}

		// Token: 0x060065B0 RID: 26032 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AddManaSetLog(int param1, int param2, int param3)
		{
		}

		// Token: 0x060065B1 RID: 26033 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AddChainSetLog(int param1, int param2, int param3)
		{
		}

		// Token: 0x060065B2 RID: 26034 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AddChainRunLog(int param1, int param2, int param3)
		{
		}

		// Token: 0x060065B3 RID: 26035 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AddRunSummonLog(int param1, int param2, int param3)
		{
		}

		// Token: 0x060065B4 RID: 26036 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AddRunSpSummonLog(int param1, int param2, int param3)
		{
		}

		// Token: 0x060065B5 RID: 26037 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AddRunFusionLog(int param1, int param2, int param3)
		{
		}

		// Token: 0x060065B6 RID: 26038 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AddRunCoinLog(int param1, int param2, int param3)
		{
		}

		// Token: 0x060065B7 RID: 26039 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AddRunDiceLog(int param1, int param2, int param3)
		{
		}

		// Token: 0x060065B8 RID: 26040 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AddChainEndLog(int param1, int param2, int param3)
		{
		}

		// Token: 0x060065B9 RID: 26041 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AddShowTextLog(int param1, int param2, int param3)
		{
		}

		// Token: 0x060065BA RID: 26042 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AddSeparateLine()
		{
		}

		// Token: 0x060065BB RID: 26043 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AddChainTag(bool team, int cardid, int iNum, ShowChainData.ChainDataType type)
		{
		}

		// Token: 0x060065BC RID: 26044 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize()
		{
		}

		// Token: 0x060065BD RID: 26045 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnScrollViewReady()
		{
		}

		// Token: 0x060065BE RID: 26046 RVA: 0x0000216D File Offset: 0x0000036D
		private void initializeComponent()
		{
		}

		// Token: 0x060065BF RID: 26047 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeDict()
		{
		}

		// Token: 0x060065C0 RID: 26048 RVA: 0x000029CC File Offset: 0x00000BCC
		private LOGACTIONTYPE GetActionType(Engine.CardMoveType moveType, Engine.CardStatus stFrom, Engine.CardStatus stTo)
		{
			return LOGACTIONTYPE.ACTION_NONE;
		}

		// Token: 0x060065C1 RID: 26049 RVA: 0x0000216D File Offset: 0x0000036D
		private void CheckSpSummon(ref LOGACTIONTYPE actiontype)
		{
		}

		// Token: 0x060065C2 RID: 26050 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetSelectorPriority(SharedDefinition.DuelSelectorPriority priority)
		{
		}

		// Token: 0x060065C3 RID: 26051 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SetPlayerIcon(Image img, int playerid)
		{
			return false;
		}

		// Token: 0x060065C4 RID: 26052 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool CheckCardidValid(int cardid)
		{
			return false;
		}

		// Token: 0x04009FE9 RID: 40937
		public const int INVALIDCARDID = 0;

		// Token: 0x04009FEA RID: 40938
		public const string LABEL_SHOWTURN = "ShowTurn";

		// Token: 0x04009FEB RID: 40939
		public const string LABEL_SHOWACTION = "ShowAction";

		// Token: 0x04009FEC RID: 40940
		public const string LABEL_SHOWCHAIN = "ShowChain";

		// Token: 0x04009FED RID: 40941
		public const string LABEL_SHOWTAG = "ShowTag";

		// Token: 0x04009FEE RID: 40942
		public const string LABEL_SHOWCARDNAME = "ShowCardName";

		// Token: 0x04009FEF RID: 40943
		public const string LABEL_SHOWTEXT = "ShowText";

		// Token: 0x04009FF0 RID: 40944
		public const string LABEL_SHOWPHASE = "ShowPhase";

		// Token: 0x04009FF1 RID: 40945
		public const string LABEL_SAPERATELINE = "SeparateLine";

		// Token: 0x04009FF2 RID: 40946
		public const string LABEL_ROOT = "Root";

		// Token: 0x04009FF3 RID: 40947
		public const string LABEL_SCROLLUP = "ScrollUp";

		// Token: 0x04009FF4 RID: 40948
		public const string LABEL_SCROLLDOWN = "ScrollDown";

		// Token: 0x04009FF5 RID: 40949
		public const string LABEL_SCROLLVIEW = "ScrollView";

		// Token: 0x04009FF6 RID: 40950
		public const string LABEL_BG = "Bg";

		// Token: 0x04009FF7 RID: 40951
		public const string LABEL_TWEEN_SHOWLOG = "ShowLog";

		// Token: 0x04009FF8 RID: 40952
		public const string LABEL_TWEEN_HIDELOG = "HideLog";

		// Token: 0x04009FF9 RID: 40953
		public const string LABEL_TWEEN_READY = "Ready";

		// Token: 0x04009FFA RID: 40954
		public const string SE_LOG_OPEN = "SE_LOG_OPEN";

		// Token: 0x04009FFB RID: 40955
		public const string SE_LOG_CLOSE = "SE_LOG_CLOSE";

		// Token: 0x04009FFC RID: 40956
		public SelectionButton m_DuelLogButton;

		// Token: 0x04009FFD RID: 40957
		protected ElementObjectManager m_EOManager;

		// Token: 0x04009FFE RID: 40958
		protected DuelLogScrollView m_ScrollView;

		// Token: 0x04009FFF RID: 40959
		protected Dictionary<string, int> m_DataTypeNumDict;

		// Token: 0x0400A000 RID: 40960
		protected Dictionary<Engine.ViewType, LogHandler> m_LogHandlerDict;

		// Token: 0x0400A001 RID: 40961
		protected Dictionary<string, LogItemHandler> m_UpdateItemDict;

		// Token: 0x0400A002 RID: 40962
		protected List<LogBaseData> m_LogBaseItemList;

		// Token: 0x0400A003 RID: 40963
		protected List<ShowTurnData> m_DataList_ShowTurn;

		// Token: 0x0400A004 RID: 40964
		protected List<ShowActionData> m_DataList_ShowAction;

		// Token: 0x0400A005 RID: 40965
		protected List<ShowChainData> m_DataList_ShowChain;

		// Token: 0x0400A006 RID: 40966
		protected List<ShowCardNameData> m_DataList_ShowCardName;

		// Token: 0x0400A007 RID: 40967
		protected List<ShowTextData> m_DataList_ShowText;

		// Token: 0x0400A008 RID: 40968
		protected List<ShowPhaseData> m_DataList_ShowPhase;

		// Token: 0x0400A009 RID: 40969
		protected List<ShowTagType> m_DataList_ShowTag;

		// Token: 0x0400A00A RID: 40970
		protected List<string> m_TemplateLabelList;

		// Token: 0x0400A00B RID: 40971
		protected Dictionary<int, Sprite> m_PlayerIconTable;

		// Token: 0x0400A00C RID: 40972
		protected Dictionary<int, Material> m_PlayerFrameTable;

		// Token: 0x0400A00D RID: 40973
		protected Dictionary<int, int> m_UidCardidTable;

		// Token: 0x0400A00E RID: 40974
		protected Queue<ShowActionData> m_CardMoveDataQueue;

		// Token: 0x0400A00F RID: 40975
		protected int m_ChainCount;

		// Token: 0x0400A010 RID: 40976
		protected bool m_IsEffectProcess;

		// Token: 0x0400A011 RID: 40977
		protected bool m_CloseTemprary;

		// Token: 0x0400A012 RID: 40978
		protected GameObject m_bgObj;

		// Token: 0x0400A013 RID: 40979
		protected DuelClient m_Host;

		// Token: 0x0400A014 RID: 40980
		private bool m_IsOpen;

		// Token: 0x0400A015 RID: 40981
		private SPSUMMONTYPE m_SpSummonFlag;

		// Token: 0x0400A016 RID: 40982
		private int m_SpSummonFlagCount;

		// Token: 0x0400A017 RID: 40983
		private string m_IconPath0;

		// Token: 0x0400A018 RID: 40984
		private string m_IconPath1;

		// Token: 0x0400A019 RID: 40985
		private string m_FramePath0;

		// Token: 0x0400A01A RID: 40986
		private string m_FramePath1;
	}
}
