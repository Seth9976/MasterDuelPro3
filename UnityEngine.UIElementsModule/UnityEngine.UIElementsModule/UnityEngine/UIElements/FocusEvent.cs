using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001DE RID: 478
	public class FocusEvent : FocusEventBase<FocusEvent>
	{
		// Token: 0x06000D74 RID: 3444 RVA: 0x0003ECED File Offset: 0x0003CEED
		static FocusEvent()
		{
			EventBase<FocusEvent>.SetCreateFunction(() => new FocusEvent());
		}
	}
}
