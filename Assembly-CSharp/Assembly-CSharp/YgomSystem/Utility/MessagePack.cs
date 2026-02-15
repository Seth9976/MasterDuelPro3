using System;
using System.IO;
using MiniMessagePack;

namespace YgomSystem.Utility
{
	// Token: 0x0200052E RID: 1326
	public class MessagePack
	{
		// Token: 0x06002A90 RID: 10896 RVA: 0x0000216A File Offset: 0x0000036A
		public static byte[] Pack(object o)
		{
			return null;
		}

		// Token: 0x06002A91 RID: 10897 RVA: 0x0000216D File Offset: 0x0000036D
		public void Pack(Stream s, object o)
		{
		}

		// Token: 0x06002A92 RID: 10898 RVA: 0x0000216A File Offset: 0x0000036A
		public static object Unpack(byte[] buf, int offset, int size)
		{
			return null;
		}

		// Token: 0x06002A93 RID: 10899 RVA: 0x0000216A File Offset: 0x0000036A
		public static object Unpack(byte[] buf)
		{
			return null;
		}

		// Token: 0x06002A94 RID: 10900 RVA: 0x0000216A File Offset: 0x0000036A
		public static object Unpack(Stream s)
		{
			return null;
		}

		// Token: 0x040029B7 RID: 10679
		public static MiniMessagePacker packer;
	}
}
