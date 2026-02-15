using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020001F8 RID: 504
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class MinIntParameter : IntParameter
	{
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000E1B RID: 3611 RVA: 0x00034042 File Offset: 0x00032242
		// (set) Token: 0x06000E1C RID: 3612 RVA: 0x0003404A File Offset: 0x0003224A
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

		// Token: 0x06000E1D RID: 3613 RVA: 0x0003405E File Offset: 0x0003225E
		public MinIntParameter(int value, int min, bool overrideState = false)
			: base(value, overrideState)
		{
			this.min = min;
		}

		// Token: 0x0400094F RID: 2383
		[NonSerialized]
		public int min;
	}
}
