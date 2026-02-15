using System;
using System.Collections.Generic;
using System.Linq;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
	// Token: 0x02000434 RID: 1076
	[Deck("Yubel", "AI_Yubel", "Normal")]
	public class YubelExecutor : DefaultExecutor
	{
		// Token: 0x060022E0 RID: 8928 RVA: 0x000E3694 File Offset: 0x000E1894
		public YubelExecutor(GameAI ai, Duel duel)
			: base(ai, duel)
		{
			base.AddExecutor(ExecutorType.Activate, 93729896, new Func<bool>(this.ActNightmareThroneSearch));
			base.AddExecutor(ExecutorType.Activate, 24224830, new Func<bool>(this.CalledbytheGraveActivate));
			base.AddExecutor(ExecutorType.Activate, 65681983, new Func<bool>(this.CrossoutDesignatorActivate));
			base.AddExecutor(ExecutorType.Activate, 10045474, new Func<bool>(this.InfiniteImpermanenceActivate));
			base.AddExecutor(ExecutorType.Activate, 14558127, new Func<bool>(this.AshBlossomActivate));
			base.AddExecutor(ExecutorType.Activate, 23434538, new Func<bool>(this.MaxxCActivate));
			base.AddExecutor(ExecutorType.Activate, 62318994, new Func<bool>(this.ActSamsaraDLotusGY));
			base.AddExecutor(ExecutorType.Activate, 78371393);
			base.AddExecutor(ExecutorType.Activate, 29479265, new Func<bool>(this.UnchainedAbominationActivate));
			base.AddExecutor(ExecutorType.Activate, 80453041, new Func<bool>(this.DontSelfNG));
			base.AddExecutor(ExecutorType.Activate, 80801743, new Func<bool>(this.ActAbo));
			base.AddExecutor(ExecutorType.Activate, 29301450, new Func<bool>(this.ActLittleKnight));
			base.AddExecutor(ExecutorType.Activate, 79559912, new Func<bool>(this.DontSelfNG));
			base.AddExecutor(ExecutorType.Activate, 82135803, new Func<bool>(this.ActDesirae));
			base.AddExecutor(ExecutorType.Activate, 70636044, new Func<bool>(this.ActVarudras));
			base.AddExecutor(ExecutorType.Activate, 67680512, new Func<bool>(this.ActRageQuickLink));
			base.AddExecutor(ExecutorType.Activate, 99989863, new Func<bool>(this.ActParadise));
			base.AddExecutor(ExecutorType.Activate, 60764609, new Func<bool>(this.ActEngraverHand));
			base.AddExecutor(ExecutorType.Activate, 98567237, new Func<bool>(this.ActTract));
			base.AddExecutor(ExecutorType.SpSummon, 97651498);
			base.AddExecutor(ExecutorType.SpSummon, 2463794, new Func<bool>(this.SSRequiem));
			base.AddExecutor(ExecutorType.Activate, 2463794, new Func<bool>(this.ActRequiemMZ));
			base.AddExecutor(ExecutorType.Activate, 28803166, new Func<bool>(this.ActLacimaCT));
			base.AddExecutor(ExecutorType.Activate, 2463794, new Func<bool>(this.ActRequiemEQ));
			base.AddExecutor(ExecutorType.SpSummon, 93860227, new Func<bool>(this.SSNecroquip));
			base.AddExecutor(ExecutorType.Activate, 60764609, new Func<bool>(this.ActEngraverGY));
			base.AddExecutor(ExecutorType.SpSummon, 79559912);
			base.AddExecutor(ExecutorType.Activate, 28803166, new Func<bool>(this.ActLacimaCTGY));
			base.AddExecutor(ExecutorType.Activate, new Func<bool>(this.S1_ActivateTerraformingForThrone));
			base.AddExecutor(ExecutorType.Activate, 93729896, new Func<bool>(this.S6_ChainThroneFollowUp));
			base.AddExecutor(ExecutorType.Summon, 81034083, new Func<bool>(this.NSDarkBeckoningBeast));
			base.AddExecutor(ExecutorType.Activate, 81034083, new Func<bool>(this.ActDarkBeckoningBeast));
			base.AddExecutor(ExecutorType.Activate, 80312545, new Func<bool>(this.S4_ActivateSpiritGates));
			base.AddExecutor(ExecutorType.Activate, 80312545, new Func<bool>(this.Gate_RecycleContinuous));
			base.AddExecutor(ExecutorType.Summon, 62318994, new Func<bool>(this.NSSamsaraDLotus));
			base.AddExecutor(ExecutorType.Activate, 62318994, new Func<bool>(this.ActSamsaraDLotus));
			base.AddExecutor(ExecutorType.SpSummon, 90829280);
			base.AddExecutor(ExecutorType.Activate, 90829280);
			base.AddExecutor(ExecutorType.Activate, 65261141, new Func<bool>(this.ActNightmarePainHand));
			base.AddExecutor(ExecutorType.Activate, 65261141, new Func<bool>(this.ActNightmarePainEffect));
			base.AddExecutor(ExecutorType.Activate, 24215921, new Func<bool>(this.SSGGS));
			base.AddExecutor(ExecutorType.Activate, 24215921, new Func<bool>(this.ActGGSGY));
			base.AddExecutor(ExecutorType.SpSummon, 71818935, new Func<bool>(this.SSMoon));
			base.AddExecutor(ExecutorType.SpSummon, 24269961, new Func<bool>(this.L2YamaSetup));
			base.AddExecutor(ExecutorType.Activate, 24269961, new Func<bool>(this.ActYamaMZ));
			base.AddExecutor(ExecutorType.Activate, 24269961, new Func<bool>(this.ActYamaGY));
			base.AddExecutor(ExecutorType.SpSummon, 67680512, new Func<bool>(this.L2RageKeepYama));
			base.AddExecutor(ExecutorType.SpSummon, 29479265, new Func<bool>(this.L4ABOSS));
			base.AddExecutor(ExecutorType.Activate, 41165831, new Func<bool>(this.ActSharvara));
			base.AddExecutor(ExecutorType.Activate, 41165831, new Func<bool>(this.ActSharvaraGY));
			base.AddExecutor(ExecutorType.SpSummon, 70636044);
			base.AddExecutor(ExecutorType.SpSummon, 60303245, new Func<bool>(this.AlmirajSummon));
			base.AddExecutor(ExecutorType.Activate, 80312545, new Func<bool>(this.Gate_Revive00Fiend));
			base.AddExecutor(ExecutorType.SpSummon, 80453041, new Func<bool>(this.SSPhantom));
			base.AddExecutor(ExecutorType.SpSummon, 56910167);
			base.AddExecutor(ExecutorType.Activate, 56910167);
			base.AddExecutor(ExecutorType.SpSummon, 26096328);
			base.AddExecutor(ExecutorType.Activate, 26096328);
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(this.SpellSetCheck));
			base.AddExecutor(ExecutorType.Repos, new Func<bool>(this.MonsterRepos));
		}

		// Token: 0x060022E1 RID: 8929 RVA: 0x000E3E64 File Offset: 0x000E2064
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

		// Token: 0x060022E2 RID: 8930 RVA: 0x0000763C File Offset: 0x0000583C
		public override bool OnSelectHand()
		{
			return true;
		}

		// Token: 0x060022E3 RID: 8931 RVA: 0x000E3ED4 File Offset: 0x000E20D4
		public List<ClientCard> ShuffleCardList(List<ClientCard> list)
		{
			int i = list.Count;
			while (i-- > 1)
			{
				int index = Program.Rand.Next(i + 1);
				ClientCard temp = list[index];
				list[index] = list[i];
				list[i] = temp;
			}
			return list;
		}

		// Token: 0x060022E4 RID: 8932 RVA: 0x000E3F24 File Offset: 0x000E2124
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

		// Token: 0x060022E5 RID: 8933 RVA: 0x000E3F60 File Offset: 0x000E2160
		private bool MonsterRepos()
		{
			if (base.Card.Id == 90829280 || base.Card.Id == 78371393 || base.Card.Id == 4779091 || base.Card.Id == 80453041)
			{
				if (base.Card.IsDefense())
				{
					base.AI.SelectPosition(CardPosition.Attack);
					return true;
				}
				return false;
			}
			else
			{
				if (base.Card.IsFacedown())
				{
					return true;
				}
				if (this.CheckInDanger() && this._totalAttack > this._totalBotAttack)
				{
					return base.Card.IsDefense();
				}
				return base.DefaultMonsterRepos();
			}
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x000E4010 File Offset: 0x000E2210
		public bool CheckAtAdvantage()
		{
			if (this.GetProblematicEnemyMonster(0, false, false, (CardType)0) == null)
			{
				if (base.Bot.GetMonsters().Any((ClientCard card) => card.IsFaceup()))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060022E7 RID: 8935 RVA: 0x000E4060 File Offset: 0x000E2260
		public bool CheckInDanger()
		{
			if (base.Duel.Phase > DuelPhase.Main1 && base.Duel.Phase < DuelPhase.Main2)
			{
				int totalAtk = 0;
				foreach (ClientCard i in base.Enemy.GetMonsters())
				{
					if (i.IsAttack() && !i.Attacked)
					{
						totalAtk += i.Attack;
					}
				}
				if (totalAtk >= base.Bot.LifePoints)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x000E4100 File Offset: 0x000E2300
		public override int OnSelectPlace(int cardId, int player, CardLocation location, int available)
		{
			if (player == 0 && location == CardLocation.MonsterZone && cardId == 67680512)
			{
				int prefer = this.GetMyLinkedMMZMask() & available & 31;
				int choose = ((prefer != 0) ? this.LowestBit(prefer) : this.LowestBit(available & 31));
				base.AI.SelectPlace(choose);
				return choose;
			}
			this.SelectSTPlace(base.Card, true, null);
			return base.OnSelectPlace(cardId, player, location, available);
		}

		// Token: 0x060022E9 RID: 8937 RVA: 0x000E4168 File Offset: 0x000E2368
		public override CardPosition OnSelectPosition(int cardId, IList<CardPosition> positions)
		{
			if (positions == null || positions.Count == 0)
			{
				return base.OnSelectPosition(cardId, positions);
			}
			bool flag;
			if (!YubelExecutor.YUBEL_SET.Contains(cardId) && (base.Card == null || !YubelExecutor.YUBEL_SET.Contains(base.Card.Id)))
			{
				if (base.Card != null)
				{
					string name = base.Card.Name;
					flag = name != null && name.Contains("Yubel");
				}
				else
				{
					flag = false;
				}
			}
			else
			{
				flag = true;
			}
			bool isYubelFamily = flag;
			if (!isYubelFamily)
			{
				return base.OnSelectPosition(cardId, positions);
			}
			CardPosition atkPref = (positions.Contains(CardPosition.FaceUpAttack) ? CardPosition.FaceUpAttack : (positions.Contains(CardPosition.Attack) ? CardPosition.Attack : ((CardPosition)0)));
			if (isYubelFamily && atkPref != (CardPosition)0)
			{
				base.AI.SelectPosition(atkPref);
				return atkPref;
			}
			CardPosition chosen = positions[0];
			base.AI.SelectPosition(chosen);
			return chosen;
		}

		// Token: 0x060022EA RID: 8938 RVA: 0x000E4230 File Offset: 0x000E2430
		public bool AshBlossomActivate()
		{
			return !this.BlockIfThrone("AshBlossom") && !this.InThroneFlow && !this.CheckWhetherNegated(true, false, (CardType)0) && this.CheckLastChainShouldNegated() && (base.Duel.LastChainPlayer != 1 || !base.Util.GetLastChainCard().IsCode(23434538) || !this.CheckAtAdvantage()) && base.DefaultAshBlossomAndJoyousSpring();
		}

		// Token: 0x060022EB RID: 8939 RVA: 0x000E42A0 File Offset: 0x000E24A0
		public bool MaxxCActivate()
		{
			return !this.BlockIfThrone("MaxxC") && !this.InThroneFlow && !this.CheckWhetherNegated(true, false, (CardType)0) && base.Duel.LastChainPlayer != 0 && base.DefaultMaxxC();
		}

		// Token: 0x060022EC RID: 8940 RVA: 0x000E42DC File Offset: 0x000E24DC
		public bool InfiniteImpermanenceActivate()
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0))
			{
				return false;
			}
			foreach (ClientCard i in base.Enemy.GetMonsters())
			{
				if (i.IsMonsterShouldBeDisabledBeforeItUseEffect() && !i.IsDisabled() && base.Duel.LastChainPlayer != 0)
				{
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
					base.AI.SelectCard(i);
					return true;
				}
			}
			ClientCard LastChainCard = base.Util.GetLastChainCard();
			if (base.Card.Location == CardLocation.SpellZone)
			{
				int this_seq = -1;
				int that_seq = -1;
				for (int k = 0; k < 5; k++)
				{
					if (base.Bot.SpellZone[k] == base.Card)
					{
						this_seq = k;
					}
					if (LastChainCard != null && LastChainCard.Controller == 1 && LastChainCard.Location == CardLocation.SpellZone && base.Enemy.SpellZone[k] == LastChainCard)
					{
						that_seq = k;
					}
					else if (base.Duel.Player == 0 && base.Util.GetProblematicEnemySpell() != null && base.Enemy.SpellZone[k] != null && base.Enemy.SpellZone[k].IsFloodgate())
					{
						that_seq = k;
					}
				}
				if ((this_seq * that_seq >= 0 && this_seq + that_seq == 4) || base.Util.IsChainTarget(base.Card) || (LastChainCard != null && LastChainCard.Controller == 1 && LastChainCard.IsCode(18144506)))
				{
					ClientCard target = this.GetProblematicEnemyMonster(0, true, false, (CardType)0);
					base.Enemy.GetMonsters();
					base.AI.SelectCard(target);
					this.infiniteImpermanenceList.Add(this_seq);
					return true;
				}
			}
			if (LastChainCard == null || LastChainCard.Controller != 1 || LastChainCard.Location != CardLocation.MonsterZone || LastChainCard.IsDisabled() || LastChainCard.IsShouldNotBeTarget() || LastChainCard.IsShouldNotBeSpellTrapTarget())
			{
				return false;
			}
			if (base.Card.Location == CardLocation.SpellZone)
			{
				for (int l = 0; l < 5; l++)
				{
					if (base.Bot.SpellZone[l] == base.Card)
					{
						this.infiniteImpermanenceList.Add(l);
						break;
					}
				}
			}
			if (base.Card.Location == CardLocation.Hand)
			{
				this.SelectSTPlace(base.Card, true, null);
			}
			if (LastChainCard != null)
			{
				base.AI.SelectCard(LastChainCard);
			}
			else
			{
				List<ClientCard> monsters = base.Enemy.GetMonsters();
				monsters.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				monsters.Reverse();
				foreach (ClientCard card in monsters)
				{
					if (card.IsFaceup() && !card.IsShouldNotBeTarget() && !card.IsShouldNotBeSpellTrapTarget())
					{
						base.AI.SelectCard(card);
						return true;
					}
				}
			}
			return true;
		}

		// Token: 0x060022ED RID: 8941 RVA: 0x000E4628 File Offset: 0x000E2828
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
					this.currentNegateCardList.AddRange(base.Enemy.MonsterZone.Where((ClientCard c) => c != null && c.IsFaceup() && c.IsCode(code)));
					return true;
				}
			}
			return false;
		}

		// Token: 0x060022EE RID: 8942 RVA: 0x000E472C File Offset: 0x000E292C
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

		// Token: 0x060022EF RID: 8943 RVA: 0x000E49FC File Offset: 0x000E2BFC
		public bool SpellSetCheck()
		{
			if (base.Card.Id == 99989863)
			{
				return false;
			}
			if (base.Duel.Phase == DuelPhase.Main1 && base.Bot.HasAttackingMonster() && base.Duel.Turn > 1)
			{
				return false;
			}
			if (new List<int> { 80801743 }.Contains(base.Card.Id) && base.Bot.HasInSpellZone(base.Card.Id, false, false))
			{
				return false;
			}
			if (!base.Card.IsTrap() && !base.Card.HasType(CardType.QuickPlay))
			{
				return false;
			}
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

		// Token: 0x060022F0 RID: 8944 RVA: 0x000E4B60 File Offset: 0x000E2D60
		public List<ClientCard> GetDangerousCardinEnemyGrave(bool onlyMonster = false)
		{
			List<ClientCard> list = base.Enemy.Graveyard.GetMatchingCards((ClientCard card) => (!onlyMonster || card.IsMonster()) && (card.HasSetcode(283) || card.HasSetcode(219) || card.HasSetcode(413))).ToList<ClientCard>();
			List<int> dangerMonsterIdList = new List<int> { 99937011, 63542003, 9411399, 28954097, 30680659 };
			list.AddRange(base.Enemy.Graveyard.GetMatchingCards((ClientCard card) => dangerMonsterIdList.Contains(card.Id)));
			return list;
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x000E4C00 File Offset: 0x000E2E00
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
				if (base.Enemy.HasInSpellZone(82732705, true, false))
				{
					return true;
				}
			}
			return disablecheck && ((base.Card.Location == CardLocation.MonsterZone || base.Card.Location == CardLocation.SpellZone) && base.Card.IsDisabled()) && base.Card.IsFaceup();
		}

		// Token: 0x060022F2 RID: 8946 RVA: 0x00037BCF File Offset: 0x00035DCF
		public bool CheckNumber41(ClientCard card)
		{
			return card != null && card.IsFaceup() && card.IsCode(90590303) && card.IsDefense() && !card.IsDisabled();
		}

		// Token: 0x060022F3 RID: 8947 RVA: 0x000E4D40 File Offset: 0x000E2F40
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

		// Token: 0x060022F4 RID: 8948 RVA: 0x000E4F2C File Offset: 0x000E312C
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

		// Token: 0x060022F5 RID: 8949 RVA: 0x000E5068 File Offset: 0x000E3268
		public bool CheckLastChainShouldNegated()
		{
			ClientCard lastcard = base.Util.GetLastChainCard();
			return lastcard != null && lastcard.Controller == 1 && (!lastcard.IsMonster() || !lastcard.HasSetcode(74) || base.Duel.Phase != DuelPhase.Standby) && !this.notToNegateIdList.Contains(lastcard.Id) && !base.DefaultCheckWhetherCardIsNegated(lastcard) && (base.Duel.Turn != 1 || !lastcard.IsCode(23434538));
		}

		// Token: 0x060022F6 RID: 8950 RVA: 0x000E50F0 File Offset: 0x000E32F0
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

		// Token: 0x060022F7 RID: 8951 RVA: 0x000E531C File Offset: 0x000E351C
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

		// Token: 0x060022F8 RID: 8952 RVA: 0x000E5374 File Offset: 0x000E3574
		public bool CheckShouldNotIgnore(ClientCard cards, bool ignore = false)
		{
			return !ignore || (!this.currentDestroyCardList.Contains(cards) && !this.currentNegateCardList.Contains(cards));
		}

		// Token: 0x060022F9 RID: 8953 RVA: 0x000E539C File Offset: 0x000E359C
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

		// Token: 0x060022FA RID: 8954 RVA: 0x000E5680 File Offset: 0x000E3880
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

		// Token: 0x060022FB RID: 8955 RVA: 0x000E5794 File Offset: 0x000E3994
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

		// Token: 0x060022FC RID: 8956 RVA: 0x000E5880 File Offset: 0x000E3A80
		public ClientCard GetBestEnemyMonster(bool onlyFaceup = false, bool canBeTarget = false)
		{
			ClientCard card = this.GetProblematicEnemyMonster(0, canBeTarget, false, (CardType)0);
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
			if (monsters.Count > 0 && !onlyFaceup)
			{
				return this.ShuffleCardList(monsters)[0];
			}
			return null;
		}

		// Token: 0x060022FD RID: 8957 RVA: 0x000E58DC File Offset: 0x000E3ADC
		public ClientCard GetBestEnemySpell(bool onlyFaceup = false, bool canBeTarget = false)
		{
			List<ClientCard> problemEnemySpellList = base.Enemy.SpellZone.Where((ClientCard c) => ((c != null) ? c.Data : null) != null && c.IsFloodgate() && c.IsFaceup() && (!canBeTarget || !c.IsShouldNotBeTarget())).ToList<ClientCard>();
			if (problemEnemySpellList.Count > 0)
			{
				return this.ShuffleCardList(problemEnemySpellList)[0];
			}
			List<ClientCard> spells = (from card in base.Enemy.GetSpells()
				where !card.IsFaceup() || !card.IsCode(15693423)
				select card).ToList<ClientCard>();
			List<ClientCard> faceUpList = spells.Where((ClientCard ecard) => ecard.IsFaceup() && (ecard.HasType(CardType.Continuous) || ecard.HasType(CardType.Field) || ecard.HasType(CardType.Pendulum))).ToList<ClientCard>();
			if (faceUpList.Count > 0)
			{
				return this.ShuffleCardList(faceUpList)[0];
			}
			if (spells.Count > 0 && !onlyFaceup)
			{
				return this.ShuffleCardList(spells)[0];
			}
			return null;
		}

		// Token: 0x060022FE RID: 8958 RVA: 0x000E59C4 File Offset: 0x000E3BC4
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
			if (!checkGrave || base.Enemy.Graveyard.Count <= 0)
			{
				return null;
			}
			List<ClientCard> graveMonsterList = base.Enemy.Graveyard.GetMatchingCards((ClientCard c) => c.IsMonster()).ToList<ClientCard>();
			if (graveMonsterList.Count > 0)
			{
				graveMonsterList.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
				graveMonsterList.Reverse();
				return graveMonsterList[0];
			}
			return this.ShuffleCardList(base.Enemy.Graveyard.ToList<ClientCard>())[0];
		}

		// Token: 0x060022FF RID: 8959 RVA: 0x000E5A84 File Offset: 0x000E3C84
		public override void OnChainSolved(int chainIndex)
		{
			ClientCard currentCard = base.Duel.GetCurrentSolvingChainCard();
			ClientCard solving = base.Duel.GetCurrentSolvingChainCard();
			bool neg = base.Duel.IsCurrentSolvingChainNegated();
			Logger.DebugWriteLine(string.Format("[CHAIN] Solved idx={0} negated={1} solving={2}", chainIndex, neg, this.CardStr(solving)));
			if (currentCard != null && !base.Duel.IsCurrentSolvingChainNegated() && currentCard.Controller == 1)
			{
				if (currentCard.IsCode(23434538))
				{
					this.enemyActivateMaxxC = true;
				}
				if (currentCard.IsCode(42141493))
				{
					this.enemyActivateMaxxC = true;
				}
				if (currentCard.IsCode(94145021))
				{
					this.enemyActivateLockBird = true;
				}
				if (currentCard.IsCode(10045474))
				{
					for (int i = 0; i < 5; i++)
					{
						if (base.Enemy.SpellZone[i] == currentCard)
						{
							this.infiniteImpermanenceList.Add(4 - i);
							return;
						}
					}
				}
			}
		}

		// Token: 0x06002300 RID: 8960 RVA: 0x000E5B68 File Offset: 0x000E3D68
		public override void OnChainEnd()
		{
			this.escapeTargetList.Clear();
			this.currentNegateCardList.Clear();
			this.currentDestroyCardList.Clear();
			this.enemyActivateInfiniteImpermanenceFromHand = false;
			for (int idx = this.enemyPlaceThisTurn.Count - 1; idx >= 0; idx--)
			{
				ClientCard checkTarget = this.enemyPlaceThisTurn[idx];
				if (checkTarget == null || (checkTarget.Location != CardLocation.SpellZone && checkTarget.Location != CardLocation.MonsterZone))
				{
					this.enemyPlaceThisTurn.RemoveAt(idx);
				}
			}
			if (this.thronePending && this._throneStage == YubelExecutor.ThroneStage.None)
			{
				this.thronePending = false;
			}
			this.ResetThroneFlow();
			Logger.DebugWriteLine("[CHAIN] OnChainEnd");
			base.OnChainEnd();
		}

		// Token: 0x06002301 RID: 8961 RVA: 0x000E5C10 File Offset: 0x000E3E10
		private void ResetThroneFlow()
		{
			Logger.DebugWriteLine(string.Format("[THRONE] Reset flow (was stage={0}, pending={1})", this._throneStage, this.thronePending));
			this.thronePending = false;
			this.throneSearched = false;
			this.throneDesiredPick = 0;
			this._throneStage = YubelExecutor.ThroneStage.None;
		}

		// Token: 0x06002302 RID: 8962 RVA: 0x000E5C60 File Offset: 0x000E3E60
		public override void OnNewTurn()
		{
			if (base.Duel.Turn <= 1)
			{
				this.dimensionShifterCount = 0;
			}
			this.enemyActivateMaxxC = false;
			this.enemyActivateLockBird = false;
			this.enemyActivateInfiniteImpermanenceFromHand = false;
			if (this.dimensionShifterCount > 0)
			{
				this.dimensionShifterCount--;
			}
			this.infiniteImpermanenceList.Clear();
			this.currentNegateCardList.Clear();
			this.currentDestroyCardList.Clear();
			this.sendToGYThisTurn.Clear();
			this.activatedCardIdList.Clear();
			this.enemyPlaceThisTurn.Clear();
			this.summonThisTurn.Clear();
			this.thronePending = false;
			this.throneSearched = false;
			this.throneDesiredPick = 0;
			this._gateReviveTargetId = 0;
			this._gateDiscardPreferredId = 0;
			this._gateWantsRecycle = false;
			this._spQuickMode = false;
			base.OnNewTurn();
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06002303 RID: 8963 RVA: 0x000E5D32 File Offset: 0x000E3F32
		private bool InThroneFlow
		{
			get
			{
				return this.thronePending || this._throneStage > YubelExecutor.ThroneStage.None;
			}
		}

		// Token: 0x06002304 RID: 8964 RVA: 0x000E5D48 File Offset: 0x000E3F48
		private int PriorityIndex(int id)
		{
			int idx = Array.IndexOf<int>(YubelExecutor.LinkFodderPriority, id);
			if (idx < 0)
			{
				return 999;
			}
			return idx;
		}

		// Token: 0x06002305 RID: 8965 RVA: 0x000E5D6C File Offset: 0x000E3F6C
		private bool IsProtectedMaterial(ClientCard c, bool allowUseYubelForLink = false)
		{
			return c == null || YubelExecutor.NEVER_SAC.Contains(c.Id) || (c.EquipCards != null && c.EquipCards.Count > 0) || (c.HasType(CardType.Link) && c.LinkCount >= 2) || c.HasType((CardType)8396864);
		}

		// Token: 0x06002306 RID: 8966 RVA: 0x000E5DD4 File Offset: 0x000E3FD4
		private ClientCard[] GetSafeMaterials(int need)
		{
			bool allowYubel = this.ShouldUseYubelForLink();
			return (from m in base.Bot.GetMonsters()
				where !this.IsProtectedMaterial(m, allowYubel)
				orderby this.PriorityIndex(m.Id), m.Attack
				select m).Take(need).ToArray<ClientCard>();
		}

		// Token: 0x06002307 RID: 8967 RVA: 0x000E5E58 File Offset: 0x000E4058
		private bool ShouldUseYubelForLink()
		{
			if (base.Duel.Player != 0)
			{
				return false;
			}
			List<ClientCard> mons = (from m in base.Bot.GetMonsters()
				where m != null
				select m).ToList<ClientCard>();
			if (mons.Count == 0)
			{
				return false;
			}
			bool flag = mons.Count <= 2;
			bool canMakeBetter;
			if (this.HasInExtra(2463794))
			{
				canMakeBetter = this.HasInExtra(71818935) || this.HasInExtra(67680512) || this.HasInExtra(24269961) || this.HasInExtra(29301450);
			}
			else
			{
				canMakeBetter = this.HasInExtra(67680512) || this.HasInExtra(24269961) || this.HasInExtra(29301450);
			}
			return flag && canMakeBetter;
		}

		// Token: 0x06002308 RID: 8968 RVA: 0x000E5F30 File Offset: 0x000E4130
		private bool HasInExtra(int id)
		{
			return base.Bot.ExtraDeck.Any((ClientCard c) => c != null && c.Id == id);
		}

		// Token: 0x06002309 RID: 8969 RVA: 0x000675A8 File Offset: 0x000657A8
		private bool DontSelfNG()
		{
			return base.Duel.LastChainPlayer != 0;
		}

		// Token: 0x0600230A RID: 8970 RVA: 0x000E5F66 File Offset: 0x000E4166
		private int LowestBit(int mask)
		{
			return mask & -mask;
		}

		// Token: 0x0600230B RID: 8971 RVA: 0x000E5F6C File Offset: 0x000E416C
		private int GetMyLinkedMMZMask()
		{
			int mask = 0;
			foreach (ClientCard i in base.Bot.GetMonsters())
			{
				if (i != null && i.IsFaceup() && i.HasType(CardType.Link))
				{
					mask |= i.GetLinkedZones();
				}
			}
			mask &= 31;
			return mask;
		}

		// Token: 0x0600230C RID: 8972 RVA: 0x000E5FE8 File Offset: 0x000E41E8
		private bool S1_ActivateTerraformingForThrone()
		{
			if (base.Type != ExecutorType.Activate)
			{
				return false;
			}
			if (base.Card.Id != 73628505)
			{
				return false;
			}
			if (base.Bot.HasInHandOrInSpellZone(93729896))
			{
				return false;
			}
			base.AI.SelectCard(93729896);
			return true;
		}

		// Token: 0x0600230D RID: 8973 RVA: 0x000E603C File Offset: 0x000E423C
		private bool ActNightmareThroneSearch()
		{
			if (base.Type != ExecutorType.Activate)
			{
				return false;
			}
			if (base.Card.Location == CardLocation.Hand && base.Bot.HasInSpellZone(93729896, false, false))
			{
				return false;
			}
			int pick = 0;
			if (!base.Bot.HasInHand(62318994) && this.CheckRemainInDeck(62318994) > 0)
			{
				pick = 62318994;
			}
			else if (base.Bot.HasInHand(62318994) && !base.Bot.HasInHand(81034083) && this.CheckRemainInDeck(81034083) > 0)
			{
				pick = 81034083;
			}
			else if (base.Bot.HasInHand(62318994) && base.Bot.HasInHand(81034083) && !base.Bot.HasInHand(27439792) && this.CheckRemainInDeck(27439792) > 0)
			{
				pick = 27439792;
			}
			this.thronePending = true;
			this.throneSearched = false;
			this.throneDesiredPick = pick;
			Logger.DebugWriteLine("[THRONE] Activate search; desiredPick=" + ((pick == 0) ? "(auto)" : pick.ToString()));
			this.DumpChain("ThroneActivate");
			return true;
		}

		// Token: 0x0600230E RID: 8974 RVA: 0x000E616A File Offset: 0x000E436A
		private bool NSDarkBeckoningBeast()
		{
			return base.Duel.Phase == DuelPhase.Main1 && (!base.Bot.HasInMonstersZone(81034083, false, false, false) || !base.Bot.HasInHand(62318994));
		}

		// Token: 0x0600230F RID: 8975 RVA: 0x000E61A8 File Offset: 0x000E43A8
		private bool ActDarkBeckoningBeast()
		{
			if (base.Duel.Phase != DuelPhase.Main1)
			{
				return false;
			}
			if (this.CheckRemainInDeck(80312545) > 0 && !base.Bot.HasInSpellZone(80312545, false, false))
			{
				base.AI.SelectCard(80312545);
				return true;
			}
			if (this.CheckRemainInDeck(27439792) > 0)
			{
				base.AI.SelectCard(27439792);
				return true;
			}
			return false;
		}

		// Token: 0x06002310 RID: 8976 RVA: 0x000E621C File Offset: 0x000E441C
		private bool S4_ActivateSpiritGates()
		{
			if (base.Type != ExecutorType.Activate)
			{
				return false;
			}
			if (base.Card.Location != CardLocation.Hand)
			{
				return false;
			}
			if (base.Bot.HasInSpellZone(80312545, true, true))
			{
				return false;
			}
			int pick = 0;
			if (base.Bot.HasInMonstersZone(81034083, false, false, true) && this.CheckRemainInDeck(27439792) > 0)
			{
				pick = 27439792;
			}
			else if (this.CheckRemainInDeck(81034083) > 0)
			{
				pick = 81034083;
			}
			else if (this.CheckRemainInDeck(27439792) > 0)
			{
				pick = 27439792;
			}
			if (pick == 0)
			{
				return false;
			}
			base.AI.SelectCard(pick);
			return true;
		}

		// Token: 0x06002311 RID: 8977 RVA: 0x000E62C3 File Offset: 0x000E44C3
		private bool Gate_RecycleContinuous()
		{
			if (base.Card.Location != CardLocation.SpellZone)
			{
				return false;
			}
			if (!this.HaveFaceupLevel10())
			{
				return false;
			}
			if (!base.Bot.HasInGraveyard(65261141))
			{
				return false;
			}
			this._gateWantsRecycle = true;
			return true;
		}

		// Token: 0x06002312 RID: 8978 RVA: 0x000E62FB File Offset: 0x000E44FB
		private bool Is00FiendId(int id)
		{
			return id == 78371393 || id == 90829280 || id == 81034083 || id == 27439792 || id == 62318994;
		}

		// Token: 0x06002313 RID: 8979 RVA: 0x000E6328 File Offset: 0x000E4528
		private int PlanSpiritGatesReviveTarget()
		{
			if (base.Bot.HasInMonstersZone(60303245, false, false, false) && base.Bot.HasInGraveyard(81034083))
			{
				return 81034083;
			}
			bool spiritOnBoard = base.Bot.HasInMonstersZone(90829280, true, false, false);
			bool spiritInGY = base.Bot.HasInGraveyard(90829280);
			bool spiritInHand = base.Bot.HasInHand(90829280);
			if (!spiritOnBoard && (spiritInGY || spiritInHand))
			{
				return 90829280;
			}
			if (base.Bot.HasInMonstersZone(60303245, true, false, false) && base.Bot.HasInGraveyard(81034083) && this.HasInExtra(71818935))
			{
				return 81034083;
			}
			if (!base.Bot.HasInMonstersZone(62318994, true, false, false) && base.Bot.HasInGraveyard(62318994))
			{
				return 62318994;
			}
			if (base.Bot.HasInGraveyard(81034083))
			{
				return 81034083;
			}
			if (base.Bot.HasInGraveyard(27439792))
			{
				return 27439792;
			}
			if (base.Bot.HasInGraveyard(78371393))
			{
				return 78371393;
			}
			if (!spiritOnBoard && spiritInHand)
			{
				return 90829280;
			}
			return 0;
		}

		// Token: 0x06002314 RID: 8980 RVA: 0x000E6464 File Offset: 0x000E4664
		private int PickSpiritGatesDiscard(int reviveTargetId)
		{
			if (reviveTargetId == 90829280 && base.Bot.HasInHand(90829280))
			{
				return 90829280;
			}
			if (base.Bot.HasInHand(99989863))
			{
				return 99989863;
			}
			if (base.Bot.HasInHand(27439792))
			{
				return 27439792;
			}
			if (base.Bot.HasInHand(4779091))
			{
				return 4779091;
			}
			List<ClientCard> hand = base.Bot.Hand.ToList<ClientCard>();
			hand.Sort((ClientCard a, ClientCard b) => this.ScoreOwnCardForCost(a).CompareTo(this.ScoreOwnCardForCost(b)));
			if (hand.Count <= 0)
			{
				return 0;
			}
			return hand[0].Id;
		}

		// Token: 0x06002315 RID: 8981 RVA: 0x000E6514 File Offset: 0x000E4714
		private bool Gate_Revive00Fiend()
		{
			if (base.Card.Location != CardLocation.SpellZone)
			{
				return false;
			}
			if (this.requiemSummoned)
			{
				if (!base.Bot.HasInHandOrInGraveyard(90829280))
				{
					return false;
				}
				if (base.Bot.Hand.Count <= 0)
				{
					return false;
				}
				this._gateReviveTargetId = 90829280;
				this._gateDiscardPreferredId = this.PickSpiritGatesDiscard(this._gateReviveTargetId);
				this._gateWantsRecycle = false;
				return true;
			}
			else
			{
				int target = this.PlanSpiritGatesReviveTarget();
				if (target == 0)
				{
					return false;
				}
				if (base.Bot.Hand.Count <= 0)
				{
					return false;
				}
				int discard = this.PickSpiritGatesDiscard(target);
				if (discard == 0)
				{
					return false;
				}
				this._gateReviveTargetId = target;
				this._gateDiscardPreferredId = discard;
				this._gateWantsRecycle = false;
				return true;
			}
		}

		// Token: 0x06002316 RID: 8982 RVA: 0x000E65CC File Offset: 0x000E47CC
		private bool HaveFaceupLevel10()
		{
			return base.Bot.MonsterZone.Any((ClientCard m) => m != null && m.IsFaceup() && m.Level == 10);
		}

		// Token: 0x06002317 RID: 8983 RVA: 0x000E65FD File Offset: 0x000E47FD
		private bool ActNightmarePainHand()
		{
			return !base.Bot.HasInSpellZone(65261141, true, true) && base.Card.Location == CardLocation.Hand;
		}

		// Token: 0x06002318 RID: 8984 RVA: 0x000E6628 File Offset: 0x000E4828
		private bool ActNightmarePainEffect()
		{
			if (base.Card.Location == CardLocation.SpellZone)
			{
				if (this.CheckRemainInDeck(24215921) == 0)
				{
					return false;
				}
				if (base.Bot.HasInMonstersZone(90829280, false, false, false) || base.Bot.HasInHand(90829280))
				{
					base.AI.SelectCard(90829280);
					base.AI.SelectNextCard(24215921);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002319 RID: 8985 RVA: 0x000E669C File Offset: 0x000E489C
		private bool S6_ChainThroneFollowUp()
		{
			if (base.Type != ExecutorType.Activate)
			{
				return false;
			}
			if (this.sendToGYThisTurn.Any((ClientCard c) => c != null && YubelExecutor.YUBEL_SET.Contains(c.Id)) && base.Bot.HasInHand(93729896))
			{
				base.AI.SelectYesNo(true);
				return true;
			}
			return false;
		}

		// Token: 0x0600231A RID: 8986 RVA: 0x000E6701 File Offset: 0x000E4901
		private bool NSSamsaraDLotus()
		{
			return !base.Bot.HasInMonstersZone(90829280, false, false, false);
		}

		// Token: 0x0600231B RID: 8987 RVA: 0x000E671C File Offset: 0x000E491C
		private bool ActSamsaraDLotus()
		{
			if (base.Duel.Player == 0 && base.Card.Location == CardLocation.MonsterZone)
			{
				base.AI.SelectCard(90829280);
				return true;
			}
			return base.Duel.Player == 1 && (base.Bot.HasInMonstersZone(78371393, false, false, false) || base.Bot.HasInMonstersZone(90829280, false, false, false));
		}

		// Token: 0x0600231C RID: 8988 RVA: 0x000E6791 File Offset: 0x000E4991
		private bool ActSamsaraDLotusGY()
		{
			if (base.Card.Location == CardLocation.Grave && base.Bot.HasInMonstersZone(78371393, false, false, false))
			{
				base.AI.SelectOption(1);
				return true;
			}
			return false;
		}

		// Token: 0x0600231D RID: 8989 RVA: 0x000E67C6 File Offset: 0x000E49C6
		private bool ActTract()
		{
			if (base.Card.Location != CardLocation.Hand)
			{
				return false;
			}
			if (this.requiemSummoned)
			{
				return false;
			}
			base.AI.SelectCard(97651498);
			base.AI.SelectNextCard(97651498);
			return true;
		}

		// Token: 0x0600231E RID: 8990 RVA: 0x000E6804 File Offset: 0x000E4A04
		private bool ActParadise()
		{
			if (base.Card.Location != CardLocation.Grave)
			{
				return false;
			}
			if (base.Bot.HasInMonstersZoneOrInGraveyard(82135803) || base.Bot.HasInBanished(82135803))
			{
				return false;
			}
			base.AI.SelectCard(82135803);
			return this.DontSelfNG();
		}

		// Token: 0x0600231F RID: 8991 RVA: 0x000E6860 File Offset: 0x000E4A60
		private bool ActDesirae()
		{
			if (base.Card.Location != CardLocation.Grave)
			{
				return false;
			}
			ClientCard target = this.GetBestEnemyCard(true, true, false);
			if (target == null)
			{
				return false;
			}
			if (base.Bot.HasInGraveyard(2463794))
			{
				base.AI.SelectCard(2463794);
				base.AI.SelectNextCard(target);
				return true;
			}
			base.AI.SelectCard(target);
			return true;
		}

		// Token: 0x06002320 RID: 8992 RVA: 0x000E68CC File Offset: 0x000E4ACC
		private bool ActRequiemMZ()
		{
			if (base.Card.Location != CardLocation.MonsterZone)
			{
				return false;
			}
			if (base.Bot.HasInHand(28803166) || this.CheckRemainInDeck(28803166) > 0)
			{
				base.AI.SelectCard(28803166);
				base.AI.SelectPosition(CardPosition.FaceUpDefence);
				return true;
			}
			if (base.Bot.HasInHand(60764609) || this.CheckRemainInDeck(60764609) > 0)
			{
				base.AI.SelectCard(60764609);
				base.AI.SelectPosition(CardPosition.FaceUpDefence);
				return true;
			}
			return false;
		}

		// Token: 0x06002321 RID: 8993 RVA: 0x000E6968 File Offset: 0x000E4B68
		private bool ActRequiemEQ()
		{
			if (!this.HasInExtra(93860227))
			{
				return false;
			}
			if (base.Card.Location != CardLocation.Grave)
			{
				return false;
			}
			if (base.Bot.HasInMonstersZone(28803166, false, false, false))
			{
				base.AI.SelectCard(28803166);
				return true;
			}
			if (base.Bot.HasInMonstersZone(60764609, false, false, false))
			{
				base.AI.SelectCard(60764609);
				return true;
			}
			return false;
		}

		// Token: 0x06002322 RID: 8994 RVA: 0x000E69E4 File Offset: 0x000E4BE4
		private bool SSNecroquip()
		{
			if (base.Bot.HasInSpellZone(2463794, false, false) && base.Bot.HasInMonstersZone(28803166, false, false, false))
			{
				base.AI.SelectCard(2463794);
				base.AI.SelectNextCard(28803166);
				return true;
			}
			if (base.Bot.HasInSpellZone(2463794, false, false) && base.Bot.HasInMonstersZone(60764609, false, false, false))
			{
				base.AI.SelectCard(2463794);
				base.AI.SelectNextCard(60764609);
				return true;
			}
			return false;
		}

		// Token: 0x06002323 RID: 8995 RVA: 0x000E6A88 File Offset: 0x000E4C88
		private bool ActLacimaCT()
		{
			if (base.Card.Location != CardLocation.MonsterZone)
			{
				return false;
			}
			if (!base.Bot.HasInHandOrInMonstersZoneOrInGraveyard(60764609) && !base.Bot.HasInBanished(60764609))
			{
				base.AI.SelectCard(60764609);
				return true;
			}
			if (!base.Bot.HasInHandOrInMonstersZoneOrInGraveyard(99989863) && !base.Bot.HasInBanished(99989863))
			{
				base.AI.SelectCard(99989863);
				return true;
			}
			return false;
		}

		// Token: 0x06002324 RID: 8996 RVA: 0x000E6B12 File Offset: 0x000E4D12
		private bool ActLacimaCTGY()
		{
			return base.Card.Location == CardLocation.Grave && !base.Bot.HasInBanished(99989863) && !base.Bot.HasInHandOrInGraveyard(99989863) && this.DontSelfNG();
		}

		// Token: 0x06002325 RID: 8997 RVA: 0x000E6B54 File Offset: 0x000E4D54
		private bool ActEngraverHand()
		{
			if (base.Card.Location != CardLocation.Hand)
			{
				return false;
			}
			if (!base.Bot.HasInHandOrInSpellZoneOrInGraveyard(98567237) && !base.Bot.HasInBanished(98567237))
			{
				base.AI.SelectCard(98567237);
				return true;
			}
			return false;
		}

		// Token: 0x06002326 RID: 8998 RVA: 0x000E6BA8 File Offset: 0x000E4DA8
		private bool ActEngraverGY()
		{
			if (base.Card.Location != CardLocation.Grave)
			{
				return false;
			}
			if (base.Bot.HasInGraveyard(97651498))
			{
				base.AI.SelectCard(97651498);
				return true;
			}
			if (base.Bot.HasInGraveyard(71818935))
			{
				base.AI.SelectCard(71818935);
				return true;
			}
			return false;
		}

		// Token: 0x06002327 RID: 8999 RVA: 0x000E6C10 File Offset: 0x000E4E10
		private bool SSMoon()
		{
			if (this.moonSummoned)
			{
				return false;
			}
			if (this.requiemSummoned)
			{
				return false;
			}
			if (!this.HasInExtra(2463794))
			{
				return false;
			}
			ClientCard[] mats = this.GetSafeMaterials(2);
			if (mats.Length < 2)
			{
				return false;
			}
			base.AI.SelectMaterials(mats, 0);
			this.moonSummoned = true;
			return true;
		}

		// Token: 0x06002328 RID: 9000 RVA: 0x000E6C65 File Offset: 0x000E4E65
		private bool ActAbo()
		{
			if (base.Bot.HasInGraveyard(67680512))
			{
				base.AI.SelectCard(67680512);
				return true;
			}
			return false;
		}

		// Token: 0x06002329 RID: 9001 RVA: 0x000E6C8C File Offset: 0x000E4E8C
		private bool AlmirajSummon()
		{
			if (base.Bot.GetMonsterCount() > 1)
			{
				return false;
			}
			ClientCard mat = base.Bot.GetMonsters().First<ClientCard>();
			if (mat.IsCode(new int[] { 81034083 }))
			{
				base.AI.SelectMaterials(mat, 0);
				return true;
			}
			return false;
		}

		// Token: 0x0600232A RID: 9002 RVA: 0x000E6CE0 File Offset: 0x000E4EE0
		private bool SSGGS()
		{
			if (!this.DontSelfNG())
			{
				return false;
			}
			if (this.BlockIfThrone("GGS"))
			{
				return false;
			}
			if (base.Duel.Player == 1)
			{
				return false;
			}
			if (this.InThroneFlow)
			{
				return false;
			}
			if (base.Card.Location != CardLocation.Hand)
			{
				return false;
			}
			if (base.Bot.HasInMonstersZone(90829280, false, false, false))
			{
				base.AI.SelectYesNo(true);
				base.AI.SelectCard(90829280);
				return true;
			}
			base.AI.SelectYesNo(false);
			return true;
		}

		// Token: 0x0600232B RID: 9003 RVA: 0x000E6D70 File Offset: 0x000E4F70
		private bool ActGGSGY()
		{
			if (base.Card.Location != CardLocation.Grave)
			{
				return false;
			}
			if (base.Bot.HasInGraveyard(90829280))
			{
				base.AI.SelectCard(90829280);
				return true;
			}
			return false;
		}

		// Token: 0x0600232C RID: 9004 RVA: 0x000E6DA8 File Offset: 0x000E4FA8
		private bool ActLittleKnight()
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
						nextMonster = this.GetBestEnemyMonster(true, true);
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

		// Token: 0x0600232D RID: 9005 RVA: 0x000E70D4 File Offset: 0x000E52D4
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

		// Token: 0x0600232E RID: 9006 RVA: 0x000E7148 File Offset: 0x000E5348
		private bool ActRageQuickLink()
		{
			if (base.Card.Location != CardLocation.MonsterZone)
			{
				return false;
			}
			if (base.Duel.Player != 1)
			{
				return false;
			}
			if (base.Duel.Phase < DuelPhase.Main1 || base.Duel.Phase > DuelPhase.Main2)
			{
				return false;
			}
			if (!this.HasValidRageLinkCandidate())
			{
				return false;
			}
			ClientCard target = this.GetBestEnemyMonster(true, true);
			if (target == null)
			{
				return false;
			}
			base.AI.SelectCard(target);
			return this.DontSelfNG();
		}

		// Token: 0x0600232F RID: 9007 RVA: 0x000E71C4 File Offset: 0x000E53C4
		private bool SSPhantom()
		{
			List<int> gyMat2Codes = new List<int> { 4779091, 78371393, 81034083, 27439792, 90829280 };
			if (!base.Bot.HasInGraveyard(gyMat2Codes))
			{
				return false;
			}
			if (base.Bot.HasInHandOrInMonstersZoneOrInGraveyard(4779091))
			{
				base.AI.SelectCard(4779091);
			}
			else if (base.Bot.HasInMonstersZoneOrInGraveyard(78371393))
			{
				base.AI.SelectCard(78371393);
			}
			else
			{
				if (!base.Bot.HasInGraveyard(90829280))
				{
					return false;
				}
				base.AI.SelectCard(90829280);
			}
			base.AI.SelectNextCard(gyMat2Codes);
			return true;
		}

		// Token: 0x06002330 RID: 9008 RVA: 0x000E729C File Offset: 0x000E549C
		private bool ActSharvara()
		{
			if (this.BlockIfThrone("Sharvara"))
			{
				return false;
			}
			if (base.Duel.Player == 1)
			{
				return false;
			}
			if (this.InThroneFlow)
			{
				return false;
			}
			if (base.Card.Location != CardLocation.Hand)
			{
				return false;
			}
			if (base.Bot.HasInMonstersZone(90829280, false, false, false))
			{
				base.AI.SelectCard(90829280);
				return true;
			}
			if (base.Bot.HasInMonstersZone(78371393, false, false, false))
			{
				base.AI.SelectCard(78371393);
				return true;
			}
			return false;
		}

		// Token: 0x06002331 RID: 9009 RVA: 0x000E7331 File Offset: 0x000E5531
		private bool ActSharvaraGY()
		{
			return base.Card.Location == CardLocation.Grave;
		}

		// Token: 0x06002332 RID: 9010 RVA: 0x000E7348 File Offset: 0x000E5548
		private bool ActVarudras()
		{
			if (this.CheckWhetherNegated(true, false, (CardType)0))
			{
				return false;
			}
			List<ClientCard> targetList = this.GetNormalEnemyTargetList(true, true, (CardType)0);
			int desc = base.ActivateDescription;
			int d = base.Util.GetStringId(70636044, 1);
			int d2 = base.Util.GetStringId(70636044, 2);
			Logger.DebugWriteLine("[Varudras] desc: " + desc.ToString() + ", timing = " + base.CurrentTiming.ToString());
			ClientCard enemyPick = targetList.FirstOrDefault((ClientCard c) => c != null && c.Controller == 1);
			if (desc == d && base.Duel.LastChainPlayer == 1 && base.Duel.CurrentChain.Count > 0)
			{
				if (!this.CheckLastChainShouldNegated())
				{
					return false;
				}
				this.activatedCardIdList.Add(base.Card.Id);
				return true;
			}
			else
			{
				if (desc != d && desc != d2 && desc != -1)
				{
					return false;
				}
				if (targetList.Count == 0)
				{
					return false;
				}
				if (enemyPick != null)
				{
					targetList.Insert(0, enemyPick);
				}
				else
				{
					ClientCard selfBest = (from c in base.Bot.GetMonsters().Concat(base.Bot.GetSpells())
						where c != null
						select c).OrderBy(new Func<ClientCard, int>(this.ScoreOwnCardForCost)).FirstOrDefault<ClientCard>();
					if (selfBest != null)
					{
						targetList.Insert(0, selfBest);
					}
				}
				if (desc == d && base.Duel.CurrentChain.Count == 0)
				{
					this.activatedCardIdList.Add(base.Card.Id + 1);
				}
				if (desc == d2)
				{
					this.activatedCardIdList.Add(base.Card.Id + 2);
				}
				base.AI.SelectCard(targetList);
				return true;
			}
		}

		// Token: 0x06002333 RID: 9011 RVA: 0x000E7514 File Offset: 0x000E5714
		private bool ShouldVarudrasDetachForPop(ClientCard target)
		{
			return target != null && (target.IsFloodgate() || target.IsMonsterDangerous() || target.IsMonsterInvincible() || this.ScoreEnemyCardForRemoval(target) >= 3000);
		}

		// Token: 0x06002334 RID: 9012 RVA: 0x000E7548 File Offset: 0x000E5748
		private bool ActYamaGY()
		{
			if (base.Card.Location != CardLocation.Grave)
			{
				return false;
			}
			if (base.Bot.HasInGraveyard(90829280))
			{
				return false;
			}
			base.AI.SelectCard(90829280);
			base.AI.SelectYesNo(false);
			return true;
		}

		// Token: 0x06002335 RID: 9013 RVA: 0x000E7597 File Offset: 0x000E5797
		private bool ActYamaMZ()
		{
			if (this.CheckRemainInDeck(41165831) == 0)
			{
				return false;
			}
			base.AI.SelectCard(41165831);
			return true;
		}

		// Token: 0x06002336 RID: 9014 RVA: 0x000E75B9 File Offset: 0x000E57B9
		private bool SSRequiem()
		{
			if (this.CheckRemainInDeck(28803166) == 0 && !base.Bot.HasInHand(28803166))
			{
				return false;
			}
			this.requiemSummoned = true;
			return true;
		}

		// Token: 0x06002337 RID: 9015 RVA: 0x000E75E4 File Offset: 0x000E57E4
		private bool L4ABOSS()
		{
			YubelExecutor.<>c__DisplayClass127_0 CS$<>8__locals1 = new YubelExecutor.<>c__DisplayClass127_0();
			if (!this.HasInExtra(29479265))
			{
				return false;
			}
			List<ClientCard> mons = base.Bot.GetMonsters();
			ClientCard yama = mons.FirstOrDefault((ClientCard m) => m != null && m.Id == 24269961);
			ClientCard rage = mons.FirstOrDefault((ClientCard m) => m != null && m.Id == 67680512);
			ClientCard yubel = mons.FirstOrDefault((ClientCard m) => m != null && m.Id == 78371393);
			ClientCard terror = mons.FirstOrDefault((ClientCard m) => m != null && m.Id == 4779091);
			if (yama != null && rage != null && this.IsInEMZ(yama))
			{
				base.AI.SelectMaterials(new ClientCard[] { yama, rage }, 0);
				return true;
			}
			if (rage != null && yubel != null && terror != null && this.IsInEMZ(rage))
			{
				base.AI.SelectMaterials(new ClientCard[] { rage, yubel, terror }, 0);
				return true;
			}
			YubelExecutor.<>c__DisplayClass127_0 CS$<>8__locals2 = CS$<>8__locals1;
			ClientCard clientCard;
			if ((clientCard = rage) == null && (clientCard = yama) == null)
			{
				clientCard = (from m in mons
					where m.HasType(CardType.Link)
					orderby m.LinkCount descending
					select m).FirstOrDefault<ClientCard>();
			}
			CS$<>8__locals2.firstLink = clientCard;
			if (CS$<>8__locals1.firstLink != null)
			{
				List<ClientCard> list = mons.Where((ClientCard m) => m != CS$<>8__locals1.firstLink && !YubelExecutor.NEVER_SAC.Contains(m.Id)).OrderBy(new Func<ClientCard, int>(this.ScoreOwnCardForCost)).ToList<ClientCard>();
				List<ClientCard> pick = new List<ClientCard> { CS$<>8__locals1.firstLink };
				int need = 4 - this.LinkValue(CS$<>8__locals1.firstLink);
				foreach (ClientCard i in list)
				{
					pick.Add(i);
					need -= this.LinkValue(i);
					if (need <= 0)
					{
						break;
					}
				}
				if (need <= 0)
				{
					base.AI.SelectMaterials(pick.ToArray(), 0);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002338 RID: 9016 RVA: 0x0008D38E File Offset: 0x0008B58E
		private int LinkValue(ClientCard c)
		{
			if (c == null || !c.HasType(CardType.Link))
			{
				return 1;
			}
			return Math.Max(1, c.LinkCount);
		}

		// Token: 0x06002339 RID: 9017 RVA: 0x000E7834 File Offset: 0x000E5A34
		private bool IsInEMZ(ClientCard c)
		{
			ClientCard[] mz = base.Bot.MonsterZone;
			return (mz.Length > 5 && mz[5] == c) || (mz.Length > 6 && mz[6] == c);
		}

		// Token: 0x0600233A RID: 9018 RVA: 0x000E786C File Offset: 0x000E5A6C
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

		// Token: 0x0600233B RID: 9019 RVA: 0x000E799C File Offset: 0x000E5B9C
		private ClientCard[] GetSafeMaterialsExcluding(HashSet<int> excludeIds, int need)
		{
			return (from m in base.Bot.GetMonsters()
				where m != null && (excludeIds == null || !excludeIds.Contains(m.Id)) && !this.IsProtectedMaterial(m, false)
				orderby this.PriorityIndex(m.Id), m.Attack
				select m).Take(need).ToArray<ClientCard>();
		}

		// Token: 0x0600233C RID: 9020 RVA: 0x000E7A19 File Offset: 0x000E5C19
		private bool CanMakeRageWithoutYama()
		{
			return this.GetSafeMaterialsExcluding(new HashSet<int> { 24269961 }, 2).Length >= 2;
		}

		// Token: 0x0600233D RID: 9021 RVA: 0x000E7A3C File Offset: 0x000E5C3C
		private bool L2YamaSetup()
		{
			ClientCard[] mats = this.GetSafeMaterials(2);
			if (mats.Length < 2)
			{
				return false;
			}
			base.AI.SelectMaterials(mats, 0);
			return true;
		}

		// Token: 0x0600233E RID: 9022 RVA: 0x000E7A68 File Offset: 0x000E5C68
		private bool L2RageKeepYama()
		{
			if (!base.Bot.HasInMonstersZone(24269961, true, false, false))
			{
				return false;
			}
			if (!this.CanMakeRageWithoutYama())
			{
				return false;
			}
			ClientCard[] mats = this.GetSafeMaterialsExcluding(new HashSet<int> { 24269961 }, 2);
			if (mats.Length < 2)
			{
				return false;
			}
			base.AI.SelectMaterials(mats, 0);
			return true;
		}

		// Token: 0x0600233F RID: 9023 RVA: 0x000E7AC8 File Offset: 0x000E5CC8
		private bool HasFreeEMZ()
		{
			ClientCard[] mz = base.Bot.MonsterZone;
			bool flag = mz.Length > 5 && mz[5] == null;
			bool slot6Free = mz.Length > 6 && mz[6] == null;
			return flag || slot6Free;
		}

		// Token: 0x06002340 RID: 9024 RVA: 0x000E7B04 File Offset: 0x000E5D04
		private bool HasValidRageLinkCandidate()
		{
			bool flag = this.HasInExtra(29301450);
			bool hasGorgon = this.HasInExtra(12067160);
			hasGorgon = this.HasInExtra(12067160);
			return flag || (hasGorgon && this.HasFreeEMZ());
		}

		// Token: 0x06002341 RID: 9025 RVA: 0x000E7B48 File Offset: 0x000E5D48
		private bool YesNoFor(int desc, int cardId, int idx)
		{
			ChainInfo info = base.Duel.GetCurrentSolvingChainInfo();
			ClientCard card = base.Duel.GetCurrentSolvingChainCard();
			return desc == base.Util.GetStringId(cardId, idx) && ((info != null && info.IsCode(cardId)) || (card != null && card.IsCode(cardId)));
		}

		// Token: 0x06002342 RID: 9026 RVA: 0x000E7B9C File Offset: 0x000E5D9C
		public override bool OnSelectYesNo(int desc)
		{
			Logger.DebugWriteLine(string.Format("[DEBUG] OnSelectYesNo: desc={0}", desc));
			ChainInfo info = base.Duel.GetCurrentSolvingChainInfo();
			ClientCard solving = base.Duel.GetCurrentSolvingChainCard();
			this.DumpChain("OnSelectYesNo");
			Logger.DebugWriteLine(string.Format("[THRONE] OnSelectYesNo desc={0} stage={1} solving={2}", desc, this._throneStage, this.CardStr(solving)));
			if (info != null && info.ActivatePlayer == 1)
			{
				return false;
			}
			if (solving != null && solving.IsCode(93729896))
			{
				this.DebugThroneDescMap(desc);
				if (this._throneStage == YubelExecutor.ThroneStage.None && !this.throneSearched)
				{
					this._throneStage = YubelExecutor.ThroneStage.Searching;
					return true;
				}
				if (this._throneStage == YubelExecutor.ThroneStage.AwaitDestroyPrompt || this.throneSearched)
				{
					this._throneStage = YubelExecutor.ThroneStage.None;
					return false;
				}
				if (this._throneStage == YubelExecutor.ThroneStage.Searching && !this.throneSearched)
				{
					return true;
				}
			}
			if (this.YesNoFor(desc, 70636044, 1))
			{
				ClientCard best = this.GetBestEnemyCard(false, false, false);
				return best != null && this.ShouldVarudrasDetachForPop(best);
			}
			if (solving != null && solving.IsCode(70636044) && base.Duel.CurrentChain.Count > 0)
			{
				ClientCard t = this.GetNormalEnemyTargetList(true, true, (CardType)0).FirstOrDefault((ClientCard c) => c.Controller == 1);
				return t != null && this.ShouldVarudrasDetachForPop(t);
			}
			if (this.YesNoFor(desc, 78371393, 2))
			{
				bool haveLotusOnField = base.Bot.GetMonsters().Any((ClientCard m) => m != null && m.Id == 62318994);
				this._yubelWantsTribute = haveLotusOnField;
				return haveLotusOnField;
			}
			return base.OnSelectYesNo(desc);
		}

		// Token: 0x06002343 RID: 9027 RVA: 0x000E7D4C File Offset: 0x000E5F4C
		public override IList<ClientCard> OnSelectCard(IList<ClientCard> cards, int min, int max, int hint, bool cancelable)
		{
			Logger.DebugWriteLine(string.Format("[DEBUG] OnSelectCard: hint={0} (0x{1:X}), min={2}, max={3}, cancelable={4}, candidates={5}", new object[]
			{
				hint,
				hint,
				min,
				max,
				cancelable,
				(cards != null) ? cards.Count : 0
			}));
			bool isReleasePrompt = (long)hint == 500L || hint.ToString().ToLower().Contains("release");
			ClientCard solving = base.Duel.GetCurrentSolvingChainCard();
			if (cards != null && cards.Count > 0)
			{
				if (this._throneStage == YubelExecutor.ThroneStage.Searching && solving != null && solving.IsCode(93729896) && !this.throneSearched && cards != null && cards.Count > 0)
				{
					this.throneSearched = true;
					this._throneStage = YubelExecutor.ThroneStage.AwaitDestroyPrompt;
					ClientCard chosen = null;
					if (this.throneDesiredPick != 0)
					{
						chosen = cards.FirstOrDefault((ClientCard c) => c != null && c.Id == this.throneDesiredPick);
					}
					if (chosen == null)
					{
						chosen = cards.FirstOrDefault((ClientCard c) => c != null && YubelExecutor.YUBEL_SET.Contains(c.Id)) ?? cards[0];
					}
					Logger.DebugWriteLine("[THRONE] Search pick => " + this.CardStr(chosen));
					return new ClientCard[] { chosen };
				}
				if (base.Card != null && base.Card.Id == 80312545 && cards != null && cards.Count > 0)
				{
					if (this._gateWantsRecycle)
					{
						ClientCard pain = cards.FirstOrDefault((ClientCard c) => c != null && c.Id == 65261141);
						if (pain != null)
						{
							return new ClientCard[] { pain };
						}
						ClientCard anyCont = cards.FirstOrDefault((ClientCard c) => c != null && c.IsSpell() && c.HasType(CardType.Continuous));
						if (anyCont != null)
						{
							return new ClientCard[] { anyCont };
						}
					}
					if (cards.All((ClientCard c) => c != null && c.Controller == 0 && c.Location == CardLocation.Hand) && this._gateDiscardPreferredId != 0)
					{
						ClientCard want = cards.FirstOrDefault((ClientCard c) => c.Id == this._gateDiscardPreferredId);
						if (want != null)
						{
							return new ClientCard[] { want };
						}
						List<ClientCard> sorted = cards.OrderBy(new Func<ClientCard, int>(this.ScoreOwnCardForCost)).ToList<ClientCard>();
						return new ClientCard[] { sorted[0] };
					}
					else if (cards.Any((ClientCard c) => c != null && c.Location == CardLocation.Grave))
					{
						if (this.requiemSummoned)
						{
							ClientCard sp = cards.FirstOrDefault((ClientCard c) => c != null && c.Id == 90829280);
							if (sp != null)
							{
								return new ClientCard[] { sp };
							}
						}
						if (this._gateReviveTargetId != 0)
						{
							ClientCard t = cards.FirstOrDefault((ClientCard c) => c != null && c.Id == this._gateReviveTargetId);
							if (t != null)
							{
								return new ClientCard[] { t };
							}
						}
						if (this.moonSummoned)
						{
							int[] array = new int[] { 90829280, 78371393 };
							for (int i = 0; i < array.Length; i++)
							{
								int id2 = array[i];
								ClientCard pick = cards.FirstOrDefault((ClientCard c) => c != null && c.Id == id2);
								if (pick != null)
								{
									return new ClientCard[] { pick };
								}
							}
						}
						else
						{
							int[] array = new int[] { 90829280, 62318994, 81034083, 27439792, 78371393 };
							for (int i = 0; i < array.Length; i++)
							{
								int id = array[i];
								ClientCard pick2 = cards.FirstOrDefault((ClientCard c) => c != null && c.Id == id);
								if (pick2 != null)
								{
									return new ClientCard[] { pick2 };
								}
							}
						}
						ClientCard any0 = cards.FirstOrDefault((ClientCard c) => c != null && this.Is00FiendId(c.Id));
						if (any0 != null)
						{
							return new ClientCard[] { any0 };
						}
					}
				}
				if (base.Card.Id == 93729896 && this._throneStage == YubelExecutor.ThroneStage.Searching && this.thronePending && !this.throneSearched)
				{
					this.throneSearched = true;
					this._throneStage = YubelExecutor.ThroneStage.AwaitDestroyPrompt;
					ClientCard chosen2 = null;
					if (this.throneDesiredPick != 0)
					{
						chosen2 = cards.FirstOrDefault((ClientCard c) => c.Id == this.throneDesiredPick);
					}
					if (chosen2 == null)
					{
						chosen2 = cards[0];
					}
					Logger.DebugWriteLine("[THRONE] Search pick => " + this.CardStr(chosen2));
					return new ClientCard[] { chosen2 };
				}
				if (this._yubelWantsTribute && isReleasePrompt && cards != null && cards.Count > 0)
				{
					ClientCard lotus = cards.FirstOrDefault((ClientCard c) => c != null && c.Id == 62318994);
					if (lotus != null)
					{
						this._yubelWantsTribute = false;
						return new ClientCard[] { lotus };
					}
					this._yubelWantsTribute = false;
				}
				if (base.Card.Id == 70636044 && hint == 502 && cards != null && cards.Count > 0)
				{
					ClientCard enemyPick = (from c in cards
						where c != null && c.Controller == 1
						orderby this.ScoreEnemyCardForRemoval(c) descending
						select c).FirstOrDefault<ClientCard>();
					if (enemyPick != null)
					{
						return new ClientCard[] { enemyPick };
					}
					return new ClientCard[] { cards[0] };
				}
				else if (base.Card.Id == 29479265 && hint == 502 && cards != null && cards.Count > 0)
				{
					ClientCard enemyPick2 = (from c in cards
						where c != null && c.Controller == 1
						orderby this.ScoreEnemyCardForRemoval(c) descending
						select c).FirstOrDefault<ClientCard>();
					if (enemyPick2 != null)
					{
						return new ClientCard[] { enemyPick2 };
					}
					return new ClientCard[] { cards[0] };
				}
				else if (solving != null && solving.IsCode(67680512))
				{
					if (cards.Any((ClientCard c) => c != null && c.Location == CardLocation.Extra))
					{
						ClientCard pickSP = cards.FirstOrDefault((ClientCard c) => c != null && c.Id == 29301450);
						if (pickSP != null)
						{
							return new List<ClientCard> { pickSP };
						}
						ClientCard pickGorgon = cards.FirstOrDefault((ClientCard c) => c != null && c.Id == 12067160);
						if (pickGorgon != null && this.HasFreeEMZ())
						{
							return new List<ClientCard> { pickGorgon };
						}
						return new List<ClientCard> { cards[0] };
					}
				}
			}
			return base.OnSelectCard(cards, min, max, hint, cancelable);
		}

		// Token: 0x06002344 RID: 9028 RVA: 0x000E8428 File Offset: 0x000E6628
		private int ScoreOwnCardForCost(ClientCard c)
		{
			if (c == null)
			{
				return int.MaxValue;
			}
			int score = 5000;
			if (YubelExecutor.NEVER_SAC.Contains(c.Id))
			{
				return int.MaxValue;
			}
			if (YubelExecutor.YUBEL_SET.Contains(c.Id))
			{
				return 9000;
			}
			if ((c.HasType(CardType.Link) && c.LinkCount >= 2) || c.HasType((CardType)8396864))
			{
				score += 2000;
			}
			if (c.EquipCards != null && c.EquipCards.Count > 0)
			{
				score += 1000;
			}
			int idx = Array.IndexOf<int>(YubelExecutor.YubelCostPriority, c.Id);
			if (idx >= 0)
			{
				score = 10 + idx;
			}
			return score + Math.Max(0, c.Attack / 100);
		}

		// Token: 0x06002345 RID: 9029 RVA: 0x000E84EC File Offset: 0x000E66EC
		private int ScoreEnemyCardForRemoval(ClientCard c)
		{
			if (c == null)
			{
				return -1;
			}
			int s = 0;
			if (c.IsFloodgate())
			{
				s += 6000;
			}
			if (c.IsMonsterDangerous())
			{
				s += 4000;
			}
			if (c.IsMonsterInvincible())
			{
				s += 3500;
			}
			if (c.EquipCards != null && c.EquipCards.Count > 0)
			{
				s += 800;
			}
			if (c.HasType((CardType)8396992))
			{
				s += 700;
			}
			if (c.HasType(CardType.Link) && c.LinkCount >= 2)
			{
				s += 700;
			}
			return s + Math.Max(0, c.Attack);
		}

		// Token: 0x06002346 RID: 9030 RVA: 0x000E8594 File Offset: 0x000E6794
		private string CardStr(ClientCard c)
		{
			if (c == null)
			{
				return "null";
			}
			string loc = c.Location.ToString();
			string face = (c.IsFaceup() ? "FU" : "FD");
			return string.Format("{0}#{1} [{2}] P{3} {4}", new object[] { c.Name, c.Id, loc, c.Controller, face });
		}

		// Token: 0x06002347 RID: 9031 RVA: 0x000E8614 File Offset: 0x000E6814
		private void DumpChain(string tag = "")
		{
			Logger.DebugWriteLine(string.Format("[CHAIN]{0} turn={1} player={2} phase={3} chainCount={4}", new object[]
			{
				(tag == "") ? "" : (" (" + tag + ")"),
				base.Duel.Turn,
				base.Duel.Player,
				base.Duel.Phase,
				base.Duel.CurrentChain.Count
			}));
			for (int i = 0; i < base.Duel.CurrentChain.Count; i++)
			{
				ClientCard c = base.Duel.CurrentChain[i];
				Logger.DebugWriteLine(string.Format("  [{0}] {1}", i, this.CardStr(c)));
			}
			ClientCard solving = base.Duel.GetCurrentSolvingChainCard();
			if (solving != null)
			{
				Logger.DebugWriteLine(string.Format("  -> Solving: {0}  ActivateDescription={1}", this.CardStr(solving), base.ActivateDescription));
			}
			if (base.Duel.ChainTargets != null && base.Duel.ChainTargets.Count > 0)
			{
				IEnumerable<string> tg = base.Duel.ChainTargets.Where((ClientCard t) => t != null).Select(new Func<ClientCard, string>(this.CardStr));
				Logger.DebugWriteLine("  targets: " + string.Join(" | ", tg));
			}
		}

		// Token: 0x06002348 RID: 9032 RVA: 0x000E87A4 File Offset: 0x000E69A4
		private void DebugThroneDescMap(int incomingDesc)
		{
			for (int i = 0; i < 5; i++)
			{
				int sid = base.Util.GetStringId(93729896, i);
				Logger.DebugWriteLine(string.Format("[THRONE] desc map i={0} strId={1} match={2}", i, sid, sid == incomingDesc));
			}
		}

		// Token: 0x06002349 RID: 9033 RVA: 0x000E87F3 File Offset: 0x000E69F3
		private bool BlockIfThrone(string who)
		{
			if (this.InThroneFlow)
			{
				Logger.DebugWriteLine("[THRONE] BLOCKED " + who + " during Throne flow");
				return true;
			}
			return false;
		}

		// Token: 0x0400250E RID: 9486
		private const int SetcodeTimeLord = 74;

		// Token: 0x0400250F RID: 9487
		private const int SetcodePhantom = 219;

		// Token: 0x04002510 RID: 9488
		private const int SetcodeOrcust = 283;

		// Token: 0x04002511 RID: 9489
		private const int SetcodeHorus = 413;

		// Token: 0x04002512 RID: 9490
		private Dictionary<int, List<int>> DeckCountTable = new Dictionary<int, List<int>>
		{
			{
				3,
				new List<int> { 90829280, 62318994, 93729896, 80312545, 14558127, 81034083, 10045474 }
			},
			{
				2,
				new List<int> { 23434538, 24224830, 24215921 }
			},
			{
				1,
				new List<int>
				{
					60764609, 99989863, 78371393, 4779091, 80801743, 65681983, 24215921, 97651498, 65261141, 73628505,
					98567237, 41165831, 27439792, 28803166
				}
			}
		};

		// Token: 0x04002513 RID: 9491
		private List<int> notToNegateIdList = new List<int> { 58699500, 20343502, 19403423 };

		// Token: 0x04002514 RID: 9492
		private List<int> notToDestroySpellTrap = new List<int> { 50005218, 6767771 };

		// Token: 0x04002515 RID: 9493
		private List<int> targetNegateIdList = new List<int>
		{
			97268402, 10045474, 52038441, 78474168, 74003290, 67037924, 9753964, 66192538, 23204029, 73445448,
			35103106, 30286474, 45002991, 5795980, 38511382, 53742162, 30430448
		};

		// Token: 0x04002516 RID: 9494
		private static readonly int[] LinkFodderPriority = new int[]
		{
			41165831, 4779091, 67680512, 24269961, 24215921, 97651498, 28803166, 60303245, 81034083, 27439792,
			62318994, 90829280, 78371393, 60764609
		};

		// Token: 0x04002517 RID: 9495
		private static readonly HashSet<int> YUBEL_SET = new HashSet<int> { 78371393, 4779091, 90829280, 80453041 };

		// Token: 0x04002518 RID: 9496
		private int _totalAttack;

		// Token: 0x04002519 RID: 9497
		private int _totalBotAttack;

		// Token: 0x0400251A RID: 9498
		private bool enemyActivateMaxxC;

		// Token: 0x0400251B RID: 9499
		private bool enemyActivateLockBird;

		// Token: 0x0400251C RID: 9500
		private int dimensionShifterCount;

		// Token: 0x0400251D RID: 9501
		private bool enemyActivateInfiniteImpermanenceFromHand;

		// Token: 0x0400251E RID: 9502
		private List<int> infiniteImpermanenceList = new List<int>();

		// Token: 0x0400251F RID: 9503
		private List<ClientCard> currentNegateCardList = new List<ClientCard>();

		// Token: 0x04002520 RID: 9504
		private List<ClientCard> currentDestroyCardList = new List<ClientCard>();

		// Token: 0x04002521 RID: 9505
		private List<ClientCard> sendToGYThisTurn = new List<ClientCard>();

		// Token: 0x04002522 RID: 9506
		private List<int> activatedCardIdList = new List<int>();

		// Token: 0x04002523 RID: 9507
		private List<ClientCard> enemyPlaceThisTurn = new List<ClientCard>();

		// Token: 0x04002524 RID: 9508
		private List<ClientCard> escapeTargetList = new List<ClientCard>();

		// Token: 0x04002525 RID: 9509
		private List<ClientCard> summonThisTurn = new List<ClientCard>();

		// Token: 0x04002526 RID: 9510
		private bool _yubelWantsTribute;

		// Token: 0x04002527 RID: 9511
		private YubelExecutor.ThroneStage _throneStage;

		// Token: 0x04002528 RID: 9512
		private int _gateReviveTargetId;

		// Token: 0x04002529 RID: 9513
		private int _gateDiscardPreferredId;

		// Token: 0x0400252A RID: 9514
		private bool _gateWantsRecycle;

		// Token: 0x0400252B RID: 9515
		private bool _spQuickMode;

		// Token: 0x0400252C RID: 9516
		private bool moonSummoned;

		// Token: 0x0400252D RID: 9517
		private bool requiemSummoned;

		// Token: 0x0400252E RID: 9518
		private bool thronePending;

		// Token: 0x0400252F RID: 9519
		private bool throneSearched;

		// Token: 0x04002530 RID: 9520
		private int throneDesiredPick;

		// Token: 0x04002531 RID: 9521
		private static readonly HashSet<int> NEVER_SAC = new HashSet<int> { 80453041, 79559912, 70636044, 29301450, 12067160, 29479265 };

		// Token: 0x04002532 RID: 9522
		private static readonly int[] YubelCostPriority = new int[] { 62318994, 97651498, 81034083, 27439792, 24215921, 60303245 };

		// Token: 0x02000435 RID: 1077
		public class CardId
		{
			// Token: 0x04002533 RID: 9523
			public const int YUBEL = 78371393;

			// Token: 0x04002534 RID: 9524
			public const int YUBEL_TERROR_INCARNATE = 4779091;

			// Token: 0x04002535 RID: 9525
			public const int SPIRIT_OF_YUBEL = 90829280;

			// Token: 0x04002536 RID: 9526
			public const int PHANTOM_OF_YUBEL = 80453041;

			// Token: 0x04002537 RID: 9527
			public const int SAMSARA_D_LOTUS = 62318994;

			// Token: 0x04002538 RID: 9528
			public const int GRUESOME_GRAVE_SQUIRMER = 24215921;

			// Token: 0x04002539 RID: 9529
			public const int FABLED_LURRIE = 97651498;

			// Token: 0x0400253A RID: 9530
			public const int SHARVARA = 41165831;

			// Token: 0x0400253B RID: 9531
			public const int FIENDSMITH_ENGRAVER = 60764609;

			// Token: 0x0400253C RID: 9532
			public const int LACRIMA_CT = 28803166;

			// Token: 0x0400253D RID: 9533
			public const int DARK_BECKONING_BEAST = 81034083;

			// Token: 0x0400253E RID: 9534
			public const int CHAOS_SUMMONING_BEAST = 27439792;

			// Token: 0x0400253F RID: 9535
			public const int NIGHTMARE_PAIN = 65261141;

			// Token: 0x04002540 RID: 9536
			public const int NIGHTMARE_THRONE = 93729896;

			// Token: 0x04002541 RID: 9537
			public const int SPIRIT_GATES = 80312545;

			// Token: 0x04002542 RID: 9538
			public const int FIENDSMITH_TRACT = 98567237;

			// Token: 0x04002543 RID: 9539
			public const int ABOMINABLE_CHAMBER = 80801743;

			// Token: 0x04002544 RID: 9540
			public const int FIENDSMITHS_PARADISE = 99989863;

			// Token: 0x04002545 RID: 9541
			public const int TERRAFORMING = 73628505;

			// Token: 0x04002546 RID: 9542
			public const int FIENDSMITHS_DESIRAE = 82135803;

			// Token: 0x04002547 RID: 9543
			public const int VARUDASN_FINAL_BRINGER = 70636044;

			// Token: 0x04002548 RID: 9544
			public const int DDD_WAVE_HIGH_KING_CAESAR = 79559912;

			// Token: 0x04002549 RID: 9545
			public const int UNCHAINED_LORD_OF_YAMA = 24269961;

			// Token: 0x0400254A RID: 9546
			public const int UNCHAINED_SOUL_OF_RAGE = 67680512;

			// Token: 0x0400254B RID: 9547
			public const int SP_LITTLE_KNIGHT = 29301450;

			// Token: 0x0400254C RID: 9548
			public const int MOON_OF_THE_CLOSED_HEAVEN = 71818935;

			// Token: 0x0400254D RID: 9549
			public const int FIENDSMITHS_REQUIEM = 2463794;

			// Token: 0x0400254E RID: 9550
			public const int SALAMANGREAT_ALMIRAJ = 60303245;

			// Token: 0x0400254F RID: 9551
			public const int NECROQUIP = 93860227;

			// Token: 0x04002550 RID: 9552
			public const int GORGONOFZIL = 12067160;

			// Token: 0x04002551 RID: 9553
			public const int GUSTAVMAX = 56910167;

			// Token: 0x04002552 RID: 9554
			public const int JUGGERNAUT = 26096328;

			// Token: 0x04002553 RID: 9555
			public const int UNCHAINDEDABOMINATION = 29479265;

			// Token: 0x04002554 RID: 9556
			public const int Fuwalos = 42141493;

			// Token: 0x04002555 RID: 9557
			public const int NaturalExterio = 99916754;

			// Token: 0x04002556 RID: 9558
			public const int NaturalBeast = 33198837;

			// Token: 0x04002557 RID: 9559
			public const int ImperialOrder = 61740673;

			// Token: 0x04002558 RID: 9560
			public const int SwordsmanLV7 = 37267041;

			// Token: 0x04002559 RID: 9561
			public const int RoyalDecree = 51452091;

			// Token: 0x0400255A RID: 9562
			public const int Number41BagooskatheTerriblyTiredTapir = 90590303;

			// Token: 0x0400255B RID: 9563
			public const int InspectorBoarder = 15397015;

			// Token: 0x0400255C RID: 9564
			public const int SkillDrain = 82732705;

			// Token: 0x0400255D RID: 9565
			public const int DivineArsenalAAZEUS_SkyThunder = 90448279;

			// Token: 0x0400255E RID: 9566
			public const int DimensionShifter = 91800273;

			// Token: 0x0400255F RID: 9567
			public const int MacroCosmos = 30241314;

			// Token: 0x04002560 RID: 9568
			public const int DimensionalFissure = 81674782;

			// Token: 0x04002561 RID: 9569
			public const int BanisheroftheRadiance = 94853057;

			// Token: 0x04002562 RID: 9570
			public const int BanisheroftheLight = 61528025;

			// Token: 0x04002563 RID: 9571
			public const int KashtiraAriseHeart = 48626373;

			// Token: 0x04002564 RID: 9572
			public const int AccesscodeTalker = 86066372;

			// Token: 0x04002565 RID: 9573
			public const int GhostMournerMoonlitChill = 52038441;

			// Token: 0x04002566 RID: 9574
			public const int NibiruThePrimalBeing = 27204311;
		}

		// Token: 0x02000436 RID: 1078
		private enum ThroneStage
		{
			// Token: 0x04002568 RID: 9576
			None,
			// Token: 0x04002569 RID: 9577
			Searching,
			// Token: 0x0400256A RID: 9578
			AwaitDestroyPrompt
		}
	}
}
