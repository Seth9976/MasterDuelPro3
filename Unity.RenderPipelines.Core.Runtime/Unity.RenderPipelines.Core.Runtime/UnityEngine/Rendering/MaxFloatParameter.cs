using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000202 RID: 514
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class MaxFloatParameter : FloatParameter
	{
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000E36 RID: 3638 RVA: 0x0003415D File Offset: 0x0003235D
		// (set) Token: 0x06000E37 RID: 3639 RVA: 0x000341AF File Offset: 0x000323AF
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

		// Token: 0x06000E38 RID: 3640 RVA: 0x000341C3 File Offset: 0x000323C3
		public MaxFloatParameter(float value, float max, bool overrideState = false)
			: base(value, overrideState)
		{
			this.max = max;
		}

		// Token: 0x04000959 RID: 2393
		[NonSerialized]
		public float max;
	}
}
