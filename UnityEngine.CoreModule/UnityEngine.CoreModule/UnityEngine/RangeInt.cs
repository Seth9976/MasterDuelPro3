using System;

namespace UnityEngine
{
	// Token: 0x020001AE RID: 430
	public struct RangeInt
	{
		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06001105 RID: 4357 RVA: 0x000243CC File Offset: 0x000225CC
		public int end
		{
			get
			{
				return this.start + this.length;
			}
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x000243EB File Offset: 0x000225EB
		public RangeInt(int start, int length)
		{
			this.start = start;
			this.length = length;
		}

		// Token: 0x04000679 RID: 1657
		public int start;

		// Token: 0x0400067A RID: 1658
		public int length;
	}
}
