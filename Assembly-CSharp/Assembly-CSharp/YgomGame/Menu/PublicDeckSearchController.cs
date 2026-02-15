using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YgomGame.Deck;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.Menu
{
	// Token: 0x02000AD3 RID: 2771
	public class PublicDeckSearchController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x060050BB RID: 20667 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override int selectorPriorityAddRange
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x060050BC RID: 20668 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060050BD RID: 20669 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x060050BE RID: 20670 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x060050BF RID: 20671 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemInitialize(GameObject gob)
		{
		}

		// Token: 0x060050C0 RID: 20672 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnGsvStanby()
		{
		}

		// Token: 0x060050C1 RID: 20673 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void OnItemSetData(GameObject gob, int dataindex)
		{
		}

		// Token: 0x060050C2 RID: 20674 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemExit(GameObject gob, int dataindex)
		{
		}

		// Token: 0x060050C3 RID: 20675 RVA: 0x0000216A File Offset: 0x0000036A
		public virtual IEnumerator DeckSearch_Search(int requestPageNo = 0)
		{
			return null;
		}

		// Token: 0x060050C4 RID: 20676 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void DeckSearch_Detail(string targetId, int deckNo, int pickCardId)
		{
		}

		// Token: 0x060050C5 RID: 20677 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnClickPublicDeck(string cardgameId, int deckNo, int pickCardId)
		{
		}

		// Token: 0x060050C6 RID: 20678 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void OpenDeckBrowser(string cardgameId, int deckNo, int pickCardId)
		{
		}

		// Token: 0x060050C7 RID: 20679 RVA: 0x0000216A File Offset: 0x0000036A
		protected virtual IEnumerator Initialize()
		{
			return null;
		}

		// Token: 0x060050C8 RID: 20680 RVA: 0x0000216A File Offset: 0x0000036A
		protected virtual IEnumerator AdditionalDeckDataLoad()
		{
			return null;
		}

		// Token: 0x060050C9 RID: 20681 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenFilterWindow()
		{
		}

		// Token: 0x060050CA RID: 20682 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetKeyWord(string keyword)
		{
		}

		// Token: 0x060050CB RID: 20683 RVA: 0x0000216D File Offset: 0x0000036D
		private void ClearFilters()
		{
		}

		// Token: 0x060050CC RID: 20684 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsActiveFilter()
		{
			return false;
		}

		// Token: 0x060050CD RID: 20685 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenCautionMMA()
		{
		}

		// Token: 0x060050CE RID: 20686 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void UpdateDecks()
		{
		}

		// Token: 0x060050CF RID: 20687 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnResult(ViewController from, object value)
		{
			return false;
		}

		// Token: 0x060050D0 RID: 20688 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateFilter()
		{
		}

		// Token: 0x060050D1 RID: 20689 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator DeckSearchRequest()
		{
			return null;
		}

		// Token: 0x060050D2 RID: 20690 RVA: 0x0000216D File Offset: 0x0000036D
		private void SortMRK(List<object> mrklist)
		{
		}

		// Token: 0x060050D3 RID: 20691 RVA: 0x0000216A File Offset: 0x0000036A
		public static int[] GetCardArrayByKeyword(string keyword)
		{
			return null;
		}

		// Token: 0x060050D4 RID: 20692 RVA: 0x0000216D File Offset: 0x0000036D
		private void JumpScrollIdx(PadInputDirection direction)
		{
		}

		// Token: 0x060050D5 RID: 20693 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OpenMaintenanceDialog()
		{
		}

		// Token: 0x04008EF5 RID: 36597
		protected readonly string k_ELabelScrollView;

		// Token: 0x04008EF6 RID: 36598
		protected readonly string k_ELabelDeckTemplate;

		// Token: 0x04008EF7 RID: 36599
		protected readonly string k_ELabelTournamentDeckTemplate;

		// Token: 0x04008EF8 RID: 36600
		protected readonly string k_ELabelPublicDeckTemplate;

		// Token: 0x04008EF9 RID: 36601
		protected readonly string k_ELabelHederArea;

		// Token: 0x04008EFA RID: 36602
		protected readonly string k_ELabelFooterArea;

		// Token: 0x04008EFB RID: 36603
		protected readonly string k_ELabelTextHeadline;

		// Token: 0x04008EFC RID: 36604
		protected readonly string k_ELabelEmptyMessage;

		// Token: 0x04008EFD RID: 36605
		protected readonly string k_ELabelEmptyMessageText;

		// Token: 0x04008EFE RID: 36606
		protected readonly string k_ELabelDeckNum;

		// Token: 0x04008EFF RID: 36607
		protected readonly string k_ELabelTournamentDeckNum;

		// Token: 0x04008F00 RID: 36608
		protected readonly string k_ELabelInputField;

		// Token: 0x04008F01 RID: 36609
		protected readonly string k_ELabelPlaceholder;

		// Token: 0x04008F02 RID: 36610
		protected readonly string k_ELabelButtonFilter;

		// Token: 0x04008F03 RID: 36611
		protected readonly string k_ELabelButtonTrash;

		// Token: 0x04008F04 RID: 36612
		protected readonly string k_ELabelButtonTrashSub;

		// Token: 0x04008F05 RID: 36613
		protected readonly string k_ELabelButtonPageUp;

		// Token: 0x04008F06 RID: 36614
		protected readonly string k_ELabelButtonPageDown;

		// Token: 0x04008F07 RID: 36615
		protected readonly string k_ELabelButtonCaution;

		// Token: 0x04008F08 RID: 36616
		protected Transform m_DeckNum;

		// Token: 0x04008F09 RID: 36617
		protected Transform m_TournamentDeckNum;

		// Token: 0x04008F0A RID: 36618
		protected ElementObjectManager m_InputFieldEom;

		// Token: 0x04008F0B RID: 36619
		protected Transform m_ButtonFilter;

		// Token: 0x04008F0C RID: 36620
		protected ElementObjectManager m_FilterEom;

		// Token: 0x04008F0D RID: 36621
		protected Transform m_FileterOn;

		// Token: 0x04008F0E RID: 36622
		protected Transform m_FileterOff;

		// Token: 0x04008F0F RID: 36623
		protected SelectionButton m_ButtonTrash;

		// Token: 0x04008F10 RID: 36624
		protected SelectionButton m_ButtonTrashSub;

		// Token: 0x04008F11 RID: 36625
		protected TextMeshProUGUI m_TextHeadline;

		// Token: 0x04008F12 RID: 36626
		private SelectionButton m_ButtonPageUp;

		// Token: 0x04008F13 RID: 36627
		private SelectionButton m_ButtonPageDown;

		// Token: 0x04008F14 RID: 36628
		protected SelectionButton m_ButtonCaution;

		// Token: 0x04008F15 RID: 36629
		protected const int numRequestPerPage = 100;

		// Token: 0x04008F16 RID: 36630
		protected int maxPageIdx;

		// Token: 0x04008F17 RID: 36631
		protected int nextPageIdx;

		// Token: 0x04008F18 RID: 36632
		protected int totalDecks;

		// Token: 0x04008F19 RID: 36633
		protected bool isUpdating;

		// Token: 0x04008F1A RID: 36634
		protected Transform m_HederArea;

		// Token: 0x04008F1B RID: 36635
		protected Transform m_FooterArea;

		// Token: 0x04008F1C RID: 36636
		private ElementObjectManager m_DeckTemplate;

		// Token: 0x04008F1D RID: 36637
		private ElementObjectManager m_TournamentDeckTemplate;

		// Token: 0x04008F1E RID: 36638
		private ElementObjectManager m_PublicDeckTemplate;

		// Token: 0x04008F1F RID: 36639
		protected InfinityScrollView m_ScrollView;

		// Token: 0x04008F20 RID: 36640
		protected ElementObjectManager m_ScrollViewEom;

		// Token: 0x04008F21 RID: 36641
		private string m_Keyword;

		// Token: 0x04008F22 RID: 36642
		private int[] m_CardArray;

		// Token: 0x04008F23 RID: 36643
		private List<CategoryReference> m_SelectedCategories;

		// Token: 0x04008F24 RID: 36644
		private List<CategoryReference> m_SelectedTags;

		// Token: 0x04008F25 RID: 36645
		private const string GROUP_LABEL = "PublicDeckSerach";

		// Token: 0x04008F26 RID: 36646
		protected const int MAX_MAIN = 60;

		// Token: 0x04008F27 RID: 36647
		protected const int MAX_EX = 15;

		// Token: 0x04008F28 RID: 36648
		private GameObject m_EmptyMessage;

		// Token: 0x04008F29 RID: 36649
		private TextMeshProUGUI m_EmptyMessageText;

		// Token: 0x04008F2A RID: 36650
		protected Dictionary<int, PublicDeckBox> m_PublicDeckUIs;

		// Token: 0x04008F2B RID: 36651
		protected bool updateFlag;

		// Token: 0x04008F2C RID: 36652
		protected Dictionary<string, object> deckDict;

		// Token: 0x04008F2D RID: 36653
		protected List<PublicDeckSearchController.DeckReference> m_PublicDecks;

		// Token: 0x02000AD4 RID: 2772
		protected class DeckReference
		{
			// Token: 0x04008F2E RID: 36654
			public int id;

			// Token: 0x04008F2F RID: 36655
			public int pickupId;

			// Token: 0x04008F30 RID: 36656
			public string cardgameId;

			// Token: 0x04008F31 RID: 36657
			public int deckNo;
		}
	}
}
