using System;
using UnityEngine;

namespace Cinemachine
{
	// Token: 0x02000079 RID: 121
	[Serializable]
	public struct AxisBase
	{
		// Token: 0x060002FB RID: 763 RVA: 0x00012FE4 File Offset: 0x000111E4
		public void Validate()
		{
			this.m_MaxValue = Mathf.Clamp(this.m_MaxValue, this.m_MinValue, this.m_MaxValue);
		}

		// Token: 0x040002C1 RID: 705
		[NoSaveDuringPlay]
		[Tooltip("The current value of the axis.")]
		public float m_Value;

		// Token: 0x040002C2 RID: 706
		[Tooltip("The minimum value for the axis")]
		public float m_MinValue;

		// Token: 0x040002C3 RID: 707
		[Tooltip("The maximum value for the axis")]
		public float m_MaxValue;

		// Token: 0x040002C4 RID: 708
		[Tooltip("If checked, then the axis will wrap around at the min/max values, forming a loop")]
		public bool m_Wrap;
	}
}
