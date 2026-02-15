using System;
using System.Collections.Generic;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x02000432 RID: 1074
	[Deck("Yosenju", "AI_Yosenju", "Normal")]
	public class YosenjuExecutor : DefaultExecutor
	{
		// Token: 0x060022CE RID: 8910 RVA: 0x000E2CC8 File Offset: 0x000E0EC8
		public YosenjuExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 59750328, new Func<bool>(this.CardOfDemiseEPEffect));
			base.AddExecutor(ExecutorType.SpSummon, 12014404, new Func<bool>(this.GagagaCowboySummon));
			base.AddExecutor(ExecutorType.Activate, 12014404);
			base.AddExecutor(ExecutorType.Activate, 18144507, new Func<bool>(base.DefaultHarpiesFeatherDusterFirst));
			base.AddExecutor(ExecutorType.Activate, 8267140, new Func<bool>(base.DefaultCosmicCyclone));
			base.AddExecutor(ExecutorType.Activate, 18144507);
			base.AddExecutor(ExecutorType.Activate, 53129443, new Func<bool>(base.DefaultDarkHole));
			base.AddExecutor(ExecutorType.Activate, 98645731, new Func<bool>(this.PotOfDualityEffect));
			base.AddExecutor(ExecutorType.Summon, 65247798, new Func<bool>(this.HaveAnotherYosenjuWithSameNameInHand));
			base.AddExecutor(ExecutorType.Summon, 92246806, new Func<bool>(this.HaveAnotherYosenjuWithSameNameInHand));
			base.AddExecutor(ExecutorType.Summon, 28630501, new Func<bool>(this.HaveAnotherYosenjuWithSameNameInHand));
			base.AddExecutor(ExecutorType.Summon, 65247798);
			base.AddExecutor(ExecutorType.Summon, 92246806);
			base.AddExecutor(ExecutorType.Summon, 28630501);
			base.AddExecutor(ExecutorType.Summon, 25244515);
			base.AddExecutor(ExecutorType.Activate, 65247798, new Func<bool>(this.YosenjuEffect));
			base.AddExecutor(ExecutorType.Activate, 92246806, new Func<bool>(this.YosenjuEffect));
			base.AddExecutor(ExecutorType.Activate, 28630501, new Func<bool>(this.YosenjuEffect));
			base.AddExecutor(ExecutorType.Activate, 25244515, new Func<bool>(this.YosenjuEffect));
			base.AddExecutor(ExecutorType.SpellSet, 41420027, new Func<bool>(this.TrapSetUnique));
			base.AddExecutor(ExecutorType.SpellSet, 40605147, new Func<bool>(this.TrapSetUnique));
			base.AddExecutor(ExecutorType.SpellSet, 84749824, new Func<bool>(this.TrapSetUnique));
			base.AddExecutor(ExecutorType.SpellSet, 30241314, new Func<bool>(this.TrapSetUnique));
			base.AddExecutor(ExecutorType.SpellSet, 5851097, new Func<bool>(this.TrapSetUnique));
			base.AddExecutor(ExecutorType.SpellSet, 59344077, new Func<bool>(this.TrapSetUnique));
			base.AddExecutor(ExecutorType.SpellSet, 47475363, new Func<bool>(this.TrapSetUnique));
			base.AddExecutor(ExecutorType.SpellSet, 40838625, new Func<bool>(this.TrapSetUnique));
			base.AddExecutor(ExecutorType.SpellSet, 58120309, new Func<bool>(this.TrapSetUnique));
			base.AddExecutor(ExecutorType.SpellSet, 41420027, new Func<bool>(this.TrapSetWhenZoneFree));
			base.AddExecutor(ExecutorType.SpellSet, 40605147, new Func<bool>(this.TrapSetWhenZoneFree));
			base.AddExecutor(ExecutorType.SpellSet, 84749824, new Func<bool>(this.TrapSetWhenZoneFree));
			base.AddExecutor(ExecutorType.SpellSet, 30241314, new Func<bool>(this.TrapSetWhenZoneFree));
			base.AddExecutor(ExecutorType.SpellSet, 5851097, new Func<bool>(this.TrapSetWhenZoneFree));
			base.AddExecutor(ExecutorType.SpellSet, 59344077, new Func<bool>(this.TrapSetWhenZoneFree));
			base.AddExecutor(ExecutorType.SpellSet, 47475363, new Func<bool>(this.TrapSetWhenZoneFree));
			base.AddExecutor(ExecutorType.SpellSet, 40838625, new Func<bool>(this.TrapSetWhenZoneFree));
			base.AddExecutor(ExecutorType.SpellSet, 58120309, new Func<bool>(this.TrapSetWhenZoneFree));
			base.AddExecutor(ExecutorType.SpellSet, 18144507, new Func<bool>(this.TrapSetWhenZoneFree));
			base.AddExecutor(ExecutorType.SpellSet, 53129443, new Func<bool>(this.TrapSetWhenZoneFree));
			base.AddExecutor(ExecutorType.SpellSet, 98645731, new Func<bool>(this.TrapSetWhenZoneFree));
			base.AddExecutor(ExecutorType.SpellSet, 8267140, new Func<bool>(this.TrapSetWhenZoneFree));
			base.AddExecutor(ExecutorType.SpellSet, 59750328);
			base.AddExecutor(ExecutorType.Activate, 59750328, new Func<bool>(this.CardOfDemiseEffect));
			base.AddExecutor(ExecutorType.SpellSet, 41420027, new Func<bool>(this.CardOfDemiseAcivated));
			base.AddExecutor(ExecutorType.SpellSet, 40605147, new Func<bool>(this.CardOfDemiseAcivated));
			base.AddExecutor(ExecutorType.SpellSet, 84749824, new Func<bool>(this.CardOfDemiseAcivated));
			base.AddExecutor(ExecutorType.SpellSet, 30241314, new Func<bool>(this.CardOfDemiseAcivated));
			base.AddExecutor(ExecutorType.SpellSet, 5851097, new Func<bool>(this.CardOfDemiseAcivated));
			base.AddExecutor(ExecutorType.SpellSet, 59344077, new Func<bool>(this.CardOfDemiseAcivated));
			base.AddExecutor(ExecutorType.SpellSet, 47475363, new Func<bool>(this.CardOfDemiseAcivated));
			base.AddExecutor(ExecutorType.SpellSet, 40838625, new Func<bool>(this.CardOfDemiseAcivated));
			base.AddExecutor(ExecutorType.SpellSet, 58120309, new Func<bool>(this.CardOfDemiseAcivated));
			base.AddExecutor(ExecutorType.SpellSet, 18144507, new Func<bool>(this.CardOfDemiseAcivated));
			base.AddExecutor(ExecutorType.SpellSet, 53129443, new Func<bool>(this.CardOfDemiseAcivated));
			base.AddExecutor(ExecutorType.SpellSet, 98645731, new Func<bool>(this.CardOfDemiseAcivated));
			base.AddExecutor(ExecutorType.SpellSet, 8267140, new Func<bool>(this.CardOfDemiseAcivated));
			base.AddExecutor(ExecutorType.SpSummon, 46772449, new Func<bool>(base.DefaultEvilswarmExcitonKnightSummon));
			base.AddExecutor(ExecutorType.Activate, 46772449, new Func<bool>(base.DefaultEvilswarmExcitonKnightEffect));
			base.AddExecutor(ExecutorType.SpSummon, 16195942, new Func<bool>(this.DarkRebellionXyzDragonSummon));
			base.AddExecutor(ExecutorType.Activate, 16195942, new Func<bool>(this.DarkRebellionXyzDragonEffect));
			base.AddExecutor(ExecutorType.SpSummon, 84013237, new Func<bool>(base.DefaultNumberS39UtopiaTheLightningSummon));
			base.AddExecutor(ExecutorType.SpSummon, 86532744);
			base.AddExecutor(ExecutorType.SpSummon, 56832966);
			base.AddExecutor(ExecutorType.Activate, 56832966, new Func<bool>(base.DefaultNumberS39UtopiaTheLightningEffect));
			base.AddExecutor(ExecutorType.Activate, 44508094, new Func<bool>(base.DefaultStardustDragonEffect));
			base.AddExecutor(ExecutorType.Activate, 58120309, new Func<bool>(base.DefaultTrap));
			base.AddExecutor(ExecutorType.Activate, 59344077);
			base.AddExecutor(ExecutorType.Activate, 84749824, new Func<bool>(base.DefaultSolemnWarning));
			base.AddExecutor(ExecutorType.Activate, 40605147, new Func<bool>(base.DefaultSolemnStrike));
			base.AddExecutor(ExecutorType.Activate, 41420027, new Func<bool>(base.DefaultSolemnJudgment));
			base.AddExecutor(ExecutorType.Activate, 30241314, new Func<bool>(base.DefaultUniqueTrap));
			base.AddExecutor(ExecutorType.Activate, 5851097, new Func<bool>(base.DefaultUniqueTrap));
			base.AddExecutor(ExecutorType.Activate, 47475363, new Func<bool>(base.DefaultUniqueTrap));
			base.AddExecutor(ExecutorType.Activate, 40838625, new Func<bool>(base.DefaultUniqueTrap));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(base.DefaultMonsterRepos));
		}

		// Token: 0x060022CF RID: 8911 RVA: 0x0000763C File Offset: 0x0000583C
		public override bool OnSelectHand()
		{
			return true;
		}

		// Token: 0x060022D0 RID: 8912 RVA: 0x000E3368 File Offset: 0x000E1568
		public override void OnNewTurn()
		{
			this.CardOfDemiseUsed = false;
			base.OnNewTurn();
		}

		// Token: 0x060022D1 RID: 8913 RVA: 0x000E3377 File Offset: 0x000E1577
		public override bool OnSelectYesNo(int desc)
		{
			return base.Card == null || !base.Card.IsCode(92246806) || base.Card.ShouldDirectAttack;
		}

		// Token: 0x060022D2 RID: 8914 RVA: 0x000E33A2 File Offset: 0x000E15A2
		public override bool OnPreBattleBetween(ClientCard attacker, ClientCard defender)
		{
			if (!defender.IsMonsterHasPreventActivationEffectInBattle() && attacker.Attribute == 8 && base.Bot.HasInHand(25244515))
			{
				attacker.RealPower += 1000;
			}
			return base.OnPreBattleBetween(attacker, defender);
		}

		// Token: 0x060022D3 RID: 8915 RVA: 0x000E33E4 File Offset: 0x000E15E4
		public override IList<ClientCard> OnSelectXyzMaterial(IList<ClientCard> cards, int min, int max)
		{
			IList<ClientCard> result = base.Util.SelectPreferredCards(25244515, cards, min, max);
			return base.Util.CheckSelectCount(result, cards, min, max);
		}

		// Token: 0x060022D4 RID: 8916 RVA: 0x000E3414 File Offset: 0x000E1614
		private bool PotOfDualityEffect()
		{
			if (this.CardOfDemiseUsed)
			{
				base.AI.SelectCard(new int[]
				{
					58120309, 59344077, 41420027, 5851097, 18144507, 47475363, 40838625, 40605147, 84749824, 30241314,
					59750328
				});
			}
			else
			{
				base.AI.SelectCard(new int[]
				{
					28630501, 65247798, 92246806, 58120309, 59344077, 5851097, 18144507, 47475363, 40838625, 40605147,
					41420027, 84749824, 30241314, 59750328
				});
			}
			return true;
		}

		// Token: 0x060022D5 RID: 8917 RVA: 0x000E3466 File Offset: 0x000E1666
		private bool CardOfDemiseEffect()
		{
			if (base.Util.IsTurn1OrMain2())
			{
				this.CardOfDemiseUsed = true;
				return true;
			}
			return false;
		}

		// Token: 0x060022D6 RID: 8918 RVA: 0x000E3480 File Offset: 0x000E1680
		private bool HaveAnotherYosenjuWithSameNameInHand()
		{
			foreach (ClientCard card in base.Bot.Hand.GetMonsters())
			{
				if (!card.Equals(base.Card) && card.IsCode(base.Card.Id))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060022D7 RID: 8919 RVA: 0x000E3500 File Offset: 0x000E1700
		private bool TrapSetUnique()
		{
			using (List<ClientCard>.Enumerator enumerator = base.Bot.GetSpells().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsCode(base.Card.Id))
					{
						return false;
					}
				}
			}
			return this.TrapSetWhenZoneFree();
		}

		// Token: 0x060022D8 RID: 8920 RVA: 0x000A437C File Offset: 0x000A257C
		private bool TrapSetWhenZoneFree()
		{
			return base.Bot.GetSpellCountWithoutField() < 4;
		}

		// Token: 0x060022D9 RID: 8921 RVA: 0x000E3570 File Offset: 0x000E1770
		private bool CardOfDemiseAcivated()
		{
			return this.CardOfDemiseUsed;
		}

		// Token: 0x060022DA RID: 8922 RVA: 0x000E3578 File Offset: 0x000E1778
		private bool YosenjuEffect()
		{
			if (base.Duel.Phase == DuelPhase.End)
			{
				return false;
			}
			base.AI.SelectCard(new int[] { 65247798, 92246806, 28630501 });
			return true;
		}

		// Token: 0x060022DB RID: 8923 RVA: 0x000E35AB File Offset: 0x000E17AB
		private bool CardOfDemiseEPEffect()
		{
			return base.Duel.Phase == DuelPhase.End;
		}

		// Token: 0x060022DC RID: 8924 RVA: 0x000E35C0 File Offset: 0x000E17C0
		private bool GagagaCowboySummon()
		{
			if (base.Enemy.LifePoints <= 800 || (base.Bot.GetMonsterCount() >= 4 && base.Enemy.LifePoints <= 1600))
			{
				base.AI.SelectPosition(CardPosition.FaceUpDefence);
				return true;
			}
			return false;
		}

		// Token: 0x060022DD RID: 8925 RVA: 0x000E3610 File Offset: 0x000E1810
		private bool DarkRebellionXyzDragonSummon()
		{
			int bestAttack = base.Util.GetBestAttack(base.Bot);
			int oppoBestAttack = base.Util.GetBestAttack(base.Enemy);
			return bestAttack <= oppoBestAttack;
		}

		// Token: 0x060022DE RID: 8926 RVA: 0x000E3648 File Offset: 0x000E1848
		private bool DarkRebellionXyzDragonEffect()
		{
			int oppoBestAttack = base.Util.GetBestAttack(base.Enemy);
			ClientCard target = base.Util.GetOneEnemyBetterThanValue(oppoBestAttack, true, false);
			if (target != null)
			{
				base.AI.SelectCard(0);
				base.AI.SelectNextCard(target);
			}
			return true;
		}

		// Token: 0x040024ED RID: 9453
		private bool CardOfDemiseUsed;

		// Token: 0x02000433 RID: 1075
		public class CardId
		{
			// Token: 0x040024EE RID: 9454
			public const int YosenjuKama1 = 65247798;

			// Token: 0x040024EF RID: 9455
			public const int YosenjuKama2 = 92246806;

			// Token: 0x040024F0 RID: 9456
			public const int YosenjuKama3 = 28630501;

			// Token: 0x040024F1 RID: 9457
			public const int YosenjuTsujik = 25244515;

			// Token: 0x040024F2 RID: 9458
			public const int HarpiesFeatherDuster = 18144507;

			// Token: 0x040024F3 RID: 9459
			public const int DarkHole = 53129443;

			// Token: 0x040024F4 RID: 9460
			public const int CardOfDemise = 59750328;

			// Token: 0x040024F5 RID: 9461
			public const int PotOfDuality = 98645731;

			// Token: 0x040024F6 RID: 9462
			public const int CosmicCyclone = 8267140;

			// Token: 0x040024F7 RID: 9463
			public const int QuakingMirrorForce = 40838625;

			// Token: 0x040024F8 RID: 9464
			public const int DrowningMirrorForce = 47475363;

			// Token: 0x040024F9 RID: 9465
			public const int StarlightRoad = 58120309;

			// Token: 0x040024FA RID: 9466
			public const int VanitysEmptiness = 5851097;

			// Token: 0x040024FB RID: 9467
			public const int MacroCosmos = 30241314;

			// Token: 0x040024FC RID: 9468
			public const int SolemnStrike = 40605147;

			// Token: 0x040024FD RID: 9469
			public const int SolemnWarning = 84749824;

			// Token: 0x040024FE RID: 9470
			public const int SolemnJudgment = 41420027;

			// Token: 0x040024FF RID: 9471
			public const int MagicDrain = 59344077;

			// Token: 0x04002500 RID: 9472
			public const int StardustDragon = 44508094;

			// Token: 0x04002501 RID: 9473
			public const int NumberS39UtopiatheLightning = 56832966;

			// Token: 0x04002502 RID: 9474
			public const int NumberS39UtopiaOne = 86532744;

			// Token: 0x04002503 RID: 9475
			public const int DarkRebellionXyzDragon = 16195942;

			// Token: 0x04002504 RID: 9476
			public const int Number39Utopia = 84013237;

			// Token: 0x04002505 RID: 9477
			public const int Number103Ragnazero = 94380860;

			// Token: 0x04002506 RID: 9478
			public const int BrotherhoodOfTheFireFistTigerKing = 96381979;

			// Token: 0x04002507 RID: 9479
			public const int Number106GiantHand = 63746411;

			// Token: 0x04002508 RID: 9480
			public const int CastelTheSkyblasterMusketeer = 82633039;

			// Token: 0x04002509 RID: 9481
			public const int DiamondDireWolf = 95169481;

			// Token: 0x0400250A RID: 9482
			public const int LightningChidori = 22653490;

			// Token: 0x0400250B RID: 9483
			public const int EvilswarmExcitonKnight = 46772449;

			// Token: 0x0400250C RID: 9484
			public const int AbyssDweller = 21044178;

			// Token: 0x0400250D RID: 9485
			public const int GagagaCowboy = 12014404;
		}
	}
}
