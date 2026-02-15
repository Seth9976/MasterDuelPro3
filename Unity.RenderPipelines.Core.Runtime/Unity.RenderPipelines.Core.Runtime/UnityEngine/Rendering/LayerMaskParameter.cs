using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020001F4 RID: 500
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class LayerMaskParameter : VolumeParameter<LayerMask>
	{
		// Token: 0x06000E16 RID: 3606 RVA: 0x00034012 File Offset: 0x00032212
		public LayerMaskParameter(LayerMask value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
