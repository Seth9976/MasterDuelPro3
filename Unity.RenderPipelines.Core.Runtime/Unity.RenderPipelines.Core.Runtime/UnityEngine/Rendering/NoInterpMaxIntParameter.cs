using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020001FB RID: 507
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpMaxIntParameter : VolumeParameter<int>
	{
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000E24 RID: 3620 RVA: 0x00034042 File Offset: 0x00032242
		// (set) Token: 0x06000E25 RID: 3621 RVA: 0x000340B9 File Offset: 0x000322B9
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

		// Token: 0x06000E26 RID: 3622 RVA: 0x000340CD File Offset: 0x000322CD
		public NoInterpMaxIntParameter(int value, int max, bool overrideState = false)
			: base(value, overrideState)
		{
			this.max = max;
		}

		// Token: 0x04000952 RID: 2386
		[NonSerialized]
		public int max;
	}
}
