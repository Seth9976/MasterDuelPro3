using System;
using System.Collections.Generic;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game
{
	// Token: 0x020001FB RID: 507
	public class Duel
	{
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x0002A134 File Offset: 0x00028334
		// (set) Token: 0x060009D4 RID: 2516 RVA: 0x0002A13C File Offset: 0x0002833C
		public bool IsFirst { get; set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x0002A145 File Offset: 0x00028345
		// (set) Token: 0x060009D6 RID: 2518 RVA: 0x0002A14D File Offset: 0x0002834D
		public bool IsNewRule { get; set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060009D7 RID: 2519 RVA: 0x0002A156 File Offset: 0x00028356
		// (set) Token: 0x060009D8 RID: 2520 RVA: 0x0002A15E File Offset: 0x0002835E
		public bool IsNewRule2020 { get; set; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x0002A167 File Offset: 0x00028367
		// (set) Token: 0x060009DA RID: 2522 RVA: 0x0002A16F File Offset: 0x0002836F
		public ClientField[] Fields { get; private set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x0002A178 File Offset: 0x00028378
		// (set) Token: 0x060009DC RID: 2524 RVA: 0x0002A180 File Offset: 0x00028380
		public int Turn { get; set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x0002A189 File Offset: 0x00028389
		// (set) Token: 0x060009DE RID: 2526 RVA: 0x0002A191 File Offset: 0x00028391
		public int Player { get; set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060009DF RID: 2527 RVA: 0x0002A19A File Offset: 0x0002839A
		// (set) Token: 0x060009E0 RID: 2528 RVA: 0x0002A1A2 File Offset: 0x000283A2
		public DuelPhase Phase { get; set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x0002A1AB File Offset: 0x000283AB
		// (set) Token: 0x060009E2 RID: 2530 RVA: 0x0002A1B3 File Offset: 0x000283B3
		public MainPhase MainPhase { get; set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x0002A1BC File Offset: 0x000283BC
		// (set) Token: 0x060009E4 RID: 2532 RVA: 0x0002A1C4 File Offset: 0x000283C4
		public BattlePhase BattlePhase { get; set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x0002A1CD File Offset: 0x000283CD
		// (set) Token: 0x060009E6 RID: 2534 RVA: 0x0002A1D5 File Offset: 0x000283D5
		public int LastChainPlayer { get; set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x0002A1DE File Offset: 0x000283DE
		// (set) Token: 0x060009E8 RID: 2536 RVA: 0x0002A1E6 File Offset: 0x000283E6
		public CardLocation LastChainLocation { get; set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x0002A1EF File Offset: 0x000283EF
		// (set) Token: 0x060009EA RID: 2538 RVA: 0x0002A1F7 File Offset: 0x000283F7
		public IList<ClientCard> CurrentChain { get; set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x0002A200 File Offset: 0x00028400
		// (set) Token: 0x060009EC RID: 2540 RVA: 0x0002A208 File Offset: 0x00028408
		public IList<ChainInfo> CurrentChainInfo { get; set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x0002A211 File Offset: 0x00028411
		// (set) Token: 0x060009EE RID: 2542 RVA: 0x0002A219 File Offset: 0x00028419
		public IList<ClientCard> ChainTargets { get; set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x0002A222 File Offset: 0x00028422
		// (set) Token: 0x060009F0 RID: 2544 RVA: 0x0002A22A File Offset: 0x0002842A
		public IList<ClientCard> LastChainTargets { get; set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x0002A233 File Offset: 0x00028433
		// (set) Token: 0x060009F2 RID: 2546 RVA: 0x0002A23B File Offset: 0x0002843B
		public IList<ClientCard> ChainTargetOnly { get; set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x0002A244 File Offset: 0x00028444
		// (set) Token: 0x060009F4 RID: 2548 RVA: 0x0002A24C File Offset: 0x0002844C
		public int LastSummonPlayer { get; set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x0002A255 File Offset: 0x00028455
		// (set) Token: 0x060009F6 RID: 2550 RVA: 0x0002A25D File Offset: 0x0002845D
		public IList<ClientCard> SummoningCards { get; set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x0002A266 File Offset: 0x00028466
		// (set) Token: 0x060009F8 RID: 2552 RVA: 0x0002A26E File Offset: 0x0002846E
		public IList<ClientCard> LastSummonedCards { get; set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x0002A277 File Offset: 0x00028477
		// (set) Token: 0x060009FA RID: 2554 RVA: 0x0002A27F File Offset: 0x0002847F
		public int SolvingChainIndex { get; set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x0002A288 File Offset: 0x00028488
		// (set) Token: 0x060009FC RID: 2556 RVA: 0x0002A290 File Offset: 0x00028490
		public IList<int> NegatedChainIndexList { get; set; }

		// Token: 0x060009FD RID: 2557 RVA: 0x0002A29C File Offset: 0x0002849C
		public Duel()
		{
			this.Fields = new ClientField[2];
			this.Fields[0] = new ClientField();
			this.Fields[1] = new ClientField();
			this.LastChainPlayer = -1;
			this.LastChainLocation = (CardLocation)0;
			this.CurrentChain = new List<ClientCard>();
			this.CurrentChainInfo = new List<ChainInfo>();
			this.ChainTargets = new List<ClientCard>();
			this.LastChainTargets = new List<ClientCard>();
			this.ChainTargetOnly = new List<ClientCard>();
			this.LastSummonPlayer = -1;
			this.SummoningCards = new List<ClientCard>();
			this.LastSummonedCards = new List<ClientCard>();
			this.SolvingChainIndex = 0;
			this.NegatedChainIndexList = new List<int>();
			this.MainPhase = new MainPhase();
			this.BattlePhase = new BattlePhase();
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0002A35F File Offset: 0x0002855F
		public ClientCard GetCard(int player, CardLocation loc, int seq)
		{
			return this.GetCard(player, (int)loc, seq, 0);
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0002A36C File Offset: 0x0002856C
		public ClientCard GetCard(int player, int loc, int seq, int subSeq)
		{
			if (player < 0 || player > 1)
			{
				return null;
			}
			bool isXyz = (loc & 128) != 0;
			CardLocation location = (CardLocation)(loc & 127);
			IList<ClientCard> cards = null;
			if (location <= CardLocation.SpellZone)
			{
				switch (location)
				{
				case CardLocation.Deck:
					cards = this.Fields[player].Deck;
					break;
				case CardLocation.Hand:
					cards = this.Fields[player].Hand;
					break;
				case (CardLocation)3:
					break;
				case CardLocation.MonsterZone:
					cards = this.Fields[player].MonsterZone;
					break;
				default:
					if (location == CardLocation.SpellZone)
					{
						cards = this.Fields[player].SpellZone;
					}
					break;
				}
			}
			else if (location != CardLocation.Grave)
			{
				if (location != CardLocation.Removed)
				{
					if (location == CardLocation.Extra)
					{
						cards = this.Fields[player].ExtraDeck;
					}
				}
				else
				{
					cards = this.Fields[player].Banished;
				}
			}
			else
			{
				cards = this.Fields[player].Graveyard;
			}
			if (cards == null)
			{
				return null;
			}
			if (seq >= cards.Count)
			{
				return null;
			}
			if (!isXyz)
			{
				return cards[seq];
			}
			ClientCard card = cards[seq];
			if (card == null || subSeq >= card.Overlays.Count)
			{
				return null;
			}
			return new ClientCard(card.Overlays[subSeq], CardLocation.Overlay, 0, 0);
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0002A488 File Offset: 0x00028688
		public void AddCard(CardLocation loc, int cardId, int player, int seq, int pos)
		{
			ClientCard card = new ClientCard(cardId, loc, seq, pos);
			this.AddCard(loc, card, player, seq, pos, cardId);
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0002A4B0 File Offset: 0x000286B0
		public void AddCard(CardLocation loc, ClientCard card, int player, int seq, int pos, int id)
		{
			card.Location = loc;
			card.Sequence = seq;
			card.Position = pos;
			card.Controller = player;
			card.SetId(id);
			if (loc <= CardLocation.SpellZone)
			{
				switch (loc)
				{
				case CardLocation.Deck:
					this.Fields[player].Deck.Add(card);
					return;
				case CardLocation.Hand:
					this.Fields[player].Hand.Add(card);
					return;
				case (CardLocation)3:
					break;
				case CardLocation.MonsterZone:
					this.Fields[player].MonsterZone[seq] = card;
					return;
				default:
					if (loc != CardLocation.SpellZone)
					{
						return;
					}
					this.Fields[player].SpellZone[seq] = card;
					return;
				}
			}
			else
			{
				if (loc == CardLocation.Grave)
				{
					this.Fields[player].Graveyard.Add(card);
					return;
				}
				if (loc == CardLocation.Removed)
				{
					this.Fields[player].Banished.Add(card);
					return;
				}
				if (loc != CardLocation.Extra)
				{
					return;
				}
				this.Fields[player].ExtraDeck.Add(card);
			}
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0002A59C File Offset: 0x0002879C
		public void RemoveCard(CardLocation loc, ClientCard card, int player, int seq)
		{
			if (loc <= CardLocation.SpellZone)
			{
				switch (loc)
				{
				case CardLocation.Deck:
					this.Fields[player].Deck.Remove(card);
					return;
				case CardLocation.Hand:
					this.Fields[player].Hand.Remove(card);
					return;
				case (CardLocation)3:
					break;
				case CardLocation.MonsterZone:
					this.Fields[player].MonsterZone[seq] = null;
					return;
				default:
					if (loc != CardLocation.SpellZone)
					{
						return;
					}
					this.Fields[player].SpellZone[seq] = null;
					return;
				}
			}
			else
			{
				if (loc == CardLocation.Grave)
				{
					this.Fields[player].Graveyard.Remove(card);
					return;
				}
				if (loc == CardLocation.Removed)
				{
					this.Fields[player].Banished.Remove(card);
					return;
				}
				if (loc != CardLocation.Extra)
				{
					return;
				}
				this.Fields[player].ExtraDeck.Remove(card);
			}
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0002A666 File Offset: 0x00028866
		public int GetLocalPlayer(int player)
		{
			if (!this.IsFirst)
			{
				return 1 - player;
			}
			return player;
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0002A675 File Offset: 0x00028875
		public ClientCard GetCurrentSolvingChainCard()
		{
			if (this.SolvingChainIndex == 0 || this.SolvingChainIndex > this.CurrentChain.Count)
			{
				return null;
			}
			return this.CurrentChain[this.SolvingChainIndex - 1];
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0002A6A7 File Offset: 0x000288A7
		public ChainInfo GetCurrentSolvingChainInfo()
		{
			if (this.SolvingChainIndex == 0 || this.SolvingChainIndex > this.CurrentChainInfo.Count)
			{
				return null;
			}
			return this.CurrentChainInfo[this.SolvingChainIndex - 1];
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0002A6D9 File Offset: 0x000288D9
		public bool IsCurrentSolvingChainNegated()
		{
			return this.SolvingChainIndex > 0 && this.NegatedChainIndexList.Contains(this.SolvingChainIndex);
		}
	}
}
