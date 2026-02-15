using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000014 RID: 20
	[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
	[UsedByNativeCode]
	[Serializable]
	internal struct MarkPositionAdjustment
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003890 File Offset: 0x00001A90
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x000038A8 File Offset: 0x00001AA8
		public float xPositionAdjustment
		{
			get
			{
				return this.m_XPositionAdjustment;
			}
			set
			{
				this.m_XPositionAdjustment = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000038B4 File Offset: 0x00001AB4
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x000038CC File Offset: 0x00001ACC
		public float yPositionAdjustment
		{
			get
			{
				return this.m_YPositionAdjustment;
			}
			set
			{
				this.m_YPositionAdjustment = value;
			}
		}

		// Token: 0x0400007F RID: 127
		[SerializeField]
		[NativeName("xCoordinate")]
		private float m_XPositionAdjustment;

		// Token: 0x04000080 RID: 128
		[SerializeField]
		[NativeName("yCoordinate")]
		private float m_YPositionAdjustment;
	}
}
