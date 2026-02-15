using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200021D RID: 541
	internal interface IPointerEventInternal
	{
		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000EBD RID: 3773
		// (set) Token: 0x06000EBE RID: 3774
		bool triggeredByOS { get; set; }

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000EBF RID: 3775
		// (set) Token: 0x06000EC0 RID: 3776
		IMouseEvent compatibilityMouseEvent { get; set; }

		// Token: 0x170002B4 RID: 692
		// (set) Token: 0x06000EC1 RID: 3777
		int displayIndex { set; }
	}
}
