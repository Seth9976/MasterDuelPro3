using System;
using System.IO;
using YGOSharp.Network.Enums;
using YGOSharp.OCGWrapper.Enums;

namespace YGOSharp
{
	// Token: 0x020001B9 RID: 441
	public static class GamePacketFactory
	{
		// Token: 0x0600076E RID: 1902 RVA: 0x00024B8D File Offset: 0x00022D8D
		public static BinaryWriter Create(StocMessage message)
		{
			BinaryWriter binaryWriter = new BinaryWriter(new MemoryStream());
			binaryWriter.Write((byte)message);
			return binaryWriter;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00024BA1 File Offset: 0x00022DA1
		public static BinaryWriter Create(GameMessage message)
		{
			BinaryWriter binaryWriter = GamePacketFactory.Create(StocMessage.GameMsg);
			binaryWriter.Write((byte)message);
			return binaryWriter;
		}
	}
}
