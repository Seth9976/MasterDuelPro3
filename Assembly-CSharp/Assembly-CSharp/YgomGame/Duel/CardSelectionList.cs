using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;
using YgomSystem.YGomTMPro;

namespace YgomGame.Duel
{
	// Token: 0x02000D06 RID: 3334
	public class CardSelectionList : MonoBehaviour, IGenericScrollViewSupport
	{
		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x06005FCB RID: 24523 RVA: 0x0000216A File Offset: 0x0000036A
		private string LABEL_TW_CLOSE
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x06005FCC RID: 24524 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool m_IsDecisionActive
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x06005FCD RID: 24525 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool m_IsDecideActive
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x06005FCE RID: 24526 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool m_IsCloseActive
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x06005FCF RID: 24527 RVA: 0x0000216A File Offset: 0x0000036A
		private string remainFormat
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x06005FD0 RID: 24528 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005FD1 RID: 24529 RVA: 0x0000216D File Offset: 0x0000036D
		public Action OnOpenBegin
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

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x06005FD2 RID: 24530 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005FD3 RID: 24531 RVA: 0x0000216D File Offset: 0x0000036D
		public Action OnOpenEnd
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

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x06005FD4 RID: 24532 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005FD5 RID: 24533 RVA: 0x0000216D File Offset: 0x0000036D
		public Action OnCloseBegin
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

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x06005FD6 RID: 24534 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005FD7 RID: 24535 RVA: 0x0000216D File Offset: 0x0000036D
		public Action OnCloseEnd
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

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x06005FD8 RID: 24536 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsActive
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x06005FD9 RID: 24537 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsDisp
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x06005FDA RID: 24538 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isCancelable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x06005FDB RID: 24539 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isTweenPlaying
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x06005FDC RID: 24540 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isFieldViewing
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x06005FDD RID: 24541 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005FDE RID: 24542 RVA: 0x0000216D File Offset: 0x0000036D
		public Action<bool, List<int>> decidedCallback
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

		// Token: 0x06005FDF RID: 24543 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(DuelClient client, Transform parent, UnityAction<CardSelectionList> onFisnih)
		{
		}

		// Token: 0x06005FE0 RID: 24544 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize(DuelClient client, Transform parent)
		{
		}

		// Token: 0x06005FE1 RID: 24545 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetList(CardSelectionList.ListType type, Action onFinished, string title, bool nocancel = false, bool decidable = false, Action onCancelled = null, bool isWaitInput = false, bool isSort = true)
		{
		}

		// Token: 0x06005FE2 RID: 24546 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitComponents()
		{
		}

		// Token: 0x06005FE3 RID: 24547 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnScrollRectValueChange(Vector2 bias)
		{
		}

		// Token: 0x06005FE4 RID: 24548 RVA: 0x000029CC File Offset: 0x00000BCC
		private int GetSeperateInfoIndexByDataIndex(int dataindex)
		{
			return 0;
		}

		// Token: 0x06005FE5 RID: 24549 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitVariables()
		{
		}

		// Token: 0x06005FE6 RID: 24550 RVA: 0x0000216A File Offset: 0x0000036A
		private CardSelectionListGroupLabel GetGroupLabel(int player, CardSelectionList.CardLocateType locate)
		{
			return null;
		}

		// Token: 0x06005FE7 RID: 24551 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetDecideVisible(bool visible, CardSelectionList.ListType type = CardSelectionList.ListType.Attack)
		{
		}

		// Token: 0x06005FE8 RID: 24552 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetDecisionVisible(bool visible)
		{
		}

		// Token: 0x06005FE9 RID: 24553 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetDecisionEnable(bool enable)
		{
		}

		// Token: 0x06005FEA RID: 24554 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetCloseVisible(bool visible, bool decidable)
		{
		}

		// Token: 0x06005FEB RID: 24555 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetCloseEnable(bool enable)
		{
		}

		// Token: 0x06005FEC RID: 24556 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetDecideButtonTransition()
		{
		}

		// Token: 0x06005FED RID: 24557 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetCloseButtonTransition()
		{
		}

		// Token: 0x06005FEE RID: 24558 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetDecisionButtonTransition()
		{
		}

		// Token: 0x06005FEF RID: 24559 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetDecideEnable(bool valid, bool reachMax = false)
		{
		}

		// Token: 0x06005FF0 RID: 24560 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetFieldViewActive(bool field_view_active)
		{
		}

