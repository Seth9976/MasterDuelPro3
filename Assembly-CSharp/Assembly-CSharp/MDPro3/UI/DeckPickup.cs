using System;
using Cysharp.Threading.Tasks;
using MDPro3.Duel.YGOSharp;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;

namespace MDPro3.UI
{
	// Token: 0x020013E2 RID: 5090
	public class DeckPickup : MonoBehaviour
	{
		// Token: 0x1700128B RID: 4747
		// (get) Token: 0x06009367 RID: 37735 RVA: 0x0014D4F4 File Offset: 0x0014B6F4
		private ElementObjectManager Manager
		{
			get
			{
				return this.m_Manager = ((this.m_Manager != null) ? this.m_Manager : base.GetComponent<ElementObjectManager>());
			}
		}

		// Token: 0x1700128C RID: 4748
		// (get) Token: 0x06009368 RID: 37736 RVA: 0x0014D528 File Offset: 0x0014B728
		private Image DeckCase
		{
			get
			{
				return this.m_DeckCase = ((this.m_DeckCase != null) ? this.m_DeckCase : this.Manager.GetElement<Image>("DeckCase"));
			}
		}

		// Token: 0x1700128D RID: 4749
		// (get) Token: 0x06009369 RID: 37737 RVA: 0x0014D564 File Offset: 0x0014B764
		private CardRawImageHandler Card1
		{
			get
			{
				return this.m_Card1 = ((this.m_Card1 != null) ? this.m_Card1 : this.Manager.GetElement<CardRawImageHandler>("Card1"));
			}
		}

		// Token: 0x1700128E RID: 4750
		// (get) Token: 0x0600936A RID: 37738 RVA: 0x0014D5A0 File Offset: 0x0014B7A0
		private CardRawImageHandler Card2
		{
			get
			{
				return this.m_Card2 = ((this.m_Card2 != null) ? this.m_Card2 : this.Manager.GetElement<CardRawImageHandler>("Card2"));
			}
		}

		// Token: 0x1700128F RID: 4751
		// (get) Token: 0x0600936B RID: 37739 RVA: 0x0014D5DC File Offset: 0x0014B7DC
		private CardRawImageHandler Card3
		{
			get
			{
				return this.m_Card3 = ((this.m_Card3 != null) ? this.m_Card3 : this.Manager.GetElement<CardRawImageHandler>("Card3"));
			}
		}

		// Token: 0x17001290 RID: 4752
		// (get) Token: 0x0600936C RID: 37740 RVA: 0x0014D618 File Offset: 0x0014B818
		private TextMeshProUGUI TextSetting
		{
			get
			{
				return this.m_TextSetting = ((this.m_TextSetting != null) ? this.m_TextSetting : this.Manager.GetElement<TextMeshProUGUI>("TextSetting"));
			}
		}

		// Token: 0x0600936D RID: 37741 RVA: 0x0014D654 File Offset: 0x0014B854
		public async void SetDeck(Deck deck)
		{
			string setting = string.Empty;
			setting += this.GetCardName(deck.Pickup[0], 1);
			setting += this.GetCardName(deck.Pickup[1], 2);
			setting += this.GetCardName(deck.Pickup[2], 3);
			this.TextSetting.text = setting;
			this.Card1.protectorCode = deck.Protector;
			this.Card2.protectorCode = deck.Protector;
			this.Card3.protectorCode = deck.Protector;
			this.Card1.SetCard(deck.Pickup[0]);
			this.Card2.SetCard(deck.Pickup[1]);
			this.Card3.SetCard(deck.Pickup[2]);
			await this.LoadDeckCaseAsync(deck.Case);
		}

		// Token: 0x0600936E RID: 37742 RVA: 0x0014D694 File Offset: 0x0014B894
		private async UniTask LoadDeckCaseAsync(int deckCase)
		{
			Sprite sprite = await Program.items.LoadDeckCaseIconAsync(deckCase, "_Open_L_HD");
			if (sprite != null)
			{
				this.DeckCase.sprite = sprite;
			}
		}

		// Token: 0x0600936F RID: 37743 RVA: 0x0014D6E0 File Offset: 0x0014B8E0
		private string GetCardName(int code, int index)
		{
			string result = string.Format("{0} : ", index);
			Card card = CardsManager.Get(code, false);
			if (card == null || card.Id == 0)
			{
				result += InterString.Get("未设置", 0);
			}
			else
			{
				result += card.Name;
			}
			return result + "\r\n";
		}

		// Token: 0x0400D1CC RID: 53708
		private ElementObjectManager m_Manager;

		// Token: 0x0400D1CD RID: 53709
		private const string LABEL_IMG_DECKCASE = "DeckCase";

		// Token: 0x0400D1CE RID: 53710
		private Image m_DeckCase;

		// Token: 0x0400D1CF RID: 53711
		private const string LABEL_CRH_CARD1 = "Card1";

		// Token: 0x0400D1D0 RID: 53712
		private CardRawImageHandler m_Card1;

		// Token: 0x0400D1D1 RID: 53713
		private const string LABEL_CRH_CARD2 = "Card2";

		// Token: 0x0400D1D2 RID: 53714
		private CardRawImageHandler m_Card2;

		// Token: 0x0400D1D3 RID: 53715
		private const string LABEL_CRH_CARD3 = "Card3";

		// Token: 0x0400D1D4 RID: 53716
		private CardRawImageHandler m_Card3;

		// Token: 0x0400D1D5 RID: 53717
		private const string LABEL_TXT_SETTING = "TextSetting";

		// Token: 0x0400D1D6 RID: 53718
		private TextMeshProUGUI m_TextSetting;
	}
}
