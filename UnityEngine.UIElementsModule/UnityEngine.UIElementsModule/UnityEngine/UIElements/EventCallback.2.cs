using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001C9 RID: 457
	// (Invoke) Token: 0x06000CF4 RID: 3316
	public delegate void EventCallback<in TEventType, in TCallbackArgs>(TEventType evt, TCallbackArgs userArgs);
}
