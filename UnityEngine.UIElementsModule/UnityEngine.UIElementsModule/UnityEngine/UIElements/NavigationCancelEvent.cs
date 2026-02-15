using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200020E RID: 526
	public class NavigationCancelEvent : NavigationEventBase<NavigationCancelEvent>
	{
		// Token: 0x06000E6F RID: 3695 RVA: 0x00040ABA File Offset: 0x0003ECBA
		static NavigationCancelEvent()
		{
			EventBase<NavigationCancelEvent>.SetCreateFunction(() => new NavigationCancelEvent());
		}
	}
}
