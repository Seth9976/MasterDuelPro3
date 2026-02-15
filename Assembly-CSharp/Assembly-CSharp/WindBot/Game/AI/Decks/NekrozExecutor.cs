using System;
using System.Collections.Generic;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x02000389 RID: 905
	[Deck("Nekroz", "AI_Nekroz", "NotFinished")]
	public class NekrozExecutor : DefaultExecutor
	{
		// Token: 0x06001A9A RID: 6810 RVA: 0x0009CDBC File Offset: 0x0009AFBC
		public NekrozExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			this.NekrozRituelCard.Add(99185129);
			this.NekrozRituelCard.Add(89463537);
			this.NekrozRituelCard.Add(88240999);
			this.NekrozRituelCard.Add(26674724);
			this.NekrozRituelCard.Add(52068432);
			this.NekrozRituelCard.Add(74122412);
			this.NekrozRituelCard.Add(25857246);
			this.NekrozSpellCard.Add(14735698);
			this.NekrozSpellCard.Add(51124303);
			this.NekrozSpellCard.Add(97211663);
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(base.DefaultSpellSet));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(base.DefaultMonsterRepos));
			base.AddExecutor(ExecutorType.Activate, 53129443, new Func<bool>(base.DefaultDarkHole));
			base.AddExecutor(ExecutorType.Activate, 32807846, new Func<bool>(this.ReinforcementOfTheArmyEffect));
			base.AddExecutor(ExecutorType.Activate, 38120068);
			base.AddExecutor(ExecutorType.Activate, 96729612);
			base.AddExecutor(ExecutorType.Activate, 14735698);
			base.AddExecutor(ExecutorType.Activate, 51124303);
			base.AddExecutor(ExecutorType.Activate, 97211663);
			base.AddExecutor(ExecutorType.Activate, 5318639, new Func<bool>(base.DefaultMysticalSpaceTyphoon));
			base.AddExecutor(ExecutorType.Activate, 51452091);
			base.AddExecutor(ExecutorType.SummonOrSet, 52738610, new Func<bool>(this.DancePrincessSummon));
			base.AddExecutor(ExecutorType.MonsterSet, 90307777, new Func<bool>(this.ShuritSet));
			base.AddExecutor(ExecutorType.Summon, 23401839, new Func<bool>(this.ThousandHandsSummon));
			base.AddExecutor(ExecutorType.Summon, 95492061, new Func<bool>(this.TenThousandHandsSummon));
			base.AddExecutor(ExecutorType.Summon, 30312361, new Func<bool>(this.PhantomOfChaosSummon));
			base.AddExecutor(ExecutorType.Activate, 89463537, new Func<bool>(this.UnicoreEffect));
			base.AddExecutor(ExecutorType.Activate, 88240999, new Func<bool>(this.DecisiveArmorEffect));
			base.AddExecutor(ExecutorType.Activate, 25857246, new Func<bool>(this.ValkyrusEffect));
			base.AddExecutor(ExecutorType.Activate, 74122412, new Func<bool>(this.GungnirEffect));
			base.AddExecutor(ExecutorType.Activate, 26674724, new Func<bool>(this.BrionacEffect));
			base.AddExecutor(ExecutorType.Activate, 99185129, new Func<bool>(this.ClausolasEffect));
			base.AddExecutor(ExecutorType.Activate, 52068432);
			base.AddExecutor(ExecutorType.Activate, 46772449, new Func<bool>(base.DefaultEvilswarmExcitonKnightEffect));
			base.AddExecutor(ExecutorType.Activate, 30312361, new Func<bool>(this.PhantomOfChaosEffect));
			base.AddExecutor(ExecutorType.Activate, 23434538);
			base.AddExecutor(ExecutorType.Activate, 23401839, new Func<bool>(this.ThousandHandsEffect));
			base.AddExecutor(ExecutorType.Activate, 95492061, new Func<bool>(this.BrionacEffect));
			base.AddExecutor(ExecutorType.Activate, 79606837);
			base.AddExecutor(ExecutorType.Activate, 90307777);
			base.AddExecutor(ExecutorType.SpSummon, 52068432);
			base.AddExecutor(ExecutorType.SpSummon, 88240999);
			base.AddExecutor(ExecutorType.SpSummon, 25857246);
			base.AddExecutor(ExecutorType.SpSummon, 74122412);
			base.AddExecutor(ExecutorType.SpSummon, 26674724);
			base.AddExecutor(ExecutorType.SpSummon, 89463537);
			base.AddExecutor(ExecutorType.SpSummon, 99185129);
			base.AddExecutor(ExecutorType.SpSummon, 46772449, new Func<bool>(base.DefaultEvilswarmExcitonKnightSummon));
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x0009D144 File Offset: 0x0009B344
		private bool ThousandHandsSummon()
		{
			if (!base.Bot.HasInHand(this.NekrozRituelCard) || base.Bot.HasInHand(90307777) || !base.Bot.HasInHand(this.NekrozSpellCard))
			{
				return true;
			}
			foreach (ClientCard Card in base.Bot.Hand)
			{
				if (Card != null && Card.IsCode(51124303) && !base.Bot.HasInHand(89463537))
				{
					return true;
				}
				if (Card.IsCode(52068432) || (Card.IsCode(88240999) && !base.Bot.HasInHand(14735698)) || !base.Bot.HasInHand(90307777))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x0009D234 File Offset: 0x0009B434
		private bool ReinforcementOfTheArmyEffect()
		{
			if (!base.Bot.HasInGraveyard(90307777) && !base.Bot.HasInHand(90307777))
			{
				base.AI.SelectCard(90307777);
				return true;
			}
			return false;
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x0009D26D File Offset: 0x0009B46D
		private bool TenThousandHandsSummon()
		{
			return !base.Bot.HasInHand(23401839) || !base.Bot.HasInHand(90307777);
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x0009D296 File Offset: 0x0009B496
		private bool DancePrincessSummon()
		{
			return !base.Bot.HasInHand(23401839) && !base.Bot.HasInHand(95492061);
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x0009D2BF File Offset: 0x0009B4BF
		private bool PhantomOfChaosSummon()
		{
			return base.Bot.HasInGraveyard(90307777) && base.Bot.HasInHand(this.NekrozSpellCard) && base.Bot.HasInHand(this.NekrozRituelCard);
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x0009D2FC File Offset: 0x0009B4FC
		private bool PhantomOfChaosEffect()
		{
			base.AI.SelectCard(90307777);
			return true;
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x0009D30F File Offset: 0x0009B50F
		private bool ShuritSet()
		{
			return !base.Bot.HasInHand(23401839) && !base.Bot.HasInHand(95492061) && !base.Bot.HasInHand(52738610);
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x0009D34A File Offset: 0x0009B54A
		private bool DecisiveArmorEffect()
		{
			if (base.Util.IsAllEnemyBetterThanValue(3300, true))
			{
				base.AI.SelectCard(88240999);
				return true;
			}
			return false;
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x0009D372 File Offset: 0x0009B572
		private bool ValkyrusEffect()
		{
			return base.Duel.Phase == DuelPhase.Battle;
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x0009D389 File Offset: 0x0009B589
		private bool GungnirEffect()
		{
			if (base.Util.IsOneEnemyBetter(true) && base.Duel.Phase == DuelPhase.Main1)
			{
				base.AI.SelectCard(base.Enemy.GetMonsters().GetHighestAttackMonster(false));
				return true;
			}
			return false;
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x0009D3C8 File Offset: 0x0009B5C8
		private bool BrionacEffect()
		{
			if (!base.Bot.HasInHand(90307777))
			{
				base.AI.SelectCard(90307777);
				return true;
			}
			if (!base.Bot.HasInHand(this.NekrozSpellCard))
			{
				base.AI.SelectCard(14735698);
				return true;
			}
			if (base.Util.IsOneEnemyBetterThanValue(3300, true) && !base.Bot.HasInHand(52068432))
			{
				base.AI.SelectCard(52068432);
				return true;
			}
			if (base.Util.IsAllEnemyBetterThanValue(2700, true) && !base.Bot.HasInHand(88240999))
			{
				base.AI.SelectCard(88240999);
				return true;
			}
			if (base.Bot.HasInHand(89463537) && !base.Bot.HasInHand(51124303))
			{
				base.AI.SelectCard(51124303);
				return true;
			}
			if (!base.Bot.HasInHand(89463537) && base.Bot.HasInHand(51124303))
			{
				base.AI.SelectCard(89463537);
				return true;
			}
			return true;
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x0009D4FC File Offset: 0x0009B6FC
		private bool ThousandHandsEffect()
		{
			if (base.Util.IsOneEnemyBetterThanValue(3300, true) && !base.Bot.HasInHand(52068432))
			{
				base.AI.SelectCard(52068432);
				return true;
			}
			if (base.Util.IsAllEnemyBetterThanValue(2700, true) && !base.Bot.HasInHand(88240999))
			{
				base.AI.SelectCard(88240999);
				return true;
			}
			if (!base.Bot.HasInHand(89463537) && base.Bot.HasInHand(51124303))
			{
				base.AI.SelectCard(89463537);
				return true;
			}
			return true;
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x0009D5AE File Offset: 0x0009B7AE
		private bool UnicoreEffect()
		{
			if (base.Bot.HasInGraveyard(90307777))
			{
				base.AI.SelectCard(90307777);
				return true;
			}
			return false;
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x0009D5D5 File Offset: 0x0009B7D5
		private bool ClausolasEffect()
		{
			if (!base.Bot.HasInHand(this.NekrozSpellCard))
			{
				base.AI.SelectCard(14735698);
				return true;
			}
			return false;
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x0009D5FD File Offset: 0x0009B7FD
		private bool IsTheLastPossibility()
		{
			return !base.Bot.HasInHand(88240999) && !base.Bot.HasInHand(52068432);
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x0009D628 File Offset: 0x0009B828
		private bool SelectNekrozWhoInvoke()
		{
			List<int> NekrozCard = new List<int>();
			bool flag;
			try
			{
				foreach (ClientCard card in base.Bot.Hand)
				{
					if (card != null && card.IsCode(this.NekrozRituelCard))
					{
						NekrozCard.Add(card.Id);
					}
				}
				foreach (int Id in NekrozCard)
				{
					if (Id == 52068432 && base.Util.IsAllEnemyBetterThanValue(2700, true) && base.Bot.HasInHand(88240999))
					{
						base.AI.SelectCard(52068432);
						return true;
					}
					if (Id == 88240999)
					{
						base.AI.SelectCard(88240999);
						return true;
					}
					if (Id == 89463537 && base.Bot.HasInHand(51124303) && !base.Bot.HasInGraveyard(90307777))
					{
						base.AI.SelectCard(89463537);
						return true;
					}
					if (Id == 25857246)
					{
						if (this.IsTheLastPossibility())
						{
							base.AI.SelectCard(25857246);
							return true;
						}
					}
					else if (Id == 74122412)
					{
						if (this.IsTheLastPossibility())
						{
							base.AI.SelectCard(74122412);
							return true;
						}
					}
					else if (Id == 99185129 && this.IsTheLastPossibility())
					{
						base.AI.SelectCard(99185129);
						return true;
					}
				}
				flag = false;
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x04001DD9 RID: 7641
		private List<int> NekrozRituelCard = new List<int>();

		// Token: 0x04001DDA RID: 7642
		private List<int> NekrozSpellCard = new List<int>();

		// Token: 0x0200038A RID: 906
		public class CardId
		{
			// Token: 0x04001DDB RID: 7643
			public const int DancePrincess = 52738610;

			// Token: 0x04001DDC RID: 7644
			public const int ThousandHands = 23401839;

			// Token: 0x04001DDD RID: 7645
			public const int TenThousandHands = 95492061;

			// Token: 0x04001DDE RID: 7646
			public const int Shurit = 90307777;

			// Token: 0x04001DDF RID: 7647
			public const int MaxxC = 23434538;

			// Token: 0x04001DE0 RID: 7648
			public const int DecisiveArmor = 88240999;

			// Token: 0x04001DE1 RID: 7649
			public const int Trishula = 52068432;

			// Token: 0x04001DE2 RID: 7650
			public const int Valkyrus = 25857246;

			// Token: 0x04001DE3 RID: 7651
			public const int Gungnir = 74122412;

			// Token: 0x04001DE4 RID: 7652
			public const int Brionac = 26674724;

			// Token: 0x04001DE5 RID: 7653
			public const int Unicore = 89463537;

			// Token: 0x04001DE6 RID: 7654
			public const int Clausolas = 99185129;

			// Token: 0x04001DE7 RID: 7655
			public const int PhantomOfChaos = 30312361;

			// Token: 0x04001DE8 RID: 7656
			public const int DarkHole = 53129443;

			// Token: 0x04001DE9 RID: 7657
			public const int ReinforcementOfTheArmy = 32807846;

			// Token: 0x04001DEA RID: 7658
			public const int TradeIn = 38120068;

			// Token: 0x04001DEB RID: 7659
			public const int PreparationOfRites = 96729612;

			// Token: 0x04001DEC RID: 7660
			public const int Mirror = 14735698;

			// Token: 0x04001DED RID: 7661
			public const int Kaleidoscope = 51124303;

			// Token: 0x04001DEE RID: 7662
			public const int Cycle = 97211663;

			// Token: 0x04001DEF RID: 7663
			public const int MysticalSpaceTyphoon = 5318639;

			// Token: 0x04001DF0 RID: 7664
			public const int RoyalDecree = 51452091;

			// Token: 0x04001DF1 RID: 7665
			public const int EvilswarmExcitonKnight = 46772449;

			// Token: 0x04001DF2 RID: 7666
			public const int HeraldOfTheArcLight = 79606837;
		}
	}
}
