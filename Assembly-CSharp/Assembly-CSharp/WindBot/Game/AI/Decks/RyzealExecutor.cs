using System;
using System.Collections.Generic;
using System.Linq;
using YGOSharp.OCGWrapper;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x020003A0 RID: 928
	[Deck("Ryzeal", "AI_Ryzeal", "Normal")]
	public class RyzealExecutor : DefaultExecutor
	{
		// Token: 0x06001BC7 RID: 7111 RVA: 0x000A61A4 File Offset: 0x000A43A4
		public RyzealExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 24224830, new Func<bool>(this.CalledbytheGraveActivate));
			base.AddExecutor(ExecutorType.Activate, 65681983, new Func<bool>(this.CrossoutDesignatorActivate));
			base.AddExecutor(ExecutorType.Activate, 10045474, new Func<bool>(this.InfiniteImpermanenceActivate));
			base.AddExecutor(ExecutorType.Activate, 97268402, new Func<bool>(this.EffectVeilerActivate));
			base.AddExecutor(ExecutorType.Activate, 59438930, new Func<bool>(this.GhostOgreAndSnowRabbitActivate));
			base.AddExecutor(ExecutorType.Activate, 14558127, new Func<bool>(this.AshBlossomActivate));
			base.AddExecutor(ExecutorType.Activate, 6798031, new Func<bool>(this.RyzealCrossActivateCard));
			base.AddExecutor(ExecutorType.Activate, 46772449, new Func<bool>(this.EvilswarmExcitonKnightActivate));
			base.AddExecutor(ExecutorType.Activate, 34909328, new Func<bool>(this.RyzealDeadnaderActivate));
			base.AddExecutor(ExecutorType.Activate, 7511613, new Func<bool>(this.RyzealDuodriveActivate));
			base.AddExecutor(ExecutorType.Activate, 45852939, new Func<bool>(this.TwinsOfTheEclipseActivate));
			base.AddExecutor(ExecutorType.Activate, 21044178, new Func<bool>(this.AbyssDwellerActivate));
			base.AddExecutor(ExecutorType.Activate, 6983839, new Func<bool>(this.TornadoDragonActivate));
			base.AddExecutor(ExecutorType.Activate, 94145021, new Func<bool>(this.LockBirdActivate));
			base.AddExecutor(ExecutorType.Activate, 84192580, new Func<bool>(this.MulcharmyPuruliaActivate));
			base.AddExecutor(ExecutorType.Activate, 87126721, new Func<bool>(this.MulcharmyNyalusActivate));
			base.AddExecutor(ExecutorType.Activate, 42141493, new Func<bool>(this.MulcharmyFuwalosActivate));
			base.AddExecutor(ExecutorType.Activate, 23434538, new Func<bool>(this.MaxxCActivate));
			base.AddExecutor(ExecutorType.Activate, 85106525, new Func<bool>(this.BonfireActivate));
			base.AddExecutor(ExecutorType.Activate, 8728498, new Func<bool>(this.DonnerDaggerFurHireActivate));
			base.AddExecutor(ExecutorType.Activate, 66011101, new Func<bool>(this.Number60DugaresTheTimelessActivate));
			base.AddExecutor(ExecutorType.Activate, 25311006, new Func<bool>(this.TripleTacticsTalentActivate));
			base.AddExecutor(ExecutorType.Activate, 85106525, new Func<bool>(this.BonfireActivateToSearchNecessary));
			base.AddExecutor(ExecutorType.Activate, 7477101, new Func<bool>(this.SeventhTachyonActivate));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(this.ChangePositionFirst));
			base.AddExecutor(ExecutorType.SpSummon, 46772449, new Func<bool>(this.EvilswarmExcitonKnightSpSummon));
			base.AddExecutor(ExecutorType.SpSummon, new Func<bool>(this.LessSpSummonExtra));
			base.AddExecutor(ExecutorType.SpSummon, 7511613, new Func<bool>(this.FirstRyzealDuodriveSpSummon));
			base.AddExecutor(ExecutorType.SpSummon, new Func<bool>(this.SecondXyzSummon));
			base.AddExecutor(ExecutorType.SpSummon, 45852939, new Func<bool>(this.TwinsOfTheEclipseSpSummon));
			base.AddExecutor(ExecutorType.SpSummon, new Func<bool>(this.FinalXyzSummon));
			base.AddExecutor(ExecutorType.SpSummon, 8728498, new Func<bool>(this.DonnerDaggerFurHireSpSummon));
			base.AddExecutor(ExecutorType.Activate, 35261759, new Func<bool>(this.PotOfDesireActivateForContinue));
			base.AddExecutor(ExecutorType.Activate, 60394026, new Func<bool>(this.RyzealPlugInActivateFirst));
			base.AddExecutor(ExecutorType.Activate, 72238166, new Func<bool>(this.NodeRyzealActivateFirst));
			base.AddExecutor(ExecutorType.Activate, 6798031, new Func<bool>(this.RyzealCrossActivateRecycleFirst));
			base.AddExecutor(ExecutorType.SpSummon, 8633261, new Func<bool>(this.IceRyzealSpSummonFirst));
			base.AddExecutor(ExecutorType.SpSummon, 72238166, new Func<bool>(this.NodeRyzealSpSummonFirst));
			base.AddExecutor(ExecutorType.Summon, 34022970, new Func<bool>(this.ExRyzealSummon));
			base.AddExecutor(ExecutorType.SpSummon, 34022970, new Func<bool>(this.ExRyzealSpSummon));
			base.AddExecutor(ExecutorType.SpSummon, 35844557, new Func<bool>(this.ThodeRyzealSpSummon));
			base.AddExecutor(ExecutorType.Summon, 8633261, new Func<bool>(this.IceRyzealSummon));
			base.AddExecutor(ExecutorType.SpSummon, 34022970, new Func<bool>(this.ExRyzealSpSummonLater));
			base.AddExecutor(ExecutorType.Summon, 35844557, new Func<bool>(this.ThodeRyzealSummon));
			base.AddExecutor(ExecutorType.SpSummon, 72238166, new Func<bool>(this.NodeRyzealSpSummon));
			base.AddExecutor(ExecutorType.Summon, new Func<bool>(this.Level4Summon));
			base.AddExecutor(ExecutorType.Activate, 72238166, new Func<bool>(this.NodeRyzealActivate));
			base.AddExecutor(ExecutorType.Activate, 60394026, new Func<bool>(this.RyzealPlugInActivate));
			base.AddExecutor(ExecutorType.SpSummon, 8633261, new Func<bool>(this.IceRyzealSpSummon));
			base.AddExecutor(ExecutorType.Activate, 9940036, new Func<bool>(this.MereologicAggregatorActivateFirst));
			base.AddExecutor(ExecutorType.Activate, 8633261, new Func<bool>(this.IceRyzealActivate));
			base.AddExecutor(ExecutorType.Activate, 35844557, new Func<bool>(this.ThodeRyzealActivate));
			base.AddExecutor(ExecutorType.Activate, 34022970, new Func<bool>(this.ExRyzealActivate));
			base.AddExecutor(ExecutorType.Activate, 9940036, new Func<bool>(this.MereologicAggregatorActivateLater));
			base.AddExecutor(ExecutorType.Activate, 6798031, new Func<bool>(this.RyzealCrossActivateRecycleLater));
			base.AddExecutor(ExecutorType.Activate, 35261759, new Func<bool>(this.PotOfDesiresActivate));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(base.DefaultMonsterRepos));
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(this.SpellSetCheck));
		}

		// Token: 0x06001BC8 RID: 7112 RVA: 0x000A6B20 File Offset: 0x000A4D20
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

		// Token: 0x06001BC9 RID: 7113 RVA: 0x000A6B90 File Offset: 0x000A4D90
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

		// Token: 0x06001BCA RID: 7114 RVA: 0x000A6BE8 File Offset: 0x000A4DE8
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

		// Token: 0x06001BCB RID: 7115 RVA: 0x000A6C24 File Offset: 0x000A4E24
		public int CheckRemainInDeck(params int[] ids)
		{
			int sum = 0;
			foreach (int id in ids)
			{
				sum += this.CheckRemainInDeck(id);
			}
			return sum;
		}

		// Token: 0x06001BCC RID: 7116 RVA: 0x000A6C54 File Offset: 0x000A4E54
		public bool CheckWhetherHaveFinalMonster()
		{
			foreach (ClientCard monster in base.Bot.MonsterZone)
			{
				if (monster != null)
				{
					if (monster.IsCode(90590303) && monster.IsDefense())
					{
						return true;
					}
					if (monster.IsCode(21044178) && monster.Overlays.Count<int>() > 0)
					{
						return true;
					}
					if (monster.IsCode(34909328) && monster.Overlays.Count<int>() > 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001BCD RID: 7117 RVA: 0x000A6CD4 File Offset: 0x000A4ED4
		public bool CheckWhetherNegated(bool disablecheck = true, bool toFieldCheck = false, CardType type = (CardType)0, bool ignore41 = false)
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
				if (((toFieldCheck && (type & CardType.Link) != (CardType)0) || base.Card.IsDefense()) && (base.Enemy.MonsterZone.Any((ClientCard card) => this.CheckNumber41(card, ignore41)) || base.Bot.MonsterZone.Any((ClientCard card) => this.CheckNumber41(card, ignore41))))
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

		// Token: 0x06001BCE RID: 7118 RVA: 0x000A6E28 File Offset: 0x000A5028
		public bool CheckNumber41(ClientCard card, bool ignoreSelf41 = false)
		{
			return card != null && card.IsFaceup() && card.IsCode(90590303) && card.IsDefense() && !card.IsDisabled() && (!ignoreSelf41 || card.Controller == 0);
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x000A6E64 File Offset: 0x000A5064
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

		// Token: 0x06001BD0 RID: 7120 RVA: 0x000A6FA0 File Offset: 0x000A51A0
		public bool CheckLastChainShouldNegated()
		{
			ClientCard lastcard = base.Util.GetLastChainCard();
			return lastcard != null && lastcard.Controller == 1 && this.CheckCardShouldNegate(lastcard);
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x000A6FD0 File Offset: 0x000A51D0
		public bool CheckCardShouldNegate(ClientCard card)
		{
			return card != null && (!card.IsMonster() || !card.HasSetcode(74) || base.Duel.Phase != DuelPhase.Standby) && !this.NotToNegateIdList.Contains(card.Id) && (!card.HasSetcode(286) || card.Location != CardLocation.Hand) && (!card.IsMonster() || card.Location != CardLocation.MonsterZone || !card.HasPosition(CardPosition.Defence) || (!base.Enemy.MonsterZone.Any((ClientCard c) => this.CheckNumber41(c, false)) && !base.Bot.MonsterZone.Any((ClientCard c) => this.CheckNumber41(c, false)))) && !base.DefaultCheckWhetherCardIsNegated(card) && (base.Duel.Player != 1 || !card.IsCode(new int[] { 84192580, 42141493, 87126721 })) && !card.IsDisabled();
		}

		// Token: 0x06001BD2 RID: 7122 RVA: 0x000A70CC File Offset: 0x000A52CC
		public bool CheckCardShouldNegate(ChainInfo chainInfo)
		{
			if (chainInfo == null)
			{
				return false;
			}
			ClientCard card = chainInfo.RelatedCard;
			return card != null && (!card.IsMonster() || !card.HasSetcode(74) || base.Duel.Phase != DuelPhase.Standby) && !this.NotToNegateIdList.Contains(card.Id) && (!card.HasSetcode(286) || card.Location != CardLocation.Hand) && (!card.IsMonster() || !chainInfo.HasLocation(CardLocation.MonsterZone) || !chainInfo.HasPosition(CardPosition.Defence) || (!base.Enemy.MonsterZone.Any((ClientCard c) => this.CheckNumber41(c, false)) && !base.Bot.MonsterZone.Any((ClientCard c) => this.CheckNumber41(c, false)))) && !base.DefaultCheckWhetherCardIsNegated(card) && (base.Duel.Player != 1 || !card.IsCode(new int[] { 84192580, 42141493, 87126721 })) && !card.IsDisabled();
		}

		// Token: 0x06001BD3 RID: 7123 RVA: 0x000A71D1 File Offset: 0x000A53D1
		public bool CheckAtAdvantage()
		{
			return this.GetProblematicEnemyMonster(0, false, false, (CardType)0) == null && (base.Duel.Player == 0 || base.Bot.GetMonsterCount() > 0);
		}

		// Token: 0x06001BD4 RID: 7124 RVA: 0x000A71FD File Offset: 0x000A53FD
		public bool CheckShouldNoMoreSpSummon()
		{
			return this.CheckAtAdvantage() && this.enemyActivateMaxxC && !this.lockBirdSolved && (base.Duel.Turn == 1 || base.Duel.Phase >= DuelPhase.Main2);
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x000A723C File Offset: 0x000A543C
		public bool CheckShouldNoMoreSpSummon(CardLocation loc)
		{
			return this.CheckShouldNoMoreSpSummon() || (!this.lockBirdSolved && (base.Duel.Turn <= 1 || base.Duel.Phase >= DuelPhase.Main2) && ((this.enemyActivatePurulia && (loc & CardLocation.Hand) != (CardLocation)0) || (this.enemyActivateFuwalos && (loc & (CardLocation)65) != (CardLocation)0) || (this.enemyActivateNyalus && (loc & (CardLocation)48) != (CardLocation)0)));
		}

		// Token: 0x06001BD6 RID: 7126 RVA: 0x000A72AD File Offset: 0x000A54AD
		public bool CheckWhetherCanSummon()
		{
			return base.Duel.Player == 0 && base.Duel.Phase < DuelPhase.End && this.summonCount > 0;
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x000A72DC File Offset: 0x000A54DC
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

		// Token: 0x06001BD8 RID: 7128 RVA: 0x000A73E8 File Offset: 0x000A55E8
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

		// Token: 0x06001BD9 RID: 7129 RVA: 0x000A7638 File Offset: 0x000A5838
		public bool CheckShouldNotIgnore(ClientCard cards, bool ignore = false)
		{
			return !ignore || (!this.currentDestroyCardList.Contains(cards) && !this.currentNegateCardList.Contains(cards));
		}

		// Token: 0x06001BDA RID: 7130 RVA: 0x000A7660 File Offset: 0x000A5860
		public bool CheckCanContinueSummon(bool skipDuodriver = false)
		{
			bool checkFlag = this.summonCount > 0 && !this.activatedCardIdList.Contains(8633261) && base.Bot.HasInHand(8633261) && !base.DefaultCheckWhetherCardIdIsNegated(8633261);
			if (base.Bot.HasInHand(35844557) && !this.spSummonedCardIdList.Contains(35844557) && !this.activatedCardIdList.Contains(35844557) && !base.DefaultCheckWhetherCardIdIsNegated(35844557))
			{
				checkFlag |= base.Bot.MonsterZone.Any((ClientCard c) => c != null && c.IsFaceup() && c.HasSetcode(446));
				checkFlag |= base.Bot.Graveyard.Any((ClientCard c) => c != null && c.IsFaceup() && c.HasSetcode(446));
			}
			checkFlag |= !this.spSummonedCardIdList.Contains(34022970) && !this.activatedCardIdList.Contains(34022970) && base.Bot.HasInHand(34022970) && !base.DefaultCheckWhetherCardIdIsNegated(34022970) && !this.CheckWhetherWillbeRemoved();
			bool flag = checkFlag;
			bool flag2;
			if (!this.activatedCardIdList.Contains(7511614) && base.Bot.HasInExtra(7511613) && !base.DefaultCheckWhetherCardIdIsNegated(7511613) && !this.CheckWhetherNegated(true, true, CardType.Monster, false) && this.summonCount > 0)
			{
				if (base.Bot.Hand.Count((ClientCard c) => c.Level == 4) > 0 && this.GetLevel4CountOnField() == 1 && !this.lockBirdSolved)
				{
					flag2 = !skipDuodriver;
					goto IL_01CF;
				}
			}
			flag2 = false;
			IL_01CF:
			return flag || flag2;
		}

		// Token: 0x06001BDB RID: 7131 RVA: 0x000A7840 File Offset: 0x000A5A40
		public List<ClientCard> GetDangerousCardinEnemyGrave(bool onlyMonster = false)
		{
			List<ClientCard> list = base.Enemy.Graveyard.GetMatchingCards((ClientCard card) => (!onlyMonster || card.IsMonster()) && (card.HasSetcode(283) || card.HasSetcode(4315) || card.HasSetcode(413))).ToList<ClientCard>();
			List<int> dangerMonsterIdList = new List<int> { 99937011, 63542003, 9411399, 28954097, 30680659 };
			list.AddRange(base.Enemy.Graveyard.GetMatchingCards((ClientCard card) => dangerMonsterIdList.Contains(card.Id)));
			return list;
		}

		// Token: 0x06001BDC RID: 7132 RVA: 0x000A78E0 File Offset: 0x000A5AE0
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
				where c.IsFaceup() && !this.currentDestroyCardList.Contains(c) && c.HasType((CardType)17694720) && this.CheckCanBeTargeted(c, canBeTarget, selfType) && !this.NotToDestroySpellTrap.Contains(c.Id)
				select c).ToList<ClientCard>();
			if (spells.Count > 0 && !ignoreSpells)
			{
				resultList.AddRange(this.ShuffleList<ClientCard>(spells));
			}
			return resultList;
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x000A7BC4 File Offset: 0x000A5DC4
		public List<ClientCard> GetNormalEnemyTargetList(bool canBeTarget = true, bool ignoreCurrentDestroy = false, CardType selfType = (CardType)0, bool forNegate = false)
		{
			List<ClientCard> targetList = this.GetProblematicEnemyCardList(canBeTarget, false, selfType);
			List<ClientCard> enemyMonster = (from card in base.Enemy.GetMonsters()
				where card.IsFaceup() && !targetList.Contains(card) && (!ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(card)) && (!forNegate || (!card.IsDisabled() && card.HasType(CardType.Effect)))
				select card).ToList<ClientCard>();
			enemyMonster.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			enemyMonster.Reverse();
			targetList.AddRange(enemyMonster);
			targetList.AddRange(this.ShuffleList<ClientCard>((from card in base.Enemy.GetSpells()
				where (!ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(card)) && this.enemyPlaceThisTurn.Contains(card) && card.IsFacedown()
				select card).ToList<ClientCard>()));
			targetList.AddRange(this.ShuffleList<ClientCard>((from card in base.Enemy.GetSpells()
				where (!ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(card)) && !this.enemyPlaceThisTurn.Contains(card) && card.IsFacedown()
				select card).ToList<ClientCard>()));
			targetList.AddRange(this.ShuffleList<ClientCard>((from card in base.Enemy.GetMonsters()
				where card.IsFacedown() && (!ignoreCurrentDestroy || !this.currentDestroyCardList.Contains(card)) && (!forNegate || (!card.IsDisabled() && card.HasType(CardType.Effect)))
				select card).ToList<ClientCard>()));
			return targetList;
		}

		// Token: 0x06001BDE RID: 7134 RVA: 0x000A7CE0 File Offset: 0x000A5EE0
		public List<ClientCard> GetMonsterListForTargetNegate(bool canBeTarget = false, CardType selfType = (CardType)0)
		{
			List<ClientCard> resultList = new List<ClientCard>();
			if (this.CheckWhetherNegated(true, false, (CardType)0, false))
			{
				return resultList;
			}
			ClientCard target = base.Enemy.MonsterZone.FirstOrDefault((ClientCard card) => ((card != null) ? card.Data : null) != null && card.IsMonsterShouldBeDisabledBeforeItUseEffect() && card.IsFaceup() && !card.IsShouldNotBeTarget() && this.CheckCanBeTargeted(card, canBeTarget, selfType) && !this.currentNegateCardList.Contains(card));
			if (target != null)
			{
				resultList.Add(target);
			}
			Func<ClientCard, bool> <>9__1;
			Func<ClientCard, bool> <>9__2;
			foreach (ClientCard chainingCard in base.Duel.CurrentChain)
			{
				if (chainingCard.Location == CardLocation.MonsterZone && chainingCard.Controller == 1 && !chainingCard.IsDisabled() && this.CheckCanBeTargeted(chainingCard, canBeTarget, selfType) && !this.currentNegateCardList.Contains(chainingCard))
				{
					if (chainingCard.HasPosition(CardPosition.Defence))
					{
						IEnumerable<ClientCard> monsterZone = base.Bot.MonsterZone;
						Func<ClientCard, bool> func;
						if ((func = <>9__1) == null)
						{
							func = (<>9__1 = (ClientCard c) => this.CheckNumber41(c, false));
						}
						monsterZone.Any(func);
						IEnumerable<ClientCard> monsterZone2 = base.Enemy.MonsterZone;
						Func<ClientCard, bool> func2;
						if ((func2 = <>9__2) == null)
						{
							func2 = (<>9__2 = (ClientCard c) => this.CheckNumber41(c, false));
						}
						monsterZone2.Any(func2);
					}
					resultList.Add(chainingCard);
				}
			}
			return resultList;
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x000A7E50 File Offset: 0x000A6050
		public List<ClientCard> GetLevel4OnField(Func<ClientCard, bool> filter)
		{
			return (from c in base.Bot.GetMonsters()
				where (filter == null || filter(c)) && c.IsFaceup() && !c.HasType((CardType)75497472) && c.Level == 4
				orderby c.GetDefensePower()
				select c).ToList<ClientCard>();
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x000A7EAF File Offset: 0x000A60AF
		public int GetLevel4CountOnField()
		{
			return base.Bot.GetMonsters().Count((ClientCard c) => c.IsFaceup() && !c.HasType((CardType)75497472) && c.Level == 4);
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x000A7EE0 File Offset: 0x000A60E0
		public int GetLevel4FinalCountOnField(bool checkSupport, out bool hasNode)
		{
			int level4Count = this.GetLevel4CountOnField();
			if (base.Bot.HasInHand(34022970) && !this.spSummonedCardIdList.Contains(34022970) && !this.CheckWhetherWillbeRemoved())
			{
				if (!checkSupport)
				{
					if (this.activatedCardIdList.Contains(34022970))
					{
						goto IL_007D;
					}
					if (!base.Bot.MonsterZone.All((ClientCard c) => c != null && (c.IsFacedown() || (!c.HasType(CardType.Link) && c.Level == 4))))
					{
						goto IL_007D;
					}
				}
				level4Count++;
			}
			IL_007D:
			if (base.Bot.HasInHand(35844557) && !this.spSummonedCardIdList.Contains(35844557))
			{
				if ((base.Bot.MonsterZone.Any((ClientCard c) => c != null && c.IsFaceup() && c.HasSetcode(446)) | base.Bot.Graveyard.Any((ClientCard c) => c != null && c.IsFaceup() && c.HasSetcode(446))) && (checkSupport || !this.activatedCardIdList.Contains(35844557)))
				{
					level4Count++;
				}
			}
			hasNode = base.Bot.MonsterZone.Any((ClientCard c) => c != null && c.IsFaceup() && c.IsCode(72238166) && !c.IsDisabled());
			if (base.Bot.HasInHand(72238166) && !this.spSummonedCardIdList.Contains(72238166))
			{
				if (base.Bot.MonsterZone.Any((ClientCard c) => c != null && c.IsFaceup() && c.HasType(CardType.Xyz)) | base.Bot.Graveyard.Any((ClientCard c) => c != null && c.IsFaceup() && c.HasType(CardType.Xyz)))
				{
					hasNode = true;
					level4Count++;
				}
			}
			if (base.Bot.HasInHand(60394026) && !this.CheckWhetherNegated(true, true, CardType.Spell, false) && checkSupport)
			{
				bool flag = false;
				List<ClientCard> graveTargetList = base.Bot.Graveyard.Where((ClientCard c) => c != null && c.IsFaceup() && c.HasSetcode(446) && !c.HasType(CardType.Xyz) && c.Level == 4).ToList<ClientCard>();
				bool flag2 = flag | (graveTargetList.Count<ClientCard>() > 0);
				hasNode |= graveTargetList.Any((ClientCard c) => c.IsCode(72238166));
				List<ClientCard> banishedTargetList = base.Bot.Banished.Where((ClientCard c) => c != null && c.IsFaceup() && c.HasSetcode(446) && !c.HasType(CardType.Xyz) && c.Level == 4).ToList<ClientCard>();
				bool flag3 = flag2 | (banishedTargetList.Count<ClientCard>() > 0);
				hasNode |= banishedTargetList.Any((ClientCard c) => c.IsCode(72238166));
				if (flag3)
				{
					level4Count++;
				}
			}
			hasNode &= !this.CheckWhetherWillbeRemoved() && !this.activatedCardIdList.Contains(72238166) && !base.DefaultCheckWhetherCardIdIsNegated(72238166);
			hasNode &= base.Bot.Graveyard.Any((ClientCard c) => c != null && c.IsMonster() && c.HasSetcode(446) && !c.IsCode(72238166) && c.Level == 4);
			if (hasNode)
			{
				if (base.Bot.Graveyard.Any((ClientCard c) => c != null && c.IsFaceup() && c.HasSetcode(446) && !c.HasType(CardType.Xyz) && c.Level == 4 && !c.IsCode(72238166)) && (checkSupport || this.GetCostFromHandAndField(null, false).Count<ClientCard>() > 0))
				{
					level4Count++;
				}
			}
			if (checkSupport)
			{
				int checkHandCount = 2;
				if (this.summonCount > 0)
				{
					if (base.Bot.Hand.Any((ClientCard c) => c.Level == 4 && !c.IsCode(new int[] { 34022970, 35844557, 72238166 })))
					{
						level4Count++;
						checkHandCount++;
					}
				}
				if (base.Bot.Hand.Count<ClientCard>() >= checkHandCount && base.Bot.HasInHand(8633261) && !this.spSummonedCardIdList.Contains(8633261) && !this.CheckWhetherWillbeRemoved())
				{
					level4Count++;
				}
			}
			return level4Count;
		}

		// Token: 0x06001BE2 RID: 7138 RVA: 0x000A8308 File Offset: 0x000A6508
		public List<ClientCard> GetCostFromHandAndFieldFirst(ClientCard exceptCard)
		{
			return base.Bot.MonsterZone.Where((ClientCard c) => c != null && !c.IsDisabled() && c.IsCode(this.NeedIceToSolveIdList) && c != exceptCard && !c.HasType(CardType.Token)).ToList<ClientCard>();
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x000A834C File Offset: 0x000A654C
		public List<ClientCard> GetCostFromHandAndField(ClientCard exceptCard, bool sendNotNecessary)
		{
			List<ClientCard> resultList = this.GetCostFromHandAndFieldFirst(exceptCard);
			if (!this.activatedCardIdList.Contains(45852940))
			{
				List<ClientCard> xyzList = base.Bot.Graveyard.Where((ClientCard c) => c.HasType(CardType.Xyz)).ToList<ClientCard>();
				ClientCard twins = base.Bot.MonsterZone.FirstOrDefault((ClientCard c) => c != null && c.IsCode(45852939) && !resultList.Contains(c));
				if (twins == null)
				{
					twins = base.Bot.SpellZone.FirstOrDefault((ClientCard c) => c != null && c.IsCode(45852939) && !resultList.Contains(c));
				}
				if (twins != null)
				{
					int twinsXyzCount = 0;
					foreach (int num in twins.Overlays)
					{
						NamedCard cardData = NamedCard.Get(num);
						if (cardData != null && cardData.HasType(CardType.Xyz))
						{
							twinsXyzCount++;
						}
					}
					bool flag = (twinsXyzCount >= 2) | (twinsXyzCount > 0 && xyzList.Count<ClientCard>() > 0);
					bool flag2;
					if (xyzList.Count<ClientCard>() > 1)
					{
						flag2 = xyzList.Count((ClientCard c) => c.IsCanRevive()) > 0;
					}
					else
					{
						flag2 = false;
					}
					if (flag || flag2)
					{
						resultList.Add(twins);
					}
				}
			}
			if (base.Bot.HasInSpellZone(6798031, true, true))
			{
				if (base.Bot.HasInExtra(7511613) && !this.activatedCardIdList.Contains(7511614) && !this.lockBirdSolved)
				{
					bool checkOverlay = true;
					ClientCard duoDrive = base.Bot.MonsterZone.FirstOrDefault((ClientCard c) => c != null && c.IsCode(7511613) && !resultList.Contains(c));
					if (duoDrive == null)
					{
						checkOverlay = false;
						duoDrive = base.Bot.SpellZone.FirstOrDefault((ClientCard c) => c != null && c.IsCode(7511613) && !resultList.Contains(c));
					}
					if (duoDrive != null)
					{
						int overlayCount = base.Bot.MonsterZone.Sum(delegate(ClientCard c)
						{
							if (c == null)
							{
								return 0;
							}
							return c.Overlays.Count<int>();
						});
						if (!checkOverlay || overlayCount < 2)
						{
							resultList.Add(duoDrive);
						}
					}
				}
				if (base.Bot.HasInExtra(34909328))
				{
					ClientCard deadnader = base.Bot.MonsterZone.FirstOrDefault((ClientCard c) => c != null && c.IsCode(34909328) && c.Overlays.Count<int>() == 0 && !resultList.Contains(c));
					if (deadnader != null)
					{
						resultList.Add(deadnader);
					}
				}
			}
			List<ClientCard> monstersInSpellZone = base.Bot.SpellZone.Where((ClientCard c) => c != null && c.Data != null && c.Data.HasType(CardType.Monster) && !c.Data.HasType((CardType)16793600) && !resultList.Contains(c)).ToList<ClientCard>();
			resultList.AddRange(monstersInSpellZone);
			List<ClientCard> enemyMonsters = base.Bot.MonsterZone.Where((ClientCard c) => c != null && !resultList.Contains(c) && c.Owner == 1).ToList<ClientCard>();
			resultList.AddRange(enemyMonsters);
			if (sendNotNecessary)
			{
				List<ClientCard> xyzMonsterWithNoMaterial = (from c in base.Bot.MonsterZone
					where c != null && c.HasType(CardType.Xyz) && c.GetDefensePower() < 2500 && c.Overlays.Count<int>() == 0 && !resultList.Contains(c)
					orderby c.GetDefensePower()
					select c).ToList<ClientCard>();
				resultList.AddRange(xyzMonsterWithNoMaterial);
				List<int> unimportantList = new List<int> { 87126721, 84192580, 42141493, 7477101 };
				resultList.AddRange(base.Bot.Hand.Where((ClientCard c) => c != null && c.IsCode(unimportantList) && !resultList.Contains(c)));
				using (List<int>.Enumerator enumerator = new List<int> { 8633261, 35844557, 34022970 }.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int checkId2 = enumerator.Current;
						if (this.summonCount == 0 && this.spSummonedCardIdList.Contains(checkId2))
						{
							List<ClientCard> ryzealList = base.Bot.Hand.Where((ClientCard c) => c != null && c != exceptCard && !resultList.Contains(c) && c.IsCode(checkId2)).ToList<ClientCard>();
							resultList.AddRange(ryzealList);
						}
					}
				}
				using (IEnumerator<ClientCard> enumerator2 = base.Bot.Hand.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						ClientCard card = enumerator2.Current;
						if (base.Bot.Hand.Count((ClientCard c) => c != null && !resultList.Contains(c) && c.IsCode(card.Id)) > 1)
						{
							resultList.Add(card);
						}
					}
				}
				if (resultList.Count<ClientCard>() == 0)
				{
					using (List<int>.Enumerator enumerator = new List<int> { 65681983, 24224830, 10045474, 59438930, 94145021, 14558127, 23434538 }.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							int checkId = enumerator.Current;
							List<ClientCard> costList = base.Bot.Hand.Where((ClientCard c) => c != null && c != exceptCard && !resultList.Contains(c) && c.IsCode(checkId)).ToList<ClientCard>();
							resultList.AddRange(costList);
						}
					}
				}
			}
			return resultList;
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x000A8970 File Offset: 0x000A6B70
		public int GetBotCurrentTotalAttack(List<ClientCard> exceptList = null)
		{
			if (base.Util.IsTurn1OrMain2() || this.botSolvedCardIdList.Contains(46772449))
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

		// Token: 0x06001BE5 RID: 7141 RVA: 0x000A8A10 File Offset: 0x000A6C10
		public int GetNegateEffectCount()
		{
			return 0 + ((base.GetCalledbytheGraveIdCount(23434538) < 2 && base.Bot.HasInHand(23434538)) ? 1 : 0) + ((base.GetCalledbytheGraveIdCount(14558127) < 2 && base.Bot.HasInHand(14558127)) ? 1 : 0) + ((base.GetCalledbytheGraveIdCount(97268402) < 2 && base.Bot.HasInHand(97268402)) ? 1 : 0) + ((base.GetCalledbytheGraveIdCount(59438930) < 2 && base.Bot.HasInHand(59438930)) ? 1 : 0) + ((base.GetCalledbytheGraveIdCount(94145021) < 2 && base.Bot.HasInHand(94145021)) ? 1 : 0) + base.Bot.SpellZone.Count((ClientCard c) => c != null && c.IsFacedown() && c.IsCode(10045474)) + Math.Min(4 - base.Bot.GetSpellCountWithoutField(), base.Bot.Hand.Count((ClientCard c) => c.IsCode(10045474)));
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x000A8B4C File Offset: 0x000A6D4C
		public override BattlePhaseAction OnBattle(IList<ClientCard> attackers, IList<ClientCard> defenders)
		{
			if (attackers.Count<ClientCard>() > 0 && defenders.Count<ClientCard>() > 0)
			{
				List<ClientCard> sortedAttacker = attackers.OrderBy((ClientCard card) => card.Attack).ToList<ClientCard>();
				ClientCard rayLancer = attackers.FirstOrDefault((ClientCard c) => c.IsCode(1269512) && !c.IsDisabled());
				if (rayLancer != null)
				{
					sortedAttacker.Remove(rayLancer);
					sortedAttacker.Insert(0, rayLancer);
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

		// Token: 0x06001BE7 RID: 7143 RVA: 0x000A8C18 File Offset: 0x000A6E18
		public override ClientCard OnSelectAttacker(IList<ClientCard> attackers, IList<ClientCard> defenders)
		{
			ClientCard twin = attackers.FirstOrDefault((ClientCard c) => c.IsCode(45852939) && !c.IsDisabled());
			if (twin != null)
			{
				if (base.Enemy.MonsterZone.Any((ClientCard c) => c != null && c.GetDefensePower() <= 2500))
				{
					return twin;
				}
			}
			return null;
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x000A8C82 File Offset: 0x000A6E82
		public override void OnSelectChain(IList<ClientCard> cards)
		{
			if (cards != null && cards.Count<ClientCard>() > 0)
			{
				this.currentCanActivateEffect.Clear();
				this.currentCanActivateEffect.AddRange(cards);
			}
			base.OnSelectChain(cards);
		}

		// Token: 0x06001BE9 RID: 7145 RVA: 0x000A8CB0 File Offset: 0x000A6EB0
		public override IList<ClientCard> OnSelectCard(IList<ClientCard> cards, int min, int max, int hint, bool cancelable)
		{
			ChainInfo currentSolvingChain = base.Duel.GetCurrentSolvingChainInfo();
			if (currentSolvingChain != null)
			{
				if (this.botSolvingCross)
				{
					if (hint == 532)
					{
						List<Func<ClientCard, bool>> list = new List<Func<ClientCard, bool>>();
						list.Add((ClientCard c) => c.IsDisabled() && c.IsCode(7511613));
						list.Add((ClientCard c) => c.IsCode(7511613));
						list.Add((ClientCard c) => c.IsDisabled() && c.IsCode(34909328));
						list.Add((ClientCard c) => c.IsCode(34909328));
						using (List<Func<ClientCard, bool>>.Enumerator enumerator = list.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								Func<ClientCard, bool> func = enumerator.Current;
								ClientCard target = cards.FirstOrDefault((ClientCard c) => c != null && func(c));
								if (target != null)
								{
									return base.Util.CheckSelectCount(new List<ClientCard> { target }, cards, min, max);
								}
							}
						}
					}
					if (hint == 519)
					{
						List<ClientCard> targets = cards.OrderBy((ClientCard c) => c.Attack).ToList<ClientCard>();
						this.botSolvingCross = false;
						return base.Util.CheckSelectCount(targets, cards, min, max);
					}
				}
				if (currentSolvingChain.ActivatePlayer == 1 && currentSolvingChain.IsCode(15693423))
				{
					Logger.DebugWriteLine("=== Evenly Matched activated.");
					List<ClientCard> banishList = new List<ClientCard>();
					List<ClientCard> list2 = (from card in base.Bot.GetMonsters()
						where !card2.HasType(CardType.Token)
						select card).ToList<ClientCard>();
					List<ClientCard> faceDownMonsters = list2.Where((ClientCard card) => card2.IsFacedown()).ToList<ClientCard>();
					banishList.AddRange(faceDownMonsters);
					List<ClientCard> dumpMainMonsterList = list2.Where((ClientCard card) => !banishList.Contains(card2) && this.CheckRemainInDeck(card2.Id) > 0).ToList<ClientCard>();
					dumpMainMonsterList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					banishList.AddRange(dumpMainMonsterList);
					bool canUsePluginToSpSummonDeadnader = base.Bot.Graveyard.Any((ClientCard c) => c != null && c.IsCanRevive() && c.IsCode(34909328));
					canUsePluginToSpSummonDeadnader |= base.Bot.Graveyard.Any((ClientCard c) => c != null && c.IsFaceup() && c.IsCanRevive() && c.IsCode(34909328));
					List<ClientCard> faceUpSpells = (from c in base.Bot.GetSpells()
						where c.IsFaceup()
						select c).ToList<ClientCard>();
					banishList.AddRange(this.ShuffleList<ClientCard>(faceUpSpells));
					List<ClientCard> faceDownSpells = (from c in base.Bot.GetSpells()
						where c.IsFacedown() && (!canUsePluginToSpSummonDeadnader || !c.IsCode(60394026))
						select c).ToList<ClientCard>();
					banishList.AddRange(this.ShuffleList<ClientCard>(faceDownSpells));
					List<ClientCard> uniqueMainMonster = list2.Where((ClientCard card) => !banishList.Contains(card2) && !card2.HasType((CardType)75505728) && this.CheckRemainInDeck(card2.Id) == 0).ToList<ClientCard>();
					uniqueMainMonster.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					banishList.AddRange(uniqueMainMonster);
					List<ClientCard> dumpExtraMonsterList = list2.Where((ClientCard card) => !banishList.Contains(card2) && card2.HasType((CardType)75505728) && this.Bot.HasInExtra(card2.Id)).ToList<ClientCard>();
					dumpExtraMonsterList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					banishList.AddRange(dumpExtraMonsterList);
					List<ClientCard> uniqueExtraMonsterList = list2.Where((ClientCard card) => !banishList.Contains(card2) && card2.HasType((CardType)75505728) && !this.Bot.HasInExtra(card2.Id)).ToList<ClientCard>();
					uniqueExtraMonsterList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					banishList.AddRange(uniqueExtraMonsterList);
					return base.Util.CheckSelectCount(banishList, cards, min, max);
				}
				if (currentSolvingChain.ActivatePlayer == 0)
				{
					if (hint == 506)
					{
						if (currentSolvingChain.IsCode(35844557))
						{
							ClientCard ice = cards.FirstOrDefault((ClientCard c) => c.IsCode(8633261));
							ClientCard ex = cards.FirstOrDefault((ClientCard c) => c.IsCode(34022970));
							if (ice != null)
							{
								bool flag2 = base.Duel.Player == 0 && this.summonCount > 0 && base.Duel.Phase < DuelPhase.End;
								bool flag = flag2 && !base.Bot.HasInHand(8633261) && !this.activatedCardIdList.Contains(8633261) && !base.DefaultCheckWhetherCardIdIsNegated(8633261);
								flag |= ex == null;
								flag |= base.Bot.GetMonsters().Any((ClientCard c) => c.IsFaceup() && !c.IsDisabled() && c.IsCode(this.NeedIceToSolveIdList)) && !this.spSummonedCardIdList.Contains(8633261) && !this.CheckWhetherWillbeRemoved();
								if (!flag2)
								{
									flag |= base.DefaultCheckWhetherCardIdIsNegated(34022970);
									flag |= this.spSummonedCardIdList.Contains(34022970) || this.activatedCardIdList.Contains(34022970);
								}
								if (flag)
								{
									return base.Util.CheckSelectCount(new List<ClientCard> { ice }, cards, min, max);
								}
							}
							if (ex != null && ((!base.Bot.HasInHand(34022970) && !this.spSummonedCardIdList.Contains(34022970) && !this.activatedCardIdList.Contains(34022970)) | base.Bot.HasInHand(8633261) | (ice == null)))
							{
								return base.Util.CheckSelectCount(new List<ClientCard> { ex }, cards, min, max);
							}
						}
						if (currentSolvingChain.IsCode(34022970))
						{
							ClientCard thode = cards.FirstOrDefault((ClientCard c) => c.IsCode(35844557));
							ClientCard node = cards.FirstOrDefault((ClientCard c) => c.IsCode(72238166));
							if (thode != null && ((node == null) | (!base.Bot.HasInHand(35844557) && !this.spSummonedCardIdList.Contains(35844557) && !this.activatedCardIdList.Contains(35844557))))
							{
								return base.Util.CheckSelectCount(new List<ClientCard> { thode }, cards, min, max);
							}
							if (node != null && ((thode == null) | (this.spSummonedCardIdList.Contains(35844557) && this.activatedCardIdList.Contains(35844557) && !base.DefaultCheckWhetherCardIdIsNegated(35844557)) | (this.CheckShouldNoMoreSpSummon(CardLocation.Hand) && !this.CheckShouldNoMoreSpSummon(CardLocation.Grave) && !this.spSummonedCardIdList.Contains(72238166))))
							{
								return base.Util.CheckSelectCount(new List<ClientCard> { node }, cards, min, max);
							}
						}
						if (currentSolvingChain.IsCode(85106525) || currentSolvingChain.IsCode(7477101))
						{
							if (!base.Bot.HasInHand(34022970) && !this.spSummonedCardIdList.Contains(34022970) && !this.CheckWhetherWillbeRemoved())
							{
								ClientCard target2 = cards.FirstOrDefault((ClientCard c) => c.IsCode(34022970));
								if (target2 != null)
								{
									return base.Util.CheckSelectCount(new List<ClientCard> { target2 }, cards, min, max);
								}
							}
							if (!base.Bot.HasInHand(8633261) && ((this.summonCount > 0 && !this.activatedCardIdList.Contains(8633261)) | (!this.spSummonedCardIdList.Contains(8633261) && base.Bot.Hand.Count > 0)))
							{
								ClientCard target3 = cards.FirstOrDefault((ClientCard c) => c.IsCode(8633261));
								if (target3 != null)
								{
									return base.Util.CheckSelectCount(new List<ClientCard> { target3 }, cards, min, max);
								}
							}
							using (List<int>.Enumerator enumerator2 = new List<int> { 34022970, 8633261 }.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									int targetId = enumerator2.Current;
									ClientCard target4 = cards.FirstOrDefault((ClientCard c) => c.IsCode(targetId));
									if (target4 != null)
									{
										return base.Util.CheckSelectCount(new List<ClientCard> { target4 }, cards, min, max);
									}
								}
							}
						}
						if (currentSolvingChain.IsCode(7511613))
						{
							if (!this.CheckWhetherNegated(true, true, CardType.Spell, false))
							{
								ClientCard cross = cards.FirstOrDefault((ClientCard c) => c.IsCode(6798031));
								if (cross != null)
								{
									return base.Util.CheckSelectCount(new List<ClientCard> { cross }, cards, min, max);
								}
								ClientCard plugin = cards.FirstOrDefault((ClientCard c) => c.IsCode(60394026));
								if (plugin != null)
								{
									return base.Util.CheckSelectCount(new List<ClientCard> { plugin }, cards, min, max);
								}
							}
							using (List<KeyValuePair<int, Func<bool>>>.Enumerator enumerator3 = new List<KeyValuePair<int, Func<bool>>>
							{
								new KeyValuePair<int, Func<bool>>(8633261, () => base.Bot.GetMonsters().Any((ClientCard c) => c.IsFaceup() && !c.IsDisabled() && c.IsCode(this.NeedIceToSolveIdList)) && !this.spSummonedCardIdList.Contains(8633261) && !this.CheckWhetherWillbeRemoved()),
								new KeyValuePair<int, Func<bool>>(34022970, () => !this.spSummonedCardIdList.Contains(34022970) && !this.activatedCardIdList.Contains(34022970) && !base.DefaultCheckWhetherCardIdIsNegated(34022970) && !this.CheckWhetherWillbeRemoved()),
								new KeyValuePair<int, Func<bool>>(8633261, () => this.summonCount > 0 && !this.activatedCardIdList.Contains(8633261) && !base.DefaultCheckWhetherCardIdIsNegated(8633261)),
								new KeyValuePair<int, Func<bool>>(35844557, () => !this.spSummonedCardIdList.Contains(35844557) && !this.activatedCardIdList.Contains(35844557) && !base.DefaultCheckWhetherCardIdIsNegated(35844557)),
								new KeyValuePair<int, Func<bool>>(72238166, () => !this.spSummonedCardIdList.Contains(72238166))
							}.GetEnumerator())
							{
								while (enumerator3.MoveNext())
								{
									KeyValuePair<int, Func<bool>> pair = enumerator3.Current;
									if (!base.Bot.HasInHand(pair.Key) && pair.Value())
									{
										ClientCard target5 = cards.FirstOrDefault((ClientCard c) => c.IsCode(pair.Key));
										if (target5 != null)
										{
											return base.Util.CheckSelectCount(new List<ClientCard> { target5 }, cards, min, max);
										}
									}
								}
							}
							using (List<int>.Enumerator enumerator2 = new List<int> { 34022970, 8633261, 35844557, 72238166 }.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									int id = enumerator2.Current;
									if (!base.Bot.HasInHand(id))
									{
										ClientCard target6 = cards.FirstOrDefault((ClientCard c) => c.IsCode(id));
										if (target6 != null)
										{
											return base.Util.CheckSelectCount(new List<ClientCard> { target6 }, cards, min, max);
										}
									}
								}
							}
							return base.Util.CheckSelectCount(this.ShuffleList<ClientCard>(cards.ToList<ClientCard>()), cards, min, max);
						}
					}
					if (hint == 509)
					{
						if (currentSolvingChain.IsCode(8633261))
						{
							ClientCard thode2 = cards.FirstOrDefault((ClientCard c) => c.IsCode(35844557));
							ClientCard ex2 = cards.FirstOrDefault((ClientCard c) => c.IsCode(34022970));
							ClientCard node2 = cards.FirstOrDefault((ClientCard c) => c.IsCode(72238166));
							if (thode2 != null && ((!this.activatedCardIdList.Contains(35844557) && !base.DefaultCheckWhetherCardIdIsNegated(35844557)) | (base.Bot.HasInHand(34022970) && !this.spSummonedCardIdList.Contains(34022970)) | (ex2 == null && node2 == null)))
							{
								return base.Util.CheckSelectCount(new List<ClientCard> { thode2 }, cards, min, max);
							}
							if (ex2 != null && ((!this.activatedCardIdList.Contains(34022970) && !base.DefaultCheckWhetherCardIdIsNegated(34022970)) | (base.Bot.HasInHand(35844557) && !this.spSummonedCardIdList.Contains(35844557)) | (thode2 == null && node2 == null)))
							{
								return base.Util.CheckSelectCount(new List<ClientCard> { ex2 }, cards, min, max);
							}
							if (node2 != null)
							{
								return base.Util.CheckSelectCount(new List<ClientCard> { node2 }, cards, min, max);
							}
						}
						if (currentSolvingChain.IsCode(45852939))
						{
							ClientCard target7 = this.TwinsOfTheEclipseRebornTarget(new List<ClientCard>(cards));
							return base.Util.CheckSelectCount(new List<ClientCard> { target7 }, cards, min, max);
						}
					}
					if (hint == 507 && currentSolvingChain.IsCode(25311006))
					{
						foreach (ClientCard hand in cards)
						{
							foreach (int setcode in this.CheckSetcodeList)
							{
								if (hand.HasSetcode(setcode))
								{
									this.enemyDeckTypeRecord.Add(setcode);
								}
							}
						}
						return base.Util.CheckSelectCount(this.ShuffleList<ClientCard>(cards.ToList<ClientCard>()), cards, min, max);
					}
					if (hint == 513 && currentSolvingChain.IsCode(new int[] { 34909328, 7511613, 60394026 }))
					{
						ClientCard effectTarget = cards.FirstOrDefault((ClientCard c) => c.IsCode(new int[] { 45852939, 9940036 }));
						if (effectTarget != null)
						{
							return base.Util.CheckSelectCount(new List<ClientCard> { effectTarget }, cards, min, max);
						}
						using (IEnumerator<ClientCard> enumerator4 = cards.GetEnumerator())
						{
							while (enumerator4.MoveNext())
							{
								ClientCard card2 = enumerator4.Current;
								if (base.Bot.Hand.Count((ClientCard c) => c.IsCode(card2.Id)) > 0)
								{
									return base.Util.CheckSelectCount(new List<ClientCard> { card2 }, cards, min, max);
								}
							}
						}
						using (IEnumerator<ClientCard> enumerator4 = cards.GetEnumerator())
						{
							while (enumerator4.MoveNext())
							{
								ClientCard card3 = enumerator4.Current;
								if (cards.Count((ClientCard c) => c.IsCode(card3.Id)) > 1)
								{
									return base.Util.CheckSelectCount(new List<ClientCard> { card3 }, cards, min, max);
								}
							}
						}
					}
					if (hint == 519 && currentSolvingChain.IsCode(7511613))
					{
						List<ClientCard> resultList2 = new List<ClientCard>();
						using (List<int>.Enumerator enumerator2 = new List<int> { 16643334, 7511613, 45852939, 1269512, 46772449, 66011101, 34909328 }.GetEnumerator())
						{
							Func<ClientCard, bool> <>9__45;
							Func<ClientCard, bool> <>9__46;
							Func<ClientCard, bool> <>9__47;
							Func<ClientCard, bool> <>9__48;
							Func<ClientCard, bool> <>9__49;
							while (enumerator2.MoveNext())
							{
								int ownerId = enumerator2.Current;
								Func<ClientCard, bool> <>9__43;
								List<ClientCard> detachMaterialList = cards.Where(delegate(ClientCard c)
								{
									IEnumerable<ClientCard> ownTargets = c.OwnTargets;
									Func<ClientCard, bool> func7;
									if ((func7 = <>9__43) == null)
									{
										func7 = (<>9__43 = (ClientCard oc) => oc.IsCode(ownerId));
									}
									return ownTargets.Any(func7);
								}).ToList<ClientCard>();
								if (detachMaterialList.Count<ClientCard>() > 0)
								{
									ClientCard deadnader = detachMaterialList.FirstOrDefault((ClientCard c) => c.IsCode(34909328));
									if (deadnader != null)
									{
										resultList2.Add(deadnader);
									}
									List<Func<ClientCard, bool>> list3 = new List<Func<ClientCard, bool>>();
									Func<ClientCard, bool> func6;
									if ((func6 = <>9__45) == null)
									{
										func6 = (<>9__45 = (ClientCard c) => !resultList2.Contains(c) && !c.IsCode(new int[] { 9940036, 45852939 }) && this.Bot.HasInSpellZone(6798031, false, false) && c.HasSetcode(446) && (c.Data == null || (c.Data.Attribute & 48) != 0));
									}
									list3.Add(func6);
									Func<ClientCard, bool> func2;
									if ((func2 = <>9__46) == null)
									{
										func2 = (<>9__46 = (ClientCard c) => !resultList2.Contains(c) && !c.IsCode(new int[] { 9940036, 45852939 }) && this.Bot.HasInSpellZone(6798031, false, false) && c.HasSetcode(446));
									}
									list3.Add(func2);
									Func<ClientCard, bool> func3;
									if ((func3 = <>9__47) == null)
									{
										func3 = (<>9__47 = (ClientCard c) => !resultList2.Contains(c) && !c.IsCode(new int[] { 9940036, 45852939 }) && (c.Data == null || (c.Data.Attribute & 48) != 0));
									}
									list3.Add(func3);
									Func<ClientCard, bool> func4;
									if ((func4 = <>9__48) == null)
									{
										func4 = (<>9__48 = (ClientCard c) => !resultList2.Contains(c) && !c.IsCode(new int[] { 9940036, 45852939 }));
									}
									list3.Add(func4);
									Func<ClientCard, bool> func5;
									if ((func5 = <>9__49) == null)
									{
										func5 = (<>9__49 = (ClientCard c) => !resultList2.Contains(c));
									}
									list3.Add(func5);
									foreach (Func<ClientCard, bool> filter in list3)
									{
										foreach (ClientCard material in detachMaterialList)
										{
											if (filter(material))
											{
												resultList2.Add(material);
											}
										}
									}
								}
							}
						}
						return base.Util.CheckSelectCount(resultList2, cards, min, max);
					}
					if (currentSolvingChain.IsCode(60394026))
					{
						if (cards.All((ClientCard c) => c.Location == CardLocation.MonsterZone))
						{
							ClientCard abyssDweller = cards.FirstOrDefault((ClientCard c) => c != null && !c.IsDisabled() && c.IsCode(21044178) && c.Overlays.Count<int>() < 2);
							if (abyssDweller != null && this.AbyssDwellerSummonCheck())
							{
								return base.Util.CheckSelectCount(new List<ClientCard> { abyssDweller }, cards, min, max);
							}
							ClientCard duoDriver = cards.FirstOrDefault((ClientCard c) => c != null && !c.IsDisabled() && c.IsCode(7511613) && c.Overlays.Count<int>() == 1);
							if (duoDriver != null && base.Bot.HasInMonstersZone(16643334, true, false, true))
							{
								return base.Util.CheckSelectCount(new List<ClientCard> { duoDriver }, cards, min, max);
							}
							ClientCard deadnader2 = cards.FirstOrDefault((ClientCard c) => c != null && !c.IsDisabled() && c.IsCode(34909328));
							if (deadnader2 != null)
							{
								return base.Util.CheckSelectCount(new List<ClientCard> { deadnader2 }, cards, min, max);
							}
							if (base.Bot.HasInSpellZone(6798031, true, true))
							{
								ClientCard ryzealXyz = cards.FirstOrDefault((ClientCard c) => c != null && c.HasSetcode(446));
								if (ryzealXyz != null)
								{
									return base.Util.CheckSelectCount(new List<ClientCard> { ryzealXyz }, cards, min, max);
								}
							}
							ClientCard tornadoDragon = cards.FirstOrDefault((ClientCard c) => c != null && !c.IsDisabled() && c.IsCode(6983839) && c.Overlays.Count<int>() == 1);
							if (tornadoDragon != null && this.TornadoDragonSummonCheck())
							{
								return base.Util.CheckSelectCount(new List<ClientCard> { tornadoDragon }, cards, min, max);
							}
							ClientCard no41 = cards.FirstOrDefault((ClientCard c) => c != null && c.IsCode(90590303));
							if (no41 != null && this.Number41BagooskatheTerriblyTiredTapirSummonCheck())
							{
								return base.Util.CheckSelectCount(new List<ClientCard> { no41 }, cards, min, max);
							}
							duoDriver = cards.FirstOrDefault((ClientCard c) => c != null && !c.IsDisabled() && c.IsCode(7511613));
							if (duoDriver != null)
							{
								return base.Util.CheckSelectCount(new List<ClientCard> { duoDriver }, cards, min, max);
							}
						}
					}
					if (currentSolvingChain.IsCode(66011101))
					{
						if (cards.All((ClientCard c) => c.Location == CardLocation.MonsterZone))
						{
							ClientCard maxAttackMonster = (from c in cards
								where c != null && (c.HasPosition(CardPosition.FaceUpAttack) || !this.summonThisTurn.Contains(c))
								orderby c.Attack descending
								select c).FirstOrDefault<ClientCard>();
							if (maxAttackMonster != null)
							{
								return base.Util.CheckSelectCount(new List<ClientCard> { maxAttackMonster }, cards, min, max);
							}
						}
					}
				}
				if (hint == 507 || hint == 504 || hint == 501)
				{
					if (cards.All((ClientCard c) => c.Controller == 0 && c.Location == CardLocation.Hand))
					{
						List<ClientCard> resultList = new List<ClientCard>();
						using (List<int>.Enumerator enumerator2 = new List<int> { 87126721, 84192580, 42141493, 7477101 }.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								int code2 = enumerator2.Current;
								List<ClientCard> targetList = cards.Where((ClientCard c) => c.IsCode(code2) && !resultList.Contains(c)).ToList<ClientCard>();
								if (targetList.Count<ClientCard>() > 0)
								{
									resultList.AddRange(targetList);
								}
							}
						}
						using (IEnumerator<ClientCard> enumerator4 = cards.GetEnumerator())
						{
							while (enumerator4.MoveNext())
							{
								ClientCard card = enumerator4.Current;
								if (!resultList.Contains(card) && cards.Where((ClientCard c) => c.IsCode(card.Id) && !resultList.Contains(c)).Count<ClientCard>() > 1)
								{
									resultList.Add(card);
								}
							}
						}
						using (List<int>.Enumerator enumerator2 = new List<int>
						{
							97268402, 10045474, 59438930, 25311006, 72238166, 94145021, 60394026, 65681983, 24224830, 6798031,
							35844557, 34022970, 8633261
						}.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								int code = enumerator2.Current;
								ClientCard target8 = cards.FirstOrDefault((ClientCard c) => c.IsCode(code) && !resultList.Contains(c));
								if (target8 != null)
								{
									resultList.Add(target8);
								}
							}
						}
						return base.Util.CheckSelectCount(resultList, cards, min, max);
					}
				}
			}
			if (currentSolvingChain == null)
			{
				ClientCard lastChainCard = base.Util.GetLastChainCard();
				if (lastChainCard != null)
				{
					if (lastChainCard.Controller == 0 && lastChainCard.IsCode(34909328))
					{
						if (hint == 519)
						{
							if (this.deadnaderDestroySelf != null)
							{
								ClientCard detachTarget = cards.FirstOrDefault((ClientCard c) => c.IsCode(new int[] { 9940036, 45852939 }));
								if (detachTarget != null)
								{
									return base.Util.CheckSelectCount(new List<ClientCard> { detachTarget }, cards, min, max);
								}
							}
							List<ClientCard> targets2 = cards.OrderBy((ClientCard c) => c.Attack).ToList<ClientCard>();
							return base.Util.CheckSelectCount(targets2, cards, min, max);
						}
						if (hint == 502)
						{
							if (this.deadnaderDestroySelf != null && cards.Contains(this.deadnaderDestroySelf))
							{
								return base.Util.CheckSelectCount(new List<ClientCard> { this.deadnaderDestroySelf }, cards, min, max);
							}
							foreach (ClientCard target9 in this.CanDestroyList(false))
							{
								if (cards.Contains(target9))
								{
									this.currentDestroyCardList.Add(target9);
									return base.Util.CheckSelectCount(new List<ClientCard> { target9 }, cards, min, max);
								}
							}
						}
					}
					if (hint == 519 && base.Bot.HasInHandOrInSpellZone(60394026))
					{
						using (List<int>.Enumerator enumerator2 = new List<int> { 72238166, 35844557, 34022970 }.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								int checkId = enumerator2.Current;
								if (!this.activatedCardIdList.Contains(checkId))
								{
									ClientCard target10 = cards.FirstOrDefault((ClientCard c) => c.IsCode(checkId));
									if (target10 != null)
									{
										return base.Util.CheckSelectCount(new List<ClientCard> { target10 }, cards, min, max);
									}
								}
							}
						}
					}
				}
			}
			return base.OnSelectCard(cards, min, max, hint, cancelable);
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x000AA884 File Offset: 0x000A8A84
		public override bool OnSelectHand()
		{
			HashSet<int> tenpaiList = new HashSet<int> { 426, 425 };
			return this.enemyDeckTypeRecord.Count<int>() <= 0 || !this.enemyDeckTypeRecord.All((int c) => tenpaiList.Contains(c));
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x000AA8E8 File Offset: 0x000A8AE8
		public override int OnSelectOption(IList<int> options)
		{
			bool tripleCheck = false;
			for (int opt = 0; opt < 3; opt++)
			{
				if (options.Contains(base.Util.GetStringId(25311006, opt)))
				{
					tripleCheck = true;
					break;
				}
			}
			if (tripleCheck)
			{
				return this.TripleTacticsTalentDecision(options);
			}
			bool no60Check = false;
			for (int opt2 = 0; opt2 < 3; opt2++)
			{
				if (options.Contains(base.Util.GetStringId(66011101, opt2)))
				{
					no60Check = true;
					break;
				}
			}
			if (no60Check)
			{
				if (options.Contains(base.Util.GetStringId(66011101, 2)) && this.Number60DugaresTheTimelessDoubleTarget() != null)
				{
					int res = options.IndexOf(base.Util.GetStringId(66011101, 2));
					if (res >= 0)
					{
						return res;
					}
				}
				if (options.Contains(base.Util.GetStringId(66011101, 0)) && this.Number60DugaresTheTimelessDrawEffect())
				{
					int res2 = options.IndexOf(base.Util.GetStringId(66011101, 0));
					if (res2 >= 0)
					{
						return res2;
					}
				}
				if (options.Contains(base.Util.GetStringId(66011101, 1)) && this.Number60DugaresTheTimelessRebornEffect())
				{
					int res3 = options.IndexOf(base.Util.GetStringId(66011101, 1));
					if (res3 >= 0)
					{
						return res3;
					}
				}
			}
			base.Duel.GetCurrentSolvingChainCard();
			return base.OnSelectOption(options);
		}

		// Token: 0x06001BEC RID: 7148 RVA: 0x000AAA38 File Offset: 0x000A8C38
		public override int OnSelectPlace(int cardId, int player, CardLocation location, int available)
		{
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

		// Token: 0x06001BED RID: 7149 RVA: 0x000AAB24 File Offset: 0x000A8D24
		public override bool OnSelectYesNo(int desc)
		{
			if (desc == base.Util.GetStringId(60394026, 1))
			{
				Logger.DebugWriteLine("** plugin set material");
				return true;
			}
			return base.OnSelectYesNo(desc);
		}

		// Token: 0x06001BEE RID: 7150 RVA: 0x000AAB50 File Offset: 0x000A8D50
		public override CardPosition OnSelectPosition(int cardId, IList<CardPosition> positions)
		{
			if (cardId == 90590303 && (base.Util.IsTurn1OrMain2() || base.Duel.Player == 1))
			{
				return CardPosition.FaceUpDefence;
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
				bool flag;
				if (base.Bot.HasInExtra(66011101) && this.GetLevel4FinalCountOnField(true, out flag) >= 2)
				{
					bestBotAttack *= 2;
				}
				if (base.Util.IsAllEnemyBetterThanValue(bestBotAttack, true))
				{
					return CardPosition.FaceUpDefence;
				}
			}
			return base.OnSelectPosition(cardId, positions);
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x000AAC54 File Offset: 0x000A8E54
		public override void OnNewTurn()
		{
			if (base.Duel.Turn <= 1)
			{
				this.dimensionShifterCount = 0;
				this.maxSummonCount = 1;
				this.hardToDestroyCardList.Clear();
				this.cannotDestroyCardList.Clear();
			}
			this.summonCount = this.maxSummonCount;
			this.enemyActivateMaxxC = false;
			this.enemyActivatePurulia = false;
			this.enemyActivateFuwalos = false;
			this.enemyActivateNyalus = false;
			this.lockBirdSolved = false;
			if (this.dimensionShifterCount > 0)
			{
				this.dimensionShifterCount--;
			}
			this.enemyActivateInfiniteImpermanenceFromHand = false;
			this.botActivateMulcharmy = false;
			this.deadnaderDestroySelf = null;
			this.botSolvingCross = false;
			this.infiniteImpermanenceList.Clear();
			this.currentNegateCardList.Clear();
			this.currentDestroyCardList.Clear();
			this.activatedCardIdList.Clear();
			this.spSummonedCardIdList.Clear();
			this.botSolvedCardIdList.Clear();
			this.enemyPlaceThisTurn.Clear();
			this.summonThisTurn.Clear();
			this.currentCanActivateEffect.Clear();
			base.OnNewTurn();
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x000AAD60 File Offset: 0x000A8F60
		public override void OnChaining(int player, ClientCard card)
		{
			base.Duel.LastChainTargets.Clear();
			if (card == null)
			{
				return;
			}
			if (player == 1)
			{
				if (card.IsCode(10045474))
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
				if (card.HasSetcode(365))
				{
					this.enemyDeckTypeRecord.Add(365);
				}
			}
			if (player == 0 && card.IsCode(new int[] { 84192580, 42141493, 87126721 }))
			{
				this.botActivateMulcharmy = true;
			}
			base.OnChaining(player, card);
		}

		// Token: 0x06001BF1 RID: 7153 RVA: 0x000AAE18 File Offset: 0x000A9018
		public override void OnChainSolved(int chainIndex)
		{
			this.botSolvingCross = false;
			ChainInfo currentChain = base.Duel.GetCurrentSolvingChainInfo();
			if (currentChain != null && !base.Duel.IsCurrentSolvingChainNegated() && !base.Duel.IsCurrentSolvingChainNegated())
			{
				if (currentChain.IsCode(94145021))
				{
					this.lockBirdSolved = true;
				}
				if (currentChain.IsCode(91800273))
				{
					this.dimensionShifterCount = 2;
				}
				if (currentChain.ActivatePlayer == 1)
				{
					if (currentChain.IsCode(23434538))
					{
						this.enemyActivateMaxxC = true;
					}
					if (currentChain.IsCode(84192580))
					{
						this.enemyActivatePurulia = true;
					}
					if (currentChain.IsCode(42141493))
					{
						this.enemyActivateFuwalos = true;
					}
					if (currentChain.IsCode(87126721))
					{
						this.enemyActivateNyalus = true;
					}
				}
				if (currentChain.ActivatePlayer == 0)
				{
					foreach (int checkId in this.CheckBotSolvedList)
					{
						if (currentChain.IsCode(checkId))
						{
							this.botSolvedCardIdList.Add(checkId);
						}
					}
				}
			}
			base.OnChainSolved(chainIndex);
		}

		// Token: 0x06001BF2 RID: 7154 RVA: 0x000AAF44 File Offset: 0x000A9144
		public override void OnChainEnd()
		{
			for (int idx = this.cannotDestroyCardList.Count - 1; idx >= 0; idx--)
			{
				ClientCard checkTarget = this.cannotDestroyCardList[idx];
				if (checkTarget == null || !checkTarget.IsOnField())
				{
					this.cannotDestroyCardList.RemoveAt(idx);
				}
			}
			for (int idx2 = this.hardToDestroyCardList.Count - 1; idx2 >= 0; idx2--)
			{
				ClientCard checkTarget2 = this.hardToDestroyCardList[idx2];
				if (checkTarget2 == null || !checkTarget2.IsOnField())
				{
					this.hardToDestroyCardList.RemoveAt(idx2);
				}
			}
			foreach (ClientCard card in this.currentDestroyCardList)
			{
				if (card != null && card.IsOnField())
				{
					if (this.hardToDestroyCardList.Contains(card))
					{
						this.cannotDestroyCardList.Add(card);
					}
					else
					{
						this.hardToDestroyCardList.Add(card);
					}
				}
			}
			this.currentNegateCardList.Clear();
			this.currentDestroyCardList.Clear();
			this.currentCanActivateEffect.Clear();
			this.enemyActivateInfiniteImpermanenceFromHand = false;
			this.botSolvingCross = false;
			this.deadnaderDestroySelf = null;
			for (int idx3 = this.enemyPlaceThisTurn.Count - 1; idx3 >= 0; idx3--)
			{
				ClientCard checkTarget3 = this.enemyPlaceThisTurn[idx3];
				if (checkTarget3 == null || !checkTarget3.IsOnField())
				{
					this.enemyPlaceThisTurn.RemoveAt(idx3);
				}
			}
			base.OnChainEnd();
		}

		// Token: 0x06001BF3 RID: 7155 RVA: 0x000AB0C4 File Offset: 0x000A92C4
		public override void OnMove(ClientCard card, int previousControler, int previousLocation, int currentControler, int currentLocation)
		{
			if (card != null)
			{
				if (previousControler == 1 && card.IsCode(10045474) && previousLocation == 2 && currentLocation == 8)
				{
					this.enemyActivateInfiniteImpermanenceFromHand = true;
				}
				if (card.Owner == 1)
				{
					foreach (int setcode in this.CheckSetcodeList)
					{
						if (card.HasSetcode(setcode))
						{
							this.enemyDeckTypeRecord.Add(setcode);
						}
					}
					if (card.IsCode(this.AlbazFusionList))
					{
						this.enemyDeckTypeRecord.Add(349);
					}
				}
				if (currentControler == 1 && (currentLocation == 4 || currentLocation == 8))
				{
					this.enemyPlaceThisTurn.Add(card);
				}
				if (previousControler == 0 && previousLocation == 4 && currentLocation != 4 && this.summonThisTurn.Contains(card))
				{
					this.summonThisTurn.Remove(card);
				}
				if (currentControler == 0 && currentLocation == 4)
				{
					this.summonThisTurn.Add(card);
				}
			}
			base.OnMove(card, previousControler, previousLocation, currentControler, currentLocation);
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x000AB1DC File Offset: 0x000A93DC
		public override void OnSpSummoned()
		{
			if (base.Duel.GetCurrentSolvingChainCard() == null)
			{
				foreach (ClientCard card in base.Duel.LastSummonedCards)
				{
					if (card.Controller == 0 && card.IsCode(new int[] { 8633261, 35844557, 72238166, 34022970 }))
					{
						this.spSummonedCardIdList.Add(card.GetOriginCode());
					}
				}
			}
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x000AB268 File Offset: 0x000A9468
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

		// Token: 0x06001BF6 RID: 7158 RVA: 0x000AB454 File Offset: 0x000A9654
		public bool IceRyzealSpSummonFirst()
		{
			if (this.CheckShouldNoMoreSpSummon((CardLocation)67))
			{
				if (base.Bot.GetMonsters().Any((ClientCard c) => c.IsFaceup() && c.HasType(CardType.Xyz)) | (this.GetLevel4CountOnField() >= 2))
				{
					return false;
				}
			}
			List<ClientCard> costList = this.GetCostFromHandAndFieldFirst(base.Card);
			if (costList.Count<ClientCard>() > 0)
			{
				base.AI.SelectCard(costList);
				return true;
			}
			return false;
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x000AB4D0 File Offset: 0x000A96D0
		public bool IceRyzealSpSummon()
		{
			if (this.CheckShouldNoMoreSpSummon((CardLocation)67))
			{
				if (base.Bot.GetMonsters().Any((ClientCard c) => c.IsFaceup() && c.HasType(CardType.Xyz)) | (this.GetLevel4CountOnField() >= 2))
				{
					return false;
				}
			}
			if (base.Card.Level != 4)
			{
				return false;
			}
			if (this.summonCount <= 0 && this.GetLevel4CountOnField() == 1)
			{
				List<ClientCard> firstCostList = this.GetCostFromHandAndField(base.Card, false);
				if (firstCostList.Count<ClientCard>() > 0)
				{
					base.AI.SelectCard(firstCostList);
					return true;
				}
				if (base.Enemy.MonsterZone.Any((ClientCard c) => c != null && c.IsFaceup() && !c.IsDisabled() && c.IsFloodgate()) || !this.CheckWhetherHaveFinalMonster())
				{
					List<ClientCard> costList = this.GetCostFromHandAndField(base.Card, true);
					if (costList.Count<ClientCard>() > 0)
					{
						base.AI.SelectCard(costList);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x000AB5D4 File Offset: 0x000A97D4
		public bool IceRyzealSummon()
		{
			if (this.CheckWhetherNegated(true, true, (CardType)0, false))
			{
				return false;
			}
			if (this.CheckShouldNoMoreSpSummon((CardLocation)67))
			{
				if (base.Bot.GetMonsters().Any((ClientCard c) => c.IsFaceup() && c.HasType(CardType.Xyz)) | (this.GetLevel4CountOnField() >= 2))
				{
					return false;
				}
			}
			this.summonCount--;
			return true;
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x000AB648 File Offset: 0x000A9848
		public bool IceRyzealActivate()
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0, false))
			{
				return false;
			}
			if (this.CheckShouldNoMoreSpSummon(CardLocation.Deck) && this.GetLevel4CountOnField() >= 2)
			{
				return false;
			}
			this.activatedCardIdList.Add(base.Card.Id);
			return true;
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x000AB684 File Offset: 0x000A9884
		public bool ThodeRyzealSpSummon()
		{
			int lv4Count = this.GetLevel4CountOnField();
			if (this.CheckShouldNoMoreSpSummon((CardLocation)67))
			{
				if (base.Bot.GetMonsters().Any((ClientCard c) => c.IsFaceup() && c.HasType(CardType.Xyz)) | (lv4Count >= 2) | (lv4Count == 1 && this.summonCount > 0))
				{
					return false;
				}
			}
			bool spsummonFlag = lv4Count == 1;
			spsummonFlag |= !this.CheckWhetherNegated(true, true, CardType.Monster, false) && this.CheckRemainInDeck(new int[] { 8633261, 34022970 }) > 0 && !this.activatedCardIdList.Contains(35844557) && !this.lockBirdSolved;
			if (this.GetLevel4CountOnField() == 0)
			{
				bool flag;
				spsummonFlag |= this.GetLevel4FinalCountOnField(true, out flag) >= 2 && !this.CheckWhetherHaveFinalMonster();
			}
			return spsummonFlag;
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x000AB764 File Offset: 0x000A9964
		public bool ThodeRyzealSummon()
		{
			if (this.CheckShouldNoMoreSpSummon(CardLocation.Extra))
			{
				int lv4Count = this.GetLevel4CountOnField();
				if (lv4Count == 1 && (!this.activatedCardIdList.Contains(35844557) & (!base.Bot.HasInHand(34022970) || this.activatedCardIdList.Contains(34022970))))
				{
					this.summonCount--;
					return true;
				}
				if (base.Bot.GetMonsters().Any((ClientCard c) => c.IsFaceup() && c.HasType(CardType.Xyz)) | (lv4Count >= 2))
				{
					return false;
				}
			}
			if (this.CheckWhetherNegated(true, false, (CardType)0, false))
			{
				return false;
			}
			this.summonCount--;
			return true;
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x000AB82C File Offset: 0x000A9A2C
		public bool ThodeRyzealActivate()
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0, false))
			{
				return false;
			}
			this.activatedCardIdList.Add(base.Card.Id);
			return true;
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x000AB854 File Offset: 0x000A9A54
		public bool NodeRyzealSpSummon()
		{
			int lv4Count = this.GetLevel4CountOnField();
			if (this.CheckShouldNoMoreSpSummon((CardLocation)67))
			{
				if (base.Bot.GetMonsters().Any((ClientCard c) => c.IsFaceup() && c.HasType(CardType.Xyz)) | (lv4Count >= 2) | (lv4Count == 1 && this.summonCount > 0))
				{
					return false;
				}
			}
			bool flag = lv4Count == 1;
			bool flag2;
			if (!this.CheckWhetherNegated(true, true, CardType.Monster, false))
			{
				flag2 = base.Bot.Graveyard.Any((ClientCard c) => !c.HasType(CardType.Xyz) && c.HasSetcode(446) && c.Level == 4);
			}
			else
			{
				flag2 = false;
			}
			return flag || flag2;
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x000AB904 File Offset: 0x000A9B04
		public bool NodeRyzealSpSummonFirst()
		{
			int lv4Count = this.GetLevel4CountOnField();
			if (this.CheckShouldNoMoreSpSummon((CardLocation)67))
			{
				if (base.Bot.GetMonsters().Any((ClientCard c) => c.IsFaceup() && c.HasType(CardType.Xyz)) | (lv4Count >= 2) | (lv4Count == 1 && this.summonCount > 0))
				{
					return false;
				}
			}
			return !this.activatedCardIdList.Contains(base.Card.Id) && this.GetCostFromHandAndField(base.Card, false).Count<ClientCard>() > 0;
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x000AB99E File Offset: 0x000A9B9E
		public bool NodeRyzealActivate()
		{
			return this.NodeRyzealActivateInner(true);
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x000AB9AC File Offset: 0x000A9BAC
		public bool NodeRyzealActivateFirst()
		{
			return this.NodeRyzealActivateInner(false);
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x000AB9BC File Offset: 0x000A9BBC
		public bool NodeRyzealActivateInner(bool sendNotNessary)
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0, false))
			{
				return false;
			}
			if (this.CheckShouldNoMoreSpSummon((CardLocation)80))
			{
				if (base.Bot.GetMonsters().Count((ClientCard c) => c.HasType(CardType.Xyz) && c.IsFaceup()) > 0)
				{
					return false;
				}
			}
			ClientCard nonLightDarkTarget = (from c in base.Bot.Graveyard
				where c != null && !c.HasType(CardType.Xyz) && c.HasSetcode(446) && c.Level == 4 && !c.HasAttribute((CardAttribute)48)
				orderby c.GetDefensePower() descending
				select c).FirstOrDefault<ClientCard>();
			ClientCard normalTarget = (from c in base.Bot.Graveyard
				where c != null && !c.HasType(CardType.Xyz) && c.HasSetcode(446) && c.Level == 4 && c != nonLightDarkTarget
				orderby c.GetDefensePower() descending
				select c).FirstOrDefault<ClientCard>();
			if (nonLightDarkTarget == null || normalTarget == null)
			{
				return false;
			}
			List<ClientCard> rebornTarget = new List<ClientCard> { nonLightDarkTarget, normalTarget };
			List<ClientCard> firstCostList = this.GetCostFromHandAndField(base.Card, false);
			if (firstCostList.Count<ClientCard>() > 0)
			{
				base.AI.SelectCard(firstCostList);
				base.AI.SelectNextCard(rebornTarget);
				this.activatedCardIdList.Add(base.Card.Id);
				return true;
			}
			if (this.GetLevel4CountOnField() == 1 && sendNotNessary)
			{
				List<ClientCard> nextCostList = this.GetCostFromHandAndField(base.Card, true);
				if (nextCostList.Count<ClientCard>() > 0)
				{
					base.AI.SelectCard(nextCostList);
					base.AI.SelectNextCard(rebornTarget);
					this.activatedCardIdList.Add(base.Card.Id);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x000ABB84 File Offset: 0x000A9D84
		public bool ExRyzealSpSummon()
		{
			if (this.CheckShouldNoMoreSpSummon((CardLocation)66))
			{
				return !this.CheckWhetherHaveFinalMonster() && this.GetLevel4CountOnField() == 1 && this.ExRyzealDiscardExtra();
			}
			if (base.Duel.Turn == 1)
			{
				if ((!this.activatedCardIdList.Contains(34022970) && !this.lockBirdSolved && !base.DefaultCheckWhetherCardIdIsNegated(34022970) && !base.Bot.HasInMonstersZone(90590303, false, false, false)) | (!base.Bot.MonsterZone.Any((ClientCard c) => c != null && c.IsFaceup() && c.HasType(CardType.Xyz)) && this.GetLevel4CountOnField() == 1))
				{
					base.AI.SelectCard(new int[] { 34909328, 7511613 });
					return true;
				}
			}
			return this.ExRyzealDiscardExtra();
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x000ABC7C File Offset: 0x000A9E7C
		public bool ExRyzealDiscardExtra()
		{
			List<int> discardIdCheckList = new List<int>
			{
				9940036, 45852939, 2061963, 16643334, 6983839, 21044178, 46772449, 1269512, 66011101, 7511613,
				34909328
			};
			List<int> discardIdList = new List<int>();
			foreach (int discardId in discardIdCheckList)
			{
				if (discardId == 9940036)
				{
					if (!base.Enemy.MonsterZone.Any((ClientCard c) => c != null && c.IsFaceup() && !c.IsDisabled() && !c.IsShouldNotBeMonsterTarget()))
					{
						if (base.Enemy.SpellZone.Any((ClientCard c) => c != null && c.IsFaceup() && !c.IsDisabled() && !c.IsShouldNotBeMonsterTarget()))
						{
							continue;
						}
					}
				}
				if (discardId == 45852939)
				{
					if (base.Bot.Graveyard.Count((ClientCard c) => c.HasType(CardType.Xyz)) < 2)
					{
						continue;
					}
					if (!base.Bot.Graveyard.Any((ClientCard c) => c.HasType(CardType.Xyz) && c.IsCanRevive()))
					{
						continue;
					}
				}
				if ((discardId != 2061963 || (this.CheckRemainInDeck(7477101) <= 0 && !base.Bot.HasInHandOrInSpellZone(2061963))) && (discardId != 6983839 || base.Enemy.GetSpellCount() <= 0))
				{
					discardIdList.Add(discardId);
				}
			}
			discardIdList.AddRange(discardIdCheckList);
			foreach (int id in discardIdList)
			{
				if (base.Bot.HasInExtra(id))
				{
					base.AI.SelectCard(id);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool ExRyzealSpSummonLater()
		{
			return false;
		}

		// Token: 0x06001C05 RID: 7173 RVA: 0x000ABEE8 File Offset: 0x000AA0E8
		public bool ExRyzealSummon()
		{
			if (this.CheckShouldNoMoreSpSummon(CardLocation.Extra) && this.GetLevel4CountOnField() == 1 && (!this.activatedCardIdList.Contains(34022970) & (!base.Bot.HasInHand(35844557) || this.activatedCardIdList.Contains(35844557))))
			{
				this.summonCount--;
				return true;
			}
			return false;
		}

		// Token: 0x06001C06 RID: 7174 RVA: 0x000ABF54 File Offset: 0x000AA154
		public bool ExRyzealActivate()
		{
			if (!this.CheckWhetherNegated(true, false, (CardType)0, false))
			{
				this.activatedCardIdList.Add(base.Card.Id);
				return true;
			}
			bool flag;
			if (base.Enemy.GetMonsters().Count((ClientCard c) => c.IsCode(90590303) && c.IsFaceup() && !c.IsDisabled() && c.HasPosition(CardPosition.FaceUpDefence)) == 1)
			{
				flag = this.currentCanActivateEffect.Any((ClientCard c) => c != null && c.IsCode(9940036));
			}
			else
			{
				flag = false;
			}
			if (flag)
			{
				this.activatedCardIdList.Add(base.Card.Id);
				return true;
			}
			return false;
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x000AC000 File Offset: 0x000AA200
		public bool MulcharmyFuwalosActivate()
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0, false) || base.Duel.Player == 0)
			{
				return false;
			}
			if (!this.lockBirdSolved)
			{
				if (!base.Duel.CurrentChain.Any((ClientCard c) => c.IsCode(94145021)))
				{
					if (base.Duel.Phase > DuelPhase.Main1)
					{
						return false;
					}
					this.botActivateMulcharmy = true;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001C08 RID: 7176 RVA: 0x000AC07C File Offset: 0x000AA27C
		public bool MulcharmyPuruliaActivate()
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0, false) || base.Duel.Player == 0)
			{
				return false;
			}
			if (!this.lockBirdSolved)
			{
				if (!base.Duel.CurrentChain.Any((ClientCard c) => c.IsCode(94145021)))
				{
					if (base.Duel.Phase > DuelPhase.Main1)
					{
						return false;
					}
					if (this.botActivateMulcharmy)
					{
						return false;
					}
					this.botActivateMulcharmy = true;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x000AC104 File Offset: 0x000AA304
		public bool MulcharmyNyalusActivate()
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0, false) || base.Duel.Player == 0)
			{
				return false;
			}
			if (!this.lockBirdSolved)
			{
				if (!base.Duel.CurrentChain.Any((ClientCard c) => c.IsCode(94145021)))
				{
					if (base.Duel.Phase > DuelPhase.Main1)
					{
						return false;
					}
					if (this.botActivateMulcharmy)
					{
						return false;
					}
					this.botActivateMulcharmy = true;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x000AC18C File Offset: 0x000AA38C
		public bool AshBlossomActivate()
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0, false) || !this.CheckLastChainShouldNegated())
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

		// Token: 0x06001C0B RID: 7179 RVA: 0x000AC1F0 File Offset: 0x000AA3F0
		public bool GhostOgreAndSnowRabbitActivate()
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0, false) || base.Duel.LastChainPlayer == 0)
			{
				return false;
			}
			ClientCard lastChainCard = base.Util.GetLastChainCard();
			return lastChainCard != null && !lastChainCard.IsDisabled() && (!lastChainCard.IsMonster() || lastChainCard.HasType((CardType)75505728));
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x000AC249 File Offset: 0x000AA449
		public bool MaxxCActivate()
		{
			return !this.CheckWhetherNegated(true, false, (CardType)0, false) && base.Duel.LastChainPlayer != 0 && !this.lockBirdSolved && base.DefaultMaxxC();
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x000AC274 File Offset: 0x000AA474
		public bool LockBirdActivate()
		{
			return !this.CheckWhetherNegated(true, false, (CardType)0, false) && base.Duel.Player != 0 && (!new List<int> { 84192580, 42141493 }.Intersect(this.botSolvedCardIdList).Any<int>() || base.Enemy.GetMonsterCount() + base.Enemy.GetSpellCount() + 6 >= base.Bot.Hand.Count<ClientCard>()) && (!this.botSolvedCardIdList.Contains(23434538) || (this.activatedCardIdList.Contains(14558127) && this.activatedCardIdList.Contains(97268402)));
		}

		// Token: 0x06001C0E RID: 7182 RVA: 0x000AC334 File Offset: 0x000AA534
		public bool EffectVeilerActivate()
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0, false))
			{
				return false;
			}
			List<ClientCard> shouldNegateList = this.GetMonsterListForTargetNegate(true, CardType.Monster);
			if (shouldNegateList.Count > 0)
			{
				ClientCard negateTarget = shouldNegateList[0];
				this.currentNegateCardList.Add(negateTarget);
				base.AI.SelectCard(negateTarget);
				return true;
			}
			return false;
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x000AC384 File Offset: 0x000AA584
		public bool SeventhTachyonActivate()
		{
			if (this.CheckWhetherNegated(true, true, CardType.Spell, false))
			{
				return false;
			}
			if ((!this.spSummonedCardIdList.Contains(34022970) && !base.Bot.HasInHand(34022970)) & (this.activatedCardIdList.Contains(8633261) || this.summonCount <= 0 || !base.Bot.HasInHand(8633261) || base.DefaultCheckWhetherCardIdIsNegated(8633261)))
			{
				this.SelectSTPlace(base.Card, true, null);
				return true;
			}
			return false;
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x000AC416 File Offset: 0x000AA616
		public bool TripleTacticsTalentActivate()
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0, false))
			{
				return false;
			}
			if (this.TripleTacticsTalentDecision(null) == -1)
			{
				return false;
			}
			this.SelectSTPlace(base.Card, true, null);
			return true;
		}

		// Token: 0x06001C11 RID: 7185 RVA: 0x000AC444 File Offset: 0x000AA644
		public int TripleTacticsTalentDecision(IList<int> options)
		{
			if (base.Enemy.GetMonsters().Any((ClientCard c) => c.IsFaceup() && !c.IsDisabled() && (c.IsFloodgate() || (c.IsCode(90590303) && c.HasPosition(CardPosition.FaceUpDefence)))))
			{
				if (options == null)
				{
					return 1;
				}
				int res = options.IndexOf(base.Util.GetStringId(25311006, 1));
				if (res >= 0)
				{
					return res;
				}
			}
			if (!this.lockBirdSolved && !this.CheckCanContinueSummon(false))
			{
				if (options == null)
				{
					return 1;
				}
				int res2 = options.IndexOf(base.Util.GetStringId(25311006, 0));
				if (res2 >= 0)
				{
					return res2;
				}
			}
			if (base.Enemy.Hand.Count<ClientCard>() > 0)
			{
				if (options == null)
				{
					return 1;
				}
				int res3 = options.IndexOf(base.Util.GetStringId(25311006, 2));
				if (res3 >= 0)
				{
					return res3;
				}
			}
			return -1;
		}

		// Token: 0x06001C12 RID: 7186 RVA: 0x000AC510 File Offset: 0x000AA710
		public bool PotOfDesiresActivate()
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0, false))
			{
				return false;
			}
			if (base.Bot.Deck.Count >= 15)
			{
				this.SelectSTPlace(base.Card, true, null);
				return true;
			}
			return false;
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x000AC548 File Offset: 0x000AA748
		public bool PotOfDesireActivateForContinue()
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0, false))
			{
				return false;
			}
			if (base.Bot.Deck.Count >= 15 && !this.CheckCanContinueSummon(false) && this.CheckRemainInDeck(new int[] { 8633261, 35844557, 34022970 }) > 0)
			{
				this.SelectSTPlace(base.Card, true, null);
				return true;
			}
			return false;
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x000AC5AC File Offset: 0x000AA7AC
		public bool BonfireActivateToSearchNecessary()
		{
			if (this.CheckWhetherNegated(true, true, CardType.Spell, false))
			{
				return false;
			}
			if ((!this.spSummonedCardIdList.Contains(34022970) && !base.Bot.HasInHand(34022970)) | (!this.activatedCardIdList.Contains(8633261) && this.summonCount > 0 && !base.Bot.HasInHand(8633261) && !base.DefaultCheckWhetherCardIdIsNegated(8633261)))
			{
				this.SelectSTPlace(base.Card, true, null);
				return true;
			}
			return false;
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x000AC644 File Offset: 0x000AA844
		public bool BonfireActivate()
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0, false))
			{
				return false;
			}
			if (!this.activatedCardIdList.Contains(66011101))
			{
				ClientCard no60 = base.Bot.MonsterZone.FirstOrDefault((ClientCard c) => c != null && c.IsFaceup() && c.IsCode(66011101) && !c.IsDisabled());
				if (no60 != null && no60.Overlays.Count<int>() >= 2)
				{
					this.SelectSTPlace(base.Card, true, null);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x000AC6C4 File Offset: 0x000AA8C4
		public bool CalledbytheGraveActivate()
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0, false) || !this.CheckLastChainShouldNegated())
			{
				return false;
			}
			ClientCard lastChainCard = base.Util.GetLastChainCard();
			if (base.Duel.LastChainPlayer == 1)
			{
				if (lastChainCard != null && lastChainCard.IsMonster())
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
					List<int> mulcharmyIdList = new List<int> { 84192580, 42141493, 87126721 };
					if (base.Duel.Player == 0 && base.Bot.HasInHand(code) && !mulcharmyIdList.Contains(code))
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
						this.currentNegateCardList.AddRange(base.Enemy.MonsterZone.Where((ClientCard c) => c != null && c.IsFaceup() && c.IsCode(code)));
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
					goto IL_0285;
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
			IL_0285:
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

		// Token: 0x06001C17 RID: 7191 RVA: 0x000AC9D4 File Offset: 0x000AABD4
		public bool RyzealPlugInActivate()
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0, false))
			{
				return false;
			}
			if (base.Duel.Player == 0 && base.CurrentTiming == -1)
			{
				bool summonFlag = this.GetLevel4CountOnField() == 1;
				if (this.GetLevel4CountOnField() == 0)
				{
					bool flag;
					summonFlag |= this.GetLevel4FinalCountOnField(true, out flag) >= 2 && !this.CheckWhetherHaveFinalMonster();
				}
				if (summonFlag)
				{
					List<int> checkIdList = new List<int> { 72238166, 34022970, 35844557, 8633261 };
					using (List<int>.Enumerator enumerator = checkIdList.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							int id2 = enumerator.Current;
							if (!this.activatedCardIdList.Contains(id2))
							{
								ClientCard target = base.Bot.Banished.FirstOrDefault((ClientCard c) => c != null && c.IsFaceup() && c.IsCode(id2));
								if (target == null)
								{
									target = base.Bot.Graveyard.FirstOrDefault((ClientCard c) => c != null && c.IsFaceup() && c.IsCode(id2));
								}
								if (target != null)
								{
									base.AI.SelectCard(target);
									this.SelectSTPlace(base.Card, true, null);
									return true;
								}
							}
						}
					}
					using (List<int>.Enumerator enumerator = checkIdList.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							int id3 = enumerator.Current;
							ClientCard target2 = base.Bot.Banished.FirstOrDefault((ClientCard c) => c != null && c.IsFaceup() && c.IsCode(id3));
							if (target2 == null)
							{
								target2 = base.Bot.Graveyard.FirstOrDefault((ClientCard c) => c != null && c.IsFaceup() && c.IsCode(id3));
							}
							if (target2 != null)
							{
								base.AI.SelectCard(target2);
								this.SelectSTPlace(base.Card, true, null);
								return true;
							}
						}
					}
				}
			}
			if (!base.Bot.HasInMonstersZone(34909328, true, true, true))
			{
				if (!base.Duel.CurrentChain.Any((ClientCard c) => c.IsCode(45852939)) && !base.DefaultCheckWhetherCardIdIsNegated(34909328) && !base.Util.ChainContainPlayer(0))
				{
					ClientCard deadnader = base.Bot.Banished.FirstOrDefault((ClientCard c) => c != null && c.IsFaceup() && c.IsCode(34909328) && c.IsCanRevive());
					if (deadnader == null)
					{
						deadnader = base.Bot.Graveyard.FirstOrDefault((ClientCard c) => c != null && c.IsFaceup() && c.IsCode(34909328) && c.IsCanRevive());
					}
					if (deadnader != null)
					{
						base.AI.SelectCard(deadnader);
						this.SelectSTPlace(base.Card, true, null);
						return true;
					}
				}
			}
			if (base.Bot.HasInSpellZone(6798031, true, true) && !this.activatedCardIdList.Contains(6798033) && this.CheckRemainInDeck(new int[] { 34022970, 8633261, 72238166, 35844557 }) > 0)
			{
				ClientCard lastChainCard = base.Util.GetLastChainCard();
				if (lastChainCard != null && lastChainCard.IsMonster() && lastChainCard.Controller == 1 && this.CheckCardShouldNegate(lastChainCard))
				{
					bool activateFlag = false;
					bool shouldRebornXyz = false;
					if (base.Duel.CurrentChain.Any((ClientCard c) => c.IsCode(7511613) && c.Controller == 0) && this.activatedCardIdList.Contains(7511614))
					{
						activateFlag = base.Bot.MonsterZone.Where((ClientCard c) => c != null && c.HasType(CardType.Xyz)).Sum((ClientCard c) => c.Overlays.Count<int>()) >= 3;
					}
					else if (!base.Bot.MonsterZone.Any((ClientCard c) => c != null && c.IsFaceup() && c.HasSetcode(446) && c.Overlays.Count<int>() > 0))
					{
						activateFlag |= base.Bot.MonsterZone.Any((ClientCard c) => c != null && c.IsFaceup() && c.HasType(CardType.Xyz) && c.HasSetcode(446));
						if (!activateFlag)
						{
							if (base.Bot.Banished.Any((ClientCard c) => c != null && c.IsFaceup() && c.HasSetcode(446) && c.IsCanRevive() && c.HasType(CardType.Xyz)) | base.Bot.Graveyard.Any((ClientCard c) => c != null && c.IsFaceup() && c.HasSetcode(446) && c.IsCanRevive() && c.HasType(CardType.Xyz)))
							{
								activateFlag = true;
								shouldRebornXyz = true;
							}
						}
					}
					if (activateFlag)
					{
						ClientCard deadnader2 = base.Bot.Banished.FirstOrDefault((ClientCard c) => c != null && c.IsFaceup() && c.IsCanRevive() && c.IsCode(34909328));
						if (deadnader2 == null)
						{
							deadnader2 = base.Bot.Graveyard.FirstOrDefault((ClientCard c) => c != null && c.IsFaceup() && c.IsCanRevive() && c.IsCode(34909328));
						}
						if (deadnader2 != null)
						{
							base.AI.SelectCard(deadnader2);
							this.SelectSTPlace(base.Card, true, null);
							return true;
						}
						if (shouldRebornXyz)
						{
							ClientCard duoDriver = base.Bot.Banished.FirstOrDefault((ClientCard c) => c != null && c.IsFaceup() && c.IsCanRevive() && c.IsCode(7511613));
							if (duoDriver == null)
							{
								duoDriver = base.Bot.Graveyard.FirstOrDefault((ClientCard c) => c != null && c.IsFaceup() && c.IsCanRevive() && c.IsCode(7511613));
							}
							if (duoDriver != null)
							{
								base.AI.SelectCard(duoDriver);
								this.SelectSTPlace(base.Card, true, null);
								return true;
							}
						}
						else
						{
							List<int> checkIdList2 = new List<int> { 72238166, 34022970, 35844557, 8633261 };
							using (List<int>.Enumerator enumerator = checkIdList2.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									int id4 = enumerator.Current;
									if (!this.activatedCardIdList.Contains(id4))
									{
										ClientCard target3 = base.Bot.Banished.FirstOrDefault((ClientCard c) => c != null && c.IsFaceup() && c.IsCode(id4));
										if (target3 == null)
										{
											target3 = base.Bot.Graveyard.FirstOrDefault((ClientCard c) => c != null && c.IsFaceup() && c.IsCode(id4));
										}
										if (target3 != null)
										{
											base.AI.SelectCard(target3);
											this.SelectSTPlace(base.Card, true, null);
											return true;
										}
									}
								}
							}
							using (List<int>.Enumerator enumerator = checkIdList2.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									int id5 = enumerator.Current;
									ClientCard target4 = base.Bot.Banished.FirstOrDefault((ClientCard c) => c != null && c.IsFaceup() && c.IsCode(id5));
									if (target4 == null)
									{
										target4 = base.Bot.Graveyard.FirstOrDefault((ClientCard c) => c != null && c.IsFaceup() && c.IsCode(id5));
									}
									if (target4 != null)
									{
										base.AI.SelectCard(target4);
										this.SelectSTPlace(base.Card, true, null);
										return true;
									}
								}
							}
						}
					}
				}
			}
			bool flag2 = base.DefaultOnBecomeTarget() && base.Card.Location == CardLocation.SpellZone;
			bool endPhaseFlag = base.Duel.Player == 1 && base.Duel.Phase == DuelPhase.End;
			if (flag2 || endPhaseFlag)
			{
				if (!base.Duel.CurrentChain.Any((ClientCard c) => c != null && c.Controller == 1 && c.IsCode(15693423)) || this.deadnaderDestroySelf == null)
				{
					ClientCard deadnader3 = base.Bot.Banished.FirstOrDefault((ClientCard c) => c != null && c.IsFaceup() && c.IsCanRevive() && c.IsCode(34909328));
					if (deadnader3 == null)
					{
						deadnader3 = base.Bot.Graveyard.FirstOrDefault((ClientCard c) => c != null && c.IsFaceup() && c.IsCanRevive() && c.IsCode(34909328));
					}
					if (deadnader3 != null)
					{
						base.AI.SelectCard(deadnader3);
						this.SelectSTPlace(base.Card, true, null);
						return true;
					}
					ClientCard duoDriver2 = base.Bot.Banished.FirstOrDefault((ClientCard c) => c != null && c.IsFaceup() && c.IsCanRevive() && c.IsCode(7511613));
					if (duoDriver2 == null)
					{
						duoDriver2 = base.Bot.Graveyard.FirstOrDefault((ClientCard c) => c != null && c.IsFaceup() && c.IsCanRevive() && c.IsCode(7511613));
					}
					if (duoDriver2 != null)
					{
						base.AI.SelectCard(duoDriver2);
						this.SelectSTPlace(base.Card, true, null);
						return true;
					}
					List<int> checkIdList3 = new List<int> { 72238166, 34022970, 35844557, 8633261 };
					using (List<int>.Enumerator enumerator = checkIdList3.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							int id6 = enumerator.Current;
							if (!this.activatedCardIdList.Contains(id6))
							{
								ClientCard target5 = base.Bot.Banished.FirstOrDefault((ClientCard c) => c != null && c.IsFaceup() && c.IsCode(id6));
								if (target5 == null)
								{
									target5 = base.Bot.Graveyard.FirstOrDefault((ClientCard c) => c != null && c.IsFaceup() && c.IsCode(id6));
								}
								if (target5 != null)
								{
									base.AI.SelectCard(target5);
									this.SelectSTPlace(base.Card, true, null);
									return true;
								}
							}
						}
					}
					if (!endPhaseFlag)
					{
						using (List<int>.Enumerator enumerator = checkIdList3.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								int id = enumerator.Current;
								ClientCard target6 = base.Bot.Banished.FirstOrDefault((ClientCard c) => c != null && c.IsFaceup() && c.IsCode(id));
								if (target6 == null)
								{
									target6 = base.Bot.Graveyard.FirstOrDefault((ClientCard c) => c != null && c.IsFaceup() && c.IsCode(id));
								}
								if (target6 != null)
								{
									base.AI.SelectCard(target6);
									this.SelectSTPlace(base.Card, true, null);
									return true;
								}
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x000AD4E8 File Offset: 0x000AB6E8
		public bool RyzealPlugInActivateFirst()
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0, false))
			{
				return false;
			}
			if (base.Duel.Player == 0 && base.CurrentTiming == -1 && !this.activatedCardIdList.Contains(72238166) && !base.DefaultCheckWhetherCardIdIsNegated(72238166) && this.GetCostFromHandAndField(base.Card, false).Count<ClientCard>() > 0)
			{
				ClientCard target = base.Bot.Banished.FirstOrDefault((ClientCard c) => c != null && c.IsFaceup() && c.IsCode(72238166));
				if (target == null)
				{
					target = base.Bot.Graveyard.FirstOrDefault((ClientCard c) => c != null && c.IsFaceup() && c.IsCode(72238166));
				}
				if (target != null)
				{
					base.AI.SelectCard(target);
					this.SelectSTPlace(base.Card, true, null);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x000AD5E0 File Offset: 0x000AB7E0
		public bool RyzealCrossActivateCard()
		{
			RyzealExecutor.<>c__DisplayClass132_0 CS$<>8__locals1 = new RyzealExecutor.<>c__DisplayClass132_0();
			if (base.ActivateDescription == base.Util.GetStringId(6798031, 3))
			{
				ChainInfo currentChainInfo = base.Duel.GetCurrentSolvingChainInfo();
				if (currentChainInfo != null && !base.Duel.IsCurrentSolvingChainNegated() && this.CheckCardShouldNegate(currentChainInfo))
				{
					Logger.DebugWriteLine("** cross negate");
					this.activatedCardIdList.Add(6798033);
					this.botSolvingCross = true;
					return true;
				}
				return false;
			}
			else
			{
				if (this.CheckWhetherNegated(true, false, (CardType)0, false))
				{
					return false;
				}
				if (base.Card.Location == CardLocation.SpellZone && base.Card.IsFaceup())
				{
					return false;
				}
				bool flag = this.RyzealCrossActivateRecycleFirst();
				RyzealExecutor.<>c__DisplayClass132_0 CS$<>8__locals2 = CS$<>8__locals1;
				bool flag2;
				if (base.Bot.HasInHandOrInSpellZone(60394026) && this.CheckRemainInDeck(new int[] { 8633261, 34022970, 72238166, 35844557 }) > 0)
				{
					if (!base.Bot.Graveyard.Any((ClientCard c) => c != null && c.HasSetcode(446) && (c.IsCanRevive() || !c.HasType(CardType.Xyz))))
					{
						flag2 = base.Bot.Banished.Any((ClientCard c) => c != null && c.IsFaceup() && c.HasSetcode(446) && (c.IsCanRevive() || !c.HasType(CardType.Xyz)));
					}
					else
					{
						flag2 = true;
					}
				}
				else
				{
					flag2 = false;
				}
				CS$<>8__locals2.canSetMaterial = flag2;
				flag |= base.Bot.MonsterZone.Count((ClientCard c) => c != null && c.IsFaceup() && c.HasType(CardType.Xyz) && c.HasSetcode(446) && ((c.Overlays.Count<int>() > 0) | CS$<>8__locals1.canSetMaterial)) > 0;
				if (base.Duel.MainPhase.SpecialSummonableCards.Any((ClientCard c) => c.IsCode(7511613)))
				{
					flag |= this.RyzealDuodriveSpSummonCheck();
				}
				return flag;
			}
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x000AD784 File Offset: 0x000AB984
		public bool RyzealCrossActivateRecycleFirst()
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0, false) || base.Card.Location != CardLocation.SpellZone || !base.Card.IsFaceup())
			{
				return false;
			}
			if (base.ActivateDescription == base.Util.GetStringId(6798031, 3))
			{
				return false;
			}
			List<int> list = new List<int>();
			list.Add(60394026);
			list.Add(7511613);
			list.Add(34909328);
			list.Add(72238166);
			list.Add(34022970);
			list.Add(8633261);
			list.Add(35844557);
			List<ClientCard> targetList = new List<ClientCard>();
			using (List<int>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int id = enumerator.Current;
					ClientCard target = base.Bot.Graveyard.FirstOrDefault((ClientCard c) => c.IsCode(id));
					if (target != null && this.CheckRemainInDeck(id) + base.Bot.ExtraDeck.Count((ClientCard c) => c.IsCode(id)) + base.Bot.Hand.Count((ClientCard c) => c.IsCode(id)) == 0)
					{
						if (target.HasType(CardType.Xyz) && this.GetLevel4CountOnField() == 1)
						{
							continue;
						}
						targetList.Add(target);
					}
					if (targetList.Count<ClientCard>() >= 2)
					{
						base.AI.SelectCard(targetList);
						this.activatedCardIdList.Add(base.Card.Id + 1);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x000AD934 File Offset: 0x000ABB34
		public bool RyzealCrossActivateRecycleLater()
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0, false) || base.Card.Location != CardLocation.SpellZone || !base.Card.IsFaceup())
			{
				return false;
			}
			if (base.ActivateDescription == base.Util.GetStringId(6798031, 3))
			{
				return false;
			}
			SortedDictionary<int, List<int>> countDict = new SortedDictionary<int, List<int>>();
			using (List<int>.Enumerator enumerator = new List<int> { 60394026, 7511613, 34909328, 72238166, 34022970, 8633261, 35844557 }.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int id2 = enumerator.Current;
					int remainCount = this.CheckRemainInDeck(id2) + base.Bot.ExtraDeck.Count((ClientCard c) => c.IsCode(id2));
					if (!countDict.ContainsKey(remainCount))
					{
						countDict.Add(remainCount, new List<int>());
					}
					countDict[remainCount].Add(id2);
				}
			}
			List<ClientCard> targetList = new List<ClientCard>();
			foreach (KeyValuePair<int, List<int>> pair in countDict)
			{
				using (List<int>.Enumerator enumerator = pair.Value.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int id = enumerator.Current;
						ClientCard target = base.Bot.Graveyard.FirstOrDefault((ClientCard c) => c.IsCode(id));
						if (target != null)
						{
							targetList.Add(target);
						}
					}
				}
			}
			if (targetList.Count<ClientCard>() >= 2)
			{
				base.AI.SelectCard(targetList);
				this.activatedCardIdList.Add(base.Card.Id + 1);
				return true;
			}
			return false;
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x000ADB58 File Offset: 0x000ABD58
		public bool CrossoutDesignatorActivate()
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0, false) || !this.CheckLastChainShouldNegated())
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
					this.currentNegateCardList.AddRange(base.Enemy.MonsterZone.Where((ClientCard c) => c != null && c.IsFaceup() && c.IsCode(code)));
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x000ADC5C File Offset: 0x000ABE5C
		public bool InfiniteImpermanenceActivate()
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0, false))
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

		// Token: 0x06001C1E RID: 7198 RVA: 0x000ADE4C File Offset: 0x000AC04C
		public bool AbyssDwellerSummonCheck()
		{
			bool flag = this.enemyDeckTypeRecord.Contains(119);
			flag |= this.enemyDeckTypeRecord.Contains(283);
			flag |= this.enemyDeckTypeRecord.Contains(4315);
			bool flag2 = flag;
			bool flag3;
			if (this.enemyDeckTypeRecord.Count<int>() > 0)
			{
				flag3 = this.enemyDeckTypeRecord.All((int c) => c == 385);
			}
			else
			{
				flag3 = false;
			}
			flag = flag2 || flag3;
			if (base.Enemy.Hand.Count<ClientCard>() + base.Enemy.Deck.Count<ClientCard>() + base.Enemy.Graveyard.Count<ClientCard>() + base.Enemy.Banished.Count<ClientCard>() + base.Enemy.ExtraDeck.Count<ClientCard>() > 65)
			{
				flag |= !this.enemyDeckTypeRecord.Contains(187);
			}
			return flag;
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x000ADF3A File Offset: 0x000AC13A
		public bool Number41BagooskatheTerriblyTiredTapirSummonCheck()
		{
			return (this.enemyDeckTypeRecord.Contains(365) | this.enemyDeckTypeRecord.Contains(349) | this.enemyDeckTypeRecord.Contains(446)) & base.Util.IsTurn1OrMain2();
		}

		// Token: 0x06001C20 RID: 7200 RVA: 0x000ADF7C File Offset: 0x000AC17C
		public bool TornadoDragonSummonCheck()
		{
			if (this.CheckWhetherNegated(true, true, CardType.Monster, false))
			{
				return false;
			}
			return this.enemyDeckTypeRecord.Contains(382) | base.Enemy.SpellZone.Any((ClientCard c) => c != null && c.IsFaceup() && !c.IsDisabled() && c.IsFloodgate()) | (base.Enemy.SpellZone.Count((ClientCard c) => c != null && !c.IsShouldNotBeMonsterTarget() && !this.NotToDestroySpellTrap.Contains(c.Id)) >= 3) | (!base.Util.IsTurn1OrMain2() && !this.botSolvedCardIdList.Contains(46772449) && base.Enemy.GetMonsterCount() == 0 && base.Enemy.SpellZone.Count((ClientCard c) => c != null && !c.IsShouldNotBeMonsterTarget() && !this.NotToDestroySpellTrap.Contains(c.Id)) > 0);
		}

		// Token: 0x06001C21 RID: 7201 RVA: 0x000AE049 File Offset: 0x000AC249
		public bool EvilswarmExcitonKnightSpSummon()
		{
			return !this.CheckWhetherNegated(true, true, CardType.Monster, false) && base.Duel.Turn != 1 && base.DefaultEvilswarmExcitonKnightSummon();
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x000AE070 File Offset: 0x000AC270
		public bool LessSpSummonExtra()
		{
			if (!this.CheckShouldNoMoreSpSummon(CardLocation.Extra))
			{
				return false;
			}
			ClientCard no41 = base.Duel.MainPhase.SpecialSummonableCards.FirstOrDefault((ClientCard c) => c.IsCode(90590303));
			if (no41 != null && this.Number41BagooskatheTerriblyTiredTapirSummonCheck())
			{
				if (base.Card != no41)
				{
					return false;
				}
				List<ClientCard> materialList = this.GetLevel4OnField(null);
				if (materialList.Count<ClientCard>() >= 2)
				{
					base.AI.SelectMaterials(materialList, 0);
					return true;
				}
			}
			ClientCard abyss = base.Duel.MainPhase.SpecialSummonableCards.FirstOrDefault((ClientCard c) => c.IsCode(21044178));
			if (abyss != null && this.AbyssDwellerSummonCheck())
			{
				if (base.Card != abyss)
				{
					return false;
				}
				List<ClientCard> materialList2 = this.GetLevel4OnField(null);
				if (materialList2.Count<ClientCard>() >= 2)
				{
					base.AI.SelectMaterials(materialList2, 0);
					return true;
				}
			}
			ClientCard deadnader = base.Duel.MainPhase.SpecialSummonableCards.FirstOrDefault((ClientCard c) => c.IsCode(34909328));
			if (deadnader != null)
			{
				List<ClientCard> materialList3 = this.GetLevel4OnField((ClientCard c) => c.HasSetcode(446));
				if (materialList3.Count<ClientCard>() >= 2)
				{
					if (base.Card != deadnader)
					{
						return false;
					}
					base.AI.SelectMaterials(materialList3, 0);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001C23 RID: 7203 RVA: 0x000AE1E8 File Offset: 0x000AC3E8
		public bool FirstRyzealDuodriveSpSummon()
		{
			if (!this.RyzealDuodriveSpSummonCheck())
			{
				return false;
			}
			if (base.Bot.Graveyard.Count((ClientCard c) => c.HasSetcode(446) && c.IsMonster()) == 0 && !this.CheckShouldNoMoreSpSummon(CardLocation.Hand) && base.Bot.HasInHand(34022970) && !this.spSummonedCardIdList.Contains(34022970))
			{
				if (base.Duel.MainPhase.SpecialSummonableCards.Any((ClientCard c) => c.IsCode(34022970)))
				{
					if (base.Bot.ExtraDeck.Count((ClientCard c) => c.IsCode(new int[] { 34909328, 7511613 })) > 2)
					{
						return false;
					}
				}
			}
			List<ClientCard> materialList = this.GetLevel4OnField(null);
			List<ClientCard> materialExceptNode = materialList.Where((ClientCard c) => !c.IsCode(72238166) || c.IsDisabled() || this.activatedCardIdList.Contains(72238166)).ToList<ClientCard>();
			if (materialExceptNode.Count<ClientCard>() >= 2)
			{
				base.AI.SelectMaterials(materialExceptNode.Take(2).ToList<ClientCard>(), 0);
				return true;
			}
			if (materialList.Count<ClientCard>() > 2 && !this.CheckCanContinueSummon(false))
			{
				base.AI.SelectMaterials(materialList.Take(2).ToList<ClientCard>(), 0);
				return true;
			}
			if (materialList.Count<ClientCard>() >= 2 && !this.CheckCanContinueSummon(true))
			{
				base.AI.SelectMaterials(materialList.Take(2).ToList<ClientCard>(), 0);
				return true;
			}
			return false;
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x000AE36C File Offset: 0x000AC56C
		public bool RyzealDuodriveSpSummonCheck()
		{
			return base.Duel.MainPhase.SpecialSummonableCards.Any((ClientCard c) => c.IsCode(7511613)) & !base.Bot.HasInMonstersZone(7511613, true, true, true) & (this.CheckRemainInDeck(new int[] { 8633261, 35844557, 72238166, 34022970, 60394026, 6798031 }) >= 2) & !base.DefaultCheckWhetherCardIdIsNegated(7511613) & !this.activatedCardIdList.Contains(7511614) & !this.CheckWhetherNegated(true, true, CardType.Monster, false) & !this.lockBirdSolved & !this.CheckShouldNoMoreSpSummon(CardLocation.Extra);
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x000AE42C File Offset: 0x000AC62C
		public bool SecondXyzSummon()
		{
			if (base.Card.Location != CardLocation.Extra)
			{
				return false;
			}
			bool flag;
			int level4Count = this.GetLevel4FinalCountOnField(true, out flag);
			bool result = this.SecondXyzSummonInner();
			Logger.DebugWriteLine("Second Xyz Count: " + level4Count.ToString());
			Logger.DebugWriteLine("Second Xyz Summon: " + result.ToString());
			return result;
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x000AE488 File Offset: 0x000AC688
		public bool SecondXyzSummonInner()
		{
			if (this.CheckShouldNoMoreSpSummon(CardLocation.Extra))
			{
				return false;
			}
			if (this.RyzealDuodriveSpSummonCheck())
			{
				Logger.DebugWriteLine("Second: summon duodriver first");
				return false;
			}
			bool hasNode;
			if (this.GetLevel4FinalCountOnField(true, out hasNode) < 4)
			{
				return false;
			}
			List<ClientCard> materialList = this.GetLevel4OnField(null);
			List<ClientCard> materialExceptNode = materialList.Where((ClientCard c) => !c.IsCode(72238166) || c.IsDisabled() || this.activatedCardIdList.Contains(72238166)).ToList<ClientCard>();
			ClientCard abyss = base.Duel.MainPhase.SpecialSummonableCards.FirstOrDefault((ClientCard c) => c.IsCode(21044178));
			if (abyss != null && this.AbyssDwellerSummonCheck())
			{
				if (base.Card != abyss)
				{
					return false;
				}
				if (materialExceptNode.Count<ClientCard>() >= 2)
				{
					base.AI.SelectMaterials(materialExceptNode.Take(2).ToList<ClientCard>(), 0);
					return true;
				}
				if (materialList.Count<ClientCard>() > 2 && !this.CheckCanContinueSummon(false))
				{
					base.AI.SelectMaterials(materialList.Take(2).ToList<ClientCard>(), 0);
					return true;
				}
			}
			ClientCard no41 = base.Duel.MainPhase.SpecialSummonableCards.FirstOrDefault((ClientCard c) => c.IsCode(90590303));
			if (no41 != null)
			{
				bool flag = hasNode & base.Util.IsTurn1OrMain2();
				bool flag2;
				if (base.Bot.HasInExtra(45852939))
				{
					flag2 = base.Bot.MonsterZone.Any((ClientCard c) => c != null && c.IsFaceup() && c.HasType(CardType.Xyz));
				}
				else
				{
					flag2 = false;
				}
				if ((flag && flag2) & (this.GetNegateEffectCount() >= 2 || this.lockBirdSolved))
				{
					if (base.Card != no41)
					{
						return false;
					}
					if (materialExceptNode.Count<ClientCard>() >= 2)
					{
						base.AI.SelectMaterials(materialExceptNode.Take(2).ToList<ClientCard>(), 0);
						return true;
					}
					if (materialList.Count<ClientCard>() >= 2 && base.Bot.HasInHandOrInSpellZone(60394026))
					{
						base.AI.SelectMaterials(materialList.Take(2).ToList<ClientCard>(), 0);
						return true;
					}
				}
			}
			ClientCard photonDragon = base.Duel.MainPhase.SpecialSummonableCards.FirstOrDefault((ClientCard c) => c.IsCode(16643334));
			bool have2MaterialDuo = false;
			if (photonDragon != null)
			{
				int duoDriveOverlayCount = 0;
				foreach (ClientCard monster in base.Bot.MonsterZone)
				{
					if (monster != null && monster.IsCode(7511613))
					{
						duoDriveOverlayCount += monster.Overlays.Count<int>();
					}
				}
				if (base.Bot.HasInHandOrInSpellZone(60394026))
				{
					duoDriveOverlayCount++;
				}
				have2MaterialDuo = duoDriveOverlayCount >= 2;
			}
			if (photonDragon != null && have2MaterialDuo && this.enemyDeckTypeRecord.Contains(277))
			{
				if (base.Card != photonDragon)
				{
					return false;
				}
				if (materialExceptNode.Count<ClientCard>() >= 2)
				{
					base.AI.SelectMaterials(materialExceptNode.Take(2).ToList<ClientCard>(), 0);
					return true;
				}
			}
			ClientCard no42 = base.Duel.MainPhase.SpecialSummonableCards.FirstOrDefault((ClientCard c) => c.IsCode(66011101));
			if (no42 != null && !this.lockBirdSolved && base.Bot.Deck.Count<ClientCard>() > 2)
			{
				if (base.Card != no42)
				{
					return false;
				}
				if (materialExceptNode.Count<ClientCard>() >= 2)
				{
					base.AI.SelectMaterials(materialExceptNode.Take(2).ToList<ClientCard>(), 0);
					return true;
				}
				if (materialList.Count<ClientCard>() >= 2 && base.Bot.HasInHandOrInSpellZone(60394026))
				{
					base.AI.SelectMaterials(materialList.Take(2).ToList<ClientCard>(), 0);
					return true;
				}
			}
			if (photonDragon != null && have2MaterialDuo)
			{
				if (base.Card != photonDragon)
				{
					return false;
				}
				if (materialExceptNode.Count<ClientCard>() >= 2)
				{
					base.AI.SelectMaterials(materialExceptNode.Take(2).ToList<ClientCard>(), 0);
					return true;
				}
			}
			ClientCard deadnader = base.Duel.MainPhase.SpecialSummonableCards.FirstOrDefault((ClientCard c) => c.IsCode(34909328));
			if (deadnader == null)
			{
				ClientCard tornadoDragon = base.Duel.MainPhase.SummonableCards.FirstOrDefault((ClientCard c) => c.IsCode(6983839));
				if (tornadoDragon != null && this.TornadoDragonSummonCheck() && base.Card == tornadoDragon)
				{
					if (materialExceptNode.Count<ClientCard>() >= 2)
					{
						base.AI.SelectMaterials(materialExceptNode.Take(2).ToList<ClientCard>(), 0);
						return true;
					}
					if (materialList.Count<ClientCard>() >= 2 && base.Bot.HasInHandOrInSpellZone(60394026))
					{
						base.AI.SelectMaterials(materialList.Take(2).ToList<ClientCard>(), 0);
						return true;
					}
				}
			}
			if (deadnader != null && base.Card == deadnader)
			{
				if (materialExceptNode.Count<ClientCard>() >= 2)
				{
					base.AI.SelectMaterials(materialExceptNode.Take(2).ToList<ClientCard>(), 0);
					return true;
				}
				if (materialList.Count<ClientCard>() >= 2 && base.Bot.HasInHandOrInSpellZone(60394026))
				{
					base.AI.SelectMaterials(materialList.Take(2).ToList<ClientCard>(), 0);
					return true;
				}
			}
			Logger.DebugWriteLine("Second: no monster to spsummon");
			return false;
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x000AE9C8 File Offset: 0x000ACBC8
		public bool TwinsOfTheEclipseSpSummon()
		{
			if (this.CheckShouldNoMoreSpSummon(CardLocation.Extra))
			{
				return false;
			}
			if (base.Util.IsTurn1OrMain2())
			{
				bool hasNode = base.Bot.HasInHand(72238166) && !this.spSummonedCardIdList.Contains(72238166);
				hasNode |= base.Bot.HasInMonstersZone(72238166, true, false, true);
				if (base.Bot.HasInHandOrInSpellZone(60394026))
				{
					hasNode |= base.Bot.Graveyard.Any((ClientCard c) => c.IsCode(72238166));
					hasNode |= base.Bot.Banished.Any((ClientCard c) => c.IsFaceup() && c.IsCode(72238166));
				}
				hasNode &= !this.activatedCardIdList.Contains(72238166) && !base.DefaultCheckWhetherCardIdIsNegated(72238166);
				List<ClientCard> materialList = new List<ClientCard>();
				ClientCard duoDriver = base.Bot.MonsterZone.FirstOrDefault((ClientCard c) => c != null && c.IsFaceup() && c.IsCode(7511613));
				if (this.activatedCardIdList.Contains(7511614) && duoDriver != null)
				{
					materialList.Add(duoDriver);
					bool flag = hasNode;
					bool flag2;
					if (!this.CheckWhetherWillbeRemoved())
					{
						flag2 = duoDriver.Overlays.Any((int id) => id == 72238166);
					}
					else
					{
						flag2 = false;
					}
					hasNode = flag || flag2;
				}
				ClientCard no60 = base.Bot.MonsterZone.FirstOrDefault((ClientCard c) => c != null && c.IsFaceup() && c.IsCode(66011101));
				if (no60 != null && (this.activatedCardIdList.Contains(66011101) || no60.IsDisabled()))
				{
					materialList.Add(no60);
					bool flag3 = hasNode;
					bool flag4;
					if (!this.CheckWhetherWillbeRemoved())
					{
						flag4 = no60.Overlays.Any((int id) => id == 72238166);
					}
					else
					{
						flag4 = false;
					}
					hasNode = flag3 || flag4;
				}
				ClientCard no61 = base.Bot.MonsterZone.FirstOrDefault((ClientCard c) => c != null && c.IsFaceup() && c.IsCode(90590303));
				if (no61 != null)
				{
					materialList.Add(no61);
					bool flag5 = hasNode;
					bool flag6;
					if (!this.CheckWhetherWillbeRemoved())
					{
						flag6 = no61.Overlays.Any((int id) => id == 72238166);
					}
					else
					{
						flag6 = false;
					}
					hasNode = flag5 || flag6;
				}
				if (materialList.Count<ClientCard>() >= 2 && hasNode)
				{
					base.AI.SelectMaterials(materialList, 0);
					return true;
				}
			}
			else
			{
				if (this.botSolvedCardIdList.Contains(46772449))
				{
					return false;
				}
				List<ClientCard> materialList2 = this.GetLevel4OnField(null);
				List<ClientCard> xyzMonsterList = base.Bot.MonsterZone.Where((ClientCard c) => c != null && c.IsFaceup() && c.HasType(CardType.Xyz) && c.Rank == 4 && c.Attack < 2500).ToList<ClientCard>();
				bool hasNode2;
				if (this.GetLevel4FinalCountOnField(true, out hasNode2) + xyzMonsterList.Count<ClientCard>() < 4)
				{
					return false;
				}
				materialList2.AddRange(xyzMonsterList);
				materialList2 = (from c in materialList2
					where c != null && c.Attack < 2500
					orderby c.GetDefensePower()
					select c).Take(2).ToList<ClientCard>();
				if (materialList2.Count<ClientCard>() >= 2)
				{
					if (base.Enemy.MonsterZone.Count((ClientCard c) => c != null && c.GetDefensePower() < 2500) >= 2 && !this.CheckWhetherNegated(true, true, CardType.Monster, false) && !base.DefaultCheckWhetherCardIdIsNegated(45852939))
					{
						if (materialList2.Sum((ClientCard c) => c.Attack) < 5000)
						{
							base.AI.SelectMaterials(materialList2, 0);
							return true;
						}
					}
					if (materialList2.Sum(delegate(ClientCard c)
					{
						if (!this.botSolvedCardIdList.Contains(60394026) || c.HasType(CardType.Xyz))
						{
							return c.Attack;
						}
						return 0;
					}) < 2500)
					{
						if (!base.Duel.MainPhase.SpecialSummonableCards.Any((ClientCard c) => c.IsCode(34909328)))
						{
							base.AI.SelectMaterials(materialList2, 0);
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x000AEE40 File Offset: 0x000AD040
		public bool FinalXyzSummon()
		{
			if (base.Card.Location != CardLocation.Extra)
			{
				return false;
			}
			bool flag;
			int level4Count = this.GetLevel4FinalCountOnField(false, out flag);
			bool result = this.FinalXyzSummonInner();
			Logger.DebugWriteLine("Final Xyz Count: " + level4Count.ToString());
			Logger.DebugWriteLine("Final Xyz Summon: " + result.ToString());
			return result;
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x000AEE9C File Offset: 0x000AD09C
		public bool FinalXyzSummonInner()
		{
			if (this.RyzealDuodriveSpSummonCheck())
			{
				Logger.DebugWriteLine("Final: summon duodriver first");
				return false;
			}
			bool flag;
			if (this.GetLevel4FinalCountOnField(false, out flag) >= 4)
			{
				return false;
			}
			ClientCard no41 = base.Duel.MainPhase.SpecialSummonableCards.FirstOrDefault((ClientCard c) => c.IsCode(90590303));
			if (no41 != null && this.Number41BagooskatheTerriblyTiredTapirSummonCheck())
			{
				if (base.Card != no41)
				{
					return false;
				}
				List<ClientCard> materialList = this.GetLevel4OnField(null);
				if (materialList.Count<ClientCard>() >= 2)
				{
					base.AI.SelectMaterials(materialList, 0);
					return true;
				}
			}
			ClientCard abyss = base.Duel.MainPhase.SpecialSummonableCards.FirstOrDefault((ClientCard c) => c.IsCode(21044178));
			if (abyss != null && this.AbyssDwellerSummonCheck())
			{
				if (base.Card != abyss)
				{
					return false;
				}
				List<ClientCard> materialList2 = this.GetLevel4OnField(null);
				if (materialList2.Count<ClientCard>() >= 2)
				{
					base.AI.SelectMaterials(materialList2, 0);
					return true;
				}
			}
			ClientCard deadnader = base.Duel.MainPhase.SpecialSummonableCards.FirstOrDefault((ClientCard c) => c.IsCode(34909328));
			if (deadnader != null)
			{
				List<ClientCard> materialList3 = this.GetLevel4OnField((ClientCard c) => c.HasSetcode(446));
				if (materialList3.Count<ClientCard>() >= 2)
				{
					if (base.Card != deadnader)
					{
						return false;
					}
					base.AI.SelectMaterials(materialList3, 0);
					return true;
				}
			}
			if (deadnader == null)
			{
				if (base.Duel.MainPhase.SummonableCards.FirstOrDefault((ClientCard c) => c.IsCode(6983839)) != null && this.TornadoDragonSummonCheck())
				{
					List<ClientCard> materialList4 = this.GetLevel4OnField(null);
					if (materialList4.Count<ClientCard>() >= 2)
					{
						base.AI.SelectMaterials(materialList4, 0);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x000AF090 File Offset: 0x000AD290
		public bool DonnerDaggerFurHireSpSummon()
		{
			if (this.CheckShouldNoMoreSpSummon(CardLocation.Extra))
			{
				return false;
			}
			bool haveEnemyTarget = base.Enemy.MonsterZone.Any((ClientCard c) => c != null && !c.IsShouldNotBeMonsterTarget()) && !this.CheckWhetherNegated(true, true, CardType.Monster, false);
			List<ClientCard> illegalList = (from card in base.Bot.GetMonsters()
				where card.IsFaceup() && card.Level != 4 && card.Rank != 4
				select card into c
				orderby c.GetDefensePower()
				select c).ToList<ClientCard>();
			bool flag = base.Bot.HasInHand(34022970) && !this.spSummonedCardIdList.Contains(34022970) && !this.activatedCardIdList.Contains(34022970) && illegalList.Count<ClientCard>() > 0;
			bool flag2;
			if (!this.CheckWhetherNegated(true, true, CardType.Monster, false))
			{
				flag2 = base.Enemy.MonsterZone.Any((ClientCard c) => c != null && !c.IsShouldNotBeMonsterTarget() && c.IsFloodgate() && !c.IsDisabled());
			}
			else
			{
				flag2 = false;
			}
			bool needDestory = flag2;
			if (flag || needDestory)
			{
				if (illegalList.Count<ClientCard>() == 1 && haveEnemyTarget)
				{
					List<ClientCard> otherMaterialList = (from card in base.Bot.GetMonsters()
						where card.IsFaceup() && !illegalList.Contains(card) && (card.Owner == 1 || !card.HasType(CardType.Xyz))
						select card).ToList<ClientCard>();
					otherMaterialList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					illegalList.AddRange(otherMaterialList);
				}
				if (illegalList.Count<ClientCard>() > 1)
				{
					List<ClientCard> materialList = illegalList.Take(2).ToList<ClientCard>();
					if (base.Util.GetBotAvailZonesFromExtraDeck(materialList) > 0)
					{
						base.AI.SelectMaterials(materialList, 0);
						return true;
					}
				}
			}
			if (base.Duel.Phase == DuelPhase.Main2)
			{
				List<ClientCard> enemyOwnerMonsters = (from c in base.Bot.MonsterZone
					where c != null && c.IsFaceup() && c.Owner == 1
					orderby c.GetDefensePower()
					select c).ToList<ClientCard>();
				if (enemyOwnerMonsters.Count<ClientCard>() > 0 && haveEnemyTarget)
				{
					if (enemyOwnerMonsters.Count<ClientCard>() == 1)
					{
						List<ClientCard> otherMaterialList2 = (from card in base.Bot.GetMonsters()
							where card.IsFaceup() && !enemyOwnerMonsters.Contains(card) && (!card.HasType(CardType.Xyz) || card.Overlays.Count<int>() == 0)
							select card into c
							orderby c.GetDefensePower()
							select c).ToList<ClientCard>();
						enemyOwnerMonsters.AddRange(otherMaterialList2);
					}
					if (enemyOwnerMonsters.Count<ClientCard>() > 1)
					{
						List<ClientCard> materialList2 = enemyOwnerMonsters.Take(2).ToList<ClientCard>();
						if (base.Util.GetBotAvailZonesFromExtraDeck(materialList2) > 0)
						{
							base.AI.SelectMaterials(materialList2, 0);
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x000AF3AC File Offset: 0x000AD5AC
		public bool MereologicAggregatorActivateFirst()
		{
			List<Func<ClientCard, bool>> list = new List<Func<ClientCard, bool>>();
			list.Add((ClientCard c) => c.IsCode(new int[] { 44665365, 48546368, 54178659 }) && c.IsMonster());
			list.Add((ClientCard c) => c.IsCode(4280258) && c.Attack >= 800);
			list.Add((ClientCard c) => c.IsCode(47297616) && c.Attack >= 500 && c.Defense >= 500);
			list.Add((ClientCard c) => c.IsCode(19652159) && c.Attack >= 1000 && c.Defense >= 1000);
			list.Add(delegate(ClientCard c)
			{
				if (c.IsCode(79600447))
				{
					return base.Enemy.MonsterZone.Any((ClientCard m) => m != null && m.IsFaceup() && m.IsCode(23288411) && m.Attack >= 1000);
				}
				return false;
			});
			List<Func<ClientCard, bool>> multiNegateFuncList = list;
			List<ClientCard> list2 = new List<ClientCard>(base.Enemy.GetMonsters());
			list2.AddRange(base.Enemy.GetSpells());
			foreach (ClientCard card in list2)
			{
				if (card != null && !card.IsFacedown() && !card.IsDisabled())
				{
					using (List<Func<ClientCard, bool>>.Enumerator enumerator2 = multiNegateFuncList.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							if (enumerator2.Current(card))
							{
								base.AI.SelectCard(card);
								this.currentNegateCardList.Add(card);
								this.activatedCardIdList.Add(base.Card.Id + 2);
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x000AF550 File Offset: 0x000AD750
		public bool MereologicAggregatorActivateLater()
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0, false))
			{
				return false;
			}
			ClientCard lastChainCard = base.Util.GetLastChainCard();
			if (lastChainCard != null && lastChainCard.Controller == 0 && lastChainCard.IsCode(34022970))
			{
				ClientCard no41 = base.Enemy.GetMonsters().FirstOrDefault((ClientCard c) => c.IsFaceup() && !c.IsDisabled() && c.IsCode(90590303) && c.HasPosition(CardPosition.FaceUpDefence) && !this.currentNegateCardList.Contains(c));
				if (no41 != null)
				{
					this.currentNegateCardList.Add(no41);
					base.AI.SelectCard(no41);
					this.activatedCardIdList.Add(base.Card.Id + 2);
					return true;
				}
			}
			List<ClientCard> targetList = (from c in this.GetNormalEnemyTargetList(true, false, CardType.Monster, true)
				where c.IsFaceup() && !c.IsDisabled()
				select c).ToList<ClientCard>();
			if (targetList.Count<ClientCard>() > 0)
			{
				this.currentNegateCardList.Add(targetList[0]);
				base.AI.SelectCard(targetList);
				this.activatedCardIdList.Add(base.Card.Id + 2);
				return true;
			}
			if (lastChainCard != null && lastChainCard.Controller == 0 && lastChainCard.IsCode(34022970))
			{
				foreach (ClientCard card in base.Bot.GetMonsters())
				{
					if (!card.IsFacedown() && !base.Duel.CurrentChain.Contains(card) && !card.IsDisabled() && card.HasType(CardType.Effect) && (card.IsCode(new int[] { 8633261, 35844557 }) | (card.IsCode(72238166) && this.activatedCardIdList.Contains(72238166)) | (card.HasType(CardType.Xyz) && !card.HasXyzMaterial() && !card.IsCode(new int[] { 34909328, 7511613, 1269512 }))))
					{
						base.AI.SelectCard(card);
						this.activatedCardIdList.Add(base.Card.Id + 2);
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001C2D RID: 7213 RVA: 0x000AF7A4 File Offset: 0x000AD9A4
		public bool RyzealDeadnaderActivate()
		{
			if (base.ActivateDescription == 96)
			{
				Logger.DebugWriteLine("** deadnader replace destroy");
				if (this.deadnaderDestroySelf != base.Card)
				{
					this.activatedCardIdList.Add(34909330);
					return true;
				}
				return false;
			}
			else
			{
				if (this.CheckWhetherNegated(true, false, (CardType)0, false))
				{
					return false;
				}
				if (base.ActivateDescription == base.Util.GetStringId(34909328, 1))
				{
					bool shouldDestroySelf = false;
					bool willBeNegated = false;
					ClientCard lastChainCard = base.Util.GetLastChainCard();
					if (lastChainCard != null && lastChainCard.Controller == 1 && lastChainCard.IsCode(this.targetNegateIdList))
					{
						shouldDestroySelf = true;
						willBeNegated = true;
					}
					shouldDestroySelf |= base.Duel.CurrentChain.Any((ClientCard c) => c != null && c.Controller == 1 && !c.IsDisabled() && !base.DefaultCheckWhetherCardIdIsNegated(c.Id) && c.IsCode(new int[] { 15693423, 35480699 }));
					shouldDestroySelf |= base.Card.Overlays.Count<int>() == 1 && !this.activatedCardIdList.Contains(34909328) && this.GetProblematicEnemyCardList(true, false, CardType.Monster).Count<ClientCard>() == 0;
					if (shouldDestroySelf)
					{
						bool canRebornSelf = base.Bot.SpellZone.Count((ClientCard c) => c != null && c.IsFacedown() && c.IsCode(60394026) && !base.Duel.ChainTargets.Contains(c)) > 0;
						bool canActivateTwin = !this.activatedCardIdList.Contains(45852940) && !base.DefaultCheckWhetherCardIdIsNegated(45852939) && !this.CheckWhetherWillbeRemoved();
						canRebornSelf |= canActivateTwin && base.Card.Overlays.Contains(45852939);
						if (base.Duel.CurrentChain.Any((ClientCard c) => c != null && c.Controller == 1 && !c.IsDisabled() && !base.DefaultCheckWhetherCardIdIsNegated(c.Id) && c.IsCode(15693423)))
						{
							canRebornSelf |= base.Bot.MonsterZone.Any((ClientCard c) => c != null && c.HasType(CardType.Xyz) && c.Overlays.Contains(45852939));
						}
						if (canRebornSelf)
						{
							this.deadnaderDestroySelf = base.Card;
							return true;
						}
					}
					return this.CanDestroyList(willBeNegated).Count<ClientCard>() > 0;
				}
				this.activatedCardIdList.Add(base.Card.Id);
				return true;
			}
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x000AF998 File Offset: 0x000ADB98
		public bool RyzealDuodriveActivate()
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0, false))
			{
				return false;
			}
			if (base.ActivateDescription != base.Util.GetStringId(7511613, 1))
			{
				this.activatedCardIdList.Add(base.Card.Id);
				return true;
			}
			int overlayCount = 0;
			foreach (ClientCard card in base.Bot.MonsterZone)
			{
				if (card != null && card.Overlays.Count<int>() != 0 && (!card.IsCode(66011101) || card.IsDisabled() || this.activatedCardIdList.Contains(66011101)))
				{
					overlayCount += card.Overlays.Count<int>();
				}
			}
			if (overlayCount >= 2)
			{
				this.activatedCardIdList.Add(base.Card.Id + 1);
				return true;
			}
			return false;
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x000AFA6C File Offset: 0x000ADC6C
		public bool TwinsOfTheEclipseActivate()
		{
			if (base.Card.Location == CardLocation.MonsterZone)
			{
				if (this.CheckWhetherNegated(true, false, (CardType)0, false))
				{
					return base.Bot.HasInHandOrInSpellZone(60394026);
				}
				this.activatedCardIdList.Add(base.Card.Id);
				return true;
			}
			else
			{
				if (base.Card.Location != CardLocation.Grave)
				{
					return false;
				}
				if (this.CheckWhetherNegated(true, false, (CardType)0, false))
				{
					return false;
				}
				this.activatedCardIdList.Add(base.Card.Id + 1);
				ClientCard rebornTarget = this.TwinsOfTheEclipseRebornTarget(null);
				if (rebornTarget != null)
				{
					ClientCard mereo = base.Bot.Graveyard.FirstOrDefault((ClientCard c) => c.IsCode(9940036));
					if (mereo != null)
					{
						base.AI.SelectCard(new List<ClientCard> { rebornTarget, mereo });
						return true;
					}
					ClientCard nonLightDark = base.Bot.Graveyard.FirstOrDefault((ClientCard c) => c.HasType(CardType.Xyz) && !c.HasAttribute((CardAttribute)48));
					if (nonLightDark != null)
					{
						base.AI.SelectCard(new List<ClientCard> { rebornTarget, nonLightDark });
						return true;
					}
					ClientCard xyzMonster = base.Bot.Graveyard.FirstOrDefault((ClientCard c) => c.HasType(CardType.Xyz));
					if (xyzMonster != null)
					{
						base.AI.SelectCard(new List<ClientCard> { rebornTarget, xyzMonster });
						return true;
					}
				}
				Logger.DebugWriteLine("** Twins of The Eclipse: although cannot find target, still should activate.");
				return true;
			}
		}

		// Token: 0x06001C30 RID: 7216 RVA: 0x000AFC08 File Offset: 0x000ADE08
		public ClientCard TwinsOfTheEclipseRebornTarget(List<ClientCard> targetList)
		{
			if (targetList == null)
			{
				targetList = base.Bot.Graveyard.Where((ClientCard c) => c.HasType(CardType.Xyz) && c.IsCanRevive()).ToList<ClientCard>();
			}
			ClientCard duoDriver = targetList.FirstOrDefault((ClientCard c) => c.IsCode(7511613));
			ClientCard deadnader = targetList.FirstOrDefault((ClientCard c) => c.IsCode(34909328));
			ClientCard no41 = targetList.FirstOrDefault((ClientCard c) => c.IsCode(90590303));
			ClientCard abyssDweller = targetList.FirstOrDefault((ClientCard c) => c.IsCode(21044178));
			if (no41 != null && !base.DefaultCheckWhetherCardIdIsNegated(90590303) && (deadnader == null || this.activatedCardIdList.Contains(34909328)) && (base.Duel.Turn != 1 || duoDriver == null))
			{
				return no41;
			}
			if (abyssDweller != null && !base.DefaultCheckWhetherCardIdIsNegated(21044178) && !this.botSolvedCardIdList.Contains(21044178) && this.AbyssDwellerSummonCheck())
			{
				return abyssDweller;
			}
			if (deadnader != null)
			{
				return deadnader;
			}
			if (duoDriver != null && (!this.activatedCardIdList.Contains(7511614) || base.Bot.HasInHandOrInSpellZone(6798031)))
			{
				return duoDriver;
			}
			if (targetList.Count<ClientCard>() > 0)
			{
				return this.ShuffleList<ClientCard>(targetList)[0];
			}
			return null;
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x000AFD90 File Offset: 0x000ADF90
		public List<ClientCard> CanDestroyList(bool ignoreCurrentDestroy = false)
		{
			List<ClientCard> destroyTargetList = this.GetNormalEnemyTargetList(true, ignoreCurrentDestroy, CardType.Monster, false).Except(this.currentNegateCardList).ToList<ClientCard>();
			List<int> cannotDestroyList = new List<int>(this.NotToDestroySpellTrap);
			destroyTargetList.RemoveAll((ClientCard c) => c.IsCode(cannotDestroyList));
			List<int> undestoryableCardIdlist = new List<int>
			{
				94977269, 58604027, 8062132, 10817524, 53315891, 10000090, 86221741, 71222868, 83257450, 97489701,
				97165977, 24550676, 55410871, 72664875, 85908279, 13331639, 20654247, 43228023, 99585850, 92770064,
				10497636, 77313225
			};
			destroyTargetList.RemoveAll((ClientCard c) => !c.IsDisabled() && c.IsCode(undestoryableCardIdlist));
			destroyTargetList.RemoveAll((ClientCard c) => !c.IsDisabled() && c.HasSetcode(208));
			if (!base.Enemy.GetSpells().Any((ClientCard c) => c.IsFacedown()))
			{
				if (!base.Enemy.GetMonsters().Any((ClientCard c) => c.IsFacedown()))
				{
					goto IL_0203;
				}
			}
			destroyTargetList.RemoveAll((ClientCard c) => c.IsCode(81497285));
			IL_0203:
			destroyTargetList.RemoveAll((ClientCard c) => !c.IsDisabled() && c.HasSetcode(208));
			return destroyTargetList;
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x000AFFC8 File Offset: 0x000AE1C8
		public bool TornadoDragonActivate()
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0, false))
			{
				return false;
			}
			List<ClientCard> spells = base.Enemy.GetSpells();
			if (spells.Count == 0)
			{
				return false;
			}
			ClientCard selected = base.Enemy.SpellZone.GetFloodgate(false);
			if (selected == null && base.Duel.Player == 1)
			{
				List<ClientCard> targetList = spells.Where((ClientCard c) => c.IsFaceup() && !this.NotToDestroySpellTrap.Contains(c.Id) && !this.currentDestroyCardList.Contains(c) && c.HasType((CardType)17694720)).ToList<ClientCard>();
				if (targetList.Count<ClientCard>() > 0)
				{
					selected = this.ShuffleList<ClientCard>(targetList)[0];
				}
			}
			if (selected != null)
			{
				this.currentDestroyCardList.Add(selected);
				base.AI.SelectCard(selected);
				return true;
			}
			if (selected == null)
			{
				List<ClientCard> setThisTurnList = base.Enemy.SpellZone.Where((ClientCard c) => c != null && c.IsFacedown() && !this.currentDestroyCardList.Contains(c) && this.enemyPlaceThisTurn.Contains(c)).ToList<ClientCard>();
				if (setThisTurnList.Count<ClientCard>() > 0)
				{
					selected = this.ShuffleList<ClientCard>(setThisTurnList)[0];
				}
			}
			if (selected == null)
			{
				List<ClientCard> setThisTurnList2 = base.Enemy.SpellZone.Where((ClientCard c) => c != null && c.IsFacedown() && !this.currentDestroyCardList.Contains(c)).ToList<ClientCard>();
				if (setThisTurnList2.Count<ClientCard>() > 0)
				{
					selected = this.ShuffleList<ClientCard>(setThisTurnList2)[0];
				}
			}
			if ((base.Duel.Player == 0) | (base.Duel.Player == 1 && base.Duel.Phase == DuelPhase.End) | base.DefaultOnBecomeTarget())
			{
				this.currentDestroyCardList.Add(selected);
				base.AI.SelectCard(selected);
				return true;
			}
			return false;
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x000B0139 File Offset: 0x000AE339
		public bool EvilswarmExcitonKnightActivate()
		{
			return !this.CheckWhetherNegated(true, false, (CardType)0, false) && base.DefaultEvilswarmExcitonKnightEffect();
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x000B0150 File Offset: 0x000AE350
		public bool AbyssDwellerActivate()
		{
			if (this.botSolvedCardIdList.Contains(21044178))
			{
				return false;
			}
			if (base.Duel.Player == 0 && base.Bot.HasInHandOrInSpellZone(60394026))
			{
				using (List<int>.Enumerator enumerator = new List<int> { 72238166, 35844557, 34022970 }.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						int checkId = enumerator.Current;
						if (base.Card.Overlays.Contains(checkId) && !base.Bot.HasInHand(checkId) && !this.activatedCardIdList.Contains(checkId))
						{
							return true;
						}
						return false;
					}
				}
			}
			return base.Duel.Player == 1 && !this.CheckWhetherNegated(true, false, (CardType)0, false) && (this.enemyDeckTypeRecord.Contains(119) || base.Enemy.GetMonsterCount() + base.Enemy.GetSpellCount() > 0);
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x000B0278 File Offset: 0x000AE478
		public bool Number60DugaresTheTimelessActivate()
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0, false))
			{
				return false;
			}
			if (this.Number60DugaresTheTimelessDrawEffect() || this.Number60DugaresTheTimelessDoubleTarget() != null || this.Number60DugaresTheTimelessRebornEffect())
			{
				this.activatedCardIdList.Add(base.Card.Id);
				return true;
			}
			return false;
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x000B02C4 File Offset: 0x000AE4C4
		public bool Number60DugaresTheTimelessDrawEffect()
		{
			if (this.lockBirdSolved || base.Bot.Deck.Count < 2)
			{
				return false;
			}
			this.activatedCardIdList.Add(base.Card.Id);
			return true;
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x000B02FC File Offset: 0x000AE4FC
		public ClientCard Number60DugaresTheTimelessDoubleTarget()
		{
			if (base.Util.IsTurn1OrMain2())
			{
				return null;
			}
			ClientCard maxAttackMonster = (from c in base.Bot.MonsterZone
				where c != null && (c.HasPosition(CardPosition.FaceUpAttack) || !this.summonThisTurn.Contains(c))
				orderby c.Attack descending
				select c).FirstOrDefault<ClientCard>();
			if (maxAttackMonster != null)
			{
				int maxBotAttack = maxAttackMonster.Attack;
				ClientCard bestEnemyMonster = (from c in base.Enemy.MonsterZone
					where c != null && c.IsFaceup() && (c.IsDisabled() || !c.IsMonsterInvincible())
					orderby c.GetDefensePower() descending
					select c).FirstOrDefault<ClientCard>();
				if (bestEnemyMonster != null)
				{
					int maxEnemyPower = bestEnemyMonster.GetDefensePower();
					if (bestEnemyMonster.IsAttack())
					{
						maxEnemyPower--;
					}
					if (maxBotAttack < maxEnemyPower && maxBotAttack * 2 > maxEnemyPower)
					{
						return maxAttackMonster;
					}
				}
				if (!this.botSolvedCardIdList.Contains(46772449))
				{
					int currentAttack = this.GetBotCurrentTotalAttack(null);
					if (currentAttack < base.Enemy.LifePoints && currentAttack + maxBotAttack >= base.Enemy.LifePoints)
					{
						return maxAttackMonster;
					}
				}
			}
			return null;
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool Number60DugaresTheTimelessRebornEffect()
		{
			return false;
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x000B0424 File Offset: 0x000AE624
		public bool DonnerDaggerFurHireActivate()
		{
			if (this.CheckAtAdvantage() && !base.Bot.HasInHand(34022970))
			{
				return false;
			}
			ClientCard targetCard = this.GetProblematicEnemyMonster(0, true, false, CardType.Monster);
			if (targetCard == null)
			{
				List<ClientCard> enemyMonsters = base.Enemy.GetMonsters();
				if (enemyMonsters.Count<ClientCard>() > 0)
				{
					enemyMonsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
					enemyMonsters.Reverse();
					targetCard = enemyMonsters[0];
				}
			}
			if (targetCard != null)
			{
				base.AI.SelectCard(base.Card);
				base.AI.SelectNextCard(targetCard);
				this.currentDestroyCardList.Add(targetCard);
				return true;
			}
			return false;
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x000B04C0 File Offset: 0x000AE6C0
		public bool Level4Summon()
		{
			if (this.CheckShouldNoMoreSpSummon((CardLocation)66))
			{
				return false;
			}
			ClientCard leastAttackLevel4 = (from c in base.Bot.Hand
				where c.Level == 4
				orderby c.Attack
				select c).FirstOrDefault<ClientCard>();
			if (leastAttackLevel4 == null || base.Card != leastAttackLevel4)
			{
				return false;
			}
			if (this.GetLevel4CountOnField() != 1)
			{
				return false;
			}
			ClientCard target = (from c in base.Duel.MainPhase.SummonableCards
				where c != null && c.Level == 4
				orderby c.Attack
				select c).FirstOrDefault<ClientCard>();
			if (base.Card != target)
			{
				return false;
			}
			this.summonCount--;
			return true;
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x000B05C4 File Offset: 0x000AE7C4
		public bool SpellSetCheck()
		{
			if (base.Duel.Phase == DuelPhase.Main1 && base.Bot.HasAttackingMonster() && base.Duel.Turn > 1)
			{
				return false;
			}
			if (base.Card.IsTrap() || base.Card.HasType(CardType.QuickPlay))
			{
				if (base.Card.IsCode(10045474) && base.Bot.GetMonsterCount() == 0 && base.Bot.GetSpellCount() == 0)
				{
					if (!base.Bot.Hand.Any((ClientCard c) => !c.IsCode(10045474) && (c.IsTrap() || c.HasType(CardType.QuickPlay))) && base.Bot.Hand.Count<ClientCard>() <= 6)
					{
						return false;
					}
				}
				if (base.Card.IsCode(60394026))
				{
					if (!(base.Bot.Graveyard.Any((ClientCard c) => c != null && c.IsFaceup() && c.HasSetcode(446) && (c.Level == 4 || c.IsCanRevive())) | base.Bot.Banished.Any((ClientCard c) => c != null && c.IsFaceup() && c.HasSetcode(446) && (c.Level == 4 || c.IsCanRevive()))))
					{
						return false;
					}
				}
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

		// Token: 0x06001C3C RID: 7228 RVA: 0x000B0814 File Offset: 0x000AEA14
		public bool ChangePositionFirst()
		{
			if (base.Card.IsFacedown() && base.Card.Level == 4)
			{
				return true;
			}
			if (base.Enemy.MonsterZone.Any((ClientCard c) => c != null && c.HasPosition(CardPosition.FaceUpDefence) && !c.IsDisabled() && c.IsCode(90590303)))
			{
				return false;
			}
			if (!base.Card.IsCode(90590303))
			{
				return false;
			}
			bool haveDangerMonster = base.Enemy.MonsterZone.Any((ClientCard c) => c != null && c.IsFloodgate() && !c.IsDisabled());
			if (base.Card.IsDefense())
			{
				return !haveDangerMonster && !base.Util.IsTurn1OrMain2();
			}
			return haveDangerMonster || base.Util.IsTurn1OrMain2();
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x000B08E6 File Offset: 0x000AEAE6
		protected override bool DefaultSetForDiabellze()
		{
			if (base.DefaultSetForDiabellze())
			{
				this.SelectSTPlace(base.Card, true, null);
				return true;
			}
			return false;
		}

		// Token: 0x04001F1F RID: 7967
		private const int attrbuteLightDark = 48;

		// Token: 0x04001F20 RID: 7968
		private const int SetcodeTimeLord = 74;

		// Token: 0x04001F21 RID: 7969
		private const int SetcodeAtlantean = 119;

		// Token: 0x04001F22 RID: 7970
		private const int SetcodeInfernoid = 187;

		// Token: 0x04001F23 RID: 7971
		private const int SetcodeMajespecter = 208;

		// Token: 0x04001F24 RID: 7972
		private const int SetcodePhantomKnight = 4315;

		// Token: 0x04001F25 RID: 7973
		private const int SetcodeSkyStriker = 277;

		// Token: 0x04001F26 RID: 7974
		private const int SetcodeOrcust = 283;

		// Token: 0x04001F27 RID: 7975
		private const int SetcodeSangen = 425;

		// Token: 0x04001F28 RID: 7976
		private const int SetcodeTenpaiDragon = 426;

		// Token: 0x04001F29 RID: 7977
		private const int SetcodeBranded = 349;

		// Token: 0x04001F2A RID: 7978
		private const int SetcodeFloowandereeze = 365;

		// Token: 0x04001F2B RID: 7979
		private const int SetcodeLabrynth = 382;

		// Token: 0x04001F2C RID: 7980
		private const int SetcodeTearlaments = 385;

		// Token: 0x04001F2D RID: 7981
		private const int SetcodeHorus = 413;

		// Token: 0x04001F2E RID: 7982
		private const int SetcodeRyzeal = 446;

		// Token: 0x04001F2F RID: 7983
		private const int hintTimingMainEnd = 4;

		// Token: 0x04001F30 RID: 7984
		private List<int> NotToNegateIdList = new List<int> { 58699500, 20343502, 25451383, 19403423 };

		// Token: 0x04001F31 RID: 7985
		private List<int> AlbazFusionList = new List<int> { 1906812, 38811586, 41373230, 44146295, 51409648, 51409648, 87746184 };

		// Token: 0x04001F32 RID: 7986
		private Dictionary<int, List<int>> DeckCountTable = new Dictionary<int, List<int>>
		{
			{
				3,
				new List<int> { 8633261, 35844557, 34022970, 14558127, 97268402, 7477101, 10045474 }
			},
			{
				2,
				new List<int> { 42141493, 59438930, 23434538, 35261759, 24224830 }
			},
			{
				1,
				new List<int> { 72238166, 84192580, 87126721, 94145021, 25311006, 85106525, 60394026, 65681983, 6798031 }
			}
		};

		// Token: 0x04001F33 RID: 7987
		private List<int> NotToDestroySpellTrap = new List<int> { 50005218, 6767771 };

		// Token: 0x04001F34 RID: 7988
		private List<int> targetNegateIdList = new List<int>
		{
			97268402, 10045474, 52038441, 78474168, 9940036, 74003290, 67037924, 9753964, 66192538, 23204029,
			73445448, 35103106, 30286474, 45002991, 5795980, 38511382, 53742162, 30430448
		};

		// Token: 0x04001F35 RID: 7989
		private List<int> NeedIceToSolveIdList = new List<int> { 80978111, 87170768 };

		// Token: 0x04001F36 RID: 7990
		private List<ClientCard> currentCanActivateEffect = new List<ClientCard>();

		// Token: 0x04001F37 RID: 7991
		private int maxSummonCount = 1;

		// Token: 0x04001F38 RID: 7992
		private int summonCount = 1;

		// Token: 0x04001F39 RID: 7993
		private bool enemyActivateMaxxC;

		// Token: 0x04001F3A RID: 7994
		private bool enemyActivatePurulia;

		// Token: 0x04001F3B RID: 7995
		private bool enemyActivateFuwalos;

		// Token: 0x04001F3C RID: 7996
		private bool enemyActivateNyalus;

		// Token: 0x04001F3D RID: 7997
		private bool lockBirdSolved;

		// Token: 0x04001F3E RID: 7998
		private int dimensionShifterCount;

		// Token: 0x04001F3F RID: 7999
		private bool botActivateMulcharmy;

		// Token: 0x04001F40 RID: 8000
		private bool botSolvingCross;

		// Token: 0x04001F41 RID: 8001
		private List<int> CheckSetcodeList = new List<int> { 4315, 283, 119, 446, 426, 425, 187, 277, 382, 385 };

		// Token: 0x04001F42 RID: 8002
		private List<int> CheckBotSolvedList = new List<int> { 23434538, 84192580, 42141493, 87126721, 21044178, 46772449, 60394026 };

		// Token: 0x04001F43 RID: 8003
		private bool enemyActivateInfiniteImpermanenceFromHand;

		// Token: 0x04001F44 RID: 8004
		private ClientCard deadnaderDestroySelf;

		// Token: 0x04001F45 RID: 8005
		private List<int> infiniteImpermanenceList = new List<int>();

		// Token: 0x04001F46 RID: 8006
		private List<ClientCard> currentNegateCardList = new List<ClientCard>();

		// Token: 0x04001F47 RID: 8007
		private List<ClientCard> currentDestroyCardList = new List<ClientCard>();

		// Token: 0x04001F48 RID: 8008
		private List<int> activatedCardIdList = new List<int>();

		// Token: 0x04001F49 RID: 8009
		private List<int> spSummonedCardIdList = new List<int>();

		// Token: 0x04001F4A RID: 8010
		private List<int> botSolvedCardIdList = new List<int>();

		// Token: 0x04001F4B RID: 8011
		private List<ClientCard> enemyPlaceThisTurn = new List<ClientCard>();

		// Token: 0x04001F4C RID: 8012
		private List<ClientCard> summonThisTurn = new List<ClientCard>();

		// Token: 0x04001F4D RID: 8013
		private List<ClientCard> hardToDestroyCardList = new List<ClientCard>();

		// Token: 0x04001F4E RID: 8014
		private List<ClientCard> cannotDestroyCardList = new List<ClientCard>();

		// Token: 0x04001F4F RID: 8015
		private HashSet<int> enemyDeckTypeRecord = new HashSet<int>();

		// Token: 0x020003A1 RID: 929
		public class CardId
		{
			// Token: 0x04001F50 RID: 8016
			public const int IceRyzeal = 8633261;

			// Token: 0x04001F51 RID: 8017
			public const int ThodeRyzeal = 35844557;

			// Token: 0x04001F52 RID: 8018
			public const int NodeRyzeal = 72238166;

			// Token: 0x04001F53 RID: 8019
			public const int ExRyzeal = 34022970;

			// Token: 0x04001F54 RID: 8020
			public const int SeventhTachyon = 7477101;

			// Token: 0x04001F55 RID: 8021
			public const int TripleTacticsTalent = 25311006;

			// Token: 0x04001F56 RID: 8022
			public const int Bonfire = 85106525;

			// Token: 0x04001F57 RID: 8023
			public const int RyzealPlugIn = 60394026;

			// Token: 0x04001F58 RID: 8024
			public const int RyzealCross = 6798031;

			// Token: 0x04001F59 RID: 8025
			public const int MereologicAggregator = 9940036;

			// Token: 0x04001F5A RID: 8026
			public const int RyzealDeadnader = 34909328;

			// Token: 0x04001F5B RID: 8027
			public const int Number104Masquerade = 2061963;

			// Token: 0x04001F5C RID: 8028
			public const int RyzealDuodrive = 7511613;

			// Token: 0x04001F5D RID: 8029
			public const int TwinsOfTheEclipse = 45852939;

			// Token: 0x04001F5E RID: 8030
			public const int FullArmoredUtopicRayLancer = 1269512;

			// Token: 0x04001F5F RID: 8031
			public const int TornadoDragon = 6983839;

			// Token: 0x04001F60 RID: 8032
			public const int StarliegePhotonBlastDragon = 16643334;

			// Token: 0x04001F61 RID: 8033
			public const int AbyssDweller = 21044178;

			// Token: 0x04001F62 RID: 8034
			public const int Number60DugaresTheTimeless = 66011101;

			// Token: 0x04001F63 RID: 8035
			public const int DonnerDaggerFurHire = 8728498;
		}
	}
}
