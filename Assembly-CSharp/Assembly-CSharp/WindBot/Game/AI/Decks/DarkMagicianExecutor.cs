using System;
using System.Collections.Generic;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x020002D3 RID: 723
	[Deck("DarkMagician", "AI_DarkMagician", "Normal")]
	public class DarkMagicianExecutor : DefaultExecutor
	{
		// Token: 0x060011B3 RID: 4531 RVA: 0x0005A7C4 File Offset: 0x000589C4
		public DarkMagicianExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 40605147, new Func<bool>(this.SolemnStrikeeff));
			base.AddExecutor(ExecutorType.Activate, 14558127, new Func<bool>(this.ChainEnemy));
			base.AddExecutor(ExecutorType.Activate, 50954680, new Func<bool>(this.CrystalWingSynchroDragoneff));
			base.AddExecutor(ExecutorType.Activate, 23434538, new Func<bool>(this.MaxxCeff));
			base.AddExecutor(ExecutorType.Activate, 70368879, new Func<bool>(this.UpstartGoblineff));
			base.AddExecutor(ExecutorType.Activate, 47222536, new Func<bool>(this.DarkMagicalCircleeff));
			base.AddExecutor(ExecutorType.Activate, 89739383, new Func<bool>(this.SpellbookOfSecreteff));
			base.AddExecutor(ExecutorType.Activate, 41735184, new Func<bool>(this.DarkMagicInheritanceeff));
			base.AddExecutor(ExecutorType.Activate, 2314238, new Func<bool>(this.DarkMagicAttackeff));
			base.AddExecutor(ExecutorType.SpellSet, 40605147);
			base.AddExecutor(ExecutorType.SpellSet, 7922915, new Func<bool>(this.MagicianNavigationset));
			base.AddExecutor(ExecutorType.SpellSet, 48680970, new Func<bool>(this.EternalSoulset));
			base.AddExecutor(ExecutorType.Activate, 43722862, new Func<bool>(this.WindwitchIceBelleff));
			base.AddExecutor(ExecutorType.Activate, 71007216, new Func<bool>(this.WindwitchGlassBelleff));
			base.AddExecutor(ExecutorType.Activate, 70117860, new Func<bool>(this.WindwitchSnowBellsp));
			base.AddExecutor(ExecutorType.SpSummon, 14577226, new Func<bool>(this.WindwitchWinterBellsp));
			base.AddExecutor(ExecutorType.Activate, 14577226, new Func<bool>(this.WindwitchWinterBelleff));
			base.AddExecutor(ExecutorType.SpSummon, 50954680, new Func<bool>(this.CrystalWingSynchroDragonsp));
			base.AddExecutor(ExecutorType.SpSummon, 90036274, new Func<bool>(this.ClearWingFastDragonsp));
			base.AddExecutor(ExecutorType.Activate, 90036274, new Func<bool>(this.ClearWingFastDragoneff));
			base.AddExecutor(ExecutorType.SpSummon, 16691074, new Func<bool>(this.OddEyesAbsoluteDragonsp));
			base.AddExecutor(ExecutorType.Activate, 16691074, new Func<bool>(this.OddEyesAbsoluteDragoneff));
			base.AddExecutor(ExecutorType.Activate, 58074177);
			base.AddExecutor(ExecutorType.Summon, 71007216, new Func<bool>(this.WindwitchGlassBellsummonfirst));
			base.AddExecutor(ExecutorType.Summon, 14824019, new Func<bool>(this.SpellbookMagicianOfProphecysummon));
			base.AddExecutor(ExecutorType.Activate, 14824019, new Func<bool>(this.SpellbookMagicianOfProphecyeff));
			base.AddExecutor(ExecutorType.Summon, 7084129, new Func<bool>(this.MagiciansRodsummon));
			base.AddExecutor(ExecutorType.Activate, 7084129, new Func<bool>(this.MagiciansRodeff));
			base.AddExecutor(ExecutorType.Summon, 71007216, new Func<bool>(this.WindwitchGlassBellsummon));
			base.AddExecutor(ExecutorType.Activate, 73616671, new Func<bool>(this.LllusionMagiceff));
			base.AddExecutor(ExecutorType.SpellSet, 73616671, new Func<bool>(this.LllusionMagicset));
			base.AddExecutor(ExecutorType.Activate, 23314220, new Func<bool>(this.SpellbookOfKnowledgeeff));
			base.AddExecutor(ExecutorType.Activate, 67775894, new Func<bool>(this.WonderWandeff));
			base.AddExecutor(ExecutorType.Activate, 1784686, new Func<bool>(this.TheEyeOfTimaeuseff));
			base.AddExecutor(ExecutorType.SpSummon, 30603688, new Func<bool>(this.ApprenticeLllusionMagiciansp));
			base.AddExecutor(ExecutorType.Activate, 30603688, new Func<bool>(this.ApprenticeLllusionMagicianeff));
			base.AddExecutor(ExecutorType.Activate, 35191415);
			base.AddExecutor(ExecutorType.Activate, 7922915, new Func<bool>(this.MagicianNavigationeff));
			base.AddExecutor(ExecutorType.Activate, 48680970, new Func<bool>(this.EternalSouleff));
			base.AddExecutor(ExecutorType.SpSummon, 80117527, new Func<bool>(this.BigEyesp));
			base.AddExecutor(ExecutorType.Activate, 80117527, new Func<bool>(this.BigEyeeff));
			base.AddExecutor(ExecutorType.SpSummon, 22110647, new Func<bool>(this.Dracossacksp));
			base.AddExecutor(ExecutorType.Activate, 22110647, new Func<bool>(this.Dracossackeff));
			base.AddExecutor(ExecutorType.SpSummon, 71384012, new Func<bool>(this.ApprenticeWitchlingsp));
			base.AddExecutor(ExecutorType.Activate, 71384012, new Func<bool>(this.ApprenticeWitchlingeff));
			base.AddExecutor(ExecutorType.SpSummon, 1482001, new Func<bool>(this.VentriloauistsClaraAndLucikasp));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(this.MonsterRepos));
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x0005AC26 File Offset: 0x00058E26
		private void EternalSoulSelect()
		{
			base.AI.SelectPosition(CardPosition.FaceUpAttack);
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x0005AC34 File Offset: 0x00058E34
		public override void OnNewPhase()
		{
			this.plan_C = false;
			this.ApprenticeLllusionMagician_count = 0;
			foreach (ClientCard count in base.Bot.GetMonsters())
			{
				if (count.IsCode(30603688) && count.IsFaceup())
				{
					this.ApprenticeLllusionMagician_count++;
				}
			}
			foreach (ClientCard dangerous in base.Enemy.GetMonsters())
			{
				if (dangerous != null && dangerous.IsShouldNotBeTarget() && dangerous.Attack > 2500 && !base.Bot.HasInHandOrHasInMonstersZone(30603688))
				{
					this.plan_C = true;
					Logger.DebugWriteLine("*********dangerous = " + dangerous.Id.ToString());
				}
			}
			if (base.Bot.HasInHand(14824019) && base.Bot.HasInHand(7084129) && base.Bot.HasInHand(71007216))
			{
				if (base.Bot.HasInHand(23314220) || base.Bot.HasInHand(67775894))
				{
					this.Rod_summon = true;
					return;
				}
				this.Spellbook_summon = true;
				return;
			}
			else if (base.Bot.HasInHand(14824019) && base.Bot.HasInHand(7084129))
			{
				if (base.Bot.HasInSpellZone(48680970, false, false) && !base.Bot.HasInHand(46986414) && !base.Bot.HasInHand(46986414))
				{
					this.Rod_summon = true;
					return;
				}
				if (base.Bot.HasInHand(23314220) || base.Bot.HasInHand(67775894))
				{
					this.Rod_summon = true;
					return;
				}
				this.Spellbook_summon = true;
				return;
			}
			else if (base.Bot.HasInHand(14824019) && base.Bot.HasInHand(71007216))
			{
				if (this.plan_A)
				{
					this.Rod_summon = true;
					return;
				}
				this.GlassBell_summon = true;
				return;
			}
			else
			{
				if (!base.Bot.HasInHand(7084129) || !base.Bot.HasInHand(71007216))
				{
					this.Spellbook_summon = true;
					this.Rod_summon = true;
					this.GlassBell_summon = true;
					return;
				}
				if (this.plan_A)
				{
					this.Rod_summon = true;
					return;
				}
				this.GlassBell_summon = true;
				return;
			}
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x0005AED8 File Offset: 0x000590D8
		public override void OnNewTurn()
		{
			this.CrystalWingSynchroDragon_used = false;
			this.Secret_used = false;
			this.maxxc_used = false;
			this.lockbird_used = false;
			this.ghost_used = false;
			this.WindwitchGlassBelleff_used = false;
			this.Spellbook_summon = false;
			this.Rod_summon = false;
			this.GlassBell_summon = false;
			this.magician_sp = false;
			this.big_attack = false;
			this.big_attack_used = false;
			this.soul_used = false;
			base.OnNewTurn();
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x0005AF48 File Offset: 0x00059148
		public int GetTotalATK(IList<ClientCard> list)
		{
			int atk = 0;
			foreach (ClientCard c in list)
			{
				if (c != null)
				{
					atk += c.Attack;
				}
			}
			return atk;
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x0005AF98 File Offset: 0x00059198
		private bool WindwitchIceBelleff()
		{
			if (this.lockbird_used)
			{
				return false;
			}
			if (base.Enemy.HasInMonstersZone(94977269, false, false, false))
			{
				return false;
			}
			if (this.maxxc_used)
			{
				return false;
			}
			if (this.WindwitchGlassBelleff_used)
			{
				return false;
			}
			if (base.Bot.GetRemainingCount(71007216, 2) >= 1)
			{
				base.AI.SelectCard(71007216);
			}
			else if (base.Bot.HasInHand(71007216))
			{
				base.AI.SelectCard(70117860);
			}
			base.AI.SelectPosition(CardPosition.FaceUpDefence);
			return true;
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x0005B030 File Offset: 0x00059230
		private bool WindwitchGlassBelleff()
		{
			if (base.Bot.HasInMonstersZone(43722862, false, false, false))
			{
				int ghost_count = 0;
				using (IEnumerator<ClientCard> enumerator = base.Enemy.Graveyard.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.IsCode(59438930))
						{
							ghost_count++;
						}
					}
				}
				if (ghost_count != this.ghost_done)
				{
					base.AI.SelectCard(43722862);
				}
				else
				{
					base.AI.SelectCard(70117860);
				}
			}
			else
			{
				base.AI.SelectCard(43722862);
			}
			this.WindwitchGlassBelleff_used = true;
			return true;
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x0005B0E8 File Offset: 0x000592E8
		private bool WindwitchSnowBellsp()
		{
			if (this.maxxc_used)
			{
				return false;
			}
			if (base.Bot.HasInMonstersZone(43722862, false, false, false) && base.Bot.HasInMonstersZone(71007216, false, false, false))
			{
				base.AI.SelectPosition(CardPosition.FaceUpDefence);
				return true;
			}
			return false;
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0005B138 File Offset: 0x00059338
		private bool WindwitchWinterBellsp()
		{
			if (this.maxxc_used)
			{
				return false;
			}
			if (base.Bot.HasInMonstersZone(43722862, false, false, false) && base.Bot.HasInMonstersZone(71007216, false, false, false) && base.Bot.HasInMonstersZone(70117860, false, false, false))
			{
				base.AI.SelectCard(new int[] { 43722862, 71007216 });
				base.AI.SelectPosition(CardPosition.FaceUpAttack);
				return true;
			}
			return false;
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x0005B1BE File Offset: 0x000593BE
		private bool WindwitchWinterBelleff()
		{
			base.AI.SelectCard(71007216);
			return true;
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x0005B1D1 File Offset: 0x000593D1
		private bool ClearWingFastDragonsp()
		{
			if (base.Bot.HasInMonstersZone(43722862, false, false, false) && base.Bot.HasInMonstersZone(71007216, false, false, false))
			{
				base.AI.SelectPosition(CardPosition.FaceUpAttack);
				return true;
			}
			return false;
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x0005B20C File Offset: 0x0005940C
		private bool ClearWingFastDragoneff()
		{
			return base.Card.Location == CardLocation.MonsterZone && (base.Duel.Player != 1 || base.DefaultTrap());
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x0005B234 File Offset: 0x00059434
		private bool CrystalWingSynchroDragonsp()
		{
			if (base.Bot.HasInMonstersZone(70117860, false, false, false) && base.Bot.HasInMonstersZone(14577226, false, false, false))
			{
				this.plan_A = true;
				return true;
			}
			return false;
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x0005B26A File Offset: 0x0005946A
		private bool OddEyesAbsoluteDragonsp()
		{
			return this.plan_C;
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x0005B278 File Offset: 0x00059478
		private bool OddEyesAbsoluteDragoneff()
		{
			Logger.DebugWriteLine("OddEyesAbsoluteDragonef 1");
			if (base.Card.Location == CardLocation.MonsterZone)
			{
				Logger.DebugWriteLine("OddEyesAbsoluteDragonef 2");
				return base.Duel.Player == 1;
			}
			if (base.Card.Location == CardLocation.Grave)
			{
				Logger.DebugWriteLine("OddEyesAbsoluteDragonef 3");
				base.AI.SelectCard(58074177);
				return true;
			}
			return false;
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x0005B2E2 File Offset: 0x000594E2
		private bool SolemnStrikeeff()
		{
			if (base.Bot.LifePoints > 1500 && base.Duel.LastChainPlayer == 1)
			{
				return true;
			}
			base.DefaultOnlyHorusSpSummoning();
			return false;
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x0005B310 File Offset: 0x00059510
		private bool ChainEnemy()
		{
			return !base.DefaultCheckWhetherCardIsNegated(base.Card) && (base.Util.GetLastChainCard() == null || !base.Util.GetLastChainCard().IsCode(70368879)) && base.Duel.LastChainPlayer == 1;
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x0005B361 File Offset: 0x00059561
		private bool CrystalWingSynchroDragoneff()
		{
			if (base.Duel.LastChainPlayer == 1)
			{
				this.CrystalWingSynchroDragon_used = true;
				return true;
			}
			return false;
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x000348E3 File Offset: 0x00032AE3
		private bool MaxxCeff()
		{
			return !base.DefaultCheckWhetherCardIsNegated(base.Card) && base.Duel.Player == 1;
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x0005B37B File Offset: 0x0005957B
		private bool EternalSoulset()
		{
			return base.Bot.GetHandCount() > 6 || !base.Bot.HasInSpellZone(48680970, false, false);
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x0005B3A4 File Offset: 0x000595A4
		private bool EternalSouleff()
		{
			IEnumerable<ClientCard> graveyard = base.Bot.Graveyard;
			IList<ClientCard> magician = new List<ClientCard>();
			foreach (ClientCard check in graveyard)
			{
				if (check.IsCode(46986414))
				{
					magician.Add(check);
				}
			}
			if (base.Util.IsChainTarget(base.Card) && base.Bot.GetMonsterCount() == 0)
			{
				base.AI.SelectYesNo(false);
				return true;
			}
			if (base.Util.ChainCountPlayer(0) > 0)
			{
				return false;
			}
			if (base.Enemy.HasInSpellZone(18144506, false, false) && base.Card.IsFacedown())
			{
				return false;
			}
			using (IEnumerator<ClientCard> enumerator = base.Duel.ChainTargets.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsCode(new int[] { 46986414, 41721210 }) && base.Card.IsFacedown())
					{
						base.AI.SelectYesNo(false);
						return true;
					}
				}
			}
			if (base.Enemy.HasInSpellZone(53129443, false, false) && base.Card.IsFacedown() && (base.Bot.HasInMonstersZone(46986414, false, false, false) || base.Bot.HasInMonstersZone(41721210, false, false, false)))
			{
				base.AI.SelectYesNo(false);
				return true;
			}
			if (base.Bot.HasInGraveyard(41721210) && !base.Bot.HasInMonstersZone(41721210, false, false, false) && !this.plan_C)
			{
				this.EternalSoulSelect();
				base.AI.SelectCard(41721210);
				return true;
			}
			if (base.Duel.Player == 1 && base.Bot.HasInSpellZone(47222536, false, false) && (base.Enemy.HasInMonstersZone(61665245, false, false, false) || base.Enemy.HasInMonstersZone(5043010, false, false, false)))
			{
				this.soul_used = true;
				this.magician_sp = true;
				this.EternalSoulSelect();
				base.AI.SelectCard(magician);
				return true;
			}
			if (base.Duel.Player == 1 && base.Duel.Phase == DuelPhase.BattleStart && base.Enemy.GetMonsterCount() > 0)
			{
				if (base.Card.IsFacedown() && base.Bot.HasInMonstersZone(1482001, false, false, false))
				{
					base.AI.SelectYesNo(false);
					return true;
				}
				if (base.Card.IsFacedown() && (base.Bot.HasInMonstersZone(46986414, false, false, false) || base.Bot.HasInMonstersZone(41721210, false, false, false)))
				{
					base.AI.SelectYesNo(false);
					return true;
				}
				if (base.Bot.HasInGraveyard(41721210) || base.Bot.HasInGraveyard(46986414))
				{
					this.soul_used = true;
					this.magician_sp = true;
					this.EternalSoulSelect();
					base.AI.SelectCard(magician);
					return true;
				}
				if (base.Bot.HasInHand(46986414))
				{
					this.soul_used = true;
					this.magician_sp = true;
					base.AI.SelectCard(46986414);
					this.EternalSoulSelect();
					return true;
				}
			}
			if (base.Duel.Player == 0 && base.Duel.Phase == DuelPhase.Main1)
			{
				if (base.Bot.HasInHand(47222536) && !base.Bot.HasInSpellZone(47222536, false, false))
				{
					return false;
				}
				if (base.Bot.HasInGraveyard(41721210) || base.Bot.HasInGraveyard(46986414))
				{
					this.soul_used = true;
					this.magician_sp = true;
					base.AI.SelectCard(magician);
					this.EternalSoulSelect();
					return true;
				}
				if (base.Bot.HasInHand(46986414))
				{
					this.soul_used = true;
					this.magician_sp = true;
					base.AI.SelectCard(46986414);
					this.EternalSoulSelect();
					return true;
				}
			}
			if (base.Duel.Phase != DuelPhase.End)
			{
				return false;
			}
			if (base.Card.IsFacedown() && base.Bot.HasInMonstersZone(1482001, false, false, false))
			{
				base.AI.SelectYesNo(false);
				return true;
			}
			if (base.Bot.HasInGraveyard(41721210) || base.Bot.HasInGraveyard(46986414))
			{
				this.soul_used = true;
				this.magician_sp = true;
				base.AI.SelectCard(magician);
				this.EternalSoulSelect();
				return true;
			}
			if (base.Bot.HasInHand(46986414))
			{
				this.soul_used = true;
				this.magician_sp = true;
				base.AI.SelectCard(46986414);
				this.EternalSoulSelect();
				return true;
			}
			return true;
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x0005B8A4 File Offset: 0x00059AA4
		private bool MagicianNavigationset()
		{
			return base.Bot.GetHandCount() > 6 || base.Bot.HasInSpellZone(73616671, false, false) || (base.Bot.HasInHand(46986414) && !base.Bot.HasInSpellZone(7922915, false, false));
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x0005B900 File Offset: 0x00059B00
		private bool MagicianNavigationeff()
		{
			bool spell_act = false;
			IList<ClientCard> spell = new List<ClientCard>();
			if (base.Duel.LastChainPlayer == 1)
			{
				foreach (ClientCard check in base.Enemy.GetSpells())
				{
					if (base.Util.GetLastChainCard() == check)
					{
						spell.Add(check);
						spell_act = true;
						break;
					}
				}
			}
			bool soul_faceup = false;
			foreach (ClientCard check2 in base.Bot.GetSpells())
			{
				if (check2.IsCode(48680970) && check2.IsFaceup())
				{
					soul_faceup = true;
				}
			}
			if (base.Card.Location == CardLocation.Grave && spell_act)
			{
				Logger.DebugWriteLine("**********************Navigationeff***********");
				base.AI.SelectCard(spell);
				return true;
			}
			if (base.Util.IsChainTarget(base.Card))
			{
				base.AI.SelectPlace(17);
				base.AI.SelectCard(46986414);
				if (base.Util.GetOneEnemyBetterThanValue(2500, true, false) != null)
				{
					base.AI.SelectNextCard(new int[] { 30603688, 46986414, 35191415 });
				}
				else
				{
					base.AI.SelectNextCard(new int[] { 30603688, 46986414, 35191415 });
				}
				this.magician_sp = true;
				return base.UniqueFaceupSpell();
			}
			if (base.DefaultOnBecomeTarget() && !soul_faceup)
			{
				base.AI.SelectPlace(17);
				base.AI.SelectCard(46986414);
				if (base.Util.GetOneEnemyBetterThanValue(2500, true, false) != null)
				{
					base.AI.SelectNextCard(new int[] { 30603688, 46986414, 35191415 });
				}
				else
				{
					base.AI.SelectNextCard(new int[] { 30603688, 46986414, 35191415 });
				}
				this.magician_sp = true;
				return true;
			}
			if (base.Duel.Player == 0 && base.Card.Location == CardLocation.SpellZone && !this.maxxc_used && base.Bot.HasInHand(46986414))
			{
				base.AI.SelectPlace(17);
				base.AI.SelectCard(46986414);
				if (base.Util.GetOneEnemyBetterThanValue(2500, true, false) != null)
				{
					base.AI.SelectNextCard(new int[] { 30603688, 46986414, 35191415 });
				}
				else
				{
					base.AI.SelectNextCard(new int[] { 30603688, 46986414, 35191415 });
				}
				this.magician_sp = true;
				return base.UniqueFaceupSpell();
			}
			if (base.Duel.Player == 1 && base.Bot.HasInSpellZone(47222536, false, false) && (base.Enemy.HasInMonstersZone(61665245, false, false, false) || base.Enemy.HasInMonstersZone(5043010, false, false, false)) && base.Card.Location == CardLocation.SpellZone)
			{
				base.AI.SelectPlace(17);
				base.AI.SelectCard(46986414);
				if (base.Util.GetOneEnemyBetterThanValue(2500, true, false) != null)
				{
					base.AI.SelectNextCard(new int[] { 30603688, 46986414, 35191415 });
				}
				else
				{
					base.AI.SelectNextCard(new int[] { 30603688, 46986414, 35191415 });
				}
				this.magician_sp = true;
				return base.UniqueFaceupSpell();
			}
			if (base.Enemy.GetFieldCount() > 0 && (base.Duel.Phase == DuelPhase.BattleStart || base.Duel.Phase == DuelPhase.End) && base.Card.Location == CardLocation.SpellZone && !this.maxxc_used)
			{
				base.AI.SelectPlace(17);
				base.AI.SelectCard(46986414);
				if (base.Util.GetOneEnemyBetterThanValue(2500, true, false) != null)
				{
					base.AI.SelectNextCard(new int[] { 30603688, 46986414, 35191415 });
				}
				else
				{
					base.AI.SelectNextCard(new int[] { 30603688, 46986414, 35191415 });
				}
				this.magician_sp = true;
				return base.UniqueFaceupSpell();
			}
			return false;
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x0005BD70 File Offset: 0x00059F70
		private bool DarkMagicalCircleeff()
		{
			if (base.Card.Location == CardLocation.Hand)
			{
				return base.Bot.LifePoints <= 4000 || base.UniqueFaceupSpell();
			}
			if (this.magician_sp)
			{
				base.AI.SelectCard(base.Util.GetBestEnemyCard(false, true));
				if (base.Util.GetBestEnemyCard(false, true) != null)
				{
					Logger.DebugWriteLine("*************SelectCard= " + base.Util.GetBestEnemyCard(false, true).Id.ToString());
				}
				this.magician_sp = false;
			}
			return true;
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x0005BE08 File Offset: 0x0005A008
		private bool LllusionMagicset()
		{
			return base.Bot.GetMonsterCount() >= 1 && (base.Bot.GetMonsterCount() != 1 || !base.Bot.HasInMonstersZone(50954680, false, false, false)) && (base.Bot.GetMonsterCount() != 1 || !base.Bot.HasInMonstersZone(90036274, false, false, false)) && (base.Bot.GetMonsterCount() != 1 || !base.Bot.HasInMonstersZone(1482001, false, false, false));
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x0005BE90 File Offset: 0x0005A090
		private bool LllusionMagiceff()
		{
			if (this.lockbird_used)
			{
				return false;
			}
			if (base.Duel.LastChainPlayer == 0)
			{
				return false;
			}
			ClientCard target = null;
			bool soul_exist = false;
			foreach (ClientCard i in base.Bot.GetSpells())
			{
				if (i.IsCode(48680970) && i.IsFaceup())
				{
					soul_exist = true;
				}
			}
			if (!this.soul_used && soul_exist && base.Bot.HasInMonstersZone(7084129, false, false, false))
			{
				base.AI.SelectCard(7084129);
				base.AI.SelectNextCard(new int[] { 46986414, 46986414 });
				return true;
			}
			if (base.Duel.Player == 0)
			{
				int ghost_count = 0;
				using (IEnumerator<ClientCard> enumerator2 = base.Enemy.Graveyard.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current.IsCode(59438930))
						{
							ghost_count++;
						}
					}
				}
				if (ghost_count != this.ghost_done && base.Duel.CurrentChain.Count >= 2 && base.Util.GetLastChainCard().IsCode(0))
				{
					base.AI.SelectCard(7084129);
					base.AI.SelectNextCard(new int[] { 46986414, 46986414 });
					return true;
				}
				int count = 0;
				foreach (ClientCard j in base.Bot.GetMonsters())
				{
					if (base.Util.IsChainTarget(j))
					{
						count++;
						target = j;
						Logger.DebugWriteLine("************IsChainTarget= " + target.Id.ToString());
						break;
					}
				}
				if (count == 0)
				{
					return false;
				}
				if (target.IsCode(new int[] { 71007216, 43722862 }) && base.Bot.HasInMonstersZone(43722862, false, false, false) && base.Bot.HasInMonstersZone(71007216, false, false, false))
				{
					return false;
				}
				base.AI.SelectCard(target);
				base.AI.SelectNextCard(new int[] { 46986414, 46986414 });
				return true;
			}
			else
			{
				if (base.Bot.HasInMonstersZone(7084129, false, false, false) || base.Bot.HasInMonstersZone(14824019, false, false, false))
				{
					base.AI.SelectCard(new int[] { 7084129, 14824019 });
					base.AI.SelectNextCard(new int[] { 46986414, 46986414 });
					return true;
				}
				if (base.Duel.Player == 1 && base.Bot.HasInMonstersZone(71007216, false, false, false))
				{
					base.AI.SelectCard(71007216);
					base.AI.SelectNextCard(new int[] { 46986414, 46986414 });
					return true;
				}
				if (base.Duel.Player == 1 && base.Bot.HasInMonstersZone(43722862, false, false, false))
				{
					base.AI.SelectCard(43722862);
					base.AI.SelectNextCard(new int[] { 46986414, 46986414 });
					return true;
				}
				if (base.Duel.Player == 1 && base.Bot.HasInMonstersZone(70117860, false, false, false))
				{
					base.AI.SelectCard(70117860);
					base.AI.SelectNextCard(new int[] { 46986414, 46986414 });
					return true;
				}
				if (base.Duel.Player == 1 && base.Bot.HasInMonstersZone(14824019, false, false, false))
				{
					base.AI.SelectCard(14824019);
					base.AI.SelectNextCard(new int[] { 46986414, 46986414 });
					return true;
				}
				if (base.Duel.Player == 1 && base.Bot.HasInMonstersZone(30603688, false, false, false) && (base.Bot.HasInSpellZone(48680970, false, false) || base.Bot.HasInSpellZone(7922915, false, false)))
				{
					base.AI.SelectCard(30603688);
					base.AI.SelectNextCard(new int[] { 46986414, 46986414 });
					return true;
				}
				if ((base.Bot.GetRemainingCount(46986414, 3) > 1 || base.Bot.HasInGraveyard(46986414)) && base.Bot.HasInSpellZone(7922915, false, false) && (base.Bot.HasInMonstersZone(46986414, false, false, false) || base.Bot.HasInMonstersZone(30603688, false, false, false)) && base.Duel.Player == 1 && !base.Bot.HasInHand(46986414))
				{
					base.AI.SelectCard(new int[] { 46986414, 30603688 });
					base.AI.SelectNextCard(new int[] { 46986414, 46986414 });
					return true;
				}
				return false;
			}
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x0005C440 File Offset: 0x0005A640
		private bool SpellbookMagicianOfProphecyeff()
		{
			Logger.DebugWriteLine("*********Secret_used= " + this.Secret_used.ToString());
			if (this.Secret_used)
			{
				base.AI.SelectCard(23314220);
			}
			else
			{
				base.AI.SelectCard(new int[] { 89739383, 23314220 });
			}
			return true;
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x0000763C File Offset: 0x0000583C
		private bool TheEyeOfTimaeuseff()
		{
			return true;
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x0000763C File Offset: 0x0000583C
		private bool UpstartGoblineff()
		{
			return true;
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x0005C4A4 File Offset: 0x0005A6A4
		private bool SpellbookOfSecreteff()
		{
			if (this.lockbird_used)
			{
				return false;
			}
			this.Secret_used = true;
			if (base.Bot.HasInHand(14824019))
			{
				base.AI.SelectCard(23314220);
			}
			else
			{
				base.AI.SelectCard(14824019);
			}
			return true;
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x0005C4F8 File Offset: 0x0005A6F8
		private bool SpellbookOfKnowledgeeff()
		{
			int count = 0;
			using (List<ClientCard>.Enumerator enumerator = base.Bot.GetMonsters().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.IsCode(50954680))
					{
						count++;
					}
				}
			}
			Logger.DebugWriteLine("%%%%%%%%%%%%%%%%SpellCaster= " + count.ToString());
			if (this.lockbird_used)
			{
				return false;
			}
			if (base.Bot.HasInSpellZone(73616671, false, false) && count < 2)
			{
				return false;
			}
			if (base.Bot.HasInMonstersZone(14824019, false, false, false) || base.Bot.HasInMonstersZone(7084129, false, false, false) || base.Bot.HasInMonstersZone(71007216, false, false, false) || base.Bot.HasInMonstersZone(43722862, false, false, false))
			{
				base.AI.SelectCard(new int[] { 14824019, 7084129, 71007216 });
				return true;
			}
			if (base.Bot.HasInMonstersZone(30603688, false, false, false) && base.Bot.GetSpellCount() < 2 && base.Duel.Phase == DuelPhase.Main2)
			{
				base.AI.SelectCard(30603688);
				return true;
			}
			if (base.Bot.HasInMonstersZone(46986414, false, false, false) && base.Bot.HasInSpellZone(48680970, false, false) && base.Duel.Phase == DuelPhase.Main2)
			{
				base.AI.SelectCard(46986414);
				return true;
			}
			return false;
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x0005C69C File Offset: 0x0005A89C
		private bool WonderWandeff()
		{
			if (this.lockbird_used)
			{
				return false;
			}
			int count = 0;
			using (List<ClientCard>.Enumerator enumerator = base.Bot.GetMonsters().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.IsCode(50954680))
					{
						count++;
					}
				}
			}
			Logger.DebugWriteLine("%%%%%%%%%%%%%%%%SpellCaster= " + count.ToString());
			if (base.Card.Location != CardLocation.Hand)
			{
				if (base.Duel.Turn != 1)
				{
					if (base.Duel.Phase == DuelPhase.Main1 && base.Enemy.GetSpellCountWithoutField() == 0 && base.Util.GetBestEnemyMonster(true, true) == null)
					{
						return false;
					}
					if (base.Duel.Phase == DuelPhase.Main1 && base.Enemy.GetSpellCountWithoutField() == 0 && base.Util.GetBestEnemyMonster(false, false).IsFacedown())
					{
						return true;
					}
					if (base.Duel.Phase == DuelPhase.Main1 && base.Enemy.GetSpellCountWithoutField() == 0 && base.Util.GetBestBotMonster(true) != null && base.Util.GetBestBotMonster(true).Attack > base.Util.GetBestEnemyMonster(true, false).Attack)
					{
						return false;
					}
				}
				return true;
			}
			if (base.Bot.HasInSpellZone(73616671, false, false) && count < 2)
			{
				return false;
			}
			if (base.Bot.HasInMonstersZone(14824019, false, false, false) || base.Bot.HasInMonstersZone(7084129, false, false, false) || base.Bot.HasInMonstersZone(71007216, false, false, false) || base.Bot.HasInMonstersZone(43722862, false, false, false))
			{
				base.AI.SelectCard(new int[] { 14824019, 7084129, 71007216, 43722862 });
				return base.UniqueFaceupSpell();
			}
			if (base.Bot.HasInMonstersZone(46986414, false, false, false) && base.Bot.HasInSpellZone(48680970, false, false) && base.Duel.Phase == DuelPhase.Main2)
			{
				base.AI.SelectCard(46986414);
				return base.UniqueFaceupSpell();
			}
			if (base.Bot.HasInMonstersZone(30603688, false, false, false) && base.Bot.GetSpellCount() < 2 && base.Duel.Phase == DuelPhase.Main2)
			{
				base.AI.SelectCard(30603688);
				return base.UniqueFaceupSpell();
			}
			if (base.Bot.HasInMonstersZone(30603688, false, false, false) && base.Bot.GetHandCount() <= 3 && base.Duel.Phase == DuelPhase.Main2)
			{
				base.AI.SelectCard(30603688);
				return base.UniqueFaceupSpell();
			}
			return false;
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x0005C974 File Offset: 0x0005AB74
		private bool ApprenticeLllusionMagiciansp()
		{
			if (base.Bot.HasInHand(46986414) && !base.Bot.HasInSpellZone(7922915, false, false))
			{
				if (base.Bot.GetRemainingCount(46986414, 3) > 0)
				{
					base.AI.SelectCard(46986414);
					base.AI.SelectPosition(CardPosition.FaceUpAttack);
					return true;
				}
				return false;
			}
			else
			{
				if (base.Bot.HasInHand(89739383) || base.Bot.HasInHand(2314238))
				{
					base.AI.SelectPosition(CardPosition.FaceUpAttack);
					base.AI.SelectCard(new int[] { 89739383, 2314238 });
					return true;
				}
				if (base.Bot.HasInMonstersZone(30603688, false, false, false))
				{
					return false;
				}
				int count = 0;
				using (IEnumerator<ClientCard> enumerator = base.Bot.Hand.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.IsCode(67775894))
						{
							count++;
						}
					}
				}
				if (count >= 2)
				{
					base.AI.SelectPosition(CardPosition.FaceUpAttack);
					base.AI.SelectCard(67775894);
					return true;
				}
				if (!base.Bot.HasInHandOrInSpellZone(48680970) && base.Bot.HasInHandOrInSpellZone(7922915) && !base.Bot.HasInHand(46986414) && base.Bot.GetHandCount() > 2 && base.Bot.GetMonsterCount() == 0)
				{
					base.AI.SelectPosition(CardPosition.FaceUpAttack);
					base.AI.SelectCard(new int[] { 35191415, 30603688, 1784686, 41735184, 67775894 });
					return true;
				}
				if (base.Bot.HasInHandOrInMonstersZoneOrInGraveyard(46986414))
				{
					return false;
				}
				if (base.Bot.HasInHandOrInSpellZone(73616671) && base.Bot.GetMonsterCount() >= 1)
				{
					return false;
				}
				base.AI.SelectPosition(CardPosition.FaceUpAttack);
				int Navigation_count = 0;
				using (IEnumerator<ClientCard> enumerator = base.Bot.Hand.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.IsCode(7922915))
						{
							Navigation_count++;
						}
					}
				}
				if (Navigation_count >= 2)
				{
					base.AI.SelectCard(7922915);
					return true;
				}
				base.AI.SelectCard(new int[] { 35191415, 30603688, 1784686, 41735184, 67775894 });
				return true;
			}
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x0005CBF4 File Offset: 0x0005ADF4
		private bool ApprenticeLllusionMagicianeff()
		{
			if (base.Util.ChainContainsCard(30603688))
			{
				return false;
			}
			if (base.Duel.Phase != DuelPhase.Battle && base.Duel.Phase != DuelPhase.BattleStart && base.Duel.Phase != DuelPhase.BattleStep && base.Duel.Phase != DuelPhase.Damage && base.Duel.Phase != DuelPhase.DamageCal)
			{
				return true;
			}
			if (base.ActivateDescription == -1)
			{
				Logger.DebugWriteLine("ApprenticeLllusionMagicianadd");
				return true;
			}
			return !base.Card.IsDisabled() && base.Bot.BattlingMonster != null && base.Enemy.BattlingMonster != null && base.Bot.BattlingMonster.Attack < base.Enemy.BattlingMonster.Attack;
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x0005CCCC File Offset: 0x0005AECC
		private bool SpellbookMagicianOfProphecysummon()
		{
			if (this.lockbird_used)
			{
				return false;
			}
			if (this.Spellbook_summon)
			{
				if (this.Secret_used)
				{
					base.AI.SelectCard(23314220);
				}
				else
				{
					base.AI.SelectCard(new int[] { 89739383, 23314220 });
				}
				return true;
			}
			return false;
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x0005CD29 File Offset: 0x0005AF29
		private bool MagiciansRodsummon()
		{
			if (this.lockbird_used)
			{
				return false;
			}
			bool rod_summon = this.Rod_summon;
			return true;
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x0005CD3D File Offset: 0x0005AF3D
		private bool DarkMagicAttackeff()
		{
			return base.DefaultHarpiesFeatherDusterFirst();
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x0005CD48 File Offset: 0x0005AF48
		private bool DarkMagicInheritanceeff()
		{
			if (this.lockbird_used)
			{
				return false;
			}
			IEnumerable<ClientCard> graveyard = base.Bot.Graveyard;
			IList<ClientCard> spell = new List<ClientCard>();
			int count = 0;
			foreach (ClientCard check in graveyard)
			{
				if (base.Card.HasType(CardType.Spell))
				{
					spell.Add(check);
					count++;
				}
			}
			if (count < 2)
			{
				return false;
			}
			base.AI.SelectCard(spell);
			if (base.Bot.HasInHandOrInSpellZone(48680970) && base.Bot.HasInHandOrInSpellZone(47222536) && base.Bot.GetRemainingCount(46986414, 3) >= 2 && !base.Bot.HasInHandOrInSpellZoneOrInGraveyard(73616671))
			{
				base.AI.SelectNextCard(73616671);
				return true;
			}
			if (base.Bot.HasInHand(30603688) && (!base.Bot.HasInHandOrInSpellZone(48680970) || !base.Bot.HasInHandOrInSpellZone(7922915)))
			{
				base.AI.SelectNextCard(7922915);
				return true;
			}
			if (base.Bot.HasInHandOrInSpellZone(48680970) && !base.Bot.HasInHandOrInMonstersZoneOrInGraveyard(46986414) && !base.Bot.HasInHandOrInSpellZoneOrInGraveyard(73616671))
			{
				base.AI.SelectNextCard(73616671);
				return true;
			}
			if (base.Bot.HasInHandOrInSpellZone(7922915) && !base.Bot.HasInHand(46986414) && !base.Bot.HasInHandOrInSpellZone(48680970) && base.Bot.GetRemainingCount(73616671, 1) > 0)
			{
				base.AI.SelectNextCard(73616671);
				return true;
			}
			if ((base.Bot.HasInHandOrInSpellZone(48680970) || base.Bot.HasInHandOrInSpellZone(7922915)) && !base.Bot.HasInHandOrInSpellZone(47222536))
			{
				base.AI.SelectNextCard(47222536);
				return true;
			}
			if (base.Bot.HasInHandOrInSpellZone(47222536))
			{
				if (base.Bot.HasInGraveyard(7922915))
				{
					base.AI.SelectNextCard(new int[] { 48680970, 7922915, 47222536 });
				}
				else
				{
					base.AI.SelectNextCard(new int[] { 48680970, 7922915, 47222536 });
				}
				return true;
			}
			if (base.Bot.HasInGraveyard(7922915))
			{
				base.AI.SelectNextCard(new int[] { 48680970, 47222536, 7922915 });
			}
			else
			{
				base.AI.SelectNextCard(new int[] { 7922915, 47222536, 48680970 });
			}
			return true;
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x0005D010 File Offset: 0x0005B210
		private bool MagiciansRodeff()
		{
			if (base.Card.Location == CardLocation.MonsterZone)
			{
				if (base.Bot.HasInHandOrInSpellZone(48680970) && base.Bot.HasInHandOrInSpellZone(47222536) && base.Bot.GetRemainingCount(46986414, 3) >= 2 && base.Bot.GetRemainingCount(73616671, 1) > 0)
				{
					base.AI.SelectCard(73616671);
					return true;
				}
				if (base.Bot.HasInHand(30603688) && !base.Bot.HasInHandOrInSpellZone(7922915) && base.Bot.GetRemainingCount(7922915, 3) > 0)
				{
					base.AI.SelectCard(7922915);
					return true;
				}
				if (base.Bot.HasInHandOrInSpellZone(48680970) && !base.Bot.HasInHandOrInMonstersZoneOrInGraveyard(46986414) && base.Bot.GetRemainingCount(73616671, 1) > 0)
				{
					base.AI.SelectCard(73616671);
					return true;
				}
				if (base.Bot.HasInHandOrInSpellZone(7922915) && !base.Bot.HasInHand(46986414) && !base.Bot.HasInHandOrInSpellZone(48680970) && base.Bot.GetRemainingCount(73616671, 1) > 0)
				{
					base.AI.SelectCard(73616671);
					return true;
				}
				if (!base.Bot.HasInHandOrInSpellZone(48680970) && base.Bot.HasInHandOrInSpellZone(47222536) && base.Bot.HasInHandOrInSpellZone(7922915) && base.Bot.GetRemainingCount(48680970, 3) > 0)
				{
					base.AI.SelectCard(48680970);
					return true;
				}
				if ((base.Bot.HasInHandOrInSpellZone(48680970) || base.Bot.HasInHandOrInSpellZone(7922915)) && !base.Bot.HasInHandOrInSpellZone(47222536) && base.Bot.GetRemainingCount(47222536, 3) > 0)
				{
					base.AI.SelectCard(47222536);
					return true;
				}
				if (!base.Bot.HasInHandOrInSpellZone(48680970) && !base.Bot.HasInHandOrInSpellZone(7922915))
				{
					if (base.Bot.HasInHand(46986414) && !base.Bot.HasInGraveyard(7922915) && base.Bot.GetRemainingCount(7922915, 3) > 0)
					{
						base.AI.SelectCard(7922915);
					}
					else if (!base.Bot.HasInHandOrInSpellZone(47222536))
					{
						base.AI.SelectCard(47222536);
					}
					else
					{
						base.AI.SelectCard(48680970);
					}
					return true;
				}
				if (!base.Bot.HasInHand(7922915))
				{
					base.AI.SelectCard(7922915);
					return true;
				}
				if (!base.Bot.HasInHand(47222536))
				{
					base.AI.SelectCard(47222536);
					return true;
				}
				if (!base.Bot.HasInHand(48680970))
				{
					base.AI.SelectCard(48680970);
					return true;
				}
				base.AI.SelectCard(new int[] { 73616671, 48680970, 47222536, 7922915 });
				return true;
			}
			else
			{
				if (base.DefaultCheckWhetherCardIsNegated(base.Card))
				{
					return false;
				}
				if (base.Bot.HasInMonstersZone(1482001, false, false, false))
				{
					base.AI.SelectCard(1482001);
					return true;
				}
				int Enemy_atk = 0;
				IList<ClientCard> list = new List<ClientCard>();
				foreach (ClientCard monster in base.Enemy.GetMonsters())
				{
					if (monster.IsAttack())
					{
						list.Add(monster);
					}
				}
				Enemy_atk = this.GetTotalATK(list);
				IList<ClientCard> list_ = new List<ClientCard>();
				foreach (ClientCard monster2 in base.Bot.GetMonsters())
				{
					if (base.Util.GetWorstBotMonster(true) != null && monster2.IsAttack() && monster2.Id != base.Util.GetWorstBotMonster(true).Id)
					{
						list_.Add(monster2);
					}
				}
				int bot_atk = this.GetTotalATK(list);
				if (base.Bot.HasInHand(7084129))
				{
					return false;
				}
				if (base.Bot.HasInMonstersZone(71384012, false, false, false) && base.Bot.GetMonsterCount() == 1 && base.Bot.HasInSpellZone(48680970, false, false))
				{
					return false;
				}
				if (base.Bot.LifePoints <= Enemy_atk - bot_atk && base.Bot.GetMonsterCount() > 1)
				{
					return false;
				}
				if (base.Bot.LifePoints - Enemy_atk <= 1000 && base.Bot.GetMonsterCount() == 1)
				{
					return false;
				}
				base.AI.SelectCard(new int[] { 1482001, 14824019, 71007216, 43722862, 7084129, 46986414, 35191415 });
				return true;
			}
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x0005D540 File Offset: 0x0005B740
		private bool WindwitchGlassBellsummonfirst()
		{
			return base.Bot.HasInMonstersZone(43722862, false, false, false) && base.Bot.HasInMonstersZone(70117860, false, false, false) && !base.Bot.HasInMonstersZone(71007216, false, false, false);
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x0005D590 File Offset: 0x0005B790
		private bool WindwitchGlassBellsummon()
		{
			return !this.lockbird_used && (this.plan_A || (!base.Bot.HasInGraveyard(71007216) && !base.Bot.HasInMonstersZone(71007216, false, false, false))) && ((this.GlassBell_summon && base.Bot.HasInMonstersZone(43722862, false, false, false) && !base.Bot.HasInMonstersZone(71007216, false, false, false)) || (!this.WindwitchGlassBelleff_used && this.GlassBell_summon));
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x0005D621 File Offset: 0x0005B821
		private bool BigEyesp()
		{
			if (this.plan_C)
			{
				return false;
			}
			if (base.Util.IsOneEnemyBetterThanValue(2500, false) && !base.Bot.HasInHandOrHasInMonstersZone(30603688))
			{
				base.AI.SelectPosition(CardPosition.FaceUpAttack);
				return true;
			}
			return false;
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x0005D664 File Offset: 0x0005B864
		private bool BigEyeeff()
		{
			ClientCard target = base.Util.GetBestEnemyMonster(false, true);
			if (target != null && target.Attack >= 2500)
			{
				base.AI.SelectCard(46986414);
				base.AI.SelectNextCard(target);
				return true;
			}
			return false;
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x0005D621 File Offset: 0x0005B821
		private bool Dracossacksp()
		{
			if (this.plan_C)
			{
				return false;
			}
			if (base.Util.IsOneEnemyBetterThanValue(2500, false) && !base.Bot.HasInHandOrHasInMonstersZone(30603688))
			{
				base.AI.SelectPosition(CardPosition.FaceUpAttack);
				return true;
			}
			return false;
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x0005D6B0 File Offset: 0x0005B8B0
		private bool Dracossackeff()
		{
			if (base.ActivateDescription == base.Util.GetStringId(22110647, 0))
			{
				base.AI.SelectCard(46986414);
				return true;
			}
			ClientCard target = base.Util.GetBestEnemyCard(false, true);
			if (target != null)
			{
				base.AI.SelectCard(22110648);
				base.AI.SelectNextCard(target);
				return true;
			}
			return false;
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x0005D718 File Offset: 0x0005B918
		private bool ApprenticeWitchlingsp()
		{
			int rod_count = 0;
			using (List<ClientCard>.Enumerator enumerator = base.Bot.GetMonsters().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsCode(7084129))
					{
						rod_count++;
					}
				}
			}
			if (rod_count >= 2)
			{
				base.AI.SelectCard(new int[] { 7084129, 7084129 });
				return true;
			}
			if (base.Bot.HasInMonstersZone(46986414, false, false, false) && base.Bot.HasInMonstersZone(7084129, false, false, false) && (base.Bot.HasInSpellZone(48680970, false, false) || base.Bot.GetMonsterCount() >= 4) && base.Duel.Phase == DuelPhase.Main2)
			{
				if (rod_count >= 2)
				{
					base.AI.SelectCard(new int[] { 7084129, 7084129 });
				}
				else
				{
					base.AI.SelectCard(new int[] { 7084129, 46986414 });
				}
				return true;
			}
			if (base.Bot.HasInMonstersZone(7084129, false, false, false) && base.Bot.HasInMonstersZone(30603688, false, false, false) && (base.Bot.HasInSpellZone(48680970, false, false) || base.Bot.HasInSpellZone(7922915, false, false)) && base.Duel.Phase == DuelPhase.Main2)
			{
				if (rod_count >= 2)
				{
					base.AI.SelectCard(new int[] { 7084129, 7084129 });
				}
				else
				{
					base.AI.SelectCard(new int[] { 7084129, 46986414 });
				}
				return true;
			}
			return false;
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x0005D904 File Offset: 0x0005BB04
		private bool ApprenticeWitchlingeff()
		{
			base.AI.SelectCard(new int[] { 7084129, 46986414, 30603688 });
			return true;
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x0000763C File Offset: 0x0000583C
		public override bool OnSelectHand()
		{
			return true;
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x0005D924 File Offset: 0x0005BB24
		private bool VentriloauistsClaraAndLucikasp()
		{
			if (base.Bot.HasInSpellZone(73616671, false, false))
			{
				return false;
			}
			if (base.Bot.HasInMonstersZone(7084129, false, false, false) && !base.Bot.HasInGraveyard(7084129) && (base.Bot.HasInSpellZone(48680970, false, false) || base.Bot.HasInSpellZone(7922915, false, false)))
			{
				base.AI.SelectCard(7084129);
				return true;
			}
			return false;
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x0005D9A9 File Offset: 0x0005BBA9
		public override void OnChaining(int player, ClientCard card)
		{
			base.OnChaining(player, card);
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x0005D9B4 File Offset: 0x0005BBB4
		public override void OnChainEnd()
		{
			if (base.Util.ChainContainsCard(23434538))
			{
				this.maxxc_used = true;
			}
			if ((base.Duel.CurrentChain.Count >= 1 && base.Util.GetLastChainCard().Id == 0) || (base.Duel.CurrentChain.Count == 2 && !base.Util.ChainContainPlayer(0) && base.Duel.CurrentChain[0].Id == 0))
			{
				Logger.DebugWriteLine("current chain = " + base.Duel.CurrentChain.Count.ToString());
				Logger.DebugWriteLine("******last chain card= " + base.Util.GetLastChainCard().Id.ToString());
				int maxxc_count = 0;
				using (IEnumerator<ClientCard> enumerator = base.Enemy.Graveyard.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.IsCode(23434538))
						{
							maxxc_count++;
						}
					}
				}
				if (maxxc_count != this.maxxc_done)
				{
					Logger.DebugWriteLine("************************last chain card= " + base.Util.GetLastChainCard().Id.ToString());
					this.maxxc_used = true;
				}
				int lockbird_count = 0;
				using (IEnumerator<ClientCard> enumerator = base.Enemy.Graveyard.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.IsCode(94145021))
						{
							lockbird_count++;
						}
					}
				}
				if (lockbird_count != this.lockbird_done)
				{
					Logger.DebugWriteLine("************************last chain card= " + base.Util.GetLastChainCard().Id.ToString());
					this.lockbird_used = true;
				}
				int ghost_count = 0;
				using (IEnumerator<ClientCard> enumerator = base.Enemy.Graveyard.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.IsCode(59438930))
						{
							ghost_count++;
						}
					}
				}
				if (ghost_count != this.ghost_done)
				{
					Logger.DebugWriteLine("************************last chain card= " + base.Util.GetLastChainCard().Id.ToString());
					this.ghost_used = true;
				}
				if (this.ghost_used && base.Util.ChainContainsCard(71007216))
				{
					base.AI.SelectCard(43722862);
					Logger.DebugWriteLine("***********WindwitchGlassBell*********************");
				}
			}
			foreach (ClientCard dangerous in base.Enemy.GetMonsters())
			{
				if (dangerous != null && dangerous.IsShouldNotBeTarget() && (dangerous.Attack > 2500 || dangerous.Defense > 2500) && !base.Bot.HasInHandOrHasInMonstersZone(30603688))
				{
					this.plan_C = true;
					Logger.DebugWriteLine("*********dangerous = " + dangerous.Id.ToString());
				}
			}
			int count = 0;
			using (IEnumerator<ClientCard> enumerator = base.Enemy.Graveyard.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsCode(23434538))
					{
						count++;
					}
				}
			}
			this.maxxc_done = count;
			count = 0;
			using (IEnumerator<ClientCard> enumerator = base.Enemy.Graveyard.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsCode(94145021))
					{
						count++;
					}
				}
			}
			this.lockbird_done = count;
			count = 0;
			using (IEnumerator<ClientCard> enumerator = base.Enemy.Graveyard.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsCode(59438930))
					{
						count++;
					}
				}
			}
			this.ghost_done = count;
			base.OnChainEnd();
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x0005DE10 File Offset: 0x0005C010
		public override bool OnPreBattleBetween(ClientCard attacker, ClientCard defender)
		{
			if (base.Bot.HasInSpellZone(58074177, false, false))
			{
				this.big_attack = true;
			}
			if (base.Duel.Player == 0 && base.Bot.GetMonsterCount() >= 2 && this.plan_C)
			{
				Logger.DebugWriteLine("*********dangerous********************* ");
				if (attacker.IsCode(new int[] { 16691074, 58074177 }))
				{
					attacker.RealPower = 9999;
				}
			}
			if (attacker.IsCode(new int[] { 46986414, 7084129, 80117527, 71384012 }) && base.Bot.HasInHandOrHasInMonstersZone(30603688))
			{
				attacker.RealPower += 2000;
			}
			if (attacker.IsCode(30603688) && this.ApprenticeLllusionMagician_count >= 2)
			{
				attacker.RealPower += 2000;
			}
			if (attacker.IsCode(new int[] { 46986414, 41721210 }) && base.Bot.HasInSpellZone(48680970, false, false))
			{
				return true;
			}
			if (attacker.IsCode(50954680))
			{
				if (defender.Level >= 5)
				{
					attacker.RealPower = 9999;
				}
				if (!this.CrystalWingSynchroDragon_used)
				{
					return true;
				}
			}
			if (!this.big_attack_used && this.big_attack)
			{
				attacker.RealPower = 9999;
				this.big_attack_used = true;
				return true;
			}
			if (attacker.IsCode(30603688))
			{
				Logger.DebugWriteLine("@@@@@@@@@@@@@@@@@@@ApprenticeLllusionMagician= " + attacker.RealPower.ToString());
			}
			return (base.Bot.HasInSpellZone(48680970, false, false) && attacker.IsCode(new int[] { 46986414, 41721210, 35191415 })) || base.OnPreBattleBetween(attacker, defender);
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x0005DFD8 File Offset: 0x0005C1D8
		public bool MonsterRepos()
		{
			return ((!base.Bot.HasInMonstersZone(58074177, false, false, false) && !base.Bot.HasInSpellZone(58074177, false, false) && !base.Bot.HasInMonstersZone(16691074, false, false, false)) || !base.Card.IsAttack()) && ((!base.Bot.HasInMonstersZone(30603688, false, false, false) && !base.Bot.HasInHand(30603688)) || !base.Card.IsAttack()) && (base.Card.IsFacedown() || base.DefaultMonsterRepos());
		}

		// Token: 0x0400166A RID: 5738
		private int attackerzone = -1;

		// Token: 0x0400166B RID: 5739
		private int defenderzone = -1;

		// Token: 0x0400166C RID: 5740
		private bool Secret_used;

		// Token: 0x0400166D RID: 5741
		private bool plan_A;

		// Token: 0x0400166E RID: 5742
		private bool plan_C;

		// Token: 0x0400166F RID: 5743
		private int maxxc_done;

		// Token: 0x04001670 RID: 5744
		private int lockbird_done;

		// Token: 0x04001671 RID: 5745
		private int ghost_done;

		// Token: 0x04001672 RID: 5746
		private bool maxxc_used;

		// Token: 0x04001673 RID: 5747
		private bool lockbird_used;

		// Token: 0x04001674 RID: 5748
		private bool ghost_used;

		// Token: 0x04001675 RID: 5749
		private bool WindwitchGlassBelleff_used;

		// Token: 0x04001676 RID: 5750
		private int ApprenticeLllusionMagician_count;

		// Token: 0x04001677 RID: 5751
		private bool Spellbook_summon;

		// Token: 0x04001678 RID: 5752
		private bool Rod_summon;

		// Token: 0x04001679 RID: 5753
		private bool GlassBell_summon;

		// Token: 0x0400167A RID: 5754
		private bool magician_sp;

		// Token: 0x0400167B RID: 5755
		private bool soul_used;

		// Token: 0x0400167C RID: 5756
		private bool big_attack;

		// Token: 0x0400167D RID: 5757
		private bool big_attack_used;

		// Token: 0x0400167E RID: 5758
		private bool CrystalWingSynchroDragon_used;

		// Token: 0x020002D4 RID: 724
		public class CardId
		{
			// Token: 0x0400167F RID: 5759
			public const int DarkMagician = 46986414;

			// Token: 0x04001680 RID: 5760
			public const int GrinderGolem = 75732622;

			// Token: 0x04001681 RID: 5761
			public const int MagicianOfLllusion = 35191415;

			// Token: 0x04001682 RID: 5762
			public const int ApprenticeLllusionMagician = 30603688;

			// Token: 0x04001683 RID: 5763
			public const int WindwitchGlassBell = 71007216;

			// Token: 0x04001684 RID: 5764
			public const int MagiciansRod = 7084129;

			// Token: 0x04001685 RID: 5765
			public const int WindwitchIceBell = 43722862;

			// Token: 0x04001686 RID: 5766
			public const int AshBlossom = 14558127;

			// Token: 0x04001687 RID: 5767
			public const int SpellbookMagicianOfProphecy = 14824019;

			// Token: 0x04001688 RID: 5768
			public const int MaxxC = 23434538;

			// Token: 0x04001689 RID: 5769
			public const int WindwitchSnowBell = 70117860;

			// Token: 0x0400168A RID: 5770
			public const int TheEyeOfTimaeus = 1784686;

			// Token: 0x0400168B RID: 5771
			public const int DarkMagicAttack = 2314238;

			// Token: 0x0400168C RID: 5772
			public const int SpellbookOfKnowledge = 23314220;

			// Token: 0x0400168D RID: 5773
			public const int UpstartGoblin = 70368879;

			// Token: 0x0400168E RID: 5774
			public const int SpellbookOfSecrets = 89739383;

			// Token: 0x0400168F RID: 5775
			public const int DarkMagicInheritance = 41735184;

			// Token: 0x04001690 RID: 5776
			public const int LllusionMagic = 73616671;

			// Token: 0x04001691 RID: 5777
			public const int DarkMagicalCircle = 47222536;

			// Token: 0x04001692 RID: 5778
			public const int WonderWand = 67775894;

			// Token: 0x04001693 RID: 5779
			public const int MagicianNavigation = 7922915;

			// Token: 0x04001694 RID: 5780
			public const int EternalSoul = 48680970;

			// Token: 0x04001695 RID: 5781
			public const int SolemnStrike = 40605147;

			// Token: 0x04001696 RID: 5782
			public const int DarkMagicianTheDragonKnight = 41721210;

			// Token: 0x04001697 RID: 5783
			public const int CrystalWingSynchroDragon = 50954680;

			// Token: 0x04001698 RID: 5784
			public const int OddEyesWingDragon = 58074177;

			// Token: 0x04001699 RID: 5785
			public const int ClearWingFastDragon = 90036274;

			// Token: 0x0400169A RID: 5786
			public const int WindwitchWinterBell = 14577226;

			// Token: 0x0400169B RID: 5787
			public const int OddEyesAbsoluteDragon = 16691074;

			// Token: 0x0400169C RID: 5788
			public const int Dracossack = 22110647;

			// Token: 0x0400169D RID: 5789
			public const int BigEye = 80117527;

			// Token: 0x0400169E RID: 5790
			public const int TroymarePhoenix = 2857636;

			// Token: 0x0400169F RID: 5791
			public const int TroymareCerberus = 75452921;

			// Token: 0x040016A0 RID: 5792
			public const int ApprenticeWitchling = 71384012;

			// Token: 0x040016A1 RID: 5793
			public const int VentriloauistsClaraAndLucika = 1482001;

			// Token: 0x040016A2 RID: 5794
			public const int HarpiesFeatherDuster = 18144506;

			// Token: 0x040016A3 RID: 5795
			public const int ElShaddollWinda = 94977269;

			// Token: 0x040016A4 RID: 5796
			public const int DarkHole = 53129443;

			// Token: 0x040016A5 RID: 5797
			public const int Ultimate = 86221741;

			// Token: 0x040016A6 RID: 5798
			public const int LockBird = 94145021;

			// Token: 0x040016A7 RID: 5799
			public const int Ghost = 59438930;

			// Token: 0x040016A8 RID: 5800
			public const int GiantRex = 80280944;

			// Token: 0x040016A9 RID: 5801
			public const int UltimateConductorTytanno = 18940556;

			// Token: 0x040016AA RID: 5802
			public const int SummonSorceress = 61665245;

			// Token: 0x040016AB RID: 5803
			public const int CrystronNeedlefiber = 50588353;

			// Token: 0x040016AC RID: 5804
			public const int FirewallDragon = 5043010;

			// Token: 0x040016AD RID: 5805
			public const int JackKnightOfTheLavenderDust = 28692962;

			// Token: 0x040016AE RID: 5806
			public const int JackKnightOfTheCobaltDepths = 92204263;

			// Token: 0x040016AF RID: 5807
			public const int JackKnightOfTheCrimsonLotus = 56809158;

			// Token: 0x040016B0 RID: 5808
			public const int JackKnightOfTheGoldenBlossom = 29415459;

			// Token: 0x040016B1 RID: 5809
			public const int JackKnightOfTheVerdantGale = 66022706;

			// Token: 0x040016B2 RID: 5810
			public const int JackKnightOfTheAmberShade = 93020401;

			// Token: 0x040016B3 RID: 5811
			public const int JackKnightOfTheAzureSky = 20537097;

			// Token: 0x040016B4 RID: 5812
			public const int MekkKnightMorningStar = 72006609;

			// Token: 0x040016B5 RID: 5813
			public const int JackKnightOfTheWorldScar = 38502358;

			// Token: 0x040016B6 RID: 5814
			public const int WhisperOfTheWorldLegacy = 62530723;

			// Token: 0x040016B7 RID: 5815
			public const int TrueDepthsOfTheWorldLegacy = 98935722;

			// Token: 0x040016B8 RID: 5816
			public const int KeyToTheWorldLegacy = 2930675;
		}
	}
}
