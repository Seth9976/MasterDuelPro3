using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000013 RID: 19
	[UsedByNativeCode]
	[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
	[Serializable]
	internal struct GlyphAnchorPoint
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00003848 File Offset: 0x00001A48
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00003860 File Offset: 0x00001A60
		public float xCoordinate
		{
			get
			{
				return this.m_XCoordinate;
			}
			set
			{
				this.m_XCoordinate = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x0000386C File Offset: 0x00001A6C
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00003884 File Offset: 0x00001A84
		public float yCoordinate
		{
			get
			{
				return this.m_YCoordinate;
			}
			set
			{
				this.m_YCoordinate = value;
			}
		}

		// Token: 0x0400007D RID: 125
		[NativeName("xPositionAdjustment")]
		[SerializeField]
		private float m_XCoordinate;

		// Token: 0x0400007E RID: 126
		[NativeName("yPositionAdjustment")]
		[SerializeField]
		private float m_YCoordinate;
	}
}
