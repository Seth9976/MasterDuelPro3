using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using WindBot.Game.AI;
using YGOSharp.Network;
using YGOSharp.Network.Enums;
using YGOSharp.Network.Utils;
using YGOSharp.OCGWrapper;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game
{
	// Token: 0x020001FD RID: 509
	public class GameBehavior
	{
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x0002C0BB File Offset: 0x0002A2BB
		// (set) Token: 0x06000A5C RID: 2652 RVA: 0x0002C0C3 File Offset: 0x0002A2C3
		public GameClient Game { get; private set; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x0002C0CC File Offset: 0x0002A2CC
		// (set) Token: 0x06000A5E RID: 2654 RVA: 0x0002C0D4 File Offset: 0x0002A2D4
		public YGOClient Connection { get; private set; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000A5F RID: 2655 RVA: 0x0002C0DD File Offset: 0x0002A2DD
		// (set) Token: 0x06000A60 RID: 2656 RVA: 0x0002C0E5 File Offset: 0x0002A2E5
		public Deck Deck { get; private set; }

		// Token: 0x06000A61 RID: 2657 RVA: 0x0002C0F0 File Offset: 0x0002A2F0
		public GameBehavior(GameClient game)
		{
			this.Game = game;
			this.Connection = game.Connection;
			this._hand = game.Hand;
			this._debug = game.Debug;
			this._packets = new Dictionary<StocMessage, Action<BinaryReader>>();
			this._messages = new Dictionary<GameMessage, Action<BinaryReader>>();
			this.RegisterPackets();
			this._room = new Room();
			this._duel = new Duel();
			this._ai = new GameAI(this.Game, this._duel);
			this._ai.Executor = DecksManager.Instantiate(this._ai, this._duel);
			this.Deck = Deck.Load(this.Game.DeckFile ?? this._ai.Executor.Deck);
			this._select_hint = 0;
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0002C1C4 File Offset: 0x0002A3C4
		public int GetLocalPlayer(int player)
		{
			if (!this._duel.IsFirst)
			{
				return 1 - player;
			}
			return player;
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0002C1D8 File Offset: 0x0002A3D8
		public void OnPacket(BinaryReader packet)
		{
			StocMessage id = (StocMessage)packet.ReadByte();
			if (id == StocMessage.GameMsg)
			{
				GameMessage msg = (GameMessage)packet.ReadByte();
				if (this._messages.ContainsKey(msg))
				{
					this._messages[msg](packet);
				}
				this._lastMessage = msg;
				return;
			}
			if (this._packets.ContainsKey(id))
			{
				this._packets[id](packet);
			}
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0002C240 File Offset: 0x0002A440
		private void RegisterPackets()
		{
			this._packets.Add(StocMessage.JoinGame, new Action<BinaryReader>(this.OnJoinGame));
			this._packets.Add(StocMessage.TypeChange, new Action<BinaryReader>(this.OnTypeChange));
			this._packets.Add(StocMessage.HsPlayerEnter, new Action<BinaryReader>(this.OnPlayerEnter));
			this._packets.Add(StocMessage.HsPlayerChange, new Action<BinaryReader>(this.OnPlayerChange));
			this._packets.Add(StocMessage.SelectHand, new Action<BinaryReader>(this.OnSelectHand));
			this._packets.Add(StocMessage.SelectTp, new Action<BinaryReader>(this.OnSelectTp));
			this._packets.Add(StocMessage.TimeLimit, new Action<BinaryReader>(this.OnTimeLimit));
			this._packets.Add(StocMessage.Replay, new Action<BinaryReader>(this.OnReplay));
			this._packets.Add(StocMessage.DuelEnd, new Action<BinaryReader>(this.OnDuelEnd));
			this._packets.Add(StocMessage.Chat, new Action<BinaryReader>(this.OnChat));
			this._packets.Add(StocMessage.ChangeSide, new Action<BinaryReader>(this.OnChangeSide));
			this._packets.Add(StocMessage.ErrorMsg, new Action<BinaryReader>(this.OnErrorMsg));
			this._packets.Add(StocMessage.TeammateSurrender, new Action<BinaryReader>(this.OnTeammateSurrender));
			this._messages.Add(GameMessage.Retry, new Action<BinaryReader>(this.OnRetry));
			this._messages.Add(GameMessage.Start, new Action<BinaryReader>(this.OnStart));
			this._messages.Add(GameMessage.Hint, new Action<BinaryReader>(this.OnHint));
			this._messages.Add(GameMessage.Win, new Action<BinaryReader>(this.OnWin));
			this._messages.Add(GameMessage.Draw, new Action<BinaryReader>(this.OnDraw));
			this._messages.Add(GameMessage.ShuffleDeck, new Action<BinaryReader>(this.OnShuffleDeck));
			this._messages.Add(GameMessage.ShuffleHand, new Action<BinaryReader>(this.OnShuffleHand));
			this._messages.Add(GameMessage.ShuffleExtra, new Action<BinaryReader>(this.OnShuffleExtra));
			this._messages.Add(GameMessage.ShuffleSetCard, new Action<BinaryReader>(this.OnShuffleSetCard));
			this._messages.Add(GameMessage.SwapGraveDeck, new Action<BinaryReader>(this.OnSwapGraveDeck));
			this._messages.Add(GameMessage.TagSwap, new Action<BinaryReader>(this.OnTagSwap));
			this._messages.Add(GameMessage.NewTurn, new Action<BinaryReader>(this.OnNewTurn));
			this._messages.Add(GameMessage.NewPhase, new Action<BinaryReader>(this.OnNewPhase));
			this._messages.Add(GameMessage.Damage, new Action<BinaryReader>(this.OnDamage));
			this._messages.Add(GameMessage.PayLpCost, new Action<BinaryReader>(this.OnDamage));
			this._messages.Add(GameMessage.Recover, new Action<BinaryReader>(this.OnRecover));
			this._messages.Add(GameMessage.LpUpdate, new Action<BinaryReader>(this.OnLpUpdate));
			this._messages.Add(GameMessage.Move, new Action<BinaryReader>(this.OnMove));
			this._messages.Add(GameMessage.Swap, new Action<BinaryReader>(this.OnSwap));
			this._messages.Add(GameMessage.Attack, new Action<BinaryReader>(this.OnAttack));
			this._messages.Add(GameMessage.Battle, new Action<BinaryReader>(this.OnBattle));
			this._messages.Add(GameMessage.AttackDisabled, new Action<BinaryReader>(this.OnAttackDisabled));
			this._messages.Add(GameMessage.PosChange, new Action<BinaryReader>(this.OnPosChange));
			this._messages.Add(GameMessage.Chaining, new Action<BinaryReader>(this.OnChaining));
			this._messages.Add(GameMessage.ChainSolving, new Action<BinaryReader>(this.OnChainSolving));
			this._messages.Add(GameMessage.ChainNegated, new Action<BinaryReader>(this.OnChainNegated));
			this._messages.Add(GameMessage.ChainDisabled, new Action<BinaryReader>(this.OnChainDisabled));
			this._messages.Add(GameMessage.ChainSolved, new Action<BinaryReader>(this.OnChainSolved));
			this._messages.Add(GameMessage.ChainEnd, new Action<BinaryReader>(this.OnChainEnd));
			this._messages.Add(GameMessage.SortCard, new Action<BinaryReader>(this.OnCardSorting));
			this._messages.Add(GameMessage.SortChain, new Action<BinaryReader>(this.OnChainSorting));
			this._messages.Add(GameMessage.UpdateCard, new Action<BinaryReader>(this.OnUpdateCard));
			this._messages.Add(GameMessage.UpdateData, new Action<BinaryReader>(this.OnUpdateData));
			this._messages.Add(GameMessage.BecomeTarget, new Action<BinaryReader>(this.OnBecomeTarget));
			this._messages.Add(GameMessage.SelectBattleCmd, new Action<BinaryReader>(this.OnSelectBattleCmd));
			this._messages.Add(GameMessage.SelectCard, new Action<BinaryReader>(this.OnSelectCard));
			this._messages.Add(GameMessage.SelectUnselect, new Action<BinaryReader>(this.OnSelectUnselectCard));
			this._messages.Add(GameMessage.SelectChain, new Action<BinaryReader>(this.OnSelectChain));
			this._messages.Add(GameMessage.SelectCounter, new Action<BinaryReader>(this.OnSelectCounter));
			this._messages.Add(GameMessage.SelectDisfield, new Action<BinaryReader>(this.OnSelectDisfield));
			this._messages.Add(GameMessage.SelectEffectYn, new Action<BinaryReader>(this.OnSelectEffectYn));
			this._messages.Add(GameMessage.SelectIdleCmd, new Action<BinaryReader>(this.OnSelectIdleCmd));
			this._messages.Add(GameMessage.SelectOption, new Action<BinaryReader>(this.OnSelectOption));
			this._messages.Add(GameMessage.SelectPlace, new Action<BinaryReader>(this.OnSelectPlace));
			this._messages.Add(GameMessage.SelectPosition, new Action<BinaryReader>(this.OnSelectPosition));
			this._messages.Add(GameMessage.SelectSum, new Action<BinaryReader>(this.OnSelectSum));
			this._messages.Add(GameMessage.SelectTribute, new Action<BinaryReader>(this.OnSelectTribute));
			this._messages.Add(GameMessage.SelectYesNo, new Action<BinaryReader>(this.OnSelectYesNo));
			this._messages.Add(GameMessage.AnnounceAttrib, new Action<BinaryReader>(this.OnAnnounceAttrib));
			this._messages.Add(GameMessage.AnnounceCard, new Action<BinaryReader>(this.OnAnnounceCard));
			this._messages.Add(GameMessage.AnnounceNumber, new Action<BinaryReader>(this.OnAnnounceNumber));
			this._messages.Add(GameMessage.AnnounceRace, new Action<BinaryReader>(this.OnAnnounceRace));
			this._messages.Add(GameMessage.RockPaperScissors, new Action<BinaryReader>(this.OnRockPaperScissors));
			this._messages.Add(GameMessage.Equip, new Action<BinaryReader>(this.OnEquip));
			this._messages.Add(GameMessage.Unequip, new Action<BinaryReader>(this.OnUnEquip));
			this._messages.Add(GameMessage.CardTarget, new Action<BinaryReader>(this.OnCardTarget));
			this._messages.Add(GameMessage.CancelTarget, new Action<BinaryReader>(this.OnCancelTarget));
			this._messages.Add(GameMessage.Summoning, new Action<BinaryReader>(this.OnSummoning));
			this._messages.Add(GameMessage.Summoned, new Action<BinaryReader>(this.OnSummoned));
			this._messages.Add(GameMessage.SpSummoning, new Action<BinaryReader>(this.OnSpSummoning));
			this._messages.Add(GameMessage.SpSummoned, new Action<BinaryReader>(this.OnSpSummoned));
			this._messages.Add(GameMessage.FlipSummoning, new Action<BinaryReader>(this.OnSummoning));
			this._messages.Add(GameMessage.FlipSummoned, new Action<BinaryReader>(this.OnSummoned));
			this._messages.Add(GameMessage.ConfirmCards, new Action<BinaryReader>(this.OnConfirmCards));
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0002C9DC File Offset: 0x0002ABDC
		private void OnJoinGame(BinaryReader packet)
		{
			packet.ReadUInt32();
			packet.ReadByte();
			packet.ReadByte();
			int duel_rule = (int)packet.ReadByte();
			this._ai.Duel.IsNewRule = duel_rule >= 4;
			this._ai.Duel.IsNewRule2020 = duel_rule >= 5;
			BinaryWriter deck = GamePacketFactory.Create(CtosMessage.UpdateDeck);
			deck.Write(this.Deck.Cards.Count + this.Deck.ExtraCards.Count);
			deck.Write(this.Deck.SideCards.Count);
			foreach (NamedCard card in this.Deck.Cards)
			{
				deck.Write(card.Id);
			}
			foreach (NamedCard card2 in this.Deck.ExtraCards)
			{
				deck.Write(card2.Id);
			}
			foreach (NamedCard card3 in this.Deck.SideCards)
			{
				deck.Write(card3.Id);
			}
			this.Connection.Send(deck);
			this._ai.OnJoinGame();
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0002CB68 File Offset: 0x0002AD68
		private void OnChangeSide(BinaryReader packet)
		{
			BinaryWriter deck = GamePacketFactory.Create(CtosMessage.UpdateDeck);
			deck.Write(this.Deck.Cards.Count + this.Deck.ExtraCards.Count);
			deck.Write(this.Deck.SideCards.Count);
			foreach (NamedCard card in this.Deck.Cards)
			{
				deck.Write(card.Id);
			}
			foreach (NamedCard card2 in this.Deck.ExtraCards)
			{
				deck.Write(card2.Id);
			}
			foreach (NamedCard card3 in this.Deck.SideCards)
			{
				deck.Write(card3.Id);
			}
			this.Connection.Send(deck);
			this._ai.OnJoinGame();
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0002CCA8 File Offset: 0x0002AEA8
		private void OnTypeChange(BinaryReader packet)
		{
			int type = (int)packet.ReadByte();
			int pos = type & 15;
			if (pos < 0 || pos > 3)
			{
				this.Connection.Close(null);
				return;
			}
			this._room.Position = pos;
			this._room.IsHost = ((type >> 4) & 15) != 0;
			this._room.IsReady[pos] = true;
			this.Connection.Send(CtosMessage.HsReady);
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0002CD14 File Offset: 0x0002AF14
		private void OnPlayerEnter(BinaryReader packet)
		{
			string name = packet.ReadUnicode(20);
			int pos = (int)packet.ReadByte();
			if (pos < 8)
			{
				this._room.Names[pos] = name;
			}
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0002CD44 File Offset: 0x0002AF44
		private void OnPlayerChange(BinaryReader packet)
		{
			byte b = packet.ReadByte();
			int pos = (b >> 4) & 15;
			int state = (int)(b & 15);
			if (pos > 3)
			{
				return;
			}
			if (state < 8)
			{
				string oldname = this._room.Names[pos];
				this._room.Names[pos] = null;
				this._room.Names[state] = oldname;
				this._room.IsReady[pos] = false;
				this._room.IsReady[state] = false;
			}
			else if (state == 9)
			{
				this._room.IsReady[pos] = true;
			}
			else if (state == 10)
			{
				this._room.IsReady[pos] = false;
			}
			else if (state == 11 || state == 8)
			{
				this._room.IsReady[pos] = false;
				this._room.Names[pos] = null;
			}
			if (this._room.IsHost && this._room.IsReady[0] && this._room.IsReady[1])
			{
				this.Connection.Send(CtosMessage.HsStart);
			}
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0002CE3C File Offset: 0x0002B03C
		private void OnSelectHand(BinaryReader packet)
		{
			int result;
			if (this._hand > 0)
			{
				result = this._hand;
			}
			else
			{
				result = this._ai.OnRockPaperScissors();
			}
			this.Connection.Send(CtosMessage.HandResult, (int)((byte)result));
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0002CE78 File Offset: 0x0002B078
		private void OnSelectTp(BinaryReader packet)
		{
			bool start = this._ai.OnSelectHand();
			this.Connection.Send(CtosMessage.TpResult, (int)(start ? 1 : 0));
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0002CEA5 File Offset: 0x0002B0A5
		private void OnTimeLimit(BinaryReader packet)
		{
			if (this.GetLocalPlayer((int)packet.ReadByte()) == 0)
			{
				this.Connection.Send(CtosMessage.TimeConfirm);
			}
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0002CEC2 File Offset: 0x0002B0C2
		private void OnReplay(BinaryReader packet)
		{
			packet.ReadToEnd();
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0002CECB File Offset: 0x0002B0CB
		private void OnDuelEnd(BinaryReader packet)
		{
			Thread.Sleep(500);
			this.Connection.Close(null);
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0002CEE4 File Offset: 0x0002B0E4
		private void OnChat(BinaryReader packet)
		{
			int player = (int)packet.ReadInt16();
			string message = packet.ReadUnicode(256);
			string myName = ((player != 0) ? this._room.Names[1] : this._room.Names[0]);
			string otherName = ((player == 0) ? this._room.Names[1] : this._room.Names[0]);
			if (player < 4)
			{
				Logger.DebugWriteLine(string.Concat(new string[] { otherName, " say to ", myName, ": ", message }));
				return;
			}
			Logger.DebugWriteLine("System message(" + player.ToString() + "): " + message);
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0002CF94 File Offset: 0x0002B194
		private void OnErrorMsg(BinaryReader packet)
		{
			int msg = (int)packet.ReadByte();
			packet.ReadByte();
			packet.ReadByte();
			packet.ReadByte();
			int pcode = packet.ReadInt32();
			Logger.DebugWriteLine("Error message received: " + msg.ToString() + ", code: " + pcode.ToString());
			if (msg == 2)
			{
				int code = pcode & 268435455;
				if (pcode >> 28 <= 5)
				{
					NamedCard card = NamedCard.Get(code);
					if (card != null)
					{
						this._ai.OnDeckError(card.Name);
						return;
					}
					this._ai.OnDeckError("Unknown Card");
					return;
				}
				else
				{
					this._ai.OnDeckError("DECK");
				}
			}
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0002D036 File Offset: 0x0002B236
		private void OnTeammateSurrender(BinaryReader packet)
		{
			Thread.Sleep(500);
			this.Game.Surrender();
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0002D04D File Offset: 0x0002B24D
		private void OnRetry(BinaryReader packet)
		{
			this._ai.OnRetry();
			this.Connection.Close(null);
			throw new Exception("Got MSG_RETRY. Last message is " + this._lastMessage.ToString());
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0002D088 File Offset: 0x0002B288
		private void OnHint(BinaryReader packet)
		{
			byte b = packet.ReadByte();
			int player = (int)packet.ReadByte();
			int data = packet.ReadInt32();
			if (b == 1 && data == 24)
			{
				this._duel.Fields[0].UnderAttack = false;
				this._duel.Fields[1].UnderAttack = false;
			}
			if (b == 3)
			{
				this._select_hint = data;
			}
			if (b == 4)
			{
				this._ai.OnReceivingAnnouce(player, data);
			}
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0002D0F4 File Offset: 0x0002B2F4
		private void OnStart(BinaryReader packet)
		{
			int type = (int)packet.ReadByte();
			this._duel.IsFirst = (type & 15) == 0;
			this._duel.Turn = 0;
			this._duel.LastChainLocation = (CardLocation)0;
			this._duel.LastChainPlayer = -1;
			this._duel.LastChainTargets.Clear();
			this._duel.LastSummonedCards.Clear();
			this._duel.LastSummonPlayer = -1;
			int duel_rule = (int)packet.ReadByte();
			this._ai.Duel.IsNewRule = duel_rule >= 4;
			this._ai.Duel.IsNewRule2020 = duel_rule >= 5;
			this._duel.Fields[this.GetLocalPlayer(0)].LifePoints = packet.ReadInt32();
			this._duel.Fields[this.GetLocalPlayer(1)].LifePoints = packet.ReadInt32();
			int deck = (int)packet.ReadInt16();
			int extra = (int)packet.ReadInt16();
			this._duel.Fields[this.GetLocalPlayer(0)].Init(deck, extra);
			deck = (int)packet.ReadInt16();
			extra = (int)packet.ReadInt16();
			this._duel.Fields[this.GetLocalPlayer(1)].Init(deck, extra);
			this._duel.CurrentChain.Clear();
			this._duel.CurrentChainInfo.Clear();
			this._duel.ChainTargets.Clear();
			this._duel.ChainTargetOnly.Clear();
			this._duel.SummoningCards.Clear();
			this._duel.SolvingChainIndex = 0;
			this._duel.NegatedChainIndexList.Clear();
			Logger.DebugWriteLine("Duel started: " + this._room.Names[0] + " versus " + this._room.Names[1]);
			this._ai.OnStart();
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0002D2D0 File Offset: 0x0002B4D0
		private void OnWin(BinaryReader packet)
		{
			int result = this.GetLocalPlayer((int)packet.ReadByte());
			string otherName = ((this._room.Position == 0) ? this._room.Names[1] : this._room.Names[0]);
			string textResult = ((result == 2) ? "Draw" : ((result == 0) ? "Win" : "Lose"));
			Logger.DebugWriteLine("Duel finished against " + otherName + ", result: " + textResult);
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0002D348 File Offset: 0x0002B548
		private void OnDraw(BinaryReader packet)
		{
			int player = this.GetLocalPlayer((int)packet.ReadByte());
			int count = (int)packet.ReadByte();
			if (this._debug)
			{
				Logger.WriteLine(string.Concat(new string[]
				{
					"(",
					player.ToString(),
					" draw ",
					count.ToString(),
					" card)"
				}));
			}
			for (int i = 0; i < count; i++)
			{
				this._duel.Fields[player].Deck.RemoveAt(this._duel.Fields[player].Deck.Count - 1);
				this._duel.Fields[player].Hand.Add(new ClientCard(0, CardLocation.Hand, -1));
			}
			this._ai.OnDraw(player);
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0002D414 File Offset: 0x0002B614
		private void OnShuffleDeck(BinaryReader packet)
		{
			int player = this.GetLocalPlayer((int)packet.ReadByte());
			foreach (ClientCard clientCard in this._duel.Fields[player].Deck)
			{
				clientCard.SetId(0);
			}
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0002D478 File Offset: 0x0002B678
		private void OnShuffleHand(BinaryReader packet)
		{
			int player = this.GetLocalPlayer((int)packet.ReadByte());
			packet.ReadByte();
			foreach (ClientCard clientCard in this._duel.Fields[player].Hand)
			{
				clientCard.SetId(packet.ReadInt32());
			}
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0002D4E8 File Offset: 0x0002B6E8
		private void OnShuffleExtra(BinaryReader packet)
		{
			int player = this.GetLocalPlayer((int)packet.ReadByte());
			packet.ReadByte();
			foreach (ClientCard card in this._duel.Fields[player].ExtraDeck)
			{
				if (!card.IsFaceup())
				{
					card.SetId(packet.ReadInt32());
				}
			}
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0002D564 File Offset: 0x0002B764
		private void OnShuffleSetCard(BinaryReader packet)
		{
			packet.ReadByte();
			int count = (int)packet.ReadByte();
			ClientCard[] list = new ClientCard[5];
			for (int i = 0; i < count; i++)
			{
				int player = this.GetLocalPlayer((int)packet.ReadByte());
				int loc = (int)packet.ReadByte();
				int seq = (int)packet.ReadByte();
				packet.ReadByte();
				ClientCard card = this._duel.GetCard(player, (CardLocation)loc, seq);
				if (card != null)
				{
					list[i] = card;
					card.SetId(0);
				}
			}
			for (int j = 0; j < count; j++)
			{
				int player2 = this.GetLocalPlayer((int)packet.ReadByte());
				int loc2 = (int)packet.ReadByte();
				int seq2 = (int)packet.ReadByte();
				packet.ReadByte();
				if (this._duel.GetCard(player2, (CardLocation)loc2, seq2) != null)
				{
					((loc2 == 4) ? this._duel.Fields[player2].MonsterZone : this._duel.Fields[player2].SpellZone)[seq2] = list[j];
				}
			}
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0002D658 File Offset: 0x0002B858
		private void OnSwapGraveDeck(BinaryReader packet)
		{
			int player = this.GetLocalPlayer((int)packet.ReadByte());
			IList<ClientCard> tmpDeckList = this._duel.Fields[player].Deck.ToList<ClientCard>();
			this._duel.Fields[player].Deck.Clear();
			int seq = 0;
			foreach (ClientCard card in this._duel.Fields[player].Graveyard)
			{
				if (card.IsExtraCard())
				{
					this._duel.Fields[player].ExtraDeck.Add(card);
					card.Location = CardLocation.Extra;
					card.Position = 10;
				}
				else
				{
					this._duel.Fields[player].Deck.Add(card);
					card.Location = CardLocation.Deck;
					card.Sequence = seq++;
				}
			}
			this._duel.Fields[player].Graveyard.Clear();
			foreach (ClientCard card2 in tmpDeckList)
			{
				this._duel.Fields[player].Graveyard.Add(card2);
				card2.Location = CardLocation.Grave;
			}
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0002D7B8 File Offset: 0x0002B9B8
		private void OnTagSwap(BinaryReader packet)
		{
			int player = this.GetLocalPlayer((int)packet.ReadByte());
			int mcount = (int)packet.ReadByte();
			int ecount = (int)packet.ReadByte();
			packet.ReadByte();
			int hcount = (int)packet.ReadByte();
			packet.ReadInt32();
			this._duel.Fields[player].Deck.Clear();
			for (int i = 0; i < mcount; i++)
			{
				this._duel.Fields[player].Deck.Add(new ClientCard(0, CardLocation.Deck, -1));
			}
			this._duel.Fields[player].ExtraDeck.Clear();
			for (int j = 0; j < ecount; j++)
			{
				int code = packet.ReadInt32() & int.MaxValue;
				this._duel.Fields[player].ExtraDeck.Add(new ClientCard(code, CardLocation.Extra, -1));
			}
			this._duel.Fields[player].Hand.Clear();
			for (int k = 0; k < hcount; k++)
			{
				int code2 = packet.ReadInt32();
				this._duel.Fields[player].Hand.Add(new ClientCard(code2, CardLocation.Hand, -1));
			}
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0002D8E0 File Offset: 0x0002BAE0
		private void OnNewTurn(BinaryReader packet)
		{
			Duel duel = this._duel;
			int turn = duel.Turn;
			duel.Turn = turn + 1;
			this._duel.Player = this.GetLocalPlayer((int)packet.ReadByte());
			this._ai.OnNewTurn();
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0002D924 File Offset: 0x0002BB24
		private void OnNewPhase(BinaryReader packet)
		{
			this._duel.Phase = (DuelPhase)packet.ReadInt16();
			if (this._debug && this._duel.Phase == DuelPhase.Standby)
			{
				Logger.WriteLine("*********Bot Hand*********");
				foreach (ClientCard clientCard in this._duel.Fields[0].Hand)
				{
					Logger.WriteLine(clientCard.Name);
				}
				Logger.WriteLine("*********Bot Spell*********");
				foreach (ClientCard clientCard2 in this._duel.Fields[0].SpellZone)
				{
					Logger.WriteLine((clientCard2 != null) ? clientCard2.Name : null);
				}
				Logger.WriteLine("*********Bot Monster*********");
				foreach (ClientCard clientCard3 in this._duel.Fields[0].MonsterZone)
				{
					Logger.WriteLine((clientCard3 != null) ? clientCard3.Name : null);
				}
				Logger.WriteLine("*********Finish*********");
			}
			if (this._debug)
			{
				Logger.WriteLine("(Go to " + this._duel.Phase.ToString() + ")");
			}
			this._duel.LastSummonPlayer = -1;
			this._duel.SummoningCards.Clear();
			this._duel.LastSummonedCards.Clear();
			this._duel.Fields[0].BattlingMonster = null;
			this._duel.Fields[1].BattlingMonster = null;
			this._duel.Fields[0].UnderAttack = false;
			this._duel.Fields[1].UnderAttack = false;
			foreach (ClientCard clientCard4 in this._duel.Fields[0].GetMonsters())
			{
				clientCard4.Attacked = false;
			}
			this._select_hint = 0;
			this._ai.OnNewPhase();
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0002DB4C File Offset: 0x0002BD4C
		private void OnDamage(BinaryReader packet)
		{
			int player = this.GetLocalPlayer((int)packet.ReadByte());
			int final = this._duel.Fields[player].LifePoints - packet.ReadInt32();
			if (final < 0)
			{
				final = 0;
			}
			if (this._debug)
			{
				Logger.WriteLine(string.Concat(new string[]
				{
					"(",
					player.ToString(),
					" got damage , LifePoint left = ",
					final.ToString(),
					")"
				}));
			}
			this._duel.Fields[player].LifePoints = final;
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0002DBE0 File Offset: 0x0002BDE0
		private void OnRecover(BinaryReader packet)
		{
			int player = this.GetLocalPlayer((int)packet.ReadByte());
			int final = this._duel.Fields[player].LifePoints + packet.ReadInt32();
			if (this._debug)
			{
				Logger.WriteLine(string.Concat(new string[]
				{
					"(",
					player.ToString(),
					" got healed , LifePoint left = ",
					final.ToString(),
					")"
				}));
			}
			this._duel.Fields[player].LifePoints = final;
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0002DC6C File Offset: 0x0002BE6C
		private void OnLpUpdate(BinaryReader packet)
		{
			int player = this.GetLocalPlayer((int)packet.ReadByte());
			this._duel.Fields[player].LifePoints = packet.ReadInt32();
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0002DCA0 File Offset: 0x0002BEA0
		private void OnMove(BinaryReader packet)
		{
			int cardId = packet.ReadInt32();
			int previousControler = this.GetLocalPlayer((int)packet.ReadByte());
			int previousLocation = (int)packet.ReadByte();
			int previousSequence = (int)packet.ReadSByte();
			packet.ReadSByte();
			int currentControler = this.GetLocalPlayer((int)packet.ReadByte());
			int currentLocation = (int)packet.ReadByte();
			int currentSequence = (int)packet.ReadSByte();
			int currentPosition = (int)packet.ReadSByte();
			packet.ReadInt32();
			ClientCard card = this._duel.GetCard(previousControler, (CardLocation)previousLocation, previousSequence);
			if (card != null)
			{
				card.LastLocation = (CardLocation)previousLocation;
			}
			if ((previousLocation & 128) != 0)
			{
				previousLocation &= 127;
				card = this._duel.GetCard(previousControler, (CardLocation)previousLocation, previousSequence);
				if (card != null)
				{
					if (this._debug)
					{
						string[] array = new string[7];
						array[0] = "(";
						array[1] = previousControler.ToString();
						array[2] = " 's ";
						array[3] = card.Name ?? "UnKnowCard";
						array[4] = " deattach ";
						int num = 5;
						NamedCard namedCard = NamedCard.Get(cardId);
						array[num] = ((namedCard != null) ? namedCard.Name : null);
						array[6] = ")";
						Logger.WriteLine(string.Concat(array));
					}
					card.Overlays.Remove(cardId);
				}
				previousLocation = 0;
			}
			else
			{
				this._duel.RemoveCard((CardLocation)previousLocation, card, previousControler, previousSequence);
			}
			if ((currentLocation & 128) != 0)
			{
				currentLocation &= 127;
				card = this._duel.GetCard(currentControler, (CardLocation)currentLocation, currentSequence);
				if (card != null)
				{
					if (this._debug)
					{
						string[] array2 = new string[7];
						array2[0] = "(";
						array2[1] = previousControler.ToString();
						array2[2] = " 's ";
						array2[3] = card.Name ?? "UnKnowCard";
						array2[4] = " overlay ";
						int num2 = 5;
						NamedCard namedCard2 = NamedCard.Get(cardId);
						array2[num2] = ((namedCard2 != null) ? namedCard2.Name : null);
						array2[6] = ")";
						Logger.WriteLine(string.Concat(array2));
					}
					card.Overlays.Add(cardId);
				}
			}
			else if (previousLocation == 0)
			{
				if (this._debug)
				{
					string[] array3 = new string[7];
					array3[0] = "(";
					array3[1] = previousControler.ToString();
					array3[2] = " 's ";
					int num3 = 3;
					NamedCard namedCard3 = NamedCard.Get(cardId);
					array3[num3] = ((namedCard3 != null) ? namedCard3.Name : null);
					array3[4] = " appear in ";
					int num4 = 5;
					CardLocation cardLocation = (CardLocation)currentLocation;
					array3[num4] = cardLocation.ToString();
					array3[6] = ")";
					Logger.WriteLine(string.Concat(array3));
				}
				this._duel.AddCard((CardLocation)currentLocation, cardId, currentControler, currentSequence, currentPosition);
			}
			else
			{
				this._duel.AddCard((CardLocation)currentLocation, card, currentControler, currentSequence, currentPosition, cardId);
				if (card != null && previousLocation != currentLocation)
				{
					card.IsSpecialSummoned = false;
				}
				if (this._debug && card != null)
				{
					string[] array4 = new string[9];
					array4[0] = "(";
					array4[1] = previousControler.ToString();
					array4[2] = " 's ";
					array4[3] = card.Name ?? "UnKnowCard";
					array4[4] = " from ";
					int num5 = 5;
					CardLocation cardLocation = (CardLocation)previousLocation;
					array4[num5] = cardLocation.ToString();
					array4[6] = " move to ";
					int num6 = 7;
					cardLocation = (CardLocation)currentLocation;
					array4[num6] = cardLocation.ToString();
					array4[8] = ")";
					Logger.WriteLine(string.Concat(array4));
				}
			}
			this._ai.OnMove(card, previousControler, previousLocation, currentControler, currentLocation);
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0002DFCC File Offset: 0x0002C1CC
		private void OnSwap(BinaryReader packet)
		{
			int cardId = packet.ReadInt32();
			int controler = this.GetLocalPlayer((int)packet.ReadByte());
			int location = (int)packet.ReadByte();
			int sequence = (int)packet.ReadByte();
			packet.ReadByte();
			int cardId2 = packet.ReadInt32();
			int controler2 = this.GetLocalPlayer((int)packet.ReadByte());
			int location2 = (int)packet.ReadByte();
			int sequence2 = (int)packet.ReadByte();
			packet.ReadByte();
			ClientCard card = this._duel.GetCard(controler, (CardLocation)location, sequence);
			ClientCard card2 = this._duel.GetCard(controler2, (CardLocation)location2, sequence2);
			if (card == null || card2 == null)
			{
				return;
			}
			this._duel.RemoveCard((CardLocation)location, card, controler, sequence);
			this._duel.RemoveCard((CardLocation)location2, card2, controler2, sequence2);
			this._duel.AddCard((CardLocation)location2, card, controler2, sequence2, card.Position, cardId);
			this._duel.AddCard((CardLocation)location, card2, controler, sequence, card2.Position, cardId2);
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0002E0B4 File Offset: 0x0002C2B4
		private void OnAttack(BinaryReader packet)
		{
			int ca = this.GetLocalPlayer((int)packet.ReadByte());
			int la = (int)packet.ReadByte();
			int sa = (int)packet.ReadByte();
			packet.ReadByte();
			int cd = this.GetLocalPlayer((int)packet.ReadByte());
			int ld = (int)packet.ReadByte();
			int sd = (int)packet.ReadByte();
			packet.ReadByte();
			ClientCard attackcard = this._duel.GetCard(ca, (CardLocation)la, sa);
			ClientCard defendcard = this._duel.GetCard(cd, (CardLocation)ld, sd);
			if (this._debug)
			{
				if (defendcard == null)
				{
					Logger.WriteLine("(" + (attackcard.Name ?? "UnKnowCard") + " direct attack!!)");
				}
				else
				{
					Logger.WriteLine(string.Concat(new string[]
					{
						"(",
						ca.ToString(),
						" 's ",
						attackcard.Name ?? "UnKnowCard",
						" attack  ",
						cd.ToString(),
						" 's ",
						defendcard.Name ?? "UnKnowCard",
						")"
					}));
				}
			}
			this._duel.Fields[attackcard.Controller].BattlingMonster = attackcard;
			this._duel.Fields[1 - attackcard.Controller].BattlingMonster = defendcard;
			this._duel.Fields[1 - attackcard.Controller].UnderAttack = true;
			if (ld == 0 && ca != 0)
			{
				this._ai.OnDirectAttack(attackcard);
			}
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0002E236 File Offset: 0x0002C436
		private void OnBattle(BinaryReader packet)
		{
			this._duel.Fields[0].UnderAttack = false;
			this._duel.Fields[1].UnderAttack = false;
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0002E236 File Offset: 0x0002C436
		private void OnAttackDisabled(BinaryReader packet)
		{
			this._duel.Fields[0].UnderAttack = false;
			this._duel.Fields[1].UnderAttack = false;
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0002E260 File Offset: 0x0002C460
		private void OnPosChange(BinaryReader packet)
		{
			packet.ReadInt32();
			int pc = this.GetLocalPlayer((int)packet.ReadByte());
			int pl = (int)packet.ReadByte();
			int ps = (int)packet.ReadSByte();
			int pp = (int)packet.ReadSByte();
			int cp = (int)packet.ReadSByte();
			ClientCard card = this._duel.GetCard(pc, (CardLocation)pl, ps);
			if (card != null)
			{
				card.Position = cp;
				if ((pp & 5) > 0 && (cp & 10) > 0)
				{
					card.ClearCardTargets();
				}
				if (this._debug)
				{
					string[] array = new string[5];
					array[0] = "(";
					array[1] = card.Name ?? "UnKnowCard";
					array[2] = " change position to ";
					int num = 3;
					CardPosition cardPosition = (CardPosition)cp;
					array[num] = cardPosition.ToString();
					array[4] = ")";
					Logger.WriteLine(string.Concat(array));
				}
			}
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0002E328 File Offset: 0x0002C528
		private void OnChaining(BinaryReader packet)
		{
			int cardId = packet.ReadInt32();
			int pcc = this.GetLocalPlayer((int)packet.ReadByte());
			int pcl = (int)packet.ReadByte();
			int pcs = (int)packet.ReadSByte();
			int subs = (int)packet.ReadSByte();
			ClientCard card = this._duel.GetCard(pcc, pcl, pcs, subs);
			if (card.Id == 0)
			{
				card.SetId(cardId);
			}
			int cc = this.GetLocalPlayer((int)packet.ReadByte());
			packet.ReadInt16();
			int desc = packet.ReadInt32();
			if (this._debug && card != null)
			{
				string[] array = new string[7];
				array[0] = "(";
				array[1] = cc.ToString();
				array[2] = " 's ";
				array[3] = card.Name ?? "UnKnowCard";
				array[4] = " activate effect from ";
				int num = 5;
				CardLocation cardLocation = (CardLocation)pcl;
				array[num] = cardLocation.ToString();
				array[6] = ")";
				Logger.WriteLine(string.Concat(array));
			}
			this._duel.LastChainLocation = (CardLocation)pcl;
			this._ai.OnChaining(card, cc);
			this._duel.ChainTargetOnly.Clear();
			this._duel.LastSummonPlayer = -1;
			this._duel.CurrentChain.Add(card);
			this._duel.CurrentChainInfo.Add(new ChainInfo(card, cc, desc));
			this._duel.LastChainPlayer = cc;
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0002E47C File Offset: 0x0002C67C
		private void OnChainSolving(BinaryReader packet)
		{
			int chainIndex = (int)packet.ReadByte();
			this._duel.SolvingChainIndex = chainIndex;
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0002E49C File Offset: 0x0002C69C
		private void OnChainNegated(BinaryReader packet)
		{
			int chainIndex = (int)packet.ReadByte();
			this._duel.NegatedChainIndexList.Add(chainIndex);
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0002E4C4 File Offset: 0x0002C6C4
		private void OnChainDisabled(BinaryReader packet)
		{
			int chainIndex = (int)packet.ReadByte();
			this._duel.NegatedChainIndexList.Add(chainIndex);
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0002E4EC File Offset: 0x0002C6EC
		private void OnChainSolved(BinaryReader packet)
		{
			int chainIndex = (int)packet.ReadByte();
			this._ai.OnChainSolved(chainIndex);
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0002E50C File Offset: 0x0002C70C
		private void OnChainEnd(BinaryReader packet)
		{
			this._ai.OnChainEnd();
			this._duel.LastChainPlayer = -1;
			this._duel.LastChainLocation = (CardLocation)0;
			this._duel.CurrentChain.Clear();
			this._duel.CurrentChainInfo.Clear();
			this._duel.ChainTargets.Clear();
			this._duel.LastChainTargets.Clear();
			this._duel.ChainTargetOnly.Clear();
			this._duel.SolvingChainIndex = 0;
			this._duel.NegatedChainIndexList.Clear();
			this._duel.SummoningCards.Clear();
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0002E5B8 File Offset: 0x0002C7B8
		private void OnCardSorting(BinaryReader packet)
		{
			this.GetLocalPlayer((int)packet.ReadByte());
			IList<ClientCard> originalCards = new List<ClientCard>();
			IList<ClientCard> cards = new List<ClientCard>();
			int count = (int)packet.ReadByte();
			for (int i = 0; i < count; i++)
			{
				int id = packet.ReadInt32();
				int controler = this.GetLocalPlayer((int)packet.ReadByte());
				CardLocation loc = (CardLocation)packet.ReadByte();
				int seq = (int)packet.ReadByte();
				ClientCard card;
				if ((loc & CardLocation.Overlay) != (CardLocation)0)
				{
					card = new ClientCard(id, CardLocation.Overlay, -1);
				}
				else
				{
					card = this._duel.GetCard(controler, loc, seq);
				}
				if (card != null)
				{
					if (id != 0)
					{
						card.SetId(id);
					}
					originalCards.Add(card);
					cards.Add(card);
				}
			}
			IList<ClientCard> selected = this._ai.OnCardSorting(cards);
			byte[] result = new byte[count];
			for (int j = 0; j < count; j++)
			{
				int id2 = 0;
				for (int k = 0; k < count; k++)
				{
					if (selected[k] != null && selected[k].Equals(originalCards[j]))
					{
						id2 = k;
						break;
					}
				}
				result[j] = (byte)id2;
			}
			BinaryWriter reply = GamePacketFactory.Create(CtosMessage.Response);
			reply.Write(result);
			this.Connection.Send(reply);
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0002E6EF File Offset: 0x0002C8EF
		private void OnChainSorting(BinaryReader packet)
		{
			GamePacketFactory.Create(CtosMessage.Response);
			this.Connection.Send(CtosMessage.Response, -1);
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0002E708 File Offset: 0x0002C908
		private void OnUpdateCard(BinaryReader packet)
		{
			int player = this.GetLocalPlayer((int)packet.ReadByte());
			int loc = (int)packet.ReadByte();
			int seq = (int)packet.ReadByte();
			packet.ReadInt32();
			ClientCard card = this._duel.GetCard(player, (CardLocation)loc, seq);
			if (card == null)
			{
				return;
			}
			card.Update(packet, this._duel);
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0002E758 File Offset: 0x0002C958
		private void OnUpdateData(BinaryReader packet)
		{
			int player = this.GetLocalPlayer((int)packet.ReadByte());
			CardLocation loc = (CardLocation)packet.ReadByte();
			IList<ClientCard> cards = null;
			if (loc <= CardLocation.SpellZone)
			{
				switch (loc)
				{
				case CardLocation.Deck:
					cards = this._duel.Fields[player].Deck;
					break;
				case CardLocation.Hand:
					cards = this._duel.Fields[player].Hand;
					break;
				case (CardLocation)3:
					break;
				case CardLocation.MonsterZone:
					cards = this._duel.Fields[player].MonsterZone;
					break;
				default:
					if (loc == CardLocation.SpellZone)
					{
						cards = this._duel.Fields[player].SpellZone;
					}
					break;
				}
			}
			else if (loc != CardLocation.Grave)
			{
				if (loc != CardLocation.Removed)
				{
					if (loc == CardLocation.Extra)
					{
						cards = this._duel.Fields[player].ExtraDeck;
					}
				}
				else
				{
					cards = this._duel.Fields[player].Banished;
				}
			}
			else
			{
				cards = this._duel.Fields[player].Graveyard;
			}
			if (cards != null)
			{
				foreach (ClientCard card in cards)
				{
					int len = packet.ReadInt32();
					long pos = packet.BaseStream.Position;
					if (len > 8)
					{
						card.Update(packet, this._duel);
					}
					packet.BaseStream.Position = pos + (long)len - 4L;
				}
			}
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0002E8BC File Offset: 0x0002CABC
		private void OnBecomeTarget(BinaryReader packet)
		{
			this._duel.LastChainTargets.Clear();
			int count = (int)packet.ReadByte();
			for (int i = 0; i < count; i++)
			{
				int player = this.GetLocalPlayer((int)packet.ReadByte());
				int loc = (int)packet.ReadByte();
				int seq = (int)packet.ReadByte();
				packet.ReadByte();
				ClientCard card = this._duel.GetCard(player, (CardLocation)loc, seq);
				if (card != null)
				{
					if (this._debug)
					{
						string[] array = new string[5];
						array[0] = "(";
						int num = 1;
						CardLocation cardLocation = (CardLocation)loc;
						array[num] = cardLocation.ToString();
						array[2] = " 's ";
						array[3] = card.Name ?? "UnKnowCard";
						array[4] = " become target)";
						Logger.WriteLine(string.Concat(array));
					}
					this._duel.ChainTargets.Add(card);
					this._duel.LastChainTargets.Add(card);
					this._duel.ChainTargetOnly.Add(card);
				}
			}
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0002E9BC File Offset: 0x0002CBBC
		private void OnSelectBattleCmd(BinaryReader packet)
		{
			packet.ReadByte();
			this._duel.BattlePhase = new BattlePhase();
			BattlePhase battle = this._duel.BattlePhase;
			int count = (int)packet.ReadByte();
			for (int i = 0; i < count; i++)
			{
				packet.ReadInt32();
				int con = this.GetLocalPlayer((int)packet.ReadByte());
				CardLocation loc = (CardLocation)packet.ReadByte();
				int seq = (int)packet.ReadByte();
				int desc = packet.ReadInt32();
				ClientCard card = this._duel.GetCard(con, loc, seq);
				if (card != null)
				{
					card.ActionIndex[0] = i;
					battle.ActivableCards.Add(card);
					battle.ActivableDescs.Add(desc);
				}
			}
			count = (int)packet.ReadByte();
			for (int j = 0; j < count; j++)
			{
				packet.ReadInt32();
				int con2 = this.GetLocalPlayer((int)packet.ReadByte());
				CardLocation loc2 = (CardLocation)packet.ReadByte();
				int seq2 = (int)packet.ReadByte();
				int diratt = (int)packet.ReadByte();
				ClientCard card2 = this._duel.GetCard(con2, loc2, seq2);
				if (card2 != null)
				{
					card2.ActionIndex[1] = j;
					if (diratt > 0)
					{
						card2.CanDirectAttack = true;
					}
					else
					{
						card2.CanDirectAttack = false;
					}
					battle.AttackableCards.Add(card2);
					card2.Attacked = false;
				}
			}
			foreach (ClientCard monster in this._duel.Fields[0].GetMonsters())
			{
				if (!battle.AttackableCards.Contains(monster))
				{
					monster.Attacked = true;
				}
			}
			battle.CanMainPhaseTwo = packet.ReadByte() > 0;
			battle.CanEndPhase = packet.ReadByte() > 0;
			this.Connection.Send(CtosMessage.Response, this._ai.OnSelectBattleCmd(battle).ToValue());
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x0002EB9C File Offset: 0x0002CD9C
		private void InternalOnSelectCard(BinaryReader packet, Func<IList<ClientCard>, int, int, int, bool, IList<ClientCard>> func)
		{
			packet.ReadByte();
			bool cancelable = packet.ReadByte() > 0;
			int min = (int)packet.ReadByte();
			int max = (int)packet.ReadByte();
			IList<ClientCard> cards = new List<ClientCard>();
			int count = (int)packet.ReadByte();
			for (int i = 0; i < count; i++)
			{
				int id = packet.ReadInt32();
				int player = this.GetLocalPlayer((int)packet.ReadByte());
				CardLocation loc = (CardLocation)packet.ReadByte();
				int seq = (int)packet.ReadByte();
				packet.ReadByte();
				ClientCard card;
				if ((loc & CardLocation.Overlay) != (CardLocation)0)
				{
					card = new ClientCard(id, CardLocation.Overlay, -1);
					CardLocation ownerLoc = loc ^ CardLocation.Overlay;
					ClientCard ownerCard = this._duel.GetCard(player, ownerLoc, seq);
					if (ownerCard != null)
					{
						card.OwnTargets.Add(ownerCard);
					}
				}
				else
				{
					card = this._duel.GetCard(player, loc, seq);
					card.Controller = player;
				}
				if (card != null)
				{
					if (card.Id == 0 || card.Location == CardLocation.Deck)
					{
						card.SetId(id);
					}
					cards.Add(card);
				}
			}
			if (this._select_hint == 575 && cancelable)
			{
				this._select_hint = 0;
				this.Connection.Send(CtosMessage.Response, -1);
				return;
			}
			IList<ClientCard> selected = func(cards, min, max, this._select_hint, cancelable);
			this._select_hint = 0;
			if (selected.Count == 0 && cancelable)
			{
				this.Connection.Send(CtosMessage.Response, -1);
				return;
			}
			byte[] result = new byte[selected.Count + 1];
			result[0] = (byte)selected.Count;
			for (int j = 0; j < selected.Count; j++)
			{
				int id2 = 0;
				for (int k = 0; k < count; k++)
				{
					if (cards[k] != null && cards[k].Equals(selected[j]))
					{
						id2 = k;
						break;
					}
				}
				result[j + 1] = (byte)id2;
			}
			BinaryWriter reply = GamePacketFactory.Create(CtosMessage.Response);
			reply.Write(result);
			this.Connection.Send(reply);
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0002ED9C File Offset: 0x0002CF9C
		private void InternalOnSelectUnselectCard(BinaryReader packet, Func<IList<ClientCard>, int, int, int, bool, IList<ClientCard>> func)
		{
			packet.ReadByte();
			bool finishable = packet.ReadByte() > 0;
			bool cancelable = packet.ReadByte() > 0 || finishable;
			packet.ReadByte();
			packet.ReadByte();
			IList<ClientCard> cards = new List<ClientCard>();
			int count = (int)packet.ReadByte();
			for (int i = 0; i < count; i++)
			{
				int id = packet.ReadInt32();
				int player = this.GetLocalPlayer((int)packet.ReadByte());
				CardLocation loc = (CardLocation)packet.ReadByte();
				int seq = (int)packet.ReadByte();
				packet.ReadByte();
				ClientCard card;
				if ((loc & CardLocation.Overlay) != (CardLocation)0)
				{
					card = new ClientCard(id, CardLocation.Overlay, -1);
				}
				else
				{
					card = this._duel.GetCard(player, loc, seq);
				}
				if (card != null)
				{
					if (card.Id == 0 || card.Location == CardLocation.Deck)
					{
						card.SetId(id);
					}
					cards.Add(card);
				}
			}
			int count2 = (int)packet.ReadByte();
			for (int j = 0; j < count2; j++)
			{
				int id2 = packet.ReadInt32();
				int player2 = this.GetLocalPlayer((int)packet.ReadByte());
				CardLocation loc2 = (CardLocation)packet.ReadByte();
				int seq2 = (int)packet.ReadByte();
				packet.ReadByte();
				ClientCard card2;
				if ((loc2 & CardLocation.Overlay) != (CardLocation)0)
				{
					card2 = new ClientCard(id2, CardLocation.Overlay, -1);
				}
				else
				{
					card2 = this._duel.GetCard(player2, loc2, seq2);
				}
				if (card2 != null && (card2.Id == 0 || card2.Location == CardLocation.Deck))
				{
					card2.SetId(id2);
				}
			}
			if (count2 == 0)
			{
				cancelable = false;
			}
			IList<ClientCard> selected = func(cards, finishable ? 0 : 1, 1, this._select_hint, cancelable);
			if (selected.Count == 0 && cancelable)
			{
				this.Connection.Send(CtosMessage.Response, -1);
				return;
			}
			byte[] result = new byte[selected.Count + 1];
			result[0] = (byte)selected.Count;
			for (int k = 0; k < selected.Count; k++)
			{
				int id3 = 0;
				for (int l = 0; l < count; l++)
				{
					if (cards[l] != null && cards[l].Equals(selected[k]))
					{
						id3 = l;
						break;
					}
				}
				result[k + 1] = (byte)id3;
			}
			BinaryWriter reply = GamePacketFactory.Create(CtosMessage.Response);
			reply.Write(result);
			this.Connection.Send(reply);
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0002EFE3 File Offset: 0x0002D1E3
		private void OnSelectCard(BinaryReader packet)
		{
			this.InternalOnSelectCard(packet, new Func<IList<ClientCard>, int, int, int, bool, IList<ClientCard>>(this._ai.OnSelectCard));
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0002EFFD File Offset: 0x0002D1FD
		private void OnSelectUnselectCard(BinaryReader packet)
		{
			this.InternalOnSelectUnselectCard(packet, new Func<IList<ClientCard>, int, int, int, bool, IList<ClientCard>>(this._ai.OnSelectCard));
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0002F018 File Offset: 0x0002D218
		private void OnSelectChain(BinaryReader packet)
		{
			packet.ReadByte();
			int count = (int)packet.ReadByte();
			packet.ReadByte();
			int hint = packet.ReadInt32();
			int hint2 = packet.ReadInt32();
			IList<ClientCard> cards = new List<ClientCard>();
			IList<int> descs = new List<int>();
			IList<bool> forces = new List<bool>();
			for (int i = 0; i < count; i++)
			{
				packet.ReadByte();
				bool forced = packet.ReadByte() > 0;
				int id = packet.ReadInt32();
				int con = this.GetLocalPlayer((int)packet.ReadByte());
				int loc = (int)packet.ReadByte();
				int seq = (int)packet.ReadByte();
				int sseq = (int)packet.ReadByte();
				int desc = packet.ReadInt32();
				if (desc == 221)
				{
					desc = 0;
				}
				ClientCard card = this._duel.GetCard(con, loc, seq, sseq);
				if (card.Id == 0)
				{
					card.SetId(id);
				}
				cards.Add(card);
				descs.Add(desc);
				forces.Add(forced);
			}
			if (cards.Count == 0)
			{
				this.Connection.Send(CtosMessage.Response, -1);
				return;
			}
			if (cards.Count == 1 && forces[0])
			{
				this.Connection.Send(CtosMessage.Response, 0);
				return;
			}
			this.Connection.Send(CtosMessage.Response, this._ai.OnSelectChain(cards, descs, forces, hint | hint2));
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x0002F160 File Offset: 0x0002D360
		private void OnSelectCounter(BinaryReader packet)
		{
			packet.ReadByte();
			int type = (int)packet.ReadInt16();
			int quantity = (int)packet.ReadInt16();
			IList<ClientCard> cards = new List<ClientCard>();
			IList<int> counters = new List<int>();
			int count = (int)packet.ReadByte();
			for (int i = 0; i < count; i++)
			{
				packet.ReadInt32();
				int player = this.GetLocalPlayer((int)packet.ReadByte());
				CardLocation loc = (CardLocation)packet.ReadByte();
				int seq = (int)packet.ReadByte();
				int num = (int)packet.ReadInt16();
				cards.Add(this._duel.GetCard(player, loc, seq));
				counters.Add(num);
			}
			IList<int> used = this._ai.OnSelectCounter(type, quantity, cards, counters);
			byte[] result = new byte[used.Count * 2];
			for (int j = 0; j < used.Count; j++)
			{
				result[j * 2] = (byte)(used[j] & 255);
				result[j * 2 + 1] = (byte)(used[j] >> 8);
			}
			BinaryWriter reply = GamePacketFactory.Create(CtosMessage.Response);
			reply.Write(result);
			this.Connection.Send(reply);
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x0002F274 File Offset: 0x0002D474
		private void OnSelectDisfield(BinaryReader packet)
		{
			packet.ReadByte();
			packet.ReadByte();
			int field = ~packet.ReadInt32();
			int player;
			CardLocation location;
			int filter;
			if ((field & 8323072) != 0)
			{
				player = 1;
				location = CardLocation.MonsterZone;
				filter = (field >> 16) & 127;
			}
			else if ((field & 520093696) != 0)
			{
				player = 1;
				location = CardLocation.SpellZone;
				filter = (field >> 24) & 31;
			}
			else if ((field & 127) != 0)
			{
				player = 0;
				location = CardLocation.MonsterZone;
				filter = field & 127;
			}
			else if ((field & 7936) != 0)
			{
				player = 0;
				location = CardLocation.SpellZone;
				filter = (field >> 8) & 31;
			}
			else if ((field & 8192) != 0)
			{
				player = 0;
				location = CardLocation.FieldZone;
				filter = 32;
			}
			else if ((field & 49152) != 0)
			{
				player = 0;
				location = CardLocation.PendulumZone;
				filter = (field >> 14) & 3;
			}
			else if ((field & 536870912) != 0)
			{
				player = 1;
				location = CardLocation.FieldZone;
				filter = 32;
			}
			else
			{
				player = 1;
				location = CardLocation.PendulumZone;
				filter = (field >> 30) & 3;
			}
			int selected = this._ai.OnSelectPlace(this._select_hint, player, location, filter);
			this._select_hint = 0;
			byte[] resp = new byte[3];
			resp[0] = (byte)this.GetLocalPlayer(player);
			if (location != CardLocation.PendulumZone && location != CardLocation.FieldZone)
			{
				resp[1] = (byte)location;
				if ((selected & filter) > 0)
				{
					filter &= selected;
				}
				if ((filter & 4) != 0)
				{
					resp[2] = 2;
				}
				else if ((filter & 2) != 0)
				{
					resp[2] = 1;
				}
				else if ((filter & 8) != 0)
				{
					resp[2] = 3;
				}
				else if ((filter & 1) != 0)
				{
					resp[2] = 0;
				}
				else if ((filter & 16) != 0)
				{
					resp[2] = 4;
				}
				else if ((filter & 64) != 0)
				{
					resp[2] = 6;
				}
				else if ((filter & 32) != 0)
				{
					resp[2] = 5;
				}
			}
			else
			{
				resp[1] = 8;
				if ((selected & filter) > 0)
				{
					filter &= selected;
				}
				if ((filter & 32) != 0)
				{
					resp[2] = 5;
				}
				if ((filter & 1) != 0)
				{
					resp[2] = 6;
				}
				if ((filter & 2) != 0)
				{
					resp[2] = 7;
				}
			}
			BinaryWriter reply = GamePacketFactory.Create(CtosMessage.Response);
			reply.Write(resp);
			this.Connection.Send(reply);
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0002F43C File Offset: 0x0002D63C
		private void OnSelectEffectYn(BinaryReader packet)
		{
			packet.ReadByte();
			int cardId = packet.ReadInt32();
			int player = this.GetLocalPlayer((int)packet.ReadByte());
			CardLocation loc = (CardLocation)packet.ReadByte();
			int seq = (int)packet.ReadByte();
			packet.ReadByte();
			int desc = packet.ReadInt32();
			if (desc == 0 || desc == 221)
			{
				desc = -1;
			}
			ClientCard card = this._duel.GetCard(player, loc, seq);
			if (card == null)
			{
				this.Connection.Send(CtosMessage.Response, 0);
				return;
			}
			if (card.Id == 0)
			{
				card.SetId(cardId);
			}
			int reply = (this._ai.OnSelectEffectYn(card, desc) ? 1 : 0);
			this.Connection.Send(CtosMessage.Response, reply);
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x0002F4EC File Offset: 0x0002D6EC
		private void OnSelectIdleCmd(BinaryReader packet)
		{
			packet.ReadByte();
			this._duel.MainPhase = new MainPhase();
			MainPhase main = this._duel.MainPhase;
			int count;
			for (int i = 0; i < 5; i++)
			{
				count = (int)packet.ReadByte();
				for (int j = 0; j < count; j++)
				{
					packet.ReadInt32();
					int con = this.GetLocalPlayer((int)packet.ReadByte());
					CardLocation loc = (CardLocation)packet.ReadByte();
					int seq = (int)packet.ReadByte();
					ClientCard card = this._duel.GetCard(con, loc, seq);
					if (card != null)
					{
						card.ActionIndex[i] = j;
						switch (i)
						{
						case 0:
							main.SummonableCards.Add(card);
							break;
						case 1:
							main.SpecialSummonableCards.Add(card);
							break;
						case 2:
							main.ReposableCards.Add(card);
							break;
						case 3:
							main.MonsterSetableCards.Add(card);
							break;
						case 4:
							main.SpellSetableCards.Add(card);
							break;
						}
					}
				}
			}
			count = (int)packet.ReadByte();
			for (int k = 0; k < count; k++)
			{
				packet.ReadInt32();
				int con2 = this.GetLocalPlayer((int)packet.ReadByte());
				CardLocation loc2 = (CardLocation)packet.ReadByte();
				int seq2 = (int)packet.ReadByte();
				int desc = packet.ReadInt32();
				ClientCard card2 = this._duel.GetCard(con2, loc2, seq2);
				if (card2 != null)
				{
					card2.ActionIndex[5] = k;
					if (card2.ActionActivateIndex.ContainsKey(desc))
					{
						card2.ActionActivateIndex.Remove(desc);
					}
					card2.ActionActivateIndex.Add(desc, k);
					main.ActivableCards.Add(card2);
					main.ActivableDescs.Add(desc);
				}
			}
			main.CanBattlePhase = packet.ReadByte() > 0;
			main.CanEndPhase = packet.ReadByte() > 0;
			packet.ReadByte();
			this.Connection.Send(CtosMessage.Response, this._ai.OnSelectIdleCmd(main).ToValue());
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0002F6EC File Offset: 0x0002D8EC
		private void OnSelectOption(BinaryReader packet)
		{
			IList<int> options = new List<int>();
			packet.ReadByte();
			int count = (int)packet.ReadByte();
			for (int i = 0; i < count; i++)
			{
				options.Add(packet.ReadInt32());
			}
			this.Connection.Send(CtosMessage.Response, this._ai.OnSelectOption(options));
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x0002F740 File Offset: 0x0002D940
		private void OnSelectPlace(BinaryReader packet)
		{
			packet.ReadByte();
			packet.ReadByte();
			int field = ~packet.ReadInt32();
			int player;
			CardLocation location;
			int filter;
			if ((field & 127) != 0)
			{
				player = 0;
				location = CardLocation.MonsterZone;
				filter = field & 127;
			}
			else if ((field & 7936) != 0)
			{
				player = 0;
				location = CardLocation.SpellZone;
				filter = (field >> 8) & 31;
			}
			else if ((field & 8192) != 0)
			{
				player = 0;
				location = CardLocation.FieldZone;
				filter = 32;
			}
			else if ((field & 49152) != 0)
			{
				player = 0;
				location = CardLocation.PendulumZone;
				filter = (field >> 14) & 3;
			}
			else if ((field & 8323072) != 0)
			{
				player = 1;
				location = CardLocation.MonsterZone;
				filter = (field >> 16) & 127;
			}
			else if ((field & 520093696) != 0)
			{
				player = 1;
				location = CardLocation.SpellZone;
				filter = (field >> 24) & 31;
			}
			else if ((field & 536870912) != 0)
			{
				player = 1;
				location = CardLocation.FieldZone;
				filter = 32;
			}
			else
			{
				player = 1;
				location = CardLocation.PendulumZone;
				filter = (field >> 30) & 3;
			}
			int selected = this._ai.OnSelectPlace(this._select_hint, player, location, filter);
			this._select_hint = 0;
			byte[] resp = new byte[3];
			resp[0] = (byte)this.GetLocalPlayer(player);
			if (location != CardLocation.PendulumZone && location != CardLocation.FieldZone)
			{
				resp[1] = (byte)location;
				if ((selected & filter) > 0)
				{
					filter &= selected;
				}
				if ((filter & 4) != 0)
				{
					resp[2] = 2;
				}
				else if ((filter & 2) != 0)
				{
					resp[2] = 1;
				}
				else if ((filter & 8) != 0)
				{
					resp[2] = 3;
				}
				else if ((filter & 1) != 0)
				{
					resp[2] = 0;
				}
				else if ((filter & 16) != 0)
				{
					resp[2] = 4;
				}
				else if ((filter & 64) != 0)
				{
					resp[2] = 6;
				}
				else if ((filter & 32) != 0)
				{
					resp[2] = 5;
				}
			}
			else
			{
				resp[1] = 8;
				if ((selected & filter) > 0)
				{
					filter &= selected;
				}
				if ((filter & 32) != 0)
				{
					resp[2] = 5;
				}
				if ((filter & 1) != 0)
				{
					resp[2] = 6;
				}
				if ((filter & 2) != 0)
				{
					resp[2] = 7;
				}
			}
			BinaryWriter reply = GamePacketFactory.Create(CtosMessage.Response);
			reply.Write(resp);
			this.Connection.Send(reply);
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x0002F90C File Offset: 0x0002DB0C
		private void OnSelectPosition(BinaryReader packet)
		{
			packet.ReadByte();
			int cardId = packet.ReadInt32();
			int pos = (int)packet.ReadByte();
			if (pos == 1 || pos == 2 || pos == 4 || pos == 8)
			{
				this.Connection.Send(CtosMessage.Response, pos);
				return;
			}
			IList<CardPosition> positions = new List<CardPosition>();
			if ((pos & 1) != 0)
			{
				positions.Add(CardPosition.FaceUpAttack);
			}
			if ((pos & 2) != 0)
			{
				positions.Add(CardPosition.FaceDownAttack);
			}
			if ((pos & 4) != 0)
			{
				positions.Add(CardPosition.FaceUpDefence);
			}
			if ((pos & 8) != 0)
			{
				positions.Add(CardPosition.FaceDownDefence);
			}
			this.Connection.Send(CtosMessage.Response, (int)this._ai.OnSelectPosition(cardId, positions));
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0002F99C File Offset: 0x0002DB9C
		private void OnSelectSum(BinaryReader packet)
		{
			bool mode = packet.ReadByte() == 0;
			packet.ReadByte();
			int sumval = packet.ReadInt32();
			int min = (int)packet.ReadByte();
			int max = (int)packet.ReadByte();
			if (max <= 0)
			{
				max = 99;
			}
			IList<ClientCard> mandatoryCards = new List<ClientCard>();
			IList<ClientCard> cards = new List<ClientCard>();
			for (int i = 0; i < 2; i++)
			{
				int count = (int)packet.ReadByte();
				for (int j = 0; j < count; j++)
				{
					int cardId = packet.ReadInt32();
					int player = this.GetLocalPlayer((int)packet.ReadByte());
					CardLocation loc = (CardLocation)packet.ReadByte();
					int seq = (int)packet.ReadByte();
					ClientCard card = this._duel.GetCard(player, loc, seq);
					if (cardId != 0 && card.Id != cardId)
					{
						card.SetId(cardId);
					}
					card.SelectSeq = j;
					int OpParam = packet.ReadInt32();
					int OpParam2 = OpParam & 65535;
					int OpParam3 = OpParam >> 16;
					if (((long)OpParam & (long)((ulong)(-2147483648))) > 0L)
					{
						OpParam2 = OpParam & int.MaxValue;
						OpParam3 = 0;
					}
					if (OpParam3 > 0 && OpParam2 > OpParam3)
					{
						card.OpParam1 = OpParam3;
						card.OpParam2 = OpParam2;
					}
					else
					{
						card.OpParam1 = OpParam2;
						card.OpParam2 = OpParam3;
					}
					if (i == 0)
					{
						mandatoryCards.Add(card);
					}
					else
					{
						cards.Add(card);
					}
				}
			}
			for (int k = 0; k < mandatoryCards.Count; k++)
			{
				sumval -= mandatoryCards[k].OpParam1;
			}
			IList<ClientCard> selected = this._ai.OnSelectSum(cards, sumval, min, max, this._select_hint, mode);
			this._select_hint = 0;
			byte[] result = new byte[mandatoryCards.Count + selected.Count + 1];
			int index = 0;
			result[index++] = (byte)(mandatoryCards.Count + selected.Count);
			while (index <= mandatoryCards.Count)
			{
				result[index++] = 0;
			}
			for (int l = 0; l < selected.Count; l++)
			{
				result[index++] = (byte)selected[l].SelectSeq;
			}
			BinaryWriter reply = GamePacketFactory.Create(CtosMessage.Response);
			reply.Write(result);
			this.Connection.Send(reply);
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x0002FBD1 File Offset: 0x0002DDD1
		private void OnSelectTribute(BinaryReader packet)
		{
			this.InternalOnSelectCard(packet, new Func<IList<ClientCard>, int, int, int, bool, IList<ClientCard>>(this._ai.OnSelectTribute));
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0002FBEC File Offset: 0x0002DDEC
		private void OnSelectYesNo(BinaryReader packet)
		{
			packet.ReadByte();
			int desc = packet.ReadInt32();
			int reply;
			if (desc == 30)
			{
				reply = (this._ai.OnSelectBattleReplay() ? 1 : 0);
			}
			else
			{
				reply = (this._ai.OnSelectYesNo(desc) ? 1 : 0);
			}
			this.Connection.Send(CtosMessage.Response, reply);
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0002FC40 File Offset: 0x0002DE40
		private void OnAnnounceAttrib(BinaryReader packet)
		{
			IList<CardAttribute> attributes = new List<CardAttribute>();
			packet.ReadByte();
			int count = (int)packet.ReadByte();
			int available = packet.ReadInt32();
			int filter = 1;
			for (int i = 0; i < 7; i++)
			{
				if ((available & filter) != 0)
				{
					attributes.Add((CardAttribute)filter);
				}
				filter <<= 1;
			}
			attributes = this._ai.OnAnnounceAttrib(count, attributes);
			int reply = 0;
			for (int j = 0; j < count; j++)
			{
				reply = (int)(reply + attributes[j]);
			}
			this.Connection.Send(CtosMessage.Response, reply);
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0002FCC8 File Offset: 0x0002DEC8
		private void OnAnnounceCard(BinaryReader packet)
		{
			IList<int> opcodes = new List<int>();
			packet.ReadByte();
			int count = (int)packet.ReadByte();
			for (int i = 0; i < count; i++)
			{
				opcodes.Add(packet.ReadInt32());
			}
			IList<int> avail = new List<int>();
			foreach (NamedCard card in NamedCardsManager.GetAllCards())
			{
				if (!card.HasType(CardType.Token) && (card.Alias <= 0 || card.Id - card.Alias >= 10))
				{
					Stack<int> stack = new Stack<int>();
					for (int j = 0; j < opcodes.Count; j++)
					{
						int num = opcodes[j];
						switch (num)
						{
						case 1073741824:
							if (stack.Count >= 2)
							{
								int rhs = stack.Pop();
								int lhs = stack.Pop();
								stack.Push(lhs + rhs);
							}
							break;
						case 1073741825:
							if (stack.Count >= 2)
							{
								int rhs2 = stack.Pop();
								int lhs2 = stack.Pop();
								stack.Push(lhs2 - rhs2);
							}
							break;
						case 1073741826:
							if (stack.Count >= 2)
							{
								int rhs3 = stack.Pop();
								int lhs3 = stack.Pop();
								stack.Push(lhs3 * rhs3);
							}
							break;
						case 1073741827:
							if (stack.Count >= 2)
							{
								int rhs4 = stack.Pop();
								int lhs4 = stack.Pop();
								stack.Push(lhs4 / rhs4);
							}
							break;
						case 1073741828:
							if (stack.Count >= 2)
							{
								int num2 = stack.Pop();
								int lhs5 = stack.Pop();
								bool flag = num2 != 0;
								bool b = lhs5 != 0;
								if (flag && b)
								{
									stack.Push(1);
								}
								else
								{
									stack.Push(0);
								}
							}
							break;
						case 1073741829:
							if (stack.Count >= 2)
							{
								int num3 = stack.Pop();
								int lhs6 = stack.Pop();
								bool flag2 = num3 != 0;
								bool b2 = lhs6 != 0;
								if (flag2 || b2)
								{
									stack.Push(1);
								}
								else
								{
									stack.Push(0);
								}
							}
							break;
						case 1073741830:
							if (stack.Count >= 1)
							{
								int rhs5 = stack.Pop();
								stack.Push(-rhs5);
							}
							break;
						case 1073741831:
							if (stack.Count >= 1)
							{
								if (stack.Pop() != 0)
								{
									stack.Push(0);
								}
								else
								{
									stack.Push(1);
								}
							}
							break;
						default:
							switch (num)
							{
							case 1073742080:
								if (stack.Count >= 1)
								{
									if (stack.Pop() == card.Id)
									{
										stack.Push(1);
									}
									else
									{
										stack.Push(0);
									}
								}
								break;
							case 1073742081:
								if (stack.Count >= 1)
								{
									if (card.HasSetcode(stack.Pop()))
									{
										stack.Push(1);
									}
									else
									{
										stack.Push(0);
									}
								}
								break;
							case 1073742082:
								if (stack.Count >= 1)
								{
									if ((stack.Pop() & card.Type) > 0)
									{
										stack.Push(1);
									}
									else
									{
										stack.Push(0);
									}
								}
								break;
							case 1073742083:
								if (stack.Count >= 1)
								{
									if ((stack.Pop() & card.Race) > 0)
									{
										stack.Push(1);
									}
									else
									{
										stack.Push(0);
									}
								}
								break;
							case 1073742084:
								if (stack.Count >= 1)
								{
									if ((stack.Pop() & card.Attribute) > 0)
									{
										stack.Push(1);
									}
									else
									{
										stack.Push(0);
									}
								}
								break;
							default:
								stack.Push(opcodes[j]);
								break;
							}
							break;
						}
					}
					if (stack.Count == 1 && stack.Pop() != 0)
					{
						avail.Add(card.Id);
					}
				}
			}
			if (avail.Count == 0)
			{
				throw new Exception("No avail card found for announce!");
			}
			this.Connection.Send(CtosMessage.Response, this._ai.OnAnnounceCard(avail));
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x00030108 File Offset: 0x0002E308
		private void OnAnnounceNumber(BinaryReader packet)
		{
			IList<int> numbers = new List<int>();
			packet.ReadByte();
			int count = (int)packet.ReadByte();
			for (int i = 0; i < count; i++)
			{
				numbers.Add(packet.ReadInt32());
			}
			this.Connection.Send(CtosMessage.Response, this._ai.OnAnnounceNumber(numbers));
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0003015C File Offset: 0x0002E35C
		private void OnAnnounceRace(BinaryReader packet)
		{
			IList<CardRace> races = new List<CardRace>();
			packet.ReadByte();
			int count = (int)packet.ReadByte();
			int available = packet.ReadInt32();
			int filter = 1;
			for (int i = 0; i < 26; i++)
			{
				if ((available & filter) != 0)
				{
					races.Add((CardRace)filter);
				}
				filter <<= 1;
			}
			races = this._ai.OnAnnounceRace(count, races);
			int reply = 0;
			for (int j = 0; j < count; j++)
			{
				reply = (int)(reply + races[j]);
			}
			this.Connection.Send(CtosMessage.Response, reply);
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x000301E4 File Offset: 0x0002E3E4
		private void OnRockPaperScissors(BinaryReader packet)
		{
			packet.ReadByte();
			int result;
			if (this._hand > 0)
			{
				result = this._hand;
			}
			else
			{
				result = this._ai.OnRockPaperScissors();
			}
			this.Connection.Send(CtosMessage.Response, result);
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x00030224 File Offset: 0x0002E424
		private void OnEquip(BinaryReader packet)
		{
			int equipCardControler = this.GetLocalPlayer((int)packet.ReadByte());
			int equipCardLocation = (int)packet.ReadByte();
			int equipCardSequence = (int)packet.ReadSByte();
			packet.ReadByte();
			int targetCardControler = this.GetLocalPlayer((int)packet.ReadByte());
			int targetCardLocation = (int)packet.ReadByte();
			int targetCardSequence = (int)packet.ReadSByte();
			packet.ReadByte();
			ClientCard equipCard = this._duel.GetCard(equipCardControler, (CardLocation)equipCardLocation, equipCardSequence);
			ClientCard targetCard = this._duel.GetCard(targetCardControler, (CardLocation)targetCardLocation, targetCardSequence);
			if (equipCard == null || targetCard == null)
			{
				return;
			}
			ClientCard equipTarget = equipCard.EquipTarget;
			if (equipTarget != null)
			{
				equipTarget.EquipCards.Remove(equipCard);
			}
			equipCard.EquipTarget = targetCard;
			targetCard.EquipCards.Add(equipCard);
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x000302D4 File Offset: 0x0002E4D4
		private void OnUnEquip(BinaryReader packet)
		{
			int equipCardControler = this.GetLocalPlayer((int)packet.ReadByte());
			int equipCardLocation = (int)packet.ReadByte();
			int equipCardSequence = (int)packet.ReadSByte();
			packet.ReadByte();
			ClientCard equipCard = this._duel.GetCard(equipCardControler, (CardLocation)equipCardLocation, equipCardSequence);
			if (equipCard == null)
			{
				return;
			}
			if (equipCard.EquipTarget != null)
			{
				equipCard.EquipTarget.EquipCards.Remove(equipCard);
				equipCard.EquipTarget = null;
			}
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x00030338 File Offset: 0x0002E538
		private void OnCardTarget(BinaryReader packet)
		{
			int ownerCardControler = this.GetLocalPlayer((int)packet.ReadByte());
			int ownerCardLocation = (int)packet.ReadByte();
			int ownerCardSequence = (int)packet.ReadSByte();
			packet.ReadByte();
			int targetCardControler = this.GetLocalPlayer((int)packet.ReadByte());
			int targetCardLocation = (int)packet.ReadByte();
			int targetCardSequence = (int)packet.ReadSByte();
			packet.ReadByte();
			ClientCard ownerCard = this._duel.GetCard(ownerCardControler, (CardLocation)ownerCardLocation, ownerCardSequence);
			ClientCard targetCard = this._duel.GetCard(targetCardControler, (CardLocation)targetCardLocation, targetCardSequence);
			if (ownerCard == null || targetCard == null)
			{
				return;
			}
			ownerCard.TargetCards.Add(targetCard);
			targetCard.OwnTargets.Add(ownerCard);
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x000303D4 File Offset: 0x0002E5D4
		private void OnCancelTarget(BinaryReader packet)
		{
			int ownerCardControler = this.GetLocalPlayer((int)packet.ReadByte());
			int ownerCardLocation = (int)packet.ReadByte();
			int ownerCardSequence = (int)packet.ReadSByte();
			packet.ReadByte();
			int targetCardControler = this.GetLocalPlayer((int)packet.ReadByte());
			int targetCardLocation = (int)packet.ReadByte();
			int targetCardSequence = (int)packet.ReadSByte();
			packet.ReadByte();
			ClientCard ownerCard = this._duel.GetCard(ownerCardControler, (CardLocation)ownerCardLocation, ownerCardSequence);
			ClientCard targetCard = this._duel.GetCard(targetCardControler, (CardLocation)targetCardLocation, targetCardSequence);
			if (ownerCard == null || targetCard == null)
			{
				return;
			}
			ownerCard.TargetCards.Remove(targetCard);
			targetCard.OwnTargets.Remove(ownerCard);
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x00030470 File Offset: 0x0002E670
		private void OnSummoning(BinaryReader packet)
		{
			this._duel.LastSummonedCards.Clear();
			packet.ReadInt32();
			int currentControler = this.GetLocalPlayer((int)packet.ReadByte());
			int currentLocation = (int)packet.ReadByte();
			int currentSequence = (int)packet.ReadSByte();
			packet.ReadSByte();
			ClientCard card = this._duel.GetCard(currentControler, (CardLocation)currentLocation, currentSequence);
			this._duel.SummoningCards.Add(card);
			this._duel.LastSummonPlayer = currentControler;
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x000304E4 File Offset: 0x0002E6E4
		private void OnSummoned(BinaryReader packet)
		{
			foreach (ClientCard card in this._duel.SummoningCards)
			{
				this._duel.LastSummonedCards.Add(card);
			}
			this._duel.SummoningCards.Clear();
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x00030550 File Offset: 0x0002E750
		private void OnSpSummoning(BinaryReader packet)
		{
			this._duel.LastSummonedCards.Clear();
			this._ai.CleanSelectMaterials();
			packet.ReadInt32();
			int currentControler = this.GetLocalPlayer((int)packet.ReadByte());
			int currentLocation = (int)packet.ReadByte();
			int currentSequence = (int)packet.ReadSByte();
			packet.ReadSByte();
			ClientCard card = this._duel.GetCard(currentControler, (CardLocation)currentLocation, currentSequence);
			this._duel.SummoningCards.Add(card);
			this._duel.LastSummonPlayer = currentControler;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x000305D0 File Offset: 0x0002E7D0
		private void OnSpSummoned(BinaryReader packet)
		{
			foreach (ClientCard card in this._duel.SummoningCards)
			{
				card.IsSpecialSummoned = true;
				this._duel.LastSummonedCards.Add(card);
			}
			this._ai.OnSpSummoned();
			this._duel.SummoningCards.Clear();
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x00030650 File Offset: 0x0002E850
		private void OnConfirmCards(BinaryReader packet)
		{
			packet.ReadByte();
			packet.ReadByte();
			int count = (int)packet.ReadByte();
			for (int i = 0; i < count; i++)
			{
				int cardId = packet.ReadInt32();
				int player = this.GetLocalPlayer((int)packet.ReadByte());
				int loc = (int)packet.ReadByte();
				int seq = (int)packet.ReadByte();
				ClientCard card = this._duel.GetCard(player, (CardLocation)loc, seq);
				if (cardId > 0)
				{
					card.SetId(cardId);
				}
				if (this._debug)
				{
					string[] array = new string[7];
					array[0] = "(Confirm ";
					array[1] = player.ToString();
					array[2] = "'s ";
					int num = 3;
					CardLocation cardLocation = (CardLocation)loc;
					array[num] = cardLocation.ToString();
					array[4] = " card: ";
					array[5] = card.Name ?? "UnKnowCard";
					array[6] = ")";
					Logger.WriteLine(string.Concat(array));
				}
			}
		}

		// Token: 0x04000DC7 RID: 3527
		private GameAI _ai;

		// Token: 0x04000DC8 RID: 3528
		private IDictionary<StocMessage, Action<BinaryReader>> _packets;

		// Token: 0x04000DC9 RID: 3529
		private IDictionary<GameMessage, Action<BinaryReader>> _messages;

		// Token: 0x04000DCA RID: 3530
		private Room _room;

		// Token: 0x04000DCB RID: 3531
		private Duel _duel;

		// Token: 0x04000DCC RID: 3532
		private int _hand;

		// Token: 0x04000DCD RID: 3533
		private bool _debug;

		// Token: 0x04000DCE RID: 3534
		private int _select_hint;

		// Token: 0x04000DCF RID: 3535
		private GameMessage _lastMessage;
	}
}
