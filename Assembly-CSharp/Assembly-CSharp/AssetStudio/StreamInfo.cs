using System;

namespace AssetStudio
{
	// Token: 0x020000EE RID: 238
	public class StreamInfo
	{
		// Token: 0x0600032C RID: 812 RVA: 0x00002739 File Offset: 0x00000939
		public StreamInfo()
		{
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000F88C File Offset: 0x0000DA8C
		public StreamInfo(ObjectReader reader)
		{
			int[] version = reader.version;
			this.channelMask = reader.ReadUInt32();
			this.offset = reader.ReadUInt32();
			if (version[0] < 4)
			{
				this.stride = reader.ReadUInt32();
				this.align = reader.ReadUInt32();
				return;
			}
			this.stride = (uint)reader.ReadByte();
			this.dividerOp = reader.ReadByte();
			this.frequency = reader.ReadUInt16();
		}

		// Token: 0x040006ED RID: 1773
		public uint channelMask;

		// Token: 0x040006EE RID: 1774
		public uint offset;

		// Token: 0x040006EF RID: 1775
		public uint stride;

		// Token: 0x040006F0 RID: 1776
		public uint align;

		// Token: 0x040006F1 RID: 1777
		public byte dividerOp;

		// Token: 0x040006F2 RID: 1778
		public ushort frequency;
	}
}
