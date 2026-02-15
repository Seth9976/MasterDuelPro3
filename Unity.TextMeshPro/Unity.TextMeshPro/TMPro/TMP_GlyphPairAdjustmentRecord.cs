using System;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

namespace TMPro
{
	// Token: 0x02000049 RID: 73
	[Serializable]
	public class TMP_GlyphPairAdjustmentRecord
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00009D09 File Offset: 0x00007F09
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x00009D11 File Offset: 0x00007F11
		public TMP_GlyphAdjustmentRecord firstAdjustmentRecord
		{
			get
			{
				return this.m_FirstAdjustmentRecord;
			}
			set
			{
				this.m_FirstAdjustmentRecord = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00009D1A File Offset: 0x00007F1A
		// (set) Token: 0x060001FB RID: 507 RVA: 0x00009D22 File Offset: 0x00007F22
		public TMP_GlyphAdjustmentRecord secondAdjustmentRecord
		{
			get
			{
				return this.m_SecondAdjustmentRecord;
			}
			set
			{
				this.m_SecondAdjustmentRecord = value;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00009D2B File Offset: 0x00007F2B
		// (set) Token: 0x060001FD RID: 509 RVA: 0x00009D33 File Offset: 0x00007F33
		public FontFeatureLookupFlags featureLookupFlags
		{
			get
			{
				return this.m_FeatureLookupFlags;
			}
			set
			{
				this.m_FeatureLookupFlags = value;
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00009D3C File Offset: 0x00007F3C
		public TMP_GlyphPairAdjustmentRecord(TMP_GlyphAdjustmentRecord firstAdjustmentRecord, TMP_GlyphAdjustmentRecord secondAdjustmentRecord)
		{
			this.m_FirstAdjustmentRecord = firstAdjustmentRecord;
			this.m_SecondAdjustmentRecord = secondAdjustmentRecord;
			this.m_FeatureLookupFlags = FontFeatureLookupFlags.None;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00009D59 File Offset: 0x00007F59
		internal TMP_GlyphPairAdjustmentRecord(GlyphPairAdjustmentRecord glyphPairAdjustmentRecord)
		{
			this.m_FirstAdjustmentRecord = new TMP_GlyphAdjustmentRecord(glyphPairAdjustmentRecord.firstAdjustmentRecord);
			this.m_SecondAdjustmentRecord = new TMP_GlyphAdjustmentRecord(glyphPairAdjustmentRecord.secondAdjustmentRecord);
			this.m_FeatureLookupFlags = FontFeatureLookupFlags.None;
		}

		// Token: 0x04000182 RID: 386
		[SerializeField]
		internal TMP_GlyphAdjustmentRecord m_FirstAdjustmentRecord;

		// Token: 0x04000183 RID: 387
		[SerializeField]
		internal TMP_GlyphAdjustmentRecord m_SecondAdjustmentRecord;

		// Token: 0x04000184 RID: 388
		[SerializeField]
		internal FontFeatureLookupFlags m_FeatureLookupFlags;
	}
}
