using System;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

namespace TMPro
{
	// Token: 0x02000048 RID: 72
	[Serializable]
	public struct TMP_GlyphAdjustmentRecord
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00009CB6 File Offset: 0x00007EB6
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x00009CBE File Offset: 0x00007EBE
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

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00009CC7 File Offset: 0x00007EC7
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x00009CCF File Offset: 0x00007ECF
		public TMP_GlyphValueRecord glyphValueRecord
		{
			get
			{
				return this.m_GlyphValueRecord;
			}
			set
			{
				this.m_GlyphValueRecord = value;
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00009CD8 File Offset: 0x00007ED8
		public TMP_GlyphAdjustmentRecord(uint glyphIndex, TMP_GlyphValueRecord glyphValueRecord)
		{
			this.m_GlyphIndex = glyphIndex;
			this.m_GlyphValueRecord = glyphValueRecord;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00009CE8 File Offset: 0x00007EE8
		internal TMP_GlyphAdjustmentRecord(GlyphAdjustmentRecord adjustmentRecord)
		{
			this.m_GlyphIndex = adjustmentRecord.glyphIndex;
			this.m_GlyphValueRecord = new TMP_GlyphValueRecord(adjustmentRecord.glyphValueRecord);
		}

		// Token: 0x04000180 RID: 384
		[SerializeField]
		internal uint m_GlyphIndex;

		// Token: 0x04000181 RID: 385
		[SerializeField]
		internal TMP_GlyphValueRecord m_GlyphValueRecord;
	}
}
