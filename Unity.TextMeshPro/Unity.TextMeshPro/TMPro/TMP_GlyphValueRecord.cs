using System;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

namespace TMPro
{
	// Token: 0x02000047 RID: 71
	[Serializable]
	public struct TMP_GlyphValueRecord
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00009B84 File Offset: 0x00007D84
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x00009B8C File Offset: 0x00007D8C
		public float xPlacement
		{
			get
			{
				return this.m_XPlacement;
			}
			set
			{
				this.m_XPlacement = value;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00009B95 File Offset: 0x00007D95
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x00009B9D File Offset: 0x00007D9D
		public float yPlacement
		{
			get
			{
				return this.m_YPlacement;
			}
			set
			{
				this.m_YPlacement = value;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00009BA6 File Offset: 0x00007DA6
		// (set) Token: 0x060001EB RID: 491 RVA: 0x00009BAE File Offset: 0x00007DAE
		public float xAdvance
		{
			get
			{
				return this.m_XAdvance;
			}
			set
			{
				this.m_XAdvance = value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00009BB7 File Offset: 0x00007DB7
		// (set) Token: 0x060001ED RID: 493 RVA: 0x00009BBF File Offset: 0x00007DBF
		public float yAdvance
		{
			get
			{
				return this.m_YAdvance;
			}
			set
			{
				this.m_YAdvance = value;
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00009BC8 File Offset: 0x00007DC8
		public TMP_GlyphValueRecord(float xPlacement, float yPlacement, float xAdvance, float yAdvance)
		{
			this.m_XPlacement = xPlacement;
			this.m_YPlacement = yPlacement;
			this.m_XAdvance = xAdvance;
			this.m_YAdvance = yAdvance;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00009BE7 File Offset: 0x00007DE7
		internal TMP_GlyphValueRecord(GlyphValueRecord_Legacy valueRecord)
		{
			this.m_XPlacement = valueRecord.xPlacement;
			this.m_YPlacement = valueRecord.yPlacement;
			this.m_XAdvance = valueRecord.xAdvance;
			this.m_YAdvance = valueRecord.yAdvance;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00009C19 File Offset: 0x00007E19
		internal TMP_GlyphValueRecord(GlyphValueRecord valueRecord)
		{
			this.m_XPlacement = valueRecord.xPlacement;
			this.m_YPlacement = valueRecord.yPlacement;
			this.m_XAdvance = valueRecord.xAdvance;
			this.m_YAdvance = valueRecord.yAdvance;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00009C50 File Offset: 0x00007E50
		public static TMP_GlyphValueRecord operator +(TMP_GlyphValueRecord a, TMP_GlyphValueRecord b)
		{
			TMP_GlyphValueRecord c;
			c.m_XPlacement = a.xPlacement + b.xPlacement;
			c.m_YPlacement = a.yPlacement + b.yPlacement;
			c.m_XAdvance = a.xAdvance + b.xAdvance;
			c.m_YAdvance = a.yAdvance + b.yAdvance;
			return c;
		}

		// Token: 0x0400017C RID: 380
		[SerializeField]
		internal float m_XPlacement;

		// Token: 0x0400017D RID: 381
		[SerializeField]
		internal float m_YPlacement;

		// Token: 0x0400017E RID: 382
		[SerializeField]
		internal float m_XAdvance;

		// Token: 0x0400017F RID: 383
		[SerializeField]
		internal float m_YAdvance;
	}
}
