using System;
using System.IO;

namespace AssetStudio
{
	// Token: 0x02000112 RID: 274
	public class SerializedTextureProperty
	{
		// Token: 0x0600036C RID: 876 RVA: 0x00012369 File Offset: 0x00010569
		public SerializedTextureProperty(BinaryReader reader)
		{
			this.m_DefaultName = reader.ReadAlignedString();
			this.m_TexDim = (TextureDimension)reader.ReadInt32();
		}

		// Token: 0x04000786 RID: 1926
		public string m_DefaultName;

		// Token: 0x04000787 RID: 1927
		public TextureDimension m_TexDim;
	}
}
