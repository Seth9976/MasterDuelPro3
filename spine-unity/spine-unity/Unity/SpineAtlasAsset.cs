using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x02000019 RID: 25
	[CreateAssetMenu(fileName = "New Spine Atlas Asset", menuName = "Spine/Spine Atlas Asset")]
	public class SpineAtlasAsset : AtlasAssetBase
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003A45 File Offset: 0x00001C45
		public override bool IsLoaded
		{
			get
			{
				return this.atlas != null;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003A50 File Offset: 0x00001C50
		public override IEnumerable<Material> Materials
		{
			get
			{
				return this.materials;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003A58 File Offset: 0x00001C58
		public override int MaterialCount
		{
			get
			{
				if (this.materials != null)
				{
					return this.materials.Length;
				}
				return 0;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003A6C File Offset: 0x00001C6C
		public override Material PrimaryMaterial
		{
			get
			{
				return this.materials[0];
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003A78 File Offset: 0x00001C78
		public static SpineAtlasAsset CreateRuntimeInstance(TextAsset atlasText, Material[] materials, bool initialize, Func<SpineAtlasAsset, TextureLoader> newCustomTextureLoader = null)
		{
			SpineAtlasAsset atlasAsset = ScriptableObject.CreateInstance<SpineAtlasAsset>();
			atlasAsset.Reset();
			atlasAsset.atlasFile = atlasText;
			atlasAsset.materials = materials;
			if (newCustomTextureLoader != null)
			{
				atlasAsset.customTextureLoader = newCustomTextureLoader(atlasAsset);
			}
			if (initialize)
			{
				atlasAsset.GetAtlas(false);
			}
			return atlasAsset;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003ABC File Offset: 0x00001CBC
		public static SpineAtlasAsset CreateRuntimeInstance(TextAsset atlasText, Texture2D[] textures, Material materialPropertySource, bool initialize, Func<SpineAtlasAsset, TextureLoader> newCustomTextureLoader = null, bool renameMaterial = false)
		{
			string[] atlasLines = atlasText.text.Replace("\r", "").Split('\n', StringSplitOptions.None);
			List<string> pages = new List<string>();
			for (int i = 0; i < atlasLines.Length - 1; i++)
			{
				string line = atlasLines[i].Trim();
				if (line.EndsWith(".png"))
				{
					pages.Add(line.Replace(".png", ""));
				}
			}
			Material[] materials = new Material[pages.Count];
			int j = 0;
			int k = pages.Count;
			while (j < k)
			{
				Material mat = null;
				string pageName = pages[j];
				int l = 0;
				int m = textures.Length;
				while (l < m)
				{
					if (string.Equals(pageName, textures[l].name, StringComparison.OrdinalIgnoreCase))
					{
						mat = new Material(materialPropertySource);
						mat.mainTexture = textures[l];
						if (renameMaterial)
						{
							mat.name = pageName;
							break;
						}
						break;
					}
					else
					{
						l++;
					}
				}
				if (!(mat != null))
				{
					throw new ArgumentException("Could not find matching atlas page in the texture array.");
				}
				materials[j] = mat;
				j++;
			}
			return SpineAtlasAsset.CreateRuntimeInstance(atlasText, materials, initialize, newCustomTextureLoader);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003BD4 File Offset: 0x00001DD4
		public static SpineAtlasAsset CreateRuntimeInstance(TextAsset atlasText, Texture2D[] textures, Shader shader, bool initialize, Func<SpineAtlasAsset, TextureLoader> newCustomTextureLoader = null)
		{
			if (shader == null)
			{
				shader = Shader.Find("Spine/Skeleton");
			}
			Material materialProperySource = new Material(shader);
			return SpineAtlasAsset.CreateRuntimeInstance(atlasText, textures, materialProperySource, initialize, newCustomTextureLoader, false);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003C09 File Offset: 0x00001E09
		private void Reset()
		{
			this.Clear();
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003C11 File Offset: 0x00001E11
		public override void Clear()
		{
			this.atlas = null;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003C1C File Offset: 0x00001E1C
		public override Atlas GetAtlas(bool onlyMetaData = false)
		{
			if (this.atlasFile == null)
			{
				Debug.LogError("Atlas file not set for atlas asset: " + base.name, this);
				this.Clear();
				return null;
			}
			if (!onlyMetaData && (this.materials == null || this.materials.Length == 0))
			{
				Debug.LogError("Materials not set for atlas asset: " + base.name, this);
				this.Clear();
				return null;
			}
			if (this.atlas != null)
			{
				return this.atlas;
			}
			Atlas atlas;
			try
			{
				TextureLoader loader;
				if (!onlyMetaData)
				{
					TextureLoader textureLoader;
					if (this.customTextureLoader != null)
					{
						textureLoader = this.customTextureLoader;
					}
					else
					{
						TextureLoader textureLoader2 = new MaterialsTextureLoader(this);
						textureLoader = textureLoader2;
					}
					loader = textureLoader;
				}
				else
				{
					loader = new NoOpTextureLoader();
				}
				this.atlas = new Atlas(new StringReader(this.atlasFile.text), "", loader);
				this.atlas.FlipV();
				atlas = this.atlas;
			}
			catch (Exception ex)
			{
				Debug.LogError(string.Concat(new string[] { "Error reading atlas file for atlas asset: ", base.name, "\n", ex.Message, "\n", ex.StackTrace }), this);
				atlas = null;
			}
			return atlas;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003D4C File Offset: 0x00001F4C
		public Mesh GenerateMesh(string name, Mesh mesh, out Material material, float scale = 0.01f)
		{
			AtlasRegion region = this.atlas.FindRegion(name);
			material = null;
			if (region != null)
			{
				if (mesh == null)
				{
					mesh = new Mesh();
					mesh.name = name;
				}
				Vector3[] verts = new Vector3[4];
				Vector2[] uvs = new Vector2[4];
				Color[] colors = new Color[]
				{
					Color.white,
					Color.white,
					Color.white,
					Color.white
				};
				int[] triangles = new int[] { 0, 1, 2, 2, 3, 0 };
				float left = (float)region.width / -2f;
				float right = left * -1f;
				float top = (float)region.height / 2f;
				float bottom = top * -1f;
				verts[0] = new Vector3(left, bottom, 0f) * scale;
				verts[1] = new Vector3(left, top, 0f) * scale;
				verts[2] = new Vector3(right, top, 0f) * scale;
				verts[3] = new Vector3(right, bottom, 0f) * scale;
				float u = region.u;
				float v = region.v;
				float u2 = region.u2;
				float v2 = region.v2;
				if (region.degrees == 90)
				{
					uvs[0] = new Vector2(u2, v2);
					uvs[1] = new Vector2(u, v2);
					uvs[2] = new Vector2(u, v);
					uvs[3] = new Vector2(u2, v);
				}
				else
				{
					uvs[0] = new Vector2(u, v2);
					uvs[1] = new Vector2(u, v);
					uvs[2] = new Vector2(u2, v);
					uvs[3] = new Vector2(u2, v2);
				}
				mesh.triangles = new int[0];
				mesh.vertices = verts;
				mesh.uv = uvs;
				mesh.colors = colors;
				mesh.triangles = triangles;
				mesh.RecalculateNormals();
				mesh.RecalculateBounds();
				material = (Material)region.page.rendererObject;
			}
			else
			{
				mesh = null;
			}
			return mesh;
		}

		// Token: 0x04000045 RID: 69
		public TextAsset atlasFile;

		// Token: 0x04000046 RID: 70
		public Material[] materials;

		// Token: 0x04000047 RID: 71
		public TextureLoader customTextureLoader;

		// Token: 0x04000048 RID: 72
		protected Atlas atlas;
	}
}
