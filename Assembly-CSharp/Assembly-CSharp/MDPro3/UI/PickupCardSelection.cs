using System;
using Cysharp.Threading.Tasks;
using MDPro3.Duel.YGOSharp;
using MDPro3.Servant;
using MDPro3.UI.PropertyOverride;
using MDPro3.UI.ServantUI;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x0200142A RID: 5162
	public class PickupCardSelection : UIWidget
	{
		// Token: 0x1700133E RID: 4926
		// (get) Token: 0x060095E3 RID: 38371 RVA: 0x0015A7DC File Offset: 0x001589DC
		private SelectionToggle_PickupCard Pickup0
		{
			get
			{
				return this.m_Pickup0 = ((this.m_Pickup0 != null) ? this.m_Pickup0 : base.Manager.GetElement<SelectionToggle_PickupCard>("pick0"));
			}
		}

		// Token: 0x1700133F RID: 4927
		// (get) Token: 0x060095E4 RID: 38372 RVA: 0x0015A818 File Offset: 0x00158A18
		private SelectionToggle_PickupCard Pickup1
		{
			get
			{
				return this.m_Pickup1 = ((this.m_Pickup1 != null) ? this.m_Pickup1 : base.Manager.GetElement<SelectionToggle_PickupCard>("pick1"));
			}
		}

		// Token: 0x17001340 RID: 4928
		// (get) Token: 0x060095E5 RID: 38373 RVA: 0x0015A854 File Offset: 0x00158A54
		private SelectionToggle_PickupCard Pickup2
		{
			get
			{
				return this.m_Pickup2 = ((this.m_Pickup2 != null) ? this.m_Pickup2 : base.Manager.GetElement<SelectionToggle_PickupCard>("pick2"));
			}
		}

		// Token: 0x17001341 RID: 4929
		// (get) Token: 0x060095E6 RID: 38374 RVA: 0x0015A890 File Offset: 0x00158A90
		private Image IconDeckCase
		{
			get
			{
				return this.m_IconDeckCase = ((this.m_IconDeckCase != null) ? this.m_IconDeckCase : base.Manager.GetElement<Image>("DeckCaseIcon"));
			}
		}

		// Token: 0x060095E7 RID: 38375 RVA: 0x0015A8CC File Offset: 0x00158ACC
		protected override void Awake()
		{
			base.Awake();
			this.SetPickups(DeckEditor.Deck);
			if (PropertyOverrider.NeedMobileLayout())
			{
				this.IconDeckCase.gameObject.SetActive(false);
				return;
			}
			this.LoadDeckCaseAsync(DeckEditor.Deck.Case);
		}

		// Token: 0x060095E8 RID: 38376 RVA: 0x0015A90C File Offset: 0x00158B0C
		public void SetPickups(Deck deck)
		{
			this.SetPickup(CardsManager.Get(deck.Pickup[0], false), 0, false);
			this.SetPickup(CardsManager.Get(deck.Pickup[1], false), 1, false);
			this.SetPickup(CardsManager.Get(deck.Pickup[2], false), 2, false);
			this.Pickup0.SetToggleOn(true);
		}

		// Token: 0x060095E9 RID: 38377 RVA: 0x0015A974 File Offset: 0x00158B74
		public void SetPickup(Card card, int index, bool selectNext = true)
		{
			if (card == null || card.Id == 0)
			{
				this.ClearPickup(index, selectNext);
				return;
			}
			switch (index)
			{
			case 0:
				this.Pickup0.SetCard(card, selectNext);
				break;
			case 1:
				this.Pickup1.SetCard(card, selectNext);
				break;
			case 2:
				this.Pickup2.SetCard(card, selectNext);
				break;
			}
			if (selectNext)
			{
				this.SelectNextPickup(index);
			}
		}

		// Token: 0x060095EA RID: 38378 RVA: 0x0015A9DE File Offset: 0x00158BDE
		public void ClearPickup(int index, bool toggleOn)
		{
			switch (index)
			{
			case 0:
				this.Pickup0.ClearCard(toggleOn, toggleOn);
				return;
			case 1:
				this.Pickup1.ClearCard(toggleOn, toggleOn);
				return;
			case 2:
				this.Pickup2.ClearCard(toggleOn, toggleOn);
				return;
			default:
				return;
			}
		}

		// Token: 0x060095EB RID: 38379 RVA: 0x0015AA1C File Offset: 0x00158C1C
		public int GetPickupIndex()
		{
			if (this.Pickup0.isOn)
			{
				return 0;
			}
			if (this.Pickup1.isOn)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060095EC RID: 38380 RVA: 0x0015AA40 File Offset: 0x00158C40
		private void SelectNextPickup(int index)
		{
			SelectionToggle_PickupCard other;
			SelectionToggle_PickupCard other2;
			if (index == 0)
			{
				other = this.Pickup1;
				other2 = this.Pickup2;
			}
			else if (index == 1)
			{
				other = this.Pickup2;
				other2 = this.Pickup0;
			}
			else
			{
				other = this.Pickup0;
				other2 = this.Pickup1;
			}
			if (!other.cardSetted)
			{
				other.SetToggleOn(true);
				return;
			}
			if (!other2.cardSetted)
			{
				other2.SetToggleOn(true);
				return;
			}
		}

		// Token: 0x060095ED RID: 38381 RVA: 0x0015AAA4 File Offset: 0x00158CA4
		private async UniTask LoadDeckCaseAsync(int deckCase)
		{
			Sprite sprite = await Program.items.LoadDeckCaseIconAsync(deckCase, "_L_HD");
			if (base.gameObject != null)
			{
				this.IconDeckCase.sprite = sprite;
			}
			else
			{
				this.IconDeckCase.gameObject.SetActive(false);
			}
		}

		// Token: 0x060095EE RID: 38382 RVA: 0x0015AAEF File Offset: 0x00158CEF
		public void Select()
		{
			this.Pickup0.GetSelectable().Select();
		}

		// Token: 0x060095EF RID: 38383 RVA: 0x0015AB01 File Offset: 0x00158D01
		public void OnLeftNavigation()
		{
			Program.instance.deckBrowser.GetUI<DeckBrowserUI>().DeckView.SelectNearestCard(this.Pickup0.transform.position);
		}

		// Token: 0x0400D43D RID: 54333
		private const string LABEL_PICKUP_CARD_0 = "pick0";

		// Token: 0x0400D43E RID: 54334
		private SelectionToggle_PickupCard m_Pickup0;

		// Token: 0x0400D43F RID: 54335
		private const string LABEL_PICKUP_CARD_1 = "pick1";

		// Token: 0x0400D440 RID: 54336
		private SelectionToggle_PickupCard m_Pickup1;

		// Token: 0x0400D441 RID: 54337
		private const string LABEL_PICKUP_CARD_2 = "pick2";

		// Token: 0x0400D442 RID: 54338
		private SelectionToggle_PickupCard m_Pickup2;

		// Token: 0x0400D443 RID: 54339
		private const string LABEL_IMG_DECK_CASE = "DeckCaseIcon";

		// Token: 0x0400D444 RID: 54340
		private Image m_IconDeckCase;
	}
}