		// Token: 0x06005FF1 RID: 24561 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetArrowVisible()
		{
		}

		// Token: 0x06005FF2 RID: 24562 RVA: 0x0000216D File Offset: 0x0000036D
		private void SwitchTween(string startlabel, string stoplabel = "")
		{
		}

		// Token: 0x06005FF3 RID: 24563 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetSelectionListWidth(int itemcount)
		{
		}

		// Token: 0x06005FF4 RID: 24564 RVA: 0x0000216A File Offset: 0x0000036A
		private ListCardData CreateChainData(int position, int index)
		{
			return null;
		}

		// Token: 0x06005FF5 RID: 24565 RVA: 0x0000216A File Offset: 0x0000036A
		private ListCardData CreateListCardData(int player, int position, int index, ListCardData.DataSource dataSource, bool forceinsight = false)
		{
			return null;
		}

		// Token: 0x06005FF6 RID: 24566 RVA: 0x0000216A File Offset: 0x0000036A
		private List<ListCardData> CreateActivableList(CardSelectionList.ListType type)
		{
			return null;
		}

		// Token: 0x06005FF7 RID: 24567 RVA: 0x0000216A File Offset: 0x0000036A
		private List<ListCardData> CreateDeckList(List<ListCardData> ret = null)
		{
			return null;
		}

		// Token: 0x06005FF8 RID: 24568 RVA: 0x0000216A File Offset: 0x0000036A
		private List<ListCardData> CreatePosSelectList(List<ListCardData> ret = null)
		{
			return null;
		}

		// Token: 0x06005FF9 RID: 24569 RVA: 0x0000216A File Offset: 0x0000036A
		private List<ListCardData> CreateNormalList()
		{
			return null;
		}

		// Token: 0x06005FFA RID: 24570 RVA: 0x0000216A File Offset: 0x0000036A
		private List<ListCardData> CreateSelectionList()
		{
			return null;
		}

		// Token: 0x06005FFB RID: 24571 RVA: 0x0000216A File Offset: 0x0000036A
		private List<ListCardData> CreateOpponentHandList()
		{
			return null;
		}

		// Token: 0x06005FFC RID: 24572 RVA: 0x0000216A File Offset: 0x0000036A
		private List<ListCardData> CreateDeckTopList(int player)
		{
			return null;
		}

		// Token: 0x06005FFD RID: 24573 RVA: 0x0000216A File Offset: 0x0000036A
		private List<ListCardData> CreateCheckCardList()
		{
			return null;
		}

		// Token: 0x06005FFE RID: 24574 RVA: 0x0000216D File Offset: 0x0000036D
		private void RemoveCardBadge(int dataindex, ref ListCardData selecteddata)
		{
		}

		// Token: 0x06005FFF RID: 24575 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator SelectIndex(int dataindexsort)
		{
			return null;
		}

		// Token: 0x06006000 RID: 24576 RVA: 0x0000216D File Offset: 0x0000036D
		private void ChangeDecideButtonText(ListCardData cardData)
		{
		}

		// Token: 0x06006001 RID: 24577 RVA: 0x0000216D File Offset: 0x0000036D
		private void AddTitleInfo()
		{
		}

		// Token: 0x06006002 RID: 24578 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetCursor(int dataindex)
		{
		}

		// Token: 0x06006003 RID: 24579 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickCard(ListCard card)
		{
		}

		// Token: 0x06006004 RID: 24580 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnHoldCard(ListCard card)
		{
		}

		// Token: 0x06006005 RID: 24581 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDoubleClickCard(ListCard card)
		{
		}

		// Token: 0x06006006 RID: 24582 RVA: 0x0000216A File Offset: 0x0000036A
		private List<ListCardData> GetGroupedDataList(List<ListCardData> infoList, bool isSort = true)
		{
			return null;
		}

		// Token: 0x06006007 RID: 24583 RVA: 0x0000216A File Offset: 0x0000036A
		private List<ListCardData> ListGroupByLocate(List<ListCardData> infoList, int playerid, bool isSort = true)
		{
			return null;
		}

		// Token: 0x06006008 RID: 24584 RVA: 0x0000216D File Offset: 0x0000036D
		private void CombineList(List<ListCardData> orglist, List<ListCardData> dstlist, CardSelectionList.CardLocateType locate, int playerid)
		{
		}

		// Token: 0x06006009 RID: 24585 RVA: 0x0000216D File Offset: 0x0000036D
		private void RecycleGroupLabel()
		{
		}

