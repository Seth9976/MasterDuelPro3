using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000284 RID: 644
	internal class RepaintData
	{
		// Token: 0x1700032F RID: 815
		// (get) Token: 0x0600110F RID: 4367 RVA: 0x000490CC File Offset: 0x000472CC
		public Matrix4x4 currentOffset { get; } = Matrix4x4.identity;

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06001110 RID: 4368 RVA: 0x000490D4 File Offset: 0x000472D4
		public Rect currentWorldClip { get; }

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06001111 RID: 4369 RVA: 0x000490DC File Offset: 0x000472DC
		// (set) Token: 0x06001112 RID: 4370 RVA: 0x000490E4 File Offset: 0x000472E4
		public Event repaintEvent { get; set; }
	}
}
