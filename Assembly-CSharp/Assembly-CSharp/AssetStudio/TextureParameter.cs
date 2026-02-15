using System;

namespace AssetStudio
{
	// Token: 0x02000120 RID: 288
	public class TextureParameter
	{
		// Token: 0x06000378 RID: 888 RVA: 0x00012804 File Offset: 0x00010A04
		public TextureParameter(ObjectReader reader)
		{
			int[] version = reader.version;
			this.m_NameIndex = reader.ReadInt32();
			this.m_Index = reader.ReadInt32();
			this.m_SamplerIndex = reader.ReadInt32();
			if (version[0] > 2017 || (version[0] == 2017 && version[1] >= 3))
			{
				reader.ReadBoolean();
			}
			this.m_Dim = reader.ReadSByte();
			reader.AlignStream();
		}

		// Token: 0x040007D7 RID: 2007
		public int m_NameIndex;

		// Token: 0x040007D8 RID: 2008
		public int m_Index;

		// Token: 0x040007D9 RID: 2009
		public int m_SamplerIndex;

		// Token: 0x040007DA RID: 2010
		public sbyte m_Dim;
	}
}
