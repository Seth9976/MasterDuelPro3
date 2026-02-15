using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000203 RID: 515
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpMaxFloatParameter : VolumeParameter<float>
	{
		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000E39 RID: 3641 RVA: 0x0003415D File Offset: 0x0003235D
		// (set) Token: 0x06000E3A RID: 3642 RVA: 0x000341D4 File Offset: 0x000323D4
		public override float value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = Mathf.Min(value, this.max);
			}
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x000341E8 File Offset: 0x000323E8
		public NoInterpMaxFloatParameter(float value, float max, bool overrideState = false)
			: base(value, overrideState)
		{
			this.max = max;
		}

		// Token: 0x0400095A RID: 2394
		[NonSerialized]
		public float max;
	}
}
