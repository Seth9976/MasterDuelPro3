using System;
using System.Collections.Generic;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x02000415 RID: 1045
	[Deck("TimeThief", "AI_Timethief", "Normal")]
	public class TimeThiefExecutor : DefaultExecutor
	{
		// Token: 0x06002185 RID: 8581 RVA: 0x000D5AB0 File Offset: 0x000D3CB0
		public TimeThiefExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 12580477, new Func<bool>(base.DefaultDarkHole));
			base.AddExecutor(ExecutorType.Activate, 81439173, new Func<bool>(this.FoolishBurialTarget));
			base.AddExecutor(ExecutorType.Activate, 10877309, new Func<bool>(this.TimeThiefStartupEffect));
			base.AddExecutor(ExecutorType.Activate, 81670445);
			base.AddExecutor(ExecutorType.Activate, 49238328, new Func<bool>(this.PotofExtravaganceActivate));
			base.AddExecutor(ExecutorType.Activate, 18144506, new Func<bool>(base.DefaultHarpiesFeatherDusterFirst));
			base.AddExecutor(ExecutorType.Activate, 35261759, new Func<bool>(this.PotOfDesireseff));
			base.AddExecutor(ExecutorType.SpellSet, 98827725);
			base.AddExecutor(ExecutorType.SpellSet, 76587747);
			base.AddExecutor(ExecutorType.SpellSet, 18678554);
			base.AddExecutor(ExecutorType.SpellSet, 84749824);
			base.AddExecutor(ExecutorType.SpellSet, 40605147);
			base.AddExecutor(ExecutorType.SpellSet, 41420027);
			base.AddExecutor(ExecutorType.SpellSet, 36975314);
			base.AddExecutor(ExecutorType.Summon, 19891131);
			base.AddExecutor(ExecutorType.SpSummon, 65367484, new Func<bool>(this.SummonToDef));
			base.AddExecutor(ExecutorType.Summon, 56308388);
			base.AddExecutor(ExecutorType.Summon, 82496079);
			base.AddExecutor(ExecutorType.Summon, 67696066);
			base.AddExecutor(ExecutorType.Summon, 74578720);
			base.AddExecutor(ExecutorType.Summon, 71564252, new Func<bool>(this.ThunderKingRaiOhsummon));
			base.AddExecutor(ExecutorType.SpSummon, 55285840);
			base.AddExecutor(ExecutorType.SpSummon, 59208943);
			base.AddExecutor(ExecutorType.SpSummon, 46772449, new Func<bool>(base.DefaultEvilswarmExcitonKnightSummon));
			base.AddExecutor(ExecutorType.SpSummon, 12014404, new Func<bool>(this.GagagaCowboySummon));
			base.AddExecutor(ExecutorType.SpSummon, 84013237, new Func<bool>(base.DefaultNumberS39UtopiaTheLightningSummon));
			base.AddExecutor(ExecutorType.SpSummon, 86532744);
			base.AddExecutor(ExecutorType.SpSummon, 56832966);
			base.AddExecutor(ExecutorType.SpSummon, 16195942, new Func<bool>(this.DarkRebellionXyzDragonSummon));
			base.AddExecutor(ExecutorType.Activate, 98827725);
			base.AddExecutor(ExecutorType.Activate, 76587747, new Func<bool>(this.RetrograteEffect));
			base.AddExecutor(ExecutorType.Activate, 18678554);
			base.AddExecutor(ExecutorType.Activate, 84749824, new Func<bool>(base.DefaultSolemnWarning));
			base.AddExecutor(ExecutorType.Activate, 40605147, new Func<bool>(base.DefaultSolemnStrike));
			base.AddExecutor(ExecutorType.Activate, 41420027, new Func<bool>(base.DefaultSolemnJudgment));
			base.AddExecutor(ExecutorType.Activate, 36975314, new Func<bool>(this.Crackdowneff));
			base.AddExecutor(ExecutorType.Activate, 55285840, new Func<bool>(this.RedoerEffect));
			base.AddExecutor(ExecutorType.Activate, 59208943, new Func<bool>(this.PerpertuaEffect));
			base.AddExecutor(ExecutorType.Activate, 46772449, new Func<bool>(base.DefaultEvilswarmExcitonKnightEffect));
			base.AddExecutor(ExecutorType.Activate, 12014404);
			base.AddExecutor(ExecutorType.Activate, 56832966, new Func<bool>(base.DefaultNumberS39UtopiaTheLightningEffect));
			base.AddExecutor(ExecutorType.Activate, 16195942, new Func<bool>(this.DarkRebellionXyzDragonEffect));
			base.AddExecutor(ExecutorType.Activate, 19891131, new Func<bool>(this.RegulatorEffect));
			base.AddExecutor(ExecutorType.Activate, 56308388);
			base.AddExecutor(ExecutorType.Activate, 74578720);
			base.AddExecutor(ExecutorType.Activate, 67696066, new Func<bool>(this.TrickClownEffect));
			base.AddExecutor(ExecutorType.Activate, 82496079);
			base.AddExecutor(ExecutorType.Activate, 71564252, new Func<bool>(this.ThunderKingRaiOheff));
			base.AddExecutor(ExecutorType.Activate, 14558127, new Func<bool>(base.DefaultAshBlossomAndJoyousSpring));
			base.AddExecutor(ExecutorType.Activate, 23434538, new Func<bool>(base.DefaultMaxxC));
		}

		// Token: 0x06002186 RID: 8582 RVA: 0x000D5E64 File Offset: 0x000D4064
		public void SelectSTPlace(ClientCard card = null, bool avoid_Impermanence = false, List<int> avoid_list = null)
		{
			List<int> list = new List<int> { 0, 1, 2, 3, 4 };
			int i = list.Count;
			while (i-- > 1)
			{
				int index = Program.Rand.Next(i + 1);
				int temp = list[index];
				list[index] = list[i];
				list[i] = temp;
			}
			foreach (int seq in list)
			{
				int zone = (int)Math.Pow(2.0, (double)seq);
				if (base.Bot.SpellZone[seq] == null && (card == null || card.Location != CardLocation.Hand || !avoid_Impermanence) && (avoid_list == null || !avoid_list.Contains(seq)))
				{
					base.AI.SelectPlace(zone);
					return;
				}
			}
			base.AI.SelectPlace(0);
		}

		// Token: 0x06002187 RID: 8583 RVA: 0x000D5F74 File Offset: 0x000D4174
		public bool SpellNegatable(bool isCounter = false, ClientCard target = null)
		{
			if (target == null)
			{
				target = base.Card;
			}
			if (target.Location != CardLocation.SpellZone && target.Location != CardLocation.Hand)
			{
				return false;
			}
			if (base.Enemy.HasInMonstersZone(99916754, true, false, false) && !isCounter)
			{
				return true;
			}
			if (target.IsSpell())
			{
				if (base.Enemy.HasInMonstersZone(33198837, true, false, false))
				{
					return true;
				}
				if (base.Enemy.HasInSpellZone(61740673, true, false) || base.Bot.HasInSpellZone(61740673, true, false))
				{
					return true;
				}
				if (base.Enemy.HasInMonstersZone(37267041, true, false, false) || base.Bot.HasInMonstersZone(37267041, true, false, false))
				{
					return true;
				}
			}
			return target.IsTrap() && (base.Enemy.HasInSpellZone(51452091, true, false) || base.Bot.HasInSpellZone(51452091, true, false));
		}

		// Token: 0x06002188 RID: 8584 RVA: 0x000D6062 File Offset: 0x000D4262
		private bool SummonToDef()
		{
			base.AI.SelectPosition(CardPosition.Defence);
			return true;
		}

		// Token: 0x06002189 RID: 8585 RVA: 0x000D6074 File Offset: 0x000D4274
		private bool RegulatorEffect()
		{
			if (base.Card.Location != CardLocation.MonsterZone)
			{
				return base.Card.Location == CardLocation.Grave;
			}
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			base.AI.SelectCard(74578720);
			base.AI.SelectCard(56308388);
			return true;
		}

		// Token: 0x0600218A RID: 8586 RVA: 0x000D60D4 File Offset: 0x000D42D4
		private bool PerpertuaEffect()
		{
			if (base.Bot.HasInGraveyard(55285840))
			{
				base.AI.SelectCard(55285840);
				return true;
			}
			if (base.Bot.HasInMonstersZone(55285840, false, false, false))
			{
				base.AI.SelectCard(82496079);
				base.AI.SelectNextCard(55285840);
				return true;
			}
			return false;
		}

		// Token: 0x0600218B RID: 8587 RVA: 0x000D6140 File Offset: 0x000D4340
		private bool RedoerEffect()
		{
			List<ClientCard> enemy = base.Enemy.GetMonstersInMainZone();
			List<int> overlays = base.Card.Overlays;
			if (base.Duel.Phase == DuelPhase.Standby && base.AI.Executor.Util.GetStringId(55285840, 0) == base.ActivateDescription)
			{
				return true;
			}
			try
			{
				for (int i = 0; i < enemy.Count; i++)
				{
					this._totalAttack += enemy[i].Attack;
				}
				foreach (ClientCard t in base.Bot.GetMonsters())
				{
					this._totalBotAttack += t.Attack;
				}
				if (this._totalAttack > base.Bot.LifePoints + this._totalBotAttack)
				{
					return false;
				}
				foreach (ClientCard t2 in enemy)
				{
					if (t2.Attack >= 2400 && t2.IsAttack())
					{
						try
						{
							base.AI.SelectCard(t2.Id);
							base.AI.SelectCard(t2.Id);
						}
						catch
						{
						}
						return true;
					}
				}
			}
			catch
			{
			}
			return base.Bot.UnderAttack;
		}

		// Token: 0x0600218C RID: 8588 RVA: 0x000D631C File Offset: 0x000D451C
		private bool RetrograteEffect()
		{
			return base.Card.Owner == 1;
		}

		// Token: 0x0600218D RID: 8589 RVA: 0x000D6330 File Offset: 0x000D4530
		private bool TimeThiefStartupEffect()
		{
			if (base.Card.Location == CardLocation.Hand)
			{
				if (base.Bot.HasInHand(19891131) && base.Bot.GetMonsterCount() <= 0)
				{
					base.AI.SelectCard(19891131);
					return true;
				}
				if (base.Bot.HasInHand(56308388) && base.Bot.GetMonsterCount() > 1)
				{
					base.AI.SelectCard(56308388);
					return true;
				}
				return true;
			}
			else
			{
				if (base.Card.Location == CardLocation.Grave)
				{
					base.AI.SelectCard(74578720);
					base.AI.SelectCard(81670445);
					base.AI.SelectCard(18678554);
					return true;
				}
				return false;
			}
		}

		// Token: 0x0600218E RID: 8590 RVA: 0x000D63F3 File Offset: 0x000D45F3
		private bool FoolishBurialTarget()
		{
			base.AI.SelectCard(67696066);
			return true;
		}

		// Token: 0x0600218F RID: 8591 RVA: 0x00086D9B File Offset: 0x00084F9B
		private bool TrickClownEffect()
		{
			if (base.Bot.LifePoints <= 1000)
			{
				return false;
			}
			base.AI.SelectPosition(CardPosition.FaceUpDefence);
			return true;
		}

		// Token: 0x06002190 RID: 8592 RVA: 0x000D6408 File Offset: 0x000D4608
		private bool GagagaCowboySummon()
		{
			if (base.Enemy.LifePoints <= 800 || (base.Bot.GetMonsterCount() >= 4 && base.Enemy.LifePoints <= 1600))
			{
				base.AI.SelectPosition(CardPosition.FaceUpDefence);
				return true;
			}
			return false;
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x000D6458 File Offset: 0x000D4658
		private bool DarkRebellionXyzDragonSummon()
		{
			int bestAttack = base.Util.GetBestAttack(base.Bot);
			int oppoBestAttack = base.Util.GetBestAttack(base.Enemy);
			return bestAttack <= oppoBestAttack;
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x000D6490 File Offset: 0x000D4690
		private bool DarkRebellionXyzDragonEffect()
		{
			int oppoBestAttack = base.Util.GetBestAttack(base.Enemy);
			ClientCard target = base.Util.GetOneEnemyBetterThanValue(oppoBestAttack, true, false);
			if (target != null)
			{
				base.AI.SelectCard(0);
				base.AI.SelectNextCard(target);
			}
			return true;
		}

		// Token: 0x06002193 RID: 8595 RVA: 0x0006EC59 File Offset: 0x0006CE59
		private bool ThunderKingRaiOhsummon()
		{
			if (base.Bot.MonsterZone[0] == null)
			{
				base.AI.SelectPlace(1);
			}
			else
			{
				base.AI.SelectPlace(16);
			}
			return true;
		}

		// Token: 0x06002194 RID: 8596 RVA: 0x000D64DC File Offset: 0x000D46DC
		private bool ThunderKingRaiOheff()
		{
			if (base.DefaultOnlyHorusSpSummoning())
			{
				return false;
			}
			if (base.Duel.SummoningCards.Count > 0)
			{
				using (IEnumerator<ClientCard> enumerator = base.Duel.SummoningCards.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Attack >= 1900)
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06002195 RID: 8597 RVA: 0x000D6558 File Offset: 0x000D4758
		private bool Crackdowneff()
		{
			if (base.Util.GetOneEnemyBetterThanMyBest(true, true) != null && base.Bot.UnderAttack)
			{
				base.AI.SelectCard(base.Util.GetOneEnemyBetterThanMyBest(true, true));
			}
			return base.Util.GetOneEnemyBetterThanMyBest(true, true) != null && base.Bot.UnderAttack;
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x0006EB3C File Offset: 0x0006CD3C
		private bool PotOfDesireseff()
		{
			return base.Bot.Deck.Count > 14 && !base.DefaultSpellWillBeNegated();
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x000D65B5 File Offset: 0x000D47B5
		public bool PotofExtravaganceActivate()
		{
			if (this.SpellNegatable(false, null))
			{
				return false;
			}
			this.SelectSTPlace(base.Card, true, null);
			base.AI.SelectOption(1);
			return true;
		}

		// Token: 0x040023F1 RID: 9201
		private int _totalAttack;

		// Token: 0x040023F2 RID: 9202
		private int _totalBotAttack;

		// Token: 0x02000416 RID: 1046
		public class Monsters
		{
			// Token: 0x040023F3 RID: 9203
			public const int TimeThiefWinder = 56308388;

			// Token: 0x040023F4 RID: 9204
			public const int TimeThiefBezelShip = 82496079;

			// Token: 0x040023F5 RID: 9205
			public const int TimeThiefCronocorder = 74578720;

			// Token: 0x040023F6 RID: 9206
			public const int TimeThiefRegulator = 19891131;

			// Token: 0x040023F7 RID: 9207
			public const int PhotonTrasher = 65367484;

			// Token: 0x040023F8 RID: 9208
			public const int PerformTrickClown = 67696066;

			// Token: 0x040023F9 RID: 9209
			public const int ThunderKingRaiOh = 71564252;

			// Token: 0x040023FA RID: 9210
			public const int MaxxC = 23434538;

			// Token: 0x040023FB RID: 9211
			public const int AshBlossomAndJoyousSpring = 14558127;
		}

		// Token: 0x02000417 RID: 1047
		public class CardId
		{
			// Token: 0x040023FC RID: 9212
			public const int ImperialOrder = 61740673;

			// Token: 0x040023FD RID: 9213
			public const int NaturalExterio = 99916754;

			// Token: 0x040023FE RID: 9214
			public const int NaturalBeast = 33198837;

			// Token: 0x040023FF RID: 9215
			public const int SwordsmanLV7 = 37267041;

			// Token: 0x04002400 RID: 9216
			public const int RoyalDecreel = 51452091;
		}

		// Token: 0x02000418 RID: 1048
		public class Spells
		{
			// Token: 0x04002401 RID: 9217
			public const int Raigeki = 12580477;

			// Token: 0x04002402 RID: 9218
			public const int FoolishBurial = 81439173;

			// Token: 0x04002403 RID: 9219
			public const int TimeThiefStartup = 10877309;

			// Token: 0x04002404 RID: 9220
			public const int TimeThiefHack = 81670445;

			// Token: 0x04002405 RID: 9221
			public const int HarpieFeatherDuster = 18144506;

			// Token: 0x04002406 RID: 9222
			public const int PotOfDesires = 35261759;

			// Token: 0x04002407 RID: 9223
			public const int PotofExtravagance = 49238328;
		}

		// Token: 0x02000419 RID: 1049
		public class Traps
		{
			// Token: 0x04002408 RID: 9224
			public const int SolemnWarning = 84749824;

			// Token: 0x04002409 RID: 9225
			public const int SolemStrike = 40605147;

			// Token: 0x0400240A RID: 9226
			public const int SolemnJudgment = 41420027;

			// Token: 0x0400240B RID: 9227
			public const int TimeThiefRetrograte = 76587747;

			// Token: 0x0400240C RID: 9228
			public const int PhantomKnightsShade = 98827725;

			// Token: 0x0400240D RID: 9229
			public const int TimeThiefFlyBack = 18678554;

			// Token: 0x0400240E RID: 9230
			public const int Crackdown = 36975314;
		}

		// Token: 0x0200041A RID: 1050
		public class XYZs
		{
			// Token: 0x0400240F RID: 9231
			public const int TimeThiefRedoer = 55285840;

			// Token: 0x04002410 RID: 9232
			public const int TimeThiefPerpetua = 59208943;

			// Token: 0x04002411 RID: 9233
			public const int CrazyBox = 42421606;

			// Token: 0x04002412 RID: 9234
			public const int GagagaCowboy = 12014404;

			// Token: 0x04002413 RID: 9235
			public const int Number39Utopia = 84013237;

			// Token: 0x04002414 RID: 9236
			public const int NumberS39UtopiatheLightning = 56832966;

			// Token: 0x04002415 RID: 9237
			public const int NumberS39UtopiaOne = 86532744;

			// Token: 0x04002416 RID: 9238
			public const int DarkRebellionXyzDragon = 16195942;

			// Token: 0x04002417 RID: 9239
			public const int EvilswarmExcitonKnight = 46772449;
		}
	}
}
