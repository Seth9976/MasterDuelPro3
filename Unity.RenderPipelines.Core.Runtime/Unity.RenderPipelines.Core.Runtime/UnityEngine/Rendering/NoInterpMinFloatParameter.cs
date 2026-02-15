using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000201 RID: 513
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpMinFloatParameter : VolumeParameter<float>
	{
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000E33 RID: 3635 RVA: 0x0003415D File Offset: 0x0003235D
		// (set) Token: 0x06000E34 RID: 3636 RVA: 0x0003418A File Offset: 0x0003238A
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

		// Token: 0x06000E35 RID: 3637 RVA: 0x0003419E File Offset: 0x0003239E
		public NoInterpMinFloatParameter(float value, float min, bool overrideState = false)
			: base(value, overrideState)
		{
			this.min = min;
		}

		// Token: 0x04000958 RID: 2392
		[NonSerialized]
		public float min;
	}
}
