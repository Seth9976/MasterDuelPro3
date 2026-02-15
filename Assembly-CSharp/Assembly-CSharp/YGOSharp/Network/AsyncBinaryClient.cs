using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace YGOSharp.Network
{
	// Token: 0x020001D7 RID: 471
	public class AsyncBinaryClient
	{
		// Token: 0x14000015 RID: 21
		// (add) Token: 0x0600082F RID: 2095 RVA: 0x000265A4 File Offset: 0x000247A4
		// (remove) Token: 0x06000830 RID: 2096 RVA: 0x000265DC File Offset: 0x000247DC
		public event Action Connected;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000831 RID: 2097 RVA: 0x00026614 File Offset: 0x00024814
		// (remove) Token: 0x06000832 RID: 2098 RVA: 0x0002664C File Offset: 0x0002484C
		public event Action<Exception> Disconnected;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000833 RID: 2099 RVA: 0x00026684 File Offset: 0x00024884
		// (remove) Token: 0x06000834 RID: 2100 RVA: 0x000266BC File Offset: 0x000248BC
		public event Action<byte[]> PacketReceived;

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x000266F1 File Offset: 0x000248F1
		public bool IsConnected
		{
			get
			{
				return this._client.IsConnected;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000836 RID: 2102 RVA: 0x000266FE File Offset: 0x000248FE
		public IPAddress RemoteIPAddress
		{
			get
			{
				return this._client.RemoteIPAddress;
			}
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0002670C File Offset: 0x0002490C
		public AsyncBinaryClient(NetworkClient client)
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

		// Token: 0x06000838 RID: 2104 RVA: 0x0002679E File Offset: 0x0002499E
		public void Connect(IPAddress address, int port)
		{
			this._client.BeginConnect(address, port);
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x000267AD File Offset: 0x000249AD
		public void Initialize(Socket socket)
		{
			this._client.Initialize(socket);
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x000267BC File Offset: 0x000249BC
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

		// Token: 0x0600083B RID: 2107 RVA: 0x0002686A File Offset: 0x00024A6A
		public void Close(Exception error = null)
		{
			this._client.Close(error);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00026878 File Offset: 0x00024A78
		private void Client_Connected()
		{
			Action connected = this.Connected;
			if (connected == null)
			{
				return;
			}
			connected();
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0002688A File Offset: 0x00024A8A
		private void Client_Disconnected(Exception ex)
		{
			Action<Exception> disconnected = this.Disconnected;
			if (disconnected == null)
			{
				return;
			}
			disconnected(ex);
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0002689D File Offset: 0x00024A9D
		private void Client_DataReceived(byte[] data)
		{
			this._receiveBuffer.AddRange(data);
			this.ExtractPackets();
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x000268B4 File Offset: 0x00024AB4
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

		// Token: 0x06000840 RID: 2112 RVA: 0x000268DC File Offset: 0x00024ADC
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

		// Token: 0x06000841 RID: 2113 RVA: 0x000269CC File Offset: 0x00024BCC
		private bool ExtractPendingPacket()
		{
			if (this._receiveBuffer.Count >= this._pendingLength)
			{
				byte[] packet = new byte[this._pendingLength];
				this._receiveBuffer.CopyTo(0, packet, 0, this._pendingLength);
				this._receiveBuffer.RemoveRange(0, this._pendingLength);
				this._pendingLength = 0;
				Action<byte[]> packetReceived = this.PacketReceived;
				if (packetReceived != null)
				{
					packetReceived(packet);
				}
				return true;
			}
			return false;
		}

		// Token: 0x04000CAF RID: 3247
		protected int MaxPacketLength = 65535;

		// Token: 0x04000CB0 RID: 3248
		protected int HeaderSize = 2;

		// Token: 0x04000CB1 RID: 3249
		protected bool IsHeaderSizeIncluded;

		// Token: 0x04000CB2 RID: 3250
		private NetworkClient _client;

		// Token: 0x04000CB3 RID: 3251
		private List<byte> _receiveBuffer = new List<byte>();

		// Token: 0x04000CB4 RID: 3252
		private byte[] _lengthBuffer = new byte[16];

		// Token: 0x04000CB5 RID: 3253
		private int _pendingLength;
	}
}
