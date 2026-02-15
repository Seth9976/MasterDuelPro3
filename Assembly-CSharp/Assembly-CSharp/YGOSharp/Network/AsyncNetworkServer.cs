using System;
using System.Net;
using System.Net.Sockets;

namespace YGOSharp.Network
{
	// Token: 0x020001D8 RID: 472
	public class AsyncNetworkServer
	{
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x00026A3A File Offset: 0x00024C3A
		// (set) Token: 0x06000843 RID: 2115 RVA: 0x00026A42 File Offset: 0x00024C42
		public bool IsListening { get; private set; }

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000844 RID: 2116 RVA: 0x00026A4C File Offset: 0x00024C4C
		// (remove) Token: 0x06000845 RID: 2117 RVA: 0x00026A84 File Offset: 0x00024C84
		public event Action<NetworkClient> ClientConnected;

		// Token: 0x06000846 RID: 2118 RVA: 0x00026AB9 File Offset: 0x00024CB9
		public AsyncNetworkServer(IPAddress address, int port)
		{
			this._listener = new TcpListener(address, port);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00026ACE File Offset: 0x00024CCE
		public void Start()
		{
			if (!this.IsListening && !this._isClosed)
			{
				this.IsListening = true;
				this._listener.Start();
				this.BeginAcceptSocket();
			}
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00026AF8 File Offset: 0x00024CF8
		public void Close()
		{
			if (!this._isClosed)
			{
				this._isClosed = true;
				this.IsListening = false;
				this._listener.Stop();
			}
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00026B1C File Offset: 0x00024D1C
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

		// Token: 0x0600084A RID: 2122 RVA: 0x00026B60 File Offset: 0x00024D60
		private void AcceptSocketCallback(IAsyncResult result)
		{
			try
			{
				Socket socket = this._listener.EndAcceptSocket(result);
				Action<NetworkClient> clientConnected = this.ClientConnected;
				if (clientConnected != null)
				{
					clientConnected(new NetworkClient(socket));
				}
				this.BeginAcceptSocket();
			}
			catch (Exception)
			{
				this.Close();
			}
		}

		// Token: 0x04000CB8 RID: 3256
		private TcpListener _listener;

		// Token: 0x04000CB9 RID: 3257
		private bool _isClosed;
	}
}
