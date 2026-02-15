using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.TextCore.LowLevel;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200001A RID: 26
	[Serializable]
	public class FontFeatureTable
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00003D29 File Offset: 0x00001F29
		internal List<GlyphPairAdjustmentRecord> glyphPairAdjustmentRecords
		{
			get
			{
				return this.m_GlyphPairAdjustmentRecords;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00003D34 File Offset: 0x00001F34
		internal List<MarkToBaseAdjustmentRecord> MarkToBaseAdjustmentRecords
		{
			get
			{
				return this.m_MarkToBaseAdjustmentRecords;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00003D4C File Offset: 0x00001F4C
		internal List<MarkToMarkAdjustmentRecord> MarkToMarkAdjustmentRecords
		{
			get
			{
				return this.m_MarkToMarkAdjustmentRecords;
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003D64 File Offset: 0x00001F64
		internal FontFeatureTable()
		{
			this.m_LigatureSubstitutionRecords = new List<LigatureSubstitutionRecord>();
			this.m_LigatureSubstitutionRecordLookup = new Dictionary<uint, List<LigatureSubstitutionRecord>>();
			this.m_GlyphPairAdjustmentRecords = new List<GlyphPairAdjustmentRecord>();
			this.m_GlyphPairAdjustmentRecordLookup = new Dictionary<uint, GlyphPairAdjustmentRecord>();
			this.m_MarkToBaseAdjustmentRecords = new List<MarkToBaseAdjustmentRecord>();
			this.m_MarkToBaseAdjustmentRecordLookup = new Dictionary<uint, MarkToBaseAdjustmentRecord>();
			this.m_MarkToMarkAdjustmentRecords = new List<MarkToMarkAdjustmentRecord>();
			this.m_MarkToMarkAdjustmentRecordLookup = new Dictionary<uint, MarkToMarkAdjustmentRecord>();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003DD4 File Offset: 0x00001FD4
		public void SortGlyphPairAdjustmentRecords()
		{
			bool flag = this.m_GlyphPairAdjustmentRecords.Count > 1;
			if (flag)
			{
				this.m_GlyphPairAdjustmentRecords = (from s in this.m_GlyphPairAdjustmentRecords
					orderby s.firstAdjustmentRecord.glyphIndex, s.secondAdjustmentRecord.glyphIndex
					select s).ToList<GlyphPairAdjustmentRecord>();
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003E50 File Offset: 0x00002050
		public void SortMarkToBaseAdjustmentRecords()
		{
			bool flag = this.m_MarkToBaseAdjustmentRecords.Count > 0;
			if (flag)
			{
				this.m_MarkToBaseAdjustmentRecords = (from s in this.m_MarkToBaseAdjustmentRecords
					orderby s.baseGlyphID, s.markGlyphID
					select s).ToList<MarkToBaseAdjustmentRecord>();
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003ECC File Offset: 0x000020CC
		public void SortMarkToMarkAdjustmentRecords()
		{
			bool flag = this.m_MarkToMarkAdjustmentRecords.Count > 0;
			if (flag)
			{
				this.m_MarkToMarkAdjustmentRecords = (from s in this.m_MarkToMarkAdjustmentRecords
					orderby s.baseMarkGlyphID, s.combiningMarkGlyphID
					select s).ToList<MarkToMarkAdjustmentRecord>();
			}
		}

		// Token: 0x04000076 RID: 118
		[SerializeField]
		internal List<MultipleSubstitutionRecord> m_MultipleSubstitutionRecords;

		// Token: 0x04000077 RID: 119
		[SerializeField]
		internal List<LigatureSubstitutionRecord> m_LigatureSubstitutionRecords;

		// Token: 0x04000078 RID: 120
		[SerializeField]
		private List<GlyphPairAdjustmentRecord> m_GlyphPairAdjustmentRecords;

		// Token: 0x04000079 RID: 121
		[SerializeField]
		internal List<MarkToBaseAdjustmentRecord> m_MarkToBaseAdjustmentRecords;

		// Token: 0x0400007A RID: 122
		[SerializeField]
		internal List<MarkToMarkAdjustmentRecord> m_MarkToMarkAdjustmentRecords;

		// Token: 0x0400007B RID: 123
		internal Dictionary<uint, List<LigatureSubstitutionRecord>> m_LigatureSubstitutionRecordLookup;

		// Token: 0x0400007C RID: 124
		internal Dictionary<uint, GlyphPairAdjustmentRecord> m_GlyphPairAdjustmentRecordLookup;

		// Token: 0x0400007D RID: 125
		internal Dictionary<uint, MarkToBaseAdjustmentRecord> m_MarkToBaseAdjustmentRecordLookup;

		// Token: 0x0400007E RID: 126
		internal Dictionary<uint, MarkToMarkAdjustmentRecord> m_MarkToMarkAdjustmentRecordLookup;
	}
}
