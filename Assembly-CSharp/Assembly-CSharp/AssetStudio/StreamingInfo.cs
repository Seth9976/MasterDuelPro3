using System;

namespace AssetStudio
{
	// Token: 0x02000140 RID: 320
	public class StreamingInfo
	{
		// Token: 0x06000397 RID: 919 RVA: 0x00013D94 File Offset: 0x00011F94
		public StreamingInfo(ObjectReader reader)
		{
			if (reader.version[0] >= 2020)
			{
				this.offset = reader.ReadInt64();
			}
			else
			{
				this.offset = (long)((ulong)reader.ReadUInt32());
			}
			this.size = reader.ReadUInt32();
			this.path = reader.ReadAlignedString();
		}

		// Token: 0x040008BA RID: 2234
		public long offset;

		// Token: 0x040008BB RID: 2235
		public uint size;

		// Token: 0x040008BC RID: 2236
		public string path;
	}
}
