using System;
using System.IO;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x0200001B RID: 27
	public class MaterialsTextureLoader : TextureLoader
	{
		// Token: 0x06000088 RID: 136 RVA: 0x00003F83 File Offset: 0x00002183
		public MaterialsTextureLoader(SpineAtlasAsset atlasAsset)
		{
			this.atlasAsset = atlasAsset;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003F94 File Offset: 0x00002194
		public void Load(AtlasPage page, string path)
		{
			string name = Path.GetFileNameWithoutExtension(path);
			Material material = null;
			foreach (Material other in this.atlasAsset.materials)
			{
				if (other.mainTexture == null)
				{
					Debug.LogError("Material is missing texture: " + other.name, other);
					return;
				}
				string textureName = other.mainTexture.name;
				if (textureName == name || (this.atlasAsset.OnDemandTextureLoader != null && textureName == this.atlasAsset.OnDemandTextureLoader.GetPlaceholderTextureName(name)))
				{
					material = other;
					break;
				}
			}
			if (material == null)
			{
				Debug.LogError("Material with texture name \"" + name + "\" not found for atlas asset: " + this.atlasAsset.name, this.atlasAsset);
				return;
			}
			page.rendererObject = material;
			if (page.width == 0 || page.height == 0)
			{
				page.width = material.mainTexture.width;
				page.height = material.mainTexture.height;
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003F81 File Offset: 0x00002181
		public void Unload(object texture)
		{
		}

		// Token: 0x04000049 RID: 73
		private SpineAtlasAsset atlasAsset;
	}
}