		// Token: 0x0600600A RID: 24586 RVA: 0x0000216A File Offset: 0x0000036A
		private List<ListCardData> ListSortByCard(List<ListCardData> infoList)
		{
			return null;
		}

		// Token: 0x0600600B RID: 24587 RVA: 0x000029CC File Offset: 0x00000BCC
		private int GetMonsterSortPoint(int cardid)
		{
			return 0;
		}

		// Token: 0x0600600C RID: 24588 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupSearchList(string path)
		{
		}

		// Token: 0x0600600D RID: 24589 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetListImpl(CardSelectionList.ListType type, Action callback, bool decidable, bool nocancel, Action cancelCb, bool isSort = true)
		{
		}

		// Token: 0x0600600E RID: 24590 RVA: 0x0000216D File Offset: 0x0000036D
		public void Open()
		{
		}

		// Token: 0x0600600F RID: 24591 RVA: 0x0000216D File Offset: 0x0000036D
		public void Close(bool forceclose = false)
		{
		}

		// Token: 0x06006010 RID: 24592 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnDecide()
		{
		}

		// Token: 0x06006011 RID: 24593 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnCancel(bool decide, bool playse = true)
		{
		}

		// Token: 0x06006012 RID: 24594 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool OnBack()
		{
			return false;
		}

		// Token: 0x06006013 RID: 24595 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool OnDecideCardInField(int player, int position, int index)
		{
			return false;
		}

		// Token: 0x06006014 RID: 24596 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnFinishClose()
		{
		}

		// Token: 0x06006015 RID: 24597 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetSelectorsEnable(bool enable)
		{
		}

		// Token: 0x06006016 RID: 24598 RVA: 0x0000216D File Offset: 0x0000036D
		private void ManualDownTransition(float position)
		{
		}

		// Token: 0x06006017 RID: 24599 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemSetData(GameObject gob, int dataindex)
		{
		}

		// Token: 0x06006018 RID: 24600 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemInitialize(GameObject gob)
		{
		}

		// Token: 0x06006019 RID: 24601 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnGsvStanby()
		{
		}

		// Token: 0x0600601A RID: 24602 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCursorToCurrentIndex()
		{
		}

		// Token: 0x0600601B RID: 24603 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SelectCardByUniqueId(int uniqueid)
		{
			return false;
		}

		// Token: 0x0600601C RID: 24604 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetTitle(string title)
		{
		}

		// Token: 0x0600601D RID: 24605 RVA: 0x0000216A File Offset: 0x0000036A
		private List<ListCardData> CreateSearchList(string searchText)
		{
			return null;
		}

		// Token: 0x0600601E RID: 24606 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupSelectorPriority()
		{
		}

		// Token: 0x0600601F RID: 24607 RVA: 0x0000216D File Offset: 0x0000036D
		public void ShowCounter(Engine.DialogRitualType type, int remainCount, int maxCount)
		{
		}

		// Token: 0x06006020 RID: 24608 RVA: 0x0000216D File Offset: 0x0000036D
		public void HideOptionalGroup()
		{
		}

		// Token: 0x06006021 RID: 24609 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupCounterMethod(bool show, Engine.DialogRitualType type, int remainCount, int maxCount)
		{
		}

		// Token: 0x06006022 RID: 24610 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupCounter(Engine.DialogRitualType type, int remainCount, int maxCount)
		{
		}

		// Token: 0x06006023 RID: 24611 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator SetStartScale()
		{
			return null;
		}

		// Token: 0x06006024 RID: 24612 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupCountGroup(CardSelectionList.CountMode mode, int maxCount)
		{
		}

		// Token: 0x06006025 RID: 24613 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCount(int remainNum)
		{
		}

		// Token: 0x06006026 RID: 24614 RVA: 0x0000216D File Offset: 0x0000036D
		private void DestroyCountObjectList()
		{
		}

		// Token: 0x06006027 RID: 24615 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetStarFadeEnable(bool enable, ListCard card)
		{
		}

		// Token: 0x06006028 RID: 24616 RVA: 0x0000216D File Offset: 0x0000036D
		public void ShowDiscardRemain(int remain)
		{
		}

		// Token: 0x06006029 RID: 24617 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupDiscardMethod(bool dispDiscard, int remain)
		{
		}

		// Token: 0x0600602A RID: 24618 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetRemain(int remainNum)
		{
		}

