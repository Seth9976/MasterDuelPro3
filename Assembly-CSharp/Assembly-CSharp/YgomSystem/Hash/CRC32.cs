using System;
using System.IO;

namespace YgomSystem.Hash
{
	// Token: 0x02000769 RID: 1897
	public class CRC32
	{
		// Token: 0x06003B20 RID: 15136 RVA: 0x000029CC File Offset: 0x00000BCC
		private static uint GetMemCRC32(uint crc32, byte[] data, int size)
		{
			return 0U;
		}

		// Token: 0x06003B21 RID: 15137 RVA: 0x000029CC File Offset: 0x00000BCC
		public static uint GetBinaryStreamCRC32(BinaryReader br)
		{
			return 0U;
		}

		// Token: 0x06003B22 RID: 15138 RVA: 0x000029CC File Offset: 0x00000BCC
		public static uint GetStringCRC32(string str)
		{
			return 0U;
		}

		// Token: 0x06003B23 RID: 15139 RVA: 0x000029CC File Offset: 0x00000BCC
		public static uint GetBinaryCRC32(byte[] data)
		{
			return 0U;
		}

		// Token: 0x0400347F RID: 13439
		private static readonly uint[] CRC32Table;

		// Token: 0x04003480 RID: 13440
		private static readonly int CHAR_BIT;

		// Token: 0x04003481 RID: 13441
		private static readonly int CHUNK_SIZE;
	}
}
