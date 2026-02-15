using System;
using System.Collections.Generic;
using YGOSharp.OCGWrapper;

namespace YGOSharp
{
	// Token: 0x020001B6 RID: 438
	public class Deck
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x00020651 File Offset: 0x0001E851
		// (set) Token: 0x060006A7 RID: 1703 RVA: 0x00020659 File Offset: 0x0001E859
		public IList<int> Main { get; private set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x00020662 File Offset: 0x0001E862
		// (set) Token: 0x060006A9 RID: 1705 RVA: 0x0002066A File Offset: 0x0001E86A
		public IList<int> Extra { get; private set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x00020673 File Offset: 0x0001E873
		// (set) Token: 0x060006AB RID: 1707 RVA: 0x0002067B File Offset: 0x0001E87B
		public IList<int> Side { get; private set; }

		// Token: 0x060006AC RID: 1708 RVA: 0x00020684 File Offset: 0x0001E884
		public Deck()
		{
			this.Main = new List<int>();
			this.Extra = new List<int>();
			this.Side = new List<int>();
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x000206B0 File Offset: 0x0001E8B0
		public void AddMain(int cardId)
		{
			Card card = Card.Get(cardId);
			if (card == null)
			{
				return;
			}
			if ((card.Type & 16384) != 0)
			{
				return;
			}
			if (card.IsExtraCard())
			{
				if (this.Extra.Count < 15)
				{
					this.Extra.Add(cardId);
					return;
				}
			}
			else if (this.Main.Count < 60)
			{
				this.Main.Add(cardId);
			}
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00020718 File Offset: 0x0001E918
		public void AddSide(int cardId)
		{
			Card card = Card.Get(cardId);
			if (card == null)
			{
				return;
			}
			if ((card.Type & 16384) != 0)
			{
				return;
			}
			if (this.Side.Count < 15)
			{
				this.Side.Add(cardId);
			}
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0002075C File Offset: 0x0001E95C
		public int Check(Banlist ban, bool ocg, bool tcg)
		{
			if (this.Main.Count < Config.GetInt("MainDeckMinSize", 40) || this.Main.Count > Config.GetInt("MainDeckMaxSize", 60) || this.Extra.Count > Config.GetInt("ExtraDeckMaxSize", 15) || this.Side.Count > Config.GetInt("SideDeckMaxSize", 15))
			{
				return 1;
			}
			IDictionary<int, int> cards = new Dictionary<int, int>();
			IList<int>[] array = new IList<int>[] { this.Main, this.Extra, this.Side };
			for (int i = 0; i < array.Length; i++)
			{
				foreach (int id in array[i])
				{
					Card card = Card.Get(id);
					Deck.AddToCards(cards, card);
					if ((!ocg && card.Ot == 1) || (!tcg && card.Ot == 2))
					{
						return id;
					}
				}
			}
			if (ban == null)
			{
				return 0;
			}
			foreach (KeyValuePair<int, int> pair in cards)
			{
				int max = ban.GetQuantity(pair.Key);
				if (pair.Value > max)
				{
					return pair.Key;
				}
			}
			return 0;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x000208D4 File Offset: 0x0001EAD4
		public bool Check(Deck deck)
		{
			if (deck.Main.Count != this.Main.Count || deck.Extra.Count != this.Extra.Count)
			{
				return false;
			}
			IDictionary<int, int> cards = new Dictionary<int, int>();
			IDictionary<int, int> ncards = new Dictionary<int, int>();
			IList<int>[] array = new IList<int>[] { this.Main, this.Extra, this.Side };
			for (int i = 0; i < array.Length; i++)
			{
				foreach (int id in array[i])
				{
					if (!cards.ContainsKey(id))
					{
						cards.Add(id, 1);
					}
					else
					{
						IDictionary<int, int> dictionary = cards;
						int num = id;
						int num2 = dictionary[num];
						dictionary[num] = num2 + 1;
					}
				}
			}
			array = new IList<int>[] { deck.Main, deck.Extra, deck.Side };
			for (int i = 0; i < array.Length; i++)
			{
				foreach (int id2 in array[i])
				{
					if (!ncards.ContainsKey(id2))
					{
						ncards.Add(id2, 1);
					}
					else
					{
						IDictionary<int, int> dictionary2 = ncards;
						int num2 = id2;
						int num = dictionary2[num2];
						dictionary2[num2] = num + 1;
					}
				}
			}
			foreach (KeyValuePair<int, int> pair in cards)
			{
				if (!ncards.ContainsKey(pair.Key))
				{
					return false;
				}
				if (ncards[pair.Key] != pair.Value)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00020ABC File Offset: 0x0001ECBC
		private static void AddToCards(IDictionary<int, int> cards, Card card)
		{
			int id = card.Id;
			if (card.Alias != 0)
			{
				id = card.Alias;
			}
			if (cards.ContainsKey(id))
			{
				int num = id;
				int num2 = cards[num];
				cards[num] = num2 + 1;
				return;
			}
			cards.Add(id, 1);
		}
	}
}
