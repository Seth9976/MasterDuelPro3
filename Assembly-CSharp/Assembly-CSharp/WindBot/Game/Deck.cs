using System;
using System.Collections.Generic;
using System.IO;
using YGOSharp.OCGWrapper;

namespace WindBot.Game
{
	// Token: 0x020001FA RID: 506
	public class Deck
	{
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x00029F9F File Offset: 0x0002819F
		// (set) Token: 0x060009CA RID: 2506 RVA: 0x00029FA7 File Offset: 0x000281A7
		public IList<NamedCard> Cards { get; private set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x00029FB0 File Offset: 0x000281B0
		// (set) Token: 0x060009CC RID: 2508 RVA: 0x00029FB8 File Offset: 0x000281B8
		public IList<NamedCard> ExtraCards { get; private set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x00029FC1 File Offset: 0x000281C1
		// (set) Token: 0x060009CE RID: 2510 RVA: 0x00029FC9 File Offset: 0x000281C9
		public IList<NamedCard> SideCards { get; private set; }

		// Token: 0x060009CF RID: 2511 RVA: 0x00029FD2 File Offset: 0x000281D2
		public Deck()
		{
			this.Cards = new List<NamedCard>();
			this.ExtraCards = new List<NamedCard>();
			this.SideCards = new List<NamedCard>();
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x00029FFC File Offset: 0x000281FC
		private void AddNewCard(int cardId, bool sideDeck)
		{
			NamedCard newCard = NamedCard.Get(cardId);
			if (newCard == null)
			{
				return;
			}
			if (!sideDeck)
			{
				this.AddCard(newCard);
				return;
			}
			this.SideCards.Add(newCard);
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0002A02B File Offset: 0x0002822B
		private void AddCard(NamedCard card)
		{
			if (card.IsExtraCard())
			{
				this.ExtraCards.Add(card);
				return;
			}
			this.Cards.Add(card);
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0002A050 File Offset: 0x00028250
		public static Deck Load(string name)
		{
			StreamReader reader = null;
			Deck deck2;
			try
			{
				reader = new StreamReader(Program.ReadFile("Decks", name, "ydk"));
				Deck deck = new Deck();
				bool side = false;
				while (!reader.EndOfStream)
				{
					string line = reader.ReadLine();
					if (line != null)
					{
						line = line.Trim();
						if (!line.StartsWith("#"))
						{
							int id;
							if (line.Equals("!side"))
							{
								side = true;
							}
							else if (int.TryParse(line, out id))
							{
								deck.AddNewCard(id, side);
							}
						}
					}
				}
				reader.Close();
				if (deck.Cards.Count > 60)
				{
					deck2 = null;
				}
				else if (deck.ExtraCards.Count > 15)
				{
					deck2 = null;
				}
				else if (deck.SideCards.Count > 15)
				{
					deck2 = null;
				}
				else
				{
					deck2 = deck;
				}
			}
			catch (Exception)
			{
				if (reader != null)
				{
					reader.Close();
				}
				deck2 = null;
			}
			return deck2;
		}
	}
}
