using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace TMPro
{
	// Token: 0x0200003C RID: 60
	[Serializable]
	public class KerningPair
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00008F82 File Offset: 0x00007182
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x00008F8A File Offset: 0x0000718A
		public uint firstGlyph
		{
			get
			{
				return this.m_FirstGlyph;
			}
			set
			{
				this.m_FirstGlyph = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00008F93 File Offset: 0x00007193
		public GlyphValueRecord_Legacy firstGlyphAdjustments
		{
			get
			{
				return this.m_FirstGlyphAdjustments;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00008F9B File Offset: 0x0000719B
		// (set) Token: 0x060001AA RID: 426 RVA: 0x00008FA3 File Offset: 0x000071A3
		public uint secondGlyph
		{
			get
			{
				return this.m_SecondGlyph;
			}
			set
			{
				this.m_SecondGlyph = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00008FAC File Offset: 0x000071AC
		public GlyphValueRecord_Legacy secondGlyphAdjustments
		{
			get
			{
				return this.m_SecondGlyphAdjustments;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00008FB4 File Offset: 0x000071B4
		public bool ignoreSpacingAdjustments
		{
			get
			{
				return this.m_IgnoreSpacingAdjustments;
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00008FBC File Offset: 0x000071BC
		public KerningPair()
		{
			this.m_FirstGlyph = 0U;
			this.m_FirstGlyphAdjustments = default(GlyphValueRecord_Legacy);
			this.m_SecondGlyph = 0U;
			this.m_SecondGlyphAdjustments = default(GlyphValueRecord_Legacy);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00008FEA File Offset: 0x000071EA
		public KerningPair(uint left, uint right, float offset)
		{
			this.firstGlyph = left;
			this.m_SecondGlyph = right;
			this.xOffset = offset;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00009007 File Offset: 0x00007207
		public KerningPair(uint firstGlyph, GlyphValueRecord_Legacy firstGlyphAdjustments, uint secondGlyph, GlyphValueRecord_Legacy secondGlyphAdjustments)
		{
			this.m_FirstGlyph = firstGlyph;
			this.m_FirstGlyphAdjustments = firstGlyphAdjustments;
			this.m_SecondGlyph = secondGlyph;
			this.m_SecondGlyphAdjustments = secondGlyphAdjustments;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000902C File Offset: 0x0000722C
		internal void ConvertLegacyKerningData()
		{
			this.m_FirstGlyphAdjustments.xAdvance = this.xOffset;
		}

		// Token: 0x04000154 RID: 340
		[FormerlySerializedAs("AscII_Left")]
		[SerializeField]
		private uint m_FirstGlyph;

		// Token: 0x04000155 RID: 341
		[SerializeField]
		private GlyphValueRecord_Legacy m_FirstGlyphAdjustments;

		// Token: 0x04000156 RID: 342
		[FormerlySerializedAs("AscII_Right")]
		[SerializeField]
		private uint m_SecondGlyph;

		// Token: 0x04000157 RID: 343
		[SerializeField]
		private GlyphValueRecord_Legacy m_SecondGlyphAdjustments;

		// Token: 0x04000158 RID: 344
		[FormerlySerializedAs("XadvanceOffset")]
		public float xOffset;

		// Token: 0x04000159 RID: 345
		internal static KerningPair empty = new KerningPair(0U, default(GlyphValueRecord_Legacy), 0U, default(GlyphValueRecord_Legacy));

		// Token: 0x0400015A RID: 346
		[SerializeField]
		private bool m_IgnoreSpacingAdjustments;
	}
}
