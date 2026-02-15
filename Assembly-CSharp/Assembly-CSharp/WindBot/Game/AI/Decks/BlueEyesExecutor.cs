using System;
using System.Collections.Generic;
using System.Linq;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x020002BA RID: 698
	[Deck("Blue-Eyes", "AI_BlueEyes", "Normal")]
	internal class BlueEyesExecutor : DefaultExecutor
	{
		// Token: 0x060010C1 RID: 4289 RVA: 0x00052CF0 File Offset: 0x00050EF0
		public BlueEyesExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 18144506, new Func<bool>(base.DefaultHarpiesFeatherDusterFirst));
			base.AddExecutor(ExecutorType.Activate, 5133471, new Func<bool>(base.DefaultGalaxyCyclone));
			base.AddExecutor(ExecutorType.Activate, 18144506);
			base.AddExecutor(ExecutorType.Activate, 41620959, new Func<bool>(this.DragonShrineEffect));
			base.AddExecutor(ExecutorType.Summon, 8240199, new Func<bool>(this.SageWithEyesOfBlueSummon));
			base.AddExecutor(ExecutorType.Activate, 48800175, new Func<bool>(this.MelodyOfAwakeningDragonEffect));
			base.AddExecutor(ExecutorType.Activate, 39701395, new Func<bool>(this.CardsOfConsonanceEffect));
			base.AddExecutor(ExecutorType.Activate, 38120068, new Func<bool>(this.TradeInEffect));
			base.AddExecutor(ExecutorType.Activate, 35261759, new Func<bool>(base.DefaultPotOfDesires));
			base.AddExecutor(ExecutorType.SpSummon, 38517737, new Func<bool>(this.AlternativeWhiteDragonSummon));
			base.AddExecutor(ExecutorType.Activate, 6853254, new Func<bool>(this.RebornEffect));
			base.AddExecutor(ExecutorType.Activate, 87025064, new Func<bool>(this.RebornEffect));
			base.AddExecutor(ExecutorType.Activate, 83764718, new Func<bool>(this.RebornEffect));
			base.AddExecutor(ExecutorType.Activate, 38517737, new Func<bool>(this.AlternativeWhiteDragonEffect));
			base.AddExecutor(ExecutorType.Activate, 8240199, new Func<bool>(this.SageWithEyesOfBlueEffect));
			base.AddExecutor(ExecutorType.Activate, 71039903, new Func<bool>(this.WhiteStoneOfAncientsEffect));
			base.AddExecutor(ExecutorType.Activate, 45467446, new Func<bool>(this.DragonSpiritOfWhiteEffect));
			base.AddExecutor(ExecutorType.Activate, 59822133, new Func<bool>(this.BlueEyesSpiritDragonEffect));
			base.AddExecutor(ExecutorType.Activate, 63767246, new Func<bool>(this.HopeHarbingerDragonTitanicGalaxyEffect));
			base.AddExecutor(ExecutorType.Activate, 18963306, new Func<bool>(this.GalaxyEyesCipherDragonEffect));
			base.AddExecutor(ExecutorType.Activate, 31801517, new Func<bool>(this.GalaxyEyesPrimePhotonDragonEffect));
			base.AddExecutor(ExecutorType.Activate, 39030163, new Func<bool>(this.GalaxyEyesFullArmorPhotonDragonEffect));
			base.AddExecutor(ExecutorType.Activate, 2530830, new Func<bool>(this.GalaxyEyesCipherBladeDragonEffect));
			base.AddExecutor(ExecutorType.Activate, 58820923, new Func<bool>(this.GalaxyEyesDarkMatterDragonEffect));
			base.AddExecutor(ExecutorType.Activate, 40908371, new Func<bool>(this.AzureEyesSilverDragonEffect));
			base.AddExecutor(ExecutorType.Activate, 33909817, new Func<bool>(this.SylvanPrincesspriteEffect));
			base.AddExecutor(ExecutorType.Summon, 8240199, new Func<bool>(this.WhiteStoneSummon));
			base.AddExecutor(ExecutorType.Summon, 71039903, new Func<bool>(this.WhiteStoneSummon));
			base.AddExecutor(ExecutorType.Summon, 79814787, new Func<bool>(this.WhiteStoneSummon));
			base.AddExecutor(ExecutorType.SpSummon, 18963306, new Func<bool>(this.GalaxyEyesCipherDragonSummon));
			base.AddExecutor(ExecutorType.SpSummon, 31801517, new Func<bool>(this.GalaxyEyesPrimePhotonDragonSummon));
			base.AddExecutor(ExecutorType.SpSummon, 39030163, new Func<bool>(this.GalaxyEyesFullArmorPhotonDragonSummon));
			base.AddExecutor(ExecutorType.SpSummon, 2530830, new Func<bool>(this.GalaxyEyesCipherBladeDragonSummon));
			base.AddExecutor(ExecutorType.SpSummon, 58820923, new Func<bool>(this.GalaxyEyesDarkMatterDragonSummon));
			base.AddExecutor(ExecutorType.SpSummon, 63422098, new Func<bool>(this.GiganticastleSummon));
			base.AddExecutor(ExecutorType.SpSummon, 59822133, new Func<bool>(this.BlueEyesSpiritDragonSummon));
			base.AddExecutor(ExecutorType.SpSummon, 63767246, new Func<bool>(this.HopeHarbingerDragonTitanicGalaxySummon));
			base.AddExecutor(ExecutorType.SpSummon, 33909817, new Func<bool>(this.SylvanPrincesspriteSummon));
			base.AddExecutor(ExecutorType.Activate, 54447022, new Func<bool>(this.SoulChargeEffect));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(this.Repos));
			base.AddExecutor(ExecutorType.Summon, 79814787, new Func<bool>(this.WhiteStoneSummonForSage));
			base.AddExecutor(ExecutorType.Summon, 71039903, new Func<bool>(this.WhiteStoneSummonForSage));
			base.AddExecutor(ExecutorType.Summon, 8240199, new Func<bool>(this.WhiteStoneSummonForSage));
			base.AddExecutor(ExecutorType.Activate, 8240199, new Func<bool>(this.SageWithEyesOfBlueEffectInHand));
			base.AddExecutor(ExecutorType.MonsterSet, 79814787);
			base.AddExecutor(ExecutorType.MonsterSet, 71039903);
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(this.SpellSet));
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x0005314A File Offset: 0x0005134A
		public override void OnNewTurn()
		{
			this.UsedAlternativeWhiteDragon.Clear();
			this.UsedGalaxyEyesCipherDragon = null;
			this.AlternativeWhiteDragonSummoned = false;
			this.SoulChargeUsed = false;
			base.OnNewTurn();
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x00053174 File Offset: 0x00051374
		public override IList<ClientCard> OnSelectCard(IList<ClientCard> cards, int min, int max, int hint, bool cancelable)
		{
			Logger.DebugWriteLine(string.Concat(new string[]
			{
				"OnSelectCard ",
				cards.Count.ToString(),
				" ",
				min.ToString(),
				" ",
				max.ToString()
			}));
			if (max == 2 && cards[0].Location == CardLocation.Deck)
			{
				Logger.DebugWriteLine("OnSelectCard MelodyOfAwakeningDragon");
				List<ClientCard> result = new List<ClientCard>();
				if (!base.Bot.HasInHand(89631139))
				{
					result.AddRange(cards.Where((ClientCard card) => card.IsCode(89631139)).Take(1));
				}
				result.AddRange(cards.Where((ClientCard card) => card.IsCode(38517737)));
				return base.Util.CheckSelectCount(result, cards, min, max);
			}
			Logger.DebugWriteLine("Use default.");
			return base.OnSelectCard(cards, min, max, hint, cancelable);
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x0005328C File Offset: 0x0005148C
		public override IList<ClientCard> OnSelectXyzMaterial(IList<ClientCard> cards, int min, int max)
		{
			Logger.DebugWriteLine(string.Concat(new string[]
			{
				"OnSelectXyzMaterial ",
				cards.Count.ToString(),
				" ",
				min.ToString(),
				" ",
				max.ToString()
			}));
			IList<ClientCard> result = base.Util.SelectPreferredCards(this.UsedAlternativeWhiteDragon, cards, min, max);
			return base.Util.CheckSelectCount(result, cards, min, max);
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x0005330C File Offset: 0x0005150C
		public override IList<ClientCard> OnSelectSynchroMaterial(IList<ClientCard> cards, int sum, int min, int max)
		{
			Logger.DebugWriteLine(string.Concat(new string[]
			{
				"OnSelectSynchroMaterial ",
				cards.Count.ToString(),
				" ",
				sum.ToString(),
				" ",
				min.ToString(),
				" ",
				max.ToString()
			}));
			if (sum != 8)
			{
				return null;
			}
			foreach (ClientCard AlternativeWhiteDragon in this.UsedAlternativeWhiteDragon)
			{
				if (cards.IndexOf(AlternativeWhiteDragon) > 0)
				{
					this.UsedAlternativeWhiteDragon.Remove(AlternativeWhiteDragon);
					Logger.DebugWriteLine("select UsedAlternativeWhiteDragon");
					return new ClientCard[] { AlternativeWhiteDragon };
				}
			}
			return null;
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x000533F0 File Offset: 0x000515F0
		public override void OnSpSummoned()
		{
			if (base.Duel.GetCurrentSolvingChainCard() == null)
			{
				foreach (ClientCard card in base.Duel.LastSummonedCards)
				{
					if (card.Controller == 0 && card.IsCode(38517737))
					{
						this.AlternativeWhiteDragonSummoned = true;
					}
				}
			}
			base.OnSpSummoned();
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x0005346C File Offset: 0x0005166C
		private bool DragonShrineEffect()
		{
			base.AI.SelectCard(new int[] { 45467446, 89631139, 71039903, 79814787 });
			if (!base.Bot.HasInHand(89631139))
			{
				base.AI.SelectNextCard(79814787);
			}
			else
			{
				base.AI.SelectNextCard(new int[] { 71039903, 45467446, 79814787 });
			}
			return true;
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x000534D6 File Offset: 0x000516D6
		private bool MelodyOfAwakeningDragonEffect()
		{
			base.AI.SelectCard(new int[] { 71039903, 45467446, 79814787, 5133471, 97268402, 38120068, 8240199 });
			return true;
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x000534F8 File Offset: 0x000516F8
		private bool CardsOfConsonanceEffect()
		{
			if (!base.Bot.HasInHand(89631139))
			{
				base.AI.SelectCard(79814787);
			}
			else if (base.Bot.HasInHand(38120068))
			{
				base.AI.SelectCard(79814787);
			}
			else
			{
				base.AI.SelectCard(71039903);
			}
			return true;
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x00053560 File Offset: 0x00051760
		private bool TradeInEffect()
		{
			if (base.Bot.HasInHand(45467446))
			{
				base.AI.SelectCard(45467446);
				return true;
			}
			if (this.HasTwoInHand(89631139))
			{
				base.AI.SelectCard(89631139);
				return true;
			}
			if (this.HasTwoInHand(38517737))
			{
				base.AI.SelectCard(38517737);
				return true;
			}
			if (!base.Bot.HasInHand(89631139) || !base.Bot.HasInHand(38517737))
			{
				base.AI.SelectCard(new int[] { 89631139, 38517737 });
				return true;
			}
			return false;
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x00053618 File Offset: 0x00051818
		private bool AlternativeWhiteDragonEffect()
		{
			ClientCard target = base.Util.GetProblematicEnemyMonster(base.Card.GetDefensePower(), false);
			if (target != null)
			{
				base.AI.SelectCard(target);
				this.UsedAlternativeWhiteDragon.Add(base.Card);
				return true;
			}
			if (base.Util.GetBotAvailZonesFromExtraDeck(base.Card) > 0 && (base.Bot.HasInMonstersZone(new int[] { 8240199, 71039903, 79814787, 89631139, 45467446 }, false, false, false) || base.Bot.GetCountCardInZone(base.Bot.MonsterZone, 38517737) >= 2))
			{
				target = base.Util.GetBestEnemyMonster(false, true);
				base.AI.SelectCard(target);
				this.UsedAlternativeWhiteDragon.Add(base.Card);
				return true;
			}
			return false;
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x000536E0 File Offset: 0x000518E0
		private bool RebornEffect()
		{
			if (base.Duel.Player == 0 && base.Duel.CurrentChain.Count > 0)
			{
				return false;
			}
			if (base.Duel.Player == 0 && (base.Duel.Phase == DuelPhase.Draw || base.Duel.Phase == DuelPhase.Standby))
			{
				return false;
			}
			IList<int> targets = new int[] { 63767246, 58820923, 38517737, 40908371, 59822133, 89631139, 45467446 };
			if (!base.Bot.HasInGraveyard(targets))
			{
				return false;
			}
			if (base.Enemy.SpellZone.GetFloodgate(false) != null && base.Bot.HasInGraveyard(45467446))
			{
				base.AI.SelectCard(45467446);
			}
			else
			{
				base.AI.SelectCard(targets);
			}
			return true;
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x000537A0 File Offset: 0x000519A0
		private bool AzureEyesSilverDragonEffect()
		{
			if (base.Enemy.GetSpellCount() > 0)
			{
				base.AI.SelectCard(45467446);
			}
			else
			{
				base.AI.SelectCard(89631139);
			}
			return true;
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x000537D3 File Offset: 0x000519D3
		private bool SageWithEyesOfBlueSummon()
		{
			return !base.Bot.HasInHand(new int[] { 71039903, 79814787 });
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x000537F9 File Offset: 0x000519F9
		private bool SageWithEyesOfBlueEffect()
		{
			if (base.Card.Location == CardLocation.Hand)
			{
				return false;
			}
			base.AI.SelectCard(new int[] { 71039903, 97268402, 79814787 });
			return true;
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x00053828 File Offset: 0x00051A28
		private bool WhiteStoneSummonForSage()
		{
			return base.Bot.HasInHand(8240199);
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x0005383C File Offset: 0x00051A3C
		private bool SageWithEyesOfBlueEffectInHand()
		{
			if (base.Card.Location != CardLocation.Hand)
			{
				return false;
			}
			if (!base.Bot.HasInMonstersZone(new int[] { 79814787, 71039903 }, false, false, false) || base.Bot.HasInMonstersZone(new int[] { 38517737, 89631139, 45467446 }, false, false, false))
			{
				return false;
			}
			base.AI.SelectCard(new int[] { 79814787, 71039903 });
			if (base.Enemy.GetSpellCount() > 0)
			{
				base.AI.SelectNextCard(45467446);
			}
			else
			{
				base.AI.SelectNextCard(89631139);
			}
			return true;
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x000538F4 File Offset: 0x00051AF4
		private bool DragonSpiritOfWhiteEffect()
		{
			if (base.ActivateDescription == -1)
			{
				ClientCard target = base.Util.GetBestEnemySpell(false);
				base.AI.SelectCard(target);
				return true;
			}
			if (this.HaveEnoughWhiteDragonInHand())
			{
				if (base.Duel.Player == 0 && base.Duel.Phase == DuelPhase.BattleStart)
				{
					return base.Card.Attacked;
				}
				if (base.Duel.Player == 1 && base.Duel.Phase == DuelPhase.End)
				{
					return base.Bot.HasInMonstersZone(40908371, true, false, false) && !base.Bot.HasInGraveyard(45467446) && !base.Bot.HasInGraveyard(89631139);
				}
				if (base.Util.IsChainTarget(base.Card))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x000539CC File Offset: 0x00051BCC
		private bool BlueEyesSpiritDragonEffect()
		{
			if (base.ActivateDescription == -1 || base.ActivateDescription == base.Util.GetStringId(59822133, 0))
			{
				return base.Duel.LastChainPlayer == 1;
			}
			if (base.Duel.Player == 1 && (base.Duel.Phase == DuelPhase.BattleStart || base.Duel.Phase == DuelPhase.End))
			{
				base.AI.SelectCard(40908371);
				return true;
			}
			if (base.Util.IsChainTarget(base.Card))
			{
				base.AI.SelectCard(40908371);
				return true;
			}
			return false;
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x00053A70 File Offset: 0x00051C70
		private bool HopeHarbingerDragonTitanicGalaxyEffect()
		{
			return (base.ActivateDescription != -1 && base.ActivateDescription != base.Util.GetStringId(63767246, 0)) || base.Duel.LastChainPlayer == 1;
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x00053AA4 File Offset: 0x00051CA4
		private bool WhiteStoneOfAncientsEffect()
		{
			if (base.ActivateDescription != base.Util.GetStringId(71039903, 0))
			{
				if (base.Enemy.GetSpellCount() > 0)
				{
					base.AI.SelectCard(45467446);
				}
				else
				{
					base.AI.SelectCard(89631139);
				}
				return true;
			}
			if (base.Bot.HasInHand(38120068) && !base.Bot.HasInHand(89631139) && !base.Bot.HasInHand(38517737))
			{
				base.AI.SelectCard(89631139);
				return true;
			}
			if (this.AlternativeWhiteDragonSummoned)
			{
				return false;
			}
			if (base.Bot.HasInHand(89631139) && !base.Bot.HasInHand(38517737) && base.Bot.HasInGraveyard(38517737))
			{
				base.AI.SelectCard(38517737);
				return true;
			}
			if (base.Bot.HasInHand(38517737) && !base.Bot.HasInHand(89631139) && base.Bot.HasInGraveyard(89631139))
			{
				base.AI.SelectCard(89631139);
				return true;
			}
			return false;
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x0000763C File Offset: 0x0000583C
		private bool AlternativeWhiteDragonSummon()
		{
			return true;
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x00053BE2 File Offset: 0x00051DE2
		private bool WhiteStoneSummon()
		{
			return base.Bot.HasInMonstersZone(new int[] { 8240199, 71039903, 79814787, 38517737, 89631139, 45467446 }, false, false, false);
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x00053C04 File Offset: 0x00051E04
		private bool GalaxyEyesCipherDragonSummon()
		{
			if (base.Duel.Turn == 1 || this.SoulChargeUsed)
			{
				return false;
			}
			List<ClientCard> monsters = base.Enemy.GetMonsters();
			if (monsters.Count == 1 && !monsters[0].IsFacedown() && monsters[0].IsDefense() && monsters[0].GetDefensePower() >= 3000 && monsters[0].HasType(CardType.Xyz))
			{
				return true;
			}
			if (monsters.Count >= 3)
			{
				foreach (ClientCard monster in monsters)
				{
					if (!monster.IsFacedown() && ((monster.IsDefense() && monster.GetDefensePower() >= 3000) || monster.HasType(CardType.Xyz)))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x00053CF8 File Offset: 0x00051EF8
		private bool GalaxyEyesPrimePhotonDragonSummon()
		{
			return base.Duel.Turn != 1 && base.Util.IsOneEnemyBetterThanValue(2999, false);
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x00053D20 File Offset: 0x00051F20
		private bool GalaxyEyesFullArmorPhotonDragonSummon()
		{
			if (base.Bot.HasInMonstersZone(18963306, false, false, false))
			{
				foreach (ClientCard monster in base.Bot.GetMonsters())
				{
					if ((monster.IsDisabled() && monster.HasType(CardType.Xyz) && !monster.Equals(this.UsedGalaxyEyesCipherDragon)) || (base.Duel.Phase == DuelPhase.Main2 && monster.Equals(this.UsedGalaxyEyesCipherDragon)))
					{
						base.AI.SelectCard(monster);
						return true;
					}
				}
			}
			if (base.Bot.HasInMonstersZone(31801517, false, false, false) && !base.Util.IsOneEnemyBetterThanValue(4000, false))
			{
				base.AI.SelectCard(31801517);
				return true;
			}
			return false;
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x00053E1C File Offset: 0x0005201C
		private bool GalaxyEyesCipherBladeDragonSummon()
		{
			if (base.Bot.HasInMonstersZone(39030163, false, false, false) && base.Util.GetProblematicEnemyCard(0, false) != null)
			{
				base.AI.SelectCard(39030163);
				return true;
			}
			return false;
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x00053E55 File Offset: 0x00052055
		private bool GalaxyEyesDarkMatterDragonSummon()
		{
			if (base.Bot.HasInMonstersZone(39030163, false, false, false))
			{
				base.AI.SelectCard(39030163);
				return true;
			}
			return false;
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x0000763C File Offset: 0x0000583C
		private bool GalaxyEyesPrimePhotonDragonEffect()
		{
			return true;
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x00053E80 File Offset: 0x00052080
		private bool GalaxyEyesCipherDragonEffect()
		{
			List<ClientCard> monsters = base.Enemy.GetMonsters();
			foreach (ClientCard monster in monsters)
			{
				if (monster.HasType(CardType.Xyz))
				{
					base.AI.SelectCard(monster);
					this.UsedGalaxyEyesCipherDragon = base.Card;
					return true;
				}
			}
			foreach (ClientCard monster2 in monsters)
			{
				if (monster2.IsDefense())
				{
					base.AI.SelectCard(monster2);
					this.UsedGalaxyEyesCipherDragon = base.Card;
					return true;
				}
			}
			this.UsedGalaxyEyesCipherDragon = base.Card;
			return true;
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x00053F68 File Offset: 0x00052168
		private bool GalaxyEyesFullArmorPhotonDragonEffect()
		{
			ClientCard target = base.Util.GetProblematicEnemySpell();
			if (target != null)
			{
				base.AI.SelectCard(target);
				return true;
			}
			target = base.Util.GetProblematicEnemyMonster(0, false);
			if (target != null)
			{
				base.AI.SelectCard(target);
				return true;
			}
			foreach (ClientCard spell in base.Enemy.GetSpells())
			{
				if (spell.IsFaceup())
				{
					base.AI.SelectCard(spell);
					return true;
				}
			}
			List<ClientCard> monsters = base.Enemy.GetMonsters();
			if (monsters.Count >= 2)
			{
				foreach (ClientCard monster in monsters)
				{
					if (monster.IsDefense())
					{
						base.AI.SelectCard(monster);
						return true;
					}
				}
				return true;
			}
			if (monsters.Count == 2)
			{
				foreach (ClientCard monster2 in monsters)
				{
					if (monster2.IsMonsterInvincible() || monster2.IsMonsterDangerous() || monster2.GetDefensePower() > 4000)
					{
						base.AI.SelectCard(monster2);
						return true;
					}
				}
			}
			return monsters.Count == 1;
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x00054100 File Offset: 0x00052300
		private bool GalaxyEyesCipherBladeDragonEffect()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				return true;
			}
			ClientCard target = base.Util.GetProblematicEnemyCard(0, false);
			if (target != null)
			{
				base.AI.SelectCard(target);
				return true;
			}
			List<ClientCard> monsters = base.Enemy.GetMonsters();
			foreach (ClientCard monster in monsters)
			{
				if (monster.IsDefense())
				{
					base.AI.SelectCard(monster);
					return true;
				}
			}
			using (List<ClientCard>.Enumerator enumerator = monsters.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					ClientCard monster2 = enumerator.Current;
					base.AI.SelectCard(monster2);
					return true;
				}
			}
			List<ClientCard> spells = base.Enemy.GetSpells();
			foreach (ClientCard spell in spells)
			{
				if (spell.IsFacedown())
				{
					base.AI.SelectCard(spell);
					return true;
				}
			}
			using (List<ClientCard>.Enumerator enumerator = spells.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					ClientCard spell2 = enumerator.Current;
					base.AI.SelectCard(spell2);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x0005429C File Offset: 0x0005249C
		private bool GalaxyEyesDarkMatterDragonEffect()
		{
			base.AI.SelectCard(new int[] { 71039903, 79814787, 45467446, 89631139 });
			base.AI.SelectNextCard(new int[] { 71039903, 79814787, 45467446, 89631139 });
			return true;
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x000542D8 File Offset: 0x000524D8
		private bool GiganticastleSummon()
		{
			if (base.Duel.Phase != DuelPhase.Main1 || base.Duel.Turn == 1 || this.SoulChargeUsed)
			{
				return false;
			}
			int bestAttack = base.Util.GetBestAttack(base.Bot);
			int bestEnemyAttack = base.Util.GetBestPower(base.Enemy, false);
			return bestAttack <= bestEnemyAttack && bestEnemyAttack > 2500 && bestEnemyAttack <= 3100;
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x00054348 File Offset: 0x00052548
		private bool BlueEyesSpiritDragonSummon()
		{
			if (base.Duel.Phase == DuelPhase.Main1)
			{
				if (this.UsedAlternativeWhiteDragon.Count > 0)
				{
					return true;
				}
				if (base.Duel.Turn == 1 || this.SoulChargeUsed)
				{
					base.AI.SelectPosition(CardPosition.FaceUpDefence);
					return true;
				}
			}
			if (base.Duel.Phase == DuelPhase.Main2)
			{
				base.AI.SelectPosition(CardPosition.FaceUpDefence);
				return true;
			}
			return false;
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x000543B8 File Offset: 0x000525B8
		private bool HopeHarbingerDragonTitanicGalaxySummon()
		{
			if (base.Duel.Phase == DuelPhase.Main1)
			{
				if (this.UsedAlternativeWhiteDragon.Count > 0)
				{
					return true;
				}
				if (base.Duel.Turn == 1 || this.SoulChargeUsed)
				{
					return true;
				}
			}
			return base.Duel.Phase == DuelPhase.Main2;
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x00054410 File Offset: 0x00052610
		private bool SylvanPrincesspriteSummon()
		{
			return base.Duel.Turn == 1 || (base.Duel.Phase == DuelPhase.Main1 && !base.Bot.HasInMonstersZone(new int[] { 38517737, 89631139, 45467446 }, false, false, false)) || (base.Duel.Phase == DuelPhase.Main2 || this.SoulChargeUsed);
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x0005447B File Offset: 0x0005267B
		private bool SylvanPrincesspriteEffect()
		{
			base.AI.SelectCard(new int[] { 79814787, 71039903 });
			return true;
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x000544A0 File Offset: 0x000526A0
		private bool SoulChargeEffect()
		{
			if (base.Bot.HasInMonstersZone(59822133, true, false, false))
			{
				return false;
			}
			int count = base.Bot.GetGraveyardMonsters().Count;
			int space = 5 - base.Bot.GetMonstersInMainZone().Count;
			if (count < space)
			{
				count = space;
			}
			if (count < 2 || base.Bot.LifePoints < count * 1000)
			{
				return false;
			}
			if (base.Duel.Turn != 1)
			{
				int attack = 0;
				int defence = 0;
				foreach (ClientCard monster in base.Bot.GetMonsters())
				{
					if (!monster.IsDefense())
					{
						attack += monster.Attack;
					}
				}
				foreach (ClientCard monster2 in base.Enemy.GetMonsters())
				{
					defence += monster2.GetDefensePower();
				}
				if (attack - defence > base.Enemy.LifePoints)
				{
					return false;
				}
			}
			base.AI.SelectCard(new int[] { 59822133, 63767246, 38517737, 89631139, 45467446, 40908371, 71039903, 79814787 });
			this.SoulChargeUsed = true;
			return true;
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x000545F8 File Offset: 0x000527F8
		private bool Repos()
		{
			bool enemyBetter = base.Util.IsAllEnemyBetter(true);
			return (base.Card.IsAttack() && enemyBetter) || base.Card.IsFacedown() || (base.Card.IsDefense() && !enemyBetter && base.Card.Attack >= base.Card.Defense) || (base.Card.IsDefense() && base.Card.IsCode(new int[] { 59822133, 40908371 })) || (base.Card.IsAttack() && base.Card.IsCode(new int[] { 8240199, 71039903, 79814787 }));
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x000546BC File Offset: 0x000528BC
		private bool SpellSet()
		{
			return (base.Card.IsTrap() || base.Card.IsCode(87025064)) && base.Bot.GetSpellCountWithoutField() < 4;
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x000546F0 File Offset: 0x000528F0
		private bool HasTwoInHand(int id)
		{
			int num = 0;
			foreach (ClientCard card in base.Bot.Hand)
			{
				if (card != null && card.IsCode(id))
				{
					num++;
				}
			}
			return num >= 2;
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x00054754 File Offset: 0x00052954
		private bool HaveEnoughWhiteDragonInHand()
		{
			return this.HasTwoInHand(89631139) || (base.Bot.HasInGraveyard(89631139) && base.Bot.HasInGraveyard(71039903));
		}

		// Token: 0x0400157D RID: 5501
		private List<ClientCard> UsedAlternativeWhiteDragon = new List<ClientCard>();

		// Token: 0x0400157E RID: 5502
		private ClientCard UsedGalaxyEyesCipherDragon;

		// Token: 0x0400157F RID: 5503
		private bool AlternativeWhiteDragonSummoned;

		// Token: 0x04001580 RID: 5504
		private bool SoulChargeUsed;

		// Token: 0x020002BB RID: 699
		public class CardId
		{
			// Token: 0x04001581 RID: 5505
			public const int WhiteDragon = 89631139;

			// Token: 0x04001582 RID: 5506
			public const int AlternativeWhiteDragon = 38517737;

			// Token: 0x04001583 RID: 5507
			public const int DragonSpiritOfWhite = 45467446;

			// Token: 0x04001584 RID: 5508
			public const int WhiteStoneOfAncients = 71039903;

			// Token: 0x04001585 RID: 5509
			public const int WhiteStoneOfLegend = 79814787;

			// Token: 0x04001586 RID: 5510
			public const int SageWithEyesOfBlue = 8240199;

			// Token: 0x04001587 RID: 5511
			public const int EffectVeiler = 97268402;

			// Token: 0x04001588 RID: 5512
			public const int GalaxyCyclone = 5133471;

			// Token: 0x04001589 RID: 5513
			public const int HarpiesFeatherDuster = 18144506;

			// Token: 0x0400158A RID: 5514
			public const int ReturnOfTheDragonLords = 6853254;

			// Token: 0x0400158B RID: 5515
			public const int PotOfDesires = 35261759;

			// Token: 0x0400158C RID: 5516
			public const int TradeIn = 38120068;

			// Token: 0x0400158D RID: 5517
			public const int CardsOfConsonance = 39701395;

			// Token: 0x0400158E RID: 5518
			public const int DragonShrine = 41620959;

			// Token: 0x0400158F RID: 5519
			public const int MelodyOfAwakeningDragon = 48800175;

			// Token: 0x04001590 RID: 5520
			public const int SoulCharge = 54447022;

			// Token: 0x04001591 RID: 5521
			public const int MonsterReborn = 83764718;

			// Token: 0x04001592 RID: 5522
			public const int SilversCry = 87025064;

			// Token: 0x04001593 RID: 5523
			public const int Giganticastle = 63422098;

			// Token: 0x04001594 RID: 5524
			public const int AzureEyesSilverDragon = 40908371;

			// Token: 0x04001595 RID: 5525
			public const int BlueEyesSpiritDragon = 59822133;

			// Token: 0x04001596 RID: 5526
			public const int GalaxyEyesDarkMatterDragon = 58820923;

			// Token: 0x04001597 RID: 5527
			public const int GalaxyEyesCipherBladeDragon = 2530830;

			// Token: 0x04001598 RID: 5528
			public const int GalaxyEyesFullArmorPhotonDragon = 39030163;

			// Token: 0x04001599 RID: 5529
			public const int GalaxyEyesPrimePhotonDragon = 31801517;

			// Token: 0x0400159A RID: 5530
			public const int GalaxyEyesCipherDragon = 18963306;

			// Token: 0x0400159B RID: 5531
			public const int HopeHarbingerDragonTitanicGalaxy = 63767246;

			// Token: 0x0400159C RID: 5532
			public const int SylvanPrincessprite = 33909817;
		}
	}
}
