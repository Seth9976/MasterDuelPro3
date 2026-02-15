using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000078 RID: 120
	[Serializable]
	public class TMP_SpriteCharacter : TMP_TextElement
	{
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x000149DD File Offset: 0x00012BDD
		// (set) Token: 0x060003C4 RID: 964 RVA: 0x000149E5 File Offset: 0x00012BE5
		public string name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				this.m_Name = value;
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x000149EE File Offset: 0x00012BEE
		public TMP_SpriteCharacter()
		{
			this.m_ElementType = TextElementType.Sprite;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x000149FD File Offset: 0x00012BFD
		public TMP_SpriteCharacter(uint unicode, TMP_SpriteGlyph glyph)
		{
			this.m_ElementType = TextElementType.Sprite;
			base.unicode = unicode;
			base.glyphIndex = glyph.index;
			base.glyph = glyph;
			base.scale = 1f;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00014A31 File Offset: 0x00012C31
		public TMP_SpriteCharacter(uint unicode, TMP_SpriteAsset spriteAsset, TMP_SpriteGlyph glyph)
		{
			this.m_ElementType = TextElementType.Sprite;
			base.unicode = unicode;
			base.textAsset = spriteAsset;
			base.glyph = glyph;
			base.glyphIndex = glyph.index;
			base.scale = 1f;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00014A6C File Offset: 0x00012C6C
		internal TMP_SpriteCharacter(uint unicode, uint glyphIndex)
		{
			this.m_ElementType = TextElementType.Sprite;
			base.unicode = unicode;
			base.textAsset = null;
			base.glyph = null;
			base.glyphIndex = glyphIndex;
			base.scale = 1f;
		}

		// Token: 0x040003A4 RID: 932
		[SerializeField]
		private string m_Name;
	}
}
