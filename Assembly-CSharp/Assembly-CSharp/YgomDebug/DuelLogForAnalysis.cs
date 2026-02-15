using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using YgomGame.Duel;
using YgomSystem.ElementSystem;

namespace YgomDebug
{
	// Token: 0x02001154 RID: 4436
	public class DuelLogForAnalysis : MonoBehaviour
	{
		// Token: 0x170010C6 RID: 4294
		// (get) Token: 0x060083E7 RID: 33767 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isOpen
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010C7 RID: 4295
		// (get) Token: 0x060083E8 RID: 33768 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060083E9 RID: 33769 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x170010C8 RID: 4296
		// (get) Token: 0x060083EA RID: 33770 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060083EB RID: 33771 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x170010C9 RID: 4297
		// (get) Token: 0x060083EC RID: 33772 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool m_IsIndent
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010CA RID: 4298
		// (get) Token: 0x060083ED RID: 33773 RVA: 0x0000216A File Offset: 0x0000036A
		private List<LogBaseDataForAnalysis> m_LogBaseItemList
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170010CB RID: 4299
		// (get) Token: 0x060083EE RID: 33774 RVA: 0x0000216A File Offset: 0x0000036A
		private List<ShowTurnDataForAnalysis> m_DataList_ShowTurn
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170010CC RID: 4300
		// (get) Token: 0x060083EF RID: 33775 RVA: 0x0000216A File Offset: 0x0000036A
		private List<ShowActionDataForAnalysis> m_DataList_ShowAction
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170010CD RID: 4301
		// (get) Token: 0x060083F0 RID: 33776 RVA: 0x0000216A File Offset: 0x0000036A
		private List<ShowChainDataForAnalysis> m_DataList_ShowChain
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170010CE RID: 4302
		// (get) Token: 0x060083F1 RID: 33777 RVA: 0x0000216A File Offset: 0x0000036A
		private List<ShowCardNameDataForAnalysis> m_DataList_ShowCardName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170010CF RID: 4303
		// (get) Token: 0x060083F2 RID: 33778 RVA: 0x0000216A File Offset: 0x0000036A
		private List<ShowTextDataForAnalysis> m_DataList_ShowText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170010D0 RID: 4304
		// (get) Token: 0x060083F3 RID: 33779 RVA: 0x0000216A File Offset: 0x0000036A
		private List<ShowPhaseDataForAnalysis> m_DataList_ShowPhase
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170010D1 RID: 4305
		// (get) Token: 0x060083F4 RID: 33780 RVA: 0x0000216A File Offset: 0x0000036A
		private List<ShowTagTypeForAnalysis> m_DataList_ShowTag
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060083F5 RID: 33781 RVA: 0x0000216A File Offset: 0x0000036A
		public static DuelLogForAnalysis Create(Transform parent)
		{
			return null;
		}

		// Token: 0x060083F6 RID: 33782 RVA: 0x0000216D File Offset: 0x0000036D
		public void Open()
		{
		}

		// Token: 0x060083F7 RID: 33783 RVA: 0x0000216D File Offset: 0x0000036D
		public void Close()
		{
		}

		// Token: 0x060083F8 RID: 33784 RVA: 0x0000216D File Offset: 0x0000036D
		public void CloseTemprary()
		{
		}

		// Token: 0x060083F9 RID: 33785 RVA: 0x0000216D File Offset: 0x0000036D
		public void Resume()
		{
		}

		// Token: 0x060083FA RID: 33786 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetAlpha(float alpha)
		{
		}

		// Token: 0x060083FB RID: 33787 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetAutoScroll(bool auto)
		{
		}

		// Token: 0x060083FC RID: 33788 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetShortkeyIconVisible(bool visible)
		{
		}

		// Token: 0x060083FD RID: 33789 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddLogData(Engine.ViewType viewType, int param1, int param2, int param3)
		{
		}

		// Token: 0x060083FE RID: 33790 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddLogText(int player, string text)
		{
		}

		// Token: 0x060083FF RID: 33791 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateLogData(DuelLogForAnalysis.DuelLogData duelLogData)
		{
		}

		// Token: 0x06008400 RID: 33792 RVA: 0x0000216D File Offset: 0x0000036D
		protected void OnUpdateListItemBase(GameObject gob, int baseitemindex)
		{
		}

		// Token: 0x06008401 RID: 33793 RVA: 0x0000216D File Offset: 0x0000036D
		protected void OnUpdateShowTurn(GameObject eom, int dataindex)
		{
		}

		// Token: 0x06008402 RID: 33794 RVA: 0x0000216D File Offset: 0x0000036D
		protected void OnUpdateShowAction(GameObject eom, int dataindex)
		{
		}

		// Token: 0x06008403 RID: 33795 RVA: 0x0000216D File Offset: 0x0000036D
		protected void OnUpdateShowChain(GameObject eom, int dataindex)
		{
		}

