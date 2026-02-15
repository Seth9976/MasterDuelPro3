using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x0200021B RID: 539
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class MaterialParameter : VolumeParameter<Material>
	{
		// Token: 0x06000E7A RID: 3706 RVA: 0x000349E6 File Offset: 0x00032BE6
		public MaterialParameter(Material value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
