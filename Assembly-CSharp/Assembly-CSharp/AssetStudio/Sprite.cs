using System;
using System.Collections.Generic;

namespace AssetStudio
{
	// Token: 0x0200013B RID: 315
	public sealed class Sprite : NamedObject
	{
		// Token: 0x06000392 RID: 914 RVA: 0x0001398C File Offset: 0x00011B8C
		public Sprite(ObjectReader reader)
			: base(reader)
		{
			this.m_Rect = new Rectf(reader);
			this.m_Offset = reader.ReadVector2();
			if (this.version[0] > 4 || (this.version[0] == 4 && this.version[1] >= 5))
			{
				this.m_Border = reader.ReadVector4();
			}
			this.m_PixelsToUnits = reader.ReadSingle();
			if (this.version[0] > 5 || (this.version[0] == 5 && this.version[1] > 4) || (this.version[0] == 5 && this.version[1] == 4 && this.version[2] >= 2) || (this.version[0] == 5 && this.version[1] == 4 && this.version[2] == 1 && this.buildType.IsPatch && this.version[3] >= 3))
			{
				this.m_Pivot = reader.ReadVector2();
			}
			this.m_Extrude = reader.ReadUInt32();
			if (this.version[0] > 5 || (this.version[0] == 5 && this.version[1] >= 3))
			{
				this.m_IsPolygon = reader.ReadBoolean();
				reader.AlignStream();
			}
			if (this.version[0] >= 2017)
			{
				Guid first = new Guid(reader.ReadBytes(16));
				long second = reader.ReadInt64();
				this.m_RenderDataKey = new KeyValuePair<Guid, long>(first, second);
				this.m_AtlasTags = reader.ReadStringArray();
				this.m_SpriteAtlas = new PPtr<SpriteAtlas>(reader);
			}
			this.m_RD = new SpriteRenderData(reader);
			if (this.version[0] >= 2017)
			{
				int m_PhysicsShapeSize = reader.ReadInt32();
				this.m_PhysicsShape = new Vector2[m_PhysicsShapeSize][];
				for (int i = 0; i < m_PhysicsShapeSize; i++)
				{
					this.m_PhysicsShape[i] = reader.ReadVector2Array();
				}
			}
		}

		// Token: 0x040008A1 RID: 2209
		public Rectf m_Rect;

		// Token: 0x040008A2 RID: 2210
		public Vector2 m_Offset;

		// Token: 0x040008A3 RID: 2211
		public Vector4 m_Border;

		// Token: 0x040008A4 RID: 2212
		public float m_PixelsToUnits;

		// Token: 0x040008A5 RID: 2213
		public Vector2 m_Pivot = new Vector2(0.5f, 0.5f);

		// Token: 0x040008A6 RID: 2214
		public uint m_Extrude;

		// Token: 0x040008A7 RID: 2215
		public bool m_IsPolygon;

		// Token: 0x040008A8 RID: 2216
		public KeyValuePair<Guid, long> m_RenderDataKey;

		// Token: 0x040008A9 RID: 2217
		public string[] m_AtlasTags;

		// Token: 0x040008AA RID: 2218
		public PPtr<SpriteAtlas> m_SpriteAtlas;

		// Token: 0x040008AB RID: 2219
		public SpriteRenderData m_RD;

		// Token: 0x040008AC RID: 2220
		public Vector2[][] m_PhysicsShape;
	}
}
