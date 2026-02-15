using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020001FF RID: 511
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpFloatParameter : VolumeParameter<float>
	{
		// Token: 0x06000E2F RID: 3631 RVA: 0x00034144 File Offset: 0x00032344
		public NoInterpFloatParameter(float value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
