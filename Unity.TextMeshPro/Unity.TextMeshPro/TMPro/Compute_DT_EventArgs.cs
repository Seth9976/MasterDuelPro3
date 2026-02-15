using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x020000A7 RID: 167
	public class Compute_DT_EventArgs
	{
		// Token: 0x06000629 RID: 1577 RVA: 0x0002E4B5 File Offset: 0x0002C6B5
		public Compute_DT_EventArgs(Compute_DistanceTransform_EventTypes type, float progress)
		{
			this.EventType = type;
			this.ProgressPercentage = progress;
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0002E4CB File Offset: 0x0002C6CB
		public Compute_DT_EventArgs(Compute_DistanceTransform_EventTypes type, Color[] colors)
		{
			this.EventType = type;
			this.Colors = colors;
		}

		// Token: 0x040005AB RID: 1451
		public Compute_DistanceTransform_EventTypes EventType;

		// Token: 0x040005AC RID: 1452
		public float ProgressPercentage;

		// Token: 0x040005AD RID: 1453
		public Color[] Colors;
	}
}
