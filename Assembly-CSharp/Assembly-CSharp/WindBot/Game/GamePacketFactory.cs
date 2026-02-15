using System;
using System.IO;
using YGOSharp.Network.Enums;

namespace WindBot.Game
{
	// Token: 0x02000200 RID: 512
	public class GamePacketFactory
	{
		// Token: 0x06000ABD RID: 2749 RVA: 0x00024B8D File Offset: 0x00022D8D
		public static BinaryWriter Create(CtosMessage message)
		{
			BinaryWriter binaryWriter = new BinaryWriter(new MemoryStream());
			binaryWriter.Write((byte)message);
			return binaryWriter;
		}
	}
}
