using System;
using YGOSharp.Network.Enums;

namespace YGOSharp.Addons
{
	// Token: 0x020001C2 RID: 450
	public class StandardStreamProtocol : AddonBase
	{
		// Token: 0x060007B4 RID: 1972 RVA: 0x000253C4 File Offset: 0x000235C4
		public StandardStreamProtocol(Game game)
			: base(game)
		{
			if (!Config.GetBool("StandardStreamProtocol", false))
			{
				return;
			}
			base.Game.OnNetworkReady += this.Game_OnNetworkReady;
			base.Game.OnNetworkEnd += this.Game_OnNetworkEnd;
			base.Game.OnPlayerChat += this.Game_OnPlayerChat;
			base.Game.OnPlayerJoin += this.Game_OnPlayerJoin;
			base.Game.OnPlayerLeave += this.Game_OnPlayerLeave;
			base.Game.OnPlayerMove += this.Game_OnPlayerMove;
			base.Game.OnPlayerReady += this.Game_OnPlayerReady;
			base.Game.OnGameStart += this.Game_OnGameStart;
			base.Game.OnGameEnd += this.Game_OnGameEnd;
			base.Game.OnDuelEnd += this.Game_OnDuelEnd;
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x000254CC File Offset: 0x000236CC
		private void Game_OnNetworkReady(object sender, EventArgs e)
		{
			Console.WriteLine("::::network-ready");
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x000254D8 File Offset: 0x000236D8
		private void Game_OnNetworkEnd(object sender, EventArgs e)
		{
			Console.WriteLine("::::network-end");
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x000254E4 File Offset: 0x000236E4
		private void Game_OnPlayerChat(object sender, PlayerChatEventArgs e)
		{
			Console.WriteLine("::::chat|" + e.Player.Name + "|" + e.Message);
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0002550C File Offset: 0x0002370C
		private void Game_OnPlayerJoin(object sender, PlayerEventArgs e)
		{
			if (base.Game.State != GameState.Lobby)
			{
				return;
			}
			if (e.Player.Type != 7)
			{
				Console.WriteLine("::::join-slot|" + e.Player.Type.ToString() + "|" + e.Player.Name);
				return;
			}
			Console.WriteLine("::::spectator|" + base.Game.Observers.Count.ToString());
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00025590 File Offset: 0x00023790
		private void Game_OnPlayerLeave(object sender, PlayerEventArgs e)
		{
			if (base.Game.State != GameState.Lobby)
			{
				return;
			}
			if (e.Player.Type != 7)
			{
				Console.WriteLine("::::left-slot|" + e.Player.Type.ToString() + "|" + e.Player.Name);
				return;
			}
			Console.WriteLine("::::spectator|" + base.Game.Observers.Count.ToString());
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00025614 File Offset: 0x00023814
		private void Game_OnPlayerMove(object sender, PlayerMoveEventArgs e)
		{
			if (base.Game.State != GameState.Lobby)
			{
				return;
			}
			if (e.FromType != 7)
			{
				Console.WriteLine("::::left-slot|" + e.FromType.ToString() + "|" + e.Player.Name);
			}
			if (e.Player.Type != 7)
			{
				Console.WriteLine("::::join-slot|" + e.Player.Type.ToString() + "|" + e.Player.Name);
			}
			if (e.FromType == 7 || e.Player.Type == 7)
			{
				Console.WriteLine("::::spectator|" + base.Game.Observers.Count.ToString());
			}
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x000256E4 File Offset: 0x000238E4
		private void Game_OnPlayerReady(object sender, PlayerEventArgs e)
		{
			Console.WriteLine("::::lock-slot|" + e.Player.Type.ToString() + "|" + base.Game.IsReady[e.Player.Type].ToString());
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00025738 File Offset: 0x00023938
		private void Game_OnGameStart(object sender, EventArgs e)
		{
			Console.WriteLine("::::start-game");
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x00025744 File Offset: 0x00023944
		private void Game_OnGameEnd(object sender, EventArgs e)
		{
			Console.WriteLine("::::end-game|" + base.Game.Winner.ToString());
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00025774 File Offset: 0x00023974
		private void Game_OnDuelEnd(object sender, EventArgs e)
		{
			Console.WriteLine("::::end-duel|" + base.Game.MatchResults[base.Game.DuelCount - 1].ToString() + "|" + base.Game.MatchReasons[base.Game.DuelCount - 1].ToString());
		}
	}
}
