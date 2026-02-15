using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x0200000B RID: 11
	[Serializable]
	public struct MarkToBaseAdjustmentRecord
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000023DD File Offset: 0x000005DD
		// (set) Token: 0x0600001D RID: 29 RVA: 0x000023E5 File Offset: 0x000005E5
		public uint baseGlyphID
		{
			get
			{
				return this.m_BaseGlyphID;
			}
			set
			{
				this.m_BaseGlyphID = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000023EE File Offset: 0x000005EE
		// (set) Token: 0x0600001F RID: 31 RVA: 0x000023F6 File Offset: 0x000005F6
		public GlyphAnchorPoint baseGlyphAnchorPoint
		{
			get
			{
				return this.m_BaseGlyphAnchorPoint;
			}
			set
			{
				this.m_BaseGlyphAnchorPoint = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000023FF File Offset: 0x000005FF
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002407 File Offset: 0x00000607
		public uint markGlyphID
		{
			get
			{
				return this.m_MarkGlyphID;
			}
			set
			{
				this.m_MarkGlyphID = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002410 File Offset: 0x00000610
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002418 File Offset: 0x00000618
		public MarkPositionAdjustment markPositionAdjustment
		{
			get
			{
				return this.m_MarkPositionAdjustment;
			}
			set
			{
				this.m_MarkPositionAdjustment = value;
			}
		}

		// Token: 0x04000017 RID: 23
		[SerializeField]
		private uint m_BaseGlyphID;

		// Token: 0x04000018 RID: 24
		[SerializeField]
		private GlyphAnchorPoint m_BaseGlyphAnchorPoint;

		// Token: 0x04000019 RID: 25
		[SerializeField]
		private uint m_MarkGlyphID;

		// Token: 0x0400001A RID: 26
		[SerializeField]
		private MarkPositionAdjustment m_MarkPositionAdjustment;
	}
}
