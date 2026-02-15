using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001E6 RID: 486
	internal static class KeyboardEventExtensions
	{
		// Token: 0x06000DA0 RID: 3488 RVA: 0x0003F2B8 File Offset: 0x0003D4B8
		internal static bool ShouldSendNavigationMoveEvent(this KeyDownEvent e)
		{
			return e.keyCode == KeyCode.Tab && !e.ctrlKey && !e.altKey && !e.commandKey && !e.functionKey;
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x0003F2F8 File Offset: 0x0003D4F8
		internal static bool ShouldSendNavigationMoveEventRuntime(this Event e)
		{
			return e.type == EventType.KeyDown && e.keyCode == KeyCode.Tab;
		}
	}
}
