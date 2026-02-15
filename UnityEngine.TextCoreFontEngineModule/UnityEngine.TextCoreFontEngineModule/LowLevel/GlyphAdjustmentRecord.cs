using System;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000011 RID: 17
	[UsedByNativeCode]
	[Serializable]
	public struct GlyphAdjustmentRecord : IEquatable<GlyphAdjustmentRecord>
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000091 RID: 145 RVA: 0x000036B8 File Offset: 0x000018B8
		public uint glyphIndex
		{
			get
			{
				return this.m_GlyphIndex;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000092 RID: 146 RVA: 0x000036D0 File Offset: 0x000018D0
		public GlyphValueRecord glyphValueRecord
		{
			get
			{
				return this.m_GlyphValueRecord;
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000036E8 File Offset: 0x000018E8
		public GlyphAdjustmentRecord(uint glyphIndex, GlyphValueRecord glyphValueRecord)
		{
			this.m_GlyphIndex = glyphIndex;
			this.m_GlyphValueRecord = glyphValueRecord;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000036FC File Offset: 0x000018FC
		[ExcludeFromDocs]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003720 File Offset: 0x00001920
		[ExcludeFromDocs]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003744 File Offset: 0x00001944
		[ExcludeFromDocs]
		public bool Equals(GlyphAdjustmentRecord other)
		{
			return base.Equals(other);
		}

		// Token: 0x04000078 RID: 120
		[SerializeField]
		[NativeName("glyphIndex")]
		private uint m_GlyphIndex;

		// Token: 0x04000079 RID: 121
		[SerializeField]
		[NativeName("glyphValueRecord")]
		private GlyphValueRecord m_GlyphValueRecord;
	}
}
