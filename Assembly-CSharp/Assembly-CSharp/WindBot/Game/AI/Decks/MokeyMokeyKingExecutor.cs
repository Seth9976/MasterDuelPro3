using System;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x02000387 RID: 903
	[Deck("MokeyMokeyKing", "AI_MokeyMokeyKing", "Easy")]
	public class MokeyMokeyKingExecutor : DefaultExecutor
	{
		// Token: 0x06001A97 RID: 6807 RVA: 0x0009CD5C File Offset: 0x0009AF5C
		public MokeyMokeyKingExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.SpSummon);
			base.AddExecutor(ExecutorType.SummonOrSet);
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(base.DefaultMonsterRepos));
			base.AddExecutor(ExecutorType.Activate, new Func<bool>(base.DefaultField));
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x0009CD9A File Offset: 0x0009AF9A
		public override int OnRockPaperScissors()
		{
			this.RockCount++;
			if (this.RockCount <= 3)
			{
				return 2;
			}
			return base.OnRockPaperScissors();
		}

		// Token: 0x04001DD6 RID: 7638
		private int RockCount;

		// Token: 0x02000388 RID: 904
		public class CardId
		{
			// Token: 0x04001DD7 RID: 7639
			public const int LeoWizard = 4392470;

			// Token: 0x04001DD8 RID: 7640
			public const int Bunilla = 69380702;
		}
	}
}
