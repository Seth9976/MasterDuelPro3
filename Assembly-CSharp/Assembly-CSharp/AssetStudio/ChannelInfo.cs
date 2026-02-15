using System;

namespace AssetStudio
{
	// Token: 0x020000EF RID: 239
	public class ChannelInfo
	{
		// Token: 0x0600032E RID: 814 RVA: 0x00002739 File Offset: 0x00000939
		public ChannelInfo()
		{
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000F8FF File Offset: 0x0000DAFF
		public ChannelInfo(ObjectReader reader)
		{
			this.stream = reader.ReadByte();
			this.offset = reader.ReadByte();
			this.format = reader.ReadByte();
			this.dimension = reader.ReadByte() & 15;
		}

		// Token: 0x040006F3 RID: 1779
		public byte stream;

		// Token: 0x040006F4 RID: 1780
		public byte offset;

		// Token: 0x040006F5 RID: 1781
		public byte format;

		// Token: 0x040006F6 RID: 1782
		public byte dimension;
	}
}
