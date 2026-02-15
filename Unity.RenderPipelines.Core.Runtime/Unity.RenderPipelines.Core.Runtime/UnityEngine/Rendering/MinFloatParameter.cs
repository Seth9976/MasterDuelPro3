using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000200 RID: 512
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class MinFloatParameter : FloatParameter
	{
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x0003415D File Offset: 0x0003235D
		// (set) Token: 0x06000E31 RID: 3633 RVA: 0x00034165 File Offset: 0x00032365
		public override float value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = Mathf.Max(value, this.min);
			}
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x00034179 File Offset: 0x00032379
		public MinFloatParameter(float value, float min, bool overrideState = false)
			: base(value, overrideState)
		{
			this.min = min;
		}

		// Token: 0x04000957 RID: 2391
		[NonSerialized]
		public float min;
	}
}
