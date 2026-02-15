using System;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x02000385 RID: 901
	[Deck("MokeyMokey", "AI_MokeyMokey", "Easy")]
	public class MokeyMokeyExecutor : DefaultExecutor
	{
		// Token: 0x06001A94 RID: 6804 RVA: 0x0009CD10 File Offset: 0x0009AF10
		public MokeyMokeyExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Summon);
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(base.DefaultMonsterRepos));
			base.AddExecutor(ExecutorType.SpellSet);
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x0009CD3B File Offset: 0x0009AF3B
		public override int OnRockPaperScissors()
		{
			this.RockCount++;
			if (this.RockCount <= 3)
			{
				return 2;
			}
			return base.OnRockPaperScissors();
		}

		// Token: 0x04001DD3 RID: 7635
		private int RockCount;

		// Token: 0x02000386 RID: 902
		public class CardId
		{
			// Token: 0x04001DD4 RID: 7636
			public const int LeoWizard = 4392470;

			// Token: 0x04001DD5 RID: 7637
			public const int Bunilla = 69380702;
		}
	}
}
