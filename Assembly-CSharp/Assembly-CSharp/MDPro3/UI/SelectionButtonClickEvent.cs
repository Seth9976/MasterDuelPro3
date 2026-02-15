using System;
using UnityEngine.Events;

namespace MDPro3.UI
{
	// Token: 0x020013A1 RID: 5025
	[Serializable]
	public class SelectionButtonClickEvent
	{
		// Token: 0x0400D00C RID: 53260
		public UnityEvent onLeftClick;

		// Token: 0x0400D00D RID: 53261
		public UnityEvent onMiddleClick;

		// Token: 0x0400D00E RID: 53262
		public UnityEvent onRightClick;
	}
}
