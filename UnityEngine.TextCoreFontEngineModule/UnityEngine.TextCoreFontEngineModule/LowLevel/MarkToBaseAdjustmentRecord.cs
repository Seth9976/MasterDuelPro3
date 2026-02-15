using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000015 RID: 21
	[UsedByNativeCode]
	[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
	[Serializable]
	internal struct MarkToBaseAdjustmentRecord
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000038D8 File Offset: 0x00001AD8
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x000038F0 File Offset: 0x00001AF0
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

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000038FC File Offset: 0x00001AFC
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00003914 File Offset: 0x00001B14
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

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00003920 File Offset: 0x00001B20
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00003938 File Offset: 0x00001B38
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

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00003944 File Offset: 0x00001B44
		// (set) Token: 0x060000AE RID: 174 RVA: 0x0000395C File Offset: 0x00001B5C
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

		// Token: 0x04000081 RID: 129
		[SerializeField]
		[NativeName("baseGlyphID")]
		private uint m_BaseGlyphID;

		// Token: 0x04000082 RID: 130
		[SerializeField]
		[NativeName("baseAnchor")]
		private GlyphAnchorPoint m_BaseGlyphAnchorPoint;

		// Token: 0x04000083 RID: 131
		[SerializeField]
		[NativeName("markGlyphID")]
		private uint m_MarkGlyphID;

		// Token: 0x04000084 RID: 132
		[NativeName("markPositionAdjustment")]
		[SerializeField]
		private MarkPositionAdjustment m_MarkPositionAdjustment;
	}
}
