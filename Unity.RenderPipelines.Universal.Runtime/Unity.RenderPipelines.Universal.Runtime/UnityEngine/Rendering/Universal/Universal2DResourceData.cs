using System;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000C2 RID: 194
	internal class Universal2DResourceData : UniversalResourceDataBase
	{
		// Token: 0x060004AD RID: 1197 RVA: 0x000124C8 File Offset: 0x000106C8
		private TextureHandle[][] CheckAndGetTextureHandle(ref TextureHandle[][] handle)
		{
			if (!base.CheckAndWarnAboutAccessibility())
			{
				return new TextureHandle[][] { new TextureHandle[] { TextureHandle.nullHandle } };
			}
			return handle;
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x000124F0 File Offset: 0x000106F0
		private void CheckAndSetTextureHandle(ref TextureHandle[][] handle, TextureHandle[][] newHandle)
		{
			if (!base.CheckAndWarnAboutAccessibility())
			{
				return;
			}
			if (handle == null || handle.Length != newHandle.Length)
			{
				handle = new TextureHandle[newHandle.Length][];
			}
			for (int i = 0; i < newHandle.Length; i++)
			{
				handle[i] = newHandle[i];
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x00012532 File Offset: 0x00010732
		// (set) Token: 0x060004B0 RID: 1200 RVA: 0x00012540 File Offset: 0x00010740
		internal TextureHandle intermediateDepth
		{
			get
			{
				return base.CheckAndGetTextureHandle(ref this._intermediateDepth);
			}
			set
			{
				base.CheckAndSetTextureHandle(ref this._intermediateDepth, value);
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x0001254F File Offset: 0x0001074F
		// (set) Token: 0x060004B2 RID: 1202 RVA: 0x0001255D File Offset: 0x0001075D
		internal TextureHandle[][] lightTextures
		{
			get
			{
				return this.CheckAndGetTextureHandle(ref this._lightTextures);
			}
			set
			{
				this.CheckAndSetTextureHandle(ref this._lightTextures, value);
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x0001256C File Offset: 0x0001076C
		// (set) Token: 0x060004B4 RID: 1204 RVA: 0x0001257A File Offset: 0x0001077A
		internal TextureHandle[] normalsTexture
		{
			get
			{
				return base.CheckAndGetTextureHandle(ref this._cameraNormalsTexture);
			}
			set
			{
				base.CheckAndSetTextureHandle(ref this._cameraNormalsTexture, value);
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x00012589 File Offset: 0x00010789
		// (set) Token: 0x060004B6 RID: 1206 RVA: 0x00012597 File Offset: 0x00010797
		internal TextureHandle shadowsTexture
		{
			get
			{
				return base.CheckAndGetTextureHandle(ref this._shadowsTexture);
			}
			set
			{
				base.CheckAndSetTextureHandle(ref this._shadowsTexture, value);
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x000125A6 File Offset: 0x000107A6
		// (set) Token: 0x060004B8 RID: 1208 RVA: 0x000125B4 File Offset: 0x000107B4
		internal TextureHandle shadowsDepth
		{
			get
			{
				return base.CheckAndGetTextureHandle(ref this._shadowsDepth);
			}
			set
			{
				base.CheckAndSetTextureHandle(ref this._shadowsDepth, value);
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x000125C3 File Offset: 0x000107C3
		// (set) Token: 0x060004BA RID: 1210 RVA: 0x000125D1 File Offset: 0x000107D1
		internal TextureHandle upscaleTexture
		{
			get
			{
				return base.CheckAndGetTextureHandle(ref this._upscaleTexture);
			}
			set
			{
				base.CheckAndSetTextureHandle(ref this._upscaleTexture, value);
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x000125E0 File Offset: 0x000107E0
		// (set) Token: 0x060004BC RID: 1212 RVA: 0x000125EE File Offset: 0x000107EE
		internal TextureHandle cameraSortingLayerTexture
		{
			get
			{
				return base.CheckAndGetTextureHandle(ref this._cameraSortingLayerTexture);
			}
			set
			{
				base.CheckAndSetTextureHandle(ref this._cameraSortingLayerTexture, value);
			}
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00012600 File Offset: 0x00010800
		public override void Reset()
		{
			this._intermediateDepth = TextureHandle.nullHandle;
			this._shadowsTexture = TextureHandle.nullHandle;
			this._shadowsDepth = TextureHandle.nullHandle;
			this._upscaleTexture = TextureHandle.nullHandle;
			this._cameraSortingLayerTexture = TextureHandle.nullHandle;
			for (int i = 0; i < this._cameraNormalsTexture.Length; i++)
			{
				this._cameraNormalsTexture[i] = TextureHandle.nullHandle;
			}
			for (int j = 0; j < this._lightTextures.Length; j++)
			{
				for (int k = 0; k < this._lightTextures[j].Length; k++)
				{
					this._lightTextures[j][k] = TextureHandle.nullHandle;
				}
			}
		}

		// Token: 0x04000404 RID: 1028
		private TextureHandle _intermediateDepth;

		// Token: 0x04000405 RID: 1029
		private TextureHandle[][] _lightTextures = new TextureHandle[0][];

		// Token: 0x04000406 RID: 1030
		private TextureHandle[] _cameraNormalsTexture = new TextureHandle[0];

		// Token: 0x04000407 RID: 1031
		private TextureHandle _shadowsTexture;

		// Token: 0x04000408 RID: 1032
		private TextureHandle _shadowsDepth;

		// Token: 0x04000409 RID: 1033
		private TextureHandle _upscaleTexture;

		// Token: 0x0400040A RID: 1034
		private TextureHandle _cameraSortingLayerTexture;
	}
}
