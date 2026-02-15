using System;
using System.Collections.Generic;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x02000312 RID: 786
	[Deck("GrenMajuThunderBoarder", "AI_GrenMajuThunderBoarder", "Normal")]
	public class GrenMajuThunderBoarderExecutor : DefaultExecutor
	{
		// Token: 0x06001443 RID: 5187 RVA: 0x0007082C File Offset: 0x0006EA2C
		public GrenMajuThunderBoarderExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.GoToBattlePhase, new Func<bool>(this.GoToBattlePhase));
			base.AddExecutor(ExecutorType.Activate, 15693423, new Func<bool>(this.EvenlyMatchedeff));
			base.AddExecutor(ExecutorType.Activate, 30241314, new Func<bool>(this.MacroCosmoseff));
			base.AddExecutor(ExecutorType.Activate, 58921041, new Func<bool>(this.AntiSpellFragranceeff));
			base.AddExecutor(ExecutorType.Activate, 14558127, new Func<bool>(base.DefaultAshBlossomAndJoyousSpring));
			base.AddExecutor(ExecutorType.Activate, 23434538, new Func<bool>(base.DefaultMaxxC));
			base.AddExecutor(ExecutorType.Activate, 10045474, new Func<bool>(base.DefaultInfiniteImpermanence));
			base.AddExecutor(ExecutorType.Activate, 84749824, new Func<bool>(base.DefaultSolemnWarning));
			base.AddExecutor(ExecutorType.Activate, 40605147, new Func<bool>(base.DefaultSolemnStrike));
			base.AddExecutor(ExecutorType.Activate, 61740673, new Func<bool>(this.ImperialOrderfirst));
			base.AddExecutor(ExecutorType.Activate, 23924608, new Func<bool>(this.HeavyStormDustereff));
			base.AddExecutor(ExecutorType.Activate, 69452756, new Func<bool>(this.UnendingNightmareeff));
			base.AddExecutor(ExecutorType.Activate, 77538567, new Func<bool>(this.DarkBribeeff));
			base.AddExecutor(ExecutorType.Activate, 61740673, new Func<bool>(this.ImperialOrdereff));
			base.AddExecutor(ExecutorType.Activate, 71564252, new Func<bool>(this.ThunderKingRaiOheff));
			base.AddExecutor(ExecutorType.Activate, 41420027, new Func<bool>(base.DefaultSolemnJudgment));
			base.AddExecutor(ExecutorType.Activate, 47475363, new Func<bool>(this.DrowningMirrorForceeff));
			base.AddExecutor(ExecutorType.Activate, 70368879, new Func<bool>(this.UpstartGoblineff));
			base.AddExecutor(ExecutorType.Activate, 18144506, new Func<bool>(base.DefaultHarpiesFeatherDusterFirst));
			base.AddExecutor(ExecutorType.Activate, 98645731, new Func<bool>(this.PotOfDualityeff));
			base.AddExecutor(ExecutorType.Activate, 35261759, new Func<bool>(this.PotOfDesireseff));
			base.AddExecutor(ExecutorType.Activate, 59750328, new Func<bool>(this.CardOfDemiseeff));
			base.AddExecutor(ExecutorType.Activate, 41999284, new Func<bool>(this.Linkuriboheff));
			base.AddExecutor(ExecutorType.SpSummon, 41999284, new Func<bool>(this.Linkuribohsp));
			base.AddExecutor(ExecutorType.SpSummon, 75452921, new Func<bool>(this.Knightmaresp));
			base.AddExecutor(ExecutorType.SpSummon, 2857636, new Func<bool>(this.Knightmaresp));
			base.AddExecutor(ExecutorType.SpSummon, 3987233, new Func<bool>(this.MissusRadiantsp));
			base.AddExecutor(ExecutorType.Activate, 3987233, new Func<bool>(this.MissusRadianteff));
			base.AddExecutor(ExecutorType.SpSummon, 41999284, new Func<bool>(this.Linkuribohsp));
			base.AddExecutor(ExecutorType.SpSummon, 98978921);
			base.AddExecutor(ExecutorType.SpSummon, 31833038, new Func<bool>(this.BorreloadDragonsp));
			base.AddExecutor(ExecutorType.Activate, 31833038, new Func<bool>(this.BorreloadDragoneff));
			base.AddExecutor(ExecutorType.Activate, 63845230, new Func<bool>(this.EaterOfMillionseff));
			base.AddExecutor(ExecutorType.Activate, 10813327, new Func<bool>(this.WakingTheDragoneff));
			base.AddExecutor(ExecutorType.Summon, 15397015, new Func<bool>(this.InspectBoardersummon));
			base.AddExecutor(ExecutorType.Summon, 36584821, new Func<bool>(this.GrenMajuDaEizosummon));
			base.AddExecutor(ExecutorType.Summon, 71564252, new Func<bool>(this.ThunderKingRaiOhsummon));
			base.AddExecutor(ExecutorType.SpSummon, 31833038, new Func<bool>(this.BorreloadDragonspsecond));
			base.AddExecutor(ExecutorType.SpSummon, 63845230, new Func<bool>(this.EaterOfMillionssp));
			base.AddExecutor(ExecutorType.Activate, 71197066, new Func<bool>(this.MetalSnakesp));
			base.AddExecutor(ExecutorType.Activate, 71197066, new Func<bool>(this.MetalSnakeeff));
			base.AddExecutor(ExecutorType.Activate, 36975314, new Func<bool>(this.Crackdowneff));
			base.AddExecutor(ExecutorType.Activate, 19508728, new Func<bool>(this.MoonMirrorShieldeff));
			base.AddExecutor(ExecutorType.Activate, 73915051, new Func<bool>(base.DefaultScapegoat));
			base.AddExecutor(ExecutorType.Activate, 61936647, new Func<bool>(this.PhatomKnightsSwordeff));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(this.MonsterRepos));
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(this.SpellSet));
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x00070C8E File Offset: 0x0006EE8E
		public override void OnNewTurn()
		{
			this.eater_eff = false;
			this.CardOfDemiseeff_used = false;
			base.OnNewTurn();
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x00070CA4 File Offset: 0x0006EEA4
		public override void OnNewPhase()
		{
			foreach (ClientCard check in base.Bot.GetMonsters())
			{
				if (check.HasType(CardType.Fusion) || check.HasType(CardType.Xyz) || check.HasType(CardType.Synchro) || check.HasType(CardType.Link) || check.HasType(CardType.Ritual))
				{
					this.eater_eff = true;
					break;
				}
			}
			foreach (ClientCard check2 in base.Enemy.GetMonsters())
			{
				if (check2.HasType(CardType.Fusion) || check2.HasType(CardType.Xyz) || check2.HasType(CardType.Synchro) || check2.HasType(CardType.Link) || check2.HasType(CardType.Ritual))
				{
					this.eater_eff = true;
					break;
				}
			}
			base.OnNewPhase();
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x00070DCC File Offset: 0x0006EFCC
		private bool GoToBattlePhase()
		{
			return base.Bot.HasInHand(15693423) && base.Duel.Turn >= 2 && base.Enemy.GetFieldCount() >= 2 && base.Bot.GetFieldCount() == 0;
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x0006EA1A File Offset: 0x0006CC1A
		private bool MacroCosmoseff()
		{
			return (base.Duel.LastChainPlayer == 1 || base.Duel.LastSummonPlayer == 1 || base.Duel.Player == 0) && base.UniqueFaceupSpell();
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x00070E0C File Offset: 0x0006F00C
		private bool AntiSpellFragranceeff()
		{
			int spell_count = 0;
			using (IEnumerator<ClientCard> enumerator = base.Bot.Hand.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.HasType(CardType.Spell))
					{
						spell_count++;
					}
				}
			}
			return spell_count < 2 && base.Duel.Player == 1 && base.UniqueFaceupSpell();
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x00070E80 File Offset: 0x0006F080
		private bool EvenlyMatchedeff()
		{
			return base.Enemy.GetFieldCount() - base.Bot.GetFieldCount() > 1;
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x00070E9C File Offset: 0x0006F09C
		private bool HeavyStormDustereff()
		{
			IList<ClientCard> targets = new List<ClientCard>();
			foreach (ClientCard check in base.Enemy.GetSpells())
			{
				if (check.HasType(CardType.Continuous) || check.HasType(CardType.Field))
				{
					targets.Add(check);
				}
			}
			if (base.Util.GetPZone(1, 0) != null && base.Util.GetPZone(1, 0).Type == 16777218)
			{
				targets.Add(base.Util.GetPZone(1, 0));
			}
			if (base.Util.GetPZone(1, 1) != null && base.Util.GetPZone(1, 1).Type == 16777218)
			{
				targets.Add(base.Util.GetPZone(1, 1));
			}
			foreach (ClientCard check2 in base.Enemy.GetSpells())
			{
				if (!check2.HasType(CardType.Continuous) && !check2.HasType(CardType.Field))
				{
					targets.Add(check2);
				}
			}
			if (base.DefaultOnBecomeTarget())
			{
				base.AI.SelectCard(targets);
				return true;
			}
			int count = 0;
			using (List<ClientCard>.Enumerator enumerator = base.Enemy.GetSpells().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Type == 16777218)
					{
						count++;
					}
				}
			}
			if (base.Util.GetLastChainCard() != null && (base.Util.GetLastChainCard().HasType(CardType.Continuous) || base.Util.GetLastChainCard().HasType(CardType.Field) || count == 2) && base.Duel.LastChainPlayer == 1)
			{
				base.AI.SelectCard(targets);
				return true;
			}
			return false;
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x000710B4 File Offset: 0x0006F2B4
		private bool UnendingNightmareeff()
		{
			if (base.Card.IsDisabled())
			{
				return false;
			}
			ClientCard card = null;
			using (List<ClientCard>.Enumerator enumerator = base.Enemy.GetSpells().GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					ClientCard check = enumerator.Current;
					if (check.HasType(CardType.Continuous) || check.HasType(CardType.Field))
					{
						card = check;
					}
				}
			}
			int count = 0;
			using (List<ClientCard>.Enumerator enumerator = base.Enemy.GetSpells().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Type == 16777218)
					{
						count++;
					}
				}
			}
			if (count == 2 && base.Util.GetPZone(1, 1) != null && base.Util.GetPZone(1, 1).Type == 16777218)
			{
				card = base.Util.GetPZone(1, 1);
			}
			if (card != null && base.Bot.LifePoints > 1000)
			{
				base.AI.SelectCard(card);
				return true;
			}
			return false;
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x000711E4 File Offset: 0x0006F3E4
		private bool DarkBribeeff()
		{
			return base.Util.GetLastChainCard() == null || !base.Util.GetLastChainCard().IsCode(70368879);
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x00071210 File Offset: 0x0006F410
		private bool ImperialOrderfirst()
		{
			return (base.Util.GetLastChainCard() == null || !base.Util.GetLastChainCard().IsCode(70368879)) && base.DefaultOnBecomeTarget() && base.Util.GetLastChainCard().HasType(CardType.Spell);
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x00071260 File Offset: 0x0006F460
		private bool ImperialOrdereff()
		{
			if (base.Util.GetLastChainCard() != null && base.Util.GetLastChainCard().IsCode(70368879))
			{
				return false;
			}
			if (base.Duel.LastChainPlayer == 1)
			{
				foreach (ClientCard check in base.Enemy.GetSpells())
				{
					if (base.Util.GetLastChainCard() == check)
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x000712FC File Offset: 0x0006F4FC
		private bool DrowningMirrorForceeff()
		{
			if (base.Enemy.GetMonsterCount() == 1 && base.Enemy.BattlingMonster.Attack - base.Bot.LifePoints >= 1000)
			{
				return base.DefaultUniqueTrap();
			}
			if (base.Util.GetTotalAttackingMonsterAttack(1) >= base.Bot.LifePoints)
			{
				return base.DefaultUniqueTrap();
			}
			return base.Enemy.GetMonsterCount() >= 2 && base.DefaultUniqueTrap();
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x00071377 File Offset: 0x0006F577
		private bool UpstartGoblineff()
		{
			return !base.DefaultSpellWillBeNegated();
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x00071384 File Offset: 0x0006F584
		private bool PotOfDualityeff()
		{
			if (base.DefaultSpellWillBeNegated())
			{
				return false;
			}
			int count = 0;
			if (base.Bot.GetMonsterCount() > 0)
			{
				count = 1;
			}
			using (IEnumerator<ClientCard> enumerator = base.Bot.Hand.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.HasType(CardType.Monster))
					{
						count++;
					}
				}
			}
			if (base.Util.GetBestEnemyMonster(false, false) != null && base.Util.GetBestEnemyMonster(false, false).Attack >= 1900)
			{
				base.AI.SelectCard(new int[] { 63845230, 35261759, 36584821, 15397015, 71564252, 73915051, 41420027, 84749824, 40605147, 10045474 });
			}
			if (count == 0)
			{
				base.AI.SelectCard(new int[] { 35261759, 15397015, 71564252, 63845230, 36584821, 73915051 });
			}
			else
			{
				base.AI.SelectCard(new int[] { 35261759, 59750328, 41420027, 84749824, 40605147, 10045474, 73915051 });
			}
			return true;
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x0007147C File Offset: 0x0006F67C
		private bool PotOfDesireseff()
		{
			return !this.CardOfDemiseeff_used && base.Bot.Deck.Count > 14 && !base.DefaultSpellWillBeNegated();
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x000714A7 File Offset: 0x0006F6A7
		private bool CardOfDemiseeff()
		{
			if (base.Bot.Hand.Count == 1 && base.Bot.GetSpellCountWithoutField() <= 3 && !base.DefaultSpellWillBeNegated())
			{
				this.CardOfDemiseeff_used = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x000714DC File Offset: 0x0006F6DC
		private bool Crackdowneff()
		{
			if (base.Util.GetOneEnemyBetterThanMyBest(true, true) != null && base.Bot.UnderAttack)
			{
				base.AI.SelectCard(base.Util.GetOneEnemyBetterThanMyBest(true, true));
			}
			return base.Util.GetOneEnemyBetterThanMyBest(true, true) != null && base.Bot.UnderAttack;
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x00071539 File Offset: 0x0006F739
		private bool MoonMirrorShieldeff()
		{
			if (base.Card.Location == CardLocation.Hand)
			{
				return base.Bot.GetMonsterCount() != 0 && !base.DefaultSpellWillBeNegated();
			}
			return base.Card.Location == CardLocation.Grave;
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x00071574 File Offset: 0x0006F774
		private bool PhatomKnightsSwordeff()
		{
			if (base.Card.IsFaceup())
			{
				return true;
			}
			if (base.Duel.Phase == DuelPhase.BattleStart && base.Bot.BattlingMonster != null && base.Enemy.BattlingMonster != null && base.Bot.BattlingMonster.Attack + 800 >= base.Enemy.BattlingMonster.GetDefensePower())
			{
				base.AI.SelectCard(base.Bot.BattlingMonster);
				return base.DefaultUniqueTrap();
			}
			return false;
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x0006EC59 File Offset: 0x0006CE59
		private bool InspectBoardersummon()
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

		// Token: 0x06001458 RID: 5208 RVA: 0x00071600 File Offset: 0x0006F800
		private bool GrenMajuDaEizosummon()
		{
			if (base.Duel.Turn == 1)
			{
				return false;
			}
			if (base.Bot.MonsterZone[0] == null)
			{
				base.AI.SelectPlace(1);
			}
			else
			{
				base.AI.SelectPlace(16);
			}
			return base.Bot.Banished.Count >= 6;
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x0006EC59 File Offset: 0x0006CE59
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

		// Token: 0x0600145A RID: 5210 RVA: 0x00071660 File Offset: 0x0006F860
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

		// Token: 0x0600145B RID: 5211 RVA: 0x000716DC File Offset: 0x0006F8DC
		private bool BorreloadDragonsp()
		{
			if (!base.Bot.HasInMonstersZone(3987233, false, false, false) && !base.Bot.HasInMonstersZone(new int[] { 75452921, 2857636 }, false, false, false))
			{
				return false;
			}
			IList<ClientCard> material_list = new List<ClientCard>();
			foreach (ClientCard monster in base.Bot.GetMonsters())
			{
				if (monster.IsCode(new int[] { 3987233, 75452921, 2857636, 98978921, 41999284 }))
				{
					material_list.Add(monster);
				}
				if (material_list.Count == 3)
				{
					break;
				}
			}
			if (material_list.Count >= 3)
			{
				base.AI.SelectMaterials(material_list, 0);
				return true;
			}
			return false;
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x000717B4 File Offset: 0x0006F9B4
		private bool BorreloadDragonspsecond()
		{
			if (!base.Bot.HasInMonstersZone(3987233, false, false, false) && !base.Bot.HasInMonstersZone(new int[] { 75452921, 2857636 }, false, false, false))
			{
				return false;
			}
			IList<ClientCard> material_list = new List<ClientCard>();
			foreach (ClientCard monster in base.Bot.GetMonsters())
			{
				if (monster.IsCode(new int[] { 3987233, 75452921, 2857636, 98978921, 41999284 }))
				{
					material_list.Add(monster);
				}
				if (material_list.Count == 3)
				{
					break;
				}
			}
			if (material_list.Count >= 3)
			{
				base.AI.SelectMaterials(material_list, 0);
				return true;
			}
			return false;
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x0007188C File Offset: 0x0006FA8C
		public bool BorreloadDragoneff()
		{
			if (base.ActivateDescription == -1 && (base.Duel.Phase == DuelPhase.BattleStart || base.Duel.Phase == DuelPhase.End))
			{
				ClientCard enemy_monster = base.Enemy.BattlingMonster;
				return enemy_monster == null || !enemy_monster.HasPosition(CardPosition.Attack) || base.Card.Attack - enemy_monster.Attack < base.Enemy.LifePoints;
			}
			ClientCard BestEnemy = base.Util.GetBestEnemyMonster(true, false);
			ClientCard WorstBot = base.Bot.GetMonsters().GetLowestAttackMonster(false);
			if (BestEnemy == null || BestEnemy.HasPosition(CardPosition.FaceDown))
			{
				return false;
			}
			if (WorstBot == null || WorstBot.HasPosition(CardPosition.FaceDown))
			{
				return false;
			}
			if (BestEnemy.Attack >= WorstBot.RealPower)
			{
				base.AI.SelectCard(BestEnemy);
				return true;
			}
			return false;
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x00071958 File Offset: 0x0006FB58
		private bool EaterOfMillionssp()
		{
			if (base.Bot.MonsterZone[0] == null)
			{
				base.AI.SelectPlace(1);
			}
			else
			{
				base.AI.SelectPlace(16);
			}
			if (base.Enemy.HasInMonstersZone(65330383, true, false, false))
			{
				return false;
			}
			if (base.Bot.HasInMonstersZone(15397015, false, false, false) && !this.eater_eff)
			{
				return false;
			}
			if (base.Util.GetProblematicEnemyMonster(0, false) == null && base.Bot.ExtraDeck.Count < 5)
			{
				return false;
			}
			if (base.Bot.GetMonstersInMainZone().Count >= 5)
			{
				return false;
			}
			if (base.Util.IsTurn1OrMain2())
			{
				return false;
			}
			base.AI.SelectPosition(CardPosition.FaceUpAttack);
			IList<ClientCard> targets = new List<ClientCard>();
			foreach (ClientCard e_c in base.Bot.ExtraDeck)
			{
				targets.Add(e_c);
				if (targets.Count >= 5)
				{
					base.AI.SelectMaterials(targets, 503);
					base.AI.SelectPlace(17);
					return true;
				}
			}
			Logger.DebugWriteLine("*** Eater use up the extra deck.");
			foreach (ClientCard s_c in base.Bot.GetSpells())
			{
				targets.Add(s_c);
				if (targets.Count >= 5)
				{
					base.AI.SelectMaterials(targets, 503);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x00071B04 File Offset: 0x0006FD04
		private bool EaterOfMillionseff()
		{
			return !base.Enemy.BattlingMonster.HasPosition(CardPosition.Attack) || base.Bot.BattlingMonster.Attack - base.Enemy.BattlingMonster.GetDefensePower() < base.Enemy.LifePoints;
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x00071B55 File Offset: 0x0006FD55
		private bool WakingTheDragoneff()
		{
			base.AI.SelectCard(new int[] { 86221741 });
			return true;
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x00071B74 File Offset: 0x0006FD74
		private bool MetalSnakesp()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (base.ActivateDescription == base.Util.GetStringId(71197066, 0) && !base.Bot.HasInMonstersZone(71197066, false, false, false))
			{
				if (base.Duel.Player == 1 && base.Duel.Phase >= DuelPhase.BattleStart)
				{
					return base.Bot.Deck.Count >= 12;
				}
				if (base.Duel.Player == 0 && base.Duel.Phase >= DuelPhase.Main1)
				{
					return base.Bot.Deck.Count >= 12;
				}
			}
			return false;
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x00071C28 File Offset: 0x0006FE28
		private bool MetalSnakeeff()
		{
			ClientCard target = base.Util.GetOneEnemyBetterThanMyBest(true, true);
			if (base.ActivateDescription == base.Util.GetStringId(71197066, 1) && target != null)
			{
				base.AI.SelectCard(new int[] { 24094258, 63288573, 50588353, 86221741, 30194529 });
				base.AI.SelectNextCard(target);
				return true;
			}
			return false;
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x00071C8C File Offset: 0x0006FE8C
		private bool MissusRadiantsp()
		{
			IList<ClientCard> material_list = new List<ClientCard>();
			foreach (ClientCard monster in base.Bot.GetMonsters())
			{
				if (monster.HasAttribute(CardAttribute.Earth) && monster.Level == 1 && !monster.IsCode(63845230))
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

		// Token: 0x06001464 RID: 5220 RVA: 0x00055600 File Offset: 0x00053800
		private bool MissusRadianteff()
		{
			base.AI.SelectCard(new int[] { 23434538, 3987233 });
			return true;
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x00071D90 File Offset: 0x0006FF90
		private bool Linkuribohsp()
		{
			foreach (ClientCard c in base.Bot.GetMonsters())
			{
				if (!c.IsCode(new int[] { 63845230, 41999284 }) && c.Level == 1)
				{
					base.AI.SelectMaterials(c, 0);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x00071E1C File Offset: 0x0007001C
		private bool Knightmaresp()
		{
			int[] firstMats = new int[] { 75452921, 2857636 };
			if (base.Bot.MonsterZone.GetMatchingCardsCount((ClientCard card) => card.IsCode(firstMats)) >= 1)
			{
				return false;
			}
			foreach (ClientCard c in base.Bot.GetMonsters())
			{
				if (!c.IsCode(63845230) && c.Level == 1)
				{
					base.AI.SelectMaterials(c, 0);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x0006F99C File Offset: 0x0006DB9C
		private bool Linkuriboheff()
		{
			return base.Duel.LastChainPlayer != 0 || !base.Util.GetLastChainCard().IsCode(41999284);
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x00071EDC File Offset: 0x000700DC
		private bool MonsterRepos()
		{
			return (!base.Card.IsCode(63845230) || !base.Card.IsAttack()) && base.DefaultMonsterRepos();
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x00071F08 File Offset: 0x00070108
		private bool SpellSet()
		{
			int count = 0;
			using (IEnumerator<ClientCard> enumerator = base.Bot.Hand.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsCode(59750328))
					{
						count++;
					}
				}
			}
			if (count == 2 && base.Bot.Hand.Count == 2 && base.Bot.GetSpellCountWithoutField() <= 2)
			{
				return true;
			}
			if (base.Card.IsCode(30241314) && base.Bot.HasInSpellZone(30241314, false, false))
			{
				return false;
			}
			if (base.Card.IsCode(58921041) && base.Bot.HasInSpellZone(58921041, false, false))
			{
				return false;
			}
			if (this.CardOfDemiseeff_used)
			{
				return true;
			}
			if (base.Card.IsCode(15693423) && base.Enemy.GetFieldCount() - base.Bot.GetFieldCount() < 0)
			{
				return false;
			}
			if (base.Card.IsCode(58921041) && base.Bot.HasInSpellZone(58921041, false, false))
			{
				return false;
			}
			if (base.Card.IsCode(30241314) && base.Bot.HasInSpellZone(30241314, false, false))
			{
				return false;
			}
			if (base.Duel.Turn > 1 && base.Duel.Phase == DuelPhase.Main1 && base.Bot.HasAttackingMonster())
			{
				return false;
			}
			if (base.Card.IsCode(10045474))
			{
				return base.Bot.GetFieldCount() > 0 && base.Bot.GetSpellCountWithoutField() < 4;
			}
			if (base.Card.IsCode(73915051))
			{
				return true;
			}
			if (base.Card.HasType(CardType.Trap))
			{
				return base.Bot.GetSpellCountWithoutField() < 4;
			}
			if (base.Bot.HasInSpellZone(58921041, true, false))
			{
				if (base.Card.IsCode(new int[] { 70368879, 35261759, 98645731 }))
				{
					return true;
				}
				if (base.Card.IsCode(59750328) && base.Bot.HasInSpellZone(59750328, false, false))
				{
					return false;
				}
				if (base.Card.HasType(CardType.Spell))
				{
					return base.Bot.GetSpellCountWithoutField() < 4;
				}
			}
			return false;
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x0007216C File Offset: 0x0007036C
		public override bool OnPreBattleBetween(ClientCard attacker, ClientCard defender)
		{
			if (attacker.IsCode(63845230) && base.Bot.HasInMonstersZone(15397015, false, false, false) && this.eater_eff && !attacker.IsDisabled())
			{
				attacker.RealPower = 9999;
				return true;
			}
			if (attacker.IsCode(63845230) && !base.Bot.HasInMonstersZone(15397015, false, false, false) && !attacker.IsDisabled())
			{
				attacker.RealPower = 9999;
				return true;
			}
			return base.OnPreBattleBetween(attacker, defender);
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x000721F8 File Offset: 0x000703F8
		public override ClientCard OnSelectAttacker(IList<ClientCard> attackers, IList<ClientCard> defenders)
		{
			for (int i = 0; i < attackers.Count; i++)
			{
				ClientCard attacker = attackers[i];
				if (attacker.IsCode(new int[] { 85289965, 63845230 }))
				{
					return attacker;
				}
			}
			return null;
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x0000763C File Offset: 0x0000583C
		public override bool OnSelectHand()
		{
			return true;
		}

		// Token: 0x040018C3 RID: 6339
		private bool CardOfDemiseeff_used;

		// Token: 0x040018C4 RID: 6340
		private bool eater_eff;

		// Token: 0x02000313 RID: 787
		public class CardId
		{
			// Token: 0x040018C5 RID: 6341
			public const int MetalSnake = 71197066;

			// Token: 0x040018C6 RID: 6342
			public const int InspectBoarder = 15397015;

			// Token: 0x040018C7 RID: 6343
			public const int ThunderKingRaiOh = 71564252;

			// Token: 0x040018C8 RID: 6344
			public const int AshBlossomAndJoyousSpring = 14558127;

			// Token: 0x040018C9 RID: 6345
			public const int GhostReaperAndWinterCherries = 62015408;

			// Token: 0x040018CA RID: 6346
			public const int GrenMajuDaEizo = 36584821;

			// Token: 0x040018CB RID: 6347
			public const int MaxxC = 23434538;

			// Token: 0x040018CC RID: 6348
			public const int EaterOfMillions = 63845230;

			// Token: 0x040018CD RID: 6349
			public const int HarpieFeatherDuster = 18144506;

			// Token: 0x040018CE RID: 6350
			public const int PotOfDesires = 35261759;

			// Token: 0x040018CF RID: 6351
			public const int CardOfDemise = 59750328;

			// Token: 0x040018D0 RID: 6352
			public const int UpstartGoblin = 70368879;

			// Token: 0x040018D1 RID: 6353
			public const int PotOfDuality = 98645731;

			// Token: 0x040018D2 RID: 6354
			public const int Scapegoat = 73915051;

			// Token: 0x040018D3 RID: 6355
			public const int MoonMirrorShield = 19508728;

			// Token: 0x040018D4 RID: 6356
			public const int InfiniteImpermanence = 10045474;

			// Token: 0x040018D5 RID: 6357
			public const int WakingTheDragon = 10813327;

			// Token: 0x040018D6 RID: 6358
			public const int EvenlyMatched = 15693423;

			// Token: 0x040018D7 RID: 6359
			public const int HeavyStormDuster = 23924608;

			// Token: 0x040018D8 RID: 6360
			public const int DrowningMirrorForce = 47475363;

			// Token: 0x040018D9 RID: 6361
			public const int MacroCosmos = 30241314;

			// Token: 0x040018DA RID: 6362
			public const int Crackdown = 36975314;

			// Token: 0x040018DB RID: 6363
			public const int AntiSpellFragrance = 58921041;

			// Token: 0x040018DC RID: 6364
			public const int ImperialOrder = 61740673;

			// Token: 0x040018DD RID: 6365
			public const int PhatomKnightsSword = 61936647;

			// Token: 0x040018DE RID: 6366
			public const int UnendingNightmare = 69452756;

			// Token: 0x040018DF RID: 6367
			public const int SolemnWarning = 84749824;

			// Token: 0x040018E0 RID: 6368
			public const int SolemStrike = 40605147;

			// Token: 0x040018E1 RID: 6369
			public const int SolemnJudgment = 41420027;

			// Token: 0x040018E2 RID: 6370
			public const int DarkBribe = 77538567;

			// Token: 0x040018E3 RID: 6371
			public const int RaidraptorUltimateFalcon = 86221741;

			// Token: 0x040018E4 RID: 6372
			public const int BorreloadDragon = 31833038;

			// Token: 0x040018E5 RID: 6373
			public const int BirrelswordDragon = 85289965;

			// Token: 0x040018E6 RID: 6374
			public const int FirewallDragon = 5043010;

			// Token: 0x040018E7 RID: 6375
			public const int NingirsuTheWorldChaliceWarrior = 30194529;

			// Token: 0x040018E8 RID: 6376
			public const int TopologicTrisbaena = 72529749;

			// Token: 0x040018E9 RID: 6377
			public const int KnightmareUnicorn = 38342335;

			// Token: 0x040018EA RID: 6378
			public const int KnightmarePhoenix = 2857636;

			// Token: 0x040018EB RID: 6379
			public const int HeavymetalfoesElectrumite = 24094258;

			// Token: 0x040018EC RID: 6380
			public const int KnightmareCerberus = 75452921;

			// Token: 0x040018ED RID: 6381
			public const int CrystronNeedlefiber = 50588353;

			// Token: 0x040018EE RID: 6382
			public const int MissusRadiant = 3987233;

			// Token: 0x040018EF RID: 6383
			public const int BrandishMaidenKagari = 63288573;

			// Token: 0x040018F0 RID: 6384
			public const int LinkSpider = 98978921;

			// Token: 0x040018F1 RID: 6385
			public const int Linkuriboh = 41999284;

			// Token: 0x040018F2 RID: 6386
			public const int KnightmareGryphon = 65330383;
		}
	}
}
