using System;
using System.Collections.Generic;
using System.Linq;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game
{
	// Token: 0x020001F2 RID: 498
	public class ClientField
	{
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x000296BD File Offset: 0x000278BD
		// (set) Token: 0x0600096B RID: 2411 RVA: 0x000296C5 File Offset: 0x000278C5
		public IList<ClientCard> Hand { get; private set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x000296CE File Offset: 0x000278CE
		// (set) Token: 0x0600096D RID: 2413 RVA: 0x000296D6 File Offset: 0x000278D6
		public ClientCard[] MonsterZone { get; private set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x000296DF File Offset: 0x000278DF
		// (set) Token: 0x0600096F RID: 2415 RVA: 0x000296E7 File Offset: 0x000278E7
		public ClientCard[] SpellZone { get; private set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x000296F0 File Offset: 0x000278F0
		// (set) Token: 0x06000971 RID: 2417 RVA: 0x000296F8 File Offset: 0x000278F8
		public IList<ClientCard> Graveyard { get; private set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x00029701 File Offset: 0x00027901
		// (set) Token: 0x06000973 RID: 2419 RVA: 0x00029709 File Offset: 0x00027909
		public IList<ClientCard> Banished { get; private set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x00029712 File Offset: 0x00027912
		// (set) Token: 0x06000975 RID: 2421 RVA: 0x0002971A File Offset: 0x0002791A
		public IList<ClientCard> Deck { get; private set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x00029723 File Offset: 0x00027923
		// (set) Token: 0x06000977 RID: 2423 RVA: 0x0002972B File Offset: 0x0002792B
		public IList<ClientCard> ExtraDeck { get; private set; }

		// Token: 0x06000979 RID: 2425 RVA: 0x00029734 File Offset: 0x00027934
		public void Init(int deck, int extra)
		{
			this.Hand = new List<ClientCard>();
			this.MonsterZone = new ClientCard[7];
			this.SpellZone = new ClientCard[8];
			this.Graveyard = new List<ClientCard>();
			this.Banished = new List<ClientCard>();
			this.Deck = new List<ClientCard>();
			this.ExtraDeck = new List<ClientCard>();
			for (int i = 0; i < deck; i++)
			{
				this.Deck.Add(new ClientCard(0, CardLocation.Deck, -1));
			}
			for (int j = 0; j < extra; j++)
			{
				this.ExtraDeck.Add(new ClientCard(0, CardLocation.Extra, -1));
			}
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x000297D0 File Offset: 0x000279D0
		public int GetMonstersExtraZoneCount()
		{
			int count = 0;
			if (this.MonsterZone[5] != null)
			{
				count++;
			}
			if (this.MonsterZone[6] != null)
			{
				count++;
			}
			return count;
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x000297FC File Offset: 0x000279FC
		public int GetMonsterCount()
		{
			return ClientField.GetCount(this.MonsterZone);
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x00029809 File Offset: 0x00027A09
		public int GetSpellCount()
		{
			return ClientField.GetCount(this.SpellZone);
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00029816 File Offset: 0x00027A16
		public int GetHandCount()
		{
			return ClientField.GetCount(this.Hand);
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x00029824 File Offset: 0x00027A24
		public int GetSpellCountWithoutField()
		{
			int count = 0;
			for (int i = 0; i < 5; i++)
			{
				if (this.SpellZone[i] != null)
				{
					count++;
				}
			}
			return count;
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x00029850 File Offset: 0x00027A50
		public int GetColumnCount(int zone, bool IncludeExtraMonsterZone = true)
		{
			int count = 0;
			if (this.SpellZone[zone] != null)
			{
				count++;
			}
			if (this.MonsterZone[zone] != null)
			{
				count++;
			}
			if (zone == 1 && IncludeExtraMonsterZone && this.MonsterZone[5] != null)
			{
				count++;
			}
			if (zone == 3 && IncludeExtraMonsterZone && this.MonsterZone[6] != null)
			{
				count++;
			}
			return count;
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x000298A8 File Offset: 0x00027AA8
		public int GetFieldCount()
		{
			return this.GetSpellCount() + this.GetMonsterCount();
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x000298B7 File Offset: 0x00027AB7
		public int GetFieldHandCount()
		{
			return this.GetSpellCount() + this.GetMonsterCount() + this.GetHandCount();
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x000298CD File Offset: 0x00027ACD
		public bool IsFieldEmpty()
		{
			return this.GetMonsters().Count == 0 && this.GetSpells().Count == 0;
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x000298EC File Offset: 0x00027AEC
		public int GetLinkedZones()
		{
			int zones = 0;
			for (int i = 0; i < 7; i++)
			{
				int num = zones;
				ClientCard clientCard = this.MonsterZone[i];
				zones = num | ((clientCard != null) ? clientCard.GetLinkedZones() : 0);
			}
			return zones;
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0002991F File Offset: 0x00027B1F
		public List<ClientCard> GetMonsters()
		{
			return ClientField.GetCards(this.MonsterZone);
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0002992C File Offset: 0x00027B2C
		public List<ClientCard> GetGraveyardMonsters()
		{
			return ClientField.GetCards(this.Graveyard, CardType.Monster);
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0002993A File Offset: 0x00027B3A
		public List<ClientCard> GetGraveyardSpells()
		{
			return ClientField.GetCards(this.Graveyard, CardType.Spell);
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x00029948 File Offset: 0x00027B48
		public List<ClientCard> GetGraveyardTraps()
		{
			return ClientField.GetCards(this.Graveyard, CardType.Trap);
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x00029956 File Offset: 0x00027B56
		public List<ClientCard> GetSpells()
		{
			return ClientField.GetCards(this.SpellZone);
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x00029963 File Offset: 0x00027B63
		public List<ClientCard> GetMonstersInExtraZone()
		{
			return (from card in this.GetMonsters()
				where card.Sequence >= 5
				select card).ToList<ClientCard>();
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x00029994 File Offset: 0x00027B94
		public List<ClientCard> GetMonstersInMainZone()
		{
			return (from card in this.GetMonsters()
				where card.Sequence < 5
				select card).ToList<ClientCard>();
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x000299C5 File Offset: 0x00027BC5
		public ClientCard GetFieldSpellCard()
		{
			return this.SpellZone[5];
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x000299CF File Offset: 0x00027BCF
		public bool HasInHand(int cardId)
		{
			return ClientField.HasInCards(this.Hand, cardId, false, false, false);
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x000299E0 File Offset: 0x00027BE0
		public bool HasInHand(IList<int> cardId)
		{
			return ClientField.HasInCards(this.Hand, cardId, false, false, false);
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x000299F1 File Offset: 0x00027BF1
		public bool HasInGraveyard(int cardId)
		{
			return ClientField.HasInCards(this.Graveyard, cardId, false, false, false);
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x00029A02 File Offset: 0x00027C02
		public bool HasInGraveyard(IList<int> cardId)
		{
			return ClientField.HasInCards(this.Graveyard, cardId, false, false, false);
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x00029A13 File Offset: 0x00027C13
		public bool HasInBanished(int cardId)
		{
			return ClientField.HasInCards(this.Banished, cardId, false, false, false);
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x00029A24 File Offset: 0x00027C24
		public bool HasInBanished(IList<int> cardId)
		{
			return ClientField.HasInCards(this.Banished, cardId, false, false, false);
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x00029A35 File Offset: 0x00027C35
		public bool HasInExtra(int cardId)
		{
			return ClientField.HasInCards(this.ExtraDeck, cardId, false, false, false);
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x00029A46 File Offset: 0x00027C46
		public bool HasInExtra(IList<int> cardId)
		{
			return ClientField.HasInCards(this.ExtraDeck, cardId, false, false, false);
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x00029A57 File Offset: 0x00027C57
		public bool HasAttackingMonster()
		{
			return this.GetMonsters().Any((ClientCard card) => card.IsAttack());
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x00029A83 File Offset: 0x00027C83
		public bool HasDefendingMonster()
		{
			return this.GetMonsters().Any((ClientCard card) => card.IsDefense());
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x00029AAF File Offset: 0x00027CAF
		public bool HasInMonstersZone(int cardId, bool notDisabled = false, bool hasXyzMaterial = false, bool faceUp = false)
		{
			return ClientField.HasInCards(this.MonsterZone, cardId, notDisabled, hasXyzMaterial, faceUp);
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x00029AC1 File Offset: 0x00027CC1
		public bool HasInMonstersZone(IList<int> cardId, bool notDisabled = false, bool hasXyzMaterial = false, bool faceUp = false)
		{
			return ClientField.HasInCards(this.MonsterZone, cardId, notDisabled, hasXyzMaterial, faceUp);
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x00029AD3 File Offset: 0x00027CD3
		public bool HasInSpellZone(int cardId, bool notDisabled = false, bool faceUp = false)
		{
			return ClientField.HasInCards(this.SpellZone, cardId, notDisabled, false, faceUp);
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00029AE4 File Offset: 0x00027CE4
		public bool HasInSpellZone(IList<int> cardId, bool notDisabled = false, bool faceUp = false)
		{
			return ClientField.HasInCards(this.SpellZone, cardId, notDisabled, false, faceUp);
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x00029AF5 File Offset: 0x00027CF5
		public bool HasInHandOrInSpellZone(int cardId)
		{
			return this.HasInHand(cardId) || this.HasInSpellZone(cardId, false, false);
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00029B0B File Offset: 0x00027D0B
		public bool HasInHandOrHasInMonstersZone(int cardId)
		{
			return this.HasInHand(cardId) || this.HasInMonstersZone(cardId, false, false, false);
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x00029B22 File Offset: 0x00027D22
		public bool HasInHandOrInGraveyard(int cardId)
		{
			return this.HasInHand(cardId) || this.HasInGraveyard(cardId);
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x00029B36 File Offset: 0x00027D36
		public bool HasInGraveyardOrInBanished(int cardId)
		{
			return this.HasInBanished(cardId) || this.HasInGraveyard(cardId);
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x00029B4A File Offset: 0x00027D4A
		public bool HasInMonstersZoneOrInGraveyard(int cardId)
		{
			return this.HasInMonstersZone(cardId, false, false, false) || this.HasInGraveyard(cardId);
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00029B61 File Offset: 0x00027D61
		public bool HasInSpellZoneOrInGraveyard(int cardId)
		{
			return this.HasInSpellZone(cardId, false, false) || this.HasInGraveyard(cardId);
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x00029B77 File Offset: 0x00027D77
		public bool HasInHandOrInMonstersZoneOrInGraveyard(int cardId)
		{
			return this.HasInHand(cardId) || this.HasInMonstersZone(cardId, false, false, false) || this.HasInGraveyard(cardId);
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00029B97 File Offset: 0x00027D97
		public bool HasInHandOrInSpellZoneOrInGraveyard(int cardId)
		{
			return this.HasInHand(cardId) || this.HasInSpellZone(cardId, false, false) || this.HasInGraveyard(cardId);
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x00029BB6 File Offset: 0x00027DB6
		public bool HasInHandOrInSpellZone(IList<int> cardId)
		{
			return this.HasInHand(cardId) || this.HasInSpellZone(cardId, false, false);
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x00029BCC File Offset: 0x00027DCC
		public bool HasInHandOrHasInMonstersZone(IList<int> cardId)
		{
			return this.HasInHand(cardId) || this.HasInMonstersZone(cardId, false, false, false);
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00029BE3 File Offset: 0x00027DE3
		public bool HasInHandOrInGraveyard(IList<int> cardId)
		{
			return this.HasInHand(cardId) || this.HasInGraveyard(cardId);
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x00029BF7 File Offset: 0x00027DF7
		public bool HasInMonstersZoneOrInGraveyard(IList<int> cardId)
		{
			return this.HasInMonstersZone(cardId, false, false, false) || this.HasInGraveyard(cardId);
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x00029C0E File Offset: 0x00027E0E
		public bool HasInSpellZoneOrInGraveyard(IList<int> cardId)
		{
			return this.HasInSpellZone(cardId, false, false) || this.HasInGraveyard(cardId);
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x00029C24 File Offset: 0x00027E24
		public bool HasInHandOrInMonstersZoneOrInGraveyard(IList<int> cardId)
		{
			return this.HasInHand(cardId) || this.HasInMonstersZone(cardId, false, false, false) || this.HasInGraveyard(cardId);
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x00029C44 File Offset: 0x00027E44
		public bool HasInHandOrInSpellZoneOrInGraveyard(IList<int> cardId)
		{
			return this.HasInHand(cardId) || this.HasInSpellZone(cardId, false, false) || this.HasInGraveyard(cardId);
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00029C64 File Offset: 0x00027E64
		public int GetRemainingCount(int cardId, int initialCount)
		{
			int remaining = initialCount - this.Hand.Count((ClientCard card) => card != null && card.IsOriginalCode(cardId));
			remaining -= this.SpellZone.Count((ClientCard card) => card != null && card.IsOriginalCode(cardId));
			remaining -= this.MonsterZone.Count((ClientCard card) => card != null && card.IsOriginalCode(cardId));
			remaining -= this.Graveyard.Count((ClientCard card) => card != null && card.IsOriginalCode(cardId));
			remaining -= this.Banished.Count((ClientCard card) => card != null && card.IsOriginalCode(cardId));
			if (remaining >= 0)
			{
				return remaining;
			}
			return 0;
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x00029D09 File Offset: 0x00027F09
		private static int GetCount(IEnumerable<ClientCard> cards)
		{
			return cards.Count((ClientCard card) => card != null);
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x00029D30 File Offset: 0x00027F30
		public int GetCountCardInZone(IEnumerable<ClientCard> cards, int cardId)
		{
			return cards.Count((ClientCard card) => card != null && card.IsCode(cardId));
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00029D5C File Offset: 0x00027F5C
		public int GetCountCardInZone(IEnumerable<ClientCard> cards, List<int> cardId)
		{
			return cards.Count((ClientCard card) => card != null && card.IsCode(cardId));
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00029D88 File Offset: 0x00027F88
		private static List<ClientCard> GetCards(IEnumerable<ClientCard> cards, CardType type)
		{
			return cards.Where((ClientCard card) => card != null && card.HasType(type)).ToList<ClientCard>();
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x00029DB9 File Offset: 0x00027FB9
		private static List<ClientCard> GetCards(IEnumerable<ClientCard> cards)
		{
			return cards.Where((ClientCard card) => card != null).ToList<ClientCard>();
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x00029DE8 File Offset: 0x00027FE8
		private static bool HasInCards(IEnumerable<ClientCard> cards, int cardId, bool notDisabled = false, bool hasXyzMaterial = false, bool faceUp = false)
		{
			return cards.Any((ClientCard card) => card != null && card.IsCode(cardId) && (!notDisabled || !card.IsDisabled()) && (!hasXyzMaterial || card.HasXyzMaterial()) && (!faceUp || !card.IsFacedown()));
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00029E2C File Offset: 0x0002802C
		private static bool HasInCards(IEnumerable<ClientCard> cards, IList<int> cardId, bool notDisabled = false, bool hasXyzMaterial = false, bool faceUp = false)
		{
			return cards.Any((ClientCard card) => card != null && card.IsCode(cardId) && (!notDisabled || !card.IsDisabled()) && (!hasXyzMaterial || card.HasXyzMaterial()) && (!faceUp || !card.IsFacedown()));
		}

		// Token: 0x04000D85 RID: 3461
		public int LifePoints;

		// Token: 0x04000D86 RID: 3462
		public ClientCard BattlingMonster;

		// Token: 0x04000D87 RID: 3463
		public bool UnderAttack;
	}
}
