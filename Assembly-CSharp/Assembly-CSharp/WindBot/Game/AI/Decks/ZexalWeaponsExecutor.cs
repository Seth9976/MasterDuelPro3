using System;
using System.Collections.Generic;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x02000458 RID: 1112
	[Deck("Zexal Weapons", "AI_ZexalWeapons", "Normal")]
	internal class ZexalWeaponsExecutor : DefaultExecutor
	{
		// Token: 0x0600249C RID: 9372 RVA: 0x000EFAD4 File Offset: 0x000EDCD4
		public ZexalWeaponsExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 5318639, new Func<bool>(base.DefaultMysticalSpaceTyphoon));
			base.AddExecutor(ExecutorType.Activate, 53129443, new Func<bool>(base.DefaultDarkHole));
			base.AddExecutor(ExecutorType.Activate, 12580477, new Func<bool>(base.DefaultRaigeki));
			base.AddExecutor(ExecutorType.Activate, 32807846, new Func<bool>(this.ReinforcementOfTheArmy));
			base.AddExecutor(ExecutorType.Activate, 11705261, new Func<bool>(this.XyzChangeTactics));
			base.AddExecutor(ExecutorType.SpSummon, 84013237);
			base.AddExecutor(ExecutorType.SpSummon, 86532744);
			base.AddExecutor(ExecutorType.SpSummon, 56832966);
			base.AddExecutor(ExecutorType.SpSummon, 29669359, new Func<bool>(this.Number61Volcasaurus));
			base.AddExecutor(ExecutorType.SpSummon, 60992364);
			base.AddExecutor(ExecutorType.SpSummon, 94119480);
			base.AddExecutor(ExecutorType.Activate, 84013237, new Func<bool>(this.Number39Utopia));
			base.AddExecutor(ExecutorType.Activate, 86532744);
			base.AddExecutor(ExecutorType.Activate, 56832966, new Func<bool>(base.DefaultNumberS39UtopiaTheLightningEffect));
			base.AddExecutor(ExecutorType.Activate, 60992364, new Func<bool>(this.ZwLionArms));
			base.AddExecutor(ExecutorType.Activate, 94119480);
			base.AddExecutor(ExecutorType.Activate, 29669359);
			base.AddExecutor(ExecutorType.Activate, 81471108, new Func<bool>(this.ZwWeapon));
			base.AddExecutor(ExecutorType.Activate, 45082499, new Func<bool>(this.ZwWeapon));
			base.AddExecutor(ExecutorType.Activate, 40941889, new Func<bool>(this.ZwWeapon));
			base.AddExecutor(ExecutorType.SpSummon, 65367484);
			base.AddExecutor(ExecutorType.SpSummon, 70095155);
			base.AddExecutor(ExecutorType.SpSummon, 33911264, new Func<bool>(this.SolarWindJammer));
			base.AddExecutor(ExecutorType.Activate, 1845204, new Func<bool>(this.InstantFusion));
			base.AddExecutor(ExecutorType.Summon, 25259669, new Func<bool>(this.GoblindberghFirst));
			base.AddExecutor(ExecutorType.Summon, 18063928, new Func<bool>(this.GoblindberghFirst));
			base.AddExecutor(ExecutorType.Summon, 24610207);
			base.AddExecutor(ExecutorType.Summon, 30914564);
			base.AddExecutor(ExecutorType.Summon, 34143852);
			base.AddExecutor(ExecutorType.Summon, 25259669);
			base.AddExecutor(ExecutorType.Summon, 18063928);
			base.AddExecutor(ExecutorType.Summon, 423585);
			base.AddExecutor(ExecutorType.Summon, 37742478);
			base.AddExecutor(ExecutorType.Activate, 25259669, new Func<bool>(this.GoblindberghEffect));
			base.AddExecutor(ExecutorType.Activate, 18063928, new Func<bool>(this.GoblindberghEffect));
			base.AddExecutor(ExecutorType.Activate, 94656263, new Func<bool>(this.KagetokageEffect));
			base.AddExecutor(ExecutorType.Activate, 423585, new Func<bool>(this.SummonerMonkEffect));
			base.AddExecutor(ExecutorType.Activate, 37742478, new Func<bool>(base.DefaultHonestEffect));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(this.MonsterRepos));
			base.AddExecutor(ExecutorType.SpSummon, 91949988);
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(base.DefaultSpellSet));
			base.AddExecutor(ExecutorType.Activate, 78474168, new Func<bool>(base.DefaultBreakthroughSkill));
			base.AddExecutor(ExecutorType.Activate, 84749824, new Func<bool>(base.DefaultSolemnWarning));
			base.AddExecutor(ExecutorType.Activate, 40605147, new Func<bool>(base.DefaultSolemnStrike));
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnSelectHand()
		{
			return false;
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x000EFE28 File Offset: 0x000EE028
		public override bool OnPreBattleBetween(ClientCard attacker, ClientCard defender)
		{
			if (!defender.IsMonsterHasPreventActivationEffectInBattle() && attacker.Attribute == 16 && base.Bot.HasInHand(37742478))
			{
				attacker.RealPower += defender.Attack;
			}
			return base.OnPreBattleBetween(attacker, defender);
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x000EFE74 File Offset: 0x000EE074
		public override IList<ClientCard> OnSelectXyzMaterial(IList<ClientCard> cards, int min, int max)
		{
			IList<ClientCard> result = base.Util.SelectPreferredCards(new int[] { 24610207, 33911264, 25259669 }, cards, min, max);
			return base.Util.CheckSelectCount(result, cards, min, max);
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x000EFEB0 File Offset: 0x000EE0B0
		private bool Number39Utopia()
		{
			return !base.Util.HasChainedTrap(0) && base.Duel.Player == 1 && base.Duel.Phase == DuelPhase.BattleStart && base.Card.HasXyzMaterial(2);
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x000A5E74 File Offset: 0x000A4074
		private bool Number61Volcasaurus()
		{
			return base.Util.IsOneEnemyBetterThanValue(2000, false);
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x000EFEF0 File Offset: 0x000EE0F0
		private bool ZwLionArms()
		{
			return base.ActivateDescription == base.Util.GetStringId(60992364, 0) || (base.ActivateDescription == base.Util.GetStringId(60992364, 1) && !base.Card.IsDisabled() && this.ZwWeapon());
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x0000763C File Offset: 0x0000583C
		private bool ZwWeapon()
		{
			return true;
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x000EFF48 File Offset: 0x000EE148
		private bool ReinforcementOfTheArmy()
		{
			base.AI.SelectCard(new int[] { 25259669, 18063928, 24610207, 94656263, 30914564 });
			return true;
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x000EFF68 File Offset: 0x000EE168
		private bool InstantFusion()
		{
			if (base.Bot.LifePoints <= 1000)
			{
				return false;
			}
			int count4 = 0;
			int count5 = 0;
			foreach (ClientCard clientCard in base.Bot.GetMonsters())
			{
				if (clientCard.Level == 5)
				{
					count5++;
				}
				if (clientCard.Level == 4)
				{
					count4++;
				}
			}
			if (count5 == 1)
			{
				base.AI.SelectCard(45231177);
				return true;
			}
			if (count4 == 1)
			{
				base.AI.SelectCard(17881964);
				return true;
			}
			return false;
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x000F0018 File Offset: 0x000EE218
		private bool XyzChangeTactics()
		{
			return base.Bot.LifePoints > 500;
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x000F002C File Offset: 0x000EE22C
		private bool GoblindberghFirst()
		{
			foreach (ClientCard card in base.Bot.Hand.GetMonsters())
			{
				if (!card.Equals(base.Card) && card.Level == 4)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x000F00A0 File Offset: 0x000EE2A0
		private bool GoblindberghEffect()
		{
			base.AI.SelectCard(new int[] { 30914564, 34143852, 24610207, 423585 });
			return true;
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x000F00C0 File Offset: 0x000EE2C0
		private bool KagetokageEffect()
		{
			ClientCard lastChainCard = base.Util.GetLastChainCard();
			return lastChainCard == null || !lastChainCard.IsCode(new int[] { 25259669, 18063928 });
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x000F0100 File Offset: 0x000EE300
		private bool SummonerMonkEffect()
		{
			IList<int> costs = new int[] { 11705261, 53129443, 5318639, 1845204 };
			if (base.Bot.HasInHand(costs))
			{
				base.AI.SelectCard(costs);
				base.AI.SelectNextCard(new int[] { 30914564, 24610207, 25259669, 18063928 });
				base.AI.SelectPosition(CardPosition.FaceUpDefence);
				return true;
			}
			return false;
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x000F0164 File Offset: 0x000EE364
		private bool SolarWindJammer()
		{
			if (!base.Bot.HasInHand(new int[] { 24610207, 1845204 }))
			{
				return false;
			}
			base.AI.SelectPosition(CardPosition.FaceUpDefence);
			return true;
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x000F0198 File Offset: 0x000EE398
		private bool MonsterRepos()
		{
			return (!base.Card.IsCode(56832966) || !base.Card.IsAttack()) && base.DefaultMonsterRepos();
		}

		// Token: 0x02000459 RID: 1113
		public class CardId
		{
			// Token: 0x04002657 RID: 9815
			public const int CyberDragon = 70095155;

			// Token: 0x04002658 RID: 9816
			public const int ZwTornadoBringer = 81471108;

			// Token: 0x04002659 RID: 9817
			public const int ZwLightningBlade = 45082499;

			// Token: 0x0400265A RID: 9818
			public const int ZwAsuraStrike = 40941889;

			// Token: 0x0400265B RID: 9819
			public const int SolarWindJammer = 33911264;

			// Token: 0x0400265C RID: 9820
			public const int PhotonTrasher = 65367484;

			// Token: 0x0400265D RID: 9821
			public const int StarDrawing = 24610207;

			// Token: 0x0400265E RID: 9822
			public const int SacredCrane = 30914564;

			// Token: 0x0400265F RID: 9823
			public const int Goblindbergh = 25259669;

			// Token: 0x04002660 RID: 9824
			public const int Honest = 37742478;

			// Token: 0x04002661 RID: 9825
			public const int Kagetokage = 94656263;

			// Token: 0x04002662 RID: 9826
			public const int HeroicChallengerExtraSword = 34143852;

			// Token: 0x04002663 RID: 9827
			public const int TinGoldfish = 18063928;

			// Token: 0x04002664 RID: 9828
			public const int SummonerMonk = 423585;

			// Token: 0x04002665 RID: 9829
			public const int InstantFusion = 1845204;

			// Token: 0x04002666 RID: 9830
			public const int Raigeki = 12580477;

			// Token: 0x04002667 RID: 9831
			public const int ReinforcementOfTheArmy = 32807846;

			// Token: 0x04002668 RID: 9832
			public const int DarkHole = 53129443;

			// Token: 0x04002669 RID: 9833
			public const int MysticalSpaceTyphoon = 5318639;

			// Token: 0x0400266A RID: 9834
			public const int BreakthroughSkill = 78474168;

			// Token: 0x0400266B RID: 9835
			public const int SolemnWarning = 84749824;

			// Token: 0x0400266C RID: 9836
			public const int SolemnStrike = 40605147;

			// Token: 0x0400266D RID: 9837
			public const int XyzChangeTactics = 11705261;

			// Token: 0x0400266E RID: 9838
			public const int FlameSwordsman = 45231177;

			// Token: 0x0400266F RID: 9839
			public const int DarkfireDragon = 17881964;

			// Token: 0x04002670 RID: 9840
			public const int GaiaDragonTheThunderCharger = 91949988;

			// Token: 0x04002671 RID: 9841
			public const int ZwLionArms = 60992364;

			// Token: 0x04002672 RID: 9842
			public const int AdreusKeeperOfArmageddon = 94119480;

			// Token: 0x04002673 RID: 9843
			public const int Number61Volcasaurus = 29669359;

			// Token: 0x04002674 RID: 9844
			public const int GemKnightPearl = 71594310;

			// Token: 0x04002675 RID: 9845
			public const int Number39Utopia = 84013237;

			// Token: 0x04002676 RID: 9846
			public const int NumberS39UtopiaOne = 86532744;

			// Token: 0x04002677 RID: 9847
			public const int NumberS39UtopiatheLightning = 56832966;

			// Token: 0x04002678 RID: 9848
			public const int MaestrokeTheSymphonyDjinn = 25341652;

			// Token: 0x04002679 RID: 9849
			public const int GagagaCowboy = 12014404;
		}
	}
}
