using System;
using System.Collections.Generic;
using System.Linq;
using YGOSharp.OCGWrapper;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x020002D7 RID: 727
	[Deck("Dogmatika", "AI_Dogmatika", "Normal")]
	public class DogmatikaExecutor : DefaultExecutor
	{
		// Token: 0x060011ED RID: 4589 RVA: 0x0005E134 File Offset: 0x0005C334
		public DogmatikaExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 80845034, new Func<bool>(this.WANTED_SeekerOfSinfulSpoilsActivate));
			base.AddExecutor(ExecutorType.Activate, 60303245, new Func<bool>(this.SalamangreatAlmirajActivate));
			base.AddExecutor(ExecutorType.Activate, 74586817, new Func<bool>(this.PSYFramelordOmegaActivate));
			base.AddExecutor(ExecutorType.Activate, 51522296, new Func<bool>(this.DogmatikaAlbaZoaActivate));
			base.AddExecutor(ExecutorType.Activate, 95679145, new Func<bool>(this.DogmatikaMaximusActivate));
			base.AddExecutor(ExecutorType.Activate, 72270339, new Func<bool>(this.DiabellstarTheBlackWitchActivate));
			base.AddExecutor(ExecutorType.Activate, 62849088, new Func<bool>(this.ThesIrisSwordsoulActivate));
			base.AddExecutor(ExecutorType.Activate, 24224830, new Func<bool>(this.CalledbytheGraveActivate));
			base.AddExecutor(ExecutorType.Activate, 65681983, new Func<bool>(this.CrossoutDesignatorActivate));
			base.AddExecutor(ExecutorType.Activate, 14558127, new Func<bool>(this.AshBlossomActivate));
			base.AddExecutor(ExecutorType.Activate, 24842059, new Func<bool>(this.LinguribohActivate));
			base.AddExecutor(ExecutorType.Activate, 82956214, new Func<bool>(this.DogmatikaPunishmentActivate));
			base.AddExecutor(ExecutorType.Activate, 69680031, new Func<bool>(this.DogmatikaFleurdelisActivate));
			base.AddExecutor(ExecutorType.Activate, 10045474, new Func<bool>(this.InfiniteImpermanenceActivate));
			base.AddExecutor(ExecutorType.Activate, 16240772, new Func<bool>(this.SinfulSpoilsOfDoom_RcielaActivate));
			base.AddExecutor(ExecutorType.Activate, new Func<bool>(this.ClearIrisFlag));
			base.AddExecutor(ExecutorType.Activate, 79606837, new Func<bool>(this.HeraldOfTheArcLightActivate));
			base.AddExecutor(ExecutorType.Activate, 80532587, new Func<bool>(this.ElderEntityNtssActivate));
			base.AddExecutor(ExecutorType.Activate, 24915933, new Func<bool>(this.GranguignolTheDuskDragonActivate));
			base.AddExecutor(ExecutorType.Activate, 11765832, new Func<bool>(this.GaruraWingsOfResonantLifeActivate));
			base.AddExecutor(ExecutorType.Activate, 10158145, new Func<bool>(this.KnightmareCorruptorIbleeActivate));
			base.AddExecutor(ExecutorType.Activate, 41373230, new Func<bool>(this.TitanikladTheAshDragonActivate));
			base.AddExecutor(ExecutorType.Activate, 53971455, new Func<bool>(this.DespianLuluwalilithActivate));
			base.AddExecutor(ExecutorType.Activate, 23434538, new Func<bool>(this.MaxxCActivate));
			base.AddExecutor(ExecutorType.Activate, 93039339, new Func<bool>(this.SuperStarslayerTYPHONActivate));
			base.AddExecutor(ExecutorType.SpSummon, 24842059, new Func<bool>(this.LinguribohSpSummon));
			base.AddExecutor(ExecutorType.SpSummon, 60303245, new Func<bool>(this.SalamangreatAlmirajSpSummon));
			base.AddExecutor(ExecutorType.SpSummon, 2220237, new Func<bool>(this.SecureGardnaSpSummon));
			base.AddExecutor(ExecutorType.Summon, 10158145, new Func<bool>(this.KnightmareCorruptorIbleeSummon));
			base.AddExecutor(ExecutorType.Activate, 1984618, new Func<bool>(this.NadirServantActivate));
			base.AddExecutor(ExecutorType.Summon, 60303688, new Func<bool>(this.DogmatikaEcclesiaSummon));
			base.AddExecutor(ExecutorType.Activate, 60303688, new Func<bool>(this.DogmatikaEcclesiaActivate));
			base.AddExecutor(ExecutorType.Activate, 35569555, new Func<bool>(this.DogmatikaMatrixActivate));
			base.AddExecutor(ExecutorType.Activate, 31002402, new Func<bool>(this.DogmatikaLamityActivate));
			base.AddExecutor(ExecutorType.Activate, 60921537, new Func<bool>(this.DogmatikaMacabreActivate));
			base.AddExecutor(ExecutorType.Activate, 69680031, new Func<bool>(this.DogmatikaFleurdelisDelayActivate));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(this.MonsterRepos));
			base.AddExecutor(ExecutorType.Summon, new Func<bool>(this.SummonForTYPHONCheck));
			base.AddExecutor(ExecutorType.SpSummon, 72270339, new Func<bool>(this.DiabellstarTheBlackWitchSpSummon));
			base.AddExecutor(ExecutorType.Activate, 31002402, new Func<bool>(this.DogmatikaLamityDelayActivate));
			base.AddExecutor(ExecutorType.SpSummon, 93039339, new Func<bool>(this.SuperStarslayerTYPHONSpSummon));
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(this.SpellSetCheck));
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x0005E700 File Offset: 0x0005C900
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

		// Token: 0x060011EF RID: 4591 RVA: 0x0005E76E File Offset: 0x0005C96E
		public void UpdateBanSpSummonFromExTurn(int newTurn)
		{
			if (base.Duel.Player == 1)
			{
				newTurn--;
			}
			this.banSpSummonFromExTurn = Math.Max(this.banSpSummonFromExTurn, newTurn);
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x0005E798 File Offset: 0x0005C998
		public ClientCard GetProblematicEnemyMonster(int attack = 0, bool canBeTarget = false, bool ignoreCurrentDestroy = false)
		{
			List<ClientCard> floodagateList = (from c in base.Enemy.GetMonsters()
				where ((c != null) ? c.Data : null) != null && c.IsFloodgate() && c.IsFaceup() && (!canBeTarget || !c.IsShouldNotBeTarget()) && (!ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(c))
				select c).ToList<ClientCard>();
			if (floodagateList.Count<ClientCard>() > 0)
			{
				floodagateList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				floodagateList.Reverse();
				return floodagateList[0];
			}
			List<ClientCard> dangerList = base.Enemy.MonsterZone.Where((ClientCard c) => ((c != null) ? c.Data : null) != null && c.IsMonsterDangerous() && c.IsFaceup() && (!canBeTarget || !c.IsShouldNotBeTarget()) && (!ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(c))).ToList<ClientCard>();
			if (dangerList.Count<ClientCard>() > 0)
			{
				dangerList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				dangerList.Reverse();
				return dangerList[0];
			}
			List<ClientCard> invincibleList = base.Enemy.MonsterZone.Where((ClientCard c) => ((c != null) ? c.Data : null) != null && c.IsMonsterInvincible() && c.IsFaceup() && (!canBeTarget || !c.IsShouldNotBeTarget()) && (!ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(c))).ToList<ClientCard>();
			if (invincibleList.Count<ClientCard>() > 0)
			{
				invincibleList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				invincibleList.Reverse();
				return invincibleList[0];
			}
			if (attack >= 0)
			{
				if (attack == 0)
				{
					attack = base.Util.GetBestAttack(base.Bot);
				}
				List<ClientCard> betterList = (from card in base.Enemy.MonsterZone.GetMonsters()
					where card.GetDefensePower() >= attack && card.GetDefensePower() > 0 && card.IsAttack() && (!canBeTarget || !card.IsShouldNotBeTarget()) && (!ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(card))
					select card).ToList<ClientCard>();
				if (betterList.Count<ClientCard>() > 0)
				{
					betterList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					betterList.Reverse();
					return betterList[0];
				}
			}
			return null;
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x0005E928 File Offset: 0x0005CB28
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

		// Token: 0x060011F2 RID: 4594 RVA: 0x0005EBD8 File Offset: 0x0005CDD8
		public ClientCard GetBestEnemyMonster(bool onlyFaceup = false, bool canBeTarget = false, bool ignoreCurrentDestroy = false)
		{
			ClientCard card = this.GetProblematicEnemyMonster(0, canBeTarget, ignoreCurrentDestroy);
			if (card != null)
			{
				return card;
			}
			card = (from c in base.Enemy.MonsterZone
				where ((c != null) ? c.Data : null) != null && c.HasType(CardType.Monster) && c.IsFaceup() && (!canBeTarget || !c.IsShouldNotBeTarget()) && (!ignoreCurrentDestroy || this.currentDestroyCardList.Contains(c))
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
				return this.ShuffleCardList(monsters)[0];
			}
			return null;
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x0005ECA4 File Offset: 0x0005CEA4
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

		// Token: 0x060011F4 RID: 4596 RVA: 0x0005ED8C File Offset: 0x0005CF8C
		public ClientCard GetBestEnemyCard(bool onlyFaceup = false, bool canBeTarget = false, bool checkGrave = false)
		{
			ClientCard card = this.GetBestEnemyMonster(onlyFaceup, canBeTarget, false);
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

		// Token: 0x060011F5 RID: 4597 RVA: 0x0005EE4C File Offset: 0x0005D04C
		public List<ClientCard> GetDangerousCardinEnemyGrave(bool onlyMonster = false)
		{
			List<ClientCard> list = base.Enemy.Graveyard.GetMatchingCards((ClientCard card) => (!onlyMonster || card.IsMonster()) && (card.HasSetcode(283) || card.HasSetcode(219))).ToList<ClientCard>();
			List<int> dangerMonsterIdList = new List<int> { 99937011, 63542003, 9411399, 28954097, 30680659, 74586817 };
			list.AddRange(base.Enemy.Graveyard.GetMatchingCards((ClientCard card) => dangerMonsterIdList.Contains(card.Id)));
			return list;
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x0005EEF8 File Offset: 0x0005D0F8
		public List<ClientCard> GetNormalEnemyTargetList(bool canBeTarget = true, bool targetKnightmare = true, bool ignoreCurrentDestroy = false)
		{
			List<ClientCard> targetList = this.GetProblematicEnemyCardList(canBeTarget, false);
			List<ClientCard> enemyMonster = (from card in base.Enemy.GetMonsters()
				where card.IsFaceup() && !targetList.Contains(card) && !card.IsCode(10158145) && (!ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(card))
				select card).ToList<ClientCard>();
			enemyMonster.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			enemyMonster.Reverse();
			targetList.AddRange(enemyMonster);
			targetList.AddRange(this.ShuffleCardList((from card in base.Enemy.GetSpells()
				where !ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(card)
				select card).ToList<ClientCard>()));
			targetList.AddRange(this.ShuffleCardList((from card in base.Enemy.GetMonsters()
				where card.IsFacedown() && (!ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(card))
				select card).ToList<ClientCard>()));
			if (targetKnightmare)
			{
				List<ClientCard> enemyKnightmare = (from card in base.Enemy.GetMonsters()
					where card.IsFaceup() && !targetList.Contains(card) && card.IsCode(10158145) && (!ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(card))
					select card).ToList<ClientCard>();
				targetList.AddRange(enemyKnightmare);
			}
			return targetList;
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x0005F008 File Offset: 0x0005D208
		public List<ClientCard> GetMonsterListForTargetNegate(bool canBeMonsterTarget = false, bool canBeTrapTarget = false)
		{
			List<ClientCard> resultList = new List<ClientCard>();
			if (this.CheckWhetherNegated(false))
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

		// Token: 0x060011F8 RID: 4600 RVA: 0x0005F108 File Offset: 0x0005D308
		public List<int> GetNeedSearchRitualCardIdList()
		{
			List<int> result = new List<int>();
			bool canSearchAlbaZoa = !base.Bot.HasInHand(51522296) && this.CheckRemainInDeck(51522296) > 0;
			int totalLevelInGY = base.Bot.Graveyard.Where((ClientCard card) => card != null && card.HasType((CardType)8256)).Sum((ClientCard c) => new int?(c.Level).GetValueOrDefault());
			bool needSearchAlbaZoa = base.Bot.HasInHandOrInSpellZone(31002402) && base.Bot.HasInExtra(53971455) && canSearchAlbaZoa;
			if (base.Bot.HasInHandOrInGraveyard(60921537))
			{
				needSearchAlbaZoa |= totalLevelInGY >= 12 && canSearchAlbaZoa;
			}
			if (needSearchAlbaZoa)
			{
				result.Add(51522296);
			}
			if (base.Bot.HasInHand(51522296) && base.Bot.HasInExtra(53971455) && !base.Bot.HasInHandOrInSpellZone(31002402) && this.CheckRemainInDeck(31002402) > 0)
			{
				result.Add(31002402);
			}
			if (base.Bot.HasInHand(51522296) && !base.Bot.HasInHandOrInSpellZone(60921537) && this.CheckRemainInDeck(60921537) > 0 && totalLevelInGY >= 12)
			{
				result.Add(60921537);
			}
			return result;
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x0005F27C File Offset: 0x0005D47C
		public ClientCard GetExtraToDiscard(int baseAtk = 0, ClientCard avoidDestroyEnemyCard = null)
		{
			ClientCard selectResult = null;
			if (baseAtk <= 2500 && base.Bot.HasInExtra(80532587) && this.CheckCalledbytheGrave(80532587) == 0)
			{
				List<ClientCard> destroyList = this.GetNormalEnemyTargetList(true, false, false);
				if (destroyList.Count<ClientCard>() > 0 && (destroyList.Count<ClientCard>() != 1 || destroyList[0] != avoidDestroyEnemyCard))
				{
					selectResult = base.Bot.ExtraDeck.FirstOrDefault((ClientCard card) => card.IsCode(80532587));
					if (selectResult != null)
					{
						return selectResult;
					}
				}
			}
			if (baseAtk <= 1500 && base.Bot.HasInExtra(11765832) && this.CheckCalledbytheGrave(11765832) == 0 && !this.activatedCardIdList.Contains(11765832) && !this.enemyActivateLockBird)
			{
				selectResult = base.Bot.ExtraDeck.FirstOrDefault((ClientCard card) => card.IsCode(11765832));
				if (selectResult != null)
				{
					return selectResult;
				}
			}
			if (baseAtk <= 2500 && base.Bot.HasInExtra(41373230) && this.CheckCalledbytheGrave(41373230) == 0 && !this.discardExtraThisTurn.Contains(41373230) && !this.enemyActivateLockBird)
			{
				if ((!this.activatedCardIdList.Contains(60303688) && this.CheckRemainInDeck(60303688) > 0) | (base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && card.HasSetcode(325)) && !base.Bot.HasInHand(69680031) && this.CheckRemainInDeck(69680031) > 0))
				{
					selectResult = base.Bot.ExtraDeck.FirstOrDefault((ClientCard card) => card.IsCode(41373230));
					if (selectResult != null)
					{
						return selectResult;
					}
				}
			}
			if (baseAtk <= 2500 && base.Bot.HasInExtra(24915933) && (base.Bot.HasInExtra(53971455) | (this.CheckRemainInDeck(new int[] { 60303688, 69680031, 95679145 }) > 0)))
			{
				selectResult = base.Bot.ExtraDeck.FirstOrDefault((ClientCard card) => card.IsCode(24915933));
				if (selectResult != null)
				{
					return selectResult;
				}
			}
			if (baseAtk <= 600 && base.Bot.HasInExtra(79606837) && !this.enemyActivateLockBird && this.GetNeedSearchRitualCardIdList().Count<int>() > 0)
			{
				selectResult = base.Bot.ExtraDeck.FirstOrDefault((ClientCard card) => card.IsCode(79606837));
				if (selectResult != null)
				{
					return selectResult;
				}
			}
			if (baseAtk <= 2800 && base.Bot.HasInExtra(74586817))
			{
				selectResult = base.Bot.ExtraDeck.FirstOrDefault((ClientCard card) => card.IsCode(74586817));
				if (selectResult != null)
				{
					return selectResult;
				}
			}
			List<ClientCard> discardableList = base.Bot.ExtraDeck.Where((ClientCard card) => card != null && card.Attack >= baseAtk).ToList<ClientCard>();
			if (discardableList.Count<ClientCard>() > 0)
			{
				discardableList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				return discardableList[0];
			}
			return selectResult;
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x0005F624 File Offset: 0x0005D824
		public void CheckDeactiveFlag()
		{
			ClientCard lastChainCard = base.Util.GetLastChainCard();
			if (lastChainCard != null && base.Duel.LastChainPlayer == 1)
			{
				if (lastChainCard.IsCode(23434538))
				{
					this.enemyActivateMaxxC = false;
				}
				if (lastChainCard.IsCode(94145021))
				{
					this.enemyActivateLockBird = false;
				}
				if (lastChainCard.IsCode(91800273))
				{
					this.dimensionShifterCount = 0;
				}
				if (lastChainCard.Controller == 1 && lastChainCard.Location == CardLocation.MonsterZone)
				{
					this.currentNegateMonsterList.Add(lastChainCard);
				}
			}
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x0005F6A8 File Offset: 0x0005D8A8
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

		// Token: 0x060011FC RID: 4604 RVA: 0x0005F6C8 File Offset: 0x0005D8C8
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

		// Token: 0x060011FD RID: 4605 RVA: 0x0005F704 File Offset: 0x0005D904
		public int CheckRemainInDeck(params int[] ids)
		{
			int sumResult = 0;
			foreach (int id in ids)
			{
				sumResult += this.CheckRemainInDeck(id);
			}
			return sumResult;
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x0005F734 File Offset: 0x0005D934
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

		// Token: 0x060011FF RID: 4607 RVA: 0x0005F870 File Offset: 0x0005DA70
		public bool CheckWhetherNegated(bool toFieldCheck = false)
		{
			if ((base.Card.IsSpell() || base.Card.IsTrap()) && this.CheckSpellWillBeNegate(false, null))
			{
				return true;
			}
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return true;
			}
			if (base.Card.IsMonster() && (toFieldCheck || base.Card.Location == CardLocation.MonsterZone))
			{
				if ((toFieldCheck || base.Card.IsDefense()) && (base.Enemy.MonsterZone.Any((ClientCard card) => this.CheckNumber41(card)) || base.Bot.MonsterZone.Any((ClientCard card) => this.CheckNumber41(card))))
				{
					return true;
				}
				if (base.Enemy.HasInSpellZone(82732705, true, true))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x00037BCF File Offset: 0x00035DCF
		public bool CheckNumber41(ClientCard card)
		{
			return card != null && card.IsFaceup() && card.IsCode(90590303) && card.IsDefense() && !card.IsDisabled();
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x0005F938 File Offset: 0x0005DB38
		public bool CheckWhetherWillbeRemoved()
		{
			if (this.dimensionShifterCount > 0)
			{
				return true;
			}
			foreach (int cardid in new List<int> { 94853057, 61528025, 30241314, 81674782 })
			{
				foreach (ClientField cf in new List<ClientField> { base.Bot, base.Enemy })
				{
					if (cf.HasInMonstersZone(cardid, true, false, false) || cf.HasInSpellZone(cardid, true, false))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x0005FA2C File Offset: 0x0005DC2C
		public bool CheckAtAdvantage()
		{
			if (this.GetProblematicEnemyMonster(0, false, false) == null)
			{
				if (!base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && !card.IsCode(10158145)) || (base.Duel.Player == 0 && base.Duel.Turn == 1))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x0005FA94 File Offset: 0x0005DC94
		public bool CheckShouldNoMoreSpSummon()
		{
			if (this.CheckAtAdvantage() && this.enemyActivateMaxxC && base.Util.IsTurn1OrMain2())
			{
				bool flag = false | base.Bot.HasInHandOrInSpellZone(82956214) | base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && card.Level >= 7 && card.HasRace(CardRace.SpellCaster));
				bool flag2;
				if (base.Bot.HasInHand(69680031))
				{
					flag2 = base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && card.HasSetcode(325));
				}
				else
				{
					flag2 = false;
				}
				return flag || flag2;
			}
			return false;
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x0005FB50 File Offset: 0x0005DD50
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
			if (lastcard.IsCode(94145021))
			{
				bool needToSearch = false;
				foreach (int checkId in new List<int> { 60303688, 1984618 })
				{
					if (base.Bot.HasInHandOrInSpellZone(checkId) && !this.activatedCardIdList.Contains(checkId))
					{
						needToSearch = true;
					}
				}
				if (this.discardExtraThisTurn.Contains(41373230))
				{
					needToSearch = true;
				}
				if (!needToSearch)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x0005FC4C File Offset: 0x0005DE4C
		public bool CheckHasExtraOnField(ClientCard exceptCard = null)
		{
			List<ClientCard> monsters = base.Bot.GetMonsters();
			monsters.AddRange(base.Enemy.GetMonsters());
			return monsters.Any((ClientCard card) => card.HasType((CardType)75505728) && card != exceptCard);
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x0000763C File Offset: 0x0000583C
		public override bool OnSelectHand()
		{
			return true;
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x0005FC94 File Offset: 0x0005DE94
		public override int OnSelectOption(IList<int> options)
		{
			foreach (int checkOption in new List<int>
			{
				base.Util.GetStringId(62849088, 4),
				base.Util.GetStringId(62849088, 2)
			})
			{
				for (int i = 0; i < options.Count<int>(); i++)
				{
					if (options[i] == checkOption)
					{
						return i;
					}
				}
			}
			return base.OnSelectOption(options);
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x0005FD38 File Offset: 0x0005DF38
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
				if (base.Duel.Player == 1)
				{
					if (!cardData.HasType(CardType.Ritual) || cardData.Defense >= cardData.Attack || base.Util.IsOneEnemyBetterThanValue(cardData.Attack, true))
					{
						return CardPosition.FaceUpDefence;
					}
				}
				else if (cardData.HasType(CardType.Ritual))
				{
					return CardPosition.FaceUpAttack;
				}
				int cardAttack = cardData.Attack;
				if (cardId == 69680031 && !this.activatedCardIdList.Contains(cardId + 1) && base.Duel.Player == 0)
				{
					cardAttack += 500;
				}
				int bestBotAttack = Math.Max(base.Util.GetBestAttack(base.Bot), cardAttack);
				if (base.Util.IsAllEnemyBetterThanValue(bestBotAttack, true))
				{
					return CardPosition.FaceUpDefence;
				}
			}
			return base.OnSelectPosition(cardId, positions);
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x0005FE3C File Offset: 0x0005E03C
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
					List<ClientCard> nonSynchroMonsters = list.Where((ClientCard card) => !card.HasType(CardType.Ritual) && !banishList.Contains(card)).ToList<ClientCard>();
					nonSynchroMonsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					banishList.AddRange(nonSynchroMonsters);
					List<ClientCard> spells = base.Bot.GetSpells();
					banishList.AddRange(this.ShuffleCardList(spells));
					List<ClientCard> synchroMonsters = list.Where((ClientCard card) => card.HasType(CardType.Ritual) && !banishList.Contains(card)).ToList<ClientCard>();
					synchroMonsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					banishList.AddRange(synchroMonsters);
					return base.Util.CheckSelectCount(banishList, cards, min, max);
				}
			}
			if (this.maximusDiscardExtraIdList.Count<int>() > 0 && min == 1 && max == 1 && hint == 504)
			{
				List<ClientCard> discardList = new List<ClientCard>();
				using (List<int>.Enumerator enumerator = this.maximusDiscardExtraIdList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int checkId = enumerator.Current;
						ClientCard discardTarget = cards.FirstOrDefault((ClientCard card) => card.IsCode(checkId));
						if (discardTarget != null)
						{
							discardList.Add(discardTarget);
						}
					}
				}
				if (discardList.Count<ClientCard>() >= max)
				{
					if (discardList.Count<ClientCard>() > 0)
					{
						List<int> list2 = this.discardExtraThisTurn;
						ClientCard clientCard = discardList[0];
						list2.Add((clientCard != null) ? clientCard.Id : 0);
					}
					return base.Util.CheckSelectCount(discardList, cards, min, max);
				}
			}
			if (this.matrixActivating && hint == 504 && min == 1 && max == 1)
			{
				bool extraFlag = true;
				int enemyFlag = 0;
				foreach (ClientCard card6 in cards)
				{
					extraFlag &= card6.Location == CardLocation.Extra;
					if (enemyFlag == 0)
					{
						enemyFlag = card6.Controller + 1;
					}
					else if (enemyFlag < 3 && enemyFlag != card6.Controller + 1)
					{
						enemyFlag = 3;
					}
				}
				Logger.DebugWriteLine("===Matrix: extraFlag = " + extraFlag.ToString() + ", enemyFlag = " + enemyFlag.ToString());
				if (extraFlag && enemyFlag < 3)
				{
					List<ClientCard> discardList2 = new List<ClientCard>();
					if (enemyFlag == 1)
					{
						ClientCard elder = null;
						ClientCard ashDragon = null;
						ClientCard garura = null;
						ClientCard arcLight = null;
						ClientCard psy = null;
						ClientCard duskDragon = null;
						ClientCard lilith = null;
						foreach (ClientCard card2 in cards)
						{
							if (card2.Id == 80532587)
							{
								elder = card2;
							}
							if (card2.Id == 41373230)
							{
								ashDragon = card2;
							}
							if (card2.Id == 11765832)
							{
								garura = card2;
							}
							if (card2.Id == 79606837)
							{
								arcLight = card2;
							}
							if (card2.Id == 74586817)
							{
								psy = card2;
							}
							if (card2.Id == 24915933)
							{
								duskDragon = card2;
							}
							if (card2.Id == 53971455)
							{
								lilith = card2;
							}
						}
						List<ClientCard> destroyList = this.GetNormalEnemyTargetList(true, true, true);
						if (elder != null && destroyList.Count<ClientCard>() > 0)
						{
							discardList2.Add(elder);
						}
						if (ashDragon != null && !this.activatedCardIdList.Contains(41373230) && !this.discardEnemyExtraIdList.Contains(41373230) && ((!this.activatedCardIdList.Contains(60303688) && this.CheckRemainInDeck(60303688) > 0 && this.CheckCalledbytheGrave(60303688) == 0) | (this.CheckRemainInDeck(69680031) > 0 && !base.Bot.HasInHand(69680031) && !this.enemyActivateLockBird)))
						{
							discardList2.Add(ashDragon);
						}
						if (garura != null && !this.activatedCardIdList.Contains(11765832) && !this.enemyActivateLockBird)
						{
							discardList2.Add(garura);
						}
						if (arcLight != null && this.GetNeedSearchRitualCardIdList().Count<int>() > 0)
						{
							discardList2.Add(arcLight);
						}
						if (psy != null)
						{
							discardList2.Add(psy);
						}
						if (duskDragon != null)
						{
							discardList2.Add(duskDragon);
						}
						if (lilith != null && !this.activatedCardIdList.Contains(53971455) && !this.discardEnemyExtraIdList.Contains(53971455))
						{
							discardList2.Add(lilith);
						}
						if (discardList2.Count<ClientCard>() > 0)
						{
							List<int> list3 = this.discardExtraThisTurn;
							ClientCard clientCard2 = discardList2[0];
							list3.Add((clientCard2 != null) ? clientCard2.Id : 0);
						}
					}
					if (enemyFlag == 2)
					{
						this.checkedEnemyExtra = true;
						this.avoid2Monster = false;
						this.confirmLink2 = false;
						List<int> discardIfKnightmare = new List<int> { 96380700, 48068378, 14812471, 32995276, 30342076, 24842059, 3679218 };
						foreach (ClientCard card3 in cards)
						{
							NamedCard cardData = NamedCard.Get(card3.Id);
							if (cardData != null)
							{
								this.confirmLink2 |= cardData.HasType(CardType.Link) && cardData.Level <= 2;
								this.avoid2Monster |= (cardData.HasType(CardType.Link) && cardData.Level <= 2) || cardData.HasType((CardType)8396800);
								if (base.Enemy.HasInMonstersZone(10158145, false, false, false) && discardIfKnightmare.Contains(card3.Id))
								{
									discardList2.Add(card3);
								}
							}
						}
						discardList2 = this.ShuffleCardList(discardList2);
						foreach (ClientCard card4 in cards)
						{
							if (!discardList2.Contains(card4))
							{
								NamedCard cardData2 = NamedCard.Get(card4.Id);
								if (cardData2 != null && base.Enemy.HasInMonstersZone(10158145, false, false, false) && cardData2.HasType(CardType.Link) && cardData2.Level <= base.Enemy.GetMonsterCount())
								{
									discardList2.Add(card4);
								}
							}
						}
						foreach (ClientCard card5 in cards)
						{
							if (!discardList2.Contains(card5) && this.discardEnemyExtraIdList.Contains(card5.Id))
							{
								discardList2.Add(card5);
							}
						}
						List<ClientCard> singleCardList = new List<ClientCard>();
						List<ClientCard> multiCardList = new List<ClientCard>();
						using (IEnumerator<ClientCard> enumerator2 = cards.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								ClientCard card = enumerator2.Current;
								if (!discardList2.Contains(card))
								{
									if (cards.Any((ClientCard oc) => card != oc && card.IsCode(oc.Id)))
									{
										multiCardList.Add(card);
									}
									else
									{
										singleCardList.Add(card);
									}
								}
							}
						}
						discardList2.AddRange(singleCardList.OrderByDescending(delegate(ClientCard c)
						{
							NamedCard namedCard = NamedCard.Get(c.Id);
							if (namedCard == null)
							{
								return 0;
							}
							return namedCard.Attack;
						}));
						discardList2.AddRange(multiCardList.OrderByDescending(delegate(ClientCard c)
						{
							NamedCard namedCard2 = NamedCard.Get(c.Id);
							if (namedCard2 == null)
							{
								return 0;
							}
							return namedCard2.Attack;
						}));
					}
					if (discardList2.Count<ClientCard>() > 0)
					{
						return base.Util.CheckSelectCount(discardList2, cards, min, max);
					}
				}
			}
			return base.OnSelectCard(cards, min, max, hint, cancelable);
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x0006074C File Offset: 0x0005E94C
		public override void OnNewTurn()
		{
			if (base.Duel.Turn <= 1)
			{
				this.banSpSummonFromExTurn = 0;
				this.checkedEnemyExtra = false;
				this.avoid2Monster = true;
				this.dimensionShifterCount = 0;
				this.enemySpSummonFromExLastTurn = 0;
				this.enemySpSummonFromExThisTurn = 0;
			}
			this.enemyActivateMaxxC = false;
			this.enemyActivateLockBird = false;
			this.omegaActivateCount = 0;
			this.enemySpSummonFromExLastTurn = this.enemySpSummonFromExThisTurn;
			this.enemySpSummonFromExThisTurn = 0;
			this.currentNegatingIdList.Clear();
			if (this.dimensionShifterCount > 0)
			{
				this.dimensionShifterCount--;
			}
			this.infiniteImpermanenceList.Clear();
			this.summoned = false;
			this.activatedCardIdList.Clear();
			this.discardExtraThisTurn.Clear();
			this.activatedMatrixList.Clear();
			if (base.Duel.Player == 1 && this.banSpSummonFromExTurn > 0)
			{
				this.banSpSummonFromExTurn--;
			}
			base.OnNewTurn();
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x00060839 File Offset: 0x0005EA39
		public override void OnMove(ClientCard card, int previousControler, int previousLocation, int currentControler, int currentLocation)
		{
			if (previousControler == 1 && currentLocation == 4)
			{
				if (previousLocation == 1)
				{
					this.enemySpSummonFromDeck = true;
				}
				if (previousLocation == 64)
				{
					this.enemySpSummonFromExtra = true;
					this.enemySpSummonFromExThisTurn++;
				}
			}
			base.OnMove(card, previousControler, previousLocation, currentControler, currentLocation);
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x00060878 File Offset: 0x0005EA78
		public override BattlePhaseAction OnBattle(IList<ClientCard> attackers, IList<ClientCard> defenders)
		{
			if (attackers.Count<ClientCard>() == 1 && defenders.Count<ClientCard>() == 1 && defenders[0].IsCode(10158145) && !this.confirmLink2)
			{
				return new BattlePhaseAction(BattlePhaseAction.BattleAction.ToMainPhaseTwo);
			}
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

		// Token: 0x0600120D RID: 4621 RVA: 0x00060938 File Offset: 0x0005EB38
		public override BattlePhaseAction OnSelectAttackTarget(ClientCard attacker, IList<ClientCard> defenders)
		{
			foreach (ClientCard defender in defenders)
			{
				attacker.RealPower = attacker.Attack;
				defender.RealPower = defender.GetDefensePower();
				if (this.OnPreBattleBetween(attacker, defender))
				{
					if (attacker.RealPower > defender.RealPower)
					{
						return base.AI.Attack(attacker, defender);
					}
					if (attacker.RealPower == defender.RealPower && defender.IsAttack() && base.Bot.GetMonsterCount() >= base.Enemy.GetMonsterCount())
					{
						return base.AI.Attack(attacker, defender);
					}
				}
			}
			if (attacker.CanDirectAttack)
			{
				return base.AI.Attack(attacker, null);
			}
			return null;
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x00060A18 File Offset: 0x0005EC18
		public override bool OnPreBattleBetween(ClientCard attacker, ClientCard defender)
		{
			if (!defender.IsMonsterHasPreventActivationEffectInBattle() && !this.activatedCardIdList.Contains(69680032) && base.Bot.HasInMonstersZone(69680031, true, false, true) && attacker.HasSetcode(325))
			{
				attacker.RealPower += 500;
			}
			return base.OnPreBattleBetween(attacker, defender);
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x00060A7C File Offset: 0x0005EC7C
		public override void OnChaining(int player, ClientCard card)
		{
			if (card == null)
			{
				return;
			}
			if (player == 1 && card.IsCode(10045474) && !base.DefaultCheckWhetherCardIdIsNegated(10045474))
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
			base.OnChaining(player, card);
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x00060AE0 File Offset: 0x0005ECE0
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
				if (currentCard.IsCode(91800273))
				{
					this.dimensionShifterCount = 2;
				}
				if (currentCard.IsCode(10045474))
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

		// Token: 0x06001211 RID: 4625 RVA: 0x00060B91 File Offset: 0x0005ED91
		public override void OnChainEnd()
		{
			this.currentNegateMonsterList.Clear();
			this.currentDestroyCardList.Clear();
			this.maximusDiscardExtraIdList.Clear();
			this.matrixActivating = false;
			base.OnChainEnd();
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x00060BC4 File Offset: 0x0005EDC4
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

		// Token: 0x06001213 RID: 4627 RVA: 0x00060DB0 File Offset: 0x0005EFB0
		public bool DogmatikaAlbaZoaActivate()
		{
			if (this.CheckWhetherNegated(false))
			{
				return false;
			}
			if (base.Enemy.GetMonsters().Any((ClientCard card) => card != null && card.HasType((CardType)75505728)) && base.Duel.Phase == DuelPhase.Main1 && base.Enemy.ExtraDeck.Count<ClientCard>() > 1)
			{
				return false;
			}
			this.activatedCardIdList.Add(base.Card.Id);
			return true;
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x00060E33 File Offset: 0x0005F033
		public bool ThesIrisSwordsoulActivate()
		{
			if (this.CheckWhetherNegated(false))
			{
				return false;
			}
			if (base.Card.Location == CardLocation.Hand)
			{
				return !this.CheckShouldNoMoreSpSummon();
			}
			return this.enemySpSummonFromDeck || this.enemySpSummonFromExtra;
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x00060E6D File Offset: 0x0005F06D
		public bool ClearIrisFlag()
		{
			this.enemySpSummonFromDeck = false;
			this.enemySpSummonFromExtra = false;
			return false;
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x00060E80 File Offset: 0x0005F080
		public bool DogmatikaFleurdelisActivate()
		{
			if (this.CheckWhetherNegated(false))
			{
				return false;
			}
			if (base.Card.Location == CardLocation.Hand)
			{
				bool flag;
				if (base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && card.HasSetcode(325)))
				{
					flag = base.Enemy.GetMonsters().Any((ClientCard card) => card.IsFaceup());
				}
				else
				{
					flag = false;
				}
				if (flag)
				{
					List<ClientCard> shouldNegateList = this.GetMonsterListForTargetNegate(true, false);
					if (shouldNegateList.Count<ClientCard>() > 0)
					{
						ClientCard target = shouldNegateList[0];
						this.currentNegateMonsterList.Add(target);
						base.AI.SelectYesNo(true);
						base.AI.SelectCard(target);
						this.activatedCardIdList.Add(69680031);
						return true;
					}
					if (base.Bot.HasInHand(62849088))
					{
						ClientCard target2 = this.GetProblematicEnemyMonster(0, true, false);
						if (target2 != null)
						{
							base.AI.SelectYesNo(true);
							base.AI.SelectCard(target2);
						}
						else
						{
							List<ClientCard> enemyTargetList = this.ShuffleCardList((from card in base.Enemy.GetMonsters()
								where card.IsFaceup() && !card.IsDisabled()
								select card).ToList<ClientCard>());
							if (enemyTargetList.Count<ClientCard>() > 0)
							{
								base.AI.SelectYesNo(true);
								base.AI.SelectCard(enemyTargetList);
							}
							else
							{
								base.AI.SelectYesNo(false);
							}
						}
						this.activatedCardIdList.Add(69680031);
						return true;
					}
				}
				if (base.Duel.Player == 0 && base.Enemy.GetMonsterCount() == 0)
				{
					int totalAttack = base.Util.GetTotalAttackingMonsterAttack(0);
					if (totalAttack < base.Enemy.LifePoints)
					{
						totalAttack += (from card in base.Bot.GetMonsters()
							where card.HasSetcode(325)
							select card).Count<ClientCard>() * 500 + 3000;
						if (totalAttack >= base.Enemy.LifePoints)
						{
							this.activatedCardIdList.Add(69680031);
							base.AI.SelectYesNo(false);
							return true;
						}
					}
				}
				if (base.Duel.Player == 1 && base.Bot.GetMonsterCount() == 0 && base.Util.GetTotalAttackingMonsterAttack(1) >= base.Bot.LifePoints && base.Duel.Phase == DuelPhase.Main1 && (base.CurrentTiming & 4) != 0 && base.Duel.Turn > 1)
				{
					this.activatedCardIdList.Add(69680031);
					List<ClientCard> enemyTargetList2 = this.ShuffleCardList((from card in base.Enemy.GetMonsters()
						where card.IsFaceup() && !card.IsDisabled()
						select card).ToList<ClientCard>());
					if (enemyTargetList2.Count<ClientCard>() > 0)
					{
						base.AI.SelectYesNo(true);
						base.AI.SelectCard(enemyTargetList2);
					}
					else
					{
						base.AI.SelectYesNo(false);
					}
					return true;
				}
			}
			if (base.Card.Location == CardLocation.MonsterZone)
			{
				this.activatedCardIdList.Add(base.Card.Id + 1);
				return true;
			}
			return false;
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x000611DC File Offset: 0x0005F3DC
		public bool DogmatikaFleurdelisDelayActivate()
		{
			if (this.CheckWhetherNegated(false))
			{
				return false;
			}
			if (base.Card.Location == CardLocation.Hand)
			{
				bool checkFlag = false;
				bool notQuickTiming = base.Duel.LastChainPlayer == -1 && base.CurrentTiming <= 0;
				if (base.Duel.Player == 0 && base.Duel.Phase == DuelPhase.Main1 && notQuickTiming && base.Duel.Turn > 1)
				{
					Logger.DebugWriteLine("=== timing: " + base.CurrentTiming.ToString());
					int attack = base.Util.GetBestAttack(base.Bot);
					IEnumerable<ClientCard> enumerable = (from card in base.Enemy.MonsterZone.GetMonsters()
						where card.GetDefensePower() >= attack
						select card).ToList<ClientCard>();
					List<ClientCard> newBetterList = (from card in base.Enemy.MonsterZone.GetMonsters()
						where card.GetDefensePower() >= 3000
						select card).ToList<ClientCard>();
					if (enumerable.Count<ClientCard>() > newBetterList.Count<ClientCard>())
					{
						checkFlag = true;
					}
				}
				if (!base.Bot.HasInHandOrInSpellZone(16240772) || base.Duel.Player != 0 || !notQuickTiming)
				{
					if (!base.Bot.GetSpells().Any((ClientCard card) => card.IsCode(16240772) && card.IsFacedown()))
					{
						goto IL_01A7;
					}
				}
				if (!base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && card.Level >= 7 && card.HasRace(CardRace.SpellCaster)))
				{
					checkFlag = true;
				}
				IL_01A7:
				if (checkFlag)
				{
					List<ClientCard> enemyTargetList = this.ShuffleCardList((from card in base.Enemy.GetMonsters()
						where card.IsFaceup() && !card.IsDisabled()
						select card).ToList<ClientCard>());
					if (enemyTargetList.Count<ClientCard>() > 0)
					{
						base.AI.SelectYesNo(true);
						base.AI.SelectCard(enemyTargetList);
					}
					else
					{
						base.AI.SelectYesNo(false);
					}
					this.activatedCardIdList.Add(69680031);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x00061414 File Offset: 0x0005F614
		public bool DogmatikaMaximusActivate()
		{
			if (this.CheckWhetherNegated(false))
			{
				return false;
			}
			if (base.Card.Location == CardLocation.Hand)
			{
				if (this.CheckShouldNoMoreSpSummon())
				{
					return false;
				}
				using (List<int>.Enumerator enumerator = new List<int> { 80532587, 11765832, 53971455 }.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int dumpId = enumerator.Current;
						IEnumerable<ClientCard> checkList = from card in base.Bot.GetGraveyardMonsters()
							where card.IsCode(dumpId)
							select card;
						if (checkList.Count<ClientCard>() > 1)
						{
							IEnumerable<ClientCard> notSummonList = checkList.Where((ClientCard card) => card.ProcCompleted == 0);
							if (notSummonList.Count<ClientCard>() > 0)
							{
								base.AI.SelectCard(notSummonList.ToList<ClientCard>());
								return true;
							}
							base.AI.SelectCard(checkList.ToList<ClientCard>());
							return true;
						}
					}
				}
				List<ClientCard> notSummonedList = this.ShuffleCardList(base.Bot.Graveyard.Where((ClientCard card) => card != null && card.IsMonster() && card.ProcCompleted == 0 && card.HasType((CardType)75505728)).ToList<ClientCard>());
				if (notSummonedList.Count<ClientCard>() > 0)
				{
					base.AI.SelectCard(notSummonedList);
					return true;
				}
				List<ClientCard> graveTargetList = (from card in base.Bot.Graveyard
					where card != null && card.IsMonster() && card.HasType((CardType)75505728)
					orderby card.Attack
					select card).ToList<ClientCard>();
				if (graveTargetList.Count<ClientCard>() > 0)
				{
					base.AI.SelectCard(graveTargetList);
					return true;
				}
			}
			IL_01D6:
			if (base.Card.Location != CardLocation.MonsterZone)
			{
				return false;
			}
			if (this.CheckWhetherWillbeRemoved())
			{
				return false;
			}
			List<int> decidedToDiscard = new List<int>();
			foreach (int checkId in new List<int> { 80532587, 79606837, 11765832, 41373230, 24915933, 74586817, 53971455 })
			{
				if (base.Bot.HasInExtra(checkId) && !this.activatedCardIdList.Contains(checkId) && (checkId != 80532587 || this.GetNormalEnemyTargetList(true, false, false).Count<ClientCard>() != 0) && (!this.enemyActivateLockBird || (checkId != 79606837 && checkId != 11765832)) && (checkId != 79606837 || (!base.Bot.HasInMonstersZone(51522296, false, false, false) && this.GetNeedSearchRitualCardIdList().Count<int>() != 0)) && (checkId != 11765832 || !this.activatedCardIdList.Contains(11765832)) && (!this.discardExtraThisTurn.Contains(checkId) || (checkId != 41373230 && checkId != 53971455)))
				{
					decidedToDiscard.Add(checkId);
				}
			}
			this.maximusDiscardExtraIdList.AddRange(decidedToDiscard);
			this.activatedCardIdList.Add(base.Card.Id);
			this.UpdateBanSpSummonFromExTurn(1);
			return true;
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x000617B4 File Offset: 0x0005F9B4
		public bool DiabellstarTheBlackWitchSpSummon()
		{
			bool hasEmptyMonsterZone = false;
			for (int i = 0; i < 5; i++)
			{
				if (base.Bot.MonsterZone[i] == null)
				{
					hasEmptyMonsterZone = true;
					break;
				}
			}
			if (hasEmptyMonsterZone)
			{
				if (base.Bot.HasInHandOrInSpellZone(80845034))
				{
					base.AI.SelectCard(80845034);
					return true;
				}
				if (this.activatedMatrixList.Count<ClientCard>() > 0)
				{
					base.AI.SelectCard(this.activatedMatrixList);
					return true;
				}
				if ((from card in base.Bot.GetSpells()
					where card.IsCode(35569555)
					select card).Count<ClientCard>() > 1)
				{
					base.AI.SelectCard(35569555);
					return true;
				}
			}
			if (base.Bot.HasInHand(69680031))
			{
				if ((from card in base.Bot.GetMonsters()
					where card.IsFaceup() && card.HasSetcode(325)
					select card).Count<ClientCard>() <= 1)
				{
					goto IL_01CA;
				}
			}
			using (List<int>.Enumerator enumerator = new List<int> { 60303688, 95679145 }.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int checkId = enumerator.Current;
					ClientCard costMonster = null;
					if (this.activatedCardIdList.Contains(checkId))
					{
						costMonster = base.Bot.GetMonsters().FirstOrDefault((ClientCard card) => card.IsCode(checkId));
					}
					if (costMonster == null)
					{
						costMonster = base.Bot.GetMonsters().FirstOrDefault((ClientCard card) => card.IsCode(checkId) && card.IsDisabled());
					}
					if (costMonster != null)
					{
						base.AI.SelectCard(costMonster);
						return true;
					}
				}
			}
			IL_01CA:
			if (hasEmptyMonsterZone)
			{
				foreach (int checkId2 in new List<int> { 10158145, 62849088 })
				{
					if (base.Bot.HasInHand(checkId2))
					{
						base.AI.SelectCard(checkId2);
						return true;
					}
				}
			}
			List<ClientCard> faceDownMonsters = (from card in base.Bot.GetMonsters()
				where card.IsFacedown()
				select card).OrderBy(delegate(ClientCard card)
			{
				NamedCard cardData = NamedCard.Get(card.Id);
				if (cardData != null)
				{
					return cardData.Attack;
				}
				return card.Attack;
			}).ToList<ClientCard>();
			if (faceDownMonsters.Count<ClientCard>() > 0)
			{
				base.AI.SelectCard(faceDownMonsters);
				return true;
			}
			if (hasEmptyMonsterZone)
			{
				if (base.Bot.HasInHand(69680031))
				{
					if ((from card in base.Bot.GetMonsters()
						where card.IsFaceup() && card.HasSetcode(325)
						select card).Count<ClientCard>() == 0)
					{
						base.AI.SelectCard(69680031);
						return true;
					}
				}
				if (this.CheckRemainInDeck(60921537) > 0)
				{
					ClientCard albaZoaInHand = base.Bot.Hand.FirstOrDefault((ClientCard card) => card.IsCode(51522296));
					if (albaZoaInHand != null)
					{
						base.AI.SelectCard(albaZoaInHand);
						return true;
					}
				}
				using (List<int>.Enumerator enumerator = new List<int> { 82956214, 10045474, 24224830, 14558127, 23434538 }.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int dumpId = enumerator.Current;
						if (base.Bot.Hand.Where((ClientCard card) => card.IsCode(dumpId)).Count<ClientCard>() + base.Bot.SpellZone.Where((ClientCard card) => card != null && card.IsCode(dumpId)).Count<ClientCard>() > 1)
						{
							base.AI.SelectCard(dumpId);
							return true;
						}
					}
				}
				foreach (ClientCard checkCard in (from card in base.Bot.GetMonsters()
					where card.HasType((CardType)67117120)
					orderby card.Attack
					select card).ToList<ClientCard>())
				{
					if (!base.Bot.HasInHand(69680031) || this.CheckHasExtraOnField(checkCard))
					{
						base.AI.SelectCard(checkCard);
						return true;
					}
				}
			}
			if (base.Bot.GetMonsterCount() == 0 || this.CheckRemainInDeck(16240772) > 0)
			{
				foreach (int spellId in new List<int> { 65681983, 10045474, 24224830, 82956214, 60921537, 31002402 })
				{
					if (base.Bot.HasInHandOrInSpellZone(spellId))
					{
						base.AI.SelectCard(spellId);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x00061D98 File Offset: 0x0005FF98
		public bool DiabellstarTheBlackWitchActivate()
		{
			if (this.CheckWhetherNegated(false))
			{
				return false;
			}
			if (base.Card.Location == CardLocation.MonsterZone)
			{
				base.AI.SelectCard(new int[] { 16240772, 80845034 });
				this.SelectSTPlace(null, false, null);
				this.activatedCardIdList.Add(base.Card.Id);
				return true;
			}
			return false;
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x00061E01 File Offset: 0x00060001
		public bool DogmatikaEcclesiaSummon()
		{
			if (this.enemyActivateLockBird)
			{
				return false;
			}
			if (this.CheckWhetherNegated(false))
			{
				return false;
			}
			if (this.activatedCardIdList.Contains(base.Card.Id))
			{
				return false;
			}
			this.summoned = true;
			return true;
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x00061E3C File Offset: 0x0006003C
		public bool DogmatikaEcclesiaActivate()
		{
			if (this.CheckWhetherNegated(false))
			{
				return false;
			}
			if (base.Card.Location != CardLocation.Hand)
			{
				if (base.Card.Location == CardLocation.MonsterZone)
				{
					if (((base.Duel.Player == 0 && base.Duel.Phase == DuelPhase.End) || (base.Duel.Player == 1 && base.Duel.Phase < DuelPhase.End)) && !base.Bot.HasInHand(69680031) && this.CheckRemainInDeck(69680031) > 0)
					{
						base.AI.SelectCard(69680031);
						this.activatedCardIdList.Add(base.Card.Id);
						this.UpdateBanSpSummonFromExTurn(1);
						return true;
					}
					if (this.CheckAtAdvantage() && this.enemyActivateMaxxC)
					{
						List<int> checkIdListFirstPart = new List<int> { 82956214, 69680031 };
						if (this.DogmatikaMatrixCanActivate())
						{
							checkIdListFirstPart.Add(35569555);
						}
						checkIdListFirstPart.AddRange(new List<int> { 95679145, 51522296, 60921537, 31002402 });
						checkIdListFirstPart.Add(35569555);
						foreach (int checkId in checkIdListFirstPart)
						{
							if (!base.Bot.HasInHandOrInSpellZone(checkId) && this.CheckRemainInDeck(checkId) > 0)
							{
								base.AI.SelectCard(checkId);
								this.activatedCardIdList.Add(base.Card.Id);
								this.UpdateBanSpSummonFromExTurn(1);
								return true;
							}
						}
					}
					bool canSearchMatrix = this.DogmatikaMatrixCanActivate() && !this.activatedCardIdList.Contains(35569555) && this.CheckRemainInDeck(35569555) > 0 && !base.Bot.HasInHand(35569555);
					if (canSearchMatrix && base.Enemy.GetMonsterCount() > 0)
					{
						base.AI.SelectCard(35569555);
						this.activatedCardIdList.Add(base.Card.Id);
						this.UpdateBanSpSummonFromExTurn(1);
						return true;
					}
					IEnumerable<int> needSearchRitualCardIdList = this.GetNeedSearchRitualCardIdList();
					bool flag;
					if (this.CheckRemainInDeck(95679145) > 0 && !this.activatedCardIdList.Contains(95679145))
					{
						flag = base.Bot.Graveyard.Where((ClientCard card) => card.HasType((CardType)75505728)).Count<ClientCard>() > 0;
					}
					else
					{
						flag = false;
					}
					bool canSearchMaximus = flag;
					if (needSearchRitualCardIdList.Count<int>() > 0)
					{
						if (canSearchMatrix)
						{
							base.AI.SelectCard(35569555);
							this.activatedCardIdList.Add(base.Card.Id);
							this.UpdateBanSpSummonFromExTurn(1);
							return true;
						}
						if (canSearchMaximus && base.Bot.HasInExtra(79606837))
						{
							base.AI.SelectCard(95679145);
							this.activatedCardIdList.Add(base.Card.Id);
							this.UpdateBanSpSummonFromExTurn(1);
							return true;
						}
					}
					if (canSearchMaximus)
					{
						base.AI.SelectCard(95679145);
						this.activatedCardIdList.Add(base.Card.Id);
						this.UpdateBanSpSummonFromExTurn(1);
						return true;
					}
					List<int> checkIdListSecondPart = new List<int> { 82956214, 69680031 };
					if (this.DogmatikaMatrixCanActivate())
					{
						checkIdListSecondPart.Add(35569555);
					}
					checkIdListSecondPart.AddRange(new List<int> { 95679145, 51522296, 60921537, 31002402 });
					checkIdListSecondPart.Add(35569555);
					foreach (int checkId2 in checkIdListSecondPart)
					{
						if (!base.Bot.HasInHandOrInSpellZone(checkId2) && this.CheckRemainInDeck(checkId2) > 0)
						{
							base.AI.SelectCard(checkId2);
							this.activatedCardIdList.Add(base.Card.Id);
							this.UpdateBanSpSummonFromExTurn(1);
							return true;
						}
					}
					return false;
				}
				return false;
			}
			if (this.activatedCardIdList.Contains(base.Card.Id))
			{
				return false;
			}
			if (this.CheckShouldNoMoreSpSummon())
			{
				if (base.Bot.HasInHand(69680031))
				{
					if (!base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && card.HasSetcode(325)))
					{
						goto IL_0083;
					}
				}
				return false;
			}
			IL_0083:
			if (this.enemyActivateLockBird)
			{
				if (base.Bot.HasInHand(69680031))
				{
					if (!base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && card.HasSetcode(325)))
					{
						return true;
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x00062354 File Offset: 0x00060554
		public bool AshBlossomActivate()
		{
			if (this.CheckWhetherNegated(false) || !this.CheckLastChainShouldNegated())
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

		// Token: 0x0600121E RID: 4638 RVA: 0x000623B4 File Offset: 0x000605B4
		public bool MaxxCActivate()
		{
			return !this.CheckWhetherNegated(false) && base.Duel.LastChainPlayer != 0 && (!base.Enemy.HasInMonstersZone(10158145, true, false, true) || this.confirmLink2) && base.DefaultMaxxC();
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x000623F4 File Offset: 0x000605F4
		public bool KnightmareCorruptorIbleeSummon()
		{
			if (this.banSpSummonFromExTurn > 0)
			{
				return false;
			}
			if (this.CheckWhetherWillbeRemoved())
			{
				return false;
			}
			if (this.activatedCardIdList.Contains(10158145))
			{
				return false;
			}
			if (base.Bot.HasInExtra(60303245) || base.Bot.HasInExtra(24842059))
			{
				this.summoned = true;
				return true;
			}
			base.Bot.HasInExtra(29301450);
			return false;
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x0006246C File Offset: 0x0006066C
		public bool KnightmareCorruptorIbleeActivate()
		{
			if (base.Util.IsTurn1OrMain2())
			{
				return true;
			}
			if (base.Duel.Turn > 1)
			{
				if (base.Bot.HasInHand(35569555) && this.DogmatikaMatrixCanActivate())
				{
					return true;
				}
				if (base.Enemy.GetMonsterCount() > 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x000624C4 File Offset: 0x000606C4
		public bool NadirServantActivate()
		{
			if (this.CheckWhetherNegated(false) || this.CheckWhetherWillbeRemoved())
			{
				return false;
			}
			ClientCard discardExtra = null;
			int searchId = 0;
			if (!this.activatedCardIdList.Contains(60303688) && this.CheckCalledbytheGrave(60303688) == 0 && !base.Bot.HasInHand(60303688) && (this.CheckHasExtraOnField(null) || !this.summoned) && (base.Bot.HasInGraveyard(60303688) || this.CheckRemainInDeck(60303688) > 0))
			{
				searchId = 60303688;
				discardExtra = this.GetExtraToDiscard(1500, null);
			}
			if ((searchId == 0 || discardExtra == null) && ((!this.activatedCardIdList.Contains(95679145) && this.CheckCalledbytheGrave(95679145) == 0 && base.Bot.HasInGraveyard(95679145)) || this.CheckRemainInDeck(95679145) > 0))
			{
				searchId = 95679145;
				discardExtra = this.GetExtraToDiscard(1500, null);
			}
			if (searchId == 0 || discardExtra == null)
			{
				if (!base.Bot.HasInHand(69680031))
				{
					if (base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && card.HasSetcode(325)) && base.Bot.HasInGraveyard(69680031))
					{
						goto IL_016C;
					}
				}
				if (this.CheckRemainInDeck(69680031) <= 0)
				{
					goto IL_0184;
				}
				IL_016C:
				searchId = 69680031;
				discardExtra = this.GetExtraToDiscard(2500, null);
			}
			IL_0184:
			if ((searchId == 0 || discardExtra == null) && (base.Bot.HasInGraveyard(60303688) || this.CheckRemainInDeck(60303688) > 0))
			{
				searchId = 60303688;
				discardExtra = this.GetExtraToDiscard(1500, null);
			}
			if (discardExtra != null && searchId > 0)
			{
				this.discardExtraThisTurn.Add((discardExtra != null) ? discardExtra.Id : 0);
				base.AI.SelectCard(discardExtra);
				ClientCard targetInGY = base.Bot.Graveyard.FirstOrDefault((ClientCard card) => card != null && card.IsCode(searchId));
				if (targetInGY != null)
				{
					base.AI.SelectNextCard(targetInGY);
				}
				else
				{
					base.AI.SelectNextCard(searchId);
				}
				this.activatedCardIdList.Add(base.Card.Id);
				this.UpdateBanSpSummonFromExTurn(1);
				this.SelectSTPlace(null, true, null);
				return true;
			}
			return false;
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x00062738 File Offset: 0x00060938
		public bool DogmatikaLamityActivate()
		{
			if (this.CheckWhetherNegated(false))
			{
				return false;
			}
			if (base.Bot.HasInExtra(53971455))
			{
				base.AI.SelectYesNo(true);
				base.AI.SelectCard(51522296);
				base.AI.SelectNextCard(53971455);
				this.discardExtraThisTurn.Add(53971455);
				this.activatedCardIdList.Add(base.Card.Id);
				this.UpdateBanSpSummonFromExTurn(1);
				this.SelectSTPlace(null, true, null);
				return true;
			}
			return false;
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x000627C8 File Offset: 0x000609C8
		public bool DogmatikaLamityDelayActivate()
		{
			if (this.CheckWhetherNegated(false) || base.Bot.HasInExtra(53971455))
			{
				return false;
			}
			if (base.Bot.HasInMonstersZone(51522296, false, false, true))
			{
				return false;
			}
			List<ClientCard> materialList = new List<ClientCard>();
			int totalLevel = 0;
			foreach (ClientCard faceDownMonster in (from card in base.Bot.GetMonsters()
				where !card.HasType(CardType.Xyz) && card.IsFacedown()
				orderby card.Level descending
				select card).ToList<ClientCard>())
			{
				materialList.Add(faceDownMonster);
				totalLevel += faceDownMonster.Level;
				if (totalLevel >= 12)
				{
					break;
				}
			}
			ClientCard handSummonTarget = base.Bot.Hand.FirstOrDefault((ClientCard card) => card.IsCode(51522296));
			if (handSummonTarget == null)
			{
				return false;
			}
			int extraUseCount = 0;
			List<ClientCard> list = (from card in base.Bot.GetMonsters()
				where !card.HasType((CardType)75497472) && card.IsFaceup()
				orderby card.Level descending
				select card).ToList<ClientCard>();
			list.AddRange((from card in base.Bot.Hand
				where card.IsMonster() && card != handSummonTarget
				orderby card.Level descending
				select card).ToList<ClientCard>());
			foreach (ClientCard faceUpMonster in list)
			{
				if (totalLevel >= 12)
				{
					break;
				}
				if (extraUseCount >= 1)
				{
					break;
				}
				materialList.Add(faceUpMonster);
				totalLevel += faceUpMonster.Level;
				extraUseCount++;
			}
			if (totalLevel >= 12)
			{
				base.AI.SelectYesNo(false);
				base.AI.SelectCard(51522296);
				base.AI.SelectNextCard(materialList);
				this.activatedCardIdList.Add(base.Card.Id);
				this.UpdateBanSpSummonFromExTurn(1);
				this.SelectSTPlace(null, true, null);
				return true;
			}
			return false;
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x00062A58 File Offset: 0x00060C58
		public bool DogmatikaMacabreActivate()
		{
			if (base.Bot.HasInMonstersZone(51522296, false, false, false))
			{
				return false;
			}
			if (this.CheckWhetherNegated(false))
			{
				return false;
			}
			List<ClientCard> list = (from card in base.Bot.Graveyard
				where card != null && card.HasType((CardType)8256)
				orderby card.Level descending
				select card).ToList<ClientCard>();
			List<ClientCard> selectMaterialList = new List<ClientCard>();
			int totalLevel = 0;
			List<int> checkDiscardThisTurnIdList = new List<int> { 53971455, 41373230 };
			foreach (ClientCard material in list)
			{
				if (!material.IsCode(74586817))
				{
					if (this.CheckAtAdvantage())
					{
						foreach (int checkId in checkDiscardThisTurnIdList)
						{
							if (material.IsCode(checkId))
							{
								this.discardExtraThisTurn.Contains(checkId);
							}
						}
					}
					totalLevel += material.Level;
					selectMaterialList.Add(material);
					if (totalLevel >= 12)
					{
						break;
					}
				}
			}
			if (totalLevel >= 12)
			{
				ClientCard graveAlbaZoa = base.Bot.Graveyard.FirstOrDefault((ClientCard card) => card.IsCode(51522296));
				if (graveAlbaZoa != null)
				{
					base.AI.SelectCard(graveAlbaZoa);
				}
				else
				{
					base.AI.SelectCard(51522296);
				}
				base.AI.SelectMaterials(selectMaterialList, 500);
				this.SelectSTPlace(null, true, null);
				return true;
			}
			return false;
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x00062C38 File Offset: 0x00060E38
		public bool SinfulSpoilsOfDoom_RcielaActivate()
		{
			DogmatikaExecutor.<>c__DisplayClass88_0 CS$<>8__locals1 = new DogmatikaExecutor.<>c__DisplayClass88_0();
			CS$<>8__locals1.<>4__this = this;
			ClientCard selfTarget = null;
			bool activateFlag = false;
			List<ClientCard> selfCasterList = (from card in base.Bot.GetMonsters()
				where card.IsFaceup() && card.Level >= 7 && card.HasRace(CardRace.SpellCaster)
				orderby card.Attack descending, card.Level descending
				select card).ToList<ClientCard>();
			bool onlyAlbaZoa = selfCasterList.Count<ClientCard>() == 1 && selfCasterList[0].IsCode(51522296);
			ClientCard lastChainCard = base.Util.GetLastChainCard();
			if (lastChainCard != null && lastChainCard.Controller == 1 && lastChainCard.IsMonster())
			{
				bool negateFlag = lastChainCard.IsCode(new int[] { 97268402, 52038441 });
				if (base.Duel.Turn > 1 || !negateFlag)
				{
					foreach (ClientCard chainTarget in base.Duel.LastChainTargets)
					{
						if (selfCasterList.Contains(chainTarget) && (!negateFlag || !chainTarget.IsCode(72270339)))
						{
							selfTarget = chainTarget;
							activateFlag = true;
							break;
						}
					}
				}
			}
			if (selfTarget == null && !onlyAlbaZoa)
			{
				selfTarget = selfCasterList.FirstOrDefault((ClientCard card) => !card.IsCode(51522296));
			}
			if (base.DefaultOnBecomeTarget() && !onlyAlbaZoa)
			{
				activateFlag = true;
			}
			if (selfTarget != null)
			{
				CS$<>8__locals1.targetAttack = selfTarget.Attack;
				if (this.GetProblematicEnemyMonster(-1, true, true) != null)
				{
					activateFlag = true;
				}
				if (!onlyAlbaZoa)
				{
					List<ClientCard> toDestroyMonsterList = (from card in base.Enemy.GetMonsters()
						where card.IsFaceup() && card.Attack > 0 && card.Attack <= CS$<>8__locals1.targetAttack && !CS$<>8__locals1.<>4__this.currentDestroyCardList.Contains(card) && (CS$<>8__locals1.<>4__this.Duel.Player == 1 || card != CS$<>8__locals1.<>4__this.Enemy.BattlingMonster)
						select card).ToList<ClientCard>();
					if (toDestroyMonsterList.Count<ClientCard>() > 1)
					{
						activateFlag = true;
						this.currentDestroyCardList.AddRange(toDestroyMonsterList);
					}
				}
				DogmatikaExecutor.<>c__DisplayClass88_0 CS$<>8__locals2 = CS$<>8__locals1;
				ClientCard worstBotMonster = base.Util.GetWorstBotMonster(false);
				CS$<>8__locals2.botWorstPower = ((worstBotMonster != null) ? worstBotMonster.GetDefensePower() : 0);
				bool flag = base.Duel.Player == 1 && base.Enemy.GetMonsters().Any((ClientCard card) => card.Attack >= CS$<>8__locals1.botWorstPower && card.IsMonsterHasPreventActivationEffectInBattle()) && base.Duel.Phase > DuelPhase.Main1 && base.Duel.Phase < DuelPhase.Main2;
				if (onlyAlbaZoa)
				{
					ClientCard battlingMonster = base.Bot.BattlingMonster;
					if (battlingMonster == null || !battlingMonster.IsCode(51522296))
					{
						goto IL_02E1;
					}
				}
				ClientCard battlingMonster2 = base.Bot.BattlingMonster;
				int num = ((battlingMonster2 != null) ? battlingMonster2.GetDefensePower() : 0);
				ClientCard battlingMonster3 = base.Enemy.BattlingMonster;
				bool flag2;
				if (num <= ((battlingMonster3 != null) ? battlingMonster3.GetDefensePower() : 0) && base.Duel.LastChainPlayer != 0 && (base.CurrentTiming & 8192) != 0)
				{
					flag2 = base.CurrentTiming > 0;
					goto IL_02E2;
				}
				IL_02E1:
				flag2 = false;
				IL_02E2:
				if (flag || flag2)
				{
					activateFlag = true;
				}
			}
			if (activateFlag)
			{
				this.SelectSTPlace(null, true, null);
				base.AI.SelectCard(selfTarget);
				return true;
			}
			return false;
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x00062F58 File Offset: 0x00061158
		public bool CalledbytheGraveActivate()
		{
			if (this.CheckWhetherNegated(false) || !this.CheckLastChainShouldNegated())
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
					if (base.Enemy.Graveyard.GetFirstMatchingCard((ClientCard card) => card.IsMonster() && card.IsOriginalCode(code)) != null)
					{
						if (base.Card.Location != CardLocation.SpellZone)
						{
							this.SelectSTPlace(null, true, null);
						}
						base.AI.SelectCard(code);
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
						int code4 = cards.GetOriginCode();
						base.AI.SelectCard(cards);
						this.currentNegatingIdList.Add(code4);
						return true;
					}
				}
				if (!base.Duel.ChainTargets.Contains(base.Card))
				{
					goto IL_0248;
				}
				List<ClientCard> enemyMonsters = base.Enemy.Graveyard.GetMatchingCards((ClientCard card) => card.IsMonster()).ToList<ClientCard>();
				if (enemyMonsters.Count<ClientCard>() > 0)
				{
					enemyMonsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					enemyMonsters.Reverse();
					int code2 = enemyMonsters[0].Id;
					base.AI.SelectCard(enemyMonsters);
					this.currentNegatingIdList.Add(code2);
					return true;
				}
			}
			IL_0248:
			if (base.Duel.LastChainPlayer == 1)
			{
				return false;
			}
			List<ClientCard> targets = this.GetDangerousCardinEnemyGrave(true);
			if (targets.Count<ClientCard>() > 0)
			{
				int code3 = targets[0].Id;
				if (base.Card.Location != CardLocation.SpellZone)
				{
					this.SelectSTPlace(null, true, null);
				}
				base.AI.SelectCard(targets);
				this.currentNegatingIdList.Add(code3);
				return true;
			}
			return false;
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x00063224 File Offset: 0x00061424
		public bool CrossoutDesignatorActivate()
		{
			if (this.CheckWhetherNegated(false) || !this.CheckLastChainShouldNegated())
			{
				return false;
			}
			if (base.Duel.LastChainPlayer == 1 && base.Util.GetLastChainCard() != null)
			{
				int code = base.Util.GetLastChainCard().GetOriginCode();
				if (code == 0)
				{
					return false;
				}
				if (code == 72270339)
				{
					return false;
				}
				if (this.CheckCalledbytheGrave(code) > 0)
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

		// Token: 0x06001228 RID: 4648 RVA: 0x000632D0 File Offset: 0x000614D0
		public bool WANTED_SeekerOfSinfulSpoilsActivate()
		{
			if (base.Card.Location == CardLocation.Hand || (base.Card.Location == CardLocation.SpellZone && base.Card.HasPosition(CardPosition.FaceDown)))
			{
				this.activatedCardIdList.Add(base.Card.Id);
				this.SelectSTPlace(null, true, null);
				return true;
			}
			return true;
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x0006332A File Offset: 0x0006152A
		public bool DogmatikaMatrixCanActivate()
		{
			return this.CheckRemainInDeck(new int[] { 51522296, 31002402, 60921537 }) > 0;
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x00063348 File Offset: 0x00061548
		public bool DogmatikaMatrixActivate()
		{
			if (this.CheckWhetherNegated(false))
			{
				return false;
			}
			if (base.Card.Location != CardLocation.Hand && (base.Card.Location != CardLocation.SpellZone || !base.Card.HasPosition(CardPosition.FaceDown)))
			{
				int option = 0;
				if (this.CheckWhetherWillbeRemoved())
				{
					option = 1;
				}
				if (!this.checkedEnemyExtra && base.Enemy.ExtraDeck.Count<ClientCard>() > 0)
				{
					option = 1;
				}
				if (base.Enemy.HasInMonstersZone(10158145, false, false, false) && this.avoid2Monster)
				{
					option = 1;
				}
				if (!base.Bot.HasInExtra(80532587) || this.GetNormalEnemyTargetList(true, false, false).Count<ClientCard>() <= 0)
				{
					List<int> list = new List<int>();
					list.Add(11765832);
					list.Add(53971455);
					list.Add(41373230);
					list.Add(24915933);
					list.Add(74586817);
					bool checkFlag = false;
					foreach (int checkId in list)
					{
						checkFlag |= !this.discardExtraThisTurn.Contains(checkId) && !this.activatedCardIdList.Contains(checkId) && base.Bot.HasInExtra(checkId);
					}
					if (!checkFlag)
					{
						option = 1;
					}
				}
				Logger.DebugWriteLine("===Matrix option: " + option.ToString());
				this.matrixActivating = true;
				base.AI.SelectOption(option);
				this.activatedMatrixList.Add(base.Card);
				return true;
			}
			List<int> neededRitualCardIdList = this.GetNeedSearchRitualCardIdList();
			if (base.Enemy.GetMonsterCount() == 0)
			{
				if (!base.Bot.MonsterZone.Any((ClientCard card) => card != null && card.IsFaceup() && card.HasType(CardType.Ritual) && card.HasSetcode(325)) && neededRitualCardIdList.Count<int>() <= 0)
				{
					return false;
				}
				this.SelectSTPlace(null, true, null);
				base.AI.SelectCard(neededRitualCardIdList);
				base.AI.SelectYesNo(true);
				this.activatedCardIdList.Add(base.Card.Id);
				return true;
			}
			else
			{
				if (neededRitualCardIdList.Count<int>() <= 0)
				{
					this.SelectSTPlace(null, true, null);
					base.AI.SelectYesNo(true);
					if (this.CheckRemainInDeck(51522296) > 0 && this.CheckRemainInDeck(new int[] { 31002402, 60921537 }) > 0)
					{
						base.AI.SelectCard(51522296);
						base.AI.SelectNextCard(new int[] { 31002402, 60921537 });
					}
					else
					{
						base.AI.SelectCard(new int[] { 51522296, 31002402, 60921537 });
						this.DogmatikaMatrixNextSearch();
					}
					this.activatedCardIdList.Add(base.Card.Id);
					return true;
				}
				this.SelectSTPlace(null, true, null);
				base.AI.SelectCard(neededRitualCardIdList);
				base.AI.SelectYesNo(true);
				this.DogmatikaMatrixNextSearch();
				this.activatedCardIdList.Add(base.Card.Id);
				return true;
			}
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x0006366C File Offset: 0x0006186C
		public void DogmatikaMatrixNextSearch()
		{
			List<ClientCard> monsters = base.Bot.GetMonsters();
			monsters.AddRange(base.Enemy.GetMonsters());
			bool hasExtraOnField = monsters.Any((ClientCard card) => card.HasType((CardType)75505728));
			if (!this.activatedCardIdList.Contains(60303688) && this.CheckCalledbytheGrave(60303688) == 0 && (hasExtraOnField || !this.summoned) && this.CheckRemainInDeck(60303688) > 0)
			{
				base.AI.SelectNextCard(60303688);
				return;
			}
			if (this.CheckRemainInDeck(95679145) > 0 && !this.activatedCardIdList.Contains(95679145))
			{
				if (base.Bot.Graveyard.Where((ClientCard card) => card.HasType((CardType)75505728)).Count<ClientCard>() > 0)
				{
					base.AI.SelectNextCard(95679145);
					return;
				}
			}
			if (this.CheckRemainInDeck(69680031) > 0 && !base.Bot.HasInHand(69680031) && hasExtraOnField)
			{
				if (base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && card.HasSetcode(325)))
				{
					base.AI.SelectNextCard(69680031);
					return;
				}
			}
			foreach (int searchId in new List<int> { 82956214, 60303688, 35569555, 95679145, 69680031, 51522296, 31002402, 60921537 })
			{
				if (this.CheckRemainInDeck(searchId) > 0)
				{
					base.AI.SelectNextCard(searchId);
					break;
				}
			}
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x00063884 File Offset: 0x00061A84
		public bool InfiniteImpermanenceActivate()
		{
			if (this.CheckWhetherNegated(false))
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
					ClientCard target = this.GetProblematicEnemyMonster(0, true, false);
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

		// Token: 0x0600122D RID: 4653 RVA: 0x00063A74 File Offset: 0x00061C74
		public bool DogmatikaPunishmentActivate()
		{
			if (this.CheckWhetherNegated(false) || this.CheckWhetherWillbeRemoved())
			{
				return false;
			}
			ClientCard targetCard = null;
			ClientCard extraToDiscard = null;
			List<ClientCard> targetList = this.GetProblematicEnemyCardList(true, true);
			if (targetList.Count<ClientCard>() > 0 && base.Duel.LastChainPlayer != 0 && base.Bot.HasInExtra(80532587))
			{
				foreach (ClientCard target in targetList)
				{
					if (target.IsFaceup() && target.IsMonster() && target.Attack <= 2500)
					{
						targetCard = target;
						extraToDiscard = this.GetExtraToDiscard(2500, target);
						if (extraToDiscard != null)
						{
							break;
						}
					}
				}
				if (targetCard == null || extraToDiscard == null)
				{
					List<ClientCard> list = (from card in base.Enemy.GetMonsters()
						where card.IsFaceup() && !card.IsShouldNotBeTarget() && card.IsShouldNotBeSpellTrapTarget()
						select card).ToList<ClientCard>();
					list.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					foreach (ClientCard target2 in list)
					{
						if (target2.IsFaceup() && target2.IsMonster() && target2.Attack <= 2500)
						{
							targetCard = target2;
							extraToDiscard = this.GetExtraToDiscard(2500, target2);
							if (extraToDiscard != null)
							{
								break;
							}
						}
					}
				}
			}
			if (targetCard == null || extraToDiscard == null)
			{
				targetCard = this.GetProblematicEnemyMonster(0, true, true);
				if (targetCard != null)
				{
					extraToDiscard = this.GetExtraToDiscard(targetCard.Attack, targetCard);
				}
			}
			if (targetCard == null || extraToDiscard == null)
			{
				bool check = base.DefaultOnBecomeTarget();
				bool flag;
				if (base.Bot.UnderAttack)
				{
					ClientCard battlingMonster = base.Bot.BattlingMonster;
					int num = ((battlingMonster != null) ? battlingMonster.GetDefensePower() : 0);
					ClientCard battlingMonster2 = base.Enemy.BattlingMonster;
					if (num <= ((battlingMonster2 != null) ? battlingMonster2.GetDefensePower() : 0))
					{
						flag = base.Duel.LastChainPlayer != 0;
						goto IL_01E1;
					}
				}
				flag = false;
				IL_01E1:
				bool check2 = flag;
				bool check3 = base.Duel.Player == 1 && base.Duel.Phase == DuelPhase.End && base.Duel.LastChainPlayer != 0;
				bool check4 = base.Duel.Player == 1 && this.avoid2Monster && base.Enemy.GetMonsterCount() >= 2 && base.Duel.LastChainPlayer != 0;
				Logger.DebugWriteLine(string.Concat(new string[]
				{
					"===punishment check flag: ",
					check.ToString(),
					" ",
					check2.ToString(),
					" ",
					check3.ToString(),
					" ",
					check4.ToString()
				}));
				if (check || check2 || check3 || check4)
				{
					foreach (ClientCard checkTarget in (from card in base.Enemy.GetMonsters()
						where card.IsFaceup() && !card.IsShouldNotBeTarget() && !this.currentDestroyCardList.Contains(card)
						select card into c
						orderby c.Attack descending
						select c).ToList<ClientCard>())
					{
						extraToDiscard = this.GetExtraToDiscard(checkTarget.Attack, checkTarget);
						if (extraToDiscard != null)
						{
							targetCard = checkTarget;
							break;
						}
					}
				}
			}
			if (targetCard != null && extraToDiscard != null)
			{
				base.AI.SelectCard(targetCard);
				base.AI.SelectNextCard(extraToDiscard);
				this.currentDestroyCardList.Add(targetCard);
				this.discardExtraThisTurn.Add((extraToDiscard != null) ? extraToDiscard.Id : 0);
				this.activatedCardIdList.Add(base.Card.Id);
				this.UpdateBanSpSummonFromExTurn(2);
				return true;
			}
			return false;
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x00063E48 File Offset: 0x00062048
		public bool GranguignolTheDuskDragonActivate()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				base.AI.SelectCard(new int[] { 53971455, 60303688, 95679145, 69680031 });
				return true;
			}
			return false;
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x00063E78 File Offset: 0x00062078
		public bool TitanikladTheAshDragonActivate()
		{
			if (!this.activatedCardIdList.Contains(60303688) && this.CheckRemainInDeck(60303688) > 0 && this.CheckCalledbytheGrave(60303688) == 0)
			{
				base.AI.SelectOption(1);
				base.AI.SelectCard(60303688);
				return true;
			}
			if (this.CheckRemainInDeck(69680031) > 0)
			{
				if (!base.Bot.HasInHand(69680031) && !this.enemyActivateLockBird)
				{
					if (base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && card.HasSetcode(325)))
					{
						base.AI.SelectOption(0);
						base.AI.SelectCard(69680031);
						return true;
					}
				}
				if (base.Duel.Player == 1 && base.Enemy.GetMonsterCount() == 0)
				{
					base.AI.SelectOption(1);
					base.AI.SelectCard(69680031);
					return true;
				}
			}
			if (this.CheckRemainInDeck(95679145) > 0)
			{
				base.AI.SelectOption(1);
				base.AI.SelectCard(95679145);
				return true;
			}
			return false;
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x00063FB0 File Offset: 0x000621B0
		public bool GaruraWingsOfResonantLifeActivate()
		{
			this.activatedCardIdList.Add(base.Card.Id);
			return true;
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x00063FCC File Offset: 0x000621CC
		public bool ElderEntityNtssActivate()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				List<ClientCard> destroyList = this.GetNormalEnemyTargetList(true, true, true);
				if (destroyList.Count<ClientCard>() > 0)
				{
					this.currentDestroyCardList.Add(destroyList[0]);
					base.AI.SelectCard(destroyList);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x0006401C File Offset: 0x0006221C
		public bool DespianLuluwalilithActivate()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				if (!this.activatedCardIdList.Contains(60303688) && this.CheckRemainInDeck(60303688) > 0 && this.CheckCalledbytheGrave(60303688) == 0 && !this.enemyActivateLockBird)
				{
					base.AI.SelectCard(60303688);
					return true;
				}
				if (this.CheckRemainInDeck(62849088) > 0)
				{
					base.AI.SelectCard(62849088);
					return true;
				}
				if (base.Duel.Turn > 1 && base.Enemy.GetMonsterCount() == 0 && this.CheckRemainInDeck(69680031) > 0)
				{
					base.AI.SelectCard(69680031);
					return true;
				}
				if (base.Bot.HasInHand(69680031))
				{
					if (!base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && card.HasSetcode(325)))
					{
						foreach (int checkId in new List<int> { 95679145, 60303688, 69680031 })
						{
							if (this.CheckRemainInDeck(checkId) > 0)
							{
								base.AI.SelectCard(checkId);
								return true;
							}
						}
					}
				}
			}
			if (base.Card.Location == CardLocation.MonsterZone)
			{
				List<ClientCard> currentChainEnemyCard = base.Duel.CurrentChain.Where((ClientCard card) => card.Controller == 1 && !this.currentNegateMonsterList.Contains(card) && (card.Location == CardLocation.MonsterZone || card.Location == CardLocation.SpellZone)).ToList<ClientCard>();
				currentChainEnemyCard.AddRange(this.GetProblematicEnemyCardList(false, false));
				currentChainEnemyCard.AddRange(this.ShuffleCardList((from card in base.Enemy.GetSpells()
					where card.IsFaceup()
					select card).ToList<ClientCard>()));
				currentChainEnemyCard.AddRange(this.ShuffleCardList((from card in base.Enemy.GetMonsters()
					where card.IsFaceup()
					select card).ToList<ClientCard>()));
				if (currentChainEnemyCard.Count<ClientCard>() > 0)
				{
					this.currentNegateMonsterList.Add(currentChainEnemyCard[0]);
					base.AI.SelectYesNo(true);
					base.AI.SelectCard(currentChainEnemyCard);
				}
				else
				{
					base.AI.SelectYesNo(false);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x000642A8 File Offset: 0x000624A8
		public bool PSYFramelordOmegaActivate()
		{
			if (base.Card.Location == CardLocation.Grave && this.omegaActivateCount <= 5)
			{
				if (this.CheckWhetherNegated(false))
				{
					return false;
				}
				List<ClientCard> targets = this.GetDangerousCardinEnemyGrave(true);
				if (targets.Count<ClientCard>() > 0)
				{
					base.AI.SelectCard(targets);
					this.omegaActivateCount++;
					return true;
				}
				List<int> recycleExtraIdList = new List<int> { 11765832, 80532587 };
				foreach (int checkId in recycleExtraIdList)
				{
					if (!base.Bot.HasInExtra(checkId) && base.Bot.HasInGraveyard(checkId))
					{
						base.AI.SelectCard(checkId);
						this.omegaActivateCount++;
						return true;
					}
				}
				foreach (int checkId2 in new List<int> { 60921537, 31002402, 51522296, 82956214, 60303688, 69680031, 35569555 })
				{
					if (this.CheckRemainInDeck(checkId2) <= 0 && base.Bot.HasInGraveyard(checkId2))
					{
						base.AI.SelectCard(checkId2);
						this.omegaActivateCount++;
						return true;
					}
				}
				recycleExtraIdList.AddRange(new List<int> { 93039339, 41373230, 79606837, 53971455, 24842059, 60303245, 2220237 });
				foreach (int checkId3 in recycleExtraIdList)
				{
					if (!base.Bot.HasInExtra(checkId3) && base.Bot.HasInGraveyard(checkId3))
					{
						base.AI.SelectCard(checkId3);
						this.omegaActivateCount++;
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x00064538 File Offset: 0x00062738
		public bool HeraldOfTheArcLightActivate()
		{
			base.AI.SelectCard(this.GetNeedSearchRitualCardIdList());
			return true;
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x0006454C File Offset: 0x0006274C
		public bool SuperStarslayerTYPHONSpSummon()
		{
			ClientCard material = (from card in base.Bot.GetMonsters()
				where card.IsFaceup()
				orderby card.Attack descending
				select card).FirstOrDefault<ClientCard>();
			if (material == null || material.Attack >= 3000)
			{
				return false;
			}
			if ((this.GetProblematicEnemyMonster(material.Attack, false, false) != null) | material.HasType(CardType.Link) | (material.Level <= 4))
			{
				base.AI.SelectMaterials(material, 0);
				return true;
			}
			return false;
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x00064600 File Offset: 0x00062800
		public bool SuperStarslayerTYPHONActivate()
		{
			if (this.CheckWhetherNegated(false))
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
				targetList.AddRange(this.ShuffleCardList((from card in base.Enemy.GetMonsters()
					where card.IsFacedown() && !targetList.Contains(card)
					select card).ToList<ClientCard>()));
				targetList.AddRange(this.ShuffleCardList((from card in base.Bot.GetMonsters()
					where card.IsFacedown() && !targetList.Contains(card)
					select card).ToList<ClientCard>()));
				targetList.AddRange(from card in base.Bot.GetMonsters()
					where card.IsFaceup() && !targetList.Contains(card)
					orderby card.Attack
					select card);
				base.AI.SelectCard(base.Card.Overlays);
				base.AI.SelectNextCard(targetList);
				return true;
			}
			return false;
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SPLittleKnightActivate()
		{
			return false;
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x000648F0 File Offset: 0x00062AF0
		public bool SecureGardnaSpSummon()
		{
			if (base.Bot.HasInHand(95679145))
			{
				if (!base.Bot.Graveyard.Any((ClientCard card) => card.IsMonster() && card.HasType((CardType)75505728)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x00064944 File Offset: 0x00062B44
		public bool LinguribohSpSummon()
		{
			return base.Enemy.GetSpells().Any((ClientCard card) => card.IsFacedown()) || !base.Bot.HasInExtra(60303245);
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x00064999 File Offset: 0x00062B99
		public bool LinguribohActivate()
		{
			return this.CheckLastChainShouldNegated();
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x000649A8 File Offset: 0x00062BA8
		public bool SalamangreatAlmirajSpSummon()
		{
			if (base.Bot.HasInMonstersZone(10158145, false, false, true))
			{
				base.AI.SelectMaterials(10158145, 0);
				return true;
			}
			if (base.Bot.HasInHand(new List<int> { 60303688, 95679145, 1984618 }))
			{
				List<ClientCard> materialList = base.Bot.MonsterZone.Where((ClientCard card) => card != null && card.IsFaceup() && card.Attack <= 1000 && !card.HasType((CardType)75505856)).ToList<ClientCard>();
				if (materialList.Count<ClientCard>() > 0)
				{
					materialList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					base.AI.SelectMaterials(materialList, 0);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x00064A70 File Offset: 0x00062C70
		public bool SalamangreatAlmirajActivate()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				return true;
			}
			if ((base.Duel.Player == 1 && (!base.Bot.HasInHand(69680031) || this.activatedCardIdList.Contains(69680031))) | base.DefaultOnBecomeTarget() | (base.Bot.UnderAttack && base.Bot.BattlingMonster == base.Card))
			{
				base.AI.SelectCard(base.Util.GetBestBotMonster(false));
				return true;
			}
			if (!base.Util.ChainContainsCard(16240772))
			{
				List<ClientCard> list = (from card in base.Bot.GetMonsters()
					where card.IsFaceup() && card != base.Card
					orderby card.Attack descending
					select card).ToList<ClientCard>();
				list.AddRange(base.Bot.GetMonsters().GetMatchingCards((ClientCard card) => card.IsFacedown()));
				foreach (ClientCard card2 in list)
				{
					if (base.Util.IsChainTarget(card2))
					{
						base.AI.SelectCard(card2);
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x00064BF4 File Offset: 0x00062DF4
		public bool SummonForTYPHONCheck()
		{
			if (base.Bot.HasInExtra(93039339))
			{
				if (!base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup()) && this.banSpSummonFromExTurn <= 0)
				{
					if (this.enemySpSummonFromExLastTurn < 2 && this.enemySpSummonFromExThisTurn < 2)
					{
						return false;
					}
					if (base.Card.IsCode(10158145) && !this.CheckWhetherNegated(false))
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

		// Token: 0x0600123E RID: 4670 RVA: 0x00064D38 File Offset: 0x00062F38
		public bool MonsterRepos()
		{
			int selfAttack = base.Card.Attack + 1;
			int extraAttackForDogmatika = 0;
			if (!this.activatedCardIdList.Contains(69680032) && base.Bot.HasInMonstersZone(69680031, true, false, true))
			{
				extraAttackForDogmatika += 500;
			}
			if (base.Card.HasSetcode(325))
			{
				selfAttack += extraAttackForDogmatika;
			}
			if (base.Card.IsFaceup() && base.Card.IsDefense() && selfAttack <= 1)
			{
				return false;
			}
			int bestAttack = 0;
			foreach (ClientCard clientCard in base.Bot.GetMonsters())
			{
				int attack = clientCard.Attack;
				if (clientCard.HasSetcode(325))
				{
					attack += extraAttackForDogmatika;
				}
				if (attack >= bestAttack)
				{
					bestAttack = attack;
				}
			}
			bool enemyBetter = base.Util.IsAllEnemyBetterThanValue(bestAttack, true);
			return (base.Card.IsAttack() && enemyBetter) || (base.Card.IsDefense() && !enemyBetter);
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x00064E54 File Offset: 0x00063054
		public bool SpellSetCheck()
		{
			if (base.Duel.Phase == DuelPhase.Main1 && base.Bot.HasAttackingMonster() && base.Duel.Turn > 1)
			{
				return false;
			}
			if (base.Card.IsCode(82956214) && base.Bot.HasInSpellZone(base.Card.Id, false, false))
			{
				return false;
			}
			if (base.Card.IsCode(16240772) && !base.Bot.HasInHand(69680031))
			{
				if (!base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup() && card.Level >= 7 && card.HasRace(CardRace.SpellCaster)))
				{
					return false;
				}
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

		// Token: 0x06001240 RID: 4672 RVA: 0x00065042 File Offset: 0x00063242
		protected override bool DefaultSetForDiabellze()
		{
			if (base.DefaultSetForDiabellze())
			{
				this.SelectSTPlace(null, true, null);
				return true;
			}
			return false;
		}

		// Token: 0x040016BB RID: 5819
		private const int SetcodeTimeLord = 74;

		// Token: 0x040016BC RID: 5820
		private const int SetcodePhantom = 219;

		// Token: 0x040016BD RID: 5821
		private const int SetcodeOrcust = 283;

		// Token: 0x040016BE RID: 5822
		private const int SetcodeDogmatika = 325;

		// Token: 0x040016BF RID: 5823
		private const int hintTimingMainEnd = 4;

		// Token: 0x040016C0 RID: 5824
		private const int hintDamageStep = 8192;

		// Token: 0x040016C1 RID: 5825
		private Dictionary<int, List<int>> DeckCountTable = new Dictionary<int, List<int>>
		{
			{
				3,
				new List<int> { 60303688, 14558127, 23434538, 10158145, 1984618, 80845034, 35569555, 10045474, 82956214 }
			},
			{
				2,
				new List<int> { 51522296, 69680031, 24224830 }
			},
			{
				1,
				new List<int> { 62849088, 95679145, 72270339, 31002402, 60921537, 16240772, 65681983 }
			}
		};

		// Token: 0x040016C2 RID: 5826
		private List<int> notToNegateIdList = new List<int> { 58699500, 20343502 };

		// Token: 0x040016C3 RID: 5827
		private List<int> discardEnemyExtraIdList = new List<int> { 90448279, 29301450, 90590303, 70534340, 60465049, 24094258, 86066372 };

		// Token: 0x040016C4 RID: 5828
		private List<int> currentNegatingIdList = new List<int>();

		// Token: 0x040016C5 RID: 5829
		private bool enemyActivateMaxxC;

		// Token: 0x040016C6 RID: 5830
		private bool enemyActivateLockBird;

		// Token: 0x040016C7 RID: 5831
		private List<int> infiniteImpermanenceList = new List<int>();

		// Token: 0x040016C8 RID: 5832
		private bool summoned;

		// Token: 0x040016C9 RID: 5833
		private List<int> activatedCardIdList = new List<int>();

		// Token: 0x040016CA RID: 5834
		private List<ClientCard> currentNegateMonsterList = new List<ClientCard>();

		// Token: 0x040016CB RID: 5835
		private List<ClientCard> currentDestroyCardList = new List<ClientCard>();

		// Token: 0x040016CC RID: 5836
		private List<int> discardExtraThisTurn = new List<int>();

		// Token: 0x040016CD RID: 5837
		private int banSpSummonFromExTurn;

		// Token: 0x040016CE RID: 5838
		private List<ClientCard> activatedMatrixList = new List<ClientCard>();

		// Token: 0x040016CF RID: 5839
		private List<int> maximusDiscardExtraIdList = new List<int>();

		// Token: 0x040016D0 RID: 5840
		private bool checkedEnemyExtra;

		// Token: 0x040016D1 RID: 5841
		private bool matrixActivating;

		// Token: 0x040016D2 RID: 5842
		private bool avoid2Monster = true;

		// Token: 0x040016D3 RID: 5843
		private bool confirmLink2;

		// Token: 0x040016D4 RID: 5844
		private int omegaActivateCount;

		// Token: 0x040016D5 RID: 5845
		private int dimensionShifterCount;

		// Token: 0x040016D6 RID: 5846
		private int enemySpSummonFromExLastTurn;

		// Token: 0x040016D7 RID: 5847
		private int enemySpSummonFromExThisTurn;

		// Token: 0x040016D8 RID: 5848
		private bool enemySpSummonFromDeck;

		// Token: 0x040016D9 RID: 5849
		private bool enemySpSummonFromExtra;

		// Token: 0x020002D8 RID: 728
		public class CardId
		{
			// Token: 0x040016DA RID: 5850
			public const int DogmatikaAlbaZoa = 51522296;

			// Token: 0x040016DB RID: 5851
			public const int ThesIrisSwordsoul = 62849088;

			// Token: 0x040016DC RID: 5852
			public const int DogmatikaFleurdelis = 69680031;

			// Token: 0x040016DD RID: 5853
			public const int DogmatikaMaximus = 95679145;

			// Token: 0x040016DE RID: 5854
			public const int DiabellstarTheBlackWitch = 72270339;

			// Token: 0x040016DF RID: 5855
			public const int DogmatikaEcclesia = 60303688;

			// Token: 0x040016E0 RID: 5856
			public const int KnightmareCorruptorIblee = 10158145;

			// Token: 0x040016E1 RID: 5857
			public const int NadirServant = 1984618;

			// Token: 0x040016E2 RID: 5858
			public const int DogmatikaLamity = 31002402;

			// Token: 0x040016E3 RID: 5859
			public const int DogmatikaMacabre = 60921537;

			// Token: 0x040016E4 RID: 5860
			public const int SinfulSpoilsOfDoom_Rciela = 16240772;

			// Token: 0x040016E5 RID: 5861
			public const int WANTED_SeekerOfSinfulSpoils = 80845034;

			// Token: 0x040016E6 RID: 5862
			public const int DogmatikaMatrix = 35569555;

			// Token: 0x040016E7 RID: 5863
			public const int DogmatikaPunishment = 82956214;

			// Token: 0x040016E8 RID: 5864
			public const int GranguignolTheDuskDragon = 24915933;

			// Token: 0x040016E9 RID: 5865
			public const int TitanikladTheAshDragon = 41373230;

			// Token: 0x040016EA RID: 5866
			public const int GaruraWingsOfResonantLife = 11765832;

			// Token: 0x040016EB RID: 5867
			public const int ElderEntityNtss = 80532587;

			// Token: 0x040016EC RID: 5868
			public const int DespianLuluwalilith = 53971455;

			// Token: 0x040016ED RID: 5869
			public const int PSYFramelordOmega = 74586817;

			// Token: 0x040016EE RID: 5870
			public const int HeraldOfTheArcLight = 79606837;

			// Token: 0x040016EF RID: 5871
			public const int SuperStarslayerTYPHON = 93039339;

			// Token: 0x040016F0 RID: 5872
			public const int SPLittleKnight = 29301450;

			// Token: 0x040016F1 RID: 5873
			public const int SecureGardna = 2220237;

			// Token: 0x040016F2 RID: 5874
			public const int Linguriboh = 24842059;

			// Token: 0x040016F3 RID: 5875
			public const int SalamangreatAlmiraj = 60303245;

			// Token: 0x040016F4 RID: 5876
			public const int NaturalExterio = 99916754;

			// Token: 0x040016F5 RID: 5877
			public const int NaturalBeast = 33198837;

			// Token: 0x040016F6 RID: 5878
			public const int ImperialOrder = 61740673;

			// Token: 0x040016F7 RID: 5879
			public const int SwordsmanLV7 = 37267041;

			// Token: 0x040016F8 RID: 5880
			public const int RoyalDecree = 51452091;

			// Token: 0x040016F9 RID: 5881
			public const int Number41BagooskatheTerriblyTiredTapir = 90590303;

			// Token: 0x040016FA RID: 5882
			public const int InspectorBoarder = 15397015;

			// Token: 0x040016FB RID: 5883
			public const int SkillDrain = 82732705;

			// Token: 0x040016FC RID: 5884
			public const int DimensionShifter = 91800273;

			// Token: 0x040016FD RID: 5885
			public const int MacroCosmos = 30241314;

			// Token: 0x040016FE RID: 5886
			public const int DimensionalFissure = 81674782;

			// Token: 0x040016FF RID: 5887
			public const int BanisheroftheRadiance = 94853057;

			// Token: 0x04001700 RID: 5888
			public const int BanisheroftheLight = 61528025;

			// Token: 0x04001701 RID: 5889
			public const int GhostMournerMoonlitChill = 52038441;
		}
	}
}
