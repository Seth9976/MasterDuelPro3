using System;
using System.Collections.Generic;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x0200039B RID: 923
	[Deck("Rainbow", "AI_Rainbow", "Normal")]
	internal class RainbowExecutor : DefaultExecutor
	{
		// Token: 0x06001B89 RID: 7049 RVA: 0x000A493C File Offset: 0x000A2B3C
		public RainbowExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 18144506);
			base.AddExecutor(ExecutorType.Activate, 911883, new Func<bool>(this.UnexpectedDaiEffect));
			base.AddExecutor(ExecutorType.Summon, 85138716, new Func<bool>(this.RescueRabbitSummon));
			base.AddExecutor(ExecutorType.Activate, 85138716, new Func<bool>(this.RescueRabbitEffect));
			base.AddExecutor(ExecutorType.Activate, 35261759, new Func<bool>(base.DefaultPotOfDesires));
			base.AddExecutor(ExecutorType.Summon, 87979586, new Func<bool>(this.AngelTrumpeterSummon));
			base.AddExecutor(ExecutorType.Summon, 81823360, new Func<bool>(this.MegalosmasherXSummon));
			base.AddExecutor(ExecutorType.Summon, 75195825, new Func<bool>(this.MasterPendulumTheDracoslayerSummon));
			base.AddExecutor(ExecutorType.Summon, 18108166, new Func<bool>(this.MysteryShellDragonSummon));
			base.AddExecutor(ExecutorType.Summon, 74852097, new Func<bool>(this.PhantomGryphonSummon));
			base.AddExecutor(ExecutorType.Summon, 33256280, new Func<bool>(this.MetalfoesGoldriverSummon));
			base.AddExecutor(ExecutorType.Summon, new Func<bool>(this.NormalSummon));
			base.AddExecutor(ExecutorType.SpSummon, 18239909, new Func<bool>(this.IgnisterProminenceTheBlastingDracoslayerSummon));
			base.AddExecutor(ExecutorType.Activate, 18239909, new Func<bool>(this.IgnisterProminenceTheBlastingDracoslayerEffect));
			base.AddExecutor(ExecutorType.SpSummon, 12014404, new Func<bool>(this.GagagaCowboySummon));
			base.AddExecutor(ExecutorType.Activate, 12014404);
			base.AddExecutor(ExecutorType.SpSummon, 46772449, new Func<bool>(base.DefaultEvilswarmExcitonKnightSummon));
			base.AddExecutor(ExecutorType.Activate, 46772449, new Func<bool>(base.DefaultEvilswarmExcitonKnightEffect));
			base.AddExecutor(ExecutorType.SpSummon, 74294676, new Func<bool>(this.EvolzarLaggiaSummon));
			base.AddExecutor(ExecutorType.Activate, 74294676, new Func<bool>(this.EvolzarLaggiaEffect));
			base.AddExecutor(ExecutorType.SpSummon, 359563, new Func<bool>(this.EvilswarmNightmareSummon));
			base.AddExecutor(ExecutorType.Activate, 359563);
			base.AddExecutor(ExecutorType.SpSummon, 61344030, new Func<bool>(this.StarliegePaladynamoSummon));
			base.AddExecutor(ExecutorType.Activate, 61344030, new Func<bool>(this.StarliegePaladynamoEffect));
			base.AddExecutor(ExecutorType.SpSummon, 22653490, new Func<bool>(this.LightningChidoriSummon));
			base.AddExecutor(ExecutorType.Activate, 22653490, new Func<bool>(this.LightningChidoriEffect));
			base.AddExecutor(ExecutorType.SpSummon, 37279508, new Func<bool>(this.Number37HopeWovenDragonSpiderSharkSummon));
			base.AddExecutor(ExecutorType.Activate, 37279508);
			base.AddExecutor(ExecutorType.SpSummon, 6511113, new Func<bool>(this.TraptrixRafflesiaSummon));
			base.AddExecutor(ExecutorType.Activate, 6511113);
			base.AddExecutor(ExecutorType.Activate, 97169186, new Func<bool>(base.DefaultSmashingGround));
			base.AddExecutor(ExecutorType.SpSummon, 82633039, new Func<bool>(base.DefaultCastelTheSkyblasterMusketeerSummon));
			base.AddExecutor(ExecutorType.Activate, 82633039, new Func<bool>(base.DefaultCastelTheSkyblasterMusketeerEffect));
			base.AddExecutor(ExecutorType.SpSummon, 18239909, new Func<bool>(this.IgnisterProminenceTheBlastingDracoslayerSummon));
			base.AddExecutor(ExecutorType.Activate, 18239909, new Func<bool>(this.IgnisterProminenceTheBlastingDracoslayerEffect));
			base.AddExecutor(ExecutorType.SpSummon, 80666118, new Func<bool>(base.DefaultScarlightRedDragonArchfiendSummon));
			base.AddExecutor(ExecutorType.Activate, 80666118, new Func<bool>(base.DefaultScarlightRedDragonArchfiendEffect));
			base.AddExecutor(ExecutorType.SpSummon, 84013237, new Func<bool>(base.DefaultNumberS39UtopiaTheLightningSummon));
			base.AddExecutor(ExecutorType.SpSummon, 56832966);
			base.AddExecutor(ExecutorType.Activate, 56832966, new Func<bool>(base.DefaultNumberS39UtopiaTheLightningEffect));
			base.AddExecutor(ExecutorType.SpSummon, 44508094, new Func<bool>(base.DefaultStardustDragonSummon));
			base.AddExecutor(ExecutorType.Activate, 44508094, new Func<bool>(base.DefaultStardustDragonEffect));
			base.AddExecutor(ExecutorType.SpSummon, 82697249, new Func<bool>(this.Number59CrookedCookSummon));
			base.AddExecutor(ExecutorType.Activate, 82697249, new Func<bool>(this.Number59CrookedCookEffect));
			base.AddExecutor(ExecutorType.SpellSet, 58120309, new Func<bool>(this.TrapSet));
			base.AddExecutor(ExecutorType.SpellSet, 40838625, new Func<bool>(this.TrapSet));
			base.AddExecutor(ExecutorType.SpellSet, 47475363, new Func<bool>(this.TrapSet));
			base.AddExecutor(ExecutorType.SpellSet, 75249652, new Func<bool>(this.TrapSet));
			base.AddExecutor(ExecutorType.SpellSet, 5650082, new Func<bool>(this.TrapSet));
			base.AddExecutor(ExecutorType.SpellSet, 44095762, new Func<bool>(this.TrapSet));
			base.AddExecutor(ExecutorType.SpellSet, 20522190, new Func<bool>(this.TrapSet));
			base.AddExecutor(ExecutorType.SpellSet, 29401950, new Func<bool>(this.TrapSet));
			base.AddExecutor(ExecutorType.SpellSet, 29616929, new Func<bool>(this.TrapSet));
			base.AddExecutor(ExecutorType.Activate, 58120309, new Func<bool>(base.DefaultTrap));
			base.AddExecutor(ExecutorType.Activate, 40838625, new Func<bool>(base.DefaultUniqueTrap));
			base.AddExecutor(ExecutorType.Activate, 47475363, new Func<bool>(base.DefaultUniqueTrap));
			base.AddExecutor(ExecutorType.Activate, 75249652, new Func<bool>(base.DefaultUniqueTrap));
			base.AddExecutor(ExecutorType.Activate, 5650082, new Func<bool>(base.DefaultUniqueTrap));
			base.AddExecutor(ExecutorType.Activate, 44095762, new Func<bool>(base.DefaultUniqueTrap));
			base.AddExecutor(ExecutorType.Activate, 20522190, new Func<bool>(base.DefaultUniqueTrap));
			base.AddExecutor(ExecutorType.Activate, 29401950, new Func<bool>(base.DefaultUniqueTrap));
			base.AddExecutor(ExecutorType.Activate, 29616929, new Func<bool>(base.DefaultUniqueTrap));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(base.DefaultMonsterRepos));
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x000A4EE7 File Offset: 0x000A30E7
		public override void OnNewTurn()
		{
			this.NormalSummoned = false;
			base.OnNewTurn();
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x000A4EF6 File Offset: 0x000A30F6
		public override bool OnPreBattleBetween(ClientCard attacker, ClientCard defender)
		{
			if (!defender.IsMonsterHasPreventActivationEffectInBattle() && base.Bot.HasInMonstersZone(37279508, true, true, false))
			{
				attacker.RealPower += 1000;
			}
			return base.OnPreBattleBetween(attacker, defender);
		}

		// Token: 0x06001B8C RID: 7052 RVA: 0x000A4F30 File Offset: 0x000A3130
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
			IList<ClientCard> result = new List<ClientCard>();
			foreach (ClientCard card in cards)
			{
				foreach (ClientCard card2 in cards)
				{
					if (card.IsCode(card2.Id) && !card.Equals(card2))
					{
						result.Add(card);
						result.Add(card2);
						break;
					}
				}
				if (result.Count > 0)
				{
					break;
				}
			}
			return base.Util.CheckSelectCount(result, cards, min, max);
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x000A5040 File Offset: 0x000A3240
		private bool UnexpectedDaiEffect()
		{
			if (base.Bot.HasInHand(85138716) || this.NormalSummoned)
			{
				base.AI.SelectCard(new int[] { 18108166, 74852097, 81823360 });
			}
			else if (base.Util.IsTurn1OrMain2())
			{
				if (base.Bot.HasInHand(18108166))
				{
					base.AI.SelectCard(18108166);
				}
				else if (base.Bot.HasInHand(81823360))
				{
					base.AI.SelectCard(81823360);
				}
				else if (base.Bot.HasInHand(87979586))
				{
					base.AI.SelectCard(87979586);
				}
			}
			else if (base.Bot.HasInHand(81823360))
			{
				base.AI.SelectCard(81823360);
			}
			else if (base.Bot.HasInHand(75195825))
			{
				base.AI.SelectCard(75195825);
			}
			else if (base.Bot.HasInHand(74852097))
			{
				base.AI.SelectCard(74852097);
			}
			else if (base.Bot.HasInHand(87979586))
			{
				base.AI.SelectCard(new int[] { 33256280, 75195825 });
			}
			return true;
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x000A51B0 File Offset: 0x000A33B0
		private bool RescueRabbitSummon()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (base.Util.GetBotAvailZonesFromExtraDeck() <= 0)
			{
				if (base.Enemy.MonsterZone.IsExistingMatchingCard((ClientCard card) => card.GetDefensePower() >= 1900, 1))
				{
					return base.Enemy.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.GetDefensePower() < 1900) > base.Bot.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.Attack >= 1900);
				}
			}
			return true;
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x000A5274 File Offset: 0x000A3474
		private bool RescueRabbitEffect()
		{
			if (base.Util.IsTurn1OrMain2())
			{
				base.AI.SelectCard(new int[] { 81823360, 18108166 });
			}
			else
			{
				base.AI.SelectCard(new int[] { 75195825, 74852097, 81823360, 33256280, 87979586 });
			}
			return true;
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x000A52CE File Offset: 0x000A34CE
		private bool MysteryShellDragonSummon()
		{
			return base.Bot.HasInMonstersZone(18108166, false, false, false);
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x000A52E3 File Offset: 0x000A34E3
		private bool PhantomGryphonSummon()
		{
			return base.Bot.HasInMonstersZone(74852097, false, false, false);
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x000A52F8 File Offset: 0x000A34F8
		private bool MasterPendulumTheDracoslayerSummon()
		{
			return base.Bot.HasInMonstersZone(75195825, false, false, false);
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x000A530D File Offset: 0x000A350D
		private bool AngelTrumpeterSummon()
		{
			return base.Bot.HasInMonstersZone(87979586, false, false, false);
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x000A5322 File Offset: 0x000A3522
		private bool MetalfoesGoldriverSummon()
		{
			return base.Bot.HasInMonstersZone(33256280, false, false, false);
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x000A5337 File Offset: 0x000A3537
		private bool MegalosmasherXSummon()
		{
			return base.Bot.HasInMonstersZone(81823360, false, false, false);
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x000A534C File Offset: 0x000A354C
		private bool NormalSummon()
		{
			return base.Card.Id != 85138716;
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x000A5363 File Offset: 0x000A3563
		private bool GagagaCowboySummon()
		{
			if (base.Enemy.LifePoints <= 800)
			{
				base.AI.SelectPosition(CardPosition.FaceUpDefence);
				return true;
			}
			return false;
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x000359F6 File Offset: 0x00033BF6
		private bool IgnisterProminenceTheBlastingDracoslayerSummon()
		{
			return base.Util.GetProblematicEnemyCard(0, false) != null;
		}

		// Token: 0x06001B99 RID: 7065 RVA: 0x000A5388 File Offset: 0x000A3588
		private bool IgnisterProminenceTheBlastingDracoslayerEffect()
		{
			if (base.ActivateDescription == base.Util.GetStringId(18239909, 1))
			{
				return true;
			}
			ClientCard target = null;
			ClientCard target2 = base.Util.GetProblematicEnemyCard(0, false);
			List<ClientCard> spells = base.Enemy.GetSpells();
			foreach (ClientCard spell in spells)
			{
				if (spell.HasType(CardType.Pendulum) && !spell.Equals(target2))
				{
					target = spell;
					break;
				}
			}
			List<ClientCard> monsters = base.Enemy.GetMonsters();
			foreach (ClientCard monster in monsters)
			{
				if (monster.HasType(CardType.Pendulum) && !monster.Equals(target2))
				{
					target = monster;
					break;
				}
			}
			if (target2 == null && target != null)
			{
				foreach (ClientCard spell2 in spells)
				{
					if (!spell2.Equals(target))
					{
						target2 = spell2;
						break;
					}
				}
				foreach (ClientCard monster2 in monsters)
				{
					if (!monster2.Equals(target))
					{
						target2 = monster2;
						break;
					}
				}
			}
			if (target2 == null)
			{
				return false;
			}
			base.AI.SelectCard(target);
			base.AI.SelectNextCard(target2);
			return true;
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x000A5538 File Offset: 0x000A3738
		private bool Number37HopeWovenDragonSpiderSharkSummon()
		{
			return base.Util.IsAllEnemyBetterThanValue(1700, false) && !base.Util.IsOneEnemyBetterThanValue(3600, true);
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x000A5564 File Offset: 0x000A3764
		private bool LightningChidoriSummon()
		{
			using (List<ClientCard>.Enumerator enumerator = base.Enemy.GetMonsters().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsFacedown())
					{
						return true;
					}
				}
			}
			using (List<ClientCard>.Enumerator enumerator = base.Enemy.GetSpells().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsFacedown())
					{
						return true;
					}
				}
			}
			return base.Util.GetProblematicEnemyCard(0, false) != null;
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x000A561C File Offset: 0x000A381C
		private bool LightningChidoriEffect()
		{
			ClientCard problematicCard = base.Util.GetProblematicEnemyCard(0, false);
			base.AI.SelectCard(0);
			base.AI.SelectNextCard(problematicCard);
			return true;
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x000A5650 File Offset: 0x000A3850
		private bool EvolzarLaggiaSummon()
		{
			return (base.Util.IsAllEnemyBetterThanValue(2000, false) && !base.Util.IsOneEnemyBetterThanValue(2400, true)) || base.Util.IsTurn1OrMain2();
		}

		// Token: 0x06001B9E RID: 7070 RVA: 0x000A5685 File Offset: 0x000A3885
		private bool EvilswarmNightmareSummon()
		{
			if (base.Util.IsTurn1OrMain2())
			{
				base.AI.SelectPosition(CardPosition.FaceUpDefence);
				return true;
			}
			return false;
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x000A56A4 File Offset: 0x000A38A4
		private bool TraptrixRafflesiaSummon()
		{
			if (base.Util.IsTurn1OrMain2() && base.Bot.GetRemainingCount(29401950, 1) + base.Bot.GetRemainingCount(29616929, 1) > 0)
			{
				base.AI.SelectPosition(CardPosition.FaceUpDefence);
				return true;
			}
			return false;
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x000A56F4 File Offset: 0x000A38F4
		private bool Number59CrookedCookSummon()
		{
			return base.Bot.GetMonsterCount() + base.Bot.GetSpellCount() - 2 <= 1 && ((base.Util.IsOneEnemyBetter(false) && !base.Util.IsOneEnemyBetterThanValue(2300, true)) || base.Util.IsTurn1OrMain2());
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x000A5750 File Offset: 0x000A3950
		private bool Number59CrookedCookEffect()
		{
			if (base.Duel.Player == 0)
			{
				if (base.Util.IsChainTarget(base.Card))
				{
					return true;
				}
			}
			else if (base.Bot.GetMonsterCount() + base.Bot.GetSpellCount() - 1 <= 1)
			{
				return true;
			}
			return false;
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x000A579E File Offset: 0x000A399E
		private bool EvolzarLaggiaEffect()
		{
			return base.DefaultTrap();
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x000A57A6 File Offset: 0x000A39A6
		private bool StarliegePaladynamoSummon()
		{
			return this.StarliegePaladynamoEffect();
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x000A57B0 File Offset: 0x000A39B0
		private bool StarliegePaladynamoEffect()
		{
			ClientCard result = base.Util.GetOneEnemyBetterThanValue(2000, true, false);
			if (result != null)
			{
				base.AI.SelectCard(0);
				base.AI.SelectNextCard(result);
				return true;
			}
			return false;
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x000A57EE File Offset: 0x000A39EE
		private bool TrapSet()
		{
			return !base.Bot.HasInMonstersZone(82697249, true, true, false);
		}

		// Token: 0x04001ED9 RID: 7897
		private bool NormalSummoned;

		// Token: 0x0200039C RID: 924
		public class CardId
		{
			// Token: 0x04001EDA RID: 7898
			public const int MysteryShellDragon = 18108166;

			// Token: 0x04001EDB RID: 7899
			public const int PhantomGryphon = 74852097;

			// Token: 0x04001EDC RID: 7900
			public const int MasterPendulumTheDracoslayer = 75195825;

			// Token: 0x04001EDD RID: 7901
			public const int AngelTrumpeter = 87979586;

			// Token: 0x04001EDE RID: 7902
			public const int MetalfoesGoldriver = 33256280;

			// Token: 0x04001EDF RID: 7903
			public const int MegalosmasherX = 81823360;

			// Token: 0x04001EE0 RID: 7904
			public const int RescueRabbit = 85138716;

			// Token: 0x04001EE1 RID: 7905
			public const int UnexpectedDai = 911883;

			// Token: 0x04001EE2 RID: 7906
			public const int HarpiesFeatherDuster = 18144506;

			// Token: 0x04001EE3 RID: 7907
			public const int PotOfDesires = 35261759;

			// Token: 0x04001EE4 RID: 7908
			public const int MonsterReborn = 83764718;

			// Token: 0x04001EE5 RID: 7909
			public const int SmashingGround = 97169186;

			// Token: 0x04001EE6 RID: 7910
			public const int QuakingMirrorForce = 40838625;

			// Token: 0x04001EE7 RID: 7911
			public const int DrowningMirrorForce = 47475363;

			// Token: 0x04001EE8 RID: 7912
			public const int BlazingMirrorForce = 75249652;

			// Token: 0x04001EE9 RID: 7913
			public const int StormingMirrorForce = 5650082;

			// Token: 0x04001EEA RID: 7914
			public const int MirrorForce = 44095762;

			// Token: 0x04001EEB RID: 7915
			public const int DarkMirrorForce = 20522190;

			// Token: 0x04001EEC RID: 7916
			public const int BottomlessTrapHole = 29401950;

			// Token: 0x04001EED RID: 7917
			public const int TraptrixTrapHoleNightmare = 29616929;

			// Token: 0x04001EEE RID: 7918
			public const int StarlightRoad = 58120309;

			// Token: 0x04001EEF RID: 7919
			public const int ScarlightRedDragonArchfiend = 80666118;

			// Token: 0x04001EF0 RID: 7920
			public const int IgnisterProminenceTheBlastingDracoslayer = 18239909;

			// Token: 0x04001EF1 RID: 7921
			public const int StardustDragon = 44508094;

			// Token: 0x04001EF2 RID: 7922
			public const int NumberS39UtopiatheLightning = 56832966;

			// Token: 0x04001EF3 RID: 7923
			public const int Number37HopeWovenDragonSpiderShark = 37279508;

			// Token: 0x04001EF4 RID: 7924
			public const int Number39Utopia = 84013237;

			// Token: 0x04001EF5 RID: 7925
			public const int EvolzarLaggia = 74294676;

			// Token: 0x04001EF6 RID: 7926
			public const int Number59CrookedCook = 82697249;

			// Token: 0x04001EF7 RID: 7927
			public const int CastelTheSkyblasterMusketeer = 82633039;

			// Token: 0x04001EF8 RID: 7928
			public const int StarliegePaladynamo = 61344030;

			// Token: 0x04001EF9 RID: 7929
			public const int LightningChidori = 22653490;

			// Token: 0x04001EFA RID: 7930
			public const int EvilswarmExcitonKnight = 46772449;

			// Token: 0x04001EFB RID: 7931
			public const int GagagaCowboy = 12014404;

			// Token: 0x04001EFC RID: 7932
			public const int EvilswarmNightmare = 359563;

			// Token: 0x04001EFD RID: 7933
			public const int TraptrixRafflesia = 6511113;
		}
	}
}
