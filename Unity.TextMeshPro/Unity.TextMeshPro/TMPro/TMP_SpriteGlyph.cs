using System;
using UnityEngine;
using UnityEngine.TextCore;

namespace TMPro
{
	// Token: 0x02000079 RID: 121
	[Serializable]
	public class TMP_SpriteGlyph : Glyph
	{
		// Token: 0x060003C9 RID: 969 RVA: 0x00014AA2 File Offset: 0x00012CA2
		public TMP_SpriteGlyph()
		{
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00014AAA File Offset: 0x00012CAA
		public TMP_SpriteGlyph(uint index, GlyphMetrics metrics, GlyphRect glyphRect, float scale, int atlasIndex)
		{
			base.index = index;
			base.metrics = metrics;
			base.glyphRect = glyphRect;
			base.scale = scale;
			base.atlasIndex = atlasIndex;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00014AD7 File Offset: 0x00012CD7
		public TMP_SpriteGlyph(uint index, GlyphMetrics metrics, GlyphRect glyphRect, float scale, int atlasIndex, Sprite sprite)
		{
			base.index = index;
			base.metrics = metrics;
			base.glyphRect = glyphRect;
			base.scale = scale;
			base.atlasIndex = atlasIndex;
			this.sprite = sprite;
		}

		// Token: 0x040003A5 RID: 933
		public Sprite sprite;
	}
}