		// Token: 0x06008404 RID: 33796 RVA: 0x0000216D File Offset: 0x0000036D
		protected void OnUpdateShowTag(GameObject eom, int dataindex)
		{
		}

		// Token: 0x06008405 RID: 33797 RVA: 0x0000216D File Offset: 0x0000036D
		protected void OnUpdateShowCardName(GameObject eom, int dataindex)
		{
		}

		// Token: 0x06008406 RID: 33798 RVA: 0x0000216D File Offset: 0x0000036D
		protected void OnUpdateShowText(GameObject eom, int dataindex)
		{
		}

		// Token: 0x06008407 RID: 33799 RVA: 0x0000216D File Offset: 0x0000036D
		protected void OnUpdateShowPhase(GameObject eom, int dataindex)
		{
		}

		// Token: 0x06008408 RID: 33800 RVA: 0x0000216D File Offset: 0x0000036D
		protected void OnUpdateSeparateLine(GameObject eom, int dataindex)
		{
		}

		// Token: 0x06008409 RID: 33801 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize()
		{
		}

		// Token: 0x0600840A RID: 33802 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnScrollViewReady()
		{
		}

		// Token: 0x0600840B RID: 33803 RVA: 0x0000216D File Offset: 0x0000036D
		private void initializeComponent()
		{
		}

		// Token: 0x0600840C RID: 33804 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeDict()
		{
		}

		// Token: 0x0600840D RID: 33805 RVA: 0x000029CC File Offset: 0x00000BCC
		private LOGACTIONTYPE GetActionType(Engine.CardMoveType moveType, Engine.CardStatus stFrom, Engine.CardStatus stTo)
		{
			return LOGACTIONTYPE.ACTION_NONE;
		}

		// Token: 0x0600840E RID: 33806 RVA: 0x0000216D File Offset: 0x0000036D
		private void CheckSpSummon(ref LOGACTIONTYPE actiontype)
		{
		}

		// Token: 0x0600840F RID: 33807 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetSelectorPriority(SharedDefinition.DuelSelectorPriority priority)
		{
		}

		// Token: 0x06008410 RID: 33808 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SetPlayerIcon(Image img, int playerid)
		{
			return false;
		}

		// Token: 0x06008411 RID: 33809 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool CheckCardidValid(int cardid)
		{
			return false;
		}

		// Token: 0x0400BF44 RID: 48964
		public const int INVALIDCARDID = 0;

		// Token: 0x0400BF45 RID: 48965
		public const string LABEL_SHOWTURN = "ShowTurn";

		// Token: 0x0400BF46 RID: 48966
		public const string LABEL_SHOWACTION = "ShowAction";

		// Token: 0x0400BF47 RID: 48967
		public const string LABEL_SHOWCHAIN = "ShowChain";

		// Token: 0x0400BF48 RID: 48968
		public const string LABEL_SHOWTAG = "ShowTag";

		// Token: 0x0400BF49 RID: 48969
		public const string LABEL_SHOWCARDNAME = "ShowCardName";

		// Token: 0x0400BF4A RID: 48970
		public const string LABEL_SHOWTEXT = "ShowText";

		// Token: 0x0400BF4B RID: 48971
		public const string LABEL_SHOWPHASE = "ShowPhase";

		// Token: 0x0400BF4C RID: 48972
		public const string LABEL_SAPERATELINE = "SeparateLine";

		// Token: 0x0400BF4D RID: 48973
		public const string LABEL_ROOT = "Root";

		// Token: 0x0400BF4E RID: 48974
		public const string LABEL_SCROLLUP = "ScrollUp";

		// Token: 0x0400BF4F RID: 48975
		public const string LABEL_SCROLLDOWN = "ScrollDown";

		// Token: 0x0400BF50 RID: 48976
		public const string LABEL_SCROLLVIEW = "ScrollView";

		// Token: 0x0400BF51 RID: 48977
		public const string LABEL_BG = "Bg";

		// Token: 0x0400BF52 RID: 48978
		public const string LABEL_TWEEN_SHOWLOG = "ShowLog";

		// Token: 0x0400BF53 RID: 48979
		public const string LABEL_TWEEN_HIDELOG = "HideLog";

		// Token: 0x0400BF54 RID: 48980
		public const string LABEL_TWEEN_READY = "Ready";

		// Token: 0x0400BF55 RID: 48981
		public const string SE_LOG_OPEN = "SE_LOG_OPEN";

		// Token: 0x0400BF56 RID: 48982
		public const string SE_LOG_CLOSE = "SE_LOG_CLOSE";

		// Token: 0x0400BF57 RID: 48983
		private const string PREFAB_NAME = "Prefabs/VC/Debug/DuelLogForAnalysis";