		// Token: 0x0600602B RID: 24619 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemExit(GameObject gob, int dataindex)
		{
		}

		// Token: 0x0600602C RID: 24620 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x0600602D RID: 24621 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x04009AB6 RID: 39606
		private const string PATH_PREHAB = "Prefabs/Duel/CardSelectionList";

		// Token: 0x04009AB7 RID: 39607
		private const string PATH_PREHAB_MOBILE = "Prefabs/Duel/CardSelectionList_Mobile";

		// Token: 0x04009AB8 RID: 39608
		private const string LABEL_EO_TEXTTITLE = "TextTitle";

		// Token: 0x04009AB9 RID: 39609
		private const string LABEL_EO_TITLE = "Title";

		// Token: 0x04009ABA RID: 39610
		private const string LABEL_EO_DECIDEBUTTON = "DecideButton";

		// Token: 0x04009ABB RID: 39611
		private const string LABEL_EO_DECISIONBUTTON = "DecisionButton";

		// Token: 0x04009ABC RID: 39612
		private const string LABEL_EO_CLOSEBUTTON = "CloseButton";

		// Token: 0x04009ABD RID: 39613
		private const string LABEL_EO_VIEWTOGGLE = "ViewToggle";

		// Token: 0x04009ABE RID: 39614
		private const string LABEL_EO_SCROLLVIEW = "ScrollView";

		// Token: 0x04009ABF RID: 39615
		private const string LABEL_EO_DECIDETEXT = "DecideText";

		// Token: 0x04009AC0 RID: 39616
		private const string LABEL_EO_CLOSETEXT = "CloseText";

		// Token: 0x04009AC1 RID: 39617
		private const string LABEL_EO_LISTWINDOW = "ListWindow";

		// Token: 0x04009AC2 RID: 39618
		private const string LABEL_EO_GROUPLABELTEMPLATE = "GroupLabelTemplate";

		// Token: 0x04009AC3 RID: 39619
		private const string LABEL_EO_GROUPLABELPOOL = "GroupLabelPool";

		// Token: 0x04009AC4 RID: 39620
		private const string LABEL_EO_GROUPSCROLLLEFT = "GroupScrollLeftShortCutSbtn";

		// Token: 0x04009AC5 RID: 39621
		private const string LABEL_EO_GROUPSCROLLRIGHT = "GroupScrollRightShortCutSbtn";

		// Token: 0x04009AC6 RID: 39622
		private const string LABEL_EO_FIELDVIEWBUTTON = "FieldViewButton";

		// Token: 0x04009AC7 RID: 39623
		private const string LABEL_EO_FIELDVIEWON = "FieldViewOn";

		// Token: 0x04009AC8 RID: 39624
		private const string LABEL_EO_FIELDVIEWOFF = "FieldViewOff";

		// Token: 0x04009AC9 RID: 39625
		private const string LABEL_EO_FIELDVIEWSHORTCUT = "FieldViewShortcut";

		// Token: 0x04009ACA RID: 39626
		private const string LABEL_EO_COUNTERGROUP = "CounterGroup";

		// Token: 0x04009ACB RID: 39627
		private const string LABEL_EO_STARGROUP = "StarGroup";

		// Token: 0x04009ACC RID: 39628
		private const string LABEL_EO_STARTEMPLATE = "StarTemplate";

		// Token: 0x04009ACD RID: 39629
		private const string LABEL_EO_LINKGROUP = "LinkGroup";

		// Token: 0x04009ACE RID: 39630
		private const string LABEL_EO_LINKTEMPLATE = "LinkTemplate";

		// Token: 0x04009ACF RID: 39631
		private const string LABEL_EO_ATTACKGROUP = "AttackGroup";

		// Token: 0x04009AD0 RID: 39632
		private const string LABEL_EO_CURRENTPARAM = "CurrentParam";

		// Token: 0x04009AD1 RID: 39633
		private const string LABEL_EO_REQUIREPARAM = "RequireParam";

		// Token: 0x04009AD2 RID: 39634
		private const string LABEL_EO_DICARDGROUP = "DiscardGroup";

		// Token: 0x04009AD3 RID: 39635
		private const string LABEL_EO_DICARDREMAIN = "DiscardRemain";

		// Token: 0x04009AD4 RID: 39636
		private const string LABEL_EO_INPUTFIELD = "InputField";

		// Token: 0x04009AD5 RID: 39637
		private const string LABEL_EO_NOHITTEXT = "NoHitText";

