using System;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x020002F8 RID: 760
	[Deck("Evilswarm", "AI_Evilswarm", "NotFinished")]
	public class EvilswarmExecutor : DefaultExecutor
	{
		// Token: 0x0600132F RID: 4911 RVA: 0x00067854 File Offset: 0x00065A54
		public EvilswarmExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 53129443, new Func<bool>(base.DefaultDarkHole));
			base.AddExecutor(ExecutorType.Activate, 8267140, new Func<bool>(base.DefaultCosmicCyclone));
			base.AddExecutor(ExecutorType.Activate, 41420027, new Func<bool>(base.DefaultSolemnJudgment));
			base.AddExecutor(ExecutorType.Activate, 84749824, new Func<bool>(base.DefaultSolemnWarning));
			base.AddExecutor(ExecutorType.Activate, 40605147, new Func<bool>(base.DefaultSolemnStrike));
			base.AddExecutor(ExecutorType.SpellSet, 27541267);
			base.AddExecutor(ExecutorType.Activate, new Func<bool>(base.DefaultDontChainMyself));
			base.AddExecutor(ExecutorType.Summon);
			base.AddExecutor(ExecutorType.SpSummon);
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(base.DefaultMonsterRepos));
			base.AddExecutor(ExecutorType.SpellSet);
		}

		// Token: 0x020002F9 RID: 761
		public class CardId
		{
			// Token: 0x040017C9 RID: 6089
			public const int DarkHole = 53129443;

			// Token: 0x040017CA RID: 6090
			public const int CosmicCyclone = 8267140;

			// Token: 0x040017CB RID: 6091
			public const int InfestationPandemic = 27541267;

			// Token: 0x040017CC RID: 6092
			public const int SolemnJudgment = 41420027;

			// Token: 0x040017CD RID: 6093
			public const int SolemnWarning = 84749824;

			// Token: 0x040017CE RID: 6094
			public const int SolemnStrike = 40605147;
		}
	}
}
