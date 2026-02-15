using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020001FE RID: 510
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class FloatParameter : VolumeParameter<float>
	{
		// Token: 0x06000E2D RID: 3629 RVA: 0x00034144 File Offset: 0x00032344
		public FloatParameter(float value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x0003414E File Offset: 0x0003234E
		public sealed override void Interp(float from, float to, float t)
		{
			this.m_Value = from + (to - from) * t;
		}
	}
}
