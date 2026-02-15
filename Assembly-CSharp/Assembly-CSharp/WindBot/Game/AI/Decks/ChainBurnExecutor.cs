using System;
using System.Collections.Generic;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x020002CF RID: 719
	[Deck("ChainBurn", "AI_ChainBurn", "Normal")]
	public class ChainBurnExecutor : DefaultExecutor
	{
		// Token: 0x06001178 RID: 4472 RVA: 0x00058144 File Offset: 0x00056344
		public ChainBurnExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 35261759);
			base.AddExecutor(ExecutorType.Activate, 98645731, new Func<bool>(this.PotOfDualityeff));
			base.AddExecutor(ExecutorType.Summon, 7733560, new Func<bool>(this.MichionTimelordsummon));
			base.AddExecutor(ExecutorType.Summon, 33015627, new Func<bool>(this.SandaionTheTimelord_summon));
			base.AddExecutor(ExecutorType.Summon, 41386308);
			base.AddExecutor(ExecutorType.Activate, 41386308, new Func<bool>(this.Mathematicianeff));
			base.AddExecutor(ExecutorType.MonsterSet, 3549275);
			base.AddExecutor(ExecutorType.Activate, 3549275);
			base.AddExecutor(ExecutorType.Summon, 45812361);
			base.AddExecutor(ExecutorType.Summon, 60990740, new Func<bool>(this.AbouluteKingBackJacksummon));
			base.AddExecutor(ExecutorType.MonsterSet, 60990740);
			base.AddExecutor(ExecutorType.Activate, 7733560);
			base.AddExecutor(ExecutorType.Activate, 33015627, new Func<bool>(this.SandaionTheTimelordeff));
			base.AddExecutor(ExecutorType.SpellSet, 12607053);
			base.AddExecutor(ExecutorType.SpellSet, 36361633);
			base.AddExecutor(ExecutorType.SpellSet, 75249652);
			base.AddExecutor(ExecutorType.SpellSet, 29843091, new Func<bool>(this.OjamaTrioset));
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(this.BrunSpellSet));
			base.AddExecutor(ExecutorType.Activate, 45812361);
			base.AddExecutor(ExecutorType.Activate, 59750328, new Func<bool>(this.CardOfDemiseeff));
			base.AddExecutor(ExecutorType.Activate, 67443336, new Func<bool>(this.BalanceOfJudgmenteff));
			base.AddExecutor(ExecutorType.Activate, 98444741);
			base.AddExecutor(ExecutorType.Activate, 75249652, new Func<bool>(this.BlazingMirrorForceeff));
			base.AddExecutor(ExecutorType.Activate, 62279055, new Func<bool>(this.MagicCylindereff));
			base.AddExecutor(ExecutorType.Activate, 36361633, new Func<bool>(this.ThreateningRoareff));
			base.AddExecutor(ExecutorType.Activate, 12607053, new Func<bool>(this.Wabokueff));
			base.AddExecutor(ExecutorType.Activate, 19665973, new Func<bool>(this.BattleFadereff));
			base.AddExecutor(ExecutorType.Activate, 83555666, new Func<bool>(this.Ring_act));
			base.AddExecutor(ExecutorType.Activate, 24068492, new Func<bool>(this.JustDessertseff));
			base.AddExecutor(ExecutorType.Activate, 36468556, new Func<bool>(this.Ceasefireeff));
			base.AddExecutor(ExecutorType.Activate, 18252559, new Func<bool>(this.SecretBlasteff));
			base.AddExecutor(ExecutorType.Activate, 27053506, new Func<bool>(this.SectetBarreleff));
			base.AddExecutor(ExecutorType.Activate, 37576645, new Func<bool>(this.RecklessGreedeff));
			base.AddExecutor(ExecutorType.Activate, 29843091, new Func<bool>(this.OjamaTrioeff));
			base.AddExecutor(ExecutorType.Activate, 60990740, new Func<bool>(this.AbouluteKingBackJackeff));
			base.AddExecutor(ExecutorType.Activate, 91623717, new Func<bool>(this.ChainStrikeeff));
			base.AddExecutor(ExecutorType.SpSummon, 41999284);
			base.AddExecutor(ExecutorType.Activate, 41999284, new Func<bool>(this.Linkuriboheff));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(this.MonsterRepos));
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x00058462 File Offset: 0x00056662
		public int[] all_List()
		{
			return new int[]
			{
				33015627, 7733560, 41386308, 3549275, 45812361, 19665973, 60990740, 35261759, 59750328, 98645731,
				91623717, 12607053, 18252559, 24068492, 29843091, 27053506, 36361633, 36468556, 37576645, 62279055,
				67443336, 75249652, 83555666, 98444741
			};
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x00058476 File Offset: 0x00056676
		public int[] AbouluteKingBackJack_List_1()
		{
			return new int[]
			{
				75249652, 12607053, 36361633, 62279055, 83555666, 37576645, 18252559, 24068492, 29843091, 27053506,
				36468556, 67443336, 98444741
			};
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x0005848A File Offset: 0x0005668A
		public int[] AbouluteKingBackJack_List_2()
		{
			return new int[]
			{
				7733560, 33015627, 35261759, 41386308, 3549275, 45812361, 19665973, 75249652, 12607053, 36361633,
				62279055, 83555666, 37576645, 18252559, 24068492, 29843091, 27053506, 36468556, 67443336, 98444741
			};
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x0005849E File Offset: 0x0005669E
		public int[] now_List()
		{
			return new int[] { 12607053, 18252559, 24068492, 27053506, 36361633, 36468556, 37576645, 83555666 };
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x000584B1 File Offset: 0x000566B1
		public int[] pot_list()
		{
			return new int[] { 35261759, 7733560, 33015627, 19665973, 12607053, 36361633, 62279055, 75249652, 83555666 };
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x000584C8 File Offset: 0x000566C8
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

		// Token: 0x0600117F RID: 4479 RVA: 0x00058518 File Offset: 0x00056718
		public bool Has_prevent_list_0(int id)
		{
			return id == 12607053 || id == 36361633 || id == 62279055 || id == 75249652 || id == 83555666;
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x00058544 File Offset: 0x00056744
		public bool Has_prevent_list_1(int id)
		{
			return id == 33015627 || id == 19665973 || id == 7733560;
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x0000763C File Offset: 0x0000583C
		public override bool OnSelectHand()
		{
			return true;
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x00058560 File Offset: 0x00056760
		public override void OnNewTurn()
		{
			if (base.Bot.HasInHand(33015627) || base.Bot.HasInHand(7733560))
			{
				Logger.DebugWriteLine("2222222222222222SandaionTheTimelord");
			}
			this.no_sp = false;
			this.prevent_used = false;
			this.Linkuribohused = true;
			this.Timelord_check = false;
			base.OnNewTurn();
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x000585C0 File Offset: 0x000567C0
		public override void OnNewPhase()
		{
			this.preventcount = 0;
			this.OjamaTrioused = false;
			IEnumerable<ClientCard> spells = base.Bot.GetSpells();
			IList<ClientCard> monster = base.Bot.GetMonsters();
			foreach (ClientCard card in spells)
			{
				if (this.Has_prevent_list_0(card.Id))
				{
					this.preventcount++;
				}
			}
			foreach (ClientCard card2 in monster)
			{
				if (this.Has_prevent_list_1(card2.Id))
				{
					this.preventcount++;
				}
			}
			foreach (ClientCard clientCard in monster)
			{
				if (base.Bot.HasInMonstersZone(33015627, false, false, false) || base.Bot.HasInMonstersZone(7733560, false, false, false))
				{
					this.prevent_used = true;
					this.Timelord_check = true;
				}
			}
			if (this.prevent_used && this.Timelord_check && (!base.Bot.HasInMonstersZone(33015627, false, false, false) || !base.Bot.HasInMonstersZone(7733560, false, false, false)))
			{
				this.prevent_used = false;
			}
			this.expected_blood = 0;
			this.one_turn_kill = false;
			this.one_turn_kill_1 = false;
			this.OjamaTrioused_draw = false;
			this.OjamaTrioused_do = false;
			this.drawfirst = false;
			this.HasAccuulatedFortune = 0;
			this.strike_count = 0;
			this.greed_count = 0;
			this.blast_count = 0;
			this.barrel_count = 0;
			this.just_count = 0;
			this.Waboku_count = 0;
			this.Roar_count = 0;
			this.Ojama_count = 0;
			IList<ClientCard> check = base.Bot.GetSpells();
			using (IEnumerator<ClientCard> enumerator = check.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsCode(98444741))
					{
						this.HasAccuulatedFortune++;
					}
				}
			}
			using (IEnumerator<ClientCard> enumerator = check.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsCode(18252559))
					{
						this.blast_count++;
					}
				}
			}
			using (IEnumerator<ClientCard> enumerator = check.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsCode(27053506))
					{
						this.barrel_count++;
					}
				}
			}
			using (IEnumerator<ClientCard> enumerator = check.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsCode(24068492))
					{
						this.just_count++;
					}
				}
			}
			using (IEnumerator<ClientCard> enumerator = check.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsCode(91623717))
					{
						this.strike_count++;
					}
				}
			}
			using (List<ClientCard>.Enumerator enumerator2 = base.Bot.GetSpells().GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current.IsCode(37576645))
					{
						this.greed_count++;
					}
				}
			}
			using (IEnumerator<ClientCard> enumerator = check.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsCode(12607053))
					{
						this.Waboku_count++;
					}
				}
			}
			using (IEnumerator<ClientCard> enumerator = check.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsCode(36361633))
					{
						this.Roar_count++;
					}
				}
			}
			this.expected_blood = base.Enemy.GetMonsterCount() * 500 * this.just_count + base.Enemy.GetFieldHandCount() * 200 * this.barrel_count + base.Enemy.GetFieldCount() * 300 * this.blast_count;
			if (base.Enemy.LifePoints <= this.expected_blood && base.Duel.Player == 1)
			{
				Logger.DebugWriteLine(" one_turn_kill");
				this.one_turn_kill = true;
			}
			this.expected_blood = 0;
			if (this.greed_count >= 2)
			{
				this.greed_count = 1;
			}
			if (this.blast_count >= 2)
			{
				this.blast_count = 1;
			}
			if (this.just_count >= 2)
			{
				this.just_count = 1;
			}
			if (this.barrel_count >= 2)
			{
				this.barrel_count = 1;
			}
			if (this.Waboku_count >= 2)
			{
				this.Waboku_count = 1;
			}
			if (this.Roar_count >= 2)
			{
				this.Roar_count = 1;
			}
			int currentchain;
			if (this.OjamaTrioused_do)
			{
				currentchain = base.Duel.CurrentChain.Count + this.blast_count + this.just_count + this.barrel_count + this.Waboku_count + this.Waboku_count + this.Roar_count + this.greed_count + this.Ojama_count;
			}
			else
			{
				currentchain = base.Duel.CurrentChain.Count + this.blast_count + this.just_count + this.barrel_count + this.Waboku_count + this.Waboku_count + this.greed_count + this.Roar_count;
			}
			if (base.Bot.HasInSpellZone(91623717, false, false))
			{
				if (this.strike_count == 1)
				{
					if (this.OjamaTrioused_do)
					{
						this.expected_blood = (base.Enemy.GetMonsterCount() + 3) * 500 * this.just_count + base.Enemy.GetFieldHandCount() * 200 * this.barrel_count + base.Enemy.GetFieldCount() * 300 * this.blast_count + (currentchain + 1) * 400;
					}
					else
					{
						this.expected_blood = base.Enemy.GetMonsterCount() * 500 * this.just_count + base.Enemy.GetFieldHandCount() * 200 * this.barrel_count + base.Enemy.GetFieldCount() * 300 * this.blast_count + (currentchain + 1) * 400;
					}
				}
				else if (this.OjamaTrioused_do)
				{
					this.expected_blood = (base.Enemy.GetMonsterCount() + 3) * 500 * this.just_count + base.Enemy.GetFieldHandCount() * 200 * this.barrel_count + base.Enemy.GetFieldCount() * 300 * this.blast_count + (currentchain + 1 + currentchain + 2) * 400;
				}
				else
				{
					this.expected_blood = base.Enemy.GetMonsterCount() * 500 * this.just_count + base.Enemy.GetFieldHandCount() * 200 * this.barrel_count + base.Enemy.GetFieldCount() * 300 * this.blast_count + (currentchain + 1 + currentchain + 2) * 400;
				}
				if (!this.one_turn_kill && base.Enemy.LifePoints <= this.expected_blood && base.Duel.Player == 1)
				{
					Logger.DebugWriteLine(" %%%%%%%%%%%%%%%%%one_turn_kill_1");
					this.one_turn_kill_1 = true;
					this.OjamaTrioused = true;
				}
			}
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x00058D7C File Offset: 0x00056F7C
		private bool must_chain()
		{
			if (base.Util.IsChainTarget(base.Card))
			{
				return true;
			}
			foreach (ClientCard card in base.Enemy.GetSpells())
			{
				if (card.IsCode(18144506) && card.IsFaceup())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x00058E00 File Offset: 0x00057000
		private bool OjamaTrioset()
		{
			return !base.Bot.HasInSpellZone(29843091, false, false);
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x00058E1C File Offset: 0x0005701C
		private bool BrunSpellSet()
		{
			return (!base.Card.IsCode(29843091) || !base.Bot.HasInSpellZone(29843091, false, false)) && (base.Card.IsTrap() || base.Card.HasType(CardType.QuickPlay)) && base.Bot.GetSpellCountWithoutField() < 5;
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x00058E80 File Offset: 0x00057080
		private bool MichionTimelordsummon()
		{
			return base.Duel.Turn != 1;
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x00058E93 File Offset: 0x00057093
		private bool SandaionTheTimelord_summon()
		{
			Logger.DebugWriteLine("&&&&&&&&&SandaionTheTimelord_summon");
			return true;
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x00058EA0 File Offset: 0x000570A0
		private bool AbouluteKingBackJacksummon()
		{
			return !this.no_sp;
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x00058EAB File Offset: 0x000570AB
		private bool AbouluteKingBackJackeff()
		{
			if (base.ActivateDescription == -1)
			{
				base.AI.SelectCard(this.AbouluteKingBackJack_List_1());
				base.AI.SelectNextCard(this.AbouluteKingBackJack_List_2());
			}
			return true;
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x00058ED9 File Offset: 0x000570D9
		private bool PotOfDualityeff()
		{
			this.no_sp = true;
			base.AI.SelectCard(this.pot_list());
			return true;
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x00058EF4 File Offset: 0x000570F4
		private bool ThreateningRoareff()
		{
			if (this.one_turn_kill_1)
			{
				return base.UniqueFaceupSpell();
			}
			if (this.drawfirst)
			{
				return true;
			}
			if (base.DefaultOnBecomeTarget())
			{
				this.prevent_used = true;
				return true;
			}
			if (this.prevent_used || base.Duel.Phase != DuelPhase.BattleStart)
			{
				return false;
			}
			this.prevent_used = true;
			return base.DefaultUniqueTrap();
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x00058F50 File Offset: 0x00057150
		private bool SandaionTheTimelordeff()
		{
			Logger.DebugWriteLine("***********SandaionTheTimelordeff");
			return true;
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x00058F60 File Offset: 0x00057160
		private bool Wabokueff()
		{
			if (this.one_turn_kill_1)
			{
				return base.UniqueFaceupSpell();
			}
			if (this.drawfirst)
			{
				this.Linkuribohused = false;
				return true;
			}
			if (base.DefaultOnBecomeTarget())
			{
				this.Linkuribohused = false;
				this.prevent_used = true;
				return true;
			}
			if (this.prevent_used || base.Duel.Player == 0 || base.Duel.Phase != DuelPhase.BattleStart)
			{
				return false;
			}
			this.prevent_used = true;
			this.Linkuribohused = false;
			return base.DefaultUniqueTrap();
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x00058FE0 File Offset: 0x000571E0
		private bool BattleFadereff()
		{
			if (base.Util.ChainContainsCard(75249652) || base.Util.ChainContainsCard(62279055))
			{
				return false;
			}
			if (this.prevent_used || base.Duel.Player == 0)
			{
				return false;
			}
			base.AI.SelectPosition(CardPosition.FaceUpDefence);
			this.prevent_used = true;
			return true;
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x00059040 File Offset: 0x00057240
		private bool BlazingMirrorForceeff()
		{
			if (this.prevent_used)
			{
				return false;
			}
			IList<ClientCard> list = new List<ClientCard>();
			foreach (ClientCard monster in base.Enemy.GetMonsters())
			{
				if (monster.IsAttack())
				{
					list.Add(monster);
				}
			}
			if (this.GetTotalATK(list) / 2 >= base.Bot.LifePoints)
			{
				return false;
			}
			Logger.DebugWriteLine("!!!!!!!!BlazingMirrorForceeff" + (this.GetTotalATK(list) / 2).ToString());
			if (this.GetTotalATK(list) / 2 >= base.Enemy.LifePoints)
			{
				return base.DefaultUniqueTrap();
			}
			if (this.GetTotalATK(list) < 3000)
			{
				return false;
			}
			this.prevent_used = true;
			return base.DefaultUniqueTrap();
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x00059124 File Offset: 0x00057324
		private bool MagicCylindereff()
		{
			if (this.prevent_used)
			{
				return false;
			}
			if (base.Bot.LifePoints <= base.Enemy.BattlingMonster.Attack)
			{
				return base.DefaultUniqueTrap();
			}
			if (base.Enemy.LifePoints <= base.Enemy.BattlingMonster.Attack)
			{
				return base.DefaultUniqueTrap();
			}
			return base.Enemy.BattlingMonster.Attack > 1800 && base.DefaultUniqueTrap();
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x000591A4 File Offset: 0x000573A4
		public bool Ring_act()
		{
			if (base.Duel.LastChainPlayer == 0 && base.Util.GetLastChainCard() != null)
			{
				return false;
			}
			ClientCard target = base.Util.GetProblematicEnemyMonster(0, false);
			if (target == null && base.Util.IsChainTarget(base.Card))
			{
				target = base.Util.GetBestEnemyMonster(true, true);
			}
			if (target == null)
			{
				return false;
			}
			if (base.Bot.LifePoints <= target.Attack)
			{
				return false;
			}
			base.AI.SelectCard(target);
			return true;
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x00059228 File Offset: 0x00057428
		private bool RecklessGreedeff()
		{
			if (this.one_turn_kill_1)
			{
				return base.UniqueFaceupSpell();
			}
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
			bool Demiseused = base.Util.ChainContainsCard(59750328);
			if (this.drawfirst)
			{
				return base.UniqueFaceupSpell();
			}
			return (base.DefaultOnBecomeTarget() && count > 1) || (!Demiseused && (count > 1 || base.Bot.LifePoints <= 3000 || (base.Bot.GetHandCount() < 1 && base.Duel.Player == 0 && base.Duel.Phase != DuelPhase.Standby)));
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x00059314 File Offset: 0x00057514
		private bool SectetBarreleff()
		{
			if (base.DefaultOnBecomeTarget())
			{
				return true;
			}
			if (base.Duel.Player == 0)
			{
				return false;
			}
			if (this.drawfirst)
			{
				return true;
			}
			if (this.one_turn_kill_1)
			{
				return base.UniqueFaceupSpell();
			}
			if (this.one_turn_kill)
			{
				return true;
			}
			if (base.DefaultOnBecomeTarget())
			{
				return true;
			}
			int count = base.Enemy.GetFieldHandCount();
			int monster_count = base.Enemy.GetMonsterCount() - base.Enemy.GetMonstersExtraZoneCount();
			if (base.Enemy.LifePoints < count * 200)
			{
				return true;
			}
			if (base.Bot.HasInSpellZone(29843091, false, false) && monster_count <= 2 && monster_count >= 1 && count + 3 >= 8)
			{
				this.OjamaTrioused = true;
				return true;
			}
			return count >= 8;
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x000593D4 File Offset: 0x000575D4
		private bool SecretBlasteff()
		{
			if (base.DefaultOnBecomeTarget())
			{
				return true;
			}
			if (base.Duel.Player == 0)
			{
				return false;
			}
			if (this.drawfirst)
			{
				return base.UniqueFaceupSpell();
			}
			if (this.one_turn_kill_1)
			{
				return base.UniqueFaceupSpell();
			}
			if (this.one_turn_kill)
			{
				return true;
			}
			int count = base.Enemy.GetFieldCount();
			int monster_count = base.Enemy.GetMonsterCount() - base.Enemy.GetMonstersExtraZoneCount();
			if (base.Enemy.LifePoints < count * 300)
			{
				return true;
			}
			if (base.Bot.HasInSpellZone(29843091, false, false) && monster_count <= 2 && monster_count >= 1 && count + 3 >= 5)
			{
				this.OjamaTrioused = true;
				return true;
			}
			return count >= 5;
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x0005948E File Offset: 0x0005768E
		private bool OjamaTrioeff()
		{
			return this.OjamaTrioused || this.OjamaTrioused_draw;
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x000594A0 File Offset: 0x000576A0
		private bool JustDessertseff()
		{
			if (base.DefaultOnBecomeTarget())
			{
				return true;
			}
			if (base.Duel.Player == 0)
			{
				return false;
			}
			if (this.drawfirst)
			{
				return base.UniqueFaceupSpell();
			}
			if (this.one_turn_kill_1)
			{
				return base.UniqueFaceupSpell();
			}
			if (this.one_turn_kill)
			{
				return true;
			}
			int count = base.Enemy.GetMonsterCount() - base.Enemy.GetMonstersExtraZoneCount();
			if (base.Enemy.LifePoints <= count * 500)
			{
				return true;
			}
			if (base.Bot.HasInSpellZone(29843091, false, false) && count <= 2 && count >= 1)
			{
				this.OjamaTrioused = true;
				return true;
			}
			return count >= 3;
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x00059548 File Offset: 0x00057748
		private bool ChainStrikeeff()
		{
			if (this.one_turn_kill)
			{
				return true;
			}
			if (this.one_turn_kill_1)
			{
				return true;
			}
			if (this.drawfirst)
			{
				return true;
			}
			if (base.DefaultOnBecomeTarget())
			{
				return true;
			}
			int chain = base.Duel.CurrentChain.Count;
			return (this.strike_count >= 2 && chain >= 2) || base.Enemy.LifePoints <= (chain + 1) * 400 || base.Duel.CurrentChain.Count >= 3;
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x000595CB File Offset: 0x000577CB
		private bool BalanceOfJudgmenteff()
		{
			return base.DefaultOnBecomeTarget() || base.Enemy.GetFieldCount() - base.Bot.GetFieldHandCount() >= 2;
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x000595F4 File Offset: 0x000577F4
		private bool CardOfDemiseeff()
		{
			foreach (ClientCard card in base.Bot.GetMonsters())
			{
				if (card.IsCode(45812361) && card.IsFaceup())
				{
					return false;
				}
			}
			if (base.Bot.GetHandCount() == 1 && base.Bot.GetSpellCountWithoutField() <= 3)
			{
				this.no_sp = true;
				return true;
			}
			return false;
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x00059688 File Offset: 0x00057888
		private bool Mathematicianeff()
		{
			if (base.Card.Location == CardLocation.MonsterZone)
			{
				base.AI.SelectCard(60990740);
				return true;
			}
			return true;
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x000596AC File Offset: 0x000578AC
		private bool DiceJarfacedown()
		{
			using (List<ClientCard>.Enumerator enumerator = base.Bot.GetMonsters().GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					ClientCard card = enumerator.Current;
					if (card.IsCode(3549275) && card.IsFacedown())
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x0005971C File Offset: 0x0005791C
		private bool Ceasefireeff()
		{
			return base.Enemy.GetMonsterCount() >= 3 || (!this.DiceJarfacedown() && base.Bot.GetMonsterCount() + base.Enemy.GetMonsterCount() >= 4);
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x00059758 File Offset: 0x00057958
		private bool Linkuriboheff()
		{
			IList<ClientCard> newlist = new List<ClientCard>();
			foreach (ClientCard newmonster in base.Enemy.GetMonsters())
			{
				if (newmonster.IsAttack())
				{
					newlist.Add(newmonster);
				}
			}
			return this.Linkuribohused && (base.Enemy.BattlingMonster == null || base.Enemy.BattlingMonster.Attack <= 1800 || !base.Bot.HasInSpellZone(62279055, false, false)) && ((this.GetTotalATK(newlist) / 2 >= base.Bot.LifePoints && base.Bot.HasInSpellZone(75249652, false, false)) || ((this.GetTotalATK(newlist) / 2 < base.Enemy.LifePoints || !base.Bot.HasInSpellZone(75249652, false, false)) && (base.Util.GetLastChainCard() == null || !base.Util.GetLastChainCard().IsCode(41999284))));
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x00059884 File Offset: 0x00057A84
		public bool MonsterRepos()
		{
			return (base.Card.IsFacedown() && !base.Card.IsCode(3549275)) || base.DefaultMonsterRepos();
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x000598B0 File Offset: 0x00057AB0
		public override bool OnPreBattleBetween(ClientCard attacker, ClientCard defender)
		{
			if (attacker.IsCode(41999284) && defender.IsFacedown())
			{
				return false;
			}
			if (attacker.IsCode(33015627) && !attacker.IsDisabled())
			{
				attacker.RealPower = 9999;
				return true;
			}
			if (attacker.IsCode(7733560) && !attacker.IsDisabled())
			{
				attacker.RealPower = 9999;
				return true;
			}
			return base.OnPreBattleBetween(attacker, defender);
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x00059920 File Offset: 0x00057B20
		public override void OnChaining(int player, ClientCard card)
		{
			this.expected_blood = 0;
			this.one_turn_kill = false;
			this.one_turn_kill_1 = false;
			this.OjamaTrioused_draw = false;
			this.OjamaTrioused_do = false;
			this.drawfirst = false;
			this.HasAccuulatedFortune = 0;
			this.strike_count = 0;
			this.greed_count = 0;
			this.blast_count = 0;
			this.barrel_count = 0;
			this.just_count = 0;
			this.Waboku_count = 0;
			this.Roar_count = 0;
			this.Ojama_count = 0;
			IList<ClientCard> check = base.Bot.GetSpells();
			using (IEnumerator<ClientCard> enumerator = check.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsCode(98444741))
					{
						this.HasAccuulatedFortune++;
					}
				}
			}
			using (IEnumerator<ClientCard> enumerator = check.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsCode(18252559))
					{
						this.blast_count++;
					}
				}
			}
			using (IEnumerator<ClientCard> enumerator = check.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsCode(27053506))
					{
						this.barrel_count++;
					}
				}
			}
			using (IEnumerator<ClientCard> enumerator = check.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsCode(24068492))
					{
						this.just_count++;
					}
				}
			}
			using (IEnumerator<ClientCard> enumerator = check.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsCode(91623717))
					{
						this.strike_count++;
					}
				}
			}
			using (List<ClientCard>.Enumerator enumerator2 = base.Bot.GetSpells().GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current.IsCode(37576645))
					{
						this.greed_count++;
					}
				}
			}
			using (IEnumerator<ClientCard> enumerator = check.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsCode(12607053))
					{
						this.Waboku_count++;
					}
				}
			}
			using (IEnumerator<ClientCard> enumerator = check.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsCode(36361633))
					{
						this.Roar_count++;
					}
				}
			}
			if (base.Bot.HasInSpellZone(29843091, false, false) && base.Enemy.GetMonsterCount() - base.Enemy.GetMonstersExtraZoneCount() <= 2 && base.Enemy.GetMonsterCount() - base.Enemy.GetMonstersExtraZoneCount() >= 1)
			{
				this.OjamaTrioused_do = true;
			}
			this.expected_blood = base.Enemy.GetMonsterCount() * 500 * this.just_count + base.Enemy.GetFieldHandCount() * 200 * this.barrel_count + base.Enemy.GetFieldCount() * 300 * this.blast_count;
			if (base.Enemy.LifePoints <= this.expected_blood && base.Duel.Player == 1)
			{
				Logger.DebugWriteLine(" %%%%%%%%%%%%%%%%%one_turn_kill");
				this.one_turn_kill = true;
			}
			this.expected_blood = 0;
			if (this.greed_count >= 2)
			{
				this.greed_count = 1;
			}
			if (this.blast_count >= 2)
			{
				this.blast_count = 1;
			}
			if (this.just_count >= 2)
			{
				this.just_count = 1;
			}
			if (this.barrel_count >= 2)
			{
				this.barrel_count = 1;
			}
			if (this.Waboku_count >= 2)
			{
				this.Waboku_count = 1;
			}
			if (this.Roar_count >= 2)
			{
				this.Roar_count = 1;
			}
			int currentchain;
			if (this.OjamaTrioused_do)
			{
				currentchain = base.Duel.CurrentChain.Count + this.blast_count + this.just_count + this.barrel_count + this.Waboku_count + this.Waboku_count + this.Roar_count + this.greed_count + this.Ojama_count;
			}
			else
			{
				currentchain = base.Duel.CurrentChain.Count + this.blast_count + this.just_count + this.barrel_count + this.Waboku_count + this.Waboku_count + this.greed_count + this.Roar_count;
			}
			if (base.Bot.HasInSpellZone(91623717, false, false))
			{
				if (this.strike_count == 1)
				{
					if (this.OjamaTrioused_do)
					{
						this.expected_blood = (base.Enemy.GetMonsterCount() + 3) * 500 * this.just_count + base.Enemy.GetFieldHandCount() * 200 * this.barrel_count + base.Enemy.GetFieldCount() * 300 * this.blast_count + (currentchain + 1) * 400;
					}
					else
					{
						this.expected_blood = base.Enemy.GetMonsterCount() * 500 * this.just_count + base.Enemy.GetFieldHandCount() * 200 * this.barrel_count + base.Enemy.GetFieldCount() * 300 * this.blast_count + (currentchain + 1) * 400;
					}
				}
				else if (this.OjamaTrioused_do)
				{
					this.expected_blood = (base.Enemy.GetMonsterCount() + 3) * 500 * this.just_count + base.Enemy.GetFieldHandCount() * 200 * this.barrel_count + base.Enemy.GetFieldCount() * 300 * this.blast_count + (currentchain + 1 + currentchain + 2) * 400;
				}
				else
				{
					this.expected_blood = base.Enemy.GetMonsterCount() * 500 * this.just_count + base.Enemy.GetFieldHandCount() * 200 * this.barrel_count + base.Enemy.GetFieldCount() * 300 * this.blast_count + (currentchain + 1 + currentchain + 2) * 400;
				}
				if (!this.one_turn_kill && base.Enemy.LifePoints <= this.expected_blood && base.Duel.Player == 1)
				{
					Logger.DebugWriteLine(" %%%%%%%%%%%%%%%%%one_turn_kill_1");
					this.one_turn_kill_1 = true;
					this.OjamaTrioused = true;
				}
			}
			base.OnChaining(player, card);
		}

		// Token: 0x0400161F RID: 5663
		private bool no_sp;

		// Token: 0x04001620 RID: 5664
		private bool one_turn_kill;

		// Token: 0x04001621 RID: 5665
		private bool one_turn_kill_1;

		// Token: 0x04001622 RID: 5666
		private int expected_blood;

		// Token: 0x04001623 RID: 5667
		private bool prevent_used;

		// Token: 0x04001624 RID: 5668
		private int preventcount;

		// Token: 0x04001625 RID: 5669
		private bool OjamaTrioused;

		// Token: 0x04001626 RID: 5670
		private bool OjamaTrioused_draw;

		// Token: 0x04001627 RID: 5671
		private bool OjamaTrioused_do;

		// Token: 0x04001628 RID: 5672
		private bool drawfirst;

		// Token: 0x04001629 RID: 5673
		private bool Linkuribohused = true;

		// Token: 0x0400162A RID: 5674
		private bool Timelord_check;

		// Token: 0x0400162B RID: 5675
		private int Waboku_count;

		// Token: 0x0400162C RID: 5676
		private int Roar_count;

		// Token: 0x0400162D RID: 5677
		private int strike_count;

		// Token: 0x0400162E RID: 5678
		private int greed_count;

		// Token: 0x0400162F RID: 5679
		private int blast_count;

		// Token: 0x04001630 RID: 5680
		private int barrel_count;

		// Token: 0x04001631 RID: 5681
		private int just_count;

		// Token: 0x04001632 RID: 5682
		private int Ojama_count;

		// Token: 0x04001633 RID: 5683
		private int HasAccuulatedFortune;

		// Token: 0x020002D0 RID: 720
		public class CardId
		{
			// Token: 0x04001634 RID: 5684
			public const int SandaionTheTimelord = 33015627;

			// Token: 0x04001635 RID: 5685
			public const int MichionTimelord = 7733560;

			// Token: 0x04001636 RID: 5686
			public const int Mathematician = 41386308;

			// Token: 0x04001637 RID: 5687
			public const int DiceJar = 3549275;

			// Token: 0x04001638 RID: 5688
			public const int CardcarD = 45812361;

			// Token: 0x04001639 RID: 5689
			public const int BattleFader = 19665973;

			// Token: 0x0400163A RID: 5690
			public const int AbouluteKingBackJack = 60990740;

			// Token: 0x0400163B RID: 5691
			public const int PotOfDesires = 35261759;

			// Token: 0x0400163C RID: 5692
			public const int CardOfDemise = 59750328;

			// Token: 0x0400163D RID: 5693
			public const int PotOfDuality = 98645731;

			// Token: 0x0400163E RID: 5694
			public const int ChainStrike = 91623717;

			// Token: 0x0400163F RID: 5695
			public const int Waboku = 12607053;

			// Token: 0x04001640 RID: 5696
			public const int SecretBlast = 18252559;

			// Token: 0x04001641 RID: 5697
			public const int JustDesserts = 24068492;

			// Token: 0x04001642 RID: 5698
			public const int SectetBarrel = 27053506;

			// Token: 0x04001643 RID: 5699
			public const int OjamaTrio = 29843091;

			// Token: 0x04001644 RID: 5700
			public const int ThreateningRoar = 36361633;

			// Token: 0x04001645 RID: 5701
			public const int Ceasefire = 36468556;

			// Token: 0x04001646 RID: 5702
			public const int RecklessGreed = 37576645;

			// Token: 0x04001647 RID: 5703
			public const int MagicCylinder = 62279055;

			// Token: 0x04001648 RID: 5704
			public const int BalanceOfJudgment = 67443336;

			// Token: 0x04001649 RID: 5705
			public const int BlazingMirrorForce = 75249652;

			// Token: 0x0400164A RID: 5706
			public const int RingOfDestruction = 83555666;

			// Token: 0x0400164B RID: 5707
			public const int AccuulatedFortune = 98444741;

			// Token: 0x0400164C RID: 5708
			public const int Linkuriboh = 41999284;

			// Token: 0x0400164D RID: 5709
			public const int HarpiesFeatherDuster = 18144506;
		}
	}
}
