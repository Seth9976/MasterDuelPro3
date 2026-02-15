using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200003D RID: 61
	[Serializable]
	public abstract class TextElement
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000192 RID: 402 RVA: 0x0000B664 File Offset: 0x00009864
		public TextElementType elementType
		{
			get
			{
				return this.m_ElementType;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000B67C File Offset: 0x0000987C
		// (set) Token: 0x06000194 RID: 404 RVA: 0x0000B694 File Offset: 0x00009894
		public uint unicode
		{
			get
			{
				return this.m_Unicode;
			}
			set
			{
				this.m_Unicode = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000195 RID: 405 RVA: 0x0000B6A0 File Offset: 0x000098A0
		// (set) Token: 0x06000196 RID: 406 RVA: 0x0000B6B8 File Offset: 0x000098B8
		public TextAsset textAsset
		{
			get
			{
				return this.m_TextAsset;
			}
			set
			{
				this.m_TextAsset = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000B6C4 File Offset: 0x000098C4
		// (set) Token: 0x06000198 RID: 408 RVA: 0x0000B6DC File Offset: 0x000098DC
		public Glyph glyph
		{
			get
			{
				return this.m_Glyph;
			}
			set
			{
				this.m_Glyph = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000B6E8 File Offset: 0x000098E8
		// (set) Token: 0x0600019A RID: 410 RVA: 0x0000B700 File Offset: 0x00009900
		public uint glyphIndex
		{
			get
			{
				return this.m_GlyphIndex;
			}
			set
			{
				this.m_GlyphIndex = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600019B RID: 411 RVA: 0x0000B70C File Offset: 0x0000990C
		// (set) Token: 0x0600019C RID: 412 RVA: 0x0000B724 File Offset: 0x00009924
		public float scale
		{
			get
			{
				return this.m_Scale;
			}
			set
			{
				this.m_Scale = value;
			}
		}

		// Token: 0x0400018E RID: 398
		[SerializeField]
		protected TextElementType m_ElementType;

		// Token: 0x0400018F RID: 399
		[SerializeField]
		internal uint m_Unicode;

		// Token: 0x04000190 RID: 400
		internal TextAsset m_TextAsset;

		// Token: 0x04000191 RID: 401
		internal Glyph m_Glyph;

		// Token: 0x04000192 RID: 402
		[SerializeField]
		internal uint m_GlyphIndex;

		// Token: 0x04000193 RID: 403
		[SerializeField]
		internal float m_Scale;
	}
}
