using System;
using System.Collections.Generic;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x0200030C RID: 780
	[Deck("Frog", "AI_Frog", "Easy")]
	public class FrogExecutor : DefaultExecutor
	{
		// Token: 0x06001428 RID: 5160 RVA: 0x0006FB78 File Offset: 0x0006DD78
		public FrogExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(base.DefaultSpellSet));
			base.AddExecutor(ExecutorType.Activate, 86780027, new Func<bool>(this.Solidarity));
			base.AddExecutor(ExecutorType.Activate, 73628505, new Func<bool>(this.Terraforming));
			base.AddExecutor(ExecutorType.Activate, 2084239, new Func<bool>(base.DefaultField));
			base.AddExecutor(ExecutorType.Activate, 53129443, new Func<bool>(base.DefaultDarkHole));
			base.AddExecutor(ExecutorType.Activate, 12580477, new Func<bool>(base.DefaultRaigeki));
			base.AddExecutor(ExecutorType.Activate, 98645731, new Func<bool>(this.PotOfDuality));
			base.AddExecutor(ExecutorType.SpSummon, 9126351, new Func<bool>(this.SwapFrogSummon));
			base.AddExecutor(ExecutorType.Activate, 9126351, new Func<bool>(this.SwapFrogActivate));
			base.AddExecutor(ExecutorType.Activate, 46239604, new Func<bool>(this.DupeFrog));
			base.AddExecutor(ExecutorType.Activate, 81278754, new Func<bool>(this.FlipFlopFrog));
			base.AddExecutor(ExecutorType.Activate, 1357146, new Func<bool>(this.Ronintoadin));
			base.AddExecutor(ExecutorType.Activate, 12538374, new Func<bool>(this.TreebornFrog));
			base.AddExecutor(ExecutorType.Activate, 56052205);
			base.AddExecutor(ExecutorType.Summon, 23950192, new Func<bool>(this.SummonFrog));
			base.AddExecutor(ExecutorType.Summon, 90311614, new Func<bool>(this.SummonFrog));
			base.AddExecutor(ExecutorType.Summon, 63948258, new Func<bool>(this.SummonFrog));
			base.AddExecutor(ExecutorType.Summon, 9126351, new Func<bool>(this.SummonFrog));
			base.AddExecutor(ExecutorType.Summon, 56052205, new Func<bool>(this.SummonFrog));
			base.AddExecutor(ExecutorType.Summon, 1357146, new Func<bool>(this.SummonFrog));
			base.AddExecutor(ExecutorType.Summon, 46239604, new Func<bool>(this.SummonFrog));
			base.AddExecutor(ExecutorType.Summon, 23408872, new Func<bool>(this.SummonFrog));
			base.AddExecutor(ExecutorType.Summon, 12538374, new Func<bool>(this.SummonFrog));
			base.AddExecutor(ExecutorType.Summon, 81278754, new Func<bool>(this.SummonFrog));
			base.AddExecutor(ExecutorType.MonsterSet, 81278754);
			base.AddExecutor(ExecutorType.MonsterSet, 46239604);
			base.AddExecutor(ExecutorType.MonsterSet, 23408872);
			base.AddExecutor(ExecutorType.MonsterSet, 1357146);
			base.AddExecutor(ExecutorType.MonsterSet, 12538374);
			base.AddExecutor(ExecutorType.MonsterSet, 56052205);
			base.AddExecutor(ExecutorType.MonsterSet, 9126351);
			base.AddExecutor(ExecutorType.MonsterSet, 63948258);
			base.AddExecutor(ExecutorType.MonsterSet, 90311614);
			base.AddExecutor(ExecutorType.MonsterSet, 23950192);
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(this.FrogMonsterRepos));
			base.AddExecutor(ExecutorType.Activate, 34351849, new Func<bool>(base.DefaultTrap));
			base.AddExecutor(ExecutorType.Activate, 99188141, new Func<bool>(base.DefaultTrap));
			base.AddExecutor(ExecutorType.Activate, 85742772, new Func<bool>(this.GravityBind));
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x0000763C File Offset: 0x0000583C
		private bool TreebornFrog()
		{
			return true;
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x0006FE90 File Offset: 0x0006E090
		private bool SwapFrogSummon()
		{
			int atk = base.Card.Attack + this.GetSpellBonus();
			if (base.Util.IsAllEnemyBetterThanValue(atk, true))
			{
				return false;
			}
			base.AI.SelectCard(1357146);
			this.m_swapFrogSummoned = base.Duel.Turn;
			return true;
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x0006FEE4 File Offset: 0x0006E0E4
		private bool SwapFrogActivate()
		{
			if (this.m_swapFrogSummoned != base.Duel.Turn)
			{
				return false;
			}
			this.m_swapFrogSummoned = -1;
			if (base.Bot.GetRemainingCount(1357146, 2) == 0)
			{
				return false;
			}
			base.AI.SelectCard(1357146);
			return true;
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x0006FF33 File Offset: 0x0006E133
		private bool DupeFrog()
		{
			base.AI.SelectCard(CardLocation.Deck);
			return true;
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x0006FF44 File Offset: 0x0006E144
		private bool FlipFlopFrog()
		{
			if (base.Card.IsDefense() || this.m_flipFlopFrogSummoned == base.Duel.Turn || base.Duel.Phase == DuelPhase.Main2)
			{
				this.m_flipFlopFrogSummoned = -1;
				List<ClientCard> monsters = base.Enemy.GetMonsters();
				monsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				monsters.Reverse();
				base.AI.SelectCard(monsters);
				return true;
			}
			return false;
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x0006FFBD File Offset: 0x0006E1BD
		private bool Ronintoadin()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (base.Bot.GetGraveyardMonsters().Count > 2)
			{
				if (this.GetSpellBonus() == 0)
				{
					base.AI.SelectPosition(CardPosition.FaceUpDefence);
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x0006FFFC File Offset: 0x0006E1FC
		private bool SummonFrog()
		{
			int atk = base.Card.Attack + this.GetSpellBonus();
			if (base.Util.IsOneEnemyBetterThanValue(atk, true))
			{
				return false;
			}
			if (base.Card.IsCode(9126351))
			{
				this.m_swapFrogSummoned = base.Duel.Turn;
			}
			return true;
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x00070054 File Offset: 0x0006E254
		private bool PotOfDuality()
		{
			List<int> cards = new List<int>();
			if (base.Util.IsOneEnemyBetter(false))
			{
				cards.Add(81278754);
			}
			if (base.Bot.SpellZone[5] == null)
			{
				cards.Add(73628505);
				cards.Add(2084239);
			}
			cards.Add(53129443);
			cards.Add(9126351);
			cards.Add(85742772);
			if (cards.Count > 0)
			{
				base.AI.SelectCard(cards);
				return true;
			}
			return false;
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x000700DE File Offset: 0x0006E2DE
		private bool Terraforming()
		{
			return !base.Bot.HasInHand(2084239) && base.Bot.SpellZone[5] == null;
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x00070106 File Offset: 0x0006E306
		private bool Solidarity()
		{
			return base.Bot.GetGraveyardMonsters().Count != 0;
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x0007011C File Offset: 0x0006E31C
		private bool GravityBind()
		{
			foreach (ClientCard spell in base.Bot.GetSpells())
			{
				if (spell.IsCode(85742772) && !spell.IsFacedown())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x0007018C File Offset: 0x0006E38C
		private bool FrogMonsterRepos()
		{
			if (base.Card.IsCode(56052205))
			{
				return base.Card.IsDefense();
			}
			if (base.Card.IsCode(90311614))
			{
				return base.Card.IsDefense();
			}
			bool enemyBetter = base.Util.IsOneEnemyBetterThanValue(base.Card.Attack + (base.Card.IsFacedown() ? this.GetSpellBonus() : 0), true);
			if (base.Card.Attack < 800)
			{
				enemyBetter = true;
			}
			bool result = false;
			if (base.Card.IsAttack() && enemyBetter)
			{
				result = true;
			}
			if (base.Card.IsDefense() && !enemyBetter)
			{
				result = true;
			}
			if (!result && base.Card.IsCode(81278754) && base.Enemy.GetMonsterCount() > 0 && base.Card.IsFacedown())
			{
				result = true;
			}
			if (base.Card.IsCode(81278754) && base.Card.IsFacedown() && result)
			{
				this.m_flipFlopFrogSummoned = base.Duel.Turn;
			}
			return result;
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x000702A8 File Offset: 0x0006E4A8
		private int GetSpellBonus()
		{
			int atk = 0;
			if (base.Bot.SpellZone[5] != null)
			{
				atk += 1200;
			}
			if (base.Bot.GetGraveyardMonsters().Count != 0)
			{
				using (List<ClientCard>.Enumerator enumerator = base.Bot.GetSpells().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.IsCode(86780027))
						{
							atk += 800;
						}
					}
				}
			}
			return atk;
		}

		// Token: 0x04001891 RID: 6289
		private int m_swapFrogSummoned;

		// Token: 0x04001892 RID: 6290
		private int m_flipFlopFrogSummoned;

		// Token: 0x0200030D RID: 781
		public class CardId
		{
			// Token: 0x04001893 RID: 6291
			public const int CryomancerOfTheIceBarrier = 23950192;

			// Token: 0x04001894 RID: 6292
			public const int DewdarkOfTheIceBarrier = 90311614;

			// Token: 0x04001895 RID: 6293
			public const int SubmarineFrog = 63948258;

			// Token: 0x04001896 RID: 6294
			public const int SwapFrog = 9126351;

			// Token: 0x04001897 RID: 6295
			public const int FlipFlopFrog = 81278754;

			// Token: 0x04001898 RID: 6296
			public const int Unifrog = 56052205;

			// Token: 0x04001899 RID: 6297
			public const int Ronintoadin = 1357146;

			// Token: 0x0400189A RID: 6298
			public const int DupeFrog = 46239604;

			// Token: 0x0400189B RID: 6299
			public const int Tradetoad = 23408872;

			// Token: 0x0400189C RID: 6300
			public const int TreebornFrog = 12538374;

			// Token: 0x0400189D RID: 6301
			public const int DarkHole = 53129443;

			// Token: 0x0400189E RID: 6302
			public const int Raigeki = 12580477;

			// Token: 0x0400189F RID: 6303
			public const int Terraforming = 73628505;

			// Token: 0x040018A0 RID: 6304
			public const int PotOfDuality = 98645731;

			// Token: 0x040018A1 RID: 6305
			public const int Solidarity = 86780027;

			// Token: 0x040018A2 RID: 6306
			public const int Wetlands = 2084239;

			// Token: 0x040018A3 RID: 6307
			public const int FroggyForcefield = 34351849;

			// Token: 0x040018A4 RID: 6308
			public const int GravityBind = 85742772;

			// Token: 0x040018A5 RID: 6309
			public const int TheHugeRevolutionIsOver = 99188141;
		}
	}
}
