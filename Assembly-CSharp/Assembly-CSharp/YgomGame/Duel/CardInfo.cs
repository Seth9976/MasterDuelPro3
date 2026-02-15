using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YgomSystem.UI;
using YgomSystem.YGomTMPro;

namespace YgomGame.Duel
{
	// Token: 0x02000CCF RID: 3279
	public class CardInfo : CardInfoBase
	{
		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06005D41 RID: 23873 RVA: 0x0000216A File Offset: 0x0000036A
		protected ExtendedTextMeshProUGUI m_SwitchCounterText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06005D42 RID: 23874 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image m_SwitchCounterIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06005D43 RID: 23875 RVA: 0x0000216A File Offset: 0x0000036A
		protected GameObject m_SwitchCounter
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06005D44 RID: 23876 RVA: 0x0000216A File Offset: 0x0000036A
		protected ExtendedTextMeshProUGUI m_DspContentSub
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06005D45 RID: 23877 RVA: 0x0000216A File Offset: 0x0000036A
		protected Tween m_TW_SwitchCounter
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06005D46 RID: 23878 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005D47 RID: 23879 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isMini
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

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06005D48 RID: 23880 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005D49 RID: 23881 RVA: 0x0000216D File Offset: 0x0000036D
		public UnityEvent onOpenEvent
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06005D4A RID: 23882 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005D4B RID: 23883 RVA: 0x0000216D File Offset: 0x0000036D
		public UnityEvent onCloseEvent
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06005D4C RID: 23884 RVA: 0x000F508C File Offset: 0x000F328C
		public Vector2 windowSize
		{
			get
			{
				return default(Vector2);
			}
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06005D4D RID: 23885 RVA: 0x0000216A File Offset: 0x0000036A
		private UiSwitchTweenAnimationController m_UISwitch
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005D4E RID: 23886 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsResourceLoaded(bool isMini)
		{
			return false;
		}

		// Token: 0x06005D4F RID: 23887 RVA: 0x0000216D File Offset: 0x0000036D
		public static void LoadResource(bool isMini)
		{
		}

		// Token: 0x06005D50 RID: 23888 RVA: 0x0000216D File Offset: 0x0000036D
		public static void UnloadResource(bool isMini)
		{
		}

		// Token: 0x06005D51 RID: 23889 RVA: 0x0000216D File Offset: 0x0000036D
		public static void LoadPrefab(bool isMini, UnityAction<GameObject> onFinished)
		{
		}

		// Token: 0x06005D52 RID: 23890 RVA: 0x0000216D File Offset: 0x0000036D
		public void Show(CardInfo.ShowPos miniPos)
		{
		}

		// Token: 0x06005D53 RID: 23891 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetMiniPos(CardInfo.ShowPos miniPos)
		{
		}

		// Token: 0x06005D54 RID: 23892 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCardByUniqueId(int uniqueId, CardInfo.ShowPos miniPos, bool force = false, bool fromCardSelectionList = false, int highlightRfxTableIndex = -1)
		{
		}

		// Token: 0x06005D55 RID: 23893 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCardByCardId(int cardid, CardInfo.ShowPos miniPos, int styleid = 1, bool force = true, bool fromCardSelectionList = false, int player = -1, int highlightRfxTableIndex = -1)
		{
		}

		// Token: 0x06005D56 RID: 23894 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCardLock(bool cardLock)
		{
		}

		// Token: 0x06005D57 RID: 23895 RVA: 0x0000216D File Offset: 0x0000036D
		public void Close()
		{
		}

		// Token: 0x06005D58 RID: 23896 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(DuelClient host, bool isLeft, bool ismini)
		{
		}

		// Token: 0x06005D59 RID: 23897 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetSelectorPriority(SharedDefinition.DuelSelectorPriority priority)
		{
		}

		// Token: 0x06005D5A RID: 23898 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetPos(CardInfo.ShowPos pos)
		{
		}

		// Token: 0x06005D5B RID: 23899 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetTitleArea()
		{
		}

		// Token: 0x06005D5C RID: 23900 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetTitleAreaByCardId()
		{
		}

		// Token: 0x06005D5D RID: 23901 RVA: 0x0000216D File Offset: 0x0000036D
		protected void ScrollReset()
		{
		}

		// Token: 0x06005D5E RID: 23902 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetCardArea()
		{
		}

		// Token: 0x06005D5F RID: 23903 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetParameterArea()
		{
		}

		// Token: 0x06005D60 RID: 23904 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetSwitchCounter()
		{
		}

		// Token: 0x06005D61 RID: 23905 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateCounter()
		{
		}

		// Token: 0x06005D62 RID: 23906 RVA: 0x0000216D File Offset: 0x0000036D
		private void SwitchToNextCounter()
		{
		}

		// Token: 0x06005D63 RID: 23907 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetDescriptionArea()
		{
		}

		// Token: 0x06005D64 RID: 23908 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetDspContent(int cardidorg, int effectid, int owner, bool isInField = false, bool isPdlPos = false, int effflag = 0)
		{
		}

		// Token: 0x06005D65 RID: 23909 RVA: 0x0000216D File Offset: 0x0000036D
		private void HighlightTextFromTable()
		{
		}

		// Token: 0x06005D66 RID: 23910 RVA: 0x0000216D File Offset: 0x0000036D
		private void HighlightProvess(string tohappentext, string happeningtext)
		{
		}

		// Token: 0x06005D67 RID: 23911 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator SetHighlightTextToFirstView()
		{
			return null;
		}

		// Token: 0x06005D68 RID: 23912 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetActivatedIconFromActivatedCardEffectNoDict(int cardId, int owner)
		{
		}

		// Token: 0x06005D69 RID: 23913 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetActivatedIconFromActivatedCardEffectNoList(List<int> effectNoList, int cardId, string ActivatedIconStr)
		{
		}

		// Token: 0x06005D6A RID: 23914 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupCardData()
		{
		}

		// Token: 0x06005D6B RID: 23915 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x040098AB RID: 39083
		private const string HIGHLIGHTTAG_STYLE_0 = "<style=\"notice2\">";

		// Token: 0x040098AC RID: 39084
		private const string HIGHLIGHTTAG_STYLE_1 = "</style>";

		// Token: 0x040098AD RID: 39085
		private const string HIGHLIGHTTAG_COLOR_77 = "<color=#777777>";

		// Token: 0x040098AE RID: 39086
		private const string HIGHLIGHTTAG_COLOR_FF = "<color=#FFFFFF>";

		// Token: 0x040098AF RID: 39087
		private const string HIGHLIGHTTAG_COLOR_MY = "<color=#00AEEF>";

		// Token: 0x040098B0 RID: 39088
		private const string HIGHLIGHTTAG_COLOR_RIVAL = "<color=#FF00FF>";

		// Token: 0x040098B1 RID: 39089
		private const string HIGHLIGHTTAG_COLOR_YELLOW = "<color=#FFFF00>";

		// Token: 0x040098B2 RID: 39090
		private const string HIGHLIGHTTAG_COLOR_END = "</color>";

		// Token: 0x040098B3 RID: 39091
		protected const string LABEL_IMG_SWITCHCOUNTER = "SwitchCounterIcon";

		// Token: 0x040098B4 RID: 39092
		protected const string LABEL_TXT_SWITCHCOUNTER = "SwitchCounterText";

		// Token: 0x040098B5 RID: 39093
		protected const string LABEL_GO_SWITCHCOUNTER = "SwitchCounter";

		// Token: 0x040098B6 RID: 39094
		protected const string LABEL_TW_ZOOMIN = "ZoomIn";

		// Token: 0x040098B7 RID: 39095
		protected const string LABEL_TW_ZOOMOUT = "ZoomOut";

		// Token: 0x040098B8 RID: 39096
		protected ExtendedTextMeshProUGUI m_SwitchCounterText_Property;

		// Token: 0x040098B9 RID: 39097
		protected Image m_SwitchCounterIcon_Property;

		// Token: 0x040098BA RID: 39098
		protected GameObject m_SwitchCounter_Property;

		// Token: 0x040098BB RID: 39099
		protected ExtendedTextMeshProUGUI m_DspContentSub_Property;

		// Token: 0x040098BC RID: 39100
		protected Tween m_TW_SwitchCounter_Property;

		// Token: 0x040098BD RID: 39101
		protected const string PATH_PREFAB = "Prefabs/Duel/UI/CardInfo";

		// Token: 0x040098BE RID: 39102
		protected const string PATH_PREFAB_MINI = "Prefabs/Duel/UI/CardInfoMini";

		// Token: 0x040098BF RID: 39103
		protected const int MAXCOUNTERNUM = 1;

		// Token: 0x040098C0 RID: 39104
		private bool cardLock;

		// Token: 0x040098C1 RID: 39105
		private bool m_FromCardSelectionList;

		// Token: 0x040098C2 RID: 39106
		private Coroutine m_CurrentCoroutine;

		// Token: 0x040098C3 RID: 39107
		private DuelClient m_Host;

		// Token: 0x040098C4 RID: 39108
		private int m_HighlightRfxTableIndex;

		// Token: 0x040098C5 RID: 39109
		private List<int> efxNoList;

		// Token: 0x040098C6 RID: 39110
		private int m_CurrentCounterIndex;

		// Token: 0x040098C7 RID: 39111
		private bool m_IsSwitching;

		// Token: 0x040098C8 RID: 39112
		private float m_DeltaTime;

		// Token: 0x040098C9 RID: 39113
		public CardInfo.ShowPos currentMiniPos;

		// Token: 0x040098CA RID: 39114
		private UiSwitchTweenAnimationController m_UISwitch_Property;

		// Token: 0x02000CD0 RID: 3280
		public enum ShowPos
		{
			// Token: 0x040098CC RID: 39116
			Left,
			// Token: 0x040098CD RID: 39117
			Right,
			// Token: 0x040098CE RID: 39118
			Top,
			// Token: 0x040098CF RID: 39119
			Bottom,
			// Token: 0x040098D0 RID: 39120
			None,
			// Token: 0x040098D1 RID: 39121
			NoChange
		}
	}
}