		// Token: 0x04009AD6 RID: 39638
		private const string LABEL_EO_CARDNAME = "CardName";

		// Token: 0x04009AD7 RID: 39639
		private const string LABEL_EO_ARROWLEFT = "ArrowLeft";

		// Token: 0x04009AD8 RID: 39640
		private const string LABEL_EO_ARROWRIGHT = "ArrowRight";

		// Token: 0x04009AD9 RID: 39641
		private const string LABEL_EO_INPUTBUTTON = "InputButton";

		// Token: 0x04009ADA RID: 39642
		private const string LABEL_TW_OPEN = "In";

		// Token: 0x04009ADB RID: 39643
		private const string LABEL_TW_VIEWTOGGLE = "ViewToggle";

		// Token: 0x04009ADC RID: 39644
		private const string LABEL_TW_DECIDEBUTTON = "DecideButton";

		// Token: 0x04009ADD RID: 39645
		private const string LABEL_TW_CLOSEBUTTON = "CloseButton";

		// Token: 0x04009ADE RID: 39646
		private const string LABEL_TW_FADEIN = "FadeIn";

		// Token: 0x04009ADF RID: 39647
		private const string LABEL_TW_FADEOUT = "FadeOut";

		// Token: 0x04009AE0 RID: 39648
		private const string LABEL_TW_NORMALLIST = "Normal";

		// Token: 0x04009AE1 RID: 39649
		private const string LABEL_TW_CHAINLIST = "Chain";

		// Token: 0x04009AE2 RID: 39650
		public const int INVALIDRUNLISTINEDX = -1;

		// Token: 0x04009AE3 RID: 39651
		private const int TITLETEXT_INDEX_DEFAULT = -1;

		// Token: 0x04009AE4 RID: 39652
		private const int TITLETEXT_INDEX_GENERIC = 266;

		// Token: 0x04009AE5 RID: 39653
		private bool m_IsWaitInput;

		// Token: 0x04009AE6 RID: 39654
		private Transform m_Parent;

		// Token: 0x04009AE7 RID: 39655
		private static readonly Dictionary<CardSelectionList.ListType, uint> CmdMask;

		// Token: 0x04009AE8 RID: 39656
		[SerializeField]
		private float MaxWidth;

		// Token: 0x04009AE9 RID: 39657
		[SerializeField]
		private float MinWidth;

		// Token: 0x04009AEA RID: 39658
		private DuelClient m_DuelClient;

		// Token: 0x04009AEB RID: 39659
		private ExtendedTextMeshProUGUI m_TextTitle;

		// Token: 0x04009AEC RID: 39660
		private Selector m_Selector;

		// Token: 0x04009AED RID: 39661
		private Selector[] m_SelectorGroup;

		// Token: 0x04009AEE RID: 39662
		private ElementObjectManager m_Eom;

		// Token: 0x04009AEF RID: 39663
		private GenericScrollView m_Gsv;

		// Token: 0x04009AF0 RID: 39664
		private ScrollRect m_ScrollRect;

		// Token: 0x04009AF1 RID: 39665
		private SelectionButton m_DecideButton;

		// Token: 0x04009AF2 RID: 39666
		private SelectionButton m_CloseButton;

		// Token: 0x04009AF3 RID: 39667
		private SelectionButton m_DecisionButton;

		// Token: 0x04009AF4 RID: 39668
		private RectTransform m_ListWindowRT;

		// Token: 0x04009AF5 RID: 39669
		private InputFieldWidget m_InputFiled;

		// Token: 0x04009AF6 RID: 39670
		private GameObject m_NoHitText;

		// Token: 0x04009AF7 RID: 39671
		private GameObject m_GroupLabelTemplate;

		// Token: 0x04009AF8 RID: 39672
		private List<ListCardData> m_GroupedDataList;

		// Token: 0x04009AF9 RID: 39673
		private List<int> m_SelectedList;

		// Token: 0x04009AFA RID: 39674
		private List<CardSelectionList.SeperateInfo> m_SeperateInfoList;

		// Token: 0x04009AFB RID: 39675
		private Stack<CardSelectionListGroupLabel> m_GroupLabelFreeStack;

		// Token: 0x04009AFC RID: 39676
		private RectTransform m_GroupLabelPool;

		// Token: 0x04009AFD RID: 39677
		private ExtendedTextMeshProUGUI m_CardName;

