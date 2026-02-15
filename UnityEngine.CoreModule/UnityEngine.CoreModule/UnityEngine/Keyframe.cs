using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200008D RID: 141
	[RequiredByNativeCode]
	public struct Keyframe
	{
		// Token: 0x06000220 RID: 544 RVA: 0x00005958 File Offset: 0x00003B58
		public Keyframe(float time, float value)
		{
			this.m_Time = time;
			this.m_Value = value;
			this.m_InTangent = 0f;
			this.m_OutTangent = 0f;
			this.m_WeightedMode = 0;
			this.m_InWeight = 0f;
			this.m_OutWeight = 0f;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x000059A7 File Offset: 0x00003BA7
		public Keyframe(float time, float value, float inTangent, float outTangent)
		{
			this.m_Time = time;
			this.m_Value = value;
			this.m_InTangent = inTangent;
			this.m_OutTangent = outTangent;
			this.m_WeightedMode = 0;
			this.m_InWeight = 0f;
			this.m_OutWeight = 0f;
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000222 RID: 546 RVA: 0x000059E4 File Offset: 0x00003BE4
		// (set) Token: 0x06000223 RID: 547 RVA: 0x000059FC File Offset: 0x00003BFC
		public float time
		{
			get
			{
				return this.m_Time;
			}
			set
			{
				this.m_Time = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00005A08 File Offset: 0x00003C08
		// (set) Token: 0x06000225 RID: 549 RVA: 0x00005A20 File Offset: 0x00003C20
		public float value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00005A2C File Offset: 0x00003C2C
		// (set) Token: 0x06000227 RID: 551 RVA: 0x00005A44 File Offset: 0x00003C44
		public float inTangent
		{
			get
			{
				return this.m_InTangent;
			}
			set
			{
				this.m_InTangent = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00005A50 File Offset: 0x00003C50
		// (set) Token: 0x06000229 RID: 553 RVA: 0x00005A68 File Offset: 0x00003C68
		public float outTangent
		{
			get
			{
				return this.m_OutTangent;
			}
			set
			{
				this.m_OutTangent = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00005A74 File Offset: 0x00003C74
		// (set) Token: 0x0600022B RID: 555 RVA: 0x00005A8C File Offset: 0x00003C8C
		public float inWeight
		{
			get
			{
				return this.m_InWeight;
			}
			set
			{
				this.m_InWeight = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00005A98 File Offset: 0x00003C98
		// (set) Token: 0x0600022D RID: 557 RVA: 0x00005AB0 File Offset: 0x00003CB0
		public float outWeight
		{
			get
			{
				return this.m_OutWeight;
			}
			set
			{
				this.m_OutWeight = value;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00005ABC File Offset: 0x00003CBC
		// (set) Token: 0x0600022F RID: 559 RVA: 0x00005AD4 File Offset: 0x00003CD4
		public WeightedMode weightedMode
		{
			get
			{
				return (WeightedMode)this.m_WeightedMode;
			}
			set
			{
				this.m_WeightedMode = (int)value;
			}
		}

		// Token: 0x04000141 RID: 321
		private float m_Time;

		// Token: 0x04000142 RID: 322
		private float m_Value;

		// Token: 0x04000143 RID: 323
		private float m_InTangent;

		// Token: 0x04000144 RID: 324
		private float m_OutTangent;

		// Token: 0x04000145 RID: 325
		private int m_WeightedMode;

		// Token: 0x04000146 RID: 326
		private float m_InWeight;

		// Token: 0x04000147 RID: 327
		private float m_OutWeight;
	}
}
