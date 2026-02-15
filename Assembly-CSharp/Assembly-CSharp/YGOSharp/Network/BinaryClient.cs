using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace YGOSharp.Network
{
	// Token: 0x020001DA RID: 474
	public class BinaryClient
	{
		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000850 RID: 2128 RVA: 0x00026C74 File Offset: 0x00024E74
		// (remove) Token: 0x06000851 RID: 2129 RVA: 0x00026CAC File Offset: 0x00024EAC
		public event Action Connected;

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06000852 RID: 2130 RVA: 0x00026CE4 File Offset: 0x00024EE4
		// (remove) Token: 0x06000853 RID: 2131 RVA: 0x00026D1C File Offset: 0x00024F1C
		public event Action<Exception> Disconnected;

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000854 RID: 2132 RVA: 0x00026D54 File Offset: 0x00024F54
		// (remove) Token: 0x06000855 RID: 2133 RVA: 0x00026D8C File Offset: 0x00024F8C
		public event Action<BinaryReader> PacketReceived;

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x00026DC1 File Offset: 0x00024FC1
		public bool IsConnected
		{
			get
			{
				return !this._wasDisconnectedEventFired;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x00026DCC File Offset: 0x00024FCC
		public IPAddress RemoteIPAddress
		{
			get
			{
				return this._client.RemoteIPAddress;
			}
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00026DDC File Offset: 0x00024FDC
		public BinaryClient(NetworkClient client)
		{
			this._client = client;
			client.Connected += this.Client_Connected;
			client.Disconnected += this.Client_Disconnected;
			client.DataReceived += this.Client_DataReceived;
			if (this._client.IsConnected)
			{
				this._client.BeginReceive();
			}
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00026E79 File Offset: 0x00025079
		public void Connect(IPAddress address, int port)
		{
			this._client.BeginConnect(address, port);
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00026E88 File Offset: 0x00025088
		public void Initialize(Socket socket)
		{
			this._client.Initialize(socket);
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00026E98 File Offset: 0x00025098
		public void Update()
		{
			if (this._wasConnected)
			{
				this._wasConnected = false;
				Action connected = this.Connected;
				if (connected != null)
				{
					connected();
				}
			}
			this.ReceivePendingPackets();
			if (this._wasDisconnected && !this._wasDisconnectedEventFired)
			{
				this._wasDisconnectedEventFired = true;
				Action<Exception> disconnected = this.Disconnected;
				if (disconnected == null)
				{
					return;
				}
				disconnected(this._closingException);
			}
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00026EF8 File Offset: 0x000250F8
		public void Send(byte[] packet)
		{
			if (packet.Length > this.MaxPacketLength)
			{
				throw new Exception("Tried to send a too large packet");
			}
			int packetLength = packet.Length;
			if (this.IsHeaderSizeIncluded)
			{
				packetLength += this.HeaderSize;
			}
			byte[] header;
			if (this.HeaderSize == 2)
			{
				header = BitConverter.GetBytes((ushort)packetLength);
			}
			else
			{
				if (this.HeaderSize != 4)
				{
					throw new Exception("Unsupported header size: " + this.HeaderSize.ToString());
				}
				header = BitConverter.GetBytes(packetLength);
			}
			byte[] data = new byte[packet.Length + this.HeaderSize];
			Array.Copy(header, 0, data, 0, header.Length);
			Array.Copy(packet, 0, data, header.Length, packet.Length);
			this._client.BeginSend(data);
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00026FA6 File Offset: 0x000251A6
		public void Close(Exception error = null)
		{
			this._client.Close(error);
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x00026FB4 File Offset: 0x000251B4
		private void ReceivePendingPackets()
		{
			bool hasReceived;
			do
			{
				byte[] packet = null;
				Queue<byte[]> pendingPackets = this._pendingPackets;
				lock (pendingPackets)
				{
					if (this._pendingPackets.Count > 0)
					{
						packet = this._pendingPackets.Dequeue();
					}
				}
				hasReceived = false;
				if (packet != null)
				{
					hasReceived = true;
					using (MemoryStream stream = new MemoryStream(packet, false))
					{
						using (BinaryReader reader = new BinaryReader(stream))
						{
							Action<BinaryReader> packetReceived = this.PacketReceived;
							if (packetReceived != null)
							{
								packetReceived(reader);
							}
						}
					}
				}
			}
			while (hasReceived);
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x00027070 File Offset: 0x00025270
		private void Client_Connected()
		{
			this._wasConnected = true;
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00027079 File Offset: 0x00025279
		private void Client_Disconnected(Exception ex)
		{
			this._wasDisconnected = true;
			this._closingException = ex;
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x00027089 File Offset: 0x00025289
		private void Client_DataReceived(byte[] data)
		{
			this._receiveBuffer.AddRange(data);
			this.ExtractPackets();
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x000270A0 File Offset: 0x000252A0
		private void ExtractPackets()
		{
			bool hasExtracted;
			do
			{
				if (this._pendingLength == 0)
				{
					hasExtracted = this.ExtractPendingLength();
				}
				else
				{
					hasExtracted = this.ExtractPendingPacket();
				}
			}
			while (hasExtracted);
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x000270C8 File Offset: 0x000252C8
		private bool ExtractPendingLength()
		{
			if (this._receiveBuffer.Count < this.HeaderSize)
			{
				return false;
			}
			this._receiveBuffer.CopyTo(0, this._lengthBuffer, 0, this.HeaderSize);
			if (this.HeaderSize == 2)
			{
				this._pendingLength = (int)BitConverter.ToUInt16(this._lengthBuffer, 0);
			}
			else
			{
				if (this.HeaderSize != 4)
				{
					throw new Exception("Unsupported header size: " + this.HeaderSize.ToString());
				}
				this._pendingLength = BitConverter.ToInt32(this._lengthBuffer, 0);
			}
			this._receiveBuffer.RemoveRange(0, this.HeaderSize);
			if (this.IsHeaderSizeIncluded)
			{
				this._pendingLength -= this.HeaderSize;
			}
			if (this._pendingLength < 0 || this._pendingLength > this.MaxPacketLength)
			{
				this._client.Close(new Exception("Tried to receive a too large packet"));
				return false;
			}
			return true;
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x000271B8 File Offset: 0x000253B8
		private bool ExtractPendingPacket()
		{
			if (this._receiveBuffer.Count >= this._pendingLength)
			{
				byte[] packet = new byte[this._pendingLength];
				this._receiveBuffer.CopyTo(0, packet, 0, this._pendingLength);
				this._receiveBuffer.RemoveRange(0, this._pendingLength);
				this._pendingLength = 0;
				Queue<byte[]> pendingPackets = this._pendingPackets;
				lock (pendingPackets)
				{
					this._pendingPackets.Enqueue(packet);
				}
				return true;
			}
			return false;
		}

		// Token: 0x04000CBD RID: 3261
		protected int MaxPacketLength = 65535;

		// Token: 0x04000CBE RID: 3262
		protected int HeaderSize = 2;

		// Token: 0x04000CBF RID: 3263
		protected bool IsHeaderSizeIncluded;

		// Token: 0x04000CC0 RID: 3264
		private NetworkClient _client;

		// Token: 0x04000CC1 RID: 3265
		private List<byte> _receiveBuffer = new List<byte>();

		// Token: 0x04000CC2 RID: 3266
		private Queue<byte[]> _pendingPackets = new Queue<byte[]>();

		// Token: 0x04000CC3 RID: 3267
		private byte[] _lengthBuffer = new byte[16];

		// Token: 0x04000CC4 RID: 3268
		private int _pendingLength;

		// Token: 0x04000CC5 RID: 3269
		private bool _wasConnected;

		// Token: 0x04000CC6 RID: 3270
		private bool _wasDisconnected;

		// Token: 0x04000CC7 RID: 3271
		private bool _wasDisconnectedEventFired;

		// Token: 0x04000CC8 RID: 3272
		private Exception _closingException;
	}
}