		// Token: 0x0400BF58 RID: 48984
		public DuelLogForAnalysis.DuelLogData m_DuelLogData;

		// Token: 0x0400BF59 RID: 48985
		protected ElementObjectManager m_EOManager;

		// Token: 0x0400BF5A RID: 48986
		protected DuelLogScrollViewForAnalysis m_ScrollView;

		// Token: 0x0400BF5B RID: 48987
		protected Dictionary<string, int> m_DataTypeNumDict;

		// Token: 0x0400BF5C RID: 48988
		protected Dictionary<Engine.ViewType, LogHandler> m_LogHandlerDict;

		// Token: 0x0400BF5D RID: 48989
		protected Dictionary<string, LogItemHandler> m_UpdateItemDict;

		// Token: 0x0400BF5E RID: 48990
		protected List<string> m_TemplateLabelList;

		// Token: 0x0400BF5F RID: 48991
		protected Dictionary<int, Sprite> m_PlayerIconTable;

		// Token: 0x0400BF60 RID: 48992
		protected Dictionary<int, Material> m_PlayerFrameTable;

		// Token: 0x0400BF61 RID: 48993
		protected bool m_CloseTemprary;

		// Token: 0x0400BF62 RID: 48994
		protected GameObject m_bgObj;

		// Token: 0x0400BF63 RID: 48995
		private bool m_IsOpen;

		// Token: 0x0400BF64 RID: 48996
		private SPSUMMONTYPE m_SpSummonFlag;

		// Token: 0x0400BF65 RID: 48997
		private int m_SpSummonFlagCount;

		// Token: 0x0400BF66 RID: 48998
		private int m_ChainCount;

		// Token: 0x02001155 RID: 4437
		[Serializable]
		public class SerializableList<T>
		{
			// Token: 0x0400BF67 RID: 48999
			public List<T> list;
		}

		// Token: 0x02001156 RID: 4438
		[Serializable]
		public class DuelLogData
		{
			// Token: 0x06008414 RID: 33812 RVA: 0x0000216A File Offset: 0x0000036A
			public static DuelLogForAnalysis.DuelLogData CreateFromJson(string json)
			{
				return null;
			}

			// Token: 0x06008415 RID: 33813 RVA: 0x0000216A File Offset: 0x0000036A
			public static DuelLogForAnalysis.DuelLogData CreateFromDuelLogControllerData(string json)
			{
				return null;
			}

			// Token: 0x06008416 RID: 33814 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetDataFromDuelLogData(List<LogBaseData> logBaseDatas, List<ShowTurnData> showTurnDatas, List<ShowActionData> showActionDatas, List<ShowChainData> showChainDatas, List<ShowCardNameData> showCardNameDatas, List<ShowTextData> showTextDatas, List<ShowPhaseData> showPhaseDatas, List<ShowTagType> showTagTypes, List<string> dataLabelList, List<string> textTable)
			{
			}

			// Token: 0x0400BF68 RID: 49000
			[SerializeField]
			public DuelLogForAnalysis.SerializableList<LogBaseDataForAnalysis> m_LogBaseItemList;

			// Token: 0x0400BF69 RID: 49001
			[SerializeField]
			public DuelLogForAnalysis.SerializableList<ShowTurnDataForAnalysis> m_DataList_ShowTurn;

			// Token: 0x0400BF6A RID: 49002
			[SerializeField]
			public DuelLogForAnalysis.SerializableList<ShowActionDataForAnalysis> m_DataList_ShowAction;

			// Token: 0x0400BF6B RID: 49003
			[SerializeField]
			public DuelLogForAnalysis.SerializableList<ShowChainDataForAnalysis> m_DataList_ShowChain;

			// Token: 0x0400BF6C RID: 49004
			[SerializeField]
			public DuelLogForAnalysis.SerializableList<ShowCardNameDataForAnalysis> m_DataList_ShowCardName;

			// Token: 0x0400BF6D RID: 49005
			[SerializeField]
			public DuelLogForAnalysis.SerializableList<ShowTextDataForAnalysis> m_DataList_ShowText;

			// Token: 0x0400BF6E RID: 49006
			[SerializeField]
			public DuelLogForAnalysis.SerializableList<ShowPhaseDataForAnalysis> m_DataList_ShowPhase;

			// Token: 0x0400BF6F RID: 49007
			[SerializeField]
			public DuelLogForAnalysis.SerializableList<ShowTagTypeForAnalysis> m_DataList_ShowTag;

			// Token: 0x0400BF70 RID: 49008
			[SerializeField]
			public DuelLogForAnalysis.SerializableList<string> m_DataLabelList;

			// Token: 0x0400BF71 RID: 49009
			[SerializeField]
			public DuelLogForAnalysis.SerializableList<string> m_TextTable;
		}
	}
}
