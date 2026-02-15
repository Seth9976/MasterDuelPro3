using System;
using System.Diagnostics;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000012 RID: 18
	[DebuggerDisplay("First glyphIndex = {m_FirstAdjustmentRecord.m_GlyphIndex},  Second glyphIndex = {m_SecondAdjustmentRecord.m_GlyphIndex}")]
	[UsedByNativeCode]
	[Serializable]
	public struct GlyphPairAdjustmentRecord : IEquatable<GlyphPairAdjustmentRecord>
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000097 RID: 151 RVA: 0x0000376C File Offset: 0x0000196C
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00003784 File Offset: 0x00001984
		public GlyphAdjustmentRecord firstAdjustmentRecord
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

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003790 File Offset: 0x00001990
		public GlyphAdjustmentRecord secondAdjustmentRecord
		{
			get
			{
				return this.m_SecondAdjustmentRecord;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000037A8 File Offset: 0x000019A8
		public FontFeatureLookupFlags featureLookupFlags
		{
			get
			{
				return this.m_FeatureLookupFlags;
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000037C0 File Offset: 0x000019C0
		public GlyphPairAdjustmentRecord(GlyphAdjustmentRecord firstAdjustmentRecord, GlyphAdjustmentRecord secondAdjustmentRecord)
		{
			this.m_FirstAdjustmentRecord = firstAdjustmentRecord;
			this.m_SecondAdjustmentRecord = secondAdjustmentRecord;
			this.m_FeatureLookupFlags = FontFeatureLookupFlags.None;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000037D8 File Offset: 0x000019D8
		[ExcludeFromDocs]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000037FC File Offset: 0x000019FC
		[ExcludeFromDocs]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003820 File Offset: 0x00001A20
		[ExcludeFromDocs]
		public bool Equals(GlyphPairAdjustmentRecord other)
		{
			return base.Equals(other);
		}

		// Token: 0x0400007A RID: 122
		[SerializeField]
		[NativeName("firstAdjustmentRecord")]
		private GlyphAdjustmentRecord m_FirstAdjustmentRecord;

		// Token: 0x0400007B RID: 123
		[SerializeField]
		[NativeName("secondAdjustmentRecord")]
		private GlyphAdjustmentRecord m_SecondAdjustmentRecord;

		// Token: 0x0400007C RID: 124
		[SerializeField]
		private FontFeatureLookupFlags m_FeatureLookupFlags;
	}
}
