using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000231 RID: 561
	[EventCategory(EventCategory.EnterLeave)]
	public sealed class PointerOutEvent : PointerEventBase<PointerOutEvent>
	{
		// Token: 0x06000F52 RID: 3922 RVA: 0x00042F35 File Offset: 0x00041135
		static PointerOutEvent()
		{
			EventBase<PointerOutEvent>.SetCreateFunction(() => new PointerOutEvent());
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x0003FCF2 File Offset: 0x0003DEF2
		internal override void Dispatch(BaseVisualElementPanel panel)
		{
			EventDispatchUtilities.DispatchToAssignedTarget(this, panel);
		}
	}
}
