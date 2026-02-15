using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001F8 RID: 504
	[EventCategory(EventCategory.EnterLeave)]
	public class MouseEnterEvent : MouseEventBase<MouseEnterEvent>
	{
		// Token: 0x06000E0F RID: 3599 RVA: 0x0003FCAC File Offset: 0x0003DEAC
		static MouseEnterEvent()
		{
			EventBase<MouseEnterEvent>.SetCreateFunction(() => new MouseEnterEvent());
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x0003FCC5 File Offset: 0x0003DEC5
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x0003FCD6 File Offset: 0x0003DED6
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.TricklesDown;
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x0003FCE1 File Offset: 0x0003DEE1
		public MouseEnterEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x0003FCF2 File Offset: 0x0003DEF2
		internal override void Dispatch(BaseVisualElementPanel panel)
		{
			EventDispatchUtilities.DispatchToAssignedTarget(this, panel);
		}
	}
}
