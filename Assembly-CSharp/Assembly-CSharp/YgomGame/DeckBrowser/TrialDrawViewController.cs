using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YgomGame.Deck;
using YgomGame.Menu;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.DeckBrowser
{
	// Token: 0x02000F98 RID: 3992
	public class TrialDrawViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x06007575 RID: 30069 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x06007576 RID: 30070 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isMobile
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x06007577 RID: 30071 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isGamePad
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007578 RID: 30072 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06007579 RID: 30073 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(ViewControllerManager vc, Dictionary<string, object> args)
		{
		}

		// Token: 0x0600757A RID: 30074 RVA: 0x0000216D File Offset: 0x0000036D
		private void FiveDraw()
		{
		}

		// Token: 0x0600757B RID: 30075 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlusOneDraw()
		{
		}

		// Token: 0x0600757C RID: 30076 RVA: 0x0000216D File Offset: 0x0000036D
		private void DrawCard()
		{
		}

		// Token: 0x0600757D RID: 30077 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator InitializeFiveDraw()
		{
			return null;
		}

		// Token: 0x0600757E RID: 30078 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemInitialize(GameObject gob)
		{
		}

		// Token: 0x0600757F RID: 30079 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnGsvStanby()
		{
		}

		// Token: 0x06007580 RID: 30080 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemSetData(GameObject gob, int dataindex)
		{
		}

		// Token: 0x06007581 RID: 30081 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDetailViewCard(int mrk, int premiumId)
		{
		}

		// Token: 0x06007582 RID: 30082 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickDetailViewCard(int idx)
		{
		}

		// Token: 0x06007583 RID: 30083 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCreatedCardCallback(DeckCard deckCard, int idx)
		{
		}

		// Token: 0x0400AEB8 RID: 44728
		private readonly string k_ELabelDetailView;

		// Token: 0x0400AEB9 RID: 44729
		private readonly string k_ELabelDetailViewMenuRoot;

		// Token: 0x0400AEBA RID: 44730
		[SerializeField]
		private ElementObjectManager m_UIPrefab;

		// Token: 0x0400AEBB RID: 44731
		[SerializeField]
		private ElementObjectManager m_UIPrefabMobile;

		// Token: 0x0400AEBC RID: 44732
		private ElementObjectManager m_UI;

		// Token: 0x0400AEBD RID: 44733
		private CardDetailWidget m_DetailWidget;

		// Token: 0x0400AEBE RID: 44734
		private int m_Regulation;

		// Token: 0x0400AEBF RID: 44735
		private const int thresClose = 5;

		// Token: 0x0400AEC0 RID: 44736
		private float holdTime;

		// Token: 0x0400AEC1 RID: 44737
		private readonly string k_ELabelTrialDraw;

		// Token: 0x0400AEC2 RID: 44738
		private readonly string k_ELabelTrialDrawInfinityView;

		// Token: 0x0400AEC3 RID: 44739
		private readonly string k_ELabelTextDeckNum;

		// Token: 0x0400AEC4 RID: 44740
		private readonly string k_ELabelFiveDrawButton;

		// Token: 0x0400AEC5 RID: 44741
		private readonly string k_ELabelPlusOneDrawButton;

		// Token: 0x0400AEC6 RID: 44742
		private readonly string k_ELabelRegulationIcon;

		// Token: 0x0400AEC7 RID: 44743
		private InfinityScrollView m_TrialDrawInfinityView;

		// Token: 0x0400AEC8 RID: 44744
		private SelectionButton m_FiveDrawButton;

		// Token: 0x0400AEC9 RID: 44745
		private SelectionButton m_PlusOneDrawButton;

		// Token: 0x0400AECA RID: 44746
		private Image m_RegulationIcon;

		// Token: 0x0400AECB RID: 44747
		private List<object> m_DeckCardMrks;

		// Token: 0x0400AECC RID: 44748
		private List<object> m_DeckCardPremiums;

		// Token: 0x0400AECD RID: 44749
		private List<CardBaseData> m_DeckList;

		// Token: 0x0400AECE RID: 44750
		private TextMeshProUGUI m_TextDeckNum;

		// Token: 0x0400AECF RID: 44751
		private List<CardBaseData> m_DeckListForTrialDraw;

		// Token: 0x0400AED0 RID: 44752
		private List<CardBaseData> m_HandListForTrialDraw;

		// Token: 0x0400AED1 RID: 44753
		private List<object> m_HandMrks;

		// Token: 0x0400AED2 RID: 44754
		private List<object> m_HandPremiums;

		// Token: 0x0400AED3 RID: 44755
		private bool m_RegulationVisible;
	}
}
