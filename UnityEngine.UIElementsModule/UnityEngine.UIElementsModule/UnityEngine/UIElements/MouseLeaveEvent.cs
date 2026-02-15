using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001FA RID: 506
	[EventCategory(EventCategory.EnterLeave)]
	public class MouseLeaveEvent : MouseEventBase<MouseLeaveEvent>
	{
		// Token: 0x06000E17 RID: 3607 RVA: 0x0003FD10 File Offset: 0x0003DF10
		static MouseLeaveEvent()
		{
			EventBase<MouseLeaveEvent>.SetCreateFunction(() => new MouseLeaveEvent());
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x0003FD29 File Offset: 0x0003DF29
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x0003FCD6 File Offset: 0x0003DED6
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.TricklesDown;
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x0003FD3A File Offset: 0x0003DF3A
		public MouseLeaveEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x0003FCF2 File Offset: 0x0003DEF2
		internal override void Dispatch(BaseVisualElementPanel panel)
		{
			EventDispatchUtilities.DispatchToAssignedTarget(this, panel);
		}
	}
}
