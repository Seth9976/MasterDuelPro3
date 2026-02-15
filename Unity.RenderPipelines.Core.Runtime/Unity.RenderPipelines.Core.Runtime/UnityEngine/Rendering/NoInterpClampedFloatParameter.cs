using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000205 RID: 517
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpClampedFloatParameter : VolumeParameter<float>
	{
		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000E3F RID: 3647 RVA: 0x0003415D File Offset: 0x0003235D
		// (set) Token: 0x06000E40 RID: 3648 RVA: 0x0003422C File Offset: 0x0003242C
		public override float value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = Mathf.Clamp(value, this.min, this.max);
			}
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x00034246 File Offset: 0x00032446
		public NoInterpClampedFloatParameter(float value, float min, float max, bool overrideState = false)
			: base(value, overrideState)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x0400095D RID: 2397
		[NonSerialized]
		public float min;

		// Token: 0x0400095E RID: 2398
		[NonSerialized]
		public float max;
	}
}
