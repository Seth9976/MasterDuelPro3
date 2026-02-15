using System;
using System.Collections.Generic;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x020002CD RID: 717
	[Deck("Burn", "AI_Burn", "Easy")]
	public class BurnExecutor : DefaultExecutor
	{
		// Token: 0x0600116D RID: 4461 RVA: 0x00057DF0 File Offset: 0x00055FF0
		public BurnExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(base.DefaultSpellSet));
			base.AddExecutor(ExecutorType.Activate, 85562745);
			base.AddExecutor(ExecutorType.Activate, 19523799);
			base.AddExecutor(ExecutorType.Activate, 45311864);
			base.AddExecutor(ExecutorType.Activate, 46918794);
			base.AddExecutor(ExecutorType.Activate, 72302403, new Func<bool>(this.SwordsOfRevealingLight));
			base.AddExecutor(ExecutorType.Activate, 98380593, new Func<bool>(this.SupremacyBerry));
			base.AddExecutor(ExecutorType.Activate, 8842266, new Func<bool>(this.PoisonOfTheOldMan));
			base.AddExecutor(ExecutorType.Activate, 20264508, new Func<bool>(this.ThunderShort));
			base.AddExecutor(ExecutorType.SpSummon, 102380, new Func<bool>(this.LavaGolem));
			base.AddExecutor(ExecutorType.MonsterSet, 31305911, new Func<bool>(this.SetInvincibleMonster));
			base.AddExecutor(ExecutorType.MonsterSet, 23205979, new Func<bool>(this.SetInvincibleMonster));
			base.AddExecutor(ExecutorType.MonsterSet, 26302522);
			base.AddExecutor(ExecutorType.SummonOrSet, 97396380);
			base.AddExecutor(ExecutorType.Summon, 2851070);
			base.AddExecutor(ExecutorType.MonsterSet, 44789585);
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(this.ReposEverything));
			base.AddExecutor(ExecutorType.Activate, 62279055, new Func<bool>(base.DefaultTrap));
			base.AddExecutor(ExecutorType.Activate, 36468556, new Func<bool>(this.Ceasefire));
			base.AddExecutor(ExecutorType.Activate, 29843091);
			base.AddExecutor(ExecutorType.Activate, 1918087);
			base.AddExecutor(ExecutorType.Activate, 48276469);
			base.AddExecutor(ExecutorType.Activate, 98139712);
			base.AddExecutor(ExecutorType.Activate, 79323590);
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x0000763C File Offset: 0x0000583C
		public override bool OnSelectHand()
		{
			return true;
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x00057F9F File Offset: 0x0005619F
		private bool SwordsOfRevealingLight()
		{
			return base.Bot.SpellZone.GetCardCount(72302403) == 0;
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x00057FB9 File Offset: 0x000561B9
		private bool SupremacyBerry()
		{
			return base.Bot.LifePoints < base.Enemy.LifePoints;
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x00057FD3 File Offset: 0x000561D3
		private bool PoisonOfTheOldMan()
		{
			base.AI.SelectOption(1);
			return true;
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x00057FE2 File Offset: 0x000561E2
		private bool ThunderShort()
		{
			return base.Enemy.GetMonsterCount() >= 3;
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x00057FF8 File Offset: 0x000561F8
		private bool SetInvincibleMonster()
		{
			using (List<ClientCard>.Enumerator enumerator = base.Bot.GetMonsters().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsCode(new int[] { 31305911, 23205979 }))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x0005806C File Offset: 0x0005626C
		private bool LavaGolem()
		{
			bool found = false;
			using (List<ClientCard>.Enumerator enumerator = base.Enemy.GetMonsters().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Attack > 2000)
					{
						found = true;
					}
				}
			}
			return found;
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x000580D0 File Offset: 0x000562D0
		private bool Ceasefire()
		{
			return base.Bot.GetMonsterCount() + base.Enemy.GetMonsterCount() >= 3;
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x000580F0 File Offset: 0x000562F0
		private bool ReposEverything()
		{
			if (base.Card.IsCode(2851070))
			{
				return base.Card.IsDefense();
			}
			if (base.Card.IsCode(97396380))
			{
				return base.DefaultMonsterRepos();
			}
			return base.Card.IsAttack();
		}

		// Token: 0x020002CE RID: 718
		public class CardId
		{
			// Token: 0x04001609 RID: 5641
			public const int LavaGolem = 102380;

			// Token: 0x0400160A RID: 5642
			public const int ReflectBounder = 2851070;

			// Token: 0x0400160B RID: 5643
			public const int FencingFireFerret = 97396380;

			// Token: 0x0400160C RID: 5644
			public const int BlastSphere = 26302522;

			// Token: 0x0400160D RID: 5645
			public const int Marshmallon = 31305911;

			// Token: 0x0400160E RID: 5646
			public const int SpiritReaper = 23205979;

			// Token: 0x0400160F RID: 5647
			public const int NaturiaBeans = 44789585;

			// Token: 0x04001610 RID: 5648
			public const int ThunderShort = 20264508;

			// Token: 0x04001611 RID: 5649
			public const int Ookazi = 19523799;

			// Token: 0x04001612 RID: 5650
			public const int GoblinThief = 45311864;

			// Token: 0x04001613 RID: 5651
			public const int TremendousFire = 46918794;

			// Token: 0x04001614 RID: 5652
			public const int SwordsOfRevealingLight = 72302403;

			// Token: 0x04001615 RID: 5653
			public const int SupremacyBerry = 98380593;

			// Token: 0x04001616 RID: 5654
			public const int ChainEnergy = 79323590;

			// Token: 0x04001617 RID: 5655
			public const int DarkRoomofNightmare = 85562745;

			// Token: 0x04001618 RID: 5656
			public const int PoisonOfTheOldMan = 8842266;

			// Token: 0x04001619 RID: 5657
			public const int OjamaTrio = 29843091;

			// Token: 0x0400161A RID: 5658
			public const int Ceasefire = 36468556;

			// Token: 0x0400161B RID: 5659
			public const int MagicCylinder = 62279055;

			// Token: 0x0400161C RID: 5660
			public const int MinorGoblinOfficial = 1918087;

			// Token: 0x0400161D RID: 5661
			public const int ChainBurst = 48276469;

			// Token: 0x0400161E RID: 5662
			public const int SkullInvitation = 98139712;
		}
	}
}
