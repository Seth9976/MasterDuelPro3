using System;
using System.Collections.Generic;
using System.Linq;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x02000383 RID: 899
	[Deck("MathMech", "AI_Mathmech", "Normal")]
	public class MathmechExecutor : DefaultExecutor
	{
		// Token: 0x06001A80 RID: 6784 RVA: 0x0009C44C File Offset: 0x0009A64C
		public MathmechExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 12580477, new Func<bool>(this.when_raigeki));
			base.AddExecutor(ExecutorType.Activate, 70368879);
			base.AddExecutor(ExecutorType.Activate, 93104632);
			base.AddExecutor(ExecutorType.SpellSet, 36361633);
			base.AddExecutor(ExecutorType.Activate, 8267140, new Func<bool>(this.when_cosmic));
			base.AddExecutor(ExecutorType.Activate, 14532163, new Func<bool>(this.lightstorm_target));
			base.AddExecutor(ExecutorType.Activate, 81439173, new Func<bool>(this.foolish_burial_target));
			base.AddExecutor(ExecutorType.Activate, 14025912, new Func<bool>(this.mathmech_equation_target));
			base.AddExecutor(ExecutorType.Activate, 35261759);
			base.AddExecutor(ExecutorType.Summon, 53577438);
			base.AddExecutor(ExecutorType.Summon, 8567955);
			base.AddExecutor(ExecutorType.Summon, 52354896);
			base.AddExecutor(ExecutorType.Summon, 16360142);
			base.AddExecutor(ExecutorType.Summon, 80965043);
			base.AddExecutor(ExecutorType.Summon, 89743495);
			base.AddExecutor(ExecutorType.Summon, 89743495);
			base.AddExecutor(ExecutorType.Activate, 27182739);
			base.AddExecutor(ExecutorType.Activate, 36361633);
			base.AddExecutor(ExecutorType.SpSummon, 85692042, new Func<bool>(this.when_Mathmechalem));
			base.AddExecutor(ExecutorType.Activate, 85692042, new Func<bool>(this.mathchalenEffect));
			base.AddExecutor(ExecutorType.SpSummon, 42632209, new Func<bool>(this.FinalSigmaSummon));
			base.AddExecutor(ExecutorType.Activate, 52354896, new Func<bool>(this.doubleEffect));
			base.AddExecutor(ExecutorType.Activate, 53577438, new Func<bool>(this.NeblaEffect));
			base.AddExecutor(ExecutorType.Activate, 89743495, new Func<bool>(this.divisionEffect));
			base.AddExecutor(ExecutorType.Activate, 8567955, new Func<bool>(this.active_balancer));
			base.AddExecutor(ExecutorType.Activate, 16360142, new Func<bool>(this.whom_subtra));
			base.AddExecutor(ExecutorType.Activate, 80965043, new Func<bool>(this.whom_addition));
			base.AddExecutor(ExecutorType.Activate, 57160136, new Func<bool>(this.how_to_cynet_mine));
			base.AddExecutor(ExecutorType.SpSummon, 15248594, new Func<bool>(this.MagmaSummon));
			base.AddExecutor(ExecutorType.Activate, 42632209);
			base.AddExecutor(ExecutorType.Activate, 15248594);
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnSelectHand()
		{
			return false;
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x0009C695 File Offset: 0x0009A895
		private bool when_cosmic()
		{
			if (base.Enemy.GetSpellCount() > 1)
			{
				base.AI.SelectCard(base.Util.GetBestEnemySpell(false));
				return true;
			}
			return false;
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x0009C6BF File Offset: 0x0009A8BF
		private bool divisionEffect()
		{
			if (base.Enemy.GetMonsterCount() > 0)
			{
				base.AI.SelectCard(base.Util.GetBestEnemyMonster(true, true));
				return true;
			}
			return false;
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x0009C6EA File Offset: 0x0009A8EA
		private bool when_raigeki()
		{
			return base.Enemy.GetMonsterCount() > 3;
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x0009C6FD File Offset: 0x0009A8FD
		private bool whom_addition()
		{
			base.AI.SelectCard(base.Util.GetBestBotMonster(true));
			return true;
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x0009C718 File Offset: 0x0009A918
		private bool whom_subtra()
		{
			bool flag;
			try
			{
				base.AI.SelectCard(base.Util.GetBestEnemyMonster(true, true));
				flag = true;
			}
			catch (Exception)
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x0009C758 File Offset: 0x0009A958
		private bool active_balancer()
		{
			if (base.Bot.HasInHand(53577438))
			{
				base.AI.SelectCard(53577438);
				return true;
			}
			return true;
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x0009C780 File Offset: 0x0009A980
		private bool lightstorm_target()
		{
			if (base.Enemy.MonsterZone.ToList<ClientCard>().Count > base.Enemy.SpellZone.ToList<ClientCard>().Count && base.Enemy.MonsterZone.ToList<ClientCard>().Count > 3)
			{
				base.AI.SelectPlace(127);
				return true;
			}
			base.AI.SelectPlace(31);
			return true;
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x0009C7EE File Offset: 0x0009A9EE
		private bool mathmech_equation_target()
		{
			if (base.Bot.HasInGraveyard(53577438))
			{
				base.AI.SelectCard(53577438);
				return true;
			}
			base.AI.SelectCard(base.Util.GetBestBotMonster(true));
			return true;
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x0009C82C File Offset: 0x0009AA2C
		private bool foolish_burial_target()
		{
			base.AI.SelectCard(53577438);
			return true;
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x0009C83F File Offset: 0x0009AA3F
		private bool how_to_cynet_mine()
		{
			base.AI.SelectCard(base.Util.GetWorstBotMonster(false));
			if (!base.Bot.HasInHandOrInMonstersZoneOrInGraveyard(27182739))
			{
				base.AI.SelectNextCard(27182739);
				return true;
			}
			return true;
		}

		// Token: 0x06001A8C RID: 6796 RVA: 0x0009C880 File Offset: 0x0009AA80
		private bool when_Mathmechalem()
		{
			return !base.Bot.HasInMonstersZone(53577438, false, false, false) && (!base.Bot.HasInMonstersZone(27182739, false, false, false) || !base.Bot.HasInMonstersZone(52354896, false, false, false)) && !base.Bot.HasInMonstersZone(85692042, false, false, false);
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x0009C8E8 File Offset: 0x0009AAE8
		private bool FinalSigmaSummon()
		{
			if (base.Duel.Turn < 1)
			{
				return false;
			}
			if (base.Bot.HasInMonstersZone(52354896, false, false, false) && (base.Bot.HasInMonstersZone(27182739, false, false, false) || base.Bot.HasInMonstersZone(53577438, false, false, false)))
			{
				base.AI.SelectPosition(CardPosition.Attack);
				try
				{
					base.AI.SelectPlace(96);
				}
				catch
				{
				}
				return true;
			}
			return true;
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x0009C978 File Offset: 0x0009AB78
		private bool NeblaEffect()
		{
			if (base.Bot.HasInMonstersZone(16360142, false, false, false) || base.Bot.HasInMonstersZone(93104633, false, false, false) || base.Bot.HasInMonstersZone(27182739, false, false, false) || base.Bot.HasInMonstersZone(80965043, false, false, false) || base.Bot.HasInMonstersZone(85692042, false, false, false) || base.Bot.HasInMonstersZone(89743495, false, false, false))
			{
				List<int> cards = new List<int>();
				cards.Add(27182739);
				cards.Add(16360142);
				cards.Add(80965043);
				cards.Add(89743495);
				cards.Add(85692042);
				cards.Add(93104633);
				List<ClientCard> monsters = base.Bot.GetMonstersInMainZone();
				for (int i = 0; i < monsters.Count; i++)
				{
					if (cards.Contains(monsters[i].Id))
					{
						int id = monsters[i].Id;
						break;
					}
				}
				base.AI.SelectCard(93104633);
				base.AI.SelectNextCard(52354896);
				return true;
			}
			return base.Card.Location == CardLocation.Grave;
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x0009CAC5 File Offset: 0x0009ACC5
		private bool doubleEffect()
		{
			return base.Bot.HasInMonstersZone(53577438, false, false, false) || base.Bot.HasInMonstersZone(27182739, false, false, false) || base.Card.Location == CardLocation.Grave;
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x0009CB08 File Offset: 0x0009AD08
		private bool mathchalenEffect()
		{
			if (base.Duel.Turn < 1)
			{
				return false;
			}
			if (base.Bot.HasInHandOrInGraveyard(53577438) && !base.Bot.HasInMonstersZone(53577438, false, false, false) && base.Card.Location == CardLocation.FieldZone && base.Card.HasXyzMaterial(0))
			{
				base.AI.SelectCard(85692042);
				base.AI.SelectNextCard(53577438);
				return true;
			}
			if (base.Bot.HasInHandOrInGraveyard(52354896) && (base.Bot.HasInMonstersZone(53577438, false, false, false) || base.Bot.HasInMonstersZone(27182739, false, false, false)) && base.Card.Location == CardLocation.FieldZone && base.Card.HasXyzMaterial(0))
			{
				base.AI.SelectCard(85692042);
				base.AI.SelectNextCard(52354896);
				return true;
			}
			if (!base.Bot.HasInHandOrInGraveyard(53577438) && base.Card.HasXyzMaterial(2))
			{
				base.AI.SelectCard(53577438);
				base.AI.SelectThirdCard(53577438);
				return true;
			}
			if (!base.Bot.HasInHandOrInGraveyard(27182739) && base.Card.HasXyzMaterial(2))
			{
				base.AI.SelectCard(27182739);
				base.AI.SelectThirdCard(27182739);
				return true;
			}
			return false;
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x0009CC94 File Offset: 0x0009AE94
		private bool MagmaSummon()
		{
			return !base.Bot.HasInMonstersZone(53577438, false, false, false) && (!base.Bot.HasInMonstersZone(27182739, false, false, false) || !base.Bot.HasInMonstersZone(52354896, false, false, false));
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x0009CCE5 File Offset: 0x0009AEE5
		public override int OnSelectPlace(int cardId, int player, CardLocation location, int available)
		{
			if (cardId == 42632209)
			{
				if ((32 & available) > 0)
				{
					return 32;
				}
				if ((64 & available) > 0)
				{
					return 64;
				}
			}
			return base.OnSelectPlace(cardId, player, location, available);
		}

		// Token: 0x02000384 RID: 900
		public class CardId
		{
			// Token: 0x04001DBD RID: 7613
			public const int MathmechNebla = 53577438;

			// Token: 0x04001DBE RID: 7614
			public const int MathmechSigma = 27182739;

			// Token: 0x04001DBF RID: 7615
			public const int MathmechDivision = 89743495;

			// Token: 0x04001DC0 RID: 7616
			public const int MathmechAddition = 80965043;

			// Token: 0x04001DC1 RID: 7617
			public const int MathmechSubtra = 16360142;

			// Token: 0x04001DC2 RID: 7618
			public const int Mathmechdouble = 52354896;

			// Token: 0x04001DC3 RID: 7619
			public const int MathmechFinalSigma = 42632209;

			// Token: 0x04001DC4 RID: 7620
			public const int Mathmechalem = 85692042;

			// Token: 0x04001DC5 RID: 7621
			public const int MathmechMagma = 15248594;

			// Token: 0x04001DC6 RID: 7622
			public const int BalancerLord = 8567955;

			// Token: 0x04001DC7 RID: 7623
			public const int LightDragon = 61399402;

			// Token: 0x04001DC8 RID: 7624
			public const int upstartGoblin = 70368879;

			// Token: 0x04001DC9 RID: 7625
			public const int raigeki = 12580477;

			// Token: 0x04001DCA RID: 7626
			public const int cynetmining = 57160136;

			// Token: 0x04001DCB RID: 7627
			public const int PotOfDesires = 35261759;

			// Token: 0x04001DCC RID: 7628
			public const int lightningStorm = 14532163;

			// Token: 0x04001DCD RID: 7629
			public const int cosmicCyclone = 8267140;

			// Token: 0x04001DCE RID: 7630
			public const int foolishBurial = 81439173;

			// Token: 0x04001DCF RID: 7631
			public const int OneTimePasscode = 93104632;

			// Token: 0x04001DD0 RID: 7632
			public const int mathmechEquation = 14025912;

			// Token: 0x04001DD1 RID: 7633
			public const int threanteningRoar = 36361633;

			// Token: 0x04001DD2 RID: 7634
			public const int securitytoken = 93104633;
		}
	}
}
