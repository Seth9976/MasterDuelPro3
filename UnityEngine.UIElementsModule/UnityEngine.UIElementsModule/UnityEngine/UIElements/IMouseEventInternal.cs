using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001EC RID: 492
	internal interface IMouseEventInternal
	{
		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000DBC RID: 3516
		// (set) Token: 0x06000DBD RID: 3517
		bool triggeredByOS { get; set; }

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000DBE RID: 3518
		// (set) Token: 0x06000DBF RID: 3519
		IPointerEvent sourcePointerEvent { get; set; }
	}
}
