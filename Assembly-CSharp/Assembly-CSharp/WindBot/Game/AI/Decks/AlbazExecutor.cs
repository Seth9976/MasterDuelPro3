using System;
using System.Collections.Generic;
using System.Linq;
using YGOSharp.OCGWrapper;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x02000252 RID: 594
	[Deck("Albaz", "AI_Albaz", "Normal")]
	public class AlbazExecutor : DefaultExecutor
	{
		// Token: 0x06000CED RID: 3309 RVA: 0x00036F10 File Offset: 0x00035110
		public AlbazExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 23434538, new Func<bool>(this.MaxxCActivate));
			base.AddExecutor(ExecutorType.Summon, new Func<bool>(this.AdvanceSummon));
			base.AddExecutor(ExecutorType.SpSummon, 3410461, new Func<bool>(this.AlbaLenatusTheAbyssDragonSpSummon));
			base.AddExecutor(ExecutorType.MonsterSet, 68468459, new Func<bool>(this.FallenOfAlbazSet));
			base.AddExecutor(ExecutorType.Activate, 95515789, new Func<bool>(this.BlazingCartesiaTheVirtuousActivateInGrave));
			base.AddExecutor(ExecutorType.Activate, 17751597, new Func<bool>(this.BrandedRetributionActivate));
			base.AddExecutor(ExecutorType.Activate, 24224830, new Func<bool>(this.CalledbytheGraveActivate));
			base.AddExecutor(ExecutorType.Activate, 65681983, new Func<bool>(this.CrossoutDesignatorActivate));
			base.AddExecutor(ExecutorType.Activate, 10045474, new Func<bool>(this.InfiniteImpermanenceActivate));
			base.AddExecutor(ExecutorType.Activate, 14558127, new Func<bool>(this.AshBlossomActivate));
			base.AddExecutor(ExecutorType.Activate, 32756828, new Func<bool>(this.BrandedBeastActivate));
			base.AddExecutor(ExecutorType.Activate, 19271881, new Func<bool>(this.BrightestBlazingBrandedKingActivate));
			base.AddExecutor(ExecutorType.Activate, 36637374, new Func<bool>(this.BrandedOpeningActivate));
			base.AddExecutor(ExecutorType.Activate, 29948294, new Func<bool>(this.BrandedInHighSpiritsActivate));
			base.AddExecutor(ExecutorType.Activate, 51409648, new Func<bool>(this.RindbrummTheStrikingDragonActivate));
			base.AddExecutor(ExecutorType.Activate, 11321089, new Func<bool>(this.GuardianChimeraActivate));
			base.AddExecutor(ExecutorType.Activate, 92892239, new Func<bool>(this.BorreloadFuriousDragonActivate));
			base.AddExecutor(ExecutorType.Activate, 19096726, new Func<bool>(this.TriBrigadeMercourierActivate));
			base.AddExecutor(ExecutorType.Activate, 72272462, new Func<bool>(this.DespianQuaeritisActivate));
			base.AddExecutor(ExecutorType.Activate, 44146295, new Func<bool>(this.MirrorjadeTheIcebladeDragonActivate));
			base.AddExecutor(ExecutorType.Activate, 32731036, new Func<bool>(this.TheBystialLubellionActivate));
			base.AddExecutor(ExecutorType.SpSummon, 32731036, new Func<bool>(this.TheBystialLubellionSpSummon));
			base.AddExecutor(ExecutorType.Activate, 81439173, new Func<bool>(this.FoolishBurialActivate));
			base.AddExecutor(ExecutorType.Activate, 75500286, new Func<bool>(this.GoldSarcophagusActivate));
			base.AddExecutor(ExecutorType.Activate, 62962630, new Func<bool>(this.AluberTheJesterOfDespiaActivate));
			base.AddExecutor(ExecutorType.Activate, 38811586, new Func<bool>(this.AlbionTheSanctifireDragonActivate));
			base.AddExecutor(ExecutorType.Activate, 6498706, new Func<bool>(this.FusionDeploymentActivate));
			base.AddExecutor(ExecutorType.Activate, 25451383, new Func<bool>(this.AlbionTheShroudedDragonActivate));
			base.AddExecutor(ExecutorType.Activate, 60242223, new Func<bool>(this.BystialSaronirActivate));
			base.AddExecutor(ExecutorType.Summon, 62962630, new Func<bool>(this.AluberTheJesterOfDespiaSummon));
			base.AddExecutor(ExecutorType.Summon, 45883110, new Func<bool>(this.GuidingQuemTheVirtuousSummonForSearch));
			base.AddExecutor(ExecutorType.Activate, 45484331, new Func<bool>(this.SpringansKittActivate));
			base.AddExecutor(ExecutorType.Summon, 45484331, new Func<bool>(this.SpringansKittSummon));
			base.AddExecutor(ExecutorType.Activate, 18973184, new Func<bool>(this.BrandedLostCardActivate));
			base.AddExecutor(ExecutorType.Activate, 24915933, new Func<bool>(this.GranguignolTheDuskDragonActivate));
			base.AddExecutor(ExecutorType.Activate, 3410461, new Func<bool>(this.AlbaLenatusTheAbyssDragonActivate));
			base.AddExecutor(ExecutorType.Activate, 1906812, new Func<bool>(this.SprindTheIrondashDragonActivate));
			base.AddExecutor(ExecutorType.Activate, 41373230, new Func<bool>(this.TitanikladTheAshDragonActivate));
			base.AddExecutor(ExecutorType.Activate, 87746184, new Func<bool>(this.AlbionTheBrandedDragonActivate));
			base.AddExecutor(ExecutorType.Activate, 70534340, new Func<bool>(this.LubellionTheSearingDragonActivate));
			base.AddExecutor(ExecutorType.Summon, 95515789, new Func<bool>(this.BlazingCartesiaTheVirtuousSummon));
			base.AddExecutor(ExecutorType.Activate, 68468459, new Func<bool>(this.FallenOfAlbazActivate));
			base.AddExecutor(ExecutorType.Activate, 44362883, new Func<bool>(this.BrandedFusionActivate));
			base.AddExecutor(ExecutorType.Activate, 95515789, new Func<bool>(this.BlazingCartesiaTheVirtuousActivate));
			base.AddExecutor(ExecutorType.Activate, 34995106, new Func<bool>(this.BrandedInWhiteActivate));
			base.AddExecutor(ExecutorType.Activate, 82738008, new Func<bool>(this.BrandedInRedActivate));
			base.AddExecutor(ExecutorType.Activate, 18973184, new Func<bool>(this.BrandedLostActivate));
			base.AddExecutor(ExecutorType.Summon, 68468459, new Func<bool>(this.FallenOfAlbazSummon));
			base.AddExecutor(ExecutorType.Summon, 45883110, new Func<bool>(this.GuidingQuemTheVirtuousSummon));
			base.AddExecutor(ExecutorType.Activate, 36577931, new Func<bool>(this.DespianTragedyActivate));
			base.AddExecutor(ExecutorType.Activate, 19096726, new Func<bool>(this.TriBrigadeMercourierActivateForSearch));
			base.AddExecutor(ExecutorType.Activate, 1984618, new Func<bool>(this.NadirServantActivate));
			base.AddExecutor(ExecutorType.MonsterSet, new Func<bool>(this.SetForChimera));
			base.AddExecutor(ExecutorType.MonsterSet, 36577931, new Func<bool>(this.DespianTragedySet));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(base.DefaultMonsterRepos));
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(this.SpellSetCheck));
			base.AddExecutor(ExecutorType.Activate, 45883110, new Func<bool>(this.GuidingQuemTheVirtuousActivate));
			base.AddExecutor(ExecutorType.Activate, 53971455, new Func<bool>(this.DespianLuluwalilithActivate));
			base.AddExecutor(ExecutorType.Activate, new Func<bool>(this.FloogateActivate));
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x0003795C File Offset: 0x00035B5C
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

		// Token: 0x06000CEF RID: 3311 RVA: 0x000379CC File Offset: 0x00035BCC
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
				if ((selfType & CardType.Trap) > (CardType)0 && card.IsShouldNotBeSpellTrapTarget() && !card.IsDisabled())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x00037A24 File Offset: 0x00035C24
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

		// Token: 0x06000CF1 RID: 3313 RVA: 0x00037A60 File Offset: 0x00035C60
		public int CheckRemainInDeck(params int[] ids)
		{
			int sum = 0;
			foreach (int id in ids)
			{
				sum += this.CheckRemainInDeck(id);
			}
			return sum;
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x00037A90 File Offset: 0x00035C90
		public bool CheckWhetherNegated(bool disablecheck = true, bool toFieldCheck = false, CardType type = (CardType)0)
		{
			bool isMonster = type == (CardType)0 && base.Card.IsMonster();
			isMonster |= (type & CardType.Monster) > (CardType)0;
			bool flag = (type == (CardType)0 && (base.Card.IsSpell() || base.Card.IsTrap())) | ((type & CardType.Spell) != (CardType)0 || (type & CardType.Trap) > (CardType)0);
			bool isCounter = (type & CardType.Counter) > (CardType)0;
			if (flag && toFieldCheck && this.CheckSpellWillBeNegate(isCounter, null))
			{
				return true;
			}
			if (base.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return true;
			}
			if (isMonster && (toFieldCheck || base.Card.Location == CardLocation.MonsterZone))
			{
				if (((toFieldCheck && (type & CardType.Link) != (CardType)0) || base.Card.IsDefense()) && (base.Enemy.MonsterZone.Any((ClientCard card) => this.CheckNumber41(card)) || base.Bot.MonsterZone.Any((ClientCard card) => this.CheckNumber41(card))))
				{
					return true;
				}
				if (base.Enemy.HasInSpellZone(82732705, true, true))
				{
					return true;
				}
			}
			return disablecheck && ((base.Card.Location == CardLocation.MonsterZone || base.Card.Location == CardLocation.SpellZone) && base.Card.IsDisabled()) && base.Card.IsFaceup();
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x00037BCF File Offset: 0x00035DCF
		public bool CheckNumber41(ClientCard card)
		{
			return card != null && card.IsFaceup() && card.IsCode(90590303) && card.IsDefense() && !card.IsDisabled();
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x00037BFC File Offset: 0x00035DFC
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

		// Token: 0x06000CF5 RID: 3317 RVA: 0x00037D38 File Offset: 0x00035F38
		public bool CheckLastChainShouldNegated()
		{
			ClientCard lastcard = base.Util.GetLastChainCard();
			return lastcard != null && lastcard.Controller == 1 && (!lastcard.IsMonster() || !lastcard.HasSetcode(74) || base.Duel.Phase != DuelPhase.Standby) && !this.notToNegateIdList.Contains(lastcard.Id) && !base.DefaultCheckWhetherCardIsNegated(lastcard) && (base.Duel.Turn != 1 || !lastcard.IsCode(23434538));
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x00037DBF File Offset: 0x00035FBF
		public bool CheckAtAdvantage()
		{
			return this.GetProblematicEnemyMonster(0, false, false, (CardType)0) == null && (base.Duel.Player == 0 || base.Bot.GetMonsterCount() > 0);
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x00037DEB File Offset: 0x00035FEB
		public bool CheckShouldNoMoreSpSummon()
		{
			return this.CheckAtAdvantage() && this.enemyActivateMaxxC && !this.enemyActivateLockBird && (base.Duel.Turn == 1 || base.Duel.Phase >= DuelPhase.Main2);
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x00037E28 File Offset: 0x00036028
		public bool CheckWhetherCanSummon()
		{
			return base.Duel.Player == 0 && base.Duel.Phase < DuelPhase.End && !this.summoned;
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x00037E54 File Offset: 0x00036054
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

		// Token: 0x06000CFA RID: 3322 RVA: 0x00037F60 File Offset: 0x00036160
		public bool CheckWhetherShouldKeepInGrave(ClientCard c)
		{
			return (c.IsCode(24915933) && c.Location == CardLocation.Grave) || (c.IsCode(new int[] { 3410461, 87746184, 41373230, 53971455, 1906812 }) && this.sendToGYThisTurn.Contains(c) && c.Location == CardLocation.Grave);
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x00037FBC File Offset: 0x000361BC
		public ClientCard GetProblematicEnemyMonster(int attack = 0, bool canBeTarget = false, bool ignoreCurrentDestroy = false, CardType selfType = (CardType)0)
		{
			ClientCard floodagateCard = (from c in base.Enemy.GetMonsters()
				where ((c != null) ? c.Data : null) != null && (ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(c)) && c.IsFloodgate() && c.IsFaceup() && this.CheckCanBeTargeted(c, canBeTarget, selfType) && this.CheckShouldNotIgnore(c, false)
				select c into card
				orderby card.Attack descending
				select card).FirstOrDefault<ClientCard>();
			if (floodagateCard != null)
			{
				return floodagateCard;
			}
			ClientCard dangerCard = (from c in base.Enemy.MonsterZone
				where ((c != null) ? c.Data : null) != null && (ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(c)) && c.IsMonsterDangerous() && c.IsFaceup() && this.CheckCanBeTargeted(c, canBeTarget, selfType) && this.CheckShouldNotIgnore(c, false)
				select c into card
				orderby card.Attack descending
				select card).FirstOrDefault<ClientCard>();
			if (dangerCard != null)
			{
				return dangerCard;
			}
			ClientCard invincibleCard = (from c in base.Enemy.MonsterZone
				where ((c != null) ? c.Data : null) != null && (ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(c)) && c.IsMonsterInvincible() && c.IsFaceup() && this.CheckCanBeTargeted(c, canBeTarget, selfType) && this.CheckShouldNotIgnore(c, false)
				select c into card
				orderby card.Attack descending
				select card).FirstOrDefault<ClientCard>();
			if (invincibleCard != null)
			{
				return invincibleCard;
			}
			ClientCard equippedCard = (from c in base.Enemy.MonsterZone
				where ((c != null) ? c.Data : null) != null && (ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(c)) && c.EquipCards.Count > 0 && this.CheckCanBeTargeted(c, canBeTarget, selfType) && this.CheckShouldNotIgnore(c, false)
				select c into card
				orderby card.Attack descending
				select card).FirstOrDefault<ClientCard>();
			if (equippedCard != null)
			{
				return equippedCard;
			}
			ClientCard enemyExtraMonster = (from c in base.Enemy.MonsterZone
				where c != null && (ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(c)) && (c.HasType((CardType)8396992) || (c.HasType(CardType.Link) && c.LinkCount >= 2)) && this.CheckCanBeTargeted(c, canBeTarget, selfType) && this.CheckShouldNotIgnore(c, false)
				select c into card
				orderby card.Attack descending
				select card).FirstOrDefault<ClientCard>();
			if (enemyExtraMonster != null)
			{
				return enemyExtraMonster;
			}
			ClientCard activatingAlbaz = base.Enemy.MonsterZone.FirstOrDefault((ClientCard c) => c != null && c.IsCode(68468459) && !c.IsDisabled() && !this.currentDestroyCardList.Contains(c) && !this.currentNegateCardList.Contains(c) && this.Duel.CurrentChain.Contains(c));
			if (activatingAlbaz != null)
			{
				return activatingAlbaz;
			}
			if (attack >= 0)
			{
				if (attack == 0)
				{
					attack = base.Util.GetBestAttack(base.Bot);
				}
				ClientCard betterCard = (from card in base.Enemy.MonsterZone
					where card != null && card.GetDefensePower() >= attack && card.GetDefensePower() > 0 && card.IsAttack() && this.CheckCanBeTargeted(card, canBeTarget, selfType) && (ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(card))
					orderby card.Attack descending
					select card).FirstOrDefault<ClientCard>();
				if (betterCard != null)
				{
					return betterCard;
				}
			}
			return null;
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x0003820C File Offset: 0x0003640C
		public bool CheckShouldNotIgnore(ClientCard cards, bool ignore = false)
		{
			return !ignore || (!this.currentDestroyCardList.Contains(cards) && !this.currentNegateCardList.Contains(cards));
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x00038234 File Offset: 0x00036434
		public List<ClientCard> GetDangerousCardinEnemyGrave(bool onlyMonster = false)
		{
			List<ClientCard> list = base.Enemy.Graveyard.GetMatchingCards((ClientCard card) => (!onlyMonster || card.IsMonster()) && (card.HasSetcode(283) || card.HasSetcode(219) || card.HasSetcode(413))).ToList<ClientCard>();
			List<int> dangerMonsterIdList = new List<int> { 99937011, 63542003, 9411399, 28954097, 30680659 };
			list.AddRange(base.Enemy.Graveyard.GetMatchingCards((ClientCard card) => dangerMonsterIdList.Contains(card.Id)));
			return list;
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x000382D4 File Offset: 0x000364D4
		public List<ClientCard> GetProblematicEnemyCardList(bool canBeTarget = false, bool ignoreSpells = false, CardType selfType = (CardType)0)
		{
			List<ClientCard> resultList = new List<ClientCard>();
			List<ClientCard> floodagateList = (from c in base.Enemy.MonsterZone
				where ((c != null) ? c.Data : null) != null && !this.currentDestroyCardList.Contains(c) && c.IsFloodgate() && c.IsFaceup() && this.CheckCanBeTargeted(c, canBeTarget, selfType)
				select c into card
				orderby card.Attack descending
				select card).ToList<ClientCard>();
			if (floodagateList.Count > 0)
			{
				resultList.AddRange(floodagateList);
			}
			List<ClientCard> problemEnemySpellList = base.Enemy.SpellZone.Where((ClientCard c) => ((c != null) ? c.Data : null) != null && !resultList.Contains(c) && !this.currentDestroyCardList.Contains(c) && c.IsFloodgate() && c.IsFaceup() && this.CheckCanBeTargeted(c, canBeTarget, selfType)).ToList<ClientCard>();
			if (problemEnemySpellList.Count > 0)
			{
				resultList.AddRange(this.ShuffleList<ClientCard>(problemEnemySpellList));
			}
			List<ClientCard> dangerList = (from c in base.Enemy.MonsterZone
				where ((c != null) ? c.Data : null) != null && !resultList.Contains(c) && !this.currentDestroyCardList.Contains(c) && c.IsMonsterDangerous() && c.IsFaceup() && this.CheckCanBeTargeted(c, canBeTarget, selfType)
				select c into card
				orderby card.Attack descending
				select card).ToList<ClientCard>();
			if (dangerList.Count > 0 && (base.Duel.Player == 0 || (base.Duel.Phase > DuelPhase.Main1 && base.Duel.Phase < DuelPhase.Main2)))
			{
				resultList.AddRange(dangerList);
			}
			List<ClientCard> invincibleList = (from c in base.Enemy.MonsterZone
				where ((c != null) ? c.Data : null) != null && !resultList.Contains(c) && !this.currentDestroyCardList.Contains(c) && c.IsMonsterInvincible() && c.IsFaceup() && this.CheckCanBeTargeted(c, canBeTarget, selfType)
				select c into card
				orderby card.Attack descending
				select card).ToList<ClientCard>();
			if (invincibleList.Count > 0)
			{
				resultList.AddRange(invincibleList);
			}
			List<ClientCard> enemyMonsters = (from c in base.Enemy.GetMonsters()
				where !this.currentDestroyCardList.Contains(c)
				select c into card
				orderby card.Attack descending
				select card).ToList<ClientCard>();
			if (enemyMonsters.Count > 0)
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
			if (spells.Count > 0 && !ignoreSpells)
			{
				resultList.AddRange(this.ShuffleList<ClientCard>(spells));
			}
			return resultList;
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x000385B8 File Offset: 0x000367B8
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
				where (!ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(card)) && this.enemyPlaceThisTurn.Contains(card)
				select card).ToList<ClientCard>()));
			targetList.AddRange(this.ShuffleList<ClientCard>((from card in base.Enemy.GetSpells()
				where (!ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(card)) && !this.enemyPlaceThisTurn.Contains(card)
				select card).ToList<ClientCard>()));
			targetList.AddRange(this.ShuffleList<ClientCard>((from card in base.Enemy.GetMonsters()
				where card.IsFacedown() && (!ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(card))
				select card).ToList<ClientCard>()));
			return targetList;
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x000386CC File Offset: 0x000368CC
		public List<ClientCard> GetMonsterListForTargetNegate(bool canBeTarget = false, CardType selfType = (CardType)0)
		{
			List<ClientCard> resultList = new List<ClientCard>();
			if (this.CheckWhetherNegated(true, false, (CardType)0))
			{
				return resultList;
			}
			ClientCard target = base.Enemy.MonsterZone.FirstOrDefault((ClientCard card) => ((card != null) ? card.Data : null) != null && card.IsMonsterShouldBeDisabledBeforeItUseEffect() && card.IsFaceup() && !card.IsShouldNotBeTarget() && this.CheckCanBeTargeted(card, canBeTarget, selfType) && !this.currentNegateCardList.Contains(card));
			if (target != null)
			{
				resultList.Add(target);
			}
			foreach (ClientCard chainingCard in base.Duel.CurrentChain)
			{
				if (chainingCard.Location == CardLocation.MonsterZone && chainingCard.Controller == 1 && !chainingCard.IsDisabled() && this.CheckCanBeTargeted(chainingCard, canBeTarget, selfType) && !this.currentNegateCardList.Contains(chainingCard))
				{
					resultList.Add(chainingCard);
				}
			}
			return resultList;
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x000387B8 File Offset: 0x000369B8
		public override BattlePhaseAction OnBattle(IList<ClientCard> attackers, IList<ClientCard> defenders)
		{
			if (attackers.Count<ClientCard>() > 0 && defenders.Count<ClientCard>() > 0)
			{
				List<ClientCard> sortedAttacker = attackers.OrderBy((ClientCard card) => card.Attack).ToList<ClientCard>();
				ClientCard abyssDragon = attackers.FirstOrDefault((ClientCard c) => c.IsCode(3410461) && !c.IsDisabled());
				if (abyssDragon != null)
				{
					sortedAttacker.Remove(abyssDragon);
					sortedAttacker.Insert(0, abyssDragon);
				}
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

		// Token: 0x06000D02 RID: 3330 RVA: 0x0000763C File Offset: 0x0000583C
		public override bool OnSelectHand()
		{
			return true;
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x00038884 File Offset: 0x00036A84
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
						where !card3.HasType(CardType.Token)
						select card).ToList<ClientCard>();
					List<ClientCard> faceDownMonsters = list.Where((ClientCard card) => card3.IsFacedown()).ToList<ClientCard>();
					banishList.AddRange(faceDownMonsters);
					List<ClientCard> dumpMainMonsterList = list.Where((ClientCard card) => !banishList.Contains(card3) && this.CheckRemainInDeck(card3.Id) > 0).ToList<ClientCard>();
					dumpMainMonsterList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					banishList.AddRange(dumpMainMonsterList);
					List<ClientCard> faceUpSpells = (from c in base.Bot.GetSpells()
						where c.IsFaceup()
						select c).ToList<ClientCard>();
					banishList.AddRange(this.ShuffleList<ClientCard>(faceUpSpells));
					List<ClientCard> faceDownSpells = (from c in base.Bot.GetSpells()
						where c.IsFacedown()
						select c).ToList<ClientCard>();
					banishList.AddRange(this.ShuffleList<ClientCard>(faceDownSpells));
					List<ClientCard> uniqueMainMonster = list.Where((ClientCard card) => !banishList.Contains(card3) && !card3.HasType((CardType)75505728) && this.CheckRemainInDeck(card3.Id) == 0).ToList<ClientCard>();
					uniqueMainMonster.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					banishList.AddRange(uniqueMainMonster);
					List<ClientCard> dumpExtraMonsterList = list.Where((ClientCard card) => !banishList.Contains(card3) && card3.HasType((CardType)75505728) && this.Bot.HasInExtra(card3.Id)).ToList<ClientCard>();
					dumpExtraMonsterList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					banishList.AddRange(dumpExtraMonsterList);
					List<ClientCard> uniqueExtraMonsterList = list.Where((ClientCard card) => !banishList.Contains(card3) && card3.HasType((CardType)75505728) && !this.Bot.HasInExtra(card3.Id)).ToList<ClientCard>();
					uniqueExtraMonsterList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					banishList.AddRange(uniqueExtraMonsterList);
					return base.Util.CheckSelectCount(banishList, cards, min, max);
				}
				int num;
				if (hint == 506)
				{
					Dictionary<int, Func<bool>> checkDict = new Dictionary<int, Func<bool>>();
					num = currentSolvingChain.Id;
					if (num <= 18973184)
					{
						if (num == 1984618)
						{
							if (!this.summoned)
							{
								ClientCard quem = cards.FirstOrDefault((ClientCard c) => c.IsCode(45883110));
								if (quem != null)
								{
									return base.Util.CheckSelectCount(new List<ClientCard> { quem }, cards, min, max);
								}
							}
							List<CardLocation> locList = new List<CardLocation>
							{
								CardLocation.Grave,
								CardLocation.Deck
							};
							if (base.Bot.HasInGraveyard(51409648))
							{
								if (cards.Where((ClientCard c) => c.IsOriginalCode(68468459) && c.Location == CardLocation.Grave).Count<ClientCard>() == 1)
								{
									locList = new List<CardLocation>
									{
										CardLocation.Deck,
										CardLocation.Grave
									};
								}
							}
							int[] array = new int[] { 68468459, 45883110 };
							for (int i = 0; i < array.Length; i++)
							{
								int checkId2 = array[i];
								using (List<CardLocation>.Enumerator enumerator = locList.GetEnumerator())
								{
									while (enumerator.MoveNext())
									{
										CardLocation loc2 = enumerator.Current;
										ClientCard target = cards.FirstOrDefault((ClientCard c) => c.IsOriginalCode(checkId2) && c.Location == loc2);
										if (target != null)
										{
											return base.Util.CheckSelectCount(new List<ClientCard> { target }, cards, min, max);
										}
									}
								}
							}
							goto IL_0637;
						}
						if (num == 3410461)
						{
							Dictionary<int, Func<bool>> dictionary = new Dictionary<int, Func<bool>>();
							dictionary.Add(44362883, () => this.BrandedFusionActivateCheck(false));
							dictionary.Add(6498706, () => true);
							checkDict = dictionary;
							goto IL_0637;
						}
						if (num != 18973184)
						{
							goto IL_0637;
						}
					}
					else if (num <= 29948294)
					{
						if (num != 19096726 && num != 29948294)
						{
							goto IL_0637;
						}
					}
					else
					{
						if (num == 45484331 || num - 62962630 <= 1)
						{
							Func<ClientCard, bool> <>9__17;
							checkDict = new Dictionary<int, Func<bool>>
							{
								{
									44362883,
									() => this.BrandedFusionActivateCheck(true)
								},
								{
									18973184,
									() => (this.Duel.Player != 0 || this.Duel.Phase < DuelPhase.End) && ((this.Bot.HasInHandOrInSpellZone(44362883) && this.BrandedFusionActivateCheck(true)) || (this.Bot.HasInHandOrInSpellZone(34995106) && this.BrandedInWhiteActivateCheck()) || (this.Bot.HasInHandOrInSpellZone(82738008) && this.BrandedInRedActivateCheck(false) != null) || (!this.summoned && this.Bot.HasInHand(68468459) && this.CheckAlbazFusion(null)) || (this.Bot.HasInMonstersZone(95515789, false, false, false) || (!this.summoned && this.Bot.HasInHand(95515789))))
								},
								{
									29948294,
									new Func<bool>(this.BrandedInHighSpiritsActivateCheck)
								},
								{
									82738008,
									() => (this.Duel.Phase == DuelPhase.End && this.nadirActivated) || this.BrandedInRedActivateCheck(false) != null
								},
								{
									34995106,
									new Func<bool>(this.BrandedInWhiteActivateCheck)
								},
								{
									17751597,
									() => cards.Any((ClientCard c) => c.IsCode(17751597) && c.Location == CardLocation.Removed)
								},
								{
									19271881,
									delegate
									{
										IEnumerable<ClientCard> monsters = this.Bot.GetMonsters();
										Func<ClientCard, bool> func2;
										if ((func2 = <>9__17) == null)
										{
											func2 = (<>9__17 = (ClientCard c) => c.IsFaceup() && c.IsCode(this.albazFusionMonster));
										}
										return monsters.Any(func2);
									}
								},
								{
									36637374,
									() => this.Bot.Hand.Count > 2
								}
							};
							goto IL_0637;
						}
						goto IL_0637;
					}
					Func<ClientCard, bool> <>9__26;
					checkDict = new Dictionary<int, Func<bool>>
					{
						{
							19096726,
							delegate
							{
								IEnumerable<ClientCard> monsters2 = this.Bot.GetMonsters();
								Func<ClientCard, bool> func3;
								if ((func3 = <>9__26) == null)
								{
									func3 = (<>9__26 = (ClientCard c) => c.IsFaceup() && c.IsCode(this.albazFusionMonster));
								}
								return monsters2.Any(func3) || (this.Bot.HasInMonstersZone(95515789, false, false, false) && this.Bot.HasInHandOrHasInMonstersZone(68468459));
							}
						},
						{
							45484331,
							() => this.CheckWhetherCanSummon() && !this.activatedCardIdList.Contains(45484332)
						},
						{
							68468459,
							() => (this.CheckWhetherCanSummon() && this.CheckAlbazFusion(null)) || this.Bot.HasInMonstersZone(95515789, false, false, false)
						},
						{
							45883110,
							() => this.CheckWhetherCanSummon()
						},
						{
							95515789,
							() => this.CheckWhetherCanSummon() || (!this.CheckShouldNoMoreSpSummon() && this.Bot.HasInMonstersZoneOrInGraveyard(68468459))
						},
						{
							25451383,
							() => !this.CheckWhetherWillbeRemoved() && !this.activatedCardIdList.Contains(25451383)
						}
					};
					IL_0637:
					using (Dictionary<int, Func<bool>>.Enumerator enumerator2 = checkDict.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							KeyValuePair<int, Func<bool>> pair2 = enumerator2.Current;
							ClientCard target2 = cards.FirstOrDefault((ClientCard card) => card3.IsCode(pair2.Key));
							if (target2 != null && pair2.Value())
							{
								return base.Util.CheckSelectCount(new List<ClientCard> { target2 }, cards, min, max);
							}
						}
					}
				}
				num = currentSolvingChain.Id;
				if (num <= 41373230)
				{
					if (num <= 19271881)
					{
						if (num <= 6498706)
						{
							if (num == 1906812)
							{
								goto IL_36BC;
							}
							if (num != 1984618)
							{
								if (num != 6498706)
								{
									goto IL_4613;
								}
								int summonId = this.FusionDeploymentSpSummonTarget();
								if (summonId <= 0)
								{
									goto IL_4613;
								}
								if (hint == 526)
								{
									if (summonId == 95515789)
									{
										ClientCard target3 = cards.FirstOrDefault((ClientCard card) => card3.IsCode(24915933));
										if (target3 != null)
										{
											return base.Util.CheckSelectCount(new List<ClientCard> { target3 }, cards, min, max);
										}
									}
									else if (summonId == 68468459)
									{
										foreach (ClientCard target4 in this.ShuffleList<ClientCard>(new List<ClientCard>(cards)))
										{
											if (target4.IsCode(this.albazFusionMonster))
											{
												return base.Util.CheckSelectCount(new List<ClientCard> { target4 }, cards, min, max);
											}
										}
									}
								}
								if (hint != 509)
								{
									goto IL_4613;
								}
								using (IEnumerator<ClientCard> enumerator4 = cards.GetEnumerator())
								{
									while (enumerator4.MoveNext())
									{
										ClientCard target5 = enumerator4.Current;
										if (target5.IsCode(summonId))
										{
											return base.Util.CheckSelectCount(new List<ClientCard> { target5 }, cards, min, max);
										}
									}
									goto IL_4613;
								}
							}
							else
							{
								if (hint != 504)
								{
									goto IL_4613;
								}
								if (this.summoned)
								{
									if (this.CheckRemainInDeck(95515789) > 0)
									{
										ClientCard lulu = cards.FirstOrDefault((ClientCard c) => c.IsOriginalCode(53971455));
										if (lulu != null)
										{
											return base.Util.CheckSelectCount(new List<ClientCard> { lulu }, cards, min, max);
										}
									}
									if (!base.Bot.MonsterZone.Any((ClientCard c) => c != null && c.HasType(CardType.Fusion)) && this.CheckRemainInDeck(45484331) > 0)
									{
										ClientCard ironDragon = cards.FirstOrDefault((ClientCard c) => c.IsOriginalCode(1906812));
										if (ironDragon != null)
										{
											return base.Util.CheckSelectCount(new List<ClientCard> { ironDragon }, cards, min, max);
										}
									}
								}
								ClientCard target6;
								this.NadirServantActivateCheck(cards, true, out target6);
								if (target6 != null)
								{
									return base.Util.CheckSelectCount(new List<ClientCard> { target6 }, cards, min, max);
								}
								goto IL_4613;
							}
						}
						else
						{
							if (num == 11321089)
							{
								List<ClientCard> targetList = new List<ClientCard>();
								targetList.AddRange(this.GetProblematicEnemyCardList(false, false, CardType.Monster));
								int bestBotPower = base.Util.GetBestPower(base.Bot, false);
								targetList.AddRange(from c in base.Enemy.MonsterZone
									where c != null && !targetList.Contains(c) && c.GetDefensePower() >= bestBotPower
									orderby c.GetDefensePower() descending
									select c);
								targetList.AddRange(this.ShuffleList<ClientCard>(this.enemyPlaceThisTurn));
								return base.Util.CheckSelectCount(targetList, cards, min, max);
							}
							if (num != 17751597)
							{
								if (num != 19271881)
								{
									goto IL_4613;
								}
								if (hint != 514)
								{
									goto IL_4613;
								}
								List<int> targetIdList = new List<int> { 44146295, 38811586, 51409648 };
								List<Func<ClientCard, bool>> list2 = new List<Func<ClientCard, bool>>();
								list2.Add((ClientCard c) => this.Duel.CurrentChain.Contains(c));
								list2.Add((ClientCard c) => true);
								using (List<Func<ClientCard, bool>>.Enumerator enumerator5 = list2.GetEnumerator())
								{
									Func<ClientCard, bool> <>9__108;
									while (enumerator5.MoveNext())
									{
										Func<ClientCard, bool> func6 = enumerator5.Current;
										List<ClientCard> chainedList = cards.Where((ClientCard c) => func6(c)).ToList<ClientCard>();
										if (chainedList.Count > 0)
										{
											using (List<int>.Enumerator enumerator6 = targetIdList.GetEnumerator())
											{
												while (enumerator6.MoveNext())
												{
													int checkId3 = enumerator6.Current;
													ClientCard target7 = chainedList.FirstOrDefault((ClientCard c) => c.IsOriginalCode(checkId3));
													if (target7 != null)
													{
														return base.Util.CheckSelectCount(new List<ClientCard> { target7 }, cards, min, max);
													}
												}
											}
											IEnumerable<ClientCard> enumerable = chainedList;
											Func<ClientCard, bool> func4;
											if ((func4 = <>9__108) == null)
											{
												func4 = (<>9__108 = (ClientCard c) => this.Duel.CurrentChain.Contains(c));
											}
											ClientCard otherChainTarget = enumerable.FirstOrDefault(func4);
											if (otherChainTarget != null)
											{
												return base.Util.CheckSelectCount(new List<ClientCard> { otherChainTarget }, cards, min, max);
											}
										}
									}
									goto IL_4613;
								}
							}
							ClientCard searing = cards.FirstOrDefault((ClientCard c) => c.IsCode(70534340));
							if (searing != null)
							{
								this.selectedFusionMaterial.Add(searing);
								return base.Util.CheckSelectCount(new List<ClientCard> { searing }, cards, min, max);
							}
							using (List<int>.Enumerator enumerator6 = new List<int> { 87746184, 44146295, 41373230, 3410461, 38811586, 1906812 }.GetEnumerator())
							{
								while (enumerator6.MoveNext())
								{
									int checkId4 = enumerator6.Current;
									List<ClientCard> gravePriorityList = cards.Where((ClientCard c) => c != null && c.IsCode(checkId4) && c.Location == CardLocation.Grave && !this.CheckWhetherShouldKeepInGrave(c)).ToList<ClientCard>();
									if (gravePriorityList.Count > 0)
									{
										this.selectedFusionMaterial.Add(gravePriorityList[0]);
										return base.Util.CheckSelectCount(new List<ClientCard> { gravePriorityList[0] }, cards, min, max);
									}
								}
							}
							List<ClientCard> graveList = cards.Where((ClientCard c) => c != null && c.Location == CardLocation.Grave).ToList<ClientCard>();
							if (graveList.Count > 0)
							{
								this.selectedFusionMaterial.Add(graveList[0]);
								return base.Util.CheckSelectCount(new List<ClientCard> { graveList[0] }, cards, min, max);
							}
							ClientCard monsterOnField = (from c in cards
								where c != null && c.Location == CardLocation.MonsterZone
								orderby c.GetDefensePower()
								select c).FirstOrDefault<ClientCard>();
							if (monsterOnField != null)
							{
								this.selectedFusionMaterial.Add(monsterOnField);
								return base.Util.CheckSelectCount(new List<ClientCard> { monsterOnField }, cards, min, max);
							}
							goto IL_4613;
						}
					}
					else
					{
						if (num <= 32731036)
						{
							if (num != 24915933)
							{
								if (num != 29948294)
								{
									if (num != 32731036)
									{
										goto IL_4613;
									}
									Dictionary<int, Func<bool>> lubellionCheckDict = new Dictionary<int, Func<bool>>();
									if (hint == 527)
									{
										lubellionCheckDict.Add(18973184, delegate
										{
											bool fusionFlag = this.Bot.HasInHandOrHasInMonstersZone(95515789);
											if (!this.activatedCardIdList.Contains(44362883) && (this.Bot.HasInHand(44362883) || (!this.summoned && this.CheckRemainInDeck(44362883) > 0 && this.Bot.HasInHand(new int[] { 62962630, 45484331 }))))
											{
												fusionFlag = true;
											}
											fusionFlag |= !this.summoned && this.Bot.HasInHand(68468459) && this.CheckAlbazFusion(null);
											fusionFlag |= this.Bot.HasInHandOrInSpellZone(34995106) && this.BrandedInWhiteActivateCheck();
											return fusionFlag | (this.Bot.HasInHandOrInSpellZone(82738008) && this.BrandedInRedActivateCheck(false) != null);
										});
										lubellionCheckDict.Add(32756828, () => true);
									}
									else if (hint == 506)
									{
										lubellionCheckDict.Add(60242223, () => true);
									}
									using (Dictionary<int, Func<bool>>.Enumerator enumerator2 = lubellionCheckDict.GetEnumerator())
									{
										while (enumerator2.MoveNext())
										{
											KeyValuePair<int, Func<bool>> pair3 = enumerator2.Current;
											ClientCard target8 = cards.FirstOrDefault((ClientCard c) => c.Id == pair3.Key);
											if (target8 != null && pair3.Value())
											{
												this.SelectSTPlace(target8, false, null);
												return base.Util.CheckSelectCount(new List<ClientCard> { target8 }, cards, min, max);
											}
										}
										goto IL_4613;
									}
									goto IL_09BB;
								}
								else
								{
									if (hint == 526)
									{
										if (base.Duel.Phase == DuelPhase.End && base.Bot.HasInMonstersZone(45883110, false, false, false))
										{
											ClientCard cartesia = cards.FirstOrDefault((ClientCard c) => c.IsOriginalCode(95515789));
											if (cartesia != null)
											{
												this.fusionTarget = cartesia;
												return base.Util.CheckSelectCount(new List<ClientCard> { cartesia }, cards, min, max);
											}
										}
										if (base.Duel.CurrentChain.Any((ClientCard c) => c.IsOriginalCode(25451383) && c.Location == CardLocation.Hand))
										{
											ClientCard shrouded = cards.FirstOrDefault((ClientCard c) => c.IsOriginalCode(25451383));
											if (shrouded != null)
											{
												this.fusionTarget = shrouded;
												return base.Util.CheckSelectCount(new List<ClientCard> { shrouded }, cards, min, max);
											}
										}
										using (List<int>.Enumerator enumerator6 = new List<int> { 60242223, 25451383, 32731036, 95515789, 68468459, 19096726 }.GetEnumerator())
										{
											while (enumerator6.MoveNext())
											{
												int discardId2 = enumerator6.Current;
												ClientCard target9 = cards.FirstOrDefault((ClientCard c) => c.IsOriginalCode(discardId2));
												if (target9 != null && (discardId2 != 32731036 || base.Duel.Player != 0 || (base.Duel.Phase > DuelPhase.Main1 && base.Duel.Phase != DuelPhase.Main2) || this.CheckRemainInDeck(60242223) <= 0 || this.activatedCardIdList.Contains(32731036)))
												{
													this.fusionTarget = target9;
													return base.Util.CheckSelectCount(new List<ClientCard> { target9 }, cards, min, max);
												}
											}
										}
									}
									if (hint != 504)
									{
										goto IL_4613;
									}
									using (List<int>.Enumerator enumerator6 = new List<int> { 87746184, 41373230, 51409648, 3410461, 24915933 }.GetEnumerator())
									{
										while (enumerator6.MoveNext())
										{
											int discardId = enumerator6.Current;
											if (!this.sendToGYThisTurn.Any((ClientCard c) => c.IsOriginalCode(discardId)))
											{
												ClientCard target10 = cards.FirstOrDefault((ClientCard c) => c.IsOriginalCode(discardId));
												if (target10 != null)
												{
													return base.Util.CheckSelectCount(new List<ClientCard> { target10 }, cards, min, max);
												}
											}
										}
										goto IL_4613;
									}
								}
							}
							else
							{
								if (hint == 504)
								{
									ClientCard target11;
									this.GranguignolTheDuskDragonSendToGYTarget(cards, out target11);
									if (target11 != null)
									{
										return base.Util.CheckSelectCount(new List<ClientCard> { target11 }, cards, min, max);
									}
								}
								if (hint == 509)
								{
									Func<ClientCard, bool> <>9__162;
									using (Dictionary<int, Func<bool>>.Enumerator enumerator2 = new Dictionary<int, Func<bool>>
									{
										{
											72272462,
											delegate
											{
												IEnumerable<ClientCard> monsterZone = this.Enemy.MonsterZone;
												Func<ClientCard, bool> func5;
												if ((func5 = <>9__162) == null)
												{
													func5 = (<>9__162 = (ClientCard c) => c != null && c.IsFaceup() && c.Attack >= this.Util.GetBestPower(this.Bot, false) && (!c.HasType(CardType.Fusion) || c.Level < 8));
												}
												return monsterZone.Any(func5);
											}
										},
										{
											45883110,
											() => this.Bot.HasInMonstersZone(44146295, false, false, false) || this.Util.GetOneEnemyBetterThanValue(1500, false, false) == null
										},
										{
											53971455,
											() => this.Duel.Player != 0 || !this.Bot.HasInHandOrInSpellZone(44362883)
										}
									}.GetEnumerator())
									{
										while (enumerator2.MoveNext())
										{
											KeyValuePair<int, Func<bool>> pair4 = enumerator2.Current;
											ClientCard target12 = cards.FirstOrDefault((ClientCard c) => c.IsOriginalCode(pair4.Key));
											if (target12 != null && pair4.Value())
											{
												return base.Util.CheckSelectCount(new List<ClientCard> { target12 }, cards, min, max);
											}
										}
										goto IL_4613;
									}
									goto IL_35AE;
								}
								goto IL_4613;
							}
						}
						else if (num <= 36637374)
						{
							if (num == 34995106)
							{
								goto IL_12BA;
							}
							if (num != 36637374)
							{
								goto IL_4613;
							}
						}
						else if (num != 38811586)
						{
							if (num != 41373230)
							{
								goto IL_4613;
							}
							goto IL_3789;
						}
						else
						{
							if (hint != base.Util.GetStringId(38811586, 1))
							{
								goto IL_4613;
							}
							ClientCard albaz = cards.FirstOrDefault((ClientCard c) => c.IsOriginalCode(68468459));
							if (albaz != null && this.CheckAlbazFusion(null))
							{
								return base.Util.CheckSelectCount(new List<ClientCard> { albaz }, cards, min, max);
							}
							ClientCard floogate = (from c in cards
								where c.IsFloodgate()
								orderby c.GetDefensePower() descending
								select c).FirstOrDefault<ClientCard>();
							if (floogate != null)
							{
								return base.Util.CheckSelectCount(new List<ClientCard> { floogate }, cards, min, max);
							}
							return base.Util.CheckSelectCount(cards.OrderByDescending((ClientCard c) => c.GetDefensePower()).ToList<ClientCard>(), cards, min, max);
						}
						if (hint == 574)
						{
							Dictionary<int, Func<bool>> dictionary2 = new Dictionary<int, Func<bool>>();
							dictionary2.Add(62962630, () => !this.activatedCardIdList.Contains(62962630) && !this.DefaultCheckWhetherCardIdIsNegated(62962630) && (!this.CheckWhetherCanSummon() || !this.Bot.HasInHand(62962630)));
							dictionary2.Add(45883110, () => true);
							dictionary2.Add(36577931, () => true);
							using (Dictionary<int, Func<bool>>.Enumerator enumerator2 = dictionary2.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									KeyValuePair<int, Func<bool>> pair5 = enumerator2.Current;
									ClientCard target13 = cards.FirstOrDefault((ClientCard card) => card3.IsCode(pair5.Key));
									if (target13 != null && pair5.Value())
									{
										return base.Util.CheckSelectCount(new List<ClientCard> { target13 }, cards, min, max);
									}
								}
								goto IL_4613;
							}
							goto IL_2345;
						}
						goto IL_4613;
					}
					IL_12BA:
					if (hint == 509)
					{
						ClientCard target14;
						this.BrandedInWhiteFusionTarget(cards, out target14);
						if (target14 != null)
						{
							this.fusionTarget = target14;
							return base.Util.CheckSelectCount(new List<ClientCard> { target14 }, cards, min, max);
						}
					}
					if (hint != 511 || this.fusionTarget == null)
					{
						goto IL_4613;
					}
					if (this.fusionTarget.IsCode(92892239))
					{
						CardLocation[] array2 = new CardLocation[]
						{
							CardLocation.Grave,
							CardLocation.Hand,
							CardLocation.MonsterZone
						};
						for (int i = 0; i < array2.Length; i++)
						{
							CardLocation loc3 = array2[i];
							List<ClientCard> list3 = (from c in cards
								where c.Location == loc3
								orderby c.GetDefensePower()
								select c).ToList<ClientCard>();
							int banishedAlbazCount = base.Bot.Banished.Where((ClientCard c) => c.IsOriginalCode(68468459)).Count<ClientCard>();
							banishedAlbazCount += this.selectedFusionMaterial.Where((ClientCard c) => c.IsOriginalCode(68468459)).Count<ClientCard>();
							foreach (ClientCard target15 in list3)
							{
								if (!target15.IsOriginalCode(68468459) || banishedAlbazCount <= 0)
								{
									this.selectedFusionMaterial.Add(target15);
									return base.Util.CheckSelectCount(new List<ClientCard> { target15 }, cards, min, max);
								}
							}
						}
					}
					if (this.fusionTarget.IsCode(72272462))
					{
						if (this.selectedFusionMaterial.Count == 0)
						{
							CardLocation[] array2 = new CardLocation[]
							{
								CardLocation.Grave,
								CardLocation.Hand,
								CardLocation.MonsterZone
							};
							for (int i = 0; i < array2.Length; i++)
							{
								CardLocation loc4 = array2[i];
								List<ClientCard> cardsInLoc = (from c in cards
									where c.Location == loc4 && c.HasSetcode(356) && (loc4 != CardLocation.Grave || !this.CheckWhetherShouldKeepInGrave(c))
									orderby c.GetDefensePower()
									select c).ToList<ClientCard>();
								if (cardsInLoc.Count > 0)
								{
									this.selectedFusionMaterial.Add(cardsInLoc[0]);
									return base.Util.CheckSelectCount(cardsInLoc, cards, min, max);
								}
							}
						}
						else
						{
							CardLocation[] array2 = new CardLocation[]
							{
								CardLocation.Grave,
								CardLocation.Hand,
								CardLocation.MonsterZone
							};
							for (int i = 0; i < array2.Length; i++)
							{
								CardLocation loc5 = array2[i];
								List<ClientCard> cardsInLoc2 = (from c in cards
									where c.Location == loc5 && c.HasAttribute((CardAttribute)48) && (loc5 != CardLocation.Grave || !this.CheckWhetherShouldKeepInGrave(c))
									orderby c.GetDefensePower()
									select c).ToList<ClientCard>();
								if (cardsInLoc2.Count > 0)
								{
									if (!this.activatedCardIdList.Contains(19096727))
									{
										ClientCard mercourier = cardsInLoc2.FirstOrDefault((ClientCard c) => c.IsCode(19096726));
										if (mercourier != null)
										{
											this.selectedFusionMaterial.Add(mercourier);
											return base.Util.CheckSelectCount(new List<ClientCard> { mercourier }, cards, min, max);
										}
									}
									if (!this.activatedCardIdList.Contains(36577931) && this.CheckRemainInDeck(new int[] { 62962630, 45883110 }) > 0)
									{
										ClientCard tragedy = cardsInLoc2.FirstOrDefault((ClientCard c) => c.IsCode(36577931));
										if (tragedy != null)
										{
											this.selectedFusionMaterial.Add(tragedy);
											return base.Util.CheckSelectCount(new List<ClientCard> { tragedy }, cards, min, max);
										}
									}
									this.selectedFusionMaterial.Add(cardsInLoc2[0]);
									return base.Util.CheckSelectCount(cardsInLoc2, cards, min, max);
								}
							}
						}
					}
					if (this.fusionTarget.IsCode(11321089))
					{
						List<ClientCard> goalMaterialList = this.ChimeraFusionMaterialList(true).Intersect(cards).ToList<ClientCard>();
						if (goalMaterialList.Count > 0)
						{
							return base.Util.CheckSelectCount(goalMaterialList, cards, min, max);
						}
					}
					if (!this.fusionTarget.IsCode(this.albazFusionMonster))
					{
						goto IL_4613;
					}
					if (this.selectedFusionMaterial.Count == 0)
					{
						foreach (CardLocation cardLocation in new CardLocation[]
						{
							CardLocation.Grave,
							CardLocation.MonsterZone,
							CardLocation.Hand
						})
						{
							ClientCard albaz2 = (from c in cards
								where c.IsCode(68468459)
								orderby c.GetDefensePower()
								select c).FirstOrDefault<ClientCard>();
							if (albaz2 != null)
							{
								this.selectedFusionMaterial.Add(albaz2);
								return base.Util.CheckSelectCount(new List<ClientCard> { albaz2 }, cards, min, max);
							}
						}
						goto IL_4613;
					}
					if (this.fusionTarget.IsOriginalCode(3410461) && cancelable)
					{
						return null;
					}
					if (base.Util.IsTurn1OrMain2() && !this.CheckWhetherWillbeRemoved())
					{
						ClientCard duskDragon = cards.FirstOrDefault((ClientCard c) => c.IsCode(24915933) && c.Location == CardLocation.MonsterZone);
						if (duskDragon != null)
						{
							this.selectedFusionMaterial.Add(duskDragon);
							return base.Util.CheckSelectCount(new List<ClientCard> { duskDragon }, cards, min, max);
						}
					}
					List<Func<ClientCard, bool>> list4 = new List<Func<ClientCard, bool>>();
					list4.Add((ClientCard c) => c.Location == CardLocation.Grave && !this.CheckWhetherShouldKeepInGrave(c));
					list4.Add((ClientCard c) => c.Location == CardLocation.MonsterZone && c.GetDefensePower() <= 2000);
					list4.Add((ClientCard c) => c.Location == CardLocation.Hand);
					list4.Add((ClientCard c) => c.Location == CardLocation.Grave);
					list4.Add((ClientCard c) => c.Location == CardLocation.MonsterZone);
					using (List<Func<ClientCard, bool>>.Enumerator enumerator5 = list4.GetEnumerator())
					{
						while (enumerator5.MoveNext())
						{
							Func<ClientCard, bool> func8 = enumerator5.Current;
							List<ClientCard> targetList4 = (from c in cards
								where func8(c)
								orderby c.GetDefensePower()
								select c).ToList<ClientCard>();
							if (targetList4.Count > 0)
							{
								this.selectedFusionMaterial.Add(targetList4[0]);
								return base.Util.CheckSelectCount(new List<ClientCard> { targetList4[0] }, cards, min, max);
							}
						}
						goto IL_4613;
					}
					goto IL_1A7A;
				}
				else
				{
					if (num <= 68468460)
					{
						if (num <= 45883110)
						{
							if (num != 44146295)
							{
								if (num == 44362883)
								{
									goto IL_1A7A;
								}
								if (num != 45883110)
								{
									goto IL_4613;
								}
								Dictionary<int, Func<bool>> dictionary3 = new Dictionary<int, Func<bool>>();
								dictionary3.Add(95515789, () => this.sendToGYThisTurn.Any((ClientCard c) => c.IsCode(87746184)) && this.CheckRemainInDeck(29948294) > 0);
								dictionary3.Add(44362883, () => this.Bot.HasInGraveyard(17751597));
								dictionary3.Add(68468459, () => !this.Bot.HasInGraveyard(68468459));
								dictionary3.Add(19096726, () => this.Bot.HasInHandOrInSpellZone(34995106));
								dictionary3.Add(17751597, () => true);
								Func<ClientCard, bool> <>9__45;
								dictionary3.Add(19271881, delegate
								{
									IEnumerable<ClientCard> monsterZone2 = this.Bot.MonsterZone;
									Func<ClientCard, bool> func7;
									if ((func7 = <>9__45) == null)
									{
										func7 = (<>9__45 = (ClientCard c) => c != null && c.IsFaceup() && c.IsCode(this.albazFusionMonster) && this.fusionToGYFlag);
									}
									return !monsterZone2.Any(func7);
								});
								dictionary3.Add(29948294, () => this.fusionToGYFlag);
								dictionary3.Add(25451383, () => true);
								using (Dictionary<int, Func<bool>>.Enumerator enumerator2 = dictionary3.GetEnumerator())
								{
									while (enumerator2.MoveNext())
									{
										KeyValuePair<int, Func<bool>> pair6 = enumerator2.Current;
										ClientCard target16 = cards.FirstOrDefault((ClientCard card) => card3.IsCode(pair6.Key));
										if (target16 != null && pair6.Value())
										{
											return base.Util.CheckSelectCount(new List<ClientCard> { target16 }, cards, min, max);
										}
									}
									goto IL_4613;
								}
							}
							else
							{
								List<ClientCard> floodgateList = this.ShuffleList<ClientCard>(cards.Where((ClientCard c) => c.Controller == 1 && c.IsFloodgate()).ToList<ClientCard>());
								if (floodgateList.Count > 0)
								{
									return base.Util.CheckSelectCount(floodgateList, cards, min, max);
								}
								List<ClientCard> extraMonsterList = (from c in cards
									where c.Controller == 1 && (c.HasType((CardType)8396992) || (c.HasType(CardType.Link) && c.LinkCount >= 2))
									orderby c.GetDefensePower() descending
									select c).ToList<ClientCard>();
								if (extraMonsterList.Count > 0)
								{
									return base.Util.CheckSelectCount(extraMonsterList, cards, min, max);
								}
								ClientCard worstBotMonster = base.Util.GetWorstBotMonster(false);
								int worstBotPower2 = ((worstBotMonster == null) ? 0 : worstBotMonster.GetDefensePower());
								List<ClientCard> betterMonsterList = (from c in cards
									where c.Controller == 1 && c.GetDefensePower() >= worstBotPower2
									orderby c.GetDefensePower() descending
									select c).ToList<ClientCard>();
								if (betterMonsterList.Count > 0)
								{
									return base.Util.CheckSelectCount(betterMonsterList, cards, min, max);
								}
								List<ClientCard> dangerMonsterList = (from c in cards
									where c.Controller == 1 && (c.IsMonsterDangerous() || c.IsMonsterInvincible())
									orderby c.GetDefensePower() descending
									select c).ToList<ClientCard>();
								if (dangerMonsterList.Count > 0)
								{
									return base.Util.CheckSelectCount(dangerMonsterList, cards, min, max);
								}
								List<ClientCard> allEnemyMonsterList = cards.Where((ClientCard c) => c.Controller == 1).OrderByDescending(delegate(ClientCard c)
								{
									if (!c.IsFacedown())
									{
										return c.GetDefensePower();
									}
									return 0;
								}).ToList<ClientCard>();
								if (allEnemyMonsterList.Count > 0)
								{
									return base.Util.CheckSelectCount(allEnemyMonsterList, cards, min, max);
								}
								ClientCard botMonsterWithEffect = cards.FirstOrDefault((ClientCard c) => c.Controller == 0 && c.IsCode(new int[] { 36577931, 19096726 }));
								if (botMonsterWithEffect != null)
								{
									return base.Util.CheckSelectCount(new List<ClientCard> { botMonsterWithEffect }, cards, min, max);
								}
								ClientCard botLubellion = cards.FirstOrDefault((ClientCard c) => c.Controller == 0 && c.IsCode(32731036));
								if (botLubellion != null)
								{
									return base.Util.CheckSelectCount(new List<ClientCard> { botLubellion }, cards, min, max);
								}
								List<ClientCard> allBotMonster = cards.Where((ClientCard c) => c.Controller == 0).OrderBy(delegate(ClientCard c)
								{
									if (!c.IsFacedown())
									{
										return c.GetDefensePower();
									}
									return 0;
								}).ToList<ClientCard>();
								if (allBotMonster.Count > 0)
								{
									return base.Util.CheckSelectCount(allBotMonster, cards, min, max);
								}
								goto IL_4613;
							}
						}
						else
						{
							if (num == 51409648)
							{
								goto IL_3856;
							}
							if (num == 53971455)
							{
								goto IL_444F;
							}
							if (num - 68468459 > 1)
							{
								goto IL_4613;
							}
							goto IL_09BB;
						}
					}
					else if (num <= 75500286)
					{
						if (num != 70534340)
						{
							if (num == 72272462)
							{
								goto IL_35AE;
							}
							if (num != 75500286)
							{
								goto IL_4613;
							}
							goto IL_1EA3;
						}
						else
						{
							if (hint == 509)
							{
								ClientCard target17;
								this.LubellionTheSearingDragonFusionTarget(cards, out target17);
								if (target17 != null)
								{
									this.fusionTarget = target17;
									return base.Util.CheckSelectCount(new List<ClientCard> { target17 }, cards, min, max);
								}
							}
							if (hint != 511 || this.fusionTarget == null)
							{
								goto IL_4613;
							}
							if (this.fusionTarget.IsCode(92892239))
							{
								List<Func<ClientCard, bool>> list5 = new List<Func<ClientCard, bool>>();
								list5.Add((ClientCard c) => c.IsFaceup() && c.Location == CardLocation.Removed && c.IsCode(87746184));
								list5.Add((ClientCard c) => c.IsFaceup() && c.Location == CardLocation.Removed && c.IsCode(68468459));
								list5.Add((ClientCard c) => c.Location == CardLocation.Grave && c.IsCode(87746184) && !this.CheckWhetherShouldKeepInGrave(c));
								list5.Add((ClientCard c) => c.IsFaceup() && c.Location == CardLocation.Removed && c.IsCode(41373230));
								list5.Add((ClientCard c) => c.IsFaceup() && c.Location == CardLocation.Removed);
								list5.Add((ClientCard c) => c.Location == CardLocation.Grave && !this.CheckWhetherShouldKeepInGrave(c));
								list5.Add((ClientCard c) => c.Location == CardLocation.Grave);
								list5.Add((ClientCard c) => c.Location == CardLocation.Hand || c.Location == CardLocation.MonsterZone);
								using (List<Func<ClientCard, bool>>.Enumerator enumerator5 = list5.GetEnumerator())
								{
									while (enumerator5.MoveNext())
									{
										Func<ClientCard, bool> func9 = enumerator5.Current;
										List<ClientCard> cardWithFunc = (from c in cards
											where func9(c)
											orderby c.GetDefensePower()
											select c).ToList<ClientCard>();
										if (cardWithFunc.Count > 0)
										{
											this.selectedFusionMaterial.Add(cardWithFunc[0]);
											return base.Util.CheckSelectCount(new List<ClientCard> { cardWithFunc[0] }, cards, min, max);
										}
									}
								}
							}
							List<Func<ClientCard, bool>> list6 = new List<Func<ClientCard, bool>>();
							list6.Add((ClientCard c) => c.IsFaceup() && c.Location == CardLocation.Removed);
							list6.Add((ClientCard c) => c.Location == CardLocation.Grave && !this.CheckWhetherShouldKeepInGrave(c));
							list6.Add((ClientCard c) => c.IsCode(70534340));
							list6.Add((ClientCard c) => c.Location == CardLocation.Grave);
							list6.Add((ClientCard c) => c.Location == CardLocation.MonsterZone);
							list6.Add((ClientCard c) => c.Location == CardLocation.Hand);
							List<Func<ClientCard, bool>> funcList = list6;
							if (this.selectedFusionMaterial.Count == 0)
							{
								if (this.fusionTarget.IsOriginalCode(72272462))
								{
									using (List<Func<ClientCard, bool>>.Enumerator enumerator5 = funcList.GetEnumerator())
									{
										while (enumerator5.MoveNext())
										{
											Func<ClientCard, bool> func10 = enumerator5.Current;
											List<ClientCard> cardsWithFunc = (from c in cards
												where func10(c) && c.HasSetcode(356)
												orderby c.GetDefensePower()
												select c).ToList<ClientCard>();
											if (cardsWithFunc.Count > 0)
											{
												this.selectedFusionMaterial.Add(cardsWithFunc[0]);
												return base.Util.CheckSelectCount(cardsWithFunc, cards, min, max);
											}
										}
									}
								}
								if (this.fusionTarget.IsCode(this.albazFusionMonster))
								{
									using (List<Func<ClientCard, bool>>.Enumerator enumerator5 = funcList.GetEnumerator())
									{
										while (enumerator5.MoveNext())
										{
											Func<ClientCard, bool> func11 = enumerator5.Current;
											List<ClientCard> cardsWithFunc2 = (from c in cards
												where func11(c) && c.IsCode(68468459)
												orderby c.GetDefensePower()
												select c).ToList<ClientCard>();
											if (cardsWithFunc2.Count > 0)
											{
												this.selectedFusionMaterial.Add(cardsWithFunc2[0]);
												return base.Util.CheckSelectCount(cardsWithFunc2, cards, min, max);
											}
										}
									}
								}
							}
							if (this.fusionTarget.IsCode(3410461) && this.selectedFusionMaterial.Count > 0)
							{
								List<Func<ClientCard, bool>> list7 = new List<Func<ClientCard, bool>>();
								list7.Add((ClientCard c) => c.IsFaceup() && c.Location == CardLocation.Removed);
								list7.Add((ClientCard c) => c.Location == CardLocation.Grave && !this.CheckWhetherShouldKeepInGrave(c));
								list7.Add((ClientCard c) => c.IsCode(70534340));
								funcList = list7;
							}
							using (List<Func<ClientCard, bool>>.Enumerator enumerator5 = funcList.GetEnumerator())
							{
								while (enumerator5.MoveNext())
								{
									Func<ClientCard, bool> func12 = enumerator5.Current;
									List<ClientCard> cardsWithFunc3 = (from c in cards
										where func12(c)
										orderby c.GetDefensePower()
										select c).ToList<ClientCard>();
									if (cardsWithFunc3.Count > 0)
									{
										this.selectedFusionMaterial.Add(cardsWithFunc3[0]);
										return base.Util.CheckSelectCount(cardsWithFunc3, cards, min, max);
									}
								}
							}
							if (this.fusionTarget.IsOriginalCode(3410461) && cancelable)
							{
								return null;
							}
							goto IL_4613;
						}
					}
					else if (num <= 82738008)
					{
						if (num - 81439173 > 1)
						{
							if (num != 82738008)
							{
								goto IL_4613;
							}
							goto IL_2345;
						}
						else
						{
							ClientCard burialTarget;
							this.FoolishBurialTarget(cards, out burialTarget);
							if (burialTarget != null)
							{
								return base.Util.CheckSelectCount(new List<ClientCard> { burialTarget }, cards, min, max);
							}
							goto IL_4613;
						}
					}
					else if (num != 87746184)
					{
						if (num - 95515789 > 1)
						{
							goto IL_4613;
						}
					}
					else
					{
						if (hint == 509)
						{
							ClientCard target18;
							this.AlbionTheBrandedDragonFusionTarget(cards, out target18);
							if (target18 != null)
							{
								this.fusionTarget = target18;
								return base.Util.CheckSelectCount(new List<ClientCard> { target18 }, cards, min, max);
							}
						}
						if (hint == 511 && this.fusionTarget != null)
						{
							if (this.fusionTarget.IsCode(92892239))
							{
								CardLocation[] array2 = new CardLocation[]
								{
									CardLocation.Grave,
									CardLocation.Hand,
									CardLocation.MonsterZone
								};
								for (int i = 0; i < array2.Length; i++)
								{
									CardLocation loc6 = array2[i];
									List<ClientCard> list8 = (from c in cards
										where c.Location == loc6
										orderby c.GetDefensePower()
										select c).ToList<ClientCard>();
									int banishedAlbazCount2 = base.Bot.Banished.Where((ClientCard c) => c.IsOriginalCode(68468459)).Count<ClientCard>();
									banishedAlbazCount2 += this.selectedFusionMaterial.Where((ClientCard c) => c.IsOriginalCode(68468459)).Count<ClientCard>();
									foreach (ClientCard target19 in list8)
									{
										if (!target19.IsOriginalCode(68468459) || banishedAlbazCount2 <= 0)
										{
											this.selectedFusionMaterial.Add(target19);
											return base.Util.CheckSelectCount(new List<ClientCard> { target19 }, cards, min, max);
										}
									}
								}
							}
							if (this.fusionTarget.IsCode(72272462))
							{
								if (this.selectedFusionMaterial.Count == 0)
								{
									CardLocation[] array2 = new CardLocation[]
									{
										CardLocation.Grave,
										CardLocation.Hand,
										CardLocation.MonsterZone
									};
									for (int i = 0; i < array2.Length; i++)
									{
										CardLocation loc7 = array2[i];
										List<ClientCard> cardsInLoc3 = (from c in cards
											where c.Location == loc7 && c.HasSetcode(356) && (loc7 != CardLocation.Grave || !this.CheckWhetherShouldKeepInGrave(c))
											orderby c.GetDefensePower()
											select c).ToList<ClientCard>();
										if (cardsInLoc3.Count > 0)
										{
											this.selectedFusionMaterial.Add(cardsInLoc3[0]);
											return base.Util.CheckSelectCount(cardsInLoc3, cards, min, max);
										}
									}
								}
								else
								{
									CardLocation[] array2 = new CardLocation[]
									{
										CardLocation.Grave,
										CardLocation.Hand,
										CardLocation.MonsterZone
									};
									for (int i = 0; i < array2.Length; i++)
									{
										CardLocation loc8 = array2[i];
										List<ClientCard> cardsInLoc4 = (from c in cards
											where c.Location == loc8 && c.HasAttribute((CardAttribute)48) && (loc8 != CardLocation.Grave || !this.CheckWhetherShouldKeepInGrave(c))
											orderby c.GetDefensePower()
											select c).ToList<ClientCard>();
										if (cardsInLoc4.Count > 0)
										{
											if (!this.activatedCardIdList.Contains(19096727))
											{
												ClientCard mercourier2 = cardsInLoc4.FirstOrDefault((ClientCard c) => c.IsCode(19096726));
												if (mercourier2 != null)
												{
													this.selectedFusionMaterial.Add(mercourier2);
													return base.Util.CheckSelectCount(new List<ClientCard> { mercourier2 }, cards, min, max);
												}
											}
											if (!this.activatedCardIdList.Contains(36577931) && this.CheckRemainInDeck(new int[] { 62962630, 45883110 }) > 0)
											{
												ClientCard tragedy2 = cardsInLoc4.FirstOrDefault((ClientCard c) => c.IsCode(36577931));
												if (tragedy2 != null)
												{
													this.selectedFusionMaterial.Add(tragedy2);
													return base.Util.CheckSelectCount(new List<ClientCard> { tragedy2 }, cards, min, max);
												}
											}
											this.selectedFusionMaterial.Add(cardsInLoc4[0]);
											return base.Util.CheckSelectCount(cardsInLoc4, cards, min, max);
										}
									}
								}
							}
							if (this.fusionTarget.IsCode(this.albazFusionMonster))
							{
								if (this.selectedFusionMaterial.Count == 0)
								{
									CardLocation[] array2 = new CardLocation[]
									{
										CardLocation.Grave,
										CardLocation.MonsterZone,
										CardLocation.Hand
									};
									for (int i = 0; i < array2.Length; i++)
									{
										CardLocation loc9 = array2[i];
										ClientCard albaz3 = (from c in cards
											where c.IsCode(68468459) && c.Location == loc9
											orderby c.GetDefensePower()
											select c).FirstOrDefault<ClientCard>();
										if (albaz3 != null)
										{
											this.selectedFusionMaterial.Add(albaz3);
											return base.Util.CheckSelectCount(new List<ClientCard> { albaz3 }, cards, min, max);
										}
									}
								}
								else
								{
									if (this.fusionTarget.IsOriginalCode(3410461) && cancelable)
									{
										return null;
									}
									List<Func<ClientCard, bool>> list9 = new List<Func<ClientCard, bool>>();
									list9.Add((ClientCard c) => c.Location == CardLocation.Grave && !this.CheckWhetherShouldKeepInGrave(c));
									list9.Add((ClientCard c) => c.Location == CardLocation.MonsterZone && c.GetDefensePower() <= 2000);
									list9.Add((ClientCard c) => c.Location == CardLocation.Grave);
									list9.Add((ClientCard c) => c.Location == CardLocation.Hand);
									list9.Add((ClientCard c) => c.Location == CardLocation.MonsterZone);
									using (List<Func<ClientCard, bool>>.Enumerator enumerator5 = list9.GetEnumerator())
									{
										while (enumerator5.MoveNext())
										{
											Func<ClientCard, bool> func = enumerator5.Current;
											List<ClientCard> targetList2 = (from c in cards
												where func(c)
												orderby c.GetDefensePower()
												select c).ToList<ClientCard>();
											if (targetList2.Count > 0)
											{
												this.selectedFusionMaterial.Add(targetList2[0]);
												return base.Util.CheckSelectCount(new List<ClientCard> { targetList2[0] }, cards, min, max);
											}
										}
									}
								}
							}
						}
						if (hint == 574)
						{
							Dictionary<int, Func<bool>> dictionary4 = new Dictionary<int, Func<bool>>();
							dictionary4.Add(29948294, () => this.Bot.HasInMonstersZone(45883110, false, false, false) && this.BrandedInHighSpiritsActivateCheck());
							Func<ClientCard, bool> <>9__214;
							dictionary4.Add(19271881, delegate
							{
								IEnumerable<ClientCard> monsterZone3 = this.Bot.MonsterZone;
								Func<ClientCard, bool> func13;
								if ((func13 = <>9__214) == null)
								{
									func13 = (<>9__214 = (ClientCard c) => c != null && c.IsFaceup() && c.IsCode(this.albazFusionMonster));
								}
								return monsterZone3.Any(func13);
							});
							dictionary4.Add(82738008, () => this.Bot.Graveyard.Any((ClientCard c) => c != null && (c.HasSetcode(356) || c.IsCode(68468459))));
							Func<ClientCard, bool> <>9__216;
							dictionary4.Add(17751597, delegate
							{
								IEnumerable<ClientCard> graveyard = this.Bot.Graveyard;
								Func<ClientCard, bool> func14;
								if ((func14 = <>9__216) == null)
								{
									func14 = (<>9__216 = (ClientCard c) => c != null && c.IsCode(this.albazFusionMonster));
								}
								return graveyard.Where(func14).Count<ClientCard>() > 1;
							});
							dictionary4.Add(44362883, () => this.CheckRemainInDeck(68468459) > 0);
							dictionary4.Add(32756828, () => this.Bot.MonsterZone.Any((ClientCard c) => c != null && c.IsFaceup() && c.HasSetcode(392)));
							dictionary4.Add(18973184, () => true);
							dictionary4.Add(34995106, () => true);
							using (Dictionary<int, Func<bool>>.Enumerator enumerator2 = dictionary4.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									KeyValuePair<int, Func<bool>> pair7 = enumerator2.Current;
									ClientCard target20 = cards.FirstOrDefault((ClientCard c) => c.IsOriginalCode(pair7.Key));
									if (target20 != null && pair7.Value())
									{
										this.fusionTarget = target20;
										return base.Util.CheckSelectCount(new List<ClientCard> { target20 }, cards, min, max);
									}
								}
								goto IL_4613;
							}
							goto IL_444F;
						}
						goto IL_4613;
					}
					if (hint == 509)
					{
						this.cartesiaMaterialList = this.cartesiaMaterialList.Where((ClientCard c) => c != null && (c.Location == CardLocation.MonsterZone || c.Location == CardLocation.Hand)).ToList<ClientCard>();
						List<ClientCard> materialList = base.Bot.MonsterZone.Where((ClientCard c) => c != null && c.GetDefensePower() <= 2500 && !c.IsCode(this.cannotBeFusionMaterialIdList)).ToList<ClientCard>();
						materialList.AddRange(base.Bot.Hand.Where((ClientCard c) => c.IsMonster() && (!this.CheckWhetherCanSummon() || ((this.activatedCardIdList.Contains(62962630) || !c.IsCode(62962630)) && (this.activatedCardIdList.Contains(45484331) || !c.IsCode(45484331))))));
						List<ClientCard> list10;
						if (this.cartesiaSummonGoal > 0)
						{
							ClientCard _fusionTarget;
							this.BlazingCartesiaTheVirtuousFusionCheck(cards, this.cartesiaSummonGoal, materialList, this.cartesiaMaterialList, out _fusionTarget, out list10);
							if (_fusionTarget != null)
							{
								this.fusionTarget = _fusionTarget;
								return base.Util.CheckSelectCount(new List<ClientCard> { this.fusionTarget }, cards, min, max);
							}
						}
						ClientCard _fusionTarget2;
						this.BlazingCartesiaTheVirtuousFusionCheck(cards, 0, materialList, this.cartesiaMaterialList, out _fusionTarget2, out list10);
						if (_fusionTarget2 != null)
						{
							this.fusionTarget = _fusionTarget2;
							return base.Util.CheckSelectCount(new List<ClientCard> { this.fusionTarget }, cards, min, max);
						}
					}
					if (hint != 511)
					{
						goto IL_4613;
					}
					List<ClientCard> mustSelectMaterialList = this.cartesiaMaterialList.Intersect(cards).ToList<ClientCard>();
					if (mustSelectMaterialList != null && mustSelectMaterialList.Count > 0)
					{
						this.selectedFusionMaterial.Add(mustSelectMaterialList[0]);
						return base.Util.CheckSelectCount(mustSelectMaterialList, cards, min, max);
					}
					ClientCard lubellion = cards.FirstOrDefault((ClientCard c) => c != null && c.IsCode(32731036) && c.Location == CardLocation.MonsterZone);
					if (lubellion != null && !base.Bot.HasInHandOrInSpellZone(32756828) && (this.activatedCardIdList.Contains(32731037) || this.CheckRemainInDeck(new int[] { 18973184, 32756828 }) == 0))
					{
						if (!base.Util.IsTurn1OrMain2())
						{
							if (base.Enemy.MonsterZone.Count((ClientCard c) => c != null && c.GetDefensePower() < 2500) <= 0)
							{
								goto IL_0F3E;
							}
						}
						return base.Util.CheckSelectCount(new List<ClientCard> { lubellion }, cards, min, max);
					}
					IL_0F3E:
					ClientCard selectTarget = (from c in cards
						where c.Attack <= 2500 && (!this.CheckWhetherCanSummon() || ((this.activatedCardIdList.Contains(62962630) || !c.IsCode(62962630)) && (this.activatedCardIdList.Contains(45484331) || !c.IsCode(45484331))))
						orderby c.GetDefensePower()
						select c).FirstOrDefault<ClientCard>();
					if (selectTarget != null)
					{
						this.selectedFusionMaterial.Add(selectTarget);
						return base.Util.CheckSelectCount(new List<ClientCard> { selectTarget }, cards, min, max);
					}
					selectTarget = cards.OrderBy((ClientCard c) => c.GetDefensePower()).FirstOrDefault<ClientCard>();
					if (selectTarget != null)
					{
						this.selectedFusionMaterial.Add(selectTarget);
						return base.Util.CheckSelectCount(new List<ClientCard> { selectTarget }, cards, min, max);
					}
					goto IL_4613;
					IL_444F:
					if (hint == 573)
					{
						List<ClientCard> enemyCardList = cards.Where((ClientCard c) => c.IsFaceup() && c.Controller == 1).ToList<ClientCard>();
						List<ClientCard> problemCardList = this.GetProblematicEnemyCardList(false, false, (CardType)0).Intersect(enemyCardList).ToList<ClientCard>();
						if (problemCardList.Count > 0)
						{
							return base.Util.CheckSelectCount(this.ShuffleList<ClientCard>(problemCardList), cards, min, max);
						}
						List<ClientCard> monsterList = this.GetMonsterListForTargetNegate(false, (CardType)0).Intersect(enemyCardList).ToList<ClientCard>();
						if (monsterList.Count > 0)
						{
							return base.Util.CheckSelectCount(this.ShuffleList<ClientCard>(monsterList), cards, min, max);
						}
						if (enemyCardList.Count > 0)
						{
							return base.Util.CheckSelectCount(this.ShuffleList<ClientCard>(enemyCardList), cards, min, max);
						}
					}
					if (hint == 509)
					{
						CardLocation[] array2 = new CardLocation[]
						{
							CardLocation.Deck,
							CardLocation.Hand
						};
						for (int i = 0; i < array2.Length; i++)
						{
							CardLocation loc10 = array2[i];
							using (List<int>.Enumerator enumerator6 = new List<int> { 95515789, 45883110 }.GetEnumerator())
							{
								while (enumerator6.MoveNext())
								{
									int checkId5 = enumerator6.Current;
									ClientCard target21 = cards.FirstOrDefault((ClientCard c) => c.Location == loc10 && c.IsOriginalCode(checkId5));
									if (target21 != null)
									{
										return base.Util.CheckSelectCount(new List<ClientCard> { target21 }, cards, min, max);
									}
								}
							}
						}
						goto IL_4613;
					}
					goto IL_4613;
				}
				IL_09BB:
				if (hint == 509)
				{
					using (List<int>.Enumerator enumerator6 = new List<int> { 44146295, 3410461, 87746184, 38811586, 70534340, 92892239, 41373230, 51409648 }.GetEnumerator())
					{
						while (enumerator6.MoveNext())
						{
							int targetId = enumerator6.Current;
							if (targetId != 70534340 || base.Bot.Hand.Count != 0)
							{
								ClientCard target22 = cards.FirstOrDefault((ClientCard c) => c.IsCode(targetId));
								if (target22 != null)
								{
									this.fusionTarget = target22;
									return base.Util.CheckSelectCount(new List<ClientCard> { target22 }, cards, min, max);
								}
							}
						}
					}
				}
				if (hint != 511)
				{
					goto IL_4613;
				}
				if (cards.Count == 1)
				{
					this.selectedFusionMaterial.AddRange(cards);
					return base.Util.CheckSelectCount(cards, cards, min, max);
				}
				List<ClientCard> sortedResult = cards.OrderByDescending((ClientCard card) => card3.GetDefensePower()).ToList<ClientCard>();
				this.selectedFusionMaterial.Add(sortedResult[0]);
				return base.Util.CheckSelectCount(sortedResult, cards, min, max);
				IL_1A7A:
				if (hint == 509)
				{
					Func<ClientCard, bool> <>9__85;
					Func<ClientCard, bool> <>9__86;
					using (Dictionary<int, Func<bool>>.Enumerator enumerator2 = new Dictionary<int, Func<bool>>
					{
						{
							41373230,
							() => this.Enemy.HasInMonstersZone(48626373, false, false, false)
						},
						{
							51409648,
							() => this.CheckWhetherWillbeRemoved() && this.CheckRemainInDeck(19096726) > 0
						},
						{
							38811586,
							() => this.CheckShouldNoMoreSpSummon()
						},
						{
							87746184,
							delegate
							{
								IEnumerable<ClientCard> graveyard2 = this.Bot.Graveyard;
								Func<ClientCard, bool> func15;
								if ((func15 = <>9__85) == null)
								{
									func15 = (<>9__85 = (ClientCard c) => c != null && c.IsMonster() && c.HasAttribute(CardAttribute.Dark) && !c.IsCode(this.cannotBeFusionMaterialIdList));
								}
								bool flag2 = graveyard2.Any(func15) | this.Bot.HasInHandOrHasInMonstersZone(19096726);
								IEnumerable<ClientCard> monsters3 = this.Bot.GetMonsters();
								Func<ClientCard, bool> func16;
								if ((func16 = <>9__86) == null)
								{
									func16 = (<>9__86 = (ClientCard c) => c.GetDefensePower() <= 1800 && c.HasAttribute(CardAttribute.Dark) && !c.IsCode(this.cannotBeFusionMaterialIdList));
								}
								return flag2 | monsters3.Any(func16);
							}
						},
						{
							70534340,
							() => !this.CheckWhetherNegated(true, true, CardType.Monster) && this.Bot.Hand.Count > 0
						},
						{
							44146295,
							() => this.Bot.HasInMonstersZone(new List<int> { 24915933, 87746184, 70534340 }, false, false, false)
						}
					}.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							KeyValuePair<int, Func<bool>> pair8 = enumerator2.Current;
							ClientCard target23 = cards.FirstOrDefault((ClientCard card) => card3.IsCode(pair8.Key));
							if (target23 != null && pair8.Value())
							{
								this.fusionTarget = target23;
								return base.Util.CheckSelectCount(new List<ClientCard> { target23 }, cards, min, max);
							}
						}
					}
				}
				if (hint != 511)
				{
					goto IL_4613;
				}
				if (this.selectedFusionMaterial.Count == 0)
				{
					CardLocation[] array2 = new CardLocation[]
					{
						CardLocation.Deck,
						CardLocation.Hand,
						CardLocation.MonsterZone
					};
					for (int i = 0; i < array2.Length; i++)
					{
						CardLocation loc = array2[i];
						ClientCard target24 = cards.FirstOrDefault((ClientCard c) => c.IsOriginalCode(68468459) && c.Location == loc);
						if (target24 != null)
						{
							this.selectedFusionMaterial.Add(target24);
							return base.Util.CheckSelectCount(new List<ClientCard> { target24 }, cards, min, max);
						}
					}
				}
				if (this.fusionTarget == null)
				{
					goto IL_4613;
				}
				List<int> checkIdList;
				new Dictionary<int, List<int>>
				{
					{
						38811586,
						new List<int> { 95515789, 97268402, 45883110 }
					},
					{
						44146295,
						new List<int> { 24915933, 87746184, 53971455 }
					},
					{
						70534340,
						new List<int> { 36577931, 60242223, 25451383, 62962630, 19096726 }
					},
					{
						41373230,
						new List<int> { 32731036, 25451383, 60242223 }
					},
					{
						51409648,
						new List<int> { 19096726, 45484331 }
					},
					{
						87746184,
						new List<int> { 32731036, 95515789, 45883110 }
					}
				}.TryGetValue(this.fusionTarget.GetOriginCode(), out checkIdList);
				if (checkIdList == null || checkIdList.Count <= 0)
				{
					goto IL_4613;
				}
				using (List<CardLocation>.Enumerator enumerator = new List<CardLocation>
				{
					CardLocation.Deck,
					CardLocation.Hand,
					CardLocation.MonsterZone
				}.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						CardLocation location = enumerator.Current;
						using (List<int>.Enumerator enumerator6 = checkIdList.GetEnumerator())
						{
							while (enumerator6.MoveNext())
							{
								int checkId6 = enumerator6.Current;
								ClientCard target25 = cards.FirstOrDefault((ClientCard c) => c.Location == location && c.IsCode(checkId6));
								if (target25 != null)
								{
									this.selectedFusionMaterial.Add(target25);
									return base.Util.CheckSelectCount(new List<ClientCard> { target25 }, cards, min, max);
								}
							}
						}
					}
					goto IL_4613;
				}
				IL_1EA3:
				ClientCard sarcophagusTarget;
				this.GoldSarcophagusTarget(cards, out sarcophagusTarget);
				if (sarcophagusTarget != null)
				{
					return base.Util.CheckSelectCount(new List<ClientCard> { sarcophagusTarget }, cards, min, max);
				}
				goto IL_4613;
				IL_2345:
				if (hint == 509)
				{
					List<ClientCard> materialList2 = base.Bot.MonsterZone.Where((ClientCard c) => c != null && c.GetDefensePower() <= 2500 && !c.IsCode(this.cannotBeFusionMaterialIdList)).ToList<ClientCard>();
					materialList2.AddRange(base.Bot.Hand.Where((ClientCard c) => c.IsMonster() && (!this.CheckWhetherCanSummon() || ((this.activatedCardIdList.Contains(62962630) || !c.IsCode(62962630)) && (this.activatedCardIdList.Contains(45484331) || !c.IsCode(45484331))))));
					List<ClientCard> list10;
					ClientCard _fusionTarget3;
					this.BrandedInRedFusionCheck(cards, 0, materialList2, this.brandedInRedMaterialList, out _fusionTarget3, out list10);
					if (_fusionTarget3 != null)
					{
						this.fusionTarget = _fusionTarget3;
						return base.Util.CheckSelectCount(new List<ClientCard> { this.fusionTarget }, cards, min, max);
					}
				}
				if (hint != 511)
				{
					goto IL_4613;
				}
				List<ClientCard> mustSelectMaterialList2 = this.brandedInRedMaterialList.Intersect(cards).ToList<ClientCard>();
				if (mustSelectMaterialList2 != null && mustSelectMaterialList2.Count > 0)
				{
					this.selectedFusionMaterial.Add(mustSelectMaterialList2[0]);
					return base.Util.CheckSelectCount(mustSelectMaterialList2, cards, min, max);
				}
				ClientCard selectTarget2 = (from c in cards
					where c.Attack <= 2500 && (!this.CheckWhetherCanSummon() || ((this.activatedCardIdList.Contains(62962630) || !c.IsCode(62962630)) && (this.activatedCardIdList.Contains(45484331) || !c.IsCode(45484331))))
					orderby c.GetDefensePower()
					select c).FirstOrDefault<ClientCard>();
				if (selectTarget2 != null)
				{
					this.selectedFusionMaterial.Add(selectTarget2);
					return base.Util.CheckSelectCount(new List<ClientCard> { selectTarget2 }, cards, min, max);
				}
				selectTarget2 = cards.OrderBy((ClientCard c) => c.GetDefensePower()).FirstOrDefault<ClientCard>();
				if (selectTarget2 != null)
				{
					this.selectedFusionMaterial.Add(selectTarget2);
					return base.Util.CheckSelectCount(new List<ClientCard> { selectTarget2 }, cards, min, max);
				}
				goto IL_4613;
				IL_35AE:
				Dictionary<int, Func<bool>> dictionary5 = new Dictionary<int, Func<bool>>();
				dictionary5.Add(68468459, () => this.CheckAlbazFusion(null));
				dictionary5.Add(45883110, () => !this.DefaultCheckWhetherCardIdIsNegated(45883110));
				dictionary5.Add(62962630, () => !this.DefaultCheckWhetherCardIdIsNegated(62962630));
				dictionary5.Add(36577931, () => true);
				using (Dictionary<int, Func<bool>>.Enumerator enumerator2 = dictionary5.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						KeyValuePair<int, Func<bool>> pair9 = enumerator2.Current;
						ClientCard target26 = cards.FirstOrDefault((ClientCard card) => card3.IsCode(pair9.Key));
						if (target26 != null && pair9.Value())
						{
							this.fusionTarget = target26;
							return base.Util.CheckSelectCount(new List<ClientCard> { target26 }, cards, min, max);
						}
					}
					goto IL_4613;
				}
				IL_36BC:
				using (Dictionary<int, Func<bool>>.Enumerator enumerator2 = new Dictionary<int, Func<bool>>
				{
					{
						68468459,
						() => this.CheckAlbazFusion(null)
					},
					{
						45484331,
						() => !this.DefaultCheckWhetherCardIdIsNegated(45484331)
					}
				}.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						KeyValuePair<int, Func<bool>> pair10 = enumerator2.Current;
						ClientCard target27 = cards.FirstOrDefault((ClientCard card) => card3.IsCode(pair10.Key));
						if (target27 != null && pair10.Value())
						{
							this.fusionTarget = target27;
							return base.Util.CheckSelectCount(new List<ClientCard> { target27 }, cards, min, max);
						}
					}
					goto IL_4613;
				}
				IL_3789:
				using (Dictionary<int, Func<bool>>.Enumerator enumerator2 = new Dictionary<int, Func<bool>>
				{
					{
						68468459,
						() => this.CheckAlbazFusion(null)
					},
					{
						45883110,
						() => !this.DefaultCheckWhetherCardIdIsNegated(45883110)
					}
				}.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						KeyValuePair<int, Func<bool>> pair11 = enumerator2.Current;
						ClientCard target28 = cards.FirstOrDefault((ClientCard card) => card3.IsCode(pair11.Key));
						if (target28 != null && pair11.Value())
						{
							this.fusionTarget = target28;
							return base.Util.CheckSelectCount(new List<ClientCard> { target28 }, cards, min, max);
						}
					}
					goto IL_4613;
				}
				IL_3856:
				if (hint == 509)
				{
					Dictionary<int, Func<bool>> dictionary6 = new Dictionary<int, Func<bool>>();
					dictionary6.Add(68468459, () => this.CheckAlbazFusion(null));
					dictionary6.Add(51409648, () => true);
					dictionary6.Add(25451383, () => true);
					using (Dictionary<int, Func<bool>>.Enumerator enumerator2 = dictionary6.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							KeyValuePair<int, Func<bool>> pair12 = enumerator2.Current;
							ClientCard target29 = cards.FirstOrDefault((ClientCard c) => c.IsOriginalCode(pair12.Key));
							if (target29 != null && pair12.Value())
							{
								return base.Util.CheckSelectCount(new List<ClientCard> { target29 }, cards, min, max);
							}
						}
					}
				}
				if (hint == 505)
				{
					List<ClientCard> problemList = (from c in this.GetProblematicEnemyCardList(false, true, CardType.Monster).Intersect(cards)
						orderby c.GetDefensePower() descending
						select c).ToList<ClientCard>();
					if (problemList.Count > 0)
					{
						return base.Util.CheckSelectCount(problemList, cards, min, max);
					}
					ClientCard worstBotMonster2 = base.Util.GetWorstBotMonster(false);
					int worstBotPower = ((worstBotMonster2 == null) ? 0 : worstBotMonster2.GetDefensePower());
					List<ClientCard> dangerList = (from c in cards
						where c.IsFaceup() && c.Controller == 1 && c.GetDefensePower() > worstBotPower
						orderby c.GetDefensePower() descending
						select c).ToList<ClientCard>();
					if (dangerList.Count > 0)
					{
						return base.Util.CheckSelectCount(dangerList, cards, min, max);
					}
					using (List<int>.Enumerator enumerator6 = new List<int> { 62962630, 45484331 }.GetEnumerator())
					{
						while (enumerator6.MoveNext())
						{
							int checkId7 = enumerator6.Current;
							ClientCard target30 = cards.FirstOrDefault((ClientCard c) => c.Controller == 0 && c.IsCode(checkId7));
							if (target30 != null)
							{
								return base.Util.CheckSelectCount(new List<ClientCard> { target30 }, cards, min, max);
							}
						}
					}
					List<ClientCard> enemyMonsterList = (from c in cards
						where c.Controller == 1
						orderby c.GetDefensePower() descending
						select c).ToList<ClientCard>();
					if (enemyMonsterList.Count > 0)
					{
						return base.Util.CheckSelectCount(enemyMonsterList, cards, min, max);
					}
					return base.Util.CheckSelectCount((from c in cards
						where c.Controller == 0
						orderby c.GetDefensePower() descending
						select c).ToList<ClientCard>(), cards, min, max);
				}
			}
			IL_4613:
			bool discardHand = hint == 501;
			bool flag;
			if (hint == 507)
			{
				flag = cards.All((ClientCard c) => c.Location == CardLocation.Hand);
			}
			else
			{
				flag = false;
			}
			bool handToDeck = flag;
			if (min == 1 && max == 1 && (discardHand || handToDeck))
			{
				if (currentSolvingChain != null && currentSolvingChain.IsCode(36637374))
				{
					ClientCard tragedy3 = cards.FirstOrDefault((ClientCard card) => card3.IsCode(36577931));
					if (tragedy3 != null)
					{
						return base.Util.CheckSelectCount(new List<ClientCard> { tragedy3 }, cards, min, max);
					}
				}
				if (discardHand)
				{
					foreach (ClientCard target31 in cards)
					{
						if (target31.IsCode(25451383) && base.Duel.CurrentChain.Contains(target31))
						{
							return base.Util.CheckSelectCount(new List<ClientCard> { target31 }, cards, min, max);
						}
					}
					using (List<int>.Enumerator enumerator6 = new List<int> { 17751597, 25451383, 60242223, 19271881, 29948294, 95515789, 36577931 }.GetEnumerator())
					{
						while (enumerator6.MoveNext())
						{
							int id2 = enumerator6.Current;
							ClientCard card2 = cards.FirstOrDefault((ClientCard c) => c.IsCode(id2));
							if (card2 != null)
							{
								return base.Util.CheckSelectCount(new List<ClientCard> { card2 }, cards, min, max);
							}
						}
					}
				}
				using (IEnumerator<ClientCard> enumerator4 = cards.GetEnumerator())
				{
					while (enumerator4.MoveNext())
					{
						ClientCard card3 = enumerator4.Current;
						if (cards.Where((ClientCard c) => c.IsCode(card3.Id)).Count<ClientCard>() > 1)
						{
							return base.Util.CheckSelectCount(new List<ClientCard> { card3 }, cards, min, max);
						}
					}
				}
				using (List<int>.Enumerator enumerator6 = new List<int>
				{
					17751597, 29948294, 36577931, 6498706, 32756828, 25451383, 36637374, 75500286, 81439173, 68468459,
					82738008, 10045474, 32731036, 18973184, 45484331, 45883110, 65681983, 24224830, 19096726, 14558127,
					23434538
				}.GetEnumerator())
				{
					while (enumerator6.MoveNext())
					{
						int id3 = enumerator6.Current;
						if (id3 != 18973184 || !base.Bot.HasInHand(44362883) || !this.BrandedFusionActivateCheck(true))
						{
							ClientCard target32 = cards.FirstOrDefault((ClientCard c) => c.IsCode(id3));
							if (target32 != null)
							{
								return base.Util.CheckSelectCount(new List<ClientCard> { target32 }, cards, min, max);
							}
						}
					}
					goto IL_4C7C;
				}
			}
			if (discardHand && min > 0 && min == max)
			{
				List<ClientCard> discardList = new List<ClientCard>();
				List<int> graveEffectIdList = new List<int> { 25451383, 17751597, 29948294, 19271881, 36577931 };
				discardList.AddRange(this.ShuffleList<ClientCard>(cards.Where((ClientCard c) => c.IsCode(graveEffectIdList)).ToList<ClientCard>()));
				List<ClientCard> remainHandList = cards.Except(discardList).ToList<ClientCard>();
				HashSet<int> seenIds = new HashSet<int>();
				for (int idx = remainHandList.Count - 1; idx >= 0; idx--)
				{
					ClientCard currentCard = remainHandList[idx];
					if (!seenIds.Add(currentCard.Id))
					{
						discardList.Add(currentCard);
						remainHandList.Remove(currentCard);
					}
				}
				using (List<int>.Enumerator enumerator6 = new List<int>
				{
					6498706, 32756828, 25451383, 36637374, 75500286, 81439173, 68468459, 82738008, 10045474, 32731036,
					18973184, 45484331, 45883110, 65681983, 24224830, 19096726, 14558127, 23434538
				}.GetEnumerator())
				{
					while (enumerator6.MoveNext())
					{
						int id = enumerator6.Current;
						ClientCard target33 = remainHandList.FirstOrDefault((ClientCard c) => c.IsCode(id));
						if (target33 != null)
						{
							discardList.Add(target33);
						}
					}
				}
				if (discardList.Count > min)
				{
					discardList = discardList.Take(min).ToList<ClientCard>();
				}
				return base.Util.CheckSelectCount(discardList, cards, min, max);
			}
			IL_4C7C:
			if (this.theBystialLubellionSelecting)
			{
				this.theBystialLubellionSelecting = false;
				ClientCard target34 = this.TheBystialLubellionSpSummonCost(cards);
				if (target34 != null)
				{
					return base.Util.CheckSelectCount(new List<ClientCard> { target34 }, cards, min, max);
				}
				List<ClientCard> targetList3 = new List<ClientCard>(cards);
				targetList3.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				return base.Util.CheckSelectCount(targetList3, cards, min, max);
			}
			else
			{
				if (!this.albionTheShroudedDragonSelecting && (currentSolvingChain == null || !currentSolvingChain.IsCode(60242223)))
				{
					return base.OnSelectCard(cards, min, max, hint, cancelable);
				}
				ClientCard retribution = cards.FirstOrDefault((ClientCard c) => c.IsCode(17751597));
				if (retribution != null && (retribution.Location == CardLocation.Deck || (from c in base.Bot.GetGraveyardMonsters()
					where c.IsCode(this.albazFusionMonster)
					select c).Count<ClientCard>() < 2))
				{
					return base.Util.CheckSelectCount(new List<ClientCard> { retribution }, cards, min, max);
				}
				if (base.Bot.HasInGraveyard(17751597) || (base.Bot.HasInGraveyard(36577931) && !this.activatedCardIdList.Contains(36577931)))
				{
					Func<ClientCard, bool> <>9__236;
					using (Dictionary<int, Func<bool>>.Enumerator enumerator2 = new Dictionary<int, Func<bool>>
					{
						{
							44362883,
							() => this.BrandedFusionActivateCheck(true)
						},
						{
							18973184,
							() => (this.Duel.Player != 0 || this.Duel.Phase < DuelPhase.End) && ((this.Bot.HasInHandOrInSpellZone(44362883) && this.BrandedFusionActivateCheck(true)) || (this.Bot.HasInHandOrInSpellZone(34995106) && this.BrandedInWhiteActivateCheck()) || (this.Bot.HasInHandOrInSpellZone(82738008) && this.BrandedInRedActivateCheck(false) != null) || (!this.summoned && this.Bot.HasInHand(68468459) && this.CheckAlbazFusion(null)) || (this.Bot.HasInMonstersZone(95515789, false, false, false) || (!this.summoned && this.Bot.HasInHand(95515789))))
						},
						{
							29948294,
							new Func<bool>(this.BrandedInHighSpiritsActivateCheck)
						},
						{
							82738008,
							() => this.BrandedInRedActivateCheck(false) != null
						},
						{
							34995106,
							new Func<bool>(this.BrandedInWhiteActivateCheck)
						},
						{
							17751597,
							() => cards.Any((ClientCard c) => c.IsCode(17751597) && c.Location == CardLocation.Removed)
						},
						{
							19271881,
							delegate
							{
								IEnumerable<ClientCard> monsters4 = this.Bot.GetMonsters();
								Func<ClientCard, bool> func17;
								if ((func17 = <>9__236) == null)
								{
									func17 = (<>9__236 = (ClientCard c) => c.IsFaceup() && c.IsCode(this.albazFusionMonster));
								}
								return monsters4.Any(func17);
							}
						},
						{
							36637374,
							() => this.Bot.Hand.Count > 2
						}
					}.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							KeyValuePair<int, Func<bool>> pair = enumerator2.Current;
							ClientCard target35 = cards.FirstOrDefault((ClientCard card) => card.Location == CardLocation.Deck && card.IsCode(pair.Key));
							if (target35 != null && pair.Value())
							{
								return base.Util.CheckSelectCount(new List<ClientCard> { target35 }, cards, min, max);
							}
						}
					}
				}
				if (this.albionTheShroudedDragonSelecting && this.FallenOfAlbazSetCheck() && (this.summoned || !base.Bot.HasInHand(new List<int> { 68468459, 29948294 })))
				{
					List<int> checkIdList2 = new List<int> { 17751597, 29948294, 19271881, 34995106, 36637374, 82738008, 32756828, 18973184 };
					if (!this.BrandedFusionActivateCheck(true))
					{
						checkIdList2.Add(44362883);
					}
					using (List<int>.Enumerator enumerator6 = checkIdList2.GetEnumerator())
					{
						while (enumerator6.MoveNext())
						{
							int checkId8 = enumerator6.Current;
							ClientCard target36 = cards.FirstOrDefault((ClientCard c) => c.Id == checkId8);
							if (target36 != null)
							{
								return base.Util.CheckSelectCount(new List<ClientCard> { target36 }, cards, min, max);
							}
						}
					}
				}
				using (List<int>.Enumerator enumerator6 = new List<int> { 29948294, 36637374, 19271881, 32756828, 18973184 }.GetEnumerator())
				{
					while (enumerator6.MoveNext())
					{
						int checkId = enumerator6.Current;
						ClientCard target37 = cards.FirstOrDefault((ClientCard c) => c.IsCode(checkId) && c.Location == CardLocation.Deck);
						if (target37 != null)
						{
							return base.Util.CheckSelectCount(new List<ClientCard> { target37 }, cards, min, max);
						}
					}
				}
				return base.Util.CheckSelectCount(this.ShuffleList<ClientCard>(new List<ClientCard>(cards)), cards, min, max);
			}
			IList<ClientCard> list11;
			return list11;
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x0003DD98 File Offset: 0x0003BF98
		public override int OnSelectOption(IList<int> options)
		{
			ChainInfo currentSolvingChain = base.Duel.GetCurrentSolvingChainInfo();
			if (currentSolvingChain != null)
			{
				if (options.Count == 2 && options.Contains(1190) && options.Contains(1152))
				{
					if (currentSolvingChain.IsCode(36637374))
					{
						if (!this.CheckShouldNoMoreSpSummon() || this.summoned || base.Duel.Player != 0)
						{
							return options.IndexOf(1152);
						}
						return options.IndexOf(1190);
					}
					else if (this.fusionTarget != null && (currentSolvingChain.IsCode(72272462) || currentSolvingChain.IsCode(41373230) || currentSolvingChain.IsCode(1906812)))
					{
						if (this.fusionTarget.IsCode(68468459))
						{
							if (!this.CheckAlbazFusion(null))
							{
								return options.IndexOf(1190);
							}
							return options.IndexOf(1152);
						}
						else if (this.fusionTarget.IsCode(new int[] { 45883110, 45484331 }))
						{
							if (!this.CheckShouldNoMoreSpSummon())
							{
								return options.IndexOf(1152);
							}
							return options.IndexOf(1190);
						}
						else if (this.fusionTarget.IsCode(62962630))
						{
							if (!this.activatedCardIdList.Contains(62962630))
							{
								return options.IndexOf(1152);
							}
							return options.IndexOf(1190);
						}
						else
						{
							if (!this.CheckShouldNoMoreSpSummon() || this.summoned)
							{
								return options.IndexOf(1152);
							}
							return options.IndexOf(1190);
						}
					}
				}
				if (currentSolvingChain.IsCode(87746184) && this.fusionTarget != null)
				{
					if (this.fusionTarget.IsOriginalCode(29948294) && base.Duel.Player == 0)
					{
						if (!this.BrandedInHighSpiritsActivateCheck())
						{
							return options.IndexOf(1153);
						}
						return options.IndexOf(1190);
					}
					else if (this.fusionTarget.IsOriginalCode(82738008) && base.Duel.Player == 0)
					{
						if (this.nadirActivated)
						{
							return options.IndexOf(1153);
						}
						if (this.BrandedInRedActivateCheck(false) == null)
						{
							return options.IndexOf(1153);
						}
						return options.IndexOf(1190);
					}
					else if (this.fusionTarget.Data != null)
					{
						if (!(this.fusionTarget.Data.HasType(CardType.Trap) | (this.fusionTarget.Data.HasType(CardType.QuickPlay) && base.Duel.Player == 0) | (base.Bot.Hand.Count >= 6 && base.Duel.Player == 0)))
						{
							return options.IndexOf(1190);
						}
						return options.IndexOf(1153);
					}
				}
			}
			return base.OnSelectOption(options);
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x0003E070 File Offset: 0x0003C270
		public override int OnSelectPlace(int cardId, int player, CardLocation location, int available)
		{
			ChainInfo currentSovingChain = base.Duel.GetCurrentSolvingChainInfo();
			if (currentSovingChain != null && currentSovingChain.ActivatePlayer == 0 && currentSovingChain.IsCode(1906812))
			{
				return this.SprindTheIrondashDragonMoveZone(available, null);
			}
			if (player == 0 && location == CardLocation.MonsterZone)
			{
				List<int> list = this.ShuffleList<int>(new List<int> { 5, 6 });
				list.AddRange(this.ShuffleList<int>(new List<int> { 0, 2, 4 }));
				list.AddRange(this.ShuffleList<int>(new List<int> { 1, 3 }));
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

		// Token: 0x06000D06 RID: 3334 RVA: 0x0003E18C File Offset: 0x0003C38C
		public override bool OnSelectYesNo(int desc)
		{
			if (desc == base.Util.GetStringId(29948294, 2))
			{
				if (this.CheckWhetherWillbeRemoved())
				{
					return false;
				}
				if (this.fusionTarget != null && this.fusionTarget.IsOriginalCode(19096726))
				{
					return !base.Bot.MonsterZone.Any((ClientCard c) => c != null && c.IsFaceup() && c.IsCode(this.albazFusionMonster));
				}
			}
			if (desc == base.Util.GetStringId(51409648, 2))
			{
				return base.Enemy.MonsterZone.Any((ClientCard c) => c != null) | base.Bot.MonsterZone.Any((ClientCard c) => c != null && (c.IsOriginalCode(62962630) || c.IsOriginalCode(45484331)));
			}
			if (desc == base.Util.GetStringId(53971455, 2))
			{
				return base.Enemy.MonsterZone.Any((ClientCard c) => c != null && c.IsFaceup() && !c.IsDisabled()) | base.Enemy.SpellZone.Any((ClientCard c) => c != null && c.IsFaceup() && !c.IsDisabled());
			}
			if (desc == base.Util.GetStringId(82738008, 0))
			{
				this.brandedInRedMaterialList = this.brandedInRedMaterialList.Where((ClientCard c) => c != null && (c.Location == CardLocation.MonsterZone || c.Location == CardLocation.Hand)).ToList<ClientCard>();
				List<ClientCard> materialList = base.Bot.MonsterZone.Where((ClientCard c) => c != null && c.Attack <= 2500 && !c.IsCode(this.cannotBeFusionMaterialIdList)).ToList<ClientCard>();
				materialList.AddRange(base.Bot.Hand.Where((ClientCard c) => c.IsMonster() && (!this.CheckWhetherCanSummon() || ((this.activatedCardIdList.Contains(62962630) || !c.IsCode(62962630)) && (this.activatedCardIdList.Contains(45484331) || !c.IsCode(45484331))))));
				ClientCard _fusionTarget;
				List<ClientCard> list;
				this.BrandedInRedFusionCheck(base.Bot.ExtraDeck, 0, materialList, this.brandedInRedMaterialList, out _fusionTarget, out list);
				return _fusionTarget != null;
			}
			if (desc == base.Util.GetStringId(1906812, 2))
			{
				ClientCard currentSolvingChain = base.Duel.GetCurrentSolvingChainCard();
				if (currentSolvingChain != null)
				{
					return this.SprindTheIrondashDragonDestroyValue(currentSolvingChain.Sequence, null) > 0;
				}
			}
			return base.OnSelectYesNo(desc);
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x0003E3C8 File Offset: 0x0003C5C8
		public override CardPosition OnSelectPosition(int cardId, IList<CardPosition> positions)
		{
			ClientCard currentSolvingChain = base.Duel.GetCurrentSolvingChainCard();
			if (currentSolvingChain != null && currentSolvingChain.IsCode(38811586))
			{
				this.sanctifireSelectPositionCount++;
				if (this.sanctifireSelectPositionCount >= 2 && base.Duel.Phase <= DuelPhase.Main2)
				{
					return CardPosition.FaceUpDefence;
				}
			}
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

		// Token: 0x06000D08 RID: 3336 RVA: 0x0003E4D0 File Offset: 0x0003C6D0
		public override void OnNewTurn()
		{
			if (base.Duel.Turn <= 1)
			{
				this.dimensionShifterCount = 0;
			}
			this.summoned = false;
			this.enemyActivateMaxxC = false;
			this.enemyActivateLockBird = false;
			this.enemyActivateInfiniteImpermanenceFromHand = false;
			this.nadirActivated = false;
			this.fusionToGYFlag = false;
			this.spSummoningAlbaz = false;
			this.cartesiaSummonGoal = 0;
			this.sanctifireSelectPositionCount = 0;
			this.quemSummonFlag = 0;
			if (this.dimensionShifterCount > 0)
			{
				this.dimensionShifterCount--;
			}
			this.cartesiaMaterialList.Clear();
			this.brandedInRedMaterialList.Clear();
			this.infiniteImpermanenceList.Clear();
			this.currentNegateCardList.Clear();
			this.currentDestroyCardList.Clear();
			this.sendToGYThisTurn.Clear();
			this.activatedCardIdList.Clear();
			this.enemyPlaceThisTurn.Clear();
			base.OnNewTurn();
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x0003E5B0 File Offset: 0x0003C7B0
		public override void OnChaining(int player, ClientCard card)
		{
			base.Duel.LastChainTargets.Clear();
			if (card == null)
			{
				return;
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

		// Token: 0x06000D0A RID: 3338 RVA: 0x0003E628 File Offset: 0x0003C828
		public override void OnChainSolved(int chainIndex)
		{
			ChainInfo currentCard = base.Duel.GetCurrentSolvingChainInfo();
			if (currentCard != null)
			{
				if (currentCard.ActivatePlayer == 0)
				{
					List<int> activateCheck = new List<int> { 1984618, 6498706, 44362883, 82738008 };
					if (currentCard.IsCode(activateCheck))
					{
						this.activatedCardIdList.Add(currentCard.ActivateId);
					}
				}
				if (!base.Duel.IsCurrentSolvingChainNegated())
				{
					if (currentCard.ActivatePlayer == 1)
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
					}
					if (currentCard.ActivatePlayer == 0 && currentCard.IsCode(1984618))
					{
						this.nadirActivated = true;
					}
				}
			}
			this.fusionTarget = null;
			this.selectedFusionMaterial.Clear();
			this.sanctifireSelectPositionCount = 0;
			base.OnChainSolved(chainIndex);
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x0003E72C File Offset: 0x0003C92C
		public override void OnChainEnd()
		{
			this.cartesiaSummonGoal = 0;
			this.cartesiaMaterialList.Clear();
			this.brandedInRedMaterialList.Clear();
			this.currentNegateCardList.Clear();
			this.currentDestroyCardList.Clear();
			this.enemyActivateInfiniteImpermanenceFromHand = false;
			this.theBystialLubellionSelecting = false;
			this.albionTheShroudedDragonSelecting = false;
			this.spSummoningAlbaz = false;
			for (int idx = this.enemyPlaceThisTurn.Count - 1; idx >= 0; idx--)
			{
				ClientCard checkTarget = this.enemyPlaceThisTurn[idx];
				if (checkTarget == null || (checkTarget.Location != CardLocation.SpellZone && checkTarget.Location != CardLocation.MonsterZone))
				{
					this.enemyPlaceThisTurn.RemoveAt(idx);
				}
			}
			if (this.quemSummonFlag > 0)
			{
				this.quemSummonFlag--;
			}
			base.OnChainEnd();
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x0003E7EC File Offset: 0x0003C9EC
		public override void OnMove(ClientCard card, int previousControler, int previousLocation, int currentControler, int currentLocation)
		{
			if (previousControler == 1 && card != null && card.IsCode(10045474) && previousLocation == 2 && currentLocation == 8)
			{
				this.enemyActivateInfiniteImpermanenceFromHand = true;
			}
			if (card != null)
			{
				if (currentControler == 1 && (currentLocation == 4 || currentLocation == 8))
				{
					this.enemyPlaceThisTurn.Add(card);
				}
				if (currentControler == 0)
				{
					base.Duel.GetCurrentSolvingChainCard();
					if (previousLocation == 16 && currentLocation != 16)
					{
						this.sendToGYThisTurn.Remove(card);
					}
					if (currentLocation == 16)
					{
						if (card.HasType(CardType.Fusion))
						{
							this.fusionToGYFlag = true;
						}
						this.sendToGYThisTurn.Add(card);
					}
					if (currentLocation == 4 && card != null && card.IsCode(45883110))
					{
						this.quemSummonFlag = 2;
					}
				}
			}
			base.OnMove(card, previousControler, previousLocation, currentControler, currentLocation);
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x0003E8B4 File Offset: 0x0003CAB4
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

		// Token: 0x06000D0E RID: 3342 RVA: 0x0003EAA0 File Offset: 0x0003CCA0
		public bool TheBystialLubellionSpSummon()
		{
			if (base.Card.Location == CardLocation.Hand)
			{
				return false;
			}
			if (this.TheBystialLubellionSpSummonCost(base.Bot.GetMonsters()) != null)
			{
				this.theBystialLubellionSelecting = true;
				this.activatedCardIdList.Add(base.Card.Id - 1);
				return true;
			}
			return false;
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0003EAF4 File Offset: 0x0003CCF4
		public ClientCard TheBystialLubellionSpSummonCost(IList<ClientCard> costList)
		{
			using (Dictionary<int, Func<ClientCard, bool>>.Enumerator enumerator = new Dictionary<int, Func<ClientCard, bool>>
			{
				{
					87746184,
					(ClientCard card) => this.sendToGYThisTurn.All((ClientCard c) => !c.IsCode(87746184))
				},
				{
					60242223,
					(ClientCard card) => !this.activatedCardIdList.Contains(60242224) && !this.CheckWhetherWillbeRemoved()
				},
				{
					41373230,
					(ClientCard card) => base.Util.IsTurn1OrMain2() || card.GetDefensePower() < 2500
				},
				{
					3410461,
					(ClientCard card) => base.Util.IsTurn1OrMain2() || card.IsDisabled() || card.GetDefensePower() < 2500
				},
				{
					25451383,
					(ClientCard card) => base.Util.IsTurn1OrMain2() || card.GetDefensePower() < 2500
				},
				{
					92892239,
					(ClientCard card) => card.IsDisabled() && this.CheckRemainInDeck(new int[] { 32756828, 18973184 }) > 0
				}
			}.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, Func<ClientCard, bool>> pair = enumerator.Current;
					foreach (ClientCard target in costList.Where((ClientCard card) => card.IsCode(pair.Key)).ToList<ClientCard>())
					{
						if (target != null && pair.Value(target))
						{
							return target;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x0003EC40 File Offset: 0x0003CE40
		public bool TheBystialLubellionActivate()
		{
			if (this.CheckWhetherNegated(true, base.Card.Location == CardLocation.MonsterZone, CardType.Monster))
			{
				return false;
			}
			if (base.Card.Location == CardLocation.Hand)
			{
				this.activatedCardIdList.Add(base.Card.Id);
			}
			else
			{
				this.activatedCardIdList.Add(base.Card.Id + 1);
			}
			return true;
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x0003ECA8 File Offset: 0x0003CEA8
		public bool AlbionTheShroudedDragonActivate()
		{
			if (this.CheckWhetherNegated(true, false, CardType.Monster) || this.CheckWhetherWillbeRemoved())
			{
				return false;
			}
			bool checkFlag = this.CheckRemainInDeck(new int[] { 17751597, 36637374, 19271881, 29948294 }) > 0;
			if (base.Bot.HasInGraveyard(17751597))
			{
				checkFlag |= this.CheckRemainInDeck(new int[] { 44362883, 32756828, 82738008, 34995106, 18973184 }) > 0;
			}
			if (base.Bot.HasInSpellZone(32756828, false, false))
			{
				checkFlag |= this.CheckRemainInDeck(18973184) > 0;
			}
			if (base.Card.Location == CardLocation.Grave)
			{
				checkFlag |= this.CheckRemainInDeck(34995106) > 0;
			}
			if (this.FallenOfAlbazSetCheck() && (this.summoned || !base.Bot.HasInHand(new List<int> { 68468459, 29948294 })))
			{
				checkFlag |= base.Bot.HasInHand(new List<int> { 32756828, 29948294, 34995106, 82738008, 18973184, 36637374, 17751597, 19271881 });
			}
			if (checkFlag)
			{
				this.activatedCardIdList.Add(base.Card.Id);
				this.albionTheShroudedDragonSelecting = true;
				return true;
			}
			return false;
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x0003EE24 File Offset: 0x0003D024
		public bool BystialSaronirActivate()
		{
			if (this.CheckWhetherNegated(true, false, CardType.Monster))
			{
				return false;
			}
			if (base.Card.Location == CardLocation.Hand)
			{
				if (base.Util.GetLastChainCard() != null && base.Duel.LastChainPlayer == 1)
				{
					List<ClientCard> chainTargetList = base.Duel.LastChainTargets.Where((ClientCard c) => this.CheckBystialCanBanish(c)).ToList<ClientCard>();
					if (chainTargetList.Count > 0)
					{
						base.AI.SelectCard(chainTargetList);
						this.currentDestroyCardList.Add(chainTargetList[0]);
						this.activatedCardIdList.Add(base.Card.Id);
						return true;
					}
				}
				List<ClientCard> enemyChainList = (from c in base.Duel.CurrentChain
					where c != null && c.Controller == 1 && this.CheckBystialCanBanish(c) && !this.currentDestroyCardList.Contains(c)
					orderby c.GetDefensePower() descending
					select c).ToList<ClientCard>();
				if (enemyChainList.Count > 0)
				{
					base.AI.SelectCard(enemyChainList);
					this.currentDestroyCardList.Add(enemyChainList[0]);
					this.activatedCardIdList.Add(base.Card.Id);
					return true;
				}
				if (!this.CheckShouldNoMoreSpSummon())
				{
					ClientCard mercourier = base.Bot.Graveyard.FirstOrDefault((ClientCard c) => c != null && c.IsCode(19096726));
					if (mercourier != null && !this.activatedCardIdList.Contains(19096727))
					{
						base.AI.SelectCard(mercourier);
						this.currentDestroyCardList.Add(mercourier);
						this.activatedCardIdList.Add(base.Card.Id);
						return true;
					}
					ClientCard tragedy = base.Bot.Graveyard.FirstOrDefault((ClientCard c) => c != null && c.IsCode(36577931));
					if (tragedy != null && !this.activatedCardIdList.Contains(36577931))
					{
						base.AI.SelectCard(tragedy);
						this.currentDestroyCardList.Add(tragedy);
						this.activatedCardIdList.Add(base.Card.Id);
						return true;
					}
					if (base.Bot.HasInGraveyard(32731036) && !this.activatedCardIdList.Contains(32731036) && base.Duel.Player == 0 && this.CheckRemainInDeck(new int[] { 18973184, 32756828 }) > 0 && base.CurrentTiming == -1)
					{
						List<ClientCard> targetList = (from c in base.Enemy.Graveyard
							where c != null && this.CheckBystialCanBanish(c)
							select c into card
							orderby card.Attack descending
							select card).ToList<ClientCard>();
						targetList.AddRange((from c in base.Bot.Graveyard
							where c != null && this.CheckBystialCanBanish(c) && !c.IsCode(32731036) && !this.CheckWhetherShouldKeepInGrave(c)
							select c into card
							orderby card.Attack
							select card).ToList<ClientCard>());
						if (targetList.Count > 0)
						{
							base.AI.SelectCard(targetList);
							this.currentDestroyCardList.Add(targetList[0]);
							this.activatedCardIdList.Add(base.Card.Id);
							return true;
						}
					}
				}
				if (base.Bot.UnderAttack && base.Bot.BattlingMonster == null)
				{
					List<ClientCard> targetList2 = (from c in base.Enemy.Graveyard
						where this.CheckBystialCanBanish(c)
						orderby c.GetDefensePower() descending
						select c).ToList<ClientCard>();
					targetList2.AddRange(from c in base.Bot.Graveyard
						where this.CheckBystialCanBanish(c)
						orderby c.GetDefensePower()
						select c);
					if (targetList2.Count > 0)
					{
						base.AI.SelectCard(targetList2);
						this.currentDestroyCardList.Add(targetList2[0]);
						this.activatedCardIdList.Add(base.Card.Id);
						return true;
					}
				}
				if (base.Duel.Player == 1 && (base.Duel.Phase == DuelPhase.Main1 || base.Duel.Phase == DuelPhase.Main2) && !this.activatedCardIdList.Contains(32756828))
				{
					if (base.Bot.SpellZone.Any((ClientCard c) => c != null && c.IsCode(32756828) && (c.IsFacedown() || !c.IsDisabled())))
					{
						if (!base.Bot.MonsterZone.Any((ClientCard c) => c != null && c.IsFaceup() && c.HasSetcode(392)) && this.GetProblematicEnemyCardList(true, false, CardType.Trap).Count > 0)
						{
							List<ClientCard> targetList3 = (from c in base.Enemy.Graveyard
								where this.CheckBystialCanBanish(c)
								orderby c.GetDefensePower() descending
								select c).ToList<ClientCard>();
							targetList3.AddRange(from c in base.Bot.Graveyard
								where this.CheckBystialCanBanish(c)
								orderby c.GetDefensePower()
								select c);
							if (targetList3.Count > 0)
							{
								base.AI.SelectCard(targetList3);
								this.currentDestroyCardList.Add(targetList3[0]);
								this.activatedCardIdList.Add(base.Card.Id);
								return true;
							}
						}
					}
				}
			}
			if (base.Card.Location == CardLocation.Grave && !this.CheckWhetherWillbeRemoved())
			{
				if (base.Bot.HasInGraveyard(17751597))
				{
					this.activatedCardIdList.Add(base.Card.Id + 1);
					return true;
				}
				if (this.CheckRemainInDeck(new int[] { 32731036, 17751597, 29948294, 19271881, 36637374 }) > 0)
				{
					this.activatedCardIdList.Add(base.Card.Id + 1);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x0003F47F File Offset: 0x0003D67F
		public bool CheckBystialCanBanish(ClientCard c)
		{
			return c != null && c.Location == CardLocation.Grave && c.IsMonster() && c.HasAttribute((CardAttribute)48);
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x0003F4A0 File Offset: 0x0003D6A0
		public bool AluberTheJesterOfDespiaSummon()
		{
			if (this.CheckWhetherNegated(true, true, CardType.Monster) || this.enemyActivateLockBird || this.activatedCardIdList.Contains(base.Card.Id))
			{
				return false;
			}
			this.summoned = true;
			return true;
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x0003F4D8 File Offset: 0x0003D6D8
		public bool AluberTheJesterOfDespiaActivate()
		{
			if (base.Card.Location == CardLocation.MonsterZone)
			{
				if (this.CheckWhetherNegated(true, true, CardType.Monster))
				{
					return false;
				}
				this.activatedCardIdList.Add(base.Card.Id);
				return true;
			}
			else
			{
				List<ClientCard> targetCardList = this.GetMonsterListForTargetNegate(true, CardType.Monster);
				ClientCard lastChainCard = base.Util.GetLastChainCard();
				if (lastChainCard != null && lastChainCard.Controller == 0)
				{
					base.AI.SelectCard(targetCardList);
					this.activatedCardIdList.Add(base.Card.Id);
					return true;
				}
				if (this.CheckWhetherNegated(true, false, CardType.Monster))
				{
					return false;
				}
				base.AI.SelectCard(targetCardList);
				this.activatedCardIdList.Add(base.Card.Id);
				return true;
			}
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x0003F58C File Offset: 0x0003D78C
		public bool FallenOfAlbazSummon()
		{
			if (this.CheckAlbazFusion(base.Card))
			{
				this.summoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x0003F5A6 File Offset: 0x0003D7A6
		public bool FallenOfAlbazSet()
		{
			if (this.FallenOfAlbazSetCheck())
			{
				this.summoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0003F5BC File Offset: 0x0003D7BC
		public bool FallenOfAlbazSetCheck()
		{
			if (!base.Bot.HasInExtra(3410461) || this.nadirActivated)
			{
				return false;
			}
			if (!base.Bot.HasInSpellZone(18973184, true, true) || base.Bot.GetHandCount() < 2)
			{
				foreach (int dangerId in this.dangerousDragonIdList)
				{
					if (base.Enemy.HasInMonstersZone(dangerId, true, false, true))
					{
						return true;
					}
				}
			}
			return (from c in base.Enemy.GetMonsters()
				where c != null && c.IsFaceup() && !c.IsCode(this.cannotBeFusionMaterialIdList) && c.HasRace(CardRace.Dragon)
				select c).Count<ClientCard>() > 1;
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0003F684 File Offset: 0x0003D884
		public bool CheckAlbazFusion(ClientCard exceptCost = null)
		{
			List<ClientCard> list;
			return this.CheckAlbazFusion(exceptCost, out list);
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x0003F69C File Offset: 0x0003D89C
		public bool CheckAlbazFusion(ClientCard exceptCost, out List<ClientCard> enemyMonsterList)
		{
			enemyMonsterList = null;
			int costHandCount = base.Bot.Hand.Where((ClientCard c) => c != exceptCost).Count<ClientCard>();
			if (costHandCount <= 0 || base.Enemy.GetMonsterCount() == 0)
			{
				return false;
			}
			if (this.CheckWhetherNegated(true, true, CardType.Monster) || this.activatedCardIdList.Contains(68468459) || this.nadirActivated)
			{
				return false;
			}
			if (!base.Bot.HasInMonstersZone(44146295, false, false, true) && !base.Bot.HasInSpellZone(44146295, false, false) && base.Bot.HasInExtra(44146295))
			{
				ClientCard target = (from c in base.Enemy.GetMonsters()
					where c.IsFaceup() && !c.IsCode(this.cannotBeFusionMaterialIdList) && c.HasType((CardType)75505728)
					orderby c.GetDefensePower() descending
					select c).FirstOrDefault<ClientCard>();
				if (target != null)
				{
					enemyMonsterList = new List<ClientCard> { target };
					return true;
				}
			}
			if (base.Bot.HasInExtra(3410461))
			{
				List<ClientCard> targetList = (from c in base.Enemy.GetMonsters()
					where c.IsFaceup() && !c.IsCode(this.cannotBeFusionMaterialIdList) && c.HasRace(CardRace.Dragon)
					orderby c.GetDefensePower() descending
					select c).ToList<ClientCard>();
				if (targetList.Count > 0)
				{
					enemyMonsterList = targetList;
					return true;
				}
			}
			if (base.Bot.HasInExtra(87746184))
			{
				ClientCard target2 = (from c in base.Enemy.GetMonsters()
					where c.IsFaceup() && !c.IsCode(this.cannotBeFusionMaterialIdList) && c.HasAttribute(CardAttribute.Light)
					orderby c.GetDefensePower() descending
					select c).FirstOrDefault<ClientCard>();
				if (target2 != null)
				{
					enemyMonsterList = new List<ClientCard> { target2 };
					return true;
				}
			}
			if (base.Bot.HasInExtra(38811586))
			{
				ClientCard target3 = (from c in base.Enemy.GetMonsters()
					where c.IsFaceup() && !c.IsCode(this.cannotBeFusionMaterialIdList) && c.HasAttribute(CardAttribute.Light) && c.HasRace(CardRace.SpellCaster)
					orderby c.GetDefensePower() descending
					select c).FirstOrDefault<ClientCard>();
				if (target3 != null)
				{
					enemyMonsterList = new List<ClientCard> { target3 };
					return true;
				}
			}
			if (base.Bot.HasInExtra(70534340))
			{
				ClientCard target4 = (from c in base.Enemy.GetMonsters()
					where c.IsFaceup() && !c.IsCode(this.cannotBeFusionMaterialIdList) && c.HasAttribute(CardAttribute.Dark)
					orderby c.GetDefensePower() descending
					select c).FirstOrDefault<ClientCard>();
				if (costHandCount >= 2 && target4 != null)
				{
					enemyMonsterList = new List<ClientCard> { target4 };
					return true;
				}
			}
			if (base.Bot.HasInExtra(92892239))
			{
				ClientCard target5 = (from c in base.Enemy.GetMonsters()
					where c.IsFaceup() && !c.IsCode(this.cannotBeFusionMaterialIdList) && c.HasRace(CardRace.Dragon) && c.HasAttribute(CardAttribute.Dark)
					orderby c.GetDefensePower() descending
					select c).FirstOrDefault<ClientCard>();
				if (target5 != null)
				{
					enemyMonsterList = new List<ClientCard> { target5 };
					return true;
				}
			}
			if (base.Bot.HasInExtra(41373230))
			{
				ClientCard target6 = (from c in base.Enemy.GetMonsters()
					where c.IsFaceup() && !c.IsCode(this.cannotBeFusionMaterialIdList) && c.Attack >= 2500
					orderby c.GetDefensePower() descending
					select c).FirstOrDefault<ClientCard>();
				if (target6 != null)
				{
					enemyMonsterList = new List<ClientCard> { target6 };
					return true;
				}
			}
			if (base.Bot.HasInExtra(51409648))
			{
				ClientCard target7 = (from c in base.Enemy.GetMonsters()
					where c.IsFaceup() && !c.IsCode(this.cannotBeFusionMaterialIdList) && c.HasRace((CardRace)49664)
					orderby c.GetDefensePower() descending
					select c).FirstOrDefault<ClientCard>();
				if (target7 != null)
				{
					enemyMonsterList = new List<ClientCard> { target7 };
					return true;
				}
			}
			if (base.Bot.HasInExtra(1906812))
			{
				ClientCard target8 = (from c in base.Enemy.GetMonsters()
					where c != null && c.IsFaceup() && !c.IsCode(this.cannotBeFusionMaterialIdList) && this.enemyPlaceThisTurn.Contains(c) && c.IsSpecialSummoned && c.GetDefensePower() >= this.Util.GetBestPower(this.Bot, false)
					orderby c.GetDefensePower() descending
					select c).FirstOrDefault<ClientCard>();
				if (target8 != null)
				{
					enemyMonsterList = new List<ClientCard> { target8 };
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x0003FB20 File Offset: 0x0003DD20
		public bool FallenOfAlbazActivate()
		{
			if (base.Bot.HasInExtra(3410461) && base.Enemy.MonsterZone.Any((ClientCard c) => c != null && c.IsFaceup() && c.IsCode(this.dangerousDragonIdList)))
			{
				return false;
			}
			if (this.CheckAlbazFusion(null))
			{
				this.activatedCardIdList.Add(base.Card.Id);
				return true;
			}
			return false;
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x0003FB81 File Offset: 0x0003DD81
		public bool SpringansKittSummon()
		{
			if (this.CheckWhetherNegated(true, true, CardType.Monster) || this.enemyActivateLockBird || this.activatedCardIdList.Contains(base.Card.Id + 1))
			{
				return false;
			}
			this.summoned = true;
			return true;
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x0003FBBC File Offset: 0x0003DDBC
		public bool SpringansKittActivate()
		{
			if (base.Card.Location == CardLocation.Hand)
			{
				if (this.CheckWhetherNegated(true, true, CardType.Monster))
				{
					return false;
				}
				if (this.CheckShouldNoMoreSpSummon())
				{
					if (!this.summoned | this.activatedCardIdList.Contains(44362883) | base.Bot.MonsterZone.Any((ClientCard c) => c != null && c.IsFaceup() && c.HasType((CardType)75505728)))
					{
						return false;
					}
				}
				this.activatedCardIdList.Add(base.Card.Id);
				return true;
			}
			else
			{
				if (base.Card.Location != CardLocation.MonsterZone)
				{
					return false;
				}
				if (this.CheckWhetherNegated(true, true, CardType.Monster))
				{
					return false;
				}
				this.activatedCardIdList.Add(base.Card.Id + 1);
				return true;
			}
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x0003FC88 File Offset: 0x0003DE88
		public bool GuidingQuemTheVirtuousSummon()
		{
			if (this.CheckWhetherNegated(true, true, CardType.Monster) || this.CheckWhetherWillbeRemoved())
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

		// Token: 0x06000D1F RID: 3359 RVA: 0x0003FCC4 File Offset: 0x0003DEC4
		public bool GuidingQuemTheVirtuousSummonForSearch()
		{
			if (this.CheckWhetherNegated(true, true, CardType.Monster) || this.CheckWhetherWillbeRemoved())
			{
				return false;
			}
			if (this.activatedCardIdList.Contains(base.Card.Id))
			{
				return false;
			}
			if (base.Bot.HasInGraveyard(17751597) && this.CheckRemainInDeck(new int[] { 44362883, 18973184, 34995106, 82738008 }) > 0)
			{
				this.summoned = true;
				return true;
			}
			if (base.Bot.HasInGraveyard(new int[] { 44362883, 18973184, 32756828 }) && this.CheckRemainInDeck(17751597) > 0)
			{
				this.summoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x0003FD6C File Offset: 0x0003DF6C
		public bool GuidingQuemTheVirtuousActivate()
		{
			int desc = -1;
			if (base.ActivateDescription >= base.Util.GetStringId(45883110, 0))
			{
				desc = base.ActivateDescription - base.Util.GetStringId(45883110, 0);
			}
			Logger.DebugWriteLine("Guiding desc: " + desc.ToString());
			Logger.DebugWriteLine("Guiding timing: " + base.CurrentTiming.ToString());
			Logger.DebugWriteLine("Guiding flag: " + this.quemSummonFlag.ToString());
			if ((base.ActivateDescription == -1 && this.quemSummonFlag == 0) || base.ActivateDescription == base.Util.GetStringId(45883110, 1))
			{
				if (this.CheckWhetherNegated(true, true, CardType.Monster))
				{
					return false;
				}
				List<KeyValuePair<int, Func<ClientCard, bool>>> list = new List<KeyValuePair<int, Func<ClientCard, bool>>>();
				list.Add(new KeyValuePair<int, Func<ClientCard, bool>>(38811586, (ClientCard c) => c.IsCanRevive() && !this.activatedCardIdList.Contains(38811586)));
				list.Add(new KeyValuePair<int, Func<ClientCard, bool>>(44146295, (ClientCard c) => c.IsCanRevive()));
				list.Add(new KeyValuePair<int, Func<ClientCard, bool>>(68468459, delegate(ClientCard c)
				{
					List<ClientCard> materialList;
					if (this.CheckAlbazFusion(null, out materialList) && !base.Util.ChainContainsCard(new int[] { 87746184, 70534340 }) && !this.spSummoningAlbaz)
					{
						bool albazFlag = materialList.Count > 1;
						if (materialList.Count > 0)
						{
							ClientCard material = materialList[0];
							albazFlag |= material.HasType((CardType)75505856);
							albazFlag |= material.IsFloodgate() || material.IsOneForXyz() || base.Util.GetWorstBotMonster(false).GetDefensePower() < material.Attack;
						}
						return albazFlag;
					}
					return false;
				}));
				list.Add(new KeyValuePair<int, Func<ClientCard, bool>>(87746184, delegate(ClientCard c)
				{
					if (c.IsCanRevive() && base.Bot.HasInSpellZone(32756828, false, false))
					{
						return base.Bot.MonsterZone.Any((ClientCard oc) => oc != null && oc.IsFaceup() && oc.HasSetcode(392));
					}
					return false;
				}));
				list.Add(new KeyValuePair<int, Func<ClientCard, bool>>(95515789, (ClientCard c) => base.Duel.Player == 0 || !this.activatedCardIdList.Contains(95515790)));
				list.Add(new KeyValuePair<int, Func<ClientCard, bool>>(19096726, (ClientCard c) => base.Bot.MonsterZone.Any((ClientCard oc) => oc != null && oc.IsFaceup() && oc.IsCode(this.albazFusionMonster))));
				list.Add(new KeyValuePair<int, Func<ClientCard, bool>>(38811586, (ClientCard c) => c.IsCanRevive()));
				list.Add(new KeyValuePair<int, Func<ClientCard, bool>>(45484331, (ClientCard c) => true));
				using (List<KeyValuePair<int, Func<ClientCard, bool>>>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<int, Func<ClientCard, bool>> pair = enumerator.Current;
						ClientCard target = base.Bot.Graveyard.FirstOrDefault((ClientCard c) => c != null && c.IsOriginalCode(pair.Key) && pair.Value(c));
						if (target != null)
						{
							if (target.IsOriginalCode(68468459))
							{
								this.spSummoningAlbaz = true;
							}
							base.AI.SelectCard(target);
							this.activatedCardIdList.Add(base.Card.Id + 1);
							return true;
						}
					}
				}
			}
			if ((base.ActivateDescription != -1 || this.quemSummonFlag <= 0) && base.ActivateDescription != base.Util.GetStringId(45883110, 0))
			{
				return false;
			}
			if (this.CheckWhetherNegated(true, true, CardType.Monster) || this.CheckWhetherWillbeRemoved())
			{
				return false;
			}
			this.quemSummonFlag = 0;
			this.activatedCardIdList.Add(base.Card.Id);
			return true;
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x0004005C File Offset: 0x0003E25C
		public bool BlazingCartesiaTheVirtuousSummon()
		{
			if (this.CheckWhetherNegated(true, true, CardType.Monster))
			{
				return false;
			}
			bool checkFlag = base.Bot.HasInHandOrInSpellZone(36637374) && !this.activatedCardIdList.Contains(62962630) && this.CheckRemainInDeck(62962630) > 0;
			checkFlag |= base.Bot.HasInHand(25451383) && !this.activatedCardIdList.Contains(25451383);
			checkFlag |= base.Bot.HasInHandOrHasInMonstersZone(60242223) && !this.activatedCardIdList.Contains(60242224);
			if (base.Bot.HasInExtra(24915933))
			{
				bool hasMaterial = base.Bot.Hand.Any((ClientCard c) => c != base.Card && c.Attack < 2000 && c.HasAttribute((CardAttribute)48));
				hasMaterial |= base.Bot.MonsterZone.Any((ClientCard c) => c != null && !c.IsCode(this.cannotBeFusionMaterialIdList) && c.Attack < 2000 && c.HasAttribute((CardAttribute)48));
				checkFlag = checkFlag || hasMaterial;
			}
			if (checkFlag)
			{
				this.summoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x00040164 File Offset: 0x0003E364
		public bool BlazingCartesiaTheVirtuousActivate()
		{
			if (base.Card.Location != CardLocation.Hand)
			{
				if (base.Card.Location == CardLocation.MonsterZone)
				{
					if (this.CheckWhetherNegated(true, true, CardType.Monster))
					{
						return false;
					}
					if (base.Duel.CurrentChain.Any((ClientCard c) => c != null && c.Controller == 0 && c.IsCode(82738008)))
					{
						return false;
					}
					List<ClientCard> materialList = base.Bot.MonsterZone.Where((ClientCard c) => c != null && c.Attack <= 2500 && !c.IsCode(this.cannotBeFusionMaterialIdList)).ToList<ClientCard>();
					materialList.AddRange(base.Bot.Hand.Where((ClientCard c) => c.IsMonster() && (!this.CheckWhetherCanSummon() || ((this.activatedCardIdList.Contains(62962630) || !c.IsCode(62962630)) && (this.activatedCardIdList.Contains(45484331) || !c.IsCode(45484331))))));
					ClientCard lastCahinCard = base.Util.GetLastChainCard();
					if (lastCahinCard != null && base.Duel.LastChainPlayer == 1)
					{
						List<ClientCard> chainTargetList = base.Duel.LastChainTargets.Where((ClientCard c) => c.Controller == 0 && c.Location == CardLocation.MonsterZone && (!c.IsCode(this.cannotBeFusionMaterialIdList) || c.Attack <= 2500)).ToList<ClientCard>();
						if (chainTargetList.Count > 0)
						{
							if (lastCahinCard.IsCode(this.targetNegateIdList))
							{
								chainTargetList = chainTargetList.Where((ClientCard c) => c.Attack <= 2500).ToList<ClientCard>();
							}
							ClientCard _fusionTarget;
							List<ClientCard> usedMaterialList;
							this.BlazingCartesiaTheVirtuousFusionCheck(base.Bot.ExtraDeck, 0, materialList, chainTargetList, out _fusionTarget, out usedMaterialList);
							if (_fusionTarget != null)
							{
								Logger.DebugWriteLine("cartesia prepare fusion1: " + _fusionTarget.Name);
								this.cartesiaMaterialList.AddRange(usedMaterialList.Intersect(chainTargetList));
								this.activatedCardIdList.Add(base.Card.Id + 1);
								return true;
							}
						}
					}
					if (!((this.CheckWhetherCanSummon() && !this.activatedCardIdList.Contains(62962630) && base.Bot.HasInHand(62962630)) | (this.CheckWhetherCanSummon() && !this.activatedCardIdList.Contains(45484331) && base.Bot.HasInHand(45484331))))
					{
						ClientCard shrouded = base.Duel.CurrentChain.FirstOrDefault((ClientCard c) => c.Controller == 0 && c.Location == CardLocation.Hand && c.IsOriginalCode(25451383));
						if (shrouded != null)
						{
							ClientCard _fusionTarget2;
							List<ClientCard> usedMaterialList2;
							this.BlazingCartesiaTheVirtuousFusionCheck(base.Bot.ExtraDeck, 0, materialList, new List<ClientCard> { shrouded }, out _fusionTarget2, out usedMaterialList2);
							if (_fusionTarget2 != null)
							{
								Logger.DebugWriteLine("cartesia prepare fusion2: " + _fusionTarget2.Name);
								this.cartesiaMaterialList.AddRange(usedMaterialList2.Intersect(new List<ClientCard> { shrouded }));
								this.activatedCardIdList.Add(base.Card.Id + 1);
								return true;
							}
						}
					}
					bool shouldActivateFlag = (base.Duel.Player == 0 && !this.CheckShouldNoMoreSpSummon()) || base.Duel.Player == 1;
					if (!base.Bot.HasInMonstersZone(44146295, false, false, true) && !base.Bot.HasInSpellZone(44146295, false, true) && shouldActivateFlag)
					{
						ClientCard _fusionTarget3;
						List<ClientCard> list;
						this.BlazingCartesiaTheVirtuousFusionCheck(base.Bot.ExtraDeck, 44146295, materialList, null, out _fusionTarget3, out list);
						if (_fusionTarget3 != null)
						{
							Logger.DebugWriteLine("cartesia prepare fusion3: " + _fusionTarget3.Name);
							this.cartesiaSummonGoal = 44146295;
							this.activatedCardIdList.Add(base.Card.Id + 1);
							return true;
						}
					}
					if (shouldActivateFlag && base.Duel.Player == 0)
					{
						using (Dictionary<int, Func<bool>>.Enumerator enumerator = new Dictionary<int, Func<bool>>
						{
							{
								60242223,
								() => !this.activatedCardIdList.Contains(60242224) && !base.DefaultCheckWhetherCardIdIsNegated(60242223)
							},
							{
								36577931,
								() => !this.activatedCardIdList.Contains(36577931) && !base.DefaultCheckWhetherCardIdIsNegated(36577931)
							}
						}.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								KeyValuePair<int, Func<bool>> pair = enumerator.Current;
								ClientCard targetMaterial = materialList.FirstOrDefault((ClientCard c) => c.IsCode(pair.Key));
								if (targetMaterial != null && pair.Value())
								{
									ClientCard _fusionTarget4;
									List<ClientCard> usedMaterialList3;
									this.BlazingCartesiaTheVirtuousFusionCheck(base.Bot.ExtraDeck, 24915933, materialList, new List<ClientCard> { targetMaterial }, out _fusionTarget4, out usedMaterialList3);
									if (_fusionTarget4 != null)
									{
										Logger.DebugWriteLine("cartesia prepare fusion4: " + _fusionTarget4.Name);
										this.cartesiaSummonGoal = 24915933;
										this.cartesiaMaterialList.Add(targetMaterial);
										this.activatedCardIdList.Add(base.Card.Id + 1);
										return true;
									}
								}
							}
						}
					}
					if (shouldActivateFlag)
					{
						ClientCard _fusionTarget5;
						List<ClientCard> usedMaterialList4;
						this.BlazingCartesiaTheVirtuousFusionCheck(base.Bot.ExtraDeck, 38811586, materialList, new List<ClientCard> { base.Card }, out _fusionTarget5, out usedMaterialList4);
						if (_fusionTarget5 != null)
						{
							Logger.DebugWriteLine("cartesia prepare fusion5: " + _fusionTarget5.Name);
							this.cartesiaSummonGoal = 38811586;
							this.cartesiaMaterialList.Add(base.Card);
							this.activatedCardIdList.Add(base.Card.Id + 1);
							return true;
						}
					}
					if (shouldActivateFlag && this.GetProblematicEnemyMonster(0, true, true, CardType.Monster) != null)
					{
						List<ClientCard> list;
						ClientCard _fusionTarget6;
						this.BlazingCartesiaTheVirtuousFusionCheck(base.Bot.ExtraDeck, 92892239, materialList, null, out _fusionTarget6, out list);
						if (_fusionTarget6 != null)
						{
							Logger.DebugWriteLine("cartesia prepare fusion6: " + _fusionTarget6.Name);
							this.cartesiaSummonGoal = 92892239;
							this.activatedCardIdList.Add(base.Card.Id + 1);
							return true;
						}
					}
					if (shouldActivateFlag && ((base.Duel.Player == 0 && base.CurrentTiming == -1) | (base.Duel.Player == 1 && (base.CurrentTiming & 4) != 0)))
					{
						List<ClientCard> list;
						ClientCard _fusionTarget7;
						this.BlazingCartesiaTheVirtuousFusionCheck(base.Bot.ExtraDeck, 0, materialList, null, out _fusionTarget7, out list);
						if (_fusionTarget7 != null)
						{
							Logger.DebugWriteLine("cartesia prepare fusion7: " + _fusionTarget7.Name);
							this.activatedCardIdList.Add(base.Card.Id + 1);
							return true;
						}
					}
				}
				return false;
			}
			if (this.CheckShouldNoMoreSpSummon() || this.CheckWhetherNegated(true, true, CardType.Monster))
			{
				return false;
			}
			this.activatedCardIdList.Add(base.Card.Id);
			return true;
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x000407A0 File Offset: 0x0003E9A0
		public bool BlazingCartesiaTheVirtuousActivateInGrave()
		{
			if (base.Card.Location != CardLocation.Grave)
			{
				return false;
			}
			if (this.CheckWhetherNegated(true, false, CardType.Monster))
			{
				return false;
			}
			this.activatedCardIdList.Add(base.Card.Id + 2);
			return true;
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x000407DC File Offset: 0x0003E9DC
		public void BlazingCartesiaTheVirtuousFusionCheck(IList<ClientCard> canSummonList, int mustSummonId, List<ClientCard> materialList, List<ClientCard> mustMaterialList, out ClientCard fusionTarget, out List<ClientCard> selectedFusionMaterialList)
		{
			fusionTarget = null;
			selectedFusionMaterialList = new List<ClientCard>();
			Dictionary<int, List<Func<ClientCard, bool>>> dictionary = new Dictionary<int, List<Func<ClientCard, bool>>>();
			int num = 24915933;
			List<Func<ClientCard, bool>> list = new List<Func<ClientCard, bool>>();
			list.Add((ClientCard c) => c.IsCode(95515789));
			list.Add((ClientCard c) => !c.IsCode(this.cannotBeFusionMaterialIdList) && c.HasAttribute((CardAttribute)48));
			dictionary.Add(num, list);
			int num2 = 38811586;
			List<Func<ClientCard, bool>> list2 = new List<Func<ClientCard, bool>>();
			list2.Add((ClientCard c) => c.IsCode(68468459));
			list2.Add((ClientCard c) => !c.IsCode(this.cannotBeFusionMaterialIdList) && c.HasAttribute(CardAttribute.Light) && c.HasRace(CardRace.SpellCaster));
			dictionary.Add(num2, list2);
			int num3 = 44146295;
			List<Func<ClientCard, bool>> list3 = new List<Func<ClientCard, bool>>();
			list3.Add((ClientCard c) => c.IsCode(68468459));
			list3.Add((ClientCard c) => !c.IsCode(this.cannotBeFusionMaterialIdList) && c.HasType((CardType)75505728));
			dictionary.Add(num3, list3);
			int num4 = 87746184;
			List<Func<ClientCard, bool>> list4 = new List<Func<ClientCard, bool>>();
			list4.Add((ClientCard c) => c.IsCode(68468459));
			list4.Add((ClientCard c) => !c.IsCode(this.cannotBeFusionMaterialIdList) && c.HasAttribute(CardAttribute.Light));
			dictionary.Add(num4, list4);
			int num5 = 70534340;
			List<Func<ClientCard, bool>> list5 = new List<Func<ClientCard, bool>>();
			list5.Add((ClientCard c) => c.IsCode(68468459));
			list5.Add((ClientCard c) => !c.IsCode(this.cannotBeFusionMaterialIdList) && c.HasAttribute(CardAttribute.Dark));
			dictionary.Add(num5, list5);
			int num6 = 72272462;
			List<Func<ClientCard, bool>> list6 = new List<Func<ClientCard, bool>>();
			list6.Add((ClientCard c) => c.HasSetcode(356));
			list6.Add((ClientCard c) => !c.IsCode(this.cannotBeFusionMaterialIdList) && c.HasAttribute((CardAttribute)48));
			dictionary.Add(num6, list6);
			dictionary.Add(92892239, new List<Func<ClientCard, bool>>
			{
				(ClientCard c) => !c.IsCode(this.cannotBeFusionMaterialIdList) && c.HasAttribute(CardAttribute.Dark) && c.HasRace(CardRace.Dragon),
				(ClientCard c) => !c.IsCode(this.cannotBeFusionMaterialIdList) && c.HasAttribute(CardAttribute.Dark) && c.HasRace(CardRace.Dragon)
			});
			Dictionary<int, Func<ClientCard, ClientCard, bool>> extraCheckDict = new Dictionary<int, Func<ClientCard, ClientCard, bool>>
			{
				{
					38811586,
					delegate(ClientCard c1, ClientCard c2)
					{
						int reviveCount = base.Bot.Graveyard.Count((ClientCard c) => c != null && c.IsMonster() && c.IsCanRevive());
						reviveCount += base.Enemy.Graveyard.Count((ClientCard c) => c != null && c.IsMonster() && c.IsCanRevive());
						if (!this.CheckWhetherWillbeRemoved() || ((base.CurrentTiming & 4) > 0 && base.Util.GetOneEnemyBetterThanValue(base.Card.GetDefensePower(), false, false) != null && base.Util.GetOneEnemyBetterThanValue(3000, false, false) == null))
						{
							reviveCount += 2;
						}
						return reviveCount >= 2;
					}
				},
				{
					70534340,
					(ClientCard c1, ClientCard c2) => base.Bot.Hand.Count((ClientCard c) => c != c1 && c != c2) > 0
				},
				{
					44146295,
					(ClientCard c1, ClientCard c2) => !this.CheckWhetherWillbeRemoved() && !base.Bot.HasInMonstersZone(44146295, false, false, true) && !base.Bot.HasInSpellZone(44146295, false, true)
				}
			};
			using (Dictionary<int, List<Func<ClientCard, bool>>>.Enumerator enumerator = dictionary.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, List<Func<ClientCard, bool>>> pair = enumerator.Current;
					if (mustSummonId <= 0 || mustSummonId == pair.Key)
					{
						ClientCard currentFusionTarget = canSummonList.FirstOrDefault((ClientCard c) => c != null && c.IsCode(pair.Key));
						if (currentFusionTarget != null)
						{
							Func<ClientCard, bool> fusionFunc = pair.Value[0];
							Func<ClientCard, bool> fusionFunc2 = pair.Value[1];
							if (mustMaterialList != null && mustMaterialList.Count > 0)
							{
								using (List<ClientCard>.Enumerator enumerator2 = mustMaterialList.GetEnumerator())
								{
									while (enumerator2.MoveNext())
									{
										ClientCard mustMaterial = enumerator2.Current;
										if (fusionFunc(mustMaterial) || fusionFunc2(mustMaterial))
										{
											foreach (ClientCard anotherMaterial in materialList)
											{
												if (anotherMaterial != mustMaterial)
												{
													bool flag = (fusionFunc(mustMaterial) && fusionFunc2(anotherMaterial)) | (fusionFunc2(mustMaterial) && fusionFunc(anotherMaterial));
													Func<ClientCard, ClientCard, bool> extraCheckFunc;
													extraCheckDict.TryGetValue(pair.Key, out extraCheckFunc);
													if (flag & (extraCheckFunc == null || extraCheckFunc(mustMaterial, anotherMaterial)))
													{
														fusionTarget = currentFusionTarget;
														selectedFusionMaterialList.Add(mustMaterial);
														selectedFusionMaterialList.Add(anotherMaterial);
														return;
													}
												}
											}
										}
									}
									continue;
								}
							}
							for (int index = 0; index < materialList.Count - 1; index++)
							{
								ClientCard material = materialList[index];
								if (fusionFunc(material) || fusionFunc2(material))
								{
									for (int index2 = index + 1; index2 < materialList.Count; index2++)
									{
										ClientCard material2 = materialList[index2];
										bool flag2 = (fusionFunc(material) && fusionFunc2(material2)) | (fusionFunc2(material) && fusionFunc(material2));
										Func<ClientCard, ClientCard, bool> extraCheckFunc2;
										extraCheckDict.TryGetValue(pair.Key, out extraCheckFunc2);
										if (flag2 & (extraCheckFunc2 == null || extraCheckFunc2(material, material2)))
										{
											fusionTarget = currentFusionTarget;
											this.selectedFusionMaterial.Add(material);
											this.selectedFusionMaterial.Add(material2);
											return;
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x00040CF0 File Offset: 0x0003EEF0
		public bool TriBrigadeMercourierActivate()
		{
			if (base.Card.Location != CardLocation.Hand && base.Card.Location != CardLocation.MonsterZone)
			{
				CardLocation location = base.Card.Location;
				return false;
			}
			if (this.CheckWhetherNegated(true, false, CardType.Monster) || !this.CheckLastChainShouldNegated())
			{
				return false;
			}
			if (base.Util.GetLastChainCard().Location == CardLocation.MonsterZone)
			{
				this.currentNegateCardList.Add(base.Util.GetLastChainCard());
			}
			this.activatedCardIdList.Add(base.Card.Id);
			return true;
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x00040D80 File Offset: 0x0003EF80
		public bool TriBrigadeMercourierActivateForSearch()
		{
			if (base.Card.Location == CardLocation.Hand || base.Card.Location == CardLocation.MonsterZone)
			{
				return false;
			}
			if (base.Card.Location != CardLocation.Removed)
			{
				return false;
			}
			if (this.CheckWhetherNegated(true, false, CardType.Monster))
			{
				return false;
			}
			this.activatedCardIdList.Add(base.Card.Id + 1);
			return true;
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x00040DE4 File Offset: 0x0003EFE4
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
				ClientCard lastChainCard = base.Util.GetLastChainCard();
				if (lastChainCard.Location == CardLocation.MonsterZone || lastChainCard.Location == CardLocation.SpellZone)
				{
					this.currentNegateCardList.Add(base.Util.GetLastChainCard());
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x00040E5E File Offset: 0x0003F05E
		public bool MaxxCActivate()
		{
			return !this.CheckWhetherNegated(true, false, (CardType)0) && base.Duel.LastChainPlayer != 0 && base.DefaultMaxxC();
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x00040E80 File Offset: 0x0003F080
		public bool DespianTragedyActivate()
		{
			if (base.ActivateDescription != base.Util.GetStringId(base.Card.Id, 1))
			{
				if (this.CheckWhetherNegated(true, false, CardType.Monster))
				{
					return false;
				}
				this.activatedCardIdList.Add(base.Card.Id);
				return true;
			}
			else
			{
				if (this.CheckWhetherNegated(true, false, CardType.Trap))
				{
					return false;
				}
				using (Dictionary<int, Func<bool>>.Enumerator enumerator = new Dictionary<int, Func<bool>>
				{
					{
						44362883,
						() => this.BrandedFusionActivateCheck(true)
					},
					{
						18973184,
						() => (base.Duel.Player != 0 || base.Duel.Phase < DuelPhase.End) && ((base.Bot.HasInHandOrInSpellZone(44362883) && this.BrandedFusionActivateCheck(true)) || (base.Bot.HasInHandOrInSpellZone(34995106) && this.BrandedInWhiteActivateCheck()) || (base.Bot.HasInHandOrInSpellZone(82738008) && this.BrandedInRedActivateCheck(false) != null) || (!this.summoned && base.Bot.HasInHand(68468459) && this.CheckAlbazFusion(null)) || (base.Bot.HasInMonstersZone(95515789, false, false, false) || (!this.summoned && base.Bot.HasInHand(95515789))))
					},
					{
						29948294,
						new Func<bool>(this.BrandedInHighSpiritsActivateCheck)
					},
					{
						82738008,
						() => this.BrandedInRedActivateCheck(false) != null
					},
					{
						34995106,
						new Func<bool>(this.BrandedInWhiteActivateCheck)
					},
					{
						19271881,
						() => base.Bot.GetMonsters().Any((ClientCard c) => c.IsFaceup() && c.IsCode(this.albazFusionMonster))
					},
					{
						36637374,
						() => base.Bot.Hand.Count > 2 && !this.activatedCardIdList.Contains(36637374)
					}
				}.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<int, Func<bool>> pair = enumerator.Current;
						ClientCard target = base.Bot.Graveyard.FirstOrDefault((ClientCard card) => card.IsCode(pair.Key));
						if (target != null && pair.Value())
						{
							this.activatedCardIdList.Add(base.Card.Id);
							base.AI.SelectCard(target);
							this.SelectSTPlace(target, true, null);
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x0004102C File Offset: 0x0003F22C
		public bool DespianTragedySet()
		{
			if (base.Bot.Graveyard.Any((ClientCard c) => c != null && c.HasType((CardType)6) && c.HasSetcode(349)))
			{
				this.summoned = true;
				return true;
			}
			return false;
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x0004106C File Offset: 0x0003F26C
		public bool NadirServantActivate()
		{
			if (this.CheckWhetherNegated(true, true, CardType.Spell) || this.CheckWhetherWillbeRemoved())
			{
				return false;
			}
			ClientCard clientCard;
			if (this.NadirServantActivateCheck(null, false, out clientCard))
			{
				this.SelectSTPlace(base.Card, true, null);
				return true;
			}
			return false;
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x000410AC File Offset: 0x0003F2AC
		public bool NadirServantActivateCheck(IList<ClientCard> cards, bool force, out ClientCard target)
		{
			using (Dictionary<int, Func<bool>>.Enumerator enumerator = new Dictionary<int, Func<bool>>
			{
				{
					87746184,
					() => !this.sendToGYThisTurn.Any((ClientCard c) => c.IsCode(87746184))
				},
				{
					53971455,
					() => this.CheckRemainInDeck(new int[] { 95515789, 45883110 }) > 0
				},
				{
					41373230,
					() => this.CheckRemainInDeck(45883110) > 0
				},
				{
					1906812,
					() => this.CheckRemainInDeck(45484331) > 0
				},
				{
					51409648,
					() => this.Bot.Graveyard.Any((ClientCard c) => c != null && c.IsOriginalCode(68468459))
				},
				{
					3410461,
					() => force && this.CheckRemainInDeck(new int[] { 6498706, 44362883 }) > 0
				},
				{
					24915933,
					() => force
				}
			}.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, Func<bool>> pair = enumerator.Current;
					if (cards == null)
					{
						if (base.Bot.HasInExtra(pair.Key) && pair.Value())
						{
							target = null;
							return true;
						}
					}
					else
					{
						ClientCard tg = cards.FirstOrDefault((ClientCard c) => c.IsOriginalCode(pair.Key));
						if (tg != null && pair.Value())
						{
							target = tg;
							return true;
						}
					}
				}
			}
			target = null;
			return false;
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x00041228 File Offset: 0x0003F428
		public bool FusionDeploymentActivate()
		{
			if (this.CheckWhetherNegated(true, true, CardType.Spell))
			{
				return false;
			}
			if (this.FusionDeploymentSpSummonTarget() > 0 && !base.Bot.HasInHand(18973184))
			{
				this.SelectSTPlace(base.Card, true, null);
				return true;
			}
			return false;
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x00041264 File Offset: 0x0003F464
		public int FusionDeploymentSpSummonTarget()
		{
			if (this.CheckRemainInDeck(68468459) > 0 && this.CheckAlbazFusion(base.Card) && this.GetProblematicEnemyMonster(0, false, false, CardType.Monster) != null)
			{
				return 68468459;
			}
			if (this.CheckRemainInDeck(95515789) > 0 && base.Bot.HasInExtra(24915933))
			{
				if (base.Bot.Hand.Any((ClientCard c) => c.IsMonster() && c.HasAttribute((CardAttribute)48)) || base.Bot.GetMonsters().Any((ClientCard c) => c.IsMonster() && c.HasAttribute((CardAttribute)48) && !c.IsCode(this.cannotBeFusionMaterialIdList)))
				{
					return 95515789;
				}
			}
			if (this.CheckRemainInDeck(68468459) > 0 && this.CheckAlbazFusion(base.Card))
			{
				return 68468459;
			}
			return 0;
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x00041338 File Offset: 0x0003F538
		public bool BrandedInWhiteActivate()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				if (this.CheckWhetherNegated(true, false, CardType.Spell))
				{
					return false;
				}
				this.activatedCardIdList.Add(base.Card.Id + 1);
				this.SelectSTPlace(base.Card, false, null);
				return true;
			}
			else
			{
				if (this._BrandedInWhiteActivateCheck(true))
				{
					this.activatedCardIdList.Add(base.Card.Id);
					this.SelectSTPlace(base.Card, true, null);
					return true;
				}
				return false;
			}
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x000413B9 File Offset: 0x0003F5B9
		public bool BrandedInWhiteActivateCheck()
		{
			return this._BrandedInWhiteActivateCheck(false);
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x000413C4 File Offset: 0x0003F5C4
		public bool _BrandedInWhiteActivateCheck(bool activate = false)
		{
			if (this.CheckWhetherNegated(true, true, CardType.Spell) || this.activatedCardIdList.Contains(34995106) || this.nadirActivated)
			{
				return false;
			}
			if (this.CheckShouldNoMoreSpSummon())
			{
				if (base.Bot.MonsterZone.Any((ClientCard c) => c != null && c.GetDefensePower() >= 2000))
				{
					return false;
				}
			}
			ClientCard _fusionTarget;
			if (this.BrandedInWhiteFusionTarget(base.Bot.ExtraDeck, out _fusionTarget) > 0)
			{
				if (activate)
				{
					Logger.DebugWriteLine("White prepare fusion: " + ((_fusionTarget != null) ? _fusionTarget.Name : null));
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x0004146C File Offset: 0x0003F66C
		public int BrandedInWhiteFusionTarget(IList<ClientCard> cards, out ClientCard target)
		{
			target = null;
			using (Dictionary<int, Func<bool>>.Enumerator enumerator = new Dictionary<int, Func<bool>>
			{
				{
					44146295,
					delegate
					{
						if (base.Bot.HasInMonstersZone(44146295, false, false, true) || base.Bot.HasInSpellZone(44146295, false, true))
						{
							return false;
						}
						return (base.Bot.Graveyard.Any((ClientCard c) => c.IsCode(68468459)) | base.Bot.MonsterZone.Any((ClientCard c) => c != null && c.IsOriginalCode(68468459)) | base.Bot.Hand.Any((ClientCard c) => c.IsOriginalCode(68468459))) && (base.Bot.Graveyard.Any((ClientCard c) => c != null && !this.sendToGYThisTurn.Contains(c) && !c.IsCode(this.cannotBeFusionMaterialIdList) && c.HasType((CardType)75505728)) | base.Bot.MonsterZone.Any((ClientCard c) => c != null && !c.IsCode(this.cannotBeFusionMaterialIdList) && (c.IsCode(this.albazFusionMonster) || c.IsCode(24915933))));
					}
				},
				{
					92892239,
					delegate
					{
						if (base.Enemy.GetMonsterCount() + base.Enemy.GetSpellCount() > 0)
						{
							List<ClientCard> darkDragonList = base.Bot.Hand.Where((ClientCard c) => c != null && c.IsMonster() && c.HasAttribute(CardAttribute.Dark) && c.HasRace(CardRace.Dragon)).ToList<ClientCard>();
							darkDragonList.AddRange(base.Bot.MonsterZone.Where((ClientCard c) => c != null && c.IsMonster() && c.HasAttribute(CardAttribute.Dark) && c.HasRace(CardRace.Dragon) && !c.IsCode(this.cannotBeFusionMaterialIdList)).ToList<ClientCard>());
							List<ClientCard> graveDarkDragonList = base.Bot.Graveyard.Where((ClientCard c) => c.HasRace(CardRace.Dragon) && c.HasAttribute(CardAttribute.Dark) && !c.IsCode(this.cannotBeFusionMaterialIdList) && !this.CheckWhetherShouldKeepInGrave(c)).ToList<ClientCard>();
							bool flag;
							if (!darkDragonList.Any((ClientCard c) => c.IsCode(68468459)))
							{
								flag = graveDarkDragonList.Any((ClientCard c) => c.IsCode(68468459));
							}
							else
							{
								flag = true;
							}
							int darkDragonCount = darkDragonList.Count;
							if (flag)
							{
								darkDragonCount += graveDarkDragonList.Count;
							}
							return darkDragonCount >= 2;
						}
						return false;
					}
				},
				{
					11321089,
					() => !this.CheckWhetherNegated(true, true, CardType.Monster) && !base.DefaultCheckWhetherCardIdIsNegated(11321089) && base.Enemy.GetMonsterCount() + base.Enemy.GetSpellCount() != 0 && this.ChimeraFusionMaterialList(true).Count > 0
				},
				{
					70534340,
					delegate
					{
						if (this.activatedCardIdList.Contains(70534340) || base.DefaultCheckWhetherCardIdIsNegated(70534340) || this.CheckWhetherNegated(true, true, CardType.Monster))
						{
							return false;
						}
						List<ClientCard> checkMaterialList = new List<ClientCard>(from c in base.Bot.Graveyard
							where c != null && c.IsMonster()
							orderby c.GetDefensePower()
							select c).ToList<ClientCard>();
						checkMaterialList.AddRange(from c in base.Bot.GetMonsters()
							orderby c.GetDefensePower()
							select c);
						checkMaterialList.AddRange(base.Bot.Hand);
						ClientCard albaz = (from c in checkMaterialList
							where c.IsCode(68468459)
							orderby c.GetDefensePower()
							select c).FirstOrDefault<ClientCard>();
						ClientCard darkMonster = checkMaterialList.Where((ClientCard c) => c != albaz && c.HasAttribute(CardAttribute.Dark)).FirstOrDefault<ClientCard>();
						return albaz != null && darkMonster != null && base.Bot.Hand.Count((ClientCard c) => c != albaz && c != darkMonster && !c.IsCode(34995106)) != 0;
					}
				},
				{
					38811586,
					delegate
					{
						List<ClientCard> checkMaterialList2 = new List<ClientCard>(from c in base.Bot.Graveyard
							where c != null && c.IsMonster()
							orderby c.GetDefensePower()
							select c).ToList<ClientCard>();
						checkMaterialList2.AddRange(from c in base.Bot.GetMonsters()
							orderby c.GetDefensePower()
							select c);
						checkMaterialList2.AddRange(base.Bot.Hand);
						ClientCard albaz = checkMaterialList2.FirstOrDefault((ClientCard c) => c.IsCode(68468459));
						ClientCard lightSpellcaster = checkMaterialList2.FirstOrDefault((ClientCard c) => c.HasRace(CardRace.SpellCaster) && c.HasAttribute(CardAttribute.Light));
						return albaz != null && lightSpellcaster != null && base.Enemy.GetGraveyardMonsters().Count + base.Bot.Graveyard.Where((ClientCard c) => c.IsMonster() && c != albaz && c != lightSpellcaster).Count<ClientCard>() + (base.Bot.HasInHand(23434538) ? 1 : 0) >= 2;
					}
				},
				{
					51409648,
					() => base.Bot.HasInGraveyard(19096726) && (base.Bot.HasInHandOrHasInMonstersZone(68468459) | base.Bot.HasInGraveyard(68468459))
				},
				{
					72272462,
					delegate
					{
						if (base.Enemy.MonsterZone.Any((ClientCard c) => c != null && c.IsFaceup() && c.Attack >= 2500 && (!c.HasType(CardType.Fusion) || c.Level < 8)))
						{
							if ((from c in base.Bot.Graveyard
								where c != null && c.HasSetcode(356) && !this.CheckWhetherShouldKeepInGrave(c)
								orderby c.GetDefensePower()
								select c).FirstOrDefault<ClientCard>() != null)
							{
								return base.Bot.HasInHandOrHasInMonstersZone(68468459) | base.Bot.HasInGraveyard(68468459);
							}
							List<ClientCard> fusionMaterialList = (from c in base.Bot.Hand
								where c.IsMonster()
								orderby c.GetDefensePower()
								select c).ToList<ClientCard>();
							fusionMaterialList.AddRange((from c in base.Bot.MonsterZone
								where c != null && !c.IsCode(this.cannotBeFusionMaterialIdList)
								orderby c.GetDefensePower()
								select c).ToList<ClientCard>());
							ClientCard despian = fusionMaterialList.FirstOrDefault((ClientCard c) => c.HasSetcode(356));
							if (despian != null)
							{
								return fusionMaterialList.Any((ClientCard c) => c != despian && c.HasAttribute((CardAttribute)48)) | base.Bot.HasInGraveyard(68468459);
							}
						}
						return false;
					}
				},
				{
					41373230,
					delegate
					{
						List<ClientCard> list = new List<ClientCard>(from c in base.Bot.Graveyard
							where c != null && c.IsMonster()
							orderby c.GetDefensePower()
							select c).ToList<ClientCard>();
						list.AddRange(from c in base.Bot.GetMonsters()
							orderby c.GetDefensePower()
							select c);
						list.AddRange(base.Bot.Hand);
						ClientCard albaz3 = (from c in list
							where c.IsCode(68468459)
							orderby c.GetDefensePower()
							select c).FirstOrDefault<ClientCard>();
						foreach (ClientCard material in list)
						{
							if (material != albaz3 && material.IsMonster() && material.Attack >= 2500 && !material.IsCode(this.cannotBeFusionMaterialIdList) && !base.Util.IsTurn1OrMain2())
							{
								bool flag2 = base.Enemy.GetMonsterCount() == 0 && !this.CheckWhetherShouldKeepInGrave(material) && (material.IsFacedown() || material.Location != CardLocation.MonsterZone);
								int expectedAttack = 2900 + material.Level * 100;
								int botBestPower = base.Util.GetBestPower(base.Bot, false);
								int beforeBetterCount = base.Enemy.MonsterZone.Count((ClientCard c) => c != null && c.GetDefensePower() >= botBestPower);
								int afterBetterCount = base.Enemy.MonsterZone.Count((ClientCard c) => c != null && c.GetDefensePower() >= expectedAttack);
								return flag2 | (afterBetterCount < beforeBetterCount);
							}
						}
						return false;
					}
				},
				{
					3410461,
					delegate
					{
						if (base.Util.GetOneEnemyBetterThanMyBest(false, false) == null && base.Duel.MainPhase.CanBattlePhase)
						{
							ClientCard albaz4 = base.Bot.Graveyard.FirstOrDefault((ClientCard c) => c != null && c.IsOriginalCode(68468459));
							if (albaz4 == null)
							{
								albaz4 = base.Bot.Graveyard.FirstOrDefault((ClientCard c) => c != null && c.IsCode(68468459));
							}
							if (albaz4 == null)
							{
								return false;
							}
							foreach (ClientCard material2 in base.Bot.Graveyard)
							{
								if (material2 != null && material2 != albaz4 && material2.IsMonster() && material2.HasRace(CardRace.Dragon) && !material2.IsCode(this.cannotBeFusionMaterialIdList))
								{
									return true;
								}
							}
							return false;
						}
						return false;
					}
				}
			}.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, Func<bool>> pair = enumerator.Current;
					target = cards.FirstOrDefault((ClientCard card) => card.IsCode(pair.Key));
					if (target != null && pair.Value())
					{
						return pair.Key;
					}
				}
			}
			target = null;
			return 0;
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x000415D4 File Offset: 0x0003F7D4
		public List<ClientCard> ChimeraFusionMaterialList(bool dragonCheck = true)
		{
			int enemyCardCount = base.Enemy.GetMonsterCount() + base.Enemy.GetSpellCount();
			List<ClientCard> fieldMonsterList = (from c in base.Bot.MonsterZone
				where c != null && c.GetDefensePower() <= 2500 && !c.IsCode(this.cannotBeFusionMaterialIdList)
				orderby c.GetDefensePower()
				select c).ToList<ClientCard>();
			List<ClientCard> handMonsterList = (from c in base.Bot.Hand
				where c.IsMonster()
				orderby c.GetDefensePower()
				select c).ToList<ClientCard>();
			if (enemyCardCount >= 2 && fieldMonsterList.Count >= 2)
			{
				if (fieldMonsterList.Count < 2 || handMonsterList.Count < 1)
				{
					return new List<ClientCard>();
				}
				foreach (ClientCard handMonster in handMonsterList)
				{
					for (int fieldIndex = 0; fieldIndex < fieldMonsterList.Count - 1; fieldIndex++)
					{
						ClientCard fieldMonster = fieldMonsterList[fieldIndex];
						if (!fieldMonster.IsCode(handMonster.Id) && !handMonster.IsCode(fieldMonster.Id))
						{
							for (int fieldIndex2 = fieldIndex + 1; fieldIndex2 < fieldMonsterList.Count; fieldIndex2++)
							{
								ClientCard fieldMonster2 = fieldMonsterList[fieldIndex2];
								if (!fieldMonster2.IsCode(handMonster.Id) && !handMonster.IsCode(fieldMonster2.Id) && !fieldMonster2.IsCode(fieldMonster.Id) && !fieldMonster.IsCode(fieldMonster2.Id))
								{
									List<ClientCard> materialList = new List<ClientCard> { handMonster, fieldMonster, fieldMonster2 };
									bool flag;
									if (dragonCheck)
									{
										flag = materialList.Any((ClientCard c) => c.HasRace(CardRace.Dragon));
									}
									else
									{
										flag = false;
									}
									if (flag)
									{
										return materialList;
									}
								}
							}
						}
					}
				}
			}
			if (enemyCardCount == 1 || fieldMonsterList.Count == 1)
			{
				if (fieldMonsterList.Count < 1 || handMonsterList.Count < 2)
				{
					return new List<ClientCard>();
				}
				foreach (ClientCard fieldMonster3 in fieldMonsterList)
				{
					for (int handIndex = 0; handIndex < handMonsterList.Count - 1; handIndex++)
					{
						ClientCard handMonster2 = handMonsterList[handIndex];
						if (!handMonster2.IsCode(fieldMonster3.Id) && !fieldMonster3.IsCode(handMonster2.Id))
						{
							for (int handIndex2 = handIndex + 1; handIndex2 < handMonsterList.Count; handIndex2++)
							{
								ClientCard handMonster3 = handMonsterList[handIndex2];
								if (!handMonster3.IsCode(fieldMonster3.Id) && !fieldMonster3.IsCode(handMonster3.Id) && !handMonster3.IsCode(handMonster2.Id) && !handMonster2.IsCode(handMonster3.Id))
								{
									List<ClientCard> materialList2 = new List<ClientCard> { fieldMonster3, handMonster2, handMonster3 };
									bool flag2;
									if (dragonCheck)
									{
										flag2 = materialList2.Any((ClientCard c) => c.HasRace(CardRace.Dragon));
									}
									else
									{
										flag2 = false;
									}
									if (flag2)
									{
										return materialList2;
									}
								}
							}
						}
					}
				}
			}
			return new List<ClientCard>();
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x000419A4 File Offset: 0x0003FBA4
		public bool BrandedFusionActivate()
		{
			if (this.BrandedFusionActivateCheck(true))
			{
				this.SelectSTPlace(base.Card, true, null);
				return true;
			}
			return false;
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x000419C0 File Offset: 0x0003FBC0
		public bool BrandedFusionActivateCheck(bool endPhaseCheck = true)
		{
			return !this.CheckWhetherNegated(true, true, CardType.Spell) && !this.activatedCardIdList.Contains(44362883) && (base.Bot.HasInHandOrHasInMonstersZone(68468459) || this.CheckRemainInDeck(68468459) != 0) && (!endPhaseCheck || base.Duel.Phase < DuelPhase.End);
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x00041A28 File Offset: 0x0003FC28
		public bool GoldSarcophagusActivate()
		{
			if (this.CheckWhetherNegated(true, true, CardType.Spell))
			{
				return false;
			}
			ClientCard clientCard;
			if (this.GoldSarcophagusTarget(null, out clientCard) > 0)
			{
				this.SelectSTPlace(base.Card, true, null);
				return true;
			}
			return false;
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x00041A60 File Offset: 0x0003FC60
		public int GoldSarcophagusTarget(IList<ClientCard> cards, out ClientCard target)
		{
			using (Dictionary<int, Func<bool>>.Enumerator enumerator = new Dictionary<int, Func<bool>>
			{
				{
					36577931,
					() => !this.activatedCardIdList.Contains(36577931) && !base.DefaultCheckWhetherCardIdIsNegated(36577931)
				},
				{
					19096726,
					() => !this.activatedCardIdList.Contains(19096727) && !base.DefaultCheckWhetherCardIdIsNegated(19096726)
				}
			}.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, Func<bool>> pair = enumerator.Current;
					int cardId = pair.Key;
					if (pair.Value())
					{
						if (cards != null)
						{
							target = cards.FirstOrDefault((ClientCard card) => card.IsCode(pair.Key));
							if (target != null && pair.Value())
							{
								return cardId;
							}
						}
						else if (this.CheckRemainInDeck(cardId) > 0)
						{
							target = null;
							return cardId;
						}
					}
				}
			}
			target = null;
			return 0;
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x00041B4C File Offset: 0x0003FD4C
		public bool FoolishBurialActivate()
		{
			if (this.CheckWhetherNegated(true, true, CardType.Spell) || this.CheckWhetherWillbeRemoved())
			{
				return false;
			}
			ClientCard clientCard;
			if (this.FoolishBurialTarget(null, out clientCard) > 0)
			{
				this.SelectSTPlace(base.Card, true, null);
				return true;
			}
			return false;
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x00041B8C File Offset: 0x0003FD8C
		public int FoolishBurialTarget(IList<ClientCard> cards, out ClientCard target)
		{
			if (!this.activatedCardIdList.Contains(36577931) && !base.DefaultCheckWhetherCardIdIsNegated(36577931))
			{
				if (cards != null)
				{
					target = cards.FirstOrDefault((ClientCard c) => c.IsCode(36577931));
					if (target != null)
					{
						return 36577931;
					}
				}
				else if (this.CheckRemainInDeck(36577931) > 0)
				{
					target = null;
					return 36577931;
				}
			}
			if ((this.CheckRemainInDeck(17751597) > 0) | (base.Bot.HasInGraveyard(17751597) && this.CheckRemainInDeck(44362883) > 0))
			{
				using (Dictionary<int, Func<bool>>.Enumerator enumerator = new Dictionary<int, Func<bool>>
				{
					{
						60242223,
						() => !this.activatedCardIdList.Contains(60242224) && !base.DefaultCheckWhetherCardIdIsNegated(60242223)
					},
					{
						25451383,
						() => !this.activatedCardIdList.Contains(25451383) && !base.DefaultCheckWhetherCardIdIsNegated(25451383)
					}
				}.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<int, Func<bool>> pair = enumerator.Current;
						int cardId = pair.Key;
						if (pair.Value())
						{
							if (cards != null)
							{
								target = cards.FirstOrDefault((ClientCard card) => card.IsCode(pair.Key));
								if (target != null && pair.Value())
								{
									return cardId;
								}
							}
							else if (this.CheckRemainInDeck(cardId) > 0)
							{
								target = null;
								return cardId;
							}
						}
					}
				}
			}
			if (!base.Bot.HasInGraveyard(68468459))
			{
				bool flag = base.Bot.HasInHand(new List<int> { 82738008, 34995106 });
				bool flag2;
				if (base.Bot.HasInHand(95515789))
				{
					flag2 = base.Bot.MonsterZone.Count((ClientCard c) => c != null && c.Sequence < 5) < 5;
				}
				else
				{
					flag2 = false;
				}
				if (flag || flag2)
				{
					int albazCountCheck = (base.Bot.HasInHandOrInSpellZone(44362883) ? 2 : 1);
					if (cards != null)
					{
						List<ClientCard> albazList = cards.Where((ClientCard c) => c.IsCode(68468459)).ToList<ClientCard>();
						if (albazList.Count >= albazCountCheck)
						{
							target = albazList.First<ClientCard>();
							return 68468459;
						}
					}
					else if (this.CheckRemainInDeck(68468459) >= albazCountCheck)
					{
						target = null;
						return 68468459;
					}
				}
			}
			target = null;
			return 0;
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x00041E20 File Offset: 0x00040020
		public bool CalledbytheGraveActivate()
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0) || !this.CheckLastChainShouldNegated())
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
					if (base.DefaultCheckWhetherCardIdIsNegated(code))
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
						this.currentDestroyCardList.Add(graveTarget);
						return true;
					}
				}
				foreach (ClientCard graveCard in base.Enemy.Graveyard)
				{
					if (base.Duel.ChainTargets.Contains(graveCard) && graveCard.IsMonster())
					{
						if (base.Card.Location != CardLocation.SpellZone)
						{
							this.SelectSTPlace(null, true, null);
						}
						int id = graveCard.Id;
						base.AI.SelectCard(graveCard);
						this.currentDestroyCardList.Add(graveCard);
						return true;
					}
				}
				if (!base.Duel.ChainTargets.Contains(base.Card))
				{
					goto IL_0245;
				}
				List<ClientCard> enemyMonsters = base.Enemy.Graveyard.GetMatchingCards((ClientCard card) => card.IsMonster()).ToList<ClientCard>();
				if (enemyMonsters.Count > 0)
				{
					enemyMonsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					enemyMonsters.Reverse();
					int code3 = enemyMonsters[0].Id;
					base.AI.SelectCard(code3);
					this.currentDestroyCardList.Add(enemyMonsters[0]);
					return true;
				}
			}
			IL_0245:
			if (base.Duel.LastChainPlayer == 1)
			{
				return false;
			}
			List<ClientCard> targets = this.GetDangerousCardinEnemyGrave(true);
			if (targets.Count > 0)
			{
				int code2 = targets[0].Id;
				if (base.Card.Location != CardLocation.SpellZone)
				{
					this.SelectSTPlace(null, true, null);
				}
				base.AI.SelectCard(code2);
				this.currentDestroyCardList.Add(targets[0]);
				return true;
			}
			return false;
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x000420F0 File Offset: 0x000402F0
		public bool BrandedInHighSpiritsActivate()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				if (this.CheckWhetherNegated(true, false, CardType.Spell))
				{
					return false;
				}
				this.activatedCardIdList.Add(base.Card.Id);
				return true;
			}
			else
			{
				if (this.BrandedInHighSpiritsActivateCheck())
				{
					this.activatedCardIdList.Add(base.Card.Id);
					this.SelectSTPlace(base.Card, true, null);
					return true;
				}
				return false;
			}
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x00042160 File Offset: 0x00040360
		public bool BrandedInHighSpiritsActivateCheck()
		{
			bool lubellionCheck = base.Bot.HasInHand(32731036) && this.CheckRemainInDeck(60242223) > 0 && !this.activatedCardIdList.Contains(32731036) && base.Duel.Player == 0 && (base.Duel.Phase <= DuelPhase.Main1 || base.Duel.Phase == DuelPhase.Main2) && !this.CheckWhetherWillbeRemoved();
			if (this.CheckWhetherNegated(true, true, CardType.Spell) || this.activatedCardIdList.Contains(29948294) || this.CheckWhetherWillbeRemoved())
			{
				return false;
			}
			Func<ClientCard, bool> <>9__5;
			Func<ClientCard, bool> <>9__7;
			Func<ClientCard, bool> <>9__8;
			Func<ClientCard, bool> <>9__10;
			Func<ClientCard, bool> <>9__9;
			foreach (KeyValuePair<int, Func<bool>> pair in new Dictionary<int, Func<bool>>
			{
				{
					87746184,
					delegate
					{
						if (!this.sendToGYThisTurn.Any((ClientCard c) => c.IsCode(87746184)) && !lubellionCheck)
						{
							IEnumerable<ClientCard> hand = this.Bot.Hand;
							Func<ClientCard, bool> func;
							if ((func = <>9__5) == null)
							{
								func = (<>9__5 = (ClientCard c) => this.BrandedInHighSpiritDiscardDragonCheck(c));
							}
							return hand.Any(func);
						}
						return false;
					}
				},
				{
					41373230,
					delegate
					{
						if (!this.sendToGYThisTurn.Any((ClientCard c) => c.IsCode(41373230)) && !lubellionCheck)
						{
							IEnumerable<ClientCard> hand2 = this.Bot.Hand;
							Func<ClientCard, bool> func2;
							if ((func2 = <>9__7) == null)
							{
								func2 = (<>9__7 = (ClientCard c) => this.BrandedInHighSpiritDiscardDragonCheck(c));
							}
							if (hand2.Any(func2))
							{
								return this.CheckRemainInDeck(new int[] { 45883110, 68468459 }) > 0;
							}
						}
						return false;
					}
				},
				{
					24915933,
					delegate
					{
						IEnumerable<ClientCard> hand3 = this.Bot.Hand;
						Func<ClientCard, bool> func3;
						if ((func3 = <>9__8) == null)
						{
							func3 = (<>9__8 = (ClientCard c) => c.HasRace(CardRace.SpellCaster) && (!this.CheckWhetherCanSummon() || !c.IsOriginalCode(45883110)));
						}
						return hand3.Any(func3);
					}
				},
				{
					51409648,
					delegate
					{
						IEnumerable<ClientCard> hand4 = this.Bot.Hand;
						Func<ClientCard, bool> func4;
						if ((func4 = <>9__9) == null)
						{
							func4 = (<>9__9 = delegate(ClientCard c)
							{
								if (!c.HasRace(CardRace.WindBeast))
								{
									return false;
								}
								if (c.IsCode(19096726))
								{
									IEnumerable<ClientCard> monsterZone = this.Bot.MonsterZone;
									Func<ClientCard, bool> func5;
									if ((func5 = <>9__10) == null)
									{
										func5 = (<>9__10 = (ClientCard c2) => c2 != null && c2.IsFaceup() && c2.IsCode(this.albazFusionMonster));
									}
									return !monsterZone.Any(func5);
								}
								return true;
							});
						}
						return hand4.Any(func4);
					}
				}
			})
			{
				if (base.Bot.HasInExtra(pair.Key) && pair.Value())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x000422D8 File Offset: 0x000404D8
		public bool BrandedInHighSpiritDiscardDragonCheck(ClientCard card)
		{
			if (!card.HasRace(CardRace.Dragon))
			{
				return false;
			}
			if (base.Duel.Player == 0 && (base.Duel.Phase <= DuelPhase.Main1 || base.Duel.Phase == DuelPhase.Main2))
			{
				if (card.IsOriginalCode(25451383) && !this.activatedCardIdList.Contains(25451383))
				{
					return false;
				}
				if (card.IsOriginalCode(32731036) && !this.activatedCardIdList.Contains(32731036) && this.CheckRemainInDeck(60242223) > 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00042374 File Offset: 0x00040574
		public bool BrandedOpeningActivate()
		{
			if (this.CheckWhetherNegated(true, true, CardType.Spell))
			{
				return false;
			}
			if (base.Duel.Player == 0)
			{
				if (base.Bot.HasInHand(25451383) && !this.CheckWhetherWillbeRemoved() && !this.activatedCardIdList.Contains(25451383))
				{
					return false;
				}
				if ((base.Bot.HasInHand(95515789) && !this.summoned) | (!this.activatedCardIdList.Contains(6498706) && base.Bot.HasInHandOrInSpellZone(6498706) && !this.CheckShouldNoMoreSpSummon() && base.Bot.HasInExtra(24915933) && this.CheckRemainInDeck(95515789) > 0))
				{
					return false;
				}
			}
			if ((this.CheckRemainInDeck(62962630) > 0 && !this.activatedCardIdList.Contains(62962630) && !this.enemyActivateLockBird) | (this.CheckRemainInDeck(45883110) > 0))
			{
				this.SelectSTPlace(base.Card, true, null);
				this.activatedCardIdList.Add(base.Card.Id);
				return true;
			}
			return false;
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x000424A4 File Offset: 0x000406A4
		public bool CrossoutDesignatorActivate()
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0) || !this.CheckLastChainShouldNegated())
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
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x00042550 File Offset: 0x00040750
		public bool BrandedInRedActivate()
		{
			ClientCard targetCard = this.BrandedInRedActivateCheck(true);
			if (targetCard != null)
			{
				base.AI.SelectCard(targetCard);
				this.SelectSTPlace(base.Card, true, null);
				return true;
			}
			return false;
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x00042588 File Offset: 0x00040788
		public ClientCard BrandedInRedActivateCheck(bool updateMaterialList = false)
		{
			if (this.CheckWhetherNegated(true, true, CardType.Spell) || this.activatedCardIdList.Contains(82738008))
			{
				return null;
			}
			if (base.Duel.CurrentChain.Any((ClientCard c) => c != null && c.Controller == 0 && c.IsCode(95515789)))
			{
				return null;
			}
			if (base.Duel.CurrentChain.Any((ClientCard c) => c != null && c.Controller == 0 && c.IsCode(87746184)) && !base.Util.ChainContainPlayer(1))
			{
				return null;
			}
			if (this.nadirActivated)
			{
				return null;
			}
			List<ClientCard> materialList = base.Bot.MonsterZone.Where((ClientCard c) => c != null && c.Attack <= 2500 && !c.IsCode(this.cannotBeFusionMaterialIdList)).ToList<ClientCard>();
			materialList.AddRange(base.Bot.Hand.Where((ClientCard c) => c.IsMonster() && (!this.CheckWhetherCanSummon() || ((this.activatedCardIdList.Contains(62962630) || !c.IsCode(62962630)) && (this.activatedCardIdList.Contains(45484331) || !c.IsCode(45484331))))));
			List<ClientCard> graveTargetList = base.Bot.Graveyard.Where((ClientCard c) => c != null && c.IsMonster() && !c.HasType((CardType)8256) && (c.IsCode(68468459) || c.HasSetcode(356))).ToList<ClientCard>();
			if (base.Duel.LastChainPlayer == 1)
			{
				List<ClientCard> targetedList = base.Duel.LastChainTargets.Where((ClientCard c) => c != null && c.Location == CardLocation.Grave && c.Controller == 0 && !c.HasType((CardType)8256) && (c.IsCode(68468459) || c.HasSetcode(356))).ToList<ClientCard>();
				if (targetedList.Count > 0)
				{
					foreach (ClientCard target in targetedList)
					{
						List<ClientCard> newMaterialList = new List<ClientCard>(materialList) { target };
						ClientCard _fusionTarget;
						List<ClientCard> list;
						this.BrandedInRedFusionCheck(base.Bot.ExtraDeck, 0, newMaterialList, new List<ClientCard> { target }, out _fusionTarget, out list);
						if (_fusionTarget != null)
						{
							if (updateMaterialList)
							{
								Logger.DebugWriteLine("Red prepare fusion 1: " + _fusionTarget.Name);
							}
							return target;
						}
					}
				}
				ClientCard lastCahinCard = base.Util.GetLastChainCard();
				if (lastCahinCard != null)
				{
					List<ClientCard> chainTargetList = base.Duel.LastChainTargets.Where((ClientCard c) => c.Controller == 0 && c.Location == CardLocation.MonsterZone && (!c.IsCode(this.cannotBeFusionMaterialIdList) || c.Attack <= 2500)).ToList<ClientCard>();
					if (chainTargetList.Count > 0)
					{
						if (lastCahinCard.IsCode(this.targetNegateIdList))
						{
							chainTargetList = chainTargetList.Where((ClientCard c) => c.Attack <= 2500 && !c.IsCode(87746184)).ToList<ClientCard>();
						}
						foreach (ClientCard target2 in graveTargetList)
						{
							List<ClientCard> newMaterialList2 = new List<ClientCard>(materialList) { target2 };
							ClientCard _fusionTarget2;
							List<ClientCard> usedMaterialList;
							this.BrandedInRedFusionCheck(base.Bot.ExtraDeck, 0, newMaterialList2, chainTargetList, out _fusionTarget2, out usedMaterialList);
							if (_fusionTarget2 != null)
							{
								if (updateMaterialList)
								{
									Logger.DebugWriteLine("Red prepare fusion 2: " + _fusionTarget2.Name);
									this.brandedInRedMaterialList.AddRange(usedMaterialList.Intersect(chainTargetList));
								}
								return target2;
							}
						}
					}
				}
			}
			bool flag = base.Duel.Player == 0 && (base.Duel.Phase <= DuelPhase.Main1 || base.Duel.Phase == DuelPhase.Main2) && !this.summoned && (base.Bot.HasInHand(new int[] { 62962630, 45883110, 45484331 }) || (base.Bot.HasInHand(68468459) && this.CheckAlbazFusion(base.Card)));
			bool idleFlag = base.Duel.Player == 1 || base.CurrentTiming == -1;
			if (flag || !idleFlag)
			{
				return null;
			}
			if (!base.Bot.MonsterZone.Any((ClientCard c) => c != null && c.HasType(CardType.Fusion)) && base.Duel.LastChainPlayer != 0 && !this.CheckWhetherNegated(true, true, CardType.Monster) && !this.activatedCardIdList.Contains(70534340))
			{
				foreach (ClientCard target3 in graveTargetList)
				{
					List<ClientCard> newMaterialList3 = new List<ClientCard>(materialList) { target3 };
					List<ClientCard> list;
					ClientCard _fusionTarget3;
					this.BrandedInRedFusionCheck(base.Bot.ExtraDeck, 70534340, newMaterialList3, new List<ClientCard> { target3 }, out _fusionTarget3, out list);
					if (_fusionTarget3 != null)
					{
						return target3;
					}
				}
			}
			if (this.GetProblematicEnemyCardList(false, false, CardType.Monster).Count > 0 || (base.Duel.Phase == DuelPhase.End && base.Duel.Player == 1))
			{
				if (!this.enemyActivateLockBird)
				{
					foreach (ClientCard target4 in graveTargetList)
					{
						List<ClientCard> newMaterialList4 = new List<ClientCard>(materialList) { target4 };
						List<ClientCard> list;
						ClientCard _fusionTarget4;
						this.BrandedInRedFusionCheck(base.Bot.ExtraDeck, 11321089, newMaterialList4, new List<ClientCard> { target4 }, out _fusionTarget4, out list);
						if (_fusionTarget4 != null)
						{
							if (updateMaterialList)
							{
								Logger.DebugWriteLine("Red prepare fusion 3: " + _fusionTarget4.Name);
							}
							return target4;
						}
					}
				}
				foreach (ClientCard target5 in graveTargetList)
				{
					List<ClientCard> newMaterialList5 = new List<ClientCard>(materialList) { target5 };
					List<ClientCard> list;
					ClientCard _fusionTarget5;
					this.BrandedInRedFusionCheck(base.Bot.ExtraDeck, 92892239, newMaterialList5, new List<ClientCard> { target5 }, out _fusionTarget5, out list);
					if (_fusionTarget5 != null)
					{
						if (updateMaterialList)
						{
							Logger.DebugWriteLine("Red prepare fusion 4: " + _fusionTarget5.Name);
						}
						return target5;
					}
				}
			}
			return null;
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x00042BA0 File Offset: 0x00040DA0
		public void BrandedInRedFusionCheck(IList<ClientCard> canSummonList, int mustSummonId, List<ClientCard> materialList, List<ClientCard> mustMaterialList, out ClientCard fusionTarget, out List<ClientCard> selectedFusionMaterialList)
		{
			fusionTarget = null;
			selectedFusionMaterialList = new List<ClientCard>();
			List<Tuple<int, List<Func<ClientCard, bool>>, Func<List<ClientCard>, bool>, Func<List<ClientCard>, List<ClientCard>, List<Func<ClientCard, bool>>, Func<List<ClientCard>, bool>, List<ClientCard>>>> list = new List<Tuple<int, List<Func<ClientCard, bool>>, Func<List<ClientCard>, bool>, Func<List<ClientCard>, List<ClientCard>, List<Func<ClientCard, bool>>, Func<List<ClientCard>, bool>, List<ClientCard>>>>();
			int num = 44146295;
			List<Func<ClientCard, bool>> list2 = new List<Func<ClientCard, bool>>();
			list2.Add((ClientCard c) => c.IsCode(68468459));
			list2.Add((ClientCard c) => !c.IsCode(this.cannotBeFusionMaterialIdList) && c.HasType((CardType)75505728));
			list.Add(new Tuple<int, List<Func<ClientCard, bool>>, Func<List<ClientCard>, bool>, Func<List<ClientCard>, List<ClientCard>, List<Func<ClientCard, bool>>, Func<List<ClientCard>, bool>, List<ClientCard>>>(num, list2, (List<ClientCard> l) => !base.Bot.HasInMonstersZone(44146295, false, false, true) && !base.Bot.HasInSpellZone(44146295, false, true), new Func<List<ClientCard>, List<ClientCard>, List<Func<ClientCard, bool>>, Func<List<ClientCard>, bool>, List<ClientCard>>(this.BrandedInRedUsing2SubFunc)));
			int num2 = 70534340;
			List<Func<ClientCard, bool>> list3 = new List<Func<ClientCard, bool>>();
			list3.Add((ClientCard c) => c.IsCode(68468459));
			list3.Add((ClientCard c) => !c.IsCode(this.cannotBeFusionMaterialIdList) && c.HasAttribute(CardAttribute.Dark));
			list.Add(new Tuple<int, List<Func<ClientCard, bool>>, Func<List<ClientCard>, bool>, Func<List<ClientCard>, List<ClientCard>, List<Func<ClientCard, bool>>, Func<List<ClientCard>, bool>, List<ClientCard>>>(num2, list3, (List<ClientCard> l) => !base.Bot.HasInMonstersZone(44146295, false, false, true) && !base.Bot.HasInSpellZone(44146295, false, true) && base.Bot.Hand.Count((ClientCard c) => !l.Contains(c)) > 0 && !this.activatedCardIdList.Contains(70534340), new Func<List<ClientCard>, List<ClientCard>, List<Func<ClientCard, bool>>, Func<List<ClientCard>, bool>, List<ClientCard>>(this.BrandedInRedUsing2SubFunc)));
			list.Add(new Tuple<int, List<Func<ClientCard, bool>>, Func<List<ClientCard>, bool>, Func<List<ClientCard>, List<ClientCard>, List<Func<ClientCard, bool>>, Func<List<ClientCard>, bool>, List<ClientCard>>>(11321089, null, null, new Func<List<ClientCard>, List<ClientCard>, List<Func<ClientCard, bool>>, Func<List<ClientCard>, bool>, List<ClientCard>>(this.BrandedInRedForChimeraFunc)));
			int num3 = 72272462;
			List<Func<ClientCard, bool>> list4 = new List<Func<ClientCard, bool>>();
			list4.Add((ClientCard c) => c.HasSetcode(356));
			list4.Add((ClientCard c) => !c.IsCode(this.cannotBeFusionMaterialIdList) && c.HasAttribute((CardAttribute)48));
			list.Add(new Tuple<int, List<Func<ClientCard, bool>>, Func<List<ClientCard>, bool>, Func<List<ClientCard>, List<ClientCard>, List<Func<ClientCard, bool>>, Func<List<ClientCard>, bool>, List<ClientCard>>>(num3, list4, null, new Func<List<ClientCard>, List<ClientCard>, List<Func<ClientCard, bool>>, Func<List<ClientCard>, bool>, List<ClientCard>>(this.BrandedInRedUsing2SubFunc)));
			list.Add(new Tuple<int, List<Func<ClientCard, bool>>, Func<List<ClientCard>, bool>, Func<List<ClientCard>, List<ClientCard>, List<Func<ClientCard, bool>>, Func<List<ClientCard>, bool>, List<ClientCard>>>(92892239, new List<Func<ClientCard, bool>>
			{
				(ClientCard c) => !c.IsCode(this.cannotBeFusionMaterialIdList) && c.HasAttribute(CardAttribute.Dark) && c.HasRace(CardRace.Dragon),
				(ClientCard c) => !c.IsCode(this.cannotBeFusionMaterialIdList) && c.HasAttribute(CardAttribute.Dark) && c.HasRace(CardRace.Dragon)
			}, null, new Func<List<ClientCard>, List<ClientCard>, List<Func<ClientCard, bool>>, Func<List<ClientCard>, bool>, List<ClientCard>>(this.BrandedInRedUsing2SubFunc)));
			int num4 = 38811586;
			List<Func<ClientCard, bool>> list5 = new List<Func<ClientCard, bool>>();
			list5.Add((ClientCard c) => c.IsCode(68468459));
			list5.Add((ClientCard c) => !c.IsCode(this.cannotBeFusionMaterialIdList) && c.HasAttribute(CardAttribute.Light) && c.HasRace(CardRace.SpellCaster));
			list.Add(new Tuple<int, List<Func<ClientCard, bool>>, Func<List<ClientCard>, bool>, Func<List<ClientCard>, List<ClientCard>, List<Func<ClientCard, bool>>, Func<List<ClientCard>, bool>, List<ClientCard>>>(num4, list5, (List<ClientCard> l) => base.Enemy.Graveyard.Count((ClientCard c) => c != null && c.IsMonster() && c.IsCanRevive()) + base.Bot.Graveyard.Count((ClientCard c) => c != null && !l.Contains(c)) >= 2, new Func<List<ClientCard>, List<ClientCard>, List<Func<ClientCard, bool>>, Func<List<ClientCard>, bool>, List<ClientCard>>(this.BrandedInRedUsing2SubFunc)));
			using (List<Tuple<int, List<Func<ClientCard, bool>>, Func<List<ClientCard>, bool>, Func<List<ClientCard>, List<ClientCard>, List<Func<ClientCard, bool>>, Func<List<ClientCard>, bool>, List<ClientCard>>>>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Tuple<int, List<Func<ClientCard, bool>>, Func<List<ClientCard>, bool>, Func<List<ClientCard>, List<ClientCard>, List<Func<ClientCard, bool>>, Func<List<ClientCard>, bool>, List<ClientCard>>> tuple = enumerator.Current;
					if (mustSummonId <= 0 || mustSummonId == tuple.Item1)
					{
						ClientCard currentFusionTarget = canSummonList.FirstOrDefault((ClientCard c) => c != null && c.IsCode(tuple.Item1));
						if (currentFusionTarget != null)
						{
							List<ClientCard> currentMaterialList = tuple.Item4(materialList, mustMaterialList, tuple.Item2, tuple.Item3);
							if (currentMaterialList.Count > 0)
							{
								fusionTarget = currentFusionTarget;
								selectedFusionMaterialList = currentMaterialList;
								break;
							}
						}
					}
				}
			}
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x00042E54 File Offset: 0x00041054
		public List<ClientCard> BrandedInRedUsing2SubFunc(List<ClientCard> materialList, List<ClientCard> mustMaterialList, List<Func<ClientCard, bool>> checkFuncList, Func<List<ClientCard>, bool> extraCheckFunc)
		{
			List<ClientCard> selectedFusionMaterialList = new List<ClientCard>();
			Func<ClientCard, bool> fusionFunc = checkFuncList[0];
			Func<ClientCard, bool> fusionFunc2 = checkFuncList[1];
			if (mustMaterialList != null && mustMaterialList.Count > 0)
			{
				using (List<ClientCard>.Enumerator enumerator = mustMaterialList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ClientCard mustMaterial = enumerator.Current;
						if (fusionFunc(mustMaterial) || fusionFunc2(mustMaterial))
						{
							foreach (ClientCard anotherMaterial in materialList)
							{
								if (anotherMaterial != mustMaterial && (((fusionFunc(mustMaterial) && fusionFunc2(anotherMaterial)) | (fusionFunc2(mustMaterial) && fusionFunc(anotherMaterial))) & (extraCheckFunc == null || extraCheckFunc(new List<ClientCard> { mustMaterial, anotherMaterial }))))
								{
									selectedFusionMaterialList.Add(mustMaterial);
									selectedFusionMaterialList.Add(anotherMaterial);
									return selectedFusionMaterialList;
								}
							}
						}
					}
					goto IL_01E1;
				}
			}
			for (int index = 0; index < materialList.Count - 1; index++)
			{
				ClientCard material = materialList[index];
				if (fusionFunc(material) || fusionFunc2(material))
				{
					for (int index2 = index + 1; index2 < materialList.Count; index2++)
					{
						ClientCard material2 = materialList[index2];
						if (((fusionFunc(material) && fusionFunc2(material2)) | (fusionFunc2(material) && fusionFunc(material2))) & (extraCheckFunc == null || extraCheckFunc(new List<ClientCard> { material, material2 })))
						{
							this.selectedFusionMaterial.Add(material);
							this.selectedFusionMaterial.Add(material2);
							return selectedFusionMaterialList;
						}
					}
				}
			}
			IL_01E1:
			return this.selectedFusionMaterial;
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x00043068 File Offset: 0x00041268
		public List<ClientCard> BrandedInRedForChimeraFunc(List<ClientCard> materialList, List<ClientCard> mustMaterialList, List<Func<ClientCard, bool>> checkFuncList, Func<List<ClientCard>, bool> extraCheckFunc)
		{
			List<ClientCard> selectedFusionMaterialList = new List<ClientCard>();
			int enemyCardCount = base.Enemy.MonsterZone.Count((ClientCard c) => c != null);
			enemyCardCount += base.Enemy.SpellZone.Count((ClientCard c) => c != null && c.Type != 2 && c.Type != 4);
			if (enemyCardCount == 0 || this.CheckWhetherNegated(true, true, CardType.Monster))
			{
				return selectedFusionMaterialList;
			}
			List<ClientCard> fieldMaterialList = (from c in materialList
				where c.Location == CardLocation.MonsterZone
				orderby c.Attack
				select c).ToList<ClientCard>();
			List<ClientCard> handMaterialList = (from c in materialList
				where c.Location == CardLocation.Hand || c.Location == CardLocation.Grave
				orderby c.Attack
				select c).ToList<ClientCard>();
			if (enemyCardCount >= 2)
			{
				foreach (ClientCard handMonster in handMaterialList)
				{
					for (int fieldIndex = 0; fieldIndex < fieldMaterialList.Count - 1; fieldIndex++)
					{
						ClientCard fieldMonster = fieldMaterialList[fieldIndex];
						if (!fieldMonster.IsCode(handMonster.Id) && !handMonster.IsCode(fieldMonster.Id))
						{
							for (int fieldIndex2 = fieldIndex + 1; fieldIndex2 < fieldMaterialList.Count; fieldIndex2++)
							{
								ClientCard fieldMonster2 = fieldMaterialList[fieldIndex2];
								if (!fieldMonster2.IsCode(fieldMonster.Id) && !fieldMonster.IsCode(fieldMonster2.Id) && !fieldMonster2.IsCode(handMonster.Id) && !handMonster.IsCode(fieldMonster2.Id))
								{
									return new List<ClientCard> { handMonster, fieldMonster, fieldMonster2 };
								}
							}
						}
					}
				}
			}
			foreach (ClientCard fieldMonster3 in fieldMaterialList)
			{
				for (int handIndex = 0; handIndex < handMaterialList.Count - 1; handIndex++)
				{
					ClientCard handMonster2 = handMaterialList[handIndex];
					if (!handMonster2.IsCode(fieldMonster3.Id) && !fieldMonster3.IsCode(handMonster2.Id))
					{
						for (int handIndex2 = handIndex + 1; handIndex2 < handMaterialList.Count; handIndex2++)
						{
							ClientCard handMonster3 = handMaterialList[handIndex2];
							if (!handMonster3.IsCode(handMonster2.Id) && !handMonster2.IsCode(handMonster3.Id) && !handMonster3.IsCode(fieldMonster3.Id) && !fieldMonster3.IsCode(handMonster3.Id))
							{
								return new List<ClientCard> { fieldMonster3, handMonster2, handMonster3 };
							}
						}
					}
				}
			}
			return selectedFusionMaterialList;
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x000433BC File Offset: 0x000415BC
		public bool BrandedLostActivate()
		{
			return base.Card.Location == CardLocation.SpellZone && base.Card.IsFaceup();
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x000433DC File Offset: 0x000415DC
		public bool BrandedLostCardActivate()
		{
			if (base.Card.Location == CardLocation.SpellZone && base.Card.IsFaceup())
			{
				return false;
			}
			if (this.CheckWhetherNegated(true, true, CardType.Spell))
			{
				return false;
			}
			if (!this.summoned && base.Bot.HasInHand(68468459) && this.CheckAlbazFusion(null) && base.Bot.Hand.Count < 3)
			{
				return false;
			}
			this.SelectSTPlace(base.Card, true, null);
			return true;
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0004345C File Offset: 0x0004165C
		public bool InfiniteImpermanenceActivate()
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0))
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
					ClientCard target = this.GetProblematicEnemyMonster(0, true, false, (CardType)0);
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
			List<ClientCard> shouldNegateList = this.GetMonsterListForTargetNegate(true, CardType.Trap);
			if (shouldNegateList.Count > 0)
			{
				ClientCard negateTarget = shouldNegateList[0];
				this.currentNegateCardList.Add(negateTarget);
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

		// Token: 0x06000D48 RID: 3400 RVA: 0x00043648 File Offset: 0x00041848
		public bool BrightestBlazingBrandedKingActivate()
		{
			if (base.Card.Location != CardLocation.Grave)
			{
				if (base.Card.Location == CardLocation.SpellZone)
				{
					if (base.Duel.CurrentChain.Any((ClientCard c) => c.Controller == 0 && c.IsFaceup() && (c.Location == CardLocation.MonsterZone || c.Location == CardLocation.SpellZone) && !c.IsCode(this.albazFusionMonster)))
					{
						return false;
					}
					if (base.Duel.CurrentChain.Any((ClientCard c) => c.Controller == 1 && c.IsFaceup() && !this.currentNegateCardList.Contains(c) && !this.currentDestroyCardList.Contains(c) && (c.Location == CardLocation.MonsterZone || c.Location == CardLocation.SpellZone)))
					{
						this.currentNegateCardList.AddRange(base.Enemy.MonsterZone.Where((ClientCard c) => c != null && c.IsFaceup()));
						this.currentNegateCardList.AddRange(base.Enemy.SpellZone.Where((ClientCard c) => c != null && c.IsFaceup()));
						this.activatedCardIdList.Add(base.Card.Id);
						return true;
					}
				}
				return false;
			}
			if (this.CheckWhetherNegated(true, false, CardType.Trap))
			{
				return false;
			}
			this.activatedCardIdList.Add(base.Card.Id);
			return true;
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x00043768 File Offset: 0x00041968
		public bool BrandedBeastActivate()
		{
			if (this.CheckWhetherNegated(true, true, CardType.Trap))
			{
				return false;
			}
			int desc = -1;
			if (base.ActivateDescription >= base.Util.GetStringId(32756828, 0))
			{
				desc = base.ActivateDescription - base.Util.GetStringId(32756828, 0);
			}
			Logger.DebugWriteLine("Beast: " + desc.ToString());
			if (base.ActivateDescription == base.Util.GetStringId(base.Card.Id, 0))
			{
				ClientCard destroyTarget = null;
				ClientCard releaseMonster = null;
				List<ClientCard> dangerCardList = this.GetProblematicEnemyCardList(true, false, CardType.Trap);
				if (dangerCardList.Count > 0)
				{
					destroyTarget = dangerCardList[0];
				}
				if (destroyTarget == null)
				{
					if (base.Duel.Player == 1)
					{
						if ((base.CurrentTiming & 4) > 0)
						{
							List<ClientCard> targetList = this.GetNormalEnemyTargetList(true, true, CardType.Trap);
							if (targetList.Count > 0)
							{
								destroyTarget = targetList[0];
							}
						}
					}
					else
					{
						destroyTarget = base.Util.GetOneEnemyBetterThanMyBest(true, true);
					}
				}
				bool forceActivateFlag = base.DefaultOnBecomeTarget();
				int bystialCount = base.Bot.MonsterZone.Count((ClientCard c) => c != null && c.IsFaceup() && c.HasSetcode(392));
				bool flag = forceActivateFlag;
				bool flag2;
				if (bystialCount > 0)
				{
					flag2 = base.Duel.ChainTargets.Count((ClientCard c) => c.Controller == 0 && c.Location == CardLocation.MonsterZone && c.IsFaceup() && c.HasSetcode(392)) == bystialCount;
				}
				else
				{
					flag2 = false;
				}
				forceActivateFlag = flag || flag2;
				forceActivateFlag |= base.Duel.CurrentChain.Any((ClientCard c) => c.Controller == 1 && c.Location == CardLocation.SpellZone && c.IsCode(12580477));
				if (destroyTarget == null && forceActivateFlag)
				{
					releaseMonster = base.Duel.ChainTargets.FirstOrDefault((ClientCard c) => c.Controller == 0 && c.Location == CardLocation.MonsterZone && c.IsFaceup() && c.HasSetcode(392));
					if (releaseMonster == null)
					{
						if (!this.activatedCardIdList.Contains(87746185))
						{
							if (!this.sendToGYThisTurn.Any((ClientCard c) => c.IsCode(87746184)))
							{
								List<ClientCard> brandedDragonList = (from c in base.Bot.MonsterZone
									where c != null && c.IsCode(87746184)
									orderby c.GetDefensePower()
									select c).ToList<ClientCard>();
								if (brandedDragonList.Count > 0)
								{
									releaseMonster = brandedDragonList[0];
								}
							}
						}
						if (releaseMonster == null)
						{
							releaseMonster = (from c in base.Bot.MonsterZone
								where c != null && c.HasRace(CardRace.Dragon) && (!c.IsOriginalCode(68468459) || !base.Util.ChainContainsCard(68468459))
								orderby c.GetDefensePower()
								select c).FirstOrDefault<ClientCard>();
						}
					}
					List<ClientCard> targetList2 = this.GetNormalEnemyTargetList(true, true, CardType.Trap);
					if (targetList2.Count > 0)
					{
						destroyTarget = targetList2[0];
					}
				}
				if (destroyTarget != null)
				{
					if (!this.activatedCardIdList.Contains(87746185))
					{
						if (!this.sendToGYThisTurn.Any((ClientCard c) => c.IsCode(87746184)))
						{
							List<ClientCard> brandedDragonList2 = (from c in base.Bot.MonsterZone
								where c != null && c.IsCode(87746184)
								orderby c.GetDefensePower()
								select c).ToList<ClientCard>();
							if (brandedDragonList2.Count > 0)
							{
								releaseMonster = brandedDragonList2[0];
							}
						}
					}
					if (releaseMonster == null)
					{
						releaseMonster = (from c in base.Bot.MonsterZone
							where c != null && c.HasRace(CardRace.Dragon) && (!c.IsOriginalCode(68468459) || !base.Util.ChainContainsCard(68468459))
							orderby c.GetDefensePower()
							select c).FirstOrDefault<ClientCard>();
					}
				}
				if (releaseMonster != null && destroyTarget != null)
				{
					this.activatedCardIdList.Add(base.Card.Id);
					base.AI.SelectCard(releaseMonster);
					base.AI.SelectNextCard(destroyTarget);
					this.currentDestroyCardList.Add(destroyTarget);
					return true;
				}
			}
			if (base.Duel.Phase == DuelPhase.End && base.Bot.HasInGraveyard(18973184))
			{
				this.activatedCardIdList.Add(base.Card.Id + 1);
				base.AI.SelectCard(18973184);
				return true;
			}
			return false;
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x00043BEC File Offset: 0x00041DEC
		public bool BrandedRetributionActivate()
		{
			if (base.Card.Location == CardLocation.SpellZone && base.Duel.LastChainPlayer == 1)
			{
				if (this.CheckWhetherNegated(true, true, CardType.Trap))
				{
					return false;
				}
				if ((base.Bot.Graveyard.Where((ClientCard c) => c != null && c.IsCode(this.albazFusionMonster)).Count<ClientCard>() > 1) | base.Bot.MonsterZone.Any((ClientCard c) => c != null && c.IsFaceup() && c.IsCode(new int[] { 70534340, 1906812, 3410461 })))
				{
					this.activatedCardIdList.Add(base.Card.Id);
					return true;
				}
			}
			if (base.Card.Location == CardLocation.Grave)
			{
				if (this.CheckWhetherNegated(true, false, CardType.Trap))
				{
					return false;
				}
				using (Dictionary<int, Func<bool>>.Enumerator enumerator = new Dictionary<int, Func<bool>>
				{
					{
						44362883,
						() => this.BrandedFusionActivateCheck(true)
					},
					{
						18973184,
						() => (base.Duel.Player != 0 || base.Duel.Phase < DuelPhase.End) && ((base.Bot.HasInHandOrInSpellZone(44362883) && this.BrandedFusionActivateCheck(true)) || (base.Bot.HasInHandOrInSpellZone(34995106) && this.BrandedInWhiteActivateCheck()) || (base.Bot.HasInHandOrInSpellZone(82738008) && this.BrandedInRedActivateCheck(false) != null) || (!this.summoned && base.Bot.HasInHand(68468459) && this.CheckAlbazFusion(null)) || (base.Bot.HasInMonstersZone(95515789, false, false, false) || (!this.summoned && base.Bot.HasInHand(95515789))))
					},
					{
						29948294,
						() => (base.Duel.Player != 1 || (!this.fusionToGYFlag && base.Duel.Phase == DuelPhase.End)) && this.BrandedInHighSpiritsActivateCheck()
					},
					{
						82738008,
						() => this.BrandedInRedActivateCheck(false) != null
					},
					{
						34995106,
						new Func<bool>(this.BrandedInWhiteActivateCheck)
					},
					{
						19271881,
						() => (base.Duel.Player != 1 || (!this.fusionToGYFlag && base.Duel.Phase == DuelPhase.End)) && base.Bot.GetMonsters().Any((ClientCard c) => c.IsFaceup() && c.IsCode(this.albazFusionMonster))
					},
					{
						36637374,
						() => base.Bot.Hand.Count > 2 && !this.activatedCardIdList.Contains(36637374)
					}
				}.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<int, Func<bool>> pair = enumerator.Current;
						ClientCard target = base.Bot.Graveyard.FirstOrDefault((ClientCard card) => card.IsCode(pair.Key));
						if (target != null && pair.Value())
						{
							this.activatedCardIdList.Add(base.Card.Id);
							base.AI.SelectCard(target);
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x00043DF8 File Offset: 0x00041FF8
		public bool GuardianChimeraActivate()
		{
			return !this.CheckWhetherNegated(true, true, CardType.Monster);
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x00043E08 File Offset: 0x00042008
		public bool AlbionTheSanctifireDragonActivate()
		{
			if (base.Card.Location == CardLocation.MonsterZone)
			{
				if (this.CheckWhetherNegated(true, true, CardType.Monster))
				{
					return false;
				}
				List<ClientCard> allTargetList = base.Enemy.Graveyard.Where((ClientCard c) => c != null && c.IsMonster() && c.IsCanRevive()).ToList<ClientCard>();
				allTargetList.AddRange(base.Bot.Graveyard.Where((ClientCard c) => c != null && c.IsMonster() && c.IsCanRevive()).ToList<ClientCard>());
				List<ClientCard> targetList = new List<ClientCard>();
				List<ClientCard> materialList;
				if (this.CheckAlbazFusion(null, out materialList) && !this.spSummoningAlbaz)
				{
					bool albazFlag = materialList.Count > 1;
					if (materialList.Count > 0)
					{
						ClientCard material = materialList[0];
						albazFlag |= material.HasType((CardType)75505856);
						albazFlag |= material.IsFloodgate() || material.IsOneForXyz() || base.Util.GetWorstBotMonster(false).GetDefensePower() < material.Attack;
						albazFlag |= base.Duel.Player == 1 && base.Duel.Phase == DuelPhase.End && base.Duel.LastChainPlayer == -1;
					}
					if (albazFlag)
					{
						ClientCard albaz = allTargetList.FirstOrDefault((ClientCard c) => c.IsOriginalCode(68468459));
						ClientCard worstMonster = (from c in allTargetList
							where c != albaz && !this.currentDestroyCardList.Contains(c)
							orderby c.GetDefensePower()
							select c).FirstOrDefault<ClientCard>();
						if (albaz != null && worstMonster != null && (this.GetProblematicEnemyMonster(0, false, false, CardType.Monster) != null || Math.Max(worstMonster.Attack, worstMonster.Defense) <= albaz.Defense))
						{
							Logger.DebugWriteLine("Sanctifire 1");
							targetList.AddRange(new ClientCard[] { albaz, worstMonster });
							this.spSummoningAlbaz = true;
						}
					}
				}
				if (targetList.Count == 0)
				{
					ClientCard floogateCard = (from c in allTargetList
						where c.IsFloodgate()
						orderby c.GetDefensePower() descending
						select c).FirstOrDefault<ClientCard>();
					if (floogateCard != null)
					{
						ClientCard worstMonster2 = (from c in allTargetList
							where c != floogateCard
							orderby c.GetDefensePower()
							select c).FirstOrDefault<ClientCard>();
						if (worstMonster2 != null)
						{
							Logger.DebugWriteLine("Sanctifire 2");
							targetList.AddRange(new ClientCard[] { floogateCard, worstMonster2 });
						}
					}
				}
				if (targetList.Count == 0 && base.Duel.LastChainPlayer == 1)
				{
					List<ClientCard> targetedList = base.Duel.LastChainTargets.Intersect(allTargetList).ToList<ClientCard>();
					if (targetedList.Count > 0)
					{
						ClientCard target3 = this.ShuffleList<ClientCard>(targetedList)[0];
						ClientCard anotherTarget;
						if (target3.GetDefensePower() >= 2000)
						{
							anotherTarget = (from c in allTargetList
								where c != target3
								orderby c.GetDefensePower()
								select c).FirstOrDefault<ClientCard>();
						}
						else
						{
							anotherTarget = (from c in allTargetList
								where c != target3
								orderby c.GetDefensePower() descending
								select c).FirstOrDefault<ClientCard>();
						}
						if (anotherTarget != null)
						{
							Logger.DebugWriteLine("Sanctifire 3");
							targetList.AddRange(new ClientCard[] { target3, anotherTarget });
						}
					}
				}
				if (targetList.Count == 0 && base.Duel.Player == 1 && base.Duel.Phase == DuelPhase.End)
				{
					using (Dictionary<int, Func<bool>>.Enumerator enumerator = new Dictionary<int, Func<bool>>
					{
						{
							92892239,
							() => base.Enemy.GetSpellCount() > 0
						},
						{
							44146295,
							() => !base.Bot.HasInMonstersZone(44146295, false, false, true) && !base.Bot.HasInSpellZone(44146295, false, true)
						},
						{
							62962630,
							() => !this.activatedCardIdList.Contains(62962630)
						},
						{
							45484331,
							() => !this.activatedCardIdList.Contains(45484332)
						}
					}.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							KeyValuePair<int, Func<bool>> pair2 = enumerator.Current;
							ClientCard target = allTargetList.FirstOrDefault((ClientCard c) => c.IsCode(pair2.Key));
							if (target != null && pair2.Value())
							{
								ClientCard anotherTarget2 = (from c in allTargetList
									where c != target
									orderby c.GetDefensePower()
									select c).FirstOrDefault<ClientCard>();
								targetList.Add(target);
								targetList.Add(anotherTarget2);
								Logger.DebugWriteLine("Sanctifire 4");
								break;
							}
						}
					}
				}
				if (targetList.Count == 0 && base.Duel.Player == 1 && base.Enemy.Hand.Count > 0 && base.Enemy.GetMonsterCount() + base.Enemy.GetSpellCount() == 0 && (base.CurrentTiming & 8) > 0)
				{
					ClientCard summonTarget = allTargetList.OrderByDescending((ClientCard c) => c.Attack).FirstOrDefault<ClientCard>();
					using (Dictionary<int, Func<bool>>.Enumerator enumerator = new Dictionary<int, Func<bool>>
					{
						{
							44146295,
							() => !base.Bot.HasInMonstersZone(44146295, false, false, true) && !base.Bot.HasInSpellZone(44146295, false, true)
						},
						{
							62962630,
							() => !this.activatedCardIdList.Contains(62962630)
						}
					}.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							KeyValuePair<int, Func<bool>> pair = enumerator.Current;
							ClientCard target2 = allTargetList.FirstOrDefault((ClientCard c) => c.IsCode(pair.Key));
							if (target2 != null && pair.Value())
							{
								summonTarget = target2;
								break;
							}
						}
					}
					if (summonTarget != null)
					{
						ClientCard anotherTarget3 = (from c in allTargetList
							where c != summonTarget
							orderby c.GetDefensePower()
							select c).FirstOrDefault<ClientCard>();
						targetList.Add(summonTarget);
						targetList.Add(anotherTarget3);
						Logger.DebugWriteLine("Sanctifire 5");
					}
				}
				if (targetList.Count > 0)
				{
					base.AI.SelectMaterials(targetList, 509);
					this.currentDestroyCardList.AddRange(targetList);
					this.activatedCardIdList.Add(base.Card.Id);
					return true;
				}
			}
			if (base.Card.Location != CardLocation.Grave)
			{
				return false;
			}
			if (this.CheckWhetherNegated(true, false, CardType.Monster))
			{
				return false;
			}
			List<ClientCard> botCards = new List<ClientCard>
			{
				base.Bot.MonsterZone[2],
				base.Bot.MonsterZone[5],
				base.Bot.MonsterZone[6]
			};
			List<ClientCard> enemyCards = new List<ClientCard>
			{
				base.Enemy.MonsterZone[2],
				base.Enemy.MonsterZone[5],
				base.Enemy.MonsterZone[6]
			};
			if (enemyCards.Any((ClientCard c) => c != null && (c.IsFloodgate() || c.IsMonsterDangerous())))
			{
				return true;
			}
			return botCards.Select(delegate(ClientCard c)
			{
				if (c != null)
				{
					return c.GetDefensePower();
				}
				return 0;
			}).Sum() < enemyCards.Select(delegate(ClientCard c)
			{
				if (c != null)
				{
					return c.GetDefensePower();
				}
				return 0;
			}).Sum();
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x000446AC File Offset: 0x000428AC
		public bool MirrorjadeTheIcebladeDragonActivate()
		{
			if (base.Card.Location != CardLocation.MonsterZone)
			{
				return !this.CheckWhetherNegated(true, false, CardType.Monster);
			}
			if (base.Card.Location == CardLocation.MonsterZone)
			{
				bool checkFlag = this.GetProblematicEnemyMonster(0, false, false, CardType.Monster) != null;
				if (base.Enemy.GetMonsterCount() > 0)
				{
					checkFlag |= base.Duel.Player == 1 && base.Duel.Phase == DuelPhase.End && base.Duel.LastChainPlayer != 0;
					int enemyBattlePower = ((base.Enemy.BattlingMonster == null) ? 0 : base.Enemy.BattlingMonster.GetDefensePower());
					int botBattlePower = ((base.Bot.BattlingMonster == null) ? 0 : base.Bot.BattlingMonster.GetDefensePower());
					checkFlag |= enemyBattlePower > 0 && enemyBattlePower > botBattlePower && base.Duel.LastChainPlayer != 0 && !this.currentDestroyCardList.Contains(base.Enemy.BattlingMonster);
					checkFlag |= base.DefaultOnBecomeTarget() && base.Duel.LastChainPlayer != 0;
				}
				if (base.Duel.CurrentChain.Any((ClientCard c) => c.IsCode(27204311) && !base.DefaultCheckWhetherCardIdIsNegated(27204311)))
				{
					checkFlag |= base.Enemy.GetMonsterCount() > 0;
					checkFlag |= base.Bot.HasInMonstersZone(new int[] { 32731036, 36577931, 19096726 }, false, false, false);
				}
				if (checkFlag)
				{
					Dictionary<int, Func<bool>> dictionary = new Dictionary<int, Func<bool>>();
					dictionary.Add(87746184, () => !this.sendToGYThisTurn.Any((ClientCard c) => c.IsCode(87746184)) && !base.DefaultCheckWhetherCardIdIsNegated(87746184) && (base.Duel.Player != 0 || !base.Bot.HasInMonstersZone(87746184, false, false, false) || !base.Bot.HasInGraveyard(32731036)));
					dictionary.Add(51409648, delegate
					{
						if (base.Duel.Player == 1)
						{
							if (base.Bot.Graveyard.Any((ClientCard c) => c.IsOriginalCode(68468459)) && base.Bot.Hand.Count > 0 && !this.activatedCardIdList.Contains(68468459))
							{
								return !base.DefaultCheckWhetherCardIdIsNegated(68468459);
							}
						}
						return false;
					});
					dictionary.Add(41373230, () => this.CheckRemainInDeck(45883110) > 0);
					dictionary.Add(1906812, () => this.CheckRemainInDeck(45484331) > 0);
					dictionary.Add(3410461, () => this.CheckRemainInDeck(new int[] { 6498706, 44362883 }) > 0);
					dictionary.Add(70534340, () => true);
					dictionary.Add(38811586, () => true);
					foreach (KeyValuePair<int, Func<bool>> pair in dictionary)
					{
						if (base.Bot.HasInExtra(pair.Key) && pair.Value())
						{
							base.AI.SelectCard(pair.Key);
							return true;
						}
					}
					return false;
				}
			}
			return false;
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x00044968 File Offset: 0x00042B68
		public bool BorreloadFuriousDragonActivate()
		{
			if (this.CheckWhetherNegated(true, true, CardType.Monster))
			{
				return false;
			}
			List<ClientCard> dangerList = this.GetProblematicEnemyCardList(true, false, CardType.Monster);
			if (dangerList.Count > 0)
			{
				ClientCard botTarget = (from c in base.Bot.GetMonsters()
					orderby c.GetDefensePower() + (c.IsCode(this.albazFusionMonster) ? 1 : 0)
					select c).FirstOrDefault<ClientCard>();
				if (botTarget != null)
				{
					base.AI.SelectCard(botTarget);
					base.AI.SelectNextCard(dangerList);
					this.currentDestroyCardList.Add(botTarget);
					this.currentDestroyCardList.Add(dangerList[0]);
					this.activatedCardIdList.Add(base.Card.Id);
					return true;
				}
			}
			List<ClientCard> enemyTargetList = this.GetNormalEnemyTargetList(true, true, CardType.Monster);
			if (base.Duel.LastChainPlayer == 1)
			{
				List<ClientCard> targetedBotMonsterList = base.Duel.LastChainTargets.Where((ClientCard c) => c.Location == CardLocation.MonsterZone && c.Controller == 0).ToList<ClientCard>();
				ClientCard lastChainCard = base.Util.GetLastChainCard();
				if (lastChainCard != null && lastChainCard.IsCode(this.targetNegateIdList))
				{
					targetedBotMonsterList = (from c in targetedBotMonsterList
						where !c.IsCode(95515789) || c.Attack < 2500
						orderby c.Attack
						select c).ToList<ClientCard>();
				}
				if (targetedBotMonsterList.Count > 0)
				{
					base.AI.SelectCard(targetedBotMonsterList);
					base.AI.SelectNextCard(enemyTargetList);
					this.currentDestroyCardList.Add(targetedBotMonsterList[0]);
					this.currentDestroyCardList.Add(enemyTargetList[0]);
					this.activatedCardIdList.Add(base.Card.Id);
					return true;
				}
			}
			if (base.Duel.Player == 1 && base.Duel.Phase == DuelPhase.End)
			{
				List<ClientCard> botTargetList = (from c in base.Bot.MonsterZone
					where c != null && c.GetDefensePower() <= 2500
					orderby c.GetDefensePower()
					select c).ToList<ClientCard>();
				if (botTargetList.Count > 0)
				{
					base.AI.SelectCard(botTargetList);
					base.AI.SelectNextCard(enemyTargetList);
					this.currentDestroyCardList.Add(botTargetList[0]);
					this.currentDestroyCardList.Add(enemyTargetList[0]);
					this.activatedCardIdList.Add(base.Card.Id);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x00044C08 File Offset: 0x00042E08
		public bool LubellionTheSearingDragonActivate()
		{
			if (this.CheckWhetherNegated(true, true, CardType.Monster))
			{
				return false;
			}
			ClientCard clientCard;
			if (base.Card.Location == CardLocation.MonsterZone && this.LubellionTheSearingDragonFusionTarget(base.Bot.ExtraDeck, out clientCard) > 0)
			{
				this.activatedCardIdList.Add(base.Card.Id);
				return true;
			}
			return false;
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x00044C60 File Offset: 0x00042E60
		public int LubellionTheSearingDragonFusionTarget(IList<ClientCard> cards, out ClientCard target)
		{
			target = null;
			bool hasAlbaz = base.Bot.Banished.Any((ClientCard c) => c != null && c.IsFaceup() && c.IsOriginalCode(68468459));
			hasAlbaz |= base.Bot.Graveyard.Any((ClientCard c) => c != null && c.IsFaceup() && c.IsOriginalCode(68468459));
			Func<ClientCard, bool> <>9__10;
			Func<ClientCard, bool> <>9__11;
			using (Dictionary<int, Func<bool>>.Enumerator enumerator = new Dictionary<int, Func<bool>>
			{
				{
					44146295,
					() => hasAlbaz && !this.DefaultCheckWhetherCardIdIsNegated(44146295)
				},
				{
					24915933,
					() => this.Bot.HasInBanished(95515789)
				},
				{
					92892239,
					delegate
					{
						IEnumerable<ClientCard> banished = this.Bot.Banished;
						Func<ClientCard, bool> func;
						if ((func = <>9__10) == null)
						{
							func = (<>9__10 = (ClientCard c) => c.IsFaceup() && c.HasRace(CardRace.Dragon) && c.HasAttribute(CardAttribute.Dark) && !c.IsCode(this.cannotBeFusionMaterialIdList));
						}
						int num = banished.Where(func).Count<ClientCard>();
						IEnumerable<ClientCard> graveyard = this.Bot.Graveyard;
						Func<ClientCard, bool> func2;
						if ((func2 = <>9__11) == null)
						{
							func2 = (<>9__11 = (ClientCard c) => c.HasRace(CardRace.Dragon) && c.HasAttribute(CardAttribute.Dark) && !c.IsCode(this.cannotBeFusionMaterialIdList) && (this.Duel.Player == 1 || !this.CheckWhetherShouldKeepInGrave(c)));
						}
						return num + graveyard.Where(func2).Count<ClientCard>() >= 2;
					}
				},
				{
					51409648,
					() => hasAlbaz && this.Bot.HasInBanished(19096726)
				},
				{
					41373230,
					delegate
					{
						if (!hasAlbaz)
						{
							return false;
						}
						ClientCard enemyMonster = this.Util.GetBestEnemyMonster(true, false);
						ClientCard botMonster = this.Util.GetBestBotMonster(true);
						int enemyPower = ((enemyMonster == null) ? 0 : enemyMonster.GetDefensePower());
						int botPower = ((botMonster == null) ? 0 : botMonster.Attack);
						if (enemyPower > 0 && enemyPower >= botPower)
						{
							List<ClientCard> list = new List<ClientCard>(this.Bot.Banished);
							list.AddRange(this.Bot.Graveyard);
							foreach (ClientCard material in list)
							{
								if (material != null && material.IsFaceup() && material.Attack >= 2500 && 2900 + material.Level >= enemyPower)
								{
									return true;
								}
							}
							return false;
						}
						return false;
					}
				},
				{
					72272462,
					delegate
					{
						if (!this.Enemy.MonsterZone.Any((ClientCard c) => c != null && c.IsFaceup() && c.Attack > 2500 && (!c.HasType(CardType.Fusion) || c.Level < 8)))
						{
							return false;
						}
						return this.Bot.Banished.Any((ClientCard c) => c != null && c.IsFaceup() && c.HasSetcode(356)) | this.Bot.Graveyard.Any((ClientCard c) => c != null && c.IsFaceup() && c.HasSetcode(356));
					}
				},
				{
					3410461,
					() => hasAlbaz && !this.Bot.HasInExtra(87746184)
				},
				{
					87746184,
					delegate
					{
						if (this.activatedCardIdList.Contains(87746184) || this.DefaultCheckWhetherCardIdIsNegated(87746184))
						{
							return false;
						}
						return this.Bot.Banished.Any((ClientCard c) => c != null && c.IsFaceup() && c.HasAttribute(CardAttribute.Light)) | this.Bot.Graveyard.Any((ClientCard c) => c != null && c.IsFaceup() && c.HasAttribute(CardAttribute.Light));
					}
				}
			}.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, Func<bool>> pair = enumerator.Current;
					target = cards.FirstOrDefault((ClientCard card) => card.IsCode(pair.Key));
					if (target != null && pair.Value())
					{
						return pair.Key;
					}
				}
			}
			target = null;
			return 0;
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x00044E30 File Offset: 0x00043030
		public bool AlbaLenatusTheAbyssDragonSpSummon()
		{
			List<ClientCard> enemyDragon = (from c in base.Enemy.GetMonsters()
				where c != null && c.IsFaceup() && !c.IsCode(this.cannotBeFusionMaterialIdList) && c.HasRace(CardRace.Dragon)
				select c).ToList<ClientCard>();
			if (enemyDragon.Count > 0)
			{
				bool flag = enemyDragon.Count > 1;
				int bestBotPower = base.Util.GetBestAttack(base.Bot);
				if (flag | enemyDragon.Any((ClientCard c) => c.GetDefensePower() >= bestBotPower))
				{
					if (!enemyDragon.Any((ClientCard c) => c.IsCode(68468459)))
					{
						ClientCard botAlbaz = base.Bot.MonsterZone.FirstOrDefault((ClientCard c) => c != null && c.IsCode(68468459));
						if (botAlbaz != null)
						{
							enemyDragon.Add(botAlbaz);
						}
					}
					base.AI.SelectMaterials(enemyDragon, 0);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x00044F17 File Offset: 0x00043117
		public bool AlbaLenatusTheAbyssDragonActivate()
		{
			if (this.CheckWhetherNegated(true, false, CardType.Monster))
			{
				return false;
			}
			this.activatedCardIdList.Add(base.Card.Id);
			return true;
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x00044F40 File Offset: 0x00043140
		public bool GranguignolTheDuskDragonActivate()
		{
			int desc = -1;
			if (base.ActivateDescription >= base.Util.GetStringId(24915933, 0))
			{
				desc = base.ActivateDescription - base.Util.GetStringId(24915933, 0);
			}
			Logger.DebugWriteLine("granguignol: " + desc.ToString());
			if (base.ActivateDescription == -1 || base.ActivateDescription == base.Util.GetStringId(24915933, 0))
			{
				if (this.CheckWhetherNegated(true, true, CardType.Monster) || this.CheckWhetherWillbeRemoved())
				{
					return false;
				}
				ClientCard clientCard;
				if (this.GranguignolTheDuskDragonSendToGYTarget(null, out clientCard) > 0)
				{
					this.activatedCardIdList.Add(base.Card.Id);
					return true;
				}
			}
			if (base.ActivateDescription != base.Util.GetStringId(24915933, 1))
			{
				return false;
			}
			if (this.CheckWhetherNegated(true, base.Card.Location == CardLocation.MonsterZone, CardType.Monster))
			{
				return false;
			}
			this.activatedCardIdList.Add(base.Card.Id + 1);
			return true;
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x00045040 File Offset: 0x00043240
		public int GranguignolTheDuskDragonSendToGYTarget(IList<ClientCard> cards, out ClientCard target)
		{
			bool needSendBranded = base.Bot.HasInGraveyard(17751597) && this.CheckRemainInDeck(44362883) > 0;
			if (this.CheckRemainInDeck(17751597) > 0)
			{
				needSendBranded |= base.Bot.Graveyard.Any((ClientCard c) => c != null && c.HasType((CardType)6) && c.HasSetcode(349) && (!this.fusionToGYFlag || !c.IsCode(new int[] { 19271881, 29948294 })));
				needSendBranded |= !this.activatedCardIdList.Contains(25451383) && !this.CheckWhetherWillbeRemoved() && base.Bot.HasInHandOrInGraveyard(25451383);
				needSendBranded |= base.Duel.CurrentChain.Any((ClientCard c) => c.Controller == 0 && c.Location == CardLocation.Grave && c.IsCode(60242223));
			}
			List<KeyValuePair<int, Func<bool>>> list = new List<KeyValuePair<int, Func<bool>>>();
			list.Add(new KeyValuePair<int, Func<bool>>(60242223, () => !this.activatedCardIdList.Contains(60242223) & needSendBranded));
			list.Add(new KeyValuePair<int, Func<bool>>(25451383, () => !this.activatedCardIdList.Contains(25451383) & needSendBranded));
			list.Add(new KeyValuePair<int, Func<bool>>(87746184, delegate
			{
				if (!this.activatedCardIdList.Contains(87746185))
				{
					return !this.sendToGYThisTurn.Any((ClientCard c) => c.IsCode(87746184));
				}
				return false;
			}));
			list.Add(new KeyValuePair<int, Func<bool>>(41373230, delegate
			{
				if (!this.activatedCardIdList.Contains(41373230))
				{
					if (!this.sendToGYThisTurn.Any((ClientCard c) => c.IsCode(41373230)))
					{
						return this.CheckRemainInDeck(new int[] { 45883110, 68468459 }) > 0;
					}
				}
				return false;
			}));
			list.Add(new KeyValuePair<int, Func<bool>>(32731036, () => this.Bot.HasInMonstersZone(new int[] { 87746184, 41373230 }, false, false, false) && this.CheckRemainInDeck(new int[] { 18973184, 32756828 }) > 0));
			list.Add(new KeyValuePair<int, Func<bool>>(1906812, delegate
			{
				if (!this.activatedCardIdList.Contains(1906812))
				{
					if (!this.sendToGYThisTurn.Any((ClientCard c) => c.IsCode(1906812)))
					{
						return this.CheckRemainInDeck(45484331) > 0;
					}
				}
				return false;
			}));
			list.Add(new KeyValuePair<int, Func<bool>>(53971455, () => this.CheckRemainInDeck(new int[] { 95515789, 45883110 }) > 0));
			list.Add(new KeyValuePair<int, Func<bool>>(25451383, () => true));
			using (List<KeyValuePair<int, Func<bool>>>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, Func<bool>> pair = enumerator.Current;
					if (cards == null)
					{
						if ((this.CheckRemainInDeck(pair.Key) > 0 || base.Bot.HasInExtra(pair.Key)) && pair.Value())
						{
							target = null;
							return pair.Key;
						}
					}
					else
					{
						ClientCard tg = cards.FirstOrDefault((ClientCard c) => c.IsOriginalCode(pair.Key));
						if (tg != null && pair.Value())
						{
							target = tg;
							return pair.Key;
						}
					}
				}
			}
			target = null;
			return 0;
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x0004530C File Offset: 0x0004350C
		public bool DespianQuaeritisActivate()
		{
			if (base.Card.Location == CardLocation.MonsterZone && base.Duel.Phase == DuelPhase.Main1 && !this.CheckWhetherNegated(true, true, CardType.Monster))
			{
				if ((base.CurrentTiming & 4) != 0 && base.Duel.Player == 1)
				{
					int bestBotPower = base.Util.GetBestPower(base.Bot, false);
					if (base.Enemy.GetMonsters().Any((ClientCard c) => c.GetDefensePower() >= bestBotPower && c.IsAttack() && (!c.HasType(CardType.Fusion) || c.Level < 8)))
					{
						this.activatedCardIdList.Add(base.Card.Id);
						return true;
					}
				}
				if (base.Duel.Player == 0)
				{
					if (base.Enemy.GetMonsters().Any((ClientCard c) => c.IsAttack() && (!c.HasType(CardType.Fusion) || c.Level < 8)))
					{
						this.activatedCardIdList.Add(base.Card.Id);
						return true;
					}
				}
			}
			if (base.Card.Location == CardLocation.MonsterZone)
			{
				return false;
			}
			if (this.CheckWhetherNegated(true, false, CardType.Monster))
			{
				return true;
			}
			this.activatedCardIdList.Add(base.Card.Id + 1);
			return true;
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x00045444 File Offset: 0x00043644
		public bool SprindTheIrondashDragonActivate()
		{
			if (base.Card.Location != CardLocation.Grave)
			{
				if (base.Card.Location == CardLocation.MonsterZone)
				{
					if (this.CheckWhetherNegated(true, true, CardType.Monster))
					{
						return false;
					}
					if (this.SprindTheIrondashDragonMoveZone(0, base.Card) > 0)
					{
						this.activatedCardIdList.Add(base.Card.Id);
						return true;
					}
				}
				return false;
			}
			if (this.CheckWhetherNegated(true, false, CardType.Monster))
			{
				return false;
			}
			this.activatedCardIdList.Add(base.Card.Id + 1);
			return true;
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x000454CC File Offset: 0x000436CC
		public int SprindTheIrondashDragonMoveZone(int available = 0, ClientCard selfCard = null)
		{
			int maxZone = -1;
			int maxValue = 0;
			for (int zoneId = 0; zoneId < 5; zoneId++)
			{
				if (base.Bot.MonsterZone[zoneId] == null)
				{
					int zone = (int)Math.Pow(2.0, (double)zoneId);
					if (available <= 0 || (available & zone) != 0)
					{
						int currentValue = this.SprindTheIrondashDragonDestroyValue(zoneId, selfCard);
						if (currentValue > maxValue)
						{
							maxZone = zone;
							maxValue = currentValue;
						}
					}
				}
			}
			return maxZone;
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x00045528 File Offset: 0x00043728
		public int SprindTheIrondashDragonDestroyValue(int zoneId, ClientCard selfCard = null)
		{
			int value = 0;
			if (zoneId == 1 || zoneId == 3)
			{
				ClientCard botMonsterInExtraZone = base.Bot.MonsterZone[(zoneId + 9) / 2];
				if (botMonsterInExtraZone != null && botMonsterInExtraZone != selfCard && botMonsterInExtraZone.IsFaceup())
				{
					value -= 5;
				}
				ClientCard enemyMonserInExtraZone = base.Enemy.MonsterZone[(11 - zoneId) / 2];
				if (enemyMonserInExtraZone != null && enemyMonserInExtraZone.IsFaceup())
				{
					value += 2;
				}
			}
			ClientCard botSpell = base.Bot.SpellZone[zoneId];
			if (botSpell != null && botSpell.IsFaceup())
			{
				value--;
			}
			ClientCard enemyMonster = base.Enemy.MonsterZone[5 - zoneId];
			if (enemyMonster != null && enemyMonster.IsFaceup())
			{
				value++;
				if (enemyMonster.IsFloodgate() || enemyMonster.IsMonsterDangerous())
				{
					value += 5;
				}
			}
			ClientCard enemySpell = base.Enemy.SpellZone[5 - zoneId];
			if (enemySpell != null && enemySpell.IsFaceup())
			{
				value++;
				if (enemySpell.IsFloodgate())
				{
					value += 5;
				}
			}
			return value;
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x0004560A File Offset: 0x0004380A
		public bool TitanikladTheAshDragonActivate()
		{
			if (base.Card.Location != CardLocation.Grave)
			{
				return false;
			}
			if (this.CheckWhetherNegated(true, false, CardType.Monster))
			{
				return false;
			}
			this.activatedCardIdList.Add(base.Card.Id);
			return true;
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x00045644 File Offset: 0x00043844
		public bool RindbrummTheStrikingDragonActivate()
		{
			if (base.Card.Location == CardLocation.MonsterZone)
			{
				if (this.CheckWhetherNegated(true, true, CardType.Monster))
				{
					return false;
				}
				bool checkFlag = false;
				ClientCard lastChainCard = base.Util.GetLastChainCard();
				if (lastChainCard != null)
				{
					checkFlag = base.Duel.LastChainPlayer == 1;
					checkFlag |= base.Duel.LastChainPlayer == 0 && lastChainCard.IsCode(44146295) && lastChainCard.Location == CardLocation.MonsterZone && base.Enemy.GetMonsterCount() == 0;
				}
				if (checkFlag)
				{
					this.activatedCardIdList.Add(base.Card.Id);
					return true;
				}
			}
			if (base.Card.Location == CardLocation.Grave)
			{
				if (!this.CheckWhetherNegated(true, true, CardType.Monster))
				{
					if (!base.Duel.CurrentChain.Any((ClientCard c) => c.Controller == 0 && c.IsCode(45883110)))
					{
						ClientCard albaz = base.Bot.Graveyard.FirstOrDefault((ClientCard c) => c.IsOriginalCode(68468459));
						bool flag;
						if (base.Card.IsCanRevive())
						{
							flag = base.Enemy.MonsterZone.Any((ClientCard c) => c != null && c.IsFaceup() && c.HasType((CardType)75505728));
						}
						else
						{
							flag = false;
						}
						bool checkFlag2 = flag;
						List<ClientCard> materialList;
						if (this.CheckAlbazFusion(null, out materialList) && albaz != null && !this.spSummoningAlbaz)
						{
							checkFlag2 |= materialList.Count > 1;
							if (materialList.Count > 0)
							{
								ClientCard material = materialList[0];
								checkFlag2 |= material.HasType((CardType)75505856);
								bool flag2 = checkFlag2;
								bool flag3;
								if (!material.IsFloodgate() && !material.IsOneForXyz())
								{
									ClientCard worstBotMonster = base.Util.GetWorstBotMonster(false);
									int? num = ((worstBotMonster != null) ? new int?(worstBotMonster.GetDefensePower()) : null);
									int attack = material.Attack;
									flag3 = (num.GetValueOrDefault() < attack) & (num != null);
								}
								else
								{
									flag3 = true;
								}
								checkFlag2 = flag2 || flag3;
								checkFlag2 |= base.Duel.Player == 1 && base.Duel.Phase == DuelPhase.End && base.Duel.LastChainPlayer == -1;
							}
						}
						if (!checkFlag2)
						{
							return false;
						}
						albaz = (from c in base.Bot.Graveyard
							where c.IsCode(68468459)
							orderby c.GetDefensePower()
							select c).FirstOrDefault<ClientCard>();
						if (checkFlag2 && albaz != null)
						{
							this.spSummoningAlbaz = true;
							this.activatedCardIdList.Add(base.Card.Id + 1);
							base.AI.SelectCard(albaz);
							return true;
						}
						return false;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x00045914 File Offset: 0x00043B14
		public bool AlbionTheBrandedDragonActivate()
		{
			if (base.Card.Location == CardLocation.MonsterZone)
			{
				if (this.CheckWhetherNegated(true, true, CardType.Monster))
				{
					return false;
				}
				ClientCard clientCard;
				if (this.AlbionTheBrandedDragonFusionTarget(base.Bot.ExtraDeck, out clientCard) > 0)
				{
					this.activatedCardIdList.Add(base.Card.Id);
					return true;
				}
			}
			if (base.Card.Location != CardLocation.Grave)
			{
				return false;
			}
			if (this.CheckWhetherNegated(true, false, CardType.Monster))
			{
				return false;
			}
			this.activatedCardIdList.Add(base.Card.Id + 1);
			return true;
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x000459A4 File Offset: 0x00043BA4
		public int AlbionTheBrandedDragonFusionTarget(IList<ClientCard> cards, out ClientCard target)
		{
			AlbazExecutor.<>c__DisplayClass151_0 CS$<>8__locals1 = new AlbazExecutor.<>c__DisplayClass151_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.cards = cards;
			target = null;
			using (Dictionary<int, Func<bool>>.Enumerator enumerator = new Dictionary<int, Func<bool>>
			{
				{
					44146295,
					delegate
					{
						bool flag = !CS$<>8__locals1.<>4__this.CheckWhetherNegated(true, false, (CardType)0) && CS$<>8__locals1.<>4__this.CheckShouldNoMoreSpSummon();
						IEnumerable<ClientCard> graveyard = CS$<>8__locals1.<>4__this.Bot.Graveyard;
						Func<ClientCard, bool> func;
						if ((func = CS$<>8__locals1.<>9__9) == null)
						{
							func = (CS$<>8__locals1.<>9__9 = (ClientCard c) => c != null && !CS$<>8__locals1.<>4__this.sendToGYThisTurn.Contains(c) && !c.IsCode(CS$<>8__locals1.<>4__this.cannotBeFusionMaterialIdList) && c.HasType((CardType)75505728));
						}
						return flag | graveyard.Any(func);
					}
				},
				{
					70534340,
					delegate
					{
						if (!CS$<>8__locals1.<>4__this.activatedCardIdList.Contains(70534340))
						{
							List<ClientCard> list = new List<ClientCard>(CS$<>8__locals1.<>4__this.Bot.GetMonsters());
							list.AddRange(CS$<>8__locals1.<>4__this.Bot.Graveyard);
							bool albazChecked = false;
							bool hasOriginalAlbaz = list.Any((ClientCard c) => c.IsOriginalCode(68468459));
							foreach (ClientCard checkCard in list)
							{
								if (!albazChecked && checkCard.IsCode(68468459) && (!hasOriginalAlbaz || !checkCard.IsOriginalCode(25451383)))
								{
									albazChecked = true;
								}
								else if (checkCard.HasAttribute(CardAttribute.Dark))
								{
									return true;
								}
							}
							if (CS$<>8__locals1.<>4__this.Bot.HasInHand(19096726) && CS$<>8__locals1.<>4__this.Bot.Hand.Count >= 2)
							{
								return true;
							}
							return false;
						}
						return false;
					}
				},
				{
					92892239,
					delegate
					{
						if (CS$<>8__locals1.<>4__this.Enemy.GetMonsterCount() + CS$<>8__locals1.<>4__this.Enemy.GetSpellCount() > 0)
						{
							IEnumerable<ClientCard> graveyard2 = CS$<>8__locals1.<>4__this.Bot.Graveyard;
							Func<ClientCard, bool> func2;
							if ((func2 = CS$<>8__locals1.<>9__11) == null)
							{
								func2 = (CS$<>8__locals1.<>9__11 = (ClientCard c) => c.HasRace(CardRace.Dragon) && c.HasAttribute(CardAttribute.Dark) && !c.IsCode(CS$<>8__locals1.<>4__this.cannotBeFusionMaterialIdList) && (CS$<>8__locals1.<>4__this.Duel.Player == 1 || !CS$<>8__locals1.<>4__this.CheckWhetherShouldKeepInGrave(c)));
							}
							int darkDragonCount = graveyard2.Where(func2).Count<ClientCard>();
							if (CS$<>8__locals1.<>4__this.Duel.Player == 1)
							{
								IEnumerable<ClientCard> monsters = CS$<>8__locals1.<>4__this.Bot.GetMonsters();
								Func<ClientCard, bool> func3;
								if ((func3 = CS$<>8__locals1.<>9__12) == null)
								{
									func3 = (CS$<>8__locals1.<>9__12 = (ClientCard c) => c.HasRace(CardRace.Dragon) && c.HasAttribute(CardAttribute.Dark) && !c.IsCode(CS$<>8__locals1.<>4__this.cannotBeFusionMaterialIdList));
								}
								if (monsters.Any(func3))
								{
									darkDragonCount++;
								}
							}
							return darkDragonCount >= 2;
						}
						return false;
					}
				},
				{
					38811586,
					delegate
					{
						ClientCard albaz = CS$<>8__locals1.<>4__this.Bot.Graveyard.FirstOrDefault((ClientCard c) => c.IsCode(68468459));
						ClientCard lightSpellcaster = CS$<>8__locals1.<>4__this.Bot.Graveyard.FirstOrDefault((ClientCard c) => c.HasRace(CardRace.SpellCaster) && c.HasAttribute(CardAttribute.Light));
						return CS$<>8__locals1.<>4__this.Enemy.GetGraveyardMonsters().Count + CS$<>8__locals1.<>4__this.Bot.Graveyard.Where((ClientCard c) => c.IsMonster() && c != albaz && c != lightSpellcaster).Count<ClientCard>() + (CS$<>8__locals1.<>4__this.Bot.HasInHand(23434538) ? 1 : 0) >= 2;
					}
				},
				{
					51409648,
					() => CS$<>8__locals1.<>4__this.Bot.HasInGraveyard(19096726)
				},
				{
					72272462,
					delegate
					{
						if (CS$<>8__locals1.<>4__this.Enemy.MonsterZone.Any((ClientCard c) => c != null && c.IsFaceup() && c.Attack >= 2500 && (!c.HasType(CardType.Fusion) || c.Level < 8)))
						{
							AlbazExecutor.<>c__DisplayClass151_2 CS$<>8__locals4 = new AlbazExecutor.<>c__DisplayClass151_2();
							CS$<>8__locals4.CS$<>8__locals1 = CS$<>8__locals1;
							AlbazExecutor.<>c__DisplayClass151_2 CS$<>8__locals5 = CS$<>8__locals4;
							IEnumerable<ClientCard> graveyard3 = CS$<>8__locals1.<>4__this.Bot.Graveyard;
							Func<ClientCard, bool> func4;
							if ((func4 = CS$<>8__locals1.<>9__17) == null)
							{
								func4 = (CS$<>8__locals1.<>9__17 = (ClientCard c) => c != null && c.HasSetcode(356) && !CS$<>8__locals1.<>4__this.CheckWhetherShouldKeepInGrave(c));
							}
							CS$<>8__locals5.despian = (from c in graveyard3.Where(func4)
								orderby c.GetDefensePower()
								select c).FirstOrDefault<ClientCard>();
							if (CS$<>8__locals4.despian == null)
							{
								CS$<>8__locals4.despian = (from c in CS$<>8__locals1.<>4__this.Bot.MonsterZone
									where c != null && c.HasSetcode(356)
									orderby c.GetDefensePower()
									select c).FirstOrDefault<ClientCard>();
							}
							if (CS$<>8__locals4.despian != null)
							{
								return CS$<>8__locals1.<>4__this.Bot.Graveyard.Any((ClientCard c) => c.HasAttribute((CardAttribute)48) && !CS$<>8__locals4.CS$<>8__locals1.<>4__this.CheckWhetherShouldKeepInGrave(c) && c != CS$<>8__locals4.despian);
							}
						}
						return false;
					}
				},
				{
					41373230,
					delegate
					{
						ClientCard albaz2 = CS$<>8__locals1.<>4__this.Bot.Graveyard.FirstOrDefault((ClientCard c) => c != null && c.IsOriginalCode(68468459));
						if (albaz2 == null)
						{
							albaz2 = CS$<>8__locals1.<>4__this.Bot.Graveyard.FirstOrDefault((ClientCard c) => c != null && c.IsCode(68468459));
						}
						foreach (ClientCard material in CS$<>8__locals1.<>4__this.Bot.Graveyard)
						{
							if (material != null && material != albaz2 && material.IsMonster() && material.Attack >= 2500 && !material.IsCode(CS$<>8__locals1.<>4__this.cannotBeFusionMaterialIdList))
							{
								return (!CS$<>8__locals1.<>4__this.Util.IsTurn1OrMain2() && CS$<>8__locals1.<>4__this.Enemy.GetMonsterCount() == 0) | !CS$<>8__locals1.<>4__this.CheckWhetherShouldKeepInGrave(material);
							}
						}
						return false;
					}
				},
				{
					3410461,
					delegate
					{
						if (CS$<>8__locals1.<>4__this.Util.GetOneEnemyBetterThanMyBest(false, false) == null)
						{
							ClientCard albaz3 = CS$<>8__locals1.<>4__this.Bot.Graveyard.FirstOrDefault((ClientCard c) => c != null && c.IsOriginalCode(68468459));
							if (albaz3 == null)
							{
								albaz3 = CS$<>8__locals1.<>4__this.Bot.Graveyard.FirstOrDefault((ClientCard c) => c != null && c.IsCode(68468459));
							}
							foreach (ClientCard material2 in CS$<>8__locals1.<>4__this.Bot.Graveyard)
							{
								if (material2 != null && material2 != albaz3 && material2.IsMonster() && material2.HasRace(CardRace.Dragon) && !material2.IsCode(CS$<>8__locals1.<>4__this.cannotBeFusionMaterialIdList))
								{
									return true;
								}
							}
							return false;
						}
						return false;
					}
				},
				{
					24915933,
					() => CS$<>8__locals1.cards != null
				}
			}.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, Func<bool>> pair = enumerator.Current;
					target = CS$<>8__locals1.cards.FirstOrDefault((ClientCard card) => card.IsCode(pair.Key));
					if (target != null && pair.Value())
					{
						return pair.Key;
					}
				}
			}
			target = null;
			return 0;
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x00045B24 File Offset: 0x00043D24
		public bool DespianLuluwalilithActivate()
		{
			if (base.Card.Location != CardLocation.MonsterZone)
			{
				if (base.Card.Location == CardLocation.Grave)
				{
					if (this.CheckWhetherNegated(true, false, CardType.Monster))
					{
						return false;
					}
					if (this.CheckRemainInDeck(new int[] { 45883110, 95515789 }) > 0)
					{
						this.activatedCardIdList.Add(base.Card.Id + 1);
						return true;
					}
				}
				return false;
			}
			if (this.CheckWhetherNegated(true, true, CardType.Monster))
			{
				return false;
			}
			this.activatedCardIdList.Add(base.Card.Id);
			return true;
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x00045BBC File Offset: 0x00043DBC
		public bool SetForChimera()
		{
			if (base.Card.Level <= 4)
			{
				return false;
			}
			if (base.Bot.GetMonsterCount() > 0 || !base.Bot.HasInHandOrInSpellZone(34995106) || !base.Bot.HasInExtra(11321089))
			{
				return false;
			}
			if (base.DefaultCheckWhetherCardIdIsNegated(11321089) || this.CheckWhetherNegated(true, true, CardType.Monster))
			{
				return false;
			}
			if (base.Enemy.MonsterZone.All((ClientCard c) => c == null))
			{
				if (base.Enemy.SpellZone.All((ClientCard c) => c == null))
				{
					return false;
				}
			}
			for (int handIndex = 0; handIndex < base.Bot.Hand.Count - 1; handIndex++)
			{
				ClientCard hand = base.Bot.Hand[handIndex];
				if (hand.IsMonster() && !hand.IsCode(base.Card.Id) && !base.Card.IsCode(hand.Id))
				{
					for (int handIndex2 = handIndex + 1; handIndex2 < base.Bot.Hand.Count; handIndex2++)
					{
						ClientCard hand2 = base.Bot.Hand[handIndex2];
						if (hand2.IsMonster() && !hand2.IsCode(base.Card.Id) && !base.Card.IsCode(hand2.Id) && !hand2.IsCode(hand.Id) && !hand.IsCode(hand2.Id) && (base.Card.HasRace(CardRace.Dragon) || hand.HasRace(CardRace.Dragon) || hand2.HasRace(CardRace.Dragon)))
						{
							this.summoned = true;
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x00045DB0 File Offset: 0x00043FB0
		public bool AdvanceSummon()
		{
			if (base.Card.Level < 5)
			{
				return false;
			}
			List<ClientCard> releaseGoal = base.Bot.MonsterZone.Where((ClientCard c) => c != null && c.IsFaceup() && !c.IsDisabled() && c.IsCode(10158145)).ToList<ClientCard>();
			if (releaseGoal.Count > 0)
			{
				if (base.Card.Level <= 6)
				{
					base.AI.SelectMaterials(releaseGoal, 0);
					this.summoned = true;
					return true;
				}
				if (base.Card.Level >= 7)
				{
					if (releaseGoal.Count < 2)
					{
						ClientCard anotherMaterial = (from c in base.Bot.MonsterZone
							where c != null && !releaseGoal.Contains(c)
							orderby c.GetDefensePower()
							select c).FirstOrDefault<ClientCard>();
						if (anotherMaterial.GetDefensePower() > base.Card.Attack)
						{
							return false;
						}
						releaseGoal.Add(anotherMaterial);
					}
					if (releaseGoal.Count >= 2)
					{
						base.AI.SelectMaterials(releaseGoal, 0);
						this.summoned = true;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x00045EF8 File Offset: 0x000440F8
		public bool SpellSetCheck()
		{
			if (base.Duel.Phase == DuelPhase.Main1 && base.Bot.HasAttackingMonster() && base.Duel.Turn > 1)
			{
				return false;
			}
			int id = base.Card.Id;
			if (id <= 19271881)
			{
				if (id != 17751597)
				{
					if (id == 19271881)
					{
						if (!base.Bot.MonsterZone.Any((ClientCard c) => c != null && c.IsFaceup() && c.IsCode(this.albazFusionMonster)))
						{
							return false;
						}
					}
				}
				else if (!((base.Bot.Graveyard.Where((ClientCard c) => c.IsCode(this.albazFusionMonster)).Count<ClientCard>() > 1) | base.Bot.MonsterZone.Any((ClientCard c) => c != null && c.IsFaceup() && c.IsCode(this.albazFusionMonster))))
				{
					return false;
				}
			}
			else if (id != 29948294)
			{
				if (id != 32756828)
				{
					if (id == 36637374)
					{
						if (this.CheckRemainInDeck(new int[] { 62962630, 45883110 }) <= 0)
						{
							return false;
						}
					}
				}
				else
				{
					bool flag;
					if (!base.Bot.HasInGraveyard(18973184))
					{
						flag = base.Bot.MonsterZone.Any((ClientCard c) => c != null && c.IsFaceup() && c.HasSetcode(392));
					}
					else
					{
						flag = true;
					}
					if (!(flag | (base.Bot.HasInHand(60242223) && (base.Enemy.Graveyard.Any((ClientCard c) => this.CheckBystialCanBanish(c)) || base.Bot.Graveyard.Any((ClientCard c) => this.CheckBystialCanBanish(c))))))
					{
						return false;
					}
				}
			}
			else
			{
				bool flag2;
				if (!base.Bot.HasInMonstersZone(45883110, false, false, false))
				{
					if (this.CheckRemainInDeck(45883110) > 0)
					{
						flag2 = this.sendToGYThisTurn.Any((ClientCard c) => c.IsCode(41373230));
					}
					else
					{
						flag2 = false;
					}
				}
				else
				{
					flag2 = true;
				}
				if (!flag2)
				{
					return false;
				}
			}
			if (base.Card.IsTrap() || base.Card.HasType(CardType.QuickPlay))
			{
				List<int> avoid_list = new List<int>();
				int setForInfiniteImpermanence = 0;
				for (int i = 0; i < 5; i++)
				{
					if (base.Enemy.SpellZone[i] != null && base.Enemy.SpellZone[i].IsFaceup() && base.Bot.SpellZone[4 - i] == null)
					{
						avoid_list.Add(4 - i);
						setForInfiniteImpermanence += (int)Math.Pow(2.0, (double)(4 - i));
					}
				}
				if (!base.Bot.HasInHand(10045474))
				{
					this.SelectSTPlace(null, false, null);
					return true;
				}
				if (base.Card.IsCode(10045474))
				{
					base.AI.SelectPlace(setForInfiniteImpermanence);
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

		// Token: 0x06000D61 RID: 3425 RVA: 0x00046233 File Offset: 0x00044433
		protected override bool DefaultSetForDiabellze()
		{
			if (base.DefaultSetForDiabellze())
			{
				this.SelectSTPlace(base.Card, true, null);
				return true;
			}
			return false;
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x00046250 File Offset: 0x00044450
		public bool FloogateActivate()
		{
			if (base.Card.Owner != 1 && !base.Card.IsFloodgate())
			{
				return false;
			}
			if (this.CheckWhetherNegated(true, false, (CardType)0))
			{
				return false;
			}
			if (base.Executors.Any((CardExecutor e) => e != null && e.Type == ExecutorType.Activate && e.CardId == base.Card.Id))
			{
				return false;
			}
			if (base.Duel.LastChainPlayer == 1)
			{
				ClientCard lastChainCard = base.Util.GetLastChainCard();
				if (lastChainCard != null && lastChainCard.IsFaceup() && this.CheckCanBeTargeted(lastChainCard, true, CardType.Monster) && (lastChainCard.Location == CardLocation.MonsterZone || lastChainCard.Location == CardLocation.SpellZone))
				{
					base.AI.SelectCard(lastChainCard);
				}
			}
			return base.Duel.LastChainPlayer == 1;
		}

		// Token: 0x04001300 RID: 4864
		private const int SetcodeTimeLord = 74;

		// Token: 0x04001301 RID: 4865
		private const int SetcodePhantom = 219;

		// Token: 0x04001302 RID: 4866
		private const int SetcodeOrcust = 283;

		// Token: 0x04001303 RID: 4867
		private const int SetcodeBranded = 349;

		// Token: 0x04001304 RID: 4868
		private const int SetcodeDespain = 356;

		// Token: 0x04001305 RID: 4869
		private const int SetcodeBystial = 392;

		// Token: 0x04001306 RID: 4870
		private const int SetcodeHorus = 413;

		// Token: 0x04001307 RID: 4871
		private const int hintTimingMainEnd = 4;

		// Token: 0x04001308 RID: 4872
		private const int hintBattleStart = 8;

		// Token: 0x04001309 RID: 4873
		private List<int> notToNegateIdList = new List<int> { 58699500, 20343502, 25451383, 19403423 };

		// Token: 0x0400130A RID: 4874
		private List<int> cannotBeFusionMaterialIdList = new List<int>
		{
			3410461, 38811586, 79229522, 65029288, 30864377, 33964637, 87116928, 13735899, 28226490, 80453041,
			63845230, 2992467, 16366810, 40217358, 47346782, 50893987, 71459017, 84433295, 85101097
		};

		// Token: 0x0400130B RID: 4875
		private List<int> albazFusionMonster = new List<int> { 41373230, 1906812, 87746184, 70534340, 3410461, 44146295, 51409648, 38811586 };

		// Token: 0x0400130C RID: 4876
		private Dictionary<int, List<int>> DeckCountTable = new Dictionary<int, List<int>>
		{
			{
				3,
				new List<int> { 62962630, 14558127, 23434538, 10045474 }
			},
			{
				2,
				new List<int> { 68468459, 1984618, 6498706, 24224830 }
			},
			{
				1,
				new List<int>
				{
					32731036, 25451383, 60242223, 45484331, 45883110, 95515789, 19096726, 36577931, 34995106, 44362883,
					75500286, 81439173, 29948294, 36637374, 65681983, 82738008, 18973184, 19271881, 32756828, 17751597
				}
			}
		};

		// Token: 0x0400130D RID: 4877
		private List<int> dangerousDragonIdList = new List<int>
		{
			27548199, 92892239, 98630720, 9753964, 99585850, 24361622, 27572350, 69120785, 96402918, 74294676,
			42752141, 18511599, 35103106, 26268488
		};

		// Token: 0x0400130E RID: 4878
		private List<int> notToDestroySpellTrap = new List<int> { 50005218, 6767771 };

		// Token: 0x0400130F RID: 4879
		private List<int> targetNegateIdList = new List<int>
		{
			97268402, 10045474, 52038441, 78474168, 74003290, 67037924, 9753964, 66192538, 23204029, 73445448,
			35103106, 30286474, 45002991, 5795980, 38511382, 53742162, 30430448
		};

		// Token: 0x04001310 RID: 4880
		private bool summoned;

		// Token: 0x04001311 RID: 4881
		private bool enemyActivateMaxxC;

		// Token: 0x04001312 RID: 4882
		private bool enemyActivateLockBird;

		// Token: 0x04001313 RID: 4883
		private int dimensionShifterCount;

		// Token: 0x04001314 RID: 4884
		private bool enemyActivateInfiniteImpermanenceFromHand;

		// Token: 0x04001315 RID: 4885
		private bool theBystialLubellionSelecting;

		// Token: 0x04001316 RID: 4886
		private bool albionTheShroudedDragonSelecting;

		// Token: 0x04001317 RID: 4887
		private bool nadirActivated;

		// Token: 0x04001318 RID: 4888
		private bool fusionToGYFlag;

		// Token: 0x04001319 RID: 4889
		private bool spSummoningAlbaz;

		// Token: 0x0400131A RID: 4890
		private int cartesiaSummonGoal;

		// Token: 0x0400131B RID: 4891
		private int sanctifireSelectPositionCount;

		// Token: 0x0400131C RID: 4892
		private int quemSummonFlag;

		// Token: 0x0400131D RID: 4893
		private List<ClientCard> cartesiaMaterialList = new List<ClientCard>();

		// Token: 0x0400131E RID: 4894
		private List<ClientCard> brandedInRedMaterialList = new List<ClientCard>();

		// Token: 0x0400131F RID: 4895
		private List<int> infiniteImpermanenceList = new List<int>();

		// Token: 0x04001320 RID: 4896
		private List<ClientCard> currentNegateCardList = new List<ClientCard>();

		// Token: 0x04001321 RID: 4897
		private List<ClientCard> currentDestroyCardList = new List<ClientCard>();

		// Token: 0x04001322 RID: 4898
		private List<ClientCard> sendToGYThisTurn = new List<ClientCard>();

		// Token: 0x04001323 RID: 4899
		private List<int> activatedCardIdList = new List<int>();

		// Token: 0x04001324 RID: 4900
		private ClientCard fusionTarget;

		// Token: 0x04001325 RID: 4901
		private List<ClientCard> selectedFusionMaterial = new List<ClientCard>();

		// Token: 0x04001326 RID: 4902
		private List<ClientCard> enemyPlaceThisTurn = new List<ClientCard>();

		// Token: 0x02000253 RID: 595
		public class CardId
		{
			// Token: 0x04001327 RID: 4903
			public const int TheBystialLubellion = 32731036;

			// Token: 0x04001328 RID: 4904
			public const int AlbionTheShroudedDragon = 25451383;

			// Token: 0x04001329 RID: 4905
			public const int BystialSaronir = 60242223;

			// Token: 0x0400132A RID: 4906
			public const int AluberTheJesterOfDespia = 62962630;

			// Token: 0x0400132B RID: 4907
			public const int FallenOfAlbaz = 68468459;

			// Token: 0x0400132C RID: 4908
			public const int SpringansKitt = 45484331;

			// Token: 0x0400132D RID: 4909
			public const int GuidingQuemTheVirtuous = 45883110;

			// Token: 0x0400132E RID: 4910
			public const int BlazingCartesiaTheVirtuous = 95515789;

			// Token: 0x0400132F RID: 4911
			public const int TriBrigadeMercourier = 19096726;

			// Token: 0x04001330 RID: 4912
			public const int DespianTragedy = 36577931;

			// Token: 0x04001331 RID: 4913
			public const int NadirServant = 1984618;

			// Token: 0x04001332 RID: 4914
			public const int FusionDeployment = 6498706;

			// Token: 0x04001333 RID: 4915
			public const int BrandedInWhite = 34995106;

			// Token: 0x04001334 RID: 4916
			public const int BrandedFusion = 44362883;

			// Token: 0x04001335 RID: 4917
			public const int GoldSarcophagus = 75500286;

			// Token: 0x04001336 RID: 4918
			public const int FoolishBurial = 81439173;

			// Token: 0x04001337 RID: 4919
			public const int BrandedInHighSpirits = 29948294;

			// Token: 0x04001338 RID: 4920
			public const int BrandedOpening = 36637374;

			// Token: 0x04001339 RID: 4921
			public const int BrandedInRed = 82738008;

			// Token: 0x0400133A RID: 4922
			public const int BrandedLost = 18973184;

			// Token: 0x0400133B RID: 4923
			public const int BrightestBlazingBrandedKing = 19271881;

			// Token: 0x0400133C RID: 4924
			public const int BrandedBeast = 32756828;

			// Token: 0x0400133D RID: 4925
			public const int BrandedRetribution = 17751597;

			// Token: 0x0400133E RID: 4926
			public const int GuardianChimera = 11321089;

			// Token: 0x0400133F RID: 4927
			public const int AlbionTheSanctifireDragon = 38811586;

			// Token: 0x04001340 RID: 4928
			public const int MirrorjadeTheIcebladeDragon = 44146295;

			// Token: 0x04001341 RID: 4929
			public const int BorreloadFuriousDragon = 92892239;

			// Token: 0x04001342 RID: 4930
			public const int LubellionTheSearingDragon = 70534340;

			// Token: 0x04001343 RID: 4931
			public const int AlbaLenatusTheAbyssDragon = 3410461;

			// Token: 0x04001344 RID: 4932
			public const int GranguignolTheDuskDragon = 24915933;

			// Token: 0x04001345 RID: 4933
			public const int DespianQuaeritis = 72272462;

			// Token: 0x04001346 RID: 4934
			public const int SprindTheIrondashDragon = 1906812;

			// Token: 0x04001347 RID: 4935
			public const int TitanikladTheAshDragon = 41373230;

			// Token: 0x04001348 RID: 4936
			public const int RindbrummTheStrikingDragon = 51409648;

			// Token: 0x04001349 RID: 4937
			public const int AlbionTheBrandedDragon = 87746184;

			// Token: 0x0400134A RID: 4938
			public const int DespianLuluwalilith = 53971455;

			// Token: 0x0400134B RID: 4939
			public const int NaturalExterio = 99916754;

			// Token: 0x0400134C RID: 4940
			public const int NaturalBeast = 33198837;

			// Token: 0x0400134D RID: 4941
			public const int ImperialOrder = 61740673;

			// Token: 0x0400134E RID: 4942
			public const int SwordsmanLV7 = 37267041;

			// Token: 0x0400134F RID: 4943
			public const int RoyalDecree = 51452091;

			// Token: 0x04001350 RID: 4944
			public const int Number41BagooskatheTerriblyTiredTapir = 90590303;

			// Token: 0x04001351 RID: 4945
			public const int InspectorBoarder = 15397015;

			// Token: 0x04001352 RID: 4946
			public const int SkillDrain = 82732705;

			// Token: 0x04001353 RID: 4947
			public const int DimensionShifter = 91800273;

			// Token: 0x04001354 RID: 4948
			public const int MacroCosmos = 30241314;

			// Token: 0x04001355 RID: 4949
			public const int DimensionalFissure = 81674782;

			// Token: 0x04001356 RID: 4950
			public const int BanisheroftheRadiance = 94853057;

			// Token: 0x04001357 RID: 4951
			public const int BanisheroftheLight = 61528025;

			// Token: 0x04001358 RID: 4952
			public const int KashtiraAriseHeart = 48626373;

			// Token: 0x04001359 RID: 4953
			public const int AccesscodeTalker = 86066372;

			// Token: 0x0400135A RID: 4954
			public const int GhostMournerMoonlitChill = 52038441;

			// Token: 0x0400135B RID: 4955
			public const int NibiruThePrimalBeing = 27204311;
		}
	}
}
