using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x0200003F RID: 63
	internal abstract class RuntimeElement : IInterval
	{
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000260 RID: 608
		public abstract long intervalStart { get; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000261 RID: 609
		public abstract long intervalEnd { get; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000262 RID: 610 RVA: 0x000089FD File Offset: 0x00006BFD
		// (set) Token: 0x06000263 RID: 611 RVA: 0x00008A05 File Offset: 0x00006C05
		public int intervalBit { get; set; }

		// Token: 0x170000AF RID: 175
		// (set) Token: 0x06000264 RID: 612
		public abstract bool enable { set; }

		// Token: 0x06000265 RID: 613
		public abstract void EvaluateAt(double localTime, FrameData frameData);

		// Token: 0x06000266 RID: 614
		public abstract void DisableAt(double localTime, double rootDuration, FrameData frameData);
	}
}
