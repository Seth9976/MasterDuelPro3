using System;

namespace AssetStudio
{
	// Token: 0x02000146 RID: 326
	public sealed class VideoClip : NamedObject
	{
		// Token: 0x0600039C RID: 924 RVA: 0x000141B8 File Offset: 0x000123B8
		public VideoClip(ObjectReader reader)
			: base(reader)
		{
			this.m_OriginalPath = reader.ReadAlignedString();
			reader.ReadUInt32();
			reader.ReadUInt32();
			reader.ReadUInt32();
			reader.ReadUInt32();
			if (this.version[0] > 2017 || (this.version[0] == 2017 && this.version[1] >= 2))
			{
				reader.ReadUInt32();
				reader.ReadUInt32();
			}
			reader.ReadDouble();
			reader.ReadUInt64();
			reader.ReadInt32();
			reader.ReadUInt16Array();
			reader.AlignStream();
			reader.ReadUInt32Array();
			reader.ReadStringArray();
			if (this.version[0] >= 2020)
			{
				int m_VideoShadersSize = reader.ReadInt32();
				PPtr<Shader>[] m_VideoShaders = new PPtr<Shader>[m_VideoShadersSize];
				for (int i = 0; i < m_VideoShadersSize; i++)
				{
					m_VideoShaders[i] = new PPtr<Shader>(reader);
				}
			}
			this.m_ExternalResources = new StreamedResource(reader);
			reader.ReadBoolean();
			if (this.version[0] >= 2020)
			{
				reader.ReadBoolean();
			}
			ResourceReader resourceReader;
			if (!string.IsNullOrEmpty(this.m_ExternalResources.m_Source))
			{
				resourceReader = new ResourceReader(this.m_ExternalResources.m_Source, this.assetsFile, this.m_ExternalResources.m_Offset, this.m_ExternalResources.m_Size);
			}
			else
			{
				resourceReader = new ResourceReader(reader, reader.BaseStream.Position, this.m_ExternalResources.m_Size);
			}
			this.m_VideoData = resourceReader;
		}

		// Token: 0x04000918 RID: 2328
		public ResourceReader m_VideoData;

		// Token: 0x04000919 RID: 2329
		public string m_OriginalPath;

		// Token: 0x0400091A RID: 2330
		public StreamedResource m_ExternalResources;
	}
}
