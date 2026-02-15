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
	// Token: 0x02000AF0 RID: 2800
	public class StructureDeckCopyController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x0600516B RID: 20843 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override int selectorPriorityAddRange
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x0600516C RID: 20844 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600516D RID: 20845 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x0600516E RID: 20846 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickOnlyHaving()
		{
		}

		// Token: 0x0600516F RID: 20847 RVA: 0x0000216D File Offset: 0x0000036D
		private void DispPickupCards()
		{
		}

		// Token: 0x06005170 RID: 20848 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemInitialize(GameObject gob)
		{
		}

		// Token: 0x06005171 RID: 20849 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnGsvStanby()
		{
		}

		// Token: 0x06005172 RID: 20850 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemSetData(GameObject gob, int dataindex)
		{
		}

		// Token: 0x06005173 RID: 20851 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickStuructureDeck(int structureId, ElementObjectManager body)
		{
		}

		// Token: 0x06005174 RID: 20852 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator Initialize()
		{
			return null;
		}

		// Token: 0x06005175 RID: 20853 RVA: 0x0000216D File Offset: 0x0000036D
		private void CheckFirstStructure()
		{
		}

		// Token: 0x06005176 RID: 20854 RVA: 0x0000216D File Offset: 0x0000036D
		private void GetStructureDecks()
		{
		}

		// Token: 0x06005177 RID: 20855 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateStructureDecks()
		{
		}

		// Token: 0x04008FAA RID: 36778
		private readonly string k_ELabelScrollView;

		// Token: 0x04008FAB RID: 36779
		private readonly string k_ELabelDeckTemplate;

		// Token: 0x04008FAC RID: 36780
		private readonly string k_ELabelTournamentDeckTemplate;

		// Token: 0x04008FAD RID: 36781
		private readonly string k_ELabelHederArea;

		// Token: 0x04008FAE RID: 36782
		private readonly string k_ELabelFooterArea;

		// Token: 0x04008FAF RID: 36783
		private readonly string k_ELabelDeckNum;

		// Token: 0x04008FB0 RID: 36784
		private readonly string k_ELabelTournamentDeckNum;

		// Token: 0x04008FB1 RID: 36785
		private readonly string k_ELabelStructureDeckView;

		// Token: 0x04008FB2 RID: 36786
		private readonly string k_ELabelTextHeadline;

		// Token: 0x04008FB3 RID: 36787
		private readonly string k_ELabelPickupCardButton;

		// Token: 0x04008FB4 RID: 36788
		private Transform m_DeckNum;

		// Token: 0x04008FB5 RID: 36789
		private Transform m_TournamentDeckNum;

		// Token: 0x04008FB6 RID: 36790
		private Transform m_StructureDeckView;

		// Token: 0x04008FB7 RID: 36791
		private ElementObjectManager m_StructureDeckViewEom;

		// Token: 0x04008FB8 RID: 36792
		private SelectionButton m_StructureDeckViewButton;

		// Token: 0x04008FB9 RID: 36793
		private Transform m_StructureDeckViewOn;

		// Token: 0x04008FBA RID: 36794
		private Transform m_StructureDeckViewOff;

		// Token: 0x04008FBB RID: 36795
		private TextMeshProUGUI m_TextHeadline;

		// Token: 0x04008FBC RID: 36796
		private SelectionButton m_PickupCardButton;

		// Token: 0x04008FBD RID: 36797
		private ElementObjectManager m_PickupCardButtonEom;

		// Token: 0x04008FBE RID: 36798
		private Transform m_PickupCardButtonOn;

		// Token: 0x04008FBF RID: 36799
		private Transform m_PickupCardButtonOff;

		// Token: 0x04008FC0 RID: 36800
		[SerializeField]
		private ElementObjectManager m_PrefabUI;

		// Token: 0x04008FC1 RID: 36801
		protected ElementObjectManager m_UI;

		// Token: 0x04008FC2 RID: 36802
		private Transform m_HederArea;

		// Token: 0x04008FC3 RID: 36803
		private Transform m_FooterArea;

		// Token: 0x04008FC4 RID: 36804
		private ElementObjectManager m_DeckTemplate;

		// Token: 0x04008FC5 RID: 36805
		private ElementObjectManager m_TournamentDeckTemplate;

		// Token: 0x04008FC6 RID: 36806
		private InfinityScrollView m_ScrollView;

		// Token: 0x04008FC7 RID: 36807
		private ElementObjectManager m_ScrollViewEom;

		// Token: 0x04008FC8 RID: 36808
		private const string GROUP_LABEL = "StructureDeckCopy";

		// Token: 0x04008FC9 RID: 36809
		private bool m_OnlyHaving;

		// Token: 0x04008FCA RID: 36810
		private bool dispPickCards;

		// Token: 0x04008FCB RID: 36811
		private List<int> m_NewIconList;

		// Token: 0x04008FCC RID: 36812
		private Dictionary<int, DeckBox> m_DeckUIs;

		// Token: 0x04008FCD RID: 36813
		private List<StructureDeckCopyController.DeckReference> m_Decks;

		// Token: 0x04008FCE RID: 36814
		private List<StructureDeckCopyController.DeckReference> m_hasDecks;

		// Token: 0x04008FCF RID: 36815
		private List<StructureDeckCopyController.DeckReference> m_notHasDecks;

		// Token: 0x02000AF1 RID: 2801
		private class DeckReference
		{
			// Token: 0x04008FD0 RID: 36816
			public int id;

			// Token: 0x04008FD1 RID: 36817
			public int structureId;

			// Token: 0x04008FD2 RID: 36818
			public string name;

			// Token: 0x04008FD3 RID: 36819
			public int caseID;

			// Token: 0x04008FD4 RID: 36820
			public int protectorID;

			// Token: 0x04008FD5 RID: 36821
			public int[] pickUpIDs;

			// Token: 0x04008FD6 RID: 36822
			public int[] pickUpDecos;
		}
	}
}
