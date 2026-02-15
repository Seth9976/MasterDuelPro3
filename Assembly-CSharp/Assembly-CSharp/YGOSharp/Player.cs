using System;
using System.IO;
using YGOSharp.Network;
using YGOSharp.Network.Enums;
using YGOSharp.Network.Utils;

namespace YGOSharp
{
	// Token: 0x020001BB RID: 443
	public class Player
	{
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x00024BB1 File Offset: 0x00022DB1
		// (set) Token: 0x06000785 RID: 1925 RVA: 0x00024BB9 File Offset: 0x00022DB9
		public Game Game { get; private set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x00024BC2 File Offset: 0x00022DC2
		// (set) Token: 0x06000787 RID: 1927 RVA: 0x00024BCA File Offset: 0x00022DCA
		public string Name { get; private set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x00024BD3 File Offset: 0x00022DD3
		// (set) Token: 0x06000789 RID: 1929 RVA: 0x00024BDB File Offset: 0x00022DDB
		public bool IsAuthentified { get; private set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x00024BE4 File Offset: 0x00022DE4
		// (set) Token: 0x0600078B RID: 1931 RVA: 0x00024BEC File Offset: 0x00022DEC
		public int Type { get; set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x00024BF5 File Offset: 0x00022DF5
		// (set) Token: 0x0600078D RID: 1933 RVA: 0x00024BFD File Offset: 0x00022DFD
		public Deck Deck { get; private set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x00024C06 File Offset: 0x00022E06
		// (set) Token: 0x0600078F RID: 1935 RVA: 0x00024C0E File Offset: 0x00022E0E
		public PlayerState State { get; set; }

		// Token: 0x06000790 RID: 1936 RVA: 0x00024C17 File Offset: 0x00022E17
		public Player(Game game, YGOClient client)
		{
			this.Game = game;
			this.Type = -1;
			this.State = PlayerState.None;
			this._client = client;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00024C3B File Offset: 0x00022E3B
		public void Send(BinaryWriter packet)
		{
			this._client.Send(packet);
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x00024C49 File Offset: 0x00022E49
		public void Disconnect()
		{
			this._client.Close(null);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x00024C57 File Offset: 0x00022E57
		public void OnDisconnected()
		{
			if (this.IsAuthentified)
			{
				this.Game.RemovePlayer(this);
			}
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00024C70 File Offset: 0x00022E70
		public void SendTypeChange()
		{
			BinaryWriter packet = GamePacketFactory.Create(StocMessage.TypeChange);
			packet.Write((byte)(this.Type + (this.Game.HostPlayer.Equals(this) ? 16 : 0)));
			this.Send(packet);
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00024CB2 File Offset: 0x00022EB2
		public bool Equals(Player player)
		{
			return this == player;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00024CB8 File Offset: 0x00022EB8
		public void Parse(BinaryReader packet)
		{
			CtosMessage msg = (CtosMessage)packet.ReadByte();
			switch (msg)
			{
			case CtosMessage.PlayerInfo:
				this.OnPlayerInfo(packet);
				break;
			case CtosMessage.CreateGame:
				this.OnCreateGame(packet);
				break;
			case CtosMessage.JoinGame:
				this.OnJoinGame(packet);
				break;
			}
			if (!this.IsAuthentified)
			{
				return;
			}
			switch (msg)
			{
			case CtosMessage.Response:
				this.OnResponse(packet);
				return;
			case CtosMessage.UpdateDeck:
				this.OnUpdateDeck(packet);
				return;
			case CtosMessage.HandResult:
				this.OnHandResult(packet);
				return;
			case CtosMessage.TpResult:
				this.OnTpResult(packet);
				return;
			default:
				switch (msg)
				{
				case CtosMessage.LeaveGame:
					this.Game.RemovePlayer(this);
					return;
				case CtosMessage.Surrender:
					this.Game.Surrender(this, 0, false);
					break;
				case CtosMessage.TimeConfirm:
					break;
				case CtosMessage.Chat:
					this.OnChat(packet);
					return;
				default:
					switch (msg)
					{
					case CtosMessage.HsToDuelist:
						this.Game.MoveToDuelist(this);
						return;
					case CtosMessage.HsToObserver:
						this.Game.MoveToObserver(this);
						return;
					case CtosMessage.HsReady:
						this.Game.SetReady(this, true);
						return;
					case CtosMessage.HsNotReady:
						this.Game.SetReady(this, false);
						return;
					case CtosMessage.HsKick:
						this.OnKick(packet);
						return;
					case CtosMessage.HsStart:
						this.Game.StartDuel(this);
						return;
					default:
						return;
					}
					break;
				}
				return;
			}
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00024DE6 File Offset: 0x00022FE6
		private void OnPlayerInfo(BinaryReader packet)
		{
			if (this.Name != null)
			{
				return;
			}
			this.Name = packet.ReadUnicode(20);
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00024DFF File Offset: 0x00022FFF
		private void OnCreateGame(BinaryReader packet)
		{
			this.Game.SetRules(packet);
			packet.ReadUnicode(20);
			packet.ReadUnicode(30);
			this.Game.AddPlayer(this);
			this.IsAuthentified = true;
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00024E34 File Offset: 0x00023034
		private void OnJoinGame(BinaryReader packet)
		{
			if (this.Name == null || this.Type != -1)
			{
				return;
			}
			if ((long)packet.ReadInt16() != (long)((ulong)Program.ClientVersion))
			{
				return;
			}
			packet.ReadInt32();
			packet.ReadInt16();
			this.Game.AddPlayer(this);
			this.IsAuthentified = true;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00024E84 File Offset: 0x00023084
		private void OnChat(BinaryReader packet)
		{
			string msg = packet.ReadUnicode(256);
			this.Game.Chat(this, msg);
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00024EAC File Offset: 0x000230AC
		private void OnKick(BinaryReader packet)
		{
			int pos = (int)packet.ReadByte();
			this.Game.KickPlayer(this, pos);
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00024ED0 File Offset: 0x000230D0
		private void OnHandResult(BinaryReader packet)
		{
			int res = (int)packet.ReadByte();
			this.Game.HandResult(this, res);
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00024EF4 File Offset: 0x000230F4
		private void OnTpResult(BinaryReader packet)
		{
			bool tp = packet.ReadByte() > 0;
			this.Game.TpResult(this, tp);
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00024F18 File Offset: 0x00023118
		private void OnUpdateDeck(BinaryReader packet)
		{
			if (this.Type == 7)
			{
				return;
			}
			Deck deck = new Deck();
			int main = packet.ReadInt32();
			int side = packet.ReadInt32();
			for (int i = 0; i < main; i++)
			{
				deck.AddMain(packet.ReadInt32());
			}
			for (int j = 0; j < side; j++)
			{
				deck.AddSide(packet.ReadInt32());
			}
			if (this.Game.State == GameState.Lobby)
			{
				this.Deck = deck;
				this.Game.IsReady[this.Type] = false;
				return;
			}
			if (this.Game.State == GameState.Side)
			{
				if (this.Game.IsReady[this.Type])
				{
					return;
				}
				if (!this.Deck.Check(deck))
				{
					BinaryWriter error = GamePacketFactory.Create(StocMessage.ErrorMsg);
					error.Write(3);
					error.Write(0);
					this.Send(error);
					return;
				}
				this.Deck = deck;
				this.Game.IsReady[this.Type] = true;
				this.Send(GamePacketFactory.Create(StocMessage.DuelStart));
				this.Game.MatchSide();
			}
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00025024 File Offset: 0x00023224
		private void OnResponse(BinaryReader packet)
		{
			if (this.Game.State != GameState.Duel)
			{
				return;
			}
			if (this.State != PlayerState.Response)
			{
				return;
			}
			byte[] resp = packet.ReadToEnd();
			if (resp.Length > 64)
			{
				return;
			}
			this.State = PlayerState.None;
			this.Game.SetResponse(resp);
		}

		// Token: 0x04000B8D RID: 2957
		private YGOClient _client;
	}
}
