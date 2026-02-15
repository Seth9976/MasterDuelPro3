using System;
using System.Collections.Generic;
using System.IO;
using YGOSharp.Network.Enums;
using YGOSharp.Network.Utils;
using YGOSharp.OCGWrapper;
using YGOSharp.OCGWrapper.Enums;

namespace YGOSharp
{
	// Token: 0x020001B7 RID: 439
	public class Game : IGame
	{
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x00020B05 File Offset: 0x0001ED05
		// (set) Token: 0x060006B3 RID: 1715 RVA: 0x00020B0D File Offset: 0x0001ED0D
		public Banlist Banlist { get; private set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x00020B16 File Offset: 0x0001ED16
		// (set) Token: 0x060006B5 RID: 1717 RVA: 0x00020B1E File Offset: 0x0001ED1E
		public int Mode { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x00020B27 File Offset: 0x0001ED27
		// (set) Token: 0x060006B7 RID: 1719 RVA: 0x00020B2F File Offset: 0x0001ED2F
		public int Region { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x00020B38 File Offset: 0x0001ED38
		// (set) Token: 0x060006B9 RID: 1721 RVA: 0x00020B40 File Offset: 0x0001ED40
		public int MasterRule { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x00020B49 File Offset: 0x0001ED49
		// (set) Token: 0x060006BB RID: 1723 RVA: 0x00020B51 File Offset: 0x0001ED51
		public int StartLp { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x00020B5A File Offset: 0x0001ED5A
		// (set) Token: 0x060006BD RID: 1725 RVA: 0x00020B62 File Offset: 0x0001ED62
		public int StartHand { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x00020B6B File Offset: 0x0001ED6B
		// (set) Token: 0x060006BF RID: 1727 RVA: 0x00020B73 File Offset: 0x0001ED73
		public int DrawCount { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x00020B7C File Offset: 0x0001ED7C
		// (set) Token: 0x060006C1 RID: 1729 RVA: 0x00020B84 File Offset: 0x0001ED84
		public int Timer { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x00020B8D File Offset: 0x0001ED8D
		// (set) Token: 0x060006C3 RID: 1731 RVA: 0x00020B95 File Offset: 0x0001ED95
		public bool EnablePriority { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x00020B9E File Offset: 0x0001ED9E
		// (set) Token: 0x060006C5 RID: 1733 RVA: 0x00020BA6 File Offset: 0x0001EDA6
		public bool NoCheckDeck { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x00020BAF File Offset: 0x0001EDAF
		// (set) Token: 0x060006C7 RID: 1735 RVA: 0x00020BB7 File Offset: 0x0001EDB7
		public bool NoShuffleDeck { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x00020BC0 File Offset: 0x0001EDC0
		// (set) Token: 0x060006C9 RID: 1737 RVA: 0x00020BC8 File Offset: 0x0001EDC8
		public bool IsMatch { get; private set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x00020BD1 File Offset: 0x0001EDD1
		// (set) Token: 0x060006CB RID: 1739 RVA: 0x00020BD9 File Offset: 0x0001EDD9
		public bool IsTag { get; private set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x00020BE2 File Offset: 0x0001EDE2
		// (set) Token: 0x060006CD RID: 1741 RVA: 0x00020BEA File Offset: 0x0001EDEA
		public bool IsTpSelect { get; private set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x00020BF3 File Offset: 0x0001EDF3
		// (set) Token: 0x060006CF RID: 1743 RVA: 0x00020BFB File Offset: 0x0001EDFB
		public GameState State { get; private set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x00020C04 File Offset: 0x0001EE04
		// (set) Token: 0x060006D1 RID: 1745 RVA: 0x00020C0C File Offset: 0x0001EE0C
		public DateTime SideTimer { get; private set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x00020C15 File Offset: 0x0001EE15
		// (set) Token: 0x060006D3 RID: 1747 RVA: 0x00020C1D File Offset: 0x0001EE1D
		public DateTime TpTimer { get; private set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x00020C26 File Offset: 0x0001EE26
		// (set) Token: 0x060006D5 RID: 1749 RVA: 0x00020C2E File Offset: 0x0001EE2E
		public DateTime RpsTimer { get; private set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x00020C37 File Offset: 0x0001EE37
		// (set) Token: 0x060006D7 RID: 1751 RVA: 0x00020C3F File Offset: 0x0001EE3F
		public int TurnCount { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x00020C48 File Offset: 0x0001EE48
		// (set) Token: 0x060006D9 RID: 1753 RVA: 0x00020C50 File Offset: 0x0001EE50
		public int CurrentPlayer { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x00020C59 File Offset: 0x0001EE59
		// (set) Token: 0x060006DB RID: 1755 RVA: 0x00020C61 File Offset: 0x0001EE61
		public int[] LifePoints { get; set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x00020C6A File Offset: 0x0001EE6A
		// (set) Token: 0x060006DD RID: 1757 RVA: 0x00020C72 File Offset: 0x0001EE72
		public Player[] Players { get; private set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x00020C7B File Offset: 0x0001EE7B
		// (set) Token: 0x060006DF RID: 1759 RVA: 0x00020C83 File Offset: 0x0001EE83
		public Player[] CurPlayers { get; private set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x00020C8C File Offset: 0x0001EE8C
		// (set) Token: 0x060006E1 RID: 1761 RVA: 0x00020C94 File Offset: 0x0001EE94
		public bool[] IsReady { get; private set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x00020C9D File Offset: 0x0001EE9D
		// (set) Token: 0x060006E3 RID: 1763 RVA: 0x00020CA5 File Offset: 0x0001EEA5
		public List<Player> Observers { get; private set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x00020CAE File Offset: 0x0001EEAE
		// (set) Token: 0x060006E5 RID: 1765 RVA: 0x00020CB6 File Offset: 0x0001EEB6
		public Player HostPlayer { get; private set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x00020CBF File Offset: 0x0001EEBF
		// (set) Token: 0x060006E7 RID: 1767 RVA: 0x00020CC7 File Offset: 0x0001EEC7
		public Replay Replay { get; private set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x00020CD0 File Offset: 0x0001EED0
		// (set) Token: 0x060006E9 RID: 1769 RVA: 0x00020CD8 File Offset: 0x0001EED8
		public int Winner { get; private set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x00020CE1 File Offset: 0x0001EEE1
		// (set) Token: 0x060006EB RID: 1771 RVA: 0x00020CE9 File Offset: 0x0001EEE9
		public int[] MatchResults { get; private set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x00020CF2 File Offset: 0x0001EEF2
		// (set) Token: 0x060006ED RID: 1773 RVA: 0x00020CFA File Offset: 0x0001EEFA
		public int[] MatchReasons { get; private set; }

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060006EE RID: 1774 RVA: 0x00020D04 File Offset: 0x0001EF04
		// (remove) Token: 0x060006EF RID: 1775 RVA: 0x00020D3C File Offset: 0x0001EF3C
		public event Action<object, EventArgs> OnNetworkReady;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060006F0 RID: 1776 RVA: 0x00020D74 File Offset: 0x0001EF74
		// (remove) Token: 0x060006F1 RID: 1777 RVA: 0x00020DAC File Offset: 0x0001EFAC
		public event Action<object, EventArgs> OnNetworkEnd;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060006F2 RID: 1778 RVA: 0x00020DE4 File Offset: 0x0001EFE4
		// (remove) Token: 0x060006F3 RID: 1779 RVA: 0x00020E1C File Offset: 0x0001F01C
		public event Action<object, EventArgs> OnGameStart;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060006F4 RID: 1780 RVA: 0x00020E54 File Offset: 0x0001F054
		// (remove) Token: 0x060006F5 RID: 1781 RVA: 0x00020E8C File Offset: 0x0001F08C
		public event Action<object, EventArgs> OnGameEnd;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060006F6 RID: 1782 RVA: 0x00020EC4 File Offset: 0x0001F0C4
		// (remove) Token: 0x060006F7 RID: 1783 RVA: 0x00020EFC File Offset: 0x0001F0FC
		public event Action<object, EventArgs> OnDuelEnd;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060006F8 RID: 1784 RVA: 0x00020F34 File Offset: 0x0001F134
		// (remove) Token: 0x060006F9 RID: 1785 RVA: 0x00020F6C File Offset: 0x0001F16C
		public event Action<object, PlayerEventArgs> OnPlayerJoin;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060006FA RID: 1786 RVA: 0x00020FA4 File Offset: 0x0001F1A4
		// (remove) Token: 0x060006FB RID: 1787 RVA: 0x00020FDC File Offset: 0x0001F1DC
		public event Action<object, PlayerEventArgs> OnPlayerLeave;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060006FC RID: 1788 RVA: 0x00021014 File Offset: 0x0001F214
		// (remove) Token: 0x060006FD RID: 1789 RVA: 0x0002104C File Offset: 0x0001F24C
		public event Action<object, PlayerMoveEventArgs> OnPlayerMove;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060006FE RID: 1790 RVA: 0x00021084 File Offset: 0x0001F284
		// (remove) Token: 0x060006FF RID: 1791 RVA: 0x000210BC File Offset: 0x0001F2BC
		public event Action<object, PlayerEventArgs> OnPlayerReady;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000700 RID: 1792 RVA: 0x000210F4 File Offset: 0x0001F2F4
		// (remove) Token: 0x06000701 RID: 1793 RVA: 0x0002112C File Offset: 0x0001F32C
		public event Action<object, PlayerChatEventArgs> OnPlayerChat;

		// Token: 0x06000702 RID: 1794 RVA: 0x00021164 File Offset: 0x0001F364
		public Game(CoreServer server)
		{
			this.State = GameState.Lobby;
			this.Mode = Config.GetInt("Mode", 0);
			this.Region = Config.GetInt("Rule", -1);
			if (this.Region != -1)
			{
				Console.Error.WriteLine("'Rule' is deprecated, please use 'Region' instead.");
			}
			else
			{
				this.Region = Config.GetInt("Region", 0);
			}
			this.MasterRule = Config.GetInt("MasterRule", 3);
			this.IsMatch = this.Mode == 1;
			this.IsTag = this.Mode == 2;
			this.CurrentPlayer = 0;
			this.LifePoints = new int[2];
			this.Players = new Player[this.IsTag ? 4 : 2];
			this.CurPlayers = new Player[2];
			this.IsReady = new bool[this.IsTag ? 4 : 2];
			this._handResult = new int[2];
			this._timelimit = new int[2];
			this.Winner = -1;
			this.MatchResults = new int[3];
			this.MatchReasons = new int[3];
			this.Observers = new List<Player>();
			int lfList = Config.GetInt("Banlist", 0);
			if (lfList >= 0 && lfList < BanlistManager.Banlists.Count)
			{
				this.Banlist = BanlistManager.Banlists[lfList];
			}
			this.StartLp = Config.GetInt("StartLp", 8000);
			this.LifePoints[0] = this.StartLp;
			this.LifePoints[1] = this.StartLp;
			this.StartHand = Config.GetInt("StartHand", 5);
			this.DrawCount = Config.GetInt("DrawCount", 1);
			this.EnablePriority = Config.GetBool("EnablePriority", false);
			this.NoCheckDeck = Config.GetBool("NoCheckDeck", false);
			this.NoShuffleDeck = Config.GetBool("NoShuffleDeck", false);
			this.Timer = Config.GetInt("GameTimer", 240);
			this._server = server;
			this._analyser = new GameAnalyser(this);
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00021368 File Offset: 0x0001F568
		public void SetRules(BinaryReader packet)
		{
			uint lfList = packet.ReadUInt32();
			if (lfList >= 0U && (ulong)lfList < (ulong)((long)BanlistManager.Banlists.Count))
			{
				this.Banlist = BanlistManager.Banlists[BanlistManager.GetIndex(lfList)];
			}
			this.Region = (int)packet.ReadByte();
			this.MasterRule = (int)packet.ReadByte();
			this.Mode = (int)packet.ReadByte();
			this.IsMatch = this.Mode == 1;
			this.IsTag = this.Mode == 2;
			this.IsReady = new bool[this.IsTag ? 4 : 2];
			this.Players = new Player[this.IsTag ? 4 : 2];
			this.EnablePriority = packet.ReadByte() > 0;
			this.NoCheckDeck = packet.ReadByte() > 0;
			this.NoShuffleDeck = packet.ReadByte() > 0;
			for (int i = 0; i < 3; i++)
			{
				packet.ReadByte();
			}
			int lifePoints = packet.ReadInt32();
			this.LifePoints[0] = lifePoints;
			this.LifePoints[1] = lifePoints;
			this.StartHand = (int)packet.ReadByte();
			this.DrawCount = (int)packet.ReadByte();
			this.Timer = (int)packet.ReadInt16();
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00021492 File Offset: 0x0001F692
		public void Start()
		{
			if (this.OnNetworkReady != null)
			{
				this.OnNetworkReady(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x000214AD File Offset: 0x0001F6AD
		public void Stop()
		{
			if (this.OnNetworkEnd != null)
			{
				this.OnNetworkEnd(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x000214C8 File Offset: 0x0001F6C8
		public void SendToAll(BinaryWriter packet)
		{
			this.SendToPlayers(packet);
			this.SendToObservers(packet);
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x000214D8 File Offset: 0x0001F6D8
		public void SendToAllBut(BinaryWriter packet, Player except)
		{
			foreach (Player player in this.Players)
			{
				if (player != null && !player.Equals(except))
				{
					player.Send(packet);
				}
			}
			foreach (Player player2 in this.Observers)
			{
				if (!player2.Equals(except))
				{
					player2.Send(packet);
				}
			}
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00021564 File Offset: 0x0001F764
		public void SendToAllBut(BinaryWriter packet, int except)
		{
			if (except < this.CurPlayers.Length)
			{
				this.SendToAllBut(packet, this.CurPlayers[except]);
				return;
			}
			this.SendToAll(packet);
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00021588 File Offset: 0x0001F788
		public void SendToPlayers(BinaryWriter packet)
		{
			foreach (Player player in this.Players)
			{
				if (player != null)
				{
					player.Send(packet);
				}
			}
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x000215B8 File Offset: 0x0001F7B8
		public void SendToObservers(BinaryWriter packet)
		{
			foreach (Player player in this.Observers)
			{
				player.Send(packet);
			}
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0002160C File Offset: 0x0001F80C
		public void SendToTeam(BinaryWriter packet, int team)
		{
			if (!this.IsTag)
			{
				this.Players[team].Send(packet);
				return;
			}
			if (team == 0)
			{
				this.Players[0].Send(packet);
				this.Players[1].Send(packet);
				return;
			}
			this.Players[2].Send(packet);
			this.Players[3].Send(packet);
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0002166C File Offset: 0x0001F86C
		public void AddPlayer(Player player)
		{
			if (this.State != GameState.Lobby)
			{
				player.Type = 7;
				if (this.State != GameState.End)
				{
					this.SendJoinGame(player);
					player.SendTypeChange();
					player.Send(GamePacketFactory.Create(StocMessage.DuelStart));
					this.Observers.Add(player);
					if (this.State == GameState.Duel)
					{
						this.InitNewSpectator(player);
					}
				}
				if (this.OnPlayerJoin != null)
				{
					this.OnPlayerJoin(this, new PlayerEventArgs(player));
				}
				return;
			}
			if (this.HostPlayer == null)
			{
				this.HostPlayer = player;
			}
			int pos = this.GetAvailablePlayerPos();
			if (pos != -1)
			{
				BinaryWriter enter = GamePacketFactory.Create(StocMessage.HsPlayerEnter);
				enter.WriteUnicode(player.Name, 20);
				enter.Write((byte)pos);
				enter.Write(0);
				this.SendToAll(enter);
				this.Players[pos] = player;
				this.IsReady[pos] = false;
				player.Type = pos;
			}
			else
			{
				BinaryWriter watch = GamePacketFactory.Create(StocMessage.HsWatchChange);
				watch.Write((short)(this.Observers.Count + 1));
				this.SendToAll(watch);
				player.Type = 7;
				this.Observers.Add(player);
			}
			this.SendJoinGame(player);
			player.SendTypeChange();
			for (int i = 0; i < this.Players.Length; i++)
			{
				if (this.Players[i] != null)
				{
					BinaryWriter enter2 = GamePacketFactory.Create(StocMessage.HsPlayerEnter);
					enter2.WriteUnicode(this.Players[i].Name, 20);
					enter2.Write((byte)i);
					enter2.Write(0);
					player.Send(enter2);
					if (this.IsReady[i])
					{
						BinaryWriter change = GamePacketFactory.Create(StocMessage.HsPlayerChange);
						change.Write((byte)((i << 4) + 9));
						player.Send(change);
					}
				}
			}
			if (this.Observers.Count > 0)
			{
				BinaryWriter nwatch = GamePacketFactory.Create(StocMessage.HsWatchChange);
				nwatch.Write((short)this.Observers.Count);
				player.Send(nwatch);
			}
			if (this.OnPlayerJoin != null)
			{
				this.OnPlayerJoin(this, new PlayerEventArgs(player));
			}
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00021850 File Offset: 0x0001FA50
		public void RemovePlayer(Player player)
		{
			if (player.Equals(this.HostPlayer) && this.State == GameState.Lobby)
			{
				this._server.Stop();
				return;
			}
			if (player.Type == 7)
			{
				this.Observers.Remove(player);
				if (this.State == GameState.Lobby)
				{
					BinaryWriter nwatch = GamePacketFactory.Create(StocMessage.HsWatchChange);
					nwatch.Write((short)this.Observers.Count);
					this.SendToAll(nwatch);
				}
				player.Disconnect();
			}
			else if (this.State == GameState.Lobby)
			{
				this.Players[player.Type] = null;
				this.IsReady[player.Type] = false;
				BinaryWriter change = GamePacketFactory.Create(StocMessage.HsPlayerChange);
				change.Write((byte)((player.Type << 4) + 11));
				this.SendToAll(change);
				player.Disconnect();
			}
			else
			{
				this.Surrender(player, 4, true);
			}
			if (this.OnPlayerLeave != null)
			{
				this.OnPlayerLeave(this, new PlayerEventArgs(player));
			}
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00021938 File Offset: 0x0001FB38
		public void MoveToDuelist(Player player)
		{
			if (this.State != GameState.Lobby)
			{
				return;
			}
			int pos = this.GetAvailablePlayerPos();
			if (pos == -1)
			{
				return;
			}
			int oldType = player.Type;
			if (player.Type != 7)
			{
				if (!this.IsTag || this.IsReady[player.Type])
				{
					return;
				}
				pos = (player.Type + 1) % 4;
				while (this.Players[pos] != null)
				{
					pos = (pos + 1) % 4;
				}
				BinaryWriter change = GamePacketFactory.Create(StocMessage.HsPlayerChange);
				change.Write((byte)((player.Type << 4) + pos));
				this.SendToAll(change);
				this.Players[player.Type] = null;
				this.Players[pos] = player;
				player.Type = pos;
				player.SendTypeChange();
			}
			else
			{
				this.Observers.Remove(player);
				this.Players[pos] = player;
				player.Type = pos;
				BinaryWriter enter = GamePacketFactory.Create(StocMessage.HsPlayerEnter);
				enter.WriteUnicode(player.Name, 20);
				enter.Write((byte)pos);
				enter.Write(0);
				this.SendToAll(enter);
				BinaryWriter nwatch = GamePacketFactory.Create(StocMessage.HsWatchChange);
				nwatch.Write((short)this.Observers.Count);
				this.SendToAll(nwatch);
				player.SendTypeChange();
			}
			if (this.OnPlayerMove != null)
			{
				this.OnPlayerMove(this, new PlayerMoveEventArgs(player, oldType));
			}
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x00021A74 File Offset: 0x0001FC74
		public void MoveToObserver(Player player)
		{
			if (this.State != GameState.Lobby)
			{
				return;
			}
			if (player.Type == 7)
			{
				return;
			}
			if (this.IsReady[player.Type])
			{
				return;
			}
			int oldType = player.Type;
			this.Players[player.Type] = null;
			this.IsReady[player.Type] = false;
			this.Observers.Add(player);
			BinaryWriter change = GamePacketFactory.Create(StocMessage.HsPlayerChange);
			change.Write((byte)((player.Type << 4) + 8));
			this.SendToAll(change);
			player.Type = 7;
			player.SendTypeChange();
			if (this.OnPlayerMove != null)
			{
				this.OnPlayerMove(this, new PlayerMoveEventArgs(player, oldType));
			}
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x00021B1C File Offset: 0x0001FD1C
		public void Chat(Player player, string msg)
		{
			BinaryWriter packet = GamePacketFactory.Create(StocMessage.Chat);
			packet.Write((short)player.Type);
			if (player.Type == 7)
			{
				string fullmsg = "[" + player.Name + "]: " + msg;
				this.CustomMessage(player, fullmsg);
			}
			else
			{
				packet.WriteUnicode(msg, msg.Length + 1);
				this.SendToAllBut(packet, player);
			}
			if (this.OnPlayerChat != null)
			{
				this.OnPlayerChat(this, new PlayerChatEventArgs(player, msg));
			}
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00021B9C File Offset: 0x0001FD9C
		public void CustomMessage(Player player, string msg)
		{
			BinaryWriter packet = GamePacketFactory.Create(StocMessage.Chat);
			packet.Write(16);
			packet.WriteUnicode(msg, msg.Length + 1);
			this.SendToAllBut(packet, player);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00021BD4 File Offset: 0x0001FDD4
		public void SetReady(Player player, bool ready)
		{
			if (this.State != GameState.Lobby)
			{
				return;
			}
			if (player.Type == 7)
			{
				return;
			}
			if (this.IsReady[player.Type] == ready)
			{
				return;
			}
			if (ready)
			{
				bool ocg = this.Region == 0 || this.Region == 2;
				bool tcg = this.Region == 1 || this.Region == 2;
				int result = 1;
				if (player.Deck != null)
				{
					result = (this.NoCheckDeck ? 0 : player.Deck.Check(this.Banlist, ocg, tcg));
				}
				if (result != 0)
				{
					BinaryWriter rechange = GamePacketFactory.Create(StocMessage.HsPlayerChange);
					rechange.Write((byte)((player.Type << 4) + 10));
					player.Send(rechange);
					BinaryWriter error = GamePacketFactory.Create(StocMessage.ErrorMsg);
					error.Write(2);
					for (int i = 0; i < 3; i++)
					{
						error.Write(0);
					}
					error.Write(result);
					player.Send(error);
					return;
				}
			}
			this.IsReady[player.Type] = ready;
			BinaryWriter change = GamePacketFactory.Create(StocMessage.HsPlayerChange);
			change.Write((byte)((player.Type << 4) + (ready ? 9 : 10)));
			this.SendToAll(change);
			if (this.OnPlayerReady != null)
			{
				this.OnPlayerReady(this, new PlayerEventArgs(player));
			}
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x00021D10 File Offset: 0x0001FF10
		public void KickPlayer(Player player, int pos)
		{
			if (this.State != GameState.Lobby)
			{
				return;
			}
			if (pos >= this.Players.Length || !player.Equals(this.HostPlayer) || player.Equals(this.Players[pos]) || this.Players[pos] == null)
			{
				return;
			}
			this.RemovePlayer(this.Players[pos]);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00021D68 File Offset: 0x0001FF68
		public void StartDuel(Player player)
		{
			if (this.State != GameState.Lobby)
			{
				return;
			}
			if (!player.Equals(this.HostPlayer))
			{
				return;
			}
			for (int i = 0; i < this.Players.Length; i++)
			{
				if (!this.IsReady[i])
				{
					return;
				}
				if (this.Players[i] == null)
				{
					return;
				}
			}
			this.State = GameState.Hand;
			this.SendToAll(GamePacketFactory.Create(StocMessage.DuelStart));
			this.SendHand();
			if (this.OnGameStart != null)
			{
				this.OnGameStart(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x00021DEC File Offset: 0x0001FFEC
		public void HandResult(Player player, int result)
		{
			if (this.State != GameState.Hand)
			{
				return;
			}
			if (player.Type == 7)
			{
				return;
			}
			if (result < 1 || result > 3)
			{
				return;
			}
			if (this.IsTag && player.Type != 0 && player.Type != 2)
			{
				return;
			}
			int type = player.Type;
			if (this.IsTag && player.Type == 2)
			{
				type = 1;
			}
			if (this._handResult[type] != 0)
			{
				return;
			}
			this._handResult[type] = result;
			if (this._handResult[0] != 0 && this._handResult[1] != 0)
			{
				BinaryWriter packet = GamePacketFactory.Create(StocMessage.HandResult);
				packet.Write((byte)this._handResult[0]);
				packet.Write((byte)this._handResult[1]);
				this.SendToTeam(packet, 0);
				this.SendToObservers(packet);
				packet = GamePacketFactory.Create(StocMessage.HandResult);
				packet.Write((byte)this._handResult[1]);
				packet.Write((byte)this._handResult[0]);
				this.SendToTeam(packet, 1);
				if (this._handResult[0] == this._handResult[1])
				{
					this._handResult[0] = 0;
					this._handResult[1] = 0;
					this.SendHand();
					return;
				}
				if ((this._handResult[0] == 1 && this._handResult[1] == 2) || (this._handResult[0] == 2 && this._handResult[1] == 3) || (this._handResult[0] == 3 && this._handResult[1] == 1))
				{
					this._startplayer = (this.IsTag ? 2 : 1);
				}
				else
				{
					this._startplayer = 0;
				}
				this.State = GameState.Starting;
				this.Players[this._startplayer].Send(GamePacketFactory.Create(StocMessage.SelectTp));
				this.TpTimer = DateTime.UtcNow;
			}
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x00021F8C File Offset: 0x0002018C
		public void TpResult(Player player, bool result)
		{
			if (this.State != GameState.Starting)
			{
				return;
			}
			if (player.Type != this._startplayer)
			{
				return;
			}
			int opt = this.MasterRule << 16;
			if (this.EnablePriority)
			{
				opt += 8;
			}
			if (this.NoShuffleDeck)
			{
				opt += 16;
			}
			if (this.IsTag)
			{
				opt += 32;
			}
			if ((result && player.Type == (this.IsTag ? 2 : 1)) || (!result && player.Type == 0))
			{
				opt += 128;
			}
			this.CurPlayers[0] = this.Players[0];
			this.CurPlayers[1] = this.Players[this.IsTag ? 2 : 1];
			this.State = GameState.Duel;
			int seed = Environment.TickCount;
			this._duel = Duel.Create((uint)seed);
			Random rand = new Random(seed);
			this._duel.SetAnalyzer(new Func<GameMessage, BinaryReader, byte[], int>(this._analyser.Analyse));
			this._duel.SetErrorHandler(new Action<string>(this.HandleError));
			this._duel.InitPlayers(this.StartLp, this.StartHand, this.DrawCount);
			this.Replay = new Replay((uint)seed, this.IsTag);
			this.Replay.Writer.WriteUnicode(this.Players[0].Name, 20);
			this.Replay.Writer.WriteUnicode(this.Players[1].Name, 20);
			if (this.IsTag)
			{
				this.Replay.Writer.WriteUnicode(this.Players[2].Name, 20);
				this.Replay.Writer.WriteUnicode(this.Players[3].Name, 20);
			}
			this.Replay.Writer.Write(this.StartLp);
			this.Replay.Writer.Write(this.StartHand);
			this.Replay.Writer.Write(this.DrawCount);
			this.Replay.Writer.Write(opt);
			int i = 0;
			while (i < this.Players.Length)
			{
				Player dplayer = this.Players[i];
				int pid = i;
				if (this.IsTag)
				{
					pid = ((i >= 2) ? 1 : 0);
				}
				if (!this.NoShuffleDeck)
				{
					List<int> cards = Game.ShuffleCards(rand, dplayer.Deck.Main);
					this.Replay.Writer.Write(cards.Count);
					using (List<int>.Enumerator enumerator = cards.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							int id = enumerator.Current;
							if (this.IsTag && (i == 1 || i == 3))
							{
								this._duel.AddTagCard(id, pid, CardLocation.Deck);
							}
							else
							{
								this._duel.AddCard(id, pid, CardLocation.Deck);
							}
							this.Replay.Writer.Write(id);
						}
						goto IL_036B;
					}
					goto IL_02CD;
				}
				goto IL_02CD;
				IL_036B:
				this.Replay.Writer.Write(dplayer.Deck.Extra.Count);
				foreach (int id2 in dplayer.Deck.Extra)
				{
					if (this.IsTag && (i == 1 || i == 3))
					{
						this._duel.AddTagCard(id2, pid, CardLocation.Extra);
					}
					else
					{
						this._duel.AddCard(id2, pid, CardLocation.Extra);
					}
					this.Replay.Writer.Write(id2);
				}
				i++;
				continue;
				IL_02CD:
				this.Replay.Writer.Write(dplayer.Deck.Main.Count);
				for (int j = dplayer.Deck.Main.Count - 1; j >= 0; j--)
				{
					int id3 = dplayer.Deck.Main[j];
					if (this.IsTag && (i == 1 || i == 3))
					{
						this._duel.AddTagCard(id3, pid, CardLocation.Deck);
					}
					else
					{
						this._duel.AddCard(id3, pid, CardLocation.Deck);
					}
					this.Replay.Writer.Write(id3);
				}
				goto IL_036B;
			}
			BinaryWriter packet = GamePacketFactory.Create(GameMessage.Start);
			packet.Write(0);
			packet.Write((byte)this.MasterRule);
			packet.Write(this.StartLp);
			packet.Write(this.StartLp);
			packet.Write((short)this._duel.QueryFieldCount(0, CardLocation.Deck));
			packet.Write((short)this._duel.QueryFieldCount(0, CardLocation.Extra));
			packet.Write((short)this._duel.QueryFieldCount(1, CardLocation.Deck));
			packet.Write((short)this._duel.QueryFieldCount(1, CardLocation.Extra));
			this.SendToTeam(packet, 0);
			packet.BaseStream.Position = 2L;
			packet.Write(1);
			this.SendToTeam(packet, 1);
			packet.BaseStream.Position = 2L;
			packet.Write(16);
			this.SendToObservers(packet);
			this.RefreshExtra(0, null);
			this.RefreshExtra(1, null);
			this._duel.Start(opt);
			this.TurnCount = 0;
			this.LifePoints[0] = this.StartLp;
			this.LifePoints[1] = this.StartLp;
			this.TimeReset();
			this.Process();
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x000224E4 File Offset: 0x000206E4
		public void Surrender(Player player, int reason, bool force = false)
		{
			if (this.State == GameState.End)
			{
				return;
			}
			if (!force && this.State != GameState.Duel)
			{
				return;
			}
			if (player.Type == 7)
			{
				return;
			}
			BinaryWriter win = GamePacketFactory.Create(GameMessage.Win);
			int team = player.Type;
			if (this.IsTag)
			{
				team = ((player.Type >= 2) ? 1 : 0);
			}
			else if (this.State == GameState.Hand)
			{
				team = 1 - team;
			}
			win.Write((byte)(1 - team));
			win.Write((byte)reason);
			this.SendToAll(win);
			this.MatchSaveResult(1 - team, reason);
			this.EndDuel(reason == 4);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x00022572 File Offset: 0x00020772
		public void RefreshAll()
		{
			this.RefreshMonsters(0, null);
			this.RefreshMonsters(1, null);
			this.RefreshSpells(0, null);
			this.RefreshSpells(1, null);
			this.RefreshHand(0, null);
			this.RefreshHand(1, null);
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x000225A4 File Offset: 0x000207A4
		public void RefreshAllObserver(Player observer)
		{
			this.RefreshMonsters(0, observer);
			this.RefreshMonsters(1, observer);
			this.RefreshSpells(0, observer);
			this.RefreshSpells(1, observer);
			this.RefreshHand(0, observer);
			this.RefreshHand(1, observer);
			this.RefreshGrave(0, observer);
			this.RefreshGrave(1, observer);
			this.RefreshExtra(0, observer);
			this.RefreshExtra(1, observer);
			this.RefreshRemoved(0, observer);
			this.RefreshRemoved(1, observer);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00022614 File Offset: 0x00020814
		public void RefreshMonsters(int player, Player observer = null)
		{
			byte[] result = this._duel.QueryFieldCard(player, CardLocation.MonsterZone, 16769023, false);
			this.SendToCorrectDestination(player, CardLocation.MonsterZone, result, observer);
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00022640 File Offset: 0x00020840
		public void RefreshSpells(int player, Player observer = null)
		{
			byte[] result = this._duel.QueryFieldCard(player, CardLocation.SpellZone, 16769023, false);
			this.SendToCorrectDestination(player, CardLocation.SpellZone, result, observer);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0002266C File Offset: 0x0002086C
		public void RefreshHand(int player, Player observer = null)
		{
			byte[] result = this._duel.QueryFieldCard(player, CardLocation.Hand, 16769023, false);
			this.SendToCorrectDestination(player, CardLocation.Hand, result, observer);
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00022698 File Offset: 0x00020898
		public void RefreshGrave(int player, Player observer = null)
		{
			byte[] result = this._duel.QueryFieldCard(player, CardLocation.Grave, 16769023, false);
			this.SendToCorrectDestination(player, CardLocation.Grave, result, observer);
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x000226C8 File Offset: 0x000208C8
		public void RefreshRemoved(int player, Player observer = null)
		{
			byte[] result = this._duel.QueryFieldCard(player, CardLocation.Removed, 16769023, false);
			this.SendToCorrectDestination(player, CardLocation.Removed, result, observer);
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x000226F8 File Offset: 0x000208F8
		public void RefreshExtra(int player, Player observer = null)
		{
			byte[] result = this._duel.QueryFieldCard(player, CardLocation.Extra, 16769023, false);
			this.SendToCorrectDestination(player, CardLocation.Extra, result, observer);
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00022728 File Offset: 0x00020928
		private void SendToCorrectDestination(int player, CardLocation location, byte[] result, Player observer)
		{
			BinaryWriter update;
			if (observer == null)
			{
				update = GamePacketFactory.Create(GameMessage.UpdateData);
				update.Write((byte)player);
				update.Write((byte)location);
				update.Write(result);
				this.SendToTeam(update, player);
			}
			update = GamePacketFactory.Create(GameMessage.UpdateData);
			update.Write((byte)player);
			update.Write((byte)location);
			this.WritePublicCards(update, result);
			if (observer == null)
			{
				this.SendToTeam(update, 1 - player);
				this.SendToObservers(update);
				return;
			}
			observer.Send(update);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0002279C File Offset: 0x0002099C
		private void WritePublicCards(BinaryWriter update, byte[] result)
		{
			MemoryStream ms = new MemoryStream(result);
			BinaryReader reader = new BinaryReader(ms);
			while (ms.Position < ms.Length)
			{
				int len = reader.ReadInt32();
				if (len == 4)
				{
					update.Write(4);
				}
				else
				{
					byte[] raw = reader.ReadBytes(len - 4);
					if ((raw[11] & 5) > 0)
					{
						update.Write(len);
						update.Write(raw);
					}
					else
					{
						update.Write(8);
						update.Write(0);
					}
				}
			}
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00022810 File Offset: 0x00020A10
		public void RefreshSingle(int player, int location, int sequence)
		{
			byte[] result = this._duel.QueryCard(player, location, sequence, 16769023, false);
			if (location == 32 && (result[15] & 10) != 0)
			{
				return;
			}
			BinaryWriter update = GamePacketFactory.Create(GameMessage.UpdateCard);
			update.Write((byte)player);
			update.Write((byte)location);
			update.Write((byte)sequence);
			update.Write(result);
			this.CurPlayers[player].Send(update);
			if (this.IsTag)
			{
				if ((location & 12) != 0)
				{
					this.SendToTeam(update, player);
					if ((result[15] & 5) != 0)
					{
						this.SendToTeam(update, 1 - player);
						return;
					}
				}
				else
				{
					this.CurPlayers[player].Send(update);
					if ((location & 144) != 0)
					{
						this.SendToAllBut(update, player);
						return;
					}
				}
			}
			else if ((location & 144) != 0 || ((location & 44) != 0 && (result[15] & 5) != 0))
			{
				this.SendToAllBut(update, player);
			}
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x000228DD File Offset: 0x00020ADD
		public int WaitForResponse()
		{
			this.WaitForResponse(this._lastresponse);
			return this._lastresponse;
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x000228F4 File Offset: 0x00020AF4
		public void WaitForResponse(int player)
		{
			this._lastresponse = player;
			this.CurPlayers[player].State = PlayerState.Response;
			this.SendToAllBut(GamePacketFactory.Create(GameMessage.Waiting), player);
			this.TimeStart();
			BinaryWriter packet = GamePacketFactory.Create(StocMessage.TimeLimit);
			packet.Write((byte)player);
			packet.Write(0);
			packet.Write((short)this._timelimit[player]);
			this.SendToPlayers(packet);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00022958 File Offset: 0x00020B58
		public void SetResponse(int resp)
		{
			if (!this.Replay.Disabled)
			{
				this.Replay.Writer.Write(4);
				this.Replay.Writer.Write(BitConverter.GetBytes(resp));
				this.Replay.Check();
			}
			this.TimeStop();
			this._duel.SetResponse(resp);
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x000229B8 File Offset: 0x00020BB8
		public void SetResponse(byte[] resp)
		{
			if (!this.Replay.Disabled)
			{
				this.Replay.Writer.Write((byte)resp.Length);
				this.Replay.Writer.Write(resp);
				this.Replay.Check();
			}
			this.TimeStop();
			this._duel.SetResponse(resp);
			this.Process();
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00022A1C File Offset: 0x00020C1C
		public void EndDuel(bool force)
		{
			if (this.State == GameState.End)
			{
				return;
			}
			if (this.State == GameState.Duel)
			{
				if (!this.Replay.Disabled)
				{
					this.Replay.End();
					byte[] replayData = this.Replay.GetContent();
					BinaryWriter packet = GamePacketFactory.Create(StocMessage.Replay);
					packet.Write(replayData);
					this.SendToAll(packet);
				}
				this._duel.End();
			}
			if (this.IsMatch && !force && !this.MatchIsEnd())
			{
				this.IsReady[0] = false;
				this.IsReady[1] = false;
				this.State = GameState.Side;
				this.SideTimer = DateTime.UtcNow;
				this.SendToPlayers(GamePacketFactory.Create(StocMessage.ChangeSide));
				this.SendToObservers(GamePacketFactory.Create(StocMessage.WaitingSide));
				return;
			}
			this.CalculateWinner();
			this.End();
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00022ADD File Offset: 0x00020CDD
		public void End()
		{
			this.State = GameState.End;
			this.SendToAll(GamePacketFactory.Create(StocMessage.DuelEnd));
			this._server.StopDelayed();
			if (this.OnGameEnd != null)
			{
				this.OnGameEnd(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00022B17 File Offset: 0x00020D17
		public void TimeReset()
		{
			this._timelimit[0] = this.Timer;
			this._timelimit[1] = this.Timer;
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00022B35 File Offset: 0x00020D35
		public void TimeStart()
		{
			this._time = new DateTime?(DateTime.UtcNow);
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00022B48 File Offset: 0x00020D48
		public void TimeStop()
		{
			if (this._time != null)
			{
				TimeSpan elapsed = DateTime.UtcNow - this._time.Value;
				this._timelimit[this._lastresponse] -= (int)elapsed.TotalSeconds;
				if (this._timelimit[this._lastresponse] < 0)
				{
					this._timelimit[this._lastresponse] = 0;
				}
				this._time = null;
			}
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00022BC0 File Offset: 0x00020DC0
		public void TimeTick()
		{
			if (this.State == GameState.Duel && this._time != null && (int)(DateTime.UtcNow - this._time.Value).TotalSeconds > this._timelimit[this._lastresponse])
			{
				this.Surrender(this.CurPlayers[this._lastresponse], 3, false);
			}
			if (this.State == GameState.Side && (DateTime.UtcNow - this.SideTimer).TotalMilliseconds >= 120000.0)
			{
				if (!this.IsReady[0] && !this.IsReady[1])
				{
					this.EndDuel(true);
					return;
				}
				this.Surrender((!this.IsReady[0]) ? this.Players[0] : this.Players[1], 3, true);
			}
			if (this.State == GameState.Starting && this.IsTpSelect && (DateTime.UtcNow - this.TpTimer).TotalMilliseconds >= 30000.0)
			{
				this.Surrender(this.CurPlayers[this._startplayer], 3, true);
			}
			if (this.State == GameState.Hand && (int)(DateTime.UtcNow - this.RpsTimer).TotalMilliseconds >= 60000)
			{
				if (this._handResult[0] != 0)
				{
					this.Surrender(this.Players[this.IsTag ? 2 : 1], 3, true);
					return;
				}
				if (this._handResult[1] != 0)
				{
					this.Surrender(this.Players[0], 3, true);
					return;
				}
				this.EndDuel(true);
			}
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00022D4C File Offset: 0x00020F4C
		public void MatchSaveResult(int player, int reason)
		{
			if (player < 2)
			{
				this._startplayer = 1 - player;
			}
			else
			{
				this._startplayer = 1 - this._startplayer;
			}
			this.MatchResults[this.DuelCount] = player;
			int[] matchReasons = this.MatchReasons;
			int duelCount = this.DuelCount;
			this.DuelCount = duelCount + 1;
			matchReasons[duelCount] = reason;
			if (this.OnDuelEnd != null)
			{
				this.OnDuelEnd(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00022DB6 File Offset: 0x00020FB6
		public void MatchKill()
		{
			this._matchKill = true;
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00022DC0 File Offset: 0x00020FC0
		public bool MatchIsEnd()
		{
			if (this._matchKill)
			{
				return true;
			}
			int[] wins = new int[3];
			for (int i = 0; i < this.DuelCount; i++)
			{
				wins[this.MatchResults[i]]++;
			}
			return wins[0] == 2 || wins[1] == 2 || wins[0] + wins[1] + wins[2] == 3;
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00022E20 File Offset: 0x00021020
		public void MatchSide()
		{
			if (this.IsReady[0] && this.IsReady[1])
			{
				this.State = GameState.Starting;
				this.IsTpSelect = true;
				this.TpTimer = DateTime.UtcNow;
				this.TimeReset();
				this.Players[this._startplayer].Send(GamePacketFactory.Create(StocMessage.SelectTp));
			}
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00022E78 File Offset: 0x00021078
		private int GetAvailablePlayerPos()
		{
			for (int i = 0; i < this.Players.Length; i++)
			{
				if (this.Players[i] == null)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00022EA8 File Offset: 0x000210A8
		private void SendHand()
		{
			this.RpsTimer = DateTime.UtcNow;
			BinaryWriter hand = GamePacketFactory.Create(StocMessage.SelectHand);
			if (this.IsTag)
			{
				this.Players[0].Send(hand);
				this.Players[2].Send(hand);
				return;
			}
			this.SendToPlayers(hand);
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00022EF4 File Offset: 0x000210F4
		private void Process()
		{
			int result = this._duel.Process();
			if (result == -1)
			{
				this.EndDuel(true);
				return;
			}
			if (result != 2)
			{
				return;
			}
			this.EndDuel(false);
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x00022F28 File Offset: 0x00021128
		private void SendJoinGame(Player player)
		{
			BinaryWriter join = GamePacketFactory.Create(StocMessage.JoinGame);
			join.Write((this.Banlist == null) ? 0U : this.Banlist.Hash);
			join.Write((byte)this.Region);
			join.Write((byte)this.Mode);
			join.Write((byte)this.MasterRule);
			join.Write(this.NoCheckDeck);
			join.Write(this.NoShuffleDeck);
			for (int i = 0; i < 3; i++)
			{
				join.Write(0);
			}
			join.Write(this.StartLp);
			join.Write((byte)this.StartHand);
			join.Write((byte)this.DrawCount);
			join.Write((short)this.Timer);
			player.Send(join);
			if (this.State != GameState.Lobby)
			{
				this.SendDuelingPlayers(player);
			}
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00022FF4 File Offset: 0x000211F4
		private void SendDuelingPlayers(Player player)
		{
			for (int i = 0; i < this.Players.Length; i++)
			{
				BinaryWriter enter = GamePacketFactory.Create(StocMessage.HsPlayerEnter);
				enter.WriteUnicode(this.Players[i].Name, 20);
				enter.Write((byte)i);
				enter.Write(0);
				player.Send(enter);
			}
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x00023048 File Offset: 0x00021248
		private void InitNewSpectator(Player player)
		{
			BinaryWriter packet = GamePacketFactory.Create(GameMessage.Start);
			packet.Write(16);
			packet.Write((byte)this.MasterRule);
			packet.Write(this.LifePoints[0]);
			packet.Write(this.LifePoints[1]);
			packet.Write(0);
			packet.Write(0);
			packet.Write(0);
			packet.Write(0);
			player.Send(packet);
			BinaryWriter turn = GamePacketFactory.Create(GameMessage.NewTurn);
			turn.Write(0);
			player.Send(turn);
			if (this.CurrentPlayer == 1)
			{
				turn = GamePacketFactory.Create(GameMessage.NewTurn);
				turn.Write(0);
				player.Send(turn);
			}
			BinaryWriter reload = GamePacketFactory.Create(GameMessage.ReloadField);
			byte[] fieldInfo = this._duel.QueryFieldInfo();
			reload.Write(fieldInfo, 1, fieldInfo.Length - 1);
			player.Send(reload);
			this.RefreshAllObserver(player);
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x00023118 File Offset: 0x00021318
		private void HandleError(string error)
		{
			BinaryWriter packet = GamePacketFactory.Create(StocMessage.Chat);
			packet.Write(7);
			packet.WriteUnicode(error, error.Length + 1);
			this.SendToAll(packet);
			File.WriteAllText("lua_" + DateTime.UtcNow.ToString("yyyy-MM-dd_HH-mm-ss") + ".txt", error);
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00023174 File Offset: 0x00021374
		private static List<int> ShuffleCards(Random rand, IEnumerable<int> cards)
		{
			List<int> shuffled = new List<int>(cards);
			for (int i = shuffled.Count - 1; i > 0; i--)
			{
				int pos = rand.Next(i + 1);
				int tmp = shuffled[i];
				shuffled[i] = shuffled[pos];
				shuffled[pos] = tmp;
			}
			return shuffled;
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x000231C4 File Offset: 0x000213C4
		private void CalculateWinner()
		{
			int winner = -1;
			if (this.DuelCount > 0)
			{
				if (!this._matchKill && this.DuelCount != 1)
				{
					int[] wins = new int[3];
					for (int i = 0; i < this.DuelCount; i++)
					{
						wins[this.MatchResults[i]]++;
					}
					if (wins[0] > wins[1])
					{
						winner = 0;
					}
					else if (wins[1] > wins[0])
					{
						winner = 1;
					}
					else
					{
						winner = 2;
					}
				}
				else
				{
					winner = this.MatchResults[this.DuelCount - 1];
				}
			}
			this.Winner = winner;
		}

		// Token: 0x04000B50 RID: 2896
		public const int DEFAULT_LIFEPOINTS = 8000;

		// Token: 0x04000B51 RID: 2897
		public const int DEFAULT_START_HAND = 5;

		// Token: 0x04000B52 RID: 2898
		public const int DEFAULT_DRAW_COUNT = 1;

		// Token: 0x04000B53 RID: 2899
		public const int DEFAULT_TIMER = 240;

		// Token: 0x04000B72 RID: 2930
		public int DuelCount;

		// Token: 0x04000B73 RID: 2931
		private CoreServer _server;

		// Token: 0x04000B74 RID: 2932
		private Duel _duel;

		// Token: 0x04000B75 RID: 2933
		private GameAnalyser _analyser;

		// Token: 0x04000B76 RID: 2934
		private int[] _handResult;

		// Token: 0x04000B77 RID: 2935
		private int _startplayer;

		// Token: 0x04000B78 RID: 2936
		private int _lastresponse;

		// Token: 0x04000B79 RID: 2937
		private int[] _timelimit;

		// Token: 0x04000B7A RID: 2938
		private DateTime? _time;

		// Token: 0x04000B7B RID: 2939
		private bool _matchKill;
	}
}
