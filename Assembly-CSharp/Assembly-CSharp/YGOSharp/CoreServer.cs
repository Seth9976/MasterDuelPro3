using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using YGOSharp.Network;

namespace YGOSharp
{
	// Token: 0x020001B4 RID: 436
	public class CoreServer
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000692 RID: 1682 RVA: 0x00020361 File Offset: 0x0001E561
		// (set) Token: 0x06000693 RID: 1683 RVA: 0x00020369 File Offset: 0x0001E569
		public bool IsRunning { get; private set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x00020372 File Offset: 0x0001E572
		// (set) Token: 0x06000695 RID: 1685 RVA: 0x0002037A File Offset: 0x0001E57A
		public bool IsListening { get; private set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x00020383 File Offset: 0x0001E583
		// (set) Token: 0x06000697 RID: 1687 RVA: 0x0002038B File Offset: 0x0001E58B
		public AddonsManager Addons { get; private set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x00020394 File Offset: 0x0001E594
		// (set) Token: 0x06000699 RID: 1689 RVA: 0x0002039C File Offset: 0x0001E59C
		public Game Game { get; private set; }

		// Token: 0x0600069B RID: 1691 RVA: 0x000203B8 File Offset: 0x0001E5B8
		public void Start()
		{
			if (this.IsRunning)
			{
				return;
			}
			this.Addons = new AddonsManager();
			this.Game = new Game(this);
			this.Addons.Init(this.Game);
			try
			{
				this._listener = new NetworkServer(IPAddress.Any, Config.GetInt("Port", CoreServer.DEFAULT_PORT));
				this._listener.ClientConnected += this.Listener_ClientConnected;
				this._listener.Start();
				this.IsRunning = true;
				this.IsListening = true;
				this.Game.Start();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x00020468 File Offset: 0x0001E668
		public void StopListening()
		{
			if (!this.IsListening)
			{
				return;
			}
			this.IsListening = false;
			this._listener.Close();
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00020488 File Offset: 0x0001E688
		public void Stop()
		{
			this.StopListening();
			foreach (YGOClient ygoclient in this._clients)
			{
				ygoclient.Close(null);
			}
			this.Game.Stop();
			this.IsRunning = false;
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x000204F4 File Offset: 0x0001E6F4
		public void StopDelayed()
		{
			this.StopListening();
			this._closePending = true;
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x00020504 File Offset: 0x0001E704
		public void AddClient(YGOClient client)
		{
			this._clients.Add(client);
			Player player = new Player(this.Game, client);
			client.PacketReceived += delegate(BinaryReader packet)
			{
				player.Parse(packet);
			};
			client.Disconnected += delegate(Exception packet)
			{
				player.OnDisconnected();
			};
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0002055C File Offset: 0x0001E75C
		public void Tick()
		{
			this._listener.Update();
			List<YGOClient> disconnectedClients = new List<YGOClient>();
			foreach (YGOClient client in this._clients)
			{
				client.Update();
				if (!client.IsConnected)
				{
					disconnectedClients.Add(client);
				}
			}
			this.Game.TimeTick();
			while (disconnectedClients.Count > 0)
			{
				this._clients.Remove(disconnectedClients[0]);
				disconnectedClients.RemoveAt(0);
			}
			if (this._closePending && this._clients.Count == 0)
			{
				this.Stop();
			}
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0002061C File Offset: 0x0001E81C
		private void Listener_ClientConnected(NetworkClient client)
		{
			this.AddClient(new YGOClient(client));
		}

		// Token: 0x04000B44 RID: 2884
		public static int DEFAULT_PORT = 7911;

		// Token: 0x04000B49 RID: 2889
		private NetworkServer _listener;

		// Token: 0x04000B4A RID: 2890
		private readonly List<YGOClient> _clients = new List<YGOClient>();

		// Token: 0x04000B4B RID: 2891
		private bool _closePending;
	}
}
