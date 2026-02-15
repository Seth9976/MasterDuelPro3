using System;

namespace TMPro
{
	// Token: 0x02000037 RID: 55
	[Serializable]
	public class TMP_Glyph : TMP_TextElement_Legacy
	{
		// Token: 0x060001A0 RID: 416 RVA: 0x00008DA8 File Offset: 0x00006FA8
		public static TMP_Glyph Clone(TMP_Glyph source)
		{
			return new TMP_Glyph
			{
				id = source.id,
				x = source.x,
				y = source.y,
				width = source.width,
				height = source.height,
				xOffset = source.xOffset,
				yOffset = source.yOffset,
				xAdvance = source.xAdvance,
				scale = source.scale
			};
		}
	}
}
