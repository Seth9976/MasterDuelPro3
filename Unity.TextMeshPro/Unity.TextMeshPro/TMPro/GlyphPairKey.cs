using System;

namespace TMPro
{
	// Token: 0x0200004A RID: 74
	public struct GlyphPairKey
	{
		// Token: 0x06000200 RID: 512 RVA: 0x00009D8C File Offset: 0x00007F8C
		public GlyphPairKey(uint firstGlyphIndex, uint secondGlyphIndex)
		{
			this.firstGlyphIndex = firstGlyphIndex;
			this.secondGlyphIndex = secondGlyphIndex;
			this.key = (secondGlyphIndex << 16) | firstGlyphIndex;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00009DA8 File Offset: 0x00007FA8
		internal GlyphPairKey(TMP_GlyphPairAdjustmentRecord record)
		{
			this.firstGlyphIndex = record.firstAdjustmentRecord.glyphIndex;
			this.secondGlyphIndex = record.secondAdjustmentRecord.glyphIndex;
			this.key = (this.secondGlyphIndex << 16) | this.firstGlyphIndex;
		}

		// Token: 0x04000185 RID: 389
		public uint firstGlyphIndex;

		// Token: 0x04000186 RID: 390
		public uint secondGlyphIndex;

		// Token: 0x04000187 RID: 391
		public uint key;
	}
}
