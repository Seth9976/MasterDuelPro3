using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000210 RID: 528
	public class NavigationSubmitEvent : NavigationEventBase<NavigationSubmitEvent>
	{
		// Token: 0x06000E74 RID: 3700 RVA: 0x00040AEF File Offset: 0x0003ECEF
		static NavigationSubmitEvent()
		{
			EventBase<NavigationSubmitEvent>.SetCreateFunction(() => new NavigationSubmitEvent());
		}
	}
}
