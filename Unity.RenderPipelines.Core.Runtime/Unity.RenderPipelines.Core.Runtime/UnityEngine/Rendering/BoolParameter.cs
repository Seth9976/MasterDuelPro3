using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020001F2 RID: 498
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class BoolParameter : VolumeParameter<bool>
	{
		// Token: 0x06000E14 RID: 3604 RVA: 0x00033FF7 File Offset: 0x000321F7
		public BoolParameter(bool value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x00034001 File Offset: 0x00032201
		public BoolParameter(bool value, BoolParameter.DisplayType displayType, bool overrideState = false)
			: base(value, overrideState)
		{
			this.displayType = displayType;
		}

		// Token: 0x0400094B RID: 2379
		[NonSerialized]
		public BoolParameter.DisplayType displayType;

		// Token: 0x020001F3 RID: 499
		public enum DisplayType
		{
			// Token: 0x0400094D RID: 2381
			Checkbox,
			// Token: 0x0400094E RID: 2382
			EnumPopup
		}
	}
}
