using System;
using System.IO;

namespace AssetStudio
{
	// Token: 0x0200010E RID: 270
	public class Hash128
	{
		// Token: 0x06000369 RID: 873 RVA: 0x000122A9 File Offset: 0x000104A9
		public Hash128(BinaryReader reader)
		{
			this.bytes = reader.ReadBytes(16);
		}

		// Token: 0x04000778 RID: 1912
		public byte[] bytes;
	}
}
