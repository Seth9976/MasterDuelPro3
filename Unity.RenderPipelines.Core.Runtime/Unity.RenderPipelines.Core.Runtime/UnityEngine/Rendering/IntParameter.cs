using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020001F6 RID: 502
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class IntParameter : VolumeParameter<int>
	{
		// Token: 0x06000E18 RID: 3608 RVA: 0x00034026 File Offset: 0x00032226
		public IntParameter(int value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x00034030 File Offset: 0x00032230
		public sealed override void Interp(int from, int to, float t)
		{
			this.m_Value = (int)((float)from + (float)(to - from) * t);
		}
	}
}
