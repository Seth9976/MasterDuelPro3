using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YgomGame.Deck;
using YgomSystem.ElementSystem;
using YgomSystem.UI.ElementWidget;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.DeckBrowser
{
	// Token: 0x02000F91 RID: 3985
	public class DeckViewWidget : ElementWidgetBehaviourBase<DeckViewWidget>
	{
		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x06007538 RID: 30008 RVA: 0x0000216A File Offset: 0x0000036A
		public TMP_Text deckNameText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x06007539 RID: 30009 RVA: 0x0000216A File Offset: 0x0000036A
		public TextMeshProUGUI mainDeckNumText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x0600753A RID: 30010 RVA: 0x0000216A File Offset: 0x0000036A
		public TextMeshProUGUI extraDeckNumText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x0600753B RID: 30011 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyList<DeckCardWidget> mainDeckWidgets
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x0600753C RID: 30012 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyList<DeckCardWidget> extraDeckWidgets
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600753D RID: 30013 RVA: 0x0000216A File Offset: 0x0000036A
		public static DeckViewWidget Create(ElementObjectManager eom, bool isMobile, int numDecks = 40, int numExDecks = 15)
		{
			return null;
		}

		// Token: 0x0600753E RID: 30014 RVA: 0x0000216A File Offset: 0x0000036A
		public DeckCardWidget AddToMainDeckByID(int id, int prem = 1, bool isRental = false)
		{
			return null;
		}

		// Token: 0x0600753F RID: 30015 RVA: 0x0000216A File Offset: 0x0000036A
		public DeckCardWidget AddToExtraDeckByID(int id, int prem = 1, bool isRental = false)
		{
			return null;
		}

		// Token: 0x06007540 RID: 30016 RVA: 0x0000216A File Offset: 0x0000036A
		public DeckCardWidget AddToMainDeckMobile(DeckCard deckCard)
		{
			return null;
		}

		// Token: 0x06007541 RID: 30017 RVA: 0x0000216A File Offset: 0x0000036A
		public DeckCardWidget AddToExtraDeckMobile(DeckCard deckCard)
		{
			return null;
		}

		// Token: 0x06007542 RID: 30018 RVA: 0x0000216A File Offset: 0x0000036A
		private DeckCardWidget AddCard(Transform parent, int id, int prem = 1, bool isRental = false)
		{
			return null;
		}

		// Token: 0x06007543 RID: 30019 RVA: 0x0000216D File Offset: 0x0000036D
		public void DispLoadingIcon(bool isLoading)
		{
		}

		// Token: 0x06007544 RID: 30020 RVA: 0x0000216D File Offset: 0x0000036D
		public void SortInDeckCard()
		{
		}

		// Token: 0x06007545 RID: 30021 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeInfinityScroll()
		{
		}

		// Token: 0x06007546 RID: 30022 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemInitialize(GameObject gob)
		{
		}

		// Token: 0x06007547 RID: 30023 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnGsvStanby()
		{
		}

		// Token: 0x06007548 RID: 30024 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemSetData(GameObject gob, int dataindex)
		{
		}

		// Token: 0x0400AE6D RID: 44653
		private const string k_ELabelDeckNameText = "HeaderArea/TextDeckNameTMP";

		// Token: 0x0400AE6E RID: 44654
		private const string k_ELabelTemplate = "MainDeckView/template";

		// Token: 0x0400AE6F RID: 44655
		private const string k_ELabelMainDeckContent = "MainDeckView/MainDeckContent";

		// Token: 0x0400AE70 RID: 44656
		private const string k_ELabelMainDeckNumText = "MainDeckView/TextMainDeckCardNum";

		// Token: 0x0400AE71 RID: 44657
		private const string k_ELabelExtraDeckContent = "ExDeckView/ExDeckContent";

		// Token: 0x0400AE72 RID: 44658
		private const string k_ELabelExtraDeckNumText = "ExDeckView/TextExDeckCardNum";

		// Token: 0x0400AE73 RID: 44659
		private const string k_ELabelTemplateParent = "MainDeckView/TemplateParent";

		// Token: 0x0400AE74 RID: 44660
		private const string k_ELabelLoadingIcon = "Loading";

		// Token: 0x0400AE75 RID: 44661
		private TMP_Text m_DeckNameText;

		// Token: 0x0400AE76 RID: 44662
		private DeckCardWidget m_Template;

		// Token: 0x0400AE77 RID: 44663
		private RectTransform m_MainDeckContent;

		// Token: 0x0400AE78 RID: 44664
		private TextMeshProUGUI m_MainDeckNumText;

		// Token: 0x0400AE79 RID: 44665
		private RectTransform m_ExtraDeckContent;

		// Token: 0x0400AE7A RID: 44666
		private TextMeshProUGUI m_ExtraDeckNumText;

		// Token: 0x0400AE7B RID: 44667
		private List<DeckCardWidget> m_MainDeckWidgets;

		// Token: 0x0400AE7C RID: 44668
		private List<DeckCardWidget> m_ExtraDeckWidgets;

		// Token: 0x0400AE7D RID: 44669
		private Transform m_TemplateParent;

		// Token: 0x0400AE7E RID: 44670
		private GameObject m_LoadingIcon;

		// Token: 0x0400AE7F RID: 44671
		private GridLayoutGroup m_MainGridLayoutGroup;

		// Token: 0x0400AE80 RID: 44672
		private const int CARDGROUP_ROW_MAX = 7;

		// Token: 0x0400AE81 RID: 44673
		private InfinityScrollView m_InfinityScroll;

		// Token: 0x0400AE82 RID: 44674
		private List<int> templateList;

		// Token: 0x0400AE83 RID: 44675
		private bool m_IsMobile;
	}
}
