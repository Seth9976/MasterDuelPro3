using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020001F9 RID: 505
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpMinIntParameter : VolumeParameter<int>
	{
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000E1E RID: 3614 RVA: 0x00034042 File Offset: 0x00032242
		// (set) Token: 0x06000E1F RID: 3615 RVA: 0x0003406F File Offset: 0x0003226F
		public override int value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = Mathf.Max(value, this.min);
			}
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x00034083 File Offset: 0x00032283
		public NoInterpMinIntParameter(int value, int min, bool overrideState = false)
			: base(value, overrideState)
		{
			this.min = min;
		}

		// Token: 0x04000950 RID: 2384
		[NonSerialized]
		public int min;
	}
}
