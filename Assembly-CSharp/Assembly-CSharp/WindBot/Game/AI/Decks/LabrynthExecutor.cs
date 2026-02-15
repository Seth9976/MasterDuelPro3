using System;
using System.Collections.Generic;
using System.Linq;
using YGOSharp.OCGWrapper;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x02000321 RID: 801
	[Deck("Labrynth", "AI_Labrynth", "Normal")]
	public class LabrynthExecutor : DefaultExecutor
	{
		// Token: 0x060014F9 RID: 5369 RVA: 0x000770B0 File Offset: 0x000752B0
		public LabrynthExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 23434538, new Func<bool>(this.MaxxCActivate));
			base.AddExecutor(ExecutorType.Activate, 49238328, new Func<bool>(this.PotOfExtravaganceActivate));
			base.AddExecutor(ExecutorType.Repos, 2347656, new Func<bool>(this.ReposForLabrynth));
			base.AddExecutor(ExecutorType.Activate, 22850702, new Func<bool>(this.ChaosAngelActivate));
			base.AddExecutor(ExecutorType.Activate, 2347656, new Func<bool>(this.LovelyLabrynthOfTheSilverCastleActivate));
			base.AddExecutor(ExecutorType.Activate, 94259633, new Func<bool>(this.RelinquishedAnimaActivate));
			base.AddExecutor(ExecutorType.Activate, 1225009, new Func<bool>(this.AriannaTheLabrynthServantActivate));
			base.AddExecutor(ExecutorType.Activate, 75730490, new Func<bool>(this.ArianeTheLabrynthServantActivate));
			base.AddExecutor(ExecutorType.Activate, 37629703, new Func<bool>(this.RecycleActivate));
			base.AddExecutor(ExecutorType.Activate, 74018812, new Func<bool>(this.RecycleActivate));
			base.AddExecutor(ExecutorType.Activate, 2511, new Func<bool>(this.RecycleActivate));
			base.AddExecutor(ExecutorType.Activate, 24269961, new Func<bool>(this.UnchainedSoulLordOfYamaActivate));
			base.AddExecutor(ExecutorType.Activate, 5380979, new Func<bool>(this.RecycleActivate));
			base.AddExecutor(ExecutorType.Activate, 93039339, new Func<bool>(this.SuperStarslayerTYPHONActivate));
			base.AddExecutor(ExecutorType.Activate, 29479265, new Func<bool>(this.UnchainedAbominationActivate));
			base.AddExecutor(ExecutorType.Repos, 75730490, new Func<bool>(this.ReposForLabrynth));
			base.AddExecutor(ExecutorType.Repos, 1225009, new Func<bool>(this.ReposForLabrynth));
			base.AddExecutor(ExecutorType.Activate, 14558127, new Func<bool>(this.AshBlossomActivate));
			base.AddExecutor(ExecutorType.Activate, 81497285, new Func<bool>(this.LadyLabrynthOfTheSilverCastleFieldActivate));
			base.AddExecutor(ExecutorType.Activate, 73602965, new Func<bool>(this.RecycleActivate));
			base.AddExecutor(ExecutorType.Activate, 29301450, new Func<bool>(this.SPLittleKnightActivate));
			base.AddExecutor(ExecutorType.Activate, 83326048, new Func<bool>(this.DimensionalBarrierActivate));
			base.AddExecutor(ExecutorType.Activate, 10045474, new Func<bool>(this.InfiniteImpermanenceActivate));
			base.AddExecutor(ExecutorType.Activate, 71607202, new Func<bool>(this.MuckrakerFromTheUnderworldActivate));
			base.AddExecutor(ExecutorType.Activate, 67680512, new Func<bool>(this.UnchainedSoulOfRageActivate));
			base.AddExecutor(ExecutorType.Activate, 6351147, new Func<bool>(this.TransactionRollbackActivate));
			base.AddExecutor(ExecutorType.Activate, 30748475, new Func<bool>(this.DestructiveDarumaKarmaCannonActivate));
			base.AddExecutor(ExecutorType.Activate, 53417695, new Func<bool>(this.EscapeOfTheUnchainedActivate));
			base.AddExecutor(ExecutorType.Activate, 81497285, new Func<bool>(this.LadyLabrynthOfTheSilverCastleHandActivate));
			base.AddExecutor(ExecutorType.Activate, 92714517, new Func<bool>(this.BigWelcomeLabrynthBecomeTargetActivate));
			base.AddExecutor(ExecutorType.Activate, 5380979, new Func<bool>(this.WelcomeLabrynthActivate));
			base.AddExecutor(ExecutorType.Activate, 92714517, new Func<bool>(this.BigWelcomeLabrynthActivate));
			base.AddExecutor(ExecutorType.Activate, 73602965, new Func<bool>(this.AriasTheLabrynthButlerActivate));
			base.AddExecutor(ExecutorType.Activate, 2511, new Func<bool>(this.LabrynthCooclockActivate));
			base.AddExecutor(ExecutorType.Activate, 92714517, new Func<bool>(this.BigWelcomeLabrynthGraveActivate));
			base.AddExecutor(ExecutorType.Activate, 93084621, new Func<bool>(this.UnchainedSoulOfAnguishActivate));
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(this.SpellSetForCooClockCheck));
			base.AddExecutor(ExecutorType.Summon, 75730490, new Func<bool>(this.ArianeTheLabrynthServantForRollbackSummon));
			base.AddExecutor(ExecutorType.Summon, 1225009, new Func<bool>(this.AriannaTheLabrynthServantSummon));
			base.AddExecutor(ExecutorType.Summon, 75730490, new Func<bool>(this.ArianeTheLabrynthServantSummon));
			base.AddExecutor(ExecutorType.Summon, new Func<bool>(this.LabrynthForCooClockSummon));
			base.AddExecutor(ExecutorType.Summon, new Func<bool>(this.ForLinkSummon));
			base.AddExecutor(ExecutorType.Summon, new Func<bool>(this.ForSynchroSummon));
			base.AddExecutor(ExecutorType.Summon, 2511, new Func<bool>(this.ForAnimaSummon));
			base.AddExecutor(ExecutorType.Activate, 37629703, new Func<bool>(this.FurnitureSetWelcomeActivate));
			base.AddExecutor(ExecutorType.Activate, 74018812, new Func<bool>(this.FurnitureSetWelcomeActivate));
			base.AddExecutor(ExecutorType.SpSummon, 22850702, new Func<bool>(this.ChaosAngelSpSummonWith2Monster));
			base.AddExecutor(ExecutorType.SpSummon, 94259633, new Func<bool>(this.RelinquishedAnimaSpSummon));
			base.AddExecutor(ExecutorType.SpSummon, 24269961, new Func<bool>(this.UnchainedSoulLordOfYamaSpSummon));
			base.AddExecutor(ExecutorType.SpSummon, 93084621, new Func<bool>(this.UnchainedSoulOfAnguishSpSummon));
			base.AddExecutor(ExecutorType.SpSummon, 67680512, new Func<bool>(this.UnchainedSoulOfRageSpSummon));
			base.AddExecutor(ExecutorType.SpSummon, 29479265, new Func<bool>(this.UnchainedAbominationSpSummon));
			base.AddExecutor(ExecutorType.SpSummon, 29301450, new Func<bool>(this.SPLittleKnightSpSummon));
			base.AddExecutor(ExecutorType.SpSummon, 22850702, new Func<bool>(this.ChaosAngelSpSummonWith3Monster));
			base.AddExecutor(ExecutorType.SpSummon, 71607202, new Func<bool>(this.MuckrakerFromTheUnderworldSpSummon));
			base.AddExecutor(ExecutorType.Activate, 41165831, new Func<bool>(this.UnchainedSoulOfSharvaraActivate));
			base.AddExecutor(ExecutorType.SpSummon, 93039339, new Func<bool>(this.SuperStarslayerTYPHONSpSummon));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(this.MonsterRepos));
			base.AddExecutor(ExecutorType.Summon, new Func<bool>(this.SummonForTYPHONCheck));
			base.AddExecutor(ExecutorType.SummonOrSet, new Func<bool>(this.ForBigWelcomeSummon));
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(this.SpellSetCheck));
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x00077968 File Offset: 0x00075B68
		public List<T> ShuffleList<T>(List<T> list)
		{
			int i = list.Count;
			while (i-- > 1)
			{
				int index = Program.Rand.Next(list.Count);
				int nextIndex = (index + Program.Rand.Next(list.Count - 1)) % list.Count;
				T tempCard = list[index];
				list[index] = list[nextIndex];
				list[nextIndex] = tempCard;
			}
			return list;
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x000779D8 File Offset: 0x00075BD8
		public ClientCard GetProblematicEnemyMonster(int attack = 0, bool canBeTarget = false, bool ignoreCurrentDestroy = false, CardType selfType = (CardType)0)
		{
			List<ClientCard> floodagateList = (from c in base.Enemy.GetMonsters()
				where ((c != null) ? c.Data : null) != null && c.IsFloodgate() && c.IsFaceup() && this.CheckCanBeTargeted(c, canBeTarget, selfType) && (!ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(c))
				select c into card
				orderby card.Attack descending
				select card).ToList<ClientCard>();
			if (floodagateList.Count<ClientCard>() > 0)
			{
				return floodagateList[0];
			}
			List<ClientCard> dangerList = (from c in base.Enemy.MonsterZone
				where ((c != null) ? c.Data : null) != null && c.IsMonsterDangerous() && c.IsFaceup() && this.CheckCanBeTargeted(c, canBeTarget, selfType) && (!ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(c))
				select c into card
				orderby card.Attack descending
				select card).ToList<ClientCard>();
			if (dangerList.Count<ClientCard>() > 0)
			{
				return dangerList[0];
			}
			List<ClientCard> invincibleList = (from c in base.Enemy.MonsterZone
				where ((c != null) ? c.Data : null) != null && c.IsMonsterInvincible() && c.IsFaceup() && this.CheckCanBeTargeted(c, canBeTarget, selfType) && (!ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(c))
				select c into card
				orderby card.Attack descending
				select card).ToList<ClientCard>();
			if (invincibleList.Count<ClientCard>() > 0)
			{
				return invincibleList[0];
			}
			List<ClientCard> equippedList = (from c in base.Enemy.MonsterZone
				where ((c != null) ? c.Data : null) != null && c.EquipCards.Count<ClientCard>() > 0 && this.CheckCanBeTargeted(c, canBeTarget, selfType) && (!ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(c))
				select c into card
				orderby card.Attack descending
				select card).ToList<ClientCard>();
			if (equippedList.Count<ClientCard>() > 0)
			{
				return equippedList[0];
			}
			List<ClientCard> enemyMonsters = (from card in base.Enemy.GetMonsters()
				orderby card.Attack descending
				select card).ToList<ClientCard>();
			if (enemyMonsters.Count<ClientCard>() > 0)
			{
				foreach (ClientCard target in enemyMonsters)
				{
					if ((target.HasType((CardType)8396992) || (target.HasType(CardType.Link) && target.LinkCount >= 2)) && this.CheckCanBeTargeted(target, canBeTarget, selfType) && (!ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(target)))
					{
						return target;
					}
				}
			}
			if (attack >= 0)
			{
				if (attack == 0)
				{
					attack = base.Util.GetBestAttack(base.Bot);
				}
				List<ClientCard> betterList = (from card in base.Enemy.MonsterZone.GetMonsters()
					where card.GetDefensePower() >= attack && card.GetDefensePower() > 0 && card.IsAttack() && this.CheckCanBeTargeted(card, canBeTarget, selfType) && (!ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(card))
					orderby card.Attack descending
					select card).ToList<ClientCard>();
				if (betterList.Count<ClientCard>() > 0)
				{
					return betterList[0];
				}
			}
			return null;
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x00077CD8 File Offset: 0x00075ED8
		public List<ClientCard> GetProblematicEnemyCardList(bool canBeTarget = false, bool ignoreSpells = false, CardType selfType = (CardType)0)
		{
			List<ClientCard> resultList = new List<ClientCard>();
			List<ClientCard> floodagateList = (from c in base.Enemy.MonsterZone
				where ((c != null) ? c.Data : null) != null && !this.currentDestroyCardList.Contains(c) && c.IsFloodgate() && c.IsFaceup() && this.CheckCanBeTargeted(c, canBeTarget, selfType)
				select c into card
				orderby card.Attack descending
				select card).ToList<ClientCard>();
			if (floodagateList.Count<ClientCard>() > 0)
			{
				resultList.AddRange(floodagateList);
			}
			List<ClientCard> problemEnemySpellList = base.Enemy.SpellZone.Where((ClientCard c) => ((c != null) ? c.Data : null) != null && !resultList.Contains(c) && !this.currentDestroyCardList.Contains(c) && c.IsFloodgate() && c.IsFaceup() && this.CheckCanBeTargeted(c, canBeTarget, selfType)).ToList<ClientCard>();
			if (problemEnemySpellList.Count<ClientCard>() > 0)
			{
				resultList.AddRange(this.ShuffleList<ClientCard>(problemEnemySpellList));
			}
			List<ClientCard> dangerList = (from c in base.Enemy.MonsterZone
				where ((c != null) ? c.Data : null) != null && !resultList.Contains(c) && !this.currentDestroyCardList.Contains(c) && c.IsMonsterDangerous() && c.IsFaceup() && this.CheckCanBeTargeted(c, canBeTarget, selfType)
				select c into card
				orderby card.Attack descending
				select card).ToList<ClientCard>();
			if (dangerList.Count<ClientCard>() > 0 && (base.Duel.Player == 0 || (base.Duel.Phase > DuelPhase.Main1 && base.Duel.Phase < DuelPhase.Main2)))
			{
				resultList.AddRange(dangerList);
			}
			List<ClientCard> invincibleList = (from c in base.Enemy.MonsterZone
				where ((c != null) ? c.Data : null) != null && !resultList.Contains(c) && !this.currentDestroyCardList.Contains(c) && c.IsMonsterInvincible() && c.IsFaceup() && this.CheckCanBeTargeted(c, canBeTarget, selfType)
				select c into card
				orderby card.Attack descending
				select card).ToList<ClientCard>();
			if (invincibleList.Count<ClientCard>() > 0)
			{
				resultList.AddRange(invincibleList);
			}
			List<ClientCard> enemyMonsters = (from c in base.Enemy.GetMonsters()
				where !this.currentDestroyCardList.Contains(c)
				select c into card
				orderby card.Attack descending
				select card).ToList<ClientCard>();
			if (enemyMonsters.Count<ClientCard>() > 0)
			{
				foreach (ClientCard target in enemyMonsters)
				{
					if ((target.HasType((CardType)8396992) || (target.HasType(CardType.Link) && target.LinkCount >= 2)) && !resultList.Contains(target) && this.CheckCanBeTargeted(target, canBeTarget, selfType))
					{
						resultList.Add(target);
					}
				}
			}
			List<ClientCard> spells = (from c in base.Enemy.GetSpells()
				where c.IsFaceup() && !this.currentDestroyCardList.Contains(c) && c.HasType((CardType)17694720) && this.CheckCanBeTargeted(c, canBeTarget, selfType) && !this.notToDestroySpellTrap.Contains(c.Id)
				select c).ToList<ClientCard>();
			if (spells.Count<ClientCard>() > 0 && !ignoreSpells)
			{
				resultList.AddRange(this.ShuffleList<ClientCard>(spells));
			}
			return resultList;
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x00077FBC File Offset: 0x000761BC
		public ClientCard GetBestEnemyMonster(bool onlyFaceup = false, bool canBeTarget = false, bool ignoreCurrentDestroy = false, CardType selfType = (CardType)0)
		{
			ClientCard card = this.GetProblematicEnemyMonster(0, canBeTarget, ignoreCurrentDestroy, selfType);
			if (card != null)
			{
				return card;
			}
			card = (from c in base.Enemy.MonsterZone
				where ((c != null) ? c.Data : null) != null && c.HasType(CardType.Monster) && c.IsFaceup() && this.CheckCanBeTargeted(c, canBeTarget, selfType) && (!ignoreCurrentDestroy || this.currentDestroyCardList.Contains(c))
				orderby c.Attack descending
				select c).FirstOrDefault<ClientCard>();
			if (card != null)
			{
				return card;
			}
			List<ClientCard> monsters = (from c in base.Enemy.GetMonsters()
				where !ignoreCurrentDestroy || this.currentDestroyCardList.Contains(c)
				select c).ToList<ClientCard>();
			if (monsters.Count<ClientCard>() > 0 && !onlyFaceup)
			{
				return this.ShuffleList<ClientCard>(monsters)[0];
			}
			return null;
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x00078094 File Offset: 0x00076294
		public List<ClientCard> GetDangerousCardinEnemyGrave(bool onlyMonster = false)
		{
			List<ClientCard> list = base.Enemy.Graveyard.GetMatchingCards((ClientCard card) => (!onlyMonster || card.IsMonster()) && (card.HasSetcode(283) || card.HasSetcode(219) || card.HasSetcode(413))).ToList<ClientCard>();
			List<int> dangerMonsterIdList = new List<int> { 99937011, 63542003, 9411399, 28954097, 30680659 };
			list.AddRange(base.Enemy.Graveyard.GetMatchingCards((ClientCard card) => dangerMonsterIdList.Contains(card.Id)));
			return list;
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x00078134 File Offset: 0x00076334
		public int GetEmptyMainMonsterZoneCount()
		{
			int remainCount = 0;
			for (int idx = 0; idx < 5; idx++)
			{
				if (base.Bot.MonsterZone[idx] == null)
				{
					remainCount++;
				}
			}
			return remainCount;
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x00078164 File Offset: 0x00076364
		public List<ClientCard> GetNormalEnemyTargetList(bool canBeTarget = true, bool ignoreCurrentDestroy = false, CardType selfType = (CardType)0)
		{
			List<ClientCard> targetList = this.GetProblematicEnemyCardList(canBeTarget, false, selfType);
			List<ClientCard> enemyMonster = (from card in base.Enemy.GetMonsters()
				where card.IsFaceup() && !targetList.Contains(card) && (!ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(card))
				select card).ToList<ClientCard>();
			enemyMonster.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			enemyMonster.Reverse();
			targetList.AddRange(enemyMonster);
			targetList.AddRange(this.ShuffleList<ClientCard>((from card in base.Enemy.GetSpells()
				where (!ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(card)) && this.enemySetThisTurn.Contains(card)
				select card).ToList<ClientCard>()));
			targetList.AddRange(this.ShuffleList<ClientCard>((from card in base.Enemy.GetSpells()
				where (!ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(card)) && !this.enemySetThisTurn.Contains(card)
				select card).ToList<ClientCard>()));
			targetList.AddRange(this.ShuffleList<ClientCard>((from card in base.Enemy.GetMonsters()
				where card.IsFacedown() && (!ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(card))
				select card).ToList<ClientCard>()));
			return targetList;
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x00078278 File Offset: 0x00076478
		public List<ClientCard> GetMonsterListForTargetNegate(bool canBeTarget = false, CardType selfType = (CardType)0)
		{
			List<ClientCard> resultList = new List<ClientCard>();
			if (this.CheckWhetherNegated(true, false, (CardType)0))
			{
				return resultList;
			}
			ClientCard target = base.Enemy.MonsterZone.FirstOrDefault((ClientCard card) => ((card != null) ? card.Data : null) != null && card.IsMonsterShouldBeDisabledBeforeItUseEffect() && card.IsFaceup() && !card.IsShouldNotBeTarget() && this.CheckCanBeTargeted(card, canBeTarget, selfType) && !this.currentNegateMonsterList.Contains(card));
			if (target != null)
			{
				resultList.Add(target);
			}
			foreach (ClientCard chainingCard in base.Duel.CurrentChain)
			{
				if (chainingCard.Location == CardLocation.MonsterZone && chainingCard.Controller == 1 && !chainingCard.IsDisabled() && this.CheckCanBeTargeted(chainingCard, canBeTarget, selfType) && !this.currentNegateMonsterList.Contains(chainingCard))
				{
					resultList.Add(chainingCard);
				}
			}
			return resultList;
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x00078364 File Offset: 0x00076564
		public int GetMaterialAttack(List<ClientCard> materials)
		{
			if (base.Util.IsTurn1OrMain2())
			{
				return 0;
			}
			int result = 0;
			foreach (ClientCard material in materials)
			{
				if (material.IsAttack() || !this.summonThisTurn.Contains(material))
				{
					result += material.Attack;
				}
			}
			return result;
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x000783DC File Offset: 0x000765DC
		public int GetBotCurrentTotalAttack(List<ClientCard> exceptList = null)
		{
			if (base.Util.IsTurn1OrMain2())
			{
				return 0;
			}
			int result = 0;
			foreach (ClientCard monster in base.Bot.GetMonsters())
			{
				if ((exceptList == null || !exceptList.Contains(monster)) && (monster.IsAttack() || !this.summonThisTurn.Contains(monster)))
				{
					result += monster.Attack;
				}
			}
			return result;
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x0007846C File Offset: 0x0007666C
		public List<ClientCard> GetCanBeUsedForLinkMaterial(bool useAdvancedMonster = false, Func<ClientCard, bool> exceptRule = null)
		{
			List<ClientCard> list = (from card in base.Bot.GetMonsters()
				where !card.IsFacedown() && (exceptRule == null || !exceptRule(card)) && (!card.IsCode(71607202) || !this.summonThisTurn.Contains(card)) && (!card.IsCode(2347656) || card.IsDisabled() || !this.Bot.HasInSpellZoneOrInGraveyard(92714517)) && ((!card.IsCode(22850702) && !card.IsCode(81497285)) || useAdvancedMonster || (!card.IsAttack() && this.summonThisTurn.Contains(card)))
				select card).ToList<ClientCard>();
			list.Sort(new Comparison<ClientCard>(this.CompareUsableAttack));
			return list;
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x000784C8 File Offset: 0x000766C8
		public bool CheckCanDirectAttack()
		{
			return base.Enemy.GetMonsterCount() == 0 && !this.activatedCardIdList.Contains(29301450) && base.Duel.Turn > 1 && base.Duel.Player == 0 && base.Duel.Phase < DuelPhase.Main2;
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x00078523 File Offset: 0x00076723
		public int CheckCalledbytheGrave(int id)
		{
			if (base.DefaultCheckWhetherCardIdIsNegated(id))
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x00078534 File Offset: 0x00076734
		public bool CheckCanBeTargeted(ClientCard card, bool canBeTarget, CardType selfType)
		{
			if (card == null)
			{
				return true;
			}
			if (canBeTarget)
			{
				if (card.IsShouldNotBeTarget())
				{
					return false;
				}
				if ((selfType & CardType.Monster) > (CardType)0 && card.IsShouldNotBeMonsterTarget())
				{
					return false;
				}
				if ((selfType & CardType.Spell) > (CardType)0 && card.IsShouldNotBeSpellTrapTarget())
				{
					return false;
				}
				if ((selfType & CardType.Trap) > (CardType)0 && (card.IsShouldNotBeSpellTrapTarget() || (!card.IsDisabled() && this.notToBeTrapTargetList.Contains(card.Id))))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x000785A0 File Offset: 0x000767A0
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

		// Token: 0x06001509 RID: 5385 RVA: 0x000785DC File Offset: 0x000767DC
		public int CheckRemainInDeck(params int[] ids)
		{
			int sumResult = 0;
			foreach (int id in ids)
			{
				sumResult += this.CheckRemainInDeck(id);
			}
			return sumResult;
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x0007860C File Offset: 0x0007680C
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

		// Token: 0x0600150B RID: 5387 RVA: 0x00078748 File Offset: 0x00076948
		public bool CheckWhetherNegated(bool disablecheck = true, bool toFieldCheck = false, CardType type = (CardType)0)
		{
			if ((base.Card.IsSpell() || base.Card.IsTrap() || (type & CardType.Spell) == (CardType)0 || (type & CardType.Trap) == (CardType)0) && this.CheckSpellWillBeNegate(false, null))
			{
				return true;
			}
			if (this.CheckCalledbytheGrave(base.Card.Id) > 0)
			{
				return true;
			}
			if ((base.Card.IsMonster() || (type & CardType.Monster) == (CardType)0) && (toFieldCheck || base.Card.Location == CardLocation.MonsterZone))
			{
				if (((toFieldCheck && (type & CardType.Link) == (CardType)0) || base.Card.IsDefense()) && (base.Enemy.MonsterZone.Any((ClientCard card) => this.CheckNumber41(card)) || base.Bot.MonsterZone.Any((ClientCard card) => this.CheckNumber41(card))))
				{
					return true;
				}
				if (base.Enemy.HasInSpellZone(82732705, true, true))
				{
					return true;
				}
			}
			return disablecheck && base.Card.IsDisabled();
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x00037BCF File Offset: 0x00035DCF
		public bool CheckNumber41(ClientCard card)
		{
			return card != null && card.IsFaceup() && card.IsCode(90590303) && card.IsDefense() && !card.IsDisabled();
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x0007883C File Offset: 0x00076A3C
		public bool CheckWhetherWillbeRemoved()
		{
			if (this.dimensionShifterCount > 0)
			{
				return true;
			}
			foreach (int cardid in new List<int> { 94853057, 61528025, 30241314, 81674782, 48626373, 58481572 })
			{
				foreach (ClientField cf in new List<ClientField> { base.Bot, base.Enemy })
				{
					if (cf.HasInMonstersZone(cardid, true, false, true) || cf.HasInSpellZone(cardid, true, true))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x00078948 File Offset: 0x00076B48
		public bool CheckAtAdvantage()
		{
			return this.GetProblematicEnemyMonster(0, false, false, (CardType)0) == null && (base.Duel.Player == 0 || base.Bot.GetMonsterCount() > 0);
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x00078974 File Offset: 0x00076B74
		public bool CheckShouldNoMoreSpSummon(bool isLabrynth = true)
		{
			if (!this.CheckAtAdvantage() || !this.enemyActivateMaxxC || (base.Duel.Turn != 1 && base.Duel.Phase < DuelPhase.Main2))
			{
				return false;
			}
			if (!isLabrynth)
			{
				return true;
			}
			if (this.cooclockAffected)
			{
				return base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && card.HasSetcode(382)) || (base.Duel.Player == 0 && !this.summoned) || this.setTrapThisTurn.Count<ClientCard>() == 0;
			}
			return true;
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x00078A24 File Offset: 0x00076C24
		public bool CheckLastChainShouldNegated()
		{
			ClientCard lastcard = base.Util.GetLastChainCard();
			return lastcard != null && lastcard.Controller == 1 && (!lastcard.IsMonster() || !lastcard.HasSetcode(74) || base.Duel.Phase != DuelPhase.Standby) && !this.notToNegateIdList.Contains(lastcard.Id);
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x00078A84 File Offset: 0x00076C84
		public bool CheckChainContainEnemyMaxxC()
		{
			foreach (ClientCard card in base.Duel.CurrentChain)
			{
				if (card.Controller == 1 && card.IsCode(23434538))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x00078AEC File Offset: 0x00076CEC
		public bool CheckBigWelcomeCanSpSummon(int cardId)
		{
			return base.Bot.HasInHandOrInGraveyard(cardId) || this.CheckRemainInDeck(cardId) > 0;
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x00078B08 File Offset: 0x00076D08
		public int CompareUsableAttack(ClientCard cardA, ClientCard cardB)
		{
			if (cardA == null && cardB == null)
			{
				return 0;
			}
			if (cardA == null)
			{
				return -1;
			}
			if (cardB == null)
			{
				return 1;
			}
			int powerA = ((cardA.IsDefense() && this.summonThisTurn.Contains(cardA)) ? 0 : cardA.Attack);
			int powerB = ((cardB.IsDefense() && this.summonThisTurn.Contains(cardB)) ? 0 : cardB.Attack);
			if (powerA < powerB)
			{
				return -1;
			}
			if (powerA == powerB)
			{
				return CardContainer.CompareCardLevel(cardA, cardB);
			}
			return 1;
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x00078B7C File Offset: 0x00076D7C
		public override IList<ClientCard> OnSelectCard(IList<ClientCard> cards, int min, int max, int hint, bool cancelable)
		{
			ClientCard currentSolvingChain = base.Duel.GetCurrentSolvingChainCard();
			if (currentSolvingChain != null)
			{
				if (currentSolvingChain.Controller == 1 && currentSolvingChain.IsCode(15693423))
				{
					Logger.DebugWriteLine("=== Evenly Matched activated.");
					List<ClientCard> banishList = new List<ClientCard>();
					List<ClientCard> list = (from card in base.Bot.GetMonsters()
						where !card.HasType(CardType.Token)
						select card).ToList<ClientCard>();
					List<ClientCard> faceDownMonsters = list.Where((ClientCard card) => card.IsFacedown()).ToList<ClientCard>();
					banishList.AddRange(faceDownMonsters);
					List<ClientCard> notImportantMonster = list.Where((ClientCard card) => !banishList.Contains(card) && ((card.HasType((CardType)75505728) && this.Bot.HasInExtra(card.Id)) || this.CheckRemainInDeck(card.Id) > 0)).ToList<ClientCard>();
					notImportantMonster.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					banishList.AddRange(notImportantMonster);
					List<ClientCard> faceUpSpells = (from c in base.Bot.GetSpells()
						where c.IsFaceup()
						select c).ToList<ClientCard>();
					banishList.AddRange(this.ShuffleList<ClientCard>(faceUpSpells));
					List<ClientCard> faceDownSpells = (from c in base.Bot.GetSpells()
						where c.IsFacedown()
						select c).ToList<ClientCard>();
					banishList.AddRange(this.ShuffleList<ClientCard>(faceDownSpells));
					List<ClientCard> importantMonster = list.Where((ClientCard card) => !banishList.Contains(card) && !card.IsCode(2347656) && ((card.HasType((CardType)75505728) && !this.Bot.HasInExtra(card.Id)) || this.CheckRemainInDeck(card.Id) == 0)).ToList<ClientCard>();
					importantMonster.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					banishList.AddRange(importantMonster);
					List<ClientCard> lovelyList = list.Where((ClientCard card) => !banishList.Contains(card) && card.IsCode(2347656)).ToList<ClientCard>();
					lovelyList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					banishList.AddRange(lovelyList);
					return base.Util.CheckSelectCount(banishList, cards, min, max);
				}
				if (currentSolvingChain.IsCode(81497285) && min == 1 && max == 1 && hint == 510)
				{
					using (SortedDictionary<int, Func<bool>>.Enumerator enumerator = new SortedDictionary<int, Func<bool>>
					{
						{
							83326048,
							new Func<bool>(this.DimensionalBarrierActivate)
						},
						{
							30748475,
							new Func<bool>(this.DestructiveDarumaKarmaCannonSetCheck)
						},
						{
							10045474,
							new Func<bool>(this.InfiniteImpermanenceSetCheck)
						}
					}.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							KeyValuePair<int, Func<bool>> pair = enumerator.Current;
							ClientCard target = cards.FirstOrDefault((ClientCard card) => card.IsCode(pair.Key));
							if (target != null && pair.Value())
							{
								this.SelectSTPlace(null, true, null);
								return base.Util.CheckSelectCount(new List<ClientCard> { target }, cards, min, max);
							}
						}
					}
					ClientCard rollback = cards.FirstOrDefault((ClientCard card) => card.IsCode(6351147));
					if (rollback != null)
					{
						bool haveUnchainSoul = false;
						if (!this.activatedCardIdList.Contains(41165831))
						{
							haveUnchainSoul |= base.Bot.HasInHand(41165831);
							bool flag = haveUnchainSoul;
							bool flag2;
							if (base.Duel.Player == 0 && base.Duel.Phase <= DuelPhase.Main2 && base.Bot.HasInExtra(24269961) && !this.activatedCardIdList.Contains(24269961) && (this.CheckRemainInDeck(41165831) > 0 || base.Bot.HasInGraveyard(41165831)))
							{
								flag2 = (from card in base.Bot.GetMonsters()
									where card.IsFaceup() && card.HasRace(CardRace.Fiend) && card.Level <= 4
									select card).Count<ClientCard>() >= 2;
							}
							else
							{
								flag2 = false;
							}
							haveUnchainSoul = flag || flag2;
						}
						bool haveAriane = false;
						if (!this.activatedCardIdList.Contains(75730490) && base.Duel.Player == 0 && base.Duel.Phase <= DuelPhase.Main2)
						{
							haveAriane |= base.Bot.HasInMonstersZone(75730490, false, false, false);
							haveAriane |= base.Bot.HasInHand(75730490) && !this.summoned;
							haveAriane |= base.Bot.GetSpells().Any((ClientCard card) => card.IsFacedown() && ((card.IsCode(5380979) && !this.activatedCardIdList.Contains(5380979) && (this.cooclockAffected || !this.setTrapThisTurn.Contains(card))) || (card.IsCode(92714517) && !this.activatedCardIdList.Contains(92714517) && (this.cooclockAffected || !this.setTrapThisTurn.Contains(card)))));
						}
						if (haveUnchainSoul || haveAriane)
						{
							return base.Util.CheckSelectCount(new List<ClientCard> { rollback }, cards, min, max);
						}
					}
					SortedDictionary<int, ClientCard> sortedDictionary = new SortedDictionary<int, ClientCard>();
					sortedDictionary.Add(92714517, cards.FirstOrDefault((ClientCard card) => card.IsCode(92714517)));
					sortedDictionary.Add(5380979, cards.FirstOrDefault((ClientCard card) => card.IsCode(5380979)));
					SortedDictionary<int, ClientCard> welcomeCheck = sortedDictionary;
					List<int> list2 = new List<int>();
					list2.Add(92714517);
					list2.Add(5380979);
					using (SortedDictionary<int, ClientCard>.Enumerator enumerator2 = welcomeCheck.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							KeyValuePair<int, ClientCard> checkPair2 = enumerator2.Current;
							if (checkPair2.Value != null && !base.Bot.HasInHand(checkPair2.Key) && !base.Bot.HasInGraveyard(checkPair2.Key) && !base.Bot.GetSpells().Any((ClientCard card) => card.IsCode(checkPair2.Key) && card.IsFacedown()))
							{
								this.SelectSTPlace(null, true, null);
								return base.Util.CheckSelectCount(new List<ClientCard> { checkPair2.Value }, cards, min, max);
							}
						}
					}
					if (welcomeCheck[92714517] != null && !base.Bot.HasInHand(92714517))
					{
						if (!base.Bot.GetSpells().Any((ClientCard card) => card.IsCode(92714517) && card.IsFacedown()))
						{
							this.SelectSTPlace(null, true, null);
							return base.Util.CheckSelectCount(new List<ClientCard> { welcomeCheck[92714517] }, cards, min, max);
						}
					}
					using (List<int>.Enumerator enumerator3 = new List<int> { 10045474, 83326048, 30748475, 92714517, 6351147, 5380979 }.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							int checkId8 = enumerator3.Current;
							ClientCard checkCard = cards.FirstOrDefault((ClientCard card) => card.IsCode(checkId8));
							if (checkCard != null)
							{
								this.SelectSTPlace(null, true, null);
								return base.Util.CheckSelectCount(new List<ClientCard> { checkCard }, cards, min, max);
							}
						}
					}
				}
				if (currentSolvingChain.IsCode(5380979))
				{
					this.banSpSummonExceptFiendCount = 2;
				}
				if (currentSolvingChain.IsCode(5380979) || (currentSolvingChain.IsCode(6351147) && this.rollbackCopyCardId == 5380979))
				{
					Logger.DebugWriteLine("rewrite welcome's select.");
					List<ClientCard> selection = new List<ClientCard>();
					ClientCard ariane = this.GetWelcomeOrBigWelcomeTarget(cards, 75730490);
					if (ariane != null)
					{
						if (!this.summonInChainList.Any((ClientCard card) => card.IsCode(75730490)) && ((base.Duel.Player == 0 && base.Duel.Phase <= DuelPhase.Main2) || (base.Duel.Player == 1 && base.Duel.Phase >= DuelPhase.Main2)) && base.Bot.HasInHandOrInSpellZone(6351147))
						{
							selection.Add(ariane);
						}
					}
					ClientCard arianna = this.GetWelcomeOrBigWelcomeTarget(cards, 1225009);
					if (arianna != null)
					{
						if (!this.summonInChainList.Any((ClientCard card) => card.IsCode(1225009)) && (!this.activatedCardIdList.Contains(1225009) && !this.CheckWhetherNegated(true, true, CardType.Monster)))
						{
							bool flag3;
							if (!this.activatedCardIdList.Contains(92714517))
							{
								if (!base.Bot.HasInGraveyard(92714517))
								{
									flag3 = !base.Bot.GetSpells().Any((ClientCard card) => card.IsFacedown() && card.IsCode(92714517));
								}
								else
								{
									flag3 = false;
								}
							}
							else
							{
								flag3 = true;
							}
							if (flag3 | (!this.activatedCardIdList.Contains(92714517) && (this.CheckBigWelcomeCanSpSummon(2347656) || base.Bot.HasInMonstersZone(2347656, true, false, true)) && base.Bot.GetSpells().Any((ClientCard card) => card.IsFacedown() && card.IsCode(92714517) && (!this.setTrapThisTurn.Contains(card) || this.cooclockAffected))) | (!base.Bot.HasInMonstersZone(2347656, true, false, true) && !this.CheckBigWelcomeCanSpSummon(2347656)))
							{
								selection.Add(arianna);
							}
						}
					}
					ClientCard arias = this.GetWelcomeOrBigWelcomeTarget(cards, 73602965);
					if (arias != null)
					{
						if (!this.summonInChainList.Any((ClientCard card) => card.IsCode(73602965)) && !base.Bot.HasInHandOrHasInMonstersZone(73602965) && (!this.activatedCardIdList.Contains(73602965) && !this.CheckWhetherNegated(true, true, CardType.Monster)) && base.Bot.HasInHand(2347656))
						{
							selection.Add(arias);
						}
					}
					ClientCard lovely = this.GetWelcomeOrBigWelcomeTarget(cards, 2347656);
					if (lovely != null)
					{
						if (!this.summonInChainList.Any((ClientCard card) => card.IsCode(2347656)) && base.Bot.HasInSpellZoneOrInGraveyard(92714517) && !this.activatedCardIdList.Contains(92714517))
						{
							selection.Add(lovely);
						}
					}
					ClientCard lady = this.GetWelcomeOrBigWelcomeTarget(cards, 81497285);
					if (lady != null && base.Bot.HasInSpellZoneOrInGraveyard(92714517) && !this.activatedCardIdList.Contains(92714517))
					{
						if (!base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && card.HasRace(CardRace.Fiend) && card.Level >= 8 && !card.HasType((CardType)75497472)))
						{
							selection.Add(lady);
						}
					}
					bool attackFlag = this.CheckCanDirectAttack();
					bool defenseFlag = base.Bot.UnderAttack && base.Bot.GetMonsterCount() == 0;
					if (attackFlag || defenseFlag)
					{
						ClientCard bestPowerMonster = null;
						int bestPower = -1;
						foreach (ClientCard target2 in cards)
						{
							NamedCard cardData = NamedCard.Get(target2.Id);
							if (cardData != null)
							{
								int power = (attackFlag ? cardData.Attack : Math.Max(cardData.Attack, cardData.Defense));
								if (bestPowerMonster == null || power > bestPower)
								{
									bestPowerMonster = target2;
									bestPower = power;
								}
							}
						}
						if (defenseFlag || (this.GetBotCurrentTotalAttack(null) < base.Enemy.LifePoints && this.GetBotCurrentTotalAttack(null) + bestPower >= base.Enemy.LifePoints))
						{
							ClientCard realTarget = this.GetWelcomeOrBigWelcomeTarget(cards, bestPowerMonster.Id);
							if (realTarget != null)
							{
								selection.Add(realTarget);
							}
						}
					}
					foreach (int checkId7 in new List<int> { 74018812, 37629703, 2511, 73602965 })
					{
						if (!base.Bot.HasInHandOrInMonstersZoneOrInGraveyard(checkId7))
						{
							ClientCard target3 = this.GetWelcomeOrBigWelcomeTarget(cards, checkId7);
							if (target3 != null)
							{
								selection.Add(target3);
							}
						}
					}
					foreach (int checkId2 in new List<int> { 81497285, 74018812, 37629703, 2511, 73602965, 75730490, 1225009 })
					{
						ClientCard target4 = this.GetWelcomeOrBigWelcomeTarget(cards, checkId2);
						if (target4 != null && !selection.Contains(target4))
						{
							selection.Add(target4);
						}
					}
					if (selection.Count<ClientCard>() > 0)
					{
						return base.Util.CheckSelectCount(selection, cards, min, max);
					}
				}
				bool flag4 = currentSolvingChain.IsCode(1225009) && hint == 506;
				bool bigwelcomeSoving = currentSolvingChain.IsCode(92714517) || (currentSolvingChain.IsCode(6351147) && this.rollbackCopyCardId == 92714517);
				if (flag4 | (bigwelcomeSoving && hint == 509 && base.Bot.GetMonsterCount() == 0))
				{
					Logger.DebugWriteLine("rewrite search.");
					new List<ClientCard>();
					List<int> furnitureCheckIdList = new List<int> { 74018812, 2511, 37629703 };
					ClientCard bigWelcome = this.GetWelcomeOrBigWelcomeTarget(cards, 92714517);
					ClientCard welcome = this.GetWelcomeOrBigWelcomeTarget(cards, 5380979);
					ClientCard arianna2 = this.GetWelcomeOrBigWelcomeTarget(cards, 1225009);
					if (base.Duel.Player == 0 && base.Duel.Phase <= DuelPhase.Main2)
					{
						if (!this.summoned && !this.activatedCardIdList.Contains(1225009) && !this.CheckWhetherNegated(true, true, CardType.Monster) && this.CheckCalledbytheGrave(1225009) == 0 && arianna2 != null && !base.Bot.HasInHand(1225009))
						{
							return base.Util.CheckSelectCount(new List<ClientCard> { arianna2 }, cards, min, max);
						}
						if (!this.CheckShouldNoMoreSpSummon(true))
						{
							if (bigWelcome != null && !this.activatedCardIdList.Contains(73602965) && base.Bot.HasInHandOrHasInMonstersZone(73602965))
							{
								return base.Util.CheckSelectCount(new List<ClientCard> { bigWelcome }, cards, min, max);
							}
							bool flag5;
							if (this.cooclockAffected || base.Bot.HasInHand(2511))
							{
								flag5 = base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && card.HasSetcode(382));
							}
							else
							{
								flag5 = false;
							}
							if (flag5 && !base.Bot.HasInHandOrInSpellZone(92714517) && !this.activatedCardIdList.Contains(92714517))
							{
								return base.Util.CheckSelectCount(new List<ClientCard> { this.AriannaSearchWelcomeTrap(cards, 92714517) }, cards, min, max);
							}
						}
					}
					ClientCard arias2 = null;
					ClientCard cooclock = null;
					if (!this.activatedCardIdList.Contains(73602965) && this.CheckRemainInDeck(73602965) > 0 && !base.Bot.HasInHand(73602965))
					{
						arias2 = this.GetWelcomeOrBigWelcomeTarget(cards, 73602965);
					}
					if (!this.activatedCardIdList.Contains(2511) && this.CheckRemainInDeck(2511) > 0 && !base.Bot.HasInHand(2511))
					{
						cooclock = this.GetWelcomeOrBigWelcomeTarget(cards, 2511);
					}
					if (arias2 != null || cooclock != null)
					{
						using (SortedDictionary<int, Func<bool>>.Enumerator enumerator = new SortedDictionary<int, Func<bool>>
						{
							{
								92714517,
								new Func<bool>(this.BigWelcomeLabrynthSetCheck)
							},
							{
								83326048,
								new Func<bool>(this.DimensionalBarrierActivate)
							},
							{
								30748475,
								new Func<bool>(this.DestructiveDarumaKarmaCannonSetCheck)
							},
							{
								5380979,
								new Func<bool>(this.WelcomeLabrynthSetCheck)
							}
						}.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								KeyValuePair<int, Func<bool>> checkPair = enumerator.Current;
								if (!base.Bot.GetSpells().Any((ClientCard card) => card.IsFacedown() && !this.setTrapThisTurn.Contains(card) && card.IsCode(checkPair.Key)) && !this.activatedCardIdList.Contains(checkPair.Key))
								{
									if (base.Bot.GetSpells().Any((ClientCard card) => card.IsFacedown() && this.setTrapThisTurn.Contains(card) && card.IsCode(checkPair.Key)) && cooclock != null)
									{
										return base.Util.CheckSelectCount(new List<ClientCard> { cooclock }, cards, min, max);
									}
									if (base.Bot.HasInHand(checkPair.Key) && checkPair.Value())
									{
										if (arias2 != null)
										{
											return base.Util.CheckSelectCount(new List<ClientCard> { arias2 }, cards, min, max);
										}
										if (base.Duel.Player == 0 && base.Duel.Phase <= DuelPhase.Main2 && cooclock != null)
										{
											return base.Util.CheckSelectCount(new List<ClientCard> { cooclock }, cards, min, max);
										}
									}
								}
							}
						}
					}
					bool lackUnimportantCost = !base.Bot.GetSpells().Any((ClientCard card) => card.IsFacedown() && card.IsCode(new int[] { 5380979, 92714517 }));
					if (lackUnimportantCost)
					{
						List<ClientCard> handCost = base.Bot.Hand.Where((ClientCard card) => card != base.Card).ToList<ClientCard>();
						bool flag6 = lackUnimportantCost;
						bool flag7;
						if (handCost.Count<ClientCard>() <= 2)
						{
							flag7 = !handCost.Any((ClientCard card) => !card.IsCode(new int[] { 23434538, 14558127, 2511 }));
						}
						else
						{
							flag7 = false;
						}
						lackUnimportantCost = flag6 && flag7;
					}
					if (!lackUnimportantCost && cooclock != null && bigWelcome != null)
					{
						foreach (int furnitureId in furnitureCheckIdList)
						{
							if (furnitureId != 2511 && this.CheckCalledbytheGrave(furnitureId) == 0 && !this.activatedCardIdList.Contains(furnitureId) && base.Bot.HasInHand(furnitureId))
							{
								return base.Util.CheckSelectCount(new List<ClientCard> { cooclock }, cards, min, max);
							}
						}
					}
					if (base.Duel.Player == 0 && base.Duel.Phase <= DuelPhase.Main2)
					{
						if (!lackUnimportantCost)
						{
							foreach (int checkId3 in furnitureCheckIdList)
							{
								ClientCard furniture3 = this.GetWelcomeOrBigWelcomeTarget(cards, checkId3);
								if (!base.Bot.HasInHandOrInMonstersZoneOrInGraveyard(checkId3) && furniture3 != null && (checkId3 != 2511 || base.Enemy.GetMonsterCount() <= 0 || base.Bot.HasInHandOrInMonstersZoneOrInGraveyard(37629703)))
								{
									return base.Util.CheckSelectCount(new List<ClientCard> { furniture3 }, cards, min, max);
								}
							}
						}
						if (bigWelcome != null)
						{
							bool flag8;
							if (!base.Bot.HasInMonstersZone(2347656, true, false, true) && !base.Bot.HasInHandOrInSpellZone(92714517))
							{
								flag8 = cards.Any((ClientCard c) => c.IsCode(2347656));
							}
							else
							{
								flag8 = false;
							}
							if (flag8 | (base.Bot.HasInMonstersZone(2347656, true, false, true) && !base.Bot.HasInHandOrInSpellZoneOrInGraveyard(92714517)))
							{
								return base.Util.CheckSelectCount(new List<ClientCard> { this.AriannaSearchWelcomeTrap(cards, 92714517) }, cards, min, max);
							}
						}
						if (welcome != null && base.Bot.HasInHandOrInSpellZone(92714517) && !base.Bot.HasInHandOrInSpellZone(5380979))
						{
							return base.Util.CheckSelectCount(new List<ClientCard> { this.AriannaSearchWelcomeTrap(cards, 5380979) }, cards, min, max);
						}
					}
					if (base.Duel.Player == 1 && (base.Duel.Phase <= DuelPhase.Main1 || base.Duel.Phase == DuelPhase.Main2) && !this.activatedCardIdList.Contains(92714517) && !this.activatedCardIdList.Contains(73602965) && !base.Bot.HasInSpellZone(92714517, false, false))
					{
						if (base.Bot.HasInHand(92714517) && !base.Bot.HasInHandOrHasInMonstersZone(73602965) && arias2 != null)
						{
							return base.Util.CheckSelectCount(new List<ClientCard> { arias2 }, cards, min, max);
						}
						if (base.Bot.HasInHand(73602965) && !base.Bot.HasInHandOrHasInMonstersZone(92714517) && bigWelcome != null)
						{
							return base.Util.CheckSelectCount(new List<ClientCard> { bigWelcome }, cards, min, max);
						}
					}
					ClientCard lady2 = this.GetWelcomeOrBigWelcomeTarget(cards, 81497285);
					bool flag9;
					if (base.Duel.Player == 0)
					{
						if (base.Bot.Hand.Any((ClientCard card) => card.Type == 4))
						{
							flag9 = base.Duel.Phase <= DuelPhase.Main2;
							goto IL_160A;
						}
					}
					flag9 = false;
					IL_160A:
					bool haveTrap = flag9;
					haveTrap |= base.Bot.GetSpells().Any((ClientCard card) => card.IsFacedown() && card.Type == 4);
					if (!base.Bot.HasInHandOrHasInMonstersZone(81497285) && !this.activatedCardIdList.Contains(81497285) && haveTrap && lady2 != null)
					{
						return base.Util.CheckSelectCount(new List<ClientCard> { lady2 }, cards, min, max);
					}
					if (!this.activatedCardIdList.Contains(1225009) && !this.CheckWhetherNegated(true, true, CardType.Monster) && this.CheckCalledbytheGrave(1225009) == 0 && arianna2 != null && !base.Bot.HasInHand(1225009))
					{
						return base.Util.CheckSelectCount(new List<ClientCard> { arianna2 }, cards, min, max);
					}
					if (!lackUnimportantCost)
					{
						foreach (int checkId4 in furnitureCheckIdList)
						{
							ClientCard furniture2 = this.GetWelcomeOrBigWelcomeTarget(cards, checkId4);
							if (!base.Bot.HasInHandOrInMonstersZoneOrInGraveyard(checkId4) && furniture2 != null && (checkId4 != 2511 || base.Enemy.GetMonsterCount() <= 0 || base.Bot.HasInHandOrInMonstersZoneOrInGraveyard(37629703)))
							{
								return base.Util.CheckSelectCount(new List<ClientCard> { furniture2 }, cards, min, max);
							}
						}
					}
					List<int> uniqueCheckIdList = new List<int> { 92714517, 74018812, 2511, 37629703, 81497285, 73602965, 75730490, 5380979 };
					foreach (int checkId5 in uniqueCheckIdList)
					{
						ClientCard targetCard = this.GetWelcomeOrBigWelcomeTarget(cards, checkId5);
						if (!base.Bot.HasInMonstersZone(checkId5, false, false, false) && !base.Bot.HasInHandOrInSpellZone(checkId5) && targetCard != null)
						{
							if (checkId5 == 92714517 || checkId5 == 5380979)
							{
								return base.Util.CheckSelectCount(new List<ClientCard> { this.AriannaSearchWelcomeTrap(cards, checkId5) }, cards, min, max);
							}
							return base.Util.CheckSelectCount(new List<ClientCard> { targetCard }, cards, min, max);
						}
					}
					foreach (int checkId6 in uniqueCheckIdList)
					{
						ClientCard targetCard2 = this.GetWelcomeOrBigWelcomeTarget(cards, checkId6);
						if (this.CheckRemainInDeck(checkId6) > 0)
						{
							if (checkId6 == 92714517 || checkId6 == 5380979)
							{
								return base.Util.CheckSelectCount(new List<ClientCard> { this.AriannaSearchWelcomeTrap(cards, checkId6) }, cards, min, max);
							}
							return base.Util.CheckSelectCount(new List<ClientCard> { targetCard2 }, cards, min, max);
						}
					}
				}
				if (bigwelcomeSoving && hint == 509)
				{
					bool activateTimingFlag = base.Duel.Phase > DuelPhase.Main2 || (base.Card.IsCode(73602965) && (base.CurrentTiming & 4) > 0);
					bool flag10 = (this.GetProblematicEnemyCardList(false, false, (CardType)0).Count<ClientCard>() > 0) | (this.activatedCardIdList.Contains(1225009) && activateTimingFlag);
					bool flag11;
					if (base.Bot.UnderAttack)
					{
						ClientCard battlingMonster = base.Bot.BattlingMonster;
						int num = ((battlingMonster != null) ? battlingMonster.GetDefensePower() : 0);
						ClientCard battlingMonster2 = base.Enemy.BattlingMonster;
						if (num <= ((battlingMonster2 != null) ? battlingMonster2.GetDefensePower() : 0))
						{
							flag11 = base.Duel.LastChainPlayer != 0;
							goto IL_1A04;
						}
					}
					flag11 = false;
					IL_1A04:
					if ((flag10 || flag11) | (base.Duel.Turn == 1 && base.Duel.Player == 0 && !this.activatedCardIdList.Contains(2347657)) | (base.Duel.Turn == 1 && base.Enemy.GetMonsterCount() == 0 && base.Enemy.GetSpellCount() == 0 && base.Enemy.Hand.Count > 0 && (base.CurrentTiming & 4) > 0))
					{
						if (cards.Any((ClientCard c) => c.IsCode(2347656)) && !this.activatedCardIdList.Contains(2347657))
						{
							return base.Util.CheckSelectCount(new List<ClientCard> { this.GetWelcomeOrBigWelcomeTarget(cards, 2347656) }, cards, min, max);
						}
					}
					if (cards.Any((ClientCard c) => c.IsCode(1225009)) && !this.activatedCardIdList.Contains(1225009) && !base.Bot.HasInMonstersZone(1225009, false, false, false))
					{
						return base.Util.CheckSelectCount(new List<ClientCard> { this.GetWelcomeOrBigWelcomeTarget(cards, 1225009) }, cards, min, max);
					}
					if (cards.Any((ClientCard c) => c.IsCode(2347656)) && !this.activatedCardIdList.Contains(2347657))
					{
						return base.Util.CheckSelectCount(new List<ClientCard> { this.GetWelcomeOrBigWelcomeTarget(cards, 2347656) }, cards, min, max);
					}
					if (cards.Any((ClientCard c) => c.IsCode(1225009)) && !this.activatedCardIdList.Contains(1225009) && !this.chainSummoningIdList.Contains(1225009))
					{
						return base.Util.CheckSelectCount(new List<ClientCard> { this.GetWelcomeOrBigWelcomeTarget(cards, 1225009) }, cards, min, max);
					}
					if (cards.Any((ClientCard c) => c.IsCode(81497285)) && base.Duel.Turn > 1 && base.Duel.Phase < DuelPhase.Main2 && base.Duel.Player == 0 && base.Enemy.GetMonsterCount() == 0)
					{
						return base.Util.CheckSelectCount(new List<ClientCard> { this.GetWelcomeOrBigWelcomeTarget(cards, 81497285) }, cards, min, max);
					}
					using (List<int>.Enumerator enumerator3 = new List<int> { 74018812, 2511, 37629703, 73602965 }.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							int furniture = enumerator3.Current;
							if (cards.Any((ClientCard c) => c.IsCode(furniture)) && !base.Bot.HasInHandOrInMonstersZoneOrInGraveyard(furniture))
							{
								return base.Util.CheckSelectCount(new List<ClientCard> { this.GetWelcomeOrBigWelcomeTarget(cards, furniture) }, cards, min, max);
							}
						}
					}
					using (List<int>.Enumerator enumerator3 = new List<int> { 75730490, 81497285, 1225009, 74018812, 2511, 37629703, 73602965 }.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							int checkId9 = enumerator3.Current;
							if (cards.Any((ClientCard c) => c.IsCode(checkId9)))
							{
								return base.Util.CheckSelectCount(new List<ClientCard> { this.GetWelcomeOrBigWelcomeTarget(cards, checkId9) }, cards, min, max);
							}
						}
					}
					Logger.DebugWriteLine("[warning] call BigWelcomeSpSummon with no select.");
				}
				if (!bigwelcomeSoving || hint != 505)
				{
					goto IL_22EF;
				}
				if (this.bigwelcomeEscaseTarget != null && cards.Contains(this.bigwelcomeEscaseTarget))
				{
					return base.Util.CheckSelectCount(new List<ClientCard> { this.bigwelcomeEscaseTarget }, cards, min, max);
				}
				ClientCard cooclock2 = cards.FirstOrDefault((ClientCard c) => c.IsCode(2511));
				bool flag12;
				if (this.CheckRemainInDeck(new int[] { 5380979, 92714517 }) > 0)
				{
					if (!base.Bot.HasInHandOrHasInMonstersZone(new List<int> { 37629703, 74018812 }))
					{
						if (!this.summonInChainList.Any((ClientCard c) => c.IsCode(1225009)))
						{
							flag12 = base.Duel.Player == 0 && !this.summoned && !this.activatedCardIdList.Contains(1225009) && base.Bot.HasInHand(1225009);
							goto IL_1F67;
						}
					}
					flag12 = true;
				}
				else
				{
					flag12 = false;
				}
				IL_1F67:
				bool canSearchWelcome = flag12;
				if (cooclock2 != null && (this.setTrapThisTurn.Count<ClientCard>() > 0 || (base.Duel.Turn == 1 && ((!this.activatedCardIdList.Contains(81497285) && base.Bot.HasInHandOrHasInMonstersZone(81497285)) || canSearchWelcome)) || (base.Duel.Turn == 0 && canSearchWelcome)))
				{
					return base.Util.CheckSelectCount(new List<ClientCard> { cooclock2 }, cards, min, max);
				}
				ClientCard defenseLady = cards.FirstOrDefault((ClientCard c) => c.IsDefense() && c.IsCode(81497285));
				ClientCard attackLady = cards.FirstOrDefault((ClientCard c) => c.IsAttack() && c.IsCode(81497285));
				if (base.Bot.GetMonsters().Any((ClientCard card) => (base.Duel.Player == 1 || card.IsDefense()) && card.IsCode(81497285)) && (!this.activatedCardIdList.Contains(81497285) || this.activatedCardIdList.Contains(81497286)))
				{
					if (defenseLady != null)
					{
						return base.Util.CheckSelectCount(new List<ClientCard> { defenseLady }, cards, min, max);
					}
					if (attackLady != null)
					{
						return base.Util.CheckSelectCount(new List<ClientCard> { attackLady }, cards, min, max);
					}
				}
				if (this.summonInChainList.Any((ClientCard c) => c.IsCode(2347656)))
				{
					using (List<int>.Enumerator enumerator3 = new List<int> { 23434538, 1225009, 14558127, 2511, 81497285, 37629703, 74018812, 73602965, 41165831, 75730490 }.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							int checkId = enumerator3.Current;
							ClientCard returnTarget = cards.FirstOrDefault((ClientCard c) => c.IsCode(checkId));
							if (returnTarget != null)
							{
								return base.Util.CheckSelectCount(new List<ClientCard> { returnTarget }, cards, min, max);
							}
						}
					}
					return base.Util.CheckSelectCount(cards.OrderBy((ClientCard card) => card.Attack).ToList<ClientCard>(), cards, min, max);
				}
				if (cards.Count<ClientCard>() == 1)
				{
					return base.Util.CheckSelectCount(cards.OrderBy((ClientCard card) => card.Attack).ToList<ClientCard>(), cards, min, max);
				}
				ClientCard ariannaNotSummon = cards.FirstOrDefault((ClientCard c) => c.IsCode(1225009) && !this.summonInChainList.Contains(c));
				if (ariannaNotSummon != null)
				{
					return base.Util.CheckSelectCount(new List<ClientCard> { ariannaNotSummon }, cards, min, max);
				}
				ClientCard fieldTarget = (from card in base.Bot.GetMonsters()
					where !card.IsCode(2347656)
					orderby card.Attack
					select card).FirstOrDefault<ClientCard>();
				if (fieldTarget != null)
				{
					return base.Util.CheckSelectCount(new List<ClientCard> { fieldTarget }, cards, min, max);
				}
			}
			IL_22EF:
			return base.OnSelectCard(cards, min, max, hint, cancelable);
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x0007AFF4 File Offset: 0x000791F4
		public ClientCard GetWelcomeOrBigWelcomeTarget(IList<ClientCard> cards, int cardId)
		{
			ClientCard graveTarget = cards.FirstOrDefault((ClientCard card) => card.IsCode(cardId) && card.Location == CardLocation.Grave);
			if (graveTarget != null)
			{
				return graveTarget;
			}
			ClientCard deckTarget = cards.FirstOrDefault((ClientCard card) => card.IsCode(cardId) && card.Location == CardLocation.Deck);
			if (deckTarget != null)
			{
				return deckTarget;
			}
			ClientCard handTarget = cards.FirstOrDefault((ClientCard card) => card.IsCode(cardId) && card.Location == CardLocation.Hand);
			if (handTarget != null)
			{
				return handTarget;
			}
			return null;
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x0007B058 File Offset: 0x00079258
		public ClientCard AriannaSearchWelcomeTrap(IList<ClientCard> cards, int welcomeId)
		{
			if (base.Bot.HasInHand(new List<int> { 2347656, 6351147, 73602965 }))
			{
				List<int> checkIdList = new List<int> { 74018812, 37629703 };
				foreach (int checkId in checkIdList)
				{
					ClientCard targetCard = this.GetWelcomeOrBigWelcomeTarget(cards, checkId);
					if (targetCard != null && !base.Bot.HasInHandOrInMonstersZoneOrInGraveyard(checkId) && this.CheckCalledbytheGrave(checkId) == 0 && !this.activatedCardIdList.Contains(checkId))
					{
						return targetCard;
					}
				}
				foreach (int checkId2 in checkIdList)
				{
					ClientCard targetCard2 = this.GetWelcomeOrBigWelcomeTarget(cards, checkId2);
					if (targetCard2 != null && this.CheckCalledbytheGrave(checkId2) == 0 && !this.activatedCardIdList.Contains(checkId2))
					{
						return targetCard2;
					}
				}
			}
			return this.GetWelcomeOrBigWelcomeTarget(cards, welcomeId);
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x0000763C File Offset: 0x0000583C
		public override bool OnSelectHand()
		{
			return true;
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x0007B198 File Offset: 0x00079398
		public override bool OnSelectMonsterSummonOrSet(ClientCard card)
		{
			return (card.Attack <= 0 || !this.CheckCanDirectAttack()) && (card.Attack <= 1000 || base.OnSelectMonsterSummonOrSet(card));
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x0007B1C4 File Offset: 0x000793C4
		public override int OnSelectPlace(int cardId, int player, CardLocation location, int available)
		{
			if (player == 0 && location == CardLocation.MonsterZone)
			{
				if (cardId == 94259633)
				{
					return base.OnSelectPlace(cardId, player, location, available);
				}
				if (cardId == 24269961 || cardId == 93084621)
				{
					if (base.Bot.MonsterZone[0] != null && base.Bot.MonsterZone[2] != null && (64 & available) != 0)
					{
						return 64;
					}
					if (base.Bot.MonsterZone[2] != null && base.Bot.MonsterZone[4] != null && (32 & available) != 0)
					{
						return 32;
					}
				}
				if (cardId == 71607202 || cardId == 67680512)
				{
					if (base.Bot.MonsterZone[1] != null && (64 & available) != 0)
					{
						return 64;
					}
					if (base.Bot.MonsterZone[3] != null && (32 & available) != 0)
					{
						return 32;
					}
				}
				List<int> list = this.ShuffleList<int>(new List<int> { 0, 2, 4 });
				list.AddRange(this.ShuffleList<int>(new List<int> { 1, 3 }));
				list.AddRange(this.ShuffleList<int>(new List<int> { 5, 6 }));
				foreach (int zoneId in list)
				{
					int zone = (int)Math.Pow(2.0, (double)zoneId);
					if ((available & zone) != 0 && base.Bot.MonsterZone[zoneId] == null)
					{
						return zone;
					}
				}
			}
			return base.OnSelectPlace(cardId, player, location, available);
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x0007B364 File Offset: 0x00079564
		public override CardPosition OnSelectPosition(int cardId, IList<CardPosition> positions)
		{
			NamedCard cardData = NamedCard.Get(cardId);
			if (cardData != null)
			{
				if (base.Duel.Turn == 1 || base.Duel.Phase >= DuelPhase.Main2)
				{
					bool turnDefense = false;
					if (cardData.Attack <= cardData.Defense)
					{
						turnDefense = true;
					}
					if (turnDefense)
					{
						return CardPosition.FaceUpDefence;
					}
				}
				if (base.Duel.Player == 1 && (cardData.Defense >= cardData.Attack || base.Util.IsOneEnemyBetterThanValue(cardData.Attack, true)))
				{
					return CardPosition.FaceUpDefence;
				}
				int cardAttack = cardData.Attack;
				int bestBotAttack = Math.Max(base.Util.GetBestAttack(base.Bot), cardAttack);
				if (base.Util.IsAllEnemyBetterThanValue(bestBotAttack, true))
				{
					return CardPosition.FaceUpDefence;
				}
			}
			return base.OnSelectPosition(cardId, positions);
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x0007B420 File Offset: 0x00079620
		public override int OnSelectOption(IList<int> options)
		{
			if (options.Count<int>() == 2 && options.Contains(1190) && options.Contains(1152))
			{
				if ((base.Duel.Player != 0 || base.Duel.Phase > DuelPhase.Main2) && !base.Bot.HasInHand(2511))
				{
					if (base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && card.HasSetcode(382)) && !this.activatedCardIdList.Contains(2511) && !this.CheckWhetherWillbeRemoved() && (this.activatedCardIdList.Contains(92714517) || base.Bot.GetSpells().All((ClientCard card) => this.setTrapThisTurn.Contains(card) || !card.IsCode(92714517))))
					{
						if (this.setTrapThisTurn.Any((ClientCard card) => card.IsFacedown() && card.IsCode(new int[] { 92714517, 83326048, 10045474, 30748475 })))
						{
							return options.IndexOf(1190);
						}
					}
				}
				if (!this.enemyActivateMaxxC)
				{
					return options.IndexOf(1152);
				}
				if (this.activatedCardIdList.Contains(2511) && !this.CheckShouldNoMoreSpSummon(true))
				{
					return options.IndexOf(1152);
				}
				return options.IndexOf(1190);
			}
			else
			{
				if (!options.Contains(base.Util.GetStringId(2347656, 3)) || !options.Contains(base.Util.GetStringId(2347656, 4)))
				{
					if (options.IndexOf(1057) >= 0 || options.IndexOf(1056) >= 0 || options.IndexOf(1063) >= 0 || options.IndexOf(1073) >= 0 || options.IndexOf(1074) >= 0)
					{
						Dictionary<int, Func<bool>> dictionary = new Dictionary<int, Func<bool>>();
						dictionary.Add(1057, new Func<bool>(this.DimensionalBarrierForRitual));
						dictionary.Add(1056, new Func<bool>(this.DimensionalBarrierForFusion));
						dictionary.Add(1063, new Func<bool>(this.DimensionalBarrierForSynchro));
						dictionary.Add(1073, new Func<bool>(this.DimensionalBarrierForXyz));
						dictionary.Add(1074, new Func<bool>(this.DimensionalBarrierForPendulum));
						this.dimensionBarrierAnnouncing = true;
						foreach (KeyValuePair<int, Func<bool>> checkPair in dictionary)
						{
							if (options.Contains(checkPair.Key) && checkPair.Value())
							{
								this.dimensionBarrierAnnouncing = false;
								this.dimensionalBarrierAnnouced.Add(checkPair.Key);
								return options.IndexOf(checkPair.Key);
							}
						}
						this.dimensionBarrierAnnouncing = false;
						List<ClientCard> enemyMonsterList = new List<ClientCard>(base.Enemy.GetMonsters());
						enemyMonsterList.AddRange(base.Enemy.GetGraveyardMonsters());
						Dictionary<int, bool> dictionary2 = new Dictionary<int, bool>();
						dictionary2.Add(1057, enemyMonsterList.Any((ClientCard card) => card.HasType(CardType.Ritual)));
						dictionary2.Add(1056, enemyMonsterList.Any((ClientCard card) => card.HasType(CardType.Fusion)));
						dictionary2.Add(1063, enemyMonsterList.Any((ClientCard card) => card.HasType(CardType.Synchro)));
						dictionary2.Add(1073, enemyMonsterList.Any((ClientCard card) => card.HasType(CardType.Xyz)));
						dictionary2.Add(1074, enemyMonsterList.Any((ClientCard card) => card.HasType(CardType.Pendulum)));
						foreach (KeyValuePair<int, bool> checkPair2 in dictionary2)
						{
							if (options.Contains(checkPair2.Key) && checkPair2.Value)
							{
								this.dimensionBarrierAnnouncing = false;
								this.dimensionalBarrierAnnouced.Add(checkPair2.Key);
								return options.IndexOf(checkPair2.Key);
							}
						}
						foreach (int annouce in new List<int> { 1073, 1063, 1056, 1074, 1057 })
						{
							if (options.Contains(annouce))
							{
								return options.IndexOf(annouce);
							}
						}
					}
					if ((options.Contains(base.Util.GetStringId(1225009, 2)) || options.Contains(base.Util.GetStringId(75730490, 2))) && this.GetEmptyMainMonsterZoneCount() > this.chainSummoningIdList.Count<int>())
					{
						bool checkFlag = false;
						if (!this.activatedCardIdList.Contains(1225009) && base.Bot.HasInHand(1225009) && !this.CheckWhetherNegated(true, true, CardType.Monster) && !this.chainSummoningIdList.Contains(1225009))
						{
							checkFlag = true;
							base.AI.SelectCard(1225009);
						}
						if (!checkFlag)
						{
							foreach (int checkId in new List<int> { 2347656, 81497285 })
							{
								if (base.Bot.HasInHand(checkId))
								{
									checkFlag = true;
									base.AI.SelectCard(checkId);
									break;
								}
							}
						}
						if (!checkFlag && base.Duel.Player == 0 && base.Duel.Phase < DuelPhase.End)
						{
							if (this.GetCanBeUsedForLinkMaterial(true, (ClientCard card) => !card.HasRace(CardRace.Fiend)).Count<ClientCard>() + this.chainSummoningIdList.Count<int>() == 2)
							{
								ClientCard selected = null;
								int attack = 0;
								foreach (ClientCard hand in base.Bot.Hand)
								{
									NamedCard cardData = NamedCard.Get(hand.Id);
									if (cardData != null && cardData.Race == 8 && (selected == null || attack > hand.Attack))
									{
										selected = hand;
										attack = hand.Attack;
									}
								}
								if (selected != null)
								{
									checkFlag = true;
									base.AI.SelectCard(selected);
								}
							}
						}
						if (!checkFlag && this.CheckCanDirectAttack())
						{
							ClientCard selected2 = null;
							int attack2 = 0;
							foreach (ClientCard hand2 in base.Bot.Hand)
							{
								NamedCard cardData2 = NamedCard.Get(hand2.Id);
								if (cardData2 != null && cardData2.Race == 8 && (selected2 == null || attack2 < hand2.Attack))
								{
									selected2 = hand2;
									attack2 = hand2.Attack;
								}
							}
							if (selected2 != null)
							{
								checkFlag = true;
								base.AI.SelectCard(selected2);
							}
						}
						if (checkFlag)
						{
							if (options.Contains(base.Util.GetStringId(1225009, 2)))
							{
								return options.IndexOf(base.Util.GetStringId(1225009, 2));
							}
							if (options.Contains(base.Util.GetStringId(75730490, 2)))
							{
								return options.IndexOf(base.Util.GetStringId(75730490, 2));
							}
						}
					}
					if ((options.Contains(base.Util.GetStringId(1225009, 3)) || options.Contains(base.Util.GetStringId(75730490, 3))) && (!base.Util.ChainContainsCard(5380979) || base.Bot.GetSpellCountWithoutField() < 4))
					{
						foreach (int checkId2 in new List<int> { 92714517, 5380979, 10045474, 83326048, 30748475 })
						{
							if (base.Bot.HasInHand(checkId2) && (checkId2 == 10045474 || !base.Bot.HasInSpellZone(checkId2, false, false)))
							{
								base.AI.SelectCard(checkId2);
								if (options.Contains(base.Util.GetStringId(1225009, 3)))
								{
									return options.IndexOf(base.Util.GetStringId(1225009, 3));
								}
								if (options.Contains(base.Util.GetStringId(75730490, 3)))
								{
									return options.IndexOf(base.Util.GetStringId(75730490, 3));
								}
							}
						}
					}
					if (options.Contains(base.Util.GetStringId(1225009, 4)))
					{
						return options.IndexOf(base.Util.GetStringId(1225009, 4));
					}
					if (options.Contains(base.Util.GetStringId(75730490, 4)))
					{
						return options.IndexOf(base.Util.GetStringId(75730490, 4));
					}
					return base.OnSelectOption(options);
				}
				int botWorstAttack = 0;
				ClientCard botWorstMonster = base.Util.GetWorstBotMonster(true);
				if (botWorstMonster != null)
				{
					botWorstAttack = botWorstMonster.Attack;
				}
				List<ClientCard> targetList = this.GetProblematicEnemyCardList(false, false, (CardType)0);
				List<ClientCard> enemyMonster = (from card in base.Enemy.GetMonsters()
					where card.IsFaceup() && !targetList.Contains(card) && card.GetDefensePower() >= botWorstAttack && !this.currentDestroyCardList.Contains(card)
					select card).ToList<ClientCard>();
				enemyMonster.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				enemyMonster.Reverse();
				targetList.AddRange(enemyMonster);
				targetList.AddRange(this.ShuffleList<ClientCard>((from card in base.Enemy.GetSpells()
					where !this.currentDestroyCardList.Contains(card) && card.IsFacedown()
					select card).ToList<ClientCard>()));
				if (targetList.Count<ClientCard>() > 0)
				{
					this.currentDestroyCardList.Add(targetList[0]);
					base.AI.SelectCard(targetList);
					return options.IndexOf(base.Util.GetStringId(2347656, 4));
				}
				return options.IndexOf(base.Util.GetStringId(2347656, 3));
			}
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x0007BF60 File Offset: 0x0007A160
		public override bool OnSelectYesNo(int desc)
		{
			if (desc == 96)
			{
				Logger.DebugWriteLine("*** muckraker replace.");
				base.AI.SelectCard((from card in base.Bot.GetMonsters()
					where card.IsFaceup() && card.HasRace(CardRace.Fiend)
					orderby card.Attack
					select card).ToList<ClientCard>());
				return true;
			}
			return base.OnSelectYesNo(desc);
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x0007BFE8 File Offset: 0x0007A1E8
		public override void OnNewTurn()
		{
			if (base.Duel.Turn <= 1)
			{
				this.dimensionShifterCount = 0;
				this.enemySpSummonFromExLastTurn = 0;
				this.enemySpSummonFromExThisTurn = 0;
				this.banSpSummonExceptFiendCount = 0;
			}
			this.enemyActivateMaxxC = false;
			this.enemySpSummonFromExLastTurn = this.enemySpSummonFromExThisTurn;
			this.enemySpSummonFromExThisTurn = 0;
			this.rollbackCopyCardId = 0;
			if (this.dimensionShifterCount > 0)
			{
				this.dimensionShifterCount--;
			}
			if (this.banSpSummonExceptFiendCount > 0)
			{
				this.banSpSummonExceptFiendCount--;
			}
			this.infiniteImpermanenceList.Clear();
			this.summoned = false;
			this.cooclockAffected = false;
			this.activatedCardIdList.Clear();
			this.setTrapThisTurn.Clear();
			this.summonThisTurn.Clear();
			this.enemySetThisTurn.Clear();
			this.dimensionalBarrierAnnouced.Clear();
			this.summonInChainList.Clear();
			base.OnNewTurn();
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x0007C0D0 File Offset: 0x0007A2D0
		public override void OnChaining(int player, ClientCard card)
		{
			if (card == null)
			{
				return;
			}
			if (this.chainSummoningIdList.Count<int>() > 0)
			{
				Logger.DebugWriteLine("[Welcome] Summoning: " + string.Join<int>(",", this.chainSummoningIdList) + "\n");
			}
			if (player == 1 && card.IsCode(10045474))
			{
				if (this.enemyActivateInfiniteImpermanenceFromHand)
				{
					this.enemyActivateInfiniteImpermanenceFromHand = false;
				}
				else
				{
					for (int i = 0; i < 5; i++)
					{
						if (base.Enemy.SpellZone[i] == card)
						{
							this.infiniteImpermanenceList.Add(4 - i);
							break;
						}
					}
				}
			}
			base.OnChaining(player, card);
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x0007C16C File Offset: 0x0007A36C
		public override void OnChainSolved(int chainIndex)
		{
			ChainInfo currentCard = base.Duel.GetCurrentSolvingChainInfo();
			if (currentCard != null && !base.Duel.IsCurrentSolvingChainNegated())
			{
				if (currentCard.ActivatePlayer == 1)
				{
					if (currentCard.IsCode(23434538))
					{
						this.enemyActivateMaxxC = true;
					}
					if (currentCard.IsCode(91800273))
					{
						this.dimensionShifterCount = 2;
					}
				}
				if (currentCard.ActivatePlayer == 0 && currentCard.IsCode(2511))
				{
					this.cooclockAffected = true;
				}
			}
			base.OnChainSolved(chainIndex);
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x0007C1EC File Offset: 0x0007A3EC
		public override void OnChainEnd()
		{
			this.rollbackCopyCardId = 0;
			this.currentNegateMonsterList.Clear();
			this.currentDestroyCardList.Clear();
			this.escapeTargetList.Clear();
			this.chainSummoningIdList.Clear();
			this.summonInChainList.Clear();
			this.enemyActivateInfiniteImpermanenceFromHand = false;
			for (int idx = this.enemySetThisTurn.Count<ClientCard>() - 1; idx >= 0; idx--)
			{
				ClientCard checkTarget = this.enemySetThisTurn[idx];
				if (checkTarget == null || checkTarget.Location != CardLocation.SpellZone || checkTarget.HasPosition(CardPosition.FaceUp))
				{
					this.enemySetThisTurn.RemoveAt(idx);
				}
			}
			if (this.cooclockActivating)
			{
				this.cooclockActivating = false;
			}
			this.furnitureActivating = false;
			this.dimensionBarrierAnnouncing = false;
			this.bigwelcomeEscaseTarget = null;
			base.OnChainEnd();
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x0007C2B0 File Offset: 0x0007A4B0
		public override void OnMove(ClientCard card, int previousControler, int previousLocation, int currentControler, int currentLocation)
		{
			if (previousControler == 1)
			{
				if (previousLocation == 64 && currentLocation == 4)
				{
					this.enemySpSummonFromExThisTurn++;
				}
				if (card != null)
				{
					if (card.IsCode(10045474) && previousLocation == 2 && currentLocation == 8)
					{
						this.enemyActivateInfiniteImpermanenceFromHand = true;
					}
					if (card.Location == CardLocation.SpellZone && card.HasPosition(CardPosition.FaceDown))
					{
						this.enemySetThisTurn.Add(card);
					}
				}
			}
			if (card != null)
			{
				if (previousControler == 0)
				{
					if (previousLocation == 4 && currentLocation != 4)
					{
						if (this.summonThisTurn.Contains(card))
						{
							this.summonThisTurn.Remove(card);
						}
						if (this.summonInChainList.Contains(card))
						{
							this.summonInChainList.Remove(card);
						}
					}
					if (previousLocation == 8 && currentLocation != 8 && this.setTrapThisTurn.Contains(card))
					{
						this.setTrapThisTurn.Remove(card);
					}
				}
				if (currentControler == 0)
				{
					ClientCard currentSolvingChain = base.Duel.GetCurrentSolvingChainCard();
					if (currentLocation == 8 && (currentSolvingChain == null || !currentSolvingChain.IsCode(73602965)) && (card.HasType(CardType.Trap) || card.IsCode(new int[] { 5380979, 92714517 })))
					{
						Logger.DebugWriteLine("[setTrapThisTurn]set " + card.Name);
						this.setTrapThisTurn.Add(card);
					}
					if (currentLocation == 4)
					{
						this.summonThisTurn.Add(card);
						if (currentSolvingChain != null)
						{
							this.summonInChainList.Add(card);
						}
					}
				}
			}
			base.OnMove(card, previousControler, previousLocation, currentControler, currentLocation);
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x0007C424 File Offset: 0x0007A624
		public override BattlePhaseAction OnBattle(IList<ClientCard> attackers, IList<ClientCard> defenders)
		{
			if (attackers.Count<ClientCard>() > 0 && defenders.Count<ClientCard>() > 0)
			{
				List<ClientCard> sortedAttacker = attackers.OrderBy((ClientCard card) => card.Attack).ToList<ClientCard>();
				for (int i = 0; i < sortedAttacker.Count; i++)
				{
					ClientCard attacker = sortedAttacker[i];
					attacker.IsLastAttacker = i == sortedAttacker.Count - 1;
					BattlePhaseAction result = this.OnSelectAttackTarget(attacker, defenders);
					if (result != null)
					{
						return result;
					}
				}
			}
			return base.OnBattle(attackers, defenders);
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x0007C4B0 File Offset: 0x0007A6B0
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

		// Token: 0x06001524 RID: 5412 RVA: 0x0007C69C File Offset: 0x0007A89C
		public void ResetCooclockEffect(bool onlyCheck)
		{
			if (!onlyCheck && this.cooclockAffected && this.setTrapThisTurn.Contains(base.Card))
			{
				this.cooclockAffected = false;
				this.setTrapThisTurn.Remove(base.Card);
			}
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x0007C6D8 File Offset: 0x0007A8D8
		public bool LadyLabrynthOfTheSilverCastleFieldActivate()
		{
			if (base.Card.Location == CardLocation.MonsterZone && (base.Util.GetLastChainCard() == null || !base.Util.GetLastChainCard().IsCode(15693423)) && (!this.CheckWhetherNegated(true, false, (CardType)0) || base.Enemy.HasInMonstersZone(81497285, false, false, false)))
			{
				this.activatedCardIdList.Add(base.Card.Id + 1);
				return true;
			}
			return false;
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x0007C754 File Offset: 0x0007A954
		public bool LadyLabrynthOfTheSilverCastleHandActivate()
		{
			if (base.Card.Location == CardLocation.Hand)
			{
				if (this.CheckShouldNoMoreSpSummon(true) || base.Util.ChainContainsCard(15693423))
				{
					return false;
				}
				bool activateFlag = false;
				activateFlag |= this.CheckChainContainEnemyMaxxC();
				if (!activateFlag && this.GetEmptyMainMonsterZoneCount() + this.chainSummoningIdList.Count<int>() <= 0)
				{
					return false;
				}
				bool flag = activateFlag;
				bool flag2;
				if (this.cooclockAffected && this.setTrapThisTurn.Count<ClientCard>() > 0)
				{
					flag2 = !base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && card.HasSetcode(382));
				}
				else
				{
					flag2 = false;
				}
				activateFlag = flag || flag2;
				activateFlag |= base.Bot.GetSpells().Any((ClientCard card) => card.IsFacedown() && card.HasType(CardType.Trap) && !this.setTrapThisTurn.Contains(card)) && !base.Bot.HasInMonstersZone(base.Card.Id, true, false, true);
				activateFlag |= base.Duel.Player == 1 && base.Duel.Phase >= DuelPhase.End;
				activateFlag |= this.setTrapThisTurn.Count<ClientCard>() > 0 && base.Duel.Phase >= DuelPhase.End;
				activateFlag |= base.Bot.UnderAttack && base.Bot.GetMonsterCount() == 0 && !base.Util.ChainContainsCard(30748475);
				if (base.Bot.HasInExtra(24269961) && base.Duel.Player == 0 && base.Duel.Phase < DuelPhase.End)
				{
					List<ClientCard> materialList = this.GetCanBeUsedForLinkMaterial(true, (ClientCard card) => !card.HasRace(CardRace.Fiend));
					int materialCount = materialList.Count<ClientCard>();
					if (!this.activatedCardIdList.Contains(41165831))
					{
						if (base.Bot.GetSpells().Any((ClientCard card) => card.IsFacedown()) && (!this.activatedCardIdList.Contains(24269961) || base.Bot.HasInHand(41165831)))
						{
							materialCount++;
						}
					}
					if (materialCount != 2)
					{
						if (materialCount != 1)
						{
							goto IL_027A;
						}
						if (!materialList.Any((ClientCard card) => card.IsCode(24269961)))
						{
							goto IL_027A;
						}
					}
					if (base.Bot.HasInExtra(24269961))
					{
						goto IL_02A1;
					}
					IL_027A:
					if (!materialList.Any((ClientCard card) => card.HasSetcode(304)))
					{
						goto IL_02D8;
					}
					IL_02A1:
					activateFlag |= base.Enemy.GetMonsterCount() > 0 && base.Bot.HasInExtra(93084621);
					activateFlag |= base.Bot.HasInExtra(67680512);
				}
				IL_02D8:
				if (activateFlag)
				{
					this.activatedCardIdList.Add(base.Card.Id);
					this.chainSummoningIdList.Add(base.Card.Id);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x0007CA6C File Offset: 0x0007AC6C
		public bool LovelyLabrynthOfTheSilverCastleActivate()
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0))
			{
				return false;
			}
			if (base.ActivateDescription == base.Util.GetStringId(2347656, 0))
			{
				List<int> checkIdList = new List<int> { 92714517, 10045474, 83326048, 30748475, 5380979 };
				foreach (int checkId in checkIdList)
				{
					if (base.Bot.HasInGraveyard(checkId) && !base.Bot.HasInHandOrInSpellZone(checkId))
					{
						base.AI.SelectCard(checkId);
						this.activatedCardIdList.Add(base.Card.Id);
						return true;
					}
				}
				foreach (int checkId2 in checkIdList)
				{
					if (base.Bot.HasInGraveyard(checkId2))
					{
						base.AI.SelectCard(checkId2);
						this.activatedCardIdList.Add(base.Card.Id);
						return true;
					}
				}
				if (this.GetCanBeUsedForLinkMaterial(true, (ClientCard card) => !card.HasRace(CardRace.Fiend)).Count<ClientCard>() == 2 && !this.activatedCardIdList.Contains(24269961) && (base.Bot.HasInGraveyard(41165831) || this.CheckRemainInDeck(41165831) > 0))
				{
					this.activatedCardIdList.Add(base.Card.Id);
					return true;
				}
				return false;
			}
			if (base.Enemy.GetHandCount() == 0)
			{
				int botWorstAttack = 0;
				ClientCard botWorstMonster = base.Util.GetWorstBotMonster(true);
				if (botWorstMonster != null)
				{
					botWorstAttack = botWorstMonster.Attack;
				}
				List<ClientCard> targetList = this.GetProblematicEnemyCardList(false, false, (CardType)0);
				List<ClientCard> enemyMonster = (from card in base.Enemy.GetMonsters()
					where card.IsFaceup() && !targetList.Contains(card) && card.GetDefensePower() >= botWorstAttack && !this.currentDestroyCardList.Contains(card)
					select card).ToList<ClientCard>();
				enemyMonster.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				enemyMonster.Reverse();
				targetList.AddRange(enemyMonster);
				targetList.AddRange(this.ShuffleList<ClientCard>((from card in base.Enemy.GetSpells()
					where !this.currentDestroyCardList.Contains(card)
					select card).ToList<ClientCard>()));
				if (targetList.Count<ClientCard>() > 0)
				{
					this.currentDestroyCardList.Add(targetList[0]);
					base.AI.SelectCard(targetList);
					base.AI.SelectOption(1);
				}
				else
				{
					base.AI.SelectOption(0);
				}
			}
			this.activatedCardIdList.Add(base.Card.Id + 1);
			return true;
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x0007CD98 File Offset: 0x0007AF98
		public bool UnchainedSoulOfSharvaraActivate()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				this.activatedCardIdList.Add(base.Card.Id + 1);
				this.SelectSTPlace(null, false, null);
				return true;
			}
			if (base.Bot.HasInSpellZone(6351147, false, false) && this.GetEmptyMainMonsterZoneCount() > this.chainSummoningIdList.Count<int>() && !this.CheckWhetherWillbeRemoved() && !this.CheckShouldNoMoreSpSummon(false))
			{
				base.AI.SelectCard(6351147);
				this.activatedCardIdList.Add(base.Card.Id);
				return true;
			}
			if (base.Duel.LastChainPlayer == 1)
			{
				ClientCard chainCard = base.Util.GetLastChainCard();
				if (chainCard != null && chainCard.IsCode(this.targetNegateIdList) && base.Duel.LastChainTargets.Any((ClientCard card) => card.Controller == 0 && card.IsFaceup() && card.HasRace(CardRace.Fiend) && base.Duel.CurrentChain.Any((ClientCard chain) => chain == card) && !card.IsCode(new int[] { 67680512, 93084621 })))
				{
					this.escapeTargetList.AddRange(base.Duel.LastChainTargets);
					base.AI.SelectCard(base.Duel.LastChainTargets);
					this.activatedCardIdList.Add(base.Card.Id);
					return true;
				}
			}
			bool destroySpells = base.Duel.Player == 0 && this.GetEmptyMainMonsterZoneCount() > this.chainSummoningIdList.Count<int>() && base.Bot.GetMonsterCount() > 0 && base.CurrentTiming <= 0;
			if (destroySpells)
			{
				List<ClientCard> materialList = this.GetCanBeUsedForLinkMaterial(true, (ClientCard card) => !card.HasRace(CardRace.Fiend));
				destroySpells = this.CheckAtAdvantage() && !base.Bot.HasInMonstersZone(67680512, false, false, false) && base.Bot.HasInExtra(67680512) && materialList.Count<ClientCard>() == 1;
				if (base.Bot.HasInExtra(93084621) && !base.Bot.HasInMonstersZone(93084621, false, false, false) && !this.activatedCardIdList.Contains(93084621))
				{
					if ((from card in base.Enemy.GetMonsters()
						where card.IsFaceup()
						select card).Count<ClientCard>() > 0)
					{
						destroySpells |= materialList.Count<ClientCard>() == 2;
						bool flag = destroySpells;
						bool flag2;
						if (materialList.Count<ClientCard>() == 1)
						{
							flag2 = materialList.Any((ClientCard card) => card.HasType(CardType.Link) && card.LinkCount == 2);
						}
						else
						{
							flag2 = false;
						}
						destroySpells = flag || flag2;
					}
				}
			}
			destroySpells |= this.CheckCanDirectAttack() && this.GetBotCurrentTotalAttack(null) < base.Enemy.LifePoints && this.GetBotCurrentTotalAttack(null) + 2000 >= base.Enemy.LifePoints && this.GetEmptyMainMonsterZoneCount() > this.chainSummoningIdList.Count<int>();
			destroySpells |= base.Duel.Player == 1 && base.Duel.Phase == DuelPhase.Main1 && base.Bot.GetMonsterCount() == 0 && (base.CurrentTiming & 4) != 0 && base.Util.GetTotalAttackingMonsterAttack(1) >= base.Bot.LifePoints;
			if (destroySpells)
			{
				using (List<int>.Enumerator enumerator = new List<int> { 10045474, 6351147, 5380979, 83326048, 30748475, 92714517 }.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int checkId = enumerator.Current;
						ClientCard target = base.Bot.GetSpells().FirstOrDefault((ClientCard card) => card.IsFacedown() && card.IsCode(checkId));
						if (target != null)
						{
							base.AI.SelectCard(target);
							this.activatedCardIdList.Add(base.Card.Id);
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x0007D1A8 File Offset: 0x0007B3A8
		public bool AriasTheLabrynthButlerActivate()
		{
			if (base.Card.Location != CardLocation.Grave)
			{
				if (base.Util.ChainContainsCard(new int[] { 90448279, 15693423, 46772449 }))
				{
					return false;
				}
				if (base.Duel.CurrentChain.Any((ClientCard card) => card.Controller == 0 && card.IsCode(1225009)))
				{
					return false;
				}
				using (IEnumerator<KeyValuePair<int, Func<bool>>> enumerator = new SortedList<int, Func<bool>>
				{
					{
						92714517,
						new Func<bool>(this.BigWelcomeLabrynthSetCheck)
					},
					{
						5380979,
						new Func<bool>(this.WelcomeLabrynthSetCheck)
					},
					{
						30748475,
						new Func<bool>(this.DestructiveDarumaKarmaCannonSetCheck)
					},
					{
						83326048,
						new Func<bool>(this.DimensionalBarrierActivate)
					}
				}.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<int, Func<bool>> pair = enumerator.Current;
						ClientCard setTarget = base.Bot.Hand.FirstOrDefault((ClientCard card) => card.IsCode(pair.Key));
						if (setTarget != null && !this.activatedCardIdList.Contains(pair.Key) && pair.Value())
						{
							base.AI.SelectOption(1);
							base.AI.SelectCard(pair.Key);
							this.activatedCardIdList.Add(base.Card.Id);
							this.SelectSTPlace(setTarget, true, null);
							return true;
						}
					}
				}
				if (base.Bot.HasInHand(2347656) && (base.Duel.Player == 0 || (base.CurrentTiming & 4) != 0))
				{
					base.AI.SelectOption(0);
					base.AI.SelectCard(2347656);
					this.chainSummoningIdList.Add(2347656);
					this.activatedCardIdList.Add(base.Card.Id);
					return true;
				}
				if (!base.Bot.HasInHand(1225009) || this.activatedCardIdList.Contains(1225009) || this.CheckWhetherNegated(true, true, (CardType)0) || this.chainSummoningIdList.Contains(1225009))
				{
					return false;
				}
				bool searchFlag = false;
				if (base.Duel.Player == 1)
				{
					searchFlag |= (base.CurrentTiming & 4) != 0;
					searchFlag |= this.GetProblematicEnemyCardList(false, false, (CardType)0).Count<ClientCard>() > 0 && (base.Bot.HasInMonstersZoneOrInGraveyard(2347656) || this.CheckRemainInDeck(2347656) > 0) && !this.activatedCardIdList.Contains(2347657);
				}
				if (base.Duel.Player == 0)
				{
					searchFlag |= this.summoned && !this.CheckShouldNoMoreSpSummon(true);
				}
				if (searchFlag)
				{
					base.AI.SelectOption(0);
					base.AI.SelectCard(1225009);
					this.chainSummoningIdList.Add(1225009);
					this.activatedCardIdList.Add(base.Card.Id);
					return true;
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x0007D4EC File Offset: 0x0007B6EC
		public bool ArianeTheLabrynthServantSummon()
		{
			if (base.Duel.Turn > 1 && base.Enemy.GetMonsterCount() == 0)
			{
				this.summoned = true;
				return true;
			}
			if (!this.activatedCardIdList.Contains(base.Card.Id) && !this.CheckWhetherNegated(true, true, (CardType)0) && !this.CheckWhetherWillbeRemoved())
			{
				bool flag;
				if (!base.Bot.Hand.Any((ClientCard card) => card.Type == 4))
				{
					flag = base.Bot.GetSpells().Any((ClientCard card) => card.IsFacedown() && card.Type == 4);
				}
				else
				{
					flag = true;
				}
				if (flag && !this.CheckShouldNoMoreSpSummon(true))
				{
					this.summoned = true;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x0007D5C3 File Offset: 0x0007B7C3
		public bool ArianeTheLabrynthServantForRollbackSummon()
		{
			if (this.activatedCardIdList.Contains(base.Card.Id))
			{
				return false;
			}
			if (base.Bot.HasInHandOrInSpellZone(6351147) && !this.CheckWhetherWillbeRemoved())
			{
				this.summoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x0007D604 File Offset: 0x0007B804
		public bool ArianeTheLabrynthServantActivate()
		{
			if (base.ActivateDescription == base.Util.GetStringId(base.Card.Id, 0))
			{
				bool haveRollback = base.Bot.HasInHandOrInSpellZone(6351147);
				if (this.CheckWhetherNegated(true, false, (CardType)0) && !haveRollback)
				{
					return false;
				}
				if (this.CheckShouldNoMoreSpSummon(true))
				{
					if (haveRollback)
					{
						if (base.Bot.Graveyard.Any((ClientCard card) => card.IsCode(new int[] { 5380979, 92714517 })))
						{
							goto IL_0082;
						}
					}
					return false;
				}
				IL_0082:
				int specialSummonId = 0;
				if (!this.activatedCardIdList.Contains(1225009) && this.CheckRemainInDeck(1225009) > 0)
				{
					specialSummonId = 1225009;
				}
				if (specialSummonId == 0)
				{
					foreach (int checkId3 in new List<int> { 74018812, 37629703, 2511 })
					{
						if (!base.Bot.HasInHandOrInMonstersZoneOrInGraveyard(checkId3) && this.CheckRemainInDeck(checkId3) > 0)
						{
							specialSummonId = checkId3;
							break;
						}
					}
				}
				if (specialSummonId == 0)
				{
					List<int> checkIdList = new List<int>();
					if (base.Enemy.GetMonsterCount() == 0)
					{
						checkIdList.AddRange(new List<int> { 1225009, 37629703, 74018812, 2511 });
					}
					else
					{
						checkIdList.AddRange(new List<int> { 37629703, 74018812, 2511, 1225009 });
					}
					foreach (int checkId2 in checkIdList)
					{
						if (this.CheckRemainInDeck(checkId2) > 0)
						{
							specialSummonId = checkId2;
							break;
						}
					}
				}
				if (specialSummonId > 0)
				{
					bool costSelected = false;
					if (haveRollback)
					{
						base.AI.SelectCard(6351147);
						costSelected = true;
					}
					if (!costSelected)
					{
						ClientCard welcome = base.Bot.GetSpells().FirstOrDefault((ClientCard card) => card.IsCode(5380979));
						if (welcome != null)
						{
							base.AI.SelectCard(welcome);
							costSelected = true;
						}
					}
					List<ClientCard> costCheckList = base.Bot.Hand.Where((ClientCard card) => card.IsFacedown() && card.Type == 4).ToList<ClientCard>();
					costCheckList.AddRange((from card in base.Bot.GetSpells()
						where card.IsFacedown() && card.Type == 4
						select card).ToList<ClientCard>());
					if (!costSelected)
					{
						using (List<int>.Enumerator enumerator = new List<int> { 10045474, 5380979, 92714517 }.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								int checkId4 = enumerator.Current;
								ClientCard dumpCard = costCheckList.FirstOrDefault((ClientCard card) => card.IsCode(checkId4));
								if (costCheckList.Count((ClientCard card) => card.IsCode(checkId4)) > 1 && dumpCard != null)
								{
									base.AI.SelectCard(dumpCard);
									costSelected = true;
									break;
								}
							}
						}
					}
					if (!costSelected)
					{
						using (List<int>.Enumerator enumerator = new List<int> { 10045474, 83326048, 30748475, 5380979, 92714517 }.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								int checkId = enumerator.Current;
								ClientCard checkCard = costCheckList.FirstOrDefault((ClientCard card) => card.IsCode(checkId));
								if (checkCard != null)
								{
									base.AI.SelectCard(checkCard);
									costSelected = true;
									break;
								}
							}
							return false;
						}
						goto IL_0405;
					}
				}
				return false;
			}
			IL_0405:
			this.activatedCardIdList.Add(base.Card.Id + 1);
			return true;
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x0007DA68 File Offset: 0x0007BC68
		public bool AriannaTheLabrynthServantSummon()
		{
			if (!this.CheckWhetherNegated(true, true, (CardType)0) && !this.activatedCardIdList.Contains(base.Card.Id))
			{
				this.summoned = true;
				return true;
			}
			if (base.Duel.Turn > 1 && base.Duel.Player == 0 && base.Duel.Phase < DuelPhase.Main2 && base.Enemy.GetMonsterCount() == 0 && !base.Bot.HasInHand(75730490))
			{
				this.summoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x0007DAF7 File Offset: 0x0007BCF7
		public bool AriannaTheLabrynthServantActivate()
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0))
			{
				return false;
			}
			this.activatedCardIdList.Add(base.Card.Id);
			return true;
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x0007DB20 File Offset: 0x0007BD20
		public bool AshBlossomActivate()
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0) || !this.CheckLastChainShouldNegated())
			{
				return false;
			}
			if (base.Util.GetLastChainCard().IsCode(23434538))
			{
				return false;
			}
			if (base.DefaultAshBlossomAndJoyousSpring())
			{
				if (base.Util.GetLastChainCard().Location == CardLocation.MonsterZone)
				{
					this.currentNegateMonsterList.Add(base.Util.GetLastChainCard());
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x0007DB8F File Offset: 0x0007BD8F
		public bool MaxxCActivate()
		{
			return !this.CheckWhetherNegated(true, false, (CardType)0) && base.Duel.LastChainPlayer != 0 && base.DefaultMaxxC();
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x0007DBB4 File Offset: 0x0007BDB4
		public bool FurnitureSetWelcomeActivate()
		{
			if (this.furnitureActivating && (base.Card.Location == CardLocation.Hand || !base.DefaultOnBecomeTarget()))
			{
				return false;
			}
			if (base.Util.ChainContainsCard(new int[] { 90448279, 15693423, 46772449 }))
			{
				return false;
			}
			if (this.CheckWhetherNegated(true, false, (CardType)0))
			{
				return false;
			}
			if (base.Card.Location != CardLocation.Grave)
			{
				bool becomeTarget = base.Card.Location == CardLocation.MonsterZone && base.DefaultOnBecomeTarget() && !this.escapeTargetList.Contains(base.Card);
				bool lackUnimportantCost = base.Bot.GetSpells().Any((ClientCard card) => card.IsFacedown() && card.IsCode(new int[] { 5380979, 92714517 }));
				if (lackUnimportantCost)
				{
					List<ClientCard> handCost = base.Bot.Hand.Where((ClientCard card) => card != base.Card).ToList<ClientCard>();
					bool flag = lackUnimportantCost;
					bool flag2;
					if (handCost.Count<ClientCard>() <= 2)
					{
						flag2 = handCost.All((ClientCard card) => card.IsCode(new int[] { 23434538, 14558127 }));
					}
					else
					{
						flag2 = false;
					}
					lackUnimportantCost = flag && flag2;
				}
				bool activateFlag = becomeTarget;
				bool flag3;
				if (this.CheckRemainInDeck(92714517) > 0 && this.cooclockAffected && !this.activatedCardIdList.Contains(92714517) && (base.Bot.HasInMonstersZone(2347656, true, false, true) || this.CheckBigWelcomeCanSpSummon(2347656)) && !base.Bot.GetSpells().Any((ClientCard card) => card.IsFacedown() && card.IsCode(92714517) && !this.setTrapThisTurn.Contains(card)))
				{
					flag3 = base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && card.HasSetcode(382)) || (base.Bot.HasInGraveyard(2511) && !this.activatedCardIdList.Contains(2512)) || (base.Bot.HasInHand(81497285) && !this.activatedCardIdList.Contains(81497285));
				}
				else
				{
					flag3 = false;
				}
				if (flag3 && this.ShouldSetBigWelcome(true))
				{
					bool force = becomeTarget | (this.GetProblematicEnemyCardList(false, false, (CardType)0).Count<ClientCard>() > 0);
					ClientCard cost = this.FurnitureGetCost(force, null);
					if (cost != null)
					{
						base.AI.SelectCard(cost);
						base.AI.SelectNextCard(92714517);
						this.activatedCardIdList.Add(base.Card.Id);
						this.furnitureActivating = true;
						this.SelectSTPlace(null, true, null);
						return true;
					}
				}
				bool keepOnField = (this.cooclockActivating || this.cooclockAffected) && this.activatedCardIdList.Contains(2512) && base.Card.Location == CardLocation.MonsterZone && !base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && card != base.Card && card.HasSetcode(382)) && this.setTrapThisTurn.Count<ClientCard>() > 0;
				activateFlag |= base.Duel.Phase > DuelPhase.Main2 && !lackUnimportantCost && !keepOnField;
				activateFlag |= base.Bot.HasInGraveyard(new List<int> { 5380979, 92714517 }) && base.Bot.HasInHand(6351147) && !this.activatedCardIdList.Contains(6351147);
				if (base.Duel.CurrentChain.Any((ClientCard card) => card != null && card.Controller == 0 && card.IsCode(92714517) && card.Location == CardLocation.SpellZone) && (base.Bot.GetMonsterCount() != 1 || base.Card.Location != CardLocation.MonsterZone))
				{
					activateFlag |= !lackUnimportantCost && base.Bot.GetMonsters().Any((ClientCard card) => card != base.Card) && !base.Bot.HasInGraveyard(base.Card.Id) && !this.activatedCardIdList.Contains(base.Card.Id + 1);
				}
				activateFlag |= !base.Util.ChainContainPlayer(0) && base.Duel.Player == 1 && base.Bot.UnderAttack && base.Bot.GetMonsterCount() == 0 && base.Bot.HasInGraveyard(2511) && !this.activatedCardIdList.Contains(2512) && (!base.Bot.HasInHand(81497285) || this.activatedCardIdList.Contains(81497285));
				if (activateFlag)
				{
					ClientCard cost2 = this.FurnitureGetCost(becomeTarget, null);
					if (cost2 != null)
					{
						base.AI.SelectCard(cost2);
						this.activatedCardIdList.Add(base.Card.Id);
						this.furnitureActivating = true;
						if (base.Bot.GetSpells().Any((ClientCard card) => card.IsFacedown() && card.IsCode(92714517)) | (base.Bot.GetMonsterCount() == 0 && !base.Bot.HasInHandOrInSpellZone(5380979) && (!base.Bot.HasInGraveyard(2511) || this.activatedCardIdList.Contains(2512)) && ((base.Duel.Player == 0 && base.Duel.Phase > DuelPhase.Main2) || !base.Bot.Hand.Any((ClientCard card) => card != base.Card && card.Level <= 4))))
						{
							base.AI.SelectNextCard(5380979);
						}
						else
						{
							base.AI.SelectNextCard(92714517);
						}
						this.SelectSTPlace(null, true, null);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x0007E170 File Offset: 0x0007C370
		public ClientCard FurnitureGetCost(bool force = false, List<ClientCard> ignoreList = null)
		{
			if (ignoreList == null)
			{
				ignoreList = new List<ClientCard>();
			}
			using (List<int>.Enumerator enumerator = new List<int> { 6351147, 2347656, 73602965, 37629703, 74018812, 5380979 }.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int checkId2 = enumerator.Current;
					ClientCard cost = base.Bot.Hand.FirstOrDefault((ClientCard card) => !ignoreList.Contains(card) && card.IsCode(checkId2) && card != this.Card);
					if (cost != null)
					{
						return cost;
					}
				}
			}
			List<ClientCard> canCostHand = base.Bot.Hand.Where((ClientCard card) => !ignoreList.Contains(card)).ToList<ClientCard>();
			List<int> appearedCode = new List<int>(canCostHand.Count<ClientCard>());
			foreach (ClientCard hand in canCostHand)
			{
				if (!base.Duel.CurrentChain.Contains(hand))
				{
					if (appearedCode.Contains(hand.Id))
					{
						return hand;
					}
					appearedCode.Add(hand.Id);
				}
			}
			List<int> costIdList = new List<int> { 10045474, 83326048, 41165831, 53417695, 30748475, 2511, 75730490, 5380979, 49238328, 81497285 };
			if (force)
			{
				costIdList.AddRange(new List<int> { 1225009, 14558127, 92714517, 23434538 });
			}
			using (List<int>.Enumerator enumerator = costIdList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int checkId3 = enumerator.Current;
					ClientCard target = canCostHand.FirstOrDefault((ClientCard card) => !this.Duel.CurrentChain.Contains(card) && card.IsCode(checkId3) && !this.Duel.CurrentChain.Contains(card));
					if (target != null)
					{
						return target;
					}
				}
			}
			using (List<int>.Enumerator enumerator = costIdList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int checkId = enumerator.Current;
					ClientCard target2 = canCostHand.FirstOrDefault((ClientCard card) => card.IsCode(checkId) && !this.Duel.CurrentChain.Contains(card));
					if (target2 != null)
					{
						return target2;
					}
				}
			}
			return null;
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x0007E490 File Offset: 0x0007C690
		public bool ShouldSetBigWelcome(bool checkArianna = true)
		{
			if (this.CheckWhetherWillbeRemoved())
			{
				return false;
			}
			bool shouldTriggerBigWelcomeFlag = this.GetProblematicEnemyCardList(false, false, (CardType)0).Count<ClientCard>() > 0;
			shouldTriggerBigWelcomeFlag |= base.Duel.Player == 1 && base.Duel.Phase > DuelPhase.Main2;
			shouldTriggerBigWelcomeFlag |= base.Duel.Player == 1 && this.GetProblematicEnemyCardList(false, false, (CardType)0).Count<ClientCard>() == 0 && this.GetProblematicEnemyMonster(0, false, false, CardType.Monster) == null && base.Enemy.Hand.Count<ClientCard>() == 1;
			if (checkArianna)
			{
				shouldTriggerBigWelcomeFlag |= base.Duel.Player == 0 && !this.summoned && base.Bot.HasInHandOrHasInMonstersZone(1225009) && !this.activatedCardIdList.Contains(1225009);
			}
			return shouldTriggerBigWelcomeFlag | (base.Duel.Player == 0 && base.Duel.Phase <= DuelPhase.Main2);
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x0007E590 File Offset: 0x0007C790
		public bool LabrynthCooclockActivate()
		{
			if (base.Card.Location == CardLocation.Hand)
			{
				bool shouldTriggerBigWelcomeFlag = false;
				if (base.Bot.HasInMonstersZone(2347656, true, false, true) || this.CheckBigWelcomeCanSpSummon(2347656))
				{
					shouldTriggerBigWelcomeFlag |= this.ShouldSetBigWelcome(true);
				}
				shouldTriggerBigWelcomeFlag &= !this.activatedCardIdList.Contains(92714517);
				if (shouldTriggerBigWelcomeFlag && !base.Bot.GetSpells().Any((ClientCard card) => card.IsFacedown() && !this.setTrapThisTurn.Contains(card) && card.IsCode(92714517)))
				{
					bool flag;
					if (base.Duel.Player == 0 && base.Bot.HasInHand(92714517))
					{
						flag = base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && card.HasSetcode(382)) || (!this.summoned && base.Bot.Hand.Any((ClientCard card) => card != base.Card && card.HasType(CardType.Monster) && card.Level <= 4 && card.HasSetcode(382)));
					}
					else
					{
						flag = false;
					}
					bool haveBigWelcome = flag;
					if (this.CheckRemainInDeck(92714517) > 0)
					{
						using (List<int>.Enumerator enumerator = new List<int> { 37629703, 74018812 }.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								int checkId = enumerator.Current;
								if (!this.activatedCardIdList.Contains(checkId) && this.CheckCalledbytheGrave(checkId) <= 0 && ((base.Bot.HasInHand(checkId) && base.Bot.Hand.Count > 2) || (base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && !card.IsDisabled() && card.IsCode(checkId)) && base.Bot.Hand.Count > 1)))
								{
									haveBigWelcome = true;
									break;
								}
							}
						}
					}
					if (haveBigWelcome)
					{
						this.activatedCardIdList.Add(base.Card.Id);
						this.cooclockActivating = true;
						return true;
					}
				}
				bool haveLabrynth = base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && card.HasSetcode(382));
				bool flag2 = base.Duel.Player == 1 && base.Duel.Phase <= DuelPhase.Main2 && this.setTrapThisTurn.Any((ClientCard card) => !this.activatedCardIdList.Contains(card.Id)) && haveLabrynth;
				bool flag3;
				if (base.Duel.Player == 1 && this.activatedCardIdList.Contains(81497286))
				{
					flag3 = base.Bot.GetSpells().Any((ClientCard card) => card.IsFacedown() && card.Type == 4) || base.Util.ChainContainsCard(81497285);
				}
				else
				{
					flag3 = false;
				}
				if ((flag2 || flag3) | (this.setTrapThisTurn.Any((ClientCard card) => card.IsFacedown() && card.IsCode(92714517) && !this.activatedCardIdList.Contains(92714517)) && haveLabrynth) | (this.setTrapThisTurn.Any((ClientCard card) => card.IsFacedown() && card.IsCode(5380979) && !this.activatedCardIdList.Contains(5380979)) && haveLabrynth))
				{
					this.activatedCardIdList.Add(base.Card.Id);
					this.cooclockActivating = true;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x0007E8DC File Offset: 0x0007CADC
		public bool RecycleActivate()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				if (base.Card.IsCode(new int[] { 74018812, 73602965 }))
				{
					if (this.CheckShouldNoMoreSpSummon(true) || this.GetEmptyMainMonsterZoneCount() + this.chainSummoningIdList.Count<int>() <= 0)
					{
						return false;
					}
					this.chainSummoningIdList.Add(base.Card.Id);
				}
				if (base.Card.IsCode(5380979))
				{
					this.SelectSTPlace(base.Card, false, null);
				}
				this.activatedCardIdList.Add(base.Card.Id + 1);
				return true;
			}
			return false;
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x0007E990 File Offset: 0x0007CB90
		public bool ForLinkSummon()
		{
			if (base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && card.HasSetcode(304)))
			{
				return false;
			}
			if (base.Card.Level > 4)
			{
				return false;
			}
			if (this.CheckShouldNoMoreSpSummon(true))
			{
				return false;
			}
			if (!base.Bot.HasInExtra(24269961))
			{
				return false;
			}
			List<ClientCard> materialList = this.GetCanBeUsedForLinkMaterial(true, (ClientCard card) => !card.HasRace(CardRace.Fiend));
			int materialCount = materialList.Count<ClientCard>();
			if (!this.activatedCardIdList.Contains(41165831))
			{
				if (base.Bot.GetSpells().Any((ClientCard card) => card.IsFacedown()) && (!this.activatedCardIdList.Contains(24269961) || base.Bot.HasInHand(41165831)))
				{
					materialCount++;
				}
			}
			if (materialCount != 2)
			{
				if (materialCount == 1)
				{
					if (materialList.Any((ClientCard card) => card.IsCode(24269961)))
					{
						goto IL_012C;
					}
				}
				return false;
			}
			IL_012C:
			if (!base.Bot.HasInExtra(24269961))
			{
				if (!materialList.Any((ClientCard card) => card.HasSetcode(304)))
				{
					return false;
				}
			}
			if (!(false | (base.Enemy.GetMonsterCount() > 0 && base.Bot.HasInExtra(93084621)) | base.Bot.HasInExtra(67680512)))
			{
				return false;
			}
			NamedCard thisCardData = NamedCard.Get(base.Card.Id);
			if (thisCardData == null)
			{
				return false;
			}
			if (thisCardData.Race != 8)
			{
				return false;
			}
			foreach (ClientCard clientCard in base.Bot.Hand)
			{
				NamedCard compareCardData = NamedCard.Get(clientCard.Id);
				if (compareCardData != null && compareCardData.HasType(CardType.Monster) && compareCardData.Level <= 4 && compareCardData.Attack < thisCardData.Attack)
				{
					return false;
				}
			}
			this.summoned = true;
			return true;
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x0007EBE0 File Offset: 0x0007CDE0
		public bool ForSynchroSummon()
		{
			if (base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && card.HasSetcode(304)))
			{
				return false;
			}
			if (!base.Card.IsCode(new List<int> { 74018812, 75730490, 1225009 }))
			{
				return false;
			}
			if (this.CheckShouldNoMoreSpSummon(true))
			{
				return false;
			}
			if (!base.Bot.HasInExtra(22850702) || this.dimensionalBarrierAnnouced.Contains(1063))
			{
				return false;
			}
			if (this.GetProblematicEnemyCardList(true, false, CardType.Monster).Count<ClientCard>() > 0)
			{
				bool flag = !this.CheckWhetherNegated(true, true, CardType.Monster);
			}
			if (base.Card.IsCode(74018812))
			{
				if (!base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && !card.HasType((CardType)75497472) && card.Level == 8 && card.HasAttribute((CardAttribute)48)))
				{
					return false;
				}
				this.summoned = true;
				return true;
			}
			else
			{
				if (!base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && !card.HasType((CardType)75497472) && card.Level == 6 && card.HasAttribute((CardAttribute)48)))
				{
					return false;
				}
				this.summoned = true;
				return true;
			}
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x0007ED30 File Offset: 0x0007CF30
		public bool ForAnimaSummon()
		{
			if (this.banSpSummonExceptFiendCount > 0 || !base.Bot.HasInExtra(94259633))
			{
				return false;
			}
			if (this.CheckWhetherNegated(true, false, (CardType)0) || base.Duel.Turn == 1)
			{
				return false;
			}
			bool checkFlag = base.Bot.MonsterZone[1] == null && base.Enemy.MonsterZone[6] != null && base.Enemy.MonsterZone[6].HasType(CardType.Link) && base.Enemy.MonsterZone[6].HasLinkMarker(CardLinkMarker.Top);
			checkFlag |= base.Bot.MonsterZone[3] == null && base.Enemy.MonsterZone[5] != null && base.Enemy.MonsterZone[5].HasType(CardType.Link) && base.Enemy.MonsterZone[5].HasLinkMarker(CardLinkMarker.Top);
			if (base.Bot.GetMonstersExtraZoneCount() == 0)
			{
				checkFlag |= base.Enemy.MonsterZone[1] != null || base.Enemy.MonsterZone[3] != null;
			}
			return checkFlag;
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x0007EE50 File Offset: 0x0007D050
		public bool LabrynthForCooClockSummon()
		{
			if (!this.cooclockAffected)
			{
				return false;
			}
			if (base.Card.Level > 4 || !base.Card.HasSetcode(382))
			{
				return false;
			}
			if (base.Bot.Hand.Any((ClientCard card) => (card.IsCode(5380979) && !this.activatedCardIdList.Contains(5380979)) || (card.IsCode(92714517) && !this.activatedCardIdList.Contains(92714517))) | base.Bot.GetSpells().Any((ClientCard card) => card.IsFacedown() && this.setTrapThisTurn.Contains(card) && ((card.IsCode(5380979) && !this.activatedCardIdList.Contains(5380979)) || (card.IsCode(92714517) && !this.activatedCardIdList.Contains(92714517)))))
			{
				if (!base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && card.HasSetcode(382)))
				{
					int currentAttack = 0;
					NamedCard cardData = NamedCard.Get(base.Card.Id);
					if (cardData != null)
					{
						currentAttack = cardData.Attack;
					}
					foreach (ClientCard clientCard in base.Bot.Hand.Where((ClientCard card) => card.IsMonster() && card.Level <= 4 && card.HasSetcode(382)).ToList<ClientCard>())
					{
						cardData = NamedCard.Get(clientCard.Id);
						if (cardData != null && cardData.Attack < currentAttack)
						{
							return false;
						}
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x0007EFA4 File Offset: 0x0007D1A4
		public bool ForBigWelcomeSummon()
		{
			if (base.Bot.HasInSpellZone(92714517, false, false) && base.Bot.GetMonsterCount() == 0 && base.Card.Level <= 4)
			{
				this.summoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x0007EFDF File Offset: 0x0007D1DF
		public bool PotOfExtravaganceActivate()
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0))
			{
				return false;
			}
			this.SelectSTPlace(base.Card, true, null);
			this.activatedCardIdList.Add(base.Card.Id);
			base.AI.SelectOption(1);
			return true;
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x0007F01F File Offset: 0x0007D21F
		public bool WelcomeLabrynthActivate()
		{
			return this.WelcomeLabrynthActivateCheck(false, false);
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x0007F029 File Offset: 0x0007D229
		public bool WelcomeLabrynthActivateCopy()
		{
			return this.WelcomeLabrynthActivateCheck(true, false);
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x0007F033 File Offset: 0x0007D233
		public bool WelcomeLabrynthSetCheck()
		{
			return !this.CheckShouldNoMoreSpSummon(true) && this.WelcomeLabrynthActivateCheck(true, true);
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x0007F048 File Offset: 0x0007D248
		public bool WelcomeLabrynthActivateCheck(bool onlyCheck = false, bool noSelect = false)
		{
			if (base.Card.Location == CardLocation.SpellZone || onlyCheck)
			{
				if (this.GetEmptyMainMonsterZoneCount() == 0)
				{
					return false;
				}
				if (this.CheckShouldNoMoreSpSummon(true))
				{
					return false;
				}
				bool activateTimingFlag = base.Duel.Phase > DuelPhase.Main2 || (base.Card.IsCode(73602965) && (base.CurrentTiming & 4) > 0);
				bool becomeTarget = base.Card.Location == CardLocation.SpellZone && base.DefaultOnBecomeTarget();
				if (((base.Duel.Player == 0 && base.Duel.Phase <= DuelPhase.Main2) || (base.Duel.Player == 1 && activateTimingFlag)) && this.CheckRemainInDeck(75730490) > 0 && base.Bot.HasInHandOrInSpellZone(6351147) && !this.chainSummoningIdList.Contains(75730490))
				{
					if (!noSelect)
					{
						this.chainSummoningIdList.Add(75730490);
						this.activatedCardIdList.Add(base.Card.Id);
					}
					return true;
				}
				if (((!base.Bot.HasInSpellZoneOrInGraveyard(92714517) || (!base.Bot.HasInMonstersZone(2347656, true, false, true) && !this.CheckBigWelcomeCanSpSummon(2347656))) | (base.Duel.Player == 1 && activateTimingFlag) | (base.Duel.Player == 0)) && this.CheckRemainInDeck(1225009) > 0 && !this.activatedCardIdList.Contains(1225009) && !this.CheckWhetherNegated(true, true, CardType.Monster) && !this.chainSummoningIdList.Contains(1225009))
				{
					if (!noSelect)
					{
						this.chainSummoningIdList.Add(1225009);
						this.activatedCardIdList.Add(base.Card.Id);
					}
					return true;
				}
				if (base.Bot.HasInSpellZoneOrInGraveyard(92714517) && !this.activatedCardIdList.Contains(92714517) && base.Bot.HasInHand(2347656) && this.CheckRemainInDeck(73602965) > 0 && !this.chainSummoningIdList.Contains(73602965) && !base.Bot.HasInMonstersZone(73602965, true, false, true))
				{
					if (!noSelect)
					{
						this.chainSummoningIdList.Add(73602965);
						this.activatedCardIdList.Add(base.Card.Id);
					}
					return true;
				}
				if (becomeTarget | (base.Bot.UnderAttack && base.Bot.GetMonsterCount() == 0) | this.ShouldSetBigWelcome(false))
				{
					if (!noSelect)
					{
						if (base.Bot.HasInSpellZoneOrInGraveyard(92714517) && !this.activatedCardIdList.Contains(92714517) && this.CheckRemainInDeck(2347656) > 0 && !this.chainSummoningIdList.Contains(2347656))
						{
							this.chainSummoningIdList.Add(2347656);
						}
						else if (!this.activatedCardIdList.Contains(1225009) && this.CheckRemainInDeck(1225009) > 0 && !this.CheckWhetherNegated(true, true, CardType.Monster) && !this.chainSummoningIdList.Contains(1225009))
						{
							this.chainSummoningIdList.Add(1225009);
						}
						else
						{
							if (base.Bot.HasInGraveyard(92714517) && !this.activatedCardIdList.Contains(92714517))
							{
								if (!base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && card.HasRace(CardRace.Fiend) && card.Level >= 8) && this.CheckRemainInDeck(81497285) > 0 && !this.chainSummoningIdList.Contains(81497285))
								{
									this.chainSummoningIdList.Add(81497285);
									goto IL_04FE;
								}
							}
							int selectId = 0;
							foreach (int checkId in new List<int> { 74018812, 37629703, 2511 })
							{
								if (!base.Bot.HasInHandOrInMonstersZoneOrInGraveyard(checkId) && this.CheckRemainInDeck(checkId) > 0 && !this.chainSummoningIdList.Contains(checkId))
								{
									selectId = checkId;
									break;
								}
							}
							List<int> fullCheckIdList = new List<int> { 81497285, 74018812, 37629703, 2511, 73602965, 75730490, 1225009 };
							if (selectId == 0)
							{
								foreach (int checkId2 in fullCheckIdList)
								{
									if (this.CheckRemainInDeck(checkId2) > 0 && !this.chainSummoningIdList.Contains(checkId2))
									{
										selectId = checkId2;
										break;
									}
								}
							}
							if (selectId > 0)
							{
								this.chainSummoningIdList.Add(selectId);
							}
						}
						IL_04FE:
						this.ResetCooclockEffect(onlyCheck);
						this.activatedCardIdList.Add(base.Card.Id);
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x0007F590 File Offset: 0x0007D790
		public bool TransactionRollbackActivate()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				foreach (KeyValuePair<int, Func<bool>> pair in new SortedList<int, Func<bool>>
				{
					{
						92714517,
						new Func<bool>(this.BigWelcomeLabrynthActivateCopy)
					},
					{
						83326048,
						new Func<bool>(this.DimensionalBarrierActivate)
					},
					{
						53417695,
						new Func<bool>(this.EscapeOfTheUnchainedActivateCopy)
					},
					{
						10045474,
						new Func<bool>(this.InfiniteImpermanenceActivateCopy)
					},
					{
						5380979,
						new Func<bool>(this.WelcomeLabrynthActivateCopy)
					},
					{
						30748475,
						new Func<bool>(this.DestructiveDarumaKarmaCannonActivate)
					}
				})
				{
					if (base.Bot.HasInGraveyard(pair.Key) && pair.Value())
					{
						this.rollbackCopyCardId = pair.Key;
						base.AI.SelectCard(pair.Key);
						return true;
					}
				}
			}
			if (base.Card.Location == CardLocation.SpellZone)
			{
				if (this.CheckWhetherNegated(true, false, (CardType)0))
				{
					return false;
				}
				foreach (KeyValuePair<int, Func<bool>> pair2 in new SortedList<int, Func<bool>>
				{
					{
						5380979,
						new Func<bool>(this.WelcomeLabrynthActivateCopy)
					},
					{
						94192409,
						new Func<bool>(base.DefaultCompulsoryEvacuationDevice)
					},
					{
						30748475,
						new Func<bool>(this.DestructiveDarumaKarmaCannonActivate)
					},
					{
						83326048,
						new Func<bool>(this.DimensionalBarrierActivate)
					},
					{
						53417695,
						new Func<bool>(this.EscapeOfTheUnchainedActivateCopy)
					},
					{
						10045474,
						new Func<bool>(this.InfiniteImpermanenceActivateCopy)
					},
					{
						78474168,
						new Func<bool>(base.DefaultBreakthroughSkill)
					},
					{
						92714517,
						new Func<bool>(this.BigWelcomeLabrynthActivateCopy)
					}
				})
				{
					if (base.Enemy.HasInGraveyard(pair2.Key) && pair2.Value())
					{
						this.rollbackCopyCardId = pair2.Key;
						base.AI.SelectCard(pair2.Key);
						this.ResetCooclockEffect(false);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x0007F814 File Offset: 0x0007DA14
		public bool InfiniteImpermanenceActivate()
		{
			return this.InfiniteImpermanenceActivateCheck(false, false);
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x0007F81E File Offset: 0x0007DA1E
		public bool InfiniteImpermanenceActivateCopy()
		{
			return this.InfiniteImpermanenceActivateCheck(true, false);
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x0007F828 File Offset: 0x0007DA28
		public bool InfiniteImpermanenceSetCheck()
		{
			return this.InfiniteImpermanenceActivateCheck(true, true);
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x0007F834 File Offset: 0x0007DA34
		public bool InfiniteImpermanenceActivateCheck(bool onlyCheck = false, bool noSelect = false)
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0))
			{
				return false;
			}
			ClientCard LastChainCard = base.Util.GetLastChainCard();
			if (base.Card.Location == CardLocation.SpellZone)
			{
				int thisSeq = -1;
				int thatSeq = -1;
				for (int i = 0; i < 5; i++)
				{
					if (base.Bot.SpellZone[i] == base.Card)
					{
						thisSeq = i;
					}
					if (LastChainCard != null && LastChainCard.Controller == 1 && LastChainCard.Location == CardLocation.SpellZone && base.Enemy.SpellZone[i] == LastChainCard)
					{
						thatSeq = i;
					}
					else if (base.Duel.Player == 0 && base.Util.GetProblematicEnemySpell() != null && base.Enemy.SpellZone[i] != null && base.Enemy.SpellZone[i].IsFloodgate())
					{
						thatSeq = i;
					}
				}
				if ((thisSeq * thatSeq >= 0 && thisSeq + thatSeq == 4) || base.Util.IsChainTarget(base.Card) || (LastChainCard != null && LastChainCard.Controller == 1 && LastChainCard.IsCode(18144506)))
				{
					ClientCard target = this.GetProblematicEnemyMonster(0, true, false, CardType.Trap);
					if (!noSelect)
					{
						if (target != null)
						{
							base.AI.SelectCard(target);
						}
						else
						{
							base.AI.SelectCard(base.Enemy.GetMonsters());
						}
					}
					if (!onlyCheck)
					{
						this.infiniteImpermanenceList.Add(thatSeq);
						if (this.cooclockAffected && this.setTrapThisTurn.Contains(base.Card))
						{
							this.cooclockAffected = false;
							this.setTrapThisTurn.Remove(base.Card);
						}
					}
					return true;
				}
			}
			List<ClientCard> shouldNegateList = this.GetMonsterListForTargetNegate(true, CardType.Trap);
			if (shouldNegateList.Count<ClientCard>() > 0)
			{
				ClientCard negateTarget = shouldNegateList[0];
				this.currentNegateMonsterList.Add(negateTarget);
				if (base.Card.Location == CardLocation.SpellZone && !onlyCheck)
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
				if (!noSelect)
				{
					base.AI.SelectCard(negateTarget);
				}
				this.currentDestroyCardList.Add(negateTarget);
				this.ResetCooclockEffect(onlyCheck);
				return true;
			}
			return false;
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x0007FA7A File Offset: 0x0007DC7A
		public bool DestructiveDarumaKarmaCannonActivate()
		{
			return this.DestructiveDarumaKarmaCannonActivateCheck(false);
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x0007FA83 File Offset: 0x0007DC83
		public bool DestructiveDarumaKarmaCannonSetCheck()
		{
			return this.DestructiveDarumaKarmaCannonActivateCheck(true);
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x0007FA8C File Offset: 0x0007DC8C
		public bool DestructiveDarumaKarmaCannonActivateCheck(bool noSelect = false)
		{
			bool activateFlag = base.Card.Location == CardLocation.SpellZone && base.DefaultOnBecomeTarget() && base.Util.IsOneEnemyBetter(true);
			bool canTriggerLovely = ((!this.activatedCardIdList.Contains(92714517) && base.Bot.GetSpells().Any((ClientCard card) => card.IsFacedown() && card.IsCode(92714517) && (!this.cooclockAffected || !this.setTrapThisTurn.Contains(card)))) || base.Util.ChainContainsCard(92714517)) && (base.Bot.HasInMonstersZone(2347656, true, false, true) || (this.CheckBigWelcomeCanSpSummon(2347656) && base.Bot.GetMonsterCount() > 0)) && !this.activatedCardIdList.Contains(2347657);
			bool flag = activateFlag;
			bool flag2;
			if (base.Bot.UnderAttack)
			{
				ClientCard battlingMonster = base.Bot.BattlingMonster;
				int num = ((battlingMonster != null) ? battlingMonster.GetDefensePower() : 0);
				ClientCard battlingMonster2 = base.Enemy.BattlingMonster;
				if (num <= ((battlingMonster2 != null) ? battlingMonster2.GetDefensePower() : 0) && !base.Util.ChainContainPlayer(0))
				{
					flag2 = !canTriggerLovely;
					goto IL_0105;
				}
			}
			flag2 = false;
			IL_0105:
			activateFlag = flag || flag2;
			activateFlag |= base.Duel.Phase > DuelPhase.Main1 && base.Duel.Phase < DuelPhase.Main2 && base.Bot.GetMonsterCount() == 0 && base.Enemy.GetMonsterCount() > 0;
			activateFlag |= base.Enemy.HasInMonstersZone(86066372, true, false, false) && !base.Util.ChainContainPlayer(0);
			int linkCount = 0;
			foreach (ClientCard monster in base.Enemy.GetMonsters())
			{
				if (!monster.IsFacedown())
				{
					if (!monster.HasType(CardType.Link))
					{
						linkCount++;
					}
					else
					{
						linkCount += monster.LinkCount;
					}
				}
			}
			activateFlag |= linkCount >= 6 && base.Util.IsOneEnemyBetter(true);
			if (activateFlag)
			{
				if (!noSelect)
				{
					this.currentDestroyCardList.AddRange(base.Enemy.GetMonsters());
					this.escapeTargetList.AddRange(base.Bot.GetMonsters());
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x0007FCC4 File Offset: 0x0007DEC4
		public bool EscapeOfTheUnchainedActivate()
		{
			return this.EscapeOfTheUnchainedActivateCheck(false, false);
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x0007FCCE File Offset: 0x0007DECE
		public bool EscapeOfTheUnchainedActivateCopy()
		{
			return this.EscapeOfTheUnchainedActivateCheck(true, false);
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x0007FCD8 File Offset: 0x0007DED8
		public bool EscapeOfTheUnchainedActivateCheck(bool onlyCheck = false, bool noSelect = false)
		{
			if (base.Card.Location != CardLocation.SpellZone && !onlyCheck)
			{
				if (!noSelect)
				{
					base.AI.SelectCard(41165831);
					this.activatedCardIdList.Add(base.Card.Id + 1);
				}
				return true;
			}
			ClientCard selfTarget = base.Bot.GetMonsters().FirstOrDefault((ClientCard card) => card.IsFaceup() && card.HasSetcode(304) && base.Duel.ChainTargets.Contains(card) && !this.escapeTargetList.Contains(card));
			if (selfTarget == null)
			{
				selfTarget = (from card in base.Bot.GetMonsters()
					where card.IsFaceup() && card.HasSetcode(304)
					orderby card.Attack
					select card).FirstOrDefault<ClientCard>();
			}
			if (selfTarget == null)
			{
				return false;
			}
			List<ClientCard> dangerList = this.GetProblematicEnemyCardList(true, false, CardType.Trap);
			if (dangerList.Count<ClientCard>() > 0 && base.Duel.LastChainPlayer != 0)
			{
				if (!noSelect)
				{
					base.AI.SelectCard(selfTarget);
					base.AI.SelectNextCard(dangerList);
					this.escapeTargetList.Add(selfTarget);
					this.currentDestroyCardList.Add(dangerList[0]);
					this.activatedCardIdList.Add(base.Card.Id);
				}
				return true;
			}
			int botBestPower = base.Util.GetBestPower(base.Bot, false);
			if (base.Duel.Player == 1 && base.Duel.Phase > DuelPhase.Main1 && base.Duel.Phase < DuelPhase.Main2)
			{
				List<ClientCard> dangerMonsters = (from card in base.Enemy.GetMonsters()
					where card.IsFaceup() && card.Attack >= botBestPower && !this.currentDestroyCardList.Contains(card) && !card.IsShouldNotBeTarget() && !card.IsShouldNotBeSpellTrapTarget()
					orderby card.Attack descending
					select card).ToList<ClientCard>();
				if (dangerMonsters.Count<ClientCard>() > 0)
				{
					if (!noSelect)
					{
						base.AI.SelectCard(selfTarget);
						base.AI.SelectNextCard(dangerMonsters);
						this.escapeTargetList.Add(selfTarget);
						this.currentDestroyCardList.Add(dangerMonsters[0]);
						this.activatedCardIdList.Add(base.Card.Id);
					}
					return true;
				}
			}
			if ((base.Duel.Player == 1 && base.Duel.Phase > DuelPhase.Main2 && ((base.Bot.HasInGraveyard(24269961) && !this.activatedCardIdList.Contains(24269962)) || (base.Bot.HasInMonstersZone(2347656, true, false, true) && !this.activatedCardIdList.Contains(2347657)))) | (base.DefaultOnBecomeTarget() && base.Card.Location == CardLocation.SpellZone && !base.Util.ChainContainsCard(15693423)))
			{
				List<ClientCard> destroyTarget = this.GetNormalEnemyTargetList(true, true, CardType.Trap);
				if (destroyTarget.Count<ClientCard>() > 0)
				{
					if (!noSelect)
					{
						base.AI.SelectCard(selfTarget);
						base.AI.SelectNextCard(destroyTarget);
						this.escapeTargetList.Add(selfTarget);
						this.currentDestroyCardList.Add(destroyTarget[0]);
						this.activatedCardIdList.Add(base.Card.Id);
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x00080028 File Offset: 0x0007E228
		public bool DimensionalBarrierActivate()
		{
			if (base.Duel.Player == 0 && base.Duel.Turn == 1)
			{
				return false;
			}
			if (this.CheckWhetherNegated(true, false, (CardType)0))
			{
				return false;
			}
			foreach (KeyValuePair<int, Func<bool>> checkType in new Dictionary<int, Func<bool>>
			{
				{
					1057,
					new Func<bool>(this.DimensionalBarrierForRitual)
				},
				{
					1056,
					new Func<bool>(this.DimensionalBarrierForFusion)
				},
				{
					1063,
					new Func<bool>(this.DimensionalBarrierForSynchro)
				},
				{
					1073,
					new Func<bool>(this.DimensionalBarrierForXyz)
				},
				{
					1074,
					new Func<bool>(this.DimensionalBarrierForPendulum)
				}
			})
			{
				if (!this.dimensionalBarrierAnnouced.Contains(checkType.Key) && checkType.Value())
				{
					this.ResetCooclockEffect(false);
					return true;
				}
			}
			return base.DefaultOnBecomeTarget();
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x00080148 File Offset: 0x0007E348
		public bool DimensionalBarrierForRitual()
		{
			foreach (ClientCard chainCard in base.Duel.CurrentChain)
			{
				if (chainCard != null && chainCard.Controller == 1 && !chainCard.IsDisabled() && chainCard.HasType(CardType.Ritual) && (chainCard.HasType(CardType.Spell) || (chainCard.Location == CardLocation.MonsterZone && !this.currentNegateMonsterList.Contains(chainCard))))
				{
					if (this.dimensionBarrierAnnouncing)
					{
						this.currentNegateMonsterList.Add(chainCard);
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x000801F0 File Offset: 0x0007E3F0
		public bool DimensionalBarrierForFusion()
		{
			foreach (ClientCard chainCard in base.Duel.CurrentChain)
			{
				if (chainCard != null && chainCard.Controller == 1 && !chainCard.IsDisabled() && (chainCard.IsFusionSpell() || (chainCard.HasType(CardType.Fusion) && chainCard.Location == CardLocation.MonsterZone && !this.currentNegateMonsterList.Contains(chainCard))))
				{
					if (this.dimensionBarrierAnnouncing)
					{
						this.currentNegateMonsterList.Add(chainCard);
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x00080294 File Offset: 0x0007E494
		public bool DimensionalBarrierForSynchro()
		{
			foreach (ClientCard chainCard in base.Duel.CurrentChain)
			{
				if (chainCard != null && chainCard.Controller == 1 && !chainCard.IsDisabled() && chainCard.HasType(CardType.Synchro) && chainCard.Location == CardLocation.MonsterZone && !this.currentNegateMonsterList.Contains(chainCard))
				{
					if (this.dimensionBarrierAnnouncing)
					{
						this.currentNegateMonsterList.Add(chainCard);
					}
					return true;
				}
			}
			if (base.Duel.Player == 1 && !base.Util.ChainContainsCard(30748475) && base.Enemy.ExtraDeck.Count<ClientCard>() > 0)
			{
				bool tunerCheck = false;
				bool nontunerCheck = false;
				foreach (ClientCard monster in base.Enemy.GetMonsters())
				{
					if (!monster.IsFacedown() && !monster.HasType((CardType)75497472))
					{
						if (monster.HasType(CardType.Tuner))
						{
							tunerCheck = true;
						}
						else
						{
							nontunerCheck = true;
						}
					}
				}
				if (tunerCheck && nontunerCheck)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x000803EC File Offset: 0x0007E5EC
		public bool DimensionalBarrierForXyz()
		{
			foreach (ClientCard chainCard in base.Duel.CurrentChain)
			{
				if (chainCard != null && chainCard.Controller == 1 && !chainCard.IsDisabled() && chainCard.HasType(CardType.Xyz) && chainCard.Location == CardLocation.MonsterZone && !this.currentNegateMonsterList.Contains(chainCard))
				{
					if (this.dimensionBarrierAnnouncing)
					{
						this.currentNegateMonsterList.Add(chainCard);
					}
					return true;
				}
			}
			if (base.Duel.Player == 1 && !base.Util.ChainContainsCard(30748475) && base.Enemy.ExtraDeck.Count<ClientCard>() > 0)
			{
				List<int> existsLevel = new List<int>(6);
				foreach (ClientCard monster in base.Enemy.GetMonsters())
				{
					if (!monster.IsFacedown())
					{
						if (monster.IsOneForXyz())
						{
							return true;
						}
						if (!monster.HasType((CardType)8404992))
						{
							int level = monster.Level;
							if (level == 2 || !monster.HasType(CardType.Link))
							{
								if (existsLevel.Contains(level))
								{
									return true;
								}
								existsLevel.Add(level);
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x00080568 File Offset: 0x0007E768
		public bool DimensionalBarrierForPendulum()
		{
			foreach (ClientCard chainCard in base.Duel.CurrentChain)
			{
				if (chainCard != null && chainCard.Controller == 1 && !chainCard.IsDisabled() && chainCard.HasType(CardType.Pendulum) && chainCard.Location == CardLocation.MonsterZone && !this.currentNegateMonsterList.Contains(chainCard))
				{
					if (this.dimensionBarrierAnnouncing)
					{
						this.currentNegateMonsterList.Add(chainCard);
					}
					return true;
				}
			}
			ClientCard i = base.Enemy.SpellZone[6];
			ClientCard r = base.Enemy.SpellZone[7];
			return i != null && r != null && i.LScale != r.RScale;
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x0008063C File Offset: 0x0007E83C
		public bool BigWelcomeLabrynthActivate()
		{
			return this.BigWelcomeLabrynthActivateCheck(false, false);
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x00080646 File Offset: 0x0007E846
		public bool BigWelcomeLabrynthBecomeTargetActivate()
		{
			return base.DefaultOnBecomeTarget() && this.BigWelcomeLabrynthActivateCheck(false, false);
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x0008065A File Offset: 0x0007E85A
		public bool BigWelcomeLabrynthActivateCopy()
		{
			return this.BigWelcomeLabrynthActivateCheck(true, false);
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x00080664 File Offset: 0x0007E864
		public bool BigWelcomeLabrynthSetCheck()
		{
			return !this.CheckShouldNoMoreSpSummon(true) && this.BigWelcomeLabrynthActivateCheck(true, true);
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x0008067C File Offset: 0x0007E87C
		public bool BigWelcomeLabrynthActivateCheck(bool onlyCheck = false, bool noSelect = false)
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0))
			{
				return false;
			}
			if (base.Card.Location != CardLocation.SpellZone && !onlyCheck)
			{
				return false;
			}
			if (this.GetEmptyMainMonsterZoneCount() == 0)
			{
				return false;
			}
			bool activateTimingFlag = base.Duel.Phase > DuelPhase.Main2 || (base.Card.IsCode(73602965) && (base.CurrentTiming & 4) > 0);
			bool needDestroyFlag = this.GetProblematicEnemyCardList(false, false, (CardType)0).Count<ClientCard>() > 0;
			needDestroyFlag |= this.activatedCardIdList.Contains(1225009) && activateTimingFlag;
			bool flag5 = needDestroyFlag;
			bool flag6;
			if (base.Bot.UnderAttack)
			{
				ClientCard battlingMonster = base.Bot.BattlingMonster;
				int num = ((battlingMonster != null) ? battlingMonster.GetDefensePower() : 0);
				ClientCard battlingMonster2 = base.Enemy.BattlingMonster;
				if (num <= ((battlingMonster2 != null) ? battlingMonster2.GetDefensePower() : 0))
				{
					flag6 = base.Duel.LastChainPlayer != 0;
					goto IL_00D6;
				}
			}
			flag6 = false;
			IL_00D6:
			needDestroyFlag = flag5 || flag6;
			needDestroyFlag |= base.Duel.Turn == 1 && base.Duel.Player == 0 && !this.activatedCardIdList.Contains(2347657);
			needDestroyFlag |= base.Duel.Turn == 1 && base.Enemy.GetMonsterCount() == 0 && base.Enemy.GetSpellCount() == 0 && base.Enemy.Hand.Count > 0 && (base.CurrentTiming & 4) > 0;
			bool haveEnemyChain = false;
			bool haveWelcome = false;
			foreach (ClientCard chain in base.Duel.CurrentChain)
			{
				if (chain != null)
				{
					if (chain.Controller == 1)
					{
						haveEnemyChain = true;
						break;
					}
					if (chain.IsCode(new int[] { 5380979, 6351147, 81497285 }))
					{
						haveWelcome = true;
					}
				}
			}
			if (haveWelcome && !haveEnemyChain)
			{
				return false;
			}
			foreach (ClientCard target in base.Bot.GetMonsters())
			{
				if (base.Duel.ChainTargets.Contains(target) && !this.escapeTargetList.Contains(target) && (!target.IsCode(new int[] { 67680512, 93084621 }) || !base.Duel.CurrentChain.Contains(target)))
				{
					Logger.DebugWriteLine("[BigWelcome]escape target");
					if (!noSelect)
					{
						this.bigwelcomeEscaseTarget = target;
						this.escapeTargetList.Add(target);
						this.activatedCardIdList.Add(base.Card.Id);
					}
					return true;
				}
			}
			if (base.Bot.GetMonsterCount() > 0)
			{
				bool flag = needDestroyFlag && !this.activatedCardIdList.Contains(2347657) && (base.Util.ChainContainPlayer(1) || base.Duel.LastChainPlayer != 0);
				bool flag2 = base.DefaultOnBecomeTarget();
				bool flag3 = base.Duel.Player == 1 && !this.activatedCardIdList.Contains(92714517) && activateTimingFlag;
				bool flag4 = base.Duel.Player == 0 && base.Duel.LastChainPlayer != 0 && !this.activatedCardIdList.Contains(92714517);
				Logger.DebugWriteLine(string.Concat(new string[]
				{
					"[BigWelcome count>0]flag: ",
					flag.ToString(),
					" ",
					flag2.ToString(),
					" ",
					flag3.ToString(),
					" ",
					flag4.ToString()
				}));
				needDestroyFlag = needDestroyFlag || flag3;
				if (flag || flag2 || flag3 || flag4)
				{
					if (this.CheckBigWelcomeCanSpSummon(2347656))
					{
						bool flag7 = !this.activatedCardIdList.Contains(2347657);
					}
					base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && card.IsCode(2347656));
					if (!noSelect)
					{
						this.activatedCardIdList.Add(base.Card.Id);
					}
					this.ResetCooclockEffect(onlyCheck);
					return true;
				}
			}
			else
			{
				bool flag8 = base.DefaultOnBecomeTarget() | (base.Duel.Player == 1 && !this.activatedCardIdList.Contains(92714517) && activateTimingFlag);
				bool flag9;
				if (base.Duel.Player == 0 && !this.summoned && !base.Bot.HasInHand(1225009) && !this.activatedCardIdList.Contains(1225009) && (base.Duel.Phase >= DuelPhase.Main1 || !base.Bot.HasInHand(49238328) || base.Bot.ExtraDeck.Count<ClientCard>() < 3))
				{
					flag9 = !base.Duel.CurrentChain.Any((ClientCard card) => card.IsCode(49238328) && card.Controller == 0);
				}
				else
				{
					flag9 = false;
				}
				if ((flag8 || flag9) && !noSelect)
				{
					this.activatedCardIdList.Add(base.Card.Id);
					this.ResetCooclockEffect(onlyCheck);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x00080BCC File Offset: 0x0007EDCC
		public bool BigWelcomeLabrynthGraveActivate()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				if (base.Bot.GetMonsters().Any((ClientCard card) => card.Level >= 8 && card.IsFaceup() && card.HasRace(CardRace.Fiend) && !card.HasType((CardType)75497472)))
				{
					ClientCard problemCard = this.GetProblematicEnemyMonster(-1, true, true, CardType.Trap);
					if (problemCard != null)
					{
						base.AI.SelectCard(problemCard);
						this.currentDestroyCardList.Add(problemCard);
						this.activatedCardIdList.Add(base.Card.Id);
						return true;
					}
					if (!base.Bot.HasInMonstersZone(2347656, true, false, true) || (this.activatedCardIdList.Contains(2347656) && this.activatedCardIdList.Contains(2347657)))
					{
						List<ClientCard> problemEnemySpellList = base.Enemy.SpellZone.Where((ClientCard c) => ((c != null) ? c.Data : null) != null && c.IsFaceup() && c.IsFloodgate() && !c.IsShouldNotBeTarget() && (c.HasType(CardType.Trap) || base.Duel.Player == 0)).ToList<ClientCard>();
						problemEnemySpellList.AddRange(base.Enemy.SpellZone.Where((ClientCard c) => ((c != null) ? c.Data : null) != null && c.IsFaceup() && !problemEnemySpellList.Contains(c) && c.HasType((CardType)17694720) && !c.IsShouldNotBeTarget() && (c.HasType(CardType.Trap) || this.Duel.Player == 0)).ToList<ClientCard>());
						if (problemEnemySpellList.Count<ClientCard>() > 0)
						{
							base.AI.SelectCard(problemEnemySpellList);
							this.currentDestroyCardList.Add(problemEnemySpellList[0]);
							this.activatedCardIdList.Add(base.Card.Id);
							return true;
						}
					}
					int botBestPower = base.Util.GetBestPower(base.Bot, false);
					if (base.Duel.Player == 1 && base.Duel.Phase > DuelPhase.Main1 && base.Duel.Phase < DuelPhase.Main2)
					{
						List<ClientCard> dangerMonsters = (from card in base.Enemy.GetMonsters()
							where card.IsFaceup() && card.Attack >= botBestPower && !this.currentDestroyCardList.Contains(card) && !card.IsShouldNotBeTarget() && !card.IsShouldNotBeSpellTrapTarget()
							orderby card.Attack descending
							select card).ToList<ClientCard>();
						if (dangerMonsters.Count<ClientCard>() > 0)
						{
							base.AI.SelectCard(dangerMonsters);
							this.currentDestroyCardList.Add(dangerMonsters[0]);
							this.activatedCardIdList.Add(base.Card.Id);
							return true;
						}
					}
					if (base.Duel.Phase > DuelPhase.Main2)
					{
						List<ClientCard> returnList = this.GetNormalEnemyTargetList(true, true, CardType.Trap);
						if (returnList.Count<ClientCard>() > 0)
						{
							base.AI.SelectCard(returnList);
							this.currentDestroyCardList.Add(returnList[0]);
							this.activatedCardIdList.Add(base.Card.Id);
							return true;
						}
					}
				}
				List<ClientCard> targetList = (from card in base.Bot.GetMonsters()
					where card.IsFaceup() && card.HasRace(CardRace.Fiend)
					select card).ToList<ClientCard>();
				foreach (ClientCard target in targetList)
				{
					if (base.Duel.ChainTargets.Contains(target) && !this.escapeTargetList.Contains(target) && (!target.IsCode(new int[] { 67680512, 93084621 }) || !base.Duel.CurrentChain.Contains(target)))
					{
						base.AI.SelectCard(target);
						this.escapeTargetList.Add(target);
						this.activatedCardIdList.Add(base.Card.Id);
						return true;
					}
				}
				if (base.Duel.Player == 0 && base.Duel.Phase <= DuelPhase.Main2 && !this.summoned && !base.Bot.HasInHand(1225009) && !this.activatedCardIdList.Contains(1225009) && !this.chainSummoningIdList.Contains(1225009))
				{
					ClientCard target2 = targetList.FirstOrDefault((ClientCard card) => card.IsCode(1225009));
					if (target2 != null)
					{
						base.AI.SelectCard(target2);
						this.escapeTargetList.Add(target2);
						this.activatedCardIdList.Add(base.Card.Id);
						return true;
					}
				}
				List<ClientCard> checkFurnitureList = new List<ClientCard>(base.Bot.Hand);
				checkFurnitureList.AddRange(base.Bot.GetMonsters());
				if (this.CheckRemainInDeck(new int[] { 5380979, 92714517 }) != 0)
				{
					if (checkFurnitureList.Any((ClientCard card) => card.IsCode(new int[] { 37629703, 74018812 })))
					{
						return false;
					}
				}
				if (base.Duel.LastChainPlayer >= 0 || base.Duel.Player != 0 || base.Bot.HasInMonstersZone(2347656, true, false, true) || (this.cooclockAffected && base.Bot.HasInHandOrInSpellZone(92714517)))
				{
					return false;
				}
				int checkCount = 0;
				foreach (int checkId in new List<int> { 37629703, 74018812, 5380979 })
				{
					if (base.Bot.HasInGraveyard(checkId) && !this.activatedCardIdList.Contains(checkId + 1))
					{
						checkCount++;
					}
				}
				if (checkCount <= 0)
				{
					return false;
				}
				ClientCard target3 = targetList.FirstOrDefault((ClientCard card) => card.IsFaceup() && card.HasRace(CardRace.Fiend) && ((card.Level <= 4 && !card.HasType((CardType)75505664)) || card.IsCode(81497285)));
				if (target3 != null)
				{
					base.AI.SelectCard(target3);
					this.escapeTargetList.Add(target3);
					this.activatedCardIdList.Add(base.Card.Id);
					return true;
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x00081210 File Offset: 0x0007F410
		public bool ChaosAngelSpSummonWith2Monster()
		{
			if (this.CheckShouldNoMoreSpSummon(false))
			{
				return false;
			}
			List<ClientCard> level2MonsterList = new List<ClientCard>();
			List<ClientCard> level4MonsterList = new List<ClientCard>();
			List<ClientCard> level6MonsterList = new List<ClientCard>();
			List<ClientCard> level8MonsterList = new List<ClientCard>();
			foreach (ClientCard monster in base.Bot.GetMonsters())
			{
				if (monster.IsFaceup() && !monster.HasType((CardType)75497472) && monster.HasAttribute((CardAttribute)48))
				{
					if (monster.Level == 2)
					{
						level2MonsterList.Add(monster);
					}
					if (monster.Level == 4)
					{
						level4MonsterList.Add(monster);
					}
					if (monster.Level == 6)
					{
						level6MonsterList.Add(monster);
					}
					if (monster.Level == 8)
					{
						level8MonsterList.Add(monster);
					}
				}
			}
			level2MonsterList.Sort(new Comparison<ClientCard>(this.CompareUsableAttack));
			level4MonsterList.Sort(new Comparison<ClientCard>(this.CompareUsableAttack));
			level6MonsterList.Sort(new Comparison<ClientCard>(this.CompareUsableAttack));
			level8MonsterList.Sort(new Comparison<ClientCard>(this.CompareUsableAttack));
			bool checkFlag = this.GetProblematicEnemyCardList(true, false, CardType.Monster).Count<ClientCard>() > 0 && !this.CheckWhetherNegated(true, true, CardType.Monster);
			ClientCard BestEnemyMonster = base.Util.GetBestEnemyMonster(false, false);
			if (BestEnemyMonster != null && base.Util.GetBestPower(base.Bot, true) <= base.Util.GetBestPower(base.Enemy, false))
			{
				checkFlag |= base.Util.GetBestPower(base.Enemy, false) <= 3500;
				checkFlag |= !BestEnemyMonster.IsShouldNotBeTarget() && !BestEnemyMonster.IsShouldNotBeMonsterTarget();
			}
			if (level4MonsterList.Count<ClientCard>() > 0 && level6MonsterList.Count<ClientCard>() > 0)
			{
				List<ClientCard> materials = new List<ClientCard>
				{
					level4MonsterList[0],
					level6MonsterList[0]
				};
				bool summonFlag = checkFlag;
				if (base.Enemy.GetMonsterCount() == 0 && base.Duel.Phase < DuelPhase.Main2)
				{
					summonFlag |= this.GetBotCurrentTotalAttack(null) < base.Enemy.LifePoints && this.GetBotCurrentTotalAttack(materials) + 3500 >= base.Enemy.LifePoints;
				}
				if (summonFlag)
				{
					base.AI.SelectMaterials(materials, 0);
					return true;
				}
			}
			if (level2MonsterList.Count<ClientCard>() > 0 && level8MonsterList.Count<ClientCard>() > 0)
			{
				foreach (ClientCard level2 in level2MonsterList)
				{
					foreach (ClientCard level3 in level8MonsterList)
					{
						List<ClientCard> materials2 = new List<ClientCard> { level2, level3 };
						if (checkFlag && (!level3.IsCode(2347656) || level3.IsDisabled() || !base.Bot.HasInSpellZoneOrInGraveyard(92714517)))
						{
							base.AI.SelectMaterials(materials2, 0);
							return true;
						}
						if (base.Enemy.GetMonsterCount() == 0 && this.GetMaterialAttack(materials2) < 3500 && base.Duel.Phase < DuelPhase.Main2 && this.GetBotCurrentTotalAttack(null) < base.Enemy.LifePoints && this.GetBotCurrentTotalAttack(materials2) + 3500 >= base.Enemy.LifePoints)
						{
							base.AI.SelectMaterials(materials2, 0);
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x00081600 File Offset: 0x0007F800
		public bool ChaosAngelSpSummonWith3Monster()
		{
			if (this.CheckShouldNoMoreSpSummon(false))
			{
				return false;
			}
			List<ClientCard> level2MonsterList = new List<ClientCard>();
			List<ClientCard> level4MonsterList = new List<ClientCard>();
			foreach (ClientCard monster in base.Bot.GetMonsters())
			{
				if (monster.IsFaceup() && !monster.HasType((CardType)75497472) && monster.HasAttribute((CardAttribute)48))
				{
					if (monster.Level == 2)
					{
						level2MonsterList.Add(monster);
					}
					if (monster.Level == 4)
					{
						level4MonsterList.Add(monster);
					}
				}
			}
			level2MonsterList.Sort(new Comparison<ClientCard>(this.CompareUsableAttack));
			level4MonsterList.Sort(new Comparison<ClientCard>(this.CompareUsableAttack));
			bool checkFlag = this.GetProblematicEnemyCardList(true, false, CardType.Monster).Count<ClientCard>() > 0 && !this.CheckWhetherNegated(true, true, CardType.Monster);
			ClientCard BestEnemyMonster = base.Util.GetBestEnemyMonster(false, false);
			if (BestEnemyMonster != null && base.Util.GetBestPower(base.Bot, true) <= base.Util.GetBestPower(base.Enemy, false))
			{
				checkFlag |= base.Util.GetBestPower(base.Enemy, false) <= 3500;
				checkFlag |= !BestEnemyMonster.IsShouldNotBeTarget() && !BestEnemyMonster.IsShouldNotBeMonsterTarget();
			}
			if (level2MonsterList.Count<ClientCard>() >= 1 && level4MonsterList.Count<ClientCard>() >= 2)
			{
				foreach (ClientCard level2 in level2MonsterList)
				{
					for (int level4Index = 0; level4Index < level4MonsterList.Count<ClientCard>() - 1; level4Index++)
					{
						ClientCard level3 = level4MonsterList[level4Index];
						for (int level4Index2 = level4Index + 1; level4Index2 < level4MonsterList.Count<ClientCard>(); level4Index2++)
						{
							ClientCard level4 = level4MonsterList[level4Index2];
							List<ClientCard> materials = new List<ClientCard> { level2, level3, level4 };
							bool summonFlag = checkFlag;
							if (base.Enemy.GetMonsterCount() == 0 && base.Duel.Phase < DuelPhase.Main2)
							{
								summonFlag |= this.GetBotCurrentTotalAttack(null) < base.Enemy.LifePoints && this.GetBotCurrentTotalAttack(materials) + 3500 >= base.Enemy.LifePoints;
							}
							if (summonFlag)
							{
								base.AI.SelectMaterials(materials, 0);
								return true;
							}
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x000818A0 File Offset: 0x0007FAA0
		public bool ChaosAngelActivate()
		{
			List<ClientCard> targetList = this.GetNormalEnemyTargetList(true, true, CardType.Monster);
			if (targetList.Count<ClientCard>() > 0)
			{
				base.AI.SelectCard(targetList);
				this.currentDestroyCardList.Add(targetList[0]);
				return true;
			}
			return false;
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x000818E4 File Offset: 0x0007FAE4
		public bool SummonForTYPHONCheck()
		{
			if (base.Bot.HasInExtra(93039339))
			{
				if (!base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup()))
				{
					if (this.enemySpSummonFromExLastTurn < 2 && this.enemySpSummonFromExThisTurn < 2)
					{
						return false;
					}
					if (base.Card.Level > 4)
					{
						return false;
					}
					int currentAttack = 0;
					NamedCard cardData = NamedCard.Get(base.Card.Id);
					if (cardData != null)
					{
						currentAttack = cardData.Attack;
					}
					foreach (ClientCard clientCard in base.Bot.Hand.Where((ClientCard card) => card.IsMonster() && card.Level <= 4).ToList<ClientCard>())
					{
						cardData = NamedCard.Get(clientCard.Id);
						if (cardData != null && cardData.Attack < currentAttack)
						{
							return false;
						}
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x00081A04 File Offset: 0x0007FC04
		public bool SuperStarslayerTYPHONSpSummon()
		{
			ClientCard material = (from card in base.Bot.GetMonsters()
				where card.IsFaceup()
				orderby card.Attack descending
				select card).FirstOrDefault<ClientCard>();
			if (material == null || (material.Attack >= 2900 && material.Owner == 0))
			{
				return false;
			}
			if (((this.GetProblematicEnemyMonster(material.Attack, false, false, (CardType)0) != null) | (material.Level <= 4)) & (!material.HasType(CardType.Link) || base.Duel.Phase < DuelPhase.Main2))
			{
				Logger.DebugWriteLine("*** TYPHON select: " + material.Name);
				base.AI.SelectMaterials(material, 0);
				return true;
			}
			return false;
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x00081AF0 File Offset: 0x0007FCF0
		public bool SuperStarslayerTYPHONActivate()
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0))
			{
				return false;
			}
			List<ClientCard> targetList = new List<ClientCard>();
			targetList.AddRange(from c in base.Enemy.GetMonsters()
				where !this.currentDestroyCardList.Contains(c) && c.IsFloodgate() && c.IsFaceup()
				select c into card
				orderby card.Attack descending
				select card);
			targetList.AddRange(from c in base.Enemy.GetMonsters()
				where !this.currentDestroyCardList.Contains(c) && c.IsMonsterDangerous() && c.IsFaceup()
				select c into card
				orderby card.Attack descending
				select card);
			targetList.AddRange(from c in base.Enemy.GetMonsters()
				where !this.currentDestroyCardList.Contains(c) && c.IsMonsterInvincible() && c.IsFaceup()
				select c into card
				orderby card.Attack descending
				select card);
			targetList.AddRange(from c in base.Enemy.GetMonsters()
				where !this.currentDestroyCardList.Contains(c) && c.GetDefensePower() >= this.Util.GetBestAttack(this.Bot) && c.IsAttack()
				select c into card
				orderby card.Attack descending
				select card);
			if (base.Duel.Phase >= DuelPhase.Main2)
			{
				targetList.AddRange(from c in base.Enemy.GetMonsters()
					where !this.currentDestroyCardList.Contains(c) && c.HasType((CardType)109060160)
					select c into card
					orderby card.Attack descending
					select card);
			}
			if (targetList.Count<ClientCard>() > 0)
			{
				targetList.AddRange(from card in base.Enemy.GetMonsters()
					where card.IsFaceup() && !targetList.Contains(card)
					orderby card.Attack descending
					select card);
				targetList.AddRange(this.ShuffleList<ClientCard>((from card in base.Enemy.GetMonsters()
					where card.IsFacedown() && !targetList.Contains(card)
					select card).ToList<ClientCard>()));
				targetList.AddRange(this.ShuffleList<ClientCard>((from card in base.Bot.GetMonsters()
					where card.IsFacedown() && !targetList.Contains(card)
					select card).ToList<ClientCard>()));
				targetList.AddRange(from card in base.Bot.GetMonsters()
					where card.IsFaceup() && !targetList.Contains(card)
					orderby card.Attack
					select card);
				base.AI.SelectCard(base.Card.Overlays);
				string text = "TYPHON first target: ";
				ClientCard clientCard = targetList[0];
				Logger.DebugWriteLine(text + ((clientCard != null) ? clientCard.Name : null));
				base.AI.SelectNextCard(targetList);
				return true;
			}
			return false;
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x00081E08 File Offset: 0x00080008
		public bool UnchainedAbominationSpSummon()
		{
			if (this.CheckShouldNoMoreSpSummon(false))
			{
				return false;
			}
			if (base.Enemy.GetMonsterCount() > 0 && base.Bot.HasInMonstersZone(93084621, false, false, false) && !this.activatedCardIdList.Contains(93084621))
			{
				return false;
			}
			List<List<ClientCard>> usableMaterialMultiList = new List<List<ClientCard>>();
			ClientCard anguish = base.Bot.GetMonsters().FirstOrDefault((ClientCard card) => card.IsCode(93084621));
			if (anguish != null)
			{
				List<ClientCard> materials = this.GetCanBeUsedForLinkMaterial(true, (ClientCard card) => card == anguish);
				if (materials.Count<ClientCard>() > 0)
				{
					usableMaterialMultiList.Add(new List<ClientCard>
					{
						anguish,
						materials[0]
					});
				}
			}
			List<ClientCard> link2List = (from card in base.Bot.GetMonsters()
				where card.HasType(CardType.Link) && card.LinkCount == 2 && (!card.IsCode(71607202) || !this.summonThisTurn.Contains(card))
				orderby card.Attack
				select card).ToList<ClientCard>();
			if (link2List.Count<ClientCard>() > 0)
			{
				ClientCard link2Material = null;
				ClientCard littleKnight = link2List.FirstOrDefault((ClientCard card) => card.Sequence >= 5 && card.IsCode(29301450));
				if (littleKnight != null)
				{
					link2Material = littleKnight;
				}
				else
				{
					link2Material = link2List[0];
				}
				if (link2List.Count<ClientCard>() >= 2)
				{
					usableMaterialMultiList.Add(new List<ClientCard>
					{
						link2Material,
						link2List.FirstOrDefault((ClientCard card) => card != link2Material)
					});
				}
				List<ClientCard> remainList = this.GetCanBeUsedForLinkMaterial(false, (ClientCard card) => card != link2Material && (!card.HasType(CardType.Link) || card.LinkMarker <= 2));
				if (remainList.Count<ClientCard>() >= 2)
				{
					usableMaterialMultiList.Add(new List<ClientCard>
					{
						link2Material,
						remainList[0],
						remainList[1]
					});
				}
			}
			foreach (List<ClientCard> currMaterials in usableMaterialMultiList)
			{
				if ((this.CheckCanDirectAttack() && this.GetBotCurrentTotalAttack(null) < base.Enemy.LifePoints && this.GetBotCurrentTotalAttack(currMaterials) + 3000 >= base.Enemy.LifePoints) | (this.GetProblematicEnemyMonster(0, false, false, (CardType)0) != null && this.GetProblematicEnemyMonster(3000, false, false, (CardType)0) == null))
				{
					base.AI.SelectMaterials(currMaterials, 0);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x000820D4 File Offset: 0x000802D4
		public bool UnchainedAbominationActivate()
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0))
			{
				return false;
			}
			List<ClientCard> targetList = this.GetNormalEnemyTargetList(true, true, CardType.Monster);
			if (targetList.Count<ClientCard>() == 0)
			{
				return false;
			}
			int logDesc = base.ActivateDescription;
			if (logDesc >= base.Util.GetStringId(29479265, 0))
			{
				logDesc = base.Util.GetStringId(29479265, 0) - 10;
			}
			Logger.DebugWriteLine("[UnchainedAbomination]desc: " + logDesc.ToString() + ", timing = " + base.CurrentTiming.ToString());
			if (base.ActivateDescription == base.Util.GetStringId(29479265, 0))
			{
				this.activatedCardIdList.Add(base.Card.Id);
			}
			if (base.ActivateDescription == base.Util.GetStringId(29479265, 1) || base.ActivateDescription == -1)
			{
				this.activatedCardIdList.Add(base.Card.Id + 1);
			}
			if (base.ActivateDescription == base.Util.GetStringId(29479265, 2))
			{
				this.activatedCardIdList.Add(base.Card.Id + 2);
			}
			base.AI.SelectCard(targetList);
			return true;
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x00082204 File Offset: 0x00080404
		public bool UnchainedSoulOfAnguishSpSummon()
		{
			if (this.CheckShouldNoMoreSpSummon(false))
			{
				return false;
			}
			ClientCard unchainedNonLink = base.Bot.GetMonsters().FirstOrDefault((ClientCard card) => card.IsFaceup() && card.HasSetcode(304) && !card.HasType(CardType.Link));
			ClientCard unchainedLink2 = base.Bot.GetMonsters().FirstOrDefault((ClientCard card) => card.IsFaceup() && card.HasSetcode(304) && card.HasType(CardType.Link) && card.LinkCount == 2);
			string text = "[Anguish summon] unchainedNonLink = ";
			ClientCard unchainedNonLink2 = unchainedNonLink;
			string text2 = ((unchainedNonLink2 != null) ? unchainedNonLink2.Name : null);
			string text3 = ", unchainedLink2 = ";
			ClientCard unchainedLink = unchainedLink2;
			Logger.DebugWriteLine(text + text2 + text3 + ((unchainedLink != null) ? unchainedLink.Name : null));
			if (unchainedNonLink == null && unchainedLink2 == null)
			{
				return false;
			}
			int needMonsterCount = 2;
			if (unchainedLink2 != null)
			{
				needMonsterCount = 1;
			}
			if (needMonsterCount == 2 && base.Bot.HasInExtra(24269961))
			{
				return false;
			}
			bool flag;
			if (!base.Bot.HasInMonstersZone(93084621, false, false, false) && !this.activatedCardIdList.Contains(93084621))
			{
				flag = base.Enemy.GetMonsters().Any((ClientCard card) => card.IsFaceup());
			}
			else
			{
				flag = false;
			}
			bool needAnguish = flag;
			if (needAnguish)
			{
				needAnguish = base.Bot.HasInExtra(67680512);
				needAnguish |= base.Bot.HasInExtra(29479265);
				needAnguish |= base.Bot.HasInExtra(29301450) && this.banSpSummonExceptFiendCount == 0;
			}
			Logger.DebugWriteLine("[Anguish summon] needAnguish = " + needAnguish.ToString());
			if (needMonsterCount == 1)
			{
				List<ClientCard> materialList = this.GetCanBeUsedForLinkMaterial(needAnguish, (ClientCard card) => card == unchainedLink2);
				Logger.DebugWriteLine("[Anguish summon 1] material count = " + materialList.Count<ClientCard>().ToString());
				if (materialList.Count<ClientCard>() == 0)
				{
					return false;
				}
				List<ClientCard> selectMaterials = new List<ClientCard>
				{
					unchainedLink2,
					materialList[0]
				};
				bool summonFlag = needAnguish;
				summonFlag |= this.CheckCanDirectAttack() && this.GetBotCurrentTotalAttack(null) < base.Enemy.LifePoints && this.GetBotCurrentTotalAttack(selectMaterials) + 2400 >= base.Enemy.LifePoints;
				Logger.DebugWriteLine("[Anguish summon 1] summon flag " + summonFlag.ToString());
				if (summonFlag)
				{
					base.AI.SelectMaterials(selectMaterials, 0);
					return true;
				}
			}
			if (needMonsterCount == 2)
			{
				List<ClientCard> materialList2 = this.GetCanBeUsedForLinkMaterial(needAnguish, (ClientCard card) => card == unchainedNonLink);
				Logger.DebugWriteLine("[Anguish summon 2] material count = " + materialList2.Count<ClientCard>().ToString());
				if (materialList2.Count<ClientCard>() >= 2)
				{
					List<ClientCard> selectMaterials2 = new List<ClientCard>
					{
						unchainedNonLink,
						materialList2[0],
						materialList2[1]
					};
					if (needAnguish || this.GetMaterialAttack(selectMaterials2) < 2400)
					{
						base.AI.SelectMaterials(selectMaterials2, 0);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x0008251C File Offset: 0x0008071C
		public bool UnchainedSoulOfAnguishActivate()
		{
			if (base.Card.Location == CardLocation.MonsterZone)
			{
				if (this.CheckWhetherNegated(true, false, (CardType)0))
				{
					return false;
				}
				List<ClientCard> targetList = (from card in base.Enemy.GetMonsters()
					where card.IsFaceup() && !card.IsShouldNotBeTarget() && !card.IsShouldNotBeMonsterTarget()
					orderby card.Attack descending
					select card).ToList<ClientCard>();
				if (targetList.Count<ClientCard>() > 0)
				{
					this.currentDestroyCardList.Add(targetList[0]);
					int summonId = 0;
					if (base.Bot.HasInExtra(29479265) && this.GetProblematicEnemyMonster(3000, false, true, (CardType)0) == null)
					{
						summonId = 29479265;
					}
					else if (this.banSpSummonExceptFiendCount == 0 && base.Bot.HasInExtra(29301450) && this.GetProblematicEnemyCardList(true, false, CardType.Monster).Count<ClientCard>() > 0)
					{
						summonId = 29301450;
					}
					else if (base.Bot.HasInExtra(67680512))
					{
						summonId = 67680512;
					}
					if (summonId > 0)
					{
						List<ClientCard> materialList = new List<ClientCard>(targetList) { base.Card };
						Logger.DebugWriteLine("*** Anguish select: " + summonId.ToString());
						base.AI.SelectCard(targetList);
						base.AI.SelectNextCard(summonId);
						base.AI.SelectMaterials(materialList, 0);
						this.activatedCardIdList.Add(base.Card.Id);
					}
					return true;
				}
			}
			return base.Card.Location == CardLocation.Grave && this.UnchainRecycleActivate();
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x000826B8 File Offset: 0x000808B8
		public bool UnchainedSoulLordOfYamaSpSummon()
		{
			if (this.CheckShouldNoMoreSpSummon(false))
			{
				return false;
			}
			if (base.Bot.HasInMonstersZone(24269961, false, false, false) || this.activatedCardIdList.Contains(24269961))
			{
				return false;
			}
			bool need3Monster = base.Bot.HasInExtra(93084621) && !base.Bot.HasInMonstersZone(93084621, false, false, false) && !this.activatedCardIdList.Contains(93084621) && this.GetProblematicEnemyMonster(0, true, false, CardType.Monster) != null;
			need3Monster |= this.CheckAtAdvantage() && base.Duel.Phase == DuelPhase.Main2 && base.Bot.HasInExtra(67680512) && !base.Bot.HasInMonstersZone(67680512, false, false, false);
			bool haveUnchainSoul = base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && card.HasSetcode(304));
			if (need3Monster)
			{
				need3Monster = base.Bot.HasInExtra(67680512);
				need3Monster |= base.Bot.HasInExtra(29479265);
				need3Monster |= base.Bot.HasInExtra(29301450) && this.banSpSummonExceptFiendCount == 0;
			}
			List<ClientCard> materialList = this.GetCanBeUsedForLinkMaterial(need3Monster, (ClientCard card) => !card.HasRace(CardRace.Fiend) || (card.HasType(CardType.Link) && card.HasSetcode(304)));
			Logger.DebugWriteLine("[Yama Summon]need3Monster = " + need3Monster.ToString() + ", material count = " + materialList.Count<ClientCard>().ToString());
			for (int index = 0; index < materialList.Count<ClientCard>() - 1; index++)
			{
				ClientCard material = materialList[index];
				for (int index2 = index + 1; index2 < materialList.Count<ClientCard>(); index2++)
				{
					ClientCard material2 = materialList[index2];
					List<ClientCard> selectMaterials = new List<ClientCard> { material, material2 };
					if (need3Monster && materialList.Count<ClientCard>() == 2 && (this.activatedCardIdList.Contains(41165831) || base.Bot.GetSpells().Count<ClientCard>() == 0) && (this.GetProblematicEnemyMonster(0, false, false, (CardType)0) != null || !this.CheckCanDirectAttack() || this.GetMaterialAttack(selectMaterials) >= 2000))
					{
						return false;
					}
					if (need3Monster | (base.Enemy.GetMonsterCount() == 0 && this.GetMaterialAttack(selectMaterials) < 2000) | (this.CheckAtAdvantage() && !haveUnchainSoul))
					{
						base.AI.SelectMaterials(selectMaterials, 0);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x00082954 File Offset: 0x00080B54
		public bool UnchainedSoulLordOfYamaActivate()
		{
			if (base.Card.Location != CardLocation.MonsterZone || (base.ActivateDescription != base.Util.GetStringId(24269961, 0) && base.ActivateDescription != -1))
			{
				if (base.Card.Location == CardLocation.Grave)
				{
					ClientCard chaosAngel = null;
					ClientCard abomination = null;
					ClientCard lady = null;
					ClientCard lovely = null;
					ClientCard arianna = null;
					ClientCard bestAttack = null;
					ClientCard rage = null;
					foreach (ClientCard grave in base.Bot.Graveyard)
					{
						if (grave.IsCode(22850702) && grave.ProcCompleted != 0 && !this.dimensionalBarrierAnnouced.Contains(1063))
						{
							chaosAngel = grave;
						}
						if (grave.IsCode(67680512) && grave.ProcCompleted != 0)
						{
							rage = grave;
						}
						if (grave.IsCode(29479265) && grave.ProcCompleted != 0)
						{
							abomination = grave;
						}
						if (grave.IsCode(81497285))
						{
							lady = grave;
						}
						if (grave.IsCode(2347656))
						{
							lovely = grave;
						}
						if (grave.IsCode(1225009))
						{
							arianna = grave;
						}
						if (base.Card != grave && grave.IsMonster() && grave.HasRace(CardRace.Fiend) && grave.IsCanRevive() && (bestAttack == null || grave.Attack > bestAttack.Attack))
						{
							bestAttack = grave;
						}
					}
					ClientCard select = null;
					bool destroyWelcome = false;
					if (chaosAngel != null && (this.GetProblematicEnemyCardList(false, false, CardType.Monster).Count<ClientCard>() > 0 || (base.Bot.GetMonsterCount() == 0 && base.Duel.Phase > DuelPhase.Main1 && base.Duel.Phase < DuelPhase.Main2)))
					{
						select = chaosAngel;
					}
					if (select == null && abomination != null && (this.GetProblematicEnemyCardList(false, false, CardType.Monster).Count<ClientCard>() > 0 || (base.Bot.GetMonsterCount() == 0 && base.Duel.Phase > DuelPhase.Main1 && base.Duel.Phase < DuelPhase.Main2)))
					{
						select = abomination;
						Logger.DebugWriteLine("[Yama] timing: " + base.CurrentTiming.ToString());
						if (base.Bot.HasInSpellZone(5380979, false, false) && (base.Duel.Phase <= DuelPhase.Main1 || base.Duel.Phase >= DuelPhase.Main2) && !this.activatedCardIdList.Contains(29479265))
						{
							destroyWelcome = true;
						}
					}
					if (select == null && rage != null && (base.Duel.Player == 0 || (!this.activatedCardIdList.Contains(67680512) && (base.Duel.Phase == DuelPhase.Main1 || base.Duel.Phase == DuelPhase.Main2))) && base.Bot.HasInExtra(new List<int> { 93084621, 29301450 }))
					{
						select = rage;
					}
					if (select == null && arianna != null && base.Duel.Player == 0 && !this.activatedCardIdList.Contains(1225009))
					{
						select = arianna;
					}
					if (select == null && lovely != null && base.Duel.Player == 1 && base.Util.GetBestAttack(base.Enemy) < 2900)
					{
						select = lovely;
					}
					if (select == null && lady != null && base.Duel.Player == 1 && base.Util.GetBestAttack(base.Enemy) < 3000)
					{
						select = lady;
					}
					if (select == null && arianna != null && !this.activatedCardIdList.Contains(1225009))
					{
						select = arianna;
					}
					if (select == null && bestAttack != null)
					{
						select = bestAttack;
					}
					if (select != null)
					{
						this.activatedCardIdList.Add(base.Card.Id + 1);
						base.AI.SelectCard(select);
						if (destroyWelcome)
						{
							base.AI.SelectYesNo(true);
							base.AI.SelectNextCard(5380979);
						}
						else
						{
							base.AI.SelectYesNo(false);
						}
						return true;
					}
				}
				return false;
			}
			if (this.CheckWhetherNegated(true, false, (CardType)0))
			{
				return false;
			}
			base.AI.SelectCard(new int[] { 41165831, 29479265, 93084621, 67680512 });
			this.activatedCardIdList.Add(base.Card.Id);
			return true;
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x00082D98 File Offset: 0x00080F98
		public bool UnchainedSoulOfRageSpSummon()
		{
			if (this.CheckShouldNoMoreSpSummon(false) || this.CheckWhetherNegated(true, true, (CardType)67108865))
			{
				return false;
			}
			if (base.Bot.HasInMonstersZone(67680512, false, false, false))
			{
				return false;
			}
			ClientCard unchained = base.Bot.GetMonsters().FirstOrDefault((ClientCard card) => card.IsFaceup() && card.HasSetcode(304) && !card.IsCode(new int[] { 93084621, 29479265 }));
			if (unchained == null)
			{
				return false;
			}
			bool summonFlag = this.CheckAtAdvantage() && base.Util.IsTurn1OrMain2();
			summonFlag |= (!base.Bot.HasInExtra(93084621) || this.activatedCardIdList.Contains(93084621)) && base.Util.IsTurn1OrMain2();
			if (summonFlag)
			{
				summonFlag = base.Bot.HasInExtra(93084621);
				summonFlag |= base.Bot.HasInExtra(29301450) && this.banSpSummonExceptFiendCount == 0;
			}
			List<ClientCard> materialList = this.GetCanBeUsedForLinkMaterial(base.Util.IsTurn1OrMain2(), (ClientCard card) => !card.HasRace(CardRace.Fiend) || card == unchained);
			if (materialList.Count<ClientCard>() > 0)
			{
				List<ClientCard> selectMaterials = new List<ClientCard>
				{
					unchained,
					materialList[0]
				};
				summonFlag |= base.Enemy.GetMonsterCount() == 0 && this.GetBotCurrentTotalAttack(null) < base.Enemy.LifePoints && this.GetBotCurrentTotalAttack(selectMaterials) + 1800 >= base.Enemy.LifePoints;
				if (summonFlag)
				{
					base.AI.SelectMaterials(selectMaterials, 0);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x00082F3C File Offset: 0x0008113C
		public bool UnchainedSoulOfRageActivate()
		{
			if (base.Card.Location == CardLocation.MonsterZone)
			{
				if (this.CheckWhetherNegated(true, false, (CardType)0))
				{
					return false;
				}
				bool flag = base.DefaultOnBecomeTarget() && !base.Util.ChainContainsCard(53417695);
				ClientCard problemMonster = this.GetProblematicEnemyMonster(-1, true, true, CardType.Monster);
				List<ClientCard> targetList = (from card in base.Enemy.GetMonsters()
					where card.IsFaceup() && !card.IsShouldNotBeTarget() && !card.IsShouldNotBeTarget()
					orderby card.Attack
					select card).ToList<ClientCard>();
				if (problemMonster != null)
				{
					targetList.Insert(0, problemMonster);
				}
				if ((flag | ((base.CurrentTiming & 4) > 0 && base.Util.IsOneEnemyBetterThanValue(base.Card.Attack, true)) | (problemMonster != null)) && targetList.Count<ClientCard>() > 0)
				{
					ClientCard target = targetList[0];
					int summonId = 0;
					if (base.Bot.HasInExtra(29479265) && this.GetProblematicEnemyMonster(3000, false, false, (CardType)0) == null && target.HasType(CardType.Link) && target.LinkCount == 2)
					{
						summonId = 93084621;
					}
					else if (this.banSpSummonExceptFiendCount == 0 && base.Bot.HasInExtra(29301450))
					{
						summonId = 29301450;
					}
					else if (base.Bot.HasInExtra(93084621) && this.GetProblematicEnemyMonster(2400, false, false, (CardType)0) == null)
					{
						summonId = 93084621;
					}
					List<ClientCard> materialList = new List<ClientCard>(targetList) { base.Card };
					base.AI.SelectCard(targetList);
					base.AI.SelectNextCard(summonId);
					base.AI.SelectMaterials(materialList, 0);
					this.activatedCardIdList.Add(base.Card.Id);
					this.escapeTargetList.Add(base.Card);
					this.currentDestroyCardList.Add(target);
					return true;
				}
			}
			return base.Card.Location == CardLocation.Grave && this.UnchainRecycleActivate();
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x0008314A File Offset: 0x0008134A
		public bool UnchainRecycleActivate()
		{
			base.AI.SelectCard(new int[] { 41165831, 2347656, 1225009, 29479265, 74018812, 37629703, 2511, 73602965, 75730490 });
			this.activatedCardIdList.Add(base.Card.Id + 1);
			return true;
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x00083184 File Offset: 0x00081384
		public bool SPLittleKnightSpSummon()
		{
			if (this.CheckCanDirectAttack())
			{
				List<ClientCard> materialList = this.SPLittleKnightSelectMaterial(false);
				if (materialList.Count<ClientCard>() >= 2 && this.GetMaterialAttack(materialList) < 1600)
				{
					base.AI.SelectMaterials(materialList, 0);
					return true;
				}
			}
			else if (!this.CheckWhetherNegated(true, true, (CardType)67108865) && this.GetProblematicEnemyCardList(true, false, CardType.Monster).Count<ClientCard>() > 0)
			{
				List<ClientCard> materialList2 = this.SPLittleKnightSelectMaterial(true);
				if (materialList2.Count<ClientCard>() >= 2)
				{
					if (materialList2.Any((ClientCard card) => card.HasType((CardType)75505728)))
					{
						base.AI.SelectMaterials(materialList2, 0);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x00083234 File Offset: 0x00081434
		public List<ClientCard> SPLittleKnightSelectMaterial(bool needToUseEffect = false)
		{
			List<ClientCard> usedMaterialList = new List<ClientCard>();
			if (base.Bot.GetMonstersExtraZoneCount() > 0)
			{
				ClientCard botMonsterExtraZome = base.Bot.GetMonstersInExtraZone()[0];
				if (botMonsterExtraZome.HasType((CardType)25174080) || botMonsterExtraZome.IsCode(94259633))
				{
					usedMaterialList.Add(botMonsterExtraZome);
					if (botMonsterExtraZome.HasType((CardType)75505728))
					{
						needToUseEffect = false;
					}
				}
				List<ClientCard> materialList = this.GetCanBeUsedForLinkMaterial(true, (ClientCard card) => card == botMonsterExtraZome);
				if (materialList.Count<ClientCard>() > 0)
				{
					foreach (ClientCard card2 in materialList)
					{
						if (!needToUseEffect || card2.HasType((CardType)8396864) || (card2.HasType(CardType.Link) && card2.LinkCount <= 2))
						{
							usedMaterialList.Add(card2);
							if (card2.HasType((CardType)75505728))
							{
								needToUseEffect = false;
							}
						}
						if (usedMaterialList.Count<ClientCard>() >= 2)
						{
							break;
						}
					}
				}
				if (usedMaterialList.Count<ClientCard>() < 2)
				{
					usedMaterialList.Clear();
				}
			}
			else
			{
				List<ClientCard> materialList2 = this.GetCanBeUsedForLinkMaterial(true, (ClientCard card) => !needToUseEffect || card.HasType((CardType)8396864) || (card.HasType(CardType.Link) && card.LinkCount <= 2));
				if (materialList2.Count<ClientCard>() >= 2)
				{
					for (int idx = 0; idx < materialList2.Count<ClientCard>() - 1; idx++)
					{
						ClientCard material = materialList2[idx];
						if (!material.HasType(CardType.Link) || material.LinkCount < 3)
						{
							bool flag = !needToUseEffect || material.HasType((CardType)75505728);
							for (int idx2 = 0; idx2 < materialList2.Count<ClientCard>(); idx2++)
							{
								ClientCard material2 = materialList2[idx2];
								if (!material2.HasType(CardType.Link) || material2.LinkCount < 3)
								{
									bool flag2 = !needToUseEffect || material2.HasType((CardType)75505728);
									if (flag || flag2)
									{
										return new List<ClientCard> { material, material2 };
									}
								}
							}
						}
					}
				}
			}
			return usedMaterialList;
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x00083484 File Offset: 0x00081684
		public bool SPLittleKnightActivate()
		{
			if (base.ActivateDescription == -1 || base.ActivateDescription == base.Util.GetStringId(29301450, 0))
			{
				List<ClientCard> problemCardList = this.GetProblematicEnemyCardList(true, false, CardType.Monster);
				problemCardList.AddRange(this.GetDangerousCardinEnemyGrave(false));
				problemCardList.AddRange(this.GetNormalEnemyTargetList(true, true, CardType.Monster));
				problemCardList.AddRange(from card in base.Enemy.Graveyard
					where card.HasType(CardType.Monster)
					orderby card.Attack descending
					select card);
				problemCardList.AddRange(base.Enemy.Graveyard.Where((ClientCard card) => !card.HasType(CardType.Monster)));
				if (problemCardList.Count<ClientCard>() > 0)
				{
					base.AI.SelectCard(problemCardList);
					this.activatedCardIdList.Add(base.Card.Id);
					return true;
				}
			}
			else if (base.ActivateDescription == base.Util.GetStringId(29301450, 1))
			{
				ClientCard selfMonster = null;
				foreach (ClientCard target in base.Bot.GetMonsters())
				{
					if (base.Duel.ChainTargets.Contains(target) && !this.escapeTargetList.Contains(target))
					{
						selfMonster = target;
						break;
					}
				}
				if (selfMonster == null && base.Duel.Player == 1)
				{
					selfMonster = (from card in base.Bot.GetMonsters()
						where card.IsAttack()
						orderby card.Attack
						select card).FirstOrDefault<ClientCard>();
					if (!base.Util.IsOneEnemyBetterThanValue(selfMonster.Attack, true))
					{
						selfMonster = null;
					}
				}
				if (selfMonster != null)
				{
					ClientCard nextMonster = null;
					List<ClientCard> selfTargetList = (from card in base.Bot.GetMonsters()
						where card != selfMonster
						select card).ToList<ClientCard>();
					if (base.Enemy.GetMonsterCount() == 0 && selfTargetList.Count<ClientCard>() > 0)
					{
						selfTargetList.Sort(new Comparison<ClientCard>(this.CompareUsableAttack));
						nextMonster = selfTargetList[0];
						this.escapeTargetList.Add(nextMonster);
					}
					if (base.Enemy.GetMonsterCount() > 0)
					{
						nextMonster = this.GetBestEnemyMonster(false, true, true, (CardType)0);
						this.currentDestroyCardList.Add(nextMonster);
					}
					if (nextMonster != null)
					{
						base.AI.SelectCard(selfMonster);
						base.AI.SelectNextCard(nextMonster);
						this.escapeTargetList.Add(selfMonster);
						this.activatedCardIdList.Add(base.Card.Id + 1);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x000837B0 File Offset: 0x000819B0
		public bool MuckrakerFromTheUnderworldSpSummon()
		{
			List<ClientCard> materialList = this.GetCanBeUsedForLinkMaterial(true, (ClientCard card) => card.HasType(CardType.Link));
			if (materialList.Count<ClientCard>() < 2)
			{
				return false;
			}
			bool willBeNegated = this.CheckWhetherNegated(true, true, (CardType)67108865) && base.Bot.Hand.Count<ClientCard>() > 0;
			bool canRebornAngel = base.Bot.Graveyard.Any((ClientCard card) => card.IsCanRevive() && card.IsCode(22850702)) && !willBeNegated;
			bool canRebornLovely = base.Bot.Graveyard.Any((ClientCard card) => card.IsCode(2347656)) && !willBeNegated;
			int bestAttackGrave = 0;
			bool chaosAngelFlag = this.GetProblematicEnemyCardList(true, false, CardType.Monster).Count<ClientCard>() > 0 && !this.CheckWhetherNegated(true, true, CardType.Monster);
			foreach (ClientCard grave in base.Bot.Graveyard)
			{
				if (grave.IsMonster() && grave.HasRace(CardRace.Fiend) && grave.IsCanRevive() && grave.Attack > bestAttackGrave)
				{
					bestAttackGrave = grave.Attack;
				}
			}
			for (int idx = 0; idx < materialList.Count<ClientCard>() - 1; idx++)
			{
				ClientCard material = materialList[idx];
				int idx2 = idx + 1;
				while (idx2 < materialList.Count<ClientCard>())
				{
					ClientCard material2 = materialList[idx];
					List<ClientCard> currentList = new List<ClientCard> { material, material2 };
					bool flag;
					if (chaosAngelFlag)
					{
						if (!canRebornAngel)
						{
							flag = currentList.Any((ClientCard card) => card.IsCode(22850702)) && !willBeNegated;
						}
						else
						{
							flag = true;
						}
					}
					else
					{
						flag = false;
					}
					bool summonFlag = flag;
					summonFlag |= base.Enemy.GetMonsterCount() == 0 && canRebornLovely;
					bool flag2 = summonFlag;
					if (this.activatedCardIdList.Contains(2347656))
					{
						goto IL_0251;
					}
					if (!base.Bot.Graveyard.Any((ClientCard card) => card.Type == 4))
					{
						goto IL_0251;
					}
					bool flag3 = currentList.Any((ClientCard card) => card.IsDisabled() && card.IsCode(2347656));
					IL_0252:
					summonFlag = flag2 || flag3;
					if (this.CheckCanDirectAttack())
					{
						summonFlag |= this.GetBotCurrentTotalAttack(null) < base.Enemy.LifePoints && this.GetBotCurrentTotalAttack(currentList) + bestAttackGrave >= base.Enemy.LifePoints;
						summonFlag |= this.GetMaterialAttack(currentList) < 1000;
					}
					if (summonFlag)
					{
						base.AI.SelectMaterials(currentList, 0);
						return true;
					}
					idx2++;
					continue;
					IL_0251:
					flag3 = false;
					goto IL_0252;
				}
			}
			return false;
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x00083AB4 File Offset: 0x00081CB4
		public bool MuckrakerFromTheUnderworldActivate()
		{
			if (base.ActivateDescription == base.Util.GetStringId(71607202, 0))
			{
				if (this.CheckWhetherNegated(true, false, (CardType)0))
				{
					return false;
				}
				ClientCard chaosAngel = null;
				ClientCard lovely = null;
				ClientCard arianna = null;
				ClientCard bestAttack = null;
				foreach (ClientCard grave in base.Bot.Graveyard)
				{
					if (grave.IsCode(22850702) && grave.ProcCompleted != 0 && !this.dimensionalBarrierAnnouced.Contains(1063))
					{
						chaosAngel = grave;
					}
					if (grave.IsCode(2347656))
					{
						lovely = grave;
					}
					if (grave.IsCode(1225009))
					{
						arianna = grave;
					}
					if (base.Card != grave && grave.IsMonster() && grave.HasRace(CardRace.Fiend) && grave.IsCanRevive() && (bestAttack == null || grave.Attack > bestAttack.Attack))
					{
						bestAttack = grave;
					}
				}
				ClientCard rebornTarget = null;
				if (chaosAngel != null && this.GetProblematicEnemyCardList(true, false, CardType.Monster).Count<ClientCard>() > 0)
				{
					rebornTarget = chaosAngel;
				}
				if (rebornTarget == null && lovely != null && base.Util.GetBestAttack(base.Enemy) < 2900 && (!this.activatedCardIdList.Contains(2347656) || base.Bot.HasInSpellZoneOrInGraveyard(92714517)))
				{
					rebornTarget = lovely;
				}
				if (rebornTarget == null && bestAttack != null && this.CheckCanDirectAttack() && this.GetBotCurrentTotalAttack(null) < base.Enemy.LifePoints && this.GetBotCurrentTotalAttack(null) + bestAttack.Attack >= base.Enemy.LifePoints)
				{
					rebornTarget = bestAttack;
				}
				if (rebornTarget == null && arianna != null && base.Duel.Player == 0 && !this.activatedCardIdList.Contains(1225009))
				{
					rebornTarget = arianna;
				}
				if (rebornTarget == null && bestAttack != null)
				{
					rebornTarget = bestAttack;
				}
				if (rebornTarget != null)
				{
					base.AI.SelectCard(rebornTarget);
					base.AI.SelectNextCard(this.FurnitureGetCost(false, null));
					this.activatedCardIdList.Contains(base.Card.Id);
					this.banSpSummonExceptFiendCount = Math.Max(1, this.banSpSummonExceptFiendCount);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x00083CEC File Offset: 0x00081EEC
		public bool RelinquishedAnimaSpSummon()
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0))
			{
				return false;
			}
			ClientCard enemyLeftEx = base.Enemy.MonsterZone[6];
			if (enemyLeftEx != null && enemyLeftEx.HasLinkMarker(128) && !enemyLeftEx.IsShouldNotBeTarget() && !enemyLeftEx.IsShouldNotBeMonsterTarget())
			{
				ClientCard selfMonsterZone = base.Bot.MonsterZone[1];
				if (selfMonsterZone == null)
				{
					base.AI.SelectMaterials(2511, 0);
					base.AI.SelectPlace(2);
					return true;
				}
				if (!selfMonsterZone.HasType((CardType)75513856) && selfMonsterZone.Level == 1)
				{
					base.AI.SelectMaterials(selfMonsterZone, 0);
					base.AI.SelectPlace(2);
					return true;
				}
			}
			ClientCard enemyRightEx = base.Enemy.MonsterZone[5];
			if (enemyRightEx != null && enemyRightEx.HasLinkMarker(128) && !enemyRightEx.IsShouldNotBeTarget() && !enemyRightEx.IsShouldNotBeMonsterTarget())
			{
				ClientCard selfMonsterZone2 = base.Bot.MonsterZone[3];
				if (selfMonsterZone2 == null)
				{
					base.AI.SelectMaterials(2511, 0);
					base.AI.SelectPlace(8);
					return true;
				}
				if (!selfMonsterZone2.HasType((CardType)75513856) && selfMonsterZone2.Level == 1)
				{
					base.AI.SelectMaterials(selfMonsterZone2, 0);
					base.AI.SelectPlace(8);
					return true;
				}
			}
			if (base.Bot.MonsterZone[5] != null || base.Bot.MonsterZone[6] != null)
			{
				return false;
			}
			ClientCard enemyMonsterLeft = base.Enemy.MonsterZone[3];
			ClientCard enemyMonsterRight = base.Enemy.MonsterZone[1];
			if (base.Enemy.MonsterZone[6] != null)
			{
				enemyMonsterLeft = null;
			}
			if (enemyMonsterLeft != null && enemyMonsterLeft.IsFacedown())
			{
				enemyMonsterLeft = null;
			}
			if (enemyMonsterLeft != null && (enemyMonsterLeft.IsShouldNotBeMonsterTarget() || enemyMonsterLeft.IsShouldNotBeTarget()))
			{
				enemyMonsterLeft = null;
			}
			if (base.Enemy.MonsterZone[5] != null)
			{
				enemyMonsterRight = null;
			}
			if (enemyMonsterRight != null && (enemyMonsterRight.IsShouldNotBeMonsterTarget() || enemyMonsterRight.IsShouldNotBeTarget()))
			{
				enemyMonsterRight = null;
			}
			if (enemyMonsterRight != null && enemyMonsterRight.IsFacedown())
			{
				enemyMonsterRight = null;
			}
			int place = -1;
			if (enemyMonsterLeft != null && enemyMonsterRight == null)
			{
				place = 32;
			}
			if (enemyMonsterLeft == null && enemyMonsterRight != null)
			{
				place = 64;
			}
			if (enemyMonsterLeft != null && enemyMonsterRight != null)
			{
				if (enemyMonsterLeft.IsFloodgate() && !enemyMonsterRight.IsFloodgate())
				{
					place = 32;
				}
				else if (!enemyMonsterLeft.IsFloodgate() && enemyMonsterRight.IsFloodgate())
				{
					place = 64;
				}
				else if (enemyMonsterLeft.GetDefensePower() >= enemyMonsterRight.GetDefensePower())
				{
					place = 32;
				}
				else
				{
					place = 64;
				}
			}
			if (place >= 0)
			{
				base.AI.SelectMaterials((from card in base.Bot.GetMonsters()
					where card.IsFaceup() && !card.HasType((CardType)75513856) && card.Level == 1
					orderby card.Attack
					select card).ToList<ClientCard>(), 0);
				base.AI.SelectPlace(place);
				return true;
			}
			if (base.Bot.HasInExtra(29301450))
			{
				if (base.Bot.GetMonsters().Count((ClientCard card) => card.IsFaceup()) >= 2)
				{
					if (!base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && card.HasType((CardType)75505728)) && this.GetProblematicEnemyCardList(true, false, CardType.Monster).Count<ClientCard>() > 0)
					{
						base.AI.SelectMaterials((from card in base.Bot.GetMonsters()
							where card.IsFaceup() && !card.HasType((CardType)75513856) && card.Level == 1
							orderby card.Attack
							select card).ToList<ClientCard>(), 0);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x000840A4 File Offset: 0x000822A4
		public bool RelinquishedAnimaActivate()
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0))
			{
				return false;
			}
			this.activatedCardIdList.Add(base.Card.Id);
			foreach (KeyValuePair<int, int> placePair in new Dictionary<int, int>
			{
				{ 1, 6 },
				{ 3, 5 },
				{ 5, 3 },
				{ 6, 1 }
			})
			{
				if (base.Bot.MonsterZone[placePair.Key] == base.Card && base.Enemy.MonsterZone[placePair.Value] != null)
				{
					this.currentDestroyCardList.Add(base.Enemy.MonsterZone[placePair.Value]);
					break;
				}
			}
			return true;
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x00084184 File Offset: 0x00082384
		public bool MonsterRepos()
		{
			if (base.Card.Attack + 1 <= 1)
			{
				return !base.Card.IsDefense();
			}
			int bestAttack = 0;
			foreach (ClientCard clientCard in base.Bot.GetMonsters())
			{
				int attack = clientCard.Attack;
				if (attack >= bestAttack)
				{
					bestAttack = attack;
				}
			}
			bool enemyBetter = base.Util.IsAllEnemyBetterThanValue(bestAttack, true);
			return (base.Card.IsAttack() && enemyBetter) || (base.Card.IsDefense() && !enemyBetter);
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x00084234 File Offset: 0x00082434
		public bool ReposForLabrynth()
		{
			return !this.activatedCardIdList.Contains(92714517) && base.Bot.HasInSpellZoneOrInGraveyard(92714517) && base.Card.IsFacedown();
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x00084268 File Offset: 0x00082468
		public bool SpellSetCheck()
		{
			if (base.Duel.Phase == DuelPhase.Main1 && base.Bot.HasAttackingMonster() && base.Duel.Turn > 1)
			{
				return false;
			}
			if (base.Card.IsCode(92714517) && base.Bot.HasInSpellZone(base.Card.Id, false, false))
			{
				return false;
			}
			if (base.Card.IsCode(6351147) && !base.Bot.HasInSpellZone(6351147, false, false))
			{
				bool haveCopyTrap = false;
				if (base.Enemy.Graveyard.Any((ClientCard card) => card.IsCode(new int[] { 5380979, 92714517, 10045474, 83326048, 30748475, 94192409, 78474168 })))
				{
					haveCopyTrap = true;
				}
				if (!haveCopyTrap && !base.Bot.HasInHand(41165831))
				{
					return false;
				}
			}
			if (base.Card.IsCode(53417695))
			{
				if (!base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && card.HasSetcode(304)))
				{
					return false;
				}
			}
			if (base.Card.IsTrap() || base.Card.HasType(CardType.QuickPlay))
			{
				List<int> avoidList = new List<int>();
				int setFornfiniteImpermanence = 0;
				for (int i = 0; i < 5; i++)
				{
					if (base.Enemy.SpellZone[i] != null && base.Enemy.SpellZone[i].IsFaceup() && base.Bot.SpellZone[4 - i] == null)
					{
						avoidList.Add(4 - i);
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
				this.SelectSTPlace(base.Card, false, avoidList);
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

		// Token: 0x06001570 RID: 5488 RVA: 0x000844B8 File Offset: 0x000826B8
		public bool SpellSetForCooClockCheck()
		{
			if (base.Card.IsCode(new int[] { 49238328, 6351147, 5380979 }) && base.Bot.HasInHand(41165831) && !this.activatedCardIdList.Contains(41165831))
			{
				this.SelectSTPlace(base.Card, false, null);
				return true;
			}
			bool haveLabrynth = base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && card.HasSetcode(382));
			if (!this.cooclockAffected || (haveLabrynth && base.Bot.HasInHand(2511) && !this.activatedCardIdList.Contains(2511)))
			{
				return false;
			}
			if (!base.Card.IsCode(new int[] { 92714517, 5380979, 10045474, 83326048 }))
			{
				return false;
			}
			if (haveLabrynth)
			{
				this.SelectSTPlace(base.Card, true, null);
				return true;
			}
			if (!base.Card.IsCode(new int[] { 92714517, 5380979 }))
			{
				return false;
			}
			if (!this.summoned)
			{
				if (base.Bot.Hand.Any((ClientCard card) => card.IsMonster() && card.Level <= 4 && card.HasSetcode(382)))
				{
					this.SelectSTPlace(base.Card, true, null);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400197D RID: 6525
		private const int SetcodeTimeLord = 74;

		// Token: 0x0400197E RID: 6526
		private const int SetcodePhantom = 219;

		// Token: 0x0400197F RID: 6527
		private const int SetcodeOrcust = 283;

		// Token: 0x04001980 RID: 6528
		private const int SetcodeUnchained = 304;

		// Token: 0x04001981 RID: 6529
		private const int SetcodeLabrynth = 382;

		// Token: 0x04001982 RID: 6530
		private const int SetcodeHorus = 413;

		// Token: 0x04001983 RID: 6531
		private const int hintTimingMainEnd = 4;

		// Token: 0x04001984 RID: 6532
		private const int hintBattleStart = 8;

		// Token: 0x04001985 RID: 6533
		private Dictionary<int, List<int>> DeckCountTable = new Dictionary<int, List<int>>
		{
			{
				3,
				new List<int> { 1225009, 37629703, 14558127, 23434538, 74018812, 2511, 10045474, 92714517 }
			},
			{
				2,
				new List<int> { 81497285, 73602965, 49238328, 5380979, 6351147 }
			},
			{
				1,
				new List<int> { 2347656, 41165831, 75730490, 30748475, 53417695, 83326048 }
			}
		};

		// Token: 0x04001986 RID: 6534
		private List<int> notToNegateIdList = new List<int> { 58699500, 20343502 };

		// Token: 0x04001987 RID: 6535
		private List<int> notToBeTrapTargetList = new List<int>
		{
			72144675, 86188410, 41589166, 11443677, 72566043, 1688285, 59071624, 6511113, 48183890, 952523,
			22423493, 73639099
		};

		// Token: 0x04001988 RID: 6536
		private List<int> targetNegateIdList = new List<int>
		{
			97268402, 10045474, 52038441, 78474168, 74003290, 67037924, 9753964, 66192538, 23204029, 73445448,
			35103106, 30286474, 45002991, 5795980, 38511382, 53742162, 30430448
		};

		// Token: 0x04001989 RID: 6537
		private List<int> notToDestroySpellTrap = new List<int> { 50005218, 6767771 };

		// Token: 0x0400198A RID: 6538
		private bool enemyActivateMaxxC;

		// Token: 0x0400198B RID: 6539
		private List<int> infiniteImpermanenceList = new List<int>();

		// Token: 0x0400198C RID: 6540
		private bool summoned;

		// Token: 0x0400198D RID: 6541
		private List<int> activatedCardIdList = new List<int>();

		// Token: 0x0400198E RID: 6542
		private List<ClientCard> currentNegateMonsterList = new List<ClientCard>();

		// Token: 0x0400198F RID: 6543
		private List<ClientCard> currentDestroyCardList = new List<ClientCard>();

		// Token: 0x04001990 RID: 6544
		private List<ClientCard> setTrapThisTurn = new List<ClientCard>();

		// Token: 0x04001991 RID: 6545
		private List<ClientCard> summonThisTurn = new List<ClientCard>();

		// Token: 0x04001992 RID: 6546
		private List<ClientCard> enemySetThisTurn = new List<ClientCard>();

		// Token: 0x04001993 RID: 6547
		private List<ClientCard> escapeTargetList = new List<ClientCard>();

		// Token: 0x04001994 RID: 6548
		private List<ClientCard> summonInChainList = new List<ClientCard>();

		// Token: 0x04001995 RID: 6549
		private bool cooclockAffected;

		// Token: 0x04001996 RID: 6550
		private bool cooclockActivating;

		// Token: 0x04001997 RID: 6551
		private bool furnitureActivating;

		// Token: 0x04001998 RID: 6552
		private bool dimensionBarrierAnnouncing;

		// Token: 0x04001999 RID: 6553
		private int banSpSummonExceptFiendCount;

		// Token: 0x0400199A RID: 6554
		private int dimensionShifterCount;

		// Token: 0x0400199B RID: 6555
		private int enemySpSummonFromExLastTurn;

		// Token: 0x0400199C RID: 6556
		private int enemySpSummonFromExThisTurn;

		// Token: 0x0400199D RID: 6557
		private bool enemyActivateInfiniteImpermanenceFromHand;

		// Token: 0x0400199E RID: 6558
		private int rollbackCopyCardId;

		// Token: 0x0400199F RID: 6559
		private List<int> dimensionalBarrierAnnouced = new List<int>();

		// Token: 0x040019A0 RID: 6560
		private List<int> chainSummoningIdList = new List<int>(3);

		// Token: 0x040019A1 RID: 6561
		private ClientCard bigwelcomeEscaseTarget;

		// Token: 0x02000322 RID: 802
		public class CardId
		{
			// Token: 0x040019A2 RID: 6562
			public const int LadyLabrynthOfTheSilverCastle = 81497285;

			// Token: 0x040019A3 RID: 6563
			public const int LovelyLabrynthOfTheSilverCastle = 2347656;

			// Token: 0x040019A4 RID: 6564
			public const int UnchainedSoulOfSharvara = 41165831;

			// Token: 0x040019A5 RID: 6565
			public const int AriasTheLabrynthButler = 73602965;

			// Token: 0x040019A6 RID: 6566
			public const int ArianeTheLabrynthServant = 75730490;

			// Token: 0x040019A7 RID: 6567
			public const int AriannaTheLabrynthServant = 1225009;

			// Token: 0x040019A8 RID: 6568
			public const int LabrynthChandraglier = 37629703;

			// Token: 0x040019A9 RID: 6569
			public const int LabrynthStovieTorbie = 74018812;

			// Token: 0x040019AA RID: 6570
			public const int LabrynthCooclock = 2511;

			// Token: 0x040019AB RID: 6571
			public const int PotOfExtravagance = 49238328;

			// Token: 0x040019AC RID: 6572
			public const int WelcomeLabrynth = 5380979;

			// Token: 0x040019AD RID: 6573
			public const int TransactionRollback = 6351147;

			// Token: 0x040019AE RID: 6574
			public const int DestructiveDarumaKarmaCannon = 30748475;

			// Token: 0x040019AF RID: 6575
			public const int EscapeOfTheUnchained = 53417695;

			// Token: 0x040019B0 RID: 6576
			public const int BigWelcomeLabrynth = 92714517;

			// Token: 0x040019B1 RID: 6577
			public const int ChaosAngel = 22850702;

			// Token: 0x040019B2 RID: 6578
			public const int SuperStarslayerTYPHON = 93039339;

			// Token: 0x040019B3 RID: 6579
			public const int UnchainedAbomination = 29479265;

			// Token: 0x040019B4 RID: 6580
			public const int UnchainedSoulOfAnguish = 93084621;

			// Token: 0x040019B5 RID: 6581
			public const int UnchainedSoulLordOfYama = 24269961;

			// Token: 0x040019B6 RID: 6582
			public const int UnchainedSoulOfRage = 67680512;

			// Token: 0x040019B7 RID: 6583
			public const int SPLittleKnight = 29301450;

			// Token: 0x040019B8 RID: 6584
			public const int MuckrakerFromTheUnderworld = 71607202;

			// Token: 0x040019B9 RID: 6585
			public const int RelinquishedAnima = 94259633;

			// Token: 0x040019BA RID: 6586
			public const int NaturalExterio = 99916754;

			// Token: 0x040019BB RID: 6587
			public const int NaturalBeast = 33198837;

			// Token: 0x040019BC RID: 6588
			public const int ImperialOrder = 61740673;

			// Token: 0x040019BD RID: 6589
			public const int SwordsmanLV7 = 37267041;

			// Token: 0x040019BE RID: 6590
			public const int RoyalDecree = 51452091;

			// Token: 0x040019BF RID: 6591
			public const int Number41BagooskatheTerriblyTiredTapir = 90590303;

			// Token: 0x040019C0 RID: 6592
			public const int InspectorBoarder = 15397015;

			// Token: 0x040019C1 RID: 6593
			public const int SkillDrain = 82732705;

			// Token: 0x040019C2 RID: 6594
			public const int DimensionShifter = 91800273;

			// Token: 0x040019C3 RID: 6595
			public const int MacroCosmos = 30241314;

			// Token: 0x040019C4 RID: 6596
			public const int DimensionalFissure = 81674782;

			// Token: 0x040019C5 RID: 6597
			public const int BanisheroftheRadiance = 94853057;

			// Token: 0x040019C6 RID: 6598
			public const int BanisheroftheLight = 61528025;

			// Token: 0x040019C7 RID: 6599
			public const int KashtiraAriseHeart = 48626373;

			// Token: 0x040019C8 RID: 6600
			public const int AccesscodeTalker = 86066372;

			// Token: 0x040019C9 RID: 6601
			public const int GhostMournerMoonlitChill = 52038441;
		}
	}
}
