using System;

namespace AssetStudio
{
	// Token: 0x02000141 RID: 321
	public class GLTextureSettings
	{
		// Token: 0x06000398 RID: 920 RVA: 0x00013DEC File Offset: 0x00011FEC
		public GLTextureSettings(ObjectReader reader)
		{
			int[] version = reader.version;
			this.m_FilterMode = reader.ReadInt32();
			this.m_Aniso = reader.ReadInt32();
			this.m_MipBias = reader.ReadSingle();
			if (version[0] >= 2017)
			{
				this.m_WrapMode = reader.ReadInt32();
				reader.ReadInt32();
				reader.ReadInt32();
				return;
			}
			this.m_WrapMode = reader.ReadInt32();
		}

		// Token: 0x040008BD RID: 2237
		public int m_FilterMode;

		// Token: 0x040008BE RID: 2238
		public int m_Aniso;

		// Token: 0x040008BF RID: 2239
		public float m_MipBias;

		// Token: 0x040008C0 RID: 2240
		public int m_WrapMode;
	}
}
