using System;

namespace AssetStudio
{
	// Token: 0x02000102 RID: 258
	public sealed class MovieTexture : Texture
	{
		// Token: 0x06000350 RID: 848 RVA: 0x00011926 File Offset: 0x0000FB26
		public MovieTexture(ObjectReader reader)
			: base(reader)
		{
			reader.ReadBoolean();
			reader.AlignStream();
			this.m_AudioClip = new PPtr<AudioClip>(reader);
			this.m_MovieData = reader.ReadUInt8Array();
		}

		// Token: 0x0400075E RID: 1886
		public byte[] m_MovieData;

		// Token: 0x0400075F RID: 1887
		public PPtr<AudioClip> m_AudioClip;
	}
}
