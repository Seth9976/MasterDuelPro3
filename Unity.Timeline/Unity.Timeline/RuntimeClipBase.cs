using System;

namespace UnityEngine.Timeline
{
	// Token: 0x0200003E RID: 62
	internal abstract class RuntimeClipBase : RuntimeElement
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600025B RID: 603
		public abstract double start { get; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600025C RID: 604
		public abstract double duration { get; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600025D RID: 605 RVA: 0x000089D4 File Offset: 0x00006BD4
		public override long intervalStart
		{
			get
			{
				return DiscreteTime.GetNearestTick(this.start);
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600025E RID: 606 RVA: 0x000089E1 File Offset: 0x00006BE1
		public override long intervalEnd
		{
			get
			{
				return DiscreteTime.GetNearestTick(this.start + this.duration);
			}
		}
	}
}
