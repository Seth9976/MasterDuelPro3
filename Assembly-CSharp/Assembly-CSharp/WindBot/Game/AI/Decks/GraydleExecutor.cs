using System;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x02000310 RID: 784
	[Deck("Graydle", "AI_Graydle", "NotFinished")]
	public class GraydleExecutor : DefaultExecutor
	{
		// Token: 0x06001441 RID: 5185 RVA: 0x00070764 File Offset: 0x0006E964
		public GraydleExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 53129443, new Func<bool>(base.DefaultDarkHole));
			base.AddExecutor(ExecutorType.Activate, 8267140, new Func<bool>(base.DefaultCosmicCyclone));
			base.AddExecutor(ExecutorType.Activate, 41420027, new Func<bool>(base.DefaultSolemnJudgment));
			base.AddExecutor(ExecutorType.Activate, 84749824, new Func<bool>(base.DefaultSolemnWarning));
			base.AddExecutor(ExecutorType.Activate, 40605147, new Func<bool>(base.DefaultSolemnStrike));
			base.AddExecutor(ExecutorType.Activate, new Func<bool>(base.DefaultDontChainMyself));
			base.AddExecutor(ExecutorType.MonsterSet);
			base.AddExecutor(ExecutorType.SpSummon);
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(base.DefaultMonsterRepos));
			base.AddExecutor(ExecutorType.SpellSet);
		}

		// Token: 0x02000311 RID: 785
		public class CardId
		{
			// Token: 0x040018BE RID: 6334
			public const int DarkHole = 53129443;

			// Token: 0x040018BF RID: 6335
			public const int CosmicCyclone = 8267140;

			// Token: 0x040018C0 RID: 6336
			public const int SolemnJudgment = 41420027;

			// Token: 0x040018C1 RID: 6337
			public const int SolemnWarning = 84749824;

			// Token: 0x040018C2 RID: 6338
			public const int SolemnStrike = 40605147;
		}
	}
}
