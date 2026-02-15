using System;
using System.Net;
using System.Net.Sockets;

namespace YGOSharp.Network
{
	// Token: 0x020001DB RID: 475
	public class NetworkClient
	{
		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000865 RID: 2149 RVA: 0x00027250 File Offset: 0x00025450
		// (remove) Token: 0x06000866 RID: 2150 RVA: 0x00027288 File Offset: 0x00025488
		public event Action Connected;

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06000867 RID: 2151 RVA: 0x000272C0 File Offset: 0x000254C0
		// (remove) Token: 0x06000868 RID: 2152 RVA: 0x000272F8 File Offset: 0x000254F8
		public event Action<Exception> Disconnected;

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06000869 RID: 2153 RVA: 0x00027330 File Offset: 0x00025530
		// (remove) Token: 0x0600086A RID: 2154 RVA: 0x00027368 File Offset: 0x00025568
		public event Action<byte[]> DataReceived;

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x0002739D File Offset: 0x0002559D
		// (set) Token: 0x0600086C RID: 2156 RVA: 0x000273A5 File Offset: 0x000255A5
		public bool IsConnected { get; private set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x000273AE File Offset: 0x000255AE
		public IPAddress RemoteIPAddress
		{
			get
			{
				return this._endPoint.Address;
			}
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x000273BB File Offset: 0x000255BB
		public NetworkClient()
		{
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x000273D3 File Offset: 0x000255D3
		public NetworkClient(Socket socket)
		{
			this.Initialize(socket);
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x000273F2 File Offset: 0x000255F2
		public void Initialize(Socket socket)
		{
			this._endPoint = (IPEndPoint)socket.RemoteEndPoint;
			this._socket = socket;
			this.IsConnected = true;
			Action connected = this.Connected;
			if (connected == null)
			{
				return;
			}
			connected();
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00027424 File Offset: 0x00025624
		public void BeginConnect(IPAddress address, int port)
		{
			if (!this.IsConnected && !this._isClosed)
			{
				this.IsConnected = true;
				try
				{
					this._endPoint = new IPEndPoint(address, port);
					this._socket = new Socket(this._endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
					this._socket.BeginConnect(this._endPoint, new AsyncCallback(this.ConnectCallback), null);
				}
				catch (Exception ex)
				{
					this.Close(ex);
				}
			}
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x000274A8 File Offset: 0x000256A8
		public void BeginSend(byte[] data)
		{
			try
			{
				this._socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(this.SendCallback), data.Length);
			}
			catch (Exception ex)
			{
				this.Close(ex);
			}
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x000274F8 File Offset: 0x000256F8
		public void BeginReceive()
		{
			try
			{
				this._socket.BeginReceive(this._receiveBuffer, 0, this._receiveBuffer.Length, SocketFlags.None, new AsyncCallback(this.ReceiveCallback), null);
			}
			catch (Exception ex)
			{
				this.Close(ex);
			}
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0002754C File Offset: 0x0002574C
		public void Close(Exception error = null)
		{
			if (!this._isClosed)
			{
				this._isClosed = true;
				try
				{
					if (this._socket != null)
					{
						this._socket.Close();
					}
				}
				catch (Exception ex)
				{
					ex = new AggregateException(new Exception[] { error, ex });
				}
				this.IsConnected = false;
				Action<Exception> disconnected = this.Disconnected;
				if (disconnected == null)
				{
					return;
				}
				disconnected(error);
			}
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x000275BC File Offset: 0x000257BC
		private void ConnectCallback(IAsyncResult result)
		{
			try
			{
				this._socket.EndConnect(result);
			}
			catch (Exception ex)
			{
				this.Close(ex);
				return;
			}
			Action connected = this.Connected;
			if (connected != null)
			{
				connected();
			}
			this.BeginReceive();
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00027608 File Offset: 0x00025808
		private void SendCallback(IAsyncResult result)
		{
			try
			{
				if (this._socket.EndSend(result) != (int)result.AsyncState)
				{
					this.Close(null);
				}
			}
			catch (Exception ex)
			{
				this.Close(ex);
			}
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x00027654 File Offset: 0x00025854
		private void ReceiveCallback(IAsyncResult result)
		{
			int bytesRead;
			try
			{
				bytesRead = this._socket.EndReceive(result);
			}
			catch (Exception ex)
			{
				this.Close(ex);
				return;
			}
			if (bytesRead == 0)
			{
				this.Close(null);
				return;
			}
			byte[] data = new byte[bytesRead];
			Array.Copy(this._receiveBuffer, data, bytesRead);
			Action<byte[]> dataReceived = this.DataReceived;
			if (dataReceived != null)
			{
				dataReceived(data);
			}
			this.BeginReceive();
		}

		// Token: 0x04000CCD RID: 3277
		private const int BufferSize = 4096;

		// Token: 0x04000CCE RID: 3278
		private Socket _socket;

		// Token: 0x04000CCF RID: 3279
		private IPEndPoint _endPoint;

		// Token: 0x04000CD0 RID: 3280
		private bool _isClosed;

		// Token: 0x04000CD1 RID: 3281
		private byte[] _receiveBuffer = new byte[4096];
	}
}
