using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000202 RID: 514
	[EventCategory(EventCategory.EnterLeaveWindow)]
	public class MouseLeaveWindowEvent : MouseEventBase<MouseLeaveWindowEvent>
	{
		// Token: 0x06000E36 RID: 3638 RVA: 0x0003FEAF File Offset: 0x0003E0AF
		static MouseLeaveWindowEvent()
		{
			EventBase<MouseLeaveWindowEvent>.SetCreateFunction(() => new MouseLeaveWindowEvent());
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0003FEC8 File Offset: 0x0003E0C8
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x0003FE2C File Offset: 0x0003E02C
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles;
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x0003FED9 File Offset: 0x0003E0D9
		public MouseLeaveWindowEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x0003FEEC File Offset: 0x0003E0EC
		public new static MouseLeaveWindowEvent GetPooled(Event systemEvent)
		{
			bool flag = systemEvent != null;
			if (flag)
			{
				PointerDeviceState.ReleaseAllButtons(PointerId.mousePointerId);
			}
			return MouseEventBase<MouseLeaveWindowEvent>.GetPooled(systemEvent);
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x0003FF18 File Offset: 0x0003E118
		protected internal override void PostDispatch(IPanel panel)
		{
			bool flag = base.pressedButtons == 0;
			if (flag)
			{
				BaseVisualElementPanel baseVisualElementPanel = panel as BaseVisualElementPanel;
				if (baseVisualElementPanel != null)
				{
					baseVisualElementPanel.ClearCachedElementUnderPointer(PointerId.mousePointerId, this);
				}
			}
			EventBase pointerEvent = ((IMouseEventInternal)this).sourcePointerEvent as EventBase;
			bool flag2 = pointerEvent == null;
			if (flag2)
			{
				BaseVisualElementPanel baseVisualElementPanel2 = panel as BaseVisualElementPanel;
				if (baseVisualElementPanel2 != null)
				{
					baseVisualElementPanel2.CommitElementUnderPointers();
				}
			}
			base.PostDispatch(panel);
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x0003FF7D File Offset: 0x0003E17D
		internal override void Dispatch(BaseVisualElementPanel panel)
		{
			EventDispatchUtilities.DispatchToCachedElementUnderPointerOrPanelRoot(this, panel, PointerId.mousePointerId, base.mousePosition);
		}
	}
}
