using System;
using System.Collections.Generic;
using System.Linq;
using YGOSharp.OCGWrapper;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI
{
	// Token: 0x02000230 RID: 560
	public abstract class DefaultExecutor : Executor
	{
		// Token: 0x06000BAE RID: 2990 RVA: 0x00032F00 File Offset: 0x00031100
		protected DefaultExecutor(GameAI ai, Duel duel)
		{
			Dictionary<int, Func<ClientCard, bool>> dictionary = new Dictionary<int, Func<ClientCard, bool>>();
			dictionary.Add(33655493, (ClientCard defender) => defender.IsFaceup());
			dictionary.Add(87294988, (ClientCard defender) => defender.HasRace(CardRace.Plant));
			dictionary.Add(19050066, (ClientCard defender) => true);
			dictionary.Add(48745395, (ClientCard defender) => defender.HasRace(CardRace.Fiend) && !defender.IsCode(48745395));
			dictionary.Add(4991081, (ClientCard defender) => defender.Level <= 6 && defender.HasSetcode(100));
			dictionary.Add(25123713, (ClientCard defender) => defender.HasSetcode(276));
			dictionary.Add(82260502, (ClientCard defender) => defender.IsFacedown());
			dictionary.Add(26420373, (ClientCard defender) => defender.HasSetcode(8214));
			dictionary.Add(2333365, (ClientCard defender) => defender.HasSetcode(66));
			dictionary.Add(99348756, (ClientCard defender) => defender.HasRace(CardRace.Warrior) && !defender.IsCode(99348756));
			dictionary.Add(75162696, (ClientCard defender) => defender.HasRace(CardRace.Fairy));
			dictionary.Add(75363626, (ClientCard defender) => defender.HasSetcode(113) && !defender.IsCode(75363626));
			dictionary.Add(67511500, (ClientCard defender) => defender.HasRace(CardRace.Dragon));
			dictionary.Add(95466842, (ClientCard defender) => defender.HasAttribute(CardAttribute.Water));
			dictionary.Add(6924874, (ClientCard defender) => defender.HasSetcode(100) && !defender.IsCode(6924874));
			dictionary.Add(94535485, (ClientCard defender) => true);
			dictionary.Add(9666558, (ClientCard defender) => defender.HasRace(CardRace.Dragon));
			dictionary.Add(2460565, (ClientCard defender) => defender.HasRace(CardRace.Warrior));
			dictionary.Add(2986553, (ClientCard defender) => defender.HasRace(CardRace.Plant));
			dictionary.Add(70458081, (ClientCard defender) => defender.HasSetcode(159));
			dictionary.Add(131182, (ClientCard defender) => defender.IsFaceup());
			dictionary.Add(25034083, (ClientCard defender) => defender.IsFaceup());
			dictionary.Add(46239604, (ClientCard defender) => true);
			dictionary.Add(12977245, (ClientCard defender) => defender.HasSetcode(259));
			dictionary.Add(55401221, (ClientCard defender) => defender.HasRace(CardRace.Thunder) && !defender.IsCode(55401221));
			dictionary.Add(61380658, (ClientCard defender) => defender.HasSetcode(14) && defender.IsFaceup());
			dictionary.Add(42166000, (ClientCard defender) => true);
			dictionary.Add(22900219, (ClientCard defender) => true);
			dictionary.Add(58672736, (ClientCard defender) => true);
			dictionary.Add(45298492, (ClientCard defender) => defender.HasRace(CardRace.Warrior) && defender.IsFaceup());
			dictionary.Add(50449881, (ClientCard defender) => true);
			dictionary.Add(97453744, (ClientCard defender) => true);
			dictionary.Add(75367227, (ClientCard defender) => defender.HasSetcode(141) || defender.IsFacedown());
			dictionary.Add(21887175, (ClientCard defender) => true);
			dictionary.Add(77967790, (ClientCard defender) => true);
			dictionary.Add(41522092, (ClientCard defender) => true);
			this.DefenderProtectRule = dictionary;
			Dictionary<int, Func<ClientCard, List<ClientCard>, bool>> dictionary2 = new Dictionary<int, Func<ClientCard, List<ClientCard>, bool>>();
			dictionary2.Add(1686814, (ClientCard defender, List<ClientCard> list) => list.Any((ClientCard monster) => !monster.Equals(defender) && monster.HasType(CardType.Synchro)));
			dictionary2.Add(92932860, (ClientCard defender, List<ClientCard> list) => list.Any((ClientCard monster) => monster.HasSetcode(153)));
			dictionary2.Add(40140448, (ClientCard defender, List<ClientCard> list) => list.Any((ClientCard monster) => !monster.Equals(defender) && monster.HasSetcode(311)));
			dictionary2.Add(40428851, (ClientCard defender, List<ClientCard> list) => list.Any((ClientCard monster) => !monster.Equals(defender) && monster.HasSetcode(311)));
			dictionary2.Add(10375182, (ClientCard defender, List<ClientCard> list) => list.Any((ClientCard monster) => !monster.Equals(defender)));
			dictionary2.Add(51962254, (ClientCard defender, List<ClientCard> list) => list.Any((ClientCard monster) => !monster.Equals(defender) && monster.HasAttribute(CardAttribute.Wind)));
			dictionary2.Add(5969957, (ClientCard defender, List<ClientCard> list) => list.Any((ClientCard monster) => monster.IsExtraCard() && monster.HasAttribute(CardAttribute.Dark)));
			dictionary2.Add(6103294, (ClientCard defender, List<ClientCard> list) => list.Any((ClientCard monster) => monster.HasType(CardType.Normal) && monster.Level <= 3));
			dictionary2.Add(24454387, (ClientCard defender, List<ClientCard> list) => list.Any((ClientCard monster) => !monster.Equals(defender) && monster.HasRace(CardRace.Dinosaur)));
			dictionary2.Add(14957440, (ClientCard defender, List<ClientCard> list) => list.Any((ClientCard monster) => !monster.HasType(CardType.Tuner)));
			dictionary2.Add(17285476, (ClientCard defender, List<ClientCard> list) => list.Any((ClientCard monster) => !monster.Equals(defender) && monster.HasSetcode(42)));
			dictionary2.Add(37617348, (ClientCard defender, List<ClientCard> list) => list.Any((ClientCard monster) => !monster.IsCode(37617348) && monster.HasSetcode(395)));
			dictionary2.Add(11825276, (ClientCard defender, List<ClientCard> list) => list.Any((ClientCard monster) => monster.IsFacedown()));
			dictionary2.Add(73483491, (ClientCard defender, List<ClientCard> list) => true);
			dictionary2.Add(75574498, (ClientCard defender, List<ClientCard> list) => list.Any((ClientCard monster) => !monster.Equals(defender)));
			dictionary2.Add(1426714, (ClientCard defender, List<ClientCard> list) => list.Any((ClientCard monster) => monster.IsCode(1426715)));
			dictionary2.Add(60025883, (ClientCard defender, List<ClientCard> list) => list.Any((ClientCard monster) => monster.IsCode(60025884)));
			dictionary2.Add(45819647, (ClientCard defender, List<ClientCard> list) => list.Any((ClientCard monster) => monster.HasSetcode(298)));
			dictionary2.Add(10000080, (ClientCard defender, List<ClientCard> list) => true);
			dictionary2.Add(23093373, (ClientCard defender, List<ClientCard> list) => list.Any((ClientCard monster) => !monster.IsCode(23093373)));
			dictionary2.Add(33206889, (ClientCard defender, List<ClientCard> list) => list.Any((ClientCard monster) => !monster.IsCode(33206889) && monster.HasSetcode(198)));
			dictionary2.Add(99193444, (ClientCard defender, List<ClientCard> list) => list.Any((ClientCard monster) => !monster.IsCode(99193444)));
			this.DefenderInvisbleRule = dictionary2;
			base..ctor(ai, duel);
			base.AddExecutor(ExecutorType.Activate, 67616300, new Func<bool>(this.DefaultChickenGame));
			base.AddExecutor(ExecutorType.Activate, 49568943, new Func<bool>(this.DefaultVaylantzWorld_ShinraBansho));
			base.AddExecutor(ExecutorType.Activate, 75952542, new Func<bool>(this.DefaultVaylantzWorld_KonigWissen));
			base.AddExecutor(ExecutorType.Activate, 46565218);
			base.AddExecutor(ExecutorType.SpellSet, new Func<bool>(this.DefaultSetForDiabellze));
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x00033934 File Offset: 0x00031B34
		public override BattlePhaseAction OnSelectAttackTarget(ClientCard attacker, IList<ClientCard> defenders)
		{
			foreach (ClientCard defender in defenders)
			{
				attacker.RealPower = attacker.Attack;
				defender.RealPower = defender.GetDefensePower();
				if (this.OnPreBattleBetween(attacker, defender) && (attacker.RealPower > defender.RealPower || (attacker.RealPower >= defender.RealPower && attacker.IsLastAttacker && defender.IsAttack())))
				{
					return base.AI.Attack(attacker, defender);
				}
			}
			if (attacker.CanDirectAttack)
			{
				return base.AI.Attack(attacker, null);
			}
			return null;
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x000339EC File Offset: 0x00031BEC
		public override bool OnPreBattleBetween(ClientCard attacker, ClientCard defender)
		{
			if (!attacker.IsMonsterHasPreventActivationEffectInBattle())
			{
				if (defender.IsMonsterInvincible() && defender.IsDefense())
				{
					return false;
				}
				if (defender.IsMonsterDangerous() && (attacker.IsDisabled() || ((!attacker.IsCode(18940556) || !defender.IsDefense()) && (!attacker.IsCode(20366274) || !defender.IsSpecialSummoned) && (!attacker.IsCode(26593852) || defender.HasAttribute(CardAttribute.Dark)))))
				{
					return false;
				}
				if (defender.EquipCards.Any((ClientCard equip) => equip.IsCode(19508728) && !equip.IsDisabled()))
				{
					return false;
				}
				if (!defender.IsDisabled())
				{
					if (defender.IsCode(21887175) && defender.IsAttack() && attacker.IsSpecialSummoned)
					{
						defender.RealPower += attacker.Attack;
					}
					if (defender.IsCode(50954680) && defender.IsAttack() && attacker.Level >= 5)
					{
						defender.RealPower += attacker.Attack;
					}
					if (defender.IsCode(26593852) && !attacker.HasAttribute(CardAttribute.Dark))
					{
						return false;
					}
					if (defender.IsCode(56832966) && defender.IsAttack() && defender.HasXyzMaterial(2, 84013237))
					{
						defender.RealPower = 5000;
					}
					if (defender.IsCode(6039967))
					{
						defender.RealPower += ((base.Enemy.LifePoints > 3000) ? 3000 : (base.Enemy.LifePoints - 100));
					}
					if (defender.IsCode(79575620) && base.Enemy.LifePoints > 2000)
					{
						defender.RealPower += 3000;
					}
				}
			}
			if (!defender.IsMonsterHasPreventActivationEffectInBattle())
			{
				if (attacker.IsCode(56832966) && !attacker.IsDisabled() && attacker.HasXyzMaterial(2, 84013237))
				{
					attacker.RealPower = 5000;
				}
				if (attacker.IsCode(63845230) && !attacker.IsDisabled())
				{
					attacker.RealPower = 9999;
				}
				if (attacker.IsMonsterInvincible())
				{
					attacker.RealPower = 9999;
				}
				if (attacker.EquipCards.Any((ClientCard equip) => equip.IsCode(19508728) && !equip.IsDisabled()))
				{
					attacker.RealPower = defender.RealPower + 100;
				}
			}
			foreach (ClientCard protecter in base.Enemy.GetMonsters())
			{
				if (!protecter.IsDisabled() && protecter != defender)
				{
					Func<ClientCard, bool> defenderRule = (ClientCard card) => false;
					if (this.DefenderProtectRule.TryGetValue(protecter.Id, out defenderRule) && defenderRule(defender))
					{
						return false;
					}
				}
			}
			if (attacker.EquipCards.Any((ClientCard equip) => equip.IsCode(19508728) && !equip.IsDisabled()))
			{
				attacker.RealPower = defender.RealPower + 100;
			}
			if (!defender.IsDisabled())
			{
				Func<ClientCard, List<ClientCard>, bool> defenderRule2 = (ClientCard card, List<ClientCard> monsterList) => false;
				if (this.DefenderInvisbleRule.TryGetValue(defender.Id, out defenderRule2) && defenderRule2(defender, base.Enemy.GetMonsters()))
				{
					return false;
				}
			}
			if (base.Enemy.GetMonsters().Any((ClientCard monster) => !monster.Equals(defender) && monster.IsCode(32491822) && !monster.IsDisabled() && monster.IsDefense()))
			{
				return false;
			}
			if (defender.OwnTargets.Any((ClientCard card) => card.IsCode(25542642) && !card.IsDisabled()))
			{
				return false;
			}
			if (defender.HasSetcode(4129) && !defender.IsDisabled())
			{
				return false;
			}
			if (base.Enemy.HasInMonstersZone(28168628, true, false, false))
			{
				if (base.Enemy.GetMonsters().Any((ClientCard card) => card.HasSetcode(405)))
				{
					goto IL_0561;
				}
			}
			bool flag;
			if (!base.Enemy.HasInMonstersZone(33652635, true, false, false) && !base.Enemy.HasInMonstersZone(19153634, false, false, false))
			{
				if (base.Enemy.HasInMonstersZone(66961194, true, false, false))
				{
					flag = base.Enemy.GetMonsters().Any((ClientCard card) => card.HasSetcode(221));
					goto IL_0562;
				}
				flag = false;
				goto IL_0562;
			}
			IL_0561:
			flag = true;
			IL_0562:
			if (flag)
			{
				if (defender.HasPosition(CardPosition.FaceDown))
				{
					return false;
				}
				if (base.Enemy.GetMonsters().Any((ClientCard card) => card.IsFaceup() && card.Attack > defender.Attack))
				{
					return false;
				}
			}
			if (base.Enemy.HasInSpellZone(29477860, true, false) && base.Enemy.HasInMonstersZone(66889139, false, false, false) && !defender.IsCode(66889139))
			{
				return false;
			}
			if (base.Enemy.HasInSpellZone(55312487, true, false))
			{
				if (base.Enemy.GetMonsters().Any((ClientCard card) => card.HasSetcode(278) && card.HasType(CardType.Link)) && !defender.HasType(CardType.Link))
				{
					return false;
				}
			}
			if (defender.IsCode(37617348) && !defender.IsDisabled())
			{
				if (base.Enemy.GetMonsters().Any((ClientCard monster) => monster.HasSetcode(395) && !monster.IsCode(37617348)))
				{
					return false;
				}
			}
			if (base.Enemy.HasInSpellZone(98477480, true, false) && base.Enemy.HasInMonstersZone(25801745, false, false, true))
			{
				if (base.Enemy.GetMonsters().Any((ClientCard card) => card.HasType(CardType.Ritual) && card.IsFaceup()) && !defender.HasType(CardType.Ritual))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x000340F8 File Offset: 0x000322F8
		public override bool OnPreActivate(ClientCard card)
		{
			ClientCard LastChainCard = base.Util.GetLastChainCard();
			return (LastChainCard == null || base.Duel.Phase != DuelPhase.Standby || !LastChainCard.IsCode(new int[] { 33015627, 6616912, 7733560, 28929131, 34137269, 60222213, 65314286, 74530899, 91712985, 92435533 })) && ((card.Location != CardLocation.Hand && (card.Location != CardLocation.SpellZone || !card.IsFacedown())) || ((!card.IsSpell() || !this.DefaultSpellWillBeNegated()) && (!card.IsTrap() || !this.DefaultTrapWillBeNegated())));
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0003417C File Offset: 0x0003237C
		public override CardPosition OnSelectPosition(int cardId, IList<CardPosition> positions)
		{
			NamedCard cardData = NamedCard.Get(cardId);
			if (cardData != null && cardData.Attack == 0)
			{
				return CardPosition.FaceUpDefence;
			}
			return (CardPosition)0;
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x000341A0 File Offset: 0x000323A0
		public override bool OnSelectBattleReplay()
		{
			if (base.Bot.BattlingMonster == null)
			{
				return false;
			}
			List<ClientCard> defenders = new List<ClientCard>(base.Duel.Fields[1].GetMonsters());
			defenders.Sort(new Comparison<ClientCard>(CardContainer.CompareDefensePower));
			defenders.Reverse();
			BattlePhaseAction result = this.OnSelectAttackTarget(base.Bot.BattlingMonster, defenders);
			return result != null && result.Action == BattlePhaseAction.BattleAction.Attack;
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00034210 File Offset: 0x00032410
		public override bool OnSelectMonsterSummonOrSet(ClientCard card)
		{
			if (card.Level <= 4)
			{
				if (base.Bot.GetMonsters().Count((ClientCard m) => m.IsFaceup()) == 0)
				{
					return base.Util.IsAllEnemyBetterThanValue(card.Attack, true);
				}
			}
			return false;
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0003426C File Offset: 0x0003246C
		public override IList<ClientCard> OnSelectCard(IList<ClientCard> cards, int min, int max, int hint, bool cancelable)
		{
			int albaZoaCount = base.Bot.ExtraDeck.Count / 2;
			if (!cancelable && min == albaZoaCount && max == albaZoaCount && base.Duel.Player == 1 && (base.Duel.Phase == DuelPhase.Main1 || base.Duel.Phase == DuelPhase.Main2))
			{
				if (cards.All((ClientCard card) => card.Controller == 0 && (card.Location == CardLocation.Hand || card.Location == CardLocation.Extra)))
				{
					Logger.DebugWriteLine("Dogmatika Alba Zoa solved");
					List<ClientCard> extraDeck = new List<ClientCard>(base.Bot.ExtraDeck);
					int shuffleCount = extraDeck.Count;
					while (shuffleCount-- > 1)
					{
						int index = Program.Rand.Next(extraDeck.Count);
						ClientCard tempCard = extraDeck[shuffleCount];
						extraDeck[shuffleCount] = extraDeck[index];
						extraDeck[index] = tempCard;
					}
					return base.Util.CheckSelectCount(extraDeck, cards, min, max);
				}
			}
			return null;
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0003436C File Offset: 0x0003256C
		public override void OnReceivingAnnouce(int player, int data)
		{
			if ((player == 1 && data == base.Util.GetStringId(14532163, 0)) || data == base.Util.GetStringId(14532163, 1))
			{
				this.lightningStormOption = data - base.Util.GetStringId(14532163, 0);
			}
			base.OnReceivingAnnouce(player, data);
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x000343C6 File Offset: 0x000325C6
		public override void OnChainEnd()
		{
			this.lightningStormOption = -1;
			base.OnChainEnd();
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x000343D8 File Offset: 0x000325D8
		public override void OnNewTurn()
		{
			if (base.Duel.Turn <= 1)
			{
				this.calledbytheGraveIdCountMap.Clear();
			}
			foreach (int dic in this.calledbytheGraveIdCountMap.Keys.ToList<int>())
			{
				if (this.calledbytheGraveIdCountMap[dic] > 0)
				{
					Dictionary<int, int> dictionary = this.calledbytheGraveIdCountMap;
					int num = dic;
					dictionary[num]--;
				}
			}
			this.crossoutDesignatorIdList.Clear();
			base.OnNewTurn();
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x00034480 File Offset: 0x00032680
		public override void OnMove(ClientCard card, int previousControler, int previousLocation, int currentControler, int currentLocation)
		{
			if (card != null)
			{
				ClientCard currentSolvingChain = base.Duel.GetCurrentSolvingChainCard();
				if (currentSolvingChain != null && currentLocation == 32)
				{
					int originId = card.Id;
					if (card.Data != null)
					{
						if (card.Data.Alias > 0)
						{
							originId = card.Data.Alias;
						}
						else
						{
							originId = card.Id;
						}
					}
					if (currentSolvingChain.IsCode(24224830))
					{
						this.calledbytheGraveIdCountMap[originId] = 2;
					}
					if (currentSolvingChain.IsCode(65681983))
					{
						this.crossoutDesignatorIdList.Add(originId);
					}
				}
			}
			base.OnMove(card, previousControler, previousLocation, currentControler, currentLocation);
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x00034518 File Offset: 0x00032718
		protected bool DefaultMysticalSpaceTyphoon()
		{
			if (base.Duel.CurrentChain.Any((ClientCard card) => card.IsCode(5318639)))
			{
				return false;
			}
			List<ClientCard> spells = base.Enemy.GetSpells();
			if (spells.Count == 0)
			{
				return false;
			}
			ClientCard selected = base.Enemy.SpellZone.GetFloodgate(false);
			if (selected == null)
			{
				if (base.Duel.Player == 0)
				{
					selected = spells.FirstOrDefault((ClientCard card) => card.IsFacedown());
				}
				if (base.Duel.Player == 1)
				{
					selected = spells.FirstOrDefault((ClientCard card) => card.HasType(CardType.Continuous) || card.HasType(CardType.Equip) || card.HasType(CardType.Field));
				}
			}
			if (selected == null)
			{
				return false;
			}
			base.AI.SelectCard(selected);
			return true;
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x000345FC File Offset: 0x000327FC
		protected bool DefaultCosmicCyclone()
		{
			using (IEnumerator<ClientCard> enumerator = base.Duel.CurrentChain.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsCode(8267140))
					{
						return false;
					}
				}
			}
			return base.Bot.LifePoints > 1000 && this.DefaultMysticalSpaceTyphoon();
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x00034674 File Offset: 0x00032874
		protected bool DefaultGalaxyCyclone()
		{
			List<ClientCard> spells = base.Enemy.GetSpells();
			if (spells.Count == 0)
			{
				return false;
			}
			ClientCard selected;
			if (base.Card.Location == CardLocation.Grave)
			{
				selected = base.Util.GetBestEnemySpell(true);
			}
			else
			{
				selected = spells.FirstOrDefault((ClientCard card) => card.IsFacedown());
			}
			if (selected == null)
			{
				return false;
			}
			base.AI.SelectCard(selected);
			return true;
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x000346F0 File Offset: 0x000328F0
		protected bool DefaultBookOfMoon()
		{
			if (base.Util.IsAllEnemyBetter(true))
			{
				ClientCard monster = base.Enemy.GetMonsters().GetHighestAttackMonster(true);
				if (monster != null && monster.HasType(CardType.Effect) && !monster.HasType(CardType.Link) && (monster.HasType(CardType.Xyz) || monster.Level > 4))
				{
					base.AI.SelectCard(monster);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0003475C File Offset: 0x0003295C
		protected bool DefaultCompulsoryEvacuationDevice()
		{
			ClientCard target = base.Util.GetProblematicEnemyMonster(0, true);
			if (target != null)
			{
				base.AI.SelectCard(target);
				return true;
			}
			if (base.Util.IsChainTarget(base.Card))
			{
				ClientCard monster = base.Util.GetBestEnemyMonster(false, true);
				if (monster != null)
				{
					base.AI.SelectCard(monster);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x000347BC File Offset: 0x000329BC
		protected bool DefaultCallOfTheHaunted()
		{
			if (!base.Util.IsAllEnemyBetter(true))
			{
				return false;
			}
			ClientCard selected = (from card in base.Bot.Graveyard.GetMatchingCards((ClientCard card) => card.IsCanRevive())
				orderby card.Attack descending
				select card).FirstOrDefault<ClientCard>();
			base.AI.SelectCard(selected);
			return true;
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x00034840 File Offset: 0x00032A40
		protected bool DefaultScapegoat()
		{
			if (this.DefaultSpellWillBeNegated())
			{
				return false;
			}
			if (base.Duel.Player == 0)
			{
				return false;
			}
			if (base.Duel.Phase == DuelPhase.End)
			{
				return true;
			}
			if (this.DefaultOnBecomeTarget())
			{
				return true;
			}
			if (base.Duel.Phase > DuelPhase.Main1 && base.Duel.Phase < DuelPhase.Main2)
			{
				if (base.Enemy.HasInMonstersZone(new int[] { 18940556, 12307878, 51788412, 12652643, 70902743 }, true, false, false))
				{
					return false;
				}
				if (base.Util.GetTotalAttackingMonsterAttack(1) >= base.Bot.LifePoints)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x000348E3 File Offset: 0x00032AE3
		protected bool DefaultMaxxC()
		{
			return !this.DefaultCheckWhetherCardIsNegated(base.Card) && base.Duel.Player == 1;
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x00034904 File Offset: 0x00032B04
		protected bool DefaultAshBlossomAndJoyousSpring()
		{
			if (this.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			int[] ignoreList = new int[] { 30241314, 70368879, 60600126, 64734921 };
			return !base.Util.GetLastChainCard().IsCode(ignoreList) && (!base.Util.GetLastChainCard().HasSetcode(286) || base.Util.GetLastChainCard().Location != CardLocation.Hand) && base.Duel.LastChainPlayer == 1;
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x00034982 File Offset: 0x00032B82
		protected bool DefaultGhostOgreAndSnowRabbit()
		{
			return !this.DefaultCheckWhetherCardIsNegated(base.Card) && (base.Util.GetLastChainCard() == null || !base.Util.GetLastChainCard().IsDisabled()) && this.DefaultTrap();
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x000349BB File Offset: 0x00032BBB
		protected bool DefaultGhostBelleAndHauntedMansion()
		{
			return !this.DefaultCheckWhetherCardIsNegated(base.Card) && this.DefaultTrap();
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x000349D4 File Offset: 0x00032BD4
		protected bool DefaultEffectVeiler()
		{
			if (this.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			ClientCard LastChainCard = base.Util.GetLastChainCard();
			return (LastChainCard == null || ((!LastChainCard.IsCode(46659709) || base.Enemy.Hand.Count < 3) && !LastChainCard.IsCode(new int[] { 97268402, 10045474 }))) && this.DefaultBreakthroughSkill();
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x00034A48 File Offset: 0x00032C48
		protected bool DefaultCalledByTheGrave()
		{
			int[] targetList = new int[] { 23434538, 94145021, 59438930, 14558127, 73642296, 97268402, 34267821 };
			if (base.Duel.LastChainPlayer == 1)
			{
				foreach (int id in targetList)
				{
					if (base.Util.GetLastChainCard().IsCode(id))
					{
						base.AI.SelectCard(id);
						return this.UniqueFaceupSpell();
					}
				}
			}
			return false;
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x00034AB0 File Offset: 0x00032CB0
		protected bool DefaultInfiniteImpermanence()
		{
			if (this.DefaultCheckWhetherCardIsNegated(base.Card))
			{
				return false;
			}
			ClientCard LastChainCard = base.Util.GetLastChainCard();
			return (LastChainCard == null || ((!LastChainCard.IsCode(46659709) || base.Enemy.Hand.Count < 3) && !LastChainCard.IsCode(new int[] { 97268402, 10045474 }))) && this.DefaultDisableMonster();
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x00034B22 File Offset: 0x00032D22
		protected bool DefaultBreakthroughSkill()
		{
			return this.DefaultUniqueTrap() && this.DefaultDisableMonster();
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x00034B34 File Offset: 0x00032D34
		protected bool DefaultDisableMonster()
		{
			if (base.Duel.Player == 1)
			{
				ClientCard target = base.Enemy.MonsterZone.GetShouldBeDisabledBeforeItUseEffectMonster(true);
				if (target != null)
				{
					base.AI.SelectCard(target);
					return true;
				}
			}
			ClientCard LastChainCard = base.Util.GetLastChainCard();
			if (LastChainCard != null && LastChainCard.Controller == 1 && LastChainCard.Location == CardLocation.MonsterZone && !LastChainCard.IsDisabled() && !LastChainCard.IsShouldNotBeTarget() && !LastChainCard.IsShouldNotBeSpellTrapTarget())
			{
				base.AI.SelectCard(LastChainCard);
				return true;
			}
			if (base.Bot.BattlingMonster != null && base.Enemy.BattlingMonster != null && !base.Enemy.BattlingMonster.IsDisabled() && base.Enemy.BattlingMonster.IsCode(63845230))
			{
				base.AI.SelectCard(base.Enemy.BattlingMonster);
				return true;
			}
			if (base.Duel.Phase == DuelPhase.BattleStart && base.Duel.Player == 1 && base.Enemy.HasInMonstersZone(56832966, true, false, false))
			{
				base.AI.SelectCard(56832966);
				return true;
			}
			return false;
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x00034C58 File Offset: 0x00032E58
		protected bool DefaultSolemnJudgment()
		{
			return !base.Util.IsChainTargetOnly(base.Card) && (base.Duel.Player != 0 || base.Duel.LastChainPlayer != -1) && !this.DefaultOnlyHorusSpSummoning() && this.DefaultTrap();
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x00034C98 File Offset: 0x00032E98
		protected bool DefaultSolemnWarning()
		{
			return base.Bot.LifePoints > 2000 && (base.Duel.Player != 0 || base.Duel.LastChainPlayer != -1) && !this.DefaultOnlyHorusSpSummoning() && this.DefaultTrap();
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x00034CD7 File Offset: 0x00032ED7
		protected bool DefaultSolemnStrike()
		{
			return base.Bot.LifePoints > 1500 && (base.Duel.Player != 0 || base.Duel.LastChainPlayer != -1) && !this.DefaultOnlyHorusSpSummoning() && this.DefaultTrap();
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x00034D18 File Offset: 0x00032F18
		protected bool DefaultOnlyHorusSpSummoning()
		{
			if (base.Duel.SummoningCards.Count != 0)
			{
				bool notOnlyHorusFlag = false;
				foreach (ClientCard card in base.Duel.SummoningCards)
				{
					if (!card.HasSetcode(413) || card.LastLocation != CardLocation.Grave)
					{
						notOnlyHorusFlag = true;
						break;
					}
				}
				return !notOnlyHorusFlag;
			}
			return false;
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x00034D98 File Offset: 0x00032F98
		protected bool DefaultTorrentialTribute()
		{
			return !base.Util.HasChainedTrap(0) && base.Util.IsAllEnemyBetter(true);
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x00034DB6 File Offset: 0x00032FB6
		protected bool DefaultHeavyStorm()
		{
			return base.Bot.GetSpellCount() < base.Enemy.GetSpellCount();
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x00034DD0 File Offset: 0x00032FD0
		protected bool DefaultHarpiesFeatherDusterFirst()
		{
			return base.Enemy.GetSpellCount() >= 2;
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x00034DE3 File Offset: 0x00032FE3
		protected bool DefaultHammerShot()
		{
			return base.Util.IsOneEnemyBetter(true);
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x00034DF1 File Offset: 0x00032FF1
		protected bool DefaultDarkHole()
		{
			return base.Util.IsOneEnemyBetter(false);
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x00034DF1 File Offset: 0x00032FF1
		protected bool DefaultRaigeki()
		{
			return base.Util.IsOneEnemyBetter(false);
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00034DF1 File Offset: 0x00032FF1
		protected bool DefaultSmashingGround()
		{
			return base.Util.IsOneEnemyBetter(false);
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x00034DFF File Offset: 0x00032FFF
		protected bool DefaultPotOfDesires()
		{
			return base.Bot.Deck.Count > 15;
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x00034E15 File Offset: 0x00033015
		protected bool DefaultSpellSet()
		{
			return (base.Card.IsTrap() || base.Card.HasType(CardType.QuickPlay) || this.DefaultSpellMustSetFirst()) && base.Bot.GetSpellCountWithoutField() < 4;
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x00034E50 File Offset: 0x00033050
		protected bool DefaultMonsterSummon()
		{
			if (base.Card.Level <= 4)
			{
				return true;
			}
			if (!this.UniqueFaceupMonster())
			{
				return false;
			}
			int tributecount = (int)Math.Ceiling(((double)base.Card.Level - 4.0) / 2.0);
			for (int i = 0; i < 7; i++)
			{
				ClientCard tributeCard = base.Bot.MonsterZone[i];
				if (tributeCard != null && tributeCard.GetDefensePower() < base.Card.Attack)
				{
					tributecount--;
				}
			}
			return tributecount <= 0;
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x00034ED9 File Offset: 0x000330D9
		protected bool DefaultField()
		{
			return base.Bot.SpellZone[5] == null;
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x00034EEC File Offset: 0x000330EC
		protected bool DefaultMonsterRepos()
		{
			if (base.Card.IsMonsterInvincible())
			{
				return base.Card.IsDefense();
			}
			if (base.Card.Attack == 0)
			{
				if (base.Card.IsFaceup() && base.Card.IsAttack())
				{
					return true;
				}
				if (base.Card.IsFaceup() && base.Card.IsDefense())
				{
					return false;
				}
			}
			if (base.Enemy.HasInMonstersZone(55410871, true, false, false) && base.Card.IsAttack() && (4000 - base.Card.Defense) * 2 > 4000 - base.Card.Attack)
			{
				return false;
			}
			if (base.Enemy.HasInMonstersZone(55410871, true, false, false) && base.Card.IsDefense() && base.Card.IsFaceup() && (4000 - base.Card.Defense) * 2 > 4000 - base.Card.Attack)
			{
				return true;
			}
			bool enemyBetter = base.Util.IsAllEnemyBetter(false);
			return (base.Card.IsAttack() && enemyBetter) || (base.Card.IsDefense() && !enemyBetter && (base.Card.Attack >= base.Card.Defense || base.Card.Attack >= base.Util.GetBestPower(base.Enemy, false)));
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x00035060 File Offset: 0x00033260
		protected bool DefaultSpellWillBeNegated()
		{
			return ((base.Bot.HasInSpellZone(61740673, true, true) || base.Enemy.HasInSpellZone(61740673, true, false)) && !base.Util.ChainContainsCard(61740673)) || this.DefaultCheckWhetherCardIsNegated(base.Card);
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x000350B8 File Offset: 0x000332B8
		protected bool DefaultTrapWillBeNegated()
		{
			return ((base.Bot.HasInSpellZone(51452091, true, true) || base.Enemy.HasInSpellZone(51452091, true, false)) && !base.Util.ChainContainsCard(51452091)) || this.DefaultCheckWhetherCardIsNegated(base.Card);
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x0003510D File Offset: 0x0003330D
		protected bool DefaultSpellMustSetFirst()
		{
			return base.Bot.HasInSpellZone(58921041, true, true) || base.Enemy.HasInSpellZone(58921041, true, false);
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x00035138 File Offset: 0x00033338
		protected bool DefaultOnBecomeTarget()
		{
			if (base.Util.IsChainTarget(base.Card))
			{
				return true;
			}
			int[] destroyAllList = new int[] { 46772449, 73580471, 57774843, 72529749, 15693423, 90448279 };
			int[] destroyAllMonsterList = new int[] { 53129443, 99330325 };
			int[] destroyAllOpponentMonsterList = new int[] { 12580477 };
			int[] destroyAllOpponentSpellList = new int[] { 18144506, 2314238 };
			return base.Util.ChainContainsCard(destroyAllList) || (base.Enemy.HasInSpellZone(destroyAllOpponentSpellList, true, false) && base.Card.Location == CardLocation.SpellZone) || (base.Util.ChainContainsCard(destroyAllMonsterList) && base.Card.Location == CardLocation.MonsterZone) || (base.Duel.CurrentChain.Any((ClientCard c) => c.Controller == 1 && c.IsCode(destroyAllOpponentMonsterList)) && base.Card.Location == CardLocation.MonsterZone) || (this.lightningStormOption == 0 && base.Card.Location == CardLocation.MonsterZone && base.Card.IsAttack()) || (this.lightningStormOption == 1 && base.Card.Location == CardLocation.SpellZone);
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x0003526F File Offset: 0x0003346F
		protected bool DefaultTrap()
		{
			return !this.DefaultCheckWhetherCardIsNegated(base.Card) && ((base.Duel.LastChainPlayer == -1 && base.Duel.LastSummonPlayer != 0) || base.Duel.LastChainPlayer == 1);
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x000352AC File Offset: 0x000334AC
		protected bool DefaultUniqueTrap()
		{
			return !base.Util.HasChainedTrap(0) && this.UniqueFaceupSpell();
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x000352C4 File Offset: 0x000334C4
		protected bool UniqueFaceupSpell()
		{
			return !base.Bot.GetSpells().Any((ClientCard card) => card.IsCode(base.Card.Id) && card.IsFaceup());
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x000352E5 File Offset: 0x000334E5
		protected bool UniqueFaceupMonster()
		{
			return !base.Bot.GetMonsters().Any((ClientCard card) => card.IsCode(base.Card.Id) && card.IsFaceup());
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x00035306 File Offset: 0x00033506
		protected bool DefaultDontChainMyself()
		{
			return base.Type != ExecutorType.Activate || (!base.Executors.Any((CardExecutor exec) => exec.Type == base.Type && exec.CardId == base.Card.Id) && base.Duel.LastChainPlayer != 0);
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x0003533C File Offset: 0x0003353C
		protected bool DefaultChickenGame()
		{
			return base.Executors.Count((CardExecutor exec) => exec.Type == base.Type && exec.CardId == base.Card.Id) <= 1 && (base.Card.IsFacedown() || (base.Bot.LifePoints > 1000 && ((base.Bot.LifePoints <= base.Enemy.LifePoints && base.ActivateDescription == base.Util.GetStringId(67616300, 0)) || (base.Bot.LifePoints > base.Enemy.LifePoints && base.ActivateDescription == base.Util.GetStringId(67616300, 1)))));
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x000353EF File Offset: 0x000335EF
		protected bool DefaultAllureofDarkness()
		{
			return base.Bot.Hand.FirstOrDefault((ClientCard card) => card.HasAttribute(CardAttribute.Dark)) != null;
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x00035424 File Offset: 0x00033624
		protected bool DefaultDimensionalBarrier()
		{
			if (base.Duel.Player != 0)
			{
				List<ClientCard> monsters = base.Enemy.GetMonsters();
				int[] levels = new int[13];
				bool tuner = false;
				bool nontuner = false;
				foreach (ClientCard monster in monsters)
				{
					if (!monster.HasType((CardType)75497472))
					{
						if (monster.HasType(CardType.Tuner))
						{
							tuner = true;
						}
						else
						{
							nontuner = true;
						}
						if (!monster.HasType(CardType.Token))
						{
							levels[monster.Level] = levels[monster.Level] + 1;
						}
					}
					if (monster.IsOneForXyz())
					{
						base.AI.SelectOption(3);
						return true;
					}
				}
				if (tuner && nontuner)
				{
					base.AI.SelectOption(2);
					return true;
				}
				for (int i = 1; i <= 12; i++)
				{
					if (levels[i] > 1)
					{
						base.AI.SelectOption(3);
						return true;
					}
				}
				ClientCard j = base.Enemy.SpellZone[6];
				ClientCard r = base.Enemy.SpellZone[7];
				if (j != null && r != null && j.LScale != r.RScale)
				{
					base.AI.SelectOption(4);
					return true;
				}
			}
			IL_0130:
			ClientCard lastchaincard = base.Util.GetLastChainCard();
			if (base.Duel.LastChainPlayer == 1 && lastchaincard != null && !lastchaincard.IsDisabled() && (lastchaincard.HasType((CardType)6) || lastchaincard.Location == CardLocation.MonsterZone))
			{
				if (lastchaincard.HasType(CardType.Ritual))
				{
					base.AI.SelectOption(0);
					return true;
				}
				if (lastchaincard.HasType(CardType.Fusion))
				{
					base.AI.SelectOption(1);
					return true;
				}
				if (lastchaincard.HasType(CardType.Synchro))
				{
					base.AI.SelectOption(2);
					return true;
				}
				if (lastchaincard.HasType(CardType.Xyz))
				{
					base.AI.SelectOption(3);
					return true;
				}
				if (lastchaincard.IsFusionSpell())
				{
					base.AI.SelectOption(1);
					return true;
				}
			}
			if (base.Util.IsChainTarget(base.Card))
			{
				base.AI.SelectOption(3);
				return true;
			}
			return false;
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x00035658 File Offset: 0x00033858
		protected bool DefaultInterruptedKaijuSlumber()
		{
			if (base.Card.Location == CardLocation.Grave)
			{
				base.AI.SelectCard(new int[] { 55063751, 29726552, 36956512, 28674152, 93332803, 48770333, 63941210 });
				return true;
			}
			if (this.DefaultDarkHole())
			{
				base.AI.SelectCard(new int[] { 63941210, 48770333, 93332803, 28674152, 36956512, 29726552, 55063751 });
				base.AI.SelectNextCard(new int[] { 84769941, 55063751, 29726552, 36956512, 28674152, 93332803, 48770333 });
				return true;
			}
			return false;
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x000356D8 File Offset: 0x000338D8
		protected bool DefaultKaijuSpsummon()
		{
			IList<int> kaijus = new int[] { 63941210, 36956512, 55063751, 28674152, 29726552, 48770333, 93332803, 84769941 };
			foreach (ClientCard monster in base.Enemy.GetMonsters())
			{
				if (monster.IsCode(kaijus))
				{
					return base.Card.GetDefensePower() > monster.GetDefensePower();
				}
			}
			ClientCard card = base.Enemy.MonsterZone.GetFloodgate(false);
			if (card != null)
			{
				base.AI.SelectCard(card);
				return true;
			}
			card = base.Enemy.MonsterZone.GetDangerousMonster(false);
			if (card != null)
			{
				base.AI.SelectCard(card);
				return true;
			}
			card = base.Util.GetOneEnemyBetterThanValue(base.Card.GetDefensePower(), false, false);
			if (card != null)
			{
				base.AI.SelectCard(card);
				return true;
			}
			return false;
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x000357D4 File Offset: 0x000339D4
		protected bool DefaultNumberS39UtopiaTheLightningSummon()
		{
			int bestBotAttack = base.Util.GetBestAttack(base.Bot);
			return base.Util.IsOneEnemyBetterThanValue(bestBotAttack, false);
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x00035800 File Offset: 0x00033A00
		protected bool DefaultNumberS39UtopiaTheLightningEffect()
		{
			return base.Card.IsAttack() && base.Card.Attack < 5000 && (base.Enemy.BattlingMonster.IsAttack() || base.Enemy.BattlingMonster.IsFacedown() || base.Enemy.BattlingMonster.GetDefensePower() >= base.Card.Attack);
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x00035874 File Offset: 0x00033A74
		protected bool DefaultEvilswarmExcitonKnightSummon()
		{
			int num = base.Bot.GetMonsterCount() + base.Bot.GetSpellCount() + base.Bot.GetHandCount();
			int oppoCount = base.Enemy.GetMonsterCount() + base.Enemy.GetSpellCount() + base.Enemy.GetHandCount();
			return num - 1 < oppoCount && this.DefaultEvilswarmExcitonKnightEffect();
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x000358D8 File Offset: 0x00033AD8
		protected bool DefaultEvilswarmExcitonKnightEffect()
		{
			int num = base.Bot.GetMonsterCount() + base.Bot.GetSpellCount();
			int oppoCount = base.Enemy.GetMonsterCount() + base.Enemy.GetSpellCount();
			if (num < oppoCount)
			{
				return true;
			}
			int valueOrDefault = base.Bot.GetMonsters().Sum((ClientCard monster) => new int?(monster.GetDefensePower())).GetValueOrDefault();
			int oppoAttack = base.Enemy.GetMonsters().Sum((ClientCard monster) => new int?(monster.GetDefensePower())).GetValueOrDefault();
			return valueOrDefault < oppoAttack;
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x0003598C File Offset: 0x00033B8C
		protected bool DefaultStardustDragonSummon()
		{
			int bestAttack = base.Util.GetBestAttack(base.Bot);
			int oppoBestAttack = base.Util.GetBestPower(base.Enemy, false);
			return (bestAttack <= oppoBestAttack && oppoBestAttack <= 2500) || base.Util.IsTurn1OrMain2();
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x000359D5 File Offset: 0x00033BD5
		protected bool DefaultStardustDragonEffect()
		{
			return base.Card.Location == CardLocation.Grave || base.Duel.LastChainPlayer == 1;
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x000359F6 File Offset: 0x00033BF6
		protected bool DefaultCastelTheSkyblasterMusketeerSummon()
		{
			return base.Util.GetProblematicEnemyCard(0, false) != null;
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x00035A08 File Offset: 0x00033C08
		protected bool DefaultCastelTheSkyblasterMusketeerEffect()
		{
			if (base.ActivateDescription == base.Util.GetStringId(82633039, 0))
			{
				return false;
			}
			ClientCard target = base.Util.GetProblematicEnemyCard(0, false);
			if (target != null)
			{
				base.AI.SelectCard(0);
				base.AI.SelectNextCard(target);
				return true;
			}
			return false;
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x00035A5C File Offset: 0x00033C5C
		protected bool DefaultScarlightRedDragonArchfiendSummon()
		{
			int bestAttack = base.Util.GetBestAttack(base.Bot);
			int oppoBestAttack = base.Util.GetBestPower(base.Enemy, false);
			return (bestAttack <= oppoBestAttack && oppoBestAttack <= 3000) || this.DefaultScarlightRedDragonArchfiendEffect();
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x00035AA0 File Offset: 0x00033CA0
		protected bool DefaultTimelordSummon()
		{
			return base.Bot.GetMonsterCount() == 0;
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x00035AB0 File Offset: 0x00033CB0
		protected bool DefaultScarlightRedDragonArchfiendEffect()
		{
			int num = base.Bot.GetMonsters().Count((ClientCard monster) => !monster.Equals(base.Card) && monster.IsSpecialSummoned && monster.HasType(CardType.Effect) && monster.Attack <= base.Card.Attack);
			int oppoCount = base.Enemy.GetMonsters().Count((ClientCard monster) => monster.IsSpecialSummoned && monster.HasType(CardType.Effect) && monster.Attack <= base.Card.Attack);
			return (num <= oppoCount && oppoCount > 0) || oppoCount >= 3;
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x00035B08 File Offset: 0x00033D08
		protected bool DefaultHonestEffect()
		{
			if (base.Card.Location == CardLocation.Hand)
			{
				return !this.DefaultCheckWhetherCardIsNegated(base.Card) && base.Bot.BattlingMonster.IsAttack() && (base.Bot.BattlingMonster.Attack < base.Enemy.BattlingMonster.Attack || base.Bot.BattlingMonster.Attack >= base.Enemy.LifePoints || (base.Bot.BattlingMonster.Attack < base.Enemy.BattlingMonster.Defense && base.Bot.BattlingMonster.Attack + base.Enemy.BattlingMonster.Attack > base.Enemy.BattlingMonster.Defense));
			}
			return base.Util.IsTurn1OrMain2();
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x00035BF1 File Offset: 0x00033DF1
		protected bool DefaultVaylantzWorld_ShinraBansho()
		{
			return !this.DefaultSpellWillBeNegated();
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00035C00 File Offset: 0x00033E00
		protected bool DefaultVaylantzWorld_KonigWissen()
		{
			if (this.DefaultSpellWillBeNegated())
			{
				return false;
			}
			List<ClientCard> monsters = base.Enemy.GetMonsters();
			if (monsters.Count == 0)
			{
				return false;
			}
			List<ClientCard> targetList = new List<ClientCard>();
			List<ClientCard> floodgateCards = (from card in monsters
				where ((card != null) ? card.Data : null) != null && card.IsFloodgate() && card.IsFaceup() && !card.IsShouldNotBeTarget()
				orderby card.Attack descending
				select card).ToList<ClientCard>();
			List<ClientCard> dangerousCards = (from card in monsters
				where ((card != null) ? card.Data : null) != null && card.IsMonsterDangerous() && card.IsFaceup() && !card.IsShouldNotBeTarget()
				orderby card.Attack descending
				select card).ToList<ClientCard>();
			List<ClientCard> attackOrderedCards = (from card in monsters
				where ((card != null) ? card.Data : null) != null && card.HasType(CardType.Monster) && card.IsFaceup() && card.IsShouldNotBeTarget()
				orderby card.Attack descending
				select card).ToList<ClientCard>();
			targetList.AddRange(floodgateCards);
			targetList.AddRange(dangerousCards);
			targetList.AddRange(attackOrderedCards);
			if (targetList != null && targetList.Count > 0)
			{
				base.AI.SelectCard(targetList);
				return true;
			}
			return false;
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x00035D54 File Offset: 0x00033F54
		protected bool DefaultCheckWhetherCardIsNegated(ClientCard card)
		{
			if (card == null)
			{
				return true;
			}
			if (card.Data == null)
			{
				return card.IsDisabled();
			}
			int originId = card.Data.Alias;
			if (originId == 0)
			{
				originId = card.Data.Id;
			}
			return this.crossoutDesignatorIdList.Contains(originId) || (this.calledbytheGraveIdCountMap.ContainsKey(originId) && this.calledbytheGraveIdCountMap[originId] > 0) || (card.IsDisabled() && (card.Location & CardLocation.Onfield) > (CardLocation)0);
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x00035DD3 File Offset: 0x00033FD3
		protected bool DefaultCheckWhetherCardIdIsNegated(int cardId)
		{
			return this.crossoutDesignatorIdList.Contains(cardId) || (this.calledbytheGraveIdCountMap.ContainsKey(cardId) && this.calledbytheGraveIdCountMap[cardId] > 0);
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x00035E04 File Offset: 0x00034004
		protected int GetCalledbytheGraveIdCount(int cardId)
		{
			if (!this.calledbytheGraveIdCountMap.ContainsKey(cardId))
			{
				return 0;
			}
			return this.calledbytheGraveIdCountMap[cardId];
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x00035E24 File Offset: 0x00034024
		protected virtual bool DefaultSetForDiabellze()
		{
			if (base.Card == null)
			{
				return false;
			}
			if (base.Card.Id == 49238328)
			{
				return false;
			}
			if (base.Enemy.HasInMonstersZone(53765052, true, false, true) && base.Card.HasType(CardType.Spell) && !base.Card.HasType(CardType.QuickPlay))
			{
				if (base.Bot.SpellZone.Any((ClientCard c) => c != null && base.Duel.MainPhase.ActivableCards.Contains(c) && c.HasType(CardType.Spell) && !base.Card.HasType(CardType.QuickPlay) && c.IsFacedown()))
				{
					return false;
				}
				foreach (CardExecutor exec in base.Executors)
				{
					if (exec.Type == ExecutorType.Activate && exec.CardId == base.Card.Id && (exec.Func == null || exec.Func()))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x04000E50 RID: 3664
		protected int lightningStormOption = -1;

		// Token: 0x04000E51 RID: 3665
		private Dictionary<int, int> calledbytheGraveIdCountMap = new Dictionary<int, int>();

		// Token: 0x04000E52 RID: 3666
		private List<int> crossoutDesignatorIdList = new List<int>();

		// Token: 0x04000E53 RID: 3667
		protected Dictionary<int, Func<ClientCard, bool>> DefenderProtectRule;

		// Token: 0x04000E54 RID: 3668
		protected Dictionary<int, Func<ClientCard, List<ClientCard>, bool>> DefenderInvisbleRule;

		// Token: 0x02000231 RID: 561
		protected class _CardId
		{
			// Token: 0x04000E55 RID: 3669
			public const int JizukirutheStarDestroyingKaiju = 63941210;

			// Token: 0x04000E56 RID: 3670
			public const int ThunderKingtheLightningstrikeKaiju = 48770333;

			// Token: 0x04000E57 RID: 3671
			public const int DogorantheMadFlameKaiju = 93332803;

			// Token: 0x04000E58 RID: 3672
			public const int RadiantheMultidimensionalKaiju = 28674152;

			// Token: 0x04000E59 RID: 3673
			public const int GadarlatheMysteryDustKaiju = 36956512;

			// Token: 0x04000E5A RID: 3674
			public const int KumongoustheStickyStringKaiju = 29726552;

			// Token: 0x04000E5B RID: 3675
			public const int GamecieltheSeaTurtleKaiju = 55063751;

			// Token: 0x04000E5C RID: 3676
			public const int SuperAntiKaijuWarMachineMechaDogoran = 84769941;

			// Token: 0x04000E5D RID: 3677
			public const int SandaionTheTimelord = 33015627;

			// Token: 0x04000E5E RID: 3678
			public const int GabrionTheTimelord = 6616912;

			// Token: 0x04000E5F RID: 3679
			public const int MichionTheTimelord = 7733560;

			// Token: 0x04000E60 RID: 3680
			public const int ZaphionTheTimelord = 28929131;

			// Token: 0x04000E61 RID: 3681
			public const int HailonTheTimelord = 34137269;

			// Token: 0x04000E62 RID: 3682
			public const int RaphionTheTimelord = 60222213;

			// Token: 0x04000E63 RID: 3683
			public const int SadionTheTimelord = 65314286;

			// Token: 0x04000E64 RID: 3684
			public const int MetaionTheTimelord = 74530899;

			// Token: 0x04000E65 RID: 3685
			public const int KamionTheTimelord = 91712985;

			// Token: 0x04000E66 RID: 3686
			public const int LazionTheTimelord = 92435533;

			// Token: 0x04000E67 RID: 3687
			public const int LeftArmofTheForbiddenOne = 7902349;

			// Token: 0x04000E68 RID: 3688
			public const int RightLegofTheForbiddenOne = 8124921;

			// Token: 0x04000E69 RID: 3689
			public const int LeftLegofTheForbiddenOne = 44519536;

			// Token: 0x04000E6A RID: 3690
			public const int RightArmofTheForbiddenOne = 70903634;

			// Token: 0x04000E6B RID: 3691
			public const int ExodiaTheForbiddenOne = 33396948;

			// Token: 0x04000E6C RID: 3692
			public const int UltimateConductorTytanno = 18940556;

			// Token: 0x04000E6D RID: 3693
			public const int ElShaddollConstruct = 20366274;

			// Token: 0x04000E6E RID: 3694
			public const int AllyOfJusticeCatastor = 26593852;

			// Token: 0x04000E6F RID: 3695
			public const int DupeFrog = 46239604;

			// Token: 0x04000E70 RID: 3696
			public const int MaraudingCaptain = 2460565;

			// Token: 0x04000E71 RID: 3697
			public const int BlackRoseDragon = 73580471;

			// Token: 0x04000E72 RID: 3698
			public const int JudgmentDragon = 57774843;

			// Token: 0x04000E73 RID: 3699
			public const int TopologicTrisbaena = 72529749;

			// Token: 0x04000E74 RID: 3700
			public const int EvilswarmExcitonKnight = 46772449;

			// Token: 0x04000E75 RID: 3701
			public const int HarpiesFeatherDuster = 18144506;

			// Token: 0x04000E76 RID: 3702
			public const int DarkMagicAttack = 2314238;

			// Token: 0x04000E77 RID: 3703
			public const int MysticalSpaceTyphoon = 5318639;

			// Token: 0x04000E78 RID: 3704
			public const int CosmicCyclone = 8267140;

			// Token: 0x04000E79 RID: 3705
			public const int GalaxyCyclone = 5133471;

			// Token: 0x04000E7A RID: 3706
			public const int BookOfMoon = 14087893;

			// Token: 0x04000E7B RID: 3707
			public const int CompulsoryEvacuationDevice = 94192409;

			// Token: 0x04000E7C RID: 3708
			public const int CallOfTheHaunted = 97077563;

			// Token: 0x04000E7D RID: 3709
			public const int Scapegoat = 73915051;

			// Token: 0x04000E7E RID: 3710
			public const int BreakthroughSkill = 78474168;

			// Token: 0x04000E7F RID: 3711
			public const int SolemnJudgment = 41420027;

			// Token: 0x04000E80 RID: 3712
			public const int SolemnWarning = 84749824;

			// Token: 0x04000E81 RID: 3713
			public const int SolemnStrike = 40605147;

			// Token: 0x04000E82 RID: 3714
			public const int TorrentialTribute = 53582587;

			// Token: 0x04000E83 RID: 3715
			public const int EvenlyMatched = 15693423;

			// Token: 0x04000E84 RID: 3716
			public const int HeavyStorm = 19613556;

			// Token: 0x04000E85 RID: 3717
			public const int HammerShot = 26412047;

			// Token: 0x04000E86 RID: 3718
			public const int DarkHole = 53129443;

			// Token: 0x04000E87 RID: 3719
			public const int Raigeki = 12580477;

			// Token: 0x04000E88 RID: 3720
			public const int SmashingGround = 97169186;

			// Token: 0x04000E89 RID: 3721
			public const int PotOfDesires = 35261759;

			// Token: 0x04000E8A RID: 3722
			public const int AllureofDarkness = 1475311;

			// Token: 0x04000E8B RID: 3723
			public const int DimensionalBarrier = 83326048;

			// Token: 0x04000E8C RID: 3724
			public const int InterruptedKaijuSlumber = 99330325;

			// Token: 0x04000E8D RID: 3725
			public const int ChickenGame = 67616300;

			// Token: 0x04000E8E RID: 3726
			public const int SantaClaws = 46565218;

			// Token: 0x04000E8F RID: 3727
			public const int CastelTheSkyblasterMusketeer = 82633039;

			// Token: 0x04000E90 RID: 3728
			public const int CrystalWingSynchroDragon = 50954680;

			// Token: 0x04000E91 RID: 3729
			public const int NumberS39UtopiaTheLightning = 56832966;

			// Token: 0x04000E92 RID: 3730
			public const int Number39Utopia = 84013237;

			// Token: 0x04000E93 RID: 3731
			public const int UltimayaTzolkin = 1686814;

			// Token: 0x04000E94 RID: 3732
			public const int MekkKnightCrusadiaAstram = 21887175;

			// Token: 0x04000E95 RID: 3733
			public const int HamonLordofStrikingThunder = 32491822;

			// Token: 0x04000E96 RID: 3734
			public const int MoonMirrorShield = 19508728;

			// Token: 0x04000E97 RID: 3735
			public const int PhantomKnightsFogBlade = 25542642;

			// Token: 0x04000E98 RID: 3736
			public const int VampireFraeulein = 6039967;

			// Token: 0x04000E99 RID: 3737
			public const int InjectionFairyLily = 79575620;

			// Token: 0x04000E9A RID: 3738
			public const int BlueEyesChaosMAXDragon = 55410871;

			// Token: 0x04000E9B RID: 3739
			public const int AshBlossom = 14558127;

			// Token: 0x04000E9C RID: 3740
			public const int MaxxC = 23434538;

			// Token: 0x04000E9D RID: 3741
			public const int LockBird = 94145021;

			// Token: 0x04000E9E RID: 3742
			public const int GhostOgreAndSnowRabbit = 59438930;

			// Token: 0x04000E9F RID: 3743
			public const int GhostBelle = 73642296;

			// Token: 0x04000EA0 RID: 3744
			public const int EffectVeiler = 97268402;

			// Token: 0x04000EA1 RID: 3745
			public const int GhostMournerMoonlitChill = 52038441;

			// Token: 0x04000EA2 RID: 3746
			public const int ArtifactLancea = 34267821;

			// Token: 0x04000EA3 RID: 3747
			public const int DimensionShifter = 91800273;

			// Token: 0x04000EA4 RID: 3748
			public const int NibiruThePrimalBeing = 27204311;

			// Token: 0x04000EA5 RID: 3749
			public const int MulcharmyPurulia = 84192580;

			// Token: 0x04000EA6 RID: 3750
			public const int MulcharmyFuwalos = 42141493;

			// Token: 0x04000EA7 RID: 3751
			public const int MulcharmyNyalus = 87126721;

			// Token: 0x04000EA8 RID: 3752
			public const int CalledByTheGrave = 24224830;

			// Token: 0x04000EA9 RID: 3753
			public const int CrossoutDesignator = 65681983;

			// Token: 0x04000EAA RID: 3754
			public const int InfiniteImpermanence = 10045474;

			// Token: 0x04000EAB RID: 3755
			public const int GalaxySoldier = 46659709;

			// Token: 0x04000EAC RID: 3756
			public const int MacroCosmos = 30241314;

			// Token: 0x04000EAD RID: 3757
			public const int UpstartGoblin = 70368879;

			// Token: 0x04000EAE RID: 3758
			public const int CyberEmergency = 60600126;

			// Token: 0x04000EAF RID: 3759
			public const int TheAgentOfCreationVenus = 64734921;

			// Token: 0x04000EB0 RID: 3760
			public const int EaterOfMillions = 63845230;

			// Token: 0x04000EB1 RID: 3761
			public const int InvokedPurgatrio = 12307878;

			// Token: 0x04000EB2 RID: 3762
			public const int ChaosAncientGearGiant = 51788412;

			// Token: 0x04000EB3 RID: 3763
			public const int UltimateAncientGearGolem = 12652643;

			// Token: 0x04000EB4 RID: 3764
			public const int RedDragonArchfiend = 70902743;

			// Token: 0x04000EB5 RID: 3765
			public const int ImperialOrder = 61740673;

			// Token: 0x04000EB6 RID: 3766
			public const int RoyalDecreel = 51452091;

			// Token: 0x04000EB7 RID: 3767
			public const int NaturalExterio = 99916754;

			// Token: 0x04000EB8 RID: 3768
			public const int NaturiaBeast = 33198837;

			// Token: 0x04000EB9 RID: 3769
			public const int SwordsmanLV7 = 37267041;

			// Token: 0x04000EBA RID: 3770
			public const int AntiSpellFragrance = 58921041;

			// Token: 0x04000EBB RID: 3771
			public const int Number41BagooskatheTerriblyTiredTapir = 90590303;

			// Token: 0x04000EBC RID: 3772
			public const int SkillDrain = 82732705;

			// Token: 0x04000EBD RID: 3773
			public const int DimensionalFissure = 81674782;

			// Token: 0x04000EBE RID: 3774
			public const int BanisheroftheRadiance = 94853057;

			// Token: 0x04000EBF RID: 3775
			public const int BanisheroftheLight = 61528025;

			// Token: 0x04000EC0 RID: 3776
			public const int KashtiraAriseHeart = 48626373;

			// Token: 0x04000EC1 RID: 3777
			public const int MaskedHERODarkLaw = 58481572;

			// Token: 0x04000EC2 RID: 3778
			public const int VaylantzWorld_ShinraBansho = 49568943;

			// Token: 0x04000EC3 RID: 3779
			public const int VaylantzWorld_KonigWissen = 75952542;

			// Token: 0x04000EC4 RID: 3780
			public const int DivineArsenalAAZEUS_SkyThunder = 90448279;

			// Token: 0x04000EC5 RID: 3781
			public const int LightningStorm = 14532163;

			// Token: 0x04000EC6 RID: 3782
			public const int BelialMarquisOfDarkness = 33655493;

			// Token: 0x04000EC7 RID: 3783
			public const int ChirubiméPrincessOfAutumnLeaves = 87294988;

			// Token: 0x04000EC8 RID: 3784
			public const int PerformapalBarokuriboh = 19050066;

			// Token: 0x04000EC9 RID: 3785
			public const int LabrynthArchfiend = 48745395;

			// Token: 0x04000ECA RID: 3786
			public const int HarpiesPetDragonFearsomeFireBlast = 4991081;

			// Token: 0x04000ECB RID: 3787
			public const int DynaHeroFurHire = 25123713;

			// Token: 0x04000ECC RID: 3788
			public const int Hieracosphinx = 82260502;

			// Token: 0x04000ECD RID: 3789
			public const int SpeedroidPassinglider = 26420373;

			// Token: 0x04000ECE RID: 3790
			public const int TyrOfTheNordicChampions = 2333365;

			// Token: 0x04000ECF RID: 3791
			public const int ValkyrianKnight = 99348756;

			// Token: 0x04000ED0 RID: 3792
			public const int Victoria = 75162696;

			// Token: 0x04000ED1 RID: 3793
			public const int MadolcheChouxvalier = 75363626;

			// Token: 0x04000ED2 RID: 3794
			public const int LadyOfD = 67511500;

			// Token: 0x04000ED3 RID: 3795
			public const int MermailAbysslung = 95466842;

			// Token: 0x04000ED4 RID: 3796
			public const int HarpiesPetBabyDragon = 6924874;

			// Token: 0x04000ED5 RID: 3797
			public const int HandHoldingGenie = 94535485;

			// Token: 0x04000ED6 RID: 3798
			public const int GolemDragon = 9666558;

			// Token: 0x04000ED7 RID: 3799
			public const int TwilightRoseKnight = 2986553;

			// Token: 0x04000ED8 RID: 3800
			public const int PerformapalThunderhino = 70458081;

			// Token: 0x04000ED9 RID: 3801
			public const int MiracleFlipper = 131182;

			// Token: 0x04000EDA RID: 3802
			public const int Decoyroid = 25034083;

			// Token: 0x04000EDB RID: 3803
			public const int AltergeistFifinellag = 12977245;

			// Token: 0x04000EDC RID: 3804
			public const int BatterymanD = 55401221;

			// Token: 0x04000EDD RID: 3805
			public const int Watthopper = 61380658;

			// Token: 0x04000EDE RID: 3806
			public const int EgyptianGodSlime = 42166000;

			// Token: 0x04000EDF RID: 3807
			public const int DinowrestlerChimeraTWrextle = 22900219;

			// Token: 0x04000EE0 RID: 3808
			public const int DinowrestlerGigaSpinosavate = 58672736;

			// Token: 0x04000EE1 RID: 3809
			public const int ScarredWarrior = 45298492;

			// Token: 0x04000EE2 RID: 3810
			public const int SharkFortress = 50449881;

			// Token: 0x04000EE3 RID: 3811
			public const int HeroicChampionClaivesolish = 97453744;

			// Token: 0x04000EE4 RID: 3812
			public const int GhostrickAlucard = 75367227;

			// Token: 0x04000EE5 RID: 3813
			public const int DinowrestlerKingTWrextle = 77967790;

			// Token: 0x04000EE6 RID: 3814
			public const int NumberF0UtopicFutureZexal = 41522092;

			// Token: 0x04000EE7 RID: 3815
			public const int PerformapalMissDirector = 92932860;

			// Token: 0x04000EE8 RID: 3816
			public const int AncientWarriorsMasterfulSunMou = 40140448;

			// Token: 0x04000EE9 RID: 3817
			public const int AncientWarriorsVirtuousLiuXuan = 40428851;

			// Token: 0x04000EEA RID: 3818
			public const int CommandKnight = 10375182;

			// Token: 0x04000EEB RID: 3819
			public const int HunterOwl = 51962254;

			// Token: 0x04000EEC RID: 3820
			public const int RokketRecharger = 5969957;

			// Token: 0x04000EED RID: 3821
			public const int EmissaryOfTheOasis = 6103294;

			// Token: 0x04000EEE RID: 3822
			public const int Zuttomozaurus = 24454387;

			// Token: 0x04000EEF RID: 3823
			public const int Otoshidamashi = 14957440;

			// Token: 0x04000EF0 RID: 3824
			public const int NaturiaMosquito = 17285476;

			// Token: 0x04000EF1 RID: 3825
			public const int RescueACEHydrant = 37617348;

			// Token: 0x04000EF2 RID: 3826
			public const int MeizenTheBattleNinja = 11825276;

			// Token: 0x04000EF3 RID: 3827
			public const int VindikiteRGenex = 73483491;

			// Token: 0x04000EF4 RID: 3828
			public const int PrincessCologne = 75574498;

			// Token: 0x04000EF5 RID: 3829
			public const int Number48ShadowLich = 1426714;

			// Token: 0x04000EF6 RID: 3830
			public const int PhantomToken = 1426715;

			// Token: 0x04000EF7 RID: 3831
			public const int DuelLinkDragonTheDuelDragon = 60025883;

			// Token: 0x04000EF8 RID: 3832
			public const int DuelDragonToken = 60025884;

			// Token: 0x04000EF9 RID: 3833
			public const int SeleneQueenOfTheMasterMagicians = 45819647;

			// Token: 0x04000EFA RID: 3834
			public const int TheWingedDragonofRaSphereMode = 10000080;

			// Token: 0x04000EFB RID: 3835
			public const int SelettriceVaalmonica = 23093373;

			// Token: 0x04000EFC RID: 3836
			public const int PerformageTrapezeWitch = 33206889;

			// Token: 0x04000EFD RID: 3837
			public const int PoseidraTheStormingAtlantean = 99193444;

			// Token: 0x04000EFE RID: 3838
			public const int RockOfTheVanquisher = 28168628;

			// Token: 0x04000EFF RID: 3839
			public const int SpiralDischarge = 29477860;

			// Token: 0x04000F00 RID: 3840
			public const int GaiaTheDragonChampion = 66889139;

			// Token: 0x04000F01 RID: 3841
			public const int CrusadiaVanguard = 55312487;

			// Token: 0x04000F02 RID: 3842
			public const int GladiatorBeastDomitianus = 33652635;

			// Token: 0x04000F03 RID: 3843
			public const int PatricianOfDarkness = 19153634;

			// Token: 0x04000F04 RID: 3844
			public const int DictatorOfD = 66961194;

			// Token: 0x04000F05 RID: 3845
			public const int LoThePrayersOfTheVoicelessVoice = 25801745;

			// Token: 0x04000F06 RID: 3846
			public const int BarrierOfTheVoicelessVoice = 98477480;

			// Token: 0x04000F07 RID: 3847
			public const int DiabellzeOfTheOriginalSin = 53765052;

			// Token: 0x04000F08 RID: 3848
			public const int PotOfExtravagance = 49238328;
		}

		// Token: 0x02000232 RID: 562
		protected class _Setcode
		{
			// Token: 0x04000F09 RID: 3849
			public const int Watt = 14;

			// Token: 0x04000F0A RID: 3850
			public const int Speedroid = 8214;

			// Token: 0x04000F0B RID: 3851
			public const int EarthboundImmortal = 4129;

			// Token: 0x04000F0C RID: 3852
			public const int Naturia = 42;

			// Token: 0x04000F0D RID: 3853
			public const int Nordic = 66;

			// Token: 0x04000F0E RID: 3854
			public const int Harpie = 100;

			// Token: 0x04000F0F RID: 3855
			public const int Madolche = 113;

			// Token: 0x04000F10 RID: 3856
			public const int Ghostrick = 141;

			// Token: 0x04000F11 RID: 3857
			public const int OddEyes = 153;

			// Token: 0x04000F12 RID: 3858
			public const int Performapal = 159;

			// Token: 0x04000F13 RID: 3859
			public const int Performage = 198;

			// Token: 0x04000F14 RID: 3860
			public const int BlueEyes = 221;

			// Token: 0x04000F15 RID: 3861
			public const int FurHire = 276;

			// Token: 0x04000F16 RID: 3862
			public const int Altergeist = 259;

			// Token: 0x04000F17 RID: 3863
			public const int Crusadia = 278;

			// Token: 0x04000F18 RID: 3864
			public const int Danger = 286;

			// Token: 0x04000F19 RID: 3865
			public const int Endymion = 298;

			// Token: 0x04000F1A RID: 3866
			public const int AncientWarriors = 311;

			// Token: 0x04000F1B RID: 3867
			public const int RescueACE = 395;

			// Token: 0x04000F1C RID: 3868
			public const int VanquishSoul = 405;

			// Token: 0x04000F1D RID: 3869
			public const int Horus = 413;
		}
	}
}
