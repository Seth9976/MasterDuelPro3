using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020001FC RID: 508
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class ClampedIntParameter : IntParameter
	{
		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000E27 RID: 3623 RVA: 0x00034042 File Offset: 0x00032242
		// (set) Token: 0x06000E28 RID: 3624 RVA: 0x000340DE File Offset: 0x000322DE
		public override int value
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

		// Token: 0x06000E29 RID: 3625 RVA: 0x000340F8 File Offset: 0x000322F8
		public ClampedIntParameter(int value, int min, int max, bool overrideState = false)
			: base(value, overrideState)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x04000953 RID: 2387
		[NonSerialized]
		public int min;

		// Token: 0x04000954 RID: 2388
		[NonSerialized]
		public int max;
	}
}
