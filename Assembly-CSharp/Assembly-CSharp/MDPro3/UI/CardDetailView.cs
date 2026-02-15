using System;
using System.Collections.Generic;
using MDPro3.Duel.YGOSharp;
using UnityEngine;
using UnityEngine.Events;

namespace MDPro3.UI
{
	// Token: 0x0200141C RID: 5148
	public class CardDetailView : UIWidgetCardBase
	{
		// Token: 0x170012E3 RID: 4835
		// (get) Token: 0x06009500 RID: 38144 RVA: 0x001550A4 File Offset: 0x001532A4
		protected CanvasGroup Content
		{
			get
			{
				return this.m_Content = ((this.m_Content != null) ? this.m_Content : base.Manager.GetElement<CanvasGroup>("Content"));
			}
		}

		// Token: 0x170012E4 RID: 4836
		// (get) Token: 0x06009501 RID: 38145 RVA: 0x001550E0 File Offset: 0x001532E0
		protected SelectionButton ButtonCard
		{
			get
			{
				return this.m_ButtonCard = ((this.m_ButtonCard != null) ? this.m_ButtonCard : base.Manager.GetNestedElement<SelectionButton>("CardArea/ButtonCard"));
			}
		}

		// Token: 0x06009502 RID: 38146 RVA: 0x0015511C File Offset: 0x0015331C
		protected override void Awake()
		{
			base.Awake();
			this.pendulumTextNeedSplit = false;
			this.Content.alpha = 0f;
			this.Content.blocksRaycasts = false;
			this.ButtonCard.SetClickEvent(new UnityAction(this.ShowCardDetail));
		}

		// Token: 0x06009503 RID: 38147 RVA: 0x00155169 File Offset: 0x00153369
		public void ShowCard(Card data)
		{
			this.Content.alpha = 1f;
			this.Content.blocksRaycasts = true;
			this.cards = null;
			this.SetCardData(data);
		}

		// Token: 0x06009504 RID: 38148 RVA: 0x00155195 File Offset: 0x00153395
		public void ShowCard(List<int> cards, int index)
		{
			this.Content.alpha = 1f;
			this.Content.blocksRaycasts = true;
			this.cards = cards;
			this.showingCardIndex = index;
			this.SetCardData(CardsManager.Get(cards[index], false));
		}

		// Token: 0x06009505 RID: 38149 RVA: 0x001551D4 File Offset: 0x001533D4
		private void ShowCardDetail()
		{
			if (this.cards != null)
			{
				UIManager.ShowCardInfoDetail(this.cards, this.showingCardIndex);
				return;
			}
			if (base.Card != null)
			{
				UIManager.ShowCardInfoDetail(base.Card);
			}
		}

		// Token: 0x0400D33F RID: 54079
		private const string LABEL_CG_CONTENT = "Content";

		// Token: 0x0400D340 RID: 54080
		private CanvasGroup m_Content;

		// Token: 0x0400D341 RID: 54081
		private const string LABEL_SBN_CARD = "CardArea/ButtonCard";

		// Token: 0x0400D342 RID: 54082
		private SelectionButton m_ButtonCard;

		// Token: 0x0400D343 RID: 54083
		private List<int> cards;

		// Token: 0x0400D344 RID: 54084
		private int showingCardIndex;
	}
}
