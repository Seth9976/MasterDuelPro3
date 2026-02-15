using System;
using System.Collections.Generic;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x020002D5 RID: 725
	[Deck("Test", "AI_Test", "Test")]
	public class DoEverythingExecutor : DefaultExecutor
	{
		// Token: 0x060011E9 RID: 4585 RVA: 0x0005E080 File Offset: 0x0005C280
		public DoEverythingExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.SpSummon);
			base.AddExecutor(ExecutorType.Activate, new Func<bool>(base.DefaultDontChainMyself));
			base.AddExecutor(ExecutorType.SummonOrSet, new Func<bool>(base.DefaultMonsterSummon));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(base.DefaultMonsterRepos));
			base.AddExecutor(ExecutorType.SpellSet);
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x0005E0DC File Offset: 0x0005C2DC
		public override IList<ClientCard> OnSelectCard(IList<ClientCard> cards, int min, int max, int hint, bool cancelable)
		{
			if (base.Duel.Phase == DuelPhase.BattleStart)
			{
				return null;
			}
			IList<ClientCard> selected = new List<ClientCard>();
			for (int i = 1; i <= max; i++)
			{
				selected.Add(cards[cards.Count - i]);
			}
			return selected;
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x0005E120 File Offset: 0x0005C320
		public override int OnSelectOption(IList<int> options)
		{
			return Program.Rand.Next(options.Count);
		}

		// Token: 0x020002D6 RID: 726
		public class CardId
		{
			// Token: 0x040016B9 RID: 5817
			public const int LeoWizard = 4392470;

			// Token: 0x040016BA RID: 5818
			public const int Bunilla = 69380702;
		}
	}
}
