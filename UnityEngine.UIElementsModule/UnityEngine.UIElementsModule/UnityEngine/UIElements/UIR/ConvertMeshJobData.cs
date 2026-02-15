using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200052B RID: 1323
	internal struct ConvertMeshJobData
	{
		// Token: 0x04001193 RID: 4499
		public IntPtr vertSrc;

		// Token: 0x04001194 RID: 4500
		public IntPtr vertDst;

		// Token: 0x04001195 RID: 4501
		public int vertCount;

		// Token: 0x04001196 RID: 4502
		public Matrix4x4 transform;

		// Token: 0x04001197 RID: 4503
		public Color32 xformClipPages;

		// Token: 0x04001198 RID: 4504
		public Color32 ids;

		// Token: 0x04001199 RID: 4505
		public Color32 addFlags;

		// Token: 0x0400119A RID: 4506
		public Color32 opacityPage;

		// Token: 0x0400119B RID: 4507
		public Color32 textCoreSettingsPage;

		// Token: 0x0400119C RID: 4508
		public int usesTextCoreSettings;

		// Token: 0x0400119D RID: 4509
		public float textureId;

		// Token: 0x0400119E RID: 4510
		public int gradientSettingsIndexOffset;

		// Token: 0x0400119F RID: 4511
		public IntPtr indexSrc;

		// Token: 0x040011A0 RID: 4512
		public IntPtr indexDst;

		// Token: 0x040011A1 RID: 4513
		public int indexCount;

		// Token: 0x040011A2 RID: 4514
		public int indexOffset;

		// Token: 0x040011A3 RID: 4515
		public int flipIndices;

		// Token: 0x040011A4 RID: 4516
		public int forceZ;

		// Token: 0x040011A5 RID: 4517
		public float positionZ;

		// Token: 0x040011A6 RID: 4518
		public int remapUVs;

		// Token: 0x040011A7 RID: 4519
		public Rect atlasRect;
	}
}
