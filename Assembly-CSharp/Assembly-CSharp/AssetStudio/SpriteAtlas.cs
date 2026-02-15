using System;
using System.Collections.Generic;

namespace AssetStudio
{
	// Token: 0x0200013D RID: 317
	public sealed class SpriteAtlas : NamedObject
	{
		// Token: 0x06000394 RID: 916 RVA: 0x00013C44 File Offset: 0x00011E44
		public SpriteAtlas(ObjectReader reader)
			: base(reader)
		{
			int m_PackedSpritesSize = reader.ReadInt32();
			this.m_PackedSprites = new PPtr<Sprite>[m_PackedSpritesSize];
			for (int i = 0; i < m_PackedSpritesSize; i++)
			{
				this.m_PackedSprites[i] = new PPtr<Sprite>(reader);
			}
			reader.ReadStringArray();
			int m_RenderDataMapSize = reader.ReadInt32();
			this.m_RenderDataMap = new Dictionary<KeyValuePair<Guid, long>, SpriteAtlasData>(m_RenderDataMapSize);
			for (int j = 0; j < m_RenderDataMapSize; j++)
			{
				Guid first = new Guid(reader.ReadBytes(16));
				long second = reader.ReadInt64();
				SpriteAtlasData value = new SpriteAtlasData(reader);
				this.m_RenderDataMap.Add(new KeyValuePair<Guid, long>(first, second), value);
			}
			reader.ReadAlignedString();
			this.m_IsVariant = reader.ReadBoolean();
			reader.AlignStream();
		}

		// Token: 0x040008B6 RID: 2230
		public PPtr<Sprite>[] m_PackedSprites;

		// Token: 0x040008B7 RID: 2231
		public Dictionary<KeyValuePair<Guid, long>, SpriteAtlasData> m_RenderDataMap;

		// Token: 0x040008B8 RID: 2232
		public bool m_IsVariant;
	}
}
