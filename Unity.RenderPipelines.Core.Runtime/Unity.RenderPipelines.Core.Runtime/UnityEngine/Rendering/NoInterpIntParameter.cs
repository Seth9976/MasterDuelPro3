using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020001F7 RID: 503
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpIntParameter : VolumeParameter<int>
	{
		// Token: 0x06000E1A RID: 3610 RVA: 0x00034026 File Offset: 0x00032226
		public NoInterpIntParameter(int value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
