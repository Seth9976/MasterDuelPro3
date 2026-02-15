using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using WindBot.Game.AI;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game
{
	// Token: 0x020001FC RID: 508
	public class GameAI
	{
		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x0002A6F7 File Offset: 0x000288F7
		// (set) Token: 0x06000A08 RID: 2568 RVA: 0x0002A6FF File Offset: 0x000288FF
		public GameClient Game { get; private set; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x0002A708 File Offset: 0x00028908
		// (set) Token: 0x06000A0A RID: 2570 RVA: 0x0002A710 File Offset: 0x00028910
		public Duel Duel { get; private set; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x0002A719 File Offset: 0x00028919
		// (set) Token: 0x06000A0C RID: 2572 RVA: 0x0002A721 File Offset: 0x00028921
		public Executor Executor { get; set; }

		// Token: 0x06000A0D RID: 2573 RVA: 0x0002A72C File Offset: 0x0002892C
		public GameAI(GameClient game, Duel duel)
		{
			this.Game = game;
			this.Duel = duel;
			this._dialogs = new Dialogs(game);
			this._activatedCards = new Dictionary<int, int>();
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0002A798 File Offset: 0x00028998
		private void CheckSurrender()
		{
			foreach (CardExecutor exec in this.Executor.Executors)
			{
				if (exec.Type == ExecutorType.Surrender && exec.Func())
				{
					this._dialogs.SendSurrender();
					this.Game.Surrender();
				}
			}
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0002A810 File Offset: 0x00028A10
		public void OnRetry()
		{
			this._dialogs.SendSorry();
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0002A81D File Offset: 0x00028A1D
		public void OnDeckError(string card)
		{
			this._dialogs.SendDeckSorry(card);
			Thread.Sleep(1000);
			this._dialogs.SendSurrender();
			this.Game.Connection.Close(null);
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0002A851 File Offset: 0x00028A51
		public void OnJoinGame()
		{
			this._dialogs.SendWelcome();
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0002A85E File Offset: 0x00028A5E
		public void OnStart()
		{
			this._dialogs.SendDuelStart();
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0002A86B File Offset: 0x00028A6B
		public void SendCustomChat(int index, params object[] opts)
		{
			this._dialogs.SendCustomChat(index, opts);
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0002A87A File Offset: 0x00028A7A
		public int OnRockPaperScissors()
		{
			return this.Executor.OnRockPaperScissors();
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0002A887 File Offset: 0x00028A87
		public bool OnSelectHand()
		{
			return this.Executor.OnSelectHand();
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0002A894 File Offset: 0x00028A94
		public void OnDraw(int player)
		{
			this.Executor.OnDraw(player);
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0002A8A2 File Offset: 0x00028AA2
		public void OnNewTurn()
		{
			this._activatedCards.Clear();
			this.Executor.OnNewTurn();
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0002A8BC File Offset: 0x00028ABC
		public void OnNewPhase()
		{
			this.m_selector.Clear();
			this.m_position.Clear();
			this.m_selector_pointer = -1;
			this.m_materialSelector = null;
			this.m_materialSelectorHint = 0;
			this.m_option = -1;
			this.m_yesno = -1;
			this.m_announce = 0;
			this.m_place = 0;
			if (this.Duel.Player == 0 && this.Duel.Phase == DuelPhase.Draw)
			{
				this._dialogs.SendNewTurn();
			}
			this.Executor.OnNewPhase();
			this.CheckSurrender();
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0002A947 File Offset: 0x00028B47
		public void OnMove(ClientCard card, int previousControler, int previousLocation, int currentControler, int currentLocation)
		{
			this.Executor.OnMove(card, previousControler, previousLocation, currentControler, currentLocation);
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0002A95B File Offset: 0x00028B5B
		public void OnDirectAttack(ClientCard card)
		{
			this._dialogs.SendOnDirectAttack(card.Name);
			this.CheckSurrender();
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x0002A974 File Offset: 0x00028B74
		public void OnChaining(ClientCard card, int player)
		{
			this.Executor.OnChaining(player, card);
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0002A983 File Offset: 0x00028B83
		public void OnChainSolved(int chainIndex)
		{
			this.Executor.OnChainSolved(chainIndex);
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0002A991 File Offset: 0x00028B91
		public void OnSpSummoned()
		{
			this.Executor.OnSpSummoned();
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0002A99E File Offset: 0x00028B9E
		public void OnChainEnd()
		{
			this.m_selector.Clear();
			this.m_selector_pointer = -1;
			this.Executor.OnChainEnd();
			this.CheckSurrender();
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0002A9C3 File Offset: 0x00028BC3
		public void OnReceivingAnnouce(int player, int data)
		{
			this.Executor.OnReceivingAnnouce(player, data);
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0002A9D4 File Offset: 0x00028BD4
		public BattlePhaseAction OnSelectBattleCmd(BattlePhase battle)
		{
			foreach (CardExecutor exec in this.Executor.Executors)
			{
				if (exec.Type == ExecutorType.GoToMainPhase2 && battle.CanMainPhaseTwo && exec.Func())
				{
					return this.ToMainPhase2();
				}
				if (exec.Type == ExecutorType.GoToEndPhase && battle.CanEndPhase && exec.Func())
				{
					return this.ToEndPhase();
				}
				for (int i = 0; i < battle.ActivableCards.Count; i++)
				{
					ClientCard card = battle.ActivableCards[i];
					if (this.ShouldExecute(exec, card, ExecutorType.Activate, battle.ActivableDescs[i], -1))
					{
						this._dialogs.SendChaining(card.Name);
						return new BattlePhaseAction(BattlePhaseAction.BattleAction.Activate, card.ActionIndex);
					}
				}
			}
			List<ClientCard> attackers = new List<ClientCard>(battle.AttackableCards);
			attackers.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			attackers.Reverse();
			List<ClientCard> defenders = new List<ClientCard>(this.Duel.Fields[1].GetMonsters());
			defenders.Sort(new Comparison<ClientCard>(CardContainer.CompareDefensePower));
			defenders.Reverse();
			ClientCard selected = this.Executor.OnSelectAttacker(attackers, defenders);
			if (selected != null && attackers.Contains(selected))
			{
				attackers.Remove(selected);
				attackers.Insert(0, selected);
			}
			BattlePhaseAction result = this.Executor.OnBattle(attackers, defenders);
			if (result != null)
			{
				return result;
			}
			if (attackers.Count == 0)
			{
				return this.ToMainPhase2();
			}
			if (defenders.Count == 0)
			{
				ClientCard attacker = attackers[attackers.Count - 1];
				return this.Attack(attacker, null);
			}
			for (int j = 0; j < attackers.Count; j++)
			{
				ClientCard attacker2 = attackers[j];
				attacker2.IsLastAttacker = j == attackers.Count - 1;
				result = this.Executor.OnSelectAttackTarget(attacker2, defenders);
				if (result != null)
				{
					return result;
				}
			}
			if (!battle.CanMainPhaseTwo)
			{
				return this.Attack(attackers[0], (defenders.Count == 0) ? null : defenders[0]);
			}
			return this.ToMainPhase2();
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0002AC2C File Offset: 0x00028E2C
		public IList<ClientCard> OnSelectCard(IList<ClientCard> cards, int min, int max, int hint, bool cancelable)
		{
			IList<ClientCard> result = this.Executor.OnSelectCard(cards, min, max, hint, cancelable);
			if (result != null)
			{
				return result;
			}
			if (hint == 509 && min == 1 && max > min)
			{
				result = this.Executor.OnSelectPendulumSummon(cards, max);
				if (result != null)
				{
					return result;
				}
			}
			CardSelector selector;
			if (hint == 511 || hint == 512 || hint == 513 || hint == 533)
			{
				if (this.m_materialSelector != null)
				{
					selector = this.m_materialSelector;
				}
				else
				{
					if (hint == 511)
					{
						result = this.Executor.OnSelectFusionMaterial(cards, min, max);
					}
					if (hint == 512)
					{
						result = this.Executor.OnSelectSynchroMaterial(cards, 0, min, max);
					}
					if (hint == 513)
					{
						result = this.Executor.OnSelectXyzMaterial(cards, min, max);
					}
					if (hint == 533)
					{
						result = this.Executor.OnSelectLinkMaterial(cards, min, max);
					}
					if (result != null)
					{
						return result;
					}
					selector = this.GetSelectedCards();
				}
			}
			else if (this.m_materialSelector != null && hint == this.m_materialSelectorHint)
			{
				selector = this.m_materialSelector;
			}
			else
			{
				selector = this.GetSelectedCards();
			}
			if (selector != null)
			{
				return selector.Select(cards, min, max);
			}
			IList<ClientCard> selected = new List<ClientCard>();
			if (hint == 549 && cancelable)
			{
				return selected;
			}
			if (cards.Count >= min)
			{
				for (int i = 0; i < min; i++)
				{
					selected.Add(cards[i]);
				}
			}
			return selected;
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0002AD8C File Offset: 0x00028F8C
		public int OnSelectChain(IList<ClientCard> cards, IList<int> descs, IList<bool> forces, int timing = -1)
		{
			this.Executor.OnSelectChain(cards);
			foreach (CardExecutor exec in this.Executor.Executors)
			{
				for (int i = 0; i < cards.Count; i++)
				{
					ClientCard card = cards[i];
					if (this.ShouldExecute(exec, card, ExecutorType.Activate, descs[i], timing))
					{
						this._dialogs.SendChaining(card.Name);
						return i;
					}
				}
			}
			for (int j = 0; j < forces.Count; j++)
			{
				if (forces[j])
				{
					this._dialogs.SendChaining(cards[j].Name);
					return j;
				}
			}
			return -1;
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0002AE64 File Offset: 0x00029064
		public IList<int> OnSelectCounter(int type, int quantity, IList<ClientCard> cards, IList<int> counters)
		{
			int[] used = new int[counters.Count];
			int i = 0;
			while (quantity > 0)
			{
				if (counters[i] >= quantity)
				{
					used[i] = quantity;
					quantity = 0;
				}
				else
				{
					used[i] = counters[i];
					quantity -= counters[i];
				}
				i++;
			}
			return used;
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0002AEB8 File Offset: 0x000290B8
		public IList<ClientCard> OnCardSorting(IList<ClientCard> cards)
		{
			IList<ClientCard> result = this.Executor.OnCardSorting(cards);
			if (result != null)
			{
				return result;
			}
			result = new List<ClientCard>();
			return cards.ToList<ClientCard>();
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0002AEE8 File Offset: 0x000290E8
		public bool OnSelectEffectYn(ClientCard card, int desc)
		{
			foreach (CardExecutor exec in this.Executor.Executors)
			{
				if (this.ShouldExecute(exec, card, ExecutorType.Activate, desc, -1))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0002AF48 File Offset: 0x00029148
		public MainPhaseAction OnSelectIdleCmd(MainPhase main)
		{
			this.CheckSurrender();
			foreach (CardExecutor exec in this.Executor.Executors)
			{
				if (exec.Type == ExecutorType.GoToEndPhase && main.CanEndPhase && exec.Func())
				{
					this._dialogs.SendEndTurn();
					return new MainPhaseAction(MainPhaseAction.MainAction.ToEndPhase);
				}
				if (exec.Type == ExecutorType.GoToBattlePhase && main.CanBattlePhase && exec.Func())
				{
					return new MainPhaseAction(MainPhaseAction.MainAction.ToBattlePhase);
				}
				for (int i = 0; i < main.ActivableCards.Count; i++)
				{
					ClientCard card = main.ActivableCards[i];
					if (this.ShouldExecute(exec, card, ExecutorType.Activate, main.ActivableDescs[i], -1))
					{
						this._dialogs.SendActivate(card.Name);
						return new MainPhaseAction(MainPhaseAction.MainAction.Activate, card.ActionActivateIndex[main.ActivableDescs[i]]);
					}
				}
				foreach (ClientCard card2 in main.MonsterSetableCards)
				{
					if (this.ShouldExecute(exec, card2, ExecutorType.MonsterSet, -1, -1))
					{
						this._dialogs.SendSetMonster();
						return new MainPhaseAction(MainPhaseAction.MainAction.SetMonster, card2.ActionIndex);
					}
				}
				foreach (ClientCard card3 in main.ReposableCards)
				{
					if (this.ShouldExecute(exec, card3, ExecutorType.Repos, -1, -1))
					{
						return new MainPhaseAction(MainPhaseAction.MainAction.Repos, card3.ActionIndex);
					}
				}
				foreach (ClientCard card4 in main.SpecialSummonableCards)
				{
					if (this.ShouldExecute(exec, card4, ExecutorType.SpSummon, -1, -1))
					{
						this._dialogs.SendSummon(card4.Name);
						return new MainPhaseAction(MainPhaseAction.MainAction.SpSummon, card4.ActionIndex);
					}
				}
				foreach (ClientCard card5 in main.SummonableCards)
				{
					if (this.ShouldExecute(exec, card5, ExecutorType.Summon, -1, -1))
					{
						this._dialogs.SendSummon(card5.Name);
						return new MainPhaseAction(MainPhaseAction.MainAction.Summon, card5.ActionIndex);
					}
					if (this.ShouldExecute(exec, card5, ExecutorType.SummonOrSet, -1, -1))
					{
						if (main.MonsterSetableCards.Contains(card5) && this.Executor.OnSelectMonsterSummonOrSet(card5))
						{
							this._dialogs.SendSetMonster();
							return new MainPhaseAction(MainPhaseAction.MainAction.SetMonster, card5.ActionIndex);
						}
						this._dialogs.SendSummon(card5.Name);
						return new MainPhaseAction(MainPhaseAction.MainAction.Summon, card5.ActionIndex);
					}
				}
				foreach (ClientCard card6 in main.SpellSetableCards)
				{
					if (this.ShouldExecute(exec, card6, ExecutorType.SpellSet, -1, -1))
					{
						return new MainPhaseAction(MainPhaseAction.MainAction.SetSpell, card6.ActionIndex);
					}
				}
			}
			if (main.CanBattlePhase && this.Duel.Fields[0].HasAttackingMonster())
			{
				return new MainPhaseAction(MainPhaseAction.MainAction.ToBattlePhase);
			}
			this._dialogs.SendEndTurn();
			return new MainPhaseAction(MainPhaseAction.MainAction.ToEndPhase);
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0002B360 File Offset: 0x00029560
		public int OnSelectOption(IList<int> options)
		{
			int result = this.Executor.OnSelectOption(options);
			if (result != -1)
			{
				return result;
			}
			if (this.m_option != -1 && this.m_option < options.Count)
			{
				return this.m_option;
			}
			return 0;
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0002B3A0 File Offset: 0x000295A0
		public int OnSelectPlace(int cardId, int player, CardLocation location, int available)
		{
			int selector_selected = this.m_place;
			this.m_place = 0;
			int executor_selected = this.Executor.OnSelectPlace(cardId, player, location, available);
			if ((executor_selected & available) > 0)
			{
				return executor_selected & available;
			}
			if ((selector_selected & available) > 0)
			{
				return selector_selected & available;
			}
			return 0;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0002B3E8 File Offset: 0x000295E8
		public CardPosition OnSelectPosition(int cardId, IList<CardPosition> positions)
		{
			CardPosition selector_selected = this.GetSelectedPosition();
			CardPosition executor_selected = this.Executor.OnSelectPosition(cardId, positions);
			if (positions.Contains(executor_selected))
			{
				return executor_selected;
			}
			if (positions.Contains(selector_selected))
			{
				return selector_selected;
			}
			return positions[0];
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0002B428 File Offset: 0x00029628
		public IList<ClientCard> OnSelectSum(IList<ClientCard> cards, int sum, int min, int max, int hint, bool mode)
		{
			IList<ClientCard> selected = this.Executor.OnSelectSum(cards, sum, min, max, hint, mode);
			if (selected != null)
			{
				return selected;
			}
			if (hint == 500 || hint == 512)
			{
				if (this.m_materialSelector != null)
				{
					selected = this.m_materialSelector.Select(cards, min, max);
				}
				else if (hint != 500)
				{
					if (hint == 512)
					{
						selected = this.Executor.OnSelectSynchroMaterial(cards, sum, min, max);
					}
				}
				else
				{
					selected = this.Executor.OnSelectRitualTribute(cards, sum, min, max);
				}
				if (selected != null)
				{
					int s = 0;
					int s2 = 0;
					foreach (ClientCard card in selected)
					{
						s += card.OpParam1;
						s2 += ((card.OpParam2 != 0) ? card.OpParam2 : card.OpParam1);
					}
					if ((mode && (s == sum || s2 == sum)) || (!mode && (s >= sum || s2 >= sum)))
					{
						return selected;
					}
				}
			}
			if (mode)
			{
				if (sum == 0 && min == 0)
				{
					return new List<ClientCard>();
				}
				if (min <= 1)
				{
					foreach (ClientCard card2 in cards)
					{
						if (card2.OpParam2 == sum)
						{
							return new ClientCard[] { card2 };
						}
					}
					foreach (ClientCard card3 in cards)
					{
						if (card3.OpParam1 == sum)
						{
							return new ClientCard[] { card3 };
						}
					}
				}
				int s3 = 0;
				int s4 = 0;
				foreach (ClientCard card4 in cards)
				{
					s3 += card4.OpParam1;
					s4 += ((card4.OpParam2 != 0) ? card4.OpParam2 : card4.OpParam1);
				}
				if (s3 == sum || s4 == sum)
				{
					return cards;
				}
				for (int i = ((min <= 1) ? 2 : min); i <= max; i++)
				{
					if (i > cards.Count)
					{
						break;
					}
					foreach (IEnumerable<ClientCard> combo in cards.GetCombinations(i))
					{
						Logger.DebugWriteLine("--");
						s3 = 0;
						s4 = 0;
						foreach (ClientCard card5 in combo)
						{
							s3 += card5.OpParam1;
							s4 += ((card5.OpParam2 != 0) ? card5.OpParam2 : card5.OpParam1);
						}
						if (s3 == sum || s4 == sum)
						{
							return combo.ToList<ClientCard>();
						}
					}
				}
			}
			else
			{
				if (min <= 1)
				{
					foreach (ClientCard card6 in cards)
					{
						if (card6.OpParam2 >= sum)
						{
							return new ClientCard[] { card6 };
						}
					}
					foreach (ClientCard card7 in cards)
					{
						if (card7.OpParam1 >= sum)
						{
							return new ClientCard[] { card7 };
						}
					}
				}
				int j = ((min <= 1) ? 2 : min);
				while (j <= max && j <= cards.Count)
				{
					foreach (IEnumerable<ClientCard> combo2 in cards.GetCombinations(j))
					{
						Logger.DebugWriteLine("----");
						int s5 = 0;
						int s6 = 0;
						foreach (ClientCard card8 in combo2)
						{
							s5 += card8.OpParam1;
							s6 += ((card8.OpParam2 != 0) ? card8.OpParam2 : card8.OpParam1);
						}
						if (s5 >= sum || s6 >= sum)
						{
							return combo2.ToList<ClientCard>();
						}
					}
					j++;
				}
			}
			Logger.WriteErrorLine("Fail to select sum.");
			return new List<ClientCard>();
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0002B8E4 File Offset: 0x00029AE4
		public IList<ClientCard> OnSelectTribute(IList<ClientCard> cards, int min, int max, int hint, bool cancelable)
		{
			List<ClientCard> sorted = new List<ClientCard>();
			sorted.AddRange(cards);
			sorted.Sort(new Comparison<ClientCard>(CardContainer.CompareCardAttack));
			IList<ClientCard> selected = new List<ClientCard>();
			int i = 0;
			while (i < min && i < sorted.Count)
			{
				selected.Add(sorted[i]);
				i++;
			}
			return selected;
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0002B939 File Offset: 0x00029B39
		public bool OnSelectYesNo(int desc)
		{
			if (this.m_yesno != -1)
			{
				return this.m_yesno > 0;
			}
			return this.Executor.OnSelectYesNo(desc);
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0002B95A File Offset: 0x00029B5A
		public bool OnSelectBattleReplay()
		{
			return this.Executor.OnSelectBattleReplay();
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0002B968 File Offset: 0x00029B68
		public int OnAnnounceCard(IList<int> avail)
		{
			int selected = this.Executor.OnAnnounceCard(avail);
			if (avail.Contains(selected))
			{
				return selected;
			}
			if (avail.Contains(this.m_announce))
			{
				return this.m_announce;
			}
			if (this.m_announce > 0)
			{
				Logger.WriteErrorLine("Pre-announced card cant be used: " + this.m_announce.ToString());
			}
			return avail[0];
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0002B9CC File Offset: 0x00029BCC
		public void SelectCard(ClientCard card)
		{
			this.m_selector_pointer = this.m_selector.Count<CardSelector>();
			this.m_selector.Add(new CardSelector(card));
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0002B9F0 File Offset: 0x00029BF0
		public void SelectCard(IList<ClientCard> cards)
		{
			this.m_selector_pointer = this.m_selector.Count<CardSelector>();
			this.m_selector.Add(new CardSelector(cards));
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0002BA14 File Offset: 0x00029C14
		public void SelectCard(int cardId)
		{
			this.m_selector_pointer = this.m_selector.Count<CardSelector>();
			this.m_selector.Add(new CardSelector(cardId));
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0002BA38 File Offset: 0x00029C38
		public void SelectCard(IList<int> ids)
		{
			this.m_selector_pointer = this.m_selector.Count<CardSelector>();
			this.m_selector.Add(new CardSelector(ids));
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0002BA38 File Offset: 0x00029C38
		public void SelectCard(params int[] ids)
		{
			this.m_selector_pointer = this.m_selector.Count<CardSelector>();
			this.m_selector.Add(new CardSelector(ids));
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0002BA5C File Offset: 0x00029C5C
		public void SelectCard(CardLocation loc)
		{
			this.m_selector_pointer = this.m_selector.Count<CardSelector>();
			this.m_selector.Add(new CardSelector(loc));
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0002BA80 File Offset: 0x00029C80
		public void SelectNextCard(ClientCard card)
		{
			if (this.m_selector_pointer == -1)
			{
				Logger.WriteErrorLine("Error: Call SelectNextCard() before SelectCard()");
				this.m_selector_pointer = 0;
			}
			this.m_selector.Insert(this.m_selector_pointer, new CardSelector(card));
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0002BAB3 File Offset: 0x00029CB3
		public void SelectNextCard(IList<ClientCard> cards)
		{
			if (this.m_selector_pointer == -1)
			{
				Logger.WriteErrorLine("Error: Call SelectNextCard() before SelectCard()");
				this.m_selector_pointer = 0;
			}
			this.m_selector.Insert(this.m_selector_pointer, new CardSelector(cards));
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0002BAE6 File Offset: 0x00029CE6
		public void SelectNextCard(int cardId)
		{
			if (this.m_selector_pointer == -1)
			{
				Logger.WriteErrorLine("Error: Call SelectNextCard() before SelectCard()");
				this.m_selector_pointer = 0;
			}
			this.m_selector.Insert(this.m_selector_pointer, new CardSelector(cardId));
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0002BB19 File Offset: 0x00029D19
		public void SelectNextCard(IList<int> ids)
		{
			if (this.m_selector_pointer == -1)
			{
				Logger.WriteErrorLine("Error: Call SelectNextCard() before SelectCard()");
				this.m_selector_pointer = 0;
			}
			this.m_selector.Insert(this.m_selector_pointer, new CardSelector(ids));
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0002BB19 File Offset: 0x00029D19
		public void SelectNextCard(params int[] ids)
		{
			if (this.m_selector_pointer == -1)
			{
				Logger.WriteErrorLine("Error: Call SelectNextCard() before SelectCard()");
				this.m_selector_pointer = 0;
			}
			this.m_selector.Insert(this.m_selector_pointer, new CardSelector(ids));
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0002BB4C File Offset: 0x00029D4C
		public void SelectNextCard(CardLocation loc)
		{
			if (this.m_selector_pointer == -1)
			{
				Logger.WriteErrorLine("Error: Call SelectNextCard() before SelectCard()");
				this.m_selector_pointer = 0;
			}
			this.m_selector.Insert(this.m_selector_pointer, new CardSelector(loc));
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0002BB7F File Offset: 0x00029D7F
		public void SelectThirdCard(ClientCard card)
		{
			if (this.m_selector_pointer == -1)
			{
				Logger.WriteErrorLine("Error: Call SelectThirdCard() before SelectCard()");
				this.m_selector_pointer = 0;
			}
			this.m_selector.Insert(this.m_selector_pointer, new CardSelector(card));
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0002BBB2 File Offset: 0x00029DB2
		public void SelectThirdCard(IList<ClientCard> cards)
		{
			if (this.m_selector_pointer == -1)
			{
				Logger.WriteErrorLine("Error: Call SelectThirdCard() before SelectCard()");
				this.m_selector_pointer = 0;
			}
			this.m_selector.Insert(this.m_selector_pointer, new CardSelector(cards));
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0002BBE5 File Offset: 0x00029DE5
		public void SelectThirdCard(int cardId)
		{
			if (this.m_selector_pointer == -1)
			{
				Logger.WriteErrorLine("Error: Call SelectThirdCard() before SelectCard()");
				this.m_selector_pointer = 0;
			}
			this.m_selector.Insert(this.m_selector_pointer, new CardSelector(cardId));
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0002BC18 File Offset: 0x00029E18
		public void SelectThirdCard(IList<int> ids)
		{
			if (this.m_selector_pointer == -1)
			{
				Logger.WriteErrorLine("Error: Call SelectThirdCard() before SelectCard()");
				this.m_selector_pointer = 0;
			}
			this.m_selector.Insert(this.m_selector_pointer, new CardSelector(ids));
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0002BC18 File Offset: 0x00029E18
		public void SelectThirdCard(params int[] ids)
		{
			if (this.m_selector_pointer == -1)
			{
				Logger.WriteErrorLine("Error: Call SelectThirdCard() before SelectCard()");
				this.m_selector_pointer = 0;
			}
			this.m_selector.Insert(this.m_selector_pointer, new CardSelector(ids));
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0002BC4B File Offset: 0x00029E4B
		public void SelectThirdCard(CardLocation loc)
		{
			if (this.m_selector_pointer == -1)
			{
				Logger.WriteErrorLine("Error: Call SelectThirdCard() before SelectCard()");
				this.m_selector_pointer = 0;
			}
			this.m_selector.Insert(this.m_selector_pointer, new CardSelector(loc));
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0002BC7E File Offset: 0x00029E7E
		public void SelectMaterials(ClientCard card, int hint = 0)
		{
			this.m_materialSelector = new CardSelector(card);
			this.m_materialSelectorHint = hint;
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0002BC93 File Offset: 0x00029E93
		public void SelectMaterials(IList<ClientCard> cards, int hint = 0)
		{
			this.m_materialSelector = new CardSelector(cards);
			this.m_materialSelectorHint = hint;
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0002BCA8 File Offset: 0x00029EA8
		public void SelectMaterials(int cardId, int hint = 0)
		{
			this.m_materialSelector = new CardSelector(cardId);
			this.m_materialSelectorHint = hint;
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0002BCBD File Offset: 0x00029EBD
		public void SelectMaterials(IList<int> ids, int hint = 0)
		{
			this.m_materialSelector = new CardSelector(ids);
			this.m_materialSelectorHint = hint;
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0002BCD2 File Offset: 0x00029ED2
		public void SelectMaterials(CardLocation loc, int hint = 0)
		{
			this.m_materialSelector = new CardSelector(loc);
			this.m_materialSelectorHint = hint;
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0002BCE7 File Offset: 0x00029EE7
		public void CleanSelectMaterials()
		{
			this.m_materialSelector = null;
			this.m_materialSelectorHint = 0;
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0002BCF7 File Offset: 0x00029EF7
		public bool HaveSelectedCards()
		{
			return this.m_selector.Count > 0 || this.m_materialSelector != null;
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0002BD14 File Offset: 0x00029F14
		public CardSelector GetSelectedCards()
		{
			CardSelector selected = null;
			if (this.m_selector.Count > 0)
			{
				selected = this.m_selector[this.m_selector.Count - 1];
				this.m_selector.RemoveAt(this.m_selector.Count - 1);
			}
			return selected;
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0002BD64 File Offset: 0x00029F64
		public CardPosition GetSelectedPosition()
		{
			CardPosition selected = CardPosition.FaceUpAttack;
			if (this.m_position.Count > 0)
			{
				selected = this.m_position[0];
				this.m_position.RemoveAt(0);
			}
			return selected;
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0002BD9B File Offset: 0x00029F9B
		public void SelectPosition(CardPosition pos)
		{
			this.m_position.Add(pos);
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0002BDA9 File Offset: 0x00029FA9
		public void SelectPlace(int zones)
		{
			this.m_place = zones;
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0002BDB2 File Offset: 0x00029FB2
		public void SelectOption(int opt)
		{
			this.m_option = opt;
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0002BDBB File Offset: 0x00029FBB
		public void SelectNumber(int number)
		{
			this.m_number = number;
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0002BDC4 File Offset: 0x00029FC4
		public void SelectAttribute(CardAttribute attribute)
		{
			this.m_attributes.Clear();
			this.m_attributes.Add(attribute);
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0002BDE0 File Offset: 0x00029FE0
		public void SelectAttributes(CardAttribute[] attributes)
		{
			this.m_attributes.Clear();
			foreach (CardAttribute attribute in attributes)
			{
				this.m_attributes.Add(attribute);
			}
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0002BE18 File Offset: 0x0002A018
		public void SelectRace(CardRace race)
		{
			this.m_races.Clear();
			this.m_races.Add(race);
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0002BE34 File Offset: 0x0002A034
		public void SelectRaces(CardRace[] races)
		{
			this.m_races.Clear();
			foreach (CardRace race in races)
			{
				this.m_races.Add(race);
			}
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0002BE6C File Offset: 0x0002A06C
		public void SelectAnnounceID(int id)
		{
			this.m_announce = id;
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0002BE75 File Offset: 0x0002A075
		public void SelectYesNo(bool opt)
		{
			this.m_yesno = (opt ? 1 : 0);
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0002BE84 File Offset: 0x0002A084
		public int OnAnnounceNumber(IList<int> numbers)
		{
			if (numbers.Contains(this.m_number))
			{
				return numbers.IndexOf(this.m_number);
			}
			return Program.Rand.Next(0, numbers.Count);
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0002BEB4 File Offset: 0x0002A0B4
		public virtual IList<CardAttribute> OnAnnounceAttrib(int count, IList<CardAttribute> attributes)
		{
			IList<CardAttribute> foundAttributes = this.m_attributes.Where(new Func<CardAttribute, bool>(attributes.Contains)).ToList<CardAttribute>();
			if (foundAttributes.Count > 0)
			{
				return foundAttributes;
			}
			return attributes;
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0002BEEC File Offset: 0x0002A0EC
		public virtual IList<CardRace> OnAnnounceRace(int count, IList<CardRace> races)
		{
			IList<CardRace> foundRaces = this.m_races.Where(new Func<CardRace, bool>(races.Contains)).ToList<CardRace>();
			if (foundRaces.Count > 0)
			{
				return foundRaces;
			}
			return races;
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0002BF24 File Offset: 0x0002A124
		public BattlePhaseAction Attack(ClientCard attacker, ClientCard defender)
		{
			this.Executor.SetCard(ExecutorType.Summon, attacker, -1, -1);
			if (defender != null)
			{
				string cardName = defender.Name ?? "monster";
				attacker.ShouldDirectAttack = false;
				this._dialogs.SendAttack(attacker.Name, cardName);
				this.SelectCard(defender);
			}
			else
			{
				attacker.ShouldDirectAttack = true;
				this._dialogs.SendDirectAttack(attacker.Name);
			}
			return new BattlePhaseAction(BattlePhaseAction.BattleAction.Attack, attacker.ActionIndex);
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0002BF99 File Offset: 0x0002A199
		public BattlePhaseAction ToEndPhase()
		{
			this._dialogs.SendEndTurn();
			return new BattlePhaseAction(BattlePhaseAction.BattleAction.ToEndPhase);
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0002BFAC File Offset: 0x0002A1AC
		public BattlePhaseAction ToMainPhase2()
		{
			return new BattlePhaseAction(BattlePhaseAction.BattleAction.ToMainPhaseTwo);
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0002BFB4 File Offset: 0x0002A1B4
		private bool ShouldExecute(CardExecutor exec, ClientCard card, ExecutorType type, int desc = -1, int timing = -1)
		{
			this.Executor.SetCard(type, card, desc, timing);
			if (card.Id != 0 && type == ExecutorType.Activate)
			{
				if (this._activatedCards.ContainsKey(card.Id) && this._activatedCards[card.Id] >= 9)
				{
					return false;
				}
				if (!this.Executor.OnPreActivate(card))
				{
					return false;
				}
			}
			bool result = card != null && exec.Type == type && (exec.CardId == -1 || exec.CardId == card.Id) && (exec.Func == null || exec.Func());
			if (card.Id != 0 && type == ExecutorType.Activate && result)
			{
				int count = (card.IsDisabled() ? 3 : 1);
				if (!this._activatedCards.ContainsKey(card.Id))
				{
					this._activatedCards.Add(card.Id, count);
				}
				else
				{
					Dictionary<int, int> activatedCards = this._activatedCards;
					int id = card.Id;
					activatedCards[id] += count;
				}
			}
			return result;
		}

		// Token: 0x04000DB6 RID: 3510
		private Dialogs _dialogs;

		// Token: 0x04000DB7 RID: 3511
		private Dictionary<int, int> _activatedCards;

		// Token: 0x04000DB8 RID: 3512
		private CardSelector m_materialSelector;

		// Token: 0x04000DB9 RID: 3513
		private int m_materialSelectorHint;

		// Token: 0x04000DBA RID: 3514
		private int m_place;

		// Token: 0x04000DBB RID: 3515
		private int m_option;

		// Token: 0x04000DBC RID: 3516
		private int m_number;

		// Token: 0x04000DBD RID: 3517
		private int m_announce;

		// Token: 0x04000DBE RID: 3518
		private int m_yesno;

		// Token: 0x04000DBF RID: 3519
		private IList<CardAttribute> m_attributes = new List<CardAttribute>();

		// Token: 0x04000DC0 RID: 3520
		private IList<CardSelector> m_selector = new List<CardSelector>();

		// Token: 0x04000DC1 RID: 3521
		private IList<CardPosition> m_position = new List<CardPosition>();

		// Token: 0x04000DC2 RID: 3522
		private int m_selector_pointer = -1;

		// Token: 0x04000DC3 RID: 3523
		private IList<CardRace> m_races = new List<CardRace>();
	}
}
