using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000016 RID: 22
	[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
	[UsedByNativeCode]
	[Serializable]
	internal struct MarkToMarkAdjustmentRecord
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003968 File Offset: 0x00001B68
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00003980 File Offset: 0x00001B80
		public uint baseMarkGlyphID
		{
			get
			{
				return this.m_BaseMarkGlyphID;
			}
			set
			{
				this.m_BaseMarkGlyphID = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x0000398C File Offset: 0x00001B8C
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x000039A4 File Offset: 0x00001BA4
		public GlyphAnchorPoint baseMarkGlyphAnchorPoint
		{
			get
			{
				return this.m_BaseMarkGlyphAnchorPoint;
			}
			set
			{
				this.m_BaseMarkGlyphAnchorPoint = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x000039B0 File Offset: 0x00001BB0
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x000039C8 File Offset: 0x00001BC8
		public uint combiningMarkGlyphID
		{
			get
			{
				return this.m_CombiningMarkGlyphID;
			}
			set
			{
				this.m_CombiningMarkGlyphID = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x000039D4 File Offset: 0x00001BD4
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x000039EC File Offset: 0x00001BEC
		public MarkPositionAdjustment combiningMarkPositionAdjustment
		{
			get
			{
				return this.m_CombiningMarkPositionAdjustment;
			}
			set
			{
				this.m_CombiningMarkPositionAdjustment = value;
			}
		}

		// Token: 0x04000085 RID: 133
		[NativeName("baseMarkGlyphID")]
		[SerializeField]
		private uint m_BaseMarkGlyphID;

		// Token: 0x04000086 RID: 134
		[NativeName("baseMarkAnchor")]
		[SerializeField]
		private GlyphAnchorPoint m_BaseMarkGlyphAnchorPoint;

		// Token: 0x04000087 RID: 135
		[NativeName("combiningMarkGlyphID")]
		[SerializeField]
		private uint m_CombiningMarkGlyphID;

		// Token: 0x04000088 RID: 136
		[NativeName("combiningMarkPositionAdjustment")]
		[SerializeField]
		private MarkPositionAdjustment m_CombiningMarkPositionAdjustment;
	}
}
