using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x02000007 RID: 7
	public abstract class AtlasAssetBase : ScriptableObject
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000011 RID: 17
		public abstract Material PrimaryMaterial { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000012 RID: 18
		public abstract IEnumerable<Material> Materials { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000013 RID: 19
		public abstract int MaterialCount { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000014 RID: 20
		public abstract bool IsLoaded { get; }

		// Token: 0x06000015 RID: 21
		public abstract void Clear();

		// Token: 0x06000016 RID: 22
		public abstract Atlas GetAtlas(bool onlyMetaData = false);

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002840 File Offset: 0x00000A40
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002848 File Offset: 0x00000A48
		public virtual AtlasAssetBase.LoadingMode TextureLoadingMode
		{
			get
			{
				return this.textureLoadingMode;
			}
			set
			{
				this.textureLoadingMode = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002851 File Offset: 0x00000A51
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002859 File Offset: 0x00000A59
		public OnDemandTextureLoader OnDemandTextureLoader
		{
			get
			{
				return this.onDemandTextureLoader;
			}
			set
			{
				this.onDemandTextureLoader = value;
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002862 File Offset: 0x00000A62
		public virtual void BeginCustomTextureLoading()
		{
			if (this.onDemandTextureLoader)
			{
				this.onDemandTextureLoader.BeginCustomTextureLoading();
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000287C File Offset: 0x00000A7C
		public virtual void EndCustomTextureLoading()
		{
			if (this.onDemandTextureLoader)
			{
				this.onDemandTextureLoader.EndCustomTextureLoading();
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002896 File Offset: 0x00000A96
		public virtual void RequireTexturesLoaded(Material material, ref Material overrideMaterial)
		{
			if (this.onDemandTextureLoader)
			{
				this.onDemandTextureLoader.RequestLoadMaterialTextures(material, ref overrideMaterial);
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000028B2 File Offset: 0x00000AB2
		public virtual void RequireTextureLoaded(Texture placeholderTexture, ref Texture replacementTexture, Action<Texture> onTextureLoaded)
		{
			if (this.onDemandTextureLoader)
			{
				this.onDemandTextureLoader.RequestLoadTexture(placeholderTexture, ref replacementTexture, onTextureLoaded);
			}
		}

		// Token: 0x04000010 RID: 16
		[SerializeField]
		protected AtlasAssetBase.LoadingMode textureLoadingMode;

		// Token: 0x04000011 RID: 17
		[SerializeField]
		protected OnDemandTextureLoader onDemandTextureLoader;

		// Token: 0x02000008 RID: 8
		public enum LoadingMode
		{
			// Token: 0x04000013 RID: 19
			Normal,
			// Token: 0x04000014 RID: 20
			OnDemand
		}
	}
}
