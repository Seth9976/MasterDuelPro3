using System;
using System.Collections.Generic;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x02000399 RID: 921
	[Deck("Qliphort", "AI_Qliphort", "Normal")]
	public class QliphortExecutor : DefaultExecutor
	{
		// Token: 0x06001B75 RID: 7029 RVA: 0x000A3C7C File Offset: 0x000A1E7C
		public QliphortExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 53129443, new Func<bool>(base.DefaultDarkHole));
			base.AddExecutor(ExecutorType.Activate, 79816536);
			base.AddExecutor(ExecutorType.Activate, 65518099, new Func<bool>(this.ScoutActivate));
			base.AddExecutor(ExecutorType.Activate, 65518099, new Func<bool>(this.ScoutEffect));
			base.AddExecutor(ExecutorType.Activate, 13073850, new Func<bool>(this.ScaleActivate));
			base.AddExecutor(ExecutorType.Activate, 90885155, new Func<bool>(this.ScaleActivate));
			base.AddExecutor(ExecutorType.Activate, 37991342, new Func<bool>(this.ScaleActivate));
			base.AddExecutor(ExecutorType.Activate, 91907707, new Func<bool>(this.ScaleActivate));
			base.AddExecutor(ExecutorType.Summon, new Func<bool>(this.NormalSummon));
			base.AddExecutor(ExecutorType.SpSummon);
			base.AddExecutor(ExecutorType.Activate, 17639150, new Func<bool>(this.SaqlificeEffect));
			base.AddExecutor(ExecutorType.Activate, 13073850, new Func<bool>(this.StealthEffect));
			base.AddExecutor(ExecutorType.Activate, 37991342, new Func<bool>(this.HelixEffect));
			base.AddExecutor(ExecutorType.Activate, 91907707, new Func<bool>(this.CarrierEffect));
			base.AddExecutor(ExecutorType.SpellSet, 82732705, new Func<bool>(this.TrapSetUnique));
			base.AddExecutor(ExecutorType.SpellSet, 5851097, new Func<bool>(this.TrapSetUnique));
			base.AddExecutor(ExecutorType.SpellSet, 83326048, new Func<bool>(this.TrapSetUnique));
			base.AddExecutor(ExecutorType.SpellSet, 53582587, new Func<bool>(this.TrapSetUnique));
			base.AddExecutor(ExecutorType.SpellSet, 40605147, new Func<bool>(this.TrapSetUnique));
			base.AddExecutor(ExecutorType.SpellSet, 44095762, new Func<bool>(this.TrapSetUnique));
			base.AddExecutor(ExecutorType.SpellSet, 94192409, new Func<bool>(this.TrapSetUnique));
			base.AddExecutor(ExecutorType.SpellSet, 99188141, new Func<bool>(this.TrapSetUnique));
			base.AddExecutor(ExecutorType.SpellSet, 17639150, new Func<bool>(this.TrapSetWhenZoneFree));
			base.AddExecutor(ExecutorType.SpellSet, 82732705, new Func<bool>(this.TrapSetWhenZoneFree));
			base.AddExecutor(ExecutorType.SpellSet, 5851097, new Func<bool>(this.TrapSetWhenZoneFree));
			base.AddExecutor(ExecutorType.SpellSet, 83326048, new Func<bool>(this.TrapSetWhenZoneFree));
			base.AddExecutor(ExecutorType.SpellSet, 53582587, new Func<bool>(this.TrapSetWhenZoneFree));
			base.AddExecutor(ExecutorType.SpellSet, 40605147, new Func<bool>(this.TrapSetWhenZoneFree));
			base.AddExecutor(ExecutorType.SpellSet, 44095762, new Func<bool>(this.TrapSetWhenZoneFree));
			base.AddExecutor(ExecutorType.SpellSet, 94192409, new Func<bool>(this.TrapSetWhenZoneFree));
			base.AddExecutor(ExecutorType.SpellSet, 99188141, new Func<bool>(this.TrapSetWhenZoneFree));
			base.AddExecutor(ExecutorType.SpellSet, 53129443, new Func<bool>(this.TrapSetWhenZoneFree));
			base.AddExecutor(ExecutorType.SpellSet, 79816536, new Func<bool>(this.TrapSetWhenZoneFree));
			base.AddExecutor(ExecutorType.SpellSet, 98645731, new Func<bool>(this.TrapSetWhenZoneFree));
			base.AddExecutor(ExecutorType.Activate, 98645731, new Func<bool>(this.PotOfDualityEffect));
			base.AddExecutor(ExecutorType.SpellSet, 59750328);
			base.AddExecutor(ExecutorType.Activate, 59750328, new Func<bool>(this.CardOfDemiseEffect));
			base.AddExecutor(ExecutorType.SpellSet, 17639150, new Func<bool>(this.CardOfDemiseAcivated));
			base.AddExecutor(ExecutorType.SpellSet, 82732705, new Func<bool>(this.CardOfDemiseAcivated));
			base.AddExecutor(ExecutorType.SpellSet, 5851097, new Func<bool>(this.CardOfDemiseAcivated));
			base.AddExecutor(ExecutorType.SpellSet, 83326048, new Func<bool>(this.CardOfDemiseAcivated));
			base.AddExecutor(ExecutorType.SpellSet, 53582587, new Func<bool>(this.CardOfDemiseAcivated));
			base.AddExecutor(ExecutorType.SpellSet, 40605147, new Func<bool>(this.CardOfDemiseAcivated));
			base.AddExecutor(ExecutorType.SpellSet, 44095762, new Func<bool>(this.CardOfDemiseAcivated));
			base.AddExecutor(ExecutorType.SpellSet, 94192409, new Func<bool>(this.CardOfDemiseAcivated));
			base.AddExecutor(ExecutorType.SpellSet, 99188141, new Func<bool>(this.CardOfDemiseAcivated));
			base.AddExecutor(ExecutorType.SpellSet, 53129443, new Func<bool>(this.CardOfDemiseAcivated));
			base.AddExecutor(ExecutorType.SpellSet, 79816536, new Func<bool>(this.CardOfDemiseAcivated));
			base.AddExecutor(ExecutorType.SpellSet, 98645731, new Func<bool>(this.CardOfDemiseAcivated));
			base.AddExecutor(ExecutorType.Activate, 99188141, new Func<bool>(base.DefaultTrap));
			base.AddExecutor(ExecutorType.Activate, 40605147, new Func<bool>(base.DefaultSolemnStrike));
			base.AddExecutor(ExecutorType.Activate, 82732705, new Func<bool>(this.SkillDrainEffect));
			base.AddExecutor(ExecutorType.Activate, 5851097, new Func<bool>(base.DefaultUniqueTrap));
			base.AddExecutor(ExecutorType.Activate, 94192409, new Func<bool>(base.DefaultCompulsoryEvacuationDevice));
			base.AddExecutor(ExecutorType.Activate, 83326048, new Func<bool>(base.DefaultDimensionalBarrier));
			base.AddExecutor(ExecutorType.Activate, 44095762, new Func<bool>(base.DefaultUniqueTrap));
			base.AddExecutor(ExecutorType.Activate, 53582587, new Func<bool>(base.DefaultTorrentialTribute));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(base.DefaultMonsterRepos));
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x0000763C File Offset: 0x0000583C
		public override bool OnSelectHand()
		{
			return true;
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x000A4201 File Offset: 0x000A2401
		public override void OnNewTurn()
		{
			this.CardOfDemiseUsed = false;
			base.OnNewTurn();
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x000A4210 File Offset: 0x000A2410
		public override IList<ClientCard> OnSelectPendulumSummon(IList<ClientCard> cards, int max)
		{
			Logger.DebugWriteLine("OnSelectPendulumSummon");
			IList<ClientCard> selected = new List<ClientCard>();
			for (int i = 1; i <= max; i++)
			{
				ClientCard card = cards[cards.Count - i];
				if (!card.IsCode(65518099) || (card.Location == CardLocation.Extra && !base.Duel.IsNewRule))
				{
					selected.Add(card);
				}
			}
			if (selected.Count == 0)
			{
				selected.Add(cards[cards.Count - 1]);
			}
			return selected;
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x000A4290 File Offset: 0x000A2490
		private bool NormalSummon()
		{
			if (base.Card.IsCode(65518099))
			{
				return false;
			}
			if (base.Card.Level < 8)
			{
				base.AI.SelectOption(1);
			}
			return true;
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x0006EBE5 File Offset: 0x0006CDE5
		private bool SkillDrainEffect()
		{
			return base.Bot.LifePoints > 1000 && base.DefaultUniqueTrap();
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x000A42C1 File Offset: 0x000A24C1
		private bool PotOfDualityEffect()
		{
			base.AI.SelectCard(new int[] { 65518099, 82732705, 5851097, 83326048, 13073850, 90885155, 37991342, 91907707, 40605147, 59750328 });
			return !this.ShouldPendulum();
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x000A42E9 File Offset: 0x000A24E9
		private bool CardOfDemiseEffect()
		{
			if (base.Util.IsTurn1OrMain2() && !this.ShouldPendulum())
			{
				this.CardOfDemiseUsed = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x000A430C File Offset: 0x000A250C
		private bool TrapSetUnique()
		{
			using (List<ClientCard>.Enumerator enumerator = base.Bot.GetSpells().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsCode(base.Card.Id))
					{
						return false;
					}
				}
			}
			return this.TrapSetWhenZoneFree();
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x000A437C File Offset: 0x000A257C
		private bool TrapSetWhenZoneFree()
		{
			return base.Bot.GetSpellCountWithoutField() < 4;
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x000A438C File Offset: 0x000A258C
		private bool CardOfDemiseAcivated()
		{
			return this.CardOfDemiseUsed;
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x000A4394 File Offset: 0x000A2594
		private bool SaqlificeEffect()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				bool pzone = base.Util.GetPZone(0, 0) != null;
				ClientCard r = base.Util.GetPZone(0, 1);
				if (!pzone && r == null)
				{
					base.AI.SelectCard(65518099);
				}
			}
			return true;
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x000A43E4 File Offset: 0x000A25E4
		private bool ScoutActivate()
		{
			if (base.Card.Location != CardLocation.Hand)
			{
				return false;
			}
			ClientCard i = base.Util.GetPZone(0, 0);
			ClientCard r = base.Util.GetPZone(0, 1);
			return (i == null && r == null) || (i == null && r.RScale != base.Card.LScale) || (r == null && i.LScale != base.Card.RScale);
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x000A4458 File Offset: 0x000A2658
		private bool ScaleActivate()
		{
			if (!base.Card.HasType(CardType.Pendulum) || base.Card.Location != CardLocation.Hand)
			{
				return false;
			}
			int count = 0;
			foreach (ClientCard card in base.Bot.Hand.GetMonsters())
			{
				if (!base.Card.Equals(card))
				{
					count++;
				}
			}
			foreach (ClientCard clientCard in base.Bot.ExtraDeck.GetFaceupPendulumMonsters())
			{
				count++;
			}
			ClientCard i = base.Util.GetPZone(0, 0);
			ClientCard r = base.Util.GetPZone(0, 1);
			if (i == null && r == null)
			{
				if (this.CardOfDemiseUsed)
				{
					return true;
				}
				bool pair = false;
				using (List<ClientCard>.Enumerator enumerator = base.Bot.Hand.GetMonsters().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.RScale != base.Card.LScale)
						{
							pair = true;
							count--;
							break;
						}
					}
				}
				return pair && count > 1;
			}
			else
			{
				if (i == null && r.RScale != base.Card.LScale)
				{
					return count > 1 || this.CardOfDemiseUsed;
				}
				return r == null && i.LScale != base.Card.RScale && (count > 1 || this.CardOfDemiseUsed);
			}
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x000A4610 File Offset: 0x000A2810
		private bool ScoutEffect()
		{
			if (base.Card.Location == CardLocation.Hand)
			{
				return false;
			}
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			int count = 0;
			int handcount = 0;
			int fieldcount = 0;
			foreach (ClientCard clientCard in base.Bot.Hand.GetMonsters())
			{
				count++;
				handcount++;
			}
			foreach (ClientCard clientCard2 in base.Bot.MonsterZone.GetMonsters())
			{
				fieldcount++;
			}
			foreach (ClientCard clientCard3 in base.Bot.ExtraDeck.GetFaceupPendulumMonsters())
			{
				count++;
			}
			if (count > 0 && !base.Bot.HasInHand(this.LowScaleCards))
			{
				base.AI.SelectCard(this.LowScaleCards);
			}
			else if (handcount > 0 || fieldcount > 0)
			{
				base.AI.SelectCard(new int[] { 17639150, 90885155, 37991342 });
			}
			else
			{
				base.AI.SelectCard(this.HighScaleCards);
			}
			return base.Bot.LifePoints > 800;
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x000A479C File Offset: 0x000A299C
		private bool StealthEffect()
		{
			if (base.Card.Location == CardLocation.Hand)
			{
				return false;
			}
			ClientCard target = base.Util.GetBestEnemyCard(false, false);
			if (target != null)
			{
				base.AI.SelectCard(target);
				return true;
			}
			return false;
		}

		// Token: 0x06001B85 RID: 7045 RVA: 0x000A47DC File Offset: 0x000A29DC
		private bool CarrierEffect()
		{
			if (base.Card.Location == CardLocation.Hand)
			{
				return false;
			}
			ClientCard target = base.Util.GetBestEnemyMonster(false, false);
			if (target != null)
			{
				base.AI.SelectCard(target);
				return true;
			}
			return false;
		}

		// Token: 0x06001B86 RID: 7046 RVA: 0x000A481C File Offset: 0x000A2A1C
		private bool HelixEffect()
		{
			if (base.Card.Location == CardLocation.Hand)
			{
				return false;
			}
			ClientCard target = base.Util.GetBestEnemySpell(false);
			if (target != null)
			{
				base.AI.SelectCard(target);
				return true;
			}
			return false;
		}

		// Token: 0x06001B87 RID: 7047 RVA: 0x000A4858 File Offset: 0x000A2A58
		private bool ShouldPendulum()
		{
			ClientCard i = base.Util.GetPZone(0, 0);
			ClientCard r = base.Util.GetPZone(0, 1);
			if (i != null && r != null && i.LScale != r.RScale)
			{
				int count = 0;
				foreach (ClientCard clientCard in base.Bot.Hand.GetMonsters())
				{
					count++;
				}
				foreach (ClientCard clientCard2 in base.Bot.ExtraDeck.GetFaceupPendulumMonsters())
				{
					count++;
				}
				return count > 1;
			}
			return false;
		}

		// Token: 0x04001EC4 RID: 7876
		private bool CardOfDemiseUsed;

		// Token: 0x04001EC5 RID: 7877
		private IList<int> LowScaleCards = new int[] { 13073850, 91907707 };

		// Token: 0x04001EC6 RID: 7878
		private IList<int> HighScaleCards = new int[] { 65518099, 90885155, 37991342 };

		// Token: 0x0200039A RID: 922
		public class CardId
		{
			// Token: 0x04001EC7 RID: 7879
			public const int Scout = 65518099;

			// Token: 0x04001EC8 RID: 7880
			public const int Stealth = 13073850;

			// Token: 0x04001EC9 RID: 7881
			public const int Shell = 90885155;

			// Token: 0x04001ECA RID: 7882
			public const int Helix = 37991342;

			// Token: 0x04001ECB RID: 7883
			public const int Carrier = 91907707;

			// Token: 0x04001ECC RID: 7884
			public const int DarkHole = 53129443;

			// Token: 0x04001ECD RID: 7885
			public const int CardOfDemise = 59750328;

			// Token: 0x04001ECE RID: 7886
			public const int SummonersArt = 79816536;

			// Token: 0x04001ECF RID: 7887
			public const int PotOfDuality = 98645731;

			// Token: 0x04001ED0 RID: 7888
			public const int Saqlifice = 17639150;

			// Token: 0x04001ED1 RID: 7889
			public const int MirrorForce = 44095762;

			// Token: 0x04001ED2 RID: 7890
			public const int TorrentialTribute = 53582587;

			// Token: 0x04001ED3 RID: 7891
			public const int DimensionalBarrier = 83326048;

			// Token: 0x04001ED4 RID: 7892
			public const int CompulsoryEvacuationDevice = 94192409;

			// Token: 0x04001ED5 RID: 7893
			public const int VanitysEmptiness = 5851097;

			// Token: 0x04001ED6 RID: 7894
			public const int SkillDrain = 82732705;

			// Token: 0x04001ED7 RID: 7895
			public const int SolemnStrike = 40605147;

			// Token: 0x04001ED8 RID: 7896
			public const int TheHugeRevolutionIsOver = 99188141;
		}
	}
}
