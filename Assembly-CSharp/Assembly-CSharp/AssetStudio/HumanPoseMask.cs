using System;

namespace AssetStudio
{
	// Token: 0x020000BB RID: 187
	public class HumanPoseMask
	{
		// Token: 0x060002F9 RID: 761 RVA: 0x0000DD58 File Offset: 0x0000BF58
		public HumanPoseMask(ObjectReader reader)
		{
			int[] version = reader.version;
			this.word0 = reader.ReadUInt32();
			this.word1 = reader.ReadUInt32();
			if (version[0] > 5 || (version[0] == 5 && version[1] >= 2))
			{
				this.word2 = reader.ReadUInt32();
			}
		}

		// Token: 0x040005DF RID: 1503
		public uint word0;

		// Token: 0x040005E0 RID: 1504
		public uint word1;

		// Token: 0x040005E1 RID: 1505
		public uint word2;
	}
}
