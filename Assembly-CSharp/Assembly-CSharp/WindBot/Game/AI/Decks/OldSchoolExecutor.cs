using System;
using System.Collections.Generic;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x0200038B RID: 907
	[Deck("OldSchool", "AI_OldSchool", "Easy")]
	public class OldSchoolExecutor : DefaultExecutor
	{
		// Token: 0x06001AAC RID: 6828 RVA: 0x0009D830 File Offset: 0x0009BA30
		public OldSchoolExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 19613556, new Func<bool>(base.DefaultHeavyStorm));
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(base.DefaultSpellSet));
			base.AddExecutor(ExecutorType.Activate, 53129443, new Func<bool>(base.DefaultDarkHole));
			base.AddExecutor(ExecutorType.Activate, 12580477, new Func<bool>(base.DefaultRaigeki));
			base.AddExecutor(ExecutorType.Activate, 26412047, new Func<bool>(base.DefaultHammerShot));
			base.AddExecutor(ExecutorType.Activate, 66788016);
			base.AddExecutor(ExecutorType.Activate, 72302403, new Func<bool>(this.SwordsOfRevealingLight));
			base.AddExecutor(ExecutorType.Activate, 43422537, new Func<bool>(this.DoubleSummon));
			base.AddExecutor(ExecutorType.Summon, 83104731, new Func<bool>(base.DefaultMonsterSummon));
			base.AddExecutor(ExecutorType.Summon, 6631034, new Func<bool>(base.DefaultMonsterSummon));
			base.AddExecutor(ExecutorType.SummonOrSet, 43096270);
			base.AddExecutor(ExecutorType.SummonOrSet, 69247929);
			base.AddExecutor(ExecutorType.MonsterSet, 30190809);
			base.AddExecutor(ExecutorType.SummonOrSet, 77542832);
			base.AddExecutor(ExecutorType.SummonOrSet, 11091375);
			base.AddExecutor(ExecutorType.SummonOrSet, 35052053);
			base.AddExecutor(ExecutorType.SummonOrSet, 49881766);
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(base.DefaultMonsterRepos));
			base.AddExecutor(ExecutorType.Activate, 44095762, new Func<bool>(base.DefaultTrap));
			base.AddExecutor(ExecutorType.Activate, 70342110, new Func<bool>(base.DefaultTrap));
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x0009D9BC File Offset: 0x0009BBBC
		private bool DoubleSummon()
		{
			if (this._lastDoubleSummon == base.Duel.Turn)
			{
				return false;
			}
			if (base.Duel.MainPhase.SummonableCards.Count == 0)
			{
				return false;
			}
			if (base.Duel.MainPhase.SummonableCards.Count == 1 && base.Duel.MainPhase.SummonableCards[0].Level < 5)
			{
				bool canTribute = false;
				foreach (ClientCard handCard in base.Bot.Hand)
				{
					if (handCard.IsMonster() && handCard.Level > 4 && handCard.Level < 6)
					{
						canTribute = true;
					}
				}
				if (!canTribute)
				{
					return false;
				}
			}
			int monsters = 0;
			using (IEnumerator<ClientCard> enumerator = base.Bot.Hand.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsMonster())
					{
						monsters++;
					}
				}
			}
			if (monsters <= 1)
			{
				return false;
			}
			this._lastDoubleSummon = base.Duel.Turn;
			return true;
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x0009DAEC File Offset: 0x0009BCEC
		private bool SwordsOfRevealingLight()
		{
			using (List<ClientCard>.Enumerator enumerator = base.Enemy.GetMonsters().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsFacedown())
					{
						return true;
					}
				}
			}
			return base.Util.IsOneEnemyBetter(true);
		}

		// Token: 0x04001DF3 RID: 7667
		private int _lastDoubleSummon;

		// Token: 0x0200038C RID: 908
		public class CardId
		{
			// Token: 0x04001DF4 RID: 7668
			public const int AncientGearGolem = 83104731;

			// Token: 0x04001DF5 RID: 7669
			public const int Frostosaurus = 6631034;

			// Token: 0x04001DF6 RID: 7670
			public const int AlexandriteDragon = 43096270;

			// Token: 0x04001DF7 RID: 7671
			public const int GeneWarpedWarwolf = 69247929;

			// Token: 0x04001DF8 RID: 7672
			public const int GearGolemTheMovingFortress = 30190809;

			// Token: 0x04001DF9 RID: 7673
			public const int EvilswarmHeliotrope = 77542832;

			// Token: 0x04001DFA RID: 7674
			public const int LusterDragon = 11091375;

			// Token: 0x04001DFB RID: 7675
			public const int InsectKnight = 35052053;

			// Token: 0x04001DFC RID: 7676
			public const int ArchfiendSoldier = 49881766;

			// Token: 0x04001DFD RID: 7677
			public const int HeavyStorm = 19613556;

			// Token: 0x04001DFE RID: 7678
			public const int DarkHole = 53129443;

			// Token: 0x04001DFF RID: 7679
			public const int Raigeki = 12580477;

			// Token: 0x04001E00 RID: 7680
			public const int HammerShot = 26412047;

			// Token: 0x04001E01 RID: 7681
			public const int Fissure = 66788016;

			// Token: 0x04001E02 RID: 7682
			public const int SwordsOfRevealingLight = 72302403;

			// Token: 0x04001E03 RID: 7683
			public const int DoubleSummon = 43422537;

			// Token: 0x04001E04 RID: 7684
			public const int MirrorForce = 44095762;

			// Token: 0x04001E05 RID: 7685
			public const int DimensionalPrison = 70342110;
		}
	}
}
