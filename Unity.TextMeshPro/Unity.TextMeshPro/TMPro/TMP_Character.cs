using System;
using UnityEngine.TextCore;

namespace TMPro
{
	// Token: 0x02000016 RID: 22
	[Serializable]
	public class TMP_Character : TMP_TextElement
	{
		// Token: 0x0600005D RID: 93 RVA: 0x000029DE File Offset: 0x00000BDE
		public TMP_Character()
		{
			this.m_ElementType = TextElementType.Character;
			base.scale = 1f;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000029F8 File Offset: 0x00000BF8
		public TMP_Character(uint unicode, Glyph glyph)
		{
			this.m_ElementType = TextElementType.Character;
			base.unicode = unicode;
			base.textAsset = null;
			base.glyph = glyph;
			base.glyphIndex = glyph.index;
			base.scale = 1f;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002A33 File Offset: 0x00000C33
		public TMP_Character(uint unicode, TMP_FontAsset fontAsset, Glyph glyph)
		{
			this.m_ElementType = TextElementType.Character;
			base.unicode = unicode;
			base.textAsset = fontAsset;
			base.glyph = glyph;
			base.glyphIndex = glyph.index;
			base.scale = 1f;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002A6E File Offset: 0x00000C6E
		internal TMP_Character(uint unicode, uint glyphIndex)
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
