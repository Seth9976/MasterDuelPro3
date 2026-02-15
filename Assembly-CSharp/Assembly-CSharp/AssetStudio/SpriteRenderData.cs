using System;

namespace AssetStudio
{
	// Token: 0x02000139 RID: 313
	public class SpriteRenderData
	{
		// Token: 0x06000390 RID: 912 RVA: 0x00013768 File Offset: 0x00011968
		public SpriteRenderData(ObjectReader reader)
		{
			int[] version = reader.version;
			this.texture = new PPtr<Texture2D>(reader);
			if (version[0] > 5 || (version[0] == 5 && version[1] >= 2))
			{
				this.alphaTexture = new PPtr<Texture2D>(reader);
			}
			if (version[0] >= 2019)
			{
				int secondaryTexturesSize = reader.ReadInt32();
				this.secondaryTextures = new SecondarySpriteTexture[secondaryTexturesSize];
				for (int i = 0; i < secondaryTexturesSize; i++)
				{
					this.secondaryTextures[i] = new SecondarySpriteTexture(reader);
				}
			}
			if (version[0] > 5 || (version[0] == 5 && version[1] >= 6))
			{
				int m_SubMeshesSize = reader.ReadInt32();
				this.m_SubMeshes = new SubMesh[m_SubMeshesSize];
				for (int j = 0; j < m_SubMeshesSize; j++)
				{
					this.m_SubMeshes[j] = new SubMesh(reader);
				}
				this.m_IndexBuffer = reader.ReadUInt8Array();
				reader.AlignStream();
				this.m_VertexData = new VertexData(reader);
			}
			else
			{
				int verticesSize = reader.ReadInt32();
				this.vertices = new SpriteVertex[verticesSize];
				for (int k = 0; k < verticesSize; k++)
				{
					this.vertices[k] = new SpriteVertex(reader);
				}
				this.indices = reader.ReadUInt16Array();
				reader.AlignStream();
			}
			if (version[0] >= 2018)
			{
				this.m_Bindpose = reader.ReadMatrixArray();
				if (version[0] == 2018 && version[1] < 2)
				{
					int m_SourceSkinSize = reader.ReadInt32();
					for (int l = 0; l < m_SourceSkinSize; l++)
					{
						this.m_SourceSkin[l] = new BoneWeights4(reader);
					}
				}
			}
			this.textureRect = new Rectf(reader);
			this.textureRectOffset = reader.ReadVector2();
			if (version[0] > 5 || (version[0] == 5 && version[1] >= 6))
			{
				this.atlasRectOffset = reader.ReadVector2();
			}
			this.settingsRaw = new SpriteSettings(reader);
			if (version[0] > 4 || (version[0] == 4 && version[1] >= 5))
			{
				this.uvTransform = reader.ReadVector4();
			}
			if (version[0] >= 2017)
			{
				this.downscaleMultiplier = reader.ReadSingle();
			}
		}

		// Token: 0x0400088D RID: 2189
		public PPtr<Texture2D> texture;

		// Token: 0x0400088E RID: 2190
		public PPtr<Texture2D> alphaTexture;

		// Token: 0x0400088F RID: 2191
		public SecondarySpriteTexture[] secondaryTextures;

		// Token: 0x04000890 RID: 2192
		public SubMesh[] m_SubMeshes;

		// Token: 0x04000891 RID: 2193
		public byte[] m_IndexBuffer;

		// Token: 0x04000892 RID: 2194
		public VertexData m_VertexData;

		// Token: 0x04000893 RID: 2195
		public SpriteVertex[] vertices;

		// Token: 0x04000894 RID: 2196
		public ushort[] indices;

		// Token: 0x04000895 RID: 2197
		public Matrix4x4[] m_Bindpose;

		// Token: 0x04000896 RID: 2198
		public BoneWeights4[] m_SourceSkin;

		// Token: 0x04000897 RID: 2199
		public Rectf textureRect;

		// Token: 0x04000898 RID: 2200
		public Vector2 textureRectOffset;

		// Token: 0x04000899 RID: 2201
		public Vector2 atlasRectOffset;

		// Token: 0x0400089A RID: 2202
		public SpriteSettings settingsRaw;

		// Token: 0x0400089B RID: 2203
		public Vector4 uvTransform;

		// Token: 0x0400089C RID: 2204
		public float downscaleMultiplier;
	}
}
