using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020001FA RID: 506
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class MaxIntParameter : IntParameter
	{
		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000E21 RID: 3617 RVA: 0x00034042 File Offset: 0x00032242
		// (set) Token: 0x06000E22 RID: 3618 RVA: 0x00034094 File Offset: 0x00032294
		public override int value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = Mathf.Min(value, this.max);
			}
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x000340A8 File Offset: 0x000322A8
		public MaxIntParameter(int value, int max, bool overrideState = false)
			: base(value, overrideState)
		{
			this.max = max;
		}

		// Token: 0x04000951 RID: 2385
		[NonSerialized]
		public int max;
	}
}
