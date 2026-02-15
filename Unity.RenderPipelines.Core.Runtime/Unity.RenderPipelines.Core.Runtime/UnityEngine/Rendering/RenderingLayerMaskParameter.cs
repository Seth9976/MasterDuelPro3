using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020001F5 RID: 501
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class RenderingLayerMaskParameter : VolumeParameter<RenderingLayerMask>
	{
		// Token: 0x06000E17 RID: 3607 RVA: 0x0003401C File Offset: 0x0003221C
		public RenderingLayerMaskParameter(RenderingLayerMask value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
