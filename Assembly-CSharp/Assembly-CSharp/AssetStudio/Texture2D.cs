using System;

namespace AssetStudio
{
	// Token: 0x02000142 RID: 322
	public sealed class Texture2D : Texture
	{
		// Token: 0x06000399 RID: 921 RVA: 0x00013E5C File Offset: 0x0001205C
		public Texture2D(ObjectReader reader)
			: base(reader)
		{
			this.m_Width = reader.ReadInt32();
			this.m_Height = reader.ReadInt32();
			reader.ReadInt32();
			if (this.version[0] >= 2020)
			{
				reader.ReadInt32();
			}
			this.m_TextureFormat = (TextureFormat)reader.ReadInt32();
			if (this.version[0] < 5 || (this.version[0] == 5 && this.version[1] < 2))
			{
				this.m_MipMap = reader.ReadBoolean();
			}
			else
			{
				this.m_MipCount = reader.ReadInt32();
			}
			if (this.version[0] > 2 || (this.version[0] == 2 && this.version[1] >= 6))
			{
				reader.ReadBoolean();
			}
			if (this.version[0] >= 2020)
			{
				reader.ReadBoolean();
			}
			if (this.version[0] > 2019 || (this.version[0] == 2019 && this.version[1] >= 3))
			{
				reader.ReadBoolean();
			}
			if (this.version[0] >= 3 && (this.version[0] < 5 || (this.version[0] == 5 && this.version[1] <= 4)))
			{
				reader.ReadBoolean();
			}
			if (this.version[0] > 2018 || (this.version[0] == 2018 && this.version[1] >= 2))
			{
				reader.ReadBoolean();
			}
			reader.AlignStream();
			if (this.version[0] > 2018 || (this.version[0] == 2018 && this.version[1] >= 2))
			{
				reader.ReadInt32();
			}
			reader.ReadInt32();
			reader.ReadInt32();
			this.m_TextureSettings = new GLTextureSettings(reader);
			if (this.version[0] >= 3)
			{
				reader.ReadInt32();
			}
			if (this.version[0] > 3 || (this.version[0] == 3 && this.version[1] >= 5))
			{
				reader.ReadInt32();
			}
			if (this.version[0] > 2020 || (this.version[0] == 2020 && this.version[1] >= 2))
			{
				reader.ReadUInt8Array();
				reader.AlignStream();
			}
			int image_data_size = reader.ReadInt32();
			if (image_data_size == 0 && ((this.version[0] == 5 && this.version[1] >= 3) || this.version[0] > 5))
			{
				this.m_StreamData = new StreamingInfo(reader);
			}
			StreamingInfo streamData = this.m_StreamData;
			ResourceReader resourceReader;
			if (!string.IsNullOrEmpty((streamData != null) ? streamData.path : null))
			{
				resourceReader = new ResourceReader(this.m_StreamData.path, this.assetsFile, this.m_StreamData.offset, (long)((ulong)this.m_StreamData.size));
			}
			else
			{
				resourceReader = new ResourceReader(reader, reader.BaseStream.Position, (long)image_data_size);
			}
			this.image_data = resourceReader;
		}

		// Token: 0x040008C1 RID: 2241
		public int m_Width;

		// Token: 0x040008C2 RID: 2242
		public int m_Height;

		// Token: 0x040008C3 RID: 2243
		public TextureFormat m_TextureFormat;

		// Token: 0x040008C4 RID: 2244
		public bool m_MipMap;

		// Token: 0x040008C5 RID: 2245
		public int m_MipCount;

		// Token: 0x040008C6 RID: 2246
		public GLTextureSettings m_TextureSettings;

		// Token: 0x040008C7 RID: 2247
		public ResourceReader image_data;

		// Token: 0x040008C8 RID: 2248
		public StreamingInfo m_StreamData;
	}
}
