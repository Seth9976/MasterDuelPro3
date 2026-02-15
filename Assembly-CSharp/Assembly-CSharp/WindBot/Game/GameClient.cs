using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using YGOSharp.Network;
using YGOSharp.Network.Enums;
using YGOSharp.Network.Utils;

namespace WindBot.Game
{
	// Token: 0x020001FE RID: 510
	public class GameClient
	{
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x0003072F File Offset: 0x0002E92F
		// (set) Token: 0x06000AB2 RID: 2738 RVA: 0x00030737 File Offset: 0x0002E937
		public YGOClient Connection { get; private set; }

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00030740 File Offset: 0x0002E940
		public GameClient(WindBotInfo Info)
		{
			this.Username = Info.Name;
			this.Deck = Info.Deck;
			this.DeckFile = Info.DeckFile;
			this.Dialog = Info.Dialog;
			this.Hand = Info.Hand;
			this.Debug = Info.Debug;
			this._chat = Info.Chat;
			this._serverHost = Info.Host;
			this._serverPort = Info.Port;
			this._roomInfo = Info.HostInfo;
			this._proVersion = (short)Info.Version;
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x000307D8 File Offset: 0x0002E9D8
		public void Start()
		{
			this.Connection = new YGOClient();
			this._behavior = new GameBehavior(this);
			this.Connection.Connected += this.OnConnected;
			this.Connection.PacketReceived += this.OnPacketReceived;
			IPAddress target_address;
			try
			{
				target_address = IPAddress.Parse(this._serverHost);
			}
			catch (Exception)
			{
				target_address = Dns.GetHostEntry(this._serverHost).AddressList.FirstOrDefault((IPAddress findIPv4) => findIPv4.AddressFamily == AddressFamily.InterNetwork);
			}
			this.Connection.Connect(target_address, this._serverPort);
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00030894 File Offset: 0x0002EA94
		private void OnConnected()
		{
			BinaryWriter packet = GamePacketFactory.Create(CtosMessage.ExternalAddress);
			packet.Write(0U);
			packet.WriteUnicodeAutoLength(this._serverHost, 255);
			this.Connection.Send(packet);
			packet = GamePacketFactory.Create(CtosMessage.PlayerInfo);
			packet.WriteUnicode(this.Username, 20);
			this.Connection.Send(packet);
			byte[] array = new byte[6];
			array[0] = 204;
			array[1] = 204;
			byte[] junk = array;
			packet = GamePacketFactory.Create(CtosMessage.JoinGame);
			packet.Write(this._proVersion);
			packet.Write(junk);
			packet.WriteUnicode(this._roomInfo, 20);
			this.Connection.Send(packet);
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0003093B File Offset: 0x0002EB3B
		public void Tick()
		{
			this.Connection.Update();
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00030948 File Offset: 0x0002EB48
		public void Chat(string message)
		{
			BinaryWriter chat = GamePacketFactory.Create(CtosMessage.Chat);
			chat.WriteUnicodeAutoLength(message, 255);
			this.Connection.Send(chat);
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00030975 File Offset: 0x0002EB75
		public void Surrender()
		{
			this.Connection.Send(CtosMessage.Surrender);
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00030984 File Offset: 0x0002EB84
		private void OnPacketReceived(BinaryReader reader)
		{
			this._behavior.OnPacket(reader);
		}

		// Token: 0x04000DD1 RID: 3537
		public string Username;

		// Token: 0x04000DD2 RID: 3538
		public string Deck;

		// Token: 0x04000DD3 RID: 3539
		public string DeckFile;

		// Token: 0x04000DD4 RID: 3540
		public string Dialog;

		// Token: 0x04000DD5 RID: 3541
		public int Hand;

		// Token: 0x04000DD6 RID: 3542
		public bool Debug;

		// Token: 0x04000DD7 RID: 3543
		public bool _chat;

		// Token: 0x04000DD8 RID: 3544
		private string _serverHost;

		// Token: 0x04000DD9 RID: 3545
		private int _serverPort;

		// Token: 0x04000DDA RID: 3546
		private short _proVersion;

		// Token: 0x04000DDB RID: 3547
		private string _roomInfo;

		// Token: 0x04000DDC RID: 3548
		private GameBehavior _behavior;
	}
}
