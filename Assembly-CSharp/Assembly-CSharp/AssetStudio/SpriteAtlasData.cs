using System;

namespace AssetStudio
{
	// Token: 0x0200013C RID: 316
	public class SpriteAtlasData
	{
		// Token: 0x06000393 RID: 915 RVA: 0x00013B60 File Offset: 0x00011D60
		public SpriteAtlasData(ObjectReader reader)
		{
			int[] version = reader.version;
			this.texture = new PPtr<Texture2D>(reader);
			this.alphaTexture = new PPtr<Texture2D>(reader);
			this.textureRect = new Rectf(reader);
			this.textureRectOffset = reader.ReadVector2();
			if (version[0] > 2017 || (version[0] == 2017 && version[1] >= 2))
			{
				this.atlasRectOffset = reader.ReadVector2();
			}
			this.uvTransform = reader.ReadVector4();
			this.downscaleMultiplier = reader.ReadSingle();
			this.settingsRaw = new SpriteSettings(reader);
			if (version[0] > 2020 || (version[0] == 2020 && version[1] >= 2))
			{
				int secondaryTexturesSize = reader.ReadInt32();
				this.secondaryTextures = new SecondarySpriteTexture[secondaryTexturesSize];
				for (int i = 0; i < secondaryTexturesSize; i++)
				{
					this.secondaryTextures[i] = new SecondarySpriteTexture(reader);
				}
				reader.AlignStream();
			}
		}

		// Token: 0x040008AD RID: 2221
		public PPtr<Texture2D> texture;

		// Token: 0x040008AE RID: 2222
		public PPtr<Texture2D> alphaTexture;

		// Token: 0x040008AF RID: 2223
		public Rectf textureRect;

		// Token: 0x040008B0 RID: 2224
		public Vector2 textureRectOffset;

		// Token: 0x040008B1 RID: 2225
		public Vector2 atlasRectOffset;

		// Token: 0x040008B2 RID: 2226
		public Vector4 uvTransform;

		// Token: 0x040008B3 RID: 2227
		public float downscaleMultiplier;

		// Token: 0x040008B4 RID: 2228
		public SpriteSettings settingsRaw;

		// Token: 0x040008B5 RID: 2229
		public SecondarySpriteTexture[] secondaryTextures;
	}
}
