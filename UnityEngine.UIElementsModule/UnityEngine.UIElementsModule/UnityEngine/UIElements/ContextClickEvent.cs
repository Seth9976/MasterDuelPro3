using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001F4 RID: 500
	public class ContextClickEvent : MouseEventBase<ContextClickEvent>
	{
		// Token: 0x06000DFD RID: 3581 RVA: 0x0003FB57 File Offset: 0x0003DD57
		static ContextClickEvent()
		{
			EventBase<ContextClickEvent>.SetCreateFunction(() => new ContextClickEvent());
		}
	}
}
