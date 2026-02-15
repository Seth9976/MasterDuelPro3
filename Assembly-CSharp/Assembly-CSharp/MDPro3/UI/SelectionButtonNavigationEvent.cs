using System;
using UnityEngine.Events;

namespace MDPro3.UI
{
	// Token: 0x0200139E RID: 5022
	[Serializable]
	public class SelectionButtonNavigationEvent
	{
		// Token: 0x0400D004 RID: 53252
		public UnityEvent onLeftNavigation;

		// Token: 0x0400D005 RID: 53253
		public UnityEvent onRightNavigation;

		// Token: 0x0400D006 RID: 53254
		public UnityEvent onUpNavigation;

		// Token: 0x0400D007 RID: 53255
		public UnityEvent onDownNavigation;
	}
}
