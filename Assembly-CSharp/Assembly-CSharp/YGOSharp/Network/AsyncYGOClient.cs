using System;
using System.IO;
using YGOSharp.Network.Enums;

namespace YGOSharp.Network
{
	// Token: 0x020001D9 RID: 473
	public class AsyncYGOClient : AsyncBinaryClient
	{
		// Token: 0x0600084B RID: 2123 RVA: 0x00026BB4 File Offset: 0x00024DB4
		public AsyncYGOClient()
			: base(new NetworkClient())
		{
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00026BC1 File Offset: 0x00024DC1
		public AsyncYGOClient(NetworkClient client)
			: base(client)
		{
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00026BCA File Offset: 0x00024DCA
		public void Send(BinaryWriter writer)
		{
			base.Send(((MemoryStream)writer.BaseStream).ToArray());
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00026BE4 File Offset: 0x00024DE4
		public void Send(CtosMessage message)
		{
			using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
			{
				writer.Write((byte)message);
				this.Send(writer);
			}
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00026C28 File Offset: 0x00024E28
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
