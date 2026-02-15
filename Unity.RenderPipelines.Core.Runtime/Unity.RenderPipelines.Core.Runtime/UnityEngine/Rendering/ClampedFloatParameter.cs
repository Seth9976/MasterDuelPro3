using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000204 RID: 516
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class ClampedFloatParameter : FloatParameter
	{
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000E3C RID: 3644 RVA: 0x0003415D File Offset: 0x0003235D
		// (set) Token: 0x06000E3D RID: 3645 RVA: 0x000341F9 File Offset: 0x000323F9
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

		// Token: 0x06000E3E RID: 3646 RVA: 0x00034213 File Offset: 0x00032413
		public ClampedFloatParameter(float value, float min, float max, bool overrideState = false)
			: base(value, overrideState)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x0400095B RID: 2395
		[NonSerialized]
		public float min;

		// Token: 0x0400095C RID: 2396
		[NonSerialized]
		public float max;
	}
}
