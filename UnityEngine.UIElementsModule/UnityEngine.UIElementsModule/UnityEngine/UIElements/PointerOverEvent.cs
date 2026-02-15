using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200022F RID: 559
	[EventCategory(EventCategory.EnterLeave)]
	public sealed class PointerOverEvent : PointerEventBase<PointerOverEvent>
	{
		// Token: 0x06000F4C RID: 3916 RVA: 0x00042F00 File Offset: 0x00041100
		static PointerOverEvent()
		{
			EventBase<PointerOverEvent>.SetCreateFunction(() => new PointerOverEvent());
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x0003FCF2 File Offset: 0x0003DEF2
		internal override void Dispatch(BaseVisualElementPanel panel)
		{
			EventDispatchUtilities.DispatchToAssignedTarget(this, panel);
		}
	}
}
