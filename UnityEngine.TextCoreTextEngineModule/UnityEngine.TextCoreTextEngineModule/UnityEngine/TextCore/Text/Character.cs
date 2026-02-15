using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000017 RID: 23
	[Serializable]
	public class Character : TextElement
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00003B87 File Offset: 0x00001D87
		public Character()
		{
			this.m_ElementType = TextElementType.Character;
			base.scale = 1f;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003BA4 File Offset: 0x00001DA4
		public Character(uint unicode, FontAsset fontAsset, Glyph glyph)
		{
			this.m_ElementType = TextElementType.Character;
			base.unicode = unicode;
			base.textAsset = fontAsset;
			base.glyph = glyph;
			base.glyphIndex = glyph.index;
			base.scale = 1f;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003BF1 File Offset: 0x00001DF1
		internal Character(uint unicode, uint glyphIndex)
		{
			this.m_ElementType = TextElementType.Character;
			base.unicode = unicode;
			base.textAsset = null;
			base.glyph = null;
			base.glyphIndex = glyphIndex;
			base.scale = 1f;
		}
	}
}
