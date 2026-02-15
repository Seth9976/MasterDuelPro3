using System;
using System.Collections.Generic;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x020002BD RID: 701
	[Deck("BlueEyesMaxDragon", "AI_BlueEyesMaxDragon", "Normal")]
	public class BlueEyesMaxDragonExecutor : DefaultExecutor
	{
		// Token: 0x060010F1 RID: 4337 RVA: 0x000547B0 File Offset: 0x000529B0
		public BlueEyesMaxDragonExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 14558127, new Func<bool>(base.DefaultAshBlossomAndJoyousSpring));
			base.AddExecutor(ExecutorType.Activate, 23434538, new Func<bool>(this.MaxxCeff));
			base.AddExecutor(ExecutorType.Activate, 10045474, new Func<bool>(base.DefaultInfiniteImpermanence));
			base.AddExecutor(ExecutorType.Activate, 24224830, new Func<bool>(this.CalledByTheGraveeff));
			base.AddExecutor(ExecutorType.Activate, 70368879);
			base.AddExecutor(ExecutorType.Activate, 38517737, new Func<bool>(this.BlueEyesAlternativeWhiteDragoneff));
			base.AddExecutor(ExecutorType.Activate, 31036355, new Func<bool>(this.CreatureSwapeff));
			base.AddExecutor(ExecutorType.Activate, 48800175, new Func<bool>(this.TheMelodyOfAwakeningDragoneff));
			base.AddExecutor(ExecutorType.Summon, 95492061);
			base.AddExecutor(ExecutorType.Activate, 95492061, new Func<bool>(this.TenTousandHandseff));
			base.AddExecutor(ExecutorType.Activate, new Func<bool>(this.DeviritualCheck));
			base.AddExecutor(ExecutorType.Activate, 46052429);
			base.AddExecutor(ExecutorType.Activate, 21082832, new Func<bool>(this.ChaosFormeff));
			base.AddExecutor(ExecutorType.SpSummon, 3987233, new Func<bool>(this.MissusRadiantsp));
			base.AddExecutor(ExecutorType.Activate, 3987233, new Func<bool>(this.MissusRadianteff));
			base.AddExecutor(ExecutorType.Activate, 41999284, new Func<bool>(this.Linkuriboheff));
			base.AddExecutor(ExecutorType.SpSummon, 41999284, new Func<bool>(this.Linkuribohsp));
			base.AddExecutor(ExecutorType.SpSummon, 98978921);
			base.AddExecutor(ExecutorType.SpSummon, 85289965, new Func<bool>(this.BirrelswordDragonsp));
			base.AddExecutor(ExecutorType.Activate, 85289965, new Func<bool>(this.BirrelswordDragoneff));
			base.AddExecutor(ExecutorType.Activate, 48800175, new Func<bool>(this.TheMelodyOfAwakeningDragoneffsecond));
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(this.SpellSet));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(base.DefaultMonsterRepos));
			base.AddExecutor(ExecutorType.Activate, 37576645, new Func<bool>(this.RecklessGreedeff));
			base.AddExecutor(ExecutorType.Activate, 73915051, new Func<bool>(this.Scapegoateff));
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x000549DE File Offset: 0x00052BDE
		public override void OnNewTurn()
		{
			this.Talismandra_used = false;
			this.Candoll_used = false;
			base.OnNewTurn();
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x000549F4 File Offset: 0x00052BF4
		private void Count_check()
		{
			this.TheMelody_count = 0;
			this.Talismandra_count = 0;
			this.Candoll_count = 0;
			this.RitualArt_count = 0;
			this.ChaosForm_count = 0;
			this.MaxDragon_count = 0;
			foreach (ClientCard clientCard in base.Bot.Hand)
			{
				if (clientCard.IsCode(46052429))
				{
					this.RitualArt_count++;
				}
				if (clientCard.IsCode(21082832))
				{
					this.ChaosForm_count++;
				}
				if (clientCard.IsCode(53303460))
				{
					this.Candoll_count++;
				}
				if (clientCard.IsCode(80701178))
				{
					this.Talismandra_count++;
				}
				if (clientCard.IsCode(55410871))
				{
					this.MaxDragon_count++;
				}
				if (clientCard.IsCode(48800175))
				{
					this.TheMelody_count++;
				}
			}
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x000348E3 File Offset: 0x00032AE3
		private bool MaxxCeff()
		{
			return !base.DefaultCheckWhetherCardIsNegated(base.Card) && base.Duel.Player == 1;
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x00054B10 File Offset: 0x00052D10
		private bool CalledByTheGraveeff()
		{
			if (base.Duel.LastChainPlayer == 1)
			{
				ClientCard lastCard = base.Util.GetLastChainCard();
				if (lastCard.IsCode(23434538))
				{
					base.AI.SelectCard(23434538);
					if (base.Util.ChainContainsCard(48800175))
					{
						base.AI.SelectNextCard(new int[] { 55410871, 55410871, 38517737 });
					}
					return base.UniqueFaceupSpell();
				}
				if (lastCard.IsCode(94145021))
				{
					base.AI.SelectCard(94145021);
					if (base.Util.ChainContainsCard(48800175))
					{
						base.AI.SelectNextCard(new int[] { 55410871, 55410871, 38517737 });
					}
					return base.UniqueFaceupSpell();
				}
				if (lastCard.IsCode(59438930))
				{
					base.AI.SelectCard(59438930);
					if (base.Util.ChainContainsCard(48800175))
					{
						base.AI.SelectNextCard(new int[] { 55410871, 55410871, 38517737 });
					}
					return base.UniqueFaceupSpell();
				}
				if (lastCard.IsCode(14558127))
				{
					base.AI.SelectCard(14558127);
					if (base.Util.ChainContainsCard(48800175))
					{
						base.AI.SelectNextCard(new int[] { 55410871, 55410871, 38517737 });
					}
					return base.UniqueFaceupSpell();
				}
			}
			return false;
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x00054C84 File Offset: 0x00052E84
		private bool BlueEyesAlternativeWhiteDragoneff()
		{
			if (base.Card.Location == CardLocation.Hand)
			{
				return base.Duel.Turn != 1;
			}
			if (base.Util.GetProblematicEnemyMonster(3000, true) != null)
			{
				base.AI.SelectCard(base.Util.GetProblematicEnemyMonster(3000, true));
				return true;
			}
			return false;
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x00054CE4 File Offset: 0x00052EE4
		private bool CreatureSwapeff()
		{
			if (base.Bot.HasInMonstersZone(55410871, true, false, false) && base.Duel.Phase == DuelPhase.Main1 && (base.Bot.HasInMonstersZone(80701178, false, false, false) || base.Bot.HasInMonstersZone(53303460, false, false, false)))
			{
				base.AI.SelectCard(new int[] { 53303460, 80701178 });
				return true;
			}
			return false;
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x00054D64 File Offset: 0x00052F64
		private bool TheMelodyOfAwakeningDragoneff()
		{
			this.Count_check();
			if (this.TheMelody_count >= 2 && base.Bot.GetRemainingCount(55410871, 3) > 0)
			{
				base.AI.SelectCard(48800175);
				base.AI.SelectNextCard(new int[] { 55410871, 55410871, 38517737 });
				return true;
			}
			if (base.Bot.HasInHand(89631139) && base.Bot.GetRemainingCount(55410871, 3) > 0)
			{
				base.AI.SelectCard(89631139);
				base.AI.SelectNextCard(new int[] { 55410871, 55410871, 38517737 });
				return true;
			}
			return false;
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x00054E18 File Offset: 0x00053018
		private bool TheMelodyOfAwakeningDragoneffsecond()
		{
			this.Count_check();
			if (this.RitualArtCanUse() && base.Bot.GetRemainingCount(55410871, 3) > 0 && !base.Bot.HasInHand(55410871) && base.Bot.Hand.Count >= 3)
			{
				if (this.RitualArt_count >= 2)
				{
					foreach (ClientCard i in base.Bot.Hand)
					{
						if (i.IsCode(46052429))
						{
							base.AI.SelectCard(i);
						}
					}
				}
				foreach (ClientCard j in base.Bot.Hand)
				{
					if (!j.IsCode(46052429))
					{
						base.AI.SelectCard(j);
					}
				}
				base.AI.SelectNextCard(new int[] { 55410871, 55410871, 38517737 });
				return true;
			}
			return false;
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x00054F4C File Offset: 0x0005314C
		private bool TenTousandHandseff()
		{
			this.Count_check();
			if (this.Talismandra_count >= 2 && base.Bot.GetRemainingCount(55410871, 3) > 0)
			{
				base.AI.SelectCard(55410871);
				return true;
			}
			if (this.Candoll_count >= 2 || this.MaxDragon_count >= 2)
			{
				if (this.RitualArtCanUse() && base.Bot.GetRemainingCount(46052429, 3) > 0)
				{
					base.AI.SelectCard(46052429);
					return true;
				}
				if (this.ChaosFormCanUse() && base.Bot.GetRemainingCount(21082832, 1) > 0)
				{
					base.AI.SelectCard(21082832);
					return true;
				}
			}
			if (this.RitualArt_count + this.ChaosForm_count >= 2)
			{
				base.AI.SelectCard(55410871);
				return true;
			}
			if (this.Candoll_count + this.Talismandra_count > 1)
			{
				if (this.MaxDragon_count >= 1)
				{
					if (this.RitualArtCanUse() && base.Bot.GetRemainingCount(46052429, 3) > 0)
					{
						base.AI.SelectCard(46052429);
						return true;
					}
					if (this.ChaosFormCanUse() && base.Bot.GetRemainingCount(21082832, 1) > 0)
					{
						base.AI.SelectCard(21082832);
						return true;
					}
				}
				if (base.Bot.HasInHand(46052429) || base.Bot.HasInHand(21082832))
				{
					base.AI.SelectCard(55410871);
					return true;
				}
			}
			if (this.ChaosForm_count >= 1)
			{
				if (this.RitualArtCanUse() && base.Bot.GetRemainingCount(46052429, 3) > 0)
				{
					base.AI.SelectCard(46052429);
					return true;
				}
				if (this.ChaosFormCanUse() && base.Bot.GetRemainingCount(21082832, 1) > 0)
				{
					base.AI.SelectCard(21082832);
					return true;
				}
			}
			if (this.Talismandra_count >= 1)
			{
				base.AI.SelectCard(55410871);
				return true;
			}
			if (this.MaxDragon_count >= 1)
			{
				if (this.RitualArtCanUse() && base.Bot.GetRemainingCount(46052429, 3) > 0)
				{
					base.AI.SelectCard(46052429);
					return true;
				}
				if (this.ChaosFormCanUse() && base.Bot.GetRemainingCount(21082832, 1) > 0)
				{
					base.AI.SelectCard(21082832);
					return true;
				}
			}
			if (this.RitualArtCanUse() && base.Bot.GetRemainingCount(46052429, 3) > 0)
			{
				base.AI.SelectCard(46052429);
			}
			if (this.ChaosFormCanUse() && base.Bot.GetRemainingCount(21082832, 1) > 0)
			{
				base.AI.SelectCard(21082832);
			}
			return true;
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x0005520A File Offset: 0x0005340A
		private bool RitualArtCanUse()
		{
			return base.Bot.GetRemainingCount(89631139, 2) > 0;
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x00055220 File Offset: 0x00053420
		private bool ChaosFormCanUse()
		{
			ClientCard check = null;
			foreach (ClientCard i in base.Bot.GetGraveyardMonsters())
			{
				if (i.IsCode(new int[] { 38517737, 55410871, 89631139 }))
				{
					check = i;
				}
			}
			foreach (ClientCard j in base.Bot.Hand)
			{
				if (j.IsCode(89631139))
				{
					check = j;
				}
			}
			return check != null;
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x000552E0 File Offset: 0x000534E0
		private bool DeviritualCheck()
		{
			this.Count_check();
			if (base.Card.IsCode(new int[] { 80701178, 53303460 }))
			{
				if (base.Card.Location == CardLocation.MonsterZone)
				{
					if (this.RitualArtCanUse())
					{
						base.AI.SelectCard(46052429);
					}
					else
					{
						base.AI.SelectCard(21082832);
					}
					return true;
				}
				if (base.Card.Location == CardLocation.Hand)
				{
					if (base.Card.IsCode(53303460) && ((this.MaxDragon_count >= 2 && this.Talismandra_count >= 1) || this.Candoll_used))
					{
						return false;
					}
					if (base.Card.IsCode(80701178))
					{
						if ((this.RitualArt_count + this.ChaosForm_count >= 2 && this.Candoll_count >= 1) || this.Talismandra_used)
						{
							return false;
						}
						this.Talismandra_used = true;
						return true;
					}
					else
					{
						if (this.RitualArtCanUse())
						{
							this.Candoll_used = true;
							base.AI.SelectCard(46052429);
							return true;
						}
						if (this.ChaosFormCanUse())
						{
							this.Candoll_used = true;
							base.AI.SelectCard(21082832);
							return true;
						}
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x00055418 File Offset: 0x00053618
		private bool ChaosFormeff()
		{
			ClientCard check = null;
			foreach (ClientCard i in base.Bot.Graveyard)
			{
				if (i.IsCode(new int[] { 38517737, 55410871, 89631139 }))
				{
					check = i;
				}
			}
			if (check != null)
			{
				base.AI.SelectCard(55410871);
				base.AI.SelectNextCard(check);
				return true;
			}
			foreach (ClientCard j in base.Bot.Hand)
			{
				if (j.IsCode(89631139))
				{
					check = j;
				}
			}
			if (check != null)
			{
				base.AI.SelectCard(55410871);
				base.AI.SelectNextCard(check);
				return true;
			}
			return false;
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x0005550C File Offset: 0x0005370C
		private bool MissusRadiantsp()
		{
			IList<ClientCard> material_list = new List<ClientCard>();
			foreach (ClientCard monster in base.Bot.GetMonsters())
			{
				if (monster.HasAttribute(CardAttribute.Earth) && monster.Level == 1)
				{
					material_list.Add(monster);
				}
				if (material_list.Count == 2)
				{
					break;
				}
			}
			if (material_list.Count < 2)
			{
				return false;
			}
			if (base.Bot.HasInMonstersZone(3987233, false, false, false))
			{
				return false;
			}
			base.AI.SelectMaterials(material_list, 0);
			if (base.Bot.MonsterZone[0] == null && base.Bot.MonsterZone[2] == null && base.Bot.MonsterZone[5] == null)
			{
				base.AI.SelectPlace(32);
			}
			else
			{
				base.AI.SelectPlace(64);
			}
			return true;
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x00055600 File Offset: 0x00053800
		private bool MissusRadianteff()
		{
			base.AI.SelectCard(new int[] { 23434538, 3987233 });
			return true;
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x00055624 File Offset: 0x00053824
		private bool Linkuribohsp()
		{
			foreach (ClientCard c in base.Bot.GetMonsters())
			{
				if (!c.IsCode(41999284) && c.Level == 1)
				{
					base.AI.SelectMaterials(c, 0);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x000556A0 File Offset: 0x000538A0
		private bool Linkuriboheff()
		{
			return !base.DefaultCheckWhetherCardIsNegated(base.Card) && (base.Duel.LastChainPlayer != 0 || !base.Util.GetLastChainCard().IsCode(41999284));
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x000556DC File Offset: 0x000538DC
		private bool BirrelswordDragonsp()
		{
			IList<ClientCard> material_list = new List<ClientCard>();
			foreach (ClientCard i in base.Bot.GetMonsters())
			{
				if (i.IsCode(3987233))
				{
					material_list.Add(i);
					break;
				}
			}
			foreach (ClientCard j in base.Bot.GetMonsters())
			{
				if (j.IsCode(41999284) || j.Level == 1)
				{
					material_list.Add(j);
					if (material_list.Count == 3)
					{
						break;
					}
				}
			}
			if (material_list.Count == 3)
			{
				base.AI.SelectMaterials(material_list, 0);
				return true;
			}
			return false;
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x000557CC File Offset: 0x000539CC
		private bool BirrelswordDragoneff()
		{
			if (base.ActivateDescription != base.Util.GetStringId(85289965, 0))
			{
				return true;
			}
			if (base.Util.IsChainTarget(base.Card) && base.Util.GetBestEnemyMonster(true, true) != null)
			{
				base.AI.SelectCard(base.Util.GetBestEnemyMonster(true, true));
				return true;
			}
			if (base.Duel.Player == 1 && base.Bot.BattlingMonster == base.Card)
			{
				base.AI.SelectCard(base.Enemy.BattlingMonster);
				return true;
			}
			if (base.Duel.Player == 1 && base.Bot.BattlingMonster != null && base.Enemy.BattlingMonster.Attack - base.Bot.BattlingMonster.Attack >= base.Bot.LifePoints)
			{
				base.AI.SelectCard(base.Enemy.BattlingMonster);
				return true;
			}
			if (base.Duel.Player == 0 && base.Duel.Phase == DuelPhase.BattleStart)
			{
				foreach (ClientCard check in base.Enemy.GetMonsters())
				{
					if (check.IsAttack())
					{
						base.AI.SelectCard(check);
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x00055948 File Offset: 0x00053B48
		private bool SpellSet()
		{
			if (base.Card.IsCode(10045474))
			{
				return !base.Bot.IsFieldEmpty();
			}
			return base.Card.IsCode(37576645) || base.Card.IsCode(73915051);
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x000559A0 File Offset: 0x00053BA0
		private bool RecklessGreedeff()
		{
			int count = 0;
			using (List<ClientCard>.Enumerator enumerator = base.Bot.GetSpells().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsCode(37576645))
					{
						count++;
					}
				}
			}
			return base.DefaultOnBecomeTarget() || (base.Duel.Player == 0 && base.Duel.Phase >= DuelPhase.Main1 && (base.Bot.LifePoints <= 4000 || count >= 2));
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x00055A40 File Offset: 0x00053C40
		private bool Scapegoateff()
		{
			if (base.Duel.Player == 0)
			{
				return false;
			}
			if (base.Duel.Phase == DuelPhase.End)
			{
				return true;
			}
			if (base.Duel.LastChainPlayer == 1 && base.DefaultOnBecomeTarget())
			{
				return true;
			}
			if (base.Duel.Phase > DuelPhase.Main1 && base.Duel.Phase < DuelPhase.Main2)
			{
				int total_atk = 0;
				foreach (ClientCard i in base.Enemy.GetMonsters())
				{
					if (i.IsAttack() && !i.Attacked)
					{
						total_atk += i.Attack;
					}
				}
				if (total_atk >= base.Bot.LifePoints)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x00055B18 File Offset: 0x00053D18
		public override ClientCard OnSelectAttacker(IList<ClientCard> attackers, IList<ClientCard> defenders)
		{
			for (int i = 0; i < attackers.Count; i++)
			{
				ClientCard attacker = attackers[i];
				if (attacker.IsCode(55410871))
				{
					Logger.DebugWriteLine(attacker.Name);
					return attacker;
				}
			}
			return base.OnSelectAttacker(attackers, defenders);
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x00055B60 File Offset: 0x00053D60
		public override BattlePhaseAction OnSelectAttackTarget(ClientCard attacker, IList<ClientCard> defenders)
		{
			if (attacker.IsCode(55410871) && !attacker.IsDisabled() && base.Enemy.HasInMonstersZone(new int[] { 80701178, 53303460 }, false, false, false))
			{
				for (int i = 0; i < defenders.Count; i++)
				{
					ClientCard defender = defenders[i];
					attacker.RealPower = attacker.Attack;
					defender.RealPower = defender.GetDefensePower();
					if (this.OnPreBattleBetween(attacker, defender) && defender.IsCode(new int[] { 53303460, 80701178 }))
					{
						return base.AI.Attack(attacker, defender);
					}
				}
			}
			return base.OnSelectAttackTarget(attacker, defenders);
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnSelectHand()
		{
			return false;
		}

		// Token: 0x040015A0 RID: 5536
		private int Talismandra_count;

		// Token: 0x040015A1 RID: 5537
		private int Candoll_count;

		// Token: 0x040015A2 RID: 5538
		private bool Talismandra_used;

		// Token: 0x040015A3 RID: 5539
		private bool Candoll_used;

		// Token: 0x040015A4 RID: 5540
		private int RitualArt_count;

		// Token: 0x040015A5 RID: 5541
		private int ChaosForm_count;

		// Token: 0x040015A6 RID: 5542
		private int MaxDragon_count;

		// Token: 0x040015A7 RID: 5543
		private int TheMelody_count;

		// Token: 0x020002BE RID: 702
		public class CardId
		{
			// Token: 0x040015A8 RID: 5544
			public const int BlueEyesWhiteDragon = 89631139;

			// Token: 0x040015A9 RID: 5545
			public const int BlueEyesAlternativeWhiteDragon = 38517737;

			// Token: 0x040015AA RID: 5546
			public const int DeviritualTalismandra = 80701178;

			// Token: 0x040015AB RID: 5547
			public const int ManguOfTheTenTousandHands = 95492061;

			// Token: 0x040015AC RID: 5548
			public const int DevirrtualCandoll = 53303460;

			// Token: 0x040015AD RID: 5549
			public const int AshBlossom = 14558127;

			// Token: 0x040015AE RID: 5550
			public const int MaxxC = 23434538;

			// Token: 0x040015AF RID: 5551
			public const int BlueEyesChaosMaxDragon = 55410871;

			// Token: 0x040015B0 RID: 5552
			public const int CreatureSwap = 31036355;

			// Token: 0x040015B1 RID: 5553
			public const int TheMelodyOfAwakeningDragon = 48800175;

			// Token: 0x040015B2 RID: 5554
			public const int UpstartGoblin = 70368879;

			// Token: 0x040015B3 RID: 5555
			public const int ChaosForm = 21082832;

			// Token: 0x040015B4 RID: 5556
			public const int AdvancedRitualArt = 46052429;

			// Token: 0x040015B5 RID: 5557
			public const int CalledByTheGrave = 24224830;

			// Token: 0x040015B6 RID: 5558
			public const int Scapegoat = 73915051;

			// Token: 0x040015B7 RID: 5559
			public const int InfiniteImpermanence = 10045474;

			// Token: 0x040015B8 RID: 5560
			public const int RecklessGreed = 37576645;

			// Token: 0x040015B9 RID: 5561
			public const int BorreloadDragon = 31833038;

			// Token: 0x040015BA RID: 5562
			public const int BirrelswordDragon = 85289965;

			// Token: 0x040015BB RID: 5563
			public const int KnightmareGryphon = 65330383;

			// Token: 0x040015BC RID: 5564
			public const int MissusRadiant = 3987233;

			// Token: 0x040015BD RID: 5565
			public const int LinkSpider = 98978921;

			// Token: 0x040015BE RID: 5566
			public const int Linkuriboh = 41999284;

			// Token: 0x040015BF RID: 5567
			public const int LockBird = 94145021;

			// Token: 0x040015C0 RID: 5568
			public const int Ghost = 59438930;
		}
	}
}
