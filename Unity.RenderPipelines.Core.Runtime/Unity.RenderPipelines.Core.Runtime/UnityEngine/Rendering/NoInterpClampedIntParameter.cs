using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020001FD RID: 509
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpClampedIntParameter : VolumeParameter<int>
	{
		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000E2A RID: 3626 RVA: 0x00034042 File Offset: 0x00032242
		// (set) Token: 0x06000E2B RID: 3627 RVA: 0x00034111 File Offset: 0x00032311
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

		// Token: 0x06000E2C RID: 3628 RVA: 0x0003412B File Offset: 0x0003232B
		public NoInterpClampedIntParameter(int value, int min, int max, bool overrideState = false)
			: base(value, overrideState)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x04000955 RID: 2389
		[NonSerialized]
		public int min;

		// Token: 0x04000956 RID: 2390
		[NonSerialized]
		public int max;
	}
}
