using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001EB RID: 491
	public interface IMouseEvent
	{
		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000DB5 RID: 3509
		EventModifiers modifiers { get; }

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000DB6 RID: 3510
		Vector2 mousePosition { get; }

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000DB7 RID: 3511
		Vector2 localMousePosition { get; }

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000DB8 RID: 3512
		Vector2 mouseDelta { get; }

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000DB9 RID: 3513
		int clickCount { get; }

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000DBA RID: 3514
		int button { get; }

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000DBB RID: 3515
		int pressedButtons { get; }
	}
}
