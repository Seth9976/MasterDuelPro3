using System;

namespace AssetStudio
{
	// Token: 0x02000121 RID: 289
	public class BufferBinding
	{
		// Token: 0x06000379 RID: 889 RVA: 0x00012875 File Offset: 0x00010A75
		public BufferBinding(ObjectReader reader)
		{
			int[] version = reader.version;
			this.m_NameIndex = reader.ReadInt32();
			this.m_Index = reader.ReadInt32();
			if (version[0] >= 2020)
			{
				this.m_ArraySize = reader.ReadInt32();
			}
		}

		// Token: 0x040007DB RID: 2011
		public int m_NameIndex;

		// Token: 0x040007DC RID: 2012
		public int m_Index;

		// Token: 0x040007DD RID: 2013
		public int m_ArraySize;
	}
}
