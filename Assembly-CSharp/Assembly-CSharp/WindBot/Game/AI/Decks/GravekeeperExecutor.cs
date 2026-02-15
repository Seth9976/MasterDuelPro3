using System;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x0200030E RID: 782
	[Deck("Gravekeeper", "AI_Gravekeeper", "NotFinished")]
	public class GravekeeperExecutor : DefaultExecutor
	{
		// Token: 0x06001437 RID: 5175 RVA: 0x00070338 File Offset: 0x0006E538
		public GravekeeperExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(base.DefaultSpellSet));
			base.AddExecutor(ExecutorType.Activate, 1475311);
			base.AddExecutor(ExecutorType.Activate, 53129443, new Func<bool>(base.DefaultDarkHole));
			base.AddExecutor(ExecutorType.Activate, 72405967);
			base.AddExecutor(ExecutorType.Activate, 99523325);
			base.AddExecutor(ExecutorType.Activate, 5318639, new Func<bool>(base.DefaultMysticalSpaceTyphoon));
			base.AddExecutor(ExecutorType.Activate, 14087893, new Func<bool>(base.DefaultBookOfMoon));
			base.AddExecutor(ExecutorType.Activate, 70000776, new Func<bool>(this.HiddenTemplesOfNecrovalleyEffect));
			base.AddExecutor(ExecutorType.Activate, 47355498, new Func<bool>(this.NecrovalleyActivate));
			base.AddExecutor(ExecutorType.Activate, 29401950, new Func<bool>(base.DefaultUniqueTrap));
			base.AddExecutor(ExecutorType.Activate, 84749824, new Func<bool>(base.DefaultSolemnWarning));
			base.AddExecutor(ExecutorType.Activate, 70342110, new Func<bool>(base.DefaultUniqueTrap));
			base.AddExecutor(ExecutorType.Activate, 30450531, new Func<bool>(base.DefaultUniqueTrap));
			base.AddExecutor(ExecutorType.Activate, 90434657, new Func<bool>(base.DefaultUniqueTrap));
			base.AddExecutor(ExecutorType.Activate, 53582587, new Func<bool>(base.DefaultTorrentialTribute));
			base.AddExecutor(ExecutorType.Summon, 25524823);
			base.AddExecutor(ExecutorType.SpSummon, 36521459, new Func<bool>(this.MaleficStardustDragonSummon));
			base.AddExecutor(ExecutorType.Summon, 3825890);
			base.AddExecutor(ExecutorType.Summon, 62473983);
			base.AddExecutor(ExecutorType.Summon, 71564252);
			base.AddExecutor(ExecutorType.Summon, 17393207, new Func<bool>(this.GravekeepersCommandantSummon));
			base.AddExecutor(ExecutorType.Summon, 25262697);
			base.AddExecutor(ExecutorType.Summon, 30213599);
			base.AddExecutor(ExecutorType.MonsterSet, 24317029);
			base.AddExecutor(ExecutorType.MonsterSet, 93023479);
			base.AddExecutor(ExecutorType.Activate, 25524823);
			base.AddExecutor(ExecutorType.Activate, 3825890);
			base.AddExecutor(ExecutorType.Activate, 62473983);
			base.AddExecutor(ExecutorType.Activate, 17393207, new Func<bool>(this.GravekeepersCommandantEffect));
			base.AddExecutor(ExecutorType.Activate, 25262697, new Func<bool>(this.GravekeepersAssailantEffect));
			base.AddExecutor(ExecutorType.Activate, 30213599, new Func<bool>(this.GravekeepersDescendantEffect));
			base.AddExecutor(ExecutorType.Activate, 24317029, new Func<bool>(this.SearchForDescendant));
			base.AddExecutor(ExecutorType.Activate, 93023479, new Func<bool>(this.SearchForDescendant));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(base.DefaultMonsterRepos));
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x000705CB File Offset: 0x0006E7CB
		private bool HiddenTemplesOfNecrovalleyEffect()
		{
			return base.Card.Location != CardLocation.Hand || !base.Bot.HasInSpellZone(base.Card.Id, false, false);
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x000705F8 File Offset: 0x0006E7F8
		private bool NecrovalleyActivate()
		{
			return base.Bot.SpellZone[5] == null;
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x0007060C File Offset: 0x0006E80C
		private bool MaleficStardustDragonSummon()
		{
			return base.Bot.SpellZone[5] != null;
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x00070620 File Offset: 0x0006E820
		private bool GravekeepersCommandantEffect()
		{
			return !base.Bot.HasInHand(47355498) && !base.Bot.HasInSpellZone(47355498, false, false);
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x0007064B File Offset: 0x0006E84B
		private bool GravekeepersCommandantSummon()
		{
			return !this.GravekeepersCommandantEffect();
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x00070658 File Offset: 0x0006E858
		private bool GravekeepersAssailantEffect()
		{
			if (!base.Card.IsAttack())
			{
				return false;
			}
			foreach (ClientCard card in base.Enemy.GetMonsters())
			{
				if ((card.IsDefense() && card.Defense > 1500 && card.Attack < 1500) || (card.Attack > 1500 && card.Defense < 1500))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x000706FC File Offset: 0x0006E8FC
		private bool GravekeepersDescendantEffect()
		{
			int bestatk = base.Bot.GetMonsters().GetHighestAttackMonster(false).Attack;
			if (base.Util.IsOneEnemyBetterThanValue(bestatk, true))
			{
				base.AI.SelectCard(base.Enemy.GetMonsters().GetHighestAttackMonster(false));
				return true;
			}
			return false;
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x0007074E File Offset: 0x0006E94E
		private bool SearchForDescendant()
		{
			base.AI.SelectCard(30213599);
			return true;
		}

		// Token: 0x0200030F RID: 783
		public class CardId
		{
			// Token: 0x040018A6 RID: 6310
			public const int GravekeepersOracle = 25524823;

			// Token: 0x040018A7 RID: 6311
			public const int MaleficStardustDragon = 36521459;

			// Token: 0x040018A8 RID: 6312
			public const int GravekeepersVisionary = 3825890;

			// Token: 0x040018A9 RID: 6313
			public const int GravekeepersChief = 62473983;

			// Token: 0x040018AA RID: 6314
			public const int ThunderKingRaiOh = 71564252;

			// Token: 0x040018AB RID: 6315
			public const int GravekeepersCommandant = 17393207;

			// Token: 0x040018AC RID: 6316
			public const int GravekeepersAssailant = 25262697;

			// Token: 0x040018AD RID: 6317
			public const int GravekeepersDescendant = 30213599;

			// Token: 0x040018AE RID: 6318
			public const int GravekeepersSpy = 24317029;

			// Token: 0x040018AF RID: 6319
			public const int GravekeepersRecruiter = 93023479;

			// Token: 0x040018B0 RID: 6320
			public const int AllureOfDarkness = 1475311;

			// Token: 0x040018B1 RID: 6321
			public const int DarkHole = 53129443;

			// Token: 0x040018B2 RID: 6322
			public const int RoyalTribute = 72405967;

			// Token: 0x040018B3 RID: 6323
			public const int GravekeepersStele = 99523325;

			// Token: 0x040018B4 RID: 6324
			public const int MysticalSpaceTyphoon = 5318639;

			// Token: 0x040018B5 RID: 6325
			public const int BookofMoon = 14087893;

			// Token: 0x040018B6 RID: 6326
			public const int HiddenTemplesOfNecrovalley = 70000776;

			// Token: 0x040018B7 RID: 6327
			public const int Necrovalley = 47355498;

			// Token: 0x040018B8 RID: 6328
			public const int BottomlessTrapHole = 29401950;

			// Token: 0x040018B9 RID: 6329
			public const int RiteOfSpirit = 30450531;

			// Token: 0x040018BA RID: 6330
			public const int TorrentialTribute = 53582587;

			// Token: 0x040018BB RID: 6331
			public const int DimensionalPrison = 70342110;

			// Token: 0x040018BC RID: 6332
			public const int SolemnWarning = 84749824;

			// Token: 0x040018BD RID: 6333
			public const int ImperialTombsOfNecrovalley = 90434657;
		}
	}
}
