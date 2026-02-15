using System;

namespace TMPro
{
	// Token: 0x02000038 RID: 56
	[Serializable]
	public struct FontAssetCreationSettings
	{
		// Token: 0x060001A2 RID: 418 RVA: 0x00008E30 File Offset: 0x00007030
		internal FontAssetCreationSettings(string sourceFontFileGUID, int pointSize, int pointSizeSamplingMode, int padding, int packingMode, int atlasWidth, int atlasHeight, int characterSelectionMode, string characterSet, int renderMode)
		{
			this.sourceFontFileName = string.Empty;
			this.sourceFontFileGUID = sourceFontFileGUID;
			this.faceIndex = 0;
			this.pointSize = pointSize;
			this.pointSizeSamplingMode = pointSizeSamplingMode;
			this.padding = padding;
			this.paddingMode = 2;
			this.packingMode = packingMode;
			this.atlasWidth = atlasWidth;
			this.atlasHeight = atlasHeight;
			this.characterSequence = characterSet;
			this.characterSetSelectionMode = characterSelectionMode;
			this.renderMode = renderMode;
			this.referencedFontAssetGUID = string.Empty;
			this.referencedTextAssetGUID = string.Empty;
			this.fontStyle = 0;
			this.fontStyleModifier = 0f;
			this.includeFontFeatures = false;
		}

		// Token: 0x04000139 RID: 313
		public string sourceFontFileName;

		// Token: 0x0400013A RID: 314
		public string sourceFontFileGUID;

		// Token: 0x0400013B RID: 315
		public int faceIndex;

		// Token: 0x0400013C RID: 316
		public int pointSizeSamplingMode;

		// Token: 0x0400013D RID: 317
		public int pointSize;

		// Token: 0x0400013E RID: 318
		public int padding;

		// Token: 0x0400013F RID: 319
		public int paddingMode;

		// Token: 0x04000140 RID: 320
		public int packingMode;

		// Token: 0x04000141 RID: 321
		public int atlasWidth;

		// Token: 0x04000142 RID: 322
		public int atlasHeight;

		// Token: 0x04000143 RID: 323
		public int characterSetSelectionMode;

		// Token: 0x04000144 RID: 324
		public string characterSequence;

		// Token: 0x04000145 RID: 325
		public string referencedFontAssetGUID;

		// Token: 0x04000146 RID: 326
		public string referencedTextAssetGUID;

		// Token: 0x04000147 RID: 327
		public int fontStyle;

		// Token: 0x04000148 RID: 328
		public float fontStyleModifier;

		// Token: 0x04000149 RID: 329
		public int renderMode;

		// Token: 0x0400014A RID: 330
		public bool includeFontFeatures;
	}
}
