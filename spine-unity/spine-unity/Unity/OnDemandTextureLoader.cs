using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x02000010 RID: 16
	public abstract class OnDemandTextureLoader : ScriptableObject
	{
		// Token: 0x0600003A RID: 58
		public abstract string GetPlaceholderTextureName(string originalTextureName);

		// Token: 0x0600003B RID: 59
		public abstract bool AssignPlaceholderTextures(out IEnumerable<Material> modifiedMaterials);

		// Token: 0x0600003C RID: 60
		public abstract bool HasPlaceholderTexturesAssigned(out List<Material> placeholderMaterials);

		// Token: 0x0600003D RID: 61 RVA: 0x0000318C File Offset: 0x0000138C
		public virtual bool HasNullMainTexturesAssigned(out List<Material> nullTextureMaterials)
		{
			nullTextureMaterials = null;
			if (!this.atlasAsset)
			{
				return false;
			}
			bool anyNullTexture = false;
			foreach (Material material in this.atlasAsset.Materials)
			{
				if (material.mainTexture == null)
				{
					anyNullTexture = true;
					if (nullTextureMaterials == null)
					{
						nullTextureMaterials = new List<Material>();
					}
					nullTextureMaterials.Add(material);
				}
			}
			return anyNullTexture;
		}

		// Token: 0x0600003E RID: 62
		public abstract bool AssignTargetTextures(out IEnumerable<Material> modifiedMaterials);

		// Token: 0x0600003F RID: 63
		public abstract void BeginCustomTextureLoading();

		// Token: 0x06000040 RID: 64
		public abstract void EndCustomTextureLoading();

		// Token: 0x06000041 RID: 65
		public abstract bool HasPlaceholderAssigned(Material material);

		// Token: 0x06000042 RID: 66
		public abstract void RequestLoadMaterialTextures(Material material, ref Material overrideMaterial);

		// Token: 0x06000043 RID: 67
		public abstract void RequestLoadTexture(Texture placeholderTexture, ref Texture replacementTexture, Action<Texture> onTextureLoaded = null);

		// Token: 0x06000044 RID: 68
		public abstract void Clear(bool clearAtlasAsset = false);

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000045 RID: 69 RVA: 0x00003210 File Offset: 0x00001410
		// (remove) Token: 0x06000046 RID: 70 RVA: 0x00003248 File Offset: 0x00001448
		protected event OnDemandTextureLoader.TextureLoadDelegate onTextureRequested;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000047 RID: 71 RVA: 0x00003280 File Offset: 0x00001480
		// (remove) Token: 0x06000048 RID: 72 RVA: 0x000032B8 File Offset: 0x000014B8
		protected event OnDemandTextureLoader.TextureLoadDelegate onTextureLoaded;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000049 RID: 73 RVA: 0x000032F0 File Offset: 0x000014F0
		// (remove) Token: 0x0600004A RID: 74 RVA: 0x00003328 File Offset: 0x00001528
		protected event OnDemandTextureLoader.TextureLoadDelegate onTextureLoadFailed;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600004B RID: 75 RVA: 0x00003360 File Offset: 0x00001560
		// (remove) Token: 0x0600004C RID: 76 RVA: 0x00003398 File Offset: 0x00001598
		protected event OnDemandTextureLoader.TextureLoadDelegate onTextureUnloaded;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600004D RID: 77 RVA: 0x000033CD File Offset: 0x000015CD
		// (remove) Token: 0x0600004E RID: 78 RVA: 0x000033D6 File Offset: 0x000015D6
		public event OnDemandTextureLoader.TextureLoadDelegate TextureRequested
		{
			add
			{
				this.onTextureRequested += value;
			}
			remove
			{
				this.onTextureRequested -= value;
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600004F RID: 79 RVA: 0x000033DF File Offset: 0x000015DF
		// (remove) Token: 0x06000050 RID: 80 RVA: 0x000033E8 File Offset: 0x000015E8
		public event OnDemandTextureLoader.TextureLoadDelegate TextureLoaded
		{
			add
			{
				this.onTextureLoaded += value;
			}
			remove
			{
				this.onTextureLoaded -= value;
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000051 RID: 81 RVA: 0x000033F1 File Offset: 0x000015F1
		// (remove) Token: 0x06000052 RID: 82 RVA: 0x000033FA File Offset: 0x000015FA
		public event OnDemandTextureLoader.TextureLoadDelegate TextureLoadFailed
		{
			add
			{
				this.onTextureLoadFailed += value;
			}
			remove
			{
				this.onTextureLoadFailed -= value;
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000053 RID: 83 RVA: 0x00003403 File Offset: 0x00001603
		// (remove) Token: 0x06000054 RID: 84 RVA: 0x0000340C File Offset: 0x0000160C
		public event OnDemandTextureLoader.TextureLoadDelegate TextureUnloaded
		{
			add
			{
				this.onTextureUnloaded += value;
			}
			remove
			{
				this.onTextureUnloaded -= value;
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003415 File Offset: 0x00001615
		protected void OnTextureRequested(Material material, int textureIndex)
		{
			if (this.onTextureRequested != null)
			{
				this.onTextureRequested(this, material, textureIndex);
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000342D File Offset: 0x0000162D
		protected void OnTextureLoaded(Material material, int textureIndex)
		{
			if (this.onTextureLoaded != null)
			{
				this.onTextureLoaded(this, material, textureIndex);
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003445 File Offset: 0x00001645
		protected void OnTextureLoadFailed(Material material, int textureIndex)
		{
			if (this.onTextureLoadFailed != null)
			{
				this.onTextureLoadFailed(this, material, textureIndex);
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000345D File Offset: 0x0000165D
		protected void OnTextureUnloaded(Material material, int textureIndex)
		{
			if (this.onTextureUnloaded != null)
			{
				this.onTextureUnloaded(this, material, textureIndex);
			}
		}

		// Token: 0x04000029 RID: 41
		public AtlasAssetBase atlasAsset;

		// Token: 0x02000011 RID: 17
		// (Invoke) Token: 0x0600005B RID: 91
		public delegate void TextureLoadDelegate(OnDemandTextureLoader loader, Material material, int textureIndex);
	}
}