		// Token: 0x04009AFE RID: 39678
		private GameObject m_FieldViewIconOn;

		// Token: 0x04009AFF RID: 39679
		private GameObject m_FieldViewIconOff;

		// Token: 0x04009B00 RID: 39680
		private SelectionButton m_ArrowLeft;

		// Token: 0x04009B01 RID: 39681
		private SelectionButton m_ArrowRight;

		// Token: 0x04009B02 RID: 39682
		private GameObject counterGroup;

		// Token: 0x04009B03 RID: 39683
		private GameObject starGroup;

		// Token: 0x04009B04 RID: 39684
		private GameObject linkGroup;

		// Token: 0x04009B05 RID: 39685
		private GameObject atkGroup;

		// Token: 0x04009B06 RID: 39686
		private ElementObjectManager starTemplate;

		// Token: 0x04009B07 RID: 39687
		private ElementObjectManager linkTemplate;

		// Token: 0x04009B08 RID: 39688
		private ExtendedTextMeshProUGUI textRequireParam;

		// Token: 0x04009B09 RID: 39689
		private ExtendedTextMeshProUGUI textCurrentParam;

		// Token: 0x04009B0A RID: 39690
		private CardSelectionList.CountMode currentCountMode;

		// Token: 0x04009B0B RID: 39691
		private int maxCount;

		// Token: 0x04009B0C RID: 39692
		private int currentCount;

		// Token: 0x04009B0D RID: 39693
		private List<ElementObjectManager> countObjects;

		// Token: 0x04009B0E RID: 39694
		private GameObject discardGroup;

		// Token: 0x04009B0F RID: 39695
		private ExtendedTextMeshProUGUI textDiscardRemain;

		// Token: 0x04009B10 RID: 39696
		private bool m_FieldViewing;

		// Token: 0x04009B11 RID: 39697
		private bool m_Closing;

		// Token: 0x04009B12 RID: 39698
		private bool m_Opening;

		// Token: 0x04009B13 RID: 39699
		private bool m_Cancable;

		// Token: 0x04009B14 RID: 39700
		private bool m_UseDecideButton;

		// Token: 0x04009B15 RID: 39701
		private int m_ChangeTitleFlag;

		// Token: 0x04009B16 RID: 39702
		private int m_SelectMaxNum;

		// Token: 0x04009B17 RID: 39703
		private int m_SelectMinNum;

		// Token: 0x04009B18 RID: 39704
		private string m_Titlemsg;

		// Token: 0x04009B19 RID: 39705
		private CardSelectionList.ListType m_ListType;

		// Token: 0x04009B1A RID: 39706
		private CanvasGroup m_CanvasGroup;

		// Token: 0x04009B1B RID: 39707
		private PopUpTextForSelectionList m_PppupText;

		// Token: 0x04009B1C RID: 39708
		private List<int> candidateCards;

		// Token: 0x04009B1D RID: 39709
		private Queue<CardSelectionList.SetListDesc> m_DescQueue;

		// Token: 0x04009B1E RID: 39710
		private Tween m_OpenTween;

		// Token: 0x04009B1F RID: 39711
		private Tween m_CloseTween;

		// Token: 0x04009B20 RID: 39712
		private int m_InputBlockCounter;

		// Token: 0x04009B21 RID: 39713
		private string cachedFmt;

		// Token: 0x04009B22 RID: 39714
		public Action OnFinished;

		// Token: 0x04009B23 RID: 39715
		public Action OnCanceled;

		// Token: 0x04009B24 RID: 39716
		public Action<Engine.CardStatus, bool> OnSelected;

		// Token: 0x04009B25 RID: 39717
		public Action OnUnselect;

		// Token: 0x04009B26 RID: 39718
		public int CurrentCursoredDataIndex;

		// Token: 0x02000D07 RID: 3335
		private class SeperateInfo
		{
			// Token: 0x17000AAA RID: 2730
			// (get) Token: 0x0600602F RID: 24623 RVA: 0x0000216A File Offset: 0x0000036A
			public RectTransform grouplabelrt
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06006030 RID: 24624 RVA: 0x00002739 File Offset: 0x00000939
			public SeperateInfo(CardSelectionList.CardLocateType locate, int playerid, CardSelectionListGroupLabel grouplabel)
			{
			}

			// Token: 0x04009B27 RID: 39719
			public int startindex;

			// Token: 0x04009B28 RID: 39720
			public int endindex;

