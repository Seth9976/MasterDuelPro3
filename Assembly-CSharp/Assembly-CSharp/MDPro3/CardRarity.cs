using System;
using System.Collections.Generic;
using System.IO;
using MDPro3.Duel.YGOSharp;
using Newtonsoft.Json;
using UnityEngine;

namespace MDPro3
{
	// Token: 0x02001234 RID: 4660
	public static class CardRarity
	{
		// Token: 0x060089C7 RID: 35271 RVA: 0x0010D5CC File Offset: 0x0010B7CC
		private static void Initialize()
		{
			if (CardRarity.initialized)
			{
				return;
			}
			if (!File.Exists("Data/Rarity.json"))
			{
				CardRarity.cards = new RarityCards();
				CardRarity.initialized = true;
				return;
			}
			string json = File.ReadAllText("Data/Rarity.json");
			try
			{
				CardRarity.cards = JsonConvert.DeserializeObject<RarityCards>(json);
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
				CardRarity.cards = new RarityCards();
			}
			CardRarity.initialized = true;
		}

		// Token: 0x060089C8 RID: 35272 RVA: 0x0010D640 File Offset: 0x0010B840
		public static void SetRarity(int card, CardRarity.Rarity rarity)
		{
			CardRarity.Initialize();
			CardRarity.cards.ShineCards.Remove(card);
			CardRarity.cards.RoyalCards.Remove(card);
			CardRarity.cards.GoldCards.Remove(card);
			CardRarity.cards.MillenniumCards.Remove(card);
			if (rarity <= CardRarity.Rarity.Royal)
			{
				if (rarity == CardRarity.Rarity.Shine)
				{
					CardRarity.cards.ShineCards.Add(card);
					return;
				}
				if (rarity != CardRarity.Rarity.Royal)
				{
					return;
				}
				CardRarity.cards.RoyalCards.Add(card);
				return;
			}
			else
			{
				if (rarity == CardRarity.Rarity.Gold)
				{
					CardRarity.cards.GoldCards.Add(card);
					return;
				}
				if (rarity != CardRarity.Rarity.Millennium)
				{
					return;
				}
				CardRarity.cards.MillenniumCards.Add(card);
				return;
			}
		}

		// Token: 0x060089C9 RID: 35273 RVA: 0x0010D6F0 File Offset: 0x0010B8F0
		public static CardRarity.Rarity GetRarity(int card)
		{
			CardRarity.Initialize();
			if (CardRarity.cards.ShineCards.Contains(card))
			{
				return CardRarity.Rarity.Shine;
			}
			if (CardRarity.cards.RoyalCards.Contains(card))
			{
				return CardRarity.Rarity.Royal;
			}
			if (CardRarity.cards.GoldCards.Contains(card))
			{
				return CardRarity.Rarity.Gold;
			}
			if (CardRarity.cards.MillenniumCards.Contains(card))
			{
				return CardRarity.Rarity.Millennium;
			}
			return CardRarity.Rarity.Normal;
		}

		// Token: 0x060089CA RID: 35274 RVA: 0x0010D754 File Offset: 0x0010B954
		public static void BookmarkCard(int card)
		{
			CardRarity.Initialize();
			CardRarity.cards.BookCards.Add(card);
		}

		// Token: 0x060089CB RID: 35275 RVA: 0x0010D76B File Offset: 0x0010B96B
		public static void UnbookmarkCard(int card)
		{
			CardRarity.Initialize();
			CardRarity.cards.BookCards.Remove(card);
		}

		// Token: 0x060089CC RID: 35276 RVA: 0x0010D783 File Offset: 0x0010B983
		public static bool CardBookmarked(int card)
		{
			CardRarity.Initialize();
			return CardRarity.cards.BookCards.Contains(card);
		}

		// Token: 0x060089CD RID: 35277 RVA: 0x0010D79C File Offset: 0x0010B99C
		private static void BookSort()
		{
			CardRarity.Initialize();
			List<Card> cs = new List<Card>();
			foreach (int code in CardRarity.cards.BookCards)
			{
				cs.Add(CardsManager.Get(code, false));
			}
			cs.Sort(CardsManager.ComparisonOfCard());
			CardRarity.cards.BookCards.Clear();
			foreach (Card card in cs)
			{
				CardRarity.cards.BookCards.Add(card.Id);
			}
		}

		// Token: 0x060089CE RID: 35278 RVA: 0x0010D86C File Offset: 0x0010BA6C
		public static List<int> GetBookCards()
		{
			CardRarity.BookSort();
			return CardRarity.cards.BookCards;
		}

		// Token: 0x060089CF RID: 35279 RVA: 0x0010D87D File Offset: 0x0010BA7D
		public static void Save()
		{
			CardRarity.Initialize();
			File.WriteAllText("Data/Rarity.json", JsonConvert.SerializeObject(CardRarity.cards, Formatting.Indented));
		}

		// Token: 0x0400C4ED RID: 50413
		private const string jsonPath = "Data/Rarity.json";

		// Token: 0x0400C4EE RID: 50414
		private static RarityCards cards;

		// Token: 0x0400C4EF RID: 50415
		private static bool initialized;

		// Token: 0x02001235 RID: 4661
		public enum Rarity
		{
			// Token: 0x0400C4F1 RID: 50417
			Unknown,
			// Token: 0x0400C4F2 RID: 50418
			Normal,
			// Token: 0x0400C4F3 RID: 50419
			Shine,
			// Token: 0x0400C4F4 RID: 50420
			Royal = 4,
			// Token: 0x0400C4F5 RID: 50421
			Gold = 8,
			// Token: 0x0400C4F6 RID: 50422
			Millennium = 16
		}
	}
}
