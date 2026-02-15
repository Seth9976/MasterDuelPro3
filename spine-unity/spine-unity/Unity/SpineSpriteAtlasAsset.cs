using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace Spine.Unity
{
	// Token: 0x0200001C RID: 28
	[CreateAssetMenu(fileName = "New Spine SpriteAtlas Asset", menuName = "Spine/Spine SpriteAtlas Asset")]
	public class SpineSpriteAtlasAsset : AtlasAssetBase
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000040A7 File Offset: 0x000022A7
		public override bool IsLoaded
		{
			get
			{
				return this.atlas != null;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000040B2 File Offset: 0x000022B2
		public override IEnumerable<Material> Materials
		{
			get
			{
				return this.materials;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000040BA File Offset: 0x000022BA
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

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000040CE File Offset: 0x000022CE
		public override Material PrimaryMaterial
		{
			get
			{
				return this.materials[0];
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000040D8 File Offset: 0x000022D8
		public static SpineSpriteAtlasAsset CreateRuntimeInstance(SpriteAtlas spriteAtlasFile, Material[] materials, bool initialize)
		{
			SpineSpriteAtlasAsset atlasAsset = ScriptableObject.CreateInstance<SpineSpriteAtlasAsset>();
			atlasAsset.Reset();
			atlasAsset.spriteAtlasFile = spriteAtlasFile;
			atlasAsset.materials = materials;
			if (initialize)
			{
				atlasAsset.GetAtlas(false);
			}
			return atlasAsset;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003C09 File Offset: 0x00001E09
		private void Reset()
		{
			this.Clear();
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000410B File Offset: 0x0000230B
		public override void Clear()
		{
			this.atlas = null;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004114 File Offset: 0x00002314
		public override Atlas GetAtlas(bool onlyMetaData = false)
		{
			if (this.spriteAtlasFile == null)
			{
				Debug.LogError("SpriteAtlas file not set for SpineSpriteAtlasAsset: " + base.name, this);
				this.Clear();
				return null;
			}
			if (!onlyMetaData && (this.materials == null || this.materials.Length == 0))
			{
				Debug.LogError("Materials not set for SpineSpriteAtlasAsset: " + base.name, this);
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
				this.atlas = this.LoadAtlas(this.spriteAtlasFile);
				atlas = this.atlas;
			}
			catch (Exception ex)
			{
				Debug.LogError(string.Concat(new string[] { "Error analyzing SpriteAtlas for SpineSpriteAtlasAsset: ", base.name, "\n", ex.Message, "\n", ex.StackTrace }), this);
				atlas = null;
			}
			return atlas;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004204 File Offset: 0x00002404
		protected void AssignRegionsFromSavedRegions(Sprite[] sprites, Atlas usedAtlas)
		{
			if (this.savedRegions == null || this.savedRegions.Length != sprites.Length)
			{
				return;
			}
			int i = 0;
			foreach (AtlasRegion region in usedAtlas)
			{
				SpineSpriteAtlasAsset.SavedRegionInfo savedRegion = this.savedRegions[i];
				AtlasPage page = region.page;
				region.degrees = ((savedRegion.packingRotation == SpritePackingRotation.None) ? 0 : 90);
				float x = savedRegion.x;
				float y = savedRegion.y;
				float width = savedRegion.width;
				float height = savedRegion.height;
				region.u = x / (float)page.width;
				region.v = y / (float)page.height;
				if (region.degrees == 90)
				{
					region.u2 = (x + height) / (float)page.width;
					region.v2 = (y + width) / (float)page.height;
				}
				else
				{
					region.u2 = (x + width) / (float)page.width;
					region.v2 = (y + height) / (float)page.height;
				}
				region.x = (int)x;
				region.y = (int)y;
				region.width = Math.Abs((int)width);
				region.height = Math.Abs((int)height);
				float temp = region.v;
				region.v = region.v2;
				region.v2 = temp;
				region.originalWidth = (int)width;
				region.originalHeight = (int)height;
				region.offsetX = 0f;
				region.offsetY = 0f;
				i++;
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000043A8 File Offset: 0x000025A8
		private Atlas LoadAtlas(SpriteAtlas spriteAtlas)
		{
			List<AtlasPage> pages = new List<AtlasPage>();
			List<AtlasRegion> regions = new List<AtlasRegion>();
			Sprite[] sprites = new Sprite[spriteAtlas.spriteCount];
			spriteAtlas.GetSprites(sprites);
			if (sprites.Length == 0)
			{
				return new Atlas(pages, regions);
			}
			Texture2D texture = SpineSpriteAtlasAsset.AccessPackedTexture(sprites);
			Material material = this.materials[0];
			material.mainTexture = texture;
			AtlasPage page = new AtlasPage();
			page.name = spriteAtlas.name;
			page.width = texture.width;
			page.height = texture.height;
			page.format = Format.RGBA8888;
			page.minFilter = TextureFilter.Linear;
			page.magFilter = TextureFilter.Linear;
			page.uWrap = TextureWrap.ClampToEdge;
			page.vWrap = TextureWrap.ClampToEdge;
			page.rendererObject = material;
			pages.Add(page);
			sprites = SpineSpriteAtlasAsset.AccessPackedSprites(spriteAtlas);
			for (int i = 0; i < sprites.Length; i++)
			{
				Sprite sprite = sprites[i];
				regions.Add(new AtlasRegion
				{
					name = sprite.name.Replace("(Clone)", ""),
					page = page,
					degrees = ((sprite.packingRotation == SpritePackingRotation.None) ? 0 : 90),
					u2 = 1f,
					v2 = 1f,
					width = page.width,
					height = page.height,
					originalWidth = page.width,
					originalHeight = page.height,
					index = i
				});
			}
			Atlas atlas = new Atlas(pages, regions);
			this.AssignRegionsFromSavedRegions(sprites, atlas);
			return atlas;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000453F File Offset: 0x0000273F
		public static Texture2D AccessPackedTexture(Sprite[] sprites)
		{
			return sprites[0].texture;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000454C File Offset: 0x0000274C
		public static Sprite[] AccessPackedSprites(SpriteAtlas spriteAtlas)
		{
			Sprite[] sprites = null;
			if (sprites == null)
			{
				sprites = new Sprite[spriteAtlas.spriteCount];
				spriteAtlas.GetSprites(sprites);
				if (sprites.Length == 0)
				{
					return null;
				}
			}
			return sprites;
		}

		// Token: 0x0400004A RID: 74
		public SpriteAtlas spriteAtlasFile;

		// Token: 0x0400004B RID: 75
		public Material[] materials;

		// Token: 0x0400004C RID: 76
		protected Atlas atlas;

		// Token: 0x0400004D RID: 77
		public bool updateRegionsInPlayMode;

		// Token: 0x0400004E RID: 78
		[SerializeField]
		protected SpineSpriteAtlasAsset.SavedRegionInfo[] savedRegions;

		// Token: 0x0200001D RID: 29
		[Serializable]
		protected class SavedRegionInfo
		{
			// Token: 0x0400004F RID: 79
			public float x;

			// Token: 0x04000050 RID: 80
			public float y;

			// Token: 0x04000051 RID: 81
			public float width;

			// Token: 0x04000052 RID: 82
			public float height;

			// Token: 0x04000053 RID: 83
			public SpritePackingRotation packingRotation;
		}
	}
}
