using System;
using System.Collections.Generic;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x020003D5 RID: 981
	[Deck("SkyStriker", "AI_SkyStriker", "Normal")]
	public class SkyStrikerExecutor : DefaultExecutor
	{
		// Token: 0x06001E01 RID: 7681 RVA: 0x000B5CE0 File Offset: 0x000B3EE0
		public SkyStrikerExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 14558127, new Func<bool>(base.DefaultAshBlossomAndJoyousSpring));
			base.AddExecutor(ExecutorType.Activate, 59438930, new Func<bool>(base.DefaultGhostOgreAndSnowRabbit));
			base.AddExecutor(ExecutorType.Activate, 97268402, new Func<bool>(base.DefaultBreakthroughSkill));
			base.AddExecutor(ExecutorType.Activate, 84749824, new Func<bool>(base.DefaultSolemnWarning));
			base.AddExecutor(ExecutorType.Activate, 41420027, new Func<bool>(base.DefaultSolemnJudgment));
			base.AddExecutor(ExecutorType.Activate, 23434538, new Func<bool>(this.MaxxCEffect));
			base.AddExecutor(ExecutorType.Activate, 32807846);
			base.AddExecutor(ExecutorType.Activate, 70368879);
			base.AddExecutor(ExecutorType.Activate, 35726888, new Func<bool>(this.FoolishBurialGoodsEffect));
			base.AddExecutor(ExecutorType.Activate, 43898403, new Func<bool>(this.TwinTwistersEffect));
			base.AddExecutor(ExecutorType.Activate, 24010609, new Func<bool>(this.MultiroleHandEffect));
			base.AddExecutor(ExecutorType.Activate, 98338152, new Func<bool>(this.WidowAnchorEffectFirst));
			base.AddExecutor(ExecutorType.Activate, 99550630, new Func<bool>(this.AfterburnersEffect));
			base.AddExecutor(ExecutorType.Activate, 25955749, new Func<bool>(this.JammingWaveEffect));
			base.AddExecutor(ExecutorType.Activate, 63166095, new Func<bool>(this.EngageEffectFirst));
			base.AddExecutor(ExecutorType.Activate, 52340444, new Func<bool>(this.HornetDronesEffect));
			base.AddExecutor(ExecutorType.Activate, 98338152, new Func<bool>(this.WidowAnchorEffect));
			base.AddExecutor(ExecutorType.Activate, 97616504, new Func<bool>(this.HerculesBaseEffect));
			base.AddExecutor(ExecutorType.Activate, 50005218, new Func<bool>(this.AreaZeroEffect));
			base.AddExecutor(ExecutorType.Activate, 24010609, new Func<bool>(this.MultiroleEffect));
			base.AddExecutor(ExecutorType.Activate, 63166095, new Func<bool>(this.EngageEffect));
			base.AddExecutor(ExecutorType.Summon, 9742784, new Func<bool>(this.TunerSummon));
			base.AddExecutor(ExecutorType.Summon, 97268402, new Func<bool>(this.TunerSummon));
			base.AddExecutor(ExecutorType.Summon, 59438930, new Func<bool>(this.TunerSummon));
			base.AddExecutor(ExecutorType.Summon, 14558127, new Func<bool>(this.TunerSummon));
			base.AddExecutor(ExecutorType.Activate, 26077387, new Func<bool>(this.RayeEffect));
			base.AddExecutor(ExecutorType.SpSummon, 63288573, new Func<bool>(this.KagariSummon));
			base.AddExecutor(ExecutorType.Activate, 63288573, new Func<bool>(this.KagariEffect));
			base.AddExecutor(ExecutorType.SpSummon, 50588353, new Func<bool>(this.CrystronNeedlefiberSummon));
			base.AddExecutor(ExecutorType.Activate, 50588353, new Func<bool>(this.CrystronNeedlefiberEffect));
			base.AddExecutor(ExecutorType.SpSummon, 61665245);
			base.AddExecutor(ExecutorType.Activate, 61665245, new Func<bool>(this.SummonSorceressEffect));
			base.AddExecutor(ExecutorType.Activate, 9742784, new Func<bool>(this.JetSynchronEffect));
			base.AddExecutor(ExecutorType.SpSummon, 42110604);
			base.AddExecutor(ExecutorType.SpSummon, 90673288, new Func<bool>(this.ShizukuSummon));
			base.AddExecutor(ExecutorType.Activate, 90673288, new Func<bool>(this.ShizukuEffect));
			base.AddExecutor(ExecutorType.SpSummon, 8491308, new Func<bool>(this.HayateSummon));
			base.AddExecutor(ExecutorType.Activate, 8491308, new Func<bool>(this.HayateEffect));
			base.AddExecutor(ExecutorType.SpSummon, 5821478, new Func<bool>(base.Util.IsTurn1OrMain2));
			base.AddExecutor(ExecutorType.Summon, 26077387, new Func<bool>(this.RayeSummon));
			base.AddExecutor(ExecutorType.SpellSet, 41420027);
			base.AddExecutor(ExecutorType.SpellSet, 84749824);
			base.AddExecutor(ExecutorType.SpellSet, 98338152);
			base.AddExecutor(ExecutorType.SpellSet, 97616504);
			base.AddExecutor(ExecutorType.SpellSet, 43898403, new Func<bool>(this.HandFull));
			base.AddExecutor(ExecutorType.SpellSet, 52340444, new Func<bool>(this.HandFull));
			base.AddExecutor(ExecutorType.Activate, 73594093);
			base.AddExecutor(ExecutorType.Activate, 24010609, new Func<bool>(this.MultiroleEPEffect));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(base.DefaultMonsterRepos));
		}

		// Token: 0x06001E02 RID: 7682 RVA: 0x0000763C File Offset: 0x0000583C
		public override bool OnSelectHand()
		{
			return true;
		}

		// Token: 0x06001E03 RID: 7683 RVA: 0x000B6121 File Offset: 0x000B4321
		public override void OnNewTurn()
		{
			this.KagariSummoned = false;
			this.ShizukuSummoned = false;
			this.HayateSummoned = false;
			this.WidowAnchorTarget = null;
			base.OnNewTurn();
		}

		// Token: 0x06001E04 RID: 7684 RVA: 0x000B6145 File Offset: 0x000B4345
		public override bool OnPreBattleBetween(ClientCard attacker, ClientCard defender)
		{
			if (!defender.IsMonsterHasPreventActivationEffectInBattle() && attacker.IsCode(42110604) && !attacker.IsDisabled())
			{
				attacker.RealPower += 200;
			}
			return base.OnPreBattleBetween(attacker, defender);
		}

		// Token: 0x06001E05 RID: 7685 RVA: 0x000B6180 File Offset: 0x000B4380
		public override bool OnSelectYesNo(int desc)
		{
			if (desc == base.Util.GetStringId(61665245, 2))
			{
				return false;
			}
			if (desc == base.Util.GetStringId(63166095, 0))
			{
				return true;
			}
			if (desc == base.Util.GetStringId(98338152, 0))
			{
				return true;
			}
			if (desc == base.Util.GetStringId(25955749, 0))
			{
				ClientCard target = base.Util.GetBestEnemyMonster(false, false);
				if (target != null)
				{
					base.AI.SelectCard(target);
					return true;
				}
				return false;
			}
			else
			{
				if (desc != base.Util.GetStringId(99550630, 0))
				{
					return base.OnSelectYesNo(desc);
				}
				ClientCard target2 = base.Util.GetBestEnemySpell(false);
				if (target2 != null)
				{
					base.AI.SelectCard(target2);
					return true;
				}
				return false;
			}
		}

		// Token: 0x06001E06 RID: 7686 RVA: 0x000348E3 File Offset: 0x00032AE3
		private bool MaxxCEffect()
		{
			return !base.DefaultCheckWhetherCardIsNegated(base.Card) && base.Duel.Player == 1;
		}

		// Token: 0x06001E07 RID: 7687 RVA: 0x000B6240 File Offset: 0x000B4440
		private bool TwinTwistersEffect()
		{
			if (base.Util.ChainContainsCard(43898403))
			{
				return false;
			}
			IList<ClientCard> targets = new List<ClientCard>();
			foreach (ClientCard target in base.Enemy.GetSpells())
			{
				if (target.IsFloodgate())
				{
					targets.Add(target);
				}
				if (targets.Count >= 2)
				{
					break;
				}
			}
			if (targets.Count < 2)
			{
				foreach (ClientCard target2 in base.Enemy.GetSpells())
				{
					if (target2.IsFacedown() || target2.HasType(CardType.Continuous) || target2.HasType(CardType.Pendulum))
					{
						targets.Add(target2);
					}
					if (targets.Count >= 2)
					{
						break;
					}
				}
			}
			if (targets.Count > 0)
			{
				base.AI.SelectCard(this.GetDiscardHand());
				base.AI.SelectNextCard(targets);
				return true;
			}
			return false;
		}

		// Token: 0x06001E08 RID: 7688 RVA: 0x000B636C File Offset: 0x000B456C
		private bool FoolishBurialGoodsEffect()
		{
			base.AI.SelectCard(new int[] { 73594093, 98338152, 63166095, 52340444 });
			return true;
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x00056B7D File Offset: 0x00054D7D
		private bool MultiroleHandEffect()
		{
			return base.Card.Location == CardLocation.Hand;
		}

		// Token: 0x06001E0A RID: 7690 RVA: 0x000B638C File Offset: 0x000B458C
		private bool MultiroleEPEffect()
		{
			if (base.Duel.Phase != DuelPhase.End)
			{
				return false;
			}
			IList<int> targets = new int[] { 63166095, 52340444, 98338152 };
			base.AI.SelectCard(targets);
			base.AI.SelectNextCard(targets);
			base.AI.SelectThirdCard(targets);
			return true;
		}

		// Token: 0x06001E0B RID: 7691 RVA: 0x000B63E4 File Offset: 0x000B45E4
		private bool AfterburnersEffect()
		{
			ClientCard target = base.Util.GetBestEnemyMonster(true, true);
			if (target != null)
			{
				base.AI.SelectCard(target);
				return true;
			}
			return false;
		}

		// Token: 0x06001E0C RID: 7692 RVA: 0x000B6414 File Offset: 0x000B4614
		private bool JammingWaveEffect()
		{
			ClientCard target = null;
			foreach (ClientCard card in base.Enemy.GetSpells())
			{
				if (card.IsFacedown())
				{
					target = card;
					break;
				}
			}
			if (target != null)
			{
				base.AI.SelectCard(target);
				return true;
			}
			return false;
		}

		// Token: 0x06001E0D RID: 7693 RVA: 0x000B6488 File Offset: 0x000B4688
		private bool WidowAnchorEffectFirst()
		{
			if (base.Util.ChainContainsCard(98338152))
			{
				return false;
			}
			ClientCard target = base.Util.GetProblematicEnemyMonster(0, true);
			if (target != null)
			{
				this.WidowAnchorTarget = target;
				base.AI.SelectCard(target);
				return true;
			}
			return false;
		}

		// Token: 0x06001E0E RID: 7694 RVA: 0x000B64D0 File Offset: 0x000B46D0
		private bool EngageEffectFirst()
		{
			if (!this.HaveThreeSpellsInGrave())
			{
				return false;
			}
			int target = this.GetCardToSearch();
			if (target > 0)
			{
				base.AI.SelectCard(target);
			}
			else
			{
				base.AI.SelectCard(new int[] { 24010609, 50005218, 99550630, 25955749, 26077387 });
			}
			return true;
		}

		// Token: 0x06001E0F RID: 7695 RVA: 0x000B6520 File Offset: 0x000B4720
		private bool EngageEffect()
		{
			int target = this.GetCardToSearch();
			if (target > 0)
			{
				base.AI.SelectCard(target);
			}
			else
			{
				base.AI.SelectCard(new int[] { 24010609, 50005218, 99550630, 25955749, 26077387 });
			}
			return true;
		}

		// Token: 0x06001E10 RID: 7696 RVA: 0x000B6564 File Offset: 0x000B4764
		private bool HornetDronesEffect()
		{
			if (base.Duel.Player == 1)
			{
				return base.Duel.Phase == DuelPhase.End;
			}
			if (base.Duel.Phase != DuelPhase.Main1)
			{
				return false;
			}
			if (base.Duel.CurrentChain.Count > 0)
			{
				return false;
			}
			if (base.Bot.GetMonstersExtraZoneCount() == 0)
			{
				return true;
			}
			if (base.Bot.HasInMonstersZone(61665245, false, false, false))
			{
				return true;
			}
			if (base.Bot.HasInMonstersZone(5821478, false, false, false) && base.Enemy.GetMonsterCount() > 1)
			{
				return true;
			}
			if (!base.Util.IsTurn1OrMain2())
			{
				using (IEnumerator<ClientCard> enumerator = base.Bot.Hand.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.IsTuner())
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001E11 RID: 7697 RVA: 0x000B665C File Offset: 0x000B485C
		private bool WidowAnchorEffect()
		{
			if (base.DefaultBreakthroughSkill())
			{
				this.WidowAnchorTarget = base.Util.GetLastChainCard();
				return true;
			}
			if (!this.HaveThreeSpellsInGrave() || base.Duel.Player == 1 || base.Duel.Phase < DuelPhase.Main1 || base.Duel.Phase >= DuelPhase.Main2 || base.Util.ChainContainsCard(98338152))
			{
				return false;
			}
			ClientCard target = base.Util.GetBestEnemyMonster(true, true);
			if (target != null && !target.IsDisabled() && !target.HasType(CardType.Normal))
			{
				this.WidowAnchorTarget = target;
				base.AI.SelectCard(target);
				return true;
			}
			return false;
		}

		// Token: 0x06001E12 RID: 7698 RVA: 0x000B6708 File Offset: 0x000B4908
		private bool HerculesBaseEffect()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				IList<ClientCard> targets = new List<ClientCard>();
				foreach (ClientCard card in base.Bot.GetGraveyardMonsters())
				{
					if (card.IsCode(new int[] { 8491308, 63288573, 90673288 }))
					{
						targets.Add(card);
					}
				}
				if (targets.Count > 0)
				{
					base.AI.SelectCard(targets);
					return true;
				}
			}
			else
			{
				if (base.Util.IsTurn1OrMain2())
				{
					return false;
				}
				ClientCard bestBotMonster = base.Util.GetBestBotMonster(true);
				if (bestBotMonster != null)
				{
					int bestPower = bestBotMonster.Attack;
					int count = 0;
					bool have3 = this.HaveThreeSpellsInGrave();
					foreach (ClientCard target in base.Enemy.GetMonsters())
					{
						if (target.GetDefensePower() < bestPower && !target.IsMonsterInvincible())
						{
							count++;
							if (count > 1 || have3)
							{
								base.AI.SelectCard(bestBotMonster);
								return true;
							}
						}
					}
					return false;
				}
			}
			return false;
		}

		// Token: 0x06001E13 RID: 7699 RVA: 0x000B6854 File Offset: 0x000B4A54
		private bool AreaZeroEffect()
		{
			if (base.Card.Location == CardLocation.Hand || base.Card.Location == CardLocation.Grave)
			{
				return true;
			}
			foreach (ClientCard target in base.Bot.GetMonsters())
			{
				if (target == this.WidowAnchorTarget && base.Duel.Phase == DuelPhase.Main2)
				{
					base.AI.SelectCard(target);
					return true;
				}
			}
			foreach (ClientCard target2 in base.Bot.GetMonsters())
			{
				if (target2.IsCode(26077387) && base.Bot.GetMonstersExtraZoneCount() == 0)
				{
					base.AI.SelectCard(target2);
					return true;
				}
			}
			foreach (ClientCard target3 in base.Bot.GetSpells())
			{
				if (!target3.IsCode(new int[] { 50005218, 24010609, 98338152 }) && target3.IsSpell())
				{
					base.AI.SelectCard(target3);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001E14 RID: 7700 RVA: 0x000B69D8 File Offset: 0x000B4BD8
		private bool MultiroleEffect()
		{
			if (base.Card.Location == CardLocation.SpellZone)
			{
				foreach (ClientCard target in base.Bot.GetMonsters())
				{
					if (target == this.WidowAnchorTarget && base.Duel.Phase == DuelPhase.Main2)
					{
						base.AI.SelectCard(target);
						return true;
					}
				}
				foreach (ClientCard target2 in base.Bot.GetMonsters())
				{
					if (target2.IsCode(26077387) && base.Bot.GetMonstersExtraZoneCount() == 0)
					{
						base.AI.SelectCard(target2);
						return true;
					}
				}
				foreach (ClientCard target3 in base.Bot.GetSpells())
				{
					if (target3.IsCode(50005218))
					{
						base.AI.SelectCard(target3);
						return true;
					}
				}
				foreach (ClientCard target4 in base.Bot.GetSpells())
				{
					if (!target4.IsCode(new int[] { 24010609, 98338152 }) && target4.IsSpell())
					{
						base.AI.SelectCard(target4);
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001E15 RID: 7701 RVA: 0x000B6BB8 File Offset: 0x000B4DB8
		private bool RayeSummon()
		{
			return base.Bot.GetMonstersExtraZoneCount() == 0;
		}

		// Token: 0x06001E16 RID: 7702 RVA: 0x000B6BCC File Offset: 0x000B4DCC
		private bool RayeEffect()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				return true;
			}
			if (base.Card.IsDisabled())
			{
				return false;
			}
			if (base.Util.IsChainTarget(base.Card))
			{
				this.RayeSelectTarget();
				return true;
			}
			if (base.Card.Attacked && base.Duel.Phase == DuelPhase.BattleStart)
			{
				this.RayeSelectTarget();
				return true;
			}
			if (base.Card == base.Bot.BattlingMonster && base.Duel.Player == 1)
			{
				this.RayeSelectTarget();
				return true;
			}
			if (base.Duel.Phase == DuelPhase.Main2)
			{
				this.RayeSelectTarget();
				return true;
			}
			return false;
		}

		// Token: 0x06001E17 RID: 7703 RVA: 0x000B6C7C File Offset: 0x000B4E7C
		private void RayeSelectTarget()
		{
			if (!this.KagariSummoned && base.Bot.HasInGraveyard(new int[] { 63166095, 52340444, 98338152 }))
			{
				base.AI.SelectCard(63288573);
				return;
			}
			base.AI.SelectCard(new int[] { 90673288, 63288573, 8491308 });
		}

		// Token: 0x06001E18 RID: 7704 RVA: 0x000B6CDC File Offset: 0x000B4EDC
		private bool KagariSummon()
		{
			if (base.Bot.HasInGraveyard(new int[] { 63166095, 52340444, 98338152 }))
			{
				this.KagariSummoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001E19 RID: 7705 RVA: 0x000B6D08 File Offset: 0x000B4F08
		private bool KagariEffect()
		{
			if (this.EmptyMainMonsterZone() && base.Util.GetProblematicEnemyMonster(0, false) != null && base.Bot.HasInGraveyard(99550630))
			{
				base.AI.SelectCard(99550630);
			}
			else if (this.EmptyMainMonsterZone() && base.Util.GetProblematicEnemySpell() != null && base.Bot.HasInGraveyard(25955749))
			{
				base.AI.SelectCard(25955749);
			}
			else
			{
				base.AI.SelectCard(new int[] { 63166095, 52340444, 98338152 });
			}
			return true;
		}

		// Token: 0x06001E1A RID: 7706 RVA: 0x000B6DA6 File Offset: 0x000B4FA6
		private bool ShizukuSummon()
		{
			if (base.Util.IsTurn1OrMain2())
			{
				this.ShizukuSummoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001E1B RID: 7707 RVA: 0x000B6DC0 File Offset: 0x000B4FC0
		private bool ShizukuEffect()
		{
			int target = this.GetCardToSearch();
			if (target != 0)
			{
				base.AI.SelectCard(target);
			}
			else
			{
				base.AI.SelectCard(new int[] { 63166095, 52340444, 98338152 });
			}
			return true;
		}

		// Token: 0x06001E1C RID: 7708 RVA: 0x000B6E02 File Offset: 0x000B5002
		private bool HayateSummon()
		{
			if (base.Util.IsTurn1OrMain2())
			{
				return false;
			}
			this.HayateSummoned = true;
			return true;
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x000B6E1C File Offset: 0x000B501C
		private bool HayateEffect()
		{
			if (!base.Bot.HasInGraveyard(26077387))
			{
				base.AI.SelectCard(26077387);
			}
			else if (!base.Bot.HasInGraveyard(52340444))
			{
				base.AI.SelectCard(52340444);
			}
			else if (!base.Bot.HasInGraveyard(98338152))
			{
				base.AI.SelectCard(98338152);
			}
			return true;
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x000B6E94 File Offset: 0x000B5094
		private bool TunerSummon()
		{
			return !base.Bot.HasInMonstersZone(new int[] { 14558127, 97268402, 59438930, 9742784 }, false, false, false) && !base.Util.IsTurn1OrMain2() && base.Bot.GetMonsterCount() > 0 && base.Bot.HasInExtra(50588353);
		}

		// Token: 0x06001E1F RID: 7711 RVA: 0x000B6EEF File Offset: 0x000B50EF
		private bool CrystronNeedlefiberSummon()
		{
			return !base.Util.IsTurn1OrMain2();
		}

		// Token: 0x06001E20 RID: 7712 RVA: 0x0009EA7D File Offset: 0x0009CC7D
		private bool CrystronNeedlefiberEffect()
		{
			base.AI.SelectCard(9742784);
			return true;
		}

		// Token: 0x06001E21 RID: 7713 RVA: 0x000B6EFF File Offset: 0x000B50FF
		private bool SummonSorceressEffect()
		{
			return base.ActivateDescription != -1;
		}

		// Token: 0x06001E22 RID: 7714 RVA: 0x000B6F10 File Offset: 0x000B5110
		private bool JetSynchronEffect()
		{
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			if (base.Bot.HasInMonstersZone(26077387, false, false, false) || base.Bot.HasInMonstersZone(50588353, false, false, false))
			{
				base.AI.SelectCard(this.GetDiscardHand());
				base.AI.SelectPosition(CardPosition.FaceUpDefence);
				return true;
			}
			return false;
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x000B6F77 File Offset: 0x000B5177
		private bool HandFull()
		{
			return base.Bot.GetSpellCountWithoutField() < 4 && base.Bot.Hand.Count > 4;
		}

		// Token: 0x06001E24 RID: 7716 RVA: 0x000B6F9C File Offset: 0x000B519C
		private int GetDiscardHand()
		{
			if (base.Bot.HasInHand(73594093))
			{
				return 73594093;
			}
			if (base.Bot.HasInHand(26077387) && !base.Bot.HasInGraveyard(26077387))
			{
				return 26077387;
			}
			if (base.Bot.HasInHand(9742784))
			{
				return 9742784;
			}
			if (base.Bot.HasInHand(32807846))
			{
				return 32807846;
			}
			if (base.Bot.HasInHand(35726888))
			{
				return 35726888;
			}
			return 0;
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x000B7034 File Offset: 0x000B5234
		private int GetCardToSearch()
		{
			if (!base.Bot.HasInHand(52340444) && base.Bot.GetRemainingCount(52340444, 3) > 0)
			{
				return 52340444;
			}
			if (base.Util.GetProblematicEnemyMonster(0, false) != null && base.Bot.GetRemainingCount(98338152, 3) > 0)
			{
				return 98338152;
			}
			if (this.EmptyMainMonsterZone() && base.Util.GetProblematicEnemyMonster(0, false) != null && base.Bot.GetRemainingCount(99550630, 1) > 0)
			{
				return 99550630;
			}
			if (this.EmptyMainMonsterZone() && base.Util.GetProblematicEnemySpell() != null && base.Bot.GetRemainingCount(25955749, 1) > 0)
			{
				return 25955749;
			}
			if (!base.Bot.HasInHand(26077387) && !base.Bot.HasInMonstersZone(26077387, false, false, false) && base.Bot.GetRemainingCount(26077387, 3) > 0)
			{
				return 26077387;
			}
			if (!base.Bot.HasInHand(98338152) && !base.Bot.HasInSpellZone(98338152, false, false) && base.Bot.GetRemainingCount(98338152, 3) > 0)
			{
				return 98338152;
			}
			return 0;
		}

		// Token: 0x06001E26 RID: 7718 RVA: 0x000B7178 File Offset: 0x000B5378
		private bool EmptyMainMonsterZone()
		{
			for (int i = 0; i < 5; i++)
			{
				if (base.Bot.MonsterZone[i] != null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x000B71A4 File Offset: 0x000B53A4
		private bool HaveThreeSpellsInGrave()
		{
			int count = 0;
			using (IEnumerator<ClientCard> enumerator = base.Bot.Graveyard.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsSpell())
					{
						count++;
					}
				}
			}
			return count >= 3;
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x000B7204 File Offset: 0x000B5404
		private bool DefaultNoExecutor()
		{
			foreach (CardExecutor exec in base.Executors)
			{
				if (exec.Type == base.Type && exec.CardId == base.Card.Id)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040020F7 RID: 8439
		private bool KagariSummoned;

		// Token: 0x040020F8 RID: 8440
		private bool ShizukuSummoned;

		// Token: 0x040020F9 RID: 8441
		private bool HayateSummoned;

		// Token: 0x040020FA RID: 8442
		private ClientCard WidowAnchorTarget;

		// Token: 0x020003D6 RID: 982
		public class CardId
		{
			// Token: 0x040020FB RID: 8443
			public const int Raye = 26077387;

			// Token: 0x040020FC RID: 8444
			public const int Kagari = 63288573;

			// Token: 0x040020FD RID: 8445
			public const int Shizuku = 90673288;

			// Token: 0x040020FE RID: 8446
			public const int Hayate = 8491308;

			// Token: 0x040020FF RID: 8447
			public const int Token = 52340445;

			// Token: 0x04002100 RID: 8448
			public const int Engage = 63166095;

			// Token: 0x04002101 RID: 8449
			public const int HornetDrones = 52340444;

			// Token: 0x04002102 RID: 8450
			public const int WidowAnchor = 98338152;

			// Token: 0x04002103 RID: 8451
			public const int Afterburners = 99550630;

			// Token: 0x04002104 RID: 8452
			public const int JammingWave = 25955749;

			// Token: 0x04002105 RID: 8453
			public const int Multirole = 24010609;

			// Token: 0x04002106 RID: 8454
			public const int HerculesBase = 97616504;

			// Token: 0x04002107 RID: 8455
			public const int AreaZero = 50005218;

			// Token: 0x04002108 RID: 8456
			public const int AshBlossom = 14558127;

			// Token: 0x04002109 RID: 8457
			public const int GhostRabbit = 59438930;

			// Token: 0x0400210A RID: 8458
			public const int MaxxC = 23434538;

			// Token: 0x0400210B RID: 8459
			public const int JetSynchron = 9742784;

			// Token: 0x0400210C RID: 8460
			public const int EffectVeiler = 97268402;

			// Token: 0x0400210D RID: 8461
			public const int ReinforcementOfTheArmy = 32807846;

			// Token: 0x0400210E RID: 8462
			public const int FoolishBurialGoods = 35726888;

			// Token: 0x0400210F RID: 8463
			public const int UpstartGoblin = 70368879;

			// Token: 0x04002110 RID: 8464
			public const int MetalfoesFusion = 73594093;

			// Token: 0x04002111 RID: 8465
			public const int TwinTwisters = 43898403;

			// Token: 0x04002112 RID: 8466
			public const int SolemnJudgment = 41420027;

			// Token: 0x04002113 RID: 8467
			public const int SolemnWarning = 84749824;

			// Token: 0x04002114 RID: 8468
			public const int HiSpeedroidChanbara = 42110604;

			// Token: 0x04002115 RID: 8469
			public const int TopologicBomberDragon = 5821478;

			// Token: 0x04002116 RID: 8470
			public const int TopologicTrisbaena = 72529749;

			// Token: 0x04002117 RID: 8471
			public const int SummonSorceress = 61665245;

			// Token: 0x04002118 RID: 8472
			public const int TroymareUnicorn = 38342335;

			// Token: 0x04002119 RID: 8473
			public const int TroymarePhoenix = 2857636;

			// Token: 0x0400211A RID: 8474
			public const int CrystronNeedlefiber = 50588353;

			// Token: 0x0400211B RID: 8475
			public const int Linkuriboh = 41999284;
		}
	}
}
