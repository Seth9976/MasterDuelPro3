using System;

namespace AssetStudio
{
	// Token: 0x0200017B RID: 379
	public class SerializedFileHeader
	{
		// Token: 0x04000A01 RID: 2561
		public uint m_MetadataSize;

		// Token: 0x04000A02 RID: 2562
		public long m_FileSize;

		// Token: 0x04000A03 RID: 2563
		public SerializedFileFormatVersion m_Version;

		// Token: 0x04000A04 RID: 2564
		public long m_DataOffset;

		// Token: 0x04000A05 RID: 2565
		public byte m_Endianess;

		// Token: 0x04000A06 RID: 2566
		public byte[] m_Reserved;
	}
}
