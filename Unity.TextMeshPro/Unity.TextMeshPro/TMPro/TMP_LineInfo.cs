using System;

namespace TMPro
{
	// Token: 0x0200005B RID: 91
	public struct TMP_LineInfo
	{
		// Token: 0x04000215 RID: 533
		internal int controlCharacterCount;

		// Token: 0x04000216 RID: 534
		public int characterCount;

		// Token: 0x04000217 RID: 535
		public int visibleCharacterCount;

		// Token: 0x04000218 RID: 536
		public int spaceCount;

		// Token: 0x04000219 RID: 537
		public int visibleSpaceCount;

		// Token: 0x0400021A RID: 538
		public int wordCount;

		// Token: 0x0400021B RID: 539
		public int firstCharacterIndex;

		// Token: 0x0400021C RID: 540
		public int firstVisibleCharacterIndex;

		// Token: 0x0400021D RID: 541
		public int lastCharacterIndex;

		// Token: 0x0400021E RID: 542
		public int lastVisibleCharacterIndex;

		// Token: 0x0400021F RID: 543
		public float length;

		// Token: 0x04000220 RID: 544
		public float lineHeight;

		// Token: 0x04000221 RID: 545
		public float ascender;

		// Token: 0x04000222 RID: 546
		public float baseline;

		// Token: 0x04000223 RID: 547
		public float descender;

		// Token: 0x04000224 RID: 548
		public float maxAdvance;

		// Token: 0x04000225 RID: 549
		public float width;

		// Token: 0x04000226 RID: 550
		public float marginLeft;

		// Token: 0x04000227 RID: 551
		public float marginRight;

		// Token: 0x04000228 RID: 552
		public HorizontalAlignmentOptions alignment;

		// Token: 0x04000229 RID: 553
		public Extents lineExtents;
	}
}
