using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x02000041 RID: 65
	[ExecuteAlways]
	[HelpURL("http://esotericsoftware.com/spine-unity#SkeletonGraphicCustomMaterials")]
	public class SkeletonGraphicCustomMaterials : MonoBehaviour
	{
		// Token: 0x06000278 RID: 632 RVA: 0x0000D5B0 File Offset: 0x0000B7B0
		private void SetCustomMaterialOverrides()
		{
			if (this.skeletonGraphic == null)
			{
				Debug.LogError("skeletonGraphic == null");
				return;
			}
			for (int i = 0; i < this.customMaterialOverrides.Count; i++)
			{
				SkeletonGraphicCustomMaterials.AtlasMaterialOverride atlasMaterialOverride = this.customMaterialOverrides[i];
				if (atlasMaterialOverride.overrideEnabled)
				{
					this.skeletonGraphic.CustomMaterialOverride[atlasMaterialOverride.originalTexture] = atlasMaterialOverride.replacementMaterial;
				}
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000D620 File Offset: 0x0000B820
		private void RemoveCustomMaterialOverrides()
		{
			if (this.skeletonGraphic == null)
			{
				Debug.LogError("skeletonGraphic == null");
				return;
			}
			for (int i = 0; i < this.customMaterialOverrides.Count; i++)
			{
				SkeletonGraphicCustomMaterials.AtlasMaterialOverride atlasMaterialOverride = this.customMaterialOverrides[i];
				Material currentMaterial;
				if (this.skeletonGraphic.CustomMaterialOverride.TryGetValue(atlasMaterialOverride.originalTexture, out currentMaterial) && !(currentMaterial != atlasMaterialOverride.replacementMaterial))
				{
					this.skeletonGraphic.CustomMaterialOverride.Remove(atlasMaterialOverride.originalTexture);
				}
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000D6A8 File Offset: 0x0000B8A8
		private void SetCustomTextureOverrides()
		{
			if (this.skeletonGraphic == null)
			{
				Debug.LogError("skeletonGraphic == null");
				return;
			}
			for (int i = 0; i < this.customTextureOverrides.Count; i++)
			{
				SkeletonGraphicCustomMaterials.AtlasTextureOverride atlasTextureOverride = this.customTextureOverrides[i];
				if (atlasTextureOverride.overrideEnabled)
				{
					this.skeletonGraphic.CustomTextureOverride[atlasTextureOverride.originalTexture] = atlasTextureOverride.replacementTexture;
				}
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000D718 File Offset: 0x0000B918
		private void RemoveCustomTextureOverrides()
		{
			if (this.skeletonGraphic == null)
			{
				Debug.LogError("skeletonGraphic == null");
				return;
			}
			for (int i = 0; i < this.customTextureOverrides.Count; i++)
			{
				SkeletonGraphicCustomMaterials.AtlasTextureOverride atlasTextureOverride = this.customTextureOverrides[i];
				Texture currentTexture;
				if (this.skeletonGraphic.CustomTextureOverride.TryGetValue(atlasTextureOverride.originalTexture, out currentTexture) && !(currentTexture != atlasTextureOverride.replacementTexture))
				{
					this.skeletonGraphic.CustomTextureOverride.Remove(atlasTextureOverride.originalTexture);
				}
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000D7A0 File Offset: 0x0000B9A0
		private void OnEnable()
		{
			if (this.skeletonGraphic == null)
			{
				this.skeletonGraphic = base.GetComponent<SkeletonGraphic>();
			}
			if (this.skeletonGraphic == null)
			{
				Debug.LogError("skeletonGraphic == null");
				return;
			}
			this.skeletonGraphic.Initialize(false);
			this.SetCustomMaterialOverrides();
			this.SetCustomTextureOverrides();
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000D7F8 File Offset: 0x0000B9F8
		private void OnDisable()
		{
			if (this.skeletonGraphic == null)
			{
				Debug.LogError("skeletonGraphic == null");
				return;
			}
			this.RemoveCustomMaterialOverrides();
			this.RemoveCustomTextureOverrides();
		}

		// Token: 0x0400017F RID: 383
		public SkeletonGraphic skeletonGraphic;

		// Token: 0x04000180 RID: 384
		[SerializeField]
		protected List<SkeletonGraphicCustomMaterials.AtlasMaterialOverride> customMaterialOverrides = new List<SkeletonGraphicCustomMaterials.AtlasMaterialOverride>();

		// Token: 0x04000181 RID: 385
		[SerializeField]
		protected List<SkeletonGraphicCustomMaterials.AtlasTextureOverride> customTextureOverrides = new List<SkeletonGraphicCustomMaterials.AtlasTextureOverride>();

		// Token: 0x02000042 RID: 66
		[Serializable]
		public struct AtlasMaterialOverride : IEquatable<SkeletonGraphicCustomMaterials.AtlasMaterialOverride>
		{
			// Token: 0x0600027F RID: 639 RVA: 0x0000D83D File Offset: 0x0000BA3D
			public bool Equals(SkeletonGraphicCustomMaterials.AtlasMaterialOverride other)
			{
				return this.overrideEnabled == other.overrideEnabled && this.originalTexture == other.originalTexture && this.replacementMaterial == other.replacementMaterial;
			}

			// Token: 0x04000182 RID: 386
			public bool overrideEnabled;

			// Token: 0x04000183 RID: 387
			public Texture originalTexture;

			// Token: 0x04000184 RID: 388
			public Material replacementMaterial;
		}

		// Token: 0x02000043 RID: 67
		[Serializable]
		public struct AtlasTextureOverride : IEquatable<SkeletonGraphicCustomMaterials.AtlasTextureOverride>
		{
			// Token: 0x06000280 RID: 640 RVA: 0x0000D873 File Offset: 0x0000BA73
			public bool Equals(SkeletonGraphicCustomMaterials.AtlasTextureOverride other)
			{
				return this.overrideEnabled == other.overrideEnabled && this.originalTexture == other.originalTexture && this.replacementTexture == other.replacementTexture;
			}

			// Token: 0x04000185 RID: 389
			public bool overrideEnabled;

			// Token: 0x04000186 RID: 390
			public Texture originalTexture;

			// Token: 0x04000187 RID: 391
			public Texture replacementTexture;
		}
	}
}
