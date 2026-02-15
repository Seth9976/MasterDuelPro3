using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200021E RID: 542
	internal interface IPointerOrMouseEvent
	{
		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000EC2 RID: 3778
		int pointerId { get; }

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000EC3 RID: 3779
		Vector3 position { get; }
	}
}
