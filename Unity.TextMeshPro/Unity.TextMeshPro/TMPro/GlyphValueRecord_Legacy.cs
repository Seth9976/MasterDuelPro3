using System;
using UnityEngine.TextCore.LowLevel;

namespace TMPro
{
	// Token: 0x0200003B RID: 59
	[Serializable]
	public struct GlyphValueRecord_Legacy
	{
		// Token: 0x060001A4 RID: 420 RVA: 0x00008EEE File Offset: 0x000070EE
		internal GlyphValueRecord_Legacy(GlyphValueRecord valueRecord)
		{
			this.xPlacement = valueRecord.xPlacement;
			this.yPlacement = valueRecord.yPlacement;
			this.xAdvance = valueRecord.xAdvance;
			this.yAdvance = valueRecord.yAdvance;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00008F24 File Offset: 0x00007124
		public static GlyphValueRecord_Legacy operator +(GlyphValueRecord_Legacy a, GlyphValueRecord_Legacy b)
		{
			GlyphValueRecord_Legacy c;
			c.xPlacement = a.xPlacement + b.xPlacement;
			c.yPlacement = a.yPlacement + b.yPlacement;
			c.xAdvance = a.xAdvance + b.xAdvance;
			c.yAdvance = a.yAdvance + b.yAdvance;
			return c;
		}

		// Token: 0x04000150 RID: 336
		public float xPlacement;

		// Token: 0x04000151 RID: 337
		public float yPlacement;

		// Token: 0x04000152 RID: 338
		public float xAdvance;

		// Token: 0x04000153 RID: 339
		public float yAdvance;
	}
}
