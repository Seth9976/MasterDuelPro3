using System;
using System.Collections.Generic;
using System.Linq;
using YGOSharp.OCGWrapper;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x020003D7 RID: 983
	[Deck("SuperheavySamurai", "AI_SuperheavySamurai", "Normal")]
	public class SuperheavySamuraiExecutor : DefaultExecutor
	{
		// Token: 0x06001E2A RID: 7722 RVA: 0x000B7274 File Offset: 0x000B5474
		public SuperheavySamuraiExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(this.MonsterRepos));
			base.AddExecutor(ExecutorType.Activate, 74586817, new Func<bool>(this.PSYFunction));
			base.AddExecutor(ExecutorType.Activate, 65741786, new Func<bool>(this.IPFunction));
			base.AddExecutor(ExecutorType.Activate, 76471944, new Func<bool>(this.SarutobiFunction));
			base.AddExecutor(ExecutorType.Activate, 38342335, new Func<bool>(this.UnicornFunction));
			base.AddExecutor(ExecutorType.Activate, 23434538, new Func<bool>(this.MaxxCFunction));
			base.AddExecutor(ExecutorType.Activate, 14558127, new Func<bool>(base.DefaultAshBlossomAndJoyousSpring));
			base.AddExecutor(ExecutorType.Activate, 59438930, new Func<bool>(base.DefaultGhostOgreAndSnowRabbit));
			base.AddExecutor(ExecutorType.Activate, 97268402, new Func<bool>(base.DefaultBreakthroughSkill));
			base.AddExecutor(ExecutorType.Activate, 94145021, new Func<bool>(this.LockBirdFunction));
			base.AddExecutor(ExecutorType.Activate, 38814750, new Func<bool>(this.FunctionInHand));
			base.AddExecutor(ExecutorType.Activate, 73642296, new Func<bool>(this.FunctionInHand));
			base.AddExecutor(ExecutorType.Activate, 64193046, new Func<bool>(this.MasurawoFunction));
			base.AddExecutor(ExecutorType.Activate, 22423493, new Func<bool>(this.GeniusFunction));
			base.AddExecutor(ExecutorType.Activate, 83334932, new Func<bool>(this.MotorbikeFunction));
			base.AddExecutor(ExecutorType.SpSummon, 78391364);
			base.AddExecutor(ExecutorType.Activate, 78391364, new Func<bool>(this.ScalesFunction));
			base.AddExecutor(ExecutorType.SpSummon, 74586817, new Func<bool>(this.PSYFramelordOmegaSynchronFunction));
			base.AddExecutor(ExecutorType.Activate, 82112494, new Func<bool>(this.WakaushiFunction));
			base.AddExecutor(ExecutorType.Activate, 82112494, new Func<bool>(this.WakaushiEffectFunction));
			base.AddExecutor(ExecutorType.Activate, 19510093, new Func<bool>(this.BenkeiFunction));
			base.AddExecutor(ExecutorType.Activate, 19510093, new Func<bool>(this.BenkeiEffectFunction));
			base.AddExecutor(ExecutorType.Summon, 90361010, new Func<bool>(this.NormalSummonFunction));
			base.AddExecutor(ExecutorType.Activate, 90361010, new Func<bool>(this.SoulpiercerFunction));
			base.AddExecutor(ExecutorType.Summon, 34496660, new Func<bool>(this.NormalSummonFunction));
			base.AddExecutor(ExecutorType.Activate, 34496660, new Func<bool>(this.WagonFunction));
			base.AddExecutor(ExecutorType.Activate, 34496660, new Func<bool>(this.WagonFunction));
			base.AddExecutor(ExecutorType.Summon, 56727340, new Func<bool>(this.BoosterNormalSummonFunction));
			base.AddExecutor(ExecutorType.Summon, 78391364, new Func<bool>(this.ScalesNormalSummonFunction));
			base.AddExecutor(ExecutorType.Activate, 56727340, new Func<bool>(this.BoosterEquipFunction));
			base.AddExecutor(ExecutorType.Activate, 56727340, new Func<bool>(this.BoosterFunction));
			base.AddExecutor(ExecutorType.SpSummon, 28912357, new Func<bool>(this.GearGigantXyzFunction));
			base.AddExecutor(ExecutorType.Activate, 28912357, new Func<bool>(this.GearGigantFunction));
			base.AddExecutor(ExecutorType.Activate, 90361010, new Func<bool>(this.SoulpiercerEquipFunction));
			base.AddExecutor(ExecutorType.SpSummon, 33918636, new Func<bool>(this.ScarecrowLinkFunction));
			base.AddExecutor(ExecutorType.Activate, 33918636, new Func<bool>(this.ScarecrowFunction));
			base.AddExecutor(ExecutorType.SpSummon, 33918636, new Func<bool>(this.ScarecrowLinkFunction2));
			base.AddExecutor(ExecutorType.SpSummon, 30983281, new Func<bool>(this.ASStardustDragonSynchronFunction));
			base.AddExecutor(ExecutorType.Activate, 27548199, new Func<bool>(this.SavageDragonFunction));
			base.AddExecutor(ExecutorType.Activate, 30983281, new Func<bool>(this.ASStardustDragonFunction));
			base.AddExecutor(ExecutorType.Activate, 82112494, new Func<bool>(this.WakaushiReturnPFunction));
			base.AddExecutor(ExecutorType.SpSummon, 84815190, new Func<bool>(this.FleurSynchronFunction));
			base.AddExecutor(ExecutorType.Activate, 84815190, new Func<bool>(this.FleurFunction));
			base.AddExecutor(ExecutorType.Activate, 95500396, new Func<bool>(this.SoulpeacemakerEquipFunction));
			base.AddExecutor(ExecutorType.Activate, 95500396, new Func<bool>(this.SoulpeacemakerFunction));
			base.AddExecutor(ExecutorType.SpSummon, 22423493, new Func<bool>(this.GeniusLinkFunction));
			base.AddExecutor(ExecutorType.SpSummon, new Func<bool>(this.Psummon));
			base.AddExecutor(ExecutorType.SpSummon, 27381364, new Func<bool>(this.ElfLinkFunction));
			base.AddExecutor(ExecutorType.Activate, 27381364, new Func<bool>(this.ElfFunction));
			base.AddExecutor(ExecutorType.Activate, 83334932, new Func<bool>(this.MotorbikeFunction));
			base.AddExecutor(ExecutorType.SpSummon, 27548199, new Func<bool>(this.SavageDragonSynchronFunction));
			base.AddExecutor(ExecutorType.SpSummon, 65741786, new Func<bool>(this.IPLinkFunction));
			base.AddExecutor(ExecutorType.Activate, 10604644, new Func<bool>(this.RegulusFunction));
			base.AddExecutor(ExecutorType.Activate, 56727340, new Func<bool>(this.BoosterEquipFunction2));
			base.AddExecutor(ExecutorType.Activate, 56727340, new Func<bool>(this.BoosterFunction));
			base.AddExecutor(ExecutorType.Activate, 56727340, new Func<bool>(this.BoosterEquipFunction3));
			base.AddExecutor(ExecutorType.Activate, 56727340, new Func<bool>(this.BoosterFunction));
			base.AddExecutor(ExecutorType.SpSummon, 64193046, new Func<bool>(this.MasurawoSynchronFunction));
			base.AddExecutor(ExecutorType.SpSummon, 76471944, new Func<bool>(this.DeSynchronFunction));
			base.AddExecutor(ExecutorType.Activate, 14624296, new Func<bool>(this.SoulhornsEquipFunction));
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x000B781C File Offset: 0x000B5A1C
		public override void OnNewTurn()
		{
			this.normal_summon = false;
			this.p_summoned = false;
			this.p_summoning = false;
			this.activate_Motorbike = false;
			this.activate_Wakaushi = false;
			this.activate_Scales = false;
			this.activate_Wagon = false;
			this.activate_Booster = false;
			this.activate_Soulpeacemaker = false;
			this.activate_Benkei = false;
			this.need_Gear = false;
			this.activate_Scarecrow = false;
			this.summon_Scarecrow = false;
			this.summon_Scarecrow2 = true;
			this.activate_Elf = false;
			this.summon_Elf = false;
			this.activate_MaxxG = false;
			this.activate_PSY = false;
			this.activate_LockBird = false;
			this.activate_Genius = false;
			this.activate_Sarutobi = false;
			this.to_deck = false;
			base.OnNewTurn();
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x0000763C File Offset: 0x0000583C
		public override bool OnSelectHand()
		{
			return true;
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x000B78CC File Offset: 0x000B5ACC
		private bool MonsterRepos()
		{
			return base.Card.IsFacedown() || (base.Card.IsFaceup() && base.Card.IsAttack() && (base.Card.Id == 64193046 || base.Card.Id == 76471944));
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x000B7929 File Offset: 0x000B5B29
		public override CardPosition OnSelectPosition(int cardId, IList<CardPosition> positions)
		{
			if (NamedCard.Get(cardId) != null && (cardId == 64193046 || cardId == 76471944))
			{
				return CardPosition.FaceUpDefence;
			}
			return (CardPosition)0;
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x000B7948 File Offset: 0x000B5B48
		public override int OnSelectPlace(int cardId, int player, CardLocation location, int available)
		{
			if (player == 0 && location == CardLocation.MonsterZone)
			{
				if (cardId == 33918636)
				{
					int a = 64 & available;
					int b = 32 & available;
					if (base.Bot.MonsterZone[2] != null && base.Bot.MonsterZone[2].Controller == 0 && !this.FinalCards(base.Bot.MonsterZone[2].Id))
					{
						a = 0;
					}
					else if (base.Bot.MonsterZone[0] != null && base.Bot.MonsterZone[0].Controller == 0 && !this.FinalCards(base.Bot.MonsterZone[0].Id))
					{
						b = 0;
					}
					if (b > 0)
					{
						return 32;
					}
					if (a > 0)
					{
						return 64;
					}
				}
				else if (cardId == 38342335 || cardId == 27381364 || cardId == 65741786)
				{
					if ((64 & available) > 0)
					{
						return 64;
					}
					if ((32 & available) > 0)
					{
						return 32;
					}
				}
				else if (cardId == 22423493)
				{
					int a2 = 64 & available;
					int b2 = 32 & available;
					if (base.Bot.MonsterZone[4] != null && base.Bot.MonsterZone[4].Controller == 0 && !this.FinalCards(base.Bot.MonsterZone[4].Id))
					{
						a2 = 0;
					}
					else if (base.Bot.MonsterZone[0] != null && base.Bot.MonsterZone[0].Controller == 0 && !this.FinalCards(base.Bot.MonsterZone[0].Id))
					{
						b2 = 0;
					}
					if (a2 > 0)
					{
						return 64;
					}
					if (b2 > 0)
					{
						return 32;
					}
				}
				else if (cardId == 10604644 || cardId == 28912357)
				{
					if ((8 & available) > 0)
					{
						return 8;
					}
				}
				else
				{
					if ((2 & available) > 0)
					{
						return 2;
					}
					if ((16 & available) > 0)
					{
						return 16;
					}
					if ((4 & available) > 0)
					{
						return 4;
					}
					if ((8 & available) > 0)
					{
						return 8;
					}
					if ((1 & available) > 0)
					{
						return 1;
					}
				}
			}
			return base.OnSelectPlace(cardId, player, location, available);
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x000B7B34 File Offset: 0x000B5D34
		public override IList<ClientCard> OnSelectCard(IList<ClientCard> cards, int min, int max, int hint, bool cancelable)
		{
			if (base.AI.HaveSelectedCards())
			{
				return null;
			}
			if (this.p_summoning || ((base.Card == base.Bot.SpellZone[0] || base.Card == base.Bot.SpellZone[4]) && hint == 509 && base.Card.HasType(CardType.Pendulum)))
			{
				List<ClientCard> result = new List<ClientCard>();
				List<ClientCard> scards = cards.Where((ClientCard card) => card != null && card.HasSetcode(154) && card.Level == 4).ToList<ClientCard>();
				if (scards.Count < 2)
				{
					scards = cards.Where((ClientCard card) => card != null && card.HasSetcode(154)).ToList<ClientCard>();
				}
				this.p_summoning = false;
				if (scards.Count > 0)
				{
					return base.Util.CheckSelectCount(result, scards, 1, 1);
				}
				if (min == 0)
				{
					return result;
				}
			}
			return base.OnSelectCard(cards, min, max, hint, cancelable);
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x000B7C40 File Offset: 0x000B5E40
		private List<ClientCard> GetZoneCards(CardLocation loc, ClientField player)
		{
			List<ClientCard> res = new List<ClientCard>();
			List<ClientCard> temp = new List<ClientCard>();
			if ((loc & CardLocation.Hand) > (CardLocation)0)
			{
				temp = player.Hand.Where((ClientCard card) => card != null).ToList<ClientCard>();
				if (temp.Count<ClientCard>() > 0)
				{
					res.AddRange(temp);
				}
			}
			if ((loc & CardLocation.MonsterZone) > (CardLocation)0)
			{
				temp = player.GetMonsters();
				if (temp.Count<ClientCard>() > 0)
				{
					res.AddRange(temp);
				}
			}
			if ((loc & CardLocation.SpellZone) > (CardLocation)0)
			{
				temp = player.GetSpells();
				if (temp.Count<ClientCard>() > 0)
				{
					res.AddRange(temp);
				}
			}
			if ((loc & CardLocation.Grave) > (CardLocation)0)
			{
				temp = player.Graveyard.Where((ClientCard card) => card != null).ToList<ClientCard>();
				if (temp.Count<ClientCard>() > 0)
				{
					res.AddRange(temp);
				}
			}
			if ((loc & CardLocation.Removed) > (CardLocation)0)
			{
				temp = player.Banished.Where((ClientCard card) => card != null).ToList<ClientCard>();
				if (temp.Count<ClientCard>() > 0)
				{
					res.AddRange(temp);
				}
			}
			if ((loc & CardLocation.Extra) > (CardLocation)0)
			{
				temp = player.ExtraDeck.Where((ClientCard card) => card != null).ToList<ClientCard>();
				if (temp.Count<ClientCard>() > 0)
				{
					res.AddRange(temp);
				}
			}
			return res;
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x000B7DB0 File Offset: 0x000B5FB0
		private bool FinalCards(int cname)
		{
			foreach (int cardname in new int[] { 64193046, 84815190, 27548199, 76471944, 10604644, 65741786 })
			{
				if (cname == cardname)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x000B7DE8 File Offset: 0x000B5FE8
		private bool TurnerCards(int cname)
		{
			foreach (int cardname in new int[] { 38814750, 82112494, 83334932 })
			{
				if (cname == cardname)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x000B7E20 File Offset: 0x000B6020
		private bool Psummon()
		{
			if ((from card in this.GetZoneCards(CardLocation.Hand, base.Bot)
				where card != null && card.HasSetcode(154) && card.Level > 1 && card.Level < 8
				select card).ToList<ClientCard>().Count > 0 && base.Card.Location == CardLocation.SpellZone)
			{
				this.p_summoning = true;
				this.p_summoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x000B7E8A File Offset: 0x000B608A
		private bool MaxxCFunction()
		{
			this.activate_MaxxG = true;
			return base.DefaultMaxxC() && !this.activate_LockBird;
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x000B4B24 File Offset: 0x000B2D24
		private bool FunctionInHand()
		{
			return !base.DefaultCheckWhetherCardIsNegated(base.Card) && base.Duel.LastChainPlayer == 1;
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x000B7EA6 File Offset: 0x000B60A6
		private bool LockBirdFunction()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (base.Duel.Player == 0 || this.activate_LockBird)
			{
				return false;
			}
			this.activate_LockBird = true;
			return !this.activate_MaxxG;
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x000B7EE0 File Offset: 0x000B60E0
		private bool MotorbikeFunction()
		{
			if (base.Card.Location == CardLocation.Hand)
			{
				if (base.DefaultCheckWhetherCardIsNegated(base.Card))
				{
					return false;
				}
				List<ClientCard> cards = (from card in this.GetZoneCards(CardLocation.MonsterZone, base.Bot)
					where card != null && card.IsFaceup()
					select card).ToList<ClientCard>();
				int targetid;
				if (!base.Bot.HasInHand(82112494) && !base.Bot.HasInMonstersZone(82112494, false, false, false) && !base.Bot.HasInSpellZone(82112494, false, false) && !this.activate_Wakaushi)
				{
					targetid = 82112494;
				}
				else if (cards.Count<ClientCard>() == 0 && !this.normal_summon)
				{
					targetid = 90361010;
				}
				else if (!base.Bot.HasInHand(95500396) && !base.Bot.HasInSpellZone(95500396, false, false) && !this.activate_Soulpeacemaker && (this.normal_summon || base.Bot.HasInMonstersZone(33918636, false, false, false)))
				{
					targetid = 95500396;
				}
				else
				{
					targetid = 90361010;
				}
				if (targetid > 0)
				{
					base.AI.SelectCard(targetid);
				}
				this.activate_Motorbike = true;
				return true;
			}
			else
			{
				if (base.Card.Location == CardLocation.MonsterZone && this.activate_Elf)
				{
					base.AI.SelectCard(base.Card);
					this.activate_Elf = false;
					return true;
				}
				return false;
			}
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x000B8050 File Offset: 0x000B6250
		private bool BoosterNormalSummonFunction()
		{
			List<ClientCard> cards = (from card in base.Bot.Hand.GetMonsters()
				where card != null && card.Id == 56727340
				select card).ToList<ClientCard>();
			return this.NormalSummonFunction() && !this.activate_Booster && cards.Count >= 2;
		}

		// Token: 0x06001E3A RID: 7738 RVA: 0x000B80B8 File Offset: 0x000B62B8
		private bool ScalesNormalSummonFunction()
		{
			return this.NormalSummonFunction() && (base.Bot.HasInGraveyard(new int[] { 90361010, 83334932, 82112494, 34496660, 56727340 }) || (base.Bot.HasInHand(56727340) && !this.activate_Booster));
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x000B810C File Offset: 0x000B630C
		private bool NormalSummonFunction()
		{
			this.normal_summon = true;
			return base.DefaultMonsterSummon();
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x000B811B File Offset: 0x000B631B
		private bool ScalesFunction()
		{
			base.AI.SelectCard(new int[] { 90361010, 83334932, 82112494, 34496660, 56727340 });
			this.activate_Scales = true;
			return true;
		}

		// Token: 0x06001E3D RID: 7741 RVA: 0x000B8144 File Offset: 0x000B6344
		private bool WagonFunction()
		{
			if (base.ActivateDescription == base.Util.GetStringId(34496660, 0))
			{
				return base.Card.IsAttack();
			}
			if (base.ActivateDescription == base.Util.GetStringId(34496660, 1))
			{
				int targetid = -1;
				if (!base.Bot.HasInHand(90361010) && !base.Bot.HasInMonstersZone(90361010, false, false, false))
				{
					targetid = 90361010;
				}
				else if (!base.Bot.HasInHand(95500396) && !this.activate_Soulpeacemaker)
				{
					targetid = 95500396;
				}
				else if (!base.Bot.HasInHand(56727340) && !this.activate_Booster)
				{
					targetid = 56727340;
				}
				if (targetid > 0)
				{
					base.AI.SelectCard(targetid);
				}
				this.activate_Wagon = true;
				return true;
			}
			return true;
		}

		// Token: 0x06001E3E RID: 7742 RVA: 0x000B8220 File Offset: 0x000B6420
		private bool SoulpiercerFunction()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				int CardCount = this.GetZoneCards(CardLocation.Hand, base.Bot).Count((ClientCard card) => card != null && card.HasSetcode(154) && card.Level >= 2 && card.Level <= 7);
				int targetid;
				if (!base.Bot.HasInHand(83334932) && !this.activate_Motorbike)
				{
					targetid = 83334932;
				}
				else if (!base.Bot.HasInHand(82112494) && !base.Bot.HasInMonstersZone(82112494, false, false, false) && !base.Bot.HasInSpellZone(82112494, false, false) && !this.activate_Wakaushi)
				{
					targetid = 82112494;
				}
				else if (!base.Bot.HasInHand(95500396) && !this.activate_Soulpeacemaker)
				{
					targetid = 95500396;
				}
				else if (!base.Bot.HasInHand(78391364) && !this.activate_Scales && (!this.normal_summon || !this.p_summoned) && (this.activate_Soulpeacemaker || (!base.Bot.HasInHand(95500396) && !base.Bot.HasInSpellZone(95500396, false, false))))
				{
					targetid = 78391364;
				}
				else if (!base.Bot.HasInHand(34496660) && !this.activate_Wagon)
				{
					targetid = 34496660;
				}
				else if (CardCount < 2 && !this.p_summoned)
				{
					targetid = 82112494;
				}
				else if (!base.Bot.HasInHand(56727340) && !this.activate_Booster)
				{
					targetid = 56727340;
				}
				else if (!base.Bot.HasInHand(14624296) && !base.Bot.HasInSpellZone(14624296, false, false) && (base.Bot.HasInMonstersZone(76471944, false, false, false) || base.Bot.HasInMonstersZone(64193046, false, false, false)))
				{
					targetid = 14624296;
				}
				else
				{
					targetid = 82112494;
				}
				if (targetid > 0)
				{
					base.AI.SelectCard(targetid);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x000B8440 File Offset: 0x000B6640
		private bool WakaushiFunction()
		{
			if (base.Card.Location != CardLocation.Hand || base.Bot.HasInMonstersZone(82112494, false, false, false))
			{
				return false;
			}
			ClientCard i = base.Util.GetPZone(0, 0);
			ClientCard r = base.Util.GetPZone(0, 1);
			return (i == null && r == null) || (i == null && r.RScale != base.Card.LScale) || (r == null && i.LScale != base.Card.RScale);
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x000B84C7 File Offset: 0x000B66C7
		private bool WakaushiEffectFunction()
		{
			if (base.Card.Location == CardLocation.SpellZone)
			{
				base.AI.SelectCard(19510093);
				this.activate_Wakaushi = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x000B84F4 File Offset: 0x000B66F4
		private bool BenkeiFunction()
		{
			if (base.Card.Location != CardLocation.Hand || base.Bot.HasInSpellZone(19510093, false, false))
			{
				return false;
			}
			IEnumerable<ClientCard> enumerable = (from card in this.GetZoneCards(CardLocation.Hand, base.Bot)
				where card != null && card.Id == 19510093
				select card).ToList<ClientCard>();
			List<ClientCard> cards2 = (from card in this.GetZoneCards(CardLocation.Removed, base.Bot)
				where card != null && card.Id == 19510093
				select card).ToList<ClientCard>();
			return enumerable.Count<ClientCard>() >= 2 || base.Bot.HasInGraveyard(19510093) || base.Bot.HasInExtra(19510093) || cards2.Count<ClientCard>() > 0;
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x000B85CC File Offset: 0x000B67CC
		private bool BenkeiEffectFunction()
		{
			if (base.Card.Location == CardLocation.SpellZone)
			{
				int targetid = -1;
				if (!base.Bot.HasInHand(90361010) && !base.Bot.HasInMonstersZone(90361010, false, false, false) && !base.Bot.HasInSpellZone(90361010, false, false) && (!base.Bot.HasInMonstersZone(33918636, false, false, false) || this.activate_Soulpeacemaker))
				{
					targetid = 90361010;
				}
				else if (!base.Bot.HasInHand(95500396) && !this.activate_Soulpeacemaker)
				{
					targetid = 95500396;
				}
				else if (!base.Bot.HasInHand(56727340) && !this.activate_Booster)
				{
					targetid = 56727340;
				}
				if (targetid > 0)
				{
					base.AI.SelectCard(targetid);
				}
				this.activate_Benkei = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x000B86A8 File Offset: 0x000B68A8
		private bool WakaushiReturnPFunction()
		{
			if (base.Card.Location == CardLocation.Extra || base.Card.Location == CardLocation.Removed)
			{
				ClientCard i = base.Util.GetPZone(0, 0);
				ClientCard r = base.Util.GetPZone(0, 1);
				if (i == null && r == null)
				{
					return true;
				}
				if (i == null && r.RScale != base.Card.LScale)
				{
					return true;
				}
				if (r == null && i.LScale != base.Card.RScale)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x000B8728 File Offset: 0x000B6928
		private bool MasurawoFunction()
		{
			if (base.ActivateDescription == 96)
			{
				List<ClientCard> cards = (from card in this.GetZoneCards(CardLocation.SpellZone, base.Bot)
					where card != null && card.HasSetcode(154)
					select card).ToList<ClientCard>();
				if (cards.Count > 0)
				{
					base.AI.SelectCard(cards);
					return true;
				}
				cards = (from card in this.GetZoneCards(CardLocation.MonsterZone, base.Bot)
					where card != null && card.HasSetcode(154) && !this.FinalCards(card.Id)
					select card).ToList<ClientCard>();
				if (cards.Count > 0)
				{
					base.AI.SelectCard(cards);
					return true;
				}
			}
			return true;
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x000B87CC File Offset: 0x000B69CC
		private bool MasurawoSynchronFunction()
		{
			bool chk = true;
			if (base.Bot.HasInMonstersZone(30983281, false, false, false) || base.Bot.HasInMonstersZone(19510093, false, false, false))
			{
				chk = false;
			}
			List<List<ClientCard>> materials_lists = base.Util.GetSynchroMaterials(base.Bot.MonsterZone, 12, 1, 1, false, chk, null, (ClientCard card) => !this.FinalCards(card.Id));
			if (materials_lists.Count <= 0)
			{
				return false;
			}
			base.AI.SelectMaterials(materials_lists[0], 0);
			base.AI.SelectPosition(CardPosition.FaceUpDefence);
			return true;
		}

		// Token: 0x06001E46 RID: 7750 RVA: 0x000B885C File Offset: 0x000B6A5C
		private bool FleurSynchronFunction()
		{
			bool chk = true;
			if (base.Bot.HasInMonstersZone(83334932, false, false, false) && (base.Bot.HasInMonstersZone(30983281, false, false, false) || base.Bot.HasInMonstersZone(19510093, false, false, false)))
			{
				chk = false;
			}
			List<List<ClientCard>> materials_lists = base.Util.GetSynchroMaterials(base.Bot.MonsterZone, 10, 1, 1, false, chk, null, (ClientCard card) => !this.FinalCards(card.Id));
			if (materials_lists.Count <= 0)
			{
				return false;
			}
			base.AI.SelectMaterials(materials_lists[0], 0);
			return true;
		}

		// Token: 0x06001E47 RID: 7751 RVA: 0x000B3104 File Offset: 0x000B1304
		private bool DeSynchronFunction()
		{
			base.AI.SelectPosition(CardPosition.FaceUpDefence);
			return true;
		}

		// Token: 0x06001E48 RID: 7752 RVA: 0x000B88F5 File Offset: 0x000B6AF5
		private bool SavageDragonSynchronFunction()
		{
			return base.Bot.HasInGraveyard(new int[] { 33918636, 65741786, 22423493, 38342335, 27381364 });
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x000B8918 File Offset: 0x000B6B18
		private bool ASStardustDragonSynchronFunction()
		{
			if (base.Bot.HasInGraveyard(83334932) || base.Bot.HasInGraveyard(38814750))
			{
				return base.Bot.HasInExtra(84815190) || base.Bot.HasInExtra(64193046);
			}
			if (base.Bot.HasInMonstersZone(83334932, false, false, false))
			{
				base.AI.SelectMaterials(83334932, 0);
				return true;
			}
			if (base.Bot.HasInMonstersZone(38814750, false, false, false))
			{
				base.AI.SelectMaterials(38814750, 0);
				return true;
			}
			return false;
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x000B89C0 File Offset: 0x000B6BC0
		private bool PSYFramelordOmegaSynchronFunction()
		{
			if (base.Bot.HasInMonstersZone(83334932, false, false, false))
			{
				base.AI.SelectMaterials(83334932, 0);
			}
			else if (base.Bot.HasInMonstersZone(38814750, false, false, false))
			{
				base.AI.SelectMaterials(38814750, 0);
			}
			return this.activate_PSY || this.activate_Scales;
		}

		// Token: 0x06001E4B RID: 7755 RVA: 0x000B8A2B File Offset: 0x000B6C2B
		private bool SavageDragonFunction()
		{
			if (base.Duel.LastChainPlayer == 1)
			{
				return true;
			}
			base.AI.SelectCard(new int[] { 38342335, 22423493, 27381364, 65741786, 33918636 });
			return true;
		}

		// Token: 0x06001E4C RID: 7756 RVA: 0x000B8A5C File Offset: 0x000B6C5C
		private bool ASStardustDragonFunction()
		{
			if (base.Duel.LastChainPlayer == 1 && base.ActivateDescription == base.Util.GetStringId(30983281, 1))
			{
				return true;
			}
			if (base.ActivateDescription == base.Util.GetStringId(30983281, 0))
			{
				int targetid = -1;
				if (base.Bot.HasInGraveyard(83334932))
				{
					targetid = 83334932;
				}
				else if (base.Bot.HasInGraveyard(38814750))
				{
					targetid = 38814750;
				}
				if (targetid > 0)
				{
					base.AI.SelectCard(targetid);
				}
				if (targetid == 83334932 && !base.Bot.HasInExtra(84815190) && base.Bot.HasInExtra(64193046))
				{
					this.activate_Elf = true;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001E4D RID: 7757 RVA: 0x000B8B28 File Offset: 0x000B6D28
		private bool ScarecrowLinkFunction()
		{
			List<ClientCard> material = new List<ClientCard>();
			if (((from card in base.Bot.GetMonstersInExtraZone()
				where card != null && card.Id == 33918636
				select card).ToList<ClientCard>().Count<ClientCard>() > 0 && !this.summon_Scarecrow) || this.summon_Scarecrow || this.activate_Scarecrow)
			{
				return false;
			}
			int targetid = -1;
			if (base.Bot.MonsterZone[0] != null && base.Bot.MonsterZone[2] != null)
			{
				if (base.Bot.MonsterZone[0].Id == 90361010)
				{
					material.Add(base.Bot.MonsterZone[0]);
				}
				else if (base.Bot.MonsterZone[2].Id == 90361010)
				{
					material.Add(base.Bot.MonsterZone[2]);
				}
				else if (!this.FinalCards(base.Bot.MonsterZone[0].Id) && base.Bot.MonsterZone[0].HasSetcode(154))
				{
					material.Add(base.Bot.MonsterZone[0]);
				}
				else if (!this.FinalCards(base.Bot.MonsterZone[2].Id) && base.Bot.MonsterZone[2].HasSetcode(154))
				{
					material.Add(base.Bot.MonsterZone[2]);
				}
			}
			else if (base.Bot.HasInMonstersZone(90361010, false, false, false))
			{
				targetid = 90361010;
			}
			else if (base.Bot.HasInMonstersZone(34496660, false, false, false))
			{
				targetid = 34496660;
			}
			if (material.Count > 0)
			{
				base.AI.SelectMaterials(material, 0);
			}
			else if (targetid > 0)
			{
				base.AI.SelectMaterials(targetid, 0);
			}
			this.summon_Scarecrow = true;
			return base.Bot.HasInGraveyard(new int[] { 90361010, 82112494, 19510093, 34496660 }) || base.Bot.HasInMonstersZone(new int[] { 90361010, 34496660, 82112494 }, false, false, false);
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x00066D4B File Offset: 0x00064F4B
		private bool DragonRavineField()
		{
			return base.Card.Location == CardLocation.Hand && base.DefaultField();
		}

		// Token: 0x06001E4F RID: 7759 RVA: 0x000B8D54 File Offset: 0x000B6F54
		private bool ScarecrowFunction()
		{
			int tributeId = -1;
			if (base.Bot.HasInHand(49036338))
			{
				tributeId = 49036338;
			}
			else if (base.Bot.HasInHand(38814750))
			{
				tributeId = 38814750;
			}
			else if (base.Bot.HasInHand(19510093))
			{
				tributeId = 19510093;
			}
			else if (base.Bot.HasInHand(73642296))
			{
				tributeId = 73642296;
			}
			else if (base.Bot.HasInHand(97268402))
			{
				tributeId = 97268402;
			}
			else if (base.Bot.HasInHand(59438930))
			{
				tributeId = 59438930;
			}
			else if (base.Bot.HasInHand(14558127))
			{
				tributeId = 14558127;
			}
			else if (base.Bot.HasInHand(56727340))
			{
				tributeId = 56727340;
			}
			else if (base.Bot.HasInHand(34496660))
			{
				tributeId = 34496660;
			}
			else if (base.Bot.HasInHand(78391364))
			{
				tributeId = 78391364;
			}
			else if (base.Bot.HasInHand(94145021))
			{
				tributeId = 94145021;
			}
			else if (base.Bot.HasInHand(23434538))
			{
				tributeId = 23434538;
			}
			int needId = -1;
			if (base.Bot.HasInGraveyard(90361010))
			{
				if (base.Bot.HasInGraveyard(78391364) && !this.activate_Scales)
				{
					needId = 78391364;
				}
				else
				{
					needId = 90361010;
				}
			}
			else if (base.Bot.HasInGraveyard(64193046))
			{
				needId = 64193046;
			}
			else if (base.Bot.HasInGraveyard(76471944))
			{
				needId = 76471944;
			}
			else if (base.Bot.HasInMonstersZone(90361010, false, false, false))
			{
				if (base.Bot.HasInGraveyard(82112494))
				{
					needId = 82112494;
				}
				if (base.Bot.HasInGraveyard(83334932))
				{
					needId = 83334932;
				}
			}
			if (this.GetZoneCards(CardLocation.Hand, base.Bot).Count((ClientCard card) => card != null && card.Id == 78391364) + this.GetZoneCards(CardLocation.Grave, base.Bot).Count((ClientCard card) => card != null && card.Id == 78391364) + this.GetZoneCards(CardLocation.Onfield, base.Bot).Count((ClientCard card) => card != null && card.Id == 78391364) == 2)
			{
				if (this.GetZoneCards(CardLocation.Hand, base.Bot).Count((ClientCard card) => card != null && card.Id == 78391364) >= 1 && !this.activate_Scales)
				{
					tributeId = 78391364;
					needId = 78391364;
				}
			}
			base.AI.SelectCard(tributeId);
			base.AI.SelectNextCard(needId);
			if (((!base.Bot.HasInHand(82112494) && !base.Bot.HasInSpellZone(82112494, false, false)) || this.activate_Wakaushi) && (!base.Bot.HasInHand(83334932) || this.activate_Motorbike) && ((!base.Bot.HasInHand(95500396) && !base.Bot.HasInSpellZone(95500396, false, false)) || this.activate_Soulpeacemaker) && (!base.Bot.HasInSpellZone(19510093, false, false) || this.activate_Benkei) && needId == 90361010 && (!this.activate_Wakaushi || !this.activate_Motorbike || !this.activate_Soulpeacemaker || !this.activate_Benkei))
			{
				this.summon_Scarecrow2 = false;
			}
			this.activate_Scarecrow = true;
			return true;
		}

		// Token: 0x06001E50 RID: 7760 RVA: 0x000B912E File Offset: 0x000B732E
		private bool ScarecrowLinkFunction2()
		{
			if (!this.summon_Scarecrow2)
			{
				this.summon_Scarecrow2 = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x000B9144 File Offset: 0x000B7344
		private bool UnicornFunction()
		{
			List<ClientCard> Enemycards = this.GetZoneCards(CardLocation.Onfield, base.Enemy);
			if (base.Bot.Hand.Count != 0)
			{
				if (Enemycards.Count((ClientCard card) => card != null && !card.IsShouldNotBeTarget()) != 0)
				{
					int tributeId = -1;
					if (base.Bot.HasInHand(49036338))
					{
						tributeId = 49036338;
					}
					else if (base.Bot.HasInHand(38814750))
					{
						tributeId = 38814750;
					}
					else if (base.Bot.HasInHand(19510093))
					{
						tributeId = 19510093;
					}
					else if (base.Bot.HasInHand(73642296))
					{
						tributeId = 73642296;
					}
					else if (base.Bot.HasInHand(97268402))
					{
						tributeId = 97268402;
					}
					else if (base.Bot.HasInHand(59438930))
					{
						tributeId = 59438930;
					}
					else if (base.Bot.HasInHand(14558127))
					{
						tributeId = 14558127;
					}
					else if (base.Bot.HasInHand(56727340))
					{
						tributeId = 56727340;
					}
					else if (base.Bot.HasInHand(34496660))
					{
						tributeId = 34496660;
					}
					else if (base.Bot.HasInHand(78391364))
					{
						tributeId = 78391364;
					}
					else if (base.Bot.HasInHand(94145021))
					{
						tributeId = 94145021;
					}
					else if (base.Bot.HasInHand(23434538))
					{
						tributeId = 23434538;
					}
					if (this.to_deck)
					{
						this.to_deck = false;
					}
					base.AI.SelectCard(tributeId);
					return true;
				}
			}
			if (this.to_deck)
			{
				this.to_deck = false;
			}
			return false;
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x000B9314 File Offset: 0x000B7514
		private bool BoosterEquipFunction()
		{
			if (base.Card.Location != CardLocation.Hand || this.activate_Booster)
			{
				return false;
			}
			foreach (ClientCard card4 in base.Bot.Hand.GetMonsters().ToList<ClientCard>())
			{
				if (card4.Id == 83334932 && !this.activate_Motorbike)
				{
					return false;
				}
				if (card4.Id == 90361010)
				{
					return false;
				}
				if (card4.Id == 95500396 && !this.activate_Soulpeacemaker)
				{
					return false;
				}
				if (card4.Id == 82112494 && !this.activate_Wakaushi)
				{
					return false;
				}
				if (card4.Id == 34496660 && (!this.activate_Wagon || !this.normal_summon))
				{
					return false;
				}
				if (card4.Id == 19510093 && !this.activate_Benkei)
				{
					return false;
				}
			}
			foreach (ClientCard card2 in (from card in this.GetZoneCards(CardLocation.SpellZone, base.Bot)
				where card != null && card.IsFaceup()
				select card).ToList<ClientCard>())
			{
				if (card2.Id == 82112494 && !this.activate_Wakaushi)
				{
					return false;
				}
				if (card2.Id == 90361010)
				{
					return false;
				}
				if (card2.Id == 95500396 && !this.activate_Soulpeacemaker)
				{
					return false;
				}
				if (card2.Id == 82112494 && !this.activate_Wakaushi)
				{
					return false;
				}
				if (card2.Id == 19510093 && !this.activate_Benkei)
				{
					return false;
				}
			}
			List<ClientCard> ChkCardsMonster = (from card in this.GetZoneCards(CardLocation.MonsterZone, base.Bot)
				where card != null && card.IsFaceup() && card.Level == 4
				select card).ToList<ClientCard>();
			if (ChkCardsMonster.Count == 0)
			{
				return false;
			}
			using (List<ClientCard>.Enumerator enumerator = ChkCardsMonster.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Id == 90361010)
					{
						return false;
					}
				}
			}
			foreach (ClientCard card3 in this.GetZoneCards(CardLocation.Grave, base.Bot).ToList<ClientCard>())
			{
				if (card3.Id == 90361010 && (base.Bot.HasInMonstersZone(33918636, false, false, false) || base.Bot.HasInExtra(33918636)))
				{
					return false;
				}
				if (card3.Level == 4 && card3.HasRace(CardRace.Machine) && base.Bot.HasInHand(78391364) && !this.normal_summon)
				{
					return false;
				}
			}
			if (base.Bot.HasInExtra(65741786) && this.p_summoned)
			{
				return true;
			}
			this.need_Gear = true;
			return true;
		}

		// Token: 0x06001E53 RID: 7763 RVA: 0x000B9694 File Offset: 0x000B7894
		private bool BoosterEquipFunction2()
		{
			return base.Bot.HasInExtra(65741786) && this.p_summoned && !this.activate_Booster;
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x000B96BC File Offset: 0x000B78BC
		private bool BoosterEquipFunction3()
		{
			List<ClientCard> cards = (from card in this.GetZoneCards(CardLocation.MonsterZone, base.Bot)
				where card != null && card.IsFaceup() && !this.FinalCards(card.Id) && card.Id != 33918636
				select card).ToList<ClientCard>();
			return base.Bot.HasInMonstersZone(65741786, false, false, false) && this.p_summoned && !this.activate_Booster && cards.Count<ClientCard>() == 0;
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x000B971D File Offset: 0x000B791D
		private bool BoosterFunction()
		{
			if (base.Card.Location == CardLocation.SpellZone)
			{
				this.activate_Booster = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001E56 RID: 7766 RVA: 0x000B9737 File Offset: 0x000B7937
		private bool GearGigantXyzFunction()
		{
			if (this.need_Gear)
			{
				this.need_Gear = false;
				return true;
			}
			return false;
		}

		// Token: 0x06001E57 RID: 7767 RVA: 0x000B974C File Offset: 0x000B794C
		private bool GearGigantFunction()
		{
			if (base.Card.Location == CardLocation.MonsterZone)
			{
				List<ClientCard> ChkCards = (from card in this.GetZoneCards(CardLocation.MonsterZone, base.Bot)
					where card != null && card.IsFaceup() && card.HasSetcode(154)
					select card).ToList<ClientCard>();
				int targetid = -1;
				if (!base.Bot.HasInHand(83334932) && !this.activate_Motorbike)
				{
					targetid = 83334932;
				}
				else if (!base.Bot.HasInHand(82112494) && !base.Bot.HasInSpellZone(82112494, false, false) && !this.activate_Wakaushi)
				{
					targetid = 82112494;
				}
				else if (!base.Bot.HasInHand(90361010) && (!this.normal_summon || ChkCards.Count >= 1))
				{
					targetid = 90361010;
				}
				if (targetid > 0)
				{
					base.AI.SelectCard(targetid);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001E58 RID: 7768 RVA: 0x000B9838 File Offset: 0x000B7A38
		private bool SoulpiercerEquipFunction()
		{
			if (base.Card.Location != CardLocation.Hand)
			{
				return false;
			}
			int tributeId = -1;
			if (base.Bot.HasInMonstersZone(34496660, false, false, false))
			{
				tributeId = 34496660;
			}
			else if (base.Bot.HasInMonstersZone(82112494, false, false, false))
			{
				tributeId = 82112494;
			}
			base.AI.SelectCard(tributeId);
			return base.Bot.HasInMonstersZone(new int[] { 82112494, 34496660 }, false, false, false);
		}

		// Token: 0x06001E59 RID: 7769 RVA: 0x000B98C0 File Offset: 0x000B7AC0
		private bool SoulpeacemakerEquipFunction()
		{
			if (base.Card.Location != CardLocation.Hand)
			{
				return false;
			}
			int tributeId = -1;
			List<ClientCard> cards = (from card in base.Bot.GetMonstersInExtraZone()
				where card != null && card.Id == 33918636
				select card).ToList<ClientCard>();
			if (cards.Count<ClientCard>() > 0)
			{
				base.AI.SelectCard(cards);
			}
			else
			{
				if (base.Bot.HasInMonstersZone(33918636, false, false, false))
				{
					tributeId = 33918636;
				}
				else if (base.Bot.HasInMonstersZone(90361010, false, false, false))
				{
					tributeId = 90361010;
				}
				base.AI.SelectCard(tributeId);
			}
			return base.Bot.HasInMonstersZone(new int[] { 33918636, 90361010 }, false, false, false);
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x000B9994 File Offset: 0x000B7B94
		private bool SoulhornsEquipFunction()
		{
			if (base.Card.Location != CardLocation.Hand)
			{
				return false;
			}
			int tributeId = -1;
			if (base.Bot.HasInMonstersZone(64193046, false, false, false))
			{
				tributeId = 64193046;
			}
			else if (base.Bot.HasInMonstersZone(76471944, false, false, false))
			{
				tributeId = 76471944;
			}
			base.AI.SelectCard(tributeId);
			return base.Bot.HasInMonstersZone(new int[] { 64193046, 76471944 }, false, false, false);
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x000B9A1C File Offset: 0x000B7C1C
		private bool SoulpeacemakerFunction()
		{
			if (base.Card.Location == CardLocation.SpellZone)
			{
				int tributeId = -1;
				if (base.Bot.HasInMonstersZone(90361010, false, false, false))
				{
					tributeId = 82112494;
				}
				else if (base.Bot.HasInGraveyard(90361010) || !this.activate_Scales)
				{
					tributeId = 78391364;
				}
				else if (!base.Bot.HasInGraveyard(90361010) || this.activate_Scales)
				{
					tributeId = 90361010;
				}
				base.AI.SelectCard(tributeId);
				this.activate_Soulpeacemaker = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001E5C RID: 7772 RVA: 0x000B9AB0 File Offset: 0x000B7CB0
		private bool GeniusLinkFunction()
		{
			if (base.Bot.MonsterZone[4] != null && base.Bot.MonsterZone[4].Controller == 0 && !this.FinalCards(base.Bot.MonsterZone[4].Id) && base.Bot.MonsterZone[0] != null && base.Bot.MonsterZone[0].Controller == 0 && !this.FinalCards(base.Bot.MonsterZone[0].Id))
			{
				return false;
			}
			if ((from card in this.GetZoneCards(CardLocation.Hand, base.Bot)
				where card != null && card.HasSetcode(154) && card.Level > 1 && card.Level < 8
				select card).ToList<ClientCard>().Count<ClientCard>() < 2 && !base.Bot.HasInMonstersZone(90361010, false, false, false))
			{
				return false;
			}
			List<ClientCard> Rcards = (from card in this.GetZoneCards(CardLocation.Removed, base.Bot)
				where card != null && card.Id == 10604644
				select card).ToList<ClientCard>();
			if (base.Bot.HasInHand(10604644) || base.Bot.HasInGraveyard(10604644) || base.Bot.HasInSpellZone(10604644, false, false) || base.Bot.HasInMonstersZone(10604644, false, false, false) || Rcards.Count<ClientCard>() > 0)
			{
				return false;
			}
			bool linkchk = false;
			List<ClientCard> materials = new List<ClientCard>();
			if (base.Bot.MonsterZone[6] != null && base.Bot.MonsterZone[6].Controller == 0 && base.Bot.MonsterZone[6].Id != 33918636 && !this.FinalCards(base.Bot.MonsterZone[6].Id))
			{
				materials.Add(base.Bot.MonsterZone[6]);
				linkchk = true;
			}
			else if (base.Bot.MonsterZone[5] != null && base.Bot.MonsterZone[5].Controller == 0 && base.Bot.MonsterZone[5].Id != 33918636 && !this.FinalCards(base.Bot.MonsterZone[5].Id))
			{
				materials.Add(base.Bot.MonsterZone[5]);
				linkchk = true;
			}
			foreach (ClientCard card2 in (from card in base.Bot.GetMonstersInMainZone()
				where card != null && card.IsFaceup() && card.HasRace(CardRace.Machine)
				select card).ToList<ClientCard>())
			{
				if (card2 != null && !this.FinalCards(card2.Id))
				{
					materials.Add(card2);
				}
			}
			if (materials.Count <= 1)
			{
				return false;
			}
			base.AI.SelectMaterials(materials, 0);
			return (base.Bot.GetMonstersInExtraZone().Count == 0 || linkchk) && !this.p_summoned && !this.activate_Genius;
		}

		// Token: 0x06001E5D RID: 7773 RVA: 0x000B9DCC File Offset: 0x000B7FCC
		private bool GeniusFunction()
		{
			if (base.ActivateDescription == base.Util.GetStringId(22423493, 1))
			{
				base.AI.SelectCard(10604644);
				this.activate_Genius = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001E5E RID: 7774 RVA: 0x000B9E04 File Offset: 0x000B8004
		private bool ElfLinkFunction()
		{
			if (!base.Bot.HasInGraveyard(83334932))
			{
				return false;
			}
			List<ClientCard> materials = new List<ClientCard>();
			if (base.Bot.MonsterZone[6] != null && base.Bot.MonsterZone[6].Controller == 0 && !this.FinalCards(base.Bot.MonsterZone[6].Id))
			{
				materials.Add(base.Bot.MonsterZone[6]);
			}
			else if (base.Bot.MonsterZone[5] != null && base.Bot.MonsterZone[5].Controller == 0 && !this.FinalCards(base.Bot.MonsterZone[5].Id))
			{
				materials.Add(base.Bot.MonsterZone[5]);
			}
			List<ClientCard> TunrerCards = (from card in base.Bot.GetMonstersInMainZone()
				where card2 != null && card2.IsFaceup() && this.TurnerCards(card2.Id) && !this.FinalCards(card2.Id)
				select card).ToList<ClientCard>();
			List<ClientCard> UnTunrercards = (from card in base.Bot.GetMonstersInMainZone()
				where card2 != null && card2.IsFaceup() && !this.TurnerCards(card2.Id) && !this.FinalCards(card2.Id)
				select card).ToList<ClientCard>();
			if (UnTunrercards.Count == 0)
			{
				return false;
			}
			if (TunrerCards.Count >= UnTunrercards.Count && UnTunrercards.Count > 0)
			{
				using (List<ClientCard>.Enumerator enumerator = TunrerCards.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ClientCard card2 = enumerator.Current;
						if (card2 != null && materials.Count((ClientCard ccard) => ccard != null && ccard.Id == card2.Id) <= 0)
						{
							materials.Add(card2);
						}
					}
					goto IL_01E7;
				}
			}
			using (List<ClientCard>.Enumerator enumerator = UnTunrercards.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ClientCard card = enumerator.Current;
					if (card != null && materials.Count((ClientCard ccard) => ccard != null && ccard.Id == card.Id) <= 0)
					{
						materials.Add(card);
					}
				}
			}
			IL_01E7:
			if (materials.Count <= 1)
			{
				return false;
			}
			base.AI.SelectMaterials(materials, 0);
			this.summon_Elf = true;
			return true;
		}

		// Token: 0x06001E5F RID: 7775 RVA: 0x000BA034 File Offset: 0x000B8234
		private bool ElfFunction()
		{
			if (base.Duel.Player == 0)
			{
				this.activate_Elf = true;
				base.AI.SelectCard(83334932);
				return base.Bot.HasInGraveyard(83334932);
			}
			IEnumerable<ClientCard> zoneCards = this.GetZoneCards(CardLocation.MonsterZone, base.Enemy);
			List<ClientCard> cards2 = this.GetZoneCards(CardLocation.SpellZone, base.Enemy);
			if (zoneCards.Count<ClientCard>() > 0 || cards2.Count<ClientCard>() >= 3)
			{
				if (base.Bot.HasInExtra(38342335) && base.Bot.HasInGraveyard(65741786))
				{
					base.AI.SelectCard(65741786);
				}
				else
				{
					base.AI.SelectCard(83334932);
					base.AI.SelectPosition(CardPosition.FaceUpDefence);
				}
				this.activate_Elf = true;
				return base.Bot.HasInGraveyard(83334932) || base.Bot.HasInGraveyard(65741786);
			}
			return false;
		}

		// Token: 0x06001E60 RID: 7776 RVA: 0x000BA124 File Offset: 0x000B8324
		private bool RegulusFunction()
		{
			if (base.Card.Location == CardLocation.Hand)
			{
				int tributeId = -1;
				if (base.Bot.HasInGraveyard(90361010))
				{
					tributeId = 90361010;
				}
				else if (base.Bot.HasInGraveyard(83334932))
				{
					tributeId = 83334932;
				}
				base.AI.SelectCard(tributeId);
				this.activate_Genius = true;
				return true;
			}
			return base.Duel.LastChainPlayer == 1;
		}

		// Token: 0x06001E61 RID: 7777 RVA: 0x000BA19C File Offset: 0x000B839C
		private bool FleurFunction()
		{
			if (base.ActivateDescription != base.Util.GetStringId(84815190, 0))
			{
				return base.ActivateDescription == base.Util.GetStringId(84815190, 1) && base.Duel.LastChainPlayer == 1;
			}
			ClientCard card = base.Util.GetProblematicEnemyMonster(0, true);
			if (card != null)
			{
				base.AI.SelectCard(card);
				return true;
			}
			card = base.Util.GetBestEnemySpell(true);
			if (card != null)
			{
				base.AI.SelectCard(card);
				return true;
			}
			List<ClientCard> cards = this.GetZoneCards(CardLocation.Onfield, base.Enemy);
			cards = cards.Where((ClientCard tcard) => tcard != null && !tcard.IsShouldNotBeTarget()).ToList<ClientCard>();
			if (cards.Count <= 0)
			{
				return false;
			}
			base.AI.SelectCard(cards);
			return true;
		}

		// Token: 0x06001E62 RID: 7778 RVA: 0x000BA280 File Offset: 0x000B8480
		private bool IPLinkFunction()
		{
			List<ClientCard> materials = (from card in this.GetZoneCards(CardLocation.MonsterZone, base.Bot)
				where card != null && card.IsFaceup() && card.Id != 33918636 && (card.Id != 27381364 || (card.Id == 27381364 && !this.summon_Elf)) && !this.FinalCards(card.Id)
				select card).ToList<ClientCard>();
			if (materials.Count <= 1)
			{
				return false;
			}
			if (base.Bot.MonsterZone[6] != null && base.Bot.MonsterZone[6].Controller == 0 && base.Bot.MonsterZone[6].HasType(CardType.Link))
			{
				if (base.Bot.MonsterZone[2] != null && this.FinalCards(base.Bot.MonsterZone[2].Id) && base.Bot.MonsterZone[4] != null && this.FinalCards(base.Bot.MonsterZone[4].Id))
				{
					return false;
				}
			}
			else if (base.Bot.MonsterZone[5] != null && base.Bot.MonsterZone[5].Controller == 0 && base.Bot.MonsterZone[5].HasType(CardType.Link) && base.Bot.MonsterZone[2] != null && this.FinalCards(base.Bot.MonsterZone[2].Id) && base.Bot.MonsterZone[0] != null && this.FinalCards(base.Bot.MonsterZone[0].Id))
			{
				return false;
			}
			base.AI.SelectMaterials(materials, 0);
			return true;
		}

		// Token: 0x06001E63 RID: 7779 RVA: 0x000BA3FC File Offset: 0x000B85FC
		private bool PSYFunction()
		{
			this.activate_PSY = true;
			return true;
		}

		// Token: 0x06001E64 RID: 7780 RVA: 0x000BA408 File Offset: 0x000B8608
		private bool IPFunction()
		{
			if (base.Duel.LastChainPlayer == 0)
			{
				return false;
			}
			if (base.Bot.HasInExtra(38342335))
			{
				List<ClientCard> material = new List<ClientCard>();
				List<ClientCard> cards = (from card in this.GetZoneCards(CardLocation.MonsterZone, base.Bot)
					where card != null && card != base.Card && card.IsFaceup() && !this.FinalCards(card.Id) && card.Id != 65741786 && card.Id != 33918636
					select card).ToList<ClientCard>();
				List<ClientCard> Enemycards = this.GetZoneCards(CardLocation.MonsterZone, base.Enemy);
				if (this.activate_Sarutobi)
				{
					Enemycards = this.GetZoneCards(CardLocation.Onfield, base.Enemy);
				}
				if (base.Bot.Hand.Count != 0)
				{
					if (Enemycards.Count((ClientCard card) => card != null && !card.IsShouldNotBeTarget()) != 0 && cards.Count != 0)
					{
						bool linkchk = false;
						foreach (ClientCard card2 in cards)
						{
							if (card2 != null && (card2.Id != 27381364 || (card2.Id == 27381364 && !this.summon_Elf)))
							{
								material.Add(card2);
								linkchk = true;
								break;
							}
						}
						base.AI.SelectCard(38342335);
						material.Insert(0, base.Card);
						base.AI.SelectMaterials(material, 0);
						if (!this.to_deck)
						{
							this.to_deck = true;
						}
						return linkchk;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x000BA57C File Offset: 0x000B877C
		private bool SarutobiFunction()
		{
			List<ClientCard> Enemycards = this.GetZoneCards(CardLocation.SpellZone, base.Enemy);
			if (Enemycards.Count((ClientCard card) => card != null && !card.IsShouldNotBeTarget()) == 0 || this.to_deck)
			{
				return false;
			}
			base.AI.SelectCard(Enemycards);
			this.activate_Sarutobi = true;
			return true;
		}

		// Token: 0x0400211C RID: 8476
		private bool normal_summon;

		// Token: 0x0400211D RID: 8477
		private bool p_summoned;

		// Token: 0x0400211E RID: 8478
		private bool p_summoning;

		// Token: 0x0400211F RID: 8479
		private bool activate_Motorbike;

		// Token: 0x04002120 RID: 8480
		private bool activate_Wakaushi;

		// Token: 0x04002121 RID: 8481
		private bool activate_Scales;

		// Token: 0x04002122 RID: 8482
		private bool activate_Wagon;

		// Token: 0x04002123 RID: 8483
		private bool activate_Booster;

		// Token: 0x04002124 RID: 8484
		private bool activate_Soulpeacemaker;

		// Token: 0x04002125 RID: 8485
		private bool activate_Benkei;

		// Token: 0x04002126 RID: 8486
		private bool need_Gear;

		// Token: 0x04002127 RID: 8487
		private bool activate_Scarecrow;

		// Token: 0x04002128 RID: 8488
		private bool summon_Scarecrow;

		// Token: 0x04002129 RID: 8489
		private bool summon_Scarecrow2 = true;

		// Token: 0x0400212A RID: 8490
		private bool activate_Sarutobi;

		// Token: 0x0400212B RID: 8491
		private bool activate_Genius;

		// Token: 0x0400212C RID: 8492
		private bool activate_Elf;

		// Token: 0x0400212D RID: 8493
		private bool summon_Elf;

		// Token: 0x0400212E RID: 8494
		private bool activate_MaxxG;

		// Token: 0x0400212F RID: 8495
		private bool activate_PSY;

		// Token: 0x04002130 RID: 8496
		private bool activate_LockBird;

		// Token: 0x04002131 RID: 8497
		private bool to_deck;

		// Token: 0x020003D8 RID: 984
		public class CardId
		{
			// Token: 0x04002132 RID: 8498
			public const int Benkei = 19510093;

			// Token: 0x04002133 RID: 8499
			public const int Wagon = 34496660;

			// Token: 0x04002134 RID: 8500
			public const int Soulpiercer = 90361010;

			// Token: 0x04002135 RID: 8501
			public const int Wakaushi = 82112494;

			// Token: 0x04002136 RID: 8502
			public const int Scales = 78391364;

			// Token: 0x04002137 RID: 8503
			public const int Booster = 56727340;

			// Token: 0x04002138 RID: 8504
			public const int Motorbike = 83334932;

			// Token: 0x04002139 RID: 8505
			public const int Soulhorns = 14624296;

			// Token: 0x0400213A RID: 8506
			public const int Soulpeacemaker = 95500396;

			// Token: 0x0400213B RID: 8507
			public const int Regulus = 10604644;

			// Token: 0x0400213C RID: 8508
			public const int MaxxG = 23434538;

			// Token: 0x0400213D RID: 8509
			public const int JoyousSpring = 14558127;

			// Token: 0x0400213E RID: 8510
			public const int PsyFrameDriver = 49036338;

			// Token: 0x0400213F RID: 8511
			public const int PsyFramegearGamma = 38814750;

			// Token: 0x04002140 RID: 8512
			public const int EffectVeiler = 97268402;

			// Token: 0x04002141 RID: 8513
			public const int HauntedMansion = 73642296;

			// Token: 0x04002142 RID: 8514
			public const int SnowRabbit = 59438930;

			// Token: 0x04002143 RID: 8515
			public const int LockBird = 94145021;

			// Token: 0x04002144 RID: 8516
			public const int Masurawo = 64193046;

			// Token: 0x04002145 RID: 8517
			public const int Fleur = 84815190;

			// Token: 0x04002146 RID: 8518
			public const int ASStardustDragon = 30983281;

			// Token: 0x04002147 RID: 8519
			public const int StardustDragon = 30983281;

			// Token: 0x04002148 RID: 8520
			public const int SavageDragon = 27548199;

			// Token: 0x04002149 RID: 8521
			public const int Sarutobi = 76471944;

			// Token: 0x0400214A RID: 8522
			public const int PSYFramelordOmega = 74586817;

			// Token: 0x0400214B RID: 8523
			public const int GearGigant = 28912357;

			// Token: 0x0400214C RID: 8524
			public const int Unicorn = 38342335;

			// Token: 0x0400214D RID: 8525
			public const int Elf = 27381364;

			// Token: 0x0400214E RID: 8526
			public const int Genius = 22423493;

			// Token: 0x0400214F RID: 8527
			public const int IP = 65741786;

			// Token: 0x04002150 RID: 8528
			public const int Scarecrow = 33918636;
		}
	}
}