			// Token: 0x04009B29 RID: 39721
			public int playerid;

			// Token: 0x04009B2A RID: 39722
			public CardSelectionList.CardLocateType locate;

			// Token: 0x04009B2B RID: 39723
			public CardSelectionListGroupLabel grouplabel;
		}

		// Token: 0x02000D08 RID: 3336
		public enum CardLocateType
		{
			// Token: 0x04009B2D RID: 39725
			FieldArea,
			// Token: 0x04009B2E RID: 39726
			FieldZone = 12,
			// Token: 0x04009B2F RID: 39727
			Hand,
			// Token: 0x04009B30 RID: 39728
			Grave = 16,
			// Token: 0x04009B31 RID: 39729
			Exclusion,
			// Token: 0x04009B32 RID: 39730
			ExDeck = 14,
			// Token: 0x04009B33 RID: 39731
			MainDeck,
			// Token: 0x04009B34 RID: 39732
			OverlayUnit = 32,
			// Token: 0x04009B35 RID: 39733
			CheckTiming,
			// Token: 0x04009B36 RID: 39734
			None = 18
		}

		// Token: 0x02000D09 RID: 3337
		private class SetListDesc
		{
			// Token: 0x06006031 RID: 24625 RVA: 0x00002739 File Offset: 0x00000939
			public SetListDesc(CardSelectionList.ListType type, Action onFinished, string title, bool nocancel = false, bool decidable = false, Action onCancelled = null, bool isWaitInput = false, bool isSort = true)
			{
			}

			// Token: 0x04009B37 RID: 39735
			public CardSelectionList.ListType type;

			// Token: 0x04009B38 RID: 39736
			public Action onFinished;

			// Token: 0x04009B39 RID: 39737
			public string title;

			// Token: 0x04009B3A RID: 39738
			public bool nocancel;

			// Token: 0x04009B3B RID: 39739
			public bool decidable;

			// Token: 0x04009B3C RID: 39740
			public Action onCancelled;

			// Token: 0x04009B3D RID: 39741
			public bool isWaitInput;

			// Token: 0x04009B3E RID: 39742
			public bool isSort;
		}

		// Token: 0x02000D0A RID: 3338
		public enum ListType
		{
			// Token: 0x04009B40 RID: 39744
			Summon,
			// Token: 0x04009B41 RID: 39745
			SpSummon,
			// Token: 0x04009B42 RID: 39746
			MonsterEffect,
			// Token: 0x04009B43 RID: 39747
			MagicTrap,
			// Token: 0x04009B44 RID: 39748
			FlipTurn,
			// Token: 0x04009B45 RID: 39749
			Attack,
			// Token: 0x04009B46 RID: 39750
			Chain,
			// Token: 0x04009B47 RID: 39751
			CheckTiming,
			// Token: 0x04009B48 RID: 39752
			NoramlList,
			// Token: 0x04009B49 RID: 39753
			NoramlListSetResult,
			// Token: 0x04009B4A RID: 39754
			Selection,
			// Token: 0x04009B4B RID: 39755
			BasicGrave,
			// Token: 0x04009B4C RID: 39756
			BasicEx,
			// Token: 0x04009B4D RID: 39757
			BasicDeck,
			// Token: 0x04009B4E RID: 39758
			OpponentHand,
			// Token: 0x04009B4F RID: 39759
			MyDeckTop,
			// Token: 0x04009B50 RID: 39760
			OpponentDeckTop,
			// Token: 0x04009B51 RID: 39761
			CheckCard,
			// Token: 0x04009B52 RID: 39762
			BlindSelect,
			// Token: 0x04009B53 RID: 39763
			SelAllCard,
			// Token: 0x04009B54 RID: 39764
			SelAllDeck,
			// Token: 0x04009B55 RID: 39765
			SelAllMonst,
			// Token: 0x04009B56 RID: 39766
			SelAllMonst2,
			// Token: 0x04009B57 RID: 39767
			SelAllGadget,
			// Token: 0x04009B58 RID: 39768
			SelAllIndeck,
			// Token: 0x04009B59 RID: 39769
			None
		}

		// Token: 0x02000D0B RID: 3339
		public enum CountMode
		{
			// Token: 0x04009B5B RID: 39771
			None,
			// Token: 0x04009B5C RID: 39772
			Star,
			// Token: 0x04009B5D RID: 39773
			Link,
			// Token: 0x04009B5E RID: 39774
			Atk
		}
	}
}
