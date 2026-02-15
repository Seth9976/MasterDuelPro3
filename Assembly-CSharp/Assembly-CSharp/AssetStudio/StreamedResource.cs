using System;
using System.IO;

namespace AssetStudio
{
	// Token: 0x02000145 RID: 325
	public class StreamedResource
	{
		// Token: 0x0600039B RID: 923 RVA: 0x00014189 File Offset: 0x00012389
		public StreamedResource(BinaryReader reader)
		{
			this.m_Source = reader.ReadAlignedString();
			this.m_Offset = reader.ReadInt64();
			this.m_Size = reader.ReadInt64();
		}

		// Token: 0x04000915 RID: 2325
		public string m_Source;

		// Token: 0x04000916 RID: 2326
		public long m_Offset;

		// Token: 0x04000917 RID: 2327
		public long m_Size;
	}
}
