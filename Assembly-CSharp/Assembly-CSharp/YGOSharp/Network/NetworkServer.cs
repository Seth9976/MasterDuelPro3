using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace YGOSharp.Network
{
	// Token: 0x020001DC RID: 476
	public class NetworkServer
	{
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000878 RID: 2168 RVA: 0x000276C4 File Offset: 0x000258C4
		// (set) Token: 0x06000879 RID: 2169 RVA: 0x000276CC File Offset: 0x000258CC
		public bool IsListening { get; private set; }

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x0600087A RID: 2170 RVA: 0x000276D8 File Offset: 0x000258D8
		// (remove) Token: 0x0600087B RID: 2171 RVA: 0x00027710 File Offset: 0x00025910
		public event Action<NetworkClient> ClientConnected;

		// Token: 0x0600087C RID: 2172 RVA: 0x00027745 File Offset: 0x00025945
		public NetworkServer(IPAddress address, int port)
		{
			this._listener = new TcpListener(address, port);
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00027765 File Offset: 0x00025965
		public void Start()
		{
			if (!this.IsListening && !this._isClosed)
			{
				this.IsListening = true;
				this._listener.Start();
				this.BeginAcceptSocket();
			}
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0002778F File Offset: 0x0002598F
		public void Close()
		{
			if (!this._isClosed)
			{
				this._isClosed = true;
				this.IsListening = false;
				this._listener.Stop();
			}
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x000277B4 File Offset: 0x000259B4
		public void Update()
		{
			List<NetworkClient> clients = new List<NetworkClient>();
			List<NetworkClient> acceptedClients = this._acceptedClients;
			lock (acceptedClients)
			{
				clients.AddRange(this._acceptedClients);
				this._acceptedClients.Clear();
			}
			foreach (NetworkClient client in clients)
			{
				Action<NetworkClient> clientConnected = this.ClientConnected;
				if (clientConnected != null)
				{
					clientConnected(client);
				}
			}
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00027858 File Offset: 0x00025A58
		private void BeginAcceptSocket()
		{
			try
			{
				this._listener.BeginAcceptSocket(new AsyncCallback(this.AcceptSocketCallback), null);
			}
			catch (Exception)
			{
				this.Close();
			}
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0002789C File Offset: 0x00025A9C
		private void AcceptSocketCallback(IAsyncResult result)
		{
			try
			{
				NetworkClient client = new NetworkClient(this._listener.EndAcceptSocket(result));
				List<NetworkClient> acceptedClients = this._acceptedClients;
				lock (acceptedClients)
				{
					this._acceptedClients.Add(client);
				}
				this.BeginAcceptSocket();
			}
			catch (Exception)
			{
				this.Close();
			}
		}

		// Token: 0x04000CD4 RID: 3284
		private TcpListener _listener;

		// Token: 0x04000CD5 RID: 3285
		private bool _isClosed;

		// Token: 0x04000CD6 RID: 3286
		private List<NetworkClient> _acceptedClients = new List<NetworkClient>();
	}
}
