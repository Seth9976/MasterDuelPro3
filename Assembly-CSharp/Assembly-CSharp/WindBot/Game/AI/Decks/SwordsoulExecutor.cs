using System;
using System.Collections.Generic;
using System.Linq;
using YGOSharp.OCGWrapper;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x020003DC RID: 988
	[Deck("Swordsoul", "AI_Swordsoul", "Normal")]
	public class SwordsoulExecutor : DefaultExecutor
	{
		// Token: 0x06001E92 RID: 7826 RVA: 0x000BA8AC File Offset: 0x000B8AAC
		public SwordsoulExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 83755611, new Func<bool>(this.BaxiaBrightnessOfTheYangZingActivate));
			base.AddExecutor(ExecutorType.Activate, 43202238, new Func<bool>(this.YaziEvilOfTheYangZingActivate));
			base.AddExecutor(ExecutorType.Activate, 55273560, new Func<bool>(this.IncredibleEcclesiaTheVirtuousActivate));
			base.AddExecutor(ExecutorType.Activate, 24224830, new Func<bool>(this.CalledbytheGraveActivate));
			base.AddExecutor(ExecutorType.Activate, 65681983, new Func<bool>(this.CrossoutDesignatorActivate));
			base.AddExecutor(ExecutorType.Activate, 14558127, new Func<bool>(this.AshBlossomActivate));
			base.AddExecutor(ExecutorType.Activate, 69248256, new Func<bool>(this.SwordsoulGrandmaster_ChixiaoActivate));
			base.AddExecutor(ExecutorType.Activate, 97268402, new Func<bool>(this.EffectVeilerActivate));
			base.AddExecutor(ExecutorType.Activate, 10045474, new Func<bool>(this.InfiniteImpermanenceActivate));
			base.AddExecutor(ExecutorType.Activate, 47710198, new Func<bool>(this.SwordsoulSinisterSovereign_QixingLongyuanActivate));
			base.AddExecutor(ExecutorType.Activate, 5041348, new Func<bool>(this.DracoBerserkerOfTheTenyiActivate));
			base.AddExecutor(ExecutorType.Activate, 9464441, new Func<bool>(this.AdamancipatorRisen_DragiteActivate));
			base.AddExecutor(ExecutorType.Activate, 84815190, new Func<bool>(this.BaronneDeFleurActivate));
			base.AddExecutor(ExecutorType.Activate, 96633955, new Func<bool>(this.SwordsoulSupremeSovereign_ChengyingActivate));
			base.AddExecutor(ExecutorType.Activate, 23434538, new Func<bool>(this.MaxxCActivate));
			base.AddExecutor(ExecutorType.Activate, 27204311, new Func<bool>(this.NibiruThePrimalBeingActivate));
			base.AddExecutor(ExecutorType.Activate, 56465981, new Func<bool>(this.SwordsoulEmergenceActivate));
			base.AddExecutor(ExecutorType.Activate, 14821890, new Func<bool>(this.SwordsoulBlackoutActivate));
			base.AddExecutor(ExecutorType.SpSummon, 43202238, new Func<bool>(this.YaziEvilOfTheYangZingSpSummon));
			base.AddExecutor(ExecutorType.SpSummon, 83755611, new Func<bool>(this.BaxiaBrightnessOfTheYangZingSpSummon));
			base.AddExecutor(ExecutorType.SpSummon, 69248256, new Func<bool>(this.SwordsoulGrandmaster_ChixiaoSpSummon));
			base.AddExecutor(ExecutorType.SpSummon, 9464441, new Func<bool>(this.AdamancipatorRisen_DragiteSpSummon));
			base.AddExecutor(ExecutorType.SpSummon, 5041348, new Func<bool>(this.DracoBerserkerOfTheTenyiSpSummon));
			base.AddExecutor(ExecutorType.SpSummon, new Func<bool>(this.Level10SpSummonCheckInit));
			base.AddExecutor(ExecutorType.SpSummon, new Func<bool>(this.Level10SpSummonCheckCount));
			base.AddExecutor(ExecutorType.SpSummon, new Func<bool>(this.Level10SpSummonCheckDecide));
			base.AddExecutor(ExecutorType.SpSummon, new Func<bool>(this.Level10SpSummonCheckFinal));
			base.AddExecutor(ExecutorType.Activate, 20001443, new Func<bool>(this.SwordsoulOfMoYeActivate));
			base.AddExecutor(ExecutorType.Activate, 56495147, new Func<bool>(this.SwordsoulOfTaiaActivate));
			base.AddExecutor(ExecutorType.Activate, new Func<bool>(this.TenyiForShamanSpSummon));
			base.AddExecutor(ExecutorType.SpSummon, 55273560, new Func<bool>(this.IncredibleEcclesiaTheVirtuousSpSummon));
			base.AddExecutor(ExecutorType.Summon, 20001443, new Func<bool>(this.SwordsoulOfMoYeSummon));
			base.AddExecutor(ExecutorType.Summon, 55273560, new Func<bool>(this.IncredibleEcclesiaTheVirtuousSummon));
			base.AddExecutor(ExecutorType.Summon, 56495147, new Func<bool>(this.SwordsoulOfTaiaSummon));
			base.AddExecutor(ExecutorType.Activate, 93490856, new Func<bool>(this.SwordsoulStrategistLongyuanActivate));
			base.AddExecutor(ExecutorType.Activate, 35261759, new Func<bool>(this.PotOfDesiresActivate));
			base.AddExecutor(ExecutorType.Activate, 78917791, new Func<bool>(this.ShamanOfTheTenyiActivate));
			base.AddExecutor(ExecutorType.Activate, 93850690, new Func<bool>(this.SwordsoulSacredSummitActivate));
			base.AddExecutor(ExecutorType.Activate, 23431858, new Func<bool>(this.TenyiSpirit_VishudaActivate));
			base.AddExecutor(ExecutorType.Activate, 87052196, new Func<bool>(this.TenyiSpirit_AshunaActivate));
			base.AddExecutor(ExecutorType.Activate, 98159737, new Func<bool>(this.TenyiSpirit_AdharaActivate));
			base.AddExecutor(ExecutorType.Activate, new Func<bool>(this.TenyiForBlackoutSpSummon));
			base.AddExecutor(ExecutorType.SpSummon, 42632209, new Func<bool>(this.GeomathmechFinalSigmaSpSummon));
			base.AddExecutor(ExecutorType.SpSummon, 60465049, new Func<bool>(this.PsychicEndPunisherSpSummon));
			base.AddExecutor(ExecutorType.Summon, new Func<bool>(this.TunerForSynchroSummon));
			base.AddExecutor(ExecutorType.Summon, new Func<bool>(this.WyrmForBlackoutSummon));
			base.AddExecutor(ExecutorType.SpSummon, 78917791, new Func<bool>(this.ShamanOfTheTenyiSpSummon));
			base.AddExecutor(ExecutorType.SpSummon, 32519092, new Func<bool>(this.MonkOfTheTenyiSpSummon));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(base.DefaultMonsterRepos));
			base.AddExecutor(ExecutorType.Activate, 60465049, new Func<bool>(this.PsychicEndPunisherActivate));
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(this.SpellSetCheck));
		}

		// Token: 0x06001E93 RID: 7827 RVA: 0x000BAEE8 File Offset: 0x000B90E8
		public List<ClientCard> ShuffleCardList(List<ClientCard> list)
		{
			int i = list.Count;
			while (i-- > 1)
			{
				int index = Program.Rand.Next(list.Count);
				int nextIndex = (index + Program.Rand.Next(list.Count - 1)) % list.Count;
				ClientCard tempCard = list[index];
				list[index] = list[nextIndex];
				list[nextIndex] = tempCard;
			}
			return list;
		}

		// Token: 0x06001E94 RID: 7828 RVA: 0x000BAF58 File Offset: 0x000B9158
		public ClientCard GetProblematicEnemyMonster(int attack = 0, bool canBeTarget = false)
		{
			List<ClientCard> floodagateList = (from c in base.Enemy.GetMonsters()
				where ((c != null) ? c.Data : null) != null && c.IsFloodgate() && c.IsFaceup() && (!canBeTarget || !c.IsShouldNotBeTarget())
				select c).ToList<ClientCard>();
			if (floodagateList.Count<ClientCard>() > 0)
			{
				floodagateList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				floodagateList.Reverse();
				return floodagateList[0];
			}
			List<ClientCard> dangerList = base.Enemy.MonsterZone.Where((ClientCard c) => ((c != null) ? c.Data : null) != null && c.IsMonsterDangerous() && c.IsFaceup() && (!canBeTarget || !c.IsShouldNotBeTarget())).ToList<ClientCard>();
			if (dangerList.Count<ClientCard>() > 0)
			{
				dangerList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				dangerList.Reverse();
				return dangerList[0];
			}
			List<ClientCard> invincibleList = base.Enemy.MonsterZone.Where((ClientCard c) => ((c != null) ? c.Data : null) != null && c.IsMonsterInvincible() && c.IsFaceup() && (!canBeTarget || !c.IsShouldNotBeTarget())).ToList<ClientCard>();
			if (invincibleList.Count<ClientCard>() > 0)
			{
				invincibleList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				invincibleList.Reverse();
				return invincibleList[0];
			}
			if (attack == 0)
			{
				attack = base.Util.GetBestAttack(base.Bot);
			}
			List<ClientCard> betterList = (from card in base.Enemy.MonsterZone.GetMonsters()
				where card.GetDefensePower() >= attack && card.IsAttack() && (!canBeTarget || !card.IsShouldNotBeTarget())
				select card).ToList<ClientCard>();
			if (betterList.Count<ClientCard>() > 0)
			{
				betterList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				betterList.Reverse();
				return betterList[0];
			}
			return null;
		}

		// Token: 0x06001E95 RID: 7829 RVA: 0x000BB0D0 File Offset: 0x000B92D0
		public List<ClientCard> GetProblematicEnemyCardList(bool canBeTarget = false, bool ignoreNormalSpell = false)
		{
			List<ClientCard> resultList = new List<ClientCard>();
			List<ClientCard> floodagateList = base.Enemy.MonsterZone.Where((ClientCard c) => ((c != null) ? c.Data : null) != null && c.IsFloodgate() && c.IsFaceup() && (!canBeTarget || !c.IsShouldNotBeTarget())).ToList<ClientCard>();
			if (floodagateList.Count<ClientCard>() > 0)
			{
				floodagateList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				floodagateList.Reverse();
				resultList.AddRange(floodagateList);
			}
			List<ClientCard> problemEnemySpellList = base.Enemy.SpellZone.Where((ClientCard c) => ((c != null) ? c.Data : null) != null && !resultList.Contains(c) && c.IsFloodgate() && c.IsFaceup() && (!canBeTarget || !c.IsShouldNotBeTarget())).ToList<ClientCard>();
			if (problemEnemySpellList.Count<ClientCard>() > 0)
			{
				resultList.AddRange(this.ShuffleCardList(problemEnemySpellList));
			}
			List<ClientCard> dangerList = base.Enemy.MonsterZone.Where((ClientCard c) => ((c != null) ? c.Data : null) != null && !resultList.Contains(c) && c.IsMonsterDangerous() && c.IsFaceup() && (!canBeTarget || !c.IsShouldNotBeTarget())).ToList<ClientCard>();
			if (dangerList.Count<ClientCard>() > 0 && (base.Duel.Player == 0 || (base.Duel.Phase > DuelPhase.Main1 && base.Duel.Phase < DuelPhase.Main2)))
			{
				dangerList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				dangerList.Reverse();
				resultList.AddRange(dangerList);
			}
			List<ClientCard> invincibleList = base.Enemy.MonsterZone.Where((ClientCard c) => ((c != null) ? c.Data : null) != null && !resultList.Contains(c) && c.IsMonsterInvincible() && c.IsFaceup() && (!canBeTarget || !c.IsShouldNotBeTarget())).ToList<ClientCard>();
			if (invincibleList.Count<ClientCard>() > 0)
			{
				invincibleList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				invincibleList.Reverse();
				resultList.AddRange(invincibleList);
			}
			List<ClientCard> enemyMonsters = base.Enemy.GetMonsters().ToList<ClientCard>();
			if (enemyMonsters.Count<ClientCard>() > 0)
			{
				enemyMonsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				enemyMonsters.Reverse();
				foreach (ClientCard target in enemyMonsters)
				{
					if ((target.HasType((CardType)8396992) || (target.HasType(CardType.Link) && target.LinkCount >= 2)) && (!canBeTarget || (!target.IsShouldNotBeTarget() && !target.IsShouldNotBeMonsterTarget())) && !resultList.Contains(target))
					{
						resultList.Add(target);
					}
				}
			}
			List<ClientCard> spells = (from c in base.Enemy.GetSpells()
				where c.IsFaceup() && c.HasType((CardType)17694720)
				select c).ToList<ClientCard>();
			if (spells.Count<ClientCard>() > 0 && !ignoreNormalSpell)
			{
				resultList.AddRange(this.ShuffleCardList(spells));
			}
			return resultList;
		}

		// Token: 0x06001E96 RID: 7830 RVA: 0x000BB380 File Offset: 0x000B9580
		public ClientCard GetBestEnemyMonster(bool onlyFaceup = false, bool canBeTarget = false)
		{
			ClientCard card = this.GetProblematicEnemyMonster(0, canBeTarget);
			if (card != null)
			{
				return card;
			}
			card = base.Enemy.MonsterZone.GetHighestAttackMonster(canBeTarget);
			if (card != null)
			{
				return card;
			}
			List<ClientCard> monsters = base.Enemy.GetMonsters();
			if (monsters.Count<ClientCard>() > 0 && !onlyFaceup)
			{
				return this.ShuffleCardList(monsters)[0];
			}
			return null;
		}

		// Token: 0x06001E97 RID: 7831 RVA: 0x000BB3DC File Offset: 0x000B95DC
		public ClientCard GetBestEnemySpell(bool onlyFaceup = false, bool canBeTarget = false)
		{
			List<ClientCard> problemEnemySpellList = base.Enemy.SpellZone.Where((ClientCard c) => ((c != null) ? c.Data : null) != null && c.IsFloodgate() && c.IsFaceup() && (!canBeTarget || !c.IsShouldNotBeTarget())).ToList<ClientCard>();
			if (problemEnemySpellList.Count<ClientCard>() > 0)
			{
				return this.ShuffleCardList(problemEnemySpellList)[0];
			}
			List<ClientCard> spells = (from card in base.Enemy.GetSpells()
				where !card.IsFaceup() || !card.IsCode(15693423)
				select card).ToList<ClientCard>();
			List<ClientCard> faceUpList = spells.Where((ClientCard ecard) => ecard.IsFaceup() && ecard.HasType((CardType)17694720)).ToList<ClientCard>();
			if (faceUpList.Count<ClientCard>() > 0)
			{
				return this.ShuffleCardList(faceUpList)[0];
			}
			if (spells.Count<ClientCard>() > 0 && !onlyFaceup)
			{
				return this.ShuffleCardList(spells)[0];
			}
			return null;
		}

		// Token: 0x06001E98 RID: 7832 RVA: 0x000BB4C4 File Offset: 0x000B96C4
		public ClientCard GetBestEnemyCard(bool onlyFaceup = false, bool canBeTarget = false, bool checkGrave = false)
		{
			ClientCard card = this.GetBestEnemyMonster(onlyFaceup, canBeTarget);
			if (card != null)
			{
				return card;
			}
			card = this.GetBestEnemySpell(onlyFaceup, canBeTarget);
			if (card != null)
			{
				return card;
			}
			if (!checkGrave || base.Enemy.Graveyard.Count<ClientCard>() <= 0)
			{
				return null;
			}
			List<ClientCard> graveMonsterList = base.Enemy.Graveyard.GetMatchingCards((ClientCard c) => c.IsMonster()).ToList<ClientCard>();
			if (graveMonsterList.Count<ClientCard>() > 0)
			{
				graveMonsterList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				graveMonsterList.Reverse();
				return graveMonsterList[0];
			}
			return this.ShuffleCardList(base.Enemy.Graveyard.ToList<ClientCard>())[0];
		}

		// Token: 0x06001E99 RID: 7833 RVA: 0x000BB584 File Offset: 0x000B9784
		public List<ClientCard> GetNormalEnemyTargetList(bool canBeTarget = true)
		{
			List<ClientCard> targetList = this.GetProblematicEnemyCardList(canBeTarget, false);
			List<ClientCard> enemyMonster = (from card in base.Enemy.GetMonsters()
				where card.IsFaceup() && !targetList.Contains(card)
				select card).ToList<ClientCard>();
			enemyMonster.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			enemyMonster.Reverse();
			targetList.AddRange(enemyMonster);
			targetList.AddRange(this.ShuffleCardList(base.Enemy.GetSpells()));
			targetList.AddRange(this.ShuffleCardList((from card in base.Enemy.GetMonsters()
				where card.IsFacedown()
				select card).ToList<ClientCard>()));
			return targetList;
		}

		// Token: 0x06001E9A RID: 7834 RVA: 0x000BB654 File Offset: 0x000B9854
		public List<ClientCard> GetMonsterListForTargetNegate(bool canBeMonsterTarget = false, bool canBeTrapTarget = false)
		{
			List<ClientCard> resultList = new List<ClientCard>();
			if (this.CheckWhetherNegated())
			{
				return resultList;
			}
			ClientCard target = base.Enemy.MonsterZone.FirstOrDefault((ClientCard card) => ((card != null) ? card.Data : null) != null && card.IsMonsterShouldBeDisabledBeforeItUseEffect() && card.IsFaceup() && !card.IsShouldNotBeTarget() && (!canBeMonsterTarget || !card.IsShouldNotBeMonsterTarget()) && (!canBeTrapTarget || !card.IsShouldNotBeSpellTrapTarget()) && !this.currentNegateMonsterList.Contains(card));
			if (target != null)
			{
				resultList.Add(target);
			}
			foreach (ClientCard chainingCard in base.Duel.CurrentChain)
			{
				if (chainingCard.Location == CardLocation.MonsterZone && chainingCard.Controller == 1 && !chainingCard.IsDisabled() && (!canBeMonsterTarget || !chainingCard.IsShouldNotBeMonsterTarget()) && (!canBeTrapTarget || !chainingCard.IsShouldNotBeSpellTrapTarget()) && !chainingCard.IsShouldNotBeTarget() && !this.currentNegateMonsterList.Contains(chainingCard))
				{
					resultList.Add(chainingCard);
				}
			}
			return resultList;
		}

		// Token: 0x06001E9B RID: 7835 RVA: 0x000BB754 File Offset: 0x000B9954
		public void CheckDeactiveFlag()
		{
			ClientCard lastChainCard = base.Util.GetLastChainCard();
			if (lastChainCard != null && base.Duel.LastChainPlayer == 1 && lastChainCard.Controller == 1 && lastChainCard.Location == CardLocation.MonsterZone)
			{
				this.currentNegateMonsterList.Add(lastChainCard);
			}
		}

		// Token: 0x06001E9C RID: 7836 RVA: 0x000BB79C File Offset: 0x000B999C
		public int CheckCalledbytheGrave(int id)
		{
			if (this.currentNegatingIdList.Contains(id))
			{
				return 1;
			}
			if (base.DefaultCheckWhetherCardIdIsNegated(id))
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06001E9D RID: 7837 RVA: 0x000BB7BC File Offset: 0x000B99BC
		public int CheckRemainInDeck(int id)
		{
			for (int count = 1; count < 4; count++)
			{
				if (this.DeckCountTable[count].Contains(id))
				{
					return base.Bot.GetRemainingCount(id, count);
				}
			}
			return 0;
		}

		// Token: 0x06001E9E RID: 7838 RVA: 0x000BB7F8 File Offset: 0x000B99F8
		public bool CheckSpellWillBeNegate(bool isCounter = false, ClientCard target = null)
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
			if (target.IsTrap() && (base.Enemy.HasInSpellZone(51452091, true, false) || base.Bot.HasInSpellZone(51452091, true, false)))
			{
				return true;
			}
			if (target.Location == CardLocation.SpellZone && (target.IsSpell() || target.IsTrap()))
			{
				int selfSeq = -1;
				for (int i = 0; i < 5; i++)
				{
					if (base.Bot.SpellZone[i] == base.Card)
					{
						selfSeq = i;
					}
				}
				if (this.infiniteImpermanenceList.Contains(selfSeq))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x000BB934 File Offset: 0x000B9B34
		public bool CheckWhetherNegated()
		{
			return ((base.Card.IsSpell() || base.Card.IsTrap()) && this.CheckSpellWillBeNegate(false, null)) || base.DefaultCheckWhetherCardIsNegated(base.Card) || (base.Card.IsMonster() && base.Card.Location == CardLocation.MonsterZone && base.Card.IsDefense() && (base.Enemy.MonsterZone.Any((ClientCard card) => this.CheckNumber41(card)) || base.Bot.MonsterZone.Any((ClientCard card) => this.CheckNumber41(card))));
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x00037BCF File Offset: 0x00035DCF
		public bool CheckNumber41(ClientCard card)
		{
			return card != null && card.IsFaceup() && card.IsCode(90590303) && card.IsDefense() && !card.IsDisabled();
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x000BB9E0 File Offset: 0x000B9BE0
		public bool CheckAtAdvantage()
		{
			if (this.GetProblematicEnemyMonster(0, false) == null)
			{
				if (base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup()) || (base.Duel.Player == 0 && base.Duel.Turn == 1))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x000BBA48 File Offset: 0x000B9C48
		public bool CheckLastChainShouldNegated()
		{
			ClientCard lastcard = base.Util.GetLastChainCard();
			if (lastcard == null || lastcard.Controller != 1)
			{
				return false;
			}
			if (lastcard.IsMonster() && lastcard.HasSetcode(74) && base.Duel.Phase == DuelPhase.Standby)
			{
				return false;
			}
			if (this.notToNegateIdList.Contains(lastcard.Id))
			{
				return false;
			}
			if (base.DefaultCheckWhetherCardIsNegated(lastcard))
			{
				return false;
			}
			if (base.Duel.CurrentChain.Count >= 2)
			{
				ClientCard lastlastChainCard = base.Duel.CurrentChain[base.Duel.CurrentChain.Count - 2];
				ClientCard lastChainCard = base.Duel.CurrentChain[base.Duel.CurrentChain.Count - 1];
				if (lastlastChainCard != null && lastlastChainCard.Controller == 0 && lastChainCard != null && lastChainCard.Controller == 1 && lastChainCard.IsCode(this.normalCounterList) && ((lastlastChainCard.Location == CardLocation.Grave && (lastlastChainCard.IsCode(56495147) || lastlastChainCard.IsCode(20001443) || lastlastChainCard.IsCode(93490856))) | (lastlastChainCard.IsCode(60465049) && base.Bot.LifePoints < base.Enemy.LifePoints)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x000BBB98 File Offset: 0x000B9D98
		public List<ClientCard> CheckDangerousCardinEnemyGrave(bool onlyMonster = false)
		{
			List<ClientCard> list = base.Enemy.Graveyard.GetMatchingCards((ClientCard card) => (!onlyMonster || card.IsMonster()) && (card.HasSetcode(283) || card.HasSetcode(219))).ToList<ClientCard>();
			List<int> list2 = new List<int>();
			list2.Add(99937011);
			list2.Add(63542003);
			list2.Add(98159737);
			list2.Add(87052196);
			list2.Add(23431858);
			list2.Add(9411399);
			list2.Add(28954097);
			list2.Add(30680659);
			return list;
		}

		// Token: 0x06001EA4 RID: 7844 RVA: 0x0000763C File Offset: 0x0000583C
		public override bool OnSelectHand()
		{
			return true;
		}

		// Token: 0x06001EA5 RID: 7845 RVA: 0x000BBC31 File Offset: 0x000B9E31
		public override int OnSelectPlace(int cardId, int player, CardLocation location, int available)
		{
			if (cardId == 42632209 && location == CardLocation.MonsterZone)
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

		// Token: 0x06001EA6 RID: 7846 RVA: 0x000BBC60 File Offset: 0x000B9E60
		public override CardPosition OnSelectPosition(int cardId, IList<CardPosition> positions)
		{
			NamedCard cardData = NamedCard.Get(cardId);
			if (cardData != null)
			{
				if (cardData.Id == 60465049)
				{
					return CardPosition.FaceUpAttack;
				}
				if (base.Util.IsTurn1OrMain2())
				{
					bool turnDefense = false;
					if (!cardData.HasType(CardType.Synchro) || cardData.Attack <= cardData.Defense)
					{
						turnDefense = true;
					}
					if (turnDefense)
					{
						return CardPosition.FaceUpDefence;
					}
				}
				if (base.Duel.Player == 1)
				{
					if (!cardData.HasType(CardType.Synchro) || cardData.Defense >= cardData.Attack || base.Util.IsOneEnemyBetterThanValue(cardData.Attack, true))
					{
						return CardPosition.FaceUpDefence;
					}
				}
				else if (cardData.HasType(CardType.Synchro))
				{
					return CardPosition.FaceUpAttack;
				}
				int bestBotAttack = Math.Max(base.Util.GetBestAttack(base.Bot), cardData.Attack);
				if (base.Util.IsAllEnemyBetterThanValue(bestBotAttack, true))
				{
					return CardPosition.FaceUpDefence;
				}
			}
			return base.OnSelectPosition(cardId, positions);
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x000BBD40 File Offset: 0x000B9F40
		public override IList<ClientCard> OnSelectCard(IList<ClientCard> cards, int min, int max, int hint, bool cancelable)
		{
			if (base.Util.ChainContainPlayer(1) && hint == 503 && base.Duel.Phase > DuelPhase.Main1 && base.Duel.Phase < DuelPhase.Main2)
			{
				int num = base.Bot.GetMonsterCount() + base.Bot.GetSpellCount();
				int oppositeCount = base.Enemy.GetMonsterCount() + base.Enemy.GetSpellCount();
				if (num - oppositeCount == min && min == max)
				{
					Logger.DebugWriteLine("=== Evenly Matched activated.");
					List<ClientCard> banishList = new List<ClientCard>();
					List<ClientCard> list = (from card in base.Bot.GetMonsters()
						where !card.HasType(CardType.Token)
						select card).ToList<ClientCard>();
					List<ClientCard> faceDownMonsters = list.Where((ClientCard card) => card.IsFacedown()).ToList<ClientCard>();
					banishList.AddRange(faceDownMonsters);
					List<ClientCard> nonSynchroMonsters = list.Where((ClientCard card) => !card.HasType(CardType.Synchro) && !banishList.Contains(card)).ToList<ClientCard>();
					nonSynchroMonsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					banishList.AddRange(nonSynchroMonsters);
					List<ClientCard> spells = base.Bot.GetSpells();
					banishList.AddRange(this.ShuffleCardList(spells));
					List<ClientCard> synchroMonsters = list.Where((ClientCard card) => card.HasType(CardType.Synchro) && !banishList.Contains(card)).ToList<ClientCard>();
					synchroMonsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					banishList.AddRange(synchroMonsters);
					return base.Util.CheckSelectCount(banishList, cards, min, max);
				}
			}
			return base.OnSelectCard(cards, min, max, hint, cancelable);
		}

		// Token: 0x06001EA8 RID: 7848 RVA: 0x000BBF04 File Offset: 0x000BA104
		public override void OnNewTurn()
		{
			this.enemyActivateMaxxC = false;
			this.enemyActivateLockBird = false;
			this.infiniteImpermanenceList.Clear();
			this.summoned = false;
			this.onlyWyrmSpSummon = false;
			this.enemyActivateInfiniteImpermanenceFromHand = false;
			this.activatedCardIdList.Clear();
			this.currentNegateMonsterList.Clear();
			this.currentNegatingIdList.Clear();
			base.OnNewTurn();
		}

		// Token: 0x06001EA9 RID: 7849 RVA: 0x000BBF68 File Offset: 0x000BA168
		public override void OnChainSolved(int chainIndex)
		{
			ChainInfo currentCard = base.Duel.GetCurrentSolvingChainInfo();
			if (currentCard != null && !base.Duel.IsCurrentSolvingChainNegated() && currentCard.ActivatePlayer == 1)
			{
				if (currentCard.IsCode(23434538))
				{
					this.enemyActivateMaxxC = true;
				}
				if (currentCard.IsCode(94145021))
				{
					this.enemyActivateLockBird = true;
				}
				if (currentCard.IsCode(10045474) && !this.enemyActivateInfiniteImpermanenceFromHand)
				{
					for (int i = 0; i < 5; i++)
					{
						if (base.Enemy.SpellZone[i] == currentCard.RelatedCard)
						{
							this.infiniteImpermanenceList.Add(4 - i);
							return;
						}
					}
				}
			}
		}

		// Token: 0x06001EAA RID: 7850 RVA: 0x000BC00C File Offset: 0x000BA20C
		public override void OnChainEnd()
		{
			this.currentNegateMonsterList.Clear();
			this.currentNegatingIdList.Clear();
			for (int idx = this.effectUsedBaronneDeFleurList.Count<ClientCard>() - 1; idx >= 0; idx--)
			{
				ClientCard checkTarget = this.effectUsedBaronneDeFleurList[idx];
				if (checkTarget == null || checkTarget.IsFacedown() || checkTarget.Location != CardLocation.MonsterZone)
				{
					this.effectUsedBaronneDeFleurList.RemoveAt(idx);
				}
			}
			this.enemyActivateInfiniteImpermanenceFromHand = false;
			base.OnChainEnd();
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x000BC081 File Offset: 0x000BA281
		public override void OnMove(ClientCard card, int previousControler, int previousLocation, int currentControler, int currentLocation)
		{
			if (previousControler == 1 && card != null && card.IsCode(10045474) && previousLocation == 2 && currentLocation == 8)
			{
				this.enemyActivateInfiniteImpermanenceFromHand = true;
			}
			base.OnMove(card, previousControler, previousLocation, currentControler, currentLocation);
		}

		// Token: 0x06001EAC RID: 7852 RVA: 0x000BC0B4 File Offset: 0x000BA2B4
		public void SelectSTPlace(ClientCard card = null, bool avoidImpermanence = false, List<int> avoidList = null)
		{
			if (card == null)
			{
				card = base.Card;
			}
			List<int> list = new List<int>();
			for (int seq = 0; seq < 5; seq++)
			{
				if (base.Bot.SpellZone[seq] == null && (card == null || card.Location != CardLocation.Hand || !avoidImpermanence || !this.infiniteImpermanenceList.Contains(seq)) && (avoidList == null || !avoidList.Contains(seq)))
				{
					list.Add(seq);
				}
			}
			int i = list.Count;
			while (i-- > 1)
			{
				int index = Program.Rand.Next(list.Count);
				int nextIndex = (index + Program.Rand.Next(list.Count - 1)) % list.Count;
				int tempInt = list[index];
				list[index] = list[nextIndex];
				list[nextIndex] = tempInt;
			}
			if (avoidImpermanence)
			{
				if (base.Bot.GetMonsters().Any((ClientCard c) => c.IsFaceup() && !c.IsDisabled()))
				{
					foreach (int seq2 in list)
					{
						ClientCard enemySpell = base.Enemy.SpellZone[4 - seq2];
						if (enemySpell == null || !enemySpell.IsFacedown())
						{
							int zone = (int)Math.Pow(2.0, (double)seq2);
							base.AI.SelectPlace(zone);
							return;
						}
					}
				}
			}
			using (List<int>.Enumerator enumerator = list.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					int seq3 = enumerator.Current;
					int zone2 = (int)Math.Pow(2.0, (double)seq3);
					base.AI.SelectPlace(zone2);
					return;
				}
			}
			base.AI.SelectPlace(0);
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x000BC2A0 File Offset: 0x000BA4A0
		public bool NibiruThePrimalBeingActivate()
		{
			if (this.CheckWhetherNegated())
			{
				return false;
			}
			if (base.Duel.Player != 0)
			{
				if (!base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && card.HasType(CardType.Synchro)))
				{
					if (base.Util.GetBestAttack(base.Enemy) > base.Util.GetBestAttack(base.Bot))
					{
						if ((base.CurrentTiming & 4) != 0)
						{
							this.SelectNibiruPosition();
							return true;
						}
						List<ClientCard> list = (from card in base.Enemy.GetMonsters()
							where card.IsFaceup() && card.IsTuner() && !card.HasType((CardType)75497472)
							select card).ToList<ClientCard>();
						List<ClientCard> nonTunerList = (from card in base.Enemy.GetMonsters()
							where card.IsFaceup() && !card.IsTuner() && !card.HasType((CardType)75497472)
							select card).ToList<ClientCard>();
						foreach (ClientCard tuner in list)
						{
							foreach (ClientCard nonTuner in nonTunerList)
							{
								if (tuner.Level + nonTuner.Level == 10)
								{
									this.SelectNibiruPosition();
									return true;
								}
							}
						}
						return false;
					}
					return false;
				}
			}
			return false;
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x000BC42C File Offset: 0x000BA62C
		public void SelectNibiruPosition()
		{
			int totalAttack = (from card in base.Bot.GetMonsters()
				where card.IsFaceup()
				select card).Sum((ClientCard m) => new int?(m.Attack)).GetValueOrDefault();
			totalAttack += (from card in base.Enemy.GetMonsters()
				where card.IsFaceup()
				select card).Sum((ClientCard m) => new int?(m.Attack)).GetValueOrDefault();
			Logger.DebugWriteLine("Nibiru token attack: " + totalAttack.ToString());
			if (totalAttack >= 3000)
			{
				base.AI.SelectPosition(CardPosition.FaceUpDefence);
				base.AI.SelectPosition(CardPosition.FaceUpDefence);
				return;
			}
			base.AI.SelectPosition(CardPosition.FaceUpAttack);
			base.AI.SelectPosition(CardPosition.FaceUpAttack);
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x000BC544 File Offset: 0x000BA744
		public bool TenyiSpirit_AshunaActivate()
		{
			if (base.ActivateDescription == base.Util.GetStringId(87052196, 0))
			{
				if (this.TenyiSpSummonForTaiaCheck() || this.Level7TenyiSpSummonCheck())
				{
					return true;
				}
			}
			else if (base.ActivateDescription == base.Util.GetStringId(87052196, 1) && base.Card.Location == CardLocation.Grave && this.CheckCalledbytheGrave(base.Card.Id) == 0)
			{
				if (base.Bot.HasInHandOrInSpellZone(14821890))
				{
					if ((from card in base.Bot.GetMonsters()
						where card.IsFaceup() && !card.HasType(CardType.Synchro) && card.HasRace(CardRace.Wyrm)
						select card).ToList<ClientCard>().Count<ClientCard>() == 0)
					{
						base.AI.SelectCard(new int[] { 98159737, 23431858 });
						this.onlyWyrmSpSummon = true;
						this.activatedCardIdList.Add(base.Card.Id);
						return true;
					}
				}
				List<int> tunerIdList = new List<int> { 14558127, 97268402, 98159737 };
				if ((base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && card.IsCode(tunerIdList)) | (!this.summoned && base.Bot.HasInHand(tunerIdList))) && this.CheckRemainInDeck(23431858) > 0)
				{
					base.AI.SelectCard(87052196);
					this.onlyWyrmSpSummon = true;
					this.activatedCardIdList.Add(base.Card.Id);
					return true;
				}
				if (base.Bot.HasInMonstersZone(87052196, false, false, true) && this.CheckRemainInDeck(98159737) > 0)
				{
					base.AI.SelectCard(98159737);
					this.onlyWyrmSpSummon = true;
					this.activatedCardIdList.Add(base.Card.Id);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001EB0 RID: 7856 RVA: 0x000BC750 File Offset: 0x000BA950
		public bool TenyiSpirit_VishudaActivate()
		{
			if (base.ActivateDescription == base.Util.GetStringId(23431858, 0))
			{
				if (this.TenyiSpSummonForTaiaCheck() || this.Level7TenyiSpSummonCheck())
				{
					return true;
				}
			}
			else if (base.ActivateDescription == base.Util.GetStringId(23431858, 1) && base.Card.Location == CardLocation.Grave && this.CheckCalledbytheGrave(base.Card.Id) == 0)
			{
				List<ClientCard> dangerList = this.GetProblematicEnemyCardList(true, true);
				if (dangerList.Count<ClientCard>() > 0)
				{
					base.AI.SelectCard(dangerList);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001EB1 RID: 7857 RVA: 0x000BC7E4 File Offset: 0x000BA9E4
		public bool Level7TenyiSpSummonCheck()
		{
			List<int> advanceSummonCheckList = new List<int> { 20001443, 56495147, 55273560 };
			List<int> tunerList = new List<int> { 98159737, 97268402, 14558127 };
			return (!this.summoned && !base.Bot.HasInHand(advanceSummonCheckList) && base.Bot.HasInHand(tunerList)) || (base.Bot.HasInExtra(60465049) && base.Bot.HasInMonstersZone(20001444, false, false, false) && !this.onlyWyrmSpSummon);
		}

		// Token: 0x06001EB2 RID: 7858 RVA: 0x000BC898 File Offset: 0x000BAA98
		public bool SwordsoulStrategistLongyuanActivate()
		{
			if (base.Card.Location != CardLocation.Hand)
			{
				return true;
			}
			if (this.CheckWhetherNegated() || (this.CheckAtAdvantage() && this.enemyActivateMaxxC && base.Util.IsTurn1OrMain2()))
			{
				return false;
			}
			List<int> discardIdList = new List<int>();
			if (this.CheckAtAdvantage() && base.Bot.HasInHand(93850690) && base.Bot.HasInHand(56495147) && !this.activatedCardIdList.Contains(56495147) && !this.activatedCardIdList.Contains(93850690))
			{
				discardIdList.Add(56495147);
			}
			if (discardIdList.Count<int>() == 0)
			{
				foreach (int tenyiId in new List<int> { 23431858, 87052196, 98159737 })
				{
					if (base.Bot.HasInHand(tenyiId))
					{
						discardIdList.Add(tenyiId);
					}
				}
			}
			if (discardIdList.Count<int>() == 0)
			{
				List<int> checkIdList2 = new List<int> { 56495147, 20001443, 14821890, 93490856, 56465981 };
				Func<ClientCard, bool> <>9__0;
				foreach (int checkId in checkIdList2)
				{
					IEnumerable<ClientCard> hand = base.Bot.Hand;
					Func<ClientCard, bool> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = (ClientCard card) => card != this.Card && card.IsCode(checkIdList2));
					}
					if (hand.Count(func) > 1)
					{
						discardIdList.Add(checkId);
					}
				}
			}
			if (discardIdList.Count<int>() == 0)
			{
				List<int> checkIdList = new List<int> { 56495147, 20001443, 14821890, 93490856, 93850690, 56465981 };
				Func<ClientCard, bool> <>9__1;
				foreach (int checkId2 in checkIdList)
				{
					IEnumerable<ClientCard> hand2 = base.Bot.Hand;
					Func<ClientCard, bool> func2;
					if ((func2 = <>9__1) == null)
					{
						func2 = (<>9__1 = (ClientCard card) => card != this.Card && card.IsCode(checkIdList));
					}
					if (hand2.Count(func2) >= 1)
					{
						discardIdList.Add(checkId2);
					}
				}
			}
			if (discardIdList.Count<int>() > 0)
			{
				base.AI.SelectCard(discardIdList);
				base.AI.SelectPosition(CardPosition.FaceUpAttack);
				base.AI.SelectPosition(CardPosition.FaceUpDefence);
				this.activatedCardIdList.Add(base.Card.Id);
				return true;
			}
			return false;
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x000BCBB8 File Offset: 0x000BADB8
		public bool SwordsoulOfTaiaActivate()
		{
			if (base.Card.Location != CardLocation.MonsterZone)
			{
				List<int> sendToGYTarget = new List<int>();
				if (!base.Bot.HasInGraveyard(20001443) && this.CheckRemainInDeck(20001443) > 0)
				{
					bool sendMoYe = false;
					if (base.Bot.HasInMonstersZone(83755611, true, false, !this.activatedCardIdList.Contains(83755611)))
					{
						sendMoYe = true;
					}
					if (base.Bot.HasInHand(93850690) && !this.activatedCardIdList.Contains(93850690))
					{
						if (base.Bot.Hand.Any((ClientCard card) => card.Id != 93850690 && (card.HasSetcode(363) || card.HasRace(CardRace.Wyrm))))
						{
							sendMoYe = true;
						}
					}
					if (sendMoYe)
					{
						sendToGYTarget.Add(20001443);
					}
				}
				foreach (int id in new List<int> { 98159737, 23431858, 87052196 })
				{
					if (this.CheckRemainInDeck(id) > 0)
					{
						sendToGYTarget.Add(id);
					}
				}
				if (sendToGYTarget.Count<int>() > 0)
				{
					base.AI.SelectCard(sendToGYTarget);
					return true;
				}
				return false;
			}
			else
			{
				if (base.Bot.HasInGraveyard(14821890) && !this.activatedCardIdList.Contains(14821890))
				{
					base.AI.SelectCard(14821890);
					base.AI.SelectPosition(CardPosition.FaceUpDefence);
					this.activatedCardIdList.Add(base.Card.Id);
					return true;
				}
				if (this.CheckWhetherNegated())
				{
					return false;
				}
				List<int> banishIdList = new List<int>();
				List<int> checkIdList = new List<int>
				{
					93490856, 56465981, 56495147, 20001443, 32519092, 78917791, 93850690, 69248256, 87052196, 23431858,
					47710198, 96633955, 5041348, 98159737
				};
				using (List<int>.Enumerator enumerator = checkIdList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int checkId = enumerator.Current;
						if (base.Bot.Graveyard.Count((ClientCard card) => card.IsCode(checkId)) > 1)
						{
							banishIdList.Add(checkId);
						}
					}
				}
				if (banishIdList.Count<int>() == 0)
				{
					foreach (int checkId2 in checkIdList)
					{
						if (base.Bot.HasInGraveyard(checkId2))
						{
							banishIdList.Add(checkId2);
						}
					}
				}
				if (banishIdList.Count<int>() > 0)
				{
					base.AI.SelectCard(banishIdList);
					base.AI.SelectPosition(CardPosition.FaceUpDefence);
					this.activatedCardIdList.Add(base.Card.Id);
					return true;
				}
				return false;
			}
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x000BCF2C File Offset: 0x000BB12C
		public bool SwordsoulOfTaiaSummon()
		{
			if (base.Bot.HasInGraveyard(14821890) && !this.activatedCardIdList.Contains(56495147) && !this.activatedCardIdList.Contains(14821890))
			{
				this.summoned = true;
				return true;
			}
			if (this.SummonLevel4ForSynchro())
			{
				this.summoned = true;
				return true;
			}
			if (this.CheckWhetherNegated())
			{
				return false;
			}
			if (this.SwordsoulOfTaiaEffectCheck(null) && !this.activatedCardIdList.Contains(56495147))
			{
				this.summoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001EB5 RID: 7861 RVA: 0x000BCFB8 File Offset: 0x000BB1B8
		public bool SwordsoulOfTaiaEffectCheck(ClientCard exceptTarget = null)
		{
			if (exceptTarget == null)
			{
				exceptTarget = base.Card;
			}
			return base.Bot.Graveyard.Count((ClientCard card) => card != exceptTarget && (card.HasSetcode(363) || card.HasRace(CardRace.Wyrm))) > 0;
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x000BD008 File Offset: 0x000BB208
		public bool SwordsoulOfMoYeActivate()
		{
			if (base.Card.Location != CardLocation.MonsterZone)
			{
				return true;
			}
			if (this.CheckWhetherNegated())
			{
				return false;
			}
			List<ClientCard> revealList = base.Bot.Hand.Where((ClientCard card) => card.HasSetcode(363) || card.HasRace(CardRace.Wyrm)).ToList<ClientCard>();
			if (revealList.Count<ClientCard>() > 0)
			{
				revealList = this.ShuffleCardList(revealList);
				base.AI.SelectCard(revealList);
				base.AI.SelectPosition(CardPosition.FaceUpDefence);
				this.activatedCardIdList.Add(base.Card.Id);
				return true;
			}
			return false;
		}

		// Token: 0x06001EB7 RID: 7863 RVA: 0x000BD0A8 File Offset: 0x000BB2A8
		public bool SwordsoulOfMoYeSummon()
		{
			if (this.SummonLevel4ForSynchro())
			{
				this.summoned = true;
				return true;
			}
			if (this.CheckWhetherNegated())
			{
				return false;
			}
			if (this.SwordsoulOfMoYeEffectCheck(null) && !this.activatedCardIdList.Contains(20001443))
			{
				this.summoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001EB8 RID: 7864 RVA: 0x000BD0F8 File Offset: 0x000BB2F8
		public bool SwordsoulOfMoYeEffectCheck(List<ClientCard> exceptList = null)
		{
			if (exceptList == null)
			{
				exceptList = new List<ClientCard> { base.Card };
			}
			return base.Bot.Hand.Count((ClientCard card) => !exceptList.Contains(card) && (card.HasSetcode(363) || card.HasRace(CardRace.Wyrm))) > 0;
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x000BD150 File Offset: 0x000BB350
		public bool SummonLevel4ForSynchro()
		{
			if (base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && !card.HasType((CardType)75497472) && !card.IsTuner()))
			{
				return false;
			}
			List<ClientCard> tunerList = (from card in base.Bot.GetMonsters()
				where card.IsFaceup() && !card.HasType((CardType)75497472) && card.IsTuner()
				select card).ToList<ClientCard>();
			if (tunerList.Count<ClientCard>() > 0)
			{
				foreach (ClientCard tuner in tunerList)
				{
					int checkLevel = tuner.Level + 4;
					if (base.Bot.ExtraDeck.Any((ClientCard card) => card.HasType(CardType.Synchro) && card.Level == checkLevel))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x000BD244 File Offset: 0x000BB444
		public bool IncredibleEcclesiaTheVirtuousActivate()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				return true;
			}
			if (base.Duel.Player == 0 && !this.CheckWhetherNegated())
			{
				bool canActivateMoye = !this.activatedCardIdList.Contains(20001443) && this.CheckRemainInDeck(20001443) > 0 && this.CheckCalledbytheGrave(20001443) == 0 && this.SwordsoulOfMoYeEffectCheck(null);
				bool canActivateTaia = !this.activatedCardIdList.Contains(56495147) && this.CheckRemainInDeck(56495147) > 0 && this.CheckCalledbytheGrave(56495147) == 0 && this.SwordsoulOfTaiaEffectCheck(null);
				if (canActivateMoye && !this.summoned && !base.Bot.HasInHand(20001443))
				{
					base.AI.SelectCard(20001443);
					this.activatedCardIdList.Add(base.Card.Id);
					return true;
				}
				if (canActivateTaia && !this.summoned && !base.Bot.HasInHand(56495147))
				{
					base.AI.SelectCard(56495147);
					this.activatedCardIdList.Add(base.Card.Id);
					return true;
				}
				if (canActivateMoye)
				{
					base.AI.SelectCard(20001443);
					this.activatedCardIdList.Add(base.Card.Id);
					return true;
				}
				if (canActivateTaia)
				{
					base.AI.SelectCard(56495147);
					this.activatedCardIdList.Add(base.Card.Id);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x000BD3D0 File Offset: 0x000BB5D0
		public bool IncredibleEcclesiaTheVirtuousSummon()
		{
			if (this.CheckWhetherNegated())
			{
				return false;
			}
			if (this.SwordsoulOfMoYeSummon() && this.CheckRemainInDeck(20001443) > 0)
			{
				this.summoned = true;
				return true;
			}
			if (this.SwordsoulOfTaiaSummon() && this.CheckRemainInDeck(56495147) > 0)
			{
				this.summoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x000BD426 File Offset: 0x000BB626
		public bool IncredibleEcclesiaTheVirtuousSpSummon()
		{
			return !this.CheckWhetherNegated() && (!this.CheckAtAdvantage() || !this.enemyActivateMaxxC || !base.Util.IsTurn1OrMain2());
		}

		// Token: 0x06001EBD RID: 7869 RVA: 0x000BD454 File Offset: 0x000BB654
		public bool AshBlossomActivate()
		{
			if (this.CheckWhetherNegated() || !this.CheckLastChainShouldNegated())
			{
				return false;
			}
			if (this.CheckAtAdvantage() && base.Duel.LastChainPlayer == 1 && base.Util.GetLastChainCard().IsCode(23434538))
			{
				return false;
			}
			if (base.DefaultAshBlossomAndJoyousSpring())
			{
				this.CheckDeactiveFlag();
				return true;
			}
			return false;
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x000BD4B3 File Offset: 0x000BB6B3
		public bool MaxxCActivate()
		{
			return !this.CheckWhetherNegated() && base.Duel.LastChainPlayer != 0 && base.DefaultMaxxC();
		}

		// Token: 0x06001EBF RID: 7871 RVA: 0x000BD4D4 File Offset: 0x000BB6D4
		public bool EffectVeilerActivate()
		{
			if (this.CheckWhetherNegated())
			{
				return false;
			}
			List<ClientCard> shouldNegateList = this.GetMonsterListForTargetNegate(true, false);
			if (shouldNegateList.Count<ClientCard>() > 0)
			{
				ClientCard target = shouldNegateList[0];
				this.currentNegateMonsterList.Add(target);
				base.AI.SelectCard(target);
				return true;
			}
			return false;
		}

		// Token: 0x06001EC0 RID: 7872 RVA: 0x000BD520 File Offset: 0x000BB720
		public bool TunerForSynchroSummon()
		{
			if (!base.Card.IsCode(14558127) && !base.Card.IsCode(98159737) && !base.Card.IsCode(97268402))
			{
				return false;
			}
			if (base.Bot.HasInExtra(32519092) && base.Bot.HasInHand(56495147) && !this.activatedCardIdList.Contains(56495147) && this.CheckCalledbytheGrave(56495147) == 0)
			{
				return false;
			}
			if (base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && card.IsTuner()))
			{
				return false;
			}
			List<int> checkOnField = new List<int> { 23431858, 87052196 };
			if (base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && checkOnField.Contains(card.Id) && !card.IsTuner()))
			{
				int totalLevel = base.Card.Level + 7;
				if (base.Bot.ExtraDeck.Any((ClientCard card) => card.HasType(CardType.Synchro) && card.Level == totalLevel && (!this.onlyWyrmSpSummon || card.HasRace(CardRace.Wyrm))))
				{
					this.summoned = true;
					return true;
				}
			}
			List<ClientCard> checkNonTuner = (from card in base.Bot.GetMonsters()
				where card.IsFaceup() && !card.IsTuner()
				select card).ToList<ClientCard>();
			checkNonTuner.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			if (base.Bot.HasInExtra(43202238) && this.GetProblematicEnemyCardList(true, true).Count<ClientCard>() > 0)
			{
				foreach (ClientCard checkCard in checkNonTuner)
				{
					if (base.Card.Level + checkCard.Level == 7)
					{
						this.summoned = true;
						return true;
					}
				}
			}
			if (base.Bot.HasInExtra(60465049))
			{
				foreach (ClientCard checkCard2 in checkNonTuner)
				{
					if ((checkCard2.IsDisabled() || !checkCard2.HasType(CardType.Synchro)) && base.Card.Level + checkCard2.Level == 11)
					{
						this.summoned = true;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x000BD7C0 File Offset: 0x000BB9C0
		public bool WyrmForBlackoutSummon()
		{
			if (base.Card.Level > 4 || !base.Card.HasRace(CardRace.Wyrm))
			{
				return false;
			}
			if (base.Bot.HasInHandOrInSpellZone(14821890))
			{
				if (!base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && card.HasRace(CardRace.Wyrm)))
				{
					this.summoned = true;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x000BD83C File Offset: 0x000BBA3C
		public bool TenyiSpirit_AdharaActivate()
		{
			if (base.ActivateDescription == base.Util.GetStringId(98159737, 0))
			{
				if (this.TenyiSpSummonForTaiaCheck())
				{
					return true;
				}
			}
			else if (base.ActivateDescription == base.Util.GetStringId(98159737, 1) && base.Card.Location == CardLocation.Grave && this.CheckCalledbytheGrave(base.Card.Id) == 0)
			{
				if (!this.activatedCardIdList.Contains(93490856) && this.SwordsoulOfMoYeEffectCheck(null) && base.Bot.HasInBanished(93490856))
				{
					base.AI.SelectCard(93490856);
					return true;
				}
				if (!this.summoned)
				{
					if (!this.activatedCardIdList.Contains(20001443) && this.SwordsoulOfMoYeEffectCheck(null) && base.Bot.HasInBanished(20001443))
					{
						base.AI.SelectCard(20001443);
						return true;
					}
					if (!this.activatedCardIdList.Contains(56495147) && this.SwordsoulOfTaiaEffectCheck(null) && base.Bot.HasInBanished(56495147))
					{
						base.AI.SelectCard(56495147);
						return true;
					}
				}
				foreach (int recycle in new List<int> { 23431858, 87052196 })
				{
					if (base.Bot.HasInBanished(recycle))
					{
						base.AI.SelectCard(recycle);
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x000BD9F0 File Offset: 0x000BBBF0
		public bool TenyiSpSummonForTaiaCheck()
		{
			if (!this.activatedCardIdList.Contains(56495147) && this.CheckCalledbytheGrave(56495147) == 0)
			{
				bool flag = (!this.summoned && base.Bot.HasInHand(56495147)) || base.Bot.HasInMonstersZone(56495147, false, false, false);
				bool noTargetInGrave = !base.Bot.Graveyard.Any((ClientCard card) => card.HasRace(CardRace.Wyrm) || card.HasSetcode(363));
				bool hasInExtra = base.Bot.HasInExtra(32519092);
				bool notLongyuan = this.activatedCardIdList.Contains(93490856) || !base.Bot.HasInHand(93490856);
				if (flag && noTargetInGrave && hasInExtra && notLongyuan)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x000BDACC File Offset: 0x000BBCCC
		public bool TenyiForShamanSpSummon()
		{
			if (!new List<int>
			{
				base.Util.GetStringId(98159737, 0),
				base.Util.GetStringId(23431858, 0),
				base.Util.GetStringId(87052196, 0)
			}.Contains(base.ActivateDescription) || this.summoned || !base.Bot.HasInExtra(78917791) || (this.CheckAtAdvantage() && this.enemyActivateMaxxC))
			{
				return false;
			}
			ClientCard toSummonMoye = base.Bot.Hand.FirstOrDefault((ClientCard card) => card.IsCode(20001443));
			if (toSummonMoye == null)
			{
				return false;
			}
			List<ClientCard> notRevealCheckList = new List<ClientCard> { base.Card, toSummonMoye };
			return this.SwordsoulOfMoYeEffectCheck(notRevealCheckList) && !this.activatedCardIdList.Contains(20001443) && !this.activatedCardIdList.Contains(56495147) && base.Bot.HasInHandOrInGraveyard(56495147);
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x000BDBF0 File Offset: 0x000BBDF0
		public bool TenyiForBlackoutSpSummon()
		{
			if (!new List<int>
			{
				base.Util.GetStringId(98159737, 0),
				base.Util.GetStringId(23431858, 0),
				base.Util.GetStringId(87052196, 0)
			}.Contains(base.ActivateDescription))
			{
				return false;
			}
			if (this.CheckAtAdvantage() && this.enemyActivateMaxxC)
			{
				return false;
			}
			if (base.Bot.HasInHandOrInSpellZone(14821890))
			{
				if (!base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && card.HasRace(CardRace.Wyrm)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x000BDCB0 File Offset: 0x000BBEB0
		public bool PotOfDesiresActivate()
		{
			if (this.CheckWhetherNegated())
			{
				return false;
			}
			if (this.CheckAtAdvantage())
			{
				bool flag = base.Bot.Deck.Count<ClientCard>() >= 15;
				if (flag)
				{
					this.SelectSTPlace(null, true, null);
				}
				return flag;
			}
			this.SelectSTPlace(null, true, null);
			return true;
		}

		// Token: 0x06001EC7 RID: 7879 RVA: 0x000BDD00 File Offset: 0x000BBF00
		public bool SwordsoulEmergenceActivate()
		{
			if (base.Card.Location == CardLocation.Removed)
			{
				return this.SwordsoulSpellBanishedEffect();
			}
			if (!base.Bot.HasInHand(20001443) && !this.activatedCardIdList.Contains(20001443) && this.CheckRemainInDeck(20001443) > 0 && this.SwordsoulOfMoYeEffectCheck(null))
			{
				base.AI.SelectCard(20001443);
				this.activatedCardIdList.Add(base.Card.Id);
				this.SelectSTPlace(null, true, null);
				return true;
			}
			if (!base.Bot.HasInHand(56495147) && !this.activatedCardIdList.Contains(56495147) && this.CheckRemainInDeck(56495147) > 0 && this.SwordsoulOfTaiaEffectCheck(null))
			{
				base.AI.SelectCard(56495147);
				this.activatedCardIdList.Add(base.Card.Id);
				this.SelectSTPlace(null, true, null);
				return true;
			}
			if (!base.Bot.HasInHand(93490856) && !this.activatedCardIdList.Contains(93490856) && this.CheckRemainInDeck(93490856) > 0 && this.SwordsoulOfMoYeEffectCheck(null))
			{
				base.AI.SelectCard(93490856);
				this.activatedCardIdList.Add(base.Card.Id);
				this.SelectSTPlace(null, true, null);
				return true;
			}
			if (!base.Bot.HasInHand(20001443) && this.CheckRemainInDeck(20001443) > 0 && this.SwordsoulOfMoYeEffectCheck(null))
			{
				base.AI.SelectCard(20001443);
				this.activatedCardIdList.Add(base.Card.Id);
				this.SelectSTPlace(null, true, null);
				return true;
			}
			foreach (int checkId in new List<int> { 56495147, 20001443, 93490856 })
			{
				if (this.CheckRemainInDeck(checkId) > 0)
				{
					base.AI.SelectCard(checkId);
					this.activatedCardIdList.Add(base.Card.Id);
					this.SelectSTPlace(null, true, null);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001EC8 RID: 7880 RVA: 0x000BDF5C File Offset: 0x000BC15C
		public bool SwordsoulSacredSummitActivate()
		{
			if (base.Card.Location == CardLocation.Removed)
			{
				return this.SwordsoulSpellBanishedEffect();
			}
			if (this.CheckAtAdvantage())
			{
				if (this.enemyActivateMaxxC && base.Util.IsTurn1OrMain2())
				{
					return false;
				}
				if (!this.activatedCardIdList.Contains(20001443) && base.Bot.HasInGraveyard(20001443) && this.CheckCalledbytheGrave(20001443) == 0 && this.SwordsoulOfMoYeEffectCheck(null))
				{
					base.AI.SelectCard(20001443);
					this.activatedCardIdList.Add(base.Card.Id);
					this.SelectSTPlace(null, true, null);
					return true;
				}
				if (!this.activatedCardIdList.Contains(56495147) && this.CheckCalledbytheGrave(56495147) == 0)
				{
					ClientCard taia = base.Bot.Graveyard.FirstOrDefault((ClientCard card) => card.IsCode(56495147));
					if (taia != null && this.SwordsoulOfTaiaEffectCheck(taia))
					{
						base.AI.SelectCard(56495147);
						this.activatedCardIdList.Add(base.Card.Id);
						this.SelectSTPlace(null, true, null);
						return true;
					}
				}
			}
			bool controlSynchro = base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && card.HasType(CardType.Synchro));
			List<ClientCard> rebornTargetList = base.Bot.Graveyard.Where((ClientCard card) => card.IsMonster() && (card.HasSetcode(363) || (controlSynchro && card.HasRace(CardRace.Wyrm)))).ToList<ClientCard>();
			rebornTargetList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			rebornTargetList.Reverse();
			if (rebornTargetList.Count<ClientCard>() <= 0)
			{
				return false;
			}
			ClientCard rebornTarget = rebornTargetList[0];
			if (rebornTarget.IsCode(20001443) && (this.activatedCardIdList.Contains(20001443) || !this.SwordsoulOfMoYeEffectCheck(null)))
			{
				return false;
			}
			if (rebornTarget.IsCode(56495147) && this.activatedCardIdList.Contains(56495147))
			{
				return false;
			}
			base.AI.SelectCard(rebornTargetList);
			this.activatedCardIdList.Add(base.Card.Id);
			this.SelectSTPlace(null, true, null);
			return true;
		}

		// Token: 0x06001EC9 RID: 7881 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SwordsoulSpellBanishedEffect()
		{
			return false;
		}

		// Token: 0x06001ECA RID: 7882 RVA: 0x000BE19C File Offset: 0x000BC39C
		public bool CalledbytheGraveActivate()
		{
			if (this.CheckWhetherNegated() || !this.CheckLastChainShouldNegated())
			{
				return false;
			}
			if (this.CheckAtAdvantage() && base.Duel.LastChainPlayer == 1 && base.Util.GetLastChainCard().IsCode(23434538))
			{
				return false;
			}
			if (base.Duel.LastChainPlayer == 1)
			{
				if (base.Util.GetLastChainCard().IsMonster())
				{
					int code = base.Util.GetLastChainCard().GetOriginCode();
					if (code == 0)
					{
						return false;
					}
					if (this.CheckCalledbytheGrave(code) > 0)
					{
						return false;
					}
					if (base.Util.GetLastChainCard().IsCode(23434538) && this.CheckAtAdvantage())
					{
						return false;
					}
					ClientCard graveTarget = base.Enemy.Graveyard.GetFirstMatchingCard((ClientCard card) => card.IsMonster() && card.GetOriginCode() == code);
					if (graveTarget != null)
					{
						if (base.Card.Location != CardLocation.SpellZone)
						{
							this.SelectSTPlace(null, true, null);
						}
						base.AI.SelectCard(graveTarget);
						this.currentNegatingIdList.Add(code);
						this.CheckDeactiveFlag();
						return true;
					}
				}
				foreach (ClientCard cards in base.Enemy.Graveyard)
				{
					if (base.Duel.ChainTargets.Contains(cards) && cards.IsMonster())
					{
						if (base.Card.Location != CardLocation.SpellZone)
						{
							this.SelectSTPlace(null, true, null);
						}
						int code4 = cards.Id;
						base.AI.SelectCard(cards);
						this.currentNegatingIdList.Add(code4);
						return true;
					}
				}
				if (!base.Duel.ChainTargets.Contains(base.Card))
				{
					goto IL_0249;
				}
				List<ClientCard> enemyMonsters = base.Enemy.Graveyard.GetMatchingCards((ClientCard card) => card.IsMonster()).ToList<ClientCard>();
				if (enemyMonsters.Count<ClientCard>() > 0)
				{
					enemyMonsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					enemyMonsters.Reverse();
					int code2 = enemyMonsters[0].Id;
					base.AI.SelectCard(code2);
					this.currentNegatingIdList.Add(code2);
					return true;
				}
			}
			IL_0249:
			if (base.Duel.LastChainPlayer == 1)
			{
				return false;
			}
			List<ClientCard> targets = this.CheckDangerousCardinEnemyGrave(true);
			if (targets.Count<ClientCard>() > 0)
			{
				int code3 = targets[0].Id;
				if (base.Card.Location != CardLocation.SpellZone)
				{
					this.SelectSTPlace(null, true, null);
				}
				base.AI.SelectCard(code3);
				this.currentNegatingIdList.Add(code3);
				return true;
			}
			return false;
		}

		// Token: 0x06001ECB RID: 7883 RVA: 0x000BE468 File Offset: 0x000BC668
		public bool CrossoutDesignatorActivate()
		{
			if (this.CheckWhetherNegated() || !this.CheckLastChainShouldNegated())
			{
				return false;
			}
			if (base.Duel.LastChainPlayer == 1 && base.Util.GetLastChainCard() != null)
			{
				int code = base.Util.GetLastChainCard().Id;
				int alias = base.Util.GetLastChainCard().Alias;
				if (alias != 0 && alias - code < 10)
				{
					code = alias;
				}
				if (code == 0)
				{
					return false;
				}
				if (base.DefaultCheckWhetherCardIdIsNegated(code))
				{
					return false;
				}
				if (this.CheckRemainInDeck(code) > 0)
				{
					if (base.Card.Location != CardLocation.SpellZone)
					{
						this.SelectSTPlace(null, true, null);
					}
					base.AI.SelectAnnounceID(code);
					this.currentNegatingIdList.Add(code);
					this.CheckDeactiveFlag();
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001ECC RID: 7884 RVA: 0x000BE528 File Offset: 0x000BC728
		public bool InfiniteImpermanenceActivate()
		{
			if (this.CheckWhetherNegated())
			{
				return false;
			}
			ClientCard LastChainCard = base.Util.GetLastChainCard();
			if (base.Card.Location == CardLocation.SpellZone)
			{
				int this_seq = -1;
				int that_seq = -1;
				for (int i = 0; i < 5; i++)
				{
					if (base.Bot.SpellZone[i] == base.Card)
					{
						this_seq = i;
					}
					if (LastChainCard != null && LastChainCard.Controller == 1 && LastChainCard.Location == CardLocation.SpellZone && base.Enemy.SpellZone[i] == LastChainCard)
					{
						that_seq = i;
					}
					else if (base.Duel.Player == 0 && base.Util.GetProblematicEnemySpell() != null && base.Enemy.SpellZone[i] != null && base.Enemy.SpellZone[i].IsFloodgate())
					{
						that_seq = i;
					}
				}
				if ((this_seq * that_seq >= 0 && this_seq + that_seq == 4) || base.Util.IsChainTarget(base.Card) || (LastChainCard != null && LastChainCard.Controller == 1 && LastChainCard.IsCode(18144506)))
				{
					this.CheckDeactiveFlag();
					ClientCard target = this.GetProblematicEnemyMonster(0, true);
					if (target != null)
					{
						base.AI.SelectCard(target);
					}
					else
					{
						base.AI.SelectCard(base.Enemy.GetMonsters());
					}
					this.infiniteImpermanenceList.Add(this_seq);
					return true;
				}
			}
			List<ClientCard> shouldNegateList = this.GetMonsterListForTargetNegate(false, true);
			if (shouldNegateList.Count<ClientCard>() > 0)
			{
				ClientCard negateTarget = shouldNegateList[0];
				this.currentNegateMonsterList.Add(negateTarget);
				if (base.Card.Location == CardLocation.SpellZone)
				{
					for (int j = 0; j < 5; j++)
					{
						if (base.Bot.SpellZone[j] == base.Card)
						{
							this.infiniteImpermanenceList.Add(j);
							break;
						}
					}
				}
				if (base.Card.Location == CardLocation.Hand)
				{
					this.SelectSTPlace(base.Card, true, null);
				}
				base.AI.SelectCard(negateTarget);
				return true;
			}
			return false;
		}

		// Token: 0x06001ECD RID: 7885 RVA: 0x000BE718 File Offset: 0x000BC918
		public bool SwordsoulBlackoutActivate()
		{
			if (base.Card.Location == CardLocation.Removed)
			{
				base.AI.SelectPosition(CardPosition.FaceUpDefence);
				this.activatedCardIdList.Add(base.Card.Id);
				return true;
			}
			List<ClientCard> list = (from card in base.Bot.GetMonsters()
				where card.IsFaceup() && card.HasRace(CardRace.Wyrm)
				select card).ToList<ClientCard>();
			list.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			ClientCard selfDestroyTarget = list[0];
			bool selfTargetIsImportant = selfDestroyTarget.HasType(CardType.Synchro);
			List<ClientCard> chengyingList = (from card in base.Bot.GetMonsters()
				where card.IsCode(96633955) && card.IsFaceup() && !card.IsDisabled()
				select card).ToList<ClientCard>();
			if (chengyingList.Count<ClientCard>() > 0 && base.Bot.Graveyard.Count<ClientCard>() > 0)
			{
				selfDestroyTarget = chengyingList[0];
				selfTargetIsImportant = false;
			}
			foreach (ClientCard selfCard in list)
			{
				if (base.Duel.LastChainTargets.Contains(selfCard))
				{
					selfDestroyTarget = selfCard;
					selfTargetIsImportant = false;
				}
			}
			List<ClientCard> problemCardList = this.GetProblematicEnemyCardList(true, false);
			if (problemCardList.Count<ClientCard>() >= 2 && base.Duel.Player == 1)
			{
				base.AI.SelectCard(selfDestroyTarget);
				base.AI.SelectNextCard(problemCardList);
				return true;
			}
			List<ClientCard> faceUpEnemyMonsterList = (from card in base.Enemy.GetMonsters()
				where card.IsFaceup()
				select card).ToList<ClientCard>();
			faceUpEnemyMonsterList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			faceUpEnemyMonsterList.Reverse();
			if (!selfTargetIsImportant && base.Duel.Player == 1)
			{
				if (faceUpEnemyMonsterList.Count<ClientCard>() >= 2)
				{
					base.AI.SelectCard(selfDestroyTarget);
					base.AI.SelectNextCard(this.GetNormalEnemyTargetList(true));
					return true;
				}
				if (base.Duel.Phase == DuelPhase.End)
				{
					base.AI.SelectCard(selfDestroyTarget);
					base.AI.SelectNextCard(this.GetNormalEnemyTargetList(true));
					return true;
				}
			}
			if (base.Duel.Phase > DuelPhase.Main1 && base.Duel.Phase < DuelPhase.Main2 && faceUpEnemyMonsterList.Count<ClientCard>() > 0)
			{
				int botBestAttack = base.Util.GetBestAttack(base.Bot);
				if (faceUpEnemyMonsterList[0].GetDefensePower() >= botBestAttack)
				{
					base.AI.SelectCard(selfDestroyTarget);
					base.AI.SelectNextCard(this.GetNormalEnemyTargetList(true));
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001ECE RID: 7886 RVA: 0x000BE9C8 File Offset: 0x000BCBC8
		public bool GeomathmechFinalSigmaSpSummon()
		{
			if (base.Bot.GetMonstersExtraZoneCount() > 0)
			{
				return false;
			}
			if (base.Enemy.GetMonsters().Any((ClientCard card) => card.HasSetcode(365)) | base.Enemy.GetSpells().Any((ClientCard card) => card.HasSetcode(365)) | base.Enemy.Graveyard.Any((ClientCard card) => card.HasSetcode(365)) | base.Enemy.Banished.Any((ClientCard card) => card.HasSetcode(365)))
			{
				base.AI.SelectMaterials(this.GetSynchroMaterial(12, false), 0);
				base.AI.SelectPosition(CardPosition.FaceUpAttack);
				return true;
			}
			return false;
		}

		// Token: 0x06001ECF RID: 7887 RVA: 0x000BEACC File Offset: 0x000BCCCC
		public bool PsychicEndPunisherSpSummon()
		{
			List<ClientCard> materialList = this.GetSynchroMaterial(11, false);
			if (materialList.Count<ClientCard>() > 1)
			{
				base.AI.SelectMaterials(materialList, 0);
				base.AI.SelectPosition(CardPosition.FaceUpAttack);
				return true;
			}
			return false;
		}

		// Token: 0x06001ED0 RID: 7888 RVA: 0x000BEB08 File Offset: 0x000BCD08
		public bool Level10SpSummonCheckInit()
		{
			this.canSpSummonLevel10IdList.Clear();
			return false;
		}

		// Token: 0x06001ED1 RID: 7889 RVA: 0x000BEB18 File Offset: 0x000BCD18
		public bool Level10SpSummonCheckCount()
		{
			foreach (int checkId in new List<int> { 96633955, 47710198, 84815190 })
			{
				if (base.Card.IsCode(checkId))
				{
					this.canSpSummonLevel10IdList.Add(checkId);
				}
			}
			return false;
		}

		// Token: 0x06001ED2 RID: 7890 RVA: 0x000BEBA0 File Offset: 0x000BCDA0
		public bool Level10SpSummonCheckDecide()
		{
			if (this.canSpSummonLevel10IdList.Count <= 1)
			{
				return false;
			}
			List<int> decideIdList = new List<int>();
			if (this.canSpSummonLevel10IdList.Contains(84815190))
			{
				if (base.Bot.HasInHand(23434538))
				{
					this.canSpSummonLevel10IdList.Clear();
					this.canSpSummonLevel10IdList.Add(84815190);
					return false;
				}
				ClientCard taia = base.Bot.Graveyard.FirstOrDefault((ClientCard card) => card.IsCode(56495147));
				if (taia != null && this.SwordsoulOfTaiaEffectCheck(taia) && base.Bot.HasInHand(93850690))
				{
					this.canSpSummonLevel10IdList.Clear();
					this.canSpSummonLevel10IdList.Add(84815190);
					return false;
				}
				decideIdList.Add(84815190);
			}
			if (this.canSpSummonLevel10IdList.Contains(47710198) && this.CheckAtAdvantage())
			{
				decideIdList.Add(47710198);
			}
			if (this.canSpSummonLevel10IdList.Contains(96633955))
			{
				int banishCount = base.Bot.Banished.Count<ClientCard>() + base.Enemy.Banished.Count<ClientCard>();
				bool decideFlag = base.Bot.HasInHandOrInSpellZone(14821890) || base.Bot.HasInMonstersZone(69248256, true, false, true);
				if (this.CheckAtAdvantage())
				{
					if (3000 + banishCount * 100 >= base.Enemy.LifePoints)
					{
						decideFlag = true;
					}
				}
				else
				{
					ClientCard enemyMonster = this.GetBestEnemyMonster(true, false);
					if (enemyMonster != null && decideIdList.Count<int>() == 0 && 3000 + banishCount * 200 >= enemyMonster.GetDefensePower())
					{
						decideFlag = true;
					}
				}
				if (decideFlag)
				{
					decideIdList.Add(96633955);
				}
			}
			if (decideIdList.Count<int>() > 0)
			{
				this.canSpSummonLevel10IdList.Clear();
				int index = Program.Rand.Next(decideIdList.Count<int>());
				int lastDecide = decideIdList[index];
				this.canSpSummonLevel10IdList.Add(lastDecide);
			}
			return false;
		}

		// Token: 0x06001ED3 RID: 7891 RVA: 0x000BEDA0 File Offset: 0x000BCFA0
		public bool Level10SpSummonCheckFinal()
		{
			if (this.canSpSummonLevel10IdList.Count<int>() == 1)
			{
				int finalDecideId = this.canSpSummonLevel10IdList[0];
				if (base.Card.IsCode(finalDecideId))
				{
					List<ClientCard> materialList = this.GetSynchroMaterial(10, base.Card.IsCode(47710198));
					if (materialList.Count<ClientCard>() > 1)
					{
						base.AI.SelectMaterials(materialList, 0);
						return true;
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001ED4 RID: 7892 RVA: 0x000BEE0C File Offset: 0x000BD00C
		public bool AdamancipatorRisen_DragiteSpSummon()
		{
			if (!base.Bot.HasInMonstersZone(69248256, true, false, false))
			{
				return false;
			}
			bool containWaterMonsterInGY = base.Bot.Graveyard.Any((ClientCard card) => card.IsMonster() && card.HasAttribute(CardAttribute.Water));
			if (!(containWaterMonsterInGY | base.Bot.GetMonsters().Any((ClientCard card) => card.HasAttribute(CardAttribute.Water) && card.IsFaceup())))
			{
				return false;
			}
			this.SelectLevel8SynchroMaterial(false, !containWaterMonsterInGY);
			return true;
		}

		// Token: 0x06001ED5 RID: 7893 RVA: 0x000BEEA1 File Offset: 0x000BD0A1
		public bool DracoBerserkerOfTheTenyiSpSummon()
		{
			if (this.CheckAtAdvantage() && this.enemyActivateMaxxC && base.Util.IsTurn1OrMain2())
			{
				return false;
			}
			this.SelectLevel8SynchroMaterial(true, false);
			return true;
		}

		// Token: 0x06001ED6 RID: 7894 RVA: 0x000BEECB File Offset: 0x000BD0CB
		public bool SwordsoulGrandmaster_ChixiaoSpSummon()
		{
			if (this.CheckAtAdvantage() && this.enemyActivateLockBird)
			{
				return false;
			}
			if (!this.activatedCardIdList.Contains(69248256))
			{
				this.SelectLevel8SynchroMaterial(true, false);
				return true;
			}
			return false;
		}

		// Token: 0x06001ED7 RID: 7895 RVA: 0x000BEEFC File Offset: 0x000BD0FC
		public bool BaxiaBrightnessOfTheYangZingSpSummon()
		{
			if (this.CheckAtAdvantage())
			{
				return false;
			}
			List<ClientCard> problemList = this.GetProblematicEnemyCardList(true, false);
			if (problemList.Count<ClientCard>() > 1 && !this.activatedCardIdList.Contains(83755612))
			{
				this.SelectLevel8SynchroMaterial(true, false);
				return true;
			}
			if (problemList.Count<ClientCard>() == 1 && base.Bot.GetSpellCount() > 0 && !this.activatedCardIdList.Contains(83755613))
			{
				bool checkFlag = false;
				if (!this.activatedCardIdList.Contains(20001443) && this.SwordsoulOfMoYeEffectCheck(null) && base.Bot.HasInGraveyard(20001443))
				{
					checkFlag = true;
				}
				if (!this.activatedCardIdList.Contains(56495147) && base.Bot.HasInGraveyard(56495147))
				{
					checkFlag = true;
				}
				if (checkFlag)
				{
					this.SelectLevel8SynchroMaterial(true, false);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x000BEFD4 File Offset: 0x000BD1D4
		public void SelectLevel8SynchroMaterial(bool needWyrmNonTuner = false, bool needWaterNonTuner = false)
		{
			List<ClientCard> tunerList = (from card in base.Bot.GetMonsters()
				where card.IsFaceup() && card.IsTuner() && card.Level < 8
				select card).ToList<ClientCard>();
			List<ClientCard> nonTunerList = (from card in base.Bot.GetMonsters()
				where card.IsFaceup() && !card.IsTuner() && card.Level < 8 && (!needWyrmNonTuner || (card.HasRace(CardRace.Wyrm) && (!needWaterNonTuner || card.HasAttribute(CardAttribute.Water))))
				select card).ToList<ClientCard>();
			tunerList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			nonTunerList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			List<ClientCard> materialList = new List<ClientCard>();
			foreach (ClientCard tuner in tunerList)
			{
				materialList.Clear();
				materialList.Add(tuner);
				if (tuner.Level == 4)
				{
					if (this.activatedCardIdList.Contains(20001443))
					{
						ClientCard moye = nonTunerList.GetFirstMatchingCard((ClientCard card) => card.IsCode(20001443));
						if (moye != null)
						{
							materialList.Add(moye);
							base.AI.SelectMaterials(materialList, 0);
							break;
						}
					}
					if (this.activatedCardIdList.Contains(56495147) && !needWaterNonTuner)
					{
						ClientCard taia = nonTunerList.GetFirstMatchingCard((ClientCard card) => card.IsCode(56495147));
						if (taia != null)
						{
							materialList.Add(taia);
							base.AI.SelectMaterials(materialList, 0);
							break;
						}
					}
				}
				foreach (ClientCard nonTuner in nonTunerList)
				{
					if (tuner.Level + nonTuner.Level == 8)
					{
						materialList.Add(nonTuner);
						base.AI.SelectMaterials(materialList, 0);
						return;
					}
				}
			}
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x000BF200 File Offset: 0x000BD400
		public bool YaziEvilOfTheYangZingSpSummon()
		{
			if (base.Enemy.GetMonsterCount() + base.Enemy.GetSpellCount() == 0)
			{
				return false;
			}
			if ((this.GetProblematicEnemyCardList(true, true).Count<ClientCard>() > 0) | (!this.activatedCardIdList.Contains(20001443) && this.CheckCalledbytheGrave(20001443) == 0 && this.CheckRemainInDeck(20001443) > 0 && this.SwordsoulOfMoYeEffectCheck(null)) | (!this.activatedCardIdList.Contains(56495147) && this.CheckCalledbytheGrave(56495147) == 0 && this.CheckRemainInDeck(56495147) > 0))
			{
				List<ClientCard> materialList = this.GetSynchroMaterial(7, false);
				if (materialList.Count<ClientCard>() > 1)
				{
					base.AI.SelectMaterials(materialList, 0);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001EDA RID: 7898 RVA: 0x000BF2C8 File Offset: 0x000BD4C8
		public List<ClientCard> GetSynchroMaterial(int level, bool needWyrmNonTuner = false)
		{
			List<ClientCard> tunerList = (from card in base.Bot.GetMonsters()
				where card.IsFaceup() && card.IsTuner() && !card.HasType((CardType)75497472)
				select card).ToList<ClientCard>();
			List<ClientCard> nonTunerList = (from card in base.Bot.GetMonsters()
				where card.IsFaceup() && !card.IsTuner() && !card.HasType((CardType)75497472) && (!needWyrmNonTuner || card.HasRace(CardRace.Wyrm))
				select card).ToList<ClientCard>();
			tunerList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			nonTunerList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			List<ClientCard> selectList = new List<ClientCard>();
			foreach (ClientCard tuner in tunerList)
			{
				selectList.Clear();
				selectList.Add(tuner);
				foreach (ClientCard nonTuner in nonTunerList)
				{
					if (tuner.Level + nonTuner.Level == level && (nonTuner.IsDisabled() || !nonTuner.HasType(CardType.Synchro)))
					{
						selectList.Add(nonTuner);
						return selectList;
					}
				}
			}
			selectList.Clear();
			return selectList;
		}

		// Token: 0x06001EDB RID: 7899 RVA: 0x000BF428 File Offset: 0x000BD628
		public bool ShamanOfTheTenyiSpSummon()
		{
			if (this.CheckAtAdvantage() && this.enemyActivateMaxxC && base.Util.IsTurn1OrMain2())
			{
				Logger.DebugWriteLine("[Shaman] advantage & maxxc, skip");
				return false;
			}
			List<ClientCard> extraZoneMonsters = base.Bot.GetMonstersInExtraZone();
			if (extraZoneMonsters.Count<ClientCard>() > 0)
			{
				if (extraZoneMonsters.Any((ClientCard card) => card.IsFacedown() || !card.HasType(CardType.Link) || !card.HasRace(CardRace.Wyrm)))
				{
					Logger.DebugWriteLine("[Shaman] extra zone occupied, skip");
					return false;
				}
			}
			if (!((!this.activatedCardIdList.Contains(56495147) && this.CheckCalledbytheGrave(56495147) == 0 && base.Bot.HasInHandOrInGraveyard(56495147)) | (!this.activatedCardIdList.Contains(20001443) && this.CheckCalledbytheGrave(20001443) == 0 && base.Bot.HasInGraveyard(20001443) && this.SwordsoulOfMoYeEffectCheck(null)) | base.Bot.GetGraveyardMonsters().Any((ClientCard card) => card.HasType(CardType.Synchro) && card.IsCanRevive() && card.HasRace(CardRace.Wyrm))))
			{
				Logger.DebugWriteLine("[Shaman] no target, skip");
				return false;
			}
			List<ClientCard> materialList = new List<ClientCard>(extraZoneMonsters);
			List<ClientCard> mainMonsterZoneMonsters = (from card in base.Bot.GetMonstersInMainZone()
				where card.IsFaceup() && !card.HasType(CardType.Synchro) && card.HasRace(CardRace.Wyrm)
				select card).ToList<ClientCard>();
			mainMonsterZoneMonsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			materialList.AddRange(mainMonsterZoneMonsters);
			if (materialList.Count<ClientCard>() >= 2)
			{
				base.AI.SelectMaterials(materialList.GetRange(0, 2), 0);
				return true;
			}
			return false;
		}

		// Token: 0x06001EDC RID: 7900 RVA: 0x000BF5C8 File Offset: 0x000BD7C8
		public bool MonkOfTheTenyiSpSummon()
		{
			List<ClientCard> materialList = (from card in base.Bot.GetMonsters()
				where card.IsFaceup() && !card.HasType((CardType)67117056) && card.HasSetcode(300)
				select card).ToList<ClientCard>();
			if (materialList.Count<ClientCard>() > 0)
			{
				materialList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				base.AI.SelectMaterials(materialList, 0);
				return true;
			}
			return false;
		}

		// Token: 0x06001EDD RID: 7901 RVA: 0x000BF638 File Offset: 0x000BD838
		public bool PsychicEndPunisherActivate()
		{
			if (base.Duel.Phase > DuelPhase.Main1 && base.Duel.Phase < DuelPhase.Main2)
			{
				return true;
			}
			if (base.Bot.LifePoints <= 1500 || this.CheckWhetherNegated())
			{
				return false;
			}
			List<ClientCard> selfBanishTarget = (from card in base.Bot.GetMonsters()
				where card != base.Card && (card.IsFacedown() || card.GetDefensePower() <= 1000)
				select card).ToList<ClientCard>();
			if (selfBanishTarget.Count<ClientCard>() == 0)
			{
				return false;
			}
			selfBanishTarget.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			base.AI.SelectCard(selfBanishTarget);
			base.AI.SelectNextCard(this.GetNormalEnemyTargetList(true));
			return true;
		}

		// Token: 0x06001EDE RID: 7902 RVA: 0x000BF6E0 File Offset: 0x000BD8E0
		public bool SwordsoulSupremeSovereign_ChengyingActivate()
		{
			if (base.ActivateDescription == base.Util.GetStringId(96633955, 0) || base.ActivateDescription == -1)
			{
				this.activatedCardIdList.Add(base.Card.Id);
				List<ClientCard> banishTargetList = base.Duel.CurrentChain.Where((ClientCard card) => card.Controller == 1 && card.Location == CardLocation.Grave).ToList<ClientCard>();
				banishTargetList.AddRange(this.CheckDangerousCardinEnemyGrave(false));
				if (banishTargetList.Count<ClientCard>() > 0)
				{
					ClientCard graveTarget = banishTargetList[0];
					Logger.DebugWriteLine("Chengying banish grave: " + ((graveTarget != null) ? graveTarget.Name : null));
				}
				List<ClientCard> fieldTargetList = this.GetNormalEnemyTargetList(true);
				if (fieldTargetList.Count<ClientCard>() > 0)
				{
					ClientCard fieldTarget = fieldTargetList[0];
					Logger.DebugWriteLine("Chengying banish field: " + ((fieldTarget != null) ? fieldTarget.Name : null));
				}
				banishTargetList.AddRange(fieldTargetList);
				base.AI.SelectCard(banishTargetList);
			}
			else if (base.ActivateDescription == 96)
			{
				List<int> removeCardIdList = new List<int>
				{
					24224830, 65681983, 10045474, 14558127, 23434538, 97268402, 32519092, 78917791, 69248256, 56495147,
					93490856, 20001443
				};
				base.AI.SelectCard(removeCardIdList);
			}
			else
			{
				Logger.DebugWriteLine("Chengying desc: " + base.ActivateDescription.ToString());
			}
			return true;
		}

		// Token: 0x06001EDF RID: 7903 RVA: 0x000BF8A8 File Offset: 0x000BDAA8
		public bool BaronneDeFleurActivate()
		{
			if (base.ActivateDescription != base.Util.GetStringId(84815190, 1))
			{
				if (base.Duel.Phase == DuelPhase.Standby)
				{
					if (this.effectUsedBaronneDeFleurList.Contains(base.Card) && !this.CheckWhetherNegated())
					{
						if (base.Duel.Player == 1)
						{
							if (!base.Bot.HasInMonstersZone(69248256, false, false, false) && base.Bot.HasInGraveyard(69248256))
							{
								base.AI.SelectCard(69248256);
								return true;
							}
						}
						else
						{
							if (this.GetProblematicEnemyCardList(true, true).Count<ClientCard>() > 0)
							{
								return false;
							}
							if (this.CheckAtAdvantage())
							{
								if (base.Bot.ExtraDeck.Any((ClientCard card) => card.IsFacedown() && card.HasType(CardType.Synchro) && card.Level == 8))
								{
									if (base.Bot.HasInGraveyard(20001443) && this.SwordsoulOfMoYeEffectCheck(null) && this.CheckCalledbytheGrave(20001443) == 0)
									{
										base.AI.SelectCard(20001443);
										return true;
									}
									if (this.CheckCalledbytheGrave(56495147) == 0)
									{
										ClientCard taia = base.Bot.Graveyard.FirstOrDefault((ClientCard card) => card.IsCode(56495147));
										if (taia != null && this.SwordsoulOfTaiaEffectCheck(taia))
										{
											base.AI.SelectCard(56495147);
											return true;
										}
									}
								}
							}
						}
					}
				}
				else
				{
					List<ClientCard> targetList = this.GetNormalEnemyTargetList(true);
					if (targetList.Count<ClientCard>() > 0)
					{
						base.AI.SelectCard(targetList);
						return true;
					}
				}
				return false;
			}
			if (this.CheckWhetherNegated() || !this.CheckLastChainShouldNegated())
			{
				return false;
			}
			if (base.Duel.LastChainPlayer == 1)
			{
				ClientCard lastChainCard = base.Util.GetLastChainCard();
				if (this.CheckAtAdvantage() && lastChainCard.IsCode(23434538))
				{
					return false;
				}
				if (base.Duel.LastChainTargets.Contains(base.Card) && lastChainCard.IsCode(new int[] { 97268402, 10045474, 78474168 }))
				{
					return false;
				}
			}
			this.CheckDeactiveFlag();
			this.effectUsedBaronneDeFleurList.Add(base.Card);
			return true;
		}

		// Token: 0x06001EE0 RID: 7904 RVA: 0x000BFAE8 File Offset: 0x000BDCE8
		public bool SwordsoulSinisterSovereign_QixingLongyuanActivate()
		{
			if (base.ActivateDescription == base.Util.GetStringId(47710198, 0))
			{
				return true;
			}
			if (base.ActivateDescription == -1 || base.ActivateDescription == base.Util.GetStringId(47710198, 1))
			{
				return true;
			}
			if (base.ActivateDescription == base.Util.GetStringId(47710198, 2))
			{
				ClientCard lastChainCard = base.Util.GetLastChainCard();
				if (lastChainCard != null && lastChainCard.Controller == 1 && (base.DefaultOnBecomeTarget() | (base.Enemy.LifePoints <= 1200) | lastChainCard.HasType((CardType)17694720)))
				{
					return true;
				}
			}
			else
			{
				Logger.DebugWriteLine("qixinglongyuan desc: " + base.ActivateDescription.ToString());
			}
			return false;
		}

		// Token: 0x06001EE1 RID: 7905 RVA: 0x000BFBB0 File Offset: 0x000BDDB0
		public bool AdamancipatorRisen_DragiteActivate()
		{
			if (base.ActivateDescription != -1 && base.ActivateDescription != base.Util.GetStringId(9464441, 0))
			{
				return !this.CheckWhetherNegated();
			}
			if (this.CheckWhetherNegated())
			{
				return false;
			}
			if (this.CheckRemainInDeck(27204311) > 0 && base.Enemy.GetMonsterCount() + base.Enemy.GetSpellCount() > 0)
			{
				base.AI.SelectCard(this.GetNormalEnemyTargetList(false));
				return true;
			}
			return false;
		}

		// Token: 0x06001EE2 RID: 7906 RVA: 0x000BFC34 File Offset: 0x000BDE34
		public bool DracoBerserkerOfTheTenyiActivate()
		{
			ClientCard lastChainCard = base.Util.GetLastChainCard();
			return lastChainCard == null || !lastChainCard.IsCode(27204311) || lastChainCard.Controller != 1;
		}

		// Token: 0x06001EE3 RID: 7907 RVA: 0x000BFC6C File Offset: 0x000BDE6C
		public bool SwordsoulGrandmaster_ChixiaoActivate()
		{
			if (base.ActivateDescription != base.Util.GetStringId(69248256, 1))
			{
				if (this.CheckAtAdvantage() && this.enemyActivateMaxxC && base.Util.IsTurn1OrMain2())
				{
					if (this.CheckRemainInDeck(14821890) > 0)
					{
						base.AI.SelectCard(14821890);
						this.activatedCardIdList.Add(base.Card.Id);
						return true;
					}
					foreach (int checkId4 in new List<int> { 14821890, 20001443, 56495147, 56465981, 93490856 })
					{
						if (this.CheckRemainInDeck(checkId4) > 0 && !base.Bot.HasInHand(checkId4))
						{
							base.AI.SelectCard(checkId4);
							this.activatedCardIdList.Add(base.Card.Id);
							return true;
						}
					}
				}
				if (this.CheckAtAdvantage())
				{
					if (!this.activatedCardIdList.Contains(93490856) && !base.Bot.HasInHand(93490856) && this.SwordsoulOfMoYeEffectCheck(null) && this.CheckRemainInDeck(93490856) > 0)
					{
						base.AI.SelectCard(93490856);
						this.activatedCardIdList.Add(base.Card.Id);
						return true;
					}
					if (!this.activatedCardIdList.Contains(93490856) && base.Bot.HasInHand(93490856) && !this.activatedCardIdList.Contains(56495147) && !this.activatedCardIdList.Contains(93850690))
					{
						if (base.Bot.HasInHandOrInGraveyard(56495147) && !base.Bot.HasInHand(93850690) && this.CheckRemainInDeck(93850690) > 0)
						{
							base.AI.SelectCard(93850690);
							this.activatedCardIdList.Add(base.Card.Id);
							return true;
						}
						if (!base.Bot.HasInHandOrInGraveyard(56495147) && base.Bot.HasInHand(93850690) && this.CheckRemainInDeck(56495147) > 0)
						{
							base.AI.SelectCard(56495147);
							this.activatedCardIdList.Add(base.Card.Id);
							return true;
						}
					}
				}
				if (!base.Bot.HasInMonstersZone(20001444, false, false, false) && base.Bot.HasInMonstersZone(93490856, false, false, false) && base.Bot.HasInMonstersZone(93490856, false, false, false) && this.CheckRemainInDeck(14821890) > 0 && !this.activatedCardIdList.Contains(14821890))
				{
					Logger.DebugWriteLine("Chixiao banish blackout");
					base.AI.SelectCard(14821890);
					base.AI.SelectOption(1);
					this.activatedCardIdList.Add(base.Card.Id);
					return true;
				}
				if (this.CheckAtAdvantage())
				{
					foreach (int checkId2 in new List<int> { 14821890, 20001443, 56495147, 56465981, 93490856 })
					{
						if (this.CheckRemainInDeck(checkId2) > 0 && !base.Bot.HasInHand(checkId2))
						{
							base.AI.SelectCard(checkId2);
							this.activatedCardIdList.Add(base.Card.Id);
							return true;
						}
					}
				}
				foreach (int checkId3 in new List<int> { 14821890, 20001443, 56495147, 56465981, 93490856 })
				{
					if (this.CheckRemainInDeck(checkId3) > 0 && !base.Bot.HasInHand(checkId3))
					{
						base.AI.SelectCard(checkId3);
						this.activatedCardIdList.Add(base.Card.Id);
						return true;
					}
				}
				return false;
			}
			if (this.CheckWhetherNegated())
			{
				return false;
			}
			List<ClientCard> negateTargetList = new List<ClientCard>();
			List<ClientCard> shouldNegateList = this.GetMonsterListForTargetNegate(true, false);
			if (shouldNegateList.Count<ClientCard>() > 0)
			{
				ClientCard target = shouldNegateList[0];
				this.currentNegateMonsterList.Add(target);
				negateTargetList.AddRange(shouldNegateList);
			}
			if (base.Duel.Phase > DuelPhase.Main1 && base.Duel.Phase < DuelPhase.Main2)
			{
				bool botCanAttack = base.Bot.GetMonsters().Any((ClientCard card) => card.IsAttack());
				if (base.Duel.Player == 0 && botCanAttack)
				{
					negateTargetList.AddRange((from card in base.Enemy.GetMonsters()
						where card.IsFaceup() && card.IsMonsterDangerous()
						select card).ToList<ClientCard>());
				}
				if (base.Duel.Player == 1)
				{
					ClientCard enemyMonster = base.Enemy.BattlingMonster;
					if (enemyMonster != null && enemyMonster.IsMonsterInvincible())
					{
						negateTargetList.Add(enemyMonster);
					}
				}
			}
			if (base.Bot.HasInMonstersZone(96633955, true, false, true) && !this.activatedCardIdList.Contains(96633955) && base.Enemy.Graveyard.Count<ClientCard>() > 0 && (this.GetProblematicEnemyMonster(0, false) != null || (base.Duel.Phase == DuelPhase.End && base.Duel.Player == 1)))
			{
				bool triggerFlag = true;
				List<ClientCard> enemyTargetList = (from card in base.Enemy.GetMonsters()
					where card.IsFaceup() && card.HasType(CardType.Effect) && !card.IsShouldNotBeMonsterTarget() && card.IsShouldNotBeTarget()
					select card).ToList<ClientCard>();
				if (enemyTargetList.Count<ClientCard>() == 0)
				{
					List<ClientCard> botTargetList = (from card in base.Bot.GetMonsters()
						where card.IsFaceup() && card.HasType(CardType.Effect) && !card.IsDisabled() && card != base.Card && !card.IsCode(96633955)
						select card).ToList<ClientCard>();
					if (botTargetList.Count<ClientCard>() == 0)
					{
						triggerFlag = false;
					}
					else
					{
						botTargetList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
						enemyTargetList.AddRange(botTargetList);
					}
				}
				else
				{
					enemyTargetList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					enemyTargetList.Reverse();
				}
				if (triggerFlag)
				{
					negateTargetList.AddRange(enemyTargetList);
				}
			}
			if (negateTargetList.Count<ClientCard>() > 0)
			{
				List<ClientCard> graveBanishList = base.Bot.Graveyard.Where((ClientCard card) => card.HasSetcode(363) || card.HasRace(CardRace.Wyrm)).ToList<ClientCard>();
				if (graveBanishList.Count<ClientCard>() > 0)
				{
					bool selectFlag = false;
					ClientCard blackOut = graveBanishList.FirstOrDefault((ClientCard card) => card.IsCode(14821890));
					if (base.Duel.Player == 0 && !this.activatedCardIdList.Contains(14821890) && blackOut != null)
					{
						base.AI.SelectCard(blackOut);
						selectFlag = true;
					}
					if (!selectFlag)
					{
						using (List<int>.Enumerator enumerator = new List<int> { 56465981, 56495147, 20001443, 93490856, 32519092, 98159737, 23431858, 87052196 }.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								int checkId5 = enumerator.Current;
								List<ClientCard> checkCardList = graveBanishList.Where((ClientCard card) => card.IsCode(checkId5)).ToList<ClientCard>();
								if (checkCardList.Count<ClientCard>() > 1)
								{
									base.AI.SelectCard(checkCardList);
									selectFlag = true;
									break;
								}
							}
						}
					}
					if (!selectFlag)
					{
						using (List<int>.Enumerator enumerator = new List<int> { 56465981, 32519092, 78917791, 56495147, 93490856, 20001443, 98159737, 23431858, 87052196 }.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								int checkId = enumerator.Current;
								List<ClientCard> checkCardList2 = graveBanishList.Where((ClientCard card) => card.IsCode(checkId)).ToList<ClientCard>();
								if (checkCardList2.Count<ClientCard>() > 0)
								{
									base.AI.SelectCard(checkCardList2);
									selectFlag = true;
									break;
								}
							}
						}
					}
					if (!selectFlag)
					{
						base.AI.SelectCard(this.ShuffleCardList(graveBanishList));
					}
				}
				base.AI.SelectNextCard(negateTargetList);
				this.activatedCardIdList.Add(base.Card.Id);
				return true;
			}
			return false;
		}

		// Token: 0x06001EE4 RID: 7908 RVA: 0x000C0628 File Offset: 0x000BE828
		public bool BaxiaBrightnessOfTheYangZingActivate()
		{
			Logger.DebugWriteLine("Baxia desc: " + base.ActivateDescription.ToString());
			if (base.ActivateDescription == base.Util.GetStringId(83755611, 0))
			{
				List<ClientCard> enemyTargetList = this.GetNormalEnemyTargetList(true);
				if (enemyTargetList.Count<ClientCard>() > 0)
				{
					base.AI.SelectCard(enemyTargetList);
					this.activatedCardIdList.Add(base.Card.Id + 1);
					return true;
				}
			}
			else
			{
				List<ClientCard> destroyTarget = base.Bot.GetSpells();
				destroyTarget.AddRange((from card in base.Bot.GetMonsters()
					where card.IsFacedown() || card.Attack <= 1000
					select card).ToList<ClientCard>());
				if (destroyTarget.Count<ClientCard>() == 0)
				{
					return false;
				}
				bool canUseMoye = !this.activatedCardIdList.Contains(20001443) && this.CheckCalledbytheGrave(20001443) == 0 && this.SwordsoulOfMoYeEffectCheck(null);
				bool canUseTaia = !this.activatedCardIdList.Contains(56495147) && this.CheckCalledbytheGrave(56495147) == 0 && this.SwordsoulOfTaiaEffectCheck(null);
				if (canUseMoye && base.Bot.HasInGraveyard(20001443))
				{
					base.AI.SelectCard(destroyTarget);
					base.AI.SelectNextCard(20001443);
					this.activatedCardIdList.Add(base.Card.Id + 2);
					return true;
				}
				if (canUseTaia && base.Bot.HasInGraveyard(56495147))
				{
					base.AI.SelectCard(destroyTarget);
					base.AI.SelectNextCard(56495147);
					this.activatedCardIdList.Add(base.Card.Id + 2);
					return true;
				}
				if (base.Bot.HasInGraveyard(55273560))
				{
					if (!this.activatedCardIdList.Contains(55273560) && ((canUseMoye && this.CheckRemainInDeck(20001443) > 0) || (canUseTaia && this.CheckRemainInDeck(56495147) > 0)))
					{
						base.AI.SelectCard(destroyTarget);
						base.AI.SelectNextCard(55273560);
						this.activatedCardIdList.Add(base.Card.Id + 2);
						return true;
					}
					if (base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && !card.IsTuner() && card.Level == 4))
					{
						base.AI.SelectCard(destroyTarget);
						base.AI.SelectNextCard(55273560);
						this.activatedCardIdList.Add(base.Card.Id + 2);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001EE5 RID: 7909 RVA: 0x000C08CC File Offset: 0x000BEACC
		public bool YaziEvilOfTheYangZingActivate()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				if (!this.activatedCardIdList.Contains(20001443) && this.CheckRemainInDeck(20001443) > 0 && this.CheckCalledbytheGrave(20001443) == 0 && this.SwordsoulOfMoYeEffectCheck(null))
				{
					base.AI.SelectCard(20001443);
					return true;
				}
				if (!this.activatedCardIdList.Contains(56495147) && this.CheckRemainInDeck(56495147) > 0 && this.CheckCalledbytheGrave(56495147) == 0)
				{
					base.AI.SelectCard(56495147);
					return true;
				}
				if (base.Bot.HasInMonstersZone(20001444, false, false, false))
				{
					foreach (int checkId in new List<int> { 93490856, 20001443, 56495147 })
					{
						if (this.CheckRemainInDeck(checkId) > 0)
						{
							base.AI.SelectCard(checkId);
							return true;
						}
					}
				}
				using (List<int>.Enumerator enumerator = new List<int> { 87052196, 23431858, 98159737, 93490856, 20001443, 56495147 }.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int checkId2 = enumerator.Current;
						if (this.CheckRemainInDeck(checkId2) > 0)
						{
							base.AI.SelectCard(checkId2);
							return true;
						}
					}
					return false;
				}
			}
			if (this.CheckWhetherNegated())
			{
				return false;
			}
			bool selfDestroy = false;
			if (!this.activatedCardIdList.Contains(20001443) && this.CheckRemainInDeck(20001443) > 0 && this.CheckCalledbytheGrave(20001443) == 0 && this.SwordsoulOfMoYeEffectCheck(null))
			{
				selfDestroy = true;
			}
			if (!this.activatedCardIdList.Contains(56495147) && this.CheckRemainInDeck(56495147) > 0 && this.CheckCalledbytheGrave(56495147) == 0)
			{
				selfDestroy = true;
			}
			if (selfDestroy)
			{
				base.AI.SelectCard(base.Card);
			}
			else
			{
				List<ClientCard> YangZingList = (from card in base.Bot.GetMonsters()
					where card.IsFaceup() && card.HasSetcode(158)
					select card).ToList<ClientCard>();
				YangZingList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				base.AI.SelectCard(YangZingList);
			}
			base.AI.SelectNextCard(this.GetNormalEnemyTargetList(true));
			return true;
		}

		// Token: 0x06001EE6 RID: 7910 RVA: 0x000C0B9C File Offset: 0x000BED9C
		public bool ShamanOfTheTenyiActivate()
		{
			if (base.Duel.Phase > DuelPhase.Main1 && base.Duel.Phase < DuelPhase.Main2)
			{
				base.AI.SelectCard(this.GetNormalEnemyTargetList(true));
				return true;
			}
			if (this.CheckAtAdvantage() && this.enemyActivateMaxxC && base.Util.IsTurn1OrMain2())
			{
				return false;
			}
			bool canUseMoye = base.Bot.HasInGraveyard(20001443) && this.CheckCalledbytheGrave(20001443) == 0 && !this.activatedCardIdList.Contains(20001443);
			bool canUseTaia = base.Bot.HasInHandOrInGraveyard(56495147) && this.CheckCalledbytheGrave(56495147) == 0 && !this.activatedCardIdList.Contains(56495147);
			bool shouldDiscardTaia = !base.Bot.HasInGraveyard(56495147) && base.Bot.HasInHand(56495147);
			List<ClientCard> sortedReviveTargetList = (from card in base.Bot.GetGraveyardMonsters()
				where card.IsCanRevive() && card.HasRace(CardRace.Wyrm)
				select card).ToList<ClientCard>();
			sortedReviveTargetList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			sortedReviveTargetList.Reverse();
			if (this.CheckAtAdvantage())
			{
				if (base.Duel.Turn > 1 && base.Enemy.GetMonsterCount() == 0)
				{
					int currentAttack = base.Util.GetTotalAttackingMonsterAttack(0);
					if (currentAttack < base.Enemy.LifePoints)
					{
						List<ClientCard> overkillList = sortedReviveTargetList.Where((ClientCard card) => card.Attack + currentAttack >= this.Enemy.LifePoints).ToList<ClientCard>();
						if (overkillList.Count<ClientCard>() > 0)
						{
							this.SelectDiscardForShamanOfTheTenyi(shouldDiscardTaia);
							base.AI.SelectNextCard(overkillList);
							return true;
						}
					}
				}
				if (canUseMoye)
				{
					this.SelectDiscardForShamanOfTheTenyi(false);
					base.AI.SelectNextCard(20001443);
					return true;
				}
				if (canUseTaia)
				{
					this.SelectDiscardForShamanOfTheTenyi(shouldDiscardTaia);
					base.AI.SelectNextCard(56495147);
					return true;
				}
				this.SelectDiscardForShamanOfTheTenyi(false);
				base.AI.SelectNextCard(sortedReviveTargetList);
				return true;
			}
			else
			{
				List<ClientCard> synchroMonsterList = sortedReviveTargetList.Where((ClientCard card) => card.HasType(CardType.Synchro)).ToList<ClientCard>();
				if (synchroMonsterList.Count<ClientCard>() > 0)
				{
					this.SelectDiscardForShamanOfTheTenyi(false);
					base.AI.SelectNextCard(synchroMonsterList);
					return true;
				}
				if (canUseMoye)
				{
					this.SelectDiscardForShamanOfTheTenyi(false);
					base.AI.SelectNextCard(20001443);
					return true;
				}
				if (canUseTaia)
				{
					this.SelectDiscardForShamanOfTheTenyi(shouldDiscardTaia);
					base.AI.SelectNextCard(56495147);
					return true;
				}
				return false;
			}
		}

		// Token: 0x06001EE7 RID: 7911 RVA: 0x000C0E44 File Offset: 0x000BF044
		public void SelectDiscardForShamanOfTheTenyi(bool useTaia = false)
		{
			if (useTaia)
			{
				base.AI.SelectCard(56495147);
				return;
			}
			foreach (int tenyiId in new List<int> { 23431858, 87052196, 98159737 })
			{
				if (base.Bot.HasInHand(tenyiId))
				{
					base.AI.SelectCard(tenyiId);
					return;
				}
			}
			using (IEnumerator<ClientCard> enumerator2 = base.Bot.Hand.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					ClientCard hand = enumerator2.Current;
					if (base.Bot.Hand.Where((ClientCard card) => card.IsCode(hand.Id)).Count<ClientCard>() > 1)
					{
						base.AI.SelectCard(hand);
						return;
					}
				}
			}
			foreach (int discardCheck in new List<int>
			{
				65681983, 35261759, 87052196, 23431858, 98159737, 27204311, 93850690, 55273560, 10045474, 24224830,
				56495147, 20001443, 93490856, 14558127, 23434538, 97268402, 56465981, 14821890
			})
			{
				if (base.Bot.HasInHand(discardCheck))
				{
					base.AI.SelectCard(discardCheck);
					break;
				}
			}
		}

		// Token: 0x06001EE8 RID: 7912 RVA: 0x000C1084 File Offset: 0x000BF284
		public bool SpellSetCheck()
		{
			if (base.Duel.Phase == DuelPhase.Main1 && base.Bot.HasAttackingMonster() && base.Duel.Turn > 1)
			{
				return false;
			}
			if (new List<int> { 14821890 }.Contains(base.Card.Id) && base.Bot.HasInSpellZone(base.Card.Id, false, false))
			{
				return false;
			}
			if (base.Card.IsTrap() || base.Card.HasType(CardType.QuickPlay))
			{
				List<int> avoid_list = new List<int>();
				int setFornfiniteImpermanence = 0;
				for (int i = 0; i < 5; i++)
				{
					if (base.Enemy.SpellZone[i] != null && base.Enemy.SpellZone[i].IsFaceup() && base.Bot.SpellZone[4 - i] == null)
					{
						avoid_list.Add(4 - i);
						setFornfiniteImpermanence += (int)Math.Pow(2.0, (double)(4 - i));
					}
				}
				if (!base.Bot.HasInHand(10045474))
				{
					this.SelectSTPlace(null, false, null);
					return true;
				}
				if (base.Card.IsCode(10045474))
				{
					base.AI.SelectPlace(setFornfiniteImpermanence);
					return true;
				}
				this.SelectSTPlace(base.Card, false, avoid_list);
				return true;
			}
			else
			{
				if ((base.Enemy.HasInSpellZone(58921041, true, false) || base.Bot.HasInSpellZone(58921041, true, false)) && base.Card.IsSpell() && !base.Bot.HasInSpellZone(base.Card.Id, false, false))
				{
					this.SelectSTPlace(null, false, null);
					return true;
				}
				return false;
			}
		}

		// Token: 0x06001EE9 RID: 7913 RVA: 0x000C122B File Offset: 0x000BF42B
		protected override bool DefaultSetForDiabellze()
		{
			if (base.DefaultSetForDiabellze())
			{
				this.SelectSTPlace(null, true, null);
				return true;
			}
			return false;
		}

		// Token: 0x04002171 RID: 8561
		private const int SetcodeTimeLord = 74;

		// Token: 0x04002172 RID: 8562
		private const int SetcodeYangZing = 158;

		// Token: 0x04002173 RID: 8563
		private const int SetcodePhantom = 219;

		// Token: 0x04002174 RID: 8564
		private const int SetcodeOrcust = 283;

		// Token: 0x04002175 RID: 8565
		private const int SetcodeTenyi = 300;

		// Token: 0x04002176 RID: 8566
		private const int SetcodeSwordsoul = 363;

		// Token: 0x04002177 RID: 8567
		private const int SetcodeFloowandereeze = 365;

		// Token: 0x04002178 RID: 8568
		private List<int> normalCounterList = new List<int> { 14558127, 84815190, 27548199, 4280258, 53262004 };

		// Token: 0x04002179 RID: 8569
		private List<int> notToNegateIdList = new List<int> { 58699500 };

		// Token: 0x0400217A RID: 8570
		private const int hintTimingMainEnd = 4;

		// Token: 0x0400217B RID: 8571
		private const int hintReplaceDestroy = 96;

		// Token: 0x0400217C RID: 8572
		private Dictionary<int, List<int>> DeckCountTable = new Dictionary<int, List<int>>
		{
			{
				3,
				new List<int> { 93490856, 56495147, 20001443, 55273560, 14558127, 23434538, 97268402, 56465981, 10045474 }
			},
			{
				2,
				new List<int> { 87052196, 35261759, 24224830, 14821890 }
			},
			{
				1,
				new List<int> { 27204311, 23431858, 98159737, 93850690, 65681983 }
			}
		};

		// Token: 0x0400217D RID: 8573
		private List<int> currentNegatingIdList = new List<int>();

		// Token: 0x0400217E RID: 8574
		private bool enemyActivateMaxxC;

		// Token: 0x0400217F RID: 8575
		private bool enemyActivateLockBird;

		// Token: 0x04002180 RID: 8576
		private bool enemyActivateInfiniteImpermanenceFromHand;

		// Token: 0x04002181 RID: 8577
		private List<int> infiniteImpermanenceList = new List<int>();

		// Token: 0x04002182 RID: 8578
		private bool summoned;

		// Token: 0x04002183 RID: 8579
		private bool onlyWyrmSpSummon;

		// Token: 0x04002184 RID: 8580
		private List<int> activatedCardIdList = new List<int>();

		// Token: 0x04002185 RID: 8581
		private List<int> canSpSummonLevel10IdList = new List<int>();

		// Token: 0x04002186 RID: 8582
		private List<ClientCard> effectUsedBaronneDeFleurList = new List<ClientCard>();

		// Token: 0x04002187 RID: 8583
		private List<ClientCard> currentNegateMonsterList = new List<ClientCard>();

		// Token: 0x020003DD RID: 989
		public class CardId
		{
			// Token: 0x04002188 RID: 8584
			public const int NibiruThePrimalBeing = 27204311;

			// Token: 0x04002189 RID: 8585
			public const int TenyiSpirit_Ashuna = 87052196;

			// Token: 0x0400218A RID: 8586
			public const int TenyiSpirit_Vishuda = 23431858;

			// Token: 0x0400218B RID: 8587
			public const int SwordsoulStrategistLongyuan = 93490856;

			// Token: 0x0400218C RID: 8588
			public const int SwordsoulOfTaia = 56495147;

			// Token: 0x0400218D RID: 8589
			public const int SwordsoulOfMoYe = 20001443;

			// Token: 0x0400218E RID: 8590
			public const int IncredibleEcclesiaTheVirtuous = 55273560;

			// Token: 0x0400218F RID: 8591
			public const int TenyiSpirit_Adhara = 98159737;

			// Token: 0x04002190 RID: 8592
			public const int SwordsoulEmergence = 56465981;

			// Token: 0x04002191 RID: 8593
			public const int SwordsoulSacredSummit = 93850690;

			// Token: 0x04002192 RID: 8594
			public const int CrossoutDesignator = 65681983;

			// Token: 0x04002193 RID: 8595
			public const int SwordsoulBlackout = 14821890;

			// Token: 0x04002194 RID: 8596
			public const int GeomathmechFinalSigma = 42632209;

			// Token: 0x04002195 RID: 8597
			public const int PsychicEndPunisher = 60465049;

			// Token: 0x04002196 RID: 8598
			public const int SwordsoulSupremeSovereign_Chengying = 96633955;

			// Token: 0x04002197 RID: 8599
			public const int BaronneDeFleur = 84815190;

			// Token: 0x04002198 RID: 8600
			public const int SwordsoulSinisterSovereign_QixingLongyuan = 47710198;

			// Token: 0x04002199 RID: 8601
			public const int AdamancipatorRisen_Dragite = 9464441;

			// Token: 0x0400219A RID: 8602
			public const int DracoBerserkerOfTheTenyi = 5041348;

			// Token: 0x0400219B RID: 8603
			public const int SwordsoulGrandmaster_Chixiao = 69248256;

			// Token: 0x0400219C RID: 8604
			public const int BaxiaBrightnessOfTheYangZing = 83755611;

			// Token: 0x0400219D RID: 8605
			public const int YaziEvilOfTheYangZing = 43202238;

			// Token: 0x0400219E RID: 8606
			public const int ShamanOfTheTenyi = 78917791;

			// Token: 0x0400219F RID: 8607
			public const int MonkOfTheTenyi = 32519092;

			// Token: 0x040021A0 RID: 8608
			public const int SwordsoulToken = 20001444;

			// Token: 0x040021A1 RID: 8609
			public const int NaturalExterio = 99916754;

			// Token: 0x040021A2 RID: 8610
			public const int NaturalBeast = 33198837;

			// Token: 0x040021A3 RID: 8611
			public const int ImperialOrder = 61740673;

			// Token: 0x040021A4 RID: 8612
			public const int SwordsmanLV7 = 37267041;

			// Token: 0x040021A5 RID: 8613
			public const int RoyalDecree = 51452091;

			// Token: 0x040021A6 RID: 8614
			public const int Number41BagooskatheTerriblyTiredTapir = 90590303;

			// Token: 0x040021A7 RID: 8615
			public const int InspectorBoarder = 15397015;
		}
	}
}
