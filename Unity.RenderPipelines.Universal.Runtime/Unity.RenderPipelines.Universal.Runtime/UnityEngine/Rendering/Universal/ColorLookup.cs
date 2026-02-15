using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000E9 RID: 233
	[VolumeComponentMenu("Post-processing/Color Lookup")]
	[SupportedOnRenderPipeline(typeof(UniversalRenderPipelineAsset))]
	[Serializable]
	public sealed class ColorLookup : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x060005E1 RID: 1505 RVA: 0x0001632F File Offset: 0x0001452F
		public bool IsActive()
		{
			return this.contribution.value > 0f && this.ValidateLUT();
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x000039B4 File Offset: 0x00001BB4
		[Obsolete("Unused #from(2023.1)", false)]
		public bool IsTileCompatible()
		{
			return true;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0001634C File Offset: 0x0001454C
		public bool ValidateLUT()
		{
			UniversalRenderPipelineAsset asset = UniversalRenderPipeline.asset;
			if (asset == null || this.texture.value == null)
			{
				return false;
			}
			int lutSize = asset.colorGradingLutSize;
			if (this.texture.value.height != lutSize)
			{
				return false;
			}
			bool valid = false;
			Texture value = this.texture.value;
			Texture2D t = value as Texture2D;
			if (t == null)
			{
				RenderTexture rt = value as RenderTexture;
				if (rt != null)
				{
					valid |= rt.dimension == TextureDimension.Tex2D && rt.width == lutSize * lutSize && !rt.sRGB;
				}
			}
			else
			{
				valid |= t.width == lutSize * lutSize && !GraphicsFormatUtility.IsSRGBFormat(t.graphicsFormat);
			}
			return valid;
		}

		// Token: 0x04000513 RID: 1299
		[Tooltip("A 2D Lookup Texture (LUT) to use for color grading.")]
		public TextureParameter texture = new TextureParameter(null, false);

		// Token: 0x04000514 RID: 1300
		[Tooltip("How much of the lookup texture will contribute to the color grading effect.")]
		public ClampedFloatParameter contribution = new ClampedFloatParameter(0f, 0f, 1f, false);
	}
}
