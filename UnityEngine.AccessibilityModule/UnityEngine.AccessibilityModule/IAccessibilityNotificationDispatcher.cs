using System;

namespace UnityEngine.Accessibility
{
	// Token: 0x02000012 RID: 18
	public interface IAccessibilityNotificationDispatcher
	{
		// Token: 0x06000082 RID: 130
		void SendScreenChanged(AccessibilityNode nodeToFocus = null);
	}
}
