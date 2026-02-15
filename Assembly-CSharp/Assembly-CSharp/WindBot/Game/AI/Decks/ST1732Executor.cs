using System;
using System.Collections.Generic;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x020003CF RID: 975
	[Deck("ST1732", "AI_ST1732", "Normal")]
	public class ST1732Executor : DefaultExecutor
	{
		// Token: 0x06001DA6 RID: 7590 RVA: 0x000B2608 File Offset: 0x000B0808
		public ST1732Executor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 8267140, new Func<bool>(base.DefaultCosmicCyclone));
			base.AddExecutor(ExecutorType.Activate, 5318639, new Func<bool>(base.DefaultMysticalSpaceTyphoon));
			base.AddExecutor(ExecutorType.Activate, 53129443, new Func<bool>(base.DefaultDarkHole));
			base.AddExecutor(ExecutorType.Activate, 14087893, new Func<bool>(base.DefaultBookOfMoon));
			base.AddExecutor(ExecutorType.Activate, 61583217, new Func<bool>(this.CynetUniverseEffect));
			base.AddExecutor(ExecutorType.SpSummon, 35595518);
			base.AddExecutor(ExecutorType.Activate, 35595518, new Func<bool>(this.LinkslayerEffect));
			base.AddExecutor(ExecutorType.SpSummon, 98978921);
			base.AddExecutor(ExecutorType.Activate, 98978921);
			base.AddExecutor(ExecutorType.Activate, 37520316, new Func<bool>(this.MindControlEffect));
			base.AddExecutor(ExecutorType.SpSummon, 71172240);
			base.AddExecutor(ExecutorType.Activate, 71172240, new Func<bool>(this.BacklinkerEffect));
			base.AddExecutor(ExecutorType.Activate, 70950698, new Func<bool>(this.BootStagguardEffect));
			base.AddExecutor(ExecutorType.Activate, 83764718, new Func<bool>(this.MonsterRebornEffect));
			base.AddExecutor(ExecutorType.Activate, 19508728, new Func<bool>(this.MoonMirrorShieldEffect));
			base.AddExecutor(ExecutorType.Activate, 43839002, new Func<bool>(this.CynetBackdoorEffect));
			base.AddExecutor(ExecutorType.Activate, 70238111);
			base.AddExecutor(ExecutorType.Summon, 8567955, new Func<bool>(this.BalancerLordSummon));
			base.AddExecutor(ExecutorType.Summon, 44956694, new Func<bool>(this.ROMCloudiaSummon));
			base.AddExecutor(ExecutorType.Activate, 44956694, new Func<bool>(this.ROMCloudiaEffect));
			base.AddExecutor(ExecutorType.Summon, 62706865, new Func<bool>(this.DraconnetSummon));
			base.AddExecutor(ExecutorType.Activate, 62706865, new Func<bool>(this.DraconnetEffect));
			base.AddExecutor(ExecutorType.Summon, 45778242);
			base.AddExecutor(ExecutorType.Activate, 45778242, new Func<bool>(this.KleinantEffect));
			base.AddExecutor(ExecutorType.Summon, 9190563);
			base.AddExecutor(ExecutorType.Activate, 9190563, new Func<bool>(this.RAMClouderEffect));
			base.AddExecutor(ExecutorType.SummonOrSet, 18789533);
			base.AddExecutor(ExecutorType.Activate, 18789533, new Func<bool>(this.DotScaperEffect));
			base.AddExecutor(ExecutorType.Summon, 8567955);
			base.AddExecutor(ExecutorType.Summon, 44956694);
			base.AddExecutor(ExecutorType.Summon, 62706865);
			base.AddExecutor(ExecutorType.SummonOrSet, 71172240);
			base.AddExecutor(ExecutorType.SummonOrSet, 32295838);
			base.AddExecutor(ExecutorType.SummonOrSet, 36211150);
			base.AddExecutor(ExecutorType.Activate, 8567955, new Func<bool>(this.BalancerLordEffect));
			base.AddExecutor(ExecutorType.SpSummon, 1861629, new Func<bool>(this.LinkSummon));
			base.AddExecutor(ExecutorType.Activate, 1861629);
			base.AddExecutor(ExecutorType.SpSummon, 32617464, new Func<bool>(this.LinkSummon));
			base.AddExecutor(ExecutorType.Activate, 32617464);
			base.AddExecutor(ExecutorType.SpSummon, 6622715, new Func<bool>(this.LinkSummon));
			base.AddExecutor(ExecutorType.Activate, 6622715);
			base.AddExecutor(ExecutorType.SpSummon, 34472920, new Func<bool>(this.LinkSummon));
			base.AddExecutor(ExecutorType.SpSummon, 79016563, new Func<bool>(this.LinkSummon));
			base.AddExecutor(ExecutorType.Activate, 79016563);
			base.AddExecutor(ExecutorType.SpellSet, 43839002, new Func<bool>(base.DefaultSpellSet));
			base.AddExecutor(ExecutorType.SpellSet, 70238111, new Func<bool>(base.DefaultSpellSet));
			base.AddExecutor(ExecutorType.SpellSet, 40605147, new Func<bool>(base.DefaultSpellSet));
			base.AddExecutor(ExecutorType.SpellSet, 94192409, new Func<bool>(base.DefaultSpellSet));
			base.AddExecutor(ExecutorType.SpellSet, 83326048, new Func<bool>(base.DefaultSpellSet));
			base.AddExecutor(ExecutorType.SpellSet, 53582587, new Func<bool>(base.DefaultSpellSet));
			base.AddExecutor(ExecutorType.SpellSet, 44095762, new Func<bool>(base.DefaultSpellSet));
			base.AddExecutor(ExecutorType.SpellSet, 29401950, new Func<bool>(base.DefaultSpellSet));
			base.AddExecutor(ExecutorType.SpellSet, 14087893, new Func<bool>(base.DefaultSpellSet));
			base.AddExecutor(ExecutorType.SpellSet, 8267140, new Func<bool>(base.DefaultSpellSet));
			base.AddExecutor(ExecutorType.SpellSet, 5318639, new Func<bool>(base.DefaultSpellSet));
			base.AddExecutor(ExecutorType.Activate, 40605147, new Func<bool>(base.DefaultSolemnStrike));
			base.AddExecutor(ExecutorType.Activate, 94192409, new Func<bool>(base.DefaultCompulsoryEvacuationDevice));
			base.AddExecutor(ExecutorType.Activate, 83326048, new Func<bool>(base.DefaultDimensionalBarrier));
			base.AddExecutor(ExecutorType.Activate, 53582587, new Func<bool>(base.DefaultTorrentialTribute));
			base.AddExecutor(ExecutorType.Activate, 44095762, new Func<bool>(base.DefaultUniqueTrap));
			base.AddExecutor(ExecutorType.Activate, 29401950, new Func<bool>(base.DefaultUniqueTrap));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(base.DefaultMonsterRepos));
		}

		// Token: 0x06001DA7 RID: 7591 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnSelectHand()
		{
			return false;
		}

		// Token: 0x06001DA8 RID: 7592 RVA: 0x000B2B10 File Offset: 0x000B0D10
		public override void OnNewTurn()
		{
			this.BalancerLordUsed = false;
			base.OnNewTurn();
		}

		// Token: 0x06001DA9 RID: 7593 RVA: 0x000B2B1F File Offset: 0x000B0D1F
		public override int OnSelectOption(IList<int> options)
		{
			if (options.Count != 2)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06001DAA RID: 7594 RVA: 0x000B2B2D File Offset: 0x000B0D2D
		public override bool OnSelectYesNo(int desc)
		{
			return desc != 210 && (desc == 31 || base.OnSelectYesNo(desc));
		}

		// Token: 0x06001DAB RID: 7595 RVA: 0x000B2B48 File Offset: 0x000B0D48
		private bool LinkslayerEffect()
		{
			IList<ClientCard> targets = base.Enemy.GetSpells();
			if (targets.Count > 0)
			{
				base.AI.SelectCard(new int[] { 7445307, 36211150, 32295838, 70238111 });
				base.AI.SelectNextCard(targets);
				return true;
			}
			return false;
		}

		// Token: 0x06001DAC RID: 7596 RVA: 0x000B2B98 File Offset: 0x000B0D98
		private bool MindControlEffect()
		{
			ClientCard target = base.Util.GetBestEnemyMonster(false, false);
			if (target != null)
			{
				base.AI.SelectCard(target);
				return true;
			}
			return false;
		}

		// Token: 0x06001DAD RID: 7597 RVA: 0x000B2BC5 File Offset: 0x000B0DC5
		private bool BacklinkerEffect()
		{
			return base.Bot.MonsterZone[5] == null && base.Bot.MonsterZone[6] == null;
		}

		// Token: 0x06001DAE RID: 7598 RVA: 0x000B2BE8 File Offset: 0x000B0DE8
		private bool BootStagguardEffect()
		{
			if (base.Card.Location != CardLocation.Hand)
			{
				base.AI.SelectPosition(CardPosition.FaceUpDefence);
			}
			return true;
		}

		// Token: 0x06001DAF RID: 7599 RVA: 0x000B2C08 File Offset: 0x000B0E08
		private bool MonsterRebornEffect()
		{
			IList<int> targets = new int[]
			{
				1861629, 6622715, 32617464, 79016563, 34472920, 7445307, 70950698, 8567955, 44956694, 35595518,
				9190563, 71172240, 45778242
			};
			if (!base.Bot.HasInGraveyard(targets))
			{
				return false;
			}
			base.AI.SelectCard(targets);
			return true;
		}

		// Token: 0x06001DB0 RID: 7600 RVA: 0x000B2C48 File Offset: 0x000B0E48
		private bool MoonMirrorShieldEffect()
		{
			using (List<ClientCard>.Enumerator enumerator = base.Bot.GetMonsters().GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					ClientCard monster = enumerator.Current;
					base.AI.SelectCard(monster);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001DB1 RID: 7601 RVA: 0x000B2CAC File Offset: 0x000B0EAC
		private bool CynetUniverseEffect()
		{
			if (base.Card.Location == CardLocation.Hand)
			{
				return base.DefaultField();
			}
			foreach (ClientCard card in base.Enemy.Graveyard)
			{
				if (card.IsMonster())
				{
					base.AI.SelectCard(card);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001DB2 RID: 7602 RVA: 0x000B2D28 File Offset: 0x000B0F28
		private bool CynetBackdoorEffect()
		{
			if ((base.Duel.Player != 0 || base.Duel.Phase != DuelPhase.Main2) && (base.Duel.Player != 1 || (base.Duel.Phase != DuelPhase.BattleStart && base.Duel.Phase != DuelPhase.End)))
			{
				return false;
			}
			if (!base.UniqueFaceupSpell())
			{
				return false;
			}
			bool selected = false;
			foreach (ClientCard monster in base.Bot.GetMonstersInExtraZone())
			{
				if (monster.Attack > 1000)
				{
					base.AI.SelectCard(monster);
					selected = true;
					break;
				}
			}
			if (!selected)
			{
				List<ClientCard> monsters = base.Bot.GetMonsters();
				foreach (ClientCard monster2 in monsters)
				{
					if (monster2.IsCode(8567955))
					{
						base.AI.SelectCard(monster2);
						selected = true;
						break;
					}
				}
				if (!selected)
				{
					foreach (ClientCard monster3 in monsters)
					{
						if (monster3.Attack >= 1700)
						{
							base.AI.SelectCard(monster3);
							selected = true;
							break;
						}
					}
				}
			}
			if (selected)
			{
				base.AI.SelectNextCard(new int[] { 44956694, 8567955, 45778242, 62706865, 71172240 });
				return true;
			}
			return false;
		}

		// Token: 0x06001DB3 RID: 7603 RVA: 0x000B2ED4 File Offset: 0x000B10D4
		private bool BalancerLordSummon()
		{
			return !this.BalancerLordUsed;
		}

		// Token: 0x06001DB4 RID: 7604 RVA: 0x000B2EE0 File Offset: 0x000B10E0
		private bool BalancerLordEffect()
		{
			if (base.Card.Location == CardLocation.Removed)
			{
				return true;
			}
			if (base.Bot.HasInHand(new int[] { 62706865, 45778242, 8567955, 44956694, 9190563, 18789533 }) && !this.BalancerLordUsed)
			{
				this.BalancerLordUsed = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001DB5 RID: 7605 RVA: 0x000B2F2E File Offset: 0x000B112E
		private bool ROMCloudiaSummon()
		{
			return base.Bot.HasInGraveyard(new int[] { 70950698, 8567955, 45778242, 35595518, 62706865, 9190563 });
		}

		// Token: 0x06001DB6 RID: 7606 RVA: 0x000B2F4C File Offset: 0x000B114C
		private bool ROMCloudiaEffect()
		{
			if (base.Card.Location == CardLocation.MonsterZone)
			{
				base.AI.SelectCard(new int[] { 70950698, 8567955, 45778242, 35595518, 62706865, 9190563 });
				return true;
			}
			base.AI.SelectCard(new int[] { 8567955, 45778242, 9190563, 18789533 });
			return true;
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x000B2FA2 File Offset: 0x000B11A2
		private bool DraconnetSummon()
		{
			return base.Bot.GetRemainingCount(32295838, 1) > 0 || base.Bot.GetRemainingCount(36211150, 1) > 0;
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x000B2FCE File Offset: 0x000B11CE
		private bool DraconnetEffect()
		{
			base.AI.SelectCard(36211150);
			return true;
		}

		// Token: 0x06001DB9 RID: 7609 RVA: 0x000B2FE4 File Offset: 0x000B11E4
		private bool KleinantEffect()
		{
			IList<int> targets = new int[] { 7445307, 36211150, 32295838, 18789533 };
			using (IEnumerator<ClientCard> enumerator = base.Bot.Hand.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsCode(targets))
					{
						base.AI.SelectCard(targets);
						return true;
					}
				}
			}
			IList<int> targets2 = new int[] { 70950699, 36211150, 32295838, 18789533 };
			using (List<ClientCard>.Enumerator enumerator2 = base.Bot.GetMonsters().GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current.IsCode(targets2))
					{
						base.AI.SelectCard(targets2);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x000B30C8 File Offset: 0x000B12C8
		private bool RAMClouderEffect()
		{
			base.AI.SelectCard(new int[] { 70950699, 36211150, 32295838, 18789533, 62706865, 71172240, 9190563 });
			base.AI.SelectNextCard(new int[]
			{
				1861629, 6622715, 32617464, 79016563, 34472920, 7445307, 70950698, 8567955, 44956694, 35595518,
				9190563
			});
			return true;
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x000B3104 File Offset: 0x000B1304
		private bool DotScaperEffect()
		{
			base.AI.SelectPosition(CardPosition.FaceUpDefence);
			return true;
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x000B3113 File Offset: 0x000B1313
		private bool LinkSummon()
		{
			return (base.Util.IsTurn1OrMain2() || base.Util.IsOneEnemyBetter(false)) && base.Util.GetBestAttack(base.Bot) < base.Card.Attack;
		}

		// Token: 0x0400207C RID: 8316
		private bool BalancerLordUsed;

		// Token: 0x020003D0 RID: 976
		public class CardId
		{
			// Token: 0x0400207D RID: 8317
			public const int Digitron = 32295838;

			// Token: 0x0400207E RID: 8318
			public const int Bitron = 36211150;

			// Token: 0x0400207F RID: 8319
			public const int DualAssembloom = 7445307;

			// Token: 0x04002080 RID: 8320
			public const int BootStagguard = 70950698;

			// Token: 0x04002081 RID: 8321
			public const int Linkslayer = 35595518;

			// Token: 0x04002082 RID: 8322
			public const int RAMClouder = 9190563;

			// Token: 0x04002083 RID: 8323
			public const int ROMCloudia = 44956694;

			// Token: 0x04002084 RID: 8324
			public const int BalancerLord = 8567955;

			// Token: 0x04002085 RID: 8325
			public const int Backlinker = 71172240;

			// Token: 0x04002086 RID: 8326
			public const int Kleinant = 45778242;

			// Token: 0x04002087 RID: 8327
			public const int Draconnet = 62706865;

			// Token: 0x04002088 RID: 8328
			public const int DotScaper = 18789533;

			// Token: 0x04002089 RID: 8329
			public const int MindControl = 37520316;

			// Token: 0x0400208A RID: 8330
			public const int DarkHole = 53129443;

			// Token: 0x0400208B RID: 8331
			public const int MonsterReborn = 83764718;

			// Token: 0x0400208C RID: 8332
			public const int MysticalSpaceTyphoon = 5318639;

			// Token: 0x0400208D RID: 8333
			public const int CosmicCyclone = 8267140;

			// Token: 0x0400208E RID: 8334
			public const int BookOfMoon = 14087893;

			// Token: 0x0400208F RID: 8335
			public const int CynetBackdoor = 43839002;

			// Token: 0x04002090 RID: 8336
			public const int MoonMirrorShield = 19508728;

			// Token: 0x04002091 RID: 8337
			public const int CynetUniverse = 61583217;

			// Token: 0x04002092 RID: 8338
			public const int BottomlessTrapHole = 29401950;

			// Token: 0x04002093 RID: 8339
			public const int MirrorForce = 44095762;

			// Token: 0x04002094 RID: 8340
			public const int TorrentialTribute = 53582587;

			// Token: 0x04002095 RID: 8341
			public const int RecodedAlive = 70238111;

			// Token: 0x04002096 RID: 8342
			public const int DimensionalBarrier = 83326048;

			// Token: 0x04002097 RID: 8343
			public const int CompulsoryEvacuationDevice = 94192409;

			// Token: 0x04002098 RID: 8344
			public const int SolemnStrike = 40605147;

			// Token: 0x04002099 RID: 8345
			public const int DecodeTalker = 1861629;

			// Token: 0x0400209A RID: 8346
			public const int EncodeTalker = 6622715;

			// Token: 0x0400209B RID: 8347
			public const int TriGateWizard = 32617464;

			// Token: 0x0400209C RID: 8348
			public const int Honeybot = 34472920;

			// Token: 0x0400209D RID: 8349
			public const int BinarySorceress = 79016563;

			// Token: 0x0400209E RID: 8350
			public const int LinkSpider = 98978921;

			// Token: 0x0400209F RID: 8351
			public const int StagToken = 70950699;
		}
	}
}
