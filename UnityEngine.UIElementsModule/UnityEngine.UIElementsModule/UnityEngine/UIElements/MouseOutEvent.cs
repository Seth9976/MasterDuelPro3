using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001FE RID: 510
	[EventCategory(EventCategory.EnterLeave)]
	public class MouseOutEvent : MouseEventBase<MouseOutEvent>
	{
		// Token: 0x06000E26 RID: 3622 RVA: 0x0003FDB0 File Offset: 0x0003DFB0
		static MouseOutEvent()
		{
			EventBase<MouseOutEvent>.SetCreateFunction(() => new MouseOutEvent());
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x0003FCF2 File Offset: 0x0003DEF2
		internal override void Dispatch(BaseVisualElementPanel panel)
		{
			EventDispatchUtilities.DispatchToAssignedTarget(this, panel);
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x0003FDC9 File Offset: 0x0003DFC9
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			base.elementTarget.UpdateCursorStyle(this.eventTypeId);
		}
	}
}
