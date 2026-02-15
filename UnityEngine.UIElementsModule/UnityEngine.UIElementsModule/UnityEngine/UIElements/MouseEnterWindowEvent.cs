using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000200 RID: 512
	[EventCategory(EventCategory.EnterLeaveWindow)]
	public class MouseEnterWindowEvent : MouseEventBase<MouseEnterWindowEvent>
	{
		// Token: 0x06000E2D RID: 3629 RVA: 0x0003FE02 File Offset: 0x0003E002
		static MouseEnterWindowEvent()
		{
			EventBase<MouseEnterWindowEvent>.SetCreateFunction(() => new MouseEnterWindowEvent());
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x0003FE1B File Offset: 0x0003E01B
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x0003FE2C File Offset: 0x0003E02C
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles;
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x0003FE37 File Offset: 0x0003E037
		public MouseEnterWindowEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x0003FE48 File Offset: 0x0003E048
		protected internal override void PostDispatch(IPanel panel)
		{
			EventBase pointerEvent = ((IMouseEventInternal)this).sourcePointerEvent as EventBase;
			bool flag = pointerEvent == null;
			if (flag)
			{
				BaseVisualElementPanel baseVisualElementPanel = panel as BaseVisualElementPanel;
				if (baseVisualElementPanel != null)
				{
					baseVisualElementPanel.CommitElementUnderPointers();
				}
			}
			base.PostDispatch(panel);
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x0003FE86 File Offset: 0x0003E086
		internal override void Dispatch(BaseVisualElementPanel panel)
		{
			EventDispatchUtilities.DispatchToElementUnderPointerOrPanelRoot(this, panel, PointerId.mousePointerId, base.mousePosition);
		}
	}
}
