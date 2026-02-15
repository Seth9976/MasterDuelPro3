using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YgomGame.Deck;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.DeckBrowser
{
	// Token: 0x02000F99 RID: 3993
	public class TrialDrawWidget : DeckBrowserOptionWidget
	{
		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x06007585 RID: 30085 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool isGamePad
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x06007586 RID: 30086 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool isMobile
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007587 RID: 30087 RVA: 0x000F65B9 File Offset: 0x000F47B9
		public TrialDrawWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x06007588 RID: 30088 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(Transform parent, Action<TrialDrawWidget> onCreated)
		{
		}

		// Token: 0x06007589 RID: 30089 RVA: 0x0000216D File Offset: 0x0000036D
		public void Init(List<object> mrks, List<object> prems, int regulation, CardDetailWidget detailView = null)
		{
		}

		// Token: 0x0600758A RID: 30090 RVA: 0x0000216D File Offset: 0x0000036D
		private void FiveDraw()
		{
		}

		// Token: 0x0600758B RID: 30091 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlusOneDraw()
		{
		}

		// Token: 0x0600758C RID: 30092 RVA: 0x0000216D File Offset: 0x0000036D
		private void DrawCard()
		{
		}

		// Token: 0x0600758D RID: 30093 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemInitialize(GameObject gob)
		{
		}

		// Token: 0x0600758E RID: 30094 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnGsvStanby()
		{
		}

		// Token: 0x0600758F RID: 30095 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemSetData(GameObject gob, int dataindex)
		{
		}

		// Token: 0x06007590 RID: 30096 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDetailViewCard(int mrk, int premiumId)
		{
		}

		// Token: 0x06007591 RID: 30097 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickDetailViewCard(int idx)
		{
		}

		// Token: 0x06007592 RID: 30098 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCreatedCardCallback(DeckCard deckCard, int idx)
		{
		}

		// Token: 0x0400AED4 RID: 44756
		private const string k_PrefPath = "Prefabs/UI/DeckBrowser/Optionals/DeckBrowserOptionForTrialDraw";

		// Token: 0x0400AED5 RID: 44757
		private ShortcutKeySetter m_ShortcutSettings;

		// Token: 0x0400AED6 RID: 44758
		private InfinityScrollView m_ScrollView;

		// Token: 0x0400AED7 RID: 44759
		private SelectionButton m_PlusOneDrawButton;

		// Token: 0x0400AED8 RID: 44760
		private List<object> m_DeckCardMrks;

		// Token: 0x0400AED9 RID: 44761
		private List<object> m_DeckCardPremiums;

		// Token: 0x0400AEDA RID: 44762
		private List<CardBaseData> m_DeckList;

		// Token: 0x0400AEDB RID: 44763
		private TextMeshProUGUI m_TextDeckNum;

		// Token: 0x0400AEDC RID: 44764
		private int m_Regulation;

		// Token: 0x0400AEDD RID: 44765
		private List<CardBaseData> m_DeckListForTrialDraw;

		// Token: 0x0400AEDE RID: 44766
		private List<CardBaseData> m_HandListForTrialDraw;

		// Token: 0x0400AEDF RID: 44767
		private List<object> m_HandMrks;

		// Token: 0x0400AEE0 RID: 44768
		private List<object> m_HandPremiums;

		// Token: 0x0400AEE1 RID: 44769
		private CardDetailWidget m_DetailWidget;
	}
}
