using System;
using UnityEngine.Internal;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200002A RID: 42
	[ExcludeFromDocs]
	[Serializable]
	public struct FontAssetCreationEditorSettings
	{
		// Token: 0x040000DE RID: 222
		public string sourceFontFileGUID;

		// Token: 0x040000DF RID: 223
		public int faceIndex;

		// Token: 0x040000E0 RID: 224
		public int pointSizeSamplingMode;

		// Token: 0x040000E1 RID: 225
		public float pointSize;

		// Token: 0x040000E2 RID: 226
		public int padding;

		// Token: 0x040000E3 RID: 227
		public int paddingMode;

		// Token: 0x040000E4 RID: 228
		public int packingMode;

		// Token: 0x040000E5 RID: 229
		public int atlasWidth;

		// Token: 0x040000E6 RID: 230
		public int atlasHeight;

		// Token: 0x040000E7 RID: 231
		public int characterSetSelectionMode;

		// Token: 0x040000E8 RID: 232
		public string characterSequence;

		// Token: 0x040000E9 RID: 233
		public string referencedFontAssetGUID;

		// Token: 0x040000EA RID: 234
		public string referencedTextAssetGUID;

		// Token: 0x040000EB RID: 235
		public int fontStyle;

		// Token: 0x040000EC RID: 236
		public float fontStyleModifier;

		// Token: 0x040000ED RID: 237
		public int renderMode;

		// Token: 0x040000EE RID: 238
		public bool includeFontFeatures;
	}
}
