using System;
using UnityEngine;
using UnityEngine.TextCore;

namespace TMPro
{
	// Token: 0x02000094 RID: 148
	[Serializable]
	public class TMP_TextElement
	{
		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x000230A1 File Offset: 0x000212A1
		public TextElementType elementType
		{
			get
			{
				return this.m_ElementType;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x000230A9 File Offset: 0x000212A9
		// (set) Token: 0x0600057E RID: 1406 RVA: 0x000230B1 File Offset: 0x000212B1
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

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x000230BA File Offset: 0x000212BA
		// (set) Token: 0x06000580 RID: 1408 RVA: 0x000230C2 File Offset: 0x000212C2
		public TMP_Asset textAsset
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

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x000230CB File Offset: 0x000212CB
		// (set) Token: 0x06000582 RID: 1410 RVA: 0x000230D3 File Offset: 0x000212D3
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

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x000230DC File Offset: 0x000212DC
		// (set) Token: 0x06000584 RID: 1412 RVA: 0x000230E4 File Offset: 0x000212E4
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

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x000230ED File Offset: 0x000212ED
		// (set) Token: 0x06000586 RID: 1414 RVA: 0x000230F5 File Offset: 0x000212F5
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

		// Token: 0x04000537 RID: 1335
		[SerializeField]
		internal TextElementType m_ElementType;

		// Token: 0x04000538 RID: 1336
		[SerializeField]
		internal uint m_Unicode;

		// Token: 0x04000539 RID: 1337
		internal TMP_Asset m_TextAsset;

		// Token: 0x0400053A RID: 1338
		internal Glyph m_Glyph;

		// Token: 0x0400053B RID: 1339
		[SerializeField]
		internal uint m_GlyphIndex;

		// Token: 0x0400053C RID: 1340
		[SerializeField]
		internal float m_Scale;
	}
}
