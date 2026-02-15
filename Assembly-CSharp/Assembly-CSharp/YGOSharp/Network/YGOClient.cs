using System;
using System.IO;
using YGOSharp.Network.Enums;

namespace YGOSharp.Network
{
	// Token: 0x020001DD RID: 477
	public class YGOClient : BinaryClient
	{
		// Token: 0x06000882 RID: 2178 RVA: 0x00027914 File Offset: 0x00025B14
		public YGOClient()
			: base(new NetworkClient())
		{
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00027921 File Offset: 0x00025B21
		public YGOClient(NetworkClient client)
			: base(client)
		{
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0002792A File Offset: 0x00025B2A
		public void Send(BinaryWriter writer)
		{
			base.Send(((MemoryStream)writer.BaseStream).ToArray());
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00027944 File Offset: 0x00025B44
		public void Send(CtosMessage message)
		{
			using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
			{
				writer.Write((byte)message);
				this.Send(writer);
			}
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00027988 File Offset: 0x00025B88
		public void Send(CtosMessage message, int value)
		{
			using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
			{
				writer.Write((byte)message);
				writer.Write(value);
				this.Send(writer);
			}
		}
	}
}
