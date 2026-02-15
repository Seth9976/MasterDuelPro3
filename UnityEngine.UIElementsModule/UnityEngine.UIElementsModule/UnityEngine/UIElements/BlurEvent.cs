using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001DA RID: 474
	public class BlurEvent : FocusEventBase<BlurEvent>
	{
		// Token: 0x06000D67 RID: 3431 RVA: 0x0003EC4D File Offset: 0x0003CE4D
		static BlurEvent()
		{
			EventBase<BlurEvent>.SetCreateFunction(() => new BlurEvent());
		}
	}
}
