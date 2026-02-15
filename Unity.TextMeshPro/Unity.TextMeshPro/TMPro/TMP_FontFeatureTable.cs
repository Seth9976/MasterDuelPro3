using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

namespace TMPro
{
	// Token: 0x02000044 RID: 68
	[Serializable]
	public class TMP_FontFeatureTable
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x000098FE File Offset: 0x00007AFE
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x00009906 File Offset: 0x00007B06
		public List<MultipleSubstitutionRecord> multipleSubstitutionRecords
		{
			get
			{
				return this.m_MultipleSubstitutionRecords;
			}
			set
			{
				this.m_MultipleSubstitutionRecords = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x0000990F File Offset: 0x00007B0F
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x00009917 File Offset: 0x00007B17
		public List<LigatureSubstitutionRecord> ligatureRecords
		{
			get
			{
				return this.m_LigatureSubstitutionRecords;
			}
			set
			{
				this.m_LigatureSubstitutionRecords = value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00009920 File Offset: 0x00007B20
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x00009928 File Offset: 0x00007B28
		public List<GlyphPairAdjustmentRecord> glyphPairAdjustmentRecords
		{
			get
			{
				return this.m_GlyphPairAdjustmentRecords;
			}
			set
			{
				this.m_GlyphPairAdjustmentRecords = value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00009931 File Offset: 0x00007B31
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x00009939 File Offset: 0x00007B39
		public List<MarkToBaseAdjustmentRecord> MarkToBaseAdjustmentRecords
		{
			get
			{
				return this.m_MarkToBaseAdjustmentRecords;
			}
			set
			{
				this.m_MarkToBaseAdjustmentRecords = value;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00009942 File Offset: 0x00007B42
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x0000994A File Offset: 0x00007B4A
		public List<MarkToMarkAdjustmentRecord> MarkToMarkAdjustmentRecords
		{
			get
			{
				return this.m_MarkToMarkAdjustmentRecords;
			}
			set
			{
				this.m_MarkToMarkAdjustmentRecords = value;
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00009954 File Offset: 0x00007B54
		public TMP_FontFeatureTable()
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

		// Token: 0x060001DB RID: 475 RVA: 0x000099C0 File Offset: 0x00007BC0
		public void SortGlyphPairAdjustmentRecords()
		{
			if (this.m_GlyphPairAdjustmentRecords.Count > 0)
			{
				this.m_GlyphPairAdjustmentRecords = (from s in this.m_GlyphPairAdjustmentRecords
					orderby s.firstAdjustmentRecord.glyphIndex, s.secondAdjustmentRecord.glyphIndex
					select s).ToList<GlyphPairAdjustmentRecord>();
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00009A34 File Offset: 0x00007C34
		public void SortMarkToBaseAdjustmentRecords()
		{
			if (this.m_MarkToBaseAdjustmentRecords.Count > 0)
			{
				this.m_MarkToBaseAdjustmentRecords = (from s in this.m_MarkToBaseAdjustmentRecords
					orderby s.baseGlyphID, s.markGlyphID
					select s).ToList<MarkToBaseAdjustmentRecord>();
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00009AA8 File Offset: 0x00007CA8
		public void SortMarkToMarkAdjustmentRecords()
		{
			if (this.m_MarkToMarkAdjustmentRecords.Count > 0)
			{
				this.m_MarkToMarkAdjustmentRecords = (from s in this.m_MarkToMarkAdjustmentRecords
					orderby s.baseMarkGlyphID, s.combiningMarkGlyphID
					select s).ToList<MarkToMarkAdjustmentRecord>();
			}
		}

		// Token: 0x04000168 RID: 360
		[SerializeField]
		internal List<MultipleSubstitutionRecord> m_MultipleSubstitutionRecords;

		// Token: 0x04000169 RID: 361
		[SerializeField]
		internal List<LigatureSubstitutionRecord> m_LigatureSubstitutionRecords;

		// Token: 0x0400016A RID: 362
		[SerializeField]
		internal List<GlyphPairAdjustmentRecord> m_GlyphPairAdjustmentRecords;

		// Token: 0x0400016B RID: 363
		[SerializeField]
		internal List<MarkToBaseAdjustmentRecord> m_MarkToBaseAdjustmentRecords;

		// Token: 0x0400016C RID: 364
		[SerializeField]
		internal List<MarkToMarkAdjustmentRecord> m_MarkToMarkAdjustmentRecords;

		// Token: 0x0400016D RID: 365
		internal Dictionary<uint, List<LigatureSubstitutionRecord>> m_LigatureSubstitutionRecordLookup;

		// Token: 0x0400016E RID: 366
		internal Dictionary<uint, GlyphPairAdjustmentRecord> m_GlyphPairAdjustmentRecordLookup;

		// Token: 0x0400016F RID: 367
		internal Dictionary<uint, MarkToBaseAdjustmentRecord> m_MarkToBaseAdjustmentRecordLookup;

		// Token: 0x04000170 RID: 368
		internal Dictionary<uint, MarkToMarkAdjustmentRecord> m_MarkToMarkAdjustmentRecordLookup;
	}
}
