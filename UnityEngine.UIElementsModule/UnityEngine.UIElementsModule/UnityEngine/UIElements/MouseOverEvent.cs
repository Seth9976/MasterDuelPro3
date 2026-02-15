using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001FC RID: 508
	[EventCategory(EventCategory.EnterLeave)]
	public class MouseOverEvent : MouseEventBase<MouseOverEvent>
	{
		// Token: 0x06000E1F RID: 3615 RVA: 0x0003FD5E File Offset: 0x0003DF5E
		static MouseOverEvent()
		{
			EventBase<MouseOverEvent>.SetCreateFunction(() => new MouseOverEvent());
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x0003FCF2 File Offset: 0x0003DEF2
		internal override void Dispatch(BaseVisualElementPanel panel)
		{
			EventDispatchUtilities.DispatchToAssignedTarget(this, panel);
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x0003FD77 File Offset: 0x0003DF77
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			base.elementTarget.UpdateCursorStyle(this.eventTypeId);
		}
	}
}
