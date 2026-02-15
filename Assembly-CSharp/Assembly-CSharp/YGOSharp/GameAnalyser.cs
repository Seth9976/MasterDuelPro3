using System;
using System.IO;
using YGOSharp.OCGWrapper.Enums;

namespace YGOSharp
{
	// Token: 0x020001B8 RID: 440
	public class GameAnalyser
	{
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x0002324B File Offset: 0x0002144B
		// (set) Token: 0x0600073B RID: 1851 RVA: 0x00023253 File Offset: 0x00021453
		public Game Game { get; private set; }

		// Token: 0x0600073C RID: 1852 RVA: 0x0002325C File Offset: 0x0002145C
		public GameAnalyser(Game game)
		{
			this.Game = game;
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0002326C File Offset: 0x0002146C
		public int Analyse(GameMessage msg, BinaryReader reader, byte[] raw)
		{
			CoreMessage cmsg = new CoreMessage(msg, reader, raw);
			switch (msg)
			{
			case GameMessage.Retry:
				this.OnRetry();
				return 1;
			case GameMessage.Hint:
				this.OnHint(cmsg);
				return 0;
			case GameMessage.Waiting:
			case GameMessage.Start:
			case GameMessage.UpdateData:
			case GameMessage.UpdateCard:
			case GameMessage.RequestDeck:
			case (GameMessage)9:
			case (GameMessage)17:
			case (GameMessage)27:
			case (GameMessage)28:
			case (GameMessage)29:
			case (GameMessage)43:
			case (GameMessage)44:
			case (GameMessage)45:
			case (GameMessage)46:
			case (GameMessage)47:
			case (GameMessage)48:
			case (GameMessage)49:
			case (GameMessage)51:
			case (GameMessage)52:
			case (GameMessage)57:
			case (GameMessage)58:
			case (GameMessage)59:
			case (GameMessage)66:
			case (GameMessage)67:
			case (GameMessage)68:
			case (GameMessage)69:
			case (GameMessage)77:
			case (GameMessage)78:
			case (GameMessage)79:
			case (GameMessage)82:
			case (GameMessage)84:
			case (GameMessage)85:
			case (GameMessage)86:
			case (GameMessage)87:
			case (GameMessage)88:
			case (GameMessage)89:
			case (GameMessage)98:
			case (GameMessage)99:
			case (GameMessage)103:
			case (GameMessage)104:
			case (GameMessage)105:
			case (GameMessage)106:
			case (GameMessage)107:
			case (GameMessage)108:
			case (GameMessage)109:
			case (GameMessage)115:
			case (GameMessage)116:
			case (GameMessage)117:
			case (GameMessage)118:
			case (GameMessage)119:
			case GameMessage.BeChainTarget:
			case GameMessage.CreateRelation:
			case GameMessage.ReleaseRelation:
			case (GameMessage)124:
			case (GameMessage)125:
			case (GameMessage)126:
			case (GameMessage)127:
			case (GameMessage)128:
			case (GameMessage)129:
			case (GameMessage)134:
			case (GameMessage)135:
			case (GameMessage)136:
			case (GameMessage)137:
			case (GameMessage)138:
			case (GameMessage)139:
			case (GameMessage)145:
			case (GameMessage)146:
			case (GameMessage)147:
			case (GameMessage)148:
			case (GameMessage)149:
			case (GameMessage)150:
			case (GameMessage)151:
			case (GameMessage)152:
			case (GameMessage)153:
			case (GameMessage)154:
			case (GameMessage)155:
			case (GameMessage)156:
			case (GameMessage)157:
			case (GameMessage)158:
			case (GameMessage)159:
				break;
			case GameMessage.Win:
				this.OnWin(cmsg);
				return 2;
			case GameMessage.SelectBattleCmd:
				this.OnSelectBattleCmd(cmsg);
				return 1;
			case GameMessage.SelectIdleCmd:
				this.OnSelectIdleCmd(cmsg);
				return 1;
			case GameMessage.SelectEffectYn:
				this.OnSelectEffectYn(cmsg);
				return 1;
			case GameMessage.SelectYesNo:
				this.OnSelectYesNo(cmsg);
				return 1;
			case GameMessage.SelectOption:
				this.OnSelectOption(cmsg);
				return 1;
			case GameMessage.SelectCard:
			case GameMessage.SelectTribute:
				this.OnSelectCard(cmsg);
				return 1;
			case GameMessage.SelectChain:
				return this.OnSelectChain(cmsg);
			case GameMessage.SelectPlace:
			case GameMessage.SelectPosition:
			case GameMessage.SelectDisfield:
				this.OnSelectPlace(cmsg);
				return 1;
			case GameMessage.SortChain:
			case GameMessage.SortCard:
				this.OnSortCard(cmsg);
				return 1;
			case GameMessage.SelectCounter:
				this.OnSelectCounter(cmsg);
				return 1;
			case GameMessage.SelectSum:
				this.OnSelectSum(cmsg);
				return 1;
			case GameMessage.SelectUnselect:
				this.OnSelectUnselect(cmsg);
				return 1;
			case GameMessage.ConfirmDecktop:
				this.OnConfirmDecktop(cmsg);
				return 0;
			case GameMessage.ConfirmCards:
				this.OnConfirmCards(cmsg);
				return 0;
			case GameMessage.ShuffleDeck:
			case GameMessage.RefreshDeck:
				this.SendToAll(cmsg, 1);
				return 0;
			case GameMessage.ShuffleHand:
				this.OnShuffleHand(cmsg);
				return 0;
			case GameMessage.SwapGraveDeck:
				this.OnSwapGraveDeck(cmsg);
				return 0;
			case GameMessage.ShuffleSetCard:
				this.OnShuffleSetCard(cmsg);
				return 0;
			case GameMessage.ReverseDeck:
				this.SendToAll(cmsg, 0);
				return 0;
			case GameMessage.DeckTop:
				this.SendToAll(cmsg, 6);
				return 0;
			case GameMessage.ShuffleExtra:
				this.OnShuffleExtra(cmsg);
				return 0;
			case GameMessage.NewTurn:
				this.OnNewTurn(cmsg);
				return 0;
			case GameMessage.NewPhase:
				this.OnNewPhase(cmsg);
				return 0;
			case GameMessage.ConfirmExtratop:
				this.OnConfirmExtratop(cmsg);
				return 0;
			case GameMessage.Move:
				this.OnMove(cmsg);
				return 0;
			case GameMessage.PosChange:
				this.OnPosChange(cmsg);
				return 0;
			case GameMessage.Set:
				this.OnSet(cmsg);
				return 0;
			case GameMessage.Swap:
				this.SendToAll(cmsg, 16);
				return 0;
			case GameMessage.FieldDisabled:
				this.SendToAll(cmsg, 4);
				return 0;
			case GameMessage.Summoning:
			case GameMessage.SpSummoning:
				this.SendToAll(cmsg, 8);
				return 0;
			case GameMessage.Summoned:
			case GameMessage.SpSummoned:
			case GameMessage.FlipSummoned:
				this.SendToAll(cmsg, 0);
				this.Game.RefreshMonsters(0, null);
				this.Game.RefreshMonsters(1, null);
				this.Game.RefreshSpells(0, null);
				this.Game.RefreshSpells(1, null);
				return 0;
			case GameMessage.FlipSummoning:
				this.OnFlipSummoning(cmsg);
				return 0;
			case GameMessage.Chaining:
				this.SendToAll(cmsg, 16);
				return 0;
			case GameMessage.Chained:
				this.SendToAll(cmsg, 1);
				this.Game.RefreshAll();
				return 0;
			case GameMessage.ChainSolving:
				this.SendToAll(cmsg, 1);
				return 0;
			case GameMessage.ChainSolved:
				this.SendToAll(cmsg, 1);
				this.Game.RefreshAll();
				return 0;
			case GameMessage.ChainEnd:
				this.SendToAll(cmsg, 0);
				this.Game.RefreshAll();
				return 0;
			case GameMessage.ChainNegated:
			case GameMessage.ChainDisabled:
				this.SendToAll(cmsg, 1);
				return 0;
			case GameMessage.CardSelected:
				this.OnCardSelected(cmsg);
				return 0;
			case GameMessage.RandomSelected:
				this.OnRandomSelected(cmsg);
				return 0;
			case GameMessage.BecomeTarget:
				this.OnBecomeTarget(cmsg);
				return 0;
			case GameMessage.Draw:
				this.OnDraw(cmsg);
				return 0;
			case GameMessage.Damage:
			case GameMessage.Recover:
			case GameMessage.LpUpdate:
			case GameMessage.PayLpCost:
				this.OnLpUpdate(cmsg);
				return 0;
			case GameMessage.Equip:
				this.SendToAll(cmsg, 8);
				return 0;
			case GameMessage.Unequip:
				this.SendToAll(cmsg, 4);
				return 0;
			case GameMessage.CardTarget:
			case GameMessage.CancelTarget:
				this.SendToAll(cmsg, 8);
				return 0;
			case GameMessage.AddCounter:
			case GameMessage.RemoveCounter:
				this.SendToAll(cmsg, 7);
				return 0;
			case GameMessage.Attack:
				this.SendToAll(cmsg, 8);
				return 0;
			case GameMessage.Battle:
				this.SendToAll(cmsg, 26);
				return 0;
			case GameMessage.AttackDisabled:
				this.SendToAll(cmsg, 0);
				return 0;
			case GameMessage.DamageStepStart:
			case GameMessage.DamageStepEnd:
				this.SendToAll(cmsg, 0);
				this.Game.RefreshMonsters(0, null);
				this.Game.RefreshMonsters(1, null);
				return 0;
			case GameMessage.MissedEffect:
				this.OnMissedEffect(cmsg);
				return 0;
			case GameMessage.TossCoin:
			case GameMessage.TossDice:
				this.OnTossCoin(cmsg);
				return 0;
			case GameMessage.RockPaperScissors:
				this.OnRockPaperScissors(cmsg);
				return 1;
			case GameMessage.HandResult:
				this.SendToAll(cmsg, 1);
				return 0;
			case GameMessage.AnnounceRace:
				this.OnAnnounceRace(cmsg);
				return 1;
			case GameMessage.AnnounceAttrib:
				this.OnAnnounceAttrib(cmsg);
				return 1;
			case GameMessage.AnnounceCard:
				this.OnAnnounceCard(cmsg);
				return 1;
			case GameMessage.AnnounceNumber:
				this.OnAnnounceNumber(cmsg);
				return 1;
			case GameMessage.AnnounceCardFilter:
				this.OnAnnounceCardFilter(cmsg);
				return 1;
			case GameMessage.CardHint:
				this.SendToAll(cmsg, 9);
				return 0;
			case GameMessage.TagSwap:
				this.OnTagSwap(cmsg);
				return 0;
			default:
				if (msg == GameMessage.PlayerHint)
				{
					this.SendToAll(cmsg, 6);
					return 0;
				}
				if (msg == GameMessage.MatchKill)
				{
					this.OnMatchKill(cmsg);
					return 0;
				}
				break;
			}
			throw new Exception("[GameAnalyser] Unhandled packet id: " + msg.ToString());
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x000238AC File Offset: 0x00021AAC
		private void OnRetry()
		{
			int player = this.Game.WaitForResponse();
			this.Game.CurPlayers[player].Send(GamePacketFactory.Create(GameMessage.Retry));
			this.Game.Replay.End();
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x000238F0 File Offset: 0x00021AF0
		private void OnHint(CoreMessage msg)
		{
			int type = (int)msg.Reader.ReadByte();
			int player = (int)msg.Reader.ReadByte();
			msg.Reader.ReadInt32();
			byte[] buffer = msg.CreateBuffer();
			BinaryWriter packet = GamePacketFactory.Create(msg.Message);
			packet.Write(buffer);
			switch (type)
			{
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
				this.Game.CurPlayers[player].Send(packet);
				return;
			case 6:
			case 7:
			case 8:
			case 9:
				this.Game.SendToAllBut(packet, player);
				return;
			case 10:
				if (this.Game.IsTag)
				{
					this.Game.CurPlayers[player].Send(packet);
					return;
				}
				this.Game.SendToAll(packet);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x000239BC File Offset: 0x00021BBC
		private void OnWin(CoreMessage msg)
		{
			int player = (int)msg.Reader.ReadByte();
			int reason = (int)msg.Reader.ReadByte();
			this.Game.MatchSaveResult(player, reason);
			this.SendToAll(msg);
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x000239F8 File Offset: 0x00021BF8
		private void OnSelectBattleCmd(CoreMessage msg)
		{
			int player = (int)msg.Reader.ReadByte();
			int count = (int)msg.Reader.ReadByte();
			msg.Reader.ReadBytes(count * 11);
			count = (int)msg.Reader.ReadByte();
			msg.Reader.ReadBytes(count * 8 + 2);
			this.Game.RefreshAll();
			this.Game.WaitForResponse(player);
			this.SendToPlayer(msg, player);
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00023A6C File Offset: 0x00021C6C
		private void OnSelectIdleCmd(CoreMessage msg)
		{
			int player = (int)msg.Reader.ReadByte();
			int count = (int)msg.Reader.ReadByte();
			msg.Reader.ReadBytes(count * 7);
			count = (int)msg.Reader.ReadByte();
			msg.Reader.ReadBytes(count * 7);
			count = (int)msg.Reader.ReadByte();
			msg.Reader.ReadBytes(count * 7);
			count = (int)msg.Reader.ReadByte();
			msg.Reader.ReadBytes(count * 7);
			count = (int)msg.Reader.ReadByte();
			msg.Reader.ReadBytes(count * 7);
			count = (int)msg.Reader.ReadByte();
			msg.Reader.ReadBytes(count * 11 + 3);
			this.Game.RefreshAll();
			this.Game.WaitForResponse(player);
			this.SendToPlayer(msg, player);
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00023B4C File Offset: 0x00021D4C
		private void OnSelectEffectYn(CoreMessage msg)
		{
			int player = (int)msg.Reader.ReadByte();
			msg.Reader.ReadBytes(13);
			this.Game.WaitForResponse(player);
			this.SendToPlayer(msg, player);
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00023B88 File Offset: 0x00021D88
		private void OnSelectYesNo(CoreMessage msg)
		{
			int player = (int)msg.Reader.ReadByte();
			msg.Reader.ReadBytes(4);
			this.Game.WaitForResponse(player);
			this.SendToPlayer(msg, player);
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x00023BC4 File Offset: 0x00021DC4
		private void OnSelectOption(CoreMessage msg)
		{
			int player = (int)msg.Reader.ReadByte();
			int count = (int)msg.Reader.ReadByte();
			msg.Reader.ReadBytes(count * 4);
			this.Game.WaitForResponse(player);
			this.SendToPlayer(msg, player);
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x00023C0C File Offset: 0x00021E0C
		private void OnSelectCard(CoreMessage msg)
		{
			BinaryWriter packet = GamePacketFactory.Create(msg.Message);
			int player = (int)msg.Reader.ReadByte();
			packet.Write((byte)player);
			packet.Write(msg.Reader.ReadBytes(3));
			int count = (int)msg.Reader.ReadByte();
			packet.Write((byte)count);
			for (int i = 0; i < count; i++)
			{
				int code = msg.Reader.ReadInt32();
				int pl = (int)msg.Reader.ReadByte();
				int loc = (int)msg.Reader.ReadByte();
				int seq = (int)msg.Reader.ReadByte();
				int pos = (int)msg.Reader.ReadByte();
				packet.Write((pl == player) ? code : 0);
				packet.Write((byte)pl);
				packet.Write((byte)loc);
				packet.Write((byte)seq);
				packet.Write((byte)pos);
			}
			this.Game.WaitForResponse(player);
			this.Game.CurPlayers[player].Send(packet);
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x00023D00 File Offset: 0x00021F00
		private void OnSelectUnselect(CoreMessage msg)
		{
			BinaryWriter packet = GamePacketFactory.Create(msg.Message);
			int player = (int)msg.Reader.ReadByte();
			packet.Write((byte)player);
			packet.Write(msg.Reader.ReadBytes(4));
			int count = (int)msg.Reader.ReadByte();
			packet.Write((byte)count);
			for (int i = 0; i < count; i++)
			{
				int code = msg.Reader.ReadInt32();
				int pl = (int)msg.Reader.ReadByte();
				int loc = (int)msg.Reader.ReadByte();
				int seq = (int)msg.Reader.ReadByte();
				int pos = (int)msg.Reader.ReadByte();
				packet.Write((pl == player) ? code : 0);
				packet.Write((byte)pl);
				packet.Write((byte)loc);
				packet.Write((byte)seq);
				packet.Write((byte)pos);
			}
			count = (int)msg.Reader.ReadByte();
			packet.Write((byte)count);
			for (int j = 0; j < count; j++)
			{
				int code2 = msg.Reader.ReadInt32();
				int pl2 = (int)msg.Reader.ReadByte();
				int loc2 = (int)msg.Reader.ReadByte();
				int seq2 = (int)msg.Reader.ReadByte();
				int pos2 = (int)msg.Reader.ReadByte();
				packet.Write((pl2 == player) ? code2 : 0);
				packet.Write((byte)pl2);
				packet.Write((byte)loc2);
				packet.Write((byte)seq2);
				packet.Write((byte)pos2);
			}
			this.Game.WaitForResponse(player);
			this.Game.CurPlayers[player].Send(packet);
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00023E8C File Offset: 0x0002208C
		private int OnSelectChain(CoreMessage msg)
		{
			int player = (int)msg.Reader.ReadByte();
			int count = (int)msg.Reader.ReadByte();
			msg.Reader.ReadBytes(10 + count * 13);
			if (count > 0)
			{
				this.Game.WaitForResponse(player);
				this.SendToPlayer(msg, player);
				return 1;
			}
			this.Game.SetResponse(-1);
			return 0;
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00023EEC File Offset: 0x000220EC
		private void OnSelectPlace(CoreMessage msg)
		{
			int player = (int)msg.Reader.ReadByte();
			msg.Reader.ReadBytes(5);
			this.Game.WaitForResponse(player);
			this.SendToPlayer(msg, player);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00023F28 File Offset: 0x00022128
		private void OnSelectCounter(CoreMessage msg)
		{
			int player = (int)msg.Reader.ReadByte();
			msg.Reader.ReadBytes(4);
			int count = (int)msg.Reader.ReadByte();
			msg.Reader.ReadBytes(count * 9);
			this.Game.WaitForResponse(player);
			this.SendToPlayer(msg, player);
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00023F80 File Offset: 0x00022180
		private void OnSelectSum(CoreMessage msg)
		{
			msg.Reader.ReadByte();
			int player = (int)msg.Reader.ReadByte();
			msg.Reader.ReadBytes(6);
			int count = (int)msg.Reader.ReadByte();
			msg.Reader.ReadBytes(count * 11);
			count = (int)msg.Reader.ReadByte();
			msg.Reader.ReadBytes(count * 11);
			this.Game.WaitForResponse(player);
			this.SendToPlayer(msg, player);
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x00024000 File Offset: 0x00022200
		private void OnSortCard(CoreMessage msg)
		{
			int player = (int)msg.Reader.ReadByte();
			int count = (int)msg.Reader.ReadByte();
			msg.Reader.ReadBytes(count * 7);
			this.Game.WaitForResponse(player);
			this.SendToPlayer(msg, player);
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00024048 File Offset: 0x00022248
		private void OnConfirmDecktop(CoreMessage msg)
		{
			msg.Reader.ReadByte();
			int count = (int)msg.Reader.ReadByte();
			msg.Reader.ReadBytes(count * 7);
			this.SendToAll(msg);
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x00024084 File Offset: 0x00022284
		private void OnConfirmExtratop(CoreMessage msg)
		{
			msg.Reader.ReadByte();
			int count = (int)msg.Reader.ReadByte();
			msg.Reader.ReadBytes(count * 7);
			this.SendToAll(msg);
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x000240C0 File Offset: 0x000222C0
		private void OnConfirmCards(CoreMessage msg)
		{
			int player = (int)msg.Reader.ReadByte();
			int count = (int)msg.Reader.ReadByte();
			msg.Reader.ReadBytes(count * 7);
			byte[] buffer = msg.CreateBuffer();
			BinaryWriter packet = GamePacketFactory.Create(msg.Message);
			packet.Write(buffer);
			if (buffer[7] == 2)
			{
				this.Game.SendToAll(packet);
				return;
			}
			this.Game.CurPlayers[player].Send(packet);
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00024134 File Offset: 0x00022334
		private void OnShuffleHand(CoreMessage msg)
		{
			BinaryWriter packet = GamePacketFactory.Create(msg.Message);
			int player = (int)msg.Reader.ReadByte();
			int count = (int)msg.Reader.ReadByte();
			packet.Write((byte)player);
			packet.Write((byte)count);
			msg.Reader.ReadBytes(count * 4);
			for (int i = 0; i < count; i++)
			{
				packet.Write(0);
			}
			this.SendToPlayer(msg, player);
			this.Game.SendToAllBut(packet, player);
			this.Game.RefreshHand(player, null);
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x000241BC File Offset: 0x000223BC
		private void OnShuffleExtra(CoreMessage msg)
		{
			BinaryWriter packet = GamePacketFactory.Create(msg.Message);
			int player = (int)msg.Reader.ReadByte();
			int count = (int)msg.Reader.ReadByte();
			packet.Write((byte)player);
			packet.Write((byte)count);
			msg.Reader.ReadBytes(count * 4);
			for (int i = 0; i < count; i++)
			{
				packet.Write(0);
			}
			this.SendToPlayer(msg, player);
			this.Game.SendToAllBut(packet, player);
			this.Game.RefreshHand(player, null);
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00024244 File Offset: 0x00022444
		private void OnSwapGraveDeck(CoreMessage msg)
		{
			int player = (int)msg.Reader.ReadByte();
			this.SendToAll(msg);
			this.Game.RefreshGrave(player, null);
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00024274 File Offset: 0x00022474
		private void OnShuffleSetCard(CoreMessage msg)
		{
			int num = (int)msg.Reader.ReadByte();
			int count = (int)msg.Reader.ReadByte();
			msg.Reader.ReadBytes(count * 8);
			this.SendToAll(msg);
			if (num == 4)
			{
				this.Game.RefreshMonsters(0, null);
				this.Game.RefreshMonsters(1, null);
				return;
			}
			this.Game.RefreshSpells(0, null);
			this.Game.RefreshSpells(1, null);
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x000242E8 File Offset: 0x000224E8
		private void OnNewTurn(CoreMessage msg)
		{
			this.Game.TimeReset();
			if (!this.Game.IsTag)
			{
				this.Game.RefreshAll();
			}
			this.Game.CurrentPlayer = (int)msg.Reader.ReadByte();
			this.SendToAll(msg);
			Game game = this.Game;
			int turnCount = game.TurnCount;
			game.TurnCount = turnCount + 1;
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0002434A File Offset: 0x0002254A
		private void OnNewPhase(CoreMessage msg)
		{
			msg.Reader.ReadInt16();
			this.SendToAll(msg);
			this.Game.RefreshAll();
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0002436C File Offset: 0x0002256C
		private void OnMove(CoreMessage msg)
		{
			byte[] raw = msg.Reader.ReadBytes(16);
			int pc = (int)raw[4];
			int pl = (int)raw[5];
			int cc = (int)raw[8];
			int cl = (int)raw[9];
			int cs = (int)raw[10];
			int cp = (int)raw[11];
			this.SendToPlayer(msg, cc);
			BinaryWriter packet = GamePacketFactory.Create(msg.Message);
			packet.Write(raw);
			if ((!Convert.ToBoolean(cl & 144) && Convert.ToBoolean(cl & 3)) || Convert.ToBoolean(cp & 10))
			{
				packet.BaseStream.Position = 2L;
				packet.Write(0);
			}
			this.Game.SendToAllBut(packet, cc);
			if (cl != 0 && (cl & 128) == 0 && (cl != pl || pc != cc))
			{
				this.Game.RefreshSingle(cc, cl, cs);
			}
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00024434 File Offset: 0x00022634
		private void OnPosChange(CoreMessage msg)
		{
			byte[] array = msg.Reader.ReadBytes(9);
			this.SendToAll(msg);
			int cc = (int)array[4];
			int cl = (int)array[5];
			int cs = (int)array[6];
			int pp = (int)array[7];
			int cp = (int)array[8];
			if ((pp & 10) != 0 && (cp & 5) != 0)
			{
				this.Game.RefreshSingle(cc, cl, cs);
			}
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00024484 File Offset: 0x00022684
		private void OnSet(CoreMessage msg)
		{
			msg.Reader.ReadBytes(4);
			byte[] raw = msg.Reader.ReadBytes(4);
			BinaryWriter packet = GamePacketFactory.Create(GameMessage.Set);
			packet.Write(0);
			packet.Write(raw);
			this.Game.SendToAll(packet);
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x000244D0 File Offset: 0x000226D0
		private void OnFlipSummoning(CoreMessage msg)
		{
			byte[] raw = msg.Reader.ReadBytes(8);
			this.Game.RefreshSingle((int)raw[4], (int)raw[5], (int)raw[6]);
			this.SendToAll(msg);
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00024508 File Offset: 0x00022708
		private void OnCardSelected(CoreMessage msg)
		{
			msg.Reader.ReadByte();
			int count = (int)msg.Reader.ReadByte();
			msg.Reader.ReadBytes(count * 4);
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0002453C File Offset: 0x0002273C
		private void OnRandomSelected(CoreMessage msg)
		{
			msg.Reader.ReadByte();
			int count = (int)msg.Reader.ReadByte();
			msg.Reader.ReadBytes(count * 4);
			this.SendToAll(msg);
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00024578 File Offset: 0x00022778
		private void OnBecomeTarget(CoreMessage msg)
		{
			int count = (int)msg.Reader.ReadByte();
			msg.Reader.ReadBytes(count * 4);
			this.SendToAll(msg);
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x000245A8 File Offset: 0x000227A8
		private void OnDraw(CoreMessage msg)
		{
			BinaryWriter packet = GamePacketFactory.Create(msg.Message);
			int player = (int)msg.Reader.ReadByte();
			int count = (int)msg.Reader.ReadByte();
			packet.Write((byte)player);
			packet.Write((byte)count);
			for (int i = 0; i < count; i++)
			{
				uint code = msg.Reader.ReadUInt32();
				if ((code & 2147483648U) != 0U)
				{
					packet.Write(code);
				}
				else
				{
					packet.Write(0);
				}
			}
			this.SendToTeam(msg, player);
			this.SendToOpponentTeam(packet, player);
			this.Game.SendToObservers(packet);
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0002463C File Offset: 0x0002283C
		private void OnLpUpdate(CoreMessage msg)
		{
			int player = (int)msg.Reader.ReadByte();
			int value = msg.Reader.ReadInt32();
			GameMessage message = msg.Message;
			switch (message)
			{
			case GameMessage.Damage:
				break;
			case GameMessage.Recover:
				this.Game.LifePoints[player] += value;
				goto IL_009B;
			case GameMessage.Equip:
				goto IL_009B;
			case GameMessage.LpUpdate:
				this.Game.LifePoints[player] = value;
				goto IL_009B;
			default:
				if (message != GameMessage.PayLpCost)
				{
					goto IL_009B;
				}
				break;
			}
			this.Game.LifePoints[player] -= value;
			if (this.Game.LifePoints[player] < 0)
			{
				this.Game.LifePoints[player] = 0;
			}
			IL_009B:
			this.SendToAll(msg);
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x000246EC File Offset: 0x000228EC
		private void OnMissedEffect(CoreMessage msg)
		{
			int player = (int)msg.Reader.ReadByte();
			msg.Reader.ReadBytes(7);
			this.SendToPlayer(msg, player);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0002471C File Offset: 0x0002291C
		private void OnTossCoin(CoreMessage msg)
		{
			msg.Reader.ReadByte();
			int count = (int)msg.Reader.ReadByte();
			msg.Reader.ReadBytes(count);
			this.SendToAll(msg);
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x00024758 File Offset: 0x00022958
		private void OnRockPaperScissors(CoreMessage msg)
		{
			int player = (int)msg.Reader.ReadByte();
			this.Game.WaitForResponse(player);
			this.SendToPlayer(msg, player);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x00024788 File Offset: 0x00022988
		private void OnAnnounceRace(CoreMessage msg)
		{
			int player = (int)msg.Reader.ReadByte();
			msg.Reader.ReadBytes(5);
			this.Game.WaitForResponse(player);
			this.SendToPlayer(msg, player);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x000247C4 File Offset: 0x000229C4
		private void OnAnnounceAttrib(CoreMessage msg)
		{
			int player = (int)msg.Reader.ReadByte();
			msg.Reader.ReadBytes(5);
			this.Game.WaitForResponse(player);
			this.SendToPlayer(msg, player);
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00024800 File Offset: 0x00022A00
		private void OnAnnounceCard(CoreMessage msg)
		{
			int player = (int)msg.Reader.ReadByte();
			msg.Reader.ReadBytes(4);
			this.Game.WaitForResponse(player);
			this.SendToPlayer(msg, player);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0002483C File Offset: 0x00022A3C
		private void OnAnnounceNumber(CoreMessage msg)
		{
			int player = (int)msg.Reader.ReadByte();
			int count = (int)msg.Reader.ReadByte();
			msg.Reader.ReadBytes(count * 4);
			this.Game.WaitForResponse(player);
			this.SendToPlayer(msg, player);
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00024884 File Offset: 0x00022A84
		private void OnAnnounceCardFilter(CoreMessage msg)
		{
			int player = (int)msg.Reader.ReadByte();
			int count = (int)msg.Reader.ReadByte();
			msg.Reader.ReadBytes(count * 4);
			this.Game.WaitForResponse(player);
			this.SendToPlayer(msg, player);
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x000248CC File Offset: 0x00022ACC
		private void OnMatchKill(CoreMessage msg)
		{
			msg.Reader.ReadInt32();
			if (this.Game.IsMatch)
			{
				this.Game.MatchKill();
				this.SendToAll(msg);
			}
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x000248FC File Offset: 0x00022AFC
		private void OnTagSwap(CoreMessage msg)
		{
			BinaryWriter packet = GamePacketFactory.Create(GameMessage.TagSwap);
			int player = (int)msg.Reader.ReadByte();
			packet.Write((byte)player);
			packet.Write(msg.Reader.ReadByte());
			int ecount = (int)msg.Reader.ReadByte();
			packet.Write((byte)ecount);
			packet.Write(msg.Reader.ReadByte());
			int hcount = (int)msg.Reader.ReadByte();
			packet.Write((byte)hcount);
			packet.Write(msg.Reader.ReadBytes(4));
			for (int i = 0; i < hcount + ecount; i++)
			{
				uint code = msg.Reader.ReadUInt32();
				if ((code & 2147483648U) != 0U)
				{
					packet.Write(code);
				}
				else
				{
					packet.Write(0);
				}
			}
			if (this.Game.CurPlayers[player].Equals(this.Game.Players[player * 2]))
			{
				this.Game.CurPlayers[player] = this.Game.Players[player * 2 + 1];
			}
			else
			{
				this.Game.CurPlayers[player] = this.Game.Players[player * 2];
			}
			this.SendToPlayer(msg, player);
			this.Game.SendToAllBut(packet, player);
			this.Game.RefreshExtra(player, null);
			this.Game.RefreshMonsters(0, null);
			this.Game.RefreshMonsters(1, null);
			this.Game.RefreshSpells(0, null);
			this.Game.RefreshSpells(1, null);
			this.Game.RefreshHand(0, null);
			this.Game.RefreshHand(1, null);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00024A90 File Offset: 0x00022C90
		private void SendToAll(CoreMessage msg)
		{
			byte[] buffer = msg.CreateBuffer();
			BinaryWriter packet = GamePacketFactory.Create(msg.Message);
			packet.Write(buffer);
			this.Game.SendToAll(packet);
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00024AC3 File Offset: 0x00022CC3
		private void SendToAll(CoreMessage msg, int length)
		{
			if (length == 0)
			{
				this.Game.SendToAll(GamePacketFactory.Create(msg.Message));
				return;
			}
			msg.Reader.ReadBytes(length);
			this.SendToAll(msg);
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x00024AF4 File Offset: 0x00022CF4
		private void SendToPlayer(CoreMessage msg, int player)
		{
			if (player != 0 && player != 1)
			{
				return;
			}
			byte[] buffer = msg.CreateBuffer();
			BinaryWriter packet = GamePacketFactory.Create(msg.Message);
			packet.Write(buffer);
			this.Game.CurPlayers[player].Send(packet);
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00024B38 File Offset: 0x00022D38
		private void SendToTeam(CoreMessage msg, int player)
		{
			if (player != 0 && player != 1)
			{
				return;
			}
			byte[] buffer = msg.CreateBuffer();
			BinaryWriter packet = GamePacketFactory.Create(msg.Message);
			packet.Write(buffer);
			this.Game.SendToTeam(packet, player);
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00024B74 File Offset: 0x00022D74
		private void SendToOpponentTeam(BinaryWriter packet, int player)
		{
			if (player != 0 && player != 1)
			{
				return;
			}
			this.Game.SendToTeam(packet, 1 - player);
		}
	}
}
